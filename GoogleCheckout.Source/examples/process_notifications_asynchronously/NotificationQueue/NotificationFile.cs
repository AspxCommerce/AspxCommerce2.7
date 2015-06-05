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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace GCheckout.NotificationQueue
{
	public class NotificationFile
	{
    private string _Name = null;
    private DateTime _CreationTime;
    private string _Id;
    private string _Type;
    private string _OrderId;

    public NotificationFile(string Name, DateTime CreationTime) {
      _Name = Name;
      _CreationTime = CreationTime;
      Regex Re = new Regex("(.+)--(.+)--(.+).xml");
      Match M = Re.Match(Name);
      if (M.Success) {
        _Type = M.Groups[1].ToString();
        _OrderId = M.Groups[2].ToString();
        _Id = M.Groups[3].ToString();
      }
      else {
        throw new ApplicationException(string.Format(
          "Notification file name '{0}' is not on the recognized " +
          "'x--y--z.xml' format.", Name));
      }
		}

    public NotificationFile(string Id, string Type, string OrderId) {
      _Id = Id;
      _Type = Type;
      _OrderId = OrderId;
      _Name = string.Format("{0}--{1}--{2}.xml", Type, OrderId, Id);
    }

    public TimeSpan Age {
      get {
        return DateTime.Now.Subtract(_CreationTime);
      }
    }

    public string Name {
      get {
        return _Name;
      }
    }

    public string Id {
      get {
        return _Id;
      }
    }

    public string Type {
      get {
        return _Type;
      }
    }

    public string OrderId {
      get {
        return _OrderId;
      }
    }

  }
}
