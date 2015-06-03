using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.ServiceItem;

/// <summary>
/// Summary description for ServiceItemWebServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ServiceItemWebServices : System.Web.Services.WebService {

    public ServiceItemWebServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    #region for Services Details

    [WebMethod]
    public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceCategoryInfo> lstService = objService.GetAllServices(aspxCommonObj);
            return lstService;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<FrontServiceCategoryView> lstService = objService.GetFrontServices(aspxCommonObj, count);
            return lstService;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<ServiceCategoryDetailsInfo> GetServiceItemDetails(int itemID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceCategoryDetailsInfo> lstSIDetail = objService.GetServiceItemDetails(itemID, aspxCommonObj);
            return lstSIDetail;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<ServiceItemSettingInfo> GetServiceItemSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceItemSettingInfo> lstServiceItem = objService.GetServiceItemSetting(aspxCommonObj);
            return lstServiceItem;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ServiceItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            objService.ServiceItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }  
    
   
    [WebMethod]
    public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<StoreLocatorInfo> lstStoreLocator = objService.GetAllStoresForService(aspxCommonObj, serviceCategoryId);
            return lstStoreLocator;
        }
        catch (Exception e)
        {
            throw e;
        }
    }   

    [WebMethod(EnableSession = true)]
    public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj,  BookAnAppointmentInfo obj)
    {
        try
        {          
            ServiceItemController objService = new ServiceItemController();
            bool isSave = objService.SaveBookAppointment(appointmentId, aspxCommonObj, obj);
            return isSave;
        }
        catch (Exception)
        {
            return false;

        }
    }    

   
    
    [WebMethod]
    public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceItemProductInfo> lstServProduct = objService.GetServiceProducts(serviceId, aspxCommonObj);
            return lstServProduct;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<ServiceAvailableDate> GetServiceDates(GetServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceAvailableDate> lstServDate = objService.GetServiceDates(getServiceDateObj, aspxCommonObj);
            return lstServDate;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<ServiceAvailableTime> GetServiceAvailableTime(GetServiceAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceAvailableTime> lstServTime = objService.GetServiceAvailableTime(getServiceTimeObj, aspxCommonObj);
            return lstServTime;
        }
        catch (Exception e)
        {

            throw e;
        }

    }
   

    [WebMethod]
    public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
    {
        ServiceItemController objService = new ServiceItemController();
        List<ServiceProviderInfo> lstServProv = objService.GetServiceProviderNameListFront(aspxCommonObj, storeBranchId, serviceCategoryId);
        return lstServProv;
    }
    [WebMethod]
    public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo getServiceBookedTimeObj, AspxCommonInfo aspxCommonObj)
    {
        ServiceItemController objService = new ServiceItemController();
        List<ServiceBookedTime> lstSBTime = objService.GetServiceBookedTime(getServiceBookedTimeObj, aspxCommonObj);
        return lstSBTime;
    }

    [WebMethod]
    public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
    {
        ServiceItemController objService = new ServiceItemController();
        objService.DeleteAppointmentForError(orderId, aspxCommonObj);
    }

    [WebMethod]
    public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ServiceItemController objService = new ServiceItemController();
            List<ServiceDetailsInfo> lstServiceDetail = objService.GetServiceDetails(servicekey, aspxCommonObj);
            return lstServiceDetail;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
   
    #endregion
    
}
