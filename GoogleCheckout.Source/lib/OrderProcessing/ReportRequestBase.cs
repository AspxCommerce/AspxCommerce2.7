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
 *  Initial Release for all ReportRequests. This is the base class for all report requests.
 * 
*/
using System;
using GCheckout;
using GCheckout.Util;
using GCheckout.Checkout;

namespace GCheckout.OrderProcessing
{
	/// <summary>
	/// Summary description for ReportRequestBase.
	/// </summary>
	public abstract class ReportRequestBase : GCheckoutRequest
	{

    /// <summary>
    /// The base constructor to create a report request
    /// </summary>
    /// <param name="merchantID">The merchant id</param>
    /// <param name="merchantKey">The merchant key</param>
    /// <param name="environment">The Environment</param>
    public ReportRequestBase(string merchantID, 
      string merchantKey, string environment) {
      _MerchantID = merchantID;
      _MerchantKey = merchantKey;
      _Environment = StringToEnvironment(environment);
    }

    /// <summary>
    /// The base constructor to create a report request
    /// </summary>
    /// <param name="merchantID">The merchant id</param>
    /// <param name="merchantKey">The merchant key</param>
    /// <param name="environment">The Environment</param>
    public ReportRequestBase(long merchantID,
      string merchantKey, EnvironmentType environment) {
      _MerchantID = merchantID.ToString();
      _MerchantKey = merchantKey;
      _Environment = environment;
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override string GetPostUrl() {
      if (Environment == EnvironmentType.Sandbox)
        return "https://sandbox.google.com/checkout/api/checkout/v2/reports/Merchant/" 
          + MerchantID;
      else
        return "https://checkout.google.com/api/checkout/v2/reports/Merchant/" 
          + MerchantID;
    }

    /// <summary>
    /// Process the response.
    /// </summary>
    /// <param name="response">The Xml Response from the Request.</param>
    /// <returns>A class that implements the IGCheckoutResponse interface.</returns>
    protected override GCheckoutResponse ParseResponse(string response) {
      return new GCheckoutResponse(response);
    }
  }
}
