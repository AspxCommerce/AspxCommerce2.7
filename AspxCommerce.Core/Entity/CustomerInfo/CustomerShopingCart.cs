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
    public class CustomerShopingCart
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_customerID", Order = 1)]
        private int _customerID;

        [DataMember(Name = "_productID", Order = 3)]
        private string _productID;

        [DataMember(Name = "_productName", Order = 4)]
        private string _productName;

        [DataMember(Name = "_SKU", Order = 5)]
        private string _SKU;

        [DataMember(Name = "_qTY", Order = 6)]
        private int _qTY;

        [DataMember(Name = "_price", Order = 6)]
        private double _price;

        [DataMember(Name = "_total", Order = 6)]
        private double _total;

        public CustomerShopingCart()
        {
        }

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

        public int CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }

        public string ProductID
        {
            get
            {
                return this._productID;
            }
            set
            {
                if ((this._productID != value))
                {
                    this._productID = value;
                }
            }
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                if ((this._productName != value))
                {
                    this._productName = value;
                }
            }
        }

        public string SKU
        {
            get
            {
                return this._SKU;
            }
            set
            {
                if ((this._SKU != value))
                {
                    this._SKU = value;
                }
            }
        }

        public int QTY
        {
            get
            {
                return this._qTY;
            }
            set
            {
                if ((this._qTY != value))
                {
                    this._qTY = value;
                }
            }
        }

        public Double Price
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
        public Double Total
        {
            get
            {
                return this._total;
            }
            set
            {
                if ((this._total != value))
                {
                    this._total = value;
                }
            }
        }
    }
}
