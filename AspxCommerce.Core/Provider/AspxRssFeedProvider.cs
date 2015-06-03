using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxRssFeedProvider
    {
        public static List<RssFeedItemInfo> GetItemRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedItemInfo>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                switch (rssOption)
                {
                    case "latestitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedLatestItem]", Parameter);
                        break;
                    case "bestsellitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedBestSellItem]", Parameter);
                        break;
                    case "specialitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedSpecialItem]", Parameter);
                        break;
                    case "featureitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedFeatureItem]", Parameter);
                        break;
                    case "heavydiscountitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedHeavyDiscountItem]", Parameter);
                        break;
                }
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedServiceType> GetServiceTypeRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedServiceType>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedServiceType>("[dbo].[usp_Aspx_GetRssFeedServiceTypeItem]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedCategory> GetCategoryRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedCategory>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedCategory>("[dbo].[usp_Aspx_GetRssFeedCategory]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedPopularTag> GetPopularTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedPopularTag>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@Tag", ""));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedPopularTag>("[dbo].[usp_Aspx_GetRssFeedPopularTag]", Parameter);
                Parameter.Remove(new KeyValuePair<string, object>("@Tag", ""));
                foreach (var popularTag in rssFeedContent)
                {
                    var popularTagItem = new List<ItemCommonInfo>();
                    Parameter.Add(new KeyValuePair<string, object>("@Tag", popularTag.TagName));
                    popularTagItem = SQLH.ExecuteAsList<ItemCommonInfo>("[dbo].[usp_Aspx_GetRssFeedPopularTag]", Parameter);
                    popularTag.TagItemInfo = popularTagItem;
                    Parameter.Remove(new KeyValuePair<string, object>("@Tag", popularTag.TagName));
                }
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedNewOrders> GetNewOrdersRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedNewOrders>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@OrderID", 0));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedNewOrders>("[dbo].[usp_Aspx_GetRssFeedNewOrder]", Parameter);
                Parameter.Remove(new KeyValuePair<string, object>("@OrderID", 0));
                foreach (var newOrderData in rssFeedContent)
                {
                    var orderItems = new List<ItemCommonInfo>();
                    Parameter.Add(new KeyValuePair<string, object>("@OrderID", newOrderData.OrderID));
                    orderItems = SQLH.ExecuteAsList<ItemCommonInfo>("[dbo].[usp_Aspx_GetRssFeedNewOrder]", Parameter);
                    newOrderData.OrderItemInfo = orderItems;
                    Parameter.Remove(new KeyValuePair<string, object>("@OrderID", newOrderData.OrderID));
                }
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedNewCustomer> GetNewCustomerRssFeedContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedNewCustomer>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedNewCustomer>("[dbo].[usp_Aspx_GetRssFeedNewCustomer]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedLowStock> GetLowStockItemRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var ssc = new StoreSettingConfig();
                int LowStockQuantity;
                LowStockQuantity = Int32.Parse(ssc.GetStoreSettingsByKey(StoreSetting.LowStockQuantity, aspxCommonObj.StoreID,
                                                             aspxCommonObj.PortalID, aspxCommonObj.CultureName));
                var rssFeedContent = new List<RssFeedLowStock>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@LowStockQuantity", LowStockQuantity));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedLowStock>("[dbo].[usp_Aspx_GetRssFeedLowStockItem]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedNewTag> GetNewTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedNewTag>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@Tag", ""));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedNewTag>("[dbo].[usp_Aspx_GetRssFeedNewTag]", Parameter);
                Parameter.Remove(new KeyValuePair<string, object>("@Tag", ""));
                foreach (var popularTag in rssFeedContent)
                {
                    var popularTagItem = new List<ItemCommonInfo>();
                    Parameter.Add(new KeyValuePair<string, object>("@Tag", popularTag.TagName));
                    popularTagItem = SQLH.ExecuteAsList<ItemCommonInfo>("[dbo].[usp_Aspx_GetRssFeedNewTag]", Parameter);
                    popularTag.TagItemInfo = popularTagItem;
                    Parameter.Remove(new KeyValuePair<string, object>("@Tag", popularTag.TagName));
                }

                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RssFeedNewItemReview> GetNewItemReviewRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedNewItemReview>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedNewItemReview>("[dbo].[usp_Aspx_GetRssFeedNewItemReview]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
       public static List<RssFeedBrand>GetBrandRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
       {
           try
           {
               List<RssFeedBrand> rssFeedContent = new List<RssFeedBrand>();
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@Count",count));
               Parameter.Add(new KeyValuePair<string, object>("@RssOption",rssOption));
               SQLHandler SQLH= new SQLHandler();
               rssFeedContent = SQLH.ExecuteAsList<RssFeedBrand>("[dbo].[usp_Aspx_GetRssFeedBrand]", Parameter);
               return rssFeedContent;

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
    }
}
