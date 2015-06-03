using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.WishItem
{
    [DataContract]
    [Serializable]
    public class WishItemsInfo
    {
        #region "Private Members"
        private int _rowTotal;
        private int _wishItemID;
        private string _costVariantValueIDs;

        private int _itemID;

        private int _itemTypeID;

        private string _itemName;

        private bool _isCostVariantItem;

        private string _costVariantValue;

        private int _quantity;

        private string _price;

        private System.Nullable<bool> _isOutOfStock;

        private DateTime _wishDate;

        private string _imagePath;

        private string _alternateText;

        private string _listPrice;

        private string _comment;

        private string _sku;



        //private bool _hidePrice;

        //private bool _hideInRSSFeed; 
        #endregion

        #region "Constructors"
        public WishItemsInfo()
        {
        }

        public WishItemsInfo(int wishItemID, string costVariantValueIDs, int itemID, string itemName, bool isCostVariantItem, string itemCostVariantValue, string price, DateTime wishDate, string imagePath, string alternateText, string listPrice, string comment, string sku)
        {
            this.WishItemID = wishItemID;
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.IsCostVariantItem = isCostVariantItem;
            this.ItemCostVariantValue = itemCostVariantValue;
            this.Price = price;
            this.WishDate = wishDate;
            this.ImagePath = imagePath;
            this.AlternateText = alternateText;
            this.ListPrice = listPrice;
            this.Comment = comment;
            this.SKU = sku;
            this.Quantity = Quantity;

        }
        #endregion

        #region "Public Properties"
        [DataMember]
        public int RowTotal
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

        [DataMember]
        public int WishItemID
        {
            get { return _wishItemID; }
            set { _wishItemID = value; }
        }

        [DataMember]
        public bool IsCostVariantItem
        {
            get { return _isCostVariantItem; }
            set { _isCostVariantItem = value; }
        }

        [DataMember]
        public string CostVariantValueIDs
        {
            get { return _costVariantValueIDs; }
            set { _costVariantValueIDs = value; }
        }
        [DataMember]
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        [DataMember]
        public int ItemTypeID
        {
            get { return _itemTypeID; }
            set { _itemTypeID = value; }
        }
        [DataMember]
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        [DataMember]
        public string ItemCostVariantValue
        {
            get { return _costVariantValue; }
            set { _costVariantValue = value; }
        }
        [DataMember]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        [DataMember]
        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }
        [DataMember]
        public System.Nullable<bool> IsOutOfStock
        {
            get
            {
                return this._isOutOfStock;
            }
            set
            {
                if ((this._isOutOfStock != value))
                {
                    this._isOutOfStock = value;
                }
            }
        }
        [DataMember]
        public DateTime WishDate
        {
            get { return _wishDate; }
            set { _wishDate = value; }
        }
        [DataMember]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        [DataMember]
        public string AlternateText
        {
            get { return _alternateText; }
            set { _alternateText = value; }
        }
        [DataMember]
        public string ListPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }
        [DataMember]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        [DataMember]
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
        #endregion
    }
}
