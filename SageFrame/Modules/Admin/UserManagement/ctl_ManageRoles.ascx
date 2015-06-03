<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ManageRoles.ascx.cs"
    Inherits="SageFrame.Modules.Admin.UserManagement.ctl_ManageRoles" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(this).PageRoleSettings({
            UserModuleID: '<%=userModuleID%>',
            RoleID: '<%=RoleID%>'
        });
    });
    //]]>	
</script>
<h1>
    <asp:Label ID="lblRolesManagement" runat="server" Text="Roles Management"></asp:Label>
</h1>
<asp:Panel ID="pnlRole" runat="server">
    <div class="sfFormwrapper">
        <h2>
            <asp:Label ID="lblAddRoles" runat="server" Text="Add New Role"></asp:Label>
        </h2>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="15%">
                    <asp:Label ID="lblRole" runat="server" CssClass="sfFormlabel" Text="Role Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRole" runat="server" CssClass="sfInputbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ErrorMessage="*" ControlToValidate="txtRole"
                        ValidationGroup="SageFrameRole"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <div class="sfButtonwrapper">
        <asp:LinkButton ID="imgAdd" runat="server" ValidationGroup="SageFrameRole" OnClick="imgAdd_Click"
            ToolTip="save" CssClass="icon-save sfBtn" Text="Save" />
        <asp:LinkButton ID="imgCancel" runat="server" CausesValidation="False" OnClick="imgCancel_Click"
            ToolTip="cancel" CssClass="icon-close sfBtn" Text="Cancel" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlRoles" runat="server">
    <div class="sfButtonwrapper">
        <asp:LinkButton ID="imbAddNewRole" runat="server" OnClick="imbAddNewRole_Click" ToolTip="Add New Role"
            CssClass="icon-addnew sfBtn" Text="Add New Role" />
        <asp:LinkButton ID="imbPageRoleSettings" runat="server" OnClick="imbPageRoleSettings_Click" ToolTip="Page Role Settings"
            CssClass="icon-settings sfBtn" Text="Page Role Settings" />
        <asp:LinkButton ID="imbDashboardRoleSettings" runat="server" ToolTip="Dashboard Role Settings"
            CssClass="icon-settings sfBtn" Text="Dashboard Role Settings" OnClick="imbDashboardRoleSettings_Click" />
    </div>
    <div class="sfGridwrapper">
        <asp:GridView ID="gdvRoles" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnRowDeleting="gdvRoles_RowDeleting" DataKeyNames="Role,RoleID" Width="100%"
            OnRowDataBound="gdvRoles_RowDataBound" OnRowCommand="gdvRoles_RowCommand">
            <Columns>
                <asp:BoundField DataField="Role" HeaderText="Roles" />
                <asp:TemplateField HeaderStyle-CssClass="sfDelete">
                    <ItemTemplate>
                        <asp:LinkButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%#Container.DataItemIndex%>'
                            CommandName="Delete" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete this role?');"
                            CssClass="icon-delete" ToolTip="Delete the role" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="sfOdd" />
            <AlternatingRowStyle CssClass="sfEven" />
        </asp:GridView>
    </div>
</asp:Panel>
<asp:Panel ID="pnlPageRoleSettings" runat="server" class="sfFormwrapper">
    <h2>
        <asp:Label ID="lblPageRoleSettings" runat="server" Text="Page Role Settings"></asp:Label>
    </h2>
    <div id="divBindRolesList" class="clearfix">
        <div class="sfCenterdivB">
            <div class="sfCenterWrapperB">
                <div class="sfCenterB">
                    <div id="dvRoleType" class="sfAdvanceRadioBtn sfMarginbtn">
                        <asp:Literal ID="ltrPagesRadioButtons" runat="server"></asp:Literal>
                    </div>
                    <div class="clearfix">
                        <div class="divPermission sfGridwrapper">
                            <table id="tblPermission" cellspacing="0" cellpadding="0" width="100%">
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sfLeftdivB">
        </div>
    </div>
    <div class="sfButtonwrapperB">
        <button type="button" id="btnSubmit" class="icon-save sfBtn">
            Save All</button>
        <button type="button" class="icon-close sfBtn" id="imbPageCancel">
            Cancel</button>
    </div>
    <div class="clear">
    </div>
</asp:Panel>
<asp:Panel ID="panelDashboardRoles" runat="server">
    <div class="sfFormwrapper">
        <h2>
            <asp:Label ID="Label1" runat="server" Text="Dashboard Role Management"></asp:Label>
        </h2>
        <table id="tblDashboardRolesSettings" runat="server" cellpadding="0" cellspacing="0"
            border="0">
            <tr id="Tr1" runat="server">
                <td id="Td1" width="18%" runat="server">
                    <asp:Label ID="lblUnselected" runat="server" CssClass="sfFormlabel" Text="Un Approved User"></asp:Label>
                </td>
                <td id="Td2" width="1%" runat="server"></td>
                <td id="Td3" runat="server">
                    <asp:Label ID="lblSelected" runat="server" CssClass="sfFormlabel" Text="Approved User"></asp:Label>
                </td>
            </tr>
            <tr id="Tr2" runat="server">
                <td id="Td4" valign="top" runat="server">
                    <asp:ListBox ID="lstUnselectedRoles" runat="server" SelectionMode="Multiple" CssClass="sfListmenubig"></asp:ListBox>
                </td>
                <td id="Td5" runat="server" class="sfSelectleftright">
                    <asp:Button ID="btnAddAllRole" runat="server" CausesValidation="False" OnClick="btnAddAllRole_Click"
                        CssClass="sfSelectallright" Text="&gt;&gt;" />
                    <asp:Button ID="btnAddRole" runat="server" CausesValidation="False" OnClick="btnAddRole_Click"
                        CssClass="sfSelectoneright" Text=" &gt; " />
                    <asp:Button ID="btnRemoveRole" runat="server" CausesValidation="False" OnClick="btnRemoveRole_Click"
                        CssClass="sfSelectoneleft" Text=" &lt; " />
                    <asp:Button ID="btnRemoveAllRole" runat="server" CausesValidation="False" OnClick="btnRemoveAllRole_Click"
                        Text="&lt;&lt;" CssClass="sfSelectallleft" />
                </td>
                <td id="Td6" valign="top" runat="server">
                    <asp:ListBox ID="lstSelectedRoles" runat="server" SelectionMode="Multiple" CssClass="sfListmenubig"></asp:ListBox>
                </td>
            </tr>
        </table>
        <div class="sfButtonwrapper">
            <label id="lblManageRoleSave" class="sfLocale icon-update sfBtn">
                Update
            <asp:Button runat="server" ID="imgManageRoleSave" OnClick="imgManageRoleSave_Click" />
            </label>
            <label id="lblManageRoleCancel" class="sfLocale icon-close sfBtn">
                Cancel
            <asp:Button runat="server" ID="imgManageRoleCancel" OnClick="imgManageRoleCancel_Click" />
            </label>
        </div>
        <div>
            &nbsp;
        </div>
    </div>
</asp:Panel>
