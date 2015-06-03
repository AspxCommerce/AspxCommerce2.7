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
    public class AttributeSetInfo
    {
        #region Constructor
        public AttributeSetInfo()
        {
        } 
        #endregion  

        #region Private Fields
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_attributeSetID", Order = 1)]
        private int _attributeSetID;

        //[DataMember(Name = "_attributeSetBaseID", Order = 1)]
        private int _attributeSetBaseID;

        [DataMember(Name = "_attributeSetName", Order = 2)]
        private string _attributeSetName;

        [DataMember(Name = "_isSystemUsed", Order = 3)]
        private System.Nullable<bool> _isSystemUsed;

        //[DataMember(Name = "_storeID", Order = 0)]
        private int _storeID;

        //[DataMember(Name = "_portalID", Order = 0)]
        private int _portalID;

        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;

        //[DataMember(Name = "_isDeleted", Order = 0)]
        private System.Nullable<bool> _isDeleted;

        //[DataMember(Name = "_isModified", Order = 0)]
        private System.Nullable<bool> _isModified;

        //[DataMember(Name = "_addedOn", Order = 0)]
        private System.Nullable<System.DateTime> _addedOn;

        //[DataMember(Name = "_updatedOn", Order = 0)]
        private System.Nullable<System.DateTime> _updatedOn;

        //[DataMember(Name = "_deletedOn", Order = 0)]
        private System.Nullable<System.DateTime> _deletedOn;

        //[DataMember(Name = "_addedBy", Order = 0)]
        private string _addedBy;

        //[DataMember(Name = "_updatedBy", Order = 0)]
        private string _updatedBy;

        //[DataMember(Name = "_deletedBy", Order = 0)]
        private string _deletedBy;

        //[DataMember(Name = "_flag", Order = 2)]
        private System.Nullable<bool> _flag;

        //[DataMember(Name = "_saveString", Order = 0)]
        private string _saveString;

        //[DataMember(Name = "_aliasName", Order = 0)]
        private string _aliasName;
        #endregion

        #region Public Fields
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

        public int AttributeSetBaseID
        {
            get
            {
                return this._attributeSetBaseID;
            }
            set
            {
                if ((this._attributeSetBaseID != value))
                {
                    this._attributeSetBaseID = value;
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

        public System.Nullable<bool> Flag
        {
            get
            {
                return this._flag;
            }
            set
            {
                if ((this._flag != value))
                {
                    this._flag = value;
                }
            }
        }

        public string SaveString
        {
            get
            {
                return this._saveString;
            }
            set
            {
                if ((this._saveString != value))
                {
                    this._saveString = value;
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
        #endregion

    }
}
