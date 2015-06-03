using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class ItemViewListController
    {
        public List<CategoryDetailsOptionsInfo> GetLatestItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj ,int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetLatestItemsDetail", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDetailsOptionsInfo> GetGiftCardItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("[usp_Aspx_GetLatestGiftCards]", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDetailsOptionsInfo> GetBestSoldItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetBestSellerItemDetails", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CategoryDetailsOptionsInfo> GetRecentlyViewedItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetRecentlyViewedItemDetails", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDetailsOptionsInfo> GetSpecialItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetSpecialItemDetails", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDetailsOptionsInfo> GetFeatureItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_GetFeatureItemDetails", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        //public List<SortOptionTypeInfo> BindItemsSortByList()
        //{
        //    try
        //    {
        //        SQLHandler sqlH = new SQLHandler();
        //        List<SortOptionTypeInfo> bind = sqlH.ExecuteAsList<SortOptionTypeInfo>("usp_Aspx_DisplayItemSortByOptions");
        //        return bind;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
