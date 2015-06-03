#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
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
using SageFrame.Framework;
using System.Data;
using SageFrame.Web;
using SageFrame.PortalSetting;
using System.Collections;
using SageFrame.Web.Utilities;
using SageFrame.SageFrameClass;
using SageFrame.Utilities;
using SageFrame.RolesManagement;
using SageFrame.Shared;
using System.IO;
using System.Text;
using SageFrame.Dashboard;
using SageFrame.Templating;
using System.Web.UI.HtmlControls;
using SageFrame.ModuleMessage;
using SageFrame.Security;
using SageFrame.Common;
using SageFrame.Core;
using SageFrame.Modules.Admin.PortalSettings;
using SageFrame.Localization;
#endregion

namespace SageFrame
{
    public partial class Sagin_Default : PageBase, SageFrameRoute
    {
        #region "Public Variables"

        public string ControlPath = string.Empty, SageFrameAppPath, SageFrameUserName;
        public string appPath = string.Empty;
        public string Extension;
        public string templateFavicon = string.Empty;
        public string userName = string.Empty;

        #endregion

        #region "Event Handlers"

        protected void Page_Init(object sender, EventArgs e)
        {
            SetPageInitPart();
        }
        protected override void OnInit(EventArgs e)
        {
            ViewStateUserKey = Session.SessionID;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageLoadPart();
            //CoreJs.IncludeLanguageCoreJs(this.Page);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("pragma", "no-cache");
            SageFrame.Version.SageFrameVersion app = new SageFrame.Version.SageFrameVersion();
            lblVersion.Text = string.Format("V {0}", app.FormatShortVersion(app.Version, true));
			AddLanguage();
        }

        #endregion

        #region "Public methods"

        public void LoadMessageControl()
        {
           

            PlaceHolder phdPlaceHolder = Page.FindControl("message") as PlaceHolder;
            if (phdPlaceHolder != null)
            {
                LoadControl(phdPlaceHolder, "~/Controls/Message.ascx");
            }
        }

        public override void ShowMessage(string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType)
        {
            string strCssClass = GetMessageCssClass(MessageType);
            int Cont = this.Page.Controls.Count;
            ControlCollection lstControls = Page.FindControl("form1").Controls;
            PlaceHolder phd = Page.FindControl("message") as PlaceHolder;
            if (phd != null)
            {
                foreach (Control c in phd.Controls)
                {
                    if (c.GetType().FullName.ToLower() == "ASP.Controls_message_ascx".ToLower())
                    {
                        SageUserControl tt = (SageUserControl)c;
                        tt.Modules_Message_ShowMessage(tt, MessageTitle, Message, CompleteMessage, isSageAsyncPostBack, MessageType, strCssClass);
                    }
                }
            }
        }

        #endregion

        #region "Private Methods"

        private void SetPageInitPart()
        {
            ltrJQueryLibrary.Text = GetAdminJqueryLibraryPath();
            CheckUserLogin();
            string PageName = Path.GetFileNameWithoutExtension(PagePath);
            //adminImage.ImageUrl = GetUserImage;
            if (PageName != null)
            {
                SecurityPolicy objSecurity = new SecurityPolicy();

                userName = objSecurity.GetUser(GetPortalID);
                templateFavicon = SetFavIcon(GetActiveTemplate);
                Extension = SageFrameSettingKeys.PageExtension;
                ApplicationController objAppController = new ApplicationController();
                objSecurity.UpdateExpireTime(userName, GetPortalID);
                if (!objAppController.CheckRequestExtension(Request))
                {
                    SageInitPart();
                }
                SetGlobalVariable();
                bool IsAdmin = true;
                IncludeStartup(GetPortalID, pchHolder, IsAdmin);
            }
            else
            {
                Response.Redirect(PortalAPI.PageNotAccessibleURL);
            }
        }

        private void CheckUserLogin()
        {
            if (!IsUserLoggedIn())
            {
                StringBuilder redirecPath = new StringBuilder();
                Uri url = HttpContext.Current.Request.Url;
                if (!IsParent)
                {
                    redirecPath.Append(url.Scheme);
                    redirecPath.Append("://");
                    redirecPath.Append(url.Authority);
                    redirecPath.Append(PortalAPI.GetApplicationName);
                    redirecPath.Append("/portal/");
                    redirecPath.Append(GetPortalSEOName);
                    redirecPath.Append("/");
                    redirecPath.Append(PortalAPI.LoginPageWithExtension);
                }
                else
                {
                    redirecPath.Append(url.Scheme);
                    redirecPath.Append("://");
                    redirecPath.Append(url.Authority);
                    redirecPath.Append(PortalAPI.LoginURL);
                }
                string strCurrentURL = Request.Url.ToString();
                if (redirecPath.ToString().Contains("?"))
                {
                    redirecPath.Append("&ReturnUrl=");
                    redirecPath.Append(strCurrentURL);
                }
                else
                {
                    redirecPath.Append("?ReturnUrl=");
                    redirecPath.Append(strCurrentURL);
                }
                HttpContext.Current.Response.Redirect(redirecPath.ToString());
            }
        }

        private void SetGlobalVariable()
        {
            appPath = GetAppPath();
            SageFrameAppPath = appPath;
            RegisterSageGlobalVariable();

        }

        private void SageInitPart()
        {
            ApplicationController objAppController = new ApplicationController();
            if (objAppController.IsInstalled())
            {
                if (!objAppController.CheckRequestExtension(Request))
                {
                    SetPortalCofig();
                    InitializePage();
                    hypUpgrade.NavigateUrl = "~/Admin/sfUpgrader" + Extension;
                    LoadMessageControl();
                    BindModuleControls();
                    SageFrameConfig sfConfig = new SageFrameConfig();
                    string defaultAdminTheme = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.DefaultAdminTheme);
                    string adminSidebarPosition = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.AdminSidebarPosition);
                    rdTemplate.SelectedValue = defaultAdminTheme;
                    if (adminSidebarPosition == "left")
                    {
                        rdLeft.Checked = true;
                        divSideBar.Attributes.Add("class", "sideBarLeft");
                    }
                    else
                    {
                        rdRight.Checked = true;
                        divSideBar.Attributes.Add("class", "sideBarRight");
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
            }
        }
        //private string GetUpgraderUrl()
        //{
        //    string upgradeLink = string.Empty;
        //    if (GetPortalID > 1)
        //    {
        //        upgradeLink = "~/portal/" + GetPortalSEOName + "/Admin/sfUpgrader" + Extension;
        //    }
        //    else
        //    {
        //        upgradeLink = "~/Admin/sfUpgrader" + Extension;
        //    }
        //    return upgradeLink;
        //}

        private void SetPortalCofig()
        {
            Hashtable hstPortals = GetPortals();
            SageUserControl suc = new SageUserControl();
            suc.PagePath = PagePath;
            int portalID = 1;
            #region "Get Portal SEO Name and PortalID"
            if (string.IsNullOrEmpty(Request.QueryString["ptSEO"]))
            {
                if (string.IsNullOrEmpty(PortalSEOName))
                {
                    PortalSEOName = GetDefaultPortalName(hstPortals, 1);
                }
                else if (!hstPortals.ContainsKey(PortalSEOName.ToLower().Trim()))
                {
                    PortalSEOName = GetDefaultPortalName(hstPortals, 1);
                }
                else
                {
                    portalID = int.Parse(hstPortals[PortalSEOName.ToLower().Trim()].ToString());
                }
            }
            else
            {
                PortalSEOName = Request.QueryString["ptSEO"].ToString().ToLower().Trim();
                portalID = Int32.Parse(Request.QueryString["ptlid"].ToString());
            }
            #endregion
            suc.SetPortalSEOName(PortalSEOName.ToLower().Trim());
            Session[SessionKeys.SageFrame_PortalSEOName] = PortalSEOName.ToLower().Trim();
            Session[SessionKeys.SageFrame_PortalID] = portalID;
            Session[SessionKeys.SageFrame_AdminTheme] = ThemeHelper.GetAdminTheme(GetPortalID, GetUsername);
            Globals.sysHst[ApplicationKeys.ActiveTemplate + "_" + portalID] = TemplateController.GetActiveTemplate(GetPortalID).TemplateSeoName;
            Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + portalID] = PresetHelper.LoadActivePagePreset(GetActiveTemplate, GetPageSEOName(Request.Url.ToString()));
            suc.SetPortalID(portalID);
            SetPortalID(portalID);
            #region "Set user credentials for modules"
            SecurityPolicy objSecurity = new SecurityPolicy();
            if (objSecurity.GetUser(GetPortalID) != string.Empty)
            {
                SettingProvider objSP = new SettingProvider();
                SageFrameConfig sfConfig = new SageFrameConfig();
                string strRoles = string.Empty;
                List<SageUserRole> sageUserRolles = objSP.RoleListGetByUsername(objSecurity.GetUser(GetPortalID), GetPortalID);
                if (sageUserRolles != null)
                {
                    foreach (SageUserRole userRole in sageUserRolles)
                    {
                        strRoles += userRole.RoleId + ",";
                    }
                }
                if (strRoles.Length > 1)
                {
                    strRoles = strRoles.Substring(0, strRoles.Length - 1);
                }
                if (strRoles.Length > 0)
                {
                    SetUserRoles(strRoles);
                }
            }
            #endregion
        }

        private void SetPageLoadPart()
        {
            ApplicationController objAppController = new ApplicationController();
            if (!objAppController.CheckRequestExtension(Request))
            {
                SagePageLoadPart();
            }
        }

        private void SagePageLoadPart()
        {
            if (!IsPostBack)
            {
                string sageNavigateUrl = string.Empty;
                SageFrameConfig sfConfig = new SageFrameConfig();

                if (!IsParent)
                {
                    sageNavigateUrl = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                }
                else
                {
                    sageNavigateUrl = GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                }
                hypHome.NavigateUrl = sageNavigateUrl;//GetHostURL + "/Admin/Admin" + Extension;
                // hypHome.Text = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                //hypHome.ImageUrl = GetAdminImageUrl("home.png", true);
                hypPreview.NavigateUrl = sageNavigateUrl;
                lnkAccount.NavigateUrl = GetProfileLink(sfConfig);
                Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetAdminImageUrl("ajax-loader.gif", true);
                }
                // bool IsAdmin = false;
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
                if (ticket != null)
                {
                    int LoggedInPortalID = int.Parse(ticket.UserData.ToString());
                    if (ticket.Name != ApplicationKeys.anonymousUser)
                    {
                        string[] sysRoles = SystemSetting.SYSTEM_SUPER_ROLES;
                        this.hypUpgrade.Visible = IsParent ? (Roles.IsUserInRole(ticket.Name, sysRoles[1]) ? true : false) : false;

                        if (GetPortalID == LoggedInPortalID || Roles.IsUserInRole(ticket.Name, sysRoles[0]))
                        {
                            RoleController _role = new RoleController();
                            string userinroles = _role.GetRoleNames(GetUsername, LoggedInPortalID);
                            if (userinroles != "" || userinroles != null)
                            {
                                bool isDashboardAccessible = _role.IsDashboardAccesible(GetUsername, GetPortalID);
                                foreach (string role in sysRoles)
                                {
                                    if (userinroles.ToLower() == role)
                                    {
                                        isDashboardAccessible = true;
                                    }
                                }
                                if (!isDashboardAccessible)
                                {
                                    divAdminControlPanel.Visible = false;
                                }
                            }
                            else
                            {
                                divAdminControlPanel.Visible = false;
                            }
                        }
                        else
                        {
                            divAdminControlPanel.Visible = false;
                        }
                    }
                    else
                    {
                        divAdminControlPanel.Visible = false;
                    }

                }
                if (IsHandheld())
                {
                    divAdminControlPanel.Visible = false;
                }
            }
            //SessionTrackerController sTracController = new SessionTrackerController();
            //sTracController.SetSessionTrackerValues(GetPortalID.ToString(), GetUsername);
        }

        private void BindModuleControls()
        {
            string preFix = string.Empty;
            string paneName = string.Empty;
            string ControlSrc = string.Empty;
            string phdContainer = string.Empty;
            string PageSEOName = string.Empty;
            SageUserControl suc = new SageUserControl();
            string PageName = PagePath;
            if (PagePath == null)
            {
                PageName = PagePath;
            }
            else
            {
                PageName = PagePath;
            }
            suc.PagePath = PageName;
            if (PagePath != null)
            {
                PageSEOName = GetPageSEOName(PagePath);
            }
            else
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                PageSEOName = GetPageSEOName(sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
            }
            //:TODO: Need to get controlType and pageID from the selected page from routing path
            //string controlType = "0";
            //string pageID = "2";
            StringBuilder redirecPath = new StringBuilder();
            Uri url = HttpContext.Current.Request.Url;
            if (PageSEOName != string.Empty)
            {
                DataSet dsPageSettings = new DataSet();
                SageFrameConfig sfConfig = new SageFrameConfig();
                dsPageSettings = sfConfig.GetPageSettingsByPageSEONameForAdmin("1", PageSEOName, GetUsername);
                if (bool.Parse(dsPageSettings.Tables[0].Rows[0][0].ToString()) == true)// Is Page Exists
                {
                    if (bool.Parse(dsPageSettings.Tables[0].Rows[0][1].ToString()) == true)// Is Page Aceessable
                    {
                        // Get ModuleControls data table
                        DataTable dtPages = dsPageSettings.Tables[1];
                        if (dtPages != null && dtPages.Rows.Count > 0)
                        {
                            OverridePageInfo(dtPages);
                        }
                        // Get ModuleDefinitions data table
                        DataTable dtPageModule = dsPageSettings.Tables[2];
                        List<string> moduleDefIDList = new List<string>();
                        if (dtPageModule != null && dtPageModule.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPageModule.Rows.Count; i++)
                            {
                                paneName = dtPageModule.Rows[i]["PaneName"].ToString();
                                if (string.IsNullOrEmpty(paneName))
                                    paneName = "ContentPane";
                                string UserModuleID = dtPageModule.Rows[i]["UserModuleID"].ToString();
                                ControlSrc = "/" + dtPageModule.Rows[i]["ControlSrc"].ToString();
                                string SupportsPartialRendering = dtPageModule.Rows[i]["SupportsPartialRendering"].ToString();
                                PlaceHolder phdPlaceHolder = (PlaceHolder)this.FindControl(paneName);
                                if (paneName.ToLower().Equals("navigation")) { divNavigation.Attributes.Add("style", "display:block"); }
                                if (paneName.ToLower().Equals("middlemaincurrent")) { divRight.Attributes.Add("style", "display:block"); }
                                if (paneName.ToLower().Equals("cpanel")) { divBottompanel.Attributes.Add("style", "display:block"); }
                                if (paneName.ToLower().Equals("lefta")) { divLeft.Attributes.Add("style", "display:block"); }
                                if (phdPlaceHolder != null)
                                {
                                    LoadControl(phdPlaceHolder, ControlSrc, paneName, UserModuleID, "", "", false, new HtmlGenericControl("div"), new HtmlGenericControl("span"), false);
                                    moduleDefIDList.Add(dtPageModule.Rows[i]["ModuleDefID"].ToString());
                                }
                            }
                        }
                        SetModuleDefList(moduleDefIDList);
                    }
                    else
                    {
                        if (!IsParent)
                        {
                            redirecPath.Append(url.Scheme);
                            redirecPath.Append("://");
                            redirecPath.Append(url.Authority);
                            redirecPath.Append(PortalAPI.GetApplicationName);
                            redirecPath.Append("/portal/");
                            redirecPath.Append(GetPortalSEOName);
                            redirecPath.Append("/");
                            redirecPath.Append(PortalAPI.LoginPageWithExtension);
                        }
                        else
                        {
                            redirecPath.Append(url.Scheme);
                            redirecPath.Append("://");
                            redirecPath.Append(url.Authority);
                            redirecPath.Append(PortalAPI.LoginURL);
                        }
                        string strCurrentURL = Request.Url.ToString();
                        if (redirecPath.ToString().Contains("?"))
                        {
                            redirecPath.Append("&ReturnUrl=");
                            redirecPath.Append(strCurrentURL);
                        }
                        else
                        {
                            redirecPath.Append("?ReturnUrl=");
                            redirecPath.Append(strCurrentURL);
                        }
                        HttpContext.Current.Response.Redirect(redirecPath.ToString());
                    }
                }
                else
                {

                    if (!IsParent)
                    {
                        redirecPath.Append(url.Scheme);
                        redirecPath.Append("://");
                        redirecPath.Append(url.Authority);
                        redirecPath.Append(PortalAPI.GetApplicationName);
                        redirecPath.Append("/portal/");
                        redirecPath.Append(GetPortalSEOName);
                        redirecPath.Append("/");
                        redirecPath.Append(PortalAPI.PageNotFoundPageWithExtension);
                    }
                    else
                    {
                        redirecPath.Append(url.Scheme);
                        redirecPath.Append("://");
                        redirecPath.Append(url.Authority);
                        redirecPath.Append(PortalAPI.PageNotFoundURL);
                    }

                    Response.Redirect(ResolveUrl(redirecPath.ToString()));
                }
            }
        }

        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
        }

        private string GetProfileLink(SageFrameConfig sfConfig)
        {
            string profileURL = "";
            if (!IsParent)
            {
                profileURL = GetParentURL + "/portal/" + GetPortalSEOName + "/" + "sfUser-Profile" + Extension;
            }
            else
            {
                profileURL = GetParentURL + "/Admin/" + "sfUser-Profile" + Extension;
            }
            return profileURL;
        }

        private bool LoadModuleInfo(PlaceHolder Container, int UserModuleID, int position)
        {
            bool status = false;
            ModuleMessageInfo objMessage = ModuleMessageController.GetModuleMessageByUserModuleID(UserModuleID, GetCurrentCulture());
            if (objMessage != null)
            {
                if (objMessage.IsActive)
                {
                    if (objMessage.MessagePosition == position)
                    {
                        string modeStyle = "sfPersist";
                        switch (objMessage.MessageMode)
                        {
                            case 0:
                                modeStyle = "sfPersist";
                                break;
                            case 1:
                                modeStyle = "sfSlideup";
                                break;
                            case 2:
                                modeStyle = "sfFadeout";
                                break;
                        }
                        string messageTypeStyle = "sfInfo";
                        switch (objMessage.MessageType)
                        {
                            case 0:
                                messageTypeStyle = "sfInfo";
                                break;
                            case 1:
                                messageTypeStyle = "sfWarning";
                                break;
                        }
                        string totalStyle = string.Format("{0} {1}", modeStyle, messageTypeStyle);
                        HtmlGenericControl moduleDiv = new HtmlGenericControl("div");
                        moduleDiv.Attributes.Add("class", "sfModuleinfo");
                        StringBuilder sb = new StringBuilder();
                        string CloseForEver = string.Format("close_{0}", UserModuleID);
                        sb.Append("<div class='" + totalStyle + "'><div class='sfLinks'><a class='sfClose' href='#'>Close</a>   <a class='sfNclose' id='" + CloseForEver + "' href='#'>Close and Never Show Again</a></div>");
                        sb.Append(objMessage.Message);
                        sb.Append("</div>");
                        moduleDiv.InnerHtml = sb.ToString();
                        Container.Controls.Add(moduleDiv);
                        status = true;
                    }
                }
            }
            return status;
        }

        protected void rdTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string themeCss = rdTemplate.SelectedValue;
            SettingProvider sageSP = new SettingProvider();
            sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.DefaultAdminTheme, themeCss, GetUsername, GetPortalID.ToString());
        }

        protected void rdLeft_CheckedChanged(object sender, EventArgs e)
        {
            divSideBar.Attributes.Remove("style");
            divSideBar.Attributes.Add("style", "float:left");
            divSideBar.Attributes.Add("class", "sideBarLeft");
            SettingProvider sageSP = new SettingProvider();
            sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.AdminSidebarPosition, "left", GetUsername, GetPortalID.ToString());
        }

        protected void rdRight_CheckedChanged(object sender, EventArgs e)
        {
            divSideBar.Attributes.Remove("style");
            divSideBar.Attributes.Add("style", "float:right");
            divSideBar.Attributes.Add("class", "sideBarRight");
            SettingProvider sageSP = new SettingProvider();
            sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.AdminSidebarPosition, "right", GetUsername, GetPortalID.ToString());
        }

        #endregion

        #region SageFrameRoute Members

        #region "LanguageChnage"
        private void AddLanguage()
        {
            int PortalID = GetPortalID;
            List<Language> lstLanguage = LocalizationSqlDataProvider.GetPortalLanguages(PortalID);
            List<Language> lstLanguageFlags = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(PortalID), PortalAPI.GetApplicationName));
            if (lstLanguage.Count < 1 || lstLanguageFlags.Count < 1)
            {
                languageSetting.Visible = false;
            }
            else
            {
                var query = from listlang in lstLanguage
                            join listflag in lstLanguageFlags
                                 on listlang.LanguageCode equals listflag.LanguageCode
                            select new
                            {
                                listlang.LanguageCode,
                                listflag.FlagPath
                            };
                StringBuilder ddlLanguage = new StringBuilder();
                string cultureCode = GetCurrentCulture();
                ddlLanguage.Append("<ul id=\"languageSelectAdmin\" class=\"sfListmenu\">");
                foreach (var item in query)
                {
                    if (item.LanguageCode == cultureCode)
                    {
                        ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\" class=\"languageSelectedAdmin\">");
                        ddlLanguage.Append(item.LanguageCode + "  " + "<img src=\"" + item.FlagPath + "\">");
                        ddlLanguage.Append("</li>");
                    }
                    else
                    {
                        ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\">");
                        ddlLanguage.Append(item.LanguageCode + "  " + "<img src=\"" + item.FlagPath + "\">");
                        ddlLanguage.Append("</li>");
                    }
                }
                ddlLanguage.Append("</ul>");
                languageSetting.Text = ddlLanguage.ToString();
            }
        }

        private List<Language> AddFlagPath(List<Language> lstAvailableLocales, string baseURL)
        {
            List<Language> filtered = new List<Language> { };
            string currentCulture = GetCurrentCultureInfo();
            foreach (Language li in lstAvailableLocales)
            {
                //if (li.LanguageCode != currentCulture)
                //{
                filtered.Add(li);
                //}
            }

            filtered.ForEach(
                delegate(Language obj)
                {
                    obj.FlagPath = baseURL + "/images/flags/" + obj.LanguageCode.Substring(obj.LanguageCode.IndexOf("-") + 1).ToLower() + ".png";
                }
                );
            return filtered;
        }
        public string GetCurrentCultureInfo()
        {
            if (HttpContext.Current.Session != null)
            {
                string code = HttpContext.Current.Session[SessionKeys.SageUICulture] == null ? "en-US" : HttpContext.Current.Session[SessionKeys.SageUICulture].ToString();
                return code;
            }
            return "en-US";
        }
        #endregion
        public string PagePath
        {
            get;
            set;
        }

        public string PortalSEOName
        {
            get;
            set;
        }
        public string UserModuleID
        {
            get;
            set;
        }
        public string ControlType
        {
            get;
            set;
        }
        public string ControlMode { get; set; }
        public string Key { get; set; }
        public string Param { get; set; }
        #endregion

    }
}
