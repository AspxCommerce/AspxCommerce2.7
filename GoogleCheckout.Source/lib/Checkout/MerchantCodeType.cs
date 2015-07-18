/*************************************************
 * Copyright (C) 2008-2011 Google Inc.
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
 *  4-14-2008   Joe Feser joe.feser@joefeser.com
 * 
*/
using System;

namespace GCheckout.Checkout {
  /// <summary>
  /// A Merchant Code Type
  /// </summary>
  public enum MerchantCodeType {

    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// The &lt;gift-certificate-adjustment&gt; tag contains information
    /// about a gift certificate that was applied to an order total.
    /// </summary>
    GiftCertificate,
    
    /// <summary>
    /// The &lt;coupon-adjustment&gt; tag contains information about a
    /// coupon that was applied to an order total.
    /// </summary>
    Coupon
  }
}
