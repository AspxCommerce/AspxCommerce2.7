using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using GCheckout.AutoGen;
using GCheckout.Util;
using System.Diagnostics;

namespace GCheckout.Checkout.Tests {
 
  [TestFixture]
  public class AlternateTaxTableTests {

    public const string MERCHANT_KEY = "567123098";
    public const string MERCHANT_ID = "987654321";

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAlternateTaxTableNullName() {
      AlternateTaxTable att = new AlternateTaxTable(null);
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAlternateTaxTableSetNameTwice() {
      AlternateTaxTable att = new AlternateTaxTable("Name1");
      att.Name = "Blow up";
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAlternateTaxAddZipCodeRuleBad1() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddZipTaxRule("555555", .025);
    }

    [Test]
    public void TestAlternateTaxAddZipCodeRuleGood() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddZipTaxRule("44444", .025);
    }

    [Test]
    public void TestAlternateTaxAddWorldAreaTaxRule() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddWorldAreaTaxRule(.025);
    }

    [Test]
    public void TestAlternateTaxStandAlone() {
      AlternateTaxTable att = new AlternateTaxTable("name", true);
      att.AddWorldAreaTaxRule(.025);
      Assert.AreEqual(true, att.StandAlone);
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAlternateTaxTableNullNameFailAddWorldAreaTwice() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddWorldAreaTaxRule(.025);
      att.AddWorldAreaTaxRule(.025); //will blow up.
    }

    [Test]
    public void TestAlternateTaxAddPostalCodeRuleGood() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddPostalAreaTaxRule("CA", .040);
      att.AddPostalAreaTaxRule("CA", "5F4F5F", .025);
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAlternateTaxAddPostalCodeBadCountry() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddPostalAreaTaxRule(string.Empty, .040);
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAlternateTaxAddPostalCodeDuplicate() {
      AlternateTaxTable att = new AlternateTaxTable();
      att.AddPostalAreaTaxRule("CA", .040);
      att.AddPostalAreaTaxRule("CA", .025);
    }

    [Test]
    public void TestAlternateTaxTable_AddCountryTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);

      var taxTable = new AlternateTaxTable("ohio");
      request.AlternateTaxTables.Add(taxTable);
      taxTable.AddCountryTaxRule(USAreas.ALL, .05);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.alternatetaxtables[0].alternatetaxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.AreEqual(typeof(USCountryArea), actualTaxTable.taxarea.Item.GetType());
    }

    [Test]
    public void TestAlternateTaxTable_AddPostalAreaTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);

      var taxTable = new AlternateTaxTable("canada");
      request.AlternateTaxTables.Add(taxTable);
      taxTable.AddPostalAreaTaxRule("CA", .05);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.alternatetaxtables[0].alternatetaxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.AreEqual(typeof(PostalArea), actualTaxTable.taxarea.Item.GetType());
    }

    [Test]
    public void TestAlternateTaxTable_AddStateTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);

      var taxTable = new AlternateTaxTable("ohio");
      request.AlternateTaxTables.Add(taxTable);
      taxTable.AddStateTaxRule("OH", .05);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.alternatetaxtables[0].alternatetaxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.AreEqual(typeof(USStateArea), actualTaxTable.taxarea.Item.GetType());

    }

    [Test]
    public void TestAlternateTaxTable_AddWorldAreaTaxRule_VerifyTaxRateSetsIsSpecified() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);

      var taxTable = new AlternateTaxTable("canada");
      request.AlternateTaxTables.Add(taxTable);
      taxTable.AddWorldAreaTaxRule(.05);

      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.alternatetaxtables[0].alternatetaxrules[0];

      Assert.AreEqual(.05, actualTaxTable.rate);
      Assert.IsTrue(actualTaxTable.rateSpecified);
      Assert.AreEqual(typeof(WorldArea), actualTaxTable.taxarea.Item.GetType());
    }

    [Test]
    public void TestAlternateTaxTable_NoRules_VerifyNodeIsCreated() {

      //create a pickup shipping method
      var request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "GBP", 120);

      var taxTable = new AlternateTaxTable("canada");
      request.AlternateTaxTables.Add(taxTable);
      
      CheckoutShoppingCart roundTrip = EncodeHelper.Deserialize(EncodeHelper.Utf8BytesToString(request.GetXml()),
        typeof(CheckoutShoppingCart)) as CheckoutShoppingCart;

      //Debug.WriteLine(EncodeHelper.Utf8BytesToString(request.GetXml()));

      var actualTaxTable = roundTrip.checkoutflowsupport.Item.taxtables.alternatetaxtables[0];

      Assert.IsNotNull(actualTaxTable.alternatetaxrules);
      Assert.IsTrue(actualTaxTable.alternatetaxrules.Length == 0);
    }

    /// <exclude/>
    [Test]
    public void TestAlternateTaxTables() {
      CheckoutShoppingCartRequest request = new CheckoutShoppingCartRequest(MERCHANT_ID, MERCHANT_KEY, EnvironmentType.Sandbox, "USD", 120);

      //Ensure the factory works as expected
      AlternateTaxTable ohio1 = new AlternateTaxTable("ohio");
      request.AlternateTaxTables.Add(ohio1);
      AlternateTaxTable ohio2 = request.AlternateTaxTables["ohio"];
      AlternateTaxTable ohio3 = new AlternateTaxTable("ohio", true);

      //Ensure that two Tax tables with the same name are not the same reference
      Assert.AreSame(ohio1, ohio2);
      Assert.IsFalse(object.ReferenceEquals(ohio1, ohio3));
      //Assert.AreEqual(ohio1, ohio3);

      //Now add some rules to the item
      ohio1.AddStateTaxRule("OH", .02);

      //Make sure we can add an item to the cart.
      ShoppingCartItem item = request.AddItem("Item 1", "Cool Candy 1", 2.00M, 1, ohio1);

      try {
        request.AddItem("Item 2", "Cool Candy 2", 2.00M, 1, ohio3);
        Assert.Fail("An exception should have been thrown when we tried to add an item that has a new Tax Reference");
      }
      catch (Exception) {

      }

      //Now this should work fine.
      request.AddItem("Item 3", "Cool Candy 3", string.Empty, 2.00M, 1, ohio2);

      //you could create this as an IShoppingCartItem or ShoppingCartItem
      IShoppingCartItem newItem = new ShoppingCartItem("Item 2", "Cool Candy 2", string.Empty, 2.00M, 2, AlternateTaxTable.Empty, "This is a test of a string of private data");
      //now decide to change your mind on the quantity and price
      newItem.Price = 20;
      newItem.Quantity = 4;

      request.AddItem(newItem);

      //Console.WriteLine("private data:" + newItem.MerchantPrivateItemData);

      Assert.AreEqual("This is a test of a string of private data", newItem.MerchantPrivateItemData);

      //now change the private data string and compare again.
      newItem.MerchantPrivateItemData = "This is a new String";
      Assert.AreEqual("This is a new String", newItem.MerchantPrivateItemData);

      //now change the private data string and compare again.
      newItem.MerchantPrivateItemData = string.Empty;
      Assert.AreEqual(string.Empty, newItem.MerchantPrivateItemData);

      Assert.AreEqual(1, ohio1.RuleCount);

      DigitalItem emailDigitalItem = new DigitalItem();
      request.AddItem("Email Digital Item", "Cool DigitalItem", 2.00m, 1, emailDigitalItem);

      DigitalItem urlDigitalItem = new DigitalItem(new Uri("http://www.google.com/download.aspx?myitem=1"), "Url Description for item");
      request.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00m, 1, urlDigitalItem);

      DigitalItem keyDigitalItem = new DigitalItem("24-235-sdf-123541-53", "Key Description for item");
      request.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00m, 1, keyDigitalItem);

      DigitalItem keyUrlItem = new DigitalItem("24-235-sdf-123541-53", "http://www.google.com/download.aspx?myitem=1", "Url/Key Description for item");
      request.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00m, 1, keyUrlItem);

      //lets make sure we can add 2 different flat rate shipping amounts

      request.AddFlatRateShippingMethod("UPS Ground", 5);
      request.AddFlatRateShippingMethod("UPS 2 Day Air", 25);
      request.AddFlatRateShippingMethod("Test", 12, new ShippingRestrictions());

      //You can't mix shipping methods
      try {
        request.AddMerchantCalculatedShippingMethod("Test", 12.95m);
        Assert.Fail("AddCarrierCalculatedShippingOption should not allow duplicates.");
      }
      catch {
      }

      //lets try adding a Carrier Calculated Shipping Type

      //this should fail because the city is empty
      try {
        request.AddShippingPackage("failedpackage", string.Empty, "OH", "44114", DeliveryAddressCategory.COMMERCIAL, 2, 3, 4);
        Assert.Fail("AddCarrierCalculatedShippingOption should not allow duplicates.");
      }
      catch {
      }

      //The first thing that needs to be done for carrier calculated shipping is we must set the FOB address.
      request.AddShippingPackage("main", "Cleveland", "OH", "44114", DeliveryAddressCategory.COMMERCIAL, 2, 3, 4);

      //this should fail because two packages exist
      try {
        request.AddShippingPackage("failedpackage", "Cleveland", "OH", "44114", DeliveryAddressCategory.COMMERCIAL, 2, 3, 4);
        Assert.Fail("AddCarrierCalculatedShippingOption should not allow duplicates.");
      }
      catch {
      }

      try {
        request.AddShippingPackage("main", "Cleveland", "OH", "44114");
        Assert.Fail("AddCarrierCalculatedShippingOption should not allow duplicates.");
      }
      catch {
      }

      //The next thing we will do is add a Fedex Home Package.
      //We will set the default to 3.99, the Pickup to Regular Pickup, the additional fixed charge to 1.29 and the discount to 2.5%
      CarrierCalculatedShippingOption option
        = request.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Home_Delivery, 3.99m, CarrierPickup.REGULAR_PICKUP, 1.29m, -2.5);
      option.AdditionalVariableChargePercent = 0; //make sure we can set it back to 0;
      option.AdditionalFixedCharge = 0;

      Assert.AreEqual(option.StatedShippingType, ShippingType.Fedex_Home_Delivery);
      Assert.AreEqual(option.Price, 3.99m);

      Assert.AreEqual(option.AdditionalVariableChargePercent, 0);
      Assert.AreEqual(option.AdditionalFixedCharge, 0);

      try {
        option.AdditionalFixedCharge = -1;
        Assert.Fail("Additional charge must be >= 0");
      }
      catch {
      }

      option.AdditionalVariableChargePercent = 2; //make sure we can set it back to 0;
      option.AdditionalFixedCharge = 3;

      Assert.AreEqual(option.AdditionalVariableChargePercent, 2);
      Assert.AreEqual(option.AdditionalFixedCharge, 3);

      //this should fail
      try {
        request.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Home_Delivery, 3.99m, CarrierPickup.REGULAR_PICKUP, 1.29m, -2.5);
        Assert.Fail("AddCarrierCalculatedShippingOption should not allow duplicates.");
      }
      catch {

      }

      //verify the rounding works
      CarrierCalculatedShippingOption ccso = request.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Ground, 1.993m);
      Assert.AreEqual(1.99m, ccso.Price);
      ccso.Price = 1.975m;
      Assert.AreEqual(1.98m, ccso.Price);
      request.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Second_Day, 9.99m, CarrierPickup.REGULAR_PICKUP, 2.34m, -24.5);

      //Ensure we are able to create the cart xml

      byte[] cart = request.GetXml();

      //Console.WriteLine(EncodeHelper.Utf8BytesToString(cart));

      //test to see if the item can desialize
      Assert.IsNotNull(GCheckout.Util.EncodeHelper.Deserialize(cart));
    }

    //Collection Tests Start Collection Tests Start Collection Tests Start Collection Tests Start Collection Tests Start

    [Test]
    public void TestAlternateTaxTableCollectionTestExists() {
      AlternateTaxTableCollection attc = new AlternateTaxTableCollection();
      attc.Add(new AlternateTaxTable("test"));
      Assert.IsTrue(attc.Exists("test"));
    }

    [Test]
    public void TestAlternateTaxTableCollectionTestFactories() {
      AlternateTaxTableCollection attc = new AlternateTaxTableCollection();
      AlternateTaxTable att = attc.Factory("name");
      Assert.AreEqual(att.Name, "name");
      att = attc.Factory("name2", true);
      Assert.AreEqual(att.Name, "name2");
      Assert.AreEqual(att.StandAlone, true);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAlternateTaxTableCollectionTestFactoriesNullName() {
      AlternateTaxTableCollection attc = new AlternateTaxTableCollection();
      AlternateTaxTable att = attc.Factory(null);
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAlternateTaxTableCollectionTestFactoriesDuplicateName() {
      AlternateTaxTableCollection attc = new AlternateTaxTableCollection();
      AlternateTaxTable att = attc.Factory("name");
      att = attc.Factory("name"); //blow up
    }

    [Test]
    public void TestAlternateTaxTableCollectionTestDelete() {
      AlternateTaxTableCollection attc = new AlternateTaxTableCollection();
      AlternateTaxTable att = attc.Factory("name");
      attc.Factory("name2", true);
      Assert.IsTrue(attc.Exists("name"));
      Assert.IsTrue(attc.Exists("name2"));
      attc.Delete("name");
      Assert.IsFalse(attc.Exists("name"));
      Assert.IsTrue(attc.Exists("name2"));
    }

  }
}
