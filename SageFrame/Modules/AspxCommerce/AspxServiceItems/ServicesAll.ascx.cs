using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using AspxCommerce.ServiceItem;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxServiceItems_ServicesAll : BaseAdministrationUserControl
{
    public string serviceModulePath;
    public string NoImageService;
    public string ServiceTypeItemRss, RssFeedUrl;
    public string isEnableService;
    public string serviceCategoryInARow;
    public string serviceCategoryCount;
    public string isEnableServiceRss;
    public string serviceRssCount;
    public string serviceDetailsPage, serviceRssPage;
    private AspxCommonInfo aspxCommonObj = new AspxCommonInfo();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                serviceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.UserName = GetUsername;
                aspxCommonObj.CultureName = GetCurrentCultureName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageService = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID,
                                                           aspxCommonObj.PortalID, aspxCommonObj.CultureName);
                ServiceTypeItemRss = "true";
                                                                                           
                IncludeJs("Service", "/Modules/AspxCommerce/AspxServiceItems/js/Services.js");
                IncludeCss("ServiceCss", "/Modules/AspxCommerce/AspxServiceItems/css/ServiceItems.css");
            }
            BindAllServices();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private Hashtable hst = null;

    private void BindAllServices()
    {
        string serviceModulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(serviceModulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        ServiceItemController objService = new ServiceItemController();
        List<ServiceItemSettingInfo> lstServiceSetting = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstServiceSetting != null && lstServiceSetting.Count > 0)
        {
            foreach (var serviceSetting in lstServiceSetting)
            {
                isEnableService = serviceSetting.IsEnableService.ToString();
                serviceCategoryInARow = serviceSetting.ServiceCategoryInARow.ToString();
                serviceCategoryCount = serviceSetting.ServiceCategoryCount.ToString();
                isEnableServiceRss = serviceSetting.IsEnableServiceRss.ToString();
                serviceRssCount = serviceSetting.ServiceRssCount.ToString();
                serviceDetailsPage = serviceSetting.ServiceDetailsPage;
                serviceRssPage = serviceSetting.ServiceRssPage;   
            }
        }       
        List<ServiceCategoryInfo> lstAllService = objService.GetAllServices(aspxCommonObj);
        StringBuilder allServiceViewBld = new StringBuilder();

        allServiceViewBld.Append("<div id=\"divBindAllServices\" class=\"cssClassAllService\">");
        if (lstAllService != null && lstAllService.Count > 0)
        {
            foreach (var allserviceInfo in lstAllService)
            {
                string serviceName = allserviceInfo.ServiceName;
                string imagePath = "Modules/AspxCommerce/AspxCategoryManagement/uploads/" + allserviceInfo.ServiceImagePath;
                if (allserviceInfo.ServiceImagePath == "")
                {
                    imagePath = NoImageService;
                }
                else
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(allserviceInfo.ServiceImagePath, ImageType.Medium, ImageCategoryType.Category, aspxCommonObj);
                }
                var hrefServices = aspxRedirectPath + serviceDetailsPage + "/" + AspxUtility.fixedEncodeURIComponent(serviceName) + pageExtension;
                allServiceViewBld.Append("<li><h3><a href=\"" + hrefServices + "\">");
                allServiceViewBld.Append("<div class=\"cssClassImgWrapper\">");
                allServiceViewBld.Append("<img src=\"" + aspxRootPath +
                                       imagePath.Replace("uploads", "uploads/Medium") +
                                         "\"/>");
                allServiceViewBld.Append("</div>" + serviceName + "</a></h3></li>");
            }
        }
        else
        {
            allServiceViewBld.Append("<span class=\"cssClassNotFound\">" + getLocale("There are no services available!") +
                                     "</span>");
        }
        allServiceViewBld.Append("</div>");
        ltrBindAllServices.Text = allServiceViewBld.ToString();
    }

    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
}