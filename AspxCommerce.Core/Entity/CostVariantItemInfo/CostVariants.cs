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
    public class CostVariants
    {
        #region Private members

        private int _costVariantID;
        private string _costVariantName;
        private int _costVariantValueID;
        private string _costVariantValueName;
        private string _costVariantPriceValue;
        private string _costVariantWeightValue;
        private bool _isPriceInPercentage;
        private bool _isWeightInPercentage;

        #endregion


        #region Public Members

        [DataMember]
        public int CostVariantID
        {
            get { return this._costVariantID; }
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
            get { return this._costVariantName; }
            set
            {
                if ((this._costVariantName != value))
                {
                    this._costVariantName = value;
                }
            }
        }

        [DataMember]
        public int CostVariantValueID
        {
            get { return this._costVariantValueID; }
            set
            {
                if ((this._costVariantValueID != value))
                {
                    this._costVariantValueID = value;
                }
            }
        }

        [DataMember]
        public string CostVariantValueName
        {
            get { return this._costVariantValueName; }
            set
            {
                if ((this._costVariantValueName != value))
                {
                    this._costVariantValueName = value;
                }
            }
        }
        [DataMember]
        public string CostVariantPriceValue
        {
            get { return this._costVariantPriceValue; }

            set
            {
                if ((this._costVariantPriceValue != value))
                {
                    this._costVariantPriceValue = value;
                }
            }
        }
        [DataMember]
        public string CostVariantWeightValue
        {
            get { return this._costVariantWeightValue; }
            set
            {
                if ((this._costVariantWeightValue != value))
                {
                    this._costVariantWeightValue = value;
                }
            }
        }
        [DataMember]
        public bool IsPriceInPercentage
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
        public bool IsWeightInPercentage
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
