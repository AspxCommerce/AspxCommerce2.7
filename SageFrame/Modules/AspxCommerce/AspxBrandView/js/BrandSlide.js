(function ($) {
    $.BrandSlideView = function (p) {
        p = $.extend
        ({
            enableBrandRss: '',
            brandRssPage: ''
        }, p);
        var BrandSlide = {
            LoadFrontBrandRssImage: function () {
                var pageurl = aspxRedirectPath + p.brandRssPage + pageExtension;
                $('#frontBrandRssImage').parent('a').show();
                $('#frontBrandRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=brands');
                $('#frontBrandRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#frontBrandRssImage').removeAttr('title').attr('title', getLocale(AspxBrandView, "Popular Brands Rss Feed Title"));
                $('#frontBrandRssImage').removeAttr('alt').attr('alt', getLocale(AspxBrandView, "Popular Brands Rss Feed Alt"));
            },
            Init: function () {                
                $(".cssClassBrandWrapper").show();
                if (p.enableBrandRss.toLowerCase() == "true") {
                    BrandSlide.LoadFrontBrandRssImage();
                }
               setTimeout(function(){
                    $("#brandSlider").bxSlider({
                        auto: true,
                        slideWidth: 120,
                        minSlides: 2,
                        maxSlides: 10,
                        moveSlides: 1,
                        slideMargin: 10,
                        controls: false
                    });
                    },500);
            }
        };
        BrandSlide.Init();
    };
    $.fn.BrandSlide = function (p) {
        $.BrandSlideView(p);
    };
})(jQuery);