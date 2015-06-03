var CurrencyManage = "";
var CurrencyID = 0;
var dot = false;
var bakCount = 0;
var count = 0;
(function($) {
    $.fn.numeric = function(options) {
     
        return this.each(function() {
            var $this = $(this);
            $this.keypress(options, function(e) {
                if ($this.val() == '') {
                    $this.prop('maxlength', 11);
                }
                if (e.which == 8 || e.which == 0) {
                                       if (dot == true) {
                        count--;
                        if (count == 0) {
                            dot = false;
                        }
                    }
                    if (count == -1) {
                        $this.prop('maxlength', 11);
                    }
                    if (dot == true && count >= bakCount) {
                        dot = false;
                        bakCount = 0;
                        count = 0;
                    }
                    return true;
                }
                if (e.which == 46) {
                    if (dot == false) {
                        dot = true;
                        bakCount = 0;
                        count = 0;
                        return true;
                    }
                }
                if (dot == true) {
                    var z = $this.val();
                    z = z.split('.');
                    if (z[1] != undefined) {
                        $this.prop('maxlength', z[0].length + 5);
                    }
                } else {
                    $this.prop('maxlength', 11);
                }
                if (dot == true) {
                    if (count < 4) {
                        count++;
                    }
                    bakCount = count;
                }
                                              if (e.which < 48 || e.which > 57) {
                    return false;
                } else {
                                   }
                               var dest = e.which - 48;
                var result = this.value + dest.toString();
                if (parseFloat(result) >= e.data.max) {
                    return false;
                }
            });
        });
    };
})(jQuery);
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

    var isUnique = true;
    var isUniqueCurrCode = true;
    CurrencyManage = {
        config: {
            isPostBack: false,
            async: false,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: "",
            error: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: CurrencyManage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: CurrencyManage.config.contentType,
                cache: CurrencyManage.config.cache,
                async: CurrencyManage.config.async,
                url: CurrencyManage.config.url,
                data: CurrencyManage.config.data,
                dataType: CurrencyManage.config.dataType,
                success: CurrencyManage.config.ajaxCallMode,
                error: CurrencyManage.ajaxFailure
            });
        },

        BindCurrencylistInGrid: function() {
            this.config.method = "BindCurrencyAddedLists";
            this.config.data = { aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblCurrencyManage_pagesize").length > 0) ? $("#tblCurrencyManage_pagesize :selected").text() : 10;
            $("#tblCurrencyManage").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCurrenciesManagement, "CurrencyID"), name: 'CurrencyID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', checkFor: '10', align: 'center', elemClass: 'currencyChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxCurrenciesManagement, "Curreny Name"), name: 'CurrencyName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCurrenciesManagement, "Currency Code"), name: 'CurrencyCode', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCurrenciesManagement, "Currency Flag"), name: 'BaseImage', coltype: 'image', cssclass: 'cssClassImageHeader', controlclass: 'cssClassGridFlagImage', alttext: '1', align: 'left' },
                    { display: getLocale(AspxCurrenciesManagement, "Rate"), name: 'ConversionRate', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCurrenciesManagement, "Currency Symbol"), name: 'CurrencySymbol', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCurrenciesManagement, "Country"), name: 'CountryName', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCurrenciesManagement, "Region"), name: 'Region', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCurrenciesManagement, "DisplayOrder"), name: 'DisplayOrder', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCurrenciesManagement, "CultureName"), name: 'CultureName', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCurrenciesManagement, "Default"), name: 'IsPrimaryForStore', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxCurrenciesManagement, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxCurrenciesManagement, "Action"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCurrenciesManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CurrencyManage.EditCurrency', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13' },
                    { display: getLocale(AspxCurrenciesManagement, "Set Store Primary"), name: 'setstoreprimary', enable: true, _event: 'click', trigger: '2', callMethod: 'CurrencyManage.SetStorePrimary', arguments: '2' },
                    { display: getLocale(AspxCurrenciesManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '3', callMethod: 'CurrencyManage.DeleteCurrency', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCurrenciesManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                imageOfType: 'currency',
                sortcol: { 0: { sorter: false }, 12: { sorter: false} }
            });
        },

        SetStorePrimary: function(tblID, argus) {
            switch (tblID) {
                case "tblCurrencyManage":
                    var CurrencyCode = argus[3];
                    CurrencyManage.config.url = CurrencyManage.config.baseURL + "SetStorePrimary";
                    CurrencyManage.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), currencyCode: CurrencyCode });
                    CurrencyManage.config.ajaxCallMode = CurrencyManage.SetStorePrimarySuccess;
                    CurrencyManage.ajaxCall(CurrencyManage.config);
                    break;
                default:
                    break;
            }
        },

        SetStorePrimarySuccess: function() {
        csscody.info("<h2>" + getLocale(AspxCurrenciesManagement, "Information Message") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Primary store updated successfully") + "</p>");        
            CurrencyManage.BindCurrencylistInGrid();
            CurrencyManage.HideAllAdvertiseDivs();
            $("#divCurrencyManage").show();
        },

        EditCurrency: function(tblID, argus) {
            switch (tblID) {
                case "tblCurrencyManage":
                    CurrencyManage.ClearData();
                    $("#_lblEditCurrencyName").html(getLocale(AspxCurrenciesManagement, "Edit Currency: ") + argus[3]);
                    $('#' + hdnInputID).attr("Value", argus[9] + ',' + argus[7]+','+argus[4]);
                    CurrencyManage.HideAllAdvertiseDivs();

                    $("#divCurrencyForm").show();
                    CurrencyID = argus[0];
                    $("#txtCurrencyName").val(argus[3]);
                    $("#currencyCode").val(argus[4]);
                    $("#txtConversionRate").val(argus[6]);
                    $("#txtCurrencySymbol").val(argus[7]);
                   
                    var image = argus[5].split('.');
                    $('#ddlCountryFlag').val(image[0]);
                    $('#ddlCountryFlag').removeClass('makeMeFancy');
                    $('#ddlCountryFlag').show();
                    $('#ddlCountryFlag').addClass('makeMeFancy');
                    $(".tzSelect").remove();
                    $('#ddlCountryFlag').attr("disabled", true);
                    MakeFancyDropDown();

                    $("#ddlCountry option:selected").text(argus[8]);
                    $("#txtRegion").val(argus[9]);
                    $("#txtDisplayOrder").val(argus[10]);
                    $("#ddlCountryFlag").val(argus[6]);
                    if (argus[13].toLowerCase() == "yes") {
                        $("#cbIsActive").prop("checked", "checked");
                    } else {
                        $("#cbIsActive").removeAttr("checked");
                    }
                    if (argus[12].toLowerCase() == "yes") {
                        $("#txtConversionRate").prop("disabled", "disabled");
                        $("#cbIsActive").prop("disabled", "disabled");
                    } else {
                        $("#txtConversionRate").removeAttr("disabled");
                        $("#cbIsActive").removeAttr("disabled");
                    }
                    $('#txtConversionRate').unbind().numeric({ max: 99999999 });
                    CurrencyManage.ActivateNumericFun();
                    $('#txtConversionRate').trigger('keypress');
                    break;
                default:
                    break;
            }
        },

        CheckUniqueness: function(value, id) {
            this.config.url = this.config.baseURL + "CheckUniquenessForDisplayOrderForCurrency";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), value: value, currencyID: id });
            this.config.ajaxCallMode = CurrencyManage.CheckDisplayOrderUniqueNessSuccess;
            this.ajaxCall(this.config);
        },

        CheckCurrCodeUniqueness: function(currencyCode, currencyID) {
            this.config.url = this.config.baseURL + "CheckCurrencyCodeUniqueness";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), currencyCode: currencyCode, currencyID: currencyID });
            this.config.ajaxCallMode = CurrencyManage.SetIsUnique;
            this.ajaxCall(this.config);
            return isUniqueCurrCode;
        },

        CheckDisplayOrderUniqueNessSuccess: function(data) {
            isUnique = data.d;
            if (isUnique == 0) {
             $("#erruniqueOrder").html(getLocale(AspxCurrenciesManagement, "Already Exist")).css("color", "red");
                $("#txtDisplayOrder").val('');
            } else {
                $("#erruniqueOrder").html('');
            }
        },

        SetIsUnique: function(data) {
            isUniqueCurrCode = data.d;
        },
        InsertCurrency: function(currencyID, currencyName, currencyCode, currencySymbol, countryName, region, conversionRate, displayOrder, countryFlag, isActive) {
            var currencyInsertObj = {
                RowTotal: 0,
                CurrencyID: currencyID,
                CurrencyName: currencyName,
                CurrencyCode: currencyCode,
                BaseImage: countryFlag,
                ConversionRate: conversionRate,
                CurrencySymbol: currencySymbol,
                CountryName: countryName,
                Region: region,
                DisplayOrder: displayOrder,
                CultureName: '',
                IsPrimaryForStore: false,
                IsActive: isActive
            };
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), currencyInsertObj: currencyInsertObj });
            this.config.method = "InsertNewCurrency";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = CurrencyManage.InsertCurrencySuccess;
            this.ajaxCall(this.config);
        },
        InsertCurrencySuccess: function() {
            if (CurrencyID > 0) {
                csscody.info("<h2>" + getLocale(AspxCurrenciesManagement, "Information Message") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Currency Updated successfully.") + "</p>");
                CurrencyManage.HideAllAdvertiseDivs();
                $("#divCurrencyManage").show();
                CurrencyManage.BindCurrencylistInGrid();
            } else {
            csscody.info("<h2>" + getLocale(AspxCurrenciesManagement, "Information Message") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Currency Inserted successfully.") + "</p>");
                CurrencyManage.HideAllAdvertiseDivs();
                $("#divCurrencyManage").show();
                CurrencyManage.BindCurrencylistInGrid();
            }
           
        },

        RealTimeUpdateSuccess: function() {
        csscody.info("<h2>" + getLocale(AspxCurrenciesManagement, "Information Message") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Realtime Rate Updated successfully.") + "</p>");
            CurrencyManage.BindCurrencylistInGrid();
            CurrencyManage.HideAllAdvertiseDivs();
            $("#divCurrencyManage").show();
        },

        ConfirmDeleteMultiple: function(currencyIDs, event) {
            if (event) {
                CurrencyManage.DeleteMultipleCurrencies(currencyIDs);
            }
            return false;
        },

        DeleteCurrency: function(tblID, argus) {
            switch (tblID) {
                case "tblCurrencyManage":
                    if (AspxCommerce.vars.IsAlive) {
                        if (argus[12].toLowerCase() == "no") {
                            CurrencyManage.DeleteCurrencyByID(argus[0]);
                        } else {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Sorry! Primary currency can not be deleted.") + '</p>');
                        }
                    } else {
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                    }
                    break;
                default:
                    break;
            }
        },
        DeleteCurrencyByID: function(currencyId) {
            var properties = {
                onComplete: function(e) {
                    CurrencyManage.ConfirmSingleDelete(currencyId, e);
                }
            };

            csscody.confirm("<h2>" + getLocale(AspxCurrenciesManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Are you sure you want to delete this currency?") + "</p>", properties);
        },

        ConfirmSingleDelete: function(currencyId, event) {
            if (event) {
                CurrencyManage.DeleteMultipleCurrencies(currencyId);
            }
            return false;
        },

        DeleteMultipleCurrencies: function(currencyIDs) {
                       this.config.url = this.config.baseURL + "DeleteMultipleCurrencyByCurrencyID";
            this.config.data = JSON2.stringify({ currencyIDs: currencyIDs, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = CurrencyManage.DeleteSucess;
            this.ajaxCall(this.config);
        },
        DeleteSucess: function() {
        csscody.info('<h2>' + getLocale(AspxCurrenciesManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Selected Currency has been deleted successfully.") + '</p>');
            CurrencyManage.BindCurrencylistInGrid();
        },
        BindCountryDetils: function (msg) {
            var length = msg.d.length;
            if (length > 0) {
                CurrencyManage.ClearData();
                var item;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    $("#ddlCountry").val(item.CountryCode);
                    if (item.CurrencyName == '' || item.CurrencyName == null) {
                        $('#txtCurrencyName').val('');
                    } else {
                        $('#txtCurrencyName').val(item.CurrencyName).prop('disabled', 'disabled');
                    }
                    if (item.Region == '' || item.Region == null) {
                        $("#txtRegion").val('');
                    } else {
                        $("#txtRegion").val(item.Region).prop('disabled', 'disabled');
                    }
                    if (item.CurrencySymbol == '' || item.CurrencySymbol == null) {
                        $("#txtCurrencySymbol").val();
                        if (!$("#txtRegion").val() == '') 
                        {
                            countryObj = $.formatCurrency.regions[$("#txtRegion").val()];
                            $("#txtCurrencySymbol").val(countryObj.symbol);
                        }
                        
                    } else {
                        $("#txtCurrencySymbol").val(item.CurrencySymbol).prop('disabled', 'disabled');
                    }
                    if (item.CurrencyCode == '' || item.CurrencyCode == null) {
                        $('#currencyCode').val(0);
                    } else {
                        $('#currencyCode').val(item.CurrencyCode);
                        $('#currencyCode').prop('disabled', 'disabled');
                    }
                    var image = item.CountryCode.toLowerCase();
                    $('#ddlCountryFlag').val(image);
                    $('#ddlCountryFlag').removeClass('makeMeFancy');
                    $('#ddlCountryFlag').show();
                    $('#ddlCountryFlag').addClass('makeMeFancy');
                    $(".tzSelect").remove();
                    $('.selectBox').prop('disabled', 'disabled');
                    MakeFancyDropDown();
                };
            } else {
            csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Sorry! Currency Information for selected country is not available!") + '</p>');
                CurrencyManage.ClearData();
            }
        },
        GetDetailsByCountryCode: function(countryCode, countryName) {
            this.config.url = this.config.baseURL + "GetDetailsByCountryCode";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), countryCode: countryCode, countryName: countryName });
            this.config.ajaxCallMode = CurrencyManage.BindCountryDetils;
            this.ajaxCall(this.config);
        },
        ActivateNumericFun: function() {
            $('#txtConversionRate').on('keyup', function() {
                var val = $('#txtConversionRate').val();
                if (val.indexOf('.') != -1) {
                    var index = val.indexOf('.');
                    dot = true;
                    count = val.split('.')[1].length;
                } else {
                    count = 0;
                    dot = false;
                }
            });
            $('#txtConversionRate').on('focus', function() {

                var val = $('#txtConversionRate').val();
                if (val.indexOf('.') != -1) {
                    var index = val.indexOf('.');
                    dot = true;
                    count = val.split('.')[1].length;
                } else {

                    count = 0; dot = false;
                }
            });
                                                                                                                                                                  },
        ClearData: function() {
            $("#txtCurrencyName").val('');
            $(".tzSelect").remove();
            $("#currencyCode").val(0);
            $("#ddlCountry").val(0);
            $("#ddlCountry option:selected").text('- Select One -');
            $("#txtCurrencySymbol").val('');
            $("#txtRegion").val('');
            $("#txtConversionRate").val('');
            $("#txtDisplayOrder").val('');
            $("#currencyCode").removeAttr('disabled');
            $("#txtCurrencyName").removeAttr('disabled');
            $("#txtRegion").val('').removeAttr('disabled');
            $('#currencyCode').val('').removeAttr('disabled');
            $("#txtCurrencySymbol").removeAttr('disabled');
        },
        SaveCurrency:function()
        {
            var v = $("#form1").validate({
                messages: {
                    currencyName: {
                        required: '*'
                    },
                    currencySymbol: {
                        required: '*'
                    },
                    region: {
                        required: '*'
                    },
                    conversionRate: {
                        required: '*',
                        maxlength: getLocale(AspxCurrenciesManagement, "* (no more than 6 digits)")
                    },
                    displayOrder: {
                        required: '*'
                    }
                }
            });
            if (v.form()) {
                var isUniqueCurrency = CurrencyManage.CheckCurrCodeUniqueness($("#currencyCode option:selected").val(), CurrencyID);
                if (!isUniqueCurrency) {
                    csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "The currency code must be unique") + '</p>');
                    return false;
                }
                if ($("#txtConversionRate").val() <= 0) {
                    csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "The conversion rate must be greater than zero") + '</p>');
                    return false;
                }
                var rate = $.trim($("#txtConversionRate").val());
                if (rate.indexOf('.') != -1) {
                    var len = rate.split('.');
                    if (len[1].length > 4) {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Please apply appropriate conversion rate (4 digits after decimal).") + '</p>');
                        return false;
                    }
                }
                var currencyName = $("#txtCurrencyName").val();
                if (CurrencyID <= 0) {
                    if ($("#ddlCountry option:selected").val() == 0) {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Select at least one country") + '</p>');
                        return false;
                    }
                }
                if ($("#currencyCode option:selected").val() == 0) {
                    csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Select at least one currency code") + '</p>');
                    return false;
                }
                var currencyCode = $("#currencyCode option:selected").val();
                var currencySymbol = $("#txtCurrencySymbol").val();
                var countryName = $("#ddlCountry option:selected").text();
                var region = $("#txtRegion").val();
                var conversionRate = $("#txtConversionRate").val();
                var displayOrder = $("#txtDisplayOrder").val();
                var countryFlag = $(".selectBox").text();
                var isActive = $("#cbIsActive").prop("checked") == true ? true : false;
                $('#' + hdnInputID).attr("Value", $("#txtRegion").val() + ',' + $("#txtCurrencySymbol").val() + ',' + $("#currencyCode option:selected").val());
                if ($("#erruniqueOrder").html().length == 0) {
                    CurrencyManage.InsertCurrency(CurrencyID, currencyName, currencyCode, currencySymbol, countryName, region, parseFloat(conversionRate), displayOrder, countryFlag, isActive);
                }
            } else {
                return false;
            }
        },
        HideAllAdvertiseDivs: function() {
            $("#divCurrencyManage").hide();
            $("#divCurrencyForm").hide();
        },
        Init: function() {
            CurrencyManage.BindCurrencylistInGrid();
            CurrencyManage.HideAllAdvertiseDivs();
            $("#divCurrencyManage").show();
            var v = $("#form1").validate({
                messages: {
                    currencyName: {
                        required: '*'
                    },
                    currencySymbol: {
                        required: '*'
                    },
                    region: {
                        required: '*'
                    },
                    conversionRate: {
                        required: '*',
                        maxlength: getLocale(AspxCurrenciesManagement, "* (no more than 6 digits)")
                    },
                    displayOrder: {
                        required: '*'
                    }
                }
            });
                       $("#btnAddNewCurrency").click(function() {
                CurrencyID = 0;
                CurrencyManage.ClearData();
                $('#_lblEditCurrencyName').html(getLocale(AspxCurrenciesManagement, "Add New Currency"));
                CurrencyManage.HideAllAdvertiseDivs();
                $("#txtConversionRate").removeAttr("disabled", "disabled");
                $("#divCurrencyForm").show();

                $('#txtConversionRate').prop("maxlength", "11");
                $('#txtConversionRate').numeric({ max: 99999999 });
                $('#' + hdnInputID).attr("Value", $("#txtRegion").val() + ',' + $("#txtCurrencySymbol").val() + ',' + $("#currencyCode option:selected").val());
            });
            CurrencyManage.ActivateNumericFun();

            $("#btnRealTimeUpdate").click(function() {
                var properties = {
                    onComplete: function(e) {
                        if (e) {
                            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                            CurrencyManage.config.method = "RealTimeUpdate";
                            CurrencyManage.config.url = CurrencyManage.config.baseURL + CurrencyManage.config.method;
                            CurrencyManage.config.data = param;
                            CurrencyManage.config.ajaxCallMode = CurrencyManage.RealTimeUpdateSuccess;
                            CurrencyManage.ajaxCall(CurrencyManage.config);
                        }
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxCurrenciesManagement, "Update Confirmation") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Are you sure you want to update the real time value?") + "</p>", properties);
            });

            $('#btnDeleteSelected').click(function() {
                if (AspxCommerce.vars.IsAlive) {
                    var currencyIDs = '';
                                       currencyIDs = SageData.Get("tblCurrencyManage").Arr.join(',');
                    if (currencyIDs.length>0) {
                        var properties = {
                            onComplete: function(e) {
                                CurrencyManage.ConfirmDeleteMultiple(currencyIDs, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxCurrenciesManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCurrenciesManagement, "Are you sure you want to delete the selected item(s)?") + "</p>", properties);
                    } else {
                    csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Please select at least one item before delete.") + '</p>');
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                }
            });

            $("#txtDisplayOrder").focusout(function() {
                $("#erruniqueOrder").show();
                if ($("#txtDisplayOrder").val() == 0) {
                    $("#txtDisplayOrder").val('');
                }
                if ($("#txtDisplayOrder").val() != '')
                    CurrencyManage.CheckUniqueness($("#txtDisplayOrder").val(), CurrencyID);
            });

            $("#btnSave").bind('click', function() {
                if (v.form()) {
                    var isUniqueCurrency = CurrencyManage.CheckCurrCodeUniqueness($("#currencyCode option:selected").val(), CurrencyID);
                    if (!isUniqueCurrency) {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "The currency code must be unique") + '</p>');
                        return false;
                    }
                    if ($("#txtConversionRate").val() <= 0) {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "The conversion rate must be greater than zero") + '</p>');
                        return false;
                    }
                    var rate = $.trim($("#txtConversionRate").val());
                    if (rate.indexOf('.') != -1) {
                        var len = rate.split('.');
                        if (len[1].length > 4) {
                            csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Please apply appropriate conversion rate (4 digits after decimal).") + '</p>');
                            return false;
                        } 
                    }
                    var currencyName = $("#txtCurrencyName").val();
                    if (CurrencyID <= 0) {
                        if ($("#ddlCountry option:selected").val() == 0) {
                            csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Select at least one country") + '</p>');
                            return false;
                        }
                    }
                    if ($("#currencyCode option:selected").val() == 0) {
                        csscody.alert('<h2>' + getLocale(AspxCurrenciesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCurrenciesManagement, "Select at least one currency code") + '</p>');
                        return false;
                    }
                    var currencyCode = $("#currencyCode option:selected").val();
                    var currencySymbol = $("#txtCurrencySymbol").val();
                    var countryName = $("#ddlCountry option:selected").text();
                    var region = $("#txtRegion").val();
                    var conversionRate = $("#txtConversionRate").val();
                    var displayOrder = $("#txtDisplayOrder").val();
                    var countryFlag = $(".selectBox").text();
                    var isActive = $("#cbIsActive").prop("checked") == true ? true : false;
                    $('#' + hdnInputID).attr("Value", $("#txtRegion").val() + ',' + $("#txtCurrencySymbol").val() + ',' + $("#currencyCode option:selected").val());
                    if ($("#erruniqueOrder").html().length == 0) {
                        CurrencyManage.InsertCurrency(CurrencyID, currencyName, currencyCode, currencySymbol, countryName, region, parseFloat(conversionRate), displayOrder, countryFlag, isActive);
                    }
                } else {
                    return false;
                }
            });

            $("#btnCancel").click(function() {
                CurrencyManage.ClearData();
                CurrencyManage.HideAllAdvertiseDivs();
                var validator = $("#form1").validate();
                validator.resetForm();
                $("#divCurrencyManage").show();
            });
            $('#ddlCountry').bind('change', function() {
                var countryCode = $(this).val();
                var countryName = $('#ddlCountry option:selected').text();
                CurrencyManage.GetDetailsByCountryCode(countryCode, countryName);
            });
        }
    };
    CurrencyManage.Init();
});