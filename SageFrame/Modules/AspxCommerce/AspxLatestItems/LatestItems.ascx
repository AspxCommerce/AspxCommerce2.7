<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestItems.ascx.cs" Inherits="Modules_AspxLatestItems_LatestItems" %>
<div id="divLatestItems" class="cssClassProducts">
    <h1>
        <asp:Label ID="lblRecentItemsHeading" runat="server" Text="New Arrivals"
            CssClass="cssClassLabel" meta:resourcekey="lblRecentItemsHeadingResource2"></asp:Label>
        <a class="cssRssImage" href="#" style="display: none">
            <img id="latestItemRssImage" alt="" src="" title="" />
        </a>
    </h1>
    <div id="tblRecentItems" runat="server" enableviewstate="false" class="cssLatestItemContainer">
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $(this).LatestItems({
            noOfLatestItems: '<%=NoOfLatestItems %>',
            tblRecentItems: '<%=tblRecentItems.ClientID%>',
            latestItemRss: '<%=EnableLatestItemRss %>',
            rssFeedUrl: '<%=RssFeedUrl %>'
        });
    });
</script>