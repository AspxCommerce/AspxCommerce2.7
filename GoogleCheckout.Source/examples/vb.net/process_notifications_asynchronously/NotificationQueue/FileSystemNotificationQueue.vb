'/*************************************************
' * Copyright (C) 2007 Google Inc.
' *
' * Licensed under the Apache License, Version 2.0 (the "License");
' * you may not use this file except in compliance with the License.
' * You may obtain a copy of the License at
' *
' *      http://www.apache.org/licenses/LICENSE-2.0
' *
' * Unless required by applicable law or agreed to in writing, software
' * distributed under the License is distributed on an "AS IS" BASIS,
' * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' * See the License for the specific language governing permissions and
' * limitations under the License.
'*************************************************/
Imports System
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Threading

Namespace GCheckout.NotificationQueue

    Public Class FileSystemNotificationQueue
        Implements INotificationQueue

        Private _InboxDir As String = Nothing
        Private _InProcessDir As String = Nothing
        Private _SuccessDir As String = Nothing
        Private _FailureDir As String = Nothing
        Private _NFileCache As NotificationFileCollection

        Public Sub New(ByVal InboxDir As String)
            _InboxDir = InboxDir
            If Not _InboxDir.EndsWith("\") Then
                _InboxDir = (_InboxDir + "\")
            End If
        End Sub

        Public Sub New(ByVal InboxDir As String, ByVal InProcessDir As String, ByVal SuccessDir As String, ByVal FailureDir As String)
            _InboxDir = InboxDir
            If Not _InboxDir.EndsWith("\") Then
                _InboxDir = (_InboxDir + "\")
            End If
            TestDir(_InboxDir)
            _InProcessDir = InProcessDir
            If Not _InProcessDir.EndsWith("\") Then
                _InProcessDir = (_InProcessDir + "\")
            End If
            TestDir(_InProcessDir)
            _SuccessDir = SuccessDir
            If Not _SuccessDir.EndsWith("\") Then
                _SuccessDir = (_SuccessDir + "\")
            End If
            TestDir(_SuccessDir)
            _FailureDir = FailureDir
            If Not _FailureDir.EndsWith("\") Then
                _FailureDir = (_FailureDir + "\")
            End If
            TestDir(_FailureDir)
        End Sub

        Private Sub TestDir(ByVal Dir As String)
            If Not Directory.Exists(Dir) Then
                Throw New ApplicationException(String.Format("Can't find directory '{0}'!", Dir))
            End If
            WriteFile((Dir + "test.txt"), "FileSystemNotificationQueue test file.")
            File.Delete((Dir + "test.txt"))
        End Sub

        Public Sub Send(ByVal M As NotificationQueueMessage) Implements INotificationQueue.Send
            Dim NFile As NotificationFile = New NotificationFile(M.Id, M.Type, M.OrderId)
            WriteFile((_InboxDir + NFile.Name), M.Xml)
        End Sub

        Public Function Receive() As NotificationQueueMessage Implements INotificationQueue.Receive
            If (_InProcessDir = Nothing) Then
                Throw New ApplicationException(("Must use the 4 parameter contstructor to create a queue object " + "that supports the Receive() method."))
            End If
            Dim RetVal As NotificationQueueMessage = Nothing
            Dim SleepSeconds As Integer = 0

            While (RetVal Is Nothing)
                Dim NFile As NotificationFile = GetNextNotificationFile()
                If (Not (NFile) Is Nothing) Then
                    RetVal = New NotificationQueueMessage(NFile.Id, NFile.Type, NFile.OrderId, ReadFile((_InboxDir + NFile.Name)))
                    File.Move((_InboxDir + NFile.Name), (_InProcessDir + NFile.Name))
                Else
                    SleepSeconds = (SleepSeconds * 2)
                    If (SleepSeconds = 0) Then
                        SleepSeconds = 1
                    End If
                    If (SleepSeconds > 30) Then
                        SleepSeconds = 30
                    End If
                    Thread.Sleep((SleepSeconds * 1000))
                End If

            End While
            Return RetVal
        End Function

        Private Function GetNextNotificationFile() As NotificationFile
            Dim RetVal As NotificationFile = Nothing
            If _NFileCache Is Nothing OrElse _NFileCache.Count = 0 Then
                _NFileCache = ReadAllNotificationFilesFromDisk()
            End If
            If (_NFileCache.Count > 0) Then
                RetVal = _NFileCache.GetFileAt(0)
                _NFileCache.RemoveFileAt(0)
            End If
            Return RetVal
        End Function

        Private Function ReadAllNotificationFilesFromDisk() As NotificationFileCollection
            Dim RetVal As NotificationFileCollection = New NotificationFileCollection
            Dim Files() As String = Directory.GetFiles(_InboxDir, "*--*--*.xml")
            For Each FileName As String In Files
                Dim Info As FileInfo = New FileInfo(FileName)
                RetVal.Add(New NotificationFile(Info.Name, Info.CreationTime))
            Next
            RetVal.RemoveNewFiles(30)
            RetVal.SortByAge()
            Return RetVal
        End Function

        Public Sub ProcessingSucceeded(ByVal M As NotificationQueueMessage) Implements INotificationQueue.ProcessingSucceeded
            Dim NFile As NotificationFile = New NotificationFile(M.Id, M.Type, M.OrderId)
            File.Move((_InProcessDir + NFile.Name), (_SuccessDir + NFile.Name))
        End Sub

        Public Sub ProcessingFailed(ByVal M As NotificationQueueMessage) Implements INotificationQueue.ProcessingFailed
            Dim NFile As NotificationFile = New NotificationFile(M.Id, M.Type, M.OrderId)
            File.Move((_InProcessDir + NFile.Name), (_FailureDir + NFile.Name))
        End Sub

        Public Function GetLength() As Integer Implements INotificationQueue.GetLength
            Return ReadAllNotificationFilesFromDisk.Count
        End Function

        Private Shared Function ReadFile(ByVal FullFileName As String) As String
            Dim SReader As StreamReader = New StreamReader(FullFileName, Encoding.UTF8)
            Dim RetVal As String = SReader.ReadToEnd
            SReader.Close()
            Return RetVal
        End Function

        Private Shared Sub WriteFile(ByVal FullFileName As String, ByVal Contents As String)
            Dim OutputFile As StreamWriter = New StreamWriter(FullFileName, False, Encoding.UTF8)
            OutputFile.Write(Contents)
            OutputFile.Close()
        End Sub
    End Class
End Namespace


