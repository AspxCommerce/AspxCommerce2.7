<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialItemsViewAll.ascx.cs" Inherits="Modules_AspxCommerce_AspxSpecials_SpecialItemsViewAll" %>

<script type="text/javascript">
    var noImageCategoryDetailPath = '<%=NoImageCategoryDetailPath %>';
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxSpecials
        });
        $(this).SpecialItemsView({
            AllowOutStockPurchase: '<%=AllowOutStockPurchase %>',
            SpecialItemModulePath: '<%=SpecialItemModulePath %>',           
            ArrayLength: '<%=ArrayLength %>',
            RowTotal: '<%=RowTotal %>',
            VarFunction: "<%=VarFunction %>"
        });
    });
  
</script>
<div id="divDetailsItemsList" class="cssClassItemListView">
    <div id="divItemHeader" class="cssClassItemHeader cssClassBMar30">
        <h2 id="h2ItemHeader" class="cssClassMiddleHeader"><span></span>
        </h2>
        <div id="divSortBy" class="sort" style="display: none;">
           
       <%--   <h4>Sort by:</h4>
            <select id="ddlSortItemDetailBy" class="sfListmenu">
            </select>--%>
            <asp:Literal ID="ltrItemViewDetailSortBy" runat="server" 
                EnableViewState="False" ></asp:Literal>
        </div>
    </div>
    <div class="itemList">
        <div id="divDetailItems" class="divDetailsItemList sfHorizontalbx-wrapper">
            <asp:Literal ID="ltrItemViewDetail" runat="server" EnableViewState="False" 
                meta:resourcekey="ltrItemViewDetailResource1"></asp:Literal>
        </div>
        <div class="cssClassPageNumber" id="divItemSearchPageNumber" style="display: none;">
            <div class="cssClassPageNumberMidBg">
                <div id="ItemPagination">
                </div>
                <div class="cssClassViewPerPage">
                    <span class="sfLocale">
                        View Per Page:
                    </span>
                    <select id="ddlItemPageSize" class="sfListmenu">
                        <option value=""></option>
                    </select></div>
            </div>
        
        </div>
    </div>
</div>
<input type="hidden" id="hdnPrice" />
<input type="hidden" id="hdnWeight" />
<input type="hidden" id="hdnQuantity" />
