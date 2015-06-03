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
    public class CategoryAttributeInfo
    {
        private int _categoryID;
        private int _parentID;
        private int _categoryLevel;
        private int _attributeID;
        private string _attributeName;
        private int _inputTypeID;
        private int _validationTypeID;
        private bool _isUnique;
        private bool _isRequired;
        private string _nvarcharValue;
        private string _textValue;
        private bool _booleanValue;
        private int _intValue;
        private System.Nullable<DateTime> _dateValue;
        private System.Nullable<decimal> _decimalValue;
        private string _fileValue;
        private string _optionValues;
        [DataMember]
        public int CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                if ((this._categoryID != value))
                {
                    this._categoryID = value;
                }
            }
        }

        [DataMember]
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
        [DataMember]
        public int CategoryLevel
        {
            get
            {
                return this._categoryLevel;
            }
            set
            {
                if ((this._categoryLevel != value))
                {
                    this._categoryLevel = value;
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
        public int ValidationTypeID
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
        public bool IsUnique
        {
            get
            {
                return this._isUnique;
            }
            set
            {
                this._isUnique = value;
            }
        }

        [DataMember]
        public bool IsRequired
        {
            get
            {
                return this._isRequired;
            }
            set
            {
                this._isRequired = value;
            }
        }

        [DataMember]
        public string NvarcharValue
        {
            get
            {
                return this._nvarcharValue;
            }
            set
            {
                if ((this.NvarcharValue != value))
                {
                    this._nvarcharValue = value;  
                }
            }
        }

        [DataMember]
        public string TextValue
        {
            get
            {
                return this._textValue;
            }
            set
            {
                if ((this._textValue != value))
                {
                    this._textValue = value;
                }
            }
        }

        [DataMember]
        public bool BooleanValue
        {
            get
            {
                return this._booleanValue;
            }
            set
            {
                if ((this._booleanValue != value))
                {
                    this._booleanValue = value;
                }
            }
        }

        [DataMember]
        public int IntValue
        {
            get
            {
                return this._intValue;
            }
            set
            {
                if ((this._intValue != value))
                {
                    this._intValue = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<DateTime> DateValue
        {
            get
            {
                return this._dateValue;
            }
            set
            {
                if ((this._dateValue != value))
                {
                    this._dateValue = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<decimal> DecimalValue
        {
            get
            {
                return this._decimalValue;
            }
            set
            {
                if ((this._decimalValue != value))
                {
                    this._decimalValue = value;
                }
            }
        }

        [DataMember]
        public string FileValue
        {
            get
            {
                return this._fileValue;
            }
            set
            {
                if ((this._fileValue != value))
                {
                    this._fileValue = value;
                }
            }
        }

        [DataMember]
        public string OptionValues
        {
            get
            {
                return this._optionValues;
            }
            set
            {
                if ((this._optionValues != value))
                {
                    this._optionValues = value;
                }
            }
        }
    }
}