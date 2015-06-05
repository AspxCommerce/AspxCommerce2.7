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
 *  Fix issue 67 and cleaned up the default currency and amount defaults
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// The &lt;charge-and-ship-order&gt; tag identifies an order for which
  /// Google should charge the customer and mark the item(s) in the 
  /// order as shipped. The &lt;charge-and-ship-order&gt; tag may specify 
  /// whether the buyer should be charged the full amount or a 
  /// partial amount. In addition, it may include tracking data 
  /// for shipments. The order will be charged synchronously, and 
  /// a charge-amount-notification is returned in the response.
  /// </summary>
  public class ChargeAndShipOrderRequest : OrderProcessingBase {

    private decimal _amount = decimal.MinValue;
    private string _currency = GCheckoutConfigurationHelper.Currency;

    /// <summary>
    /// Get the amount for the Request
    /// </summary>
    public decimal Amount {
      get {
        return _amount;
      }
      set {
        _amount = Math.Round(value, 2); //fix for sending in fractional cents
      }
    }

    /// <summary>
    ///The &lt;item-shipping-information-list&gt; tag contains a list of 
    ///&lt;item-shipping-information&gt; elements, each of which identifies 
    ///an item in an order and may also provide shipment tracking 
    ///information for that item.
    /// </summary>
    public ItemShippingInformation ItemShippingInformation {
      get;
      private set;
    }

    /// <summary>
    /// Send Email to the Customer?
    /// </summary>
    public bool SendEmail {
      get;
      set;
    }

    /// <summary>
    /// The &lt;tracking-data-list&gt; tag contains a list of 
    /// &lt;tracking-data&gt; elements, each of which contains 
    /// shipment tracking information for an item.
    /// </summary>
    public TrackingDataList TrackingDataList {
      get;
      private set;
    }

    /// <summary>
    /// Create a new &lt;charge-and-ship-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    public ChargeAndShipOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      ItemShippingInformation = new ItemShippingInformation();
      TrackingDataList = new TrackingDataList();
    }

    /// <summary>
    /// Create a new &lt;charge-and-ship-order&gt; API request message
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    /// <param name="currency">The Currency used to charge the order</param>
    /// <param name="amount">The Amount to charge</param>
    public ChargeAndShipOrderRequest(string merchantID, string merchantKey,
      string env, string googleOrderNumber, string currency, decimal amount)
      : base(merchantID, merchantKey, env, googleOrderNumber) {
      ItemShippingInformation = new ItemShippingInformation();
      TrackingDataList = new TrackingDataList();
      _currency = currency;
      this.Amount = amount;
    }

    /// <summary>
    /// Create a new &lt;charge-and-ship-order&gt; API request message
    /// using the configuration settings
    /// </summary>
    /// <param name="googleOrderNumber">The Google Order Number</param>
    public ChargeAndShipOrderRequest(string googleOrderNumber)
      : base(googleOrderNumber) {
      ItemShippingInformation = new ItemShippingInformation();
      TrackingDataList = new TrackingDataList();
    }

    /// <summary>
    /// Get the Xml Needed to make the request.
    /// </summary>
    /// <returns></returns>
    public override byte[] GetXml() {

      AutoGen.ChargeAndShipOrderRequest retVal = new AutoGen.ChargeAndShipOrderRequest();
      retVal.googleordernumber = GoogleOrderNumber;

      if (Amount > decimal.MinValue && _currency != null) {
        retVal.amount = new AutoGen.Money();
        retVal.amount.currency = _currency;
        retVal.amount.Value = Amount;
      }
      retVal.sendemail = SendEmail;
      retVal.sendemailSpecified = true;

      //Both of these items can't be set at the same time. 
      //TODO do we validate that one of the two items are set?
      var shippingInfo = ItemShippingInformation.ToArray();
      if (shippingInfo != null && shippingInfo.Length > 0) {
        retVal.itemshippinginformationlist = shippingInfo;
      }

      var trackingData = TrackingDataList.ToArray();
      if (trackingData != null && trackingData.Length > 0) {
        retVal.trackingdatalist = trackingData;
      }

      return EncodeHelper.Serialize(retVal);
    }
  }

  /// <summary>
  /// Item Shipping Information used for ChargeAndShipOrderRequest
  /// </summary>
  public class ItemShippingInformation {

    //The list of boxes for the order.
    private Dictionary<string, ShipItemBox> _boxes
      = new Dictionary<string, ShipItemBox>(StringComparer.OrdinalIgnoreCase);

    private Dictionary<string, AutoGen.ItemShippingInformation>
      _items = new Dictionary<string, AutoGen.ItemShippingInformation>(
        StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Add a new box to the order that items will be placed in.
    /// </summary>
    /// <param name="carrier">
    /// The &lt;carrier&gt; tag contains the name of the company 
    /// responsible for shipping the item. Valid values for this 
    /// tag are DHL, FedEx, UPS, USPS and Other.
    /// </param>
    /// <param name="trackingNumber">
    /// The &lt;tracking-number&gt; tag contains the shipper's tracking
    /// number that is associated with an order.
    /// </param>
    /// <returns></returns>
    public ShipItemBox AddBox(string carrier, string trackingNumber) {
      if (_boxes.ContainsKey(trackingNumber))
        throw new ApplicationException(
          "You attempted to add a duplicate tracking number.");

      ShipItemBox retVal = ShipItemBox.CreateBox(carrier, trackingNumber, _items);
      _boxes.Add(trackingNumber, retVal);

      return retVal;
    }

    /// <summary>
    /// Add a new item based on a MerchantItemID
    /// </summary>
    /// <param name="carrier">
    /// The &lt;carrier&gt; tag contains the name of the company 
    /// responsible for shipping the item. Valid values for this 
    /// tag are DHL, FedEx, UPS, USPS and Other.
    /// </param>
    /// <param name="trackingNumber">
    /// The &lt;tracking-number&gt; tag contains the shipper's tracking
    /// number that is associated with an order.
    /// </param>
    /// <param name="merchantItemID">
    /// The &lt;merchant-item-id&gt; tag contains a value,
    /// such as a stock keeping unit (SKU), 
    /// that you use to uniquely identify an item.
    /// </param>
    public void AddMerchantItemId(string carrier, string trackingNumber,
      string merchantItemID) {

      ShipItemBox box = null;

      if (!_boxes.TryGetValue(trackingNumber, out box)) {
        box = ShipItemBox.CreateBox(carrier, trackingNumber, _items);
        _boxes[trackingNumber] = box;
      }

      box.AddMerchantItemID(merchantItemID);
    }

    /// <summary>
    /// Return the List of
    /// <see cref="GCheckout.AutoGen.ItemShippingInformation"/>[]
    /// </summary>
    public AutoGen.ItemShippingInformation[] ToArray() {
      AutoGen.ItemShippingInformation[] retVal
        = new AutoGen.ItemShippingInformation[_items.Count];
      _items.Values.CopyTo(retVal, 0);
      return retVal;
    }

  }

  /// <summary>
  /// The TrackingDataList Class
  /// </summary>
  public class TrackingDataList {

    List<AutoGen.TrackingData> _tracking
      = new List<AutoGen.TrackingData>();

    /// <summary>
    /// Get the array of elements.
    /// </summary>
    /// <returns></returns>
    public AutoGen.TrackingData[] ToArray() {
      return _tracking.ToArray();
    }

    /// <summary>
    /// Add a Tracking number to the request
    /// </summary>
    /// <param name="carrier">The Carrier</param>
    /// <param name="trackingNumber">The Tracking Number</param>
    public void AddTrackingData(string carrier, string trackingNumber) {
      if (string.IsNullOrEmpty(carrier)) {
        throw new ArgumentNullException("carrier");
      }
      if (string.IsNullOrEmpty(trackingNumber)) {
        throw new ArgumentNullException("trackingNumber");
      }

      _tracking.Add(new AutoGen.TrackingData() {
        carrier = carrier,
        trackingnumber = trackingNumber
      });
    }
  }

}
