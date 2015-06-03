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
    class CategoryAttribute
    {
        private Int32 _categoryID;
        [DataMember]
        public Int32 CategoryID
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

        private Int32 _attributeID;
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

        private string _attributeName;
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

        private Int32 _inputTypeID;
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

        private Int32 _validationTypeID;
        [DataMember]
        public Int32 ValidationTypeID
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

        private System.Nullable<bool> _isUnique;
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

        private System.Nullable<bool> _isRequired;
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

        private string _nvarcharValue;
        [DataMember]
        public string NvarcharValue
        {
            get
            {
                return this._nvarcharValue;
            }
            set
            {
                if ((this._nvarcharValue != value))
                {
                    this._nvarcharValue = value;
                }
            }
        }

        private string _textValue;
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

        private System.Nullable<bool> _booleanValue;
        [DataMember]
        public System.Nullable<bool> BooleanValue
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

        private Int32 _intValue;
        [DataMember]
        public Int32 IntValue
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

        private DateTime _dateValue;
        [DataMember]
        public DateTime DateValue
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

        private decimal _decimalValue;
        [DataMember]
        public decimal DecimalValue
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

        private Int32 _fileValue;
        [DataMember]
        public Int32 FileValue
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
    }
}
