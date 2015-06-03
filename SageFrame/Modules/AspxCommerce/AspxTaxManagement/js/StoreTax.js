var StoreTax = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    StoreTax = {
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
                type: StoreTax.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: StoreTax.config.contentType,
                cache: StoreTax.config.cache,
                async: StoreTax.config.async,
                url: StoreTax.config.url,
                data: StoreTax.config.data,
                dataType: StoreTax.config.dataType,
                success: StoreTax.ajaxSuccess,
                error: StoreTax.ajaxFailure
            });
        },
        init: function() {
            StoreTax.LoadStoreTaxImageStaticImage();
            StoreTax.BindStoreTaxesGrid(null, true, false, false);
            $("input[id$='HdnValue']").val($("#ddlTaxReport").val());
            $("#ddlTaxReport").change(function() {
                StoreTax.ShowReport();
            });
            $('#txtSearchName').keyup(function(event) {
                if (event.keyCode == 13) {
                    StoreTax.SearchItems();
                }
            });
        },
        LoadStoreTaxImageStaticImage: function() {
            $('#ajaxStoreTaxImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ShowReport: function() {
            var selectreport = $("#ddlTaxReport").val();
            $("input[id$='HdnValue']").val(selectreport);
            switch (selectreport) {
                case '1':
                    StoreTax.BindStoreTaxesGrid(null, true, false, false);
                    break;
                case '2':
                    StoreTax.BindStoreTaxesGrid(null, false, true, false);
                    break;
                case '3':
                    StoreTax.BindStoreTaxesGrid(null, false, false, true)
                    break;
            }
        },
        BindStoreTaxesGrid: function(Nm, monthly, weekly, hourly) {
            this.config.method = "GetStoreSalesTaxes";
            var taxDataObj = {
                taxRuleName: Nm,
                monthly: monthly,
                weekly: weekly,
                hourly: hourly
            };
            this.config.data = { taxDataObj: taxDataObj, aspxCommonObj: aspxCommonObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvStoreTaxes_pagesize").length > 0) ? $("#gdvStoreTaxes_pagesize :selected").text() : 10;
            $("#gdvStoreTaxes").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                      { display: getLocale(AspxTaxManagement, 'Tax Name'), name: 'tax_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                      { display: getLocale(AspxTaxManagement, 'Rate'), name: 'rate', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                      { display: getLocale(AspxTaxManagement, 'Quantity'), name: 'quantity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                      { display: getLocale(AspxTaxManagement, 'In Percent'), name: 'is_percent', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                      { display: getLocale(AspxTaxManagement, 'No. of orders'), name: 'noOfOrders', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                      { display: getLocale(AspxTaxManagement, 'Tax Amount'), name: 'taxamount', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                      { display: getLocale(AspxTaxManagement, 'Period'), name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                  ],
                rp: perpage,
                nomsg: getLocale(AspxTaxManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

        },
        SearchItems: function() {
            var Nm = $.trim($("#txtSearchName").val());
            if (Nm.length < 1) {
                Nm = null;
            }
            var selectreport = $("#ddlTaxReport").val();
            switch (selectreport) {
                case '1':
                    StoreTax.BindStoreTaxesGrid(Nm, true, false, false);
                    break;
                case '2':
                    StoreTax.BindStoreTaxesGrid(Nm, false, true, false);
                    break;
                case '3':
                    StoreTax.BindStoreTaxesGrid(Nm, false, false, true);
                    break;
            }
        },

        ajaxSuccess: function(msg) {
            switch (StoreTax.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    };
    StoreTax.init();
});