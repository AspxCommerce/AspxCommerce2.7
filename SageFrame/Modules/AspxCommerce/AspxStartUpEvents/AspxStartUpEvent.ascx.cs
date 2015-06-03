using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SageFrame.Common;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Data;
using SageFrame.Shared;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using SageFrame.Web.Utilities;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using ServiceInvoker;

public partial class Modules_AspxCommerce_AspxStartUpEvents_AspxStartUpEvent : SageFrameStartUpControl
{
    string sageRedirectPath = string.Empty;
    string sageNavigateUrl = string.Empty;
    public string LoginPage = string.Empty;
    public string PageExtension, AllowRealTimeNotifications;
    public string SendEcommerceEmailsFrom = string.Empty;
    SageFrameConfig sfConfig = new SageFrameConfig();
    AspxStartUpController ctl = new AspxStartUpController();

    protected void Page_Init(object sender, EventArgs e)
    {
        Session[SessionKeys.SageFrame_StoreID] = GetPortalID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int StoreID, PortalID;
        int CustomerID;
        string UserName, CultureName, SessionCode;
        SetCustomerID();
        GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, 
            out CultureName, out SessionCode);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, 
            CultureName, CustomerID, SessionCode);

        StoreSettingConfig ssc = new StoreSettingConfig();
        string timeToDeleteCartItems, timeToAbandonCart;
        ssc.GetStoreSettingParamTwo(StoreSetting.TimeToDeleteAbandonedCart, StoreSetting.CartAbandonedTime,
            out timeToDeleteCartItems, out timeToAbandonCart,
            StoreID, PortalID, CultureName);

        ctl.DeleteAbandonedCartItems(StoreID, PortalID, decimal.Parse(timeToDeleteCartItems), 
            decimal.Parse(timeToAbandonCart));

        StoreAccessDetailsInfo storeAccessTracker = new StoreAccessDetailsInfo();
        storeAccessTracker.PortalID = PortalID;
        storeAccessTracker.StoreID =StoreID;
        storeAccessTracker.Username = UserName;

        StartUpInfoCollection objStartInfo = new StartUpInfoCollection();
        AspxCommonController objCommonCont = new AspxCommonController();
        objStartInfo = objCommonCont.GetStartUpInformation("AspxKPI", "AspxABTesting", storeAccessTracker);

        CheckStoreAccessible(aspxCommonObj, objStartInfo.IsStoreAccess,objStartInfo.IsStoreClosed);
        CreateGlobalVariables(aspxCommonObj,objStartInfo.IsKPIInstalled,objStartInfo.IsABTestInstalled);
        IncludeCoreLanguageJS();
        IncludeTemplateLanguageJS();
        IncludeRssFeedLanguageJS();
        if (HttpContext.Current.Session["IsLoginClick" + UserName] != null)
        {
            if (bool.Parse(HttpContext.Current.Session["IsLoginClick" + UserName].ToString()))
            {

                objCommonCont.UpdateCartAnonymoususertoRegistered(StoreID, PortalID, CustomerID, SessionCode);
                HttpContext.Current.Session["IsLoginClick" + UserName] = false;
            }
        }

        List<CurrrencyRateInfo> ratelist = GetCountryCodeRates(aspxCommonObj);
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        string jsonRates = json_serializer.Serialize(ratelist);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ratelist", " var currencyRate='" + jsonRates + "';", true);

    }
    private void CheckStoreAccessible(AspxCommonInfo aspxCommonObj,bool isStoreAccess,bool isStoreClosed)
    {
        //PortalAPI
        SageFrameConfig sfConfig = new SageFrameConfig();
        LoginPage = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage);
        PageExtension = SageFrameSettingKeys.PageExtension;
        #region "StoreAccessLogic"
        if (!Request.Url.AbsoluteUri.Contains(LoginPage))
        {
            if (!isStoreClosed)
            {
                if (!isStoreAccess)
                {
                    string blockedPortalUrl = string.Empty;
                    if (!IsParent)
                    {
                        blockedPortalUrl = PortalAPI.DefaultPageURL;                  
                    }
                    else
                    {
                        blockedPortalUrl = PortalAPI.DefaultPageURL;
                    }
                    Session["StoreBlocked"] = blockedPortalUrl;
                    HttpContext.Current.Response.Redirect(
                        ResolveUrl("~/Modules/AspxCommerce/Store-Not-Accessed" + PageExtension));
                }
            }
            else
            {
                string closePortalUrl = string.Empty;
                if (!IsParent)
                {
                    closePortalUrl = PortalAPI.DefaultPageURL;
                }
                else
                {


                    closePortalUrl = PortalAPI.DefaultPageURL;
                }
                Session["StoreClosed"] = closePortalUrl;
                HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/AspxCommerce/Store-Closed" + PageExtension));
            }
        }
        #endregion
    }

    private void CreateGlobalVariables(AspxCommonInfo aspxCommonObj,bool isKPIInstalled,bool isABTestInstalled)
    {
        
        Page.ClientScript.RegisterClientScriptInclude("AspxCommereCore",
            ResolveUrl("~/js/SageFrameCorejs/aspxcommercecormin.js"));
        if (isABTestInstalled)
        {
            IncludeJs("AspxABTesting", "/Modules/AspxCommerce/AspxABTesting/js/ABTest.js", 
                "/Modules/AspxCommerce/AspxABTesting/Language/AspxABTesting.js");
        }
        if (isKPIInstalled)
        {
            IncludeJs("AspxKPI", "/Modules/AspxCommerce/AspxKPI/js/KPICommon.js", 
                "/Modules/AspxCommerce/AspxKPI/Language/AspxKPILanguage.js");
        }
        IncludeJs("StartUpJs", "/js/CurrencyFormat/jquery.currencies.js", "/js/jquery.masonry.js", 
            "/js/Templating/tmpl.js");

        IncludeCss("ui", "/js/jquery-ui-1.8.14.custom/css/redmond/jquery-ui-1.8.16.custom.css");
        PageExtension = SageFrameSettingKeys.PageExtension;

        string userIP = HttpContext.Current.Request.UserHostAddress;
        string countryName = "";
        IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
        ipToCountry.GetCountry(userIP, out countryName);
        StoreSettingConfig ssc = new StoreSettingConfig();
        string myCartURL, currencyCode, sortByOptions, 
            sortByOptionsDefault, viewAsOptions, viewAsOptionsDefault;

        ssc.GetStoreSettingParamSeven(StoreSetting.ShoppingCartURL, StoreSetting.MainCurrency, StoreSetting.SendEcommerceEmailsFrom, 
            StoreSetting.SortByOptions, StoreSetting.SortByOptionsDefault,
            StoreSetting.ViewAsOptions, StoreSetting.ViewAsOptionsDefault, 
            out myCartURL, out currencyCode, out SendEcommerceEmailsFrom, 
            out sortByOptions, out sortByOptionsDefault,
          out viewAsOptions, out viewAsOptionsDefault, aspxCommonObj.StoreID,
          aspxCommonObj.PortalID, aspxCommonObj.CultureName);

        Session["SendEcommerceEmailsFrom"] = SendEcommerceEmailsFrom;
        
        string resolveUrl = ResolveUrl("~/");
        if (!IsParent)
        {
            sageRedirectPath = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/");
            sageNavigateUrl = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + PortalAPI.DefaultPageURL + PageExtension);
        }
        else
        {
            sageRedirectPath = resolveUrl;
            sageNavigateUrl = ResolveUrl("~/" + PortalAPI.DefaultPageURL + PageExtension);
        }


        StringBuilder strGlobalVar = new StringBuilder();
        
        strGlobalVar.Append("var aspxservicePath='");
        strGlobalVar.Append(resolveUrl);
        strGlobalVar.Append("Modules/AspxCommerce/AspxCommerceServices/");
        strGlobalVar.Append("';");
        strGlobalVar.Append("var aspxRootPath='");
        strGlobalVar.Append(resolveUrl);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var aspxTemplateFolderPath='");
        strGlobalVar.Append(resolveUrl);
        strGlobalVar.Append("Templates/");
        strGlobalVar.Append(TemplateName);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var pageExtension='");
        strGlobalVar.Append(PageExtension);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var templateName='");
        strGlobalVar.Append(TemplateName);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var customerID='");
        strGlobalVar.Append(aspxCommonObj.CustomerID);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var IsUseFriendlyUrls='");
        strGlobalVar.Append(true);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var sessionCode='");
        strGlobalVar.Append(aspxCommonObj.SessionCode);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var clientIPAddress='");
        strGlobalVar.Append(HttpContext.Current.Request.UserHostAddress);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var sortByOptions='");
        strGlobalVar.Append(sortByOptions);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var sortByOptionsDefault='");
        strGlobalVar.Append(sortByOptionsDefault);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var viewAsOptions='");
        strGlobalVar.Append(viewAsOptions);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var viewAsOptionsDefault='");
        strGlobalVar.Append(viewAsOptionsDefault);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var BaseCurrency='");
        strGlobalVar.Append(currencyCode);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var myCartURL='");
        strGlobalVar.Append(myCartURL);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var homeURL='");
        strGlobalVar.Append(sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
        strGlobalVar.Append("';");
        strGlobalVar.Append("var LogInURL='");
        strGlobalVar.Append(LoginPage);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var itemImagePath='Modules/AspxCommerce/AspxItemsManagement/uploads/';");
        strGlobalVar.Append("var categoryImagePath='Modules/AspxCommerce/AspxCategoryManagement/uploads/';");
        strGlobalVar.Append("var section='");
        strGlobalVar.Append(0);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var aspxRedirectPath='");
        strGlobalVar.Append(sageRedirectPath);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var IsKPIInstalled='");
        strGlobalVar.Append(isKPIInstalled);
        strGlobalVar.Append("';");
        strGlobalVar.Append("var IsABTestInstalled='");
        strGlobalVar.Append(isABTestInstalled);
        strGlobalVar.Append("';");
        strGlobalVar.Append(" var aspxCountryName='");
        strGlobalVar.Append(countryName);
        strGlobalVar.Append("';");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), 
            "aspxGlobalVariables", strGlobalVar.ToString(), true);
    }

    private void SetCustomerID()
    {
        int customerID = 0;
        SecurityPolicy objSecurity = new SecurityPolicy();
        FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
        if (ticket != null)
        {
            CustomerGeneralInfo sageUserCust = CustomerGeneralInfoController.CustomerIDGetByUsername(ticket.Name, GetStoreID, GetPortalID);
            if (sageUserCust != null)
            {
                customerID = sageUserCust.CustomerID;
            }
            Session[SessionKeys.SageFrame_CustomerID] = customerID;
        }
    }
    private List<CurrrencyRateInfo> GetCountryCodeRates(AspxCommonInfo aspxCommonObj)
    {
        Hashtable hst = new Hashtable();
        StoreSettingProvider sep = new StoreSettingProvider();
        if (HttpContext.Current.Cache["AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString()] != null)
        {
            hst = (Hashtable)HttpContext.Current.Cache["AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString()];

            List<CurrrencyRateInfo> currencies = (List<CurrrencyRateInfo>)hst["CURRENCY"];
            return currencies;
        }
        else
        {
            List<CurrrencyRateInfo> currencyRate = AspxCurrencyController.GetCountryCodeRates(aspxCommonObj);
            if (currencyRate.Count > 0)
            {
                hst.Add("CURRENCY", currencyRate);
                HttpContext.Current.Cache.Insert("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString(), hst);
            }
            
            return currencyRate;
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
                    if (!File.Exists(Server.MapPath("~"+FileUrl)))
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
    public void IncludeLanguageAPIJS()
    {
        try
        {

            string strScript = string.Empty;
            string langFolder = "~/Modules/AspxCommerce/AspxAPIJs/Language/";
            string modulePath = "/Modules/AspxCommerce/AspxAPIJs/" + "Language/";
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
                }
                string inputString = string.Empty;

                if (!File.Exists(Server.MapPath("~"+FileUrl)))
                {
                    FileUrl = modulePath + languageFile + ".js";
                }
                IncludeJsTop(modulePath, FileUrl);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void IncludeTemplateLanguageJS()
    {
        try
        {
            string strScript = string.Empty;
            string langFolder = "~/Modules/AspxCommerce/AspxTemplate/" + "Language/";
            string modulePath = "/Modules/AspxCommerce/AspxTemplate/" + "Language/";
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
                    if (!File.Exists(Server.MapPath("~"+FileUrl)))
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
                    if (!File.Exists(Server.MapPath("~"+FileUrl)))
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