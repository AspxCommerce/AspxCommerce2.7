using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.SpecialItems;
using System.Collections;
using System.Text;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxSpecials_SpecialItemsViewAll : BaseAdministrationUserControl
{
    public string SessionCode = string.Empty;
    public string AllowOutStockPurchase, NoImageCategoryDetailPath, SortByOptions, SortByOptionDefault;
    private AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    public int ArrayLength = 0;
    public int RowTotal = 0;
    private Hashtable hst = null;
    private const int limit = 8;
    private const int offset = 1;
    private int sortBy = 1;
    public string VarFunction = string.Empty;
    public string ServiceItemDetailPage = string.Empty;
    public string SpecialItemModulePath = string.Empty;
    public bool EnableSpecialItems = true;
    public int NoOfSpecialItems = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.UserName = GetUsername;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        SpecialItemModulePath = ResolveUrl((this.AppRelativeTemplateSourceDirectory));
        if (HttpContext.Current.Session.SessionID != null)
        {
            SessionCode = HttpContext.Current.Session.SessionID.ToString();
        }
        StoreSettingConfig ssc = new StoreSettingConfig();
        AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, GetStoreID, GetPortalID, GetCurrentCultureName);
        NoImageCategoryDetailPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, GetStoreID, GetPortalID, GetCurrentCultureName);
              SortByOptions = ssc.GetStoreSettingsByKey(StoreSetting.SortByOptions, GetStoreID, GetPortalID, GetCurrentCultureName);
        SortByOptionDefault = ssc.GetStoreSettingsByKey(StoreSetting.SortByOptionsDefault, GetStoreID, GetPortalID, GetCurrentCultureName);
        sortBy = Convert.ToInt32(SortByOptionDefault);      
        if (!IsPostBack)
        {
            IncludeCss("AspxSpecialItemsViewDetailsCss", "/Templates/" + TemplateName + "/css/ToolTip/ToolTip.css",
                "/Modules/AspxCommerce/AspxSpecialsItems/css/SpecialItems.css");
            IncludeJs("AspxSpecialItemsViewDetailsJs", "/js/encoder.js", "/Modules/AspxCommerce/AspxSpecialsItems/js/SpecialItemsViewAll.js", "/js/SageFrameCorejs/ItemViewList.js", "/js/jquery.tipsy.js", "/js/FancyDropDown/itemFancyDropdown.js", "/js/Paging/jquery.pagination.js");
            SortByList();

        }
        IncludeLanguageJS();
    }

    private void SortByList()
    {
        StringBuilder itemViewListSortByBdl = new StringBuilder();
        itemViewListSortByBdl.Append("<span class=\"sfLocale\">Sort by:</span><select id=\"ddlSortItemDetailBy\" class=\"sfListmenu\">");
        SortByOptions = SortByOptions.TrimEnd(',');
        if (SortByOptions != "")
        {
            foreach (string sortByOpt in SortByOptions.Split(','))
            {
                string[] sortByOpt1 = sortByOpt.Split('#');
                if (sortByOpt1[0] == SortByOptionDefault)
                {
                    itemViewListSortByBdl.Append("<option selected=\"selected\" data-html-text=\"" + sortByOpt1[1] + "\" value=" +
                                             sortByOpt1[0] + ">" + sortByOpt1[1] + "</option>");
                }
                else
                {
                    itemViewListSortByBdl.Append("<option data-html-text=\"" + sortByOpt1[1] + "\" value=" +
                                             sortByOpt1[0] + ">" + sortByOpt1[1] + "</option>");
                }

            }

        }
        itemViewListSortByBdl.Append("</select>");
        ltrItemViewDetailSortBy.Text = itemViewListSortByBdl.ToString();

        if (Request.QueryString["id"] == "special")
        {
            LoadAllSpecialItems();
        }
    }  

    private void LoadAllSpecialItems()
    {
        VarFunction = "LoadAllSpecialItems";      
        SpecialItemsController sic = new SpecialItemsController();
        List<SpecialItemsInfo> lstSpeDetail = sic.GetAllSpecialItems(offset, limit, aspxCommonObj, sortBy, RowTotal);
        BindAllItems(lstSpeDetail);
    }
    
    private void BindAllItems<T>(List<T> lst)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder itemVLstStringBld = new StringBuilder();
        ArrayLength = lst.Count;
        if (lst != null && lst.Count > 0)
        {
            itemVLstStringBld.Append("<div class=\'clearfix\'>");
            foreach (T value in lst)
            {
                string SKU = "", imagePath = "", alternateText = "", name = "", isFeatured = "", isSpecial = "";
                decimal price = 0;
                string costVariants = "";
                bool isOutOfStock = false;
                int itemID = 0, itemTypeID = 1;

                var prop0 = value.GetType().GetProperty("RowTotal");
                RowTotal = (int)prop0.GetValue(value, null);

                var prop1 = value.GetType().GetProperty("SKU");
                if (prop1 != null)
                {
                    SKU = (string)Convert.ChangeType(prop1.GetValue(value, null), prop1.PropertyType);
                }
                var prop2 = value.GetType().GetProperty("ImagePath");
                if (prop2 != null)
                {
                    imagePath = (string)Convert.ChangeType(prop2.GetValue(value, null), prop2.PropertyType);
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(imagePath, ImageType.Medium, ImageCategoryType.Item, aspxCommonObj);
                }
                var prop3 = value.GetType().GetProperty("AlternateText");
                if (prop3 != null)
                {
                    alternateText = (string)Convert.ChangeType(prop3.GetValue(value, null), prop3.PropertyType);
                }
                var prop4 = value.GetType().GetProperty("Name");
                if (prop4 != null)
                {
                    name = (string)Convert.ChangeType(prop4.GetValue(value, null), prop4.PropertyType);
                }
                var prop5 = value.GetType().GetProperty("IsFeatured");
                if (prop5 != null)
                {
                    isFeatured = (string)Convert.ChangeType(prop5.GetValue(value, null), prop5.PropertyType);
                }
                var prop6 = value.GetType().GetProperty("IsSpecial");
                if (prop6 != null)
                {
                    isSpecial = (string)Convert.ChangeType(prop6.GetValue(value, null), prop6.PropertyType);
                }
                var prop7 = value.GetType().GetProperty("IsOutOfStock");

                if (prop7 != null)
                {
                    var val = prop7.GetValue(value, null);
                    if (val != null)
                        isOutOfStock = (bool)val;
                    else
                    {
                        isOutOfStock = false;
                    }
                }
                var prop8 = value.GetType().GetProperty("ItemID");
                if (prop8 != null)
                {
                    var val = prop8.GetValue(value, null);
                    if (val != null)
                        itemID = (int)val;
                }
                var prop9 = value.GetType().GetProperty("Price");
                if (prop9 != null)
                {
                    price = Convert.ToDecimal(prop9.GetValue(value, null));
                }
                var prop10 = value.GetType().GetProperty("CostVariants");
                if (prop10 != null)
                {
                    costVariants = (string)Convert.ChangeType(prop10.GetValue(value, null), prop10.PropertyType);
                }

                var prop11 = value.GetType().GetProperty("ItemTypeID");
                if (prop11 != null)
                {
                    var val = prop11.GetValue(value, null);
                    if (val != null)
                        itemTypeID = (int)val;
                }

                var hrefItem = "";
                if (itemTypeID == 4)
                {
                    hrefItem = aspxRedirectPath + "Service-Item-Details" + pageExtension + "?id=" + itemID;
                }
                else
                {
                    hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(SKU) + pageExtension;
                }
                if (string.IsNullOrEmpty(imagePath))
                {
                    imagePath = NoImageCategoryDetailPath;
                }
                else
                {
                    imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + imagePath;
                }
                if (string.IsNullOrEmpty(alternateText))
                {
                    alternateText = name;
                }
                string imageSize = "Medium";
                if (isFeatured == "Yes" || isSpecial == "True")
                {
                    itemVLstStringBld.Append("<div id=\"product_" + itemID + "\" class=\"classInfo\">");
                    itemVLstStringBld.Append("<div  id=\"productImageWrapID_" + itemID + "\" class=\"itemImageClass\">");
                    itemVLstStringBld.Append("<a href=\"" + hrefItem + "\">");
                    itemVLstStringBld.Append("<img class=\"lazy\"  alt=\"" + alternateText + "\"  title=\"" +
                                             alternateText + "\" src=\"" + aspxRootPath +
                                             imagePath.Replace("uploads", "uploads/" + imageSize + "") + " \" />");
                    itemVLstStringBld.Append("</a>");
                    itemVLstStringBld.Append(imageSize == "Small"
                                                 ? "<div class=\"classIsFeatureSmall\"></div>"
                                                 : "<div class=\"classIsFeatureMedium\"></div>");
                    if (isSpecial == "Yes" || isSpecial == "True")
                    {
                        itemVLstStringBld.Append(imageSize == "Small"
                                                     ? "<div class=\"classIsSpecialSmall\"></div>"
                                                     : "<div class=\"classIsSpecialMedium\"></div>");
                    }
                    itemVLstStringBld.Append("</div>");
                }
                else
                {
                    itemVLstStringBld.Append("<div id=\"product_" + itemID + "\" class=\"classInfo\">");
                    itemVLstStringBld.Append("<div  id=\"productImageWrapID_" + itemID + "\" class=\"itemImageClass\">");
                    itemVLstStringBld.Append("<a href=\"" + hrefItem + "\">");
                    itemVLstStringBld.Append("<img  alt=\"" + alternateText + "\"  title=\"" +
                                             alternateText + "\" src=\"" + aspxRootPath +
                                             imagePath.Replace("uploads", "uploads/" + imageSize + "") + " \" />");
                    itemVLstStringBld.Append("</a>");
                    if (isSpecial == "Yes" || isSpecial == "True")
                    {
                        itemVLstStringBld.Append(imageSize == "Small"
                                                     ? "<div class=\"classIsSpecialSmall\"></div>"
                                                     : "<div class=\"classIsSpecialMedium\"></div>");
                    }
                    itemVLstStringBld.Append("</div>");
                }
                itemVLstStringBld.Append("<div class=\"itemInfoClass\"><ul>");
                itemVLstStringBld.Append("<li>" + name + "</li>");
                itemVLstStringBld.Append("<div class=\"cssClassProductPrice\"></div>");
                itemVLstStringBld.Append("<li class=\"cssClassProductRealPrice \">");
                if (itemTypeID == 5)
                {
                    itemVLstStringBld.Append(getLocale("Starting At "));
                    itemVLstStringBld.Append("<span id=\"spanPrice_" + itemID + "\" class=\"cssClassFormatCurrency\">" +
                                          Math.Round((price), 2).ToString("N2") + "</span>");
                }
                else
                {
                    itemVLstStringBld.Append("<span id=\"spanPrice_" + itemID + "\" class=\"cssClassFormatCurrency\">" +
                                       Math.Round((price), 2).ToString("N2") + "</span>");
                }
              
                itemVLstStringBld.Append("<input type=\"hidden\"  id=\"hdnPrice_" + itemID +
                                         "\" class=\"cssClassFormatCurrency\">");
                itemVLstStringBld.Append("</li>");
                itemVLstStringBld.Append("</ul>");

                StringBuilder dataContent = new StringBuilder();
                dataContent.Append("data-class=\"addtoCart\" data-ItemTypeID=\"" + itemTypeID + "\" data-ItemID=\"" + itemID + "\" data-type=\"button\" data-addtocart=\"");
                dataContent.Append("addtocart");
                dataContent.Append(itemID);
                dataContent.Append("\" data-title=\"");
                dataContent.Append(name);
                dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                dataContent.Append(itemID);
                dataContent.Append(",");
                dataContent.Append(price);
                dataContent.Append(",'");
                dataContent.Append(SKU);
                dataContent.Append("',");
                dataContent.Append(1);
                dataContent.Append(",'");
                dataContent.Append(costVariants);
                dataContent.Append("',this);\"");

                if (AllowOutStockPurchase.ToLower() == "false")
                {
                    if (isOutOfStock)
                    {
                        itemVLstStringBld.Append("<div "+dataContent+" class=\"sfButtonwrapper cssClassOutOfStock\">");
                        itemVLstStringBld.Append("<button type=\"button\"><span>");
                        itemVLstStringBld.Append(getLocale("Out Of Stock"));
                        itemVLstStringBld.Append("</span></button></div>");

                    }
                    else
                    {
                        if (itemTypeID != 4)
                        {
                            itemVLstStringBld.Append("<div " + dataContent + " class=\"sfButtonwrapper\">");
                            itemVLstStringBld.Append("<label class=\"i-cart cssClassGreenBtn\"><button type=\"button\" class=\"addtoCart\" ");
                            itemVLstStringBld.Append("addtocart=\"");
                            itemVLstStringBld.Append("addtocart");
                            itemVLstStringBld.Append(itemID);
                            itemVLstStringBld.Append("\" title=\"");
                            itemVLstStringBld.Append(name);

                            itemVLstStringBld.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            itemVLstStringBld.Append(itemID + ",");
                            itemVLstStringBld.Append(price + ",");
                            itemVLstStringBld.Append("'" + SKU + "'" + "," + 1);
                            itemVLstStringBld.Append(",'");
                            itemVLstStringBld.Append(costVariants);
                            itemVLstStringBld.Append("',this);\">");
                            itemVLstStringBld.Append(getLocale("Cart +"));
                            itemVLstStringBld.Append("</button></label></div>");
                        }
                    }
                }
                else
                {
                    if (itemTypeID != 4)
                    {
                        itemVLstStringBld.Append("<div " + dataContent + " class=\"sfButtonwrapper\">");
                        itemVLstStringBld.Append("<label class=\"cssClassGreenBtn i-cart cssClassCartLabel\"><button type=\"button\" class=\"addtoCart\" ");
                        itemVLstStringBld.Append("addtocart=\"");
                        itemVLstStringBld.Append("addtocart");
                        itemVLstStringBld.Append(itemID);
                        itemVLstStringBld.Append("\" title=\"");
                        itemVLstStringBld.Append(name);

                        itemVLstStringBld.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                        itemVLstStringBld.Append(itemID + ",");
                        itemVLstStringBld.Append(price + ",");
                        itemVLstStringBld.Append("'" + SKU + "'" + "," + 1);
                        itemVLstStringBld.Append(",'");
                        itemVLstStringBld.Append(costVariants);
                        itemVLstStringBld.Append("',this);\">");
                        itemVLstStringBld.Append(getLocale("Cart +"));
                        itemVLstStringBld.Append("</button></label></div>");
                    }
                }

                itemVLstStringBld.Append("<div class=\"classButtons\">");
                itemVLstStringBld.Append("<div class=\"classWishlist\">");

                itemVLstStringBld.Append("<div class=\"cssClassWishListButton\">");
                itemVLstStringBld.Append("<input type=\"hidden\" name='itemwish' value=");
                itemVLstStringBld.Append(itemID);
                itemVLstStringBld.Append(",'");
                itemVLstStringBld.Append(SKU);
                itemVLstStringBld.Append("',this  />");
                itemVLstStringBld.Append("</div>");
                itemVLstStringBld.Append("</div>");
                itemVLstStringBld.Append("</div>");

              /*  itemVLstStringBld.Append("<div class=\"classViewDetails\">");
                itemVLstStringBld.Append("<a href=\"" + hrefItem + "\"><span>" + getLocale("View Details") +
                                         "</span></a></div>");*/
                itemVLstStringBld.Append("</div>");
                itemVLstStringBld.Append("</div>");
            }
            
            itemVLstStringBld.Append("</div>");
                   }
        else
        {
            itemVLstStringBld.Append("<span class=\"cssClassNotFound\"><b>" +
                                     getLocale("This store has no items listed yet!") + "</b></span>");
        }
        ltrItemViewDetail.Text = itemVLstStringBld.ToString();
    }

    private string GetStringScript(string codeToRun)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){ " +
                      codeToRun + "});</script>");
        return script.ToString();
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