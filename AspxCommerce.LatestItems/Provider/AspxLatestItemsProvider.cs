using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.LatestItems
{
    public class AspxLatestItemsProvider
    {
        public AspxLatestItemsProvider()
        {
        }

        public List<LatestItemsInfo> GetLatestItemsByCount(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<LatestItemsInfo>("[dbo].[usp_Aspx_LatestItemsGetByCount]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetLatestItemsByCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsDataSet("[dbo].[usp_Aspx_GetLatestItemInfo]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LatestItemSettingInfo GetLatestItemSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                LatestItemSettingInfo objLatestSetting = new LatestItemSettingInfo();
                objLatestSetting = sqlH.ExecuteAsObject<LatestItemSettingInfo>("[dbo].[usp_Aspx_LatestItemSettingGet]", parameterCollection);
                return objLatestSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LatestItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
                SQLHandler sqLH = new SQLHandler();
                sqLH.ExecuteNonQuery("[dbo].[usp_Aspx_LatestItemSettingsUpdate]", parameterCollection);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LatestItemRssInfo> GetLatestItemRssContent(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<LatestItemRssInfo> rssFeedContent = new List<LatestItemRssInfo>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                rssFeedContent = SQLH.ExecuteAsList<LatestItemRssInfo>("[dbo].[usp_Aspx_GetRssFeedLatestItem]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
