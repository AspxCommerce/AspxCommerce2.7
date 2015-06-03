using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Common;
using SageFrame.Web.Utilities;

namespace AspxCommerce.MegaCategory
{
    public class MegaCategoryProvider
    {
        public MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                MegaCategorySettingInfo view =
                    sqlH.ExecuteAsObject<MegaCategorySettingInfo>("[dbo].[usp_Aspx_GetMegaCategorySetting]", paramCol);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
                SQLHandler sqlH = new SQLHandler();
                List<MegaCategorySettingInfo> view =
                    sqlH.ExecuteAsList<MegaCategorySettingInfo>("[dbo].[usp_Aspx_MegaCategorySettingUpdate]", paramCol);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MegaCategoryViewInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
        {

            List<MegaCategoryViewInfo> catInfo = new List<MegaCategoryViewInfo>();
            if (!CacheHelper.Get("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName, out catInfo))
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sageSQL = new SQLHandler();
                catInfo = sageSQL.ExecuteAsList<MegaCategoryViewInfo>("[dbo].[usp_Aspx_GetMegaCategoryMenuAttributes]", paramCol);
                CacheHelper.Add(catInfo, "CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
            }
            return catInfo;
        }

    }
}
