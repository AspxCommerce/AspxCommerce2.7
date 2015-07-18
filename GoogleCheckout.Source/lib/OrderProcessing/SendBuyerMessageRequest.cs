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

namespace GCheckout.OrderProcessing {
  /// <summary>
  /// This class contains methods that construct &lt;send-buyer-message&gt; 
  /// API requests.
  /// </summary>
  public class SendBuyerMessageRequest : OrderProcessingBase {

    private string _Message = null;
    private bool _SendEmail = true;

    /// <summary>
    /// The Message to send to the customer
    /// </summary>
    public string Message {
      get {
        return _Message;
      }
    }

    /// <summary>
    /// Send an Email
    /// </summary>
    public bool SendEmail {
      get {
        return _SendEmail;
      }
    }

    /// <summary>
    /// Create a new &lt;send-buyer-message&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Message">The Message to send to the buyer</param>
    public SendBuyerMessageRequest(string MerchantID, string MerchantKey,
      string Env, string GoogleOrderNumber, string Message)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
      _Message = Message;
    }

    /// <summary>
    /// Create a new &lt;send-buyer-message&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Message">The Message to send to the buyer</param>
    public SendBuyerMessageRequest(string GoogleOrderNumber, string Message)
      : base(GoogleOrderNumber) {
      _Message = Message;
    }

    /// <summary>
    /// Create a new &lt;send-buyer-message&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Message">The Message to send to the buyer</param>
    /// <param name="SendEmail">Send an email to the buyer</param>
    public SendBuyerMessageRequest(string MerchantID, string MerchantKey,
      string Env, string GoogleOrderNumber, string Message, bool SendEmail)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
      _Message = Message;
      _SendEmail = SendEmail;
    }

    /// <summary>
    /// Create a new &lt;send-buyer-message&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Message">The Message to send to the buyer</param>
    /// <param name="SendEmail">Send an email to the buyer</param>
    public SendBuyerMessageRequest(string GoogleOrderNumber, 
      string Message, bool SendEmail)
      : base(GoogleOrderNumber) {
      _Message = Message;
      _SendEmail = SendEmail;
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.SendBuyerMessageRequest Req =
        new AutoGen.SendBuyerMessageRequest();
      Req.googleordernumber = GoogleOrderNumber;
      Req.message = EncodeHelper.EscapeXmlChars(_Message);
      Req.sendemail = _SendEmail;
      Req.sendemailSpecified = true;
      return EncodeHelper.Serialize(Req);
    }

  }
}
