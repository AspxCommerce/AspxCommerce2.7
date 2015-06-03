<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxRssFeed.ascx.cs" Inherits="Modules_AspxCommerce_AspxRssFeed_AspxRssFeed" %>
<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxRssFeedLocale
        });
    });
    //<![CDATA[
    $(function() {
        var rssFeed = {
            init: function() {
                // rssFeed.LoadRssImage();
            },
            LoadRssImage: function() {
                var pageurl = aspxRedirectPath + "RssFeed" + pageExtension;
                $('.cssRssFeedImg').parent('a').removeAttr('href').attr('href', pageurl + '?type=rss&action=bestselleritems');
                $('.cssRssFeedImg').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            }
        };
        rssFeed.init();
    });
    //]]>
</script>

<a href="#" style="display: none">
    <img id="rssFeedImage" class="cssRssFeedImg" alt="" src="" title="" />
</a>