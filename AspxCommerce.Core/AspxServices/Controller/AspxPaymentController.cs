using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxPaymentController
    {
       public AspxPaymentController()
       {
       }

       public static void OrderTaxRuleMapping(int itemID, int orderID, int taxManageRuleID, decimal taxSubTotal, int storeID, int portalID, string addedBy, string costVariantValueIDs)
       {
           try
           {
               AspxPaymentProvider.OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
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
               List<PaymentGateWayInfo> lstPayGateWay = AspxPaymentProvider.GetAllPaymentMethod(offset, limit, aspxCommonObj, paymentMethodObj);
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
               List<TransactionInfoList> lstTransaction = AspxPaymentProvider.GetAllTransactionDetail(aspxCommonObj, paymentGatewayID, orderID);
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
               AspxPaymentProvider.DeletePaymentMethod(paymentGatewayID, aspxCommonObj);
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
               AspxPaymentProvider.UpdatePaymentMethod(aspxCommonObj, updatePaymentObj);
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
               AspxPaymentProvider.AddUpdatePaymentGateWaySettings(paymentGatewaySettingValueID, paymentGatewayID, settingKeys, settingValues, isActive, aspxCommonObj);
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
               List<GetOrderdetailsByPaymentGatewayIDInfo> lstOrderDetail = AspxPaymentProvider.GetOrderDetailsbyPayID(offset, limit, bindOrderObj, aspxCommonObj);
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
               List<OrderDetailsByOrderIDInfo> lstOrderDetail = AspxPaymentProvider.GetAllOrderDetailsByOrderID(orderId, storeId, portalId);
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
               List<OrderItemsInfo> lstOrderItem = AspxPaymentProvider.GetAllOrderDetailsForView(orderId, aspxCommonObj);
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
               List<OrderItemsTaxInfo> lstOrderItem = AspxPaymentProvider.GetTaxDetailsByOrderID(orderId, aspxCommonObj);
               return lstOrderItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
