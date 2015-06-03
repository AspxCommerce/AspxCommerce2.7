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
    public class ReturnDetailsInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_returnID", Order = 1)]
        private System.Nullable<int> _returnID;
        [DataMember(Name = "_orderID", Order = 2)]
        private System.Nullable<int> _orderID;
        [DataMember(Name = "_itemID", Order = 3)]
        private System.Nullable<int> _itemID;
        [DataMember(Name = "_SKU", Order = 4)]
        private string _SKU;
        [DataMember(Name = "_orderedDate", Order = 5)]
        private string _orderedDate;
        [DataMember(Name = "_customerID", Order = 6)]
        private System.Nullable<int> _customerID;
        [DataMember(Name = "_customerName", Order = 7)]
        private string _customerName;
        [DataMember(Name = "_returnStatus", Order = 8)]
        private string _returnStatus;
        [DataMember(Name = "_dateAdded", Order = 9)]
        private string _dateAdded;
        [DataMember(Name = "_dateModified", Order =10)]
        private string _dateModified;
        [DataMember(Name = "_returnActionID", Order = 11)]
        private string _returnActionID;
        [DataMember(Name = "_shippingMethodID", Order = 12)]
        private string _shippingMethodID;
        [DataMember(Name = "_userName", Order = 13)]
        private string _userName;
        [DataMember(Name = "_otherPostalCharges", Order = 14)]
        private decimal _otherPostalCharges;



        public ReturnDetailsInfo()
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
        public System.Nullable<int> ReturnID
        {
            get
            {
                return this._returnID;
            }
            set
            {
                if ((this._returnID != value))
                {
                    this._returnID = value;
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
        public string OrderedDate
        {
            get
            {
                return this._orderedDate;
            }
            set
            {
                if ((this._orderedDate != value))
                {
                    this._orderedDate = value;
                }
            }
        }


        public System.Nullable<int> CustomerID
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





        public string ReturnStatus
        {
            get
            {
                return this._returnStatus;
            }
            set
            {
                if ((this._returnStatus != value))
                {
                    this._returnStatus = value;
                }
            }
        }



        public string DateAdded
        {
            get
            {
                return this._dateAdded;
            }
            set
            {
                if ((this._dateAdded != value))
                {
                    this._dateAdded = value;
                }
            }
        }

        public string DateModified
        {
            get
            {
                return this._dateModified;
            }
            set
            {
                if ((this._dateModified != value))
                {
                    this._dateModified = value;
                }
            }
        }

        public string ReturnActionID
        {
            get
            {
                return this._returnActionID;
            }
            set
            {
                if ((this._returnActionID != value))
                {
                    this._returnActionID = value;
                }
            }
        }

        public string ShippingMethodID
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
        public decimal OtherPostalCharges
        {
            get
            {
                return this._otherPostalCharges;
            }
            set
            {
                if ((this._otherPostalCharges != value))
                {
                    this._otherPostalCharges = value;
                }
            }
        }





    }
    public class RetunDetailsBasicInfo
    {

        public System.Nullable<int> ReturnID { get; set; }
        public System.Nullable<int> OrderID { get; set; }
        public int ReturnActionID { get; set; }
        public int ReturnStatusID { get; set; }
        public int shippingMethodID { get; set; }
        public decimal ShippingCost { get; set; }
        public string OtherPostalCharges { get; set; }
        public string CommentText { get; set; }
        public int IsCustomerNotifiedByEmail { get; set; }
        public string CustomerName { get; set; }
        public string ReturnStatus { get; set; }
        public string DateAdded { get; set; }
        public string DateModified { get; set; }

    }

}
