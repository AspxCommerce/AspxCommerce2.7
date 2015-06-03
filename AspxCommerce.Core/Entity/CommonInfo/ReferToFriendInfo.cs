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
    public class ReferToFriendInfo
    {
        [DataMember(Name = "_rowTotal", Order = 1)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_emailAFriendID", Order = 2)]
        private int _emailAFriendID;

        [DataMember(Name = "_itemID", Order = 3)]
        private System.Nullable<int> _itemID;

        [DataMember(Name = "_senderName", Order = 4)]
        private string _senderName;

        [DataMember(Name = "_senderEmail", Order = 5)]
        private string _senderEmail;

        [DataMember(Name = "_receiverName", Order = 6)]
        private string _receiverName;

        [DataMember(Name = "_receiverEmail", Order = 7)]
        private string _receiverEmail;

        [DataMember(Name = "_subject", Order = 8)]
        private string _subject;

        [DataMember(Name = "_message", Order = 9)]
        private string _message;


        [DataMember(Name = "_addedOn", Order = 10)]
        private string _addedOn;

        [DataMember(Name = "_itemName", Order = 11)]
        private string _itemName;
        [DataMember(Name = "_SKU", Order = 12)]
        private string _SKU;

        public ReferToFriendInfo()
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

        public int EmailAFriendID
        {
            get
            {
                return this._emailAFriendID;
            }
            set
            {
                if ((this._emailAFriendID != value))
                {
                    this._emailAFriendID = value;
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

        public string ReceiverName
        {
            get
            {
                return this._receiverName;
            }
            set
            {
                if ((this._receiverName != value))
                {
                    this._receiverName = value;
                }
            }
        }

        public string ReceiverEmail
        {
            get
            {
                return this._receiverEmail;
            }
            set
            {
                if ((this._receiverEmail != value))
                {
                    this._receiverEmail = value;
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
        public string SKU
        {
            get
            {
                return this._SKU;
            }
            set
            {
                if ((this._SKU != value))
                {
                    this._SKU = value;
                }
            }
        }
    }
    public class ReferToFriendEmailInfo : SendEmailInfo
    {
        public int ItemID { get; set; }
    }
}
