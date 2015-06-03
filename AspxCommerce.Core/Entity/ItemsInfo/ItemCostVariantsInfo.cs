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
    public class ItemCostVariantsInfo
    {
        #region Private Fields
        private int _costVariantID;
        private string _costVariantName;
        private int _inputTypeID;
        private System.Nullable<int> _costVariantValueID;
        private string _costVariantValueName;
        private bool _isOutOfStock;
        private System.Nullable<decimal> _costVariantPriceValue;
        private System.Nullable<decimal> _costVariantWeightValue;
        private System.Nullable<bool> _isPriceInPercentage;
               
        private System.Nullable<bool> _isWeightInPercentage;
        #endregion

        #region Constructor
        public ItemCostVariantsInfo()
        {
        }
        #endregion

        #region Public Fields
        [DataMember]
        public int CostVariantID
        {
            get
            {
                return this._costVariantID;
            }
            set
            {
                if ((this._costVariantID != value))
                {
                    this._costVariantID = value;
                }
            }
        }

        [DataMember]
        public string CostVariantName
        {
            get
            {
                return this._costVariantName;
            }
            set
            {
                if ((this._costVariantName != value))
                {
                    this._costVariantName = value;
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
        public System.Nullable<int> CostVariantsValueID
        {
            get
            {
                return this._costVariantValueID;
            }
            set
            {
                if ((this._costVariantValueID != value))
                {
                    this._costVariantValueID = value;
                }
            }
        }

        [DataMember]
        public string CostVariantsValueName
        {
            get
            {
                return this._costVariantValueName;
            }
            set
            {
                if ((this._costVariantValueName != value))
                {
                    this._costVariantValueName = value;
                }
            }
        }

        [DataMember]
        public bool IsOutOfStock
        {
            get { return this._isOutOfStock; }
            set
            {
                if ((this._isOutOfStock != value))
                {
                    this._isOutOfStock = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<decimal> CostVariantsPriceValue
        {
            get
            {
                return this._costVariantPriceValue;
            }
            set
            {
                if ((this._costVariantPriceValue != value))
                {
                    this._costVariantPriceValue = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<decimal> CostVariantsWeightValue
        {
            get
            {
                return this._costVariantWeightValue;
            }
            set
            {
                if ((this._costVariantWeightValue != value))
                {
                    this._costVariantWeightValue = value;
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
        #endregion
    }
}