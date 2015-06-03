<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LowStockItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemsManagement_LowStockItems" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxItemsManagement
        });
    });
    var btnExportToExcel='<%=btnExportToExcel.ClientID %>';
    var lowStock = '<%=LowStockQuantity%>';
    //]]>
</script>

<div id="gdvLowStockItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Low Stock" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" 
                            meta:resourcekey="btnExportToExcelResource1"  />
                    </p>
                    <p>
                      <%--  <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonLowStock_Click" Text="Export to CSV" 
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
                                   Item Name:</label>
                                <input type="text" id="txtSearchName" class="sfTextBoxSmall" />
                            </td>
                   <td>
                                <label class="cssClassLabel sfLocale">
                                    SKU:</label>
                                <input type="text" id="txtSearchSKU" class="sfTextBoxSmall" />
                            </td>
                  <td>
                                <label class="cssClassLabel sfLocale">
                                    Active:</label>
                                <select id="ddlIsActive" class="sfListmenu">
                                    <option value="" class="sfLocale">--All--</option>
                                    <option value="True" class="sfLocale">True</option>
                                    <option value="False" class="sfLocale">False</option>
                                </select>
                            </td>
                            <td>
                               
                                        <button type="button" id="btnSearchLowStockItems" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                  
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxLowStockItemImage" src=""  alt="loading...." title="loading...." class="sfLocale"/>
                </div>
                <div class="log">
                </div>
                <table id="gdvLowStockItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="LowStockExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvLowStockHiddenCsv" runat="server" />
