using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxStoreCloseProvider
    {
       public AspxStoreCloseProvider()
       {
       }
       
       public static void SaveStoreClose(System.Nullable<bool> temporary, System.Nullable<bool> permanent, System.Nullable<DateTime> closeFrom, System.Nullable<DateTime> closeTill, int storeID, int portalID, string userName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@Temporary", temporary));
               parameter.Add(new KeyValuePair<string, object>("@Permanent", permanent));
               parameter.Add(new KeyValuePair<string, object>("@CloseFrom", closeFrom));
               parameter.Add(new KeyValuePair<string, object>("@CloseTill", closeTill));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_SaveStoreClose", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
