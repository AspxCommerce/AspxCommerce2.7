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
using System.Threading;
using NUnit.Framework;

namespace GCheckout.NotificationQueue.Test {
  [TestFixture()]
	public class FileSystemNotificationQueueTests {
    private string INBOX_DIR = "inbox";
    private string INPROCESS_DIR = "inprocess";
    private string SUCCESS_DIR = "success";
    private string FAILURE_DIR = "failure";
    private static string TYPE1 = "new-order-notification";
    private static string ORDERID1 = "259835294";
    private static string ID1 = "78sd-877f-r34f";
    private static string FILENAME1 = string.Format("{0}--{1}--{2}.xml", TYPE1, ORDERID1, ID1);
    private static string TYPE2 = "order-state-change-notification";
    private static string ORDERID2 = "245629824";
    private static string ID2 = "345g-3w4f-6yg4";
    private static string FILENAME2 = string.Format("{0}--{1}--{2}.xml", TYPE2, ORDERID2, ID2);
    private static string TYPE3 = "charge-amount-notification";
    private static string ORDERID3 = "345475345";
    private static string ID3 = "3rcf-f4wtvgw-gwg5";
    private static string FILENAME3 = string.Format("{0}--{1}--{2}.xml", TYPE3, ORDERID3, ID3);
    private Random Randomizer = new Random();
    private string XML = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<new-order-notification xmlns=""http://checkout.google.com/schema/2"" serial-number=""742cfe78-b780-4681-b486-df37550c78e4"">
  <timestamp>2006-08-14T18:58:36.226Z</timestamp>
  <google-order-number>934180849872676</google-order-number>
  <shopping-cart>
    <items>
      <item>
        <quantity>2</quantity>
        <unit-price currency=""USD"">0.75</unit-price>
        <item-name>Snickers</item-name>
        <item-description>Packed with peanuts</item-description>
      </item>
      <item>
        <quantity>1</quantity>
        <unit-price currency=""USD"">2.99</unit-price>
        <item-name>Gallon of Milk</item-name>
        <item-description>Milk goes great with candy bars!</item-description>
      </item>
    </items>
  </shopping-cart>
  <buyer-shipping-address>
    <email>someone@email.com</email>
    <address1>123 Main St</address1>
    <address2 />
    <company-name />
    <contact-name>Martin Omander</contact-name>
    <phone />
    <fax />
    <country-code>US</country-code>
    <city>Nome</city>
    <region>AK</region>
    <postal-code>99763</postal-code>
  </buyer-shipping-address>
  <buyer-billing-address>
    <email>someone@email.com</email>
    <address1>123 Main St</address1>
    <address2 />
    <company-name />
    <contact-name>Martin Omander</contact-name>
    <phone />
    <fax />
    <country-code>US</country-code>
    <city>Nome</city>
    <region>AK</region>
    <postal-code>99763</postal-code>
  </buyer-billing-address>
  <buyer-marketing-preferences>
    <email-allowed>false</email-allowed>
  </buyer-marketing-preferences>
  <order-adjustment>
    <merchant-codes />
    <shipping>
      <flat-rate-shipping-adjustment>
        <shipping-name>USPS</shipping-name>
        <shipping-cost currency=""USD"">3.08</shipping-cost>
      </flat-rate-shipping-adjustment>
    </shipping>
    <total-tax currency=""USD"">0.0</total-tax>
    <adjustment-total currency=""USD"">3.08</adjustment-total>
  </order-adjustment>
  <order-total currency=""USD"">7.57</order-total>
  <fulfillment-order-state>NEW</fulfillment-order-state>
  <financial-order-state>REVIEWING</financial-order-state>
  <buyer-id>352162941084959</buyer-id>
</new-order-notification>";

    [SetUp]
    [TearDown]
    public void DeleteTempDirs() {
      DeleteDirAndFiles(INBOX_DIR);
      DeleteDirAndFiles(INPROCESS_DIR);
      DeleteDirAndFiles(SUCCESS_DIR);
      DeleteDirAndFiles(FAILURE_DIR);
    }

    [Test()]
    [ExpectedException(typeof(ApplicationException))]
    public void Create_Failure_1 () {
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
    }

    [Test()]
    [ExpectedException(typeof(ApplicationException))]
    public void Create_Failure_2 () {
      Directory.CreateDirectory(INBOX_DIR);
      Directory.CreateDirectory(INPROCESS_DIR);
      Directory.CreateDirectory(SUCCESS_DIR);
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
    }

    [Test()]
    public void Create_OK () {
      CreateTempDirs();
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
      Assert.AreEqual(0, Q.GetLength());
    }

    [Test()]
    [ExpectedException(typeof(ApplicationException))]
    public void Receive_Failure () {
      FileSystemNotificationQueue Q = 
        new FileSystemNotificationQueue(INBOX_DIR);
      Q.Receive();
    }

    [Test()]
    public void GetLength() {
      CreateTempDirs();
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
      WriteFile(INBOX_DIR, FILENAME1, XML, 60);
      WriteFile(INBOX_DIR, FILENAME2, XML, 60);
      WriteFile(INBOX_DIR, FILENAME3, XML, 10);
      Assert.AreEqual(2, Q.GetLength());
    }

    [Test()]
    public void Send() {
      CreateTempDirs();
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
      Q.Send(new NotificationQueueMessage(ID1, TYPE1, ORDERID1, XML));
      Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length);
      Assert.AreEqual(GetFullFileName(INBOX_DIR, FILENAME1), Directory.GetFiles(INBOX_DIR)[0]);
      Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(SUCCESS_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length);
      Q.Send(new NotificationQueueMessage(ID2, TYPE2, ORDERID2, XML));
      Q.Send(new NotificationQueueMessage(ID3, TYPE3, ORDERID3, XML));
      Assert.AreEqual(3, Directory.GetFiles(INBOX_DIR).Length);
    }

    [Test()]
    public void ProcessFiles() {
      CreateTempDirs();
      WriteFile(INBOX_DIR, FILENAME1, XML, 40);
      WriteFile(INBOX_DIR, FILENAME2, XML, 50);
      WriteFile(INBOX_DIR, FILENAME3, XML, 60);
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
      Assert.AreEqual(3, Q.GetLength());
      NotificationQueueMessage M = Q.Receive();
      Assert.AreEqual("charge-amount-notification", M.Type);
      Assert.AreEqual("345475345", M.OrderId);
      Assert.AreEqual("3rcf-f4wtvgw-gwg5", M.Id);
      Assert.AreEqual(2, Q.GetLength());
      Assert.AreEqual(2, Directory.GetFiles(INBOX_DIR).Length);
      Assert.AreEqual(1, Directory.GetFiles(INPROCESS_DIR).Length);
      Assert.AreEqual(GetFullFileName(INPROCESS_DIR, FILENAME3), Directory.GetFiles(INPROCESS_DIR)[0]);
      Assert.AreEqual(0, Directory.GetFiles(SUCCESS_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length);
      Q.ProcessingSucceeded(M);
      Assert.AreEqual(2, Directory.GetFiles(INBOX_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length);
      Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length);
      Assert.AreEqual(GetFullFileName(SUCCESS_DIR, FILENAME3), Directory.GetFiles(SUCCESS_DIR)[0]);
      Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length);
      M = Q.Receive();
      Assert.AreEqual("order-state-change-notification", M.Type);
      Assert.AreEqual("245629824", M.OrderId);
      Assert.AreEqual("345g-3w4f-6yg4", M.Id);
      Assert.AreEqual(1, Q.GetLength());
      Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length);
      Assert.AreEqual(1, Directory.GetFiles(INPROCESS_DIR).Length);
      Assert.AreEqual(GetFullFileName(INPROCESS_DIR, FILENAME2), Directory.GetFiles(INPROCESS_DIR)[0]);
      Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length);
      Q.ProcessingFailed(M);
      Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length);
      Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length);
      Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length);
      Assert.AreEqual(1, Directory.GetFiles(FAILURE_DIR).Length);
      Assert.AreEqual(GetFullFileName(FAILURE_DIR, FILENAME2), Directory.GetFiles(FAILURE_DIR)[0]);
    }

    [Test, Explicit]
    public void PerformanceTest() {
      int BACKLOG = 1000;
      // Seed the queue with a backlog.
      CreateTempDirs();
      FileSystemNotificationQueue Q = new FileSystemNotificationQueue
        (INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR);
      for (int i = 0; i < BACKLOG; i++) {
        Q.Send(new NotificationQueueMessage(GetRandomId(), GetRandomType(), GetRandomOrderId(), XML));
      }
      // Sleep 30 seconds to make sure the notification files are old enough.
      Thread.Sleep(30000);
      // Run the actual test.
      DateTime StartTime = DateTime.Now;
      for (int i = 0; i < BACKLOG; i++) {
        Q.Send(new NotificationQueueMessage(GetRandomId(), GetRandomType(), GetRandomOrderId(), XML));
        NotificationQueueMessage M = Q.Receive();
        Q.ProcessingSucceeded(M);
      }
      TimeSpan Duration = DateTime.Now.Subtract(StartTime);
      Console.WriteLine("Took {0} seconds to catch up with a backlog of {1} messages, while new ones were being added.", Duration.TotalSeconds, BACKLOG);
      Console.WriteLine("Throughput: {0} messages per second.", BACKLOG / Duration.TotalSeconds);
    }

    private string GetRandomType() {
      string RetVal = "";
      int Type = Randomizer.Next(5);
      if (Type == 0) RetVal = "new-order-notification";
      if (Type == 1) RetVal = "order-state-change-notification";
      if (Type == 2) RetVal = "risk-information-notification";
      if (Type == 3) RetVal = "refund-amount-notification";
      if (Type == 4) RetVal = "charge-amount-notification";
      if (Type == 5) RetVal = "chargeback-amount-notification";
      return RetVal;
    }

    private string GetRandomOrderId() {
      StringBuilder SB = new StringBuilder();
      for (int i = 0; i < 14; i++) {
        SB.Append(Randomizer.Next(9));
      }
      return SB.ToString();
    }

    private string GetRandomId() {
      StringBuilder SB = new StringBuilder();
      for (int i = 0; i < 4; i++) {
        for (int j = 0; j < 8; j++) {
          SB.Append(Randomizer.Next(9));
        }
        if (i < 3) SB.Append("-");
      }
      return SB.ToString();
    }

    private void CreateTempDirs() {
      Directory.CreateDirectory(INBOX_DIR);
      Directory.CreateDirectory(INPROCESS_DIR);
      Directory.CreateDirectory(SUCCESS_DIR);
      Directory.CreateDirectory(FAILURE_DIR);
    }

    private static void WriteFile(string Dir, string FileName, string Contents, int BackDateSeconds) {
      string FullFileName = GetFullFileName(Dir, FileName);
      StreamWriter OutputFile = 
        new StreamWriter(FullFileName, false, Encoding.UTF8);
      OutputFile.Write(Contents);
      OutputFile.Close();
      if (BackDateSeconds > 0) {
        FileInfo FI = new FileInfo(FullFileName);
        FI.CreationTime = DateTime.Now.Subtract(new TimeSpan(0, 0, BackDateSeconds));
      }
    }

    private static void DeleteDirAndFiles(string Dir) {
      if (Directory.Exists(Dir)) {
        foreach (string FileName in Directory.GetFiles(Dir)) {
          File.Delete(FileName);
        }
        Directory.Delete(Dir);
      }
    }

    private static string GetFullFileName(string Dir, string FileName) {
      string RetVal = Dir;
      if (!RetVal.EndsWith("\\")) RetVal += "\\";
      RetVal += FileName;
      return RetVal;
    }


	}
}
