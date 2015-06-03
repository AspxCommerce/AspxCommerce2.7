using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
   public class YouJustExploredItemsInfo
    {
        public YouJustExploredItemsInfo()
        {
        }

        [DataMember] private string _itemName;
        [DataMember] private string _sku;
        [DataMember] private int _itemId;
        [DataMember] private int _rowTotal;
        [DataMember] private decimal _price;
        [DataMember] private decimal _listPrice;
        [DataMember] private string _imagePath;

        public string ItemName
        {
            get { return this._itemName; }
            set
            {
                if(this._itemName !=value)
                {
                    this._itemName = value;
                }
            }
        }
        public string Sku
        {
            get { return this._sku; }
            set
            {
                if(this._sku !=value)
                {
                    this._sku = value;
                }
            }
        }
        public int ItemId
        {
            get { return this._itemId; }
            set
            {
                if(this._itemId != value)
                {
                    this._itemId = value;
                }
            }
        }

        public int RowTotal
        {
            get { return this._rowTotal; }
            set
            {
                if (this._rowTotal != value)
                {
                    this._rowTotal = value;
                }
            }
        }
        public decimal Price
        {
            get { return this._price; }
            set
            {
                if (this._price != value)
                {
                    this._price = value;
                }
            }
        }
        public decimal ListPrice
        {
            get { return this._listPrice; }
            set
            {
                if (this._listPrice != value)
                {
                    this._listPrice = value;
                }
            }
        }
        public string ImagePath
        {
            get { return this._imagePath; }
            set
            {
                if (this._imagePath != value)
                {
                    this._imagePath = value;
                }
            }
        }


    }
}
