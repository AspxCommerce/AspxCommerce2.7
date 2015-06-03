<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleLoader.ascx.cs" Inherits="Modules_ModuleLoader_ModuleLoader" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(this).ModuleLoader({
            UserModuleID: '<%=userModuleID%>'
        });
    });
    //]]>	
</script>


<div>Module Loader</div>
<div id="divUControl"></div>
<asp:PlaceHolder ID="pchUserControl" runat="server"></asp:PlaceHolder>
<asp:ScriptManager ID="addScriptManager" runat="server"></asp:ScriptManager>