var ReturnReport = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    ReturnReport = {
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
                type: ReturnReport.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", UserModuleIDReturn);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ReturnReport.config.contentType,
                cache: ReturnReport.config.cache,
                async: ReturnReport.config.async,
                url: ReturnReport.config.url,
                data: ReturnReport.config.data,
                dataType: ReturnReport.config.dataType,
                success: ReturnReport.ajaxSuccess,
                error: ReturnReport.ajaxFailure
            });
        },
        init: function() {
            ReturnReport.LoadReturnReportImageStaticImage();
            ReturnReport.GetReturnStatus();
            $("input[id$='HdnValue']").val($("#ddlReturnReport").val());
            ReturnReport.BindReturnReportsGrid(null, true, false, false);
            $("#ddlReturnReport").change(function() {
                ReturnReport.ShowReport();
            });
            $('#ddlReturnStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    ReturnReport.SearchItems();
                }
            });
        },
        LoadReturnReportImageStaticImage: function() {
            $('#ajaxReturnReportImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        GetReturnStatus: function() {
            this.config.url = this.config.baseURL + "GetReturnStatusList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ShowReport: function() {
            var Nm = $("#ddlReturnStatus option:selected").text();
            if ($("#ddlReturnStatus option:selected").val() == "0") {
                Nm = null;
            }
            var selectreport = $("#ddlReturnReport").val();
            $("input[id$='HdnValue']").val(selectreport);
            switch (selectreport) {
                case '1':
                    ReturnReport.BindReturnReportsGrid(Nm, true, false, false);
                    break;
                case '2':
                    ReturnReport.BindReturnReportsGrid(Nm, false, true, false);
                    break;
                case '3':
                    ReturnReport.BindReturnReportsGrid(Nm, false, false, true)
                    break;
            }
        },
        BindReturnReportsGrid: function(Nm, monthly, weekly, hourly) {

            var returnReportObj = {
                ReturnStatus: Nm,
                Monthly: monthly,
                Weekly: weekly,
                Hourly: hourly
            };
            this.config.method = "GetReturnReport";
            this.config.data = { aspxCommonObj: aspxCommonObj, returnReportObj: returnReportObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvReturnReport_pagesize").length > 0) ? $("#gdvReturnReport_pagesize :selected").text() : 10;
            $("#gdvReturnReport").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                        { display: getLocale(AspxReturnAndPolicy,'Refunded Amount'), name: 'refundAmount', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: getLocale(AspxReturnAndPolicy,'Shipping Cost'), name: 'shippingCost', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: getLocale(AspxReturnAndPolicy,'Other Postal Charges'), name: 'shippingCost', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: getLocale(AspxReturnAndPolicy,'Quantity'), name: 'quantity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
  				        { display: getLocale(AspxReturnAndPolicy,'No. of Returns'), name: 'noOfReturns', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxReturnAndPolicy,'Period'), name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                     ],
                rp: perpage,
                nomsg: getLocale(AspxReturnAndPolicy,"No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, returnReportObj: returnReportObj },
                current: current_,
                pnew: offset_,
                sortcol: { }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

        },
        SearchItems: function() {
            var Nm = $("#ddlReturnStatus option:selected").text();

            if ($("#ddlReturnStatus option:selected").val() == "0") {
                Nm = null;
            }
            var selectreport = $("#ddlReturnReport").val();
            switch (selectreport) {
                case '1':
                    ReturnReport.BindReturnReportsGrid(Nm, true, false, false);
                    break;
                case '2':
                    ReturnReport.BindReturnReportsGrid(Nm, false, true, false);
                    break;
                case '3':
                    ReturnReport.BindReturnReportsGrid(Nm, false, false, true);
                    break;
            }
        },
        ajaxSuccess: function(msg) {
            switch (ReturnReport.config.ajaxCallMode) {
                case 0:
                    break;
                case 2:
                    var item;
                    var length = msg.d.length;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        var returnStatusElements = "<option value=" + item.Value + ">" + item.Text + "</option>";
                        $("#ddlReturnStatus").append(returnStatusElements);
                    };
                    break;
            }
        }

    };
    ReturnReport.init();
});