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
using System.Web;
using System.Web.UI;
using SageFrame.Common;
using SageFrame.RolesManagement;
using SageFrame.Web;
using SageFrame.Security;
using SageFrame.Security.Entities;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Web.Security;
using SageFrame.Core;
using System.Text;
using AspxCommerce.RewardPoints;
using System.Collections;
using System.Web.Script.Serialization;

public partial class Modules_AspxUserDashBoard_UserDashBoard : BaseAdministrationUserControl
{
    public int storeID, portalID, customerID, userModuleIDUD;

    public string sessionCode = string.Empty, AllowAddToCart = string.Empty;

    public string cultureName, userName, userEmail, userFirstName, userLastName, userEmailWishList, userPicture;
    public string MyOrders = string.Empty;


    public string allowMultipleAddress, allowWishListMyAccount, RewardPointsGeneralSettingsIsActive = string.Empty;
    public string IsRewardInstl = string.Empty;
    public string userIP;
    public string countryName = string.Empty;
    public string aspxfilePath;
    public string CurrencyCodeSlected = string.Empty;
    public string ModulePath = string.Empty;

    public int addressId = 0;
    public bool defaultShippingExist = false;
    public bool defaultBillingExist = false;

    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            aspxfilePath = ResolveUrl(modulePath).Replace("AspxUserDashBoard", "AspxItemsManagement");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxUserDashBoardModulePath='" + ResolveUrl(modulePath) + "';", true);

            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            SageFrameConfig pagebase = new SageFrameConfig();
            string PortalLoginpage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage);//Ask santosh to get portal login page
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);

            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            base.GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
            aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

            RewardPointsGeneralSettingsIsActive = RewardPointsController.RewardPointsGeneralSettingsIsActive(aspxCommonObj).ToString();

            StoreSettingConfig ssc = new StoreSettingConfig();
            ssc.GetStoreSettingParamThree(StoreSetting.AllowUsersToCreateMultipleAddress,
                                        StoreSetting.MainCurrency,
                                        StoreSetting.ShowAddToCartButton,
                                        out allowMultipleAddress,
                                        out CurrencyCodeSlected,
                                        out AllowAddToCart,
                                        storeID,
                                        portalID,
                                        cultureName
                                        );

            IsRewardInstl = AspxCommonController.GetModuleInstallationInfo("AspxRewardPoints", aspxCommonObj).ToString();

            if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
            {
                if (!IsPostBack)
                {
                    string templateName = TemplateName;

                    IncludeCss("UserDashBoardCSS", "/Templates/" + templateName + "/css/GridView/tablesort.css", "/Templates/" + templateName + "/css/StarRating/jquery.rating.css", "/Templates/" + templateName + "/css/MessageBox/style.css",
                                "/Templates/" + templateName + "/css/PopUp/style.css", "/Templates/" + templateName + "/css/JQueryUIFront/jquery.ui.all.css", "/Templates/" + templateName + "/css/PasswordValidation/jquery.validate.password.css", "/Templates/" + templateName + "/css/ToolTip/tooltip.css");
                    IncludeJs("UserDashBoardJS", "/Modules/AspxCommerce/AspxWishList/js/WishItemList.js", "/js/jDownload/jquery.jdownload.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js",
                        "/js/MessageBox/alertbox.js", "/js/StarRating/jquery.MetaData.js", "/js/FormValidation/jquery.validate.js", "/js/PasswordValidation/jquery.validate.password.js",
                        "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/GridView/jquery.tablesorter.min.js", "/js/StarRating/jquery.rating.pack.js", "/js/encoder.js",
                        "/js/StarRating/jquery.rating.js", "/js/PopUp/custom.js", "/js/jquery.tipsy.js", "/Modules/AspxCommerce/AspxUserDashBoard/js/userdashboard.js", "/js/Paging/jquery.pagination.js", "/js/FormValidation/jquery.form-validation-and-hints.js");
                
                    userModuleIDUD = int.Parse(SageUserModuleID);
                    ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);

                    MembershipController m = new MembershipController();
                    UserInfo sageUser = m.GetUserDetails(GetPortalID, GetUsername);

                    AspxCommonController objUser = new AspxCommonController();
                    UsersInfo userDetails = objUser.GetUserDetails(aspxCommonObj);

                    if (userDetails.UserName != null)
                    {
                        userEmail = userDetails.Email;
                        userFirstName = userDetails.FirstName;
                        userLastName = userDetails.LastName;
                        userPicture = userDetails.ProfilePicture;
                        userEmailWishList = userEmail;//userDetail.Email;//added later for wishlist
                        userIP = HttpContext.Current.Request.UserHostAddress;
                        IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                        ipToCountry.GetCountry(userIP, out countryName);
                    }


                    BindUserDetails();
                }
                IncludeAllLanguageJS();
            }
            else
            {
                if (!IsParent)
                {
                    Response.Redirect(ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + PortalLoginpage) + ".aspx?ReturnUrl=" + Request.Url.ToString(), false);
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/" + PortalLoginpage) + ".aspx?ReturnUrl=" + Request.Url.ToString(), false);
                }
            }
            IncludeAllLanguageJS();

            GetUserRecentActivity(aspxCommonObj);
            GetAddressBookDetails(aspxCommonObj);
            GetAllCountry();

            GetMyOrders(aspxCommonObj);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetMyOrders(AspxCommonInfo aspxCommonObj)
    {
        MyOrders = new JavaScriptSerializer().Serialize(AspxUserDashController.GetMyOrderList(1, 10, aspxCommonObj));
    }

    private void BindUserDetails()
    {
        StringBuilder userImagePath = new StringBuilder();
        if (userPicture != "" && userPicture != null)
        {
            userImagePath.Append("Modules/Admin/UserManagement/UserPic/");
            userImagePath.Append(userPicture);
        }
        else
        {
            userPicture = "default-profile-pic.png";
            userImagePath.Append("Modules/Admin/UserManagement/UserPic/");
            userImagePath.Append(userPicture);
        }
        StringBuilder user = new StringBuilder();
        user.Append("<div class=\"cssProfileImage\">");
        user.Append("<img src=\"");
        user.Append(userImagePath);
        user.Append("\" alt=\"");
        user.Append(userName);
        user.Append("\" />");
        user.Append("</div><div class=\"cssUserName\">");
        user.Append(userName);
        user.Append(" </div><div class=\"cssUserEmail\">");
        user.Append(userEmail);
        user.Append("</div>");
        ltrUserDetails.Text = user.ToString();
    }


    Hashtable hst = null;
    public void GetAddressBookDetails(AspxCommonInfo aspxCommonObj)
    {

        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        List<AddressInfo> lstAddress = AspxUserDashController.GetUserAddressDetails(aspxCommonObj);
        StringBuilder defaultBillingAddressElements = new StringBuilder();
        StringBuilder defaultShippingAddressElements = new StringBuilder();
        if (lstAddress.Count > 0)
        {
            foreach (AddressInfo value in lstAddress)
            {
                if ((bool.Parse(value.DefaultBilling.ToString())) &&
                    (bool.Parse(value.DefaultShipping.ToString())))
                {
                    addressId = value.AddressID;
                }

                if (!defaultShippingExist)
                {
                    if ((bool.Parse(value.DefaultShipping.ToString())))
                    {
                        defaultShippingExist = true;
                    }
                    else
                    {
                        defaultShippingExist = false;
                    }
                }

                if (!defaultBillingExist)
                {
                    if (bool.Parse(value.DefaultBilling.ToString()))
                    {
                        defaultBillingExist = true;
                    }
                    else
                    {
                        defaultBillingExist = false;
                    }
                }

                if ((bool.Parse(value.DefaultBilling.ToString())) ||
                    ((bool.Parse(value.DefaultShipping.ToString()))))
                {
                    if (bool.Parse(value.DefaultShipping.ToString()))
                    {

                        defaultShippingAddressElements.Append("<div id=\"liDefaultShippingAddress\">");
                        defaultShippingAddressElements.Append("<h3>");
                        defaultShippingAddressElements.Append(getLocale("Default Shipping Address"));
                        defaultShippingAddressElements.Append("</h3>");
                        defaultShippingAddressElements.Append("<div><span name=\"FirstName\">");
                        defaultShippingAddressElements.Append(value.FirstName);
                        defaultShippingAddressElements.Append("</span>");
                        defaultShippingAddressElements.Append(" ");
                        defaultShippingAddressElements.Append("<span name=\"LastName\">");
                        defaultShippingAddressElements.Append(value.LastName);
                        defaultShippingAddressElements.Append("</span></div>");
                        defaultShippingAddressElements.Append("<div><span name=\"Email\">");
                        defaultShippingAddressElements.Append(value.Email);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Company != "")
                        {
                            defaultShippingAddressElements.Append("<div><span name=\"Company\">");
                            defaultShippingAddressElements.Append(value.Company);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("<div><span name=\"Address1\">");
                        defaultShippingAddressElements.Append(value.Address1);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Address2 != "")
                        {
                            defaultShippingAddressElements.Append("<div><span name=\"Address2\">");
                            defaultShippingAddressElements.Append(value.Address2);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("<div><span name=\"City\">");
                        defaultShippingAddressElements.Append(value.City);
                        defaultShippingAddressElements.Append("</span>, ");
                        defaultShippingAddressElements.Append("<span name=\"State\">");
                        defaultShippingAddressElements.Append(value.State);
                        defaultShippingAddressElements.Append("</span>, ");
                        defaultShippingAddressElements.Append("<span name=\"Country\">");
                        defaultShippingAddressElements.Append(value.Country);
                        defaultShippingAddressElements.Append("</span></div><div>");
                        defaultShippingAddressElements.Append("Zip: <span name=\"Zip\">");
                        defaultShippingAddressElements.Append(value.Zip);
                        defaultShippingAddressElements.Append("</span></div><div>");
                        defaultShippingAddressElements.Append("<i class=\"i-phone\"></i>");
                        defaultShippingAddressElements.Append("<span name=\"Phone\">");
                        defaultShippingAddressElements.Append(value.Phone);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Mobile != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<i class=\"i-mobile\"></i>");
                            defaultShippingAddressElements.Append("<span name=\"Mobile\">");
                            defaultShippingAddressElements.Append(value.Mobile);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        if (value.Fax != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<i class=\"i-fax\"></i>");
                            defaultShippingAddressElements.Append("<span name=\"Fax\">");
                            defaultShippingAddressElements.Append(value.Fax);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        if (value.Website != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<span name=\"Website\">");
                            defaultShippingAddressElements.Append(value.Website);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("</div>");
                        defaultShippingAddressElements.Append(
                            "<div class=\"cssClassChange\"><a href=\"#\" rel=\"popuprel\" name=\"EditAddress\" Flag=\"1\" value=\"");
                        defaultShippingAddressElements.Append(value.AddressID);
                        defaultShippingAddressElements.Append("\" Element=\"Shipping\">");
                        defaultShippingAddressElements.Append("<i class=\"icon-edit\"></i>");
                        defaultShippingAddressElements.Append("</a></div>");
                        ltrShipAddress.Text = defaultShippingAddressElements.ToString();
                    }

                    if (bool.Parse(value.DefaultBilling.ToString()))
                    {
                        defaultBillingAddressElements.Append("<div id=\"liDefaultBillingAddress\">");
                        defaultBillingAddressElements.Append("<h3>");
                        defaultBillingAddressElements.Append(getLocale("Default Billing Address"));
                        defaultBillingAddressElements.Append("</h3>");
                        defaultBillingAddressElements.Append("<div><span name=\"FirstName\">");
                        defaultBillingAddressElements.Append(value.FirstName);
                        defaultBillingAddressElements.Append("</span>");
                        defaultBillingAddressElements.Append(" ");
                        defaultBillingAddressElements.Append("<span name=\"LastName\">");
                        defaultBillingAddressElements.Append(value.LastName);
                        defaultBillingAddressElements.Append("</span></div>");
                        defaultBillingAddressElements.Append("<div><span name=\"Email\">");
                        defaultBillingAddressElements.Append(value.Email);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Company != "")
                        {
                            defaultBillingAddressElements.Append("<div><span name=\"Company\">");
                            defaultBillingAddressElements.Append(value.Company);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("<div><span name=\"Address1\">");
                        defaultBillingAddressElements.Append(value.Address1);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Address2 != "")
                        {
                            defaultBillingAddressElements.Append("<div><span name=\"Address2\">");
                            defaultBillingAddressElements.Append(value.Address2);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("<div><span name=\"City\">");
                        defaultBillingAddressElements.Append(value.City);
                        defaultBillingAddressElements.Append("</span>, ");
                        defaultBillingAddressElements.Append("<span name=\"State\">");
                        defaultBillingAddressElements.Append(value.State);
                        defaultBillingAddressElements.Append("</span>, ");
                        defaultBillingAddressElements.Append("<span name=\"Country\">");
                        defaultBillingAddressElements.Append(value.Country);
                        defaultBillingAddressElements.Append("</span></div><div>");
                        defaultBillingAddressElements.Append("Zip: <span name=\"Zip\">");
                        defaultBillingAddressElements.Append(value.Zip);
                        defaultBillingAddressElements.Append("</span></div><div>");
                        defaultBillingAddressElements.Append("<i class=\"i-phone\"></i>");
                        defaultBillingAddressElements.Append("<span name=\"Phone\">");
                        defaultBillingAddressElements.Append(value.Phone);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Mobile != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<i class=\"i-mobile\"></i>");
                            defaultBillingAddressElements.Append("<span name=\"Mobile\">");
                            defaultBillingAddressElements.Append(value.Mobile);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        if (value.Fax != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<i class=\"i-fax\"></i>");
                            defaultBillingAddressElements.Append("<span name=\"Fax\">");
                            defaultBillingAddressElements.Append(value.Fax);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        if (value.Website != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<span name=\"Website\">");
                            defaultBillingAddressElements.Append(value.Website);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("</div>");
                        defaultBillingAddressElements.Append(
                            "<div class=\"cssClassChange\"><a href=\"#\" rel=\"popuprel\" name=\"EditAddress\" Flag=\"1\" value=\"");
                        defaultBillingAddressElements.Append(value.AddressID);
                        defaultBillingAddressElements.Append("\" Element=\"Billing\">");
                        defaultBillingAddressElements.Append("<i class=\"icon-edit\"></i>");
                        defaultBillingAddressElements.Append("</a></div>");
                        ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
                    }
                }
            }
            if (!defaultShippingExist)
            {
                defaultShippingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Shipping Adresss Yet!</span>");
                ltrShipAddress.Text = defaultShippingAddressElements.ToString();
            }
            if (!defaultBillingExist)
            {
                defaultBillingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Billing Adresss Yet!</span>");
                ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
            }
        }
        else
        {
            defaultBillingAddressElements.Append("<div id=\"liDefaultBillingAddress\"></div>");
            ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
            defaultShippingAddressElements.Append("<div id=\"liDefaultShippingAddress\"></div>");
            ltrShipAddress.Text = defaultShippingAddressElements.ToString();
        }
    }
    private void GetUserRecentActivity(AspxCommonInfo aspxCommonObj)
    {
        string ShoppingCartURL = string.Empty;
        
        string myCartLink = string.Empty;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder cartHeader = new StringBuilder();
        StringBuilder scriptExecute = new StringBuilder();
        int cartCount = AspxHeaderController.GetCartItemsCount(aspxCommonObj);
        int wishCount = AspxHeaderController.CountWishItems(aspxCommonObj);

        StoreSettingConfig ssc = new StoreSettingConfig();
        ShoppingCartURL = ssc.GetStoreSettingsByKey(StoreSetting.ShoppingCartURL, GetStoreID, GetPortalID, GetCurrentCultureName);
        string WishlistURL = "/My-WishList.aspx";
        SageFrameConfig pagebase = new SageFrameConfig();
        bool IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
        StringBuilder recentActivity = new StringBuilder();
        recentActivity.Append("<h2>");
        recentActivity.Append(getLocale("Recent Activities"));
        recentActivity.Append("</h2>");
        recentActivity.Append("<li>");
        recentActivity.Append("<input type=\"hidden\" name=\"dashitemwishmenu\" />");
        recentActivity.Append("<li id='dashMyWishlist'>Your Wishlist Contains:<a href='");
        recentActivity.Append(WishlistURL);
        recentActivity.Append("'> <span class='cssClassTotalCount'>");
        recentActivity.Append(wishCount);
        recentActivity.Append(" items</span></a></li>");
        recentActivity.Append("</li>");

        if (AllowAddToCart.ToLower() == "true")
        {
            myCartLink = ShoppingCartURL + pageExtension;
            recentActivity.Append("<li>");
            recentActivity.Append(getLocale("Your Carts Contains:"));
            recentActivity.Append("<a href=\"");
            recentActivity.Append(myCartLink);
            recentActivity.Append("\"> ");
            recentActivity.Append(cartCount);
            recentActivity.Append(" ");
            recentActivity.Append("items");
            recentActivity.Append("</a>");
            recentActivity.Append("</li>");
        }

        ltrRecentActivity.Text = recentActivity.ToString();

    }

    public void GetAllCountry()
    {
        string pageExtension = SageFrameSettingKeys.PageExtension;
        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
        if (lstCountry != null && lstCountry.Count > 0)
        {
            StringBuilder countryElements = new StringBuilder();
            countryElements.Append("<select id=\"ddlCountry\" class=\"sfListmenu\">");
            foreach (CountryInfo value in lstCountry)
            {
                countryElements.Append("<option value=");
                countryElements.Append(value.Value);
                countryElements.Append(">");
                countryElements.Append(value.Text);
                countryElements.Append("</option>");
            }
            countryElements.Append("</select>");
            ltrCountry.Text = countryElements.ToString();

        }
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
