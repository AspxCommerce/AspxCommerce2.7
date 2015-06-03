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
using System.Collections;
using System.Collections.Generic;
using AspxCommerce.Core;
using System.Text;

public partial class Modules_AspxBrowseByCategory_BrowseByCategory : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName;
    public int count, level;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            IncludeCss("BrowseByCategory", "/Modules/AspxCommerce/AspxBrowseByCategory/css/module.css");
            IncludeLanguageJS();
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            GetBrowseByCategorySetting(aspxCommonObj);
            GetShoppingOptionsByCategory(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;

    public void GetBrowseByCategorySetting(AspxCommonInfo aspxCommonObj)
    {

        List<CategoryDetailsInfo> lstCategorySetting = AspxBrowseCategoryController.GetBrowseByCategorySetting(aspxCommonObj);

        if (lstCategorySetting != null && lstCategorySetting.Count > 0)
        {
            foreach (CategoryDetailsInfo item in lstCategorySetting)
            {
                count = item.ItemCount;
                level = item.CategoryLevel;
            }
        }
        
    }
    public void GetShoppingOptionsByCategory(AspxCommonInfo aspxCommonObj)
    {
        int categoryId = 0;
        string pageExtension = SageFrameSettingKeys.PageExtension;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        List<CategoryDetailsInfo> lstCategory = AspxBrowseCategoryController.BindCategoryDetails(categoryId,count,level, aspxCommonObj);
        StringBuilder categoryOptions = new StringBuilder();
        categoryOptions.Append("<div class=\"cssClassBrowseByCategory\" id=\"divCategoryItemsOptions\">");
        if (lstCategory != null && lstCategory.Count > 0)
        {
            categoryOptions.Append("<h2>");
            categoryOptions.Append(getLocale("Browse by"));
            categoryOptions.Append("</h2><ul>");
            foreach (CategoryDetailsInfo item in lstCategory)
            {
                categoryOptions.Append("<li><a href=\"");
                categoryOptions.Append(aspxRedirectPath);
                categoryOptions.Append("category/");
                categoryOptions.Append(AspxUtility.fixedEncodeURIComponent(item.CategoryName));
                categoryOptions.Append(pageExtension);
                categoryOptions.Append("\" alt=\"");
                categoryOptions.Append(item.CategoryName);
                categoryOptions.Append("\" title=\"");
                categoryOptions.Append(item.CategoryName);
                categoryOptions.Append("\">");
                categoryOptions.Append(item.CategoryName);
                categoryOptions.Append("</a></li>");
            }
            categoryOptions.Append("</ul><div class=\"cssClassclear\"></div>");
        }
        else
        {
            categoryOptions.Append("<span class=\"cssClassNotFound\">");
            categoryOptions.Append(getLocale("No category with item is found!"));
            categoryOptions.Append("</span>");
        }
        categoryOptions.Append("</div>");
        ltrCategoryItemsOptions.Text = categoryOptions.ToString();
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
