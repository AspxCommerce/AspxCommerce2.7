using System;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.BrandView;
using System.Collections.Generic;

public partial class Modules_AspxCommerce_AspxBrandView_BrandView : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID;
    public string UserName;
    public string CultureName;
    public string BrandModulePath;
    public int BrandRssCount;
    public bool EnableBrandRss;
    public string  BrandRssPage;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreSettingConfig ssc = new StoreSettingConfig();
                IncludeJs("BrandView", "/Modules/AspxCommerce/AspxBrandView/js/BrandView.js");
                IncludeCss("BrandView", "/Modules/AspxCommerce/AspxBrandView/css/module.css");
                PortalID = GetPortalID;
                StoreID = GetStoreID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                BrandModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                GetBrandSetting();
            }
            IncludeLanguageJS();
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
        AspxBrandViewController objBrand = new AspxBrandViewController();
        BrandSettingInfo lstBrandSetting = objBrand.GetBrandSetting(aspxCommonObj);
        if (lstBrandSetting != null)
        {
            EnableBrandRss = lstBrandSetting.IsEnableBrandRss;
            BrandRssCount = lstBrandSetting.BrandRssCount;
            BrandRssPage = lstBrandSetting.BrandRssPage;
        }
    }
}
