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
    public class CatalogPriceRulePaging
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;

        [DataMember(Name = "_catalogPriceRuleID", Order = 1)]
        private int _catalogPriceRuleID;

        [DataMember(Name = "_catalogPriceRuleName", Order = 2)]
        private string _catalogPriceRuleName;

        [DataMember(Name = "_fromDate", Order = 3)]
        private System.Nullable<System.DateTime> _fromDate;

        [DataMember(Name = "_toDate", Order = 4)]
        private System.Nullable<System.DateTime> _toDate;

        [DataMember(Name = "_isActive", Order = 5)]
        private System.Nullable<bool> _isActive;

        [DataMember(Name = "_priority", Order = 6)]
        private int _priority;

        //private int _RowNumber;

        public CatalogPriceRulePaging()
        {

        }
                
        public int RowTotal
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
        
        public int CatalogPriceRuleID
        {
            get
            {
                return this._catalogPriceRuleID;
            }
            set
            {
                if ((this._catalogPriceRuleID != value))
                {
                    this._catalogPriceRuleID = value;
                }
            }
        }
       
        public string CatalogPriceRuleName
        {
            get
            {
                return this._catalogPriceRuleName;
            }
            set
            {
                if ((this._catalogPriceRuleName != value))
                {
                    this._catalogPriceRuleName = value;
                }
            }
        }
        
        public System.Nullable<System.DateTime> FromDate
        {
            get
            {
                return this._fromDate;
            }
            set
            {
                if ((this._fromDate != value))
                {
                    this._fromDate = value;
                }
            }
        }
        
        public System.Nullable<System.DateTime> ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }
        
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
        [DataMember(Name = "RowNumber", Order = 6)]
        public int Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                if ((this._priority != value))
                {
                    this._priority = value;
                }
            }
        }
    }
}
