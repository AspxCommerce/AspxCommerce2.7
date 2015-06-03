<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageMenu.ascx.cs" Inherits="Modules_SageMenu_SageMenu" %>
<asp:Literal ID="ltrNav" runat="server" EnableViewState="false"></asp:Literal>
<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(this).SageMenuBuilder({
            ContainerClientID: '<%=ContainerClientID %>',
            MenuType: '<%=menuType%>'
        });
    });
    //]]>	
</script>

