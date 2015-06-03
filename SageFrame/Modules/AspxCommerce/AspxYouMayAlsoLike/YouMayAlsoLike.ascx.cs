/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using AspxCommerce.YouMayAlsoLike;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxYouMayAlsoLike_YouMayAlsoLike : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID, NoOfYouMayAlsoLikeItems,NoOfYouMayAlsoLikeInARow;
    public string UserName, CultureName,YouMayAlsoLikeModulePath;
    public string SessionCode = string.Empty;
    public bool EnableYouMayAlsoLike;
    public string NoImageYouMayAlsoLikePath,AllowAddToCart,  AllowOutStockPurchase;
    public decimal Rate = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CustomerID = GetCustomerID;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
                StoreSettingConfig ssc = new StoreSettingConfig();
                decimal additionalCCVR = decimal.Parse(ssc.GetStoreSettingsByKey(StoreSetting.AdditionalCVR, StoreID, PortalID, CultureName));
                string MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
                if (HttpContext.Current.Session["CurrencyRate"] != null)
                {
                    if (Session["CurrencyCode"].ToString() != MainCurrency)
                    {
                        decimal rate1 = decimal.Parse(Session["CurrencyRate"].ToString());
                        Rate = Math.Round(rate1 + (rate1 * additionalCCVR / 100), 4);
                    }
                    else
                    {
                        Rate = decimal.Parse(Session["CurrencyRate"].ToString());
                    }
                }
                YouMayAlsoLikeModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                NoImageYouMayAlsoLikePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);                
                AllowAddToCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowAddToCartButton, StoreID, PortalID, CultureName);              
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
                IncludeCss("YouMayAlsoLike", "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css");
                IncludeJs("YouMayAlsoLike", "/js/jquery.tipsy.js");

            }
            IncludeLanguageJS();
            GetYouMayAlsoLikeSetting();
            if (EnableYouMayAlsoLike == true && NoOfYouMayAlsoLikeItems > 0)
            {
                string itemsku = null;
                string url = HttpContext.Current.Request.Url.ToString();
                if(url.Contains("item"))
                {
                    itemsku = url.Substring(url.LastIndexOf('/'));
                    itemsku = itemsku.Substring(1, (itemsku.LastIndexOf('.') - 1));
                }

                GetItemRelatedUpSellAndCrossSellList(itemsku);
            }
            IncludeLanguageJS();

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void GetYouMayAlsoLikeSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
        YouMayAlsoLikeSettingInfo lstRelatedItemSetting = objYouMayLike.GetYouMayAlsoLikeSetting(aspxCommonObj);


        if (lstRelatedItemSetting != null)
        {
            EnableYouMayAlsoLike = lstRelatedItemSetting.IsEnableYouMayAlsoLike;
            NoOfYouMayAlsoLikeItems = lstRelatedItemSetting.YouMayAlsoLikeCount;
            NoOfYouMayAlsoLikeInARow = lstRelatedItemSetting.YouMayAlsoLikeInARow;

        }
    }
    Hashtable hst = null;
    public void GetItemRelatedUpSellAndCrossSellList(string itemsku)
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.UserName = UserName;
        aspxCommonObj.CultureName = CultureName;
        aspxCommonObj.CustomerID = CustomerID;
        aspxCommonObj.SessionCode = SessionCode;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
        List<YouMayAlsoLikeInfo> lstRelatedItem = objYouMayLike.GetYouMayAlsoLikeItems(itemsku, aspxCommonObj, NoOfYouMayAlsoLikeItems);
        StringBuilder realatedItemCartContent = new StringBuilder();
         if (lstRelatedItem != null && lstRelatedItem.Count > 0)
        {
            realatedItemCartContent.Append("<h2 class=\"sfLocale\">You May Also Like</h2>");   
            foreach (YouMayAlsoLikeInfo item in lstRelatedItem)
            {
                string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.BaseImage;
                string altImagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.Name;
                if (item.BaseImage == "")
                {
                    imagePath = NoImageYouMayAlsoLikePath;
                }
                else
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(item.BaseImage, ImageType.Medium, ImageCategoryType.Item, aspxCommonObj);
                }
                if (item.AlternateText == "")
                {
                    item.AlternateText = item.Name;
                }
                string itemPrice = Convert.ToDecimal(item.Price).ToString("N2");
                string itemPriceValue = item.Price;
                string itemPriceRate = Convert.ToDecimal(item.Price).ToString("N2"); 
                StringBuilder dataContent = new StringBuilder();
                dataContent.Append("data-class=\"addtoCart\" data-ItemTypeID=\"" + item.ItemTypeID + "\" data-ItemID=\"" + item.ItemID + "\"data-type=\"button\" data-addtocart=\"");
                dataContent.Append("addtocart");
                dataContent.Append(item.ItemID);
                dataContent.Append("\" data-title=\"");
                dataContent.Append(item.Name);
                dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                dataContent.Append(item.ItemID);
                dataContent.Append(",");
                dataContent.Append(itemPriceValue);
                dataContent.Append(",'");
                dataContent.Append(item.SKU);
                dataContent.Append("',");
                dataContent.Append(1);
                dataContent.Append(",'");
                dataContent.Append(item.IsCostVariantItem);
                dataContent.Append("',this);\"");           
                if ((lstRelatedItem.IndexOf(item) + 1) % NoOfYouMayAlsoLikeInARow == 0)
                {
                    realatedItemCartContent.Append("<div class=\"cssClassProductsBox cssClassNoMargin\">");
                }
                else
                {
                    realatedItemCartContent.Append("<div class=\"cssClassProductsBox\">");
                }
                var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(item.SKU) + pageExtension;
                realatedItemCartContent.Append("<div id=\"productImageWrapID_");
                realatedItemCartContent.Append(item.ItemID);
                realatedItemCartContent.Append("\" class=\"cssClassProductsBoxInfo\" costvariantItem=");
                realatedItemCartContent.Append(item.IsCostVariantItem);
                realatedItemCartContent.Append("  itemid=\"");
                realatedItemCartContent.Append(item.ItemID);
                realatedItemCartContent.Append("\">");
                realatedItemCartContent.Append("<h3>");
                realatedItemCartContent.Append(item.SKU);
                realatedItemCartContent.Append("</h3><div class=\"divQuickLookonHover\"><div class=\"divitemImage cssClassProductPicture\"><a href=\"");
                realatedItemCartContent.Append(hrefItem);
                realatedItemCartContent.Append("\" ><img id=\"img_");
                realatedItemCartContent.Append(item.ItemID);
                realatedItemCartContent.Append("\"  alt=\"");
                realatedItemCartContent.Append(item.AlternateText);
                realatedItemCartContent.Append("\"  title=\"");
                realatedItemCartContent.Append(item.AlternateText);
                realatedItemCartContent.Append("\"");
                realatedItemCartContent.Append("src=\"");
                realatedItemCartContent.Append(aspxRootPath);
                realatedItemCartContent.Append(imagePath.Replace("uploads", "uploads/Medium"));
                realatedItemCartContent.Append("\" orignalPath=\"");
                realatedItemCartContent.Append(imagePath.Replace("uploads", "uploads/Medium"));
                realatedItemCartContent.Append("\" altImagePath=\"");
                realatedItemCartContent.Append(altImagePath.Replace("uploads", "uploads/Medium"));
                realatedItemCartContent.Append("\"/></a></div>");
                realatedItemCartContent.Append("<div class='cssLatestItemInfo'>");
                realatedItemCartContent.Append("<h2><a href=\"");
                realatedItemCartContent.Append(hrefItem);
                realatedItemCartContent.Append("\" title=\"" + item.Name + "\">");
                string name = string.Empty;
                if (item.Name.Length > 50)
                {
                    name = item.Name.Substring(0, 50);
                    int index = 0;
                    index = name.LastIndexOf(' ');
                    name = name.Substring(0, index);
                    name = name + "...";
                }
                else
                {
                    name = item.Name;
                }
                realatedItemCartContent.Append(name);
                realatedItemCartContent.Append("</a></h2>");
                if (item.HidePrice != true)
                {
                    if (item.ListPrice != null && item.ListPrice!="")
                    {

                                               if (item.ItemTypeID == 5)
                        {
                            realatedItemCartContent.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                            realatedItemCartContent.Append("<p class=\"cssClassProductRealPrice \">");
                            realatedItemCartContent.Append(getLocale("Starting At "));
                            realatedItemCartContent.Append("<span class=\"cssClassFormatCurrency\">");
                            realatedItemCartContent.Append(itemPriceRate);
                            realatedItemCartContent.Append("</span></p></div></div>");

                        }
                        else
                        {
                            string strAmount = Math.Round(Convert.ToDecimal(item.ListPrice), 2).ToString("N2");
                            realatedItemCartContent.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                            realatedItemCartContent.Append("<p class=\"cssClassProductOffPrice\">");
                            realatedItemCartContent.Append("<span class=\"cssClassFormatCurrency\">");
                            realatedItemCartContent.Append(strAmount);
                            realatedItemCartContent.Append("</span></p><p class=\"cssClassProductRealPrice \">");
                            realatedItemCartContent.Append("<span class=\"cssClassFormatCurrency\">");
                            realatedItemCartContent.Append(itemPriceRate);
                            realatedItemCartContent.Append("</span></p></div></div>");
                        }
                    }
                    else
                    {//Added for group type products
                        if (item.ItemTypeID == 5)
                        {
                            realatedItemCartContent.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                            realatedItemCartContent.Append("<p class=\"cssClassProductRealPrice \">");
                            realatedItemCartContent.Append(getLocale("Starting At "));
                            realatedItemCartContent.Append("<span class=\"cssClassFormatCurrency\">");
                            realatedItemCartContent.Append(itemPriceRate);
                            realatedItemCartContent.Append("</span></p></div></div>");
                        }
                        else
                        {
                            realatedItemCartContent.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                            realatedItemCartContent.Append("<p class=\"cssClassProductRealPrice \">");
                            realatedItemCartContent.Append("<span class=\"cssClassFormatCurrency\">");
                            realatedItemCartContent.Append(itemPriceRate);
                            realatedItemCartContent.Append("</span></p></div></div>");
                        }
                    }
                }
                else
                {
                    realatedItemCartContent.Append("<div class=\"cssClassProductPriceBox\"></div>");
                }
                realatedItemCartContent.Append("<div class=\"cssClassProductDetail\"><p><a href=\"");
                realatedItemCartContent.Append(aspxRedirectPath);
                realatedItemCartContent.Append("item/");
                realatedItemCartContent.Append(item.SKU);
                realatedItemCartContent.Append(pageExtension);
                realatedItemCartContent.Append("\">");
                realatedItemCartContent.Append(getLocale("Details"));
                realatedItemCartContent.Append("</a></p></div>");

                realatedItemCartContent.Append("<div class=\"sfQuickLook\" style=\"display:none\">");
                realatedItemCartContent.Append("<img itemId=\"");
                realatedItemCartContent.Append(item.ItemID);
                realatedItemCartContent.Append("\" sku=\"");
                realatedItemCartContent.Append(item.SKU);
                realatedItemCartContent.Append("\" src=\"");
                realatedItemCartContent.Append(aspxTemplateFolderPath);
                realatedItemCartContent.Append("/images/QV_Button.png\" alt=\"\" rel=\"popuprel\" />");
                realatedItemCartContent.Append("</div>");
                string itemSKU = item.SKU;
                string itemName = item.Name;
                if (AllowAddToCart.ToLower() == "true")
                {
                    if (AllowOutStockPurchase.ToLower() == "false")
                    {
                        if (item.IsOutOfStock == true)
                        {

                            realatedItemCartContent.Append("<div class=\"cssClassAddtoCard\"><div " + dataContent + " class=\"sfButtonwrapper cssClassOutOfStock\">");
                            realatedItemCartContent.Append("<button type=\"button\"><span>");
                            realatedItemCartContent.Append(getLocale("Out Of Stock"));
                            realatedItemCartContent.Append("</span></button></div></div>");
                        }
                        else
                        {
                            realatedItemCartContent.Append("<div class=\"cssClassAddtoCard\"><div " + dataContent + "class=\"sfButtonwrapper\">");
                            realatedItemCartContent.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                            realatedItemCartContent.Append("data-addtocart=\"");
                            realatedItemCartContent.Append("addtocart");
                            realatedItemCartContent.Append(item.ItemID);
                            realatedItemCartContent.Append("\" title=\"");
                            realatedItemCartContent.Append(itemName);
                            realatedItemCartContent.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            realatedItemCartContent.Append(item.ItemID);
                            realatedItemCartContent.Append(",");
                            realatedItemCartContent.Append(itemPriceValue);
                            realatedItemCartContent.Append(",'");
                            realatedItemCartContent.Append(itemSKU);
                            realatedItemCartContent.Append("',");
                            realatedItemCartContent.Append(1);
                            realatedItemCartContent.Append(",'");
                            realatedItemCartContent.Append(item.IsCostVariantItem);
                            realatedItemCartContent.Append("',this);\">");
                            realatedItemCartContent.Append(getLocale("Cart +"));
                            realatedItemCartContent.Append("</button></label></div></div>");
                        }
                    }
                    else
                    {
                        realatedItemCartContent.Append("<div class=\"cssClassAddtoCard\"><div " + dataContent + " class=\"sfButtonwrapper\">");
                        realatedItemCartContent.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                        realatedItemCartContent.Append("data-addtocart=\"");
                        realatedItemCartContent.Append("addtocart");
                        realatedItemCartContent.Append(item.ItemID);
                        realatedItemCartContent.Append("\" title=\"");
                        realatedItemCartContent.Append(itemName);
                        realatedItemCartContent.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                        realatedItemCartContent.Append(item.ItemID);
                        realatedItemCartContent.Append(",");
                        realatedItemCartContent.Append(itemPriceValue);
                        realatedItemCartContent.Append(",'");
                        realatedItemCartContent.Append(itemSKU);
                        realatedItemCartContent.Append("',");
                        realatedItemCartContent.Append(1);
                        realatedItemCartContent.Append(",'");
                        realatedItemCartContent.Append(item.IsCostVariantItem);
                        realatedItemCartContent.Append("',this);\">");
                        realatedItemCartContent.Append(getLocale("Cart +"));
                        realatedItemCartContent.Append("</button></label></div></div>");

                    }
                    realatedItemCartContent.Append("</div></div>");
                }
                realatedItemCartContent.Append("</div>");
                realatedItemCartContent.Append("</div>");
            }
        }       
        ltrRelatedItemInCart.Text = realatedItemCartContent.ToString();
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
