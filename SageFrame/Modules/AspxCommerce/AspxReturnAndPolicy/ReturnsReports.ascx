<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReturnsReports.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxReturnAndPolicy_ReturnsReports" %>

<script type="text/javascript">
    var ReturnsHistory = '';
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxReturnAndPolicy
        });
    });
    var UserModuleIDReturn='<%= UserModuleIDReturn%>'
</script>

<div align="right">
    <label class="sfLocale">
        <b>Show Return Reports:</b></label>
    <select id="ddlReturnReport">
        <option value="1" class="sfLocale">Show Year Report</option>
        <option value="2" class="sfLocale">Show Current Month Report</option>
        <option value="3" class="sfLocale">Show Today's Report</option>
    </select></div>
<div id="gdvReturnReport_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Return Reports" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonReturn_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
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
                                <label class="cssClassLabel">
                                    <span class ="sfLocale">Return Status:</span></label><select id="ddlReturnStatus" class="sfListmenu">
                                        <option value="0" class="sfLocale">--All--</option>
                                    </select>
                            </td>
                            <td>
                                
                                        <button type="button" onclick="ReturnReport.SearchItems()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                  
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxReturnReportImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvReturnReport" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ReturnReportExportTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvRetunHiddenValue" runat="server" />
