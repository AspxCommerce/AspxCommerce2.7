<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewTaggedItems.ascx.cs" Inherits="Modules_AspxCommerce_AspxPopularTags_ViewTaggedItems" %>
<div id="divTagItemViewOptions" class="viewWrapper">
    <div id="divViewAs" class="view">
        <span class="sfLocale">View as:</span>
        <select id="ddlViewTagItemAs" class="sfListmenu" style="display: none">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        <span class="sfLocale">Sort by:</span>
        <select id="ddlSortTagItemBy" class="sfListmenu">
            <option value=""></option>
        </select>
    </div>
</div>
<div id="divShowTagItemResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<div class="cssClassPageNumber" id="divTagItemPageNumber">
    <div class="cssClassPageNumberMidBg">
        <div id="Pagination">
        </div>
        <div class="cssClassViewPerPage">
            <h4 class="sfLocale">
                View Per Page:
            </h4>
            <select id="ddlTagItemPageSize" class="sfListmenu">
                <option value=""></option>
            </select></div>
    </div>
</div>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxPopularTags
        });
        $(this).TaggedItem({
            PopularTagsModulePath: "<%=PopularTagsModulePath %>",
            AllowAddToCart: '<%=AllowAddToCart %>',
            AllowOutStockPurchase: '<%=AllowOutStockPurchase %>',
            NoImageTagItemPath: '<%=NoImageTagItemsPath %>',
            AllowWishListTagItem: '<%=AllowWishListViewTagsItems %>',
            NoOfItemsInRow: '<%=NoOfItemsInARow %>',
            Displaymode: '<%=ItemDisplayMode %>'.toLowerCase(),
            TagsIDs: '<%=TagsIDs %>'
        });
    }); 

    //]]>                                              
    
</script>