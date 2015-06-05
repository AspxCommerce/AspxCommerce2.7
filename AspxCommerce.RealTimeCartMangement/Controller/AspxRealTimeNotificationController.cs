using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AspxCommerce.Core;


namespace Aspxcommerce.RealTimeCartManagement
{
    public class AspxRealTimeNotificationController
    {
        public static List<OnlineUsersInfo> GetOnlineUserList(AspxCommonInfo aspxCommonObj, string sessionID)
        {
            try
            {
                List<OnlineUsersInfo> lstUsers = AspxRealTimeNotificationProvider.GetOnlineUserList(aspxCommonObj, sessionID);
                return lstUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AddNewUser(RealTimeNotificationInfo realTimeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRealTimeNotificationProvider.AddNewUser(realTimeObj, aspxCommonObj);
             
              
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void RemoveUser(int portalID, string connectionID)
        {
            try
            {
                AspxRealTimeNotificationProvider.RemoveUser(portalID, connectionID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RegisterHubConnection(string connectionId, AspxCommonInfo aspxCommonObj)
        {
            var RealTimeNotificationInfo = new RealTimeNotificationInfo
            {
                ConnectionId = connectionId,
                SessionId = HttpContext.Current.Session.SessionID
            };
            AddNewUser(RealTimeNotificationInfo, aspxCommonObj);

        }

       
    }

}