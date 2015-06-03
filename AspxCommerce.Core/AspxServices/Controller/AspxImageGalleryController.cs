using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxImageGalleryController
    {
       public AspxImageGalleryController()
       {
       }
       public static void SaveGallerySettings(string settingKeys, string settingsValues, string userModuleID, string portalID, string culture)
       {
           try
           {
               AspxImageGalleryProvider.SaveGallerySettings(settingKeys, settingsValues, userModuleID, portalID, culture);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static ImageGalleryInfo GetGallerySettingValues(int userModuleID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               ImageGalleryInfo infoObject = AspxImageGalleryProvider.GetGallerySettingValues(userModuleID, aspxCommonObj);
               return infoObject;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ImageGalleryItemsInfo> GetItemInfoList(int storeID, int portalID, string culture)
       {
           List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryProvider.GetItemInfoList(storeID, portalID, culture);
           return itemsInfoList;
       }

       public static List<ImageGalleryItemsInfo> GetItemsImageGalleryList(int storeID, int portalID, string userName, string culture)
       {
           List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryProvider.GetItemsImageGalleryList(storeID, portalID, userName, culture);
           return itemsInfoList;
       }

       public static List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, string combinationId)
       {
           List<ItemsInfoSettings> itemsInfoContainer = AspxImageGalleryProvider.GetItemsImageGalleryInfoByItemSKU(itemSKU, aspxCommonObj, combinationId);
           return itemsInfoContainer;
       }

       public static List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemID(int itemID, AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfoSettings> itemsInfoContainer = AspxImageGalleryProvider.GetItemsImageGalleryInfoByItemID(itemID, aspxCommonObj);
           return itemsInfoContainer;
       }
    }
}
