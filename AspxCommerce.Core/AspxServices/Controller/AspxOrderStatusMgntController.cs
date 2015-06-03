using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxOrderStatusMgntController
    {
       public AspxOrderStatusMgntController()
       {
       }
       //------------------------bind Allorder status name list-------------------------------    
       
       public static List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string orderStatusName, System.Nullable<bool> isActive)
       {
           try
           {
               List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntProvider.GetAllStatusList(offset, limit, aspxCommonObj, orderStatusName, isActive);
               return lstOrderStat;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<OrderStatusListInfo> AddUpdateOrderStatus(AspxCommonInfo aspxCommonObj, SaveOrderStatusInfo SaveOrderStatusObj)
       {
           try
           {
               List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntProvider.AddUpdateOrderStatus(aspxCommonObj, SaveOrderStatusObj);
               return lstOrderStat;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static OrderStatusListInfo GetOrderStatusDetailByOrderStatusID(AspxCommonInfo aspxCommonObj,int OrderStatusID)
       {
           try
           {
               OrderStatusListInfo lstOrderStat = AspxOrderStatusMgntProvider.GetOrderStatusDetailByOrderStatusID(aspxCommonObj, OrderStatusID);
               return lstOrderStat;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    
       public static void DeleteOrderStatusByID(int orderStatusID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxOrderStatusMgntProvider.DeleteOrderStatusByID(orderStatusID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteOrderStatusMultipleSelected(string orderStatusIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxOrderStatusMgntProvider.DeleteOrderStatusMultipleSelected(orderStatusIDs, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckOrderStatusUniqueness(AspxCommonInfo aspxCommonObj, int orderStatusId, string orderStatusAliasName)
       {
           try
           {
               bool isUnique = AspxOrderStatusMgntProvider.CheckOrderStatusUniqueness(aspxCommonObj, orderStatusId, orderStatusAliasName);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
