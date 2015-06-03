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
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;
using AspxCommerce.WishItem;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxWishItems_WishItems : BaseAdministrationUserControl
{
    public string UserIp;
    public string CountryName = string.Empty;
    public int StoreID, PortalID, NoOfRecentAddedWishItems;
    public string UserName, CultureName;
    public string NoImageWishItemPath, ShowWishedItemImage, WishListURL;
    public bool IsUseFriendlyUrls = true;
    public bool userFriendlyURL = true;
    public string ModuleCollapsible;
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            UserIp = HttpContext.Current.Request.UserHostAddress;
            IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
            ipToCountry.GetCountry(UserIp, out CountryName);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            if (!IsPostBack)
            {

                IncludeJs("WishItems", "/js/DateTime/date.js", "/js/jquery.tipsy.js", "/Modules/AspxCommerce/AspxWishList/js/WishItems.js");
                IncludeCss("WishItemsCss", "/Modules/AspxCommerce/AspxWishList/css/WishItems.css");
                userFriendlyURL = IsUseFriendlyUrls;
                GetWishListItemsSettig(aspxCommonObj);
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamThree(StoreSetting.DefaultProductImageURL, StoreSetting.ShowItemImagesInWishList,StoreSetting.ModuleCollapsible, out NoImageWishItemPath, out ShowWishedItemImage, out ModuleCollapsible, StoreID, PortalID, CultureName);
            }
            if (NoOfRecentAddedWishItems > 0)
            {
                BindMyWishList(aspxCommonObj);
            }
            IncludeLanguageJS();

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    Hashtable hst = null;
    public void BindMyWishList(AspxCommonInfo aspxCommonObj)
    {
        string flagShowAll = "0";
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        WishItemController ctrl = new WishItemController();
        List<WishItemsInfo> lstWishItem = ctrl.GetRecentWishItemList(aspxCommonObj, flagShowAll,
                                                                                       NoOfRecentAddedWishItems);
        StringBuilder recentWishList = new StringBuilder();
        recentWishList.Append("<div class=\"cssClassCommonSideBoxTable wishItem\">"); 
        recentWishList.Append("<table class=\"cssClassMyWishItemTable\" id=\"tblWishItem\" width=\"100%\">");
        recentWishList.Append("<tbody>");

        if (lstWishItem != null && lstWishItem.Count > 0)
        {
            string myWishListLink = ""; 
            string cssClass = string.Empty;
            StringBuilder dataContent;
            string imagePath = string.Empty;

            if (userFriendlyURL)
            {
                myWishListLink = WishListURL + pageExtension;
            }
            else
            {
                myWishListLink = WishListURL;
            }

            for (int i = 0; i < lstWishItem.Count; i++)
            {
                imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + lstWishItem[i].ImagePath;
                if (string.IsNullOrEmpty(lstWishItem[i].ImagePath))
                {
                    imagePath = NoImageWishItemPath;
                }
                else
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(lstWishItem[i].ImagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                }
                if (lstWishItem[i].AlternateText == "")
                {
                    lstWishItem[i].AlternateText = lstWishItem[i].ItemName;
                }
                var href = aspxRedirectPath + "item/" + lstWishItem[i].SKU + pageExtension;
                if (lstWishItem[i].CostVariantValueIDs != "")
                {
                    href += "?varId=" + lstWishItem[i].CostVariantValueIDs;
                }
                dataContent = new StringBuilder();
                dataContent.Append("data-class=\"addtoCart\" data-type=\"button\" data-addtocart=\"");
                dataContent.Append("addtocart");
                dataContent.Append(lstWishItem[i].ItemID);
                dataContent.Append("\" data-title=\"");
                dataContent.Append(lstWishItem[i].ItemName);
                dataContent.Append("\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(");
                dataContent.Append(lstWishItem[i].ItemID);
                dataContent.Append(",");
                dataContent.Append(lstWishItem[i].Price);
                dataContent.Append(",'");
                dataContent.Append(lstWishItem[i].SKU);
                dataContent.Append("',");
                dataContent.Append(1);
                dataContent.Append(",'");
                dataContent.Append(lstWishItem[i].IsCostVariantItem);
                dataContent.Append("',this);\"");
                if (i % 2 == 0)
                {
                    cssClass = "sfEven";
                }
                else
                {
                    cssClass = "sfOdd";
                }
                recentWishList.Append("<tr class=\"");
                recentWishList.Append(cssClass);
                recentWishList.Append("\" id=\"trWishItem_");
                recentWishList.Append(lstWishItem[i].ItemID);
                recentWishList.Append("\"><td class=\"cssClassWishItemDetails\">");
                if (ShowWishedItemImage.ToLower() == "true")
                {
                    recentWishList.Append("<a href =\"");
                    recentWishList.Append(href);
                    recentWishList.Append("\">");
                    recentWishList.Append("<div class=\"cssClassImage\"><img src=\"");
                    recentWishList.Append(aspxRootPath);
                    recentWishList.Append(imagePath.Replace("uploads", "uploads/Small"));
                    recentWishList.Append("\" alt=\"");
                    recentWishList.Append(lstWishItem[i].AlternateText);
                    recentWishList.Append("\" title=\"");
                    recentWishList.Append(lstWishItem[i].AlternateText);
                    recentWishList.Append("\"/></div></a>");
                }
                recentWishList.Append("<a href=\"");
                recentWishList.Append(href);
                recentWishList.Append("\">");
                recentWishList.Append(lstWishItem[i].ItemName);
                if (!string.IsNullOrEmpty(lstWishItem[i].ItemCostVariantValue))
                {
                    recentWishList.Append("(");
                    recentWishList.Append(lstWishItem[i].ItemCostVariantValue);
                    recentWishList.Append(")");
                }
                recentWishList.Append("</a>");
                recentWishList.Append("</br><span class=\"cssClassPrice cssClassFormatCurrency\">");
                recentWishList.Append(decimal.Parse(lstWishItem[i].Price).ToString("N2"));
                recentWishList.Append("</span></td>");
                recentWishList.Append("<td class=\"cssClassDelete\">");
                recentWishList.Append("<img onclick=\"wishItemsFront.DeleteWishListItem(");
                recentWishList.Append(lstWishItem[i].WishItemID);
                recentWishList.Append(")\" src=\"");
                recentWishList.Append(aspxTemplateFolderPath);
                recentWishList.Append("/images/admin/btndelete.png\"/>");
                recentWishList.Append("</td></tr>");
            }
            recentWishList.Append("</tbody>");
            recentWishList.Append("</table>");
            recentWishList.Append("<div class=\"cssClassWishLink\">");
            recentWishList.Append("<a href=\"");
            recentWishList.Append(aspxRedirectPath);
            recentWishList.Append(myWishListLink);
            recentWishList.Append("\" id=\"lnkGoToWishlist\">");
            recentWishList.Append("<span class=\"gowishlist\">");
            recentWishList.Append(getLocale("Go to Wishlist"));
            recentWishList.Append("</span></a>");
            recentWishList.Append("</div></div>");
        }
        else
        {
            recentWishList.Append("<tr><td><span class=\"cssClassNotFound\">");
            recentWishList.Append(getLocale("Your Wishlist is empty!"));
            recentWishList.Append("</span></td></tr>");
            recentWishList.Append("</tbody>");
            recentWishList.Append("</table></div>");
        }
        ltrWishItem.Text = recentWishList.ToString();
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

    private void GetWishListItemsSettig(AspxCommonInfo aspxCommonObj)
    {
        WishItemController wic = new WishItemController();
        WishItemsSettingInfo objWishItemSetting = wic.GetWishItemsSetting(aspxCommonObj);
        if (objWishItemSetting != null)
        {
            NoOfRecentAddedWishItems = objWishItemSetting.NoOfRecentAddedWishItems;
            WishListURL = objWishItemSetting.WishListPageName;
        }
    }
}