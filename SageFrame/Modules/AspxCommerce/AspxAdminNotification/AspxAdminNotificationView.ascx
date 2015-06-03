<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxAdminNotificationView.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminNotification_AspxAdminNotificationView" %>

<script type="text/javascript">
    // <![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxAdminNotificationLanguage
        });
        $(this).AdminNotificationViewDetails({
            aspxAdminNotificationModulePath: '<%=AspxAdminNotificationModulePath %>'
        });
    });
    // ]]>
</script>
<div class="loader">
</div>
<div runat="server" id="divNotification" class="sfHtmlview notificationsSticker">
            <ul>
                <li class="sfqckUserInfo">
                    <span id="spanUsersInfo" class="notired" style="display:none">0</span>
                    <a id="linkUsersInfo" class="showfrindreq mesgnotfctn topopup icon-users" title="Click to View User Info">&nbsp;</a></li>
                <li class="sfqckItemsInfo">
                    <span id="spanItemsInfo" class="notired" style="display:none">0</span>
                    <a id="linkItemsInfo" class="showmesgreq  topopup icon-cart" title="Click to View Items Info">&nbsp;</a></li>
                <li class="sfqckOrdersInfo">
                    <span id="spanOrdersInfo" class="notired" style="display:none">0</span>
                    <a id="linkOrdersInfo" class="showmesgreq topopup icon-orders" title="Click to View Orders Info">&nbsp;</a></li>
                     
            </ul>
  </div>
