using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
using AspxCommerce.Core;

public partial class Modules_AspxPaymentGateway_AuthorizeDotNET : BaseAdministrationUserControl
{
    public string AIMPath;
    public string AddressPath, MainCurrency;
    bool _isUseFriendlyUrls = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                var sfConfig = new SageFrameConfig();
                _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);

                if (_isUseFriendlyUrls)
                {
                    if (!IsParent)
                    {
                        AddressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";

                    }
                    else
                    {
                        AddressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                    }
                }
                StoreSettingConfig ssc = new StoreSettingConfig();               
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetPortalID, GetPortalID, GetCurrentCultureName);  

            }
            IncludeLanguageJS();
        }
        catch
        {
        }    
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        AIMPath = ResolveUrl(modulePath);
    }    
}
