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



using System.Web;

namespace AspxCommerce.Core
{
    public class StoreAccessDetailsInfo
    {
        #region Private Fields
        private HttpContext _context;
        private string _userIPAddress;
        private string _userDomainURL;
        private int _portalID;
        private int _storeID;
        private string _username;
        #endregion

        #region Constructor
        public StoreAccessDetailsInfo()
        {
            try
            {
                _context = HttpContext.Current;
                _userIPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                if ((_context.Request.Url != null))
                {
                    _userDomainURL = HttpContext.Current.Request.Url.ToString();
                }
            }
            catch
            {
            }
        } 
        #endregion

        #region Public Fields

        public string UserDomainURL
        {
            get { return _userDomainURL; }
        }

        public string UserIPAddress
        {
            get { return _userIPAddress; }
        }

        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }

        public int StoreID
        {
            get { return _storeID; }
            set { _storeID = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        #endregion
    }
}
