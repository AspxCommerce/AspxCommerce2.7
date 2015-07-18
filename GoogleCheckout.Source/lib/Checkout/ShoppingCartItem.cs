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
 *  Initial Release.
 *  3-14-2009   Joe Feser joe.feser@joefeser.com
 *  We no longer allow people to pass in fractional amounts. All numbers are trimmed to $x.xx
*/
using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using GCheckout.Util;
using GCheckout.Checkout;

namespace GCheckout.Checkout {

  /// <summary>
  /// A wrapper containing information about an individual item listed 
  /// in the customer's shopping cart
  /// </summary>
  public class ShoppingCartItem : IShoppingCartItem, ICloneable {

    private string _name;
    private string _description;
    private decimal _price;
    private int _quantity;
    private string _merchantItemID;
    private XmlNode[] _merchantPrivateItemDataNodes = new XmlNode[] { };
    private AlternateTaxTable _taxTable;
    private DigitalItem _digitalContent;
    private double _itemWeight;
    //this is only used by the callback method and should not be used by the cart
    private string _taxtableselector = string.Empty;
    //we want the for the table table information.
    private GCheckout.AutoGen.Item _notificationItem;
    private Subscription _subscription;

    /// <summary>
    /// Identifies the name of an item
    /// </summary>
    public virtual string Name {
      get {
        return _name;
      }
      set {
        _name = EncodeHelper.CleanString(value);
      }
    }

    /// <summary>
    /// Contains a description of an item
    /// </summary>
    public virtual string Description {
      get {
        return _description;
      }
      set {
        _description = EncodeHelper.CleanString(value);
      }
    }

    /// <summary>
    /// The cost of the item. This tag has one required attribute, 
    /// which identifies the currency of the price
    /// </summary>
    public virtual decimal Price {
      get {
        return _price;
      }
      set {
        value = Math.Round(value, 2); //fix for sending in fractional cents
        _price = value;
      }
    }

    /// <summary>
    /// Identifies how many units of a particular item are 
    /// included in the customer's shopping cart.
    /// </summary>
    public virtual int Quantity {
      get {
        return _quantity;
      }
      set {
        _quantity = value;
      }
    }

    /// <summary>
    /// Contains a value, such as a stock keeping unit (SKU), 
    /// that you use to uniquely identify an item.
    /// </summary>
    /// <remarks>Google Checkout will include this value in 
    /// the merchant calculation 
    /// callbacks and the new order notification for the order.</remarks>
    public virtual string MerchantItemID {
      get {
        return _merchantItemID;
      }
      set {
        _merchantItemID = EncodeHelper.CleanString(value);
      }
    }

    /// <summary>
    /// A legacy method of allowing the private data be set with a string
    /// </summary>
    public string MerchantPrivateItemData {
      get {
        foreach (XmlNode node in MerchantPrivateItemDataNodes) {
          if (node.Name == CheckoutShoppingCartRequest.MERCHANT_DATA_HIDDEN)
            return node.InnerXml;
        }
        //if we get this far and we have one node, just return it
        if (MerchantPrivateItemDataNodes.Length == 1)
          return MerchantPrivateItemDataNodes[0].OuterXml;

        return null;
      }
      set {
        if (value == null)
          value = string.Empty;

        XmlNode merchantNode = null;

        foreach (XmlNode node in MerchantPrivateItemDataNodes) {
          if (node.Name == CheckoutShoppingCartRequest.MERCHANT_DATA_HIDDEN) {
            merchantNode = node;
            break;
          }
        }

        //we must delete the data
        if (value == string.Empty) {
          //we are not going to remove the node. we are just going to set it
          if (merchantNode != null) {
            merchantNode.InnerXml = string.Empty;
          }
        }
        else {
          if (merchantNode == null) {
            //we need to copy the array and then add this item to it.
            merchantNode = CheckoutShoppingCartRequest.MakeXmlElement(value);
            //now put the node in the array.
            XmlNode[] nodes
              = new XmlNode[MerchantPrivateItemDataNodes.Length + 1];
            MerchantPrivateItemDataNodes.CopyTo(nodes, 0);
            nodes[nodes.Length - 1] = merchantNode;
            _merchantPrivateItemDataNodes = nodes;
          }
          else {
            merchantNode.InnerXml = value;
          }
        }
      }
    }

    /// <summary>
    /// Contains any well-formed XML sequence that should 
    /// accompany an individual item in an order.
    /// </summary>
    public virtual XmlNode[] MerchantPrivateItemDataNodes {
      get {
        return _merchantPrivateItemDataNodes;
      }
      set {
        if (value == null)
          value = new XmlNode[] { };

        //see if there is a private node set as a string first
        string mpd = null;
        foreach (XmlNode node in MerchantPrivateItemDataNodes) {
          if (node.Name == CheckoutShoppingCartRequest.MERCHANT_DATA_HIDDEN)
            mpd = node.InnerXml;
        }

        _merchantPrivateItemDataNodes = value;

        if (mpd != null && mpd.Length > 0) {
          MerchantPrivateItemData = mpd;
        }
      }
    }

    /// <summary>
    /// Identifies an alternate tax table that should be
    /// used to calculate tax for a particular item.
    /// </summary>
    public virtual AlternateTaxTable TaxTable {
      get {
        if (_notificationItem != null)
          throw new NotImplementedException("This Item was built from a " +
          "new-order-notification and has no knowledge of a tax table. " +
          "Please call 'TaxTableSelector' to read the tax-table-selector property.");
        if (_taxTable == null)
          _taxTable = new AlternateTaxTable();
        return _taxTable;
      }
      set {
        _taxTable = value;
      }
    }

    /// <summary>
    /// Property used only when this object was built from an 
    /// <seealso cref="GCheckout.AutoGen.Item"/> object.
    /// </summary>
    public virtual string TaxTableSelector {
      get {
        if (_notificationItem == null)
          throw new NotImplementedException("This Item was not built from a " +
            "new-order-notification. " +
            "Please call 'TaxTable' to read the AlternateTaxTable for a " +
            "GCheckoutRequest.");
        if (_notificationItem.taxtableselector == null)
          _notificationItem.taxtableselector = string.Empty;
        return _notificationItem.taxtableselector;
      }
    }

    /// <summary>
    /// Contains information relating to digital
    /// delivery of an item.
    /// </summary>
    public virtual DigitalItem DigitalContent {
      get {
        return _digitalContent;
      }
      set {
        _digitalContent = value;
      }
    }

    /// <summary>
    /// Subscription information for an item
    /// </summary>
    public virtual Subscription Subscription {
      get {
        return _subscription;
      }
      set {
        _subscription = value;
      }
    }

    /// <summary>
    /// The &lt;item-weight&gt; tag specifies the weight of an 
    /// individual item in the customer's shopping cart.
    /// </summary>
    public virtual double Weight {
      get {
        return _itemWeight;
      }
      set {
        if (value < 0)
          throw new
            ArgumentOutOfRangeException("The value must be 0 or larger");
        _itemWeight = value;
      }
    }

    /// <summary>
    /// Initialize a new instance of the 
    /// <see cref="ShoppingCartItem"/> class, which creates an object
    /// corresponding to an individual item in an order. This method 
    /// is used for items that do not have an associated
    /// &lt;merchant-private-item-data&gt; XML block.
    /// </summary>
    public ShoppingCartItem() {

    }

    /// <summary>
    /// ctor used by the extended notification class
    /// </summary>
    /// <param name="theItem"></param>
    public ShoppingCartItem(GCheckout.AutoGen.Item theItem) {
      if (theItem.digitalcontent != null)
        this.DigitalContent = new DigitalItem(theItem.digitalcontent);
      this.Description = theItem.itemdescription;
      this.MerchantItemID = theItem.merchantitemid;
      if (theItem.merchantprivateitemdata != null)
        this.MerchantPrivateItemDataNodes
          = theItem.merchantprivateitemdata.Any;
      else
        this.MerchantPrivateItemDataNodes = new XmlNode[] { };
      this.Name = theItem.itemname;
      this.Price = theItem.unitprice.Value; //is checked for fractions
      this.Quantity = theItem.quantity;
      _taxtableselector = theItem.taxtableselector;
      _notificationItem = theItem;
      if (theItem.subscription != null) {
        this.Subscription = new Subscription(theItem.subscription);
      }
    }

    /// <summary>
    /// Initialize a new instance of the 
    /// <see cref="ShoppingCartItem"/> class, which creates an object
    /// corresponding to an individual item in an order. This method 
    /// is used for items that do not have an associated
    /// &lt;merchant-private-item-data&gt; XML block.
    /// </summary>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">A description of the item.</param>
    /// <param name="merchantItemID">The Merchant Item Id that uniquely 
    /// identifies the product in your system. (optional)</param>
    /// <param name="price">The price of the item, per item.</param>
    /// <param name="quantity">The number of the item that is 
    /// included in the order.</param>
    public ShoppingCartItem(string name, string description,
      string merchantItemID, decimal price, int quantity)
      : this(name, description, merchantItemID, price,
      quantity, AlternateTaxTable.Empty, new XmlNode[] { }) {
    }

    /// <summary>
    /// Initialize a new instance of the 
    /// <see cref="ShoppingCartItem"/> class, which creates an object
    /// corresponding to an individual item in an order. This method 
    /// is used for items that do have an associated
    /// &lt;merchant-private-item-data&gt; XML block.
    /// </summary>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">A description of the item.</param>
    /// <param name="merchantItemID">The Merchant Item Id that uniquely 
    /// identifies the product in your system. (optional)</param>
    /// <param name="price">The price of the item, per item.</param>
    /// <param name="quantity">The number of the item that is 
    /// included in the order.</param>
    /// <param name="merchantPrivateItemData">The merchant private
    /// item data associated with the item.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem(string name, string description,
      string merchantItemID, decimal price, int quantity,
      AlternateTaxTable taxTable,
      string merchantPrivateItemData) {
      Name = name;
      Description = description;
      if (merchantItemID == string.Empty)
        merchantItemID = null;
      MerchantItemID = merchantItemID;
      Price = price; //is checked for fractions
      Quantity = quantity;
      MerchantPrivateItemData = merchantPrivateItemData;
      TaxTable = taxTable;
    }

    /// <summary>
    /// Initialize a new instance of the 
    /// <see cref="ShoppingCartItem"/> class, which creates an object
    /// corresponding to an individual item in an order. This method 
    /// is used for items that do have an associated
    /// &lt;merchant-private-item-data&gt; XML block.
    /// </summary>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">A description of the item.</param>
    /// <param name="merchantItemID">The Merchant Item Id that uniquely 
    /// identifies the product in your system. (optional)</param>
    /// <param name="price">The price of the item, per item.</param>
    /// <param name="quantity">The number of the item that is 
    /// included in the order.</param>
    /// <param name="merchantPrivateItemData">The merchant private
    /// item data associated with the item.</param>
    /// <param name="taxTable">The <see cref="AlternateTaxTable"/> 
    /// To assign to the item </param>
    public ShoppingCartItem(string name, string description,
      string merchantItemID, decimal price, int quantity,
      AlternateTaxTable taxTable,
      params XmlNode[] merchantPrivateItemData) {
      Name = name;
      Description = description;
      if (merchantItemID == string.Empty)
        merchantItemID = null;
      MerchantItemID = merchantItemID;
      Price = price; //is checked for fractions
      Quantity = quantity;
      MerchantPrivateItemDataNodes = merchantPrivateItemData;
      TaxTable = taxTable;
    }

    #region ICloneable Members

    /// <summary>
    /// Clone the item
    /// </summary>
    /// <returns></returns>
    public object Clone() {
      ShoppingCartItem retVal = this.MemberwiseClone() as ShoppingCartItem;

      //clone the xml
      XmlNode[] nodes = new XmlNode[this.MerchantPrivateItemDataNodes.Length];

      for (int i = 0; i < nodes.Length; i++) {
        nodes[i] = MerchantPrivateItemDataNodes[i].Clone();
      }

      retVal.MerchantPrivateItemDataNodes = nodes;

      //clone the digital item
      if (DigitalContent != null)
        retVal.DigitalContent = DigitalContent.Clone() as DigitalItem;

      if (Subscription != null)
        retVal.Subscription = Subscription.Clone() as Subscription;

      return retVal;
    }

    #endregion

    /// <summary>
    /// Convert the item to an autogen item
    /// </summary>
    /// <param name="currency">The currency</param>
    /// <returns></returns>
    public AutoGen.Item CreateAutoGenItem(string currency) {
      return ShoppingCartItem.CreateAutoGenItem(this, currency);
    }

    /// <summary>
    /// Convert the item to an auto gen item.
    /// </summary>
    /// <param name="item">The item to convert</param>
    /// <param name="currency">The currency</param>
    /// <returns></returns>
    public static AutoGen.Item CreateAutoGenItem(IShoppingCartItem item,
      string currency) {
      GCheckout.AutoGen.Item currentItem = new AutoGen.Item();

      currentItem.itemname = EncodeHelper.EscapeXmlChars(item.Name);
      currentItem.itemdescription =
        EncodeHelper.EscapeXmlChars(item.Description);
      currentItem.quantity = item.Quantity;

      //if there is a subscription, you may not have a unit price

      if (item.Subscription != null && item.Price > 0) {
        throw new ApplicationException(
          "The unit price must be 0 if you are using a subscription item.");
      }

      currentItem.unitprice = new AutoGen.Money();
      currentItem.unitprice.currency = currency;
      currentItem.unitprice.Value = item.Price;

      //TODO determine if we should try to catch if a UK customer tries to
      //use this value.
      if (item.Weight > 0) {
        currentItem.itemweight = new AutoGen.ItemWeight();
        currentItem.itemweight.unit = "LB"; //this is the only option.
        currentItem.itemweight.value = item.Weight;
      }

      if (item.MerchantItemID != null
        && item.MerchantItemID.Length > 0) {
        currentItem.merchantitemid = item.MerchantItemID;
      }

      if (item.MerchantPrivateItemDataNodes != null
        && item.MerchantPrivateItemDataNodes.Length > 0) {
        AutoGen.anyMultiple any = new AutoGen.anyMultiple();

        any.Any = item.MerchantPrivateItemDataNodes;
        currentItem.merchantprivateitemdata = any;
      }

      if (item.TaxTable != null &&
        item.TaxTable != AlternateTaxTable.Empty) {
        currentItem.taxtableselector = item.TaxTable.Name;
      }

      //if we have a digital item then we need to append the information
      if (item.DigitalContent != null) {
        currentItem.digitalcontent = new GCheckout.AutoGen.DigitalContent();
        //we have one of two types, email or key/url
        if (item.DigitalContent.EmailDelivery) {
          currentItem.digitalcontent.emaildelivery = true;
          currentItem.digitalcontent.emaildeliverySpecified = true;
        }
        else {
          if (item.DigitalContent.Description.Length > 0) {
            currentItem.digitalcontent.description =
              item.DigitalContent.Description;
          }
          if (item.DigitalContent.Key.Length > 0) {
            currentItem.digitalcontent.key =
              item.DigitalContent.Key;
          }
          if (item.DigitalContent.Url.Length > 0) {
            currentItem.digitalcontent.url =
              item.DigitalContent.Url;
          }
          currentItem.digitalcontent.displaydisposition
            = item.DigitalContent.GetSerializedDisposition();
        }
      }

      //see if we have subscription information
      if (item.Subscription != null) {
        if (item.Subscription.RecurrentItem == null) {
          throw new ApplicationException(
            "You must set a RecurrentItem for a subscription.");
        }
        Subscription sub = item.Subscription;
        //we have to assume the item was validated
        currentItem.subscription = new GCheckout.AutoGen.Subscription();
        GCheckout.AutoGen.Subscription autoSub = currentItem.subscription;
        if (sub.NoChargeAfter.HasValue) {
          autoSub.nochargeafter = sub.NoChargeAfter.Value;
          autoSub.nochargeafterSpecified = true;
        }
        autoSub.payments
          = new GCheckout.AutoGen.SubscriptionPayment[sub.Payments.Count];
        for (int sp = 0; sp < sub.Payments.Count; sp++) {
          SubscriptionPayment payment = sub.Payments[sp];
          autoSub.payments[sp] = new GCheckout.AutoGen.SubscriptionPayment();
          autoSub.payments[sp].maximumcharge
            = EncodeHelper.Money(currency, payment.MaximumCharge);
          if (payment.Times > 0) {
            autoSub.payments[sp].times = payment.Times;
            autoSub.payments[sp].timesSpecified = true;
          }
          autoSub.period = sub.Period;
          if (sub.RecurrentItem != null
            && sub.RecurrentItem.Subscription != null) {
            throw new ApplicationException(
              "A RecurrentItem may not contain a subscription.");
          }
          //call this method to create the recurrent item.
          autoSub.recurrentitem
            = ShoppingCartItem.CreateAutoGenItem(sub.RecurrentItem, currency);
          if (sub.StartDate.HasValue) {
            autoSub.startdate = sub.StartDate.Value;
            autoSub.startdateSpecified = true;
          }
          autoSub.type = sub.Type.ToString();
        }
      }
      return currentItem;
    }
  }
}
