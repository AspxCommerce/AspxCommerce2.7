using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxFedex_ctl_setting_usps : BaseAdministrationUserControl
{
    public string PathUsps = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        PathUsps = ResolveUrl(modulePath);
        IncludeLanguageJS();
        UserModuleID = SageUserModuleID;
    }

    public string UserModuleID { get; set; }
}
