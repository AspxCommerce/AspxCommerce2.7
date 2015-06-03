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
    public class AttributesBasicInfo
    {
        #region Constructor
        public AttributesBasicInfo()
        {
        }
        #endregion

        #region Private Members
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_attributeID", Order = 1)]
        private int _attributeID;

        [DataMember(Name = "_attributeName", Order = 2)]
        private string _attributeName;

        [DataMember(Name = "_aliasName", Order = 3)]
        private string _aliasName;

        [DataMember(Name = "_isRequired", Order = 4)]
        private System.Nullable<bool> _isRequired;

        [DataMember(Name = "_isActive", Order = 5)]
        private bool _isActive;

        [DataMember(Name = "_isSystemUsed", Order = 6)]
        private System.Nullable<bool> _isSystemUsed;

        [DataMember(Name = "_showInSearch", Order = 7)]
        private System.Nullable<bool> _showInSearch;

        [DataMember(Name = "_isUsedInConfigItem", Order = 8)]
        private System.Nullable<bool> _isUsedInConfigItem;

        [DataMember(Name = "_showInComparison", Order = 9)]
        private System.Nullable<bool> _showInComparison;

        [DataMember(Name = "_addedOn", Order = 10)]
        private System.Nullable<System.DateTime> _addedOn;
        #endregion

        #region Public Members
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

        public int AttributeID
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

        public string AttributeName
        {
            get
            {
                return this._attributeName;
            }
            set
            {
                if ((this._attributeName != value))
                {
                    this._attributeName = value;
                }
            }
        }

        public string AliasName
        {
            get
            {
                return this._aliasName;
            }
            set
            {
                if ((this._aliasName != value))
                {
                    this._aliasName = value;
                }
            }
        }

        public System.Nullable<bool> IsRequired
        {
            get
            {
                return this._isRequired;
            }
            set
            {
                if ((this._isRequired != value))
                {
                    this._isRequired = value;
                }
            }
        }

        public bool IsActive
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

        public System.Nullable<bool> IsSystemUsed
        {
            get
            {
                return this._isSystemUsed;
            }
            set
            {
                if ((this._isSystemUsed != value))
                {
                    this._isSystemUsed = value;
                }
            }
        }

        public System.Nullable<bool> ShowInSearch
        {
            get
            {
                return this._showInSearch;
            }
            set
            {
                if ((this._showInSearch != value))
                {
                    this._showInSearch = value;
                }
            }
        }

        public System.Nullable<bool> IsUsedInConfigItem
        {
            get
            {
                return this._isUsedInConfigItem;
            }
            set
            {
                if ((this._isUsedInConfigItem != value))
                {
                    this._isUsedInConfigItem = value;
                }
            }
        }

        public System.Nullable<bool> ShowInComparison
        {
            get
            {
                return this._showInComparison;
            }
            set
            {
                if ((this._showInComparison != value))
                {
                    this._showInComparison = value;
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

        #endregion

    }
    public class AttributeBindInfo
    {
        public int AttributeID { get; set; }
        public string AttributeName { get; set; }
        public string AliasName { get; set; }
        public System.Nullable<bool> IsRequired { get; set; }
        public bool IsActive { get; set; }
        public System.Nullable<bool> IsSystemUsed { get; set; }
        public System.Nullable<bool> ShowInSearch { get; set; }
        public System.Nullable<bool> IsUsedInConfigItem { get; set; }
        public System.Nullable<bool> ShowInComparison { get; set; }
        public System.Nullable<bool> IsShowInItemListing { get; set; }
        public System.Nullable<bool> IsShowInItemDetail { get; set; }
        public System.Nullable<System.DateTime> AddedOn { get; set; }
    }


}
