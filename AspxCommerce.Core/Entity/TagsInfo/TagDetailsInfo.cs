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
    public class TagDetailsInfo
    {
        public TagDetailsInfo()
        {
        }

        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_itemTagIDs", Order = 1)]
        private string _itemTagIDs;

        [DataMember(Name = "_tag", Order = 2)]
        private string _tag;

        [DataMember(Name = "_userCount", Order = 3)]
        private System.Nullable<int> _userCount;

        [DataMember(Name = "_itemCount", Order = 4)]
        private System.Nullable<int> _itemCount;

        [DataMember(Name = "_status", Order = 5)]
        private string _status;

        [DataMember(Name = "_statusID", Order = 6)]
        private System.Nullable<int> _statusID;

        [DataMember(Name = "_userIDs", Order = 7)]
        private string _userIDs;

        [DataMember(Name = "_itemIDs", Order = 8)]
        private string _itemIDs;

        [DataMember(Name = "_tagCount", Order = 9)]
        private System.Nullable<int> _tagCount;

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

        public string ItemTagIDs
        {
            get
            {
                return this._itemTagIDs;
            }
            set
            {
                if (this._itemTagIDs != value)
                {
                    this._itemTagIDs = value;
                }
            }
        }

        public string Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                if ((this._tag != value))
                {
                    this._tag = value;
                }
            }
        }

        public System.Nullable<int> UserCount
        {
            get
            {
                return this._userCount;
            }
            set
            {
                if ((this._userCount != value))
                {
                    this._userCount = value;
                }
            }
        }

        public System.Nullable<int> ItemCount
        {
            get
            {
                return this._itemCount;
            }
            set
            {
                if ((this._itemCount != value))
                {
                    this._itemCount = value;
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
                if (this._status != value)
                {
                    this._status = value;
                }
            }
        }
        public System.Nullable<int> StatusID
        {
            get
            {
                return this._statusID;
            }
            set
            {
                if (this._statusID != value)
                {
                    this._statusID = value;
                }
            }
        }

        public string UserIDs
        {
            get
            {
                return this._userIDs;
            }
            set
            {
                if ((this._userIDs != value))
                {
                    this._userIDs = value;
                }
            }
        }

        public string ItemIDs
        {
            get
            {
                return this._itemIDs;
            }
            set
            {
                if ((this._itemIDs != value))
                {
                    this._itemIDs = value;
                }
            }
        }

        public System.Nullable<int> TagCount
        {
            get
            {
                return this._tagCount;
            }
            set
            {
                if ((this._tagCount != value))
                {
                    this._tagCount = value;
                }
            }
        }
    }
}
 
