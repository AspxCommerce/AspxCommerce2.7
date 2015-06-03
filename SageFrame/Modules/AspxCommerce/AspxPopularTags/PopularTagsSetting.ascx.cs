using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxPopularTags_PopularTagsSetting :BaseAdministrationUserControl
{
    public string PopularTagModulePath;
    public int StoreID;
    public int CustomerID;
    public int PortalID;
    public string CultureName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PopularTagModulePath = ResolveUrl((this.AppRelativeTemplateSourceDirectory));

            IncludeJs("PopularTags", "/Modules/AspxCommerce/AspxPopularTags/js/PopularTagsSetting.js");
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CustomerID = GetCustomerID;
            CultureName = GetCurrentCultureName;

            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}