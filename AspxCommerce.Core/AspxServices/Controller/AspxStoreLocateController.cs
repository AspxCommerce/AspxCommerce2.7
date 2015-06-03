using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxStoreLocateController
    {
       public AspxStoreLocateController()
       {
       }

       public static List<StoreLocatorInfo> GetAllStoresLocation(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateProvider.GetAllStoresLocation(aspxCommonObj);
               return lstStoreLocate;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static List<StoreLocatorInfo> GetLocationsNearBy(double latitude, double longitude, double searchDistance, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateProvider.GetLocationsNearBy(latitude, longitude, searchDistance, aspxCommonObj);
               return lstStoreLocate;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static bool UpdateStoreLocation(AspxCommonInfo aspxCommonObj, string storeName, String storeDescription, string streetName, string localityName, string city, string state, string country, string zip, double latitude, double longitude)
       {
           try
           {
               bool retValue = AspxStoreLocateProvider.UpdateStoreLocation(aspxCommonObj, storeName, storeDescription, streetName, localityName, city, state, country, zip, latitude, longitude);
               return retValue;
           }
           catch (Exception e)
           {
               return false;
               throw e;
           }
       }

       public static void AddStoreLocatorSettings(string settingKey, string settingValue, string cultureName, int storeID, int portalID, string userName)
       {
           try
           {
               AspxStoreLocateProvider.AddStoreLocatorSettings(settingKey, settingValue, cultureName, storeID, portalID, userName);
           }
           catch (Exception e)
           {

               throw e;
           }
       }
    }
}
