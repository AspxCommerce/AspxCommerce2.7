﻿/*
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
    public class TaxItemClassInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_taxItemClassID", Order = 1)]
        private System.Nullable<int> _taxItemClassID;

        [DataMember(Name = "_taxItemClassName", Order = 2)]
        private string _taxItemClassName;
        public TaxItemClassInfo()
        {
        }
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

        public System.Nullable<int> TaxItemClassID
        {
            get
            {
                return this._taxItemClassID;
            }
            set
            {
                if ((this._taxItemClassID != value))
                {
                    this._taxItemClassID = value;
                }
            }
        }

        public string TaxItemClassName
        {
            get
            {
                return this._taxItemClassName;
            }
            set
            {
                if ((this._taxItemClassName != value))
                {
                    this._taxItemClassName = value;
                }
            }
        }
    }
    public class TaxDateData {
         public int offset{get;set;}
         public int? limit{get;set;}
         public string taxRuleName{get;set;}
         public bool monthly{get;set;}
         public bool weekly{get;set;}
         public bool hourly{get;set;}
    }
}
