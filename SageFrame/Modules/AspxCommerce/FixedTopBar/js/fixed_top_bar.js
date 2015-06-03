var box_down = false;
function slide_up() {
    if (box_down == true) {
        $("#top_side").animate({
            top: '-33',
            opacity: 1
        }, { queue: false, duration: "slow" }, "easein");
        box_down = false;
    }
};
function slide_down() {
    if (box_down == false) {
        $("#top_side").animate({
            top: '+0',
            opacity: 1
        }, { queue: false, duration: "slow" }, "easein");
        box_down = true;
    }
};
if (jQuery.browser.msie == true && jQuery.browser.version <= 6) {
    /* IE6 Hack - Start */
    $(document).ready(function() {
        function resize_for_ie6() {
            $("#top_side").width($(".ie6_wrapper").width() - 20);
        }
        resize_for_ie6();
        $(".ie6_wrapper").resize(function() {
            resize_for_ie6();
        });
        $(".ie6_wrapper").scroll(function(event_info) {
            debugging = $(".ie6_wrapper").scrollTop();
            check_scroll_position = $(".ie6_wrapper").scrollTop();
            if (check_scroll_position <= 30) {
                setTimeout("slide_up()", 500);
                           } else {
                setTimeout("slide_down()", 500);
                           }
            $('#event_info_text').html('<div>'+getLocale(FixedTopBar,'.scroll called.')+'</div>' + debugging);
            return false;
        });
    });
    /* IE6 Hack - End */
} else {
    $(document).ready(function() {
        $(window).scroll(function(event_info) {
            debugging = $(window).scrollTop();
            check_scroll_position = $(window).scrollTop();
            if (check_scroll_position <= 30) {
                slide_up();
            } else {
                slide_down();
            }
            $('#event_info_text').html('<div>' + getLocale(FixedTopBar, '.scroll called.') + '</div>' + debugging);
            return true;
        });
    });
};
