using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.ServiceItem;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxServiceItems_BookAppointment : BaseAdministrationUserControl
{
    public int StoreID, PortalID,CustomerID;
    public string UserName, CultureName;
    public string serviceModulePath, appointmentSuccessPage;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                CustomerID = GetCustomerID;
                serviceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                IncludeCss("BookAppointment", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Modules/AspxCommerce/AspxServiceItems/css/ServiceItems.css");
                IncludeJs("BookAppointment", "/Modules/AspxCommerce/AspxServiceItems/js/BookAppointment.js",
                          "/js/FormValidation/jquery.validate.js",
                          "/js/GridView/jquery.dateFormat.js",
                          "/js/MessageBox/alertbox.js","/js/encoder.js");
            }
            IncludeLanguageJS();
            BindServiceSetting();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void BindServiceSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        ServiceItemController objService = new ServiceItemController();
        List<ServiceItemSettingInfo> lstServiceSetting = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstServiceSetting != null && lstServiceSetting.Count > 0)
        {
            foreach (var serviceSetting in lstServiceSetting)
            {                
                appointmentSuccessPage = serviceSetting.AppointmentSuccessPage;
            }
        }
    }
}
