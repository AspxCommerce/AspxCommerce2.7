using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SageFrame.Common;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Data;
using SageFrame.Shared;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using ServiceInvoker;

public partial class Modules_AspxCommerce_AspxStartUpEvents_AspxStartUpAdminEvents : SageFrameStartUpControl
{
    string sageRedirectPath = string.Empty;
    string sageNavigateUrl = string.Empty;
    public string LogInURL = string.Empty;
    bool IsUseFriendlyUrls = true;
    public string PageExtension, AllowRealTimeNotifications;
    SageFrameConfig sfConfig = new SageFrameConfig();
    AspxStartUpController ctl = new AspxStartUpController();

    protected void Page_Init(object sender, EventArgs e)
    {
        Session[SessionKeys.SageFrame_StoreID] = GetPortalID;     
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        SetCustomerID();
        GetStoreSettings();
        LoadGlobalVariables();
        IncludeCoreLanguageJS();
        IncludeRssFeedLanguageJS();
        StoreSettingConfig ssc = new StoreSettingConfig();
        AllowRealTimeNotifications = ssc.GetStoreSettingsByKey(StoreSetting.AllowRealTimeNotifications, GetStoreID, GetPortalID, GetCurrentCultureName);
        if (AllowRealTimeNotifications.ToLower() == "true")
        {
            IncludeJs("SignalR", "/js/SignalR/jquery.signalR-2.2.0.min.js", "/signalr/hubs", "/Modules/AspxCommerce/AspxStartUpEvents/js/RealTimeAspxMgmt.js");
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ratelist", " var currencyRate='[]';", true);
    }

    public void LoadGlobalVariables()
    {
        Page.ClientScript.RegisterClientScriptInclude("AspxCommereCore", ResolveUrl("~/js/SageFrameCorejs/aspxcommercecormin.js"));
        IncludeJs("Session", "/js/Session.js");
        IncludeJs("Encoder", "/js/encoder.js");
        bool isIE = HttpContext.Current.Request.Browser.Type.ToUpper().Contains("IE");
        if (isIE)
        {
            IncludeJs("Base64", "/js/Base64/base64.js");
        }
        IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);       
        PageExtension = SageFrameSettingKeys.PageExtension;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRootPath", " var aspxRootPath='" + ResolveUrl("~/") + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalTemplateFolderPath", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "pageExtension", " var pageExtension='" + PageExtension + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "storeID", " var storeID='" + GetStoreID + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "portalID", " var portalID='" + GetPortalID + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "culturename", " var cultureName='" + GetCurrentCultureName + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "templatename", " var templateName='" + TemplateName + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "customerid", " var customerID='" + GetCustomerID + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "username", " var userName='" + GetUsername + "';", true);
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isfriendlyurl", " var IsUseFriendlyUrls='" + IsUseFriendlyUrls + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sessioncode", " var sessionCode='" + HttpContext.Current.Session.SessionID.ToString() + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientIPAddress", " var clientIPAddress='" + HttpContext.Current.Request.UserHostAddress + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoginURL", " var LoginURL='" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "itemImagePath", " var itemImagePath='Modules/AspxCommerce/AspxItemsManagement/uploads/';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "categoryImagePath", " var categoryImagePath='Modules/AspxCommerce/AspxCategoryManagement/uploads/';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "section", " var section='" + 1 + "';", true);
        string userIP = HttpContext.Current.Request.UserHostAddress;
        string countryName = "";
        IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
        ipToCountry.GetCountry(userIP, out countryName);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientCountry", " var aspxCountryName='" + countryName + "';", true);
        StoreSettingConfig ssc = new StoreSettingConfig();
        string currencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);
        if (currencyCode != null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "region",
                                                    " var region='" +
                                                    StoreSetting.GetRegionFromCurrencyCode(currencyCode, GetStoreID,
                                                                                           GetPortalID) + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "curSymbol",
                                                    " var curSymbol='" +
                                                    StoreSetting.GetSymbolFromCurrencyCode(currencyCode, GetStoreID,
                                                                                           GetPortalID) + "';", true);
        }
        if (HttpContext.Current.Session["CurrencyRate"] == null)
        {
            HttpContext.Current.Session["CurrencyRate"] = 1;
        }
        if (HttpContext.Current.Session["CurrencyCode"] == null)
        {
            HttpContext.Current.Session["CurrencyCode"] = currencyCode;
        }
        if (IsUseFriendlyUrls)
        {
            if (!IsParent)
            {
                sageRedirectPath = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/");
                sageNavigateUrl = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + PageExtension);
            }
            else
            {
                sageRedirectPath = ResolveUrl("~/");
                sageNavigateUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + PageExtension);
            }
        }
        else
        {
            sageRedirectPath = ResolveUrl("{~/Default" + PageExtension + "?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=");
            sageNavigateUrl = ResolveUrl("~/Default" + PageExtension + "?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRedirectPath", " var aspxRedirectPath='" + sageRedirectPath + "';", true);

    }

    private void SetCustomerID()
    {
        SageUserControl suc = new SageUserControl();
        int customerID = 0;
        SecurityPolicy objSecurity = new SecurityPolicy();
        FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
        if (ticket != null)
        {
            CustomerGeneralInfo sageUserCust = CustomerGeneralInfoController.CustomerIDGetByUsername(ticket.Name,
                                                                                                     GetPortalID,
                                                                                                     GetStoreID);
            if (sageUserCust != null)
            {
                customerID = sageUserCust.CustomerID;
            }
            Session[SessionKeys.SageFrame_CustomerID] = customerID;
            suc.SetCustomerID(customerID);
            SetCustomerID(customerID);
        }
    }

    private void GetStoreSettings()
    {
        Hashtable hst = new Hashtable();
        StoreSettingProvider sep = new StoreSettingProvider();
        if (HttpContext.Current.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()] != null)
        {
            hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()];
            StoreSettingConfig ssc = new StoreSettingConfig();
            decimal timeToDeleteCartItems = decimal.Parse(ssc.GetStoreSettingsByKey(StoreSetting.TimeToDeleteAbandonedCart, GetStoreID, GetPortalID, GetCurrentCultureName));
            decimal timeToAbandonCart = Convert.ToDecimal(ssc.GetStoreSettingsByKey(StoreSetting.CartAbandonedTime, GetStoreID, GetPortalID, GetCurrentCultureName));
            ctl.DeleteAbandonedCartItems(GetStoreID, GetPortalID, timeToDeleteCartItems, timeToAbandonCart);
        }
        else
        {
            DataTable dt = sep.GetStoreSettings(GetStoreID, GetPortalID, GetCurrentCultureName);            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                    StoreSettingConfig ssc = new StoreSettingConfig();
                    decimal timeToDeleteCartItems = decimal.Parse(ssc.GetStoreSettingsByKey(StoreSetting.TimeToDeleteAbandonedCart, GetStoreID, GetPortalID, GetCurrentCultureName));
                    decimal timeToAbandonCart = Convert.ToDecimal(ssc.GetStoreSettingsByKey(StoreSetting.CartAbandonedTime, GetStoreID, GetPortalID, GetCurrentCultureName));
                    ctl.DeleteAbandonedCartItems(GetStoreID, GetPortalID, timeToDeleteCartItems, timeToAbandonCart);
                }
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString(), hst);
            }
        }
    }

    public void IncludeCoreLanguageJS()
    {
        try
        {
            string strScript = string.Empty;
            string langFolder = "~/Modules/AspxCommerce/CoreLanguage/" + "Language/";
            string modulePath = "/Modules/AspxCommerce/CoreLanguage/" + "Language/";
            if (Directory.Exists(Server.MapPath(langFolder)))
            {
                bool isTrue = false;
                string[] fileList = Directory.GetFiles(Server.MapPath(langFolder));

                string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";

                Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                Match match = regex.Match(fileList[0]);
                string languageFile = match.Groups[2].Value;
                string FileUrl = string.Empty;
                isTrue = GetCurrentCulture() == "en-US" ? true : false;
                if (isTrue)
                {
                    FileUrl = modulePath + languageFile + ".js";
                }
                else
                {
                    FileUrl = modulePath + languageFile + "." + GetCurrentCulture() + ".js";
                    if (!File.Exists(Server.MapPath("~" + FileUrl)))
                    {
                        FileUrl = modulePath + languageFile + ".js";
                    }
                }
                IncludeJsTop(modulePath, FileUrl);


            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public void IncludeRssFeedLanguageJS()
    {
        try
        {
            string strScript = string.Empty;
            string langFolder = "~/Modules/AspxCommerce/AspxRssFeed/" + "Language/";
            string modulePath = "/Modules/AspxCommerce/AspxRssFeed/" + "Language/";
            if (Directory.Exists(Server.MapPath(langFolder)))
            {
                bool isTrue = false;
                string[] fileList = Directory.GetFiles(Server.MapPath(langFolder));

                string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";

                Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);

                Match match = regex.Match(fileList[0]);
                string languageFile = match.Groups[2].Value;
                string FileUrl = string.Empty;
                isTrue = GetCurrentCulture() == "en-US" ? true : false;
                if (isTrue)
                {
                    FileUrl = modulePath + languageFile + ".js";
                }
                else
                {
                    FileUrl = modulePath + languageFile + "." + GetCurrentCulture() + ".js";
                    if (!File.Exists(Server.MapPath("~" + FileUrl)))
                    {
                        FileUrl = modulePath + languageFile + ".js";
                    }
                }
                IncludeJsTop(modulePath, FileUrl);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
