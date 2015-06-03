<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponPerSalesManage.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCouponManagement_CouponItemsManage" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxCouponManagement
        });
    });

    //<![CDATA[
    var couponPerSalesDataToExcel = '<%= btnExportDataToExcel.ClientID%>';
    var couponPerSalesTitle = '<%=lblTitle.ClientID %>';
    var couponPerSalesDetailTitel = '<%=lblDetailTitle.ClientID %>';
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<div id="gdvCouponPerSales_grid">
    <div class="cssClassCommonBox Curve">
        <div id="divCouponPersalesGrid" class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Coupon Per Sales" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportDataToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="btnExportDataToExcel_Click" Text="Export to Excel" meta:resourcekey="btnExportDataToExcelResource1" />
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCouponPerSale_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    <%-- <p>
                        <button type="button" id="btnBackToCouponPerSalesTbl">
                            <span><span>Back</span></span></button>
                    </p>--%>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div id="divCouponPerSalesDetail" class="cssClassHeader" style="display: none;">
            <h2>
                <asp:Label ID="lblDetailTitle" runat="server" Text="Details of coupon" meta:resourcekey="lblDetailTitleResource1"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnBackToCouponPerSalesTbl" class="sfBtn">
                            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportDetailDataToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="btnExportDetailDataToExcel_Click" Text="Export to Excel" meta:resourcekey="btnExportDetailDataToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportDetailToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="btnExportDetailToCSV_Click" Text="Export to CSV" meta:resourcekey="btnExportDetailToCSVResource1" />
                    </p>
                    
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassClear">
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="cssClassCouponCode" width="243px">
                                <label class="cssClassLabel sfLocale">
                                    Coupon Code:</label>
                                <input type="text" id="txtSearchNameCoupon" class="sfTextBoxSmall" />
                            </td>
                            <td class="cssClassOrderID" style="display: none">
                                <label class="cssClassLabel sfLocale">
                                    Order ID:</label>
                                <input type="text" id="txtSearchOrderID" class="sfTextBoxSmall" style="width:100px !important;" />
                            </td>
                            <td class="cssClassUserName" style="display: none">
                                <label class="cssClassLabel sfLocale">
                                    User Name:</label>
                                <input type="text" id="txtSearchUserName" class="sfTextBoxSmall" />
                            </td>
                            <td class="cssClassddlIspercentage" style="display: none">
                                <label class="cssClassLabel sfLocale">
                                    Is Percentage:</label>
                                <select id="ddlCouponIsPercentage" class="sfListmenu">
                                    <option value="0" class="sfLocale">--All--</option>
                                    <option value="1" class="sfLocale">Yes</option>
                                    <option value="2" class="sfLocale">No</option>
                                </select>
                            </td>
                            <td class="cssClassCouponAmount" style="display: none; width:275px;">
                                <label class="cssClassLabel sfLocale">
                                    Coupon Discount Amount:</label><br />

                                <span class="sfLocale">From:</span>
                                <input type="text" id="txtCouponAmountFrom" class="sfTextBoxSmall" style="width:85px !important;" />
                                <span class="sfLocale">To:</span>
                                <input type="text" id="txtSearchCouponAmountTo" class="sfTextBoxSmall" style="width:85px !important;"/>
                            </td>
                            <td class="cssClassGrandTotalAmount" style="display: none; width:275px;">
                                <label class="cssClassLabel sfLocale">
                                    Grand Total:</label><br />

                                <span class="sfLocale">From:</span>
                                <input type="text" id="txtGrandTotalAmountFrom" class="sfTextBoxSmall"  style="width:85px !important;"/>
                                <span class="sfLocale">To:</span>
                                <input type="text" id="txtGrandTotalAmountTo" class="sfTextBoxSmall"  style="width:85px !important;"/>
                            </td>
                            <td>

                                        <button type="button" onclick="couponPerSalesMgmt.SearchItems()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                   
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponPerSalesImage" src="" class="sfLocale" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCouponPerSales" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CouponPerSalesExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
                <table id="gdvCouponPerSalesDetailView" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CouponPerSalesExportDataTblDetail" width="100%" border="0" cellpadding="0"
                    cellspacing="0" style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvCouponPerSalesHiddenValue" runat="server" />
<asp:HiddenField ID="HdnValueDetail" runat="server" />
<asp:HiddenField ID="_csvCouponPerSalesHiddenValueDetail" runat="server" />
