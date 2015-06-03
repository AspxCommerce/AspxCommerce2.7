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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxCommonInfo
    {

        private int storeID;
        private int portalID;
        private string userName;
        private string cultureName;
        private int? customerID;
        private string sessionCode;
        public AspxCommonInfo()
        {
        }
        public AspxCommonInfo(int storeID, int portalID, string userName, string cultureName, int? customerID, string sessionCode)
        {
            StoreID = storeID;
            PortalID = portalID;
            UserName = userName;
            CultureName = cultureName;
            CustomerID = customerID;
            SessionCode = sessionCode;
        }

        public AspxCommonInfo(int storeID, int portalID, string userName, string cultureName)
        {
            StoreID = storeID;
            PortalID = portalID;
            UserName = userName;
            CultureName = cultureName;           
        }

        public AspxCommonInfo(int storeID, int portalID, string cultureName)
        {
            StoreID = storeID;
            PortalID = portalID;           
            CultureName = cultureName;
        }      
        

        public int StoreID 
        {
            get { return storeID; }
            set { storeID = value; }
        }
        public int PortalID
        {
            get { return portalID; }
            set { portalID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string CultureName
        {
            get { return cultureName; }
            set { cultureName = value; }
        }
        public int? CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public string SessionCode
        {
            get { return sessionCode; }
            set { sessionCode = value; }
        }
    }

    public class AspxExtraCommonInfo
    {
        public string IP { get; set; }
        public string CountryName { get; set; }
        public string TemplateName { get; set; }
        public bool? IsUserFriendlyUrl { get; set; }
        public string AspxServicePath { get; set; }
        public string AspxRedirectPath { get; set; }
        public string AspxRoothPath { get; set; }
        public string AspxTemplateFolderpath { get; set; }
    }
}
