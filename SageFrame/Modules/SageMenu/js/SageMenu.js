(function ($) {
    $.createSageMenu = function (p) {
        p = $.extend({
            ContainerClientID: 'divNav1',
            MenuType: '1'
        }, p);
        var SageMenu = {
            config: {
                ContainerClientID: p.ContainerClientID,
                MenuType: p.MenuType
            },
            init: function () {
                this.BindEvents();
            },
            BindEvents: function () {
                $('#' + SageMenu.config.ContainerClientID).addClass("sfNavigation clearfix");
                SageMenu.InitializeSuperfish("ul.sfDropdown,ul.sf-navbar,ul.sf-vertical");
                if (SageMenu.config.MenuType == '2') {
                    SageMenu.BuildSideMenu();
                }
                //responsive menu
                $('#sfResponsiveNavBtn').unbind().bind('click', function () {
                    var $this = $(this);
                    if ($this.hasClass('inactive')) {
                        $this.removeClass('inactive').addClass('active');
                        $('body').addClass("offCanvas");
                    }
                    else {
                        $this.removeClass('active').addClass('inactive');
                         $('body').removeClass("offCanvas");
                    }
                });
            },
            InitializeSuperfish: function (selector) {
                jQuery(selector).superfish({
                    delay: 100, // one second delay on mouseout 
                    animation: {
                        opacity: 'show',
                        height: 'show'
                    }, // fade-in and slide-down animation 
                    speed: 'fast', // faster animation speed 
                    autoArrows: false, // disable generation of arrow mark-up 
                    dropShadows: false // disable drop shadows 
                });
            },
            BuildSideMenu: function (data, settings) {
                var menu = $(SageMenu.config.ContainerClientID + " ul li");
                $.each(menu, function (index, item) {
                    var hreflink = $(this).find("a").attr("href");
                    if (hreflink !== undefined) {
                        if (location.href.toLowerCase().indexOf(p.Extension) == -1) {
                            if (location.href.toLowerCase().indexOf(hreflink.toLowerCase()) > -1 || hreflink.toLowerCase().indexOf(SageFrameDefaultPage) > -1) {
                                $(this).find("a:first").addClass('sfExpanded sfActive');
                                $(this).parents("ul").prev("a.sfParent").addClass("sfExpanded");
                                $(this).parents("ul").slideDown();
                            }
                        } else {
                            if (location.href.toLowerCase().indexOf(hreflink.toLowerCase()) > -1) {
                                $(this).find("a:first").addClass('sfExpanded sfActive');
                                $(this).parents("ul").prev("a.sfParent").addClass("sfExpanded");
                                $(this).parents("li.sfLevel1").find("ul:first").slideDown()
                  .end();
                                $(this).parents("ul").slideDown();
                                $(this).find("a:first").next("ul").slideDown();
                            }
                        }
                    }
                });
                $('a.sfParent').on("click", function () {
                    if ($(this).hasClass("sfExpanded")) {
                        $(this).removeClass("sfExpanded");
                        $(this).next("ul").slideUp();
                        return false;
                    } else {
                        $(this).addClass("sfExpanded");
                        $(this).next("ul").slideDown();
                        return false;
                    }
                });
            }
        };
        SageMenu.init();
    };
    $.fn.SageMenuBuilder = function (p) {
        $.createSageMenu(p);
    };
})(jQuery);