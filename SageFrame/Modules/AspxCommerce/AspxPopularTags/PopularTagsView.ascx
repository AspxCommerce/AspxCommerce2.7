<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularTagsView.ascx.cs" Inherits="Modules_AspxCommerce_AspxPopularTags_PopularTagsView" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        var popularTagRss = '<%=IsEnablePopularTagRss %>';
        var rssFeedUrl = '<%=PopularTagsRssPageName %>';
        var ModuleCollapsible = "<%=ModuleCollapsible %>";

        $(".sfLocale").localize({
            moduleKey: AspxPopularTags
        });
        if (ModuleCollapsible.toLowerCase() == 'true') {
            $(".cssClassPopularTagHeader").addClass("sfCollapsible");
            $(".cssClassPopularTagHeader").on('click', function () {
                $(".cssClassPopularTagBox").slideToggle('fast');
            });
        }
        if (popularTagRss.toLowerCase() == 'true') {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#popularTagRssImage').parent('a').show();
            $('#popularTagRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=populartags');
            $('#popularTagRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#popularTagRssImage').removeAttr('title').attr('title', getLocale(AspxPopularTags, "Popular Tag Rss Feed Title"));
            $('#popularTagRssImage').removeAttr('alt').attr('alt', getLocale(AspxPopularTags, "Popular Tag Rss Feed Alt"));
        }
    });
    //]]>
</script>

<div class="cssClassLeftSideBox cssClassPopularTagWrapper" id="divPopularTag">
    <div class="cssClassPopularTagHeader">
        <h2 class="cssClassLeftHeader">
            <asp:Label ID="lblPopularTagsTitle" runat="server" Text="Popular Tags" CssClass="cssClassPopularTags"
                meta:resourcekey="lblPopularTagsTitleResource1"></asp:Label>
            <a href="#" class="cssRssImage" style="display: none">
                <img id="popularTagRssImage" alt="" src="" title="" />
            </a>
            <asp:Literal ID="ltrViewAllTag" runat="server" EnableViewState="false">
            </asp:Literal>
        </h2>
    </div>
    <div class="cssClassPopularTagBox">
        <asp:Literal ID="ltrPopularTags" runat="server" EnableViewState="false">
        </asp:Literal>
    </div>
    <div class="cssClassClear">
    </div>
</div>
