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
    public class AttributeFormInfo
    {
        #region Constructor
        public AttributeFormInfo()
        {
        }
        #endregion

        #region Private Fields

        private int _attributeID;

        private string _attributeName;

        private string _toolTip;

        private string _help;

        private Int32 _inputTypeID;

        private string _inputTypeValues;

        private string _defaultValue;

        private System.Nullable<int> _validationTypeID;

        private System.Nullable<int> _length;

        private System.Nullable<bool> _isUnique;

        private System.Nullable<bool> _isRequired;

        private System.Nullable<bool> _isEnableEditor;

        private System.Nullable<int> _groupID;

        private string _groupName;
        private string _groupTabName;

        private int _storeID;

        private int _portalID;

        private System.Nullable<bool> _isIncludeInPriceRule;

        private System.Nullable<bool> _isIncludeInPromotions;

        private int _displayOrder;

        private string _attributeValues;

        private System.Nullable<int> _attributeSetID;

        private System.Nullable<int> _itemTypeID;

        private System.Nullable<int> _taxItemClass;
        private System.Nullable<int> _currencyID;
        private string _currencyCode;
        #endregion

        #region Public Fields
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public string ToolTip
        {
            get
            {
                return this._toolTip;
            }
            set
            {
                if ((this._toolTip != value))
                {
                    this._toolTip = value;
                }
            }
        }
        [DataMember]
        public string Help
        {
            get
            {
                return this._help;
            }
            set
            {
                if ((this._help != value))
                {
                    this._help = value;
                }
            }
        }
        [DataMember]
        public Int32 InputTypeID
        {
            get
            {
                return this._inputTypeID;
            }
            set
            {
                if ((this._inputTypeID != value))
                {
                    this._inputTypeID = value;
                }
            }
        }
        [DataMember]
        public string InputTypeValues
        {
            get
            {
                return this._inputTypeValues;
            }
            set
            {
                if ((this._inputTypeValues != value))
                {
                    this._inputTypeValues = value;
                }
            }
        }
        [DataMember]
        public string DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                if ((this._defaultValue != value))
                {
                    this._defaultValue = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> ValidationTypeID
        {
            get
            {
                return this._validationTypeID;
            }
            set
            {
                if ((this._validationTypeID != value))
                {
                    this._validationTypeID = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> Length
        {
            get
            {
                return this._length;
            }
            set
            {
                if ((this._length != value))
                {
                    this._length = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<bool> IsUnique
        {
            get
            {
                return this._isUnique;
            }
            set
            {
                if ((this._isUnique != value))
                {
                    this._isUnique = value;
                }
            }
        }
        [DataMember]
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
        [DataMember]
        public System.Nullable<bool> IsEnableEditor
        {
            get
            {
                return this._isEnableEditor;
            }
            set
            {
                if ((this._isEnableEditor != value))
                {
                    this._isEnableEditor = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> GroupID
        {
            get
            {
                return this._groupID;
            }
            set
            {
                if ((this._groupID != value))
                {
                    this._groupID = value;
                }
            }
        }
        [DataMember]
        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                if ((this._groupName != value))
                {
                    this._groupName = value;
                }
            }
        }
        [DataMember]
        public string GroupTabName
        {
            get
            {
                return this._groupTabName;
            }
            set
            {
                if ((this._groupTabName != value))
                {
                    this._groupTabName = value;
                }
            }
        }
        [DataMember]
        public int StoreID
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
        [DataMember]
        public int PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsIncludeInPriceRule
        {
            get
            {
                return this._isIncludeInPriceRule;
            }
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
            get
            {
                return this._isIncludeInPromotions;
            }
            set
            {
                if ((this._isIncludeInPromotions != value))
                {
                    this._isIncludeInPromotions = value;
                }
            }
        }

        [DataMember]
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

        [DataMember]
        public string AttributeValues
        {
            get
            {
                return this._attributeValues;
            }
            set
            {
                if ((this._attributeValues != value))
                {
                    this._attributeValues = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> AttributeSetID
        {
            get
            {
                return this._attributeSetID;
            }
            set
            {
                if ((this._attributeSetID != value))
                {
                    this._attributeSetID = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> ItemTypeID
        {
            get
            {
                return this._itemTypeID;
            }
            set
            {
                if ((this._itemTypeID != value))
                {
                    this._itemTypeID = value;
                }
            }
        }

           [DataMember]
        public System.Nullable<int> TaxItemClass
        {
            get
            {
                return this._taxItemClass;
            }
            set
            {
                if ((this._taxItemClass != value))
                {
                    this._taxItemClass = value;
                }
            }
        }
           [DataMember]
           public System.Nullable<int> CurrencyID
           {
               get
               {
                   return this._currencyID;
               }
               set
               {
                   if ((this._currencyID != value))
                   {
                       this._currencyID = value;
                   }
               }
           }

           [DataMember]
           public string CurrencyCode
           {
               get
               {
                   return this._currencyCode;
               }
               set
               {
                   if ((this._currencyCode != value))
                   {
                       this._currencyCode = value;
                   }
               }
           }
        #endregion
    }
}