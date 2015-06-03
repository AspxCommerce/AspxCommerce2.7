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

public partial class Modules_Logo_LogoSetting :BaseAdministrationUserControl
{
    public int moduleID, portalID;
    public string culture, userName, resolvedUrl, currentDirectory;
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeJs("LogoSeting", "/Modules/Logo/js/setting.js");
        IncludeJs("LogoUploader", "/Modules/Logo/js/ajaxupload.js");
        IncludeCss("Logo", "/Modules/Logo/css/module.css");
        moduleID = UserModuleID;
        portalID = GetPortalID;
        resolvedUrl = ResolveUrl("~/");
        culture = GetCurrentCulture();
        currentDirectory = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
    }
    public int UserModuleID
    {
        get
        {
            if (!string.IsNullOrEmpty(SageUserModuleID))
            {
                moduleID = Int32.Parse(SageUserModuleID);
            }
            return moduleID;
        }
    }
}
