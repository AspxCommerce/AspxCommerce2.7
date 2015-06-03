$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID()
    };

    var inventoryDetails = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: inventoryDetails.config.type,
                contentType: inventoryDetails.config.contentType,
                cache: inventoryDetails.config.cache,
                async: inventoryDetails.config.async,
                url: inventoryDetails.config.url,
                data: inventoryDetails.config.data,
                dataType: inventoryDetails.config.dataType,
                success: inventoryDetails.ajaxSuccess,
                error: inventoryDetails.ajaxFailure
            });
        },
        GetInventoryDetails: function() {
            this.config.url = this.config.baseURL + "GetInventoryDetails";
            this.config.data = JSON2.stringify({ count: lowStock, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (inventoryDetails.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item !== null) {
                                $('#lblItemtotal').html(item.TotalItem);
                                $('#lblAvtive').html(item.Active);
                                $('#lblHidden').html(item.Hidden);
                                $('#lblDownloadable').html(item.DItemscountNo);
                                $('#lblSpecial').html(item.SItemsCountNo);
                                $('#lblLowstock').html(item.LowStockItemCount);
                                $('#lblGroupItem').html(item.GroupItemCount);
                                $('#lblKitItem').html(item.KitItemCount);
                            }
                        };
                    } else {
                        $('#lblItemtotal').html('0');
                        $('#lblAvtive').html('0');
                        $('#lblHidden').html('0');
                        $('#lblDownloadable').html('0');
                        $('#lblSpecial').html('0');
                        $('#lblLowstock').html('0');
                        $('#lblGroupItem').html('0');
                        $('#lblKitItem').html('0');
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (inventoryDetails.config.ajaxCallMode) {
                case 0:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Failed to connect with server!") + '</p>');
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Failed to load Inventory Details!") + '</p>');
                    break;
            }
        },
        init: function() {
            inventoryDetails.GetInventoryDetails();
        }
    };
    inventoryDetails.init();
});