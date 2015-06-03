<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchTermManage.ascx.cs"
    Inherits="Modules_AspxSearchTerm_SearchTermManage" %>

<script type="text/javascript">

    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxSearchTermManagement
        });
    });
    var btnExportToExcel = '<%=btnExportToExcel.ClientID %>';
    var umi = '<%=UserModuleID%>';
//]]>
</script>

<div id="divShowSearchTermDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Search Terms"
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnDeleteAllSearchTerm" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportToExcel" class="sfBtn" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="sfBtn" Text="Export to CSV"
                            OnClick="btnExportToCSV_Click" meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Search Term:</label>
                                <input type="text" id="txtSearchTerm" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <button type="button" id="btnSearchTerm" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxSearchTermImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvSearchTerm" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="searchDataExportTbl" cellspacing="0" cellpadding="0" border="0" width="100%" style="display: none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvHiddenValue" runat="server" />
