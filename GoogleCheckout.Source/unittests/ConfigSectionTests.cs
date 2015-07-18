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
using NUnit.Framework;
using System.Xml;
using System.IO;
using System.Diagnostics;
using GCheckout.Util;


namespace GCheckout.Checkout.Tests {

  /// <exclude/>
  [TestFixture]
  public class ConfigSectionTests {

    /// <exclude/>
    [Test]
    public void TestConfigSections() {
      Debug.Assert(GCheckoutConfigurationHelper.Currency == "GBP", "Currency expected GBP");
      Debug.Assert(GCheckoutConfigurationHelper.Environment == GCheckout.EnvironmentType.Sandbox, "Expected Sandbox");
      //if logging is set to false, this test will fail.
      //we may want to change this test.
      Debug.Assert(GCheckoutConfigurationHelper.LogDirectory == @"c:\test\", @"LogDirectory expected c:\test\ ended with:'" + GCheckoutConfigurationHelper.LogDirectory + "'");

      Debug.Assert(GCheckoutConfigurationHelper.Logging == true, "Logging expected true");

      if (GCheckoutConfigurationHelper.Environment == GCheckout.EnvironmentType.Sandbox) {
        Debug.Assert(GCheckoutConfigurationHelper.MerchantID == 25235626236, "MerchantID expected 25235626236");
        Debug.Assert(GCheckoutConfigurationHelper.MerchantKey == "sdgsdgsdg", "MerchantKey expected sdgsdgsdg");
      }
      else {
        Debug.Assert(GCheckoutConfigurationHelper.MerchantID == 557347325253, "MerchantID expected 557347325253");
        Debug.Assert(GCheckoutConfigurationHelper.MerchantKey == "sdgsghdjfgjeesdf", "MerchantKey expected sdgsghdjfgjeesdf");
      }

      Debug.Assert(GCheckoutConfigurationHelper.PlatformID == 124248135971073, "PlatformID expected 124248135971073");
      Debug.Assert(GCheckoutConfigurationHelper.ProxyDomain == "mydomain", "ProxyDomain expected mydomain");
      Debug.Assert(GCheckoutConfigurationHelper.ProxyHost == "http://www.sitename.com:2345/", "ProxyHost expected http://www.sitename.com:2345/");
      Debug.Assert(GCheckoutConfigurationHelper.ProxyPassword == "235235", "ProxyPassword expected 235235");
      Debug.Assert(GCheckoutConfigurationHelper.ProxyUserName == "35235", "ProxyUserName expected 35235");
      Debug.Assert(GCheckoutConfigurationHelper.UseProxy == true, "UseProxy expected true");


    }

  }
}
