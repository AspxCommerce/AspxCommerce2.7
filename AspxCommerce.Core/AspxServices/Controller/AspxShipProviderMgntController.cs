using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxShipProviderMgntController
    {
        public AspxShipProviderMgntController()
        {
        }

        public static List<ShippingProviderNameListInfo> GetShippingProviderNameList(int offset, int limit, AspxCommonInfo aspxCommonObj, string shippingProviderName, System.Nullable<bool> isActive)
        {
            try
            {
                List<ShippingProviderNameListInfo> lstShipProvider = AspxShipProviderMgntProvider.GetShippingProviderNameList(offset, limit, aspxCommonObj, shippingProviderName, isActive);
                return lstShipProvider;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string LoadProviderSetting(int providerId, AspxCommonInfo aspxCommonObj)
        {
            string retStr = AspxShipProviderMgntProvider.LoadProviderSetting(providerId, aspxCommonObj);
            return retStr;
        }


        public static void ShippingProviderAddUpdate(List<ShippingMethod> methods,
            ShippingProvider provider, bool isAddedZip, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntProvider.ShippingProviderAddUpdate(methods, provider, isAddedZip, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteShippingProviderByID(int shippingProviderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntProvider.DeleteShippingProviderByID(shippingProviderID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void DeleteShippingProviderMultipleSelected(string shippingProviderIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntProvider.DeleteShippingProviderMultipleSelected(shippingProviderIDs, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckShippingProviderUniqueness(AspxCommonInfo aspxCommonObj, int shippingProviderId, string shippingProviderName)
        {
            try
            {
                bool isUnique = AspxShipProviderMgntProvider.CheckShippingProviderUniqueness(aspxCommonObj, shippingProviderId, shippingProviderName);
                return isUnique;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void DeactivateShippingMethod(int shippingMethodId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            AspxShipProviderMgntProvider.DeactivateShippingMethod(shippingMethodId, aspxCommonObj, isActive);
        }

        public static List<ProviderShippingMethod> GetProviderRemainingMethod(int shippingProviderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ProviderShippingMethod> services = AspxShipProviderMgntProvider.GetProviderRemainingMethod(shippingProviderId, aspxCommonObj);
                return services;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public int SaveUpdateProviderSetting(ShippingProvider provider, string settingKey, string settingValue, AspxCommonInfo commonInfo)
        {
            try
            {
                int providerID = AspxShipProviderMgntProvider.SaveUpdateProviderSetting(provider, settingKey, settingValue, commonInfo);
                return providerID;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveSetting(int providerId, string settingKey, string settingValue, AspxCommonInfo info)
        {
            try
            {
                AspxShipProviderMgntProvider.SaveSetting(providerId, settingKey, settingValue, info);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveShippingLabelInfo(ShippingLabelInfo labelInfo, AspxCommonInfo commonInfo)
        {
            try
            {
                AspxShipProviderMgntProvider.SaveShippingLabelInfo(labelInfo, commonInfo);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public bool IsShippingLabelCreated(int orderId, AspxCommonInfo commonInfo)
        {
            try
            {
                bool isCreated = AspxShipProviderMgntProvider.IsShippingLabelCreated(orderId, commonInfo);
                return isCreated;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ShippingLabelInfo GetShippingLabelInfo(int orderId, AspxCommonInfo commonInfo)
        {
            try
            {
                ShippingLabelInfo info = AspxShipProviderMgntProvider.GetShippingLabelInfo(orderId, commonInfo);
                return info;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
