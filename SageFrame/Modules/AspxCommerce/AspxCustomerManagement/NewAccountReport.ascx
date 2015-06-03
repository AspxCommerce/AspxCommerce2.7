<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewAccountReport.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxNewAccountReport_NewAccountReport" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxCustomerManagement
        });
    });
  
    var btnExportToExcelNAR='<%=btnExportToExcelNAR.ClientID %>';
//]]>
</script>


<div id="divNewAccountDetailsByMonthly">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReviewsGridHeading" runat="server" Text="New Accounts" 
                    meta:resourcekey="lblReviewsGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcelNAR" class="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" 
                            meta:resourcekey="btnExportToExcelNARResource1" />
                    </p>
                    <p>
                       <%-- <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonNewAccount_Click" Text="Export to CSV" 
                            meta:resourcekey="btnExportToCSVResource1"/>
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
                    <table  width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                        <td> <label>
        <b class="sfLocale">Show Reports:</b></label>
    <select id="ddlNewAccountReport">
        <option value="1" class="sfLocale">Show Year Monthly Report</option>
        <option value="2" class="sfLocale">Show Current Month Weekly Report</option>
        <option value="3" class="sfLocale">Show Today's Report</option>
    </select>
                        </td>
                        </tr>
                        </table>
                        </div>
                        <div class="loading">
                    <img id="ajaxNewAccountReportImageLoad" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvNewAccountList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="NewAccountExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvNewAccountHiddenValue" runat="server" />
