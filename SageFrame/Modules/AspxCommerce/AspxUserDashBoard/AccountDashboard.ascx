<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountDashboard.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxUserDashBoard_AccountDashboard" %>

<div class="clearfix">
    <div class="welcome-msg">
        <h2 class="sub-title">
            <span class="sfLocale">Hello,</span><span id="spanUserName"></span>
        </h2>
        <p class="sfLocale">
            From your My Account Dashboard you have the ability to view a snapshot of your recent
            account activity and update your account information. Select a link below to view
            or edit information.
        </p>
    </div>
    <div class="cssCustomerRecentActivity">
        <asp:Literal ID="ltrRecentActivity" runat="server"></asp:Literal>
    </div>
</div>
<div id="divMyOrders">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span id="lblTitle" class="sfLocale">My Orders</span>
            </h2>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxAccountDashBoardImage" src="" alt="loading...." class="sfLocale" title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvMyOrders" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divOrderDetails" class="sfFormwrapper clearfix">
    <div class="cssClassStoreDetail cssClassBMar30 clearfix">
        <ul>
            <li><span class="cssClassLabel sfLocale">Ordered Date: </span><span id="orderedDate"></span></li>
            <li><span class="cssClassLabel sfLocale">Invoice Number: </span><span id="invoicedNo"></span></li>
            <li><span class="cssClassLabel sfLocale">Store Name: </span><span id="storeName"></span>
            </li>
            <li class="cssPaymentDetail"><span class="cssClassLabel sfLocale">Payment Method: </span>
                <span id="paymentMethod"></span></li>
        </ul>
    </div>
    <div class="cssClassBillingAddress cssClassStorePayment cssBox">
        <h2 class="sfLocale">Billing Address :</h2>
        <ul class="cssBillingAddressUl cssClassTMar10">
        </ul>
    </div>
    <div class="cssClassServiceDetails" style="display: none">
        <ul>
            <li>
                <h2>
                    <span class="sfLocale">Service Details:</span></h2>
            </li>
            <li>
                <label class="sfLocale">
                    Service Name:</label><span class="cssClassLabel" id="serviceName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Product Name:</label><span class="cssClassLabel" id="serviceProductName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Duration:</label><span class="cssClassLabel" id="serviceDuration"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Provider Name:</label><span class="cssClassLabel" id="providerName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Store Location:</label><span class="cssClassLabel" id="storeLocationName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Date:</label><span class="cssClassLabel" id="serviceDate"></span> </li>
            <li>
                <label class="sfLocale">
                    Available Time:</label><span class="cssClassLabel" id="availableTime"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Appointment Time:</label><span class="cssClassLabel" id="bookAppointmentTime"></span>
            </li>
        </ul>
    </div>
    <div class="cssClassHeader">
        <h2>
            <span class="cssClassLabel sfLocale">Ordered Items: </span>
        </h2>
    </div>
    <div class="sfGridwrapper clearfix">
        <table class="sfGridWrapperTable" cellspacing="0" cellpadding="0" border="0" width="100%">
            <thead>
                <tr class="cssClassHeading">
                    <td class=" sfLocale">
                        <strong>Item Name </strong>
                    </td>
                    <td class=" sfLocale">
                        <strong>SKU </strong>
                    </td>
                    <td class=" sfLocale">
                        <strong>Shipping Address </strong>
                    </td>
                    <td class=" sfLocale">
                        <strong>Shipping Rate </strong>
                    </td>
                    <td class=" sfLocale">
                        <strong>Price </strong>
                    </td>
                    <td class=" cssClassQtyTbl sfLocale">
                        <strong>Quantity </strong>
                    </td>
                    <td class=" cssClassSubTotalTbl sfLocale">
                        <strong>Sub Total </strong>
                    </td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div class="cssClassTMar20 cssClassBMar20 cssClassLMar20">
            <a href="#" id="lnkBack" class="cssClassBack sfLocale cssClassDarkBtn">Go back</a>
        </div>
    </div>
</div>
<div class="cssClassMyAddressInformation">
    <div class="cssClassHeader">
        <h2>
            <span class="sfLocale">Address Book</span>
        </h2>
    </div>
    <div class="cssClassCommonWrapper">
        <div class="cssClassCol1">
            <div class="cssClassAddressBook sfCol_48">
                <div class="cssClassShippingAdd">
                    <asp:Literal ID="ltrShipAddress" runat="server" EnableViewState="false"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="cssClassCol2">
            <div class="cssClassAddressBook sfCol_48">
                <div class="cssClassBillAdd">
                    <asp:Literal ID="ltrBillingAddress" runat="server" EnableViewState="false"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssPopUpBody">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale"><i class="i-close"></i>Close</span></button>
        </div>
        <h2>
            <span id="lblAddressTitle" class="sfLocale">Address Details</span>
        </h2>
        <div class="sfFormwrapper cssClassTMar10">
            <div id="tblNewAddress">
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFirstNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2"
                            maxlength="40" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="cssClassLabel"
                                meta:resourcekey="lblLastNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtLastName" name="LastName" class="required" minlength="2"
                            maxlength="40" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel" meta:resourcekey="lblEmailResource1"></asp:Label><span
                                class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtEmailAddress" name="Email" class="required email" minlength="2" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblCompany" Text="Company:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblCompanyResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtCompanyName" name="Company" maxlength="40" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblAddress1" Text="Address 1:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblAddress1Resource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtAddress1" name="Address1" class="required" minlength="2"
                            maxlength="250" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblAddress2" Text="Address 2:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblAddress2Resource1"></asp:Label>
                        </div>
                        <input type="text" id="txtAddress2" name="Address2" maxlength="250" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblCountry" Text="Country:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblCountryResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <asp:Literal ID="ltrCountry" runat="server" EnableViewState="false"></asp:Literal>
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblState" Text="State/Province:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblStateResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtState" name="State" class="required" minlength="2" maxlength="250" />
                        <select id="ddlUSState" class="sfListmenu">
                        </select>
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblZip" Text="Zip/Postal Code:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblZipResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtZip" name="Zip" class="required alpha_dash" minlength="4"
                            maxlength="10" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblCityResource1"></asp:Label><span
                                class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtCity" name="City" class="required" minlength="2" maxlength="250" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblPhoneResource1"></asp:Label><span
                                class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtPhone" name="Phone" class="required number" minlength="7"
                            maxlength="20" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblMobile" Text="Mobile:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblMobileResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtMobile" name="Mobile" class="number" minlength="10" maxlength="20" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblFaxResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtFax" name="Fax" class="number" maxlength="20" minlength="7" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblWebsite" Text="Website:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblWebsiteResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtWebsite" name="Wedsite" class="url" maxlength="50" />
                    </li>
                </ul>
                <ul id="trShippingAddress" class="clearfix">
                    <li class="cssBillingChk">
                        <input type="checkbox" id="chkShippingAddress" class="sfLocale" />
                        <asp:Label ID="lblDefaultShipping" Text=" Use as Default Shipping Address" runat="server"
                            CssClass="cssClassLabel" meta:resourcekey="lblDefaultShippingResource1"></asp:Label>
                    </li>
                </ul>
                <ul id="trBillingAddress" class="clearfix">
                    <li class="cssBillingChk">
                        <input type="checkbox" id="chkBillingAddress" class="sfLocale" />
                        <asp:Label ID="lblDefaultBilling" Text="Use as Default Billing Address" runat="server"
                            CssClass="cssClassLabel" meta:resourcekey="lblDefaultBillingResource1"></asp:Label>
                    </li>
                </ul>
            </div>
            <div class="sfButtonwrapper">
                <label class="icon-save cssClassGreenBtn">
                    <button type="button" id="btnSubmitAddress" class="cssClassButtonSubmit">
                        <span class="sfLocale">Save</span></button></label>
            </div>
        </div>
    </div>
</div>
<%--<div id="divLoadUserControl" class="cssClasMyAccountInformation">
    <div class="cssClassMyDashBoardInformation">
    </div>
</div>--%>
<input type="hidden" id="hdnAddressID" />
<input type="hidden" id="hdnDefaultShippingExist" />
<input type="hidden" id="hdnDefaultBillingExist" />
<script type="text/javascript">
    //<![CDATA[
    var defaultShippingExist = '<%=defaultShippingExist %>';
    var defaultBillingExist = '<%=defaultBillingExist %>';
    var addressId = '<%=addressId%>';

    $(".sfLocale").localize({
        moduleKey: AspxUserDashBoard
    });
    $(".sfLocale").localize({
        moduleKey: AspxWishItems
    });
    //]]>
</script>