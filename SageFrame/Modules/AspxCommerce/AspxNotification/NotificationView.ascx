<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotificationView.ascx.cs" Inherits="Modules_AspxCommerce_AspxNotification_NotificationView" %>
<script type="text/javascript">
     var ModulePath = "<%=ModulePath %>";
</script>

<div id="notifs_icon_wrapper">
  <div id="orders_notif" class="notifs">
        <span id="orders_notif_number_wrapper" class="number_wrapper" style="display: none;">
            <span id="orders_notif_value">0</span> </span>
        <div id="orders_notif_wrapper" class="notifs_wrapper">
            <h3 class="sflocale">
                Last orders</h3>
            <p class="no_notifs sflocale">
                No new orders has been placed on your shop</p>
            <ul id="list_orders_notif">
            </ul>
            <p>
                <a href="#" class="sflocale">Show
                    all orders</a>
            </p>
        </div>
    </div>
    <div id="customers_notif" class="notifs notifs_alternate">
        <span id="customers_notif_number_wrapper" class="number_wrapper" style="display: none;">
            <span id="customers_notif_value" notifyType="">0</span> </span>
        <div id="customers_notif_wrapper" class="notifs_wrapper">
            <h3>
                <span class="sfLocale">Last customers</span></h3>
            <p class="no_notifs sfLocale">
                No new customers registered on your shop</p>
            <ul id="list_customers_notif">
            </ul>
            <p>
                <a id="lnkShowAllCustomers" class="sfLocale">
                    Show all customers</a>
            </p>
        </div>
    </div>
   <%-- <div id="customer_messages_notif" class="notifs">
        <span id="customer_messages_notif_number_wrapper" class="number_wrapper" style="display: none;">
            <span id="customer_messages_notif_value">0</span> </span>
        <div id="customer_messages_notif_wrapper" class="notifs_wrapper">
            <h3>
                Last messages</h3>
            <p class="no_notifs">
                No new messages posted on your shop</p>
            <ul id="list_customer_messages_notif">
            </ul>
            <p>
                <a href="index.php?tab=AdminCustomerThreads&token=e840120aeef4e424817a21be594e4715">
                    Show all messages</a>
            </p>
        </div>
    </div>--%>
</div>
