<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageframeUpgradeView.ascx.cs"
    Inherits="Modules_Upgrade_SageframeUpgrade" %>
<style>
    .borderClass
    {
        border: solid 1px black;
        height: 150px;
        width: 350px;
        padding: 5px 5px 5px 35px;
    }
    .headerClass
    {
        background-color: Gray;
        font-size: large;
        font-weight: bold;
        padding: 5px 5px 5px 25px;
    }
    .errorMsgClass
    {
        color: Red;
    }
    #uniform-ctl13_fuUpgrade input
    {
        opacity: 100 !important;
    }
</style>
<h1>
    SageFrame Upgrader</h1>
<div class="sfFormwrapper">
    <asp:FileUpload ID="fuUpgrade" runat="server" />
    <div class="sfButtonwrapper">
    <label class="icon-update sfBtn sfLocale">Upgrade
        <asp:Button ID="btnUpload"  runat="server" OnClick="btnUpload_Click"
           /></label>
        <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="sfRequired"></asp:Label></div>
</div>
