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

namespace GCheckout.Checkout {
  /// <summary>
  /// You can also use the shipping-restrictions element to specify areas where a 
  /// flat-rate or merchant-calculated shipping option is (or is not) available. 
  /// Shipping options can be restricted to particular zip codes or zip code ranges, 
  /// to particular states or to particular areas of the United States.
  /// </summary>
  public class ShippingRestrictions {
    private AutoGen.ShippingRestrictions _Restrictions;
    private bool _allowUSPoBoxes = true;

    /// <summary>
    /// The &lt;allow-us-po-box&gt; tag indicates whether a particular
    /// shipping method can be used toship an order to a U.S. post 
    /// office (P.O.) box.
    /// </summary>
    /// <remarks>The Default is true</remarks>
    public bool AllowUSPOBoxes {
      get {
        return _allowUSPoBoxes;
      }
      set {
        _allowUSPoBoxes = value;
        _Restrictions.allowuspobox = value;
        _Restrictions.allowuspoboxSpecified = true;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShippingRestrictions"/> 
    /// class.
    /// </summary>
    public ShippingRestrictions() {
      _Restrictions = new AutoGen.ShippingRestrictions();
      _Restrictions.allowedareas = 
        new AutoGen.ShippingRestrictionsAllowedareas();
      _Restrictions.allowedareas.Items = new object[0];
      _Restrictions.excludedareas = 
        new AutoGen.ShippingRestrictionsExcludedareas();
      _Restrictions.excludedareas.Items = new object[0];
    }

    /// <summary>
    /// This tag represents the entire world. This tag has no attributes
    /// and does not contain anyother elements.
    /// </summary>
    /// <remarks>you can use the &lt;world-area&gt; tag to indicate that a shipping 
    /// option is available worldwide and then identify specific excluded 
    /// areas where the shipping option is unavailable. Those excluded areas 
    /// could identify regions that are covered by other
    /// shipping options or regions where you do not ship items.</remarks>
    public void AddAllowedWorldArea() {
      AddNewAllowedArea(new AutoGen.WorldArea());
    }

    /// <summary>You can use the &lt;postal-area&gt; tag to identify any country 
    /// in the world, including the United States. You can also specify 
    /// regions by postal codes. 
    /// </summary>
    /// <param name="countryCode">This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    public void AddAllowedPostalArea(string countryCode) {
      AddAllowedPostalArea(countryCode, null);
    }

    /// <summary>You can use the &lt;postal-area&gt; tag to identify any country 
    /// in the world, including the United States. You can also specify 
    /// regions by postal codes. 
    /// </summary>
    /// <param name="countryCode">This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    /// <param name="postalCodePattern">This tag identifies a postal code or 
    /// a range of postal codes for the postal area. To specify a range of 
    /// postal codes, use an asterisk as a wildcard operator in the tag's value. 
    /// For example, you can specify that a shipping option is available for all 
    /// postal codes beginning with "SW" by entering SW* as the 
    /// &lt;postal-code-pattern&gt; value.</param>
    public void AddAllowedPostalArea(string countryCode, string postalCodePattern) {
      AutoGen.PostalArea area = new GCheckout.AutoGen.PostalArea();
      area.countrycode = countryCode;
      if (postalCodePattern != null && postalCodePattern.Length > 0)
        area.postalcodepattern = postalCodePattern;
      AddNewAllowedArea(area);
    }

    /// <summary>
    /// This method adds an allowed zip code pattern to a 
    /// &lt;us-zip-area&gt; element. The &lt;us-zip-area&gt; element, 
    /// in turn, appears as a subelement of &lt;allowed-areas&gt;.
    /// </summary>
    /// <param name="ZipPattern">The zip pattern.</param>
    public void AddAllowedZipPattern(string ZipPattern) {
      AutoGen.USZipArea NewArea = new AutoGen.USZipArea();
      NewArea.zippattern = ZipPattern;
      AddNewAllowedArea(NewArea);
    }

    /// <summary>
    /// This method adds an allowed U.S. state code to a 
    /// &lt;us-state-area&gt; element. The &lt;us-state-area&gt; element, 
    /// in turn, appears as a subelement of &lt;allowed-areas&gt;.
    /// </summary>
    /// <param name="StateCode">The state code.</param>
    public void AddAllowedStateCode(string StateCode) {
      AutoGen.USStateArea NewArea = new AutoGen.USStateArea();
      NewArea.state = StateCode;
      AddNewAllowedArea(NewArea);
    }

    /// <summary>
    /// This method adds an allowed U.S. country area to a 
    /// &lt;us-country-area&gt; element. The &lt;us-country-area&gt; element, 
    /// in turn, appears as a subelement of &lt;allowed-areas&gt;.
    /// </summary>
    /// <param name="CountryArea">The country area.</param>
    public void AddAllowedCountryArea(AutoGen.USAreas CountryArea) {
      AutoGen.USCountryArea NewArea = new AutoGen.USCountryArea();
      NewArea.countryarea = CountryArea;
      AddNewAllowedArea(NewArea);
    }

    /// <summary>
    /// This method adds an allowed zip code area, state area or country area 
    /// to a Checkout API request.
    /// </summary>
    /// <param name="NewArea">The new area.</param>
    private void AddNewAllowedArea(object NewArea) {
      object[] NewAllowedAreas = 
        new object[_Restrictions.allowedareas.Items.Length + 1];
      for (int i = 0; i < _Restrictions.allowedareas.Items.Length; i++) {
        NewAllowedAreas[i] = _Restrictions.allowedareas.Items[i];
      }
      NewAllowedAreas[NewAllowedAreas.Length - 1] = NewArea;
      _Restrictions.allowedareas.Items = NewAllowedAreas;
    }


    /// <summary>You can use the &lt;postal-area&gt; tag to identify any country 
    /// in the world, including the United States. You can also specify 
    /// regions by postal codes. 
    /// </summary>
    /// <param name="countryCode">This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    public void AddExcludedPostalArea(string countryCode) {
      AddExcludedPostalArea(countryCode, null);
    }

    /// <summary>You can use the &lt;postal-area&gt; tag to identify any country 
    /// in the world, including the United States. You can also specify 
    /// regions by postal codes. 
    /// </summary>
    /// <param name="countryCode">This tag contains the two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    /// <param name="postalCodePattern">This tag identifies a postal code or 
    /// a range of postal codes for the postal area. To specify a range of 
    /// postal codes, use an asterisk as a wildcard operator in the tag's value. 
    /// For example, you can specify that a shipping option is available for all 
    /// postal codes beginning with "SW" by entering SW* as the 
    /// &lt;postal-code-pattern&gt; value.</param>
    public void AddExcludedPostalArea(string countryCode, string postalCodePattern) {
      AutoGen.PostalArea area = new GCheckout.AutoGen.PostalArea();
      area.countrycode = countryCode;
      if (postalCodePattern != null && postalCodePattern.Length > 0)
        area.postalcodepattern = postalCodePattern;
      AddNewExcludedArea(area);
    }


    /// <summary>
    /// This method adds an excluded zip code pattern to a 
    /// &lt;us-zip-area&gt; element. The &lt;us-zip-area&gt; element, 
    /// in turn, appears as a subelement of &lt;excluded-areas&gt;.
    /// </summary>
    /// <param name="ZipPattern">The zip pattern.</param>
    public void AddExcludedZipPattern(string ZipPattern) {
      AutoGen.USZipArea NewArea = new AutoGen.USZipArea();
      NewArea.zippattern = ZipPattern;
      AddNewExcludedArea(NewArea);
    }

    /// <summary>
    /// This method adds an excluded U.S. state code to a 
    /// &lt;us-state-area&gt; element. The &lt;us-state-area&gt; element, 
    /// in turn, appears as a subelement of &lt;excluded-areas&gt;.
    /// </summary>
    /// <param name="StateCode">The state code.</param>
    public void AddExcludedStateCode(string StateCode) {
      AutoGen.USStateArea NewArea = new AutoGen.USStateArea();
      NewArea.state = StateCode;
      AddNewExcludedArea(NewArea);
    }

    /// <summary>
    /// This method adds an excluded U.S. country area to a 
    /// &lt;us-country-area&gt; element. The &lt;us-country-area&gt; element, 
    /// in turn, appears as a subelement of &lt;excluded-areas&gt;.
    /// </summary>
    /// <param name="CountryArea">The country area.</param>
    public void AddExcludedCountryArea(AutoGen.USAreas CountryArea) {
      AutoGen.USCountryArea NewArea = new AutoGen.USCountryArea();
      NewArea.countryarea = CountryArea;
      AddNewExcludedArea(NewArea);
    }

    /// <summary>
    /// This method adds an excluded zip code area, state area or country area 
    /// to a Checkout API request.
    /// </summary>
    /// <param name="NewArea">The new area.</param>
    private void AddNewExcludedArea(object NewArea) {
      object[] NewExcludedAreas = 
        new object[_Restrictions.excludedareas.Items.Length + 1];
      for (int i = 0; i < _Restrictions.excludedareas.Items.Length; i++) {
        NewExcludedAreas[i] = _Restrictions.excludedareas.Items[i];
      }
      NewExcludedAreas[NewExcludedAreas.Length - 1] = NewArea;
      _Restrictions.excludedareas.Items = NewExcludedAreas;
    }

    /// <summary>
    /// This method returns the complete set of shipping restrictions for 
    /// a Checkout API request.
    /// </summary>
    /// <value>The XML restrictions.</value>
    public AutoGen.ShippingRestrictions XmlRestrictions {
      get {
        return _Restrictions;
      }
    }

  }
}
