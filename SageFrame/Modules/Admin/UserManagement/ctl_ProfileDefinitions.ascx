<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ProfileDefinitions.ascx.cs"
    Inherits="SageFrame.Modules.Admin.UserManagement.ctl_ProfileDefinitions" %>
<asp:UpdatePanel ID="udpProDef" runat="server">
    <ContentTemplate>
        <h1>
            <asp:Label ID="lblProfileDefinition" runat="server" Text="User Profile Definition"
                meta:resourcekey="lblProfileDefinitionResource1"></asp:Label>
        </h1>
        <div id="divGridViewWrapper" runat="server">
            <div class="sfGridwrapper">
                <asp:GridView ID="gdvList" runat="server" AutoGenerateColumns="False" CssClass="tablestyle"
                    GridLines="None" EmptyDataText="No Record to Show..." Width="100%" AllowPaging="True"
                    PageSize="15" OnPageIndexChanging="gdvList_PageIndexChanging" OnRowCommand="gdvList_RowCommand"
                    OnRowDataBound="gdvList_RowDataBound" OnRowDeleting="gdvList_RowDeleting" OnRowEditing="gdvList_RowEditing"
                    OnRowUpdating="gdvList_RowUpdating" meta:resourcekey="gdvListResource1">
                    <Columns>
                        <asp:TemplateField HeaderText="Caption" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUsername" runat="server" CommandArgument='<%# Eval("ProfileID") %>'
                                    CommandName="Edit" Text='<%# Eval("Name") %>' meta:resourcekey="lnkUsernameResource1"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" meta:resourcekey="TemplateFieldResource2">
                            <ItemTemplate>
                                <asp:Label ID="lblPropertyTypeName" runat="server" Text='<%# Eval("PropertyTypeName") %>'
                                    meta:resourcekey="lblPropertyTypeNameResource1"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IsActive" meta:resourcekey="TemplateFieldResource3">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# (Eval("IsActive")) %>'
                                    class="sfCheckbox" meta:resourcekey="chkIsActiveResource1" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" CssClass="sfIsactive" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="AddedOn" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Added On"
                            meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle CssClass="sfAddedon" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UpdatedOn" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Updated On"
                            meta:resourcekey="BoundFieldResource2">
                            <HeaderStyle CssClass="sfUpdate" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource4">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgUp" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DisplayOrder") %>'
                                        CommandName="Up" ImageUrl='<%# GetTemplateImageUrl("imgup.png", true) %>' ToolTip="Up"
                                        meta:resourcekey="imgUpResource1" />
                                </div>
                                <div>
                                    <asp:ImageButton ID="imgDown" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DisplayOrder") %>'
                                        CommandName="Down" ImageUrl='<%# GetTemplateImageUrl("imgdown.png", true) %>'
                                        ToolTip="Down" meta:resourcekey="imgDownResource1" />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle CssClass="sfOrder" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource5">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProfileID") %>'
                                    CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                                    ToolTip="Edit" meta:resourcekey="btnEditResource1" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="sfEdit" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource6">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProfileID") %>'
                                    CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                    ToolTip="Delete" meta:resourcekey="imgDeleteResource1" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="sfDelete" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="sfOdd" />
                    <AlternatingRowStyle CssClass="sfEven" />
                </asp:GridView>
            </div>
            <div class="sfButtonwrapper">
                <asp:ImageButton ID="imbAddNew" ToolTip="Add New" runat="server" OnClick="imbAddNew_Click"
                    meta:resourcekey="imbAddNewResource1" />
                <asp:Label ID="lblAddNew" runat="server" Style="cursor: pointer" AssociatedControlID="imbAddNew"
                    Text="Add New" meta:resourcekey="lblAddNewResource1"></asp:Label>
                <asp:ImageButton ID="imbSaveChanges" ToolTip="Save Changes" runat="server" OnClick="imbSaveChanges_Click"
                    meta:resourcekey="imbSaveChangesResource1" />
                <asp:Label ID="lblSaveChanges" AssociatedControlID="imbSaveChanges" Style="cursor: pointer"
                    runat="server" Text="Save Changes" meta:resourcekey="lblSaveChangesResource1"></asp:Label>
                <asp:ImageButton ID="imbRefresh" ToolTip="Refresh" runat="server" OnClick="imbRefresh_Click"
                    meta:resourcekey="imbRefreshResource1" />
                <asp:Label ID="lblRefresh" runat="server" Style="cursor: pointer" AssociatedControlID="imbRefresh"
                    Text="Refresh" meta:resourcekey="lblRefreshResource1"></asp:Label>
            </div>
        </div>
        <div runat="server" id="divForm">
            <div class="sfFormwrapper sfPadding">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="20%">
                            <asp:Label ID="lblCaption" runat="server" Text="Caption :" CssClass="sfFormlabel"
                                meta:resourcekey="lblCaptionResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCaption" runat="server" MaxLength="50" CssClass="sfInputbox"
                                meta:resourcekey="txtCaptionResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPropertyType" runat="server" Text="Property Type :" CssClass="sfFormlabel"
                                meta:resourcekey="lblPropertyTypeResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPropertyType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPropertyType_SelectedIndexChanged"
                                CssClass="sfListmenu" meta:resourcekey="ddlPropertyTypeResource1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDataType" runat="server" Text="Data Type :" CssClass="sfFormlabel"
                                meta:resourcekey="lblDataTypeResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDataType" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlDataTypeResource1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIsRequired" runat="server" Text="Is Required :" CssClass="sfFormlabel"
                                meta:resourcekey="lblIsRequiredResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsRequred" runat="server" CssClass="sfCheckbox" meta:resourcekey="chkIsRequredResource1" />
                        </td>
                    </tr>
                    <tr id="trListPropertyValue" runat="server">
                        <td width="20%" valign="top" runat="server">
                            <asp:Label ID="lblPropertyValue" runat="server" Text="Value :" CssClass="sfFormlabel"></asp:Label>
                        </td>
                        <td runat="server">
                            <table class="cssTableRowFormTable">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPropertyValue" runat="server" CssClass="sfInputbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="sfButtonwrapper">
                                            <asp:ImageButton ToolTip="Add" ID="imbAdd" runat="server" OnClick="imbAdd_Click" />
                                            <asp:Label ID="lblAdd" runat="server" Style="cursor: pointer" AssociatedControlID="imbAdd"
                                                Text="Add"></asp:Label>
                                            <asp:ImageButton ToolTip="Delete" ID="imbDelete" runat="server" OnClick="imbDelete_Click" />
                                            <asp:Label ID="Label1" runat="server" Style="cursor: pointer" AssociatedControlID="imbDelete"
                                                Text="Delete"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstvPropertyValue" runat="server" SelectionMode="Multiple" CssClass="sfListmenu">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="sfButtonwrapper">
                <asp:ImageButton ID="imbSave" runat="server" ToolTip="Save" OnClick="imbSave_Click"
                    meta:resourcekey="imbSaveResource1" />
                <asp:Label ID="lblSave" runat="server" Style="cursor: pointer" AssociatedControlID="imbSave"
                    Text="Save" meta:resourcekey="lblSaveResource1"></asp:Label>
                <asp:ImageButton ToolTip="Cancel" ID="imbCancel" runat="server" OnClick="imbCancel_Click"
                    meta:resourcekey="imbCancelResource1" />
                <asp:Label ID="lblCancel" runat="server" Style="cursor: pointer" AssociatedControlID="imbCancel"
                    Text="Cancel" meta:resourcekey="lblCancelResource1"></asp:Label>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imbSaveChanges" />
    </Triggers>
</asp:UpdatePanel>