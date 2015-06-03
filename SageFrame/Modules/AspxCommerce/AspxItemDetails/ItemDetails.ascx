<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemDetails.ascx.cs" Inherits="Modules_AspxCommerce_AspxItemDetails_ItemDetails" %>
<script type="text/javascript">
    //<![CDATA[   
    var userEmail = "<%=userEmail %>";
    var itemID = "<%=itemID %>";
    var itemNamePageBehind = "<%=itemName%>";
    var itemSKU = "<%=itemSKU%>";
    var aspxFilePath = "<%=aspxfilePath%>";
    var allowAddToCart = '<%=AllowAddToCart %>';
    var allowOutStockPurchase = '<%=allowOutStockPurchase %>';
    var allowMultipleReviewPerIP = '<%=allowMultipleReviewPerIP %>';
    var allowMultipleReviewPerUser = '<%=allowMultipleReviewPerUser %>';
    var allowAnonymousReviewRate = '<%=allowAnonymousReviewRate %>';
    var enableEmailFriend = '<%=enableEmailFriend %>';
    var noItemDetailImagePath = '<%=noItemDetailImagePath %>';
    var variantQuery = '<%=variantQuery%>';
    var lblListPrice = '<%=lblListPrice.ClientID%>';
    var art = '<%=AvarageRating %>';
    var trc = '<%=RatingCount %>';
    var itemTypeId = '<%=itemTypeId %>';
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    var itemPagePath = '<%=ItemPagePath %>';
    var isReviewExistByUser = '<%=isReviewExistByUser %>';
    var isReviewExistByIP = '<%=isReviewExistByIP %>';
    var ItemBasicInfo = '<%=ItemBasicInfo %>';
    var lstItemCostVariant = '<%=lstItemCostVariant%>';
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxItemDetails
        });
    });
    //]]>
</script>
<div class="cssClassCommonWrapper" id="itemDetails">
    <div class="cssClassProductInformation clearfix" style="display: none">
        <div class="cssClassProductPictureDetails">
            <div class="cssClassLeftSide">
                <h1><span id="spanItemName"></span></h1>
                <div class="cssClassItemRating">
                    <div class="cssRatingwrapper">
                        <div class="cssClassItemRatingBox cssClassToolTip">
                            <div class="cssClassRatingStar clearfix">
                                <asp:Literal ID="ltrRatings" runat="server" EnableViewState="false"></asp:Literal>
                            </div>
                            <span class="cssClassRatingTitle"></span>
                            <asp:Literal ID="ltrratingDetails" runat="server" EnableViewState="false"></asp:Literal>
                        </div>
                        <span class="cssClassTotalReviews"></span>
                        <a href="javascript:void(0);" rel="popuprel2" class="popupAddReview" value=""><span class="cssClassAddYourReview"></span></a>
                    </div>
                </div>
                <div class="cssItemCategories clearfix" style="display: none">
                    <div class="cssClassItemCategoriesHeading"><strong class="sfLocale">In Categories : </strong></div>
                    <div class="cssClassCategoriesName"></div>
                </div>
                <div>
                    <div class="cssViewer">
                        <i class="cssClassView i-preview"></i><span id="viewCount"></span>
                    </div>
                    <div class="cssClassAvailiability">
                        <strong>
                            <asp:Label ID="lblAvailability" runat="server" Text="Availability: " meta:resourcekey="lblAvailabilityResource1"></asp:Label></strong>
                        <span id="spanAvailability"></span>
                    </div>
                    <div id="Notify" style="display: none">
                        <div class="sfButtonwrapper">
                            <input type="text" name="notify" value="Enter your email here..." id="txtNotifiy" class="cssClassNotifyMeTxt" />
                            <label class="cssClassOrangeBtn i-notification">
                                <button id="btnNotify" type="button" class="sfBtn">
                                    <span class="sfLocale">Notify Me</span></button></label>
                        </div>
                    </div>
                </div>
                <div id="divCostVariant" class="clearfix">
                </div>
                <div class="cssPriceDetailwrap clearfix">
                    <div class="clearfix">
                        <div class="cssClassProductRealPrice">
                            <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                            <span id="spanPrice" class="cssClassFormatCurrency"></span>
                            <br />
                        </div>
                        <div class="cssClassProductOffPrice">
                            <asp:Label ID="lblListPrice" runat="server" Text=""></asp:Label>
                            <span id="spanListPrice" class="cssClassFormatCurrency"></span>
                        </div>
                    </div>

                    <div class="cssClassYouSave">
                        <asp:Label ID="lblSaving" runat="server" Text="You save: " meta:resourcekey="lblSavingResource1"></asp:Label>
                        <span id="spanSaving"></span>
                    </div>
                    <asp:Literal ID="ltrPriceHistory" runat="server"></asp:Literal>
                    <div class="cssClassDwnWrapper clearfix">
                        <asp:Literal ID="litQtyDiscount" runat="server" EnableViewState="false"></asp:Literal>
                        <div class="cssClassDownload cssClassRight" id="dwnlDiv">
                            <asp:Label ID="lblDownloadTitle" runat="server" Text="Download Sample Item:" meta:resourcekey="lblDownloadTitleResource1"></asp:Label>
                            <i class="i-download"></i><span id="spanDownloadLink" class="cssClassLink"></span>
                        </div>
                        <h2>
                            <span id="spnShowAvailability"></span>
                        </h2>
                    </div>
                </div>
                <div class="detailButtonWrapper clearfix">
                    <div class="cssQtywrapper">
                        <label class="cssClssQTY">
                            <asp:Label ID="lblQty" runat="server" Text="Qty: " meta:resourcekey="lblQtyResource1"></asp:Label>
                            <input type="text" id="txtQty" /><label id='lblNotification' style="color: #FF0000;"></label>
                        </label>
                    </div>
                    <div class="sfButtonwrapper clearfix">
                        <label class="i-cart cssClassCartLabel">
                            <button class="addtoCart" id="btnAddToMyCart" type="button" onclick="ItemDetail.AddToMyCart();">
                                <span class="sfLocale ">Cart+</span></button></label>
                         <input type="hidden" name="itemDetailWish" id="addWishListThis" />
                        <label class="i-wishlist cssWishListLabel cssClassDarkBtn">
                            <button type="button" id="btnAddToWishList" onclick="ItemDetail.AddWishItemFromDetailPage($(this))"><span>WishList +</span></button></label>
                        <input type="hidden" name="itemDetailCompare" id="addCompareListThis" />
                    </div>
                    <div class="cssDlinks clearfix">
                        <ul>
                            <li><a href="#" id="lnkContinueShopping" class="sfAnchor">
                                <span class="sfLocale">Continue Shopping </span></a></li>
                            <li id="divEmailAFriend">
                                <a href="#" rel="popuprel" class="popupEmailAFriend sfAnchor"><span class="sfLocale">Email a Friend</span> </a>
                            </li>
                            <li id="divPrintPage">
                                <a onclick="javascript:window.print();window.location.reload()" href="javascript:void(0)" class="sfAnchor"><span class="sfLocale">Print This Page</span></a>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="cssClassItemQuickOverview" style="display: none">
                    <h2>
                        <asp:Label ID="lblQuickOverview" Text="Quick Overview :" runat="server" meta:resourcekey="lblQuickOverviewResource1" />
                    </h2>
                    <div id="divItemShortDesc" class="cssClassTMar10">
                    </div>
                </div>
            </div>
            <div class="itemBrand">
            </div>
        </div>
    </div>
</div>
<div id="controlload">
</div>
<div class="popupbox" id="popuprel2">
    <div class="cssPopUpBody">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale"><i class="i-close"></i>Close</span></button>
        </div>
        <h2>
            <asp:Label ID="lblWriteReview" runat="server" Text="Write Your Own Review" meta:resourcekey="lblWriteReviewResource1"></asp:Label>
        </h2>
        <div class="sfFormwrapper">

            <div class="cssClassPopUpHeading">
                <h3>
                    <label id="lblYourReviewing">
                    </label>
                </h3>
            </div>
            <asp:Label ID="lblHowToRate" runat="server" Text="How do you rate this item?" meta:resourcekey="lblHowToRateResource1"></asp:Label>

            <table border="0" cellspacing="0" cellpadding="0" width="99%" id="tblRatingCriteria">
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Nickname:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtUserName" name="name" class="required" minlength="2" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Summary Of Review:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="required" minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Review:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextarea required"
                            maxlength="300"></textarea>
                    </td>
                </tr>
                <asp:Literal ID="ltrRatingCriteria" runat="server"></asp:Literal>
            </table>
            <div class="sfButtonwrapper cssClassWriteaReview cssClassTMar20">

                <label class="cssClassGreenBtn i-apply">
                    <button type="submit" id="btnSubmitReview">
                        <span class="sfLocale">Submit Review</span></button></label>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnPrice" />
<input type="hidden" id="hdnWeight" />
<input type="hidden" id="hdnQuantity" />
<input type="hidden" id="hdnListPrice" />
<input type="hidden" id="hdnTaxRateValue" />