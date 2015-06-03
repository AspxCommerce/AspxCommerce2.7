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
    public class ShareWishListItemInfo
    {
        public ShareWishListItemInfo()
        {
        }
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_shareWishID", Order = 1)]
        private int _shareWishID;

        [DataMember(Name = "_sharedWishItemIDs", Order = 2)]
        private string _sharedWishItemIDs;

        [DataMember(Name = "_sharedWishItemName", Order = 3)]
        private string _sharedWishItemName;

        [DataMember(Name = "_itemSku", Order = 4)]
        private string _itemSku;

        [DataMember(Name = "_senderName", Order = 5)]
        private string _senderName;

        [DataMember(Name = "_senderEmail", Order = 6)]
        private string _senderEmail;

        [DataMember(Name = "_receiverEmailID", Order = 7)]
        private string _receiverEmailID;

        [DataMember(Name = "_subject", Order = 8)]
        private string _subject;


        [DataMember(Name = "_message", Order = 9)]
        private string _message;

        [DataMember(Name = "_addedOn", Order = 10)]
        private string _addedOn;
        

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

        public int ShareWishID
        {
            get
            {
                return this._shareWishID;
            }
            set
            {
                if ((this._shareWishID != value))
                {
                    this._shareWishID = value;
                }
            }
        }

        public string SharedWishItemIDs
        {
            get
            {
                return this._sharedWishItemIDs;
            }
            set
            {
                if ((this._sharedWishItemIDs != value))
                {
                    this._sharedWishItemIDs = value;
                }
            }
        }

        public string SharedWishItemName
        {
            get
            {
                return this._sharedWishItemName;
            }
            set
            {
                if ((this._sharedWishItemName != value))
                {
                    this._sharedWishItemName = value;
                }
            }
        }

        public string ItemSku
        {
            get
            {
                return this._itemSku;
            }
            set
            {
                if ((this._itemSku != value))
                {
                    this._itemSku = value;
                }
            }
        }

        public string SenderName
        {
            get
            {
                return this._senderName;
            }
            set
            {
                if ((this._senderName != value))
                {
                    this._senderName = value;
                }
            }
        }

        public string SenderEmail
        {
            get
            {
                return this._senderEmail;
            }
            set
            {
                if ((this._senderEmail != value))
                {
                    this._senderEmail = value;
                }
            }
        }

        public string ReceiverEmailID
        {
            get
            {
                return this._receiverEmailID;
            }
            set
            {
                if ((this._receiverEmailID != value))
                {
                    this._receiverEmailID = value;
                }
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                if ((this._subject != value))
                {
                    this._subject = value;
                }
            }
        }

        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                if ((this._message != value))
                {
                    this._message = value;
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
    }
}
