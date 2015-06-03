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




namespace AspxCommerce.Core
{
    public class CouponPortalUserListInfo
    {
        public CouponPortalUserListInfo()
        {
        }

        private System.Nullable<int> _rowTotal;

        private int _portalUserID;

        private string _userName;

        private string _customerName;

        private string _email;

        private bool _isAlreadySent;

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

        public int PortalUserID
        {
            get
            {
                return this._portalUserID;
            }
            set
            {
                if ((this._portalUserID != value))
                {
                    this._portalUserID = value;
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

        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                if ((this._customerName != value))
                {
                    this._customerName = value;
                }
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                if ((this._email != value))
                {
                    this._email = value;
                }
            }
        }
        public bool IsAlreadySent
        {
            get
            {
                return this._isAlreadySent;
            }
            set
            {
                if((this._isAlreadySent!=value))
                {
                    this._isAlreadySent=value;
                }
            }
        }
    }
}
