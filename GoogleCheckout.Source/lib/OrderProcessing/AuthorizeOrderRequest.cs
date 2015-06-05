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
using GCheckout.Util;
namespace GCheckout.OrderProcessing {

  /// <summary>
  /// The &lt;authorize-order&gt; command instructs Google Checkout to explicitly 
  /// reauthorize a customer's credit card for the uncharged balance of an 
  /// order to verify that funds for the order are available.
  /// </summary>
  public class AuthorizeOrderRequest: OrderProcessingBase {

    /// <summary>
    /// Create a new &lt;authorize-order&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public AuthorizeOrderRequest(string MerchantID, 
      string MerchantKey, string Env, string GoogleOrderNumber)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;authorize-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public AuthorizeOrderRequest(string GoogleOrderNumber)
      : base(GoogleOrderNumber) {
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.AuthorizeOrderRequest Req = new AutoGen.AuthorizeOrderRequest();
      Req.googleordernumber = GoogleOrderNumber;
      return EncodeHelper.Serialize(Req);
    }
  }
}
