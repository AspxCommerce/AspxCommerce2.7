using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxShipMethodMgntController
    {
       public AspxShipMethodMgntController()
       {
       }
       //-----------Bind Shipping methods In grid-----------------------------

       public static List<ShippingMethodInfo> GetStoreProvidersAvailableMethod(int providerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShippingMethodInfo> lstShipMethod = AspxShipMethodMgntProvider.GetStoreProvidersAvailableMethod(providerId, aspxCommonObj);
               return lstShipMethod;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static List<ShippingMethodInfoByProvider> GetShippingMethodsByProvider(int offset, int limit, int shippingProviderId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShippingMethodInfoByProvider> shipping = AspxShipMethodMgntProvider.GetShippingMethodsByProvider(offset, limit, shippingProviderId, aspxCommonObj);
               return shipping;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ShippingMethodInfo> GetShippingMethods(int offset, int limit, ShippingMethodInfoByProvider shippingMethodObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShippingMethodInfo> shipping = AspxShipMethodMgntProvider.GetShippingMethods(offset, limit, shippingMethodObj, aspxCommonObj);
               return shipping;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------------delete multiple shipping methods----------------------

       public static void DeleteShippings(string shippingMethodIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.DeleteShippings(shippingMethodIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //----------------bind shipping service list---------------

       public static List<ShippingProviderListInfo> GetShippingProviderList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShippingProviderListInfo> lstShipProvider = AspxShipMethodMgntProvider.GetShippingProviderList(aspxCommonObj);
               return lstShipProvider;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------------------SaveAndUpdate shipping methods-------------------

       public static void SaveAndUpdateShippings(int shippingMethodID, string shippingMethodName, string imagePath, string alternateText, int displayOrder, string deliveryTime,
              decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID, bool isActive, AspxCommonInfo aspxCommonObj)
       {
           try
           {

               AspxShipMethodMgntProvider.SaveAndUpdateShippings(shippingMethodID, shippingMethodName, imagePath, alternateText, displayOrder, deliveryTime, weightLimitFrom, weightLimitTo, shippingProviderID, isActive, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------------bind Cost dependencies  in Grid--------------------------

       public static List<ShippingCostDependencyInfo> GetCostDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
       {
           try
           {
               List<ShippingCostDependencyInfo> bind = AspxShipMethodMgntProvider.GetCostDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------------bind Weight dependencies  in Grid--------------------------

       public static List<ShippingWeightDependenciesInfo> GetWeightDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
       {
           try
           {
               List<ShippingWeightDependenciesInfo> bind = AspxShipMethodMgntProvider.GetWeightDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------------bind Item dependencies  in Grid--------------------------

       public static List<ShippingItemDependenciesInfo> GetItemDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
       {
           try
           {
               List<ShippingItemDependenciesInfo> bind = AspxShipMethodMgntProvider.GetItemDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------Delete multiple cost Depandencies --------------------------

       public static void DeleteCostDependencies(string shippingProductCostIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.DeleteCostDependencies(shippingProductCostIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------Delete multiple weight Depandencies --------------------------

       public static void DeleteWeightDependencies(string shippingProductWeightIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.DeleteWeightDependencies(shippingProductWeightIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------Delete multiple item Depandencies --------------------------

       public static void DeleteItemDependencies(string shippingItemIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.DeleteItemDependencies(shippingItemIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------save  cost dependencies----------------

       public static void AddCostDependencies(int shippingProductCostID, int shippingMethodID, string costDependenciesOptions, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.AddCostDependencies(shippingProductCostID, shippingMethodID, costDependenciesOptions, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------- save weight dependencies-------------------------------

       public static void AddWeightDependencies(int shippingProductWeightID, int shippingMethodID, string weightDependenciesOptions, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.AddWeightDependencies(shippingProductWeightID, shippingMethodID, weightDependenciesOptions, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------- save item dependencies-------------------------------

       public static void AddItemDependencies(int shippingItemID, int shippingMethodID, string itemDependenciesOptions, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxShipMethodMgntProvider.AddItemDependencies(shippingItemID, shippingMethodID, itemDependenciesOptions, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckUniquenessForDisplayOrder(AspxCommonInfo aspxCommonObj, int value, int shippingMethodID)
       {
           try
           {
               bool isUnique = AspxShipMethodMgntProvider.CheckUniquenessForDisplayOrder(aspxCommonObj, value, shippingMethodID);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------------Shipping Reports--------------------
       public static List<ShippedReportInfo> GetShippedDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ShippedReportBasicInfo ShippedReportObj)
       {
           try
           {
               List<ShippedReportInfo> lstShipReport = AspxShipMethodMgntProvider.GetShippedDetails(offset, limit, aspxCommonObj, ShippedReportObj);
               return lstShipReport;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       //--ShipmentsListManagement

       public static List<ShipmentsDetailsInfo> GetShipmentsDetails(int offset, System.Nullable<int> limit, ShipmentsBasicinfo shipmentObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShipmentsDetailsInfo> lstShipmentDet = AspxShipMethodMgntProvider.GetShipmentsDetails(offset, limit, shipmentObj, aspxCommonObj);
               return lstShipmentDet;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------View Shipments Details--------------------------

       public static List<ShipmentsDetailsViewInfo> BindAllShipmentsDetails(int orderID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShipmentsDetailsViewInfo> lstShipDetView = AspxShipMethodMgntProvider.BindAllShipmentsDetails(orderID, aspxCommonObj);
               return lstShipDetView;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
