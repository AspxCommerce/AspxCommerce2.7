<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ManageCss.ascx.cs"
    Inherits="SageFrame.Modules.Admin.CSSManagement.ctl_ManageCss" %>
<h1>
    <asp:Label ID="lblCSSManagement" runat="server" Text="CSS Management" meta:resourcekey="lblCSSManagementResource1"></asp:Label>
</h1>
<div class="sfFormwrapper sfPadding">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td width="10%">
                <asp:Label ID="lblCSSFile" runat="server" Text="CSS File" CssClass="sfFormlabel"
                    meta:resourcekey="lblCSSFileResource1"></asp:Label>
            </td>
            <td width="5%">
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlCssFileList" runat="server" CssClass="sfListmenu" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlCssFileList_SelectedIndexChanged" ValidationGroup="ValidateCssManagement"
                    meta:resourcekey="ddlCssFileListResource1">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCssFileList"
                    InitialValue="-1" ValidationGroup="ValidateCssManagement" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblFileContent" runat="server" Text="File Content" CssClass="sfFormlabel"
                    meta:resourcekey="lblFileContentResource1"></asp:Label>
            </td>
            <td valign="top">
                :
            </td>
            <td>
                <asp:TextBox ID="txtFileContent" runat="server" TextMode="MultiLine" Columns="200"
                    Rows="15" CssClass="sfTextarea sfFullwidth sfCSS" ValidationGroup="ValidateCssManagement"
                    meta:resourcekey="txtFileContentResource1" Height="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="ValidateCssManagement" ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="txtFileContent" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
<div class="sfButtonwrapper">
    <asp:ImageButton ID="imbSave" runat="server" ValidationGroup="ValidateCssManagement"
        OnClick="imbSave_Click" ToolTip="Save" meta:resourcekey="imbSaveResource1" />
    <asp:Label ID="lblSave" runat="server" AssociatedControlID="imbSave" Text="Save"
        meta:resourcekey="lblSaveResource1"></asp:Label>
    <asp:ImageButton ID="imbRefresh" runat="server" ToolTip="Refresh" OnClick="imbRefresh_Click"
        meta:resourcekey="imbRefreshResource1" />
    <asp:Label ID="lblRefresh" runat="server" AssociatedControlID="imbRefresh" Text="Refresh"
        meta:resourcekey="lblRefreshResource1"></asp:Label>
    <div class="cssClassclear">
    </div>
</div>
