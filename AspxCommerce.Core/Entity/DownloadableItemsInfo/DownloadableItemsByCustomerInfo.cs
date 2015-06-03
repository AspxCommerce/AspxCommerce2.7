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
    public class DownloadableItemsByCustomerInfo
    {
        #region Constructor
        public DownloadableItemsByCustomerInfo()
        {
        }
        #endregion
        #region Private Fields
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        
        [DataMember(Name = "_orderItemID", Order = 1)]
        private int _orderItemID;

        [DataMember(Name = "_orderItemIDDup", Order = 2)]
        private int _orderItemIDDup;

        [DataMember(Name = "_orderID", Order = 3)]
        private int _orderID;

        [DataMember(Name = "_randomNo", Order = 4)]
        private string _randomNo;


        [DataMember(Name = "_itemID", Order = 5)]
        private int _itemID;

        [DataMember(Name = "_sku", Order = 6)]
        private string _sku;

        [DataMember(Name = "_itemName", Order = 7)]
        private string _itemName;

        [DataMember(Name = "_sampleLink", Order = 8)]
        private string _sampleLink;

        [DataMember(Name = "_actualLink", Order = 9)]
        private string _actualLink;

        [DataMember(Name = "_sampleFile", Order = 10)]
        private string _sampleFile;

        [DataMember(Name = "_actualFile", Order = 11)]
        private string _actualFile;

        [DataMember(Name = "_orderStatusID", Order = 12)]
        private int _orderStatusID;

        [DataMember(Name = "_status", Order = 13)]
        private string _status;

        [DataMember(Name = "_downloads", Order = 14)]
        private string _downloads;

        [DataMember(Name = "_remainingDownload", Order = 15)]
        private string _remainingDownload;

        [DataMember(Name = "_lastDownloadDate", Order = 16)]
        private String _lastDownloadDate;


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

       

        public int OrderItemID
        {
            get
            {
                return this._orderItemID;
            }
            set
            {
                if ((this._orderItemID != value))
                {
                    this._orderItemID = value;
                }
            }
        }

        public int OrderItemIDDup
        {
            get
            {
                return this._orderItemIDDup;
            }
            set
            {
                if ((this._orderItemIDDup != value))
                {
                    this._orderItemIDDup = value;
                }
            }
        }

        public int OrderID
        {
            get
            {
                return this._orderID;
            }
            set
            {
                if ((this._orderID != value))
                {
                    this._orderID = value;
                }
            }
        }

        public string RandomNo
        {
            get
            {
                return this._randomNo;
            }
            set
            {
                if ((this._randomNo != value))
                {
                    this._randomNo = value;
                }
            }
        }

        public int ItemID
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

        public string SampleLink
        {
            get
            {
                return this._sampleLink;
            }
            set
            {
                if ((this._sampleLink != value))
                {
                    this._sampleLink = value;
                }
            }
        }

        public string ActualLink
        {
            get
            {
                return this._actualLink;
            }
            set
            {
                if ((this._actualLink != value))
                {
                    this._actualLink = value;
                }
            }
        }

        public string SampleFile
        {
            get
            {
                return this._sampleFile;
            }
            set
            {
                if ((this._sampleFile != value))
                {
                    this._sampleFile = value;
                }
            }
        }

        public string ActualFile
        {
            get
            {
                return this._actualFile;
            }
            set
            {
                if ((this._actualFile != value))
                {
                    this._actualFile = value;
                }
            }
        }

        public int OrderStatusID
        {
            get
            {
                return this._orderStatusID;
            }
            set
            {
                if ((this._orderStatusID != value))
                {
                    this._orderStatusID = value;
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

        public string Downloads
        {
            get
            {
                return this._downloads;
            }
            set
            {
                if ((this._downloads != value))
                {
                    this._downloads = value;
                }
            }
        }

        public string RemainingDownload
        {
            get
            {
                return this._remainingDownload;
            }
            set
            {
                if ((this._remainingDownload != value))
                {
                    this._remainingDownload = value;
                }
            }
        }

        public String LastDownloadDate
        {
            get
            {
                return this._lastDownloadDate;
            }
            set
            {
                if ((this._lastDownloadDate != value))
                {
                    this._lastDownloadDate = value;
                }
            }
        }


        #endregion
    }
}
