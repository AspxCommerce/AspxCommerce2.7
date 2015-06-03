using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxCouponStatusMgmtProvider
    {
        public AspxCouponStatusMgmtProvider()
        {
        }

        public static List<CouponStatusInfo> GetAllCouponStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string couponStatusName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusName", couponStatusName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                List<CouponStatusInfo> lstCouponStat = sqlH.ExecuteAsList<CouponStatusInfo>("[dbo].[usp_Aspx_GetCouponAliasStatusList]", parameter);
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
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@CouponStatusId", couponStatusId));
                Parameter.Add(new KeyValuePair<string, object>("@CouponStatusName", couponStatusName));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCouponStatusUniquness]", Parameter, "@IsUnique");
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", SaveCouponStatusObj.CouponStatusID));
                parameter.Add(new KeyValuePair<string, object>("@CouponStatusName", SaveCouponStatusObj.CouponStatus));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", SaveCouponStatusObj.IsActive));
                SQLHandler sqlH = new SQLHandler();
                List<CouponStatusInfo> lstCouponStat = sqlH.ExecuteAsList<CouponStatusInfo>("[dbo].[usp_Aspx_CouponStatusAddUpdate]", parameter);
                return lstCouponStat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
