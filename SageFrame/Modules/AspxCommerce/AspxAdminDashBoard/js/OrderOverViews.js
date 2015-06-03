$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var staticOrderStatusCount = 10;
    var orderOverViews = {
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
                type: orderOverViews.config.type,
                contentType: orderOverViews.config.contentType,
                cache: orderOverViews.config.cache,
                async: orderOverViews.config.async,
                url: orderOverViews.config.url,
                data: orderOverViews.config.data,
                dataType: orderOverViews.config.dataType,
                success: orderOverViews.ajaxSuccess,
                error: orderOverViews.ajaxFailure
            });
        },
        GetStaticOrderStatusAdminDash: function() {

            this.config.url = this.config.baseURL + "GetStaticOrderStatusAdminDash";
            var day = $.trim($("#ddlInventory").val());
            this.config.data = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (orderOverViews.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var length = msg.d.length;
                    if (length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper" width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '';
                        headELements += '<tr class="cssClassHeading"><td > ' + getLocale(AspxAdminDashBoard,"Status Name")+'</td>';
                        headELements += '<td > ' + getLocale(AspxAdminDashBoard,"Total Order")+'</td>';
                        headELements += '<td >' + getLocale(AspxAdminDashBoard,"Total Amount")+'</td>';
                        headELements += '</tr></tbody></table>';
                        $("#divStaticOrderStatusAdmindash").html(headELements);

                        var value;
                        for (var index = 0; index < length; index++) {
                            value = msg.d[index];
                            if (value !== null) {
                                bodyElements += '<tr ><td><label class="cssClassLabel">' + (value.StatusName != null ? value.StatusName : 0) + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + (value.TotalOrder != null ? value.TotalOrder : 0) + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel cssClassFormatCurrency">' + (value.TotalAmount != null ? value.TotalAmount : 0) + '</label></td>';
                                bodyElements += '</tr>';
                            }
                        };

                        $("#divStaticOrderStatusAdmindash").find('table>tbody').append(bodyElements);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $(".classTableWrapper > tbody tr:even").addClass("sfEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("sfOdd");
                    } else {
                    $("#divStaticOrderStatusAdmindash").html("<span class=\"cssClassNotFound\">" + getLocale(AspxAdminDashBoard,'No Data Found!!')+"</span>");
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (orderOverViews.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard,"Error Message")+'</h1><p>' + getLocale(AspxAdminDashBoard,"Failed to load Order Overview.")+'</p>');
                    break;
            }
        },
        init: function(config) {
            orderOverViews.GetStaticOrderStatusAdminDash();
            $("#ddlInventory").bind("change", function() {
                orderOverViews.GetStaticOrderStatusAdminDash();
            });
        }
    };
    orderOverViews.init();
});