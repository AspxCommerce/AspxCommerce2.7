var couponPerUserMgmt;
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
    couponPerUserMgmt = {
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
        ajaxCall: function(config) {
            $.ajax({
                type: couponPerUserMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: couponPerUserMgmt.config.contentType,
                cache: couponPerUserMgmt.config.cache,
                async: couponPerUserMgmt.config.async,
                url: couponPerUserMgmt.config.url,
                data: couponPerUserMgmt.config.data,
                dataType: couponPerUserMgmt.config.dataType,
                success: couponPerUserMgmt.ajaxSuccess,
                error: couponPerUserMgmt.ajaxFailure
            });
        },
        HideAll: function() {
            $("#divShowCouponTypeDetails").hide();
            $("#divCouponUserForm").hide();
        },

        BindCouponUserDetails: function(searchCouponCode, userName, couponStatusID, validateFrom, validateTo) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = userName;
            var couponUserDetailObj = {
                CouponCode: searchCouponCode,
                CouponStatusID: couponStatusID,
                ValidFrom: validateFrom,
                ValidTo: validateTo
            };
            this.config.method = "GetCouponUserDetails";
            this.config.data = { couponUserObj: couponUserDetailObj, aspxCommonObj: aspxCommonInfo, userName: userName };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponUser_pagesize").length > 0) ? $("#gdvCouponUser_pagesize :selected").text() : 10;

            $("#gdvCouponUser").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Coupon User ID"), name: 'coupon_type_id', cssclass: 'cssClassHide', coltype: 'checkbox', align: 'center', elemClass: 'CouponUserChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon ID"), name: 'coupon_id', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Code"), name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "User Name"), name: 'user', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon Life"), name: 'couponLife', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "No Of Uses"), name: 'no_of_use', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon Status ID"), name: 'coupon_status_id', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Status"), name: 'coupon_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Valid From"), name: 'valid_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Valid To"), name: 'valid_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false} }
            });
        },
        GetCouponStatus: function() {
            this.config.url = this.config.baseURL + "GetCouponStatus";
            this.config.data = JSON2.stringify({aspxCommonObj: aspxCommonObj()});
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        DeleteCouponUserByID: function(ids, event) {
            if (event) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;
                this.config.method = "DeleteCouponUser";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ couponUserID: ids, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
        },
        SearchCouponCode: function() {
            var searchCouponCode = $.trim($("#txtSearchCouponCode").val());
            var userName = $.trim($("#txtSearchUserName").val());
            var couponStatusID = $.trim($("#ddlCouponStatus").val());
            var validateFrom = $.trim($("#txtValidFrom").val());
            var validateTo = $.trim($("#txtValidTo").val());
            if (couponStatusID == "0") {
                couponStatusID = null;
            }
            if (searchCouponCode.length < 1) {
                searchCouponCode = null;
            }
            if (userName.length < 1) {
                userName = null;
            }
            if (validateFrom.length < 1) {
                validateFrom = null;
            }
            if (validateTo.length < 1) {
                validateTo = null;
            }
            if ((validateFrom <= validateTo) || (validateFrom != '' && validateTo == null) || (validateFrom == null && validateTo != '')) {
                couponPerUserMgmt.BindCouponUserDetails(searchCouponCode, userName, couponStatusID, validateFrom, validateTo);
            } else {
                csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Valid From date must be less than Valid To date!") + "</p>");
            }
            $('#txtSearchCouponCode,#txtSearchUserName,#ddlCouponStatus,#txtValidFrom,#txtValidTo').blur();
        },


        ajaxSuccess: function(data) {
            switch (couponPerUserMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(data.d, function(index, value) {
                        var couponStatusElements = "<option value=" + value.CouponStatusID + ">" + value.CouponStatus + "</option>";
                        $("#ddlCouponStatusType").append(couponStatusElements);
                        $("#ddlCouponStatus").append(couponStatusElements);
                    });
                    break;
                case 2:
                    couponPerUserMgmt.BindCouponUserDetails(null, null, null, null, null);
                    break;

            }
        },
        ajaxFailure: function(data) {
            switch (couponPerUserMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxCouponManagement, "Error Message") + '</h1><p>' + getLocale(AspxCouponManagement, "Could not load Order Status!!!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h1>' + getLocale(AspxCouponManagement, "Error Message") + '</h1><p>' + getLocale(AspxCouponManagement, "Error Occured!!") + '</p>');
                    break;
            }
        },
        init: function () {          
            $("#txtValidFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtValidTo").datepicker({ dateFormat: 'yy/mm/dd' });
            couponPerUserMgmt.HideAll();
            $("#divShowCouponTypeDetails").show();
            couponPerUserMgmt.BindCouponUserDetails(null, null, null, null, null);
            couponPerUserMgmt.GetCouponStatus();
            $("#btnDeleteAllNonPendingCoupon").bind('click', function() {
                var properties = {
                    onComplete: function(e) {
                        couponPerUserMgmt.DeleteCouponUserByID(0, e);
                    }
                };
                csscody.confirm("<h1>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h1><p>" + getLocale(AspxCouponManagement, "Do you want to delete all non pending coupon User(s)?") + "</p>", properties);
            });
            $("#" + couponPerSalesDataToExcel).bind('click', function() {
                couponPerUserMgmt.ExportCouponPerUserDivDataToExcel();
            });
            $('#txtSearchCouponCode,#txtSearchUserName,#ddlCouponStatus,#txtValidFrom,#txtValidTo').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponPerUserMgmt.SearchCouponCode();
                }
            });
        }
    };
    couponPerUserMgmt.init();
});