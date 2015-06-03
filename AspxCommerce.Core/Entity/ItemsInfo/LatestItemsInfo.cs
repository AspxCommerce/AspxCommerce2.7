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

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class LatestItemsInfo
    {
        #region Constructor
        public LatestItemsInfo()
        {
        }
        #endregion

        #region Private Fields
        [DataMember]
        private int _rowTotal;
        [DataMember]
        private bool _isCostVariantItem;
        [DataMember]
        private int _itemID;

        [DataMember]
        private int _attributeSetID;

        [DataMember]
        private int _itemTypeID;

        [DataMember]
        private System.Nullable<System.DateTime> _dateFrom;

        [DataMember]
        private System.Nullable<System.DateTime> _dateTo;

        [DataMember]
        private System.Nullable<bool> _isFeatured;

        [DataMember]
        private string _sku;

        [DataMember]
        private string _name;

        [DataMember]
        private decimal _price;

        [DataMember]
        private System.Nullable<decimal> _listPrice;

        [DataMember]
        private int _quantity;

        [DataMember]
        private System.Nullable<bool> _hidePrice;

        [DataMember]
        private System.Nullable<bool> _hideInRSSFeed;

        [DataMember]
        private System.Nullable<bool> _hideToAnonymous;

        [DataMember]
        private System.Nullable<bool> _isOutOfStock;

        [DataMember]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember]
        private string _imagePath;

        [DataMember]
        private string _alternateImagePath;

        [DataMember]
        private string _alternateText;

         [DataMember]
        private string _attributeValues;
        
        #endregion

        #region Public Fields
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
        public bool IsCostVariantItem
        {
            get
            {
                return this._isCostVariantItem;
            }
            set
            {
                if ((this._isCostVariantItem != value))
                {
                    this._isCostVariantItem = value;
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

        public System.Nullable<System.DateTime> DateFrom
        {
            get
            {
                return this._dateFrom;
            }
            set
            {
                if ((this._dateFrom != value))
                {
                    this._dateFrom = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DateTo
        {
            get
            {
                return this._dateTo;
            }
            set
            {
                if ((this._dateTo != value))
                {
                    this._dateTo = value;
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

        public System.Nullable<bool> IsFeatured
        {
            get
            {
                return this._isFeatured;
            }
            set
            {
                if ((this._isFeatured != value))
                {
                    this._isFeatured = value;
                }
            }
        }

        public decimal Price
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

        public System.Nullable<decimal> ListPrice
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

        public int Quantity
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

        public System.Nullable<bool> HidePrice
        {
            get
            {
                return this._hidePrice;
            }
            set
            {
                if ((this._hidePrice != value))
                {
                    this._hidePrice = value;
                }
            }
        }

        public System.Nullable<bool> HideInRSSFeed
        {
            get
            {
                return this._hideInRSSFeed;
            }
            set
            {
                if ((this._hideInRSSFeed != value))
                {
                    this._hideInRSSFeed = value;
                }
            }
        }

        public System.Nullable<bool> HideToAnonymous
        {
            get
            {
                return this._hideToAnonymous;
            }
            set
            {
                if ((this._hideToAnonymous != value))
                {
                    this._hideToAnonymous = value;
                }
            }
        }

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

        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                if ((this._imagePath != value))
                {
                    this._imagePath = value;
                }
            }
        }

        public string AlternateImagePath
        {
            get
            {
                return this._alternateImagePath;
            }
            set
            {
                if ((this._alternateImagePath != value))
                {
                    this._alternateImagePath = value;
                }
            }
        }

        public string AlternateText
        {
            get
            {
                return this._alternateText;
            }
            set
            {
                if ((this._alternateText != value))
                {
                    this._alternateText = value;
                }
            }
        }

        public string AttributeValues
        {
            get
            {
                return this._attributeValues;
            }
            set
            {
                if ((this._attributeValues != value))
                {
                    this._attributeValues = value;
                }
            }
        }
        #endregion
    }   
}
