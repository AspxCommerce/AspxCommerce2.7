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
    public class ShippingMethodInfoByProvider
    {
        public int RowTotal { get; set; }
        public int ShippingMethodID { get; set; }
        public string ShippingMethodName { get; set; }
        public System.Nullable<decimal> WeightLimitFrom { get; set; }
        public System.Nullable<decimal> WeightLimitTo { get; set; }
        public string DeliveryTime { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ShippingMethodInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_shippingMethodID", Order = 1)]
        private int _shippingMethodID;

        [DataMember(Name = "_shippingMethodName", Order = 2)]
        private string _shippingMethodName;

        [DataMember(Name = "_shippingProviderID", Order = 3)]
        private int _shippingProviderID;

        [DataMember(Name = "_imagePath", Order = 4)]
        private string _imagePath;

        [DataMember(Name = "_alternateText", Order = 5)]
        private string _alternateText;

        [DataMember(Name = "_displayOrder", Order = 6)]
        private int _displayOrder;

        [DataMember(Name = "_deliveryTime", Order = 7)]
        private string _deliveryTime;

        [DataMember(Name = "_weightLimitFrom", Order = 8)]
        private System.Nullable<decimal> _weightLimitFrom;

        [DataMember(Name = "_weightLimitTo", Order = 9)]
        private System.Nullable<decimal> _weightLimitTo;

        [DataMember(Name = "_isActive", Order = 10)]
        private string _isActive;


        [DataMember(Name = "_addedOn", Order = 11)]
        private System.Nullable<System.DateTime> _addedOn;


        [DataMember(Name = "_addedBy", Order = 12)]
        private string _addedBy;

        [DataMember(Name = "_shippingCost", Order = 13)]
        private string _shippingCost;
        [DataMember(Name = "_isRealTime", Order = 14)]
        private bool _isRealTime;


        public ShippingMethodInfo()
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

        public int ShippingProviderID
        {
            get
            {
                return this._shippingProviderID;
            }
            set
            {
                if ((this._shippingProviderID != value))
                {
                    this._shippingProviderID = value;
                }
            }
        }

        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                if ((this._imagePath != value))
                {
                    this._imagePath = value;
                }
            }
        }

        public string AlternateText
        {
            get
            {
                return this._alternateText;
            }
            set
            {
                if ((this._alternateText != value))
                {
                    this._alternateText = value;
                }
            }
        }

        public int DisplayOrder
        {
            get
            {
                return this._displayOrder;
            }
            set
            {
                if ((this._displayOrder != value))
                {
                    this._displayOrder = value;
                }
            }
        }

        public string DeliveryTime
        {
            get
            {
                return this._deliveryTime;
            }
            set
            {
                if ((this._deliveryTime != value))
                {
                    this._deliveryTime = value;
                }
            }
        }

        public System.Nullable<decimal> WeightLimitFrom
        {
            get
            {
                return this._weightLimitFrom;
            }
            set
            {
                if ((this._weightLimitFrom != value))
                {
                    this._weightLimitFrom = value;
                }
            }
        }

        public System.Nullable<decimal> WeightLimitTo
        {
            get
            {
                return this._weightLimitTo;
            }
            set
            {
                if ((this._weightLimitTo != value))
                {
                    this._weightLimitTo = value;
                }
            }
        }

        public string IsActive
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

        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                if ((this._addedBy != value))
                {
                    this._addedBy = value;
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

        public bool IsRealTime
        {
            get
            {
                return this._isRealTime;
            }
            set
            {
                if ((this._isRealTime != value))
                {
                    this._isRealTime = value;
                }
            }
        }

    }

}
