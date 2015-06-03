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
    public class CouponDetailFrontInfo
    {
        public CouponDetailFrontInfo()
        {
        }

        private System.Nullable<int> _couponID;
        private string _couponType;

        private string _couponCode;

        private System.Nullable<decimal> _couponAmount;

        private string _validateFrom;

        private string _validateTo;


        public System.Nullable<int> CouponID
        {
            get
            {
                return this._couponID;
            }
            set
            {
                if ((this._couponID != value))
                {
                    this._couponID = value;
                }
            }
        }

        public string CouponType
        {
            get
            {
                return this._couponType;
            }
            set
            {
                if ((this._couponType != value))
                {
                    this._couponType = value;
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

        public System.Nullable<decimal> CouponAmount
        {
            get
            {
                return this._couponAmount;
            }
            set
            {
                if ((this._couponAmount != value))
                {
                    this._couponAmount = value;
                }
            }
        }

        public string ValidateFrom
        {
            get
            {
                return this._validateFrom;
            }
            set
            {
                if ((this._validateFrom != value))
                {
                    this._validateFrom = value;
                }
            }
        }

        public string ValidateTo
        {
            get
            {
                return this._validateTo;
            }
            set
            {
                if ((this._validateTo != value))
                {
                    this._validateTo = value;
                }
            }
        }
    }
}
