
(function ($) {
    $.SpecialItemsViewAll = function (p) {
        p = $.extend({
            AllowOutStockPurchase: "",
            SpecialItemModulePath: "",          
            ArrayLength: "",
            RowTotal: "",
            VarFunction: ""
        }, p);
        var costVarIDArr = new Array();
        var calledID = new Array();
        var currentPage = 0;
        var arrItemListType = new Array();
        var url = "";
        var QueryString = "";
        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                UserName: AspxCommerce.utils.GetUserName()
            };
            return aspxCommonObj;
        };
        var items_per_page = '';
        var SpecialItemsDetails = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxRootPath + "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsHandler.ashx/",
                method: "",
                url: aspxservicePath + "AspxCommerceWebService.asmx/",
                userName: userName,
                ajaxCallMode: 0,                itemid: 0
            },
            vars: {
                countCompareItems: 0
            },

            BindItemsSortByDropDown: function () {
                SpecialItemsDetails.config.method = "BindItemsSortByList";
                SpecialItemsDetails.config.url = SpecialItemsDetails.config.url + SpecialItemsDetails.config.method;
                SpecialItemsDetails.config.data = "{}";
                SpecialItemsDetails.config.ajaxCallMode = SpecialItemsDetails.BindItemsSortByDropDownSuccess;
                SpecialItemsDetails.ajaxCall(SpecialItemsDetails.config);
            },

            BindItemsSortByDropDownSuccess: function (data) {
                if (data.d.length > 0) {
                    $("#ddlSortItemDetailBy").html('');
                    $.each(data.d, function (index, item) {
                        var displayOptions = "<option data-html-text='" + item.OptionType + "' value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>";
                        $("#ddlSortItemDetailBy").append(displayOptions);
                    });
                }
            },

            LoadAllSpecialItems: function (offset, limit, current, sortBy) {
                currentPage = current;
                SpecialItemsDetails.config.method = "GetAllSpecialItems";
                SpecialItemsDetails.config.url = SpecialItemsDetails.config.baseURL + SpecialItemsDetails.config.method;
                SpecialItemsDetails.config.data = JSON2.stringify({ offset: offset, limit: limit, aspxCommonObj: AspxCommonObj(), sortBy: sortBy, rowTotal: p.RowTotal });
                SpecialItemsDetails.config.ajaxCallMode = SpecialItemsDetails.BindAllSpecialItems;
                SpecialItemsDetails.ajaxCall(SpecialItemsDetails.config);
            },

            BindAllSpecialItems: function (data) {
                ItemViewList('divDetailsItemList', 'ddlItemPageSize', 'ItemPagination', 'divItemSearchPageNumber', data, arrItemListType, p.RowTotal, p.AllowOutStockPurchase, SpecialItemsDetails.LoadAllHeavyDiscountItems, currentPage, 'SpecialItemsDetails', costVarIDArr, calledID, 'ddlSortItemDetailBy', 'Medium');
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: SpecialItemsDetails.config.type,
                    contentType: SpecialItemsDetails.config.contentType,
                    cache: SpecialItemsDetails.config.cache,
                    async: SpecialItemsDetails.config.async,
                    url: SpecialItemsDetails.config.url,
                    data: SpecialItemsDetails.config.data,
                    dataType: SpecialItemsDetails.config.dataType,
                    success: SpecialItemsDetails.config.ajaxCallMode,
                    error: SpecialItemsDetails.config.ajaxFailure
                });
            },

            Init: function () {
                var offset = 1;
                $("#ddlItemPageSize").html('');
                var itemPageSize = '';
                itemPageSize += "<option data-html-text='8' value='8'>" + 8 + "</option>";
                itemPageSize += "<option data-html-text='16' value='16'>" + 16 + "</option>";
                itemPageSize += "<option data-html-text='24' value='24'>" + 24 + "</option>";
                itemPageSize += "<option data-html-text='32' value='32'>" + 32 + "</option>";
                itemPageSize += "<option data-html-text='40' value='40'>" + 40 + "</option>";
                itemPageSize += "<option data-html-text='64' value='64'>" + 64 + "</option>";
                $("#ddlItemPageSize").html(itemPageSize);
                url = window.location.href;
                QueryString = url.substring(url.indexOf('id=') + 3);
               
                if (QueryString == "special") {
                    $('#h2ItemHeader').html("<span> " + getLocale(AspxSpecials, 'Special Items') + " </span>");
                    $("#ddlItemPageSize").change(function () {
                        items_per_page = $(this).val();
                        SpecialItemsDetails.LoadAllSpecialItems(offset, items_per_page, 0, $("#ddlSortItemDetailBy option:selected").val());
                    });
                    $("#ddlSortItemDetailBy").change(function () {
                        items_per_page = $('#ddlItemPageSize').val();
                        SpecialItemsDetails.LoadAllSpecialItems(offset, items_per_page, 0, $("#ddlSortItemDetailBy option:selected").val());
                    });
                }
                if (parseInt(p.ArrayLength) > 0) {
                    items_per_page = $("#ddlItemPageSize").val();
                    $('#ItemPagination').pagination(p.RowTotal, {
                                               items_per_page: items_per_page,
                                               current_page: currentPage,
                        callfunction: true,
                        function_name: { name: "SpecialItemsDetails." + p.VarFunction, limit: $('#ddlItemPageSize').val(), sortBy: $('#ddlSortItemDetailBy').val() },
                        prev_text: getLocale(AspxSpecials, "Prev"),
                        next_text: getLocale(AspxSpecials, "Next"),
                        prev_show_always: false,
                        next_show_always: false
                    });
                    $('#divItemSearchPageNumber').show();
                }
                $('#divSortBy').show();
                $('#divItemSearchPageNumber').show();
                           }
        };
        SpecialItemsDetails.Init();
    };
    $.fn.SpecialItemsView = function (p) {
        $.SpecialItemsViewAll(p);
    }
})(jQuery);