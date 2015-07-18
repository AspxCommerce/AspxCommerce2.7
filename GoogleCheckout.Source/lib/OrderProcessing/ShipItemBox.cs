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
using System.Collections;
using System.Collections.Generic;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// This is used by the ShipItemRequest to allow people to
  /// create boxes and then add items to the box
  /// </summary>
  public class ShipItemBox {

    private ArrayList _items = new ArrayList();
    private AutoGen.TrackingData _tracking;
    private Dictionary<string, AutoGen.ItemShippingInformation> _lookup;
    /// <summary>
    /// The &lt;carrier&gt; tag contains the name of the company 
    /// responsible for shipping the item. Valid values for this 
    /// tag are DHL, FedEx, UPS, USPS and Other.
    /// </summary>
    public string Carrier {
      get {
        return _tracking.carrier;
      }
      set {
        _tracking.carrier = value;
      }
    }

    /// <summary>
    /// The &lt;tracking-number&gt; tag contains the shipper's tracking
    /// number that is associated with an order.
    /// </summary>
    public string TrackingNumber {
      get {
        return _tracking.trackingnumber;
      }
      set {
        _tracking.trackingnumber = value;
      }
    }

    /// <summary>
    /// Create a new instance of a ShipItemBox
    /// </summary>
    /// <param name="carrier">The Carrier of the Box (UPS, USPS, FEDEX)</param>
    /// <param name="trackingNumber">The Tracking Number of the box</param>
    internal ShipItemBox(string carrier, string trackingNumber) {
      _tracking = new GCheckout.AutoGen.TrackingData();
      _tracking.carrier = carrier;
      _tracking.trackingnumber = trackingNumber;
    }

    /// <summary>
    /// Create a new instance of the ShipItemBox.
    /// </summary>
    private ShipItemBox() {
      throw new NotImplementedException("This ctor is not supported.");
    }

    /// <summary>
    /// The &lt;merchant-item-id&gt; tag contains a value, 
    /// such as a stock keeping unit (SKU), 
    /// that you use to uniquely identify an item.
    /// </summary>
    /// <param name="itemID">The item to add to the box</param>
    public void AddMerchantItemID(string itemID) {
      if (itemID == null || itemID.Length == 0)
        throw new ArgumentException("itemID must be valid length > 0.");

      AutoGen.ItemShippingInformation[] items
        = new AutoGen.ItemShippingInformation[_items.Count];
      _items.CopyTo(items, 0);

      for (int i = 0; i < items.Length; i++) {
        if (items[i].itemid.merchantitemid == itemID) {
          throw new ApplicationException(
            "Duplicate MerchantItemID added to the box'" + itemID + "'.");
        }
      }

      //The hash starts with G for google items and M for merchant items
      AutoGen.ItemShippingInformation item = null;

      string key = "M:" + itemID;

      if (!_lookup.TryGetValue(key, out item)) {

        item = new AutoGen.ItemShippingInformation();

        AutoGen.ItemId lineItem = new GCheckout.AutoGen.ItemId();
        lineItem.merchantitemid = itemID;
        item.itemid = lineItem;

        _lookup[key] = item;
      }
      AppendTracking(item);
      _items.Add(item);

    }

    private void AppendTracking(AutoGen.ItemShippingInformation item) {
      if (item.trackingdatalist == null)
        item.trackingdatalist = new AutoGen.TrackingData[] { };

      AutoGen.TrackingData[] newList
        = new GCheckout.AutoGen.TrackingData[item.trackingdatalist.Length + 1];

      if (item.trackingdatalist.Length > 0) {
        Array.Copy(item.trackingdatalist, 0,
          newList, 0, item.trackingdatalist.Length);
      }

      newList[newList.Length - 1] = _tracking;
      item.trackingdatalist = newList;

    }

    /// <summary>
    /// Create a new box
    /// </summary>
    /// <param name="carrier">The carrier</param>
    /// <param name="trackingNumber">The Tracking number.</param>
    /// <param name="lookup">The lookup value</param>
    /// <returns></returns>
    public static ShipItemBox CreateBox(string carrier, string trackingNumber, 
      Dictionary<string, AutoGen.ItemShippingInformation> lookup) {
      ShipItemBox retVal = new ShipItemBox(carrier, trackingNumber);
      retVal._lookup = lookup;
      retVal.Carrier = carrier;
      retVal.TrackingNumber = trackingNumber;
      return retVal;
    }

  }
}
