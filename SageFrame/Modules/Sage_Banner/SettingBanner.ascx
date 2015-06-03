<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingBanner.ascx.cs"
    Inherits="Modules_Sage_Banner_SettingBanner" %>
<div id="divBannerSetting" runat="server" class="sfFormwrapper">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="25%">
                <asp:Label ID="lblBannerToUse" runat="server" CssClass="sfFormlabel" Text="Bannner Use"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                
                <asp:DropDownList ID="ddlBannerListToUse" runat="server" AutoPostBack="False" CssClass="sfListmenu">
                </asp:DropDownList> 
                <label class="icon-refresh">              
                <asp:Button runat="server" ID="btnRefreshList" OnClick=" btnRefreshList_Click" /></label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMode" runat="server" CssClass="sfFormlabel" Text="Transition Mode"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlTransitionMode" runat="server" CssClass="sfListmenu">
                    <asp:ListItem Value="0">horizontal</asp:ListItem>
                    <asp:ListItem Value="2"> fade</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblInfiniteLoop" runat="server" CssClass="sfFormlabel" Text="Infinite loop"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkInfiniteLoop" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSpeed" runat="server" CssClass="sfFormlabel" Text="Speed(in ms)"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:TextBox ID="txtSpeed" runat="server" CssClass="sfInputbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtSpeed" runat="server" ControlToValidate="txtSpeed"
                    SetFocusOnError="true" ValidationGroup="bannersetting" ErrorMessage="*" CssClass="cssClasssNormalRed"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtSpeed" runat="server" ControlToValidate="txtSpeed"
                    ErrorMessage="*" ValidationExpression="^\d+$">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPager" runat="server" CssClass="sfFormlabel" Text="PagerType"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:Label ID="lblNumeric" runat="server" CssClass="sfFormlabel" Text="Numeric"></asp:Label>
                <asp:CheckBox ID="chkNumeric" runat="server" />
            </td>
        </tr>
        <tr id="tdAutoslide" runat="server">
            <td>
                <asp:Label ID="lblAuto" runat="server" CssClass="sfFormlabel" Text="Auto Slide"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkAutoSlide" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPause" runat="server" CssClass="sfFormlabel" Text="Pause Time(in ms)"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:TextBox ID="txtPauseTime" runat="server" Text="0" CssClass="sfInputbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPauseTime" runat="server" ControlToValidate="txtPauseTime"
                    SetFocusOnError="true" ValidationGroup="bannersetting" ErrorMessage="*" CssClass="cssClasssNormalRed"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPauseTime"
                ErrorMessage="*"  ValidationExpression="^\d+$" ValidationGroup="bannersetting"></asp:RegularExpressionValidator>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="sfFormlabel" Text="Enable Next/Prev Button"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkEnableControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <div class="sfButtonwrapper" runat="server">
                 <label class="sfLocale icon-save sfBtn">Save
                    <asp:Button ID="imbSaveBannerSetting" runat="server" ValidationGroup="bannersetting"
                        OnClick="imbSaveBannerSetting_Click" /></label>
                    
                </div>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblSettingMessage" runat="server" CssClass="sfFormlabel"></asp:Label>
</div>
