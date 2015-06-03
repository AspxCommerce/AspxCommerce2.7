var CostVariants = "";
$(function () {
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    var isUnique = false;
    var VariantID = '';
    var parentRow = '';
    var editFlag = '';
    var isNewVariant = false;

    checkExist = function (arr, tocheck) {
        var x = arr;
        for (var i = 0; i < x.length; i++) {
            if (x[i] == tocheck) {
                return true;
            }
        }
        return false;
    };
    CostVariants = {
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
            $('#ddlVatiantPriceModifierType').html("<option selected=\"selected\" value=\"false\">" + curSymbol + "</option><option value=\"true\">%</option>");
            CostVariants.LoadAllImages();
            CostVariants.BindCostVariantInGrid(null);
            CostVariants.HideAllDiv();
            $("#divShowOptionDetails").show();
            CostVariants.BindCostVariantsInputType();

            CostVariants.InitializeVariantTable();
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");
                CostVariants.CostVariantsInfoByID(VariantID);

            });
            $('#btnDeleteSelected').click(function () {
                var costVariant_ids = '';
                var hasSystemVariants = false;
                $(".costVariantChkbox").each(function (i) {
                    if ($(this).prop("checked")) {

                        if ($.trim($(this).parent('td').next().next().text()).toLowerCase() == 'yes') {
                            hasSystemVariants = true;
                        }
                    }
                });
                costVariant_ids = SageData.Get("gdvCostVariantGrid").Arr.join(',');
                if (!hasSystemVariants) {
                    if (costVariant_ids.length > 0) {
                        var properties = {
                            onComplete: function (e) {
                                CostVariants.ConfirmDeleteMultipleCostVariants(costVariant_ids, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxCostVariantOptionsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxCostVariantOptionsManagement, 'Are you sure you want to delete the selected variant option?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Please select at least one variant option.") + '</p>');
                    }
                }
                else {
                    csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Sorry! System cost variants can not be deleted.") + '</p>');
                    return false;
                }
            });

            $("#btnSaveVariantOption").click(function () {
                var existingOrders = $("#existingorders").val().trim().split(',');
                var selectedDisplayOrder = $("#txtDisplayOrder").val();

                for (var i = 0; i < existingOrders.length; i++) {
                    if (parseInt(existingOrders[i]) == parseInt(selectedDisplayOrder)) {
                        csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Informaion Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Please select a different Display Order,") + selectedDisplayOrder + getLocale(AspxCostVariantOptionsManagement,"is being used.") + '</p>');
                        return false;
                    }
                }
                var counter = 0;
                var checkfristMsg = false;
                $('#tblVariantTable>tbody tr:eq(0)').each(function () {
                    if (($(this).find('input.cssClassDisplayOrder,input.cssClassVariantValueName').val() == '') || ($(this).find('input.cssClassDisplayOrder').val() != '' && $(this).find('input.cssClassVariantValueName').val() == '') || ($(this).find('input.cssClassDisplayOrder').val() == '' && $(this).find('input.cssClassVariantValueName').val() != '')) {
                        csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Informaion Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Please enter Item Cost Variant values.") + '</p>');
                        counter++;
                        checkfristMsg = true;
                        return false;
                    }
                });
                var variantsProperties = $("#tblVariantTable tr:gt(1)").find("input.cssClassDisplayOrder,input.cssClassVariantValueName");
                var count = 0;
                if (checkfristMsg != true) {
                    $.each(variantsProperties, function (index, item) {
                        if ($(this).val() <= '') {
                            csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Informaion Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Please enter Item Cost Variant Properties.") + '</p>');
                            count++;
                            return false;
                        }
                    });
                }
                if (count == 0 && counter == 0)
                    CostVariants.SaveCostVariantsInfo();
            });

            $('#ddlAttributeType').change(function () {
                CostVariants.HideAllCostVariantImages();
            });

            $("#btnSearchCostVariants").on("click", function () {
                CostVariants.SearchCostVariantName();
            });
            $('#txtVariantName').keyup(function (event) {
                if (event.keyCode == 13) {
                    $("#btnSearchCostVariants").click();
                }
            });
            $("#btnAddNewVariantOption").click(function () {
                isNewVariant = true;
                CostVariants.OnInit();
                CostVariants.ClearForm();
                CostVariants.HideAllDiv();
                $("#divAddNewOptions").show();
                $("#txtPos").DigitOnly('.cssClassDisplayOrder', '');
                $("#txtPriceModifier").DigitDecimalAndNegative('.cssClassPriceModifier', '');
                $("#txtWeightModifier").DigitDecimalAndNegative('.cssClassWeightModifier', '');
            });
            $("#txtPriceModifier").bind("contextmenu", function (e) {
                return false;
            });
            $("#txtWeightModifier").bind("contextmenu", function (e) {
                return false;
            });
            $("#btnBack").click(function () {
                CostVariants.HideAllDiv();
                $("#divShowOptionDetails").show();
            });

            $("#btnReset").click(function () {
                CostVariants.OnInit();
                CostVariants.ClearForm();
            });
            $("#txtDisplayOrder").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    $("#dispalyOrder").html(getLocale(AspxCostVariantOptionsManagement,"Digits Only")).css("color", "red").show().fadeOut(1600);
                    return false;
                }
            });

                       $('#txtCostVariantName').blur(function () {
                               var errors = '';
                var costVariantName = $(this).val();
                var variant_id = $('#btnSaveVariantOption').prop("name");
                if (variant_id == '') {
                    variant_id = 0;
                }
                if (!costVariantName) {
                    errors += getLocale(AspxCostVariantOptionsManagement, 'Please enter cost variant name');
                }
                                   else if (!CostVariants.IsUnique(costVariantName, variant_id)) {
                    errors += getLocale(AspxCostVariantOptionsManagement,"Please enter unique cost variant name.") + costVariantName.trim() + getLocale(AspxCostVariantOptionsManagement,"already exists.")+'<br/>';
                }

                if (errors) {
                    $('.cssClassRight').hide();
                    $('.cssClassError').show();
                    $(".cssClassError").parent('div').addClass("diverror");
                    $('.cssClassError').prevAll("input:first").addClass("error");
                    $('.cssClassError').html(errors);
                    return false;
                } else {
                    $('.cssClassRight').show();
                    $('.cssClassError').hide();
                    $(".cssClassError").parent('div').removeClass("diverror");
                    $('.cssClassError').prevAll("input:first").removeClass("error");
                }

            });

            $(".delbutton").click(function () {
                               var costVariantId = $(this).prop("id").replace(/[^0-9]/gi, '');
                CostVariants.DeleteCostVariants(costVariantId);
                CostVariants.HideAllDiv();
                $("#divShowOptionDetails").show();

            });
        },
        ajaxCall: function (config) {
            $.ajax({
                type: CostVariants.config.type,
                contentType: CostVariants.config.contentType,
                cache: CostVariants.config.cache,
                async: CostVariants.config.async,
                data: CostVariants.config.data,
                dataType: CostVariants.config.dataType,
                url: CostVariants.config.url,
                success: CostVariants.ajaxSuccess,
                error: CostVariants.ajaxFailure
            });
        },
        LoadAllImages: function () {
            $("#ajaxLoad").prop("src", '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('.cssClassSuccessImg').prop("src", '' + aspxTemplateFolderPath + '/images/right.png');
            $('.cssClassAddRow').prop("src", '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
        },
        CheckSameCostVarValue: function () {
            var arr = [];
            $("#tblVariantTable >tbody>tr").each(function (index) {
                arr.push($(this).find("input[class=cssClassVariantValueName]").val().toLower());
            });
            return arr.Unique();
        },
        InitializeVariantTable: function () {
                       $("#tblVariantTable").on("click", ".cssClassAddRow", function () {

                if ($(this).parents("tr:eq(0)").index() == 0)
                    $(this).parents("tr:eq(0)").find("td:last").html("<a class=\"icon-delete cssClassRemoveRow\"></a>");
                var cloneRow = $(this).closest('tr').clone(true);
                $(cloneRow).appendTo("#tblVariantTable");
                $(cloneRow).find("input[type='text']").val('');
                $(cloneRow).find("input[type='hidden']").val('0');
                $(cloneRow).find(".cssClassDisplayOrder").val($("#tblVariantTable >tbody>tr:last")[0].rowIndex);
                $(cloneRow).find("td:last").find("a").remove();
                $(cloneRow).find("td:last").append("<a class=\"icon-delete cssClassRemoveRow\"></a>");
                repos();
            });
            var repos = function () {
                $("#tblVariantTable tbody tr").each(function (index, tr) { var pos = index + 1; $(this).find('.cssClassDisplayOrder').val(pos); });
            }
            $("#tblVariantTable").on("click", ".cssClassRemoveRow", function () {


                $(this).closest('tr').remove();
                repos();
                if ($("#tblVariantTable tbody tr").length == 1) {
                    $("#tblVariantTable tbody tr:first").find("td:last").html('');
                }
            });
        },

        HideAllCostVariantImages: function () {
            var selectedVal = $("#ddlAttributeType").val();
            if (selectedVal == 9 || selectedVal == 11) {                $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").hide();
            } else {
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").show();
            }
        },

        DeleteCostVaraiantValue: function (costVariantValueId, parentRow) {
            var properties = {
                onComplete: function (e) {
                    CostVariants.ConfirmDeleteCostVariantValue(costVariantValueId, parentRow, e);
                }
            };
                       csscody.confirm("<h2>" + getLocale(AspxCostVariantOptionsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxCostVariantOptionsManagement, 'Do you want to delete this cost variant value?') + "</p>", properties);
        },

        ConfirmDeleteCostVariantValue: function (costVariantValueId, parentRow, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteCostVariantValue";
                this.config.data = JSON2.stringify({ costVariantValueID: costVariantValueId, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
        },

        BindCostVariantInGrid: function (costVariantNm) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.method = "GetCostVariants";
            this.config.data = { variantName: costVariantNm, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCostVariantGrid_pagesize").length > 0) ? $("#gdvCostVariantGrid_pagesize :selected").text() : 10;

            $("#gdvCostVariantGrid").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                     { display: getLocale(AspxCostVariantOptionsManagement, 'Cost Variant ID'), name: 'costvariant_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'costVariantChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                     { display: getLocale(AspxCostVariantOptionsManagement, 'Cost Variant Name'), name: 'cost_variant_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                     { display: getLocale(AspxCostVariantOptionsManagement, 'In System'), name: '_isSystem', cssclass: 'cssClassHeadBoolean', controlclass: '', elemClass: 'cssClassIsSystem', coltype: 'label', align: 'left', type: 'boolean', hide: true, format: 'Yes/No' },
                     { display: getLocale(AspxCostVariantOptionsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: getLocale(AspxCostVariantOptionsManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CostVariants.EditCostVariant', arguments: '1,2,3,4' },
                     { display: getLocale(AspxCostVariantOptionsManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'CostVariants.DeleteCostVariant', arguments: '1,2,3,4' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCostVariantOptionsManagement, "No Records Found!"),
                param: data,                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 3: { sorter: false } }
            });
        },

        EditCostVariant: function (tblID, argus) {
            VariantID = argus[0];
            switch (tblID) {
                case "gdvCostVariantGrid":
                    isNewVariant = false;
                    $('#languageSelect').find('li').each(function () {
                        if ($(this).attr("value") == aspxCommonObj.CultureName) {
                            $('#languageSelect').find('li').removeClass("languageSelected");
                            $(this).addClass("languageSelected");
                            return;

                        }
                    });
                    editFlag = argus[0];
                    CostVariants.ClearForm();
                    CostVariants.OnInit();
                    $(".delbutton").prop("id", 'variantid_' + argus[0]);
                    if (argus[4].toLowerCase() == 'yes') {
                        $(".delbutton").hide();
                    }
                    else {
                        $(".delbutton").show();
                    }
                    $("#btnSaveVariantOption").prop("name", argus[0]);
                    $("#" + lblCostVarFormHeading).html(getLocale(AspxCostVariantOptionsManagement,"Edit Cost variant Option:") + argus[3]);
                    CostVariants.CostVariantsInfoByID(VariantID);
                    $(".cssClassDisplayOrder").prop("disabled", "disabled");
            }
        },
        CostVariantsInfoByID: function (ID) {
            //Added for rebinding data in language select options
            if (isNewVariant) {
                return false;
            }
            var aspxCommonInfo = aspxCommonObj();
            if ($("#languageSelect").length > 0) {
                aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            };
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetCostVariantInfoByCostVariantID";
            this.config.data = JSON2.stringify({ costVariantID: ID, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        FillForm: function (response) {
            $.each(response.d, function (index, item) {
               
                $('#txtCostVariantName').val(item.CostVariantName);
                $('#ddlAttributeType').val(item.InputTypeID);
                $('#ddlAttributeType').prop('disabled', 'disabled');
                $('#txtDisplayOrder').val(item.DisplayOrder);
                $("#txtDescription").val(item.Description);
                $('input[name=chkActive]').prop('checked', item.IsActive);
                $("#existingorders").val(item.OtherDisplayOrders);
            });
        },
        BindCostVariantValueByCostVariantID: function (costVariantId) {
            var aspxCommonInfo = aspxCommonObj();
            if ($("#languageSelect").length > 0) {
                aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            };
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetCostVariantValuesByCostVariantIDForAdmin";
            this.config.data = JSON2.stringify({ costVariantID: costVariantId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
                   },
        DeleteCostVariant: function (tblID, argus) {
            switch (tblID) {
                case "gdvCostVariantGrid":
                    if (argus[4].toLowerCase() == 'no') {
                        CostVariants.DeleteCostVariants(argus[0]);
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Sorry! System cost variants can not be deleted.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        DeleteCostVariants: function (_costVariantId) {

            var properties = {
                onComplete: function (e) {
                    CostVariants.ConfirmSingleDeleteCostVariant(_costVariantId, e);
                }
            };
                       csscody.confirm("<h2>" + getLocale(AspxCostVariantOptionsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxCostVariantOptionsManagement, 'Are you sure you want to delect this variant option?') + "</p>", properties);

        },
        ConfirmSingleDeleteCostVariant: function (costVariantID, event) {
            if (event) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;
                this.config.url = this.config.baseURL + "DeleteSingleCostVariant";
                this.config.data = JSON2.stringify({ costVariantID: costVariantID, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            return false;
        },
        ConfirmDeleteMultipleCostVariants: function (costVariant_ids, event) {
            if (event) {
                CostVariants.DeleteMultipleCostVariants(costVariant_ids);
            }
        },
        DeleteMultipleCostVariants: function (_costVariant_ids) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.CultureName = null;
            this.config.url = this.config.baseURL + "DeleteMultipleCostVariants";
            this.config.data = JSON2.stringify({ costVariantIDs: _costVariant_ids, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
            return false;
        },
        HideAllDiv: function () {
            $("#divShowOptionDetails").hide();
            $("#divAddNewOptions").hide();
        },
        BindCostVariantsInputType: function () {
            this.config.url = this.config.baseURL + "GetCostVariantInputTypeList";
            this.config.data = '{}';
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },
        ClearForm: function () {
            $(".delbutton").removeAttr("id");
            $("#btnSaveVariantOption").removeAttr("name");
            $(".delbutton").hide();
            $("#btnReset").show();
            $("#txtCostVariantName").val('');
            $("#txtDescription").val('');
            $("#ddlAttributeType").prop('selectedIndex', 0);
            $('#ddlAttributeType').removeAttr('disabled');
            $('#txtDisplayOrder').val('');
            $('input[name=chkActive]').prop('checked', 'checked');

            $("#" + lblCostVarFormHeading).html(getLocale(AspxCostVariantOptionsManagement,"Add New Cost Variant Option"));
                       $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
            $("#tblVariantTable>tbody").find("input[type='text']").val('');
            $("#tblVariantTable>tbody").find("select").val(1);
            $("#tblVariantTable>tbody").find("input[type='hidden']").val('0');
            $("#tblVariantTable>tbody").find(".cssClassDisplayOrder").val('').val(1);
            $(".cssClassDisplayOrder").prop("disabled", "disabled");
                       return false;
        },
        OnInit: function () {
            $('#btnReset').hide();
            $('.cssClassRight').hide();
            $('.cssClassError').hide();
            CostVariants.SelectFirstTab();
        },
        SelectFirstTab: function () {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
        },
        IsUnique: function (costVariantName, costVariantId) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            aspxCommonInfo.CultureName = null;
            this.config.url = this.config.baseURL + "CheckUniqueCostVariantName";
            this.config.data = JSON2.stringify({ costVariantName: costVariantName, costVariantId: costVariantId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
            return isUnique;
        },
        SaveCostVariantsInfo: function () {
            var variant_id = $('#btnSaveVariantOption').prop("name");
            if (variant_id != '') {
                CostVariants.SaveCostVariant(variant_id, false);
            } else {
                CostVariants.SaveCostVariant(0, true);
            }
        },
        SaveCostVariant: function (_costVariantId, _isNewflag) {
            editFlag = _costVariantId;
            var validateErrorMessage = '';
                       var costVariantName = $('#txtCostVariantName').val();
            if (!costVariantName) {
                validateErrorMessage += '' + getLocale(AspxCostVariantOptionsManagement, "Please enter cost variant option name.") + '<br/>';
            } else if (!CostVariants.IsUnique(costVariantName, _costVariantId)) {
                validateErrorMessage += '' + getLocale(AspxCostVariantOptionsManagement, "Please enter unique cost variant name.") + '"' + costVariantName.trim() + '"' + getLocale(AspxCostVariantOptionsManagement, " already exists.") + '<br/>';
            }

                       var costVariantDisplayOrder = $("#txtDisplayOrder").val();
            if (!costVariantDisplayOrder) {
                $("#txtDisplayOrder").focus();
                validateErrorMessage += '' + getLocale(AspxCostVariantOptionsManagement, "Please enter cost variant display order.") + '<br/>';
            } else {
                var value = costVariantDisplayOrder.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                var intRegex = /^\d+$/;
                if (!intRegex.test(value)) {
                    $("#txtDisplayOrder").focus();
                    validateErrorMessage += '' + getLocale(AspxCostVariantOptionsManagement, "Cost variant display order is numeric value.") + '<br/>';
                }
            }

            if (validateErrorMessage) {
                validateErrorMessage = '' + getLocale(AspxCostVariantOptionsManagement, "The following value are required:") + ' <br/>' + validateErrorMessage;
                csscody.alert('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Information Alert") + '</h2><p>' + validateErrorMessage + '</p>');
                return false;
            } else {
                var selectedCostVariantType = $("#ddlAttributeType :selected").val();
                var costVariantObj = {
                    CostVariantID: _costVariantId,
                    CostVariantName: $('#txtCostVariantName').val(),
                    InputTypeID: $('#ddlAttributeType').val(),
                    DisplayOrder: $('#txtDisplayOrder').val(),
                    ShowInAdvanceSearch: false,                    ShowInComparison: false,                    IsIncludeInPriceRule: false,                    Flag: _isNewflag,
                    Description: $('#txtDescription').val(),
                    IsActive: $('input[name=chkActive]').is(':checked'),
                    IsModified: !(_isNewflag)
                };
                var _VariantOptions = '';
                var arr = [];
                var counter = 1;
                $('#tblVariantTable>tbody tr').each(function (index) {
                    if (!checkExist(arr, $(this).find("input[class=cssClassVariantValueName]").val().toLowerCase())) {
                        arr.push($(this).find("input[class=cssClassVariantValueName]").val().toLowerCase());
                        _VariantOptions += $(this).find(".cssClassVariantValue").val() + '%';
                        _VariantOptions += counter + '%';                        _VariantOptions += $(this).find(".cssClassVariantValueName").val() + '%';                        _VariantOptions += $(this).find(".cssClassIsActive").val() + '#';
                        counter++;
                    }
                });

                CostVariants.AddCostVariantInfo(costVariantObj, _VariantOptions);
            }
            return false;
        },

        AddCostVariantInfo: function (costVariantObj, _VariantOptions) {
            var aspxTempCommonObj = aspxCommonObj();
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            var params = { variantObj: costVariantObj, variantOptions: _VariantOptions, aspxCommonObj: aspxTempCommonObj };
            this.config.url = this.config.baseURL + "SaveAndUpdateCostVariant";
            this.config.data = JSON2.stringify(params);
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
        },
        SearchCostVariantName: function () {
            var costVariantNm = $.trim($("#txtVariantName").val());
            if (costVariantNm.length < 1) {
                costVariantNm = null;
            }
            CostVariants.BindCostVariantInGrid(costVariantNm);
        },
        ajaxSuccess: function (data) {
            switch (CostVariants.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                                       csscody.info('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Cost variants properties value has been deleted successfully.") + '</p>');
                    return false;
                    break;
                case 2:
                    CostVariants.FillForm(data);
                     CostVariants.BindCostVariantValueByCostVariantID(VariantID);
                    CostVariants.HideAllDiv();
                    $("#divAddNewOptions").show();
                    break;
                case 3:
                    if (data.d.length > 0) {
                        $("#tblVariantTable>tbody").html('');
                        $.each(data.d, function (index, item) {
                            if (item.DisplayOrder == null) {
                                item.DisplayOrder = '';
                            }
                            var newVariantRow = '';
                            newVariantRow += '<tr><td><input type="hidden" size="3" class="cssClassVariantValue" value="' + item.CostVariantsValueID + '"><input type="text" size="3" class="cssClassDisplayOrder" value="' + item.DisplayOrder + '"></td>';
                            newVariantRow += '<td><input type="text" class="cssClassVariantValueName" value="' + item.CostVariantsValueName + '"></td>';
                                                                                                                                        newVariantRow += '<td><select class="cssClassIsActive isActive_' + item.CostVariantsValueID + '"><option value="true">Active</option><option value="false">Disabled</option></select></td>';
                            newVariantRow += '<td><span class="nowrap">';
                            newVariantRow += '<img class="cssClassAddRow" title="Add empty item" alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif">&nbsp;';
                                                                                  newVariantRow += '</span></td><td>&nbsp;</td></tr>';
                            $("#tblVariantTable>tbody").append(newVariantRow);

                                                                                  $('.isActive_' + item.CostVariantsValueID).val('' + item.IsActive + '');
                            $("#divAddNewOptions").show();
                            $("#txtPos").DigitOnly('.cssClassDisplayOrder', '');
                                                                                                                                                                                                                                                });
                                           }
                    break;
                case 4:
                    CostVariants.BindCostVariantInGrid(null);
                    csscody.info('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Cost variants option has been deleted successfully.") + '</p>');
                    break;
                case 5:
                    CostVariants.BindCostVariantInGrid(null);
                    csscody.info('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Cost variants options has been deleted successfully.") + '</p>');
                    break;
                case 6:
                    $.each(data.d, function (index, item) {
                        $("#ddlAttributeType").append("<option value=" + item.InputTypeID + ">" + item.InputType + "</option>");
                    });
                    break;
                case 7:
                    isUnique = data.d;
                    break;
                case 8:
                    CostVariants.BindCostVariantInGrid(null);
                    CostVariants.HideAllDiv();
                    $("#divShowOptionDetails").show();
                    if (editFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Cost variants option has been updated successfully.") + '</p>');
                    } else {
                        csscody.info('<h2>' + getLocale(AspxCostVariantOptionsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCostVariantOptionsManagement, "Cost variants option has been saved successfully.") + '</p>');
                    }
                    break;
            }
        }
    };
    CostVariants.init();
});