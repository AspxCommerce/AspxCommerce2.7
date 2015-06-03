<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItemsSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxWishList_WishItemsSetting" %>
<div>
    <h3 class="sfLocale">Wish Items module setting</h3>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEnableImageInWishList" runat="server" Text="Enable Image In Wish Items:"></asp:Label></td>
            <td>
                <input type="checkbox" id="chkEnableImageInWishList" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNoOfRecentWishItems" runat="server" Text="No Of Recents Wish Items:"></asp:Label></td>
            <td>
                <input type="text" id="txtNoOfRecentWishItems" disabled="disabled" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWishItemPageName" runat="server" Text="Wish Item Page Name:"></asp:Label></td>
            <td>
                <input type="text" id="txtWishItemsPageName" disabled="disabled" /></td>
        </tr>
        <tr>
            <td>
                <input type="button" class="sfLocale" id="btnWishItemsSettingSave" value="Save" /></td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxWishItems
        });
        $(this).WishItemsSetting({
            wishItemSetting: '<%=wishItemsSettings %>'
        });
    });
    //]]>
</script>