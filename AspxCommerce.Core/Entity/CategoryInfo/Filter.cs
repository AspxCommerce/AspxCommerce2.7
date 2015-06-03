using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [Serializable]
    [DataContract]
    public class Filter
    {

        [DataMember(Name = "_itemCount", Order = 0)]
        private int _itemCount;

        [DataMember(Name = "_attributeID", Order = 1)]
        private string _attributeID;

        [DataMember(Name = "_inputTypeID", Order = 2)]
        private int _inputTypeID;

        [DataMember(Name = "_validationTypeID", Order = 3)]
        private int _validationTypeID;


        [DataMember(Name = "_attributeName", Order = 4)]
        private string _attributeName;

        [DataMember(Name = "_attributeValue", Order = 5)]
        private string _attributeValue;

        public int ItemCount
        {
            get
            {
                return this._itemCount;

            }
            set
            {
                if (this._itemCount != value)
                {
                    _itemCount = value;
                }

            }


        }

        public string AttributeID
        {
            get
            {
                return _attributeID;
            }
            set
            {
                if (this._attributeID != value)
                {
                    _attributeID = value;
                }
            }

        }
        public int InputTypeID
        {
            get
            {
                return this._inputTypeID;

            }
            set
            {
                if (this._inputTypeID != value)
                {
                    _inputTypeID = value;
                }

            }


        }
        public int ValidationTypeID
        {
            get
            {
                return this._validationTypeID;

            }
            set
            {
                if (this._validationTypeID != value)
                {
                    _validationTypeID = value;
                }

            }


        }


        public string AttributeName
        {
            get
            {
                return _attributeName;
            }
            set
            {
                if (this._attributeName != value)
                {
                    _attributeName = value;
                }
            }
        }

        public string AttributeValue
        {
            get
            {
                return _attributeValue;
            }
            set
            {
                if (this._attributeValue != value)
                {
                    _attributeValue = value;
                }
            }
        }
    }
}
