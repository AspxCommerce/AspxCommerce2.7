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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

public partial class Modules_AspxGeneralSearch_SimpleSearch : BaseAdministrationUserControl
{
    #region Variables
    private int StoreID, PortalID;
    private string UserName;
    private string CultureName;
    private StringBuilder Elements;
    public string ShowCategoryForSearch, EnableAdvanceSearch, ShowSearchKeyWords, ResultPage, AdvanceSearchPageName;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            GetPortalCommonInfo(out  StoreID, out  PortalID, out  UserName, out  CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            if (!IsPostBack)
            {
                IncludeJs("SimpleSearch", "/Modules/AspxCommerce/AspxGeneralSearch/js/SimpleSearch.js");
                IncludeCss("SimpleSearch", "/Modules/AspxCommerce/AspxGeneralSearch/css/module.css");
                ResultPage = ssc.GetStoreSettingsByKey(StoreSetting.DetailPageURL, StoreID, PortalID, CultureName);
            }
            GetSearchTerms(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private Hashtable hst = null;
    #region Get All Category For Search
    private void GetAllCategoryForSearch(AspxCommonInfo aspxCommonObj, string modulePath, string pageExtension)
    {
        bool isActive = true;
        string prefix = "---";
        List<CategoryInfo> lstCategory = AspxSearchController.GetAllCategoryForSearch(prefix, isActive, aspxCommonObj);
        if (lstCategory != null && lstCategory.Count > 0)
        {
            Elements = new StringBuilder();
            Elements.Append("<select id=\"sfSimpleSearchCategory\">");
            Elements.Append("<option value=\"0\" ><a href=\"#\"><span class=\"value\" category=\"--All Category--\">");
            Elements.Append(getLocale("--All Category--"));
            Elements.Append("</span></a></option>");
            foreach (CategoryInfo item in lstCategory)
            {
                Elements.Append("<option value=\"");
                Elements.Append(item.CategoryID);
                Elements.Append("\" isGiftCard=\"");
                Elements.Append(item.IsChecked);
                Elements.Append("\"><a href=\"#\"><span class=\"value\" category=\"");
                Elements.Append(item.LevelCategoryName);
                Elements.Append("\">");
                Elements.Append(item.LevelCategoryName);
                Elements.Append("</span></a></option>");
            }
            Elements.Append("</select>");
            litSSCat.Text = Elements.ToString();
        }
    }
    #endregion

    #region Localization Method
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

    #region GetSearchTerms
    private void GetSearchTerms(AspxCommonInfo aspxCommonObj)
    {
        int TotalRows = 0;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string pageExtension = SageFrameSettingKeys.PageExtension;
        GetAllCategoryForSearch(aspxCommonObj, modulePath, pageExtension);
        hst = AppLocalized.getLocale(modulePath);
        DataSet dsGeneralSearch = AspxSearchController.GetGeneralSearchDataSet(aspxCommonObj);
        if (dsGeneralSearch != null && dsGeneralSearch.Tables.Count == 2)
        {
            #region Search Terms
            DataTable dtSearchTerms = dsGeneralSearch.Tables[0];
            TotalRows = dtSearchTerms.Rows.Count;
            if (TotalRows > 0)
            {
                Elements = new StringBuilder();
                Elements.Append("<div id=\"topSearch\" class=\"cssClassTopSearch\" style=\"display: none\">");
                Elements.Append("<span>");
                Elements.Append(getLocale("Popular:"));
                Elements.Append("</span>");
                Elements.Append("<ul id=\"topSearchNew\">");
                string searchTerms = string.Empty;
                for (int i = 0; i < TotalRows; i++)
                {
                    searchTerms = dtSearchTerms.Rows[i]["SearchTerm"].ToString();
                    Elements.Append("<li><a href=\"");
                    Elements.Append(aspxRedirectPath);
                    Elements.Append("search/simplesearch");
                    Elements.Append(pageExtension);
                    Elements.Append("?cid=0&amp;isgiftcard=false&amp;q=");
                    Elements.Append(searchTerms);
                    Elements.Append("\">");
                    Elements.Append(searchTerms);
                    Elements.Append("</a></li>");
                }
                Elements.Append("</ul>");
                Elements.Append("</div>");
                litTopSearch.Text = Elements.ToString();
            }
            #endregion
            #region Search Setting
            DataTable dsSearchSetting = dsGeneralSearch.Tables[1];
            TotalRows = dsSearchSetting.Rows.Count;
            if (TotalRows == 1)
            {
                ShowCategoryForSearch = dsSearchSetting.Rows[0]["ShowCategoryForSearch"].ToString();
                EnableAdvanceSearch = dsSearchSetting.Rows[0]["EnableAdvanceSearch"].ToString();
                ShowSearchKeyWords = dsSearchSetting.Rows[0]["ShowSearchKeyWord"].ToString();
                AdvanceSearchPageName = dsSearchSetting.Rows[0]["AdvanceSearchPageName"].ToString();
            }
            #endregion
        }
    }
    #endregion
}
