<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageModuleStatistics.ascx.cs"
    Inherits="Modules_DashBoard_PageModuleStatistics" %>

<script type="text/javascript">
//<![CDATA[ 
$(function(){
 $('#tabPortalStat').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
});
$(function() {
    var PageModuleStatistics = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            method: "",
            url: "",
            categoryList: "",
            ajaxCallMode: 0,
            arr: [],
            arrModules: [],
            baseURL: '<%=appPath%>' + '/Modules/Dashboard/Services/DashboardWebService.asmx/',
            PortalID: SageFramePortalID,
            UserName: '<%=UserName%>',
            Path: '<%=appPath%>',
            PortalName: '<%=PortalName%>',
            TemplateName: '<%=TemplateName%>'
        },
        init: function() {
            PageModuleStatistics.GetGeneralSnapShot();
            $('#liPages').click(function() {
                PageModuleStatistics.GetPageName();
                $("#tblPages").quickPager({ pagerClass: "sfPagination" });                
            });
            $('#liModules').click(function() {
                PageModuleStatistics.GetModules();
                $("#tblModules").quickPager({ pagerClass: "sfPagination" });
            });
            $('#liUser').click(function() {
                PageModuleStatistics.GetUsers();
                $("#tblUser").quickPager({ pagerClass: "sfPagination" });
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: PageModuleStatistics.config.type,
                contentType: PageModuleStatistics.config.contentType,
                cache: PageModuleStatistics.config.cache,
                async: PageModuleStatistics.config.async,
                url: PageModuleStatistics.config.url,
                data: PageModuleStatistics.config.data,
                dataType: PageModuleStatistics.config.dataType,
                success: PageModuleStatistics.ajaxSuccess,
                error: PageModuleStatistics.ajaxFailure
            });
        },
        GetPageName: function() {
            this.config.method = "GetPageName";
            this.config.url = PageModuleStatistics.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ PortalID: SageFramePortalID, IsAdmin: false });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindPageName: function(data) {
            var html = '';
            var PageList = data.d;
            $.each(PageList, function(index, item) {
                var style = index % 2 == 0 ? "sfEven" : "sfOdd";
                html += '<tr class= ' + style + '><td>' + item.PageName + '</td></tr>';
            });
            $('#tblPages').html(html);

        },
        GetModules: function() {
            this.config.method = "GetModules";
            this.config.url = PageModuleStatistics.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ PortalID: PageModuleStatistics.config.PortalID });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        GetGeneralSnapShot: function() {
            this.config.method = "GeneralSnapShot";
            this.config.url = PageModuleStatistics.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ PortalID: PageModuleStatistics.config.PortalID, IsAdmin: false });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        BindGeneralSnapShot: function(data) {
            var lblUserCount = '<%=lblUserCount.ClientID%>';
            var lblPageCount = '<%=lblPageCount.ClientID%>';
            var lblTotalUserCount = '<%=lblTotalUserCount.ClientID%>';
            var lblPortalName = '<%=lblPortalName.ClientID%>';
            var lblUserName = '<%=lblUserName.ClientID%>';
            var lblTemplate = '<%=lblTemplate.ClientID%>';            
            var Snapshot = data.d;
            $('#' + lblUserName).text(PageModuleStatistics.config.UserName);
            $('#' + lblUserCount).text('AnonymousUser-' + Snapshot.AnonymousUser + ' & ' + 'LoginUser-' + Snapshot.LoginUser);
            $('#' + lblPageCount).text(Snapshot.PageCount);
            $('#' + lblTotalUserCount).text(Snapshot.UserCount);
            $('#' + lblPortalName).text(PageModuleStatistics.config.PortalName);
            $('#' + lblTemplate).text(PageModuleStatistics.config.TemplateName);
        },
        BindModules: function(data) {
            var html = '';
            var ModuleList = data.d;
            $.each(ModuleList, function(index, item) {
                var style = index % 2 == 0 ? "sfEven" : "sfOdd";
                html += '<tr class= ' + style + '><td>' + item.FriendlyName + '</td></tr>';
            });
            $('#tblModules').html(html);
        },
        GetUsers: function() {
            this.config.method = "GetUsers";
            this.config.url = PageModuleStatistics.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ PortalID: PageModuleStatistics.config.PortalID, UserName: PageModuleStatistics.config.UserName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindUsers: function(data) {
            var html = '';
            var UserList = data.d;
            $.each(UserList, function(index, item) {
                var style = index % 2 == 0 ? "sfEven" : "sfOdd";
                html += '<tr class= ' + style + '><td>' + item.UserName + '</td></tr>';
            });
            $('#tblUser').html(html);
        },
        ajaxSuccess: function(data) {
        $('#ajaxBusy').hide();
            switch (PageModuleStatistics.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    PageModuleStatistics.BindPageName(data);
                    break;
                case 2:
                    PageModuleStatistics.BindModules(data);
                    break;
                case 3:
                    PageModuleStatistics.BindUsers(data);
                    break;
                case 4:
                    PageModuleStatistics.BindGeneralSnapShot(data);
                    break;
            }
        }
    };
    PageModuleStatistics.init();
});
//]]>
</script>

<h2>
    Your Portal Snapshot</h2>
<div class="sfFormwrapper" id="tabPortalStat">
    <ul>
        <li><a id="liGeneral" href="#dvGeneral">General Snapshot</a></li>
        <li><a id="liPages" href="#dvPages">Pages</a></li>
        <li><a id="liModules" href="#dvModules">Modules</a></li>
        <li><a id="liUser" href="#dvUsers">Users</a></li>
    </ul>
    <div id="dvGeneral" class="sfSnapshotTabDiv sfGridwrapper">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="sfOdd">
                <td>
                    <label class="sfFormlabel">
                        You are:</label>
                </td>
                <td>
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="sfEven">
                <td>
                    <label class="sfFormlabel">
                        Portal Name:</label>
                </td>
                <td>
                    <asp:Label ID="lblPortalName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="sfOdd">
                <td>
                    <label class="sfFormlabel">
                        Pages Count:</label>
                </td>
                <td>
                    <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="sfEven">
                <td>
                    <label class="sfFormlabel">
                        Total No Of Users:</label>
                </td>
                <td>
                    <asp:Label ID="lblTotalUserCount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="sfOdd">
                <td>
                    <label class="sfFormlabel">
                        Users visit status:</label>
                </td>
                <td>
                    <asp:Label ID="lblUserCount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="sfEven">
                <td>
                    <label class="sfFormlabel">
                        Active Template:</label>
                </td>
                <td>
                    <asp:Label ID="lblTemplate" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPages" class="sfGridwrapper">
        <table id="tblPages" width="100%" cellpadding="0" cellspacing="0">
        </table>
    </div>
    <div id="dvModules" class="sfGridwrapper">
        <table id="tblModules" width="100%" cellpadding="0" cellspacing="0">
        </table>
    </div>
    <div id="dvUsers" class="sfGridwrapper">
        <table id="tblUser" width="100%" cellpadding="0" cellspacing="0">
        </table>
    </div>
</div>
