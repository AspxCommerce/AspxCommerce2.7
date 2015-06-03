using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using SageFrame.Web;

namespace AspxCommerce.Core
{
   public class AspxRssFeedController
    {
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
                //string forCss = "type='text/css' href='" +pagepath.Replace("RssFeed.aspx", "Modules/AspxCommerce/AspxRssFeed/RssFeed.css") + "'";
                //rssXml.WriteProcessingInstruction("xml-stylesheet", forCss); 

                rssXml.WriteStartElement("rss");
                rssXml.WriteAttributeString("version", "2.0");
                rssXml.WriteStartElement("channel");
                rssXml.WriteElementString("link", pagepath);
                switch (rssOption)
                {
                    case "latestitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Latest Items"));
                        GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "bestsellitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Best sell Items"));
                        GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "specialitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Special Items"));
                        GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "featureitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Feature Items"));
                        GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "heavydiscountitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Heavy Discount Items"));
                        GetItemRssFeedContents(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "servicetypeitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Service Type Items"));
                        GetServiceTypeRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "category":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Category"));
                        GetCategoryRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "populartags":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Popular Tags"));
                        GetPopularTagRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "neworders":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce New Orders"));
                        GetNewOrderRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "newcustomers":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce New Customers"));
                        GetNewCustomerRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "newitemreview":
                        rssXml.WriteElementString("title",getLocale("AspxCommerce New Item Review"));
                        GetNewItemReviewRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "newtags":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce New Tag"));
                        GetNewItemTagRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "lowstockitems":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Low Stock Items"));
                        GetLowStockItemRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "brands":
                        rssXml.WriteElementString("title",getLocale("AspxCommerce Popular Brands"));
                        GetBrandRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "fbrands":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce Featured Brands"));
                        GetBrandRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
                        break;

                    case "abrands":
                        rssXml.WriteElementString("title", getLocale("AspxCommerce All Brands"));
                        GetBrandRssFeedContent(aspxCommonObj, rssXml, pageURL, rssOption, count);
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
        private static string getLocale(string messageKey)
        {
            string modulePath = "~/Modules/AspxCommerce/AspxRssFeed/";
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
                List<RssFeedItemInfo> itemRss =AspxRssFeedProvider.GetItemRssContent(aspxCommonObj, rssOption, count);
                if (itemRss.Count>0)
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
                        description = "<div><a href=http://" + pageUrl + "/item/" + rssItemData.SKU +
                                      SageFrameSettingKeys.PageExtension + "><img alt=" +
                                      rssItemData.AlternateText + " src=http://" + pageUrl + "/" +
                                      imagePath.Replace("uploads", "uploads/Small") + " /> </a></div>";
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
                    rssXml.WriteElementString("link","");
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

        private void GetServiceTypeRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
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
                List<RssFeedServiceType> bestSellItemRss = AspxRssFeedProvider.GetServiceTypeRssContent(aspxCommonObj, rssOption, count);
                if (bestSellItemRss.Count > 0)
                {
                    foreach (RssFeedServiceType rssItemData in bestSellItemRss)
                    {
                        string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
                        rssXml.WriteStartElement("item");
                        rssXml.WriteElementString("title", rssItemData.ServiceName);
                        rssXml.WriteElementString("link",
                                                  "http://" + pageUrl + "/service/" + rssItemData.ServiceName +
                                                  SageFrameSettingKeys.PageExtension);
                        rssXml.WriteStartElement("description");
                        if (rssItemData.ImagePath == "")
                        {
                            imagePath = noImageUrl;
                        }
                        var description = "";
                        description = "<div><a href=http://" + pageUrl + "/service/" + rssItemData.ServiceName +
                                      SageFrameSettingKeys.PageExtension + "><img alt=" +
                                      rssItemData.ServiceName + " src=http://" + pageUrl + "/" +
                                      imagePath.Replace("uploads/Small", "uploads/Small") + " /> </a></div>";
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
                    description = "<div><h2><span>There are no services available!</span></h2></div>";
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

        private void GetCategoryRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
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
                List<RssFeedCategory> categoryRss = AspxRssFeedProvider.GetCategoryRssContent(aspxCommonObj, rssOption, count);
                if (categoryRss.Count > 0)
                {
                    foreach (RssFeedCategory rssItemData in categoryRss)
                    {
                        string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
                        rssXml.WriteStartElement("item");
                        rssXml.WriteElementString("title", rssItemData.CategoryName);
                        rssXml.WriteElementString("link",
                                                  "http://" + pageUrl + "/category/" + rssItemData.CategoryName +
                                                  SageFrameSettingKeys.PageExtension);
                        rssXml.WriteStartElement("description");
                        if (rssItemData.ImagePath == "")
                        {
                            imagePath = noImageUrl;
                        }
                        var description = "";
                        description = "<div><a href=http://" + pageUrl + "/category/" + rssItemData.CategoryName +
                                      SageFrameSettingKeys.PageExtension + "><img alt=" +
                                      rssItemData.CategoryName + " src=http://" + pageUrl + "/" +
                                      imagePath.Replace("uploads/Small", "uploads/Small") + " /> </a></div>";
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
                    description = "<div><h2><span>This store has no category found!</span></h2></div>";
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
                List<RssFeedPopularTag> popularTagRss = AspxRssFeedProvider.GetPopularTagsRssContent(aspxCommonObj, rssOption, count);
                if (popularTagRss.Count > 0)
                {
                    foreach (RssFeedPopularTag rssItemData in popularTagRss)
                    {
                        rssXml.WriteStartElement("item");
                        rssXml.WriteElementString("title", rssItemData.TagName);
                        rssXml.WriteElementString("link",
                                                  "http://" + pageUrl + "/tagsitems/tags" +
                                                  SageFrameSettingKeys.PageExtension + "?tagsId=" + rssItemData.TagIDs +
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

        private void GetNewOrderRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
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
                List<RssFeedNewOrders> popularTagRss = AspxRssFeedProvider.GetNewOrdersRssContent(aspxCommonObj,
                                                                                                    rssOption, count);
                foreach (RssFeedNewOrders rssItemData in popularTagRss)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", "");
                    rssXml.WriteElementString("link", " ");
                    rssXml.WriteStartElement("description");

                    var description = "";

                    description += "<div><table width=\"100%\"><tr><td colspan=\"2\">OrderID:" + rssItemData.OrderID + "</td></tr>";
                    description += "<tr><td>Order Date: " + rssItemData.AddedOn + "</td><td>Order Status: " + rssItemData.OrderStatus + "</td></tr>";
                    description += "<tr><td>Store Name: " + rssItemData.StoreName + "</td><td>Customer Name: " + rssItemData.CustomerName + "</td></tr>";
                    description += "<tr><td>Grand Total: " + rssItemData.GrandTotal + "</td><td>Payment Method Name: " + rssItemData.PaymentMethodName + "</td></tr>";
                    description += "</table></div>";
                    description += "<div><ul style=list-style-type: none><h2> Ordered Items:</h2>";
                    foreach (var orderItemInfo in rssItemData.OrderItemInfo)
                    {
                        if (orderItemInfo.ImagePath == "")
                        {
                            orderItemInfo.ImagePath = noImageUrl;
                        }
                        description += "<li style=\"float: left;display: inline;padding-right: 50px\"><h2>" +
                                       orderItemInfo.ItemName + "</h2><a href=http://" + pageUrl + "/item/" +
                                       orderItemInfo.SKU + SageFrameSettingKeys.PageExtension + "><img alt=" +
                                       orderItemInfo.ItemName + " src=http://" + pageUrl + "/" +
                                       orderItemInfo.ImagePath.Replace("uploads", "uploads/Small") + " /></a></li>";
                    }
                    description += "</ul></div>";

                    rssXml.WriteCData(description);
                    rssXml.WriteEndElement();

                    // rssXml.WriteElementString("pubDate", rssItemData.AddedOn);
                    rssXml.WriteElementString("pubDate", "");
                    rssXml.WriteEndElement();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetNewCustomerRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            try
            {
                string[] path = pageURL.Split('?');
                string pagepath = path[0];
                string x = HttpContext.Current.Request.ApplicationPath;
                string authority = HttpContext.Current.Request.Url.Authority;
                string pageUrl = authority + x;
                List<RssFeedNewCustomer> categoryRss = AspxRssFeedProvider.GetNewCustomerRssFeedContent(aspxCommonObj, rssOption, count);
                foreach (RssFeedNewCustomer rssItemData in categoryRss)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssItemData.UserName);
                    rssXml.WriteElementString("link", "");
                    rssXml.WriteStartElement("description");

                    var description = "<div><ul>";
                    description += "<li><h2>UserName: " + rssItemData.UserName + "</h2></li>";
                    description += "<li><h2>Customer Name: " + rssItemData.CustomerName + "</h2></li>";
                    description += "<li><h2>Customer Email: " + rssItemData.Email + "</h2></li>";
                    description += "<li><h2>Registered On: " + rssItemData.AddedOn + "</h2></li>";
                    description += "</ul></div>";

                    rssXml.WriteCData(description);
                    rssXml.WriteEndElement();

                    rssXml.WriteElementString("pubDate", " ");
                    rssXml.WriteEndElement();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetNewItemReviewRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            try
            {
                string[] path = pageURL.Split('?');
                string noImageUrl = string.Empty;
                StoreSettingConfig ssc = new StoreSettingConfig();
                noImageUrl = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
                string pagepath = path[0];
                string x = HttpContext.Current.Request.ApplicationPath;
                string authority = HttpContext.Current.Request.Url.Authority;
                string pageUrl = authority + x;
                List<RssFeedNewItemReview> popularTagRss = AspxRssFeedProvider.GetNewItemReviewRssContent(aspxCommonObj, rssOption, count);
                foreach (RssFeedNewItemReview rssItemData in popularTagRss)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssItemData.ItemName);
                    rssXml.WriteElementString("link", "http://" + pageUrl + "/item/" + rssItemData.SKU + SageFrameSettingKeys.PageExtension);
                    rssXml.WriteStartElement("description");
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
                    if (rssItemData.ImagePath == "")
                    {
                        imagePath = noImageUrl;
                    }
                    var description = "";
                    description = "<div><a href=http://" + pageUrl + "/item/" + rssItemData.SKU +
                                  SageFrameSettingKeys.PageExtension + "><img alt=" +
                                  rssItemData.ItemName + " src=http://" + pageUrl + "/" +
                                  imagePath.Replace("uploads", "uploads/Small") + " /> </a></div>";
                    description += "<div class=\"itemTable\"><table width=\"100%\">";
                    description += "<tr><td>Reviewed By: " + rssItemData.UserName + " </td></tr>";
                    description += "<tr><td>Total Averge Rating: " + rssItemData.TotalRatingAverage + "</td></tr>";
                    description += "<tr><td>Review Status: " + rssItemData.Status + "</td></tr>";
                    description += "<tr><td>Summary Review: " + rssItemData.ReviewSummary + "</td></tr>";
                    description += "</table></div>";
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
                List<RssFeedNewTag> popularTagRss = AspxRssFeedProvider.GetNewTagsRssContent(aspxCommonObj, rssOption, count);
                foreach (RssFeedNewTag rssItemData in popularTagRss)
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

        private void GetLowStockItemRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
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
                List<RssFeedLowStock> bestSellItemRss = AspxRssFeedProvider.GetLowStockItemRssContent(aspxCommonObj, rssOption, count);
                foreach (RssFeedLowStock rssItemData in bestSellItemRss)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssItemData.ItemName);
                    rssXml.WriteElementString("link",
                                              "http://" + pageUrl + "/item/" + rssItemData.SKU +
                                              SageFrameSettingKeys.PageExtension);
                    rssXml.WriteStartElement("description");
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + rssItemData.ImagePath;
                    if (rssItemData.ImagePath == "")
                    {
                        imagePath = noImageUrl;
                    }
                    var description = "";
                    description = "<div><a href=http://" + pageUrl + "/item/" + rssItemData.SKU +
                                  SageFrameSettingKeys.PageExtension + "><img alt=" +
                                  rssItemData.ItemName + " src=http://" + pageUrl + "/" +
                                  imagePath.Replace("uploads", "uploads/Small") + " /> </a>";

                    description += "<div class=\"itemTable\"><table width=\"20%\">";
                    description += "<tr><td><h2>Price: </h2>" + rssItemData.Price + "</td><td><h2>Quantity: </h2>" +
                                   rssItemData.Quantity + "</td></tr>";
                    description += "<tr><td><h2>Status: </h2>" + rssItemData.Status + "</td><td></td></tr>";
                    description += "</table></div>";
                    description += "</div>";
                    rssXml.WriteCData(description);
                    rssXml.WriteEndElement();
                    rssXml.WriteElementString("pubDate", "");
                    rssXml.WriteEndElement();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetBrandRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
       {
            try
            {
                string[] path = pageURL.Split('?');
                string pagepath = path[0];
                string x = HttpContext.Current.Request.ApplicationPath;
                string authority = HttpContext.Current.Request.Url.Authority;
                string pageUrl = authority + x;
                List<RssFeedBrand> brandRssContent = AspxRssFeedProvider.GetBrandRssContent(aspxCommonObj, rssOption,count);
                foreach (RssFeedBrand rssFeedBrand in brandRssContent)
                {
                    rssXml.WriteStartElement("item");
                    rssXml.WriteElementString("title", rssFeedBrand.BrandName);
                    rssXml.WriteElementString("link",
                                              "http://" + pageUrl + "/brand/" + rssFeedBrand.BrandName +SageFrameSettingKeys.PageExtension);
                    rssXml.WriteStartElement("description");
                    string description = "";
                    description += "<div>";
                    description += "<div><a href=http://"+pageUrl+"/brand/"+rssFeedBrand.BrandName+SageFrameSettingKeys.PageExtension +">";
                    description += "<img src=http://"+pageUrl+"/"+ rssFeedBrand.BrandImageUrl.Replace("uploads","uploads/Small")+"  />";
                    description += "</a></div>";
                    description +="<p>"+HttpUtility.HtmlDecode(rssFeedBrand.BrandDescription) +"</p>";

                    description += "</div>";
                    rssXml.WriteCData(description);
                    rssXml.WriteEndElement();
                    rssXml.WriteElementString("pubDate", rssFeedBrand.AddedOn);
                    rssXml.WriteEndElement();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
       }
    }
}
