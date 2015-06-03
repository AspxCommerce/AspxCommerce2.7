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
    public class InvoiceDetailsInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_invoiceNumber", Order = 1)]
        private string _invoiceNumber;

        [DataMember(Name = "_invoiceDate", Order = 2)]
        private string _invoiceDate;

        [DataMember(Name = "_orderID", Order = 3)]
        private System.Nullable<int> _orderID;

        [DataMember(Name = "_customerName", Order = 4)]
        private string _customerName;

        [DataMember(Name = "_orderDate", Order = 5)]
        private string _orderDate;

        [DataMember(Name = "_billToName", Order = 6)]
        private string _billToName;

        [DataMember(Name = "_shipToName", Order = 7)]
        private string _shipToName;

        [DataMember(Name = "_orderStatusName", Order = 8)]
        private string _orderStatusName;

        [DataMember(Name = "_amount", Order = 9)]
        private System.Nullable<decimal> _amount;

        [DataMember(Name = "_customerEmail", Order = 10)]
        private string _customerEmail;



        public InvoiceDetailsInfo()
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

        public string InvoiceNumber
        {
            get
            {
                return this._invoiceNumber;
            }
            set
            {
                if ((this._invoiceNumber != value))
                {
                    this._invoiceNumber = value;
                }
            }
        }

        public string InvoiceDate
        {
            get
            {
                return this._invoiceDate;
            }
            set
            {
                if ((this._invoiceDate != value))
                {
                    this._invoiceDate = value;
                }
            }
        }

        public System.Nullable<int> OrderID
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

        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                if ((this._customerName != value))
                {
                    this._customerName = value;
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

        public System.Nullable<decimal> Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                if ((this._amount != value))
                {
                    this._amount = value;
                }
            }
        }

        public string CustomerEmail
        {
            get
            {
                return this._customerEmail;
            }
            set
            {
                if ((this._customerEmail != value))
                {
                    this._customerEmail = value;
                }
            }
        }
    }
    public class InvoiceBasicInfo
    {
        public string InvoiceNumber { get; set; }
        public string BillToName { get; set; }
        public string OrderStatusName { get; set; }


    }
}
