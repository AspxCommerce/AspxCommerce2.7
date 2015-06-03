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



using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    public class StatusInfo
    {
        public StatusInfo()
        {
        }
        private System.Nullable<int> _statusID;
        private string _status;
                private System.Nullable<int> _orderStatusID;
        private string _orderStatusName;

        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public System.Nullable<int> OrderStatusID
        {
            get
            {
                return this._orderStatusID;
            }
            set
            {
                if (this._orderStatusID != value)
                {
                    this._orderStatusID = value;
                }
            }
        }
        [DataMember]
        public string OrderStatusName
        {
            get
            {
                return this._orderStatusName;
            }
            set
            {
                if (this._orderStatusName != value)
                {
                    this._orderStatusName = value;
                }
            }
        }
    }
}
