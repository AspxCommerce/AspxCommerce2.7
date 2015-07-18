using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsProvider
    {
        public PopularTagsProvider() { }

        public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
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

        public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<PopularTagsSettingInfo> pSettingList;
                pSettingList = sqlH.ExecuteAsList<PopularTagsSettingInfo>("[dbo].[usp_Aspx_PopularTagsSettingGet]", paramCollection);
                return pSettingList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCollection.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                SQLHandler sqlH = new SQLHandler();
                PopularTagsSettingKeyPair pTSettingByKey = new PopularTagsSettingKeyPair();
                pTSettingByKey = sqlH.ExecuteAsObject<PopularTagsSettingKeyPair>("[dbo].[usp_Aspx_PopularTagsSettingValueGetBYKey]", paramCollection);
                return pTSettingByKey;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUpdatePopularTagsSetting(AspxCommonInfo aspxCommonObj, PopularTagsSettingKeyPair pTSettingList)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCollection.Add(new KeyValuePair<string, object>("@SettingKeys", pTSettingList.SettingKey));
                paramCollection.Add(new KeyValuePair<string, object>("@SettingValues", pTSettingList.SettingValue));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_PopularTagsSettingsUpdate]", paramCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<PopularTagsRssFeedInfo> GetPopularTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<PopularTagsRssFeedInfo>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@Tag", ""));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<PopularTagsRssFeedInfo>("[dbo].[usp_Aspx_GetRssFeedPopularTag]", Parameter);
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

        public List<RssFeedNewTags> GetNewTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedNewTags>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@Tag", ""));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<RssFeedNewTags>("[dbo].[usp_Aspx_GetRssFeedNewTag]", Parameter);
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

        public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@TagIDs", tagIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (SortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDDesc]";
                }
                else if (SortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDAsc]";
                }
                else if (SortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceDesc]";
                }
                else if (SortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceAsc]";
                }
                else if (SortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByName]";
                }
                else if (SortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByViewCount]";
                }
                else if (SortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsFeatured]";
                }
                else if (SortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsSpecial]";
                }
                else if (SortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortBySoldItem]";
                }
                else if (SortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByDiscount]";
                }
                else if (SortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetItemsByTagIDSortByRatedValue]";
                }
                List<ItemBasicDetailsInfo> lstItemBasic = sqlH.ExecuteAsList<ItemBasicDetailsInfo>(spName, parameterCollection);
                return lstItemBasic;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
