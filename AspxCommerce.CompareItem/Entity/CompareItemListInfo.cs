using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.CompareItem
{
    [Serializable]
    public class CompareItemListInfo
    {
        #region PrivateMember

        private int _itemID;

        private int _inputTypeID;

        private int _attributeID;

        private string _attributeName;

        private string _attributeValue;
        #endregion

        #region Public Member
        [DataMember]
        public int ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if ((this._itemID != value))
                {
                    this._itemID = value;
                }
            }
        }
        [DataMember]
        public int InputTypeID
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
        public string AttributeValue
        {
            get
            {
                return this._attributeValue;
            }
            set
            {
                if ((this._attributeValue != value))
                {
                    this._attributeValue = value;
                }
            }
        }
        #endregion
    }
}
