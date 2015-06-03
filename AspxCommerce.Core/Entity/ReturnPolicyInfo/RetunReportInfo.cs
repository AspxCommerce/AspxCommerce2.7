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
    public class RetunReportInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_refundAmount", Order = 1)]
        private string _refundAmount;

        [DataMember(Name = "_shippingCost", Order = 2)]
        private string _shippingCost;

        [DataMember(Name = "_otherPostalCharges", Order = 3)]
        private string _otherPostalCharges;

        [DataMember(Name = "_quantity", Order = 4)]
        private int _quantity;

        [DataMember(Name = "_noOfReturns", Order = 5)]
        private int _noOfReturns;


        [DataMember(Name = "_addedOn", Order = 6)]
        private string _addedOn;



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



        public string RefundAmount
        {
            get
            {
                return this._refundAmount;
            }
            set
            {
                if ((this._refundAmount != value))
                {
                    this._refundAmount = value;
                }
            }
        }

        public string ShippingCost
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

        public string OtherPostalCharges
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



        public int NoOfReturns
        {
            get
            {
                return this._noOfReturns;
            }
            set
            {
                if ((this._noOfReturns != value))
                {
                    this._noOfReturns = value;
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
    public class ReturnReportBasicInfo
    {

        public string ReturnStatus { get; set; }
        public bool Monthly { get; set; }
        public bool Weekly { get; set; }
        public bool Hourly { get; set; }


    }
}
