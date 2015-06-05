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
 *  Created as a base class for all responses from the server.
 *  
*/
using System;
using System.IO;
using System.Net;
using GCheckout.Util;
using GCheckout.Checkout;

namespace GCheckout {

  /// <summary>
  /// This class contains methods for sending API requests to Google Checkout.
  /// </summary>
  public abstract class GCheckoutRequest {
    /// <summary>Google Checkout Merchant ID</summary>
    protected string _MerchantID;
    /// <summary>Google Checkout Merchant Key</summary>
    protected string _MerchantKey;
    /// <summary>EnvironmentType used to determine where the messages are 
    /// posted (Sandbox, Production)</summary>
    protected EnvironmentType _Environment = EnvironmentType.Unknown;

    /// <summary>Method that is called to produce the Xml message that 
    /// can be posted to Google Checkout.</summary>
    public abstract byte[] GetXml();

    /// <summary>Convert a String like Sandbox to the 
    /// EnvironmentType enum</summary>
    protected static EnvironmentType StringToEnvironment(string Env) {
      return (EnvironmentType)Enum.Parse(typeof(EnvironmentType), Env, true);
    }

    /// <summary>
    /// This is designed to allow derived response classes to parse for non generic responses.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected abstract GCheckoutResponse ParseResponse(string response);

    /// <summary>Send the Message to Google Checkout</summary>
    public virtual GCheckoutResponse Send() {
      CheckSendPreConditions();
      var sendXml = GetXml();
      Log.Xml("send-" + Guid.NewGuid() + ".xml", EncodeHelper.Utf8BytesToString(sendXml));
      string responseXml = HttpHelper.SendMessage(sendXml, GetPostUrl(), MerchantID, MerchantKey);
      return ParseResponse(responseXml);
    }

    /// <summary></summary>
    public virtual string GetPostUrl() {
      if (_Environment == EnvironmentType.Sandbox) {
        return string.Format(
          "https://sandbox.google.com/checkout/api/checkout/v2/request/Merchant/{0}",
          _MerchantID);
      }
      else {
        return string.Format(
          "https://checkout.google.com/api/checkout/v2/request/Merchant/{0}",
          _MerchantID);
      }
    }

    /// <summary>
    /// Check the conditions to ensure we are able to send the message.
    /// </summary>
    protected void CheckSendPreConditions() {
      if (_Environment == EnvironmentType.Unknown) {
        throw new ApplicationException(
          "Environment has not been set to Sandbox or Production");
      }
      if (_MerchantID == null) {
        throw new ApplicationException("MerchantID has not been set");
      }
      if (_MerchantKey == null) {
        throw new ApplicationException("MerchantKey has not been set");
      }
    }

    /// <summary>Google Checkout Merchant ID</summary>
    public string MerchantID {
      get {
        return _MerchantID;
      }
      set {
        _MerchantID = value;
      }
    }

    /// <summary>Google Checkout Merchant Key</summary>
    public string MerchantKey {
      get {
        return _MerchantKey;
      }
      set {
        _MerchantKey = value;
      }
    }

    /// <summary>EnvironmentType used to determine where 
    /// the messages are posted (Sandbox, Production)</summary>
    public EnvironmentType Environment {
      get {
        return _Environment;
      }
      set {
        _Environment = value;
      }
    }
  }

  /// <summary>Determine where the messages are posted
  /// (Sandbox, Production)</summary>
  public enum EnvironmentType {
    /// <summary>Use the Sandbox account to post the messages</summary>
    Sandbox = 0,
    /// <summary>Use the Production account to post the messages</summary>
    Production = 1,
    /// <summary>Unknown account.</summary>
    Unknown = 2
  }
}
