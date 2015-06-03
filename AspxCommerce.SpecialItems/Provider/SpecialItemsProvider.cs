using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using AspxCommerce.SpecialItems;
using SageFrame.Web.Utilities;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsProvider
    {
        public List<SpecialItemsInfo> GetSpecialItems(AspxCommonInfo aspxCommonObj, int count)
        {
            List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramCol.Add(new KeyValuePair<string, object>("@count", count));
            SQLHandler sageSQL = new SQLHandler();
            List<SpecialItemsInfo> slInfo = sageSQL.ExecuteAsList<SpecialItemsInfo>("[dbo].[usp_Aspx_GetSpecialItems]", paramCol);
            return slInfo;
        }

        public SpecialItemsSettingInfo GetSpecialItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                SpecialItemsSettingInfo objHDSetting = new SpecialItemsSettingInfo();
                objHDSetting = sqlH.ExecuteAsObject<SpecialItemsSettingInfo>("[dbo].[usp_Aspx_SpecialItemsSettingsGet]", parameterCollection);
                return objHDSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SaveAndUpdateSpecialItemsSetting(AspxCommonInfo aspxCommonObj, SpecialItemsSettingKeyPairInfo specialObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", specialObj.SettingKey));
            parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues", specialObj.SettingValue));           
            SQLHandler sqlhandle = new SQLHandler();
            sqlhandle.ExecuteNonQuery("[dbo].[usp_Aspx_SpecialItemsSettingsUpdate]", parameterCollection);
        }

        public List<RssFeedItemInfo> GetItemRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedItemInfo>();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                switch (rssOption)
                {
                    case "specialitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("[dbo].[usp_Aspx_GetRssFeedSpecialItem]", Parameter);
                        break;
                }
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialItemsInfo> GetAllSpecialItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
                SQLHandler sqlH = new SQLHandler();
                if (sortBy == 1)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByItemIDDesc]";
                }
                else if (sortBy == 2)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByItemIDAsc]";
                }
                else if (sortBy == 3)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByPriceDesc]";
                }
                else if (sortBy == 4)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByPriceAsc]";
                }
                else if (sortBy == 5)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByName]";
                }
                else if (sortBy == 6)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByViewCount]";
                }
                else if (sortBy == 7)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByIsFeatured]";
                }
                else if (sortBy == 8)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByIsSpecial]";
                }
                else if (sortBy == 9)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortBySoldItem]";
                }
                else if (sortBy == 10)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByDiscount]";
                }
                else if (sortBy == 11)
                {
                    spName = "[dbo].[usp_Aspx_GetSpecialItemDetailsSortByRatedValue]";
                }
                List<SpecialItemsInfo> lstCatDetail = sqlH.ExecuteAsList<SpecialItemsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetSpecialItemsandSettingDataSet(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            SQLHandler SQLH = new SQLHandler();
            return SQLH.ExecuteAsDataSet("[dbo].[usp_Aspx_GetSpecialItemsInfo]", parameter);
        }
    }
}
