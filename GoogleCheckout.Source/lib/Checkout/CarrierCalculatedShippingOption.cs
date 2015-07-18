/*************************************************
 * Copyright (C) 2007-2009 Google Inc.
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
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Fix issue 67 and cleaned up the default currency and amount defaults
 * 
*/
using System;
using GCheckout.Util;
using td = GCheckout.Util.TypeDictionaryEntryAttribute;
using eh = GCheckout.Util.EncodeHelper;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;carrier-calculated-shipping-option&gt; tag contains information
  /// about a single shipping method for which Google Checkout should obtain
  /// shipping costs.
  /// </summary>
  public class CarrierCalculatedShippingOption {
    
    private string _currency = null; //do not set to default
    private ShippingType _statedShippingType;
    private CarrierPickup _carrierPickup = CarrierPickup.DROP_OFF;
    private AutoGen.CarrierCalculatedShippingOption _autoGenClass;

    /// <summary>
    /// The default cost for the shipping option. 
    /// The default cost will be assessed if Google's attempt to obtain the 
    /// carrier's shipping rates fails for any reason.
    /// </summary>
    public decimal Price {    
      get {
        return _autoGenClass.price.Value;
      }
      set {
        value = Math.Round(value, 2); //fix for sending in fractional cents
        AutoGen.Money m = eh.Money(_currency, value);

        AutoGen.CarrierCalculatedShippingOptionPrice tp =
          new GCheckout.AutoGen.CarrierCalculatedShippingOptionPrice();
        tp.currency = m.currency;
        tp.Value = m.Value;
        _autoGenClass.price = tp;
      }
    }

    /// <summary>
    /// The &lt;shipping-company&gt; tag contains the name of the company that
    /// will ship the order. The only valid values for this tag are FedEx, 
    /// UPS and USPS.
    /// </summary>
    public string ShippingCompany {
      get {
        return ShippingTypeHelper.GetShippingCompany(_statedShippingType);
      }
    }

    /// <summary>
    /// The &lt;shipping-type&gt; tag identifies the shipping option
    /// that is being offered to the buyer.
    /// </summary>
    public string ShippingType {
      get {
        return ShippingTypeHelper.GetSerializedName(_statedShippingType);   
      }
    }

    /// <summary>
    /// The &lt;shipping-type&gt; tag identifies the shipping option
    /// that is being offered to the buyer.
    /// </summary>
    public ShippingType StatedShippingType {
      get {
        return _statedShippingType;
      }
    }

    /// <summary>
    /// The &lt;carrier-pickup&gt; tag specifies how the package will be 
    /// transferred from the merchant to the shipper. Valid values for this
    /// tag are REGULAR_PICKUP, SPECIAL_PICKUP and DROP_OFF. The default
    /// value for this tag is DROP_OFF.
    /// </summary>
    public CarrierPickup CarrierPickup { 
      get {
        return _carrierPickup;
      }
      set {
        _carrierPickup = value;
        _autoGenClass.carrierpickup = value.ToString();
      }
    }

    /// <summary>
    /// The &lt;additional-fixed-charge&gt; tag allows you to specify a fixed
    /// charge that will be added to the total cost of an order if the buyer
    /// selects the associated shipping option. If you also adjust the
    /// calculated shipping cost using the
    /// &lt;additional-variable-charge-percent&gt; tag, the fixed charge will
    /// be added to the adjusted shipping rate.
    /// </summary>
    public decimal AdditionalFixedCharge {
      get {
        if (_autoGenClass.additionalfixedcharge != null)
          return _autoGenClass.additionalfixedcharge.Value;
        else
          return 0;
      }
      set {
        if (value < 0) {
          throw new ArgumentOutOfRangeException("AdditionalFixedCharge", 
            "The value must be a positive number");
        }
        if (value == 0) {
          _autoGenClass.additionalfixedcharge = null;
        }
        else {
          value = Math.Round(value, 2); //fix for sending in fractional cents
          _autoGenClass.additionalfixedcharge = eh.Money(_currency, value);           
        }
      }
    }

    /// <summary>
    /// The &lt;additional-variable-charge-percent&gt; tag specifies a
    /// percentage amount by which a carrier-calculated shipping rate will be
    /// adjusted. The tag's value may be positive or negative. For example, if
    /// the tag's value is 15, then the carrier's shipping rate will
    /// effectively be multiplied by 1.15 to determine the shipping cost
    /// presented to the buyer. So, if the carrier shipping rate were $10.00,
    /// the adjusted shipping rate would be $11.50 – i.e. $10.00 +
    /// ($10.00 X 15%). If the &lt;additional-variable-charge-percent&gt; tag
    /// value is negative, the calculated shipping rate will be discounted by 
    /// the specified percentage.
    /// </summary>
    public double AdditionalVariableChargePercent {
      get {
        return _autoGenClass.additionalvariablechargepercent;
      }
      set {
        if (value != 0) {
          _autoGenClass.additionalvariablechargepercent = value;
          _autoGenClass.additionalvariablechargepercentSpecified = true;           
        }
        else {
          _autoGenClass.additionalvariablechargepercent = 0;
          _autoGenClass.additionalvariablechargepercentSpecified = false;           
        }
      }
    }

    /// <summary>
    /// This is the shipping option created by the internal type
    /// It should not be modified outside of this class or it will not be in sync.
    /// </summary>
    internal AutoGen.CarrierCalculatedShippingOption ShippingOption {
      get {
        return _autoGenClass;
      }
    }

    /// <summary>
    /// Create an instance of carrier-calculated-shipping-option
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="shippingType">
    /// The &lt;shipping-type&gt; tag identifies the shipping option
    /// that is being offered to the buyer.
    /// </param>
    /// <param name="defaultPrice">
    /// The default cost for the shipping option. 
    /// The default cost will be assessed if Google's attempt to obtain the 
    /// carrier's shipping rates fails for any reason.
    /// </param>
    public CarrierCalculatedShippingOption(string currency,
      ShippingType shippingType, decimal defaultPrice) {

      defaultPrice = Math.Round(defaultPrice, 2); //fix for sending in fractional cents
      _autoGenClass = new GCheckout.AutoGen.CarrierCalculatedShippingOption();

      _currency = currency;
      _statedShippingType = shippingType;

      //must call the setter to set the price.
      Price = defaultPrice;

      //set the defaults that the object needs.
      _autoGenClass.shippingtype = ShippingType;
      _autoGenClass.shippingcompany = ShippingCompany;
      _autoGenClass.carrierpickup = CarrierPickup.ToString();
    }

  }
}
