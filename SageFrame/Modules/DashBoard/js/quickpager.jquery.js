//-------------------------------------------------
//		Quick Pager jquery plugin
//		Created by dan and emanuel @geckonm.com
//		www.geckonewmedia.com
// 
//		v1.1
//		18/09/09 * bug fix by John V - http://blog.geekyjohn.com/
//-------------------------------------------------

(function($) {

    $.fn.quickPager = function(options) {

        var defaults = {
            pageSize: 10,
            currentPage: 1,
            holder: null,
            pagerLocation: "after",
            pagerClass: ""
        };

        var options = $.extend(defaults, options);


        return this.each(function() {


            var selector = $(this).find("tbody");
            
            var pageCounter = 1;

            //selector.wrap("<div class='simplePagerContainer'></div>");

            selector.children().each(function(i) {

                if (i < pageCounter * options.pageSize && i >= (pageCounter - 1) * options.pageSize) {
                    $(this).addClass("simplePagerPage" + pageCounter);
                }
                else {
                    $(this).addClass("simplePagerPage" + (pageCounter + 1));
                    pageCounter++;
                }


            });

            // show/hide the appropriate regions 
            selector.children().hide();
            selector.children(".simplePagerPage" + options.currentPage).show();

            if (pageCounter <= 1) {
                return;
            }

            //Build pager navigation
            var pageNav = "<table class='simplePagerNav'><tr class=" + options.pagerClass + ">";
            for (i = 1; i <= pageCounter; i++) {
                if (i == options.currentPage) {
                    pageNav += "<td class='currentPage simplePageNav" + i + "'><a class='active' rel='" + i + "' href='#'>" + i + "</a></td>";
                }
                else {
                    pageNav += "<td class='simplePageNav" + i + "'><a rel='" + i + "' href='#'>" + i + "</a></td>";
                }
            }
            pageNav += "</tr></table>";

            if (!options.holder) {
                switch (options.pagerLocation) {
                    case "before":
                        selector.before(pageNav);
                        break;
                    case "both":
                        selector.before(pageNav);
                        selector.after(pageNav);
                        break;
                    default:
                        selector.after(pageNav);
                }
            }
            else {
                $(options.holder).html(pageNav);
            }

            //pager navigation behaviour
            selector.parent().find(".simplePagerNav a").click(function() {

                //grab the REL attribute 
                var clickedLink = $(this).attr("rel");
                options.currentPage = clickedLink;
                if (options.holder) {
                    $(this).parents("tr").find('a.active').removeClass("active");
                    $(this).parents("tr").find("a[rel='" + clickedLink + "']").addClass("active");
                }
                else {
                    //remove current current (!) page
                    $(this).parents("tr").find('a.active').removeClass("active");
                    //Add current page highlighting
                    $(this).parents("tr").find("a[rel='" + clickedLink + "']").addClass("active");
                }

                //hide and show relevant links
                selector.children().hide();
                selector.find(".simplePagerPage" + clickedLink).show();

                return false;
            });
        });
    }


})(jQuery);

