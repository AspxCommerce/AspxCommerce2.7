using System;
using AspxCommerce.Core;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using System.Web.Security;

public partial class Modules_AspxCommerce_AspxReturnAndPolicy_ReturnsSettings : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, templateName,path;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("ReturnsSettings",
                    "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css");
                IncludeJs("ReturnsSettings",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                            "/Modules/AspxCommerce/AspxReturnAndPolicy/js/ReturnsSettings.js",
                            "/Modules/AspxCommerce/AspxReturnAndPolicy/Language/AspxReturnAndPolicy.js",
                            
                            "/js/FormValidation/jquery.validate.js",
                            "/js/GridView/jquery.global.js",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js",
                            "/js/jquery.cookie.js", "/js/jquery.tipsy.js",
                            "/js/MessageBox/alertbox.js"
                           );

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                templateName = TemplateName;
                path = ResolveUrl("~");
                UserModuleID = SageUserModuleID;

                
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }


    public string UserModuleID { get; set; }
}
