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
using GCheckout.Util;

namespace GCheckout.Checkout {
  /// <summary>
  /// Third-Party Conversion Tracking Parameter type.
  /// </summary>
  /// <remarks>
  /// See
  /// http://code.google.com/apis/checkout/developer/checkout_pixel_tracking.html
  /// For additional information on third party tracking
  /// </remarks>
  public enum UrlParameterType {

    ///<summary>
    ///The city associated with the order's billing address.
    ///</summary>
    [EnumSerilizedName("")]
    Unknown = 0,
    ///<summary>
    ///The city associated with the order's billing address.
    ///</summary>
    [EnumSerilizedName("billing-city")]
    BillingCity,
    ///<summary>
    ///The two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    ///country code associated with the order's billing address.
    ///</summary>
    [EnumSerilizedName("billing-country-code")]
    BillingCountryCode,
    ///<summary>
    /// The five-digit U.S. zip code associated with the order's billing address.
    ///</summary>
    [EnumSerilizedName("billing-postal-code")]
    BillingPostalCode,
    ///<summary>
    /// The U.S. state associated with the order's billing address.
    ///</summary>
    [EnumSerilizedName("billing-region")]
    BillingRegion,
    ///<summary>
    /// A Google-assigned value that uniquely identifies a customer email address.
    ///</summary>
    [EnumSerilizedName("buyer-id")]
    BuyerID,
    ///<summary>
    /// The total amount of all coupons factored into the order total.
    ///</summary>
    [EnumSerilizedName("coupon-amount")]
    CouponAmount,
    ///<summary>
    /// A Google-assigned value that uniquely identifies an order. This value is displayed in the Merchant Center for each order. If you have implemented the Notification API, you will also see this value in all Google Checkout notifications.
    ///</summary>
    [EnumSerilizedName("order-id")]
    OrderID,
    ///<summary>
    /// The total cost for all of the items in the order including coupons and discounts but excluding taxes and shipping charges.
    ///</summary>
    [EnumSerilizedName("order-subtotal")]
    OrderSubTotal,
    ///<summary>
    /// The total cost for all of the items in the order, including shipping charges, coupons and discounts, but excluding taxes.
    ///</summary>
    [EnumSerilizedName("order-subtotal-plus-shipping")]
    OrderSubTotalPlusShipping,
    ///<summary>
    /// The total cost for all of the items in the order, including taxes, coupons and discounts, but excluding shipping charges.
    ///</summary>
    [EnumSerilizedName("order-subtotal-plus-tax")]
    OrderSubTotalPlusTax,
    ///<summary>
    /// The total cost for all of the items in the order, including taxes, shipping charges, coupons and discounts.
    ///</summary>
    [EnumSerilizedName("order-total")]
    OrderTotal,
    ///<summary>
    /// The shipping cost associated with an order.
    ///</summary>
    [EnumSerilizedName("shipping-amount")]
    ShippingAmount,
    ///<summary>
    /// The city associated with the order's shipping address.
    ///</summary>
    [EnumSerilizedName("shipping-city")]
    ShippingCity,
    ///<summary>
    /// The two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code associated with the order's shipping address.
    ///</summary>
    [EnumSerilizedName("shipping-country-code")]
    ShippingCountryCode,
    ///<summary>
    /// The five-digit U.S. zip code associated with the order's shipping address.
    ///</summary>
    [EnumSerilizedName("shipping-postal-code")]
    ShippingPostalCode,
    ///<summary>
    /// The U.S. state associated with the order's shipping address.
    ///</summary>
    [EnumSerilizedName("shipping-region")]
    ShippingRegion,
    ///<summary>
    /// The total amount of taxes charged for an order.
    ///</summary>
    [EnumSerilizedName("tax-amount")]
    TaxAmount
  }
}
