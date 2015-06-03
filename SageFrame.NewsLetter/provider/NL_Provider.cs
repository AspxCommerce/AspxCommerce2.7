using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Message;

namespace SageFrame.NewsLetter
{  
    /// <summary>
    /// Manipulates data for NL_Controller class.
    /// </summary>
    public class NL_Provider
    {
        /// <summary>
        /// Connects to database and save email for subscribing.
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="UsermoduleID">UsermoduleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="clientIP">clientIP</param>
        public void SaveEmailSubscriber(string Email, int UsermoduleID, int PortalID,string UserName,string clientIP)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@SubscriberEmail", Email));
                Param.Add(new KeyValuePair<string, object>("@UsermoduleID", UsermoduleID));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
                Param.Add(new KeyValuePair<string, object>("@clientIP", clientIP));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_SaveEmailSubscriber", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and saves mobile number for subscribing.
        /// </summary>
        /// <param name="Phone">Phone</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="clientIP">clientIP</param>
        public void SaveMobileSubscriber(Int64 Phone, int UserModuleID, int PortalID, string UserName, string clientIP)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@SubscriberPhone", Phone));
                Param.Add(new KeyValuePair<string, object>("@UsermoduleID", UserModuleID));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
                Param.Add(new KeyValuePair<string, object>("@clientIP", clientIP));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_SaveMobileSubscriber", Param);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and saves settings.
        /// </summary>
        /// <param name="SettingKey">SettingKey</param>
        /// <param name="SettingValue">SettingValue</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveNLSetting(string SettingKey, string SettingValue, int UserModuleID, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKey));
                Param.Add(new KeyValuePair<string, object>("@SettingValues", SettingValue));
                Param.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_SaveNLSetting", Param);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns NL_SettingInfo list containing settings.
        /// </summary>
        /// <param name="UsermoduleID">UsermoduleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>NL_SettingInfo list</returns>
        public List<NL_SettingInfo> GetNLSetting(int UsermoduleID, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@UsermoduleID", UsermoduleID));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<NL_SettingInfo>("[usp_NLGetSetting]", Param);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NL_Info> CheckPreviousEmailSubscription(string Email)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Email", Email));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<NL_Info>("usp_NL_GetDataByEmail", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and retruns NL_SettingInfo list containing settings for unsubscribe.
        /// </summary>
        /// <returns>NL_SettingInfo list</returns>
        public List<NL_SettingInfo> GetNLSettingForUnSubscribe()
        {
            try
            {
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<NL_SettingInfo>("usp_NL_GetNLSettingForUnSubscribe");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and unsubscribe user by email.
        /// </summary>
        /// <param name="Email">Email</param>
        public void UnSubscribeUserByEmail(string Email)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Email", Email));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_UnSubscribeUserByEmail", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and unsubscribe user by phone number.
        /// </summary>
        /// <param name="Phone"></param>
        public void UnSubscribeUserByMobile(Int64 Phone)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Phone", Phone));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_UnSubscribeUserByPhone", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns MessageManagementInfo list containing message template list for subscribe.
        /// </summary>
        /// <param name="current">current</param>
        /// <param name="pagesize">pagesize</param>
        /// <param name="IsActive">IsActive</param>
        /// <param name="IsDeleted">IsDeleted</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="CultureName">CultureName</param>
        /// <returns>MessageManagementInfo list</returns>
        public List<MessageManagementInfo> GetMessageTemplateListForSubscribe(int current, int pagesize, bool IsActive,bool IsDeleted, int PortalID, string UserName, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Current", current));
                Param.Add(new KeyValuePair<string, object>("@Pagesize", pagesize));
                Param.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                Param.Add(new KeyValuePair<string, object>("@IsDeleted", IsDeleted));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
                Param.Add(new KeyValuePair<string, object>("@CurrentCulture", CultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<MessageManagementInfo>("usp_NL_GetMessageTemplateList", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns MessageManagementInfo list containing message info by message template id.
        /// </summary>
        /// <param name="messageTemplateID">messageTemplateID</param>
        /// <returns>MessageManagementInfo list</returns>
        public List<MessageManagementInfo> GetMessageInfoByMessageTemplateID(int messageTemplateID)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@messageTemplateID", messageTemplateID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<MessageManagementInfo>("usp_NL_GetMessageInfoByID", Param);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and unsubscribe by email link.
        /// </summary>
        /// <param name="subscriberID">subscriberID</param>
        public void UnSubscribeByEmailLink(int subscriberID)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@subscriberID", subscriberID));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_NL_UnSubscribeByEmailLink", Param);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns NL_Info list containing subscriber list.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>NL_Info list</returns>
        public List<NL_Info>  GetSubscriberList(int index) 
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Offset", index));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<NL_Info>("usp_NL_GetSubscriberList", Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns list containing subscriber email list for given portal id.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>NL_Info list</returns>
        public List<NL_Info> GetSubscriberEmailList(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<NL_Info>("[usp_NL_GetSubscriberEmailList]", Parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and saves newsletter.
        /// </summary>
        /// <param name="Subject">Subject</param>
        /// <param name="BodyMsg">BodyMsg</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveNewsLetter(string Subject, string BodyMsg, int UserModuleID, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@Subject", Subject));
                Parameter.Add(new KeyValuePair<string, object>("@BodyMsg", BodyMsg));
                Parameter.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("[usp_NL_SaveNewsLetter]", Parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connects to database and returns MessageManagementInfo list containing message template by id.
        /// </summary>
        /// <param name="MessageTemplateTypeID">MessageTemplateTypeID</param>
        /// <param name="CultureName">CultureName</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>MessageManagementInfo list.</returns>
        public List<MessageManagementInfo> GetMessageTemplateByTypeID(int MessageTemplateTypeID, string CultureName, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                Parameter.Add(new KeyValuePair<string, object>("@Culture", CultureName));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<MessageManagementInfo>("[usp_NL_GetMessageTemplateByTypeID]", Parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
