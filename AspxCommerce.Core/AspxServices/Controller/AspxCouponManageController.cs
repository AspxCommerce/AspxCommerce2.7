using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using SageFrame.Message;

namespace AspxCommerce.Core
{
    public class AspxCouponManageController
    {

        public AspxCouponManageController()
        {
        }
        public static List<CouponInfo> BindAllCouponDetails(int offset, int limit, GetCouponDetailsInfo couponDetailObj, AspxCommonInfo aspxCommonObj)
        {

            List<CouponInfo> lstCoupon = AspxCouponManageProvider.BindAllCouponDetails(offset, limit, couponDetailObj, aspxCommonObj);
            return lstCoupon;
        }

        public static List<CouponPortalUserListInfo> GetPortalUsersList(int offset, int limit, int couponID, string customerName, AspxCommonInfo aspxCommonObj)
        {
            List<CouponPortalUserListInfo> lstCoupUser = AspxCouponManageProvider.GetPortalUsersList(offset, limit, couponID, customerName, aspxCommonObj);
            return lstCoupUser;
        }

        public static void AddUpdateCoupons(CouponSaveObj couponSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageProvider.AddUpdateCoupons(couponSaveObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void AddUpdatPromoCode(PromoCodeSaveObj promoSaveObj,AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageProvider.AddUpdatePromoCode(promoSaveObj, aspxCommonObj);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }       
        public static List<CouponStatusInfo> BindCouponStatus(AspxCommonInfo aspxCommonObj)
        {
            List<CouponStatusInfo> lstCoupStat = AspxCouponManageProvider.BindCouponStatus(aspxCommonObj);
            return lstCoupStat;
        }

        public static List<CouponSettingKeyValueInfo> GetCouponSettingKeyValueInfo(int couponID, AspxCommonInfo aspxCommonObj)
        {
            List<CouponSettingKeyValueInfo> lstCoupKeyVal = AspxCouponManageProvider.GetCouponSettingKeyValueInfo(couponID, aspxCommonObj);
            return lstCoupKeyVal;
        }

        public static void DeleteCoupons(string couponIDs, AspxCommonInfo aspxCommonObj)
        {
            AspxCouponManageProvider.DeleteCoupons(couponIDs, aspxCommonObj);
        }

        public static CouponVerificationInfo VerifyUserCoupon(decimal totalCost, string couponCode,string itemIds,string cartItemIds, AspxCommonInfo aspxCommonObj, int appliedCount)
        {
            CouponVerificationInfo objCoupVeri = AspxCouponManageProvider.VerifyUserCoupon(totalCost, couponCode,itemIds,cartItemIds, aspxCommonObj, appliedCount);
            return objCoupVeri;
        }

        public static void UpdateCouponUserRecord(CouponSession coupon, int storeID, int portalID, string userName, int orderID)
        {
            AspxCouponManageProvider.UpdateCouponUserRecord(coupon, storeID, portalID, userName, orderID);
        }

        public static List<CouponUserInfo> GetCouponUserDetails(int offset, int? limit, GetCouponUserDetailInfo couponUserObj, AspxCommonInfo aspxCommonObj,string userName)
        {
            List<CouponUserInfo> lstCoupUser = AspxCouponManageProvider.GetCouponUserDetails(offset, limit, couponUserObj, aspxCommonObj,userName);
            return lstCoupUser;
        }

        public static List<CouponUserListInfo> GetCouponUserList(int offset, int limit, CouponCommonInfo bindCouponUserObj, AspxCommonInfo aspxCommonObj)
        {
            List<CouponUserListInfo> lstCoupUser = AspxCouponManageProvider.GetCouponUserList(offset, limit, bindCouponUserObj, aspxCommonObj);
            return lstCoupUser;
        }

        public static string GetPromoItemCheckIDs(int CouponID, AspxCommonInfo aspxCommomObj)
        {
            string lstPromoItems = AspxCouponManageProvider.GetPromoItemCheckIDs(CouponID, aspxCommomObj);
            return lstPromoItems;
        }

        public static List<PromoItemsInfo> GetAllPromoItems(int offset,int limit,int couponTypeId,int couponId,AspxCommonInfo aspxCommomOnj)
        {
            List<PromoItemsInfo> lstPromoItems = AspxCouponManageProvider.GetAllPromoItems(offset,limit,couponTypeId,couponId,aspxCommomOnj);
            return lstPromoItems;
        }

        public static List<ItemsForPromoInfo> ItemsForPromoCode(int offset,int limit,AspxCommonInfo aspxCommonObj,string itemName,int? couponId)
        {
            List<ItemsForPromoInfo> lst = AspxCouponManageProvider.ItemsForPromoCode(offset, limit, aspxCommonObj,itemName,couponId);
            return lst;
        }

        public static void DeleteCouponUser(string couponUserID, AspxCommonInfo aspxCommonObj)
        {
            AspxCouponManageProvider.DeleteCouponUser(couponUserID, aspxCommonObj);
        }

        public static void UpdateCouponUser(int couponUserID, int couponStatusID, AspxCommonInfo aspxCommonObj)
        {
            AspxCouponManageProvider.UpdateCouponUser(couponUserID, couponStatusID, aspxCommonObj);
        }

        //public void SendCouponCodeEmail(string senderEmail, string receiverEmailDs, string subject,ArrayList messageBody)
        public static void SendCouponCodeEmail(CouponEmailInfo couponEmailObj)
        {
            AspxCouponManageProvider.SendCouponCodeEmail(couponEmailObj);
        }

        #region Coupon Type Manage

        public static List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponTypeInfo> lstCoupType = AspxCouponManageProvider.GetCouponTypeDetails(offset, limit, couponTypeName, aspxCommonObj);
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
                AspxCouponManageProvider.AddUpdateCouponType(couponTypeObj, aspxCommonObj);
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
                AspxCouponManageProvider.DeleteCouponType(IDs, aspxCommonObj);
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
                List<CouponPerSales> lstCoupPerSale = AspxCouponManageProvider.GetCouponDetailsPerSales(offset, limit, couponCode, aspxCommonObj);
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
                List<CouponPerSalesViewDetailInfo> lstCoupPSVDetail = AspxCouponManageProvider.GetCouponPerSalesDetailView(offset, limit, couponPerSaesObj, aspxCommonObj);
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
                AspxCouponManageProvider.DeleteCouponSettingsKey(settingID, storeID, portalID, userName); 
               
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
                List<CouponSettingKeyInfo> lstCoupSetting = AspxCouponManageProvider.CouponSettingManageKey();
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
                AspxCouponManageProvider.AddUpdateCouponSettingKey(ID, settingKey, validationTypeID, isActive, storeID, portalID, cultureName, userName);
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
                List<CouponDetailFrontInfo> lstCoupDetail = AspxCouponManageProvider.GetCouponDetailListFront(count, aspxCommonObj);
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
                bool isUnique = AspxCouponManageProvider.CheckCouponTypeUniqueness(aspxCommonObj, couponTypeId, couponType);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
