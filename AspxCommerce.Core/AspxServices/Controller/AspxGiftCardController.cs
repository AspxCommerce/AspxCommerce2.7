using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AspxCommerce.Core.Mobile;

namespace AspxCommerce.Core
{
    public class AspxGiftCardController
    {
        public AspxGiftCardController()
        {
        }

        public static bool CheckGiftCardUsed(AspxCommonInfo aspxCommonObj, string giftCardCode, decimal amount)
        {
             bool allowToCheckout = AspxGiftCardProvider.CheckGiftCardUsed(aspxCommonObj, giftCardCode,amount);
            return allowToCheckout;
        }

        public static List<GiftCardReport> GetGiftCardReport(int offset, int? limit, GiftCardReport objGiftcard, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardReport> giftCardReports = AspxGiftCardProvider.GetGiftCardReport(offset, limit, objGiftcard, aspxCommonObj);
                return giftCardReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GiftCardType> GetGiftCardTypes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardType> lstGiftCard = AspxGiftCardProvider.GetGiftCardTypes(aspxCommonObj);
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
                int strType = AspxGiftCardProvider.GetGiftCardType(aspxCommonObj, cartitemId);
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
                List<GiftCardType> lstGiftCardType = AspxGiftCardProvider.GetGiftCardTypeId(aspxCommonObj);
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
                Vefification objVerify = AspxGiftCardProvider.VerifyGiftCard(giftcardCode, pinCode, aspxCommonObj);
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
                List<BalanceInquiry> lstBalanceInq = AspxGiftCardProvider.CheckGiftCardBalance(giftcardCode, giftCardPinCode, aspxCommonObj);
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
                List<GiftCardHistory> lstGCHistory = AspxGiftCardProvider.GetGiftCardHistory(giftcardId, aspxCommonObj);
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
                List<GiftCard> lstGiftCard = AspxGiftCardProvider.GetGiftCardDetailById(giftcardId, aspxCommonObj);
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
                AspxGiftCardProvider.SaveGiftCardByAdmin(giftCardId, giftCardDetail, isActive, aspxCommonObj);
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
                List<GiftCardGrid> ii = AspxGiftCardProvider.GetAllPaidGiftCard(offset, limit, aspxCommonObj, giftCardDataObj);
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
                AspxGiftCardProvider.DeleteGiftCard(giftCardId, aspxCommonObj);
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
                bool isGiftCard = AspxGiftCardProvider.CheckGiftCardCategory(aspxCommonObj, giftcardCategoryName);
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
                AspxGiftCardProvider.SaveNewGiftCardCategory(giftCardGraphicId, aspxCommonObj, giftcardCategoryName, isActive);
            }
            catch (Exception e)
            { throw e; }
        }

        public static void SaveGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
        {
            try
            {
                AspxGiftCardProvider.SaveGiftCardCategory(giftCardCategoryId, aspxCommonObj, giftcardCategoryName, isActive);
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
                AspxGiftCardProvider.DeleteGiftCardCategory(giftCardCategoryId, aspxCommonObj);
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
                AspxGiftCardProvider.DeleteGiftCardThemeImage(giftCardGraphicId, aspxCommonObj);
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
                string strValue = AspxGiftCardProvider.SaveGiftCardItemCategory(itemId, ids, aspxCommonObj);
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
                List<GiftCardInfo> lstGiftCard = AspxGiftCardProvider.GetGiftCardThemeImagesByItem(itemId, aspxCommonObj);
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
                string strValue = AspxGiftCardProvider.GetGiftCardItemCategory(itemId, aspxCommonObj);
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
                List<GiftCardCategoryInfo> lstGCCat = AspxGiftCardProvider.GetAllGiftCardCategoryGrid(offset, limit, aspxCommonObj, categoryName, addedon, status);
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
                List<GiftCardCategoryInfo> lstGCCat = AspxGiftCardProvider.GetGiftCardCategoryDetailByID(categoryID, aspxCommonObj);
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
                List<GiftCardInfo> lstGiftCard = AspxGiftCardProvider.GetAllGiftCardCategory(aspxCommonObj);
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
                List<GiftCardInfo> lstGiftCard = AspxGiftCardProvider.GetAllGiftCardThemeImageByCategory(giftCardCategoryId, aspxCommonObj);
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
                List<GiftCardInfo> lstGiftCard = AspxGiftCardProvider.GetAllGiftCardThemeImage(aspxCommonObj, categoryId);
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
                AspxGiftCardProvider.SaveGiftCardThemeImage(graphicThemeName, graphicImage, giftCardCategoryId, aspxCommonObj);
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
                return AspxGiftCardProvider.SaveGiftCardThemeImageReturnGiftCardGraphicId(graphicThemeName, graphicImage, aspxCommonObj);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void IssueGiftCard(string giftCardIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardProvider.IssueGiftCard(giftCardIds,null, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void NotifyUser(int giftCardId, AspxCommonInfo aspxCommonObj)
        {
            AspxGiftCardProvider.NotifyUser(giftCardId, aspxCommonObj);
        }

        public static string Parse(int orderID,string transId, string invoice, string POrderno, int responseCode, int responsereasonCode,
                                  string responsetext, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string retStr = AspxGiftCardProvider.Parse(orderID,transId, invoice, POrderno, responseCode, responsereasonCode, responsetext, aspxCommonObj);
                return retStr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void IssueGiftCard(List<OrderItemInfo> itemList,int orderID, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardProvider.IssueGiftCard(itemList,orderID, isActive, aspxCommonObj);
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
                AspxGiftCardProvider.CreateLog(giftcardId, storeId, portalId, amount, userName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateGiftCardUsage(List<GiftCardUsage> gDetail, int storeId, int portalId, int orderId, string userName, string cultureName)
        {
            try
            {
                AspxGiftCardProvider.UpdateGiftCardUsage(gDetail, storeId, portalId, orderId, userName, cultureName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateGiftCardUsage(string detail, int storeId, int portalId, int orderId, string userName, string cultureName)
        {
            try
            {
                AspxGiftCardProvider.UpdateGiftCardUsage(detail, storeId, portalId, orderId, userName, cultureName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void IssueGiftCardForMobile(List<OrderItem> itemList,int orderID, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardProvider.IssueGiftCardForMobile(itemList,orderID, isActive, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
