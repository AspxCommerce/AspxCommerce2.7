<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerByOrderTotal.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_CustomerByOrderTotal" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCustomerManagement
        });
    });
  
     var btnExportToExcelCTO='<%=btnExportToExcelCTO.ClientID %>';

    //]]>
</script>

<div id="divCustomerOrderTotal">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReviewHeading" runat="server" 
                    Text="Customers by Total Orders" meta:resourcekey="lblReviewHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcelCTO" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" 
                            meta:resourcekey="btnExportToExcelCTOResource1"  />
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerByOrder_Click" Text="Export to CSV"
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
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                  Customer Name:</label>
                                <input type="text" id="txtSearchUserNm" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                        <button type="button" id="btnSearchCustomerTotalOrders" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerByOrderTotal" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerOrderTotal" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CustomerTotalOrderExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvCustomerByOrderHiddenValue" runat="server" />
