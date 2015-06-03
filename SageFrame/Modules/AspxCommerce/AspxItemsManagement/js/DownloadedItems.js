var DownloadedItems;
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
    DownloadedItems = {
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
            DownloadedItems.LoadDownloadableItemStaticImage();
            DownloadedItems.BindDownLoadableItemsGrid(null, null);
            $("#btnSearchDownloadedItems").click(function() {
                DownloadedItems.SearchItems();
            });

            $('.cssClassDownload').jDownload({
                root: rootPath,
                dialogTitle: getLocale(AspxItemsManagement, 'AspxCommerce Download Sample Item:')
            });
            $('#txtSearchName,#txtSearchSKU').keyup(function(event) {
                if (event.keyCode == 13) {
                    DownloadedItems.SearchItems();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: DownloadedItems.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: DownloadedItems.config.contentType,
                cache: DownloadedItems.config.cache,
                async: DownloadedItems.config.async,
                url: DownloadedItems.config.url,
                data: DownloadedItems.config.data,
                dataType: DownloadedItems.config.dataType,
                success: DownloadedItems.ajaxSuccess,
                error: DownloadedItems.ajaxFailure
            });
        },
        LoadDownloadableItemStaticImage: function() {
            $('#ajaxDownloadableItemImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
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

            DownloadedItems.BindDownLoadableItemsGrid(sku, Nm);
        },

        BindDownLoadableItemsGrid: function(sku, Nm) {
            var downloadableItem = {
                SKU: sku,
                ItemName: Nm,
                CheckUser: false
            };
            this.config.method = "GetDownLoadableItemsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvDownLoadableItems_pagesize").length > 0) ? $("#gdvDownLoadableItems_pagesize :selected").text() : 10;

            $("#gdvDownLoadableItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Sample Link'), name: 'sample_link', cssclass: '', controlclass: 'cssClassDownload', coltype: 'linklabel', align: 'left', value: '4', downloadarguments: '', downloadmethod: '' },
                    { display: getLocale(AspxItemsManagement, 'Actual Link'), name: 'actual_link', cssclass: '', controlclass: 'cssClassDownload', coltype: 'linklabel', align: 'left', value: '5', downloadarguments: '', downloadmethod: '' },
                    { display: 'Sample File', name: 'sample_file', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actual File', name: 'actual_file', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxItemsManagement, 'Purchases'), name: 'purchase', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Downloads'), name: 'download', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: { downloadableObj: downloadableItem, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: {}
            });
        },
        ajaxSuccess: function(msg) {
            switch (DownloadedItems.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    };
    DownloadedItems.init();
});
   