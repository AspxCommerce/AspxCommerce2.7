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

namespace GCheckout.MerchantCalculation {
  /// <summary>
  /// This abstract class defines methods for constructing a 
  /// &lt;merchant-calculation-results&gt; XML response.
  /// </summary>
  public abstract class CallbackRules {
    /// <summary>
    /// Return a The &lt;merchant-code-results%gt; tag contains information 
    /// about coupons and gift certificates that were calculated into an 
    /// order total
    /// </summary>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <param name="MerchantCode">Contains a coupon or gift certificate code
    /// that the customer entered for an order.</param>
    /// <returns></returns>
    public virtual MerchantCodeResult GetMerchantCodeResult(Order ThisOrder, 
      AnonymousAddress Address, string MerchantCode) {
      return new MerchantCodeResult();
    }

    /// <summary>
    /// The &lt;total-tax&gt; tag contains the total tax amount for an order
    /// </summary>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <param name="ShippingRate">The cost of shipping the order.</param>
    /// <returns></returns>
    public virtual decimal GetTaxResult(Order ThisOrder, 
      AnonymousAddress Address, decimal ShippingRate) {
      return 0;
    }

    /// <summary>
    /// The &lt;shipping-rate&gt; tag contains the shipping costs 
    /// that have been calculated for an order.
    /// </summary>
    /// <param name="ShipMethodName">Identifies a shipping method for which
    /// costs need to be calculated.</param>
    /// <param name="ThisOrder">The Order to perform the calculation</param>
    /// <param name="Address">contains a possible shipping address for an order.
    /// This address should be used to calculate taxes and shipping costs 
    /// for the order.</param>
    /// <returns></returns>
    public virtual ShippingResult GetShippingResult(string ShipMethodName, 
      Order ThisOrder, AnonymousAddress Address) {
      return new ShippingResult();
    }
  }

}
