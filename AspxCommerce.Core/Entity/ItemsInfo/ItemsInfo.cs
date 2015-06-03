/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class ItemsInfo
    {
        #region ItemsInfo
        #region Constructor
        public ItemsInfo()
        {
        }
        #endregion
        #region Private Fields
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_ID", Order = 1)]
        private int _ID;

        [DataMember(Name = "_itemID", Order = 2)]
        private int _itemID;

        [DataMember(Name = "_sku", Order = 3)]
        private string _sku;

        [DataMember(Name = "_name", Order = 4)]
        private string _name;

        [DataMember(Name = "_itemTypeID", Order = 5)]
        private int _itemTypeID;

        [DataMember(Name = "_itemTypeName", Order = 6)]
        private string _itemTypeName;

        [DataMember(Name = "_attributeSetID", Order = 7)]
        private int _attributeSetID;

        [DataMember(Name = "_attributeSetName", Order = 8)]
        private string _attributeSetName;

        [DataMember(Name = "_price", Order = 9)]
        private string _price;

        [DataMember(Name = "_listPrice", Order = 10)]
        private string _listPrice;

        [DataMember(Name = "_quantity", Order = 11)]
        private string _quantity;

        [DataMember(Name = "_visibility", Order = 12)]
        private string _visibility;

        [DataMember(Name = "_status", Order = 13)]
        private string _status;

        [DataMember(Name = "_addedOn", Order = 14)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_isChecked", Order = 15)]
        private System.Nullable<bool> _isChecked;

        [DataMember(Name = "_currencyCode", Order = 16)]
        private string _currencyCode;
        #endregion
        #region Public Fields
        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._rowTotal;
            }
            set
            {
                if ((this._rowTotal != value))
                {
                    this._rowTotal = value;
                }
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        public int ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if ((this._itemID != value))
                {
                    this._itemID = value;
                }
            }
        }


        public string SKU
        {
            get
            {
                return this._sku;
            }
            set
            {
                if ((this._sku != value))
                {
                    this._sku = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if ((this._name != value))
                {
                    this._name = value;
                }
            }
        }

        public int ItemTypeID
        {
            get
            {
                return this._itemTypeID;
            }
            set
            {
                if ((this._itemTypeID != value))
                {
                    this._itemTypeID = value;
                }
            }
        }

        public string ItemTypeName
        {
            get
            {
                return this._itemTypeName;
            }
            set
            {
                if ((this._itemTypeName != value))
                {
                    this._itemTypeName = value;
                }
            }
        }

        public int AttributeSetID
        {
            get
            {
                return this._attributeSetID;
            }
            set
            {
                if ((this._attributeSetID != value))
                {
                    this._attributeSetID = value;
                }
            }
        }


        public string AttributeSetName
        {
            get
            {
                return this._attributeSetName;
            }
            set
            {
                if ((this._attributeSetName != value))
                {
                    this._attributeSetName = value;
                }
            }
        }

        public string Price
        {
            get
            {
                return this._price;
            }
            set
            {
                if ((this._price != value))
                {
                    this._price = value;
                }
            }
        }

        public string ListPrice
        {
            get
            {
                return this._listPrice;
            }
            set
            {
                if ((this._listPrice != value))
                {
                    this._listPrice = value;
                }
            }
        }

        public string Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                if ((this._quantity != value))
                {
                    this._quantity = value;
                }
            }
        }

        public string Visibility
        {
            get
            {
                return this._visibility;
            }
            set
            {
                if ((this._visibility != value))
                {
                    this._visibility = value;
                }
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if ((this._status != value))
                {
                    this._status = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }

        public System.Nullable<bool> IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                if ((this._isChecked != value))
                {
                    this._isChecked = value;
                }
            }
        }

        public string CurrencyCode
        {
            get
            {
                return this._currencyCode;
            }
            set
            {
                if ((this._currencyCode != value))
                {
                    this._currencyCode = value;
                }
            }
        }

        #endregion
        #endregion

        public class ItemSaveBasicInfo
        {
            public int ItemId { get; set; }
            public int ItemTypeId { get; set; }
            public int AttributeSetId { get; set; }
            public int BrandId { get; set; }
            public string CurrencyCode { get; set; }
            public string ItemVideoIDs { get; set; }
            public int TaxRuleId { get; set; }
            public string CategoriesIds { get; set; }
            public string AssociatedItemIds { get; set; }
            public string RelatedItemsIds { get; set; }
            public string UpSellItemsIds { get; set; }
            public string CrossSellItemsIds { get; set; }
            public string DownloadItemsValue { get; set; }
            public string SourceFileCol { get; set; }
            public string DataCollection { get; set; }
            public string FormVars { get; set; }
            public ItemSetting Settings { get; set; }
            public List<ItemPriceGroupInfo> GroupPrice { get; set;}
            public KitConfiguration KitConfig { get; set; }
          
        }

    }

    public class ItemCommonInfo
    {
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemName { get; set; }
        public string SKU { get; set; }
        public string ImagePath { get; set; }
    }
   
    public class MostViewItemInfoAdminDash : ItemCommonInfo
    {
        public decimal? Price { get; set; }
        public int? ViewCount { get; set; }
    }

    public class AddItemToCartInfo
    {
        public int ItemID { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string CostVariantIDs { get; set; }
        public bool IsGiftCard { get; set; }
        public bool IsKitItem { get; set; }
    }



    public class ItemSmallCommonInfo
    {
        public string SKU { get; set; }
        public string ItemName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetItemListInfo : ItemSmallCommonInfo
    {
        public string ItemTypeID { get; set; }
        public string AttributeSetID { get; set; }
        public bool? Visibility { get; set; }
    }
    [DataContract]
    [Serializable]
    public class GiftCardReport
    {
        public int RowTotal { get; set; }
        public string ItemName { get; set; }
        public string SKU { get; set; }
        public string GiftCardCode { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public int TotalPurchases { get; set; }
        public bool IsActive { get; set; }
        public int GiftCardType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }

    public class ItemDetailsCommonInfo
    { 
        public bool ServiceBit { get; set; }
        public int SelfItemID { get; set; }
        public string ItemSKU { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeID { get; set; }
        public string AttributeSetID { get; set; }
    }

    public class ItemPriceGroupInfo
    {
        public string GroupID { get; set; }
        public decimal Price { get; set; }
    }
    public class ItemCartInfo
    {
        public decimal ItemQuantityInCart { get; set; }
        public decimal UserItemQuantityInCart { get; set; }

    }
}