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
 *  3-14-2009   Joe Feser joe.feser@joefeser.com
 *  We no longer allow people to pass in fractional amounts. All numbers are trimmed to $x.xx
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Fix issue 67 and cleaned up the default currency and amount defaults
 * 
*/
using System;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// This class contains methods that construct &lt;charge-order&gt; API 
  /// requests.
  /// </summary>
  public class ChargeOrderRequest : OrderProcessingBase {
    private string _currency = GCheckoutConfigurationHelper.Currency;
    private decimal _amount = decimal.MinValue;

    /// <summary>
    /// Get the amount for the Request
    /// </summary>
    public decimal Amount {
      get {
        return _amount;
      }
    }

    /// <summary>
    /// Create a new &lt;charge-order&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public ChargeOrderRequest(string MerchantID, string MerchantKey, 
      string Env, string GoogleOrderNumber) 
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;charge-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public ChargeOrderRequest(string GoogleOrderNumber)
      : base(GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;charge-order&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Currency">The Currency used to charge the order</param>
    /// <param name="Amount">The Amount to charge</param>
    public ChargeOrderRequest(string MerchantID, string MerchantKey, 
      string Env, string GoogleOrderNumber, string Currency, decimal Amount)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
      _currency = Currency;
      _amount = Math.Round(Amount, 2); //fix for sending in fractional cents
    }

    /// <summary>
    /// Create a new &lt;charge-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Currency">The Currency used to charge the order</param>
    /// <param name="Amount">The Amount to charge</param>
    public ChargeOrderRequest(string GoogleOrderNumber, string Currency, 
      decimal Amount) : base(GoogleOrderNumber) {
      _currency = Currency;
      _amount = Math.Round(Amount, 2); //fix for sending in fractional cents
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      
      AutoGen.ChargeOrderRequest retVal = new AutoGen.ChargeOrderRequest();
      retVal.googleordernumber = GoogleOrderNumber;

      if (_amount > decimal.MinValue && _currency != null) {
        retVal.amount = new AutoGen.Money();
        retVal.amount.currency = _currency;
        retVal.amount.Value = _amount;
      }
      
      return EncodeHelper.Serialize(retVal);
    }

  }
}
