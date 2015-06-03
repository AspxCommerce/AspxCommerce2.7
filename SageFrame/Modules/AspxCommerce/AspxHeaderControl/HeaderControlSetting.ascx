<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControlSetting.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxHeaderControl_HeaderControlSetting" %>
<div class="sfFormwrapper">
    <h3 class="sfLocale">
        Header Settings</h3>
    <table border="0" width="100%" id="tblHeaderSettingsForm">
        <tr>
            <td>
                <asp:Label ID="lblHeaderType" runat="server" Text="Header Type:" 
                    CssClass="sfFormlabel" meta:resourcekey="lblHeaderTypeResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <select id="ddlHeaderType" class="sfListmenu">
                    <option value="Horizontal" class="sfLocale">Horizontal</option>
                    <option value="Dropdown" class="sfLocale">Dropdown</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSaveHeaderSetting" class="sfLocale" value="Save" />
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">

    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxHeaderControl
        });
        var ModuleServicePath = '<%=ModuleServicePath %>';
        var HeaderType = '<%=HeaderType %>';
        var storeId = '<%=aspxCommonObj.StoreID %>';
        var portalId = '<%=aspxCommonObj.PortalID %>';
        var cultureName = '<%=aspxCommonObj.CultureName %>';
        var aspxCommonObj = {
            StoreID: storeId,
            PortalID: portalId,
            CultureName: cultureName
        };
        $("#ddlHeaderType").val(HeaderType);
        $('#btnSaveHeaderSetting').click(function() {
            var headerType = $("#ddlHeaderType").val();
            $.ajax({
                type: 'post',
                async: false,
                url: ModuleServicePath + "AspxCommonHandler.ashx/SetHeaderSetting",
                contentType: "application/json;charset=utf-8",
                data: JSON2.stringify({ headerType: headerType, aspxCommonObj: aspxCommonObj }),
                dataType: 'JSON',
                success: function() {
                    SageFrame.messaging.show(getLocale(AspxHeaderControl, "Setting Saved Successfully"), "Success");
                },
                error: ''
            });
        });

    });
               
</script>

