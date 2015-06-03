/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using System.Web.Security;
using SageFrame.UserManagement;
using SageFrame.Message;
using SageFrame.Security.Entities;
using SageFrame.Security.Crypto;
using SageFrame.Security.Helpers;
using SageFrame.Security.Providers;
using SageFrame.Security;
using System.IO;
using SageFrame.Web.Utilities;
using SageFrame.Common;
using SageFrame.UserProfile;
using SageFrame.NewsLetter;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrameClass.MessageManagement;
using AspxCommerce.RealTimeCartManagement;
using Microsoft.AspNet.SignalR;
using AspxCommerce.AdminNotification;
using AspxCommerce.Core;
using SageFrame.Core;

namespace SageFrame.Modules.UserRegistration
{
    public partial class ctl_UserRegistration : BaseAdministrationUserControl
    {
        string Extension;
        public string headerTemplate = string.Empty;
        private Random random = new Random();
        SageFrameConfig pagebase = new SageFrameConfig();
        public string LoginPath = string.Empty, defpage = string.Empty;
        MembershipController _member = new MembershipController();
        protected void Page_Load(object sender, EventArgs e)
        {

            Extension = SageFrameSettingKeys.PageExtension;
            IncludeJs("UserRegistration", "/js/jquery.pstrength-min.1.2.js");
            IncludeJs("UserRegistrationValidation", "/js/jquery.validate.js");
            try
            {
                if (!IsParent)
                {
                    defpage = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension);
                }
                else
                {
                    defpage = ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension);
                }
                int UserRegistrationType = pagebase.GetSettingIntValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration);

                if (UserRegistrationType == 0)
                {
                    Response.Redirect(defpage);
                }
                IncludeCss("UserRegistration", "/Administrator/Templates/Default/css/login.css");
                ForgotPasswordInfo template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.USER_REGISTRATION_HELP, GetPortalID);
                if (template != null)
                {
                    headerTemplate = "<div>" + template.Body + "</div>";
                }
                if (!IsPostBack)
                {
                    if (_member.EnableCaptcha)
                    {
                        InitializeCaptcha();
                        GenerateCaptchaImage();
                    }
                    else { HideCaptcha(); }
                    SetValidatorErrorMessage();
                    CheckDivVisibility(true, false);
                    this.divRegister.Visible = true;
                    this.divRegistration.Visible = false;
                    this.divRegConfirm.Visible = false;
                }
                if (!IsParent)
                {
                    LoginPath = ResolveUrl("~/portal/" + GetPortalSEOName + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + Extension);
                }
                else
                {
                    LoginPath = ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + Extension);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void InitializeCaptcha()
        {
            CaptchaValue.Text = string.Empty;
            if (this.Session[SessionKeys.CaptchaImageText] == null)
            {
                this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
            }
            //  cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
            CaptchaImage.ImageUrl = "~/CaptchaImageHandler.aspx";
            Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
            captchaTR.Visible = true;
            CaptchaImage.Visible = true;
            CaptchaLabel.Visible = true;
            CaptchaValue.Visible = true;
            captchaValidator.Visible = false;
            Refresh.Visible = true;
            DataLabel.Visible = true;
            rfvCaptchaValueValidator.Enabled = true;
        }
        private void HideCaptcha()
        {
            captchaTR.Visible = false;
            CaptchaImage.Visible = false;
            CaptchaLabel.Visible = false;
            CaptchaValue.Visible = false;
            Refresh.Visible = false;
            DataLabel.Visible = false;
            rfvCaptchaValueValidator.Enabled = false;
            captchaValidator.Visible = false;
        }

        private void SetValidatorErrorMessage()
        {
            rfvUserNameRequired.Text = "*";
            rfvPasswordRequired.Text = "*";
            rfvConfirmPasswordRequired.Text = "*";
            rfvFirstName.Text = "*";
            rfvLastName.Text = "*";
            rfvEmailRequired.Text = "*";
            //rfvQuestionRequired.Text = "*";
            //rfvAnswerRequired.Text = "*";
            rfvCaptchaValueValidator.Text = "*";
            cvPasswordCompare.Text = "Passwords must match";
            // cvCaptchaValue.Text = "*";
            revEmail.Text = "*";
            rfvUserNameRequired.ErrorMessage = GetSageMessage("UserRegistration", "UserNameIsRequired");
            rfvPasswordRequired.ErrorMessage = GetSageMessage("UserRegistration", "PasswordIsRequired");
            rfvConfirmPasswordRequired.ErrorMessage = GetSageMessage("UserRegistration", "PasswordConfirmIsRequired");
            rfvFirstName.ErrorMessage = GetSageMessage("UserRegistration", "FirstNameIsRequired");
            rfvLastName.ErrorMessage = GetSageMessage("UserRegistration", "LastNameIsRequired");
            rfvEmailRequired.ErrorMessage = GetSageMessage("UserRegistration", "EmailAddressIsRequired");
            //rfvQuestionRequired.ErrorMessage = GetSageMessage("UserRegistration", "SecurityQuestionIsRequired");
            // rfvAnswerRequired.ErrorMessage = GetSageMessage("UserRegistration", "SecurityAnswerIsRequired");
            rfvCaptchaValueValidator.ErrorMessage = GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode");
            cvPasswordCompare.ErrorMessage = GetSageMessage("UserRegistration", "PasswordRetypeMatch");
            // cvCaptchaValue.ErrorMessage = GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode");
            revEmail.ErrorMessage = GetSageMessage("UserRegistration", "EmailAddressIsNotValid");
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            //changes made by hari for multiportal captcha
            //try
            //{
            //    this.Session["CaptchaImageText"] = GenerateRandomCode();
            //    cvCaptchaValue.ValueToCompare = this.Session["CaptchaImageText"].ToString();
            //    CaptchaImage.ImageUrl = "CaptchaImageHandler.aspx?=dummy" + DateTime.Now.ToLongTimeString();
            //    CaptchaValue.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    ProcessException(ex);
            //}
            GenerateCaptchaImage();
        }

        private void GenerateCaptchaImage()
        {
            try
            {
                this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
                // cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
                CaptchaImage.ImageUrl = ResolveUrl("~") + "CaptchaImageHandler.aspx?=dummy" + DateTime.Now.ToLongTimeString();
                CaptchaValue.Text = "";
                Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
                captchaValidator.Visible = false;
                this.divRegister.Visible = true;
                this.divRegistration.Visible = false;
                this.divRegConfirm.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string GenerateRandomCode()
        {
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, CapchaValue[this.random.Next(36)]);
            return s;
        }

        protected void FinishButton_Click(object sender, EventArgs e)
        {
            //  this.Session[SessionKeys.CaptchaImageText] = null;
            if (_member.EnableCaptcha)
            {
                bool testValid = ValidateCaptcha();
                if (testValid)
                {
                    RegisterUser();
                }
            }
            else
            {
                RegisterUser();
            }
        }

        private bool ValidateCaptcha()
        {
            string captchaText = string.Empty;
            if (Session[SessionKeys.CaptchaImageText] != null)
            {
                captchaText = Session[SessionKeys.CaptchaImageText].ToString();
            }
            this.Session[SessionKeys.CaptchaImageText] = null;
            if (!(captchaText == CaptchaValue.Text))
            {
                ShowMessage("", GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode"), "", SageMessageType.Error);
                GenerateCaptchaImage();
                this.divRegistration.Visible = false;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RegisterUser()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text) || string.IsNullOrEmpty(CaptchaValue.Text))
                {
                    ShowMessage("", GetSageMessage("UserRegistration", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
                }
                 else if (UserName.Text.Length > 50)
                {
                    ShowMessage("", "Username should not be greater than 50 characters", "", SageMessageType.Alert);
                }
                else
                {
                    int UserRegistrationType = pagebase.GetSettingIntValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration);
                    bool isUserActive = UserRegistrationType == 2 ? true : false;
                    UserInfo objUser = new UserInfo();
                    objUser.ApplicationName = Membership.ApplicationName;
                    objUser.FirstName = Server.HtmlEncode(FirstName.Text);
                    objUser.UserName = Server.HtmlEncode(UserName.Text);
                    objUser.LastName = Server.HtmlEncode(LastName.Text);
                    string Pwd, PasswordSalt;
                    PasswordHelper.EnforcePasswordSecurity(_member.PasswordFormat, Password.Text, out Pwd, out PasswordSalt);
                    objUser.Password = Pwd;
                    objUser.PasswordSalt = PasswordSalt;
                    objUser.Email = Email.Text;
                    objUser.SecurityQuestion = " ";
                    objUser.SecurityAnswer = " ";
                    objUser.IsApproved = true;
                    objUser.CurrentTimeUtc = DateTime.Now;
                    objUser.CreatedDate = DateTime.Now;
                    objUser.UniqueEmail = 0;
                    objUser.PasswordFormat = _member.PasswordFormat;
                    objUser.PortalID = GetPortalID;
                    objUser.AddedOn = DateTime.Now;
                    objUser.AddedBy = GetUsername;
                    objUser.UserID = Guid.NewGuid();
                    objUser.RoleNames = SystemSetting.REGISTER_USER_ROLENAME;
                    objUser.StoreID = GetStoreID;
                    objUser.CustomerID = 0;
                    UserCreationStatus status = new UserCreationStatus();
                    CheckRegistrationType(UserRegistrationType, ref objUser);
                    MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.REGISTER);
                    if (status == UserCreationStatus.DUPLICATE_USER)
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "NameAlreadyExists"), "", SageMessageType.Alert);
                        GenerateCaptchaImage();
                    }
                    else if (status == UserCreationStatus.DUPLICATE_EMAIL)
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
                        GenerateCaptchaImage();
                    }
                    else if (status == UserCreationStatus.SUCCESS)
                    {
                        try
                        {
                            IsModuleInstalled(1);
                            MembershipUser userInfo = Membership.GetUser(UserName.Text.Trim());
                            if (chkIsSubscribeNewsLetter.Checked)
                            {
                                int? newID = 0;
                                ManageNewsLetterSubscription(Email.Text, ref newID);
                                IsModuleInstalled(2);
                            }
                            HandlePostRegistration(UserRegistrationType);
                        }
                        catch (Exception)
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "SecureConnection"), "", SageMessageType.Alert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void clearField()
        {
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;
            UserName.Text = string.Empty;
            CaptchaValue.Text = string.Empty;
            Email.Text = string.Empty;
        }

        private void ManageNewsLetterSubscription(string email, ref int? newID)
        {
            string clientIP = Request.UserHostAddress.ToString();
            NL_Controller objController = new NL_Controller();
            objController.SaveEmailSubscriber(email, Int32.Parse(SageUserModuleID), GetPortalID, GetUsername, clientIP);
        }

        private void CheckRegistrationType(int UserRegistrationType, ref UserInfo user)
        {
            switch (UserRegistrationType)
            {
                case 0:
                    break;
                case 1:
                    user.IsApproved = false;
                    break;
                case 2:
                    user.IsApproved = true;
                    break;
                case 3:
                    user.IsApproved = false;
                    break;
            }
        }

        public void HandlePostRegistration(int UserRegistrationType)
        {
            switch (UserRegistrationType)
            {
                case 0:
                    ForgotPasswordInfo template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.USER_RESISTER_SUCESSFUL_INFORMATION_NONE, GetPortalID);
                    if (template != null)
                    {
                        USER_RESISTER_SUCESSFUL_INFORMATION.Text = template.Body;
                    }
                    break;
                case 1:
                    template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.USER_RESISTER_SUCESSFUL_INFORMATION_PRIVATE, GetPortalID);
                    if (template != null)
                    {
                        USER_RESISTER_SUCESSFUL_INFORMATION.Text = template.Body;
                    }
                    this.divRegistration.Visible = true;
                    this.divRegConfirm.Visible = true;
                    this.divRegister.Visible = false;
                    break;
                case 3:
                    template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.USER_RESISTER_SUCESSFUL_INFORMATION_VERIFIED, GetPortalID);
                    if (template != null)
                    {
                        USER_RESISTER_SUCESSFUL_INFORMATION.Text = template.Body;
                    }

                    List<ForgotPasswordInfo> objFpwd = UserManagementController.GetMessageTemplateListByMessageTemplateTypeID(SystemSetting.ACCOUNT_ACTIVATION_EMAIL, GetPortalID);
                    foreach (ForgotPasswordInfo messageTemplate in objFpwd)
                    {
                        CommonFunction comm = new CommonFunction();
                        DataTable dtActivationTokenValues = UserManagementController.GetActivationTokenValue(UserName.Text, GetPortalID);
                        string replaceMessageSubject = MessageToken.ReplaceAllMessageToken(messageTemplate.Subject, dtActivationTokenValues);
                        string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, UserName.Text, GetPortalID);
                        //  string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, dtActivationTokenValues);
                        MailHelper.SendMailNoAttachment(messageTemplate.MailFrom, Email.Text, replaceMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                    }
                    // CheckDivVisibility(false, true);
                    this.divRegistration.Visible = true;
                    this.divRegConfirm.Visible = true;
                    this.divRegister.Visible = false;
                    break;
                case 2:
                    template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.USER_RESISTER_SUCESSFUL_INFORMATION_PUBLIC, GetPortalID);
                    if (template != null)
                    {
                        USER_RESISTER_SUCESSFUL_INFORMATION.Text = template.Body;
                    }
                    LogInPublicModeRegistration();
                    break;
            }
        }

        private void CheckDivVisibility(bool RegistrationDiv, bool RegistrationConfDiv)
        {
            /// this.divRegistration.Visible = RegistrationDiv;
            // this.divRegConfirm.Visible = RegistrationConfDiv;
        }

        private void LogInPublicModeRegistration()
        {
            string strRoles = string.Empty;
            MembershipController member = new MembershipController();
            RoleController role = new RoleController();
            UserInfo user = member.GetUserDetails(GetPortalID, UserName.Text.Trim());

            if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(Password.Text)))
            {
                if (PasswordHelper.ValidateUser(user.PasswordFormat, Password.Text, user.Password, user.PasswordSalt))
                {
                    string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
                    strRoles += userRoles;
                    if (strRoles.Length > 0)
                    {
                        SetUserRoles(strRoles);
                        //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                        //sessionTracker.PortalID = GetPortalID.ToString();
                        //sessionTracker.Username = UserName.Text;
                        //Session[SessionKeys.Tracker] = sessionTracker;
                        SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                        SageFrameConfig SageConfig = new SageFrameConfig();
                        SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
                        bool EnableSessionTracker = bool.Parse(SageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.EnableSessionTracker));
                        if (EnableSessionTracker)
                        {
                            SLog.SessionTrackerUpdateUsername(UserName.Text, GetPortalID.ToString());
                        }
                        {
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                  user.UserName,
                                  DateTime.Now,
                                  DateTime.Now.AddMinutes(30),
                                  true,
                                  GetPortalID.ToString(),
                                  FormsAuthentication.FormsCookiePath);

                            StoreSettingConfig ssc = new StoreSettingConfig();


                            if (ssc.GetStoreSettingsByKey(StoreSetting.AllowRealTimeNotifications, GetStoreID, GetPortalID, GetCurrentCultureName).ToLower() == "true")
                            {
                                RealTimeHelper.UpdateAdminNotifications(GetStoreID, GetPortalID);    
                            }
                            

                            // Encrypt the ticket.
                            string encTicket = FormsAuthentication.Encrypt(ticket);

                            // Create the cookie.
                            string randomCookieValue = GenerateRandomCookieValue();
                            Session[SessionKeys.RandomCookieValue] = randomCookieValue;
                            SecurityPolicy objSecurity = new SecurityPolicy();
                            HttpCookie cookie = new HttpCookie(objSecurity.FormsCookieName(GetPortalID), encTicket);
                            SageFrameConfig objConfig = new SageFrameConfig();
                            string ServerCookieExpiration = objConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.ServerCookieExpiration);
                            int expiryTime = Math.Abs(int.Parse(ServerCookieExpiration));
                            expiryTime = expiryTime < 5 ? 5 : expiryTime;
                            cookie.Expires = DateTime.Now.AddMinutes(expiryTime);
                            Response.Cookies.Add(cookie);
                            string sessionCode = string.Empty;
                            ServiceSecurity.IssueToken(GetPortalID);
                            if (HttpContext.Current.Session.SessionID != null)
                            {
                                sessionCode = HttpContext.Current.Session.SessionID.ToString();
                            }
                            int CustomerID = GetUserDetailsByUserName(UserName.Text,GetPortalID).CustomerID;
                            UpdateCartAnonymoususertoRegistered(GetStoreID, GetPortalID, CustomerID, sessionCode);
                            if (!IsParent)
                            {
                                Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension), false);
                            }
                            else
                            {
                                Response.Redirect(ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension), false);
                            }
                        }
                    }
                }
            }
        }
        private string GenerateRandomCookieValue()
        {
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 10; i++)
                s = String.Concat(s, CapchaValue[this.random.Next(36)]);
            return s;
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

        public UserProfileInfo GetUserDetailsByUserName(string UserName, int PortalID)
        {
            return UserProfileController.GetProfile(UserName, PortalID);
        }
        public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
        {
            return UserProfileController.UpdateCartAnonymoususertoRegistered(storeID, portalID, customerID, sessionCode);
        }

        private void IsModuleInstalled(int rewardRuleID)
        {
            try
            {
                string moduleFriendlyName = "AspxRewardPoints";
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.CultureName = GetCurrentCultureName;
                bool isInstalled = AspxCommonController.GetModuleInstallationInfo(moduleFriendlyName, aspxCommonObj);
                if (isInstalled == true)
                {
                    //rewardRuleID = 1; ///Signing Up
                    //rewardRuleID = 2; ///NewsLetter
                    string uName = string.Empty;
                    string email = string.Empty;
                    if (Email.Text != string.Empty || Email.Text != null)
                    {
                        email = Email.Text;
                    }
                    uName = UserName.Text;

                    AspxRewardPointsController.RewardPointsSaveByCore(rewardRuleID, uName, email, aspxCommonObj);

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}