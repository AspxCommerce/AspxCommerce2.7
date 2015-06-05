/*************************************************
 * Copyright (C) 2008 Google Inc.
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
 *  Initial Release.
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Checkout;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// Create a new Hello Request to ping the Server.
  /// </summary>
  public class HelloRequest : GCheckoutRequest {

    /// <summary>
    /// Create a new &lt;hello&gt; request.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    public HelloRequest(string merchantID, string merchantKey,
       string environment) {
      _MerchantID = merchantID;
      _MerchantKey = merchantKey;
      _Environment = StringToEnvironment(environment);
    }

    /// <summary>
    /// Create a new &lt;hello&gt; request.
    /// </summary>
    public HelloRequest() {
      _MerchantID = GCheckoutConfigurationHelper.MerchantID.ToString();
      _MerchantKey = GCheckoutConfigurationHelper.MerchantKey;
      _Environment = GCheckoutConfigurationHelper.Environment;
    }

    /// <summary>
    /// Get the Xml needed to create the request.
    /// </summary>
    /// <returns></returns>
    public override byte[] GetXml() {
      AutoGen.Hello retVal = new GCheckout.AutoGen.Hello();
      return EncodeHelper.Serialize(retVal);
    }

    /// <summary>
    /// Parse an unknown response.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected override GCheckoutResponse ParseResponse(string response) {
      return new HelloCheckoutResponse(response);
    }
  }
}
