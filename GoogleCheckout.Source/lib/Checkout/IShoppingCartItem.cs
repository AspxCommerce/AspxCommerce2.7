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
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using GCheckout.Util;

namespace GCheckout.Checkout {

  /// <summary>
  /// A wrapper containing information about an individual item listed 
  /// in the customer's shopping cart
  /// </summary>
  /// <remarks>This will allow for a custom implementation of a cart item.
  /// </remarks>
  public interface IShoppingCartItem : ICloneable {

    /// <summary>
    /// Identifies the name of an item
    /// </summary>
    string Name {
      get;
      set;
    }

    /// <summary>
    /// Contains a description of an item
    /// </summary>
    string Description {
      get;
      set;
    }

    /// <summary>
    /// The cost of the item. This tag has one required attribute, 
    /// which identifies the currency of the price
    /// </summary>
    decimal Price {
      get;
      set;
    }

    /// <summary>
    /// Identifies how many units of a particular item are 
    /// included in the customer's shopping cart.
    /// </summary>
    int Quantity {
      get;
      set;
    }

    /// <summary>
    /// Contains a value, such as a stock keeping unit (SKU), 
    /// that you use to uniquely identify an item.
    /// </summary>
    /// <remarks>Google Checkout will include this value in 
    /// the merchant calculation 
    /// callbacks and the new order notification for the order.</remarks>
    string MerchantItemID {
      get;
      set;
    }

    /// <summary>
    /// A legacy method of allowing the private data be set with a string
    /// </summary>
    string MerchantPrivateItemData {
      get;
      set;
    }

    /// <summary>
    /// Contains any well-formed XML sequence that should 
    /// accompany an individual item in an order.
    /// </summary>
    XmlNode[] MerchantPrivateItemDataNodes {
      get;
      set;
    }

    /// <summary>
    /// Identifies an alternate tax table that should be
    /// used to calculate tax for a particular item.
    /// </summary>
    AlternateTaxTable TaxTable {
      get;
      set;
    }

    /// <summary>
    /// Contains information relating to digital
    /// delivery of an item.
    /// </summary>
    DigitalItem DigitalContent {
      get;
      set;
    }

    /// <summary>
    /// This item is a subscription item if this node has a value.
    /// </summary>
    Subscription Subscription {
      get;
      set;
    }

    /// <summary>
    /// The &lt;item-weight&gt; tag specifies the weight of an 
    /// individual item in the customer's shopping cart.
    /// </summary>
    double Weight {
      get;
      set;
    }
  }
}
