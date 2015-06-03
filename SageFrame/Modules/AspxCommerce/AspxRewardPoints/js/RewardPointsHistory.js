var RewardPointsHistory;
$(function() {
    var $accor = '';
    var chkIsActive = true;   
    var ModuleServicePath = RewardPointsModulePath + "RewardPointsHandler.ashx/";
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var generalSettings = {
        AdditionOrderStatusID: 0,
        SubtractOrderStatusID: 0
    };
    var rewardPointsCommonObj = {
        RewardPointSettingsID: 0,
        RewardRuleName: "",
        RewardRuleID: 0,
        RewardRuleType: "",
        RewardPoints: 0,
        PurchaseAmount: 0,
        IsActive: true
    };
    var generalSettingsObj = {
        GeneralSettingsID: 0,
        RewardPoints: 0,
        RewardExchangeRate:0,
        AddOrderStatusID: 0,
        SubOrderStatusID: 0,
        RewardPointsExpiresInDays: 0,
        MinRedeemBalance: 0,
        BalanceCapped: 0,
        IsActive: true
    };
    var rewardPointsHistoryObj = {
        CustomerName: "",
        Email: "",
        CustomerID: 0,
        DateFrom: "",
        DateTo: ""
    };

    RewardPointsHistory = {

        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
                       baseURL: aspxRootPath + 'Modules/AspxCommerce/AspxRewardPoints/RewardPointsHandler.ashx/',
            method: "",
            url: "",
            ajaxCallMode: "",
            error: "",
            sessionValue: ""
        },

        ajaxCall: function(config) {
            $.ajax({
                type: RewardPointsHistory.config.type,
                contentType: RewardPointsHistory.config.contentType,
                cache: RewardPointsHistory.config.cache,
                async: RewardPointsHistory.config.async,
                url: RewardPointsHistory.config.url,
                data: RewardPointsHistory.config.data,
                dataType: RewardPointsHistory.config.dataType,
                success: RewardPointsHistory.config.ajaxCallMode,
                error: RewardPointsHistory.config.error
            });
        },
        SearchRewards: function() {
            var dateFrom = $.trim($("#txtDateFrom").val());

            if (dateFrom.length < 1) {
                dateFrom = null;
            }

            RewardPointsHistory.GetRewardPointsHistoryByCustomer(rewardPointsHistoryObj.CustomerID, dateFrom);
        },

        Init: function () {            
            $("#spanCurrency").html(curSymbol);
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            $("#txtDateFrom").datepicker({ dateFormat: 'yy/mm/dd' });

            var form2 = $("#form1").validate({
                ignore: ":hidden",
                messages: {

                    RewardPoints: { required: '*', number: 'Please enter a number',
                        maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                    },
                    RewardAmount: { required: '*', number: 'Please enter a number',
                        maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                    },
                    RewardPointsExpiresInDays: { required: '*', number: 'Please enter a number',
                        maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                    },
                    MinRedeemBalance: { required: '*', number: 'Please enter a number',
                        maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                    },
                    CappedBalance: { required: '*', number: 'Please enter a number',
                        maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                    },
                    ddlOrderStatus: { required: '*'
                    },
                    SelectOrderStatus: { required: '*'
                    }

                }
            });



            RewardPointsHistory.HideAll();
            $("#dvEditView").show();

            $("#btnBackHistory").click(function() {
                RewardPointsHistory.HideAll();
                $("#dvEditView").show();
            });

            $("#btnEdit").click(function() {
                RewardPointsHistory.GoBackToHistoryPage();
            });

            $("#btnBack").click(function() {
                RewardPointsHistory.HideAll();
                $("#dvEditView").show();

            });

            $("#btnBackEdit").click(function() {
                RewardPointsHistory.HideAll();
                $("#dvEditView").show();
            });
            $("#btnSave").bind("click", function() {
                if (form2.form()) {
                    var rp = $("#txtRewardPoints").val();
                    var ra = $("#txtRewardAmount").val();
                                       var rb = $("#txtMinRedeemBalance").val();
                    var cb = $("#txtCappedBalance").val();
                    if (eval(rp) <= 0 || eval(ra) <= 0 || eval(rb) <= 0 || eval(cb) <= 0) {
                        csscody.alert("<h2>" + getLocale(AspxRewardPoints, "Information Alert") + "</h2><p>" + getLocale(AspxRewardPoints, getLocale(AspxRewardPoints, "Input values must be greater than zero.!")) + "</p>");
                    }
                    else {
                        RewardPointsHistory.SaveGeneralSettings();
                    }
                }
            });
            $("#btnAddRewardRule").click(function() {
                RewardPointsHistory.HideAll();
                RewardPointsHistory.LoadControl("Modules/AspxCommerce/AspxRewardPoints/RewardPointsSetting.ascx");
            });
            $("#btnSearchRewardRule").click(function() {
                RewardPointsHistory.SearchRewardPoints();
            });
            $('#txtRewardRule,#ddlIsActive').keyup(function(event) {
                if (event.keyCode == 13) {
                    RewardPointsHistory.SearchRewardPoints();
                }
            });
            $("#btnDeleteRewardPointsRuleEdit").bind("click", function() {
                RewardPointsHistory.DeleteRewardPointsRule($("#hdnRewardRuleSettingID").val());
            });

            $("#btnSubmitRewardPointsRuleEdit").bind("click", function() {
                var form3 = $("#form1").validate({
                    ignore: ":hidden",
                    messages: {

                        RewardRuleNameEdit: { required: '*'
                        },
                        RewardPointsEdit: { required: '*', number: "Please enter a number",
                            maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                        },
                        PurchaseAmountEdit: { required: '*', number: "Please enter a number",
                            maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                        }

                    }
                });

                if (form3.form()) {
                    RewardPointsHistory.UpdateNewRewardPoints();
                }

            });
            $("#btnCancelRewardPointsRuleEdit").bind("click", function() {
                RewardPointsHistory.GoBackToHistoryPage();
            });
            $("#btnView").click(function() {
                RewardPointsHistory.HideAll();
                $("#dvRewardPointsHistory").show();
                RewardPointsHistory.GetRewardPointsHistory(null, null);
            });
            $("#btnSearchHistory").click(function() {
                RewardPointsHistory.SearchRewardPointsHistory();
            });
            $('#txtCustomerName,#txtEmail').keyup(function(event) {
                if (event.keyCode == 13) {
                    RewardPointsHistory.SearchRewardPointsHistory();
                }
            });
            $("#btnBackToHistory").click(function() {
                $("#txtDateFrom").val('');
                RewardPointsHistory.HideAll();
                $("#dvRewardPointsHistory").show();
                RewardPointsHistory.GetRewardPointsHistory(null, null);
            });
        },
        SearchRewardPointsHistory: function() {
            var customerName = $.trim($("#txtCustomerName").val());
            var email = $.trim($("#txtEmail").val());

            if (customerName.length < 1) {
                customerName = null;
            }
            if (email.length < 1) {
                email = null;
            }
            RewardPointsHistory.GetRewardPointsHistory(customerName, email);
        },
        SearchRewardPoints: function() {
            var rewardRuleName = $.trim($("#txtRewardRule").val());
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : $.trim($("#ddlIsActive").val()) == 0 ? true : false;
            if (rewardRuleName.length < 1) {
                rewardRuleName = null;
            }
            RewardPointsHistory.GetRewardPointSettings(rewardRuleName, isAct);
        },
        LoadControl: function(controlName) {
            $.ajax({
                type: "POST",
                url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    $('#divLoadUserControl').html(response.d);
                },
                error: function() {
                    csscody.error('<h2>' + getLocale(AspxRewardPoints, 'Error Message') + '</h2><p>' + getLocale(AspxRewardPoints, 'Failed to load control!.') + '</p>');
                }
            });
        },
        HideAll: function() {
            $("#dvEditView").hide();
            $("#dvRewardPointsHistory").hide();
            $("#dvEditRewardPoints").hide();
            $("#dvRewardPointsEditForm").hide();
            $("#dvRewardPointsHistoryByCustomer").hide();
        },
        GetRewardPointsHistory: function(customerNm, email) {
            rewardPointsHistoryObj.CustomerName = customerNm;
            rewardPointsHistoryObj.Email = email;
            this.config.method = "RewardPointsHistoryGetAll";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRewardPointsHistory_pagesize").length > 0) ? $("#gdvRewardPointsHistory_pagesize :selected").text() : 10;
              $("#gdvRewardPointsHistory").sagegrid({
                url: ModuleServicePath,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxRewardPoints, 'S.No.'), name: 'row_number', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Customer ID'), name: 'customer_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Customer Name'), name: 'customer_name', cssclass: 'cssClassCustomerName', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Email'), name: 'email', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Total Points'), name: 'total_points', cssclass: 'cssClassTotalPoints', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Used Points'), name: 'used_points', cssclass: 'cssClassUsedPoints', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Net Points'), name: 'net_points', cssclass: 'cssClassNetPoints', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Net Amount'), name: 'net_amount', cssclass: 'cssClassNetAmount', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Added By'), name: 'added_by', cssclass: 'cssClassAddedBy', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                     { display: getLocale(AspxRewardPoints, 'View'), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'RewardPointsHistory.ViewRewardPointsHistory', arguments: '1,2,3,4,5,6,7,8,9,10,11,12' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxRewardPoints, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, RewardPointsHistoryCommonObj: rewardPointsHistoryObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 9: { sorter: false} }


            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ViewRewardPointsHistory: function(tblID, argus) {

            switch (tblID) {
                case "gdvRewardPointsHistory":
                    $("#spanNetPoints").html(argus[8]);
                    $("#spanNetAmount").html(argus[9]);
                    RewardPointsHistory.HideAll();
                    $("#dvRewardPointsHistoryByCustomer").show();
                    RewardPointsHistory.GetRewardPointsHistoryByCustomer(argus[3], null);
                    break;
                default:
                    break;
            }
        },

        GetRewardPointsHistoryByCustomer: function(customerId, dateFrom) {
            rewardPointsHistoryObj.CustomerID = customerId;
            rewardPointsHistoryObj.DateFrom = dateFrom;
                       this.config.method = "GetMyRewardPointsHistory";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRewardPointsHistoryByCustomer_pagesize").length > 0) ? $("#gdvRewardPointsHistoryByCustomer_pagesize :selected").text() : 10;

            $("#gdvRewardPointsHistoryByCustomer").sagegrid({
                url: ModuleServicePath,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxRewardPoints, 'S.No.'), name: 'row_number', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Points Rewarded'), name: 'rewardPoints', cssclass: 'cssClassRewardPoints', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Reward Amount'), name: 'rewardAmount', cssclass: 'cssClassRewardAmount', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Points Used'), name: 'usedRewardPoints', cssclass: 'cssClassUsedRewardPoints', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Balance'), name: 'balanceRewardPoints', cssclass: 'cssClassBalanceRewardPoints', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Reward Reason'), name: 'rewardReason', cssclass: 'cssClassRewardReason', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Added On'), name: 'addedOn', cssclass: 'cssClassAddedOn', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Expires On'), name: 'expiresOn', cssclass: 'cssClassExpiresOn', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Expires in Days'), name: 'expiresInDays', cssclass: 'cssClassExpiresInDays', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False', hide: true }
                ],
                rp: perpage,
                nomsg: getLocale(AspxRewardPoints, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, RewardPointsHistoryCommonObj: rewardPointsHistoryObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 9: { sorter: false} }

            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        GetRewardPointSettings: function(rewardRuleName, isAct) {
            rewardPointsCommonObj.RewardRuleName = rewardRuleName;
            rewardPointsCommonObj.IsActive = isAct;

            this.config.method = "RewardPointsSettingGetAll";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRewadPointsSetting_pagesize").length > 0) ? $("#gdvRewadPointsSetting_pagesize :selected").text() : 10;
            $("#gdvRewadPointsSetting").sagegrid({
                url: ModuleServicePath,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxRewardPoints, 'S.No.'), name: 'row_number', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Reward Point Settings ID'), name: 'rewardPointSettings_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Reward Rule Name'), name: 'rewardRule_name', cssclass: 'cssClassRewardRuleName', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Reward Rule ID'), name: 'rewardRule_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxRewardPoints, 'Reward Rule Type'), name: 'rewardRule_type', cssclass: 'cssClassRewardRuleDetails', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Reward Points'), name: 'reward_points', cssclass: 'cssClassRewardPoints', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'On Purchase') + " (" + curSymbol + ")", name: 'reward_points', cssclass: 'cssClassRewardPoints', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                    { display: getLocale(AspxRewardPoints, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False' },
                    { display: getLocale(AspxRewardPoints, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxRewardPoints, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'RewardPointsHistory.EditRewardPoints', arguments: '1,2,3,4,5,6,7,8,9,10,11,12' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxRewardPoints, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, rewardPointsCommonObj: rewardPointsCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false} }


            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        Boolean: function(str) {
            switch (str.toLowerCase()) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        },
        EditRewardPoints: function(tblID, argus) {
            switch (tblID) {
                case "gdvRewadPointsSetting":

                    if (argus[5] == 3 || argus[5] == 8 || argus[5] == 9) {
                        $("#trPAmount").show();
                    }
                    else {
                        $("#trPAmount").hide();
                    }
                    $("#hdnRewardRuleSettingID").val(argus[3]);
                    $("#txtRewardRuleNameEdit").val(argus[4]);
                    $("#hdnRewardRuleID").val(argus[5]);
                    $("#txtRewardPointsEdit").val(argus[7]);
                    $("#txtPurchaseAmountEdit").val(argus[8]);
                    $("#chkIsActiveEdit").prop('checked', RewardPointsHistory.Boolean(argus[9]));
                    chkIsActive = RewardPointsHistory.Boolean(argus[7]);
                    $("#" + lblRewardPointsRuleEdit).html("Editing Reward Points Rule : " + argus[4]);
                    RewardPointsHistory.HideAll();
                    $("#dvRewardPointsEditForm").show();
                    RewardPointsHistory.RewardPointsRuleListBind();
                    break;

            }
        },
        GoBackToHistoryPage: function() {
            RewardPointsHistory.HideAll();
            RewardPointsHistory.GetOrderStatus();
            RewardPointsHistory.GetRewardPointSettings(null, null);
            $("#dvEditRewardPoints").show();
        },
        UpdateNewRewardPoints: function() {

            rewardPointsCommonObj.RewardPointSettingsID = $("#hdnRewardRuleSettingID").val();
            rewardPointsCommonObj.RewardRuleName = $("#txtRewardRuleNameEdit").val();
            rewardPointsCommonObj.RewardRuleID = $("#ddlRewardRule option:selected").val();
            rewardPointsCommonObj.RewardRuleType = $("#ddlRewardRule option:selected").text();
            rewardPointsCommonObj.RewardPoints = $("#txtRewardPointsEdit").val();
            rewardPointsCommonObj.PurchaseAmount = $("#txtPurchaseAmountEdit").val();
            rewardPointsCommonObj.IsActive = $('#chkIsActiveEdit').prop('checked') ? true : false;

            this.config.method = "RewardPointsSaveUpdateNewRule";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ rewardPointsCommonObj: rewardPointsCommonObj, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = RewardPointsHistory.UpdateNewRewardPointsSuccess;
            this.config.error = RewardPointsHistory.UpdateNewRewardPointsError;
            this.ajaxCall(this.config);
        },
        UpdateNewRewardPointsSuccess: function() {
            RewardPointsHistory.GoBackToHistoryPage();
            csscody.info("<h2>" + getLocale(AspxRewardPoints, "Successful Message") + "</h2><p>" + getLocale(AspxRewardPoints, "New reward points rule saved successfully.") + "</p>");
            $('#fade, #popuprel2').fadeOut();
        },
        UpdateNewRewardPointsError: function() {
            csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to save new reward points rule!") + "</p>");
        },

        DeleteRewardPointsRule: function(rewardRuleSettingID) {
            var properties = {
                onComplete: function(e) {
                    RewardPointsHistory.DeleteRewardRule(rewardRuleSettingID, e);
                }
            }
            csscody.confirm("<h2>" + getLocale(AspxRewardPoints, "Delete Confirmation") + "</h2><p>" + getLocale(AspxRewardPoints, "Are you sure you want to delete this reward rule?") + "</p>", properties);
        },

        DeleteRewardRule: function(_rewardRuleSettingID, event) {
            rewardPointsCommonObj.RewardPointSettingsID = _rewardRuleSettingID;
            if (event) {
                this.config.url = ModuleServicePath + "RewardPointsRuleDelete";
                this.config.data = JSON2.stringify({ rewardPointCommonObj: rewardPointsCommonObj, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = RewardPointsHistory.RewardPointsRuleDeleteSuccess;
                this.ajaxCall(this.config);
                return false;
            } else return false;
        },
        RewardPointsRuleDeleteSuccess: function() {
            RewardPointsHistory.GoBackToHistoryPage();
            csscody.info("<h2>" + getLocale(AspxRewardPoints, "Successful Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Reward points rule deleted successfully.") + "</p>");
        },
        RewardPointsRuleListBind: function() {
            this.config.method = "RewardPointsRuleListBind";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = RewardPointsHistory.RewardPointsRuleListBindStatus;
            this.config.error = RewardPointsHistory.RewardPointsRuleListBindError;
            this.ajaxCall(this.config);
        },
        RewardPointsRuleListBindStatus: function(msg) {
            $("#ddlRewardRule").empty();
            var item;
            var length = msg.d.length;
            for (var index = 0; index < length; index++) {
                item = msg.d[index];
                if ($("#hdnRewardRuleID").val() == item.RewardRuleID) {
                    var rewardRuleElements = "<option value=" + item.RewardRuleID + ">" + item.RewardRuleType + "</option>";
                    $("#ddlRewardRule").append(rewardRuleElements);
                }
            };
        },
        RewardPointsRuleListBindError: function() {
            csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to load order satatus!") + "</p>");
        },
        GetOrderStatus: function() {
            $("#ddlOrderStatus").empty();
            $("#SelectOrderStatus").empty();
            this.config.method = "AspxCoreHandler.ashx/GetStatusList";
            this.config.url = aspxservicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.async = false;
            this.config.ajaxCallMode = RewardPointsHistory.BindOrderStatus;
            this.config.error = RewardPointsHistory.OrderStatusError;
            this.ajaxCall(this.config);
        },

        BindOrderStatus: function(msg) {
            var item;
            var length = msg.d.length;
            for (var index = 0; index < length; index++) {
                item = msg.d[index];
                var orderStatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                $("#ddlOrderStatus").append(orderStatusElements);
                $("#SelectOrderStatus").append(orderStatusElements);
            };
            RewardPointsHistory.GetGeneralSettings();           
            var str1 = generalSettings.AdditionOrderStatusID;
            str1 = str1.split(',');
            var obj = $('#ddlOrderStatus');
            for (var i in str1) {
                var val = str1[i];
                obj.find('option[value=' + val + ']').attr('selected', 1);
            }
            var str2 = generalSettings.SubtractOrderStatusID;
            str2 = str2.split(',');
            var obj = $('#SelectOrderStatus');
            for (var i in str2) {
                var val = str2[i];
                obj.find('option[value=' + val + ']').attr('selected', 1);
            }
        },
        OrderStatusError: function() {
            csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to load order satatus!") + "</p>");
        },
        GetGeneralSettings: function() {
            this.config.method = "GetGeneralSetting";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = RewardPointsHistory.BindGeneralSettings;
            this.config.error = RewardPointsHistory.GeneralSettingsError;
            this.ajaxCall(this.config);
        },
        BindGeneralSettings: function(msg) {
            var item;
            var length = msg.d.length;
            for (var index = 0; index < length; index++) {
                item = msg.d[index];
                $("#chkEnableRewardPoints").prop('checked', item.IsActive);
                $("#txtRewardPoints").val(item.RewardPoints);
                $("#txtRewardAmount").val(item.RewardExchangeRate);
                $("#txtRewardPointsExpiresInDays").val(item.RewardPointsExpiresInDays);
                $("#txtMinRedeemBalance").val(item.MinRedeemBalance)
                $("#txtCappedBalance").val(item.BalanceCapped)
                generalSettings.AdditionOrderStatusID = item.AddOrderStatusID;
                generalSettings.SubtractOrderStatusID = item.SubOrderStatusID;
            };
        },
        GeneralSettingsError: function() {
            csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to load previous setting values!") + "</p>");
        },
        SaveGeneralSettings: function() {
            var str1 = '';
            var str2 = '';
            var strStatusID1 = '';
            var strStatusID2 = '';
            generalSettingsObj.RewardPoints = $("#txtRewardPoints").val();
            generalSettingsObj.RewardExchangeRate = $("#txtRewardAmount").val();
            $("#ddlOrderStatus option:selected").each(function() {
                str1 += $(this).val() + ',';
                strStatusID1 = str1.substr(0, str1.length - 1)
            });
            var statusID1 = strStatusID1;
            generalSettingsObj.AddOrderStatusID = statusID1;

            $("#SelectOrderStatus option:selected").each(function() {
                str2 += $(this).val() + ',';
                strStatusID2 = str2.substr(0, str2.length - 1)
            });
            var statusID2 = strStatusID2;
            generalSettingsObj.SubOrderStatusID = statusID2;
            generalSettingsObj.IsActive = $('#chkEnableRewardPoints').prop('checked') ? true : false;
            generalSettingsObj.RewardPointsExpiresInDays = $("#txtRewardPointsExpiresInDays").val();
            generalSettingsObj.MinRedeemBalance = $("#txtMinRedeemBalance").val();
            generalSettingsObj.BalanceCapped = $("#txtCappedBalance").val();
            this.config.method = "RewardPointsSaveGeneralSettings";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ generalSettingobj: generalSettingsObj, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = RewardPointsHistory.SaveGeneralSettingsSuccess;
            this.config.error = RewardPointsHistory.SaveGeneralSettingsError;
            this.ajaxCall(this.config);
        },
        SaveGeneralSettingsSuccess: function() {
            csscody.info("<h2>" + getLocale(AspxRewardPoints, "Successful Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Setting has been saved successfully.") + "</p>");
            $('#fade, #popuprel2').fadeOut();
        },
        SaveGeneralSettingsError: function() {
            csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to save general settings for reward points!") + "</p>");
        }
    };

    RewardPointsHistory.Init();

});