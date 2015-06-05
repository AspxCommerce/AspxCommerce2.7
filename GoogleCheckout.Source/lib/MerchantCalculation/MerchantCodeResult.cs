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
  /// This class creates an object to indicate whether the coupon and gift 
  /// certificate codes that were supplied by the customer are valid. The 
  /// object also indicates whether the code is for a coupon, gift certificate 
  /// or other discount as well as the amount of the credit associated with 
  /// the code. Your class that inherits from CallbackRules.cs will access 
  /// these objects to calculate discounts associated with coupons and gift 
  /// certificates.
  /// </summary>
  public class MerchantCodeResult {

    private decimal _amount;

    /// <summary>
    /// Define the Type of Merchant Code
    /// </summary>
    public MerchantCodeType Type = MerchantCodeType.Undefined;
    /// <summary>
    /// Return if the Code is valid or not
    /// </summary>
    public bool Valid = false;

    /// <summary>
    /// Return the Value of the Code
    /// </summary>
    public decimal Amount {
      get {
        return _amount;
      }
      set {
        value = Math.Round(value, 2); //fix for sending in fractional cents
        _amount = value;
      }
    }
    
    /// <summary>
    /// Return a message relating to the callback.
    /// </summary>
    public string Message = "";
  }

  /// <summary>
  /// Merchant Code Types
  /// </summary>
  public enum MerchantCodeType {
    /// <summary>Unknown</summary>
    Undefined = 0,
    /// <summary>Gift Certificate</summary>
    GiftCertificate = 1,
    /// <summary>Coupon</summary>
    Coupon = 2
  }

}
