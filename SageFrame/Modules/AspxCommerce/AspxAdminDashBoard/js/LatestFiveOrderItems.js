$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var latestOrderItemCount = 5;

    var latestOrderItems = {
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
                type: latestOrderItems.config.type,
                contentType: latestOrderItems.config.contentType,
                cache: latestOrderItems.config.cache,
                async: latestOrderItems.config.async,
                url: latestOrderItems.config.url,
                data: latestOrderItems.config.data,
                dataType: latestOrderItems.config.dataType,
                success: latestOrderItems.ajaxSuccess,
                error: latestOrderItems.ajaxFailure
            });
        },
        GetLatestOrderItems: function() {
            this.config.url = this.config.baseURL + "GetLatestOrderItems";
            this.config.data = JSON2.stringify({ count: latestOrderItemCount, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (latestOrderItems.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                var length = msg.d.length;
                if (length > 0) {
                    var bodyElements = '';
                    var headELements = '';
                    
                    headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                    headELements += '<tr class="cssClassHeading">';                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Customer") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Ordered Date") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Grand Total") + '</td>';
                    headELements += '</tr></tbody></table>';
                    $("#divLatestOrderStatics").html(headELements);
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (value !== null) {
                            bodyElements += '<tr>';
                            bodyElements += '<td><label class="cssClassLabel">' + (value.FirstName != null ? value.FirstName : '') + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + (value.AddedOn != null ? value.AddedOn : '') + '</label>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + (value.GrandTotal != null ? (value.GrandTotal).toFixed(2) : 0) + '</label>';
                            bodyElements += '</tr>';
                        }
                    };

                    $("#divLatestOrderStatics").find('table>tbody').append(bodyElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $(".classTableWrapper > tbody tr:even").addClass("sfEven");
                    $(".classTableWrapper > tbody tr:odd").addClass("sfOdd");
                } else {
                    $("#divLatestOrderStatics").html("<span class=\"cssClassNotFound\">&nbsp;&nbsp;&nbsp;" + getLocale(AspxAdminDashBoard, "No Data Found!!") + "</span>");
                }
                break;
            }
        },
        ajaxFailure: function(msg) {
            switch (latestOrderItems.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h1> ' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p> '+getLocale(AspxAdminDashBoard, "Failed to load Latest Ordered Items.")+'</p>');
                break;
            }
        },
        init: function() {
            latestOrderItems.GetLatestOrderItems();
        }
    };
    latestOrderItems.init();
});