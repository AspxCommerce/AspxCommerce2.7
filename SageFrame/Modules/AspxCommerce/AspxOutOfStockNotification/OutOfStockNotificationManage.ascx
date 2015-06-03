<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OutOfStockNotificationManage.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxOutOfStockNotification_OutOfStockNotificationManage" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxOutOfStockNotification
        });
    });
    UserEmail = "<%=userEmail %>";
    var umi = '<%=UserModuleID%>';
</script>

<div id="divShowNotification">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblOutofStockNotificationManage"  runat="server" 
                    meta:resourcekey="lblOutofStockNotificationManageResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnShow" class="sfBtn">
                            <span class="sfLocale icon-show">Show All</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                    <%--<p>
                        <button type="button" id="btnEmailSelected">
                            <span><span>Send Email To Selected</span></span></button>
                    </p>--%>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                        <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Item SKU:</label>
                                <input type="text" id="txtProductName" class="sfTextBoxSmall" />
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                   Customer:</label>
                                <input type="text" id="txtCustomerName" class="sfTextBoxSmall" />
                            </td>
                        <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Mail Status:</label>
                                <select id="txtMailStatus" class="sfSelect">
                                    <option value="0" class="sfLocale">--All--</option>
                                    <option value="Yes" class="sfLocale">Send</option>
                                    <option value="No" class="sfLocale">Not Send</option>
                                </select>
                            </td>
                           <%-- <td>
                            <label class="cssClassLabel">
                                    Item Status:</label>
                                <select id="ddlItemStatus">
                                    <option value="0">Select Item Status</option>
                                    <option value="Available">Available</option>
                                    <option value="OutofStock">OutofStock</option>
                                </select>
                            
                            </td>--%>
                            <td><br />
                                        <button id="btnSearch" type="button"  class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxImageLoad" />
                </div>
                <div class="log">
                </div>
                <table id="tblOutofStockNotification" cellspacing="0" cellpadding="0" border="0"
                    width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
