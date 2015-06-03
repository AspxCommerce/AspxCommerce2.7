using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxGetTemplates
    {
        public static List<AspxTemplateKeyValue> GetAspxTemplateKeyValue()
        {
            List<AspxTemplateKeyValue> list = new List<AspxTemplateKeyValue>
                          {
                              new AspxTemplateKeyValue
                                  {TemplateKey = "scriptResultGrid", TemplateValue = scriptResultGrid().ToString()},
                              //new AspxTemplateKeyValue
                              //    {TemplateKey = "scriptResultGrid2", TemplateValue = scriptResultGrid2().ToString()},
                              //new AspxTemplateKeyValue
                              //    {TemplateKey = "scriptResultGrid3", TemplateValue = scriptResultGrid3().ToString()},
                              new AspxTemplateKeyValue
                                  {TemplateKey = "scriptResultList", TemplateValue = scriptResultList().ToString()}
                              //new AspxTemplateKeyValue
                              //    {TemplateKey = "scriptCompactList", TemplateValue = scriptCompactList().ToString()},
                              //new AspxTemplateKeyValue
                              //    {
                              //        TemplateKey = "scriptResultListWithoutOptions",
                              //        TemplateValue = scriptResultListWithoutOptions().ToString()
                              //    },
                              //new AspxTemplateKeyValue
                              //    {TemplateKey = "scriptResultProductGrid", TemplateValue = scriptResultProductGrid().ToString()}
                          };
            return list;
        }

        private static StringBuilder scriptResultGrid()
        {
            StringBuilder gridTemp = new StringBuilder();           
            gridTemp.Append("<div class=\"cssClassProductsBox\">");
            gridTemp.Append("<div class=\"cssClassProductsBoxInfo\">");            
            gridTemp.Append("<h3>${sku}</h3>");
            gridTemp.Append("<div class=\"cssClassProductPicture\">");
            gridTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            gridTemp.Append("<img src=\"${imagePath}\" alt=\"${alternateText}\" title=\"${name}\"/></a>");
            gridTemp.Append("</div>");
            gridTemp.Append("<div class=\"cssLatestItemInfo\">");
            gridTemp.Append("<h2><a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\" title=\"${titleName}\">");
            gridTemp.Append("${name}");
            gridTemp.Append("</a></h2>");
            gridTemp.Append("<div class=\"cssClassProductPriceBox\">");
            gridTemp.Append("<div class=\"cssClassProductPrice\">");
            gridTemp.Append("<p class=\"cssClassProductOffPrice\">");
            gridTemp.Append("<span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span></p>");
            gridTemp.Append("<p class=\"cssClassProductRealPrice\">");
            gridTemp.Append("<span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span></p>");
            gridTemp.Append("</div>");
            gridTemp.Append("</div>");
            gridTemp.Append("<div class=\"cssClassProductDetail\">");
            gridTemp.Append("<p>");
            gridTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\" class=\"sfLocale\">" + getLocale("Details") + "</a></p>");
            gridTemp.Append("</div>");
            gridTemp.Append("<div class=\"cssGridDyanamicAttr\">$DynamicAttr</div>");
            gridTemp.Append("<div class=\"cssClassTMar20\">");
            gridTemp.Append("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">");
            gridTemp.Append("<div class=\"sfButtonwrapper\" data-itemid=\"${itemID}\" data-class=\"addtoCart\" data-type=\"button\" data-addtocart=addtocart${itemID} data-title=${titleName} data-onclick=AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);>");
            gridTemp.Append("<label class=\"i-cart cssClassCartLabel cssClassGreenBtn\"><button class=\"addtoCart\" type=\"button\" addtocart=addtocart${itemID} title=${titleName} onclick=AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);>");
            gridTemp.Append("<span class=\"sfLocale\">" + getLocale("Cart +") + "</span></button></label>");
            gridTemp.Append("</div>");
            //gridTemp.Append("</div>");//remove addto cart button from cssClassProductsBoxInfo div 
            gridTemp.Append("</div>");
            gridTemp.Append("<div class=\"cssClassWishListButton\">");
            gridTemp.Append("<label class=\"i-wishlist cssWishListLabel cssClassDarkBtn\"><button type=\"button\" id=\"addWishList\" onclick=AspxCommerce.RootFunction.CheckWishListUniqueness(${itemID},${JSON2.stringify(sku)},this)><span>" + getLocale("Wishlist+") + "</span></button></label></div>");           
            gridTemp.Append("<div class=\"cssClassclear\"></div>");
            gridTemp.Append("<div class=\"cssClassCompareButton\">");
            gridTemp.Append("<input type=\"hidden\" name=\"itemcompare\" value=${itemID},${JSON2.stringify(sku)},this></div>");
            gridTemp.Append("</div></div></div></div>");            
            return gridTemp;
        }

        private static StringBuilder scriptResultGrid2()
        {
            StringBuilder grid2Temp = new StringBuilder();

            grid2Temp.Append("<div>"); // intital extra div wrapper
            grid2Temp.Append("<div class=\"cssClassGrid2Box\">");
            grid2Temp.Append("<div class=\"cssClassGrid2BoxInfo\">");
            grid2Temp.Append("<h2><a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">${name}</a></h2>");
            grid2Temp.Append("<div class=\"cssClassGrid2Picture\">");
            grid2Temp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            grid2Temp.Append("<img alt=\"${alternateText}\" src=\"${imagePath}\" title=\"${name}\"/>");
            grid2Temp.Append("</a>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("<div class=\"cssClassGrid2PriceBox\">");
            grid2Temp.Append("<div class=\"cssClassGrid2Price\">");
            grid2Temp.Append("<p class=\"cssClassGrid2OffPrice\">");
            grid2Temp.Append("<span class=\"cssRegularPrice_${itemID} sfLocale\">" + getLocale("Price :") + " </span><span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span><br/>");
            grid2Temp.Append("<span class=\"cssClassGrid2RealPrice\"><span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span></span>");
            grid2Temp.Append("</p>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">");
            grid2Temp.Append("<div class=\"sfButtonwrapper\">");
            grid2Temp.Append("<button type=\"button\" addtocart=\"addtocart${itemID}\" title=\"${titleName}\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);\">");
            grid2Temp.Append("<span class=\"sfLocale\">" + getLocale("Add to Cart") + "</span></button>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("<div class=\"cssClassclear\">");
            grid2Temp.Append("</div>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("</div>");
            grid2Temp.Append("</div>"); // end of extra wrapper div

            return grid2Temp;
        }

        private static StringBuilder scriptResultGrid3()
        {
            StringBuilder grid3Temp = new StringBuilder();
            grid3Temp.Append("<div>"); // initial extra div wrapper
            grid3Temp.Append("<div class=\"cssClassGrid3Box\">");
            grid3Temp.Append("<div class=\"cssClassGrid3BoxInfo\">");
            grid3Temp.Append("<h2>");
            grid3Temp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">${name}</a></h2>");
            grid3Temp.Append("<div class=\"cssClassGrid3Picture\">");
            grid3Temp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            grid3Temp.Append("<img alt=\"${alternateText}\" src=\"${imagePath}\" title=\"${name}\"/>");
            grid3Temp.Append("</a>");
            grid3Temp.Append("</div>");
            grid3Temp.Append("<div class=\"cssClassGrid3PriceBox\">");
            grid3Temp.Append("<div class=\"cssClassGrid3Price\">");
            grid3Temp.Append("<p class=\"cssClassGrid3OffPrice\">");
            grid3Temp.Append("<span class=\"cssRegularPrice_${itemID} sfLocale\">" + getLocale("Price :") + " </span><span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span>");
            grid3Temp.Append("<br/>");
            grid3Temp.Append("<span class=\"cssClassGrid3RealPrice\"> <span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span></span>");
            grid3Temp.Append("</p>");
            grid3Temp.Append("</div>");
            grid3Temp.Append("<div class=\"cssClassclear\">");
            grid3Temp.Append("</div>");
            grid3Temp.Append("</div>");
            grid3Temp.Append("</div>");
            grid3Temp.Append("</div>");
            grid3Temp.Append("</div>"); // end of extra wrapper div

            return grid3Temp;
        }

        private static StringBuilder scriptResultList()
        {
            StringBuilder scriptListTemp = new StringBuilder();            
            scriptListTemp.Append("<div class=\"cssClassProductListView clearfix\">");
            scriptListTemp.Append("<div class=\"cssClassProductListViewLeft\">");
            scriptListTemp.Append("<p class=\"cssClassProductPicture\">");
            scriptListTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            scriptListTemp.Append("<img alt=\"${alternateText}\" src=\"${imagePath}\" title=\"${name}\"/>");
            scriptListTemp.Append("</a>");
            scriptListTemp.Append("</p>");
            scriptListTemp.Append("</div>");
            scriptListTemp.Append("<div class=\"cssClassProductListViewRight\">");
            scriptListTemp.Append("<div class=\"cssClassProductName\">");
            scriptListTemp.Append("<h2><a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\" title=\"${titleName}\">");
            scriptListTemp.Append("${name}");
            scriptListTemp.Append("</a></h2></div>");
            scriptListTemp.Append("<p>{{html shortDescription}}</p>");
            scriptListTemp.Append("<p class=\"cssClassProductOffPrice\">");
            scriptListTemp.Append("<span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span></p>");
            scriptListTemp.Append("<p class=\"cssClassProductRealPrice\">");
            scriptListTemp.Append("<span>");
            scriptListTemp.Append("<span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span>");
            scriptListTemp.Append("</span>");
            scriptListTemp.Append("</p>");
            scriptListTemp.Append("<div class=\"cssListDyanamicAttr\">$DynamicAttr</div>");
            scriptListTemp.Append("<div class=\"cssClassViewDetailsAddtoCart\">");
            scriptListTemp.Append("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">");
            scriptListTemp.Append("<div class=\"sfButtonwrapper\" data-itemid=\"${itemID}\" data-class=\"addtoCart\" data-type=\"button\" data-addtocart=\"addtocart${itemID}\" data-title=\"${titleName}\" data-onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);\">");
            scriptListTemp.Append("<label class=\"i-cart cssClassCartLabel cssClassGreenBtn\"><button class=addtoCart type=button addtocart=addtocart${itemID} title=${titleName} onclick=AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);>");
            scriptListTemp.Append("<span class=\"sfLocale\">" + getLocale("Cart +") + "</span></button></label>");
            scriptListTemp.Append("</div>");           
            scriptListTemp.Append("</div>");
            scriptListTemp.Append("<div class=\"cssClassWishListButton\">");
            scriptListTemp.Append("<label class=\"i-wishlist cssWishListLabel cssClassDarkBtn\"><button type=\"button\" id=\"addWishList\" onclick=AspxCommerce.RootFunction.CheckWishListUniqueness(${itemID},${JSON2.stringify(sku)},this)<span>" + getLocale("Wishlist+") + "</span></button></label></div>");           
            scriptListTemp.Append("</div>");
            scriptListTemp.Append("</div>");
            scriptListTemp.Append("</div>");          

            return scriptListTemp;
        }

        private static StringBuilder scriptCompactList()
        {
            StringBuilder compactListTemp = new StringBuilder();
            compactListTemp.Append("<tr>");
            compactListTemp.Append("<td class=\"cssClassCLPicture\">");
            compactListTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            compactListTemp.Append("<img src=\"${imagePath}\" alt=\"${alternateText}\" title=\"${name}\"/>");
            compactListTemp.Append("</a></td>");
            compactListTemp.Append("<td class=\"cssClassCLProduct\">");
            compactListTemp.Append("<p class=\"cssClassCLProductInfo\">");
            compactListTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}.${pageExtension}\">${name}</a></p>");
            compactListTemp.Append("</td>");
            compactListTemp.Append("<td class=\"cssClassCLProductCode\">");
            compactListTemp.Append("<p>${sku}</p>");
            compactListTemp.Append("</td>");
            compactListTemp.Append("<td class=\"cssClassCLPrice\">");
            compactListTemp.Append("<p>");
            compactListTemp.Append("<span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span></p>");
            compactListTemp.Append("</td>");
            compactListTemp.Append("<td class=\"cssClassCLAddtoCart\">");
            compactListTemp.Append("<div class=\"cssClassAddtoCard_${itemID}\">");
            compactListTemp.Append("<div class=\"sfButtonwrapper\">");
            compactListTemp.Append("<button type=\"button\" addtocart=\"addtocart${itemID}\" title=\"${titleName}\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);\">");
            compactListTemp.Append("<span class=\"sfLocale\">" + getLocale("Add to Cart") + "</span></button>");
            compactListTemp.Append("</div>");
            compactListTemp.Append("</div>");
            compactListTemp.Append("</td>");
            compactListTemp.Append("</tr>");

            return compactListTemp;
        }

        private static StringBuilder scriptResultListWithoutOptions()
        {
            StringBuilder listWithoutOptions = new StringBuilder();
            listWithoutOptions.Append("<div>"); //initial extra wrapper div
            listWithoutOptions.Append("<div class=\"cssClassListViewWithOutOptions\">");
            listWithoutOptions.Append("<div class=\"cssClassListViewWithOutOptionsLeft\">");
            listWithoutOptions.Append("<p class=\"cssClassProductPicture\">");
            listWithoutOptions.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            listWithoutOptions.Append("<img alt=\"${alternateText}\"  src=\"${imagePath}\" title=\"${name}\" />");
            listWithoutOptions.Append("</a></p>");
            listWithoutOptions.Append("</div>");
            listWithoutOptions.Append("<div class=\"cssClassListViewWithOutOptionsRight\">");
            listWithoutOptions.Append("<h2><a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">${name}</a></h2>");
            listWithoutOptions.Append("<p class=\"cssClassProductCode\">${sku}</p>");
            listWithoutOptions.Append("<p>{{html shortDescription}}</p>");
            listWithoutOptions.Append("<p class=\"cssClassListViewWithOutOptionsPrice\">");
            listWithoutOptions.Append("<span class=\"sfLocale\">Price : </span><span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span>");
            listWithoutOptions.Append("<span class=\"cssRegularPrice_${itemID} cssClassListViewWithOutOptionsOffPrice\">");
            listWithoutOptions.Append("<span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span>");
            listWithoutOptions.Append("</span>");
            listWithoutOptions.Append("<span class=\"cssClassInstock_${itemID} sfLocale\">" + getLocale("In stock") + "</span></p>");
            listWithoutOptions.Append("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">");
            listWithoutOptions.Append("<div class=\"sfButtonwrapper\">");
            listWithoutOptions.Append("<button type=\"button\" addtocart=\"addtocart${itemID}\" title=\"${titleName}\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);\">");
            listWithoutOptions.Append("<span class=\"sfLocale\">" + getLocale("Add to Cart") + "</span></button>");
            listWithoutOptions.Append("</div></div>");
            listWithoutOptions.Append("<div class=\"sfButtonwrapper cssClassWishListWithoutOption\">");
            listWithoutOptions.Append("<button type=\"button\" id=\"addWishList\" onclick=\"AspxCommerce.RootFunction.AddToWishList(${itemID},${JSON2.stringify(costVariantItem)},${JSON2.stringify(sku)});\">");
            listWithoutOptions.Append("<span><span class=\"sfLocale\">" + getLocale("+Add to Wishlist") + "</span></span></button>");
            listWithoutOptions.Append("</div></div>");
            listWithoutOptions.Append("<div class=\"cssClassClear\"></div>");
            listWithoutOptions.Append("</div>");
            listWithoutOptions.Append("</div>"); // end of extra wrapper div

            return listWithoutOptions;
        }

        private static StringBuilder scriptResultProductGrid()
        {
            StringBuilder productGridTemp = new StringBuilder();
            productGridTemp.Append("<div>"); //initial extra wrapper div
            productGridTemp.Append("<div class=\"cssClassProductsGridBox\">");
            productGridTemp.Append("<div class=\"cssClassProductsGridInfo\">");
            productGridTemp.Append("<h2><a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">${name}</a></h2>");
            productGridTemp.Append("<div class=\"cssClassProductsGridPicture\">");
            productGridTemp.Append("<a href=\"${aspxRedirectPath}item/${sku}${pageExtension}\">");
            productGridTemp.Append("<img class=\"lazy\" data-original=\"${imagePath}\" src=\"${loaderpath}\" alt=\"${alternateText}\" title=\"${name}\"/>");
            productGridTemp.Append("</a></div>");
            productGridTemp.Append("<div class=\"cssClassProductsGridPriceBox\">");
            productGridTemp.Append("<div class=\"cssClassProductsGridPrice\">");
            productGridTemp.Append("<p class=\"cssClassProductsGridOffPrice\">");
            productGridTemp.Append("<span class=\"cssRegularPrice_${itemID} sfLocale\">" + getLocale("Price :") + " </span>");
            productGridTemp.Append("<span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span>");
            productGridTemp.Append("<br/><span class=\"cssClassProductsGridRealPrice\">");
            productGridTemp.Append("<span class=\"cssClassFormatCurrency\">${parseFloat(price).toFixed(2)}</span></span>");
            productGridTemp.Append("</p></div></div>");
            productGridTemp.Append("<div class=\"sfButtonwrapper\">");
            productGridTemp.Append("<div class=\"cssClassWishListButton\">");
            productGridTemp.Append("<button onclick=\"AspxCommerce.RootFunction.AddToWishList(${itemID},${JSON2.stringify(costVariantItem)},${JSON2.stringify(sku)});\" id=\"addWishListProductGrid\" type=\"button\">");
            productGridTemp.Append("<span class=\"sfLocale\">" + getLocale("+Add to Wishlist") + "</span></button>");
            productGridTemp.Append("</div></div>");
            productGridTemp.Append("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">");
            productGridTemp.Append("<div class=\"sfButtonwrapper\">");
            productGridTemp.Append("<button type=\"button\" addtocart=\"addtocart${itemID}\" title=\"${titleName}\" onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${JSON2.stringify(isCostVariant)},this);\">");
            productGridTemp.Append("<span class=\"sfLocale\">" + getLocale("Add to Cart") + "</span></button>");
            productGridTemp.Append("</div></div>");
            productGridTemp.Append("<div class=\"cssClassclear\"></div>");
            productGridTemp.Append("</div>");
            productGridTemp.Append("</div>");
            productGridTemp.Append("</div>"); // end of extra wrapper div

            return productGridTemp;
        }

        private static Hashtable hst = null;
        private static string getLocale(string messageKey)
        {
            string modulePath = "~/Modules/AspxCommerce/AspxTemplate/";
            hst = AppLocalized.getLocale(modulePath);
            string retStr = messageKey;
            if (hst != null && hst[messageKey] != null)
            {
                retStr = hst[messageKey].ToString();
            }
            return retStr;
        }

        public StringBuilder ReturnNotificationEmailTemplate()
        {
            StringBuilder returnEmailTemp = new StringBuilder();
            returnEmailTemp.Append("<table width=\"650\" cellspacing=\"5\" cellpadding=\"0\" border=\"0\" bgcolor=\"#e0e0e0\" align=\"center\" style=\"font:12px Arial, Helvetica, sans-serif;\">");
            returnEmailTemp.Append("<tbody><tr><td valign=\"top\" align=\"center\"><table width=\"680\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
            returnEmailTemp.Append("<tbody><tr><td><img width=\"1\" height=\"10\" src=\"http://%ServerPath%/blank.gif\" alt=\" \" /></td></tr>");
            returnEmailTemp.Append("<tr><td><table width=\"680\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr>");
            returnEmailTemp.Append("<td width=\"300\"><a href=\"http://%ServerPath%\" target=\"_blank\" style=\"outline:none; border:none;\"><img width=\"143\" height=\"62\" src=\"%LogoSource%\" alt=\"Logo\" title=\"Logo\"/></a></td>");
            returnEmailTemp.Append("<td width=\"191\" valign=\"middle\" align=\"left\">&nbsp;</td>");
            returnEmailTemp.Append("<td width=\"189\" valign=\"middle\" align=\"right\"><b style=\"padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;\"> Date: %DateTime%</b></td>");
            returnEmailTemp.Append("</tr></tbody></table></td></tr>");
            returnEmailTemp.Append("<tr><td><img width=\"1\" height=\"10\" src=\"http://%ServerPath%/blank.gif\" alt=\" \" /></td></tr>");
            returnEmailTemp.Append("<tr><td bgcolor=\"#fff\"><div style=\"border:1px solid #c7c7c7; background:#fff; padding:20px\">");
            returnEmailTemp.Append("<table width=\"650\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" bgcolor=\"#FFFFFF\">");
            returnEmailTemp.Append("<tbody><tr><td colspan=\"2\">");
            returnEmailTemp.Append(" <p style=\"font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;\">Return Notification.</p></td></tr>");
            returnEmailTemp.Append("<tr><td>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Return ID: %ReturnID%</p>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Item Name: %ItemName%</p>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Quantity: %Quantity%</p> ");
            returnEmailTemp.Append("</td><td>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Order ID: %OrderID%</p>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Variant: %Variant%</p>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Return Satutus: %ReturnStatus%</p> ");
            returnEmailTemp.Append("</td></tr>");
            returnEmailTemp.Append("<tr><td colspan=\"2\"><p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Return Action: %ReturnAction%</p> </td></tr>");
            returnEmailTemp.Append("</tbody></table>");
            returnEmailTemp.Append("<p style=\"margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;\">Thank You,<br />");
            returnEmailTemp.Append("<span style=\"font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;\">AspxCommerce Team </span>");
            returnEmailTemp.Append("</p></div></td></tr><tr>");
            returnEmailTemp.Append("<td><img width=\"1\" height=\"20\" src=\"http://%ServerPath%/blank.gif\" alt=\"blank \" /></td></tr>");
            returnEmailTemp.Append("<tr><td valign=\"top\" align=\"center\"> <p style=\"font-size:11px; color:#4d4d4d\">&copy; %DateYear% AspxCommerce. All Rights Reserved.</p></td></tr>");
            returnEmailTemp.Append("<tr><td valign=\"top\" align=\"center\"><img width=\"1\" height=\"10\" src=\"http://%ServerPath%/blank.gif\" alt=\" \" /></td></tr>");
            returnEmailTemp.Append("</tbody></table></td></tr></tbody></table>");
            return returnEmailTemp;
        }
        public static string GetAspxTemplate(int TemplateID)
        {
            switch (TemplateID)
            {
                case 1:
                    return scriptResultGrid().ToString();
                case 2:
                    return scriptResultList().ToString();
                default:
                    return scriptResultGrid().ToString();
            }
        }
    }

    public class AspxTemplateKeyValue
    {
        public string TemplateKey { get; set; }
        public string TemplateValue { get; set; }
    }




}
