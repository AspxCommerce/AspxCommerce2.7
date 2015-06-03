<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemEventStartUpManagement.ascx.cs"
    Inherits="Modules_Admin_SystemStartUpManagement_SystemEventStartUpManagement" %>
<h2>
    <label id="lblSystemEventManagement">
        System Event StartUp Management</label></h2>
<asp:Panel ID="pnlEventStartUp" runat="server">
    <div class="sfFormwrapper">
        <h2>
            <asp:Label ID="lblAddEditSystemEventStartUp" runat="server" Text="Add/Edit System Event StartUp"></asp:Label>
        </h2>
        <asp:HiddenField ID="hdnPortalStartUpID" runat="server" Value="0" />
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td width="140">
                    <asp:Label ID="lblControlUrl" runat="server" CssClass="sfFormlabel" Text="Control Url"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlControlUrl" ToolTip="Select Control Url"
                        runat="server"
                        AutoPostBack="False" CssClass="sfListmenu">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEventLocation" runat="server" CssClass="sfFormlabel" Text="Event Location"></asp:Label>
                </td>
                <td>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEventLocation" ToolTip="Select Event Type"
                        runat="server"
                        AutoPostBack="False" CssClass="sfListmenu">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsAdmin" runat="server" CssClass="sfFormlabel" Text="Is Admin"></asp:Label>
                </td>
                <td>:
                </td>
                <td>
                    <asp:CheckBox ID="chkIsAdmin" runat="server" CssClass="sfCheckbox" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsControlUrl" runat="server" CssClass="sfFormlabel" Text="Is ControlUrl"></asp:Label>
                </td>
                <td>:
                </td>
                <td>
                    <asp:CheckBox ID="chkIsControlUrl" runat="server" CssClass="sfCheckbox" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsActive" runat="server" CssClass="sfFormlabel" Text="Is Active"></asp:Label>
                </td>
                <td>:
                </td>
                <td>
                    <asp:CheckBox ID="chkIsActive" runat="server" CssClass="sfCheckbox" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="sfButtonwrapper">
        <label id="lblSave" class="sfBtn icon-save" runat="server">
            Save
        <asp:Button ID="imbSave" runat="server" OnClick="imbSave_Click" />
        </label>
        <label id="lblCancel" class="sfBtn icon-close" runat="server">
            Cancel
        <asp:Button ID="imbCancel" runat="server" OnClick="imbCancel_Click"
            CausesValidation="False" />
        </label>

    </div>
</asp:Panel>
<asp:Panel ID="pnlSystemEventStartUpList" runat="server">
    <div class="sfButtonwrapper">
        <label id="lblAddNew" class="sfBtn icon-addnew" runat="server">
            Add System Event StartUp
       <asp:Button ID="imbAddNew" runat="server" OnClick="imbAddNew_Click" />
        </label>

    </div>
    <div class="sfGridwrapper">
        <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record to Show..."
            GridLines="None" AllowPaging="True" PageSize="15" BorderColor="White" BorderWidth="0px"
            OnPageIndexChanging="grdList_PageIndexChanging" OnRowCommand="grdList_RowCommand" OnRowDataBound="gdvList_RowDataBound"
            Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="ControlUrl">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditEvent" CommandArgument='<%# Eval("PortalStartUpID") %>'>
                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("ControlUrl") %>'></asp:Label>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Event Location">
                    <ItemTemplate>
                        <asp:Label ID="lblEventLocation" runat="server" Text='<%# Eval("EventLocationName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="IsAdmin" HeaderText="Is Admin">
                    <HeaderStyle CssClass="sfColumnIsActive" />
                </asp:BoundField>
                <asp:BoundField DataField="IsControlUrl" HeaderText="Is ControlUrl">
                    <HeaderStyle CssClass="sfColumnIsActive" />
                </asp:BoundField>
                <asp:BoundField DataField="IsSystem" HeaderText="Is System">
                    <HeaderStyle CssClass="sfColumnIsActive" />
                </asp:BoundField>
                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                    <HeaderStyle CssClass="sfColumnIsActive" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="AddedOn">
                    <ItemTemplate>
                        <%# Eval("AddedOn","{0:yyyy/MM/dd}") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="sfColumnAddedOn" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedeBY" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="sfAddedBy" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle CssClass="sfColumnEdit" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("PortalStartUpID") %>'
                            CommandName="EditEvent" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                            ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="sfDelete" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("PortalStartUpID") %>'
                            CommandName="DeleteEvent" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete this startup event?');" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                            meta:resourcekey="imbDeleteResource1" />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="sfPagination" />
            <HeaderStyle CssClass="sfClassHeadingOne" />
            <RowStyle CssClass="sfOdd" />
            <AlternatingRowStyle CssClass="sfEven" />
        </asp:GridView>
    </div>
</asp:Panel>
