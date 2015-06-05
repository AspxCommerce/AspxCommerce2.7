using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using GCheckout.MerchantCalculation;

namespace GCheckout.Checkout.Tests {

  [TestFixture]
  public class MerchantCalculationTests {

    [Test]
    public void TestAnonymousAddress() {

      GCheckout.AutoGen.AnonymousAddress ag = new GCheckout.AutoGen.AnonymousAddress();
      ag.city = "city";
      ag.countrycode = "ca";
      ag.id = "12";
      ag.postalcode = "12345";
      ag.region = "zz";

      AnonymousAddress aa = new AnonymousAddress(ag);
      Assert.AreEqual(aa.City, "city");
      Assert.AreEqual(aa.CountryCode, "ca");
      Assert.AreEqual(aa.Id, "12");
      Assert.AreEqual(aa.PostalCode, "12345");
      Assert.AreEqual(aa.Region, "zz");
    }

    [Test]
    public void TestMerchantCodeResult() {
      MerchantCodeResult mcr = new MerchantCodeResult();
      mcr.Amount = 12.975m;
      mcr.Message = "Hello";
      mcr.Type = GCheckout.MerchantCalculation.MerchantCodeType.Coupon;
      mcr.Valid = true;

      Assert.AreEqual(12.98, mcr.Amount);
      Assert.AreEqual("Hello", mcr.Message);
      Assert.AreEqual(GCheckout.MerchantCalculation.MerchantCodeType.Coupon, mcr.Type);
      Assert.IsTrue(mcr.Valid);

    }

    [Test]
    public void TestShippingResult() {
      ShippingResult sr = new ShippingResult();
      sr.Shippable = true;
      sr.ShippingRate = 12.975m;
      Assert.IsTrue(sr.Shippable);
      Assert.AreEqual(12.98, sr.ShippingRate);
    }

  }
}
