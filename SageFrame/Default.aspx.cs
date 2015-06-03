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
using System.Web.UI.HtmlControls;
using SageFrame.Web.Common.SEO;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SageFrame.Templating;
using SageFrame.ModuleControls;
using SageFrame.Templating.xmlparser;
using SageFrame.Common;
using SageFrame.Security;
using System.Globalization;
using SageFrame.UserAgent;
using SageFrame;
using SageFrame.Core;
using SageFrame.Pages;

#endregion

namespace SageFrame
{
    public partial class _Default : PageBase, SageFrameRoute
    {
        #region "Public Varibales"

        public string ControlPath = string.Empty;
        public static string path = string.Empty, appPath = string.Empty, layoutcontrol = string.Empty;
        public static int ModuleId = 0;
        public string prevpage, templatefavicon = "favicon.ico";
        public string Extension = string.Empty;

        #endregion

        #region "Event Handlers"

        string activeTemplate = string.Empty;
        int currentportalID = 0;
        protected void Page_Init(object sender, EventArgs e)
        {
            SetPageInitPart();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageLoadPart();
        }

        protected void Page_End(object sender, EventArgs e)
        {

        }

        #endregion

        #region "Public Methods"

        public void BuildLayoutControl()
        {
            string pagename = PagePath;
            if (pagename == null)
            {
                pagename = "default";
            }
            SageFrameConfig sfConfig = new SageFrameConfig();
            pagename = pagename.ToLower().Equals("default") ? sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") : pagename.Replace(" ", "-");
            layoutcontrol = "";
            switch (HandHeldMode())
            {
                case 1:
                    layoutcontrol = PresetHelper.LoadActivePresetForPage(activeTemplate, pagename);
                    break;
                case 2:
                    layoutcontrol = PresetHelper.LoadHandheldControl(activeTemplate);
                    break;
                case 3:
                    layoutcontrol = PresetHelper.LoadDeviceType3(activeTemplate);
                    break;
            }
            LoadControl(pchWhole, layoutcontrol, 3);
        }

        public int HandHeldMode()
        {
            string GetMode = GetUserAgent();
            int Mode = 1;
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            if (GetMode == "3")
            {
                if (strUserAgent != null)
                {
                    if (Request.Browser.IsMobileDevice == false)
                    {
                        Mode = 1;
                    }
                    else if (Request.Browser.IsMobileDevice == true)
                    {
                        Mode = 2;
                        #region "Need to modify when new device mapping will be allowed"
                        //if ((strUserAgent.Contains("iphone") ||
                        //strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                        //strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                        //strUserAgent.Contains("palm")))
                        //{
                        //    Mode = 2;
                        //}
                        //else
                        //{
                        //    Mode = 2;
                        //} 
                        #endregion
                    }
                }
            }
            else
            {
                Mode = int.Parse(GetMode);
            }
            return Mode;
        }

        public string GetUserAgent()
        {
            UserAgentController objController = new UserAgentController();
            string getMode = string.Empty;
            if (Globals.sysHst[ApplicationKeys.UserAgent + "_" + currentportalID] != null)
            {
                getMode = Globals.sysHst[ApplicationKeys.UserAgent + "_" + currentportalID].ToString();
            }
            else
            {
                getMode = objController.GetUserAgent(currentportalID, true);
                Globals.sysHst[ApplicationKeys.UserAgent + "_" + currentportalID] = getMode;
            }
            return getMode;
        }

        public HtmlGenericControl GetModuleControls(int UserModuleID, bool ContainsEdit, int ControlCount, int moduleDefID)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            if (ContainsEdit && ControlCount > 1)
            {
                div.Attributes.Add("class", "sfModuleControl");
                string url = string.Format("{0}/Sagin/HandleModuleControls" + Extension + "?uid={1}&pid={2}&mdefID={3}", appPath, UserModuleID, currentportalID, moduleDefID);
                string html = "<a class='sfManageControl' rel='" + url + "'></a>";
                div.InnerHtml = html;
            }
            return div;
        }

        public HtmlGenericControl GetPaneNameContainer(string PaneName)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("class", "sfPosition");
            span.Attributes.Add("style", "display:none;");
            span.InnerHtml = PaneName;
            return span;
        }

        /// <summary>
        /// Show Message In The Page
        /// </summary>
        /// <param name="MessageTitle"> Display Message Title</param>
        /// <param name="Message"> Display Message</param>
        /// <param name="CompleteMessage">Complete Message</param>
        /// <param name="isSageAsyncPostBack">Set True If Update Panel Post Back</param>
        /// <param name="MessageType">Message Type</param>
        public override void ShowMessage(string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType)
        {
            string strCssClass = GetMessageCssClass(MessageType);
            int Cont = this.Page.Controls.Count;
            ControlCollection lstControls = Page.FindControl("form1").Controls;
            UserControl uc = pchWhole.FindControl("lytA") as UserControl;
            PlaceHolder phd = uc.FindControl("pch_message") as PlaceHolder;
            if (phd != null)
            {
                foreach (Control c in phd.Controls)
                {
                    if (c.GetType().FullName.ToLower() == "ASP.controls_message_ascx".ToLower())
                    {
                        SageUserControl tt = (SageUserControl)c;
                        tt.Modules_Message_ShowMessage(tt, MessageTitle, Message, CompleteMessage, isSageAsyncPostBack, MessageType, strCssClass);
                    }
                }
            }
        }

        public void ManageSSLConnection()
        {
            ApplicationController objAppController = new ApplicationController();
            if (!objAppController.CheckRequestExtension(Request))
            {
                if (Session[SessionKeys.Ssl] == null)
                {
                    Session[SessionKeys.Ssl] = "True";
                    //check logic redirect to or not
                    //btn click login and logout prob
                    PageController objController = new PageController();
                    List<SecurePageInfo> sp = objController.GetSecurePage(currentportalID, GetCurrentCulture());
                    string pagename = GetPageSEOName(PagePath);
                    if (pagename != "Page-Not-Found")
                    {
                        if (Session[SessionKeys.pagename] != null)
                        {
                            prevpage = Session[SessionKeys.pagename].ToString();
                        }
                        if (prevpage != pagename)
                        {
                            Session[SessionKeys.pagename] = pagename;
                            for (int i = 0; i < sp.Count; i++)
                            {
                                if (pagename.ToLower() == sp[i].SecurePageName.ToString().ToLower())
                                {
                                    if (bool.Parse(sp[i].IsSecure.ToString()))
                                    {
                                        if (!HttpContext.Current.Request.IsSecureConnection)
                                        {
                                            if (!HttpContext.Current.Request.Url.IsLoopback) //Don't check when in development mode (i.e. localhost)
                                            {
                                                Session[SessionKeys.prevurl] = "https";
                                                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Session[SessionKeys.prevurl] = "http";
                                        Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                                    }
                                }
                            }
                        }
                        else if (Session[SessionKeys.prevurl] != null)
                        {
                            if (Session[SessionKeys.prevurl].ToString() != Request.Url.ToString().Split(':')[0].ToString())
                            {
                                for (int i = 0; i < sp.Count; i++)
                                {
                                    if (pagename.ToLower() == sp[i].SecurePageName.ToString().ToLower())
                                    {
                                        if (bool.Parse(sp[i].IsSecure.ToString()))
                                        {
                                            if (!HttpContext.Current.Request.IsSecureConnection)
                                            {
                                                if (!HttpContext.Current.Request.Url.IsLoopback) //Don't check when in development mode (i.e. localhost)
                                                {
                                                    Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region "Private Methods"

        private void SetPageInitPart()
        {
            ltrJQueryLibrary.Text = GetJqueryLibraryPath();
            Extension = SageFrameSettingKeys.PageExtension;
            activeTemplate = GetActiveTemplate;
            currentportalID = GetPortalID;
            try
            {
                SetPortalCofig();
                BuildLayoutControl();
                templatefavicon = SetFavIcon(activeTemplate);
            }
            catch (Exception ex)
            {
                Session[SessionKeys.TemplateError] = ex;
            }
            SageInitPart();
            //ManageSSLConnection();
            SetGlobalVariable();
            bool IsAdmin = false;
            IncludeStartup(currentportalID, pchWhole, IsAdmin);

        }

        private void SetGlobalVariable()
        {
            appPath = GetAppPath();
            RegisterSageGlobalVariable();
        }

        private void SageInitPart()
        {
            try
            {
                ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsInstalled())
                {
                    if (!objAppController.CheckRequestExtension(Request))
                    {
                        InitializePage();
                        SageFrameConfig sfConfig = new SageFrameConfig();
                        SetAdminParts();
                        BindModuleControls();
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
                }
            }
            catch
            {
                //throw ex;
            }
        }

        private void SetAdminParts()
        {
            SecurityPolicy objSecurity = new SecurityPolicy();
            HttpCookie authCookie = Request.Cookies[objSecurity.FormsCookieName(GetPortalID)];
            if (authCookie != null)
            {
                RoleController _role = new RoleController();
                bool isDashboardAccessible = _role.IsDashboardAccesible(GetUsername, GetPortalID);
                if (isDashboardAccessible)
                {
                    divAdminControlPanel.Visible = true;
                    ApplicationController objAppController = new ApplicationController();
                    // objAppController.ChangeCss(Page, "pchWhole", "lytA", "sfOuterWrapper", "style", "margin-top:30px");
                }
            }
            else
            {
                divAdminControlPanel.Visible = false;
            }
            if (IsHandheld())
            {
                divAdminControlPanel.Visible = false;
            }
        }

        private void SetPortalCofig()
        {
            Hashtable hstPortals = GetPortals();
            SageUserControl suc = new SageUserControl();
            int portalID = 1;

            #region "Get Portal SEO Name and PortalID"
            if (string.IsNullOrEmpty(Request.QueryString["ptSEO"]))
            {
                if (string.IsNullOrEmpty(PortalSEOName))
                {
                    PortalSEOName = GetDefaultPortalName(hstPortals, 1);// 1 is for Default Portal
                }
                else if (!hstPortals.ContainsKey(PortalSEOName.ToLower().Trim()))
                {
                    PortalSEOName = GetDefaultPortalName(hstPortals, 1);// 1 is for Default Portal
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
            string tempName = /*activeTemplate;// */ TemplateController.GetActiveTemplate(currentportalID).TemplateSeoName;
            string tempPath = Decide.IsTemplateDefault(tempName) ? Utils.GetTemplatePath_Default(tempName) : Utils.GetTemplatePath(tempName);
            if (!Directory.Exists(tempPath))
            {
                tempName = "default";
            }
            Globals.sysHst[ApplicationKeys.ActiveTemplate + "_" + currentportalID] = tempName;
            //Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + currentportalID] = LoadActivePagePreset() || PresetHelper.LoadActivePagePreset(tempName, GetPageSEOName(Request.Url.ToString()));            
            LoadActivePagePreset();
            suc.SetPortalID(portalID);
            SetPortalID(portalID);
            #region "Set user credentials for modules"
            SecurityPolicy objSecurity = new SecurityPolicy();
            if (objSecurity.GetUser(GetPortalID) != string.Empty)
            {
                SettingProvider objSP = new SettingProvider();
                SageFrameConfig sfConfig = new SageFrameConfig();
                string strRoles = string.Empty;
                List<SageUserRole> sageUserRolles = objSP.RoleListGetByUsername(objSecurity.GetUser(GetPortalID), currentportalID);
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

        public PresetInfo LoadActivePagePreset()
        {
            PresetInfo presetInfo = new PresetInfo();
            if (Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + currentportalID] != null)
            {
                presetInfo = (PresetInfo)Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + currentportalID];
            }
            else
            {
                presetInfo = PresetHelper.LoadActivePagePreset(activeTemplate, GetPageSEOName(Request.Url.ToString()));
                Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + currentportalID] = presetInfo;
            }
            return presetInfo;
        }

        private void SetPageLoadPart()
        {
            ApplicationController objAppController = new ApplicationController();
            if (!objAppController.CheckRequestExtension(Request))
            {
                //CoreJs.IncludeLanguageCoreJs(this.Page);
                SessionTrackerController sTracController = new SessionTrackerController();
                sTracController.SetSessionTrackerValues(currentportalID.ToString(), GetUsername);

            }
        }


        private void BindModuleControls()
        {
            string preFix = string.Empty;
            string paneName = string.Empty;
            string ControlSrc = string.Empty;
            string phdContainer = string.Empty;
            string PageSEOName = string.Empty;
            SageUserControl suc = new SageUserControl();

            SageFrameConfig sfConfig = new SageFrameConfig();
            string portalDefaultPage = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
            if (PagePath != null)
            {
                suc.PagePath = PagePath;
            }
            else
            {
                suc.PagePath = portalDefaultPage;
            }
            if (PagePath != null)
            {
                PageSEOName = GetPageSEOName(PagePath);
            }
            else
            {
                PageSEOName = GetPageSEOName(portalDefaultPage);
            }
            PageSEOName = PageSEOName.Replace("-and-", "&").Replace(" ", "-");

            //:TODO: Need to get controlType and pageID from the selected page from routing path
            //string controlType = "0";
            //string pageID = "2";
            StringBuilder redirecPath = new StringBuilder();
            if (PageSEOName != string.Empty)
            {

                string SEOName = portalDefaultPage.Replace(" ", "-");
                List<UserModuleInfo> lstUserModules = new List<UserModuleInfo>();
                bool SuperRole = false;
                string previewCode = "none";
                bool isPreview = false;
                if (Request.QueryString["preview"] != null)
                {
                    previewCode = Request.QueryString["preview"].ToString();
                    isPreview = true;
                }

                if (Session[SessionKeys.SageRoles] != null && Session[SessionKeys.SageRoles].ToString() != string.Empty)
                {
                    string[] objRole = Session[SessionKeys.SageRoles].ToString().Split(',');
                    foreach (string role in objRole)
                    {
                        if (role.Replace(" ", string.Empty).ToLower().Equals(ApplicationKeys.Super_User.ToLower().Replace("-", string.Empty)))
                        {
                            SuperRole = true;
                        }
                    }
                }
                if (GetUsername.Equals(ApplicationKeys.anonymousUser))
                {
                    lstUserModules = sfConfig.GetPageModules_Anonymous("1", PageSEOName, GetUsername, GetCurrentCulture());
                }
                else if (SuperRole)
                {
                    lstUserModules = sfConfig.GetPageModules_Superuser("1", PageSEOName, GetUsername, GetCurrentCulture(), isPreview, previewCode);
                }
                else
                {
                    lstUserModules = sfConfig.GetPageModules("1", PageSEOName, GetUsername, GetCurrentCulture(), isPreview, previewCode);
                }
                Uri url = HttpContext.Current.Request.Url;
                if (lstUserModules[0].IsPageAvailable)
                {

                    if (lstUserModules[0].IsPageAccessible)
                    {
                        #region "Load Controls"

                        if (lstUserModules.Count > 0)
                        {
                            OverridePageInfo(lstUserModules[0]);
                            bool isUserLoggedIn = IsUserLoggedIn();
                            if (isUserLoggedIn)
                            {
                                SecurityPolicy objSecurity = new SecurityPolicy();
                                objSecurity.UpdateExpireTime(GetUsername, GetPortalID);
                            }
                            bool isHandheld = IsHandheld();
                            List<string> moduleDefIDList = new List<string>();
                            foreach (UserModuleInfo usermodule in lstUserModules)
                            {
                                bool handheld_status = bool.Parse(usermodule.IsHandHeld.ToString());
                                if (isHandheld == handheld_status)
                                {
                                    paneName = usermodule.PaneName;
                                    paneName = "pch_" + paneName;
                                    if (string.IsNullOrEmpty(paneName))
                                        paneName = "ContentPane";
                                    string UserModuleTitle = usermodule.UserModuleTitle != string.Empty ? usermodule.UserModuleTitle.ToString() : string.Empty;
                                    ControlSrc = usermodule.ControlSrc;
                                    string SupportsPartialRendering = usermodule.SupportsPartialRendering.ToString();
                                    string SuffixClass = usermodule.SuffixClass.ToString();
                                    string HeaderText = usermodule.ShowHeaderText ? usermodule.HeaderText : "";
                                    bool ContainsEdit = usermodule.IsEdit;
                                    int ControlCount = usermodule.ControlsCount;
                                    UserControl uc = pchWhole.FindControl("lytA") as UserControl;
                                    PlaceHolder phdPlaceHolder = uc.FindControl(paneName) as PlaceHolder;
                                    SuffixClass = isUserLoggedIn && ContainsEdit ? string.Format("sfLogged sfModule{0}", SuffixClass) : string.Format("sfModule{0}", SuffixClass);
                                    if (phdPlaceHolder != null)
                                    {
                                        string TemplateControls = Server.MapPath(string.Format("~/Templates/{0}/modules/{1}", activeTemplate, ControlSrc.Substring(ControlSrc.IndexOf('/'), ControlSrc.Length - ControlSrc.IndexOf('/'))));
                                        ControlSrc = File.Exists(TemplateControls) ? string.Format("/Templates/{0}/modules/{1}", activeTemplate, ControlSrc.Substring(ControlSrc.IndexOf('/'), ControlSrc.Length - ControlSrc.IndexOf('/'))) : string.Format("/{0}", ControlSrc);
                                        LoadControl(phdPlaceHolder, ControlSrc, paneName, usermodule.UserModuleID.ToString(), SuffixClass, HeaderText, isUserLoggedIn, GetModuleControls(usermodule.UserModuleID, ContainsEdit, ControlCount, usermodule.ModuleDefID), GetPaneNameContainer(UserModuleTitle), ContainsEdit);
                                        //changecss 1 take module list here take usermodulename
                                        moduleDefIDList.Add(usermodule.ModuleDefID.ToString());
                                    }
                                }
                            }
                            SetModuleDefList(moduleDefIDList);
                        }

                        #endregion
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

                            Response.Redirect(redirecPath.ToString());
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
                            redirecPath.Append(PortalAPI.PageNotAccessiblePageWithExtension);
                        }
                        else
                        {
                            redirecPath.Append(url.Scheme);
                            redirecPath.Append("://");
                            redirecPath.Append(url.Authority);
                            redirecPath.Append(PortalAPI.PageNotAccessibleURL);
                        }
                        Response.Redirect(redirecPath.ToString());
                    }
                }
                else
                {
                    //page is not found
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
                    Response.Redirect(redirecPath.ToString());
                }
            }
            SetScreenWidth(GetUsername);
        }

        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
        }

        private void LoadControl(PlaceHolder ContainerControl, string controlSource, int id)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ctl.ID = "lytA";
            ContainerControl.Controls.Add(ctl);
        }

        #endregion

        #region "Obsolete Methods"
        [Obsolete("Not used after SageFrame2.0")]
        private void Redirect()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Redirect(ResolveUrl("~/sf/sflogin" + Extension));
        }

        [Obsolete("Not used after SageFrame2.0")]
        public void LoadMessageControl()
        {
            UserControl uc = pchWhole.FindControl("lytA") as UserControl;
            PlaceHolder phdPlaceHolder = uc.FindControl("pch_message") as PlaceHolder;
            if (phdPlaceHolder != null)
            {
                LoadControl(phdPlaceHolder, "~/Controls/Message.ascx");
            }
        }
        #endregion

        #region SageFrameRoute Members

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
