<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteAnalyticSetting.ascx.cs"
    Inherits="Modules_DashBoardControl_DashBoardControlSetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="sfFormwrapper">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblStartDate" runat="server" Text="Start Date" CssClass="sfFormlabel"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="sfInputbox"></asp:TextBox>
                <cc1:CalendarExtender ID="ccStartDate" runat="server" TargetControlID="txtStartDate">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                    ErrorMessage="*" ToolTip="Field is required" ValidationGroup="rfvS"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvStartDate" runat="server" ControlToValidate="txtStartDate"
                    ErrorMessage="Must Be Valid Date" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date" ValidationGroup="rfvS"></asp:CompareValidator>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEndDate" runat="server" Text="End Date" CssClass="sfFormlabel"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="sfInputbox"></asp:TextBox>
                <cc1:CalendarExtender ID="ccEndDate" runat="server" TargetControlID="txtEndDate">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                    ErrorMessage="*" ToolTip="Field is required" ValidationGroup="rfvS"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvEndDate" runat="server" ControlToValidate="txtEndDate"
                    ErrorMessage="Must Be Valid Date" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date" ValidationGroup="rfvS"></asp:CompareValidator>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="rfvS"
                    CssClass="sfBtn" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>
