(function($) {
    $.fn.LoadFirst = function(templateFavIcon) {
        $('div.sfMessage').on("click", function() {
            $(this).slideUp();
        });
        $('a.sfManageControl').on("click", function(e) {
            var $this = $(this);
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            var screen_res = screen.width;
            var align = (screen_res - 800) / 2;
            var url = $(this).attr("rel");
            $('#divFrame').attr("src", url);
            var dialogOptions = {
                "title": $this.parents('.sfModule').find('.sfPosition').text(),
                "width": 800,
                "height": 600,
                "modal": true,
                "position": [align, 150],
                "z-index": 500,
                "close": function() { location.reload(); }
            };
            if ($("#button-cancel").attr("checked")) {
                dialogOptions["buttons"] = { "Cancel": function() {
                    $(this).dialog("close");
                }
                };
            }
            //dialog-extend options
            var dialogExtendOptions = {
                "maximize": true,
                "minimize": false
            };
            //open dialog
            $("#divFrame").dialog(dialogOptions).dialogExtend(dialogExtendOptions);
            $('div.ui-dialog').css("z-index", "3000");
            $('div.ui-dialog').resizable('destroy');
            return false;
        });
        $("#btnScrollTop").hide();
        // fade in #btnScrollTop           
        $(window).scroll(function() {
            if ($(this).scrollTop() > 100) {
                $('#btnScrollTop').fadeIn();
            } else {
                $('#btnScrollTop').fadeOut();
            }
            if ($(this).scrollTop() > 0) {
                $('.sfTopbar').css('position', 'fixed');
            } else {
                $('.sfTopbar').css('position', 'relative');
            }

        });

        // scroll body to 0px on click
        $('#btnScrollTop').click(function() {
            $('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
        var link = document.createElement('link');
        link.type = 'image/x-icon';
        link.rel = 'shortcut icon';
        link.href = templateFavIcon;
        document.getElementsByTagName('head')[0].appendChild(link);
    };
})(jQuery);

