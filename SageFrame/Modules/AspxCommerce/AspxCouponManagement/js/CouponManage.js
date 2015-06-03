var couponMgmt;
var catalogDot = false;
var catalogBakCount = 0;
var catalogCount = 0;
$(function () {
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonInfo;
    };
    var senderEmail = userEmail;
    var serverLocation = ServerVariables;
    var deleteAllSelectedCouponUser = 0;
    var seachByCouponUser = 0;
    var portalusers_emailid = '';
    var portalusers_customername = '';
    var portalusers_username = '';
    (function ($) {
        $.fn.numeric = function (options) {

            return this.each(function () {
                var $this = $(this);
                $this.keypress(options, function (e) {
                    if ($this.val() == '') {
                        $this.prop('maxlength', 11);
                    }
                    if (e.which == 8 || e.which == 0) {
                        if (catalogDot == true) {
                            catalogCount--;
                        }
                        if (catalogCount == -1) {
                            $this.prop('maxlength', 11);
                        }
                        if (catalogDot == true && catalogBakCount >= catalogCount) {
                            catalogDot = false;
                            catalogBakCount = 0;
                            catalogCount = 0;
                        }
                        return true;
                    }
                    if (e.which == 46) {
                        if (catalogDot == false) {
                            catalogDot = true;
                            catalogBakCount = 0;
                            catalogCount = 0;
                            return true;
                        }
                    }
                    if (catalogDot == true) {
                        var z = $this.val();
                        z = z.split('.');
                        $this.prop('maxlength', z[0].length + 3);
                    } else {
                        $this.prop('maxlength', 11);
                    }
                    if (catalogDot == true) {
                        if (catalogCount < 2) {
                            catalogCount++;
                        }
                        catalogBakCount = catalogCount;
                    }
                    if (e.which < 48 || e.which > 57)
                        return false;
                    var dest = e.which - 48;
                    var result = this.value + dest.toString();
                    if (result > e.data.max) {
                        return false;
                    }
                });
            });
        };
    })(jQuery);
    couponMgmt = {
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
        ajaxCall: function (config) {
            $.ajax({
                type: couponMgmt.config.type,
                contentType: couponMgmt.config.contentType,
                cache: couponMgmt.config.cache,
                async: couponMgmt.config.async,
                url: couponMgmt.config.url,
                data: couponMgmt.config.data,
                dataType: couponMgmt.config.dataType,
                success: couponMgmt.ajaxSuccess,
                error: couponMgmt.ajaxFailure
            });
        },
        LoadCouponAjaxImage: function () {
            $('#ajaxCouponMgmtImageLoad').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxCouponImageLoad2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#btnRefresh').prop('src', '' + aspxTemplateFolderPath + '/images/refresh_icon.png');
        },
        HideAllCouponDivs: function () {
            $("#divShowCouponDetails").hide();
            $("#divCouponForm").hide();
        },
        ClearSearchTextBox: function () {
            $('#txtSearchUserName').val('');
            $('#txtSearchCouponCode').val('');
            $('#txtSearchValidateFrom').val('');
            $('#txtSearchValidateTo').val('');
            $('#ddlSearchCouponType').val(0);
            $('#btnBackToCouponTbl').click();
            couponMgmt.BindCouponDetails(null, null, null, null, null);
        },
        ClearCouponForm: function () {
            document.getElementById('btnGenerateCode').disabled = false;
            $("#ddlCouponType").val(0);
            $("#txtUsesPerCustomer").val('');
            $("#txtAmount").val('');
            $("#txtNewCoupon").val('');
            $("#txtValidTo").val('');
            $("#txtValidFrom").val('');
            $("#txtApplyAmountRange").val('');
            $("#txtAmount").removeAttr("disabled");
            $("#ddlCouponAmountType").removeAttr("disabled");
            $("#txtAmount").parents('tr').show();
            $("#txtUsesPerCustomer").removeAttr("disabled");
            $("#ddlCouponType").removeAttr("disabled");
            $("#ddlIsForFreeShipping").removeAttr("disabled");
            $("#chkIsActive").prop("checked", true);
            $("#ddlIsForFreeShipping").val(1);
            couponMgmt.BindAllPortalUsersByCouponID(0, null);
            couponMgmt.ClearCouponFormError();
            portalusers_customername = '';
            portalusers_emailid = '';
            portalusers_username = '';
        },
        ClearCouponFormError: function () {
            $('#txtUsesPerCustomer').removeClass('error');
            $('#txtUsesPerCustomer').parents('td').find('label').remove();
            $('#txtAmount').removeClass('error');
            $('#txtAmount').parents('td').find('label').remove();
            $('#txtNewCoupon').removeClass('error');
            $('#txtNewCoupon').parents('td').find('label').remove();
            $('#txtValidTo').removeClass('error');
            $('#txtValidTo').parents('td').find('label').remove();
            $('#txtValidFrom').removeClass('error');
            $('#txtValidFrom').parents('td').find('label').remove();
            $('#ddlCouponType').removeClass('error');
            $('#ddlCouponType').parents('td').find('label').remove();
            $('#created').html('');
            $('.to').parents('td').find('input').prop("style", '');
            $('#ddlIsForFreeShipping').removeClass('error');
            $('#couponAmountErrorLabel').html(''); couponMgmt.CreateCouponTypeDdl(0);
        },
        GenerateCodeString: function () {
            var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
            var string_length = 15;
            var codeString = '';
            for (var i = 0; i < string_length; i++) {
                var rnum = Math.floor(Math.random() * chars.length);
                codeString += chars.substring(rnum, rnum + 1);
            }
            $("#txtNewCoupon").val(codeString);
        },
        GetAllCouponType: function () {
            var offset = 0;
            var limit = 0;
            var couponTypeName = null;
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.method = "GetCouponTypeDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ offset: offset, limit: limit, couponTypeName: couponTypeName, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        SearchPromoItems: function () {
            var itemNm = $.trim($("#txtItemNm").val());
            couponMgmt.GetAllItems(itemNm, null);
        },
        GetCouponStatus: function () {
            this.config.url = this.config.baseURL + "GetCouponStatus";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        AddUpdateCoupon: function (settingId, settingValue) {
            var couponTypeId = $("#ddlCouponType").val();
            var couponId = $("#hdnCouponID").val();
            var couponCode = $("#txtNewCoupon").val();
            var freeShipping = false;
            if ($("#ddlIsForFreeShipping").val() == 1) {
                freeShipping = false;
            } else {
                freeShipping = true;
            }
            var couponAmount = $("#txtAmount").val() == "" ? null : $("#txtAmount").val();
            var isPercentage = $("#ddlCouponAmountType").val();
            if (couponAmount == 0 && $('#ddlIsForFreeShipping').val() != 2) {
                $('#txtAmount').addClass('error');
                $('#couponAmountErrorLabel').html(getLocale(AspxCouponManagement, "Zero coupon amount is not allowed!!")).css("color", "red");
                return false;
            } else {
                $('#txtAmount').removeClass('error');
                $('#couponAmountErrorLabel').html('');
            }
            var couponLife = $('#txtUsesPerCustomer').val();
            var validFrom = $("#txtValidFrom").val();
            var validTo = $("#txtValidTo").val();
            var isActive = $("#chkIsActive").prop("checked");
            var couponName = $('#ddlCouponType option:selected').text();

            if (couponTypeId == 1) {
                var item_ids = '';
                item_ids = SageData.Get("gdvPromoItems").Arr.join(',');
                var promoCodeSaveObj = {
                    CouponID: couponId,
                    CouponTypeID: couponTypeId,
                    CouponCode: couponCode,
                    CouponAmount: couponAmount,
                    IsPercentage: isPercentage,
                    ValidateFrom: validFrom,
                    ValidateTo: validTo,
                    IsActive: isActive,
                    SettingIDs: settingId,
                    SettingValues: settingValue,
                    PromoItems: item_ids
                };
                couponMgmt.SaveAndUpdatePromoCode(promoCodeSaveObj);
            } else {

                $("#gdvPortalUser .portalUserChkbox").each(function (i) {
                    if ($(this).prop("checked") && $(this).prop('disabled') == false) {
                        portalusers_username += $(this).parent('td').next('td').text() + '#';
                        portalusers_customername += $(this).parent('td').next('td').next('td').text() + '#';
                        portalusers_emailid += $(this).parent('td').next('td').next('td').next('td').text() + '#';
                    }
                });
                portalusers_username = portalusers_username.substring(0, portalusers_username.length - 1);
                portalusers_customername = portalusers_customername.substring(0, portalusers_customername.length - 1);
                portalusers_emailid = portalusers_emailid.substring(0, portalusers_emailid.length - 1);

                var serverHostLoc = 'http://' + serverLocation;
                var subject = getLocale(AspxCouponManagement, "Congratulation You Got a CouponCode ");

                var fullDate = new Date();
                var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                if (twoDigitMonth.length == 2) {
                } else if (twoDigitMonth.length == 1) {
                    twoDigitMonth = '0' + twoDigitMonth;
                }
                var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                var dateyear = fullDate.getFullYear();

                var emailtemplate = [];
                var unames = [];
                unames = portalusers_customername.split('#');
                var messageBodyHtml = '';
                for (var nn in unames) {
                    messageBodyHtml += '<table width="100%" border="0" align="left" cellpadding="0" cellspacing="5" bgcolor="#e0e0e0" style="font:12px Arial, Helvetica, sans-serif;"><tr><td align="center" valign="top"><table style="font:12px Arial, Helvetica, sans-serif;" width="680" border="0" cellspacing="0" cellpadding="0">';
                    messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr>';
                    messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr><tr><td align="left"><table style="font:12px Arial, Helvetica, sans-serif;" width="680" border="0" cellspacing="0" cellpadding="0"><tr><td width="300">';
                    messageBodyHtml += '<a href="' + serverHostLoc + '" target="_blank" style="outline:none; border:none;"><img src="' + serverHostLoc + aspxRootPath + StoreLogoImg + '" width="143" height="62" alt="' + StoreName + '" title="' + StoreName + '"/></a></td>';
                    messageBodyHtml += '<td width="191" align="left" valign="middle">&nbsp;</td><td width="189" align="right" valign="middle"><b style="padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;">' + currentDate + '</b></td></tr></table></td></tr>';
                    messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr>';
                    messageBodyHtml += '<tr><td bgcolor="#fff"><div style="border:1px solid #c7c7c7; background:#fff; padding:20px">';
                    messageBodyHtml += '<table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF"><tr><td><p style="font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;">' + getLocale(AspxCouponManagement, "Congratulation !!");
                    messageBodyHtml += unames[nn].toUpperCase();
                    messageBodyHtml += getLocale(AspxCouponManagement, "You have got Coupon code for shopping!!") + '</p></td></tr><tr><td><span style="font-weight:normal; font-size:14px; margin-bottom:5px; font-family:Arial, Helvetica, sans-serif;">' + getLocale(AspxCouponManagement, "Enjoy your Shopping !!") + '</span></td></tr></table>';
                    messageBodyHtml += '<div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px"><table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td><table  style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0"><tr>';
                    messageBodyHtml += '<td width="120" height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">' + getLocale(AspxCouponManagement, "Coupon Type:") + '</span></td> <td align="left">' + couponName + '</td>';
                    messageBodyHtml += '<td width="150" style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px; align="left"">' + getLocale(AspxCouponManagement, "Valid From:") + ' </td><td align="left">' + validFrom + '</td></tr>';
                    messageBodyHtml += '<tr><td height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Code: </span></td> <td align="left">' + couponCode + '</td>';
                    messageBodyHtml += ' <td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px; align="left"">' + getLocale(AspxCouponManagement, "Valid Upto:") + '</td><td align="left">' + validTo + '</td></tr>';
                    messageBodyHtml += '<tr><td height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Life: </span></td><td align="left">' + couponLife + '</td>';
                    messageBodyHtml += '<td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">&nbsp;</td><td>&nbsp;</td></tr>';
                    if ($('#ddlIsForFreeShipping option:selected').val() == 2) {
                        messageBodyHtml += '<tr><td height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">' + getLocale(AspxCouponManagement, "Coupon Amount:") + '</span></td><td align="left">' + getLocale(AspxCouponManagement, "Do not worry!! Its Free Shipping Coupon.") + '</td>';
                    } else {
                        if (isPercentage == 0) {
                            messageBodyHtml += '<tr><td height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">' + getLocale(AspxCouponManagement, "Coupon Amount:") + '</span></td><td align="left">' + curSymbol + ' ' + couponAmount + '</td>';
                        } else {
                            messageBodyHtml += '<tr><td height="20" align="left"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">' + getLocale(AspxCouponManagement, "Coupon Amount:") + '</span></td><td align="left">' + couponAmount + getLocale(AspxCouponManagement, "% of total amount.") + '</td>';
                        }
                    }
                    messageBodyHtml += ' <td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">&nbsp;</td><td>&nbsp;</td></tr></table></td></tr></table></div>';
                    messageBodyHtml += '<p style="margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666; text-align:left">' + getLocale(AspxCouponManagement, "Thank You,") + '<br /><span style="font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;">' + StoreName + ' Team </span></p></div></td></tr>';
                    messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="20" alt=" "/></td></tr>';
                    messageBodyHtml += '<tr><td align="center" valign="top"><p style="font-size:11px; color:#4d4d4d"> © ' + dateyear + ' ' + StoreName + getLocale(AspxCouponManagement, ". All Rights Reserved.") + '</p></td></tr>';
                    messageBodyHtml += '    <tr><td align="center" valign="top"><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr></table></td></tr></table>';
                    emailtemplate.push(messageBodyHtml);
                    messageBodyHtml = '';
                }
                var emailInfo = {
                    SenderName: aspxCommonObj().UserName,
                    SenderEmail: senderEmail,
                    ReceiverName: portalusers_customername,
                    ReceiverEmail: portalusers_emailid,
                    Subject: subject,
                    Message: '',
                    MessageBodyTemplate: emailtemplate
                };
                var couponSaveObj = {
                    CouponID: couponId,
                    CouponTypeID: couponTypeId,
                    CouponCode: couponCode,
                    CouponAmount: couponAmount,
                    IsPercentage: isPercentage,
                    ValidateFrom: validFrom,
                    ValidateTo: validTo,
                    IsActive: isActive,
                    SettingIDs: settingId,
                    SettingValues: settingValue,
                    PortalUser_UserName: portalusers_username
                };
                this.config.method = "AddUpdateCouponDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    couponSaveObj: couponSaveObj,
                    couponEmailObj: emailInfo,
                    aspxCommonObj: aspxCommonObj()
                });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
        },
        SaveAndUpdatePromoCode: function (obj) {
            this.config.method = "AddUpdatePromoCodeDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                promoSaveObj: obj,
                aspxCommonObj: aspxCommonObj()
            });
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
        },
        BindAllPortalUsersByCouponID: function (couponId, customerName) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.method = "GetPortalUsersByCouponID";
            this.config.url = this.config.baseURL;
            this.config.data = { couponID: couponId, customerName: customerName, aspxCommonObj: aspxCommonInfo };
            var data2 = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPortalUser_pagesize").length > 0) ? $("#gdvPortalUser_pagesize :selected").text() : 10;
            $("#gdvPortalUser").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetPortalUsersByCouponID',
                colModel: [
                    { display: getLocale(AspxCouponManagement, "Portal User ID"), name: 'portal_user_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '4', elemClass: 'portalUserChkbox', elemDefault: false, controlclass: 'userHeaderChkbox', checkedItems: '4' },
                    { display: getLocale(AspxCouponManagement, "User Name"), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxCouponManagement, "Customer Name"), name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Email"), name: 'email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxCouponManagement, "IsAlreadySent"), name: 'is_already_sent', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Customers Found!"),
                param: data2,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false} }
            });
        },
        GetPromoItemCheckIDs: function (couponID) {
            if (couponID == null) {
                couponID = 0;
            }
            this.config.method = "GetPromoItemCheckIDs";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                CouponID: couponID,
                aspxCommonObj: aspxCommonObj()
            });
            this.config.ajaxCallMode = 10;
            this.ajaxCall(this.config);
        },
        GetAllItems: function (itemName, couponId) {
            var aspxCommonInfo = aspxCommonObj();
            this.config.method = "ItemsForPromoCode";
            this.config.url = this.config.baseURL;
            this.config.data = { aspxCommonObj: aspxCommonInfo, itemName: itemName, couponId: couponId };
            var data2 = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPromoItems_pagesize").length > 0) ? $("#gdvPromoItems_pagesize :selected").text() : 10;
            $("#gdvPromoItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'ItemsForPromoCode',
                colModel: [
                    { display: getLocale(AspxCouponManagement, "ItemID"), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemsChkbox', elemDefault: false, controlclass: 'classClassCheckBox', checkedItems: '6' },
                    { display: getLocale(AspxCouponManagement, "Name"), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Price"), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Quantity"), name: 'qty', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Visibility"), name: 'visibility', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Active?"), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "IsAlreadyExists"), name: 'is_already_sent', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Items Found!"),
                param: data2,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false} }
            });
            couponMgmt.GetPromoItemCheckIDs(couponId);
        },
        BindCouponDetails: function (SearchCouponTypeId, SearchCouponCode, validateFromDate, validateToDate, userName) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = userName;
            var getCouponDetailObj = {
                CouponTypeID: SearchCouponTypeId,
                CouponCode: SearchCouponCode,
                ValidateFrom: validateFromDate,
                ValidateTo: validateToDate
            };
            this.config.method = "GetCouponDetails";
            this.config.url = this.config.baseURL;
            this.config.data = { couponDetailObj: getCouponDetailObj, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCoupons_pagesize").length > 0) ? $("#gdvCoupons_pagesize :selected").text() : 10;

            $("#gdvCoupons").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "CouponID"), name: 'coupon_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxCouponManagement, "Coupon Type ID"), name: 'coupon_type_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Type"), name: 'coupon_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon Code"), name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Number Of Uses"), name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Validate From"), name: 'validate_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Validate To"), name: 'validate_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Coupon Amount"), name: 'balance_amount', checkFor: '8', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Is Percentage"), name: 'is_Percentage', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxCouponManagement, "Shipping Free"), name: 'IsFreeShipping', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Miminum Apply Amount"), name: 'usesPeritem', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Added On"), name: 'added_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Updated On"), name: 'updated_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Active"), name: 'is_active', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCouponManagement, "View"), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'couponMgmt.ViewCoupons', arguments: '1,3' },
                    { display: getLocale(AspxCouponManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponMgmt.EditCoupons', arguments: '1,3,5,6,7,8,9,10,11,12,13,14' },
                    { display: getLocale(AspxCouponManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponMgmt.DeleteCoupons', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false} }
            });
        },
        Boolean: function (str) {
            switch (str.toLowerCase()) {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        },
        ViewCoupons: function (tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    deleteAllSelectedCouponUser = 1;
                    seachByCouponUser = 1;
                    $('#gdvCoupons').hide();
                    $('#gdvCoupons_Pagination').hide();
                    $('#btnBackToCouponTbl').show();
                    $('#gdvCouponUser').show();
                    $('#btnAddNewCoupon').hide();
                    $('.cssClassddlCouponStatus').show();
                    $('.cssClasstxtSearchUserName').show();
                    $('.cssClassddlCouponType').hide();
                    $('.cssClasstxtSearchCouponCode').hide();
                    $('.cssClasstxtSearchValidateFrom').hide();
                    $('.cssClasstxtSearchValidateTo').hide();
                    $('#btnDeleteSelectedCoupon').show();
                    $('.cssClassSearchPanel').show();
                    $("#hdnCouponID").val(argus[0]);
                    var couponID = argus[0];
                    var couponCode = "";
                    if (argus[3] != 1) {
                        couponMgmt.BindCouponUsers(couponID, null, null, null);
                    } else {
                        $('#btnDeleteSelectedCoupon').hide();
                        $('.cssClassSearchPanel').hide();
                        $('.cssCouponView').hide();
                        couponMgmt.BindPromoItems(parseInt(argus[3]), couponID);
                    }
                    break;
                default:
                    break;
            }
        },
        BindPromoItems: function (couponTypeId, couponId) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = userName;
            this.config.method = "GetPromoItemList";
            this.config.url = this.config.baseURL;
            this.config.data = { couponTypeId: couponTypeId, couponId: couponId, aspxCommonObj: aspxCommonInfo };
            var dataCouponUsers = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponUser_pagesize").length > 0) ? $("#gdvCouponUser_pagesize :selected").text() : 10;

            $("#gdvCouponUser").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "ItemID"), name: 'itemId', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'promoViewChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox2', hide: true },
                    { display: getLocale(AspxCouponManagement, "Item Name"), name: 'userName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Promo Amount"), name: 'balance_amount', checkFor: '3', cssclass: 'cssClassHeadNumber sfAlignRight', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Promo Life"), name: 'couponLife', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Is Percentage"), name: 'IsPercentage', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                ],
                buttons: [
                    { display: getLocale(AspxCouponManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponMgmt.EditCouponsStatus', arguments: '1,2,3,4,5,6' },
                    { display: getLocale(AspxCouponManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponMgmt.DeleteCouponsUser', arguments: '1,2,3,5' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: dataCouponUsers,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 5: { sorter: false} }
            });
        },
        BindCouponUsers: function (couponID, SearchCouponCode, userName, couponStatusID) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = userName;
            var bindCouponUserInfo = {
                CouponID: couponID,
                CouponCode: SearchCouponCode,
                CouponStatusID: couponStatusID
            };
            this.config.method = "GetCouponUserList";
            this.config.url = this.config.baseURL;
            this.config.data = { bindCouponUserObj: bindCouponUserInfo, aspxCommonObj: aspxCommonInfo };
            var dataCouponUsers = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponUser_pagesize").length > 0) ? $("#gdvCouponUser_pagesize :selected").text() : 10;

            $("#gdvCouponUser").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCouponManagement, "CouponUserID"), name: 'couponUserID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponViewChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxCouponManagement, "CouponID"), name: 'coupon_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Code"), name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "User Name"), name: 'userName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon Amount"), name: 'balance_amount', checkFor: '5', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxCouponManagement, "Is Percentage"), name: 'is_Percentage', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Status ID"), name: 'coupon_status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCouponManagement, "Coupon Status"), name: 'coupon_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Coupon Life"), name: 'couponLife', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Number Of Uses"), name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCouponManagement, "Validate From"), name: 'validate_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Validate To"), name: 'validate_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCouponManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCouponManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponMgmt.EditCouponsStatus', arguments: '0,1,2,3,4,5,6,7,8,9,10,11,12' },
                    { display: getLocale(AspxCouponManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponMgmt.DeleteCouponsUser', arguments: '1,2,3,5' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCouponManagement, "No Records Found!"),
                param: dataCouponUsers,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 12: { sorter: false} }
            });
        },
        EditCoupons: function (tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    couponMgmt.ClearCouponFormError();
                    couponMgmt.CreateCouponTypeDdl(0);
                    if (argus[3] != 1) {
                        $("#tblEditCouponForm tbody #trCouponCustomer,#trUserPerCustomer").show();
                        $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
                        $('#txtSearchCustomerName').val('');
                        couponMgmt.BindAllPortalUsersByCouponID(argus[0], null);
                    } else {
                        $("#tblEditCouponForm tbody #trCouponCustomer,#trUserPerCustomer").hide();
                        $("#tblEditCouponForm tbody #trPromoCodeItems").show();
                        couponMgmt.GetAllItems(null, argus[0]);
                    }
                    $("#txtApplyAmountRange").val(argus[10]);
                    document.getElementById('btnGenerateCode').disabled = true;
                    $("#" + lblCouponManageTitle).html("Edit Coupon: '" + argus[4] + "'");
                    $("#hdnCouponID").val(argus[0]);
                    $("#ddlCouponType").val(argus[3]);
                    $("#txtNewCoupon").val(argus[4]);
                    $("#txtValidFrom").val(argus[5]);
                    $("#txtValidTo").val(argus[6]);
                    $("#txtAmount").val(argus[7]);
                    $("#btnGenerateCode").hide();
                    if (argus[8] == 'Yes') {
                        $("#ddlCouponAmountType").val(1);
                    } else {
                        $("#ddlCouponAmountType").val(0);
                    }
                    $("#chkIsActive").prop('checked', couponMgmt.Boolean(argus[13]));
                    var couponId = argus[0];
                    var userNamesColl = "";
                    couponMgmt.BindSetting(couponId);
                    $("#txtNewCoupon").prop("disabled", "disabled");
                    $("#txtAmount").prop("disabled", "disabled");
                    $("#ddlCouponAmountType").prop("disabled", "disabled");
                    $("#txtUsesPerCustomer").prop("disabled", "disabled");
                    $("#ddlCouponType").prop("disabled", "disabled");
                    $("#ddlIsForFreeShipping").prop("disabled", "disabled");
                    $("#spancouponCode").html('');
                    couponMgmt.HideAllCouponDivs();
                    $("#divCouponForm").show();
                    portalusers_customername = '';
                    portalusers_emailid = '';
                    portalusers_username = '';
                    if (argus[9].toLowerCase() == 'no') {
                        $("#ddlIsForFreeShipping").val(1);
                    } else {
                        $("#ddlIsForFreeShipping").val(2);
                    }
                    break;
                default:
                    break;
            }
        },
        BindSetting: function (couponId) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.CultureName = null;
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetSettinKeyValueByCouponID";
            this.config.data = JSON2.stringify({ couponID: couponId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        DeleteCoupons: function (tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    var properties = {
                        onComplete: function (e) {
                            couponMgmt.DeleteCouponByID(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete this coupn code?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },
        DeleteCouponByID: function (Ids, event) {
            if (event) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;

                this.config.url = this.config.baseURL + "DeleteCoupons";
                this.config.data = JSON2.stringify({ couponIDs: Ids, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            }
        },
        DeleteMultipleCoupons: function (Ids, event) {
            couponMgmt.DeleteCouponByID(Ids, event);
        },
        EditCouponsStatus: function (tblID, argus) {
            switch (tblID) {
                case "gdvCouponUser":
                    $("#divShowCouponDetails").hide();
                    $("#hdnCouponUserID").val(argus[0]);
                    $("#txtCouponCode").text(argus[5]);
                    $("#" + lblCouponUserTitle).html(getLocale(AspxCouponManagement, "Edit Coupon Provided to: ") + argus[6] + " ");
                    $("#txtUserName").text(argus[6]);
                    $('#ddlCouponStatusType').val($('#ddlCouponStatus option:contains(' + argus[10] + ')').prop('value'));
                    $('#ddlCouponStatusType').prop('disabled', 'disabled');
                    $("#divCouponUserForm").show();
                    break;
                default:
                    break;
            }
        },
        DeleteCouponsUser: function (tblID, argus) {
            switch (tblID) {
                case "gdvCouponUser":
                    var couponUserIDs = argus[0];
                    if (argus[6] == 3) {
                        csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "This coupon has been provided to") + "'" + argus[5] + getLocale(AspxCouponManagement, "'. Deleting prevents '") + argus[5] + getLocale(AspxCouponManagement, "' from using this coupon!") + "</p>");
                    }
                    var properties = {
                        onComplete: function (e) {
                            couponMgmt.DeleteCouponUserByID(couponUserIDs, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete this coupon user?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },
        DeleteCouponUserByID: function (couponUserIDs, event) {
            if (event) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;
                this.config.url = this.config.baseURL + "DeleteCouponUser";
                this.config.data = JSON2.stringify({ couponUserID: couponUserIDs, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            }
        },
        UpdateCouponUser: function () {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            var couponUserID = $("#hdnCouponUserID").val();
            var couponStatusID = $("#ddlCouponStatusType").val();

            this.config.url = this.config.baseURL + "UpdateCouponUser";
            this.config.data = JSON2.stringify({ couponUserID: couponUserID, couponStatusID: couponStatusID, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },
        CheckUniqueCouponCode: function (couponCode) {
            var aspxCommonInfo = aspxCommonObj();
            this.config.url = this.config.baseURL + "CheckUniqueCouponCode";
            this.config.data = JSON2.stringify({ couponCode: couponCode, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
        },
        SearchCouponPortalUsers: function () {
            var couponId = $("#hdnCouponID").val();
            var searchCustomerName = $('#txtSearchCustomerName').val();
            couponMgmt.BindAllPortalUsersByCouponID(couponId, searchCustomerName);
        },
        SearchCouponDetails: function () {
            var SearchCouponTypeId = $("#ddlSearchCouponType").val();
            var SearchCouponCode = $.trim($("#txtSearchCouponCode").val());
            var validateFromDate = $.trim($("#txtSearchValidateFrom").val());
            var validateToDate = $.trim($("#txtSearchValidateTo").val());

            if (Date.parse(validateFromDate) > Date.parse(validateToDate)) {
                csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Validate To Date must be greater than Validate from Date!") + "</p>");
                return false;
            }
            if (SearchCouponTypeId != "0") {
                SearchCouponTypeId = $("#ddlSearchCouponType").val();
            } else {
                SearchCouponTypeId = null;
            }
            if (validateFromDate.length < 1) {
                validateFromDate = null;
            } else {
                validateFromDate = $.trim($("#txtSearchValidateFrom").val());
            }
            if (validateToDate.length < 1) {
                validateToDate = null;
            } else {
                validateToDate = $.trim($("#txtSearchValidateTo").val());
            }
            if (SearchCouponCode.length < 1) {
                SearchCouponCode = null;
            }

            var searchcouponID = $("#hdnCouponID").val();
            var userName = $.trim($("#txtSearchUserName").val());
            var couponStatusID = $.trim($("#ddlCouponStatus").val());
            if (couponStatusID == "0") {
                couponStatusID = null;
            }
            if (userName.length < 1) {
                userName = null;
            }
            if (seachByCouponUser == 1) {
                couponMgmt.BindCouponUsers(searchcouponID, null, userName, couponStatusID);
            } else {
                couponMgmt.BindCouponDetails(SearchCouponTypeId, SearchCouponCode, validateFromDate, validateToDate, userName);
            }
        },
        ajaxSuccess: function (msg) {
            switch (couponMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(msg.d, function (index, value) {
                        var couponTypeElements = "<option value=" + value.CouponTypeID + ">" + value.CouponType + "</coupon>";
                        $("#ddlCouponType").append(couponTypeElements);
                        $("#ddlSearchCouponType").append(couponTypeElements);
                    });
                    break;
                case 2:
                    $.each(msg.d, function (index, value) {
                        var couponStatusElements = "<option value=" + value.CouponStatusID + ">" + value.CouponStatus + "</option>";
                        $("#ddlCouponStatusType").append(couponStatusElements);
                        $("#ddlCouponStatus").append(couponStatusElements);
                    });
                    break;
                case 3:
                    var checkMessage = msg.d.split(',');
                    couponMgmt.BindCouponDetails(null, null, null, null, null);
                    couponMgmt.HideAllCouponDivs();
                    $("#divShowCouponDetails").show();
                    $('#gdvCouponUser').hide();
                    if (checkMessage[1] == "emailSend" && checkMessage[0] == "dataSave") {
                        csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Email has been send successfully for selected customer.") + "<br/>" + getLocale(AspxCouponManagement, "And coupon code has been saved successfully.") + "</p>");
                    } else if (checkMessage[1] == "emailIDBlank" && checkMessage[0] == "dataSave") {
                        if (portalusers_emailid == '') {
                            csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon code has been saved successfully.") + "</p>");
                        }
                    } else if (checkMessage[1] == "emailSendFail" && checkMessage[0] == "dataSave") {
                        csscody.info("<h2>Information Message</h2><p>" + getLocale(AspxCouponManagement, "Coupon code has been saved successfully.") + "<br/>" + getLocale(AspxCouponManagement, "But Email send fail for selected Customer.") + "</p>");
                    }
                    else if (checkMessage[1] == "emailSendFail" && checkMessage[0] == "dataSaveFail") {
                        csscody.error("<h2>" + getLocale(AspxCouponManagement, "Error Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Email send fail for selected Customer.") + "<br/> " + getLocale(AspxCouponManagement, "And fail to save coupon code!") + "</p>");
                    }
                    break;
                case 4:
                    $.each(msg.d, function (index, value) {
                        if (value.SettingID == 1) {
                            $("#txtUsesPerCoupon").val(value.SettingValue);
                        } else if (value.SettingID == 2) {
                            $("#txtUsesPerCustomer").val(value.SettingValue);
                        } else {
                            $("#ddlIsForFreeShipping").val(value.SettingValue);
                        }
                    });
                    break;
                case 5:
                    csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon code has been deleted successfully.") + "</p>");
                    couponMgmt.BindCouponDetails(null, null, null, null, null);
                    break;
                case 6:
                    csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon user has been deleted successfully.") + "</p>");
                    var couponID = $("#hdnCouponID").val();
                    couponMgmt.BindCouponUsers(couponID, null, null, null);
                    break;
                case 7:
                    var couponID = $("#hdnCouponID").val();
                    couponMgmt.BindCouponUsers(couponID, null, null, null);
                    csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon user has been updated successfully.") + "</p>");
                    $("#divShowCouponDetails").show();
                    $("#divCouponUserForm").hide();
                    break;
                case 8:
                    couponMgmt.BindCouponDetails(null, null, null, null, null);
                    couponMgmt.HideAllCouponDivs();
                    $("#divShowCouponDetails").show();
                    gridData = [];
                    csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Promo code has been save successfully.") + "</p>");
                    break;
                case 9:
                    if (msg.d == true) {
                        $("#txtNewCoupon").val('');
                        csscody.info("<h2>" + getLocale(AspxCouponManagement, "Information Message") + "</h2><p>" + getLocale(AspxCouponManagement, "Coupon code already exists.") + "</p>");
                    }
                    break;
                case 10:
                    var length = msg.d.length;
                    if (length > 0) {
                        var catCheckedItemID = msg.d;
                        if (catCheckedItemID != null) {
                            if (catCheckedItemID != "") {
                                var catArr = [];
                                catArr = catCheckedItemID.split(',');
                                var index = SageData.getIndex("gdvPromoItems");
                                var catArrLength = catArr.length;
                                for (var i = 0; i < catArrLength; i++) {
                                    SageData.pushArr(index, catArr[i]);
                                }
                            }
                        }
                    }
                    break;
            }
        },
        ajaxFailure: function (data) {
            switch (couponMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxCouponManagement, "Error Message") + '</h1><p>' + getLocale(AspxCouponManagement, "Failed to load coupon type!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h1>' + getLocale(AspxCouponManagement, "Error Message") + '</h1><p>' + getLocale(AspxCouponManagement, "Failed to load coupon status!") + '</p>');
                    break;
                case 3:
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Failed to send mail to selected customer!") + "</p>");
                    break;
                case 4:
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Failed to load coupon details!") + "</p>");
                    break;
                case 5:
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Failed to delete!") + "</p>");
                    break;
                case 6:
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Failed to delete coupon user!") + "</p>");
                    break;
                case 7:
                    csscody.alert("<h2>" + getLocale(AspxCouponManagement, "Information Alert") + "</h2><p>" + getLocale(AspxCouponManagement, "Failed to update!") + "</p>");
                    break;
            }
        },
        bindfocusout: function () {
            $('#txtAmount').bind('focusout', function () {
                if ($('#txtAmount').val() == 0) {
                    $('#txtAmount').addClass('error');
                    $('#couponAmountErrorLabel').html(getLocale(AspxCouponManagement, "Zero coupon amount is not allowed!!")).css("color", "red");
                } else {
                    $('#txtAmount').removeClass('error');
                    $('#couponAmountErrorLabel').html('').css("color", "red");
                }
            });
        },
        CreateCouponTypeDdl: function (type) {
            if (type == 1) {
                $('#ddlCouponAmountType').html("<option selected=\"selected\" value=\"0\">Absolute (" + curSymbol + ")</option>");
            } else if (type == 2) {
                $('#ddlCouponAmountType').html("<option selected=\"selected\" value=\"0\">Absolute (" + curSymbol + ")</option><option value=\"1\">Percent (%)</option>");
            } else {
                $('#ddlCouponAmountType').html("<option selected=\"selected\" value=\"0\">Absolute (" + curSymbol + ")</option><option value=\"1\">Percent (%)</option>");
            }
        },
        init: function () {
            couponMgmt.LoadCouponAjaxImage();
            $('.cssClassUsesPerCoupon').hide();
            $('.cssClassddlCouponStatus').hide();
            $('.cssClasstxtSearchUserName').hide();
            $('#btnRefresh').show();
            $('#gdvCouponUser').hide();
            $("#divCouponUserForm").hide();
            $("#txtValidFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtValidTo").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtSearchValidateFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtSearchValidateTo").datepicker({ dateFormat: 'yy/mm/dd' });
            couponMgmt.BindCouponDetails(null, null, null, null, null);
            couponMgmt.GetAllCouponType();
            couponMgmt.GetCouponStatus();
            couponMgmt.HideAllCouponDivs();
            $("#divShowCouponDetails").show();
            couponMgmt.CreateCouponTypeDdl(0);
            $(".hasDatepicker").bind("contextmenu", function (e) {
                return false;
            });
            $('#txtUsesPerCustomer,#txtAmount,.hasDatepicker').bind('paste', function (e) {
                e.preventDefault();
            });
            $("#txtNewCoupon").blur(function () {
                couponMgmt.CheckUniqueCouponCode($.trim($(this).val()));
            });
            $("#btnAddNewCoupon").bind('click', function () {
                $("#" + lblCouponManageTitle).html(getLocale(AspxCouponManagement, "Add New Coupon"));
                $("#hdnCouponID").val(0);
                $("#txtNewCoupon").removeAttr("disabled", "disabled");
                couponMgmt.ClearCouponForm();
                couponMgmt.HideAllCouponDivs();

                $("#divCouponForm").show();
                $("#btnGenerateCode").show();
                $("#spancouponCode").html('');
                $("#tblEditCouponForm tbody #trCouponCustomer").hide();
                $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
            });
            $("#btnGenerateCode").click(function () {
                $("#spancouponCode").html('');
            });
            $("#btnCancelCouponUpdate").click(function () {
                couponMgmt.HideAllCouponDivs();

                $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
                $("#tblEditCouponForm tbody #trCouponCustomer").show();
                $("#divShowCouponDetails").show();
            });
            $('#txtAmount').bind('focusout', function () {
                if ($('#txtAmount').val() == 0) {
                    $('#txtAmount').addClass('error');
                    $('#couponAmountErrorLabel').html(getLocale(AspxCouponManagement, "Zero coupon amount is not allowed!!")).css("color", "red");
                } else {
                    $('#txtAmount').removeClass('error');
                    $('#couponAmountErrorLabel').html('').css("color", "red");
                }
            });
            var c = $("#form1").validate({
                rules: {
                    newCoupon: "required",
                    amount: "required",
                    validateFrom: "required",
                    userPerCustomer: "required",
                    usesPerCustomer: "required"
                },
                messages: {
                    newCoupon: "at least 2 chars",
                    amount: "*",
                    validateFrom: "*",
                    validateTo: "*",
                    userPerCustomer: "*",
                    usesPerCustomer: "*"
                },
                ignore: ':hidden'
            });
            $("#btnSubmitCoupon").click(function () {
                if (Date.parse($('.from').val()) > Date.parse($('.to').val())) {
                    $('.to').parents('td').find('input').css({ "background-color": "#FCC785" });
                    $('#created').html('').html(getLocale(AspxCouponManagement, 'Valid To date must be higher than Valid From date!'));
                    return false;
                } else {
                    $('#created').html('');
                    $('.to').parents('td').find('input').prop("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                var UsesPerCoupon = 0;
                var settingId = "1,2,3,4";
                var minAmountToApply = $.trim($("#txtApplyAmountRange").val());
                if (minAmountToApply == null || minAmountToApply == '') {
                    minAmountToApply = 0;
                }
                var userPerCustomer = $("#txtUsesPerCustomer").val();
                if (userPerCustomer == "") {
                    userPerCustomer = 0;
                }
                var settingValue = UsesPerCoupon + "," + userPerCustomer + "," + $("#ddlIsForFreeShipping option:selected").text() + "," + minAmountToApply;
                if ($('#ddlCouponType option:selected').val() != 0) {
                    if (c.form()) {
                        if ($("#txtNewCoupon").val() == '') {
                            $("#spancouponCode").html(getLocale(AspxCouponManagement, 'Coupon Code Is Required')).css("color", "red");
                            return false;
                        } else {
                            couponMgmt.AddUpdateCoupon(settingId, settingValue);
                            return false;
                        }
                    } else {
                        return false;
                    }
                } else {
                    $('#ddlCouponType').prop('class', 'sfListmenu error');
                    return false;
                }
            });
            $("#tblEditCouponForm tbody #trCouponCustomer").show();
            $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
            $("#ddlCouponType").change(function () {
                if ($(this).val() == 1) {
                    couponMgmt.GetAllItems(null, null);
                    $("#tblEditCouponForm tbody #trCouponCustomer,#trUserPerCustomer").hide();
                    $("#tblEditCouponForm tbody #trPromoCodeItems").show();
                    couponMgmt.CreateCouponTypeDdl(1);
                } else if ($(this).val() == 0) {
                    $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
                    $("#tblEditCouponForm tbody #trCouponCustomer").hide();
                    couponMgmt.CreateCouponTypeDdl(0);
                } else {
                    $("#tblEditCouponForm tbody #trCouponCustomer,#trUserPerCustomer").show();
                    $("#tblEditCouponForm tbody #trPromoCodeItems").hide();
                    couponMgmt.CreateCouponTypeDdl(0);
                }
            });

            $('#btnBackToCouponTbl').click(function () {
                $('#gdvCouponUser').hide();
                $('#btnBackToCouponTbl').hide();
                $('#gdvCoupons').show();
                $('#gdvCoupons_Pagination').show();
                $('#gdvCouponUser_Pagination').hide();
                $("#btnAddNewCoupon").show();
                deleteAllSelectedCouponUser = 0;
                seachByCouponUser = 0;

                $('.cssClassddlCouponStatus').hide();
                $('.cssClasstxtSearchUserName').hide();
                $('#btnRefresh').show();
                $('.cssClassddlCouponType').show();
                $('.cssClasstxtSearchCouponCode').show();
                $('.cssClasstxtSearchValidateFrom').show();
                $('.cssClasstxtSearchValidateTo').show();
                $('#btnDeleteSelectedCoupon').show();
                $('.cssClassSearchPanel').show();
            });

            $("#btnCancelCouponUserUpdate").click(function () {
                $("#divShowCouponDetails").show();
                $("#divCouponUserForm").hide();
                $('#ddlCouponType').val(0);
            });
            $("#btnSubmitCouponUser").click(function () {
                couponMgmt.UpdateCouponUser();
            });
            $("#btnDeleteSelectedCoupon").click(function () {
                var coupon_Ids = '';
                if (deleteAllSelectedCouponUser == 1) {
                    coupon_Ids = SageData.Get("gdvCouponUser").Arr.join(',');
                } else {
                    coupon_Ids = SageData.Get("gdvCoupons").Arr.join(',');
                }

                if (coupon_Ids.length == 0) {
                    if (deleteAllSelectedCouponUser == 1) {
                        csscody.alert('<h2>' + getLocale(AspxCouponManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCouponManagement, "Please select at least one coupon user before delete.") + '</p>');
                        return false;
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxCouponManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCouponManagement, "Please select at least one coupon before delete.") + '</p>');
                        return false;
                    }

                }
                var properties = {
                    onComplete: function (e) {
                        if (deleteAllSelectedCouponUser == 1) {
                            couponMgmt.DeleteCouponUserByID(coupon_Ids, e);
                        } else {
                            couponMgmt.DeleteMultipleCoupons(coupon_Ids, e);
                        }
                    }
                };
                if (deleteAllSelectedCouponUser == 1) {
                    csscody.confirm("<h1>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h1><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete the selected coupon user(s)?") + "</p>", properties);
                } else {
                    csscody.confirm("<h1>" + getLocale(AspxCouponManagement, "Delete Confirmation") + "</h1><p>" + getLocale(AspxCouponManagement, "Are you sure you want to delete the selected coupon code(s)?") + "</p>", properties);
                }
            });
            $('#ddlIsForFreeShipping').change(function () {
                if ($('#ddlIsForFreeShipping option:selected').val() == 2) {
                    $("#txtAmount").prop("disabled", "disabled");
                    $("#txtAmount").val('');
                    $("#txtAmount").parents('tr').hide();
                } else {
                    $("#txtAmount").removeAttr("disabled");
                    $("#txtAmount").parents('tr').show();
                }
            });
            $("#txtValidFrom").bind("change", function () {
                $('#created').html('');
                $('.to').parents('td').find('input').prop("style", '');
                $(this).removeClass('error');
                $('.to').parents('td').find('label').remove();
            });
            $("#txtValidTo").bind("change", function () {
                if ($(this).val() != "") {
                    $('#created').html('');
                    $('.to').parents('td').find('input').prop("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                $(this).removeClass('error');
                $('.from').parents('td').find('label').remove();
            });

            $("#txtAmount").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtApplyAmountRange").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtUsesPerCoupon").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtUsesPerCustomer").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $('#ddlSearchCouponType,#txtSearchCouponCode,#txtSearchValidateFrom,#txtSearchValidateTo,#txtSearchUserName').keyup(function (event) {
                if (event.keyCode == 13) {
                    $('#ddlSearchCouponType,#txtSearchCouponCode,#txtSearchValidateFrom,#txtSearchValidateTo,#txtSearchUserName').blur();
                    couponMgmt.SearchCouponDetails();
                }
            });
            $("#ddlCouponAmountType").change(function () {
                if (catalogDot == true) {
                    catalogCount--;
                }
                if (catalogCount == -1) {
                    $(this).prop('maxlength', 11);
                }
                if (catalogDot == true && catalogBakCount >= catalogCount) {
                    catalogDot = false;
                    catalogBakCount = 0;
                    catalogCount = 0;
                }
                return true;
            });
            $('#ddlCouponAmountType').change(function () {
                $('#txtAmount').removeClass('error');
                $('#couponAmountErrorLabel').html('').css("color", "red");
                $('#txtAmount').val('');
                if ($('#ddlCouponAmountType option:selected').val() == 1) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCouponManagement, " must be lower than 100")).fadeOut(5000);
                    $('#txtAmount').unbind();
                    $('#txtAmount').numeric({ max: 99.99 });
                    $('#txtAmount').prop('maxlength', 5);
                    couponMgmt.bindfocusout();
                    if ($('#txtAmount').val() >= 100) {
                        $('#txtAmount').val('');
                    }
                } else {
                    $('#txtAmount').unbind();
                    $('#txtAmount').numeric({ max: 99999999.99 });
                    $('#txtAmount').prop('maxlength', 8);
                    couponMgmt.bindfocusout();
                }
            });
            $("#txtAmount").keypress(function (e) {
                if ($.trim($("#ddlCouponAmountType").val()) == 0) {
                    $('#txtAmount').unbind();
                    $('#txtAmount').numeric({ max: 99999999.99 });
                    $('#txtAmount').prop('maxlength', 11);
                    var dest1 = e.which - 48;
                    var result1 = this.value + dest1.toString();
                    if (result1 > 99999999.99) {
                        return false;
                    }
                    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                    couponMgmt.bindfocusout();
                } else {
                    $('#txtAmount').unbind();
                    $('#txtAmount').numeric({ max: 99.99 });
                    $('#txtAmount').prop('maxlength', 5);
                    if (e.which == 8 || e.which == 0 || e.which == 46)
                        return true;
                    if (e.which < 48 || e.which > 57)
                        return false;
                    var dest = e.which - 48;
                    var result = this.value + dest.toString();
                    if (result > 99.99) {
                        return false;
                    }
                    couponMgmt.bindfocusout();
                }
            });
        }
    };
    couponMgmt.init();
});