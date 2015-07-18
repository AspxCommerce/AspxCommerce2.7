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
using NUnit.Framework;

namespace GCheckout.NotificationQueue.Test {
  [TestFixture()]
	public class NotificationQueueMessageTests {
    [Test()]
		public void Create() {
      string Id = "85f54628-538a-44fc-8605-ae62364f6c71";
      string Type = "order-state-change-notification";
      string OrderId = "841171949013218";
      string Xml = "<a>blah</a>\n<b>blah</b>";
      NotificationQueueMessage M = new NotificationQueueMessage(Id, Type, OrderId, Xml);
      Assert.AreEqual(Id, M.Id);
      Assert.AreEqual(Type, M.Type);
      Assert.AreEqual(OrderId, M.OrderId);
      Assert.AreEqual(Xml, M.Xml);
		}
	}
}
