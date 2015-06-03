var CatalogPricingRule = "";
var catalogDot = false;
var catalogBakCount = 0;
var catalogCount = 0;

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
    var plusButtonTemplate = '';
    var masterConditionTemplate = '';
    var pricingRuleTemplate = new Array();
    var clickonce = 0;
    var catalogEditFlag = 0;   
    (function($) {
        $.fn.numeric = function(options) {
            return this.each(function() {
                var $this = $(this);
                $this.keypress(options, function(e) {
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

    CatalogPricingRule = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            data: '{}',
            dataType: "json",
            url: "",
            method: "",
            ajaxCallMode: 0
        },
        vars: {
            isUnique: false
        },
        init: function() {
            CatalogPricingRule.GetPricingRules(null, null, null, null);
            CatalogPricingRule.GetRoles();
            CatalogPricingRule.InitializePricingRuleConditions();           
            if (isCatalogThreadRunning.toLowerCase()=="true") {
                $("#btnApplyRules").prop("disabled", "disabled");
                $("#btnSaveAndApplyPricingRule").prop("disabled", "disabled");
                $(".catalogMessage").show();
            }
            else {
                $("#btnApplyRules").removeAttr("disabled");
                $("#btnSaveAndApplyPricingRule").removeAttr("disabled");
                $(".catalogMessage").hide();
            }
            $('#CatalogPriceRule-TabContainer').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            CatalogPricingRule.HideShowPrincingRulePanel(true, false);
            $("#CatalogPriceRule-txtFromDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#CatalogPriceRule-txtToDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtPricingRuleStartDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtPricingRuleEndDate").datepicker({ dateFormat: 'yy/mm/dd' });
            $(".hasDatepicker").bind("contextmenu", function(e) {
                return false;
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
                    }
                },
                messages: {
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
                    }
                },
                submitHandler: function() {

                }
            });

            $('#CatalogPriceRule-txtValue').on('select', function() {
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

            $("#CatalogPriceRule-cboApply").change(function() {
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
            $("#btnSavePricingRule").click(function() {

                if (v.form()) {
                    if (Date.parse($('.from').val()) > Date.parse($('.to').val())) {
                        // $('.to').parents('td').find('input').css({ "background-color": "#FCC785" });
                        $('#created').html('').html(getLocale(AspxCatalogPricingRule, "To Date Must be higher or equal to From Date"));
                        CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");

                    } else {
                        $('#created').html('');
                        $('.to').parents('td').find('input').prop("style", '');
                        if (clickonce == 0) {
                            clickonce++;
                            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
                            if (!CatalogPricingRule.CheckPriorityUniqueness(pricingRuleID)) {
                                $("#priority").html(getLocale(AspxCatalogPricingRule, "priority already assigned."));
                                $('#CatalogPriceRule-txtPriority').removeClass('valid').addClass('error');
                                clickonce = 0;
                                var errorCatalogTab = $("#CatalogPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                                if (errorCatalogTab.length > 0) {
                                    var errorCatalogTabName = errorCatalogTab.prop('id');
                                    var $tabs = $('#CatalogPriceRule-TabContainer').tabs();
                                    $tabs.tabs('option', 'active', errorCatalogTabName);
                                }
                                return false;
                            } else {
                                $('#CatalogPriceRule-txtPriority').removeClass('valid').removeClass('error');
                                $("#priority").html("");
                                CatalogPricingRule.SavePricingRule();
                            }
                        }
                    }
                } else {
                    var errorCatalogTab = $("#CatalogPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                    if (errorCatalogTab.length > 0) {
                        var errorCatalogTabName = errorCatalogTab.prop('id');
                        var $tabs = $('#CatalogPriceRule-TabContainer').tabs();
                        $tabs.tabs('option', 'active', errorCatalogTabName);
                    }
                }
            });
            $("#btnSaveAndApplyPricingRule").click(function() {

                if (v.form()) {
                    if (Date.parse($('.from').val()) > Date.parse($('.to').val())) {
                        // $('.to').parents('td').find('input').css({ "background-color": "#FCC785" });
                        $('#created').html('').html(getLocale(AspxCatalogPricingRule, "To Date Must be higher or equal to From Date"));
                        CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");

                    } else {
                        $('#created').html('');
                        $('.to').parents('td').find('input').prop("style", '');
                        if (clickonce == 0) {
                            clickonce++;
                            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
                            if (!CatalogPricingRule.CheckPriorityUniqueness(pricingRuleID)) {
                                $("#priority").html(getLocale(AspxCatalogPricingRule, "priority already assigned."));
                                $('#CatalogPriceRule-txtPriority').removeClass('valid').addClass('error');
                                clickonce = 0;
                                var errorCatalogTab = $("#CatalogPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                                if (errorCatalogTab.length > 0) {
                                    var errorCatalogTabName = errorCatalogTab.prop('id');
                                    var $tabs = $('#CatalogPriceRule-TabContainer').tabs();
                                    $tabs.tabs('option', 'active', errorCatalogTabName);
                                }
                                return false;
                            } else {
                                $('#CatalogPriceRule-txtPriority').removeClass('valid').removeClass('error');
                                $("#priority").html("");
                                CatalogPricingRule.SaveAndApplyPricingRule();
                            }
                        }
                    }
                } else {
                    var errorCatalogTab = $("#CatalogPriceRule-TabContainer").find('div .error').not('label').parents('div:eq(0)');
                    if (errorCatalogTab.length > 0) {
                        var errorCatalogTabName = errorCatalogTab.prop('id');
                        var $tabs = $('#CatalogPriceRule-TabContainer').tabs();
                        $tabs.tabs('option', 'active', errorCatalogTabName);
                    }
                }
            });

            $("#btnApplyRules").click(function () {
                var properties = {
                    onComplete: function (e) {                        
                        CatalogPricingRule.CheckCatalogRuleExist(e);                        
                    }
                };
                csscody.confirm('<h2>' + getLocale(AspxCatalogPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Are you sure you want to apply the rules? It may takes a long time.") + '</p>', properties);
            });

            $('#CatalogPriceRule-txtPriority').keypress(function(e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    $("#priority").html("Digits Only").css("color", "red").show().fadeOut(1600);
                    return false;
                }
            });

            $("#CatalogPriceRule-txtToDate").bind("change", function() {
                $('#created').html('');
                $('.to').parents('td').find('input').prop("style", '');
                $(this).removeClass('error');
                $('.to').parents('td').find('label').remove();

            });
            $("#CatalogPriceRule-txtFromDate").bind("change", function() {
                if ($(this).val() != "") {
                    $('#created').html('');
                    $('.to').parents('td').find('input').prop("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                $(this).removeClass('error');
                $('.from').parents('td').find('label').remove();
            });

            $('#btnDeleteCatRules').click(function() {
                var catRule_ids = '';
                catRule_ids = SageData.Get("gdvCatalogPricingRules").Arr.join(',');
                if (catRule_ids.length > 0) {
                    var properties = {
                        onComplete: function(e) {
                            CatalogPricingRule.CatPricingRulesMultipleDelete(catRule_ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxCatalogPricingRule, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCatalogPricingRule, "Are you sure you want to delete the selected catalog price rule(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxCatalogPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Please select at least one catalog price rule before delete.") + '</p>');
                }
            });

            $("#btnAddNewCatRule").click(function() {
                CatalogPricingRule.AddPricingRule();
            });
            $("#CatalogPriceRule-txtValue").keypress(function() {
                if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCatalogPricingRule, "must be lower than 100")).fadeOut(5000);
                }
            });

            $("#CatalogPriceRule-cboApply").change(function() {
                $("#CatalogPriceRule-txtValue").val('');
                if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCatalogPricingRule, "must be lower than 100")).fadeOut(5000);
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').numeric({ max: 100 });
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "5");
                    $('#CatalogPriceRule-txtValue').bind('select', function() {
                        $(this).val('');
                    });
                    CatalogPricingRule.bindfocusout();
                    if ($("#CatalogPriceRule-txtValue").val() >= 100) {
                        $("#CatalogPriceRule-txtValue").val('');
                    }
                } else {
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "8");
                    $('#CatalogPriceRule-txtValue').numeric({ max: 99999999 });
                    CatalogPricingRule.bindfocusout();
                }
            });

            $("#CatalogPriceRule-txtValue").change(function() {
                if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                    $("#percError").show();
                    $("#percError").html('').html(getLocale(AspxCatalogPricingRule, "must be lower than 100")).fadeOut(5000);
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').numeric({ max: 100 });
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "5");
                    $('#CatalogPriceRule-txtValue').bind('select', function() {
                        $(this).val('');
                    });
                    CatalogPricingRule.bindfocusout();
                    if ($("#CatalogPriceRule-txtValue").val() >= 100) {
                        $("#CatalogPriceRule-txtValue").val('');
                    }
                } else {
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "8");
                    $('#CatalogPriceRule-txtValue').numeric({ max: 99999999 });
                    CatalogPricingRule.bindfocusout();
                }
            });

            if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                $('#CatalogPriceRule-txtValue').unbind();
                $('#CatalogPriceRule-txtValue').numeric({ max: 100 });
                $('#CatalogPriceRule-txtValue').prop("maxlength", "5");
                $('#CatalogPriceRule-txtValue').bind('select', function() {
                    $(this).val('');
                });
                CatalogPricingRule.bindfocusout();
            } else {
                $('#CatalogPriceRule-txtValue').prop("maxlength", "8");
                $('#CatalogPriceRule-txtValue').numeric({ max: 99999999 });
            }
            CatalogPricingRule.bindfocusout();
            $('#txtCatalogPriceRuleSrc,#txtPricingRuleStartDate,#txtPricingRuleEndDate,#ddlPricingRuleIsActive').keyup(function(event) {
                if (event.keyCode == 13) {
                    CatalogPricingRule.SearchPricingRule();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: CatalogPricingRule.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: CatalogPricingRule.config.contentType,
                cache: CatalogPricingRule.config.cache,
                async: CatalogPricingRule.config.async,
                data: CatalogPricingRule.config.data,
                dataType: CatalogPricingRule.config.dataType,
                url: CatalogPricingRule.config.url,
                success: CatalogPricingRule.ajaxSuccess,
                error: CatalogPricingRule.ajaxFailure
            });
        },
        bindfocusout: function() {
            $("#CatalogPriceRule-txtValue").focusout(function() {
                if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                    if ($("#CatalogPriceRule-txtValue").val() >= 100) {
                        $("#CatalogPriceRule-txtValue").val('');
                        $("#percError").show();
                        $("#percError").html('').html(getLocale(AspxCatalogPricingRule, "must be lower than 100")).fadeOut(5000);
                    }
                }
            });
        },
        JSONDateToString: function(jsonDate, dateFormat) {
            if (jsonDate) {
                var dateStr = 'new ' + jsonDate.replace(/[/]/gi, '');
                var date = eval(dateStr);
                return formatDate(date, dateFormat);
            } else {
                return jsonDate;
            }
        },
        CatPricingRulesMultipleDelete: function(catRule_ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteMultipleCatPricingRules";
                this.config.data = JSON2.stringify({ catRulesIds: catRule_ids, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },

        Edit: function(obj) {
            $(obj).closest('.cssClassFieldSetLabel').val($(obj).val());
            $(obj).parent('SPAN').addClass("cssClassOnClickEdit");
            $(obj).next('span').find('SELECT').val($(obj).prop('title'));
            $(obj).next('span').find('SELECT').focus();
            $(obj).parent().find('a.cssClassOnClickApply').html('<span class="sfBtn icon-success"></span>');
        },

        GetDropdownValue: function(self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            $(self).siblings('input').val($(self).val());
            $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html($(self).val());
        },
        GetDropdownText: function(self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var selectedText = $(self).find("option:selected").text();
            $(self).siblings('input').val($(self).find("option:selected").text());
            $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(selectedText);
            $(self).parent().parent('SPAN').find('a.cssClassFieldSetLabel').prop('title', $(self).val());

            if ($(self).prop('title') != "" && $(self).prop('title') != "operator" && $(self).prop('title') != "aggregator" && $(self).prop('title') != "value" && $(self).prop('title') != "type" && $(self).prop('title') != "attribute") {
                $(self).parents('li:eq(0)').next('li').find('span a:eq(0)').html($(self).parents('li').next('li:eq(0)').find('span select:first option:selected').text());
            }
        },
        GetTextBoxValue: function(self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var val = $(self).parent().parent('SPAN').find("input.input-text").val();
            val = $.trim(val);
            $(self).parent().parent('SPAN').find("input.input-text").val(val);
            if (val != null && val.length > 0) {
                $(self).siblings('input').val(val);
                $(self).parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(val);
            }
            CatalogPricingRule.ValidateConditionFields($(self));
        },
        ValidateConditionFields: function(obj) {
            var inputVal = $(obj).parent().parent('SPAN').find("input.input-text").val();
            //               if($.trim(inputVal)==''){
            //                 $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();     
            //                 $(obj).parent().parent('SPAN').find("input.input-text").after('<span class="cssValidationError" style="color:Red;">* Please Enter Value<span>');
            //                 $(obj).parent().parent('SPAN').addClass("cssClassOnClickEdit");
            //                 return false;
            //              } 
            //              else{
            //                  $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();    
            //              }   
            if (parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 8 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 13 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 15 || parseInt($(obj).closest('li').find('input[title="attribute"]').val()) == 5) {
                var numericReg = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
                if (!numericReg.test(inputVal)) {
                    $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();
                    $(obj).parent().parent('SPAN').find("input.input-text").after('<span class="cssValidationError" style="color:Red;">* Invalid Input<span>');
                    $(obj).parent().parent('SPAN').find("input.input-text").val('');
                    $(obj).parent().parent('SPAN').addClass("cssClassOnClickEdit");
                    return false;
                } else {
                    $(obj).parent().parent('SPAN').find("span.cssValidationError").remove();
                }
            }
        },
        GetMultipleValue: function(self) {
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

        GetRoles: function() {
            this.config.url = this.config.baseURL + "GetAllRoles";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        GetCategoryValue: function(self) {
            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            var selectedCategories = '';
            $(self).siblings('div.pricingRuleCategoryList').find('ul').each(function(i, item) {
                $(this).find('li').each(function(j, li) {
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

        ConditionSelected: function(self) {
            var priority = $(self).closest('ul').find('>li').length;
            var path = $(self).prop("title");
            var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: ($(self).prop("title") * 1 + 1), AttributeID: $(self).val(), value: "", valueText: "..." }];
            if ($(self).val() == 0) {
                $("#PricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                $(self).closest('li').parent().find(".MultipleSelectBox_PricingRule").multipleSelectBox(
                {
                    onSelectEnd: function(e, resultList) {
                        $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                        $(this).parent().siblings('input').val(resultList.join(", "));
                    }
                });
                CatalogPricingRule.Delete(self);
            } else if ($(self).val() == -1) {
                CatalogPricingRule.GetDropdownValue(self);
                $("#PricingRuleTemplate_master").render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                CatalogPricingRule.Delete(self);
            } else if ($(self).val() > 0) {
                $("#PricingRuleTemplate_" + $(self).val()).render(ruleInfo).appendTo($(self).closest('li').parent());
                $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                CatalogPricingRule.GetDropdownText(self);
                $(self).closest('li').parent().find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });
                CatalogPricingRule.Delete(self);
            } else {
                $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
            }
        },
        Delete: function(self) {
            $(self).closest('li').remove();
        },
        InitializePricingRuleConditions: function() {
            var treeHTML = '';
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/" + 'GetCategoryAll',
                data: JSON2.stringify({ isActive: true, aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    CategoryList = data.d;
                    treeHTML += '<ul class="MultipleSelectBox_PricingRule">';
                    var deepLevel = 0;
                    $.each(CategoryList, function(i, item) {
                        if (item.CategoryLevel == 0) {
                            treeHTML += '<li class="category_' + item.CategoryID + '" style="padding-left:' + item.CategoryLevel * 15 + 'px;">' + item.CategoryName + '</li>';
                            htmlChild = CatalogPricingRule.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                            if (htmlChild != "") {
                                treeHTML += htmlChild;
                            }
                            treeHTML += "";
                        }
                    });
                    treeHTML += '</ul>';

                    $.ajax({
                        type: "POST", beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", umi);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        url: aspxservicePath + "AspxCoreHandler.ashx/" + 'GetPricingRuleAttributes',
                        data: JSON2.stringify({ isActive: 1, aspxCommonObj: aspxCommonObj() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function(response) {
                            var template = '';
                            masterConditionTemplate = '<li><input type="hidden" name="{{= ChildRulePath }}" title="type" value="combination" /> If <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)">ALL</a><span class="cssClassElement"><select name="{{= ChildRulePath }}" title="aggregator" class=" element-value-changer select" onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)"><option value="ALL" selected="selected">ALL</option><option value="ANY">ANY</option></select></span></span>&nbsp; of these conditions are <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)">TRUE</a><span class="cssClassElement"><select name="value_{{= ChildRulePath }}" title="value" class=" element-value-changer select" onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)"><option value="TRUE" selected="selected">TRUE</option><option value="FALSE">FALSE</option></select></span></span>&nbsp;: <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span><ul class="cssClassOnClickChildren" id=""><li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"><span class="cssClassAdd"></span></a><span class="cssClassElement"><select title="{{= ChildRulePath }}" class="element-value-changer select" onblur="CatalogPricingRule.ConditionSelected(this)"><option value="-2" selected="selected">Please choose a condition to add...</option><option value="-1">Condition combination..</option><optgroup label="Product Attribute">';
                            plusButtonTemplate = '<li>&nbsp;<span class="cssClassOnClick cssClassOnClickNewChild"><a href="#" class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"><span class="sfBtn icon-addnew">' + getLocale(AspxCatalogPricingRule, "Add Condition") + '</span></a><span class="cssClassElement"><select title="{{= Level }}" class="element-value-changer select" onblur="CatalogPricingRule.ConditionSelected(this)"><option value="-2" selected="selected">Please choose a condition to add...</option><option value="-1">Condition combination..</option><optgroup label="Product Attribute">';

                            plusButtonTemplate += '<option value="0"> Category </option>';
                            masterConditionTemplate += '<option value="0"> Category </option>';
                            template = '<li><input type="hidden" name="type_{{= RulePath }}" value="Attribute" title="type" /><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="0"/> Category <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select" onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" title="operator">';
                            template += '<option value="1"> Is </option><option value="2"> Is Not </option><option value="9"> Is One Of </option><option value="10"> Is Not One Of </option>';
                            template += '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement">';
                            template += '<div class="pricingRuleCategoryList">' + treeHTML + '</div><a href="#" class="cssClassOnClickApply" onclick="CatalogPricingRule.GetCategoryValue(this)"><span class="sfBtn icon-success"></span></a></span></span><span class="cssClassOnClick"><a href="javascript:void(0)" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>';
                            template = '<script id="PricingRuleTemplate_0" type="text/html">' + template + '<\/script>';
                            $('#placeholder-templates').append(template);
                            $.each(response.d, function(i, item) {
                                var options = '';
                                var operators = '';
                                var template = '';
                                if (item.AttributeID != 41 && item.AttributeID != 42 && item.AttributeID != 43 && item.AttributeID != 38) {
                                    plusButtonTemplate += '<option value="' + item.AttributeID + '"> ' + item.AttributeNameAlias + ' </option>';
                                    masterConditionTemplate += '<option value="' + item.AttributeID + '"> ' + item.AttributeNameAlias + ' </option>';

                                    if (item.InputTypeID == 1) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                        var oprts = eval(item.Operators);
                                        if (oprts != undefined && oprts.length > 0) {
                                            for (var i = 0; i < oprts.length; i++) {
                                                if (item.AttributeID != 1 || item.AttributeID != 4) {
                                                    if (item.AttributeID != 15) {
                                                        if (i < 2 || i > 5) {
                                                            var val = oprts[i];
                                                            if (i == 0) {
                                                                operators += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                                                            } else {
                                                                operators += '<option value="' + val.value + '">' + val.text + '</option>';
                                                            }
                                                        }
                                                    } else {
                                                        if (i < 7) {
                                                            var val = oprts[i];
                                                            if (i == 0) {
                                                                operators += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                                                            } else {
                                                                operators += '<option value="' + val.value + '"> ' + val.text + '</option>';
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="CatalogPricingRule.GetTextBoxValue(this)"><span class="sfBtn icon-success"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    } else if (item.InputTypeID == 2) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                        var oprts = eval(item.Operators);
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
                                        template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="CatalogPricingRule.GetTextBoxValue(this)"><span class="sfBtn icon-success"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    } else if (item.InputTypeID == 7) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" name="operator_{{= RulePath }}" id="operator_{{= RulePath }}" title="operator">';
                                        var oprts = eval(item.Operators);
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
                                        template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="CatalogPricingRule.GetTextBoxValue(this)"><span class="sfBtn icon-success"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    } else if (item.InputTypeID == 3) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" title="attribute" value="{{= AttributeID}}"/>' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" title="operator">';
                                        var oprts = eval(item.Operators);
                                        if (oprts != undefined && oprts.length > 0) {
                                            for (var i = 0; i < oprts.length; i++) {
                                                var val = oprts[i];
                                                operators += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                        var d = new Date();
                                        template += operators + '</select> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> ' + d.getFullYear() + '/' + (d.getMonth() * 1 + 1) + '/' + d.getDate() + ' </a><span class="cssClassElement"><input class="element-value-changer input-text datepicker" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><a href="#" class="cssClassOnClickApply" onclick="CatalogPricingRule.GetTextBoxValue(this)"><span class="sfBtn icon-success"></span></a> </span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    } else if (item.InputTypeID == 4 || item.InputTypeID == 6 || item.InputTypeID == 9 || item.InputTypeID == 10) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" value="{{= AttributeID}}" title="attribute" />' + item.AttributeNameAlias + ' <span class="cssClassOnClick"><a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" title="operator">';
                                        var oprts = eval(item.Operators);
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
                                        template += operators + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><select class="element-value-changer select" onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)">';
                                        var opts = eval(item.Values);
                                        if (opts != undefined && opts.length > 0) {
                                            for (var i = 0; i < opts.length; i++) {
                                                var val = opts[i];
                                                options += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                        template += options + '</select></span></span>&nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'
                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    } else if (item.InputTypeID == 5 || item.InputTypeID == 11 || item.InputTypeID == 12) {
                                        template = '<li><input type="hidden" name="type_{{= RulePath }}" id="type_{{= RulePath }}" title="type" value="Attribute"/><input type="hidden" name="attribute_{{= RulePath }}" id="attribute_{{= RulePath }}" value="{{= AttributeID}}" title="attribute" />' + item.AttributeNameAlias + ' <span class="cssClassOnClick"> <a class="cssClassFieldSetLabel" onclick="CatalogPricingRule.Edit(this)"> Is </a><span class="cssClassElement"><select class="element-value-changer select"  onblur="CatalogPricingRule.GetDropdownText(this)" onchange="CatalogPricingRule.GetDropdownText(this)" title="operator">';
                                        var oprts = eval(item.Operators);
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
                                        template += operators + '</select></span></span> &nbsp; <span class="cssClassOnClick"><a href="#" class="cssClassFieldSetLabel"  onclick="CatalogPricingRule.Edit(this)"> {{= valueText }} </a><span class="cssClassElement"><input class="element-value-changer input-text" name="value_{{= RulePath }}" id="value_{{= RulePath }}" title="value"  value="{{= value }}" /><select class="element-value-changer select" multiple="multiple"   onblur="GetMultipleValue(this)">';
                                        var opts = eval(item.Values);
                                        if (opts != undefined && opts.length > 0) {
                                            for (var i = 0; i < opts.length; i++) {
                                                var val = opts[i];
                                                options += '<option value="' + val.value + '"> ' + val.text + ' </option>';
                                            }
                                        }
                                        template += options + '</select><a href="#" class="cssClassOnClickApply"></span></span><span class="cssClassOnClick"><a href="javascript:void(0)" class="cssClassOnClickRemove" title="Remove" onclick="CatalogPricingRule.Delete(this)"><span class="sfBtn icon-close"></span></a></span></li>'

                                        template = '<script id="PricingRuleTemplate_' + item.AttributeID + '" type="text/html">' + template + '<\/script>';
                                        $('#placeholder-templates').append(template);
                                    }
                                }
                            });
                            plusButtonTemplate += '</optgroup></select></span></span>&nbsp;</li>';
                            masterConditionTemplate += '</optgroup></select></span></span></li></ul></li>';
                            masterConditionTemplate = '<script id="PricingRuleTemplate_master" type="text/html">' + masterConditionTemplate + '<\/script>';
                            $('#placeholder-templates').append(masterConditionTemplate);
                            plusButtonTemplate = '<script id="PricingRuleTemplate_plus" type="text/html">' + plusButtonTemplate + '<\/script>';
                            $('#placeholder-templates').append(plusButtonTemplate);
                            var ruleInfo = [{ Level: 0, RulePath: 0, value: "", valueText: "..." }];
                            $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo('.cssClassOnClickChildren');
                        }
                    });
                },
                error: function(err) {
                    csscody.error('<h2>' + getLocale(AspxCatalogPricingRule, "Error Message") + '</h2><p>' + JSON2.stringify(err) + '</p>');
                }
            });
            return treeHTML;
        },

        BindTreeViewChild: function(CategoryID, CategoryName, ParentID, CategoryLevel, deepLevel) {
            deepLevel = deepLevel + 1;
            var hasChild = false;
            var html = '';
            $.each(CategoryList, function(index, item) {
                if (item.CategoryLevel == CategoryLevel) {
                    if (item.ParentID == ParentID) {
                        html += '<li class="category_' + item.CategoryID + '" style="padding-left:' + item.CategoryLevel * 10 + 'px;">' + item.CategoryName + '</li>';
                        htmlChild = CatalogPricingRule.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                        if (htmlChild != "") {
                            html += htmlChild;
                        }
                    }
                }
            });
            return html;
        },

        isObject: function(x) {
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

        isFunction: function(x) {
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

        SavePricingRule: function() {
            var isValid = true;
            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
            catalogEditFlag = pricingRuleID;
            var txtName = $('#CatalogPriceRule-txtName').val();
            var txtDescription = $('#CatalogPriceRule-txtDescription').val();
            var lstCartPriceRuleRole = [];
            $('#CatalogPriceRule-mulRoles option:selected').each(function(i, option) {
                lstCartPriceRuleRole[i] = { CatalogPriceRuleID: pricingRuleID, RoleID: $(option).val() };
            });
            var txtFromDate = $('#CatalogPriceRule-txtFromDate').val();
            var txtToDate = $('#CatalogPriceRule-txtToDate').val();
            var txtPriority = $('#CatalogPriceRule-txtPriority').val() * 1;
            var chkIsActive = $('#CatalogPriceRule-chkIsActive').prop('checked') ? true : false;
            var ddlApply = $('#CatalogPriceRule-cboApply').val() * 1;
            var txtApplyValue = $('#CatalogPriceRule-txtValue').val().replace(/[^0-9.]/gi, '');
            var chkIsFurtherProcess = $('#CatalogPriceRule-chkFurtherRuleProcessing').prop('checked') ? true : false;
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
            var lstRuleConditions = new Array();
            var lstConditionDetails = new Array();
            lstRuleConditions[lstRuleConditions.length] = { IsAll: isAll, IsTrue: isTrue, ParentID: 0, CatalogConditionDetail: lstConditionDetails };

            $.each($('div.cssClassFieldSetContent > ul > li'), function(i, listItem) {


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
                    var att_priority = attrs[1] * 1;

                    lstConditionDetails[i] = { AttributeID: att_id, Priority: att_priority, RuleOperatorID: att_op, Value: $.trim(att_val) };
                    if ($.trim(att_val) == "") {
                        isValid = false;
                        return false;
                    }
                } else if (String(type).toLowerCase() == 'combination') {
                    isValid = CatalogPricingRule.GetChildPricingRule(lstRuleConditions, (listItem));
                }
            });

            $('div.cssClassFieldSetContent').find('ul').each(function(index) {
                $(this).prop('id', index + 1);
            });

            var arrParent = new Array();
            arrParent.push(0);

            $('div.cssClassFieldSetContent').find('ul li').find('input[value="combination"]').each(function() {
                arrParent.push($(this).parent().parent('ul').prop('id'));
            });

            var PriceRule = {
                CatalogPriceRule: {
                    CatalogPriceRuleID: pricingRuleID,
                    CatalogPriceRuleName: txtName,
                    CatalogPriceRuleDescription: txtDescription,
                    Apply: ddlApply,
                    Value: txtApplyValue,
                    IsFurtherProcessing: chkIsFurtherProcess,
                    FromDate: txtFromDate,
                    ToDate: txtToDate,
                    Priority: txtPriority,
                    IsActive: chkIsActive
                }
            };

            var objCatalogPricingRule = {
                CatalogPricingRuleInfo: {
                    CatalogPriceRule: PriceRule.CatalogPriceRule,
                    CatalogPriceRuleConditions: lstRuleConditions,
                    CatalogPriceRuleRoles: lstCartPriceRuleRole
                }
            };
            if (isValid) {
                this.config.url = this.config.baseURL + "SavePricingRule";
                this.config.data = JSON2.stringify({ "objCatalogPricingRuleInfo": objCatalogPricingRule.CatalogPricingRuleInfo, aspxCommonObj: aspxCommonObj(), "parentID": arrParent });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            } else {
                clickonce = 0;
                csscody.alert('<h2>' + getLocale(AspxCatalogPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Please enter valid data!") + '</p>');
            }
        },
        SaveAndApplyPricingRule: function() {
            var isValid = true;
            var pricingRuleID = $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val().replace(/[^0-9]/gi, '') * 1;
            catalogEditFlag = pricingRuleID;
            var txtName = $('#CatalogPriceRule-txtName').val();
            var txtDescription = $('#CatalogPriceRule-txtDescription').val();
            var lstCartPriceRuleRole = [];
            $('#CatalogPriceRule-mulRoles option:selected').each(function(i, option) {
                lstCartPriceRuleRole[i] = { CatalogPriceRuleID: pricingRuleID, RoleID: $(option).val() };
            });
            var txtFromDate = $('#CatalogPriceRule-txtFromDate').val();
            var txtToDate = $('#CatalogPriceRule-txtToDate').val();
            var txtPriority = $('#CatalogPriceRule-txtPriority').val() * 1;
            var chkIsActive = $('#CatalogPriceRule-chkIsActive').prop('checked') ? true : false;
            var ddlApply = $('#CatalogPriceRule-cboApply').val() * 1;
            var txtApplyValue = $('#CatalogPriceRule-txtValue').val().replace(/[^0-9.]/gi, '');
            var chkIsFurtherProcess = $('#CatalogPriceRule-chkFurtherRuleProcessing').prop('checked') ? true : false;
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
            var lstRuleConditions = new Array();
            var lstConditionDetails = new Array();
            lstRuleConditions[lstRuleConditions.length] = { IsAll: isAll, IsTrue: isTrue, ParentID: 0, CatalogConditionDetail: lstConditionDetails };

            $.each($('div.cssClassFieldSetContent > ul > li'), function(i, listItem) {


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
                    var att_priority = attrs[1] * 1;

                    lstConditionDetails[i] = { AttributeID: att_id, Priority: att_priority, RuleOperatorID: att_op, Value: $.trim(att_val) };
                    if ($.trim(att_val) == "") {
                        isValid = false;
                        return false;
                    }
                } else if (String(type).toLowerCase() == 'combination') {
                    isValid = CatalogPricingRule.GetChildPricingRule(lstRuleConditions, (listItem));
                }
            });

            $('div.cssClassFieldSetContent').find('ul').each(function(index) {
                $(this).prop('id', index + 1);
            });

            var arrParent = new Array();
            arrParent.push(0);

            $('div.cssClassFieldSetContent').find('ul li').find('input[value="combination"]').each(function() {
                arrParent.push($(this).parent().parent('ul').prop('id'));
            });

            var PriceRule = {
                CatalogPriceRule: {
                    CatalogPriceRuleID: pricingRuleID,
                    CatalogPriceRuleName: txtName,
                    CatalogPriceRuleDescription: txtDescription,
                    Apply: ddlApply,
                    Value: txtApplyValue,
                    IsFurtherProcessing: chkIsFurtherProcess,
                    FromDate: txtFromDate,
                    ToDate: txtToDate,
                    Priority: txtPriority,
                    IsActive: chkIsActive
                }
            };

            var objCatalogPricingRule = {
                CatalogPricingRuleInfo: {
                    CatalogPriceRule: PriceRule.CatalogPriceRule,
                    CatalogPriceRuleConditions: lstRuleConditions,
                    CatalogPriceRuleRoles: lstCartPriceRuleRole
                }
            };
            if (isValid) {                
                $("#btnApplyRules").prop("disabled", "disabled");
                $("#btnSaveAndApplyPricingRule").prop("disabled", "disabled");
                $(".catalogMessage").show();
                this.config.url = this.config.baseURL + "SaveAndApplyPricingRule";
                this.config.data = JSON2.stringify({ "objCatalogPricingRuleInfo": objCatalogPricingRule.CatalogPricingRuleInfo, aspxCommonObj: aspxCommonObj(), "parentID": arrParent });
                this.config.ajaxCallMode = 7;
                this.ajaxCall(this.config);
            } else {
                clickonce = 0;
                csscody.alert('<h2>' + getLocale(AspxCatalogPricingRule, "Information Alert") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Please enter valid data!") + '</p>');
            }
        },
        GetChildPricingRule: function(lstRuleConditions, parent) {
            var isValid = true;
            var pricingRuleID = $(parent).find('input[value="combination"]').prop('name').replace(/[^0-9]/gi, '') * 1;
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
            var lstConditionDetails = new Array();
            lstRuleConditions[lstRuleConditions.length] = { IsAll: isAll, IsTrue: isTrue, ParentID: 0, CatalogConditionDetail: lstConditionDetails };

            $.each($(parent).find('> ul > li'), function(i, childListItem) {
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
                    var att_priority = attrs[1] * 1;

                    lstConditionDetails[i] = { AttributeID: att_id, Priority: att_priority, RuleOperatorID: att_op, Value: att_val };
                    if ($.trim(att_val) == "") {
                        isValid = false;
                        return false;
                    }

                } else if (String(type).toLowerCase() == 'combination') {
                    isValid = CatalogPricingRule.GetChildPricingRule(lstRuleConditions, $(childListItem))
                }
            });
            return isValid;
        },

        GetPricingRules: function(ruleNm, startDt, endDt, isAct) {
            this.config.method = "GetPricingRules";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCatalogPricingRules_pagesize").length > 0) ? $("#gdvCatalogPricingRules_pagesize :selected").text() : 10;

            $("#gdvCatalogPricingRules").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCatalogPricingRule, "Catalog Pricing Rule ID"), cssclass: 'cssClassHeadCheckBox', name: 'CatalogPriceRuleID', coltype: 'checkbox', align: 'center', elemClass: 'attrCatPricingChkbox', elemDefault: false, controlclass: 'catPricingHeaderChkbox' },
                    { display: getLocale(AspxCatalogPricingRule, "Rule Name"), name: 'CatalogPriceRuleName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCatalogPricingRule, "From"), name: 'FromDate', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCatalogPricingRule, "To"), name: 'ToDate', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCatalogPricingRule, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', type: 'boolean', format: 'True/False', align: 'left' },
                    { display: getLocale(AspxCatalogPricingRule, "Priority"), name: 'priority', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCatalogPricingRule, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCatalogPricingRule, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CatalogPricingRule.EditCatalogPricingRule', arguments: '0' },
                    { display: getLocale(AspxCatalogPricingRule, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'CatalogPricingRule.DeleteCatalogPricingRule', arguments: '0' }
                ],
                txtClass: 'sfInputbox',
                rp: perpage,
                nomsg: getLocale(AspxCatalogPricingRule, "No Records Found!"),
                param: { ruleName: ruleNm, startDate: startDt, endDate: endDt, isActive: isAct, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false } }
            });
        },

        EditCatalogPricingRule: function(tblID, argus) {
            switch (tblID) {
            case "gdvCatalogPricingRules":
                $("#resetPricngRule").hide();
                CatalogPricingRule.GetPricingRuleByPricingRuleID(argus[3]);

                break;
            default:
                break;
            }
        },

        DeleteCatalogPricingRule: function(tblID, argus) {
            switch (tblID) {
            case "gdvCatalogPricingRules":
                if (argus[3]) {
                    var properties = { onComplete: function(e) { CatalogPricingRule.PricingRuleDelete(argus[0], e); } };
                    csscody.confirm("<h2>" + getLocale(AspxCatalogPricingRule, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCatalogPricingRule, "Are you sure you want to delete this catalog price rule?") + "</p>", properties);
                }
                break;
            default:
                break;
            }
        },
        CheckCatalogRuleExist:function(event)
        {
            if (event) {
                this.config.url = this.config.baseURL + "CheckCatalogRuleExist";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            }
        },
        AppyCatalogRules: function (event) {
            this.config.url = this.config.baseURL + "AppyCatalogRules";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
        },
        
        PricingRuleDelete: function(priceRuleID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeletePricingRule";
                this.config.data = JSON2.stringify({ catalogPricingRuleID: priceRuleID, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
            return false;
        },

        GetPricingRuleByPricingRuleID: function(pricingRuleID) {
            this.config.url = this.config.baseURL + "GetPricingRule";
            this.config.data = JSON2.stringify({ catalogPriceRuleID: pricingRuleID, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
            CatalogPricingRule.HideShowPrincingRulePanel(false, true);
        },

        BindChildCondition: function(arrCatalogPriceRuleCondition) {
            var parentself = '';
            var self = '';
            for (var c = 1; c < arrCatalogPriceRuleCondition.length; c++) {
                var catalogPriceRuleCondition = arrCatalogPriceRuleCondition[c];

                parentself = $('div.cssClassFieldSetContent > ul select[title="' + (c - 1) + '"]');
                $('div.cssClassFieldSetContent ul').each(function(i, item) {
                    if ($(item).prop('id') == catalogPriceRuleCondition.ParentID) {
                        parentself = $(item).children('li:last').find('select');
                        return false;
                    }
                });

                CatalogPricingRule.GetDropdownValue(parentself);
                var nchild = $(parentself).closest('ul').find('> li').length + 1;
                var priority = $(parentself).prop('title') * 1 + 1;
                var path = $(parentself).prop('title') * 1;
                var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: c, AttributeID: 0, value: "", valueText: "..." }];
                $("#PricingRuleTemplate_master").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(parentself).closest('li').parent());
                $(parentself).closest('ul').prop('id', catalogPriceRuleCondition.ParentID);

                $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(0) > a.cssClassFieldSetLabel').text(catalogPriceRuleCondition.IsAll ? "ALL" : "ANY");
                $(parentself).closest('ul').find('> li:nth-child(' + nchild + ') > span:eq(1) > a.cssClassFieldSetLabel').text(catalogPriceRuleCondition.IsTrue ? "TRUE" : "FALSE");
                CatalogPricingRule.Delete(parentself);
                var cat_index = 0;
                for (var d = 0; d < catalogPriceRuleCondition.CatalogConditionDetail.length; d++) {
                    var catalogConditionDetail = catalogPriceRuleCondition.CatalogConditionDetail[d];
                    var attr_ID = catalogConditionDetail.AttributeID;
                    var self = $('div.cssClassFieldSetContent > ul select[title="' + c + '"]');
                    var priority = $(self).prop('title') * 1;
                    var path = $(self).prop('title') * 1;
                    var valueText = "...";
                    if (catalogConditionDetail.Value == "") {
                        valueText = "...";
                    } else {
                        valueText = catalogConditionDetail.Value;
                    }

                    var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (path * 1 + 1), AttributeID: attr_ID, value: catalogConditionDetail.Value, valueText: valueText }];

                    if (attr_ID == "0") {
                        $("#PricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                        $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                        $(self).closest('li').parent().find(".MultipleSelectBox_PricingRule").multipleSelectBox(
                        {
                            onSelectEnd: function(e, resultList) {
                                $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                $(this).parent().siblings('input').val(resultList.join(", "));
                            }
                        });
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(CatalogPricingRule.setOperators(catalogConditionDetail.RuleOperatorID));
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', catalogConditionDetail.RuleOperatorID);


                        var arrCategoryID = catalogConditionDetail.Value.split(",");
                        if (arrCategoryID != "") {
                            var catName = [];
                            var catlen = arrCategoryID.length;
                            for (var i = 0; i < catlen; i++) {

                                $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function() {
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
                        CatalogPricingRule.Delete(self);
                    } else if (attr_ID > 0) {
                        $("#PricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                        $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                        CatalogPricingRule.GetDropdownText(self);
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(CatalogPricingRule.setOperators(catalogConditionDetail.RuleOperatorID));
                        $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', catalogConditionDetail.RuleOperatorID);

                        var arrValues = catalogConditionDetail.Value.split(",");
                        var seconLast = $(self).closest('ul').find('>li').length * 1 - 1;
                        $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('select.element-value-changer option').each(function() {
                            for (var i = 0; i < arrValues.length; i++) {
                                if ($(this).val() == $.trim(arrValues[i])) {
                                    $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                    $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $.trim(arrValues[i]));
                                }
                            }
                        });
                        CatalogPricingRule.Delete(self);
                    } else {
                        $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                    }
                }
            }
        },

        setOperators: function(RuleOperatorID) {
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

        SetTabActive: function(index, tabContainerID) {
            var $tabs = $("#" + tabContainerID).tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', index);
        },

        HideShowPrincingRulePanel: function(showGrid, showTabMenu) {
            if (showGrid) {
                $('#pricingRuleGrid').show();
            } else {
                $('#pricingRuleGrid').hide();
            }
            if (showTabMenu) {
                $('#pricingRuleTabMenu').show();
            } else {
                $('#pricingRuleTabMenu').hide();
            }
        },

        DeleteSelectedCatRules: function() {
        },

        AddPricingRule: function() {
            CatalogPricingRule.ResetPricingRule();
            $("#resetPricngRule").show();
            CatalogPricingRule.HideShowPrincingRulePanel(false, true);
        },

        CancelPricingRule: function() {
            CatalogPricingRule.HideShowPrincingRulePanel(true, false);
            $('#CatalogPriceRule-txtPriority').removeClass('valid').removeClass('error');
            $("#priority").html("");
            $('#created').html('');
            $('.to').parents('td').find('input').prop("style", '');
            $('label.error').html('');
            $('.error').removeClass("error");
        },

        ResetPricingRule: function() {
            $('div.cssClassFieldSetContent > ul > li').not('li:last').remove();
            $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val(0);
            $('#CatalogPriceRule-txtName').val('');
            $('#CatalogPriceRule-txtDescription').val('');
            $('#CatalogPriceRule-txtFromDate').val('');
            $('#CatalogPriceRule-txtToDate').val('');
            $('#CatalogPriceRule-txtPriority').val('');
            $('#CatalogPriceRule-chkIsActive').prop("checked", "checked");
            $('#CatalogPriceRule-cboApply').val(1);
            $('#CatalogPriceRule-txtValue').val('');
            $('#CatalogPriceRule-chkFurtherRuleProcessing').removeAttr("checked");
            $('#CatalogPriceRule-mulRoles').find('option').each(function() {
                $(this).removeAttr("selected");
            });
            $('label.error').html('');
            $('.error').removeClass("error");
            $('#created').html('');
            $('.to').parents('td').find('input').prop('style', '');
            CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");
            if ($("#CatalogPriceRule-cboApply option:selected").val() == 1 || $("#CatalogPriceRule-cboApply option:selected").val() == 3) {
                $('#CatalogPriceRule-txtValue').unbind();
                $('#CatalogPriceRule-txtValue').numeric({ max: 100 });
                $('#CatalogPriceRule-txtValue').prop("maxlength", "5");
                $('#CatalogPriceRule-txtValue').bind('select', function() {
                    $(this).val('');
                });
                CatalogPricingRule.bindfocusout();
            } else {
                $('#CatalogPriceRule-txtValue').unbind();
                $('#CatalogPriceRule-txtValue').prop("maxlength", "8");
                $('#CatalogPriceRule-txtValue').numeric({ max: 99999999 });
                CatalogPricingRule.bindfocusout();
            }
            $('#CatalogPriceRule-txtPriority').removeClass('valid').removeClass('error');
            $("#priority").html("");
        },

        SearchPricingRule: function() {
            var ruleNm = $.trim($("#txtCatalogPriceRuleSrc").val());
            var startDt = $.trim($("#txtPricingRuleStartDate").val());
            var endDt = $.trim($('#txtPricingRuleEndDate').val());
            var isAct = $.trim($('#ddlPricingRuleIsActive').val()) == "" ? null : ($.trim($('#ddlPricingRuleIsActive').val()) == "True" ? true : false);

            if (ruleNm.length < 1) {
                ruleNm = null;
            }

            if (startDt.length < 1) {
                startDt = null;
            }

            if (endDt.length < 1) {
                endDt = null;
            }

            CatalogPricingRule.GetPricingRules(ruleNm, startDt, endDt, isAct);
        },
        /*============= End of Pricing Rules ===============================*/

        CheckPriorityUniqueness: function(catalogPriceRuleID) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            aspxCommonInfo.CultureName = null;
            var priorityVal = $('#CatalogPriceRule-txtPriority').val();
            this.config.url = this.config.baseURL + "CheckCatalogPriorityUniqueness";
            this.config.data = JSON2.stringify({ catalogPriceRuleID: catalogPriceRuleID, priority: priorityVal, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
            return CatalogPricingRule.vars.isUnique;
        },
        ajaxSuccess: function(data) {
            switch (CatalogPricingRule.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Successful Message") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Selected catalog price rule(s) has been deleted successfully.") + '</p>');
                CatalogPricingRule.GetPricingRules(null, null, null, null);
                break;
            case 2:
                var options = '';
                $.each(data.d, function(i, item) {
                    options += '<option value="' + item.RoleID + '"> ' + item.RoleName + ' </option>';
                });
                $("#CatalogPriceRule-mulRoles").html(options);
                break;
            case 3:
                csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Successful Message") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Catalog price rule has been deleted successfully.") + '</p>');
                CatalogPricingRule.GetPricingRules(null, null, null, null);
                break;
            case 4:
                CatalogPricingRule.vars.isUnique = data.d;
                break;
            case 5:
                var notificationText = "";
                switch (data.d) {
                case "success":
                    if (catalogEditFlag > 0) {
                        notificationText = getLocale(AspxCatalogPricingRule, "Catalog price rule has been updated successfully.");
                    } else {
                        notificationText = getLocale(AspxCatalogPricingRule, "Catalog price rule has been saved successfully.");
                    }
                    break;
                case "notify":
                    notificationText = getLocale(AspxCatalogPricingRule, "More than 3 rules are not allowed in free version of AspxCommerce!");
                    break;
                }
                if (notificationText != "") {

                    csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Information Message") + '</h2><p>' + notificationText + '</p>');
                    arrParent = new Array();
                    CatalogPricingRule.GetPricingRules(null, null, null, null);
                    CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");
                    CatalogPricingRule.HideShowPrincingRulePanel(true, false);
                }
                clickonce = 0;
                break;
            case 6:
                CatalogPricingRule.SetTabActive(1, "CatalogPriceRule-TabContainer");
                $('div.cssClassFieldSetContent > ul > li').not('li:last').remove();
                var dsData = eval(data.d);
                var catalogPriceRule = dsData.CatalogPriceRule;
                var arrCatalogPriceRuleCondition = dsData.CatalogPriceRuleConditions;
                var arrCatalogPriceRuleRole = dsData.CatalogPriceRuleRoles;
                $('div.cssClassFieldSetContent > span > input[name="pricingRuleID"]').val(catalogPriceRule.CatalogPriceRuleID)
                $('#CatalogPriceRule-txtName').val(catalogPriceRule.CatalogPriceRuleName);
                $('#CatalogPriceRule-txtDescription').val(catalogPriceRule.CatalogPriceRuleDescription);
                $('#CatalogPriceRule-txtFromDate').val(CatalogPricingRule.JSONDateToString(catalogPriceRule.FromDate, "yyyy/MM/dd"));
                $('#CatalogPriceRule-txtToDate').val(CatalogPricingRule.JSONDateToString(catalogPriceRule.ToDate, "yyyy/MM/dd"));
                $('#CatalogPriceRule-txtPriority').val(catalogPriceRule.Priority);
                $('#CatalogPriceRule-chkIsActive').prop("checked", catalogPriceRule.IsActive);
                $('#CatalogPriceRule-cboApply').val(catalogPriceRule.Apply);
                $('#CatalogPriceRule-txtValue').val(catalogPriceRule.Value);
                $('#CatalogPriceRule-chkFurtherRuleProcessing').prop("checked", catalogPriceRule.IsFurtherProcessing);
                $('#CatalogPriceRule-mulRoles').find('option').each(function() {
                    $(this).removeAttr("selected");
                });
                if ($("#CatalogPriceRule-cboApply").val() == 1 || $("#CatalogPriceRule-cboApply").val() == 3) {
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').numeric({ max: 100 });
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "5");
                    $('#CatalogPriceRule-txtValue').bind('select', function() {
                        $(this).val('');
                    });
                    CatalogPricingRule.bindfocusout();
                } else {
                    $('#CatalogPriceRule-txtValue').unbind();
                    $('#CatalogPriceRule-txtValue').prop("maxlength", "8");
                    $('#CatalogPriceRule-txtValue').numeric({ max: 99999999 });
                    CatalogPricingRule.bindfocusout();
                }
                for (var r = 0; r < arrCatalogPriceRuleRole.length; r++) {
                    $('#CatalogPriceRule-mulRoles').find('option').each(function() {
                        if ($(this).val() == arrCatalogPriceRuleRole[r].RoleID) {
                            $(this).prop("selected", "selected");
                        }
                    });
                }
                var catalogPriceRuleCondition = arrCatalogPriceRuleCondition[0];
                if (catalogPriceRuleCondition.ParentID == 0) {
                    $('div.cssClassFieldSetContent > span:nth-child(1) > span > select.element-value-changer').val(catalogPriceRuleCondition.IsAll ? "ALL" : "ANY");
                    $('div.cssClassFieldSetContent > span:nth-child(1) > a.cssClassFieldSetLabel').text(catalogPriceRuleCondition.IsAll ? "ALL" : "ANY");
                    $('div.cssClassFieldSetContent > span:nth-child(2) > span > select.element-value-changer').val(catalogPriceRuleCondition.IsTrue ? "TRUE" : "FALSE");
                    $('div.cssClassFieldSetContent > span:nth-child(2) > a.cssClassFieldSetLabel').text(catalogPriceRuleCondition.IsTrue ? "TRUE" : "FALSE");

                    var cat_index = 0;
                    for (var d = 0; d < catalogPriceRuleCondition.CatalogConditionDetail.length; d++) {
                        var catalogConditionDetail = catalogPriceRuleCondition.CatalogConditionDetail[d];
                        var attr_ID = catalogConditionDetail.AttributeID;
                        var self = $('div.cssClassFieldSetContent > ul > li:last > span > span > select.element-value-changer ');

                        var priority = $(self).prop('title') * 1 + 1;
                        var path = $(self).prop('title') * 1;
                        var valueText = "...";
                        if (catalogConditionDetail.Value == "") {
                            valueText = "...";
                        } else {
                            valueText = catalogConditionDetail.Value;
                        }
                        var ruleInfo = [{ Level: path, RulePath: (path + '-' + priority), ChildRulePath: (catalogPriceRuleCondition.ParentID), AttributeID: attr_ID, value: catalogConditionDetail.Value, valueText: valueText }];

                        if (attr_ID == "0") {
                            $("#PricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());
                            $(self).closest('li').parent().find(".MultipleSelectBox_PricingRule").multipleSelectBox(
                            {
                                onSelectEnd: function(e, resultList) {
                                    $(this).parent().parent().parent('SPAN').find("a.cssClassFieldSetLabel").html(resultList.join(", "));
                                    $(this).parent().siblings('input').val(resultList.join(", "));
                                }
                            });
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(CatalogPricingRule.setOperators(catalogConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', catalogConditionDetail.RuleOperatorID);

                            var arrCategoryID = catalogConditionDetail.Value.split(",");
                            if (arrCategoryID != "") {
                                var catName = [];
                                var catlen = arrCategoryID.length;
                                for (var i = 0; i < catlen; i++) {

                                    $(self).closest('li').parent('ul').find('div.pricingRuleCategoryList > ul > li').each(function() {
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
                            CatalogPricingRule.Delete(self);
                        } else if (attr_ID > 0) {
                            $("#PricingRuleTemplate_" + attr_ID).render(ruleInfo).appendTo($(self).closest('li').parent());
                            $("#PricingRuleTemplate_plus").render(ruleInfo).appendTo($(self).closest('li').parent());

                            var arrValues = valueText.split(",");
                            var seconLast = $(self).closest('ul').find('>li').length * 1 - 1;

                            CatalogPricingRule.GetDropdownText(self);

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').parent().parent().find('a').html($(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').val());
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' });

                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').text(CatalogPricingRule.setOperators(catalogConditionDetail.RuleOperatorID));
                            $(self).closest('li').parent().children('li:eq(' + (d + 1) + ')').find('span:eq(0)').find('a.cssClassFieldSetLabel').prop('title', catalogConditionDetail.RuleOperatorID);


                            $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('select.element-value-changer option').each(function() {
                                for (var i = 0; i < arrValues.length; i++) {
                                    if ($(this).val() == $.trim(arrValues[i])) {
                                        $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').text($(this).html());
                                        $(self).closest('ul').find('> li:nth-child(' + seconLast + ')').find('span:eq(2)').find('a.cssClassFieldSetLabel').prop('title', $.trim(arrValues[i]));
                                    }
                                }
                            });
                            CatalogPricingRule.Delete(self);
                        } else {
                            $(self).parent().parent('SPAN').removeClass("cssClassOnClickEdit");
                        }
                    }
                    CatalogPricingRule.BindChildCondition(arrCatalogPriceRuleCondition);
                }
                CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");
                break;
            case 7:
                var notificationText = "";
                switch (data.d) {
                case "success":
                    if (catalogEditFlag > 0) {
                        notificationText = getLocale(AspxCatalogPricingRule, "Catalog price rule has been updated successfully.But the process of applying may take more time...the process is going on.");
                    } else {
                        notificationText = getLocale(AspxCatalogPricingRule, "Catalog price rule has been saved successfully.");
                    }
                    break;
                case "notify":
                    notificationText = getLocale(AspxCatalogPricingRule, "More than 3 rules are not allowed in free version of AspxCommerce!");
                    break;
                }
                if (notificationText != "") {

                    csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Information Message") + '</h2><p>' + notificationText + '</p>');
                    arrParent = new Array();
                    CatalogPricingRule.GetPricingRules(null, null, null, null);
                    CatalogPricingRule.SetTabActive(0, "CatalogPriceRule-TabContainer");
                    CatalogPricingRule.HideShowPrincingRulePanel(true, false);
                }
                clickonce = 0;
                break;
                case 8:                    
                    if (data.d) { 
                        $("#btnApplyRules").prop("disabled", "disabled");
                        $("#btnSaveAndApplyPricingRule").prop("disabled", "disabled");
                        CatalogPricingRule.AppyCatalogRules();
                        $(".catalogMessage").show();
                    }
                    else {
                        csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Information Message") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "There is no active catalog rules") + '</p>');
                    }
                case 9:
                    csscody.info('<h2>' + getLocale(AspxCatalogPricingRule, "Successful Message") + '</h2><p>' + getLocale(AspxCatalogPricingRule, "Catalog rule has been applied successfully. But the process is going on it may takes several minutes.") + '</p>');                   
                    break;              
            }
        }
    };
    CatalogPricingRule.init();
});