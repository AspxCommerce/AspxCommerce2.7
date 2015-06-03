using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxOrderStatusMgntProvider
    {
       public AspxOrderStatusMgntProvider()
       {
       }
       //------------------------bind Allorder status name list-------------------------------    

       public static List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string orderStatusName, System.Nullable<bool> isActive)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@OrderStatusName", orderStatusName));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               SQLHandler sqlH = new SQLHandler();
               List<OrderStatusListInfo> lstOrderStat = sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_GetOrderAliasStatusList]", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", SaveOrderStatusObj.OrderStatusID));
               parameter.Add(new KeyValuePair<string, object>("@OrderStatusAliasName", SaveOrderStatusObj.OrderStatusAliasName));
               parameter.Add(new KeyValuePair<string, object>("@AliasToolTip", SaveOrderStatusObj.AliasToolTip));
               parameter.Add(new KeyValuePair<string, object>("@AliasHelp", SaveOrderStatusObj.AliasHelp));
               parameter.Add(new KeyValuePair<string, object>("@IsSystem", SaveOrderStatusObj.IsSystemUsed));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", SaveOrderStatusObj.IsActive));
               parameter.Add(new KeyValuePair<string, object>("@IsReduceQuantity", SaveOrderStatusObj.IsReduceInQuantity));
               SQLHandler sqlH = new SQLHandler();
               List<OrderStatusListInfo> lstOrderStat = sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_OrderStatusAddUpdate]", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", OrderStatusID));
               SQLHandler sqlH = new SQLHandler();
               OrderStatusListInfo lstOrderStat = sqlH.ExecuteAsObject<OrderStatusListInfo>("[dbo].[usp_Aspx_GetOrderStatusDetailByOrderStatusID]", parameter);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusByID]", parameterCollection);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderStatusIDs", orderStatusIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusMultipleSelected]", parameterCollection);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusId));
               Parameter.Add(new KeyValuePair<string, object>("@OrderStatusName", orderStatusAliasName));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckOrderStatusUniquness]", Parameter, "@IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
