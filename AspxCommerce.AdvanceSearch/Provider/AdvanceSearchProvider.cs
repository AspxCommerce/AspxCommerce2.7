using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;
using SageFrame.Common;
using System.Data;

namespace AspxCommerce.AdvanceSearch
{
    public class AdvanceSearchProvider
    {
        public DataSet GetAdvanceSearchDataSet(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            try
            {
                DataSet objHDSetting = new DataSet();
                if (!CacheHelper.Get("AdvanceSearchDataSet" + aspxCommonObj.StoreID.ToString() + aspxCommonObj.PortalID.ToString() + "_" + aspxCommonObj.CultureName, out objHDSetting))
                {
                    int itemID = 0;
                    List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                    parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", CategoryID));
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsGiftCard", IsGiftCard));
                    parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                    parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                    parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                    SQLHandler sqlH = new SQLHandler();                    
                    objHDSetting = sqlH.ExecuteAsDataSet("[dbo].[usp_Aspx_AdvanceSearch]", parameterCollection);
                }
                return objHDSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public List<AdvanceSearchSettingInfo> GetAdvanceSearchSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<AdvanceSearchSettingInfo> objHDSetting;
                objHDSetting = sqlH.ExecuteAsList<AdvanceSearchSettingInfo>("[dbo].[usp_Aspx_AdvanceSearchSettingsGet]", parameterCollection);
                return objHDSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateAdvanceSearchSetting(AspxCommonInfo aspxCommonObj, AdvanceSearchSettingKeyPairInfo advanceObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", advanceObj.SettingKey));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues", advanceObj.SettingValue));
                SQLHandler sqlhandle = new SQLHandler();
                sqlhandle.ExecuteNonQuery("[dbo].[usp_Aspx_AdvanceSearchSettingsUpdate]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CategoryInfo> GetAllCategoryForSearch(string prefix, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int itemID = 0;
                List<CategoryInfo> catList = new List<CategoryInfo>();

                if (!CacheHelper.Get("CategoryForSearch" + aspxCommonObj.StoreID.ToString() + aspxCommonObj.PortalID.ToString() +"_" + aspxCommonObj.CultureName, out catList))
                {
                    List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                    parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                    parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                    SQLHandler sqlH = new SQLHandler();
                    catList = sqlH.ExecuteAsList<CategoryInfo>("dbo.usp_Aspx_GetCategoryList",
                                                               parameterCollection);
                    CacheHelper.Add(catList, "CategoryForSearch" + aspxCommonObj.StoreID.ToString() + aspxCommonObj.PortalID.ToString() + "_" + aspxCommonObj.CultureName);
                }
                return catList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Advance Search

        public List<ItemTypeInfo> GetItemTypeList()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<ItemTypeInfo> lstItemType = sqlH.ExecuteAsList<ItemTypeInfo>("usp_Aspx_GetItemTypeList");
                return lstItemType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SearchTermList> GetSearchedTermList(string search, AspxCommonInfo aspxCommonObj)
        {

            List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
            paramCol.Add(new KeyValuePair<string, object>("@Search", search));
            SQLHandler sageSQL = new SQLHandler();
            List<SearchTermList> srInfo = sageSQL.ExecuteAsList<SearchTermList>("[dbo].[usp_Aspx_GetListSearched]", paramCol);
            return srInfo;
        }

        public List<AttributeFormInfo> GetAttributeByItemType(int itemTypeID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                List<AttributeFormInfo> lstAttrForm = sqlH.ExecuteAsList<AttributeFormInfo>("usp_Aspx_GetAttributeByItemType", parameterCollection);
                return lstAttrForm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region More Advanced Search
        //------------------get dyanamic Attributes for serach-----------------------   

        public List<AttributeShowInAdvanceSearchInfo> GetAttributes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<AttributeShowInAdvanceSearchInfo> lstAttr = sqlH.ExecuteAsList<AttributeShowInAdvanceSearchInfo>("usp_Aspx_GetAttributesShowInAdvanceSearch", parameter);
                return lstAttr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------get items by dyanamic Advance serach-----------------------

        public List<AdvanceSearchDetailsInfo> GetItemsByDyanamicAdvanceSearch(int offset, int limit, AspxCommonInfo aspxCommonObj, ItemsByDynamicAdvanceSearchInfo searchObj)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", searchObj.CategoryID));
                parameter.Add(new KeyValuePair<string, object>("@IsGiftCard", searchObj.IsGiftCard));
                parameter.Add(new KeyValuePair<string, object>("@SearchText", searchObj.SearchText));
                parameter.Add(new KeyValuePair<string, object>("@BrandID", searchObj.BrandID));
                parameter.Add(new KeyValuePair<string, object>("@PriceFrom", searchObj.PriceFrom));
                parameter.Add(new KeyValuePair<string, object>("@PriceTo", searchObj.PriceTo));
                parameter.Add(new KeyValuePair<string, object>("@Attributes", searchObj.AttributeIDs));
                parameter.Add(new KeyValuePair<string, object>("@RowTotal", searchObj.RowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (searchObj.SortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByItemIDDesc]";
                }
                else if (searchObj.SortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByItemID]";
                }
                else if (searchObj.SortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByPriceDesc]";
                }
                else if (searchObj.SortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByPrice]";
                }
                else if (searchObj.SortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByName]";
                }
                else if (searchObj.SortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByViewCount]";
                }
                else if (searchObj.SortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByIsFeatured]";
                }
                else if (searchObj.SortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByIsSpecial]";
                }
                else if (searchObj.SortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortBySoldItem]";
                }
                else if (searchObj.SortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByDiscount]";
                }
                else if (searchObj.SortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_AdvanceSearchSortByRatedValue]";
                }
                List<AdvanceSearchDetailsInfo> lstAdvanceSearch = sqlH.ExecuteAsList<AdvanceSearchDetailsInfo>(spName, parameter);
                return lstAdvanceSearch;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public List<Filter> GetDynamicAttributesForAdvanceSearch(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@CategoryID", CategoryID));
            parameter.Add(new KeyValuePair<string, object>("@IsGiftCard", IsGiftCard));
            SQLHandler sqlH = new SQLHandler();
            List<Filter> lstFilter = sqlH.ExecuteAsList<Filter>("[dbo].[usp_Aspx_GetDynamicAttrByCategoryIDforAdvanceSearch]", parameter);
            return lstFilter;
        }

        public List<BrandItemsInfo> GetAllBrandForSearchByCategoryID(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", CategoryID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsGiftCard", IsGiftCard));
                SQLHandler sqlH = new SQLHandler();
                List<BrandItemsInfo> lstBrandItem = sqlH.ExecuteAsList<BrandItemsInfo>("[dbo].[usp_Aspx_GetAllBrandForSearchByCategoryID]", parameterCollection);
                return lstBrandItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
