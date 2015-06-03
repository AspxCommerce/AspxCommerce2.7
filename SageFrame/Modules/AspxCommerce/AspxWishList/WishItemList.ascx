<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItemList.ascx.cs"
    Inherits="WishItemList" %>
<div id="divWishListSort" class="sort" style="display: none">
    <asp:Literal ID="ltrWishListSortBy" runat="server" EnableViewState="False"></asp:Literal>
</div>
<div id="divWishListContent" class="sfFormwrapper cssClassWishListDash">
    <div class="cssClassCommonCenterBox">
        <div class="cssClassHeader">
            <h2 class="cssClassWishItem sfLocale">My WishList Content</h2>
        </div>
        <div class="cssClassCommonCenterBoxTable">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblWishItemList"
                class="cssClassMyWishItemTable sfGridTableWrapper">
                <asp:Literal ID="ltrWishList" runat="server" EnableViewState="False"></asp:Literal>
            </table>
            <div class="cssClassPageNumber cssDashPageNumber" id="divWishListPageNumber" style="display: none">
                <div id="Pagination">
                </div>
                <div class="cssClassViewPerPage">
                    <asp:Literal ID="ltrWishListPagination" runat="server" EnableViewState="False"></asp:Literal>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper" id="wishitemBottom" style="display: none">
            <asp:Literal ID="ltrWishListButon" runat="server" EnableViewState="False"></asp:Literal>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel5">
    <div class="cssPopUpBody">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale">
                    <i class="i-close"></i>Close</span></button>
        </div>
        <h2>
            <label id="lblWishHeading" class="cssClassWishItem sfLocale">
                Share Your WishList</label>
        </h2>
        <div id="divShareWishList" class="sfFormwrapper">
            <div class="cssClassCommonCenterBox">
                <div class="cssClassPopUpHeading">
                    <h3>
                        <label id="lblShareHeading" class="cssClassLabel sfLocale">
                            Sharing Information</label>
                    </h3>
                </div>
                <div class="cssClassCommonCenterBoxTable">
                    <div class="cssClassShareList clearfix">
                        <ul>
                            <li>
                                <label id="lblEmailHeading" class="sfLocale">
                                    Email addresses, separated by commas</label>
                                <span class="cssClassRequired">*</span>
                                <br />
                                <textarea id="txtEmailID" name="receiveremailIDs" class="required" rows="5" cols="60"
                                    onclick="WishItem.HideMessage();"></textarea>
                                <br />
                                <p class="errorMessage">
                                    <span class="cssClassRequired sfLocale">Enter Valid EmailID with comma separated</span>
                                </p>
                            </li>
                            <li>
                                <label id="lblEmailMessage" class="sfLocale">
                                    Message</label><br />
                                <textarea id="txtEmailMessage" class="emailMessage" rows="5" cols="60" name="emailMessage"></textarea>
                            </li>
                        </ul>
                    </div>
                    <div class="sfButtonwrapper">
                        <label class="cssClassGreenBtn i-share">
                            <button type="button" id="btnShareWishItem">
                                <span class="sfLocale">Share WishList</span></button></label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnWishItem" />
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxWishItems
        });
        if ($("#divLoadUserControl").length != 0) {
            $(".sfLocale").localize({
                moduleKey: AspxUserDashBoard
            });
        }
        var userModuleIDWishList = "<%= UserModuleIDWishList%>";
        if (userModuleIDWishList == 0) {
            userModuleIDWishList = userModuleIDUD;
        }
        $(this).WishItemList({
            CountryName: aspxCountryName,
            UserEmailIDWishList: '<%=UserEmailWishList %>',
            ServerNameVariables: '<%=Request.ServerVariables["SERVER_NAME"]%>',
            AllowAddToCart: '<%=AllowAddToCart %>',
            AllowOutStockPurchaseSetting: '<%=AllowOutStockPurchase %>',
            ShowImageInWishlistSetting: '<%=ShowImageInWishlist %>',
            NoImageWishListSetting: '<%=NoImageWishList%>',
            CurrentPage: 0,
            RowTotal: '<%=RowTotal %>',
            ArrayLength: '<%=ArrayLength %>',
            ServicePath: '<%=ServicePath %>',
            UserFullName: '<%=UserFullName%>',
            userModuleIDWishList: userModuleIDWishList
        });
        $('.cssClassImage img[title]').tipsy({ gravity: 'n' });
        //$(".i-delete").tipsy({ gravity: 'n' });
    });
    //]]>
</script>