using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.Core
{
   public class StoreSettingProvider
    {
       public StoreSettingProvider()
       {
       }

       public DataSet GetStoreSettingsByPortal(int storeID,int portalID, string cultureName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
               parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               DataSet ds = new DataSet();
               SQLHandler sagesql = new SQLHandler();
               ds = sagesql.ExecuteAsDataSet("usp_Aspx_GetStoreSettingForCache", parameterCollection);
               return ds;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public DataTable GetStoreSettings(int storeID, int portalID, string cultureName)
       {
           try
           {
               DataTable dt = new DataTable();
               DataSet ds = GetStoreSettingsByPortal(storeID, portalID, cultureName);
               if (ds != null && ds.Tables != null && ds.Tables[0] != null)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
