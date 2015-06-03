<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesAll.ascx.cs" Inherits="Modules_AspxCommerce_AspxServiceItems_ServicesAll" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxServiceLocale
        });

        $(this).ServiceViewAll({
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

 
    
</script>
<div id="divServiceAllContainer">
<h2 class="cssClassMiddleHeader"><span class="sfLocale">Services</span> <a href="#" class="cssRssImage" style="display: none">
            <img id="serviceItemRssImage" alt="" src="" title="" />
        </a></h2>
<asp:Literal ID="ltrBindAllServices" runat="server" EnableViewState="False"></asp:Literal>
</div>