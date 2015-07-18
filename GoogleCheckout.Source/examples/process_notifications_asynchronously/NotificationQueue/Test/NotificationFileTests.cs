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
using NUnit.Framework;

namespace GCheckout.NotificationQueue.Test
{
  [TestFixture()]
	public class NotificationFileTests
	{
    [Test()]
    public void Create() {
      string Id = "85f54628-538a-44fc-8605-ae62364f6c71";
      string Type = "order-state-change-notification";
      string OrderId = "841171949013218";
      DateTime CreationTime = DateTime.Now;
      NotificationFile NFile = new NotificationFile(Id, Type, OrderId);
      Assert.AreEqual(Id, NFile.Id);
      Assert.AreEqual(Type, NFile.Type);
      Assert.AreEqual(OrderId, NFile.OrderId);
      Assert.AreEqual(string.Format("{0}--{1}--{2}.xml", Type, OrderId, Id), NFile.Name);
    }

    [Test()]
    public void CreateAndParse_OK() {
      string Name = "new-order-notification--123--456-789.xml";
      DateTime CreationTime = DateTime.Now;
      NotificationFile NFile = 
        new NotificationFile(Name, CreationTime);
      Assert.AreEqual(Name, NFile.Name);
      Assert.IsTrue(NFile.Age.TotalSeconds < 1);
      Assert.IsTrue(NFile.Age.TotalSeconds >= 0);
      Assert.AreEqual("new-order-notification", NFile.Type);
      Assert.AreEqual("123", NFile.OrderId);
      Assert.AreEqual("456-789", NFile.Id);
    }

    [Test()]
    [ExpectedException(typeof(ApplicationException))]
    public void CreateAndParse_Failure() {
      string Name = "new-order-notification-123-456-789.xml";
      DateTime CreationTime = DateTime.Now;
      NotificationFile NFile = 
        new NotificationFile(Name, CreationTime);
    }
  
  }
}
