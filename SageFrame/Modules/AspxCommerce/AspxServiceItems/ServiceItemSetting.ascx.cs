using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.ServiceItem;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxServiceItems_ServiceItemSetting : BaseAdministrationUserControl
{
    public string  ServiceModulePath;
    public string Settings = string.Empty;
    public int StoreID, PortalID;
    public string CultureName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                ServiceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                IncludeJs("ServiceSetting", "/js/FormValidation/jquery.validate.js");
                GetServiceItemSetting();
                IncludeLanguageJS();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void GetServiceItemSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        ServiceItemController objService = new ServiceItemController();
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        List<ServiceItemSettingInfo> lstServiceSetting = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstServiceSetting != null && lstServiceSetting.Count > 0)
        {
            foreach (var serviceSetting in lstServiceSetting)
            {
                object obj = new
                {
                    IsEnableService = serviceSetting.IsEnableService,
                    ServiceCategoryInARow = serviceSetting.ServiceCategoryInARow,
                    ServiceCategoryCount = serviceSetting.ServiceCategoryCount,
                    IsEnableServiceRss = serviceSetting.IsEnableServiceRss,
                    ServiceRssCount = serviceSetting.ServiceRssCount,
                    ServiceRssPage = serviceSetting.ServiceRssPage,
                    ServiceDetailsPage = serviceSetting.ServiceDetailsPage,
                    ServiceItemDetailPage = serviceSetting.ServiceItemDetailsPage,
                    BookAnAppointmentPage = serviceSetting.BookAnAppointmentPage,
                    AppointmentSuccessPage = serviceSetting.AppointmentSuccessPage
                };
                Settings = json_serializer.Serialize(obj);
            }
        }
    }
}