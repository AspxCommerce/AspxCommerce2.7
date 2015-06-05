/*************************************************
 * Copyright (C) 2011 Google Inc.
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
 *  4-21-2011   Joe Feser joe.feser@joefeser.com
 *  Initial Coding
 *  Place the code from one of these methods into your button click event
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GCheckout;
using GCheckout.Checkout;
using GCheckout.Util;
using System.Diagnostics;

namespace SubscriptionExamples {

  class Program {

    // Methods
    public static void InitialCharge() {

      //http://code.google.com/apis/checkout/developer/Google_Checkout_Beta_Subscriptions.html
      //using an initial charge with a recurring charge using a different item.

      CheckoutShoppingCartRequest cartRequest
        = new CheckoutShoppingCartRequest("123456", "merchantkey", EnvironmentType.Sandbox, "USD", 120);

      //if you are using a web page and it has the Google Checkout button, you would use this syntax.
      //= GCheckoutButton1.CreateRequest()

      ShoppingCartItem initialItem = new ShoppingCartItem();
      ShoppingCartItem recurrentItem = new ShoppingCartItem();

      initialItem.Price = decimal.Zero;
      initialItem.Quantity = 1;
      initialItem.Name = "Item that shows in cart";
      initialItem.Description = "This is the item that shows in the cart";

      recurrentItem.Price = 2M;
      recurrentItem.Quantity = 1;
      recurrentItem.Name = "Item that is charged every month";
      recurrentItem.Description = "Description for item that is charged every month";

      Subscription subscription = new Subscription();
      subscription.Period = GCheckout.AutoGen.DatePeriod.MONTHLY;
      subscription.Type = SubscriptionType.merchant;

      SubscriptionPayment payment = new SubscriptionPayment();
      payment.MaximumCharge = 120M;
      payment.Times = 12;

      subscription.AddSubscriptionPayment(payment);

      //You must set the item that will be charged for every month.
      subscription.RecurrentItem = recurrentItem;

      //you must set the subscription for the item
      initialItem.Subscription = subscription;

      cartRequest.AddItem(initialItem);

      Debug.WriteLine(EncodeHelper.Utf8BytesToString(cartRequest.GetXml()));

      //Send the request to Google
      //GCheckout.Util.GCheckoutResponse resp = cartRequest.Send();

      //Uncommment this line or perform additional actions
      //if (resp.IsGood) {
      //Response.Redirect(resp.RedirectUrl, True)
      //}
      //else{
      //Response.Write("Resp.ResponseXml = " & Resp.ResponseXml & "<br>");
      //Response.Write("Resp.RedirectUrl = " & Resp.RedirectUrl & "<br>");
      //Response.Write("Resp.IsGood = " & Resp.IsGood & "<br>");
      //Response.Write("Resp.ErrorMessage = " & Resp.ErrorMessage & "<br>");      
      //}
    }

    [STAThread]
    public static void Main() {
      InitialCharge();
      RecurringChargeRightAway();
    }

    public static void RecurringChargeRightAway() {

      CheckoutShoppingCartRequest cartRequest
        = new CheckoutShoppingCartRequest("123456", "merchantkey", EnvironmentType.Sandbox, "USD", 120);
      //if you are using a web page and it has the Google Checkout button, you would use this syntax.
      //= GCheckoutButton1.CreateRequest()

      Subscription gSubscription = new Subscription();
      SubscriptionPayment maxCharge = new SubscriptionPayment();

      DigitalItem urlDigitalItem = new DigitalItem(new Uri("http://www.url.com/login.aspx"),
        "Congratulations, your account has been created!");

      DigitalItem urlDigitalItemSubscription = new DigitalItem(new Uri("http://www.url.com/login.aspx"),
        "You may now continue to login to your account.");

      ShoppingCartItem gRecurrentItem = new ShoppingCartItem();
      maxCharge.MaximumCharge = 29.99M;

      gRecurrentItem.Name = "Entry Level Plan";
      gRecurrentItem.Description = "Allows for basic stuff. Monthly Subscription:";
      gRecurrentItem.Quantity = 1;
      gRecurrentItem.Price = 29.99M;
      gRecurrentItem.DigitalContent = urlDigitalItemSubscription;
      gRecurrentItem.DigitalContent.Disposition = DisplayDisposition.Pessimistic;

      urlDigitalItem.Disposition = DisplayDisposition.Pessimistic;

      gSubscription.Type = SubscriptionType.google;
      gSubscription.Period = GCheckout.AutoGen.DatePeriod.MONTHLY;
      gSubscription.AddSubscriptionPayment(maxCharge);
      gSubscription.RecurrentItem = gRecurrentItem;

      cartRequest.AddItem("Entry Level Plan", "Allows for basic stuff.", 1, gSubscription);
      cartRequest.AddItem("Entry Level Plan", "First Month:", 29.99M, 1, urlDigitalItem);

      cartRequest.MerchantPrivateData = "UserName:Joe87";

      Debug.WriteLine(EncodeHelper.Utf8BytesToString(cartRequest.GetXml()));

      //Send the request to Google
      //GCheckout.Util.GCheckoutResponse resp = cartRequest.Send();

      //Uncommment this line or perform additional actions
      //if (resp.IsGood) {
      //Response.Redirect(resp.RedirectUrl, True)
      //}
      //else{
      //Response.Write("Resp.ResponseXml = " & Resp.ResponseXml & "<br>");
      //Response.Write("Resp.RedirectUrl = " & Resp.RedirectUrl & "<br>");
      //Response.Write("Resp.IsGood = " & Resp.IsGood & "<br>");
      //Response.Write("Resp.ErrorMessage = " & Resp.ErrorMessage & "<br>");      
      //}

    }

  }
}
