<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Returns.ascx.cs" Inherits="Modules_AspxCommerce_AspxReturnAndPolicy_Returns" %>

<script type="text/javascript">
    $(function() {
        var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
        var serverHostLoc = "http://" + serverLocation;
        $(".sfLocale").localize({
            moduleKey: AspxReturnAndPolicy
        });
    });
    var lblReturnForm1 = "<%= lblReturnForm1.ClientID %>";
    var senderEmail = '<%=SenderEmail %>';
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';

</script>

<div id="divReturnDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReturnHeading" runat="server" Text="Returns" meta:resourcekey="lblReturnHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                             meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonReturn_Click"
                             meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table breturn="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Return ID:</label>
                                <input type="text" id="txtReturnID" class="sfTextBoxSmall" style="width:90px !important;" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Order ID:</label>
                                <input type="text" id="txtOrderID" class="sfTextBoxSmall" style="width:90px !important;" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Customer Name:</label>
                                <input type="text" id="txtCustomerName" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Return Status:</label>
                                <select id="ddlReturnStatus" class="sfListmenu sfTextBoxSmall" style="width:90px !important;">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Date Added:</label>
                                <input type="text" id="txtDateAdded" class="sfTextBoxSmall" style="width:90px !important;" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Date Modified:</label>
                                <input type="text" id="txtDateModified" class="sfTextBoxSmall" style="width:90px !important;"/>
                            </td>
                            <td>
                               <br />

                                        <button type="button" onclick="ReturnManage.SearchReturns()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                   
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxReturnMgmtStaticImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvReturnDetails" cellspacing="0" cellpadding="0" breturn="0" width="100%">
                </table>
                <table id="ReturnExportData" cellspacing="0" cellpadding="0" breturn="0" width="100%"
                    style="display: none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div class="sfFormwrapper" style="display: none">
    <div class="cssClassCommonBox Curve" id="divReturnDetailForm" >
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReturnForm1" runat="server" meta:resourcekey="lblReturnFormResource1"></asp:Label>
            </h2>
        </div>
        <div id="divReturnDetailHead">
            <div class="cssClassBillingAddress clearfix">
                <table id="tblShippingInfo" width="100%">
                    <tr>
                        <td style="text-align: left; vertical-align:top;">
                            <h4>
                                <span class="cssClassLabel sfLocale">Return Shipping Address :</span></h4>
                            <ul id="ulReturnAddress" class="">
                            </ul>
                        </td>
                        <td style="text-align: left; vertical-align:top;">
                            <h4>
                                <span class="cssClassLabel sfLocale">Order Shipping Address :</span></h4>
                            <ul id="ulOrderAddress" class="">
                            </ul>
                        </td>
                        <td style="text-align: left; vertical-align:top;">
                            <div id="ShippingDetails">
                                <span class="cssClassLabel sfLocale"><strong>Shipping Method :</strong></span>
                                <ul id="ulShippingMethods" class="cssClassLabel">
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clear">
            </div>
            <div id="divItemReturnDetailsForm" class="sfPriMrg-t">
                <div id="divReturnInfo" >
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h3 class="sfLocale">
                                <span class="sfLocale">Item Return Information:</span></h3>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <table class="sfGridWrapperTable" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <thead>
                                        <tr class="cssClassHeading">
                                            <td class="header sfLocale">
                                                Return ID
                                            </td>
                                            <td class="header sfLocale">
                                                Item Name
                                            </td>
                                            <td class="header sfLocale">
                                                Variants
                                            </td>
                                            <td class="header sfLocale">
                                                Qty.
                                            </td>
                                            <td class="header sfLocale">
                                                Reson For Return
                                            </td>
                                            <td class="header sfLocale">
                                                Other Faults
                                            </td>
                                            <td class="header sfLocale">
                                                Item Condition
                                            </td>
                                            <td class="header sfLocale">
                                                Return Staus
                                            </td>
                                            <td class="header sfLocale">
                                                Return Action
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divSaveComments" class="sfPriMrg-t">
                <div class="cssClassCommonBox Curve">
                    <div class="cssClassHeader">
                        <h3>
                            <asp:Label ID="lblCommentHeading" runat="server" Text="Comments :" meta:resourcekey="lblCommentHeadingResource1"></asp:Label>
                        </h3>
                    </div>
                    <div>
                        <div id="divComments">
                            <br />
                            <div>
                                &nbsp;&nbsp;<span id="spanPostComments" class="cssClassspanPostComments"><b>Post Comments
                                    :</b></span>
                            </div>
                            <div>
                                <span>&nbsp;&nbsp;<textarea id="txtAreaComments" cols="20" rows="5" style="width: 800px;
                                    height: 76px;"></textarea><br />
                                </span>
                            </div>
                            <div>
                                <span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="sfLocale"><b>Notify Customer by Email</b></span>
                                    &nbsp;&nbsp;
                                    <input type="checkbox" id="chkIsCustomerNotifyByEmail" class="cssClasschkIsCustomerNotifyByEmail" />
                                </span><span id="spanSaveComments" class="cssClassspanSaveComments">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <button type="button" id="btnSaveComments" class="sfBtn" value="">
                                        <span class="sfLocale icon-send">Post Comment</span></button>
                                </span>
                            </div>
                        </div>
                        <br />
                        <div id="divCommentsList" class="cssClassdivCommentsList">
                            <ul id="ulCommentsList" class="">
                            </ul>
                        </div>
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
<div id="divEditReturnStatus" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReturnStatusHeading" runat="server" Text="Edit Return Status :"
                    meta:resourcekey="lblReturnStatusHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table id="tblReturnStatusEditForm" cellspacing="0" cellpadding="0" breturn="0" width="100%"
                class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblReturnFiledDate" runat="server" Text="Return Filed Date :" CssClass="cssClassLabel sfLocale"
                            meta:resourcekey="lblReturnFiledDateResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanReturnFiledDate"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderDate" runat="server" Text="Order Date :" CssClass="cssClassLabel"
                            meta:resourcekey="lblOrderDateResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanOrderDate"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReturnID" runat="server" Text="Return ID :" CssClass="cssClassLabel"
                            meta:resourcekey="lblReturnIDResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanReturnID"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderID" runat="server" Text="Order ID :" CssClass="cssClassLabel"
                            meta:resourcekey="lblOrderIDResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanOrderID"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" CssClass="cssClassLabel"
                            meta:resourcekey="lblCustomerNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanCustomerName"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReturnStaus" runat="server" Text="Return Status :" CssClass="cssClassLabel"
                            meta:resourcekey="lblReturnStausResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="selectReturnStatus" class="sfListmenu" name="" title="Return Status List">
                            <option value="0" class="sfLocale">--Select--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReturnAction" runat="server" Text="Return Action :" CssClass="cssClassLabel"
                            meta:resourcekey="lblReturnActionResource1"></asp:Label>
                    </td>
                    <td>
                        <select id="selectReturnAction" class="sfListmenu" name="" title="Return Action List">
                            <option value="0" class="sfLocale">--Select--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOtherPostalCharges" runat="server" Text="Other Postal Charges :"
                            CssClass="cssClassLabel" meta:resourcekey="lblOtherPostalChargesResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="hidden" id="hdnOtherPostalCharges" />
                        <input type="text" id="txtOtherPostalCharges" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span id="spanShippingMethod" class="cssClassSpan"></span>
                    </td>
                    <td>
                        <input type="hidden" id="hdnShippingCost" />
                        <select id="selectShippingMethod" class="sfListmenu" name="" title="Shipping Method List">
                            <option value="0" class="sfLocale">--Select--</option>
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
                <button type="button" id="btnUpdateReturnStatus" class="cssClassButtonSubmit sfBtn" value="">
                    <span class="sfLocale icon-refresh">Update</span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input type="hidden" id="hdnReturnID" />
<input type="hidden" id="hdnOrderID" />
<input type="hidden" id="hdnCustomerID" />
<input type="hidden" id="hdnUserName" />
<input type="hidden" id="hdnReturnDate" />
<input type="hidden" id="hdnItemName" />
<input type="hidden" id="hdnVariant" />
<input type="hidden" id="hdnQty" />
<input type="hidden" id="hdnReturnStatus" />
<input type="hidden" id="hdnReturnAction" />
<input type="hidden" id="hdnRcvrEmail" />
<asp:HiddenField ID="_csvReturnHdnValue" runat="server" />
