using System;
using System.Collections.Generic;
using AspxCommerce.Core;

namespace AspxCommerce.AdminNotification
{
    public class AdminNotificationController
    {
        public AdminNotificationController()
        {

        }
        public static void NotificationSaveUpdateSettings(NotificationSettingsInfo saveUpdateInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AdminNotificationProvider.NotificationSaveUpdateSettings(saveUpdateInfo, aspxCommonObj);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static NotificationSettingsInfo NotificationSettingsGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {

                NotificationSettingsInfo listInfo = AdminNotificationProvider.NotificationSettingsGetAll(aspxCommonObj);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static NotificationGetAllInfo NotificationGetAll(int StoreID, int PortalID)
        {
            try
            {
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = StoreID;
                aspxCommonObj.PortalID = PortalID;

                NotificationGetAllInfo listInfo = AdminNotificationProvider.NotificationGetAll(aspxCommonObj);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<SubscriptionInfo> NotificationUsersGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<SubscriptionInfo> listInfo = AdminNotificationProvider.NotificationUsersGetAll(aspxCommonObj);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public static List<OutOfStockInfo> NotificationItemsGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OutOfStockInfo> listInfo = AdminNotificationProvider.NotificationitemsGetAll(aspxCommonObj);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static List<NotificationOrdersInfo> NotificationOrdersGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<NotificationOrdersInfo> listInfo = AdminNotificationProvider.NotificationOrdersGetAll(aspxCommonObj);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
