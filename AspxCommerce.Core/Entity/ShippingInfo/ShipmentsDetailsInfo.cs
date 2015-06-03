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
    public class ShipmentsDetailsInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_shippingMethodID", Order = 1)]
        private System.Nullable<int> _shippingMethodID;

        [DataMember(Name = "_shippingMethodName", Order = 2)]
        private string _shippingMethodName;

        [DataMember(Name = "_orderID", Order = 3)]
        private System.Nullable<int> _orderID;

        //[DataMember(Name = "_ShippedDate", Order = 4)]
        //private string _ShippedDate;

        [DataMember(Name = "_shipToName", Order = 5)]
        private string _shipToName;

        [DataMember(Name = "_shippingRate", Order = 6)]
        private System.Nullable<decimal> _shippingRate;

        public ShipmentsDetailsInfo()
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

        public System.Nullable<int> ShippingMethodID
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

        //public string ShippedDate
        //{
        //    get
        //    {
        //        return this._ShippedDate;
        //    }
        //    set
        //    {
        //        if ((this._ShippedDate != value))
        //        {
        //            this._ShippedDate = value;
        //        }
        //    }
        //}

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

        public System.Nullable<decimal> ShippingRate
        {
            get
            {
                return this._shippingRate;
            }
            set
            {
                if ((this._shippingRate != value))
                {
                    this._shippingRate = value;
                }
            }
        }
    }
    public class ShipmentsBasicinfo
    {
        public System.Nullable<int> ShippingMethodID { get; set; }
        public string ShippingMethodName { get; set; }
        public System.Nullable<int> OrderID { get; set; }
        public string ShipToName { get; set; }
        public string ShippingRate { get; set; }



    }
}
