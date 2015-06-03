<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BookAppointment.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxServiceItems_BookAppointment" %>

<div id="appointmentMainDiv" class="cssClassAppointmentMainDiv">
    <div class="cssClassAppointmentcontainer">
        <div id="" class="cssClassAppointmentContent">
            <div class="cssClassAppointmentHeading">
                <h2 class="cssClassMiddleHeader">
                    <span class="sfLocale cssClassAppointmentHeading">Schedule an Appointment</span>
                </h2>
            </div>
            <div class="cssClassAppointmentBody">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr class="cssClassServiceInfo">
                            <td colspan="2" width="0%">
                                <asp:Label ID="lblServiceDescription" runat="server" CssClass="cssClassLabel" Text="Service Description"
                                    meta:resourcekey="lblServiceDescriptionResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="22%">
                                <asp:Label ID="lblServices" runat="server" Text="Available Services:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblServicesResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <select id="ddlServices" class="sfListmenu">
                                    <option value="0">--- </option>
                                </select><span id="serviceError"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="cssServiceProducts">
                                    <asp:Label ID="lblServiceProducts" runat="server" Text="Service Products:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblServiceProductsResource1"></asp:Label><span class="cssClassRequired">*</span></span>
                            </td>
                            <td>
                                <select id="ddlServiceProducts" class="sfListmenu">
                                    <option value="0">--- </option>
                                </select><span id="serviceProductError"></span>
                            </td>
                        </tr>
                        <tr class="cssServiceProductDuration" style="display: none">
                            <td>
                                <span>
                                    <asp:Label ID="lblServiceDuration" runat="server" CssClass="cssClassLabel" Text="Service Duration:"
                                        meta:resourcekey="lblServiceDurationResource1"></asp:Label>
                                    <%--<input id="serviceProductDuration" class="cssServiceDuration" type="text" value=""/>--%>
                                    <td>
                                        <label id="serviceProductDuration" style="color: #686868">
                                        </label>
                                    </td>
                                </span>
                            </td>
                        </tr>
                        <tr class="cssServiceProductsPrice" style="display: none">
                            <td>
                                <span>
                                    <asp:Label ID="lblServiceProductPrice" runat="server" CssClass="cssClassLabel" Text="Product Price:"
                                        meta:resourcekey="lblServiceProductPriceResource1"></asp:Label>
                                    <%--<input id="serviceProductPrice" class="cssProductPrice cssClassFormatCurrency" type="text" value=""/>--%>
                                    <td>
                                        <label id="serviceProductPrice" class="cssProductPrice cssClassFormatCurrency" style="color: #686868">
                                        </label>
                                    </td>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStoreLocation" runat="server" Text="Store Location:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblStoreLocationResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <select id="ddlStoreLocation" class="sfListmenu">
                                    <option value=""></option>
                                </select><span id="storeError"></span>
                            </td>
                        </tr>
                        <tr class="cssClassServiceProvider" style="display: none">
                            <td>
                                <asp:Label ID="lblStoreServiceProvicer" runat="server" Text="Service Providers:"
                                    CssClass="cssClassLabel" meta:resourcekey="lblStoreServiceProvicerResource1"></asp:Label><span
                                        class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <select id="ddlStoreServiceProviders" class="sfListmenu">
                                    <option value="0">--</option>
                                </select><span id="serviceProviderError"></span>
                            </td>
                        </tr>
                        <tr class="cssClassAvailableDate" style="display: none">
                            <td>
                                <asp:Label ID="lblAvailableDate" runat="server" Text="Available Date:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblAvailableDateResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtAvailableDate" name="PreferredDate" class="sfInputbox availableDate" />
                                <label id="lblAvailableDateError" class="cssClassRequired">
                                    *</label>
                            </td>
                        </tr>
                        <tr class="cssClassAvailableTime" style="display: none">
                            <td>
                                <asp:Label ID="lblAvailableTime" runat="server" Text="Available Time:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblAvailableTimeResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <div class="cssClassTimeList" style="width: 220px; height: 120px; overflow: auto;
                                    background: #C8BFE1">
                                    <ul id="idServiceTimeLi" class="cssClassUlTimeList">
                                    </ul>
                                </div>
                                <label id="lblTimeError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr class="cssClassPersonalInfo">
                            <td colspan="2">
                                <asp:Label ID="lblUserDescription" runat="server" CssClass="cssClassLabel" Text="Personal Information"
                                    meta:resourcekey="lblUserDescriptionResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitle" runat="server" Text="Title:" CssClass="cssClassLabel" meta:resourcekey="lblTitleResource1"></asp:Label>
                            </td>
                            <td>
                                <select id="ddlTitle" class="sfListmenu">
                                    <option></option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblFirstNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtFirstName" name="FirstName" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblLastNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtLastName" name="LastName" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblGender" runat="server" Text="Gender:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblGenderResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td id="chkGender">
                                <input type="checkbox" id="chkMale" value="male" name="gender" /><label class="sfLocale">Male</label>
                                <input type="checkbox" id="chkFemale" value="female" name="gender" /><label class="sfLocale">Female</label>
                                <input type="checkbox" id="chkOthers" value="others" name="gender" /><label class="sfLocale">Others</label>
                                <label id="genderError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblMobileNumberResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtMobileNumber" name="MobileNumber" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPhoneNumber" runat="server" Text="Telephone Number:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblPhoneNumberResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtPhoneNumber" name="PhoneNumber" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmailID" runat="server" Text="Email ID:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblEmailIDResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtEmailAddressAppointment" name="AppointmentEmail" class="sfInputbox required" />
                            </td>
                        </tr>
                        <%--   <tr>
                            <td>
                                 <asp:Label ID="lblPreferredDate" runat="server" Text="Preferred Date:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                 <input type="text" id="txtPreferredDate" name="PreferredDate" class="sfInputbox required"/>
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                            <asp:Label ID="lblPreferredTime" runat="server" Text="Preferred Time:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                 <select id="ddlHour" class="sfListmenu">
                              <option></option>
                                 </select><label class="cssClassLabel"> : </label>
                                 <select id="ddlMinute" class="sfListmenu">
                               <option></option>
                                 </select><label class="cssClassLabel"> : </label>
                                 <select id="ddlAmPm" class="sfListmenu">
                                     <option value="am">AM</option>
                                     <option value="pm">PM</option>
                                 </select>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblTypeOfTreatment" runat="server" Text="Type Of Treatment:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblTypeOfTreatmentResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                                <input type="text" id="txtTypeofTreatment" name="TypeOfTreatment" class="sfInputbox required" />
                            </td>
                        </tr>
                        <%--  <tr>
                            <td>
                              <asp:Label ID="lblStoreLocation" runat="server" Text="Store Location:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td>
                               <select id="ddlStoreLocation" class="sfListmenu">
                                 <option value=""></option>
                                </select>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblCustomerType" runat="server" Text="Type Of Customer:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblCustomerTypeResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td id="customerType">
                                <input type="checkbox" id="chkOld" value="old" name="customerType" /><label class="sfLocale">Old</label>
                                <input type="checkbox" id="chkNew" value="new" name="customerType" /><label class="sfLocale">New</label>
                                <label id="customerTypeError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMembershipElite" runat="server" Text="Membership Elite" CssClass="cssClassLabel"
                                    meta:resourcekey="lblMembershipEliteResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td id="membershipElite">
                                <input type="checkbox" id="chkYes" value="yes" name="membershipElite" /><label class="sfLocale">Yes</label>
                                <input type="checkbox" id="chkNo" value="no" name="membershipElite" /><label class="sfLocale">No</label>
                                <label id="membershipeEliteError">
                                </label>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                <input type="button" id="btnSubmit" value="Submit" />
                            </td>
                            <td></td>
                        </tr>--%>
                    </tbody>
                </table>
            </div>
            <div id="dvPaymentInfo" class="cssClassPaymentMethods">
                <div class="cssClassPaymentList">
                    <asp:Label ID="lblPaymentListHeader" CssClass="cssClassLabel" runat="server" Text="Payment Information"
                        meta:resourcekey="lblPaymentListHeaderResource1"></asp:Label>
                    <div id="dvPGList">
                    </div>
                </div>
                <%-- <div class="sfButtonwrapper cssClassRightBtn">
                    <button id="btnPaymentInfoBack" type="button" value="" class="back">
                        <span><span class="sfLocale">Back</span></span></button>
                    <button id="btnPaymentInfoContinue" type="button" value="" class="continue">
                        <span><span class="sfLocale">Continue</span></span></button>
                </div>--%>
</div>
            <div id="dvPlaceOrder" class="cssClassOrderReview cssClassTMar30">
                <div class="sfButtonwrapper ">
                    <%-- <button id="btnPlaceBack" type="button" value="back" class="back">
                            <span><span class="sfLocale">Back</span></span></button>--%>
                    <button id="btnPlaceOrder" type="button" class="submit cssClassGreenBtn">
                        <span class="sfLocale">Place Order</span></button>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxServiceLocale
        });
        $(this).BookAppointment({
            serviceModulePath: '<%=serviceModulePath %>',
            appointmentSuccessPage: '<%=appointmentSuccessPage %>'
        });
    });    
</script>