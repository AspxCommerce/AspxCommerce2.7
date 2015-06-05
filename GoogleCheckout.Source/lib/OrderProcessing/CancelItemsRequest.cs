/*************************************************
 * Copyright (C) 2007 Google Inc.
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
using System.Collections;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {
  /// <summary>
  /// The &lt;reset-items-shipping-information&gt; command lets you remove
  /// the shipping status associated with one or more items and return those
  /// items to the Not yet shipped state
  /// </summary>
  public class CancelItemsRequest 
    : BackOrderCancelReturnAndResetItemBase {

    private string _comment;
    private string _reason;

    /// <summary>
    /// Create a new &lt;reset-items-shipping-information&gt;
    /// API request message using the Configuration Settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public CancelItemsRequest(string GoogleOrderNumber)
      : base(GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;reset-items-shipping-information&gt;
    /// API request message using the Configuration Settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="sendEmail">
    /// The &lt;send-email&gt; tag indicates whether Google Checkout 
    /// should email the buyer 
    /// </param>
    public CancelItemsRequest(string GoogleOrderNumber,
      bool sendEmail)
      : base(GoogleOrderNumber) {
      SendEmail = sendEmail;
    }

    /// <summary>
    /// Create a new &lt;reset-items-shipping-information&gt;
    /// API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public CancelItemsRequest(string merchantID, 
      string merchantKey, string env, string GoogleOrderNumber) 
      : base(merchantID, merchantKey, env, GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;reset-items-shipping-information&gt;
    /// API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="sendEmail">
    /// The &lt;send-email&gt; tag indicates whether Google Checkout 
    /// should email the buyer 
    /// </param>
    public CancelItemsRequest(string merchantID, 
      string merchantKey, string env, string GoogleOrderNumber,
      bool sendEmail) 
      : base(merchantID, merchantKey, env, GoogleOrderNumber) {
      SendEmail = SendEmail;
    }

    /// <summary>
    /// Create a new &lt;reset-items-shipping-information&gt;
    /// API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="comment">A Comment for the cancel request</param>
    /// <param name="reason">The reason for the cancel request</param>
    public CancelItemsRequest(string merchantID, 
      string merchantKey, string env, string GoogleOrderNumber,
      string comment, string reason) 
      : base(merchantID, merchantKey, env, GoogleOrderNumber) {
      _comment = comment;
      _reason = reason;
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.CancelItemsRequest retVal
        = new GCheckout.AutoGen.CancelItemsRequest();
      retVal.googleordernumber = GoogleOrderNumber;
      if (_sendEmailSpecified) {
        retVal.sendemail = SendEmail;
        retVal.sendemailSpecified = true;
      }

      if (_comment != null && _comment.Length > 0)
        retVal.comment = _comment;

      if (_reason != null && _reason.Length > 0)
        retVal.reason = _reason;

      retVal.itemids = Items;

      return EncodeHelper.Serialize(retVal);
    }

  }
}
