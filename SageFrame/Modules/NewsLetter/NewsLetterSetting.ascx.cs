using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_NewsLetter_NewsLetterSetting : BaseAdministrationUserControl
{
    public string UserName = "", ModulePath = "";
    public int UserModuleID = 0, PortalID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserName = GetUsername;
        UserModuleID = Int32.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        IncludeJs("newsjs", "/Modules/NewsLetter/js/NewsLetterSetting.js", "/Modules/NewsLetter/js/jquery.validate.js");
    }
}