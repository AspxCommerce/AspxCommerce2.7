; (function($) {
    $.View = function(p) {
        p = $.extend
        ({
            Auto_Slide: 1,
            InfiniteLoop: '',
            NumericPager: '',
            TransitionMode: '',
            speed: '',
            pause: '',
            controls: ''
        }, p);
        var BannerView = {
            Init: function() {
                var Auto_Slide = p.Auto_Slide;
                var InfiniteLoop = p.InfiniteLoop;
                var NumericPager = p.NumericPager;
                var TransitionMode = p.TransitionMode;
                if (Auto_Slide == "True") {
                    Auto_Slide = true;
                } else {
                    Auto_Slide = false;
                }
                if (InfiniteLoop == "True") {
                    InfiniteLoop = true;
                } else {
                    InfiniteLoop = false;
                }
                if (NumericPager == "True") {
                    NumericPager = true;
                } else {
                    NumericPager = false;
                }
                TransitionMode = jQuery.trim(TransitionMode);
                var f = $("#sfSlider").bxSlider({
                    mode: TransitionMode,
                    infiniteLoop: InfiniteLoop,
                    speed: p.speed,
                    pager: NumericPager,
                    pagerType: "full",
                    pagerLocation: "bottom",
                    pagerActiveClass: "pager-active",
                    nextText: "next",
                    prevText: "prev",
                    captions: false,
                    captionsSelector: null,
                    auto: Auto_Slide,
                    autoDirection: "next",
                    autoControls: false,
                    autoControlsSelector: null,
                    autoStart: true,
                    autoHover: true,
                    pause: parseInt(p.pause),
                    startText: "start",
                    stopText: "stop",
                    stopImage: "",
                    wrapperClass: "",
                    startingSlide: 0,
                    displaySlideQty: 1,
                    moveSlideQty: 1,
                    controls: p.controls.toLowerCase() == "false" ? false : true
                });
                if (TransitionMode.toLowerCase() == 'horizontal') {
                    $('#sfSlider li').css('display', 'block');
                }
                $(".thumbs a").on("click", function() {
                    var b = $(".thumbs a").index(this);
                    f.goToSlide(b);
                    $(".thumbs a").removeClass("pager-active");
                    $(this).addClass("pager-active");
                    return false;
                });
                $(this).addClass("pager-active");
				setTimeout(function() {
                    var sliderheight = $('#sfSlider li:nth-child(1) img').height();
                    $('div.bx-viewport').css('height', sliderheight);
                }, 500);


              
                 if (p.TransitionMode == "horizontal") {
                    $('.bx-wrapper').addClass('sfHorz');
                }
				
				$('#sfSlider li').attr("display:none;");
            }
        };
        BannerView.Init();
    };
    $.fn.BannerView = function(p) {
        $.View(p);
    };
})(jQuery);