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

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;ship-from&gt; tag contains information about the location
  /// from which an order will be shipped.
  /// </summary>
  public class ShipFrom {
    
    private string _id;
    private string _city;
    private string _region;
    private string _postalCode;
    private string _countryCode;

    /// <summary>
    /// The Id of the ShipFrom Address
    /// </summary>
    public string ID {
      get {
        return _id; 
      }
    }

    /// <summary>
    /// The &lt;city&gt; tag identifies the city associated with a
    /// shipping adrress
    /// </summary>
    public string City {
      get {
        return _city;    
      }
      set {
        _city = value;
      }
    }

    /// <summary>
    /// The &lt;region&gt; tag identifies the state or province associated
    /// with a shipping address.
    /// </summary>
    public string Region {
      get {
        return _region;
      }
      set {
        _region = value;
      }
    }

    /// <summary>
    /// The &lt;postal-code&gt; tag identifies the zip code or postal code.
    /// </summary>
    public string PostalCode {
      get {
        return _postalCode;
      }
      set {
        _postalCode = value;
      }
    }

    /// <summary>
    /// This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.
    /// </summary>
    public string CountryCode {
      get {
        return _countryCode;
      }
      set {
        _countryCode= value;
      }
    }
    
    /// <summary>
    /// Create a new instance of ShipFrom
    /// </summary>
    /// <param name="id">The ID of the Ship From</param>
    public ShipFrom(string id) {
      _id = id;
    }

    /// <summary>
    /// Create a new instance of ShipFrom
    /// </summary>
    /// <param name="id">The ID of the Ship From</param>
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
    public ShipFrom(string id, string city, string region, 
      string postalCode, string countryCode) {
      _id = id;
      City = city;
      Region = region;
      PostalCode = postalCode;
      CountryCode = countryCode;
    }

  }
}
