using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxBrandProvider
    {
        public AspxBrandProvider()
        {
        }
       

        public static List<BrandInfo> GetAllBrandList(int offset, int limit, AspxCommonInfo aspxCommonObj, string brandName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
                ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
                ParaMeter.Add(new KeyValuePair<string, object>("@BrandName", brandName));
                SQLHandler sqLH = new SQLHandler();
                List<BrandInfo> lstBrand= sqLH.ExecuteAsList<BrandInfo>("usp_Aspx_GetAllBrandList", ParaMeter);
                return lstBrand;
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
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqLH = new SQLHandler();
                List<BrandInfo> lstBrand = sqLH.ExecuteAsList<BrandInfo>("usp_Aspx_GetAllBrandForItem", ParaMeter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandID", brandInsertObj.BrandID));
                parameter.Add(new KeyValuePair<string, object>("@BrandName", brandInsertObj.BrandName));
                parameter.Add(new KeyValuePair<string, object>("@Branddescription", brandInsertObj.BrandDescription));
                parameter.Add(new KeyValuePair<string, object>("@BrandImgUrl", imagePath));
                parameter.Add(new KeyValuePair<string, object>("@isShowInSlider", brandInsertObj.IsShowInSlider));
                parameter.Add(new KeyValuePair<string, object>("@IsFeatured", brandInsertObj.IsFeatured));
                parameter.Add(new KeyValuePair<string, object>("@FeaturedFrom", brandInsertObj.FeaturedFrom));
                parameter.Add(new KeyValuePair<string, object>("@FeaturedTo", brandInsertObj.FeaturedTo));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_InsertAndUpdateBrand", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandID", BrandID));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_DeleteBrandByID", parameter);
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
                List<KeyValuePair<string, object>> ParaMeter =CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
                SQLHandler sqLH = new SQLHandler();
                List<BrandInfo> lstBrand= sqLH.ExecuteAsList<BrandInfo>("usp_Aspx_GetBrandByItemID", ParaMeter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandID", brandID));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_BrandActivate", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandID", brandID));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_BrandDeActivate", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ItemBasicDetailsInfo> GetBrandItemsByBrandID(int offset, int limit, string brandName, int SortBy,int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@BrandName", brandName));
                parameter.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (SortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByItemIDDesc]";
                }
                else if (SortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByItemIDAsc]";
                }
                else if (SortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByPriceDesc]";
                }
                else if (SortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByPriceAsc]";
                }
                else if (SortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByName]";
                }
                else if (SortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByViewCount]";
                }
                else if (SortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByIsFeatured]";
                }
                else if (SortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByIsSpecial]";
                }
                else if (SortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortBySoldItem]";
                }
                else if (SortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByDiscount]";
                }
                else if (SortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetBrandItemByBrandIDSortByRatedValue]";
                }
                List<ItemBasicDetailsInfo> lstItem = sqlH.ExecuteAsList<ItemBasicDetailsInfo>(spName, parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandName", brandName));
                SQLHandler sqlH = new SQLHandler();
                List<BrandInfo> lstBrand= sqlH.ExecuteAsList<BrandInfo>("usp_Aspx_GetBrandDetailByBrandID", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@BrandName", brandName));
                SQLHandler sqlH = new SQLHandler();
                bool isUnique= sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckBrandUniqueness", parameter, "@isUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
