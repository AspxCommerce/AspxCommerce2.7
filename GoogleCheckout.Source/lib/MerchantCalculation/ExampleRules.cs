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
 * 
*/
using System;

namespace GCheckout.MerchantCalculation {
  /// <summary>
  /// This class contains a sample class that inherits from CallbackRules.cs. 
  /// This class demonstrates how you could subclass CallbackRules.cs to 
  /// define your own merchant-calculated shipping, tax and coupon options.
  /// </summary>
  public class ExampleRules : CallbackRules {
    private const string SAVE10 = "SAVE10";
    private const string SAVE20 = "SAVE20";
    private const string GIFTCERT = "GIFTCERT";

    /// <summary>
    /// Example rules showing possible ways to handle callbacks
    /// </summary>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <param name="MerchantCode">Contains a coupon or gift certificate code
    /// that the customer entered for an order.</param>
    /// <returns></returns>
    public override MerchantCodeResult GetMerchantCodeResult(Order ThisOrder, 
      AnonymousAddress Address, string MerchantCode) {
      MerchantCodeResult RetVal = new MerchantCodeResult();
      if (MerchantCode.ToUpper() == SAVE10) {
        RetVal.Amount = 10;
        RetVal.Type = MerchantCodeType.Coupon;
        RetVal.Valid = true;
        RetVal.Message = "You saved $10!";
      }
      else if (MerchantCode.ToUpper() == SAVE20) {
        RetVal.Amount = 20;
        RetVal.Type = MerchantCodeType.Coupon;
        RetVal.Valid = true;
        RetVal.Message = "You saved $20!";
      }
      else if (MerchantCode.ToUpper() == GIFTCERT) {
        RetVal.Amount = 23.46m;
        RetVal.Type = MerchantCodeType.GiftCertificate;
        RetVal.Valid = true;
        RetVal.Message = "Your gift certificate has a balance of $23.46.";
      }
      else {
        RetVal.Message = "Sorry, we didn't recognize code '" + MerchantCode + 
          "'.";
      }
      return RetVal;
    }

    /// <summary>
    /// Example showing how Tax is calculated for a callback
    /// </summary>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <param name="ShippingRate">The cost of shipping the order.</param>
    /// <returns></returns>
    public override decimal GetTaxResult(Order ThisOrder, 
      AnonymousAddress Address, decimal ShippingRate) {
      decimal RetVal = 0;
      if (Address.Region == "HI") {
        decimal Total = 0;
        foreach (OrderLine Line in ThisOrder) {
          Total += Line.UnitPrice * Line.Quantity;
        }
        RetVal = decimal.Round(Total * 0.09m, 2);
      }
      return RetVal;
    }

    /// <summary>
    /// Example showing how shipping is calculated for a callback
    /// </summary>
    /// <param name="ShipMethodName">Identifies a shipping method for which
    /// costs need to be calculated.</param>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <returns></returns>
    public override ShippingResult GetShippingResult(string ShipMethodName, 
      Order ThisOrder, AnonymousAddress Address) {
      ShippingResult RetVal = new ShippingResult();
      if (ShipMethodName == "UPS Ground" && Address.Region != "HI" && 
        Address.Region != "AL") {
        RetVal.Shippable = true;
        RetVal.ShippingRate = 20;
      }
      else if (ShipMethodName == "SuperShip") {
        RetVal.Shippable = true;
        RetVal.ShippingRate = 0;
      }
      else {
        //next we will look at the merchant-private-item-data
        //if the supplier-id is "ABC Candy Company" then you will get free shipping.
        //do not assume the nodes will be in the same order, we will walk the node
        //list looking for a node with the name of "supplier-id"

        //you can just as easily import all the nodes into an XmlDocument and perform
        //XPath statements.

        //You can also create a string dictionary by performing a foreach statement
        //on the nodes and using the node name as the key and the innerText as the
        //value.

        //We are just showing one of many ways to work with an array of XmlNodes.
        //As you can see from the sample code, you may also have children on any
        //of the MerchantPrivateDataNodes.

        string supplierID = "ABC Candy Company".ToUpper();

        foreach (OrderLine Line in ThisOrder) {
          foreach (System.Xml.XmlNode node in Line.MerchantPrivateDataNodes) {
            if (node.Name == "supplier-id") {
              if (supplierID == node.InnerText.ToUpper()) {
                RetVal.Shippable = true;
                RetVal.ShippingRate = 0;
                break;
              }
            }
          }
        }
      }
      return RetVal;
    }
  }
}
