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
using AspxCommerce.SpecialItems;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class Modules_AspxSpecialsItems_Specials : BaseAdministrationUserControl
{
    private int StoreID, PortalID;
    private string UserName, CultureName, DefaultImagePath;
    private string AllowAddToCart, AllowOutStockPurchase;
    private string SpecialDetailPage;
    private Hashtable hst = null;

    public int NoOfSpecialItems;
    public string SpecialItemRss, RssFeedUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            if (!IsPostBack)
            {
                IncludeJs("Specials", "/Modules/AspxCommerce/AspxSpecialsItems/js/Specials.js");
                IncludeCss("SpecialCss", "/Modules/AspxCommerce/AspxSpecialsItems/css/SpecialItems.css");
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamThree(StoreSetting.DefaultProductImageURL, StoreSetting.ShowAddToCartButton,
                   StoreSetting.AllowOutStockPurchase, out  DefaultImagePath, out AllowAddToCart,
                   out AllowOutStockPurchase, StoreID, PortalID, CultureName);
            }
            IncludeLanguageJS();
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            BindSpecialItems(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    #region Localization
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
    #endregion

    #region Special Items
    private void BindSpecialItems(AspxCommonInfo aspxCommonObj)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxRootPath = ResolveUrl("~/");
        string aspxTemplateFolderPath = aspxRootPath + "Templates/" + TemplateName;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        int NoOfItemInRow = 2;

        DataSet dsSpecialItems = SpecialItemsController.GetSpecialItemsandSettingDataSet(aspxCommonObj);

        if (dsSpecialItems != null && dsSpecialItems.Tables.Count == 2)
        {
            #region Special Items Setting
            DataTable dtSpecialItemsSetting = dsSpecialItems.Tables[0];
            if (dtSpecialItemsSetting != null & dtSpecialItemsSetting.Rows.Count > 0)
            {
                NoOfItemInRow = Convert.ToInt32(dtSpecialItemsSetting.Rows[0]["NoOfItemInRow"].ToString());
                SpecialItemRss = dtSpecialItemsSetting.Rows[0]["IsEnableSpecialItemsRss"].ToString();
                SpecialDetailPage = dtSpecialItemsSetting.Rows[0]["SpecialItemsDetailPageName"].ToString();
                RssFeedUrl = dtSpecialItemsSetting.Rows[0]["SpecialItemsRssPageName"].ToString();
            }
            #endregion

            #region Special Items Html Helper
            DataTable dtSpecialItems = dsSpecialItems.Tables[1];
            int nosOfSpecialItems = dtSpecialItems.Rows.Count;
            StringBuilder specialContent = new StringBuilder();

            if (dtSpecialItems != null && nosOfSpecialItems > 0)
            {
                specialContent.Append("<div class=\"cssClassSpecialBoxInfo\" id=\"divSpItem\">");
                int i = 0;
                foreach(DataRow drSpecialItem in dtSpecialItems.Rows)
                {
                    i++;
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + drSpecialItem["ImagePath"];
                    string altImagePath = "";
                    if (string.IsNullOrEmpty(drSpecialItem["ImagePath"].ToString()))
                    {
                        imagePath = DefaultImagePath;
                    }
                    else
                    {
                        //Resize Image Dynamically
                        InterceptImageController.ImageBuilder(drSpecialItem["ImagePath"].ToString(), ImageType.Medium, ImageCategoryType.Item, aspxCommonObj);
                    }                    
                    if (string.IsNullOrEmpty(drSpecialItem["ImagePath"].ToString()))
                    {
                        altImagePath = imagePath;
                    }
                    string itemPrice = Convert.ToDecimal(drSpecialItem["Price"].ToString()).ToString("N2");
                    string itemPriceValue = drSpecialItem["Price"].ToString();
                    string itemPriceRate = Convert.ToDecimal(drSpecialItem["Price"].ToString()).ToString("N2");

                    if (i % NoOfItemInRow == 0)
                    {
                        specialContent.Append("<div class=\"cssClassProductsBox cssClassNoMargin\">");
                    }
                    else
                    {
                        specialContent.Append("<div class=\"cssClassProductsBox\">");
                    }
                    var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(drSpecialItem["SKU"].ToString()) + pageExtension;
                    specialContent.Append("<div id=\"productImageWrapID_");
                    specialContent.Append(drSpecialItem["ItemID"]);
                    specialContent.Append("\" class=\"cssClassProductsBoxInfo\" costvariantItem=");
                    specialContent.Append(drSpecialItem["CostVariants"]);
                    specialContent.Append("  itemid=\"");
                    specialContent.Append(drSpecialItem["ItemID"]);
                    specialContent.Append("\">");
                    specialContent.Append("<h3>");
                    specialContent.Append(drSpecialItem["SKU"]);
                    specialContent.Append("</h3><div class=\"divQuickLookonHover\"><div class=\"divitemImage cssClassProductPicture\"><a href=\"");
                    specialContent.Append(hrefItem);
                    specialContent.Append("\" ><img id=\"img_");
                    specialContent.Append(drSpecialItem["ItemID"]);
                    specialContent.Append("\"  alt=\"");
                    specialContent.Append(drSpecialItem["Name"]);
                    specialContent.Append("\"  title=\"");
                    specialContent.Append(drSpecialItem["Name"]);
                    specialContent.Append("\"");
                    specialContent.Append("src=\"");
                    specialContent.Append(aspxRootPath);
                    specialContent.Append(imagePath.Replace("uploads", "uploads/Medium"));
                    specialContent.Append("\" orignalPath=\"");
                    specialContent.Append(imagePath.Replace("uploads", "uploads/Medium"));
                    specialContent.Append("\" altImagePath=\"");
                    specialContent.Append(altImagePath.Replace("uploads", "uploads/Medium"));
                    specialContent.Append("\"/></a></div>");
                    specialContent.Append("<div class='cssLatestItemInfo clearfix'>");
                    specialContent.Append("<h2><a href=\"");
                    specialContent.Append(hrefItem);
                    specialContent.Append("\" title=\"");
                    specialContent.Append(drSpecialItem["Name"]);
                    specialContent.Append("\">");

                    string name = string.Empty;
                    if (drSpecialItem["Name"].ToString().Length > 50)
                    {
                        name = drSpecialItem["Name"].ToString().Substring(0, 50);
                        int index = 0;
                        index = name.LastIndexOf(' ');
                        name = name.Substring(0, index);
                        name = name + "...";
                    }
                    else
                    {
                        name = drSpecialItem["Name"].ToString();
                    }
                    specialContent.Append(name);
                    specialContent.Append("</a></h2>");
                    StringBuilder dataContent = new StringBuilder();
                    dataContent.Append("data-class=\"addtoCart\" data-ItemTypeID=\"");
                    dataContent.Append(drSpecialItem["ItemTypeID"]);
                    dataContent.Append("\" data-type=\"button\" data-ItemID=\"");
                    dataContent.Append(drSpecialItem["ItemID"]);
                    dataContent.Append("\" data-addtocart=\"");
                    dataContent.Append("addtocart");
                    dataContent.Append(drSpecialItem["ItemID"]);
                    dataContent.Append("\" data-title=\"");
                    dataContent.Append(name);
                    dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                    dataContent.Append(drSpecialItem["ItemID"]);
                    dataContent.Append(",");
                    dataContent.Append(itemPriceValue);
                    dataContent.Append(",'");
                    dataContent.Append(drSpecialItem["SKU"]);
                    dataContent.Append("',");
                    dataContent.Append(1);
                    dataContent.Append(",'");
                    dataContent.Append(drSpecialItem["CostVariants"]);
                    dataContent.Append("',this);\"");

                    specialContent.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");

                    if (!string.IsNullOrEmpty(drSpecialItem["ListPrice"].ToString()))
                    {//Added for group type products
                        if ((int)drSpecialItem["ItemTypeID"] == 5)
                        {
                            specialContent.Append("<p class=\"cssClassProductRealPrice \">");
                            specialContent.Append(getLocale("Starting At"));

                        }
                        else
                        {
                            string strAmount = Math.Round((decimal)(drSpecialItem["ListPrice"]), 2).ToString("N2");
                            specialContent.Append("<p class=\"cssClassProductOffPrice\">");
                            specialContent.Append("<span class=\"cssClassFormatCurrency\">");
                            specialContent.Append(strAmount);
                            specialContent.Append("</span></p><p class=\"cssClassProductRealPrice \">");
                        }
                    }
                    else
                    {
                        if ((int)drSpecialItem["ItemTypeID"] == 5)
                        {
                            specialContent.Append("<p class=\"cssClassProductRealPrice \" >");
                            specialContent.Append(getLocale("Starting At"));
                        }
                        else
                        {
                            specialContent.Append("<p class=\"cssClassProductRealPrice \" >");
                        }
                    }

                    specialContent.Append("<span class=\"cssClassFormatCurrency\">");
                    specialContent.Append(itemPriceRate);
                    specialContent.Append("</span></p></div></div>");

                    specialContent.Append("<div class=\"cssClassProductDetail\"><p><a href=\"");
                    specialContent.Append(aspxRedirectPath);
                    specialContent.Append("item/");
                    specialContent.Append(drSpecialItem["SKU"]);
                    specialContent.Append(pageExtension);
                    specialContent.Append("\">");
                    specialContent.Append(getLocale("Details"));
                    specialContent.Append("</a></p></div>");

                    specialContent.Append("<div class=\"sfQuickLook\" style=\"display:none\">");
                    specialContent.Append("<img itemId=\"");
                    specialContent.Append(drSpecialItem["ItemID"]);
                    specialContent.Append("\" sku=\"");
                    specialContent.Append(drSpecialItem["SKU"]);
                    specialContent.Append("\" src=\"");
                    specialContent.Append(aspxTemplateFolderPath);
                    specialContent.Append("/images/QV_Button.png\" alt=\"\" rel=\"popuprel\" />");
                    specialContent.Append("</div>");
                    if (!string.IsNullOrEmpty(drSpecialItem["AttributeValues"].ToString()))
                    {
                            specialContent.Append("<div class=\"cssGridDyanamicAttr\">");
                            if (!string.IsNullOrEmpty(drSpecialItem["AttributeValues"].ToString()))
                            {
                                string[] attributeValues = drSpecialItem["AttributeValues"].ToString().Split(',');
                                foreach (string element in attributeValues)
                                {
                                    string[] attributes = element.Split('#');
                                    string attributeName = attributes[0];
                                    string attributeValue = attributes[1];
                                    int inputType = Int32.Parse(attributes[2]);
                                    string validationType = attributes[3];
                                    specialContent.Append("<div class=\"cssDynamicAttributes\">");
                                    specialContent.Append("<div class=\"cssDynamicAttributes\">");
                                    specialContent.Append("<span>");
                                    specialContent.Append(attributeName);
                                    specialContent.Append("</span> :");
                                    if (inputType == 7)
                                    {
                                        specialContent.Append("<span class=\"cssClassFormatCurrency\">");
                                    }
                                    else
                                    {
                                        specialContent.Append("<span>");
                                    }
                                    specialContent.Append(attributeValue);
                                    specialContent.Append("</span></div>");
                                }
                            }
                            specialContent.Append("</div>");
                        }
                    string itemSKU = drSpecialItem["SKU"].ToString();
                    string itemName = drSpecialItem["Name"].ToString();

                    specialContent.Append("<div class=\"cssClassTMar20\">");
                    if (AllowAddToCart.ToLower() == "true")
                    {
                        if (AllowOutStockPurchase.ToLower() == "false")
                        {
                            if ((bool)drSpecialItem["IsOutOfStock"])
                            {

                                specialContent.Append("<div class=\"cssClassAddtoCard\"><div ");
                                specialContent.Append(dataContent);
                                specialContent.Append(" class=\"sfButtonwrapper cssClassOutOfStock\">");
                                specialContent.Append("<button type=\"button\"><span>");
                                specialContent.Append(getLocale("Out Of Stock"));
                                specialContent.Append("</span></button></div></div>");
                            }
                            else
                            {
                                specialContent.Append("<div class=\"cssClassAddtoCard\"><div ");
                                specialContent.Append(dataContent);
                                specialContent.Append(" class=\"sfButtonwrapper\">");
                                specialContent.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                                specialContent.Append("data-addtocart=\"");
                                specialContent.Append("addtocart");
                                specialContent.Append(drSpecialItem["ItemID"]);
                                specialContent.Append("\" title=\"");
                                specialContent.Append(itemName);
                                specialContent.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                                specialContent.Append(drSpecialItem["ItemID"]);
                                specialContent.Append(",");
                                specialContent.Append(itemPriceValue);
                                specialContent.Append(",'");
                                specialContent.Append(itemSKU);
                                specialContent.Append("',");
                                specialContent.Append(1);
                                specialContent.Append(",'");
                                specialContent.Append(drSpecialItem["CostVariants"]);
                                specialContent.Append("',this);\">");
                                specialContent.Append(getLocale("Cart +"));
                                specialContent.Append("</button></label></div></div>");
                            }
                        }
                        else
                        {
                            specialContent.Append("<div class=\"cssClassAddtoCard\"><div ");
                            specialContent.Append(dataContent);
                            specialContent.Append(" class=\"sfButtonwrapper\">");
                            specialContent.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                            specialContent.Append("data-addtocart=\"");
                            specialContent.Append("addtocart");
                            specialContent.Append(drSpecialItem["ItemID"]);
                            specialContent.Append("\" title=\"");
                            specialContent.Append(itemName);
                            specialContent.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            specialContent.Append(drSpecialItem["ItemID"]);
                            specialContent.Append(",");
                            specialContent.Append(itemPriceValue);
                            specialContent.Append(",'");
                            specialContent.Append(itemSKU);
                            specialContent.Append("',");
                            specialContent.Append(1);
                            specialContent.Append(",'");
                            specialContent.Append(drSpecialItem["CostVariants"]);
                            specialContent.Append("',this);\">");
                            specialContent.Append(getLocale("Cart +"));
                            specialContent.Append("</button></label></div></div>");

                        }
                    }
                    if (GetCustomerID > 0 && GetUsername.ToLower() != "anonymoususer")
                    {
                        specialContent.Append("<div class=\"cssClassWishListButton\">");
                        specialContent.Append("<label class='i-wishlist cssWishListLabel cssClassDarkBtn'><button type=\"button\" id=\"addWishList\" onclick=AspxCommerce.RootFunction.CheckWishListUniqueness(");
                        specialContent.Append(drSpecialItem["ItemID"]);
                        specialContent.Append(",'");
                        specialContent.Append(drSpecialItem["SKU"]);
                        specialContent.Append("',this);><span>");
                        specialContent.Append(getLocale("Wishlist+"));
                        specialContent.Append("</span></button></label></div>");
                    }
                    else
                    {
                        specialContent.Append("<div class=\"cssClassWishListButton\">");
                        specialContent.Append("<label class='i-wishlist cssWishListLabel cssClassDarkBtn'><button type=\"button\" id=\"addWishList\" onclick=\"AspxCommerce.RootFunction.Login();\">");
                        specialContent.Append("<span>");
                        specialContent.Append(getLocale("Wishlist+"));
                        specialContent.Append("</span></button></label></div>");
                    }
                    specialContent.Append("<div class=\"cssClassWishListButton\">");
                    specialContent.Append("<input type=\"hidden\" name='itemwish' value=");
                    specialContent.Append(drSpecialItem["ItemID"]);
                    specialContent.Append(",'");
                    specialContent.Append(drSpecialItem["SKU"]);
                    specialContent.Append("',this  />");
                    specialContent.Append("</div>");
                    specialContent.Append("</div></div>");
                    specialContent.Append("</div></div>");
                    specialContent.Append("</div>");
                }

                specialContent.Append("</div>");
                if (nosOfSpecialItems > NoOfItemInRow)
                {
                    string strHtml = "<a href=\"" + aspxRedirectPath + SpecialDetailPage + pageExtension + "?id=special\">" + getLocale("View More") + "</a>";
                    divViewMoreSpecial.InnerHtml = strHtml;
                }
                ltrSpecialItems.Text = specialContent.ToString();
            }
            else
            {
                StringBuilder noSpl = new StringBuilder();
                noSpl.Append("<span class=\"cssClassNotFound\">");
                noSpl.Append(getLocale("No special item found in this store!"));
                noSpl.Append("</span>");
                divSpclBox.InnerHtml = noSpl.ToString();
                divSpclBox.Attributes.Add("class", "");
            }
            #endregion
        }
    }
    #endregion
}