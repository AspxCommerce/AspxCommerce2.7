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
using AspxCommerce.RewardPoints;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Web.Script.Serialization;
using System.Linq;
using System.Text.RegularExpressions;
using AspxCommerce.ImageResizer;
using System.Data;


public partial class MyCart : BaseAdministrationUserControl
{

    private int StoreID, PortalID, CustomerID;

    public int CartItemCount = 0, UserModuleIDCart;
    private string UserName, CultureName;
    public string SessionCode = string.Empty;
    public string AllowRealTimeNotifications, NoImageMyCartPath, AllowMultipleAddShipping, AllowShippingRateEstimate, AllowCouponDiscount, ShowItemImagesOnCart, MinCartSubTotalAmount, AllowOutStockPurchase, MultipleAddressChkOutURL;
    
    private AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    public string Coupon = string.Empty;
    public string Items = string.Empty;
    public decimal qtyDiscount = 0;
    public string CartPRDiscount = string.Empty;
    public string CartModulePath = string.Empty;
    JavaScriptSerializer json_serializer = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetValidUntilExpires(true);

        GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
        aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);

        UserModuleIDCart = int.Parse(SageUserModuleID);

        try
        {
            CartModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            SageFrameConfig pagebase = new SageFrameConfig();            

            string template = TemplateName;
            StoreSettingConfig ssc = new StoreSettingConfig();
            ssc.GetStoreSettingParamNine(StoreSetting.DefaultProductImageURL,
                                        StoreSetting.AllowMultipleShippingAddress,
                                        StoreSetting.ShowItemImagesInCart,
                                        StoreSetting.MinimumCartSubTotalAmount,
                                        StoreSetting.AllowOutStockPurchase,
                                        StoreSetting.MultiCheckOutURL,
                                        StoreSetting.AllowShippingRateEstimate,
                                        StoreSetting.AllowCouponDiscount,
                                        StoreSetting.AllowRealTimeNotifications,
                                        out NoImageMyCartPath,
                                        out AllowMultipleAddShipping,
                                        out ShowItemImagesOnCart,
                                        out MinCartSubTotalAmount,
                                        out AllowOutStockPurchase,
                                        out MultipleAddressChkOutURL,
                                        out AllowShippingRateEstimate,
                                        out AllowCouponDiscount,
                                        out AllowRealTimeNotifications,
                                        StoreID,
                                        PortalID,
                                        CultureName
                                        );

            if (!IsPostBack)
            {
                IncludeCss("MyCart", "/Templates/" + template + "/css/GridView/tablesort.css",
                           "/Templates/" + template + "/css/MessageBox/style.css",
                           "/Templates/", template, "/css/ToolTip/tooltip.css",
                           "/Modules/AspxCommerce/AspxCart/css/module.css");
                IncludeJs("MyCart","/Modules/AspxCommerce/AspxCart/js/MyCart.js", "/js/encoder.js", "/js/MessageBox/alertbox.js", "/js/jquery.easing.1.3.js", "/js/jquery.tipsy.js", "/js/Session.js");
            }
            DisplayCartItems(template);
            IncludeLanguageJS();
            CouponInfo();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void CouponInfo()
    {
        List<CouponSession> cs = new List<CouponSession>();
        cs = CheckOutSessions.Get<List<CouponSession>>("CouponSession");
        Coupon = json_serializer.Serialize(cs);
    }

    Hashtable hst = null;
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }


    private void DisplayCartItems(string template)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        string aspxRootPath = ResolveUrl("~/");
        StringBuilder aspxTemplateFolderPath = new StringBuilder();
        aspxTemplateFolderPath.Append(aspxRootPath);
        aspxTemplateFolderPath.Append("Templates/");
        aspxTemplateFolderPath.Append(template);

        double arrRewardtotalPrice = 0;
        StringBuilder arrRewardDetails = new StringBuilder();
        StringBuilder arrRewardSub = new StringBuilder();

        List<CartInfo> itemsList = LoadCartItems();

        if (itemsList.Count > 0)
        {
            CartPRDiscount = AspxCartController.GetDiscountPriceRule(itemsList[0].CartID, aspxCommonObj, 0);
            GetDiscount();
        }
        itemsList = itemsList.Select(e => { e.KitData = Regex.Replace(e.KitData, "[\"\"]+", "'"); return e; }).ToList();
        Items = json_serializer.Serialize(itemsList);

        StringBuilder scriptBuilder_root = new StringBuilder();
        StringBuilder cartItemList = new StringBuilder();
        if (itemsList.Count > 0)
        {
            CartItemCount = itemsList.Count;
            cartItemList.Append(
                GetStringScript(
                    " $('.cssClassSubTotalAmount,.cssClassLeftRightBtn,.cssClassapplycoupon,.cssClassBlueBtnWrapper').show();"));
            cartItemList.Append(
                "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" id=\"tblCartList\" class=\"sfGridTableWrapper\">");
            cartItemList.Append("<thead><tr class=\"cssClassHeadeTitle\" >");
            cartItemList.Append("<th class=\"cssClassSN\">Sn.");
            if (Boolean.Parse(ShowItemImagesOnCart))
            {
                cartItemList.Append("</th><th class=\"cssClassItemImageWidth\">");
                cartItemList.Append(getLocale("Item Description"));
            }
            cartItemList.Append("</th><th>");
            cartItemList.Append(getLocale("Variants"));
            cartItemList.Append("</th>");
            cartItemList.Append("<th class=\"cssClassQTY\">");
            cartItemList.Append(getLocale("Qty"));
            cartItemList.Append("</th>");
            cartItemList.Append("<th class=\"cssClassItemPrice\">");
            cartItemList.Append(getLocale("Unit Price"));
            cartItemList.Append("</th>");
            cartItemList.Append("<th class=\"cssClassSubTotal\">");
            cartItemList.Append(getLocale("Line Total"));
            cartItemList.Append("</th>");
            cartItemList.Append("<th class=\"cssClassAction\">");
            cartItemList.Append(getLocale("Action"));
            cartItemList.Append("</th>");
            cartItemList.Append(" </tr>");
            cartItemList.Append("</thead>");
            cartItemList.Append("<tbody>");
            List<BasketItem> basketItems = new List<BasketItem>();
            int index = 0;

            StringBuilder bsketItems = new StringBuilder();
            bsketItems.Append("[");
            foreach (CartInfo item in itemsList)
            {
                if (item.ItemTypeID == 1)
                {
                    StringBuilder bitems = new StringBuilder();
                    bitems.Append("{");
                    bitems.Append(String.Format(
                                        "\'Height\':'{0}',\'ItemName\':'{1}',\'Length\':'{2}',\'Quantity\':'{3}',\'WeightValue\':'{4}',\'Width\':'{5}'",
                                        item.Height ?? 0, item.ItemName,
                                        item.Length ?? 0, item.Quantity.ToString(),
                                        decimal.Parse(item.Weight.ToString()), item.Width ?? 0
                                        ));

                    bitems.Append("},");
                    bsketItems.Append(bitems);

                }

                index = index + 1;
                StringBuilder imagePath = new StringBuilder();
                imagePath.Append("Modules/AspxCommerce/AspxItemsManagement/uploads/");
                imagePath.Append(item.ImagePath);
                if (String.IsNullOrEmpty(item.ImagePath))
                {
                    imagePath.Clear();
                    imagePath.Append(NoImageMyCartPath);
                }
                else if (String.IsNullOrEmpty(item.AlternateText))
                {
                    item.AlternateText = item.ItemName;
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(item.ImagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                }
                else if (item.ImagePath != "")
                {
                    //Resize Image Dynamically
                    InterceptImageController.ImageBuilder(item.ImagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                }

                if ((itemsList.IndexOf(item)) % 2 == 0)
                {
                    cartItemList.Append("<tr class=\"sfEven\" >");
                }
                else
                {
                    cartItemList.Append("<tr class=\"sfOdd\" >");
                }
                cartItemList.Append("<td>");
                cartItemList.Append("<b>");
                cartItemList.Append(index);
                cartItemList.Append(".");
                cartItemList.Append("</b>");

                cartItemList.Append("</td>");
                if (item.ItemTypeID == 3)
                {



                    cartItemList.Append("<td>");

                    if (Boolean.Parse(ShowItemImagesOnCart))
                    {

                        cartItemList.Append("<p class=\"cssClassCartPicture\">");
                        cartItemList.Append("<img src='");
                        cartItemList.Append(aspxRootPath);
                        cartItemList.Append(imagePath.Replace("uploads", "uploads/Small"));
                        cartItemList.Append("'  alt=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" title=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" ></p>");
                    }
                    cartItemList.Append("<div class=\"cssClassCartPictureInformation\">");


                    cartItemList.Append("<a href=\"item/");
                    cartItemList.Append(item.SKU);
                    cartItemList.Append(pageExtension);
                    cartItemList.Append("\" costvariants=\"");
                    cartItemList.Append(item.CostVariants);
                    cartItemList.Append("\" onclick=\"AspxCart.SetCostVartSession(this);\" >");
                    cartItemList.Append(item.ItemName);
                    cartItemList.Append("</a>");
                    cartItemList.Append("<ul class='giftcardInfo'>");
                    cartItemList.Append("<li>");
                    cartItemList.Append(item.ShortDescription);
                    cartItemList.Append("</li>");
                    cartItemList.Append("</ul>");
                    cartItemList.Append("</div>");



                    cartItemList.Append("</td>");
                }
                else if (item.ItemTypeID == 6)
                {

                    cartItemList.Append("<td>");

                    if (Boolean.Parse(ShowItemImagesOnCart))
                    {

                        cartItemList.Append("<p class=\"cssClassCartPicture\">");
                        cartItemList.Append("<img src='");
                        cartItemList.Append(aspxRootPath);
                        cartItemList.Append(imagePath.Replace("uploads", "uploads/Small"));
                        cartItemList.Append("'  alt=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" title=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" ></p>");
                    }
                    cartItemList.Append("<div class=\"cssClassCartPictureInformation\">");


                    cartItemList.Append("<a href=\"item/");
                    cartItemList.Append(item.SKU);
                    cartItemList.Append(pageExtension);
                    cartItemList.Append("\" costvariants=\"");
                    cartItemList.Append(item.CostVariants);
                    cartItemList.Append("\" onclick=\"AspxCart.SetCostVartSession(this);\" >");
                    cartItemList.Append(item.ItemName);
                    cartItemList.Append("</a>");
                    string[] lis = Regex.Split(item.ShortDescription, "</br>");
                    cartItemList.Append("<ul class='kitInfo'>");

                    foreach (var li in lis)
                    {
                        cartItemList.Append("<li>");
                        cartItemList.Append(li);
                        cartItemList.Append("</li>");
                    }
                    cartItemList.Append("</ul>");
                    cartItemList.Append("</div>");


                    cartItemList.Append("</td>");
                }
                else
                {
                    cartItemList.Append("<td>");
                    if (Boolean.Parse(ShowItemImagesOnCart))
                    {

                        cartItemList.Append("<p class=\"cssClassCartPicture\">");
                        cartItemList.Append("<img src='");
                        cartItemList.Append(aspxRootPath);
                        cartItemList.Append(imagePath.Replace("uploads", "uploads/Small"));
                        cartItemList.Append("'  alt=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" title=\"");
                        cartItemList.Append(item.AlternateText);
                        cartItemList.Append("\" ></p>");
                    }
                    cartItemList.Append("<div class=\"cssClassCartPictureInformation\">");

                    if (item.CostVariantsValueIDs != "")
                    {
                        cartItemList.Append("<a href=\"item/");
                        cartItemList.Append(item.SKU);
                        cartItemList.Append(pageExtension);
                        cartItemList.Append("?varId=");
                        cartItemList.Append(item.CostVariantsValueIDs);
                        cartItemList.Append("\"  costvariants=\"");
                        cartItemList.Append(item.CostVariants);
                        cartItemList.Append("\" onclick=\"AspxCart.SetCostVartSession(this);\" >");
                        cartItemList.Append(item.ItemName);
                        cartItemList.Append("</a>");
                    }
                    else
                    {
                        cartItemList.Append("<a href=\"item/");
                        cartItemList.Append(item.SKU);
                        cartItemList.Append(pageExtension);
                        cartItemList.Append("\" costvariants=\"");
                        cartItemList.Append(item.CostVariants);
                        cartItemList.Append("\" onclick=\"AspxCart.SetCostVartSession(this);\" >");
                        cartItemList.Append(item.ItemName);
                        cartItemList.Append("</a>");
                    }
                    cartItemList.Append("</div>");
                    cartItemList.Append("</td>");
                }
                cartItemList.Append("<td class=\"row-variants\">");
                cartItemList.Append(item.CostVariants);
                cartItemList.Append("</td>");
                cartItemList.Append("<td class=\"cssClassQTYInput\">");
                cartItemList.Append("<input class=\"num-pallets-input\" autocomplete=\"off\" price=\"");
                cartItemList.Append(Convert.ToDecimal(item.Price).ToString("N2"));
                cartItemList.Append("\" id=\"txtQuantity_");
                cartItemList.Append(item.CartItemID);
                cartItemList.Append("\" type=\"text\" cartID=\"");
                cartItemList.Append(item.CartID);
                cartItemList.Append("\" value=\"");
                cartItemList.Append(item.Quantity);
                cartItemList.Append("\" sku=\"");
                cartItemList.Append(item.SKU);
                cartItemList.Append("\"  quantityInCart=\"");
                cartItemList.Append(item.Quantity);
                cartItemList.Append("\" actualQty=\"");
                cartItemList.Append(item.ItemQuantity);
                cartItemList.Append("\" costVariantID=\"");
                cartItemList.Append(item.CostVariantsValueIDs);
                cartItemList.Append("\" itemID=\"");
                cartItemList.Append(item.ItemID);
                cartItemList.Append("\" addedValue=\"");
                cartItemList.Append(item.Quantity);
                cartItemList.Append("\" minCartQuantity=\"");
                cartItemList.Append(item.MinCartQuantity);
                cartItemList.Append("\" maxCartQuantity=\"");
                cartItemList.Append(item.MaxCartQuantity);
                cartItemList.Append("\">");

                cartItemList.Append("<label class=\"lblNotification\" style=\"color: #FF0000;\"></label></td>");
                cartItemList.Append("<td class=\"price-per-pallet\">");
                cartItemList.Append("<span class=\"cssClassFormatCurrency\">");
                cartItemList.Append(Convert.ToDecimal(item.Price).ToString("N2"));
                cartItemList.Append("</span>");
                cartItemList.Append("</td>");
                cartItemList.Append("<td class=\"row-total\">");
                cartItemList.Append("<input class=\"row-total-input cssClassFormatCurrency\" autocomplete=\"off\" id=\"txtRowTotal_");
                cartItemList.Append(item.CartID);
                cartItemList.Append("\" value=\"");
                cartItemList.Append(Convert.ToDecimal(item.TotalItemCost).ToString("N2"));
                cartItemList.Append("\"  readonly=\"readonly\" type=\"text\" />");
                cartItemList.Append("</td>");
                cartItemList.Append("<td>");
                cartItemList.Append(" <a class=\"ClassDeleteCartItems\" title=\"Delete\" value=\"");
                cartItemList.Append(item.CartItemID);
                cartItemList.Append("\" cartID=\"");
                cartItemList.Append(item.CartID);
                cartItemList.Append("\"><i class=\"i-delete\" original-title=\"" + getLocale("Delete") + "\"></i></a>");
                cartItemList.Append("</td>");
                cartItemList.Append("</tr>");

                arrRewardtotalPrice += Math.Round(double.Parse((item.Price * item.Quantity).ToString()), 2);

                arrRewardSub.Append("'<li>'+ ");
                arrRewardSub.Append(item.Quantity);
                arrRewardSub.Append("+'X' +eval(");
                arrRewardSub.Append((item.Price));
                arrRewardSub.Append("* rewardRate).toFixed(2)+ '</li>'+");

                arrRewardDetails.Append("'<li><b>'+eval( ");
                arrRewardDetails.Append((item.TotalItemCost));
                arrRewardDetails.Append("* rewardRate).toFixed(2)+ '</b> ");
                arrRewardDetails.Append(getLocale("Points for Product:"));
                arrRewardDetails.Append(item.ItemName);
                arrRewardDetails.Append("&nbsp &nbsp</li>'+");

                if (index == itemsList.Count)
                {

                    StringBuilder scriptBuilder = new StringBuilder();

                    scriptBuilder.Append("AspxCart.Vars.CartID =");
                    scriptBuilder.Append(item.CartID);
                    scriptBuilder.Append(";");
                    scriptBuilder.Append(" var rewardRate = parseFloat($('#hdnRewardRate').val());");
                    scriptBuilder.Append("var arrRewardDetails =");
                    scriptBuilder.Append(arrRewardDetails.ToString().Substring(0, arrRewardDetails.Length - 1));
                    scriptBuilder.Append(";");
                    scriptBuilder.Append("var  arrRewardSub =");
                    scriptBuilder.Append(arrRewardSub.ToString().Substring(0, arrRewardSub.Length - 1));
                    scriptBuilder.Append(";");
                    scriptBuilder.Append("if (isPurchaseActive == true){");
                    scriptBuilder.Append("$('#dvPointsTotal').empty(); $('#ulRewardDetails').html(arrRewardDetails);");
                    scriptBuilder.Append("$('#ulRewardSub').html(arrRewardSub);");
                    scriptBuilder.Append("$('#dvPointsTotal').append(eval(");
                    scriptBuilder.Append(arrRewardtotalPrice);
                    scriptBuilder.Append(" * rewardRate).toFixed(2));");

                    scriptBuilder.Append("} ");
                    scriptBuilder.Append("AspxCart.GetDiscountCartPriceRule(AspxCart.Vars.CartID, 0);");
                    scriptBuilder.Append("$('#tblCartList tr:even ').addClass('sfEven');");
                    scriptBuilder.Append("$('#tblCartList tr:odd ').addClass('sfOdd');");
                    //scriptBuilder.Append("$('.cssClassCartPicture img[title]').tipsy({ gravity: 'n' });");
                    //scriptBuilder.Append("$(\".i-delete\").tipsy({gravity:'n'});");
                    scriptBuilder.Append("AspxCart.BindCartFunctions();");
                    string bsItemsTemp = bsketItems.ToString().Substring(0, bsketItems.Length - 1);
                    bsketItems.Clear();
                    bsketItems.Append(bsItemsTemp);
                    bsketItems.Append("]");
                    scriptBuilder.Append(" AspxCart.SetBasketItems(eval(\"");
                    scriptBuilder.Append(bsketItems);
                    scriptBuilder.Append("\"));");
                }

            }
            cartItemList.Append("</tbody></table>");

            scriptBuilder_root.Append("if (isPurchaseActive == true){");
            scriptBuilder_root.Append(" var rewardRate =browns fashion parseFloat($('#hdnRewardRate').val());");
            scriptBuilder_root.Append("var arrRewardDetails =");
            scriptBuilder_root.Append(arrRewardDetails.ToString().Substring(0, arrRewardDetails.Length - 1));
            scriptBuilder_root.Append(";");
            scriptBuilder_root.Append("var  arrRewardSub =");
            scriptBuilder_root.Append(arrRewardSub.ToString().Substring(0, arrRewardSub.Length - 1) +
                                 ";");
            scriptBuilder_root.Append("$('#dvPointsTotal').empty(); $('#ulRewardDetails').html(arrRewardDetails);");
            scriptBuilder_root.Append("$('#ulRewardSub').html(arrRewardSub);");
            scriptBuilder_root.Append("$('#dvPointsTotal').append(eval(" + arrRewardtotalPrice + " * rewardRate).toFixed(2));");

            scriptBuilder_root.Append("} ");
            ltCartItems.Text = cartItemList.ToString();
        }
        else
        {
            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder.Append("$('.cssClassCartInformation').html('<span class=\"cssClassNotFound\">" +
                                 getLocale("Your cart is empty!") + "</span>');");
            string script = GetStringScript(scriptBuilder.ToString());
            ltCartItems.Text = script;
        }
    }

    private void GetDiscount()
    {

        qtyDiscount = AspxCartController.GetDiscountQuantityAmount(aspxCommonObj);

    }


    private string GetStringScript(string codeToRun)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){");
        script.Append(codeToRun);
        script.Append("});</script>");
        return script.ToString();
    }

    private List<CartInfo> LoadCartItems()
    {
        List<CartInfo> cartInfos = AspxCoreController.GetCartDetails(aspxCommonObj);
        return cartInfos;
    }
}
