using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using SageFrame.Message;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using SageFrame.SageFrameClass.MessageManagement;

namespace AspxCommerce.Core
{
    public class ServiceItemHandler
    {
        public void SaveServiceItem(AspxCommonInfo aspxCommonObj, int categoryId, List<ServiceItemInfo> serviceInfo, SqlTransaction tran)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                foreach (var serviceItemInfo in serviceInfo)
                {
                    parameter.Add(new KeyValuePair<string, object>("@ServiceId", serviceItemInfo.ServiceId));
                    parameter.Add(new KeyValuePair<string, object>("@Option", "serviceInfo"));
                    parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                    parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryId));
                    parameter.Add(new KeyValuePair<string, object>("@Position", serviceItemInfo.Position));
                    parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", serviceItemInfo.BranchId));
                    parameter.Add(new KeyValuePair<string, object>("@StoreBranchName", serviceItemInfo.BranchName));
                    var serviceInfoId = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,"[dbo].[usp_Aspx_AddServicesItem]", parameter,"@ServiceItemID");

                    parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceInfo"));
                    foreach (var employeeDataInfo in serviceItemInfo.EmployeeData)
                    {
                        parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeUpdateId",employeeDataInfo.ServiceEmployeeId));
                        parameter.Add(new KeyValuePair<string, object>("@EmployeeID", employeeDataInfo.EmployeeId));
                        parameter.Add(new KeyValuePair<string, object>("@Option", "serviceEmployeeData"));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeName",employeeDataInfo.EmployeeName));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));
                        var serviceAssinEmployeeId = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,"[dbo].[usp_Aspx_AddServicesItem]", parameter,"@serviceEmployeeID");

                        parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeUpdateId",employeeDataInfo.ServiceEmployeeId));
                        parameter.Remove(new KeyValuePair<string, object>("@EmployeeID", employeeDataInfo.EmployeeId));
                        parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceEmployeeData"));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeName",employeeDataInfo.EmployeeName));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));
                        foreach (var serviceDateInfo in employeeDataInfo.AvailableDate)
                        {
                            parameter.Add(new KeyValuePair<string, object>("@ServiceDateUpdateId",serviceDateInfo.ServiceDateId));
                            parameter.Add(new KeyValuePair<string, object>("@Option", "serviceDate"));
                            parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID", serviceAssinEmployeeId));
                            parameter.Add(new KeyValuePair<string, object>("@ServiceDateFrom",serviceDateInfo.ServiceDateFrom));
                            parameter.Add(new KeyValuePair<string, object>("@ServiceDateTo",serviceDateInfo.ServiceDateTo));
                            parameter.Add(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));
                            var serviceDateId = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,"[dbo].[usp_Aspx_AddServicesItem]", parameter,"@ServiceDateID");

                            parameter.Remove(new KeyValuePair<string, object>("@ServiceDateUpdateId",serviceDateInfo.ServiceDateId));
                            parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceDate"));
                            parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceAssinEmployeeId));
                            parameter.Remove(new KeyValuePair<string, object>("@ServiceDateFrom",serviceDateInfo.ServiceDateFrom));
                            parameter.Remove(new KeyValuePair<string, object>("@ServiceDateTo",serviceDateInfo.ServiceDateTo));
                            parameter.Remove(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));

                            foreach (var serviceTimeInfo in serviceDateInfo.ServiceTime)
                            {
                                parameter.Add(new KeyValuePair<string, object>("@ServiceTimeUpdateId",serviceTimeInfo.ServiceTimeId));
                                parameter.Add(new KeyValuePair<string, object>("@Option", "serviceTime"));
                                parameter.Add(new KeyValuePair<string, object>("@ServiceDateIDInsert", serviceDateId));
                                parameter.Add(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));
                                parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceAssinEmployeeId));
                                parameter.Add(new KeyValuePair<string, object>("@ServiceTimeFrom",serviceTimeInfo.ServiceTimeFrom));
                                parameter.Add(new KeyValuePair<string, object>("@ServiceTimeTo",serviceTimeInfo.ServiceTimeTo));
                              //  parameter.Add(new KeyValuePair<string, object>("@ServiceQuantity",serviceTimeInfo.ServiceQuantity));
                                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,"[dbo].[usp_Aspx_AddServicesItem]", parameter);

                                parameter.Remove(new KeyValuePair<string, object>("@ServiceTimeUpdateId",serviceTimeInfo.ServiceTimeId));
                                parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceTime"));
                                parameter.Remove(new KeyValuePair<string, object>("@ServiceDateIDInsert", serviceDateId));
                                parameter.Remove(new KeyValuePair<string, object>("@ServiceInfoID", serviceInfoId));
                                parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceAssinEmployeeId));
                                parameter.Remove(new KeyValuePair<string, object>("@ServiceTimeFrom",serviceTimeInfo.ServiceTimeFrom));
                                parameter.Remove(new KeyValuePair<string, object>("@ServiceTimeTo",serviceTimeInfo.ServiceTimeTo));
                                //parameter.Remove(new KeyValuePair<string, object>("@ServiceQuantity",serviceTimeInfo.ServiceQuantity));
                            }
                        }
                    }
                    parameter.Clear();
                }
                //  tran.Commit();
            }
            catch (Exception ex)
            {
                //ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                //  obj.DeleteSingleItem(itemId, storeId, portalId, userName);
                //SQLHandler sqlH = new SQLHandler();
                //List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                //parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                //parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                //parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                //sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteServiceItemByItemID]", parameter);
                //  tran.Rollback();
                throw ex;
            }
        }

        public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            List<ServiceItemInfo> serviceInfo;

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@Option", "serviceInfo"));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
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
                    parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceEmployeeInfo.ServiceEmployeeId));
                    serviceDateInfo = sqlH.ExecuteAsList<ServiceDateInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]",parameter);
                    serviceEmployeeInfo.AvailableDate = serviceDateInfo;

                    parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceDate"));
                    parameter.Remove(new KeyValuePair<string, object>("@ServiceID", serviceItemInfo.ServiceId));
                    parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceEmployeeInfo.ServiceEmployeeId));

                    foreach (var dateInfo in serviceDateInfo)
                    {
                        List<ServiceTimeInfo> servicTimeInfo;
                        parameter.Add(new KeyValuePair<string, object>("@Option", "serviceTime"));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceDateID", dateInfo.ServiceDateId));
                        parameter.Add(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceEmployeeInfo.ServiceEmployeeId));
                        servicTimeInfo = sqlH.ExecuteAsList<ServiceTimeInfo>("[dbo].[usp_Aspx_GetServiceItemInfo]",parameter);
                        dateInfo.ServiceTime = servicTimeInfo;

                        parameter.Remove(new KeyValuePair<string, object>("@Option", "serviceTime"));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceDateID", dateInfo.ServiceDateId));
                        parameter.Remove(new KeyValuePair<string, object>("@ServiceEmployeeID",serviceEmployeeInfo.ServiceEmployeeId));
                    }
                }
            }
            return serviceInfo;
        }

        public void DeleteServiceItem(string option, AspxCommonInfo aspxCommonObj, int id)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Option", option));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@ID", id));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteServiceItem]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveServiceProvider(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerSaveInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", providerSaveInfo.StoreBranchID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", providerSaveInfo.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeName", providerSaveInfo.EmployeeName));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeNickName", providerSaveInfo.EmployeeNickName));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeImage", providerSaveInfo.EmployeeImage));

                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[dbo].[usp_Aspx_AddUpdateServiceProvider]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteServiceProvider(AspxCommonInfo aspxCommonObj, string id, int storeBranchId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", id));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteProviderFrmBranch]", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ServiceProviderInfo>GetServiceProviderNameList(AspxCommonInfo aspxCommonObj,int storeBranchId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<ServiceProviderInfo>("[dbo].[usp_Aspx_GetServiceProviderNameList]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", serviceCategoryId));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<ServiceProviderInfo>("[usp_Aspx_GetServiceProviderForStore]", parameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAppointmentInfo obj)
        {
            var isSuccess = false;
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@AppointmentID", appointmentId));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", obj.OrderID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", obj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceProductID", obj.ServiceProductID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceProductPrice", obj.ServiceProductPrice));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
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
                parameter.Add(new KeyValuePair<string, object>("@UserName", obj.FirstName));
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

        public void SendMailNotificatiion(int storeId, int portalId, string cultureName, BookAppointmentInfo objInfo)
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

        public static string[] GetAllToken(string template)
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

        public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo bookedTimeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ServiceCategoryID", bookedTimeObj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("@BranchID", bookedTimeObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("@EmployeeID", bookedTimeObj.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDateID", bookedTimeObj.ServiceDateID));
                parameter.Add(new KeyValuePair<string, object>("@ServiceDate", bookedTimeObj.ServiceDate));
                parameter.Add(new KeyValuePair<string, object>("@ServiceTimeID", bookedTimeObj.ServiceTimeID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", bookedTimeObj.ItemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<ServiceBookedTime>("[dbo].[usp_Aspx_GetServiceBookedTime]", parameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckServiceProviderUniqueness(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerUniqueInfo)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName",aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", providerUniqueInfo.StoreBranchID));
                Parameter.Add(new KeyValuePair<string, object>("@EmployeeID", providerUniqueInfo.EmployeeID));
                Parameter.Add(new KeyValuePair<string, object>("@EmployeeName", providerUniqueInfo.EmployeeName));
                return sqlH.ExecuteNonQueryAsBool("[usp_Aspx_ServiceProviderUniqunessCheck]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteAppointmentForError]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
