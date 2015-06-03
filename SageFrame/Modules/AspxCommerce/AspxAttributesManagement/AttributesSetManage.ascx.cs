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

public partial class Modules_AspxAttributesManagement_AttributesSetManage : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID;
    public string UserName;
    public string CultureName, UserModuleId;
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

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("AttributeSetManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/TreeView/ui.tree.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/PopUp/style.css", "/Modules/AspxCommerce/AspxAttributesManagement/css/module.css");
                IncludeJs("AttributeSetManage", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                           "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/TreeView/jquery.tree.ui.core.js", "/js/TreeView/ui.tree.js",
                           "/js/TreeView/contextmenu.js","/js/PopUp/custom.js", "/js/FormValidation/jquery.validate.js","/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxAttributesManagement/js/AttributesSetManage.js");

                AddImageUrls();                
                PortalID = int.Parse(GetPortalID.ToString());
                StoreID = int.Parse(GetStoreID.ToString());
                UserName = GetUsername;
                CultureName = GetCurrentCultureName; UserModuleId = SageUserModuleID;
            }
            AddLanguage();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void AddImageUrls()
    {
        string imageFolder = "~/Templates/" + TemplateName + "/images/";
        imgRename.Src = GetImageUrl(imageFolder, "context_edit.png", true);
        imgDelete.Src = GetImageUrl(imageFolder, "context_delete.png", true);
        imgRemove.Src = GetImageUrl(imageFolder, "context_delete.png", true);
    }

    public string GetImageUrl(string _imageFolder, string imageName, bool isServerControl)
    {
        string path = string.Empty;
        if (isServerControl == true)
        {
            path = _imageFolder + imageName;
        }
        return path;
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
}
