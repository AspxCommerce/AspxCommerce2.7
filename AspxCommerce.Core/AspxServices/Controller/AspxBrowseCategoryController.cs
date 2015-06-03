using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxBrowseCategoryController
    {
       public AspxBrowseCategoryController()
       {
       }

       public static List<CategoryDetailsInfo> BindCategoryDetails(int categoryID,int count,int level, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CategoryDetailsInfo> lstCatDetail = AspxBrowseCategoryProvider.BindCategoryDetails(categoryID,count,level,aspxCommonObj);
               return lstCatDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CategoryDetailsInfo> GetBrowseByCategorySetting(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CategoryDetailsInfo> lstCatDetail = AspxBrowseCategoryProvider.GetBrowseByCategorySetting(aspxCommonObj);
               return lstCatDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void UpdateBrowseByCategorySetting(string settingKeys, string settingValues, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxBrowseCategoryProvider.UpdateBrowseByCategorySetting(settingKeys, settingValues, aspxCommonObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }


    }
}
