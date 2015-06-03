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
using System.Web;
using System.Web.UI;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using AspxCommerce.LatestItems;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class Modules_AspxLatestItems_LatestItems : BaseAdministrationUserControl
{
    private int StoreID, PortalID,NoOfLatestItemsInARow;
    public bool EnableLatestItemRss;
    private string UserName, CultureName,resolveUrl, DefaultImagePath,  
        AllowAddToCart, AllowOutStockPurchase;
    public int NoOfLatestItems;
    public string RssFeedUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            if (!IsPostBack)
            {
                string templateName = TemplateName;

                IncludeCss("LatestItems", "/Templates/" + templateName + "/css/MessageBox/style.css",
                       "/Templates/" + templateName + "/css/FancyDropDown/fancy.css",
                       "/Templates/" + templateName + "/css/ToolTip/tooltip.css",
                        "/Modules/AspxCommerce/AspxLatestItems/css/module.css");
                IncludeJs("LatestItems",
                    "/Modules/AspxCommerce/AspxLatestItems/js/LatestItems.js"
                    , "/js/MessageBox/alertbox.js");

                resolveUrl = ResolveUrl("~/");

                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamThree(StoreSetting.DefaultProductImageURL, StoreSetting.ShowAddToCartButton,
                    StoreSetting.AllowOutStockPurchase, out  DefaultImagePath, out AllowAddToCart,
                    out AllowOutStockPurchase, StoreID, PortalID, CultureName);

            }
            IncludeLanguageJS();
            GetLatestItemsByCount(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;
    private void GetLatestItemsByCount(AspxCommonInfo aspxCommonObj)
    {

        AspxLatestItemsController objLatestItems = new AspxLatestItemsController();
        DataSet ds = new DataSet();
        ds = objLatestItems.GetLatestItemsInfo(aspxCommonObj);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        {
            DataTable dtSettings = ds.Tables[0];
            if (dtSettings != null && dtSettings.Rows.Count > 0)
            {
                NoOfLatestItems = int.Parse(dtSettings.Rows[0]["LatestItemCount"].ToString());
                EnableLatestItemRss = bool.Parse(dtSettings.Rows[0]["IsEnableLatestRss"].ToString());
                NoOfLatestItemsInARow = int.Parse(dtSettings.Rows[0]["LatestItemInARow"].ToString());
                RssFeedUrl = dtSettings.Rows[0]["LatestItemRssPage"].ToString();
            }

            StringBuilder RecentItemContents = new StringBuilder();
            string modulePath = this.AppRelativeTemplateSourceDirectory;//"~/Modules/AspxCommerce/AspxLatestItems/";
            hst = AppLocalized.getLocale(modulePath);
            string pageExtension = SageFrameSettingKeys.PageExtension;
            string aspxTemplateFolderPath = resolveUrl + "Templates/" + TemplateName;
            string aspxRootPath = resolveUrl;

            DataTable dtItems = ds.Tables[1];

            if (dtItems != null && dtItems.Rows.Count > 0)
            {
                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + dtItems.Rows[i]["ImagePath"];
                    string altImagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + dtItems.Rows[i]["AlternateImagePath"];
                    if (dtItems.Rows[i]["ImagePath"].ToString() == string.Empty)
                    {
                        imagePath = DefaultImagePath;

                    }
                    else
                    {   //Resize Image Dynamically
                        InterceptImageController.ImageBuilder(dtItems.Rows[i]["ImagePath"].ToString(), ImageType.Medium, ImageCategoryType.Item, aspxCommonObj);
                    }
                    if (dtItems.Rows[i]["AlternateText"].ToString() == string.Empty)
                    {
                        dtItems.Rows[i]["AlternateText"] = dtItems.Rows[i]["Name"];
                    }
                    if (dtItems.Rows[i]["AlternateImagePath"].ToString() == string.Empty)
                    {
                        altImagePath = imagePath;
                    }
                    string itemPrice = Convert.ToDecimal(dtItems.Rows[i]["Price"]).ToString("N2");
                    string itemPriceValue = dtItems.Rows[i]["Price"].ToString(); 
                    string itemPriceRate = Convert.ToDecimal(dtItems.Rows[i]["Price"]).ToString("N2"); 

                    if ((i + 1) % NoOfLatestItemsInARow == 0)
                    {
                        RecentItemContents.Append("<div class=\"cssClassProductsBox cssClassNoMargin\">");
                    }
                    else
                    {
                        RecentItemContents.Append("<div class=\"cssClassProductsBox\">");
                    }
                    var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(dtItems.Rows[i]["SKU"].ToString()) + pageExtension;//dt.rows[i]["SKU"].tos();
                    RecentItemContents.Append("<div id=\"productImageWrapID_");
                    RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                    RecentItemContents.Append("\" class=\"cssClassProductsBoxInfo\" costvariantItem=");
                    RecentItemContents.Append(dtItems.Rows[i]["IsCostVariantItem"]);
                    RecentItemContents.Append("  itemid=\"");
                    RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                    RecentItemContents.Append("\">");
                    RecentItemContents.Append("<h3>");
                    RecentItemContents.Append(dtItems.Rows[i]["SKU"]);
                    RecentItemContents.Append("</h3><div><div class=\"divitemImage cssClassProductPicture\"><a href=\"");
                    RecentItemContents.Append(hrefItem);
                    RecentItemContents.Append("\" ><img id=\"img_");
                    RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                    RecentItemContents.Append("\"  alt=\"");
                    RecentItemContents.Append(dtItems.Rows[i]["AlternateText"]);
                    RecentItemContents.Append("\"  title=\"");
                    RecentItemContents.Append(dtItems.Rows[i]["AlternateText"]);
                    RecentItemContents.Append("\"");
                    RecentItemContents.Append("src=\"");
                    RecentItemContents.Append(aspxRootPath);
                    RecentItemContents.Append(imagePath.Replace("uploads", "uploads/Medium"));
                    RecentItemContents.Append("\" orignalPath=\"");
                    RecentItemContents.Append(imagePath.Replace("uploads", "uploads/Medium"));
                    RecentItemContents.Append("\" altImagePath=\"");
                    RecentItemContents.Append(altImagePath.Replace("uploads", "uploads/Medium"));
                    RecentItemContents.Append("\"/></a></div>");
                    RecentItemContents.Append("<div class='cssLatestItemInfo clearfix'>");
                    RecentItemContents.Append("<h2><a href=\"");
                    RecentItemContents.Append(hrefItem);
                    RecentItemContents.Append("\" title=\"");
                    RecentItemContents.Append(dtItems.Rows[i]["Name"]);
                    RecentItemContents.Append("\">");
                    string name = string.Empty;
                    if (dtItems.Rows[i]["Name"].ToString().Length > 50)
                    {
                        name = dtItems.Rows[i]["Name"].ToString().Substring(0, 50);
                        int index = 0;
                        index = name.LastIndexOf(' ');
                        name = name.Substring(0, index);
                        name = name + "...";
                    }
                    else
                    {
                        name = dtItems.Rows[i]["Name"].ToString();
                    }
                    RecentItemContents.Append(name);
                    RecentItemContents.Append("</a></h2>");
                    if (bool.Parse(dtItems.Rows[i]["HidePrice"].ToString()) != true)
                    {
                        if (dtItems.Rows[i]["ListPrice"] != null && dtItems.Rows[i]["ListPrice"].ToString() != string.Empty)
                        {
                            if ((int)dtItems.Rows[i]["ItemTypeID"] == 5)
                            {
                                RecentItemContents.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                                RecentItemContents.Append("<p class=\"cssClassProductRealPrice \">");
                                RecentItemContents.Append(getLocale("Starting At "));
                                RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                RecentItemContents.Append(itemPriceRate);
                                RecentItemContents.Append("</span></p></div></div>");

                            }
                            else
                            {
                                string strAmount = Convert.ToDecimal((dtItems.Rows[i]["ListPrice"])).ToString("N2"); 
                                RecentItemContents.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                                RecentItemContents.Append("<p class=\"cssClassProductOffPrice\">");
                                RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                RecentItemContents.Append(strAmount);
                                RecentItemContents.Append("</span></p><p class=\"cssClassProductRealPrice \">");
                                RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                RecentItemContents.Append(itemPriceRate);
                                RecentItemContents.Append("</span></p></div></div>");
                            }
                        }
                        else
                        {
                            if ((int)dtItems.Rows[i]["ItemTypeID"] == 5)
                            {
                                RecentItemContents.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                                RecentItemContents.Append("<p class=\"cssClassProductRealPrice \">");
                                RecentItemContents.Append(getLocale("Starting At "));
                                RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                RecentItemContents.Append(itemPriceRate);
                                RecentItemContents.Append("</span></p></div></div>");
                            }
                            else
                            {
                                RecentItemContents.Append("<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\">");
                                RecentItemContents.Append("<p class=\"cssClassProductRealPrice \">");
                                RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                RecentItemContents.Append(itemPriceRate);
                                RecentItemContents.Append("</span></p></div></div>");
                            }
                        }
                    }
                    else
                    {
                        RecentItemContents.Append("<div class=\"cssClassProductPriceBox\"></div>");
                    }
                    RecentItemContents.Append("<div class=\"cssClassProductDetail\"><p><a href=\"");
                    RecentItemContents.Append(aspxRedirectPath);
                    RecentItemContents.Append("item/");
                    RecentItemContents.Append(dtItems.Rows[i]["SKU"]);
                    RecentItemContents.Append(pageExtension);
                    RecentItemContents.Append("\">");
                    RecentItemContents.Append(getLocale("Details"));
                    RecentItemContents.Append("</a></p></div>");
                    if (dtItems.Rows[i]["AttributeValues"] != null)
                    {
                        if (dtItems.Rows[i]["AttributeValues"].ToString() != string.Empty)
                        {
                            RecentItemContents.Append("<div class=\"cssGridDyanamicAttr\">");
                            string[] attributeValues = dtItems.Rows[i]["AttributeValues"].ToString().Split(',');
                            foreach (string element in attributeValues)
                            {
                                string[] attributes = element.Split('#');
                                string attributeName = attributes[0];
                                string attributeValue = attributes[1];
                                int inputType = Int32.Parse(attributes[2]);
                                string validationType = attributes[3];
                                RecentItemContents.Append("<div class=\"cssDynamicAttributes\">");
                                RecentItemContents.Append("<span>");
                                RecentItemContents.Append(attributeName);
                                RecentItemContents.Append("</span> :");
                                if (inputType == 7)
                                {
                                    RecentItemContents.Append("<span class=\"cssClassFormatCurrency\">");
                                }
                                else
                                {
                                    RecentItemContents.Append("<span>");
                                }
                                RecentItemContents.Append(attributeValue);
                                RecentItemContents.Append("</span></div>");
                            }
                            RecentItemContents.Append("</div>");
                        }
                    }
                    string itemSKU = dtItems.Rows[i]["SKU"].ToString();
                    string itemName = dtItems.Rows[i]["Name"].ToString();
                    StringBuilder dataContent = new StringBuilder();
                    dataContent.Append("data-class=\"addtoCart\" data-type=\"button\" data-addtocart=\"");
                    dataContent.Append("addtocart");
                    dataContent.Append(dtItems.Rows[i]["ItemID"]);
                    dataContent.Append("\" data-title=\"");
                    dataContent.Append(itemName);
                    dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                    dataContent.Append(dtItems.Rows[i]["ItemID"]);
                    dataContent.Append(",");
                    dataContent.Append(itemPriceValue);
                    dataContent.Append(",'");
                    dataContent.Append(itemSKU);
                    dataContent.Append("',");
                    dataContent.Append(1);
                    dataContent.Append(",'");
                    dataContent.Append(dtItems.Rows[i]["IsCostVariantItem"]);
                    dataContent.Append("',this);\"");
                    RecentItemContents.Append("<div class=\"cssClassTMar20 clearfix\">");
                    if (AllowAddToCart.ToLower() == "true")
                    {
                        if (AllowOutStockPurchase.ToLower() == "false")
                        {
                            if (bool.Parse(dtItems.Rows[i]["IsOutOfStock"].ToString()) == true)
                            {

                                RecentItemContents.Append("<div class=\"cssClassAddtoCard\"><div class=\"sfButtonwrapper cssClassOutOfStock\"  data-ItemTypeID=\"");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemTypeID"]);
                                RecentItemContents.Append("\" data-ItemID=\"");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                                RecentItemContents.Append("\" ");
                                RecentItemContents.Append(dataContent);
                                RecentItemContents.Append(">");
                                RecentItemContents.Append("<button type=\"button\"><span>");
                                RecentItemContents.Append(getLocale("Out Of Stock"));
                                RecentItemContents.Append("</span></button></div></div>");
                            }
                            else
                            {
                                RecentItemContents.Append("<div class=\"cssClassAddtoCard\"><div data-ItemTypeID=\"");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemTypeID"]);
                                RecentItemContents.Append("\" data-ItemID=\"");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                                RecentItemContents.Append("\" ");
                                RecentItemContents.Append(dataContent);
                                RecentItemContents.Append(" class=\"sfButtonwrapper\">");
                                RecentItemContents.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                                RecentItemContents.Append("addtocart=\"");
                                RecentItemContents.Append("addtocart");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                                RecentItemContents.Append("\" title=\"");
                                RecentItemContents.Append(itemName);
                                RecentItemContents.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                                RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                                RecentItemContents.Append(",");
                                RecentItemContents.Append(itemPriceValue);
                                RecentItemContents.Append(",'");
                                RecentItemContents.Append(itemSKU);
                                RecentItemContents.Append("',");
                                RecentItemContents.Append(1);
                                RecentItemContents.Append(",'");
                                RecentItemContents.Append(dtItems.Rows[i]["IsCostVariantItem"]);
                                RecentItemContents.Append("',this);\">");
                                RecentItemContents.Append(getLocale("Cart +"));
                                RecentItemContents.Append("</button></label></div></div>");
                            }
                        }
                        else
                        {
                            RecentItemContents.Append("<div class=\"cssClassAddtoCard\"><div data-ItemTypeID=\"");
                            RecentItemContents.Append(dtItems.Rows[i]["ItemTypeID"]);
                            RecentItemContents.Append("\" data-ItemID=\"");
                            RecentItemContents.Append(dtItems.Rows[i]["ItemID "]);
                            RecentItemContents.Append("\"");
                            RecentItemContents.Append(dataContent);
                            RecentItemContents.Append(" class=\"sfButtonwrapper\">");
                            RecentItemContents.Append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\"");
                            RecentItemContents.Append("addtocart=\"");
                            RecentItemContents.Append("addtocart");
                            RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                            RecentItemContents.Append("\" title=\"");
                            RecentItemContents.Append(itemName);
                            RecentItemContents.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                            RecentItemContents.Append(",");
                            RecentItemContents.Append(itemPriceValue);
                            RecentItemContents.Append(",'");
                            RecentItemContents.Append(itemSKU);
                            RecentItemContents.Append("',");
                            RecentItemContents.Append(1);
                            RecentItemContents.Append(",'");
                            RecentItemContents.Append(dtItems.Rows[i]["IsCostVariantItem"]);
                            RecentItemContents.Append("',this);\">");
                            RecentItemContents.Append(getLocale("Cart +"));
                            RecentItemContents.Append("</button></label></div></div>");

                        }
                    }
                    if (GetCustomerID > 0 && GetUsername.ToLower() != "anonymoususer")
                    {
                        RecentItemContents.Append("<div class=\"cssClassWishListButton\">");
                        RecentItemContents.Append("<label class='i-wishlist cssWishListLabel cssClassDarkBtn'><button type=\"button\" id=\"addWishList\" onclick=AspxCommerce.RootFunction.CheckWishListUniqueness(");
                        RecentItemContents.Append(dtItems.Rows[i]["ItemID"]);
                        RecentItemContents.Append(",'");
                        RecentItemContents.Append(dtItems.Rows[i]["SKU"]);
                        RecentItemContents.Append("',this);><span>");
                        RecentItemContents.Append(getLocale("Wishlist+"));
                        RecentItemContents.Append("</span></button></label></div>");
                    }
                    else
                    {
                        RecentItemContents.Append("<div class=\"cssClassWishListButton\">");
                        RecentItemContents.Append("<label class='i-wishlist cssWishListLabel cssClassDarkBtn'><button type=\"button\" id=\"addWishList\" onclick=\"AspxCommerce.RootFunction.Login();\">");
                        RecentItemContents.Append("<span>");
                        RecentItemContents.Append(getLocale("Wishlist+"));
                        RecentItemContents.Append("</span></button></label></div>");
                    }

                    RecentItemContents.Append("</div></div>");
                    RecentItemContents.Append("</div></div>");
                    RecentItemContents.Append("</div>");
                }
            }

            else
            {
                RecentItemContents.Append("<span class=\"cssClassNotFound\">");
                RecentItemContents.Append(getLocale("This store has no items listed yet!"));
                RecentItemContents.Append("</span>");
            }
            tblRecentItems.InnerHtml = RecentItemContents.ToString();
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
