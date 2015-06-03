<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventViewer.ascx.cs" Inherits="SageFrame.Modules.Admin.EventViewer.EventViewer" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<script language="javascript" type="text/javascript">
    //<![CDATA[
    function flipFlopException(eTarget) {
        if (document.getElementById(eTarget).style.display == 'none') {
            document.getElementById(eTarget).style.display = '';
        }
        else {
            document.getElementById(eTarget).style.display = 'none';
        }

    }
    BindEvents();

    $(function () {

        var userGrid = '#' + '<%=gdvLog.ClientID%>';
        $(userGrid).find("tr:first input:checkbox").bind("click", function () {
            if ($(this).prop("checked")) {
                $(userGrid).find("tr input:checkbox").prop("checked", true);
            }
            else {
                $(userGrid).find("tr input:checkbox").prop("checked", false);
            }
        });


        var gridid = '#' + '<%=gdvLog.ClientID%>';
        $(gridid).find("tr.sfEven>td:gt(0),tr.sfOdd>td:gt(0)").bind("click", function () {
            var self = $(this).parent("tr");
            if (!$(self).hasClass("sfEventdetail")) {
                $(self).parent().find("tr.sfEventdetail").hide();
                $(self).parent().find("tr.sfActive").not(self).removeClass("sfActive");
                if (!$(self).hasClass("sfActive")) {
                    $(self).next("tr.sfEventdetail").show();
                    $(self).addClass("sfActive");
                }
                else {
                    $(self).removeClass("sfActive");
                }
            }
        });

    });
    function BindEvents() {
        $(function () {
            var logGrid = '#' + '<%=gdvLog.ClientID%>';
            $(logGrid).find('input[id*="imbDelete"]').on("click", function (e) {
                return ConfirmDialog($(this), 'Confirmation', 'Are you sure you want to delete this event log?');
            });
        });
    }
    //]]>	            
</script>
<h1>
    <asp:Label ID="lblEventViewerManagement" runat="server" Text="Event Viewer Management"
        meta:resourcekey="lblEventViewerManagementResource1"></asp:Label>
</h1>
<div class="sfButtonwrapper">
    <label class="sfLocale icon-clear-log sfBtn">
        Clear Log
        <asp:Button ID="imgLogClear" runat="server" ToolTip="Clear all Logs" OnClick="imgLogClear_Click"
            CausesValidation="False" meta:resourcekey="imgLogClearResource1" /></label>
    <label class="sfLocale icon-delete sfBtn">
        Delete Selected Logs
        <asp:Button ID="imgLogDelete" runat="server" ToolTip="Delete Selected Logs" CausesValidation="False"
            OnClick="imgLogDelete_Click" meta:resourcekey="imgLogDeleteResource1" />
    </label>
    <label class="sfLocale icon-page-preview sfBtn">
        Export Event Logs to Excel
        <asp:Button ID="btnExportToExcel" runat="server" ToolTip=" Export Event Logs to Excel"
            CausesValidation="False" OnClick="btnExportToExcel_Click" />
    </label>
</div>
<div class="sfFormwrapper sfPadding sfTableOption">
    <table id="tblEventViewer" cellspacing="0" cellpadding="0" runat="server" width="100%">
        <tr>
            <td>
                <p class="sfNote">
                    <i class="icon-info"></i>
                    <asp:Label ID="lblClickRow" runat="server" Text="Click on row for details" meta:resourcekey="lblClickRowResource1" /></p>
            </td>
            <td class="sfTxtAlignRgt">
                <asp:Label ID="lblLogType" runat="server" CssClass="sfFormlabel" Text="Type :" meta:resourcekey="lblLogTypeResource1" />
            </td>
            <td>
                <asp:DropDownList ID="ddlLogType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLogType_SelectedIndexChanged"
                    CssClass="sfListmenu" meta:resourcekey="ddlLogTypeResource1" />
            </td>
            <td class="sfTxtAlignRgt">
                <asp:Label ID="lblRecordsPage" runat="server" CssClass="sfFormlabel" Text="Show rows :"
                    meta:resourcekey="lblRecordsPageResource1" />
            </td>
            <td width="80" class="sfTxtAlignRgt">
                <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                    CssClass="sfListmenu sfAuto" meta:resourcekey="ddlRecordsPerPageResource1">
                    <asp:ListItem Value="10" meta:resourcekey="ListItemResource1">10</asp:ListItem>
                    <asp:ListItem Value="25" meta:resourcekey="ListItemResource2">25</asp:ListItem>
                    <asp:ListItem Value="50" meta:resourcekey="ListItemResource3">50</asp:ListItem>
                    <asp:ListItem Value="100" meta:resourcekey="ListItemResource4">100</asp:ListItem>
                    <asp:ListItem Value="150" meta:resourcekey="ListItemResource5">150</asp:ListItem>
                    <asp:ListItem Value="200" meta:resourcekey="ListItemResource6">200</asp:ListItem>
                    <asp:ListItem Value="250" meta:resourcekey="ListItemResource7">250</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    //<![CDATA[
    Sys.Application.add_load(BindEvents);
    //]]>	
</script>
<div class="sfGridwrapper sfEventview">
    <asp:GridView Width="100%" runat="server" ID="gdvLog" OnSelectedIndexChanged="gdvLog_SelectedIndexChanged"
        GridLines="None" AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="..........LogType Not Found.........."
        OnPageIndexChanging="gdvLog_PageIndexChanging" OnRowCommand="gdvLog_RowCommand"
        OnRowDataBound="gdvLog_RowDataBound" OnRowDeleting="gdvLog_RowDeleting" meta:resourcekey="gdvLogResource1">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                <HeaderTemplate>
                    <input id="chkBoxHeader" runat="server" type="checkbox" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hdfLogID" runat="server" Value='<%# Eval("LogID") %>' />
                    <asp:CheckBox ID="chkSendEmail" CssClass="sfEventcheck" runat="server" meta:resourcekey="chkSendEmailResource1" />
                </ItemTemplate>
                <HeaderStyle VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("AddedOn") %>' ID="lblDate" meta:resourcekey="lblDateResource1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LogType" meta:resourcekey="TemplateFieldResource3">
                <ItemTemplate>
                    <asp:Literal ID="ltrLogType" runat="server" Text='<%# Eval("LogTypeName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Portal Name" meta:resourcekey="TemplateFieldResource4">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("PortalName") %>' ID="lblPortalName" meta:resourcekey="lblPortalNameResource1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Client IP" meta:resourcekey="TemplateFieldResource5">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ClientIPAddress") %>' ID="lblClientIP"
                        meta:resourcekey="lblClientIPResource1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("PageURL") %>' ID="lblPageURL" Visible="False"
                        meta:resourcekey="lblPageURLResource1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="sfDelete" meta:resourcekey="TemplateFieldResource3">
                <ItemTemplate>
                    <asp:LinkButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("LogID") %>'
                        CommandName="Delete" CssClass="icon-delete" meta:resourcekey="imbDeleteResource1" />
                </ItemTemplate>
                <HeaderStyle VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                <ItemTemplate>
                    <tr class="sfEventdetail" style="display: none">
                        <td colspan="8">
                            <div class="sfEventinfo">                            
                            <asp:Panel ID="pnlClientIP" runat="server" Width="100%" meta:resourcekey="pnlClientIPResource1">
                                <p>
                                    <asp:Label ID="lblClientIP1" runat="server" CssClass="sfFormlabel" Text="Client IP:"
                                        meta:resourcekey="lblClientIP1Resource1"></asp:Label>
                                    <asp:Literal ID="ltrClientIP" runat="server" Text='<%# Eval("ClientIPAddress") %>'></asp:Literal>
                                </p>
                                <p>
                                    <asp:Label ID="lblPageUrl1" runat="server" Text="PageUrl:" class="sfFormlabel" meta:resourcekey="lblPageUrl1Resource1"></asp:Label>
                                    <asp:Literal ID="ltrPageUrl" runat="server" Text='<%# Eval("PageUrl") %>'></asp:Literal>
                                </p>
                                <p>
                                    <asp:Label ID="lblException1" runat="server" Text="Exception:" class="cssClassBoldText"
                                        meta:resourcekey="lblException1Resource1"></asp:Label>
                                    <asp:Literal ID="ltrException" runat="server" Text='<%# Eval("Exception") %>'></asp:Literal>
                                </p>
                            </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="sfPagination" />
        <RowStyle CssClass="sfOdd" />
        <AlternatingRowStyle CssClass="sfEven" />
    </asp:GridView>
</div>
<div>
    <div class="sfExceptions">
        <h3>
            Send Exceptions</h3>
        <table id="tblSendException" runat="server" cellpadding="0" cellspacing="0" width="100%">
            <tr runat="server">
                <td colspan="2" runat="server">
                    <p class="sfNote">
                        <i class="icon-info"></i>
                        <asp:Label ID="Label4" runat="server" Text="<strong>Please note:</strong> By using these features below, you may be sending sensitive data over the Internet in clear text (not encrypted). Before sending your exception submission, please review the contents of your exception log to verify that no sensitive data is contained within it. The row that is checked is sent as an email along with the optional message."></asp:Label>
                    </p>
                </td>
            </tr>
            <tr runat="server">
                <td style="vertical-align: top;" runat="server">
                    <div class="sfFormwrapper">
                        <table id="tblSendExceptionsInfo" runat="server" width="100%" cellpadding="0" cellspacing="0">
                            <tr id="tr1" runat="server">
                                <td class="SubHead" style="width: 175px" valign="top" runat="server">
                                    <asp:Label ID="lblEmailAddress1" runat="server" CssClass="sfFormlabel" Text="Email Address: "></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtEmailAdd" runat="server" CssClass="sfInputbox" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                        CssClass="sfError" ControlToValidate="txtEmailAdd" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                        ValidationGroup="sendMail" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvEmailAdd" runat="server" ControlToValidate="txtEmailAdd"
                                        CssClass="sfError" ErrorMessage="Please Enter Your Email Address" SetFocusOnError="True"
                                        ValidationGroup="sendMail" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td align="left" width="200" runat="server">
                                    <asp:Label ID="Label2" runat="server" CssClass="sfFormlabel" Text="Subject:"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtSubject1" runat="server" CssClass="sfInputbox" />
                                    <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="txtSubject1"
                                        CssClass="sfError" ErrorMessage="Enter the subject" SetFocusOnError="True" ValidationGroup="sendMail"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td align="left" width="200" runat="server">
                                    <asp:Label ID="Label3" runat="server" CssClass="sfFormlabel" Text="Message(Optional):"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtMessage1" CssClass="sfTextarea" runat="server" Rows="6" Columns="25"
                                        TextMode="MultiLine" Width="350px" Height="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td align="left" runat="server">
                                    &nbsp;
                                </td>
                                <td runat="server">
                                    <div class="sfButtonwrapper">
                                        <label class="sfLocale icon-send sfBtn">
                                            Send
                                            <asp:Button ID="imgSendEmail" runat="server" ToolTip="Send Email" OnClick="imgSendEmail_Click"
                                                ValidationGroup="sendMail" />
                                        </label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
