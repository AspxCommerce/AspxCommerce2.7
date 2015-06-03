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
using System.Data;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using SageFrame.Framework;
using SageFrame.UserManagement;
using SageFrame.Web;
using SageFrame.Message;
using SageFrame.Security.Helpers;
using SageFrame.Security.Entities;
using SageFrame.Security;
using SageFrameClass.MessageManagement;
using SageFrame.SageFrameClass.MessageManagement;
#endregion

namespace SageFrame.Modules.PasswordRecovery
{
    public partial class ctl_RecoverPassword : BaseUserControl
    {
        public string helpTemplate = string.Empty;
        MembershipController m = new MembershipController();
        ForgotPasswordInfo sageframeuser = new ForgotPasswordInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            ForgotPasswordInfo objInfo = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_HELP, GetPortalID);
            if (objInfo != null)
            {
                helpTemplate = objInfo.Body;
            }
            if (!IsPostBack)
            {
                string RecoveringCode = string.Empty;
                if (Request.QueryString["RecoveringCode"] != null)
                {
                    RecoveringCode = Request.QueryString["RecoveringCode"].ToString();
                    try
                    {
                        RecoveringCode = EncryptionMD5.Decrypt(RecoveringCode);
                        hdnRecoveryCode.Value = RecoveringCode;
                        AddImageUrls();
                        sageframeuser = UserManagementController.GetUsernameByActivationOrRecoveryCode(hdnRecoveryCode.Value, GetPortalID);
                        if (sageframeuser.CodeForUsername != null)
                        {
                            if (sageframeuser.IsAlreadyUsed)
                            {
                                ShowMessage("", GetSageMessage("PasswordRecovery", "RecoveryCodeAlreadyActivated"), "", SageMessageType.Alert);
                                divRecoverpwd.Visible = false;
                            }
                            else
                            {
                                divRecoverpwd.Visible = true;
                            }
                        }
                        else
                        {
                            divRecoverpwd.Visible = false;
                            ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                        }
                    }
                    catch
                    {
                        ShowMessage("", GetSageMessage("PasswordRecovery", "InvalidRecoveringCode"), "", SageMessageType.Alert);
                        divRecoverpwd.Visible = false;
                    }
                }
                else
                {
                    ShowMessage("", GetSageMessage("PasswordRecovery", "RecoveringCodeIsNotAvailable"), "", SageMessageType.Error);
                    divRecoverpwd.Visible = false;
                }
                SetValidatorErrorMessage();
            }
        }

        private void AddImageUrls()
        {
            wzdPasswordRecover.CancelButtonImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            wzdPasswordRecover.StartNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wzdPasswordRecover.StepNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wzdPasswordRecover.StepPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wzdPasswordRecover.FinishPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wzdPasswordRecover.FinishCompleteButtonImageUrl = GetTemplateImageUrl("imgfinished.png", true);
        }

        private void SetValidatorErrorMessage()
        {
            rfvRecoveredPassword.Text = "*";
            rfvRetypePassword.Text = "*";
            cvPassword.ErrorMessage = GetSageMessage("PasswordRecovery", "PasswordDoesnotMatched");
            rfvRecoveredPassword.ErrorMessage = GetSageMessage("PasswordRecovery", "PasswordIsRequired");
            rfvRetypePassword.ErrorMessage = GetSageMessage("PasswordRecovery", "RetypePassword");
            cvPassword.Text = "*";
        }

        protected void wzdPasswordRecover_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            hdnRecoveryCode.Value = "";
            GotoLoginPage();
        }
        private void GotoLoginPage()
        {
            SageFrameConfig objSageConfig = new SageFrameConfig();
            if (!IsParent)
            {
                Response.Redirect(GetParentURL + "/portal/" + GetPortalSEOName + "/sf/" + objSageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension);
            }
            else
            {
                Response.Redirect(GetParentURL + "/sf/" + objSageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension);
            }

        }
        protected void wzdPasswordRecover_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                if (txtPassword.Text != null && txtRetypePassword.Text != "" && txtRetypePassword.Text == txtPassword.Text)
                {
                    if (txtPassword.Text.Length < 4)
                    {
                        ShowMessage("", GetSageMessage("PasswordRecovery", "PasswordLength"), "", SageMessageType.Alert);
                        e.Cancel = true;
                    }
                    else
                    {
                        if (hdnRecoveryCode.Value != "")
                        {
                            sageframeuser = UserManagementController.GetUsernameByActivationOrRecoveryCode(hdnRecoveryCode.Value, GetPortalID);
                            if (sageframeuser.CodeForUsername != null)
                            {
                                UserInfo userOld = m.GetUserDetails(GetPortalID, sageframeuser.CodeForUsername);
                                string Password, PasswordSalt;
                                PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtPassword.Text, out Password, out PasswordSalt);
                                UserInfo user = new UserInfo(userOld.UserID, Password, PasswordSalt, m.PasswordFormat);
                                m.ChangePassword(user);
                                List<ForgotPasswordInfo> messageTemplates = UserManagementController.GetMessageTemplateListByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCCESSFUL_EMAIL, GetPortalID);
                                foreach (ForgotPasswordInfo messageTemplate in messageTemplates)
                                {
                                    DataTable dtTokenValues = UserManagementController.GetPasswordRecoverySuccessfulTokenValue(userOld.UserName, GetPortalID);
                                    string replacedMessageSubject = MessageToken.ReplaceAllMessageToken(messageTemplate.Subject, dtTokenValues);
                                    string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, dtTokenValues);
                                    try
                                    {
                                        MailHelper.SendMailNoAttachment(messageTemplate.MailFrom, userOld.Email, replacedMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                                    }
                                    catch (Exception)
                                    {
                                        ShowMessage("", GetSageMessage("PasswordRecovery", "SecureConnectionFPRError"), "", SageMessageType.Alert);
                                        e.Cancel = true;
                                        divRecoverpwd.Visible = false;
                                    }
                                }
                                UserManagementController.DeactivateRecoveryCode(userOld.UserName, GetPortalID);
                                ForgotPasswordInfo template = UserManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCESSFUL_INFORMATION, GetPortalID);
                                if (template != null)
                                {
                                    ((Literal)WizardStep2.FindControl("litPasswordChangedSuccessful")).Text = template.Body;
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                                ShowMessage("", GetSageMessage("PasswordRecovery", "UnknownErrorPleaseTryAgaing"), "", SageMessageType.Alert);
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            ShowMessage("", GetSageMessage("PasswordRecovery", "UnknownError"), "", SageMessageType.Alert);
                        }
                    }
                }
                else
                {
                    ShowMessage("", GetSageMessage("PasswordRecovery", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnRecoveryCode.Value = string.Empty;
            GotoLoginPage();
        }
    }
}