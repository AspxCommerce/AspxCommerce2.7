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

   public class StoreTaxesInfo
    {
        #region Constructor
       public StoreTaxesInfo()
        {
        }
       #endregion
       #region Private Fields
       [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

       [DataMember(Name = "_taxManageRuleName", Order = 1)]
       private string _taxManageRuleName;

       [DataMember(Name = "_taxRate", Order = 2)]
       private string _taxRate;

       [DataMember(Name = "_quantity", Order = 3)]
       private int _quantity;

       [DataMember(Name = "_isPercent", Order = 4)]
       private bool _isPercent;

       [DataMember(Name = "_noOfOrders", Order = 6)]
       private int _noOfOrders;

       [DataMember(Name = "_totalTaxAmount", Order = 6)]
       private string _totalTaxAmount;

       [DataMember(Name = "_addedOn", Order = 7)]
       private string _addedOn;
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



        public string TaxManageRuleName
        {
            get
            {
                return this._taxManageRuleName;
            }
            set
            {
                if ((this._taxManageRuleName != value))
                {
                    this._taxManageRuleName = value;
                }
            }
        }

        public string TaxRate
        {
            get
            {
                return this._taxRate;
            }
            set
            {
                if ((this._taxRate != value))
                {
                    this._taxRate = value;
                }
            }
        }

        public int Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                if ((this._quantity != value))
                {
                    this._quantity = value;
                }
            }
        }

        public bool IsPercent
        {
            get
            {
                return this._isPercent;
            }
            set
            {
                if ((this._isPercent != value))
                {
                    this._isPercent = value;
                }
            }
        }

        public int NoOfOrders
        {
            get
            {
                return this._noOfOrders;
            }
            set
            {
                if ((this._noOfOrders != value))
                {
                    this._noOfOrders = value;
                }
            }
        }



        public string TotalTaxAmount
        {
            get
            {
                return this._totalTaxAmount;
            }
            set
            {
                if ((this._totalTaxAmount != value))
                {
                    this._totalTaxAmount = value;
                }
            }
        }

        public string AddedOn
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

        #endregion
    }
}
