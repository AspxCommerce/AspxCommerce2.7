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
using AjaxControlToolkit;
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

#endregion

namespace SageFrame
{
    public partial class Sagin_Admin : PageBase, SageFrameRoute
    {
        #region "Public Variables"

        public string ControlPath = string.Empty, SageFrameAppPath, SageFrameUserName;
        public string appPath = string.Empty;
        public string Extension;
        public string templateFavicon = string.Empty;

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
            // SagePageLoadPart();
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

        private void SetGlobalVariable()
        {
            appPath = Request.ApplicationPath;
            SageFrameAppPath = appPath;
            imgLogo.ImageUrl = ResolveUrl("~/") + "images/aspxcommerce.png";
            lnkLoginCss.Href = ResolveUrl("~/") + "Administrator/Templates/Default/css/login.css";
            RegisterSageGlobalVariable();
        }

        private void SetPageInitPart()
        {
            ltrJQueryLibrary.Text = GetJqueryLibraryPath();
            templateFavicon = SetFavIcon(GetActiveTemplate);
            Extension = SageFrameSettingKeys.PageExtension;
            ApplicationController objAppController = new ApplicationController();
            if (!objAppController.CheckRequestExtension(Request))
            {
                SageInitPart();
            }
            SetGlobalVariable();
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
                    LoadMessageControl();
                    BindModuleControls();
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
            }
        }

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
                    PortalSEOName = GetDefaultPortalName(hstPortals, 1);// 1 is default parent PortalID 
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
                StringBuilder sb = new StringBuilder();
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
                    sageNavigateUrl = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + Extension;
                }
                else
                {
                    sageNavigateUrl = GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + Extension;
                }
                hypPreview.NavigateUrl = sageNavigateUrl;
                Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetAdminImageUrl("ajax-loader.gif", true);
                }
            }
            ////SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
            //if (string.IsNullOrEmpty(sessionTracker.PortalID))
            //{
            //    //sessionTracker.PortalID = GetPortalID.ToString();
            //    //sessionTracker.Username = GetUsername;
            //    SageFrameConfig sfConfig = new SageFrameConfig();
            //    //sessionTracker.InsertSessionTrackerPages = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.InsertSessionTrackingPages);
            //    SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
            //    SLog.SessionTrackerUpdateUsername(sessionTracker, GetUsername, GetPortalID.ToString());
            //    //Session[SessionKeys.Tracker] = sessionTracker;
            //}
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
                string PageUrl = Request.RawUrl;
                PageName = Path.GetFileNameWithoutExtension(PageUrl);
            }
            else
            {
                PageName = PagePath;
            }
            suc.PagePath = PageName;
            if (Request.QueryString["pgnm"] != null)
            {
                PageSEOName = Request.QueryString["pgnm"].ToString();
            }
            else
            {
                PageSEOName = GetPageSEOName(PageName);
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
                if (bool.Parse(dsPageSettings.Tables[0].Rows[0][0].ToString()) == true)
                {
                    #region "Control Load Part"

                    if (bool.Parse(dsPageSettings.Tables[0].Rows[0][1].ToString()) == true)
                    {
                        // Get ModuleControls data table
                        DataTable dtPages = dsPageSettings.Tables[1];
                        if (dtPages != null && dtPages.Rows.Count > 0)
                        {
                            OverridePageInfo(dtPages);
                        }
                        List<string> moduleDefIDList = new List<string>();
                        // Get ModuleDefinitions data table
                        DataTable dtPageModule = dsPageSettings.Tables[2];
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
                                if (paneName.Equals("navigation"))
                                {
                                    divNavigation.Attributes.Add("style", "display:block");
                                }
                                if (phdPlaceHolder != null)
                                {
                                    //bool status = LoadModuleInfo(phdPlaceHolder, int.Parse(UserModuleID), 0);
                                    LoadControl(phdPlaceHolder, ControlSrc, paneName, UserModuleID, "", "", false, new HtmlGenericControl("div"), new HtmlGenericControl("span"), false);
                                    //if (!status)
                                    //{
                                    //    LoadModuleInfo(phdPlaceHolder, int.Parse(UserModuleID), 1);
                                    //}
                                    moduleDefIDList.Add(dtPageModule.Rows[i]["ModuleDefID"].ToString());
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
        }

        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
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
                        string totalStyle = string.Format("{0} {1} sfModuletext", modeStyle, messageTypeStyle);
                        HtmlGenericControl moduleDiv = new HtmlGenericControl("div");
                        StringBuilder sb = new StringBuilder();
                        string CloseForEver = string.Format("close_{0}", UserModuleID);
                        sb.Append("<div class='sfModuleInfo'><div class='" + totalStyle + "'><div class='sfLinks'><a class='sfClose' href='#'>Close</a> || <a class='sfNclose' id='" + CloseForEver + "' href='#'>Close and Never Show Again</a></div>");
                        sb.Append(objMessage.Message);
                        sb.Append("</div></div>");
                        moduleDiv.InnerHtml = sb.ToString();
                        Container.Controls.Add(moduleDiv);
                        status = true;
                    }
                }
            }
            return status;
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
