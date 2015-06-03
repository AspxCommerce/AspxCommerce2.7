using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using AspxCommerce.PopularTags;

public partial class Modules_AspxCommerce_AspxPopularTags_ViewTaggedItems : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName, UserIP, CountryName;
    public string TagsIDs = string.Empty;
    public string SessionCode = string.Empty;
    public string NoImageTagItemsPath, AllowAddToCart, AllowOutStockPurchase, AllowWishListViewTagsItems;
    public int NoOfItemsInARow;
    public string ItemDisplayMode, PopularTagsModulePath;
    public List<AspxTemplateKeyValue> AspxTemplateValue = new List<AspxTemplateKeyValue>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("ViewTagsItems", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css", "/Templates/" + TemplateName + "/css/FancyDropDown/fancy.css");
                IncludeJs("ViewTagsItems", "/js/Templating/tmpl.js",
                        "/js/encoder.js", "/js/Paging/jquery.pagination.js",
                           "/js/Templating/AspxTemplate.js",
                           "/js/MessageBox/jquery.easing.1.3.js",
                           "/js/MessageBox/alertbox.js", "/js/jquery.tipsy.js",
                           "/js/FancyDropDown/itemFancyDropdown.js",
                           "/js/SageFrameCorejs/itemTemplateView.js",
                           "/Modules/AspxCommerce/AspxPopularTags/js/ViewTaggedItem.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                PopularTagsModulePath = ResolveUrl((this.AppRelativeTemplateSourceDirectory));
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);           

                TagsIDs = Request.QueryString["tagsId"];

                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageTagItemsPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowAddToCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowAddToCartButton, StoreID, PortalID, CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);            
                ItemDisplayMode = ssc.GetStoreSettingsByKey(StoreSetting.ItemDisplayMode, StoreID, PortalID, CultureName);
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

    public void GetPopularTagsSettings()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        PopularTagsController ptc = new PopularTagsController();
        List<PopularTagsSettingInfo> ptSettingInfo = ptc.GetPopularTagsSetting(aspxCommonObj);
        if (ptSettingInfo != null && ptSettingInfo.Count > 0)
        {
            foreach (var item in ptSettingInfo)
            {
               
                NoOfItemsInARow = item.TaggedItemInARow;                
            }
        }
    }
}