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
 *  3-14-2009   Joe Feser joe.feser@joefeser.com
 *  We no longer allow people to pass in fractional amounts. All numbers are trimmed to $x.xx
 *  OrderLine takes care of it's own rounding.
*/

using System;
using System.Xml;
using System.Collections;
using GCheckout.Checkout;

namespace GCheckout.MerchantCalculation {
  /// <summary>
  /// This class contains methods that parse a 
  /// &lt;merchant-calculation-callback&gt; request and creates an object 
  /// identifying the items in the customer's shopping cart. Your class that 
  /// inherits from CallbackRules.cs will receive an object of this type to 
  /// identify the items in the customer's order.
  /// </summary>
  public class Order {
    private ArrayList _OrderLines;
    private XmlNode[] _MerchantPrivateDataNodes = new XmlNode[] {};

    /// <summary>
    /// Process a Merchant Calculation callback.
    /// </summary>
    /// <param name="Callback">The Callback message.</param>
    public Order(AutoGen.MerchantCalculationCallback Callback) {
      _OrderLines = new ArrayList();
      for (int i = 0; i < Callback.shoppingcart.items.Length; i++) {
        AutoGen.Item ThisItem = Callback.shoppingcart.items[i];

        XmlNode[] merchantItemPrivateDataNodes = new XmlNode[] {};

        if (ThisItem.merchantprivateitemdata != null 
          && ThisItem.merchantprivateitemdata.Any != null
          && ThisItem.merchantprivateitemdata.Any.Length > 0) {

          merchantItemPrivateDataNodes 
            = ThisItem.merchantprivateitemdata.Any;
        }
        _OrderLines.Add(
          new OrderLine(ThisItem.itemname, ThisItem.itemdescription,
          ThisItem.quantity, ThisItem.unitprice.Value,
          ThisItem.taxtableselector,
          merchantItemPrivateDataNodes));
      }

      if (Callback.shoppingcart.merchantprivatedata != null
        && Callback.shoppingcart.merchantprivatedata.Any != null
        && Callback.shoppingcart.merchantprivatedata.Any.Length > 0) {
        
        _MerchantPrivateDataNodes 
          = Callback.shoppingcart.merchantprivatedata.Any;
      }
    }

    /// <summary>
    /// Return an Iterator to process the Order Lines
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator() {
      return _OrderLines.GetEnumerator();
    }

    /// <summary>
    /// Return the sub total of all the items (Quantity * Price)
    /// </summary>
    public decimal ItemSubTotal {
      get {
        decimal retVal = 0;
        foreach (OrderLine Line in this) {
          retVal += Line.UnitPrice * Line.Quantity;
        }
        return retVal;
      }
    }

    /// <summary>
    /// Merchant Private Data
    /// </summary>
    public string MerchantPrivateData {
      get {
        if (_MerchantPrivateDataNodes != null
          && _MerchantPrivateDataNodes.Length > 0) {
          //what we must do is look for an xml node by the name of
          //MERCHANT_DATA_HIDDEN
          foreach (XmlNode node in _MerchantPrivateDataNodes) {
            if (node.Name == CheckoutShoppingCartRequest.MERCHANT_DATA_HIDDEN)
              return node.InnerXml;
          }
          //if we get this far and we have one node, just return it
          if (_MerchantPrivateDataNodes.Length == 1)
            return _MerchantPrivateDataNodes[0].OuterXml;
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// An Array of <see cref="System.Xml.XmlNode" /> 
    /// for the Merchant Private Data
    /// </summary>
    public System.Xml.XmlNode[] MerchantPrivateDataNodes {
      get {
        return _MerchantPrivateDataNodes;
      }
    }
  }
}
