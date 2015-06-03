<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocalModuleTitle.ascx.cs"
    Inherits="Modules_Language_LocalModuleTitle" %>
<h1>
    <asp:Label ID="lblLocalModuleTitle" runat="server" Text="Localize Module Title"></asp:Label>
</h1>
<div class="sfFormwrapper sfTableOption">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="15%">
                <div>
                    <asp:Label ID="lblAvailableLocales" runat="server" CssClass="sfFormlabel" Text="Available Locales"
                        meta:resourcekey="lblAvailableLocalesResource1"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlAvailableLocales" runat="server" CssClass="sfListmenu" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlAvailableLocales_SelectedIndexChanged" meta:resourcekey="ddlAvailableLocalesResource1">
                </asp:DropDownList>
                <asp:Image ID="imgFlag" runat="server" meta:resourcekey="imgFlagResource1" />
            </td>
        </tr>
    </table>
</div>
<div class="sfGridwrapper">
    <asp:GridView ID="gdvLocalModuleTitle" class="cssClassKeyValueGrid" DataKeyNames="UserModuleID"
        runat="server" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="gdvLocalModuleTitle_SelectedIndexChanged"
        OnSelectedIndexChanging="gdvLocalModuleTitle_SelectedIndexChanging" OnPageIndexChanging="gdvLocalModuleTitle_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="S.N">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modules">
                <ItemTemplate>
                    <asp:Label ID="lbldefaultValue" runat="server" Text='<%# Eval("UserModuleTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Module Local Title" meta:resourcekey="TemplateFieldResource6">
                <ItemTemplate>
                    <asp:TextBox ID="txtLocalModuleTitle" Style="width: 350px" ToolTip="<%# Container.DataItemIndex+1 %>"
                        runat="server" CssClass="sfInputbox" Text='<%# Eval("LocalModuleTitle") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="sfEven" />
        <RowStyle CssClass="sfOdd" />
    </asp:GridView>
</div>
<div class="sfButtonwrapper">
<label class="sfLocale icon-save sfBtn">
        Save
    <asp:Button ID="imbUpdate" runat="server" meta:resourcekey="imbUpdateResource1"
        OnClick="imbUpdate_Click" Style="height: 16px" />
          </label>
    <%--<asp:Label ID="lblUpdateResxFile" runat="server" Text="Save" CssClass="cssClassFormLabel"
        AssociatedControlID="imbUpdate" Style="cursor: pointer;" meta:resourcekey="lblUpdateResxFileResource1"></asp:Label>--%>
         <label class="sfLocale icon-close sfBtn">
        Cancel
    <asp:Button ID="imbCancel" runat="server" meta:resourcekey="imbCancelResource1"
        OnClick="imbCancel_Click" /></label>
    <%--<asp:Label ID="Label2" runat="server" CssClass="cssClassFormLabel" Text="Back" AssociatedControlID="imbCancel"
        Style="cursor: pointer;" meta:resourcekey="Label2Resource1"></asp:Label>--%>
</div>


  