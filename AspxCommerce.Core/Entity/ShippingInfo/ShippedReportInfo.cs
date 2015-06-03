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
    public class ShippedReportInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_shippingMethodID", Order = 1)]
        private int _shippingMethodID;

        [DataMember(Name = "_shippingMethodName", Order = 2)]
        private string _shippingMethodName;

        [DataMember(Name = "_numberOfOrders", Order = 3)]
        private int _numberOfOrders;

        [DataMember(Name = "_totalShipping", Order = 4)]
        private int _totalShipping;

        [DataMember(Name = "_addedOn", Order = 5)]
        private string _addedOn;

        public ShippedReportInfo()
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

        public int NumberOfOrders
        {
            get
            {
                return this._numberOfOrders;
            }
            set
            {
                if ((this._numberOfOrders != value))
                {
                    this._numberOfOrders = value;
                }
            }
        }

        public int TotalShipping
        {
            get
            {
                return this._totalShipping;
            }
            set
            {
                if ((this._totalShipping != value))
                {
                    this._totalShipping = value;
                }
            }
        }
        public string AddedOn
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
    }
    public class ShippedReportBasicInfo
    {
        public string ShippingMethodName { get; set; }
        public bool Monthly { get; set; }
        public bool Weekly { get; set; }
        public bool Hourly { get; set; }



    }
}
