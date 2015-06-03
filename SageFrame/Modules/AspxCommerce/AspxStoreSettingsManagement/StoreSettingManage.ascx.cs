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
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Core;
using SageFrame.Web;
using SageFrame.Pages;
using System.Collections.Specialized;
using SageFrame.Framework;
using AspxCommerce.Core;
using System.Collections.Generic;

public partial class Modules_AspxStoreSettings_StoreSettingManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID,MaxFileSize;
    public string UserName, CultureName;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRootPath", " var aspxRootPath='" + ResolveUrl("~/") + "';", true);                    
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxStoreSetModulePath='" + ResolveUrl(modulePath) + "';", true);
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
                IncludeCss("StoreSettingManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css");
                IncludeJs("StoreSettingManage", "/js/FormValidation/jquery.validate.js", "/js/AjaxFileUploader/ajaxupload.js",
                           "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                            "Modules/AspxCommerce/AspxStoreSettingsManagement/js/StoreSetting.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                UserModuleID = SageUserModuleID;
				BindPageDropDown();

                StoreSettingConfig ssc = new StoreSettingConfig();
                MaxFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void BindPageDropDown()
    {
        try
        {
            List<PageEntity> LINQParentPages = StoreSetting.GetActivePortalPages(GetPortalID, GetUsername, "---", true, false, DBNull.Value, DBNull.Value);

            ddlMyAccountURL.Items.Clear();
            ddlMyAccountURL.DataSource = LINQParentPages;
            ddlMyAccountURL.DataTextField = "PageName";
            ddlMyAccountURL.DataValueField = "SEOName";
            ddlMyAccountURL.DataBind();
            ddlMyAccountURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlShoppingCartURL.Items.Clear();
            ddlShoppingCartURL.DataSource = LINQParentPages;
            ddlShoppingCartURL.DataTextField = "PageName";
            ddlShoppingCartURL.DataValueField = "SEOName";
            ddlShoppingCartURL.DataBind();
            ddlShoppingCartURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
          

            ddlDetailsPageURL.Items.Clear();
            ddlDetailsPageURL.DataSource = LINQParentPages;
            ddlDetailsPageURL.DataTextField = "PageName";
            ddlDetailsPageURL.DataValueField = "SEOName";
            ddlDetailsPageURL.DataBind();
            ddlDetailsPageURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlItemDetailURL.Items.Clear();
            ddlItemDetailURL.DataSource = LINQParentPages;
            ddlItemDetailURL.DataTextField = "PageName";
            ddlItemDetailURL.DataValueField = "SEOName";
            ddlItemDetailURL.DataBind();
            ddlItemDetailURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlCategoryDetailURL.Items.Clear();
            ddlCategoryDetailURL.DataSource = LINQParentPages;
            ddlCategoryDetailURL.DataTextField = "PageName";
            ddlCategoryDetailURL.DataValueField = "SEOName";
            ddlCategoryDetailURL.DataBind();
            ddlCategoryDetailURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));           

            ddlSingleCheckOutURL.Items.Clear();
            ddlSingleCheckOutURL.DataSource = LINQParentPages;
            ddlSingleCheckOutURL.DataTextField = "PageName";
            ddlSingleCheckOutURL.DataValueField = "SEOName";
            ddlSingleCheckOutURL.DataBind();
            ddlSingleCheckOutURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlMultiCheckOutURL.Items.Clear();
            ddlMultiCheckOutURL.DataSource = LINQParentPages;
            ddlMultiCheckOutURL.DataTextField = "PageName";
            ddlMultiCheckOutURL.DataValueField = "SEOName";
            ddlMultiCheckOutURL.DataBind();
            ddlMultiCheckOutURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));            

            ddlStoreLocatorURL.Items.Clear();
            ddlStoreLocatorURL.DataSource = LINQParentPages;
            ddlStoreLocatorURL.DataTextField = "PageName";
            ddlStoreLocatorURL.DataValueField = "SEOName";
            ddlStoreLocatorURL.DataBind();
            ddlStoreLocatorURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));          


            ddlRssFeedURL.Items.Clear();
            ddlRssFeedURL.DataSource = LINQParentPages;
            ddlRssFeedURL.DataTextField = "PageName";
            ddlRssFeedURL.DataValueField = "SEOName";
            ddlRssFeedURL.DataBind();
            ddlRssFeedURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
           
            ddlTrackPackageUrl.Items.Clear();
            ddlTrackPackageUrl.DataSource = LINQParentPages;
            ddlTrackPackageUrl.DataTextField = "PageName";
            ddlTrackPackageUrl.DataValueField = "SEOName";
            ddlTrackPackageUrl.DataBind();
            ddlTrackPackageUrl.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlShippingDetailPageURL.Items.Clear();
            ddlShippingDetailPageURL.DataSource = LINQParentPages;
            ddlShippingDetailPageURL.DataTextField = "PageName";
            ddlShippingDetailPageURL.DataValueField = "SEOName";
            ddlShippingDetailPageURL.DataBind();
            ddlShippingDetailPageURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlItemMgntPageURL.Items.Clear();
            ddlItemMgntPageURL.DataSource = LINQParentPages;
            ddlItemMgntPageURL.DataTextField = "PageName";
            ddlItemMgntPageURL.DataValueField = "SEOName";
            ddlItemMgntPageURL.DataBind();
            ddlItemMgntPageURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlCatMgntPageURL.Items.Clear();
            ddlCatMgntPageURL.DataSource = LINQParentPages;
            ddlCatMgntPageURL.DataTextField = "PageName";
            ddlCatMgntPageURL.DataValueField = "SEOName";
            ddlCatMgntPageURL.DataBind();
            ddlCatMgntPageURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));           
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }


    public string UserModuleID { get; set; }
}
