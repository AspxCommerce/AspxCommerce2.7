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
/*
 Edit History:
 *  August 2008   Joe Feser joe.feser@joefeser.com  
 *  Added test for digital item to ensure 1024 characters is the limit.
*/

using System;
using System.Text;
using NUnit.Framework;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using GCheckout.Util;
using GCheckout.Checkout;
using GCheckout.AutoGen;

namespace GCheckout.Checkout.Tests {

  /// <exclude/>
  [TestFixture]
  public class CheckoutTests {

    public const string MERCHANT_KEY = "567123098";
    public const string MERCHANT_ID = "987654321";
    public const string ORDER_NUMBER = "1234567890";
    public const string MESSAGE = "This is the test Message";
    public const string REASON = "This is the test Reason";
    public const string COMMENT = "This is a Test Comment";
    public const string MERCHANT_ORDER_NUMBER = "ABCDEFGHIJ";
    public const string UPS_TRACKING = "Z1234567890";
    public const string CONST_URL = "http://www.example.com/?hideme=YWJj4oKsxZLihKLCqcKuw6XDq8Oxw7bDvyEiIyQlJicoKSorLC0uLz8/Pz/Lhj8/4oKsUD8=";

    /// <exclude/>
    [Test]
    public void TestURLParamters() {

      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);
      ParameterizedUrl url = request.AddParameterizedUrl("http://localhost/default.aspx?url1=test$&url2=false&url3=@@Hello^World");

      url = request.AddParameterizedUrl("http://crazyurl.com:8888/crazy dir/default.aspx?url1=test$&url2=false&url3=@@Hello^World", true);

      //Create a second Param
      url = request.AddParameterizedUrl("http://localhost/order.aspx", true);

      ParameterizedUrl theUrl = new ParameterizedUrl("http://localhost/purl.aspx");
      request.AddParameterizedUrl(theUrl);

      url.AddParameter("orderid", UrlParameterType.OrderID);
      url.AddParameter("ordertotal", UrlParameterType.OrderTotal);

      try {
        url.AddParameter(string.Empty, UrlParameterType.BillingCity);
        Assert.Fail("Empty parameters names are not allowed.");
      }
      catch {
      }

      try {
        url.AddParameter("Test", UrlParameterType.Unknown);
        Assert.Fail("Unknown Parameter type is not allowed.");
      }
      catch {
      }

      //needed for 100% coverage
      ParameterizedUrls testUrls = new ParameterizedUrls();
      testUrls.AddUrl("http://localhost/test.aspx");
      testUrls.AddUrl(new ParameterizedUrl("http://localhost/new.aspx"));

      //Now get the XML
      byte[] cart = request.GetXml();

      XmlDocument doc = new XmlDocument();
      XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
      ns.AddNamespace("d", "http://checkout.google.com/schema/2");
      ns.AddNamespace(string.Empty, "http://checkout.google.com/schema/2");

      using (MemoryStream ms = new MemoryStream(cart)) {
        doc.Load(ms);
      }

      XmlNodeList nodeList = doc.SelectNodes("/d:checkout-shopping-cart/d:checkout-flow-support/d:merchant-checkout-flow-support/d:parameterized-urls/d:parameterized-url/d:parameters/d:url-parameter", ns);

      Assert.AreEqual(2, nodeList.Count);

      int index = 0;
      foreach (XmlElement node in nodeList) {
        string name = node.GetAttribute("name");
        string type = node.GetAttribute("type");
        if (index == 0) {
          Assert.AreEqual("orderid", name);
          Assert.AreEqual("order-id", type);
        }
        else {
          Assert.AreEqual("ordertotal", name);
          Assert.AreEqual("order-total", type);
        }
        index++;
      }

    }

    /// <exclude/>
    [Test]
    public void TestMerchantCalcTaxes() {
      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);
      request.MerchantCalculatedTax = true;

      Assert.AreEqual(true, request.MerchantCalculatedTax);

      byte[] xml = null;
      try {
        xml = request.GetXml();
        Assert.Fail("You should not be able to obtain the xml:MerchantCalculatedTax");
      }
      catch (Exception ex) {
        if (ex.Message.IndexOf("MerchantCalculatedTax=true, you must add at least one tax rule.") == -1) {
          Assert.Fail(ex.Message);
        }
      }

      //now we want to set a tax table and let it blow up because the callback url was not set.
      request.AddStateTaxRule("OH", .06, true);

      try {
        xml = request.GetXml();
        Assert.Fail("You should not be able to obtain the xml:AddStateTaxRule");
      }
      catch (Exception ex) {
        if (ex.Message.IndexOf("MerchantCalculatedTax=true, you must set MerchantCalculationsUrl.") == -1) {
          Assert.Fail(ex.Message);
        }
      }

      request.MerchantCalculatedTax = false;
      request.AcceptMerchantCoupons = true;

      try {
        xml = request.GetXml();
        Assert.Fail("You should not be able to obtain the xml:AcceptMerchantCoupons");
      }
      catch (Exception ex) {
        if (ex.Message.IndexOf("AcceptMerchantCoupons=true, you must set MerchantCalculationsUrl.") == -1) {
          Assert.Fail(ex.Message);
        }
      }

      request.AcceptMerchantCoupons = false;
      request.AcceptMerchantGiftCertificates = true;

      try {
        xml = request.GetXml();
        Assert.Fail("You should not be able to obtain the xml:AcceptMerchantGiftCertificates");
      }
      catch (Exception ex) {
        if (ex.Message.IndexOf("AcceptMerchantGiftCertificates=true, you must set") == -1) {
          Assert.Fail(ex.Message);
        }
      }

      request.AcceptMerchantGiftCertificates = false;

      //set to false to test carrier option
      request.MerchantCalculationsUrl = "http://localhost/test.aspx";

      //Ship from test
      request.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Second_Day, 4.99m);

      try {
        xml = request.GetXml();
        Assert.Fail("You should not be able to obtain the xml:carrier-calculated-shipping item exists");
      }
      catch (Exception ex) {
        if (ex.Message.IndexOf("a ShipFrom address must also be set") == -1) {
          Assert.Fail(ex.Message);
        }
      }
    }

    /// <exclude/>
    [Test]
    public void TestExamples() {
      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      //Make sure we can add an item to the cart.
      request.AddItem("Item 1", "Cool Candy 1", 2.00M, 1);

      request.AddStateTaxRule("CT", .06, true);

      byte[] cart = request.GetXml();

      //Console.WriteLine(EncodeHelper.Utf8BytesToString(cart));

      //test to see if the item can desialize
      Assert.IsNotNull(GCheckout.Util.EncodeHelper.Deserialize(cart));


      //example 2

      request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      //Make sure we can add an item to the cart.
      request.AddItem("Item 1", "Cool Candy 1", 2.00M, 1);

      request.AddStateTaxRule("CT", .06, true);
      request.AddStateTaxRule("MD", .05, false);

      cart = request.GetXml();

      //Console.WriteLine(EncodeHelper.Utf8BytesToString(cart));

      //test to see if the item can desialize
      Assert.IsNotNull(GCheckout.Util.EncodeHelper.Deserialize(cart));

      //example 2a

      request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      //Make sure we can add an item to the cart.
      request.AddItem("Item 1", "Cool Candy 1", 2.00M, 1);

      cart = request.GetXml();

      //Console.WriteLine(EncodeHelper.Utf8BytesToString(cart));

      //test to see if the item can desialize
      Assert.IsNotNull(GCheckout.Util.EncodeHelper.Deserialize(cart));


      //example 3

      request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      //Make sure we can add an item to the cart.
      request.AddItem("Item 1", "Cool Candy 1", 2.00M, 1);

      request.AddZipTaxRule("100*", 0.08375, false);
      request.AddStateTaxRule("NY", 0.0400, true);

      //this should be an invalid format
      try {
        request.AddZipTaxRule("255333", .05, true);
        Assert.Fail("255333 should not be a correct zip code format");
      }
      catch {
      }

      cart = request.GetXml();

      //Console.WriteLine(EncodeHelper.Utf8BytesToString(cart));

      //test to see if the item can desialize
      Assert.IsNotNull(GCheckout.Util.EncodeHelper.Deserialize(cart));

      request.AddMerchantCalculatedShippingMethod("Test 1", 12.11m);
      request.AddMerchantCalculatedShippingMethod("Test 2", 4.95m, new ShippingRestrictions());
      request.AddMerchantCalculatedShippingMethod("Test 3", 5.95m, new ShippingRestrictions());
      request.AddMerchantCalculatedShippingMethod("MerchantCalc", 12.95m, new ShippingRestrictions(), new ShippingRestrictions());

      //create a pickup shipping method
      request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);
      request.AddPickupShippingMethod("Name", 4.95m);
      request.AddCountryTaxRule(GCheckout.AutoGen.USAreas.ALL, .05, true);
      request.AddWorldAreaTaxRule(.02, true);
      //Tax Canada at 5%
      request.AddPostalAreaTaxRule("CA", .05, true);

      //Tax all cities that start with L4L at 7%
      request.AddPostalAreaTaxRule("CA", "L4L*", .07, true);

      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<data />");
      request.AddMerchantPrivateDataNode(doc.DocumentElement);

      //we must pass in a valid node
      try {
        request.AddMerchantPrivateDataNode(null);
        Assert.Fail("Null can't be sent to AddMerchantPrivateDataNode.");
      }
      catch {
      }

    }

    /// <exclude/>
    [Test]
    public void TestAddItem() {

      //due to the complexity of the add items. we are going to create a known set of data points and add them to the collection.
      ShoppingCartItem si = new ShoppingCartItem();
      si.Description = "Description";
      si.DigitalContent = new DigitalItem("Digital Item Key", "Digital Item Description");
      si.MerchantItemID = "Merchant Item ID";
      si.MerchantPrivateItemData = "Private Data";

      XmlDocument mpdDoc = new XmlDocument();
      mpdDoc.LoadXml("<data />");
      mpdDoc.DocumentElement.AppendChild(mpdDoc.CreateElement("node1"));
      mpdDoc.DocumentElement.AppendChild(mpdDoc.CreateElement("node2"));
      XmlNode[] mpdNodes = new XmlNode[] { mpdDoc.DocumentElement.ChildNodes[0], mpdDoc.DocumentElement.ChildNodes[1] };

      si.MerchantPrivateItemDataNodes = mpdNodes;
      si.Name = "Name";
      si.Price = 0.99m;
      si.Quantity = 1;

      AlternateTaxTable taxTable = new AlternateTaxTable("Example");
      taxTable.AddStateTaxRule("OH", .06);

      si.TaxTable = taxTable;
      si.Weight = 10.5;

      si.TaxTable.AddCountryTaxRule(GCheckout.AutoGen.USAreas.ALL, 5.0);

      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      request.ContinueShoppingUrl = "http://localhost/";
      request.AnalyticsData = "Test data";
      request.PlatformID = 1234567890;
      request.EditCartUrl = "http://localhost/editcart.aspx";
      request.RequestBuyerPhoneNumber = true;
      request.MerchantCalculationsUrl = "http://localhost/calculate.aspx";
      request.AcceptMerchantCoupons = true;
      request.AcceptMerchantGiftCertificates = true;
      request.SetRoundingPolicy(RoundingMode.FLOOR, RoundingRule.TOTAL);
      request.AddShippingPackage("main", "Cleveland", "OH", "44114");

      request.MerchantPrivateData = "Test Cool Stuff";
      request.AddMerchantPrivateDataNode(mpdNodes[0]);

      XmlNode[] mpdn = request.MerchantPrivateDataNodes;

      Assert.AreSame(mpdn[0], mpdNodes[0]);

      try {
        request.AddItem(null);
        Assert.Fail("Null can't be passed to the AddItem methods");
      }
      catch {
      }

      try {
        MethodInfo mi = typeof(CheckoutShoppingCartRequest).GetMethod("AddItem", new Type[] { typeof(IShoppingCartItem) });
        mi.Invoke(request, new object[] { null });
        Assert.Fail("Null can't be passed to the AddItem methods");
      }
      catch {
      }

      request.AddItem(si);
      request.AddItem(si.Clone() as IShoppingCartItem);

      MethodInfo[] methods = typeof(CheckoutShoppingCartRequest).GetMethods();

      foreach (MethodInfo mi in methods) {
        bool cancel = false;
        //we are only working with AddItems
        if (mi.Name == "AddItem") {
          Type sct = typeof(ShoppingCartItem);
          ShoppingCartItem si2 = si.Clone() as ShoppingCartItem;
          ParameterInfo[] parameters = mi.GetParameters();
          object[] setter = new object[parameters.Length];
          for (int i = 0; i < parameters.Length; i++) {
            ParameterInfo pi = parameters[i];
            if (pi.ParameterType == typeof(ShoppingCartItem) || pi.ParameterType == typeof(IShoppingCartItem)) {
              cancel = true;
              continue;
            }
            //get the property from the object
            PropertyInfo source;
            if (pi.Name != "digitalItem") {
              source = sct.GetProperty(pi.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }
            else {
              source = sct.GetProperty("DigitalContent");
            }
            setter[i] = source.GetValue(si2, null);

            //we want to split and take the first item
            if (!pi.ParameterType.IsArray && source.PropertyType.IsArray) {
              object[] vals = setter[i] as object[];
              setter[i] = vals[0] as object;
            }
          }
          if (!cancel) {
            //now call the method
            ShoppingCartItem called = mi.Invoke(request, setter) as ShoppingCartItem;

            //this is to fix a params array issue.
            if (parameters[parameters.Length - 1].Name == "MerchantPrivateItemDataNodes") {
              called.MerchantPrivateItemDataNodes = si2.MerchantPrivateItemDataNodes;
            }
          }
        }
      }

      byte[] toXml = request.GetXml();

      //Make sure we can add an item to the cart.
      ShoppingCartItem item = request.AddItem("Item 1", "Cool Candy 1", "Merchant Item ID", 2.00M, 1);
      item.Weight = 2.2;
      item.MerchantPrivateItemData = null; //perform a null check

      //verify we can't set the price to fractions.

      item.Price = 1.975m;
      Assert.AreEqual(1.98m, item.Price);

      item.Price = 1.994m;
      Assert.AreEqual(1.99m, item.Price);

      Assert.AreEqual(2.2, item.Weight);
      Assert.AreEqual("Merchant Item ID", item.MerchantItemID);

      //this is a very specific test to make sure that if only one node exists, return it. it may be for a reason.

      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<data />");
      doc.DocumentElement.SetAttribute("test", "cool");

      string xml = doc.OuterXml;
      item.MerchantPrivateItemDataNodes = new XmlNode[] { doc.DocumentElement };
      string xmlReturn = item.MerchantPrivateItemData;
      Assert.AreEqual(xml, xmlReturn);

      //create a new node
      XmlNode secondNode = doc.DocumentElement.AppendChild(doc.CreateElement("test"));
      item.MerchantPrivateItemDataNodes = new XmlNode[] { doc.DocumentElement, secondNode };

      xmlReturn = item.MerchantPrivateItemData;
      Assert.AreEqual(null, xmlReturn);

      item.MerchantPrivateItemDataNodes = null;
      Assert.AreEqual(new XmlNode[] { }, item.MerchantPrivateItemDataNodes);

      //this should throw an exception
      try {
        item.Weight = -1;
        Assert.Fail("Weight should not be allowed to be negative.");
      }
      catch {
      }

      //create a new instance of the cart item
      ShoppingCartItem testItem = new ShoppingCartItem();
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_Digital_Item_Expect_Failure_1024_Char_Limit() {
      DigitalItem aaa = new DigitalItem(new Uri(CONST_URL), "Url Description for item 111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
    }

    [Test]
    public void Test_DigitalItem_EncodedUrls() {

      string url = CONST_URL;

      DigitalItem urlDigitalItem = new DigitalItem(new Uri(url), "Url Description for item");

      Assert.AreEqual(url, urlDigitalItem.Url);

      url = "HTTP://www.ConToso.com/thick%20and%20thin.htm";

      urlDigitalItem = new DigitalItem(new Uri(url), "Url Description for item");
      Assert.AreEqual(url.ToLower(), urlDigitalItem.Url);

      Uri uri = new Uri(url);
      Assert.AreEqual(url.ToLower(), uri.AbsoluteUri.ToLower());
      Assert.IsFalse(url.ToLower().Equals(uri.ToString().ToLower()));

    }


    [Test]
    public void TestSubscriptions_All_Data() {
      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);
      //Make sure we can add an item to the cart.
      Subscription sub = new Subscription();
      sub.AddSubscriptionPayment(new SubscriptionPayment(2, 12));
      sub.NoChargeAfter = new DateTime(2010, 1, 1);
      sub.Period = GCheckout.AutoGen.DatePeriod.DAILY;
      sub.RecurrentItem = new ShoppingCartItem("Sub Item", "Sub Item Description", "Item 1", 0, 3);
      sub.StartDate = new DateTime(2009, 7, 1);
      sub.Type = SubscriptionType.merchant;
      ShoppingCartItem item = request.AddItem("Item 1", "Cool Candy 1", 1, sub);
      //Console.WriteLine(EncodeHelper.Utf8BytesToString(request.GetXml()));

      //now deserialize it
      AutoGen.CheckoutShoppingCart cart = EncodeHelper.Deserialize(request.GetXml()) as AutoGen.CheckoutShoppingCart;
      //Console.WriteLine(EncodeHelper.Utf8BytesToString(EncodeHelper.Serialize(cart)));

      foreach (AutoGen.Item ai in cart.shoppingcart.items) {
        ShoppingCartItem ci = new ShoppingCartItem(ai);
        Assert.AreEqual(ci.Subscription.NoChargeAfter, sub.NoChargeAfter);
        SubscriptionPayment sp = new SubscriptionPayment(ai.subscription.payments[0]);
        SubscriptionPayment dp = sub.Payments[0];

        Assert.AreEqual(sp.MaximumCharge, dp.MaximumCharge);
        Assert.AreEqual(sp.Times, dp.Times);

        Assert.AreEqual(ci.Subscription.Period, sub.Period);
        Assert.AreEqual(ci.Subscription.StartDate, sub.StartDate);
        Assert.AreEqual(ci.Subscription.Type, sub.Type);

      }

      sub.NoChargeAfter = null;
      sub.Period = GCheckout.AutoGen.DatePeriod.QUARTERLY;
      sub.StartDate = null;
      sub.Type = SubscriptionType.google;

      //reset
      cart = EncodeHelper.Deserialize(request.GetXml()) as AutoGen.CheckoutShoppingCart;
      //Console.WriteLine(EncodeHelper.Utf8BytesToString(EncodeHelper.Serialize(cart)));

      foreach (AutoGen.Item ai in cart.shoppingcart.items) {
        ShoppingCartItem ci = new ShoppingCartItem(ai);
        //Console.WriteLine(ci.Subscription.NoChargeAfter);
        Assert.IsFalse(ci.Subscription.NoChargeAfter.HasValue, "NoChargeAfter should be null");
        SubscriptionPayment sp = new SubscriptionPayment(ai.subscription.payments[0]);
        SubscriptionPayment dp = sub.Payments[0];

        Assert.AreEqual(sp.MaximumCharge, dp.MaximumCharge);
        Assert.AreEqual(sp.Times, dp.Times);

        Assert.AreEqual(ci.Subscription.Period, DatePeriod.QUARTERLY);
        //Console.WriteLine(ci.Subscription.StartDate);
        //bug in .net
        //Assert.IsFalse(ci.Subscription.StartDate.HasValue, "StartDate should be null");
        Assert.AreEqual(ci.Subscription.Type, SubscriptionType.google);

      }

    }

    /// <exclude/>
    [Test]
    public void RequestInitialAuthDetails() {
      byte[] Xml;
      AutoGen.CheckoutShoppingCart Cart;
      CheckoutShoppingCartRequest Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Sandbox, "USD", 0);
      Req.AddItem("Mars bar", "", 0.75m, 2);
      Req.AddFlatRateShippingMethod("USPS", 4.30m);
      // Check the <order-processing-support> tag is not there by default.
      Xml = Req.GetXml();
      Cart = (AutoGen.CheckoutShoppingCart)EncodeHelper.Deserialize(Xml);
      Assert.IsNull(Cart.orderprocessingsupport);
      // Set RequestInitialAuthDetails and check that the XML changes.
      Req.RequestInitialAuthDetails = true;
      Xml = Req.GetXml();
      Cart = (AutoGen.CheckoutShoppingCart)EncodeHelper.Deserialize(Xml);
      Assert.IsNotNull(Cart.orderprocessingsupport);
      Assert.IsTrue(Cart.orderprocessingsupport.requestinitialauthdetails);
      Assert.IsTrue(Cart.orderprocessingsupport.requestinitialauthdetailsSpecified);
    }

    /// <exclude/>
    [Test]
    public void CartExpiration() {
      byte[] Xml;
      AutoGen.CheckoutShoppingCart Cart;
      CheckoutShoppingCartRequest Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Sandbox, "USD", 0);
      Req.AddItem("Mars bar", "", 0.75m, 2);
      Req.AddFlatRateShippingMethod("USPS", 4.30m);
      Req.SetExpirationMinutesFromNow(10);
      Xml = Req.GetXml();
      Cart = (AutoGen.CheckoutShoppingCart)EncodeHelper.Deserialize(Xml);
      DateTime Exp = Cart.shoppingcart.cartexpiration.gooduntildate;
      Exp = Exp.ToLocalTime();
      TimeSpan T = Exp.Subtract(DateTime.Now);
      Assert.IsTrue(T.TotalSeconds <= 600);
      Assert.IsTrue(T.TotalSeconds > 595);
    }

    /// <exclude/>
    [Test]
    public void PostUrl() {
      CheckoutShoppingCartRequest Req;
      // Sandbox.
      Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Sandbox, "USD", 0);
      Assert.AreEqual("https://sandbox.google.com/checkout/api/checkout/v2/merchantCheckout/Merchant/123", Req.GetPostUrl());
      Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Production, "USD", 0);
      Assert.AreEqual("https://checkout.google.com/api/checkout/v2/merchantCheckout/Merchant/123", Req.GetPostUrl());

      Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Sandbox, "USD", 0, true);
      Assert.AreEqual("https://sandbox.google.com/checkout/api/checkout/v2/merchantCheckout/Donations/123", Req.GetPostUrl());

      Req = new CheckoutShoppingCartRequest
        ("123", "456", EnvironmentType.Production, "USD", 0, true);
      Assert.AreEqual("https://checkout.google.com/api/checkout/v2/merchantCheckout/Donations/123", Req.GetPostUrl());

    }

    [Test]
    public void Test_MerchantCode() {
      MerchantCode mc = new MerchantCode();
      mc.AppliedAmount = 1.975m;
      mc.CalculatedAmount = 1.994m;
      Assert.AreEqual(1.98m, mc.AppliedAmount);
      Assert.AreEqual(1.99m, mc.CalculatedAmount);
    }

  }
}
