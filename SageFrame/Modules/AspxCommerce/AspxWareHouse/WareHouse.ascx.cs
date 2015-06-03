using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxWareHouse_WareHouse : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterClientScriptInclude("JTablesorter",
                                                          ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
            IncludeCss("Warehousecss", "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                      
                       "/Templates/" + TemplateName + "/css/MessageBox/style.css");
            IncludeJs("Warehouse", "/js/FormValidation/jquery.validate.js",
                      "/js/GridView/jquery.grid.js",
                      "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                      "/js/DateTime/date.js",
                      "/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js", "/js/MessageBox/alertbox.js",
                      "/Modules/AspxCommerce/AspxWareHouse/js/wareHouse.js");
            IncludeLanguageJS();


        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }
    }
}
