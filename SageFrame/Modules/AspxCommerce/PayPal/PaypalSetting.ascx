<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaypalSetting.ascx.cs"
    Inherits="Modules_PaymentGatewayManagement_PaypalSetting" %>

<script type="text/javascript">

    //<![CDATA[
    var Setting = "";
    $(function() {
        $(".sfLocale").localize({
            moduleKey: PayPal
        });
        var umi = '<%=UserModuleID%>';
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var userIP = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var aspxCommonObj = {
            StoreID: storeId,
            PortalID: portalId,
            UserName: userName
        }
        Setting = {
            LoadPaymentGatewaySetting: function(id, PopUpID) {
                var paymentGatewayId = id;
                var param = JSON2.stringify({ paymentGatewayID: paymentGatewayId, storeId: storeId, portalId: portalId });
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: '<%=AspxPaymentModulePath%>' + "Services/PayPalWebService.asmx/GetAllPayPalSetting",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        var item;
                        var length = msg.d.length;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            $("#txtReturnUrl").val(item.ReturnUrl);
                            $("#txtCancelUrl").val(item.CancelUrl);
                            $("#txtBusinessAccount").val(item.BusinessAccount);
                            $("#txtVerificationUrl").val(item.VerificationUrl);
                            $("#txtAuthenticationToken").val(item.AuthToken);
                            $("#chkIsTest").attr('checked', Boolean.parse(item.IsTestPaypal));
                        };
                        ShowPopupControl(PopUpID);
                        $(".cssClassClose").click(function() {
                            $('#fade, #popuprel2').fadeOut();
                        });
                    },
                    error: function() {
                        csscody.error('<h2>'+getLocale(PayPal,'Error Message')+'</h2><p>'+getLocale(PayPal,'Failed to load')+'</p>');
                    }
                });
                $("#btnSavePaypalSetting").bind("click", function() {
                    Setting.SaveUpdatePayPalSetting();
                });
            },
            SaveUpdatePayPalSetting: function() {
                var paymentGatewaySettingValueID = 0;
                var paymentGatewayID = $("#hdnPaymentGatewayID").val();

                var settingKey = '';
                settingKey += 'ReturnUrl' + "#" + 'CancelUrl' + "#" + 'BusinessAccount' + "#" + 'VerificationUrl' + "#" + 'AuthToken' + "#" + 'IsTestPaypal';
                var settingValue = '';
                settingValue += $("#txtReturnUrl").val() + '#' + $("#txtCancelUrl").val() + "#" + $("#txtBusinessAccount").val() + "#" + $("#txtVerificationUrl").val() + "#" + $("#txtAuthenticationToken").val() + "#" + $("#chkIsTest").is(':checked');
                var isActive = true;
                var param = JSON2.stringify({ paymentGatewaySettingValueID: paymentGatewaySettingValueID, paymentGatewayID: paymentGatewayID, settingKeys: settingKey, settingValues: settingValue, isActive: isActive, aspxCommonObj: aspxCommonObj });
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: aspxservicePath + "AspxCoreHandler.ashx/AddUpdatePaymentGateWaySettings",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function() {
                        csscody.info('<h2>'+getLocale(PayPal,'Successful Message')+'</h2><p>'+getLocale(PayPal,'Setting has been saved successfully.')+'</p>');
                        $('#fade, #popuprel2').fadeOut();
                    },
                    error: function() {
                        csscody.error('<h2>'+getLocale(PayPal,'Error Message')+'</h2><p>'+getLocale(PayPal,'Failed to save!')+'</p>');
                    }
                });
            }
        };
    });


    //]]>
</script>

<div class="cssClassCloseIcon">
    <button type="button" class="cssClassClose">
        <span class="sfLocale">Close</span></button>
</div>
<h2>
    <asp:Label ID="lblTitle" runat="server" Text="Paypal Setting Information" meta:resourcekey="lblTitleResource1"></asp:Label>
</h2>
<div class="sfFormwrapper">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblReturnUrl" runat="server" Text="Return Url:" CssClass="cssClassLabel "
                    meta:resourcekey="lblReturnUrlResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" id="txtReturnUrl" class="sfInputbox">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCancelUrl" runat="server" Text="Cancel Url:" CssClass="cssClassLabel "
                    meta:resourcekey="lblCancelUrlResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtCancelUrl">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBusinessAccount" runat="server" Text="Business Account:" CssClass="cssClassLabel "
                    meta:resourcekey="lblBusinessAccountResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtBusinessAccount">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblVerificationUrl" runat="server" Text="Verification Url:" CssClass="cssClassLabel "
                    meta:resourcekey="lblVerificationUrlResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtVerificationUrl">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAuthenticationToken" runat="server" Text="Authentication Token:"
                    CssClass="cssClassLabel " meta:resourcekey="lblAuthenticationTokenResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="sfInputbox" id="txtAuthenticationToken">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIsTest" runat="server" Text="Is Test:" CssClass="cssClassLabel "
                    meta:resourcekey="lblIsTestResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkIsTest" class="cssClassCheckBox " />
            </td>
        </tr>
    </table>
    <div class="sfButtonwrapper">
        <p>
            <button id="btnSavePaypalSetting" type="button" class="sfBtn">
                <span class="sfLocale icon-save">Save</span></button>
        </p>
    </div>
</div>
