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
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxUserDashBoard_UserRecentHistory : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName, AllowAddToCompare, SessionCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);

            StoreSettingConfig ssc = new StoreSettingConfig();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
