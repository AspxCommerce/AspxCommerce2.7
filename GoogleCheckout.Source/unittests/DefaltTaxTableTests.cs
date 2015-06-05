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
 *  4-21-2011   Joe Feser joe.feser@joefeser.com
 *  Fixed a bug in Tax tables where the rateSpecified was not being set
*/

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using GCheckout.Checkout;
using GCheckout.AutoGen;
using GCheckout.Util;

namespace GCheckout.Checkout.Tests {

  [TestFixture]
  public class DefaltTaxTableTests {

    public const string MERCHANT_KEY = "567123098";
    public const string MERCHANT_ID = "987654321";

    [Test]
    public void VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);
      request.AddPickupShippingMethod("Name", 4.95m);
      request.AddCountryTaxRule(GCheckout.AutoGen.USAreas.ALL, .05, true);
      request.AddWorldAreaTaxRule(.02, true);
      //Tax GB at 5%
      request.AddPostalAreaTaxRule("GB", .05, true);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      Assert.IsTrue(roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0].rateSpecified);
      Assert.IsTrue(roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0].shippingtaxed);
      Assert.IsTrue(roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0].shippingtaxedSpecified);
    }


    [Test]
    public void DefaultTaxTable_AddCountryTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);
      request.AddCountryTaxRule(USAreas.ALL, .05, true);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.IsTrue(actualTaxTable.shippingtaxed);
      Assert.AreEqual(typeof(USCountryArea), actualTaxTable.taxarea.Item.GetType());
    }

    [Test]
    public void DefaultTaxTable_AddPostalAreaTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);
      request.AddPostalAreaTaxRule("CA", .05, true);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.IsTrue(actualTaxTable.shippingtaxed);
      Assert.AreEqual(typeof(PostalArea), actualTaxTable.taxarea.Item.GetType());
    }

    [Test]
    public void DefaultTaxTable_AddStateTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);
      request.AddStateTaxRule("OH", .05, true);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.IsTrue(actualTaxTable.shippingtaxed);
      Assert.AreEqual(typeof(USStateArea), actualTaxTable.taxarea.Item.GetType());

    }

    [Test]
    public void DefaultTaxTable_AddWorldAreaTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);
      request.AddWorldAreaTaxRule(.05, true);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.defaulttaxtable.taxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.IsTrue(actualTaxTable.shippingtaxed);
      Assert.AreEqual(typeof(WorldArea), actualTaxTable.taxarea.Item.GetType());
    }
  }

}
