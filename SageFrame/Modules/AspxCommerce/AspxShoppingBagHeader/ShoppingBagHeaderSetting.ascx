<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingBagHeaderSetting.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShoppingBagHeader_ShoppingBagHeaderSetting" %>
<script type="text/javascript">
   
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxShoppingBagHeader
        });
    });
    var umi = '<%=UserModuleID%>';
</script>

<div class="sfFormwrapper">
    <h3 class="sfLocale">
        Shopping Bag Settings</h3>
    <table border="0" width="100%" id="tblShoppingBagSettingsForm">
        <tr>
            <td>
                <asp:Label ID="lblShoppingBagType" runat="server" Text="Bag Type:" 
                    CssClass="sfFormlabel" meta:resourcekey="lblShoppingBagTypeResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <select id="ddlBagType" class="sfListmenu">
                    <option value="Slider" class="sfLocale">Slider</option>
                    <option value="Popup" class="sfLocale">Popup</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSaveShoppingBagSetting" class="sfLocale" value="Save" />
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">

    $(function() {

        $(".sfLocale").localize({
            moduleKey: AspxShoppingBagHeader
        });

        var ModuleServicePath = '<%=ModuleServicePath %>';
        var BagType = '<%=BagType %>';
        var storeId = '<%=aspxCommonObj.StoreID %>';
        var portalId = '<%=aspxCommonObj.PortalID %>';
        var cultureName = '<%=aspxCommonObj.CultureName %>';
        var aspxCommonObj = {
            StoreID: storeId,
            PortalID: portalId,
            CultureName: cultureName
        };
        $("#ddlBagType").val(BagType);
        $('#btnSaveShoppingBagSetting').click(function() {
            var bagType = $("#ddlBagType").val();
            $.ajax({
                type: 'post',
                async: false,
                url: ModuleServicePath + "AspxCommerceWebService.asmx/SetShoppingBagSetting",
                contentType: "application/json;charset=utf-8",
                data: JSON2.stringify({ bagType: bagType, aspxCommonObj: aspxCommonObj }),
                dataType: 'JSON',
                success: function() {
                    SageFrame.messaging.show(getLocale(AspxShoppingBagHeader,"Setting Saved Successfully"), "Success");
                },
                error: ''
            });
        });

    });
               
</script>

