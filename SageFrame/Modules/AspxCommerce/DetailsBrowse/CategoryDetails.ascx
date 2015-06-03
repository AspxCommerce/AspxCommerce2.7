<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryDetails.ascx.cs"
    Inherits="Modules_AspxDetails_AspxCategoryDetails_CategoryDetails" %>
<div id="divShopFilter" class="cssClassFilter" style="display: none;">
    <div id="tblFilter">
        <h2 class="cssClassLeftHeader">
            <span></span>
        </h2>
        <asp:Literal ID="ltrFilter" runat="server" EnableViewState="false"></asp:Literal>
    </div>
</div>
<div class="cssCategoryWrapper">
    <%--<div id="divHeader" class="cssClassSlider" style="display: none;">
    </div>--%>
        <asp:Literal ID="ltrCatSlider" runat="server" EnableViewState="false"></asp:Literal>
        <asp:Literal ID="ltrSortView" runat="server" EnableViewState="false"></asp:Literal>
<%--    </div>--%>
    <div id="divShowCategoryItemsList" class="cssClassDisplayResult clearfix">
        <asp:Literal ID="ltrCategoryItems" runat="server" EnableViewState="false"></asp:Literal>
    </div>
    <!-- TODO:: paging Here -->
    <asp:Literal ID="ltrPagination" runat="server" EnableViewState="false"></asp:Literal>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: DetailsBrowse
        });
    });
    var maxPrice = parseFloat('<%=maxPrice %>');
    var minPrice = parseFloat('<%=minPrice %>');
    var isCategoryHasItems = parseInt('<%=IsCategoryHasItems %>');
    var categorykey = "<%=Categorykey%>";
    var allowAddToCart = '<%=AllowAddToCart %>';
    var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
    var noImageCategoryDetailPath = '<%=NoImageCategoryDetailPath %>';
    var noOfItemsInRow = '<%=NoOfItemsInARow %>';
    var displaymode = '<%=ItemDisplayMode %>'.toLowerCase();
    //]]>
</script>