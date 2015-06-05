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
using System.Collections;
using System.Collections.Generic;

namespace GCheckout.Checkout {
  
  /// <summary>
  /// Base class to support merchant codes on notifications.
  /// </summary>
  public class MerchantCode {

    private string _message;
    private decimal _calculatedamount;
    private decimal _appliedamount;
    private string _code;
    private MerchantCodeType _merchantCodeType;

    /// <summary>
    /// The &lt;merchant-codes&gt; tag contains a list of gift certificate
    /// codes and coupon codes that were applied to an order total.
    /// </summary>
    public MerchantCodeType CodeType {
      get{
        return _merchantCodeType;
      } 
      set {
        _merchantCodeType = value;
      }
    }

    /// <summary>
    /// The &lt;message&gt; tag contains a message associated with a coupon 
    /// or gift certificate or, when used in a &lt;send-buyer-message&gt; 
    /// request, a message that you want to communicate to a buyer about a 
    /// specific order. The maximum accepted length for the tag value
    /// is 255 characters. 
    /// </summary>
    public string Message {
      get{
        return _message;
      }
      set{
        _message = value;
      }
    }

    /// <summary>
    /// The &lt;calculated-amount&gt; tag contains the 
    /// calculated amount of a coupon or gift certificate.
    /// </summary>
    public decimal CalculatedAmount{
      get{
        return _calculatedamount;
      }
      set{
        value = Math.Round(value, 2); //fix for sending in fractional cents
        _calculatedamount = value;
      }
    }

    /// <summary>
    /// The &lt;applied-amount&gt; tag contains the amount of a coupon or gift 
    /// certificate that was applied to an order total.
    /// </summary>
    public decimal AppliedAmount{
      get{
        return _appliedamount;
      }
      set{
        value = Math.Round(value, 2); //fix for sending in fractional cents
        _appliedamount = value;
      }
    }
    
    /// <summary>
    /// The &lt;code&gt; tag contains the code for a coupon or gift certificate
    /// that was applied to an order.
    /// </summary>
    public string Code {
      get{
        return _code;
      }
      set{
        _code = value;
      }
    }

    /// <summary>
    /// Create a new Merchant Code
    /// </summary>
    public MerchantCode() {

    }

    /// <summary>
    /// Convert a coupon adjustment into a merchant code.
    /// </summary>
    /// <param name="item">The coupon to convert</param>
    public MerchantCode(AutoGen.CouponAdjustment item) {
      this.CodeType = MerchantCodeType.Coupon;
      this.AppliedAmount = item.appliedamount.Value;
      this.CalculatedAmount = item.calculatedamount.Value;
      this.Code = item.code;
      this.Message = item.message;
    }

    /// <summary>
    /// Convert a coupon adjustment into a merchant code.
    /// </summary>
    /// <param name="item">The Gift Certification to convert</param>
    public MerchantCode(AutoGen.GiftCertificateAdjustment item) {
      this.CodeType = MerchantCodeType.GiftCertificate;
      this.AppliedAmount = item.appliedamount.Value;
      this.CalculatedAmount = item.calculatedamount.Value;
      this.Code = item.code;
      this.Message = item.message;
    }

    /// <summary>
    /// Obtain the Merchant Codes from the Order Notification.
    /// </summary>
    /// <param name="notification">The <seealso cref="AutoGen.NewOrderNotification"/></param>
    /// <returns></returns>
    public static List<MerchantCode> GetMerchantCodes(AutoGen.NewOrderNotification notification) {
      return GetMerchantCodes(notification.orderadjustment);
    }

    /// <summary>
    /// Obtain the Merchant Codes from the Order Adjustment.
    /// </summary>
    /// <param name="adjustment">The <seealso cref="AutoGen.OrderAdjustment"/></param>
    /// <returns></returns>
    public static List<MerchantCode> GetMerchantCodes(AutoGen.OrderAdjustment adjustment) {
      if (adjustment == null)
        return new List<MerchantCode>();
      return GetMerchantCodes(adjustment.merchantcodes);
    }

    /// <summary>
    /// Obtain the Merchant Codes from the OrderAdjustmentMerchantcodes.
    /// </summary>
    /// <param name="merchantcodes">The <seealso cref="AutoGen.OrderAdjustmentMerchantcodes"/></param>
    /// <returns></returns>
    public static List<MerchantCode> GetMerchantCodes(AutoGen.OrderAdjustmentMerchantcodes merchantcodes) {
      List<MerchantCode> retVal = new List<MerchantCode>();
      if (merchantcodes == null)
        return retVal;

      foreach(object item in merchantcodes.Items) {
        if (item is AutoGen.CouponAdjustment) {
          AutoGen.CouponAdjustment adjust = item as AutoGen.CouponAdjustment;
          retVal.Add(new MerchantCode(adjust));
        }
        if (item is AutoGen.GiftCertificateAdjustment) {
          AutoGen.GiftCertificateAdjustment adjust = item as AutoGen.GiftCertificateAdjustment;
          retVal.Add(new MerchantCode(adjust));
        }
      }
      return retVal;
    }
  }
}
