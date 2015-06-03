var LowStockItems;
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    LowStockItems = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            method: "",
            url: ""
        },
        init: function() {
            LowStockItems.LoadLowStockItemStaticImage();
            LowStockItems.BindLowStockItemsGrid(null, null, null);
            $("#btnSearchLowStockItems").click(function() {
                LowStockItems.SearchItems();
            });
            $('#txtSearchName,#txtSearchSKU,#ddlIsActive').keyup(function(event) {
                if (event.keyCode == 13) {
                    LowStockItems.SearchItems();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: LowStockItems.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: LowStockItems.config.contentType,
                cache: LowStockItems.config.cache,
                async: LowStockItems.config.async,
                url: LowStockItems.config.url,
                data: LowStockItems.config.data,
                dataType: LowStockItems.config.dataType,
                success: LowStockItems.ajaxSuccess,
                error: LowStockItems.ajaxFailure
            });
        },
        LoadLowStockItemStaticImage: function() {
            $('#ajaxLowStockItemImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        SearchItems: function() {
            var sku = $.trim($("#txtSearchSKU").val());
            var Nm = $.trim($("#txtSearchName").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);

            LowStockItems.BindLowStockItemsGrid(sku, Nm, isAct);
        },

        BindLowStockItemsGrid: function(sku, Nm, isAct) {
            var lowStockObj = {
                SKU: sku,
                ItemName: Nm,
                IsActive: isAct
            };
            this.config.method = "GetLowStockItemsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvLowStockItems_pagesize").length > 0) ? $("#gdvLowStockItems_pagesize :selected").text() : 10;

            $("#gdvLowStockItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'ItemID', name: 'id', cssclass: 'cssClassHide', coltype: '', align: 'center', controlclass: '', hide: true },
                    { display: getLocale(AspxItemsManagement, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'quantity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Active'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: { lowStockObj: lowStockObj, aspxCommonObj: aspxCommonObj(), lowStock: lowStock },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        ajaxSuccess: function(msg) {
            switch (LowStockItems.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    };
    LowStockItems.init();
});