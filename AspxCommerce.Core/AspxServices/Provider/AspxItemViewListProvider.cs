using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxItemViewListProvider
    {
        public AspxItemViewListProvider()
        {
        }

        public static List<CategoryDetailsOptionsInfo> GetLatestItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetLatestItemsDetailSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryDetailsOptionsInfo> GetGiftCardItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("[usp_Aspx_GetLatestGiftCards]", parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryDetailsOptionsInfo> GetBestSoldItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetBestSellerItemDetailsSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<CategoryDetailsOptionsInfo> GetRecentlyViewedItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetRecentlyViewedItemDetailsSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryDetailsOptionsInfo> GetSpecialItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryDetailsOptionsInfo> GetFeatureItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetFeatureItemDetailsSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<CategoryDetailsOptionsInfo> GetAllHeavyDiscountItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetHeavyDiscountDetailsSortByRatedValue]";
                }
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<CategoryDetailsOptionsInfo> GetAllSeasonalItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                List<CategoryDetailsOptionsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetSeasonalDetails", parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
