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

public partial class Modules_AspxCommerce_AspxServiceItems_FrontServiceView : BaseAdministrationUserControl
{
    public string serviceModulePath;
    public string NoImageService;
    public string RssFeedUrl;
    public int count, rowTotal;
    public string isEnableService;
    public string serviceCategoryInARow;
    public string serviceCategoryCount;
    public string isEnableServiceRss;
    public string serviceRssCount;
    public string serviceDetailsPage,serviceRssPage;
    AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            serviceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            StoreSettingConfig ssc = new StoreSettingConfig();
            NoImageService = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                       aspxCommonObj.CultureName);           
          
            BindFrontServiceView();

        }

        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss("AspxFrontServices", "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css", "/Modules/AspxCommerce/AspxServiceItems/css/ServiceItems.css");
        IncludeJs("AspxFrontServices", "/Modules/AspxCommerce/AspxServiceItems/js/FrontServiceView.js", "/js/jquery.tipsy.js");
        IncludeLanguageJS();
    }

    Hashtable hst = null;
    public void BindFrontServiceView()
    {
        string serviceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
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
        count = int.Parse(serviceCategoryCount);             
        List<FrontServiceCategoryView> lstService = objService.GetFrontServices(aspxCommonObj, count);
        rowTotal = lstService.Count;
        if (rowTotal > count)
            lstService.RemoveAt(lstService.Count - 1);
        StringBuilder frontServiceView = new StringBuilder();
        if (lstService != null && lstService.Count > 0)
        {

            frontServiceView.Append("<div id=\"divFrontService\"><ul>");
            foreach (var serviceView in lstService)
            {
                string serviceDesc = "";
                int index = 0;
                string serviceDetails = "";
                if (serviceView.ServiceDetail != "" || serviceView.ServiceDetail != "null")
                {
                    serviceDesc = serviceView.ServiceDetail;
                    if (serviceDesc.IndexOf(' ') > 1)
                    {
                        if (serviceDesc.LastIndexOf(' ') > 60)
                        {
                            index = serviceDesc.Substring(0, 60).LastIndexOf(' ');
                            serviceDetails = serviceDesc.Substring(0, index);
                            serviceDetails = serviceDetails + "...";
                        }
                    }
                    else
                    {
                        serviceDetails = serviceDesc;
                    }
                }
                else
                {
                    serviceDetails = "";
                }
                string imagePath = "Modules/AspxCommerce/AspxCategoryManagement/uploads/" + serviceView.ServiceImagePath;
                if (serviceView.ServiceImagePath == "")
                {
                    imagePath = NoImageService;
                }
                else
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(serviceView.ServiceImagePath, ImageType.Medium, ImageCategoryType.Category, aspxCommonObj);
                }
                var hrefServices = aspxRedirectPath + "service/" + AspxUtility.fixedEncodeURIComponent(serviceView.ServiceName) + pageExtension;
                frontServiceView.Append("<li><a href=\"");
                frontServiceView.Append(hrefServices);
                frontServiceView.Append("\"><img title=\"");
                frontServiceView.Append(serviceView.ServiceName);
                frontServiceView.Append("\" alt=\"");
                frontServiceView.Append(serviceView.ServiceName);
                frontServiceView.Append("\" src=\"");
                frontServiceView.Append(aspxRootPath);
                frontServiceView.Append(imagePath.Replace("uploads", "uploads/Medium"));
                frontServiceView.Append("\"/></a><a href=\"");
                frontServiceView.Append(hrefServices);
                frontServiceView.Append("\"><span class=\"cssClassImgWrapper\">");
                frontServiceView.Append(serviceView.ServiceName);
                frontServiceView.Append("</span></a><p>");
                frontServiceView.Append(serviceDetails);
                frontServiceView.Append("</p></li>");
            }
            frontServiceView.Append("</ul></div>");
            if (rowTotal > count)
            {
                frontServiceView.Append("<div id=\"divViewMoreServices\" class=\"cssViewMore\">");
                frontServiceView.Append("<a href=\"" + aspxRootPath + serviceDetailsPage + pageExtension + "\">" +
                                        getLocale("View More") + "</a>");
                frontServiceView.Append("</div>");
            }
        }
        else
        {
            frontServiceView.Append("<div id=\"divFrontService\"><span class=\"cssClassNotFound\">" +
                                    getLocale("There are no services available!") + "</span></div>");
        }
        ltrForntServiceView.Text = frontServiceView.ToString();
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