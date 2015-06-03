<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Specials.ascx.cs" Inherits="Modules_AspxSpecialsItems_Specials" %>
<script type="text/javascript">
    //<![CDATA[
    var countSpecials = eval('<%=NoOfSpecialItems%>');
    var specialItemRss = '<%=SpecialItemRss %>';
    var rssFeedUrl = '<%=RssFeedUrl %>';
    //]]>
</script>
<div id="divSpecialItems" class="cssClassProducts">
    <h1>
        <asp:Label ID="Label1" runat="server" Text="Special Items"
            CssClass="cssClassLabel" meta:resourcekey="Label1Resource1"></asp:Label>
        <a class="cssRssImage" href="#" style="display: none">
            <img id="specialItemRssImage" alt="" src="" title="" />
        </a>
    </h1>
    <div id="divSpclBox" class="cssClassSpecialBox" runat="server" enableviewstate="false">
    <asp:Literal ID="ltrSpecialItems" runat="server" EnableViewState="False" 
            meta:resourcekey="ltrSpecialItemsResource1"></asp:Literal>
    </div>
    <span class="cssClassViewMore" id="divViewMoreSpecial" runat="server" enableviewstate="false"></span>
</div>