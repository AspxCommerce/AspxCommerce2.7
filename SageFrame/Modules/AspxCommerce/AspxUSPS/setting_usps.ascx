<%@ Control Language="C#" AutoEventWireup="true" CodeFile="setting_usps.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxFedex_ctl_setting_usps" %>
<script type="text/javascript">

    var uspsServicePath = '<%=PathUsps %>';
    var providerServicePath = uspsServicePath + 'WebService/USPSWebService.asmx/RollBack';


    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxUSPS
        });
        var umi = '<%=UserModuleID%>';
        var newProviderId = 0;
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var setting = function () {
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
                    url: uspsServicePath + 'WebService/USPSWebService.asmx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            var aspxCommonObj = function () {
                var aspxCommonInfo = {
                    StoreID: AspxCommerce.utils.GetStoreID(),
                    PortalID: AspxCommerce.utils.GetPortalID(),
                    UserName: AspxCommerce.utils.GetUserName(),
                    CultureName: AspxCommerce.utils.GetCultureName(),
                    SessionCode: AspxCommerce.utils.GetSessionCode(),
                    CustomerID: AspxCommerce.utils.GetCustomerID()
                };
                return aspxCommonInfo;
            };
            var saveUspsSetting = function () {
                var v = $("#form1").validate({
                    messages: {
                        userid: {
                            required: '*'
                        },
                        minweight: {
                            required: '*'
                        },
                        maxweight: {
                            required: '*'
                        },
                        rateurl: {
                            required: '*'
                        },
                        shipmenturl: {
                            required: '*'
                        },
                        trackurl: {
                            required: '*'
                        }
                    },
                    rules:
                        {
                            userid: { required: true },
                            minweight: { required: true, number: true },
                            maxweight: { required: true, number: true },
                            rateurl: { required: true },
                            shipmenturl: { required: true },
                            trackurl: { required: true }
                        },
                    ignore: ":hidden"
                });
                if (v.form()) {
                    var settingKey = "UspsUserId#UspsMinWeight#UspsMaxWeight#UspsWeightUnit#UspsRateApiUrl#UspsShipmentApiUrl#UspsTrackApiUrl#UspsTestMode";
                    var settingValue = $.trim($("#txtUserId").val()) + "#" +
                        $.trim($("#txtMinWeight").val()) + "#" +
                            $.trim($("#txtMaxWeight").val()) + "#" +
                                $.trim($("#txtWeightUnit").val()) + "#" +
                                    $.trim($("#txtRateApiUrl").val()) + "#" +
                                        $.trim($("#txtShipmentApiUrl").val()) + "#" +
                                            $.trim($("#txtTrackApiUrl").val()) + "#" +
                                                $("#ckbTestMode").is(":checked");
                    var param2 = JSON2.stringify({
                        providerId: shippingProviderMgmt.GetSettingId(),
                        settingKey: settingKey,
                        settingValue: settingValue,
                        commonInfo: aspxCommonObj()
                    });
                    $ajaxCall("SaveUpdateUspsSetting", param2, function (data) {
                        $("#fade,.popupbox").fadeOut();
                        csscody.info('<h2>' + getLocale(AspxUSPS, 'Successful Message') + '</h2><p>' + getLocale(AspxUSPS, 'Provider setting has been saved successfully.') + '</p>');
                    }, null);
                }


            };

            var loadSetting = function (providerid) {
                var param = JSON2.stringify({
                    providerId: providerid,
                    commonInfo: aspxCommonObj()
                });
                $ajaxCall("GetUSPSSetting", param, function (data) {
                    if (data.d != null && data.d != "") {
                        var info = data.d;
                        $("#txtUserId").val(info.UspsUserId);
                        $("#txtMinWeight").val(info.UspsMinWeight);
                        $("#txtMaxWeight").val(info.UspsMaxWeight);
                        $("#txtWeightUnit").val(info.UspsWeightUnit);
                        $("#txtRateApiUrl").val(info.UspsRateApiUrl);
                        $("#txtShipmentApiUrl").val(info.UspsShipmentApiUrl);
                        $("#txtTrackApiUrl").val(info.UspsTrackApiUrl);
                        info.UspsTestMode ? $("#ckbTestMode").attr('checked', 'checked') : $("#ckbTestMode").removeAttr('checked');
                    }
                }, null);
            };
            var init = function () {
                if (shippingProviderMgmt.GetSettingId() != 0) {
                    loadSetting(shippingProviderMgmt.GetSettingId());
                }
                $("#btnSaveUspsSetting").unbind().bind("click", function () {
                    saveUspsSetting();
                });
            }();


        }();

    });

</script>

<div class="cssClassCloseIcon">
    <button type="button" class="cssClassClose">
        <span class="sfLocale">Close</span></button>
</div>
<div id="dvSetting" style="width: 500px;">

    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblShippingProvider" runat="server" Text="USPS Setting"
                    meta:resourcekey="lblShippingProviderResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblUserId" runat="server" CssClass="cssClassLabel"
                            Text="UserId:" meta:resourcekey="lblUserIdResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtUserId" class="cssClassTextBox" name="userid" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMinWeight" runat="server" CssClass="cssClassLabel"
                            Text="Min. Weight:" meta:resourcekey="lblMinWeightResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMinWeight" class="cssClassTextBox" name="minweight" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMaxWeight" runat="server" CssClass="cssClassLabel"
                            Text="Max. Weight:" meta:resourcekey="lblMaxWeightResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMaxWeight" class="cssClassTextBox" name="maxweight" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblWeightUnit" runat="server" CssClass="cssClassLabel"
                            Text="Max. Weight:" meta:resourcekey="lblWeightUnitResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtWeightUnit" class="cssClassTextBox" name="weightUnit" disabled="disabled" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblRateApiUrl" runat="server" CssClass="cssClassLabel"
                            Text="Rate Api Url:" meta:resourcekey="lblRateApiUrlResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtRateApiUrl" class="cssClassTextBox" name="rateurl" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShipmentUrl" runat="server" CssClass="cssClassLabel"
                            Text="Shipment Api Url:" meta:resourcekey="lblShipmentUrlResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtShipmentApiUrl" class="cssClassTextBox" name="shipment" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTrackApiUrl" runat="server" CssClass="cssClassLabel"
                            Text="Track Api Url:" meta:resourcekey="lblTrackApiUrlResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtTrackApiUrl" class="cssClassTextBox" name="trackurl" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTestMode" runat="server" CssClass="cssClassLabel"
                            Text="Test Mode:" meta:resourcekey="lblTestModeResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="ckbTestMode" class="cssClassCheckBox" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="sfButtonwrapper cssClassPaddingNone">
                            <p>
                                <button type="button" id="btnSaveUspsSetting" class="sfBtn">
                                    <span class="sfLocale icon-save">Save</span></button>
                            </p>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>



