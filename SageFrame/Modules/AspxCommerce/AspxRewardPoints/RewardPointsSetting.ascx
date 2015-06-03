<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RewardPointsSetting.ascx.cs"
    Inherits="Modules_AspxCommerce_RewardPoints_RewardPointsSetting" %>
<script type="text/javascript" language="javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxRewardPoints
        });
    });
    var RewardPointsModulePath = '<%=AspxRewardPointsModulePath%>';
    var RewardPointsSetting;
    $(function () {
        var umi = '<%=UserModuleID%>';
        var $accor = '';
        var arrRewardRuleIDs = [];
        var modulePath = RewardPointsModulePath;
        var ModuleServicePath = modulePath + "Services/RewardPointsWebService.asmx/";
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var rewardPointsCommonObj = {
            RewardPointSettingsID: "",
            RewardRuleName: "",
            RewardRuleID: "",
            RewardRuleType: "",
            RewardPoints: "",
            PurchaseAmount: "",
            IsActive: true
        };

        RewardPointsSetting = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: "",
                error: "",
                sessionValue: ""
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: RewardPointsSetting.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: RewardPointsSetting.config.contentType,
                    cache: RewardPointsSetting.config.cache,
                    async: RewardPointsSetting.config.async,
                    url: RewardPointsSetting.config.url,
                    data: RewardPointsSetting.config.data,
                    dataType: RewardPointsSetting.config.dataType,
                    success: RewardPointsSetting.config.ajaxCallMode,
                    error: RewardPointsSetting.config.error
                });
            },
            Init: function () {
                RewardPointsSetting.RewardRule();
                RewardPointsSetting.HideAll();
                $("#dvAddNewRewardPoints").show();
                RewardPointsSetting.RewardPointsRuleListBind();
                var frm = $("#form2").validate({
                    rules: {
                        RewardRuleNm: {
                            required: '*'
                        },
                        purchase: { required: true, number: true },
                        RwrdPoints: {
                            required: true, number: true,
                            maxlength: 5
                        },
                        RewardRule: {
                            required: true
                        }
                    },
                    messages: {
                        RewardRuleNm: {
                            required: '*'
                        },
                        purchase: { required: '*' },
                        RwrdPoints: {
                            required: '*', number: 'Please enter a valid number',
                            maxlength: getLocale(AspxRewardPoints, "* (no more than 5 digits)")
                        },
                        RewardRule: {
                            required: '*'
                        }
                    }, ignore: ":hidden"
                });
                $("#selectRewardRule").change(function () {
                    var val = $("#selectRewardRule option:selected").val();
                    if (val == 3 || val == 8 || val == 9) {
                        $("#trAmount").show();
                        $("#spanCurrencySymbol").html(curSymbol);
                    }
                    else {
                        $("#trAmount").hide();
                    }
                });

                $("#btnSaveNewRewardPoints").bind("click", function () {
                    var ddlValue = $('#selectRewardRule option:selected').val();
                    var arr = arrRewardRuleIDs;
                    if (arr == null) {
                        arr = 0;
                    }
                    if (arr.indexOf(ddlValue) < 0) {
                        if ($('#selectRewardRule option:selected').val() == 0) {
                            csscody.alert("<h2>" + getLocale(AspxRewardPoints, "Information Alert") + "</h2><p>" + getLocale(AspxRewardPoints, "Please Select reward rule type!") + "</p>");
                        }
                        else if (frm.form()) {
                            RewardPointsSetting.SaveNewRewardPoints();
                            RewardPointsSetting.RewardRule();
                            RewardPointsSetting.ClearAll();
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        csscody.alert("<h2>" + getLocale(AspxRewardPoints, "Information Alert") + "</h2><p>" + getLocale(AspxRewardPoints, "Selected reward rule type already exists.!") + "</p>");
                    }

                });
                $("#btnBack").bind("click", function () {
                    RewardPointsSetting.HideAll();
                    RewardPointsHistory.GoBackToHistoryPage();
                });

            },
            RewardRule: function () {
                rewardPointsCommonObj.RewardRuleName = null;
                rewardPointsCommonObj.IsActive = null;
                var offset = 1;
                var limit = 100;

                this.config.method = "RewardPointsSettingGetAll";
                this.config.url = ModuleServicePath + this.config.method;
                this.config.data = JSON2.stringify({ offset: offset, limit: limit, aspxCommonObj: aspxCommonObj, rewardPointsCommonObj: rewardPointsCommonObj });
                this.config.ajaxCallMode = RewardPointsSetting.RewardRuleBind;
                this.config.error = RewardPointsSetting.RewardRuleError;
                this.ajaxCall(this.config);
            },
            RewardRuleBind: function (msg) {
                if (msg.d) {
                    $.each(msg.d, function (index, item) {
                        arrRewardRuleIDs += item.RewardRuleID + ",";
                    });
                }
            },
            RewardRuleError: function () {
                csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to bind reward rules!") + "</p>");
            },
            HideAll: function () {
                $("#dvAddNewRewardPoints").hide();
            },
            ClearAll: function () {
                $("#txtRewardRuleName").val('');
                $("#txtRwrdPoints").val('');
                $("#txtPurchaseAmount").val('');
            },
            LoadControl: function (controlName) {
                $.ajax({
                    type: "POST",
                    url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $('#divLoadUserControl').html(response.d);
                    },
                    error: function () {
                        csscody.error('<h2>' + getLocale(AspxRewardPoints, 'Error Message') + '</h2><p>' + getLocale(AspxRewardPoints, 'Failed to load control!.') + '</p>');
                    }
                });
            },
            RewardPointsRuleListBind: function () {
                $("#ddlOrderStatus").empty();
                $("#SelectOrderStatus").empty();
                this.config.method = "RewardPointsRuleListBind";
                this.config.url = ModuleServicePath + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = RewardPointsSetting.RewardPointsRuleListBindStatus;
                this.config.error = RewardPointsSetting.RewardPointsRuleListBindError;
                this.ajaxCall(this.config);
            },
            RewardPointsRuleListBindStatus: function (msg) {
                $.each(msg.d, function (index, item) {
                    var rewardRuleElements = "<option value=" + item.RewardRuleID + ">" + item.RewardRuleType + "</option>";
                    $("#selectRewardRule").append(rewardRuleElements);
                });
            },
            RewardPointsRuleListBindError: function () {
                csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to load order satatus!") + "</p>");
            },
            SaveNewRewardPoints: function () {
                var txtPoints = $("#txtPurchaseAmount").val();
                if (txtPoints == '' || txtPoints == null) {
                    txtPoints = 0;
                }
                rewardPointsCommonObj.RewardRuleName = $("#txtRewardRuleName").val();
                rewardPointsCommonObj.RewardRuleID = $("#selectRewardRule option:selected").val();
                rewardPointsCommonObj.RewardRuleType = $("#selectRewardRule option:selected").text();
                rewardPointsCommonObj.RewardPoints = $("#txtRwrdPoints").val();
                rewardPointsCommonObj.PurchaseAmount = txtPoints;
                rewardPointsCommonObj.IsActive = $('#chkIsActiveRule').prop('checked') ? true : false;

                this.config.method = "RewardPointsSaveUpdateNewRule";
                this.config.url = ModuleServicePath + this.config.method;
                this.config.async = false;
                this.config.data = JSON2.stringify({ rewardPointsCommonObj: rewardPointsCommonObj, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = RewardPointsSetting.SaveNewRewardPointsSuccess;
                this.config.error = RewardPointsSetting.SaveNewRewardPointsError;
                this.ajaxCall(this.config);
            },
            SaveNewRewardPointsSuccess: function () {
                RewardPointsSetting.HideAll();
                RewardPointsHistory.GoBackToHistoryPage();
                csscody.info("<h2>" + getLocale(AspxRewardPoints, "Successful Message") + "</h2><p>" + getLocale(AspxRewardPoints, "New reward points rule saved successfully.") + "</p>");
                $('#fade, #popuprel2').fadeOut();
            },
            SaveNewRewardPointsError: function () {
                csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to save new reward points rule!") + "</p>");
            }

        };

        RewardPointsSetting.Init();

    });
    //]]>
</script>
<form id="form2" action="RewardPointsSetting.ascx">
<div id="dvAddNewRewardPoints" style="display: none;">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h3>
                <asp:Label ID="lblNewRewardPoint" runat="server" Text="Add New Reward Rule :" meta:resourcekey="lblNewRewardPointResource1"></asp:Label>
            </h3>
        </div>
        <div class="sfGridwrapper sfFormwrapper">
            <div class="sfGridWrapperContent">
                <table id="tblNewEwardPoint" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label id="lblRewardRuleName" class="sfFormlabel sfLocale">
                                Reward Rule Name :</label>
                        </td>
                        <td>
                            <input type="text" id="txtRewardRuleName" name="RewardRuleNm" class="sfInputbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblRewardRuleType" class="sfFormlabel sfLocale">
                                Reward Rule Type :</label>
                        </td>
                        <td>
                            <select id="selectRewardRule" name="RewardRule" class="sfListmenu">
                                <option value="0" class="sfLocale">--Select Reward Rule--</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblRewardPoints" class="sfFormlabel sfLocale">
                                Reward Points :</label>
                        </td>
                        <td>
                            <input type="text" id="txtRwrdPoints" name="RwrdPoints" class="sfInputbox" datatype="Integer"
                                maxlength="5" />
                        </td>
                    </tr>
                    <tr id="trAmount" style="display: none;">
                        <td>
                            <label id="lblOnPurchaseAmount" class="sfFormlabel sfLocale">
                                On Purchase Amount ( <span id="spanCurrencySymbol"></span>):</label>
                        </td>
                        <td>
                            <input type="text" id="txtPurchaseAmount" name="purchase" class="sfInputbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblIsActive" class="sfFormlabel sfLocale">
                                Active :</label>
                        </td>
                        <td>
                            <input type="checkbox" id="chkIsActiveRule" name="IsActiveRule" class="cssClassCheckBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <div class="sfButtonwrapper ">
                                <button id="btnBack" type="button" class="sfBtn">
                                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                                <button id="btnSaveNewRewardPoints" type="button" class="sfBtn">
                                    <span class="sfLocale icon-save">Save Reward Rule</span></button>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divLoadUserControl" class="cssClasMyAccountInformation">
    <div class="cssClassMyDashBoardInformation">
    </div>
</div>
</form>
