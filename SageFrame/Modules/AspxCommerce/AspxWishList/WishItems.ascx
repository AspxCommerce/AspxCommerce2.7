<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItems.ascx.cs" Inherits="Modules_AspxWishItems_WishItems" %>
<div class="cssClassLeftSideBox cssClassRecentWishWrapper" id="divRecentlyAddedWishList">
    <div class="cssClassWishItemsHeader">
        <h2 class="cssClassLeftHeader">
            <asp:Label ID="lblRecentAddedWishItemsTitle" runat="server" Text="Recent Wished Items"
                CssClass="cssClassWishItem" meta:resourcekey="lblRecentAddedWishItemsTitleResource1"></asp:Label></h2>
    </div>
    <div id="wishItem">
        <asp:Literal ID="ltrWishItem" runat="server" EnableViewState="False"></asp:Literal>
    </div>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxWishItems
        });
        if (ModuleCollapsible.toLowerCase() == 'true') {
            $(".cssClassWishItemsHeader").addClass("sfCollapsible");
            $(".cssClassWishItemsHeader").bind('click', function () {
                $("#wishItem").slideToggle('fast');
            });
        }
    });
    var countryNameWishList = '<%=CountryName %>';
    var wishListNoImagePath = '<%=NoImageWishItemPath %>';
    var showWishItemImage = '<%=ShowWishedItemImage %>';
    var wishListURLSetting = '<%=WishListURL%>';
    var noOfRecentAddedWishItems = '<%=NoOfRecentAddedWishItems%>';
    var ModuleCollapsible = "<%=ModuleCollapsible %>";
    //]]>
</script>