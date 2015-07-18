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
using GCheckout.Util;
using GCheckout.AutoGen;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// This class contains methods that construct 
  /// &lt;add-merchant-order-number&gt; API requests.
  /// </summary>
  public class AddMerchantOrderNumberRequest : OrderProcessingBase {

    private string _MerchantOrderNumber;

    /// <summary>
    /// Create a new &lt;add-merchant-order-number&gt; API requests  message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="MerchantOrderNumber">The Merchant Order Number</param>
    public AddMerchantOrderNumberRequest(string MerchantID, string MerchantKey,
      string Env, string GoogleOrderNumber, string MerchantOrderNumber)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
      _MerchantOrderNumber = MerchantOrderNumber;
    }

    /// <summary>
    /// Create a new &lt;add-merchant-order-number&gt; API requests  message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="MerchantOrderNumber">The Merchant Order Number</param>
    public AddMerchantOrderNumberRequest(string GoogleOrderNumber, 
      string MerchantOrderNumber) : base(GoogleOrderNumber) {
      _MerchantOrderNumber = MerchantOrderNumber;
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.AddMerchantOrderNumberRequest Req = 
        new AutoGen.AddMerchantOrderNumberRequest();
      Req.googleordernumber = GoogleOrderNumber;
      Req.merchantordernumber = _MerchantOrderNumber;
      return EncodeHelper.Serialize(Req);
    }

  }
}
