<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderManagement.ascx.cs"
    Inherits="Modules_AspxOrderManagement_OrderManagement" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxOrderManagement
        });
    });
        //<![CDATA[
        var lblOrderForm1 = "<%= lblOrderForm.ClientID %>";
        var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
        var serverHostLoc = "http://" + serverLocation;
        var storeName = '<%=StoreName %>';
        var templatename = '<%=templateName %>';
        var newOrderRss = '<%=NewOrderRss %>';
        var rssFeedUrl = '<%=RssFeedUrl %>';
        var wareHouseAddress = '<%=WareHouseAddress %>';
        var orderNotificationID = strDecrypt(getParameterByName("orderid"));
        var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    var umi = '<%=UserModuleID%>';
        //]]>
	//]]>
</script>

<div id="divOrderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Orders" 
                    meta:resourcekey="lblOrderHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassRssDiv">
                <a href="#" style="display: none">
                    <img id="orderRssImage" alt="" src="" title="" />
                </a>
            </div>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <%--<p>
                        <button type="button" id="btnCreateNewOrder">
                            <span><span>Create New Order</span></span></button>
                    </p>--%>
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" 
                            meta:resourcekey="btnExportToExcelResource1" /><%--OnClientClick="OrderManage.ExportDivDataToExcel()" --%>
                    </p>
                    <p>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonOrder_Click" Text="Export to CSV"  
                                meta:resourcekey="btnExportToCSVResource1"/>  <%--OnClientClick="OrderManage.ExportOrdersToCsvData()"--%>
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
                                    Customer Name:</label>
                                <input type="text" id="txtCustomerName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Order Status:</label>
                                <select id="ddlOrderStatus" class="sfListmenu">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td>
                                        <button type="button" onclick="OrderManage.SearchOrders()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxOrderMgmtStaticImage" src=""  alt="loading...." class="sfLocale"/>
                </div>
                <div class="log">
                </div>
                <table id="gdvOrderDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="orderExportData" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div class="sfFormwrapper" style="display:none">
<div class="cssClassCommonBox Curve" id="divOrderDetailForm">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblOrderForm" runat="server" 
                meta:resourcekey="lblOrderFormResource1"></asp:Label>
        </h2>
    </div>
    <div id="divOrderDetailHead">
        <div class="cssClassStoreDetail">
            <b><span class="cssClassLabel sfLocale">Ordered Date :</span></b><span id="OrderDate"></span>
            <br />
            <b><span class="cssClassLabel sfLocale">Invoice Number :</span></b><span id="invoiceNo">
            </span>
            <br />
            <b><span class="cssClassLabel sfLocale">Store Name : </span></b><span id="storeName"></span>
            <br />
            <b><span class="cssClassLabel sfLocale">Store Description : </span></b><span id="storeDescription">
            </span>
            <br />
            <div class="cssPaymentDetail">
                <b><span class="cssClassLabel sfLocale">Payment Method : </span></b><span id="PaymentMethod">
                </span>
            </div>
        </div>
        
        <div class="cssClassBillingAddress cssClassStorePayment">
            <ul class="cssBillingAddressUl">
            </ul>
        </div>
        <br />
    </div>
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2 class="sfLocale">
                Order Details:</h2>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                              <td class="sfLocale cssClassTDHeading" >
                                SKU
                            </td>
                            <td  class="sfLocale cssClassTDHeading">
                                Item Name
                            </td>
                            <td class="sfLocale cssClassTDHeading">
                                Shipping Method
                            </td>
                            <td class="sfLocale cssClassTDHeading" >
                                Shipping Address
                            </td>
                            <td class="sfLocale cssClassTDHeading">
                                Shipping Rate
                            </td>
                            <td class="sfLocale cssClassTDHeading" >
                                Unit Price
                            </td>
                            <td class="sfLocale cssClassTDHeading" >
                                Quantity
                            </td>
                            <td class="sfLocale cssClassTDHeading">
                                Line Total
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <span class="remarks"></span>
            </div>
        </div>
    </div>
    <div class="sfButtonwrapper">
        <p>
            <button type="button" id="btnBack" class="sfBtn">
                <span class="sfLocale icon-arrow-slim-w">Back</span></button>
        </p>
           <p>
            <button type="button" id="btnCreateShippingLabel" class="sfBtn">
                <span class="sfLocale icon-addnew">Create/View Shipping Label</span></button>
        </p>
    </div>
</div>
</div>
<div id="divEditOrderStatus" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderStatusHeading" runat="server" Text="Edit Order Status :" 
                    meta:resourcekey="lblOrderStatusHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table id="tblOrderStatusEditForm" cellspacing="0" cellpadding="0" border="0" width="100%"
                class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" 
                            CssClass="cssClassLabel" meta:resourcekey="lblCustomerNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="customerNameEdit"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderDate" runat="server" Text="Ordered Date :" 
                            CssClass="cssClassLabel" meta:resourcekey="lblOrderDateResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanOrderDate"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderGrandTotal" runat="server" Text="Order Total :" 
                            CssClass="cssClassLabel" meta:resourcekey="lblOrderGrandTotalResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span class="cssClassFormatCurrency" id="OrderGrandTotal"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status :" 
                            CssClass="cssClassLabel" meta:resourcekey="lblOrderStatusResource1"></asp:Label>
                    </td>
                    <td>
                        <select id="selectStatus" class="sfListmenu" name="" title="Order Status List">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnSPBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
            <p>
                <button type="button" id="btnUpdateOrderStatus" class="cssClassButtonSubmit sfBtn" value="">
                    <span class="sfLocale icon-refresh">Update</span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input type="hidden" id="hdnOrderID" />
<input type="hidden" id="hdnReceiverEmail" />
<input type="hidden" id="hdnInvoice" />
<asp:HiddenField ID="_csvOrderHdnValue" runat="server" />