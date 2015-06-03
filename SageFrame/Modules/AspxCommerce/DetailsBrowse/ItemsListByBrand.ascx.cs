using System;
using System.Collections.Generic;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame;
using SageFrame.Core;
public partial class Modules_Admin_DetailsBrowse_ItemsListByBrand : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName, UserIP, CountryName, DefaultShoppingOptionImgPath,AllowAddToCart, AllowOutStockPurchase;
    public string SessionCode = string.Empty;
    public string BrandName = string.Empty;
    public int NoOfItemsInARow;
    public string ItemDisplayMode;
    public List<AspxTemplateKeyValue> AspxTemplateValue = new List<AspxTemplateKeyValue>();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("BrandItems1", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css", "/Templates/" + TemplateName + "/css/FancyDropDown/fancy.css");
                IncludeJs("BrandItems1", "/js/jquery.cookie.js", "/js/Templating/tmpl.js", "/js/encoder.js", "/js/Paging/jquery.pagination.js",
                       "/js/Templating/AspxTemplate.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", 
                       "/js/jquery.tipsy.js", "/js/FancyDropDown/itemFancyDropdown.js", "/js/SageFrameCorejs/itemTemplateView.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;

                StoreSettingConfig ssc = new StoreSettingConfig();
                DefaultShoppingOptionImgPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowAddToCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowAddToCartButton, StoreID, PortalID, CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);                
                NoOfItemsInARow = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfDisplayItems, StoreID, PortalID, CultureName));
                ItemDisplayMode = ssc.GetStoreSettingsByKey(StoreSetting.ItemDisplayMode, StoreID, PortalID, CultureName);
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);
                SageFrameRoute parentPage = (SageFrameRoute)this.Page;
                BrandName =AspxUtility.fixedDecodeURIComponent(parentPage.Key);
                IncludeLanguageJS();
                GetAspxTemplates();
            }
            IncludeLanguageJS();


        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
   
    private void GetAspxTemplates()
    {
        AspxTemplateValue = AspxGetTemplates.GetAspxTemplateKeyValue();

        foreach (AspxTemplateKeyValue value in AspxTemplateValue)
        {
            string xx = value.TemplateKey + "@" + value.TemplateValue;
            Page.ClientScript.RegisterArrayDeclaration("jsTemplateArray", "\'" + xx + "\'");
        }
    }
}
