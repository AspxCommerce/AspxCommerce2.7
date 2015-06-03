<%@ WebService Language="C#" Class="NewsLetterWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.NewsLetter;
using SageFrame.Message;
using SageFrame.Web;
using SageFrame.Services;

/// <summary>
/// Summary description for NewsLetterWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class NewsLetterWebService : AuthenticateService
{
    SageFrameConfig pagebase = new SageFrameConfig();
    public NewsLetterWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void SaveEmailSubscriber(string Email, int UserModuleID, int PortalID, string UserName, string secureToken)
    {
        try
        {
                string clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
                NL_Controller cont = new NL_Controller();
                cont.SaveEmailSubscriber(Email, UserModuleID, PortalID, UserName, clientIP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void SaveMobileSubscriber(Int64 Phone, int UserModuleID, int PortalID, string UserName, string secureToken)
    {
        try
        {
                string clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
                NL_Controller cont = new NL_Controller();
                cont.SaveMobileSubscriber(Phone, UserModuleID, PortalID, UserName, clientIP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void SaveNLSetting(string SettingKey, string SettingValue, int UserModuleID, int PortalID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                NL_Controller cont = new NL_Controller();
                cont.SaveNLSetting(SettingKey, SettingValue, UserModuleID, PortalID);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<NL_SettingInfo> GetNLSetting(int UserModuleID, int PortalID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                NL_Controller cont = new NL_Controller();
                return cont.GetNLSetting(UserModuleID, PortalID);
            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<NL_Info> CheckPreviousEmailSubscription(string Email, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        try
        {
                NL_Controller cont = new NL_Controller();
                return cont.CheckPreviousEmailSubscription(Email);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<NL_SettingInfo> GetNLSettingForUnSubscribe()
    {
        try
        {


            NL_Controller cont = new NL_Controller();
            return cont.GetNLSettingForUnSubscribe();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void UnSubscribeUserByEmail(string Email)
    {
        try
        {

            NL_Controller cont = new NL_Controller();
            cont.UnSubscribeUserByEmail(Email);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void UnSubscribeUserByMobile(Int64 Phone)
    {
        try
        {

            NL_Controller cont = new NL_Controller();
            cont.UnSubscribeUserByMobile(Phone);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MessageManagementInfo> LoadMessageTemplateType(int PortalID, string UserName, string CultureName, int UserModuleID, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                MessageManagementController msgController = new MessageManagementController();
                return msgController.GetMessageTemplateTypeList(true, false, PortalID, UserName, CultureName);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MessageManagementInfo> GetMessageTokenByTemplate(int MessageTemplateTypeID, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                MessageManagementController msgController = new MessageManagementController();
                return msgController.GetMessageTemplateTypeTokenListByMessageTemplateType(MessageTemplateTypeID, PortalID);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MessageManagementInfo> GetMessageTemplateList(int current, int pageSize, int PortalID, string UserName, string CultureName, int UserModuleID, string secureToken)
    {
        try
        {

            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                NL_Controller cont = new NL_Controller();
                return cont.GetMessageTemplateListForSubscribe(current, pageSize, true, false, PortalID, UserName, CultureName);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MessageManagementInfo> GetMessageInfoByMessageTemplateID(int messageTemplateID)
    {
        try
        {

            NL_Controller cont = new NL_Controller();
            return cont.GetMessageInfoByMessageTemplateID(messageTemplateID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void UnSubscribeByEmailLink(int subscriberID)
    {
        try
        {

            NL_Controller cont = new NL_Controller();
            cont.UnSubscribeByEmailLink(subscriberID);


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void SendEmailForSubscriber(string subscriberList, string Subject, string BodyMsg, int UserModuleID, int PortalID)
    {
        try
        {

            if (subscriberList == string.Empty)
            {
                SaveNewsLetter(Subject, BodyMsg, UserModuleID, PortalID);
            }
            else
            {
                SendMail(subscriberList, Subject, BodyMsg);
            }

            //SendMail(subscriberList,Subject, BodyMsg);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveNewsLetter(string Subject, string BodyMsg, int UserModuleID, int PortalID)
    {
        try
        {


            NL_Controller objc = new NL_Controller();
            objc.SaveNewsLetter(Subject, BodyMsg, UserModuleID, PortalID);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    string mailList = "";
    public void SendMail(string subscriberList, string Subject, string BodyMsg)
    {
        try
        {


            if (subscriberList != string.Empty)
            {
                mailList = subscriberList;
                string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                SageFrame.SageFrameClass.MessageManagement.MailHelper.SendMailNoAttachment(emailSiteAdmin, mailList, Subject, BodyMsg, string.Empty, string.Empty);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<NL_Info> GetSubscriberList(int index)
    {

        NL_Controller obj = new NL_Controller();
        return obj.GetSubscriberList(index);

    }
    [WebMethod]
    public List<NL_Info> GetSubscriberEmailList(int PortalID)
    {
        try
        {

            NL_Controller obj = new NL_Controller();
            return obj.GetSubscriberEmailList(PortalID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MessageManagementInfo> GetMessageTemplateByTypeID(int MessageTemplateTypeID, string CultureName, int PortalID)
    {
        try
        {

            NL_Controller cont = new NL_Controller();
            return cont.GetMessageTemplateByTypeID(MessageTemplateTypeID, CultureName, PortalID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}