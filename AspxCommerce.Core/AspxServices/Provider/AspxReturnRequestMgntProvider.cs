using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxReturnRequestMgntProvider
    {
       public AspxReturnRequestMgntProvider()
       {
       }

       public static void ReturnSaveUpdateSettings(AspxCommonInfo aspxCommonObj, int expiresInDays)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);               
               parameter.Add(new KeyValuePair<string, object>("@ExpiresInDays", expiresInDays));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_ReturnSaveUpdateSettings", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static List<ReturnsSettingsInfo> ReturnGetSettings(AspxCommonInfo aspxCommonObj)      
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);              
               SQLHandler sqlH = new SQLHandler();
               List<ReturnsSettingsInfo> info = sqlH.ExecuteAsList<ReturnsSettingsInfo>("usp_Aspx_ReturnGetSettings", parameter);
               return info;             
              
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
