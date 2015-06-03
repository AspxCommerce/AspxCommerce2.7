
(function($) {
    $.fn.LatestItemView = function(p) {
        p = $.extend
        ({        
            noOfLatestItems: 0,          
            tblRecentItems: '',
            latestItemRss: '',
            rssFeedUrl: ''
        }, p);       
        LatestItems = {
            LoadLatestItemRssImage: function() {
                var pageurl = aspxRedirectPath + p.rssFeedUrl + pageExtension;
                $('#latestItemRssImage').parent('a').show();
                $('#latestItemRssImage').parent('a').removeAttr('href').attr('href', pageurl + '');
                $('#latestItemRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#latestItemRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Latest Items Rss Feed Title"));
                $('#latestItemRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Latest Items Rss Feed Alt"));
            },
            init: function() {
                $("#divLatestItems").hide();
                if (p.noOfLatestItems > 0) {                    
                    if ($("#divLatestItems").find("span.cssClassNotFound").length ==0) {
                        var $container = $("#" + p.tblRecentItems);
                        $container.imagesLoaded(function () {
                            $container.masonry({
                                itemSelector: '.cssClassProductsBox',
                                EnableSorting: false
                            });
                        });
                    }
                    $("#divLatestItems").show();
                }
                $("#btnAddToMyCart").hide();             
                if (p.latestItemRss.toLowerCase() == 'true') {
                    LatestItems.LoadLatestItemRssImage();
                }
            }
        };
        LatestItems.init();  
    };
    $.fn.LatestItems = function(p) {
        $.fn.LatestItemView(p);
    };
})(jQuery);    