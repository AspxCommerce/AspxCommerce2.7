using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;
using System.Data;

namespace AspxCommerce.AdminNotification
{
    public class AdminNotificationProvider
    {

        public static void NotificationSaveUpdateSettings(NotificationSettingsInfo saveUpdateInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AllActive", saveUpdateInfo.AllActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserNotificationActive", saveUpdateInfo.UserNotificationActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserNotificationCount", saveUpdateInfo.UserNotificationCount));
                parameterCollection.Add(new KeyValuePair<string, object>("@SubscriptionNotificationActive ", saveUpdateInfo.SubscriptionNotificationActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@SubscriptionNotificationCount", saveUpdateInfo.SubscriptionNotificationCount));
                parameterCollection.Add(new KeyValuePair<string, object>("@OutofStockNotificationActive", saveUpdateInfo.OutofStockNotificationActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@OutofStockNotificationCount", saveUpdateInfo.OutofStockNotificationCount));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemsLowStockNotificationActive", saveUpdateInfo.ItemsLowStockNotificationActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemsLowStockCount", saveUpdateInfo.ItemsLowStockCount));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrdersNotificationAtive", saveUpdateInfo.OrdersNotificationAtive));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrdersNotificationCount", saveUpdateInfo.OrdersNotificationCount));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_NotificationSaveUpdateSettings]", parameterCollection);

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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                NotificationSettingsInfo listInfo = sqlH.ExecuteAsObject<NotificationSettingsInfo>("[dbo].[usp_Aspx_NotificationSettingsGetAll]", parameterCollection);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static NotificationGetAllInfo NotificationGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                NotificationGetAllInfo listInfo = sqlH.ExecuteAsObject<NotificationGetAllInfo>("[dbo].[usp_Aspx_NotificationGetAll]", parameterCollection);
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);                 
                SQLHandler sqLH = new SQLHandler();
                DataSet ds = sqLH.ExecuteAsDataSet("[dbo].[usp_Aspx_NotificationUsersGetAll]",parameterCollection);
                List<SubscriptionInfo> SubscriptionDetails = new List<SubscriptionInfo>();
                if (ds.Tables.Count > 0)
                {
                    SubscriptionDetails = DataSourceHelper.FillCollection<SubscriptionInfo>(ds.Tables[0]);    
                }                
                return SubscriptionDetails;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public static List<OutOfStockInfo> NotificationitemsGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                NotificationItemList ih = new NotificationItemList();
                SQLHandler sqLH = new SQLHandler();
                DataSet ds = sqLH.ExecuteAsDataSet("[dbo].[usp_Aspx_NotificationItemsGetAll]",parameterCollection);
                List<OutOfStockInfo> OutOfStockDetails = new List<OutOfStockInfo>();
                if (ds.Tables.Count > 0)
                {
                   OutOfStockDetails = DataSourceHelper.FillCollection<OutOfStockInfo>(ds.Tables[0]);   
                }                
                return OutOfStockDetails;
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<NotificationOrdersInfo> listInfo = sqlH.ExecuteAsList<NotificationOrdersInfo>("[dbo].[usp_Aspx_NotificationOrdersGetAll]", parameterCollection);
                return listInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
