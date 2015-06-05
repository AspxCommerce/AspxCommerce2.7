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
  public class ResetReturnBackOrderAndCancelItemTests {
    string originalOrderID = "841171949013218";

    /// <exclude/>
    [Test]
    public void VerifyResetReturnBackOrderAndCancelItem() {

      string xml;
      ResetItemsShippingInformationRequest req1 =
        new ResetItemsShippingInformationRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID);
      req1.AddMerchantItemId("A1");
      req1.AddMerchantItemId("B1");
      req1.AddMerchantItemId("123456");

      xml = Util.EncodeHelper.Utf8BytesToString(req1.GetXml());

      AutoGen.ResetItemsShippingInformationRequest D
        = EncodeHelper.Deserialize(xml) as AutoGen.ResetItemsShippingInformationRequest;

      Assert.AreEqual(req1.GoogleOrderNumber, D.googleordernumber);

      req1 =
        new ResetItemsShippingInformationRequest(originalOrderID);
      req1.AddMerchantItemId("A1");
      req1.AddMerchantItemId("B1");
      req1.AddMerchantItemId("123456");
      req1.SendEmail = true;
      xml = Util.EncodeHelper.Utf8BytesToString(req1.GetXml());

      VerifyMessage(xml, typeof(AutoGen.ResetItemsShippingInformationRequest));


      BackorderItemsRequest req2 =
        new BackorderItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID);
      req2.AddMerchantItemId("A1");
      req2.AddMerchantItemId("B1");
      req2.AddMerchantItemId("123456");

      xml = Util.EncodeHelper.Utf8BytesToString(req2.GetXml());
      VerifyMessage(xml, typeof(AutoGen.BackorderItemsRequest));

      req2 =
        new BackorderItemsRequest(originalOrderID);
      req2.AddMerchantItemId("A1");
      req2.AddMerchantItemId("B1");
      req2.AddMerchantItemId("123456");
      req2.SendEmail = true;
      xml = Util.EncodeHelper.Utf8BytesToString(req2.GetXml());

      CancelItemsRequest req3 =
        new CancelItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID);
      req3.AddMerchantItemId("A1");
      req3.AddMerchantItemId("B1");
      req3.AddMerchantItemId("123456");

      xml = Util.EncodeHelper.Utf8BytesToString(req3.GetXml());
      VerifyMessage(xml, typeof(AutoGen.CancelItemsRequest));

      req3 =
        new CancelItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID, true);
      req3.AddMerchantItemId("A1");
      req3.AddMerchantItemId("B1");
      req3.AddMerchantItemId("123456");

      xml = Util.EncodeHelper.Utf8BytesToString(req3.GetXml());

      req3 =
        new CancelItemsRequest(originalOrderID);
      req3.AddMerchantItemId("A1");
      req3.AddMerchantItemId("B1");
      req3.AddMerchantItemId("123456");
      xml = Util.EncodeHelper.Utf8BytesToString(req3.GetXml());

      req3 =
        new CancelItemsRequest(originalOrderID, false);
      req3.AddMerchantItemId("A1");
      req3.AddMerchantItemId("B1");
      req3.AddMerchantItemId("123456");
      xml = Util.EncodeHelper.Utf8BytesToString(req3.GetXml());

      req3 =
        new CancelItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID, "Comment", "Reason");
      req3.AddMerchantItemId("A1");
      req3.AddMerchantItemId("B1");
      req3.AddMerchantItemId("123456");
      xml = Util.EncodeHelper.Utf8BytesToString(req3.GetXml());

      ReturnItemsRequest req4 =
        new ReturnItemsRequest(
        "1234", "5678", EnvironmentType.Sandbox.ToString(),
        originalOrderID);
      req4.AddMerchantItemId("A1");
      req4.AddMerchantItemId("B1");
      req4.AddMerchantItemId("123456");

      xml = Util.EncodeHelper.Utf8BytesToString(req4.GetXml());
      VerifyMessage(xml, typeof(AutoGen.ReturnItemsRequest));

      req4 =
        new ReturnItemsRequest(originalOrderID);
      req4.AddMerchantItemId("A1");
      req4.AddMerchantItemId("B1");
      req4.AddMerchantItemId("123456");
      req4.SendEmail = true;
      xml = Util.EncodeHelper.Utf8BytesToString(req4.GetXml());

    }

    private void VerifyMessage(string xml, Type theType) {

      //verify the type first
      object obj = Util.EncodeHelper.Deserialize(xml);

      Assert.AreEqual(obj.GetType(), theType);

      StringBuilder sb;
      //XmlNode item;
      XmlNodeList list;
      System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
      XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
      ns.AddNamespace("d", "http://checkout.google.com/schema/2");
      ns.AddNamespace(string.Empty, "http://checkout.google.com/schema/2");

      doc.LoadXml(xml);

      string googleOrderID = doc.DocumentElement.SelectSingleNode(
        "@google-order-number").InnerText;
      Assert.AreEqual(originalOrderID, googleOrderID);

      //ensure we have 3 items

      sb = new StringBuilder();
      sb.Append("/d:");
      sb.Append(Util.EncodeHelper.GetTopElement(xml));
      sb.Append("/d:item-ids/d:item-id");

      list = doc.DocumentElement.SelectNodes(sb.ToString(), ns);
      Assert.AreEqual(list.Count, 3);

      //we should have 2 merchant items
      sb = new StringBuilder();
      sb.Append("/d:");
      sb.Append(Util.EncodeHelper.GetTopElement(xml));
      sb.Append("/d:item-ids/d:item-id/d:merchant-item-id");

      list = doc.DocumentElement.SelectNodes(sb.ToString(), ns);
      Assert.AreEqual(list.Count, 3);
    }
  }
}
