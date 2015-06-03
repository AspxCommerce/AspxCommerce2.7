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
    [Serializable]
    [DataContract]
    public class CartInfo
    {
        [DataMember]
        private int _cartID;
        [DataMember]
        private int _itemTypeID;

        [DataMember]
        private int _cartItemID;

        [DataMember]
        private string _sku;

        [DataMember]
        private System.Nullable<int> _itemID;

        [DataMember]
        private string _itemName;

        [DataMember]
        private string _imagePath;

        [DataMember]
        private string _alternateText;

        [DataMember]
        private System.Nullable<decimal> _price;

        [DataMember]
        private System.Nullable<int> _quantity;

        [DataMember]
        private System.Nullable<int> _itemQuantity;

        [DataMember]
        private System.Nullable<decimal> _length;
        [DataMember]
        private System.Nullable<decimal> _width;
        [DataMember]
        private System.Nullable<decimal> _height;

        [DataMember]
        private System.Nullable<decimal> _weight;

        [DataMember]
        private System.Nullable<decimal> _taxRateValue;

        [DataMember]
        private string _shortDescription;

        [DataMember]
        private string _costVariants;

        [DataMember]
        private System.Nullable<decimal> _totalItemCost;

        [DataMember]
        private string _remarks;

        [DataMember]
        private string _sessionCode;

        [DataMember]
        private string _costVariantsValueIDs;

        [DataMember]
        private string _userName;

        [DataMember]
        private System.Nullable<int> _storeID;

        [DataMember]
        private System.Nullable<int> _portalID;

        [DataMember]
        private System.Nullable<int> _costVariantCountbyItemID;

        [DataMember]
        private int _minCartQuantity;

        [DataMember]
        private int _maxCartQuantity;

        [DataMember]
        private string _kitData;



        public CartInfo()
        {
        }

        public string KitData
        {
            get
            {
                return this._kitData;
            }
            set
            {
                if ((this._kitData != value))
                {
                    this._kitData = value;
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
        public int CartID
        {
            get
            {
                return this._cartID;
            }
            set
            {
                if ((this._cartID != value))
                {
                    this._cartID = value;
                }
            }
        }

        public int CartItemID
        {
            get
            {
                return this._cartItemID;
            }
            set
            {
                if ((this._cartItemID != value))
                {
                    this._cartItemID = value;
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

        public System.Nullable<int> ItemID
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

        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                if ((this._itemName != value))
                {
                    this._itemName = value;
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

        public System.Nullable<decimal> Price
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
        public System.Nullable<int> Quantity
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

        public System.Nullable<int> ItemQuantity
        {
            get
            {
                return this._itemQuantity;
            }
            set
            {
                if ((this._itemQuantity != value))
                {
                    this._itemQuantity = value;
                }
            }
        }
        public System.Nullable<decimal> Length
        {
            get
            {
                return this._length;
            }
            set
            {
                if ((this._length != value))
                {
                    this._length = value;
                }
            }
        }
        public System.Nullable<decimal> Width
        {
            get
            {
                return this._width;
            }
            set
            {
                if ((this._width != value))
                {
                    this._width = value;
                }
            }
        }
        public System.Nullable<decimal> Height
        {
            get
            {
                return this._height;
            }
            set
            {
                if ((this._height != value))
                {
                    this._height = value;
                }
            }
        }

        public System.Nullable<decimal> Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                if ((this._weight != value))
                {
                    this._weight = value;
                }
            }
        }

        public System.Nullable<decimal> TaxRateValue
        {
            get
            {
                return this._taxRateValue;
            }
            set
            {
                if ((this._taxRateValue != value))
                {
                    this._taxRateValue = value;
                }
            }
        }

        public string ShortDescription
        {
            get
            {
                return this._shortDescription;
            }
            set
            {
                if ((this._shortDescription != value))
                {
                    this._shortDescription = value;
                }
            }
        }

        public string CostVariants
        {
            get
            {
                return this._costVariants;
            }
            set
            {
                if ((this._costVariants != value))
                {
                    this._costVariants = value;
                }
            }
        }

        public System.Nullable<decimal> TotalItemCost
        {
            get
            {
                return this._totalItemCost;
            }
            set
            {
                if ((this._totalItemCost != value))
                {
                    this._totalItemCost = value;
                }
            }
        }
        public string Remarks
        {
            get
            {
                return this._remarks;
            }
            set
            {
                if ((this._remarks != value))
                {
                    this._remarks = value;
                }
            }
        }

        public string SessionCode
        {
            get
            {
                return this._sessionCode;
            }
            set
            {
                if ((this._sessionCode != value))
                {
                    this._sessionCode = value;
                }
            }
        }

        public string CostVariantsValueIDs
        {
            get
            {
                return this._costVariantsValueIDs;
            }
            set
            {
                if ((this._costVariantsValueIDs != value))
                {
                    this._costVariantsValueIDs = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                if ((this._userName != value))
                {
                    this._userName = value;
                }
            }
        }

        public System.Nullable<int> StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }

        public System.Nullable<int> PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
                }
            }
        }

        public System.Nullable<int> CostVariantCountbyItemID
        {
            get
            {
                return this._costVariantCountbyItemID;
            }
            set
            {
                if ((this._costVariantCountbyItemID != value))
                {
                    this._costVariantCountbyItemID = value;
                }
            }
        }
        public int MinCartQuantity
        {
            get
            {
                return this._minCartQuantity;
            }
            set
            {
                if ((this._minCartQuantity != value))
                {
                    this._minCartQuantity = value;
                }
            }
        }
        public int MaxCartQuantity
        {
            get
            {
                return this._maxCartQuantity;
            }
            set
            {
                if ((this._maxCartQuantity != value))
                {
                    this._maxCartQuantity = value;
                }
            }
        }

    }
}

