//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//using AspxCommerce.Core.Mobile;
//using SageFrame.Web.Utilities;

//namespace AspxCommerce.Core
//{
//    public static class GiftCardController
//    {
//        public static string Parse(string transId, string invoice, string POrderno, int responseCode, int responsereasonCode,
//                                   string responsetext, AspxCommonInfo aspxCommonObj)
//        {
//            try
//            {
//                var ot = new OrderDetailsCollection();
//                var odinfo = new OrderDetailsInfo();
//                var cms = new CartManageSQLProvider();
//                var cf = new CommonInfo();
//                cf.StoreID = aspxCommonObj.StoreID;
//                cf.PortalID = aspxCommonObj.PortalID;
//                cf.AddedBy = aspxCommonObj.UserName;
//                // UpdateOrderDetails
//                var objad = new AspxOrderDetails();
//                odinfo.TransactionID = odinfo.ResponseCode.ToString( transId);
//                odinfo.InvoiceNumber = Convert.ToString(invoice);
//                odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
//                odinfo.ResponseCode = Convert.ToInt32(responseCode);
//                odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
//                odinfo.ResponseReasonText = Convert.ToString(responsetext);
//                ot.ObjOrderDetails = odinfo;
//                ot.ObjCommonInfo = cf;
//                odinfo.OrderStatusID = 8;
//                objad.UpdateOrderDetails(ot);

//                if (HttpContext.Current.Session["OrderCollection"] != null)
//                {
//                    var orderdata2 = new OrderDetailsCollection();
//                    orderdata2 = (OrderDetailsCollection) HttpContext.Current.Session["OrderCollection"];
//                    objad.UpdateItemQuantity(orderdata2);
//                    IssueGiftCard(orderdata2.LstOrderItemsInfo,true, aspxCommonObj);
//                }
//                HttpContext.Current.Session.Remove("OrderID");
//                cms.ClearCartAfterPayment(aspxCommonObj);
//                return "This transaction has been approved";
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

      
//        public static void SaveGiftCardByAdmin(int giftCardId, GiftCard giftCardDetail, bool isActive, AspxCommonInfo aspxCommonObj)
//        {
//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardGraphicsId", giftCardDetail.GraphicThemeId));
//                parameter.Add(new KeyValuePair<string, object>("@IsRecipientNotified",
//                                                               giftCardDetail.IsRecipientNotified));
//                //add column in table expiry date /add default 1 year after/ paid 
//                parameter.Add(new KeyValuePair<string, object>("@ExpireDate", giftCardDetail.ExpireDate));
//                parameter.Add(new KeyValuePair<string, object>("@Amount", giftCardDetail.Price));
//                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));

//                parameter.Add(new KeyValuePair<string, object>("@RecipientEmail", giftCardDetail.RecipientEmail));
//                parameter.Add(new KeyValuePair<string, object>("@RecipientName", giftCardDetail.RecipientName));
//                parameter.Add(new KeyValuePair<string, object>("@SenderEmail", giftCardDetail.SenderEmail));
//                parameter.Add(new KeyValuePair<string, object>("@SenderName", giftCardDetail.SenderName));
//                parameter.Add(new KeyValuePair<string, object>("@Messege", giftCardDetail.Messege));
//                if (giftCardId>0)
//                {
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", giftCardDetail.GiftCardCode));
//                }else
//                {
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
//                }
               
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardTypeId", giftCardDetail.GiftCardTypeId));
//                    //always virtual
//                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
//                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
//                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
//                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
//                var sqlH = new SQLHandler();
//                var i = sqlH.ExecuteAsScalar<int>("usp_Aspx_SaveGiftCardByAdmin", parameter);
//                var typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, i);
//                if (giftCardId==0)
//                {
//                    IssueGiftCard(i, aspxCommonObj);
//                    if (typeId == 2 || typeId == 3) //both or virtual
//                        NotifyUser(i, aspxCommonObj);
//                }
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }
//        }

//        private static int GetGiftCardType(int storeId, int portalId, int giftCardId)
//        {

//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
//                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
//                var sqlH = new SQLHandler();
//                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetGiftTypeId", parameter);

//            }
//            catch (Exception e)
//            {
//                throw e;
//            }
//        }
//        private static void IssueGiftCard(int giftCardId, AspxCommonInfo aspxCommonObj)
//        {
//            try
//            {

//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
//                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
//                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
//                parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
//                var sqlH = new SQLHandler();
//                sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);

//            }

//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static void IssueGiftCard(List<OrderItemInfo> itemList,bool isActive, AspxCommonInfo aspxCommonObj)
//        {
//            foreach (var orderItemInfo in itemList)
//            {
//                if (orderItemInfo.IsGiftCard)
//                {
//                    try
//                    {
//                        var giftCardId = GetGiftCardIdByCartItemId(orderItemInfo.CartItemId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
//                        if (giftCardId > 0)
//                        {
//                            var parameter = new List<KeyValuePair<string, object>>();
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
//                            //  parameter.Add(new KeyValuePair<string, object>("@Amount", orderItemInfo.Price));
//                            // parameter.Add(new KeyValuePair<string, object>("@UsedAmount", 0));
//                            parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
//                            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
//                            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
//                            parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
//                            var sqlH = new SQLHandler();
//                            sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
//                            //CreateLog(giftCardId, storeId, portalId, orderItemInfo.Price, userName);
//                            var typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
//                            if (typeId == 2 || typeId == 3)
//                                NotifyUser(giftCardId, aspxCommonObj);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        throw ex;
//                    }
//                }
//            }
//        }

//        public static void IssueGiftCardForMobile(List<OrderItem> itemList, bool isActive,AspxCommonInfo aspxCommonObj)
//        {
//            foreach (var orderItemInfo in itemList)
//            {
//                if (orderItemInfo.IsGiftCard)
//                {
//                    try
//                    {
//                        var giftCardId = GetGiftCardIdByCartItemId(orderItemInfo.CartItemId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
//                        if (giftCardId > 0)
//                        {
//                            var parameter = new List<KeyValuePair<string, object>>();
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
//                            parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
//                            //  parameter.Add(new KeyValuePair<string, object>("@Amount", orderItemInfo.Price));
//                            // parameter.Add(new KeyValuePair<string, object>("@UsedAmount", 0));
//                            parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
//                            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
//                            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
//                            parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
//                            var sqlH = new SQLHandler();
//                            sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
//                            //CreateLog(giftCardId, storeId, portalId, orderItemInfo.Price, userName);
//                            var typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
//                            if (typeId == 2 || typeId == 3)
//                                NotifyUser(giftCardId,aspxCommonObj);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        throw ex;
//                    }
//                }
//            }
//        }

     
//        public static void IssueGiftCard(string giftCardIds,AspxCommonInfo aspxCommonObj)
//        {
//            var ids = giftCardIds.Split('N');

//            foreach (var id in ids)
//            {
//                try
//                {
//                    int giftCardId = int.Parse(id);
//                    var parameter = new List<KeyValuePair<string, object>>();
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
//                    parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
//                    parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
//                    parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
//                    var sqlH = new SQLHandler();
//                    sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
//                    var typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
//                    if (typeId == 2 || typeId == 3)
//                        NotifyUser(giftCardId, aspxCommonObj);
//                }

//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }

//        }

//        public static void UpdateGiftCardUsage(List<GiftCardUsage> gDetail, int storeId, int portalId, int orderId, string userName, string cultureName)
//        {
//            foreach (var giftCardUsage in gDetail)
//            {
//                try
//                {
//                    var parameter = new List<KeyValuePair<string, object>>();
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardUsage.GiftCardId));
//                    parameter.Add(new KeyValuePair<string, object>("@UsedAmount", giftCardUsage.ReducedAmount));
//                    parameter.Add(new KeyValuePair<string, object>("@OrderId", orderId));
//                    parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
//                    parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
//                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
//                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
//                    parameter.Add(new KeyValuePair<string, object>("@IsActive",
//                                                                   giftCardUsage.ReducedAmount != giftCardUsage.Balance));
//                    parameter.Add(new KeyValuePair<string, object>("@Balance", giftCardUsage.Balance));
//                    parameter.Add(new KeyValuePair<string, object>("@Amount", giftCardUsage.Price));
//                    var sqlH = new SQLHandler();
//                    sqlH.ExecuteNonQuery("usp_aspx_UpdateUsageofGiftCard", parameter);

//                }
//                catch (Exception e)
//                {
//                    throw e;
//                }
//            }
//        }
//        public static void UpdateGiftCardUsage(string detail, int storeId, int portalId, int orderId, string userName, string cultureName)
//        {
//            string[] glist = detail.Split('N');

//            foreach (var giftCard in glist)
//            {
//                string[] vls = giftCard.Split('-');
//                int giftCardId = int.Parse(vls[0]); //giftcard id
//                decimal reduceAmount = decimal.Parse(vls[1]); //reduce amount
//                decimal balance = decimal.Parse(vls[2]); //giftcard balance now
//                decimal price = decimal.Parse(vls[3]); //giftcard old bal

//                try
//                {
//                    var parameter = new List<KeyValuePair<string, object>>();
//                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                    parameter.Add(new KeyValuePair<string, object>("@UsedAmount", reduceAmount));
//                    parameter.Add(new KeyValuePair<string, object>("@OrderId", orderId));
//                    parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
//                    parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
//                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
//                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
//                    parameter.Add(new KeyValuePair<string, object>("@IsActive",
//                                                                   reduceAmount != balance));
//                    parameter.Add(new KeyValuePair<string, object>("@Balance", balance));
//                    parameter.Add(new KeyValuePair<string, object>("@Amount", price));
//                    var sqlH = new SQLHandler();
//                    sqlH.ExecuteNonQuery("usp_aspx_UpdateUsageofGiftCard", parameter);

//                }
//                catch (Exception e)
//                {
//                    throw e;
//                }
//            }
//        }

//        private static int GetGiftCardIdByCartItemId(int cartItemId, int storeId, int portalId)
//        {
//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@CartItemId", cartItemId));
//                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
//                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
//                var sqlH = new SQLHandler();
//                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetGiftCardIdByCartItemId ", parameter);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static void CreateLog(int giftcardId, int storeId, int portalId, decimal amount, string userName)
//        {
//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftcardId));
//                parameter.Add(new KeyValuePair<string, object>("@Amount", amount));
//                parameter.Add(new KeyValuePair<string, object>("@UsedAmount", 0));
//                parameter.Add(new KeyValuePair<string, object>("@Balance", amount));
//                parameter.Add(new KeyValuePair<string, object>("@Note", "Created by User:" + userName));

//                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
//                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
//                var sqlH = new SQLHandler();
//                sqlH.ExecuteNonQuery("usp_Aspx_LogGiftCardHistory ", parameter);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static void NotifyUser(int giftCardId, AspxCommonInfo aspxCommonObj)
//        {
//            //var m_searchthread = new System.Threading.Thread(new System.Threading.ThreadStart(Search));
//            //m_searchthread.IsBackground = true;
//            //m_searchthread.Start();
//            //sending email run in thread if exeption then not to update 
//           var giftCardInfo= GetGiftCardInfoById(giftCardId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);

//         var emailSent= EmailTemplate.SendEmailForGiftCard(aspxCommonObj.PortalID, aspxCommonObj.StoreID, aspxCommonObj.CultureName, giftCardInfo);
//         if (emailSent)
//         {
//             UpdateNotification(giftCardId,aspxCommonObj.StoreID,aspxCommonObj.PortalID);
//         }

//        }

//        private static GiftCard GetGiftCardInfoById(int giftCardId,int storeId,int portalId)
//        {
//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
//                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
//                var sqlH = new SQLHandler();
//                return sqlH.ExecuteAsObject<GiftCard>("usp_Aspx_GetGiftCardDetailById ", parameter);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private static void UpdateNotification(int giftCardId, int storeId, int portalId)
//        {
//            try
//            {
//                var parameter = new List<KeyValuePair<string, object>>();
//                parameter.Add(new KeyValuePair<string, object>("@IsNotified", true));
//                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
//                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
//                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
//                var sqlH = new SQLHandler();
//                sqlH.ExecuteNonQuery("usp_Aspx_UpdateIsNotifiedGiftCard ", parameter);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        private static string GetGiftCardKey()
//        {
//            const int maxSize = 8;
//            //int minSize = 8;
//            string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890";
//            var chars = a.ToCharArray();
//            int size = maxSize;
//            var data = new byte[1];
//            var crypto = new RNGCryptoServiceProvider();
//            crypto.GetNonZeroBytes(data);
//            size = maxSize;
//            data = new byte[size];
//            crypto.GetNonZeroBytes(data);
//            var result = new StringBuilder(size);
//            foreach (byte b in data)
//            {
//                result.Append(chars[b%(chars.Length - 1)]);
//            }
//            return result.ToString();

//        }
//        private static int GetGiftPinCode()
//        {
//            var rnd = new Random(); //Initialize the random-number generator
//            string pinCode="";
//            for (int i = 0; i < 4;i++ )
//            {
//                pinCode += rnd.Next(0, 9).ToString(CultureInfo.InvariantCulture);
//                if(pinCode=="0")
//                {
//                    i--;
//                }
//            }
//            return int.Parse(pinCode);

//        }
//    }
//}
