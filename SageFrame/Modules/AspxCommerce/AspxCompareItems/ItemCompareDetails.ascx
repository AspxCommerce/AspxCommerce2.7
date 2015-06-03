<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemCompareDetails.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemsCompare_ItemCompareDetails" %>
<script id="scriptStaticField" type="text/x-jquery-tmpl">
</script>
<script id="scriptAttributeValue" type="txt/x-jquery-tmpl">
<td class="${CssClass}">{{html AttributeValue}}</td>
</script>
<script id="scriptResultProductGrid2" type="text/x-jquery-tmpl">
    <td>
        <div id="comparePride" class="cssClassProductsGridBox">
            <div class="cssClassProductsGridInfo">
                <div class="cssClassProductsGridPicture cssClassBMar20">
                    <img src='${imagePath}' alt='${alternateText}' title='${name}' />
                </div>
                <h2><a href="${aspxRedirectPath}item/${sku}${pageExtension}">${name}</a></h2>
                <div class="cssClassProductsGridPriceBox">
                    <div class="cssClassProductsGridPrice">
                        <p class="cssClassProductsGridOffPrice"><span class="cssRegularPrice_${itemID} sfLocale">Price :</span><span class="cssRegularPrice_${itemID} cssClassFormatCurrency">${parseFloat(listPrice).toFixed(2)}</span> </p>
                        <p id="cssClassProductsGridRealPrice_${itemID}" class="cssClassProductsGridRealPrice"><span class="cssClassFormatCurrency">${parseFloat(price).toFixed(2)}</span></p>
                    </div>
                </div>
                <div id="compareAddToWishlist" class="sfButtonwrapper clearfix cssClassTMar10">
                    <div class="cssClassWishListButton">
                        <label class="cssClassDarkBtn i-wishlist">
                            <button onclick="ItemCompareDetailsAPI.CheckWishListUniqueness(${itemID},${JSON2.stringify(sku)},${JSON2.stringify(CostVariant)});" id="addWishList" type="button"><span class="sfLocale">Wishlist +</span></button></label>
                    </div>
                    <div class="compareAddToCart cssClassAddtoCard" id="cssClassAddtoCard_${itemID}_${index}" style="display: none">
                        <div class="sfButtonwrapper" data-itemtypeid="${itemTypeID}" data-itemid="${itemID}" data-type="button" data-title="${name}" data-addtocart="addtocart${itemID}" data-onclick="AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},'${CostVariant}',this);">
                            <label class="cssClassGreenBtn i-cart">
                                <button type="button" addtocart="addtocart${itemID}" title="${name}" onclick="AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},'${CostVariant}',this);"><span class="sfLocale">Cart +</span></button></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </td>
</script>
<div id="divItemCompareElements" class="sfFormwrapper">
</div>
<div id="dvCompareList" class="cssClassCommonBox cssClassCompareBox">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblCompareTitle" runat="server" class="cssClassCompareItem" Text="Compare following Items"
                meta:resourcekey="lblCompareTitleResource1"></asp:Label>
        </h2>
    </div>
    <div id="divCompareElementsPopUP" class="sfFormwrapper cssBox">
        <table id="tblItemCompareList" width="100%" border="0" cellspacing="0" cellpadding="0">
            <thead>
                <tr class="cssGreenBorderBtm">
                    <td></td>
                </tr>
            </thead>
            <tbody id="itemDetailBody">
                <tr>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="cssClassHeaderRight clearfix">
        <div class="sfButtonwrapper">
            <label class="cssClassDarkBtn i-print">
                <button type="button" id="btnPrintItemCompare">
                    <span class="sfLocale">Print</span></button></label>
        </div>
    </div>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function () {
        $(".sfLocale").localize({
            moduleKey: AspxItemsCompare
        });
        $(this).ItemCompareDetails({
            servicepath: '<%=ServicePath%>',
            NoImageItemComparePath: '<%=NoImageItemComparePath %>',
            AllowAddToCart: '<%=AllowAddToCart %>',
            AllowOutStockPurchase: '<%=AllowOutStockPurchase %>'
        });
    });
    //]]>
</script>