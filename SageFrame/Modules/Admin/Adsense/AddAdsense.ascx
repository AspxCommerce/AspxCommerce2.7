<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddAdsense.ascx.cs" Inherits="SageFrame.Modules.Admin.Adsense.AddAdsense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Adsense.ascx" TagName="Adsense" TagPrefix="uc1" %>
<%@ Register Assembly="SageFrame.GoogleAdUnit" Namespace="SageFrame.GoogleAdUnit" TagPrefix="wwc" %>
<asp:Panel ID="pnlAdsenseEdit" runat="server" meta:resourcekey="pnlAdsenseEditResource1">
    <div class="sfFormwrapper">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="hdfAdsenseID" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblShowinUserModule" runat="server" CssClass="sfFormlabel" Text=" Show In UserModule:"
                        meta:resourcekey="lblShowinUserModuleResource1" />
                </td>
                <td width="27%">
                    <asp:CheckBox ID="chkShow" runat="server" CssClass="sfCheckbox" Checked="True" meta:resourcekey="chkShowResource1" />
                </td>
                <td width="48%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFormat" runat="server" CssClass="sfFormlabel" Text="Format:" meta:resourcekey="lblFormatResource1"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUnitFormat" runat="server" CssClass="sfListmenu" DataTextField="Key"
                        DataValueField="Value" meta:resourcekey="ddlUnitFormatResource1">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:HyperLink ID="hplHelp" runat="server" NavigateUrl="https://www.google.com/adsense/static/en_US/AdFormats.html?hl=en_US&amp;gsessionid=HqT8clPax7R_NzPVDDj_zw"
                        Target="_blank" Text="Learn More" meta:resourcekey="hplHelpResource1"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddUnitType" runat="server" CssClass="sfFormlabel" Text="Add Unit Type:"
                        meta:resourcekey="lblAddUnitTypeResource1"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList DataTextField="Key" DataValueField="Value" ID="ddlAddType" CssClass="sfListmenu"
                        runat="server" meta:resourcekey="ddlAddTypeResource1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblChannelID" runat="server" CssClass="sfFormlabel" Text="Channel ID:"
                        meta:resourcekey="lblChannelIDResource1"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtChannelID" runat="server" CssClass="sfInputbox" meta:resourcekey="txtChannelIDResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBorderColor" runat="server" CssClass="sfFormlabel" Text="Border Color:"
                        meta:resourcekey="lblBorderColorResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBorder" runat="server" CssClass="sfInputbox" meta:resourcekey="txtBorderResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="imbBorder" runat="server" meta:resourcekey="imbBorderResource1" />
                    <cc1:ColorPickerExtender ID="cpBackGround" runat="server" TargetControlID="txtBorder"
                        PopupButtonID="imbBorder" Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBackColor" runat="server" CssClass="sfFormlabel" Text="Back Color:"
                        meta:resourcekey="lblBackColorResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbackcolor" runat="server" CssClass="sfInputbox" meta:resourcekey="txtbackcolorResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="imbBack" runat="server" meta:resourcekey="imbBackResource1" />
                    <cc1:ColorPickerExtender ID="cpBack" runat="server" TargetControlID="txtbackcolor"
                        PopupButtonID="imbBack" Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLinkColor" runat="server" CssClass="sfFormlabel" Text="Link Color:"
                        meta:resourcekey="lblLinkColorResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLink" runat="server" CssClass="sfInputbox" meta:resourcekey="txtLinkResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="imblink" runat="server" meta:resourcekey="imblinkResource1" />
                    <cc1:ColorPickerExtender ID="cpLink" runat="server" TargetControlID="txtLink" PopupButtonID="imblink"
                        Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTextColor" runat="server" CssClass="sfFormlabel" Text="Text Color:"
                        meta:resourcekey="lblTextColorResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtText" runat="server" CssClass="sfInputbox" meta:resourcekey="txtTextResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="imbText" runat="server" meta:resourcekey="imbTextResource1" />
                    <cc1:ColorPickerExtender ID="ColorPickerExtender3" runat="server" TargetControlID="txtText"
                        PopupButtonID="imbText" Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblURLColor" runat="server" CssClass="sfFormlabel" Text="URL Color:"
                        meta:resourcekey="lblURLColorResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtURL" runat="server" CssClass="sfInputbox" meta:resourcekey="txtURLResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="imbURL" runat="server" meta:resourcekey="imbURLResource1" />
                    <cc1:ColorPickerExtender ID="ColorPickerExtender4" runat="server" TargetControlID="txtURL"
                        PopupButtonID="imbURL" Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAlternate" runat="server" CssClass="sfFormlabel" Text="Alternate ads:"
                        meta:resourcekey="lblAlternateResource1"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlAlternateAds" DataTextField="Key" DataValueField="Value"
                        CssClass="sfListmenu" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlAlternateAds_SelectedIndexChanged"
                        meta:resourcekey="ddlAlternateAdsResource1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="solidFill" runat="server" visible="False">
                <td runat="server">
                    <asp:Label ID="Label7" runat="server" CssClass="sfFormlabel" Text="Solid Fill Color:"></asp:Label>
                </td>
                <td style="text-align: left" runat="server">
                    <asp:TextBox ID="txtSolidFill" runat="server" CssClass="sfInputbox"></asp:TextBox>
                </td>
                <td runat="server">
                    <asp:ImageButton ID="imbSolidFill" runat="server" />
                    <cc1:ColorPickerExtender ID="ColorPickerExtender1" runat="server" TargetControlID="txtSolidFill"
                        PopupButtonID="imbSolidFill" Enabled="True" />
                </td>
            </tr>
            <tr id="anotherURL" runat="server" visible="False">
                <td runat="server">
                    <asp:Label ID="lblURLLink" runat="server" CssClass="sfFormlabel" Text="URL:"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left" runat="server">
                    <asp:TextBox ID="txtanotherURL" runat="server" CssClass="sfInputbox"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtanotherURL"
                        ErrorMessage="Invalid" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                        ValidationGroup="adsense" CssClass="sfError"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsActive" runat="server" CssClass="sfFormlabel" Text="Active:"
                        meta:resourcekey="lblIsActiveResource1"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:CheckBox ID="chkActive" runat="server" CssClass="sfCheckbox" Checked="True"
                        meta:resourcekey="chkActiveResource1" />
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
        </table>
        <div class="sfButtonwrapper">
            <asp:LinkButton ID="ImbPreview" ToolTip="Preview" runat="server" Text="Preview" OnClick="Preview_Click" CssClass="sfLocale icon-preview sfBtn"
                meta:resourcekey="ImbPreviewResource1" />
            <%--<asp:Label ID="lblPreview" runat="server" Text="Preview" AssociatedControlID="ImbPreview"
                Style="cursor: pointer;" meta:resourcekey="lblPreviewResource1"></asp:Label>--%>
            <asp:LinkButton ID="ImbSave" runat="server" Text="Save" ToolTip="Save" OnClick="btnSave_Click" CssClass="sfLocale icon-save sfBtn"
                meta:resourcekey="ImbSaveResource1" CausesValidation="true" ValidationGroup="adsense" />
           <%-- <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="ImbSave"
                Style="cursor: pointer;" meta:resourcekey="lblSaveResource1"></asp:Label>--%>
            <asp:LinkButton ID="imbDelete" Visible="False" Text="Delete" ToolTip="Delete" runat="server" OnClick="imbDelete_Click" CssClass="sfLocale icon-close sfBtn"
                meta:resourcekey="imbDeleteResource1" />
           <%-- <asp:Label ID="lblDelete" runat="server" Text="Delete" AssociatedControlID="imbDelete"
                Style="cursor: pointer;" Visible="False" meta:resourcekey="lblDeleteResource1"></asp:Label>--%>
        </div>
        <wwc:AdUnit ID="AdUnit1" runat="server" Visible="False" AdUnitFormat="LeaderBoard_728x90_H"
            AdUnitType="TextAndImage" AffiliateId="" AlternateAdType="PublicServiceAds" AnotherUrl=""
            BackColor="White" BorderColor="AliceBlue" ChannelId="" LinkColor="Blue" SolidFillColor="Blue"
            TextColor="Black" UrlColor="Green">
        </wwc:AdUnit>
    </div>
</asp:Panel>
