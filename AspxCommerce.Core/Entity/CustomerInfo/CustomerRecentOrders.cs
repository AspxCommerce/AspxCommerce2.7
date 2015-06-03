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
    public class CustomerRecentOrders
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_orderID", Order = 1)]
        private int _orderID;

        [DataMember(Name = "_orderID", Order = 2)]
        private string _orderDate;

        [DataMember(Name = "_billingAddress", Order = 2)]
        private string _billingAddress;

        [DataMember(Name = "_shippingAddress", Order = 3)]
        private string _shippingAddress;

        [DataMember(Name = "_shippingAddress", Order = 4)]
        private string _orderTotal;

        public CustomerRecentOrders()
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

        public int OrderID
        {
            get
            {
                return this._orderID;
            }
            set
            {
                if ((this._orderID != value))
                {
                    this._orderID = value;
                }
            }
        }

        public string OrderDate
        {
            get
            {
                return this._orderDate;
            }
            set
            {
                if ((this._orderDate != value))
                {
                    this._orderDate = value;
                }
            }
        }

        public string BillingAddress
        {
            get
            {
                return this._billingAddress;
            }
            set
            {
                if ((this._billingAddress != value))
                {
                    this._billingAddress = value;
                }
            }
        }

        public string ShippingAddress
        {
            get
            {
                return this._shippingAddress;
            }
            set
            {
                if ((this._shippingAddress != value))
                {
                    this._shippingAddress = value;
                }
            }
        }

        public string OrderTotal
        {
            get
            {
                return this._orderTotal;
            }
            set
            {
                if ((this._orderTotal != value))
                {
                    this._orderTotal = value;
                }
            }
        }
    }
}
