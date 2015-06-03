using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;

namespace AspxCommerce.YouMayAlsoLike
{
    public class AspxYouMayAlsoLikeProvider
    {
        public AspxYouMayAlsoLikeProvider()
        { 
        }
        public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItemsListByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                List<YouMayAlsoLikeInfo> lstYouMayLike = sqlH.ExecuteAsList<YouMayAlsoLikeInfo>("usp_Aspx_GetYouMayAlsoLikeItemsByItemSKU", parameter);
                return lstYouMayLike;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItems(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                List<YouMayAlsoLikeInfo> lstYouMayLike = sqlH.ExecuteAsList<YouMayAlsoLikeInfo>("usp_Aspx_GetYouMayAlsoLikeItems", parameter);
                return lstYouMayLike;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public YouMayAlsoLikeSettingInfo GetYouMayAlsoLikeSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                YouMayAlsoLikeSettingInfo view =
                    sqlH.ExecuteAsObject<YouMayAlsoLikeSettingInfo>("[dbo].[usp_Aspx_YouMayAlsoLikeSettingGet]", paramCol);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void YouMayAlsoLikeSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
                SQLHandler sqlH = new SQLHandler();               
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_YouMayAlsoLikeSettingsUpdate]", paramCol);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
