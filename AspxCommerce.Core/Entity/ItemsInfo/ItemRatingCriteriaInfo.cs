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
    [Serializable]
    [DataContract]
    public class ItemRatingCriteriaInfo
    {

      //  [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

      //  [DataMember(Name = "_itemRatingCriteriaID", Order = 1)]
        private System.Nullable<int> _itemRatingCriteriaID;

      //  [DataMember(Name = "_itemRatingCriteria", Order = 2)]
        private string _itemRatingCriteria;

     //   [DataMember(Name = "_addedOn", Order = 3)]
        private DateTime _addedOn;

     //   [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;


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
        public System.Nullable<int> ItemRatingCriteriaID
        {
            get
            {
                return this._itemRatingCriteriaID;
            }
            set
            {
                if((this._itemRatingCriteriaID != value))
                {
                    this._itemRatingCriteriaID=value;
                }
            }
        }
        public string ItemRatingCriteria
        {
            get
            {
                return this._itemRatingCriteria;
            }
            set
            {
                if ((this._itemRatingCriteria != value))
                {
                    this._itemRatingCriteria = value;
                }
            }
        }
        public DateTime AddedOn
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
        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                    this._isActive = value;
            }
        }
    }
       
}
