using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.AdminNotification;
using Microsoft.AspNet.SignalR;
using AspxCommerce.Core;

namespace AspxCommerce.RealTimeCartManagement
{
    public class RealTimeHelper
    {
        public RealTimeHelper()
        {
        }

        public static void UpdateAdminNotifications(int StoreID, int PortalID)
        {
            try
            {
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = StoreID;
                aspxCommonObj.PortalID = PortalID;
                NotificationGetAllInfo listInfo = AdminNotificationController.NotificationGetAll(aspxCommonObj.StoreID, aspxCommonObj.PortalID);
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<RealTimeHub>();
                hubContext.Clients.All.NotificationGetAllSuccess(listInfo);

            }
            catch (Exception)
            {
                //TODO
            }
        }

    }
}
