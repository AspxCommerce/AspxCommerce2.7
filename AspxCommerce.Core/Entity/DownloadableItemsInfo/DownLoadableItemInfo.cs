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
    public class DownLoadableItemInfo
    {
        public DownLoadableItemInfo()
        {
        }
        private int _itemID;
        private int _downloadableID;
        private string _title;
        private int _maxDownload;
        private bool _isSharable;
        private string _sampleFile;
        private string _actualFile;
        private int _displayOrder;

        [DataMember]
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
        [DataMember]
        public int DownloadableID
        {
            get
            {
                return this._downloadableID;
            }
            set
            {
                if ((this._downloadableID != value))
                {
                    this._downloadableID = value;
                }
            }
        }
        [DataMember]
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if ((this._title != value))
                {
                    this._title = value;
                }
            }
        }
        [DataMember]
        public int MaxDownload
        {
            get
            {
                return this._maxDownload;
            }
            set
            {
                if ((this._maxDownload != value))
                {
                    this._maxDownload = value;
                }
            }
        }
        [DataMember]
        public bool IsSharable
        {
            get
            {
                return this._isSharable;
            }
            set
            {
                if ((this._isSharable != value))
                {
                    this._isSharable = value;
                }
            }
        }
        [DataMember]
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
        [DataMember]
        public string  ActualFile
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
        [DataMember]
        public int DisplayOrder
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
    }
}

		