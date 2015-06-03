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

public partial class Modules_Admin_ModuleMessage_ModuleMessage : BaseAdministrationUserControl
{
    public string appPath = "";
    public int PortalID, userModuleId;
    public string UserName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        userModuleId = int.Parse(SageUserModuleID);
        ArrayList jsArrColl = new ArrayList();
        jsArrColl.Add(appPath + "/editors/ckeditor/ckeditor.js");
        jsArrColl.Add(appPath + "/editors/ckeditor/adapters/jquery.js");
        PortalID = GetPortalID;
        UserName = GetUsername;
        IncludeScriptFile(jsArrColl);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + userModuleId + "';", true);
    }
}
