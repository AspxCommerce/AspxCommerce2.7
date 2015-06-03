<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsCompareSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxItemsCompare_ItemsCompareSetting" %>
<div class="cssCompareItemSetting sfFormwrapper">
    <h3 class="sfLocale">Compare Items Settings</h3>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblCompareItemCount" runat="server" Text="Enter the Number of Products Displayed"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtCompareItemCount" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCompareItemDeatailPage" runat="server" Text="Compare Item Detail Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtCompareItemDetailPage" disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnCompareItemSettingSave" class="sfLocale sfbtn" value="Save" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxItemsCompare
        });
        $(this).compareSetting({
            compareItemsSettings: '<%=compareItemsSettings %>'
        });
    });
    //]]>
</script>