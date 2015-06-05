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
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
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
  /// NotificationDataResponse
  /// </summary>
  public class NotificationDataResponse : GCheckoutResponse {
    private AutoGen.NotificationDataResponse _response;

     /// <summary>
    /// Create a new Instance of the Data Token Response
    /// </summary>
    /// <param name="responseXml"></param>
    public NotificationDataResponse(string responseXml)
      : base(responseXml) {

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
        return (base.IsGood || _response != null);
      }
    }

    /// <summary>
    /// Parse the Message for a notification data token response message.
    /// </summary>
    /// <returns></returns>
    protected override bool ParseMessage() {
      try {
        if (ResponseXml.IndexOf("<notification-data-response") > -1) {
          _response = (AutoGen.NotificationDataResponse)
            EncodeHelper.Deserialize(ResponseXml,
            typeof(AutoGen.NotificationDataResponse));
          Log.Xml(_response.serialnumber , ResponseXml);
          return true;
        }
      }
      catch (Exception ex) {
        Log.Err("NotificationDataResponse ParseResponse:" + ex.Message);
      }

      return false;
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

    /// <summary>
    /// Gets the serial number. Google attaches a unique serial number to
    /// every response.
    /// </summary>
    /// <value>
    /// The serial number, for example 58ea39d3-025b-4d52-a697-418f0be74bf9.
    /// </value>
    public override string SerialNumber {
      get {
        if (_response != null) {
          return _response.serialnumber;
        }

        string retVal = base.SerialNumber;
        if (!string.IsNullOrEmpty(retVal))
          return retVal;

        throw new ApplicationException("We were not able to parse the message.");
      }
    }

  }
}
