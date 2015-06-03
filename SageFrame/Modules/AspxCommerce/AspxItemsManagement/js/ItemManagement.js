var listImages = new Array();
var itemTypeId = '';
var currencyCodeSlected = '';
var currencyCodeEdit = '';
var primaryCode = currencyCode;
var ImageList = '';
var notificationItemID;
try {
    notificationItemID = strDecrypt(getParameterByName("itemid"));
} catch (e) {

}
function AddMoreVariantOptions(obj) {
    if ($(obj).parents('tr:eq(0)').find(".ddlCostVariantsCollection").find("option:selected").val() != 0) {
        if ($(obj).parents('tr:eq(0)').find(".ddlCostVariantsCollection").find("option").length > 2) {
            var variantOptIndex = $(obj).closest("tr")[0].rowIndex;
            var parentTable = $(obj).parents('table:eq(0)');

            var trhtml = '<tr id="variantOptValue_' + variantOptIndex + '">' + $(obj).parents('tr:eq(0)').html() + "</tr>";
            var optionValue = $.trim($(obj).parents('tr:eq(0)').find(".ddlCostVariantsCollection").val());
            $(obj).parents('tr:eq(0)').find(".ddlCostVariantsCollection").prop("disabled", "disabled"); parentTable.find('td').find("a.cssClassCvAddMore").remove();
            $(trhtml).appendTo(parentTable).find(".ddlCostVariantsCollection").find("option[value=" + optionValue + "]").remove();
            parentTable.find(".ddlCostVariantValues:last").html('<option value="0">' + getLocale(AspxItemsManagement, "No values") + '</option>');
            parentTable.find('td').find("a.cssClassCvClose").show();

        } else {
            csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Store does not have more cost variants") + '</p>');
        }
    } else {
        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select cost variants") + '</p>');
    }
}

function CloseCombinationRow(obj) {
    var properties = {
        onComplete: function (e) {
            if (e) {
                var parentTable = $(obj).parents('table:eq(0)');
                if (parentTable.find('tr').length > 2) {
                    if ($(obj).parents('tr:eq(0)').find("a.cssClassCvAddMore").length > 0) {
                        var addmore = $(obj).parents('tr:eq(0)').find("a.cssClassCvAddMore");
                        $(obj).parents('tr:eq(0)').prev('tr').find(".tdCostVariant").append(addmore);

                        $(obj).parents('tr:eq(0)').prev('tr').find(".ddlCostVariantsCollection").removeAttr("disabled").prop("enabled", "enabled");
                        $(obj).parents('tr:eq(0)').remove();
                        if (parentTable.find('tr').length <= 2) {
                            parentTable.find('td').find("a.cssClassCvClose").hide();
                        }
                    } else {
                        if ($(obj).closest("tr")[0].rowIndex == 1) {

                            var $dvDropdown = $(obj).parents('tr:eq(0)').find(".ddlCostVariantsCollection");
                            var val = $(obj).parents('tr:eq(0)').next('tr:eq(0)').find(".ddlCostVariantsCollection").find("option:selected").val();
                            $(obj).parents('tr:eq(0)').next('tr:eq(0)').find(".ddlCostVariantsCollection").html($dvDropdown.html()).find("option[value=" + val + "]").prop('selected', 'selected');
                            $(obj).parents('tr:eq(0)').remove();
                            if (parentTable.find('tr').length <= 2) {
                                parentTable.find('td').find("a.cssClassCvClose").hide();
                            }
                        } else {
                            $(obj).parents('tr:eq(0)').remove();
                            if (parentTable.find('tr').length <= 2) {
                                parentTable.find('td').find("a.cssClassCvClose").hide();
                            }
                        }
                    }
                } else {
                    parentTable.find('td').find("a.cssClassCvClose").hide();
                }
                return false;
            }
        }
    };
    csscody.confirm("<h2>" + getLocale(AspxItemsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxItemsManagement, 'Are you sure you want to delete this Cost Variant Combination?') + "</p>", properties);

}

function CloseMainCombinationList(obj) {

    var parentTable = $(obj).parents('table:eq(0)');

    if (parentTable.find('>tbody>tr').length > 1) {
        listImages.splice($(obj).closest("tr")[0].rowIndex - 1, 1);
        var addmore = $(obj).parents('tr:eq(0)').find("button.cssclassAddCVariants");
        $(obj).parents('tr:eq(0)').prev('tr').find(".addButton").append(addmore);
        $(obj).parents('tr:eq(0)').remove();



        if (parentTable.find('>tbody>tr').length <= 1) {
            parentTable.find('>tbody>tr>td').find("a.cssClassCvCloseMain").hide();
        }
        else {
            if ($(obj).closest("tr")[0].rowIndex == 1) {
                $(obj).parents('tr:eq(0)').remove();
            } else {
                $(obj).parents('tr:eq(0)').remove();
            }
        }
    }
    else {
        AddCombinationListRow(obj);
        $(obj).parents('tr:eq(0)').remove();
    }
    $("#dvCvForm table:first").find(">tbody>tr").find(".cssClassDisplayOrder").each(function (index, i) {
        $(this).val(index + 1);
    });
    return false;
}

function AddCombinationListRow(obj) {
    var variantIndex = $(".cssclassAddCVariants:last").closest("tr")[0].rowIndex + 1;
    var parentTable = $(obj).parents('table:eq(0)');
    var trhtml = '<tr id="variantValue_' + variantIndex + '">' + $(obj).parents('tr:eq(0)').html() + '</tr>';
    parentTable.find('td').find("button.cssclassAddCVariants").remove();
    $("#dvCvForm table:first").append(trhtml);
    $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find("table tr:gt(1)").remove(); $(".cssClassTableCostVariant:last").find(".ddlCostVariantsCollection").removeAttr("disabled").prop("enabled", "enabled");
    $(".cssClassTableCostVariant:last").find(".ddlCostVariantsCollection").removeAttr("disabled").prop("enabled", "enabled");
    $(".cssClassTableCostVariant:last").find(".ddlCostVariantValues").html("<option value='0'>" + getLocale(AspxItemsManagement, "No values") + "</option>").removeAttr("disabled").prop("enabled", "enabled");
    $("#dvCvForm table:first").find(">tbody>tr:last").find(".cssClassDisplayOrder").val(variantIndex);
    var addMoreLen = $(".cssClassTableCostVariant:last").find(".tdCostVariant").find("a.cssClassCvAddMore").length;
    if (addMoreLen > 0) {

    } else {
        $(".cssClassTableCostVariant:last").find(".tdCostVariant").append("<a href=\"#variantValue_" + variantIndex + "\" class=\"cssClassCvAddMore\" onclick=\"AddMoreVariantOptions(this); return false;\">" + getLocale(AspxItemsManagement, "Add More") + "</a>");
    }

    $("#dvCvForm table:first>tbody>tr").find("a.cssClassCvCloseMain").show();
}

CheckContains = function (checkon, toCheck) {
    if (checkon != null) {
        var x = checkon.split('@');
        for (var i = 0; i < x.length; i++) {
            if (x[i] == toCheck) {
                return true;
            }
        }
    }
    return false;
};

var ItemMangement = {};

var DynamicAttribute = function () {
    var attributeId = 0;
    currentConfig = {
        ItemID: 0,
        GroupID: 0,
        ItemTypeId: 0,
        AttributeSetID: parseInt($("#ddlAttributeSet").val())
    };
    addEvent = function (id) {
        currentConfig.GroupID = id;
        currentConfig.ItemTypeId = ItemMangement.vars.itemTypeId
        ShowPopupControl('popuprel12');
        //Copy the langauge flag to the attribute setting
        $('.LanguageFlag').html('');
        $('.LanguageFlag').html($('.languageSelected').html());
        //end of copy
        var grpID = $("#ddlItemType").val();
        $('#ddlApplyTo').val(1).trigger('change');
        $("#lstItemType").val(grpID);
    };
    set = function (config) {
        currentConfig = config;
    };
    get = function () {
        currentConfig.AttributeSetID = parseInt($("#ddlAttributeSet").val());
        return currentConfig;
    }
    process = function (attrInfo) {
        $('#fade, #popuprel2,  #popuprel12').fadeOut();
        var html = ItemMangement.GetAttributeHtml(0, $("#ddlItemType").val(), attrInfo.AttributeID, attrInfo.AttributeName, attrInfo.InputTypeID, attrInfo.InputTypeValues != "" ? eval(attrInfo.InputTypeValues) : '', attrInfo.DefaultValue, attrInfo.ToolTip, attrInfo.Length, attrInfo.ValidationTypeID, attrInfo.IsEnableEditor, attrInfo.IsUnique, attrInfo.IsRequired, attrInfo.GroupID, attrInfo.IsIncludeInPriceRule, attrInfo.DisplayOrder);

        var tr = "<tr>";
        tr += "<td><label class='cssClassLabel' >" + attrInfo.AttributeName + "</label></td>";
        tr += "<td class='cssClassTableRightCol'><div></div> </td></tr>";

        $("#" + currentConfig.GroupID).find("table:first>tbody").append(html);

    }

    return { Set: set, AddAttr: addEvent, Get: get, Process: process };
}();
$(function () {




    var progressTime = null;
    var progress = 0;
    var pcount = 0;
    var percentageInterval = [10, 20, 30, 40, 60, 80, 100];
    var timeInterval = [1, 2, 4, 2, 1, 5, 1];
    var DatePickerIDs = new Array();
    var FileUploaderIDs = new Array();
    var htmlEditorIDs = new Array();
    var editorList = new Array();
    var rowCount = 0;
    var contents = '';
    var isSaved = false;
    var FormCount = new Array();
    var itemEditFlag = 0;
    var productUrl = '';
    var serviceBit = false;
    var showPopup = true;
    var dynamicItemId;
    var dynamicItemSKU;
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };

    function getObjects(obj, key) {
        var objects = [];
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                objects.push(obj[i][key]);
            }
        }
        return objects;
    }

    function removeDuplicateCombination(arrlist) {
        var costVariantCombinationList = [];
        costVariantCombinationList = arrlist.reverse();
        var arr = getObjects(arrlist, 'CombinationType');
        var arr2 = getObjects(arrlist, 'CombinationValues');
        var i = arr.length, j, val;
        if (i > 1) {
            while (i--) {
                val = arr[i];
                var combType = val.split('@');
                var combTypeValues = arr2[i].split('@');
                j = i;
                while (j--) {
                    var combType2 = arr[j].split('@');
                    var combType2Value = arr2[j].split('@');
                    if (combType.length == combType2.length) {
                        var matchedtype = 0;
                        var matchedValues = 0;
                        for (var z = 0; z < combType.length; z++) {
                            if (CheckContains(val, combType2[z])) {
                                matchedtype++;
                            }
                            if (matchedtype == combType2.length) {

                                for (var y = 0; y < combType2.length; y++) {
                                    if (CheckContains(arr2[i], combType2Value[y])) {
                                        matchedValues++;
                                    }
                                    if (matchedValues == combType2.length) {
                                        arr.splice(j, 1);
                                        arr2.splice(j, 1);
                                        arrlist.splice(j, 1);
                                        costVariantCombinationList = [];
                                        costVariantCombinationList = arrlist;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return costVariantCombinationList;
    }

    var quickNavigation = "";
    var $accordian;
    var p = $.parseJSON(ItemTabSetting);
    var relCheckedItemID = "";
    var upCheckedItemID = "";
    var crossCheckedItemID = "";
    ItemMangement = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath + 'AspxCoreHandler.ashx/',
            method: "",
            url: ""
        },
        vars: {
            isUnique: false,
            itemId: 0,
            sku: '',
            itemCostVariantId: 0,
            costVariantId: 0,
            isItemHasCostVariant: false,
            attributeSetId: 1,
            itemTypeId: 1,
            showDeleteBtn: false,
            arrRoles: new Array(),
            parentRow: ''
        },

        GetAttributeHtml: function (itemId, itemTypeId, attID, attName, attType, attTypeValue, attDefVal, attToolTip, attLen, attValType, isEditor, isUnique, isRequired, groupId, isIncludeInPriceRule, displayOrder) {

            var retString = '';

            retString += '<tr><td><label class="cssClassLabel">' + attName + ': </label></td>';

            switch (attType) {
                case 1:
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/>';
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                    break;
                case 2: var editorDiv = '';
                    if (isEditor) {
                        htmlEditorIDs[htmlEditorIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + "_editor";
                        editorDiv = '<div id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_editor"></div>';
                    }
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><textarea id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" ' + ((isEditor == true) ? ' style="display: none !important;" ' : '') + ' rows="' + attLen + '"  class="cssClassTextArea dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + ItemMangement.GetValidationTypeErrorMessage(attValType) + '">' + attDefVal + '</textarea>' + editorDiv + '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 3: DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder;

                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox hasDatepicker dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                    break;
                case 4: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + (isRequired == true ? "required" : "") + '">';
                    if (attDefVal == 1) {
                        retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '" checked="checked"/>';
                    }
                    else {
                        retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/>';
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 5: retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" size="' + attLen + '" class="cssClassMultiSelect dynFormItem" multiple>';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                            }
                            else {
                                retString += '<option value="' + val.value + '">' + val.text + '</option>';
                            }
                        }
                    }
                    retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 6:
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" class="sfListmenu dynFormItem">';

                    for (var i = 0; i < attTypeValue.length; i++) {
                        var val = attTypeValue[i];
                        if (val.isdefault == 1) {
                            retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                        }
                        else {
                            retString += '<option value="' + val.value + '">' + val.text + '</option>';
                        }
                    }
                    retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                    break;
                case 7:
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                    break;
                case 8: FileUploaderIDs[FileUploaderIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder;
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><div class="' + attDefVal + '" name="Upload/temp" lang="' + attLen + '"><input type="hidden" id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_hidden" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_hidden" value="" class="cssClassBrowse dynFormItem"/>';
                    retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="file" class="cssClassBrowse dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '" />';
                    retString += ' <span class="response"></span></div><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 9: if (attTypeValue.length > 0) {
                        retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                            }
                        }
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    break;
                case 10: retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">'
                    for (var i = 0; i < attTypeValue.length; i++) {
                        var val = attTypeValue[i];
                        if (val.isdefault == 1) {
                            retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                        }
                        else {
                            retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 11: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '" checked="checked"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '"/><label>' + val.text + '</label>';
                            }
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 12: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                            }
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 13: retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + ' Password" value="' + attDefVal + '" title="' + attToolTip + '"/>'
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                default:
                    break; z
            }
            retString += '</tr>';
            if (allowRealTimeNotifications.toLowerCase() == 'true') {
                UpdateNotifications(1);
            }
            return retString;
        },
        init: function () {

            $('#txtMaxDownload').bind('paste', function (e) {
                e.preventDefault();
            });
            notificationItemID = NotificationItemID;
            var notArgus = [];
            notArgus.push(parseInt(NotificationItemID));
            notArgus.push(1);
            notArgus.push(1);
            notArgus.push(parseInt(NotificationItemTypeID));
            notArgus.push(parseInt(NotificationAttributeSetID));
            notArgus.push(NotificationCurrencyCode);

            var url = window.location.href;
            productUrl = url.substring(0, url.indexOf('Admin'));

            $('#ddlAttributeSet').change(function () {
                ItemMangement.BindItemType();
                var attributeSetId = '';
                var itemTypeId = '';
                attributeSetId = $("#ddlAttributeSet").val();
                itemTypeId = $("#ddlItemType").val();
                $("#spanSample").html("");
                $("#spanActual").html("");
                if (attributeSetId == 3 && itemTypeId == 4) {
                    serviceBit = true;
                } else {
                    serviceBit = false;
                }
                ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);
                if (itemTypeId == 5) {
                    ItemMangement.GroupItems.AttributeSetBind();
                    ItemMangement.GroupItems.ItemTypeBind();
                    GetAllCategory();
                    ItemMangement.GroupItems.BindEvents();
                }
                ItemMangement.BindItemTypeSearch();
                ItemMangement.BindAttributeSetSearch();

            });
            $('#ddlItemType').change(function () {
                var attributeSetId = '';
                var itemTypeId = '';
                attributeSetId = $("#ddlAttributeSet").val();
                itemTypeId = $("#ddlItemType").val();
                $("#spanSample").html("");
                $("#spanActual").html("");
                if (attributeSetId == 3 && itemTypeId == 4) {
                    serviceBit = true;
                } else {
                    serviceBit = false;
                }
                ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);
                if (itemTypeId == 5) {
                    ItemMangement.GroupItems.AttributeSetBind();
                    ItemMangement.GroupItems.ItemTypeBind();
                    GetAllCategory();
                    ItemMangement.GroupItems.BindEvents();
                }
                ItemMangement.BindItemTypeSearch();
                ItemMangement.BindAttributeSetSearch();
            });
            $('#ddlAttributeType').change(function () {
                ItemMangement.HideAllCostVariantImages();
            });
            ItemMangement.LoadItemStaticImage();

            ItemMangement.InitializeVariantTable();

            ItemMangement.BindItemsGrid(null, null, null, null, null, null);
            ItemMangement.BindItemType();
            ItemMangement.BindAttributeSet();
            ItemMangement.BindItemTabSetting();
            if (lowStockItemRss.toLowerCase() == 'true') {
                ItemMangement.LoadLowStockRssImage();
            }
            $("#gdvItems_grid").show();
            $("#gdvItems_form").hide();
            $("#gdvItems_accordin").hide();

            $('#btnDeleteSelected').click(function () {
                AspxCommerce.CheckSessionActive(aspxCommonObj());
                if (AspxCommerce.vars.IsAlive) {
                    var item_ids = '';
                    item_ids = SageData.Get("gdvItems").Arr.join(',');
                    if (item_ids.length > 0) {
                        var properties = {
                            onComplete: function (e) {
                                ItemMangement.ConfirmDeleteMultiple(item_ids, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxItemsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxItemsManagement, 'Are you sure you want to delete the selected item(s)?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select at least one item before delete") + '</p>');
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                }
                return false;
            });

            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

                //codes added for language select combination
                var aspxCommonInfo = aspxCommonObj();
                if ($("#languageSelect").length > 0) {
                    aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
                }
                if (itemId > 0) {
                    ItemMangement.BindDataInAccordin(itemId, attributeSetId, itemTypeId, aspxCommonInfo);
                    //ItemMangement.ResetCVHtml(false, '', aspxCommonInfo);
                    ItemMangement.InitCostVariantCombination($("#ItemMgt_itemID").val(), !($("#ItemMgt_itemID").val() == 0), aspxCommonInfo);
                    ItemMangement.BindDataInImageTab(itemId, aspxCommonInfo);
                    ItemMangement.BindItemQuantityDiscountsByItemID(itemId);

                    if (itemTypeId == 2) {
                        ItemMangement.BindDownloadableForm(itemId);
                    }
                } else {
                    if (itemTypeId != 5) {
                        ItemMangement.InitCostVariantCombination($("#ItemMgt_itemID").val(), !($("#ItemMgt_itemID").val() == 0), aspxCommonInfo);
                        ItemMangement.BindItemQuantityDiscountsByItemID(0);
                        $("#btnSaveQuantityDiscount,#btnDeleteQuantityDiscount").remove();
                    }
                }

                if (itemTypeId == 3) {
                    ItemMangement.GiftCard.Init(aspxCommonInfo);
                }
                else {
                    aspxCommonInfo.UserName = aspxCommonObj().UserName;
                    ItemMangement.CreateCategoryMultiSelect(itemId, aspxCommonInfo);
                }

                if (itemTypeId != 5) {
                    ItemMangement.BindTaxManageRule(aspxCommonInfo);
                }

                if (itemTypeId == 1 || itemTypeId == 2) {
                    ItemMangement.GetAllBrandForItem(aspxCommonInfo);
                    if (itemId > 0) {
                        ItemMangement.GetBrandByItemID(itemId, aspxCommonInfo);
                    }
                }
                if (itemTypeId == 2) {
                    ItemMangement.SampleFileUploader(maxDownloadFileSize);
                    ItemMangement.ActualFileUploader(maxDownloadFileSize);
                }
                if (itemTypeId == 5) {
                    ItemMangement.GroupItems.Get(itemId, null, null, null, null);
                }
                ItemMangement.ImageUploader(maxFileSize);

                $("#ddlCurrency").val(currencyCodeEdit);
                $("#ddlCurrencyLP").val(currencyCodeEdit);
                $("#ddlCurrencyCP").val(currencyCodeEdit);
                $("#ddlCurrencySP").val(currencyCodeEdit);
                $("#ddlCurrencyMP").val(currencyCodeEdit);
                $("#gdvItems_grid").hide();
                $("#gdvItems_form").show();
                $("#gdvItems_accordin").show();
                $("div.popbox").hide();
                $('.popbox').popbox();

                $("#txtDownloadTitle").on("keypress", function (e) {
                    if (e.which == 37 || e.which == 44) {
                        return false;
                    }
                });
                $("#txtMaxDownload").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtDownDisplayOrder").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $('#txtMaxDownload').bind('paste', function (e) {
                    e.preventDefault();
                });


                $(".cssClassDisplayOrder").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $(".cssClassPriceModifier").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                        if (e.which == 45) { return true; }
                        return false;
                    }
                });
                $(".cssclassCostVariantItemQuantity").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $(".cssClassWeightModifier").on("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                        if (e.which == 45) { return true; }
                        return false;
                    }
                });
                $(".cssClassWeightModifier").bind("contextmenu", function (e) {
                    return false;
                });
                $(".cssClassPriceModifier").bind("contextmenu", function (e) {
                    return false;
                });

                $(".cssClassSKU").on("keypress", function (e) {
                    if (e.which == 39 || e.which == 34) {
                        return false;
                    }
                });
                $("#btnSaveCostVariantCombination").off("click").bind("click", function () {

                    var variantsProperties = $("#dvCvForm table:first>tbody>tr>td.cssClassTableCostVariant").find("input.cssClassDisplayOrder,input.cssClassVariantValueName, .ddlCostVariantsCollection, .ddlCostVariantValues");
                    var isEmpty = false;
                    $.each(variantsProperties, function (index, item) {
                        if ($.trim($(this).val()) == 0) {
                            csscody.alert("<h2>" + getLocale(AspxItemsManagement, "Information Alert") + "</h2><p>" + getLocale(AspxItemsManagement, 'Please enter item cost variant properties.') + "</p>");
                            isEmpty = true;
                            return false;
                        }
                    });
                    if (!isEmpty) {
                        ItemMangement.SaveItemCostVariantsInfo('', aspxCommonInfo);
                    }
                    return false;
                });
                $("#btnBackVariantOptions").bind("click", function () {
                    ItemMangement.OnInit();
                    $("#variantsGrid").show();
                    $("#newCostvariants").hide();
                    $('.classAddImages').removeAttr("name");
                    $('.classAddImagesEdit').removeAttr("name").removeAttr("onclick").removeClass("classAddImagesEdit").addClass("classAddImages");
                    return false;
                });
                $("#btnExisingBack").click(function () {
                    $("#variantsGrid").show();
                    $("#newCostvariants").hide();
                    return false;
                });
                $("#btnResetVariantOptions").click(function () {
                    ItemMangement.OnInit();
                    ItemMangement.ClearVariantForm();
                    return false;
                });

                $("#ddlCurrency").change(function () {
                    $("#ddlCurrencyLP option[value=" + $(this).val() + "]").prop("selected", "selected");
                    $("#ddlCurrencyCP option[value=" + $(this).val() + "]").prop("selected", "selected");
                    $("#ddlCurrencySP option[value=" + $(this).val() + "]").prop("selected", "selected");
                    $("#ddlCurrencyMP option[value=" + $(this).val() + "]").prop("selected", "selected");
                    currencyCodeSlected = $(this).val();
                    currencyCodeEdit = currencyCodeSlected;
                    $('#tblQuantityDiscount').find('thead').find('.cssClassUnitPrice').html('').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):');
                    $('#tblGroupPrice').find('.cssClassUnitPrice').html('').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):');
                    ItemMangement.InitCostVariantCombination($("#ItemMgt_itemID").val(), !($("#ItemMgt_itemID").val() == 0), aspxCommonInfo);

                });

                $('#btnApplyExisingOption').click(function () {
                    var variant_Id = $('#ddlExistingOptions').val();
                    var item_Id = $("#ItemMgt_itemID").val();
                    if (variant_Id != null && item_Id != null) {
                        var params = { itemId: item_Id, costVariantID: variant_Id, aspxCommonObj: aspxCommonInfo };
                        var mydata = JSON2.stringify(params);
                        $.ajax({
                            type: "POST", beforeSend: function (request) {
                                request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                request.setRequestHeader("UMID", umi);
                                request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                request.setRequestHeader("PType", "v");
                                request.setRequestHeader('Escape', '0');
                            },
                            url: aspxservicePath + "AspxCoreHandler.ashx/AddItemCostVariant",
                            data: mydata,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                RemovePopUp();
                                ItemMangement.BindItemCostVariantInGrid(item_Id, aspxCommonInfo);
                                ItemMangement.BindCostVariantsOptions(item_Id, aspxCommonInfo);
                                csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item cost variant has been saved successfully.") + '</p>');
                                $("#variantsGrid").show();
                                $('#divExistingVariant,#lblCostVariantOptionTitle').hide();
                            },
                            error: function () {
                                csscody.error('<h1>' + getLocale(AspxItemsManagement, "Error Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Failed to save item cost variant") + '</p>');
                            }
                        });
                    }
                    return false;
                });
                $("#txtDownDisplayOrder,#txtMaxDownload").bind("contextmenu", function (e) {
                    return false;
                });
                $('.open').click(function () {
                    $('.classPriceHistory').show();
                    return false;
                });
                $(".FeaturedDropDown").trigger('change');
                $(".SpecialDropDown").trigger('change');

                return false;

            });
            $('#btnReset').click(function () {
                ItemMangement.ClearForm();
                return false;
            });

            $('#btnContinue').click(function () {


            });
            $("input[name='ItemAttributeTab']").change(function () {
                if ($("#rdbGeneralType").prop("checked")) {
                    $("#divAdvancedContent").hide();
                    $("#divGeneralContent").show();
                    $("#advancedTab").removeClass('sfActive');
                    $("#generalTab").addClass('sfActive');
                }
                if ($("#rdbAdvancedType").prop("checked")) {
                    $("#divGeneralContent").hide();
                    $("#divAdvancedContent").show();
                    $("#generalTab").removeClass('sfActive');
                    $("#advancedTab").addClass('sfActive');

                }
            });
            $('#btnSearchItems').on('click', function () {
                ItemMangement.SearchItems();
                return false;
            });
            $('#txtSearchSKU,#txtSearchName,#ddlSearchItemType,#ddlAttributeSetName,#ddlVisibitity,#ddlIsActive').keyup(function (event) {
                if (event.keyCode == 13) {
                    $('#btnSearchItems').click();
                }
            });

            $("#dynItemForm").on("click", "#btnSearchRelatedItems", function () {
                ItemMangement.SearchRelatedItems();
                return false;
            });

            $('#txtItemSKU,#txtItemName,#ddlSelectItemType,#ddlSelectAttributeSetName').on("keyup", function (event) {
                if (event.keyCode == 13) {
                    $('#btnSearchRelatedItems').click();
                    return false;
                }
            });

            $("#dynItemForm").on("click", "#btnSearchUpSellItems", function () {
                ItemMangement.SearchUpSellItems();
                return false;
            });
            $('#txtItemSKUSell,#txtItemNameSell,#ddlSelectItemTypeSell,#ddlSelectAttributeSetNameSell').on("keyup", function (event) {
                if (event.keyCode == 13) {
                    $('#btnSearchUpSellItems').click();
                    return false;
                }
            });

            $("#dynItemForm").on("click", "#btnSearchCrossSellItems", function () {
                ItemMangement.SearchCrossSellItems();
                return false;
            });
            $('#txtItemSKUcs,#txtItemNamecs,#ddlSelectItemTypecs,#ddlSelectAttributeSetNamecs').on("keyup", function (event) {
                if (event.keyCode == 13) {
                    $('#btnSearchCrossSellItems').click();
                    return false;
                }
            });

            $("#btnAddNew").click(function () {
                $('#spnAddTitle').text("" + getLocale(AspxItemsManagement, 'Add New Item') + "");
                $("#divItemMgrTitle").show();
                $("#btnDelete").hide();
                $("#gdvItems_grid").hide();
                $("#gdvItems_form").show();
                $(".cssClassAttribute").show();
                $("#gdvItems_accordin").hide();
                $("#ddlSearchItemType>option").val(1);
                $("#ddlAttributeSetName>option").val(1);
                primaryCode = currencyCode;
                $('#Todatevalidation').prop('class', '');
                $('#Fromdatevalidation').prop('class', '');
                $("#ItemMgt_itemID").val(0);
                var attributeSetId = '';
                var itemTypeId = '';
                attributeSetId = $("#ddlAttributeSet").val();
                itemTypeId = $("#ddlItemType").val();
                $("#spanSample").html("");
                $("#spanActual").html("");
                if (attributeSetId == 3 && itemTypeId == 4) {
                    serviceBit = true;
                } else {
                    serviceBit = false;
                }
                ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);
                if (itemTypeId == 5) {
                    ItemMangement.GroupItems.AttributeSetBind();
                    ItemMangement.GroupItems.ItemTypeBind();
                    GetAllCategory();
                    ItemMangement.GroupItems.BindEvents();
                }
                ItemMangement.BindItemTypeSearch();
                ItemMangement.BindAttributeSetSearch();
                return false;
            });

            $("#btnAddItemSetting").click(function () {
                $(".sfGridwrapper").hide();
                $("#divItemButtonWrapper").hide();
                $(".cssClassItemSetting").show();
                $(".gdvItems_form").hide();
                return false;
            });

            $("#btnItemSettingBack").click(function () {
                $(".cssClassItemSetting").hide();
                $("#divItemButtonWrapper").show();
                $(".sfGridwrapper").show();
                return false;
            });

            $("#btnItemSettingSave").click(function () {
                ItemMangement.SaveItemTabSetting();
                return false;
            });
            $("#btnBack").click(function () {
                $("#gdvItems_grid").show();
                $("#gdvItems_form").hide();
                $("#gdvItems_accordin").hide();
                return false;
            });

            $('#divItemTabWrapper').on('click', '#btnReturn', function () {
                ItemMangement.BackToItemGrid();
                return false;
            });
            if (notificationItemID > 0) {
                ItemMangement.EditItems("gdvItems", notArgus)
            }

            $('#btnResetForm').click(function () {
                ItemMangement.ClearAttributeForm();
                return false;
            });

            $('#dynItemForm, #divGeneralContent').on('click', '#btnDelete', function () {
                ItemMangement.vars.itemId = $("#ItemMgt_itemID").val();
                ItemMangement.ClickToDelete(ItemMangement.vars.itemId);
                return false;
            });

            $(".cssClassClose").click(function () {
                $('#fade, #popuprel2,  #popuprel12').fadeOut();
                $("#VariantsImagesTable").hide();
                return false;
            });

            $("#btnImageBack").click(function () {
                $('#fade, #popuprel2').fadeOut();
                $("#VariantsImagesTable").hide();
                return false;
            });

            $('#txtCostVariantName').on('blur', function () {
                var errors = '';
                var costVariantName = $(this).val();
                var variant_id = $('#btnSaveItemVariantOption').prop("name");
                if (variant_id == '') {
                    variant_id = 0;
                }
                if (!costVariantName) {
                    errors += getLocale(AspxItemsManagement, "Please enter cost variant name");
                }
                else if (!ItemMangement.IsUniqueCostVariant(costVariantName, variant_id)) {
                    errors += getLocale(AspxItemsManagement, "Please enter unique cost variant name!") + costVariantName.trim() + getLocale(AspxItemsManagement, "already exists.") + '<br/>';
                    $('#txtCostVariantName').val('');
                }

                if (errors) {
                    $('.cssClassCostVarRight').hide();
                    $('.cssClassCostVarError').show();
                    $(".cssClassCostVarError").parent('div').addClass("diverror");
                    $('.cssClassCostVarError').prevAll("input:first").addClass("error");
                    $('.cssClassCostVarError').html(errors);
                    return false;
                } else {
                    $('.cssClassCostVarRight').show();
                    $('.cssClassCostVarError').hide();
                    $(".cssClassCostVarError").parent('div').removeClass("diverror");
                    $('.cssClassCostVarError').prevAll("input:first").removeClass("error");
                }
            });
            ItemMangement.GroupItems.BindEvents();
            $('#btnApplyExisingOption').click(function () {
                var variant_Id = $('#ddlExistingOptions').val();
                var item_Id = $("#ItemMgt_itemID").val();
                if (variant_Id != null && item_Id != null) {
                    var params = { itemId: item_Id, costVariantID: variant_Id, aspxCommonObj: aspxCommonInfo };
                    var mydata = JSON2.stringify(params);
                    $.ajax({
                        type: "POST", beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", umi);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        url: aspxservicePath + "AspxCoreHandler.ashx/AddItemCostVariant",
                        data: mydata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            RemovePopUp();
                            ItemMangement.BindItemCostVariantInGrid(item_Id, aspxCommonInfo);
                            ItemMangement.BindCostVariantsOptions(item_Id, aspxCommonInfo);
                            csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxItemsManagement, 'Cost variant option has been applied successfully.') + "</p>");
                        },
                        error: function () {
                            csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to save item cost variant!") + '</p>');
                        }
                    });
                }
                return false;
            });
        },
        LoadLowStockRssImage: function () {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#lowStockRssImage').parent('a').show();
            $('#lowStockRssImage').parent('a').removeAttr('href').prop('href', pageurl + '?type=rss&action=lowstockitems');
            $('#lowStockRssImage').removeAttr('src').prop('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#lowStockRssImage').removeAttr('title').prop('title', getLocale(AspxItemsManagement, 'Low Stock Items Rss Feed'));
            $('#lowStockRssImage').removeAttr('alt').prop('alt', getLocale(AspxItemsManagement, 'Low Stock Items Rss Feed'));
        },
        ajaxCall: function (config) {
            $.ajax({
                type: ItemMangement.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ItemMangement.config.contentType,
                cache: ItemMangement.config.cache,
                async: ItemMangement.config.async,
                url: ItemMangement.config.url,
                data: ItemMangement.config.data,
                dataType: ItemMangement.config.dataType,
                success: ItemMangement.ajaxSuccess,
                error: ItemMangement.ajaxFailure
            });
        },
        LoadItemStaticImage: function () {
            $('.cssClassAddRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
            $('.cssClassCloneRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
            $('.cssClassDeleteRow').prop('src', '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');
            $('.cssClassSuccessImg').prop('src', '' + aspxTemplateFolderPath + '/images/right.jpg');
        },
        GiftCard: function () {

            var $ajaxCall = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };

            var getGiftCardCategory = function (aspxCommonInfo) {
                var aspxCommonInfo = aspxCommonInfo;
                aspxCommonInfo.UserName = null;
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetAllGiftCardCategory", param, bindGiftCartCategory, null);
            };

            var bindGiftCartCategory = function (data) {
                var options = '';
                // options = '<option value="0" selected="selected" >' + getLocale(AspxItemsManagement, "All") + '</option>';
                $("#ddlGCCategory").html('')
                $.each(data.d, function (index, item) {
                    options += "<option value=" + item.GiftCardCategoryId + ">" + item.GiftCardCategory + "</option>";
                });
                $("#ddlGCCategory").append(options);
                $("#editGCCategory,#ddlGCCategoryImg").html('').append('<option value="0">' + getLocale(AspxItemsManagement, "Choose One") + '</option>' + options);
                if (itemId != 0) {
                    getGiftCardItemCategory();
                }
            };

            var saveGiftCardItemCategory = function (id) {
                var ids = $("#ddlGCCategory").val() == null ? 0 : $("#ddlGCCategory").val().join(',');
                var param = JSON2.stringify({ itemId: id, ids: ids, aspxCommonObj: aspxCommonObj() });
                $ajaxCall("SaveGiftCardItemCategory", param, null, null);
            };
            var getGiftCardItemCategory = function (x, y) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var param = JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetGiftCardItemCategory", param, bindGiftCardItemCategory, null);
            };
            var bindGiftCardItemCategory = function (data) {

                if (data.d != "") {
                    var ids = data.d;
                    if (ids == "0") {
                        $("#ddlGCCategory").find("option[value=0]").prop('selected', 'selected');
                    }
                    else {
                        var id = ids.split(',');
                        $("#ddlGCCategory").find("option[value=0]").removeAttr('selected');
                        for (var i = 0; i < id.length; i++) {
                            $("#ddlGCCategory").find("option[value=" + id[i] + "]").prop('selected', 'selected');
                        }

                    }
                }
            };

            var load = function (aspxCommonInfo) {
                getGiftCardCategory(aspxCommonInfo);
            };
            return {
                Init: load,
                SaveItemCategory: saveGiftCardItemCategory
            };
        } (),
        BindTaxManageRule: function (aspxCommonInfo) {
            var isActive = true;
            this.config.url = this.config.baseURL + "GetAllTaxItemClass";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, isActive: isActive });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindItemQuantityDiscountsByItemID: function (itemId) {
            this.config.url = this.config.baseURL + "GetItemQuantityDiscountsByItemID";
            this.config.data = JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        GetUserInRoleList: function (arrRoles) {
            var aspxCommonInfo = aspxCommonObj();
            delete aspxCommonInfo.StoreID;
            delete aspxCommonInfo.CultureName;
            var IsAll = true;
            this.config.url = this.config.baseURL + "BindRoles";
            this.config.data = JSON2.stringify({ isAll: IsAll, aspxCommonObj: aspxCommonInfo });
            this.vars.arrRoles = arrRoles;
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        BindRolesList: function (item) {
            var RoleInCheckbox = '<input type="checkbox" class="cssClassCheckBox"  value="' + item.RoleID + '" /><label>' + item.RoleName + '</label>&nbsp &nbsp';
            $('.cssClassUsersInRoleCheckBox').append(RoleInCheckbox);
        },
        DeleteItemDiscountQuantity: function () {
            var item_Id = $("#ItemMgt_itemID").val();

            this.config.url = this.config.baseURL + "DeleteItemQuantityDiscount";
            this.config.data = JSON2.stringify({ quantityDiscountID: 0, itemID: item_Id, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 44;
            this.ajaxCall(this.config);
        },
        SaveItemDiscountQuantity: function (itemId) {

            var _DiscountQuantityOptions = '';
            var item_Id = $("#ItemMgt_itemID").val() == 0 ? itemId : $("#ItemMgt_itemID").val();
            var isValid = true;
            $("#tblQuantityDiscount>tbody tr").each(function () {
                _DiscountQuantityOptions += $(this).find(".cssClassQuantityDiscount").val() + ',';
                if ($(this).find(".cssClassQuantity").val() != '') {
                    _DiscountQuantityOptions += $(this).find(".cssClassQuantity").val() + ',';
                } else {
                    isValid = false;
                }
                if ($(this).find(".cssClassPrice").val() != '') {
                    _DiscountQuantityOptions += $(this).find(".cssClassPrice").val() + '%';
                } else {
                    isValid = false;
                }
                var check = $(this).find("input[type='checkbox']:checked");
                if (check.length != 0) {
                    $.each(check, function () {
                        _DiscountQuantityOptions += $(this).val() + ',';
                    });
                    _DiscountQuantityOptions = _DiscountQuantityOptions.substring(0, _DiscountQuantityOptions.length - 1);
                } else {
                    isValid = false;
                }
                _DiscountQuantityOptions += '#';
            });


            if (isValid) {

                this.config.url = this.config.baseURL + "SaveItemDiscountQuantity";
                this.config.data = JSON2.stringify({ discountQuantity: _DiscountQuantityOptions, itemID: item_Id, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            else {
                csscody.alert("'<h2>" + getLocale(AspxItemsManagement, 'Information Alert') + "</h2><p>" + getLocale(AspxItemsManagement, 'Please submit valid data!.') + "</p>'");
            }
        },
        HideAllCostVariantImages: function () {
            var selectedVal = $("#ddlAttributeType").val();
            if (selectedVal == 9 || selectedVal == 11) {
                $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").hide();
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").hide();
            } else {
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").show();
                $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").show();
            }
        },

        InitializeVariantTable: function () {
            $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();

            $("img.cssClassAddRow").on("click", function () {
                var cloneRow = $(this).closest('tr').clone(true);
                $(cloneRow).appendTo("#tblVariantTable");
                $(cloneRow).find("input[type='text']").val('');
                $(cloneRow).find("input[type='hidden']").val('0');
                $(cloneRow).find(".cssClassDeleteRow").show();
                $(cloneRow).find(".classAddImages").removeAttr("name");
                var x = $("#tblVariantTable>tbody>tr").length - 1;
                $(cloneRow).find(".classAddImages").prop("value", x);
                var rid = $("#tblVariantTable>tbody>tr").length - 1;
                $(cloneRow).find(".classAddImagesEdit").removeAttr("name").removeAttr("value");
                $(cloneRow).find(".classAddImagesEdit").prop("value", rid);
                $("#VariantsImagesTable>tbody").html('');
                return false;
            });

            $("img.cssClassCloneRow").on("click", function () {
                var cloneRow = $(this).closest('tr').clone(true);
                $(cloneRow).appendTo("#tblVariantTable");
                $(cloneRow).find("input[type='hidden']").val('0');

                $(cloneRow).find("select.cssClassPriceModifierType").val($(this).closest('tr').find("select.cssClassPriceModifierType").val());
                $(cloneRow).find("select.cssClassWeightModifierType").val($(this).closest('tr').find("select.cssClassWeightModifierType").val());
                $(cloneRow).find("select.cssClassIsActive").val($(this).closest('tr').find("select.cssClassIsActive").val());
                $(cloneRow).find(".cssClassDeleteRow").show();
                return false;
            });

            $("img.cssClassDeleteRow").on("click", function () {
                x--;
                var parentRow = $(this).closest('tr');
                if (parentRow.is(":first-child")) {
                    return false;
                } else {
                    var costVariantValueID = $(parentRow).find("input[type='hidden']").val();
                    if (costVariantValueID > 0) {
                        var item_Id = $("#ItemMgt_itemID").val();
                        ItemMangement.vars.parentRow = $(parentRow);
                        ItemMangement.DeleteItemCostVaraiantValue(costVariantValueID, item_Id, parentRow);
                    } else {
                        $(parentRow).remove();
                    }
                }
                return false;
            });

            $("#gdvItems_accordin").on("click", ".cssClassAddDiscountRow", function () {
                var cloneRow = $(this).closest('tr').clone(true);
                $(cloneRow).appendTo("#tblQuantityDiscount");
                $(cloneRow).find("input[type='text']").val('');
                $(cloneRow).find("input[type='hidden']").val('0');
                $(cloneRow).find("input[type='checkbox']").removeAttr('checked');
                $(cloneRow).find("img.cssClassDeleteDiscountRow").show();
                return false;
            });
            $("#gdvItems_accordin").on("click", ".cssClassCloneDiscountRow", function () {
                var cloneRow = $(this).closest('tr').clone(true);
                $(cloneRow).appendTo("#tblQuantityDiscount");
                $(cloneRow).find("input[type='hidden']").val('0');
                $(cloneRow).find("img.cssClassDeleteDiscountRow").show();
                return false;
            });

            $("#gdvItems_accordin").on("click", ".cssClassDeleteDiscountRow", function () {
                var parentRow = $(this).closest('tr');
                if (parentRow.is(":first-child")) {
                    return false;
                } else {
                    var quantityDiscountID = $(parentRow).find("input[type='hidden']").val();
                    if (quantityDiscountID > 0) {
                        ItemMangement.vars.parentRow = $(parentRow);
                        ItemMangement.DeleteItemQuantityDiscount(quantityDiscountID, parentRow);
                    } else {
                        $(parentRow).remove();
                    }
                }
                return false;
            });
        },

        DeleteItemQuantityDiscount: function (quantityDiscountID, parentRow) {
            var properties = {
                onComplete: function (e) {
                    ItemMangement.ConfirmDeleteItemQuantityDiscount(quantityDiscountID, parentRow, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxItemsManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxItemsManagement, "Are you sure you want to delete this item quatity discount value?") + "</p>", properties);
        },

        ConfirmDeleteItemQuantityDiscount: function (quantityDiscountID, parentRow, event) {
            if (event) {
                var _itemId = $("#ItemMgt_itemID").val();
                this.config.url = this.config.baseURL + "DeleteItemQuantityDiscount";
                this.config.data = JSON2.stringify({ quantityDiscountID: quantityDiscountID, itemID: _itemId, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            }
        },

        DeleteItemCostVaraiantValue: function (costVariantValueID, itemId, parentRow) {
            var properties = {
                onComplete: function (e) {
                    ItemMangement.ConfirmDeleteItemCostVariantValue(costVariantValueID, itemId, parentRow, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxItemsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxItemsManagement, 'Are you sure you want to delete this item cost variant value?') + "</p>", properties);
        },

        ConfirmDeleteItemCostVariantValue: function (costVariantValueID, itemId, parentRow, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteItemCostVariantValue";
                this.config.data = JSON2.stringify({ costVariantValueID: costVariantValueID, itemId: itemId, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            }
            return false;
        },

        BindItemCostVariantInGrid: function (itemId, aspxCommonInfo) {
            // var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.method = "GetItemCostVariants";
            this.config.data = { aspxCommonObj: aspxCommonInfo, itemID: itemId };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvItemCostVariantGrid_pagesize").length > 0) ? $("#gdvItemCostVariantGrid_pagesize :selected").text() : 10;

            $("#gdvItemCostVariantGrid").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxItemsManagement, 'Item Cost Variant ID'), name: 'item_costvariant_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'Cost Variant ID'), name: 'cost_variant_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'Cost Variant Name'), name: 'cost_variant_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],
                buttons: [
                { display: getLocale(AspxItemsManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemMangement.EditItemCostVariant', arguments: '1,2,3' },
                { display: getLocale(AspxItemsManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemMangement.DeleteItemCostVariant', arguments: '1' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false} }
            });
        },

        EditItemCostVariant: function (tblID, argus) {
            $(".cssClassDisplayOrder").on('keypress', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    if (e.which == 45) { return true; }
                    return false;
                }
            });

            $(".cssClassPriceModifier").on('keypress', function (e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    if (e.which == 45) { return true; }
                    return false;
                }
            });

            $(".cssClassWeightModifier").on('keypress', function (e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    if (e.which == 45) { return true; }
                    return false;
                }
            });
            $(".cssClassWeightModifier").on("contextmenu", function (e) {
                return false;
            });

            $(".cssClassPriceModifier").on("contextmenu", function (e) {
                return false;
            });
            switch (tblID) {
                case "gdvItemCostVariantGrid":
                    ItemMangement.ClearVariantForm();
                    $('#ddlAttributeType').html('');
                    ItemMangement.BindCostVariantsInputType();
                    ItemMangement.OnInit();
                    $("#tabFrontDisplay").hide();

                    $("#hdnItemCostVar").val(argus[0]);

                    $("#btnSaveItemVariantOption").prop("name", argus[4]);
                    $("#lblHeading").html(getLocale(AspxItemsManagement, 'Edit Cost variant Option:') + argus[5]);
                    ItemMangement.BindItemCostVariantsDetail(argus[0], argus[3], argus[4]);

                    break;
                default:
                    break;
            }
        },

        BindItemCostVariantsDetail: function (itemCostVariantId, itemId, costVariantID) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetItemCostVariantInfoByCostVariantID";
            this.config.data = JSON2.stringify({ itemCostVariantId: itemCostVariantId, itemId: itemId, costVariantID: costVariantID, aspxCommonObj: aspxCommonInfo });
            this.vars.itemCostVariantId = itemCostVariantId;
            this.vars.itemId = itemId;
            this.vars.costVariantId = costVariantID;
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },

        SubmitForm: function (frmID, attributeSetId, itemTypeId) {
            var itemId = $("#ItemMgt_itemID").val();
            var frm = $("#" + frmID);
            $('.cssClassTextArea').each(function () {
                $(this).val(Encoder.htmlEncode($(this).val()));
            });
            for (var i = 0; i < editorList.length; i++) {
                var id = String(editorList[i].ID);
                var textArea = $("#" + id.replace("_editor", ""));
                textArea.val(Encoder.htmlEncode(editorList[i].Editor.getData()));
            }

            var itemSKUTxtBoxID = $("#hdnSKUTxtBox").val();
            var itemSKU = $("#" + itemSKUTxtBoxID).val();
            var validPrice = false;
            var validActiveDate = false;

            validPrice = ItemMangement.ValidateExtraField("classItemPrice", "classItemListPrice", "price", getLocale(AspxItemsManagement, "List Price should be equal or greater than Price!"));
            validActiveDate = ItemMangement.ValidateExtraField("classActiveFrom", "classActiveTo", "date", getLocale(AspxItemsManagement, "Active To date must be higher date than Active From date!"));




            if ($("#dvCvForm>table").attr('style')) {
                if ($("#dvCvForm>table").is(":hidden") == false) {
                    var variantsProperties = $("#dvCvForm table:first>tbody>tr>td.cssClassTableCostVariant").find("input.cssClassDisplayOrder,input.cssClassVariantValueName, .ddlCostVariantsCollection, .ddlCostVariantValues");
                    var isEmpty = false;
                    $.each(variantsProperties, function (index, item) {
                        if ($.trim($(this).val()) == 0) {
                            isEmpty = true;
                            return false;
                        }
                    });
                    if (isEmpty) {
                        csscody.alert("<h2>" + getLocale(AspxItemsManagement, "Information Alert") + "</h2><p>" + getLocale(AspxItemsManagement, 'Please enter item cost variant properties.') + "</p>");
                        $("a.st_tab[href='#dv_Content_9']").trigger('click');
                        return false;
                    }
                }
            }
            if ($("#tblQuantityDiscount").parents('div.sfFormwrapper:eq(0)').attr('style')) {
                if ($("#tblQuantityDiscount").parents('div.sfFormwrapper:eq(0)').is(":hidden") == false) {
                    var isTPValid = true;
                    $("#tblQuantityDiscount>tbody tr").each(function () {

                        if ($(this).find(".cssClassQuantity").val() != '') {

                        } else {
                            isTPValid = false; return;
                        }
                        if ($(this).find(".cssClassPrice").val() != '') {

                        } else {
                            isTPValid = false; return;
                        }
                        var check = $(this).find("input[type='checkbox']:checked");
                        if (check.length != 0) {
                        } else {
                            isTPValid = false; return;
                        }

                    });
                    if (!isTPValid) {
                        $("a.st_tab[href='#dv_Content_10']").trigger('click');
                        return false;
                    }
                }
            }

            if (checkForm(frm) && ItemMangement.CheckUniqueness(itemSKU, itemId) && validPrice && validActiveDate) {
                ItemMangement.SaveItem("#" + frmID, attributeSetId, itemTypeId, itemId, itemSKU);
            }
            else {
                var errorAccr = $("#st_vertical").find('.diverror:first').parents('div.st_tab_view').find('h4>a').html();
                if (errorAccr != "" || errorAccr != null) {

                    var accrHeading = $("#st_vertical").find('.st_tab');
                    $.each(accrHeading, function (i, item) {
                        if ($.trim($(item).html()) == $.trim(errorAccr)) {
                            $(item).trigger('click');
                            return false;
                        }
                    });
                }
                return false;
            }
        },

        FillForm: function (response) {
            $.each(response.d, function (index, item) {
                $('#txtCostVariantName').val(item.CostVariantName);
                $('#txtCostVariantName').prop('disabled', 'disabled');
                $('#ddlAttributeType').val(item.InputTypeID);
                $('#ddlAttributeType').prop('disabled', 'disabled');
                $('#txtDisplayOrder').val(item.DisplayOrder);
                $("#txtDescription").val(item.Description);
                $("#txtDescription").prop('disabled', 'disabled');
                $('input[name=chkActive]').prop('checked', item.IsActive);
                $('input[name=chkUseInAdvancedSearch]').prop('checked', item.ShowInAdvanceSearch);
                $('input[name=chkComparable]').prop('checked', item.ShowInComparison);
                $('input[name=chkUseForPriceRule]').prop('checked', item.IsIncludeInPriceRule);
                $('input[name=chkIsUseInFilter]').prop('checked', item.IsUseInFilter);
            });
        },

        BindItemCostVariantValueByCostVariantID: function (itemCostVariantId, itemId, costVariantId) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetItemCostVariantValuesByCostVariantID";
            this.config.data = JSON2.stringify({ itemCostVariantId: itemCostVariantId, itemId: itemId, costVariantID: costVariantId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
        },
        DeleteItemCostVariant: function (tblID, argus) {
            switch (tblID) {
                case "gdvItemCostVariantGrid":
                    ItemMangement.DeleteItemCostVariantByID(argus[0], argus[3]);
                    break;
                default:
                    break;
            }
        },

        DeleteItemCostVariantByID: function (_itemCostVariantId, _itemId) {
            var properties = {
                onComplete: function (e) {
                    ItemMangement.ConfirmSingleDeleteItemCostVariant(_itemCostVariantId, _itemId, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxItemsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxItemsManagement, 'Are you sure you want to delete this item cost variant option?') + "</p>", properties);
        },

        ConfirmSingleDeleteItemCostVariant: function (itemCostVariantID, itemId, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteSingleItemCostVariant";
                this.config.data = JSON2.stringify({ itemCostVariantID: itemCostVariantID, itemId: itemId, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(this.config);
            }
            return false;
        },

        BindCostVariantsInputType: function () {
            this.config.url = this.config.baseURL + "GetCostVariantInputTypeList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 10;
            this.ajaxCall(this.config);
        },

        BindInputTypeDropDown: function (item) {
            $("#ddlAttributeType").append("<option value=" + item.InputTypeID + ">" + item.InputType + "</option>");
        },

        BindCostVariantsOptions: function (itemId, aspxCommonInfo) {
            this.config.url = this.config.baseURL + "GetCostVariantsOptionsList";
            this.config.data = JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 11;
            this.ajaxCall(this.config);
        },

        BindCostVariantsDropDown: function (item) {
            $("#ddlExistingOptions").append("<option value=" + item.CostVariantID + ">" + item.CostVariantName + "</option>");
        },

        ClearVariantForm: function () {
            $("#btnSaveItemVariantOption").removeAttr("name");
            $("#btnResetVariantOptions").show();

            $("#txtCostVariantName").val('');
            $('#txtCostVariantName').removeAttr('disabled');
            $("#txtDescription").val('');
            $("#txtDescription").removeAttr('disabled');
            $('#ddlAttributeType').val(1);
            $('#ddlAttributeType').removeAttr('disabled');
            $('input[name=chkActive]').prop('checked', 'checked');
            $('.cssClassPriceModifierType,.cssClassWeightModifierType').val(0);
            $('.cssClassIsActive').val(1);
            $("#lblHeading").html(getLocale(AspxItemsManagement, "Add New Cost Variant Option"));

            $('input[name=chkUseInAdvancedSearch]').removeAttr('checked');
            $('input[name=chkComparable]').removeAttr('checked');
            $('input[name=chkUseForPriceRule]').removeAttr('checked');

            $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
            $("#tblVariantTable>tbody").find("input[type='text']").val('');
            $("#tblVariantTable>tbody").find("select").val(1);
            $("#tblVariantTable>tbody").find("input[type='hidden']").val('0');
            $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();
            return false;
        },

        OnInit: function () {
            $('#btnResetVariantOptions').hide();
            $("#hdnItemCostVar").val('0');
            $('.cssClassCostVarRight').hide();
            $('.cssClassCostVarError').hide();
            ItemMangement.SelectFirstTab();
        },

        SelectFirstTab: function () {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
            $tabs.tabs('option', 'active', 0);
        },

        IsUniqueCostVariant: function (costVariantName, costVariantId) {
            this.config.url = this.config.baseURL + "CheckUniqueCostVariantName";
            this.config.data = JSON2.stringify({ costVariantName: costVariantName, costVariantId: costVariantId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 12;
            this.ajaxCall(this.config);
            return ItemMangement.vars.isUnique;
        },

        SaveItemCostVariantsInfo: function (itemId, aspxCommonInfo) {
            ItemMangement.SaveItemCostVariant(itemId, aspxCommonInfo);
        },

        SaveItemCostVariant: function (itemId, aspxCommonInfo) {
            var CostVariantCombinationList = [];
            var i = 0;
            var totalQuantity = 0;
            $("#dvCvForm table:first").find(">tbody>tr").each(function () {
                var cType = [];
                var cValues = [];
                var cValuesName = [];
                var CostVariantCombination = {
                    DisplayOrder: "",
                    CombinationType: "",
                    CombinationValues: "",
                    CombinationValuesName: "",
                    CombinationPriceModifier: "",
                    CombinationPriceModifierType: "",
                    CombinationWeightModifier: "",
                    CombinationWeightModifierType: "",
                    CombinationQuantity: 0,
                    CombinationIsActive: false,
                    ImageFile: ""
                };
                $(this).find(">td.cssClassTableCostVariant").each(function () {

                    $(this).find("tbody>tr").each(function () {
                        if ($(this).find(".tdCostVariant .ddlCostVariantsCollection").find("option:selected").val() != 0) {
                            if ($(this).find(".tdCostVariantValues .ddlCostVariantValues").find("option:selected").val() != 0) {
                                cType.push($(this).find(".tdCostVariant .ddlCostVariantsCollection").find("option:selected").val());
                                cValues.push($(this).find(".tdCostVariantValues .ddlCostVariantValues").find("option:selected").val());
                                cValuesName.push($(this).find(".tdCostVariantValues .ddlCostVariantValues").find("option:selected").text());
                            }

                        }
                    });
                    if (cType.length != 0 && cValues.length != 0) {
                        CostVariantCombination.CombinationType = cType.join('@');
                        CostVariantCombination.CombinationValues = cValues.join('@');
                        CostVariantCombination.CombinationValuesName = cValuesName.join('@');

                        if ($(this).find("div .cssclassCostVariantItemQuantity").val() != '') {
                            CostVariantCombination.CombinationQuantity = $(this).find("div .cssclassCostVariantItemQuantity").val();
                        } else {
                            CostVariantCombination.CombinationQuantity = "0";
                        }

                        if ($(this).find("div .cssClassPriceModifier").val() != '') {
                            CostVariantCombination.CombinationPriceModifier = $(this).find("div .cssClassPriceModifier").val();
                        } else {
                            CostVariantCombination.CombinationPriceModifier = "0.00";
                        }
                        CostVariantCombination.CombinationPriceModifierType = $.trim($(this).find("div .cssClassPriceModifierType").find("option:selected").val()) == 0 ? true : false;
                        if ($(this).find("div .cssClassWeightModifier").val() != '') {
                            CostVariantCombination.CombinationWeightModifier = $(this).find("div .cssClassWeightModifier").val();
                        } else {
                            CostVariantCombination.CombinationWeightModifier = "0.00";
                        }
                        CostVariantCombination.CombinationWeightModifierType = $.trim($(this).find("div .cssClassWeightModifierType").find("option:selected").val()) == 0 ? true : false;
                    }
                });
                if (CostVariantCombination.CombinationType != "") {
                    CostVariantCombination.DisplayOrder = $(this).find(".cssClassDisplayOrder").val();
                    CostVariantCombination.CombinationIsActive = $(this).find(".cssClassIsActive").find("option:selected").val() == 1 ? true : false;
                    if ($(this).find(".cssClassIsActive").find("option:selected").val() == 1) {
                        totalQuantity += parseInt($(this).find("div .cssclassCostVariantItemQuantity").val());
                    }
                    CostVariantCombination.ImageFile = listImages[i];
                    i++;
                    CostVariantCombinationList.push(CostVariantCombination);
                }
            });
            var valx = getObjects(CostVariantCombinationList, 'CombinationType');
            var valy = getObjects(CostVariantCombinationList, 'CombinationValues');
            if (valx.length >= 1 && valx[0] != "") {
                $(".cssClassItemQuantity").val(totalQuantity);
                if ($(".cssClassItemQuantity").val() == "0") {
                    $(".cssClassItemQuantity").removeAttr("disabled", "disabled");
                } else {
                    $(".cssClassItemQuantity").prop("disabled", "disabled");
                }
                removeDuplicateCombination(CostVariantCombinationList);

                var ItemsCostVariant = {
                    ItemId: $("#ItemMgt_itemID").val() == 0 ? itemId : $("#ItemMgt_itemID").val(),
                    VariantOptions: CostVariantCombinationList
                };
            }
            else {
                CostVariantCombinationList = [];
                var ItemsCostVariant = {
                    ItemId: $("#ItemMgt_itemID").val() == 0 ? itemId : $("#ItemMgt_itemID").val(),
                    VariantOptions: CostVariantCombinationList
                };
            }
            ItemMangement.AddItemCostVariantInfo(ItemsCostVariant, aspxCommonInfo);
        },

        AddItemCostVariantInfo: function (objCVCombination, aspxCommonInfo) {

            var params = { itemCostVariants: objCVCombination, aspxCommonObj: aspxCommonInfo };
            this.config.url = this.config.baseURL + "SaveAndUpdateItemCostVariantCombination";
            this.config.data = JSON2.stringify(params);
            this.config.ajaxCallMode = 13;
            this.vars.itemId = objCVCombination.ItemId;
            this.ajaxCall(this.config);
        },

        ClickToDeleteImage: function (objImg) {
            $(objImg).closest('span').html('');
            return false;
        },

        ConfirmDeleteMultiple: function (item_ids, event) {
            if (event) {
                ItemMangement.DeleteMultipleItems(item_ids);
            }
            return false;
        },

        DeleteMultipleItems: function (_itemIds) {
            this.config.url = this.config.baseURL + "DeleteMultipleItemsByItemID";
            this.config.data = JSON2.stringify({ itemIds: _itemIds, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 14;
            this.ajaxCall(this.config);
        },
        GetRelatedCheckIDs: function (itemId) {
            this.config.url = this.config.baseURL + "GetRelatedCheckIDs";
            this.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 45;
            this.ajaxCall(this.config);
        },

        GetUpSellCheckIDs: function (itemId) {
            this.config.url = this.config.baseURL + "GetUpSellCheckIDs";
            this.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 46;
            this.ajaxCall(this.config);
        },
        GetCrossSellCheckIDs: function (itemId) {
            this.config.url = this.config.baseURL + "GetCrossSellCheckIDs";
            this.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 47;
            this.ajaxCall(this.config);
        },

        BindItemsGrid: function (sku, Nm, itemType, attributeSetNm, visibility, isAct) {
            var getItemListObj = {
                SKU: sku,
                ItemName: Nm,
                ItemTypeID: parseInt(itemType),
                AttributeSetID: attributeSetNm,
                Visibility: visibility,
                IsActive: isAct
            };
            this.config.method = "GetItemsList";
            this.config.data = { getItemListObj: getItemListObj, aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvItems_pagesize").length > 0) ? $("#gdvItems_pagesize :selected").text() : 10;

            $("#gdvItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxItemsManagement, 'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemsChkbox', elemDefault: false, controlclass: 'classClassCheckBox' },
                { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'ItemType ID'), name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Type'), name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'AttributeSet ID'), name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Attribute Set Name'), name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'List Price'), name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'qty', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Visibility'), name: 'visibility', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Active?'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                { display: getLocale(AspxItemsManagement, 'IDTobeChecked'), name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                { display: getLocale(AspxItemsManagement, 'CurrencyCode'), name: 'currency_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                { display: getLocale(AspxItemsManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemMangement.EditItems', arguments: '4,6,15' },
                { display: getLocale(AspxItemsManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemMangement.DeleteItems', arguments: '' },
                { display: getLocale(AspxItemsManagement, 'Activate'), name: 'active', enable: true, _event: 'click', trigger: '4', callMethod: 'ItemMangement.ActiveItems', arguments: '' },
                { display: getLocale(AspxItemsManagement, 'Deactivate'), name: 'deactive', enable: true, _event: 'click', trigger: '5', callMethod: 'ItemMangement.DeactiveItems', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false }, 16: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        EditItems: function (tblID, argus) {
            $(".cssClassAttribute ").hide();
            $('#spnAddTitle').text('Edit Item');
            $("#divItemMgrTitle").show();
            $(window).scrollTop(0);
            $('#Todatevalidation').prop('class', '');
            $('#Fromdatevalidation').prop('class', '');
            switch (tblID) {
                case "gdvItems":
                    $("#ItemMgt_itemID").val(argus[0]);
                    if (argus[3] == 4 && argus[4] == 3) {
                        serviceBit = true;
                    } else {
                        serviceBit = false;
                    }
                    primaryCode = argus[5];
                    currencyCodeSlected = argus[5];
                    currencyCodeEdit = argus[5];
                    ItemMangement.ContinueForm(true, argus[4], argus[3], argus[0]);
                    itemTypeId = argus[3];

                    showPopup = true;
                    var itemSKUTxtBoxID = $("#hdnSKUTxtBox").val();
                    var sku = $("#" + itemSKUTxtBoxID).val();
                    ItemMangement.vars.sku = sku;
                    if (itemTypeId != 5) {
                        ItemMangement.InitCostVariantCombination(argus[0], true, aspxCommonObj());
                        ItemMangement.GroupPrice.Set();
                        $('#tblQuantityDiscount').find('thead').find('.cssClassUnitPrice').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeEdit + '):');
                        ItemMangement.ItemSetting.Get();
                    }

                    if (itemTypeId == 5) {
                        ItemMangement.GroupItems.AttributeSetBind();
                        ItemMangement.GroupItems.ItemTypeBind();
                        GetAllCategory();
                        ItemMangement.GroupItems.BindEvents();
                    }
                    ItemMangement.BindItemTypeSearch();
                    ItemMangement.BindAttributeSetSearch();

                    break;
                default:
                    break;
            }
        },
        GetPriceHistory: function (id) {
            var param = JSON2.stringify({ itemId: id, aspxCommerceObj: aspxCommonObj() });
            this.config.method = "GetPriceHistoryList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 34;
            this.ajaxCall(this.config);
        },

        ClickToDelete: function (itemId) {
            ItemMangement.DeleteItemByID(itemId);
        },

        DeleteItems: function (tblID, argus) {
            switch (tblID) {
                case "gdvItems":
                    AspxCommerce.CheckSessionActive(aspxCommonObj());
                    if (AspxCommerce.vars.IsAlive) {
                        ItemMangement.DeleteItemByID(argus[0]);
                    }
                    else {
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteItemByID: function (_itemId) {
            var properties = {
                onComplete: function (e) {
                    ItemMangement.ConfirmSingleDelete(_itemId, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxItemsManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxItemsManagement, "Are you sure you want to delete this item?") + "</p>", properties);
        },

        ConfirmSingleDelete: function (item_id, event) {
            if (event) {
                ItemMangement.DeleteSingleItem(item_id);
            }
            return false;
        },

        DeleteSingleItem: function (_itemId) {
            this.config.url = this.config.baseURL + "DeleteItemByItemID";
            this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 15;
            this.ajaxCall(this.config);
        },

        ActiveItems: function (tblID, argus) {
            switch (tblID) {
                case "gdvItems":
                    ItemMangement.ActivateItemID(argus[0], true);
                    break;
                default:
                    break;
            }
        },

        DeactiveItems: function (tblID, argus) {
            switch (tblID) {
                case "gdvItems":
                    ItemMangement.DeActivateItemID(argus[0], false);
                    break;
                default:
                    break;
            }
        },

        DeActivateItemID: function (_itemId, _isActive) {
            AspxCommerce.CheckSessionActive(aspxCommonObj());
            if (AspxCommerce.vars.IsAlive) {
                this.config.url = this.config.baseURL + "UpdateItemIsActiveByItemID";
                this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), aspxCommonObj: aspxCommonObj(), isActive: _isActive });
                this.config.ajaxCallMode = 16;
                this.ajaxCall(this.config);
                return false;
            } else {
                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
            }
        },

        ActivateItemID: function (_itemId, _isActive) {
            AspxCommerce.CheckSessionActive(aspxCommonObj());
            if (AspxCommerce.vars.IsAlive) {
                this.config.url = this.config.baseURL + "UpdateItemIsActiveByItemID";
                this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), aspxCommonObj: aspxCommonObj(), isActive: _isActive });
                this.config.ajaxCallMode = 17;
                this.ajaxCall(this.config);
                return false;
            }
            else {
                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
            }
        },

        BindRelatedItemsGrid: function (selfItemID, itemSKU, itemName, itemTypeID, attributeSetID, aspxCommonInfo) {

            var itemDetails = {
                serviceBit: serviceBit,
                selfItemId: selfItemID,
                itemSKU: itemSKU,
                itemName: itemName,
                itemTypeID: itemTypeID,
                attributeSetID: attributeSetID
            };
            this.config.method = "GetRelatedItemsList";
            this.config.data = { IDCommonObj: itemDetails, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRelatedItems_pagesize").length > 0) ? $("#gdvRelatedItems_pagesize :selected").text() : 10;

            $("#gdvRelatedItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxItemsManagement, 'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkRelatedControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'ItemType ID'), name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Type'), name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'AttributeSet ID'), name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Attribute Set Name'), name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'List Price'), name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Visibility'), name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Active?'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                { display: getLocale(AspxItemsManagement, 'IDTobeChecked'), name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                { display: getLocale(AspxItemsManagement, 'CurrencyCode'), name: 'currency_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false} }
            });
            ItemMangement.GetRelatedCheckIDs(selfItemID);
        },

        BindUpSellItemsGrid: function (selfItemID, itemSKU, itemName, itemTypeID, attributeSetID, aspxCommonInfo) {
            var UpSellCommonObj = {
                serviceBit: serviceBit,
                selfItemId: selfItemID,
                itemSKU: itemSKU,
                itemName: itemName,
                itemTypeID: itemTypeID,
                attributeSetID: attributeSetID
            };
            this.config.method = "GetUpSellItemsList";
            this.config.data = { UpSellCommonObj: UpSellCommonObj, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvUpSellItems_pagesize").length > 0) ? $("#gdvUpSellItems_pagesize :selected").text() : 10;

            $("#gdvUpSellItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxItemsManagement, 'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkUpSellControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'ItemType ID'), name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Type'), name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'AttributeSet ID'), name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Attribute Set Name'), name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'List Price'), name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Visibility'), name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Active?'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                { display: getLocale(AspxItemsManagement, 'IDTobeChecked'), name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                { display: getLocale(AspxItemsManagement, 'CurrencyCode'), name: 'currency_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false} }
            });
            ItemMangement.GetUpSellCheckIDs(selfItemID);
        },

        BindCrossSellItemsGrid: function (selfItemID, itemSKU, itemName, itemTypeID, attributeSetID, aspxCommonInfo) {
            var CrossSellCommonObj = {
                serviceBit: serviceBit,
                selfItemId: selfItemID,
                itemSKU: itemSKU,
                itemName: itemName,
                itemTypeID: itemTypeID,
                attributeSetID: attributeSetID
            };
            this.config.method = "GetCrossSellItemsList";
            this.config.data = { CrossSellCommonObj: CrossSellCommonObj, aspxCommonObj: aspxCommonInfo };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCrossSellItems_pagesize").length > 0) ? $("#gdvCrossSellItems_pagesize :selected").text() : 10;

            $("#gdvCrossSellItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxItemsManagement, 'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkCrossSellControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'ItemType ID'), name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Type'), name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'AttributeSet ID'), name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Attribute Set Name'), name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'List Price'), name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Visibility'), name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Active?'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxItemsManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                { display: getLocale(AspxItemsManagement, 'IDTobeChecked'), name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                { display: getLocale(AspxItemsManagement, 'CurrencyCode'), name: 'currency_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false} }
            });
            ItemMangement.GetCrossSellCheckIDs(selfItemID);
        },

        BindAttributeSet: function () {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            aspxCommonInfo.constructor = null;
            this.config.url = this.config.baseURL + "GetAttributeSetList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 18;
            this.ajaxCall(this.config);
        },

        BindItemType: function () {
            $('#ddlItemType').get(0).options.length = 0;
            $.each(itemTypeArray, function (index, item) {
                if ($('#ddlAttributeSet').val() != 3) {
                    if (item.ItemTypeID != 4) {
                        $("#ddlItemType").get(0).options[$("#ddlItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                    }
                } else if ($('#ddlAttributeSet').val() == 3) {
                    if (item.ItemTypeID == 4) {
                        $("#ddlItemType").get(0).options[$("#ddlItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                    }
                }
                $("#ddlSearchItemType").get(0).options[$("#ddlSearchItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
            });
        },

        BindAttributeSetSearch: function () {
            $.each(attributeSetArray, function (index, item) {
                if ($("#ddlSelectAttributeSetName").length > 0)
                    $("#ddlSelectAttributeSetName").get(0).options[$("#ddlSelectAttributeSetName").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                if ($("#ddlSelectAttributeSetNameSell").length > 0)
                    $("#ddlSelectAttributeSetNameSell").get(0).options[$("#ddlSelectAttributeSetNameSell").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                if ($("#ddlSelectAttributeSetNamecs").length > 0)
                    $("#ddlSelectAttributeSetNamecs").get(0).options[$("#ddlSelectAttributeSetNamecs").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
            });
        },
        BindItemTypeSearch: function () {
            $.each(itemTypeArray, function (index, item) {
                if ($("#ddlSelectItemType").length > 0)
                    $("#ddlSelectItemType").get(0).options[$("#ddlSelectItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                if ($("#ddlSelectItemTypeSell").length > 0)
                    $("#ddlSelectItemTypeSell").get(0).options[$("#ddlSelectItemTypeSell").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                if ($("#ddlSelectItemTypecs").length > 0)
                    $("#ddlSelectItemTypecs").get(0).options[$("#ddlSelectItemTypecs").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);

            });
        },

        ClearForm: function () {
            $('#ddlAttributeSet').val('2');
            $('#ddlItemType').val('1');
        },

        ContinueForm: function (showDeleteBtn, attributeSetId, itemTypeId, itemId) {
            ItemMangement.ResetHTMLEditors();
            ItemMangement.GetFormFieldList(attributeSetId, itemTypeId, showDeleteBtn, itemId);
        },

        FillItemAttributes: function (itemId, item) {
            var attNameNoSpace = "_" + item.AttributeName.replace(new RegExp(" ", "g"), '-');
            var id = item.AttributeID + '_' + item.InputTypeID + '_' + item.ValidationTypeID + '_' + item.IsRequired + '_' + item.GroupID
        + '_' + item.IsIncludeInPriceRule + '_' + item.DisplayOrder;
            var val = '';
            switch (item.InputTypeID) {
                case 1: if (item.ValidationTypeID != null) {
                        if (item.ValidationTypeID == 3) {
                            $("#" + id).val(item.AttributeValues);
                            break;
                        }
                        else if (item.ValidationTypeID == 5) {
                            $("#" + id).val(item.AttributeValues);
                            break;
                        }
                        else {
                            $("#" + id).val(unescape(item.AttributeValues));
                            break;
                        }
                    }
                case 2: if (item.AttributeValues != null) {
                        $("#" + id).val(Encoder.htmlDecode(item.AttributeValues));
                        for (var i = 0; i < editorList.length; i++) {
                            if (editorList[i].ID == id + "_editor") {
                                editorList[i].Editor.setData(Encoder.htmlDecode(item.AttributeValues));
                            }
                        }
                    }
                    break;
                case 3: if (item.AttributeValues != null) {
                        if (item.AttributeValues == "1900/01/01" || item.AttributeValues == "2999/12/30") {
                            $("#" + id).val("");
                        }
                        else {
                            $("#" + id).val(item.AttributeValues);
                        }
                    }
                    break;
                case 4: if (item.AttributeValues != null) {
                        if (item.AttributeValues.toLowerCase() == "1") {
                            $("#" + id).prop("checked", "checked");
                        }
                        else if (item.AttributeValues.toLowerCase() == "0") {
                            $("#" + id).removeAttr("checked");
                        }
                    }
                    break;
                case 5: if (item.AttributeValues != null) {
                        val = item.AttributeValues;
                        vals = val.split(',');
                        $.each(vals, function (i) {
                            $("#" + id + " option[value=" + vals[i] + "]").prop("selected", "selected");
                        });
                    }
                    break;
                case 6: $("#" + id).val('');
                    val = item.AttributeValues;
                    if (val != null) {
                        vals = val.split(',');
                        $.each(vals, function (i) {
                            $("#" + id + " option[value=" + vals[i] + "]").prop("selected", "selected");
                        });
                    }
                    break;
                case 7: if (item.AttributeValues != null) {
                        $("#" + id).val(item.AttributeValues);
                    }
                    break;
                case 8: if (item.AttributeValues != null) {
                        var d = $("#" + id).parent();
                        var filePath = item.AttributeValues;
                        var fileName = filePath.substring(filePath.lastIndexOf("/") + 1);
                        if (filePath != "") {
                            var fileExt = (-1 !== filePath.indexOf('.')) ? filePath.replace(/.*[.]/, '') : '';
                            myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                            if (myregexp.test(fileExt)) {
                                $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + filePath + '" class="uploadImage" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                            }
                            else {
                                $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + filePath + '" class="uploadFile" target="_blank">' + fileName + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                            }
                            $(d).find('input[type="hidden"]').val(filePath);
                        }
                    }
                    break;
                case 9: if (item.AttributeValues != null) {
                        if (item.AttributeValues == "") {
                            $("#" + id).removeAttr("checked");
                        }
                        else {
                            $("#" + id).prop("checked", "checked");
                        }
                    }
                    break;
                case 10: if (item.AttributeValues != null) {
                        $("input[value=" + item.AttributeValues + "]:radio").prop("checked", "checked");
                    }
                    break;
                case 11: if (item.AttributeValues != null) {
                        if (item.AttributeValues == "") {
                            $("#" + id).removeAttr("checked");
                        }
                        else {
                            $("#" + id).prop("checked", "checked");
                        }
                    }
                    break;
                case 12: if (item.AttributeValues != null) {
                        var inputs = $("input[name=" + id + "]");
                        $.each(inputs, function (i) {
                            $(this).removeAttr("checked");
                        });
                        val = item.AttributeValues;
                        vals = val.split(',');
                        $.each(vals, function (i) {
                            $("input[value=" + vals[i] + "]").prop("checked", "checked");
                        });
                    }
                    break;
                case 13: if (item.AttributeValues != null) {
                        $("#" + id).val(item.AttributeValues);
                    }
                    break;
            }
        },

        DateDeserialize: function (dateStr) {
            return dateStr.replace(new RegExp("\/", "g"), ' ');
        },

        GetFormFieldList: function (attributeSetId, itemTypeId, showDeleteBtn, itemId) {
            this.config.url = this.config.baseURL + "GetItemFormAttributes";
            this.config.data = JSON2.stringify({ attributeSetID: attributeSetId, itemTypeID: itemTypeId, aspxCommonObj: aspxCommonObj() });
            this.vars.attributeSetId = attributeSetId;
            this.vars.itemTypeId = itemTypeId;
            this.vars.showDeleteBtn = showDeleteBtn;
            this.vars.itemId = itemId;
            this.config.ajaxCallMode = 20;
            this.ajaxCall(this.config);
        },

        BindDataInImageTab: function (itemId, aspxCommonInfo) {
            if (itemId > 0) {
                this.config.url = this.config.baseURL + "GetImageContents";
                this.config.data = JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 21;
                this.ajaxCall(this.config);
            }
        },

        BindToTable: function (msg) {
            ItemMangement.CreateTableHeader();
            $("#multipleUpload .classTableWrapper > tbody").html('');
            $.each(msg.d, function (index, item) {
                rowCount = index;
                var j = rowCount + 1;
                var newRowImage = '';
                var imagePath = itemImagePath + item.ImagePath;
                newRowImage += '<tr class="classRowData' + j + '" value="' + item.ItemImageID + '">';
                newRowImage += '<td><img src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" class="uploadImage"/></td>';
                newRowImage += '<td><div class="field required"><input type="textbox" class="sfInputbox cssClassImageDiscription" maxlength="256" value="' + item.AlternateText + '"/><span class="iferror"></span></div></td>';
                newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassDisplayOrder" maxlength="3" value="' + item.DisplayOrder + '"/><span class="iferror">' + getLocale(AspxItemsManagement, "Integer Number") + '</span></div></td>';
                newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Base Image" class="notTest" /></td>';
                newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Small Image" class="notTest" /></td>';
                newRowImage += '<td><input type="radio" name="itemimage_' + j + '"  value="ThumbNail" class="notTest" /></td>';
                newRowImage += '<td><input type="checkbox" class="notTest" id="chkIsActive_' + j + '" /></td>';
                newRowImage += '<td><i class="imgDelete icon-delete" id="btn' + j + '" onclick="ItemMangement.DeleteImage(this)" /></i></td>';
                newRowImage += '</tr>';
                $("#multipleUpload .classTableWrapper > tbody").append(newRowImage);
                $(".cssClassDisplayOrder").bind("contextmenu", function (e) {
                    return false;
                });
                $('.cssClassDisplayOrder').bind('paste', function (e) {
                    e.preventDefault();
                });
                if (item.IsActive) {
                    $('#chkIsActive_' + j + '').prop('checked', item.IsActive);
                }

                if (item.ImageType == "Base Image") {
                    $("tbody>tr.classRowData" + j + ">td:eq(3) input:radio").prop("checked", "checked");
                }
                else if (item.ImageType == "Small Image") {
                    $("tbody>tr.classRowData" + j + ">td:eq(4) input:radio").prop("checked", "checked");
                }
                else if (item.ImageType == "ThumbNail") {
                    $("tbody>tr.classRowData" + j + ">td:eq(5) input:radio").prop("checked", "checked");
                }
                $("i.imgDelete").click(function () {
                    $("#VariantsImagesTable").html('');
                    $(this).parent().parent().remove();
                    $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfEven");
                    $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfOdd");
                    $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("sfEven");
                    $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("sfOdd");
                    return false;
                });
                rowCount++;
            });
            $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfEven");
            $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfOdd");
            $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("sfEven");
            $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("sfOdd");
            $(".cssClassImageDiscription").keypress(function (e) {
                if (e.which == 35 || e.which == 37) {
                    return false;
                }
            });
            ItemMangement.DisallowSameImageDisplayOrder();
        },

        ImageUploader: function (maxFileSize) {
            var aspxCommonInfo = aspxCommonObj();
            var uploader = new qq.FileUploader({
                element: document.getElementById('fileUpload'),
                action: aspxItemModulePath + 'MultipleFileUploadHandler.ashx',
                allowedExtensions: ['jpg', 'jpeg', 'jpe', 'gif', 'bmp', 'png', 'ico'],
                debug: false,
                params: aspxCommonInfo,
                sizeLimit: maxFileSize * 1024,
                multiple: true,
                onComplete: function (id, fileName, responseJSON) {
                    if (responseJSON.success) {
                        ItemMangement.CreateTableHeader();
                        ItemMangement.AddNewImages(responseJSON.path);
                        $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfEven");
                        $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfOdd");
                        $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("sfEven");
                        $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("sfOdd");
                        $(".cssClassDisplayOrder").keypress(function (e) {
                            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                                return false;
                            }
                        });
                        $("#divImageCollapsable").show();
                        $(".cssClassDescription").keypress(function (e) {
                            if (e.which == 35 || e.which == 37) {
                                return false;
                            }
                        });

                    }
                }
            });

        },


        dummyProgress: function (progressBar, percentage) {
            if (percentageInterval[pcount]) {
                progress = percentageInterval[pcount] + Math.floor(Math.random() * 2);
                percentage.text(progress.toString() + '%');
                progressBar.progressbar({
                    value: progress
                });
                var percent = percentage.text();
                percent = percent.replace('%', '');
                if (percent == 100 || percent > 100) {
                    percentage.text('100%');
                    $('.progress').hide(1000);
                }
            }

            if (timeInterval[pcount]) {
                progressTime = setTimeout(function () {
                    ItemMangement.dummyProgress(progressBar, percentage);
                }, timeInterval[pcount] * 50);
            }
            pcount++;
        },

        AddNewImages: function (response) {
            var j = rowCount + 1;
            var newRowImage = '';
            newRowImage += '<tr class="classRowData' + j + '" value="0">';
            newRowImage += '<td><img src="' + aspxRootPath + response + '" class="uploadImage" height="93px" width="125px"/></td>';
            newRowImage += '<td><div class="field required"><input type="textbox" class="sfInputbox cssClassDescription" maxlength="256" /><span class="iferror"></span></div></td>';
            newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassDisplayOrder" maxlength="3" /><span class="iferror">' + getLocale(AspxItemsManagement, "Integer Number") + '</span></div></td>';
            newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Base Image" class="notTest" /></td>';
            newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Small Image" class="notTest" /></td>';
            newRowImage += '<td><input type="radio" name="itemimage_' + j + '"  value="ThumbNail" class="notTest" checked="checked" /></td>';
            newRowImage += '<td><input type="checkbox" class="notTest" checked="checked"/></td>';
            newRowImage += '<td><i class="imgDelete icon-delete" id="btn' + j + '" onclick="ItemMangement.DeleteImage(this)" /></i></td>';
            newRowImage += '</tr>';
            $("#multipleUpload .classTableWrapper > tbody").append(newRowImage);
            if (j == 1) {
                $('input[value="Base Image"]').prop("checked", "checked");
            }
            rowCount++;
            $(".cssClassDescription").keypress(function (e) {
                if (e.which == 35 || e.which == 37) {
                    return false;
                }
            });
            $(".cssClassDisplayOrder").bind("contextmenu", function (e) {
                return false;
            });
            $('.cssClassDisplayOrder').bind('paste', function (e) {
                e.preventDefault();
            });
            ItemMangement.DisallowSameImageDisplayOrder();
        },

        DisallowSameImageDisplayOrder: function () {
            $("#divTableWrapper>table").find("td .cssClassDisplayOrder").on("keyup", function () {
                var value = parseInt($(this).val()); var clear = false;
                var $elem = $(this);
                $("#divTableWrapper>table").find("td .cssClassDisplayOrder").not(this).each(function () {
                    if (parseInt($(this).val()) == value) {
                        clear = true;
                    }
                });
                if (clear) $(this).val('');

            });
        },
        DeleteImage: function (onjImg) {
            $(onjImg).parent().parent().remove();
            $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfEven");
            $("#multipleUpload .classTableWrapper > tbody tr").removeClass("sfOdd");
            $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("sfEven");
            $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("sfOdd");
        },

        CreateForm: function (itemFormFields, attributeSetId, itemTypeId, showDeleteBtn, itemId) {
            var strDynRow = '';
            var attGroup = new Array();
            attGroup.length = 0;
            $.each(itemFormFields, function (index, item) {
                var isGroupExist = false;
                for (var i = 0; i < attGroup.length; i++) {
                    if (attGroup[i].key == item.GroupID) {
                        isGroupExist = true;
                        break;
                    }
                }
                if (!isGroupExist) {
                    attGroup.push({ key: item.GroupID, value: item.GroupName, tabName: item.GroupTabName, html: '' });
                }
            });
            FileUploaderIDs = new Array();
            $.each(itemFormFields, function (index, item) {
                strDynRow = ItemMangement.createRow(itemId, itemTypeId, item.AttributeID, item.AttributeName, item.InputTypeID, item.InputTypeValues != "" ? eval(item.InputTypeValues) : '', item.DefaultValue, item.ToolTip, item.Length, item.ValidationTypeID, item.IsEnableEditor, item.IsUnique, item.IsRequired, item.GroupID, item.IsIncludeInPriceRule, item.DisplayOrder);
                for (var i = 0; i < attGroup.length; i++) {
                    if (attGroup[i].key == item.GroupID) {
                        attGroup[i].html += strDynRow;
                    }
                }
            });

            ItemMangement.CreateAccordion(attGroup, attributeSetId, itemTypeId, showDeleteBtn);
            $("#newCostvariants").hide();
            if (itemTypeId != 5) {
                ItemMangement.BindTaxManageRule(aspxCommonObj());
            }
            if (itemTypeId == 1 || itemTypeId == 2) {
                ItemMangement.GetAllBrandForItem(aspxCommonObj());
                if (itemId > 0) {
                    ItemMangement.GetBrandByItemID(itemId, aspxCommonObj());
                }
            }
            if (itemId > 0) {
                ItemMangement.GetItemVideoContents(itemId);
            }
            if (itemTypeId != 3)
                ItemMangement.CreateCategoryMultiSelect(itemId, aspxCommonObj());

            ItemMangement.BindCurrencyList();
            $('.cssClassSKU').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
                this.value = this.value.replace(/\s/g, '').replace(' ', '');
            });

            $('.cssClassRight').hide();
            $('.cssClassError').hide();
            $('.cssClassError').html('');
            ItemMangement.BindPopUP();
            ItemMangement.BindTierPriceCommand();
            $(".classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger").DigitAndDecimal('.classItemListPrice,.classItemPrice,.verifyDecimal,.verifyInteger ', '');
            $(".classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger,.hasDatepicker").bind("contextmenu", function (e) {
                return false;
            });
            $('.classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger,.hasDatepicker').bind('paste', function (e) {
                e.preventDefault();
            });
            $(".SpecialDropDown").bind("change", function () {
                if ($(this).val() == 10) {
                    $('.classSpecialFrom').removeClass('error');
                    $('.classSpecialTo').removeClass('error');
                    $('.classSpecialFrom').parent('div').removeClass('diverror');
                    $('.classSpecialFrom').parent('div').removeClass('diverror');
                    $('.classSpecialTo').next('span').html('');
                }
                if ($(this).val() == 5) {
                    $(".classSpecialFrom  ").datepicker("option", "disabled", false);
                    $(".classSpecialTo").datepicker("option", "disabled", false);
                } else {
                    $(".classSpecialFrom  ").datepicker("option", "disabled", true);
                    $(".classSpecialTo").datepicker("option", "disabled", true);
                }
            });


            $(".FeaturedDropDown").bind("change", function () {
                if ($(this).val() == 8) {
                    $('.classFeaturedFrom').removeClass('error');
                    $('.classFeaturedTo').removeClass('error');
                    $('.classFeaturedFrom').parent('div').removeClass('diverror');
                    $('.classFeaturedFrom').parent('div').removeClass('diverror');
                    $('.classFeaturedTo').next('span').html('');
                }
                if ($(this).val() == 3) {
                    $(".classFeaturedFrom  ").datepicker("option", "disabled", false);
                    $(".classFeaturedTo").datepicker("option", "disabled", false);
                } else {
                    $(".classFeaturedFrom").datepicker("option", "disabled", true);
                    $(".classFeaturedTo").datepicker("option", "disabled", true);
                }
            });

        },

        BindDownloadableForm: function (itemId) {
            this.config.url = this.config.baseURL + "GetDownloadableItem";
            this.config.data = JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 22;
            this.ajaxCall(this.config);
        },

        FillDownlodableItemForm: function (response) {
            $.each(response.d, function (index, msg) {
                $("#txtDownloadTitle").val(msg.Title);
                if (msg.MaxDownload == 0) {
                    $("#txtMaxDownload").val('');
                }
                else {
                    $("#txtMaxDownload").val(msg.MaxDownload);
                }
                $("#fileSample").prop("title", msg.SampleFile);
                if (msg.SampleFile == '') {
                    $("#spanSample").html("");
                }
                else
                    $("#spanSample").html(getLocale(AspxItemsManagement, "Previous: "));
                $("#spanSample").append(msg.SampleFile);
                $("#fileActual").prop("title", msg.ActualFile);
                if (msg.ActualFile == '') {
                    $("#spanActual").html("");
                }
                else
                    $("#spanActual").html(getLocale(AspxItemsManagement, "Previous: "));
                $("#spanActual").append(msg.ActualFile);
                if (msg.DisplayOrder == 0) {
                    $("#txtDownDisplayOrder").val('');
                } else {
                    $("#txtDownDisplayOrder").val(msg.DisplayOrder);
                }
                $("#btnSave").prop("name", msg.DownloadableID);
            });
        },

        BindPopUP: function () {
            $('#btnAddExistingOption').on('click', function () {
                var item_Id = $("#ItemMgt_itemID").val();
                ItemMangement.BindCostVariantsOptions(item_Id, aspxCommonObj());
                $("#variantsGrid,#divNewVariant").hide();
                $("#newCostvariants,#divExistingVariant").show();
                return false;
            });
            $('#btnAddNewOption').on('click', function () {
                $('.cssClassPriceModifierType option[value*=false]').each(function () {
                    $(this).val($(this).val().replace('false', '0'));
                });
                $('.cssClassPriceModifierType option[value*=true]').each(function () {
                    $(this).val($(this).val().replace('true', '1'));
                });
                $('.cssClassWeightModifierType option[value*=false]').each(function () {
                    $(this).val($(this).val().replace('false', '0'));
                });
                $('.cssClassWeightModifierType option[value*=true]').each(function () {
                    $(this).val($(this).val().replace('true', '1'));
                });
                $('.cssClassIsActive option[value*=false]').each(function () {
                    $(this).val($(this).val().replace('false', '0'));
                });
                $('.cssClassIsActive option[value*=true]').each(function () {
                    $(this).val($(this).val().replace('true', '1'));
                });
                ItemMangement.OnInit();
                ItemMangement.ClearVariantForm();
                $('#ddlAttributeType').html('');
                ItemMangement.BindCostVariantsInputType();
                $("#tabFrontDisplay").show();
                $("#variantsGrid,#divExistingVariant").hide();
                $("#newCostvariants,#divNewVariant").show();
                $("#VariantsImagesTable>tbody").html('');
                listImages = new Array();
                return false;
            });
            $('#dvCvForm').on('click', '.classAddImages', function () {


                var value = $(this).closest("tr")[0].rowIndex - 1;
                $("#btnSaveImages").prop("value", value);
                $("#imageUploader").show();


                if (listImages[value] != null && listImages[value] != "") {
                    var subStr = listImages[value].split('@');
                    var List = '';
                    $.each(subStr, function (index) {
                        List += '<tr>';
                        List += '<td><img src="' + aspxRootPath + "Modules/AspxCommerce/AspxItemsManagement/uploads/" + subStr[index] + '" class="uploadImage" height="100px" width="125px"/></td>';
                        List += '<td><i class="imgDelete icon-delete" id="btn" onclick="ItemMangement.DeleteImage(this)" /></i></td>';
                        List += '</tr>';
                    });

                    if (List != '') {
                        $("#VariantsImagesTable>tbody").html(List);
                    }
                    $("#VariantsImagesTable").show();
                    $('#btnSaveImages').show();
                    $('#btnImageBack').show();

                } else {
                    $("#VariantsImagesTable>tbody").html('');
                    $("#VariantsImagesTable").hide();
                    $('#btnSaveImages').hide();
                    $('#btnImageBack').hide();
                }
                ShowPopupControl('popuprel2');
                ItemMangement.CostVariantsImageUploader(maxFileSize);
                $("#VariantsImagesTable>tbody tr:even").addClass("sfEven");
                $("#VariantsImagesTable>tbody tr:odd").addClass("sfOdd");
                return false;
            });
            $('.classAddImagesEdit').on('click', function () {
                var value = $(this).closest("tr")[0].rowIndex - 1;
                $("#btnSaveImages").prop("value", value);
                return false;
            });

            $("#btnSaveImages").click(function () {
                var i = $(this).val();
                $('#fade, #popuprel2').fadeOut();
                var list = '';
                $('#VariantsImagesTable>tbody>tr').each(function () {
                    list += $(this).find("img").attr("src").replace(aspxRootPath + "Modules/AspxCommerce/AspxItemsManagement/uploads/", "") + '@';
                });
                list = list.substring(0, list.length - 1);
                listImages[i] = list;
                $('#tblVariantTable>tbody tr input[type="button"]').find("name").text('');
                $('#tblVariantTable>tbody tr input[type="button"]').find("name").append(listImages[i]);
                $("#VariantsImagesTable").hide();
                $("#btnSaveImages").removeAttr("value");
                return false;
            });
        },

        BindTierPriceCommand: function () {
            $("#btnSaveQuantityDiscount").bind("click", function () {

                ItemMangement.SaveItemDiscountQuantity($("#ItemMgt_itemID").val());
                return false;
            });
            $("#btnDeleteQuantityDiscount").bind("click", function () {
                var properties = {
                    onComplete: function (e) {
                        if (e) {
                            ItemMangement.DeleteItemDiscountQuantity();
                        }
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxItemsManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxItemsManagement, "Are you sure you want to delete all quantity discounts?") + "</p>", properties);

                return false;
            });
            $("#btnAddQuantityDiscount").bind("click", function () {
                $("#btnAddQuantityDiscount").attr('clicked', 1);
                $("#tblQuantityDiscount").parent('div.sfFormwrapper').show();
                $("#dvAddNewQuantityDiscount").hide();
                ItemMangement.BindItemQuantityDiscountsByItemID($("#ItemMgt_itemID").val());
                return false;
            });

        },

        HideAllVariantDivs: function () {
            $("#divExistingVariant").hide();
            $("#divNewVariant").hide();
        },

        BindDataInAccordin: function (itemId, attributeSetId, itemTypeId, aspxCommonInfo) {
            this.config.url = this.config.baseURL + "GetItemFormAttributesValuesByItemID";
            this.config.data = JSON2.stringify({ itemID: itemId, attributeSetID: attributeSetId, itemTypeID: itemTypeId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 23;
            this.ajaxCall(this.config);
        },

        CreateCategoryMultiSelect: function (itemId, aspxCommonInfo) {
            this.config.url = this.config.baseURL + "GetCategoryList";
            this.config.data = JSON2.stringify({ prefix: '---', isActive: true, aspxCommonObj: aspxCommonInfo, itemId: itemId, serviceBit: serviceBit });
            this.config.ajaxCallMode = 24;
            this.ajaxCall(this.config);
        },

        FillMultiSelect: function (msg) {
            if (itemTypeId != 3) {

                $('#lstCategories').get(0).options.length = 0;
                if (attributeSetId == 3) {
                    $('#lstCategories').removeAttr('multiple');
                } else {
                    $('#lstCategories').prop('multiple', 'multiple');
                }
                $('#lstCategories').prop('size', '5');
                $.each(msg.d, function (index, item) {
                    $("#lstCategories").get(0).options[$("#lstCategories").get(0).options.length] = new Option(item.LevelCategoryName, item.CategoryID);
                    if (item.IsChecked) {
                        $("#lstCategories option[value=" + item.CategoryID + "]").prop("selected", "selected");
                    }
                });
            }
        },
        GetAllBrandForItem: function (aspxCommonInfo) {
            this.config.url = this.config.baseURL + "GetAllBrandForItem";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 28;
            this.ajaxCall(this.config);
        },

        BindAllBrandForItem: function (msg) {
            $('#lstBrands').get(0).options.length = 0;
            $('#lstBrands').prop('size', '5');
            $('#lstBrands').append("<option value='0'>" + getLocale(AspxItemsManagement, "None") + "</option>");
            $.each(msg.d, function (index, item) {
                $('#lstBrands').append("<option value='" + item.BrandID + "'>" + item.BrandName + "</option>");

            });
            $("#lstBrands").val('0');
        },
        GetBrandByItemID: function (itemId, aspxCommonInfo) {
            this.config.url = this.config.baseURL + "GetBrandByItemID";
            this.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 30;
            this.ajaxCall(this.config);
        },
        GetItemVideoContents: function (itemId) {
            this.config.url = this.config.baseURL + "GetItemVideoContents";
            this.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 33;
            this.ajaxCall(this.config);
        },
        createRow: function (itemId, itemTypeId, attID, attName, attType, attTypeValue, attDefVal, attToolTip, attLen, attValType, isEditor, isUnique, isRequired, groupId, isIncludeInPriceRule, displayOrder) {
            var retString = '';
            if ((attID == 15 && itemTypeId == 2) || (attID == 5 && itemTypeId == 2)) {
                retString += '<tr><td><label class="cssClassLabel" hidden="true">' + attName + ': </label></td>';
            }
            else {
                retString += '<tr><td><label class="cssClassLabel">' + attName + ': </label></td>';
            }
            switch (attType) {
                case 1: if (attID == 4) {
                        $("#hdnSKUTxtBox").val(attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder);
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox cssClassSKU dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" onblur="ItemMangement.CheckUniqueness(this.value, ' + itemId + ' )"/>';
                        retString += '<span class="cssClassRight"><img class="cssClassSuccessImg" height="13" width="18" alt="Right" src="' + aspxTemplateFolderPath + '/images/right.jpg"></span><b class="cssClassError">' + getLocale(AspxItemsManagement, "Ops! found something error, must be unique with no spaces") + '</b>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 5 && itemTypeId != 2) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/><span>[' + WeightUnit + ']</span>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 32) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/><span>[' + DimensionUnit + ']</span>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 33) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/><span>[' + DimensionUnit + ']</span>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 34) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/><span>[' + DimensionUnit + ']</span>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 5 && itemTypeId == 2) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" readonly="readonly" hidden="true"/>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 15 && itemTypeId == 2) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" readonly="readonly" hidden="true"/>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    } else if (attID == 15 && itemTypeId == 3) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" readonly="readonly"/>';
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else {
                        if (attID == 15 && attType == 1)//item quantity
                            retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox cssClassItemQuantity cssClassItemName dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/>';
                        else retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox  cssClassItemName dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/>';

                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }

                    break;
                case 2: var editorDiv = '';
                    if (isEditor) {
                        htmlEditorIDs[htmlEditorIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + "_editor";
                        editorDiv = '<div id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_editor"></div>';
                    }
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><textarea id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" ' + ((isEditor == true) ? ' style="display: none !important;" ' : '') + ' rows="' + attLen + '"  class="cssClassTextArea dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + ItemMangement.GetValidationTypeErrorMessage(attValType) + '">' + attDefVal + '</textarea>' + editorDiv + '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 4: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + (isRequired == true ? "required" : "") + '">';
                    if (attDefVal == 1) {
                        retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '" checked="checked"/>';
                    }
                    else {
                        retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/>';
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 3: DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder;
                    if (attID == 6) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classNewFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 7) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classNewTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 19) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classActiveFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 20) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classActiveTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 27) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classFeaturedFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 28) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classFeaturedTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 30) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classSpecialFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 31) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classSpecialTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 46) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classSpecialPriceFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else if (attID == 47) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classSpecialPriceTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /' + getLocale(AspxItemsManagement, "field") + ' --></p></div></td>';
                    }
                    else {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    break;
                case 5: retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" size="' + attLen + '" class="cssClassMultiSelect dynFormItem" multiple>';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                            }
                            else {
                                retString += '<option value="' + val.value + '">' + val.text + '</option>';
                            }
                        }
                    }
                    retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 6: if (attID == 26) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" class="sfListmenu dynFormItem FeaturedDropDown">';
                    }
                    else if (attID == 29) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" class="sfListmenu dynFormItem SpecialDropDown">';
                    }
                    else {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '"  title="' + attToolTip + '" class="sfListmenu dynFormItem">';
                    }
                    for (var i = 0; i < attTypeValue.length; i++) {
                        var val = attTypeValue[i];
                        if (val.isdefault == 1) {
                            retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                        }
                        else {
                            retString += '<option value="' + val.value + '">' + val.text + '</option>';
                        }
                    }
                    retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                    break;
                case 7: if (attID == 8) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classItemPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><select class="sfcurrencyList" id="ddlCurrency"></select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div><div class="popbox"><a class="open" href=#>' + getLocale(AspxItemsManagement, "Price History") + '</a><div class="collapse"><div class="box"><div class="arrow"></div><div class="arrow-border"></div><div class="classPriceHistory" style="display: none"></div></div></div></div></td>';
                    }
                    else if (attID == 13) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classItemListPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><select class="sfcurrencyList" id="ddlCurrencyLP" disabled="disabled"></select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 44) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classItemCostPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><select class="sfcurrencyList" id="ddlCurrencyCP" disabled="disabled"></select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 45) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classItemSpecialPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><select class="sfcurrencyList" id="ddlCurrencySP" disabled="disabled"></select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    else if (attID == 48) {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem classItemManufacturerPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><select class="sfcurrencyList" id="ddlCurrencyMP" disabled="disabled"></select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                        if (p.EnableGroupPrice == true) {
                            if (itemTypeId != 5) {
                                retString += '<tr><td class="cssClassTableRightCol">' + getLocale(AspxItemsManagement, "Group Price") + '</td><td><div class="cssClassGroupPrice">' + ItemMangement.GroupPrice.Build() + '</div></td></tr>';

                            }
                        }
                        if (itemTypeId != 2 && itemTypeId != 3 && itemTypeId != 4 && itemTypeId != 5) {
                            if (p.EnableTierPrice) {
                                retString += '<tr><td class="cssClassTableRightCol">' + getLocale(AspxItemsManagement, "Tier Price Options") + '</td><td><div id="dvAddNewQuantityDiscount" style="display:none;"><div class="sfButtonwrapper"><p><button type="button" id="btnAddQuantityDiscount" class="sfBtn" ><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add New") + '</span></button></p></div></div><div class="sfFormwrapper nopadding"><table width="100%" cellspacing="0" cellpadding="0" id="tblQuantityDiscount"><thead><tr class="cssClassHeading"><td>' + getLocale(AspxItemsManagement, "Quantity More Than") + ':</td><td class="cssClassUnitPrice">' + getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):</td><td>' + getLocale(AspxItemsManagement, "User In Role") + ':</td><td>&nbsp;</td></tr></thead><tbody></tbody></table>';
                                retString += '<div class="sfButtonwrapper"><p><button type="button" id="btnSaveQuantityDiscount" class="sfBtn"><span class="icon-save">' + getLocale(AspxItemsManagement, "Save") + '</span></button></p><p><button type="button" id="btnDeleteQuantityDiscount" class="sfBtn"><span icon-delete>' + getLocale(AspxItemsManagement, "Delete") + '</span></button></p></div></div></div></td></tr>';
                            }
                        }
                    }
                    else {
                        retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }

                    break;
                case 8: FileUploaderIDs[FileUploaderIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder;
                    retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><div class="' + attDefVal + '" name="Upload/temp" lang="' + attLen + '"><input type="hidden" id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_hidden" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_hidden" value="" class="cssClassBrowse dynFormItem"/>';
                    retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="file" class="cssClassBrowse dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '" />';
                    retString += ' <span class="response"></span></div><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 9: if (attTypeValue.length > 0) {
                        retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                            }
                        }
                        retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    }
                    break;
                case 10: retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">'
                    for (var i = 0; i < attTypeValue.length; i++) {
                        var val = attTypeValue[i];
                        if (val.isdefault == 1) {
                            retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                        }
                        else {
                            retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 11: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '" checked="checked"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '"/><label>' + val.text + '</label>';
                            }
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 12: retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                    if (attTypeValue.length > 0) {
                        for (var i = 0; i < attTypeValue.length; i++) {
                            var val = attTypeValue[i];
                            if (val.isdefault == 1) {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                            }
                            else {
                                retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                            }
                        }
                    }
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                case 13: retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="sfInputbox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + ' Password" value="' + attDefVal + '" title="' + attToolTip + '"/>'
                    retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                    break;
                default:
                    break;
            }
            retString += '</tr>';
            return retString;
        },

        SampleFileUploader: function (maxFileSize) {
            var upload = new AjaxUpload($('#fileSample'), {
                action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
                name: 'myfile[]',
                multiple: true,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    if (ext != "exe") {
                        this.setData({
                            'MaxFileSize': maxFileSize
                        });
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Alert Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Not a valid file type!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null) {
                        ItemMangement.showSampleLoadedFile(res);
                        return false;
                    }
                    else {
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to upload file!") + '</p>');
                        return false;
                    }
                }
            });
        },

        showSampleLoadedFile: function (response) {
            $("#spanSample").html('LoadedFile: ');
            $("#spanSample").append(response.Message);
            $("#fileSample").prop('name', response.Message);
        },

        showActualLoadedFile: function (response) {
            $("#spanActual").html('LoadedFile: ');
            $("#spanActual").append(response.Message);
            $("#fileActual").prop('name', response.Message);
        },
        GetEmail: function () {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            var sku = ItemMangement.vars.sku;
            this.config.url = this.config.baseURL + "GetEmail";
            this.config.data = JSON2.stringify({ SKU: sku, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 31;
            this.ajaxCall(this.config);
        },
        SendEmailToUser: function (varinatIds, variantValues, mail) {
            var sku = ItemMangement.vars.sku;
            if (mail.length > 0) {
                var subject = getLocale(AspxItemsManagement, "Product you interested is now available");
                var fullDate = new Date();
                var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                if (twoDigitMonth.length == 2) {
                } else if (twoDigitMonth.length == 1) {
                    twoDigitMonth = '0' + twoDigitMonth;
                }
                var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                var dateyear = fullDate.getFullYear();
                var messageBodyHtml = '';
                messageBodyHtml += '<table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0">  <tr>';
                messageBodyHtml += '<td width="33%"><div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;"> ';
                messageBodyHtml += '<p style="margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;"> <span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">';
                messageBodyHtml += 'Item SKU: #sku#</span><br />';
                messageBodyHtml += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif;text-decoration:blink; text-shadow:1px 1px 0 #fff;"><a style="color: rgb(39, 142, 230);" href="#server#">' + getLocale(AspxItemsManagement, "click here to view all details") + '</a></span> ';
                messageBodyHtml += '</p> </div></td></tr> </table>';
                var emailInfo = {
                    SenderName: aspxCommonObj().UserName,
                    SenderEmail: userEmail,
                    ReceiverName: '',
                    ReceiverEmail: mail,
                    Subject: subject,
                    Message: '',
                    MessageBody: messageBodyHtml
                };
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), emailInfo: emailInfo, VariantId: varinatIds, VarinatValue: variantValues, sku: sku, ProductUrl: productUrl });
                this.config.method = "SendEmailNotification";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 32;
                this.config.error = 32;
                this.ajaxCall(this.config);
            }
        },

        ActualFileUploader: function (maxFileSize) {
            var upload = new AjaxUpload($('#fileActual'), {
                action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
                name: 'myfile[]',
                multiple: true,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    if (ext != "exe") {
                        this.setData({
                            'MaxFileSize': maxFileSize
                        });
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Alert Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Not a valid file type!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null) {
                        ItemMangement.showActualLoadedFile(res);
                        return false;
                    }
                    else {
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to upload the image!") + '</p>');
                        return false;
                    }
                }
            });
        },



        InitCostVariantCombination: function (itemId, isEdit, aspxCommonInfo) {
            $("#dvCvForm").on("change", ".ddlCostVariantsCollection", function () {
                var elem = $(this).parents('tr:eq(0)').find(".tdCostVariantValues").find(".ddlCostVariantValues");
                ItemMangement.GetCostVariantValues(this.value, elem, aspxCommonInfo);


            });
            $("#btnCancelCostVariantCombination").off("click").on("click", function () {
                ItemMangement.ResetCVHtml(aspxCommonInfo);
                return false;
            });

            $("#btnBackCostVariantCombination").off("click").on("click", function () {

                $("#dvCvForm >table").hide();
                $("#dvCostVarAdd").prev(".sfButtonwrapper").hide();
                $("#dvCostVarAdd").show();
                $(".cssClassItemQuantity").removeAttr("disabled", "disabled");
                return false;
            });
            $("#btnAddCostVariantCombination").off("click").on("click", function () {
                $("#dvCvForm >table").show();
                $("#dvCostVarAdd").prev(".sfButtonwrapper").show();
                $("#dvCostVarAdd").hide();
                $("#btnDeleteCostVariantCombination").hide();
                $("#btnCancelCostVariantCombination").hide();
                $("#dvCvForm table:first>tbody>tr>td").find("a.cssClassCvCloseMain").hide();
                return false;
            });
            $("#btnDeleteCostVariantCombination").off("click").on("click", function () {
                var properties = {
                    onComplete: function (e) {
                        if (e) {
                            $(".cssClassItemQuantity").removeAttr("disabled", "disabled");
                            ItemMangement.DeleteCostVariantCombination(aspxCommonInfo);
                        }
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxItemsManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxItemsManagement, 'Are you sure you want to delete this Cost Variant Combination?') + "</p>", properties);
                return false;
            });

            if (!isEdit) {
                $("#btnSaveCostVariantCombination,#btnDeleteCostVariantCombination").remove()
            }
            ItemMangement.ResetCVHtml(false, '', aspxCommonInfo);

        },
        CreateCombinations: function (index, costvarId, costVarValueId, aspxCommonInfo) {
            var parentTableM = $("#dvCvForm table:first").find('>tbody>tr:last');
            var parentTable = $(parentTableM).find("table");
            var trhtml = "<tr>" + $(parentTable).find(">tbody>tr:last").html() + "</tr>";
            if (index != 0) {
                var optionValue = $.trim($(parentTable).find(">tbody>tr:last").find(".ddlCostVariantsCollection").val());

                $(trhtml).appendTo(parentTable).find(".ddlCostVariantsCollection").find("option[value=" + optionValue + "]").remove();
            }
            $(parentTable).find(".ddlCostVariantsCollection:last").find("option[value=" + costvarId + "]").prop("selected", "selected");
            var elem = $(parentTable).find(".ddlCostVariantValues:last");
            ItemMangement.GetCostVariantValues(costvarId, elem, aspxCommonInfo);
            $(parentTable).find(".ddlCostVariantValues:last").find("option[value=" + costVarValueId + "]").prop("selected", "selected");

            $(parentTable).find(">tbody>tr").find(".ddlCostVariantsCollection").prop("disabled", "disabled"); $(parentTable).find(">tbody>tr").find(".ddlCostVariantValues").prop("disabled", "disabled");
            $(parentTable).find(">tbody>tr:last").find("a.cssClassCvAddMore").remove();
            $(parentTable).find('td').find("a.cssClassCvClose").show();
            $(parentTable).find('td').find("a.cssClassCvCloseMain").show();
            $("#dvCvForm table:first>tbody>tr>td").find("a.cssClassCvCloseMain").show();
        },
        CreateCombinationTableRow: function (index, item) {
            if (index != 0) {
                var parentTable = $("#dvCvForm table:first");
                var trhtml = "<tr>" + parentTable.find('>tbody>tr:last').html() + "</tr>";
                parentTable.find('td').find("button.cssclassAddCVariants").remove();
                $(trhtml).appendTo("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find("table tr:gt(1)").remove(); $(".cssClassTableCostVariant:last").find(".ddlCostVariantsCollection").removeAttr("disabled").prop("enabled", "enabled");
                $(".cssClassTableCostVariant:last").find(".ddlCostVariantValues").html("<option value='0'>" + getLocale(AspxItemsManagement, "No values") + "</option>");
                $("#dvCvForm table:first").find(">tbody>tr:last").find(".cssClassDisplayOrder").val($(".cssclassAddCVariants:last").closest("tr")[0].rowIndex);
                if ($(".cssClassTableCostVariant:last").find(".tdCostVariant").find("a.cssClassCvAddMore").length > 0) {
                } else {
                    $(".cssClassTableCostVariant:last").find(".tdCostVariant").append("<a href=\"#dvCvForm\" class=\"cssClassCvAddMore\" onclick=\"AddMoreVariantOptions(this); return false;\">" + getLocale(AspxItemsManagement, "Add More") + "</a>");
                }
                $("#dvCvForm table:first>tbody>tr>td").find("a.cssClassCvCloseMain").show();
            }
            else {
                $("#dvCvForm table:first>tbody>tr>td").find("a.cssClassCvCloseMain").show();
            }
            listImages.push(item.ImageFile);

            $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find(".cssclassCostVariantItemQuantity").val(item.CombinationQuantity);
            $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find(".cssClassPriceModifier").val(item.CombinationPriceModifier);
            $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find(".cssClassWeightModifier").val(item.CombinationWeightModifier);
            var priceMt = item.CombinationPriceModifierType == true ? 0 : 1;
            $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find(".cssClassPriceModifierType").find("option[value=" + priceMt + "]").prop("selected", "selected");
            var weightMt = item.CombinationWeightModifierType == true ? 0 : 1;
            $("#dvCvForm table:first").find(".cssClassTableCostVariant:last").find(".cssClassWeightModifierType").find("option[value=" + weightMt + "]").prop("selected", "selected");
            var xActive = item.CombinationIsActive == false ? 0 : 1;
            $("#dvCvForm table:first").find(".cssClassIsActive:last").find("option[value=" + xActive + "]").prop("selected", "selected");



        },
        GetCostVariantValues: function (costvarId, element, aspxCommonInfo) {
            var html = '';
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                async: false,
                url: aspxservicePath + "AspxCoreHandler.ashx/GetCostVariantValues",
                data: JSON2.stringify({ costVariantID: costvarId, aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            html += "<option value=" + item.CostVariantsValueID + ">" + item.CostVariantsValueName + "</option>";
                        };
                    } else {
                        html += "<option value='0'>" + getLocale(AspxItemsManagement, "No values") + "</option>";

                    }
                    $(element).html(html);
                    $("#dvCvForm table:first").find(">tbody>tr").each(function () {
                        if ($(this).find(".cssClassIsActive").find("option:selected").val() == 1) {
                            $(".cssClassItemQuantity").prop("disabled", "disabled");
                        }
                    });
                },
                error: function () {
                }
            });
        },
        BindCostVariantsOfItem: function (showMsg, alertMsg, aspxCommonInfo) {
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/GetCostVariantsOfItem",
                data: JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var length = msg.d.length;
                    if (length > 0) {
                        ItemMangement.vars.isItemHasCostVariant = true;
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            ItemMangement.CreateCombinationTableRow(index, item);
                            var ids = item.CombinationType.split('@');
                            var vids = item.CombinationValues.split('@');
                            var len = ids.length;
                            for (var id = 0; id < len; id++) {
                                ItemMangement.CreateCombinations(id, ids[id], vids[id], aspxCommonInfo);
                            }
                        };
                        $("#btnDeleteCostVariantCombination").show();
                        $("#btnCancelCostVariantCombination").show();
                        $("#btnBackCostVariantCombination").hide();
                    } else {
                        listImages = [];
                        $("#dvCvForm >table").hide();
                        $("#dvCostVarAdd").prev(".sfButtonwrapper").hide();
                        $("#dvCostVarAdd").show();
                        $("#btnBackCostVariantCombination").show();

                    }
                    if (showMsg) {
                        csscody.info(alertMsg);
                    }


                },
                error: function () {
                }
            });
        },
        DeleteCostVariantCombination: function (aspxCommonInfo) {
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/DeleteCostVariantForItem",
                data: JSON2.stringify({ aspxCommonObj: aspxCommonInfo, itemId: $("#ItemMgt_itemID").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var alertMsg = '<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item cost variant has been deleted successfully.") + '</p>';
                    ItemMangement.ResetCVHtml(true, alertMsg, aspxCommonInfo);
                },
                error: function () {
                    csscody.error('<h1>' + getLocale(AspxItemsManagement, "Error Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Failed to delete item cost variant") + '</p>');
                }
            });
        },
        ResetCVHtml: function (showMsg, alertMsg, aspxCommonInfo) {
            var html = '<table><thead><tr class="cssClassHeading"><td>' + getLocale(AspxItemsManagement, "Pos.") + '</td><td>' + getLocale(AspxItemsManagement, "Combination") + ' </td><td>' + getLocale(AspxItemsManagement, "Status") + '</td><td></td><td></td><td></td></tr></thead> <tbody> <tr> <td> <input size="3" class="cssClassVariantValue" value="0" type="hidden"><input size="3" class="cssClassDisplayOrder" type="text" value="1" disabled="disabled"> </td> <td class="cssClassTableCostVariant"><table><thead><tr><td><b>' + getLocale(AspxItemsManagement, "Cost Variant Name") + '</b></td><td><b>' + getLocale(AspxItemsManagement, "Cost Variant Values") + '</b></td><td></td></tr></thead><tr><td class="tdCostVariant"><select class="ddlCostVariantsCollection"></select><a href="#dvCvForm" class="cssClassCvAddMore sfBtn icon-addnew" style="margin-top:6px;" onclick="AddMoreVariantOptions(this); return false;">' + getLocale(AspxItemsManagement, "") + '</a> </td> <td class="tdCostVariantValues"> <select class="ddlCostVariantValues"><option>' + getLocale(AspxItemsManagement, "No values") + '</option></select></td> <td> <a href="#" class="cssClassCvClose" onclick="CloseCombinationRow(this); return false;" style="display:none;"><i class="imgDelete icon-delete" title=' + getLocale(AspxItemsManagement, "delete") + ' alt=' + getLocale(AspxItemsManagement, "delete") + ' /></i></a> </td> </tr> </table> <div class="cssclassItemCostVariant"> <div class="CostVariantItemQuantity"> <label>' + getLocale(AspxItemsManagement, "Quantity:") + '</label> <input type="text" class="cssclassCostVariantItemQuantity" value="1"/> </div> <div class="PriceModifier"><label>' + getLocale(AspxItemsManagement, "Cost Modifier Type:") + '</label> <input size="5" class="cssClassPriceModifier" type="text" value="0.00"> <select class="cssClassPriceModifierType"> <option value="0">%</option> <option value="1">' + currencyCodeEdit + '</option> </select></div> <div class="WeightModifier"> <label>' + getLocale(AspxItemsManagement, "Weight Modifier Type:") + '</label> <input size="5" class="cssClassWeightModifier" type="text" value="0.00" ><select class="cssClassWeightModifierType"><option value="0">%</option> <option value="1">' + getLocale(AspxItemsManagement, "lbs") + '</option> </select> </div> </div> </td> <td> <select class="cssClassIsActive"> <option value="1">' + getLocale(AspxItemsManagement, "Active") + '</option> <option value="0">' + getLocale(AspxItemsManagement, "Inactive") + '</option> </select> </td> <td> <span class="nowrap"> <button rel="popuprel2" class="classAddImages sfBtn" value="0" type="button"><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add Images") + '</span></button> </span> </td> <td> <span class="addButton"> <button type="button" value="0" class="cssclassAddCVariants sfBtn" onclick="AddCombinationListRow(this); return false;" ><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add More Cost Variants") + '</span></button> </span> </td> <td> <a href="#" class="cssClassCvCloseMain" onclick ="CloseMainCombinationList(this); return false;" ><i class="imgDelete icon-delete" title=' + getLocale(AspxItemsManagement, "delete") + ' alt=' + getLocale(AspxItemsManagement, "delete") + '/></i></a> </td> </tr> </tbody> </table>';
            $("#dvCvForm").html('').html(html);
            ItemMangement.GetAllCostVariants(showMsg, alertMsg, aspxCommonInfo);
        },
        GetAllCostVariants: function (showMsg, alertMsg, aspxCommonInfo) {
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/GetCostVariantForItem",
                data: JSON2.stringify({ aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var options = "<option value='0'>" + getLocale(AspxItemsManagement, "Select one") + "</option>";
                    var length = msg.d.length;
                    if (msg.d.length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            options += "<option value=" + item.CostVariantID + ">" + item.CostVariantName + "</option>";
                        };
                        $(".cssClassTableCostVariant:first").find(".ddlCostVariantsCollection").html(options);
                        ItemMangement.BindCostVariantsOfItem(showMsg, alertMsg, aspxCommonInfo);
                    } else {
                        $(".cssClassTableCostVariant:first").find(".ddlCostVariantsCollection").html(options);
                        listImages = [];
                        $("#dvCvForm >table").hide();
                        $("#dvCostVarAdd").prev(".sfButtonwrapper").hide();
                        $("#dvCostVarAdd").show();
                        $("#btnBackCostVariantCombination").show();
                    }

                },
                error: function () {
                }
            });


        },
        ItemTypes: function () {
            var $ajaxCall = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };


            var kit = function () {
                var components = [];
                var itemComponents = [];
                var kits = [];
                var itemKits = [];
                var COMPONENTTYPE = { RADIO: 2, CHECKBOX: 3 };
                var tempId = 0;
                var editCompontentId = 0;
                var editCompontentTempId = 0;
                var kitSummary = {};
                var basePrice = 0, baseWeight = 0;

                getInfoObj = function () {
                    tempId++;
                    var info = {
                        ItemKitID: 0,
                        ItemID: 0,
                        KitID: 0,
                        KitName: '',
                        KitComponentName: '',
                        KitComponentType: 0,
                        KitComponentID: 0,
                        KitComponentOrder: 0,
                        Price: 0,
                        Quantity: 1,
                        Weight: 0,
                        IsDefault: false,
                        KitOrder: 0, TempID: tempId
                    }
                    return info;
                }
                getDefault = function (kitsArr) {
                    for (var z in kitsArr) {
                        if (kitsArr[z].IsDefault) {
                            return kitsArr[z];
                        }
                    }
                    return null;
                };
                showSummary = function () {

                    basePrice = $(".cssClassPrice").val();

                    var startingfrom = kitSummary.LowPrice + basePrice;
                    var endto = kitSummary.HighPrice + basePrice;

                    var startingweight = kitSummary.LowWeight + baseWeight;
                    var endweight = kitSummary.HighWeight + baseWeight;
                    var defaultPrice = kitSummary.DefaultPrice + basePrice;
                    var defaultweight = kitSummary.DefaultWeight + baseWeight;
                    $("#dvKitSummary").show();
                    $("#lblItemPriceRange").html("<span class='cssClassFormatCurrency'>" + startingfrom + "</span>-<span class='cssClassFormatCurrency'>" + endto + "</span>");

                    $("#lblItemWeightRange").html("<span>" + startingweight + "</span>-<span>" + endweight + "</span>");
                    $("#lblDefaultItemPrice").html("<span class='cssClassFormatCurrency'>" + defaultPrice + "</span>");
                    $("#lblDefaultItemWeight").html("<span class='cssClassFormatCurrency'>" + defaultweight + "</span>");


                };
                getsummary = function () {


                    var lowWeight = 0;
                    var highWeight = 0;
                    var defaultWeight = 0;
                    var lowPrice = 0;
                    var hightPrice = 0;
                    var defaultPrice = 0;

                    var tempComponents = getunique(itemComponents);
                    tempComponents = tempComponents.sort(function (a, b) {
                        return a.KitComponentOrder - b.KitComponentOrder
                    })
                    var tlen = tempComponents.length;
                    for (var z = 0; z < tlen; z++) {

                        var kts = getItemKitsByComponent(tempComponents[z]);

                        var len = kts.length;
                        var price = kts.sort(function (a, b) {
                            return a.Price - b.Price;
                        });
                        lowPrice += price[0].Price;
                        hightPrice += price[len - 1].Price;
                        var defaultkit = getDefault(kts);
                        if (defaultkit != null) {
                            defaultPrice += defaultkit.Price;
                            defaultWeight += defaultkit.Weight;
                        }

                        var weight = kts.sort(function (a, b) {
                            return a.Weight - b.Weight;
                        });
                        lowWeight += weight[0].Weight;
                        highWeight += weight[len - 1].Weight;
                    }

                    return { LowWeight: lowWeight, HighWeight: highWeight, DefaultWeight: defaultWeight, LowPrice: lowPrice, HighPrice: hightPrice, DefaultPrice: defaultPrice };
                };

                mapComponentToItem = function (selectedComponent) {
                    var obj1 = getInfoObj();
                    var final = $.extend(obj1, selectedComponent);
                    itemComponents.push(final);
                };
                setDefaultKit = function (kitId, component) {
                    if (parseInt(component.KitComponentID) == 0) {
                        for (var z in itemComponents) {
                            if (itemComponents[z].TempID == component.TempID) {
                                if (itemComponents[z].KitID == kitId)
                                    itemComponents[z].IsDefault = true;
                                else
                                    itemComponents[z].IsDefault = false;
                            }
                        }
                    } else {
                        for (var z in itemComponents) {
                            if (itemComponents[z].KitComponentID == component.KitComponentID) {
                                if (itemComponents[z].KitID == kitId)
                                    itemComponents[z].IsDefault = true;
                                else
                                    itemComponents[z].IsDefault = false;
                            }
                        }
                    }

                }

                getUnMappedComponent = function (componentId, tempId) {

                    var obj = {};
                    if (componentId == 0) {
                        for (var i in components) {
                            if (tempId == components[i].TempID)
                                return components[i];
                        }

                    } else {
                        for (var i in components) {
                            if (componentId == components[i].KitComponentID)
                                return components[i];
                        }
                    }
                    return obj;
                }
                mapKitToItemComponent = function (componentId, tempId, cKit) {


                    var len = cKit.length;
                    var z = 0;
                    if (componentId == 0) {
                        for (z = 0; z < len; z++) {
                            var obj1 = getInfoObj();
                            obj1.TempID = tempId;
                            var cObj = getUnMappedComponent(componentId, tempId);
                            var finalObj = $.extend(obj1, cObj);
                            var tempholder = $.extend(finalObj, cKit[z])
                            itemComponents.push(tempholder);
                        }
                    } else {
                        for (z = 0; z < len; z++) {
                            var obj1 = getInfoObj();
                            obj1.TempID = tempId;
                            var cObj = getUnMappedComponent(componentId, 0);
                            var finalObj = $.extend(obj1, cObj);
                            var tempholder = $.extend(finalObj, cKit[z])
                            itemComponents.push(tempholder);
                        }
                    }



                };
                getkitsFromItem = function (kitComponent) {

                    var usedIds = [];
                    if (kitComponent.KitComponentID == 0) {

                        for (var z in itemComponents) {
                            if (itemComponents[z].TempID == kitComponent.TempID) {
                                usedIds.push(parseInt(itemComponents[z].KitID));
                            }
                        }
                    }
                    else {
                        for (var z in itemComponents) {
                            if (itemComponents[z].KitComponentID == kitComponent.KitComponentID) {
                                usedIds.push(parseInt(itemComponents[z].KitID));
                            }
                        }
                    }
                    return usedIds;
                };
                rebuild = function () {

                    buidItemComponetsUI();
                    $("#dvExistingComponents").hide();
                    $("#dvItemComponentsWrapper").show();
                    $("#dvItemKitEdit").hide();
                    $("#dvTopButton").show();
                    if ($("input[name^=componentdefaultkit]").is(":checked"))
                        $("input[name^=componentdefaultkit]:checked").trigger("click");
                };
                getkitList = function (kitComponent) {
                    var availablekits = [];
                    var kitIds = getkitsFromItem(kitComponent);

                    $.each(kits, function (index, item) {

                        var push = false;
                        for (var z in kitIds) {

                            if (item.KitID == kitIds[z]) {
                                push = true;
                                break;
                            }
                        }
                        if (!push) {
                            availablekits.push(item);
                        }
                    });
                    return availablekits;
                };
                buildkitsUI = function (kitComponent) {

                    $("#tblAvailableKits tbody").html('');

                    var avKits = getkitList(kitComponent);
                    for (var y in avKits) {
                        var item = avKits[y];
                        var tr = "";
                        tr += "<tr>";
                        tr += "<td><input type='checkbox' /></td>";
                        tr += "<td>" + item.KitName + "</td>";
                        tr += "<td class='cssClassFormatCurrency'>" + item.Price + "</td>";
                        tr += "<td>" + item.Weight + "</td>";
                        tr += "</tr>";
                        var $tr = $(tr);
                        $tr.data('item', item);
                        $("#tblAvailableKits tbody").append($tr);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    }
                };
                getItemKitsByComponent = function (component) {



                    var len = itemComponents.length;
                    var items = [];

                    if (component.KitComponentID == 0) {
                        for (var z = 0; z < len; z++) {

                            if (itemComponents[z].TempID == component.TempID)
                                items.push(itemComponents[z]);
                        }

                    } else {
                        for (var z = 0; z < len; z++) {

                            if (itemComponents[z].KitComponentID == component.KitComponentID)
                                items.push(itemComponents[z]);
                        }
                    }
                    return items;

                };
                getComponent = function (id) {

                    var len = itemComponents.length;
                    var component = {};
                    var index = 0;
                    for (var z = 0; z < len; z++) {

                        if (itemComponents[z].ComponentID == id) {
                            component = itemComponents[z];
                            index = z;
                            break;
                        }
                    }
                    return { Index: index, Item: component };

                };
                setDefaulItemKitOnComponent = function (componentId, defaultKitId) {

                };

                getComponents = function () {

                    var ids = getuniqueIds(itemComponents);

                    var availableComponents = getuniqueComponents(ids);
                    return availableComponents;
                };
                var getAllData = function () {
                    kits = [];
                    components = [];
                    itemComponents = [];
                    itemkitDeleted = [];
                    $ajaxCall("GetKits", JSON2.stringify({ commonInfo: aspxCommonObj() }), function (data) {
                        if (data.d) {
                            kits = data.d;
                        }
                    }, null);

                    $ajaxCall("GetComponents", JSON2.stringify({ commonInfo: aspxCommonObj() }), function (data) {
                        if (data.d) {
                            components = data.d;
                        }

                    }, null);

                    if (parseInt($("#ItemMgt_itemID").val()) != 0) {
                        $ajaxCall("GetItemKits", JSON2.stringify({ itemID: parseInt($("#ItemMgt_itemID").val()), commonInfo: aspxCommonObj() }), function (data) {
                            if (data.d) {
                                itemComponents = data.d;
                                buidItemComponetsUI();
                            }
                        }, null);
                    }





                };

                addComponent = function () {

                    var name = $("#txtNewComponent").val();
                    var type = $("#ddlComponentType").val();
                    var _comp = getInfoObj();
                    _comp.KitComponentName = name;
                    _comp.KitComponentType = type;
                    _comp.TempID = tempId;
                    itemComponents.push(_comp);
                    components.push(_comp);
                    buidItemComponetsUI();
                };
                getunique = function (x) {

                    var unique = [];
                    var distinct = [];
                    for (var i in x) {

                        if (x[i].KitID == 0) {

                            if (x[i].KitComponentID != 0) {
                                unique.push(x[i].KitComponentID);
                            }

                            distinct.push(x[i]);
                        }


                    }
                    return distinct;
                }
                getuniqueIds = function (x) {

                    var unique = {};
                    var distinct = [];
                    for (var i in x) {
                        if (typeof (unique[x[i].KitComponentID]) == "undefined") {
                            distinct.push(x[i].KitComponentID);
                        }
                        unique[x[i].KitComponentID] = 0;
                    }
                    return distinct;
                };
                getuniqueComponents = function (ids) {
                    var distinct = [];
                    for (var i in components) {
                        var add = true;
                        for (var j in ids) {
                            if (components[i].KitComponentID == ids[j]) {
                                add = false;
                                break;
                                distinct.push(components[i]);
                            }
                        }
                        if (add) {
                            distinct.push(components[i]);
                        }
                    }
                    return distinct;
                };
                buidItemComponetsUI = function () {
                    $("#dvItemComponentsWrapper").html('');

                    var tempComponents = getunique(itemComponents);
                    tempComponents = tempComponents.sort(function (a, b) {
                        return a.KitComponentOrder - b.KitComponentOrder
                    });
                    var len = tempComponents.length;
                    var dvItemComponentsWrapperHtml = "";
                    var d = "";
                    for (var z = 0; z < len; z++) {
                        var html = ui.ItemComponent();

                        var $elem = $(html);
                        $elem.data('item', tempComponents[z]);

                        $elem.find(".cssClassHeader h3 label").html(tempComponents[z].KitComponentName);
                        var kts = getItemKitsByComponent(tempComponents[z]);
                        buildItemComponentKits($elem.find("table tbody"), kts)
                        d = d + $elem.html();
                        //$("#dvItemComponentsWrapper").append($elem);
                    }
                    setTimeout(function (e) { 
                        $("#dvItemComponentsWrapper").append(d);
                    },500);
                    enableSorting();
                }
                buildItemComponentKits = function ($appendTo, componentKits) {

                    var len = componentKits.length;
                    componentKits = componentKits.sort(function (a, b) {
                        return a.KitOrder - b.KitOrder
                    });

                    for (var z = 0; z < len; z++) {

                        var kIt = componentKits[z];
                        if (kIt.KitID != 0) {
                            var tr = "";
                            tr += "<tr>";
                            tr += "<td>" + kIt.KitName + "</td>";
                            tr += "<td>" + kIt.Quantity + "</td>";
                            tr += "<td class='cssClassFormatCurrency'>" + kIt.Price + "</td>";
                            tr += "<td>" + kIt.Weight + "</td>";
                            tr += "<td> <input type='radio'  name='componentdefaultkit_" + kIt.KitComponentName + "' checked=" + kIt.IsDefault + " /></td>";
                            tr += "<td><label class='kitEdit'><i class='icon-edit'></i></label><label class='kitDelete'><i class='icon-delete'></i></label></td>";
                            tr += "</tr>";
                            var elem = $(tr);
                            elem.data('item', kIt);
                            $appendTo.append(elem);
                        }


                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                };

                function findPropertyName(object, property_value, strict) {
                    if (typeof strict == 'undefined') {
                        strict = false;
                    };
                    for (property in object) {
                        if ((strict && object[property] === property_value) ||
                            (!strict && object[property] == property_value)) {
                            return property;
                        }
                    }
                    return false;
                }

                getKitsDetail = function () {


                };

                showExistingComponents = function () {
                    $("#tblAvailableComponents tbody").html('');
                    var list = getComponents();
                    if (list.length == 0) {
                        $("#tblAvailableComponents tbody").append("<tr><td colspan='4'>No More Available. </td></tr>");
                    }
                    for (var i in list) {
                        var item = list[i];
                        var tr = "";
                        tr += "<tr>";
                        tr += "<td>" + item.KitComponentName + "</td>";
                        var type = findPropertyName(COMPONENTTYPE, item.KitComponentType, true);
                        tr += "<td>" + type + "</td>";
                        tr += "<td>" + /*getKitsDetail()*/(item.Kits).substring(0,item.Kits.length-1)  + "</td>";
                        tr += "<td><label class='icon-addnew addCompToItem'> </label></td>";
                        tr += "</tr>";
                        var elem = $(tr);
                        elem.data('item', item);
                        $("#tblAvailableComponents tbody").append(elem);
                    }

                };

                editComponent = function () {
                    var name = $.trim($("#txtNewComponent").val());
                    var type = parseInt($("#ddlComponentType").val());
                    for (var z = 0; z < itemComponents.length; z++) {
                        var item = itemComponents[z];
                        if (editCompontentId == 0) {
                            if (item.KitComponentID == editCompontentId && item.TempID == editCompontentTempId) {
                                item.KitComponentName = name;
                                item.KitComponentType = type;
                            }
                        } else if (item.KitComponentID == editCompontentId) {
                            item.KitComponentName = name;
                            item.KitComponentType = type;

                        }

                    }

                };
                var itemkitDeleted = [];
                removeComponent = function (componentInfo) {
                    var len = itemComponents.length;
                    var temp = itemComponents.concat();
                    var counter = 0; ;
                    for (var z = 0; z < len; z++) {
                        var item = temp[z];
                        if (componentInfo.KitComponentID == 0) {
                            if (item.KitComponentID == componentInfo.KitComponentID && item.TempID == componentInfo.TempID) {
                                itemComponents.splice(z, 1);
                            }
                        } else if (item.KitComponentID == componentInfo.KitComponentID) {
                            itemkitDeleted.push(item);
                            var arrIndex = z - counter;
                            itemComponents.splice(arrIndex, 1);
                            counter++;
                        }

                    }


                };
                removeKit = function (kitInfo) {
                    var len = itemComponents.length;
                    var temp = itemComponents.concat();
                    var counter = 0; ;
                    for (var z = 0; z < len; z++) {
                        var item = temp[z];
                        if (item.KitID == kitInfo.KitID && item.KitComponentID == kitInfo.KitComponentID) {
                            itemkitDeleted.push(item);

                            var arrIndex = z - counter;
                            itemComponents.splice(arrIndex, 1);
                            counter++;
                            break;
                        }

                    }


                };

                addKitToComponent = function () {



                };

                buildSelectedKit = function (selectedKits) {

                    $("#dvEditKits").html('');
                    var html = ui.KitConfig();
                    var len = selectedKits.length;
                    for (var i = 0; i < len; i++) {
                        var $elem = $(html);
                        var item = selectedKits[i];

                        $elem.find(".cssClassHeader label").html(item.KitName);
                        $elem.find(".cssClassHeader label").attr('kid', item.KitID);
                        $elem.find("input[name=kitquantity]").val(item.Quantity);
                        $elem.find("input[name=kitprice]").val(item.Price);
                        $elem.find("select.kitpricechanger").find("option[value=0]").attr('value', item.Price);
                        $elem.find("input[name=kitweight]").val(item.Weight);
                        $elem.find("select.kitweightchanger").find("option[value=0]").attr('value', item.Weight);
                        $("#dvEditKits").append($elem);
                    }

                    $("input[name=kitquantity]").DigitOnly("input[name=kitquantity]", '');
                    $("input[name=kitprice] ,input[name=kitweight]").DigitAndDecimal('input[name=kitprice] ,input[name=kitweight]', '');
                };

                getKitValues = function () {
                    var _kts = [];
                    $("#dvEditKits .cssClassCommonBox").each(function (index, item) {
                        var _kit = {};
                        _kit.KitID = parseInt($(this).find(".cssClassHeader label").attr('kid'));
                        _kit.KitName = $(this).find(".cssClassHeader label").html();
                        _kit.Quantity = parseInt($(this).find("input[name=kitquantity]").val());
                        _kit.Price = parseFloat($(this).find("input[name=kitprice]").val());
                        _kit.Weight = parseFloat($(this).find("input[name=kitweight]").val());
                        _kit.IsDefault = $(this).find("input[type=radio]").is(":checked");
                        _kts.push(_kit);

                    });
                    return _kts;
                };

                showAvailableKit = function () { };
                addNewKit = function () { };
                sortKitProducts = function () { };

                editKitProduct = function (kitinfo) {

                    for (var z = 0; z < itemComponents.length; z++) {
                        var item = itemComponents[z];
                        if (item.KitID == kitinfo.KitID && item.KitComponentID == kitinfo.KitComponentID) {
                            item.Quantity = parseInt(kitinfo.Quantity);
                            item.Price = parseFloat(kitinfo.Price);
                            item.Weight = parseFloat(kitinfo.Weight);
                            item.IsDefault = kitinfo.IsDefault;
                            break;
                        }

                    }

                };

                calcAveragePriceNWeight = function () {

                };

                getSorted = function () {
                    var sortedList = [];
                    var componentIndex = 1
                    $("#dvItemComponentsWrapper .cssClassCommonBox").each(function (index, item) {
                        var kitIndex = 1;
                        var component = $(this).data('item');
                        component.KitComponentOrder = componentIndex;
                        sortedList.push(component);
                        componentIndex++;
                        $(this).find("table tbody tr").each(function (idx, row) {
                            var kit = $(this).data('item');
                            kit.KitOrder = kitIndex;
                            sortedList.push(kit);
                            kitIndex++;
                        });
                    });
                    itemComponents = sortedList;
                    return { KitConfig: sortedList, KitDeleted: itemkitDeleted };
                };

                ui = function () {
                    first = function () {
                        var firstUI = "";
                        firstUI += "<div id='dvTopButton' class='sfButtonwrapper'> <button type='button' id='btnAddNewComponent' class='icon-addnew' > New Component</button> <button type='button' id='btnAddOldComponent' class='icon-addnew' > Existing Component</button> </div> ";
                        firstUI += "<div id='dvKitSummary' style='display:none;' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label >Kit Summary</label></h3></div>";
                        firstUI += "<table width='100%' border='0'><tbody><tr><td>Price Range:</td><td><label id='lblItemPriceRange'></label> </td>";
                        firstUI += "<td>Default:</td><td><label id='lblDefaultItemPrice'></label></td></tr>";
                        firstUI += "  <tr><td>Weight Range</td><td><label id='lblItemWeightRange'></label> </td>";
                        firstUI += "<td>Default:</td><td><label id='lblDefaultItemWeight'></label></td></tr>";
                        firstUI += "</tbody> </table>";
                        firstUI += "</div>";

                        firstUI += "<div id='dvItemComponentsWrapper'></div>";
                        firstUI += "<div id='dvExistingComponents' style='display:none;' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label>Existing Component</label></h3></div>";
                        firstUI += "<table width='100%' border='0' class='cssClassPadding'  id='tblAvailableComponents'><thead><tr><td>Name</td><td>Type</td><td>Kits</td><td></td></tr></thead><tbody></tbody> </table>";
                        firstUI += " <div class='sfButtonwrapper'><button type='button' id='btnBacktoMainForm' class='icon-close' >" + getLocale(AspxItemsManagement, "Back") + "</button>  </div>";

                        firstUI += "</div>";


                        firstUI += "<div id='dvItemKitEdit' style='display:none;' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label>Add Kit</label></h3></div>";
                        firstUI += "<div id='dvEditKits'></div>";
                        firstUI += "<div class='sfButtonwrapper'><button type='button' id='btnKitSave' class='icon-save' >Save</button> <button type='button' id='btnKitSaveBack' class='icon-close' >" + getLocale(AspxItemsManagement, "Back") + "</button>  </div></div>";

                        firstUI += "<div id='dvAvailableKit' style='display:none;' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label>Kits</label></h3></div>";
                        firstUI += "<div ><table width='100%' border='0' class='cssClassPadding'  id='tblAvailableKits'><thead><tr><td></td><td>Name</td><td>Price</td><td>Weight</td></tr></thead><tbody></tbody> </table></div>";
                        firstUI += "<div class='sfButtonwrapper'><button type='button' id='btnAddKitToitemComponent'class='icon-addnew' >Add to Component</button> <button type='button' id='btnBacktoMain' class='icon-close' >" + getLocale(AspxItemsManagement, "Back") + "</button> </div></div>";




                        firstUI += "<div id='dvNewComponent' style='display:none;' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label>Add New Component</label></h3></div>";
                        firstUI += "<table width='100%' border='0' class='cssClassPadding' id='tblNewComponent'><tbody><tr><td>Name</td><td><input type='text' class='sfInputbox' id='txtNewComponent'/> <span style='color:red' id='spnReqName'>*</span></td></tr>";
                        firstUI += "<tr><td>Input Type</td><td><select id='ddlComponentType'><option value='2'>Radio</option><option value='3'>CheckBox</option></select> </td></tr></tbody> </table>";
                        firstUI += "<div class='sfButtonwrapper'><button type='button' id='btnComponentSave' class='icon-save' >Save</button> <button type='button' id='btnComponentCancel' class='icon-close' >Cancel</button>  </div></div>";


                        return firstUI;
                    }
                    existingComponents = function () {
                        var newcomponentUI = "";
                        newcomponentUI += "<div class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label>Add New Component</label></h3></div>";
                        newcomponentUI += "<table width='100%' border='0' class='cssClassPadding' id='tblNewComponent'><tbody><tr><td>Name</td><td><input type='text' class='sfInputbox' id='txtNewComponent'/> </td></tr>";
                        newcomponentUI += "<tr><td>Input Type</td><td><select id='ddlComponentType'><option value='1'>DropDown</option><option value='2'>Radio</option><option value='3'>CheckBox</option></select> </td></tr></tbody> </table>";
                        newcomponentUI += "<div class='sfButtonwrapper'><button type='button' id='btnComponentSave' >Save</button> <button type='button' id='btnComponentCancel' >Cancel</button>  </div></div>";

                        return newcomponentUI;
                    }
                    itemComponent = function () {
                        var itemsCompoKitUI = "";
                        itemsCompoKitUI += "<div class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label >ComponentNam here</label></h3><div><label class='ComponentEdit icon-edit' ><a href='javascript:'></a></label><label  class='itemComponentDelete icon-delete'><a href='javascript:'></a></label></div>";

                        itemsCompoKitUI += "";
                        itemsCompoKitUI += "<table width='100%' border='0' class='cssClassPadding'><thead><tr><td>Item</td><td>Quantity</td><td>Price</td><td>Weight</td><td>Default</td><td></td></tr></thead><tbody></tbody></table>";
                        itemsCompoKitUI += "<div class='sfButtonwrapper'><button type='button' class='icon-addnew' > Kit </button></div>";
                        itemsCompoKitUI += "</div>";
                        return itemsCompoKitUI;
                    }

                    itemKitConfig = function () {
                        var itemKitConfigHtml = "";
                        itemKitConfigHtml += "<div class='cssClassCommonBox Curve'><div class='cssClassHeader'><h3><label >Kit here</label></h3></div>";

                        itemKitConfigHtml += "<table width='100%' border='0' class='cssClassPadding'><thead>";
                        itemKitConfigHtml += "<tr><td>Quantity:</td>";
                        itemKitConfigHtml += "<td><input type='text' value='1' name='kitquantity'/></td></tr>";
                        itemKitConfigHtml += "<tr><td>Kit Price:</td>";
                        itemKitConfigHtml += "<td><select class='kitpricechanger' ><option value=\"0\" data-hide=\"true\">Kit Price</option><option value='1'>Fixed</option></select></td></tr>";
                        itemKitConfigHtml += "<tr class='modifyPrice' style='display:none;' ><td>Price:</td>";
                        itemKitConfigHtml += "<td><input type='text' value='1' name='kitprice'/></td></tr>";

                        itemKitConfigHtml += "<tr><td>Kit Weight:</td>";
                        itemKitConfigHtml += "<td><select class='kitweightchanger' ><option value=\"0\" data-hide=\"true\">Kit Weight</option><option value='1'>Fixed</option></select></td></tr>";
                        itemKitConfigHtml += "<tr class='modifyWeight' style='display:none;' ><td>Weight:</td>";
                        itemKitConfigHtml += "<td><input type='text' value='1' name='kitweight'/></td></tr>";
                        itemKitConfigHtml += "<tr><td>IsDefault:</td>";
                        itemKitConfigHtml += "<td><input type='radio'  name='kitisdefault'/></td></tr>";
                        itemKitConfigHtml += "</thead><tbody></tbody></table>";
                        itemKitConfigHtml += "</div>";
                        return itemKitConfigHtml;
                    }

                    return { First: first, ItemComponent: itemComponent, KitConfig: itemKitConfig };
                } ();

                enableSorting = function () {

                    var fixHelperModified = function (e, tr) {

                        var $originals = tr.children();
                        var isChecked = tr.find("input[type=radio]").is(":checked")
                        var indx = tr.find("input[type=radio]").parent('td').index();
                        var $helper = tr.clone();
                        $helper.children().each(function (index) {
                            $(this).width($originals.eq(index).width())

                            $(this).find("input[type=radio]").attr('checked', 'checked');
                            if ($originals.eq(index).find("input[type=radio]").length > 0) {
                                var isChecked = $originals.eq(index).find("input[type=radio]").is(":checked")
                                if (isChecked)
                                    $(this).find("input[type=radio]").attr('checked', 'checked');
                            }

                        });

                        return $helper;

                    };
                    updateIndex = function (e, ui) {
                        getSorted();
                        $('td.index', ui.item.parent()).each(function (i) {
                        });
                    };

                    $("#dvItemComponentsWrapper table tbody").sortable({
                        helper: 'original',
                        stop: updateIndex
                    }).disableSelection();

                    $("#dvItemComponentsWrapper").sortable({
                        helper: 'original',
                        stop: updateIndex
                    }).disableSelection();
                }

                UIEvents = function () {





                    $("body").on("click", "input[name^=componentdefaultkit]", function () {

                        var selectedKit = $(this).parents("tr:eq(0)").data('item');
                        setDefaultKit(selectedKit.KitID, $(this).parents(".cssClassCommonBox:eq(0)").data("item"));
                        kitSummary = getsummary();
                        showSummary();
                        return false;
                    });

                    $("body").on("click", "#tblAvailableComponents tbody .addCompToItem", function () {

                        mapComponentToItem($(this).parents("tr:eq(0)").data('item'));
                        rebuild();
                        return false;
                    });


                    $("body").on("click", ".ComponentEdit", function () {

                        var component = $(this).parents("div.cssClassCommonBox:eq(0)").data('item');
                        $("#txtNewComponent").val(component.KitComponentName);
                        $("#ddlComponentType").val(component.KitComponentType);

                        editCompontentId = component.KitComponentID;
                        editCompontentTempId = component.TempID;
                        $("#dvNewComponent").show();
                        $("#dvItemComponentsWrapper").hide();
                        return false;
                    });

                    $("body").on("click", ".itemComponentDelete", function () {
                        var component = $(this).parents("div.cssClassCommonBox:eq(0)").data('item');
                        removeComponent(component);
                        rebuild();
                        return false;
                    });



                    $("body").on("click", "#dvItemComponentsWrapper .sfButtonwrapper button", function () {

                        editKit = false;
                        var currentComponent = $(this).parents("div:eq(2)").data('item');
                        $("#btnKitSave").data('cid', currentComponent.KitComponentID);

                        if (currentComponent.KitComponentID == 0)
                            $("#btnKitSave").data('tid', currentComponent.TempID);
                        else
                            $("#btnKitSave").data('tid', 0);

                        buildkitsUI(currentComponent);

                        $("#dvItemComponentsWrapper").hide();
                        $("#dvTopButton").hide();
                        $("#dvAvailableKit").show();
                        $("#dvItemKitEdit,#dvExistingComponents").hide();
                        return false;
                    });
                    $("body").on("click", "#btnBacktoMain", function () {
                        $("#dvItemComponentsWrapper").show();
                        $("#dvTopButton").show();
                        $("#dvAvailableKit,#dvItemKitEdit,#dvExistingComponents").hide();
                        return false;
                    });
                    $("body").on("click", "#btnAddKitToitemComponent", function () {
                        if ($("#tblAvailableKits tbody tr input:checkbox").is(":checked")) {
                            $("#dvItemKitEdit").show();
                            var selectedKits = [];
                            $("#tblAvailableKits tbody tr").each(function () {
                                if ($(this).find("td:first").find("input[type=checkbox]").is(":checked")) {
                                    var item = $(this).find("td:first").find("input[type=checkbox]:checked").parents("tr:eq(0)").data('item');
                                    selectedKits.push(item);
                                }
                            });
                            buildSelectedKit(selectedKits);

                        }
                        return false;
                    });

                    $("body").on("click", "#btnKitSave", function () {
                        var maxPriceVal = $("#divGeneralContent table input[type=text][title=\"Price ToolTip\"]").attr("maxlength");
                        var form = $("#form1").validate({
                            rules: {
                                kitquantity: { required: true, digits: true },
                                kitprice: { required: true, number: true },
                                kitweight: { required: true, number: true }
                            },
                            messages: {
                                kitquantity: "*",
                                kitprice: "*",
                                kitweight: "*"
                            },
                            ignore: ':hidden'
                        });
                        if (form.form()) {
                            var userdefinedKits = getKitValues();
                            var cid = $(this).data('cid');
                            var tid = $(this).data('tid');
                            if (editKit) {
                                userdefinedKits[0].KitComponentID = cid;
                                userdefinedKits[0].TempID = tid;
                                editKitProduct(userdefinedKits[0]);
                                editKit = false;
                            } else {

                                mapKitToItemComponent(cid, tid, userdefinedKits);
                            }
                            rebuild();
                            $("#dvAvailableKit").hide();
                        }
                        return false;
                    });
                    $("body").on("click", "#btnKitSaveBack", function () {

                        $("#dvItemKitEdit,#dvExistingComponents").hide()
                        $("#dvItemComponentsWrapper,#dvTopButton").show();
                        return false;
                    });

                    $("body").on("change", ".kitpricechanger", function () {
                        var kitPriceSelectedOption = $(".kitpricechanger option:selected").data('hide');
                        if (typeof(kitPriceSelectedOption) != 'undefined' && kitPriceSelectedOption) {
                            $(this).parents('tr:eq(0)').next("tr.modifyPrice").hide();
                            $(this).parents('tr:eq(0)').next("tr.modifyPrice").find("input[type=text]").val(isNaN(parseInt($(this).val())) ? "0" : parseInt($(this).val()));
                        } else {
                            $(this).parents('tr:eq(0)').next("tr.modifyPrice").show();

                        }
                    });
                    $("body").on("change", ".kitweightchanger", function () {
                        var kitWeightSelectedOption = $(".kitweightchanger option:selected").data('hide');
                        if (typeof(kitWeightSelectedOption) != 'undefined' && kitWeightSelectedOption) {
                            $(this).parents('tr:eq(0)').next("tr.modifyWeight").hide();
                            $(this).parents('tr:eq(0)').next("tr.modifyWeight").find("input[type=text]").val(isNaN(parseInt($(this).val())) ? "0" : parseInt($(this).val()));
                        } else {
                            $(this).parents('tr:eq(0)').next("tr.modifyWeight").show();
                        }
                    });


                    var editKit = false;
                    $("body").on("click", "#dvItemComponentsWrapper .kitEdit", function () {

                        var x = $(this).parents("tr:eq(0)").data('item');
                        editKit = true;
                        var xArr = [];
                        xArr.push(x);
                        $("#btnKitSave").data('cid', x.KitComponentID);
                        if (x.KitComponentID == 0)
                            $("#btnKitSave").data('tid', x.TempID);
                        else
                            $("#btnKitSave").data('tid', 0);

                        buildSelectedKit(xArr);
                        $("#dvExistingComponents").hide();
                        $("#dvItemComponentsWrapper").hide();
                        $("#dvItemKitEdit").show();
                        $("#dvTopButton").hide();
                        return false;
                    });
                    $("body").on("click", "#dvItemComponentsWrapper .kitDelete", function () {

                        var x = $(this).parents("tr:eq(0)").data('item');
                        removeKit(x);
                        rebuild();
                        return false;
                    });
                    $("body").on("click", "#btnComponentSave", function () {

                        if ($.trim($("#txtNewComponent").val()) == '') {
                            $("#spnReqName").show();
                            return false;
                        } else {
                            $("#spnReqName").hide();
                        }
                        if (editCompontentId == 0 && editCompontentTempId == 0) {
                            addComponent();
                        } else {

                            editComponent();
                            rebuild();
                            editCompontentId = 0;
                            editCompontentTempId = 0;
                        }
                        $("#txtNewComponent").val('');
                        $("#ddlComponentType").val(1);
                        $("#dvItemComponentsWrapper,#dvTopButton").show();
                        $("#dvAvailableKit,#dvItemKitEdit,#dvExistingComponents,#dvNewComponent").hide();
                        return false;
                    });
                    $("body").on("click", "#btnComponentCancel", function () {
                        editCompontentId = 0;
                        editCompontentTempId = 0;
                        $("#txtNewComponent").val('');
                        $("#ddlComponentType").val(1);
                        $("#dvNewComponent").hide();
                        $("#dvItemComponentsWrapper").show();
                        return false;
                    });

                    $("body").on("click", "#btnAddNewComponent", function () {
                        editCompontentId = 0;
                        editCompontentTempId = 0;
                        $("#dvNewComponent").show();
                        $("#dvItemComponentsWrapper").hide();
                        $("#dvAvailableKit,#dvItemKitEdit,#dvExistingComponents").hide();
                        return false;
                    });

                    $("body").on("click", "#btnBacktoMainForm", function () {

                        $("#dvExistingComponents,#dvItemKitEdit").hide();
                        $("#dvItemComponentsWrapper").show();
                        $("#dvTopButton").show();
                        return false;
                    });
                    $("body").on("click", "#btnAddOldComponent", function () {
                        $("#dvExistingComponents").show();
                        $("#dvItemComponentsWrapper").hide();
                        $("#dvItemKitEdit").hide();
                        $("#dvTopButton").hide();
                        $("#dvAvailableKit,#dvItemKitEdit").hide()
                        showExistingComponents();
                        return false;
                    });


                } ();

                init = function () {
                    getAllData();
                };

                return { Init: init, UI: ui, Get: getSorted };
            } ();

            var grouped = function () { };

            return { Kit: kit };
        } (),
        GroupItems: function () {

            BuildGrpForm = function (hrefObj) {
                var html = '';
                html += '<div class="sfGridwrapper">'
                 + '<div class="sfGridWrapperContent"><div class="cssClassSearchPanel sfFormwrapper">'
              + '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>'
              + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item SKU") + ' :</label><input type="text" id="txtAssociatedItemSKU" class="sfTextBoxSmall" /></td>'
              + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item Name") + ' :</label><input type="text" id="txtAssociatedItemName" class="sfTextBoxSmall" /><select style="display:none"class="sfcurrencyListGroup" id="ddlCurrencyGroup"></td>'
               + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item Type") + ' :</label><select id="ddlSelectAssociatedItemType" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select></td>'
               + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Attribute Set Name") + ' :</label><select id="ddlSelectAssociatedAttributeSetName" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select></td>'
                + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Category Name") + ' :</label><select id="allCatags" class="sfListmenu"> <option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select></td>'
              + '<td><div class="sfButtonwrapper cssClassPaddingNone"><p><button type="button" id="btnSearchAssociatedItems"><span>' + getLocale(AspxItemsManagement, "Search") + '</span></button></p></div></td>'
              + '</tr></table>'
              + '</div></div></div>'

          + '<table id="gdvAssociatedItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table>';
                return html;

            };
            Ajaxcall = function (url, data, successFxn, errorFxn) {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: url,
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: successFxn,
                    error: errorFxn
                });
            };
            BindAttributeSets = function () {
                var Elements = '';
                $("#ddlSelectAssociatedAttributeSetName").html('');
                $.each(attributeSetArray, function (index, item) {
                    Elements += '<option value="' + item.AttributeSetID + '">' + item.AliasName + '</option>';
                });
                $("#ddlSelectAssociatedAttributeSetName").append(Elements);
                $('#ddlSelectAssociatedAttributeSetName option:contains("Default")').prop('selected', true);
                $('#ddlSelectAssociatedAttributeSetName').attr({ 'disabled': 'disabled' });
            };
            BindItemTypes = function () {
                var htmlElement = '';
                $("#ddlSelectAssociatedItemType").html('');
                $.each(itemTypeArray, function (index, item) {
                    htmlElement += '<option value="' + item.ItemTypeID + '">' + item.ItemTypeName + '</option>';
                });
                $("#ddlSelectAssociatedItemType").append(htmlElement);
                $('#ddlSelectAssociatedItemType option:contains("Simple Item")').prop('selected', true);
                $('#ddlSelectAssociatedItemType').attr({ 'disabled': 'disabled' });

            };

            BindEvents = function () {
                $("#btnSearchAssociatedItems").on("click", function () {
                    ItemMangement.GroupItems.Search();
                    return false;
                });

            };
            GetAllCategory = function () {
                var isActive = true;
                this.Ajaxcall(aspxservicePath + "AspxCommonHandler.ashx/GetAllCategoryForSearch", JSON2.stringify({ prefix: '---', isActive: isActive, aspxCommonObj: aspxCommonObj() }), function (msg) {
                    BindAllCategory(msg);
                }, function () {
                    csscody.error('<h1>' + getLocale(AspxItemsManagement, "Error Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Failed to load Item Categories") + '</p>');
                });

            };
            BindAllCategory = function (msg) {
                var Elements = '';
                $.each(msg.d, function (index, item) {
                    Elements += '<option value="' + item.CategoryID + '">' + item.LevelCategoryName + '</option>';
                });
                $("#allCatags").append(Elements);

            };

            GetGrpProduct = function (selfItemID, itemSKU, itemName, itemTypeID, attributeSetID, categoryID) {
                var itemDetails = {
                    serviceBit: serviceBit,
                    selfItemId: selfItemID,
                    itemSKU: itemSKU,
                    itemName: itemName,
                    itemTypeID: itemTypeID,
                    attributeSetID: attributeSetID
                };
                if ((categoryID == "") || (categoryID == null)) {
                    categoryID = 0;
                }
                ItemMangement.config.method = "GetAssociatedItemsList";
                ItemMangement.config.data = { IDCommonObj: itemDetails, categoryID: categoryID, aspxCommonObj: aspxCommonObj() };
                var data = ItemMangement.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvAssociatedItems_pagesize").length > 0) ? $("#gdvAssociatedItems_pagesize :selected").text() : 10;

                $("#gdvAssociatedItems").sagegrid({
                    url: ItemMangement.config.baseURL,
                    functionMethod: ItemMangement.config.method,
                    colModel: [
                 { display: getLocale(AspxItemsManagement, 'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkAssociatedControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                 { display: getLocale(AspxItemsManagement, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                 { display: getLocale(AspxItemsManagement, 'SKU'), name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'ItemType ID'), name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Type'), name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'AttributeSet ID'), name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Attribute Set Name'), name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: getLocale(AspxItemsManagement, 'List Price'), name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: getLocale(AspxItemsManagement, 'Quantity'), name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Visibility'), name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Active?'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: getLocale(AspxItemsManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                 { display: getLocale(AspxItemsManagement, 'IDTobeChecked'), name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                 { display: getLocale(AspxItemsManagement, 'CurrencyCode'), name: 'currency_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 14: { sorter: false} }
                });
                SetGrpProduct(selfItemID);
            };
            SetGrpProduct = function () {
                ItemMangement.config.url = ItemMangement.config.baseURL + "GetAssociatedCheckIDs";
                ItemMangement.config.data = JSON2.stringify({ ItemID: itemId, aspxCommonObj: aspxCommonObj() });
                ItemMangement.config.ajaxCallMode = 48;
                ItemMangement.ajaxCall(ItemMangement.config);
            };
            SearchGrpProduct = function () {
                var itemSKU = $.trim($("#txtAssociatedItemSKU").val());
                var itemName = $.trim($("#txtAssociatedItemName").val());
                var itemTypeID = '';
                var attributeSetID = '';
                if (itemSKU.length < 1) {
                    itemSKU = null;
                }
                if (itemName.length < 1) {
                    itemName = null;
                }

                if ($("#ddlSelectAssociatedItemType option:selected").val() != 0) {
                    itemTypeID = $.trim($("#ddlSelectAssociatedItemType  option:selected").val());
                }
                else {
                    itemTypeID = null;
                }

                if ($("#ddlSelectAssociatedAttributeSetName option:selected").val() != 0) {
                    attributeSetID = $.trim($("#ddlSelectAssociatedAttributeSetName option:selected").val());
                }
                else {
                    attributeSetID = null;
                }

                var itemIDSearch = $("#ItemMgt_itemID").val();
                var categoryIdforSearch = $.trim($("#allCatags option:selected").val());
                GetGrpProduct(itemIDSearch, itemSKU, itemName, itemTypeID, attributeSetID, categoryIdforSearch);
            };

            return { Get: GetGrpProduct, BindEvents: BindEvents, Build: BuildGrpForm, Set: SetGrpProduct, Search: SearchGrpProduct, AttributeSetBind: BindAttributeSets, ItemTypeBind: BindItemTypes };

        } (),

        GroupPrice: function () {

            var grpPrice = [];
            var price = { ItemID: $("#ItemMgt_itemID").val(), GroupID: '', Price: 0 };

            buildForm = function () {
                var html = '';
                html += '<div class="sfFormWrapper sfGridwrapper">';
                html += '<table width="100%" cellspacing="0" cellpadding="0" id="tblGroupPrice" ><thead><tr class="cssClassHeading"><td>S.N</td><td class="cssClassUnitPrice">' + getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):</td><td>' + getLocale(AspxItemsManagement, "User Group:") + '</td><td></td></tr></thead>';
                html += '<tbody><tr class="sfEven"><td>1</td><td><input type="text" size="5"></td><td><div class="cssClassUsersInRoleCheckBox2"></div></td>';
                html += '<td><i class="icon-addnew" name="add"></i>&nbsp;&nbsp;<i class="icon-close" name="remove"style="display: none;"></i></span></td></tr>';
                html += '</tbody></table></div>';
                return html;
            };

            get = function (lists) {
                grpPrice = [];
                $("#tblGroupPrice").find("tbody tr").each(function (index, item) {
                    var price = $(this).find("input[type=text]").val();
                    if (price != '') {
                        var role = $(this).find("input[name^=roles]:checked").attr('value');
                        var obj = { GroupID: role, Price: price };
                        grpPrice.push(obj);
                    }

                });
                return grpPrice;

            };

            set = function () {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: aspxservicePath + "AspxCoreHandler.ashx/GetItemGroupPrices",
                    data: JSON2.stringify({ ItemID: $("#ItemMgt_itemID").val(), aspxCommonObj: aspxCommonObj() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        bindGroupPrices(msg.d);
                    },
                    error: function () {
                    }
                });
            };

            bindGroupPrices = function (pricelist) {
                grpPrice = pricelist;
                var frstRw = $("#tblGroupPrice").find("tbody tr:first");
                $.each(pricelist, function (index, item) {
                    if (index == 0) {
                        frstRw.find("input[type=text]").val(parseFloat(item.Price).toFixed(2));
                        frstRw.find("input[name=roles][value=" + item.GroupID + "]").prop("checked", "checked");
                    } else {
                        var elem = $("#tblGroupPrice").find("tbody tr:eq(" + (index - 1) + ")");
                        fillOrClear(true, elem, item);

                    }
                });

            };

            var roles;
            var counter = 0;
            fillOrClear = function (fill, trElem, item) {
                counter++;
                var $nxtTr = trElem.clone();

                var nxtAvailableRoles = $nxtTr.find("input[name^=roles]").not(":checked");
                $nxtTr.find("input[name^=roles]").attr('name', 'roles' + counter);
                var len = $("#tblGroupPrice").find("tbody tr").length;

                $nxtTr.find("input[name^=roles]:checked").removeAttr('checked');


                if (fill) {

                    $nxtTr.find("input[type=text]").val(parseFloat(item.Price).toFixed(2));
                    $nxtTr.find("input[name^=roles][value=" + item.GroupID + "]").prop("checked", "checked");

                } else {

                    $nxtTr.find("input[type=text]").val('');
                    $($nxtTr.find("input[name^=roles]").not(":checked").not(":disabled")[0]).prop("checked", "checked");

                }

                $nxtTr.insertAfter($("#tblGroupPrice").find("tbody tr:last"));
                $nxtTr.find("input[name^=roles]:checked").trigger('click');
                if (len >= 1) {
                    $("#tblGroupPrice").find("tbody tr").find("i[name=remove]").show();
                }
                $("#tblGroupPrice").find("tbody tr").each(function (index, item) {
                    $(this).find("td:first").html(index + 1);
                });

                $("#tblGroupPrice input[type=text]").DigitOnly("#tblGroupPrice input[type=text]", "");
            }
            formEvents = function () {

                $("body").on("click", "#tblGroupPrice i[name=add]", function () {

                    var $currentTr = $(this).parents("tr:eq(0)");
                    roles = $("#tblGroupPrice").data('items');
                    if (roles.length > $("#tblGroupPrice").find("tbody tr").length) {
                        fillOrClear(false, $currentTr, null);
                    }
                    return false;

                });
                $("body").on("click", "#tblGroupPrice input[name^=roles]", function () {

                    $("#tblGroupPrice input[name^=roles]").removeAttr('disabled')
                    var value = $(this).prop('value');

                    $("#tblGroupPrice input[name^=roles]:checked").each(function (index, item) {
                        var val = $(this).prop('value');
                        $("#tblGroupPrice input[name^=roles][value=" + val + "]").attr("disabled", "disabled");
                    });

                    $("#tblGroupPrice input[name^=roles][value=" + value + "]").attr("disabled", "disabled");
                    return false;
                });


                $("body").on("click", "#tblGroupPrice i[name=remove]", function () {

                    var $currentTr = $(this).parents("tr:eq(0)");
                    var len = $("#tblGroupPrice").find("tbody tr").length;
                    if (len > 1) {
                        $currentTr.remove();
                        if (len - 1 == 1) {

                            $("#tblGroupPrice").find("tbody tr:first").find("i[name=remove]").hide();
                        }
                    }
                    $("#tblGroupPrice").find("tbody tr").each(function (index, item) {
                        $(this).find("td:first").html(index + 1);
                    });

                    $("#tblGroupPrice input[name^=roles]").not(":checked").removeAttr('disabled')
                    $("#tblGroupPrice input[name^=roles]:checked").each(function (index, item) {
                        var val = $(this).prop('value');
                        $("#tblGroupPrice input[name^=roles][value=" + val + "]").attr("disabled", "disabled");
                    });
                    return false;
                });

            } ();

            add = function () {


            };
            deleteGroup = function () {

            };

            return { Build: buildForm, Set: set, Get: get };

        } (),
        ItemSetting: function () {
            var setting = {
                IsManageInventory: true,
                IsUsedStoreSetting: false,
                MinCartQuantity: 1,
                MaxCartQuantity: 1,
                LowStockQuantity: 1,
                OutOfStockQuantity: 1
            };
            get = function () {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: aspxservicePath + "AspxCoreHandler.ashx/GetItemSetting",
                    data: JSON2.stringify({ ItemID: $("#ItemMgt_itemID").val(), aspxCommonObj: aspxCommonObj() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        setting = msg.d;
                        bindSetting(setting);
                    },
                    error: function () {
                    }
                });
            };
            bindSetting = function (itemSetting) {
                $("#cbManageInventory").prop('checked', itemSetting.IsManageInventory);
                $("#cbUseStoreSetting").prop('checked', itemSetting.IsUsedStoreSetting);
                $("#txtMinCartQuantity").val(itemSetting.MinCartQuantity);
                $("#txtMaxCartQuantity").val(itemSetting.MaxCartQuantity);
                $("#txtLowStockQuantity").val(itemSetting.LowStockQuantity);
                $("#txtOutOfStockQuantity").val(itemSetting.OutOfStockQuantity);
                $("#txtStockQuantity").val($(".cssClassItemQuantity").val());
                if (itemSetting.IsManageInventory) {
                    $("#tblItemSetting").find("tr:eq(1)").fadeIn();
                    $("#dbUseStoreSetting").removeAttr("disabled");
                    if (itemSetting.IsUsedStoreSetting) {
                        $("#tblItemSetting").find("tr:gt(2)").fadeOut();
                    } else {
                        $("#tblItemSetting").find("tr:gt(2)").fadeIn();
                    }

                } else {
                    $("#tblItemSetting").find("tr:eq(1)").fadeOut();
                    $("#cbUseStoreSetting").attr("disabled", "disabled");

                }
            };
            bindchangeEvents = function () {
                $("body").on("change", "#cbManageInventory", function () {
                    if ($(this).is(":checked")) {

                        $("#tblItemSetting").find("tr:eq(1)").fadeIn();
                        $("#cbUseStoreSetting").removeAttr("disabled");
                        $("#cbUseStoreSetting").trigger("change");
                    } else {
                        $("#tblItemSetting").find("tr:eq(1)").fadeOut();

                        $("#cbUseStoreSetting").prop('checked', 'checked');
                        $("#cbUseStoreSetting").trigger("change");
                        $("#cbUseStoreSetting").attr("disabled", "disabled");


                    }
                });

                $("body").on("change", "#cbUseStoreSetting", function () {

                    if ($(this).is(":checked")) {

                        $("#tblItemSetting").find("tr:gt(2)").fadeOut();

                    } else {
                        $("#tblItemSetting").find("tr:gt(2)").fadeIn();
                        $("#txtMinCartQuantity").DigitOnly('#txtMinCartQuantity', '');
                        $("#txtMaxCartQuantity").DigitOnly('#txtMaxCartQuantity', '');
                        $("#txtLowStockQuantity").DigitOnly('#txtLowStockQuantity', '');
                        $("#txtOutOfStockQuantity").DigitOnly('#txtOutOfStockQuantity', '');
                        $("#txtStockQuantity").DigitOnly('#txtStockQuantity', '');
                    }
                });
                $("body").on("change", "#txtStockQuantity", function () {
                    $(".cssClassItemQuantity").val($("#txtStockQuantity").val());
                });

                $("body").on("change", ".cssClassItemQuantity", function () {
                    $("#txtStockQuantity").val($(".cssClassItemQuantity").val());
                });
            } ();

            getCustomSetting = function () {
                var setting2 = {};
                setting2.IsManageInventory = $("#cbManageInventory").is(":checked");
                setting2.IsUsedStoreSetting = $("#cbUseStoreSetting").is(":checked");
                setting2.MinCartQuantity = $("#txtMinCartQuantity").val() == '' ? 1 : $("#txtMinCartQuantity").val();
                setting2.MaxCartQuantity = $("#txtMaxCartQuantity").val() == '' ? 1 : $("#txtMaxCartQuantity").val();
                setting2.LowStockQuantity = $("#txtLowStockQuantity").val() == '' ? 1 : $("#txtLowStockQuantity").val();
                setting2.OutOfStockQuantity = $("#txtOutOfStockQuantity").val() == '' ? 1 : $("#txtOutOfStockQuantity").val();
                return setting2;
            };

            buildForm = function (itemType) {
                var html = "";
                html += '<table id="tblItemSetting" width="100%" border="0" cellspacing="0" cellpadding="0">';
                html += '<tr><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Manage Inventory") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input id="cbManageInventory" type="checkbox" checked="checked" /></div></td></tr>';

                html += '<tr  ><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Available Stock") + '</label></td>';

                if (itemType == 2) {
                    html += '<td class="cssClassTableRightCol"><div><input id="txtStockQuantity" type="text" value="1" disabled="disabled" /></div></td></tr>';
                }
                else {
                    html += '<td class="cssClassTableRightCol"><div><input id="txtStockQuantity" type="text" value="1"  /></div></td></tr>';
                }
                html += '<tr><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Use StoreSetting") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input id="cbUseStoreSetting" type="checkbox" checked="checked"  /></div></td></tr>';

                html += '<tr style="display:none;"><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "MinCart Quantity") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input id="txtMinCartQuantity" value="1" type="text" /></div></td></tr>';

                html += '<tr style="display:none;"><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "MaxCart Quantity") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input id="txtMaxCartQuantity" value="1" type="text" /></div></td></tr>';

                html += '<tr style="display:none;"><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "LowStock Quantity") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input  id="txtLowStockQuantity" value="1" type="text"  /></div></td></tr>';

                html += '<tr style="display:none;"><td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "OutOfStock Quantity") + '</label></td>';
                html += '<td class="cssClassTableRightCol"><div><input id="txtOutOfStockQuantity" value="1" type="text"  /></div></td></tr>';

                html += '</table>';
                switch (itemType) {

                    case 1:
                        break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                    case 6: break;

                }

                return html;

            };


            return { Get: get, Build: buildForm, GetSetting: getCustomSetting };
        } (),
        CreateAccordion: function (attGroup, attributeSetId, itemTypeId, showDeleteBtn) {
            if (FormCount) {
                FormCount = new Array();
            }
            var FormID = "form_" + (FormCount.length * 10 + Math.floor(Math.random() * 10));
            FormCount[FormCount.length] = FormID;
            var dynHTML = '';
            var tabs = '';
            var generalContain = '';
            quickNavigation += "<div class='st_tabs_container'><div class='st_slide_container'><ul class='st_tabs'>";
            tabs += "<div class=\"st_view_containerWrap\"><div class=\"st_view_container\"><div class=\"st_view\">";
            for (var i = 0; i < attGroup.length; i++) {
                if (attGroup[i].tabName == 'General Information') {
                    generalContain = '<div id=' + attGroup[i].key + '>';
                    generalContain += '<button type="button" class="cssClassAddAttrib sfBtn " onclick="DynamicAttribute.AddAttr(' + attGroup[i].key + ')" ><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add New Attribute") + '</span></button>';
                    generalContain += '<div><table width="100%" border="0" cellpadding="0" cellspacing="0">' + attGroup[i].html + '';

                }
                else {
                    quickNavigation += "<li><a class='st_tab' rel='v_tab" + attGroup[i].value + "' href=" + '#' + attGroup[i].key + ">" + attGroup[i].value + "</a></li>";
                    tabs += '<div id=' + attGroup[i].key + ' class="st_tab_view"><h4><a href="#" name="' + attGroup[i].key + '">' + attGroup[i].value + '</a></h4>';
                    tabs += '<br/><button type="button" class="cssClassAddAttrib sfBtn" onclick="DynamicAttribute.AddAttr(' + attGroup[i].key + ')" ><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add New Attribute") + '</span></button>';
                    tabs += '<div><table width="100%" border="0" cellpadding="0" cellspacing="0">' + attGroup[i].html + '</table></div></div>';
                }
            }

            if (itemTypeId == 1 || itemTypeId == 6) {
                quickNavigation += "<li><a class='st_tab' rel='v_tab2' href='#dv_Content_21'>" + getLocale(AspxItemsManagement, "Item Setting") + "</a></li>";
                tabs += '<div class="st_tab_view" id="dv_Content_21"><h4><a href="#">' + getLocale(AspxItemsManagement, "Item Setting") + '</a></h4><div >' + ItemMangement.ItemSetting.Build(itemTypeId) + '</div></div>';
            }

            if (itemTypeId != 5) {
                quickNavigation += "<li><a class='st_tab' rel='v_tab3' href='#dv_Content_3'>" + getLocale(AspxItemsManagement, "Item Tax Class") + "</a></li>";

                tabs += '<div class="st_tab_view" id="dv_Content_3"><h4><a href="#">' + getLocale(AspxItemsManagement, "Item Tax Class") + '</a></h4><div id="divTax"><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item Tax Class Name:") + ' </span><select id="ddlTax" class="sfListmenu" /></div></div>';
            }

            if (itemTypeId == 1 || itemTypeId == 2) {
                quickNavigation += "<li><a class='st_tab' rel='v_tab4' href='#dv_Content_4'>" + getLocale(AspxItemsManagement, "Brand") + "</a></li>";

                tabs += '<div class="st_tab_view" id="dv_Content_4"><h4><a href="#">' + getLocale(AspxItemsManagement, "Brand") + '</a></h4><div id="tblBrandTree" width="100%" border="0" cellpadding="0" cellspacing="0"><select id="lstBrands" class="cssClassMultiSelect"></select><span id="spanNoBrand" class="cssClassLabel"></span></div></div>';
            }
            if (itemTypeId != 3) {
                generalContain += '<tr id="dv_Content_8"><td><label>' + getLocale(AspxItemsManagement, "Categories") + '</label></td><td><div id="tblCategoryTree" width="100%" border="0" cellpadding="0" cellspacing="0"><select id="lstCategories" class="cssClassMultiSelect"></select><span id="spanNoCat" class="cssClassLabel"></span></div></td></tr>';
            }
            generalContain += '<tr><td style="vertical-align:top;"><label>' + getLocale(AspxItemsManagement, "Images") + '</label></td><td><div id="divUploader"><div id="fileUpload" ></div><div class="progress ui-helper-clearfix"><div class="progressBar" id="progressBar"></div><div class="percentage"></div></div></div><div id="divImageCollapsable" class="sfCollapsewrapper" style="display:none;"><div class="sfAccordian"><div class="sfAccordianholder" id="dv_Content_6"><div class="sfAccordianheader sfFloatLeft"><h4>' + getLocale(AspxItemsManagement, "View Images") + '</h4></div></div><div id="multipleUpload" class="sfCollapsecontent sfGridwrapper" style="clear:both;">';
            generalContain += '<div id="divTableWrapper" class="sfGridWrapperContent" ><table class="classTableWrapper" width="100%" border="0" cellpadding="o" cellspacing="0"><thead></thead><tbody></tbody></table></div></div></div></div></td></tr>';
            generalContain += '</table></div></div>';
            if (itemTypeId == 2) {
                generalContain += '<div class="sfCollapsewrapper"><div class="sfAccordian"><div class="sfAccordianholder" id="dv_Content_2"><div class="sfAccordianheader"><h2>' + getLocale(AspxItemsManagement, "Download Information") + '</h2></div></div>';
                generalContain += '<div id="divDownloadInfo" class="sfCollapsecontent">';
                generalContain += '<table class="sfFormwrapper" width="100%" border="0" cellpadding="o" cellspacing="0">';
                generalContain += '<tbody>';
                generalContain += '<tr><td><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Title:") + '</span></td><td class="cssClassTableRightCol"><div class="field required"><input type="text" id="txtDownloadTitle" class="sfInputbox" maxlength="256"/><span class="iferror"></span></div></td></tr>';
                generalContain += '<tr><td><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Maximum Download:") + '</span></td><td class="cssClassTableRightCol"><div class="field required"><input type="text" id="txtMaxDownload" class="sfInputbox" maxlength="3"/><span class="iferror">' + getLocale(AspxItemsManagement, "Integer Number") + '</span></div></td></tr>';

                generalContain += '<tr><td><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Sample File:") + '</span></td><td class="cssClassTableRightCol"><input id="fileSample" type="file" class="cssClassBrowse notTest" /><span id="spanSample" class="cssClassLabel"></span></td></tr>';
                generalContain += '<tr><td><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Actual File:") + ' </span></td><td class="cssClassTableRightCol"><input id="fileActual" type="file" class="cssClassBrowse notTest" /><span id="spanActual" class="cssClassLabel"></span></td></tr>';

                generalContain += '</tbody>';
                generalContain += '</table></div></div></div>';
            }
            if (itemTypeId == 5) {
                generalContain += '<div class="sfCollapsewrapper"><div class="sfAccordian"><div class="sfAccordianholder" id="dv_Content_11"><div class="sfAccordianheader"><h2>' + getLocale(AspxItemsManagement, "Associated Products") + '</h2></div></div><div class="sfGridwrapper sfCollapsecontent">' + ItemMangement.GroupItems.Build(); +'</div></div></div>';
            };
            if (itemTypeId == 6) {
                generalContain += '<div class="sfCollapsewrapper"><div class="sfAccordian"><div class="sfAccordianholder" id="dv_Content_22"><div class="sfAccordianheader"><h2><a href="#">' + getLocale(AspxItemsManagement, "KIT") + '</a></h2></div></div><div class="sfCollapsecontent">' + ItemMangement.ItemTypes.Kit.UI.First() + '</div></div></div>';
                ItemMangement.ItemTypes.Kit.Init();
            }
            if (itemTypeId == 3) {
                generalContain += '<div class="sfCollapsewrapper"><div class="sfAccordian"><div class="sfAccordianholder" id="dv_Content_5"><div class="sfAccordianheader"><h2>' + getLocale(AspxItemsManagement, "Gift Card Category") + '</h2></div></div>';
                generalContain += '<div id="divGCThemes" class="sfFormwrapper sfCollapsecontent">';
                generalContain += '<table  width="100%" border="0" cellpadding="o" cellspacing="0">';
                generalContain += '<tbody>';
                generalContain += '<tr><td><span class="cssClassLabel">' + getLocale(AspxItemsManagement, "Gift Card Category:") + '</span></td><td class="cssClassTableRightCol"><div ><select  multiple="multiple" id="ddlGCCategory"  style="width:200px;height:100px"><option value="0">' + getLocale(AspxItemsManagement, "ALL") + '</option></select></div><div><span>' + getLocale(AspxItemsManagement, "(* select category of gift card)") + '</span></div></td></tr>';
                generalContain += '</tbody>';
                generalContain += '</table></div></div></div>';
            }
            generalContain += '</div>';

            quickNavigation += "<li><a class='st_tab' rel='v_tab7' href='#dv_Content_7'>" + getLocale(AspxItemsManagement, "Videos") + "</a></li>";



            tabs += '<div class="st_tab_view" id="dv_Content_7"><h4><a href="#">' + getLocale(AspxItemsManagement, "Videos") + '</a></h4><div><table id="tblVideosTree" width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td><label id="lblVideoID" class="cssClassLabel">' + getLocale(AspxItemsManagement, "YouTube Video Ids :") + '</label></td><td class="cssClassTableRightCol"><input type="text" class="sfInputbox  cssClassItemName" title=' + getLocale(AspxItemsManagement, "Enter Youtube Video Id, If Multiple Separeted With Comma") + ' id="txtVideoID"/></td></table></div></div>';
            if (itemTypeId != 2 && itemTypeId != 3 && itemTypeId != 4 && itemTypeId != 5) {
                if (p.EnableCostVariantOption == true) {
                    if (!ItemMangement.CheckIsItemInGroupItem(itemId)) {
                        quickNavigation += "<li><a class='st_tab' rel='v_tab9' href='#dv_Content_9'>" + getLocale(AspxItemsManagement, "Cost Variant Combination") + "</a></li>";
                        tabs += '<div class="st_tab_view" id="dv_Content_9"><h4>' + getLocale(AspxItemsManagement, "Cost Variant Combination") + '</h4><div><div class="sfGridwrapper" id="dvCvForm">';
                        tabs += '<table><thead><tr class="cssClassHeading"><td> Pos. </td> <td>' + getLocale(AspxItemsManagement, "Combination") + '</td><td>' + getLocale(AspxItemsManagement, "Status") + '</td><td></td><td></td><td></td></tr></thead><tbody><tr><td><input size="3" class="cssClassVariantValue" value="0" type="hidden"><input size="3" class="cssClassDisplayOrder" type="text" value="1" disabled="disabled"></td><td class="cssClassTableCostVariant"><table><thead><tr><td><b>' + getLocale(AspxItemsManagement, "Cost Variant Name") + '</b></td><td><b>' + getLocale(AspxItemsManagement, "Cost Variant Values") + '</b></td><td></td> </tr></thead> <tr> <td class="tdCostVariant"> <select class="ddlCostVariantsCollection"></select><a href="#dvCvForm" class="cssClassCvAddMore sfBtn icon-addnew" style="margin-top:6px;" onclick="AddMoreVariantOptions(this); return false;">' + getLocale(AspxItemsManagement, "") + '</a> </td> <td class="tdCostVariantValues"> <select class="ddlCostVariantValues"><option>' + getLocale(AspxItemsManagement, "No values") + '</option></select></td> <td> <a href="#" class="cssClassCvClose" onclick="CloseCombinationRow(this); return false;" style="display:none;"><i class="imgDelete icon-delete" title=' + getLocale(AspxItemsManagement, "delete") + ' alt=' + getLocale(AspxItemsManagement, "delete") + '/></i></a> </td> </tr> </table> <div class="cssclassItemCostVariant"> <div class="CostVariantItemQuantity"> <label>' + getLocale(AspxItemsManagement, "Quantity") + ':</label> <input type="text" class="cssclassCostVariantItemQuantity" value="1" /> </div> <div class="PriceModifier"><label>' + getLocale(AspxItemsManagement, "Cost ModifierType") + ':</label> <input size="5" class="cssClassPriceModifier" type="text" value="0.00"> <select class="cssClassPriceModifierType"> <option value="0">%</option> <option value="1">' + curSymbol + '</option> </select></div> <div class="WeightModifier"> <label>' + getLocale(AspxItemsManagement, "Weight ModifierType") + ':</label> <input size="5" class="cssClassWeightModifier" type="text" value="0.00"><select class="cssClassWeightModifierType"><option value="0">%</option> <option value="1">' + getLocale(AspxItemsManagement, "lbs") + '</option> </select> </div> </div> </td> <td> <select class="cssClassIsActive"> <option value="1">' + getLocale(AspxItemsManagement, "Active") + '</option> <option value="0">' + getLocale(AspxItemsManagement, "Inactive") + '</option> </select> </td> <td> <span class="nowrap"> <button rel="popuprel2" class="classAddImages sfBtn" value="0" type="button"><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add Images") + '</span></button> </span> </td> <td> <span class="addButton"> <button type="button" value="0" class="cssclassAddCVariants sfBtn" onclick="AddCombinationListRow(this); return false;"> <span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add More Cost Variants") + ' </span></button> </span> </td> <td> <a href="#" class="cssClassCvCloseMain" onclick ="CloseMainCombinationList(this); return false;" ><i class="imgDelete icon-delete" title=' + getLocale(AspxItemsManagement, "delete") + ' alt=' + getLocale(AspxItemsManagement, "delete") + ' /></i></a> </td> </tr> </tbody> </table>';
                        tabs += '</div><div class="sfButtonwrapper"><p><button id="btnBackCostVariantCombination" type="button" class="sfBtn"><span class="icon-arrow-slim-w">' + getLocale(AspxItemsManagement, "Back") + '</span></button></p> <p><button id="btnCancelCostVariantCombination" type="button" class="sfBtn"><span class="icon-refresh">' + getLocale(AspxItemsManagement, "Reset") + '</span></button></p><p><button id="btnSaveCostVariantCombination" type="button" class="sfBtn"><span class="icon-save">' + getLocale(AspxItemsManagement, "Save Cost Variants Combination") + 'Option</span></button></p><p><button id="btnDeleteCostVariantCombination" type="button" class="sfBtn"><span class="icon-delete">' + getLocale(AspxItemsManagement, "Delete Cost Variants Combination Option") + '</span></button></p></div>';
                        tabs += ' <div id="dvCostVarAdd" style="display:none;" class="sfButtonwrapper"><p><button id="btnAddCostVariantCombination" type="button" class="sfBtn"><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add Cost Variants Combination Option") + '</span></button></p></div></div></div>';
                    }
                }

            }

            if (p.EnableRelatedItem == true) {
                quickNavigation += "<li><a class='st_tab getrelItem' rel='v_tab12' href='#dv_Content_12'>" + getLocale(AspxItemsManagement, "Related Products") + "</a></li>";
            }
            if (p.EnableUpSellItem == true) {
                quickNavigation += "<li><a class='st_tab getUpItem' rel='v_tab13' href='#dv_Content_13'>" + getLocale(AspxItemsManagement, "Up-sells") + "</a></li>";
            }
            if (p.EnableCrossSellItem == true) {
                quickNavigation += "<li><a class='st_tab getCrossItem' rel='v_tab14' href='#dv_Content_14'>" + getLocale(AspxItemsManagement, "Cross-sells") + "</a></li>";
            }
            if (p.EnableRelatedItem == true) {
                tabs += '<div class="st_tab_view" id="dv_Content_12"><h4><a href="#">' + getLocale(AspxItemsManagement, "Related Products") + '</a></h4><div class="sfGridwrapper">'

                    + '<div class="sfGridwrapper" ><div class="sfGridWrapperContent"><div class="sfFormwrapper sfTableOPtion">'
                    + '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>'
                    + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item SKU") + ' :</label><input type="text" id="txtItemSKU" class="sfTextBoxSmall" /></td>'
                    + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item Name") + ' :</label><input type="text" id="txtItemName" class="sfTextBoxSmall" /></td>'
                     + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Item Type") + ' :</label><select id="ddlSelectItemType" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                     + '<td><label class="cssClassLabel sfLocale">' + getLocale(AspxItemsManagement, "Attribute Set Name") + ' :</label><select id="ddlSelectAttributeSetName" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                    + '<td><br/><button type="button" id="btnSearchRelatedItems" class="sfBtn"><span class="icon-search">' + getLocale(AspxItemsManagement, "Search") + '</span></button></td>'
                    + '</tr></table>'
                    + '</div></div></div>'

                + '<table id="gdvRelatedItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div></div>';
            }
            if (p.EnableUpSellItem == true) {
                tabs += '<div class="st_tab_view" id="dv_Content_13"><h4><a href="#">' + getLocale(AspxItemsManagement, "Up-sells") + '</a></h4><div class="sfGridwrapper">'

                 + '<div class="sfGridwrapper"><div class="sfGridWrapperContent"><div class="sfFormwrapper sfTableOption">'
                    + '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>'
                    + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item SKU") + ' :</label><input type="text" id="txtItemSKUSell" class="sfTextBoxSmall" /></td>'
                    + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item Name") + ' :</label><input type="text" id="txtItemNameSell" class="sfTextBoxSmall" /></td>'
                     + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item Type") + ' :</label><select id="ddlSelectItemTypeSell" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                     + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Attribute Set Name") + ' :</label><select id="ddlSelectAttributeSetNameSell" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                    + '<td><br/><button type="button" id="btnSearchUpSellItems" class="sfBtn"><span class="icon-search">' + getLocale(AspxItemsManagement, "Search") + '</span></button></td>'
                    + '</tr></table>'
                    + '</div></div></div>'

               + '<table id="gdvUpSellItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div></div>';
            }
            if (p.EnableCrossSellItem == true) {
                tabs += '<div class="st_tab_view" id="dv_Content_14"><h4><a href="#">' + getLocale(AspxItemsManagement, "Cross-sells") + '</a></h4><div class="sfGridwrapper">'

                  + '<div><div class="sfGridWrapperContent"><div class="sfFormwrapper sfTableOption">'
                    + '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>'
                    + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item SKU") + ' :</label><input type="text" id="txtItemSKUcs" class="sfTextBoxSmall" /></td>'
                    + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item Name") + ' :</label><input type="text" id="txtItemNamecs" class="sfTextBoxSmall" /></td>'
                     + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Item Type") + ' :</label><select id="ddlSelectItemTypecs" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                     + '<td><label class="cssClassLabel">' + getLocale(AspxItemsManagement, "Attribute Set Name") + ' :</label><select id="ddlSelectAttributeSetNamecs" class="sfListmenu"><option value="0">' + getLocale(AspxItemsManagement, "--All--") + '</option></select>'
                    + '<td><br/><button type="button" id="btnSearchCrossSellItems" class="sfBtn"><span class="icon-search">' + getLocale(AspxItemsManagement, "Search") + '</span></button></td>'
                    + '</tr></table>'
                    + '</div></div></div>'

                + '<table id="gdvCrossSellItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div></div>';
            }
            tabs += "</div></div></div>";

            quickNavigation += "</ul></div></div>";
            dynHTML += tabs + quickNavigation;
            var frmIDQuoted = "'" + FormID + "'";
            var buttons = '<div class="sfButtonwrapper"><p><button type="button" id="btnReturn" class="sfBtn"><span class="icon-arrow-slim-w">' + getLocale(AspxItemsManagement, "Back") + '</span></button></p>';
            if (!showDeleteBtn) {
                buttons += '<p><button type="button" id="btnResetForm" class="sfBtn" ><span class="icon-refresh">' + getLocale(AspxItemsManagement, "Reset") + '</span></button></p>';
            }
            else {
                buttons += '<p><button type="button" id="btnDelete" class="delbutton sfBtn" ><span class="icon-delete">' + getLocale(AspxItemsManagement, "Delete Item") + '</span></button></p>';
            }
            buttons += '<p><button type="button" id="saveForm" class="sfBtn" onclick="ItemMangement.SubmitForm(' + frmIDQuoted + ',' + attributeSetId + ',' + itemTypeId + ')" ><span class="icon-save">' + getLocale(AspxItemsManagement, "Save Item") + '</span></button></p>';
            buttons += '<div class="clear"></div></div>';
            $("#divGeneralContent").html(generalContain + buttons);

            $("#dynItemForm").html('<div><div id="st_vertical" class="st_vertical">' + dynHTML + '</div><div class="clear"></div></div>' + buttons + '');
            $(".cssItemContentWrapper").prop('id', FormID);
            quickNavigation = "";
            ItemMangement.EnableAccordion();
            $('div.sfCollapsecontent').hide();
            ItemMangement.EnableFormValidation(FormID);
            ItemMangement.EnableDatePickers();
            ItemMangement.EnableFileUploaders();
            ItemMangement.EnableHTMLEditors();
            if ($("#rdbGeneralType").prop("checked")) {
                $("#divGeneralContent").show();
            }
            if ($("#rdbAdvancedType").prop("checked")) {
                $("#divAdvancedContent").show();
            }

            var lastTr = $("#dvCvForm table>tbody:first tr:last");
            if (lastTr.html() == '') {
                $("#dvCvForm table>tbody:first tr:last").remove();
            }
            $('#btnResetForm').bind("click", function () {
                $('#ddlAttributeSet').val('2');
                $('#ddlItemType').val('1');
                ItemMangement.ClearAttributeForm();
                ItemMangement.OnInit();
                ItemMangement.ClearVariantForm();
                ItemMangement.BindCostVariantsInputType();
                ItemMangement.LoadItemStaticImage();
                var lastTr = $("#dvCvForm table>tbody:first tr:last");
                if (lastTr.html() == '') {
                    $("#dvCvForm table>tbody:first tr:last").remove();
                }
                return false;
            });

            $(".getrelItem").click(function(){
			  if (p.EnableRelatedItem == true) {
			      ItemMangement.BindRelatedItemsGrid(itemId, null, null, null, null, aspxCommonObj());
                }
            });

            $(".getUpItem").click(function () {
                if (p.EnableUpSellItem == true) {
                    ItemMangement.BindUpSellItemsGrid(itemId, null, null, null, null, aspxCommonObj());
                }              
            });

            $(".getCrossItem").click(function () {
                if (p.EnableCrossSellItem == true) {
                    ItemMangement.BindCrossSellItemsGrid(itemId, null, null, null, null, aspxCommonObj());
                }
            });
			
            $('div.sfAccordianholder').on("click", function () {
                if (!$(this).hasClass("Active")) {

                    $(this).addClass("Active");
                    $(this).parent().next("div").slideDown("fast");
                    $(this).parent().find('.sfCollapsecontent').show();
                }
                else {
                    $(this).removeClass("Active");
                    $(this).parent().find('.sfCollapsecontent').hide();
                    $(this).parent().next("div").slideUp("fast");
                }
                return false;
            });
            if (itemTypeId == 3) {
                ItemMangement.GiftCard.Init(aspxCommonObj());
            }
        },

        EnableAccordion: function () {
            $('div#st_vertical').slideTabs({ tabsScroll: false, contentAnim: 'slideH', contentAnimTime: 600, contentEasing: 'easeInOutExpo', orientation: 'vertical', tabsAnimTime: 300 });

        },

        EnableFormValidation: function (frmID) {
            mustCheck = true;
            $("#" + frmID + " ." + classprefix + "Cancel").click(function (event) {
                mustCheck = false;
                return false;
            });
            var fe = $("#" + frmID + " input");
            for (var j = 0; j < fe.length; j++) {
                if ((fe[j]).title.indexOf("**") == 0) {
                    if ((fe[j]).value == "" || (fe[j]).value == titleHint) {
                        var titleHint = (fe[j]).title.substring(2);
                        (fe[j]).value = titleHint;
                    }
                } else if (((fe[j]).type == "text" || (fe[j]).type == "password" || (fe[j]).type == "textarea") && (fe[j]).title.indexOf("*") == 0) {
                    addHint((fe[j]));
                    $(fe[j]).blur(function (event) { addHint(this); });
                    $(fe[j]).focus(function (event) { removeHint(this); });
                }
            }
        },

        EnableDatePickers: function () {
            for (var i = 0; i < DatePickerIDs.length; i++) {
                $("#" + DatePickerIDs[i]).datepicker({ dateFormat: 'yy/mm/dd' });
            }
        },

        HTMLEditor: function (editorID, editorObject) {
            this.ID = editorID;
            this.Editor = editorObject;
        },

        EnableHTMLEditors: function () {
            for (var i = 0; i < htmlEditorIDs.length; i++) {
                config = { skin: "office2003" };
                var html = getLocale(AspxItemsManagement, "Initially Text if necessary");

                var editorID = htmlEditorIDs[i];
                var instance = CKEDITOR.instances[editorID];
                if (instance) {
                    CKEDITOR.remove(instance);
                }
                var editor = CKEDITOR.replace(editorID, config, html);

                var obj = new ItemMangement.HTMLEditor(editorID, editor);
                editorList[editorList.length] = obj;
            }
        },

        ResetHTMLEditors: function () {
            htmlEditorIDs.length = 0;
            editorList.length = 0;
        },

        EnableFileUploaders: function () {
            for (var i = 0; i < FileUploaderIDs.length; i++) {
                ItemMangement.CreateFileUploader(String(FileUploaderIDs[i]));
            }
        },

        GetValidationTypeClasses: function (attValType, isUnique, isRequired) {
            var returnClass = ''
            if (isRequired == true) {
                returnClass = "required";
            }
            return returnClass;
        },

        GetValidationTypeErrorMessage: function (attValType) {
            var retString = ''
            switch (attValType) {
                case 1: retString = getLocale(AspxItemsManagement, "Alphabets Only");
                    break;
                case 2: retString = getLocale(AspxItemsManagement, "AlphaNumeric");
                    break;
                case 3: retString = getLocale(AspxItemsManagement, "Decimal Number");
                    break;
                case 4: retString = getLocale(AspxItemsManagement, "Email Address");
                    break;
                case 5: retString = getLocale(AspxItemsManagement, "Integer Number");
                    break;
                case 6: retString = getLocale(AspxItemsManagement, "Price error");
                    break;
                case 7: retString = getLocale(AspxItemsManagement, "Web URL");
                    break;
            }
            return retString;
        },
        CheckUniqueness: function (sku, itemId) {
            var errors = '';
            sku = $.trim(sku);
            if (!sku) {
                errors += getLocale(AspxItemsManagement, "Please enter Sku code.");
                $('.cssClassRight').hide();
                $('.cssClassError').show();
                $('.cssClassError').html(getLocale(AspxItemsManagement, "Please enter Sku code.") + '<br/>');
            }
            else if (!ItemMangement.IsUnique(sku, itemId)) {
                errors += getLocale(AspxItemsManagement, "Please enter unique item Sku code") + '! "' + sku.trim() + getLocale(AspxItemsManagement, "already exists") + '.<br/>';
                $('.cssClassRight').hide();
                $('.cssClassError').show();
                $('.cssClassError').html(getLocale(AspxItemsManagement, "Please enter unique item Sku code") + '! "' + sku.trim() + getLocale(AspxItemsManagement, "already exists") + '.<br/>');
                $(".cssClassError").parent('div').addClass("diverror");
                $('.cssClassError').prevAll("input:first").addClass("error");
            }

            if (errors) {
                return false;
            }
            else {
                $('.cssClassRight').show();
                $('.cssClassError').html('');
                $('.cssClassError').hide();
                $(".cssClassError").parent('div').removeClass("diverror");
                $('.cssClassError').prevAll("input:first").removeClass("error");
                return true;
            }
        },
        CheckIsItemInGroupItem: function (itemID) {
            this.config.url = this.config.baseURL + "CheckIsItemInGroupItem";
            this.config.data = JSON2.stringify({ ItemID: itemID, aspxCommonObj: aspxCommonObj() }),
            this.config.ajaxCallMode = 51;
            this.ajaxCall(this.config);
            return ItemMangement.vars.IsItemInGroupItem;
        },
        IsUnique: function (sku, itemId) {
            this.config.url = this.config.baseURL + "CheckUniqueItemSKUCode";
            this.config.data = JSON2.stringify({ SKU: sku, itemId: itemId, aspxCommonObj: aspxCommonObj() }),
                this.config.ajaxCallMode = 25;
            this.ajaxCall(this.config);
            return ItemMangement.vars.isUnique;
        },

        CheckUnique: function (id) {
            var val = $('#' + id).val();
            if (val) {
                var arrID = id.split('_');
                this.config.url = this.config.baseURL + "IsUnique";
                this.config.data = JSON2.stringify({ storeID: aspxCommonObj().StoreID, portalID: aspxCommonObj().PortalID, ItemID: id, AttributeID: arrID[0], AttributeType: arrID[1], AttributeValue: val });
                this.config.ajaxCallMode = 26;
                this.ajaxCall(this.config);
                return ItemMangement.vars.isUnique;
            }
            else {
                return false;
            }
        },
        createValidation: function (id, attType, attValType, isUnique, isRequired) {
            var retString = '';
            var validationClass = '';

            switch (attValType) {
                case 1: validationClass += 'verifyAlphabetsOnly';
                    break;
                case 2: validationClass += 'verifyAlphaNumeric';
                    break;
                case 3: validationClass += 'verifyDecimal';
                    break;
                case 4: validationClass += 'verifyEmail';
                    break;
                case 5: validationClass += 'verifyInteger';
                    break;
                case 6: validationClass += 'verifyPrice';
                    break;
                case 7: validationClass += 'verifyUrl';
                    break;
            }
            retString = validationClass;
            return retString;
        },

        BackToItemGrid: function () {
            listImages = [];
            ItemMangement.ResetHTMLEditors();
            var n = $("#btnDelete").length;
            if (n != 0) {
                $("#gdvItems_grid").show();
                $("#gdvItems_form").hide();
                $("#gdvItems_accordin").hide();
                $("#divItemMgrTitle").hide();
            }
            else {
                $("#gdvItems_form").hide();
                $("#gdvItems_grid").show();
                $("#gdvItems_accordin").hide();
                $("#divItemMgrTitle").hide();
            }
        },
        //Send the list of images to the ImageResizer
        ResizeImageDynamically: function (Imagelist, ImageType) {
            ItemMangement.config.method = "MultipleImageResizer";
            ItemMangement.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + ItemMangement.config.method;
            ItemMangement.config.data = JSON2.stringify({ imgCollection: Imagelist, types: ImageType, imageCatType: "Item", aspxCommonObj: aspxCommonObj() });
            ItemMangement.config.ajaxCallMode = ItemMangement.ResizeImageSuccess;
            ItemMangement.ajaxCall(ItemMangement.config);

        },
        ResizeImageSuccess: function () {
        },
        ValidateExtraField: function (cssClassFirst, cssClassSecond, validateType, ErrorMessage) {
            var valFirst = $('.' + cssClassFirst + '').val();
            var valSecond = $('.' + cssClassSecond + '').val();
            var prevFirstDiv = $('.' + cssClassFirst + '').parent('div');
            var prevSecondDiv = $('.' + cssClassSecond + '').parent('div');
            if (prevFirstDiv.length > 0 && prevSecondDiv.length > 0) {
                switch (validateType) {
                    case "price":
                        if (valFirst != "") {
                            valFirst = parseFloat(valFirst);
                        }
                        if (valSecond != "") {
                            valSecond = parseFloat(valSecond);
                        }
                        break;
                    case "date":
                        if (valFirst != "") {
                            valFirst = Date.parse(valFirst);
                        }
                        if (valSecond != "") {
                            valSecond = Date.parse(valSecond);
                        }
                        break;
                    default:
                        valFirst = eval(valFirst);
                        valSecond = eval(valSecond);
                }
                if (valFirst != "" && valSecond != "") {
                    if (valSecond >= valFirst) {
                        $('.' + cssClassFirst + '').removeClass('error');
                        $('.' + cssClassSecond + '').removeClass('error');
                        prevFirstDiv.removeClass('diverror');
                        prevSecondDiv.removeClass('diverror');
                        return true;
                    }
                    else {
                        $('.' + cssClassSecond + '').parent('div').find('span').show().html(ErrorMessage);
                        $('.' + cssClassFirst + '').addClass('error');
                        prevFirstDiv.addClass('diverror');
                        $('.' + cssClassSecond + '').addClass('error');
                        prevSecondDiv.addClass('diverror');
                        return false;
                    }
                }
                else if (valFirst != "" && valSecond == "") {
                    $('.' + cssClassFirst + '').removeClass('error');
                    $('.' + cssClassSecond + '').removeClass('error');
                    prevFirstDiv.removeClass('diverror');
                    prevSecondDiv.removeClass('diverror');
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                prevFirstDiv.removeClass('diverror');
                prevSecondDiv.removeClass('diverror');
                return true;
            }
        },
        CostVariantsImageUploader: function (maxFileSize) {
            var upload = new AjaxUpload($('#imageUploader'), {
                action: aspxItemModulePath + "ItemCostVariantsFileUpload.aspx",
                name: 'myfile[]',
                multiple: true,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize,
                                'StoreID': AspxCommerce.utils.GetStoreID(),
                                'PortalID': AspxCommerce.utils.GetPortalID(),
                                'CultureName': AspxCommerce.utils.GetCultureName()
                            });
                        } else {
                            csscody.alert('<h1>' + getLocale(AspxItemsManagement, "Alert Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Not a valid image!") + '</p>');
                            return false;
                        }
                    }
                    else {
                        csscody.alert('<h1>' + getLocale(AspxItemsManagement, "Alert Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Not a valid image!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null && res.Status > 0) {
                        ItemMangement.AddNewVariantsImages(res);
                        return false;
                    }
                    else {
                        csscody.error('<h1>' + getLocale(AspxItemsManagement, "Alert Message") + '</h1><p>' + res.Message + '</p>');
                        return false;
                    }
                }
            });
        },

        AddNewVariantsImages: function (response) {
            $("#VariantsImagesTable").show();
            $('#btnSaveImages').show();
            $('#btnImageBack').show();
            var imageList = '<tr>';
            imageList += '<td><img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="100px" width="125px"/></td>';
            imageList += '<td><i class="imgDeleteCostVariant icon-delete" id="btnDeleteCV" onclick="ItemMangement.DeleteCVImage(this)" /></i></td>';
            imageList += '</tr>';
            $("#VariantsImagesTable>tbody").append(imageList);
            $("#VariantsImagesTable>tbody tr:even").addClass("sfEven");
            $("#VariantsImagesTable>tbody tr:odd").addClass("sfOdd");
        },
        DeleteCVImage: function (onjImg) {
            var $target = $(onjImg).closest('tr');
            var old = $target.find('img[class=uploadImage]').attr('src');

            $target.remove();
            $.post(aspxItemModulePath + "ItemCostVariantsFileUpload.aspx", { DeleteImage: true, OldFileName: old }, function (ajaxFileResponse) {
            });


        },
        ClearAttributeForm: function () {
            $('#Todatevalidation').prop('class', '');
            $('#Fromdatevalidation').prop('class', '');
            $("#ItemMgt_itemID").val(0);
            var attributeSetId = '';
            var itemTypeId = '';
            attributeSetId = $("#ddlAttributeSet").val();
            itemTypeId = $("#ddlItemType").val();
            $("#spanSample").html("");
            $("#spanActual").html("");
            ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);

        },

        ResetImageTab: function () {
            $("#divTableWrapper>table>thead").html('');
            $("#divTableWrapper>table>tbody").html('');
        },

        SaveItem: function (formID, attributeSetId, itemTypeId, itemId, itemSKU) {

            dynamicItemId = itemId;
            dynamicItemSKU = itemSKU;
            var BrandID = 0;
            itemEditFlag = itemId;
            var itemVideoIDs = "";
            var sourceFileCollection = '';
            var filepath = '';
            var contents = '';
            var counter = 0;
            var categoriesSelected = false;
            if (itemTypeId == 1 || itemTypeId == 2) {
                BrandID = $("#lstBrands option:selected").val();
                if (BrandID == undefined) {
                    csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select brand where it belongs") + '.</p>');
                    return false;
                }
            }
            itemVideoIDs = $("#txtVideoID").val();

            $("#multipleUpload .classTableWrapper > tbody >tr").each(function () {
                if (aspxRootPath != "/") {
                    filepath = $(this).find(" td:first >img").attr("src").replace(aspxRootPath, "");
                }
                else {
                    filepath = $(this).find(" td:first >img").attr("src").replace('/', '');
                }
                filepath = filepath.replace("/Small", "");
                filepath = filepath.replace("/Medium", "");
                filepath = filepath.replace("/Large", "");
                var path_array = filepath.split('/');
                var sizeofArray = path_array.length;

                var fileName = path_array[sizeofArray - 1];

                sourceFileCollection += fileName + '%';
                contents += filepath + "%"; if ($(this).find(" td:eq(6) input:checkbox").is(":checked")) {
                    contents += 1;
                    contents += '%';
                }
                else {
                    contents += 0;
                    contents += '%';
                }
                if ($(this).find(" td:eq(3) input:radio").is(":checked")) {
                    counter += 1;
                    contents += $(this).find(" td:eq(3) input:radio:checked").prop("value");
                    contents += '%';
                }
                else if ($(this).find(" td:eq(4) input:radio").is(":checked")) {
                    contents += $(this).find(" td:eq(4) input:radio:checked").prop("value");
                    contents += '%';
                }
                else if ($(this).find(" td:eq(5) input:radio").is(":checked")) {
                    contents += $(this).find(" td:eq(5) input:radio:checked").prop("value");
                    contents += '%';
                }
                else {
                    contents += "None";
                    contents += '%';
                }
                if ($(this).find(" td:eq(1) input").prop("value") != null) {
                    contents += $(this).find(" td:eq(1) input").prop("value");
                    contents += '%';
                }
                else {
                    contents += " ";
                    contents += '%';
                }
                if ($(this).find(" td:eq(2) input").prop("value") != null) {
                    contents += $(this).find(" td:eq(2) input").prop("value");
                    contents += '%';
                }
                contents += $(this).attr("value");
                contents += '#';
            });

            if (counter <= 1) {
                if (itemTypeId == 5) {
                    if ($("#gdvAssociatedItems").html() == '') {
                        ItemMangement.GroupItems.Get(itemId, null, null, null, null);
                    }
                    var associatedItems_ids = SageData.Get("gdvAssociatedItems").Arr.join(',');
                    if (associatedItems_ids.length <= 0) {
                        $("#gdvAssociatedItems").addClass("cssClassGroupProductError");
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select at least 1 item to be in a group") + '.</p>');
                        return false;
                    }
                    else {
                        $("#gdvAssociatedItems").removeClass("cssClassGroupProductError");
                    }
                }
                else {
                    var associatedItems_ids = "";
                }
                var relatedItems_ids = '';
                var upSellItems_ids = '';
                var crossSellItems_ids = '';
                if (SageData != undefined) {
                    if (p.EnableRelatedItem == true) {
                        if (SageData.Get("gdvRelatedItems") != undefined) {
                            relatedItems_ids = SageData.Get("gdvRelatedItems").Arr.join(',');
                        }
                    }
                    else {
                        relatedItems_ids = relCheckedItemID;
                    }

                    if (p.EnableUpSellItem == true) {
                        if (SageData.Get("gdvUpSellItems") != undefined) {
                            upSellItems_ids = SageData.Get("gdvUpSellItems").Arr.join(',');
                        }
                    }
                    else {
                        upSellItems_ids = upCheckedItemID;
                    }

                    if (p.EnableCrossSellItem == true) {
                        if (SageData.Get("gdvCrossSellItems") != undefined) {
                            crossSellItems_ids = SageData.Get("gdvCrossSellItems").Arr.join(',');
                        }
                    }
                    else {
                        crossSellItems_ids = crossCheckedItemID;
                    }
                }

                var taxRuleId = '';
                if (itemTypeId == 5) {
                    taxRuleId = 1;
                }
                else {
                    if ($('#ddlTax').val() >= 0) {
                        taxRuleId = $('#ddlTax').val();
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select at least one tax rule") + '.</p>');
                        return false;
                    }
                }

                var currencyCode;
                if (itemTypeId == 5) {
                    currencyCode = $("#ddlCurrencyGroup").val();
                }
                else {
                    currencyCode = $("#ddlCurrency").val();
                }

                var categoriesSelectedID = "";
                if (itemTypeId == 3) {
                    var ids = $("#ddlGCCategory").val() == null ? 0 : $("#ddlGCCategory").val().join(',');
                    if (ids != 0) {
                        categoriesSelectedID = ids;
                        categoriesSelected = true;
                    }
                    else {
                        csscody.alert("<h2>" + getLocale(AspxItemsManagement, 'Information Alert') + "</h2><p>" + getLocale(AspxItemsManagement, 'select category of gift card') + "</p>");
                        return false
                    }
                } else {

                    $("#lstCategories").each(function () {
                        if ($("#lstCategories :selected").length != 0) {
                            categoriesSelected = true;
                            $("#lstCategories option:selected").each(function (i) {
                                categoriesSelectedID += $(this).val() + ',';
                            });
                            categoriesSelectedID = categoriesSelectedID.substr(0, categoriesSelectedID.length - 1);
                        }
                    });
                }

                if (itemTypeId == 2) {
                    if (itemId == 0) {
                        var fileActualNewPath = $("#fileActual").prop('name');
                    }
                    else {
                        var fileActualNewPath = $("#fileActual").prop('title');
                    }
                    if (fileActualNewPath == "") {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please upload the downloadable file") + '.</p>');
                        return false;
                    }
                }
                var itemSetting = ItemMangement.ItemSetting.GetSetting();
                var itemGroupPrice = ItemMangement.GroupPrice.Get();
                var kitConfig = ItemMangement.ItemTypes.Kit.Get();
                if (itemTypeId == 6) {
                    if (kitConfig.KitConfig.length == 0) {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please add one the kit item") + '.</p>');
                        return false;
                    }
                }
                var itemSaveInfo = {
                    ItemId: itemId,
                    ItemTypeId: itemTypeId,
                    AttributeSetId: attributeSetId,
                    BrandId: BrandID,
                    CurrencyCode: currencyCode,
                    ItemVideoIDs: itemVideoIDs,
                    TaxRuleId: taxRuleId,
                    CategoriesIds: categoriesSelectedID,
                    AssociatedItemIds: associatedItems_ids,
                    RelatedItemsIds: relatedItems_ids,
                    UpSellItemsIds: upSellItems_ids,
                    CrossSellItemsIds: crossSellItems_ids,
                    DownloadItemsValue: ItemMangement.GetDownloadableFormData(itemTypeId),
                    SourceFileCol: sourceFileCollection,
                    DataCollection: contents,
                    FormVars: ItemMangement.SerializeForm(formID)
                    , Settings: itemSetting,
                    GroupPrice: itemGroupPrice,
                    KitConfig: kitConfig
                };

                var aspxTempCommonObj = aspxCommonObj();
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                if (categoriesSelected) {
                    AspxCommerce.CheckSessionActive(aspxCommonObj());
                    if (AspxCommerce.vars.IsAlive) {
                        this.config.url = this.config.baseURL + "SaveItemAndAttributes";
                        this.config.data = JSON2.stringify({ itemObj: itemSaveInfo, aspxCommonObj: aspxTempCommonObj });
                        this.config.ajaxCallMode = 27;
                        this.ajaxCall(this.config);
                    }
                    else {
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                    }
                }
                else {
                    csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select at least one category where it belongs.") + '</p>');
                    return false;
                }

            }
            else {
                csscody.alert('<h2>' + getLocale(AspxItemsManagement, "Information Alert") + '</h2><p>' + getLocale(AspxItemsManagement, "Please select only one base image for item.") + '</p>');
                return false;
            }
            listImages = new Array();
        },

        GetDownloadableFormData: function (itemTypeId) {
            var downloadabelItem = "";
            if (itemTypeId == 2) {
                var titleHead = $("#txtDownloadTitle").val();
                var maxDownload = $("#txtMaxDownload").val();
                var isSharable = false; var fileSamplePrevious = $("#fileSample").prop("title"); var fileSampleNewPath = $("#fileSample").prop('name');
                var fileActualPrevious = $("#fileActual").prop("title"); var fileActualNewPath = $("#fileActual").prop('name');
                var displayorder = 1;
                downloadabelItem = '' + titleHead + '%' + maxDownload + '%' + isSharable + '%' + fileSamplePrevious + '%' + fileSampleNewPath + '%' + fileActualPrevious + '%' + fileActualNewPath + '%' + displayorder + '';
            }
            return downloadabelItem;
        },

        RemoveHtml: function () {
            $('#multipleUpload div.sfGridWrapperContent>table>tbody').html('');
        },

        CreateHtml: function () {
            $('#multipleUpload div.sfGridWrapperContent').html("<table class=\"classTableWrapper\" width=\"100%\" border=\"0\" cellpadding=\"o\" cellspacing=\"0\"> <thead></thead><tbody></tbody></table>");
        },

        CreateTableHeader: function () {
            if ($("#multipleUpload .classTableWrapper > thead>tr").val() == null) {
                $("<tr class=\"cssClassHeading\"><td>" + getLocale(AspxItemsManagement, "Image") + "</td><td>" + getLocale(AspxItemsManagement, "Description") + "</td><td>" + getLocale(AspxItemsManagement, "Display Order") + "</td><td>" + getLocale(AspxItemsManagement, "Base Image") + "</td><td>" + getLocale(AspxItemsManagement, "Small Image") + "</td><td>" + getLocale(AspxItemsManagement, "Thumbnail") + "</td><td>" + getLocale(AspxItemsManagement, "Active") + "</td><td>" + getLocale(AspxItemsManagement, "Remove") + "</td></tr>").appendTo("#multipleUpload .classTableWrapper > thead");
            }
        },
        BindCurrencyList: function () {
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/BindCurrencyList",
                data: JSON2.stringify({ aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    var currencyElements = '';
                    $.each(msg.d, function (index, value) {
                        if (value.IsPrimaryForStore == true) {
                            currencyElements += '<option value=' + value.CurrencyCode + ' selected =selected>' + value.CurrencyName + '</option>';
                        }
                        else {
                            currencyElements += '<option value=' + value.CurrencyCode + '>' + value.CurrencyName + '</option>';
                        }
                    });
                    $(".sfcurrencyList").append(currencyElements);
                    $(".sfcurrencyListGroup").append(currencyElements);
                    if (currencyCodeEdit == "") {
                        currencyCodeEdit = currencyCode;
                    }
                    if (primaryCode != currencyCodeEdit) {
                        primaryCode = currencyCodeEdit;
                    }
                    $("#ddlCurrency").val(primaryCode);
                    $("#ddlCurrencyGroup").val(primaryCode);
                    $("#ddlCurrencyLP").val(primaryCode);
                    $("#ddlCurrencyCP").val(primaryCode);
                    $("#ddlCurrencySP").val(primaryCode);
                    $("#ddlCurrencyMP").val(primaryCode);
                    $('#tblQuantityDiscount').find('thead').find('.cssClassUnitPrice').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):');

                }
            });
        },

        SerializeForm: function (formID) {
            var jsonStr = '';
            var frmValues = new Array();
            radioGroups = new Array();
            checkboxGroups = new Array();
            selectGroups = new Array();
            inputs = $(formID).find('INPUT, SELECT, TEXTAREA');
            $.each(inputs, function (i, item) {
                input = $(item);
                if (input.hasClass("dynFormItem")) {
                    var found = false;
                    switch (input[0].type) {
                        case 'text':
                            jsonStr += '{"name":"' + input.prop('name') + '","value":"' + $.trim(input.val()) + '"},';
                            break;
                        case 'select-multiple':
                            for (var i = 0; i < selectGroups.length; i++) {
                                if (selectGroups[i] == input.prop('name')) {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) {
                                selectGroups[selectGroups.length] = input.prop('name');
                            }
                            break;
                        case 'select-one':
                            jsonStr += '{"name":"' + input.prop('name') + '","value":"' + input.get(0)[input.prop('selectedIndex')].value + '"},';
                            break;

                        case 'checkbox':
                            var ids = String(input.prop('name')).split("_");
                            if (ids[1] == 4) {
                                jsonStr += '{"name":"' + input.prop('name') + '","value":"' + input.is(':checked') + '"},';
                            }
                            else {
                                for (var i = 0; i <= checkboxGroups.length; i++) {
                                    if (checkboxGroups[i] == input.prop('name')) {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found) {
                                    checkboxGroups[checkboxGroups.length] = input.prop('name');
                                }
                            }
                            break;

                        case 'radio':
                            for (var i = 0; i < radioGroups.length; i++) {
                                if (radioGroups[i] == input.prop('name')) {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) {
                                radioGroups[radioGroups.length] = input.prop('name');
                            }
                            break;

                        case 'file':
                            var d = input.parent();
                            var img = $(d).find('span.response img.uploadImage');
                            if (img.length > 0) {
                                var imgToUpload = "";
                                if (img.attr("src") != undefined) {
                                    imgToUpload = img.attr("src");
                                }
                                jsonStr += '{"name":"' + input.prop('name') + '","value":"' + imgToUpload.replace(aspxRootPath, "") + '"},';
                            }
                            else {
                                var a = $(d).find('span.response a.uploadFile');
                                var fileToUpload = "";
                                if (a.prop("href") != undefined) {
                                    fileToUpload = a.prop("href");
                                }
                                if (a) {
                                    jsonStr += '{"name":"' + input.prop('name') + '","value":"' + fileToUpload.replace(aspxRootPath, "") + '"},';
                                }
                            }
                            var hdn = $(d).find('input[type="hidden"]');
                            if (hdn) {
                                jsonStr += '{"name":"' + hdn.prop('name') + '","value":"' + hdn.val() + '"},';
                            }
                            break;

                        case 'password':
                            jsonStr += '{"name":"' + input.prop('name') + '","value":"' + $.trim(input.val()) + '"},';
                            break;
                        case 'textarea':
                            jsonStr += '{"name":"' + input.prop('name') + '","value":"' + $.trim(input.val().replace(/(&nbsp;)*/g, "")) + '"},';

                            break;
                        default:
                            break;
                    }
                }
            });
            for (var i = 0; i < selectGroups.length; i++) {
                var selIDs = '';
                $('#' + selectGroups[i] + ' :selected').each(function (i, selected) {
                    selIDs += $(selected).val() + ",";
                });
                selIDs = selIDs.substr(0, selIDs.length - 1);
                jsonStr += '{"name":"' + selectGroups[i] + '","value":"' + selIDs + '"},';
            }

            for (var i = 0; i < checkboxGroups.length; i++) {
                var chkValues = '';
                $('input[name=' + checkboxGroups[i] + ']').each(function (i, item) {
                    if ($(this).is(':checked')) {
                        chkValues += $(this).val() + ",";
                    }
                });
                chkValues = chkValues.substr(0, chkValues.length - 1);
                jsonStr += '{"name":"' + checkboxGroups[i] + '","value":"' + chkValues + '"},';
            }

            for (var i = 0; i < radioGroups.length; i++) {
                var radValues = '';
                radValues = $('input[name=' + radioGroups[i] + ']:checked').val();
                jsonStr += '{"name":"' + radioGroups[i] + '","value":"' + radValues + '"},';
            }
            jsonStr = jsonStr.substr(0, jsonStr.length - 1);
            return '[' + jsonStr + ']';
        },

        CreateFileUploader: function (uploaderID) {
            new AjaxUpload(String(uploaderID), {
                action: aspxItemModulePath + 'FileUploader.aspx',
                name: 'myfile',
                onSubmit: function (file, ext) {
                    d = $('#' + uploaderID).parent();
                    baseLocation = d.prop("name");
                    validExt = d.prop("class");
                    maxFileSize = d.prop("lang");
                    var regExp = /\s+/g;
                    myregexp = new RegExp("(" + validExt.replace(regExp, "|") + ")", "i");
                    if (ext != "exe") {
                        if (ext && myregexp.test(ext)) {
                            this.setData({
                                'BaseLocation': baseLocation, 'ValidExtension': validExt, 'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxItemsManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxItemsManagement, 'You are trying to upload invalid file type!') + '</p>');
                            return false;
                        }
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxItemsManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxItemsManagement, 'You are trying to upload invalid file type!') + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, ajaxFileResponse) {
                    d = $('#' + uploaderID).parent();
                    var res = eval(ajaxFileResponse);
                    if (res.Status > 0) {
                        baseLocation = d.prop("name");
                        validExt = d.prop("class");
                        var fileExt = (-1 !== file.indexOf('.')) ? file.replace(/.*[.]/, '') : '';
                        myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                        if (myregexp.test(fileExt)) {
                            $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + res.UploadedPath + '" class="uploadImage" height="90px" width="100px" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt=' + getLocale(AspxItemsManagement, "Delete") + ' title=' + getLocale(AspxItemsManagement, "Delete") + '/></div>');
                        }
                        else {
                            $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + res.UploadedPath + '" class="uploadFile" target="_blank">' + file + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt=' + getLocale(AspxItemsManagement, "Delete") + ' title=' + getLocale(AspxItemsManagement, "Delete") + '/></div>');
                        }
                    }
                    else {
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, 'Error Message') + '</h2><p>' + res.Message + '</p>');
                    }
                }
            });
        },

        SearchItems: function () {
            var sku = $.trim($("#txtSearchSKU").val());
            var Nm = $.trim($("#txtSearchName").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var itemType = '';
            if ($("#ddlSearchItemType").val() != 0) {
                itemType = $.trim($("#ddlSearchItemType").val());
            }
            else {
                itemType = null;
            }
            var attributeSetNm = '';
            if ($("#ddlAttributeSetName").val() != 0) {
                attributeSetNm = $.trim($("#ddlAttributeSetName").val());
            }
            else {
                attributeSetNm = null;
            }
            var visibility = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);
            ItemMangement.BindItemsGrid(sku, Nm, itemType, attributeSetNm, visibility, isAct);
        },
        SearchRelatedItems: function () {
            var itemSKU = $.trim($("#txtItemSKU").val());
            var itemName = $.trim($("#txtItemName").val());
            var itemTypeID = '';
            var attributeSetID = '';
            if (itemSKU.length < 1) {
                itemSKU = null;
            }
            if (itemName.length < 1) {
                itemName = null;
            }

            if ($("#ddlSelectItemType option:selected").val() != 0) {
                itemTypeID = $.trim($("#ddlSelectItemType  option:selected").val());
            }
            else {
                itemTypeID = null;
            }

            if ($("#ddlSelectAttributeSetName option:selected").val() != 0) {
                attributeSetID = $.trim($("#ddlSelectAttributeSetName option:selected").val());
            }
            else {
                attributeSetID = null;
            }

            var itemIDSearch = $("#ItemMgt_itemID").val();
            ItemMangement.BindRelatedItemsGrid(itemIDSearch, itemSKU, itemName, itemTypeID, attributeSetID);

        },

        SearchUpSellItems: function () {
            var itemSKUSell = $.trim($("#txtItemSKUSell").val());
            var itemNameSell = $.trim($("#txtItemNameSell").val());
            var itemTypeIDSell = '';
            var attributeSetIDSell = '';
            if (itemSKUSell.length < 1) {
                itemSKUSell = null;
            }
            if (itemNameSell.length < 1) {
                itemNameSell = null;
            }

            if ($("#ddlSelectItemTypeSell option:selected").val() != 0) {
                itemTypeIDSell = $.trim($("#ddlSelectItemTypeSell  option:selected").val());
            }
            else {
                itemTypeIDSell = null;
            }

            if ($("#ddlSelectAttributeSetNameSell option:selected").val() != 0) {
                attributeSetIDSell = $.trim($("#ddlSelectAttributeSetNameSell option:selected").val());
            }
            else {
                attributeSetIDSell = null;
            }

            var itemIDSearchSell = $("#ItemMgt_itemID").val();
            ItemMangement.BindUpSellItemsGrid(itemIDSearchSell, itemSKUSell, itemNameSell, itemTypeIDSell, attributeSetIDSell);

        },
        SearchCrossSellItems: function () {
            var itemSKUcs = $.trim($("#txtItemSKUcs").val());
            var itemNamecs = $.trim($("#txtItemNamecs").val());
            var itemTypeIDcs = '';
            var attributeSetIDcs = '';
            if (itemSKUcs.length < 1) {
                itemSKUcs = null;
            }
            if (itemNamecs.length < 1) {
                itemNamecs = null;
            }

            if ($("#ddlSelectItemTypecs option:selected").val() != 0) {
                itemTypeIDcs = $.trim($("#ddlSelectItemTypecs  option:selected").val());
            }
            else {
                itemTypeIDcs = null;
            }

            if ($("#ddlSelectAttributeSetNamecs option:selected").val() != 0) {
                attributeSetIDcs = $.trim($("#ddlSelectAttributeSetNamecs option:selected").val());
            }
            else {
                attributeSetIDcs = null;
            }

            var itemIDSearchcs = $("#ItemMgt_itemID").val();
            ItemMangement.BindCrossSellItemsGrid(itemIDSearchcs, itemSKUcs, itemNamecs, itemTypeIDcs, attributeSetIDcs);

        },
        AddImages: function (data, val) {
            var lst = $(data).prop("name");
            var subStr = lst.split('@');
            var List = '';
            $.each(subStr, function (index) {
                List += '<tr>';
                List += '<td><img src="' + aspxRootPath + subStr[index] + '" class="uploadImage" height="100px" width="125px"/></td>';
                List += '<td><i class="imgDelete icon-delete" id="btn" onclick="ItemMangement.DeleteImage(this)" /></i></td>';
                List += '</tr>';
            });
            if (lst != '' && lst != "undefined") {
                $("#VariantsImagesTable>tbody").html('');
                $("#VariantsImagesTable").show();
                $("#VariantsImagesTable>tbody").append(List);
                $("#VariantsImagesTable>tbody tr:even").addClass("sfEven");
                $("#VariantsImagesTable>tbody tr:odd").addClass("sfOdd");
                $('#btnSaveImages').show();
                $('#btnImageBack').show();
            } else {
                $("#VariantsImagesTable>tbody").html('');
                $("#VariantsImagesTable").hide();
                $('#btnSaveImages').hide();
                $('#btnImageBack').hide();
            }
            $("#imageUploader").show();
            ShowPopupControl('popuprel2');
            ItemMangement.CostVariantsImageUploader(maxFileSize);
        },
        ClearSearchFields: function () {
            $("#txtSearchSKU").val('');
            $("#txtSearchName").val('');
            $("#ddlSearchItemType").val('0');
            $("#ddlAttributeSetName").val('0');
            $("#ddlVisibitity").val('');
            $("#ddlIsActive").val('');
        },
        BindItemTabSetting: function () {
            $("#chkEnableCostVariantOption").prop('checked', p.EnableCostVariantOption);
            $("#chkEnableGroupPrice").prop('checked', p.EnableGroupPrice);
            $("#chkEnableTierPrice").prop('checked', p.EnableTierPrice);
            $("#chkEnableRelatedItem").prop('checked', p.EnableRelatedItem);
            $("#chkEnableCrossSellItem").prop('checked', p.EnableCrossSellItem);
            $("#chkEnableUpSellItem").prop('checked', p.EnableUpSellItem);
        },
        SaveItemTabSetting: function () {
            var chkEnableCostVariantOption = $("#chkEnableCostVariantOption").prop('checked');
            var chkEnableGroupPrice = $("#chkEnableGroupPrice").prop('checked');
            var chkEnableTierPrice = $("#chkEnableTierPrice").prop('checked');
            var chkEnableRelatedItem = $("#chkEnableRelatedItem").prop('checked');
            var chkEnableCrossSellItem = $("#chkEnableCrossSellItem").prop('checked');
            var chkEnableUpSellItem = $("#chkEnableUpSellItem").prop('checked');
            var settingKeys = "EnableCostVariantOption*EnableGroupPrice*EnableTierPrice*EnableRelatedItem*EnableCrossSellItem*EnableUpSellItem";
            var settingValues = chkEnableCostVariantOption + "*" + chkEnableGroupPrice + "*" + chkEnableTierPrice + "*" + chkEnableRelatedItem
            + "*" + chkEnableCrossSellItem + "*" + chkEnableUpSellItem;
            this.config.url = this.config.baseURL + "ItemTabSettingSave";
            this.config.data = JSON2.stringify({ SettingKeys: settingKeys, SettingValues: settingValues, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 49;
            this.ajaxCall(this.config);
        },
        GetItemTabSettings: function () {
            this.config.url = this.config.baseURL + "ItemTabSettingGet";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 50;
            this.ajaxCall(this.config);
        },
        ajaxFailure: function (msg) {
            switch (ItemMangement.config.ajaxCallMode) {
                case 1:
                    csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to bind tax rules!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to bind item quantity discount!") + '</p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to bind roles!") + '</p>');
                    break;
                case 4:
                    if (showPopup)
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to save item quantity discount!") + '</p>');
                    break;
                case 5:
                    csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to delete item quantity discount!!") + '</p>');
                    break;
                case 6:
                    csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to delete item cost variant value!") + '</p>');
                    break;
                case 13:
                    if (showPopup)
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to save item cost variant!") + '</p>');
                    break;
            }
        },
        ajaxSuccess: function (msg) {
            switch (ItemMangement.config.ajaxCallMode) {
                case 1:
                    var option = '';
                    $("#ddlTax").html('');
                    $.each(msg.d, function (ind, item) {

                        option += '<option value="' + item.TaxItemClassID + '">' + item.TaxItemClassName + '</option>';

                    });
                    $("#ddlTax").append(option);
                    return true;
                    break;
                case 2:
                    $("#tblQuantityDiscount>tbody").html('');
                    var length = msg.d.length;
                    if (length > 0) {
                        var arrItems = new Array();
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            var newQuantityDiscountRow = '';
                            newQuantityDiscountRow += '<tr><td><input type="hidden" size="3" class="cssClassQuantityDiscount" value="' + item.QuantityDiscountID + '"><input type="text" size="3" class="cssClassQuantity" value="' + item.Quantity + '"></td>';
                            newQuantityDiscountRow += '<td><input type="text" size="5" class="cssClassPrice" value="' + parseFloat(item.Price).toFixed(2) + '"></td>';
                            newQuantityDiscountRow += '<td><div class="cssClassUsersInRoleCheckBox"></div></td>';
                            newQuantityDiscountRow += '<td><span class="nowrap">';
                            newQuantityDiscountRow += '<img  class="cssClassAddDiscountRow" title=' + getLocale(AspxItemsManagement, "Add empty item") + ' alt=' + getLocale(AspxItemsManagement, "Add empty item") + ' name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" >&nbsp;';
                            newQuantityDiscountRow += '<img  class="cssClassCloneDiscountRow" alt=' + getLocale(AspxItemsManagement, "Clone this item") + ' title=' + getLocale(AspxItemsManagement, "Clone this item") + ' name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" >&nbsp;';
                            newQuantityDiscountRow += '<img  class="cssClassDeleteDiscountRow" alt=' + getLocale(AspxItemsManagement, "Remove this item") + ' name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" >&nbsp;';
                            newQuantityDiscountRow += '</span></td></tr>';
                            $("#tblQuantityDiscount>tbody").append(newQuantityDiscountRow);
                            arrItems.push(item.RoleIDs);
                        };
                        ItemMangement.GetUserInRoleList(arrItems);
                    }
                    else {
                        var arrItems = new Array();
                        var newQuantityDiscountRow = '';
                        newQuantityDiscountRow += '<tr><td><input type="hidden" size="3" class="cssClassQuantityDiscount" value="0"><input type="text" size="3" class="cssClassQuantity"></td>';
                        newQuantityDiscountRow += '<td><input type="text" size="5" class="cssClassPrice"></td>';
                        newQuantityDiscountRow += '<td><div class="cssClassUsersInRoleCheckBox"></div></td>';
                        newQuantityDiscountRow += '<td><span class="nowrap">';
                        newQuantityDiscountRow += '<i class="cssClassAddDiscountRow sfBtn icon-addnew" >' + getLocale(AspxItemsManagement, "Add empty item") + '</i>';
                        newQuantityDiscountRow += '<i class="cssClassCloneDiscountRow sfBtn icon-clone" >' + getLocale(AspxItemsManagement, "Clone this item") + '</i>';
                        newQuantityDiscountRow += '<i class="cssClassDeleteDiscountRow sfBtn icon-delete" >' + getLocale(AspxItemsManagement, "Remove this item") + '</i>';
                        newQuantityDiscountRow += '</span></td></tr>';
                        $("#tblQuantityDiscount>tbody").append(newQuantityDiscountRow);

                        if ($("#btnAddQuantityDiscount").attr('clicked')) {

                        } else {
                            var backButton = $("<button type='button' class='sfBtn'/>");
                            backButton.html(getLocale(AspxItemsManagement, "Back"));
                            backButton.click(function () {
                                $("#tblQuantityDiscount").parent('div.sfFormwrapper').hide();
                                $("#dvAddNewQuantityDiscount").show();
                                return false;
                            });

                            $("#btnDeleteQuantityDiscount").parents('div.sfButtonwrapper:eq(0)').append(backButton);

                            $("#tblQuantityDiscount").parent('div.sfFormwrapper').hide();
                            $("#dvAddNewQuantityDiscount").show();
                        }
                        ItemMangement.GetUserInRoleList(arrItems);
                    }
                    $("#tblQuantityDiscount>tbody tr:even").addClass("sfEven");
                    $("#tblQuantityDiscount>tbody tr:odd").addClass("sfOdd");
                    $("#tblQuantityDiscount>tbody").find("tr:eq(0)").find("img.cssClassDeleteDiscountRow").hide();
                    $(".cssClassPrice").DigitAndDecimal('.cssClassPrice', '');
                    $(".cssClassQuantity").DigitOnly('.cssClassQuantity', '');

                    $(".cssClassPrice,.cssClassQuantity").bind("contextmenu", function (e) {
                        return false;
                    });
                    break;
                case 3:
                    var groproles = "";
                    $.each(msg.d, function (index, item) {
                        if (itemTypeId == 2 && $.trim(item.RoleName) == "Anonymous User") {
                        } else {
                            ItemMangement.BindRolesList(item);
                            groproles += "<div><label><input type='radio' name='roles' value=" + item.RoleID + " >" + item.RoleName + " </label></div>";
                        }
                        $(".cssClassUsersInRoleCheckBox2").html(groproles);
                        $("#tblGroupPrice").data('items', msg.d);
                        $(".cssClassUsersInRoleCheckBox2").find("input[type=radio]:first").prop("checked", "checked").attr('disabled', 'disabled');
                    });
                    arrRoles = ItemMangement.vars.arrRoles;
                    if (arrRoles.length > 0) {
                        var divData = $('div.cssClassUsersInRoleCheckBox');
                        $.each(divData, function (index, item) {
                            $.each(arrRoles, function (i) {
                                if (i == index) {
                                    var arr = arrRoles[i].split(",");
                                    $.each(arr, function (j) {
                                        $(item).find("input[value=" + arr[j] + "]").prop("checked", "checked");
                                    });
                                }
                            });
                        });
                    }
                    break;
                case 4:
                    var item_Id = $("#ItemMgt_itemID").val();
                    ItemMangement.BindItemQuantityDiscountsByItemID(item_Id);
                    csscody.info("<h2>" + getLocale(AspxItemsManagement, "Successful Information") + "</h2><p>" + getLocale(AspxItemsManagement, 'Item discount quantity has been saved successfully') + "</p>");
                    break;
                case 5:
                    ItemMangement.vars.parentRow.remove();
                    csscody.info("<h2>" + getLocale(AspxItemsManagement, "Successful Information") + "</h2><p>" + getLocale(AspxItemsManagement, 'Item discount quantity  has been deleted successfully') + "</p>");
                    return false;
                    break;
                case 6:
                    ItemMangement.vars.parentRow.remove();
                    csscody.info("<h2>" + getLocale(AspxItemsManagement, "Successful Information") + "</h2><p>" + getLocale(AspxItemsManagement, "Item cost variant value has been deleted successfully.") + "</p>");
                    return false;
                    break;
                case 7:
                    ItemMangement.FillForm(msg);
                    ItemMangement.BindItemCostVariantValueByCostVariantID(ItemMangement.vars.itemCostVariantId, ItemMangement.vars.itemId, ItemMangement.vars.costVariantId);
                    $("#variantsGrid,#divExistingVariant").hide();
                    $("#newCostvariants,#divNewVariant").show();
                    break;
                case 8:
                    var length = msg.d.length;
                    if (length > 0) {
                        $("#tblVariantTable>tbody").html('');
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            listImages.push(item.ImagePath);
                            if (item.DisplayOrder == null) {
                                item.DisplayOrder = '';
                            }
                            var newVariantRow = '';
                            newVariantRow += '<tr><td><input type="hidden" size="3" class="cssClassVariantValue" value="' + item.CostVariantsValueID + '"><input type="text" size="3" class="cssClassDisplayOrder" value="' + item.DisplayOrder + '"></td>';
                            newVariantRow += '<td><input type="text" class="cssClassItemCostVariantValueName" value="' + item.CostVariantsValueName + '"></td>';
                            newVariantRow += '<td><input type="text" size="5" class="cssClassPriceModifier" value="' + item.CostVariantsPriceValue + '">&nbsp;/&nbsp;';
                            newVariantRow += '<select class="cssClassPriceModifierType priceModifierType_' + item.CostVariantsValueID + '"><option value="false">' + currencyCodeSlected + '</option><option value="true">%</option></select></td>';
                            newVariantRow += '<td><input type="text" size="5" class="cssClassWeightModifier" value="' + item.CostVariantsWeightValue + '">&nbsp;/&nbsp;';
                            newVariantRow += '<select class="cssClassWeightModifierType weightModifierType_' + item.CostVariantsValueID + '"><option value="false">lbs</option><option value="true">%</option></select></td>';
                            newVariantRow += '<td><select class="cssClassIsActive isActive_' + item.CostVariantsValueID + '"><option value="true">Active</option><option value="false">Inactive</option></select></td>';
                            newVariantRow += '<td><span class="nowrap">';
                            newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassAddRow" title=' + getLocale(AspxItemsManagement, "Add empty item") + ' alt=' + getLocale(AspxItemsManagement, "Add empty item") + ' name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" >&nbsp;';
                            newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassCloneRow" alt=' + getLocale(AspxItemsManagement, "Clone this item") + ' title=' + getLocale(AspxItemsManagement, "Clone this item") + ' name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" >&nbsp;';
                            newVariantRow += '<img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" alt=' + getLocale(AspxItemsManagement, "Remove this item") + ' name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" >&nbsp;';
                            newVariantRow += '<button type="button" value=' + index + ' name="' + item.ImagePath + '" class="classAddImagesEdit sfBtn" rel="popuprel2" onclick="ItemMangement.AddImages(this,' + index + ')" ><span class="icon-addnew">' + getLocale(AspxItemsManagement, "Add Images") + '</span></button>';
                            newVariantRow += '</span></td></tr>';
                            $("#tblVariantTable>tbody").append(newVariantRow);
                            $('.priceModifierType_' + item.CostVariantsValueID).val('' + item.IsPriceInPercentage + '');
                            $('.weightModifierType_' + item.CostVariantsValueID).val('' + item.IsWeightInPercentage + '');
                            $('.isActive_' + item.CostVariantsValueID).val('' + item.IsActive + '');
                        };
                        $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();
                    }
                    break;
                case 9:
                    ItemMangement.BindCostVariantsOptions(itemId, aspxCommonObj());
                    ItemMangement.BindItemCostVariantInGrid(itemId, aspxcommonObj());
                    break;
                case 10:
                    $.each(msg.d, function (index, item) {
                        ItemMangement.BindInputTypeDropDown(item);
                    });
                    break;
                case 11:
                    var length = msg.d.length;
                    if (length > 0) {
                        $('#lblExistingOptions').html(getLocale(AspxItemsManagement, "Existing Cost Variant Options:"));
                        $("select[id$=ddlExistingOptions] > option").remove();
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            ItemMangement.BindCostVariantsDropDown(item);
                        };
                        $("#divExisitingDropDown,#ddlExistingOptions,#btnApplyExisingOption").show();
                    }
                    else {
                        $("#ddlExistingOptions").hide();
                        $("#btnApplyExisingOption").hide();
                        $("#lblExistingOptions").html(getLocale(AspxItemsManagement, "There is no any existing cost variant options available!"));
                        $('#btnExisingBack').show();
                    }
                    break;
                case 12:
                    ItemMangement.vars.isUnique = msg.d;
                    break;
                case 13:
                    listImages = []; var alertMsg = '<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item cost variant has been saved successfully.") + '</p>';
                    ItemMangement.ResetCVHtml(true, alertMsg, aspxCommonObj());
                    if (ItemMangement.vars.isItemHasCostVariant) {
                        ItemMangement.GetEmail();
                    }
                    break;
                case 14:
                    csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Selected item(s) has been deleted successfully.") + '</p>');
                    ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                    break;
                case 15:
                    csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item has been deleted successfully.") + '</p>');
                    ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                    $("#gdvItems_form").hide();
                    $("#gdvItems_accordin").hide();
                    $("#ItemMgt_itemID").val(0);
                    $("#gdvItems_grid").show();
                    break;
                case 16:
                    ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                    break;
                case 17:
                    ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                    break;
                case 18:
                    attributeSetArray = msg.d;
                    $("#ddlAttributeSet").get(0).options.length = 0;
                    $.each(msg.d, function (index, item) {
                        $("#ddlAttributeSet").get(0).options[$("#ddlAttributeSet").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                        $("#ddlAttributeSetName").get(0).options[$("#ddlAttributeSetName").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                    });
                    break;
                case 19:
                    $('#ddlItemType').get(0).options.length = 0;
                    $.each(msg.d, function (index, item) {
                        if ($('#ddlAttributeSet').val() != 3) {
                            if (item.ItemTypeID != 4) {
                                $("#ddlItemType").get(0).options[$("#ddlItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                            }
                        } else if ($('#ddlAttributeSet').val() == 3) {
                            if (item.ItemTypeID == 4) {
                                $("#ddlItemType").get(0).options[$("#ddlItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                            }
                        }
                        $("#ddlSearchItemType").get(0).options[$("#ddlSearchItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                    });
                    break;
                case 35:
                    $.each(msg.d, function (index, item) {
                        $("#ddlSelectAttributeSetName").get(0).options[$("#ddlSelectAttributeSetName").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                        $("#ddlSelectAttributeSetNameSell").get(0).options[$("#ddlSelectAttributeSetNameSell").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                        $("#ddlSelectAttributeSetNamecs").get(0).options[$("#ddlSelectAttributeSetNamecs").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                    });
                    break;
                case 36:
                    $.each(msg.d, function (index, item) {
                        $("#ddlSelectItemType").get(0).options[$("#ddlSelectItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                        $("#ddlSelectItemTypeSell").get(0).options[$("#ddlSelectItemTypeSell").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                        $("#ddlSelectItemTypecs").get(0).options[$("#ddlSelectItemTypecs").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);

                    });
                    break;



                case 20:
                    maxFileSize = maxFileSize;
                    attributeSetId = ItemMangement.vars.attributeSetId;
                    itemTypeId = ItemMangement.vars.itemTypeId;
                    showDeleteBtn = ItemMangement.vars.showDeleteBtn;

                    itemId = ItemMangement.vars.itemId;
                    ItemMangement.CreateForm(msg.d, attributeSetId, itemTypeId, showDeleteBtn, itemId);
                    if (itemId > 0) {
                        ItemMangement.BindDataInAccordin(itemId, attributeSetId, itemTypeId, aspxCommonObj());
                        ItemMangement.BindDataInImageTab(itemId, aspxCommonObj());
                        ItemMangement.BindItemCostVariantInGrid(itemId, aspxCommonObj());
                        ItemMangement.BindItemQuantityDiscountsByItemID(itemId);

                        if (itemTypeId == 2) {
                            ItemMangement.BindDownloadableForm(itemId);
                        }
                    } else {
                        if (itemTypeId != 5) {
                            ItemMangement.InitCostVariantCombination($("#ItemMgt_itemID").val(), !($("#ItemMgt_itemID").val() == 0), aspxCommonObj());
                            ItemMangement.BindItemQuantityDiscountsByItemID(0);
                            $("#btnSaveQuantityDiscount,#btnDeleteQuantityDiscount").remove();
                        }
                    }
                    if (itemTypeId == 2) {
                        ItemMangement.SampleFileUploader(maxDownloadFileSize);
                        ItemMangement.ActualFileUploader(maxDownloadFileSize);
                    }
                    if (itemTypeId == 5) {
                        ItemMangement.GroupItems.Get(itemId, null, null, null, null);
                    }
                    ItemMangement.ImageUploader(maxFileSize);
                    $("#ddlCurrency").val(currencyCodeEdit);
                    $("#ddlCurrencyLP").val(currencyCodeEdit);
                    $("#ddlCurrencyCP").val(currencyCodeEdit);
                    $("#ddlCurrencySP").val(currencyCodeEdit);
                    $("#ddlCurrencyMP").val(currencyCodeEdit);
                    $("#gdvItems_grid").hide();
                    $("#gdvItems_form").show();
                    $("#gdvItems_accordin").show();
                    $("div.popbox").hide();
                    if (itemTypeId != 5) {
                        ItemMangement.GetPriceHistory(itemId);
                    }
                    $('.popbox').popbox();

                    $("#txtDownloadTitle").on("keypress", function (e) {
                        if (e.which == 37 || e.which == 44) {
                            return false;
                        }
                    });
                    $("#txtMaxDownload").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    });
                    $("#txtDownDisplayOrder").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    });

                    $('#txtMaxDownload').bind('paste', function (e) {
                        e.preventDefault();
                    });


                    $(".cssClassDisplayOrder").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    });

                    $(".cssClassPriceModifier").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                            if (e.which == 45) { return true; }
                            return false;
                        }
                    });
                    $(".cssclassCostVariantItemQuantity").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    });

                    $(".cssClassWeightModifier").on("keypress", function (e) {
                        if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                            if (e.which == 45) { return true; }
                            return false;
                        }
                    });
                    $(".cssClassWeightModifier").bind("contextmenu", function (e) {
                        return false;
                    });
                    $(".cssClassPriceModifier").bind("contextmenu", function (e) {
                        return false;
                    });

                    $(".cssClassSKU").on("keypress", function (e) {
                        if (e.which == 39 || e.which == 34) {
                            return false;
                        }
                    });
                    $("#btnSaveCostVariantCombination").off("click").bind("click", function () {

                        var variantsProperties = $("#dvCvForm table:first>tbody>tr>td.cssClassTableCostVariant").find("input.cssClassDisplayOrder,input.cssClassVariantValueName, .ddlCostVariantsCollection, .ddlCostVariantValues");
                        var isEmpty = false;
                        $.each(variantsProperties, function (index, item) {
                            if ($.trim($(this).val()) == 0) {
                                csscody.alert("<h2>" + getLocale(AspxItemsManagement, "Information Alert") + "</h2><p>" + getLocale(AspxItemsManagement, 'Please enter item cost variant properties.') + "</p>");
                                isEmpty = true;
                                return false;
                            }
                        });
                        if (!isEmpty) {
                            ItemMangement.SaveItemCostVariantsInfo('', aspxCommonObj());
                        }
                        return false;
                    });
                    $("#btnBackVariantOptions").bind("click", function () {
                        ItemMangement.OnInit();
                        $("#variantsGrid").show();
                        $("#newCostvariants").hide();
                        $('.classAddImages').removeAttr("name");
                        $('.classAddImagesEdit').removeAttr("name").removeAttr("onclick").removeClass("classAddImagesEdit").addClass("classAddImages");
                        return false;
                    });
                    $("#btnExisingBack").click(function () {
                        $("#variantsGrid").show();
                        $("#newCostvariants").hide();
                        return false;
                    });
                    $("#btnResetVariantOptions").click(function () {
                        ItemMangement.OnInit();
                        ItemMangement.ClearVariantForm();
                        return false;
                    });

                    $("#ddlCurrency").change(function () {
                        $("#ddlCurrencyLP option[value=" + $(this).val() + "]").prop("selected", "selected");
                        $("#ddlCurrencyCP option[value=" + $(this).val() + "]").prop("selected", "selected");
                        $("#ddlCurrencySP option[value=" + $(this).val() + "]").prop("selected", "selected");
                        $("#ddlCurrencyMP option[value=" + $(this).val() + "]").prop("selected", "selected");
                        currencyCodeSlected = $(this).val();
                        currencyCodeEdit = currencyCodeSlected;
                        $('#tblQuantityDiscount').find('thead').find('.cssClassUnitPrice').html('').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):');
                        $('#tblGroupPrice').find('.cssClassUnitPrice').html('').html(getLocale(AspxItemsManagement, "Unit Price") + '(' + currencyCodeSlected + '):');
                        ItemMangement.InitCostVariantCombination($("#ItemMgt_itemID").val(), !($("#ItemMgt_itemID").val() == 0), aspxCommonObj());

                    });

                    $('#btnApplyExisingOption').click(function () {
                        var variant_Id = $('#ddlExistingOptions').val();
                        var item_Id = $("#ItemMgt_itemID").val();
                        if (variant_Id != null && item_Id != null) {
                            var params = { itemId: item_Id, costVariantID: variant_Id, aspxCommonObj: aspxCommonObj() };
                            var mydata = JSON2.stringify(params);
                            $.ajax({
                                type: "POST", beforeSend: function (request) {
                                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                    request.setRequestHeader("UMID", umi);
                                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                    request.setRequestHeader("PType", "v");
                                    request.setRequestHeader('Escape', '0');
                                },
                                url: aspxservicePath + "AspxCoreHandler.ashx/AddItemCostVariant",
                                data: mydata,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () {
                                    RemovePopUp();
                                    ItemMangement.BindItemCostVariantInGrid(item_Id, aspxCommonObj());
                                    ItemMangement.BindCostVariantsOptions(item_Id, aspxCommonObj());
                                    csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item cost variant has been saved successfully.") + '</p>');
                                    $("#variantsGrid").show();
                                    $('#divExistingVariant,#lblCostVariantOptionTitle').hide();
                                },
                                error: function () {
                                    csscody.error('<h1>' + getLocale(AspxItemsManagement, "Error Message") + '</h1><p>' + getLocale(AspxItemsManagement, "Failed to save item cost variant") + '</p>');
                                }
                            });
                        }
                        return false;
                    });
                    $("#txtDownDisplayOrder,#txtMaxDownload").bind("contextmenu", function (e) {
                        return false;
                    });
                    $('.open').click(function () {
                        $('.classPriceHistory').show();
                        return false;
                    });
                    $(".FeaturedDropDown").trigger('change');
                    $(".SpecialDropDown").trigger('change');
                    break;
                case 21:
                    var length = msg.d.length;
                    if (length > 0) {
                        ImageType = {
                            "Large": "Large",
                            "Medium": "Medium",
                            "Small": "Small"
                        };
                        Imagelist = '';
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item.ImagePath != "") {
                                Imagelist += item.ImagePath + ';';
                            }
                        };
                        var ImageType = ImageType.Small;
                        ItemMangement.ResizeImageDynamically(Imagelist, ImageType);
                        ItemMangement.BindToTable(msg);
                        $("#divImageCollapsable").show();
                    }
                    break;
                case 22:
                    $.each(msg.d, function (index, item) {
                        ItemMangement.FillDownlodableItemForm(msg);
                    });
                    break;
                case 23:
                    $.each(msg.d, function (index, item) {
                        ItemMangement.FillItemAttributes(itemId, item);
                        if (index == 0) {
                            currencyCodeEdit = item.CurrencyCode;
                            $('.sfcurrencyList option[value=' + item.CurrencyCode + ']').prop('selected', 'selected');
                            $('.sfcurrencyListGroup option[value=' + item.CurrencyCode + ']').prop('selected', 'selected');
                            $('#ddlTax').val(item.TaxItemClass);
                        }

                    });
                    break;
                case 24:
                    ItemMangement.FillMultiSelect(msg);
                    break;
                case 25:
                    ItemMangement.vars.isUnique = msg.d;
                    break;
                case 26:
                    ItemMangement.vars.isUnique = msg.d;
                    ItemMangement.FillMultiSelect(msg);
                    break;
                case 27:

                    var id = msg.d;
                    if (itemTypeId == 3) {
                        ItemMangement.GiftCard.SaveItemCategory(id);
                    }
                    if ($("#ItemMgt_itemID").val() == 0) {
                        showPopup = false;
                        if ($("#dvCvForm>table").attr('style')) {                           
                            if (!($("#dvCvForm>table").css('display') == 'none')) {
                                var variantsProperties = $("#dvCvForm table:first>tbody>tr>td.cssClassTableCostVariant").find("input.cssClassDisplayOrder,input.cssClassVariantValueName, .ddlCostVariantsCollection, .ddlCostVariantValues");
                                var isEmpty = false;
                                $.each(variantsProperties, function (index, item) {
                                    if ($.trim($(this).val()) == 0) {
                                        isEmpty = true;
                                        return;
                                    }
                                });
                                if (!isEmpty) {
                                    ItemMangement.SaveItemCostVariantsInfo(id, aspxCommonObj());
                                }
                            }
                        }
                        if ($("#tblQuantityDiscount").parents('div.sfFormwrapper:eq(0)').attr('style')) {
                            if (!$("#tblQuantityDiscount").parents('div.sfFormwrapper:eq(0)').attr('style').contains("none")) {
                                ItemMangement.SaveItemDiscountQuantity(id);
                            }
                        }
                    }

                    ItemMangement.ClearSearchFields();
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        var outOfStockQuantity = $("#txtOutOfStockQuantity").val();
                        var itemQuantity = $(".cssClassItemQuantity").val();
                        if (allowRealTimeNotifications.toLowerCase() == 'true') {
                            try {
                                var itemOnCart = $.connection._aspxrthub;
                                if ($("#cbUseStoreSetting").prop("checked") == false) {
                                    if (itemQuantity > outOfStockQuantity) {

                                        itemOnCart.server.updateItemStockByAdmin(dynamicItemId, dynamicItemSKU, '', aspxCommonObj());
                                    }
                                }
                                else {

                                    itemOnCart.server.updateItemStockByAdmin(dynamicItemId, dynamicItemSKU, '', aspxCommonObj());
                                }
                            }
                            catch (Exception) {
                                console.log('<p>' + getLocale(AspxItemsManagement, 'Error Connecting Hub.') + '</p>');
                            }
                        }
                    }

                    $("#dynItemForm").html('');
                    $("#gdvItems_form").hide();
                    $("#divItemMgrTitle").hide();
                    $("#gdvItems_accordin").hide();
                    ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                    $("#gdvItems_grid").show();
                    if (itemEditFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item has been updated successfully.") + '</p>');
                        ItemMangement.GetEmail();
                    }
                    else {
                        csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Item has been saved successfully.") + '</p>');
                    }
                    ItemMangement.RemoveHtml();
                    gridData = [];
                    break;
                case 28:
                    ItemMangement.BindAllBrandForItem(msg);
                    break;
                case 30:
                    if (msg.d.length > 0)
                        $("#lstBrands").val(msg.d[0].BrandID);
                    else
                        $("#lstBrands").val('0');
                    break;
                case 31:
                    var receiverEmailIds = '';
                    var varinatIds = '';
                    var variantValues = '';
                    var length = msg.d.length;
                    if (length > 0) {
                        var value;
                        for (var index = 0; index < length; index++) {
                            value = msg.d[index];
                            receiverEmailIds += value.Email + ',';
                            varinatIds += value.VariantID + ',';
                            variantValues += value.VariantValue + '@';
                        };
                        ItemMangement.SendEmailToUser(varinatIds, variantValues, receiverEmailIds);

                    }
                    break;
                case 32:
                    csscody.info('<h2>' + getLocale(AspxItemsManagement, "Successful Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Email successfully Send.") + '</p>');
                case 33:
                    if (msg.d.length > 0)
                        $("#txtVideoID").val(msg.d[0].ItemVideoIDs);
                    else
                        $("#txtVideoID").val('');
                    break;
                case 34:
                    var length = msg.d.length;
                    if (length > 0) {
                        $('.popbox').show();
                        $("div.classPriceHistory").html('');
                        html = '<table class=classPriceHistoryList><thead><th>' + getLocale(AspxItemsManagement, "Price") + '</th><th>' + getLocale(AspxItemsManagement, "Date") + '</th><th>' + getLocale(AspxItemsManagement, "User Name") + '</th></thead><tbody>';
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            html += '<tr><td><span>' + parseFloat(item.ConvertedPrice.toFixed(2)) + '</span></td><td><span>' + item.Date + '</span></td><td>' + item.AddedBy + '<span></span></td></tr>';
                        };
                        html += '</tbody></table>';
                        $("div.classPriceHistory").append(html);
                    } else {
                        $("div.popbox").hide();
                    }

                    break;
                case 44:
                    csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Information') + "</h2><p>" + getLocale(AspxItemsManagement, 'Item discount quantity has been deleted successfully') + "</p>");
                    $("#tblQuantityDiscount").parent('div.sfFormwrapper').hide();
                    $("#dvAddNewQuantityDiscount").show();
                    break;
                case 45:
                    relCheckedItemID = msg.d;
                    if (relCheckedItemID != null) {
                        if (relCheckedItemID != "") {
                            var catArr = [];
                            catArr = relCheckedItemID.split(',');
                            var index = SageData.getIndex("gdvRelatedItems");
                            for (var i = 0; i < catArr.length; i++) {
                                SageData.pushArr(index, catArr[i]);
                            }
                        }
                    }
                    break;
                case 46:
                    upCheckedItemID = msg.d;
                    if (upCheckedItemID != null) {
                        if (upCheckedItemID != "") {
                            var catArr = [];
                            catArr = upCheckedItemID.split(',');
                            var index = SageData.getIndex("gdvUpSellItems");
                            for (var i = 0; i < catArr.length; i++) {
                                SageData.pushArr(index, catArr[i]);
                            }
                        }
                    }
                    break;
                case 47:
                    crossCheckedItemID = msg.d;
                    if (crossCheckedItemID != null) {
                        if (crossCheckedItemID != "") {
                            var catArr = [];
                            catArr = crossCheckedItemID.split(',');
                            var index = SageData.getIndex("gdvCrossSellItems");
                            for (var i = 0; i < catArr.length; i++) {
                                SageData.pushArr(index, catArr[i]);
                            }
                        }
                    }
                    break;

                case 48:
                    var catCheckedItemID = msg.d;
                    if (catCheckedItemID != null) {
                        if (catCheckedItemID != "") {
                            var catArr = [];
                            catArr = catCheckedItemID.split(',');
                            var index = SageData.getIndex("gdvAssociatedItems");
                            for (var i = 0; i < catArr.length; i++) {
                                SageData.pushArr(index, catArr[i]);
                            }
                        }
                    }
                    break;
                case 49:
                    ItemMangement.GetItemTabSettings();
                    csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Information') + "</h2><p>" + getLocale(AspxItemsManagement, 'Setting Saved Successfully') + "</p>");
                    $(".cssClassItemSetting").hide();
                    $("#divItemButtonWrapper").show();
                    $(".sfGridwrapper").show();
                    break
                case 50:
                    p.EnableCostVariantOption = msg.d.EnableCostVariantOption;
                    p.EnableCrossSellItem = msg.d.EnableCostVariantOption;
                    p.EnableGroupPrice = msg.d.EnableGroupPrice;
                    p.EnableRelatedItem = msg.d.EnableRelatedItem;
                    p.EnableCrossSellItem = msg.d.EnableCrossSellItem;
                    p.EnableUpSellItem = msg.d.EnableUpSellItem;
                    break;
                case 51:
                    ItemMangement.vars.IsItemInGroupItem = msg.d;
                    break;
            }
        }
    };
    ItemMangement.init();
});
