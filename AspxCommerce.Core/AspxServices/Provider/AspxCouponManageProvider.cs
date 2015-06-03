using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;

namespace AspxCommerce.Core
{
    public class AspxCouponManageProvider
    {
        public AspxCouponManageProvider()
        {
        }
        public static List<CouponInfo> BindAllCouponDetails(int offset, int limit, GetCouponDetailsInfo couponDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponDetailObj.CouponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponDetailObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", couponDetailObj.ValidateFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidateTo", couponDetailObj.ValidateTo));
                SQLHandler sqlH = new SQLHandler();
                List<CouponInfo> lstCoupon = sqlH.ExecuteAsList<CouponInfo>("usp_Aspx_GetCouponDetails", parameter);
                return lstCoupon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckUniqueCouponCode(string couponCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                SQLHandler sqlH = new SQLHandler();
                bool isExists = sqlH.ExecuteNonQueryAsGivenType<bool>("dbo.usp_Aspx_CheckUniqueCouponCode", parameter, "@IsExist");
                return isExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CouponPortalUserListInfo> GetPortalUsersList(int offset, int limit, int couponID, string customerName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerName", customerName));
                SQLHandler sqlH = new SQLHandler();
                List<CouponPortalUserListInfo> lstCoupUser = sqlH.ExecuteAsList<CouponPortalUserListInfo>("usp_Aspx_GetAllPortalUserLists", parameter);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddUpdateCoupons(CouponSaveObj couponSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponSaveObj.CouponID));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponSaveObj.CouponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponSaveObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponAmount", couponSaveObj.CouponAmount));
                parameter.Add(new KeyValuePair<string, object>("@IsPercentage", couponSaveObj.IsPercentage));
                parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", couponSaveObj.ValidateFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidateTo", couponSaveObj.ValidateTo));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", couponSaveObj.IsActive));
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", couponSaveObj.SettingIDs));
                parameter.Add(new KeyValuePair<string, object>("@SettingValues", couponSaveObj.SettingValues));
                parameter.Add(new KeyValuePair<string, object>("@PortalUser_UserName", couponSaveObj.PortalUser_UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCoupons", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void AddUpdatePromoCode(PromoCodeSaveObj promoCodeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", promoCodeObj.CouponID));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", promoCodeObj.CouponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", promoCodeObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponAmount", promoCodeObj.CouponAmount));
                parameter.Add(new KeyValuePair<string, object>("@IsPercentage", promoCodeObj.IsPercentage));
                parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", promoCodeObj.ValidateFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidateTo", promoCodeObj.ValidateTo));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", promoCodeObj.IsActive));
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", promoCodeObj.SettingIDs));
                parameter.Add(new KeyValuePair<string, object>("@SettingValues", promoCodeObj.SettingValues));
                parameter.Add(new KeyValuePair<string, object>("@PromoItems", promoCodeObj.PromoItems));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_AddUpdatePromoCode", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<ItemsForPromoInfo> ItemsForPromoCode(int offset, int limit, AspxCommonInfo aspxCommomObj, string itemName, int? couponId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommomObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponId));
                SQLHandler sqlh = new SQLHandler();
                List<ItemsForPromoInfo> lst = sqlh.ExecuteAsList<ItemsForPromoInfo>("dbo.usp_Aspx_ItemsForPromoCode", parameter);
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<CouponStatusInfo> BindCouponStatus(AspxCommonInfo aspxCommomObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommomObj);
                SQLHandler sqlH = new SQLHandler();
                List<CouponStatusInfo> lstCoupStat = sqlH.ExecuteAsList<CouponStatusInfo>("usp_Aspx_GetCouponStatus", parameter);
                return lstCoupStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CouponSettingKeyValueInfo> GetCouponSettingKeyValueInfo(int couponID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
                SQLHandler sqlH = new SQLHandler();
                List<CouponSettingKeyValueInfo> lstCoupKeyVal = sqlH.ExecuteAsList<CouponSettingKeyValueInfo>("usp_Aspx_GetCouponSettingKeyValueByCouponID", parameter);
                return lstCoupKeyVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteCoupons(string couponIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCoupons", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CouponVerificationInfo VerifyUserCoupon(decimal totalCost, string couponCode, string itemIds, string cartItemIds, AspxCommonInfo aspxCommonObj, int appliedCount)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@totalCost", totalCost));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
                parameter.Add(new KeyValuePair<string, object>("@CartItemIDs", cartItemIds));
                parameter.Add(new KeyValuePair<string, object>("@AppliedCount", appliedCount));
                SQLHandler sqlH = new SQLHandler();
                CouponVerificationInfo lstCoupVeri = sqlH.ExecuteAsObject<CouponVerificationInfo>("[usp_Aspx_VerifyCouponCode]", parameter);
                return lstCoupVeri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateCouponUserRecord(CouponSession coupon, int storeID, int portalID, string userName, int orderID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", coupon.Key));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CouponUsedCount", coupon.AppliedCount));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", parameter);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static List<CouponUserInfo> GetCouponUserDetails(int offset, int? limit, GetCouponUserDetailInfo couponUserObj, AspxCommonInfo aspxCommonObj, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponUserObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponUserObj.CouponStatusID));
                parameter.Add(new KeyValuePair<string, object>("@ValidFrom", couponUserObj.ValidFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidTo", couponUserObj.ValidTo));
                SQLHandler sqlH = new SQLHandler();
                List<CouponUserInfo> lstCoupUser = sqlH.ExecuteAsList<CouponUserInfo>("usp_Aspx_CouponUserDetails", parameter);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CouponUserListInfo> GetCouponUserList(int offset, int limit, CouponCommonInfo bindCouponUserObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponID", bindCouponUserObj.CouponID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", bindCouponUserObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", bindCouponUserObj.CouponStatusID));
                SQLHandler sqlH = new SQLHandler();
                List<CouponUserListInfo> lstCoupUser = sqlH.ExecuteAsList<CouponUserListInfo>("[usp_Aspx_GetCouponUserList]", parameter);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetPromoItemCheckIDs(int CouponID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", CouponID));
                SQLHandler sqlh = new SQLHandler();
                string lst = sqlh.ExecuteAsScalar<string>("dbo.usp_Aspx_GetPromoItemCheckIDs", parameter);
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PromoItemsInfo> GetAllPromoItems(int offset, int limit, int couponTypeId, int couponId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponId));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeId));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlh = new SQLHandler();
                List<PromoItemsInfo> lst = sqlh.ExecuteAsList<PromoItemsInfo>("[dbo].[usp_Aspx_GetPromoItemsList]", parameter);
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteCouponUser(string couponUserID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponUser", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateCouponUser(int couponUserID, int couponStatusID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponStatusID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUser", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void SendCouponCodeEmail(string senderEmail, string receiverEmailDs, string subject,ArrayList messageBody)
        public static void SendCouponCodeEmail(CouponEmailInfo couponEmailObj)
        {
            string[] receiverIDs;
            char[] spliter = { '#' };
            //    receiverIDs = receiverEmailDs.Split(spliter);
            receiverIDs = couponEmailObj.ReceiverEmail.Split(spliter);

            for (int i = 0; i < receiverIDs.Length; i++)
            {
                string receiverEmailID = receiverIDs[i];
                string emailSuperAdmin;
                string emailSiteAdmin;
                SageFrameConfig pagebase = new SageFrameConfig();
                emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                //string individualMsgBody = messageBody[i].ToString();
                string individualMsgBody = couponEmailObj.MessageBodyTemplate[i].ToString();
                //   MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, individualMsgBody, emailSiteAdmin, emailSuperAdmin);
                MailHelper.SendMailNoAttachment(couponEmailObj.SenderEmail, receiverEmailID, couponEmailObj.Subject, individualMsgBody, emailSiteAdmin, emailSuperAdmin);
            }
        }

        #region Coupon Type Manage

        public static List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeName", couponTypeName));
                SQLHandler sqlH = new SQLHandler();
                List<CouponTypeInfo> lstCoupType = sqlH.ExecuteAsList<CouponTypeInfo>("usp_Aspx_GetAllCouponType", parameter);
                return lstCoupType;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddUpdateCouponType(CouponTypeInfo couponTypeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeObj.CouponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponType", couponTypeObj.CouponType));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", couponTypeObj.IsActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponType", parameter);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteCouponType(string IDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", IDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponType", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Coupons Per Sales Management

        public static List<CouponPerSales> GetCouponDetailsPerSales(int offset, int? limit, string couponCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                List<CouponPerSales> lstCoupPerSale = sqlH.ExecuteAsList<CouponPerSales>("usp_Aspx_GetCouponListPerSales", parameter);
                return lstCoupPerSale;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CouponPerSalesViewDetailInfo> GetCouponPerSalesDetailView(int offset, int? limit, CouponPerSalesGetInfo couponPerSaesObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));

                parameterCollection.Add(new KeyValuePair<string, object>("@CouponCode", couponPerSaesObj.CouponCode));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderId", couponPerSaesObj.OrderID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CouponAmountFrom", couponPerSaesObj.CouponAmountFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@CouponAmountTo", couponPerSaesObj.CouponAmountTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@GrandTotalAmountFrom", couponPerSaesObj.GrandTotalAmountFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@GrandTotalAmountTo", couponPerSaesObj.GrandTotalAmountTo));
                SQLHandler sqlH = new SQLHandler();
                List<CouponPerSalesViewDetailInfo> lstCoupPSVDetail = sqlH.ExecuteAsList<CouponPerSalesViewDetailInfo>("[dbo].[usp_Aspx_GetCouponPerSalesDetailView]",
                                                                        parameterCollection);
                return lstCoupPSVDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Coupon Setting Manage/Admin
        public static void DeleteCouponSettingsKey(string settingID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", settingID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponSettingsKey", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CouponSettingKeyInfo> CouponSettingManageKey()
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                List<CouponSettingKeyInfo> lstCoupSetting = sqlH.ExecuteAsList<CouponSettingKeyInfo>("usp_Aspx_GetAllCouponSettingsKey");
                return lstCoupSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddUpdateCouponSettingKey(int ID, string settingKey, int validationTypeID, string isActive, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", ID));
                parameter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                parameter.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponSettingKey", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Front Coupon Show
        public static List<CouponDetailFrontInfo> GetCouponDetailListFront(int count, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
                SQLHandler sqlH = new SQLHandler();
                List<CouponDetailFrontInfo> lstCoupDetail = sqlH.ExecuteAsList<CouponDetailFrontInfo>("usp_Aspx_GetCouponDetailsForFront", parameter);
                return lstCoupDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        public static bool CheckCouponTypeUniqueness(AspxCommonInfo aspxCommonObj, int couponTypeId, string couponType)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeId));
                Parameter.Add(new KeyValuePair<string, object>("@CouponType", couponType));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCouponTypeUniquness]", Parameter, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
