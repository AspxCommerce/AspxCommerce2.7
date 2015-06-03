using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.PopularTags;
using System.Xml;
using System.Text;
using System.Collections;

public partial class Modules_AspxCommerce_AspxPopularTags_PopularTagsRss : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string CultureName;
    public int PopularTagRssCount;   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CultureName = GetCurrentCultureName;
            GetPopularTagsSettings();
            GetPopularTagsRssFeed();

        }
        IncludeLanguageJS();
    }

    public void GetPopularTagsSettings()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        PopularTagsController ptc = new PopularTagsController();
        List<PopularTagsSettingInfo> ptSettingInfo = ptc.GetPopularTagsSetting(aspxCommonObj);
        if (ptSettingInfo != null && ptSettingInfo.Count>0)
        {
            foreach (var item in ptSettingInfo)
            {
                PopularTagRssCount = item.PopularTagRssCount;
            }          
        }
    }

    private void GetPopularTagsRssFeed()
    {
        try
        {
            string rssOption = Request.QueryString["type"];
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            string pageURL = Request.Url.AbsoluteUri;            
            GetRssFeedContens(aspxCommonObj, pageURL, rssOption, PopularTagRssCount);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void GetRssFeedContens(AspxCommonInfo aspxCommonObj, string pageURL, string rssOption, int count)
    {
        try
        {
            string[] path = pageURL.Split('?');
            string pagepath = path[0];
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/xml";
            XmlTextWriter rssXml = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);

            rssXml.WriteStartDocument();
                      
            rssXml.WriteStartElement("rss");
            rssXml.WriteAttributeString("version", "2.0");
            rssXml.WriteStartElement("channel");
            rssXml.WriteElementString("link", pagepath);
            switch (rssOption)
            {
                case "populartags":
                    rssXml.WriteElementString("title", getLocale("AspxCommerce Popular Tags"));
                    GetPopularTagRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                    break;

                case "newtags":
                    rssXml.WriteElementString("title", getLocale("AspxCommerce New Tag"));
                    GetNewItemTagRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                    break;

                default:
                    break;
            }
            rssXml.WriteEndElement();
            rssXml.WriteEndElement();
            rssXml.WriteEndDocument();
            rssXml.Flush();
            rssXml.Close();
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static Hashtable hst = null;
    private string getLocale(string messageKey)
    {
        string modulePath = ResolveUrl((this.AppRelativeTemplateSourceDirectory));
        hst = AppLocalized.getLocale(modulePath);
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }

    private void GetPopularTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
    {
        try
        {
            string noImageUrl = string.Empty;
            StoreSettingConfig ssc = new StoreSettingConfig();
            noImageUrl = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID,
                                                   aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string[] path = pageURL.Split('?');
            string pagepath = path[0];
            string x = HttpContext.Current.Request.ApplicationPath;
            string authority = HttpContext.Current.Request.Url.Authority;
            string pageUrl = authority + x;
            PopularTagsController ptc = new PopularTagsController();
            List<PopularTagsRssFeedInfo> popularTagRss= ptc.GetRssFeedContens(aspxCommonObj, pageURL, rssOption, PopularTagRssCount);

            if (popularTagRss.Count > 0)
            {
                foreach (PopularTagsRssFeedInfo rssItemData in popularTagRss)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssItemData.TagName);
                    rssXml.WriteElementString("link", "http://" + pageUrl + "/tagsitems/tags" + SageFrameSettingKeys.PageExtension + "?tagsId=" + rssItemData.TagIDs +
                                              "");
                    rssXml.WriteStartElement("description");

                    var description = "";

                    description += "<div><ul style=list-style-type: none><h2> Taged Items:</h2>";
                    foreach (var tagItemInfo in rssItemData.TagItemInfo)
                    {
                        string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + tagItemInfo.ImagePath;
                        if (tagItemInfo.ImagePath == "")
                        {
                            imagePath = noImageUrl;
                        }
                        description += "<li style=\"float: left;display: inline;padding-right: 50px\"><h2>" +
                                       tagItemInfo.ItemName + "</h2><a href=http://" + pageUrl + "/item/" +
                                       tagItemInfo.SKU + SageFrameSettingKeys.PageExtension + "><img alt=" +
                                       tagItemInfo.ItemName + " src=http://" + pageUrl + "/" +
                                       imagePath.Replace("uploads", "uploads/Small") + " /></a></li>";
                    }
                    description += "</ul></div>";
                    rssXml.WriteCData(description);
                    rssXml.WriteEndElement();
                    rssXml.WriteEndElement();
                }
            }
            else
            {
                rssXml.WriteStartElement("item");
                rssXml.WriteElementString("title", "");
                rssXml.WriteElementString("link", "");
                rssXml.WriteStartElement("description");
                var description = "";
                description = "<div><h2><span>Not any items have been tagged yet!</span></h2></div>";
                rssXml.WriteCData(description);
                rssXml.WriteEndElement();
                rssXml.WriteEndElement();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetNewItemTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
    {
        try
        {
            string noImageUrl = string.Empty;
            StoreSettingConfig ssc = new StoreSettingConfig();
            noImageUrl = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID,
                                                   aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string[] path = pageURL.Split('?');
            string pagepath = path[0];
            string x = HttpContext.Current.Request.ApplicationPath;
            string authority = HttpContext.Current.Request.Url.Authority;
            string pageUrl = authority + x;
            PopularTagsProvider ptp = new PopularTagsProvider();
            List<RssFeedNewTags> popularTagRss = ptp.GetNewTagsRssContent(aspxCommonObj, rssOption, count);
            foreach (RssFeedNewTags rssItemData in popularTagRss)
            {
                rssXml.WriteStartElement("item");
                rssXml.WriteElementString("title", rssItemData.TagName);
                rssXml.WriteElementString("link",
                                          "http://" + pageUrl + "/tagsitems/tags" +
                                          SageFrameSettingKeys.PageExtension + "?tagsId=" + rssItemData.TagIDs + "");
                rssXml.WriteStartElement("description");

                var description = "";

                description += "<div><h2>Tag Name: " + rssItemData.TagName + "</h2></br><h2><span>Tag Status: " +
                               rssItemData.TagStatus +
                               "</span></h2><ul style=list-style-type: none><h2> Taged Item:</h2>";
                foreach (var tagItemInfo in rssItemData.TagItemInfo)
                {
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + tagItemInfo.ImagePath;
                    if (tagItemInfo.ImagePath == "")
                    {
                        imagePath = noImageUrl;
                    }
                    description += "<li style=\"float: left;display: inline;padding-right: 50px\"><h2>" +
                                   tagItemInfo.ItemName + "</h2><a href=http://" + pageUrl + "/item/" +
                                   tagItemInfo.SKU + SageFrameSettingKeys.PageExtension + "><img alt=" +
                                   tagItemInfo.ItemName + " src=http://" + pageUrl + "/" +
                                   imagePath.Replace("uploads", "uploads/Small") + " /></a></li>";
                }
                description += "</ul></div>";
                rssXml.WriteCData(description);
                rssXml.WriteEndElement();
                rssXml.WriteElementString("pubDate", rssItemData.AddedOn);
                rssXml.WriteEndElement();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}