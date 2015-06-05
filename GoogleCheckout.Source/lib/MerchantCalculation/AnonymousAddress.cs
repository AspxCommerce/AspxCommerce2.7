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

namespace GCheckout.MerchantCalculation {
  /// <summary>
  /// This class creates an object that identifies the customer's shipping 
  /// address. Your class that inherits from CallbackRules.cs will receive an 
  /// object of this type that identifies the customer's shipping address.
  /// </summary>
  public class AnonymousAddress {
    private string _City;
    private string _CountryCode;
    private string _Id;
    private string _PostalCode;
    private string _Region;

    /// <summary>
    /// Create a new <see cref="AnonymousAddress"/>
    /// </summary>
    /// <param name="ThisAddress">The <see cref="AutoGen.AnonymousAddress"/>
    /// used to create the object.</param>
    public AnonymousAddress(AutoGen.AnonymousAddress ThisAddress) {
      _City = ThisAddress.city;
      _CountryCode = ThisAddress.countrycode;
      _Id = ThisAddress.id;
      _PostalCode = ThisAddress.postalcode;
      _Region = ThisAddress.region;
    }

    /// <summary>
    /// The city to which an order is being shipped.
    /// </summary>
    public string City {
      get {
        return _City;
      }
    }

    /// <summary>
    /// Identifies the country code associate with an order's billing
    /// address or shipping address. The value of this tag must be a 
    /// two-letter
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code.
    /// </summary>
    public string CountryCode {
      get {
        return _CountryCode;
      }
    }

    /// <summary>
    /// The id attribute specifies a unique that identifies
    ///  a particular address.
    /// </summary>
    public string Id {
      get {
        return _Id;
      }
    }
  
    /// <summary>
    /// Identifies the zip code or postal code associated with 
    /// either a billing address or a shipping address
    /// </summary>
    public string PostalCode {
      get {
        return _PostalCode;
      }
    }
  
    /// <summary>
    /// Identifies the state or province associated with a billing 
    /// address or shipping address.
    /// </summary>
    public string Region {
      get {
        return _Region;
      }
    }

  }
}
