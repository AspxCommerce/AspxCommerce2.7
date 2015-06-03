<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingBagHeader.ascx.cs"
    Inherits="Modules_AspxShoppingBagHeader_ShoppingBagHeader" %>
<script type="text/javascript">
    //<![CDATA[
    //$(function() {
    //    $(".sfLocale").localize({
    //        moduleKey: AspxShoppingBagHeader
    //    });
    //});
    var showMiniShopCart = '<%=ShowMiniShopCart%>';
    var allowAddToCart = '<%=AllowAddToCart %>';
    var allowAnonymousCheckOut = "<%=AllowAnonymousCheckOut%>";
    var minCartSubTotalAmount = '<%=MinCartSubTotalAmount%>';
    var allowMultipleAddChkOut = '<%=AllowMultipleAddChkOut%>';
    var shoppingCartURL = '<%=ShoppingCartURL %>';
    var singleAddressChkOutURL = '<%=SingleAddressChkOutURL %>';
    var BagType = '<%=BagType %>';
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    var shoppingUMId = '<%=UserModuleID%>';
    //]]> 
</script>
<div id="divMiniShoppingCart" class="cssClassShoppingCart" style="display: none;">
    <div class="cssClassFloatLeft">
        <div id="fullShoppingBag">
            <a onclick="if(!this.disabled){ShopingBag.showShoppingCart();};" href="javascript:void(0);" id="lnkShoppingBag" class="cssGreenCart"><i class="i-cart"></i></a>
        </div>
        <div id="emptyShoppingBag"><i class="i-cart cssGreyCart"></i></div>
    </div>
    <div class="cssClassCartInfo">
        <span id="cartItemCount"></span>
        <img id="imgarrow" src="" alt="down" class="sfLocale" title="down" onclick="if(!this.disabled){ShopingBag.showShoppingCart();}" />
    </div>
</div>
<div id="ShoppingCartPopUp" style="display: none;" class="Shopingcartpopup">
    <h2>
        <asp:Label ID="lblMyCartTitle" runat="server" Text="My Cart Item(s)" CssClass="cssClassShoppingBag"
            meta:resourcekey="lblMyCartTitleResource1"></asp:Label></h2>
    <div class="cssClassCommonSideBoxTable">
        <table id="tblListCartItems" cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <div class="sfButtonwrapper sfShoppingBagButton">
            <div class="cssClassViewCart">
            <label class="i-cart cssClassCartLabel cssClassGreenBtn"><button id="lnkViewCart" type="button">
                <span class="sfLocale" >View Cart</span></button></label>
                </div>
             <div class="cssClassWishListButton">
            <label class="i-checkout cssWishListLabel cssClassDarkBtn"><button id="lnkMiniCheckOut" type="button">
                <span class="sfLocale">Checkout</span></button></label>
                 </div>
        </div>
    </div>
</div>
