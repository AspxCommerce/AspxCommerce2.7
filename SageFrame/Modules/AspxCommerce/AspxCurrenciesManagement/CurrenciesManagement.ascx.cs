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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Web.Hosting;

public partial class Modules_AspxCommerce_AspxCurrenciesManagement_CurrenciesManagement : BaseAdministrationUserControl
{
    #region "Public Properties"
    public int StoreID;
    public int PortalID, maxFileSize;
    public string UserName;
    public string CultureName, UserModuleId;
    public static string StrRootPath
    {
        get
        {
            return HostingEnvironment.MapPath("~/").ToString();
        }

    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetAllCountry();
            BindCountryImage();
            Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));
            IncludeJs("CurrencyManage", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/MessageBox/jquery.easing.1.3.js",
                    "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxCurrenciesManagement/js/CurrencyManagement.js", "/js/AjaxFileUploader/ajaxupload.js", "/js/FancyDropDown/fancyDropDown.js", "/js/FormValidation/jquery.validate.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                     "/js/CurrencyFormat/jquery.formatCurrency.all.js");
            PortalID = GetPortalID;
            StoreID = GetStoreID;
            UserName = GetUsername;
            CultureName = GetCurrentCultureName;
            UserModuleId = SageUserModuleID;
            maxFileSize = Convert.ToInt32(StoreSetting.GetStoreSettingValueByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));

            IncludeCss("CurrencyManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css", "/Modules/AspxCommerce/AspxCurrenciesManagement/css/module.css");
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void GetAllCountry()
    {
        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
        if (lstCountry != null && lstCountry.Count > 0)
        {
            StringBuilder countryElements = new StringBuilder();
            countryElements.Append("<select id=\"ddlCountry\" class=\"sfListmenu\">");
            countryElements.Append("<option value=\"0\">" + "- Select one -" + "</option>");
            foreach (CountryInfo value in lstCountry)
            {
                countryElements.Append("<option name=\"Test\" value=");
                countryElements.Append(value.Value);
                countryElements.Append(">");
                countryElements.Append(value.Text);
                countryElements.Append("</option>");
            }
            countryElements.Append("</select>");
            ltrCountry.Text = countryElements.ToString();
        }
    }

    private void BindCountryImage()
    {
        StringBuilder options = new StringBuilder();

        string aspxRootPath = ResolveUrl("~/");
        string strFolder = Server.MapPath("~/images/flags/");
        string[] arrFiles = Directory.GetFiles(strFolder);
        options.Append("<select id=\"ddlCountryFlag\" class=\"makeMeFancy\">");
        foreach (string strFile in arrFiles)
        {
            options.Append("<option data-icon=\"");
            options.Append(aspxRootPath);
            options.Append("images/flags/");
            options.Append(Path.GetFileName(strFile));
            options.Append("\"  data-html-text=\"");
            options.Append(Path.GetFileName(strFile));
            options.Append("\" value=\"");
            string[] stringSeparators = new string[] { "." };
            string image = Path.GetFileName(strFile);
            string[] result = image.Split(stringSeparators, StringSplitOptions.None);
            options.Append(Path.GetFileName(result[0]));
            options.Append("\" >");
            options.Append(Path.GetFileName(strFile));
            options.Append("</option>");
        }
        options.Append("</select>");
        string script = "MakeFancyDropDown()";
        options.Append(GetStringScript(script));
        ltrCountryFlag.Text = options.ToString();
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

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));
        Page.ClientScript.RegisterClientScriptInclude("J11", ResolveUrl("~/Editors/ckeditor/ckeditor.js"));
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
    }

    private string GetStringScript(string codeToRun)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){ setTimeout(function(){ " +
                      codeToRun + "},500); });</script>");
        return script.ToString();
    }

}