#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.ModuleControls;
using SageFrame;
using System.Collections.Generic;
using SageFrame.Shared;
using SageFrame.Security;
using SageFrame.Common;
using SageFrame.Services;

#endregion

public partial class Sagin_HandleModuleControls : PageBase, SageFrameRoute
{
    #region "Public Variables"

    public int portalid = 0, tabcount = 0;
    int moduledefID = 0;

    #endregion

    #region "Event handlers"

    protected void Page_Init(object sender, EventArgs e)
    {
        IncludeStartup(GetPortalID, pchHolder, false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageLoadPart();
    }

    #endregion

    #region "Public Methods"

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

    private void SetPageLoadPart()
    {
        #region "Unused Code"

        //string redirectPathLogin = "", redirectPathNoAccess = "";
        //SageFrameConfig sfConfig = new SageFrameConfig();
        //if (GetPortalID > 1)
        //{
        //    redirectPathLogin =
        //        ResolveUrl("~/portal/" + GetPortalSEOName + "/sf/" +
        //                   sfConfig.GetSettingsByKey(
        //                       SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension);
        //    redirectPathNoAccess =
        //        ResolveUrl("~/portal/" + GetPortalSEOName + "/sf/" +
        //                   sfConfig.GetSettingsByKey(
        //                       SageFrameSettingKeys.PortalPageNotAccessible) + SageFrameSettingKeys.PageExtension);

        //}
        //else
        //{
        //    redirectPathLogin = ResolveUrl("~/sf/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension);
        //    redirectPathNoAccess = ResolveUrl("~/sf/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible) + SageFrameSettingKeys.PageExtension);
        //} 
        ltrJQueryLibrary.Text = GetAdminJqueryLibraryPath();
        #endregion

        int UserModuleID = 0;
        if (Request.QueryString["uid"] != null)
        {
            UserModuleID = int.Parse(Request.QueryString["uid"].ToString());
        }
        if (Request.QueryString["pid"] != null)
        {
            portalid = int.Parse(Request.QueryString["pid"].ToString());
        }
        if (Request.QueryString["mdefID"] != null)
        {
            moduledefID = int.Parse(Request.QueryString["mdefID"].ToString());
        }
        AuthenticateService objAuthenticate = new AuthenticateService();
        if (objAuthenticate.IsPostAuthenticated(portalid, UserModuleID, GetUsername, SageFrameSecureToken))
        {
            LoadModuleControls(UserModuleID);
            SetPortalCofig();
            LoadMessageControl();
            SetGlobalVariable();
        }
        else
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string redirectPathNoAccess = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalPageNotAccessible) + SageFrameSettingKeys.PageExtension;
            Response.Redirect(redirectPathNoAccess);
        }

    }

    private void SetGlobalVariable()
    {

        string appPath = GetAppPath();
        RegisterSageGlobalVariable();
    }

    private void LoadControl(PlaceHolder ContainerControl, string controlSource)
    {
        UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
        ctl.EnableViewState = true;
        ContainerControl.Controls.Add(ctl);
    }

    private void LoadModuleControls(int UserModuleID)
    {
        int MID = ModuleControlDataProvider.GetModuleID(UserModuleID);
        string MName = ModuleControlDataProvider.GetModuleName(UserModuleID);
        List<ModuleControlInfo> lstModCtls = ModuleControlDataProvider.GetControlType(MID);
        tabcount = lstModCtls.Count;
        ShowHideTabs(lstModCtls);
        string appPath = GetAppPath() + "/";
        foreach (ModuleControlInfo obj in lstModCtls)
        {
            switch (obj.ControlType)
            {
                case "2":
                    LoadControl(pchEdit, appPath + obj.ControlSrc, UserModuleID.ToString());
                    break;
                case "3":
                    LoadControl(lstModCtls.Count > 2 ? pchSetting : pchEdit, appPath + obj.ControlSrc, UserModuleID.ToString());
                    break;
            }
        }
        List<string> moduleDefIDList = new List<string>();
        HttpContext.Current.Session[SessionKeys.ModuleList] = moduledefID.ToString();
    }

    private void ShowHideTabs(List<ModuleControlInfo> lstModCtls)
    {
        bool ShowEdit = lstModCtls.Exists(
                delegate(ModuleControlInfo obj)
                {
                    return (obj.ControlType == "2");
                }
            );
        bool ShowSettings = lstModCtls.Exists(
                delegate(ModuleControlInfo obj)
                {
                    return (obj.ControlType == "3");
                }
            );
        TabContainerManagePages.Visible = true;
        TabPanelEdit.Visible = true;
        TabPanelSettings.Visible = tabcount > 2 ? ShowSettings : false;

        if (tabcount == 2 && ShowSettings)
        {
            TabPanelEdit.HeaderText = "Settings";
        }
    }

    private void LoadControl(PlaceHolder ContainerControl, string controlSource, string UserModuleID)
    {
        SageUserControl ctl = this.Page.LoadControl(controlSource) as SageUserControl;
        ctl.EnableViewState = true;
        ctl.SageUserModuleID = UserModuleID;
        ContainerControl.Controls.Add(ctl);
    }

    private void SetPortalCofig()
    {
        Session[SessionKeys.SageFrame_PortalID] = portalid;
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


