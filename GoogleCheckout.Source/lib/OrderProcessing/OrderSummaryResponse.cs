/*************************************************
 * Copyright (C) 2010 Google Inc.
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
 *  5-22-2010   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added Xml logging
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// The OrderSummaryResponse
  /// </summary>
  public class OrderSummaryResponse : GCheckoutResponse {

    private AutoGen.OrderSummaryResponse _response;

    /// <summary>
    /// Gets a value indicating whether Google returned an error code or not.
    /// </summary>
    /// <value>
    /// <c>true</c> if the response did not indicate an error; 
    /// otherwise, <c>false</c>.
    /// </value>
    public override bool IsGood {
      get {
        return (base.IsGood || _response != null);
      }
    }

    /// <summary>
    /// The Response that was returned from the server.
    /// </summary>
    public override object Response {
      get {
        if (_response != null)
          return _response;
        return base.Response;
      }
    }

    private List<AutoGen.OrderSummary> _orderSummary;

    /// <summary>
    /// The order summary returned from the request
    /// </summary>
    public List<AutoGen.OrderSummary> OrderSummary {
      get {
        if (_orderSummary == null) {
          _orderSummary = new List<GCheckout.AutoGen.OrderSummary>();
          if (_response != null && _response.ordersummaries != null) {
            _orderSummary.AddRange(_response.ordersummaries);
          }
        }
        return _orderSummary;
      }
    }

    /// <summary>
    /// Parse the Xml for the correct message type.
    /// </summary>
    /// <param name="responseXml">The Xml to parse</param>
    public OrderSummaryResponse(string responseXml)
      : base(responseXml) {

    }

    /// <summary>
    /// Parse the Message for a notification data token response message.
    /// </summary>
    /// <returns></returns>
    protected override bool ParseMessage() {
      try {
        if (ResponseXml.IndexOf("<order-summary-response") > -1) {
          _response = (AutoGen.OrderSummaryResponse)
            EncodeHelper.Deserialize(ResponseXml,
            typeof(AutoGen.OrderSummaryResponse));
          Log.Xml(_response.serialnumber, ResponseXml);
          return true;
        }
      }
      catch (Exception ex) {
        Log.Err("OrderSummaryResponse ParseResponse:" + ex.Message);
      }

      return false;
    }
  }
}
