using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxGiftCardManagement_GiftCard : BaseAdministrationUserControl
{
    public string UserModuleID;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            UserModuleID = SageUserModuleID;
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
                IncludeCss("giftCardcss", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("giftcardmgt", "/js/FormValidation/jquery.validate.js", "/js/GridView/jquery.grid.js",
                            "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js","/js/DateTime/date.js",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js", "/js/MessageBox/alertbox.js",
                            "/Modules/AspxCommerce/AspxGiftCardManagement/js/giftCard.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/CurrencyFormat/jquery.formatCurrency.all.js");
              
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
