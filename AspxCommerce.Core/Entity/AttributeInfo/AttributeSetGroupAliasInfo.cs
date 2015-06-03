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
    public class AttributeSetGroupAliasInfo
    {
        #region Constructor
        public AttributeSetGroupAliasInfo()
        {
        }
        #endregion

        #region Private Fields
        [DataMember(Name = "_groupID", Order = 0)]
        private int _groupID;

        //[DataMember(Name = "_cultureName", Order = 0)]
        private string _cultureName;

        [DataMember(Name = "_aliasName", Order = 1)]
        private string _aliasName;

        [DataMember(Name = "_attributeSetID", Order = 2)]
        private int _attributeSetID;

        //[DataMember(Name = "_storeID", Order = 0)]
        private int _storeID;

        //[DataMember(Name = "_portalID", Order = 0)]
        private int _portalID;

        //[DataMember(Name = "_isActive", Order = 0)]
        private System.Nullable<bool> _isActive;

        //[DataMember(Name = "_isModified", Order = 0)]
        private System.Nullable<bool> _isModified;

        //[DataMember(Name = "_updatedBy", Order = 0)]
        private string _updatedBy;

        #endregion

        #region public Fields
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
        #endregion
    }

}