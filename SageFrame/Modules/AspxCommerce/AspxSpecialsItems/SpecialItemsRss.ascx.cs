using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.SpecialItems;
using System.Web.Script.Serialization;
using System.Xml;
using System.Text;
using System.Collections;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxSpecials_SpecialItemsRss : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string CultureName;
    public int SpecialItemsRssCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        string UserName = string.Empty;
        GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);   
        if (!IsPostBack)
        {

            GetSpecialItemSetting(aspxCommonObj);
            GetSpecialItemsRssFeed(aspxCommonObj);
        }
        IncludeLanguageJS();
    }


    private void GetSpecialItemSetting(AspxCommonInfo aspxCommonObj)
    {

        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        SpecialItemsController sic = new SpecialItemsController();
        SpecialItemsSettingInfo lstSpecialSetting = sic.GetSpecialItemsSetting(aspxCommonObj);

        if (lstSpecialSetting != null)
        {
            SpecialItemsRssCount = lstSpecialSetting.SpecialItemsRssCount;
        }
    }

    private void GetSpecialItemsRssFeed(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            if (Request.QueryString["type"] != null && Request.QueryString["type"] != string.Empty)
            {
                string rssOption = Request.QueryString["type"];
                string pageURL = Request.Url.AbsoluteUri;
                GetRssFeedContens(aspxCommonObj, pageURL, rssOption, SpecialItemsRssCount);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetRssFeedContens(AspxCommonInfo aspxCommonObj, string pageURL, string rssOption, int count)
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
                case "specialitems":
                    rssXml.WriteElementString("title", getLocale("AspxCommerce Special Items"));
                    GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
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

    private void GetItemRssFeedContents(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
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
                      SpecialItemsController sic = new SpecialItemsController();

            List<RssFeedItemInfo> itemRss = sic.GetItemRssFeedContents(aspxCommonObj, rssOption, count);

            if (itemRss.Count > 0)
            {
                foreach (RssFeedItemInfo rssItemData in itemRss)
                {
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
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
                             imagePath.Replace("uploads", "uploads/Small") + " alt=" + rssItemData.ItemName + " /> </a></div>";
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