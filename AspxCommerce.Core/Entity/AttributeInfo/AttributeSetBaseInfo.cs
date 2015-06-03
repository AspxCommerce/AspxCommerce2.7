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
    public class AttributeSetBaseInfo
    {
        #region Constructor
        public AttributeSetBaseInfo()
        {
        }
        #endregion

        #region Private Fields
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_attributeSetID", Order = 1)]
        private int _attributeSetID;

        [DataMember(Name = "_attributeSetName", Order = 2)]
        private string _attributeSetName;

        [DataMember(Name = "_isSystemUsed", Order = 3)]
        private System.Nullable<bool> _isSystemUsed;
        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;
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
    public class AttributeSetBindInfo
    {
        public int AttributeSetID { get; set; }
        public string AttributeSetName { get; set; }
        public System.Nullable<bool> IsSystemUsed { get; set; }
        public System.Nullable<bool> IsActive { get; set; }

    }
    public class AttributeSaveInfo : AttributeSetBindInfo
    {
        public int AttributeID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string AliasName { get; set; }
        public bool IsModified { get; set; }
        public bool Flag { get; set; }
    }
        #endregion
}
