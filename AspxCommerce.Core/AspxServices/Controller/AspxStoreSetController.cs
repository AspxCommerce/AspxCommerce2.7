using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxStoreSetController
    {
       public AspxStoreSetController()
       {

       }
       public static StoreSettingInfo GetAllStoreSettings(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               StoreSettingInfo DefaultStoreSettings = AspxStoreSetProvider.GetAllStoreSettings(aspxCommonObj);
               return DefaultStoreSettings;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static void UpdateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath, string prevStoreLogoPath, string newStoreLogoPath, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxStoreSetProvider.UpdateStoreSettings(settingKeys, settingValues, prevFilePath, newFilePath, prevStoreLogoPath, newStoreLogoPath, aspxCommonObj);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static string GetStoreSettingValueByKey(string settingKey, int storeID, int portalID, string cultureName)
       {
           try
           {
               string setValue = AspxStoreSetProvider.GetStoreSettingValueByKey(settingKey, storeID, portalID, cultureName);
               return setValue;

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static List<DisplayItemsOptionsInfo> BindItemsViewAsList()
       {
           try
           {
               List<DisplayItemsOptionsInfo> bind = AspxStoreSetProvider.BindItemsViewAsList();
               return bind;

           }
           catch (Exception e)
           {
               throw e;
           }
       }
   }
}
