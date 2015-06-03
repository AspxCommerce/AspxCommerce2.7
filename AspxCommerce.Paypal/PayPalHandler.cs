using System;
using System.Collections.Generic;
using System.Web;
using AspxCommerce.Core;
using AspxCommerce.Core.Mobile;
using SageFrame.Core;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Text;

namespace AspxCommerce.PayPal
{
    public class PayPalHandler
    {
       
        public void ParseIPN(int orderId, string transId, string status, int storeId, int portalId, string userName, int customerId, string sessionCode)
        {
            var ph = new PayPalHandler();
            try
            {

                var ot = new OrderDetailsCollection();
                var odinfo = new OrderDetailsInfo();
                var cms = new CartManageSQLProvider();
                var cf = new CommonInfo {StoreID = storeId, PortalID = portalId, AddedBy = userName};
                // UpdateOrderDetails
            
                var sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                // WcfSession ws = new WcfSession();
                odinfo.OrderID = orderId;//ws.GetSessionVariable("OrderID");
                odinfo.ResponseReasonText = status;
                odinfo.TransactionID = transId;
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                AspxOrderController.UpdateOrderDetails(ot);

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ParseAfterIPN(string postData, AspxCommonInfo aspxCommonObj, string templateName, string addressPath)
        {
            var ph = new PayPalHandler();
            string transId = string.Empty;
            string orderStatus = string.Empty;

            try
            {
                //split response into string array using whitespace delimeter
                String[] stringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */
                var ot = new OrderDetailsCollection();
                var odinfo = new OrderDetailsInfo();
                var cms = new CartManageSQLProvider();
                var cf = new CommonInfo {StoreID = aspxCommonObj.StoreID, PortalID = aspxCommonObj.PortalID, AddedBy = aspxCommonObj.UserName};
                // UpdateOrderDetails
            
                var sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
             
                int i;
                for (i = 1; i < stringArray.Length - 1; i++)
                {
                    String[] stringArray1 = stringArray[i].Split('=');

                    String sKey = stringArray1[0];
                    String sValue = HttpUtility.UrlDecode(stringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            odinfo.ResponseReasonText = Convert.ToString(sValue);
                            orderStatus = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            odinfo.TransactionID = Convert.ToString(sValue);
                            transId = Convert.ToString(sValue);
                            break;

                    }
                }
             
                ot.ObjCommonInfo = cf;
                //odinfo.OrderStatusID = 8;
                //objad.UpdateOrderDetails(ot);
                if (odinfo.ResponseReasonText.ToLower().Trim() == "completed")
                {
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {
                        var orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                        AspxOrderController.UpdateItemQuantity(orderdata2);
                    }
                }
                cms.ClearCartAfterPayment(aspxCommonObj);

                //invoice  transID
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    var orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    orderdata.ObjOrderDetails.OrderStatus = orderStatus;
                    EmailTemplate.SendEmailForOrder(aspxCommonObj.PortalID, orderdata, addressPath, templateName, transId);
                }
                HttpContext.Current.Session.Remove("OrderCollection");
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ParseAfterIPNMobile(OrderInfo orderInfo,List<CouponSession> coupons,List<OrderItem> itemsInfo,string postData,UserAddressInfo billingAddress,UserAddressInfo shippingAddress, string templateName, string addressPath)
        {

            string transId = string.Empty;

            try
            {

                String[] stringArray = postData.Split('\n');
                var ot = new OrderDetailsCollection();
                var odinfo = new OrderDetailsInfo();
                var cms = new CartManageSQLProvider();
                var cf = new CommonInfo
                             {StoreID = orderInfo.StoreId, PortalID = orderInfo.PortalId, AddedBy = orderInfo.AddedBy};
                // UpdateOrderDetails
            
                var sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter

                int i;
                for (i = 1; i < stringArray.Length - 1; i++)
                {
                    String[] stringArray1 = stringArray[i].Split('=');

                    String sKey = stringArray1[0];
                    String sValue = HttpUtility.UrlDecode(stringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            odinfo.ResponseReasonText = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            odinfo.TransactionID = Convert.ToString(sValue);
                            transId = Convert.ToString(sValue);
                            break;

                    }
                }

                ot.ObjCommonInfo = cf;
                //odinfo.OrderStatusID = 8; var orderInfo = (OrderInfo) HttpContext.Current.Session["OrderCollection"];

                UpdateItemQuantityAndCoupon(orderInfo, itemsInfo, coupons, orderInfo.StoreId, orderInfo.PortalId,
                                            orderInfo.AddedBy);
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.CustomerID = orderInfo.CustomerId;
                aspxCommonObj.SessionCode = orderInfo.SessionCode;
                aspxCommonObj.StoreID = orderInfo.StoreId;
                aspxCommonObj.PortalID = orderInfo.PortalId;
                cms.ClearCartAfterPayment(aspxCommonObj);
                EmailTemplate.SendEmailForOrderMobile(orderInfo, billingAddress, shippingAddress, addressPath,
                                                      templateName, transId);


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
        

        public void SaveErrorLog(string postData)
        {
            var ph = new PayPalHandler();

            try
            {
                //split response into string array using whitespace delimeter
                String[] stringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */

                int i;
                for (i = 1; i < stringArray.Length - 1; i++)
                {
                    String[] stringArray1 = stringArray[i].Split('=');

                    String sKey = stringArray1[0];
                    String sValue = HttpUtility.UrlDecode(stringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            //  odinfo.ResponseReasonText = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            //  odinfo.TransactionID = Convert.ToString(sValue);
                            break;

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

