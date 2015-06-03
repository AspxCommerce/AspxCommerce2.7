#region "Copyright"
/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using SageFrame.Message;
using SageFrame.Web;
using SageFrame.Framework;
using System.Net.Mail;
using SageFrame.UserManagement;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Common;
using SageFrameClass.MessageManagement;
using SageFrame.SageFrameClass.MessageManagement;
#endregion

namespace SageFrame.Modules.PasswordRecovery
{
    public partial class ctl_ForgotPassword : BaseAdministrationUserControl
    {
        public string helpTemplate = string.Empty;
        private Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            IncludeLanguageJS();
            ForgotPasswordInfo objInfo = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_FORGOT_HELP, GetPortalID);
            if (objInfo != null)
            {
                helpTemplate = "<b>" + objInfo.Body + "</b>";
            }
            if (!IsPostBack)
            {
                InitializeCaptcha();
                GenerateCaptchaImage();
                //SetValidatorErrorMessage();
            }
        }

        private void SetValidatorErrorMessage()
        {
           

            FailureText.Text += string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "UserNameIsRequired"));
            FailureText.Text += string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "UserEmailAddressIsRequired"));
            FailureText.Text += string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "UserEmailAddressIsNotValid"));
        }

        private void InitializeCaptcha()
        {
            this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
            cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
            CaptchaImage.ImageUrl = "~/CaptchaImageHandler.aspx";
            Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
            rfvCaptchaValueValidator.Enabled = true;
        }

        private string GenerateRandomCode()
        {
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, CapchaValue[this.random.Next(36)]);
            return s;
        }

        protected void wzdForgotPassword_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                GotoLoginPage();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void GotoLoginPage()
        {
            string strRedURL = string.Empty;
            SageFrameConfig objSageConfig = new SageFrameConfig();
            if (!IsParent)
            {
                strRedURL = GetParentURL + "/portal/" + GetPortalSEOName + "/sf/" + objSageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension;
            }
            else
            {
                strRedURL = GetParentURL + "/sf/" + objSageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension;
            }
            Response.Redirect(strRedURL, false);
        }
        protected void wzdForgotPassword_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                
                if (ValidateCaptcha())
                {
                    MembershipController member = new MembershipController();
                    if (txtEmail.Text != "" && txtUsername.Text != "")
                    {
                        UserInfo user = member.GetUserDetails(GetPortalID, txtUsername.Text);
                        if (user.UserExists)
                        {
                            if (user.IsApproved == true)
                            {
                                if (user.Email.ToLower().Equals(txtEmail.Text.ToLower()))
                                {
                                    ForgotPasswordInfo objInfo = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_FORGOT_USERNAME_PASSWORD_MATCH, GetPortalID);
                                    if (objInfo != null)
                                    {
                                        ((Literal)WizardStep2.FindControl("litInfoEmailFinish")).Text = objInfo.Body;
                                    }
                                    List<ForgotPasswordInfo> objList = UserManagementController.GetMessageTemplateListByMessageTemplateTypeID(SystemSetting.PASSWORD_CHANGE_REQUEST_EMAIL, GetPortalID);
                                    foreach (ForgotPasswordInfo objPwd in objList)
                                    {
                                        DataTable dtTokenValues = UserManagementController.GetPasswordRecoveryTokenValue(txtUsername.Text, GetPortalID);
                                        CommonFunction comm = new CommonFunction();
                                        string replaceMessageSubject = MessageToken.ReplaceAllMessageToken(objPwd.Subject, dtTokenValues);
                                        string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(objPwd.Body, dtTokenValues);
                                        try
                                        {
                                            MailHelper.SendMailNoAttachment(objPwd.MailFrom, txtEmail.Text, replaceMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                                        }
                                        catch (Exception)
                                        {
                                            //divForgotPwd.Visible = false;
                                            InitializeCaptcha();
                                            CaptchaValue.Text = string.Empty;
                                            FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "SecureConnectionFPError"));
                                            e.Cancel = true;
                                        }
                                    }
                                }
                                else
                                {
                                    InitializeCaptcha();
                                    CaptchaValue.Text = string.Empty;
                                    FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "UsernameOrEmailAddressDoesnotMatched"));
                                    e.Cancel = true;
                                }
                            }
                            else
                            {
                                InitializeCaptcha();
                                CaptchaValue.Text = string.Empty;
                                FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "UsernameNotActivated"));
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            InitializeCaptcha();
                            CaptchaValue.Text = string.Empty;
                            FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserManagement", "UserDoesNotExist"));
                            e.Cancel = true;
                           
                        }
                    }
                    else
                    {
                        InitializeCaptcha();
                        e.Cancel = true;
                        CaptchaValue.Text = string.Empty;
                        FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("PasswordRecovery", "PleaseEnterAllTheRequiredFields"));
                    }
                }
                else
                {
                    InitializeCaptcha();
                    e.Cancel = true;
                    CaptchaValue.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private bool ValidateCaptcha()
        {
            FailureText.Text = string.Empty;
            if (!(cvCaptchaValue.ValueToCompare == CaptchaValue.Text))
            {
                //ShowMessage("", GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode"), "", SageMessageType.Error);
                FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode"));
                return false;
            }
            return true;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                FailureText.Text = string.Empty;
                GotoLoginPage();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void Refresh_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GenerateCaptchaImage();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void GenerateCaptchaImage()
        {
            try
            {
                this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
                cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
                CaptchaImage.ImageUrl = ResolveUrl("~") + "CaptchaImageHandler.aspx?=dummy" + DateTime.Now.ToLongTimeString();
                CaptchaValue.Text = "";
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}
