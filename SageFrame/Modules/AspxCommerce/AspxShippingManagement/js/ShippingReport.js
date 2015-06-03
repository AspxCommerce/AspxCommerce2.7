var ShippingReport = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    ShippingReport = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            data: '{}',
            dataType: "json",
            url: "",
            method: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: ShippingReport.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ShippingReport.config.contentType,
                cache: ShippingReport.config.cache,
                async: ShippingReport.config.async,
                url: ShippingReport.config.url,
                data: ShippingReport.config.data,
                dataType: ShippingReport.config.dataType,
                success: ShippingReport.ajaxSuccess,
                error: ShippingReport.ajaxFailure
            });
        },
        init: function() {
            ShippingReport.LoadShippingReportStaticImage();
            ShippingReport.ShippedReportDetails(null, true, false, false);
            $("input[id$='HdnValue']").val($("#ddlShippingReport").val());
            $("#ddlShippingReport").change(function() {
                ShippingReport.ShowReport();
            });
            $('#txtShippingMethodNm').keyup(function(event) {
                if (event.keyCode == 13) {
                    ShippingReport.SearchShippingReport();
                }
            });
        },
        LoadShippingReportStaticImage: function() {
            $('#ajaxShippingReportImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ShowReport: function() {
            var selectreport = $("#ddlShippingReport").val();
            $("input[id$='HdnValue']").val(selectreport);
            switch (selectreport) {
                case '1':
                    ShippingReport.ShippedReportDetails(null, true, false, false);
                    break;
                case '2':
                    ShippingReport.ShippedReportDetails(null, false, true, false);
                    break;
                case '3':
                    ShippingReport.ShippedReportDetails(null, false, false, true)
                    break;
            }
        },

        ShippedReportDetails: function(shippingName, monthly, weekly, hourly) {
            var ShippedReportObj = {
                ShippingMethodName: shippingName,
                Monthly: monthly,
                Weekly: weekly,
                Hourly: hourly
            };
            this.config.method = "GetShippedDetails";
            this.config.data = { aspxCommonObj: aspxCommonObj, ShippedReportObj: ShippedReportObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShippedReportDetails_pagesize").length > 0) ? $("#gdvShippedReportDetails_pagesize :selected").text() : 10;

            $("#gdvShippedReportDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                     { display: getLocale(AspxShippingManagement, 'Shipping Method ID'), name: 'shipping_methodId', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: getLocale(AspxShippingManagement, 'Shipping Method Name'), name: 'shipping_method_name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Number Of Orders'), name: 'num_of_orders', cssclass: 'cssClassLinkHeader', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Total Shipping'), name: 'ship_to_name', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Period'), name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                 ],
                buttons: [
                                ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, ShippedReportObj: ShippedReportObj },
                current: current_,
                pnew: offset_,
                sortcol: { o: { sorter: true }, 5: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ajaxSuccess: function(msg) {
            switch (ShippingReport.config.ajaxCallMode) {
                case 0:
                    break;
            }
        },
        SearchShippingReport: function() {
            var Nm = $.trim($("#txtShippingMethodNm").val());
            if (Nm.length < 1) {
                Nm = null;
            }
            var selectreport = $("#ddlShippingReport").val();
            switch (selectreport) {
                case '1':
                    ShippingReport.ShippedReportDetails(Nm, true, false, false);
                    break;
                case '2':
                    ShippingReport.ShippedReportDetails(Nm, false, true, false);
                    break;
                case '3':
                    ShippingReport.ShippedReportDetails(Nm, false, false, true);
                    break;
            }
        }
    };
    ShippingReport.init();
});