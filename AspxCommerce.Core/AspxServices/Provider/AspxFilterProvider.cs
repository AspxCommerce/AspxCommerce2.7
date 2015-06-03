using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.Core
{
    public class AspxFilterProvider
    {
        public AspxFilterProvider()
        {
        }

        public static List<Filter> GetShoppingFilter(AspxCommonInfo aspxCommonObj, string categoryName, bool isByCategory)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@categoryKey", categoryName));
            parameter.Add(new KeyValuePair<string, object>("@isByCategory", isByCategory));
            SQLHandler sqlH = new SQLHandler();
            List<Filter> lstFilter = sqlH.ExecuteAsList<Filter>("usp_Aspx_ShoppingFilter", parameter);
            return lstFilter;
        }

        public static List<CategoryDetailFilter> GetCategoryDetailFilter(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryName", categorykey));
                SQLHandler sqlH = new SQLHandler();
                List<CategoryDetailFilter> lstCatDetFilter = sqlH.ExecuteAsList<CategoryDetailFilter>("usp_Aspx_CategoryDetailsForFilter", parameterCollection);
                return lstCatDetFilter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ItemBasicDetailsInfo> GetShoppingFilterItemsResult(int offset, int limit, string brandIds, string attributes, decimal priceFrom, decimal priceTo, string categoryName, bool isByCategory, int sortBy, AspxCommonInfo aspxCommonObj)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@BrandIDs", brandIds));
                parameter.Add(new KeyValuePair<string, object>("@Attributes", attributes));
                parameter.Add(new KeyValuePair<string, object>("@PriceFrom", priceFrom));
                parameter.Add(new KeyValuePair<string, object>("@PriceTo", priceTo));
                parameter.Add(new KeyValuePair<string, object>("@CategoryKey", categoryName));
                parameter.Add(new KeyValuePair<string, object>("@IsByCategory", isByCategory));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetShoppingOptionsItemsResultSortByRatedValue]";
                }
                List<ItemBasicDetailsInfo> lstItemBasic = sqlH.ExecuteAsList<ItemBasicDetailsInfo>(spName, parameter);
                return lstItemBasic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //changed to get dataset 
        public static List<CategoryDetailFilter> GetAllSubCategoryForFilter(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@categorykey", categorykey));
                SQLHandler sqlH = new SQLHandler();
                List<CategoryDetailFilter> lstCatDet = sqlH.ExecuteAsList<CategoryDetailFilter>("usp_Aspx_GetAllSubCategoryForFilter", parameterCollection);
                return lstCatDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetCategoryDetailInfoForFilter(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@categorykey", categorykey));
                SQLHandler sqlH = new SQLHandler();
                DataSet lstCatDet = sqlH.ExecuteAsDataSet("usp_Aspx_GetAllSubCategoryForFilter", parameterCollection);
                return lstCatDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<BrandItemsInfo> GetAllBrandForCategory(string categorykey, bool isByCategory, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@categorykey", categorykey));
                parameterCollection.Add(new KeyValuePair<string, object>("@isByCategory", isByCategory));
                SQLHandler sqlH = new SQLHandler();
                List<BrandItemsInfo> lstBrandItem = sqlH.ExecuteAsList<BrandItemsInfo>("usp_Aspx_GetAllBrandForCategory", parameterCollection);
                return lstBrandItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CategorySEOInfo GetSEOSettingsByCategoryName(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@CatName", categorykey));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsObject<CategorySEOInfo>("usp_Aspx_CategorySEODetailsByCatName", parameterCollection);
        }


    }
}
