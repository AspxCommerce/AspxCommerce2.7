using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxStoreSetProvider
    {
       public AspxStoreSetProvider()
       {
       }

       public static StoreSettingInfo GetAllStoreSettings(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               StoreSettingInfo DefaultStoreSettings;
               DefaultStoreSettings = sqlH.ExecuteAsObject<StoreSettingInfo>("usp_Aspx_GetAllStoreSettings", parameter);
               return DefaultStoreSettings;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void UpdateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath, string prevStoreLogoPath, string newStoreLogoPath, AspxCommonInfo aspxCommonObj)
       {

           try
           {
               FileHelperController fileObj = new FileHelperController();
               string uplodedValue;
               if (newFilePath != null && prevFilePath != newFilePath)
               {
                   string tempFolder = @"Upload\temp";
                   uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\", aspxCommonObj.StoreID, aspxCommonObj, "store_");
               }
               else
               {
                   uplodedValue = prevFilePath;
               }

               string uploadStorelogoValue;
               if (newStoreLogoPath != null && prevStoreLogoPath != newStoreLogoPath)
               {
                   string tempFolder = @"Upload\temp";
                   uploadStorelogoValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevStoreLogoPath, newStoreLogoPath, @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\", aspxCommonObj.StoreID, aspxCommonObj, "storelogo_");
               }
               else
               {
                   uploadStorelogoValue = prevStoreLogoPath;
               }

               settingKeys = "DefaultProductImageURL" + '*' + "StoreLogoURL" + '*' + settingKeys;
               settingValues = uplodedValue + '*' + uploadStorelogoValue + '*' + settingValues;

               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
               parameter.Add(new KeyValuePair<string, object>("@SettingValues", settingValues));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_GetStoreSettingsUpdate", parameter);
               StoreSettingConfig ssc = new StoreSettingConfig();
               HttpContext.Current.Cache.Remove("AspxStoreSetting" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
               ssc.ResetStoreSettingKeys(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static string GetStoreSettingValueByKey(string settingKey, int storeID, int portalID, string cultureName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               SQLHandler sqlH = new SQLHandler();
               string setValue= sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_GetStoreSettingValueBYKey", parameter, "@SettingValue");
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

               SQLHandler sqlH = new SQLHandler();
               List<DisplayItemsOptionsInfo> bind = sqlH.ExecuteAsList<DisplayItemsOptionsInfo>("usp_Aspx_DisplayItemViewAsOptions");
               return bind;

           }
           catch (Exception e)
           {
               throw e;
           }
       }
   }
}
