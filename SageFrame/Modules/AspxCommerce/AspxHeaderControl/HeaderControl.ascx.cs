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
using System.Collections;
using System.Text;
using SageFrame.RolesManagement;
using System.Collections.Generic;
using AspxCommerce.WishItem;

public partial class Modules_AspxHeaderControl_HeaderControl : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string SessionCode = string.Empty;
    public string MyAccountURL, ShoppingCartURL,
        WishListURL, AllowAddToCart, AllowAnonymousCheckOut, AllowMultipleShipping,
        MinCartSubTotalAmount, SingleAddressChkOutURL, LogInURL;
    public bool IsUseFriendlyUrls = true;
    public bool FrmLogin = false;
    public int LoginMessageInfoCount = 0;
    public int CartCount = 0;
    public int WishCount = 0;
    public string HeaderType;
    public int userRoleBit = 0;
    protected void Page_PreRender(object sender, EventArgs e)
    {
        GetCartItemsCount();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);

            SageFrameConfig sfConfig = new SageFrameConfig();
            LogInURL = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage);

            HeaderSettingInfo objHeaderSetting = new HeaderSettingInfo();
            objHeaderSetting = AspxHeaderController.GetHeaderSetting(aspxCommonObj);
            HeaderType = objHeaderSetting.HeaderType;

            HeaderItemsCount objHeaderCount = new HeaderItemsCount();
            AspxHeaderController objHeader = new AspxHeaderController();
            objHeaderCount = objHeader.GetHeaderItemsCount(aspxCommonObj);
            WishCount = objHeaderCount.WishCount;
            CartCount = objHeaderCount.CartCount;

            hst = AppLocalized.getLocale(this.AppRelativeTemplateSourceDirectory);//

            StoreSettingConfig ssc = new StoreSettingConfig();
            ssc.GetStoreSettingParamSeven(StoreSetting.MyAccountURL, StoreSetting.ShoppingCartURL,
                StoreSetting.ShowAddToCartButton, StoreSetting.AllowAnonymousCheckOut, StoreSetting.AllowMultipleShippingAddress,
                StoreSetting.MinimumCartSubTotalAmount, StoreSetting.SingleCheckOutURL, out MyAccountURL, out ShoppingCartURL,
                out AllowAddToCart, out AllowAnonymousCheckOut, out AllowMultipleShipping, out MinCartSubTotalAmount,
                out SingleAddressChkOutURL, StoreID, PortalID, CultureName);

            string templateName = TemplateName;

            if (HttpContext.Current.Session.SessionID != null)
            {
                SessionCode = HttpContext.Current.Session.SessionID.ToString();
            }
            if (!IsPostBack)
            {
                IncludeCss("AspxHeaderControl", "/Templates/" + templateName + "/css/MessageBox/style.css", "/Templates/" + templateName + "/css/PopUp/style.css",
                     "/Modules/AspxCommerce/AspxHeaderControl/css/module.css");
                IncludeJs("AspxHeaderControl", "/js/PopUp/custom.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxHeaderControl/js/HeaderControl.js");//"/js/jquery.easing.1.3.js",
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    string urlContent = HttpContext.Current.Request.UrlReferrer.AbsolutePath;
                    if (urlContent.Contains(LogInURL) && UserName.ToLower() != "anonymoususer")
                    {
                        FrmLogin = true;
                        if (HttpContext.Current.Session["LoginMessageInfo"] == null)
                        {
                            HttpContext.Current.Session["LoginMessageInfo"] = true;
                        }
                        int x = Convert.ToInt32(HttpContext.Current.Session["LoginMessageInfoCount"]);
                        HttpContext.Current.Session["LoginMessageInfoCount"] = x + 1;
                    }
                    else if (HttpContext.Current.Session["LoginMessageInfo"] != null)
                    {
                        HttpContext.Current.Session.Remove("LoginMessageInfo");
                        HttpContext.Current.Session.Remove("LoginMessageInfoCount");
                    }
                }

            }
            IncludeLanguageJS();
            WishListURL = objHeaderSetting.WishListPageName;
            CountWishItems(aspxCommonObj);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private Hashtable hst = null;
    public void GetCartItemsCount()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder cartHeader = new StringBuilder();
        StringBuilder scriptExecute = new StringBuilder();
        string myCartLink = "";
        if (AllowAddToCart.ToLower() == "true")
        {
            myCartLink = ShoppingCartURL + pageExtension;
            cartHeader.Append("<a id=\"lnkMyCart\"");
            cartHeader.Append(" href=\"");
            cartHeader.Append(aspxRedirectPath);
            cartHeader.Append(myCartLink);
            cartHeader.Append("\"><i class='i-mycart'></i>");
            cartHeader.Append(getLocale("My Cart"));
            cartHeader.Append("<span class=\"cssClassTotalCount\"> [");
            cartHeader.Append(CartCount);
            cartHeader.Append("]</span>");
            cartHeader.Append("</a>");
            litCartHead.Text = cartHeader.ToString();
            if (CartCount == 0)
            {
                FrmLogin = false;
            }
        }
    }

    public void CountWishItems(AspxCommonInfo aspxCommonObj)
    {
        string pageExtension = SageFrameSettingKeys.PageExtension;


        string myWishlistLink = "";
        string loginLink = "";

        myWishlistLink = WishListURL + pageExtension;
        loginLink = LogInURL + pageExtension + "?ReturnUrl=" + aspxRedirectPath + myWishlistLink;


        string strWListLink = string.Empty;
        if (CustomerID > 0 && UserName.ToLower() != "anonymoususer")
        {
            strWListLink = " href=\"" + aspxRedirectPath + myWishlistLink + "\"";
        }
        else
        {
            strWListLink = " href=\"" + aspxRedirectPath + loginLink + "\"";
        }
        StringBuilder wishHeader = new StringBuilder();
        wishHeader.Append("<a id=\"lnkMyWishlist\"");
        wishHeader.Append(strWListLink);
        wishHeader.Append(">");
        wishHeader.Append("<i class='i-mywishlist'></i>");
        wishHeader.Append(getLocale("My Wishlist"));
        wishHeader.Append(" <span class=\"cssClassTotalCount\">[");
        wishHeader.Append(WishCount);
        wishHeader.Append("]</span>");
        wishHeader.Append("</a>");
        litWishHead.Text = wishHeader.ToString();
    }

    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
}
