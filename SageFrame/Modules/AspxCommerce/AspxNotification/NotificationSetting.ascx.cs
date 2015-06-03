using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxNotification_NotificationSetting : BaseAdministrationUserControl
{
    public string SessionCode = string.Empty;
    public string ModulePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("NotificationSetting", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("NotificationSetting", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxNotification/js/NotificationSettings.js");
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
            }
            IncludeLanguageJS();

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
        
    }
}
