using System;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.AdminNotification;
using System.Collections.Generic;

/// <summary>
/// Summary description for AdminNotificationWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AdminNotificationWebService : System.Web.Services.WebService
{

    [WebMethod]
    public void NotificationSaveUpdateSettings(NotificationSettingsInfo saveUpdateInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AdminNotificationController.NotificationSaveUpdateSettings(saveUpdateInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    [WebMethod]
    public NotificationSettingsInfo NotificationSettingsGetAll(AspxCommonInfo aspxCommonObj)
    {
        try
        {

            NotificationSettingsInfo listInfo = AdminNotificationController.NotificationSettingsGetAll(aspxCommonObj);
            return listInfo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    [WebMethod]
    public NotificationGetAllInfo NotificationGetAll(AspxCommonInfo aspxCommonObj)
    {
        try
        {

            NotificationGetAllInfo listInfo = AdminNotificationController.NotificationGetAll(aspxCommonObj.StoreID, aspxCommonObj.PortalID);
            return listInfo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
       [WebMethod]
    public List<SubscriptionInfo> NotificationUsersGetAll(AspxCommonInfo aspxCommonObj)
    {
        try
        {

            List<SubscriptionInfo> listInfo = AdminNotificationController.NotificationUsersGetAll(aspxCommonObj);
            return listInfo;
        }
        catch (Exception e)
        {
            throw e;
        }

    }

       [WebMethod]
       public List<OutOfStockInfo> NotificationItemsGetAll(AspxCommonInfo aspxCommonObj)
       {
           try
           {

               List<OutOfStockInfo> listInfo = AdminNotificationController.NotificationItemsGetAll(aspxCommonObj);
               return listInfo;
           }
           catch (Exception e)
           {
               throw e;
           }

       }
      [WebMethod]
       public List<NotificationOrdersInfo> NotificationOrdersGetAll(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<NotificationOrdersInfo> listInfo = AdminNotificationController.NotificationOrdersGetAll(aspxCommonObj);
               return listInfo;
           }
           catch (Exception e)
           {
               throw e;
           }
       }



}

