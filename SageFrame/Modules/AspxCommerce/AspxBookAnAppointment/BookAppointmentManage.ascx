<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BookAppointmentManage.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxBookAnAppointment_BookAppointmentManage" %>

<style>

.hide
{
    display:none;
}
</style>

<script type="text/javascript">

    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxBookAnAppointment
        });
    });
    var modulePath = '<%=modulePath %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divBookAppointManage">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblBookAppointmentHeader" runat="server" Text="Appointment Management"
                    meta:resourcekey="lblBookAppointmentHeaderResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnDeleteSelectedAppointment" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportToExcel" class="sfBtn hide" runat="server" Text="Export to Excel"
                            OnClick="btnExportToExcel_Click" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCsv" runat="server" class="sfBtn hide" Text="Export to CSV"
                            OnClick="btnExportToCsv_Click" meta:resourcekey="btnExportToCsvResource1" />
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
                                    Customer Name:</label>
                                <input type="text" id="txtSearchUserName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Appointment Status:
                                </label>
                                <select id="ddlAppointmentStatus" class="sfListmenu" style="width: 150px;">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Store Location:
                                </label>
                                <select id="ddlAllStoresSearch" class="sfListmenu">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Service Provider:
                                </label>
                                <select id="ddlStoreServiceProvidersSearch" class="sfListmenu">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select>
                            </td>
                            <td>
                                <br />

                                <button type="button" id="btnSearchAppointment" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxAppointmentImageLoad" src="" class="sfLocale" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="tblBookAppointment" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="tblAppointmentExportData" cellspacing="0" cellpadding="0" border="0" width="100%"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="appointmentMainDiv" class="cssClassCommonBox Curve" style="display: none">
    <div class="cssClassAppointmentcontainer">
        <div id="" class="cssClassAppointmentContent">
            <div class="cssClassAppointmentHeading cssClassHeader">
                <h2>
                    <span class="sfLocale cssClassAppointmentHeading ">SCHEDULED APPOINTMENT</span>
                </h2>
            </div>
            <div class="sfFormwrapper">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <h3>
                                    <asp:Label runat="server" Text="Appointment for Order ID:" meta:resourcekey="LabelResource1"></asp:Label>
                                    <span id="spanOrderID"></span>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAppointmentStatus" runat="server" Text="Appointment Status:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblAppointmentStatusResource1"></asp:Label>
                            </td>
                            <td>
                                <select id="ddlAppointmentStatusList" class="sfListmenu">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select><span id="statusError"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblServiceName" runat="server" Text="Service Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblServiceNameResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="hidden" id="ServiceCategoryID" />
                                <input type="text" id="txtServiceName" name="ServiceName" class="sfInputbox cssClassDisable" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductName" runat="server" Text="Product Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblProductNameResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="hidden" id="ServiceProductID" />
                                <input type="text" id="txtProductName" name="ProductName" class="sfInputbox cssClassDisable" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductDuration" runat="server" Text="Service Duration:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblProductDurationResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtServiceDuration" name="ServiceDuration" class="sfInputbox cssClassDisable" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrice" runat="server" Text="Price:" CssClass="cssClassLabel" meta:resourcekey="lblPriceResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPrice" name="Price" class="sfInputbox cssClassFormatCurrency cssClassDisable"
                                    style="text-align: left" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStoreLocation" runat="server" Text="Store Location:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblStoreLocationResource1"></asp:Label>
                            </td>
                            <td>
                                <select id="ddlStoreLocation" class="sfListmenu">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select><span id="storeLocationError"></span>
                                <input type="button" id="btnResetData" value="Reset"  class="sfBtn" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblServiceProviderName" runat="server" Text="Service Provider Name:"
                                    CssClass="cssClassLabel" meta:resourcekey="lblServiceProviderNameResource1"></asp:Label>
                            </td>
                            <td>
                                <select id="ddlStoreServiceProvider" class="sfListmenu">
                                    <option value="0" class="sfLocale">- All -</option>
                                </select><span id="ServiceProviderError"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPreferredDate" runat="server" Text="Service Date:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblPreferredDateResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPreferredDate" name="PreferredDate" class="sfInputbox" />
                                <label id="lblAvailableDateError" class="cssClassRequired">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPreferredTime" runat="server" Text="Provider Availability:" CssClass="cssClassLabel cssClassDisable"
                                    meta:resourcekey="lblPreferredTimeResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPreferredTime" name="PreferredTime" class="sfInputbox cssClassDisable" />
                                <div class="cssClassTimeList" style="display: none; width: 200px; height: 120px; overflow: auto; background: #C8BFE1">
                                    <ul id="idServiceTimeLi" class="cssClassUlTimeList">
                                    </ul>
                                </div>
                                <label id="lblTimeError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPreferredTimeInterval" runat="server" Text="Appointment Time:"
                                    CssClass="cssClassLabel" meta:resourcekey="lblPreferredTimeIntervalResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPreferredTimeInterval" name="PreferredTimeInterval" class="sfInputbox cssClassDisable" />
                                <label id="lblBookTimeIntervalError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPaymentMethodType" runat="server" Text="Payment Method:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblPaymentMethodTypeResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPaymentMethod" name="PaymentMethod" class="sfInputbox cssClassDisable" />
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
                                    meta:resourcekey="lblFirstNameResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtFirstName" name="FirstName" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblLastNameResource1"></asp:Label>
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
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkMale" value="male" name="gender" />Male</label>
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkFemale" value="female" name="gender" />Female</label>
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkOthers" value="others" name="gender" />Others</label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblMobileNumberResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtMobileNumber" name="MobileNumber" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPhoneNumber" runat="server" Text="Telephone Number:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblPhoneNumberResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtPhoneNumber" name="PhoneNumber" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmailID" runat="server" Text="Email ID:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblEmailIDResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtEmailAddressAppointment" name="AppointmentEmail" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTypeOfTreatment" runat="server" Text="Type Of Treatment:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblTypeOfTreatmentResource1"></asp:Label>
                            </td>
                            <td>
                                <input type="text" id="txtTypeofTreatment" name="TypeOfTreatment" class="sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCustomerType" runat="server" Text="Type Of Customer:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblCustomerTypeResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td id="customerType">
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkOld" value="old" name="customerType" />Old</label>
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkNew" value="new" name="customerType" />New
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMembershipElite" runat="server" Text="Membership Elite" CssClass="cssClassLabel"
                                    meta:resourcekey="lblMembershipEliteResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td id="membershipElite">
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkYes" value="yes" name="membershipElite" />Yes</label>
                                <label class="sfLocale">
                                    <input type="checkbox" id="chkNo" value="no" name="membershipElite" />No</label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="sfButtonwrapper">
                <p>
                    <button type="button" id="btnCancelAppointment" class="sfBtn">
                        <span class="sfLocale icon-close">Cancel</span></button>
                </p>
                <p>
                    <button type="button" id="btnUpdateAppointment" class="sfBtn">
                        <span class="sfLocale icon-save">Save</span></button>
                </p>

            </div>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="AppHdnValue" runat="server" />
<asp:HiddenField ID="AppCsvHiddenValue" runat="server" />
