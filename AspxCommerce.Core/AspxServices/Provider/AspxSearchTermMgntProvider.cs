using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxSearchTermMgntProvider
    {
       public AspxSearchTermMgntProvider()
       {
       }
  
       public static List<SearchTermInfo> ManageSearchTerm(int offset, int? limit, AspxCommonInfo aspxCommonObj, string searchTerm)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Offset", offset));
           parameter.Add(new KeyValuePair<string, object>("@Limit", limit));
           parameter.Add(new KeyValuePair<string, object>("@SearchTerm", searchTerm));
           SQLHandler sqlH = new SQLHandler();
           List<SearchTermInfo> lstSearchTerm= sqlH.ExecuteAsList<SearchTermInfo>("usp_Aspx_GetSearchTermDetails", parameter);
           return lstSearchTerm;
       }

       public static void DeleteSearchTerm(string Ids, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@SearchTermID", Ids));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteSearchTerm", parameter);
       }

       public static void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@HasData", hasData));
           parameter.Add(new KeyValuePair<string, object>("@SearchTerm", searchTerm));
           SQLHandler sqlh = new SQLHandler();
           sqlh.ExecuteNonQuery("usp_Aspx_AddUpdateSearchTerm", parameter);
       }
    }
}
