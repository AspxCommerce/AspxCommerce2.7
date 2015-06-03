using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using SageFrame.Framework;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using System.Web.Security;

public partial class Modules_AspxCommerce_AspxOutOfStockNotification_OutOfStockNotificationManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, userEmail;
    protected void page_init(object sender, EventArgs e)
    {
   
        try
        {
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IncludeCss("OutOfStockNotification", "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                       "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Modules/AspxCommerce/AspxOutOfStockNotification/css/module.css");
            IncludeJs("OutOfStockNotification", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                      "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                      "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxOutOfStockNotification/js/stockNotification.js");


            StoreID = GetStoreID;
            PortalID = GetPortalID;
            UserName = GetUsername;
            UserModuleID = SageUserModuleID;
            CultureName = GetCurrentCultureName;
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
            if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
            {
                MembershipController member = new MembershipController();
                UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
                userEmail = userDetail.Email;
            }
        }
        IncludeLanguageJS();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath",
                                                " var aspxservicePath='" + ResolveUrl("~/") +
                                                "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
        
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

    public string UserModuleID { get; set; }
}
