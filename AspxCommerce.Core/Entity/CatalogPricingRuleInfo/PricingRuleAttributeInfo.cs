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
    public class PricingRuleAttributeInfo
    {
        #region Private Properties
        private int _attributeID;
        private string _attributeName;
        private string _attributeNameAlias;
        private int _inputTypeID;
        private string _inputType;
        private string _values;
        private string _operators; 
        #endregion

        #region Public Properties
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
        public string AttributeNameAlias
        {
            get
            {
                return this._attributeNameAlias;
            }
            set
            {
                if ((this._attributeNameAlias != value))
                {
                    this._attributeNameAlias = value;
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
        public string InputType
        {
            get
            {
                return this._inputType;
            }
            set
            {
                if ((this._inputType != value))
                {
                    this._inputType = value;
                }
            }
        }

        [DataMember]
        public string Values
        {
            get
            {
                return this._values;
            }
            set
            {
                if ((this._values != value))
                {
                    this._values = value;
                }
            }
        }

        [DataMember]
        public string Operators
        {
            get
            {
                return this._operators;
            }
            set
            {
                if ((this._operators != value))
                {
                    this._operators = value;
                }
            }
        } 
        #endregion
    }
}
