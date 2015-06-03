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
{[Serializable]
    [DataContract]
    public class CustCartInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;

        [DataMember(Name = "_cartItemID", Order = 1)]
        private System.Nullable<int> _cartItemID;    
        
        [DataMember(Name = "_itemID", Order = 2)]
        private System.Nullable<int> _itemID;

        [DataMember(Name = "_itemName", Order = 3)]
        private string _itemName;

        [DataMember(Name = "_sku", Order = 4)]
        private string _sku;

        [DataMember(Name = "_price", Order = 5)]
        private System.Nullable<decimal> _price;

        [DataMember(Name = "_quantity", Order = 6)]
        private System.Nullable<int> _quantity;

        public CustCartInfo()
        {
        }

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

        public System.Nullable<int> CartItemID
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
                if((this._quantity != value))
                {
                    this._quantity=value;
                }
            }
        }

    }
}

