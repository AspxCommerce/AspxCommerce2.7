using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame;


public partial class Modules_Admin_CacheMaintenance_CacheMaintenance : BaseAdministrationUserControl
{
    public int PortalID;
    public int ModuleID;
    public string UserName;
    protected void Page_Load(object sender, EventArgs e)
    {
        PortalID = GetPortalID;
        ModuleID = int.Parse(SageUserModuleID);
        UserName = GetUsername;
        IncludeJs("CacheMaintenance", "/Modules/Admin/CacheMaintenance/JS/CacheMaintenance.js");
    }
   
}
