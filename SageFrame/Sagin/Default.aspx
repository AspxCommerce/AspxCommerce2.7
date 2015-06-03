<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SageFrame.Sagin_Default" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TopStickyBar.ascx" TagName="TopStickyBar" TagPrefix="ucstickybar" %>
<%@ Register Src="~/Controls/DashboardQuickLinks.ascx" TagName="DashboardQuickLinks"
    TagPrefix="ucquicklink" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagName="Sidebar" TagPrefix="ucsidebar" %>
<%@ Register Src="~/Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ctl_CPanleFooter.ascx" TagName="ctl_CPanleFooter" TagPrefix="uc3" %>
<%@ Register Src="../Controls/ctl_AdminBreadCrum.ascx" TagName="AdminBreadCrumb"
    TagPrefix="uc4" %>
<%@ Register Src="../Controls/PageHelp.ascx" TagName="AdminPageHelp" TagPrefix="uc5" %>
<%@ Register Src="~/Modules/SiteAnalytics/TopFiveCountry.ascx" TagName="SiteAnalytics"
    TagPrefix="TopFive" %>
<%@ Register Src="../Modules/AspxCommerce/AspxAdminNotification/AspxAdminNotificationView.ascx" TagName="AspxAdminNotificationView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head">
    <link type="icon shortcut" media="icon" href="favicon.ico" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" />
    <meta id="MetaDescription" runat="Server" name="DESCRIPTION" />
    <meta id="MetaKeywords" runat="Server" name="KEYWORDS" />
    <meta id="MetaCopyright" runat="Server" name="COPYRIGHT" />
    <meta id="MetaGenerator" runat="Server" name="GENERATOR" />
    <meta id="MetaAuthor" runat="Server" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
   <meta http-equiv="x-ua-compatible" content="IE=edge" >
    <!-- Mimic Internet Explorer 7 -->
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />
    <!--[if IE 8]><script type="text/javascript" src="../js/SageFrameCorejs/excanvas.js"></script><![endif]-->
    <!--[if IE]><link rel="stylesheet" href="../css/IE.css" type="text/css" media="screen" /><![endif]-->
    <!--[if IE 7]><script type="text/javascript" src="../js/SageFrameCorejs/IE8.js"></script><![endif]-->
    <!--[if !IE 7]>
	<style type="text/css">
		#wrap {display:table;height:100%}
	</style>
	<![endif]-->
    <!--[if IE 8]><link rel="stylesheet" type="text/css" href="../css/ie8.css" media="screen"><![endif]-->
    <!--[if IE 9]><link rel="stylesheet" type="text/css" href="../css/ie9.css" media="screen"><![endif]-->
    <script>
        /*@cc_on
        @if (@_jscript_version == 10)
              document.write(' <link type= "text/css" rel="stylesheet" href="../css/ie10.css" />');
          @end
        @*/
    </script>
    <asp:PlaceHolder ID="pchHolder" runat="server"></asp:PlaceHolder>
    <title>SageFrame Website</title>
    <asp:Literal runat="server" ID="ltrJQueryLibrary"></asp:Literal>
    <asp:Literal ID="SageFrameModuleCSSlinks" runat="server"></asp:Literal>
</head>
<body onload="__loadScript();">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server" LoadScriptsBeforeUI="false"
            ScriptMode="Release">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div class="sfLoadingbg">
                    &nbsp;
                </div>
                <div class="sfLoadingdiv">
                    <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..." />
                    <br />
                    <asp:Label ID="lblPrgress" runat="server" Text="Please wait..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <noscript>
            <asp:Label ID="lblnoScript" runat="server" Text="This page requires java-script to be enabled. Please adjust your browser-settings."></asp:Label>
        </noscript>
        <div id="sfOuterwrapper">
            <div class="sfSagewrapper">
                <div class="sfTopbar clearfix" id="divAdminControlPanel" runat="server" style="display: block;">
                    <ul class="left">
                        <li>
                            <div class="sfLogo">
                                <a href="../Admin/Admin<%=Extension %>">
                                    <img src="<%=appPath%>/Administrator/Templates/Default/images/sagecomers-logoicon.png" alt="AspxCommerce" /></a>
                                <asp:Label runat="server" ID="lblVersion"></asp:Label>
                            </div>
                        </li>
                        <li class="sfUpgrade">
                            <asp:HyperLink ID="hypUpgrade" runat="server" Text="Upgrade"></asp:HyperLink></li>
                    </ul>
                    <ul class="right">
                        <li class="home">
                            <asp:HyperLink ID="hypHome" runat="server" CssClass="icon-home"></asp:HyperLink>
                        </li>
                        <li class="preview">
                            <asp:HyperLink ID="hypPreview" runat="server" Text="Preview" Target="_blank" CssClass="icon-preview"></asp:HyperLink>
                        </li>
                        <li class="sfquickNotification">
                            <uc2:AspxAdminNotificationView ID="AspxAdminNotificationView1" runat="server" />
                        </li>
                        <li class="cssClassLanguageSettingWrapperAdmin">
                            <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                        </li>
                        <li class="loggedin"><span class="icon-user">
                            <%--  <asp:Image runat="server" ID="adminImage" CssClass="icon-user" />--%>
                            <asp:Literal ID="litUserName" runat="server" Text="Logged As"></asp:Literal>
                            &nbsp; </span><strong>
                                <%= userName%></strong></li>
                        <li class="logout"><span class='myProfile  icon-arrow-s'></span>
                            <div class="myProfileDrop Off" style="display: none;">
                                <ul>
                                    <li>
                                        <%= userName%>
                                    </li>
                                    <li>
                                        <asp:HyperLink runat="server" ID="lnkAccount" Text="Logged As">                                
                                <strong>Profile</strong>
                                        </asp:HyperLink></li>
                                    <li>
                                        <uc1:LoginStatus ID="LoginStatus1" runat="server" />
                                    </li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                    <div class="clear">
                    </div>
                    <div id="templateChangeWrapper" class="Off" style="top: -189px;">
                        <div class="templateChange">
                            <h6>Theme</h6>
                            <asp:RadioButtonList CssClass="sfTableThemeColor" runat="server" ID="rdTemplate"
                                AutoPostBack="true" OnSelectedIndexChanged="rdTemplate_SelectedIndexChanged">
                                <asp:ListItem Text="Default UI" Value="green"></asp:ListItem>
                                <asp:ListItem Text="Gray UI" Value="gray"></asp:ListItem>
                                <asp:ListItem Text="Dark UI" Value="dark"></asp:ListItem>
                            </asp:RadioButtonList>
                            <h6>SideBar Position</h6>
                            <table class="sfTableSidebarPosition">
                                <tr>
                                    <td>
                                        <asp:RadioButton GroupName="position" runat="server" ID="rdLeft" Text="Left" AutoPostBack="true"
                                            OnCheckedChanged="rdLeft_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:RadioButton GroupName="position" runat="server" ID="rdRight" AutoPostBack="true"
                                            Text="Right" OnCheckedChanged="rdRight_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <span class="sfMiddle middleTheme icon-themesetting"></span>
                    </div>
                </div>
                <!--End of CPanel Head-->
                <!--Navigation Wrapper-->
                <div class="sfNavigation clearfix" id="divNavigation" runat="server" style="display: none">
                    <asp:PlaceHolder ID="navigation" runat="server"></asp:PlaceHolder>
                </div>
                <!--Body Content-->
                <div class="sfContentwrapper clearfix">
                    <div id="divCenterContent">
                        <div runat="server" id="divSideBar">
                            <ucsidebar:Sidebar ID="Sidebar1" runat="server" />
                            <div class="sfFooterwrapper clearfix" id="divFooterWrapper" runat="server">
                                <uc3:ctl_CPanleFooter ID="ctl_CPanleFooter1" runat="server" />
                            </div>
                        </div>
                        <div class="sfMaincontent">
                            <div class="sfBreadcrumb pageHelpWrap clearfix">
                                <uc4:AdminBreadCrumb ID="adminbreadcrumb" runat="server" />
                                <uc5:AdminPageHelp ID="adminHelp" runat="server" />
                            </div>
                            <asp:PlaceHolder ID="message" runat="server"></asp:PlaceHolder>
                            <asp:PlaceHolder ID="dashboardinfo" runat="server"></asp:PlaceHolder>
                            <div class="sfInnerwrapper">
                                <div id="sfToppane" class='sfCol_70'>
                                    <asp:PlaceHolder ID='toppane' runat='server'></asp:PlaceHolder>
                                </div>
                                <div id="sfTopfivecountry" class='sfCol_30'>
                                    <asp:PlaceHolder ID='topfivecountry' runat='server'></asp:PlaceHolder>
                                    <%--<TopFive:SiteAnalytics runat="server" ID="topFiveAnalytics" />--%>
                                </div>
                            </div>
                            <div class="sfInnerwrapper clearfix">
                                <div class="sfCol_60" id="divLeft" runat="server" style="display: none">
                                    <asp:PlaceHolder ID="LeftA" runat="server"></asp:PlaceHolder>
                                </div>
                                <div class="sfCol_40" id="divRight" runat="server" style="display: none">
                                    <asp:PlaceHolder ID="middlemaincurrent" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="sfCpanel sfInnerwrapper" runat="server" id="divBottompanel" style="display: none">
                                <asp:PlaceHolder ID="cpanel" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Footer Wrapper-->
        </div>
        <div id="dialog" title="Confirmation Required">
            <label id="sf_lblConfirmation">
            </label>
        </div>
        <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>
    </form>
    <script type="text/javascript">
        $(function () {
            $('.middleTheme').on('click', function () {
                var divHeight = 0;
                if ($('#templateChangeWrapper').hasClass('On')) {
                    // divHeight = parseInt($('.sfMiddle').height()) - parseInt($('#templateChangeWrapper').height());
                    divHeight = -189;
                    $('#templateChangeWrapper').removeClass('On').addClass('Off');
                }
                else {
                    divHeight = 0;
                    $('#templateChangeWrapper').removeClass('Off').addClass('On');
                }
                $('#templateChangeWrapper').animate({
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

            $("#languageSelectAdmin li").click(function () {
                var code = $(this).attr('value');
                var mydata = JSON2.stringify({ CultureCode: code });
                $.ajax({
                    type: "POST",
                    url: SageFrameAppPath + "/Modules/LanguageSwitcher/js/WebMethods.aspx/" + 'SetCultureInfo',
                    data: mydata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        location.reload(true);
                    },
                    error: function () {

                    }
                });

            });


        });
    </script>
</body>
</html>
