<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageHelp.ascx.cs" Inherits="Modules_Admin_PageHelp_PageHelp" %>
<span class="pageHelp off">Page Help</span>
<div class="sfPageHelpHolder clearfix" style="display: none;">
    <h4>
        <%=pageName%>
    </h4>
    <div id="sfPageHelpDetails">
        <asp:Literal runat="server" ID="ltrPageHelp"></asp:Literal>
    </div>
    <div class="sfPageHelpMoreInfo">
    	<h6>For more information</h6>
        <asp:HyperLink Target="_blank" runat="server" ID="lnkpage" Text="Documentation on "></asp:HyperLink>
    </div>
</div>

<script type="text/javascript">
    $(function() {
        $("#sfPageHelpDetails").tabs();
        $('.pageHelp').on('click', function() {
            var $this = $(this);
            if ($this.hasClass('off')) {
                $this.removeClass('off');
                $('.sfPageHelpHolder').slideDown();
            }
            else {
                $this.addClass('off');
                $('.sfPageHelpHolder').slideUp();
            }
        });
    });
</script>