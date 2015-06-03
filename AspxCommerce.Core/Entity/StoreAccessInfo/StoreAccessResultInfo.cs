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
    public class StoreAccessResultInfo
    {

        #region Private Fields
        private System.Nullable<bool> _isAccess;

        private System.Nullable<bool> _storeClosed;
        #endregion

        #region Constructor
        public StoreAccessResultInfo()
        {
        }        
        #endregion

        #region Public Fields
        public System.Nullable<bool> IsAccess
        {
            get
            {
                return this._isAccess;
            }
            set
            {
                if ((this._isAccess != value))
                {
                    this._isAccess = value;
                }
            }
        }

        public System.Nullable<bool> StoreClosed
        {
            get
            {
                return this._storeClosed;
            }
            set
            {
                if ((this._storeClosed != value))
                {
                    this._storeClosed = value;
                }
            }
        }
        #endregion
    }
}
