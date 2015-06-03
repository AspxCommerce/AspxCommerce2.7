using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxServiceController
    {
       public AspxServiceController()
       {
       }

       public void SetServiceSessionVariable(string key, object value)
       {         
           System.Web.HttpContext.Current.Session[key] = value;
       }

       public void SaveServiceItem(AspxCommonInfo aspxCommonObj, int categoryId, List<ServiceItemInfo> serviceInfo)
       {
           SQLHandler sqlH = new SQLHandler();
           SqlTransaction tran;
           tran = (SqlTransaction)sqlH.GetTransaction();
           try
           {
               AspxServiceProvider.SaveServiceItem(aspxCommonObj, categoryId, serviceInfo, tran);
               tran.Commit();
           }
           catch (Exception e)
           {
               tran.Rollback();
               throw e;
           }
       }

       public static List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
       {
           List<ServiceItemInfo> serviceInfo=AspxServiceProvider.GetServiceItemInfo(aspxCommonObj,categoryId);
           return serviceInfo;
       }


       public static void DeleteServiceItem(string option, AspxCommonInfo aspxCommonObj, int id)
       {
           try
           {
               AspxServiceProvider.DeleteServiceItem(option, aspxCommonObj, id);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<BookAppointmentGridInfo> GetBookAppointmentList(int offset, int limit, AspxCommonInfo aspxCommonObj, string appointmentStatusName, string branchName, string employeeName)
       {
           try
           {

               List<BookAppointmentGridInfo> lstBookAppoint = AspxServiceProvider.GetBookAppointmentList(offset, limit, aspxCommonObj, appointmentStatusName, branchName, employeeName);
               return lstBookAppoint;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static void DeleteAppointment(string appointmentID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxServiceProvider.DeleteAppointment(appointmentID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static List<BookAppointmentInfo> GetAppointmentDetailByID(int appointmentID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<BookAppointmentInfo> lstBookAppoint = AspxServiceProvider.GetAppointmentDetailByID(appointmentID, aspxCommonObj);
               return lstBookAppoint;
           }
           catch (Exception e)
           {

               throw e;
           }
       }


       public static List<AppointmentStatusInfo> GetAppointmentStatusList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<AppointmentStatusInfo> lstAppointStatus =AspxServiceProvider.GetAppointmentStatusList(aspxCommonObj);
               return lstAppointStatus;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static List<AppointmentSatusInfoBasic> GetAppointmentStatusListGrid(int limit,int offset,string statusName,bool? isActive,AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<AppointmentSatusInfoBasic> lstAppointStatus = AspxServiceProvider.GetAppointmentStatusListGrid(limit,offset,statusName,isActive,aspxCommonObj);
               return lstAppointStatus;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void AddUpdateAppointmentStatus(AspxCommonInfo aspxCommonObj, AppointmentSatusInfoBasic appointmentStatusObj)
       {
           AspxServiceProvider.AddUpdateAppointmentStatus(aspxCommonObj, appointmentStatusObj);
       }     

      
       public static void SaveServiceProvider(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerSaveInfo)
       {
           try
           {
               AspxServiceProvider.SaveServiceProvider(aspxCommonObj, providerSaveInfo);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static List<ServiceProviderInfo> GetServiceProviderNameList(AspxCommonInfo aspxCommonObj, int storeBranchId)
       {
           try
           {
               List<ServiceProviderInfo> lstServProv = AspxServiceProvider.GetServiceProviderNameList(aspxCommonObj,storeBranchId);
               return lstServProv;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ServiceProviderInfo> GetBranchProviderNameList(int offset, int? limit, int storeBranchId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ServiceProviderInfo> lstServProv= AspxServiceProvider.GetBranchProviderNameList(offset,limit,storeBranchId,aspxCommonObj);
               return lstServProv;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteServiceProvider(AspxCommonInfo aspxCommonObj, string id, int storeBranchId)
       {
           try
           {
               AspxServiceProvider.DeleteServiceProvider(aspxCommonObj, id, storeBranchId);
           }
           catch (Exception e)
           {
               throw e;
           }
       }     

     

       public static bool CheckServiceProviderUniqueness(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerUniqueInfo)
       {
           try
           {
               bool isSPUnique = AspxServiceProvider.CheckServiceProviderUniqueness(aspxCommonObj,providerUniqueInfo);
               return isSPUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }       

       public static List<OrderServiceDetailInfo> GetServiceDetailsByOrderID(int orderID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<OrderServiceDetailInfo> lstBookAppoint = AspxServiceProvider.GetServiceDetailsByOrderID(orderID, aspxCommonObj);
               return lstBookAppoint;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static List<BookAppointmentInfo> GetAppointmentDetailsForExport(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<BookAppointmentInfo> lstBookAppoint = AspxServiceProvider.GetAppointmentDetailsForExport(aspxCommonObj);
               return lstBookAppoint;
           }
           catch (Exception e)
           {

               throw e;
           }
       }
       public static List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
       {

           try
           {
               List<StoreLocatorInfo> lstStoreLocator = AspxServiceProvider.GetAllStoresForService(aspxCommonObj, serviceCategoryId);
               return lstStoreLocator;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       #region "Service Management"
      
       public List<ServiceManageInfo> GetServiceEmployeeDetails(int offset, int limit, AspxCommonInfo aspxCommonInfo, int serviceId, int employeeId, int branchID, string itemName)
       {
           try
           {
               List<ServiceManageInfo> list = AspxServiceProvider.GetServiceEmployeeDetails(offset, limit, aspxCommonInfo, serviceId, employeeId, branchID, itemName);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
      
       public List<ServiceGridListInfo> GetAllServiceList(int offset, int limit, AspxCommonInfo aspxCommonInfo, string serviceName, string branchName)
       {
           try
           {
               List<ServiceGridListInfo> list = AspxServiceProvider.GetAllServiceList(offset, limit, aspxCommonInfo, serviceName, branchName);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
       public List<ServiceEmpInfo> GetServiceEmployee(int offset, int limit, AspxCommonInfo aspxCommonInfo, int serviceId, int branchID, string serviceEmpName)
       {
           try
           {
               List<ServiceEmpInfo> list = AspxServiceProvider.GetServiceEmployee(offset, limit, aspxCommonInfo, serviceId, branchID, serviceEmpName);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

             
       public List<BookingDetailsInfo> GetBookingDetails(int offset, int limit, AspxCommonInfo aspxCommonInfo, int employeeId, int? statusId, int branchID)
       {
           try
           {
               List<BookingDetailsInfo> list = AspxServiceProvider.GetServiceBookingDetails(offset, limit, aspxCommonInfo, employeeId, statusId, branchID);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public List<ServiceCoreAvailableDate> GetServiceDates(GetCoreServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxServiceProvider objService = new AspxServiceProvider();
               List<ServiceCoreAvailableDate> lstServDate = objService.GetServiceDates(getServiceDateObj, aspxCommonObj);
               return lstServDate;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAnAppointmentCoreInfo obj)
       {
           var isSuccess = false;
           try
           {
               AspxServiceProvider objService = new AspxServiceProvider();
               objService.SaveBookAppointment(appointmentId, aspxCommonObj, obj);
               isSuccess = true;
           }
           catch (Exception)
           {
               isSuccess = false;

           }
           return isSuccess;
       }

       public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
       {
           try
           {
               AspxServiceProvider objService = new AspxServiceProvider();
               List<ServiceProviderInfo> lstServProv = objService.GetServiceProviderNameListFront(aspxCommonObj, storeBranchId, serviceCategoryId);
               return lstServProv;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public List<ServiceCoreAvailableTime> GetServiceAvailableTime(GetServiceCoreAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxServiceProvider objService = new AspxServiceProvider();
               List<ServiceCoreAvailableTime> lstServTime = objService.GetServiceAvailableTime(getServiceTimeObj, aspxCommonObj);
               return lstServTime;
           }
           catch (Exception e)
           {

               throw e;
           }

       }

       public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo bookedTimeObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxServiceProvider objService = new AspxServiceProvider();
               List<ServiceBookedTime> lstSBTime = objService.GetServiceBookedTime(bookedTimeObj, aspxCommonObj);
               return lstSBTime;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion
    }
}
