<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguageSetUp.ascx.cs"
    Inherits="Modules_Language_LanguageSetUp" %>
      <%@ Reference VirtualPath="~/Modules/Language/CreateLanguagePack.ascx" %>    
      <%@ Reference VirtualPath="~/Modules/Language/LocalPage.ascx" %>  
<h1>
    <asp:Label ID="lblTimeZoneEditor" runat="server" Text="Language Editor" meta:resourcekey="lblTimeZoneEditorResource1"></asp:Label>
</h1>
<div class="sfFormwrapper">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" valign="top">
                <asp:Label ID="lblLanguage" CssClass="sfFormlabel" runat="server" Text="Select Language"
                    meta:resourcekey="lblLanguageResource1"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <div class="sfAvailablelanguage">
                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="sfListmenu" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" meta:resourcekey="ddlLanguageResource1">
                    </asp:DropDownList>
                    <asp:Image ID="imgFlagLanguage" runat="server" meta:resourcekey="imgFlagLanguageResource1" />
                </div>
                <div class="sfRadiobutton">
                    <asp:RadioButtonList ID="rbLanguageType" RepeatDirection="Horizontal" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="rbLanguageType_SelectedIndexChanged"
                        meta:resourcekey="rbLanguageTypeResource1">
                        <asp:ListItem Text="English" Value="0" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Native" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="sfButtonwrapper">
<label class="sfLocale icon-save sfBtn">Save
    <asp:Button ID="imbUpdate" runat="server" 
        OnClick="imbUpdate_Click" meta:resourcekey="imbUpdateResource1" /></label>
    <%--<asp:Label ID="lblAddLanguage" runat="server" Text="Save" CssClass="sfFormlabel"
        AssociatedControlID="imbUpdate" Style="cursor: pointer;" meta:resourcekey="lblAddLanguageResource1"></asp:Label>--%>
        <label class="sfLocale icon-close sfBtn">Cancel
    <asp:Button ID="imbCancel" runat="server" 
        OnClick="imbCancel_Click" meta:resourcekey="imbCancelResource1" /></label>
    <%--<asp:Label ID="lblInstallLang" runat="server" CssClass="sfFormlabel" Text="Cancel"
        AssociatedControlID="imbCancel" Style="cursor: pointer;" meta:resourcekey="lblInstallLangResource1"></asp:Label>--%>
</div>
