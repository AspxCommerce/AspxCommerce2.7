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
using GCheckout.Util;
using ch = GCheckout.Util.GCheckoutConfigurationHelper;

namespace GCheckout.MerchantCalculation {

  /// <summary>
  /// This class contains methods that parse a 
  /// &lt;merchant-calculation-callback&gt; request, allowing you to access 
  /// items in the request, shipping methods, shipping addresses, tax 
  /// information, and coupon and gift certificate codes entered by the 
  /// customer.
  /// </summary>
  public class CallbackProcessor {
    CallbackRules _Rules = null;

    private string _currency = string.Empty;

    /// <summary>
    /// Create a new instance of the Processor, passing in the rules used
    /// to process the request.
    /// </summary>
    /// <param name="Rules">The <see cref="CallbackRules"/> object 
    /// used for the Process Request.</param>
    public CallbackProcessor(CallbackRules Rules) {
      _Rules = Rules;
      _currency = ch.DefaultCurrency;
    }

    /// <summary>
    /// Create a new instance of the Processor, passing in the rules used
    /// to process the request.
    /// </summary>
    /// <param name="Rules">The <see cref="CallbackRules"/> object 
    /// used for the Process Request.</param>
    /// <param name="currency">The currency associated with prices in a 
    /// Checkout API request.</param>
    public CallbackProcessor(CallbackRules Rules, string currency) {
      _Rules = Rules;
      _currency = currency;
    }

    /// <summary>
    /// Process the Xml Message
    /// </summary>
    /// <param name="CallbackXML">The Callback message to process</param>
    /// <returns>The <see cref="AutoGen.MerchantCalculationResults"/> 
    /// that is returned to Google Checkout</returns>
    public byte[] Process(string CallbackXML) {
      // Deserialize the callback request.
      AutoGen.MerchantCalculationCallback Callback = 
        (AutoGen.MerchantCalculationCallback) 
        EncodeHelper.Deserialize
        (CallbackXML, typeof(AutoGen.MerchantCalculationCallback));
      // Create the callback response.
      AutoGen.MerchantCalculationResults RetVal = 
        new AutoGen.MerchantCalculationResults();
      // Create the order.
      Order ThisOrder = new Order(Callback);
      // Are there shipping methods?
      string[] ShippingMethods = new string[1] {""};
      if (Callback.calculate.shipping != null && 
        Callback.calculate.shipping.Length > 0) {
        ShippingMethods = new string[Callback.calculate.shipping.Length];
        for (int s = 0; s < ShippingMethods.Length; s++) {
          ShippingMethods[s] = Callback.calculate.shipping[s].name;
        }
      }

      RetVal.results = 
        new AutoGen.Result
        [Callback.calculate.addresses.Length * ShippingMethods.Length];
      int ResultIndex = 0;
      for (int a = 0; a < Callback.calculate.addresses.Length; a++) {
        for (int s = 0; s < ShippingMethods.Length; s++) {
          AutoGen.Result ThisResult = new AutoGen.Result();
          ThisResult.addressid = Callback.calculate.addresses[a].id;
          AnonymousAddress ThisAddress = 
            new AnonymousAddress(Callback.calculate.addresses[a]);
          // Check shipping, if requested.
          if (ShippingMethods[s] != string.Empty) {
            ThisResult.shippingname = ShippingMethods[s];
            ShippingResult SResult = 
              _Rules.GetShippingResult
              (ShippingMethods[s], ThisOrder, ThisAddress);
            ThisResult.shippableSpecified = true;
            ThisResult.shippable = SResult.Shippable;
            ThisResult.shippingrate = new AutoGen.ResultShippingrate();
            ThisResult.shippingrate.currency = _currency;
            ThisResult.shippingrate.Value = SResult.ShippingRate;
          }
          // Check tax, if requested.
          if (Callback.calculate.tax) {
            ThisResult.totaltax = new AutoGen.ResultTotaltax();
            ThisResult.totaltax.currency = _currency;
            ThisResult.totaltax.Value = 
              _Rules.GetTaxResult
              (ThisOrder, ThisAddress, 
              (ThisResult.shippingrate != null? 
              ThisResult.shippingrate.Value: 0));
          }
          // Check merchant codes.
          if (Callback.calculate.merchantcodestrings != null) {
            ThisResult.merchantcoderesults = 
              new AutoGen.ResultMerchantcoderesults();
            ThisResult.merchantcoderesults.Items = 
              new object[Callback.calculate.merchantcodestrings.Length];
            ArrayList UsedMerchantCodes = new ArrayList();
            for (int c = 0; c < Callback.calculate.merchantcodestrings.Length; 
              c++) {
              MerchantCodeResult MCR;
              string CurrentMerchantCode = 
                Callback.calculate.merchantcodestrings[c].code;
              if (UsedMerchantCodes.Contains(CurrentMerchantCode.ToUpper())) {
                AutoGen.CouponResult CouponResult = new AutoGen.CouponResult();
                CouponResult.calculatedamount 
                  = new AutoGen.CouponResultCalculatedamount();
                CouponResult.calculatedamount.currency = _currency;
                CouponResult.calculatedamount.Value = 0;
                CouponResult.code = CurrentMerchantCode;
                CouponResult.message = "Code already used.";
                CouponResult.valid = false;
                ThisResult.merchantcoderesults.Items[c] = CouponResult;
              }
              else {
                MCR = 
                  _Rules.GetMerchantCodeResult
                  (ThisOrder, ThisAddress, CurrentMerchantCode);
                if (MCR.Type == MerchantCodeType.GiftCertificate) {
                  AutoGen.GiftCertificateResult GCResult = 
                    new AutoGen.GiftCertificateResult();
                  GCResult.calculatedamount 
                    = new AutoGen.GiftCertificateResultCalculatedamount();
                  GCResult.calculatedamount.currency = _currency;
                  GCResult.calculatedamount.Value = MCR.Amount;
                  GCResult.code = CurrentMerchantCode;
                  GCResult.message = MCR.Message;
                  GCResult.valid = MCR.Valid;
                  ThisResult.merchantcoderesults.Items[c] = GCResult;
                  UsedMerchantCodes.Add(CurrentMerchantCode.ToUpper());
                }
                else {
                  AutoGen.CouponResult CouponResult = 
                    new AutoGen.CouponResult();
                  CouponResult.calculatedamount 
                    = new AutoGen.CouponResultCalculatedamount();
                  CouponResult.calculatedamount.currency = _currency;
                  CouponResult.calculatedamount.Value = MCR.Amount;
                  CouponResult.code = CurrentMerchantCode;
                  CouponResult.message = MCR.Message;
                  CouponResult.valid = MCR.Valid;
                  ThisResult.merchantcoderesults.Items[c] = CouponResult;
                  UsedMerchantCodes.Add(CurrentMerchantCode.ToUpper());
                }
              }
            }
          }
          RetVal.results[ResultIndex] = ThisResult;
          ResultIndex++;
        }
      }

      return EncodeHelper.Serialize(RetVal);
    }

  }
}
