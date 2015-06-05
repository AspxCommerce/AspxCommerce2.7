/*************************************************
 * Copyright (C) 2007 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*************************************************/

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;

namespace GCheckout.NotificationQueue {
	public class FileSystemNotificationQueue : INotificationQueue {
    private string _InboxDir = null;
    private string _InProcessDir = null;
    private string _SuccessDir = null;
    private string _FailureDir = null;
    private NotificationFileCollection _NFileCache;

    public FileSystemNotificationQueue(string InboxDir) {
      _InboxDir = InboxDir;
      if (!_InboxDir.EndsWith("\\")) _InboxDir += "\\";
    }
      
    public FileSystemNotificationQueue(string InboxDir, string InProcessDir,
      string SuccessDir, string FailureDir) {
      _InboxDir = InboxDir;
      if (!_InboxDir.EndsWith("\\")) _InboxDir += "\\";
      TestDir(_InboxDir);
      _InProcessDir = InProcessDir;
      if (!_InProcessDir.EndsWith("\\")) _InProcessDir += "\\";
      TestDir(_InProcessDir);
      _SuccessDir = SuccessDir;
      if (!_SuccessDir.EndsWith("\\")) _SuccessDir += "\\";
      TestDir(_SuccessDir);
      _FailureDir = FailureDir;
      if (!_FailureDir.EndsWith("\\")) _FailureDir += "\\";
      TestDir(_FailureDir);
    }

    private void TestDir(string Dir) {
      if (!Directory.Exists(Dir)) {
        throw new ApplicationException(
          string.Format("Can't find directory '{0}'!", Dir));
      }
      WriteFile(Dir + "test.txt", "FileSystemNotificationQueue test file.");
      File.Delete(Dir + "test.txt");
    }

    public void Send(NotificationQueueMessage M) {
      NotificationFile NFile = new NotificationFile(M.Id, M.Type, M.OrderId);
      WriteFile(_InboxDir + NFile.Name, M.Xml);
    }

    public NotificationQueueMessage Receive() {
      if (_InProcessDir == null) throw new ApplicationException(
        "Must use the 4 parameter contstructor to create a queue object " +
        "that supports the Receive() method.");
      NotificationQueueMessage RetVal = null;
      int SleepSeconds = 0;
      while (RetVal == null) {
        NotificationFile NFile = GetNextNotificationFile();
        if (NFile != null) {
          RetVal = new NotificationQueueMessage(NFile.Id, NFile.Type, 
            NFile.OrderId, ReadFile(_InboxDir + NFile.Name));
          File.Move(_InboxDir + NFile.Name, _InProcessDir + NFile.Name);
        }
        else {
          SleepSeconds = SleepSeconds * 2;
          if (SleepSeconds == 0) SleepSeconds = 1;
          if (SleepSeconds > 30) SleepSeconds = 30;
          Thread.Sleep(SleepSeconds * 1000);
        }
      }
      return RetVal;
    }

    private NotificationFile GetNextNotificationFile() {
      NotificationFile RetVal = null;
      if (_NFileCache == null || _NFileCache.Count == 0) {
        _NFileCache = ReadAllNotificationFilesFromDisk();
      }
      if (_NFileCache.Count > 0) {
        RetVal = _NFileCache.GetFileAt(0);
        _NFileCache.RemoveFileAt(0);
      }
      return RetVal;
    }

    private NotificationFileCollection ReadAllNotificationFilesFromDisk() {
      NotificationFileCollection RetVal = new NotificationFileCollection();
      string[] Files = Directory.GetFiles(_InboxDir, "*--*--*.xml");
      foreach (string FileName in Files) {
        FileInfo Info = new FileInfo(FileName);
        RetVal.Add(new NotificationFile(Info.Name, Info.CreationTime));
      }
      RetVal.RemoveNewFiles(30);
      RetVal.SortByAge();
      return RetVal;
    }

    public void ProcessingSucceeded(NotificationQueueMessage M) {
      NotificationFile NFile = new NotificationFile(M.Id, M.Type, M.OrderId);
      File.Move(_InProcessDir + NFile.Name, _SuccessDir + NFile.Name);
    }

    public void ProcessingFailed(NotificationQueueMessage M) {
      NotificationFile NFile = new NotificationFile(M.Id, M.Type, M.OrderId);
      File.Move(_InProcessDir + NFile.Name, _FailureDir + NFile.Name);
    }

    public int GetLength() {
      return ReadAllNotificationFilesFromDisk().Count;
    }

    private static string ReadFile(string FullFileName) {
      StreamReader SReader = new StreamReader(FullFileName, Encoding.UTF8);
      string RetVal = SReader.ReadToEnd();
      SReader.Close();
      return RetVal;
    }

    private static void WriteFile(string FullFileName, string Contents) {
      StreamWriter OutputFile = 
        new StreamWriter(FullFileName, false, Encoding.UTF8);
      OutputFile.Write(Contents);
      OutputFile.Close();
    }


	}
}
