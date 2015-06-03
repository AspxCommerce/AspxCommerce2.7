<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackageDetails.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.PackageDetails" %>

<div id="divPackageSettings" runat="server">
    <div class="sfMarginTopPri sfFormwrapper sfPadding">
        <h3>Package Settings</h3>
        <p class="sfNote">
            <asp:Label ID="lblPackageSettingsHelp" runat="server" Text="In this section you can configure the package information for this Module."
                meta:resourcekey="lblPackageSettingsHelpResource1"></asp:Label>
        </p>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="20%">
                    <asp:Label ID="lblPackageName" runat="server" Text="Package Name" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPackageName" runat="server" CssClass="sfInputbox required"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPackageName" runat="server" ControlToValidate="txtPackageName"
                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True"
                        CssClass="sfRequired"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="sfInputbox" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVersion" runat="server" Text="Version" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFirst" runat="server" CssClass="sfListmenu sfAuto" meta:resourcekey="ddlFirstResource1">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSecond" runat="server" CssClass="sfListmenu sfAuto" meta:resourcekey="ddlSecondResource1">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlLast" runat="server" CssClass="sfListmenu sfAuto" meta:resourcekey="ddlLastResource1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLicense" runat="server" Text="License" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLicense" runat="server" CssClass="sfInputbox" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReleaseNotes" runat="server" Text="Release Notes" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReleaseNotes" runat="server" CssClass="sfInputbox" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOwner" runat="server" Text="Owner" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOwner" runat="server" CssClass="sfInputbox" meta:resourcekey="txtOwnerResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOrganization" runat="server" CssClass="sfInputbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUrl" runat="server" Text="Url" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" CssClass="sfInputbox" meta:resourcekey="txtUrlResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revUrl" runat="server" ControlToValidate="txtUrl"
                        CssClass="sfRequired" ErrorMessage="The Url is not valid." SetFocusOnError="True"
                        ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$"
                        meta:resourcekey="revUrlResource1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="sfInputbox" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email address is not valid."
                        CssClass="sfRequired" ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_-]+@[a-zA-Z]+[.]{1}[a-zA-Z]+$"
                        meta:resourcekey="revEmailResource1"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
</div>
