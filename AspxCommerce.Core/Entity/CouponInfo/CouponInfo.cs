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
    public class CouponInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_couponID", Order = 1)]
        private System.Nullable<int> _couponID;

        [DataMember(Name = "_couponTypeID", Order = 2)]
        private System.Nullable<int> _couponTypeID;

        [DataMember(Name = "_couponType", Order = 3)]
        private string _couponType;

        [DataMember(Name = "_couponCode", Order = 4)]
        private string _couponCode;

        [DataMember(Name = "_numberOfUses", Order = 5)]
        private System.Nullable<int> _numberOfUses;

        //[DataMember(Name = "_CouponStatusID", Order = 6)]
        //private System.Nullable<int> _CouponStatusID;

        //[DataMember(Name = "_CouponStatus", Order = 7)]
        //private string _CouponStatus;

        [DataMember(Name = "_validateFrom", Order = 8)]
        private DateTime _validateFrom;

        [DataMember(Name = "_validateTo", Order = 9)]
        private DateTime _validateTo;

        [DataMember(Name = "_balanceAmount", Order = 10)]
        private System.Nullable<decimal> _balanceAmount;

        [DataMember(Name = "_isPerentage", Order = 11)]
        private System.Nullable<bool> _isPercentage;  

        [DataMember(Name = "_isFreeShipping", Order = 12)]
        private string _isFreeShipping;

        [DataMember(Name = "_minimumApplyAmount", Order = 12)]
        private decimal _minimumApplyAmount;

        [DataMember(Name = "_addedOn", Order = 13)]
        private string _addedOn;

        [DataMember(Name = "_updatedOn", Order = 14)]
        private string _updatedOn;

        [DataMember(Name = "_isActive", Order = 15)]
        private System.Nullable<bool> _isActive;

      

        public CouponInfo()
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

        public System.Nullable<int> CouponTypeID
        {
            get
            {
                return this._couponTypeID;
            }
            set
            {
                if ((this._couponTypeID != value))
                {
                    this._couponTypeID = value;
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

        public System.Nullable<int> NumberOfUses
        {
            get
            {
                return this._numberOfUses;
            }
            set
            {
                if ((this._numberOfUses != value))
                {
                    this._numberOfUses = value;
                }
            }
        }

        //public System.Nullable<int> CouponStatusID
        //{
        //    get
        //    {
        //        return this._CouponStatusID;
        //    }
        //    set
        //    {
        //        if ((this._CouponStatusID != value))
        //        {
        //            this._CouponStatusID = value;
        //        }
        //    }
        //}

        //public string CouponStatus
        //{
        //    get
        //    {
        //        return this._CouponStatus;
        //    }
        //    set
        //    {
        //        if ((this._CouponStatus != value))
        //        {
        //            this._CouponStatus = value;
        //        }
        //    }
        //}

        public DateTime ValidateFrom
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

        public DateTime ValidateTo
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


        public System.Nullable<decimal> BalanceAmount
        {
            get
            {
                return this._balanceAmount;
            }
            set
            {
                if ((this._balanceAmount != value))
                {
                    this._balanceAmount = value;
                }
            }
        }

        public System.Nullable<bool> IsPercentage
        {
            get
            {
                return this._isPercentage;
            }
            set
            {
                if ((this._isPercentage != value))
                {
                    this._isPercentage = value;
                }
            }
        }   

        public string IsFreeShipping
        {
            get
            {
                return this._isFreeShipping;
            }
            set
            {
                if ((this._isFreeShipping != value))
                {
                    this._isFreeShipping = value;
                }
            }
        }


        public decimal MinimumApplyAmount
        {
            get
            {
                return this._minimumApplyAmount;
            }
            set
            {
                if ((this._minimumApplyAmount != value))
                {
                    this._minimumApplyAmount = value;
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

        public string UpdatedOn
        {
            get
            {
                return this._updatedOn;
            }
            set
            {
                if ((this._updatedOn != value))
                {
                    this._updatedOn = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }

       

    }

    public class CouponCommonInfo
    {
        public int CouponID { get; set; }
        public int CouponTypeID { get; set; }
        public string CouponCode { get; set; }
        public decimal? CouponAmount { get; set; }
        public int? CouponStatusID { get; set; }
    }

    public class CouponSession
    {
        public string Key { get; set; }
        public bool IsPercentage { get; set; }
        public decimal Value { get; set; }
        public int AppliedCount { get; set; }
        public bool IsForFreeShipping { get; set; }
    }

    public class GetCouponDetailsInfo
    {
        public int? CouponTypeID { get; set; }
        public string CouponCode { get; set; }
        public string ValidateFrom { get; set; }
        public string ValidateTo { get; set; }
    }

    public class CouponSaveObj:CouponCommonInfo
    {
        public string IsPercentage { get; set; }
        public string ValidateFrom { get; set; }
        public string ValidateTo { get; set; }
        public bool IsActive { get; set; }
        public string SettingIDs { get; set; }
        public string SettingValues { get; set; }
        public string PortalUser_UserName { get; set; }
    }
    public class PromoCodeSaveObj:CouponSaveObj
    {
        public bool IsFreeShipping { get; set; }
        public int UsesPerItem { get; set; }
        public string PromoItems { get; set; }
        public decimal ApplyAmountRange { get; set; }
    }
    public class GetCouponUserDetailInfo : CouponCommonInfo
    {
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
