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
using NUnit.Framework;
using GCheckout.Util;
using GCheckout.Checkout.Tests;
using GCheckout.OrderProcessing;

namespace GCheckout.Checkout.Tests {

  /// <exclude/>
  [TestFixture]
  public class OrderProcessingRequestTests {

    public const string MERCHANT_KEY = "567123098";
    public const string MERCHANT_ID = "987654321";
    public const string ORDER_NUMBER = "1234567890";
    public const string MESSAGE = "This is the test Message";
    public const string REASON = "This is the test Reason";
    public const string COMMENT = "This is a Test Comment";
    public const string MERCHANT_ORDER_NUMBER = "ABCDEFGHIJ";
    public const string UPS_TRACKING = "Z1234567890";

    /// <exclude/>
    [Test]
    public void DeliverOrderRequest() {
      DeliverOrderRequest Req;
      DeliverOrderRequest Req2;
      AutoGen.DeliverOrderRequest D;

      // Test the first constructor.
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(false, D.sendemailSpecified);
      Assert.AreEqual(null, D.trackingdata);

      Req2 = new DeliverOrderRequest(ORDER_NUMBER);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(false, D.sendemailSpecified);
      Assert.AreEqual(null, D.trackingdata);

      // Test the second constructor.
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, "UPS",
        "1234", false);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(true, D.sendemailSpecified);
      Assert.AreEqual(false, D.sendemail);
      Assert.AreEqual("UPS", D.trackingdata.carrier);
      Assert.AreEqual("1234", D.trackingdata.trackingnumber);

      Req2 = new DeliverOrderRequest(ORDER_NUMBER, "UPS", "1234", false);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(true, D.sendemailSpecified);
      Assert.AreEqual(false, D.sendemail);
      Assert.AreEqual("UPS", D.trackingdata.carrier);
      Assert.AreEqual("1234", D.trackingdata.trackingnumber);

      // Test the third constructor.
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, "UPS",
        "1234");
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(false, D.sendemailSpecified);
      Assert.AreEqual("UPS", D.trackingdata.carrier);
      Assert.AreEqual("1234", D.trackingdata.trackingnumber);

      Req2 = new DeliverOrderRequest(ORDER_NUMBER, "UPS", "1234");
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(false, D.sendemailSpecified);
      Assert.AreEqual("UPS", D.trackingdata.carrier);
      Assert.AreEqual("1234", D.trackingdata.trackingnumber);

      // Test the fourth constructor.
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, false);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(true, D.sendemailSpecified);
      Assert.AreEqual(false, D.sendemail);
      Assert.AreEqual(null, D.trackingdata);

      // Test the 8th constructor.
      Req2 = new DeliverOrderRequest(ORDER_NUMBER, false);
      D = ParseDeliverOrderRequest(Req.GetXml());
      Assert.AreEqual(Req2.GoogleOrderNumber, D.googleordernumber);
      Assert.AreEqual(true, D.sendemailSpecified);
      Assert.AreEqual(false, D.sendemail);
      Assert.AreEqual(null, D.trackingdata);
    }

    [Test]
    public void TestHelloRequest1() {
      HelloRequest hr = new HelloRequest();
      AutoGen.Hello sr = EncodeHelper.Deserialize(hr.GetXml()) as AutoGen.Hello;
      Assert.IsNotNull(sr);
      hr = new HelloRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Production.ToString());
      Assert.AreEqual(MERCHANT_ID, hr.MerchantID);
      Assert.AreEqual(MERCHANT_KEY, hr.MerchantKey);
      Assert.AreEqual(EnvironmentType.Production, hr.Environment);
    }

    [Test]
    public void TestNotificationDataRequest() {
      NotificationDataRequest hr = new NotificationDataRequest();
      AutoGen.NotificationDataRequest sr = EncodeHelper.Deserialize(hr.GetXml()) as AutoGen.NotificationDataRequest;
      Assert.IsNotNull(sr);

      hr = new NotificationDataRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Production.ToString(), "test_token");
      Assert.AreEqual(MERCHANT_ID, hr.MerchantID);
      Assert.AreEqual(MERCHANT_KEY, hr.MerchantKey);
      Assert.AreEqual(EnvironmentType.Production, hr.Environment);
      Assert.AreEqual("test_token", hr.Token);
    }

    [Test]
    public void TestNotificationDataResponse() {
      AutoGen.NotificationDataResponse dr = new GCheckout.AutoGen.NotificationDataResponse();
      dr.continuetoken = "ct";
      dr.hasmorenotifications = true;
      dr.hasmorenotificationsSpecified = true;
      dr.notifications = new GCheckout.AutoGen.NotificationDataResponseNotifications();
      dr.serialnumber = "12345";

      string message = EncodeHelper.Utf8BytesToString(EncodeHelper.Serialize(dr));

      NotificationDataResponse hr = new NotificationDataResponse(message);

      Assert.AreEqual(hr.IsGood, true);
      Assert.AreEqual(hr.SerialNumber, "12345");
    }

    /// <exclude/>
    [Test]
    public void UnarchiveOrderRequestTests() {
      UnarchiveOrderRequest req = new UnarchiveOrderRequest(ORDER_NUMBER);
      AutoGen.UnarchiveOrderRequest post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.UnarchiveOrderRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);

      req = new UnarchiveOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.UnarchiveOrderRequest;
    }

    /// <exclude/>
    [Test]
    public void AuthorizeOrderRequestTests() {
      AuthorizeOrderRequest req = new AuthorizeOrderRequest(ORDER_NUMBER);
      AutoGen.AuthorizeOrderRequest post
        = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.AuthorizeOrderRequest;
      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);

      req = new AuthorizeOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      post
        = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.AuthorizeOrderRequest;
      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);
    }

    /// <exclude/>
    [Test]
    public void SendBuyerMessageRequestTests() {
      SendBuyerMessageRequest req = new SendBuyerMessageRequest(ORDER_NUMBER, MESSAGE, true);
      AutoGen.SendBuyerMessageRequest post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.SendBuyerMessageRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);
      Assert.AreEqual(req.Message, post.message);
      Assert.AreEqual(req.SendEmail, true);

      req = new SendBuyerMessageRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, MESSAGE, true);
      post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.SendBuyerMessageRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);
      Assert.AreEqual(req.Message, post.message);
      Assert.AreEqual(req.SendEmail, post.sendemail);

      req = new SendBuyerMessageRequest(ORDER_NUMBER, MESSAGE);
      post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.SendBuyerMessageRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);
      Assert.AreEqual(req.Message, post.message);
      Assert.AreEqual(req.SendEmail, post.sendemail);

      req = new SendBuyerMessageRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, MESSAGE);
      post = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.SendBuyerMessageRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);
      Assert.AreEqual(req.Message, post.message);
      Assert.AreEqual(req.SendEmail, post.sendemail, "Send Email");
    }

    /// <exclude/>
    [Test]
    public void CancelOrderRequest() {
      CancelOrderRequest Req;
      AutoGen.CancelOrderRequest D;
      // Test the first constructor.
      Req = new CancelOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON);
      D = ParseCancelOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.comment);
      Assert.AreEqual(REASON, D.reason);
      Assert.AreEqual(Req.GoogleOrderNumber, D.googleordernumber);

      // Test the second constructor.
      Req = new CancelOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON, COMMENT);
      D = ParseCancelOrderRequest(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(COMMENT, D.comment);
      Assert.AreEqual(REASON, D.reason);

      Req = new CancelOrderRequest(ORDER_NUMBER, REASON);
      D = ParseCancelOrderRequest(Req.GetXml());

      Req = new CancelOrderRequest(ORDER_NUMBER, REASON, COMMENT);
      D = ParseCancelOrderRequest(Req.GetXml());

    }

    /// <exclude/>
    [Test, ExpectedException(typeof(ApplicationException))]
    public void InvalidCancelOrderRequest1() {
      // Reason is not allowed to be null.
      CancelOrderRequest Req = new CancelOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox",
        ORDER_NUMBER, null);
    }

    /// <exclude/>
    [Test, ExpectedException(typeof(ApplicationException))]
    public void InvalidCancelOrderRequest2() {
      // Reason is not allowed to be an empty string.
      CancelOrderRequest Req = new CancelOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox",
        ORDER_NUMBER, "");
    }

    /// <exclude/>
    [Test]
    public void ChargeOrderRequest() {
      ChargeOrderRequest Req;
      AutoGen.ChargeOrderRequest D;

      // Test the first constructor.
      Req = new ChargeOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      D = (AutoGen.ChargeOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.amount);

      // Test the second constructor.
      Req = new ChargeOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, "GBP", 10.2m);
      D = (AutoGen.ChargeOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual("GBP", D.amount.currency);
      Assert.AreEqual(10.2m, D.amount.Value);

      Req = new ChargeOrderRequest(ORDER_NUMBER);
      D = (AutoGen.ChargeOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(Req.GoogleOrderNumber, D.googleordernumber);

      Req = new ChargeOrderRequest(ORDER_NUMBER, "USD", 12.975m);
      D = (AutoGen.ChargeOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(Req.GoogleOrderNumber, D.googleordernumber);
      Assert.AreEqual(Req.Amount, 12.98m);
    }

    /// <exclude/>
    [Test]
    public void ProcessOrderRequest() {
      byte[] xml;
      ProcessOrderRequest req = new ProcessOrderRequest(ORDER_NUMBER);
      xml = req.GetXml();

      AutoGen.ProcessOrderRequest post
        = EncodeHelper.Deserialize(xml) as AutoGen.ProcessOrderRequest;

      Assert.AreEqual(req.GoogleOrderNumber, post.googleordernumber);

      req = new ProcessOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      xml = req.GetXml();


    }

    /// <exclude/>
    [Test]
    public void RefundOrderRequest() {
      RefundOrderRequest Req;
      AutoGen.RefundOrderRequest D;
      // Test the first constructor.
      Req = new RefundOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.amount);
      Assert.AreEqual(null, D.comment);
      Assert.AreEqual(REASON, D.reason);
      Assert.AreEqual(Req.GoogleOrderNumber, D.googleordernumber);

      // Test the second constructor.
      Req = new RefundOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON, "GBP", 100.975m);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual("GBP", D.amount.currency);
      Assert.AreEqual(100.98m, D.amount.Value);
      Assert.AreEqual(null, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the third constructor.
      Req = new RefundOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON, COMMENT);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.amount);
      Assert.AreEqual(COMMENT, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the fourth constructor.
      Req = new RefundOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER,
        REASON, "USD", 100.993m, COMMENT);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual("USD", D.amount.currency);
      Assert.AreEqual(100.99m, D.amount.Value);
      Assert.AreEqual(COMMENT, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the fifth constructor.
      Req = new RefundOrderRequest(ORDER_NUMBER, REASON);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the sixth constructor.
      Req = new RefundOrderRequest(ORDER_NUMBER,
        REASON, "USD", 100.993m);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(100.99m, D.amount.Value);
      Assert.AreEqual(null, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the seventh constructor.
      Req = new RefundOrderRequest(ORDER_NUMBER,
        REASON, COMMENT);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(null, D.amount);
      Assert.AreEqual(COMMENT, D.comment);
      Assert.AreEqual(REASON, D.reason);

      // Test the eighth constructor.
      Req = new RefundOrderRequest(ORDER_NUMBER,
        REASON, "USD", 100.993m, COMMENT);
      D = (AutoGen.RefundOrderRequest)EncodeHelper.Deserialize(Req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, D.googleordernumber);
      Assert.AreEqual(100.99m, D.amount.Value);
      Assert.AreEqual(COMMENT, D.comment);
      Assert.AreEqual(REASON, D.reason);

    }

    /// <exclude/>
    [Test]
    public void PostUrl() {
      DeliverOrderRequest Req;
      // Sandbox.
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, false);
      Assert.AreEqual("https://sandbox.google.com/checkout/api/checkout/v2/request/Merchant/" + MERCHANT_ID, Req.GetPostUrl());
      Req = new DeliverOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Production", ORDER_NUMBER, false);
      Assert.AreEqual("https://checkout.google.com/api/checkout/v2/request/Merchant/" + MERCHANT_ID, Req.GetPostUrl());
    }

    /// <exclude/>
    [Test]
    public void AddTrackingDataRequestTests() {
      AddTrackingDataRequest req
        = new AddTrackingDataRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox",
        ORDER_NUMBER, "UPS", UPS_TRACKING);
      Assert.AreEqual(ORDER_NUMBER, req.GoogleOrderNumber);
      Assert.AreEqual("UPS", req.Carrier);
      Assert.AreEqual(UPS_TRACKING, req.TrackingNo);
      req.GetXml();

      req = new AddTrackingDataRequest(ORDER_NUMBER, "UPS", UPS_TRACKING);
      Assert.AreEqual("UPS", req.Carrier);
      Assert.AreEqual(UPS_TRACKING, req.TrackingNo);
      req.GetXml();
    }

    /// <exclude/>
    [Test]
    public void ArchiveOrderRequestTests() {
      ArchiveOrderRequest req = new ArchiveOrderRequest(ORDER_NUMBER);
      ArchiveOrderRequest req2
        = EncodeHelper.Deserialize(req.GetXml()) as ArchiveOrderRequest;

      Assert.AreEqual(ORDER_NUMBER, req.GoogleOrderNumber);

      req = new ArchiveOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      Assert.AreEqual(ORDER_NUMBER, req.GoogleOrderNumber);

    }

    /// <exclude/>
    [Test]
    public void TestAddMerchantOrderNumberRequest() {
      AddMerchantOrderNumberRequest req = new AddMerchantOrderNumberRequest(ORDER_NUMBER, MERCHANT_ORDER_NUMBER);
      AutoGen.AddMerchantOrderNumberRequest D
        = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.AddMerchantOrderNumberRequest;

      Assert.AreEqual(req.GoogleOrderNumber, D.googleordernumber);
      Assert.AreEqual(MERCHANT_ORDER_NUMBER, D.merchantordernumber);

      req = new AddMerchantOrderNumberRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, MERCHANT_ORDER_NUMBER);
      D = EncodeHelper.Deserialize(req.GetXml()) as AutoGen.AddMerchantOrderNumberRequest;

      Assert.AreEqual(req.GoogleOrderNumber, D.googleordernumber);
      Assert.AreEqual(MERCHANT_ORDER_NUMBER, D.merchantordernumber);
    }

    [Test]
    public void TestChargeAndShipOrderRequest() {
      ChargeAndShipOrderRequest req;
      AutoGen.ChargeAndShipOrderRequest d;

      // Test the first constructor.
      req = new ChargeAndShipOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER);
      d = (AutoGen.ChargeAndShipOrderRequest)EncodeHelper.Deserialize(req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, d.googleordernumber);
      Assert.AreEqual(null, d.amount);

      // Test the second constructor.
      req = new ChargeAndShipOrderRequest(MERCHANT_ID, MERCHANT_KEY, "Sandbox", ORDER_NUMBER, "GBP", 10.2m);
      d = (AutoGen.ChargeAndShipOrderRequest)EncodeHelper.Deserialize(req.GetXml());
      Assert.AreEqual(ORDER_NUMBER, d.googleordernumber);
      Assert.AreEqual("GBP", d.amount.currency);
      Assert.AreEqual(10.2m, d.amount.Value);

      req = new ChargeAndShipOrderRequest(ORDER_NUMBER);
      d = (AutoGen.ChargeAndShipOrderRequest)EncodeHelper.Deserialize(req.GetXml());
      Assert.AreEqual(req.GoogleOrderNumber, d.googleordernumber);

      req = new ChargeAndShipOrderRequest(ORDER_NUMBER);
      req.Amount = 12.98m;
      d = (AutoGen.ChargeAndShipOrderRequest)EncodeHelper.Deserialize(req.GetXml());
      Assert.AreEqual(req.GoogleOrderNumber, d.googleordernumber);
      Assert.AreEqual(req.Amount, 12.98m);


      req = new ChargeAndShipOrderRequest(ORDER_NUMBER);
      req.Amount = 12.98m;
      req.SendEmail = true;
      req.ItemShippingInformation.AddMerchantItemId("UPS", "123456", "12");
      req.TrackingDataList.AddTrackingData("UPS", "987655");
      d = (AutoGen.ChargeAndShipOrderRequest)EncodeHelper.Deserialize(req.GetXml());
      Assert.AreEqual(req.GoogleOrderNumber, d.googleordernumber);
      Assert.AreEqual(req.Amount, 12.98m);
      Assert.AreEqual(req.SendEmail, d.sendemail);

    }

    [Test]
    public void TestOrderSummaryRequest() {
      var shipRequestOriginal = new OrderSummaryRequest("1234567890");
      var shipRequestNew = EncodeHelper.Deserialize(shipRequestOriginal.GetXml()) as AutoGen.OrderSummaryRequest;
      Assert.AreEqual(shipRequestOriginal.GoogleOrderNumbers[0], shipRequestNew.ordernumbers[0]);

      //we need to simulate a response.
      var tempResonse = new AutoGen.OrderSummaryResponse();
      tempResonse.serialnumber = "1234567890";
      var tempSummary = new AutoGen.OrderSummary();
      tempSummary.buyerid = 12;
      tempSummary.googleordernumber = "1234567890";
      tempResonse.ordersummaries = new GCheckout.AutoGen.OrderSummary[] { tempSummary };
      var tempXml = EncodeHelper.Utf8BytesToString(EncodeHelper.Serialize(tempResonse));

      var shipResponse = new OrderSummaryResponse(tempXml);

      Assert.AreEqual(1, shipResponse.OrderSummary.Count);
      Assert.AreEqual("1234567890", shipResponse.OrderSummary[0].googleordernumber);

    }

    private AutoGen.DeliverOrderRequest ParseDeliverOrderRequest(byte[] Xml) {
      object testVal = null;

      Type T = typeof(AutoGen.DeliverOrderRequest);

      testVal = EncodeHelper.Deserialize(Xml);
      Assert.IsNotNull(testVal);
      Assert.AreEqual(testVal.GetType(), T);

      string Xml2 = EncodeHelper.Utf8BytesToString(Xml);

      //we want to test the generic Deserialize Method first.
      testVal = EncodeHelper.Deserialize(Xml2);
      Assert.IsNotNull(testVal);
      Assert.AreEqual(testVal.GetType(), T);

      return (AutoGen.DeliverOrderRequest)EncodeHelper.Deserialize(Xml2, T);
    }

    private AutoGen.CancelOrderRequest ParseCancelOrderRequest(byte[] Xml) {
      Type T = typeof(AutoGen.CancelOrderRequest);
      string Xml2 = EncodeHelper.Utf8BytesToString(Xml);
      return (AutoGen.CancelOrderRequest)EncodeHelper.Deserialize(Xml2, T);
    }
  }
}
