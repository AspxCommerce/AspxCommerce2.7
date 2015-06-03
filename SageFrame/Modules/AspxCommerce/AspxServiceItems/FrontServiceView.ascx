<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FrontServiceView.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxServiceItems_FrontServiceView" %>

<div id="divFrontServiceContainer" class="cssClassService">
    <h2 class="cssClassMiddleHeader">
        <span class="sfLocale">Services</span> <a href="#" class="cssRssImage" style="display: none">
            <img id="serviceItemRssImage" alt="" src="" title="" />
        </a>
    </h2>
    <asp:Literal ID="ltrForntServiceView" runat="server" EnableViewState="False"
        meta:resourcekey="ltrForntServiceViewResource1"></asp:Literal>
</div>

<script type="text/javascript">    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxServiceLocale
        });
        $(this).FrontServices({
            IsEnableService: '<%=isEnableService %>',
            ServiceCategoryInARow: '<%=serviceCategoryInARow %>',
            ServiceCategoryCount: '<%=serviceCategoryCount %>',
            IsEnableServiceRss: '<%=isEnableServiceRss %>',
            ServiceRssCount: '<%=serviceRssCount %>',
            ServiceDetailsPage: '<%=serviceDetailsPage %>',
            ServiceModuelPath: '<%=serviceModulePath %>',
            NoImageService: '<%=NoImageService%>',
            ServiceRssPage: '<%=serviceRssPage %>'
        });
    });



    //]]>
</script>
