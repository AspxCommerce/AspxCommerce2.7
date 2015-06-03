using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_NewsLetter_NLUnSubscribe :BaseAdministrationUserControl
{
    public string ModulePath = "";
    public int UserModuleID = 0;
    public int PortalID = 0;
    public string UserName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserName = GetUsername;
        UserModuleID = Int32.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        IncludeCss("newsletter", "/Modules/NewsLetter/css/module.css");
        IncludeJs("newsjs", "/Modules/NewsLetter/js/unSubscribe.js", "/Modules/NewsLetter/js/jquery.validate.js");

    }
}