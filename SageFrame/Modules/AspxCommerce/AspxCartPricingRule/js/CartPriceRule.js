var cartPriceRuleFormat = '';
var dot = false;
var bakCount = 0;
var count = 0;
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
    var pricingRuleTemplate = new Array();
    var clickonce = 0;
    var cartPriceEditFlag = 0;
    var isUnique = false;
    (function ($) {
        $.fn.numeric = function (options) {
            return this.each(function () {
                var $this = $(this);
                $this.keypress(options, function (e) {
                    if ($this.val() == '') {
                        $this.prop('maxlength', 11);
                    }
                    if (e.which == 8 || e.which == 0) {
                        if (dot == true) {
                            count--;
                        }
                        if (count == -1) {
                            $this.prop('maxlength', 11);
                        }
                        if (dot == true && bakCount >= count) {
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
                        $this.prop('maxlength', z[0].length + 3);
                    } else {
                        $this.prop('maxlength', 11);
                    }
                    if (dot == true) {
                        if (count < 2) {
                            count++;
                        }
                        bakCount = count;
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

    cartPriceRuleFormat = {
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
                type: cartPriceRuleFormat.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: cartPriceRuleFormat.config.contentType,
                cache: cartPriceRuleFormat.config.cache,
                async: cartPriceRuleFormat.config.async,
                url: cartPriceRuleFormat.config.url,
                data: cartPriceRuleFormat.config.data,
                dataType: cartPriceRuleFormat.config.dataType,
                success: cartPriceRuleFormat.ajaxSuccess,
                error: cartPriceRuleFormat.ajaxFailure
            });
        },
        HideShowPrincingRulePanel: function (showGrid, showTabMenu) {
            if (showGrid) {
                $('#cartPricingRuleGrid').show();
            } else {
                $('#cartPricingRuleGrid').hide();
            }
            if (showTabMenu) {
                $('#cartPricingRuleTabMenu').show();
            } else {
                $('#cartPricingRuleTabMenu').hide();
            }
        },

        SetTabActive: function (index, tabContainerID) {
            var $tabs = $("#" + tabContainerID).tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', index);
        },

        bindfocusout: function () {
            $("#CartPriceRule-txtValue").focusout(function () {
                if ($("#CartPriceRule-cboApply option:selected").val() == 1) {
                    if ($("#CartPriceRule-txtValue").val() >= 100) {
                        $("#CartPriceRule-txtValue").val('');
                        $("#percError").show();
                        $("#percError").html('').html(getLocale(AspxCartPricingRule, "must be lower than 100")).fadeOut(5000);
                    }
                }
            });
        },

        ResetCartPricingRule: function () {
            $('#CartPriceRule-txtDiscountQuantity').val(0);
            $('#CartPriceRule-txtDiscountStep').val(0);
            $('#CartPriceRule-chkApplytoShippingAmount').removeAttr("checked");
            $('#CartPriceRule-cboFreeShipping').val(0);
            $('div.cssClassFieldSetContent > ul > li').not('li:last').remove();
            $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val(0);
            $('#CartPriceRule-txtName').val('');
            $('#CartPriceRule-txtDescription').val('');
            $('#CartPriceRule-txtFromDate').val('');
            $('#CartPriceRule-txtToDate').val('');
            $('#CartPriceRule-txtPriority').val('');
            $('#CartPriceRule-chkIsActive').prop("checked", "checked");
            $('#CartPriceRule-cboApply').val(1);
            $('#CartPriceRule-txtValue').val('');
            $('#CartPriceRule-chkFurtherRuleProcessing').removeAttr("checked");
            $('#CartPriceRule-mulRoles').find('option').each(function () {
                $(this).removeAttr("selected");
            });
            $('label.error').html('');
            $('.error').removeClass("error");
            $('#created').html('');
            $('.to').parents('td').find('input').prop("style", '');

            cartPriceRuleFormat.SetTabActive(0, "CartPriceRule-TabContainer");

            if ($("#CartPriceRule-cboApply option:selected").val() == 1) {
                $('#CartPriceRule-txtValue').unbind();
                $('#CartPriceRule-txtValue').numeric({ max: 100 });
                $('#CartPriceRule-txtValue').prop("maxlength", "5");
                $('#CartPriceRule-txtValue').bind('select', function () {
                    $(this).val('');
                });
                cartPriceRuleFormat.bindfocusout();
            } else {
                $('#CartPriceRule-txtValue').unbind();
                $('#CartPriceRule-txtValue').prop("maxlength", "8");
                $('#CartPriceRule-txtValue').numeric({ max: 99999999 });
                cartPriceRuleFormat.bindfocusout();
            }
            $('#CartPriceRule-txtPriority').removeClass('valid').removeClass('error');
            $("#spanPriority").html("");
        },
        JSONDateToString: function (jsonDate, dateFormat) {
            if (jsonDate) {
                var dateStr = 'new ' + jsonDate.replace(/[/]/gi, '');
                var date = eval(dateStr);
                return formatDate(date, dateFormat);
            } else {
                return jsonDate;
            }
        },
        CancelCartPricingRule: function () {
            cartPriceRuleFormat.HideShowPrincingRulePanel(true, false);
            $('#CartPriceRule-txtPriority').removeClass('valid').removeClass('error');
            $("#spanPriority").html("");
            $('#created').html('');
            $('.to').parents('td').find('input').prop("style", '');
            $('label.error').html('');
            $('.error').removeClass("error");
        },

        AddCartPricingRule: function () {
            cartPriceRuleFormat.ResetCartPricingRule();
            $("#btnResetCartPricingRule").show();
            cartPriceRuleFormat.HideShowPrincingRulePanel(false, true);
        },

        GetCartPricingRules: function (ruleNm, startDt, endDt, isAct) {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCartPricingRules";

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCartPricingRules_pagesize").length > 0) ? $("#gdvCartPricingRules_pagesize :selected").text() : 10;

            $("#gdvCartPricingRules").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCartPricingRule, "Cart Pricing Rule ID"), cssclass: 'cssClassHeadCheckBox', name: 'CartPriceRuleID', controlclass: '', coltype: 'checkbox', align: 'center', elemClass: 'attrCartPricingChkbox', elemDefault: false, controlclass: 'cartPricingHeaderChkbox' },
                    { display: getLocale(AspxCartPricingRule, "Rule Name"), name: 'CartPriceRuleName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCartPricingRule, "From"), name: 'FromDate', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCartPricingRule, "To"), name: 'ToDate', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCartPricingRule, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', type: 'boolean', format: 'True/False', align: 'left' },
                    { display: getLocale(AspxCartPricingRule, "Priority"), name: 'priority', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCartPricingRule, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCartPricingRule, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'cartPriceRuleFormat.EditCartPricingRule', arguments: '0' },
                    { display: getLocale(AspxCartPricingRule, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'cartPriceRuleFormat.DeleteCartPricngRule', arguments: '0' }
                ],
                txtClass: 'sfInputbox',
                rp: perpage,
                nomsg: getLocale(AspxCartPricingRule, "No Records Found!"),
                param: { ruleName: ruleNm, startDate: startDt, endDate: endDt, isActive: isAct, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false } }
            });
        },
        GetRoles: function () {
            this.config.url = this.config.baseURL + "GetAllRoles";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        getShippingMethods: function () {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.url = this.config.baseURL + "GetShippingMethods";
            this.config.data = JSON2.stringify({ isActive: true, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },

        GetCountry: function () {
            this.config.url = this.config.baseURL + "BindCountryList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
        },

        GetState: function () {
            this.config.url = this.config.baseURL + "GetShippingMethods";
            this.config.data = "{}";
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
        },

        BindTreeViewChild: function (CategoryID, CategoryName, ParentID, CategoryLevel, deepLevel) {
            deepLevel = deepLevel + 1;
            var hasChild = false;
            var html = '';
            $.each(CategoryList, function (index, item) {
                if (item.CategoryLevel == CategoryLevel) {
                    if (item.ParentID == ParentID) {
                        html += '<li class="category_' + item.CategoryID + '" style="padding-left:' + item.CategoryLevel * 10 + 'px;">' + item.CategoryName + '</li>';
                        htmlChild = cartPriceRuleFormat.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                        if (htmlChild != "") {
                            html += htmlChild;
                        }
                    }
                }
            });
            return html;
        },

        Edit: function (obj) {
            $(obj).closest('.cssClassFieldSetLabel').val($(obj).val());
            $(obj).parent('SPAN').addClass("cssClassOnClickEdit");
            $(obj).next('span').find('SELECT').val($(obj).prop('title'));
            $(obj).next('span').find('SELECT').focus();
            $(obj).parent().find('a.cssClassOnClickApply').html('<span class="cssClassRightGreen"></span>');
        },

        GetDropdownValue: function (self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            $(self).siblings('input').val($(self).val());
            $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html($(self).val());
        },

        GetDropdownText: function (self) {

            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var selectedText = $.trim($(self).find("option:selected").text());
            $(self).siblings('input').val($(self).find("option:selected").val());
            $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(selectedText);

            $(self).parent().parent('SPAN').find('a.cssClassFieldSetLabel').prop('title', $(self).val());

            if ($(self).prop('title') != "" && $(self).prop('title') != "operator" && $(self).prop('title') != "aggregator" && $(self).prop('title') != "value" && $(self).prop('title') != "type" && $(self).prop('title') != "attribute") {
                $(self).parents('li:eq(0)').next('li').find('span a:eq(0)').html($(self).parents('li').next('li:eq(0)').find('span select:first option:selected').text());
            }
        },
        GetCategoryValue: function (self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var selectedCategories = '';
            $(self).siblings('div.pricingRuleCategoryList').find('ul').each(function (i, item) {
                $(this).find('li').each(function (j, li) {
                    if ($(this).hasClass("selected")) {
                        var cat_id = $(this).prop("class").replace(/[^0-9]/gi, '');
                        selectedCategories += ' ' + cat_id + ',';
                    }
                });
            });
            if (selectedCategories.length > 0) {
                selectedCategories = selectedCategories.substring(0, selectedCategories.length - 1);
            }
            $(self).parent().parent().find("a.cssClassFieldSetLabel").prop('title', selectedCategories);
        },

        Delete: function (self) {
            $(self).closest('li').remove();
        },
        GetTextBoxValue: function (self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var val = $(self).parent().parent('SPAN').find("input.input-text").val();
            val = $.trim(val);
            $(self).parent().parent('SPAN').find("input.input-text").val(val);

            if (val != null && val.length > 0) {
                $(self).siblings('input').val(val);
                $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(val);
            }
            cartPriceRuleFormat.ValidateConditionFields($(self));
        },
        ValidateConditionFields: function (obj) {
            var inputVal = $(obj).parent().parent('SPAN').find("input.input-text").val();
            if (parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 8 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 13 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 15 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 5 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 46 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 47 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 48) {
                var numericReg = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
                if (!numericReg.test(inputVal)) {
                    $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();
                    $(obj).parent().parent('SPAN').find("input.input-text").after('<span class="cssValidationError" style="color:Red;">' + getLocale(AspxCartPricingRule, "No Records Found!") + '<span>');
                    $(obj).parent().parent('SPAN').find("input.input-text").val('');
                    $(obj).parent().parent('SPAN').addClass("cssClassOnClickEdit");
                    return false;
                } else {
                    $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();
                }
            }
        },
        GetMultipleValue: function (self) {
            var multiSelectObject = $(self);
            var selectedValue = '';
            var selectedText = '';
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            for (var i = 0; i < self.options.length; i++) {
                if (self.options[i].selected == true) {
                    selectedValue += ' ' + self.options[i].value + ',';
                    selectedText += ' ' + self.options[i].text + ',';
                }
            }
            if (selectedValue.length > 0) {
                selectedValue = selectedValue.substring(0, selectedValue.length - 1);
                selectedText = selectedText.substring(0, selectedText.length - 1);
            }
            $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(selectedText);
            $(self).siblings('input').val(selectedValue);
        },

        isObject: function (x) {
            switch (typeof x) {
                case "function":
                    return false;
                case "object":
                    if (x != null)
                        return true;
                    else
                        return false;
                    break;
                default:
                    return false;
            }
        },

        isFunction: function (x) {
            switch (typeof x) {
                case "function":
                    return true;
                case "object":
                    if ("function" !== typeof x.toString)
                        return (x + "").match(/function/) !== null;
                    else
                        return Object.prototype.toString.call(x) === "[object Function]";
                    break;
                default:
                    return false;
            }
        },

        ConditionSelected: function (self) {
            var priority = $(self).closest('ul').find('>li').length;
            var path = $(self).prop("title");
            var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: ($(self).prop("title") * 1 + 1), AttributeID: $(self).val(), value: "", valueText: "..." }];
            if ($(self).val() == "0") {
                $("#CartPricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                $(self).closest('li').parent().find(".MultipleSelectBox_CartPricingRule").multipleSelectBox(
                    {
                        onSelectEnd: function (e, resultList) {
                            $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                            $(this).parent().siblings('input').val(resultList.join(", "));
                        }
                    });
                cartPriceRuleFormat.Delete(self);

            }
            if ($(self).val() == "41") {

                $("#CartPricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
            }
            if ($(self).val() == "42") {
                $("#CartPricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
            }
            if ($(self).val() == "43") {
                $("#CartPricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
            }
            else if ($(self).val() == 'CC') {
                $("#CartPricingRuleTemplate_master").render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
            } else if ($(self).val() == 0) {
                $("#CartPricingRuleTemplate_master").render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
                $('#.cssPAC li').find('span.cssClassOnClickNewChild').each(function () {
                    $(this).find('select').html($('#.cssPAC li').children().last().find('select').html());
                });

                $('#.cssPS li').find('span.cssClassOnClickNewChild').each(function () {
                    $(this).find('select').html($('#.cssPS li').children().last().find('select').html());
                });
            } else if ($(self).val() == 'PAC') {
                $("#CartPricingRuleTemplate_master_PAC").render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.Delete(self);
            } else if ($(self).val() == 'PS') {

                $("#CartPricingRuleTemplate_master_PS").render(ruleInfo).appendTo($(self).closest('li').parent()).find('select').find('option[value="3"],option[value="4"],option[value="5"],option[value="6"],option[value="7"],option[value="8"],option[value="9"],option[value="10"]').hide();
                $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());

                cartPriceRuleFormat.Delete(self);
            } else if ($(self).val() > 0 && $(self).val() != "0" && $(self).val() != "41" && $(self).val() != "42" && $(self).val() != "43") {
                $("#CartPricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                cartPriceRuleFormat.GetDropdownText(self);
                $(self).closest('li').parent().find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });
                cartPriceRuleFormat.Delete(self);
            } else {
                $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            }
        },

        InitializeCartPricingRuleConditions: function () {
            var treeHTML = '';
            $.ajax({
                type: "POST",
                url: aspxservicePath + "AspxCoreHandler.ashx/GetCategoryAll",
                data: JSON2.stringify({ isActive: true, aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    CategoryList = data.d;
                    treeHTML += '<ul class="MultipleSelectBox_CartPricingRule">';
                    var deepLevel = 0;
                    $.each(CategoryList, function (i, item) {
                        if (item.CategoryLevel == 0) {
                            treeHTML += '<li class="category_' + item.CategoryID + '" style="padding-left:' + item.CategoryLevel * 10 + 'px;">' + item.CategoryName + '</li>';
                            htmlChild = cartPriceRuleFormat.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                            if (htmlChild != "") {
                                treeHTML += htmlChild;
                            }
                            treeHTML += "";

                        }
                    });
                    treeHTML += '</ul>';

                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCoreHandler.ashx/GetCartPricingRuleAttributes",
                        data: JSON2.stringify({ aspxCommonObj: aspxCommonObj() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var template = '';
                            var plusButtonTemplateMaster = '';
                            var plusButtonTemplateChild = '';
                            var masterConditionTemplate = '';
                            var masterProductAttributeConditionTemplate = '';
                            var masterProductSubselectionConditionTemplate = '';
                            var Flag = 0;

                            plusButtonTemplateMaster = '<li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"><span class="sfBtn icon-addnew">Add Condition</span></a><span class="cssClassElement"><select title="{{= Level }}" class="element-value-changer select" onblur="cartPriceRuleFormat.ConditionSelected(this)"><option value="-1" selected="selected">' + getLocale(AspxCartPricingRule, "Please choose a condition to add...") + '</option><option value="PAC">' + getLocale(AspxCartPricingRule, "Item attribute combination..") + '</option><option value="PS">' + getLocale(AspxCartPricingRule, "Items subselection..") + '</option><option value="CC">' + getLocale(AspxCartPricingRule, "Condition combination..") + '</option><optgroup label="Cart Attribute">';
                            masterConditionTemplate = '<li><input type="hidden" name="{{= ChildRulePath }}" title="type" value="combination" /> If <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)">' + getLocale(AspxCartPricingRule, "ALL") + '</a><span class="cssClassElement"><select name="{{= ChildRulePath }}" title="aggregator" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="ALL" selected="selected">ALL</option><option value="ANY">' + getLocale(AspxCartPricingRule, "ANY") + '</option></select></span></span>&nbsp; ' + getLocale(AspxCartPricingRule, "of these conditions are ") + '<span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)">TRUE</a><span class="cssClassElement"><select name="value_{{= ChildRulePath }}" title="value" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="TRUE" selected="selected">TRUE</option><option value="FALSE">FALSE</option></select></span></span>&nbsp;: <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span><ul class="cssClassOnClickChildren" id=""><li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"><span class="sfBtn icon-addnew">Add Condition</span></a><span class="cssClassElement"><select title="{{= ChildRulePath }}" class="element-value-changer select" onblur="cartPriceRuleFormat.ConditionSelected(this)"><option value="-1" selected="selected">Please choose a condition to add...</option><option value="PAC">Item attribute combination..</option><option value="PS">Items subselection..</option><option value="CC">' + getLocale(AspxCartPricingRule, "Condition combination..") + '</option><optgroup label="Cart Attribute">';

                            plusButtonTemplateChild = '<li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"><span class="sfBtn icon-addnew">Add Condition</span></a><span class="cssClassElement"><select title="{{= Level }}" class="element-value-changer select" onblur="cartPriceRuleFormat.ConditionSelected(this)"><option value="-1" selected="selected">Please choose a condition to add...</option><option value="0">Condition combination..</option><optgroup label="Product Attribute">';
                            masterProductAttributeConditionTemplate = '<li><input type="hidden" name="{{= ChildRulePath }}" title="type" value="combination" />' + getLocale(AspxCartPricingRule, "IF an Item is ") + '<span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)">' + getLocale(AspxCartPricingRule, "Found") + '</a><span class="cssClassElement"><select name="{{= ChildRulePath }}" title="aggregator" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="Found" selected="selected">Found</option><option value="Not Found">Not Found</option></select></span></span>&nbsp; in the cart with <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)">ALL</a><span class="cssClassElement"><select name="value_{{= ChildRulePath }}" title="value" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="ALL" selected="selected">ALL</option><option value="ANY">ANY</option></select></span></span>&nbsp; of these conditions true: <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span><ul class="cssClassOnClickChildren cssPAC" id=""><li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"><span class="sfBtn icon-addnew">Add Condition</span></a><span class="cssClassElement"><select title="{{= ChildRulePath }}" class="element-value-changer select" onblur="cartPriceRuleFormat.ConditionSelected(this)"><option value="-1" selected="selected">Please choose a condition to add...</option><option value="0">Condition combination..</option><optgroup label="Product Attribute">';
                            masterProductSubselectionConditionTemplate = '<li><input type="hidden" name="{{= ChildRulePath }}" title="type" value="combination" /> IF <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> total quantity </a><span class="cssClassElement"><select name="{{= ChildRulePath }}" title="aggregator" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="TQ" selected="selected">total quantity</option><option value="TA"> total amount </option></select></span></span> &nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select name="{{= ChildRulePath }}" title="aggregator" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="1" selected="selected"> Is </option><option value="2"> Is Not </option><option value="3"> Equals or Greater Than </option><option value="4"> Equals or Less Than </option><option value="5"> Greater Than </option><option value="6"> Less Than </option><option value="9"> Is One Of </option><option value="10"> Is Not One Of </option></select> </span> </span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; for a sub selection of items in cart matching <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)">ALL</a><span class="cssClassElement"><select name="value_{{= ChildRulePath }}" title="value" class=" element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)"><option value="ALL" selected="selected">ALL</option><option value="ANY">ANY</option></select></span></span>&nbsp; of these conditions: <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span><ul class="cssClassOnClickChildren cssPS" id=""><li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"><span class="sfBtn icon-addnew">Add Condition</span></a><span class="cssClassElement"><select title="{{= ChildRulePath }}" class="element-value-changer select" onblur="cartPriceRuleFormat.ConditionSelected(this)"><option value="-1" selected="selected">Please choose a condition to add...</option><option value="0">Condition combination..</option><optgroup label="Product Attribute">';

                            plusButtonTemplateMaster += '<option value="41"> SubTotal </option>';
                            masterConditionTemplate += '<option value="41"> SubTotal </option>';
                            template = '<li> <input type="hidden" name="type_{{= RulePath }}" value="Attribute" title="type" /><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="41"/> SubTotal <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                            template += '<option value="1"> Is </option><option value="2"> Is Not </option><option value="3"> Equals or Greater Than </option><option value="4"> Equals or Less Than </option><option value="5"> Greater Than </option><option value="6"> Less Than </option><option value="9"> Is One Of </option><option value="10"> Is Not One Of </option>';
                            template += '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>';
                            template = '<script id="CartPricingRuleTemplate_41" type="text/html">' + template + '<\/script>';
                            $('#placeholder-templates').append(template);

                            plusButtonTemplateMaster += '<option value="42"> Total Items Quantity </option>';
                            masterConditionTemplate += '<option value="42"> Total Items Quantity </option>';
                            template = '<li> <input type="hidden" name="type_{{= RulePath }}" value="Attribute" title="type" /><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="42"/> Total Items Quantity <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                            template += '<option value="1"> Is </option><option value="2"> Is Not </option><option value="3"> Equals or Greater Than </option><option value="4"> Equals or Less Than </option><option value="5"> Greater Than </option><option value="6"> Less Than </option>';
                            template += '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>';
                            template = '<script id="CartPricingRuleTemplate_42" type="text/html">' + template + '<\/script>';
                            $('#placeholder-templates').append(template);

                            plusButtonTemplateMaster += '<option value="43"> Total Weight </option>';
                            masterConditionTemplate += '<option value="43"> Total Weight </option>';
                            template = '<li> <input type="hidden" name="type_{{= RulePath }}" value="Attribute" title="type" /><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="43"/> Total Weight <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                            template += '<option value="1"> Is </option><option value="2"> Is Not </option><option value="3"> Equals or Greater Than </option><option value="4"> Equals or Less Than </option><option value="5"> Greater Than </option><option value="6"> Less Than </option><option value="9"> Is One Of </option><option value="10"> Is Not One Of </option>';
                            template += '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>';
                            template = '<script id="CartPricingRuleTemplate_43" type="text/html">' + template + '<\/script>';
                            $('#placeholder-templates').append(template);






                            plusButtonTemplateChild += '<option value="0"> Category </option>';
                            masterProductAttributeConditionTemplate += '<option value="0"> Category </option>';
                            masterProductSubselectionConditionTemplate += '<option value="0"> Category </option>';

                            template = '<li> <input type="hidden" name="type_{{= RulePath }}" value="Attribute" title="type" /><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="0"/> Category <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                            template += '<option value="1"> Is </option><option value="2"> Is Not </option><option value="9"> Is One Of </option><option value="10"> Is Not One Of </option>';
                            template += '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement">';
                            template += '<div class="pricingRuleCategoryList">' + treeHTML + '</div><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetCategoryValue(this)"><span class="cssClassRightGreen"></span></a></span></span><span class="cssClassOnClick"><a href="javascript:void(0)" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                            template = '<script id="CartPricingRuleTemplate_0" type="text/html">' + template + '<\/script>';
                            $('#placeholder-templates').append(template);


                            $.each(response.d, function (i, item) {
                                var options = '';
                                var operators = '';
                                var template = '';

                                if (item.AttributeID == 46) {
                                    masterProductAttributeConditionTemplate += '<optgroup label="Cart Item Attributes">';
                                    masterProductSubselectionConditionTemplate += '<optgroup label="Cart Item Attributes">';
                                    plusButtonTemplateChild += '<optgroup label="Cart Item Attributes">';
                                    Flag = 1;
                                }

                                masterProductAttributeConditionTemplate += '<option value="' + item.AttributeID + '"> ' + item.AttributeNameAlias + ' </option>';
                                masterProductSubselectionConditionTemplate += '<option value="' + item.AttributeID + '"> ' + item.AttributeNameAlias + ' </option>';
                                plusButtonTemplateChild += '<option value="' + item.AttributeID + '"> ' + item.AttributeNameAlias + ' </option>';

                                if (item.InputTypeID == 1) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            if (item.AttributeID != 47 && item.AttributeID != 15) {
                                                if (i < 2 || i > 5) {
                                                    var val = oprts[i];
                                                    if (i == 0) {
                                                        operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                                    } else {
                                                        operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                                    }
                                                }
                                            } else {
                                                var val = oprts[i];
                                                if (i == 0) {
                                                    operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                                } else {
                                                    operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                                }
                                            }
                                        }
                                    }
                                    template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                    $('#placeholder-templates').append(template);
                                } else if (item.InputTypeID == 2) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            var val = oprts[i];
                                            if (i == 0) {
                                                operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                            } else {
                                                operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                    }
                                    template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                    $('#placeholder-templates').append(template);
                                } else if (item.InputTypeID == 7) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            var val = oprts[i];
                                            if (i == 0) {
                                                operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                            } else {
                                                operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                    }
                                    template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text sfInputbox" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                    $('#placeholder-templates').append(template);
                                } else if (item.InputTypeID == 3) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            var val = oprts[i];
                                            operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                        }
                                    }
                                    var d = new Date();
                                    template += operators + '</select></span></span>&nbsp;<span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> ' + d.getFullYear() + '/' + (d.getMonth() * 1 + 1) + '/' + d.getDate() + ' </a><span class="cssClassElement"><input class="element-value-changer input-text datepicker" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="cartPriceRuleFormat.GetTextBoxValue(this)"><span class="cssClassRightGreen"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';

                                    $('#placeholder-templates').append(template);
                                } else if (item.InputTypeID == 4 || item.InputTypeID == 6 || item.InputTypeID == 9 || item.InputTypeID == 10) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" value="{{= AttributeID}}" title="attribute" />' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';

                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            var val = oprts[i];
                                            if (i == 0) {
                                                operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                            } else {
                                                operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                    }
                                    template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><select class="element-value-changer select" onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)">';

                                    var opts = eval(item.Values);
                                    if (opts != undefined && opts.length > 0) {
                                        for (var i = 0; i < opts.length; i++) {
                                            var val = opts[i];
                                            options += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                        }
                                    }
                                    template += options + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                    $('#placeholder-templates').append(template);
                                } else if (item.InputTypeID == 5 || item.InputTypeID == 11 || item.InputTypeID == 12) {
                                    template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" value="{{= AttributeID}}" title="attribute" />' + item.AttributeNameAlias + ' <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="cartPriceRuleFormat.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="cartPriceRuleFormat.GetDropdownText(this)" onchange="cartPriceRuleFormat.GetDropdownText(this)" title="operator">';
                                    var oprts = eval(item.Operators)
                                    if (oprts != undefined && oprts.length > 0) {
                                        for (var i = 0; i < oprts.length; i++) {
                                            var val = oprts[i];
                                            if (i == 0) {
                                                operators += '<option value="' + val.value + '" selected="selected"> ' + val.text + ' </option>';
                                            } else {
                                                operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                    }
                                    template += operators + '</select></span></span> &nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="cartPriceRuleFormat.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input type="hidden" class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><select class="element-value-changer select" multiple="multiple"   onblur="GetMultipleValue(this)">';
                                    var opts = eval(item.Values);
                                    if (opts != undefined && opts.length > 0) {
                                        for (var i = 0; i < opts.length; i++) {
                                            var val = opts[i];
                                            options += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                        }
                                    }
                                    template += options + '</select><a href="#" class="cssClassOnClickApply"></span></span><span class="cssClassOnClick"><a href="javascript:void(0)" class="cssClassOnClickRemove" title="Remove" onclick="cartPriceRuleFormat.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                    template = '<script id="CartPricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                    $('#placeholder-templates').append(template);
                                }

                            });

                            var ruleInfo = [{ Level: 0, RulePath: 0, value: "", valueText: "..." }];
                            if (Flag == 1) {
                                masterProductSubselectionConditionTemplate += '</optgroup></optgroup></select></span></span></li></ul></li>';
                                masterProductSubselectionConditionTemplate = '<script id="CartPricingRuleTemplate_master_PS" type="text/html">' + masterProductSubselectionConditionTemplate + '<\/script>';
                                $('#placeholder-templates').append(masterProductSubselectionConditionTemplate);

                                masterProductAttributeConditionTemplate += '</optgroup></optgroup></select></span></span></li></ul></li>';
                                masterProductAttributeConditionTemplate = '<script id="CartPricingRuleTemplate_master_PAC" type="text/html">' + masterProductAttributeConditionTemplate + '<\/script>';
                                $('#placeholder-templates').append(masterProductAttributeConditionTemplate);

                                plusButtonTemplateChild += '</optgroup></optgroup></select></span></span>&nbsp;</li>';
                                plusButtonTemplateChild = '<script id="CartPricingRuleTemplate_plus_Child" type="text/html">' + plusButtonTemplateChild + '<\/script>';
                                $('#placeholder-templates').append(plusButtonTemplateChild);
                                $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo('.cssClassOnClickChildren');
                                Flag = 0;
                            } else {
                                masterProductSubselectionConditionTemplate += '</optgroup></select></span></span></li></ul></li>';
                                masterProductSubselectionConditionTemplate = '<script id="CartPricingRuleTemplate_master_PS" type="text/html">' + masterProductSubselectionConditionTemplate + '<\/script>';
                                $('#placeholder-templates').append(masterProductSubselectionConditionTemplate);

                                masterProductAttributeConditionTemplate += '</optgroup></select></span></span></li></ul></li>';
                                masterProductAttributeConditionTemplate = '<script id="CartPricingRuleTemplate_master_PAC" type="text/html">' + masterProductAttributeConditionTemplate + '<\/script>';
                                $('#placeholder-templates').append(masterProductAttributeConditionTemplate);

                                plusButtonTemplateChild += '</optgroup></select></span></span>&nbsp;</li>';
                                plusButtonTemplateChild = '<script id="CartPricingRuleTemplate_plus_Child" type="text/html">' + plusButtonTemplateChild + '<\/script>';
                                $('#placeholder-templates').append(plusButtonTemplateChild);
                                $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo('.cssClassOnClickChildren');
                            }

                            masterConditionTemplate += '</optgroup></select></span></span></li></ul></li>';
                            masterConditionTemplate = '<script id="CartPricingRuleTemplate_master" type="text/html">' + masterConditionTemplate + '<\/script>';
                            $('#placeholder-templates').append(masterConditionTemplate);

                            plusButtonTemplateMaster += '</optgroup></select></span></span>&nbsp;</li>';
                            plusButtonTemplateMaster = '<script id="CartPricingRuleTemplate_plus_Master" type="text/html">' + plusButtonTemplateMaster + '<\/script>';
                            $('#placeholder-templates').append(plusButtonTemplateMaster);
                            $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo('.cssClassOnClickChildren');

                            $('div.cssClassFieldSetContent > ul > li').not('li:last').remove();
                        }
                    });

                },
                error: function (err) {
                    csscody.error('<h2>Error Message</h2><p>' + JSON2.stringify(err) + '</p>');
                }
            });
            return treeHTML;
        },

        SaveCartPricingRule: function () {
            var isValid = true;
            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
            cartPriceEditFlag = pricingRuleID;
            var txtName = $('#CartPriceRule-txtName').val();
            var txtDescription = $('#CartPriceRule-txtDescription').val();
            var arrCartPriceRuleRoles = new Array();
            $('#CartPriceRule-mulRoles option:selected').each(function (i, option) {
                arrCartPriceRuleRoles[i] = { CartPriceRuleID: pricingRuleID, RoleID: $(option).val() };
            });


            var txtFromDate = $('#CartPriceRule-txtFromDate').val();
            var txtToDate = $('#CartPriceRule-txtToDate').val();


            var txtPriority = $('#CartPriceRule-txtPriority').val() * 1;
            var chkIsActive = $('#CartPriceRule-chkIsActive').prop('checked') ? true : false;
            var ddlApply = $('#CartPriceRule-cboApply').val() * 1;
            var txtApplyValue = $('#CartPriceRule-txtValue').val().replace(/[^0-9.]/gi, '');
            var txtDiscountQuantity = $('#CartPriceRule-txtDiscountQuantity').val().replace(/[^0-9.]/gi, '');
            var txtDiscountStep = $('#CartPriceRule-txtDiscountStep').val().replace(/[^0-9.]/gi, '');
            var chkApplytoShippingAmount = $('#CartPriceRule-chkApplytoShippingAmount').prop('checked') ? true : false;
            var cboFreeShipping = $('#CartPriceRule-cboFreeShipping').val().replace(/[^0-9.]/gi, '');
            var chkIsFurtherProcess = $('#CartPriceRule-chkFurtherRuleProcessing').prop('checked') ? true : false;

            if (!(txtDiscountQuantity != null && txtDiscountQuantity.length != 0)) {
                txtDiscountQuantity = 0;
            }
            if (!(txtDiscountStep != null && txtDiscountStep.length != 0)) {
                txtDiscountStep = 0;
            }

            var isAll = $('div.cssClassFieldSetContent > span:nth-child(1) > span > select.element-value-changer').parent().parent().find('a.cssClassFieldSetLabel').text();
            if (String(isAll).toUpperCase() == "ALL") {
                isAll = true;
            } else {
                isAll = false;
            }
            var isTrue = $('div.cssClassFieldSetContent > span:nth-child(3) > span > select.element-value-changer').parent().parent().find('a.cssClassFieldSetLabel').text();

            if (String(isTrue).toUpperCase() == "TRUE") {
                isTrue = true;
            } else {
                isTrue = false;
            }
            var PR = {
                CartPriceRule: {
                    Apply: ddlApply,
                    CartPriceRuleDescription: txtDescription,
                    CartPriceRuleID: pricingRuleID,
                    CartPriceRuleName: txtName,
                    DiscountQuantity: txtDiscountQuantity,
                    DiscountStep: txtDiscountStep,
                    ApplytoShippingAmount: chkApplytoShippingAmount,
                    FreeShipping: cboFreeShipping,
                    IsFurtherProcessing: chkIsFurtherProcess,
                    Priority: txtPriority,
                    FromDate: txtFromDate,
                    ToDate: txtToDate,
                    Value: txtApplyValue,
                    IsActive: chkIsActive
                }
            };

            var arrCartPriceRuleCondition = new Array();
            var arrCartConditionDetail = new Array();
            var arrRuleCondition = new Array();

            arrCartPriceRuleCondition[0] = { IsAll: isAll, IsTrue: isTrue, LstCartConditionDetails: arrCartConditionDetail };
            arrRuleCondition[0] = { RuleConditionType: "CC", LstCartPriceRuleConditions: arrCartPriceRuleCondition };


            $.each($('div.cssClassFieldSetContent > ul > li'), function (i, listItem) {

                var type = $(listItem).find('input[title="type"]').val();
                if (String(type).toLowerCase() == 'attribute') {
                    var att_op = '';
                    var att_val = '';
                    var att_id = $(listItem).find('input[title="attribute"]').val() * 1;

                    if ($(listItem).find('a.cssClassFieldSetLabel').prop('title').length > 0)
                        att_op = $(listItem).find('a.cssClassFieldSetLabel').prop('title');
                    else
                        att_op = $(listItem).find('> span > span > select[title="operator"]').val() * 1;

                    if ($(listItem).find('> span > span > input[title="value"]').length > 0) {
                        att_val = $(listItem).find('> span > span > input[title="value"]').val();
                        if (att_val == "" && $(listItem).find('> span:eq(1)>span>input').hasClass('hasDatepicker')) {
                            att_val = $(listItem).find('> span:eq(1) > a').text();
                        }
                    } else if ($(listItem).find('> span:eq(1) > a').prop('title').length > 0) {
                        att_val = $(listItem).find('> span:eq(1) > a').prop('title');
                    }
                    var name = $(listItem).find('input[title="attribute"]').prop('name');
                    var nameparts = String(name).split('_');
                    var attrs = nameparts[1].split('-');
                    var att_priority = attrs[0] * 1;
                    if ($.trim(att_val) == "") {
                        isValid = false;
                        return false;

                    }
                    arrCartConditionDetail[i] = { AttributeID: att_id, CartPriceRuleID: pricingRuleID, Priority: att_priority, RuleOperatorID: att_op, Value: $.trim(att_val) };
                } else if (String(type).toLowerCase() == 'combination') {
                    isValid = cartPriceRuleFormat.GetChildCartPricingRule(arrRuleCondition, $(listItem));
                }
            });

            var CartPrice = {
                CartPricingRuleInfo: { CartPriceRule: PR.CartPriceRule, LstRuleCondition: arrRuleCondition, LstCartPriceRuleRoles: arrCartPriceRuleRoles }
            };

            $('div.cssClassFieldSetContent').find('ul').each(function (index) {
                $(this).prop('id', index + 1);
            });
            var arrParent = new Array();
            arrParent.push(0);

            $('div.cssClassFieldSetContent').find('ul li').find('input[value="combination"]').each(function () {
                arrParent.push($(this).parent().parent('ul').prop('id'));
            });
            if (isValid) {
                this.config.url = this.config.baseURL + "SaveCartPricingRule";
                this.config.data = JSON2.stringify({ "objCartPriceRule": CartPrice.CartPricingRuleInfo, aspxCommonObj: aspxCommonObj(), "parentID": arrParent });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            } else {
                clickonce = 0;
                csscody.alert('<h2>Information Alert</h2><p>Please enter valid data!</p>');

            }
        },

        GetChildCartPricingRule: function (arrRuleCondition, parent) {
            var isValid = true;
            var pricingRuleID = $(parent).find('input[value="combination"]').prop('name').replace(/[^0-9]/gi, '') * 1;
            var arrCartConditionDetail = new Array();

            if ($(parent).find('> ul').hasClass('cssPAC')) {
                var isFound = $(parent).find('> span > span > select[title="aggregator"]').parent().parent().find('a.cssClassFieldSetLabel').text();
                var isAll = $(parent).find('> span > span > select[title="value"]').parent().parent().find('a.cssClassFieldSetLabel').text();

                var arrProductAttributeRuleConditions = new Array();
                arrProductAttributeRuleConditions[0] = {
                    IsAll: String(isAll).toUpperCase() == "ALL" ? true : false,
                    IsFound: String(isFound).toUpperCase() == "FOUND" ? true : false,
                    LstCartConditionDetails: arrCartConditionDetail
                };
                arrRuleCondition[arrRuleCondition.length] = { RuleConditionType: "PAC", LstProductAttributeRuleConditions: arrProductAttributeRuleConditions };
            } else if ($(parent).find('> ul').hasClass('cssPS')) {
                var operatorID = '';
                var isQty = $(parent).find('> span > span > select[title="aggregator"]').val();
                if ($(parent).find('> span:eq(1) a').prop('title').length > 0)
                    operatorID = $(parent).find('> span:eq(1) a').prop('title');
                else
                    operatorID = $(parent).find('> span:eq(1) select[title="aggregator"]').val() * 1;

                var value = $(parent).find('> span:eq(2) input[title="value"]').parent().parent().find('a.cssClassFieldSetLabel').text();
                var isAll = $(parent).find('> span:eq(3) select[title="value"]').parent().parent().find('a.cssClassFieldSetLabel').text();
                if ($.trim(value) == "...")
                    isValid = false;
                var arrProductSubSelectionRuleConditions = new Array();
                arrProductSubSelectionRuleConditions[0] = { IsQuantity: String(isQty).toUpperCase() == "TQ" ? true : false, IsAll: String(isAll).toUpperCase() == "ALL" ? true : false, Value: value, RuleOperatorID: operatorID, LstCartConditionDetails: arrCartConditionDetail };
                arrRuleCondition[arrRuleCondition.length] = { RuleConditionType: "PS", LstProductSublectionRuleConditions: arrProductSubSelectionRuleConditions };
            } else {
                var isAll = $(parent).find('> span > span > select[title="aggregator"]').parent().parent().find('a.cssClassFieldSetLabel').text();
                if (String(isAll).toUpperCase() == "ALL") {
                    isAll = true;
                } else {
                    isAll = false;
                }
                var isTrue = $(parent).find('> span > span > select[title="value"]').parent().parent().find('a.cssClassFieldSetLabel').text();

                if (String(isTrue).toUpperCase() == "TRUE") {
                    isTrue = true;
                } else {
                    isTrue = false;
                }

                var arrCartPriceRuleCondition = new Array();
                arrCartPriceRuleCondition[0] = { IsAll: isAll, IsTrue: isTrue, LstCartConditionDetails: arrCartConditionDetail };
                arrRuleCondition[arrRuleCondition.length] = { RuleConditionType: "CC", LstCartPriceRuleConditions: arrCartPriceRuleCondition };

            }

            $.each($(parent).find('> ul > li'), function (i, childListItem) {
                var type = $(childListItem).find('input[title="type"]').val();
                if (String(type).toLowerCase() == 'attribute') {
                    var att_op = '';
                    var att_val = '';
                    var att_id = $(childListItem).find('input[title="attribute"]').val() * 1;

                    if ($(childListItem).find('a.cssClassFieldSetLabel').prop('title').length > 0)
                        att_op = $(childListItem).find('a.cssClassFieldSetLabel').prop('title');
                    else
                        att_op = $(childListItem).find('> span > span > select[title="operator"]').val() * 1;

                    if ($(childListItem).find('> span > span > input[title="value"]').length > 0) {
                        att_val = $(childListItem).find('> span > span > input[title="value"]').val();
                        if (att_val == "" && $(childListItem).find('> span:eq(1)>span>input').hasClass('hasDatepicker')) {
                            att_val = $(childListItem).find('> span:eq(1) > a').text();
                        }
                    } else if ($(childListItem).find('> span:eq(1) > a').prop('title').length > 0) {
                        att_val = $(childListItem).find('> span:eq(1) > a').prop('title');
                    }
                    var name = $(childListItem).find('input[title="attribute"]').prop('name');
                    var nameparts = String(name).split('_');
                    var attrs = nameparts[1].split('-');
                    var att_priority = attrs[0] * 1;

                    arrCartConditionDetail[i] = { AttributeID: att_id, CartPriceRuleID: pricingRuleID, Priority: att_priority, RuleOperatorID: att_op, Value: $.trim(att_val) };

                    if ($.trim(att_val) == "") {
                        isValid = false;
                        return false;
                    }
                } else if (String(type).toLowerCase() == 'combination') {
                    isValid = cartPriceRuleFormat.GetChildCartPricingRule(arrRuleCondition, $(childListItem))
                }
            });
            return isValid;
        },

        EditCartPricingRule: function (tblID, argus) {
            switch (tblID) {
                case "gdvCartPricingRules":
                    $("#btnResetCartPricingRule").hide();
                    cartPriceRuleFormat.GetCartPricingRuleByCartPricingRuleID(argus[3]);
                    break;
                default:
                    break;
            }
        },

        DeleteCartPricngRule: function (tblID, argus) {
            switch (tblID) {
                case "gdvCartPricingRules":
                    if (argus[3]) {
                        var properties = { onComplete: function (e) { cartPriceRuleFormat.CartPricingRuleDelete(argus[0], e); } }
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this cart price rule?</p>", properties);
                    }
                    break;
                default:
                    break;
            }
        },

        CartPricingRuleDelete: function (priceRuleID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteCartPricingRule";
                this.config.data = JSON2.stringify({ cartPricingRuleID: priceRuleID, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            return false;
        },
        BindCartPriceRuleByID: function (response) {
            $('div.cssClassFieldSetContent > ul > li').not('li:last').remove();
            var dsData = eval(response.d);
            var cartPriceRule = dsData.CartPriceRule;
            var arrRuleCondition = dsData.LstRuleCondition;
            var arrCartPriceRuleRole = dsData.LstCartPriceRuleRoles;
            $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val(cartPriceRule.CartPriceRuleID)
            $('#CartPriceRule-txtName').val(cartPriceRule.CartPriceRuleName);
            $('#CartPriceRule-txtDescription').val(cartPriceRule.CartPriceRuleDescription);
            $('#CartPriceRule-txtFromDate').val(cartPriceRuleFormat.JSONDateToString(cartPriceRule.FromDate, "yyyy/MM/dd"));
            $('#CartPriceRule-txtToDate').val(cartPriceRuleFormat.JSONDateToString(cartPriceRule.ToDate, "yyyy/MM/dd"));
            $('#CartPriceRule-txtPriority').val(cartPriceRule.Priority);
            $('#CartPriceRule-chkIsActive').prop("checked", cartPriceRule.IsActive);
            $('#CartPriceRule-cboApply').val(cartPriceRule.Apply);
            $('#CartPriceRule-txtValue').val(cartPriceRule.Value);
            $('#CartPriceRule-chkFurtherRuleProcessing').prop("checked", cartPriceRule.IsFurtherProcessing);
            $('#CartPriceRule-mulRoles').find('option').each(function () {
                $(this).removeAttr("selected");
            });
            $('#CartPriceRule-txtDiscountQuantity').val(cartPriceRule.DiscountQuantity)
            $('#CartPriceRule-txtDiscountStep').val(cartPriceRule.DiscountStep)
            $('#CartPriceRule-chkApplytoShippingAmount').prop("checked", cartPriceRule.ApplytoShippingAmount);
            $('#CartPriceRule-cboFreeShipping').val(cartPriceRule.FreeShipping);
            $('#CartPriceRule-mulStores').find('option').each(function () {
                $(this).removeAttr("selected");
            });
            if ($("#CartPriceRule-cboApply").val() == 1) {
                $('#CartPriceRule-txtValue').unbind();
                $('#CartPriceRule-txtValue').numeric({ max: 100 });
                $('#CartPriceRule-txtValue').prop("maxlength", "5");
                $('#CartPriceRule-txtValue').bind('select', function () {
                    $(this).val('');
                });
                cartPriceRuleFormat.bindfocusout();
            } else {
                $('#CartPriceRule-txtValue').unbind();
                $('#CartPriceRule-txtValue').prop("maxlength", "8");
                $('#CartPriceRule-txtValue').numeric({ max: 99999999 });
                cartPriceRuleFormat.bindfocusout();
            }
            for (var r = 0; r < arrCartPriceRuleRole.length; r++) {
                $('#CartPriceRule-mulRoles').find('option').each(function () {
                    if ($(this).val() == arrCartPriceRuleRole[r].RoleID) {
                        $(this).prop("selected", "selected");
                    }
                });
            }
            var ruleCondition = arrRuleCondition[0];
            $('div.cssClassFieldSetContent > ul').prop('id', ruleCondition.ParentID);

            if (ruleCondition.ParentID == 0) {
                var cartPriceRuleCondition = arrRuleCondition[0].LstCartPriceRuleConditions[0];
                $('div.cssClassFieldSetContent > span:nth-child(1) > span > select.element-value-changer').val(cartPriceRuleCondition.IsAll ? "ALL" : "ANY");
                $('div.cssClassFieldSetContent > span:nth-child(1) > a.cssClassFieldSetLabel').text(cartPriceRuleCondition.IsAll ? "ALL" : "ANY");
                $('div.cssClassFieldSetContent > span:nth-child(2) > span > select.element-value-changer').val(cartPriceRuleCondition.IsTrue ? "TRUE" : "FALSE");
                $('div.cssClassFieldSetContent > span:nth-child(2) > a.cssClassFieldSetLabel').text(cartPriceRuleCondition.IsTrue ? "TRUE" : "FALSE");

                for (var d = 0; d < cartPriceRuleCondition.LstCartConditionDetails.length; d++) {
                    var cartConditionDetail = cartPriceRuleCondition.LstCartConditionDetails[d];
                    var attr_ID = cartConditionDetail.AttributeID;
                    var self = $('div.cssClassFieldSetContent > ul > li:last > span > span > select.element-value-changer ');
                    var priority = $(self).prop('title') * 1 + 1;
                    var path = $(self).prop('title') * 1;
                    var valueText = "...";
                    if (cartConditionDetail.Value == "") {
                        valueText = "...";
                    } else {
                        valueText = cartConditionDetail.Value;
                    }
                    var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (ruleCondition.ParentID), AttributeID: attr_ID, value: cartConditionDetail.Value, valueText: valueText }];

                    if (attr_ID == "0") {

                        $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                        $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                        $(self).closest('li').parent().find(".MultipleSelectBox_CartPricingRule").multipleSelectBox(
                            {
                                onSelectEnd: function (e, resultList) {
                                    $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                    $(this).parent().siblings('input').val(resultList.join(", "));
                                }
                            });
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                        var arrCategoryID = cartConditionDetail.Value.split(",");
                        if (arrCategoryID != "") {
                            var catName = [];
                            $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function () {
                                for (var i = 0; i < arrCategoryID.length; i++) {
                                    if ($(this).hasClass("category_" + $.trim(arrCategoryID[i]))) {
                                        catName.push($(this).html());
                                    }
                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList').parent().parent().find('a.cssClassFieldSetLabel').text(catName.join(','));
                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList').parent().parent().find('a.cssClassFieldSetLabel').prop('title', arrCategoryID);
                                }
                            });
                        } else {
                            $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList').parent().parent().find('a.cssClassFieldSetLabel').text(valueText);
                        }
                        cartPriceRuleFormat.Delete(self);
                    } else if (attr_ID > 0) {
                        $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                        $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                        var arrValues = cartConditionDetail.Value.split(",");
                        var seconLast = $(self).closest('ul').find('>li').length * 1 - 1;
                        cartPriceRuleFormat.GetDropdownText(self);
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('select.element-value-changer option').each(function (i, itm) {
                            if ($(itm).val() == $.trim(cartConditionDetail.Value)) {
                                $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $(this).val());
                            }
                        });
                        cartPriceRuleFormat.Delete(self);
                    } else {
                        $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                    }
                }
                cartPriceRuleFormat.BindChildCondition(arrRuleCondition);
            }
            cartPriceRuleFormat.SetTabActive(0, "CartPriceRule-TabContainer");
            ShowFields($("#CartPriceRule-cboApply").val());
        },
        GetCartPricingRuleByCartPricingRuleID: function (pricingRuleID) {
            this.config.url = this.config.baseURL + "GetCartPricingRule";
            this.config.data = JSON2.stringify({ cartPriceRuleID: pricingRuleID, aspxCommonObj: aspxCommonObj() }),
            this.config.ajaxCallMode = 10;
            this.ajaxCall(this.config);
            cartPriceRuleFormat.HideShowPrincingRulePanel(false, true);
        },

        BindChildCondition: function (arrRuleCondition) {
            var parentself = '';
            var self = '';
            for (var c = 1; c < arrRuleCondition.length; c++) {
                var ruleCondition = arrRuleCondition[c];
                if (jQuery.trim(ruleCondition.RuleConditionType.toUpperCase()) == "CC" && ruleCondition.ParentID != 0) {


                    parentself = $('div.cssClassFieldSetContent > ul select[title="' + (c - 1) + '"]');
                    $('div.cssClassFieldSetContent ul').each(function (i, item) {
                        if ($(item).prop('id') == ruleCondition.ParentID) {
                            parentself = $(item).children('li:last').find('select');
                            return false;
                        }
                    });

                    var nchild = $(parentself).closest('ul').find('> li').length + 1;
                    var priority = $(parentself).prop('title') * 1 + 1;
                    var path = $(parentself).prop('title') * 1;
                    var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: c, AttributeID: 0, value: "", valueText: "..." }];

                    $("#CartPricingRuleTemplate_master").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                    $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                    $(parentself).closest('ul').prop('id', ruleCondition.ParentID);

                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(0) > a.cssClassFieldSetLabel').text(ruleCondition.LstCartPriceRuleConditions[0].IsAll ? "ALL" : "ANY");
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > a.cssClassFieldSetLabel').text(ruleCondition.LstCartPriceRuleConditions[0].IsTrue ? "TRUE" : "FALSE");
                    cartPriceRuleFormat.Delete(parentself);
                    var catIndex_1 = 0;
                    for (var d = 0; d < ruleCondition.LstCartPriceRuleConditions[0].LstCartConditionDetails.length; d++) {
                        var cartConditionDetail = ruleCondition.LstCartPriceRuleConditions[0].LstCartConditionDetails[d];
                        var attr_ID = cartConditionDetail.AttributeID;
                        self = $('div.cssClassFieldSetContent > ul select[title="' + c + '"]');
                        var priority = $(self).prop('title') * 1;
                        var path = $(self).prop('title') * 1;
                        var valueText = "...";
                        if (cartConditionDetail.Value == "") {
                            valueText = "...";
                        } else {
                            valueText = cartConditionDetail.Value;
                        }
                        var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (path * 1 + 1), AttributeID: attr_ID, value: cartConditionDetail.Value, valueText: valueText }];
                        if (attr_ID == "0") {
                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                            $(self).closest('li').parent().find(".MultipleSelectBox_CartPricingRule").multipleSelectBox(
                                {
                                    onSelectEnd: function (e, resultList) {
                                        $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                        $(this).parent().siblings('input').val(resultList.join(", "));
                                    }
                                });
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                            var arrCategoryID = cartConditionDetail.Value.split(",");
                            if (arrCategoryID != "") {
                                var catName = [];
                                for (var i = 0; i < arrCategoryID.length; i++) {

                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function () {
                                        if ($(this).hasClass("category_" + $.trim(arrCategoryID[i]))) {

                                            catName.push($(this).html());
                                            return false;
                                        }

                                    });
                                }


                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + catIndex_1 + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(catName.join(','));
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + catIndex_1 + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').prop('title', arrCategoryID);
                                catIndex_1++;
                            } else {
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + catIndex_1 + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(valueText);
                            }
                            cartPriceRuleFormat.Delete(self);
                        } else if (attr_ID > 0) {
                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(self).closest('li').parent());
                            cartPriceRuleFormat.GetDropdownText(self);
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('select.element-value-changer option').each(function (i, itm) {
                                if ($(itm).val() == $.trim(cartConditionDetail.Value)) {
                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $(this).val());
                                }
                            });
                            cartPriceRuleFormat.Delete(self);
                        } else {
                            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                        }
                    }
                }

                if (jQuery.trim(ruleCondition.RuleConditionType.toUpperCase()) == "PAC") {

                    parentself = $('div.cssClassFieldSetContent > ul select[title="' + (c - 1) + '"]');
                    $('div.cssClassFieldSetContent ul').each(function (i, item) {
                        if ($(item).prop('id') == ruleCondition.ParentID) {
                            parentself = $(item).children('li:last').find('select');
                            return false;
                        }
                    });
                    var nchild = $(parentself).closest('ul').find('> li').length + 1;
                    var priority = $(parentself).prop('title') * 1 + 1;
                    var path = $(parentself).prop('title') * 1;
                    var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: c, AttributeID: 0, value: "", valueText: "..." }];

                    $("#CartPricingRuleTemplate_master_PAC").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                    $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                    $(parentself).closest('ul').prop('id', ruleCondition.ParentID);
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > a.cssClassFieldSetLabel').text(ruleCondition.LstProductAttributeRuleConditions[0].IsAll ? "ALL" : "ANY");
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(0) > a.cssClassFieldSetLabel').text(ruleCondition.LstProductAttributeRuleConditions[0].IsFound ? "Found" : "Not Found");
                    cartPriceRuleFormat.Delete(parentself);
                    var catIndex_2 = 0;
                    for (var d = 0; d < ruleCondition.LstProductAttributeRuleConditions[0].LstCartConditionDetails.length; d++) {
                        var cartConditionDetail = ruleCondition.LstProductAttributeRuleConditions[0].LstCartConditionDetails[d];
                        var attr_ID = cartConditionDetail.AttributeID;
                        self = $('div.cssClassFieldSetContent > ul select[title="' + c + '"]');
                        var priority = $(self).prop('title') * 1;
                        var path = $(self).prop('title') * 1;
                        var valueText = "...";
                        if (cartConditionDetail.Value == "") {
                            valueText = "...";
                        } else {
                            valueText = cartConditionDetail.Value;
                        }
                        var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (path * 1 + 1), AttributeID: attr_ID, value: cartConditionDetail.Value, valueText: valueText }];
                        if (attr_ID == "0") {
                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                            $(self).closest('li').parent().find(".MultipleSelectBox_CartPricingRule").multipleSelectBox(
                                {
                                    onSelectEnd: function (e, resultList) {
                                        $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                        $(this).parent().siblings('input').val(resultList.join(", "));
                                    }
                                });
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                            var arrCategoryID = valueText.split(",");
                            var catLen = arrCategoryID.length;
                            if (arrCategoryID != "...") {

                                var catName = [];
                                for (var i = 0; i < catLen; i++) {
                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function () {

                                        if ($(this).hasClass("category_" + $.trim(arrCategoryID[i]))) {
                                            catName.push($(this).html());
                                            return false;
                                        }
                                    });
                                }
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + catIndex_2 + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(catName.join(','));
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + catIndex_2 + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').prop('title', arrCategoryID);
                                catIndex_2++;


                            } else {
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + cat_index + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(valueText);
                            }
                            cartPriceRuleFormat.Delete(self);
                        } else if (attr_ID > 0) {
                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                            cartPriceRuleFormat.GetDropdownText(self);
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);


                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('select.element-value-changer option').each(function (i, itm) {
                                if ($(itm).val() == $.trim(cartConditionDetail.Value)) {
                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $(this).val());
                                }
                            });
                            cartPriceRuleFormat.Delete(self);
                        } else {
                            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                        }
                    }
                }

                if (jQuery.trim(ruleCondition.RuleConditionType.toUpperCase()) == "PS") {
                    parentself = $('div.cssClassFieldSetContent > ul select[title="' + (c - 1) + '"]');
                    $('div.cssClassFieldSetContent ul').each(function (i, item) {
                        if ($(item).prop('id') == ruleCondition.ParentID) {
                            parentself = $(item).children('li:last').find('select');
                            return false;
                        }
                    });

                    var nchild = $(parentself).closest('ul').find('> li').length + 1;
                    var priority = $(parentself).prop('title') * 1 + 1;
                    var path = $(parentself).prop('title') * 1;
                    var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: c, AttributeID: 0, value: "", valueText: "..." }];

                    $("#CartPricingRuleTemplate_master_PS").render(ruleInfo).appendTo($(parentself).closest('li').parent()).find('select').find('option[value="3"],option[value="4"],option[value="5"],option[value="6"],option[value="7"],option[value="8"],option[value="9"],option[value="10"]').hide();
                    ;
                    $("#CartPricingRuleTemplate_plus_Master").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                    $(parentself).closest('ul').prop('id', ruleCondition.ParentID);
                    var ruleOperator = '';
                    if (ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID == 1)
                        ruleOperator = 'Is';
                    else if (ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID == 2)
                        ruleOperator = 'Is Not';
                    else if (ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID == 9)
                        ruleOperator = 'Is One Of';
                    else if (ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID == 10)
                        ruleOperator = 'Is Not One Of';

                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(0) > a.cssClassFieldSetLabel').text(ruleCondition.LstProductSublectionRuleConditions[0].IsQuantity ? "total quantity" : "total amount");
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > select[title="aggregator"]').val(ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID);
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > a.cssClassFieldSetLabel').text(ruleOperator); $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > a.cssClassFieldSetLabel').prop('title', ruleCondition.LstProductSublectionRuleConditions[0].RuleOperatorID);
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(2) input[title="value"]').val(ruleCondition.LstProductSublectionRuleConditions[0].Value);
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(2) > a.cssClassFieldSetLabel').text(ruleCondition.LstProductSublectionRuleConditions[0].Value);
                    $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(3) > a.cssClassFieldSetLabel').text(ruleCondition.LstProductSublectionRuleConditions[0].IsAll ? "ALL" : "ANY");

                    cartPriceRuleFormat.Delete(parentself);
                    var cat_index = 0;
                    for (var d = 0; d < ruleCondition.LstProductSublectionRuleConditions[0].LstCartConditionDetails.length; d++) {
                        var cartConditionDetail = ruleCondition.LstProductSublectionRuleConditions[0].LstCartConditionDetails[d];
                        var attr_ID = cartConditionDetail.AttributeID;
                        self = $('div.cssClassFieldSetContent > ul select[title="' + c + '"]');
                        var priority = $(self).prop('title') * 1;
                        var path = $(self).prop('title') * 1;
                        var valueText = "...";
                        if (cartConditionDetail.Value == "") {
                            valueText = "...";
                        } else {
                            valueText = cartConditionDetail.Value;
                        }
                        var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (path * 1 + 1), AttributeID: attr_ID, value: cartConditionDetail.Value, valueText: valueText }];
                        if (attr_ID == "0") {
                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                            $(self).closest('li').parent().find(".MultipleSelectBox_CartPricingRule").multipleSelectBox(
                                {
                                    onSelectEnd: function (e, resultList) {
                                        $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                        $(this).parent().siblings('input').val(resultList.join(", "));
                                    }
                                });
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                            var arrCategoryID = cartConditionDetail.Value.split(",");
                            if (arrCategoryID != "") {
                                var catName = [];
                                var catlen = arrCategoryID.length;

                                for (var i = 0; i < catlen; i++) {
                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function () {

                                        if ($(this).hasClass("category_" + $.trim(arrCategoryID[i]))) {
                                            catName.push($(this).html());
                                            return false;
                                        }
                                    });
                                }

                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + cat_index + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(catName.join(','));
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + cat_index + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').prop('title', arrCategoryID);
                                cat_index++;


                            } else {
                                $(self).parents('ul:eq(0)').find('div.pricingRuleCategoryList:eq(' + cat_index + ')').parents('.cssClassOnClick').find('.cssClassFieldSetLabel').text(valueText);
                            }
                            cartPriceRuleFormat.Delete(self);
                        } else if (attr_ID > 0) {

                            $("#CartPricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#CartPricingRuleTemplate_plus_Child").render(ruleInfo).appendTo($(self).closest('li').parent());
                            cartPriceRuleFormat.GetDropdownText(self);

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(cartPriceRuleFormat.setOperators(cartConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', cartConditionDetail.RuleOperatorID);

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('select.element-value-changer option').each(function (i, itm) {
                                if ($(itm).val() == $.trim(cartConditionDetail.Value)) {

                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                    $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $(this).val());
                                }
                            });
                            cartPriceRuleFormat.Delete(self);
                        } else {
                            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                        }
                    }
                }
            }
        },
        CartPricingRulesMultipleDelete: function (cartRule_ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteMultipleCartPricingRules";
                this.config.data = JSON2.stringify({ cartRulesIds: cartRule_ids, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            }
            return false;
        },
        setOperators: function (RuleOperatorID) {
            var opt = '';
            if (RuleOperatorID == 1)
                opt = 'Is';
            else if (RuleOperatorID == 2)
                opt = 'Is Not';
            else if (RuleOperatorID == 3)
                opt = 'Equals or Greater Than';
            else if (RuleOperatorID == 4)
                opt = 'Equals or Less Than';
            else if (RuleOperatorID == 5)
                opt = 'Greater Than';
            else if (RuleOperatorID == 6)
                opt = 'Less Than';
            else if (RuleOperatorID == 7)
                opt = 'Contains';
            else if (RuleOperatorID == 8)
                opt = 'Does Not Contain';
            else if (RuleOperatorID == 9)
                opt = 'Is One Of';
            else if (RuleOperatorID == 10)
                opt = 'Is Not One Of';
            return opt;
        },
        SearchCartPricingRule: function () {
            var ruleNm = $.trim($("#txtCartPriceRuleSrc").val());
            var startDt = $.trim($("#txtCartPricingRuleStartDate").val());
            var endDt = $.trim($('#txtCartPricingRuleEndDate').val());
            var isAct = $.trim($('#ddlCartPricingRuleIsActive').val()) == "" ? null : ($.trim($('#ddlCartPricingRuleIsActive').val()) == "True" ? true : false);
            if (ruleNm.length < 1) {
                ruleNm = null;
            }
            if (startDt.length < 1) {
                startDt = null;
            }
            if (endDt.length < 1) {
                endDt = null;
            }
            cartPriceRuleFormat.GetCartPricingRules(ruleNm, startDt, endDt, isAct);
        },
        /*============= End of Pricing Rules ===============================*/

        CheckCartPriorityUniqueness: function (cartPriceRuleID) {
            var portalID = aspxCommonObj().PortalID;
            var cartPriorityVal = $('#CartPriceRule-txtPriority').val();
            this.config.url = this.config.baseURL + "CheckCartPricePriorityUniqueness";
            this.config.data = JSON2.stringify({ cartPriceRuleID: cartPriceRuleID, priority: cartPriorityVal, portalID: portalID });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
            return isUnique;
        },
        ajaxSuccess: function (msg) {
            switch (cartPriceRuleFormat.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var options = '';
                    $.each(msg.d, function (i, item) {
                        options += '<option value="' + item.RoleID + '"> ' + item.RoleName + ' </option>';
                    });
                    $("#CartPriceRule-mulRoles").html(options);
                    break;
                case 3:
                    var notificationText = "";
                    switch (msg.d) {
                        case "success":
                            if (cartPriceEditFlag > 0) {
                                notificationText = getLocale(AspxCartPricingRule, "Cart pricing rule has been updated successfully.");
                            } else {
                                notificationText = getLocale(AspxCartPricingRule, "Cart pricing rule has been saved successfully.");
                            }
                            break;
                        case "notify":
                            notificationText = getLocale(AspxCartPricingRule, "More than 3 rules are not allowed in free version of AspxCommerce!");
                            break;
                    }
                    if (notificationText != "") {
                        csscody.info('<h2>' + getLocale(AspxCartPricingRule, "Successful Message") + '</h2><p>' + notificationText + '</p>');
                        arrParent = new Array();
                        cartPriceRuleFormat.GetCartPricingRules(null, null, null, null);
                        cartPriceRuleFormat.SetTabActive(0, "CartPriceRule-TabContainer");
                        cartPriceRuleFormat.HideShowPrincingRulePanel(true, false);
                    }
                    clickonce = 0;
                    break;
                case 4:
                    csscody.info('<h2>' + getLocale(AspxCartPricingRule, "Successful Message") + '</h2><p>' + getLocale(AspxCartPricingRule, "Cart price rule has been deleted successfully.") + '</p>');
                    cartPriceRuleFormat.GetCartPricingRules(null, null, null, null);
                    break;
                case 5:
                    isUnique = msg.d;
                    break;
                case 6:
                    csscody.info('<h2>' + getLocale(AspxCartPricingRule, "Successful Message") + '</h2><p>' + getLocale(AspxCartPricingRule, "Selected cart price rule(s) has been deleted successfully.") + '</p>');
                    cartPriceRuleFormat.GetCartPricingRules(null, null, null, null);
                    break;
                case 7:
                    $.each(msg.d, function (i, item) {
                        shippingMethods += '<option value=' + item.ShippingMethodID + '>' + item.ShippingMethodName + '</option>';
                    });
                    break;
                case 8:
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            shippingCountry += '<option value=' + item.Value + '> ' + item.Text + '</option>';
                        };
                    }
                    break;
                case 9:
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            shippingState += '<option value=' + item.Value + '> ' + item.Text + '</option>';
                        };
                    }
                    break;
                case 10:
                    cartPriceRuleFormat.BindCartPriceRuleByID(msg);

                    break;
            }
        },
        ajaxFailure: function (msg) {
            switch (cartPriceRuleFormat.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to load Roles !") + '</p>');
                    break;
                case 2:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to load Stores !") + '</p>');
                    break;
                case 3:
                    clickonce = 0;
                    break;
                case 4:
                    csscody.error('<h2>Error Message</h2><p>' + getLocale(AspxCartPricingRule, "Failed to Delete CartPrice Rule!!") + '</p>');
                    break;
                case 5:
                    break;
                case 6:
                    csscody.error('<h2>' + getLocale(AspxCartPricingRule, "Error Message") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to Delete selected CartPrice Rule!!") + '</p>');
                    break;
                case 7:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to Load Shipping Methods!!") + '</p>');
                    break;
                case 8:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to Load Country List!!") + '</p>');
                    break;
                case 9:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p> ' + getLocale(AspxCartPricingRule, "Failed to Load States List!!") + '</p>');
                    break;
                case 10:
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Failed to Load Details !!") + '</p>');
                    break;
            }
        },
        init: function (config) {
            cartPriceRuleFormat.GetCartPricingRules(null, null, null, null);
            cartPriceRuleFormat.GetRoles();
            cartPriceRuleFormat.InitializeCartPricingRuleConditions();
            cartPriceRuleFormat.HideShowPrincingRulePanel(true, false);
            $('#CartPriceRule-TabContainer').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });

            $("#CartPriceRule-txtFromDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#CartPriceRule-txtToDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtCartPricingRuleStartDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtCartPricingRuleEndDate").datepicker({ dateFormat: 'yy/mm/dd' });

            $("#btnAddCartPricingRule").bind("click", function () {
                cartPriceRuleFormat.AddCartPricingRule();
            });
            $("#btnResetCartPricingRule").bind("click", function () {
                cartPriceRuleFormat.ResetCartPricingRule();
            });
            $("#btnCancelCartPricingRule").bind("click", function () {
                cartPriceRuleFormat.CancelCartPricingRule();
            });
            $(".hasDatepicker").bind("contextmenu", function (e) {
                return false;
            });
            $('#txtCartPriceRuleSrc,#txtCartPricingRuleStartDate,#txtCartPricingRuleEndDate,#ddlCartPricingRuleIsActive').keyup(function (event) {
                if (event.keyCode == 13) {
                    cartPriceRuleFormat.SearchCartPricingRule();
                }
            });
            var v = $("#form1").validate({
                rules: {
                    Description: {
                        required: true
                    },
                    Priority: {
                        required: true,
                        number: true
                    },
                    Value: {
                        required: true,
                        number: true
                    },
                    MaximumQtyDiscountAppliedTo: {
                        number: true
                    },
                    DiscountQtyStep: {
                        number: true
                    }
                }, messages: {

                    FromDate: {
                        required: "*"
                    },
                    ToDate: {
                        required: "*"
                    },
                    Roles: {
                        required: "*"
                    },
                    RuleName: {
                        required: "*"
                    },
                    Description: {
                        required: "*"
                    },
                    Priority: {
                        required: "*",
                        number: "numbers only!"
                    },
                    Value: {
                        required: "*",
                        number: "numbers only!"
                    },
                    MaximumQtyDiscountAppliedTo: {
                        number: "numbers only!"
                    },
                    DiscountQtyStep: {
                        number: "numbers only!"
                    }

                },
                submitHandler: function () {

                }
            });

            $("#CartPriceRule-txtToDate").bind("change", function () {
                $('#created').html('');
                $('.to').parents('td').find('input').prop("style", '');
                $(this).removeClass('error');
                $('.to').parents('td').find('label').remove();
            });

            $("#CartPriceRule-txtFromDate").bind("change", function () {
                if ($(this).val() != "") {
                    $('#created').html('');
                    $('.to').parents('td').find('input').prop("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                $(this).removeClass('error');
                $('.from').parents('td').find('label').remove();

            });

            $("#btnSaveCartPricingRule").bind("click", function () {
                if (v.form()) {
                    if (Date.parse($('.from').val()) > Date.parse($('.to').val())) {
                        //$('.to').parents('td').find('input').css({ "background-color": "#FCC785" });
                        $('#created').html('').html('To Date Must be higher or equal to From Date');
                        cartPriceRuleFormat.SetTabActive(0, "CartPriceRule-TabContainer");
                    } else {
                        $('#created').html('');
                        $('.to').parents('td').find('input').prop("style", '');
                        if (clickonce == 0) {
                            clickonce++;
                            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
                            if (!cartPriceRuleFormat.CheckCartPriorityUniqueness(pricingRuleID)) {
                                $("#spanPriority").html(getLocale(AspxCartPricingRule, "priority already assigned."));
                                $('#CartPriceRule-txtPriority').removeClass('valid').addClass('error');
                                clickonce = 0;
                                var errorCatalogTab = $("#CartPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                                if (errorCatalogTab.length > 0) {
                                    var errorCatalogTabName = errorCatalogTab.prop('id');
                                    var $tabs = $('#CartPriceRule-TabContainer').tabs();
                                    $tabs.tabs('option', 'active', errorCatalogTabName);
                                }
                                return false;
                            } else {
                                $('#CartPriceRule-txtPriority').removeClass('valid').removeClass('error');
                                $("#spanPriority").html("");
                                cartPriceRuleFormat.SaveCartPricingRule();
                            }
                        }
                    }
                } else {
                    var errorCatalogTab = $("#CartPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                    if (errorCatalogTab.length > 0) {
                        var errorCatalogTabName = errorCatalogTab.prop('id');
                        var $tabs = $('#CartPriceRule-TabContainer').tabs();
                        $tabs.tabs('option', 'active', errorCatalogTabName);
                    }
                }
            });


            $('#CartPriceRule-txtPriority').bind("keypress", function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    $("#spanPriority").html("Digits Only").css("color", "red").show().fadeOut(1600);
                    return false;
                }
            });


            $("#CartPriceRule-txtValue").keypress(function () {
                if ($("#CartPriceRule-cboApply option:selected").val() == 1) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCartPricingRule, "must be lower than 100")).fadeOut(5000);
                }
            });


            $("#CartPriceRule-cboApply").change(function () {
                $("#CartPriceRule-txtValue").val('');
                if ($("#CartPriceRule-cboApply option:selected").val() == 1) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCartPricingRule, "must be lower than 100")).fadeOut(5000);
                    $('#CartPriceRule-txtValue').unbind();
                    $('#CartPriceRule-txtValue').numeric({ max: 100 });
                    $('#CartPriceRule-txtValue').prop("maxlength", "5");
                    $('#CartPriceRule-txtValue').bind('select', function () {
                        $(this).val('');
                    });
                    cartPriceRuleFormat.bindfocusout();
                } else {
                    $('#CartPriceRule-txtValue').unbind();
                    $('#CartPriceRule-txtValue').prop("maxlength", "8");
                    $('#CartPriceRule-txtValue').numeric({ max: 99999999 });
                    cartPriceRuleFormat.bindfocusout();
                }
            });

            $("#CartPriceRule-txtValue").change(function () {
                if ($("#CartPriceRule-cboApply option:selected").val() == 1) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCartPricingRule, "must be lower than 100")).fadeOut(5000);
                    $('#CartPriceRule-txtValue').unbind();
                    $('#CartPriceRule-txtValue').numeric({ max: 100 });
                    $('#CartPriceRule-txtValue').prop("maxlength", "5");
                    $('#CartPriceRule-txtValue').bind('select', function () {
                        $(this).val('');
                    });
                    cartPriceRuleFormat.bindfocusout();
                    if ($("#CartPriceRule-txtValue").val() >= 100) {
                        $("#CartPriceRule-txtValue").val('');
                    }
                } else {
                    $("#CartPriceRule-txtValue").prop('style', '');
                    $('#CartPriceRule-txtValue').prop("maxlength", "8");
                    $('#CartPriceRule-txtValue').numeric({ max: 99999999 });
                    cartPriceRuleFormat.bindfocusout();
                }
            });

            $('#CartPriceRule-txtDiscountQuantity').numeric({ max: 999 });
            $('#CartPriceRule-txtDiscountStep').numeric({ max: 999 });
            cartPriceRuleFormat.bindfocusout();

            $('#btnDeleteCartRules').click(function () {
                var cartRule_ids = '';
                cartRule_ids = SageData.Get("gdvCartPricingRules").Arr.join(',');
                if (cartRule_ids.length > 0) {
                    var properties = {
                        onComplete: function (e) {
                            cartPriceRuleFormat.CartPricingRulesMultipleDelete(cartRule_ids, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxCartPricingRule, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCartPricingRule, "Are you sure you want to delete the selected cart price rule(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxCartPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCartPricingRule, "Please select at least one cart price rule.") + '</p>');
                }
            });
            $("#CartPriceRule-cboApply").change(function () {
                ShowFields($(this).val());
                if (dot == true) {
                    count--;
                }
                if (count == -1) {
                    $(this).prop('maxlength', 11);
                }
                if (dot == true && bakCount >= count) {
                    dot = false;
                    bakCount = 0;
                    count = 0;
                }
                return true;
            });

            $("#CartPriceRule-chkApplytoShippingAmount").bind("click", function () {
                if ($(this).prop("checked") == "checked" || $(this).prop("checked") == "true" || $(this).prop("checked") == true)
                    $("#trApplytoShipping").show();
                else
                    $("#trApplytoShipping").hide();
            });


            $('#CartPriceRule-txtValue').on('select', function () {
                if (dot == true) {
                    count--;
                }
                if (count == -1) {
                    $(this).prop('maxlength', 11);
                }
                if (dot == true && bakCount >= count) {
                    dot = false;
                    bakCount = 0;
                    count = 0;
                }
                return true;
            });
        }
    };
    cartPriceRuleFormat.init();
});

function ShowFields(ddlval) {

    $(".tdbordernone tr").show();

    if ($("#CartPriceRule-chkApplytoShippingAmount").prop("checked") == "checked" || $("#CartPriceRule-chkApplytoShippingAmount").prop("checked") == "true" || $("#CartPriceRule-chkApplytoShippingAmount").prop("checked") == true)
        $("#trApplytoShipping").show();
    else
        $("#trApplytoShipping").hide();
    switch (parseInt(ddlval)) {
        case 1:
            $(".tdbordernone tr").show();
            $("#trBuyX,#trApplytoShipping").hide();
            $("#CartPriceRule-lblValue").html("Value:");
            if ($("#CartPriceRule-chkApplytoShippingAmount").is(":checked"))
                $("#trApplytoShipping").show();
            break;
        case 2:
            $(".tdbordernone tr").show();
            $("#trBuyX,#trApplytoShipping").hide();
            $("#CartPriceRule-lblValue").html("Value:");
            if ($("#CartPriceRule-chkApplytoShippingAmount").is(":checked"))
                $("#trApplytoShipping").show();
            break;
        case 3:
            $(".tdbordernone tr").show();
            $("#trBuyX,#trApplytoShipping").hide();
            $(".tdbordernone tr:eq(2)").hide();
            $(".tdbordernone tr:eq(3)").hide();
            $("#CartPriceRule-lblValue").html("Value:");
            if ($("#CartPriceRule-chkApplytoShippingAmount").is(":checked"))
                $("#trApplytoShipping").show();
            break;
        case 4:
            $(".tdbordernone tr").show();
            $("#trApplytoShipping").hide();
            $(".tdbordernone tr:eq(2)").hide();
            $("#CartPriceRule-lblValue").html("Value(Y):");
            if ($("#CartPriceRule-chkApplytoShippingAmount").is(":checked"))
                $("#trApplytoShipping").show();
            break;
    }
}