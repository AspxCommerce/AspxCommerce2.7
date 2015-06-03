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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{

    [DataContract]
    [Serializable]
    public class CatalogPricingRuleInfo
    {
        private CatalogPriceRule _catalogPriceRule;

        private List<CatalogPriceRuleCondition> _catalogPriceRuleConditions;

        private List<CatalogPriceRuleRole> _catalogPriceRuleRoles;

        [DataMember(Name = "CatalogPriceRule", Order = 0)]
        public CatalogPriceRule CatalogPriceRule
        {
            get { return _catalogPriceRule; }
            set { _catalogPriceRule = value; }
        }

        [DataMember(Name = "CatalogPriceRuleConditions", Order = 1)]
        public List<CatalogPriceRuleCondition> CatalogPriceRuleConditions
        {
            get { return _catalogPriceRuleConditions; }
            set { _catalogPriceRuleConditions = value; }
        }

        [DataMember(Name = "CatalogPriceRuleRoles", Order = 2)]
        public List<CatalogPriceRuleRole> CatalogPriceRuleRoles
        {
            get { return _catalogPriceRuleRoles; }
            set { _catalogPriceRuleRoles = value; }
        }

       
    }

    [DataContract]
    [Serializable]
    public class CatalogConditionDetail
    {
        private System.Nullable<int> _catalogPriceRuleConditionID;

        private System.Nullable<int> _catalogPriceRuleID;

        private System.Nullable<int> _attributeID;

        private System.Nullable<int> _ruleOperatorID;

        private string _value;

        private System.Nullable<int> _priority;

        public CatalogConditionDetail()
        {
        }

        [DataMember(Name = "CatalogPriceRuleConditionID", Order = 0)]
        public System.Nullable<int> CatalogPriceRuleConditionID
        {
            get
            {
                return this._catalogPriceRuleConditionID;
            }
            set
            {
                if ((this._catalogPriceRuleConditionID != value))
                {
                    this._catalogPriceRuleConditionID = value;
                }
            }
        }

        [DataMember(Name = "CatalogPriceRuleID", Order = 1)]
        public System.Nullable<int> CatalogPriceRuleID
        {
            get
            {
                return this._catalogPriceRuleID;
            }
            set
            {
                if ((this._catalogPriceRuleID != value))
                {
                    this._catalogPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "AttributeID", Order = 2)]
        public System.Nullable<int> AttributeID
        {
            get
            {
                return this._attributeID;
            }
            set
            {
                if ((this._attributeID != value))
                {
                    this._attributeID = value;
                }
            }
        }

        [DataMember(Name = "RuleOperatorID", Order = 3)]
        public System.Nullable<int> RuleOperatorID
        {
            get
            {
                return this._ruleOperatorID;
            }
            set
            {
                if ((this._ruleOperatorID != value))
                {
                    this._ruleOperatorID = value;
                }
            }
        }

        [DataMember(Name = "Value", Order = 4)]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if ((this._value != value))
                {
                    this._value = value;
                }
            }
        }

        [DataMember(Name = "Priority", Order = 5)]
        public System.Nullable<int> Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                if ((this._priority != value))
                {
                    this._priority = value;
                }
            }
        }
    }

    [DataContract]
    [Serializable]
    public class CatalogPriceRule
    {
        private int _catalogPriceRuleID;

        private string _catalogPriceRuleName;

        private string _catalogPriceRuleDescription;

        private System.Nullable<int> _apply;

        private string _value;

        private System.Nullable<bool> _isFurtherProcessing;

        private System.Nullable<DateTime> _fromDate;

        private System.Nullable<DateTime> _toDate;

        private System.Nullable<int> _priority;

        private System.Nullable<bool> _isActive;
        //private CatalogPriceRuleCondition _CatalogPriceRuleConditions;

        public CatalogPriceRule()
        {

        }
        [DataMember(Name = "CatalogPriceRuleID", Order = 0)]
        public int CatalogPriceRuleID
        {
            get
            {
                return this._catalogPriceRuleID;
            }
            set
            {
                if ((this._catalogPriceRuleID != value))
                {
                    this._catalogPriceRuleID = value;
                }
            }
        }
        [DataMember(Name = "CatalogPriceRuleName", Order = 1)]
        public string CatalogPriceRuleName
        {
            get
            {
                return this._catalogPriceRuleName;
            }
            set
            {
                if ((this._catalogPriceRuleName != value))
                {
                    this._catalogPriceRuleName = value;
                }
            }
        }
        [DataMember(Name = "CatalogPriceRuleDescription", Order = 2)]
        public string CatalogPriceRuleDescription
        {
            get
            {
                return this._catalogPriceRuleDescription;
            }
            set
            {
                if ((this._catalogPriceRuleDescription != value))
                {
                    this._catalogPriceRuleDescription = value;
                }
            }
        }
        [DataMember(Name = "Apply", Order = 3)]
        public System.Nullable<int> Apply
        {
            get
            {
                return this._apply;
            }
            set
            {
                if ((this._apply != value))
                {
                    this._apply = value;
                }
            }
        }
        [DataMember(Name = "Value", Order = 4)]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if ((this._value != value))
                {
                    this._value = value;
                }
            }
        }
        [DataMember(Name = "IsFurtherProcessing", Order = 5)]
        public System.Nullable<bool> IsFurtherProcessing
        {
            get
            {
                return this._isFurtherProcessing;
            }
            set
            {
                if ((this._isFurtherProcessing != value))
                {
                    this._isFurtherProcessing = value;
                }
            }
        }
        [DataMember(Name = "FromDate", Order = 6)]
        public System.Nullable<DateTime> FromDate
        {
            get
            {
                return this._fromDate;
            }
            set
            {
                if ((this._fromDate != value))
                {
                    this._fromDate = value;
                }
            }
        }
        [DataMember(Name = "ToDate", Order = 7)]
        public System.Nullable<DateTime> ToDate
        {
            get
            {
                return this._toDate;
            }
            set
            {
                if ((this._toDate != value))
                {
                    this._toDate = value;
                }
            }
        }
        [DataMember(Name = "Priority", Order = 8)]
        public System.Nullable<int> Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                if ((this._priority != value))
                {
                    this._priority = value;
                }
            }
        }
        [DataMember(Name = "IsActive", Order = 9)]
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

    [DataContract]
    [Serializable]
    public class CatalogPriceRuleCondition
    {
        private int _catalogPriceRuleConditionID;

        private System.Nullable<int> _catalogPriceRuleID;

        private System.Nullable<bool> _isAll;

        private System.Nullable<bool> _isTrue;

        private int _parentID;

        private List<CatalogConditionDetail> _catalogConditionDetail;        

        public CatalogPriceRuleCondition()
        {
        }

        [DataMember(Name = "CatalogPriceRuleConditionID", Order = 0)]
        public int CatalogPriceRuleConditionID
        {
            get
            {
                return this._catalogPriceRuleConditionID;
            }
            set
            {
                if ((this._catalogPriceRuleConditionID != value))
                {
                    this._catalogPriceRuleConditionID = value;
                }
            }
        }

        [DataMember(Name = "CatalogPriceRuleID", Order = 1)]
        public System.Nullable<int> CatalogPriceRuleID
        {
            get
            {
                return this._catalogPriceRuleID;
            }
            set
            {
                if ((this._catalogPriceRuleID != value))
                {
                    this._catalogPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "IsAll", Order = 2)]
        public System.Nullable<bool> IsAll
        {
            get
            {
                return this._isAll;
            }
            set
            {
                if ((this._isAll != value))
                {
                    this._isAll = value;
                }
            }
        }

        [DataMember(Name = "IsTrue", Order = 3)]
        public System.Nullable<bool> IsTrue
        {
            get
            {
                return this._isTrue;
            }
            set
            {
                if ((this._isTrue != value))
                {
                    this._isTrue = value;
                }
            }
        }

        [DataMember(Name = "ParentID", Order = 4)]
        public int ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                if ((this._parentID != value))
                {
                    this._parentID = value;
                }
            }
        }

        [DataMember(Name = "CatalogConditionDetail", Order = 5)]
        public List<CatalogConditionDetail> CatalogConditionDetail
        {
            get
            {
                return this._catalogConditionDetail;
            }
            set
            {
                this._catalogConditionDetail = value;

            }
        }
    }

    [DataContract]
    [Serializable]
    public class CatalogPriceRuleRole
    {
        private int _catalogPriceRuleID;

        private string _roleID;

        public CatalogPriceRuleRole()
        {
        }
        [DataMember(Name = "CatalogPriceRuleID", Order = 0)]
        public int CatalogPriceRuleID
        {
            get
            {
                return this._catalogPriceRuleID;
            }
            set
            {
                if ((this._catalogPriceRuleID != value))
                {
                    this._catalogPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "RoleID", Order = 1)]
        public string RoleID
        {
            get
            {
                return this._roleID;
            }
            set
            {
                if ((this._roleID != value))
                {
                    this._roleID = value;
                }
            }
        }
    }
}
