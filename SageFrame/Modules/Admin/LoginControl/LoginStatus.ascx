<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginStatus.ascx.cs" Inherits="Modules_Admin_LoginControl_LoginStatus"
    EnableViewState="false" %>
<%@ Register Src="~/Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>
<script type="text/javascript">
    $(function () {
        $(".sfLocalee").SystemLocalize();
    });
</script>
<div class="sfLogininfo">
    <ul>
        <div id="LoginView1" runat="server" enableviewstate="False">
            <div id="divAnonymousTemplate" runat="server">
                <li class="sfLogin">
                    <uc1:LoginStatus ID="LoginStatus1" runat="server" EnableViewState="False" />
                </li>
                
                <li class="sfLoginCredInfo">


                    <a href="#">Backend Login info</a>
                    <div class="sfLoginCredInfoBox">
                        <div class="triangle"></div>
                        <h6>Login credentials for backend</h6>
                        <div class="sfSuperUserInfo">
                            <p><span>Username: </span>superuser</p>
                            <p><span>Password: </span>superuser</p>
                        </div>
                        <div class="sfAdminInfo">
                            <p><span>Username: </span>admin</p>
                            <p><span>Password: </span>admin</p>
                        </div>
                    </div>

                </li>
                <li class="sfRegister">
                    <%=RegisterURL%>
                </li>
            </div>
            <div id="divLoggedInTemplate" runat="server">
                <li class="sfWelcomeMsg">
                    <asp:Label ID="lblWelcomeMsg" Text="Welcome" runat="server" meta:resourcekey="lblWelcomeMsgResource1"></asp:Label>
                </li>
                <%-- <li class="sfWelcomeMsg">
                    <asp:Label ID="lblWelcomeMsgSignIN" runat="server" meta:resourcekey="lblWelcomeMsgSignINResource1"></asp:Label>
                </li>--%>
                <%--<li>
                    <asp:Label ID="lblProfileURL" runat="server" meta:resourcekey="lblProfileURLResource1"></asp:Label>
                </li>
                <li class="sfLoggedOut">
                    <uc1:LoginStatus ID="LoginStatus2" runat="server" EnableViewState="False" />
                </li>--%>
            </div>
        </div>
    </ul>
</div>
