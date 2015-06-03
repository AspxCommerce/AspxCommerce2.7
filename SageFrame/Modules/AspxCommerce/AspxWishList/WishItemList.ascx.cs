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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using System.Web.Security;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;
using AspxCommerce.WishItem;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class WishItemList : BaseAdministrationUserControl
{
    #region variables
    private int StoreID, PortalID, CustomerID;
    private string UserName, CultureName;
    private string pageExtension, templateName, resolveUrl, sortByOptions;

    public int UserModuleIDWishList, RowTotal = 0, ArrayLength;
    public string UserFullName;
    public string SessionCode = string.Empty;
    public string UserEmailWishList = string.Empty, ServicePath = string.Empty;
    public string NoImageWishList, AllowAddToCart, AllowOutStockPurchase;
    public bool ShowImageInWishlist = true;
    #endregion

    protected void page_init(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            ServicePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
            templateName = TemplateName;
            IncludeCss("WishItemListCSS", "/Templates/" + templateName + "/css/MessageBox/style.css", "/Templates/" + templateName + "/css/PopUp/style.css", "/Templates/" + templateName + "/css/ToolTip/tooltip.css", "/Modules/AspxCommerce/AspxWishList/css/WishItems.css");
            IncludeJs("WishItemListJS", "/Modules/AspxCommerce/AspxWishList/js/WishItemList.js", "/js/Paging/jquery.pagination.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/js/PopUp/custom.js", "/js/jquery.tipsy.js", "/js/encoder.js");
            if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
            {
                MembershipController member = new MembershipController();
                UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
                UserFullName = userDetail.FirstName + " " + userDetail.LastName;
                UserEmailWishList = userDetail.Email;
                if (UserEmailWishList.Contains(","))
                {
                    string[] commaSeparator = { "," };
                    string[] value = UserEmailWishList.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                    UserEmailWishList = value[0];
                }
                if (!string.IsNullOrEmpty(SageUserModuleID))
                {
                    UserModuleIDWishList = int.Parse(SageUserModuleID);
                }
                if (!IsPostBack)
                {
                    resolveUrl = ResolveUrl("~/"); 
                }
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamFour(StoreSetting.ShowAddToCartButton, StoreSetting.DefaultProductImageURL, StoreSetting.AllowOutStockPurchase, StoreSetting.SortByOptions, out AllowAddToCart, out NoImageWishList, out AllowOutStockPurchase, out sortByOptions, StoreID, PortalID, CultureName);
            }
            else
            {
                SageFrameConfig pagebase = new SageFrameConfig();
                pageExtension = SageFrameSettingKeys.PageExtension;
                if (GetPortalID > 1)
                {
                    Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)) + pageExtension + "?ReturnUrl=" + Request.Url.ToString(), false);
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)) + pageExtension + "?ReturnUrl=" + Request.Url.ToString(), false);
                }
            }
            IncludeLanguageJS();
            BindWishList(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;
    private void SortByList()
    {
        string[] sortByOption = sortByOptions.Substring(0, sortByOptions.LastIndexOf(',')).Split(',');
        StringBuilder wishListSortByBdl = new StringBuilder();
        wishListSortByBdl.Append("<span>");
        wishListSortByBdl.Append(getLocale("Sort by:"));
        wishListSortByBdl.Append("</span><select id=\"ddlWishListSortBy\" class=\"sfListmenu\">");
        string[] sortByOptionsplit;
        foreach (string sortByOption1 in sortByOption)
        {
            sortByOptionsplit = sortByOption1.Split('#');
            wishListSortByBdl.Append("<option data-html-text=\"");
            wishListSortByBdl.Append(sortByOptionsplit[1]);
            wishListSortByBdl.Append("\" value=");
            wishListSortByBdl.Append(sortByOptionsplit[0]);
            wishListSortByBdl.Append(">");
            wishListSortByBdl.Append(sortByOptionsplit[1]);
            wishListSortByBdl.Append("</option>");
        }
        wishListSortByBdl.Append("</select>");
        ltrWishListSortBy.Text = wishListSortByBdl.ToString();
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

    #region BindWishList
    private void BindWishList(AspxCommonInfo aspxCommonObj)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string pageExtension = SageFrameSettingKeys.PageExtension;
        hst = AppLocalized.getLocale(modulePath);
        string aspxTemplateFolderPath = resolveUrl + "Templates/" + TemplateName;
        string aspxRootPath = resolveUrl;
        string cssClass;

        DataSet ds = WishItemController.GetWishItemListDataSet(aspxCommonObj);
        #region WishList Setting
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        {
            DataTable dtWishListSetting = ds.Tables[0];
            if (dtWishListSetting.Rows.Count > 0)
            {
                ShowImageInWishlist = bool.Parse(dtWishListSetting.Rows[0]["IsEnableImageInWishlist"].ToString());
            }
        }
        #endregion

        #region WishList Content Html
        StringBuilder wishListItems = new StringBuilder();

        DataTable dtWishItems = ds.Tables[1];
        RowTotal = dtWishItems.Rows.Count;
        ArrayLength = (int)dtWishItems.Rows[0]["RowTotal"];

        #region Variables
        string imagePath = string.Empty, imagePathColumn = string.Empty, alternateText = string.Empty;
        string WishDate = string.Empty, cartUrl = string.Empty, href = string.Empty;

        int i = 0;

        StringBuilder dataContent = new StringBuilder();
        StringBuilder currencyContent;
        #endregion
        if (dtWishItems != null && RowTotal > 0)
        {
            wishListItems.Append("<thead><tr class=\"cssClassCommonCenterBoxTableHeading\"><th class=\"cssClassWishItemChkbox\"> <input type=\"checkbox\" id=\"chkHeading\"/></th>");
            if (ShowImageInWishlist)
                wishListItems.Append("<th class=\"cssClassWishItemImg\"> <label class=\"sfLocale\">Image</label></th>");
            wishListItems.Append("<th class=\"cssClassWishItemDetails\"><label id=\"lblItem\" class=\"sfLocale\">Item Details and Comment</label></th><th class=\"row-variants\"><label id=\"lblVariant\" class=\"sfLocale\">Variants</label></th>");
            if (bool.Parse(AllowAddToCart))
                wishListItems.Append("<th class=\"cssClassAddToCart\"><span id=\"lblAddToCart\" class=\"sfLocale\">Add To Cart</span></th>");
            wishListItems.Append("<th class=\"cssClassDelete\"><span class=\"\">Action</span></th></tr></thead><tbody>");

            foreach (DataRow dr in dtWishItems.Rows)
            {
                imagePath = string.Empty; imagePathColumn = string.Empty; alternateText = string.Empty;
                WishDate = string.Empty; cartUrl = string.Empty; href = string.Empty;
                href = "";
                cartUrl = "";

                i++;

                imagePathColumn = dr["ImagePath"].ToString();
                alternateText = dr["AlternateText"].ToString();
                imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + imagePathColumn;
                if (string.IsNullOrEmpty(imagePathColumn))
                    imagePath = NoImageWishList;
                else if (string.IsNullOrEmpty(alternateText))
                {
                    alternateText = dr["ItemName"].ToString();
                    InterceptImageController.ImageBuilder(imagePathColumn, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                }
                else
                {
                    InterceptImageController.ImageBuilder(imagePathColumn, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                }
                WishDate = Convert.ToDateTime(dr["WishDate"]).ToShortDateString();
                var itemSKU = new JavaScriptSerializer().Serialize(dr["SKU"]);
                var costVariant = new JavaScriptSerializer().Serialize(dr["CostVariantValueIDs"]);
                if (string.IsNullOrEmpty(dr["CostVariantValueIDs"].ToString()))
                {
                    cartUrl = "#";
                    href = aspxRedirectPath + "item/" + dr["SKU"].ToString() + pageExtension;
                }
                else
                {
                    cartUrl = aspxRedirectPath + "item/" + dr["SKU"].ToString() + pageExtension + "?varId=" + dr["CostVariantValueIDs"];
                    href = aspxRedirectPath + "item/" + dr["SKU"] + pageExtension + "?varId=" + dr["CostVariantValueIDs"];
                }

                #region DataContent StringBuilder
                dataContent = new StringBuilder();
                dataContent.Append("data-class=\"addtoCart\" data-type=\"button\" data-addtocart=\"");
                dataContent.Append("addtocart");
                dataContent.Append(dr["ItemID"]);
                dataContent.Append("\" data-title=\"");
                dataContent.Append(dr["ItemName"]);
                dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                dataContent.Append(dr["ItemID"]);
                dataContent.Append(",");
                dataContent.Append(dr["Price"]);
                dataContent.Append(",'");
                dataContent.Append(dr["SKU"]);
                dataContent.Append("',");
                dataContent.Append(1);
                dataContent.Append(",'");
                dataContent.Append(dr["IsCostVariantItem"]);
                dataContent.Append("',this);\"");
                #endregion

                if (i % 2 == 0)
                {
                    cssClass = "sfEven";
                    wishListItems.Append("<tr class=\"sfEven\" id=\"tr_");
                }
                else
                {
                    cssClass = "sfOdd";
                    wishListItems.Append("<tr class=\"sfOdd\" id=\"tr_");
                }
                #region Table Row
                wishListItems.Append("<tr class=\"");
                wishListItems.Append(cssClass);
                wishListItems.Append("\" id=\"tr_");
                wishListItems.Append(dr["ItemID"]);
                wishListItems.Append("\"><td class=\"cssClassWishItemChkbox\"><input type=\"checkbox\" id=\"");
                wishListItems.Append(dr["WishItemID"]);
                wishListItems.Append("\" class=\"cssClassWishItem\"/></td>");
                if (ShowImageInWishlist)
                {
                    wishListItems.Append("<td class=\"cssClassWishItemImg\">");
                    wishListItems.Append("<div class=\"cssClassImage\">");
                    wishListItems.Append("<img src=\"");
                    wishListItems.Append(aspxRootPath);
                    wishListItems.Append(imagePath.Replace("uploads", "uploads/Small"));
                    wishListItems.Append("\" alt=\"");
                    wishListItems.Append(alternateText);
                    wishListItems.Append("\" title=\"");
                    wishListItems.Append(alternateText);
                    wishListItems.Append("\"/>");
                    wishListItems.Append("</div></td>");
                }
                wishListItems.Append("<td class=\"cssClassWishItemDetails\"><a href=\"");
                wishListItems.Append(href);
                wishListItems.Append("\">");
                wishListItems.Append(dr["ItemName"]);
                wishListItems.Append("</a>");
                wishListItems.Append("<div class=\"cssClassWishDate\"><i class='i-calender'></i>");
                wishListItems.Append(WishDate);
                wishListItems.Append("</div>");
                wishListItems.Append("<div class=\"cssClassWishComment\"><textarea maxlength=\"600\" onkeyup=\"WishItem.ismaxlength(this);\"");
                wishListItems.Append(" id=\"comment_");
                wishListItems.Append(dr["WishItemID"]);
                wishListItems.Append("\" class=\"comment\">");
                wishListItems.Append(dr["Comment"]);
                wishListItems.Append("</textarea></div></td>");
                wishListItems.Append("<td><input type=\"hidden\" name=\"hdnCostVariandValueIDS\" value=");
                wishListItems.Append(costVariant);
                wishListItems.Append("/>");
                wishListItems.Append("<span>");
                wishListItems.Append(dr["ItemCostVariantValue"]);
                wishListItems.Append("</span></td>");
                if (bool.Parse(AllowAddToCart))
                {
                    wishListItems.Append("<td class=\"cssClassWishToCart\">");
                    currencyContent = new StringBuilder();
                    currencyContent.Append("<span class=\"cssClassPrice cssClassFormatCurrency\">");
                    currencyContent.Append(decimal.Parse(dr["Price"].ToString()).ToString("N2"));
                    currencyContent.Append("</span>");
                    if ((int)dr["ItemTypeID"] == 5)
                    {
                        wishListItems.Append("<p class=\"cssClassGroupPriceWrapper\">");
                        wishListItems.Append(getLocale("Starting At "));
                        wishListItems.Append(currencyContent);
                        wishListItems.Append("</p>");
                    }
                    else
                    {
                        wishListItems.Append(currencyContent);
                    }
                    wishListItems.Append("<div data-ItemTypeID=\"");
                    wishListItems.Append(dr["ItemTypeID"]);
                    wishListItems.Append("\" data-ItemID=\"");
                    wishListItems.Append(dr["ItemID"]);
                    wishListItems.Append("\"");
                    wishListItems.Append(dataContent);
                    wishListItems.Append(" class=\"sfButtonwrapper");
                    #region Allow Add To Cart
                    if (!bool.Parse(AllowOutStockPurchase))
                    {  
                        #region Allow Out Stock Purchase True
                        if (dr["IsOutOfStock"] != null && (bool)dr["IsOutOfStock"])
                        {
                            #region Is Out Of Stock True
                            wishListItems.Append(" cssClassOutOfStock\">");
                            wishListItems.Append("<span class=\"cssClassOutStock\">");
                            wishListItems.Append(getLocale("Out Of Stock"));
                            wishListItems.Append("</span>");
                            #endregion
                        }
                        else
                        {
                            #region Is Out Of Stock False
                            wishListItems.Append("\">");
                            wishListItems.Append("<label class=\"i-cart cssClassCartLabel cssClassGreenBtn\"><button type=\"button\" class=\"addtoCart\"");
                            wishListItems.Append("addtocart=\"");
                            wishListItems.Append("addtocart");
                            wishListItems.Append(dr["ItemID"]);
                            wishListItems.Append("\" title=\"");
                            wishListItems.Append(dr["ItemName"]);
                            wishListItems.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                            wishListItems.Append(dr["ItemID"]);
                            wishListItems.Append(",");
                            wishListItems.Append(dr["Price"]);
                            wishListItems.Append(",");
                            wishListItems.Append("'");
                            wishListItems.Append(dr["SKU"]);
                            wishListItems.Append("'");
                            wishListItems.Append(",");
                            wishListItems.Append(1);
                            wishListItems.Append(",'");
                            wishListItems.Append(dr["IsCostVariantItem"]);
                            wishListItems.Append("',this);\"><span>");
                            wishListItems.Append(getLocale("Cart +"));
                            wishListItems.Append("</span></button></label>");
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Allow Out Stock Purchase False
                        wishListItems.Append("\">");
                        wishListItems.Append("<label class=\"i-cart cssClassCartLabel cssClassGreenBtn\"><button type=\"button\" class=\"addtoCart\"");
                        wishListItems.Append("addtocart=\"");
                        wishListItems.Append("addtocart");
                        wishListItems.Append(dr["ItemID"]);
                        wishListItems.Append("\" title=\"");
                        wishListItems.Append(dr["ItemName"]);
                        wishListItems.Append("\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                        wishListItems.Append(dr["ItemID"]);
                        wishListItems.Append(",");
                        wishListItems.Append(dr["Price"]);
                        wishListItems.Append(",");
                        wishListItems.Append("'");
                        wishListItems.Append(dr["SKU"]);
                        wishListItems.Append("'");
                        wishListItems.Append(",");
                        wishListItems.Append(1);
                        wishListItems.Append(",'");
                        wishListItems.Append(dr["IsCostVariantItem"]);
                        wishListItems.Append("',this);\"><span>");
                        wishListItems.Append(getLocale("Cart +"));
                        wishListItems.Append("</span></button></label>");
                        #endregion
                    }
                    wishListItems.Append("</div></td>");
                    #endregion
                }
                wishListItems.Append("<td class=\"cssClassDelete\">");
                wishListItems.Append("<a onclick=\"WishItem.Delete(");
                wishListItems.Append(dr["WishItemID"]);
                wishListItems.Append(")\" title=\"");
                wishListItems.Append(getLocale("Delete"));
                wishListItems.Append("\"><i class='i-delete' original-title=\"");
                wishListItems.Append(getLocale("Delete"));
                wishListItems.Append("\"></i></a>");
                wishListItems.Append("</td></tr>");
                #endregion
            }
            wishListItems.Append("</tbody>");
           /// wishListItems.Append("<script type=\"text/javascript\">$(document).ready(function(){$('.cssClassImage img[title]').tipsy({ gravity: 'n' });});</script>");

            StringBuilder wishLstButtonBdl = new StringBuilder();
            wishLstButtonBdl.Append("<label class='i-wishlist cssClassGreenBtn'><button type=\"button\" id=\"shareWishList\">");
            wishLstButtonBdl.Append("<span class=\"sfLocale\">Share Wishlist</span></button></label>");
            wishLstButtonBdl.Append(
                "<label class='i-update cssClassDarkBtn'><button type=\"button\" id=\"updateWishList\" onclick=\"WishItem.Update();\">");
            wishLstButtonBdl.Append("<span class=\"sfLocale\">Update Selected</span></button></label>");
            wishLstButtonBdl.Append(
                "<label class='i-clear cssClassGreyBtn'><button type=\"button\" id=\"clearWishList\" onclick=\"WishItem.Clear();\">");
            wishLstButtonBdl.Append("<span class=\"sfLocale\">Clear WishList</span></button></label>");
            wishLstButtonBdl.Append("<label class='i-delete cssClassGreenBtn'><button type=\"button\" id=\"btnDeletedMultiple\">");
            wishLstButtonBdl.Append("");
            wishLstButtonBdl.Append("<span class=\"sfLocale\">Delete Selected</span></button></label>");
            wishLstButtonBdl.Append("<label class='i-arrow-right cssClassDarkBtn'><button type=\"button\" id=\"continueInStore\">");
            wishLstButtonBdl.Append("<span class=\"sfLocale\">Continue Shopping</span></button ></label>");

            StringBuilder wishListPaginationBdl = new StringBuilder();
            wishListPaginationBdl.Append("<span class=\"sfLocale\">View Per Page: </span><select id=\"ddlWishListPageSize\" class=\"sfListmenu\"><option value=\"\"></option></select>");

            ltrWishListButon.Text = wishLstButtonBdl.ToString();
            ltrWishListPagination.Text = wishListPaginationBdl.ToString();

            SortByList();
        }
        else
        {
            wishListItems.Append("<tr><td class=\"cssClassNotFound\">");
            wishListItems.Append(getLocale("Your wishlist is empty!"));
            wishListItems.Append("</td></tr>");
        }
        ltrWishList.Text = wishListItems.ToString();
        #endregion
    }
    #endregion
}