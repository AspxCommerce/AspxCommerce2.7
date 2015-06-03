var couponPerSalesMgmt;
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
    var couponPerSaleDetailFlag = false;
    var searchCouponCode = '';
    couponPerSalesMgmt = {
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
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function() {
            $.ajax({
                type: couponPerSalesMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: couponPerSalesMgmt.config.contentType,
                cache: couponPerSalesMgmt.config.cache,
                async: couponPerSalesMgmt.config.async,
                url: couponPerSalesMgmt.config.url,
                data: couponPerSalesMgmt.config.data,
                dataType: couponPerSalesMgmt.config.dataType,
                success: couponPerSalesMgmt.ajaxSuccess,
                error: couponPerSalesMgmt.ajaxFailure
            });
        },
        ForceNumericInput: function (defaultQuantityInGroup) {
            $("#txtSearchOrderID").keydown(function (e) {
                               if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                                       return;
                }
                               if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
                if (e.keyCode == 96) {
                    if ($(this).val() == 0) {
                        e.preventDefault();
                    }
                }
                if (e.keyCode == 8) {
                    if (($(this).val() > 0) && ($(this).val() < 9)) {
                        e.preventDefault();
                    }
                }

            });
        },
       
        BindAllCouponPerSalesList: function(SearchCouponCode) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            aspxCommonInfo.CultureName = null;
            this.config.method = "GetCouponDetailsPerSales";
            this.config.url = this.config.baseURL;
            this.config.data = { couponCode: SearchCouponCode, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponPerSales_pagesize").length > 0) ? $("#gdvCouponPerSales_pagesize :selected").text() : 10;

            $("#gdvCouponPerSales").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Coupon Code"), name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Number Of Uses As Order"), name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Total Discount Amount Gained By Coupon"), name: 'discount_amount', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Total Sales Amount"), name: 'sales_amount', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCouponManagement, "View"), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'couponPerSalesMgmt.ViewCouponPerSalesDetails' }
                ],

                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false } }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        BindCouponPerSalesDetail: function(couponCode, orderId, userName, couponAmountFrom, couponAmountTo, grandTotalAmountFrom, grandTotalAmountTo) {
            var couponPerSalesGetObj = {
                CouponCode: couponCode,
                OrderID: orderId,
                CouponAmountFrom: couponAmountFrom,
                CouponAmountTo: couponAmountTo,
                GrandTotalAmountFrom: grandTotalAmountFrom,
                GrandTotalAmountTo: grandTotalAmountTo
            };
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = userName;
            this.config.method = "GetCouponPerSalesDetailView";
            this.config.url = this.config.baseURL;
            this.config.data = { aspxCommonObj: aspxCommonInfo, couponPerSaesObj: couponPerSalesGetObj };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponPerSalesDetailView_pagesize").length > 0) ? $("#gdvCouponPerSalesDetailView_pagesize :selected").text() : 10;

            $("#gdvCouponPerSalesDetailView").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Coupon Code"), name: 'couponcode', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Order ID"), name: 'orderID', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Customer ID"), name: 'customerID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "User Name"), name: 'userName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon ID"), name: 'couponID', cssclass: '', controlclass: '', coltype: 'label', align: 'right', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Discount Amount"), name: '', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Grand Total"), name: 'grandTotal', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "No Of Use"), name: 'noOfUse', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'label', align: 'center' },
                    { display: getLocale(AspxCouponManagement, "Order Date"), name: 'orderDate', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'right', type: 'date', format: 'yyyy/MM/dd' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        ViewCouponPerSalesDetails: function(tblID, argus) {
            switch (tblID) {
            case "gdvCouponPerSales":
                                   $('#divCouponPersalesGrid').hide();
                $('#divCouponPerSalesDetail').show();
                couponPerSaleDetailFlag = true;
                searchCouponCode = argus[0];
                 $("input[id$='HdnValueDetail']").val(searchCouponCode);
                $('#gdvCouponPerSales').hide();
                $('#gdvCouponPerSales_Pagination').hide();
                $('#gdvCouponPerSalesDetailView').show();
                $('#btnBackToCouponPerSalesTbl').show();

                $('.cssClassCouponCode').hide();
                $('.cssClassOrderID').show();
                $('.cssClassUserName').show();
                                   $('.cssClassCouponAmount').show();
                $('.cssClassGrandTotalAmount').show();
                $('#txtSearchOrderID,#txtSearchUserName,#txtCouponAmountFrom,#txtSearchCouponAmountTo,#txtGrandTotalAmountFrom,#txtGrandTotalAmountTo').val('');
                                   $('#' + couponPerSalesDetailTitel).html("Details of coupon" + " '" + argus[0] + "'");
                couponPerSalesMgmt.BindCouponPerSalesDetail(argus[0], null, null, null, null, null, null);
                break;
            default:
                break;
            }
        },
        SearchItems: function() {
            var coupName = $.trim($("#txtSearchNameCoupon").val());
            var orderID = $.trim($("#txtSearchOrderID").val());
            var userName = $.trim($("#txtSearchUserName").val());
            var couponAmountFrom = $.trim($("#txtCouponAmountFrom").val());
            var couponAmountTo = $.trim($("#txtSearchCouponAmountTo").val());
            var grandTotalAmountFrom = $.trim($("#txtGrandTotalAmountFrom").val());
            var grandTotalAmountTo = $.trim($("#txtGrandTotalAmountTo").val());
           
            if (coupName.length < 1) {
                coupName = null;
            }
            if (orderID.length < 1) {
                orderID = null;
            }
            if (userName.length < 1) {
                userName = null;
            }
            if (couponAmountFrom < 1) {
                couponAmountFrom = 0.00;
            } else {
                couponAmountFrom = parseFloat(couponAmountFrom);
            }
            if (couponAmountTo.length < 1) {
                couponAmountTo = 0.00;
            } else {
                couponAmountTo = parseFloat(couponAmountTo);
            }
            if (grandTotalAmountFrom.length < 1) {
                grandTotalAmountFrom = 0.00;
            } else {
                grandTotalAmountFrom = parseFloat(grandTotalAmountFrom);
            }
            if (grandTotalAmountTo.length < 1) {
                grandTotalAmountTo = 0.00;
            }
            else {
                grandTotalAmountTo = parseFloat(grandTotalAmountTo);
            }           
            if (couponPerSaleDetailFlag == true) {              
                if ((couponAmountFrom <= couponAmountTo)) {
                    if ((grandTotalAmountFrom <= grandTotalAmountTo)) {
                        if (couponAmountFrom == 0)
                        {
                            couponAmountFrom = null;
                        }
                        if (couponAmountTo == 0) {
                            couponAmountTo = null;
                        }
                        if (grandTotalAmountFrom == 0) {
                            grandTotalAmountFrom = null;
                        }
                        if (grandTotalAmountTo == 0) {
                            grandTotalAmountTo = null;
                        }
                        couponPerSalesMgmt.BindCouponPerSalesDetail(searchCouponCode, orderID, userName, couponAmountFrom, couponAmountTo, grandTotalAmountFrom, grandTotalAmountTo);
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Grand Total From must be less than Grand Total To!") + "</p>");
                    }
                } else {                  
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon Amount From must be less than Coupon Amount To!") + "</p>");
                }
            } else {
                couponPerSalesMgmt.BindAllCouponPerSalesList(coupName);
            }
            $('#txtSearchNameCoupon,#txtSearchOrderID,#txtSearchUserName,#txtCouponAmountFrom,#txtSearchCouponAmountTo,#txtGrandTotalAmountFrom,#txtGrandTotalAmountTo').blur();
        },
       
       
        ajaxSuccess: function(data) {
            switch (couponPerSalesMgmt.config.ajaxCallMode) {
            case 0:
                break;
            }
        },
        HideAll: function() {
            $('.cssClassOrderID').hide();
            $('.cssClassUserName').hide();
            $('.cssClassddlIspercentage').hide();
            $('.cssClassCouponAmount').hide();
            $('.cssClassGrandTotalAmount').hide();
        },
        init: function() {
            couponPerSalesMgmt.BindAllCouponPerSalesList(null);
            $('#gdvCouponPerSalesDetailView').hide();
            $('#btnBackToCouponPerSalesTbl').hide();

            couponPerSalesMgmt.HideAll();
            $("#txtCouponAmountFrom").DigitAndDecimal('#txtCouponAmountFrom', '');
            $("#txtSearchCouponAmountTo").DigitAndDecimal('#txtSearchCouponAmountTo', '');
            $("#txtGrandTotalAmountFrom").DigitAndDecimal('#txtGrandTotalAmountFrom', '');
            $("#txtGrandTotalAmountTo").DigitAndDecimal('#txtGrandTotalAmountTo', '');
            $('#txtSearchNameCoupon,#txtSearchOrderID,#txtSearchUserName,#txtCouponAmountFrom,#txtSearchCouponAmountTo,#txtGrandTotalAmountFrom,#txtGrandTotalAmountTo').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponPerSalesMgmt.SearchItems();
                                  }
            });
            couponPerSalesMgmt.ForceNumericInput();
            $('#btnBackToCouponPerSalesTbl').bind('click', function() {
                couponPerSaleDetailFlag = false;
                $('#gdvCouponPerSales').show();
                $('#gdvCouponPerSales_Pagination').show();
                               $('#gdvCouponPerSalesDetailView').hide();
                $('#btnBackToCouponPerSalesTbl').hide();
                $('#gdvCouponPerSalesDetailView_Pagination').hide();
                $('.cssClassCouponCode').show();
                $('#divCouponPerSalesDetail').hide();
                $('#divCouponPersalesGrid').show();
                couponPerSalesMgmt.HideAll();
            });
        }
    };
    couponPerSalesMgmt.init();
});