using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_Admin_DetailsBrowse_ItemLists : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID, CategoryID;
    public string UserName, CultureName, UserIP, CountryName;
    public string SessionCode = string.Empty;
    public string SearchText = string.Empty;
    public string NoImageItemListPath, AllowAddToCart, AllowOutStockPurchase;
    public int NoOfItemsInARow;
    public string ItemDisplayMode;
    public bool IsGiftCard = false;
    public List<AspxTemplateKeyValue> AspxTemplateValue = new List<AspxTemplateKeyValue>();
    Hashtable hst = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("ItemLists", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css", "/Templates/" + TemplateName + "/css/FancyDropDown/fancy.css");
                IncludeJs("ItemLists", "/js/Templating/tmpl.js", "/js/encoder.js", "/js/Paging/jquery.pagination.js",
               "/js/Templating/AspxTemplate.js");
                IncludeJs("ItemLists", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                    "/js/jquery.tipsy.js", "/js/FancyDropDown/itemFancyDropdown.js", "/js/SageFrameCorejs/itemTemplateView.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);

                CategoryID = Int32.Parse(Request.QueryString["cid"]);
                SearchText = Request.QueryString["q"];
                IsGiftCard = bool.Parse(Request.QueryString["isgiftcard"]);
                hst = AppLocalized.getLocale(this.AppRelativeTemplateSourceDirectory);
                this.Page.Title = getLocale("Search") + " - " + SearchText;
                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageItemListPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowAddToCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowAddToCartButton, StoreID, PortalID, CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);               
                NoOfItemsInARow = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfDisplayItems, StoreID, PortalID, CultureName));
                ItemDisplayMode = ssc.GetStoreSettingsByKey(StoreSetting.ItemDisplayMode, StoreID, PortalID, CultureName);
            }
            GetAspxTemplates();
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
