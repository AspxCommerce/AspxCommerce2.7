using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using AspxCommerce.USPS.Entity;
using SageFrame.Web.Utilities;

namespace AspxCommerce.USPS.Provider
{
    public static class USPSProvider
    {
        public static UspsSetting GetSetting(int providerId, AspxCommonInfo commonInfo )
        {
            try
            {
                List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
                paraMeter.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
                paraMeter.Add(new KeyValuePair<string, object>("@ShippingProviderID", providerId));
                var sqLh = new SQLHandler();
                UspsSetting setting= sqLh.ExecuteAsObject<UspsSetting>("usp_Aspx_GetUSPSShippingProviderSetting", paraMeter);
                return setting;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private static ShippingProvider GetProviderInfo(int providerId, int storeId, int portalId)
        {
            try
            {
                var paraMeter = new List<KeyValuePair<string, object>>
                                   {
                                       new KeyValuePair<string, object>("@StoreId", storeId),
                                       new KeyValuePair<string, object>("@PortalId", portalId),
                                       new KeyValuePair<string, object>("@ShippingProviderId", providerId)
                                   };
                var sqLh = new SQLHandler();
                return sqLh.ExecuteAsObject<ShippingProvider>("usp_Aspx_GetShippingProviderInfo", paraMeter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static List<ShippingMethodInfo> GetStoreProvidersAvailableMethod(int providerId, int storeId, int portalId)
        {
            try
            {
                var sqlHandler = new SQLHandler();
                var paramList = new List<KeyValuePair<string, object>>
                                   {
                                       new KeyValuePair<string, object>("@StoreID", storeId),
                                       new KeyValuePair<string, object>("@ShippingProviderId", providerId),
                                       new KeyValuePair<string, object>("@PortalID", portalId),
                                       new KeyValuePair<string, object>("@IsActive", true),
                                       new KeyValuePair<string, object>("@CultureName", "en-US")
                                   };
                return sqlHandler.ExecuteAsList<ShippingMethodInfo>("[usp_Aspx_GetProviderShippingMethods]", paramList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
