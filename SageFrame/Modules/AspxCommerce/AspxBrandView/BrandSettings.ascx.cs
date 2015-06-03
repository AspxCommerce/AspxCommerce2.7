using System;
using System.Collections.Generic;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.BrandView;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxBrandView_BrandSettings :BaseAdministrationUserControl
{
    public string BrandModulePath;
    public int StoreID, PortalID;
    public string CultureName = string.Empty;
    public string Settings = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                BrandModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                IncludeJs("BrandSetting", "/js/FormValidation/jquery.validate.js");
                IncludeLanguageJS();
                GetBrandSetting();                
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
    public void GetBrandSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        AspxBrandViewController objBrand = new AspxBrandViewController();
        BrandSettingInfo lstBrandSetting = objBrand.GetBrandSetting(aspxCommonObj);
        if (lstBrandSetting != null)
        {
            object obj = new
            {
                BrandCount = lstBrandSetting.BrandCount,
                BrandAllPage = lstBrandSetting.BrandAllPage,
                EnableBrandRss = lstBrandSetting.IsEnableBrandRss,
                BrandRssCount = lstBrandSetting.BrandRssCount,
                BrandRssPage = lstBrandSetting.BrandRssPage,
                BrandModulePath = BrandModulePath
            };
            Settings = json_serializer.Serialize(obj);
        }
    }
}
