using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxAdminDashProvider
    {
       public AspxAdminDashProvider()
       {

       }

       public static List<CategoryQuantityStatics> GetTopCategoryByItemSell(int top, int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           parameter.Add(new KeyValuePair<string, object>("@Top", top));
           SQLHandler sqlH = new SQLHandler();
           List<CategoryQuantityStatics> lstCat =
               sqlH.ExecuteAsList<CategoryQuantityStatics>("usp_Aspx_GetTopCategoriesBySell", parameter);
           return lstCat;
       }
       public static List<CategoryRevenueStatics> GetTopCategoryByHighestRevenue(int top, int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           parameter.Add(new KeyValuePair<string, object>("@Top", top));
           SQLHandler sqlH = new SQLHandler();
           List<CategoryRevenueStatics> lstCat =
               sqlH.ExecuteAsList<CategoryRevenueStatics>("usp_Aspx_GetTopCategoriesByHighestRevenue", parameter);
           return lstCat;
       }

       public static List<VisitorOrderStatics> GetVisitorsOrder(int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day)); 
           SQLHandler sqlH = new SQLHandler();
           List<VisitorOrderStatics> lstVisitor =
               sqlH.ExecuteAsList<VisitorOrderStatics>("usp_Aspx_GetVisitorAndOrderRatio", parameter);
           return lstVisitor;
       }
       public static List<VisitorNewAccountStatics> GetVisitorsNewAccount(int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           SQLHandler sqlH = new SQLHandler();
           List<VisitorNewAccountStatics> lstVisitor =
               sqlH.ExecuteAsList<VisitorNewAccountStatics>("usp_Aspx_GetVisitorNewAccountRatio", parameter);
           return lstVisitor;
       }

       public static List<VisitorNewOrderStatics> GetVisitorsNewOrder(int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           SQLHandler sqlH = new SQLHandler();
           List<VisitorNewOrderStatics> lstVisitor =
               sqlH.ExecuteAsList<VisitorNewOrderStatics>("usp_Aspx_GetNewAccountNewOrderRatio", parameter);
           return lstVisitor;
       }
       public static List<RefundStatics> GetTotalRefund(int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           SQLHandler sqlH = new SQLHandler();
           List<RefundStatics> lstRefund =
               sqlH.ExecuteAsList<RefundStatics>("usp_GetTotalRefunded", parameter);
           return lstRefund;
       }


       public static List<RefundReasonStatics> GetTopRefundReason(int day, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Day", day));
           SQLHandler sqlH = new SQLHandler();
           List<RefundReasonStatics> lstRefund =
               sqlH.ExecuteAsList<RefundReasonStatics>("usp_GetTopFiveReasonsToRefund", parameter);
           return lstRefund;
       }

       public static List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, AspxCommonInfo aspxCommonObj)
       {

           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Count", count));
           SQLHandler sqlH = new SQLHandler();
           List<LatestOrderStaticsInfo> lstLOSI = sqlH.ExecuteAsList<LatestOrderStaticsInfo>("usp_Aspx_GetLatestOrderStatics", parameter);
           return lstLOSI;
       }

       public static List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Count", count));
           SQLHandler sqlH = new SQLHandler();
           List<MostViewItemInfoAdminDash> lstMVI= sqlH.ExecuteAsList<MostViewItemInfoAdminDash>("usp_Aspx_GetMostViewdItemAdminDashboard", parameter);
           return lstMVI;
       }

       public static List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int day,AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj); 
               parameter.Add(new KeyValuePair<string, object>("@Day", day));
               SQLHandler sqlH = new SQLHandler();
               List<StaticOrderStatusAdminDashInfo> lstSOS= sqlH.ExecuteAsList<StaticOrderStatusAdminDashInfo>("usp_Aspx_GetStaticOrderStatusAdminDash", parameter);
               return lstSOS;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@Count", count));
           SQLHandler sqlH = new SQLHandler();
           List<TopCustomerOrdererInfo> lstTCO= sqlH.ExecuteAsList<TopCustomerOrdererInfo>("usp_Aspx_GetTopCustomerAdmindash", parameter);
           return lstTCO;
       }

       public static List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           SQLHandler sqlH = new SQLHandler();
           List<TotalOrderAmountInfo> lstTOAmount= sqlH.ExecuteAsList<TotalOrderAmountInfo>("usp_Aspx_GetTotalOrderAmountStatus", parameter);
           return lstTOAmount;
       }

       public static List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@LowStockCount", count));
           SQLHandler sqlH = new SQLHandler();
           List<InventoryDetailAdminDashInfo> lstInvDetail= sqlH.ExecuteAsList<InventoryDetailAdminDashInfo>("usp_Aspx_GetInventoryDetailsAdminDash", parameter);
           return lstInvDetail;
       }

       public static List<OrderChartInfo> GetOrderChartDetailsByLastWeek(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<OrderChartInfo> lstOrderChart= sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartDetailsByLastWeek", parameter);
               return lstOrderChart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<OrderChartInfo> GetOrderChartDetailsBycurentMonth(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<OrderChartInfo> lstOrderChart= sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderDetailsByCurrentMonth", parameter);
               return lstOrderChart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<OrderChartInfo> GetOrderChartDetailsByOneYear(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<OrderChartInfo> lstOrderChart = sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartDetailsByOneYear", parameter);
               return lstOrderChart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<OrderChartInfo> GetOrderChartDetailsBy24Hours(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               return sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartBy24hours", parameter); ;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public void UpdateItemRating(ItemReviewBasicInfo ratingManageObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingManageObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("@StatusID", ratingManageObj.StatusID));
               parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", ratingManageObj.ReviewSummary));
               parameter.Add(new KeyValuePair<string, object>("@Review", ratingManageObj.Review));
               parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", ratingManageObj.ItemReviewID));
               parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", ratingManageObj.ViewFromIP));
               parameter.Add(new KeyValuePair<string, object>("@ViewFromCountry", ratingManageObj.viewFromCountry));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", ratingManageObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@UserBy", aspxCommonObj.UserName));
               sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRating", parameter);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static StoreQuickStaticsInfo GetStoreQuickStatics(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               return sqlH.ExecuteAsObject<StoreQuickStaticsInfo>("usp_Aspx_GetStoreQuickStatics", parameter); ;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


    }
}
