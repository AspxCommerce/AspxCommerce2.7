var couponTypeMgmt;
$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonInfo;
    };
    var couponTypeFlag = 0;
    var isUnique = false;
    var addedOn = "";
    couponTypeMgmt = {
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
                type: couponTypeMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: couponTypeMgmt.config.contentType,
                cache: couponTypeMgmt.config.cache,
                async: couponTypeMgmt.config.async,
                url: couponTypeMgmt.config.url,
                data: couponTypeMgmt.config.data,
                dataType: couponTypeMgmt.config.dataType,
                success: couponTypeMgmt.ajaxSuccess,
                error: couponTypeMgmt.ajaxFailure
            });
        },
        ClearForm: function() {
            $("#txtNewCouponType").val('');
            $("#chkIsActive").prop("checked", true);
            $('#ctErrorLabel').html('');
            $("#chkIsActive").removeAttr("disabled", "disabled");
        },
        HideAllCouponTypeDivs: function() {
            $("#divShowCouponTypeDetails").hide();
            $("#divCouponTypeProviderForm").hide();
        },
        BindCouponTypeDetails: function(searchCouponType) {
            var commonInfo = aspxCommonObj();
            commonInfo.UserName = null;
            this.config.method = "GetCouponTypeDetails";
            this.config.url = this.config.baseURL;
            this.config.data = { couponTypeName: searchCouponType, aspxCommonObj: commonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponType_pagesize").length > 0) ? $("#gdvCouponType_pagesize :selected").text() : 10;

            $("#gdvCouponType").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Coupon Type ID"), name: 'coupon_type_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponTypeChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxCouponManagement, "Coupon Type"), name: 'setting_key', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Added On"), name: 'added_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Active"), name: 'is_active', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCouponManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponTypeMgmt.EditCouponType', arguments: '1,2,3' },
                    { display: getLocale(AspxCouponManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponTypeMgmt.DeleteCouponType', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false} }
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
        EditCouponType: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponType":                   
                    $("#" + lblCouponTypeFormTitle).html(getLocale(AspxCouponManagement, "Edit Coupon Type: ") + "'" + argus[3] + "'");
                    couponTypeMgmt.ClearForm();
                    couponTypeFlag = argus[0];
                    $("#txtNewCouponType").val(argus[3]);
                    addedOn = argus[4];
                    $("#chkIsActive").prop("checked", couponTypeMgmt.Boolean(argus[5]));
                    if (argus[0] == 1) {
                        $("#chkIsActive").prop("disabled", "disabled");
                    }
                    couponTypeMgmt.HideAllCouponTypeDivs();
                    $("#divCouponTypeProviderForm").show();
                    break;
                default:
                    break;
            }
        },
        DeleteCouponType: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponType":
                    if (argus[0] != 1) {
                        var properties = {
                            onComplete: function(e) {
                                couponTypeMgmt.DeleteCouponTypeByID(argus[0], e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete this coupon type?") + "</p>", properties);
                    } else {
                        csscody.info('<h2>' + getLocale(AspxCouponManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCouponManagement, "Sorry! System coupon type can not be deleted..") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        DeleteCouponTypeByID: function(ids, event) {
            if (event) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;
                this.config.method = "DeleteCouponType";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ IDs: ids, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
        },
        DeleteMultipleCouponTypes: function(ids, event) {
            couponTypeMgmt.DeleteCouponTypeByID(ids, event);
        },
        SearchCouponType: function() {
            var searchCouponType = $.trim($("#txtSearchCouponType").val());
                       if (searchCouponType.length < 1) {
                searchCouponType = null;
            }
            couponTypeMgmt.BindCouponTypeDetails(searchCouponType);
        },
        AddUpdateCouponType: function () {           
            var couponTypeObj = {
                RowTotal: 0,
                CouponTypeID: couponTypeFlag,
                CouponType: $("#txtNewCouponType").val(),
                              IsActive: $("#chkIsActive").prop("checked")
            };
            var aspxTempCommonObj = aspxCommonObj();
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            this.config.method = "AddUpdateCouponType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ couponTypeObj: couponTypeObj, aspxCommonObj: aspxTempCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        CheckCouponTypeUniquness: function(couponTypeId) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            var couponTypeName = $.trim($('#txtNewCouponType').val());
            this.config.method = "CheckCouponTypeUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, couponTypeId: couponTypeId, couponType: couponTypeName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return isUnique;
        },
        ajaxSuccess: function(data) {
            switch (couponTypeMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    couponTypeMgmt.BindCouponTypeDetails(null);
                    if (couponTypeFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxCouponManagement,"Successful Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Coupon type has been updated successfully.") + '</p>');
                    } else {
                        csscody.info('<h2>'+getLocale(AspxCouponManagement,"Successful Message")+'</h2><p>' + getLocale(AspxCouponManagement, "Coupon type has been saved successfully.") + '</p>');
                    }
                    couponTypeMgmt.HideAllCouponTypeDivs();
                    $("#divShowCouponTypeDetails").show();
                    break;
                case 2:
                    couponTypeMgmt.BindCouponTypeDetails(null);
                    csscody.info('<h2>'+getLocale(AspxCouponManagement,"Successful Message")+'</h2><p>' + getLocale(AspxCouponManagement, "Coupon type has been deleted successfully.") + '</p>');
                    break;
                case 3:
                    isUnique = data.d;
                    if (data.d == true) {
                        $('#txtNewCouponType').removeClass('error');
                        $('#ctErrorLabel').html('');
                    } else {
                        $('#txtNewCouponType').addClass('error');
                        $('#ctErrorLabel').html(getLocale(AspxCouponManagement, "This coupon type already exist!")).css("color", "red");
                        return false;
                    }
                    break;
            }
        },
        ajaxFailure: function() {
            switch (couponTypeMgmt.config.ajaxCallModw) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxCouponManagement, "Error Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Failed to save coupon type!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxCouponManagement, "Error Message") + '</h2><p>' + getLocale(AspxCouponManagement, "Failed to delete coupon type!") + '</p>');
                    break;
            }
        },
        init: function() {
            couponTypeMgmt.BindCouponTypeDetails(null);
            couponTypeMgmt.HideAllCouponTypeDivs();
            $("#divShowCouponTypeDetails").show();
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

            });
            $("#btnAddNewCouponType").bind('click', function() {
                $("#" + lblCouponTypeFormTitle).html(getLocale(AspxCouponManagement, "Add New Coupon Type"));
                couponTypeFlag = 0;
                couponTypeMgmt.HideAllCouponTypeDivs();
                $("#divCouponTypeProviderForm").show();
                couponTypeMgmt.ClearForm();
            });
            $("#btnCancelCouponTypeUpdate").click(function() {
                couponTypeMgmt.HideAllCouponTypeDivs();
                $("#divShowCouponTypeDetails").show();
                $('#txtNewCouponType').removeClass('error');
                $('#txtNewCouponType').parents('td').find('label').remove();
                $("#chkIsActive").removeAttr("disabled", "disabled");
            });
                                             $("#btnSubmitCouponType").click(function () {            
                var v = $("#form1").validate({
                    messages: {
                        CouponTypeName: {
                            required: '*',
                            minlength: getLocale(AspxCouponManagement, "* (at least 2 chars)")
                        }
                    }
                });
                if (v.form() && couponTypeMgmt.CheckCouponTypeUniquness(couponTypeFlag)) {                
                    couponTypeMgmt.AddUpdateCouponType();
                } else {
                    return false;
                }
            });
            $('#txtSearchCouponType').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponTypeMgmt.SearchCouponType();
                }
            });
            $("#btnDeleteSelectedCouponType").click(function() {
                var coupontype_ids = '';
                               coupontype_ids = SageData.Get("gdvCouponType").Arr.join(',');
                if (coupontype_ids.length == 0) {
                    csscody.alert('<h2>' + getLocale(AspxCouponManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCouponManagement, "Please select at least one coupon type before delete.") + '</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        couponTypeMgmt.DeleteMultipleCouponTypes(coupontype_ids, e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete the selected coupon type(s)?") + "</p>", properties);
            });
        }
    };
    couponTypeMgmt.init();
});