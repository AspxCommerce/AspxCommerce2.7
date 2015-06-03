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
   public class DownLoadableItemGetInfo
   {
       #region Constructors
       public DownLoadableItemGetInfo()
       {
       }
       #endregion

       #region Private Fields
       [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        //[DataMember(Name = "_ID", Order = 1)]
        //private int _ID;

        [DataMember(Name = "_sku", Order = 1)]
        private string _sku;

        [DataMember(Name = "_itemName", Order = 2)]
        private string _itemName;

        [DataMember(Name = "_sampleLink", Order = 3)]
        private string _sampleLink;

        [DataMember(Name = "_actualLink", Order = 4)]
        private string _actualLink;

        [DataMember(Name = "_sampleFile", Order = 5)]
        private string _sampleFile;

        [DataMember(Name = "_actualFile", Order = 6)]
        private string _actualFile;
        
        

        [DataMember(Name = "_purchases", Order = 7)]
        private string _purchases;

        [DataMember(Name = "_downloads", Order = 8)]
        private string _downloads;
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

        //public int ID
        //{
        //    get
        //    {
        //        return this._ID;
        //    }
        //    set
        //    {
        //        if ((this._ID != value))
        //        {
        //            this._ID = value;
        //        }
        //    }
        //}

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

       

        public string Purchases
        {
            get
            {
                return this._purchases;
            }
            set
            {
                if ((this._purchases != value))
                {
                    this._purchases = value;
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

        #endregion
    }

    public class GetDownloadableItemInfo:ItemSmallCommonInfo
    {
        public bool? CheckUser { get; set; }
    }
}
