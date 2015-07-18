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
using System.Reflection;
using GCheckout.Util;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;shipping-type&gt; tag identifies the shipping option that is
  /// being offered to the buyer. The table below shows the valid values for 
  /// this tag for each carrier. None of these shipping options are for 
  /// international shipping. Please note that Google will reject a Checkout 
  /// API request that specifies a carrier-calculated shipping option for
  /// which the &lt;shipping-type&gt; is not valid for the specified carrier.
  /// </summary>
  public enum ShippingType {
    /// <summary></summary>
    [TypeDictionaryEntry("default-value", "true")]
    [TypeDictionaryEntry("shipping-company", "")]
    [EnumSerilizedName("")]
    Unknown = 0,

    //FedEx
    /// <summary>FedEx Ground</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("Ground")]
    Fedex_Ground = 10,
    /// <summary>FedEx Home Delivery</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("Home Delivery")]
    Fedex_Home_Delivery = 11,
    /// <summary>FedEx Express Saver</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("Express Saver")]
    Fedex_Express_Saver = 12,
    /// <summary>FedEx First Overnight</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("First Overnight")]
    Fedex_First_Overnight = 13,
    /// <summary>FedEx Priority Overnight</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("Priority Overnight")]
    Fedex_Priority_Overnight = 14,
    /// <summary>FedEx Standard Overnight</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("Standard Overnight")]
    Fedex_Standard_Overnight = 15,
    /// <summary>FedEx 2 Day</summary>
    [TypeDictionaryEntry("shipping-company", "Fedex")]
    [EnumSerilizedName("2Day")]
    Fedex_Second_Day = 16,


    //UPS
    /// <summary>UPS Next Day Air</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("Next Day Air")]
    UPS_Next_Day_Air = 50,
    /// <summary>UPS Next Day Air Early AM</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("Next Day Air Early AM")]
    UPS_Next_Day_Air_Early_AM = 51,
    /// <summary>UPS Next Day Air Saver</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("Next Day Air Saver")]
    UPS_Next_Day_Air_Saver = 52,
    /// <summary>UPS 2nd Day Air</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("2nd Day Air")]
    UPS_2nd_Day_Air = 53,
    /// <summary>UPS 2nd Day Air AM</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("2nd Day Air AM")]
    UPS_2nd_Day_Air_AM = 54,
    /// <summary>UPS 3 Day Select</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("3 Day Select")]
    UPS_Three_Day_Select = 55,
    /// <summary>UPS Ground</summary>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("Ground")]
    UPS_Ground = 56,
    /// <summary>UPS MI</summary>
    /// <remarks>Please note that UPS MI and UPS Mail Innovations 
    /// both refer to the same UPS service.</remarks>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("MI")]
    UPS_MI = 57,
    /// <summary>UPS Mail Innovations</summary>
    /// <remarks>Please note that UPS MI and UPS Mail Innovations 
    /// both refer to the same UPS service.</remarks>
    [TypeDictionaryEntry("shipping-company", "UPS")]
    [EnumSerilizedName("Mail Innovations")]
    UPS_Mail_Innovations = 58,

    //USPS
    /// <summary>Express Mail</summary>
    [TypeDictionaryEntry("shipping-company", "USPS")]
    [EnumSerilizedName("Express Mail")]
    USPS_Express_Mail = 100,
    /// <summary>Priority Mail</summary>
    [TypeDictionaryEntry("shipping-company", "USPS")]
    [EnumSerilizedName("Priority Mail")]
    USPS_Priority_Mail = 101,
    /// <summary>Parcel Post</summary>
    [TypeDictionaryEntry("shipping-company", "USPS")]
    [EnumSerilizedName("Parcel Post")]
    USPS_Parcel_Post = 102,
    /// <summary>Media Mail</summary>
    [TypeDictionaryEntry("shipping-company", "USPS")]
    [EnumSerilizedName("Media Mail")]
    USPS_Media_Mail = 103
  }

  /// <summary>
  /// Helper Class to Obtain information on ShippingType
  /// </summary>
  public abstract class ShippingTypeHelper {

    private static Type t = typeof(ShippingType);
    
    /// <summary>
    /// Get the Shipping Comany Name for an Enum Value
    /// </summary>
    /// <param name="shippingType">The ShippingType Value</param>
    /// <returns></returns>
    public static string GetShippingCompany(ShippingType shippingType) {
      FieldInfo fi = t.GetField(shippingType.ToString());
      return TypeDictionaryEntryAttribute.GetValue(fi, "shipping-company");
    }

    /// <summary>
    /// Get the Name for the Enum Value
    /// </summary>
    /// <param name="shippingType">The ShippingType Value</param>
    /// <returns></returns>
    public static string GetSerializedName(ShippingType shippingType) {
      FieldInfo fi = t.GetField(shippingType.ToString());
      return EnumSerilizedNameAttribute.GetValue(fi);
    }
  }
}
