<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuthorizeDotNetAIMSetting.ascx.cs"
    Inherits="Modules_PaymentGatewayManagement_AuthorizeDotNetAIMSetting" %>

<script type="text/javascript">
    //<![CDATA[

    var Setting = "";
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AIMAuthorize
        });
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName()
            };
            return aspxCommonInfo;
        };
        Setting = {
            LoadPaymentGatewaySetting: function (id, PopUpID) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var paymentGatewayId = id;
                var param = JSON2.stringify({ paymentGatewayID: paymentGatewayId, aspxCommonObj: aspxCommonInfo });
                $.ajax({
                    type: "POST",
                    url: '<%=AspxPaymentModulePath%>' + "AIMHandler.ashx/GetAllAuthorizedNetAIMSetting",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $.each(msg.d, function (index, item) {
                            $("#txtVersion").val(item.Version);
                            $("#txtApiLogin").val(item.APILoginID);
                            $("#txtTransactionKey").val(item.TransactionKey);
                            $("#chkIsTest").attr('checked', Boolean.parse(item.IsTestAIM));
                        });
                        ShowPopupControl(PopUpID);
                        $(".cssClassClose").click(function () {
                            $('#fade, #popuprel2').fadeOut();
                        });
                    },
                    error: function () {
                        alert(getLocale(AIMAuthorize, "Error!"));
                    }
                });
            },
            SaveUpdateAuthorizedDotNetSetting: function () {
                var paymentGatewaySettingValueID = 0;
                var paymentGatewayID = $("#hdnPaymentGatewayID").val();
                var userModuleId = 1;
                var settingKey = '';
                settingKey += 'Version' + '#' + 'APILoginID' + "#";
                settingKey += 'TranctionKey' + "#" + 'IsTestAIM';
                var settingValue = '';
                settingValue += $("#txtVersion").val() + "#" + $("#txtApiLogin").val() + "#";
                settingValue += $("#txtTransactionKey").val() + "#" + $("#chkIsTest").is(':checked');

                var isActive = $("#chkIsActive").attr('checked');
                var param = JSON2.stringify({ paymentGatewaySettingValueID: paymentGatewaySettingValueID, paymentGatewayID: paymentGatewayID, settingKeys: settingKey, settingValues: settingValue, isActive: isActive, aspxCommonObj: aspxCommonObj() });
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCoreHandler.ashx/AddUpdatePaymentGateWaySettings",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        csscody.info('<h2>' + getLocale(AIMAuthorize, 'Successful Message') + '</h2><p>' + getLocale(AIMAuthorize, 'Setting has been saved successfully.') + '</p>');
                        $('#fade, #popuprel2').fadeOut();
                    },
                    error: function () {
                        csscody.error('<h2>' + getLocale(AIMAuthorize, 'Error Message') + '</h2><p>' + getLocale(AIMAuthorize, 'Failed to save!') + '</p>');
                    }
                });
            },
            Init: function () {
                $("#divForLaterUseSettings").hide();
                $("#chkRelayResponse").attr("disabled", "disabled");
                $("#btnSavePaymentSetting").bind("click", function () {
                    Setting.SaveUpdateAuthorizedDotNetSetting();
                });
            }
        };
        Setting.Init();
    });


    //]]> 
</script>

<div class="cssClassCloseIcon">
    <button type="button" class="cssClassClose">
        <span class="sfLocale">Close</span></button>
</div>
<h2>
    <asp:Label ID="lblTitle" runat="server"
        Text="Authorize .Net AIM Setting Information"
        meta:resourcekey="lblTitleResource1"></asp:Label>
</h2>
<div class="sfFormwrapper">
    <div id="divForLaterUseAIMSettings">
    </div>

    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblVersionName" runat="server" Text="Version:"
                    CssClass="cssClassLabel" meta:resourcekey="lblVersionNameResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" id="txtVersion" class="sfInputbox">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApiLogin" runat="server" Text="API Login ID:"
                    CssClass="cssClassLabel" meta:resourcekey="lblApiLoginResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtApiLogin">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTransactionKey" runat="server" Text="Transaction Key:"
                    CssClass="cssClassLabel" meta:resourcekey="lblTransactionKeyResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtTransactionKey">
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblIsTest" runat="server" Text="Is Test:"
                    CssClass="cssClassLabel" meta:resourcekey="lblIsTestResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkIsTest" class="cssClassCheckBox" />
            </td>
        </tr>
    </table>
    <div class="sfButtonwrapper">
        <p>
            <button id="btnSavePaymentSetting" type="button" class="sfBtn">
                <span class="sfLocale icon-save">Save</span></button>
        </p>
    </div>
</div>
