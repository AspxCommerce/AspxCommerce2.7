using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class TrackController
    {

       public List<ShippingProvider> GetProviderList(AspxCommonInfo commonInfo)
       {
           try
           {
               SQLHandler sqlHandler = new SQLHandler();
               List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
               paramList.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
               paramList.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
               List<ShippingProvider> list = sqlHandler.ExecuteAsList<ShippingProvider>("usp_Aspx_GetShippingProviderToTrack", paramList);
               return list;

           }
           catch (Exception ex)
           {

               throw ex;
           }

       }

    }
}
