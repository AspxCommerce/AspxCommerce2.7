<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceManagement.ascx.cs"
    Inherits="Modules_AspxInvoiceManagement_InvoiceManagement" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxInvoiceManagement
        });
    });
    //<![CDATA[
    var lblInvoiceForm = '<%=lblInvoiceForm.ClientID %>';
    var lblInvoiceHeader = '<%=lblInvoiceHeading.ClientID %>';
    var storeName = "<%=StoreName %>";
    var storeLogoUrl = "<%=StoreLogoUrl%>";
    var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
    var serverHostLoc = "http://" + serverLocation;
    var templatename = '<%=templateName %>';
    var noItemImagePath = '<%=NoItemImagePath %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divOrderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h1>
                    <asp:Label ID="lblInvoiceHeading" runat="server" Text="Invoices"
                        meta:resourcekey="lblInvoiceHeadingResource2"></asp:Label>
                </h1>
                <div class="cssClassHeaderRight">
                    <div class="sfButtonwrapper">
                        <p>
                            <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                                Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                        </p>
                        <p>
                            <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonInvoice_Click"
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
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        Invoice Number:
                                    </label>
                                    <input type="text" id="txtInvoiceNumber" class="sfTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        Bill To Name:
                                    </label>
                                    <input type="text" id="txtbillToName" class="sfTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        Status:
                                    </label>
                                    <select id="ddlStatus" class="sfListmenu">
                                        <option value="0" class="sfLocale">--All--</option>
                                    </select>
                                </td>
                                <td>
                                    <button type="button" onclick="invoiceMgmt.SearchInvoices()" class="sfBtn">
                                        <span class="sfLocale icon-search">Search</span></button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="loading">
                        <img id="ajaxInvoiceMgmtImageLoad" src="" alt="loading...." class="sfLocale" />
                    </div>
                    <div class="log">
                    </div>
                    <table id="gdvInvoiceDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                    </table>
                    <table id="invoiceExportDataTbl" cellspacing="0" cellpadding="0" border="0" width="100%"
                        style="display: none">
                    </table>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<%--Invoice Form --%>
<div id="divInvoiceForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblInvoiceForm" runat="server" meta:resourcekey="lblInvoiceFormResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnSavePDFForm2" runat="server" Text="Save As Pdf" OnClick="btnSavePDFForm2_Click"
                            OnClientClick="invoiceMgmt.GenerateInvoicePDF()" CssClass="cssClassButtonSubmit"
                            meta:resourcekey="btnSavePDFForm2Resource1" />
                    </p>
                    <p>
                        <button type="button" id="btnPrint" class="sfBtn">
                            <span class="sfLocale icon-print">Print</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnEmail" class="sfBtn">
                            <span class="sfLocale icon-email">Email</span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div id="divPrintInvoiceForm" class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td>
                        <img id="imgStoreLogo" src="" alt="StoreLogo" class="sfLocale" />
                    </td>
                    <td>
                        <br />
                        <b>
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No: " CssClass="cssClassLabel"
                                meta:resourcekey="lblInvoiceNoResource1"></asp:Label></b><span id="spanInvoiceNo"></span><br />
                        <%--   <b><asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date: " 
                            CssClass="cssClassLabel" meta:resourcekey="lblInvoiceDateResource1"></asp:Label></b><span
                            id="spanInvoiceDate"></span>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="lblStoreName" runat="server" Text="Store Name: " CssClass="cssClassLabel"
                                meta:resourcekey="lblStoreNameResource1"></asp:Label></b><span id="spanStoreName"></span><br />
                        <b>
                            <asp:Label ID="lblStoreDescription" runat="server" Text="Store Description: " CssClass="cssClassLabel"
                                meta:resourcekey="lblStoreDescriptionResource1"></asp:Label></b><span id="spanStoreDescription"></span><br />
                        <b>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: " CssClass="cssClassLabel"
                                meta:resourcekey="lblCustomerNameResource1"></asp:Label></b><span id="spanCustomerName"></span><br />
                        <b>
                            <asp:Label ID="lblCustomeEmail" runat="server" Text="Customer Email: " CssClass="cssClassLabel"
                                meta:resourcekey="lblCustomeEmailResource1"></asp:Label></b><span id="spanCustomerEmail"></span>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="lblOrderID" runat="server" Text="Order ID: " CssClass="cssClassLabel"
                                meta:resourcekey="lblOrderIDResource1"></asp:Label></b><span id="spanOrderID"></span>
                        <br />
                        <b>
                            <asp:Label ID="lblOrderDate" runat="server" Text="ORDER DATE: " CssClass="cssClassLabel"
                                meta:resourcekey="lblOrderDateResource1"></asp:Label></b> <span id="spanOrderDate"></span>
                        <br />
                        <b>
                            <asp:Label ID="lblOrderStatus" runat="server" Text="STATUS: " CssClass="cssClassLabel"
                                meta:resourcekey="lblOrderStatusResource1"></asp:Label></b><span id="spanOrderStatus"></span>
                        <br />
                        <b>
                            <asp:Label ID="lblPaymentMethod" runat="server" Text="PAYMENT METHOD: " CssClass="cssClassLabel"
                                meta:resourcekey="lblPaymentMethodResource1"></asp:Label></b><span id="spanPaymentMethod"></span>
                        <br />
                        <b>
                            <asp:Label ID="lblShippingMethod" runat="server" Text="SHIPPING METHOD: " CssClass="cssClassLabel"
                                meta:resourcekey="lblShippingMethodResource1"></asp:Label></b><span id="spanShippingMethod"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="cssClassShipping" id="divShippingAddressInfo">
                        </div>
                    </td>
                    <td class="cssClassTableLeftCol">
                        <div class="cssClassBilling" id="divBillingAddressInfo">
                        </div>
                    </td>
                </tr>
            </table>
            <div class="cssClassCommonBox Curve" id="invoiceOrderedDetailGrid">
                <div class="cssClassHeader">
                    <br />
                    <h3 class="sfLocale">Ordered Items:</h3>
                </div>
                <div class="sfGridwrapper">
                    <div id="divOrderItemDetails" class="sfGridWrapperContent">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
        </div>
    </div>
</div>
<asp:HiddenField ID="_csvInvoiceHiddenValue" runat="server" />
<asp:HiddenField ID="invoiceHeaderDetails" runat="server" />
<asp:HiddenField ID="hdnIsMultipleShipping" runat="server" />
<asp:HiddenField ID="hdnRemarks" runat="server" />
