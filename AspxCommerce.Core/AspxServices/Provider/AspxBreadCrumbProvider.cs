using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxBreadCrumbProvider
    {
       public AspxBreadCrumbProvider()
       {
       }

       public static List<BreadCrumInfo> GetBreadCrumb(string SEOName, AspxCommonInfo commonInfo)
       {
           try
           {

               List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@SEOName", SEOName));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", commonInfo.CultureName));
              
               SQLHandler SQLH = new SQLHandler();
               return SQLH.ExecuteAsList<BreadCrumInfo>("usp_Aspx_BreadCrumbGetFromPageName", ParaMeterCollection);
               //}
           }
           catch (Exception)
           {

               throw;
           }

       }

       public static string GetCategoryForItem(int storeID, int portalID, string itemSku,string cultureName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@ItemSku", itemSku));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               SQLHandler sqlH = new SQLHandler();
               string retString= sqlH.ExecuteAsScalar<string>("usp_Aspx_GetCategoryforItems", parameter);
               return retString;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<BreadCrumInfo> GetCategoryName(string name, AspxCommonInfo commonInfo)
       {
           try
           {

               List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@CategoryName", name));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", commonInfo.CultureName));
               SQLHandler SQLH = new SQLHandler();
               return SQLH.ExecuteAsList<BreadCrumInfo>("[usp_Aspx_GetCategoryForBreadCrumb]", ParaMeterCollection);

           }
           catch (Exception ex)
           {

               throw ex;
           }

       }

       public static List<BreadCrumInfo> GetItemCategories(string itemName, AspxCommonInfo commonInfo)
       {

           try
           {
               List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", itemName));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", commonInfo.CultureName));


               SQLHandler SQLH = new SQLHandler();
               return SQLH.ExecuteAsList<BreadCrumInfo>("[usp_Aspx_GetItemsCategoryForBreadcrumb]", ParaMeterCollection);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

    }
}
