using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxStoreLocateProvider
    {
       public AspxStoreLocateProvider()
       {
       }

       public static List<StoreLocatorInfo> GetAllStoresLocation(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<StoreLocatorInfo>  lstStoreLocate= sqlH.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_StoreLocatorGetAllStore", parameterCollection);
               return lstStoreLocate;
           }
           catch (Exception e)
           {

               throw e;
           }
       }
   
       public static List<StoreLocatorInfo> GetLocationsNearBy(double latitude, double longitude, double searchDistance, AspxCommonInfo aspxCommonObj)
       {

           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@CenterLatitude", latitude));
           parameterCollection.Add(new KeyValuePair<string, object>("@CenterLongitude", longitude));
           parameterCollection.Add(new KeyValuePair<string, object>("@SearchDistance", searchDistance));
           parameterCollection.Add(new KeyValuePair<string, object>("@EarthRadius", 3961));
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<StoreLocatorInfo> lstStoreLocate= sqlH.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_StoreLocatorGetNearbyStore", parameterCollection);
               return lstStoreLocate;
           }
           catch (Exception e)
           {

               throw e;
           }
       }
     
       public static bool UpdateStoreLocation(AspxCommonInfo aspxCommonObj, string storeName, String storeDescription, string streetName, string localityName, string city, string state, string country, string zip, double latitude, double longitude)
       {
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@StoreName", storeName));
           parameterCollection.Add(new KeyValuePair<string, object>("@StoreDescription", storeDescription));
           parameterCollection.Add(new KeyValuePair<string, object>("@StreetName", streetName));
           parameterCollection.Add(new KeyValuePair<string, object>("@LocalityName", localityName));
           parameterCollection.Add(new KeyValuePair<string, object>("@City", city));
           parameterCollection.Add(new KeyValuePair<string, object>("@State", state));
           parameterCollection.Add(new KeyValuePair<string, object>("@Country", country));
           parameterCollection.Add(new KeyValuePair<string, object>("@ZIP", zip));
           parameterCollection.Add(new KeyValuePair<string, object>("@Latitude", latitude));
           parameterCollection.Add(new KeyValuePair<string, object>("@Longitude", longitude));
           try
           {
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_StoreLocatorLocationUpdate", parameterCollection);
               return true;
           }
           catch (Exception e)
           {
               return false;
               throw e;
           }
       }
   
       public static void AddStoreLocatorSettings(string settingKey, string settingValue, string cultureName, int storeID, int portalID, string userName)
       {
           List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
           parameterCollection.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
           parameterCollection.Add(new KeyValuePair<string, object>("@SettingValue", settingValue));
           parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", userName));
           try
           {
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_StoreLocatorSettingsAdd", parameterCollection);
           }
           catch (Exception e)
           {

               throw e;
           }
       }
    }
}
