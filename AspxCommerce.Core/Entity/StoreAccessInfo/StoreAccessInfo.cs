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
   
    public  class StoreAccessInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_storeAccessID", Order = 1)]
        private int _storeAccessID;
        [DataMember(Name = "_storeAccessData", Order = 2)]
        private string _storeAccessData;
        [DataMember(Name = "_reason", Order = 3)]
        private string _reason;
        [DataMember(Name = "_isActive", Order = 5)]
        private System.Nullable<bool> _isActive;
        [DataMember(Name = "_addedOn", Order = 4)]
        private string _addedOn;

        [DataMember(Name = "_storeAccessKeyValue", Order = 6)]
        private string _storeAccessKeyValue;
        [DataMember(Name = "_skid", Order = 7)]
        private int _skid;  

        public StoreAccessInfo()
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
        public int StoreAccessID
        {
            get
            {
                return this._storeAccessID;
            }
            set
            {
                if ((this._storeAccessID != value))
                {
                    this._storeAccessID = value;
                }
            }
        }

        public string StoreAccessData
        {
            get
            {
                return this._storeAccessData;
            }
            set
            {
                if ((this._storeAccessData != value))
                {
                    this._storeAccessData = value;
                }
            }
        }

       
        public string Reason
        {
            get
            {
                return this._reason;
            }
            set
            {
                if ((this._reason != value))
                {
                    this._reason = value;
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

     
        public string StoreAccessKeyValue
        {
            get
            {
                return this._storeAccessKeyValue;
            }
            set
            {
                if ((this._storeAccessKeyValue != value))
                {
                    this._storeAccessKeyValue = value;
                }
            }
        }

        public int SKID
        {
            get
            {
                return this._skid;
            }
            set
            {
                if ((this._skid != value))
                {
                    this._skid = value;
                }
            }
        }
    }
}
