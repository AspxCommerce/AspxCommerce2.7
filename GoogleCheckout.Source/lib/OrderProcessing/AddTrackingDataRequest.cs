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
using GCheckout.AutoGen;

namespace GCheckout.OrderProcessing {
  /// <summary>
  /// This class contains methods that construct &lt;add-tracking-data&gt; 
  /// API requests.
  /// </summary>
  public class AddTrackingDataRequest : OrderProcessingBase {

    private string _Carrier;
    private string _TrackingNo;

    /// <summary>
    /// The Carrier the package was shipped with
    /// </summary>
    public string Carrier {
      get {
        return _Carrier;
      }
    }

    /// <summary>
    /// The Tracking number for the carrier
    /// </summary>
    public string TrackingNo {
      get {
        return _TrackingNo;
      }
    }

    /// <summary>
    /// Create a new &lt;add-tracking-data&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Carrier">The Carrier the package was shipped with</param>
    /// <param name="TrackingNo">The Tracking number for the carrier</param>
    public AddTrackingDataRequest(string MerchantID, string MerchantKey, 
      string Env, string GoogleOrderNumber,
      string Carrier, string TrackingNo)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
      _Carrier = Carrier;
      _TrackingNo = TrackingNo;
    }

    /// <summary>
    /// Create a new &lt;add-tracking-data&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    /// <param name="Carrier">The Carrier the package was shipped with</param>
    /// <param name="TrackingNo">The Tracking number for the carrier</param>
    public AddTrackingDataRequest(string GoogleOrderNumber,
      string Carrier, string TrackingNo) : base(GoogleOrderNumber) {
      _Carrier = Carrier;
      _TrackingNo = TrackingNo;
    }

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {
      AutoGen.AddTrackingDataRequest Req = 
        new AutoGen.AddTrackingDataRequest();
      Req.googleordernumber = GoogleOrderNumber;
      Req.trackingdata = new AutoGen.TrackingData();
      Req.trackingdata.carrier = _Carrier;
      Req.trackingdata.trackingnumber = _TrackingNo;
      return EncodeHelper.Serialize(Req);
    }

  }
}
