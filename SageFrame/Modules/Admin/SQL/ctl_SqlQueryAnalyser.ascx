<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SqlQueryAnalyser.ascx.cs"
    Inherits="SageFrame.Modules.Admin.SQL.ctl_SqlQueryAnalyser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<h1>
    <asp:Label ID="lblSqlQueryAnalyser" runat="server" Text="SQL Query Analyser" meta:resourcekey="lblSqlQueryAnalyserResource1"></asp:Label>
</h1>

<div class="sfButtonwrapper sfPadding">
    <label class="sfLocale icon-data sfBtn">
        Archive Session Tracker
                        <asp:Button ID="btnBackup" runat="server" OnClick="btnBackup_Click"
                            ToolTip="Archive Session Tracker" /></label>
    <label class="sfLocale icon-sql sfBtn" title="this script cleans all the portal  pages, modules and data giving a fresh sageframe to start">
        Database Backup
                        <asp:Button ID="btnDatabasebackup" runat="server" OnClick="btnDatabasebackup_Click" 
                            ToolTip="Database Backup" /></label>
       <label class="sfLocale icon-sql sfBtn">
        Run clean up script
                        <asp:Button ID="btnSageFrameCleanup" runat="server" OnClick="btnDatabaseCleanup_Click"
                            ToolTip="Database Backup" /></label>
</div>
<div class="sfFormwrapper">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblSelectSqlScriptFile" runat="server" CssClass="sfFormlabel sfFloatLeft" Text="SQL File"
                    ToolTip="Upload a file into the SQL Query window (Optional)." meta:resourcekey="lblSelectSqlScriptFileResource1"></asp:Label>
                <div class="sfButtonwrapper sfSql">
                    <asp:FileUpload ID="fluSqlScript" runat="server" meta:resourcekey="fluSqlScriptResource1" />
                    <label class="sfLocale icon-upload sfBtn">
                        Upload
                        <asp:Button ID="imbUploadSqlScript" runat="server" OnClick="imbUploadSqlScript_Click"
                            ToolTip="Load the selected file." meta:resourcekey="imbUploadSqlScriptResource1" /></label>
                </div>
            </td>
            <td></td>

        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtSqlQuery" runat="server" TextMode="MultiLine" Rows="10" CssClass="sfTextarea"
                    Height="250px" Width="75%" EnableViewState="False" meta:resourcekey="txtSqlQueryResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sfButtonwrapper">
                    <asp:CheckBox ID="chkRunAsScript" runat="server" Text="Run as Script" TextAlign="Right"
                        ToolTip="include 'GO' directives; for testing &amp; update scripts" CssClass="sfCheckbox"
                        meta:resourcekey="chkRunAsScriptResource1" />
                    <label class="icon-execute sfLocale sfBtn">
                        Execute
                        <asp:Button ID="imbExecuteSql" runat="server" Style="margin-left: 15px" OnClick="imbExecuteSql_Click"
                            ToolTip="can include {directives} and /*comments*/" meta:resourcekey="imbExecuteSqlResource1" />
                    </label>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="sfGridwrapper" style="overflow: scroll;">
    <asp:GridView ID="gdvResults" runat="server" EnableViewState="False" meta:resourcekey="gdvResultsResource1">
        <EmptyDataTemplate>
            <asp:Label ID="lblEmptyText" runat="server" Text="The query did not return any data"
                meta:resourcekey="lblEmptyTextResource1" />
        </EmptyDataTemplate>
        <RowStyle CssClass="sfOdd" />
        <AlternatingRowStyle CssClass="sfEven" />
    </asp:GridView>
</div>
