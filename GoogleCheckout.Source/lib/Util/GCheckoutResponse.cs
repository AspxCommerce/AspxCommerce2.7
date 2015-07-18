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
 *  Cleaned up all of the classes to pass the responses through this class.
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Checkout;
using GCheckout.AutoGen;

namespace GCheckout.Util {
  //GCheckoutResponse
  /// <summary>
  /// Generic Response Parser
  /// </summary>
  public class GCheckoutResponse {

    private AutoGen.RequestReceivedResponse _GoodResponse;
    private AutoGen.ErrorResponse _ErrorResponse;
    private string _responseXml;
    //private bool _parsed = false;

    /// <summary>
    /// The Good Response was received
    /// </summary>
    protected AutoGen.RequestReceivedResponse GoodResponse {
      get {
        return _GoodResponse;
      }
      set {
        _GoodResponse = value;
      }
    }

    /// <summary>
    /// An Error Response was received
    /// </summary>
    protected AutoGen.ErrorResponse ErrorResponse {
      get {
        return _ErrorResponse;
      }
      set {
        _ErrorResponse = value;
      }
    }

    #region IGCheckoutResponse Members

    /// <summary>
    /// If Google responded with an error (IsGood = false) then this 
    /// property will contain the human-readable error message.
    /// </summary>
    /// <value>
    /// The error message returned by Google, or an empty string if
    /// there was no error.
    /// </value>
    public virtual string ErrorMessage {
      get {
        if (_ErrorResponse != null) {
          return _ErrorResponse.errormessage;
        }
        else {
          return string.Empty;
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether Google returned an error code or not.
    /// </summary>
    /// <value>
    /// <c>true</c> if the response did not indicate an error; 
    /// otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsGood {
      get {
        return (_GoodResponse != null);
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
    public virtual string RedirectUrl {
      get {
        return string.Empty;
      }
    }

    /// <summary>
    /// The Response that was returned from the server.
    /// </summary>
    public virtual object Response {
      get {
        if (_GoodResponse != null)
          return _GoodResponse;
        if (_ErrorResponse != null)
          return _ErrorResponse;
        return null;
      }
    }

    /// <summary>
    /// The Response Xml
    /// </summary>
    public virtual string ResponseXml {
      get {
        return _responseXml;
      }
    }

    /// <summary>
    /// Gets the serial number. Google attaches a unique serial number to
    /// every response.
    /// </summary>
    /// <value>
    /// The serial number, for example 58ea39d3-025b-4d52-a697-418f0be74bf9.
    /// </value>
    public virtual string SerialNumber {
      get {

        IGCheckoutResponse response = Response as IGCheckoutResponse;

        if (response != null) {
          return response.MessageSerialNumber;
        }
        if (_GoodResponse != null) {
          return _GoodResponse.serialnumber;
        }
        if (_ErrorResponse != null) {
          return _ErrorResponse.serialnumber;
        }
        return string.Empty;
      }
    }

    #endregion

    /// <summary>
    /// Create a new instance of the Response parser
    /// </summary>
    /// <param name="responseXml"></param>
    public GCheckoutResponse(string responseXml) {
      //to cut down on the number or exceptions, we are going to try to
      //predetermine the type of message being returned this will allow for
      //a greater experience on the dev side. If you are not using debug
      //symbols, it is very difficult to determine if the error is real
      //or not if you are breaking on all thrown exceptions.

      _responseXml = responseXml;

      if (ResponseXml.IndexOf("<request-received") > -1) {
        _GoodResponse = (AutoGen.RequestReceivedResponse)
          EncodeHelper.Deserialize(ResponseXml,
          typeof(AutoGen.RequestReceivedResponse));
        Log.Xml(_GoodResponse.serialnumber, ResponseXml);
        //_parsed = true;
      }
      else if (ResponseXml.IndexOf("<error") > -1) {
        _ErrorResponse = (AutoGen.ErrorResponse)
          EncodeHelper.Deserialize(ResponseXml,
          typeof(AutoGen.ErrorResponse));
        Log.Xml(_ErrorResponse.serialnumber, ResponseXml);
        //_parsed = true;
      }
      else if (ParseMessage()) {
        //_parsed = true;
      }
      else {
        //What we want to do is determine if there is a type so we
        //can report it to the user so they can report the error
        string messageTypeFound = string.Empty;
        try {
          var theType = EncodeHelper.Deserialize(ResponseXml);
          if (theType != null) {
            messageTypeFound = theType.GetType().Name;
          }
          else {
            messageTypeFound = "Unknown Message Type";
          }
          Log.Xml(Guid.NewGuid().ToString(), ResponseXml);
        }
        catch (Exception ex) {
          Log.Xml(Guid.NewGuid().ToString(), ResponseXml);
          Log.Err("GCheckoutResponse else check failed:" + ex.Message);
        }
        
        _ErrorResponse = new GCheckout.AutoGen.ErrorResponse();
        _ErrorResponse.errormessage 
          = string.Format("Couldn't parse ResponseXml. " 
          + "Message type found was {0}. Please See ResponseXml",
          messageTypeFound);
      }

    }

    /// <summary>
    /// If the base is not able to parse then it is the job of the class to parse the message.
    /// </summary>
    /// <returns></returns>
    protected virtual bool ParseMessage() {
      return false;
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
      "SerialNo: '{1}', ErrorMsg: '{2}']",
        IsGood, SerialNumber, ErrorMessage);
    }
  }
}
