<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_LoginStatus.ascx.cs"
    Inherits="SageFrame.Controls.ctl_LoginStatus" %>
<%@ Register Src="LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>
<ul>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <li>Have an account?</li>
            <li>
                <uc1:LoginStatus ID="LoginStatus1" runat="server" />
            </li>
            <li>New User?</li>
            <li><a href='<%= RegisterURL %>'>
                <%= SageFrame.Web.SystemSetting.Register %></a></li>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <li>
                <%=userName%>
            </li>
            <%--<li>
                <uc1:LoginStatus ID="LoginStatus2" runat="server" />
            </li>--%>
        </LoggedInTemplate>
    </asp:LoginView>
</ul>
