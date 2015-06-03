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
    public class UserRatingInformationInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_itemReviewID", Order = 1)]
        private int _itemReviewID;
        [DataMember(Name = "_itemID", Order = 2)]
        private System.Nullable<int> _itemID;
        [DataMember(Name = "_username", Order = 3)]
        private string _username;
        [DataMember(Name = "_totalRatingAverage", Order = 4)]
        private string _totalRatingAverage;
        [DataMember(Name = "_viewFromIP", Order = 5)]
        private string _viewFromIP;
        [DataMember(Name = "_reviewSummary", Order = 6)]
        private string _reviewSummary;
        [DataMember(Name = "_review", Order = 7)]
        private string _review;
        [DataMember(Name = "_status", Order = 8)]
        private string _status;
        [DataMember(Name = "_itemName", Order = 9)]
        private string _itemName;
        [DataMember(Name = "_addedOn", Order = 10)]
        private string _addedOn;
        [DataMember(Name = "_addedBy", Order = 11)]
        private string _addedBy;
        [DataMember(Name = "_statusID", Order = 12)]
        private int _statusID;
        [DataMember(Name = "_sku", Order = 13)]
        private string _sku;

        public UserRatingInformationInfo()
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
        public int ItemReviewID
        {
            get
            {
                return this._itemReviewID;
            }
            set
            {
                if ((this._itemReviewID != value))
                {
                    this._itemReviewID = value;
                }
            }
        }
        public System.Nullable<int> ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if ((this._itemID != value))
                {
                    this._itemID = value;
                }
            }
        }
        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                if ((this._username != value))
                {
                    this._username = value;
                }
            }
        }
        public string TotalRatingAverage
        {
            get
            {
                return this._totalRatingAverage;
            }
            set
            {
                if ((this._totalRatingAverage != value))
                {
                    this._totalRatingAverage = value;
                }
            }
        }
        public string ViewFromIP
        {
            get
            {
                return this._viewFromIP;
            }
            set
            {
                if ((this._viewFromIP != value))
                {
                    this._viewFromIP = value;
                }
            }
        }
        public string ReviewSummary
        {
            get
            {
                return this._reviewSummary;
            }
            set
            {
                if ((this._reviewSummary != value))
                {
                    this._reviewSummary = value;
                }
            }
        }

        public string Review
        {
            get
            {
                return this._review;
            }
            set
            {
                if ((this._review != value))
                {
                    this._review = value;
                }
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if ((this._status != value))
                {
                    this._status = value;
                }
            }
        }
        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                if ((this._itemName != value))
                {
                    this._itemName = value;
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
        public int StatusID
        {
            get
            {
                return this._statusID;
            }
            set
            {
                if ((this._statusID != value))
                {
                    this._statusID = value;
                }
            }
        }
        public string SKU
        {
            get
            {
                return this._sku;
            }
            set
            {
                if ((this._sku != value))
                {
                    this._sku = value;
                }
            }
        }
    }
    public class UserRatingBasicInfo
    {
        public System.Nullable<int> ItemID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string ItemName { get; set; }

    }
}
