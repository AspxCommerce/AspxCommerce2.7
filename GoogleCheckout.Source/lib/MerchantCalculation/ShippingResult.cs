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
  /// This class creates an object to indicate the cost of a particular 
  /// shipping method and the availability of that shipping method for the 
  /// designated shipping address.
  /// </summary>
  public class ShippingResult {

    private decimal _shippingRate;

    /// <summary>Shipping Rate</summary>
    public decimal ShippingRate {
      get {
        return _shippingRate;
      }
      set {
        value = Math.Round(value, 2); //fix for sending in fractional cents
        _shippingRate = value;
      }
    }
    /// <summary>Is this shippable?</summary>
    public bool Shippable = false;
  }
}
