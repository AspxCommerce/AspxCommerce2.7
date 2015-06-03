using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxOutStockNotifyProvider
    {
       public AspxOutStockNotifyProvider()
       {
       }

       public static List<Notification> GetNotificationList(int offset, int limit, GetAllNotificationInfo getAllNotificationObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
               ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
               ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", getAllNotificationObj.ItemSKU));
               ParaMeter.Add(new KeyValuePair<string, object>("@mailStatus", getAllNotificationObj.MailStatus));
               ParaMeter.Add(new KeyValuePair<string, object>("@itemStatus", getAllNotificationObj.ItemStatus));
               ParaMeter.Add(new KeyValuePair<string, object>("@customer", getAllNotificationObj.Customer));
               SQLHandler sqLH = new SQLHandler();
               List<Notification> lstNotification= sqLH.ExecuteAsList<Notification>("usp_Aspx_GetNotificationList", ParaMeter);
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
               List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               ParaMeter.Add(new KeyValuePair<string, object>("@notificationID", notificationID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteNotification", ParaMeter);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@SKU", SKU));
               SQLHandler sqlH = new SQLHandler();
               List<NotifictionMail> lstNotifMail= sqlH.ExecuteAsList<NotifictionMail>("usp_Aspx_GetNotificationByItemSKU", parameterCollection);
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
               EmailTemplate.SendOutOfNotification(aspxCommonObj, emailInfo, VariantId, VarinatValue, sku, ProductUrl);
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@itemSKU", sku));
               parameter.Add(new KeyValuePair<string, object>("@VariantID", VariantId));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_UpdateNotification", parameter);

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void InsertNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo insertNotificationObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@itemId", insertNotificationObj.ItemID));
           parameter.Add(new KeyValuePair<string, object>("@costVariantValuesIds", insertNotificationObj.VariantID));
           parameter.Add(new KeyValuePair<string, object>("@itemSKU", insertNotificationObj.ItemSKU));
           parameter.Add(new KeyValuePair<string, object>("@customer", aspxCommonObj.UserName));
           parameter.Add(new KeyValuePair<string, object>("@email", insertNotificationObj.Email));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_InsertNotification", parameter);
       }

       public static List<Notification> GetAllNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo getNotificationObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@itemId", getNotificationObj.ItemID));
           parameter.Add(new KeyValuePair<string, object>("@costVariantValuesIds", getNotificationObj.VariantID));
           parameter.Add(new KeyValuePair<string, object>("@ItemSKU", getNotificationObj.ItemSKU));
           parameter.Add(new KeyValuePair<string, object>("@UserEmail", getNotificationObj.Email));
           SQLHandler sqlH = new SQLHandler();
           List<Notification> lstNotification= sqlH.ExecuteAsList<Notification>("usp_Aspx_GetAllNotification", parameter);
           return lstNotification;
       }
    }
}
