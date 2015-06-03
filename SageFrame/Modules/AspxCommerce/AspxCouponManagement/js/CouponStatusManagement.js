var CouponStatusMgmt = "";

$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var editFlag = 0;
    var isUnique = false;
    CouponStatusMgmt = {
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
            ajaxCallMode: 0        },

        ajaxCall: function(config) {
            $.ajax({
                type: CouponStatusMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: CouponStatusMgmt.config.contentType,
                cache: CouponStatusMgmt.config.cache,
                async: CouponStatusMgmt.config.async,
                url: CouponStatusMgmt.config.url,
                data: CouponStatusMgmt.config.data,
                dataType: CouponStatusMgmt.config.dataType,
                success: CouponStatusMgmt.ajaxSuccess,
                error: CouponStatusMgmt.ajaxFailure
            });
        },
        HideAlldiv: function() {
            $('#divCouponStatusDetail').hide();
            $('#divEditCouponStatus').hide();
        },
        Reset: function() {
            $('#txtCouponStatusName').val('');
                   },
        ClearForm: function() {
            $("#btnSaveCouponStatus").removeAttr("name");

            $('#txtCouponStatusName').val('');
           
            $('#txtCouponStatusName').removeClass('error');
            $('#txtCouponStatusName').parents('td').find('label').remove();
            $('#csErrorLabel').html('');
        },
        BindCouponsStatusInGrid: function(CouponStatusName, isAct) {
            this.config.method = "GetAllCouponStatusList";
            this.config.url = this.config.baseURL;
            this.config.data = { aspxCommonObj: aspxCommonObj, couponStatusName: CouponStatusName, isActive: isAct };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblCouponStatusDetails_pagesize").length > 0) ? $("#tblCouponStatusDetails_pagesize :selected").text() : 10;

            $("#tblCouponStatusDetails").sagegrid({
                url: this.config.baseURL ,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Coupon Status ID"), name: 'CouponStatusID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Status Name"), name: 'CouponStatus', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: getLocale(AspxCouponManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CouponStatusMgmt.EditCouponStatus', arguments: '1,2' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 3: { sorter: false } }
            });
        },
        Boolean: function(str) {
            switch (str.toLowerCase()) {
            case "yes":
                return true;
            case "no":
                return false;
            default:
                return false;
            }
        },
        EditCouponStatus: function(tblID, argus) {
            switch (tblID) {
            case "tblCouponStatusDetails":
                editFlag = argus[0];
                CouponStatusMgmt.ClearForm();
                $("#btnReset").hide();
                $('#divCouponStatusDetail').hide();
                $('#divEditCouponStatus').show();

                $('#' + lblHeading).html(getLocale(AspxCouponManagement, "Edit Order Status:") + "'" + argus[3] + "'");
                $('#txtCouponStatusName').val(argus[3]);
                var isactive = argus[4];
                                                                                            $("#btnSaveCouponStatus").prop("name", argus[0]);
                break;
            default:
                break;
            }
        },
        SaveCouponStatus: function(CouponStatusID) {
            editFlag = CouponStatusID;
            var CouponStatusName = $('#txtCouponStatusName').val();
                       var SaveCouponStatusObj = {
                CouponStatusID: CouponStatusID,
                CouponStatus: CouponStatusName,
                IsActive: true
            };
            this.config.method = "AddUpdateCouponStatus";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                aspxCommonObj: aspxCommonObj,
                SaveCouponStatusObj: SaveCouponStatusObj
            });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        SearchCouponStatus: function() {
            var CouponStatusAliasName = $.trim($("#txtCouponStateName").val());
            if (CouponStatusAliasName.length < 1) {
                CouponStatusAliasName = null;
            }
            var isAct = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);

            CouponStatusMgmt.BindCouponsStatusInGrid(CouponStatusAliasName, isAct);
        },
        CheckCouponStatusUniquness: function(CouponStatusId) {
            var CouponStatusName = $.trim($('#txtCouponStatusName').val());
            this.config.method = "CheckCouponStatusUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, couponStatusId: CouponStatusId, couponStatusName: CouponStatusName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
            return isUnique;
        },
        ajaxSuccess: function(data) {
            switch (CouponStatusMgmt.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                CouponStatusMgmt.BindCouponsStatusInGrid(null, null);
                CouponStatusMgmt.ClearForm();
                if (editFlag > 0) {
                    csscody.info('<h2>' + getLocale(AspxCouponManagement, "Information Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Order status has been updated successfully.") + '</p>');
                } else {
                    csscody.info('<h2>' + getLocale(AspxCouponManagement, "Information Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Order status has been saved successfully.") + '</p>');
                }
                $('#divCouponStatusDetail').show();
                $('#divEditCouponStatus').hide();
                break;
            case 2:
                isUnique = data.d;
                if (data.d == true) {
                    $('#txtCouponStatusName').removeClass('error');
                    $('#csErrorLabel').html('');
                } else {
                    $('#txtCouponStatusName').addClass('error');
                    $('#csErrorLabel').html(getLocale(AspxCouponManagement, "This coupon status already exist!")).css("color", "red");
                    return false;
                }
                break;
            }
        },
        ajaxFailure: function(data) {
            switch (CouponStatusMgmt.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h2>' + getLocale(AspxCouponManagement, "Error Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Failed to save coupon status") + '</p>');
                break;
            }
        },
        init: function() {
            CouponStatusMgmt.HideAlldiv();
            $('#divCouponStatusDetail').show();
            CouponStatusMgmt.BindCouponsStatusInGrid(null, null);
            $("#btnBack").bind('click', function() {
                $("#divCouponStatusDetail").show();
                $("#divEditCouponStatus").hide();
            });
            $("#btnReset").bind('click', function() {
                CouponStatusMgmt.Reset();
                CouponStatusMgmt.ClearForm();
            });
            $('#btnSaveCouponStatus').bind('click', function() {
                AspxCommerce.CheckSessionActive(aspxCommonObj);
                if (AspxCommerce.vars.IsAlive) {
                    var v = $("#form1").validate({
                        messages: {
                            StatusName: {
                                required: '*',
                                minlength: "* (at least 2 chars)"
                            }
                        }
                    });
                    if (v.form() && CouponStatusMgmt.CheckCouponStatusUniquness(editFlag)) {
                        var CouponStatus_id = $(this).prop("name");
                        if (CouponStatus_id != '') {
                            CouponStatusMgmt.SaveCouponStatus(CouponStatus_id);
                        } else {
                            CouponStatusMgmt.SaveCouponStatus(0);
                        }
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                }
            });
            $("#txtCouponStatusName").bind('focusout', function() {
                CouponStatusMgmt.CheckCouponStatusUniquness(editFlag);
            });
            $('#txtOrderStateName,#ddlVisibitity').keyup(function(event) {
                if (event.keyCode == 13) {
                    CouponStatusMgmt.SearchCouponStatus();
                }
            });
        }
    };
    CouponStatusMgmt.init();
});