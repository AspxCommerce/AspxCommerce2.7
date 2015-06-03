#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
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
#endregion

public partial class Modules_DashBoard_DashboardManager : BaseAdministrationUserControl
{
    public string appPath = "";
    public string Theme = "", UserName = "", PortalName = "";
    public int PortalID = 0, userModuleId = 0;
    public string PageExtension = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        PortalID = GetPortalID;
        userModuleId = int.Parse(SageUserModuleID);
        Page.ClientScript.RegisterClientScriptInclude("J9Conteeeext", ResolveUrl("~/js/jquery.validate.js"));
        Theme = GetActiveAdminTheme;
        UserName = GetUsername;
        PortalName = GetPortalSEOName;
        IncludeJs("Dashboard", "/Administrator/Templates/Default/js/ajaxupload.js");
        IncludeCss("Dashboard", "/Modules/Dashboard/css/module.css");
        IncludeCss("TaskToDo", "/css/jquery.alerts.css");
        IncludeJs("TaskToDo", "/js/jquery.alerts.js");
        PageExtension = SageFrameSettingKeys.PageExtension;
    }
}