using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxStoreCloseController
    {
       public AspxStoreCloseController()
       {
       }
       public static void SaveStoreClose(System.Nullable<bool> temporary, System.Nullable<bool> permanent, System.Nullable<DateTime> closeFrom, System.Nullable<DateTime> closeTill, int storeID, int portalID, string userName)
       {
           try
           {
              AspxStoreCloseProvider.SaveStoreClose(temporary,permanent,closeFrom,closeTill,storeID,portalID,userName);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
