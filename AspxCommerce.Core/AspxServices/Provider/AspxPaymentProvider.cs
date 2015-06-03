using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxPaymentProvider
    {
        public AspxPaymentProvider()
        {
        }

        public static void OrderTaxRuleMapping(int itemID, int orderID, int taxManageRuleID, decimal taxSubTotal, int storeID, int portalID, string addedBy, string costVariantValueIDs)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleID", taxManageRuleID));
                parameter.Add(new KeyValuePair<string, object>("@TaxSubTotal", taxSubTotal));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantsValueIDs", costVariantValueIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_OrderTaxRuleMapping", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PaymentGateWayInfo> GetAllPaymentMethod(int offset, int limit, AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo paymentMethodObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayName", paymentMethodObj.PaymentGatewayName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", paymentMethodObj.IsActive));
                SQLHandler sqlH = new SQLHandler();
                List<PaymentGateWayInfo> lstPayGateWay = sqlH.ExecuteAsList<PaymentGateWayInfo>("usp_Aspx_GetPaymentGateWayMethod", parameterCollection);
                return lstPayGateWay;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<TransactionInfoList> GetAllTransactionDetail(AspxCommonInfo aspxCommonObj, int paymentGatewayID, System.Nullable<int> orderID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                List<TransactionInfoList> lstTransaction = sqlH.ExecuteAsList<TransactionInfoList>("usp_Aspx_GetAllTransactionDetail", parameterCollection);
                return lstTransaction;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeletePaymentMethod(string paymentGatewayID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeletePaymentMethodName", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdatePaymentMethod(AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo updatePaymentObj)
        {
            try
            {
                var fileObj = new FileHelperController();
                updatePaymentObj.DestinationUrl = updatePaymentObj.DestinationUrl.Substring(updatePaymentObj.DestinationUrl.LastIndexOf("Module"));
                updatePaymentObj.LogoUrl = fileObj.MoveFile(updatePaymentObj.LogoUrl, updatePaymentObj.DestinationUrl, updatePaymentObj.OldLogoUrl);
                if (updatePaymentObj.LogoUrl == "Modules/AspxCommerce/AspxPaymentGateWayManagement/Logos/")
                {
                    updatePaymentObj.LogoUrl = "";
                }
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", updatePaymentObj.PaymentGateWayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", updatePaymentObj.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUse", updatePaymentObj.IsUse));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeName", updatePaymentObj.PaymentGatewayName));
                parameterCollection.Add(new KeyValuePair<string, object>("@LogoUrl", updatePaymentObj.LogoUrl));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdatePaymentMethod", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddUpdatePaymentGateWaySettings(int paymentGatewaySettingValueID, int paymentGatewayID, string settingKeys, string settingValues, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewaySettingValueID", paymentGatewaySettingValueID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues ", settingValues));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_GetPaymentGatewaySettingsSaveUpdate", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GetOrderdetailsByPaymentGatewayIDInfo> GetOrderDetailsbyPayID(int offset, int limit, PaymentGateWayBasicInfo bindOrderObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@BillToName", bindOrderObj.BillToName));
                parameter.Add(new KeyValuePair<string, object>("@ShipToName", bindOrderObj.ShipToName));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusAliasName", bindOrderObj.OrderStatusName));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", bindOrderObj.PaymentGateWayID));
                SQLHandler sqlH = new SQLHandler();
                List<GetOrderdetailsByPaymentGatewayIDInfo> lstOrderDetail = sqlH.ExecuteAsList<GetOrderdetailsByPaymentGatewayIDInfo>("usp_Aspx_GetOrderDetailsByPaymentGetwayID", parameter);
                return lstOrderDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<OrderDetailsByOrderIDInfo> GetAllOrderDetailsByOrderID(int orderId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<OrderDetailsByOrderIDInfo> lstOrderDetail = sqlH.ExecuteAsList<OrderDetailsByOrderIDInfo>("usp_Aspx_GetBillingAndShippingAddressDetailsByOrderID", parameterCollection);
                return lstOrderDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<OrderItemsInfo> GetAllOrderDetailsForView(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                SQLHandler sqlH = new SQLHandler();
                List<OrderItemsInfo> lstOrderItem = sqlH.ExecuteAsList<OrderItemsInfo>("usp_Aspx_GetAddressDetailsByOrderID", parameterCollection);
                return lstOrderItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<OrderItemsTaxInfo> GetTaxDetailsByOrderID(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                SQLHandler sqlH = new SQLHandler();
                List<OrderItemsTaxInfo> lstOrderItem = sqlH.ExecuteAsList<OrderItemsTaxInfo>("usp_Aspx_GetTaxDetailsByOrderID", parameterCollection);
                return lstOrderItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
