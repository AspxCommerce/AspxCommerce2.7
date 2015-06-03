using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxAdminDashController
    {
       public AspxAdminDashController()
       {
       }

       public static List<CategoryQuantityStatics> GetTopCategoryByItemSell(int top, int day, AspxCommonInfo aspxCommonObj)
       { 
           
           List<CategoryQuantityStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByItemSell(top, day, aspxCommonObj);
           return lstCat;
       }
       public static List<CategoryRevenueStatics> GetTopCategoryByHighestRevenue(int top, int day, AspxCommonInfo aspxCommonObj)
       {
           List<CategoryRevenueStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByHighestRevenue(top, day, aspxCommonObj);
           return lstCat;
       }

       public static List<VisitorOrderStatics> GetVisitorsOrder(int day, AspxCommonInfo aspxCommonObj)
       {
           List<VisitorOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsOrder( day, aspxCommonObj);
           return lstVisitor;
       }
       public static List<VisitorNewAccountStatics> GetVisitorsNewAccount(int day, AspxCommonInfo aspxCommonObj)
       {
           List<VisitorNewAccountStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewAccount(day, aspxCommonObj);
           return lstVisitor;
       }

       public static List<VisitorNewOrderStatics> GetVisitorsNewOrder(int day, AspxCommonInfo aspxCommonObj)
       {
           List<VisitorNewOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewOrder(day, aspxCommonObj);
           return lstVisitor;
       }
       public static List<RefundStatics> GetTotalRefund(int day, AspxCommonInfo aspxCommonObj)
       {
           List<RefundStatics> lstRefund = AspxAdminDashProvider.GetTotalRefund(day, aspxCommonObj);
           return lstRefund;
       }


       public static List<RefundReasonStatics> GetTopRefundReason(int day, AspxCommonInfo aspxCommonObj)
       {
           List<RefundReasonStatics> lstRefund = AspxAdminDashProvider.GetTopRefundReason(day, aspxCommonObj);
           return lstRefund;
       }

       public static List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, AspxCommonInfo aspxCommonObj)
       {
           List<LatestOrderStaticsInfo> lstLOSI = AspxAdminDashProvider.GetLatestOrderItems(count, aspxCommonObj);
           return lstLOSI;
       }

       public static List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, AspxCommonInfo aspxCommonObj)
       {
          
            List<MostViewItemInfoAdminDash> lstMVI= AspxAdminDashProvider.GetMostViwedItemAdmindash(count, aspxCommonObj);
            return lstMVI;
       }

       public static List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int day, AspxCommonInfo aspxCommonObj)
       {
           try
           {

               List<StaticOrderStatusAdminDashInfo> lstSOS= AspxAdminDashProvider.GetStaticOrderStatusAdminDash(day,aspxCommonObj);
               return lstSOS;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, AspxCommonInfo aspxCommonObj)
       {

           List<TopCustomerOrdererInfo> lstTCO = AspxAdminDashProvider.GetTopCustomerOrderAdmindash(count,aspxCommonObj);
           return lstTCO;
       }

       public static List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(AspxCommonInfo aspxCommonObj)
       {

           List<TotalOrderAmountInfo> lstTOAmount = AspxAdminDashProvider.GetTotalOrderAmountAdmindash(aspxCommonObj);
           return lstTOAmount;
       }

       public static List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, AspxCommonInfo aspxCommonObj)
       {
           List<InventoryDetailAdminDashInfo> lstInvDetail = AspxAdminDashProvider.GetInventoryDetails(count,aspxCommonObj);
           return lstInvDetail;
       }

       public static List<OrderChartInfo> GetOrderChartDetailsByLastWeek(AspxCommonInfo aspxCommonObj)
       {
           try
           {

               List<OrderChartInfo> lstOrderChart = AspxAdminDashProvider.GetOrderChartDetailsByLastWeek(aspxCommonObj);
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
               List<OrderChartInfo> lstOrderChart = AspxAdminDashProvider.GetOrderChartDetailsBycurentMonth(aspxCommonObj);
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
               List<OrderChartInfo> lstOrderChart = AspxAdminDashProvider.GetOrderChartDetailsByOneYear(aspxCommonObj);
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

               List<OrderChartInfo> lstOrderChart = AspxAdminDashProvider.GetOrderChartDetailsBy24Hours(aspxCommonObj);
               return lstOrderChart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static StoreQuickStaticsInfo GetStoreQuickStatics(AspxCommonInfo aspxCommonObj)
       {
           StoreQuickStaticsInfo lstInvDetail = AspxAdminDashProvider.GetStoreQuickStatics(aspxCommonObj);
           return lstInvDetail;
       }

    }
}
