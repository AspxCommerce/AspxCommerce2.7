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
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// TODO
  /// </summary>
  public class NotificationDataRequest : ReportRequestBase {

    string _token;

    /// <summary>
    /// The Token
    /// </summary>
    public string Token {
      get {
        return _token;
      }
    }
    
    /// <summary>
    /// Create a new Instance of NotificationDataRequest
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="token">The Token needed to obtain the messages</param>
    public NotificationDataRequest(string merchantID,
      string merchantKey, string environment, string token)
      : base(merchantID, merchantKey, environment) {
      _token = token;
    }

     /// <summary>
    /// Create a new Instance of NotificationDataRequest
    /// </summary>
    public NotificationDataRequest()
      : base(GCheckoutConfigurationHelper.MerchantID.ToString(),
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment.ToString()) {
    }

    /// <summary>
    /// Parse the Message for a NotificationDataResponse
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected override GCheckoutResponse ParseResponse(string response) {
      return new NotificationDataResponse(response);
    }

    /// <summary>
    /// Get the Xml for the message
    /// </summary>
    /// <returns></returns>
    public override byte[] GetXml() {
      AutoGen.NotificationDataRequest retVal
      = new GCheckout.AutoGen.NotificationDataRequest();
      retVal.continuetoken = _token;
      return EncodeHelper.Serialize(retVal);
    }
  }
}
