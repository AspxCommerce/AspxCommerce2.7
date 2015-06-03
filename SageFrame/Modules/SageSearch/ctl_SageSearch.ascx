<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SageSearch.ascx.cs"
    Inherits="Modules_SageSearch_ctl_SageSearch" %>

<script type="text/javascript">
//<![CDATA[
    $(function () {
        $('#fade').on("click", function () {
            $('#sfSearch').slideUp(function () {
                $('#fade').fadeOut().remove();
            });
        });
        $('#spnSearch').on("click", function () {
            $('#sfSearch').slideDown();
            $('body').append("<div id='fade'></div>");
        });
    });
    //]]>
</script>

<asp:Panel ID="pnlSearch" runat="server" CssClass="sfSearchwrapper">
</asp:Panel>
