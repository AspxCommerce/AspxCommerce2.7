var TaxRules = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var taxRuleFlag = 0;
    var TaxRuleID = 0;
    TaxRules = {
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
            TaxRules.HideAll();
            $("#divTaxManageRulesGrid").show();
            TaxRules.LoadTaxRuleMgmtStaticImage();
            TaxRules.BindTaxManageRules(null, null, null, null, null, null);
                       TaxRules.BindCustomerRoleList();
            TaxRules.BindItemTaxClass();
            TaxRules.BindTaxRates();
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

            });
            $("#btnAddNewTaxRule").click(function() {
                TaxRules.ClearForm();
                TaxRules.HideAll();
                $("#divTaxRuleInformation").show();
                $("#hdnTaxManageRuleID").val(0);
                TaxRuleID = 0;
                $('#errDisplayOrder').hide();
            });

            $("#btnDeleteSelected").click(function() {
                var taxManageRule_Id = '';
                taxManageRule_Id = SageData.Get("gdvTaxRulesDetails").Arr.join(',');
                if (taxManageRule_Id.length>0) {
                    var properties = {
                        onComplete: function(e) {
                            TaxRules.ConfirmDeleteTaxRules(taxManageRule_Id, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete the selected tax rule(s)?') + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please select at least one tax rule before delete.') + '</p>');
                }
            });

            $("#txtPriority").DigitOnly('.priority', '#errmsgPriority');
            $("#txtDisplayOrder").DigitOnly('.displayOrder', '#errmsgDisplayOrder');

            $("#txtDisplayOrder").blur(function() {
                TaxRules.CheckUniqueness($("#txtDisplayOrder").val(), TaxRuleID);
                $("#errDisplayOrder").show();
            });

            var trm = $("#form1").validate({
                ignore: ':hidden',
                rules: {
                    ruleName: "required",
                    priority: "required",
                    displayOrder: "required"
                },
                messages: {
                    ruleName: "* (at least 2 chars)",
                    priority: "*",
                    displayOrder: "*"
                }
            });

            $("#btnSaveTaxRule").click(function() {
                if ($('#ddlCustomerRoleClass option:selected').val() == 0) {
                    $('#ddlCustomerRoleClass').prop('class', 'sfListmenu error');
                    return false;
                } else if ($('#ddlItemTaxClass option:selected').val() == 0) {
                    $('#ddlItemTaxClass').prop('class', 'sfListmenu error');
                    return false;
                } else if ($('#ddlTaxRate option:selected').val() == 0) {
                    $('#ddlTaxRate').prop('class', 'sfListmenu error');
                    return false;
                } else if (trm.form()) {
                    TaxRules.SaveAndUpdateTaxRules($("#txtDisplayOrder").val(), TaxRuleID);
                    return false;
                } else {
                    return false;
                }
            });

            $("#btnCancel").click(function() {
                TaxRules.HideAll();
                $("#ddlCustomerRoleClass option").removeAttr('selected');
                $("#divTaxManageRulesGrid").show();
            });
            $("#btnExportToCSV").click(function() {
                $('#gdvTaxRulesDetails').table2CSV();
            });
            $('#txtRuleName,#ddlCustomerRoleName,#ddlItemClassName,#ddlTaxRateTitle,#txtSearchPriority,#txtSearchDisplayOrder').keyup(function(event) {
                if (event.keyCode == 13) {
                    TaxRules.SearchTaxManageRules();
                }
            });
        },
        LoadTaxRuleMgmtStaticImage: function() {
            $('#ajaxTaxRuleMgmtImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ajaxCall: function(config) {
            $.ajax({
                type: TaxRules.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: TaxRules.config.contentType,
                cache: TaxRules.config.cache,
                async: TaxRules.config.async,
                data: TaxRules.config.data,
                dataType: TaxRules.config.dataType,
                url: TaxRules.config.url,
                success: TaxRules.ajaxSuccess,
                error: TaxRules.ajaxFailure
            });
        },
        HideAll: function() {
            $("#divTaxManageRulesGrid").hide();
            $("#divTaxRuleInformation").hide();
        },

        ClearForm: function() {
            $("#" + lblTaxRuleHeading).html(getLocale(AspxTaxManagement, "New Tax Rule Information"));
            $("#txtTaxManageRuleName").val('');
            $("#ddlCustomerRoleClass").val(0);
            $("#ddlItemTaxClass").val(0);
            $("#ddlTaxRate").val(0);
            $("#txtPriority").val('');
            $("#txtDisplayOrder").val('');
            TaxRules.ClearErrorLabel();
        },
        CheckUniqueness: function(value, id) {
            this.config.url = this.config.baseURL + "CheckTaxUniqueness";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, value: value, taxRuleID: id });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },
        ClearErrorLabel: function() {
            $('#txtTaxManageRuleName').removeClass('error');
            $('#txtTaxManageRuleName').parents('td').find('label').remove();
            $('#txtPriority').removeClass('error');
            $('#txtPriority').parents('td').find('label').remove();
            $('#txtDisplayOrder').removeClass('error');
            $('#txtDisplayOrder').parents('td').find('label').remove();

            $('#ddlCustomerRoleClass').removeClass('error');
            $('#ddlItemTaxClass').removeClass('error');
            $('#ddlTaxRate').removeClass('error');
        },
        BindTaxManageRules: function(ruleNm, roleName, itemClassNm, taxRateTitle, searchPriority, searchDisplayOrder) {
            var taxRuleDataObj = {
                TaxManageRuleName: ruleNm,
                RoleName: roleName,
                TaxItemClassName: itemClassNm,
                TaxRateTitle: taxRateTitle,
                Priority: searchPriority,
                DisplayOrder: searchDisplayOrder
            };
            this.config.method = "GetTaxRules";
            this.config.data = { taxRuleDataObj: taxRuleDataObj, aspxCommonObj: aspxCommonObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvTaxRulesDetails_pagesize").length > 0) ? $("#gdvTaxRulesDetails_pagesize :selected").text() : 10;

            $("#gdvTaxRulesDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxTaxManagement, 'TaxManageRule_ID'), name: 'taxManageRule_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxRuleChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxTaxManagement, 'Tax Rule Name'), name: 'taxManageRuleName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Customer Roles'), name: 'roleName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Item Tax Class Name'), name: 'taxItemClassName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Tax Rate Name'), name: 'taxRateTitle', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Priority'), name: 'priority', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Display Order'), name: 'displayOrder', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTaxManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxTaxManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'TaxRules.EditTaxRule', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxTaxManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'TaxRules.DeleteTaxRule', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxTaxManagement, "No Records Found!"),
                param: data,                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },
        EditTaxRule: function(tblID, argus) {
            switch (tblID) {
            case "gdvTaxRulesDetails":
                TaxRules.ClearErrorLabel();
                TaxRuleID = argus[0];
                $("#hdnTaxManageRuleID").val(argus[0]);
                $("#txtTaxManageRuleName").val(argus[3]);
                $("#" + lblTaxRuleHeading).html(getLocale(AspxTaxManagement, "Edit Tax Rule:") + "'" + argus[3] + "'");
                var str = argus[4];
                var array = str.split(',');
                $.each(array, function(index, value) {
                    $("#ddlCustomerRoleClass option").each(function() {
                        if ($(this).text() == array[index]) {
                            $(this).prop("selected", "selected");
                        }
                    });
                });
                $("#ddlItemTaxClass option").each(function() {
                    if ($(this).text() == argus[5]) {
                        $(this).prop("selected", "selected");
                    }
                });
                $("#ddlTaxRate option").each(function() {
                    if ($(this).text() == argus[6]) {
                        $(this).prop("selected", "selected");
                    }
                });
                $("#txtPriority").val(argus[7]);
                $("#txtDisplayOrder").val(argus[8]);
                TaxRules.HideAll();
                $("#divTaxRuleInformation").show();
                break;
            default:
                break;
            }
        },
        DeleteTaxRule: function(tblID, argus) {
            switch (tblID) {
            case "gdvTaxRulesDetails":
                var properties = {
                    onComplete: function(e) {
                        TaxRules.DeleteTaxRuleByID(argus[0], e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete this tax rule?') + "</p>", properties);
                break;
            default:
                break;
            }
        },
        ConfirmDeleteTaxRules: function(Ids, event) {
            TaxRules.DeleteTaxRuleByID(Ids, event);
        },
        DeleteTaxRuleByID: function(_taxRule_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteTaxManageRules";
                this.config.data = JSON2.stringify({ taxManageRuleIDs: _taxRule_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },
        BindCustomerRoleList: function() {
            this.config.url = this.config.baseURL + "GetAllRoles";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindItemTaxClass: function() {
            this.config.url = this.config.baseURL + "GetItemTaxClass";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        BindTaxRates: function() {
            this.config.url = this.config.baseURL + "GetTaxRate";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        SaveAndUpdateTaxRules: function (value, id) {
            this.config.url = this.config.baseURL + "CheckTaxUniqueness";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, value: value, taxRuleID: id });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(data) {
            switch (TaxRules.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rule has been deleted successfully.') + '</p>');
                TaxRules.BindTaxManageRules(null, null, null, null, null, null);
                break;
            case 2:
                if (taxRuleFlag > 0) {
                    csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rule has been updated successfully.') + '</p>');
                } else {
                    csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Tax rule has been saved successfully.') + '</p>');
                }
                TaxRules.BindTaxManageRules(null, null, null, null, null, null);
                TaxRules.HideAll();
                $("#divTaxManageRulesGrid").show();
                break;
            case 3:
                $.each(data.d, function (index, item) {
                    $("#ddlCustomerRoleClass").append("<option value=" + item.RoleID + ">" + item.RoleName + "</option>");
                    $("#ddlCustomerRoleName").append("<option value=" + item.RoleID + ">" + item.RoleName + "</option>");
                });
                break;
            case 4:
                $.each(data.d, function(index, item) {
                    $("#ddlItemTaxClass").append("<option value=" + item.TaxItemClassID + ">" + item.TaxItemClassName + "</option>");
                    $("#ddlItemClassName").append("<option value=" + item.TaxItemClassID + ">" + item.TaxItemClassName + "</option>");
                });
                break;
            case 5:
                $.each(data.d, function(index, item) {
                    $("#ddlTaxRate").append("<option value=" + item.TaxRateID + ">" + item.TaxRateTitle + "</option>");
                    $("#ddlTaxRateTitle").append("<option value=" + item.TaxRateID + ">" + item.TaxRateTitle + "</option>");
                });
            case 6:
                if (data.d == 0) {
                    $("#errDisplayOrder").html("Already Exist").css("color", "red");
                    $("#txtDisplayOrder").val('');
                } else {
                    $("#errDisplayOrder").html('');
                }
                break;
            case 7:
                if (data.d == 0) {
                    $("#errDisplayOrder").html("Already Exist").css("color", "red");
                    $("#txtDisplayOrder").val('');
                } else {
                    var taxManageRuleId = $("#hdnTaxManageRuleID").val();
                    taxRuleFlag = taxManageRuleId;
                    var taxManageRuleName = $.trim($("#txtTaxManageRuleName").val());
                    var str1 = '';
                    var str2 = '';
                    var strRoleID = '';
                    var strRoleName = '';
                    $("#ddlCustomerRoleClass option:selected").each(function () {
                        str1 += $(this).val() + ',';
                        strRoleID = str1.substr(0, str1.length - 1)
                    });
                    var roleID = strRoleID;
                    $("#ddlCustomerRoleClass option:selected").each(function () {
                        str2 += $(this).text() + ',';
                        strRoleName = str2.substr(0, str2.length - 1)
                    });
                    var roleName = strRoleName;
                    var TaxItemClassId = $("#ddlItemTaxClass").val();
                    var TaxRateId = $("#ddlTaxRate").val();
                    var Priority = $.trim($("#txtPriority").val());
                    var DisplayOrder = $.trim($("#txtDisplayOrder").val());
                    TaxRules.config.url = TaxRules.config.baseURL + "SaveAndUpdateTaxRule";
                    var taxRuleDataObj = {
                        TaxManageRuleID: taxManageRuleId,
                        TaxManageRuleName: taxManageRuleName,
                        RoleID: roleID,
                        RoleName: roleName,
                        TaxItemClassID: TaxItemClassId,
                        TaxRateID: TaxRateId,
                        Priority: Priority,
                        DisplayOrder: DisplayOrder
                    };
                    var aspxTempCommonObj = aspxCommonObj;
                    aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                    TaxRules.config.data = JSON2.stringify({ taxRuleDataObj: taxRuleDataObj, aspxCommonObj: aspxTempCommonObj });
                    TaxRules.config.ajaxCallMode = 2;
                    TaxRules.ajaxCall(this.config);
                    $("#errDisplayOrder").html('');
                }
            }
        },
        ajaxFailure: function(data) {
            switch (TaxRules.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to delete tax rule!') + '</p>');
                break;
            case 2:
                csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + "</h2><p>" + getLocale(AspxTaxManagement, 'Failed to save tax rate!') + '</p>');
                break;
            }
        },
        SearchTaxManageRules: function() {
            var ruleNm = $.trim($("#txtRuleName").val());
            var roleName = '';
            var itemClassNm = '';
            var taxRateTitle = '';
            var searchPriority = $.trim($("#txtSearchPriority").val());
            var searchDisplayOrder = $.trim($("#txtSearchDisplayOrder").val());
            if (ruleNm.length < 1) {
                ruleNm = null;
            }
            if (searchPriority.length < 1) {
                searchPriority = null;
            }

            if (searchDisplayOrder.length < 1) {
                searchDisplayOrder = null;
            }

            if ($("#ddlCustomerRoleName").val() != 0) {
                               var str1 = '';
                var str = '';
                $("#ddlCustomerRoleName option:selected").each(function() {
                    str1 += $(this).text() + ',';
                    str = str1.substr(0, str1.length - 1)
                });
                roleName = str;
            } else {
                roleName = null;
            }
            if ($("#ddlItemClassName").val() != 0) {
                itemClassNm = $.trim($("#ddlItemClassName").val());
            } else {
                itemClassNm = null;
            }
            if ($("#ddlTaxRateTitle").val() != 0) {
                taxRateTitle = $.trim($("#ddlTaxRateTitle").val());
            } else {
                taxRateTitle = null;
            }
            TaxRules.BindTaxManageRules(ruleNm, roleName, itemClassNm, taxRateTitle, searchPriority, searchDisplayOrder);
        }
    };
    TaxRules.init();

    $("#ddlCustomerRoleClass option").click(function () {
        if ($(this).val() == "0") {
            $(this).removeAttr("selected");
        }
    });

    
});