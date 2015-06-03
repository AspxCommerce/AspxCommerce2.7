using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxCommerce_RewardPoints_RewardPointsHistory : BaseAdministrationUserControl
{
    public string baseUrl;
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string SessionCode = string.Empty;
    public bool IsUseFriendlyUrls = true;
    public string AspxRewardPointsModulePath;
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);          
            AspxRewardPointsModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            IncludeLanguageJS();
            InitializeJS();
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
                IncludeCss("RewardPointsHistory", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css","/Templates/" + TemplateName + "/css/PopUp/style.css",
                "/Modules/AspxCommerce/AspxRewardPoints/css/RewardPoints.css");
                IncludeJs("RewardPointsHistory", "/js/FormValidation/jquery.validate.js",
                      "/js/GridView/jquery.grid.js","/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", 
                      "/js/GridView/jquery.dateFormat.js","/js/DateTime/date.js","/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js",
                      "/js/jquery.cookie.js", "/js/jquery.tipsy.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                            "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                      "/js/MessageBox/alertbox.js", "/js/DateTime/date.js", 
                      "/Modules/AspxCommerce/AspxRewardPoints/js/RewardPointsHistory.js"
                    
                      );
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;              
                
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }
    
}
