#region "Copyright"
/*
SageFrame� - http://www.sageframe.com
Copyright (c) 2009-2012 by SageFrame
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
using SageFrame.Web;
using SageFrame.Localization;
using SageFrame.Framework;
using System.Collections.Specialized;
using SageFrame.Core.ListManagement;
using System.Collections;
using System.IO;
using SageFrame.SageFrameClass;
using System.Text;
using SageFrame.Modules.Admin.PortalSettings;
using SageFrame.Shared;
using SageFrame.Common;
using SageFrame.Pages;
using SageFrame.FileManager;
using SageFrame.Message;
using SageFrame.Security;
using SageFrame.SageFrameClass.MessageManagement;
using System.Data;
#endregion

namespace SageFrame.Modules.Admin.PortalSettings
{
    public partial class ctl_PortalSettings : BaseAdministrationUserControl
    {
        private string languageMode = "Normal";
        protected string BaseDir;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IncludeCss("PortalSettings", "/Modules/Admin/PortalSettings/css/popup.css");
                if (!IsPostBack)
                {
                    AddImageUrls();
                    BinDDls();
                    BindData();
                    SageFrameConfig sfConf = new SageFrameConfig();
                    ViewState["SelectedLanguageCulture"] = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultLanguage);
                    GetLanguageList();
                    GetFlagImage();
                }
                RoleController _role = new RoleController();
                string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
                if (!roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
                {
                    TabContainer.Tabs[2].Visible = false;
                    TabContainer.Tabs[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ShowHideSMTPCredentials()
        {
            if (Int32.Parse(rblSMTPAuthentication.SelectedItem.Value) == 1)
            {
                trSMTPUserName.Style.Add("display", "");
                trSMTPPassword.Style.Add("display", "");
            }
            else
            {
                trSMTPUserName.Style.Add("display", "none");
                trSMTPPassword.Style.Add("display", "none");
            }
        }


        protected void rblSMTPAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideSMTPCredentials();
        }

        protected void lnkTestSMTP_Click(object sender, EventArgs e)
        {
            TestEmailServer();
        }

        private void TestEmailServer()
        {
            try
            {
                if (txtSMTPServerAndPort.Text.Trim() != string.Empty)
                {
                    MailHelper.SendMailNoAttachment(txtHostEmail.Text, txtHostEmail.Text, "Test Email for configration", "Test Email", string.Empty, string.Empty);
                    lblSMTPEmailTestResult.Text = GetSageMessage("PortalSettings", "SMTPServerIsWorking");
                    lblSMTPEmailTestResult.Visible = true;
                    lblSMTPEmailTestResult.CssClass = "Normalbold";
                }
                else
                {
                    lblSMTPEmailTestResult.Text = GetSageMessage("PortalSettings", "PleaseFillServerAddress");
                    lblSMTPEmailTestResult.Visible = true;
                    lblSMTPEmailTestResult.CssClass = "NormalRed";
                }
            }
            catch (Exception ex)
            {
                lblSMTPEmailTestResult.Text = ex.Message;
                lblSMTPEmailTestResult.Visible = true;
                lblSMTPEmailTestResult.CssClass = "NormalRed";
            }
        }
        protected void imbRestart_Click(object sender, EventArgs e)
        {
            RestartApplication();
        }

        private void RestartApplication()
        {
            SageFrame.Application.Application app = new SageFrame.Application.Application();
            File.SetLastWriteTime((app.ApplicationMapPath + "\\web.config"), System.DateTime.Now);
            SecurityPolicy objSecurity = new SecurityPolicy();
            HttpCookie authenticateCookie = new HttpCookie(objSecurity.FormsCookieName(GetPortalID));
            authenticateCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(authenticateCookie);
            System.Web.Security.FormsAuthentication.SignOut();
            SetUserRoles(string.Empty);
            string redUrl = string.Empty;
            SageFrameConfig sfConfig = new SageFrameConfig();
            if (!IsParent)
            {
                redUrl = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension;
            }
            else
            {
                redUrl = GetParentURL + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension;
            }
            Response.Redirect(redUrl);
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


        private void BindSMTPAuthntication()
        {
            rblSMTPAuthentication.DataSource = SageFrameLists.SMTPAuthntication();
            rblSMTPAuthentication.DataTextField = "value";
            rblSMTPAuthentication.DataValueField = "key";
            rblSMTPAuthentication.DataBind();
            if (rblSMTPAuthentication.Items.Count > 0)
            {
                rblSMTPAuthentication.SelectedIndex = 0;
            }
        }
        private void AddImageUrls()
        {
            //imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            //imbRefresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
            //imbRestart.ImageUrl = GetAdminImageUrl("imgrestart.png", true);
        }

        private void BinDDls()
        {
            BindPageDlls();
            Bindlistddls();
            SageFrameConfig pagebase = new SageFrameConfig();
            BindddlTimeZone(pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultLanguage));
            BindRegistrationTypes();
            BindYesNoRBL();
        }

        private void BindData()
        {
            Hashtable hsts = GetAllSettings();
            SageFrameConfig pagebase = new SageFrameConfig();
            txtPortalTitle.Text = hsts[SageFrameSettingKeys.PageTitle].ToString();
            txtDescription.Text = hsts[SageFrameSettingKeys.MetaDescription].ToString();
            txtKeyWords.Text = hsts[SageFrameSettingKeys.MetaKeywords].ToString();
            txtCopyright.Text = Server.HtmlDecode(hsts[SageFrameSettingKeys.PortalCopyright].ToString());
            txtLogoTemplate.Text = Server.HtmlDecode(hsts[SageFrameSettingKeys.PortalLogoTemplate].ToString());
            txtPortalGoogleAdSenseID.Text = hsts[SageFrameSettingKeys.PortalGoogleAdSenseID].ToString();
            chkOptCss.Checked = bool.Parse(hsts[SageFrameSettingKeys.OptimizeCss].ToString());
            chkOptJs.Checked = bool.Parse(hsts[SageFrameSettingKeys.OptimizeJs].ToString());
            chkLiveFeeds.Checked = bool.Parse(hsts[SageFrameSettingKeys.EnableLiveFeeds].ToString());
            txtSiteAdminEmailAddress.Text = hsts[SageFrameSettingKeys.SiteAdminEmailAddress].ToString();
            chkShowSidebar.Checked = bool.Parse(hsts[SageFrameSettingKeys.ShowSideBar].ToString());
            //scheduler
            txtScheduler.Checked = bool.Parse(hsts[SageFrameSettingKeys.Scheduler].ToString());
            //added by Bj for OpenID
            chkOpenID.Checked = bool.Parse(hsts[SageFrameSettingKeys.ShowOpenID].ToString());

            int userAgent = int.Parse(hsts[SageFrameSettingKeys.UserAgentMode].ToString());

            switch (userAgent)
            {
                case 1:
                    rdBtnPC.Checked = true;
                    break;
                case 2:
                    rdBtnMobile.Checked = true;
                    break;
                case 3:
                    rdBtnDefault.Checked = true;
                    break;
            }

            if (chkOpenID.Checked == true)
            {
                tblOpenIDInfo.Visible = true;
            }
            else
            {
                tblOpenIDInfo.Visible = false;
            }

            chkDashboardHelp.Checked = bool.Parse(hsts[SageFrameSettingKeys.EnableDasboardHelp].ToString());
            txtServerCookieExpiration.Text = hsts[SageFrameSettingKeys.ServerCookieExpiration].ToString();
            chkEnableCDN.Checked = bool.Parse(hsts[SageFrameSettingKeys.EnableCDN].ToString());
            chkSessionTracker.Checked = bool.Parse(hsts[SageFrameSettingKeys.EnableSessionTracker].ToString());
            txtFacebookConsumerKey.Text = hsts[SageFrameSettingKeys.FaceBookConsumerKey].ToString();
            txtLinkedInConsumerKey.Text = hsts[SageFrameSettingKeys.LinkedInConsumerKey].ToString();
            txtLinkedInSecretKey.Text = hsts[SageFrameSettingKeys.LinkedInSecretKey].ToString();
            txtFaceBookSecretKey.Text = hsts[SageFrameSettingKeys.FaceBookSecretkey].ToString();

            if (rblPortalShowProfileLink.Items.Count > 0)
            {
                string ExistingPortalShowProfileLink = hsts[SageFrameSettingKeys.PortalShowProfileLink].ToString();
                rblPortalShowProfileLink.SelectedIndex = rblPortalShowProfileLink.Items.IndexOf(rblPortalShowProfileLink.Items.FindByValue(ExistingPortalShowProfileLink));
            }

            //RemeberMe setting
            chkEnableRememberme.Checked = bool.Parse(hsts[SageFrameSettingKeys.RememberCheckbox].ToString());


            if (rblUserRegistration.Items.Count > 0)
            {
                string ExistingData = hsts[SageFrameSettingKeys.PortalUserRegistration].ToString();
                rblUserRegistration.SelectedIndex = rblUserRegistration.Items.IndexOf(rblUserRegistration.Items.FindByValue(ExistingData));
            }


            if (ddlLoginPage.Items.Count > 0)
            {
                string ExistingPortalLoginpage = hsts[SageFrameSettingKeys.PortalLoginpage].ToString();
                ExistingPortalLoginpage = ExistingPortalLoginpage.StartsWith("sf/") ? ExistingPortalLoginpage.Replace("sf/", "") : ExistingPortalLoginpage;
                ddlLoginPage.SelectedIndex = ddlLoginPage.Items.IndexOf(ddlLoginPage.Items.FindByValue(ExistingPortalLoginpage));
            }

            if (ddlUserRegistrationPage.Items.Count > 0)
            {
                string ExistingPortalUserActivation = hsts[SageFrameSettingKeys.PortalUserActivation].ToString();
                ExistingPortalUserActivation = ExistingPortalUserActivation.StartsWith("sf/") ? ExistingPortalUserActivation.Replace("sf/", "") : ExistingPortalUserActivation;
                ddlPortalUserActivation.SelectedIndex = ddlPortalUserActivation.Items.IndexOf(ddlPortalUserActivation.Items.FindByValue(ExistingPortalUserActivation));
            }

            if (ddlUserRegistrationPage.Items.Count > 0)
            {
                string ExistingPortalRegistrationPage = hsts[SageFrameSettingKeys.PortalRegistrationPage].ToString();
                ExistingPortalRegistrationPage = ExistingPortalRegistrationPage.StartsWith("sf/") ? ExistingPortalRegistrationPage.Replace("sf/", "") : ExistingPortalRegistrationPage;
                ddlUserRegistrationPage.SelectedIndex = ddlUserRegistrationPage.Items.IndexOf(ddlUserRegistrationPage.Items.FindByValue(ExistingPortalRegistrationPage));
            }

            if (ddlPortalForgotPassword.Items.Count > 0)
            {
                string ExistingPortalForgotPassword = hsts[SageFrameSettingKeys.PortalForgotPassword].ToString();
                ExistingPortalForgotPassword = ExistingPortalForgotPassword.StartsWith("sf/") ? ExistingPortalForgotPassword.Replace("sf/", "") : ExistingPortalForgotPassword;
                ddlPortalForgotPassword.SelectedIndex = ddlPortalForgotPassword.Items.IndexOf(ddlPortalForgotPassword.Items.FindByValue(ExistingPortalForgotPassword));
            }

            //ddlPortalPageNotAccessible
            if (ddlPortalPageNotAccessible.Items.Count > 0)
            {
                string ExistingPortalPageNotAccessible = hsts[SageFrameSettingKeys.PortalPageNotAccessible].ToString();
                ExistingPortalPageNotAccessible = ExistingPortalPageNotAccessible.StartsWith("sf/") ? ExistingPortalPageNotAccessible.Replace("sf/", "") : ExistingPortalPageNotAccessible;
                ddlPortalPageNotAccessible.SelectedIndex = ddlPortalPageNotAccessible.Items.IndexOf(ddlPortalPageNotAccessible.Items.FindByValue(ExistingPortalPageNotAccessible));
            }

            //ddlPortalPageNotFound
            if (ddlPortalPageNotFound.Items.Count > 0)
            {
                string ExistingPortalPageNotFound = hsts[SageFrameSettingKeys.PortalPageNotFound].ToString();
                ExistingPortalPageNotFound = ExistingPortalPageNotFound.StartsWith("sf/") ? ExistingPortalPageNotFound.Replace("sf/", "") : ExistingPortalPageNotFound;

                ddlPortalPageNotFound.SelectedIndex = ddlPortalPageNotFound.Items.IndexOf(ddlPortalPageNotFound.Items.FindByValue(ExistingPortalPageNotFound));
            }

            //ddlPortalPasswordRecovery
            if (ddlPortalPasswordRecovery.Items.Count > 0)
            {
                string ExistingPortalPasswordRecovery = hsts[SageFrameSettingKeys.PortalPasswordRecovery].ToString();
                ExistingPortalPasswordRecovery = ExistingPortalPasswordRecovery.StartsWith("sf/") ? ExistingPortalPasswordRecovery.Replace("sf/", "") : ExistingPortalPasswordRecovery;

                ddlPortalPasswordRecovery.SelectedIndex = ddlPortalPasswordRecovery.Items.IndexOf(ddlPortalPasswordRecovery.Items.FindByValue(ExistingPortalPasswordRecovery));
            }

            //ddlPortalDefaultPage
            if (ddlPortalDefaultPage.Items.Count > 0)
            {
                string ExistingPortalDefaultPage = hsts[SageFrameSettingKeys.PortalDefaultPage].ToString();
                ExistingPortalDefaultPage = ExistingPortalDefaultPage.StartsWith("sf/") ? ExistingPortalDefaultPage.Replace("sf/", "") : ExistingPortalDefaultPage;
                ddlPortalDefaultPage.SelectedIndex = ddlPortalDefaultPage.Items.IndexOf(ddlPortalDefaultPage.Items.FindByValue(ExistingPortalDefaultPage));
            }

            //ddlPortalUserProfilePage
            if (ddlPortalUserProfilePage.Items.Count > 0)
            {
                string ExistingPortalUserProfilePage = hsts[SageFrameSettingKeys.PortalUserProfilePage].ToString();
                ExistingPortalUserProfilePage = ExistingPortalUserProfilePage.StartsWith("sf/") ? ExistingPortalUserProfilePage.Replace("sf/", "") : ExistingPortalUserProfilePage;
                ddlPortalUserProfilePage.SelectedIndex = ddlPortalUserProfilePage.Items.IndexOf(ddlPortalUserProfilePage.Items.FindByValue(ExistingPortalUserProfilePage));
            }

            if (ddlDefaultLanguage.Items.Count > 0)
            {
                string ExistingDefaultLanguage = hsts[SageFrameSettingKeys.PortalDefaultLanguage].ToString();

                ddlDefaultLanguage.SelectedIndex = ddlDefaultLanguage.Items.IndexOf(ddlDefaultLanguage.Items.FindByValue(ExistingDefaultLanguage));
                BindddlTimeZone(ddlDefaultLanguage.SelectedValue.ToString());
            }

            if (ddlPortalTimeZone.Items.Count > 0)
            {
                string ExistingPortalTimeZone = hsts[SageFrameSettingKeys.PortalTimeZone].ToString();
                ddlPortalTimeZone.SelectedIndex = ddlPortalTimeZone.Items.IndexOf(ddlPortalTimeZone.Items.FindByValue(ExistingPortalTimeZone));
            }



            ///Superuser settings

            SageFrame.Version.SageFrameVersion appVersion = new SageFrame.Version.SageFrameVersion();
            lblVVersion.Text = appVersion.FormatVersion(appVersion.Version, true);

            SageFrame.Application.Application app = new SageFrame.Application.Application();
            lblVProduct.Text = app.Description;
            lblVDataProvider.Text = app.DataProvider;
            lblVDotNetFrameWork.Text = app.NETFrameworkIISVersion.ToString();
            lblVASPDotNetIdentiy.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            lblVServerName.Text = app.DNSName;
            lblVIpAddress.Text = app.ServerIPAddress;
            lblVPermissions.Text = Framework.SecurityPolicy.Permissions;
            lblVRelativePath.Text = app.ApplicationPath;
            lblVPhysicalPath.Text = app.ApplicationMapPath;
            lblVServerTime.Text = DateTime.Now.ToString();
            lblVGUID.Text = hsts[SageFrameSettingKeys.GUID].ToString();
            BindSitePortal();
            if (ddlHostPortal.Items.Count > 0)
            {
                ddlHostPortal.SelectedIndex = ddlHostPortal.Items.IndexOf(ddlHostPortal.Items.FindByValue(hsts[SageFrameSettingKeys.SuperUserPortalId].ToString()));
            }
            txtHostTitle.Text = hsts[SageFrameSettingKeys.SuperUserTitle].ToString();
            txtHostUrl.Text = hsts[SageFrameSettingKeys.SuperUserURL].ToString();
            txtHostEmail.Text = hsts[SageFrameSettingKeys.SuperUserEmail].ToString();

            //Apprence
            chkCopyright.Checked = bool.Parse(hsts[SageFrameSettingKeys.SuperUserCopyright].ToString());
            chkUseCustomErrorMessages.Checked = bool.Parse(hsts[SageFrameSettingKeys.UseCustomErrorMessages].ToString());


            //SMTP
            txtSMTPServerAndPort.Text = hsts[SageFrameSettingKeys.SMTPServer].ToString();
            BindSMTPAuthntication();
            if (rblSMTPAuthentication.Items.Count > 0)
            {
                string ExistsSMTPAuth = hsts[SageFrameSettingKeys.SMTPAuthentication].ToString();
                if (!string.IsNullOrEmpty(ExistsSMTPAuth))
                {
                    rblSMTPAuthentication.SelectedIndex = rblSMTPAuthentication.Items.IndexOf(rblSMTPAuthentication.Items.FindByValue(ExistsSMTPAuth));
                }
            }
            chkSMTPEnableSSL.Checked = bool.Parse(hsts[SageFrameSettingKeys.SMTPEnableSSL].ToString());
            txtSMTPUserName.Text = hsts[SageFrameSettingKeys.SMTPUsername].ToString();
            string ExistsSMTPPassword = hsts[SageFrameSettingKeys.SMTPPassword].ToString();
            if (!string.IsNullOrEmpty(ExistsSMTPPassword))
            {
                txtSMTPPassword.Attributes.Add("value", ExistsSMTPPassword);
            }
            ShowHideSMTPCredentials();

            //Others
            txtFileExtensions.Text = hsts[SageFrameSettingKeys.FileExtensions].ToString();
            txtHelpUrl.Text = hsts[SageFrameSettingKeys.HelpURL].ToString();
            txtPageExtension.Text = hsts[SageFrameSettingKeys.SettingPageExtension].ToString();


            string ms = hsts[SageFrameSettingKeys.MessageTemplate].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMsgTemplate", " var MsgTemplate='" + ms + "';", true);

        }
        private void BindSitePortal()
        {
            try
            {
                SettingProvider spr = new SettingProvider();
                ddlHostPortal.DataSource = spr.GetAllPortals();
                ddlHostPortal.DataTextField = "Name";
                ddlHostPortal.DataValueField = "PortalID";
                ddlHostPortal.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private Hashtable GetAllSettings()
        {

            SettingProvider objSettingProvider = new SettingProvider();
            DataSet objData = objSettingProvider.GetAllSettings(GetPortalID.ToString(), string.Empty);
            Hashtable hstall = new Hashtable();
            DataTable dt = new DataTable();
            if (objData != null && objData.Tables != null && objData.Tables[0] != null)
            {
                dt = objData.Tables[0];
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        hstall.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                    }
                    catch
                    {
                        throw;
                    }

                }
            }
            return hstall;
        }

        private void BindPageDlls()
        {
            try
            {
                PageController objPageController = new PageController();
                List<PageEntity> LINQParentPages = objPageController.GetActivePortalPages(GetPortalID, GetUsername, "---", true, false, DBNull.Value, DBNull.Value);

                List<PageEntity> objFilterPageLst = FilterPages(LINQParentPages, "sf/");
                List<PageEntity> objstartUpPage = FilterPortalPage(LINQParentPages);

                ddlLoginPage.Items.Clear();
                ddlLoginPage.DataSource = objFilterPageLst;
                ddlLoginPage.DataTextField = "PageName";
                ddlLoginPage.DataValueField = "SEOName";
                ddlLoginPage.DataBind();
                ddlLoginPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                ddlUserRegistrationPage.Items.Clear();
                ddlUserRegistrationPage.DataSource = objFilterPageLst;
                ddlUserRegistrationPage.DataTextField = "PageName";
                ddlUserRegistrationPage.DataValueField = "SEOName";
                ddlUserRegistrationPage.DataBind();
                ddlUserRegistrationPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalUserActivation
                ddlPortalUserActivation.Items.Clear();
                ddlPortalUserActivation.DataSource = objFilterPageLst;
                ddlPortalUserActivation.DataTextField = "PageName";
                ddlPortalUserActivation.DataValueField = "SEOName";
                ddlPortalUserActivation.DataBind();
                ddlPortalUserActivation.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalForgotPassword
                ddlPortalForgotPassword.Items.Clear();
                ddlPortalForgotPassword.DataSource = objFilterPageLst;
                ddlPortalForgotPassword.DataTextField = "PageName";
                ddlPortalForgotPassword.DataValueField = "SEOName";
                ddlPortalForgotPassword.DataBind();
                ddlPortalForgotPassword.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPageNotAccessible
                ddlPortalPageNotAccessible.Items.Clear();
                ddlPortalPageNotAccessible.DataSource = objFilterPageLst;
                ddlPortalPageNotAccessible.DataTextField = "PageName";
                ddlPortalPageNotAccessible.DataValueField = "SEOName";
                ddlPortalPageNotAccessible.DataBind();
                ddlPortalPageNotAccessible.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPageNotFound
                ddlPortalPageNotFound.Items.Clear();
                ddlPortalPageNotFound.DataSource = objFilterPageLst;
                ddlPortalPageNotFound.DataTextField = "PageName";
                ddlPortalPageNotFound.DataValueField = "SEOName";
                ddlPortalPageNotFound.DataBind();
                ddlPortalPageNotFound.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPasswordRecovery
                ddlPortalPasswordRecovery.Items.Clear();
                ddlPortalPasswordRecovery.DataSource = objFilterPageLst;
                ddlPortalPasswordRecovery.DataTextField = "PageName";
                ddlPortalPasswordRecovery.DataValueField = "SEOName";
                ddlPortalPasswordRecovery.DataBind();
                ddlPortalPasswordRecovery.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalDefaultPage

                ddlPortalDefaultPage.Items.Clear();
                ddlPortalDefaultPage.DataSource = objstartUpPage;
                ddlPortalDefaultPage.DataTextField = "PageName";
                ddlPortalDefaultPage.DataValueField = "SEOName";
                ddlPortalDefaultPage.DataBind();
                ddlPortalDefaultPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));


                //ddlPortalUserProfilePage
                ddlPortalUserProfilePage.Items.Clear();
                ddlPortalUserProfilePage.DataSource = objFilterPageLst;
                ddlPortalUserProfilePage.DataTextField = "PageName";
                ddlPortalUserProfilePage.DataValueField = "SEOName";
                ddlPortalUserProfilePage.DataBind();
                ddlPortalUserProfilePage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private List<PageEntity> FilterPages(List<PageEntity> objPageLists, string Filter)
        {
            IEnumerable<PageEntity> results = from page in objPageLists
                                              where (page.PortalID != -1 || (page.PortalID == -1 && page.SEOName.StartsWith("sf")))
                                              select page;
            objPageLists = results.ToList<PageEntity>();
            return objPageLists;
        }

        private List<PageEntity> FilterPortalPage(List<PageEntity> objPageLists)
        {
            IEnumerable<PageEntity> results = from page in objPageLists
                                              where (page.PortalID != -1)
                                              select page;
            objPageLists = results.ToList<PageEntity>();
            return objPageLists;
        }

        private void Bindlistddls()
        {
            try
            {
                //ListManagementDataContext db = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                //var LINQ = db.sp_GetListEntrybyNameAndID("Country", -1,GetCurrentCultureName);
                //ddlDefaultLanguage.DataSource = ListManagementController.GetListEntrybyNameAndID("Country", -1,GetCurrentCultureName);
                //ddlDefaultLanguage.DataTextField = "Text";
                //ddlDefaultLanguage.DataValueField = "Value";
                //ddlDefaultLanguage.DataBind();
                //LINQ = db.sp_GetListEntrybyNameAndID("Processor", -1,GetCurrentCultureName);
                //ddlPaymentProcessor.DataSource = LINQ;
                //ddlPaymentProcessor.DataTextField = "Text";
                //ddlPaymentProcessor.DataValueField = "Value";
                //ddlPaymentProcessor.DataBind();

                ////ddlCurrency
                //LINQ = db.sp_GetListEntrybyNameAndID("Currency", -1,GetCurrentCultureName);
                //ddlCurrency.DataSource = LINQ;
                //ddlCurrency.DataTextField = "Text";
                //ddlCurrency.DataValueField = "Value";
                //ddlCurrency.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindddlTimeZone(string language)
        {
            try
            {
                NameValueCollection nvlTimeZone = SageFrame.Localization.Localization.GetTimeZones(language);
                ddlPortalTimeZone.DataSource = nvlTimeZone;
                ddlPortalTimeZone.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        private void BindRegistrationTypes()
        {
            rblUserRegistration.DataSource = SageFrameLists.UserRegistrationTypes();
            rblUserRegistration.DataValueField = "key";
            rblUserRegistration.DataTextField = "value";
            rblUserRegistration.DataBind();
        }

        private void BindRBLWithREF(RadioButtonList rbl)
        {
            rbl.DataSource = SageFrameLists.YESNO();
            rbl.DataTextField = "value";
            rbl.DataValueField = "key";
            rbl.DataBind();
            rbl.RepeatColumns = 2;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
        }

        private void BindYesNoRBL()
        {
            BindRBLWithREF(rblPortalShowProfileLink);
        }

        private void RefreshPage()
        {
            try
            {
                HttpRuntime.Cache.Remove(CacheKeys.SageSetting);
                BindData();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void SavePortalSettings()
        {
            try
            {
                SettingProvider sageSP = new SettingProvider();
                //Add Single Key Values that may contain Comma values so need to be add sepratly
                #region "Single Key Value Add/Updatge"

                //SageFrameSettingKeys.PageTitle
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PageTitle,
                    txtPortalTitle.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.MetaDescription
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.MetaDescription,
                    txtDescription.Text, GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.MetaKeywords
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.MetaKeywords,
                    txtKeyWords.Text, GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalLogoTemplate
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalLogoTemplate,
                    txtLogoTemplate.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalCopyright
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalCopyright,
                    txtCopyright.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalTimeZone
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalTimeZone,
                    ddlPortalTimeZone.SelectedItem.Value, GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.Message Setting       
                string mt = rdbDefault.Checked == true ? rdbDefault.Value : rdbCustom.Value;
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.MessageTemplate,
                    mt, GetUsername, GetPortalID.ToString());

                #endregion

                //For Multiple Keys and Values
                #region "Multiple Key Value Add/Update"

                StringBuilder sbSettingKey = new StringBuilder();
                StringBuilder sbSettingValue = new StringBuilder();
                StringBuilder sbSettingType = new StringBuilder();

                //Collecting Setting Values
                ///Super user settings
                StringBuilder sbSettingKey_super = new StringBuilder();
                StringBuilder sbSettingValue_super = new StringBuilder();
                StringBuilder sbSettingType_super = new StringBuilder();


                //SageFrameSettingKeys.SiteAdminEmailAddress
                sbSettingKey.Append(SageFrameSettingKeys.SiteAdminEmailAddress + ",");
                sbSettingValue.Append(txtSiteAdminEmailAddress.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalGoogleAdSenseID
                sbSettingKey.Append(SageFrameSettingKeys.PortalGoogleAdSenseID + ",");
                sbSettingValue.Append(txtPortalGoogleAdSenseID.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalShowProfileLink
                sbSettingKey.Append(SageFrameSettingKeys.PortalShowProfileLink + ",");
                sbSettingValue.Append(rblPortalShowProfileLink.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.RememberCheckbox
                sbSettingKey.Append(SageFrameSettingKeys.RememberCheckbox + ",");
                sbSettingValue.Append(chkEnableRememberme.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //CssJs Optimization
                sbSettingKey.Append(SageFrameSettingKeys.OptimizeCss + ",");
                sbSettingValue.Append(chkOptCss.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                sbSettingKey.Append(SageFrameSettingKeys.OptimizeJs + ",");
                sbSettingValue.Append(chkOptJs.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                sbSettingKey.Append(SageFrameSettingKeys.EnableLiveFeeds + ",");
                sbSettingValue.Append(chkLiveFeeds.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.ShowSideBar
                sbSettingKey.Append(SageFrameSettingKeys.ShowSideBar + ",");
                sbSettingValue.Append(chkShowSidebar.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalUserRegistration
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserRegistration + ",");
                sbSettingValue.Append(rblUserRegistration.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalLoginpage
                sbSettingKey.Append(SageFrameSettingKeys.PortalLoginpage + ",");
                sbSettingValue.Append(ddlLoginPage.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlLoginPage.SelectedItem.Value) : string.Format("{0},", ddlLoginPage.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalUserActivation
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserActivation + ",");
                sbSettingValue.Append(ddlPortalUserActivation.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalUserActivation.SelectedItem.Value) : string.Format("{0},", ddlPortalUserActivation.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalRegistrationPage
                sbSettingKey.Append(SageFrameSettingKeys.PortalRegistrationPage + ",");
                sbSettingValue.Append(ddlUserRegistrationPage.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlUserRegistrationPage.SelectedItem.Value) : string.Format("{0},", ddlUserRegistrationPage.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalForgotPassword
                sbSettingKey.Append(SageFrameSettingKeys.PortalForgotPassword + ",");
                sbSettingValue.Append(ddlPortalForgotPassword.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalForgotPassword.SelectedItem.Value) : string.Format("{0},", ddlPortalForgotPassword.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalPageNotAccessible
                sbSettingKey.Append(SageFrameSettingKeys.PortalPageNotAccessible + ",");
                sbSettingValue.Append(ddlPortalPageNotAccessible.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalPageNotAccessible.SelectedItem.Value) : string.Format("{0},", ddlPortalPageNotAccessible.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalPageNotFound
                sbSettingKey.Append(SageFrameSettingKeys.PortalPageNotFound + ",");
                sbSettingValue.Append(ddlPortalPageNotFound.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalPageNotFound.SelectedItem.Value) : string.Format("{0},", ddlPortalPageNotFound.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalPasswordRecovery
                sbSettingKey.Append(SageFrameSettingKeys.PortalPasswordRecovery + ",");
                sbSettingValue.Append(ddlPortalPasswordRecovery.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalPasswordRecovery.SelectedItem.Value) : string.Format("{0},", ddlPortalPasswordRecovery.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //PortalUserProfilePage
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserProfilePage + ",");
                sbSettingValue.Append(ddlPortalUserProfilePage.SelectedItem.Value.StartsWith("sf") ? string.Format("sf/{0},", ddlPortalUserProfilePage.SelectedItem.Value) : string.Format("{0},", ddlPortalUserProfilePage.SelectedItem.Value));
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //PortalDefaultPage
                sbSettingKey.Append(SageFrameSettingKeys.PortalDefaultPage + ",");
                sbSettingValue.Append(ddlPortalDefaultPage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalDefaultLanguage
                sbSettingKey.Append(SageFrameSettingKeys.PortalDefaultLanguage + ",");
                sbSettingValue.Append(ddlDefaultLanguage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //Added by Bj for OpenID conumer key and Secret key

                //SageFrameSettingKeys.FaceBookConsumerKey
                sbSettingKey.Append(SageFrameSettingKeys.ShowOpenID + ",");
                sbSettingValue.Append(chkOpenID.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.FaceBookConsumerKey
                sbSettingKey.Append(SageFrameSettingKeys.FaceBookConsumerKey + ",");
                sbSettingValue.Append(txtFacebookConsumerKey.Text + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.FaceBokkSecretkey
                sbSettingKey.Append(SageFrameSettingKeys.FaceBookSecretkey + ",");
                sbSettingValue.Append(txtFaceBookSecretKey.Text + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.LinkedInConsumerKey
                sbSettingKey.Append(SageFrameSettingKeys.LinkedInConsumerKey + ",");
                sbSettingValue.Append(txtLinkedInConsumerKey.Text + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.LinkedInSecretKey
                sbSettingKey.Append(SageFrameSettingKeys.LinkedInSecretKey + ",");
                sbSettingValue.Append(txtLinkedInSecretKey.Text + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.EnableCDN
                bool enableCDN = chkEnableCDN.Checked == true ? true : false;
                sbSettingKey.Append(SageFrameSettingKeys.EnableCDN + ",");
                sbSettingValue.Append(enableCDN + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.EnableSessionTracker
                bool enableSessionTracker = chkSessionTracker.Checked == true ? true : false;
                sbSettingKey.Append(SageFrameSettingKeys.EnableSessionTracker + ",");
                sbSettingValue.Append(enableSessionTracker + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.EnableDasboardHelp                    
                sbSettingKey_super.Append(SageFrameSettingKeys.EnableDasboardHelp + ",");
                sbSettingValue_super.Append(chkDashboardHelp.Checked + ",");
                sbSettingType_super.Append(SettingType.SiteAdmin + ",");

                RoleController _role = new RoleController();
                string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
                if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
                {
                    ///Superuser Settings 
                    //Collecting Setting Values
                    sbSettingKey_super.Append(SageFrameSettingKeys.SuperUserPortalId + ",");
                    sbSettingValue_super.Append(ddlHostPortal.SelectedItem.Value + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SuperUserTitle                
                    sbSettingKey_super.Append(SageFrameSettingKeys.SuperUserTitle + ",");
                    sbSettingValue_super.Append(txtHostTitle.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SuperUserURL
                    sbSettingKey_super.Append(SageFrameSettingKeys.SuperUserURL + ",");
                    sbSettingValue_super.Append(txtHostUrl.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SuperUserEmail
                    sbSettingKey_super.Append(SageFrameSettingKeys.SuperUserEmail + ",");
                    sbSettingValue_super.Append(txtHostEmail.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SuperUserCopyright
                    sbSettingKey_super.Append(SageFrameSettingKeys.SuperUserCopyright + ",");
                    sbSettingValue_super.Append(chkCopyright.Checked + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.UseCustomErrorMessages
                    sbSettingKey_super.Append(SageFrameSettingKeys.UseCustomErrorMessages + ",");
                    sbSettingValue_super.Append(chkUseCustomErrorMessages.Checked + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");


                    //SageFrameSettingKeys.UseFriendlyUrls
                    sbSettingKey_super.Append(SageFrameSettingKeys.UseFriendlyUrls + ",");
                    sbSettingValue_super.Append(true + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");



                    //SageFrameSettingKeys.SMTPServer
                    sbSettingKey_super.Append(SageFrameSettingKeys.SMTPServer + ",");
                    sbSettingValue_super.Append(txtSMTPServerAndPort.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SMTPAuthentication
                    sbSettingKey_super.Append(SageFrameSettingKeys.SMTPAuthentication + ",");
                    sbSettingValue_super.Append(rblSMTPAuthentication.SelectedItem.Value + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SMTPEnableSSL
                    sbSettingKey_super.Append(SageFrameSettingKeys.SMTPEnableSSL + ",");
                    sbSettingValue_super.Append(chkSMTPEnableSSL.Checked + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SMTPUsername
                    sbSettingKey_super.Append(SageFrameSettingKeys.SMTPUsername + ",");
                    sbSettingValue_super.Append(txtSMTPUserName.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SMTPPassword
                    sbSettingKey_super.Append(SageFrameSettingKeys.SMTPPassword + ",");
                    sbSettingValue_super.Append(txtSMTPPassword.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");


                    //SageFrameSettingKeys.FileExtensions
                    sbSettingKey_super.Append(SageFrameSettingKeys.FileExtensions + ",");
                    sbSettingValue_super.Append(txtFileExtensions.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.HelpURL
                    sbSettingKey_super.Append(SageFrameSettingKeys.HelpURL + ",");
                    sbSettingValue_super.Append(txtHelpUrl.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.SettingPageExtension
                    sbSettingKey_super.Append(SageFrameSettingKeys.SettingPageExtension + ",");
                    sbSettingValue_super.Append(txtPageExtension.Text.Trim() + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.Scheduler
                    sbSettingKey_super.Append(SageFrameSettingKeys.Scheduler + ",");
                    sbSettingValue_super.Append(txtScheduler.Checked + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                    //SageFrameSettingKeys.UserAgentMode
                    int userAgent = rdBtnPC.Checked == true ? 1 : (rdBtnMobile.Checked == true ? 2 : 3);
                    sbSettingKey_super.Append(SageFrameSettingKeys.UserAgentMode + ",");
                    sbSettingValue_super.Append(userAgent + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");




                    //SageFrameSettingKeys.ServerCookieExpiration
                    sbSettingKey_super.Append(SageFrameSettingKeys.ServerCookieExpiration + ",");
                    sbSettingValue_super.Append(txtServerCookieExpiration.Text + ",");
                    sbSettingType_super.Append(SettingType.SuperUser + ",");

                }
                string SettingTypes = sbSettingType.ToString();
                if (SettingTypes.Contains(","))
                {
                    SettingTypes = SettingTypes.Remove(SettingTypes.LastIndexOf(","));
                }
                string SettingKeys = sbSettingKey.ToString();
                if (SettingKeys.Contains(","))
                {
                    SettingKeys = SettingKeys.Remove(SettingKeys.LastIndexOf(","));
                }
                string SettingValues = sbSettingValue.ToString();
                if (SettingValues.Contains(","))
                {
                    SettingValues = SettingValues.Remove(SettingValues.LastIndexOf(","));
                }
                string SettingTypes_super = sbSettingType_super.ToString();
                if (SettingTypes_super.Contains(","))
                {
                    SettingTypes_super = SettingTypes_super.Remove(SettingTypes_super.LastIndexOf(","));
                }
                string SettingKeys_super = sbSettingKey_super.ToString();
                if (SettingKeys_super.Contains(","))
                {
                    SettingKeys_super = SettingKeys_super.Remove(SettingKeys_super.LastIndexOf(","));
                }
                string SettingValues_super = sbSettingValue_super.ToString();
                if (SettingValues_super.Contains(","))
                {
                    SettingValues_super = SettingValues_super.Remove(SettingValues_super.LastIndexOf(","));
                }

                sageSP.SaveSageSettings(SettingTypes, SettingKeys, SettingValues, GetUsername, GetPortalID.ToString());
                if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
                {
                    sageSP.SaveSageSettings(SettingTypes_super, SettingKeys_super, SettingValues_super, GetUsername, "1");
                }
                HttpRuntime.Cache.Remove(CacheKeys.SageSetting);
                BindData();
                #endregion
                ShowMessage("", GetSageMessage("PortalSettings", "PortalSettingIsSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbSave_Click(object sender, EventArgs e)
        {           
            int expire;
            int.TryParse(txtServerCookieExpiration.Text, out expire);            
            if (expire != 0)
            {
                SavePortalSettings();
            }
            else
            {
                ShowMessage("", GetSageMessage("PortalSettings", "ServerCookieExpiration"), "", SageMessageType.Success);
            }
        }

        //protected void lnkSave_Click(object sender, EventArgs e)
        //{
        //    SavePortalSettings();
        //}

        protected void imbRefresh_Click(object sender, EventArgs e)
        {
            RefreshPage();
        }

        //protected void lnkRefresh_Click(object sender, EventArgs e)
        //{
        //    RefreshPage();
        //}
        protected void ddlDefaultLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFlagImage();
            ViewState["SelectedLanguageCulture"] = this.ddlDefaultLanguage.SelectedValue;
            string language = this.ddlDefaultLanguage.SelectedValue;
            BindddlTimeZone(language);
        }
        protected void GetFlagImage()
        {
            string code = this.ddlDefaultLanguage.SelectedValue;
            imgFlag.ImageUrl = ResolveUrl(GetHostURL() + "/images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
        }
        protected void rbLanguageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbLanguageType.SelectedIndex)
            {
                case 0:
                    GetLanguageList();
                    break;
                case 1:
                    LoadNativeNames();
                    break;
            }
        }
        protected void LoadNativeNames()
        {
            languageMode = "Native";
            GetLanguageList();
        }
        public void GetLanguageList()
        {
            string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
            List<Language> lstAvailableLocales = LocaleController.AddNativeNamesToList(LocalizationSqlDataProvider.GetAvailableLocales());

            ddlDefaultLanguage.DataSource = lstAvailableLocales;
            ddlDefaultLanguage.DataTextField = mode;
            ddlDefaultLanguage.DataValueField = "LanguageCode";
            ddlDefaultLanguage.DataBind();
            ddlDefaultLanguage.SelectedIndex = ddlDefaultLanguage.Items.IndexOf(ddlDefaultLanguage.Items.FindByValue(ViewState["SelectedLanguageCulture"].ToString()));
            ViewState["RowCount"] = lstAvailableLocales.Count;
        }

        protected void btnRefreshCache_Click(object sender, EventArgs e)
        {
            HttpRuntime.Cache.Remove(CacheKeys.SageFrameCss);
            HttpRuntime.Cache.Remove(CacheKeys.SageFrameJs);
            string optimized_path = Server.MapPath(SageFrameConstants.OptimizedResourcePath);
            IOHelper.DeleteDirectoryFiles(optimized_path, ".js,.css");
            if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedCssMap)))
            {
                XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedCssMap), "resourcemaps/resourcemap");
            }
            if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedJsMap)))
            {
                XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedJsMap), "resourcemap/resourcemap");
            }
        }
        protected void chkOpenID_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOpenID.Checked == true)
            {
                tblOpenIDInfo.Visible = true;
            }
            else
            {
                tblOpenIDInfo.Visible = false;
            }
        }
    }
}