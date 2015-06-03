using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using SageFrame.Message;
using System.Web;
using SageFrame.SageFrameClass.MessageManagement;
using AspxCommerce.ServiceItem;
using AspxCommerce.Core;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemProvider
    {
        public ServiceItemProvider() { 
        }

        public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<ServiceCategoryInfo> lstService = sqlH.ExecuteAsList<ServiceCategoryInfo>("[usp_Aspx_GetAllServices]", parameterCollection);
                return lstService;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                List<FrontServiceCategoryView> lstService = sqlH.ExecuteAsList<FrontServiceCategoryView>("[usp_Aspx_GetFrontServices]", parameterCollection);
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqlH = new SQLHandler();
                List<ServiceCategoryDetailsInfo> lstSIDetail = sqlH.ExecuteAsList<ServiceCategoryDetailsInfo>("[usp_Aspx_GetServiceItemDetails]", parameterCollection);
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
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                List<ServiceItemSettingInfo> view =
                    sqlH.ExecuteAsList<ServiceItemSettingInfo>("[dbo].[usp_Aspx_ServiceItemSettingGet]", paramCol);
                return view;
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
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_ServiceItemSettingsUpdate]", paramCol);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
       

        public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            List<ServiceItemInfo> serviceInfo;

            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@Option", "serviceInfo"));
            parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryId));
            SQLHandler sqlH = new SQLHandler();

            serviceInfo = sqlH.ExecuteAsList<ServiceItemInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]", parameter);
            parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceInfo"));

            foreach (var serviceItemInfo in serviceInfo)
            {
                List<ServiceEmployeeInfo> serviceEmployeeDataInfo;
                parameter.Add(new KeyValuePair<string, object>("@Option", "serviceEmployeeData"));
                parameter.Add(new KeyValuePair<string, object>("@ServiceID", serviceItemInfo.ServiceId));
                serviceEmployeeDataInfo = sqlH.ExecuteAsList<ServiceEmployeeInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]", parameter);
                serviceItemInfo.EmployeeData = serviceEmployeeDataInfo;

                parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceEmployeeData"));
                parameter.Remove(new KeyValuePair<string, object>("@ServiceID", serviceItemInfo.ServiceId));

                foreach (var serviceEmployeeInfo in serviceEmployeeDataInfo)
                {
                    List<ServiceDateInfo> serviceDateInfo;
                    parameter.Add(new KeyValuePair<string, object>("@Option", "serviceDate"));
                    parameter.Add(new KeyValuePair<string, object>("@ServiceID", serviceItemInfo.ServiceId));
                    parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID", serviceEmployeeInfo.ServiceEmployeeId));
                    serviceDateInfo = sqlH.ExecuteAsList<ServiceDateInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]", parameter);
                    serviceEmployeeInfo.AvailableDate = serviceDateInfo;

                    parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceDate"));
                    parameter.Remove(new KeyValuePair<string, object>("@ServiceID", serviceItemInfo.ServiceId));
                    parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID", serviceEmployeeInfo.ServiceEmployeeId));

                    foreach (var dateInfo in serviceDateInfo)
                    {
                        List<ServiceTimeInfo> servicTimeInfo;
                        parameter.Add(new KeyValuePair<string, object>("@Option", "serviceTime"));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceDateID", dateInfo.ServiceDateId));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID", serviceEmployeeInfo.ServiceEmployeeId));
                        servicTimeInfo = sqlH.ExecuteAsList<ServiceTimeInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]", parameter);
                        dateInfo.ServiceTime = servicTimeInfo;

                        parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceTime"));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceDateID", dateInfo.ServiceDateId));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID", serviceEmployeeInfo.ServiceEmployeeId));
                    }
                }
            }
            return serviceInfo;
        }

        public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@ServiceID", serviceCategoryId));
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<StoreLocatorInfo> lstStoreLocator = sqlH.ExecuteAsList<StoreLocatorInfo>("[dbo].[usp_Aspx_GetAllStoreForService]", parameterCollection);
                return lstStoreLocator;
            }
            catch (Exception e)
            {
                throw e;
            }
        }      

        public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj,  BookAnAppointmentInfo obj)//upto this 1/13/2013
        {
            var isSuccess = false;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@AppointmentID", appointmentId));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", obj.OrderID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", obj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceProductID", obj.ServiceProductID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceProductPrice", obj.ServiceProductPrice));
                parameter.Add(new KeyValuePair<string, object>("@AppointmentStatusID", obj.AppointmentStatusID));
                parameter.Add(new KeyValuePair<string, object>("@Title", obj.Title));
                parameter.Add(new KeyValuePair<string, object>("@FirstName", obj.FirstName));
                parameter.Add(new KeyValuePair<string, object>("@LastName", obj.LastName));
                parameter.Add(new KeyValuePair<string, object>("@Gender", obj.Gender));
                parameter.Add(new KeyValuePair<string, object>("@Mobile", obj.MobileNumber));
                parameter.Add(new KeyValuePair<string, object>("@Phone", obj.PhoneNumber));
                parameter.Add(new KeyValuePair<string, object>("@Email", obj.Email));
                parameter.Add(new KeyValuePair<string, object>("@PreferredDateID", obj.ServiceDateId));
                parameter.Add(new KeyValuePair<string, object>("@PreferredDate", obj.PreferredDate));
                parameter.Add(new KeyValuePair<string, object>("@PreferredTime", obj.PreferredTime));
                parameter.Add(new KeyValuePair<string, object>("@TypeOfTreatment", obj.TypeOfTreatment));
                parameter.Add(new KeyValuePair<string, object>("@StoreLocation", obj.StoreLocation));
                parameter.Add(new KeyValuePair<string, object>("@CustomerType", obj.TypeOfCustomer));
                parameter.Add(new KeyValuePair<string, object>("@MembershipElite", obj.MembershipElite));
                parameter.Add(new KeyValuePair<string, object>("@UserName", obj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@PaymentMethodID", obj.PaymentMethodID));
                parameter.Add(new KeyValuePair<string, object>("@PreferredTimeInterval", obj.PreferredTimeInterval));
                parameter.Add(new KeyValuePair<string, object>("@PreferredTimeID", obj.PreferredTimeId));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", obj.EmployeeID));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[dbo].[usp_Aspx_AddAppointment]", parameter);

                if (appointmentId != 0 && obj.AppointmentStatusName.ToLower() == "completed")
                {
                    SendMailNotificatiion(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName, obj);
                }
                else if (appointmentId == 0 && obj.AppointmentStatusName.ToLower() == "pending")
                {
                    SendMailNotificatiion(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName, obj);
                }
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;
                // throw ex;

            }
            return isSuccess;
        }

        public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ServiceID", serviceId));
                SQLHandler sqlh = new SQLHandler();
                List<ServiceItemProductInfo> lstServProduct = sqlh.ExecuteAsList<ServiceItemProductInfo>("[dbo].[usp_Aspx_GetServiceProducts]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ServiceID", getServiceDateObj.ServiceID));
                parameter.Add(new KeyValuePair<string, object>("@BranchID", getServiceDateObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", getServiceDateObj.EmployeeID));
                SQLHandler sqlh = new SQLHandler();
                List<ServiceAvailableDate> lstServDate = sqlh.ExecuteAsList<ServiceAvailableDate>("[dbo].[usp_Aspx_GetServiceDates]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", getServiceTimeObj.CategoryID));
                parameter.Add(new KeyValuePair<string, object>("@BranchID", getServiceTimeObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", getServiceTimeObj.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDateID", getServiceTimeObj.ServiceDateID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDate", getServiceTimeObj.ServiceDate));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", getServiceTimeObj.ItemID));
                SQLHandler sqlh = new SQLHandler();
                List<ServiceAvailableTime> lstServTime = sqlh.ExecuteAsList<ServiceAvailableTime>("[dbo].[usp_Aspx_GetServiceTime]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", serviceCategoryId));
                SQLHandler sqlh = new SQLHandler();
                List<ServiceProviderInfo> lstServProv = sqlh.ExecuteAsList<ServiceProviderInfo>("[usp_Aspx_GetServiceProviderForStore]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", bookedTimeObj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("@BranchID", bookedTimeObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", bookedTimeObj.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDateID", bookedTimeObj.ServiceDateID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDate", bookedTimeObj.ServiceDate));
                parameter.Add(new KeyValuePair<string, object>("@ServiceTimeID", bookedTimeObj.ServiceTimeID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", bookedTimeObj.ItemID));
                SQLHandler sqlh = new SQLHandler();
                List<ServiceBookedTime> lstSBTime = sqlh.ExecuteAsList<ServiceBookedTime>("[dbo].[usp_Aspx_GetServiceBookedTime]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteAppointmentForError]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendMailNotificatiion(int storeId, int portalId, string cultureName,  BookAnAppointmentInfo objInfo)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeId, portalId, cultureName);
            string name = "Appointment Approval - Email";
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@MessageTemplateTypeName", name));
            SQLHandler sql = new SQLHandler();

            int messageTemplateTypeId = sql.ExecuteNonQuery("[dbo].[usp_Aspx_GetMessageTemplateTypeID]", parameter, "@MessageTemplateID");
            MessageManagementController msgController = new MessageManagementController();
            var template = msgController.GetMessageTemplate(messageTemplateTypeId, portalId);

            string messageTemplate = template.Body;
            string src = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";

            string receiverEmailID = objInfo.Email;
            string subject = template.Subject;
            string senderEmail = template.MailFrom;
            var headerMsg = string.Empty;
            var customMessage = "";
            if (objInfo.AppointmentID > 0)
            {
                headerMsg = "status has been modified as follow.";
            }
            else if (objInfo.AppointmentID == 0)
            {
                headerMsg = "has been scheduled as following date and time.";
            }
            if (template != null)
            {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (var token in tokens)
                {
                    switch (token)
                    {
                        case "%LogoSource%":
                            string imgSrc = src + logosrc;
                            messageTemplate = messageTemplate.Replace(token, imgSrc);
                            break;
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("MM/dd/yyyy"));
                            break;
                        case "%PreferredDate%":
                            messageTemplate = messageTemplate.Replace(token, objInfo.PreferredDate.ToString("MM/dd/yyyy"));
                            break;
                        case "%PreferredTime%":
                            messageTemplate = messageTemplate.Replace(token, objInfo.PreferredTime);
                            break;
                        case "%PreferredTimeInterval%":
                            messageTemplate = messageTemplate.Replace(token, objInfo.PreferredTimeInterval);
                            break;
                        case "%AppointmentStatus%":
                            messageTemplate = messageTemplate.Replace(token, objInfo.AppointmentStatusName);
                            break;
                        case "%ServerPath%":
                            messageTemplate = messageTemplate.Replace(token, src);
                            break;
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                            break;
                        case "%AppointmentHeadingMessage%":
                            messageTemplate = messageTemplate.Replace(token, headerMsg);
                            break;
                        case "%AppointmentCustomMessage%":
                            messageTemplate = messageTemplate.Replace(token, customMessage);
                            break;
                        case "%ServiceProductName%":
                            messageTemplate = messageTemplate.Replace(token, objInfo.ServiceProductName);
                            break;
                    }
                }
            }

            SageFrameConfig pagebase = new SageFrameConfig();
            string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);

            MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, messageTemplate, emailSiteAdmin, emailSuperAdmin);
        }

        public string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string> { };
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            while (preIndex > -1)
            {
                returnValue.Add(template.Substring(preIndex, (postIndex - preIndex) + 1));
                template = template.Substring(postIndex + 1, (template.Length - postIndex) - 1);
                preIndex = template.IndexOf('%', 0);
                postIndex = template.IndexOf('%', preIndex + 1);
            }
            return returnValue.ToArray();
        }

        public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ServiceKey", servicekey));
                SQLHandler sqlH = new SQLHandler();
                List<ServiceDetailsInfo> lstServiceDetail = sqlH.ExecuteAsList<ServiceDetailsInfo>("usp_Aspx_GetServiceDetails", parameterCollection);
                return lstServiceDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ServiceItemRss> GetServiceTypeRssContent(AspxCommonInfo aspxCommonObj,int count)
        {
            try
            {               
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler SQLH = new SQLHandler();
                List<ServiceItemRss> rssFeedContent = SQLH.ExecuteAsList<ServiceItemRss>("[dbo].[usp_Aspx_GetRssFeedServiceTypeItem]", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
