
(function ($) {
    $.TaggedItems = function (p) {
        p = $.extend({
            PopularTagsModulePath: "",
            AllowAddToCart: "",
            AllowOutStockPurchase: "",
            NoImageTagItemPath: "",
            AllowWishListTagItem: "",
            NoOfItemsInRow: "",
            Displaymode: "",
            TagsIDs: ""
        }, p);

        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                UserName: AspxCommerce.utils.GetUserName()
            };
            return aspxCommonObj;
        };
        var currentPage = 0;
        var templateScriptArr = [];
        var rowTotal = 0;
        var ViewTagItem = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.PopularTagsModulePath + "PopularTagsHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: "",
                error: ""
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: ViewTagItem.config.type,
                    contentType: ViewTagItem.config.contentType,
                    cache: ViewTagItem.config.cache,
                    async: ViewTagItem.config.async,
                    url: ViewTagItem.config.url,
                    data: ViewTagItem.config.data,
                    dataType: ViewTagItem.config.dataType,
                    success: ViewTagItem.config.ajaxCallMode,
                    error: ViewTagItem.config.error
                });
            },


            BindUserTaggedItems: function (msg) {
                rowTotal = msg.d[0].RowTotal;
                BindTemplateDetails('divShowTagItemResult', 'divTagItemViewOptions', 'divViewAs', 'ddlViewTagItemAs', 'ddlSortTagItemBy', 'divTagItemPageNumber', 'Pagination', 'ddlTagItemPageSize', currentPage, msg, ViewTagItem.ListTagsItems, 'ViewTagItem', p.AllowAddToCart, p.AllowOutStockPurchase, p.NoImageTagItemPath, p.NoOfItemsInRow, p.Displaymode, templateScriptArr);
            },

            GetLoadTagItemsErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxPopularTags, "Error Message") + "</h2><p>" + getLocale(AspxPopularTags, "Sorry, Failed to load tag items!") + "</p>");
            },

            TagItemHideAll: function () {
                $("#divTagItemPageNumber").hide();
                $("#divTagItemViewOptions").hide();
            },

            ListTagsItems: function (offset, limit, currentpage1, sortBy) {
                currentPage = currentpage1;
                ViewTagItem.config.method = "GetUserTaggedItems";
                ViewTagItem.config.url = this.config.baseURL + ViewTagItem.config.method;
                ViewTagItem.config.data = JSON2.stringify({ offset: offset, limit: limit, tagIDs: p.TagsIDs, SortBy: sortBy, rowTotal: rowTotal, aspxCommonObj: AspxCommonObj() });
                ViewTagItem.config.ajaxCallMode = ViewTagItem.BindUserTaggedItems;
                ViewTagItem.config.error = ViewTagItem.GetLoadTagItemsErrorMsg;
                ViewTagItem.ajaxCall(this.config);
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
                ViewTagItem.TagItemHideAll();
                CreateDdlPageSizeOption('ddlTagItemPageSize');
                               createDropDown('ddlSortTagItemBy', 'divSortBy', 'sortBy', p.Displaymode);
                createDropDown('ddlViewTagItemAs', 'divViewAs', 'viewAs', p.Displaymode);
                ViewTagItem.ListTagsItems(1, $("#ddlTagItemPageSize").val(), 0, $("#ddlSortTagItemBy option:selected").val());
                
                $("#ddlViewTagItemAs").on("change", function () {
                    BindResults('divShowTagItemResult', 'divViewAs', 'ddlViewTagItemAs', null, p.AllowOutStockPurchase, p.AllowAddToCart, p.NoImageTagItemPath, p.NoOfItemsInRow, p.Displaymode);
                });
                $("#divViewAs").find('a').on('click', function () {                   
                    $("#divViewAs").find('a').removeClass('sfactive');
                    $(this).addClass("sfactive");
                    BindResults('divShowTagItemResult', 'divViewAs', 'ddlViewTagItemAs', null, p.AllowOutStockPurchase, p.AllowAddToCart, p.NoImageTagItemPath, p.NoOfItemsInRow, p.Displaymode);
                });
                $("#ddlSortTagItemBy").on("change", function () {
                    var items_per_page = $('#ddlTagItemPageSize').val();
                    ViewTagItem.ListTagsItems(1, items_per_page, 0, $("#ddlSortTagItemBy option:selected").val());
                });

                $("#ddlTagItemPageSize").bind("change", function () {
                    var items_per_page = $(this).val();
                    ViewTagItem.ListTagsItems(1, items_per_page, 0, $("#ddlSortTagItemBy option:selected").val());
                });
            }
        };
        ViewTagItem.Init();
    };
    $.fn.TaggedItem = function (p) {
        $.TaggedItems(p);
    };
})(jQuery);
