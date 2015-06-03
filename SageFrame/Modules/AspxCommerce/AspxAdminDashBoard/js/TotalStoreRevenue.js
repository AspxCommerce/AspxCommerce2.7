$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var totalStoreRevenue = {
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
                type: totalStoreRevenue.config.type,
                contentType: totalStoreRevenue.config.contentType,
                cache: totalStoreRevenue.config.cache,
                async: totalStoreRevenue.config.async,
                url: totalStoreRevenue.config.url,
                data: totalStoreRevenue.config.data,
                dataType: totalStoreRevenue.config.dataType,
                success: totalStoreRevenue.ajaxSuccess,
                error: totalStoreRevenue.ajaxFailure
            });
        },
        GetTotalOrdererRevenueAdmindash: function() {
            this.config.url = this.config.baseURL + "GetTotalOrderAmountAdmindash";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (totalStoreRevenue.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                var length = msg.d.length;
                if (length > 0) {
                    var bodyElements = '';
                    var headELements = '';
                    headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                    headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Revenue") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Tax") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Shipping Cost") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Quantity") + '</td>';
                    headELements += '</tr></tbody></table>';
                    $("#divTotalOrderRevenueAdmindash").html(headELements);

                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (value !== null) {
                            bodyElements += '<tr class="cssClassAlignRight"><td><label class="cssClassLabel cssClassFormatCurrency">' + (value.Revenue != null ? value.Revenue.toFixed(2) : 0) + '</label></td>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + (value.TaxTotal != null ? value.TaxTotal.toFixed(2) : 0) + '</label></td>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + (value.ShippingCost != null ? value.ShippingCost.toFixed(2) : 0) + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + (value.Quantity != null ? value.Quantity : 0)+ '</label>';
                            bodyElements += '</tr>';
                        }                        
                    };
                    $("#divTotalOrderRevenueAdmindash").find('table>tbody').append(bodyElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $(".classTableWrapper > tbody tr:even").addClass("sfEven");
                    $(".classTableWrapper > tbody tr:odd").addClass("sfOdd");
                } else {
                $("#divTotalOrderRevenueAdmindash").html("<span class=\"cssClassNotFound\">" + getLocale(AspxAdminDashBoard,'No Data Found!!')+"</span>");
                }
                break;
            }
        },
        ajaxFailure: function(msg) {
            switch (totalStoreRevenue.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Total Store Revenue.") + '</p>');
                break;
            }
        },
        init: function(config) {
            totalStoreRevenue.GetTotalOrdererRevenueAdmindash();
        }
    };
    totalStoreRevenue.init();
});
