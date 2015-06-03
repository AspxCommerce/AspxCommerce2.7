using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AspxCommerce_AspxAdminNotification_AspxAdminNotificationManagement : BaseAdministrationUserControl
{
    public string AspxAdminNotificationModulePath;
    public bool IsUseFriendlyUrls = true;
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            AspxAdminNotificationModulePath = ResolveUrl(modulePath);
            IncludeLanguageJS();

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeJs("AspxCommereCore", "/js/SageFrameCorejs/aspxcommercecore.js");
                IncludeCss("AspxAdminNotificationView",                   
                    "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css",
                    "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css",
                    "/Templates/" + TemplateName + "/css/PopUp/style.css"
                    );
                IncludeJs("AspxAdminNotificationView",
                    "/js/MessageBox/alertbox.js",
                    "/js/PopUp/popbox.js",
                    "/js/FormValidation/jquery.validate.js",
                    "/Modules/AspxCommerce/AspxAdminNotification/js/AspxAdminNotificationManagement.js"
                      );
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}