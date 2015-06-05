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
using System.IO;
using System.Xml;
using System.Reflection;
using GCheckout.Checkout;
using GCheckout.AutoGen;
using GCheckout.AutoGen.Extended;
using NUnit.Framework;
using System.Collections.Generic;

namespace GCheckout.Checkout.Tests {

  /// <summary>
  /// Summary description for NotificationTests.
  /// </summary>
  [TestFixture]
  public class NotificationTests {

    private static NewOrderNotification extended;

    /// <summary>
    /// Initialize the Xml Documents
    /// </summary>
    static NotificationTests() {
      using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("GCheckout.Checkout.Tests.Xml.new-order-notification-base.xml")) {
        extended = GCheckout.Util.EncodeHelper.Deserialize(s) as NewOrderNotification;
      }
    }

    [Test]
    public void Notification_ShoppingItems_ShippingMethod(){
      Assert.AreEqual("SuperShip", extended.ShippingMethod);
    }
  
    [Test]
    public void Notification_ShoppingItems_ShippingCost(){
      Assert.AreEqual(Convert.ToDecimal(13.00), extended.ShippingCost);
    }

    [Test]
    public void Notification_ShoppingItems_Merchant_Private_Item_Data(){
//        <merchant-private-item-data>
//          <my-data>
//            <weight>1.5</weight>
//            <color>white</color>
//            <item-note>Popular item: Check inventory and order more if needed</item-note>
//          </my-data>
//        </merchant-private-item-data>
      ShoppingCartItem item;
      item = extended.Items[1];

      Assert.IsNotNull(item.MerchantPrivateItemDataNodes);
      XmlNode[] nodes = item.MerchantPrivateItemDataNodes;

      Assert.AreEqual(1, nodes.Length);
      XmlNode node = nodes[0];
      Assert.AreEqual(3, node.ChildNodes.Count);

      int nodeCounter = 0;
      string[] nodeNames = new string[] {"weight", "color", "item-note"};
      string[] nodeValues = new string[] {"1.5", "white", "Popular item: Check inventory and order more if needed"};

      foreach(XmlNode child in node) {
        Assert.AreEqual(nodeNames[nodeCounter], child.Name);
        Assert.AreEqual(nodeValues[nodeCounter], child.InnerText);
        nodeCounter++;
      }
    }

    [Test]
    public void Notification_MerchantCodes_Verify(){
      VerifyMerchantCodes(extended.MerchantCodes);
    }

    [Test]
    public void Notification_MerchantCodes_Convert_On_Notification(){
      VerifyMerchantCodes(MerchantCode.GetMerchantCodes(extended));
    }
    
    [Test]
    public void Notification_MerchantCodes_Convert_On_OrderAdjustment(){
      VerifyMerchantCodes(MerchantCode.GetMerchantCodes(extended.orderadjustment));
    }
    
    [Test]
    public void Notification_MerchantCodes_Convert_On_OrderAdjustmentMerchantcodes(){
      VerifyMerchantCodes(MerchantCode.GetMerchantCodes(extended.orderadjustment.merchantcodes));
    }

    private void VerifyMerchantCodes(List<MerchantCode> codes) {
//      <coupon-adjustment>
//        <applied-amount currency="USD">5.00</applied-amount>
//        <code>FirstVisitCoupon</code>
//        <calculated-amount currency="USD">5.00</calculated-amount>
//        <message>Congratulations! You saved $5.00 on your first visit!</message>
//      </coupon-adjustment>
//      <gift-certificate-adjustment>
//        <applied-amount currency="USD">10.00</applied-amount>
//        <code>GiftCert012345</code>
//        <calculated-amount currency="USD">10.00</calculated-amount>
//        <message>You used your Gift Certificate!</message>
//      </gift-certificate-adjustment>

      //we should get 2 codes back one of each

      Assert.AreEqual(2, codes.Count);

      MerchantCode code;

      code = codes[0];

      Assert.AreEqual(MerchantCodeType.Coupon, code.CodeType);
      Assert.AreEqual("FirstVisitCoupon", code.Code);
      Assert.AreEqual(Convert.ToDecimal(5.00), code.AppliedAmount);
      Assert.AreEqual(Convert.ToDecimal(5.00), code.CalculatedAmount);
      Assert.AreEqual("Congratulations! You saved $5.00 on your first visit!", code.Message);

      code = codes[1];

      Assert.AreEqual(MerchantCodeType.GiftCertificate, code.CodeType);
      Assert.AreEqual("GiftCert012345", code.Code);
      Assert.AreEqual(Convert.ToDecimal(10.00), code.AppliedAmount);
      Assert.AreEqual(Convert.ToDecimal(10.00), code.CalculatedAmount);
      Assert.AreEqual("You used your Gift Certificate!", code.Message);

    }

    [Test]
    public void Notification_ShoppingItems_Validate(){
      ShoppingCartItem item;

      item = extended.Items[0];
      Assert.AreEqual("A pack of highly nutritious dried food for emergency - store in your garage for up to one year!!",item.Description);
      Assert.AreEqual(string.Empty, item.MerchantItemID);
      Assert.AreEqual("Dry Food Pack AA1453", item.Name);
      Assert.AreEqual(35.00m,item.Price);
      Assert.AreEqual(1,item.Quantity);
      Assert.AreEqual("food", item.TaxTableSelector);

      item = extended.Items[1];
      Assert.AreEqual("Portable MP3 player - stores 500 songs, easy-to-use interface, color display",item.Description);
      Assert.AreEqual(string.Empty, item.MerchantItemID);
      Assert.AreEqual("MegaSound 2GB MP3 Player", item.Name);
      Assert.AreEqual(Convert.ToDecimal(178.00),item.Price);
      Assert.AreEqual(1,item.Quantity);
      Assert.AreEqual(string.Empty, item.TaxTableSelector);
    }

  }
}

#region hold
//item = extended.Items[0];
//Assert.AreEqual("",item.Description);
//Assert.AreEqual(string.Empty, item.MerchantItemID);
//Assert.AreEqual( "", item.Name);
//Assert.AreEqual(0d,item.Price);
//Assert.AreEqual(1,item.Quantity);
//Assert.AreEqual("", item.TaxTable.Name);

#endregion

