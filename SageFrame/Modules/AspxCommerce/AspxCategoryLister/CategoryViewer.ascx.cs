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
using SageFrame.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public partial class Modules_CategoryLister_CategoryViewer : BaseAdministrationUserControl
{
    private int StoreID, PortalID;
    private string UserName, CultureName;
    private string itemPath = string.Empty;
    private Hashtable hst = null;

    public string CategoryRss, RssFeedUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            IncludeJs("CategoryViewer", "/js/jquery.cookie.js", "/js/MegaMenu/jquery.hoverIntent.minified.js", "/js/superfish.js");
            IncludeCss("CategoryViewer", "/Templates/" + TemplateName + "/css/SuperfishMenu/superfish.css");
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            if (!IsPostBack)
            {
                StoreSettingConfig ssc = new StoreSettingConfig();
                CategoryRss = ssc.GetStoreSettingsByKey(StoreSetting.NewCategoryRss, GetStoreID, GetPortalID, GetCurrentCultureName);
                if (CategoryRss.ToLower() == "true")
                {
                    RssFeedUrl = ssc.GetStoreSettingsByKey(StoreSetting.RssFeedURL, GetStoreID, GetPortalID, GetCurrentCultureName);
                }
            }
            GetCategoryMenuList(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    #region Get Category Menu List
    private void GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
    {
        List<CategoryInfo> catInfo = AspxCategoryListController.GetCategoryMenuList(aspxCommonObj);
        if (catInfo.Count > 0)
        {
            int categoryID = 0;
            int parentID = 0;
            int? categoryLevel;
            string attributeValue = string.Empty; ;
            StringBuilder catListmaker = new StringBuilder();
            string catList = string.Empty;
            catListmaker.Append("<div class=\"cssClassCategoryNavHor\"><ul class='sf-menu'>");
            string css, hrefParentCategory, pageExtension =  SageFrameSettingKeys.PageExtension;
            foreach (CategoryInfo eachCat in catInfo)
            {
                css = string.Empty;
                if (eachCat.ChildCount > 0)
                {
                    css = " class=\"cssClassCategoryParent\"";
                }
                else
                {
                    css = "";
                }
                categoryID = eachCat.CategoryID;
                parentID = eachCat.ParentID;
                categoryLevel = eachCat.CategoryLevel;
                attributeValue = eachCat.AttributeValue;
                if (eachCat.CategoryLevel == 0)
                {
                    hrefParentCategory = string.Empty;
                    hrefParentCategory = aspxRedirectPath + "category/" + AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue) + pageExtension;
                    catListmaker.Append("<li");
                    catListmaker.Append(css);
                    catListmaker.Append("><a href=");
                    catListmaker.Append(hrefParentCategory);
                    catListmaker.Append(">");
                    catListmaker.Append(eachCat.AttributeValue);
                    catListmaker.Append("</a>");
                    if (eachCat.ChildCount > 0)
                    {
                        catListmaker.Append("<ul>");
                        itemPath += eachCat.AttributeValue;
                        catListmaker.Append(BindChildCategory(catInfo, categoryID));
                        catListmaker.Append("</ul>");
                    }
                    catListmaker.Append("</li>");
                }
                itemPath = string.Empty;
            }
            catListmaker.Append("<div class=\"cssClassclear\"></div></ul></div>");
            divCategoryListerH.InnerHtml = catListmaker.ToString();
        }
        else
        {
            string strText = ("<span class=\"cssClassNotFound\">" + getLocale("This store has no category found!") + "</span>");//Need to add Local Text
            divCategoryListerH.InnerHtml = strText;
        }          
    }
    #endregion

    #region BindChildCategory
    private string BindChildCategory(List<CategoryInfo> response, int categoryID)
    {
        StringBuilder strListmaker = new StringBuilder();
        string childNodes = string.Empty; ;
        string path = string.Empty; ;
        itemPath += "/";
        string pageExtension = SageFrameSettingKeys.PageExtension;
        foreach (CategoryInfo eachCat in response)
        {
            if (eachCat.CategoryLevel > 0)
            {
                if (eachCat.ParentID == categoryID)
                {
                    string css = string.Empty;
                    if (eachCat.ChildCount > 0)
                    {
                        css = " class=\"cssClassCategoryParent\"";
                    }
                    else
                    {
                        css = "";
                    }
                    string hrefCategory = aspxRedirectPath + "category/" + AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue) + pageExtension;
                    itemPath += eachCat.AttributeValue;
                    strListmaker.Append("<li");
                    strListmaker.Append(css);
                    strListmaker.Append("><a href=");
                    strListmaker.Append(hrefCategory);
                    strListmaker.Append(">");
                    strListmaker.Append(eachCat.AttributeValue);
                    strListmaker.Append("</a>");
                    childNodes = BindChildCategory(response, eachCat.CategoryID);
                    itemPath = itemPath.Replace(itemPath.LastIndexOf(eachCat.AttributeValue).ToString(), "");
                    if (childNodes != string.Empty)
                    {
                        strListmaker.Append("<ul>");
                        strListmaker.Append(childNodes);
                        strListmaker.Append("</ul>");
                    }
                    strListmaker.Append("</li>");
                }
            }
        }
        return strListmaker.ToString();
    }
    #endregion

    #region Localization
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
    #endregion
}
