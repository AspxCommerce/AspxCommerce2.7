using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace Aspxcommerce.RealTimeCartManagement
{
    public class AspxRealTimeNotificationProvider
    {
        public AspxRealTimeNotificationProvider()
        {
        }
       
        public static List<OnlineUsersInfo> GetOnlineUserList(AspxCommonInfo aspxCommonObj, string sessionID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@SessionCode", sessionID));
                SQLHandler sqlH = new SQLHandler();
                List<OnlineUsersInfo> lstUsers = sqlH.ExecuteAsList<OnlineUsersInfo>("[dbo].[Aspx_GetOnlineUsers]", parameterCollection);
                return lstUsers;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static void AddNewUser(RealTimeNotificationInfo realTimeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ConnectionID", realTimeObj.ConnectionId));
                parameterCollection.Add(new KeyValuePair<string, object>("@SessionCode", realTimeObj.SessionId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[Aspx_AddOnlineUser]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void RemoveUser(int portalID, string connectionID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", 1));
                parameterCollection.Add(new KeyValuePair<string, object>("@ConnectionID", connectionID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[Aspx_RemoveOnlineUser]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
