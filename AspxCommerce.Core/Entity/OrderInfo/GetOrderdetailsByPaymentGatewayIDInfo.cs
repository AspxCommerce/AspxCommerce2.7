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
    public class GetOrderdetailsByPaymentGatewayIDInfo
    {
       
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_paymentGatewayTypeID", Order = 1)]
        private int _paymentGatewayTypeID;

        [DataMember(Name = "_orderID", Order = 2)]
        private int _orderID;

        [DataMember(Name = "_storeID", Order = 3)]
        private int _storeID;

        [DataMember(Name = "_addedOn", Order = 4)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_billToName", Order = 5)]
        private string _billToName;

        [DataMember(Name = "_shipToName", Order = 6)]
        private string _shipToName;

        [DataMember(Name = "_grandTotal", Order = 7)]
        private decimal _grandTotal;

        [DataMember(Name = "_orderStatusName", Order = 8)]
        private string _orderStatusName;
        [DataMember(Name = "_isMultipleShipping", Order = 9)]
        private System.Nullable<bool> _isMultipleShipping;
        
        public GetOrderdetailsByPaymentGatewayIDInfo()
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

        public int PaymentGatewayTypeID
        {
            get
            {
                return this._paymentGatewayTypeID;
            }
            set
            {
                if ((this._paymentGatewayTypeID != value))
                {
                    this._paymentGatewayTypeID = value;
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

        public int StoreID
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

        public string BillToName
        {
            get
            {
                return this._billToName;
            }
            set
            {
                if ((this._billToName != value))
                {
                    this._billToName = value;
                }
            }
        }

        public string ShipToName
        {
            get
            {
                return this._shipToName;
            }
            set
            {
                if ((this._shipToName != value))
                {
                    this._shipToName = value;
                }
            }
        }

        public decimal GrandTotal
        {
            get
            {
                return this._grandTotal;
            }
            set
            {
                if ((this._grandTotal != value))
                {
                    this._grandTotal = value;
                }
            }
        }
        public string OrderStatusName
        {
            get
            {
                return this._orderStatusName;
            }
            set
            {
                if ((this._orderStatusName != value))
                {
                    this._orderStatusName = value;
                }
            }
        }

        public System.Nullable<bool> IsMultipleShipping
        {
            get
            {
                return this._isMultipleShipping;
            }
            set
            {
                if ((this._isMultipleShipping != value))
                {
                    this._isMultipleShipping = value;
                }
            }
        }
    }
}
