#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region  "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.UserManagement;
using SageFrame.Message;
using SageFrame.Security;
using SageFrame.Security.Entities;
using System.Web.Security;
using SageFrame.Security.Helpers;
using SageFrame.Common;
using SageFrameClass.MessageManagement;
using SageFrame.SageFrameClass.MessageManagement;
#endregion

namespace SageFrame.Modules.UserRegistration
{
    public partial class ctl_UserActivation : BaseUserControl
    {
        MembershipController _member = new MembershipController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    SageFrameConfig pb = new SageFrameConfig();
                    string ActivationCode = string.Empty;
                    if (Request.QueryString["ActivationCode"] != null)
                    {
                        ActivationCode = Request.QueryString["ActivationCode"].ToString();
                        try
                        {
                            ActivationCode = EncryptionMD5.Decrypt(ActivationCode);
                        }
                        catch
                        {
                            ShowMessage("", GetSageMessage("UserRegistration", "InvalidActivationCode"), "", SageMessageType.Alert);
                            return;
                        }
                        ForgotPasswordInfo sageframeuser = new ForgotPasswordInfo();
                        sageframeuser = UserManagementController.GetUsernameByActivationOrRecoveryCode(ActivationCode, GetPortalID);
                        if (sageframeuser.CodeForUsername != null)
                        {
                            if (!sageframeuser.IsAlreadyUsed)
                            {
                                string UserName = _member.ActivateUser(ActivationCode, GetPortalID);
                                if (!String.IsNullOrEmpty(UserName))
                                {
                                    UserInfo user = _member.GetUserDetails(GetPortalID, UserName);
                                    if (user.UserExists)
                                    {
                                        List<ForgotPasswordInfo> messageTemplates = UserManagementController.GetMessageTemplateListByMessageTemplateTypeID(SystemSetting.ACTIVATION_SUCCESSFUL_EMAIL, GetPortalID);

                                        foreach (ForgotPasswordInfo messageTemplate in messageTemplates)
                                        {
                                            DataTable dtActivationSuccessfulTokenValues = UserManagementController.GetActivationSuccessfulTokenValue(user.UserName, GetPortalID);
                                            string replaceMessageSubject = MessageToken.ReplaceAllMessageToken(messageTemplate.Subject, dtActivationSuccessfulTokenValues);
                                            string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, dtActivationSuccessfulTokenValues);
                                            try
                                            {
                                                MailHelper.SendMailNoAttachment(messageTemplate.MailFrom, user.Email, replaceMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                                            }
                                            catch (Exception)
                                            {
                                                ShowMessage("", GetSageMessage("UserRegistration", "SecureConnectionUAEmailError"), "", SageMessageType.Alert);
                                                return;
                                            }
                                        }
                                        ForgotPasswordInfo template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.ACTIVATION_SUCCESSFUL_INFORMATION, GetPortalID);
                                        if (template != null)
                                        {
                                            ACTIVATION_INFORMATION.Text = template.Body;
                                        };
                                        LogInPublicModeRegistration(user);
                                    }
                                    else
                                    {
                                        ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                                    }
                                }
                                else
                                {
                                    ForgotPasswordInfo template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.ACTIVATION_FAIL_INFORMATION, GetPortalID);
                                    if (template != null)
                                    {
                                        ACTIVATION_INFORMATION.Text = template.Body;
                                    };
                                }
                            }
                            else
                            {
                                ShowMessage("", GetSageMessage("UserRegistration", "ActivationCodeAlreadyUsed"), "", SageMessageType.Alert);
                            }
                        }
                        else
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected string GetActivationCode(string pagePath)
        {
            string ActivationCode = string.Empty;
            if (string.IsNullOrEmpty(ActivationCode))
            {
                ActivationCode = "Home";
            }
            else
            {
                string[] pagePaths = pagePath.Split('/');
                ActivationCode = pagePaths[pagePaths.Length - 1];
                if (string.IsNullOrEmpty(ActivationCode))
                {
                    ActivationCode = pagePaths[pagePaths.Length - 2];
                }
                ActivationCode = ActivationCode.Replace(SageFrameSettingKeys.PageExtension, "");
            }
            return ActivationCode;
        }

        private void LogInPublicModeRegistration(UserInfo user)
        {
            string strRoles = string.Empty;
            RoleController role = new RoleController();
            SageFrameConfig sfConfig = new SageFrameConfig();
            string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
            strRoles += userRoles;
            if (strRoles.Length > 0)
            {
                SetUserRoles(strRoles);
                //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                //sessionTracker.PortalID = GetPortalID.ToString();
                //sessionTracker.Username = user.UserName;
                //Session[SessionKeys.Tracker] = sessionTracker;
                SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                SageFrameConfig SageConfig = new SageFrameConfig();
                SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
                bool EnableSessionTracker = bool.Parse(SageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.EnableSessionTracker));
                if (EnableSessionTracker)
                {
                    SLog.SessionTrackerUpdateUsername(user.UserName, GetPortalID.ToString());
                }
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        true,
                        GetPortalID.ToString(),
                        FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    SecurityPolicy objSecurity = new SecurityPolicy();
                    Response.Cookies.Add(new HttpCookie(objSecurity.FormsCookieName(GetPortalID), encTicket));
                    if (!IsParent)
                    {
                        Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension), false);
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension), false);
                    }
                }
            }
        }

        public void SetUserRoles(string strRoles)
        {
            Session[SessionKeys.SageUserRoles] = strRoles;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookiesKeys.SageUserRolesCookie];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookiesKeys.SageUserRolesCookie);
            }
            cookie[CookiesKeys.SageUserRolesProtected] = strRoles;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}