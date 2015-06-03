using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxFilterController
    {
       public AspxFilterController()
       {
       }

       public static List<Filter> GetShoppingFilter(AspxCommonInfo aspxCommonObj, string categoryName, bool isByCategory)
       {
           List<Filter> lstFilter = AspxFilterProvider.GetShoppingFilter(aspxCommonObj, categoryName, isByCategory);
           return lstFilter;
       }

       public static List<CategoryDetailFilter> GetCategoryDetailFilter(string categorykey, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CategoryDetailFilter> lstCatDetFilter = AspxFilterProvider.GetCategoryDetailFilter(categorykey, aspxCommonObj);
               return lstCatDetFilter;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static List<ItemBasicDetailsInfo> GetShoppingFilterItemsResult(int offset, int limit, string brandIds, string attributes, decimal priceFrom, decimal priceTo, string categoryName, bool isByCategory, int sortBy, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ItemBasicDetailsInfo> lstItemBasic = AspxFilterProvider.GetShoppingFilterItemsResult(offset, limit, brandIds,attributes,priceFrom,priceTo ,categoryName, isByCategory, sortBy, aspxCommonObj);
               return lstItemBasic;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       //Has been modified to return dataset
       public static List<CategoryDetailFilter> GetAllSubCategoryForFilter(string categorykey, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CategoryDetailFilter> lstCatDet = AspxFilterProvider.GetAllSubCategoryForFilter(categorykey, aspxCommonObj);
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
               DataSet lstCatDet = AspxFilterProvider.GetCategoryDetailInfoForFilter(categorykey, aspxCommonObj);
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
               List<BrandItemsInfo> lstBrandItem = AspxFilterProvider.GetAllBrandForCategory(categorykey, isByCategory, aspxCommonObj);
               return lstBrandItem;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static CategorySEOInfo GetSEOSettingsByCategoryName(string categorykey, AspxCommonInfo aspxCommonObj)
       {

           return AspxFilterProvider.GetSEOSettingsByCategoryName(categorykey, aspxCommonObj);
       }
    }
}
