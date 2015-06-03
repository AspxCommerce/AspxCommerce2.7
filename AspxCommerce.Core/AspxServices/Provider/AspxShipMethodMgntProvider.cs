using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxShipMethodMgntProvider
    {
        public AspxShipMethodMgntProvider()
        {
        }

        //-----------Bind Shipping methods In grid-----------------------------

        public static List<ShippingMethodInfo> GetStoreProvidersAvailableMethod(int providerId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramList.Add(new KeyValuePair<string, object>("@ShippingProviderId", providerId));
                paramList.Add(new KeyValuePair<string, object>("@IsActive", true));
                List<ShippingMethodInfo> lstShipMethod = sqlHandler.ExecuteAsList<ShippingMethodInfo>("[usp_Aspx_GetProviderShippingMethods]", paramList);
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
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderId", shippingProviderId));
                List<ShippingMethodInfoByProvider> shipping = sqlH.ExecuteAsList<ShippingMethodInfoByProvider>("usp_Aspx_GetShippingMethodbyProvider", parameterCollection);
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
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingMethodName", shippingMethodObj.ShippingMethodName));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeliveryTime", shippingMethodObj.DeliveryTime));
                parameterCollection.Add(new KeyValuePair<string, object>("@WeightLimitFrom", shippingMethodObj.WeightLimitFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@WeightLimitTo", shippingMethodObj.WeightLimitTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", shippingMethodObj.IsActive));
                List<ShippingMethodInfo> shipping = sqlH.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_BindShippingMethodInGrid", parameterCollection);
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingMethodIDs", shippingMethodIds));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingMethods", parameterCollection);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<ShippingProviderListInfo> lstShipProvider = sqlH.ExecuteAsList<ShippingProviderListInfo>("usp_Aspx_BindShippingProvider", parameter);
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

                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodName", shippingMethodName));
                parameter.Add(new KeyValuePair<string, object>("@ImagePath", imagePath));
                parameter.Add(new KeyValuePair<string, object>("@AlternateText", alternateText));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@DeliveryTime", deliveryTime));
                parameter.Add(new KeyValuePair<string, object>("@WeightLimitFrom", weightLimitFrom));
                parameter.Add(new KeyValuePair<string, object>("@WeightLimitTo", weightLimitTo));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));              
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateShippingMethods", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingCostDependencyInfo> bind = sqlH.ExecuteAsList<ShippingCostDependencyInfo>("usp_Aspx_BindShippingCostDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingWeightDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingWeightDependenciesInfo>("usp_Aspx_BindWeightDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingItemDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingItemDependenciesInfo>("usp_Aspx_BindItemDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductCostIDs", shippingProductCostIds));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductWeightIDs", shippingProductWeightIds));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingWeightDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingItemIDs", shippingItemIds));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingItemDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductCostID", shippingProductCostID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@CostDependenciesOptions", costDependenciesOptions));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveCostDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductWeightID", shippingProductWeightID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@WeightDependenciesOptions", weightDependenciesOptions));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveWeightDependencies", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShippingItemID", shippingItemID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@ItemDependenciesOptions", itemDependenciesOptions));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveItemDependencies", parameter);
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
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@Value", value));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                bool isUnique= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForDisplayOrder]", Parameter, "@IsUnique");
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
                List<ShippedReportInfo> shipInfo;
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@offset", offset));
                paramCol.Add(new KeyValuePair<string, object>("@limit", limit));
                paramCol.Add(new KeyValuePair<string, object>("@ShppingMethod", ShippedReportObj.ShippingMethodName));
                SQLHandler sageSQL = new SQLHandler();
                if (ShippedReportObj.Monthly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_ShippingReportDetails]", paramCol);
                    return shipInfo;
                }
                if (ShippedReportObj.Weekly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_GetShippingDetailsByCurrentMonth]", paramCol);
                    return shipInfo;
                }
                if (ShippedReportObj.Hourly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_GetShippingReportDetailsBy24hours]", paramCol);
                    return shipInfo;
                }
                else
                    return new List<ShippedReportInfo>();
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodName", shipmentObj.ShippingMethodName));
                parameter.Add(new KeyValuePair<string, object>("@ShipToName", shipmentObj.ShipToName));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", shipmentObj.OrderID));
                SQLHandler sqlH = new SQLHandler();
                List<ShipmentsDetailsInfo> lstShipmentDet= sqlH.ExecuteAsList<ShipmentsDetailsInfo>("usp_Aspx_GetShipmentsDetails", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                List<ShipmentsDetailsViewInfo> lstShipDetView= sqlH.ExecuteAsList<ShipmentsDetailsViewInfo>("usp_Aspx_GetShipmentsDetalisForView", parameter);
                return lstShipDetView;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
