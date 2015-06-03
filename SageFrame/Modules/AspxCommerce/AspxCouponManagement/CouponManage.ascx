<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponManage.ascx.cs"
    Inherits="Modules_AspxCouponManagement_CouponManage" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
        moduleKey: AspxCouponManagement
        });
    });
    //<![CDATA[
    var lblCouponManageTitle = "<%=lblCouponManageTitle.ClientID %>";
    var lblCouponUserTitle = "<%=lblCouponUserTitle.ClientID %>";
    var userEmail = "<%=UserEmail %>";
    var ServerVariables = '<%=Request.ServerVariables["SERVER_NAME"]%>';
    var StoreLogoImg = '<%=StoreLogoImg %>';
    var StoreName = '<%=StoreName %>';
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<div id="divShowCouponDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Coupons" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p style="display:none">
                    <button type="button" id="btnRefresh" class="sfBtn" onclick="couponMgmt.ClearSearchTextBox()">
                           <span class="sfLocale icon-refresh">Refresh</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewCoupon" class="sfBtn">
                           <span class="sfLocale icon-addnew">Add New Coupon</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelectedCoupon" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnBackToCouponTbl" class="sfBtn" style="display:none">
                           <span class="sfLocale icon-arrow-slim-w">Back</span></button>
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
                <div class="sfFormwrapper sfTableOption cssCouponView">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="cssClassddlCouponType" width="150">
                                <label class="cssClassLabel sfLocale">
                                    Coupon Type:</label>
                                <select id="ddlSearchCouponType" class="sfListmenu">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td class="cssClassddlCouponStatus" width="150">
                                <label class="cssClassLabel sfLocale">
                                    Coupon Status:</label>
                                <select id="ddlCouponStatus" class="sfListmenu">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td class="cssClasstxtSearchUserName" width="150">
                                <label class="cssClassLabel sfLocale">
                                    UserName:</label>
                                <input type="text" id="txtSearchUserName" class="sfTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchCouponCode" width="150">
                                <label class="cssClassLabel sfLocale">
                                    Coupon Code:</label>
                                <input type="text" id="txtSearchCouponCode" class="sfTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchValidateFrom" width="80">
                                <label class="cssClassLabel sfLocale">
                                    Validate From:</label>
                                <input type="text" id="txtSearchValidateFrom" class="sfTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchValidateTo" width="80">
                                <label class="cssClassLabel sfLocale">
                                    Validate To:</label>
                                <input type="text" id="txtSearchValidateTo" class="sfTextBoxSmall" />
                            </td>
                            <td><br />

                                        <button type="button" onclick="couponMgmt.SearchCouponDetails()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponImageLoad2" />
                </div>
                <div class="log">
                </div>
                <table id="gdvCoupons" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="gdvCouponUser" cellspacing="0" cellpadding="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divCouponForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCouponManageTitle" runat="server" meta:resourcekey="lblCouponManageTitleResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <table border="0"  id="tblEditCouponForm" class="cssClassPadding">
                <tr>
                    <td >
                        <asp:Label ID="lblCouponType" Text="Coupon Type:" runat="server" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponTypeResource1"></asp:Label>
                       
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlCouponType" class="sfListmenu required">
                            <option value="0" class="sfLocale">--Select One--</option> <span class="cssClassRequired">*</span>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsForFreeShipping" runat="server" Text="Is For Free Shipping:"
                            CssClass="cssClassLabel" meta:resourcekey="lblIsForFreeShippingResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsForFreeShipping" class="sfListmenu">
                            <option value="1" class="sfLocale">NO</option>
                            <option value="2" class="sfLocale">YES</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCoupon" runat="server" Text="Coupon Code:" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponResource1"></asp:Label>
                     
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCoupon" name="newCoupon" class="sfInputbox required"
                            minlength="2" />   <span class="cssClassRequired">*</span>
                        <button type="button" id="btnGenerateCode" class="sfBtn" onclick="couponMgmt.GenerateCodeString()">
                            <span class="sfLocale icon-generate">Generate Code</span></button><span id="spancouponCode"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCouponAmount" Text="Amount:" runat="server" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponAmountResource1"></asp:Label>
                       
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAmount" name="amount" class="sfInputbox required" />
                        <select id="ddlCouponAmountType" class="sfListmenu">
                        </select><span id="couponAmountErrorLabel"></span><span id="percError" style="color: #ED1C24;"></span> <span class="cssClassRequired">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValidFrom" Text="Valid" runat="server" 
                            CssClass="cssClassLabel" meta:resourcekey="lblValidFromResource2"
                            ></asp:Label>
                     
                    </td>
                    <td class="cssClassTableRightCol">
                     <span class="sfLocale">From:</span>
                        <input type="text" id="txtValidFrom" name="validateFrom" class="from sfInputbox required" />   <span class="cssClassRequired">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                        <span class="sfLocale">To:</span>
                        <input type="text" id="txtValidTo" name="validateTo" class="to sfInputbox required" />
                        <span id="created" style="color: #ED1C24;"></span>  <span class="cssClassRequired">*</span>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="lblIsActive" runat="server" Text="Active:" CssClass="cssClassLabel"
                            meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="chkIsActive" class="cssClassCheckBox" />
                    </td>
                </tr>
                <tr class="cssClassUsesPerCoupon">
                    <td>
                        <asp:Label ID="lblUsesPerCoupon" runat="server" Text="Uses Per Coupon:" CssClass="cssClassLabel"
                            Visible="False" meta:resourcekey="lblUsesPerCouponResource1"></asp:Label>
                        <%-- <span class="cssClassRequired">*</span>--%>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtUsesPerCoupon" visible="false" name="usesPerCoupon" class="sfInputbox required" />
                    </td>
                </tr>
                <tr id="trUserPerCustomer">
                    <td>
                        <asp:Label ID="lblUsesPerCustomer" runat="server" Text="Uses Per Customer:" CssClass="cssClassLabel"
                            meta:resourcekey="lblUsesPerCustomerResource1"></asp:Label>
                        
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtUsesPerCustomer" name="userPerCustomer" class="sfInputbox required" /><span class="cssClassRequired">*</span>
                    </td>
                </tr>
                  <tr><td><label class="cssClassLabel sfLocale">Minimum Cart Amount To Apply:</label></td><td><input type="text" class="sfInputbox" id="txtApplyAmountRange"/></td></tr>
                <tr id="trCouponCustomer">
                    <td>
                        <asp:Label ID="lblPortalUser" runat="server" Text="Select Customers:" CssClass="cssClassLabel"
                            meta:resourcekey="lblPortalUserResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <div class="cssClassCommonBox Curve">
                            <div class="cssClassHeader">
                                <h2>
                                    <span id="ctl13_lblTitle" class="sfLocale">Select whom To send the Coupon Code:</span>
                                </h2>
                            </div>
                            <div class="sfGridwrapper">
                                <div class="sfGridWrapperContent">
                                    <div class="cssClassSearchPanel sfFormwrapper">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <%--<td class="cssClasstxtRoleName">
                                                    <label class="cssClassLabel">
                                                        Role Name:</label>
                                                    <input type="text" id="txtSearchRoleName" class="sfTextBoxSmall" />
                                                </td>
                                                <td class="cssClasstxtSearchUserName">
                                                <label class="cssClassLabel">
                                                    UserName:</label>
                                                <input type="text" id="txtSearchUserName" class="sfTextBoxSmall" />
                                                </td>--%>
                                                <td class="cssClasstxtCustomerName">
                                                    <label class="cssClassLabel sfLocale">
                                                        Customer Name:</label>
                                                    <input type="text" id="txtSearchCustomerName" class="sfTextBoxSmall" />
                                                </td>
                                                <td>
                                                    <div class="sfButtonwrapper cssClassPaddingNone">
                                                        <p>
                                                            <button type="button" onclick="couponMgmt.SearchCouponPortalUsers()" class="sfBtn">
                                                                <span class="sfLocale icon-search">Search</span></button>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="loading">
                                        <img id="ajaxCouponMgmtImageLoad" />
                                    </div>
                                    <div class="log">
                                    </div>
                                    <table id="gdvPortalUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="trPromoCodeItems">
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Select Items:</label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <div class="cssClassCommonBox Curve">
                            <div class="cssClassHeader">
                                <h2>
                                    <span id="Span1" class="sfLocale">Select Promo Code Items:</span>
                                </h2>
                            </div>
                            <div class="sfGridwrapper">
                                <div class="sfGridWrapperContent">
                                    <div class="sfFormwrapper sfTableOption">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="cssClasstxtCustomerName">
                                                    <label class="cssClassLabel sfLocale">
                                                        Item Name:</label>
                                                    <input type="text" id="txtItemNm" class="sfTextBoxSmall" />
                                                </td>
                                                <td>
                                                            <button type="button" onclick="couponMgmt.SearchPromoItems()" class="sfBtn">
                                                                <span class="sfLocale icon-search">Search</span></button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table id="gdvPromoItems" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancelCouponUpdate" class="sfBtn">
                   <span class="sfLocale icon-close">Cancel</span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCoupon" class="sfBtn">
                   <span class="sfLocale icon-save">Save</span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnCouponID" />
<div id="divCouponUserForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCouponUserTitle" runat="server" meta:resourcekey="lblCouponUserTitleResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <table border="0" width="100%" id="Table1" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCouponCode" Text="Coupon Code:" runat="server" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponCodeResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <label id="txtCouponCode" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="cssClassLabel"
                            meta:resourcekey="lblUserNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <label id="txtUserName" cssclass="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCouponStatus" Text="Coupon Status:" runat="server" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponStatusResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlCouponStatusType" class="sfListmenu">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
             <p>
                <button type="button" id="btnCancelCouponUserUpdate" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p> 
             <p>
                <button type="button" id="btnSubmitCouponUser" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
                      
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input type="hidden" id="hdnCouponUserID" />
<input type="hidden" id="hdnDeleteAllSelectedCouponUser" />
