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
    public class CostVariantsvalueInfo
    {
        private int _costVariantsValueID;

        private string _costVariantsValueName;

        private string _costVariantsPriceValue;

        private string _costVariantsWeightValue;

        private System.Nullable<bool> _isPriceInPercentage;

        private System.Nullable<bool> _isWeightInPercentage;

        private System.Nullable<int> _displayOrder;

        private System.Nullable<bool> _isActive;

        private int _Quantity;

        private string _ImagePath;

        private System.Nullable<System.DateTime> _addedOn;

        private string _addedBy;

        public CostVariantsvalueInfo()
        {
        }

        [DataMember]
        public int CostVariantsValueID
        {
            get
            {
                return this._costVariantsValueID;
            }
            set
            {
                if ((this._costVariantsValueID != value))
                {
                    this._costVariantsValueID = value;
                }
            }
        }

        [DataMember]
        public string CostVariantsValueName
        {
            get
            {
                return this._costVariantsValueName;
            }
            set
            {
                if ((this._costVariantsValueName != value))
                {
                    this._costVariantsValueName = value;
                }
            }
        }

        [DataMember]
        public string CostVariantsPriceValue
        {
            get
            {
                return this._costVariantsPriceValue;
            }
            set
            {
                if ((this._costVariantsPriceValue != value))
                {
                    this._costVariantsPriceValue = value;
                }
            }
        }

        [DataMember]
        public string CostVariantsWeightValue
        {
            get
            {
                return this._costVariantsWeightValue;
            }
            set
            {
                if ((this._costVariantsWeightValue != value))
                {
                    this._costVariantsWeightValue = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsPriceInPercentage
        {
            get
            {
                return this._isPriceInPercentage;
            }
            set
            {
                if ((this._isPriceInPercentage != value))
                {
                    this._isPriceInPercentage = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsWeightInPercentage
        {
            get
            {
                return this._isWeightInPercentage;
            }
            set
            {
                if ((this._isWeightInPercentage != value))
                {
                    this._isWeightInPercentage = value;
                }
            }
        }

        [DataMember]
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

        [DataMember]
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
        [DataMember]
        public int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                }
            }
        }
        [DataMember]
        public string ImagePath
        {
            get
            {
                return this._ImagePath;
            }
            set
            {
                if ((this._ImagePath != value))
                {
                    this._ImagePath = value;
                }
            }
        }
        [DataMember]
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

        [DataMember]
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
    }
}
