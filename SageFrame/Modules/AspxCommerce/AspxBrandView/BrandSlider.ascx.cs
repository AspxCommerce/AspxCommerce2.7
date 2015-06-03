using System;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using AspxCommerce.BrandView;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class Modules_AspxCommerce_AspxBrandView_BrandSlider : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID;
    public string UserName;
    public string CultureName;
    public string BrandModulePath;
    public int BrandCount, BrandRssCount;
    public bool EnableBrandRss;
    public string  BrandAllPage, BrandRssPage;
    private List<BrandViewInfo> lstBrand = new List<BrandViewInfo>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string templateName = TemplateName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                IncludeJs("BrandSlider", "/Modules/AspxCommerce/AspxBrandView/js/BrandSlide.js", "/js/Sliderjs/jquery.bxSlider.js");
                IncludeCss("BrandSlider", "/Templates/" + templateName + "/css/Slider/style.css", "/Templates/" + templateName + "/css/ToolTip/tooltip.css",
                    "/Modules/AspxCommerce/AspxBrandView/css/module.css");                  
                BrandModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);               
            }
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            GetBrandSetting(aspxCommonObj);
            GetAllBrandForSlider(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void GetBrandSetting(AspxCommonInfo aspxCommonObj)
    {
        AspxBrandViewController objBrand = new AspxBrandViewController();
        DataSet brandList = objBrand.GetBrandSettingAndSlider(aspxCommonObj);

        DataTable settings = brandList.Tables[0];
        List<BrandSettingInfo> lstBrandSetting = AspxCommerce.Core.CommonHelper.ConvertTo<BrandSettingInfo>(settings);
        BrandSettingInfo brandSetting = lstBrandSetting[0];
        if (brandSetting != null)
        {
            BrandCount = brandSetting.BrandCount;
            BrandAllPage = brandSetting.BrandAllPage;
            EnableBrandRss = brandSetting.IsEnableBrandRss;
            BrandRssCount = brandSetting.BrandRssCount;
            BrandRssPage = brandSetting.BrandRssPage;
        }

        DataTable list = brandList.Tables[1];
        lstBrand = AspxCommerce.Core.CommonHelper.ConvertTo<BrandViewInfo>(list);
    }

    Hashtable hst = null;
    public void GetAllBrandForSlider(AspxCommonInfo aspxCommonObj)
    {
        string aspxRootPath = ResolveUrl("~/");       
        hst = AppLocalized.getLocale(BrandModulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder element = new StringBuilder();
        if (lstBrand != null && lstBrand.Count > 0)
        {
            element.Append("<ul id=\"brandSlider\">");
            foreach (BrandViewInfo value in lstBrand)
            {
                
                
                var imagepath = aspxRootPath + value.BrandImageUrl;
                string []imageFile=value.BrandImageUrl.ToString().Split('/');
                if (value.BrandImageUrl != "")
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(imageFile[imageFile.Length-1], ImageType.Small, ImageCategoryType.Brand, aspxCommonObj);
                }
                element.Append("<li><a href=\"");
                element.Append(aspxRedirectPath);
                element.Append("brand/");
                element.Append(AspxUtility.fixedEncodeURIComponent(value.BrandName));
                element.Append(pageExtension);
                element.Append("\"><img brandId=\"");
                element.Append(value.BrandID);
                element.Append("\" src=\"");
                element.Append(imagepath.Replace("uploads", "uploads/Small"));
                element.Append("\" alt=\"");
                element.Append(value.BrandName);
                element.Append("\" title=\"");

                element.Append(value.BrandName);
                element.Append("\"  /></a></li>");
            }
            element.Append("</ul>");
            element.Append("<span class=\"cssClassViewMore\"><a href=\"");
            element.Append(aspxRedirectPath);
            element.Append(BrandAllPage);
            element.Append(pageExtension);
            element.Append("\">");
            element.Append(getLocale("View All Brands"));
            element.Append("</a></span>");
        }

        else
        {
            element.Append("<span class='cssClassNotFound'>");
            element.Append(getLocale("The store has no brand!"));
            element.Append("</span>");
        }
        litSlide.Text = element.ToString();
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
