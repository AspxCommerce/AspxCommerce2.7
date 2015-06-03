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
    public class CustomerDetailsInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_customerID", Order = 1)]
        private int _customerID;

        [DataMember(Name = "_userName", Order = 2)]
        private string _userName;

        [DataMember(Name = "_cultureName", Order = 3)]
        private string _cultureName;

        [DataMember(Name = "_addedOn", Order = 4)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_updatedOn", Order = 5)]
        private System.Nullable<System.DateTime> _updatedOn;

        [DataMember(Name = "_isSameUser", Order = 6)]
        private bool _isSameUser;

        public CustomerDetailsInfo()
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

        public int CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                if ((this._userName != value))
                {
                    this._userName = value;
                }
            }
        }

        public string CultureName
        {
            get
            {
                return this._cultureName;
            }
            set
            {
                if ((this._cultureName != value))
                {
                    this._cultureName = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
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

        public System.Nullable<System.DateTime> UpdatedOn
        {
            get
            {
                return this._updatedOn;
            }
            set
            {
                if ((this._updatedOn != value))
                {
                    this._updatedOn = value;
                }
            }
        }
        public bool IsSameUser
        {
            get
            {
                return this._isSameUser;
            }
            set
            {
                if ((this._isSameUser != value))
                {
                    this._isSameUser = value;
                }
            }
        }
    }
}
