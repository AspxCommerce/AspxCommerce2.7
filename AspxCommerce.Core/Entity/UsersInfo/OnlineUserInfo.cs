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
    public class OnlineUserInfo
    {
        [DataMember(Name = "_RowTotal", Order = 0)]
        public System.Nullable<int> RowTotal { get; set; }

        [DataMember(Name = "_UserName", Order = 1)]
        public string UserName { get; set; }

        [DataMember(Name = "_SessionUserHostAddress", Order = 2)]
        public string SessionUserHostAddress { get; set; }

        [DataMember(Name = "_SessionUserAgent", Order = 3)]
        public string SessionUserAgent { get; set; }

        [DataMember(Name = "_SessionBrowser", Order = 4)]
        public string SessionBrowser { get; set; }

        [DataMember(Name = "_SessionURL", Order = 5)]
        public string SessionURL { get; set; }

        [DataMember(Name = "_Start", Order = 6)]
        public DateTime Start { get; set; }

        public string SessionCrawler { get; set; }
        public string SessionOriginalReferrer { get; set; }
        public string SessionOriginalURL { get; set; }
        public string PortalID { get; set; }
        public DateTime End { get; set; }
    }
}
