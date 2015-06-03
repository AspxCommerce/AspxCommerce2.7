using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxNotification_NotificationView : BaseAdministrationUserControl
{
    public string ModulePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("NotificationView", "/Modules/AspxCommerce/AspxNotification/css/Notification.css");
                IncludeJs("NotificationView", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxNotification/js/NotificationView.js");
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }


    }
}
