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
    public class CouponPerSales
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_couponCode", Order = 1)]
        private string _couponCode;

        [DataMember(Name = "_useCount", Order = 2)]
        private int _useCount;

        [DataMember(Name = "_totalAmountDiscountedbyCoupon", Order = 3)]
        private System.Nullable<decimal> _totalAmountDiscountedbyCoupon;

        [DataMember(Name = "_totalSalesAmount", Order = 4)]
        private System.Nullable<decimal> _totalSalesAmount;


        public CouponPerSales()
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

        public string CouponCode
        {
            get
            {
                return this._couponCode;
            }
            set
            {
                if ((this._couponCode != value))
                {
                    this._couponCode = value;
                }
            }
        }

        public int UseCount
        {
            get
            {
                return this._useCount;
            }
            set
            {
                if ((this._useCount != value))
                {
                    this._useCount = value;
                }
            }
        }

        public System.Nullable<decimal> TotalAmountDiscountedbyCoupon
        {
            get
            {
                return this._totalAmountDiscountedbyCoupon;
            }
            set
            {
                if ((this._totalAmountDiscountedbyCoupon != value))
                {
                    this._totalAmountDiscountedbyCoupon = value;
                }
            }
        }

        public System.Nullable<decimal> TotalSalesAmount
        {
            get
            {
                return this._totalSalesAmount;
            }
            set
            {
                if ((this._totalSalesAmount != value))
                {
                    this._totalSalesAmount = value;
                }
            }
        }
    }
}
