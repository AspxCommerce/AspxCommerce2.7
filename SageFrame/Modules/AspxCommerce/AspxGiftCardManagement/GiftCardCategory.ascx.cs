using System;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Web.UI;
using SageFrame.Common;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SageFrame.Localization;
using System.Web;
using System.Web.UI.WebControls;

public partial class Modules_AspxCommerce_AspxGiftCardManagement_GiftCardCategory : BaseAdministrationUserControl
{
    public string aspxItemModulePath=string.Empty;
    public string UserModuleID;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            UserModuleID = SageUserModuleID;
            InitializeJs();
            AddLanguage();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
          aspxItemModulePath = ResolveUrl("~/Modules/AspxCommerce/AspxItemsManagement");

          IncludeCss("giftCardcategorycss", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/skins/tango/jcarousel-tango.css");
        IncludeJs("giftcardcategorymgt", "/js/FormValidation/jquery.validate.js", "/js/GridView/jquery.grid.js",
                    "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/DateTime/date.js",
                    "/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js", "/js/MessageBox/alertbox.js", "/js/AjaxFileUploader/ajaxupload.js", "/js/jquery.jcarousel.js");
        IncludeLanguageJS();
              
    }
    private void AddLanguage()
    {
        int PortalID = GetPortalID;
        List<Language> lstLanguage = LocalizationSqlDataProvider.GetPortalLanguages(PortalID);
        List<Language> lstLanguageFlags = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(PortalID), GetApplicationName));
        if (lstLanguage.Count < 1 || lstLanguageFlags.Count < 1)
        {
            languageSetting.Visible = false;
        }
        else
        {
            var query = from listlang in lstLanguage
                        join listflag in lstLanguageFlags
                             on listlang.LanguageCode equals listflag.LanguageCode
                        select new
                        {
                            listlang.LanguageCode,
                            listflag.FlagPath
                        };
            StringBuilder ddlLanguage = new StringBuilder();
            string cultureCode = GetCurrentCulture();
            ddlLanguage.Append("<ul id=\"languageSelect\" class=\"sfListmenu\"");
            ddlLanguage.Append(">");
            foreach (var item in query)
            {
                if (item.LanguageCode == cultureCode)
                {
                    ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\" class='languageSelected'>");
                    ddlLanguage.Append(item.LanguageCode + "-" + "<img src=\"" + item.FlagPath + "\">");
                    ddlLanguage.Append("</li>");
                }
                else
                {
                    ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\">");
                    ddlLanguage.Append(item.LanguageCode + "-" + "<img src=\"" + item.FlagPath + "\">");
                    ddlLanguage.Append("</li>");
                }
            }
            ddlLanguage.Append("</ul>");
            languageSetting.Text = ddlLanguage.ToString();
        }
    }

    private List<Language> AddFlagPath(List<Language> lstAvailableLocales, string baseURL)
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

    public string GetCurrentCultureInfo()
    {
        if (HttpContext.Current.Session != null)
        {
            string code = HttpContext.Current.Session[SessionKeys.SageUICulture] == null ? "en-US" : HttpContext.Current.Session[SessionKeys.SageUICulture].ToString();
            return code;
        }
        return "en-US";
    }

    private void InitializeJs()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    } 
}
