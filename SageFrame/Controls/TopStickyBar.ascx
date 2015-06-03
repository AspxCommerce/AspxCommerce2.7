<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopStickyBar.ascx.cs"
    Inherits="Controls_TopStickyBar" %>
<%@ Register Src="LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>
<%@ Register Src="../Modules/AspxCommerce/AspxAdminNotification/AspxAdminNotificationView.ascx" TagName="AspxAdminNotificationView" TagPrefix="uc2" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocalee").SystemLocalize();
        $('#imgAdmin').attr("src", SageFrameAppPath + "/Administrator/Templates/Default/images/admin-icon.png");
        $('#rdbEdit').attr("checked", true);
        $('span.sfPosition').hide();
        $('div.sfModule').append('<div class="sfClearDivTemp" style="clear:both"></div>');
        var isAdmin = '<%=IsAdmin%>';          
        if (isAdmin.toLowerCase()=='true')
        {
            $(".sfquickNotification").show();
            $(".sfDashBoard").show();            
        }
        $('input[name="mode"]').on("click", function () {
            switch ($(this).attr("id")) {
                case "rdbEdit":
                    $('div.sfModuleControl').show();
                    $('span.sfPosition').hide();
                    $('div.sfModule').append('<div class="sfClearDivTemp" style="clear:both"></div>');
                    $('div.sfClearDivTemp').remove();
                    $('div.sfWrapper').removeClass("sfLayoutmode");
                    $('span.sfUsermoduletitle').hide();
                    $('div.sfWrapper div').css("opacity", "1");
                    break;
                case "rdbLayout":
                    $('span.sfPosition').show();
                    $('div.sfModuleControl').hide();
                    $('div.sfModule').append('<div class="sfClearDivTemp" style="clear:both"></div>');
                    var positions = $('div.sfWrapper');
                    $.each(positions, function () {
                        $(this).addClass("sfLayoutmode");
                    });
                    $('div.sfLayoutmode div.sfModule').css("opacity", "0.5");
                    $('span.sfUsermoduletitle').show();
                    $('div.sfLayoutmode').hover(function () {
                        $(this).css("opacity", "1");
                        $(this).find("div.sfModule").css("opacity", "1");
                    }, function () { $(this).find("div.sfModule").css("opacity", "0.5"); });
                    break;
                case "rdbNone":
                    $('div.sfModuleControl').hide();
                    $('span.sfPosition').hide();
                    $('div.sfClearDivTemp').remove();
                    $('div.sfWrapper').removeClass("sfLayoutmode");
                    $('span.sfUsermoduletitle').hide();
                    $('div.sfWrapper div').css("opacity", "1");
                    break;
            }
        });
        //        $(".signin").click(function(e) {
        //            e.preventDefault();
        //            $("div#signin_menu").toggle();
        //            $(".signin").toggleClass("menu-open");
        //        });
        //        $("div#signin_menu").mouseup(function() {
        //            return false
        //        });
        //        $(document).mouseup(function(e) {
        //            if ($(e.target).parent("a.signin").length == 0) {
        //                $(".signin").removeClass("menu-open");
        //                $("div#signin_menu").hide();
        //            }
        //        });

        $('.sfCpanel').on('click', function () {
            var divHeight = 0;
            if ($('#divCpanel').hasClass('On')) {
                // divHeight = parseInt($('.sfMiddle').height()) - parseInt($('#templateChangeWrapper').height());
                divHeight = -254;
                $('#divCpanel').removeClass('On').addClass('Off');
            }
            else {
                divHeight = 0;
                $('#divCpanel').removeClass('Off').addClass('On');
            }
            $('#divCpanel').animate({
                top: divHeight
            });
        });

        $('.myProfile').on('click', function () {
            if ($('.myProfileDrop').hasClass('Off')) {
                $('.myProfileDrop').removeClass('Off');
                $('.myProfileDrop').show();
            }
            else {
                $('.myProfileDrop').addClass('Off');
                $('.myProfileDrop').hide();
            }
        });
    });
    //]]>	
</script>
<div class="sfTopbar clearfix">
    <ul class="left">
        <li>
            <div class="sfLogo">
                <asp:HyperLink ID="hypLogo" runat="server">Dashboard</asp:HyperLink>
                <asp:Label runat="server" ID="lblVersion"></asp:Label>
            </div>
        </li>
    </ul>
    <ul class="right">
    </ul>
    <div id="cpanel" runat="server">
        <div id="divCpanel" style="top: -254px;">
            <div id="signin_menu" class="clearfix">
                <ul>
                    <li>
                        <h6 class="sfLocalee">Customize:</h6>
                        <p>
                            <label class="sfLocalee">
                                Themes:</label>
                            <asp:DropDownList ID="ddlThemes" runat="server" meta:resourcekey="ddlThemesResource1">
                            </asp:DropDownList>
                        </p>
                        <p>
                            <label class="sfLocalee">
                                Screen:</label>
                            <asp:DropDownList ID="ddlScreen" runat="server" meta:resourcekey="ddlScreenResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">fluid</asp:ListItem>
                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">wide</asp:ListItem>
                                <asp:ListItem Value="2" meta:resourcekey="ListItemResource3">narrow</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <label class="sfLocalee">
                                Layouts:</label>
                            <asp:DropDownList ID="ddlLayout" runat="server" meta:resourcekey="ddlLayoutResource1">
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:Button ID="btnApply" OnClick="btnApply_Click" runat="server" Text="Apply" CssClass="sfBtn"
                                meta:resourcekey="btnApplyResource1" />
                        </p>
                        <div class="sfMode">
                            <h6 class="sfLocalee">Mode:</h6>
                            <label class="sfLocale">
                                <input id="rdbEdit" name="mode" type="radio" />
                                Edit</label>
                            <label class="sfLocale">
                                <input id="rdbLayout" name="mode" type="radio" />
                                Layout</label>
                            <label class="sfLocale">
                                <input id="rdbNone" name="mode" type="radio" />
                                None</label>
                        </div>
                    </li>
                </ul>
            </div>
            <span class="sfCpanel icon-themesetting signin"></span>
        </div>
    </div>
    <ul class="right">
    <li class="sfDashBoard" style="display:none;">
            <asp:HyperLink ID="hlnkDashboard" CssClass="icon-dashboard" runat="server" meta:resourcekey="hlnkDashboardResource1">Dashboard</asp:HyperLink>
        </li>
    	<li class="sfquickNotification" style="display:none">
        <uc2:AspxAdminNotificationView ID="AspxAdminNotificationView1" runat="server" Visible="false" />
     </li>  
        <li class="loggedin"><span class="icon-user">
            <asp:Literal ID="litUserName" runat="server" Text="Logged As" meta:resourcekey="litUserNameResource1">
            </asp:Literal></span> &nbsp;<strong><%= userName%></strong> </li>
        <li class="logout"><span class='myProfile  icon-arrow-s'></span>
            <div class="myProfileDrop Off" style="display: none;">
                <ul>
                    <li>
                        <%= userName%>
                    </li>
                    <li class="myaccount">
                        <asp:HyperLink runat="server" ID="lnkAccount" Text="My Account" meta:resourcekey="lnkAccountResource1"></asp:HyperLink>
                    </li>
                    <li>
                        <uc1:LoginStatus ID="LoginStatus2" runat="server" />
                    </li>
                </ul>
            </div>
        </li>
    </ul>
</div>
