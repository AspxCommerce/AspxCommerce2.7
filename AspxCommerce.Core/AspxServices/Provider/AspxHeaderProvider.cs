using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxHeaderProvider
    {
        public AspxHeaderProvider()
       {
       }

       public static int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetCartItemsCount", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public HeaderItemsCount GetHeaderItemsCount(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               return sqlH.ExecuteAsObject<HeaderItemsCount>("[usp_Aspx_GetHeaderItemsCount]", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static int CountWishItems(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetWishItemsCount", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static HeaderSettingInfo GetHeaderSetting(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlHandle = new SQLHandler();
               HeaderSettingInfo objHeadSetting = new HeaderSettingInfo();
               objHeadSetting = sqlHandle.ExecuteAsObject<HeaderSettingInfo>("[usp_Aspx_GetHeaderSettings]", parameterCollection);
               return objHeadSetting;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
           parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           parameterCollection.Add(new KeyValuePair<string, object>("@HeaderType", headerType));
           SQLHandler sqlhandle = new SQLHandler();
           sqlhandle.ExecuteNonQuery("[usp_Aspx_UpdateHeaderSettings]", parameterCollection);
       }
    }
}
