using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using AspxCommerce.Core;
using SageFrame;
using SageFrame.Framework;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxGiftCard_GiftCardsAll : BaseAdministrationUserControl
{
    public string UserIp;
    public string CountryName = string.Empty;
    public string SessionCode = string.Empty;
    public int StoreID, PortalID, CustomerID, UserModuleID,rowTotal;
    public string UserName, CultureName;
    public string aspxfilePath;

    public string DefaultImagePath,
                  AllowOutStockPurchase,                                
                  AllowAddToCart;

    public int  NoOfItemsInARow, MaxCompareItemCount;
    public string NoImageCategoryDetailPath;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CustomerID = GetCustomerID;
            UserName = GetUsername;
            CultureName = GetCurrentCultureName;
            aspxfilePath = ResolveUrl("~") + "Modules/AspxCommerce/AspxItemsManagement/";
            IncludeCss("GirdCardAll","/Templates/" + TemplateName + "/css/ToolTip/tooltip.css");
            IncludeJs("GirdCardAll", "/js/Paging/jquery.pagination.js", "/js/jquery.tipsy.js");
            if (HttpContext.Current.Session.SessionID != null)
            {
                SessionCode = HttpContext.Current.Session.SessionID.ToString();
            }
            if (HttpContext.Current.Session.SessionID != null)
            {
                SessionCode = HttpContext.Current.Session.SessionID.ToString();
            }
            if (SageUserModuleID != "")
            {
                UserModuleID = int.Parse(SageUserModuleID);
            }
            else
            {
                UserModuleID = 0;
            }
            UserIp = HttpContext.Current.Request.UserHostAddress;
            IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
            ipToCountry.GetCountry(UserIp, out CountryName);
            StoreSettingConfig ssc = new StoreSettingConfig();
            AllowAddToCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowAddToCartButton, StoreID, PortalID, CultureName);
            DefaultImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID,
                                                         CultureName);                     
            AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID,
                                                              CultureName);
            MaxCompareItemCount =
                int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaxNoOfItemsToCompare, StoreID, PortalID, CultureName));
            NoOfItemsInARow = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfDisplayItems, StoreID, PortalID, CultureName));
                     
            GetAllGiftCards();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private Hashtable hst = null;

    public void GetAllGiftCards()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.UserName = UserName;
        aspxCommonObj.CultureName = CultureName;
        int offset = 1;
        int limit = 8;
        List<LatestItemsInfo> lstGiftItems = AspxItemMgntController.GetAllGiftCards(offset,limit,rowTotal,aspxCommonObj);
        StringBuilder GiftItems = new StringBuilder();
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        
        if (lstGiftItems != null && lstGiftItems.Count > 0)
        {
            foreach (LatestItemsInfo item in lstGiftItems)
            {
                rowTotal = item.RowTotal;
                string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.ImagePath;
                if (item.ImagePath == "")
                {
                    item.ImagePath = DefaultImagePath;
                }
                if (item.AlternateText == "")
                {
                    item.AlternateText = item.Name;
                }
                if ((lstGiftItems.IndexOf(item) + 1) % NoOfItemsInARow == 0)
                {
                    GiftItems.Append("<div class=\"cssClassProductsBox cssClassNoMargin\">");
                }
                else
                {
                    GiftItems.Append("<div class=\"cssClassProductsBox\">");
                }
                string itemPrice = Convert.ToDecimal(item.Price).ToString("N2");
                string itemPriceRate = Convert.ToDecimal(item.Price).ToString("N2");

                var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(item.SKU) + pageExtension;
                GiftItems.Append("<div id=\"productImageWrapID_");
                GiftItems.Append(item.ItemID);
                GiftItems.Append("\" class=\"cssClassProductsBoxInfo\" costvariantItem=");
                GiftItems.Append(item.IsCostVariantItem);
                GiftItems.Append("  itemid=\"");
                GiftItems.Append(item.ItemID);
                GiftItems.Append("\"><h2>");
                GiftItems.Append(item.Name);
                GiftItems.Append("</h2><h3>");
                GiftItems.Append(item.SKU);
                GiftItems.Append(
                    "</h3><div id=\"divQuickLookonHover\"><div id=\"divitemImage\" class=\"cssClassProductPicture\"><a href=\"");
                GiftItems.Append(hrefItem);
                GiftItems.Append("\" ><img id=\"");
                GiftItems.Append(item.ItemID);
                GiftItems.Append("\"  alt=\"");
                GiftItems.Append(item.AlternateText);
                GiftItems.Append("\"  title=\"");
                GiftItems.Append(item.AlternateText);
                GiftItems.Append("\" data-original=\"");
                GiftItems.Append(aspxTemplateFolderPath);
                GiftItems.Append("/images/loader_100x12.gif\" src=\"");
                GiftItems.Append(aspxRootPath);
                GiftItems.Append(imagePath.Replace("uploads", "uploads/Medium"));
                GiftItems.Append("\"></a></div>");
                if (item.HidePrice != true)
                {
                    if (item.ListPrice != null)
                    {
                        string strAmount = Math.Round((double)(item.ListPrice), 2).ToString();
                        GiftItems.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                        GiftItems.Append("<p class=\"cssClassProductOffPrice\">");
                        GiftItems.Append("<span class=\"cssClassFormatCurrency\" value=");
                        GiftItems.Append(Convert.ToDecimal(item.ListPrice).ToString("N2"));
                        GiftItems.Append(">");
                        GiftItems.Append(strAmount);
                        GiftItems.Append("</span></p><p class=\"cssClassProductRealPrice \" >");
                        GiftItems.Append("<span class=\"cssClassFormatCurrency\" value=");
                        GiftItems.Append(itemPrice);
                        GiftItems.Append(">");
                        GiftItems.Append(itemPriceRate);
                        GiftItems.Append("</span></p></div></div>");
                    }
                    else
                    {
                        GiftItems.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                        GiftItems.Append("<p class=\"cssClassProductRealPrice \" >");
                        GiftItems.Append("<span class=\"cssClassFormatCurrency\" value=");
                        GiftItems.Append(itemPrice);
                        GiftItems.Append(">");
                        GiftItems.Append(itemPriceRate);
                        GiftItems.Append("</span></p></div></div>");
                    }
                }
                else
                {
                    GiftItems.Append("<div class=\"cssClassProductPriceBox\"></div>");
                }
                GiftItems.Append("<div class=\"cssClassProductDetail\"><p><a href=\"");
                GiftItems.Append(aspxRedirectPath);
                GiftItems.Append("item/");
                GiftItems.Append(item.SKU);
                GiftItems.Append(pageExtension);
                GiftItems.Append("\">");
                GiftItems.Append(getLocale("Details"));
                GiftItems.Append("</a></p></div>");
                GiftItems.Append("<div class=\"sfQuickLook\" style=\"display:none\">");
                GiftItems.Append("<img itemId=\"");
                GiftItems.Append(item.ItemID);
                GiftItems.Append("\" sku=\"");
                GiftItems.Append(item.SKU);
                GiftItems.Append("\" src=\"");
                GiftItems.Append(aspxTemplateFolderPath);
                GiftItems.Append("/images/QV_Button.png\" rel=\"popuprel\" />");
                GiftItems.Append("</div>");
                GiftItems.Append("</div>");
                GiftItems.Append("<div class=\"sfButtonwrapper\">");                
                    if (GetCustomerID > 0 && GetUsername.ToLower() != "anonymoususer")
                    {
                        GiftItems.Append("<div class=\"cssClassWishListButton\">");
                        GiftItems.Append("<input type=\"hidden\" name='itemwish' value=");
                        GiftItems.Append(item.ItemID);
                        GiftItems.Append(",'");
                        GiftItems.Append(item.SKU);
                        GiftItems.Append("',this  />");
                        GiftItems.Append("</div>");
                    }
                    else
                    {
                        GiftItems.Append("<div class=\"cssClassWishListButton\">");
                        GiftItems.Append(
                            "<button type=\"button\" id=\"addWishList\" onclick=\"AspxCommerce.RootFunction.Login();\">");
                        GiftItems.Append("<span><span><span>+</span>");
                        GiftItems.Append(getLocale("Wishlist"));
                        GiftItems.Append("</span></span></button></div>");
                    } 
                    GiftItems.Append("<div class=\"cssClassCompareButton\">");
                    GiftItems.Append("<input type=\"hidden\" name='itemcompare' value=");
                    GiftItems.Append(item.ItemID);
                    GiftItems.Append(",'");
                    GiftItems.Append(item.SKU);
                    GiftItems.Append("',this  />");
                    GiftItems.Append(" </div>");                  
               
                GiftItems.Append("</div>");
                GiftItems.Append("<div class=\"cssClassclear\"></div></div>");
                string itemSKU = item.SKU;
                string itemName = item.Name;
                if (AllowAddToCart.ToLower() == "true")
                {
                    if (AllowOutStockPurchase.ToLower() == "false")
                    {
                        if (item.IsOutOfStock == true)
                        {
                            GiftItems.Append(
                                "<div class=\"cssClassAddtoCard\"><div class=\"sfButtonwrapper cssClassOutOfStock\">");
                            GiftItems.Append("<button type=\"button\"><span>");
                            GiftItems.Append(getLocale("Out Of Stock"));
                            GiftItems.Append("</span></button></div></div>");
                        }
                        else
                        {                         
                            GiftItems.Append("<div class=\"cssClassAddtoCard\"><div class=\"sfButtonwrapper\">");
                            GiftItems.Append("<button type=\"button\" class=\"addtoCart\"");
                            GiftItems.Append("data-addtocart=\"");
                            GiftItems.Append("addtocart");
                            GiftItems.Append(item.ItemID);
                            GiftItems.Append("\" title=\"");
                            GiftItems.Append(item.Name);

                            GiftItems.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            GiftItems.Append(item.ItemID + ",");
                            GiftItems.Append(item.Price + ",");
                            GiftItems.Append("'" + item.SKU + "'" + "," + 1);
                            GiftItems.Append(",'");
                            GiftItems.Append(item.IsCostVariantItem);
                            GiftItems.Append("',this);\"><span>");
                            GiftItems.Append(getLocale("Add To Cart"));
                            GiftItems.Append("</span></button></div></div>");
                        }
                    }
                    else
                    {    
                        GiftItems.Append("<div class=\"cssClassAddtoCard\"><div class=\"sfButtonwrapper\">");
                        GiftItems.Append("<button type=\"button\" class=\"addtoCart\"");
                        GiftItems.Append("data-addtocart=\"");
                        GiftItems.Append("addtocart");
                        GiftItems.Append(item.ItemID);
                        GiftItems.Append("\" title=\"");
                        GiftItems.Append(item.Name);

                        GiftItems.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                        GiftItems.Append(item.ItemID + ",");
                        GiftItems.Append(item.Price + ",");
                        GiftItems.Append("'" + item.SKU + "'" + "," + 1);
                        GiftItems.Append(",'");
                        GiftItems.Append(item.IsCostVariantItem);
                        GiftItems.Append("',this);\"><span>");
                        GiftItems.Append(getLocale("Add To Cart"));
                        GiftItems.Append("</span></button></div></div>");
                    }
                    GiftItems.Append("</div>");
                }
            }
        }

        else
        {
            GiftItems.Append("<span class=\"cssClassNotFound\">");
            GiftItems.Append(getLocale("No items found!"));
            GiftItems.Append("</span>");
        }
        divGiftCards.InnerHtml = GiftItems.ToString();
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
