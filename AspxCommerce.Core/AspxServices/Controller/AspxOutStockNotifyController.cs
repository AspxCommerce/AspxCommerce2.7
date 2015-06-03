using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxOutStockNotifyController
    {
       public AspxOutStockNotifyController()
       {
       }

       public static List<Notification> GetNotificationList(int offset, int limit, GetAllNotificationInfo getAllNotificationObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<Notification> lstNotification = AspxOutStockNotifyProvider.GetNotificationList(offset, limit, getAllNotificationObj, aspxCommonObj);
               return lstNotification;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteNotification(string notificationID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxOutStockNotifyProvider.DeleteNotification(notificationID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<NotifictionMail> GetEmail(string SKU, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<NotifictionMail> lstNotifMail = AspxOutStockNotifyProvider.GetEmail(SKU, aspxCommonObj);
               return lstNotifMail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void SendEmailNotification(SendEmailInfo emailInfo, string VariantId, string VarinatValue, string sku, string ProductUrl, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxOutStockNotifyProvider.SendEmailNotification(emailInfo, VariantId, VarinatValue, sku, ProductUrl, aspxCommonObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void InsertNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo insertNotificationObj)
       {
           AspxOutStockNotifyProvider.InsertNotification(aspxCommonObj, insertNotificationObj);
       }

       public static List<Notification> GetAllNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo getNotificationObj)
       {
           List<Notification> lstNotification = AspxOutStockNotifyProvider.GetAllNotification(aspxCommonObj, getNotificationObj);
           return lstNotification;
       }
    }
}
