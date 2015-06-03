<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUsEdit.ascx.cs"
    Inherits="SageFrame.Modules.ContactUs.ContactUsEdit" %>
<div class="sfFormwrapper">
    <div class="sfGridwrapper">
        <asp:GridView runat="server" ID="gdvContacters" Width="100%" AutoGenerateColumns="false"
            AllowPaging="True" OnPageIndexChanging="gdvContacters_PageIndexChanging" OnRowCancelingEdit="gdvContacters_RowCancelingEdit"
            OnRowCommand="gdvContacters_RowCommand" OnRowDataBound="gdvContacters_RowDataBound"
            OnRowDeleting="gdvContacters_RowDeleting" OnRowEditing="gdvContacters_RowEditing"
            OnRowUpdating="gdvContacters_RowUpdating" EmptyDataText="...Contact Not  Found...">
            <Columns>
                <asp:TemplateField HeaderText="S.N">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("AddedOn")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Message">
                    <ItemTemplate>
                        <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ContactUsID") %>'
                            CommandName="Delete" CssClass="icon-delete" 
                            OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete ?');" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfDelete" />
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="sfEven" />
            <PagerStyle CssClass="sfPagination" />
            <RowStyle CssClass="sfOdd" />
        </asp:GridView>
    </div>
</div>
