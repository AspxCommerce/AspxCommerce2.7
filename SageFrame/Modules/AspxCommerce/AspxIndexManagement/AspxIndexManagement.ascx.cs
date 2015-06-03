using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxIndexManagement_AspxIndexManagement : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserModuleID = SageUserModuleID;
        if (!Page.IsPostBack)
        {
            IncludeCss("InvoiceManagement", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                "/Modules/AspxCommerce/AspxIndexManagement/css/module.css");
            
            IncludeJs("IndexManagement", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.tablesorter.js",
                                "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxIndexManagement/js/AspxIndexManagement.js");    
        }
        IncludeLanguageJS();
    }

    public string UserModuleID;
}