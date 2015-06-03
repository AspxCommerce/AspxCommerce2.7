    var TaxRate="";
    $(function () {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var taxRateFlag = 0;
        TaxRate = {
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
            init: function () {
                TaxRate.HideAll();
                $('#ddlTaxRateType').html("<option selected=\"selected\" value=\"False\">Absolute (" + curSymbol + ")</option><option value=\"True\">Percent (%)</option>");
                $("#divTaxRatesGrid").show();
                TaxRate.LoadTaxRateStaticImage();
                TaxRate.BindTaxRates(null, null, null, null);
                TaxRate.GetCountryList();
                TaxRate.GetStateList($("#ddlSearchCountry").val());
                $("#languageSelect li").click(function () {
                    $('#languageSelect').find('li').removeClass("languageSelected");
                    $(this).addClass("languageSelected");

                });
                $("#btnAddNewTaxRate").click(function () {
                    $("#ddlState").hide();
                    $("#trZipPostCode").show();
                    $("#trRangeFrom").hide();
                    $("#trRangeTo").hide();
                    TaxRate.ClearForm();
                    TaxRate.HideAll();
                    $("#divTaxRateInformation").show();
                    $("#hdnTaxRateID").val(0);
                });

                $("#ddlCountry").change(function () {
                    TaxRate.GetStateList($(this).val());
                    $("#txtState").val('');
                });

                $("#chkIsTaxZipRange").click(function () {
                    if ($(this).is(':checked')) {
                        $("#trZipPostCode").hide();
                        $("#trRangeFrom").show();
                        $("#trRangeTo").show();
                    } else {
                        $("#trRangeFrom").hide();
                        $("#trRangeTo").hide();
                        $("#trZipPostCode").show();
                    }
                });

                $("#btnDeleteSelected").click(function () {
                    var taxRate_Ids = '';
                    taxRate_Ids = SageData.Get("gdvTaxRateDetails").Arr.join(',');
                    if (taxRate_Ids.length>0) {
                        var properties = {
                            onComplete: function (e) {
                                TaxRate.ConfirmDeleteTaxRates(taxRate_Ids, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete the selected tax rate(s)?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please select at least one tax rate before delete.') + '</p>');
                    }
                });

                var tr = $("#form1").validate(
                {
                    ignore: ':hidden',
                    rules: {
                        rateTitle: "required",
                        rangefrom: "required",
                        rangeTo: "required",
                        taxRate: {
                            required: true,
                            number: true
                        }
                    },
                    messages: {
                        rateTitle: "* (at least 2 chars)",
                        state: "* (at least 2 chars)",
                        zipCode: "* (at least 5 chars)",
                        rangefrom: "* (at least 5 chars)",
                        rangeTo: "* (at least 5 chars)",
                        taxRate: "*"
                    }
                });

                $("#btnSaveTaxRate").click(function () {
                    if ($('#ddlCountry option:selected').val() != 0) {
                        var rangeFrom = parseInt($('#txtRangeFrom').val());
                        var rangeTo = parseInt($('#txtRangeTo').val());

                        var taxRateValue = parseInt($('#txtTaxRateValue').val());

                        if (rangeFrom > 0 && rangeFrom > rangeTo) {
                            csscody.alert("<h2>" + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please enter a valid range!') + "</p>");
                            return false;
                        }

                        if (taxRateValue > 0 && taxRateValue > 100 && $('#ddlTaxRateType').val() == 'True') {
                            csscody.alert("<h2>" + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rate cannot be bigger than 100!') + "</p>");
                            return false;
                        }

                        if (tr.form()) {
                            TaxRate.SaveAndUpdateTaxRate();
                            return false;
                        } else {
                            return false;
                        }
                    } else {
                        $('#ddlCountry ').prop('class', 'sfListmenu error');
                    }
                });

                $("#btnCancel").click(function () {
                    TaxRate.HideAll();
                    $("#divTaxRatesGrid").show();
                });

                $("#txtZipPostCode").DigitOnly('.zipPostcode', '#errmsgZipPostCode');
                $("#txtRangeFrom").DigitOnly('.rangeFrom', '#errmsgRangeFrom');
                $("#txtRangeTo").DigitOnly('.rangeTo', '#errmsgRangeTo');

                $("#ddlTaxRateType").change(function () {
                    if ($.trim($("#ddlTaxRateType").val().toLowerCase()) == "false") {
                    } else {
                        if ($("#txtTaxRateValue").val() < 1000) {
                        } else {
                            $("#txtTaxRateValue").val('');
                        }
                    }
                });
                $("#txtTaxRateValue").keypress(function (e) {
                    if ($.trim($("#ddlTaxRateType").val().toLowerCase()) == "false") {
                        if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                            $("#errmsgTaxRateValue").html(getLocale(AspxTaxManagement,"Enter Digits And Decimal Only")).css("color", "red").show().fadeOut(1600);
                            return false;
                        }
                    } else {
                        if (e.which == 8 || e.which == 0 || e.which == 46)
                            return true;
                                               if (e.which < 48 || e.which > 57)
                            return false;
                                               var dest = e.which - 48;
                        var result = this.value + dest.toString();
                        if (result > 999.99) {
                            return false;
                        }
                    }
                });
                $('#txtRateTitle,#ddlSearchCountry,#txtSearchState,#txtSearchZip').keyup(function (event) {
                    if (event.keyCode == 13) {
                        TaxRate.SearchTaxRate();
                    }
                });
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: TaxRate.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: TaxRate.config.contentType,
                    cache: TaxRate.config.cache,
                    async: TaxRate.config.async,
                    data: TaxRate.config.data,
                    dataType: TaxRate.config.dataType,
                    url: TaxRate.config.url,
                    success: TaxRate.ajaxSuccess,
                    error: TaxRate.ajaxFailure
                });
            },
            LoadTaxRateStaticImage: function () {
                $('#ajaxTaxRateImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            HideAll: function () {
                $("#divTaxRatesGrid").hide();
                $("#divTaxRateInformation").hide();
            },
            ClearForm: function () {
                $("#" + lblTaxRateHeading).html(getLocale(AspxTaxManagement,"New Tax Rate Information"));
                $("#txtTaxRateTitle").val('');
                $("#ddlCountry").val(0);
                $("#ddlState").val('');
                $("#txtZipPostCode").val('');
                $("#chkIsTaxZipRange").removeAttr('checked');
                $("#txtRangeFrom").val('');
                $("#txtRangeTo").val('');
                $("#txtTaxRateValue").val('');
                $("#txtState").val('');
                $('#txtTaxRateTitle').removeClass('error');
                $('#txtTaxRateTitle').parents('td').find('label').remove();
                $('#txtZipPostCode').removeClass('error');
                $('#txtZipPostCode').parents('td').find('label').remove();
                $('#txtRangeFrom').removeClass('error');
                $('#txtRangeFrom').parents('td').find('label').remove();
                $('#txtRangeTo').removeClass('error');
                $('#txtRangeTo').parents('td').find('label').remove();
                $('#txtState').removeClass('error');
                $('#txtState').parents('td').find('label').remove();
                $('#txtTaxRateValue').removeClass('error');
                $('#txtTaxRateValue').parents('td').find('label').remove();
            },
            BindTaxRates: function (taxName, country, state, zipPostCode) {
                this.config.method = "GetTaxRateDetails";
                var taxRateDataObj = {
                    TaxName: taxName,
                    Country: country,
                    State: state,
                    Zip: zipPostCode
                };
                this.config.data = { taxRateDataObj: taxRateDataObj, aspxCommonObj: aspxCommonObj };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxRateDetails_pagesize").length > 0) ? $("#gdvTaxRateDetails_pagesize :selected").text() : 10;

                $("#gdvTaxRateDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: getLocale(AspxTaxManagement, 'TaxRate_ID'), name: 'taxrate_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxRateChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: getLocale(AspxTaxManagement, 'Tax Rate Title'), name: 'tax_rate_title', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Country'), name: 'country', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'State/Province'), name: 'state_region', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Zip/Post Code'), name: 'tax_zip_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Zip/Post In Range'), name: 'is_tax_zip_range', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Tax Rate Value'), name: 'tax_rate_value', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Rate In Percentage'), name: 'tax_rate_value', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: getLocale(AspxTaxManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'TaxRate.EditTaxRate', arguments: '1,2,3,4,5,6,7' },
                        { display: getLocale(AspxTaxManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'TaxRate.DeleteTaxRate', arguments: '' }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxTaxManagement, "No Records Found!"),
                    param: data,                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 8: { sorter: false} }
                });
            },
            EditTaxRate: function (tblID, argus) {
                switch (tblID) {
                    case "gdvTaxRateDetails":
                        $("#" + lblTaxRateHeading).html(getLocale(AspxTaxManagement,'Edit Tax Rate:') + argus[3]);
                        $("#hdnTaxRateID").val(argus[0]);
                        $("#txtTaxRateTitle").val(argus[3]);

                        $("#ddlCountry option").each(function () {
                            if ($(this).text() == argus[4]) {
                                $(this).prop("selected", "selected");
                            }
                        });
                        $.ajax({
                            type: "POST", beforeSend: function (request) {
                                request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                request.setRequestHeader("UMID", umi);
                                request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                request.setRequestHeader("PType", "v");
                                request.setRequestHeader('Escape', '0');
                            },
                            url: TaxRate.config.baseURL + "BindStateList",
                            data: JSON2.stringify({ countryCode: $("#ddlCountry option:selected").val() }),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                $("#ddlState").html('');
                                $.each(msg.d, function (index, item) {
                                    $("#ddlState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                                });
                                var length = msg.d.length;
                                if (length > 2) {
                                    $("#ddlState option").each(function () {
                                        if ($(this).text() == argus[5]) {
                                            $(this).prop('selected', 'selected');
                                        }
                                    });

                                    $("#txtState").hide();
                                    $("#ddlState").show();
                                } else {
                                    $("#txtState").val(argus[5]);
                                    $("#ddlState").hide();
                                    $("#txtState").show();
                                }
                            }
                        });
                        $("#txtZipPostCode").val(argus[6]);
                        var range = argus[6];
                        var subStr = range.split('-');
                        $("#txtRangeFrom").val(subStr[0]);
                        $("#txtRangeTo").val(subStr[1]);
                        $("#txtTaxRateValue").val(argus[8]);
                        $("#ddlTaxRateType").val(argus[9]);
                        TaxRate.HideAll();
                        $("#divTaxRateInformation").show();
                        $("#chkIsTaxZipRange").prop('checked', $.parseJSON(argus[7].toLowerCase()));
                        if ($("#chkIsTaxZipRange").is(':checked')) {
                            $("#trZipPostCode").hide();
                            $("#trRangeFrom").show();
                            $("#trRangeTo").show();
                            $("#txtZipPostCode").val('');
                        } else {
                            $("#trRangeFrom").hide();
                            $("#trRangeTo").hide();
                            $("#trZipPostCode").show();
                            $("#txtRangeFrom").val('');
                            $("#txtRangeTo").val('');
                        }
                        break;
                    default:
                        break;
                }
            },
            DeleteTaxRate: function (tblID, argus) {
                switch (tblID) {
                    case "gdvTaxRateDetails":
                        var properties = {
                            onComplete: function (e) {
                                TaxRate.DeleteTaxRateByID(argus[0], e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete this tax rate?') + "</p>", properties);
                        break;
                    default:
                        break;
                }
            },
            ConfirmDeleteTaxRates: function (Ids, event) {
                TaxRate.DeleteTaxRateByID(Ids, event);
            },
            DeleteTaxRateByID: function (_taxRate_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxRates";
                    this.config.data = JSON2.stringify({ taxRateIDs: _taxRate_Ids, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 3;
                    this.ajaxCall(this.config);
                }
                return false;
            },
            GetCountryList: function () {
                this.config.url = this.config.baseURL + "BindCountryList";
                this.config.data = '{}';
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            GetStateList: function (countryCode) {
                this.config.url = this.config.baseURL + "BindStateList";
                this.config.data = JSON2.stringify({ countryCode: countryCode });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            SaveAndUpdateTaxRate: function () {
                var TaxRateId = $("#hdnTaxRateID").val();
                taxRateFlag = TaxRateId;
                var TaxRateTitle = $.trim($("#txtTaxRateTitle").val());
                if (TaxRateTitle != "") {
                    var TaxCountryCode = '';
                    if ($("#ddlCountry option:selected").val() != "0") {
                        TaxCountryCode = $("#ddlCountry option:selected").val();
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please select country!') + "</p>");
                        return false;
                    }
                    $.ajax({
                        type: "POST",
                        url: TaxRate.config.baseURL + "BindStateList",
                        data: JSON2.stringify({ countryCode: $("#ddlCountry option:selected").val() }),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var TaxStateCode = '';
                            if (msg.d.length > 2) {
                                if ($("#ddlState").val() != 0) {
                                    TaxStateCode = $("#ddlState option:selected").text();
                                } else {
                                    TaxStateCode = '';

                                }
                            } else {
                                if ($("#txtState").val() != "") {
                                    TaxStateCode = $.trim($("#txtState").val());
                                } else {
                                    TaxStateCode = '';
                                }
                            }
                            var IsTaxZipRange = $("#chkIsTaxZipRange").prop('checked');
                            var zipPostRange = '';
                            if ($("#chkIsTaxZipRange").is(':checked')) {
                                zipPostRange = $.trim($("#txtRangeFrom").val()) + '-' + $.trim($("#txtRangeTo").val());
                            } else {
                                zipPostRange = $.trim($("#txtZipPostCode").val());
                            }
                            var TaxRateValue = $.trim($("#txtTaxRateValue").val());
                            var RateType = $("#ddlTaxRateType").val();
                            TaxRate.config.url = TaxRate.config.baseURL + "SaveAndUpdateTaxRates";
                            var taxRateDataObj = {
                                TaxRateID: TaxRateId,
                                TaxRateTitle: TaxRateTitle,
                                Country: TaxCountryCode,
                                State: TaxStateCode,
                                Zip: zipPostRange,
                                IsZipPostRange: IsTaxZipRange,
                                TaxRateValue: TaxRateValue,
                                RateType: RateType
                            };
                            var aspxTempCommonObj = aspxCommonObj;
                            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                            TaxRate.config.data = JSON2.stringify({ taxRateDataObj: taxRateDataObj, aspxCommonObj: aspxTempCommonObj });
                            TaxRate.config.ajaxCallMode = 4;
                            TaxRate.ajaxCall(TaxRate.config);
                        }
                    });
                } else {
                    csscody.alert("<h2>" + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rate title can not be empty!') + "</p>");
                    return false;
                }
            },
            ajaxSuccess: function (data) {
                switch (TaxRate.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        $.each(data.d, function (index, item) {
                            $("#ddlCountry").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                            $("#ddlSearchCountry").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 2:
                        $("#ddlState").html('');
                        $.each(data.d, function (index, item) {
                            if (item.Text != 'NotExists') {
                                $("#ddlState").show();
                                $("#txtState").hide();
                                $("#ddlState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                            } else {
                                $("#ddlState").hide();
                                $("#txtState").show();
                            }
                            $("#ddlSearchState").append("<option value" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 3:
                        csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rate has been deleted successfully.') + '</p>');
                        TaxRate.BindTaxRates(null, null, null, null);
                        break;
                    case 4:
                        if (taxRateFlag > 0) {
                            csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rate has been updated successfully.') + '</p>');
                        } else {
                            csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rate has been saved successfully.') + '</p>');
                        }
                        TaxRate.BindTaxRates(null, null, null, null);
                        TaxRate.HideAll();
                        $("#divTaxRatesGrid").show();
                        break;
                }
            },
            ajaxFailure: function () {
                switch (TaxRate.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to delete tax rate!') + '</p>');
                        break;
                    case 4:
                        csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to save tax rate!') + '</p>');
                        break;
                }
            },
            SearchTaxRate: function () {
                var taxName = $.trim($("#txtRateTitle").val());
                var country = '';
                var state = $.trim($("#txtSearchState").val());
                var zipPostCode = $.trim($("#txtSearchZip").val());
                if (taxName.length < 1) {
                    taxName = null;
                }
                if ($("#ddlSearchCountry").val() != "0") {
                    country = $.trim($("#ddlSearchCountry option:selected").val());
                } else {
                    country = null;
                }
                if (state.length < 1) {
                    state = null;
                }
                if (zipPostCode.length < 1) {
                    zipPostCode = null;
                }
                TaxRate.BindTaxRates(taxName, country, state, zipPostCode);
            }
        };
        TaxRate.init();
    });