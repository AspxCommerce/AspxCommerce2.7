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
    public class CouponUserInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_CuponUserID", Order = 1)]
        private System.Nullable<int> _couponUserID;

        [DataMember(Name = "_couponID", Order = 2)]
        private System.Nullable<int> _couponID;

        [DataMember(Name = "_couponCode", Order = 3)]
        private string _couponCode;

        [DataMember(Name = "_userName", Order = 4)]
        private string _userName;

        [DataMember(Name = "_couponLife", Order = 5)]
        private string _couponLife;

        [DataMember(Name = "_noOfUse", Order = 6)]
        private System.Nullable<int> _noOfUse;

        [DataMember(Name = "_couponStatusID", Order = 7)]
        private System.Nullable<int> _couponStatusID;

        [DataMember(Name = "_couponStatus", Order = 8)]
        private string _couponStatus;

        [DataMember(Name = "_validateFrom", Order = 9)]
        private System.Nullable<System.DateTime> _validateFrom;

        [DataMember(Name = "_validateTo", Order = 10)]
        private System.Nullable<System.DateTime> _validateTo;   


        public CouponUserInfo()
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


        public System.Nullable<int> CouponUserID
        {
            get
            {
                return this._couponUserID;
            }
            set
            {
                if ((this._couponUserID != value))
                {
                    this._couponUserID = value;
                }
            }
        }


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

        public string CouponLife
        {
            get
            {
                return this._couponLife;
            }
            set
            {
                if ((this._couponLife != value))
                {
                    this._couponLife = value;
                }
            }
        }


        public System.Nullable<int> NoOfUse
        {
            get
            {
                return this._noOfUse;
            }
            set
            {
                if ((this._noOfUse != value))
                {
                    this._noOfUse = value;
                }
            }
        }


        public System.Nullable<int> CouponStatusID
        {
            get
            {
                return this._couponStatusID;
            }
            set
            {
                if ((this._couponStatusID != value))
                {
                    this._couponStatusID = value;
                }
            }
        }


        public string CouponStatus
        {
            get
            {
                return this._couponStatus;
            }
            set
            {
                if ((this._couponStatus != value))
                {
                    this._couponStatus = value;
                }
            }
        }


        public System.Nullable<System.DateTime> ValidateFrom
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

        public System.Nullable<System.DateTime> ValidateTo
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
