/*************************************************
 * Copyright (C) 2007 Google Inc.
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
using System.Collections;
using GCheckout.Util;
using System.Collections.Generic;

namespace GCheckout.OrderProcessing {
  /// <summary>
  /// the &lt;ship-items&gt; command lets you specify shipping information for
  /// one or more items in an order. When you send a &lt;ship-items&gt;
  /// request, the shipping information in that request will be appended to 
  /// the existing shipping records for the specified order.
  /// </summary>
  /// <remarks>
  /// A Box can only contain one Tracking number.
  /// Many Items can fit in a box and one item may
  /// by placed in multiple boxes.
  /// Because of this, There will be two different ways to code this message.
  /// The First way is to Add a Box to the shipment and then add items to it.
  /// The Second way is to just add items to the shipment passing in the
  /// tracking information.
  /// </remarks>
  public class ShipItemsRequest : OrderProcessingBase {
    private bool _sendEmail;
    private bool _sendEmailSpecified;
    //The list of boxes for the order.
    private Dictionary<string, ShipItemBox> _boxes
      = new Dictionary<string, ShipItemBox>(StringComparer.OrdinalIgnoreCase);

    private Dictionary<string, AutoGen.ItemShippingInformation>
      _items = new Dictionary<string, AutoGen.ItemShippingInformation>(
        StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// The &lt;send-email&gt; tag indicates whether Google Checkout 
    /// should email the buyer 
    /// </summary>
    public bool SendEmail {
      get {
        return _sendEmail;
      }
      set {
        _sendEmail = value;
        _sendEmailSpecified = true;
      }
    }

    /// <summary>
    /// Return the List of
    /// <see cref="GCheckout.AutoGen.ItemShippingInformation"/>[]
    /// </summary>
    public AutoGen.ItemShippingInformation[] ItemShippingInfo {
      get {
        AutoGen.ItemShippingInformation[] retVal
          = new AutoGen.ItemShippingInformation[_items.Count];
        _items.Values.CopyTo(retVal, 0);
        return retVal;
      }
    }

    /// <summary>
    /// Create a new &lt;ship-items&gt; API request message using the
    /// Configuration Settings
    /// </summary>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public ShipItemsRequest(string GoogleOrderNumber)
      : base(GoogleOrderNumber) {
    }

    /// <summary>
    /// Create a new &lt;ship-items&gt; API request message
    /// </summary>
    /// <param name="MerchantID">Google Checkout Merchant ID</param>
    /// <param name="MerchantKey">Google Checkout Merchant Key</param>
    /// <param name="Env">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="GoogleOrderNumber">The Google Order Number</param>
    public ShipItemsRequest(string MerchantID, string MerchantKey,
      string Env, string GoogleOrderNumber)
      : base(MerchantID, MerchantKey, Env, GoogleOrderNumber) {
    }

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

    /// <summary>Method that is called to produce the Xml message 
    /// that can be posted to Google Checkout.</summary>
    public override byte[] GetXml() {

      AutoGen.ShipItemsRequest retVal = new AutoGen.ShipItemsRequest();
      retVal.googleordernumber = GoogleOrderNumber;
      if (_sendEmailSpecified) {
        retVal.sendemail = SendEmail;
        retVal.sendemailSpecified = true;
      }

      retVal.itemshippinginformationlist = ItemShippingInfo;

      return EncodeHelper.Serialize(retVal);

    }

  }
}
