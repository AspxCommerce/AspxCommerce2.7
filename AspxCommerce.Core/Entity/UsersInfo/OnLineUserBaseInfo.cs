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
    public class OnLineUserBaseInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_userName", Order = 1)]
        private string _userName;

        [DataMember(Name = "_sessionUserHostAddress", Order = 2)]
        private string _sessionUserHostAddress;

        [DataMember(Name = "_sessionUserAgent", Order = 3)]
        private string _sessionUserAgent;

        [DataMember(Name = "_sessionBrowser", Order = 4)]
        private string _sessionBrowser;

        [DataMember(Name = "_sessionURL", Order = 5)]
        private string _sessionURL;

        [DataMember(Name = "_start", Order = 6)]
        private string _start;

        public OnLineUserBaseInfo()
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
        public string SessionUserHostAddress
        {
            get
            {
                return this._sessionUserHostAddress;
            }
            set
            {
                if ((this._sessionUserHostAddress != value))
                {
                    this._sessionUserHostAddress = value;
                }
            }
        }
        public string SessionUserAgent
        {
            get
            {
                return this._sessionUserAgent;
            }
            set
            {
                if ((this._sessionUserAgent != value))
                {
                    this._sessionUserAgent = value;
                }
            }
        }
        public string SessionBrowser
        {
            get
            {
                return this._sessionBrowser;
            }
            set
            {
                if ((this._sessionBrowser != value))
                {
                    this._sessionBrowser = value;
                }
            }
        }
        public string SessionURL
        {
            get
            {
                return this._sessionURL;
            }
            set
            {
                if ((this._sessionURL != value))
                {
                    this._sessionURL = value;
                }
            }
        }
        public string Start
        {
            get
            {
                return this._start;
            }
            set
            {
                if ((this._start != value))
                {
                    this._start = value;
                }
            }
        }
    }
}
