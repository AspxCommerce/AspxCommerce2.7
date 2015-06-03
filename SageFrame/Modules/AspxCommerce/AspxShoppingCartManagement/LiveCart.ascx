<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LiveCart.ascx.cs" Inherits="Modules_AspxShoppingCartManagement_LiveCart" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxShoppingCartManagement
        });
    });
    var timeToAbandonCart = '<%= TimeToAbandonCart%>';
    //]]>
</script>


<div id="divShoppingCartItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCartItemGridHeading" runat="server" Text="Items In Carts" meta:resourcekey="lblCartItemGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnLiveCartExportToExcel" class="cssClassButtonSubmit" runat="server"
                            OnClick="btnLiveCartExportToExcel_Click" Text="Export to Excel" meta:resourcekey="btnLiveCartExportToExcelResource1" />
                    </p>
                    <p>
                        <%--  <button type="button" id="btnLiveCartExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                        <asp:Button ID="btnLiveCartExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonLiveCart_Click" Text="Export to CSV" meta:resourcekey="btnLiveCartExportToCSVResource1" />
                    </p>
                    <%--                    <p>
                        <button type="button" id="btnDeleteAllSearchTerm">
                            <span><span>Delete All Selected</span> </span>
                        </button>
                    </p>--%>
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
                                    Item Name:</label>
                                <input type="text" id="txtSearchItemName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Customer Name</label>
                                <input type="text" id="txtCustomerName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    <span class="sfLocale">Quantity:</span></label>
                                <input type="text" id="txtQuantity" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <button type="button" id="btnLiveSearch" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxLiveCartImage" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShoppingCart" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="liveCartExportData" cellspacing="0" cellpadding="0" border="0" width="100%"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnLiveCartValue" runat="server" />
<asp:HiddenField ID="_csvLiveCartHiddenValue" runat="server" />
