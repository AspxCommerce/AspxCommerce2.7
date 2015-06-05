/*************************************************
 * Copyright (C) 2006-2012 Google Inc.
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
using System.Text;
using System.Xml;
using NUnit.Framework;
using GCheckout.Util;
using GCheckout.OrderProcessing;

namespace GCheckout.Checkout.Tests {

  /// <exclude/>
  [TestFixture]
  public class ShipItemsRequestTests {
    string originalOrderID = "841171949013218";

    /// <exclude/>
    [Test]
    public void TwoItemsShippedInTwoBoxes() {

      ShipItemBox box;
      string xml;

      //Console.WriteLine("Using AddMerchantItemId");
      ShipItemsRequest req = new ShipItemsRequest(
      "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      req.AddMerchantItemId("UPS", "55555555", "A1");
      req.AddMerchantItemId("UPS", "77777777", "B2");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());
      VerifyTwoItemsShippingInTwoBoxes(xml, false);

      req = new ShipItemsRequest(originalOrderID);
      req.SendEmail = true;
      req.AddMerchantItemId("UPS", "55555555", "A1");
      req.AddMerchantItemId("UPS", "77777777", "B2");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());
      VerifyTwoItemsShippingInTwoBoxes(xml, false);

      //Console.WriteLine("Using Boxes");

      //now we want to try the same thing with adding boxes and
      //putting items into the boxes.
      req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      box = req.AddBox("UPS", "55555555");
      box.AddMerchantItemID("A1");
      box = req.AddBox("UPS", "77777777");
      box.AddMerchantItemID("B2");

      Assert.AreEqual(box.Carrier, "UPS");
      Assert.AreEqual(box.TrackingNumber, "77777777");

      //you can't add the same box twice
      try {
        box = req.AddBox("UPS", "55555555");
        Assert.Fail("The same box can't be added twice.");
      }
      catch {
      }

      //you can't add an empty merchantitemid
      try {
        box.AddMerchantItemID(string.Empty);
        Assert.Fail("you can't add an empty merchantitemid.");
      }
      catch {
      }

      //you can't add a duplicate merchantitemid
      try {
        box.AddMerchantItemID("B2");
        Assert.Fail("you can't add a duplicate merchantitemid.");
      }
      catch {
      }


      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());
      VerifyTwoItemsShippingInTwoBoxes(xml, false);

      //Console.WriteLine("Google Items using AddGoogleItemId");

      req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      req.AddMerchantItemId("UPS", "55555555", "123456");
      req.AddMerchantItemId("UPS", "77777777", "7891234");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());
      VerifyTwoItemsShippingInTwoBoxes(xml, true);

      //Console.WriteLine("Google Items using Boxes");

      //now we want to try the same thing with adding boxes and
      //putting items into the boxes.
      req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      box = req.AddBox("UPS", "55555555");
      box.AddMerchantItemID("123456");
      box = req.AddBox("UPS", "77777777");
      box.AddMerchantItemID("7891234");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());
      VerifyTwoItemsShippingInTwoBoxes(xml, true);

    }

    private void VerifyTwoItemsShippingInTwoBoxes(string xml, bool googleItems) {
      XmlNode item;
      XmlNode childItem;
      XmlNodeList list;
      StringBuilder sb;

      System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
      XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
      ns.AddNamespace("d", "http://checkout.google.com/schema/2");
      ns.AddNamespace(string.Empty, "http://checkout.google.com/schema/2");

      doc.LoadXml(xml);

      string googleOrderID = doc.DocumentElement.SelectSingleNode(
        "@google-order-number").InnerText;
      Assert.AreEqual(originalOrderID, googleOrderID);

      sb = new StringBuilder();
      sb.Append("/d:ship-items/d:item-shipping-information-list");
      sb.Append("/d:item-shipping-information");
      if (googleItems)
        sb.Append("[d:item-id/d:merchant-item-id = '123456']");
      else
        sb.Append("[d:item-id/d:merchant-item-id = 'A1']");


      item = doc.SelectSingleNode(sb.ToString(), ns);
      Assert.IsNotNull(item);

      //Try to obtain only one tracking node
      sb = new StringBuilder("d:tracking-data-list/d:tracking-data");
      list = item.SelectNodes(sb.ToString(), ns);
      Assert.AreEqual(list.Count, 1);

      //now try for the 55555555 tracking number
      sb.Append("[d:tracking-number = '55555555']");
      childItem = item.SelectSingleNode(sb.ToString(), ns);
      Assert.IsNotNull(childItem);

      sb = new StringBuilder();
      sb.Append("/d:ship-items/d:item-shipping-information-list");
      sb.Append("/d:item-shipping-information");
      if (googleItems)
        sb.Append("[d:item-id/d:merchant-item-id = '7891234']");
      else
        sb.Append("[d:item-id/d:merchant-item-id = 'B2']");

      item = doc.SelectSingleNode(sb.ToString(), ns);
      Assert.IsNotNull(item);

      //Try to obtain only one tracking node
      sb = new StringBuilder("d:tracking-data-list/d:tracking-data");
      list = item.SelectNodes(sb.ToString(), ns);
      Assert.AreEqual(list.Count, 1);

      //now try for the 55555555 tracking number
      sb.Append("[d:tracking-number = '77777777']");
      childItem = item.SelectSingleNode(sb.ToString(), ns);
      Assert.IsNotNull(childItem);
    }

    /// <exclude/>
    [Test]
    public void TwoItemsShipInTheSameBox() {
      ShipItemBox box;
      string xml;

      //Console.WriteLine("Using AddMerchantItemId");
      ShipItemsRequest req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      req.AddMerchantItemId("UPS", "55555555", "A1");
      req.AddMerchantItemId("UPS", "55555555", "B2");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);
      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());

      //Console.WriteLine("Using Boxes");

      //now we want to try the same thing with adding boxes and
      //putting items into the boxes.
      req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      box = req.AddBox("UPS", "55555555");
      box.AddMerchantItemID("A1");
      box.AddMerchantItemID("B2");

      Assert.AreEqual(req.ItemShippingInfo.Length, 2);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());

    }

    /// <exclude/>
    [Test]
    public void OneItemShipsInTwoBoxes() {
      ShipItemBox box;
      string xml;

      //Console.WriteLine("Using AddMerchantItemId");
      ShipItemsRequest req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      req.AddMerchantItemId("UPS", "55555555", "A1");
      req.AddMerchantItemId("UPS", "77777777", "A1");

      Assert.AreEqual(req.ItemShippingInfo.Length, 1);
      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());

      //Console.WriteLine("Using Boxes");

      //now we want to try the same thing with adding boxes and
      //putting items into the boxes.
      req = new ShipItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(), originalOrderID);
      req.SendEmail = true;
      box = req.AddBox("UPS", "55555555");
      box.AddMerchantItemID("A1");
      box = req.AddBox("UPS", "77777777");
      box.AddMerchantItemID("A1");

      Assert.AreEqual(req.ItemShippingInfo.Length, 1);

      xml = Util.EncodeHelper.Utf8BytesToString(req.GetXml());

    }

  }
}
