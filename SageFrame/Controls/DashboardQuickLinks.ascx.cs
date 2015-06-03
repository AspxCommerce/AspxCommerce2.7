/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using SageFrame.Dashboard;
using System.Text;
using SageFrame.Templating;
using System.Collections.Generic;
using SageFrame.Utilities;
using SageFrame.Version;


public partial class Controls_DashboardQuickLinks : BaseAdministrationUserControl
{
    public string appPath = string.Empty, UserName = string.Empty, PortalName = string.Empty;
    public int PortalID = 0;
    public int IsSideBarVisible = 0;
    public string Extension;
    protected void Page_Init(object sedner, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        Extension = SageFrameSettingKeys.PageExtension;
        UserName = GetUsername;
        PortalID = GetPortalID;
        PortalName = GetPortalSEOName;
        SageFrameConfig sfConfig = new SageFrameConfig();
        bool ShowSideBar = sfConfig.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.ShowSideBar);
        IsSideBarVisible = ShowSideBar ? 1 : 0;
        BuildQuickLinks();
        //lblVersion.Text = string.Format("V {0}", Config.GetSetting("SageFrameVersion"));
        SageFrameVersion app = new SageFrameVersion();
        lblVersion.Text = string.Format("V {0}", app.FormatShortVersion(app.Version, true));
        
    }

    public void BuildQuickLinks()
    {
        List<QuickLink> lstQuickLinks = DashboardController.GetQuickLinks(GetUsername, GetPortalID);
        StringBuilder sb = new StringBuilder();
        sb.Append("<ul>");
        foreach (QuickLink item in lstQuickLinks)
        {
            string image = string.Format("{0}/Modules/Dashboard/Icons/{1}", appPath, item.ImagePath);
            string url = Utils.BuildURL(item.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
            sb.Append("<li><a href='" + url + "'><img src='" + image + "' width='24' height='24' alt='" + item.DisplayName + "' /><span>" + item.DisplayName + "</span></a></li>");
        }
        sb.Append("</ul>");
        ltrQuicklinks.Text = sb.ToString();
    }
}