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
 *  Converted class to use generics and removed hashtable and the ArrayList.
 * 
*/
using System;
using System.Collections;
using eh = GCheckout.Util.EncodeHelper;
using System.Collections.Generic;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;carrier-calculated-shipping&gt; tag contains information that
  /// Google will need to request shipping costs for an order from the
  /// shipment carrier.
  /// </summary>
  public class CarrierCalculatedShipping {
  
    private CheckoutShoppingCartRequest _request = null;
    private List<ShippingPackage> _packages = new List<ShippingPackage>();
    private Dictionary<ShippingType, 
      CarrierCalculatedShippingOption> _shippingOptions
      = new Dictionary<ShippingType,CarrierCalculatedShippingOption>();
    private AutoGen.CarrierCalculatedShipping _shippingNode;

    /// <summary>
    /// Return the count of CarrierCalculatedShipping Items
    /// </summary>
    public int ShippingOptionsCount {
      get {
        return _shippingOptions.Count;
       }
    }

    /// <summary>
    /// Return the count of CarrierCalculatedShipping Items
    /// </summary>
    public bool PackagesExist {
      get {
        return _packages.Count > 0;
      }
    }

    /// <summary>
    /// Create a new instance of the &lt;carrier-calculated-shipping&gt; tag
    /// </summary>
    public CarrierCalculatedShipping(CheckoutShoppingCartRequest request) {
      _request = request;
    }

    /// <summary>
    /// Create a new Carrier Calculated Shipping Option with the minimum
    /// Amount of information needed.
    /// </summary>
    /// <param name="shippingType">The Shipping Type to add
    /// (This must be unique)</param>
    /// <param name="defaultValue">The default cost for the shipping option. 
    /// The default cost will be assessed if Google's attempt to obtain the 
    /// carrier's shipping rates fails for any reason.</param>
    /// <returns></returns>
    public CarrierCalculatedShippingOption AddShippingOption(
      ShippingType shippingType, decimal defaultValue) {

      //CarrierCalculatedShippingOption verifies the fractional cents

      if (_shippingNode == null) {
        //don't add it until we make sure we can use it.
       GCheckout.AutoGen.CarrierCalculatedShipping cs 
         = new GCheckout.AutoGen.CarrierCalculatedShipping();
        cs.carriercalculatedshippingoptions 
          = new GCheckout.AutoGen.CarrierCalculatedShippingOption[] {};
        //This will blow up if the type is not allowed.
        _request.VerifyShippingMethods(cs);
        _shippingNode = cs;
        _request.AddNewShippingMethod(cs);
      }

      if (_shippingOptions.ContainsKey(shippingType))
        throw new ApplicationException(
          string.Format("The carrier option {0} already exists."));

      CarrierCalculatedShippingOption retVal 
        = new CarrierCalculatedShippingOption(_request._Currency, 
        shippingType, defaultValue);

      //we need to copy the array and add an item to the end of the array
      //we would modify the xsd to be lists but it would most likely
      //confuse people.
      GCheckout.AutoGen.CarrierCalculatedShippingOption[] newArray
        = new GCheckout.AutoGen.CarrierCalculatedShippingOption[
        ShippingOptionsCount + 1
        ];
    
      Array.Copy(_shippingNode.carriercalculatedshippingoptions, 
        newArray, ShippingOptionsCount);
      newArray[newArray.Length - 1] = retVal.ShippingOption;

      _shippingNode.carriercalculatedshippingoptions = newArray;

      _shippingOptions.Add(shippingType, retVal);

      Sync();

      return retVal;
    }

    /// <summary>
    /// Create a new Carrier Calculated Shipping Option with the minimum
    /// Amount of information needed.
    /// </summary>
    /// <param name="shippingType">The Shipping Type to add
    /// (This must be unique)</param>
    /// <param name="defaultValue">The default cost for the shipping option. 
    /// The default cost will be assessed if Google's attempt to obtain the 
    /// carrier's shipping rates fails for any reason.</param>
    /// <param name="carrierPickup">
    /// The &lt;carrier-pickup&gt; tag specifies how the package will be 
    /// transferred from the merchant to the shipper. Valid values for this
    /// tag are REGULAR_PICKUP, SPECIAL_PICKUP and DROP_OFF. The default
    /// value for this tag is DROP_OFF.
    /// </param>
    /// <param name="additionalFixedCharge">
    /// The &lt;additional-fixed-charge&gt; tag allows you to specify a fixed
    /// charge that will be added to the total cost of an order if the buyer
    /// selects the associated shipping option. If you also adjust the
    /// calculated shipping cost using the
    /// &lt;additional-variable-charge-percent&gt; tag, the fixed charge will
    /// be added to the adjusted shipping rate.
    /// </param>
    /// <param name="additionalVariableChargePercent">
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
    /// </param>
    /// <returns></returns>
    public CarrierCalculatedShippingOption AddShippingOption(
      ShippingType shippingType, decimal defaultValue,
      CarrierPickup carrierPickup, decimal additionalFixedCharge,
      double additionalVariableChargePercent) {

      //CarrierCalculatedShippingOption verifies the fractional cents
      //call the default Add to perform the validation
      CarrierCalculatedShippingOption retVal 
        = AddShippingOption(shippingType, defaultValue);

      additionalFixedCharge = Math.Round(additionalFixedCharge, 2);

      retVal.CarrierPickup = carrierPickup;
      retVal.AdditionalFixedCharge = additionalFixedCharge;

      if (additionalVariableChargePercent != 0)
        retVal.AdditionalVariableChargePercent 
          = additionalVariableChargePercent;

      return retVal;
    }

    /// <summary>
    /// Add a Ship Address For Calculated Shipping
    /// </summary>
    /// <param name="shipFromId">The ID of the Ship From</param>
    /// <param name="city">
    /// The &lt;city&gt; tag identifies the city associated with a
    /// shipping adrress
    /// </param>
    /// <param name="region">
    /// The &lt;region&gt; tag identifies the state or province associated
    /// with a shipping address.
    /// </param>
    /// <param name="postalCode">
    /// The &lt;postal-code&gt; tag identifies the zip code or postal code.
    /// </param>
    /// <param name="countryCode">
    /// This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.
    /// </param>
    /// <returns></returns>
    public ShippingPackage AddShippingPackage(string shipFromId, string city, string region, 
      string postalCode, string countryCode) {
      
      if (_packages.Count > 0) {
        throw new ArgumentException("At this time, only one ShipFrom" + 
          " address may be added.");   
      }

      shipFromId = eh.CleanString(shipFromId);
      city = eh.CleanString(city);
      region = eh.CleanString(region);
      postalCode = eh.CleanString(postalCode);
      countryCode = eh.CleanString(countryCode);

      if (shipFromId.Length == 0 || city.Length == 0
        || region.Length == 0 || postalCode.Length == 0
        || countryCode.Length == 0)
        throw new ArgumentNullException("All of the parameters are required.");

      ShippingPackage retVal = new ShippingPackage();

      retVal.ShippingLocation = new ShipFrom(shipFromId, city, region, 
        postalCode, countryCode);
      
      _packages.Add(retVal);

      Sync();

      return retVal;
    }

    /// <summary>
    /// Add a Ship Address For Calculated Shipping
    /// </summary>
    /// <param name="shipFromId">The ID of the Ship From</param>
    /// <param name="city">
    /// The &lt;city&gt; tag identifies the city associated with a
    /// shipping adrress
    /// </param>
    /// <param name="region">
    /// The &lt;region&gt; tag identifies the state or province associated
    /// with a shipping address.
    /// </param>
    /// <param name="postalCode">
    /// The &lt;postal-code&gt; tag identifies the zip code or postal code.
    /// </param>
    /// <param name="countryCode">
    /// This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.
    /// </param>
    /// <param name="addressCategory">
    /// The &lt;delivery-address-category&gt; tag indicates whether the shipping 
    /// method should be applied to a residential or a commercial address. Valid
    /// values for this tag are RESIDENTIAL and COMMERCIAL. Please note that 
    /// these names are case-sensitive.
    /// </param>
    /// <param name="height">The Height of the Package</param>
    /// <param name="length">The Length of the Package</param>
    /// <param name="width">The Width of the Package</param>
    /// <returns></returns>
    public ShippingPackage AddShippingPackage(string shipFromId, string city, string region, 
      string postalCode, string countryCode, DeliveryAddressCategory addressCategory,
      int height, int length, int width) {
    
      ShippingPackage retVal = AddShippingPackage(shipFromId, city, region, 
        postalCode, countryCode);

      retVal.AddressCategory = addressCategory;
      retVal.Height = height;
      retVal.Length = length;
      retVal.Width = width;
 
      return retVal;
    }

    private void Sync() {
      //now we want to add it to the node
      if (_shippingNode != null && _packages.Count > 0) {
        _shippingNode.shippingpackages 
          = new AutoGen.ShippingPackage[_packages.Count];

        for (int i = 0; i < _packages.Count; i++) {

          ShippingPackage package = _packages[i] as ShippingPackage;

          AutoGen.ShippingPackage sp = new GCheckout.AutoGen.ShippingPackage();
          
          _shippingNode.shippingpackages[i] = sp;
          
          if (package.AddressCategory != DeliveryAddressCategory.UNKNOWN)
            sp.deliveryaddresscategory = package.AddressCategory.ToString();

          if (package.Height > 0)
            sp.height = GetDimension(package.Height);
          if (package.Length > 0)
            sp.length = GetDimension(package.Length);
          
          ShipFrom sf = package.ShippingLocation;
          sp.shipfrom = new GCheckout.AutoGen.AnonymousAddress();
          sp.shipfrom.city = sf.City;
          sp.shipfrom.countrycode = sf.CountryCode;
          sp.shipfrom.id = sf.ID;
          sp.shipfrom.postalcode = sf.PostalCode;
          sp.shipfrom.region = sf.Region;

          if (package.Width > 0)
            sp.width = GetDimension(package.Width);
        }
      }
    }

    private AutoGen.Dimension GetDimension(int size) {
      AutoGen.Dimension retVal = new AutoGen.Dimension();
      retVal.unit = "IN";
      retVal.value = size;
      return retVal;
    }

  }
}
