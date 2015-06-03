using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxTagsProvider
    {
        public AspxTagsProvider()
        {
        }

        #region PopularTags Module

        public static void AddTagsOfItem(string itemSKU, string tags, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tags));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddTagsOfItem", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static List<ItemTagsInfo> GetItemTags(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                SQLHandler sqlH = new SQLHandler();
                List<ItemTagsInfo> lstItemTags = sqlH.ExecuteAsList<ItemTagsInfo>("[usp_Aspx_GetTagsByItemID]", parameter);
                return lstItemTags;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteUserOwnTag(string itemTagID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemTagID", itemTagID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserOwnTag", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteMultipleTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@TagsIDS", itemTagIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleTags", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tag));
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> lstTagDetail = sqlH.ExecuteAsList<TagDetailsInfo>("[dbo].[usp_Aspx_GetAllTagsPending]", parameter);
                return lstTagDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tag));
                parameter.Add(new KeyValuePair<string, object>("@TagStatus", tagStatus));
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> lstTagDetail = sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetAllTags", parameter);
                return lstTagDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> lstTagDetail = sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetPopularTags", parameterCollection);
                return lstTagDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<TagDetailsInfo> GetTagsByUserName(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> lstTagDetail = sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetTagsOfUser", parameterCollection);
                return lstTagDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Tags Reports
        //---------------------Customer tags------------

        public static List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerTagInfo> lstCustomerTag = sqlH.ExecuteAsList<CustomerTagInfo>("usp_Aspx_GetCustomerItemTags", parameter);
                return lstCustomerTag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------Show Customer tags list------------

        public static List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<ShowCustomerTagsListInfo> lstCustTag = sqlH.ExecuteAsList<ShowCustomerTagsListInfo>("usp_Aspx_ShowCustomerTagList", parameter);
                return lstCustTag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------Item tags details------------

        public static List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<ItemTagsDetailsInfo> lstItemTags = sqlH.ExecuteAsList<ItemTagsDetailsInfo>("usp_Aspx_GetItemTagsDetails", parameter);
                return lstItemTags;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------Show Item tags list------------

        public static List<ShowItemTagsListInfo> ShowItemTagList(int offset, System.Nullable<int> limit, int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqlH = new SQLHandler();
                List<ShowItemTagsListInfo> lstShowItemTags = sqlH.ExecuteAsList<ShowItemTagsListInfo>("usp_Aspx_ShowTagsByItems", parameter);
                return lstShowItemTags;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------Popular tags details------------

        public static List<PopularTagsInfo> GetPopularTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<PopularTagsInfo> lstPopTags = sqlH.ExecuteAsList<PopularTagsInfo>("usp_Aspx_GetPopularityTags", parameter);
                return lstPopTags;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------Show Popular tags list------------

        public static List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, System.Nullable<int> limit, string tagName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@TagName", tagName));
                SQLHandler sqlH = new SQLHandler();
                List<ShowpopulartagsDetailsInfo> lstShowPopTag = sqlH.ExecuteAsList<ShowpopulartagsDetailsInfo>("usp_Aspx_ShowPopularTagsDetails", parameter);
                return lstShowPopTag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy,int rowTotal, AspxCommonInfo aspxCommonObj)
        //{
        //    string spName = string.Empty;
        //    try
        //    {
        //        List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
        //        parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
        //        parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
        //        parameterCollection.Add(new KeyValuePair<string, object>("@TagIDs", tagIDs));
        //        parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
        //        SQLHandler sqlH = new SQLHandler();
        //        if (SortBy == 1)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDDesc]";
        //        }
        //        else if (SortBy == 2)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDAsc]";
        //        }
        //        else if (SortBy == 3)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceDesc]";
        //        }
        //        else if (SortBy == 4)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceAsc]";
        //        }
        //        else if (SortBy == 5)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByName]";
        //        }
        //        else if (SortBy == 6)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByViewCount]";
        //        }
        //        else if (SortBy == 7)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsFeatured]";
        //        }
        //        else if (SortBy == 8)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsSpecial]";
        //        }
        //        else if (SortBy == 9)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortBySoldItem]";
        //        }
        //        else if (SortBy == 10)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByDiscount]";
        //        }
        //        else if (SortBy == 11)
        //        {
        //            spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByRatedValue]";
        //        }
        //        List<ItemBasicDetailsInfo> lstItemBasic = sqlH.ExecuteAsList<ItemBasicDetailsInfo>(spName, parameterCollection);
        //        return lstItemBasic;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion
        #endregion

        #region Tags Management

        public static void UpdateTag(string itemTagIDs, int? itemId, int statusID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemTagIDs", itemTagIDs));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateTag", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemTagIDs", itemTagIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTag", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string itemIDs, string tagName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
                parameter.Add(new KeyValuePair<string, object>("@TagName", tagName));
                SQLHandler sqlH = new SQLHandler();
                List<ItemBasicDetailsInfo> lstItemBasic = sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByMultipleItemID", parameter);
                return lstItemBasic;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion
    }
}
