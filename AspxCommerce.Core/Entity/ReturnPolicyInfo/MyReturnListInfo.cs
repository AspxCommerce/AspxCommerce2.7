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
    public class MyReturnListInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_returnID", Order = 1)]
        private System.Nullable<int> _returnID;

        [DataMember(Name = "_orderID", Order = 2)]
        private System.Nullable<int> _orderID;

        [DataMember(Name = "_returnStatus", Order = 3)]
        private string _returnStatus;

        [DataMember(Name = "_returnedDate", Order = 4)]
        private string _returnFileDate;

        [DataMember(Name = "_customerID", Order = 5)]
        private System.Nullable<int> _customerID;

        [DataMember(Name = "_userName", Order = 6)]
        private string _userName; 
        

        public MyReturnListInfo()
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

        public System.Nullable<int> ReturnID
        {
            get
            {
                return this._returnID;
            }
            set
            {
                if ((this._returnID != value))
                {
                    this._returnID = value;
                }
            }
        }
        
        public System.Nullable<int> OrderID
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


        public string ReturnStatus
        {
            get
            {
                return this._returnStatus;
            }
            set
            {
                if ((this._returnStatus != value))
                {
                    this._returnStatus = value;
                }
            }
        }

        public string ReturnFileDate
        {
            get
            {
                return this._returnFileDate;
            }
            set
            {
                if ((this._returnFileDate != value))
                {
                    this._returnFileDate = value;
                }
            }
        }

        public System.Nullable<int> CustomerID
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
		
		
    }
}
