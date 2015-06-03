var OrderedItems = "";
$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    OrderedItems = {
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
            OrderedItems.LoadOrderedItemStaticImage();
            OrderedItems.BindOrderedItemsGrid(null);
            $("#btnExportToCSV").click(function() {
                $('#gdvOrderedItems').table2CSV();
            });
            $('#txtSearchName').keyup(function(event) {
                if (event.keyCode == 13) {
                    OrderedItems.SearchItems();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: OrderedItems.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: OrderedItems.config.contentType,
                cache: OrderedItems.config.cache,
                async: OrderedItems.config.async,
                url: OrderedItems.config.url,
                data: OrderedItems.config.data,
                dataType: OrderedItems.config.dataType,
                success: OrderedItems.ajaxSuccess,
                error: OrderedItems.ajaxFailure
            });
        },
        LoadOrderedItemStaticImage: function() {
            $('#ajaxOrderedItemImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        SearchItems: function() {
            var Nm = $.trim($("#txtSearchName").val());
            if (Nm.length < 1) {
                Nm = null;
            }
            OrderedItems.BindOrderedItemsGrid(Nm);
        },
        BindOrderedItemsGrid: function(Nm) {
            this.config.method = "GetOrderedItemsList";
            this.config.data = { name: Nm, aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvOrderedItems_pagesize").length > 0) ? $("#gdvOrderedItems_pagesize :selected").text() : 10;

            $("#gdvOrderedItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'ItemID', name: 'id', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxItemsManagement,'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement,'Quantity Ordered'), name: 'quantityordered', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false } }
            });
        },
        ajaxSuccess: function(msg) {
            switch (OrderedItems.config.ajaxCallMode) {
            case 0:
                break;
            }
        }
    };
    OrderedItems.init();
});