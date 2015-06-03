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




using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    public class AttributeSetGetByAttributeSetIdInfo
    {
        #region Constructor
        public AttributeSetGetByAttributeSetIdInfo()
        {
        } 
        #endregion

        #region Private Fields
        [DataMember(Name = "_attributeSetID", Order = 0)]
        private int _attributeSetID;

        [DataMember(Name = "_attributeSetName", Order = 1)]
        private string _attributeSetName;        

        [DataMember(Name = "_storeID", Order = 2)]
        private int _storeID;

        [DataMember(Name = "_portalID", Order = 3)]
        private int _portalID;

        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;

        [DataMember(Name = "_isDeleted", Order = 5)]
        private System.Nullable<bool> _isDeleted;

        [DataMember(Name = "_isModified", Order = 6)]
        private System.Nullable<bool> _isModified;

        [DataMember(Name = "_addedOn", Order = 7)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_updatedOn", Order = 8)]
        private System.Nullable<System.DateTime> _updatedOn;

        [DataMember(Name = "_deletedOn", Order = 9)]
        private System.Nullable<System.DateTime> _deletedOn;

        [DataMember(Name = "_addedBy", Order = 10)]
        private string _addedBy;

        [DataMember(Name = "_updatedBy", Order = 11)]
        private string _updatedBy;

        [DataMember(Name = "_deletedBy", Order = 12)]
        private string _deletedBy;

        [DataMember(Name = "_attributeSetGroupID", Order = 13)]
        private int _attributeSetGroupID;

        [DataMember(Name = "_attributeID", Order = 14)]
        private int _attributeID;

        [DataMember(Name = "_attributeName", Order = 15)]
        private string _attributeName;

        [DataMember(Name = "_isSystemUsed", Order = 16)]
        private System.Nullable<bool> _isSystemUsed;

        [DataMember(Name = "_groupID", Order = 17)]
        private int _groupID;

        [DataMember(Name = "_groupName", Order = 18)]
        private string _groupName;

        [DataMember(Name = "_isSystemUsedGroup", Order = 19)]
        private System.Nullable<bool> _isSystemUsedGroup;

        [DataMember(Name = "_cultureName", Order = 20)]
        private string _cultureName;

        [DataMember(Name = "_unassignedAttributes", Order = 21)]
        private string _unassignedAttributes;

        [DataMember(Name = "_unassignedAttributesName", Order = 22)]
        private string _unassignedAttributesName;         
        #endregion

        #region Public Fields
        public int AttributeSetID
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

        public string AttributeSetName
        {
            get
            {
                return this._attributeSetName;
            }
            set
            {
                if ((this._attributeSetName != value))
                {
                    this._attributeSetName = value;
                }
            }
        }        

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

        public System.Nullable<bool> IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
            set
            {
                if ((this._isDeleted != value))
                {
                    this._isDeleted = value;
                }
            }
        }

        public System.Nullable<bool> IsModified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                if ((this._isModified != value))
                {
                    this._isModified = value;
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

        public System.Nullable<System.DateTime> UpdatedOn
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

        public System.Nullable<System.DateTime> DeletedOn
        {
            get
            {
                return this._deletedOn;
            }
            set
            {
                if ((this._deletedOn != value))
                {
                    this._deletedOn = value;
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

        public string UpdatedBy
        {
            get
            {
                return this._updatedBy;
            }
            set
            {
                if ((this._updatedBy != value))
                {
                    this._updatedBy = value;
                }
            }
        }

        public string DeletedBy
        {
            get
            {
                return this._deletedBy;
            }
            set
            {
                if ((this._deletedBy != value))
                {
                    this._deletedBy = value;
                }
            }
        }

        public int AttributeSetGroupID
        {
            get
            {
                return this._attributeSetGroupID;
            }
            set
            {
                if ((this._attributeSetGroupID != value))
                {
                    this._attributeSetGroupID = value;
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

        public int GroupID
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

        public System.Nullable<bool> IsSystemUsedGroup
        {
            get
            {
                return this._isSystemUsedGroup;
            }
            set
            {
                if ((this._isSystemUsedGroup != value))
                {
                    this._isSystemUsedGroup = value;
                }
            }
        }

        public string CultureName
        {
            get
            {
                return this._cultureName;
            }
            set
            {
                if ((this._cultureName != value))
                {
                    this._cultureName = value;
                }
            }
        }

        public string UnassignedAttributes
        {
            get
            {
                return this._unassignedAttributes;
            }
            set
            {
                if ((this._unassignedAttributes != value))
                {
                    this._unassignedAttributes = value;
                }
            }
        }

        public string UnassignedAttributesName
        {
            get
            {
                return this._unassignedAttributesName;
            }
            set
            {
                if ((this._unassignedAttributesName != value))
                {
                    this._unassignedAttributesName = value;
                }
            }
        } 
        #endregion
    }
}
