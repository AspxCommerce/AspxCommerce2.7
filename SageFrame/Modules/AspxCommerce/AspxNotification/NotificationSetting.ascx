<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotificationSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxNotification_NotificationSetting" %>
<script type="text/javascript">
    var ModulePath = "<%=ModulePath %>";
</script>
<div class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblSeasonalSetting" Text="Admin Email Notification Setting" 
                runat="server" meta:resourcekey="lblSeasonalSettingResource1"></asp:Label>
        </h2>
    </div>
    <div class="cssClassFormWrapper">
        <table border="0" width="100%" id="tblNotificationSetting" class="cssClassPadding tdpadding">
            <tr>
                <td>
                    <asp:Label ID="lblEnableNewOrder" runat="server" Text="Enable New Order Notification"
                        CssClass="cssClassLabel" meta:resourcekey="lblEnableNewOrderResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableNewOrder" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNewOrderNotReceiver" runat="server" Text="New Order Notification Receipent Email"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblNewOrderNotReceiverResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtNewOrderNoticeReceiver" type="text" name="NewOrderNoticeReceiver" class="cssClassNormalTextBox required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableOrderStatChange" runat="server" Text="Enable Order Status Change Notification"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblEnableOrderStatChangeResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableOrderStatChange" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrderStatChangeNotifyEmail" runat="server" Text="Order Status Change Notification Receipent Email"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblOrderStatChangeNotifyEmailResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtOrderStatChangeNotifyEmail" type="text" name="OrderStatChangeNotifyEmail" class="cssClassNormalTextBox required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableCustomerRegNotify" runat="server" Text="Enable Customer Registration Notification"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblEnableCustomerRegNotifyResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableCustomerRegNotify" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomerRegEmail" runat="server" Text="New Customer Registration Notification Receipent Email"
                        CssClass="cssClassLabel" meta:resourcekey="lblCustomerRegEmailResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtCustomerRegEmail" type="text" name="CustomerRegEmail" class="cssClassNormalTextBox required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableReviewsNotify" runat="server" Text="Enable New Reviews Notification"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblEnableReviewsNotifyResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableReviewsNotify" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReviewNotifyEmail" runat="server" Text="New Reviews Notification Receipent Email"
                        CssClass="cssClassLabel" meta:resourcekey="lblReviewNotifyEmailResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtReviewNotifyEmail" type="text" name="ReviewNotifyEmail" class="cssClassNormalTextBox required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableNewTagsNotify" runat="server" Text="Enable New Tags Notification"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblEnableNewTagsNotifyResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableNewTagsNotify" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNewTagsNotifyEmail" runat="server" Text="New Tags Notification Receipent Email"
                        CssClass="cssClassLabel" meta:resourcekey="lblNewTagsNotifyEmailResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtNewTagsNotifyEmail" type="text" name="NewTagsNotifyEmail" class="cssClassNormalTextBox required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableCouponNotify" runat="server" Text="Enable Each Time Coupon Used Notification"
                        CssClass="cssClassLabel" meta:resourcekey="lblEnableCouponNotifyResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="chkEnableCouponNotify" type="checkbox" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCouponUseNotifyEmail" runat="server" Text="Coupon Used Notification Receipent Email"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblCouponUseNotifyEmailResource1"></asp:Label>
                </td>
                <td>
                    <input id="txtCouponUseNotifyEmail" type="text" name="CouponUseNotifyEmail" class="cssClassNormalTextBox required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEnableSuscribeNotify" runat="server" Text="Enable Customer Subscribes To a Newsletter or Unsubscribes From a Newsletter Notification"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblEnableSuscribeNotifyResource1"></asp:Label>
                </td>
                <td>
                    <input id="chkEnableSuscribeNotify" type="checkbox" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustSuscribeNotifyEmail" runat="server" Text="Customer Subscribes To a Newsletter or Unsubscribes From a Newsletter Notification Receipent Email"
                        CssClass="cssClassLabel" 
                        meta:resourcekey="lblCustSuscribeNotifyEmailResource1"></asp:Label>
                </td>
                <td class="cssClassTableRightCol">
                    <input id="txtCustSuscribeNotifyEmail" type="text" name="CustSuscribeNotifyEmail" class="cssClassNormalTextBox required" />
                </td>
            </tr>
        </table>
    </div>
  <div class="cssClassButtonWrapper">
            <p>
                <button type="submit" id="btnSaveNotificationSetting" class="cssClassButtonSubmit sfBtn">
                    <span class="sflocale icon-save">Save Settings</span></button>
            </p>
        </div>
</div>
