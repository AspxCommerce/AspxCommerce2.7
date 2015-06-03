$(function () {
    var SpecialItems = {
        LoadSpecialItemRssImage: function () {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#specialItemRssImage').parent('a').show();
            $('#specialItemRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=specialitems');
            $('#specialItemRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#specialItemRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Special Items Rss Feed Title"));
            $('#specialItemRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Special Items Rss Feed Alt"));
        },
        Init: function () {
            $("#divSpecialItems").hide();
            $("#divSpecialItems").show();
            if (specialItemRss.toLowerCase() == 'true') {
                SpecialItems.LoadSpecialItemRssImage();
            }
        }
    };
    SpecialItems.Init();
});
  