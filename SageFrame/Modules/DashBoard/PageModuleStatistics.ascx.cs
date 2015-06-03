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
using System.Collections.Generic;
using SageFrame.Pages;
using SageFrame.ModuleManager;
using SageFrame.Dashboard;
using SageFrame.Security.Entities;
using System.Text.RegularExpressions;
using SageFrame.Security;
#endregion 

public partial class Modules_DashBoard_PageModuleStatistics : BaseAdministrationUserControl
{
    public int PortalID = 0;
    public string UserName = "";
    public string appPath = "";
    public string PortalName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        RegScript();
        GetVariable();
    }

    public void RegScript()
    {
        Page.ClientScript.RegisterClientScriptInclude("tblPaging", ResolveUrl("~/Modules/DashBoard/js/quickpager.jquery.js"));
    }

    public void GetVariable()
    {
        PortalID = GetPortalID;
        PortalName = GetPortalSEOName;
        UserName = GetUsername;
        appPath = GetApplicationName;
    }
}
