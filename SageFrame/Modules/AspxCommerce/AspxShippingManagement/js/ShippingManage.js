var ShippingManage = "";
$(function () {
    var progressTime = null;
    var progress = 0;
    var pcount = 0;
    var percentageInterval = [10, 20, 30, 40, 60, 80, 100];
    var timeInterval = [1, 2, 4, 2, 1, 5, 1];
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var editFlag = 0;
    var shippingMethodID = 0;
    var j = ''; var k = ''; var i = '';
    ShippingManage = {
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
        ajaxCall: function (config) {
            $.ajax({
                type: ShippingManage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ShippingManage.config.contentType,
                cache: ShippingManage.config.cache,
                async: ShippingManage.config.async,
                data: ShippingManage.config.data,
                dataType: ShippingManage.config.dataType,
                url: ShippingManage.config.url,
                success: ShippingManage.ajaxSuccess,
                error: ShippingManage.ajaxFailure
            });
        },
        ForceNumericInput: function (defaultQuantityInGroup) {
            $("#txtWeightFrom,#txtWeightTo").keydown(function (e) {
                               if ($.inArray(e.keyCode, [8, 9, 27, 13, 110, 190, 46]) !== -1 ||
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
        init: function () {
            $("#btnReset").click(function () {
                ShippingManage.ResetForm();
            });
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

            });
            $("#lblCostDedendencies").text("More Than" + " " + curSymbol);
            $('#ddlCostDependencies').html("<option selected=\"selected\" value=\"0\">" + getLocale(AspxShippingManagement, "Absolute") + " (" + curSymbol + ")</option><option value=\"1\">Percent (%)</option>");
            $('#ddlWeightDependencies').html("<option selected=\"selected\" value=\"0\">" + getLocale(AspxShippingManagement, "Absolute") + " (" + curSymbol + ")</option><option value=\"1\">Percent (%)</option>");
            $('#ddlItemDependencies').html("<option selected=\"selected\" value=\"0\">" + getLocale(AspxShippingManagement, "Absolute") + " (" + curSymbol + ")</option><option value=\"1\">Percent (%)</option>");

            $("#txtCost").DigitAndDecimal('.cssClassCost', '#errmsgCost');
            $("#txtCostRateValue").DigitAndDecimal('.cssClassCostRateValue', '#errmsgRateValue');
            $("#txtWeight").DigitAndDecimal('.cssClassWeight', '#errmsgWeight');
            $("#txtWeightRateValue").DigitAndDecimal('.cssClassWeightRateValue', '#errmsgWeightRateValue');
            $("#txtQuantity").DigitOnly('.cssClassQuantity', '#errmsgQty');
            $("#txtQuantityRateValue").DigitAndDecimal('.cssClassQuantityRateValue', '#errmsgQtyRateValue');
            $("#txtDisplayOrder").DigitOnly('.displayOrder', '#errdisplayOrder');
            $("#txtDisplayOrder").focusout(function () {
                $("#erruniqueOrder").show();
                if ($("#txtDisplayOrder").val() == 0) {
                    $("#txtDisplayOrder").val('');
                }
                if ($("#txtDisplayOrder").val() != '')
                    ShippingManage.CheckUniqueness($("#txtDisplayOrder").val(), shippingMethodID);
            });
            $("#txtWeightLimitFrom").DigitAndDecimal('.weightFrom', '#lblNotificationlf');
            $("#txtWeightLimitTo").DigitAndDecimal('.weightTo', '#lblNotificationlt');

            $('#txtWeightLimitFrom').change(function () {
                if (eval($("#txtWeightLimitFrom").val()) >= eval($("#txtWeightLimitTo").val())) {
                    $('#lblNotificationlf').html(getLocale(AspxShippingManagement, 'Weight Limit From must be less than Weight Limit To.'));
                    $("#txtWeightLimitFrom").val('');
                } else {
                    $('#lblNotificationlf').html('');
                }
                return false;
            });
            $('#txtMethodName,#txtSearchDeliveryTime,#txtWeightFrom,#txtWeightTo,#ddlIsActive').keyup(function (event) {
                if (event.keyCode == 13) {
                    ShippingManage.SearchShippingMethods();
                }
            });

            $('#txtWeightLimitTo').change(function () {
                if (eval($("#txtWeightLimitTo").val()) <= eval($("#txtWeightLimitFrom").val())) {
                    $('#lblNotificationlt').html(getLocale(AspxShippingManagement, 'Weight Limit To must be greater than Weight Limit From.'));
                    $("#txtWeightLimitTo").val('');
                } else {
                    $('#lblNotificationlt').html('');
                }
                return false;
            });
            $.validator.addMethod('selectNone',
                function (value, element) {
                    return this.optional(element) ||
                        (value.indexOf("0") == -1);
                }, getLocale(AspxShippingManagement, "Please select an option"));
           var validator= $("#form1").validate({
                rules: {
                    providerList: {
                        selectNone: true
                    }
                },
                messages: {
                    name: {
                        required: '*',
                        minlength: getLocale(AspxShippingManagement, "* (at least 2 chars)")
                    },
                    displayOrder: {
                        required: '*',
                        minlength: getLocale(AspxShippingManagement, "* (at least 1 chars)"),
                        digits: '*'
                    },
                    deliveryTime: {
                        required: '*',
                        minlength: getLocale(AspxShippingManagement, "* (at least 2 chars)")
                    },
                    weightFrom: {
                        required: '*',
                        minlength: getLocale(AspxShippingManagement, "* (at least 1 chars)"),
                        maxlength: getLocale(AspxShippingManagement, "* (no more than 5 chars)"),
                        number: '*'
                    },
                    weightTo: {
                        required: '*',
                        minlength: getLocale(AspxShippingManagement, "* (at least 1 chars)"),
                        maxlength: getLocale(AspxShippingManagement, "* (no more than 5 chars)"),
                        number: '*'
                    },
                    providerList: {
                        required: '*'
                    }
                },
                submitHandler: function (form) {
                    ShippingManage.SaveAndUpdateShippingMethod();
                }
            });
            ShippingManage.LoadShippingMgmtStaticImage();
            ShippingManage.BindShippingMethodGrid(null, null, null, null, null);
            ShippingManage.BindShippingProviderList();
            ShippingManage.ImageUploader();
            ShippingManage.HideAll();
            ShippingManage.ForceNumericInput();
            $("#divShowShippingMethodGrid").show();

            $('#btnDeleteSelected').click(function () {
                var shippingMethos_Ids = '';
                               shippingMethos_Ids = SageData.Get("gdvShippingMethod").Arr.join(',');
                if (shippingMethos_Ids.length>0) {
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.ConfirmDeleteMultipleShippings(shippingMethos_Ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete selected shipping method(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please select at least one shipping method before delete.") + '</p>');
                }
            });

            $("#btnAddNewShippingMethod").click(function () {
                ShippingManage.ClearForm();
                ShippingManage.HideAll();
                $("#divAddNewShippingMethodForm").show();
                $("#liShippingSettingChanges").hide();
                $("#btnReset").show();
                shippingMethodID = 0;
                $("#erruniqueOrder").hide();
            });

            $("#btnCancel").click(function () {
                ShippingManage.ClearForm();
                ShippingManage.HideAll();
                $("#divShowShippingMethodGrid").show();
                validator.resetForm();
            });

            $(".cssClassClose").click(function () {
                $("#tblcostdependencies tr:gt(0)").next().remove();
                $("#tblWeightDependencies tr:gt(0)").next().remove();
                $("#tblItemDependencies tr:gt(0)").next().remove();
                RemovePopUp();
                validator.resetForm();
            });

            $("#btnAddCostDependencies").click(function () {
                ShippingManage.HideTables();
                $("#lblTitleDependencies").html(getLocale(AspxShippingManagement, 'Add Cost Dependencies'));
                $("#tblcostdependencies").show();
                $("#CostDependencyButtonWrapper").show();
                ShippingManage.ClearAddDependencies();
                $("#tblcostdependencies tr:gt(0)").next().remove();
                ShowPopup(this);
            });

            $("#tblcostdependencies").find("img.cssClassDeleteRow").hide();

            $("img.cssClassAddRow").click(function () {
                i = $("#tblcostdependencies>tbody>tr").length;
                var cloneRow = $(this).closest('tr').clone(true);
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    $(cloneRow).appendTo("#tblcostdependencies");
                    if (i == 1) {
                        $(cloneRow).find('.cssClassCost').removeClass('cssClassCost').addClass('cssClassCost' + i);
                        $(cloneRow).find('.cssClassCostRateValue').removeClass('cssClassCostRateValue').addClass('cssClassCostRateValue' + i);
                        $(cloneRow).find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + i);
                    } else {
                        var x = i;
                        var y = i - 1;
                        $(cloneRow).find('.cssClassCost' + y).removeClass('cssClassCost' + y).addClass('cssClassCost' + x);
                        $(cloneRow).find('.cssClassCostRateValue' + y).removeClass('cssClassCostRateValue' + y).addClass('cssClassCostRateValue' + x);
                        $(cloneRow).find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x);
                    }
                    $(cloneRow).find(".cssClassDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblcostdependencies");
                    $(cloneRow).find("input[type='text']").val('');
                    $(cloneRow).find(".cssClassDeleteRow").show();
                }
            });

            $("img.cssClassCloneRow").click(function () {
                var cloneRow = $(this).closest('tr').clone(true);
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    i = $("#tblcostdependencies>tbody>tr").length;
                    var costValue = '';
                    var rateValue = '';
                    if (i == 1) {
                        costValue = $(this).closest('tr').find('.cssClassCost').val();
                        rateValue = $(this).closest('tr').find('.cssClassCostRateValue').val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies').val();
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassCost').removeClass('cssClassCost').addClass('cssClassCost' + i).val(costValue);
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassCostRateValue').removeClass('cssClassCostRateValue').addClass('cssClassCostRateValue' + i).val(rateValue);
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + i).val(ddl);

                    } else {
                        var x = i;
                        var y = i - 1;
                        costValue = $(this).closest('tr').find('.cssClassCost' + y).val();
                        rateValue = $(this).closest('tr').find('.cssClassCostRateValue' + y).val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies' + y).val();
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassCost' + y).removeClass('cssClassCost' + y).addClass('cssClassCost' + x).val(costValue);
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassCostRateValue' + y).removeClass('cssClassCostRateValue' + y).addClass('cssClassCostRateValue' + x).val(rateValue);
                        $(cloneRow).appendTo("#tblcostdependencies").find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x).val(ddl);
                    }
                    $(cloneRow).find(".cssClassDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblcostdependencies");
                    $(cloneRow).find(".cssClassDeleteRow").show();
                }
            });

            $("img.cssClassDeleteRow").click(function () {
                var parentRow = $(this).closest('tr');
                if (parentRow.is(":first-child")) {
                    return false;
                } else {
                    $(parentRow).remove();
                }
            });

            $("#btnCreateCost").click(function () {
                var costInput = eval($('#tblcostdependencies input[type="text"]'));
                var count = 0;
                $.each(costInput, function (index, item) {
                    if ($(this).val() <= '') {
                        csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Please enter cost and rate.") + '</p>');
                        count = 1;
                        return false;
                    }
                });
                if (count == 0)
                    ShippingManage.SaveCostDependenciesValues();
            });

            $("#btnCancelCostDependencies").click(function () {
                $("#tblcostdependencies tr:gt(0)").next().remove();
                RemovePopUp();
                return false;
            });

            $('#btnDeleteCostDependencies').click(function () {
                var shippingProductCost_Ids = '';
                $(".CostChkbox").each(function (i) {
                    if ($(this).prop("checked")) {
                        shippingProductCost_Ids += $(this).val() + ',';
                    }
                });
                if (shippingProductCost_Ids != "") {
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.ConfirmDeleteMultipleShippingCostDependencies(shippingProductCost_Ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete the selected cost dependencies?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please select at least one cost dependencies before delete.") + '</p>');
                }
            });

            $("#btnAddWeightDependencies").click(function () {
                ShippingManage.HideTables();
                $("#lblTitleDependencies").html(getLocale(AspxShippingManagement, "Add Weight Dependencies"));
                $("#tblWeightDependencies").show();
                $("#WeightDependencyButtonWrapper").show();
                ShippingManage.ClearAddDependencies();
                $("#tblWeightDependencies tr:gt(0)").next().remove();
                ShowPopup(this);
            });

            $("#tblWeightDependencies").find("img.cssClassWeightDeleteRow").hide();

            $("img.cssClassWeightAddRow").click(function () {
                var cloneRow = $(this).closest('tr').clone(true);
                j = $("#tblWeightDependencies>tbody>tr").length;
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    $(cloneRow).appendTo("#tblWeightDependencies");
                    if (j == 1) {
                        $(cloneRow).find('.cssClassWeight').removeClass('cssClassWeight').addClass('cssClassWeight' + j);
                        $(cloneRow).find('.cssClassWeightRateValue').removeClass('cssClassWeightRateValue').addClass('cssClassWeightRateValue' + j);
                        $(cloneRow).find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + j);
                        $(cloneRow).find('.cssClassWeightIsActive').removeClass('cssClassWeightIsActive').addClass('cssClassWeightIsActive' + j);
                    } else {
                        var x = j;
                        var y = j - 1;
                        $(cloneRow).find('.cssClassWeight' + y).removeClass('cssClassWeight' + y).addClass('cssClassWeight' + x);
                        $(cloneRow).find('.cssClassWeightRateValue' + y).removeClass('cssClassWeightRateValue' + y).addClass('cssClassWeightRateValue' + x);
                        $(cloneRow).find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x);
                        $(cloneRow).find('.cssClassWeightIsActive' + y).removeClass('cssClassWeightIsActive' + y).addClass('cssClassWeightIsActive' + x);
                    }
                    $(cloneRow).find(".cssClassWeightDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblWeightDependencies");
                    $(cloneRow).find("input[type='text']").val('');
                    $(cloneRow).find("#ddlWeightDependencies").val(0);
                    $(cloneRow).find("#chkPerLbs").removeAttr('checked');
                    $(cloneRow).find(".cssClassWeightDeleteRow").show();
                }
            });

            $("img.cssClassWeightCloneRow").click(function () {
                var cloneRow = $(this).closest('tr').clone(true);
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    j = $("#tblWeightDependencies>tbody>tr").length;
                    var weightValue = '';
                    var rateValue = '';
                    var ddl = '';
                    var isactive = '';
                    if (j == 1) {
                        weightValue = $(this).closest('tr').find('.cssClassWeight').val();
                        rateValue = $(this).closest('tr').find('.cssClassWeightRateValue').val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies').val();
                        isactive = $(this).closest('tr').find('.cssClassWeightIsActive').prop("checked");
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeight').removeClass('cssClassWeight').addClass('cssClassWeight' + j).val(weightValue);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeightRateValue').removeClass('cssClassWeightRateValue').addClass('cssClassWeightRateValue' + j).val(rateValue);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + j).val(ddl);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeightIsActive').removeClass('cssClassWeightIsActive').addClass('cssClassWeightIsActive' + j).prop("checked", isactive);
                    } else {
                        var x = j;
                        var y = j - 1;
                        weightValue = $(this).closest('tr').find('.cssClassWeight' + y).val();
                        rateValue = $(this).closest('tr').find('.cssClassWeightRateValue' + y).val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies' + y).val();
                        isactive = $(this).closest('tr').find('.cssClassWeightIsActive' + y).prop("checked");
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeight' + y).removeClass('cssClassWeight' + y).addClass('cssClassWeight' + x).val(weightValue);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeightRateValue' + y).removeClass('cssClassWeightRateValue' + y).addClass('cssClassWeightRateValue' + x).val(rateValue);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x).val(ddl);
                        $(cloneRow).appendTo("#tblWeightDependencies").find('.cssClassWeightIsActive' + y).removeClass('cssClassWeightIsActive' + y).addClass('cssClassWeightIsActive' + x).prop("checked", isactive);
                    }
                    $(cloneRow).find(".cssClassWeightDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblWeightDependencies");
                    $(cloneRow).find(".cssClassWeightDeleteRow").show();
                }
            });

            $("img.cssClassWeightDeleteRow").click(function () {
                var parentRow = $(this).closest('tr');
                if (parentRow.is(":first-child")) {
                    return false;
                } else {
                    $(parentRow).remove();
                }
            });

            $("#btnCreateWeight").click(function () {
                var weightInput = eval($('#tblWeightDependencies input[type="text"]'));
                var count = 0;
                $.each(weightInput, function (index, item) {
                    if ($(this).val() <= '') {
                        csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Please enter weight and rate.") + '</p>');
                        count = 1;
                        return false;
                    }
                });
                if (count == 0)
                    ShippingManage.SaveWeightDependenciesValues();
            });

            $("#btnCancelWeightDependencies").click(function () {
                $("#tblWeightDependencies tr:gt(0)").next().remove();
                RemovePopUp();
                return false;
            });

            $('#btnDeleteWeightDependencies').click(function () {
                var shippingProductWeight_Ids = '';
                $(".WeightChkbox").each(function (i) {
                    if ($(this).prop("checked")) {
                        shippingProductWeight_Ids += $(this).val() + ',';
                    }
                });
                if (shippingProductWeight_Ids != "") {
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.ConfirmDeleteMultipleShippingWeightDependencies(shippingProductWeight_Ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete the selected weight dependencies?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Please select at least one weight dependencies before delete.") + '</p>');
                }
            });

            $("#btnAddItemDependencies").click(function () {
                ShippingManage.HideTables();
                $("#lblTitleDependencies").html(getLocale(AspxShippingManagement, 'Add Item Dependencies'));
                $("#tblItemDependencies").show();
                $("#ItemDependencyButtonWrapper").show();
                ShippingManage.ClearAddDependencies();
                $("#tblItemDependencies tr:gt(0)").next().remove();
                ShowPopup(this);
            });

            $("#tblItemDependencies").find("img.cssClassItemDeleteRow").hide();

            $("img.cssClassItemAddRow").click(function () {
                var cloneRow = $(this).closest('tr').clone(true);
                k = $("#tblItemDependencies>tbody>tr").length;
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    $(cloneRow).appendTo("#tblItemDependencies");
                    if (k == 1) {
                        $(cloneRow).find('.cssClassQuantity').removeClass('cssClassQuantity').addClass('cssClassQuantity' + k);
                        $(cloneRow).find('.cssClassQuantityRateValue').removeClass('cssClassQuantityRateValue').addClass('cssClassQuantityRateValue' + k);
                        $(cloneRow).find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + k);
                        $(cloneRow).find('.cssClassItemIsActive').removeClass('cssClassItemIsActive').addClass('cssClassItemIsActive' + k);
                    } else {
                        var x = k;
                        var y = k - 1;
                        $(cloneRow).find('.cssClassQuantity' + y).removeClass('cssClassQuantity' + y).addClass('cssClassQuantity' + x);
                        $(cloneRow).find('.cssClassQuantityRateValue' + y).removeClass('cssClassQuantityRateValue' + y).addClass('cssClassQuantityRateValue' + x);
                        $(cloneRow).find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x);
                        $(cloneRow).find('.cssClassItemIsActive' + y).removeClass('cssClassItemIsActive' + y).addClass('cssClassItemIsActive' + x);
                    }
                    $(cloneRow).find(".cssClassItemDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblItemDependencies");
                    $(cloneRow).find("input[type='text']").val('');
                    $(cloneRow).find("#ddlItemDependencies").val(0);
                    $(cloneRow).find("#chkPerItems").removeAttr('checked');
                    $(cloneRow).find(".cssClassItemDeleteRow").show();
                }
            });

            $("img.cssClassItemCloneRow").click(function () {
                var cloneRow = $(this).closest('tr').clone(true);
                if (($.browser.msie) && ($.browser.version == '9.0')) {
                    k = $("#tblItemDependencies>tbody>tr").length;
                    var quantityValue = '';
                    var rateValue = '';
                    var ddl = '';
                    var isactive = '';
                    if (k == 1) {
                        quantityValue = $(this).closest('tr').find('.cssClassQuantity').val();
                        rateValue = $(this).closest('tr').find('.cssClassQuantityRateValue').val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies').val();
                        isactive = $(this).closest('tr').find('.cssClassItemIsActive').prop("checked");
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassQuantity').removeClass('cssClassQuantity').addClass('cssClassQuantity' + k).val(quantityValue);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassQuantityRateValue').removeClass('cssClassQuantityRateValue').addClass('cssClassQuantityRateValue' + k).val(rateValue);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassDropDownCostDependencies').removeClass('cssClassDropDownCostDependencies').addClass('cssClassDropDownCostDependencies' + i).val(ddl);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassItemIsActive').removeClass('cssClassItemIsActive').addClass('cssClassItemIsActive' + i).prop("checked", isactive);
                    } else {
                        var x = k;
                        var y = k - 1;
                        quantityValue = $(this).closest('tr').find('.cssClassQuantity' + y).val();
                        rateValue = $(this).closest('tr').find('.cssClassQuantityRateValue' + y).val();
                        ddl = $(this).closest('tr').find('.cssClassDropDownCostDependencies' + y).val();
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassQuantity' + y).removeClass('cssClassQuantity' + y).addClass('cssClassQuantity' + x).val(quantityValue);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassQuantityRateValue' + y).removeClass('cssClassQuantityRateValue' + y).addClass('cssClassQuantityRateValue' + x).val(rateValue);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassDropDownCostDependencies' + y).removeClass('cssClassDropDownCostDependencies' + y).addClass('cssClassDropDownCostDependencies' + x).val(ddl);
                        $(cloneRow).appendTo("#tblItemDependencies").find('.cssClassItemIsActive' + y).removeClass('cssClassItemIsActive' + y).addClass('cssClassItemIsActive' + x).prop("checked", isactive);
                    }
                    $(cloneRow).find(".cssClassItemDeleteRow").show();
                } else {
                    $(cloneRow).appendTo("#tblItemDependencies");
                    $(cloneRow).find(".cssClassItemDeleteRow").show();
                }
            });

            $("img.cssClassItemDeleteRow").click(function () {
                var parentRow = $(this).closest('tr');
                if (parentRow.is(":first-child")) {
                    return false;
                } else {
                    $(parentRow).remove();
                }
            });

            $('#btnDeleteItemDependencies').click(function () {
                var shippimgItem_Ids = '';
                $(".ItemChkbox").each(function (i) {
                    if ($(this).prop("checked")) {
                        shippimgItem_Ids += $(this).val() + ',';
                    }
                });
                if (shippimgItem_Ids != "") {
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.ConfirmDeleteMultipleShippingItemDependencies(shippimgItem_Ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete the selected item depencencies?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Please select at least one item dependencies before delete.") + '</p>');
                }
            });

            $("#btnCreateItem").click(function () {
                var itemInput = $('#tblItemDependencies input[type="text"]');
                var count = 0;
                $.each(itemInput, function (index, item) {
                    if ($(this).val() <= '') {
                        csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Please enter quantity and rate.") + '</p>');
                        count = 1;
                        return false;
                    }
                });
                if (count == 0)
                    ShippingManage.SaveItemDependenciesValues();
            });

            $("#btnCancelItemDependencies").click(function () {
                $("#tblItemDependencies tr:gt(0)").next().remove();
                RemovePopUp();
                return false;
            });
        },
        LoadShippingMgmtStaticImage: function () {

            $('.cssClassAddRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
            $('.cssClassCloneRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
            $('.cssClassDeleteRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');

            $('.cssClassItemAddRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
            $('.cssClassItemCloneRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
            $('.cssClassItemDeleteRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');

            $('.cssClassWeightAddRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
            $('.cssClassWeightCloneRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
            $('.cssClassWeightDeleteRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');

            $('#ajaxShippingMgmtImage1').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxShippingMgmtImage2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxShippingMgmtImage3').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxShippingMgmtImage4').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        ResetForm: function () {
                       $('#txtShippingMethodName').val('');
            $('#txtDeliveryTime').val('');
            $('#txtAlternateText').val('');
            $('#txtWeightLimitFrom').val('');
            $('#txtWeightLimitTo').val('');
            $('#txtDisplayOrder').val('');

            $('#txtShippingMethodName').removeClass('error');
            $('#txtShippingMethodName').parents('td').find('label').remove();
            $('#txtDeliveryTime').removeClass('error');
            $('#txtDeliveryTime').parents('td').find('label').remove();
            $('#txtAlternateText').removeClass('error');
            $('#txtAlternateText').parents('td').find('label').remove();
            $('#txtWeightLimitFrom').removeClass('error');
            $('#txtWeightLimitFrom').parents('td').find('label').remove();
            $('#txtWeightLimitTo').removeClass('error');
            $('#txtWeightLimitTo').parents('td').find('label').remove();
            $('#txtDisplayOrder').removeClass('error');
            $('#txtDisplayOrder').parents('td').find('label').remove();
            $("#ddlShippingService").val(0);
            $('#chkIsActive').removeAttr('checked');
            $("#shippingIcon").html('');
        },

        ImageUploader: function () {
            var upload = new AjaxUpload($('#fileUpload'), {
                action: aspxShippingModulePath + "MultipleFileUploadHandler.aspx",
                name: 'myfile[]',
                multiple: false,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                                   },
                onSubmit: function (file, ext) {
                                       pcount = 0;
                    var percentage = $('.progress').find('.percentage');
                    var progressBar = $('.progress').find('.progressBar');
                    $('.progress').show();
                    ShippingManage.dummyProgress(progressBar, percentage);
                   
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Sorry! it is not a valid image type.") + '</p>');
                            return false;
                        }
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Sorry! it is not a valid image type") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null && res.Status > 0) {
                                               ShippingManage.AddNewImages(res);
                        return false;
                    } else {
                        csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + '</h2><p>' + res.Message + '</p>');
                        return false;
                    }
                }
            });
        },
        dummyProgress: function (progressBar, percentage) {
            if (percentageInterval[pcount]) {
                progress = percentageInterval[pcount] + Math.floor(Math.random() * 10 + 1);
                percentage.text(progress.toString() + '%');
                progressBar.progressbar({
                    value: progress
                });
                var percent = percentage.text();
                percent = percent.replace('%', '');
                if (percent == 100 || percent > 100) {
                    percentage.text('100%');
                                       $('.progress').hide();
                }
            }

            if (timeInterval[pcount]) {
                progressTime = setTimeout(function () {
                    ShippingManage.dummyProgress(progressBar, percentage)
                }, timeInterval[pcount] * 10);
            }
            pcount++;
        },

        AddNewImages: function (response) {
            if (response.Message != null && response.Message != "") {
                $("#shippingIcon").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
                $("#shippingIcon").append('<img src="' + aspxRootPath + 'Administrator/Templates/Default/images/delete.png' + '" id="imgDelete" alt="Delete" title="Delete" />');
                $("#imgDelete").off().click(function () {
                    $("#shippingIcon").html('');
                    $("#hdnPrevFilePath").val('');
                });
            }
        },

        SelectFirstTab: function () {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
        },
        BindShippingProviderList: function () {
            this.config.url = this.config.baseURL + "GetShippingProviderList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindShippingMethodGrid: function (shippingMethodNm, deliveryTime, weightFrom, weightTo, isAct) {
            var shippingMethodObj = {
                ShippingMethodName: shippingMethodNm,
                DeliveryTime: deliveryTime,
                WeightLimitFrom: weightFrom,
                WeightLimitTo: weightTo,
                IsActive: isAct
            };
            this.config.method = "GetShippingMethodList";
            this.config.data = { shippingMethodObj: shippingMethodObj, aspxCommonObj: aspxCommonObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShippingMethod_pagesize").length > 0) ? $("#gdvShippingMethod_pagesize :selected").text() : 10;

            $("#gdvShippingMethod").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShippingManagement, 'ShippingMethodID'), name: 'shippingmethod_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'ShippingChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxShippingManagement, 'Shipping Method Name'), name: 'shipping_method_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxShippingManagement, 'Shipping PrividerID'), name: 'shipping_providerId', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Image Path'), name: 'image_path', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Alternate Text'), name: 'alternate_text', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Display Order'), name: 'display_order', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Delivery Time'), name: 'delivery_time', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Weight Limit From'), name: 'weight_limit_from', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Weight Limit To'), name: 'weight_limit_to', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Active'), name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxShippingManagement, 'Added By'), name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Shipping Cost'), name: 'ShippingCost', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'IsRealTime'), name: 'IsRealTime', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'true/false', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxShippingManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ShippingManage.EditShippingMethod', arguments: '1,2,3,4,5,6,7,8,9,10,11,13' },
                    { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ShippingManage.DeleteShippingMethod', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false } }
            });
        },
        SaveAndUpdateShippingMethod: function () {
            var prevFilePath = $("#hdnPrevFilePath").val();
            var newFilePath = null;
            if ($("#shippingIcon>img").length > 0) {
                newFilePath = $("#shippingIcon>img:eq(0)").attr("src").replace(aspxRootPath, "");
            }
            var AlternateText = $.trim($("#txtAlternateText").val());

            var shippingMethodId = $("#hdnShippingMethodID").val();
            editFlag = shippingMethodId;
            var ShippingMethodName = $.trim($("#txtShippingMethodName").val());

            var DisplayOrder = $.trim($("#txtDisplayOrder").val());
            var DeliveryTime = $.trim($("#txtDeliveryTime").val());
            var WeightLimitFrom = $.trim($("#txtWeightLimitFrom").val());
            var WeightLimitTo = $.trim($("#txtWeightLimitTo").val());
            var shippingService = $("#ddlShippingService option:selected").val();
            var IsActive = $("#chkIsActive").is(':checked');
            if (shippingService != 0) {
                $("#ddlShippingService ").removeClass('error');
                $("#ddlShippingService").parent('td').find('.cssClassRequired').html('');
                var aspxTempCommonObj = aspxCommonObj;
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                this.config.url = this.config.baseURL + "SaveAndUpdateShippingMethods";
                this.config.data = JSON2.stringify({ shippingMethodID: shippingMethodId, shippingMethodName: ShippingMethodName, prevFilePath: prevFilePath, newFilePath: newFilePath, alternateText: AlternateText, displayOrder: DisplayOrder, deliveryTime: DeliveryTime, weightLimitFrom: WeightLimitFrom, weightLimitTo: WeightLimitTo, shippingProviderID: shippingService, isActive: IsActive, aspxCommonObj: aspxTempCommonObj });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            } else {
                $("#ddlShippingService ").addClass('error');
                $("#ddlShippingService").parent('td').find('.cssClassRequired').html(getLocale(AspxShippingManagement, 'you need to create shipping service provider before adding shipping method.'));
            }
        },
        CheckUniqueness: function (value, id) {
            this.config.url = this.config.baseURL + "CheckUniquenessForDisplayOrder";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, value: value, shippingMethodID: id });
            this.config.ajaxCallMode = 10;
            this.ajaxCall(this.config);
        },
        EditShippingMethod: function (tblID, argus) {
            switch (tblID) {
                case "gdvShippingMethod":
                    console.log('edit');
                    if (!Boolean.parse(argus[14])) {
                        ShippingManage.SelectFirstTab();
                        $("#ddlShippingService").val(argus[4]);
                        $("#hdnPrevFilePath").val(argus[5]);
                        $("#shippingIcon").html('');
                        if (argus[5] != null && argus[5] != "") {
                            $("#shippingIcon").html('<img src="' + aspxRootPath + argus[5] + '" class="uploadImage" height="90px" width="100px"/>');
                            $("#shippingIcon").append('<img src="' + aspxRootPath + 'Administrator/Templates/Default/images/delete.png' + '" id="imgDelete" alt="Delete" title="Delete" />');
                            $("#imgDelete").off().click(function () {
                                $("#shippingIcon").html('');
                                $("#hdnPrevFilePath").val('');
                            });
                        }
                        $("#txtAlternateText").val(argus[6]);
                        $("#hdnShippingMethodID").val(argus[0]);
                        $("#txtShippingMethodName").val(argus[3]);
                        $("#txtDisplayOrder").val(argus[7]);
                        $("#txtDeliveryTime").val(argus[8]);
                        $("#txtWeightLimitFrom").val(argus[9]);
                        $("#txtWeightLimitTo").val(argus[10]);
                        $("#chkIsActive").prop('checked', $.parseJSON(argus[11].toLowerCase()));
                        $("#lblHeading").html("Editing shipping method:" + argus[3]);
                        ShippingManage.HideAll();
                        $("#divAddNewShippingMethodForm").show();
                        $("#generalSettings").show();
                        $("#liShippingSettingChanges").show();
                                               $(".icon-refresh").hide();
                        shippingMethodID = argus[0];
                        $("#erruniqueOrder").hide();

                                               ShippingManage.BindShippingCostDependencies(argus[0]);
                        ShippingManage.BindShippingWeightDependencies(argus[0]);
                        ShippingManage.BindShippingItemDependencies(argus[0]);
                        $("#divAddNewShippingMethodForm h2").text(getLocale(AspxShippingManagement, "Edit Existing Shipping Method"));

                    } else {
                        csscody.alert("<h2>" + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Access Denied! This is Real Time Provider's method.") + "</p>");
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteShippingMethod: function (tblID, argus) {
            switch (tblID) {
                case "gdvShippingMethod":
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.DeleteShippingInfo(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this shipping method?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultipleShippings: function (Ids, event) {
            ShippingManage.DeleteShippingInfo(Ids, event);
        },

        DeleteShippingInfo: function (_shippingMethod_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteShippingByShippingMethodID";
                this.config.data = JSON2.stringify({ shippingMethodIds: _shippingMethod_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
            return false;
        },

        HideAll: function () {
            $("#divShowShippingMethodGrid").hide();
            $("#divAddNewShippingMethodForm").hide();
        },

        ClearForm: function () {
            $("#txtShippingMethodName").val('');
            $('#txtShippingMethodName').removeClass('error');
            $('#txtShippingMethodName').parents('td').find('label').remove();
            $("#txtDeliveryTime").val('');
            $('#txtDeliveryTime').removeClass('error');
            $('#txtDeliveryTime').parents('td').find('label').remove();
            $("#txtAlternateText").val('');
            $('#txtAlternateText').removeClass('error');
            $('#txtAlternateText').parents('td').find('label').remove();
            $("#txtWeightLimitFrom").val('');
            $('#txtWeightLimitFrom').removeClass('error');
            $('#txtWeightLimitFrom').parents('td').find('label').remove();
            $("#txtWeightLimitTo").val('');
            $('#txtWeightLimitTo').removeClass('error');
            $('#txtWeightLimitTo').parents('td').find('label').remove();
            $("#txtDisplayOrder").val('');
            $('#txtDisplayOrder').removeClass('error');
            $('#txtDisplayOrder').parents('td').find('label').remove();
            $("#ddlShippingService").val(0);
            $('#chkIsActive').removeAttr('checked');
            $("#fileUpload").val('');
            $("#shippingIcon").html('');
            $("#lblHeading").html(getLocale(AspxShippingManagement, 'Add New Shipping Method:'));
            $("#hdnShippingMethodID").val(0);
            $("#hdnPrevFilePath").val("");
            ShippingManage.SelectFirstTab();
        },

        ClearAddDependencies: function () {
            $("#txtCost").val('');
            $("#txtCostRateValue").val('');
            $("#ddlCostDependencies").val(0);
            $("#txtWeight").val('');
            $("#txtWeightRateValue").val('');
            $("#ddlWeightDependencies").val(0);
            $('#chkPerLbs').removeAttr('checked');
            $("#txtQuantity").val('');
            $("#txtQuantityRateValue").val('');
            $("#ddlItemDependencies").val(0);
            $('#chkPerItems').removeAttr('checked');
        },

        HideTables: function () {
            $("#tblcostdependencies").hide();
            $("#tblWeightDependencies").hide();
            $("#tblItemDependencies").hide();
            $("#CostDependencyButtonWrapper").hide();
            $("#WeightDependencyButtonWrapper").hide();
            $("#ItemDependencyButtonWrapper").hide();
        },

        BindShippingCostDependencies: function (methodId) {
            this.config.method = "GetCostDependenciesListInfo";
            this.config.data = { aspxCommonObj: aspxCommonObj, shippingMethodId: methodId };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAddCostDependencies_pagesize").length > 0) ? $("#gdvAddCostDependencies_pagesize :selected").text() : 10;
            var symbol = curSymbol;
            $("#gdvAddCostDependencies").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShippingManagement, 'ShippingProductCostID'), name: 'shippingproductcost_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CostChkbox', elemDefault: false, controlclass: 'costHeaderChkbox' },
                    { display: getLocale(AspxShippingManagement, 'Shipping Method ID'), name: 'shipping_method_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Product Cost') + ' (More than ' + curSymbol + ')', name: 'cost', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxShippingManagement, 'Rate Value'), name: 'rate_value', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Is Rate In Percentage'), name: 'is_price_in_percentage', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxShippingManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxShippingManagement, 'Added By'), name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ShippingManage.DeleteCostDependencies', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        DeleteCostDependencies: function (tblID, argus) {
            switch (tblID) {
                case "gdvAddCostDependencies":
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.DeleteShippingCostInfo(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this cost dependencies?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultipleShippingCostDependencies: function (Ids, event) {
            ShippingManage.DeleteShippingCostInfo(Ids, event);
        },

        DeleteShippingCostInfo: function (ShippingProductCost_Ids, event) {
            if (event) {
                var methodId = $("#hdnShippingMethodID").val();
                this.config.url = this.config.baseURL + "DeleteCostDependencies";
                this.config.data = JSON2.stringify({ shippingProductCostIds: ShippingProductCost_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            return false;
        },

        SaveCostDependenciesValues: function () {
            var methodId = $("#hdnShippingMethodID").val();
            var shippingProductCost_ID = 0;
            var _CostDependenciesOptions = '';
            $('#tblcostdependencies>tbody tr').each(function () {
                _CostDependenciesOptions += $(this).find("#txtCost").val() + ',';
                _CostDependenciesOptions += $(this).find("#txtCostRateValue").val() + ',';
                var selectedCostStatus = $(this).find('#ddlCostDependencies option:selected').val();
                _CostDependenciesOptions += selectedCostStatus + '#';
            });
            this.config.url = this.config.baseURL + "SaveCostDependencies";
            this.config.data = JSON2.stringify({ shippingProductCostID: shippingProductCost_ID, shippingMethodID: methodId, costDependenciesOptions: _CostDependenciesOptions, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        BindShippingWeightDependencies: function (methodId) {
            this.config.method = "GetWeightDependenciesListInfo";
            this.config.data = { aspxCommonObj: aspxCommonObj, shippingMethodId: methodId };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAddWeightDependencies_pagesize").length > 0) ? $("#gdvAddWeightDependencies_pagesize :selected").text() : 10;

            $("#gdvAddWeightDependencies").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShippingManagement, 'ShippingProductWeightID'), name: 'shippingproductweight_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'WeightChkbox', elemDefault: false, controlclass: 'weightHeaderChkbox' },
                    { display: getLocale(AspxShippingManagement, 'Shipping Method ID'), name: 'shipping_method_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Product Weight (More than lbs)'), name: 'produst_weight', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxShippingManagement, 'Rate Value'), name: 'rate_value_from', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Is Rate In Percentage'), name: 'is_price_in_percentage', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Per Lbs'), name: 'Per_Lbs', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxShippingManagement, 'Added By'), name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ShippingManage.DeleteWeightDependencies', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false } }
            });

        },

        DeleteWeightDependencies: function (tblID, argus) {
            switch (tblID) {
                case "gdvAddWeightDependencies":
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.DeleteShippingWeightInfo(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this weight dependencies?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultipleShippingWeightDependencies: function (weight_Ids, event) {
            ShippingManage.DeleteShippingWeightInfo(weight_Ids, event);
        },

        DeleteShippingWeightInfo: function (_shippingProductWeight_Ids, event) {
            if (event) {
                var methodId = $("#hdnShippingMethodID").val();
                this.config.url = this.config.baseURL + "DeleteWeightDependencies";
                this.config.data = JSON2.stringify({ shippingProductWeightIds: _shippingProductWeight_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            }
            return false;
        },

        SaveWeightDependenciesValues: function () {
            var methodId = $("#hdnShippingMethodID").val();
            var shippingProductWeight_ID = 0;
            var _WeightDependenciesOptions = '';
            $('#tblWeightDependencies>tbody tr').each(function () {
                _WeightDependenciesOptions += $(this).find("#txtWeight").val() + ',';
                _WeightDependenciesOptions += $(this).find("#txtWeightRateValue").val() + ',';
                var selectedWeightStatus = $(this).find('#ddlWeightDependencies option:selected').val();
                _WeightDependenciesOptions += selectedWeightStatus + ',';
                _WeightDependenciesOptions += $(this).find("#chkPerLbs").prop('checked') + '#';
            });
            this.config.url = this.config.baseURL + "SaveWeightDependencies";
            this.config.data = JSON2.stringify({ shippingProductWeightID: shippingProductWeight_ID, shippingMethodID: methodId, weightDependenciesOptions: _WeightDependenciesOptions, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },

        BindShippingItemDependencies: function (methodId) {
            this.config.method = "GetItemDependenciesListInfo";
            this.config.data = { aspxCommonObj: aspxCommonObj, shippingMethodId: methodId };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAddItemDependencies_pagesize").length > 0) ? $("#gdvAddItemDependencies_pagesize :selected").text() : 10;

            $("#gdvAddItemDependencies").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShippingManagement, 'ShippingItemID'), name: 'shippingitem_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'ItemChkbox', elemDefault: false, controlclass: 'itemHeaderChkbox' },
                    { display: getLocale(AspxShippingManagement, 'Shipping Method ID'), name: 'shipping_method_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Product Quantity (More than item(s))'), name: 'produst_quantity', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxShippingManagement, 'Rate Value'), name: 'rate_value_from', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Is Rate In Percentage'), name: 'is_price_in_percentage', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Per Item'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShippingManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxShippingManagement, 'Added By'), name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ShippingManage.DeleteItemDependencies', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false } }
            });
        },

        DeleteItemDependencies: function (tblID, argus) {
            switch (tblID) {
                case "gdvAddItemDependencies":
                    var properties = {
                        onComplete: function (e) {
                            ShippingManage.DeleteShippingItemInfo(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this item dependencies?") + "</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultipleShippingItemDependencies: function (Ids, event) {
            ShippingManage.DeleteShippingItemInfo(Ids, event);
        },

        DeleteShippingItemInfo: function (_ShippingItem_Ids, event) {
            if (event) {
                var methodId = $("#hdnShippingMethodID").val();
                this.config.url = this.config.baseURL + "DeleteItemDependencies";
                this.config.data = JSON2.stringify({ shippingItemIds: _ShippingItem_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            }
            return false;
        },

        SaveItemDependenciesValues: function () {
            var methodId = $("#hdnShippingMethodID").val();
            var shippingItem_ID = 0;
            var _ItemDependenciesOptions = '';
            $('#tblItemDependencies>tbody tr').each(function () {
                _ItemDependenciesOptions += $(this).find("#txtQuantity").val() + ',';
                _ItemDependenciesOptions += $(this).find("#txtQuantityRateValue").val() + ',';
                var selectedItemStatus = $(this).find('#ddlItemDependencies option:selected').val();
                _ItemDependenciesOptions += selectedItemStatus + ',';
                _ItemDependenciesOptions += $(this).find("#chkPerItems").prop('checked') + '#';
            });
            this.config.url = this.config.baseURL + "SaveItemDependencies";
            this.config.data = JSON2.stringify({ shippingItemID: shippingItem_ID, shippingMethodID: methodId, itemDependenciesOptions: _ItemDependenciesOptions, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
        },
        SearchShippingMethods: function () {
            var shippingMethodNm = $.trim($("#txtMethodName").val());
            var deliveryTime = $.trim($("#txtSearchDeliveryTime").val());
            var weightFrom = $.trim($("#txtWeightFrom").val());
            var weightTo = $.trim($("#txtWeightTo").val());
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : $.trim($("#ddlIsActive").val()) == 0 ? true : false;
            if (shippingMethodNm.length < 1) {
                shippingMethodNm = null;
            }
            if (deliveryTime.length < 1) {
                deliveryTime = null;
            }

            if (weightFrom.length < 1) {
                weightFrom = null;
            }

            if (weightTo.length < 1) {
                weightTo = null;
            }

            if (parseInt(weightTo) < parseInt(weightFrom)) {
                csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + '</h2><p>' + getLocale(AspxShippingManagement, "Invalid Weight range! Weight From should be less than Weight To..") + '</p>');
                return false;
            }
            ShippingManage.BindShippingMethodGrid(shippingMethodNm, deliveryTime, weightFrom, weightTo, isAct);
        },
        ajaxSuccess: function (data) {
            var methodId = $("#hdnShippingMethodID").val();
            switch (ShippingManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(data.d, function (index, item) {
                        $("#ddlShippingService").append("<option value=" + item.ShippingProviderID + ">" + item.ShippingProviderServiceCode + "</option>");
                    });
                    break;
                case 2:
                    if (editFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping method has been updated successfully.") + '</p>');
                    } else {
                        csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping method has been saved successfully.") + '</p>');
                    }
                    ShippingManage.BindShippingMethodGrid(null, null, null, null, null);
                    ShippingManage.HideAll();
                    $("#divShowShippingMethodGrid").show();
                    break;
                case 3:
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + '</h2><p>' + getLocale(AspxShippingManagement, "Shipping method has been deleted successfully.") + '</p>');
                    ShippingManage.BindShippingMethodGrid(null, null, null, null, null);
                    break;
                case 4:
                    ShippingManage.BindShippingCostDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + '</h2><p>' + getLocale(AspxShippingManagement, "Cost dependencies has been deleted successfully.") + '</p>');
                    break;
                case 5:
                    ShippingManage.BindShippingCostDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Cost dependencies has been save successfully.") + '</p>');
                    $("#tblcostdependencies tr:gt(0)").next().remove();
                    RemovePopUp();
                    return false;
                    break;
                case 6:
                    ShippingManage.BindShippingWeightDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Weight dependencies has been deleted successfully.") + '</p>');
                    break;
                case 7:
                    ShippingManage.BindShippingWeightDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Weight dependencies has been save successfully.") + '</p>');
                    $("#tblWeightDependencies tr:gt(0)").next().remove();
                    RemovePopUp();
                    return false;
                    break;
                case 8:
                    ShippingManage.BindShippingItemDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Item dependencies has been deleted successfully.") + '</p>');
                    break;
                case 9:
                    ShippingManage.BindShippingItemDependencies(methodId);
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Item dependencies has been save successfully.") + '</p>');
                    $("#tblItemDependencies tr:gt(0)").next().remove();
                    RemovePopUp();
                    return false;
                case 10:
                    if (data.d == 0) {
                        $("#erruniqueOrder").html(getLocale(AspxShippingManagement, "Already Exist")).css("color", "red");
                        $("#txtDisplayOrder").val('');
                    } else {
                        $("#erruniqueOrder").html('');
                    }
                    break;
            }
        },
        ajaxFailure: function () {
            switch (ShippingManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to save shipping method!") + '</p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete shipping method!") + '</p>');
                    break;
                case 4:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete cost dependencies!") + '</p>');
                    break;
                case 5:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to save cost dependencies!") + '</p>');
                    break;
                case 6:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete weight dependencies!") + '</p>');
                    break;
                case 7:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to save weight dependencies!") + '</p>');
                    break;
                case 8:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete item dependencies!") + '</p>');
                    break;
                case 9:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to save item dependencies!") + '</p>');
                    break;
            }
        }
    };
    ShippingManage.init();
});