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
using System.Web;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxShoppingBagHeader_ShoppingBagHeader : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string SessionCode = string.Empty;
    public string AllowRealTimeNotifications,ShowMiniShopCart, AllowAddToCart, AllowMultipleAddChkOut,
        MinCartSubTotalAmount, AllowAnonymousCheckOut, ShoppingCartURL, SingleAddressChkOutURL;
    public string BagType;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            GetPortalCommonInfo(out  StoreID, out  PortalID, out  CustomerID, out  UserName, out  CultureName);
            if (!IsPostBack)
            {
                IncludeCss("ShoppingBagHeader", "/Templates/" + TemplateName + "/css/PopUp/style.css", 
                    "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Modules/AspxCommerce/AspxShoppingBagHeader/css/module.css");
                IncludeJs("ShoppingBagHeader", "/js/PopUp/custom.js", "/js/MessageBox/jquery.easing.1.3.js", 
                    "/js/MessageBox/alertbox.js",
                    "/Modules/AspxCommerce/AspxShoppingBagHeader/js/ShoppingBag.js", "/js/Session.js");   
                UserModuleID = SageUserModuleID;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamEight(StoreSetting.ShowMiniShoppingCart, StoreSetting.ShowAddToCartButton,
                    StoreSetting.AllowMultipleShippingAddress, StoreSetting.MinimumCartSubTotalAmount, StoreSetting.AllowAnonymousCheckOut, 
                    StoreSetting.ShoppingCartURL,StoreSetting.SingleCheckOutURL, StoreSetting.AllowRealTimeNotifications,
                    out ShowMiniShopCart, out AllowAddToCart, out AllowMultipleAddChkOut, out MinCartSubTotalAmount, out AllowAnonymousCheckOut,
                    out ShoppingCartURL, out SingleAddressChkOutURL, out AllowRealTimeNotifications, StoreID, PortalID, CultureName);
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID,CultureName);
                BagType = AspxShoppingBagController.GetShoppingBagSetting(aspxCommonObj);
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public string UserModuleID { get; set; }
}
