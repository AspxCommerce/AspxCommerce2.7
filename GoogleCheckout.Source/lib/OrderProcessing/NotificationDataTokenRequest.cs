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
using GCheckout.Checkout;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// TODO
  /// </summary>
  public class NotificationDataTokenRequest : ReportRequestBase {

    private DateTime _beginDate;

    /// <summary>
    /// Create a new instance of the NotificationDataTokenRequest
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="beginDate">The Begin Date for the Notifications.</param>
    public NotificationDataTokenRequest(string merchantID,
      string merchantKey, string environment, DateTime beginDate)
      : base(merchantID, merchantKey, environment) {
      _beginDate = beginDate;
    }

    /// <summary>
    /// Parse the response for the Response Token
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected override GCheckoutResponse ParseResponse(string response) {
      return new NotificationDataTokenResponse(response);
    }

    /// <summary>
    /// Return the Xml data needed to be posted to the server.
    /// </summary>
    /// <returns></returns>
    public override byte[] GetXml() {
      AutoGen.NotificationDataTokenRequest retVal
      = new GCheckout.AutoGen.NotificationDataTokenRequest();
      retVal.starttime = _beginDate;
      return EncodeHelper.Serialize(retVal);
    }
  }
}
