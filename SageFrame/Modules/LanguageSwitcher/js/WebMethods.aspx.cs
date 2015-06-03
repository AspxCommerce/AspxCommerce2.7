using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using SageFrame.Localization;
using System.Data;
using SageFrame.Web.Utilities;
using SageFrame.Core;
using SageFrame.Web;
using SageFrame.Framework;
using System.Threading;
using SageFrame.Common;

[ScriptService]
public partial class Modules_LanguageSwitcher_js_WebMethods : PageBase
{    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    [WebMethod(true)]
    public static string GetSession()
    {
        return(HttpContext.Current.Session["LanguageCode"]==null?"en-US":HttpContext.Current.Session["LanguageCode"].ToString());
        
    }
    [WebMethod()]
    public static List<Language> GetCountryList()
    {
       List<Language> lstLanguage=LocaleController.GetCultures();
       foreach (Language obj in lstLanguage)
       {
           obj.LanguageCode = obj.LanguageCode.Substring(0, obj.LanguageCode.IndexOf("-"));
       }

       return lstLanguage;
    }
    [WebMethod]
    public static string GetLanguageName()
    {
        return (LocaleController.GetLanguageNameOnly(HttpContext.Current.Session["LanguageCode"].ToString()));
    }
    [WebMethod]
    public static void AddLanguageSwitchSettings(string SwitchType,bool ListTypeFlags,bool ListTypeName,bool ListTypeBoth,string ListAlign,bool EnableCarousel,string DropDownType,int PortalID,int UserModuleID)
    {
        List<LanguageSwitchKeyValue> lstSettings = new List<LanguageSwitchKeyValue>();
        lstSettings.Add(new LanguageSwitchKeyValue("SwitchType", SwitchType.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("ListTypeFlag", ListTypeFlags.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("ListTypeName", ListTypeName.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("ListTypeBoth", ListTypeBoth.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("ListAlign", ListAlign.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("EnableCarousel", EnableCarousel.ToString()));
        lstSettings.Add(new LanguageSwitchKeyValue("DropDownType", DropDownType.ToString()));
        foreach (LanguageSwitchKeyValue obj in lstSettings)
        {
            obj.AddedBy="superuser";
            obj.IsActive=true;
        }
        try
        {
            LocaleController.AddLanguageSwitchSettings(lstSettings,UserModuleID,PortalID);
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    [WebMethod]
    public static List<Language> GetAvailableLanguages(string baseURL,int PortalID)
    {
        List<Language> lstLanguage = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(PortalID), baseURL));
        return lstLanguage;
    }
    protected static List<Language> AddFlagPath(List<Language> lstAvailableLocales,string baseURL)
    {
        List<Language> filtered = new List<Language> { };
       string currentCulture = GetCurrentCultureInfo();
       foreach (Language li in lstAvailableLocales)
       {
           //if (li.LanguageCode != currentCulture)
           //{
               filtered.Add(li);
           //}
       }

        filtered.ForEach(
            delegate(Language obj)
            {
                obj.FlagPath = baseURL + "/images/flags/" + obj.LanguageCode.Substring(obj.LanguageCode.IndexOf("-") + 1).ToLower() + ".png";
            }
            );
        return filtered;
    }
    
    [WebMethod]
    public static List<LanguageSwitchKeyValue> GetLanguageSettings(int PortalID, int UserModuleID)
    {
        return (LocaleController.GetLanguageSwitchSettings(PortalID,UserModuleID));
    }
    [WebMethod]
    public static void SetCultureInfo(string CultureCode)
    {
       AppErazer.ClearSysCache();
        PageBase.SetCultureInfo(CultureCode, CultureCode);

        HttpContext.Current.Session["IsCultureChange"] = 1;
            
    }

    [WebMethod]
    public static string GetCurrentCultureInfo()
    {
        if (HttpContext.Current.Session != null)
        {
            string code = HttpContext.Current.Session[SessionKeys.SageUICulture] == null ? "en-US" : HttpContext.Current.Session[SessionKeys.SageUICulture].ToString();
            return code;
        }
        return "en-US";
    }
}
