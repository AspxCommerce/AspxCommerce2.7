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
 *  Initial Release.
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added Xml logging
 * 
*/
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using GCheckout.Util;
using GCheckout.Checkout;

namespace GCheckout.Util {

  /// <summary>
  /// This request contains methods that handle a &lt;checkout-redirect&gt; 
  /// response from Google Checkout and capture the URL to which a customer 
  /// should be redirected to complete the checkout process.
  /// </summary>
  public class GCheckoutShoppingCartResponse : GCheckoutResponse {
    AutoGen.CheckoutRedirect _CheckoutRedirectResponse;

    /// <summary>
    /// Creates a new instance of the <see cref="GCheckoutShoppingCartResponse"/> class.
    /// </summary>
    /// <param name="ResponseXml">The XML returned from Google.</param>
    public GCheckoutShoppingCartResponse(string ResponseXml)
      : base(ResponseXml) {

    }

    /// <summary>
    /// Parse the message for a checkout-redirect
    /// </summary>
    /// <returns></returns>
    protected override bool ParseMessage() {
      //to cut down on the number or exceptions, we are going to try to
      //predetermine the type of message being returned this will allow for
      //a greater experience on the dev side. If you are not using debug
      //symbols, it is very difficult to determine if the error is real
      //or not if you are breaking on all thrown exceptions.

      try {
        if (ResponseXml.IndexOf("<checkout-redirect") > -1) {
          _CheckoutRedirectResponse = (AutoGen.CheckoutRedirect)
            EncodeHelper.Deserialize(ResponseXml,
            typeof(AutoGen.CheckoutRedirect));
          Log.Xml(_CheckoutRedirectResponse.serialnumber , ResponseXml);
          return true;
        }
      }
      catch (Exception ex) {
        Log.Err("GCheckoutShoppingCartResponse ParseResponse:" + ex.Message);
      }

      return false;

    }

    /// <summary>
    /// The Response that was returned from the server.
    /// </summary>
    public override object Response {
      get {
        if (_CheckoutRedirectResponse != null)
          return _CheckoutRedirectResponse;
        return base.Response;
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current 
    /// GCheckoutResponse. Intended for debugging only.
    /// </summary>
    /// <returns>
    /// A human-readable <see cref="T:System.String"/> that represents the 
    /// current GCheckoutResponse. Note that the format of the string may 
    /// change in future releases.
    /// </returns>
    public override string ToString() {
      return string.Format("[GCheckoutResponse -- IsGood: '{0}', " +
      "SerialNo: '{1}', ErrorMsg: '{2}', RedirectUrl: '{3}']",
        IsGood, SerialNumber, ErrorMessage, RedirectUrl);
    }

    /// <summary>
    /// Gets a value indicating whether Google returned an error code or not.
    /// </summary>
    /// <value>
    /// <c>true</c> if the response did not indicate an error; 
    /// otherwise, <c>false</c>.
    /// </value>
    public override bool IsGood {
      get {
        return (base.IsGood || _CheckoutRedirectResponse != null);
      }
    }

    /// <summary>
    /// Gets the serial number. Google attaches a unique serial number to
    /// every response.
    /// </summary>
    /// <value>
    /// The serial number, for example 58ea39d3-025b-4d52-a697-418f0be74bf9.
    /// </value>
    public override string SerialNumber {
      get {
        if (_CheckoutRedirectResponse != null) {
          return _CheckoutRedirectResponse.serialnumber;
        }

        string retVal = base.SerialNumber;
        if (!string.IsNullOrEmpty(retVal))
          return retVal;

        throw new ApplicationException("All three responses are null; " +
          "something's wrong!");
      }
    }

    /// <summary>
    /// If Google indicated a redirect URL in the response, this property
    /// will contain the URL string.
    /// </summary>
    /// <value>
    /// The redirect URL, or the empty string if Google didn't send a redirect
    /// URL.
    /// </value>
    public override string RedirectUrl {
      get {
        if (_CheckoutRedirectResponse != null) {
          return _CheckoutRedirectResponse.redirecturl.Replace("&amp;", "&");
        }
        else {
          return string.Empty;
        }
      }
    }
  }
}
