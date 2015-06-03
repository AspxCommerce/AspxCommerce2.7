<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsListByIds.ascx.cs"
    Inherits="Modules_Admin_DetailsBrowse_ItemsListByIds" %>

<script type="text/javascript">
    //<![CDATA[
    var ItemList = "";
    $(function () {

        $(".sfLocale").localize({
            moduleKey: DetailsBrowse
        });
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var itemIds = "<%=ItemIds%>";
        var allowAddToCart = '<%=AllowAddToCart %>';
        var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
        var noImageItemListByIdsPath = '<%=DefaultShoppingOptionImgPath %>';       
        var noOfItemsInRow = '<%=NoOfItemsInARow %>';
        var displaymode = '<%=ItemDisplayMode %>'.toLowerCase();
        var currentPage = 0;
        var templateScriptArr = [];
        ItemList = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: "",
                error: ""
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: ItemList.config.type,
                    contentType: ItemList.config.contentType,
                    cache: ItemList.config.cache,
                    async: ItemList.config.async,
                    url: ItemList.config.url,
                    data: ItemList.config.data,
                    dataType: ItemList.config.dataType,
                    success: ItemList.config.ajaxCallMode,
                    error: ItemList.config.error
                });
            },

            BindShoppingOptionsItemsResult: function (msg) {
                if (msg.d.length > 0) {
                    $("#divOptionItemViewOptions").show();
                    $("#divOptionPageNumber").show();
                }
                BindTemplateDetails('divOptionsSearchResult', 'divOptionItemViewOptions', 'divOptionViewAs', 'ddlOptionViewAs', 'ddlOptionSortBy', 'divOptionPageNumber', 'OptionPagination', 'ddlOptionPageSize', currentPage, msg, ItemList.BindShoppingOptionResultItems, 'ItemList', allowAddToCart, allowOutStockPurchase, allowWishListItemListByIds, noImageItemListByIdsPath, noOfItemsInRow, displaymode, templateScriptArr);
            },

            GetShoppingOptionLoadErrorMsg: function () {
                csscody.error("<h2>" + getLocale(DetailsBrowse, "Error Message") + "</h2><p>" + getLocale(DetailsBrowse, "Sorry, Failed to load shopping option results!") + "</p>");
            },

            BindShoppingOptionResultItems: function (offset, limit, currentpage1, sortBy) {
                currentPage = currentpage1;
                var brandIDs = $.cookies.get("BrandIds");
                var lowerLimit = parseFloat($.cookies.get("lowerLimit"));
                var upperLimit = parseFloat($.cookies.get("upperLimit"));
                ItemList.config.method = "AspxCommerceWebService.asmx/GetShoppingOptionsItemsResultByBrandAndPrice";
                ItemList.config.url = ItemList.config.baseURL + ItemList.config.method;
                ItemList.config.data = JSON2.stringify({ offset: offset, limit: limit, brandIDs: brandIDs, priceFrom: lowerLimit, priceTo: upperLimit, SortBy: sortBy, aspxCommonObj: aspxCommonObj });
                ItemList.config.ajaxCallMode = ItemList.BindShoppingOptionsItemsResult;
                ItemList.config.error = ItemList.GetShoppingOptionLoadErrorMsg;
                ItemList.ajaxCall(ItemList.config);

            },

            ShoppingOptionsHideAll: function () {
                $("#divOptionItemViewOptions").hide();
                $("#divOptionPageNumber").hide();
                $("#divOptionsSearchResult").hide();
            },

            Init: function () {
                $.each(jsTemplateArray, function (index, value) {
                    var tempVal = jsTemplateArray[index].split('@');
                    var templateScript = {
                        TemplateKey: tempVal[0],
                        TemplateValue: tempVal[1]
                    };
                    templateScriptArr.push(templateScript);
                });
                CreateDdlPageSizeOption('ddlOptionPageSize');
                                ItemList.ShoppingOptionsHideAll();
                createDropDown('ddlOptionSortBy', 'divSortBy', 'sortBy', displaymode);
                createDropDown('ddlOptionViewAs', 'divOptionViewAs', 'viewAs', displaymode);
                ItemList.BindShoppingOptionResultItems(1, $('#ddlOptionPageSize').val(), 0, $("#ddlOptionSortBy option:selected").val());


                $("#ddlOptionViewAs").on("change", function () {
                    BindResults('divOptionsSearchResult', 'divOptionViewAs', 'ddlOptionViewAs', null, allowOutStockPurchase, allowAddToCart,  noImageItemListByIdsPath, noOfItemsInRow, displaymode);

                });
                $("#divOptionViewAs").find('a').on('click', function () {
                    $("#divOptionViewAs").find('a').removeClass('sfactive');
                    $(this).addClass("sfactive");
                    BindResults('divOptionsSearchResult', 'divOptionViewAs', 'ddlOptionViewAs', null, allowOutStockPurchase, allowAddToCart,  noImageItemListByIdsPath, noOfItemsInRow, displaymode);

                });

                $("#ddlOptionSortBy").on("change", function () {
                    var items_per_page = $("#ddlOptionPageSize").val();
                    var offset = 1;
                    ItemList.BindShoppingOptionResultItems(offset, items_per_page, 0, $("#ddlOptionSortBy option:selected").val());
                });

                $("#ddlOptionPageSize").bind("change", function () {
                    var items_per_page = $(this).val();
                    var offset = 1;
                    ItemList.BindShoppingOptionResultItems(offset, items_per_page, 0, $("#ddlOptionSortBy option:selected").val());
                });
            }
        };
        $(".cssClassMasterLeft").html('');
        $("#divCenterContent").removeClass("cssClassMasterWrapperLeftCenter");
        $("#divCenterContent").addClass("cssClassMasterWrapperCenter");
        ItemList.Init();
    });

    //]]>
    
</script>

<div id="divOptionItemViewOptions" class="viewWrapper" style="display:none">
    <div id="divOptionViewAs" class="view">
        <span class="sfLocale">View as:</span>
        <select id="ddlOptionViewAs" class="sfListmenu" style="display: none">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        <span class="sfLocale">Sort by:</span>
        <select id="ddlOptionSortBy" class="sfListmenu">
        </select>
    </div>
</div>
<div id="divOptionsSearchResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<!-- TODO:: paging Here -->
<div class="cssClassPageNumber" id="divOptionPageNumber" style="display:none">
    <div class="cssClassPageNumberMidBg">
        <div id="OptionPagination">
        </div>
        <div class="cssClassViewPerPage">
            <h4 class="sfLocale">
                View Per Page:
            </h4>
            <select id="ddlOptionPageSize" class="sfListmenu">
                <option value=""></option>
            </select></div>
    </div>
</div>
