using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Common;

namespace AspxCommerce.Core
{
   public class AspxCategoryListProvider
    {
       public AspxCategoryListProvider()
       {
       }

       public static List<CategoryInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
       {

           List<CategoryInfo> catInfo = new List<CategoryInfo>();
           if (!CacheHelper.Get("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID +"_"+ aspxCommonObj.CultureName, out catInfo))
           {
               List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sageSQL = new SQLHandler();
               catInfo = sageSQL.ExecuteAsList<CategoryInfo>("[dbo].[usp_Aspx_GetCategoryMenuAttributes]", paramCol);
               CacheHelper.Add(catInfo, "CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
           }
           return catInfo;
       }

    }
}
