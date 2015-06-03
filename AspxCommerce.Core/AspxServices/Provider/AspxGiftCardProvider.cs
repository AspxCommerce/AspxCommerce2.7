using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using SageFrame.Web.Utilities;
using System.Web;
using AspxCommerce.Core.Mobile;

namespace AspxCommerce.Core
{
    public class AspxGiftCardProvider
    {
        public AspxGiftCardProvider()
        {
        }

         public static bool CheckGiftCardUsed(AspxCommonInfo aspxCommonObj, string giftCardCode, decimal amount)
         {

             try
             {
                 List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);

                 paramCol.Add(new KeyValuePair<string, object>("@Amount", amount));
                 paramCol.Add(new KeyValuePair<string, object>("@GiftCardCode", giftCardCode));
                 SQLHandler sqlHl = new SQLHandler();

                 int allowToCheckOut = sqlHl.ExecuteAsScalar<int>("[dbo].[usp_Aspx_CheckGiftCardIsUsed]",
                                                                    paramCol);
                 return allowToCheckOut == 1;

             }
             catch (Exception e)
             {
                 throw e;
             }
         }

        public static List<GiftCardReport> GetGiftCardReport(int offset, int? limit, GiftCardReport objGiftcard, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@offset", offset));
                paramCol.Add(new KeyValuePair<string, object>("@limit", limit));
                paramCol.Add(new KeyValuePair<string, object>("@FromDate", objGiftcard.FromDate));
                paramCol.Add(new KeyValuePair<string, object>("@ToDate", objGiftcard.ToDate));
                paramCol.Add(new KeyValuePair<string, object>("@SKU", objGiftcard.SKU));
                paramCol.Add(new KeyValuePair<string, object>("@ItemName", objGiftcard.ItemName));
                paramCol.Add(new KeyValuePair<string, object>("@GiftCardType", objGiftcard.GiftCardType));               
                SQLHandler sqlHl = new SQLHandler();
                List<GiftCardReport> lstGiftCard =
                    sqlHl.ExecuteAsList<GiftCardReport>("[dbo].[usp_Aspx_GetGiftCardReports]",
                                                        paramCol);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardType> GetGiftCardTypes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                //List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
                //SQLHandler sqlHl = new SQLHandler();
                //List<GiftCardType> lstGiftCard = sqlHl.ExecuteAsList<GiftCardType>("[dbo].[usp_Aspx_GetGiftCardTypes]",
                //                                         paramCol);
                List<GiftCardType> lstGiftCard = new List<GiftCardType>()
                                                     {
                                                         new GiftCardType() {Type = "Email", TypeId = 1},
                                                         new GiftCardType() {Type = "Mail", TypeId = 2}
                                                        // ,  new GiftCardType() {Type = "Both", TypeId = 3},

                                                     };
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }  

        public static int GetGiftCardType(AspxCommonInfo aspxCommonObj, int cartitemId)
        {
            try
            {
               
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CartItemId", cartitemId));
                SQLHandler sqlH = new SQLHandler();
                int strType = sqlH.ExecuteAsScalar<int>("usp_Aspx_GetGiftCardType", parameter);
                return strType;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardType> GetGiftCardTypeId(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardType> lstGiftCardType = GetGiftCardTypes(aspxCommonObj);//("usp_Aspx_GetGiftCardTypeId", parameter);
                return lstGiftCardType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Vefification VerifyGiftCard(string giftcardCode, string pinCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", giftcardCode));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", Encrypt(pinCode)));
                SQLHandler sqlH = new SQLHandler();
                Vefification objVerify = sqlH.ExecuteAsObject<Vefification>("usp_Aspx_VerifyGiftCard", parameter);
                return objVerify;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<BalanceInquiry> CheckGiftCardBalance(string giftcardCode, string giftCardPinCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", giftcardCode));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode",Encrypt(giftCardPinCode)));
                SQLHandler sqlH = new SQLHandler();
                List<BalanceInquiry> lstBalanceInq = sqlH.ExecuteAsList<BalanceInquiry>("usp_Aspx_GetBalanceInfoByGiftCardCode", parameter);
                return lstBalanceInq;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardHistory> GetGiftCardHistory(int giftcardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftcardId));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardHistory> lstGCHistory = sqlH.ExecuteAsList<GiftCardHistory>("usp_Aspx_GetGiftCardHistory", parameter);
                return lstGCHistory;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCard> GetGiftCardDetailById(int giftcardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftcardId));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCard> lstGiftCard = sqlH.ExecuteAsList<GiftCard>("usp_Aspx_GetGiftCardDetailById ", parameter);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SaveGiftCardByAdmin(int giftCardId, GiftCard giftCardDetail, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardGraphicsId", giftCardDetail.GraphicThemeId));
                parameter.Add(new KeyValuePair<string, object>("@IsRecipientNotified",
                                                               giftCardDetail.IsRecipientNotified));
                //add column in table expiry date /add default 1 year after/ paid 
                parameter.Add(new KeyValuePair<string, object>("@ExpireDate", giftCardDetail.ExpireDate));
                parameter.Add(new KeyValuePair<string, object>("@Amount", giftCardDetail.Price));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));

                parameter.Add(new KeyValuePair<string, object>("@RecipientEmail", giftCardDetail.RecipientEmail));
                parameter.Add(new KeyValuePair<string, object>("@RecipientName", giftCardDetail.RecipientName));
                parameter.Add(new KeyValuePair<string, object>("@SenderEmail", giftCardDetail.SenderEmail));
                parameter.Add(new KeyValuePair<string, object>("@SenderName", giftCardDetail.SenderName));
                parameter.Add(new KeyValuePair<string, object>("@Messege", giftCardDetail.Messege));
                if (giftCardId > 0)
                {
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", giftCardDetail.GiftCardCode));
                }
                else
                {
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
                }

                parameter.Add(new KeyValuePair<string, object>("@GiftCardTypeId", giftCardDetail.GiftCardTypeId));
                SQLHandler sqlH = new SQLHandler();
                int i = sqlH.ExecuteAsScalar<int>("usp_Aspx_SaveGiftCardByAdmin", parameter);
                var typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, i);
                if (giftCardId == 0)
                {
                    IssueGiftCard(i,null ,aspxCommonObj);
                    if (typeId == 1) //both or virtual
                        NotifyUser(i, aspxCommonObj);
                }   else
                {
                    if ((bool) (!giftCardDetail.IsRecipientNotified))
                    {
                        NotifyUser(i, aspxCommonObj);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardGrid> GetAllPaidGiftCard(int offset, int limit, AspxCommonInfo aspxCommonObj, GiftCardDataInfo giftCardDataObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", giftCardDataObj.OrderID));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", giftCardDataObj.GiftcardCode));
                parameter.Add(new KeyValuePair<string, object>("@Balance", giftCardDataObj.Balance));
                parameter.Add(new KeyValuePair<string, object>("@BalanceTo", giftCardDataObj.BalanceTo));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", giftCardDataObj.StartDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", giftCardDataObj.EndDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", giftCardDataObj.Status));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardGrid> ii = sqlH.ExecuteAsList<GiftCardGrid>("[usp_Aspx_GetAllPaidGiftCard]", parameter);
                return ii;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteGiftCard(string giftCardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteGiftCard", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckGiftCardCategory(AspxCommonInfo aspxCommonObj, string giftcardCategoryName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryName", giftcardCategoryName.Trim()));
                SQLHandler sqlH = new SQLHandler();
                bool isGiftCard = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckGiftCardCategory ", parameter);
                return isGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SaveNewGiftCardCategory(string giftCardGraphicId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardGraphicId", giftCardGraphicId));
                parameter.Add(new KeyValuePair<string, object>("@GiftcardCategoryName", giftcardCategoryName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddGiftCardCategory", parameter);
            }
            catch (Exception e)
            { throw e; }
        }

        public static void SaveGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryId", giftCardCategoryId));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryName", giftcardCategoryName.Trim()));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateGiftCardCategory", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void DeleteGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryId", giftCardCategoryId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteGiftCardCategory", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteGiftCardThemeImage(int giftCardGraphicId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardGraphicId ", giftCardGraphicId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteGiftCardThemeImage", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string SaveGiftCardItemCategory(int itemId, string ids, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemId", itemId));
                parameter.Add(new KeyValuePair<string, object>("@Ids", ids));
                SQLHandler sqlH = new SQLHandler();
                string strValue = sqlH.ExecuteAsScalar<string>("usp_Aspx_SaveGiftCardItemCategory", parameter);
                return strValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardInfo> GetGiftCardThemeImagesByItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemId", itemId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardInfo> lstGiftCard = sqlH.ExecuteAsList<GiftCardInfo>("usp_Aspx_GetGiftCardThemeImage", parameter);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetGiftCardItemCategory(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemId", itemId));
                SQLHandler sqlH = new SQLHandler();
                string strValue = sqlH.ExecuteAsScalar<string>("usp_Aspx_GetGiftCardItemCategory", parameter);
                return strValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardCategoryInfo> GetAllGiftCardCategoryGrid(int offset, int limit, AspxCommonInfo aspxCommonObj, string categoryName, DateTime? addedon, bool? status)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CategoryName", categoryName));
                parameter.Add(new KeyValuePair<string, object>("@AddedDate", addedon));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardCategoryInfo> lstGCCat = sqlH.ExecuteAsList<GiftCardCategoryInfo>("usp_Aspx_GetAllGiftCardCategoryGrid", parameter);
                return lstGCCat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardCategoryInfo> GetGiftCardCategoryDetailByID(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardCategoryInfo> lstGCCat = sqlH.ExecuteAsList<GiftCardCategoryInfo>("usp_Aspx_GetGiftCardCategoryDetailByID", parameter);
                return lstGCCat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public static List<GiftCardInfo> GetAllGiftCardCategory(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardInfo> lstGiftCard = sqlH.ExecuteAsList<GiftCardInfo>("usp_Aspx_GetAllGiftCardCategory", parameter);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static List<GiftCardInfo> GetAllGiftCardThemeImageByCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryId", giftCardCategoryId));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardInfo> lstGiftCard = sqlH.ExecuteAsList<GiftCardInfo>("usp_Aspx_GetAllGiftCardThemeByCategory", parameter);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GiftCardInfo> GetAllGiftCardThemeImage(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryId", categoryId));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardInfo> lstGiftCard = sqlH.ExecuteAsList<GiftCardInfo>("usp_Aspx_GetAllGiftCardThemeByCategory", parameter);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SaveGiftCardThemeImage(string graphicThemeName, string graphicImage, int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GraphicName", graphicThemeName));
                parameter.Add(new KeyValuePair<string, object>("@GraphicImage", graphicImage));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCategoryId", giftCardCategoryId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddGiftCardTheme", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int SaveGiftCardThemeImageReturnGiftCardGraphicId(string graphicThemeName, string graphicImage, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GraphicName", graphicThemeName));
                parameter.Add(new KeyValuePair<string, object>("@GraphicImage", graphicImage));
                SQLHandler sqH = new SQLHandler();
                return sqH.ExecuteNonQuery("[dbo].[usp_Aspx_AddGiftCardThemeOutID]", parameter, "@GiftCardGraphicId");
            }
            catch (Exception e)
            { throw e; }
        }

        private static int GetGiftCardType(int storeId, int portalId, int giftCardId)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetGiftTypeId", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static void IssueGiftCard(int giftCardId,int? orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", true));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public static void IssueGiftCard(string giftCardIds,int? orderID, AspxCommonInfo aspxCommonObj)
        {
            var ids = giftCardIds.Split('N');

            foreach (var id in ids)
            {
                try
                {
                    int giftCardId = int.Parse(id);
                    List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
                    parameter.Add(new KeyValuePair<string, object>("@IsActive", true));
                    parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
                    parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
                    int typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
                    if (typeId == 1)
                        NotifyUser(giftCardId, aspxCommonObj);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

        public static void NotifyUser(int giftCardId, AspxCommonInfo aspxCommonObj)
        {
            //var m_searchthread = new System.Threading.Thread(new System.Threading.ThreadStart(Search));
            //m_searchthread.IsBackground = true;
            //m_searchthread.Start();
            //sending email run in thread if exeption then not to update 
            var giftCardInfo = GetGiftCardInfoById(giftCardId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
             var emailSent = EmailTemplate.SendEmailForGiftCard(aspxCommonObj.PortalID, aspxCommonObj.StoreID,
                                                               aspxCommonObj.CultureName, giftCardInfo);
            if (emailSent)
            {
                UpdateNotification(giftCardId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
            }

        }

        public static void NotifyUserForGiftCardActivation(int orderID, AspxCommonInfo aspxCommonObj)
        {
            List<GiftCardGrid> giftCardID = GetGiftCardIDByOrderID(orderID, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
            if (giftCardID != null && giftCardID.Count > 0)
            {
                foreach (GiftCardGrid item in giftCardID)
                {                   
                    var giftCardInfo = GetGiftCardInfoById(item.GiftCardId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
                    var emailSent = EmailTemplate.SendEmailForGiftCardActivation(aspxCommonObj.PortalID, aspxCommonObj.StoreID,
                                                                      aspxCommonObj.CultureName, giftCardInfo);
                }
            }
           
        }

        private static GiftCard GetGiftCardInfoById(int giftCardId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                SQLHandler sqlH = new SQLHandler();
                GiftCard objGiftCard= sqlH.ExecuteAsObject<GiftCard>("usp_Aspx_GetGiftCardDetailById ", parameter);
                if (objGiftCard != null && !string.IsNullOrEmpty(objGiftCard.GiftCardPinCode))
                {
                    objGiftCard.GiftCardPinCode = Decrypt(objGiftCard.GiftCardPinCode);
                }
                return objGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static List<GiftCardGrid> GetGiftCardIDByOrderID(int orderID, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<GiftCardGrid> objGiftCard = sqlH.ExecuteAsList<GiftCardGrid>("usp_Aspx_GetGiftCardIDByOrderID ", parameter);               
                return objGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void UpdateNotification(int giftCardId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@IsNotified", true));
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateIsNotifiedGiftCard ", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string GetGiftCardKey()
        {
            const int maxSize = 16;
            //int minSize = 8;
            string a = "AEYZ1FGHIQRSTX2345JBCDKLMNOP67890UVW";
            var chars = a.ToCharArray();
            int size = maxSize;
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();

        }

        public static string HashPassword(string pasword)
        {
            byte[] arrbyte = new byte[pasword.Length];
            SHA256 hash = new SHA256CryptoServiceProvider();
            arrbyte = hash.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            return Convert.ToBase64String(arrbyte);
        }
      
        private static string GetGiftPinCode()
        {
            var rnd = new Random(); //Initialize the random-number generator
            string pinCode="";
            for (int i = 0; i < 4; i++)
            {
                pinCode += rnd.Next(0, 9).ToString();
                if (pinCode == "0")
                {
                    i--;
                }
               
            }
            return Encrypt(pinCode.ToString());

        }
        private static string Encrypt(string input)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        private const string key = "_giftCardPinCode";
        private static string Decrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Parse(int orderId,string transId, string invoice, string POrderno, int responseCode, int responsereasonCode,
                                  string responsetext, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = aspxCommonObj.StoreID;
                cf.PortalID = aspxCommonObj.PortalID;
                cf.AddedBy = aspxCommonObj.UserName;
                // UpdateOrderDetails
              
                odinfo.OrderID = orderId;
                odinfo.TransactionID = odinfo.ResponseCode.ToString(transId);
                odinfo.InvoiceNumber = Convert.ToString(invoice);
                odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
                odinfo.ResponseCode = Convert.ToInt32(responseCode);
                odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
                odinfo.ResponseReasonText = Convert.ToString(responsetext);
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                //GIFT CARD PURCHAGE PAYMENT COMPLETED
                odinfo.OrderStatusID = 3;
                AspxOrderController.UpdateOrderDetails(ot);

                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    var orderdata2 = new OrderDetailsCollection();
                    orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    AspxOrderController.UpdateItemQuantity(orderdata2);
                    IssueGiftCard(orderdata2.LstOrderItemsInfo,orderId, true, aspxCommonObj);
                }
                HttpContext.Current.Session.Remove("OrderID");
                cms.ClearCartAfterPayment(aspxCommonObj);
                return "This transaction has been approved";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void IssueGiftCard(List<OrderItemInfo> itemList,int orderID, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            foreach (OrderItemInfo orderItemInfo in itemList)
            {
                if (orderItemInfo.IsGiftCard)
                {
                    try
                    {
                        int giftCardId = GetGiftCardIdByCartItemId(orderItemInfo.CartItemId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
                        if (giftCardId > 0)
                        {
                            List<KeyValuePair<string, object>> parameter =CommonParmBuilder.GetParamSP(aspxCommonObj);
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
                            //  parameter.Add(new KeyValuePair<string, object>("@Amount", orderItemInfo.Price));
                            // parameter.Add(new KeyValuePair<string, object>("@UsedAmount", 0));                           
                            parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                            parameter.Add(new KeyValuePair<string, object>("OrderID", orderID));
                            parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                            SQLHandler sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
                            //CreateLog(giftCardId, storeId, portalId, orderItemInfo.Price, userName);
                            int typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
                            if (typeId == 1)
                                NotifyUser(giftCardId, aspxCommonObj);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        private static int GetGiftCardIdByCartItemId(int cartItemId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CartItemId", cartItemId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                SQLHandler sqlH = new SQLHandler();
                int giftCardID= sqlH.ExecuteAsScalar<int>("usp_Aspx_GetGiftCardIdByCartItemId ", parameter);
                return giftCardID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CreateLog(int giftcardId, int storeId, int portalId, decimal amount, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftcardId));
                parameter.Add(new KeyValuePair<string, object>("@Amount", amount));
                parameter.Add(new KeyValuePair<string, object>("@UsedAmount", 0));
                parameter.Add(new KeyValuePair<string, object>("@Balance", amount));
                parameter.Add(new KeyValuePair<string, object>("@Note", "Created by User:" + userName));

                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_LogGiftCardHistory ", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateGiftCardUsage(List<GiftCardUsage> gDetail, int storeId, int portalId, int orderId, string userName, string cultureName)
        {
            foreach (var giftCardUsage in gDetail)
            {
                try
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardUsage.GiftCardId));
                    parameter.Add(new KeyValuePair<string, object>("@UsedAmount", giftCardUsage.ReducedAmount));
                    parameter.Add(new KeyValuePair<string, object>("@OrderId", orderId));
                    parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                    parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                    parameter.Add(new KeyValuePair<string, object>("@IsActive",
                                                                   giftCardUsage.ReducedAmount != giftCardUsage.Balance));
                    parameter.Add(new KeyValuePair<string, object>("@Balance", giftCardUsage.Balance));
                    parameter.Add(new KeyValuePair<string, object>("@Amount", giftCardUsage.Price));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_aspx_UpdateUsageofGiftCard", parameter);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void UpdateGiftCardUsage(string detail, int storeId, int portalId, int orderId, string userName, string cultureName)
        {
            string[] glist = detail.Split('N');

            foreach (var giftCard in glist)
            {
                string[] vls = giftCard.Split('-');
                int giftCardId = int.Parse(vls[0]); //giftcard id
                decimal reduceAmount = decimal.Parse(vls[1]); //reduce amount
                decimal balance = decimal.Parse(vls[2]); //giftcard balance now
                decimal price = decimal.Parse(vls[3]); //giftcard old bal

                try
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                    parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                    parameter.Add(new KeyValuePair<string, object>("@UsedAmount", reduceAmount));
                    parameter.Add(new KeyValuePair<string, object>("@OrderId", orderId));
                    parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                    parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                    parameter.Add(new KeyValuePair<string, object>("@IsActive",
                                                                   reduceAmount != balance));
                    parameter.Add(new KeyValuePair<string, object>("@Balance", balance));
                    parameter.Add(new KeyValuePair<string, object>("@Amount", price));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_aspx_UpdateUsageofGiftCard", parameter);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void IssueGiftCardForMobile(List<OrderItem> itemList,int orderID, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            foreach (OrderItem orderItemInfo in itemList)
            {
                if (orderItemInfo.IsGiftCard)
                {
                    try
                    {
                        int giftCardId = GetGiftCardIdByCartItemId(orderItemInfo.CartItemId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);
                        if (giftCardId > 0)
                        {
                            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardId", giftCardId));
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardCode", GetGiftCardKey()));
                            parameter.Add(new KeyValuePair<string, object>("@GiftCardPinCode", GetGiftPinCode()));
                            parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                            parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                            parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                            SQLHandler sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_IssueGiftCard ", parameter);
                            int typeId = GetGiftCardType(aspxCommonObj.StoreID, aspxCommonObj.PortalID, giftCardId);
                            if (typeId == 1)      //if email type only
                                NotifyUser(giftCardId, aspxCommonObj);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}
