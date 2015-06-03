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
    public class AttributesInputTypeInfo
    {
        #region Constructor
        public AttributesInputTypeInfo()
        {
        } 
        #endregion        

        #region Private Fields
        [DataMember(Name = "_inputTypeID", Order = 0)]
        private int _inputTypeID;

        [DataMember(Name = "_inputType", Order = 1)]
        private string _inputType;

        //[DataMember(Name = "_displayOrder", Order = 2)]
        private System.Nullable<int> _displayOrder;

        //[DataMember(Name = "_isDefaultSelected", Order = 3)]
        private System.Nullable<bool> _isDefaultSelected;
        #endregion

        #region Public Fields
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

        public System.Nullable<int> DisplayOrder
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

        public System.Nullable<bool> IsDefaultSelected
        {
            get
            {
                return this._isDefaultSelected;
            }
            set
            {
                if ((this._isDefaultSelected != value))
                {
                    this._isDefaultSelected = value;
                }
            }
        } 
        #endregion
    }
}
