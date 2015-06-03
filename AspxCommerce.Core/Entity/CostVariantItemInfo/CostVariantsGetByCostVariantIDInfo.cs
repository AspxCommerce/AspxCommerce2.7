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
    public class CostVariantsGetByCostVariantIDInfo
    {
        private int _costVariantID;

        private string _costVariantName;

        private int _inputTypeID;

        private System.Nullable<int> _displayOrder;

        private System.Nullable<bool> _showInSearch;

        private System.Nullable<bool> _showInGrid;

        private System.Nullable<bool> _showInAdvanceSearch;

        private System.Nullable<bool> _showInComparison;

        private System.Nullable<bool> _isEnableSorting;

        private System.Nullable<bool> _isSystemUsed;

        private System.Nullable<bool> _isUseInFilter;

        private System.Nullable<bool> _isIncludeInPriceRule;

        private System.Nullable<bool> _isIncludeInPromotions;

        private System.Nullable<bool> _isShownInRating;

        private System.Nullable<bool> _isUsedInConfigItem;

        private System.Nullable<bool> _flag;

        private string _description;

        private System.Nullable<bool> _isActive;


        private System.Nullable<bool> _isModified;

        private string _otherDisplayOrders;


        public CostVariantsGetByCostVariantIDInfo()
        {
        }

        [DataMember]
        public int CostVariantID
        {
            get { return this._costVariantID; }
            set
            {
                if ((this._costVariantID != value))
                {
                    this._costVariantID = value;
                }
            }
        }

        [DataMember]
        public string CostVariantName
        {
            get { return this._costVariantName; }
            set
            {
                if ((this._costVariantName != value))
                {
                    this._costVariantName = value;
                }
            }
        }

        [DataMember]
        public int InputTypeID
        {
            get { return this._inputTypeID; }
            set
            {
                if ((this._inputTypeID != value))
                {
                    this._inputTypeID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> DisplayOrder
        {
            get { return this._displayOrder; }
            set
            {
                if ((this._displayOrder != value))
                {
                    this._displayOrder = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> ShowInSearch
        {
            get { return this._showInSearch; }
            set
            {
                if ((this._showInSearch != value))
                {
                    this._showInSearch = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> ShowInGrid
        {
            get { return this._showInGrid; }
            set
            {
                if ((this._showInGrid != value))
                {
                    this._showInGrid = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> ShowInAdvanceSearch
        {
            get { return this._showInAdvanceSearch; }
            set
            {
                if ((this._showInAdvanceSearch != value))
                {
                    this._showInAdvanceSearch = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> ShowInComparison
        {
            get { return this._showInComparison; }
            set
            {
                if ((this._showInComparison != value))
                {
                    this._showInComparison = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsEnableSorting
        {
            get { return this._isEnableSorting; }
            set
            {
                if ((this._isEnableSorting != value))
                {
                    this._isEnableSorting = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsSystemUsed
        {
            get { return this._isSystemUsed; }
            set
            {
                if ((this._isSystemUsed != value))
                {
                    this._isSystemUsed = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsUseInFilter
        {
            get { return this._isUseInFilter; }
            set
            {
                if ((this._isUseInFilter != value))
                {
                    this._isUseInFilter = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsIncludeInPriceRule
        {
            get { return this._isIncludeInPriceRule; }
            set
            {
                if ((this._isIncludeInPriceRule != value))
                {
                    this._isIncludeInPriceRule = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsIncludeInPromotions
        {
            get { return this._isIncludeInPromotions; }
            set
            {
                if ((this._isIncludeInPromotions != value))
                {
                    this._isIncludeInPromotions = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsShownInRating
        {
            get { return this._isShownInRating; }
            set
            {
                if ((this._isShownInRating != value))
                {
                    this._isShownInRating = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsUsedInConfigItem
        {
            get { return this._isUsedInConfigItem; }
            set
            {
                if ((this._isUsedInConfigItem != value))
                {
                    this._isUsedInConfigItem = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> Flag
        {
            get { return this._flag; }
            set
            {
                if ((this._flag != value))
                {
                    this._flag = value;
                }
            }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set
            {
                if ((this._description != value))
                {
                    this._description = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsActive
        {
            get { return this._isActive; }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsModified
        {
            get { return this._isModified; }
            set
            {
                if ((this._isModified != value))
                {
                    this._isModified = value;
                }
            }
        }

        [DataMember]
        public String OtherDisplayOrders
        {
            get { return this._otherDisplayOrders; }
            set
            {
                if ((this._otherDisplayOrders != value))
                {
                    this._otherDisplayOrders = value;
                }
            }
        }

    }
}
