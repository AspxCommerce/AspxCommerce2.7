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
using System.Collections;
using eh = GCheckout.Util.EncodeHelper;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;shipping-package&gt; tag encapsulates information about
  /// an individual package that will be shipped to the buyer.
  /// </summary>
  public class ShippingPackage {

    private DeliveryAddressCategory _addressCategory 
      = DeliveryAddressCategory.UNKNOWN;

    private ShipFrom _shipFrom;

    private int _height;
    private int _length;
    private int _width;

    /// <summary>
    /// The Height of the Package.
    /// </summary>
    public int Height {
      get {
        return _height;
      }
      set {
        _height = value;
      }
    }

    /// <summary>
    /// The Length of the Package
    /// </summary>
    public int Length {
      get {
        return _length;
      }
      set {
        _length = value;
      }
    }

    /// <summary>
    /// The Width of the Package
    /// </summary>
    public int Width {
      get {
        return _width;
      }
      set {
        _width = value;
      }
    }

    /// <summary>
    /// The &lt;delivery-address-category&gt; tag indicates whether the shipping 
    /// method should be applied to a residential or a commercial address. Valid
    /// values for this tag are RESIDENTIAL and COMMERCIAL. Please note that 
    /// these names are case-sensitive.
    /// </summary>
    public DeliveryAddressCategory AddressCategory {
      get {
        return _addressCategory;
      }
      set {
        _addressCategory =value;
      }
    }

    /// <summary>
    /// The &lt;ship-from&gt; tag contains information about the location
    /// from which an order will be shipped.
    /// </summary>
    public ShipFrom ShippingLocation {
      get {
        return _shipFrom;
      }
      set {
        _shipFrom = value;
      }
    }

    /// <summary>
    /// Create a new Shipping Package.
    /// </summary>
    public ShippingPackage() {

    }
  }
}
