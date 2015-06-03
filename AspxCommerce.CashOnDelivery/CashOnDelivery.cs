using System;
using System.Collections.Generic;
using System.Web;
using AspxCommerce.Core.Mobile;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;

namespace AspxCommerce.CashOnDelivery
{
    public class CashOnDelivery
    {
        public static string Parse(string transId, string invoice, string POrderno, int responseCode, int responsereasonCode, string responsetext, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                var ot = new OrderDetailsCollection();
                var odinfo = new OrderDetailsInfo();
                var cms = new CartManageSQLProvider();
                var cf = new CommonInfo {StoreID = aspxCommonObj.StoreID, PortalID = aspxCommonObj.PortalID, AddedBy = aspxCommonObj.UserName};
                // UpdateOrderDetails
              
                odinfo.OrderID =int.Parse(HttpContext.Current.Session["OrderID"].ToString());
                odinfo.TransactionID = odinfo.ResponseCode.ToString(transId);
                odinfo.InvoiceNumber = Convert.ToString(invoice);
                odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
                odinfo.ResponseCode = Convert.ToInt32(responseCode);
                odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
                odinfo.ResponseReasonText = Convert.ToString(responsetext);
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                AspxOrderController.UpdateOrderDetails(ot);
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    var orderdata2 =  (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    AspxOrderController.UpdateItemQuantity(orderdata2);
                }
                HttpContext.Current.Session.Remove("OrderID");
                cms.ClearCartAfterPayment(aspxCommonObj);
                return "This transaction has been approved";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string ParseForMobile(string transId, OrderInfo orderInfo, string POrderno, int responseCode, int responsereasonCode, string responsetext)
        {
            try
            {
                var ot = new OrderDetailsCollection();
                var odinfo = new OrderDetailsInfo();
                var cms = new CartManageSQLProvider();
                var cf = new CommonInfo
                             {StoreID = orderInfo.StoreId, PortalID = orderInfo.PortalId, AddedBy = orderInfo.AddedBy};
                // UpdateOrderDetails
              
                odinfo.OrderID = orderInfo.OrderId;
                odinfo.TransactionID = odinfo.ResponseCode.ToString(transId);
                odinfo.InvoiceNumber = orderInfo.InvoiceNumber;
                odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
                odinfo.ResponseCode = Convert.ToInt32(responseCode);
                odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
                odinfo.ResponseReasonText = Convert.ToString(responsetext);
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                AspxOrderController.UpdateOrderDetails(ot);
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.CustomerID = orderInfo.CustomerId;
                aspxCommonObj.SessionCode = orderInfo.SessionCode;
                aspxCommonObj.StoreID = orderInfo.StoreId;
                aspxCommonObj.PortalID = orderInfo.PortalId;
                cms.ClearCartAfterPayment(aspxCommonObj);
                return "This transaction has been approved";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// update the quantity of items after order and also update the coupon code 
        /// that used on that order
        /// 
        /// </summary>
        /// /// <param name="orderInfo">info of order </param>
        /// <param name="itemsInfo">collection of items </param>
        /// <param name="coupon">and coupon code in coponCode#codeapplided  as single string</param>
        /// <param name="storeId">id of the store</param>
        /// <param name="portalId">id of the portal</param>
        /// <param name="userName">userName of customer</param>
        public static void UpdateItemQuantityAndCoupon(OrderInfo orderInfo, List<OrderItem> itemsInfo, List<CouponSession> coupons, int storeId, int portalId, string userName)
        {
            try
            {
                string error = "";
                try
                {

                    foreach (var coupon in coupons)
                    {
                        var paraMeter = new List<KeyValuePair<string, object>>
                                                {
                                                    new KeyValuePair<string, object>("@CouponCode",coupon.Key),
                                                    new KeyValuePair<string, object>("@StoreID", storeId),
                                                    new KeyValuePair<string, object>("@PortalID", portalId),
                                                    new KeyValuePair<string, object>("@UserName", userName),
                                                    new KeyValuePair<string, object>("@CouponUsedCount",coupon.AppliedCount),
                                                    new KeyValuePair<string, object>("@OrderID", orderInfo.OrderId)

                                                };
                        var sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", paraMeter);
                    }

                }
                catch (Exception ex)
                {
                    error = ex.InnerException.ToString();
                }


                foreach (var item in itemsInfo)
                {
                    if (item.IsDownloadable == false || item.IsGiftCard == false)
                    {
                        var paraMeter = new List<KeyValuePair<string, object>>
                                            {
                                                new KeyValuePair<string, object>("@StoreID", storeId),
                                                new KeyValuePair<string, object>("@PortalID", portalId),
                                                new KeyValuePair<string, object>("@AddedBy", userName),
                                                new KeyValuePair<string, object>("@ItemID", item.ItemId),
                                                new KeyValuePair<string, object>("@Quantity", item.Quantity),
                                                new KeyValuePair<string, object>("@OrderID", orderInfo.OrderId),
                                                new KeyValuePair<string, object>("@CostVariantsIDs", item.CostVariantIds)
                                            };
                        var sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", paraMeter);
                        if (!string.IsNullOrEmpty(error))
                        {
                            throw new Exception(error);
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
