<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyCart.ascx.cs" Inherits="MyCart" %>
<%@ Register Src="~/Modules/AspxCommerce/AspxRewardPoints/RewardPoint.ascx" TagPrefix="uc1" TagName="RewardPoint" %>
<%@ Register Src="../AspxShippingRateEstimate/ShippingRateEstimate.ascx" TagName="ShippingRateEstimate" TagPrefix="uc2" %>

<div class="cssClassCartInformation cssClassCartTotal" id="divMyCart">
    <div class="cssMyCartContainer" style="display: none;">
        <div class="cssClassBlueBtnWrapper" style="display: none">
        </div>
        <div class="cssClassCartInformationDetails" id="divCartDetails">
            <asp:Literal runat="server" ID="ltCartItems" EnableViewState="False"></asp:Literal>
        </div>
        <div class="cssClassCartbtn clearfix">
            <div class="sfButtonwrapper">
                <label class="cssClassGreenBtn i-arrow-right">
                    <button type="button" id="lnkContinueShopping"><span class="sfLocale">Continue Shopping</span></button></label>
                <label class="cssClassDarkBtn i-update">
                    <button type="button" id="btnUpdateShoppingCart">
                        <span class="sfLocale">Update Cart</span></button></label>
                <label class="cssClassGreyBtn i-clear">
                    <button type="button" id="btnClear">
                        <span class="sfLocale">Clear Cart</span></button></label>
            </div>
        </div>
        <div class="cssClassCartbtmwrapper clearfix">
            <div id="divShippingRate" style="display: none;">
                <uc2:ShippingRateEstimate ID="ShippingRateEstimate1" runat="server" />
            </div>
            <div class="cssClassApplyWrap">
                <div class="cssClassLeftRightBtn" style="display: none">
                    <div class="cssClassCartInformation" id="divCouponDiscountBox" style="display: none;">
                        <div class="cssClassHeader cssClasscarttotal clearfix">
                            <h2 class="sfLocale">Discount Coupon</h2>
                            <div class="cssClassCouponHelp" style="display: none">
                                <span class="cssClassRequired ">*</span><span class="sfLocale">Coupon amount is applied to your cart after you click the apply button. You can check your coupon code in your mail!</span>
                            </div>
                            <div class="cssClassapplycoupon" style="display: none">
                                <p><strong class="sfLocale">Enter your coupon code if you have one.</strong></p>
                                <input type="text" id="txtCouponCode" />
                                <div class="sfButtonwrapper">
                                    <label class="cssClassOrangeBtn i-apply">
                                        <button type="button" id="btnSubmitCouponCode"><span class="sfLocale">Apply Coupon</span></button>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <uc1:RewardPoint runat="server" ID="RewardPoint" />
            </div>
            <div class="cssClassSubTotal">
                <div class="cssClassHeader">
                    <h2 class="sfLocale">Total</h2>
                </div>
                <div class="cssClassTwrap">
                    <table class="cssClassSubTotalAmount">
                    </table>
                    <div class="cssClassBlueBtnWrapper" style="display: none">
                        <div class="sfButtonwrapper">
                            <a id="lnkProceedToSingleChkout" href="#" class="cssClassGreenBtn"><span class="sfLocale">Proceed to Checkout</span>
                            </a>
                        </div>
                        <%--   <div class="cssClassBlueBtn" id="divCheckOutMultiple">
            <a id="lnkProceedToMultiCheckout" href="#" onclick='SetSession();'><span>Checkout With Multiple Address</span>
            </a>
        </div>--%>
                        <div class="cssClassClear">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassBlueBtnWrapper" style="display: none">
            <div class="cssClassBlueBtn" id="divCheckOutMultiple" style="display: none">
                <a id="lnkProceedToMultipleChkout" href="#"><span class="sfLocale">Checkout with Multiple Addresses</span>
                </a>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCartLocale
        });

        $(this).MyCart({
            ShowItemImagesOnCartSetting: '<%=ShowItemImagesOnCart%>',
            AllowOutStockPurchaseSetting: '<%=AllowOutStockPurchase %>',
            MinCartSubTotalAmountSetting: '<%=MinCartSubTotalAmount%>',
            AllowMultipleAddShippingSetting: '<%=AllowMultipleAddShipping%>',
            NoImageMyCartPathSetting: '<%=NoImageMyCartPath %>',
            MultipleAddressChkOutURL: '<%=MultipleAddressChkOutURL %>',
            Coupon: '<%=Coupon%>',
            Items: '<%=Items%>',
            CartPRDiscount: parseFloat('<%=CartPRDiscount %>'),
            QuantityDiscount: '<%=qtyDiscount %>',
            CartModulePath: '<%=CartModulePath%>',
            AllowShippingRateEstimate: '<%=AllowShippingRateEstimate %>',
            AllowCouponDiscount: '<%=AllowCouponDiscount %>',
            AllowRealTimeNotifications: '<%=AllowRealTimeNotifications %>',
            CartItemCount: '<%=CartItemCount %>'
        });
        //$(".tipsy").remove();
        //$('#tblCartList .cssClassCartPicture img[title]').tipsy();
        //$("#tblCartList .i-delete").tipsy();
    });
    var userModuleIDCart = '<%=UserModuleIDCart %>';
    $(window).load(function () {
        if (IsABTestInstalled.toLowerCase() == "true") {
            ABTest.ABTestSaveVisitCount();
        }
        //KPI Saves Visit Counts for Cart Page             
        if (IsKPIInstalled.toLowerCase() == "true") {
            KPICommon.KPISaveVisit('My Cart');
        }
    });
    //]]>
</script>