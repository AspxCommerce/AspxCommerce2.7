<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingReport.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShippingReport_ShippingReport" %>

<script type="text/javascript">
    //<![CDATA[

    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxShippingManagement
        });
    }); var umi = '<%=UserModuleID%>';

    //]]>
</script>


<div id="divShippiedReport">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Shipping Reports" meta:resourcekey="lblOrderHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <%-- <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonShippingCsv_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="328px">
                                <label class="cssClassLabel">
                                    <span class="sfLocale">Shipping Method Name:</span></label>
                                <input type="text" id="txtShippingMethodNm" class="sfTextBoxSmall" />
                            </td>
                            <td>

                                <button type="button" onclick="ShippingReport.SearchShippingReport()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>

                            </td>
                            <td align="right">

                                <label>
                                    <b class="sfLocale">Show Shipping Reports:</b></label>
                                <select id="ddlShippingReport">
                                    <option value="1" class="sfLocale">Show Year Report</option>
                                    <option value="2" class="sfLocale">Show Current Month Report</option>
                                    <option value="3" class="sfLocale">Show Today's Report</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxShippingReportImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShippedReportDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="ShippingDataExportTbl" cellspacing="0" cellpadding="0" border="0" width="100%"
                    style="display: none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvShippingHiddenValue" runat="server" />
