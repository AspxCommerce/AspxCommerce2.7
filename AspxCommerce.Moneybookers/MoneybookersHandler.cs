using System;
using System.Collections.Generic;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Moneybookers
{
    public class MoneybookersHandler
    {
        public static void ParseIPN(int orderID, string transID, string status, int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            MoneybookersHandler ph = new MoneybookersHandler();
            try
            {
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                // WcfSession ws = new WcfSession();
                odinfo.OrderID = orderID;//ws.GetSessionVariable("OrderID");
                odinfo.ResponseReasonText = status;
                odinfo.TransactionID = transID;
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                objad.UpdateOrderDetails(ot);
                // UpdateItemQuantity
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateItemQuantity(string itemIds, string coupon, int storeId, int portalId, string userName)
        {
            try
            {
                string[] ids = itemIds.Split(',');
                //id,quantity,isdownloadable
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Contains("&"))
                    {
                        string[] itemdetails = ids[i].Split('&');
                        string[] coupondetails = coupon.Split('&');
                        if (itemdetails[0] != null)
                        {
                            var paraMeter = new List<KeyValuePair<string, object>>();
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                            paraMeter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                            paraMeter.Add(new KeyValuePair<string, object>("@ItemID", itemdetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@Quantity", itemdetails[1]));
                            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", itemdetails[2]));
                            paraMeter.Add(new KeyValuePair<string, object>("@CostVariantsIDs", itemdetails[3]));
                            var sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", paraMeter);
                        }
                        if (coupondetails[0] != null && coupondetails[1] != null)
                        {
                            var paraMeter = new List<KeyValuePair<string, object>>();
                            paraMeter.Add(new KeyValuePair<string, object>("@CouponCode", coupondetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                            paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
                            paraMeter.Add(new KeyValuePair<string, object>("@CouponUsedCount", coupondetails[1]));
                            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", itemdetails[2]));
                            var sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", paraMeter);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

