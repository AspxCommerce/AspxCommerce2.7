using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxImageGalleryProvider
    {
       public AspxImageGalleryProvider()
       {
       }

       public static void SaveGallerySettings(string settingKeys, string settingsValues, string userModuleID, string portalID, string culture)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
               parameterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleID));
               parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
               parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues", settingsValues));
               parameterCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
               SQLHandler sagesql = new SQLHandler();
               sagesql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateSettingsItemsGallery]", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static ImageGalleryInfo GetGallerySettingValues(int userModuleID, AspxCommonInfo aspxCommonObj)
       {
          
           List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
           try
           {
               parameterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleID));
               parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
               parameterCollection.Add(new KeyValuePair<string, object>("@Culture", aspxCommonObj.CultureName));
               SQLHandler sagesql = new SQLHandler();
               ImageGalleryInfo infoObject = sagesql.ExecuteAsObject<ImageGalleryInfo>("[dbo].[usp_Aspx_GetGallerySettings]", parameterCollection);
               return infoObject;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ImageGalleryItemsInfo> GetItemInfoList(int storeID, int portalID, string culture)
       {
           List<KeyValuePair<string, object>> paramCollection = new List<KeyValuePair<string, object>>();
           paramCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           paramCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           paramCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
           SQLHandler sageSql = new SQLHandler();
            List<ImageGalleryItemsInfo> itemsInfoList = sageSql.ExecuteAsList<ImageGalleryItemsInfo>("[dbo].[usp_Aspx_GalleryItemsInfo]", paramCollection);
           return itemsInfoList;
       }

       public static List<ImageGalleryItemsInfo> GetItemsImageGalleryList(int storeID, int portalID, string userName, string culture)
       {  
           List<KeyValuePair<string, object>> paramCollection = new List<KeyValuePair<string, object>>();
           paramCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           paramCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           paramCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
           paramCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
           SQLHandler sageSql = new SQLHandler();
           List<ImageGalleryItemsInfo> itemsInfoList = sageSql.ExecuteAsList<ImageGalleryItemsInfo>("[dbo].[usp_Aspx_ItemsImageGalleryInfo]", paramCollection);
           return itemsInfoList;
       }

       public static List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, string combinationId)
       {    
           SQLHandler sageSql = new SQLHandler();
           List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
           paramCol.Add(new KeyValuePair<string, object>("@ItemSKU", itemSKU));
           paramCol.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           paramCol.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           paramCol.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           paramCol.Add(new KeyValuePair<string, object>("@CostVariantConfigID", combinationId));
            List<ItemsInfoSettings> itemsInfoContainer = sageSql.ExecuteAsList<ItemsInfoSettings>("[dbo].[usp_Aspx_GetImageInformationsBySKU]", paramCol);
           return itemsInfoContainer;
       }

       public static List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemID(int itemID, AspxCommonInfo aspxCommonObj)
       {
           SQLHandler sageSql = new SQLHandler();
           List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
           paramCol.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           paramCol.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           paramCol.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           paramCol.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           List<ItemsInfoSettings> itemsInfoContainer = sageSql.ExecuteAsList<ItemsInfoSettings>("[dbo].[usp_Aspx_GetImageInformationsByItemID]", paramCol);
           return itemsInfoContainer;
       }
    }
}
