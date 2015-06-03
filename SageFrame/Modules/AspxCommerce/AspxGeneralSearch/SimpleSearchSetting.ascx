<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SimpleSearchSetting.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxGeneralSearch_SimpleSearchSetting" %>
<div class="sfFormwrapper">
    <h3>
        <strong class="sfLocale">Search Settings</strong></h3>
    <table border="0" width="100%" id="tblSearchSettingsForm">
        <tr>
            <td>
                <asp:Label ID="lblShowCategoryForSearch" runat="server" Text="Show Category For Search:"
                    CssClass="sfFormlabel" meta:resourcekey="lblShowCategoryForSearchResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkShowCatForSearch" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEnableAdvanceSearch" runat="server" Text="Enable Advance Search:"
                    CssClass="sfFormlabel" meta:resourcekey="lblEnableAdvanceSearchResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkEnableAdvanceSearch" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblShowSearchKeyWords" runat="server" Text="Show Top Search Keywords:"
                    CssClass="sfFormlabel" meta:resourcekey="lblShowSearchKeyWordsResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkShowSearchKeyWord" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSaveSearchSetting" class="sfLocale" value="Save" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(this).SimpleSearchSettingInit({
            ModuleServicePath: '<%=ModuleServicePath.ToString() %>',
            ShowCategoryForSearch: '<%=ShowCategoryForSearch %>',
            EnableAdvanceSearch: '<%=EnableAdvanceSearch %>',
            ShowSearchKeyWord: '<%=ShowSearchKeyWord %>'
        });
    });
    //]]>
</script>