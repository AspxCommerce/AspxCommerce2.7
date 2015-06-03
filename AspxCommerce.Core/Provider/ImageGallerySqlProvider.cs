/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class ImageGallerySqlProvider
    {
        /// <summary>
        /// Saves the Image Gallery keys Settings values
        /// </summary>
        /// <param name="settingKeys"></param>
        /// <param name="settingsValues"></param>
        /// <param name="portalID"></param>
        /// <param name="userModuleID"></param>
        /// <param name="portalID"></param>
        /// <param name="culture"></param>
        public void SaveGallerySettings(string settingKeys, string settingsValues, string userModuleID, string portalID, string culture)
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


        public ImageGalleryInfo GetGallerySettingValues(int userModuleID, AspxCommonInfo aspxCommonObj)
        {
            ImageGalleryInfo infoObject;
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            try
            {
                parameterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Culture", aspxCommonObj.CultureName));
                SQLHandler sagesql = new SQLHandler();
                infoObject = sagesql.ExecuteAsObject<ImageGalleryInfo>("[dbo].[usp_Aspx_GetGallerySettings]", parameterCollection);
                return infoObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ImageGalleryItemsInfo> GetItemInfoList(int storeID, int portalID, string culture)
        {
            List<ImageGalleryItemsInfo> itemsInfoList;
            List<KeyValuePair<string, object>> paramCollection = new List<KeyValuePair<string, object>>();
            paramCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
            SQLHandler sageSql = new SQLHandler();
            itemsInfoList = sageSql.ExecuteAsList<ImageGalleryItemsInfo>("[dbo].[usp_Aspx_GalleryItemsInfo]", paramCollection);
            return itemsInfoList;
        }

        public List<ImageGalleryItemsInfo> GetItemsImageGalleryList(int storeID, int portalID, string userName, string culture)
        {
            List<ImageGalleryItemsInfo> itemsInfoList;
            List<KeyValuePair<string, object>> paramCollection = new List<KeyValuePair<string, object>>();
            paramCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            paramCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
            SQLHandler sageSql = new SQLHandler();
            itemsInfoList = sageSql.ExecuteAsList<ImageGalleryItemsInfo>("[dbo].[usp_Aspx_ItemsImageGalleryInfo]", paramCollection);
            return itemsInfoList;
        }

        public List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, string combinationId)
        {
            List<ItemsInfoSettings> itemsInfoContainer;
            SQLHandler sageSql = new SQLHandler();
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@ItemSKU", itemSKU));
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramCol.Add(new KeyValuePair<string, object>("@CostVariantConfigID", combinationId));
            itemsInfoContainer = sageSql.ExecuteAsList<ItemsInfoSettings>("[dbo].[usp_Aspx_GetImageInformationsBySKU]", paramCol);
            return itemsInfoContainer;
        }

        public List<ItemsInfoSettings> GetItemsImageGalleryInfoByItemID(int itemID)
        {
            List<ItemsInfoSettings> itemsInfoContainer;
            SQLHandler sageSql = new SQLHandler();
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            itemsInfoContainer = sageSql.ExecuteAsList<ItemsInfoSettings>("[dbo].[usp_Aspx_GetImageInformationsByItemID]", paramCol);
            return itemsInfoContainer;
        }
    }
}
