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

namespace GCheckout.NotificationQueue {
	public class NotificationQueueMessage {
    private string _Id;
    private string _Type;
    private string _OrderId;
    private string _Xml;

		public NotificationQueueMessage(string Id, string Type, string OrderId, 
      string Xml) {
      _Id = Id;
      _Type = Type;
      _OrderId = OrderId;
      _Xml = Xml;
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

    public string Xml {
      get {
        return _Xml;
      }
    }
  
  }
}
