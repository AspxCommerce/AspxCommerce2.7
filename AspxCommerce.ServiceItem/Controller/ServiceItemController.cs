using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using SageFrame.Web;
using System.Web.SessionState;
using AspxCommerce.Core;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemController
    {
        public ServiceItemController()
        {
        }

        public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceCategoryInfo> lstService = objService.GetAllServices(aspxCommonObj);
                return lstService;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void SetServiceSessionVariable(string key, object value)
        {
            //HttpContext.Current.Session[key] = value;
            System.Web.HttpContext.Current.Session[key] = value;
        }  

        public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<FrontServiceCategoryView> lstService = objService.GetFrontServices(aspxCommonObj, count);
                return lstService;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<ServiceCategoryDetailsInfo> GetServiceItemDetails(int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceCategoryDetailsInfo> lstSIDetail = objService.GetServiceItemDetails(itemID, aspxCommonObj);
                return lstSIDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ServiceItemSettingInfo> GetServiceItemSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceItemSettingInfo> lstServiceItem = objService.GetServiceItemSetting(aspxCommonObj);
                return lstServiceItem;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ServiceItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                objService.ServiceItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            ServiceItemProvider objService = new ServiceItemProvider();
            List<ServiceItemInfo> serviceInfo = objService.GetServiceItemInfo(aspxCommonObj, categoryId);
            return serviceInfo;
        }



        public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
        {

            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<StoreLocatorInfo> lstStoreLocator = objService.GetAllStoresForService(aspxCommonObj, serviceCategoryId);
                return lstStoreLocator;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAnAppointmentInfo obj)
        {
            var isSuccess = false;
            try
            {
                SetServiceSessionVariable("AppointmentCollection",obj);
                ServiceItemProvider objService = new ServiceItemProvider();
                objService.SaveBookAppointment(appointmentId, aspxCommonObj, obj);
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;

            }
            return isSuccess;
        }


        public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceItemProductInfo> lstServProduct = objService.GetServiceProducts(serviceId, aspxCommonObj);
                return lstServProduct;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<ServiceAvailableDate> GetServiceDates(GetServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceAvailableDate> lstServDate = objService.GetServiceDates(getServiceDateObj, aspxCommonObj);
                return lstServDate;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<ServiceAvailableTime> GetServiceAvailableTime(GetServiceAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceAvailableTime> lstServTime = objService.GetServiceAvailableTime(getServiceTimeObj, aspxCommonObj);
                return lstServTime;
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceProviderInfo> lstServProv = objService.GetServiceProviderNameListFront(aspxCommonObj, storeBranchId, serviceCategoryId);
                return lstServProv;
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
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceBookedTime> lstSBTime = objService.GetServiceBookedTime(bookedTimeObj, aspxCommonObj);
                return lstSBTime;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                objService.DeleteAppointmentForError(orderId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceDetailsInfo> lstServiceDetail = objService.GetServiceDetails(servicekey, aspxCommonObj);
                return lstServiceDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }       
       
        public List<ServiceItemRss> GetServiceTypeRssFeedContent(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {               
                ServiceItemProvider objService = new ServiceItemProvider();
                List<ServiceItemRss> serviceItemRss = objService.GetServiceTypeRssContent(aspxCommonObj, count);
                return serviceItemRss;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

