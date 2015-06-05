using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using AspxCommerce.Core;
using System.Collections;
using AspxCommerce.AdminNotification;
using System.Web;
using SageFrame.Shared;
using SageFrame.Common;
using System.Text.RegularExpressions;
using SageFrame.Framework;
using Aspxcommerce.RealTimeCartManagement;

namespace AspxCommerce.RealTimeCartManagement
{
    public static class UserHandler //this static class is to store the number of users conected at the same time
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }


    public class TestHub : Hub
    {
        public TestHub()
        {
        }
        public void Hello(int test)
        {
            Clients.All.callit();
        }
    }
    [HubName("_aspxrthub")]
    public class RealTimeHub : Hub
    {

        #region Admin Notifications

        public void NotificationUsersGetAll(int StoreID, int PortalID, int Type)
        {
            try
            {
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = StoreID;
                aspxCommonObj.PortalID = PortalID;

                NotificationGetAllInfo listInfo = new NotificationGetAllInfo();

                switch (Type)
                {
                    case 1:
                        List<OutOfStockInfo> ni = AdminNotificationController.NotificationItemsGetAll(aspxCommonObj);
                        listInfo.ItemsInfoCount = ni.Count;
                        break;
                    case 2:
                        List<NotificationOrdersInfo> no = AdminNotificationController.NotificationOrdersGetAll(aspxCommonObj);
                        listInfo.NewOrdersCount = no.Count;
                        break;
                    case 3:
                        List<SubscriptionInfo> nu = AdminNotificationController.NotificationUsersGetAll(aspxCommonObj);
                        listInfo.UsersInfoCount = nu.Count;
                        break;
                    case 4:
                        listInfo = AdminNotificationController.NotificationGetAll(StoreID, PortalID); ;
                        break;
                    default:
                        listInfo = AdminNotificationController.NotificationGetAll(StoreID, PortalID); ;
                        break;
                }

                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext("_aspxrthub");
                hubContext.Clients.Group("aspx_rt_users").NotificationGetAllSuccess(listInfo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Cart Management
        public void CheckIfItemOutOfStock(int itemID, string SKU, string costVariantsValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, costVariantsValueIDs, aspxCommonObj);
                var LastUpdatedBy = Context.ConnectionId;
                Clients.Group("aspx_rt_users").BindOutOfStock(retValue, itemID, SKU);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemToStock(int itemID, string itemSKU)
        {
            try
            {
                var LastUpdatedBy = Context.ConnectionId;
                Clients.Group("aspx_rt_users").BindAddToCart(itemID, itemSKU);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemStockByAdmin(int itemID, string itemSKU, string costVariantsValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, costVariantsValueIDs, aspxCommonObj);
                if (retValue == false)
                {
                    Clients.Group("aspx_rt_users").BindAddToCart(itemID, itemSKU);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemStockFromItemDetails(List<GroupProductCartReturnInfo> itemCartObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                if ((itemCartObj != null) && (itemCartObj.Count > 0))
                {
                    foreach (GroupProductCartReturnInfo itemInfo in itemCartObj)
                    {
                        int itemID = Convert.ToInt32(itemInfo.CartItemIDs);
                        string itemSKU = itemInfo.CartItemSkus;
                        int retVal = Convert.ToInt32(itemInfo.CartItemReturnVals);
                        bool retValue;
                        if (retVal == 1)
                        {
                            continue;
                        }
                        else if (retVal == 2)
                        {
                            retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, string.Empty, aspxCommonObj);
                            Clients.Group("aspx_rt_users").BindOutOfStock(retValue, itemID, itemSKU);

                        }
                        else
                        {
                            retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, string.Empty, aspxCommonObj);
                            Clients.Group("aspx_rt_users").BindOutOfStock(retValue, itemID, itemSKU);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CheckIfItemOutOfStockFromCart(List<GroupProductCartReturnInfo> itemCartObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                if ((itemCartObj != null) && (itemCartObj.Count > 0))
                {
                    foreach (GroupProductCartReturnInfo itemInfo in itemCartObj)
                    {
                        int itemID = Convert.ToInt32(itemInfo.CartItemIDs);
                        string itemSKU = itemInfo.CartItemSkus;
                        bool retValue;
                        retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, string.Empty, aspxCommonObj);
                        Clients.Group("aspx_rt_users").BindOutOfStock(retValue, itemID, itemSKU);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public override Task OnConnected()
        {
            string sessionAspxCommonInfoCookie = Context.RequestCookies["session_aspxCommonInfo_cookie"].Value;
            string connectionId = Context.ConnectionId;
            string[] cookieValues = sessionAspxCommonInfoCookie.Split('-');

            if (cookieValues.Count() > 1)
            {

                int tempInt = 0;
                AspxCommonInfo aspxCommonInfo = new AspxCommonInfo();
                aspxCommonInfo.StoreID = Int32.TryParse(cookieValues[1], out tempInt) ? tempInt : 0;
                aspxCommonInfo.PortalID = Int32.TryParse(cookieValues[2], out tempInt) ? tempInt : 0;
                aspxCommonInfo.UserName = cookieValues[3];
                int connectedUsers = Aspxcommerce.RealTimeCartManagement.AspxRealTimeNotificationController.GetOnlineUserList(aspxCommonInfo, cookieValues[0]).Count;
                if (connectedUsers < 200)
                {
                    var realTimeNotificationInfo = new RealTimeNotificationInfo
                    {
                        ConnectionId = connectionId,
                        SessionId = cookieValues[0]
                    };

                    Aspxcommerce.RealTimeCartManagement.AspxRealTimeNotificationController.AddNewUser(realTimeNotificationInfo, aspxCommonInfo);
                    UserHandler.ConnectedIds.Add(connectionId);
                    base.Groups.Add(connectionId, "aspx_rt_users");
                    return base.OnConnected();
                }
            }
                
            Clients.All.MaxConnectionTimeOut(true);
            return base.OnConnected();
           
        }

        public override Task OnDisconnected(bool test)
        {
            int portalID = GetPortalID;
            string connectionId = Context.ConnectionId;
            UserHandler.ConnectedIds.Remove(connectionId);
            //Clients.All.usersConnected(UserHandler.ConnectedIds.Count());
            base.Groups.Remove(connectionId, "aspx_rt_users");
            Aspxcommerce.RealTimeCartManagement.AspxRealTimeNotificationController.RemoveUser(portalID, connectionId);
            return base.OnDisconnected(true);
        }
        private string GetUserName()
        {

            try
            {
                SecurityPolicy sp = new SecurityPolicy();
                string userName = sp.GetUser(GetPortalID);
                if (userName != ApplicationKeys.anonymousUser)
                {
                    return userName;
                }
                else
                {
                    return ApplicationKeys.anonymousUser;
                }
            }
            catch
            {
                return ApplicationKeys.anonymousUser;
            }

        }
        public int GetPortalID
        {
            get
            {
                try
                {
                    Hashtable hstPortals = GetPortals();
                    string URL = HttpContext.Current.Request.Url.ToString();
                    if (URL.Contains("/portal/"))
                    {
                        var RegMatch = Regex.Matches(URL, @"^http[s]?\s*:.*\/portal\/([^/]+)+\/.*");
                        string PortalName = "";
                        foreach (Match match in RegMatch)
                        {
                            PortalName = match.Groups[1].Value;
                        }
                        int PortalID = Int32.Parse(hstPortals[PortalName].ToString());
                        return PortalID;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }
        public Hashtable GetPortals()
        {
            Hashtable hstAll = new Hashtable();
            if (HttpRuntime.Cache[CacheKeys.Portals] != null)
            {
                hstAll = (Hashtable)HttpRuntime.Cache[CacheKeys.Portals];
            }
            else
            {
                SettingProvider objSP = new SettingProvider();
                List<SagePortals> sagePortals = objSP.PortalGetList();
                foreach (SagePortals portal in sagePortals)
                {
                    hstAll.Add(portal.SEOName.ToLower().Trim(), portal.PortalID);
                }
            }
            HttpRuntime.Cache.Insert(CacheKeys.Portals, hstAll);
            return hstAll;
        }

    }
}