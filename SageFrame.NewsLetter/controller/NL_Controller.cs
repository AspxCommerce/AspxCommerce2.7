using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Message;
using System.Net.Mail;
using SageFrame.Web;
namespace SageFrame.NewsLetter
{

    /// <summary>
    /// Business logic class for NewsLetter.
    /// </summary>
    public  class NL_Controller
    {
        /// <summary>
        /// Saves email of subscriber.
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="UsermoduleID">UsermoduleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="clientIP">clientIP</param>
        public void SaveEmailSubscriber(string Email, int UsermoduleID, int PortalID, string UserName, string clientIP)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                cont.SaveEmailSubscriber(Email, UsermoduleID, PortalID, UserName, clientIP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  Saves mobile of subscriber.
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
                NL_Provider cont = new NL_Provider();
                cont.SaveMobileSubscriber(Phone, UserModuleID, PortalID, UserName, clientIP);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Saves setting value for setting key.
        /// </summary>
        /// <param name="SettingKey">SettingKey</param>
        /// <param name="SettingValue">SettingValue</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveNLSetting(string SettingKey, string SettingValue, int UserModuleID, int PortalID)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                cont.SaveNLSetting(SettingKey, SettingValue, UserModuleID, PortalID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain NL_SettingInfo list for given PortalID.
        /// </summary>
        /// <param name="UsermoduleID">UsermoduleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>NL_SettingInfo list.</returns>
        public List<NL_SettingInfo> GetNLSetting(int UsermoduleID, int PortalID)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                return cont.GetNLSetting(UsermoduleID, PortalID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Check existence email for subscription.
        /// </summary>
        /// <param name="Email">Email</param>
        /// <returns>NL_Info list.</returns>
        public List<NL_Info> CheckPreviousEmailSubscription(string Email)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                return cont.CheckPreviousEmailSubscription(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  Obtain setting for unsubscribe.
        /// </summary>
        /// <returns>Setting for unsubscribe.</returns>
        public List<NL_SettingInfo> GetNLSettingForUnSubscribe()
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                return cont.GetNLSettingForUnSubscribe();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Unsubscribe user by email.
        /// </summary>
        /// <param name="Email">Email</param>
        public void UnSubscribeUserByEmail(string Email)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                cont.UnSubscribeUserByEmail(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Unsubscribe user by mobile number.
        /// </summary>
        /// <param name="Phone">Phone</param>
        public void UnSubscribeUserByMobile(Int64 Phone)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                cont.UnSubscribeUserByMobile(Phone);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain message template list for subscribe
        /// </summary>
        /// <param name="current">current</param>
        /// <param name="pagesize">pagesize</param>
        /// <param name="IsActive">IsActive</param>
        /// <param name="IsDeleted">IsDeleted</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="CultureName">CultureName</param>
        /// <returns>MessageManagementInfo list</returns>
        public List<MessageManagementInfo> GetMessageTemplateListForSubscribe(int current, int pagesize, bool IsActive, bool IsDeleted, int PortalID, string UserName, string CultureName)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                return provider.GetMessageTemplateListForSubscribe(current, pagesize, true, false, PortalID, UserName, CultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain message info by message template id.
        /// </summary>
        /// <param name="messageTemplateID">MessageTemplateID</param>
        /// <returns>MessageManagementInfo list</returns>
        public List<MessageManagementInfo> GetMessageInfoByMessageTemplateID(int messageTemplateID)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                return provider.GetMessageInfoByMessageTemplateID(messageTemplateID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Unsubscribe by email link.
        /// </summary>
        /// <param name="subscriberID">SubscriberID</param>
        public void UnSubscribeByEmailLink(int subscriberID)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                provider.UnSubscribeByEmailLink(subscriberID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain subscriber list.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>NL_Info list</returns>
        public List<NL_Info> GetSubscriberList(int index)
        {
            NL_Provider provider = new NL_Provider();
            return provider.GetSubscriberList(index);
        }
        public List<NL_Info> GetSubscriberEmailList(int PortalID)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                return provider.GetSubscriberEmailList(PortalID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Saves newsletter.
        /// </summary>
        /// <param name="Subject">Subject</param>
        /// <param name="BodyMsg">BodyMsg</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveNewsLetter(string Subject, string BodyMsg, int UserModuleID, int PortalID)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                provider.SaveNewsLetter(Subject, BodyMsg,UserModuleID,PortalID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Obtain message template by id.
        /// </summary>
        /// <param name="MessageTemplateTypeID">MessageTemplateTypeID</param>
        /// <param name="CultureName">CultureName</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>MessageManagementInfo list.</returns>
        public List<MessageManagementInfo> GetMessageTemplateByTypeID(int MessageTemplateTypeID, string CultureName, int PortalID)
        {
            try
            {
                NL_Provider provider = new NL_Provider();
                return provider.GetMessageTemplateByTypeID(MessageTemplateTypeID, CultureName, PortalID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

    }


}
