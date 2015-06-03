using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxReturnRequestMgntController
    {
       public AspxReturnRequestMgntController()
       {
       }

       public static void ReturnSaveUpdateSettings(AspxCommonInfo aspxCommonObj, int expiresInDays)
       {
           try
           {
               AspxReturnRequestMgntProvider.ReturnSaveUpdateSettings(aspxCommonObj, expiresInDays);
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
               List<ReturnsSettingsInfo> info = AspxReturnRequestMgntProvider.ReturnGetSettings(aspxCommonObj);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
