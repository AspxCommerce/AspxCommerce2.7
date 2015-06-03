<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SageSearchExtensions.ascx.cs"
    Inherits="Modules_SageSearch_ctl_SageSearchExtensions" %>
<h2>
    <asp:Label ID="lblSageSearchExtensionManagement" runat="server" Text="Search Extension Management"></asp:Label>
</h2>
<div class="sfButtonwrapper" id="actionWrapper" runat="server">
    <asp:ImageButton ID="imbAddNew" runat="server" CausesValidation="False" OnClick="imbAddNew_Click" />
    <asp:Label ID="lblAddNew" runat="server" Text="Add New" AssociatedControlID="imbAddNew"
        Style="cursor: pointer;"></asp:Label>
</div>
<div class="cssClassGridWrapper" id="gdvWrapper" runat="server">
    <asp:GridView Width="100%" runat="server" ID="gdvList" AutoGenerateColumns="False"
        AllowPaging="True" EmptyDataText=".........No Extensions found........." OnPageIndexChanging="gdvList_PageIndexChanging"
        OnRowCommand="gdvList_RowCommand" OnRowDataBound="gdvList_RowDataBound" OnRowDeleting="gdvList_RowDeleting"
        OnRowEditing="gdvList_RowEditing" OnRowUpdating="gdvList_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUsername" runat="server" CommandArgument='<%# Eval("SageFrameSearchProcedureID")%>'
                        CommandName="Edit" Text='<%# Eval("SageFrameSearchTitle")%>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Procedure Name">
                <ItemTemplate>
                    <asp:Label ID="lblSageFrameSearchProcedureName" runat="server" Text='<%# Eval("SageFrameSearchProcedureName")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Execute As">
                <ItemTemplate>
                    <asp:Label ID="lblSageFrameSearchProcedureExecuteAs" runat="server" Text='<%# Eval("SageFrameSearchProcedureExecuteAs")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UpdatedOn">
                <ItemTemplate>
                    <asp:Label ID="lblNewsDate" runat="server" Text='<%# Eval("UpdatedOn")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("SageFrameSearchProcedureID")%>'
                        CommandName="Edit" ToolTip="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnEdit" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("SageFrameSearchProcedureID") %>'
                        CommandName="Delete" ToolTip="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="cssClassHeadingOne" />
        <RowStyle CssClass="cssClassAlternativeOdd" />
        <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
    </asp:GridView>
</div>
<div id="frmWrapper" runat="server">
    <div class="sfFormwrapper">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="20%">
                    <asp:Label ID="lblSageFrameSearchTitle" runat="server" Text="Title:" CssClass="sfFormlabel"
                        ToolTip="Fill the tile for your search"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSageFrameSearchTitle" runat="server" ValidationGroup="SearchExtensionValidation"
                        CssClass="sfInputbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="SearchExtensionValidation"
                        ControlToValidate="txtSageFrameSearchTitle" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSageFrameSearchProcedureName" runat="server" Text="Procedure Name:"
                        CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSageFrameSearchProcedureName" runat="server" ValidationGroup="SearchExtensionValidation"
                        CssClass="sfInputbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="SearchExtensionValidation"
                        ControlToValidate="txtSageFrameSearchProcedureName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSageFrameSearchProcedureExecuteAs" runat="server" Text="Execute As:"
                        CssClass="sfFormlabel" ToolTip="like dbo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSageFrameSearchProcedureExecuteAs" ValidationGroup="SearchExtensionValidation"
                        Text="dbo" runat="server" CssClass="sfInputbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="SearchExtensionValidation"
                        ControlToValidate="txtSageFrameSearchProcedureExecuteAs" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <div class="sfButtonwrapper">
        <asp:ImageButton ID="imbSave" runat="server" ValidationGroup="SearchExtensionValidation"
            OnClick="imbSave_Click" />
        <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="imbSave"
            Style="cursor: pointer;"></asp:Label>
        <asp:ImageButton ID="imbCancel" runat="server" OnClick="imbCancel_Click" />
        <asp:Label ID="lblCancel" runat="server" Text="Cancel" AssociatedControlID="imbCancel"
            Style="cursor: pointer;"></asp:Label>
    </div>
</div>
