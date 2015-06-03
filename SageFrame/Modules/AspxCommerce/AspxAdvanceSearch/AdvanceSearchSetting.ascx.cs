using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdvanceSearch_AdvanceSearchSetting : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName, AdvanceSearchModulePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;

                AdvanceSearchModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                IncludeJs("AdvanceSearchSetting", "/js/jquery-1.9.1.js", "/Modules/AspxCommerce/AspxAdvanceSearch/js/AdvanceSearchSetting.js");
                IncludeLanguageJS();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}