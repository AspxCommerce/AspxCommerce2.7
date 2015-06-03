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
   public class CardTypeInfo
    {
        public CardTypeInfo()
        {
        }

        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_cardTypeID", Order = 1)]
        private int _cardTypeID;

        [DataMember(Name = "_cardTypeName", Order = 2)]
        private string _cardTypeName;

        [DataMember(Name = "_isSystemUsed", Order = 3)]
        private System.Nullable<bool> _isSystemUsed;

        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;

        [DataMember(Name = "_isDeleted", Order = 5)]
        private System.Nullable<bool> _isDeleted;

        [DataMember(Name = "_imagePath", Order = 6)]
        private string _imagePath;

        [DataMember(Name = "_alternateText", Order = 7)]
        private string _alternateText;

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

        public int CardTypeID
        {
            get
            {
                return this._cardTypeID;
            }
            set
            {
                if ((this._cardTypeID != value))
                {
                    this._cardTypeID = value;
                }
            }
        }

        public string CardTypeName
        {
            get
            {
                return this._cardTypeName;
            }
            set
            {
                if ((this._cardTypeName != value))
                {
                    this._cardTypeName = value;
                }
            }
        }

        public System.Nullable<bool> IsSystemUsed
        {
            get
            {
                return this._isSystemUsed;
            }
            set
            {
                if ((this._isSystemUsed != value))
                {
                    this._isSystemUsed = value;
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
                {
                    this._isActive = value;
                }
            }
        }

        public System.Nullable<bool> IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
            set
            {
                if ((this._isDeleted != value))
                {
                    this._isDeleted = value;
                }
            }
        }

        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                if ((this._imagePath != value))
                {
                    this._imagePath = value;
                }
            }
        }

        public string AlternateText
        {
            get
            {
                return this._alternateText;
            }
            set
            {
                if ((this._alternateText != value))
                {
                    this._alternateText = value;
                }
            }
        }
    }

    public class CardTypeSaveInfo
    {
        public int CardTypeID { get; set; }
        public string CardTypeName { get; set; }
        public bool IsActive { get; set; }
        public string AlternateText { get; set; }
        public string ImagePath { get; set; }
        public string NewFilePath { get; set; }
        public string PrevFilePath { get; set; }
    }
}
