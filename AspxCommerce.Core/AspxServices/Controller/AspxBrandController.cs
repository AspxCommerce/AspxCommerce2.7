using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxBrandController
    {
       public AspxBrandController()
       {
       }
      

       public static List<BrandInfo> GetAllBrandList(int offset, int limit, AspxCommonInfo aspxCommonObj, string brandName)
       {
           try
           {
               List<BrandInfo> lstBrand = AspxBrandProvider.GetAllBrandList(offset,limit,aspxCommonObj,brandName);
               return lstBrand;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void InsertNewBrand(string prevFilePath, AspxCommonInfo aspxCommonObj, BrandInfo brandInsertObj, string imagePath)
       {
           try
           {
               AspxBrandProvider.InsertNewBrand(prevFilePath, aspxCommonObj, brandInsertObj, imagePath);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<BrandInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<BrandInfo> lstBrand = AspxBrandProvider.GetAllBrandForItem(aspxCommonObj);
               return lstBrand;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteBrand(string BrandID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxBrandProvider.DeleteBrand(BrandID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
      
       public static List<BrandInfo> GetBrandByItemID(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<BrandInfo> lstBrand = AspxBrandProvider.GetBrandByItemID(ItemID,aspxCommonObj);
               return lstBrand;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void ActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxBrandProvider.ActivateBrand(brandID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxBrandProvider.DeActivateBrand(brandID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ItemBasicDetailsInfo> GetBrandItemsByBrandID(int offset, int limit, string brandName, int SortBy,int rowTotal, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ItemBasicDetailsInfo> lstItem = AspxBrandProvider.GetBrandItemsByBrandID(offset,limit,brandName,SortBy,rowTotal,aspxCommonObj);
               return lstItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<BrandInfo> GetBrandDetailByBrandID(string brandName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<BrandInfo> lstBrand = AspxBrandProvider.GetBrandDetailByBrandID(brandName, aspxCommonObj);
               return lstBrand;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckBrandUniqueness(string brandName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               bool isUnique = AspxBrandProvider.CheckBrandUniqueness(brandName, aspxCommonObj);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
