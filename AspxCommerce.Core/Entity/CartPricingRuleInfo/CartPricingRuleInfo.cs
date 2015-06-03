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
    public class CartPricingRuleInfo
    {
        private CartPriceRule _cartPriceRule;

        private List<RuleCondition> _lstRuleCondition;

        private List<CartPriceRuleCondition> _lstCartPriceRuleConditions;

        private List<ProductAttributeRuleCondition> _lstProductAttributeRuleCondition;

        private List<ProductSubSelectionRuleCondition> _lstProductSubSelectionRuleCondition;

        private List<CartPriceRuleRole> _lstCartPriceRuleRoles;

        private List<CartPriceRuleStore> _lstCartPriceRuleStores;

       
        [DataMember(Name = "CartPriceRule", Order = 0)]
        public CartPriceRule CartPriceRule
        {
            get { return _cartPriceRule; }
            set { _cartPriceRule = value; }
        }

        [DataMember(Name = "LstRuleCondition", Order = 1)]
        public List<RuleCondition> LstRuleCondition
        {
            get { return _lstRuleCondition; }
            set { _lstRuleCondition = value; }
        }


        [DataMember(Name = "LstCartPriceRuleConditions", Order = 2)]
        public List<CartPriceRuleCondition> LstCartPriceRuleConditions
        {
            get { return _lstCartPriceRuleConditions; }
            set { _lstCartPriceRuleConditions = value; }
        }

        [DataMember(Name = "LstProductAttributeRuleCondition", Order = 3)]
        public List<ProductAttributeRuleCondition> LstProductAttributeRuleCondition
        {
            get { return _lstProductAttributeRuleCondition; }
            set { _lstProductAttributeRuleCondition = value; }
        }

        [DataMember(Name = "LstProductSubSelectionRuleCondition", Order = 4)]
        public List<ProductSubSelectionRuleCondition> LstProductSubSelectionRuleCondition
        {
            get { return _lstProductSubSelectionRuleCondition; }
            set { _lstProductSubSelectionRuleCondition = value; }
        }

        [DataMember(Name = "LstCartPriceRuleRoles", Order = 5)]
        public List<CartPriceRuleRole> LstCartPriceRuleRoles
        {
            get { return _lstCartPriceRuleRoles; }
            set { _lstCartPriceRuleRoles = value; }
        }

        [DataMember(Name = "LstCartPriceRuleStores", Order = 6)]
        public List<CartPriceRuleStore> LstCartPriceRuleStores
        {
            get { return _lstCartPriceRuleStores; }
            set { _lstCartPriceRuleStores = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class CartConditionDetail
    {
        private System.Nullable<int> _cartPriceRuleConditionDetailID;

        private int _ruleConditionID;

        private System.Nullable<int> _cartPriceRuleID;

        private System.Nullable<int> _attributeID;

        private System.Nullable<int> _ruleOperatorID;

        private string _value;

        private System.Nullable<int> _priority;

        public CartConditionDetail()
        {
        }

        [DataMember(Name = "CartPriceRuleConditionDetailID", Order = 0)]
        public System.Nullable<int> CartPriceRuleConditionDetailID
        {
            get
            {
                return this._cartPriceRuleConditionDetailID;
            }
            set
            {
                if ((this._cartPriceRuleConditionDetailID != value))
                {
                    this._cartPriceRuleConditionDetailID = value;
                }
            }
        }

        [DataMember(Name = "RuleConditionID", Order = 1)]
        public int RuleConditionID
        {
            get
            {
                return this._ruleConditionID;
            }
            set
            {
                if ((this._ruleConditionID != value))
                {
                    this._ruleConditionID = value;
                }
            }
        }

        [DataMember(Name = "CartPriceRuleID", Order = 2)]
        public System.Nullable<int> CartPriceRuleID
        {
            get
            {
                return this._cartPriceRuleID;
            }
            set
            {
                if ((this._cartPriceRuleID != value))
                {
                    this._cartPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "AttributeID", Order = 3)]
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

        [DataMember(Name = "RuleOperatorID", Order = 4)]
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

        [DataMember(Name = "Value", Order = 5)]
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

        [DataMember(Name = "Priority", Order = 6)]
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
    public class CartPriceRule
    {
        private int _cartPriceRuleID;

        private string _cartPriceRuleName;

        private string _cartPriceRuleDescription;

        private System.Nullable<int> _apply;

        private string _value;
        private System.Nullable<int> _discountQuantity;
        private System.Nullable<int> _discountStep;
        private System.Nullable<bool> _applytoShippingAmount;
        private System.Nullable<int> _freeShipping;
        private System.Nullable<bool> _isFurtherProcessing;

        private System.Nullable<System.DateTime> _fromDate;

        private System.Nullable<System.DateTime> _toDate;

        private System.Nullable<int> _priority;

        private System.Nullable<bool> _isActive;
        public CartPriceRule()
        {

        }
        [DataMember(Name = "CartPriceRuleID", Order = 0)]
        public int CartPriceRuleID
        {
            get
            {
                return this._cartPriceRuleID;
            }
            set
            {
                if ((this._cartPriceRuleID != value))
                {
                    this._cartPriceRuleID = value;
                }
            }
        }
        [DataMember(Name = "CartPriceRuleName", Order = 1)]
        public string CartPriceRuleName
        {
            get
            {
                return this._cartPriceRuleName;
            }
            set
            {
                if ((this._cartPriceRuleName != value))
                {
                    this._cartPriceRuleName = value;
                }
            }
        }
        [DataMember(Name = "CartPriceRuleDescription", Order = 2)]
        public string CartPriceRuleDescription
        {
            get
            {
                return this._cartPriceRuleDescription;
            }
            set
            {
                if ((this._cartPriceRuleDescription != value))
                {
                    this._cartPriceRuleDescription = value;
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
        [DataMember(Name = "DiscountQuantity", Order = 5)]
        public System.Nullable<int> DiscountQuantity
        {
            get
            {
                return this._discountQuantity;
            }
            set
            {
                if ((this._discountQuantity != value))
                {
                    this._discountQuantity = value;
                }
            }
        }
        [DataMember(Name = "DiscountStep", Order = 6)]
        public System.Nullable<int> DiscountStep
        {
            get
            {
                return this._discountStep;
            }
            set
            {
                if ((this._discountStep != value))
                {
                    this._discountStep = value;
                }
            }
        }
        [DataMember(Name = "ApplytoShippingAmount", Order = 7)]
        public System.Nullable<bool> ApplytoShippingAmount
        {
            get
            {
                return this._applytoShippingAmount;
            }
            set
            {
                if ((this._applytoShippingAmount != value))
                {
                    this._applytoShippingAmount = value;
                }
            }
        }
        [DataMember(Name = "FreeShipping", Order = 8)]
        public System.Nullable<int> FreeShipping
        {
            get
            {
                return this._freeShipping;
            }
            set
            {
                if ((this._freeShipping != value))
                {
                    this._freeShipping = value;
                }
            }
        }
        [DataMember(Name = "IsFurtherProcessing", Order = 9)]
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
        [DataMember(Name = "FromDate", Order = 10)]
        public System.Nullable<System.DateTime> FromDate
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
        [DataMember(Name = "ToDate", Order = 11)]
        public System.Nullable<System.DateTime> ToDate
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
        [DataMember(Name = "Priority", Order = 12)]
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
        [DataMember(Name = "IsActive", Order = 13)]
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
    public class RuleCondition
    {
        private System.Nullable<int> _ruleConditionID;

        private string _ruleConditionType;

        private int _cartPriceRuleID;

        private System.Nullable<int> _parentID;

        private List<CartConditionDetail> _lstCartConditionDetails;

        private List<CartPriceRuleCondition> _lstCartPriceRuleConditions;

        private List<ProductAttributeRuleCondition> _lstProductAttributeRuleConditions;

        private List<ProductSubSelectionRuleCondition> _lstProductSublectionRuleConditions;

        public RuleCondition()
        {

        }

        [DataMember(Name = "RuleConditionID", Order = 0)]
        public System.Nullable<int> RuleConditionID
        {
            get
            {
                return this._ruleConditionID;
            }
            set
            {
                if ((this._ruleConditionID != value))
                {
                    this._ruleConditionID = value;
                }
            }
        }

        [DataMember(Name = "RuleConditionType", Order = 1)]
        public string RuleConditionType
        {
            get
            {
                return this._ruleConditionType;
            }
            set
            {
                if ((this._ruleConditionType != value))
                {
                    this._ruleConditionType = value;
                }
            }
        }

        [DataMember(Name = "CartPriceRuleID", Order = 2)]
        public int CartPriceRuleID
        {
            get
            {
                return this._cartPriceRuleID;
            }
            set
            {
                if ((this._cartPriceRuleID != value))
                {
                    this._cartPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "ParentID", Order = 3)]
        public System.Nullable<int> ParentID
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

        [DataMember(Name = "LstCartPriceRuleConditions", Order = 4)]
        public List<CartPriceRuleCondition> LstCartPriceRuleConditions
        {
            get { return _lstCartPriceRuleConditions; }
            set { _lstCartPriceRuleConditions = value; }
        }

        [DataMember(Name = "LstProductAttributeRuleConditions", Order = 5)]
        public List<ProductAttributeRuleCondition> LstProductAttributeRuleConditions
        {
            get { return _lstProductAttributeRuleConditions; }
            set { _lstProductAttributeRuleConditions = value; }
        }

        [DataMember(Name = "LstProductSublectionRuleConditions", Order = 6)]
        public List<ProductSubSelectionRuleCondition> LstProductSublectionRuleConditions
        {
            get { return _lstProductSublectionRuleConditions; }
            set { _lstProductSublectionRuleConditions = value; }
        }

        [DataMember(Name = "LstCartConditionDetails", Order = 7)]
        public List<CartConditionDetail> LstCartConditionDetails
        {
            get { return _lstCartConditionDetails; }
            set { _lstCartConditionDetails = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class CartPriceRuleCondition
    {
        private int _cartPriceRuleConditionID;

        private System.Nullable<int> _ruleConditionID;

        private System.Nullable<bool> _isAll;

        private System.Nullable<bool> _isTrue;

        private List<CartConditionDetail> _lstCartConditionDetails;

        public CartPriceRuleCondition()
        {
        }

        [DataMember(Name = "CartPriceRuleConditionID", Order = 0)]
        public int CartPriceRuleConditionID
        {
            get
            {
                return this._cartPriceRuleConditionID;
            }
            set
            {
                if ((this._cartPriceRuleConditionID != value))
                {
                    this._cartPriceRuleConditionID = value;
                }
            }
        }

        [DataMember(Name = "RuleConditionID", Order = 1)]
        public System.Nullable<int> RuleConditionID
        {
            get
            {
                return this._ruleConditionID;
            }
            set
            {
                if ((this._ruleConditionID != value))
                {
                    this._ruleConditionID = value;
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

        [DataMember(Name = "LstCartConditionDetails", Order = 4)]
        public List<CartConditionDetail> LstCartConditionDetails
        {
            get { return _lstCartConditionDetails; }
            set { _lstCartConditionDetails = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class ProductAttributeRuleCondition
    {
        private int _productAttributeRuleConditionID;

        private System.Nullable<int> _ruleConditionID;

        private System.Nullable<bool> _isAll;

        private System.Nullable<bool> _isFound;

        private List<CartConditionDetail> _lstCartConditionDetails;

        public ProductAttributeRuleCondition()
        {
        }

        [DataMember(Name = "ProductAttributeRuleConditionID", Order = 0)]
        public int ProductAttributeRuleConditionID
        {
            get
            {
                return this._productAttributeRuleConditionID;
            }
            set
            {
                if ((this._productAttributeRuleConditionID != value))
                {
                    this._productAttributeRuleConditionID = value;
                }
            }
        }

        [DataMember(Name = "RuleConditionID", Order = 1)]
        public System.Nullable<int> RuleConditionID
        {
            get
            {
                return this._ruleConditionID;
            }
            set
            {
                if ((this._ruleConditionID != value))
                {
                    this._ruleConditionID = value;
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

        [DataMember(Name = "IsFound", Order = 3)]
        public System.Nullable<bool> IsFound
        {
            get
            {
                return this._isFound;
            }
            set
            {
                if ((this._isFound != value))
                {
                    this._isFound = value;
                }
            }
        }

        [DataMember(Name = "LstCartConditionDetails", Order = 4)]
        public List<CartConditionDetail> LstCartConditionDetails
        {
            get { return _lstCartConditionDetails; }
            set { _lstCartConditionDetails = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class ProductSubSelectionRuleCondition
    {
        private int _productSubSelectionRuleConditionID;

        private System.Nullable<int> _ruleConditionID;

        private System.Nullable<bool> _isAll;

        private string _value;

        private System.Nullable<bool> _isQuantity;

        private System.Nullable<int> _ruleOperatorID;

        private List<CartConditionDetail> _lstCartConditionDetails;

        public ProductSubSelectionRuleCondition()
        {
        }

        [DataMember(Name = "ProductSubSelectionRuleConditionID", Order = 0)]
        public int ProductSubSelectionRuleConditionID
        {
            get
            {
                return this._productSubSelectionRuleConditionID;
            }
            set
            {
                if ((this._productSubSelectionRuleConditionID != value))
                {
                    this._productSubSelectionRuleConditionID = value;
                }
            }
        }

        [DataMember(Name = "RuleConditionID", Order = 1)]
        public System.Nullable<int> RuleConditionID
        {
            get
            {
                return this._ruleConditionID;
            }
            set
            {
                if ((this._ruleConditionID != value))
                {
                    this._ruleConditionID = value;
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

        [DataMember(Name = "Value", Order = 3)]
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

        [DataMember(Name = "IsQuantity", Order = 4)]
        public System.Nullable<bool> IsQuantity
        {
            get
            {
                return this._isQuantity;
            }
            set
            {
                if ((this._isQuantity != value))
                {
                    this._isQuantity = value;
                }
            }
        }

        [DataMember(Name = "RuleOperatorID", Order = 5)]
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

        [DataMember(Name = "LstCartConditionDetails", Order = 6)]
        public List<CartConditionDetail> LstCartConditionDetails
        {
            get { return _lstCartConditionDetails; }
            set { _lstCartConditionDetails = value; }
        }

    }

    [DataContract]
    [Serializable]
    public class CartPriceRuleRole
    {
        private int _cartPriceRuleID;

        private string _roleID;

        public CartPriceRuleRole()
        {
        }
        [DataMember(Name = "CartPriceRuleID", Order = 0)]
        public int CartPriceRuleID
        {
            get
            {
                return this._cartPriceRuleID;
            }
            set
            {
                if ((this._cartPriceRuleID != value))
                {
                    this._cartPriceRuleID = value;
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

    [DataContract]
    [Serializable]
    public class CartPriceRuleStore
    {
        private int _cartPriceRuleID;

        private string _storeID;

        public CartPriceRuleStore()
        {
        }
        [DataMember(Name = "CartPriceRuleID", Order = 0)]
        public int CartPriceRuleID
        {
            get
            {
                return this._cartPriceRuleID;
            }
            set
            {
                if ((this._cartPriceRuleID != value))
                {
                    this._cartPriceRuleID = value;
                }
            }
        }

        [DataMember(Name = "StoreID", Order = 1)]
        public string StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }
    }
}
