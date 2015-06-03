<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LangSwitch.ascx.cs" Inherits="Modules_Language_LangSwitch" %>
<script type="text/javascript">
    //<![CDATA[  
    $(function () {
        $(this).langswitcher({
            PortalID: '<%=PortalID%>',
            UserModuleID: '<%=UserModuleID%>',
            CultureCode:'<%=CultureCode%>',
            LangSwitchContainerID: '#' + '<%=ContainerClientID%>',
            SwitchType: '<%=switchType %>',
            DropDownType: '<%=dropDownType %>'
        });
    });
    //]]>	   
</script>
<div class="sfLanguageContainer">
	<asp:Label ID="lblLang" runat="server" Text="Your Language:" 
    CssClass="sfFormlabel" meta:resourcekey="lblLangResource1"></asp:Label>
	<asp:Literal ID="ltrNav" runat="server" EnableViewState="false"></asp:Literal>
</div>