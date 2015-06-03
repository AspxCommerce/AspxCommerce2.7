using System;
using System.Collections.Generic;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.BrandView;
using System.Web;
using System.Collections;
using System.Xml;
using System.Text;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxBrandView_BrandRss : BaseAdministrationUserControl
{
    public int StoreID, PortalID, BrandRssCount;
    public string CultureName;
    public string rssOption;
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserName = string.Empty;
        GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);   
        if (!IsPostBack)
        {
            rssOption = Request.QueryString["type"];
            GetBrandSetting(aspxCommonObj);
            GetBrandRssFeedContent(aspxCommonObj);

        }
        IncludeLanguageJS();
    }
    private void GetBrandSetting(AspxCommonInfo aspxCommonObj)
    {
        AspxBrandViewController objBrand = new AspxBrandViewController();
        BrandSettingInfo lstBrandSetting = objBrand.GetBrandSetting(aspxCommonObj);
        if (lstBrandSetting != null)
        {
            BrandRssCount = lstBrandSetting.BrandRssCount;
        }
    }   

    private static Hashtable hst = null;
    private void GetBrandRssFeedContent(AspxCommonInfo aspxCommonObj)
    {
        try
        { 
            AspxBrandViewController objBrand = new AspxBrandViewController();
            List<BrandRssInfo> brandRssContent = objBrand.GetBrandRssFeedContent(aspxCommonObj, rssOption, BrandRssCount);
            BindBrandRss(brandRssContent, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindBrandRss(List<BrandRssInfo> brandRssContent, AspxCommonInfo aspxCommonObj)
    {
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
        switch (rssOption)
        {
            case "brands":
                rssXml.WriteElementString("title", getLocale("AspxCommerce Popular Brands"));               
                break;
            case "fbrands":
                rssXml.WriteElementString("title", getLocale("AspxCommerce Featured Brands"));               
                break;
            case "abrands":
                rssXml.WriteElementString("title", getLocale("AspxCommerce All Brands"));               
                break;
            default:
                break;
        }
        if (brandRssContent.Count > 0)
        {
            foreach (BrandRssInfo rssFeedBrand in brandRssContent)
            {
                rssXml.WriteStartElement("item");
                rssXml.WriteElementString("title", rssFeedBrand.BrandName);
                rssXml.WriteElementString("link",
                                          "http://" + pageUrl + "/brand/" +AspxUtility.fixedEncodeURIComponent(rssFeedBrand.BrandName) + SageFrameSettingKeys.PageExtension);
                rssXml.WriteStartElement("description");
                string description = "";
                description += "<div>";
                string []brandURL= rssFeedBrand.BrandImageUrl.Split('/');
                string brandImage = brandURL[brandURL.Length - 1] ;
                if (brandImage!="")
                {
                 //Resize Image Dynamically
                  InterceptImageController.ImageBuilder(brandImage, ImageType.Small, ImageCategoryType.Brand, aspxCommonObj);
                }
                description += "<div><a href=http://" + pageUrl + "/brand/" + rssFeedBrand.BrandName + SageFrameSettingKeys.PageExtension + ">";
                description += "<img src=http://" + pageUrl + "/" + rssFeedBrand.BrandImageUrl.Replace("uploads", "uploads/Small") + "  />";
                description += "</a></div>";
                description += "<p>" + HttpUtility.HtmlDecode(rssFeedBrand.BrandDescription) + "</p>";

                description += "</div>";
                rssXml.WriteCData(description);
                rssXml.WriteEndElement();
                rssXml.WriteElementString("pubDate", rssFeedBrand.AddedOn);
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
            description = "<div><h2><span>"+getLocale("This store has no items listed yet!")+"</span></h2></div>";
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
