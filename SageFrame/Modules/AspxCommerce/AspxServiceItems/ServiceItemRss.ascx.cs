using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.ServiceItem;
using System.Collections;
using System.Xml;
using System.Text;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxServiceItems_ServiceItemRss :BaseAdministrationUserControl
{
    public int StoreID, PortalID, ServiceRssCount;
    public string CultureName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CultureName = GetCurrentCultureName;
            GetServiceSetting();
            GetServiceItemsRssFeed();

        }
        IncludeLanguageJS();
    }
    public void GetServiceSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        ServiceItemController objService = new ServiceItemController();
        List<ServiceItemSettingInfo> lstService = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstService != null && lstService.Count > 0)
        {
            foreach (ServiceItemSettingInfo item in lstService)
            {
                ServiceRssCount = item.ServiceRssCount;
            }
        }
    }
    private void GetServiceItemsRssFeed()
    {
        try
        {           
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCulture();
            string pageURL = Request.Url.AbsoluteUri;

            GetRssFeedContens(aspxCommonObj, pageURL, ServiceRssCount);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetRssFeedContens(AspxCommonInfo aspxCommonObj, string pageURL, int count)
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
            rssXml.WriteElementString("title", getLocale("AspxCommerce Services"));
            GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL,count);
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
    public string getLocale(string messageKey)
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

    private void GetItemRssFeedContents(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL,int count)
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
            ServiceItemController sic = new ServiceItemController();
            List<ServiceItemRss> itemRss = sic.GetServiceTypeRssFeedContent(aspxCommonObj, count);
            if (itemRss.Count > 0)
            {
                foreach (ServiceItemRss rssItemData in itemRss)
                {
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
                    if (rssItemData.ImagePath != "")
                    {
                        //Resize Image Dynamically
                        InterceptImageController.ImageBuilder(rssItemData.ImagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                    }
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssItemData.ServiceName);
                    rssXml.WriteElementString("link",
                                              "http://" + pageUrl + "/service/" + rssItemData.ServiceName +
                                              SageFrameSettingKeys.PageExtension);
                    rssXml.WriteStartElement("description");
                    var description = "";
                    if (rssItemData.ImagePath == "")
                    {
                        imagePath = noImageUrl;
                    }
                    description = "<div><a href=http://" + pageUrl + "/service/" + rssItemData.ServiceName +
                             SageFrameSettingKeys.PageExtension + "><img src=http://" + pageUrl + "/" +
                             imagePath.Replace("uploads", "uploads/Small") + " alt=" + rssItemData.ServiceName + " /> </a></div>";
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
                description = "<div><h2><span>This store has no items listed yet!</span></h2></div>";
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

}
