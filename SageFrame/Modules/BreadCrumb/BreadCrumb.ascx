<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BreadCrumb.ascx.cs" Inherits="Modules_BreadCrumb_BreadCrumb" %>

<script type="text/javascript">
    //<![CDATA[    
    var DefaultPortalHomePage = '<%=DefaultPortalHomePage %>';
    var Extension = '<%=Extension %>';
    $(function() {
        $(this).BreadCrumbBuilder({
            baseURL: BreadCrumPagePath + 'Modules/BreadCrumb/BreadCrumbWebService.asmx/',
            PagePath: BreadCrumPageLink,
            PortalID: '<%=PortalID%>',
            PageName: '<%=PageName%>',
            Container: "div.sfBreadcrumb",
            MenuId: '<%=MenuID%>',
            CultureCode: '<%=CultureCode %>'
        });
    });
    //]]>	
</script>
<div class="sfBreadcrumb">
    
</div>
