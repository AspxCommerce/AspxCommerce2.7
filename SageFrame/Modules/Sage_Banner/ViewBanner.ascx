<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewBanner.ascx.cs" Inherits="Modules_Sage_Banner_ViewBanner" %>
<script type="text/javascript">
    //<![CDATA[
    $(function() {
        if ('<%=bannerCount %>' > 0) {
            $(this).BannerView({
                Auto_Slide: '<%=Auto_Slide %>',
                InfiniteLoop: '<%=InfiniteLoop%>',
                NumericPager: '<%=NumericPager%>',
                TransitionMode: '<%=TransitionMode%>',
                speed: parseInt('<%=Speed%>'),
                pause: parseInt('<%=Pause_Time%>'),
                controls: '<%=EnableControl%>'
            });
        }
    });
    //]]>
</script>

<asp:Literal EnableViewState="false" runat="server" ID="sageSlider"></asp:Literal>