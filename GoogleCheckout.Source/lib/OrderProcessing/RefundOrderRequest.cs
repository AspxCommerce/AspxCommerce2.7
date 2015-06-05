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
*/
using System;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {
  
  /// <summary>
  /// This class contains methods that construct &lt;refund-order&gt; API 
  /// requests.
  /// </summary>
  public class RefundOrderRequest : OrderProcessingBase {
    private string _reason;
    private string _currency = GCheckoutConfigurationHelper.Currency;
    private decimal _amount = decimal.MinValue;
    private string _comment;

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    public RefundOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber, string reason)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      _reason = reason;
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    public RefundOrderRequest(string googleOrderNumber, string reason)
      : base(googleOrderNumber) {
      _reason = reason;
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="currency">The Currency used to charge the order</param>
    /// <param name="Amount">The Amount to charge</param>
    public RefundOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber, string reason, string currency,
      decimal Amount)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      _reason = reason;
      _currency = currency;
      _amount = Math.Round(Amount, 2); //fix for sending in fractional cents
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="currency">The Currency used to charge the order</param>
    /// <param name="amount">The Amount to charge</param>
    public RefundOrderRequest(string googleOrderNumber, string reason,
      string currency, decimal amount) : base(googleOrderNumber) {
      _reason = reason;
      _currency = currency;
      _amount = Math.Round(amount, 2); //fix for sending in fractional cents
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="comment">A comment to append to the order</param>
    public RefundOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber, string reason, string comment)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      _reason = reason;
      _comment = comment;
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="comment">A comment to append to the order</param>
    public RefundOrderRequest(string googleOrderNumber, 
      string reason, string comment) : base(googleOrderNumber) {
      _reason = reason;
      _comment = comment;
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="currency">The Currency used to charge the order</param>
    /// <param name="amount">The Amount to charge</param>
    /// <param name="comment">A comment to append to the order</param>
    public RefundOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber, string reason, string currency,
      decimal amount, string comment)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      _reason = reason;
      _currency = currency;
      _amount = Math.Round(amount, 2); //fix for sending in fractional cents
      _comment = comment;
    }

    /// <summary>
    /// Create a new &lt;refund-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="reason">The Reason for the refund</param>
    /// <param name="currency">The Currency used to charge the order</param>
    /// <param name="amount">The Amount to charge</param>
    /// <param name="comment">A comment to append to the order</param>
    public RefundOrderRequest(string googleOrderNumber, string reason, 
      string currency, decimal amount, string comment)
      : base(googleOrderNumber) {
      _reason = reason;
      _currency = currency;
      _amount = Math.Round(amount, 2); //fix for sending in fractional cents
      _comment = comment;
    }

    /// <summary>Method that is called to produce the Xml message
    ///  that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.RefundOrderRequest retVal = new AutoGen.RefundOrderRequest();
      retVal.googleordernumber = GoogleOrderNumber;
      retVal.reason = EncodeHelper.EscapeXmlChars(_reason);
      if (_amount > decimal.MinValue && _currency != null) {
        retVal.amount = new AutoGen.Money();
        retVal.amount.currency = _currency;
        retVal.amount.Value = _amount; //already checked.
      }
      if (_comment != null) {
        retVal.comment = _comment;
      }
      return EncodeHelper.Serialize(retVal);
    }
  }
}
