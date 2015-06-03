using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using AspxCommerce.LatestItems;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxLatestItems_LatestItemRss : BaseAdministrationUserControl
{
    public int StoreID, PortalID, LatestItemRssCount;
    public string CultureName;   
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserName = string.Empty;
        GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);   
        if (!IsPostBack)
        {
            GetLatestSetting(aspxCommonObj);
            GetLatestRssFeed(aspxCommonObj);
           
        }
        IncludeLanguageJS();
    }
    public void GetLatestSetting(AspxCommonInfo aspxCommonObj)
    {
        AspxLatestItemsController objLatest = new AspxLatestItemsController();
        LatestItemSettingInfo latestSetting = objLatest.GetLatestItemSetting(aspxCommonObj);
        if (latestSetting != null)
        {
            LatestItemRssCount = latestSetting.LatestItemRssCount;
        }
    }
    private void GetLatestRssFeed(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            string pageURL = Request.Url.AbsoluteUri;
            AspxLatestItemsController objLatest = new AspxLatestItemsController();
            List<LatestItemRssInfo> lstLatestItemRss = objLatest.GetLatestRssFeedContent(aspxCommonObj, LatestItemRssCount);
            BindLatestItemRss(lstLatestItemRss, aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private static Hashtable hst = null;
    private void BindLatestItemRss(List<LatestItemRssInfo> lstLatestItemRss, AspxCommonInfo aspxCommonObj)
    {
        string noImageUrl = string.Empty;
        StoreSettingConfig ssc = new StoreSettingConfig();
        noImageUrl = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID,
                                               aspxCommonObj.PortalID, aspxCommonObj.CultureName);
        string x = HttpContext.Current.Request.ApplicationPath;
        string authority = HttpContext.Current.Request.Url.Authority;
        string pageUrl = authority + x;
        string pageURL = Request.Url.AbsoluteUri;
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
        rssXml.WriteElementString("title", getLocale("AspxCommerce Latest Items"));
        if (lstLatestItemRss.Count > 0)
        {
            foreach (LatestItemRssInfo rssItemData in lstLatestItemRss)
            {
                string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads" + rssItemData.ImagePath;
                rssXml.WriteStartElement("item");
                rssXml.WriteElementString("title", rssItemData.ItemName);
                rssXml.WriteElementString("link",
                                          "http://" + pageUrl + "/item/" + rssItemData.SKU +
                                          SageFrameSettingKeys.PageExtension);
                rssXml.WriteStartElement("description");
                var description = "";
                if (rssItemData.ImagePath == "")
                {
                    imagePath = noImageUrl;
                }
                else
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(rssItemData.ImagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);

                }
                description = "<div><a href=http://" + pageUrl + "/item/" + rssItemData.SKU +
                              SageFrameSettingKeys.PageExtension + "><img src=http://" + pageUrl + "/" +
                              imagePath.Replace("uploads", "uploads/Small/") + " alt=" + rssItemData.ItemName + " /> </a></div>";
                description += "</br>" + HttpUtility.HtmlDecode(rssItemData.ShortDescription);
                rssXml.WriteCData(description);
                rssXml.WriteEndElement();
                rssXml.WriteElementString("pubDate", rssItemData.AddedOn);
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
            description = "<div><h2><span>" + getLocale("This store has no items listed yet!") + "</span></h2></div>";
            rssXml.WriteCData(description);
            rssXml.WriteEndElement();
            rssXml.WriteEndElement();
        }
        rssXml.WriteEndElement();
        rssXml.WriteEndElement();
        rssXml.WriteEndDocument();
        rssXml.Flush();
        rssXml.Close();
        HttpContext.Current.Response.End();
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
