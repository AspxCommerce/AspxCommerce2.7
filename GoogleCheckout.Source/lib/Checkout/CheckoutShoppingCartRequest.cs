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
 *  August 2008   Joe Feser joe.feser@joefeser.com
 *  ParseReponse Added.
 *  3-14-2009   Joe Feser joe.feser@joefeser.com
 *  We no longer allow people to pass in fractional amounts. All numbers are trimmed to $x.xx
 *  Any method that forwards the request to a new method or creates an object assumes that
 *  the object will perform it's own validation.
 *  4-21-2011   Joe Feser joe.feser@joefeser.com
 *  Fixed a bug in Tax tables where the rateSpecified was not being set
 *  Removed array copy code since we now have ToArray()
*/
using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using GCheckout.Util;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace GCheckout.Checkout {

  /// <summary>
  /// Class used to create the structure needed by Google Checkout
  /// </summary>
  /// <remarks>
  /// The class also has the ability to send that request to Google or return
  /// the Xml needed to place in the hidden form fields.
  /// </remarks>
  public class CheckoutShoppingCartRequest : GCheckoutRequest {

    internal const string MERCHANT_DATA_HIDDEN = "MERCHANT_DATA_HIDDEN";
    internal const string ZIP_CODE_PATTERN_EXCEPTION =
      "Zip code patterns must be five " +
      "numeric characters, or zero to 4 numeric characters followed by " +
      "a single asterisk as a wildcard character.";

    private BuyerMessages _buyerMessages = new BuyerMessages();
    private List<IShoppingCartItem> _items = new List<IShoppingCartItem>();
    private AutoGen.TaxTables _TaxTables;
    private AutoGen.MerchantCheckoutFlowSupportShippingmethods
      _ShippingMethods;
    private string _MerchantPrivateData = null;
    private ArrayList _MerchantPrivateDataNodes = new ArrayList();
    private bool _AcceptMerchantCoupons = false;
    private bool _AcceptMerchantGiftCertificates = false;
    private string _MerchantCalculationsUrl = null;
    private string _ContinueShoppingUrl = null;
    private string _EditCartUrl = null;
    private bool _RequestBuyerPhoneNumber = false;
    private DateTime _CartExpiration = DateTime.MinValue;
    internal string _Currency = null;
    private string _AnalyticsData = null;
    private long _PlatformID = 0;
    private bool _roundingRuleSet = false;
    private RoundingMode _roundingMode = RoundingMode.HALF_EVEN;
    private RoundingRule _roundingRule = RoundingRule.TOTAL;
    private bool _RequestInitialAuthDetails = false;
    private bool _IsDonation = false;

    private ParameterizedUrls _ParameterizedUrls = new ParameterizedUrls();

    //jf Tax Tables added 3/1/2007
    private AlternateTaxTableCollection _alternateTaxTables
      = new AlternateTaxTableCollection();
    //carrier calculated shipping options
    private CarrierCalculatedShipping _carrierCalculatedShipping = null;

    /// <summary>
    /// This method is called by the <see cref="GCheckoutButton"/> class and
    /// initializes a new instance of the 
    /// <see cref="CheckoutShoppingCartRequest"/> class.
    /// </summary>
    /// <param name="MerchantID">The Google Checkout merchant ID assigned
    /// to a particular merchant.</param>
    /// <param name="MerchantKey">The Google Checkout merchant key assigned
    /// to a particular merchant.</param>
    /// <param name="Env">The environment where a request is being executed. 
    /// Valid values for this parameter are "Sandbox" and "Production".</param>
    /// <param name="Currency">The currency associated with prices in a 
    /// Checkout API request.</param>
    /// <param name="CartExpirationMinutes">
    /// The length of time, in minutes, after which the shopping cart will 
    /// expire if it has not been submitted. A value of <b>0</b> indicates 
    /// the cart does not expire.
    /// </param>
    public CheckoutShoppingCartRequest(string MerchantID, string MerchantKey,
      EnvironmentType Env, string Currency, int CartExpirationMinutes) :
      this(MerchantID, MerchantKey, Env, Currency, CartExpirationMinutes,
      false) {
    }

    /// <summary>
    /// This method is called by the <see cref="GCheckoutButton"/> class and
    /// initializes a new instance of the 
    /// <see cref="CheckoutShoppingCartRequest"/> class.
    /// </summary>
    /// <param name="MerchantID">The Google Checkout merchant ID assigned
    /// to a particular merchant.</param>
    /// <param name="MerchantKey">The Google Checkout merchant key assigned
    /// to a particular merchant.</param>
    /// <param name="Env">The environment where a request is being executed. 
    /// Valid values for this parameter are "Sandbox" and "Production".</param>
    /// <param name="Currency">The currency associated with prices in a 
    /// Checkout API request.</param>
    /// <param name="CartExpirationMinutes">
    /// The length of time, in minutes, after which the shopping cart will 
    /// expire if it has not been submitted. A value of <b>0</b> indicates 
    /// the cart does not expire.
    /// </param>
    /// <param name="IsDonation">
    /// True if this is a donation to a non-profit, false if it is a regular 
    /// purchase. The Checkout pages have slightly different wording for 
    /// donations.
    /// </param>
    public CheckoutShoppingCartRequest(string MerchantID, string MerchantKey,
      EnvironmentType Env, string Currency, int CartExpirationMinutes,
      bool IsDonation) {
      _MerchantID = MerchantID;
      _MerchantKey = MerchantKey;
      _Environment = Env;
      _TaxTables = new AutoGen.TaxTables();
      _TaxTables.defaulttaxtable = new AutoGen.DefaultTaxTable();
      _TaxTables.defaulttaxtable.taxrules = new AutoGen.DefaultTaxRule[0];
      _ShippingMethods =
        new AutoGen.MerchantCheckoutFlowSupportShippingmethods();
      _ShippingMethods.Items = new Object[0];
      _Currency = Currency;
      if (CartExpirationMinutes > 0) {
        SetExpirationMinutesFromNow(CartExpirationMinutes);
      }
      _IsDonation = IsDonation;
      _carrierCalculatedShipping = new CarrierCalculatedShipping(this);
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity) {
      ShoppingCartItem retVal = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="subscription">The subscription item</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      int Quantity, Subscription subscription) {
      ShoppingCartItem retVal = new ShoppingCartItem(Name, Description,
        string.Empty, 0, Quantity);
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity, DigitalItem digitalItem) {
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    /// <param name="subscription">The subscription item</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      int Quantity, DigitalItem digitalItem, Subscription subscription) {
      ShoppingCartItem retVal = new ShoppingCartItem(Name, Description,
        string.Empty, 0, Quantity);
      retVal.DigitalContent = digitalItem;
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      decimal Price, int Quantity, AlternateTaxTable taxTable) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, Price,
        Quantity, taxTable);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="subscription">The subscription item</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      int Quantity, AlternateTaxTable taxTable, Subscription subscription) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, 0,
        Quantity, taxTable);
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      decimal Price, int Quantity, AlternateTaxTable taxTable,
      DigitalItem digitalItem) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity, taxTable);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="subscription">The subscription item.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, int Quantity, Subscription subscription) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, 0,
        Quantity);
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, DigitalItem digitalItem) {
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    /// <param name="subscription">The subscription item</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, int Quantity, DigitalItem digitalItem,
      Subscription subscription) {
      ShoppingCartItem retVal = new ShoppingCartItem(Name, Description,
        MerchantItemID, 0, Quantity);
      retVal.DigitalContent = digitalItem;
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, decimal Price, int Quantity,
      AlternateTaxTable taxTable) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity, taxTable);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="subscription">The subscription item</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, int Quantity, AlternateTaxTable taxTable,
      Subscription subscription) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, 0,
        Quantity, taxTable);
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// do not have &lt;merchant-private-item-data&gt; XML blocks associated 
    /// with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, decimal Price, int Quantity,
      AlternateTaxTable taxTable, DigitalItem digitalItem) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity, taxTable);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemData">An XML block that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity, string MerchantPrivateItemData) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, Price,
        Quantity, AlternateTaxTable.Empty, MakeXmlElement(MerchantPrivateItemData));
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemData">An XML block that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="subscription">The subscription information.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      int Quantity, string MerchantPrivateItemData, Subscription subscription) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, 0,
        Quantity, AlternateTaxTable.Empty, MakeXmlElement(MerchantPrivateItemData));
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity, XmlNode MerchantPrivateItemDataNodes) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, Price,
        Quantity, AlternateTaxTable.Empty, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity, XmlNode MerchantPrivateItemDataNodes, DigitalItem digitalItem) {

      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity, AlternateTaxTable.Empty,
        MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      AlternateTaxTable taxTable) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, Price,
        Quantity, taxTable, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      AlternateTaxTable taxTable, DigitalItem digitalItem) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity, taxTable, MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity, AlternateTaxTable.Empty, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="subscription">The subscription item.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      Subscription subscription) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, 0,
        Quantity, AlternateTaxTable.Empty, MerchantPrivateItemDataNodes);
      retVal.Subscription = subscription;
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      DigitalItem digitalItem) {
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity, AlternateTaxTable.Empty,
        MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      AlternateTaxTable taxTable) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity, taxTable, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, XmlNode MerchantPrivateItemDataNodes,
      AlternateTaxTable taxTable, DigitalItem digitalItem) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity, taxTable, MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node array that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, AlternateTaxTable taxTable,
      params XmlNode[] MerchantPrivateItemDataNodes) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity, taxTable, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An XML node array that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, AlternateTaxTable taxTable,
      DigitalItem digitalItem, params XmlNode[] MerchantPrivateItemDataNodes) {
      _alternateTaxTables.VerifyTaxRule(taxTable);
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity, taxTable, MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An array of XML nodes
    /// that should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      decimal Price,
      int Quantity, params XmlNode[] MerchantPrivateItemDataNodes) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, string.Empty, Price,
        Quantity, AlternateTaxTable.Empty, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An array of XML nodes that
    /// should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description, decimal Price,
      int Quantity, DigitalItem digitalItem,
      params XmlNode[] MerchantPrivateItemDataNodes) {
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        string.Empty, Price, Quantity, AlternateTaxTable.Empty,
        MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely 
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An array of XML nodes that
    /// should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, params XmlNode[] MerchantPrivateItemDataNodes) {
      ShoppingCartItem retVal
        = new ShoppingCartItem(Name, Description, MerchantItemID, Price,
        Quantity, AlternateTaxTable.Empty, MerchantPrivateItemDataNodes);
      _items.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// This method adds an item to an order. This method handles items that 
    /// have &lt;merchant-private-item-data&gt; XML blocks associated with them.
    /// </summary>
    /// <param name="Name">The name of the item. This value corresponds to the 
    /// value of the &lt;item-name&gt; tag in the Checkout API request.</param>
    /// <param name="Description">The description of the item. This value 
    /// corresponds to the value of the &lt;item-description&gt; tag in the 
    /// Checkout API request.</param>
    /// <param name="MerchantItemID">The Merchant Item Id that uniquely 
    /// identifies the product in your system.</param>
    /// <param name="Price">The price of the item. This value corresponds to 
    /// the value of the &lt;unit-price&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="Quantity">The number of this item that is included in the 
    /// order. This value corresponds to the value of the &lt;quantity&gt; tag 
    /// in the Checkout API request.</param>
    /// <param name="MerchantPrivateItemDataNodes">An array of XML nodes that
    /// should be 
    /// associated with the item in the Checkout API request. This value 
    /// corresponds to the value of the value of the 
    /// &lt;merchant-private-item-data&gt; tag in the Checkout API 
    /// request.</param>
    /// <param name="digitalItem">
    /// The &lt;digital-content&gt; tag contains information relating to
    /// digital delivery of an item.
    /// </param>
    public ShoppingCartItem AddItem(string Name, string Description,
      string MerchantItemID,
      decimal Price, int Quantity, DigitalItem digitalItem,
      params XmlNode[] MerchantPrivateItemDataNodes) {
      ShoppingCartItem item = new ShoppingCartItem(Name, Description,
        MerchantItemID, Price, Quantity, AlternateTaxTable.Empty,
        MerchantPrivateItemDataNodes);
      item.DigitalContent = digitalItem;
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order.
    /// </summary>
    /// <param name="item">The shopping cart item to add.</param>
    public ShoppingCartItem AddItem(ShoppingCartItem item) {
      if (item == null)
        throw new ArgumentNullException("item must not be null");
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds an item to an order.
    /// </summary>
    /// <param name="item">The shopping cart item to add.</param>
    public IShoppingCartItem AddItem(IShoppingCartItem item) {
      if (item == null)
        throw new ArgumentNullException("item must not be null");
      _items.Add(item);
      return item;
    }

    /// <summary>
    /// This method adds a flat-rate shipping method to an order. This method 
    /// handles flat-rate shipping methods that do not have shipping 
    /// restrictions.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="Cost">The cost associated with the shipping method.</param>
    public void AddFlatRateShippingMethod(string Name, decimal Cost) {
      Cost = Math.Round(Cost, 2); //no fractional cents
      AddFlatRateShippingMethod(Name, Cost, null);
    }

    /// <summary>
    /// This method adds a flat-rate shipping method to an order. This method 
    /// handles flat-rate shipping methods that have shipping restrictions.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="Cost">The cost associated with the shipping method.</param>
    /// <param name="Restrictions">A list of country, state or zip code areas 
    /// where the shipping method is either available or unavailable.</param>
    public void AddFlatRateShippingMethod(string Name, decimal Cost,
      ShippingRestrictions Restrictions) {
      Cost = Math.Round(Cost, 2); //no fractional cents
      AutoGen.FlatRateShipping Method = new AutoGen.FlatRateShipping();
      Method.name = Name;
      Method.price = new AutoGen.FlatRateShippingPrice();
      Method.price.currency = _Currency;
      Method.price.Value = Cost;
      if (Restrictions != null) {
        Method.shippingrestrictions = Restrictions.XmlRestrictions;
      }
      AddNewShippingMethod(Method);
    }

    /// <summary>
    /// This method adds a merchant-calculated shipping method to an order. 
    /// This method handles merchant-calculated shipping methods that do not 
    /// have shipping restrictions.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="DefaultCost">The default cost associated with the shipping 
    /// method. This value is the amount that Gogle Checkout will charge for 
    /// shipping if the merchant calculation callback request fails.</param>
    public void AddMerchantCalculatedShippingMethod(string Name,
      decimal DefaultCost) {
      AddMerchantCalculatedShippingMethod(Name, DefaultCost, null);
    }

    /// <summary>
    /// This method adds a merchant-calculated shipping method to an order. 
    /// This method handles merchant-calculated shipping methods that have 
    /// shipping restrictions.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="DefaultCost">The default cost associated with the shipping 
    /// method. This value is the amount that Gogle Checkout will charge for 
    /// shipping if the merchant calculation callback request fails.</param>
    /// <param name="Restrictions">A list of country, state or zip code areas 
    /// where the shipping method is either available or unavailable.</param>
    public void AddMerchantCalculatedShippingMethod(string Name,
      decimal DefaultCost, ShippingRestrictions Restrictions) {

      AddMerchantCalculatedShippingMethod(Name, DefaultCost, Restrictions, null);
    }

    /// <summary>
    /// This method adds a merchant-calculated shipping method to an order. 
    /// This method handles merchant-calculated shipping methods that have 
    /// shipping restrictions.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="DefaultCost">The default cost associated with the shipping 
    /// method. This value is the amount that Gogle Checkout will charge for 
    /// shipping if the merchant calculation callback request fails.</param>
    /// <param name="Restrictions">A list of country, state or zip code areas 
    /// where the shipping method is either available or unavailable.</param>
    /// <param name="AddressFilters">enables you to specify a geographic area 
    /// where a particular merchant-calculated shipping method is available or
    /// unavailable. Google Checkout applies address filters before sending you
    /// a &lt;merchantcalculation-callback&gt; request so that you are not asked
    /// to calculate shipping costs for a shipping method that is not actually 
    /// available.</param>
    public void AddMerchantCalculatedShippingMethod(string Name,
      decimal DefaultCost, ShippingRestrictions Restrictions,
      ShippingRestrictions AddressFilters) {

      DefaultCost = Math.Round(DefaultCost, 2); //no fractional cents

      AutoGen.MerchantCalculatedShipping Method =
        new AutoGen.MerchantCalculatedShipping();
      Method.name = Name;
      Method.price = new AutoGen.MerchantCalculatedShippingPrice();
      Method.price.currency = _Currency;
      Method.price.Value = DefaultCost;
      if (Restrictions != null) {
        Method.shippingrestrictions = Restrictions.XmlRestrictions;
      }
      if (AddressFilters != null) {
        Method.addressfilters = AddressFilters.XmlRestrictions;
      }
      AddNewShippingMethod(Method);
    }

    /// <summary>
    /// This method adds an instore-pickup shipping option to an order.
    /// </summary>
    /// <param name="Name">The name of the shipping method. This value will be 
    /// displayed on the Google Checkout order review page.</param>
    /// <param name="Cost">The cost associated with the shipping method.</param>
    public void AddPickupShippingMethod(string Name, decimal Cost) {

      Cost = Math.Round(Cost, 2); //no fractional cents

      AutoGen.Pickup Method = new AutoGen.Pickup();
      Method.name = Name;
      Method.price = new AutoGen.PickupPrice();
      Method.price.currency = _Currency;
      Method.price.Value = Cost;
      AddNewShippingMethod(Method);
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
    public CarrierCalculatedShippingOption
      AddCarrierCalculatedShippingOption(
        ShippingType shippingType, decimal defaultValue) {

      return _carrierCalculatedShipping.AddShippingOption(
        shippingType, defaultValue);
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
    public CarrierCalculatedShippingOption
      AddCarrierCalculatedShippingOption(
        ShippingType shippingType, decimal defaultValue,
        CarrierPickup carrierPickup, decimal additionalFixedCharge,
      double additionalVariableChargePercent) {

      return _carrierCalculatedShipping.AddShippingOption(shippingType,
        defaultValue, carrierPickup, additionalFixedCharge,
        additionalVariableChargePercent);
    }

    /// <summary>
    /// Add a Ship Address For Calculated Shipping
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
    /// <returns></returns>
    public ShippingPackage AddShippingPackage(string id, string city,
      string region, string postalCode) {
      return _carrierCalculatedShipping.AddShippingPackage(id, city, region,
        postalCode, "US");
    }

    /// <summary>
    /// Add a Ship Address For Calculated Shipping
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
    public ShippingPackage AddShippingPackage(string id, string city,
      string region, string postalCode,
      DeliveryAddressCategory addressCategory,
      int height, int length, int width) {
      return _carrierCalculatedShipping.AddShippingPackage(id, city, region,
        postalCode, "US", addressCategory, height, length, width);
    }

    /// <summary>
    /// Adds the new shipping method.
    /// </summary>
    /// <param name="NewShippingMethod">The new shipping method.</param>
    internal void AddNewShippingMethod(Object NewShippingMethod) {

      VerifyShippingMethods(NewShippingMethod);

      Object[] NewMethods = new Object[_ShippingMethods.Items.Length + 1];
      for (int i = 0; i < _ShippingMethods.Items.Length; i++) {
        NewMethods[i] = _ShippingMethods.Items[i];
      }

      NewMethods[NewMethods.Length - 1] = NewShippingMethod;
      _ShippingMethods.Items = NewMethods;
    }

    internal void VerifyShippingMethods(Object newShippingMethod) {
      //ensure the type is the only type that exists in the set
      //shipping methods is an xs:choice so only one type can exist.
      if (_ShippingMethods.Items.Length > 0) {
        //since we have been validating the entire time, all we need to do
        //is grab the first item and ensure the class types are ==
        Type originalType = _ShippingMethods.Items[0].GetType();
        Type newType = newShippingMethod.GetType();
        if (originalType != newType) {

          //change of rules. if it is carrier calculated it can be
          //combined with flat rate options.
          //if the first item is flat or calc
          //and aslo the new one is flat or carrier calc
          //then the combination is allowed, just return.
          if ((originalType == typeof(AutoGen.FlatRateShipping)
            || originalType == typeof(AutoGen.CarrierCalculatedShipping))
            &&
            (newType == typeof(AutoGen.FlatRateShipping)
            || newType == typeof(AutoGen.CarrierCalculatedShipping))) {
            return;
          }

          string theError = "You may only have one Shipping method." +
            " The cart already contains a {0} Shipping method and you" +
            " attempted to add a {1} Shipping method.";
          throw new ArgumentException(string.Format(theError, originalType,
            newShippingMethod.GetType()));
        }
      }
    }

    /// <summary>
    /// This method adds a tax rule associated with a zip code pattern.
    /// </summary>
    /// <param name="ZipPattern">The zip pattern.</param>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax rates 
    /// are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    public void AddZipTaxRule(string ZipPattern, double TaxRate,
      bool ShippingTaxed) {
      if (!IsValidZipPattern(ZipPattern)) {
        throw new ApplicationException(ZIP_CODE_PATTERN_EXCEPTION);
      }
      AutoGen.DefaultTaxRule Rule = new AutoGen.DefaultTaxRule();
      Rule.rateSpecified = true;
      Rule.rate = TaxRate;
      Rule.shippingtaxedSpecified = true;
      Rule.shippingtaxed = ShippingTaxed;
      Rule.taxarea = new AutoGen.DefaultTaxRuleTaxarea();
      AutoGen.USZipArea Area = new AutoGen.USZipArea();
      Rule.taxarea.Item = Area;
      Area.zippattern = ZipPattern;
      AddNewTaxRule(Rule);
    }

    /// <summary>
    /// Add an already <see cref="System.Web.HttpUtility.UrlEncode(string)"/> Url
    /// </summary>
    /// <param name="url">The UrlEncoded &lt;parameterized-url&gt; to add to the collection</param>
    public ParameterizedUrl AddParameterizedUrl(string url) {
      return AddParameterizedUrl(url, false);
    }

    /// <summary>
    /// Add a Third Party Tracking URL.
    /// </summary>
    /// <param name="url">The &lt;parameterized-url&gt; to add to the collection</param>
    /// <param name="urlEncode">true if you need the url to be <see cref="System.Web.HttpUtility.UrlEncode(string)"/></param>
    /// <returns>A new <see cref="ParameterizedUrl" /></returns>
    public ParameterizedUrl AddParameterizedUrl(string url, bool urlEncode) {
      ParameterizedUrl retVal = ParameterizedUrls.AddUrl(url, urlEncode);
      return retVal;
    }

    /// <summary>
    /// Add a Third Party Tracking URL.
    /// </summary>
    /// <param name="url">The &lt;parameterized-url&gt; object to add to the collection</param>
    /// <returns>A new <see cref="ParameterizedUrl" /></returns>
    public void AddParameterizedUrl(ParameterizedUrl url) {
      _ParameterizedUrls.AddUrl(url);
    }

    /// <summary>
    /// This method verifies that a given zip code pattern is valid. Zip code 
    /// patterns may be five-digit numbers or they may be one- to four-digit 
    /// numbers followed by an asterisk.
    /// </summary>
    /// <param name="ZipPattern">This parameter contains the zip code pattern 
    /// that is being evaluated.</param>
    /// <returns>
    ///   This method returns <b>true</b> if the provided zip code pattern
    ///   is valid, meaning it is either a series of five digits or it is 
    ///   a series of zero to four digits followed by an asterisk. If the 
    ///   zip code pattern is not valid, this method returns <b>false</b>.
    /// </returns>
    public static bool IsValidZipPattern(string ZipPattern) {
      Regex r = new Regex("^((\\d{0,4}\\*)|(\\d{5}))$");
      Match m = r.Match(ZipPattern);
      return m.Success;
    }

    /// <summary>
    /// This method adds a tax rule associated with a particular state.
    /// </summary>
    /// <param name="StateCode">This parameter contains a two-letter U.S. state 
    /// code associated with a tax rule.</param>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax 
    /// rates are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    public void AddStateTaxRule(string StateCode, double TaxRate,
      bool ShippingTaxed) {
      AutoGen.DefaultTaxRule Rule = new AutoGen.DefaultTaxRule();
      Rule.rateSpecified = true;
      Rule.rate = TaxRate;
      Rule.shippingtaxedSpecified = true;
      Rule.shippingtaxed = ShippingTaxed;
      Rule.taxarea = new AutoGen.DefaultTaxRuleTaxarea();
      AutoGen.USStateArea Area = new AutoGen.USStateArea();
      Rule.taxarea.Item = Area;
      Area.state = StateCode;
      AddNewTaxRule(Rule);
    }

    /// <summary>
    /// Adds the country tax rule.
    /// This method adds a tax rule associated with a particular state.
    /// </summary>
    /// <param name="Area">The area.</param>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax 
    /// rates are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    /// <example>
    /// <code>
    ///   // We assume Req is a CheckoutShoppingCartRequest object.
    ///   // Charge the 50 states 8% tax and do not tax shipping.
    ///   Req.AddCountryTaxRule(AutoGen.USAreas.FULL_50_STATES, 0.08, false);
    ///   // Charge the 48 continental states 5% tax and do tax shipping.
    ///   Req.AddCountryTaxRule(AutoGen.USAreas.CONTINENTAL_48, 0.05, true);
    ///   // Charge all states (incl territories) 9% tax, don't tax shipping.
    ///   Req.AddCountryTaxRule(AutoGen.USAreas.ALL, 0.09, false);
    /// </code>
    /// </example>
    public void AddCountryTaxRule(AutoGen.USAreas Area, double TaxRate,
      bool ShippingTaxed) {
      AutoGen.DefaultTaxRule Rule = new AutoGen.DefaultTaxRule();
      Rule.rateSpecified = true;
      Rule.rate = TaxRate;
      Rule.shippingtaxedSpecified = true;
      Rule.shippingtaxed = ShippingTaxed;
      Rule.taxarea = new AutoGen.DefaultTaxRuleTaxarea();
      AutoGen.USCountryArea ThisArea = new AutoGen.USCountryArea();
      Rule.taxarea.Item = ThisArea;
      ThisArea.countryarea = Area;
      AddNewTaxRule(Rule);
    }



    /// <summary>
    /// Adds the country tax rule.
    /// This method adds a tax rule associated with a particular state.
    /// </summary>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax 
    /// rates are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    public void AddWorldAreaTaxRule(double TaxRate, bool ShippingTaxed) {
      AutoGen.DefaultTaxRule Rule = new AutoGen.DefaultTaxRule();
      Rule.rateSpecified = true;
      Rule.rate = TaxRate;
      Rule.shippingtaxedSpecified = true;
      Rule.shippingtaxed = ShippingTaxed;
      Rule.taxarea = new AutoGen.DefaultTaxRuleTaxarea();
      AutoGen.WorldArea ThisArea = new AutoGen.WorldArea();
      Rule.taxarea.Item = ThisArea;
      AddNewTaxRule(Rule);
    }

    /// <summary>
    /// Adds the postal area tax rule.
    /// This method adds a tax rule associated with a particular postal area.
    /// </summary>
    /// <param name="countryCode">Required. This tag contains the 
    /// two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax 
    /// rates are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    public void AddPostalAreaTaxRule(string countryCode,
      double TaxRate, bool ShippingTaxed) {
      AddPostalAreaTaxRule(countryCode, string.Empty, TaxRate, ShippingTaxed);
    }

    /// <summary>
    /// Adds the country tax rule.
    /// This method adds a tax rule associated with a particular state.
    /// </summary>
    /// <param name="countryCode">Required. This tag contains the 
    /// two-letter 
    /// <a href="http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1.html">ISO 3166-1</a>
    /// country code for the postal area.</param>
    /// <param name="postalCodePattern">Optional. This tag identifies a postal
    ///  code or a range of postal codes for the postal area. To specify a 
    ///  range of postal codes, use an asterisk as a wildcard operator in the
    ///  tag's value. For example, you can specify that a shipping option is 
    ///  available for all postal codes beginning with "SW" by entering SW* 
    ///  as the &lt;postal-code-pattern&gt; value.</param>
    /// <param name="TaxRate">The tax rate associated with a tax rule. Tax 
    /// rates are expressed as decimal values. For example, a value of 0.0825 
    /// specifies a tax rate of 8.25%.</param>
    /// <param name="ShippingTaxed">
    /// If this parameter has a value of <b>true</b>, then shipping costs will
    /// be taxed for items that use the associated tax rule.
    /// </param>
    public void AddPostalAreaTaxRule(string countryCode, string postalCodePattern,
      double TaxRate, bool ShippingTaxed) {
      AutoGen.DefaultTaxRule Rule = new AutoGen.DefaultTaxRule();
      Rule.rateSpecified = true;
      Rule.rate = TaxRate;
      Rule.shippingtaxedSpecified = true;
      Rule.shippingtaxed = ShippingTaxed;
      Rule.taxarea = new AutoGen.DefaultTaxRuleTaxarea();
      AutoGen.PostalArea ThisArea = new AutoGen.PostalArea();
      Rule.taxarea.Item = ThisArea;
      ThisArea.countrycode = countryCode;
      if (postalCodePattern != null && postalCodePattern != string.Empty) {
        ThisArea.postalcodepattern = postalCodePattern;
      }
      AddNewTaxRule(Rule);
    }

    /// <summary>
    /// This method adds a new tax rule to the &lt;default-tax-table&gt;.
    /// This method is called by the methods that create the XML blocks
    /// for flat-rate shipping, merchant-calculated shipping and instore-pickup
    /// shipping methods.
    /// </summary>
    /// <param name="NewRule">This parameter contains an object representing
    /// a default tax rule.</param>
    private void AddNewTaxRule(AutoGen.DefaultTaxRule NewRule) {

      var rules = new List<AutoGen.DefaultTaxRule>();

      if (_TaxTables.defaulttaxtable.taxrules != null
        && _TaxTables.defaulttaxtable.taxrules.Length > 0) {
        rules.AddRange(_TaxTables.defaulttaxtable.taxrules);
      }

      rules.Add(NewRule);
      //Always create the array, even if it is empty.
      _TaxTables.defaulttaxtable.taxrules = rules.ToArray();
    }

    /// <summary>
    /// This property adds a XmlNode to the 
    /// &lt;merchant-private-data&gt; element.
    /// </summary>
    /// <value>The &lt;merchant-private-data&gt; element.</value>
    public void AddMerchantPrivateDataNode(XmlNode node) {
      if (node == null)
        throw new ArgumentNullException("node");

      _MerchantPrivateDataNodes.Add(node);
    }

    /// <summary>
    /// This method generates the XML for a Checkout API request. This method
    /// also calls the <b>CheckPreConditions</b> method, which verifies that
    /// if the API request indicates that the merchant will calculate tax and
    /// shipping costs, that the input data for those calculations is included
    /// in the request.
    /// </summary>
    /// <returns>This method returns the XML for a Checkout API request.
    /// </returns>
    public override byte[] GetXml() {

      // Verify that if the API request calls for merchant calculations, the 
      // input data for those calculations is included in the request.
      //
      CheckPreConditions();

      AutoGen.CheckoutShoppingCart retVal = new AutoGen.CheckoutShoppingCart();
      retVal.shoppingcart = new AutoGen.ShoppingCart();

      // Add the Shopping cart expiration element.
      if (CartExpiration != DateTime.MinValue) {
        retVal.shoppingcart.cartexpiration = new AutoGen.CartExpiration();
        retVal.shoppingcart.cartexpiration.gooduntildate = CartExpiration;
      }

      // Add the items in the shopping cart to the API request.
      retVal.shoppingcart.items = new AutoGen.Item[_items.Count];
      for (int i = 0; i < _items.Count; i++) {

        IShoppingCartItem MyItem = _items[i] as IShoppingCartItem;
        //this can't happen at this time but if we expose a way for people to add items
        //this test needs to be peformed.
        //        if (MyItem == null) {
        //          throw new ApplicationException(
        //            "The Shopping Cart Item did not implement IShoppingCartItem." +
        //            " This is caused by someone changing the implementation of" +
        //            " GCheckout.Checkout.ShoppingCartItem.");
        //        }

        //set to a local variable of current item to lessen the change of
        //a mistake
        retVal.shoppingcart.items[i] = ShoppingCartItem.CreateAutoGenItem(MyItem, _Currency);
      }

      //because we are merging the nodes, create a new ArrayList
      ArrayList copiedMerchantPrivateData = new ArrayList();

      // Add the &lt;merchant-private-data&gt; element to the API request.
      if (MerchantPrivateData != null) {
        copiedMerchantPrivateData.Add(MakeXmlElement(MerchantPrivateData));
      }

      if (_MerchantPrivateDataNodes != null
        && _MerchantPrivateDataNodes.Count > 0) {
        for (int i = 0; i < _MerchantPrivateDataNodes.Count; i++)
          copiedMerchantPrivateData.Add(_MerchantPrivateDataNodes[i]);
      }

      if (copiedMerchantPrivateData.Count > 0) {
        AutoGen.anyMultiple any = new AutoGen.anyMultiple();

        System.Xml.XmlNode[] nodes =
          new System.Xml.XmlNode[copiedMerchantPrivateData.Count];
        copiedMerchantPrivateData.CopyTo(nodes);
        any.Any = nodes;

        retVal.shoppingcart.merchantprivatedata = any;
      }

      // Add the &lt;continue-shopping-url&gt; element to the API request.
      retVal.checkoutflowsupport =
        new AutoGen.CheckoutShoppingCartCheckoutflowsupport();

      retVal.checkoutflowsupport.Item =
        new AutoGen.MerchantCheckoutFlowSupport();

      if (ContinueShoppingUrl != null) {
        retVal.checkoutflowsupport.Item.continueshoppingurl =
          ContinueShoppingUrl;
      }
      object[] buyerMessages = BuyerMessages.ConvertMessages();
      if (buyerMessages != null && buyerMessages.Length > 0) {
        retVal.shoppingcart.buyermessages = new GCheckout.AutoGen.ShoppingCartBuyermessages();
        retVal.shoppingcart.buyermessages.Items = buyerMessages;
      }

      if (AnalyticsData != null) {
        retVal.checkoutflowsupport.Item.analyticsdata = AnalyticsData;
      }

      if (PlatformID != 0) {
        retVal.checkoutflowsupport.Item.platformid = PlatformID;
        retVal.checkoutflowsupport.Item.platformidSpecified = true;
      }

      // Add the &lt;edit-cart-url&gt; element to the API request.
      if (EditCartUrl != null) {
        retVal.checkoutflowsupport.Item.editcarturl = EditCartUrl;
      }

      // Add the &lt;request-buyer-phone-number&gt; element to the API request.
      if (RequestBuyerPhoneNumber) {
        retVal.checkoutflowsupport.Item.requestbuyerphonenumber = true;
        retVal.checkoutflowsupport.Item.requestbuyerphonenumberSpecified =
          true;
      }

      // Add the shipping methods to the API request.
      // Fix potential issue.
      if (_ShippingMethods != null && _ShippingMethods.Items != null
        && _ShippingMethods.Items.Length > 0) {
        retVal.checkoutflowsupport.Item.shippingmethods = _ShippingMethods;
      }

      //jf Tax Tables added 3/1/2007
      if (_alternateTaxTables != null) {
        _alternateTaxTables.AppendToRequest(_TaxTables);
      }

      // Add the tax tables to the API request.
      //Fix for Issue 36
      //Only if we have a tax table do we want to append it.
      if (_TaxTables != null) {
        if (
          (_TaxTables.alternatetaxtables != null
          && _TaxTables.alternatetaxtables.Length > 0)
          ||
          (_TaxTables.defaulttaxtable != null
          && _TaxTables.defaulttaxtable.taxrules != null
          && _TaxTables.defaulttaxtable.taxrules.Length > 0)
          ) {
          retVal.checkoutflowsupport.Item.taxtables = _TaxTables;
        }
      }

      // Add the merchant calculations URL to the API request.
      if (MerchantCalculationsUrl != null) {
        retVal.checkoutflowsupport.Item.merchantcalculations =
          new AutoGen.MerchantCalculations();
        retVal.checkoutflowsupport.Item.merchantcalculations.
          merchantcalculationsurl = MerchantCalculationsUrl;
      }

      // Indicate whether the merchant accepts coupons and gift certificates.
      if (AcceptMerchantCoupons) {
        retVal.checkoutflowsupport.Item.merchantcalculations.
          acceptmerchantcoupons = true;
        retVal.checkoutflowsupport.Item.merchantcalculations.
          acceptmerchantcouponsSpecified = true;
      }
      if (AcceptMerchantGiftCertificates) {
        retVal.checkoutflowsupport.Item.merchantcalculations.
          acceptgiftcertificates = true;
        retVal.checkoutflowsupport.Item.merchantcalculations.
          acceptgiftcertificatesSpecified = true;
      }

      if (_roundingRuleSet) {
        retVal.checkoutflowsupport.Item.roundingpolicy =
          new GCheckout.AutoGen.RoundingPolicy();
        retVal.checkoutflowsupport.Item.roundingpolicy.mode
          = (AutoGen.RoundingMode)Enum.Parse(typeof(AutoGen.RoundingMode),
          _roundingMode.ToString(), true);
        retVal.checkoutflowsupport.Item.roundingpolicy.rule
          = (AutoGen.RoundingRule)Enum.Parse(typeof(AutoGen.RoundingRule),
          _roundingRule.ToString(), true);
      }

      if (RequestInitialAuthDetails) {
        retVal.orderprocessingsupport = new AutoGen.OrderProcessingSupport();
        retVal.orderprocessingsupport.requestinitialauthdetailsSpecified = true;
        retVal.orderprocessingsupport.requestinitialauthdetails = true;
      }

      //See if we have any ParameterizedUrl that need to be added to the message.
      if (_ParameterizedUrls.Urls.Length > 0) {
        GCheckout.AutoGen.ParameterizedUrl[] destUrls
          = new GCheckout.AutoGen.ParameterizedUrl[_ParameterizedUrls.Urls.Length];
        for (int i = 0; i < _ParameterizedUrls.Urls.Length; i++) {
          ParameterizedUrl fromUrl = _ParameterizedUrls.Urls[i];
          GCheckout.AutoGen.ParameterizedUrl toUrl
            = new GCheckout.AutoGen.ParameterizedUrl();
          toUrl.url = fromUrl.URL;

          //ok now we have to see if params exist, 
          //and if they do we need to look that array and build it out.
          if (fromUrl.Params.Length > 0) {
            GCheckout.AutoGen.UrlParameter[] destparams
              = new GCheckout.AutoGen.UrlParameter[fromUrl.Params.Length];
            for (int j = 0; j < fromUrl.Params.Length; j++) {
              UrlParamter fromParam = fromUrl.Params[j];
              GCheckout.AutoGen.UrlParameter toParam
              = new GCheckout.AutoGen.UrlParameter();
              toParam.name = fromParam.Name;
              //Use the reflection code to get the string value of the enum.
              toParam.type = fromParam.SerializedValue;
              destparams[j] = toParam;
            }
            toUrl.parameters = destparams;
          }
          destUrls[i] = toUrl; //add the url to the array.
          retVal.checkoutflowsupport.Item.parameterizedurls = destUrls;
        }
      }

      // Call the EncodeHelper.Serialize method to generate the XML for
      // the Checkout API request.
      return EncodeHelper.Serialize(retVal);
    }

    /// <summary>
    /// This method is used to perform several checks that verify the integrity
    /// of the information in a Checkout API XML request. This method will 
    /// throw an exception if the request does not pass any of these tests.
    /// </summary>
    private void CheckPreConditions() {
      // 1. If the request indicates that tax will be calculated by the 
      // merchant, the request must contain at least one default tax rule.
      if (_TaxTables.merchantcalculated &&
        _TaxTables.defaulttaxtable.taxrules.Length == 0) {
        throw new ApplicationException("If you set " +
          "MerchantCalculatedTax=true, you must add at least one tax rule.");
      }
      // 2. If the request indicates that tax will be calculated by the
      // merchant, the request must specify a merchant-calculations-url.
      if (_TaxTables.merchantcalculated && _MerchantCalculationsUrl == null) {
        throw new ApplicationException("If you set " +
          "MerchantCalculatedTax=true, you must set MerchantCalculationsUrl.");
      }
      // 3. If the request indicates that the merchant accepts coupons, the
      // request must also specify a merchant-calculations-url.
      if (_AcceptMerchantCoupons && _MerchantCalculationsUrl == null) {
        throw new ApplicationException("If you set " +
          "AcceptMerchantCoupons=true, you must set MerchantCalculationsUrl.");
      }
      // 4. If the request indicates that the merchant accepts gift 
      // certificates, the request must also specify a 
      // merchant-calculations-url.
      if (_AcceptMerchantGiftCertificates
        && _MerchantCalculationsUrl == null) {
        throw new ApplicationException("If you set " +
          "AcceptMerchantGiftCertificates=true, you must set " +
          "MerchantCalculationsUrl.");
      }

      // 5. If a carrier-calculated-shipping item exists.
      //a shipFrom must also exist
      if (_carrierCalculatedShipping.ShippingOptionsCount > 0
        && !_carrierCalculatedShipping.PackagesExist) {
        throw new ApplicationException("If you use Carrier Calculated " +
          "Shipping, a ShipFrom address must also be set." +
          " Please call the AddShippingPackage method.");
      }
    }

    /// <summary>
    /// This method is used to construct the &lt;merchant-private-data&gt; and 
    /// &lt;merchant-private-item-data&gt; XML elements. Both of these elements
    /// contain freeform XML blocks that are specified by the merchant.
    /// </summary>
    /// <param name="Xml">The XML.</param>
    /// <returns>This method returns the element value for either the 
    /// &lt;merchant-private-data&gt; or the &lt;merchant-private-item-data&gt; 
    /// XML element.</returns>
    internal static XmlElement MakeXmlElement(string Xml) {
      XmlDocument Doc = new XmlDocument();
      XmlElement El = Doc.CreateElement(MERCHANT_DATA_HIDDEN);
      El.InnerXml = Xml;
      return El;
    }

    /// <summary>
    /// Process the response.
    /// </summary>
    /// <param name="response">The Xml Response from the Request.</param>
    /// <returns>A class that implements the IGCheckoutResponse interface.</returns>
    protected override GCheckoutResponse ParseResponse(string response) {
      return new GCheckoutShoppingCartResponse(response);
    }

    /// <summary>
    /// This method sets the value of the &lt;good-until-date&gt; using the
    /// value of the <b>CartExpirationMinutes</b> parameter. This method 
    /// converts that value into Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="ExpirationMinutesFromNow">
    /// The length of time, in minutes, after which the shopping cart should
    /// expire. This property contains the value of the 
    /// <b>CartExpirationMinutes</b> property.
    /// </param>
    public void SetExpirationMinutesFromNow(int ExpirationMinutesFromNow) {
      CartExpiration = DateTime.UtcNow.AddMinutes(ExpirationMinutesFromNow);
    }

    /// <summary>
    /// Append Buyer Messages to the Shopping Cart.
    /// </summary>
    public BuyerMessages BuyerMessages {
      get {
        return _buyerMessages;
      }
    }

    /// <summary>
    /// This property sets or retrieves a value that indicates whether the 
    /// merchant is responsible for calculating taxes for the default
    /// tax table.
    /// </summary>
    /// <value>
    ///   The value of this property should be <b>true</b> if the merchant
    ///   will calculate taxes for the order. Otherwise, this value should be
    ///   <b>false</b>. The value should only be <b>true</b> if the merchant
    ///   has implemented the Merchant Calculations API.
    /// </value>
    public bool MerchantCalculatedTax {
      get {
        return _TaxTables.merchantcalculated;
      }
      set {
        _TaxTables.merchantcalculated = value;
        _TaxTables.merchantcalculatedSpecified = true;
      }
    }

    /// <summary>
    /// This property sets or retrieves a value that indicates whether the 
    /// merchant accepts coupons. If this value is set to <b>true</b>, the
    /// Google Checkout order confirmation page will display a text field 
    /// where the customer can enter a coupon code.
    /// </summary>
    /// <value>
    ///   This value of this property is a Boolean value that indicates
    ///   whether the merchant accepts coupons. This value should only be 
    ///   set to <b>true</b> if the merchant has implemented the Merchant 
    ///   Calculations API.
    /// </value>
    public bool AcceptMerchantCoupons {
      get {
        return _AcceptMerchantCoupons;
      }
      set {
        _AcceptMerchantCoupons = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves a value that indicates whether the 
    /// merchant accepts gift certificates. If this value is set to 
    /// <b>true</b>, the Google Checkout order confirmation page will 
    /// display a text field where the customer can enter a gift certificate 
    /// code.
    /// </summary>
    /// <value>
    ///   This value of this property is a Boolean value that indicates
    ///   whether the merchant accepts gift certificates. This value should 
    ///   only be set to <b>true</b> if the merchant has implemented the 
    ///   Merchant Calculations API.
    /// </value>
    public bool AcceptMerchantGiftCertificates {
      get {
        return _AcceptMerchantGiftCertificates;
      }
      set {
        _AcceptMerchantGiftCertificates = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;merchant-calculations-url&gt; element. This value is the URL to 
    /// which Google Checkout will send &lt;merchant-calculation-callback&gt;
    /// requests. This property is only relevant for merchants who are
    /// implementing the Merchant Calculations API.
    /// </summary>
    /// <value>The &lt;merchant-calculations-url&gt; element value.</value>
    public string MerchantCalculationsUrl {
      get {
        return _MerchantCalculationsUrl;
      }
      set {
        _MerchantCalculationsUrl = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;merchant-private-data&gt; element.
    /// </summary>
    /// <value>The &lt;merchant-private-data&gt; element value.</value>
    public string MerchantPrivateData {
      get {
        return _MerchantPrivateData;
      }
      set {
        _MerchantPrivateData = value;
      }
    }

    /// <summary>
    /// This property retrieves the value of the 
    /// &lt;merchant-private-data&gt; element.
    /// </summary>
    /// <value>The &lt;merchant-private-data&gt; element value.</value>
    public System.Xml.XmlNode[] MerchantPrivateDataNodes {
      get {
        System.Xml.XmlNode[] nodes =
          new System.Xml.XmlNode[_MerchantPrivateDataNodes.Count];
        _MerchantPrivateDataNodes.CopyTo(nodes);
        return nodes;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;continue-shopping-url&gt; element. Google Checkout will display 
    /// a link to this URL on the page that the customer sees after completing 
    /// her purchase.
    /// </summary>
    /// <value>The &lt;continue-shopping-url&gt; element value.</value>
    public string ContinueShoppingUrl {
      get {
        return _ContinueShoppingUrl;
      }
      set {
        _ContinueShoppingUrl = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;edit-cart-url&gt; element. Google Checkout will display 
    /// a link to this URL on the Google Checkout order confirmation page.
    /// The customer can click this link to edit the shopping cart contents
    /// before completing a purchase.
    /// </summary>
    /// <value>The &lt;edit-cart-url&gt; element value.</value>
    public string EditCartUrl {
      get {
        return _EditCartUrl;
      }
      set {
        _EditCartUrl = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;request-buyer-phone-number&gt; element. If this value is true,
    /// the buyer must enter a phone number to complete a purchase.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the Google should send the buyer's phone number
    ///   to the merchant, otherwise <c>false</c>.
    /// </value>
    public bool RequestBuyerPhoneNumber {
      get {
        return _RequestBuyerPhoneNumber;
      }
      set {
        _RequestBuyerPhoneNumber = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;good-until-date&gt; element.
    /// </summary>
    /// <value>The cart expiration.</value>
    public DateTime CartExpiration {
      get {
        return _CartExpiration;
      }
      set {
        _CartExpiration = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;analytics-data&gt; element. Google Analytics uses this
    /// to Track Google Checkout Orders.
    /// </summary>
    /// <remarks>
    /// Please read http://code.google.com/apis/checkout/developer/checkout_analytics_integration.html"
    /// for more information.
    /// </remarks>
    /// <value>The &lt;analytics-data&gt; element value.</value>
    public string AnalyticsData {
      get {
        return _AnalyticsData;
      }
      set {
        _AnalyticsData = value;
      }
    }

    /// <summary>
    /// This property sets or retrieves the value of the 
    /// &lt;platform-id&gt; element.
    /// </summary>
    /// <remarks>
    /// The &lt;platform-id&gt; tag should only be used by eCommerce providers
    /// who make API requests on behalf of a merchant. The tag's value contains
    /// a Google Checkout merchant ID that identifies the eCommerce provider.
    /// </remarks>
    /// <value>The &lt;analytics-data&gt; element value.</value>
    public long PlatformID {
      get {
        return _PlatformID;
      }
      set {
        _PlatformID = value;
      }
    }

    /// <summary>
    /// The &lt;parameterized-urls&gt; tag
    /// </summary>
    /// <remarks>
    /// It contains information about all of the
    /// web beacons that the merchant wants to add to the Google Checkout order 
    /// confirmation page. This tag encapsulates a list of one or more
    /// &lt;parameterized-url&gt; (<see cref="ParameterizedUrl"/>) tags.
    /// See
    /// http://code.google.com/apis/checkout/developer/checkout_pixel_tracking.html
    /// For additional information on Third-Party Conversion Tracking
    /// </remarks>
    /// <example><code>
    /// CheckoutShoppingCartRequest Req;
    /// // Req is assigned to a new CheckoutShoppingCartRequest somehow.
    /// ParameterizedUrl u = new ParameterizedUrl("https://example.com/track");
    /// u.AddParameter("oid", UrlParameterType.OrderID);
    /// u.AddParameter("ot", UrlParameterType.OrderTotal);
    /// Req.ParameterizedUrls.AddUrl(u);
    /// GCheckoutResponse Resp = Req.Send();
    /// </code></example>
    public ParameterizedUrls ParameterizedUrls {
      get {
        return _ParameterizedUrls;
      }
    }

    /// <summary>
    /// The Alternate Tax Tables container
    /// </summary>
    /// <remarks>
    /// All of the Alternate Tax tables are added to this Container.
    /// A Factory Method is also provided to create a Tax table and return it.
    /// </remarks>
    public AlternateTaxTableCollection AlternateTaxTables {
      get {
        return _alternateTaxTables;
      }
    }

    /// <summary>
    /// Lets the merchant request the detailed information about the initial
    /// authorization.
    /// </summary>
    /// <remarks>
    /// If the merchant sets this to true, Google will return an
    /// &lt;authorized-amount-notification&gt; when the order is first placed.
    /// </remarks>
    public bool RequestInitialAuthDetails {
      get {
        return _RequestInitialAuthDetails;
      }
      set {
        _RequestInitialAuthDetails = value;
      }
    }

    /// <summary></summary>
    public override string GetPostUrl() {
      if (_Environment == EnvironmentType.Sandbox) {
        if (_IsDonation) {
          return string.Format(
            "https://sandbox.google.com/checkout/api/checkout/v2/merchantCheckout/Donations/{0}",
            _MerchantID);
        }
        else {
          return string.Format(
            "https://sandbox.google.com/checkout/api/checkout/v2/merchantCheckout/Merchant/{0}",
            _MerchantID);
        }
      }
      else {
        if (_IsDonation) {
          return string.Format(
            "https://checkout.google.com/api/checkout/v2/merchantCheckout/Donations/{0}",
            _MerchantID);
        }
        else {
          return string.Format(
            "https://checkout.google.com/api/checkout/v2/merchantCheckout/Merchant/{0}",
            _MerchantID);
        }
      }
    }

    /// <summary>
    /// Set the &lt;rounding-policy&gt; node of 
    ///&lt;merchant-checkout-flow-support&gt;
    /// </summary>
    /// <param name="mode">The <see cref="RoundingMode"/></param>
    /// <param name="rule">The <see cref="RoundingRule"/></param>
    public void SetRoundingPolicy(RoundingMode mode, RoundingRule rule) {
      _roundingRuleSet = true;
      _roundingMode = mode;
      _roundingRule = rule;
    }


  }
}
