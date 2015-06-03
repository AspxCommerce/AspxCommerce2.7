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

public partial class Modules_AspxCommerce_AspxBrandManagement_BrandManage : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID, maxFileSize;
    public string UserName;
    public string CultureName, UserModuleId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                PortalID = GetPortalID;
                StoreID = GetStoreID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                UserModuleId = SageUserModuleID;
                Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));                
                IncludeJs("BrandManage", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/MessageBox/jquery.easing.1.3.js",
                    "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxBrandManagement/js/BrandManage.js", "/js/AjaxFileUploader/ajaxupload.js");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
                IncludeJs("BrandManageCk", "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js");
                IncludeCss("BrandManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css",
                        "/Modules/AspxCommerce/AspxBrandManagement/css/module.css");
                maxFileSize = Convert.ToInt32(StoreSetting.GetStoreSettingValueByKey(StoreSetting.MaximumImageSize, GetStoreID, GetPortalID, GetCurrentCultureName));
            }
           
            AddLanguage();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void AddLanguage()
    {
        List<Language> lstLanguage = LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID);
        List<Language> lstLanguageFlags = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID), GetApplicationName));
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
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
            Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));        
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
    }
}
