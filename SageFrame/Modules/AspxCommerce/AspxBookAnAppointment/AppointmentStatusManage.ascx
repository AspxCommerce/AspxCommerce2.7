<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppointmentStatusManage.ascx.cs" Inherits="Modules_AspxCommerce_AspxBookAnAppointment_AppointmentStatusManage" %>
<script type="text/javascript">
    //<![CDATA[
    var lblHeading = "<%= lblAppStatusHeading.ClientID %>";
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxBookAnAppointment
        });
    });
    var umi = '<%=UserModuleID%>';
//]]>
</script>

<div id="divAppointmentStatusDetail">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAppStatusHeading" runat="server" 
                    Text="Manage Appointment Status" meta:resourcekey="lblAppStatusHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table cellspacing="0" cellpadding="0" border="0" >
                        <tr>
                            <td >
                                <asp:Label ID="lblStatusName" runat="server" CssClass="cssClassLabel" 
                                    Text="Status Name:" meta:resourcekey="lblStatusNameResource1"
                                    ></asp:Label>
                                <input type="text" id="txtSearchStatusName" class="sfTextBoxSmall" />
                            </td>
                            <td >
                                <asp:Label ID="Label1" runat="server" CssClass="cssClassLabel" 
                                    Text="Active:" meta:resourcekey="Label1Resource1"></asp:Label>
                                <select id="ddlStatusVisibitity" class= "sfListmenu">
                                    <option class="sfLocale" value="">--All--</option>
                                    <option class="sfLocale" value="True">Yes</option>
                                    <option class="sfLocale" value="False">No</option>
                                </select>
                            </td>
                            <td>
                                        <button type="button" onclick="AppointmentStatusMgmt.SearchAppointmentStatus()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxAppointmentStatusMgmtImage" src="" title="loading...." class="sfLocale" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="tblAppointmentStatusDetails" cellspacing="0" cellpadding="0" bcoupon="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>

<div id="divEditAppointmentStatus" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHeading" runat="server" Text="Edit Appointment Status ID:" 
                    meta:resourcekey="lblHeadingResource1" ></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" bcoupon="0" class="cssClassPadding">
                <tr>
                    <td >
                        <asp:Label ID="lblAppointmentStatusName" runat="server" 
                            Text="Appointment Status Name:" CssClass="cssClassLabel" meta:resourcekey="lblAppointmentStatusNameResource1"
                           ></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAppointmentStatusName" name="StatusName" class="sfInputbox required"
                            minlength="2" /><span id="csErrorLabel"></span><span class="cssClassRequired sfLocale">*</span>
                    </td>
                </tr>
                <%--<tr id="isActiveTR">
                    <td>
                        <asp:Label ID="lblCouponStatusIsActive" runat="server" Text="Active:" CssClass="cssClassLabel"
                            meta:resourcekey="lblCouponStatusIsActiveResource1"></asp:Label>
                    </td>
                    <td>
                        <div id="chkIsActiveCouponStatus" class="cssClassCheckBox">
                            <input id="chkIsActiveCoupon" type="checkbox" name="chkIsActive" />
                        </div>
                    </td>
                </tr>--%>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnAppointmentBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
            <%--<p>
                <button type="reset" id="btnAppointmentReset">
                    <span><span>Reset</span></span></button>
            </p>--%>
            <p>
                <button type="button" id="btnSaveAppointmentStatus" class="sfBtn">
                    <span class="sfLocale icon-save">Save Status</span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>