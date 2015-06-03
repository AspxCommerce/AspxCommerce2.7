var attributesManage = '';
function reinitialise() {
    if ($('body').find('#BoxOverlay').length == 0) {
        csscody.initialize();
    }
}
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
    var editFlag = 0;
    var arrAttrValueId = "";
    attributesManage = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: attributesManage.config.type,
                beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: attributesManage.config.contentType,
                cache: attributesManage.config.cache,
                async: attributesManage.config.async,
                url: attributesManage.config.url,
                data: attributesManage.config.data,
                dataType: attributesManage.config.dataType,
                success: attributesManage.ajaxSuccess,
                error: attributesManage.ajaxFailure
            });
        },
        LoadAttributeStaticImage: function () {
            $('.cssClassSuccessImg').prop('src', '' + aspxTemplateFolderPath + '/images/right.jpg');
        },
        ClearOptionTable: function (btnAddOption) {
            btnAddOption.closest("tr:eq(0)").find("input:not(:last)").each(function (i) {
                $(this).val('');
                $(this).removeAttr('checked');
            });
        },
        onInit: function () {
            attributesManage.SetFirstTabActive();
            $("#ddlApplyTo").val('0');
            $('.itemTypes').hide();
            $('#btnReset').hide();
            $('.cssClassRight').hide();
            $('.cssClassError').hide();
            $("#lstItemType").each(function () {
                $("#lstItemType option").removeAttr("selected");
            });
        },
        SetFirstTabActive: function () {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
            $tabs.tabs('option', 'active', 0);
        },
        Boolean: function (str) {
            switch (str) {
                case "1":
                    return true;
                case "0":
                    return false;
                default:
            }
        },
        CreateValidationClass: function (attValType) {
            var validationClass = '';

            switch (attValType) {
                case "1":
                    validationClass += 'verifyAlphabetsOnly';
                    break;
                case "2":
                    validationClass += 'verifyAlphaNumeric';
                    break;
                case "3":
                    validationClass += 'verifyDecimal';
                    break;
                case "4":
                    validationClass += 'verifyEmail';
                    break;
                case "5":
                    validationClass += 'verifyInteger';
                    break;
                case "6":
                    validationClass += 'verifyPrice';
                    break;
                case "7":
                    validationClass += 'verifyUrl';
                    break;
                default:
                    validationClass += '';
                    break;
            }
            return validationClass;
        },
        GetValidationTypeErrorMessage: function (attValType) {
            var retString = ''
            switch (attValType) {
                case "1":
                    retString = getLocale(AspxAttributesManagement, 'Alphabets Only');
                    break;
                case "2":
                    retString = getLocale(AspxAttributesManagement, 'AlphaNumeric');
                    break;
                case "3":
                    retString = getLocale(AspxAttributesManagement, 'Decimal Number');
                    break;
                case "4":
                    retString = getLocale(AspxAttributesManagement, 'Email Address');
                    break;
                case "5":
                    retString = getLocale(AspxAttributesManagement, 'Integer Number');
                    break;
                case "6":
                    retString = getLocale(AspxAttributesManagement, 'Price error');
                    break;
                case "7":
                    retString = getLocale(AspxAttributesManagement, 'Web URL');
                    break;
            }
            return retString;
        },
        ClearForm: function () {
            $('.class-text').removeClass('error').next('span').removeClass('error');
            var inputs = $("#container-7").find('INPUT, SELECT, TEXTAREA');
            $.each(inputs, function (i, item) {
                rmErrorClass(item);
                $(this).val('');
                $(this).prop('checked', false);
            });
            attributesManage.onInit();
            $('#' + lblAttrFormHeading).html(getLocale(AspxAttributesManagement, "New Item Attribute"));
            $(".delbutton").removeAttr("id");
            $("#btnSaveAttribute").removeAttr("name");
            $('#' + lblLength).html(getLocale("Length:"));
            $(".delbutton").hide();
            $("#btnReset").show();
            $(".required:enabled").each(function () {

                if ($(this).parent("td").find("span.error").length == 1) {
                    $(this).removeClass("error").addClass("required");
                    $(this).parent("td").find("span.error").remove();
                }

            });
            $('#txtAttributeName').val('');
            $('#txtAttributeName').removeAttr('disabled');
            $('#ddlAttributeType').val('1');
            $('#ddlAttributeType').removeAttr('disabled');

            $("#default_value_text").prop("class", "sfInputbox");
            $("#default_value_text").val('');
            $("#default_value_textarea").val('');
            $("#default_value_date").val('');
            $("#trdefaultValue").show();
            $("#default_value_text").show();
            $("#fileDefaultTooltip").html('');
            $("#fileDefaultTooltip").hide();
            $("#default_value_textarea").hide();
            $("#div_default_value_date").hide();
            $("#default_value_yesno").hide();

            $('#default_value_text').val('');
            $("#dataTable tr:gt(1)").remove();
            attributesManage.ClearOptionTable($("input[type='button'].AddOption"));
            $('#trOptionsAdd').hide();

            $('#ddlTypeValidation').val('8');
            $('#ddlTypeValidation').removeAttr('disabled');

            $('#txtLength').val('');
            $('#txtLength').removeAttr('disabled');
            $('#txtLength').next('span').next('span').show();
            $('#txtAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $('#txtDisplayOrder').val('');
            $('#ddlApplyTo').val('0');
            $('.itemTypes').hide();

            $('input[name=chkUniqueValue]').removeAttr('checked');
            $('input[name=chkValuesRequired]').removeAttr('checked');
            $('input[name=chkActive]').prop('checked', 'checked');
            $('#activeTR').show();

            $('input[name=chkIsEnableEditor]').removeAttr('checked');
            $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
            $('input[name=chkUseInAdvancedSearch]').removeAttr('disabled');
            $('input[name=chkUseInAdvancedSearch]').removeAttr('checked');
            $('input[name=chkComparable]').removeAttr('disabled');
            $('input[name=chkComparable]').removeAttr('checked');
            $('input[name=chkUseForPriceRule]').removeAttr('disabled');
            $('input[name=chkUseForPriceRule]').removeAttr('checked');
            $('input[name=chkIsUseInFilter]').removeAttr('disabled');
            $('input[name=chkShowInItemListing]').removeAttr('checked');
            $('input[name=chkShowInItemDetail]').removeAttr('checked');

            $('input[name=optionValueId]').val('0');
            return false;
        },
        BindAttributeOptionsValues: function (_fillOptionValues) {
            var _fillOptions = _fillOptionValues;
            if (_fillOptions != undefined && _fillOptions != "") {
                var arr = _fillOptions.split("!#!");
                var htmlContent = '';
                $.each(arr, function (i) {
                    var btnOption = "Add More";
                    var btnName = "AddMore";
                    if (i > 0) {
                        btnOption = "Delete Option";
                        var btnName = "DeleteOption";
                    }
                    var arr2 = arr[i].split("#!#");
                    var cloneRow = $('#dataTable tbody>tr:last').clone(true);
                    $(cloneRow).find("input").each(function (j) {

                        if (this.name == "optionValueId") {
                            $(this).val(arr2[0]);
                        } else if (this.name == "value") {
                            $(this).val(arr2[1]);
                        } else if (this.name == "position") {
                            $(this).val(arr2[2]);
                        } else if (this.name == "Alias") {
                            $(this).val(arr2[3]);
                        } else if ($(this).hasClass("class-isdefault")) {
                            this.checked = attributesManage.Boolean(arr2[4]);
                        } else if ($(this).hasClass("AddOption")) {
                            $(this).prop("name", btnName);
                            $(this).prop("value", btnOption);
                        }
                    });
                    $(cloneRow).appendTo("#dataTable");
                });
                $('#dataTable>tbody tr:first').remove();
            }
        },
        ValidationTypeEnableDisable: function (fillOptionValues, isChanged) {
            var selectedVal = $("#ddlAttributeType :selected").val();
            switch (selectedVal) {
                case "1":
                    $("#ddlTypeValidation").removeAttr('disabled');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Default Value:"));
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "2":
                    $('#ddlTypeValidation').val('8');
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Rows:");
                    if (isChanged) {
                        $('#txtLength').val(3);
                    }
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").show();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').removeAttr('disabled');
                    break;
                case "3":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Default Value:"));
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").show();
                    $("#default_value_date").datepicker({ dateFormat: 'yy/mm/dd' });
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "4":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Default Value:"));
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").show();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "5":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Size:"));
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    if (isChanged) {
                        $('#txtLength').val(3);
                    }
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").val("Add More");
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "6":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").val('');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").val("Add More");
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "7":
                    $('#ddlTypeValidation').val('6');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Default Value:"));
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "8":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Allowed File Extension(s):"));
                    $("#fileDefaultTooltip").html(getLocale(AspxAttributesManagement, "- Separate each file extensions with space"));
                    $("#fileDefaultTooltip").show();
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Size:(KB)"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "9":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();

                    $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").hide();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "10":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").val("Add More");
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "11":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").hide();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "12":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").prop('disabled', 'disabled');
                    $("#txtLength").prop('disabled', 'disabled');
                    $('#txtLength').next('span').next('span').hide();
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").val(getLocale(AspxAttributesManagement, "Add More"));
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                case "13":
                    $("#ddlTypeValidation").removeAttr('disabled');
                    $('#' + lblDefaultValue).html(getLocale(AspxAttributesManagement, "Default Value:"));
                    $('#' + lblLength).html(getLocale(AspxAttributesManagement, "Length:"));
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#txtLength").removeAttr('disabled');
                    $('#txtLength').next('span').next('span').show();
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                    break;
                default:
                    break;
            }
        },

        BindAttributeGrid: function (attributeNm, required, SearchComparable, isSystem) {
            this.config.url = this.config.baseURL;
            this.config.method = "GetAttributesList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAttributes_pagesize").length > 0) ? $("#gdvAttributes_pagesize :selected").text() : 10;
            var attrbuteBindObj = {
                AttributeName: attributeNm,
                IsRequired: required,
                ShowInComparison: SearchComparable,
                IsSystemUsed: isSystem
            };
            $("#gdvAttributes").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxAttributesManagement, 'Attribute ID'), name: 'attr_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: getLocale(AspxAttributesManagement, 'Attribute Name'), name: 'attr_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxAttributesManagement, 'Attribute Alias'), name: 'attr_alias', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxAttributesManagement, 'Required'), name: 'IsRequired', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, 'In System'), name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, 'Searchable'), name: 'ShowInSearch', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxAttributesManagement, 'Used In Configurable Item'), name: 'IsUsedInConfigItem:', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxAttributesManagement, 'Comparable'), name: 'ShowInComparison', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                                   { display: getLocale(AspxAttributesManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: getLocale(AspxAttributesManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'attributesManage.EditAttributes', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxAttributesManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'attributesManage.DeleteAttributes', arguments: '5' },
                                   { display: getLocale(AspxAttributesManagement, "Activate"), name: 'activate', enable: true, _event: 'click', trigger: '4', callMethod: 'attributesManage.ActiveAttributes', arguments: '5' },
                    { display: getLocale(AspxAttributesManagement, "Deactivate"), name: 'deactivate', enable: true, _event: 'click', trigger: '5', callMethod: 'attributesManage.DeactiveAttributes', arguments: '5' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxAttributesManagement, 'No Records Found!'),
                param: { attrbuteBindObj: attrbuteBindObj, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 10: { sorter: false} }
            });
        },
        FillDefaultValue: function (defaultVal) {
            var selectedAttributeType = $("#ddlAttributeType :selected").val();
            switch (selectedAttributeType) {
                case "1":
                    $('#default_value_text').val(defaultVal);
                    break;
                case "2":
                    $('textarea#default_value_textarea').val(defaultVal);
                    break;
                case "3":
                    $('#default_value_date').val(defaultVal);
                    break;
                case "4":
                    $('#default_value_yesno').val(defaultVal);
                    break;
                case "8":
                    $('#default_value_text').val(defaultVal);
                    break;
                default:
                    break;
            }
        },
        FillForm: function (response) {
            $.each(response.d, function (index, item) {

                $('#txtAttributeName').val(item.AttributeName);
                $('#ddlAttributeType').val(item.InputTypeID);
                $('#ddlAttributeType').prop('disabled', 'disabled');

                attributesManage.FillDefaultValue(item.DefaultValue);

                $('#ddlTypeValidation').val(item.ValidationTypeID);
                $('#txtLength').val(item.Length);
                $('#txtAliasName').val(item.AliasName);
                $('#txtAliasToolTip').val(item.AliasToolTip);
                $('#txtAliasHelp').val(item.AliasHelp);
                $('#txtDisplayOrder').val(item.DisplayOrder);

                $('input[name=chkUniqueValue]').prop('checked', item.IsUnique);
                $('input[name=chkValuesRequired]').prop('checked', item.IsRequired);
                $('input[name=chkActive]').prop('checked', item.IsActive);

                $('input[name=chkIsEnableEditor]').prop('checked', item.IsEnableEditor);
                $('input[name=chkUseInAdvancedSearch]').prop('checked', item.ShowInAdvanceSearch);
                $('input[name=chkComparable]').prop('checked', item.ShowInComparison);
                $('input[name=chkUseForPriceRule]').prop('checked', item.IsIncludeInPriceRule);
                $('input[name=chkIsUseInFilter]').prop('checked', item.IsUseInFilter);
                $('input[name=chkShowInItemListing]').prop('checked', item.IsShowInItemListing);
                $('input[name=chkShowInItemDetail]').prop('checked', item.IsShowInItemDetail);
                attributesManage.ValidationTypeEnableDisable(item.FillOptionValues, false);


                if (item.ItemTypes.length > 0) {
                    $('#ddlApplyTo').val('1');
                    $('.itemTypes').show();
                    var itemsType = item.ItemTypes;
                    var arr = itemsType.split(",");
                    $.each(arr, function (i) {
                        $("#lstItemType option[value=" + arr[i] + "]").prop("selected", "selected");
                    });
                } else {
                    $('#ddlApplyTo').val('0');
                }
            });
        },
        EditAttributes: function (tblID, argus) {
            attributesManage.ClearForm();
            switch (tblID) {
                case "gdvAttributes":
                    $('#languageSelect').find('li').each(function () {
                        if ($(this).attr("value") == AspxCommerce.utils.GetCultureName()) {
                            $('#languageSelect').find('li').removeClass("languageSelected");
                            $(this).addClass("languageSelected");
                            return;

                        }
                    });
                    $('#' + lblAttrFormHeading).html(getLocale(AspxAttributesManagement, 'Edit Item Attribute: ') + argus[3]);
                    $('#txtAttributeName').prop('disabled', 'disabled');
                    if (argus[7].toLowerCase() != "yes") {
                        $(".delbutton").prop("id", 'attributeid' + argus[0]);
                        $(".delbutton").show();
                        $('#activeTR').show();
                        $("#ddlTypeValidation").removeAttr('disabled');
                        $('input[name=chkUseInAdvancedSearch]').removeAttr('disabled');
                        $('input[name=chkComparable]').removeAttr('disabled');
                        $('input[name=chkIsUseInFilter]').removeAttr('disabled');
                        $('input[name=chkUseForPriceRule]').removeAttr('disabled');
                        $('input[name=chkShowInItemListing]').removeAttr('disabled');
                        $('input[name=chkShowInItemDetail]').removeAttr('disabled');

                        $("input[class=class-text]").removeAttr('disabled');
                        $("input[class=class-isdefault]").removeAttr('disabled');
                        $("input[name=AddMore]").removeAttr('disabled');
                        $("input[name=DeleteOption]").removeAttr('disabled');
                        $('#lstItemType').removeAttr('disabled');
                        $('#txtDisplayOrder').removeAttr('disabled');
                        $('#txtLength').removeAttr('disabled');
                        $('#ddlApplyTo').removeAttr('disabled');
                        $("input[name=default_value_text]").removeAttr('disabled');
                        $('#txtLength').removeAttr('disabled');
                        $("input[name=chkUniqueValue]").removeAttr('disabled');
                        $("input[name=chkValuesRequired]").removeAttr('disabled');
                    } else {
                        $(".delbutton").hide();
                        $('#activeTR').hide();
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $('input[name=chkUseInAdvancedSearch]').prop('disabled', 'disabled');
                        $('input[name=chkComparable]').prop('disabled', 'disabled');
                        $('input[name=chkIsUseInFilter]').prop('disabled', 'disabled');
                        $('input[name=chkUseForPriceRule]').prop('disabled', 'disabled');
                        $('input[name=chkShowInItemListing]').prop('disabled', 'disabled');
                        $('input[name=chkShowInItemDetail]').prop('disabled', 'disabled');


                        $("input[class=class-text]").prop('disabled', 'disabled');
                        $("input[class=class-isdefault]").prop('disabled', 'disabled');
                        $("input[name=AddMore]").attr("disabled", "disabled");
                        $("input[name=DeleteOption]").attr("disabled", "disabled");
                        $("input[name=Alias]").removeAttr('disabled');
                        $('#lstItemType').attr("disabled", "disabled");
                        $('#txtDisplayOrder').attr("disabled", "disabled");
                        $('#txtLength').attr("disabled", "disabled");
                        $('#ddlApplyTo').attr("disabled", "disabled");
                        $("input[name=default_value_text]").prop('disabled', 'disabled');
                        $('#txtLength').attr("disabled", "disabled");
                        $("input[name=chkUniqueValue]").prop('disabled', 'disabled');
                        $("input[name=chkValuesRequired]").prop('disabled', 'disabled');
                    }
                    $("#btnSaveAttribute").prop("name", argus[0]);
                    attributesManage.onInit();

                    attributesManage.config.url = attributesManage.config.baseURL + "GetAttributeDetailsByAttributeID";
                    attributesManage.config.data = JSON2.stringify({ attributeId: argus[0], aspxCommonObj: aspxCommonObj() });
                    attributesManage.config.ajaxCallMode = 4;
                    attributesManage.ajaxCall(attributesManage.config);
                    var attValType = $("#ddlTypeValidation").val();
                    $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "");
                    $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
                    break;
                default:
                    break;
            }
        },
        DateDeserialize: function (dateStr) {
            return eval('new' + dateStr.replace(/\//g, ' '));
        },

        RebindAttributesOnLanguageChange:function()
        {
            //Added for rebinding data in language select options
            var aspxCommonInfo = aspxCommonObj();
            if ($("#languageSelect").length > 0) {
                aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            }
            //attributesManage.onInit();
            attributesManage.config.url = attributesManage.config.baseURL + "GetAttributeDetailsByAttributeID";
            attributesManage.config.data = JSON2.stringify({ attributeId: $("#btnSaveAttribute").prop("name"), aspxCommonObj: aspxCommonInfo });
            attributesManage.config.ajaxCallMode = 4;
            attributesManage.ajaxCall(attributesManage.config);
            var attValType = $("#ddlTypeValidation").val();
            $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "");
            $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
        },

        DeleteAttributes: function (tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.DeleteAttribute(argus[0]);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxAttributesManagement, "Sorry! System attribute can not be deleted.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        ConfirmDeleteMultiple: function (attribute_ids, event) {
            if (event) {
                attributesManage.DeleteMultipleAttribute(attribute_ids);
            }
        },

        DeleteMultipleAttribute: function (_attributeIds) {
            this.config.url = this.config.baseURL + "DeleteMultipleAttributesByAttributeID";
            this.config.data = JSON2.stringify({ attributeIds: _attributeIds, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
            return false;
        },

        DeleteAttribute: function (_attributeId) {
            var properties = {
                onComplete: function (e) {
                    attributesManage.ConfirmSingleDelete(_attributeId, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxAttributesManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxAttributesManagement, "Are you sure you want to delete this attribute?") + "</p>", properties);
        },

        ConfirmSingleDelete: function (attribute_id, event) {
            if (event) {
                attributesManage.DeleteSingleAttribute(attribute_id);
            }
            return false;
        },

        DeleteSingleAttribute: function (_attributeId) {
            this.config.url = this.config.baseURL + "DeleteAttributeByAttributeID";
            this.config.data = JSON2.stringify({ attributeId: parseInt(_attributeId), aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        ActivateAttribute: function (_attributeId, _isActive) {
            this.config.url = this.config.baseURL + "UpdateAttributeIsActiveByAttributeID";
            this.config.data = JSON2.stringify({ attributeId: parseInt(_attributeId), aspxCommonObj: aspxCommonObj(), isActive: _isActive });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
            return false;
        },
        DeactiveAttributes: function (tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.ActivateAttribute(argus[0], false);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxAttributesManagement, "Sorry! System attribute can not be deactivated.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        ActiveAttributes: function (tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.ActivateAttribute(argus[0], true);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxAttributesManagement, "Sorry! System attribute can not be activated.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        IsUnique: function (attributeName, attributeId) {
            var attrbuteUniqueObj = {
                AttributeID: attributeId,
                AttributeName: attributeName
            };
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            this.config.url = this.config.baseURL + "CheckUniqueAttributeName";
            this.config.data = JSON2.stringify({ attrbuteUniqueObj: attrbuteUniqueObj, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
            return isUnique;
        },
        SaveAttribute: function (_attributeId, _flag) {
            $('#iferror').hide();
            if (checkForm($("#form1"))) {
                var selectedItemTypeID = '';
                var validateErrorMessage = '';
                var itemSelected = false;
                var isUsedInConfigItem = false;

                var attributeName = $('#txtAttributeName').val();
                if (!attributeName) {
                    validateErrorMessage += 'Please enter attribute name.<br/>';
                } else if (!attributesManage.IsUnique(attributeName, _attributeId)) {
                    validateErrorMessage += "'" + getLocale(AspxAttributesManagement, "Please enter unique attribute name") + "'" + attributeName.trim() + "'" + getLocale(AspxAttributesManagement, "already exists.") + '<br/>';
                }
                var selectedValue = $("#ddlApplyTo").val();
                if (selectedValue !== "0") {
                    $("#lstItemType").each(function () {
                        if ($("#lstItemType :selected").length != 0) {
                            itemSelected = true;
                            $("#lstItemType option:selected").each(function (i) {
                                selectedItemTypeID += $(this).val() + ',';
                                if ($(this).val() == '3') {
                                    isUsedInConfigItem = true;
                                }
                            });
                        }
                    });
                    if (!itemSelected) {
                        validateErrorMessage += getLocale(AspxAttributesManagement, "Please select at least one item type.") + "<br/>";
                    }
                } else {
                    isUsedInConfigItem = true;
                    $("#lstItemType option").each(function (i) {
                        selectedItemTypeID += $(this).val() + ',';
                    });
                }

                selectedItemTypeID = selectedItemTypeID.substring(0, selectedItemTypeID.length - 1);

                if ($('#toggleElement').is(':checked'))
                    var _Length = '';
                if (!($('#txtLength').is(':disabled'))) {
                    _Length = $('#txtLength').val();
                }
                var selectedVal = $("#ddlAttributeType :selected").val();
                var _saveOptions = '';
                if (selectedVal == 5 || selectedVal == 6 || selectedVal == 9 || selectedVal == 10 || selectedVal == 11 || selectedVal == 12) {
                    $("#dataTable").find("tr input").each(function (i) {
                        $(this).parent('td').find('span').removeClass('error');
                        $(this).removeClass('error');
                        var optionsText = $(this).val();
                        if ($(this).hasClass("class-text")) {
                            if (!optionsText && $(this).prop("name") != "Alias") {
                                validateErrorMessage = getLocale(AspxAttributesManagement, "Please enter all option values and display order for your attribute.") + "<br/>";
                                $(this).parent('td').find('span').addClass('error').show();
                                attributesManage.SetFirstTabActive();
                                $(this).addClass('error');
                                $(this).focus();
                            } else {
                                if ($(this).prop("name") == "position") {
                                    var value = optionsText.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                                    var intRegex = /^\d+$/;
                                    if (!intRegex.test(value)) {
                                        validateErrorMessage = getLocale(AspxAttributesManagement, "Display order is numeric value.") + '<br/>';
                                        $(this).parent('td').find('span').addClass('error').show();
                                        attributesManage.SetFirstTabActive();
                                        $(this).addClass('error');
                                        $(this).focus();
                                    }
                                }
                                _saveOptions += optionsText + "#!#";
                            }
                        } else if ($(this).hasClass("class-isdefault")) {
                            var _IsChecked = $(this).prop('checked');
                            _saveOptions += _IsChecked + "!#!";
                        }
                    });
                }
                _saveOptions = _saveOptions.substring(0, _saveOptions.length - 3);
                if (!validateErrorMessage) {
                    var aspxCommonInfo = aspxCommonObj();
                    var _StoreID = aspxCommonInfo.StoreID;
                    var _PortalID = aspxCommonInfo.PortalID;
                    var _CultureName = $(".languageSelected").attr("value");
                    var _UserName = aspxCommonInfo.UserName;

                    var _attributeName = $('#txtAttributeName').val();
                    var _inputTypeID = $('#ddlAttributeType').val();

                    var selectedAttributeType = $("#ddlAttributeType :selected").val();
                    var _DefaultValue = "";
                    switch (selectedAttributeType) {
                        case "1":
                            _DefaultValue = $("#default_value_text").val();
                            break;
                        case "2":
                            _DefaultValue = $("textarea#default_value_textarea").val();
                            break;
                        case "3":
                            _DefaultValue = $("#default_value_date").val();
                            break;
                        case "4":
                            _DefaultValue = $("#default_value_yesno").val();
                            break;
                        case "8":
                            _DefaultValue = $("#default_value_text").val();
                            break;
                        default:
                            _DefaultValue = '';
                    }

                    var _ValidationTypeID = $('#ddlTypeValidation').val();
                    var _AliasName = $('#txtAliasName').val();
                    var _AliasToolTip = $('#txtAliasToolTip').val();
                    var _AliasHelp = $('#txtAliasHelp').val();
                    var _DisplayOrder = $('#txtDisplayOrder').val();

                    var _IsUnique = $('input[name=chkUniqueValue]').prop('checked');
                    var _IsRequired = $('input[name=chkValuesRequired]').prop('checked');
                    var _IsEnableEditor = $('input[name=chkIsEnableEditor]').prop('checked');
                    var _ShowInAdvanceSearch = $('input[name=chkUseInAdvancedSearch]').prop('checked');
                    var _ShowInComparison = $('input[name=chkComparable]').prop('checked');
                    var _IsUseInFilter = $('input[name=chkIsUseInFilter]').prop('checked'); var _IsIncludeInPriceRule = $('input[name=chkUseForPriceRule]').prop('checked');
                    var _IsShowInItemListing = $('input[name=chkShowInItemListing]').prop('checked');
                    var _IsShowInItemDetail = $('input[name=chkShowInItemDetail]').prop('checked');
                    var _IsActive = $('input[name=chkActive]').prop('checked');
                    var _IsModified = true;
                    var _attributeValueId = arrAttrValueId;
                    var _ItemTypes = selectedItemTypeID;
                    var _Flag = _flag;
                    var _IsUsedInConfigItem = isUsedInConfigItem;

                    attributesManage.AddAttributeInfo(_attributeId, _attributeName, _inputTypeID, _DefaultValue,
                        _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder, _IsUnique, _IsRequired,
                        _IsEnableEditor, _ShowInAdvanceSearch, _ShowInComparison, _IsUseInFilter,
                        _IsIncludeInPriceRule, _IsShowInItemListing, _IsShowInItemDetail, _StoreID, _PortalID, _IsActive, _IsModified, _UserName,
                        _CultureName, _ItemTypes, _Flag, _IsUsedInConfigItem, _saveOptions, _attributeValueId);

                    return false;
                }
            }
        },

        AddAttributeInfo: function (_attributeId, _attributeName, _inputTypeID, _DefaultValue,
            _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder,
            _IsUnique, _IsRequired, _IsEnableEditor,
            _ShowInAdvanceSearch, _ShowInComparison, _IsUseInFilter, _IsIncludeInPriceRule, _IsShowInItemListing, _IsShowInItemDetail,
            _storeId, _portalId, _IsActive, _IsModified, _userName, _CultureName, _ItemTypes, _flag, _isUsedInConfigItem, _saveOptions, _attributeValueId) {

            var info = {
                AttributeID: parseInt(_attributeId),
                AttributeName: _attributeName,
                InputTypeID: _inputTypeID,
                DefaultValue: _DefaultValue,
                ValidationTypeID: _ValidationTypeID,
                Length: _Length >= 0 ? _Length : null,
                AliasName: _AliasName,
                AliasToolTip: _AliasToolTip,
                AliasHelp: _AliasHelp,
                DisplayOrder: _DisplayOrder,
                IsUnique: _IsUnique,
                IsRequired: _IsRequired,
                IsEnableEditor: _IsEnableEditor,
                ShowInAdvanceSearch: _ShowInAdvanceSearch,
                ShowInComparison: _ShowInComparison,
                IsIncludeInPriceRule: _IsIncludeInPriceRule,
                IsShowInItemListing: _IsShowInItemListing,
                IsShowInItemDetail: _IsShowInItemDetail,
                IsUseInFilter: _IsUseInFilter,
                StoreID: _storeId,
                PortalID: _portalId,
                IsActive: _IsActive,
                IsModified: _IsModified,
                UpdatedBy: _userName,
                AddedBy: _userName,
                CultureName: _CultureName,
                ItemTypes: _ItemTypes,
                Flag: _flag,
                IsUsedInConfigItem: _isUsedInConfigItem,
                SaveOptions: _saveOptions,
                AttributeValueID: _attributeValueId
            };

            this.config.url = this.config.baseURL + "SaveUpdateAttribute";
            this.config.data = JSON2.stringify({ attributeInfo: info });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
            return false;
        },
        BindAttributesInputType: function () {
            this.config.url = this.config.baseURL + "GetAttributesInputTypeList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindAttributesValidationType: function () {
            this.config.url = this.config.baseURL + "GetAttributesValidationTypeList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        BindAttributesItemType: function () {
            this.config.url = this.config.baseURL + "GetAttributesItemTypeList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        SearchAttributeName: function () {
            var attributeNm = $.trim($("#txtSearchAttributeName").val());
            var required = $.trim($('#ddlIsRequired').val()) == "" ? null : $.trim($('#ddlIsRequired').val()) == "True" ? true : false;
            var SearchComparable = $.trim($("#ddlComparable").val()) == "" ? null : $.trim($("#ddlComparable").val()) == "True" ? true : false;
            var isSystem = $.trim($("#ddlIsSystem").val()) == "" ? null : $.trim($("#ddlIsSystem").val()) == "True" ? true : false;
            if (attributeNm.length < 1) {
                attributeNm = null;
            }
            attributesManage.BindAttributeGrid(attributeNm, required, SearchComparable, isSystem);
        },
        ajaxSuccess: function (msg) {
            switch (attributesManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $("#ddlAttributeType").get(0).options.length = 0;
                    $.each(msg.d, function (index, item) {
                        $("#ddlAttributeType").get(0).options[$("#ddlAttributeType").get(0).options.length] = new Option(item.InputType, item.InputTypeID);
                    });
                    break
                case 2:
                    $.each(msg.d, function (index, item) {
                        $("#ddlTypeValidation").get(0).options[$("#ddlTypeValidation").get(0).options.length] = new Option(item.ValidationType, item.ValidationTypeID);
                    });
                    break;
                case 3:
                    $('#lstItemType').get(0).options.length = 0;
                    $('#lstItemType').prop('multiple', 'multiple');
                    $('#lstItemType').prop('size', '5');
                    $.each(msg.d, function (index, item) {
                        $("#lstItemType").get(0).options[$("#lstItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                    });
                    break;
                case 4:
                    attributesManage.FillForm(msg);
                    $('#divAttribGrid').hide();
                    $('#divAttribForm').show();
                    break;
                case 5:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute has been deleted successfully.') + "</p>");
                    $('#divAttribForm').hide();
                    $('#divAttribGrid').show();
                    break;
                case 6:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Selected attribute(s) has been deleted successfully.') + "</p>");
                    break;
                case 7:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    break;
                case 8:
                    isUnique = msg.d;
                    break;
                case 9:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    $('#divAttribGrid').show();
                    if (editFlag > 0) {
                        csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute has been updated successfully.') + "</p>");
                    } else {
                        csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute has been saved successfully.') + "</p>");
                    }
                    attributesManage.ClearForm();
                    $('#divAttribForm').hide();
                    break;
            }
        },
        ajaxFailure: function (msg) {
            switch (attributesManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to load attributes input type.") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to load validation type.") + '</p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to load attributes item type.") + '</p>');
                    break;
                case 4:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to update attributes.") + '</p>');
                    break;
                case 5:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to delete attribute.") + '</p>');
                    break;
                case 6:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to delete attributes.") + '</p>');
                    break;
                case 7:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to operate.") + '</p>');
                    break;
                case 8:
                    break;
                case 9:
                    csscody.error('<h2>' + getLocale(AspxAttributesManagement, "Error Message") + '</h2><p>' + getLocale(AspxAttributesManagement, "Failed to save attribute.") + '</p>');
                    break;
            }
        },
        init: function (config) {
            attributesManage.LoadAttributeStaticImage();
            attributesManage.BindAttributeGrid(null, null, null, null);
            $('#divAttribForm').hide();
            $('#divAttribGrid').show();
            attributesManage.BindAttributesInputType();
            attributesManage.BindAttributesValidationType();
            attributesManage.BindAttributesItemType();
            $('.itemTypes').hide();
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");
                if ($("#btnSaveAttribute").prop("name") != "") {
                    attributesManage.RebindAttributesOnLanguageChange();
                }

            });
            $('#ddlApplyTo').change(function () {
                var selectedValue = $(this).val();
                if (selectedValue !== "0") {
                    $('.itemTypes').show();
                } else {
                    $('.itemTypes').hide();
                }
            });

            $('#btnDeleteSelected').click(function () {
                var attribute_ids = '';
                attribute_ids = SageData.Get("gdvAttributes").Arr.join(',');
                if (attribute_ids.length > 0) {
                    var properties = {
                        onComplete: function (e) {
                            attributesManage.ConfirmDeleteMultiple(attribute_ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxAttributesManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Are you sure you want to delete selected attribute(s)?') + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h2><p>' + getLocale(AspxAttributesManagement, "Please select at least one attribute before delete.") + '</p>');
                }
            });

            $('#btnAddNew').bind("click", function () {
                $('#divAttribGrid').hide();
                $('#divAttribForm').show();
                attributesManage.ClearForm();
            });

            $('#btnBack').bind("click", function () {
                $('#divAttribForm').hide();
                $('#divAttribGrid').show();
                attributesManage.ClearForm();
            });

            $('#btnReset').bind("click", function () {
                attributesManage.ClearForm();
            });

            $('#btnSaveAttribute').click(function () {
                var attribute_id = $(this).prop("name");
                if (attribute_id != '') {
                    editFlag = attribute_id;
                    attributesManage.SaveAttribute(attribute_id, false);
                } else {
                    editFlag = 0;
                    attributesManage.SaveAttribute(0, true);
                }
            });

            $('#txtAttributeName').blur(function () {
                var errors = '';
                var attributeName = $(this).val();
                var attribute_id = $('#btnSaveAttribute').prop("name");
                if (attribute_id == '') {
                    attribute_id = 0;
                }
                if (!attributeName) {

                    errors += getLocale(AspxAttributesManagement, "Please enter attribute name");
                }
                else if (!attributesManage.IsUnique(attributeName, attribute_id)) {
                    errors += "'" + getLocale(AspxAttributesManagement, "Please enter Please enter unique attribute name") + "'" + attributeName.trim() + "'" + getLocale(AspxAttributesManagement, "already exists.") + '<br/>';
                }

                if (errors) {
                    $('.cssClassRight').hide();
                    $('.cssClassError').show();
                    $(".cssClassError").parent('div').addClass("diverror");
                    $('.cssClassError').prevAll("input:first").addClass("error");
                    $('.cssClassError').html(errors);
                    return false;
                } else {
                    $(this).parent("td").find("span.error").hide();
                    $('.cssClassRight').show();
                    $('.cssClassError').hide();
                    $(".cssClassError").parent('div').removeClass("diverror");
                    $('.cssClassError').prevAll("input:first").removeClass("error");
                }
            });

            $(".delbutton").click(function () {
                var attribute_id = $(this).prop("id").replace(/[^0-9]/gi, '');
                attributesManage.DeleteAttribute(attribute_id);
            });

            $("td.required input, td select").focusout(function () {
                $tdParent = $(this).parent();
                if ($tdParent.find('.cssClassRequired')) {
                    if ($(this).val() != '' && $(this).val() != '0') {
                        $tdParent.find('.cssClassRequired').hide();
                    }
                    else {
                        $tdParent.find('.cssClassRequired').show();
                    }
                }
            });

            $("#ddlAttributeType").bind("change", function () {
                $('.class-text').removeClass('error').next('span').removeClass('error');
                $("#default_value_text").prop("class", "sfInputbox");
                $('#ddlTypeValidation').val('8');
                $('#iferror').html('');
                $('#iferror').hide();
                if ($(this).val() == 1 || $(this).val() == 2 || $(this).val() == 3 || $(this).val() == 7) {
                    $("input[name=chkValuesRequired]").prop('checked', false).prop("disabled", false);
                } else {
                    $("input[name=chkValuesRequired]").prop('checked', false).prop("disabled", true);
                }
                $("#dataTable tr:gt(1)").remove();
                $("#dataTable>tbody tr").find("input:not(:last)").each(function (i) {
                    if (this.name == "optionValueId") {
                        if ($(this).val() == '') {
                            $(this).val('0');
                        }
                    } else if (this.name == "value") {
                        $(this).val('');
                    } else if (this.name == "position") {
                        $(this).val('');
                    } else if ($(this).hasClass("class-isdefault")) {
                        this.checked = false;
                    }

                });

                attributesManage.ValidationTypeEnableDisable("", true);
                if ($(this).val() == 10) {
                    $("#dataTable .tddefault").find('input[name=defaultRdo]').prop('checked', true);
                }
                var attValType = $("#ddlTypeValidation").val();
                $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "");
                $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
            });

            $("#ddlTypeValidation").bind("change", function () {
                var attValType = $("#ddlTypeValidation").val();
                $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "").val('');
                $('#iferror').hide();
                $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
            });

            $("input[type=button].AddOption").click(function () {
                var checkedState = false;
                if ($(this).prop("name") == "DeleteOption") {
                    var t = $(this).closest('tr');

                    var attrId = t.find("td").find('input[type="hidden"]').val();
                    if (attrId != '0') {
                        arrAttrValueId += attrId + ',';

                    }
                    t.find("td")
                        .wrapInner("<div style='DISPLAY: block'/>")
                        .parent().find("td div")
                        .slideUp(300, function () {
                            t.remove();
                        });


                } else if ($(this).prop("name") == "AddMore") {
                    checkedState = $('#dataTable>tbody tr:first').find('input[type="radio"]').prop("checked");
                    var cloneRow = $(this).closest('tr').clone(true);
                    $(cloneRow).find("input").each(function (i) {
                        if (this.name == "optionValueId") {
                            $(this).val('0');
                        } else if (this.name == "value") {
                            $(this).val('');
                        } else if (this.name == "position") {
                            $(this).val('');
                        } else if (this.name == "Alias") {
                            $(this).val('');
                        } else if ($(this).hasClass("class-isdefault")) {
                            this.checked = false;
                        } else if ($(this).hasClass("AddOption")) {
                            $(this).prop("name", "DeleteOption");
                            $(this).prop("value", "Delete Option");
                        }
                        $(this).parent('td').find('span').removeClass('error');
                        $(this).removeClass('error');
                    });
                    $(cloneRow).appendTo("#dataTable");
                    $('#dataTable>tbody tr:first').find('input[type="radio"]').prop("checked", checkedState);
                    $('#dataTable tr:last').hide();
                    $('#dataTable tr:last td').fadeIn('slow');
                    $('#dataTable tr:last').show();
                    $('#dataTable tr:last td').show();
                }
            });
            $("#btnSearchAttribute").bind("click", function () {
                attributesManage.SearchAttributeName();
            });

            $('#txtSearchAttributeName,#ddlIsRequired,#ddlComparable,#ddlIsSystem').keyup(function (event) {
                if (event.keyCode == 13) {
                    $("#btnSearchAttribute").click();
                }
            });
        }
    };
    attributesManage.init();
});