<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularTagsSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxPopularTags_PopularTagsSetting" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxPopularTags
        });
        $(this).PopularTag({
            PopularTagsModulePath: "<%=PopularTagModulePath %>"
        });
    });
</script>
<div class="cssPopularTagSetting sfFormwrapper">
 <h3 class="sfLocale">
                Popular Tags Settings</h3>
    <table>        
         <tr>
            <td>
                <asp:Label ID="lblEnablePopularTag" runat="server" Text="Enable PopularTag:"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnablePopularTag" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPopularTagCount" runat="server" Text="Number of Popular Tags Displayed:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtPopularTagCount" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPopularTagInARow" runat="server" Text="Number of Products Dispalyed In Row:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtPopularTagInARow" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblShowPopularTagRss" runat="server" Text="Enable Rss:"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnablePopularTagRss" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPopularTagRssCount" runat="server" Text="Number of Rss To Show:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtPopularTagRssCount" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblPopularTagsRssPageName" runat="server" Text="Popular Tags RssFeed Page Name:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtPopularTagRssPageName" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblViewAllTagsPageName" runat="server" Text="View All Tags Page Name:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtViewAllTagsPageName" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblViewTaggedItemPageName" runat="server" Text="View Tagged Item Page Name:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtViewTaggedItemPageName" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnPopularTagSettingSave" class="sfLocale sfbtn" value="Save" />
            </td>
        </tr>
    </table>
</div>