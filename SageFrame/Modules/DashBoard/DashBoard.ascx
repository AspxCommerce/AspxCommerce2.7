<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="SageFrame.Modules.DashBoard.DashBoard" %>
<h2>
    <asp:Label ID="lblSfInfo" runat="server" Text="SageFrame CPanel"></asp:Label>
</h2>
<asp:Repeater ID="rptDashBoard" runat="server">
    <HeaderTemplate>
        <div class="sfDashboard clearfix">
            <ul>
    </HeaderTemplate>
    <FooterTemplate>
        </ul> </div>
    </FooterTemplate>
    <ItemTemplate>
        <li><a id="hypPageURL" runat="server" href='<%# Eval("Url") %>'><span class="sfModuleicon">
           <%-- <asp:Image ID="imgDisplayImage" ImageAlign="Middle" runat="server" CssClass="sfImageheight"
                ImageUrl='<%# Eval("IconFile") %>' meta:resourcekey="imgDisplayImageResource1" />--%>
                <asp:Literal runat="server" ID="ltrImage" Text='<%# Eval("IconFile")%>'></asp:Literal>
        </span>
            <asp:Label class="sfModulename" ID="lblPageName" runat="server" Text='<%# Eval("PageName") %>'
                meta:resourcekey="lblPageNameResource1"></asp:Label>
        </a>
            <asp:Label ID="lblSEOName" runat="server" Visible="False" Text='<%# Eval("PageName") %>'
                meta:resourcekey="lblSEONameResource1"></asp:Label>
        </li>
    </ItemTemplate>
</asp:Repeater>
<div class="clear">
</div>
