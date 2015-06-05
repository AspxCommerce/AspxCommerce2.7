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
 *  August 2008   Joe Feser joe.feser@joefeser.com
 *  Added ParseResponse to allow it to be part of the new message api.
 * 
*/
using System;
using GCheckout.Util;
using GCheckout.Checkout;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// The Base Class for all Order Processing API messages.
  /// </summary>
  public abstract class OrderProcessingBase : GCheckoutRequest {

    private string _googleOrderNumber = null;

    /// <summary>
    /// The Google Order Number
    /// </summary>
    public string GoogleOrderNumber {
      get {
        return _googleOrderNumber;
      }
    }

    /// <summary>
    /// Base ctor to initialize the messages
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    public OrderProcessingBase(string merchantID, 
      string merchantKey, string env, string googleOrderNumber) {
      _MerchantID = merchantID;
      _MerchantKey = merchantKey;
      _Environment = StringToEnvironment(env);
      _googleOrderNumber = googleOrderNumber;
    }

    /// <summary>
    /// Base ctor to initialize the messages
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    public OrderProcessingBase(string googleOrderNumber) {
      _MerchantID = GCheckoutConfigurationHelper.MerchantID.ToString();
      _MerchantKey = GCheckoutConfigurationHelper.MerchantKey;
      _Environment = GCheckoutConfigurationHelper.Environment;
      _googleOrderNumber = googleOrderNumber;
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
