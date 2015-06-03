var NewAccountReport;
$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    NewAccountReport = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            url: "",
            method: ""
        },
        init: function() {
            $("input[id$='HdnValue']").val($("#ddlNewAccountReport").val());
            NewAccountReport.BindNewAccountReport(true, false, false);
            $("#ddlNewAccountReport").change(function() {
                NewAccountReport.ShowReport();
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: NewAccountReport.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: NewAccountReport.config.contentType,
                cache: NewAccountReport.config.cache,
                async: NewAccountReport.config.async,
                url: NewAccountReport.config.url,
                data: NewAccountReport.config.data,
                dataType: NewAccountReport.config.dataType,
                success: NewAccountReport.ajaxSuccess,
                error: NewAccountReport.ajaxFailure
            });
        },
        ShowReport: function() {
            var selectreport = $("#ddlNewAccountReport").val();
            $("input[id$='HdnValue']").val(selectreport);
            switch (selectreport) {
                case '1':
                    NewAccountReport.BindNewAccountReport(true, false, false);
                    break;
                case '2':
                    NewAccountReport.BindNewAccountReport(false, true, false);
                    break;
                case '3':
                    NewAccountReport.BindNewAccountReport(false, false, true);
                    break;
            }
        },


        BindNewAccountReport: function(monthly, weekly, hourly) {
            this.config.method = "GetNewAccounts";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvNewAccountList_pagesize").length > 0) ? $("#gdvNewAccountList_pagesize :selected").text() : 10;

            $("#gdvNewAccountList").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCustomerManagement, "Period"), name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCustomerManagement, "Number Of New Accounts"), name: 'new_acounts', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCustomerManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                ],
                buttons: [
                ],
                rp: perpage,
                nomsg: getLocale(AspxCustomerManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj(), monthly: monthly, weekly: weekly, hourly: hourly },
                current: current_,
                pnew: offset_,
                sortcol: { 2: { sorter: false} }
            });
        },
        ajaxSuccess: function(msg) {
            switch (NewAccountReport.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    };
    NewAccountReport.init();
});