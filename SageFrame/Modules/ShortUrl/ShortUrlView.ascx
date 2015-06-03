<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShortUrlView.ascx.cs"
    Inherits="Modules_ShortUrl_ShortUrlView" %>
<div runat="server">
    <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
    <asp:Button ID="btnGenerateShortUrl" runat="server" Text="Generate Code" 
        onclick="btnGenerateShortUrl_Click" />
</div>
<div runat="server">
    <asp:Label ID="lblShortUrl" runat="server"></asp:Label>
    <asp:HyperLink ID="hypCode" runat="server" Target="_blank"></asp:HyperLink>
</div>
