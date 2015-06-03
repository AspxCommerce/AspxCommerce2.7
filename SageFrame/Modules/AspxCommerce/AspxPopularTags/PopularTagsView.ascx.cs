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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Text;
using AspxCommerce.PopularTags;

public partial class Modules_AspxCommerce_AspxPopularTags_PopularTagsView : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName;
    public string ModuleCollapsible;

    public bool IsEnablePopularTag;
    public int PopularTagCount;
    public int TaggedItemInARow;
    public bool IsEnablePopularTagRss;
    public string PopularTagsRssPageName = string.Empty;
    public int PopularTagRssCount;
    public string ViewAllTagsPageName;
    public string ViewTaggedItemPageName;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                GetPopularTagsSettings();
                StoreSettingConfig ssc = new StoreSettingConfig();
                ModuleCollapsible = ssc.GetStoreSettingsByKey(StoreSetting.ModuleCollapsible, StoreID, PortalID, CultureName);
            }
            IncludeLanguageJS();
            if (PopularTagCount > 0)
            {
                GetAllPopularTags();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void GetPopularTagsSettings()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        PopularTagsController ptc = new PopularTagsController();
        List<PopularTagsSettingInfo> ptSettingInfo = ptc.GetPopularTagsSetting(aspxCommonObj);
        if (ptSettingInfo != null && ptSettingInfo.Count > 0)
        {
            foreach (var item in ptSettingInfo)
            {
                IsEnablePopularTag = item.IsEnablePopularTag;
                PopularTagCount = item.PopularTagCount;
                TaggedItemInARow = item.TaggedItemInARow;
                IsEnablePopularTagRss = item.IsEnablePopularTagRss;
                PopularTagRssCount = item.PopularTagRssCount;
                PopularTagsRssPageName = item.PopularTagsRssPageName;
                ViewAllTagsPageName = item.ViewAllTagsPageName;
                ViewTaggedItemPageName = item.ViewTaggedItemPageName;
            }
        }
    }

    Hashtable hst = null;
    public void GetAllPopularTags()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.UserName = UserName;
        aspxCommonObj.CultureName = CultureName;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        int? tagCount = 0;
        PopularTagsController ptc = new PopularTagsController();
        List<TagDetailsInfo> lstTagDetail = ptc.GetAllPopularTags(aspxCommonObj, PopularTagCount);
        tagCount = lstTagDetail.Count;
        if (tagCount > PopularTagCount)
            lstTagDetail.RemoveAt(lstTagDetail.Count - 1);
        StringBuilder popularTagContent = new StringBuilder();
        popularTagContent.Append("<div id=\"divPopularTags\" class=\"cssClassPopularTags\">");
        StringBuilder allTag = new StringBuilder();
        allTag.Append("<span id=\"divViewAllTags\" class=\"cssClassViewAllTags\"></span>");
        ltrViewAllTag.Text = allTag.ToString();
        if (lstTagDetail != null && lstTagDetail.Count > 0)
        {
            float? totalTagCount = 0;
            popularTagContent.Append("<ul id=\"tagList\">");
            for (int index = 0; index < lstTagDetail.Count; index++)
            {
                totalTagCount = lstTagDetail[index].TagCount;
                string fSize = (totalTagCount / 10 < 1) ? ((totalTagCount / 10) + 1) + "em" : (((totalTagCount / 10) > 2) ? "2em" : (totalTagCount / 10) + "em");
                if (index != lstTagDetail.Count - 1)
                {
                    popularTagContent.Append("<li><a title=\"See all items tagged with ");
                    popularTagContent.Append(lstTagDetail[index].Tag);
                    popularTagContent.Append("\" href=\"");
                    popularTagContent.Append(aspxRedirectPath + ViewTaggedItemPageName + pageExtension + "?tagsId=");
                    popularTagContent.Append(lstTagDetail[index].ItemTagIDs);
                    popularTagContent.Append("\" style=\"");
                    popularTagContent.Append("font-size: ");
                    popularTagContent.Append(fSize);
                    popularTagContent.Append(";\">");
                    popularTagContent.Append(lstTagDetail[index].Tag + " ");
                    popularTagContent.Append("</a></li>");
                }
                else
                {
                    popularTagContent.Append("<li><a title=\"See all items tagged with ");
                    popularTagContent.Append(lstTagDetail[index].Tag);
                    popularTagContent.Append("\" href=\"");
                    popularTagContent.Append(aspxRedirectPath + ViewTaggedItemPageName + pageExtension + "?tagsId=");
                    popularTagContent.Append(lstTagDetail[index].ItemTagIDs);
                    popularTagContent.Append("\" style=\"");
                    popularTagContent.Append("font-size: ");
                    popularTagContent.Append(fSize);
                    popularTagContent.Append("\">");
                    popularTagContent.Append(lstTagDetail[index].Tag);
                    popularTagContent.Append("</a></li>");
                }
            }
            popularTagContent.Append("</ul><div class=\"cssClassClear\"></div>");
            if (tagCount > PopularTagCount && tagCount > 0)
            {
                string strHtml = "<span class=\"cssClassViewMore\"><a href=\"" + aspxRedirectPath + ViewAllTagsPageName + pageExtension + "\" title =\"View all tags\">" + getLocale("View All Tags") + "</a></span>";
                popularTagContent.Append(strHtml);
                ltrViewAllTag.Visible = true;
            }
            else
            {
                ltrViewAllTag.Visible = false;
            }
        }
        else
        {
            popularTagContent.Append("<span class=\"cssClassNotFound\">");
            popularTagContent.Append(getLocale("Not any items have been tagged yet!"));
            popularTagContent.Append("</span>");
            ltrViewAllTag.Visible = false;
        }
        popularTagContent.Append("</div>");
        ltrPopularTags.Text = popularTagContent.ToString();
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