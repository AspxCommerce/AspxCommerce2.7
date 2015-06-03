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
    public class ShippingCostInfo
    {
        #region Private Numbers

        private int _shippingMethodID;
        private string _shippingMethodName;
        private int _itemID;
        private string _itemName;
        private string _sku;
        private decimal _itemCost;
        private int _quantity;
        private decimal _weight;
        private decimal _shippingCost;
        private decimal _subTotal;
        private decimal _totalCost;
        private decimal _taxRateValue;
        private string _variants;
        private string _variantIDs;
        private int _itemTypeID;

        #endregion

        #region Public Members

        [DataMember]
        public int ShippingMethodID
        {
            get
            {
                return this._shippingMethodID;
            }
            set
            {
                if ((this._shippingMethodID != value))
                {
                    this._shippingMethodID = value;
                }
            }
        }
        [DataMember]
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
        [DataMember]
        public string ShippingMethodName
        {
            get
            {
                return this._shippingMethodName;
            }
            set
            {
                if ((this._shippingMethodName != value))
                {
                    this._shippingMethodName = value;
                }
            }
        }

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        public string Sku
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

        [DataMember]
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
         [DataMember]
        public decimal Weight
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
        
        [DataMember]
        public decimal ShippingCost
        {
            get
            {
                return this._shippingCost;
            }
            set
            {
                if ((this._shippingCost != value))
                {
                    this._shippingCost = value;
                }
            }
        }
        
        [DataMember]
        public decimal UnitPrice
        {
            get
            {
                return this._itemCost;
            }
            set
            {
                if ((this._itemCost != value))
                {
                    this._itemCost = value;
                }
            }
        }
        [DataMember]
        public decimal SubTotalPrice
        {
            get
            {
                return this._subTotal;
            }
            set
            {
                if ((this._subTotal != value))
                {
                    this._subTotal = value;
                }
            }
        }

        [DataMember]
        public decimal TotalCost
        {
            get
            {
                return this._totalCost;
            }
            set
            {
                if ((this._totalCost != value))
                {
                    this._totalCost = value;
                }
            }
        }
        [DataMember]
        public decimal TaxRateValue
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

          [DataMember]
        public string Variants
        {
            get
            {
                return this._variants;
            }
            set
            {
                if ((this._variants != value))
                {
                    this._variants = value;
                }
            }
        }
           [DataMember]
          public string VariantIDs
        {
            get
            {
                return this._variantIDs;
            }
            set
            {
                if ((this._variantIDs != value))
                {
                    this._variantIDs = value;
                }
            }
        }
        
        
        #endregion
    }
}
