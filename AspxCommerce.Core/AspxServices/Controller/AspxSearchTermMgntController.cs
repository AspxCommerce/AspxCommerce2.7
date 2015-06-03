using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public class AspxSearchTermMgntController
    {
      public AspxSearchTermMgntController()
      {
      }

      public static List<SearchTermInfo> ManageSearchTerm(int offset, int? limit, AspxCommonInfo aspxCommonObj, string searchTerm)
      {
          List<SearchTermInfo> lstSearchTerm = AspxSearchTermMgntProvider.ManageSearchTerm(offset, limit, aspxCommonObj, searchTerm);
          return lstSearchTerm;
      }

      public static void DeleteSearchTerm(string Ids, AspxCommonInfo aspxCommonObj)
      {
          AspxSearchTermMgntProvider.DeleteSearchTerm(Ids, aspxCommonObj);
      }

      public static void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
      {
          AspxSearchTermMgntProvider.AddUpdateSearchTerm(hasData, searchTerm, aspxCommonObj);
      }
    }
}
