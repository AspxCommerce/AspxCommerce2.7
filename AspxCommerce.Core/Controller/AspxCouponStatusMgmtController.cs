using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxCouponStatusMgmtController
    {
        public AspxCouponStatusMgmtController()
        {
        }

        public static List<CouponStatusInfo> GetAllCouponStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string couponStatusName, System.Nullable<bool> isActive)
        {
            try
            {
                List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtProvider.GetAllCouponStatusList(offset, limit, aspxCommonObj, couponStatusName, isActive);
                return lstCouponStat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckCouponStatusUniqueness(AspxCommonInfo aspxCommonObj, int couponStatusId, string couponStatusName)
        {
            try
            {
                bool isUnique = AspxCouponStatusMgmtProvider.CheckCouponStatusUniqueness(aspxCommonObj, couponStatusId, couponStatusName);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CouponStatusInfo> AddUpdateCouponStatus(AspxCommonInfo aspxCommonObj, CouponStatusInfo SaveCouponStatusObj)
        {
            try
            {
                List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtProvider.AddUpdateCouponStatus(aspxCommonObj, SaveCouponStatusObj);
                return lstCouponStat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
