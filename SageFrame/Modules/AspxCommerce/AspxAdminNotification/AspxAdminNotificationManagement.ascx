<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxAdminNotificationManagement.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminNotification_AspxAdminNotificationManagement" %>

<script type="text/javascript">
    // <![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxAdminNotificationLanguage
        });
        $(this).AdminNotificationManagementDetails({
            aspxAdminNotificationModulePath: '<%=AspxAdminNotificationModulePath %>'
        });
    });
    // ]]>
</script>


<div id="divAsxpABTesting">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <label id="lblHeading" class="sfLocale">Admin Notification</label>
            </h1>
        </div>
        <div class="sfFormwrapper">
                <table id="tblNotificationSettings" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="sfTableRowBrd-btm">
                        <td colspan="1">
                            <label id="lblNotificationAll" class="sfFormlabel sfLocale">
                                All Notifications</label>
                        </td>
                        <td colspan="4">
                            <select id="ddlNotificationAll" name="NotificationAll" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label id="lblNotificationUserRegistration" class="sfFormlabel sfLocale">
                                User Registration</label>
                        </td>
                        <td>
                            <select id="ddlNotificationUserRegistration" name="NotificationUserRegistration" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                        <td>
                            <label id="lblNotificationCountForUser" class="sfFormlabel sfLocale">
                                Number of notifications to show</label>
                        </td>
                        <td colspan="2">
                            <input type="text" id="txtNotificationCountForUser" name="NotificationCountForUser" class="sfInputbox sfShortInputbox required number" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label id="lblNotificationSubscription" class="sfFormlabel sfLocale">
                                Subscription</label>
                        </td>
                        <td>
                            <select id="ddlNotificationSubscription" name="NNotificationSubscription" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                        <td>
                            <label id="lblNotificationCountForSubscription" class="sfFormlabel sfLocale">
                                Number of notifications</label>
                        </td>
                        <td colspan="2">
                            <input type="text" id="txtNotificationCountForSubscription" name="NotificationCountForSubscription" class="sfInputbox sfShortInputbox required number" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label id="lblNotificationOutOfStock" class="sfFormlabel sfLocale">
                                Out of stock</label>
                        </td>
                        <td>
                            <select id="ddlNotificationOutOfStock" name="NotificationOutOfStock" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                        <td>
                            <label id="lblNotificationCountForOutOfStock" class="sfFormlabel sfLocale">
                                Number of notifications</label>
                        </td>
                        <td colspan="2">
                            <input type="text" id="txtNotificationCountForOutOfStock" name="NotificationCountForOutOfStock" class="sfInputbox sfShortInputbox required number" />
                        </td>
 

                    </tr>
                    <tr>
                        <td>
                            <label id="lblNotificationLowStock" class="sfFormlabel sfLocale">
                                Low stock</label>
                        </td>
                        <td>
                            <select id="ddlNotificationLowStock" name="NotificationLowStock" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                        <td>
                            <label id="lblNotificationCountForLowStock" class="sfFormlabel sfLocale">
                                Number of notifications</label>
                        </td>
                        <td>
                            <input type="text" id="txtNotificationCountForLowStock" name="NotificationCountForLowStock" class="sfInputbox sfShortInputbox required number" />
                        </td>
                      
                    </tr>

                    <tr>
                        <td>
                            <label id="lblNotificationOrders" class="sfFormlabel sfLocale">
                                Orders</label>
                        </td>
                        <td>
                            <select id="ddlNNotificationOrders" name="NotificationOrders" class="sfListmenu">
                                <option value="true" class="sfLocale">ON</option>
                                <option value="false" class="sfLocale">OFF</option>
                            </select>
                        </td>
                        <td>
                            <label id="lblNotificationCountForOrders" class="sfFormlabel sfLocale">
                                Number of notifications</label>
                        </td>
                        <td colspan="2">
                            <input type="text" id="txtNotificationCountForOrders" name="NotificationCountForOrders" class="sfInputbox sfShortInputbox required number" />
                        </td>

                    </tr>

                </table>
            
            
        </div>
        <div class="sfButtonwrapper sftype1">
                <button id="btnSaveNotificationSettings" class="sfSave sfLocale sfBtn icon-save">Save</button>
                <button id="btnRefrershNotificationSettings" class="sfRefresh sfLocale sfBtn icon-refresh">Refresh</button>
            </div>
    </div>
</div>

