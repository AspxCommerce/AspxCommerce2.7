$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var MostViewedItemCount = 5;

    var mostViewedItems = {
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
                type: mostViewedItems.config.type,
                contentType: mostViewedItems.config.contentType,
                cache: mostViewedItems.config.cache,
                async: mostViewedItems.config.async,
                url: mostViewedItems.config.url,
                data: mostViewedItems.config.data,
                dataType: mostViewedItems.config.dataType,
                success: mostViewedItems.ajaxSuccess,
                error: mostViewedItems.ajaxFailure
            });
        },
        GetMostViewedItemAdmindash: function() {
            this.config.url = this.config.baseURL + "GetMostViwedItemAdmindash";
            this.config.data = JSON2.stringify({ count: MostViewedItemCount, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (mostViewedItems.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                if (msg.d.length > 0) {
                    var bodyElements = '';
                    var headELements = '';
                    headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                    headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard,"Item Name")+'</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard,"Price")+'</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard,"Views")+'</td>';
                    headELements += '</tr></tbody></table>';
                    $("#divMostViewedItemAdmindash").html(headELements);

                    $.each(msg.d, function(index, value) {
                        bodyElements += '<tr><td><label class="cssClassLabel">' + value.ItemName + '</label></td>';
                        if (value.ItemTypeID == 5) {
                            bodyElements += '<td class="cssClassAlignRight">' + getLocale(AspxAdminDashBoard, "Starting At") + '<label class="cssClassLabel cssClassFormatCurrency">' + value.Price.toFixed(2) + '</label></td>';
                        }
                        else {
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.Price.toFixed(2) + '</label></td>';
                        }
                    bodyElements += '<td><label class="cssClassLabel">' + value.ViewCount + '</label>';
                    bodyElements += '</tr>';
                    });
                    $("#divMostViewedItemAdmindash").find('table>tbody').append(bodyElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $(".classTableWrapper > tbody tr:even").addClass("sfEven");
                    $(".classTableWrapper > tbody tr:odd").addClass("sfOdd");
                } else {
                    $("#divMostViewedItemAdmindash").html("<span class=\"cssClassNotFound\">&nbsp;&nbsp;&nbsp; " + getLocale(AspxAdminDashBoard,'No Data Found!!')+"</span>");
                }
                break;
            }
        },
        ajaxFailure: function(msg) {
            switch (mostViewedItems.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h1>' + getLocale(AspxAdminDashBoard,"Error Message")+'</h1><p>' + getLocale(AspxAdminDashBoard,"Failed to load Most Viewed Items.")+'</p>');
                break;
            }
        },
        init: function(config) {
            mostViewedItems.GetMostViewedItemAdmindash();
        }
    };
    mostViewedItems.init();
});
