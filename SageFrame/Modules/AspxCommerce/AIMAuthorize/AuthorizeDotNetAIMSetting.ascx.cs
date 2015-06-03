using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;

public partial class Modules_PaymentGatewayManagement_AuthorizeDotNetAIMSetting : BaseAdministrationUserControl
{
    public string AspxPaymentModulePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            IncludeLanguageJS();
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            AspxPaymentModulePath = ResolveUrl(modulePath);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
