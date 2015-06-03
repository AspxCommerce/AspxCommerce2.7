/*
 * Sliding Tabs 1.1.6 jQuery Plugin - http://codecanyon.net/item/sliding-tabs-jquery-plugin/141774
 * 
 * Copyright 2011, Christian André
 * All rights reserved.*
 *
 */

(function($) {

    $.fn.slideTabs = function(client_config) {
        var default_config = {
            autoplay: false,
            autoplayClickStop: false,
            autoplayInterval: 4000,
            autoHeight: false,
            autoHeightTime: 0,
            buttonsFunction: 'slide',
            classBtnDisabled: 'st_btn_disabled',
            classBtnNext: 'st_next',
            classBtnPrev: 'st_prev',
            classExtLink: 'st_ext',
            classTab: 'st_tab',
            classTabActive: 'st_tab_active',
            classTabsContainer: 'st_tabs_container',
            classTabsList: 'st_tabs',
            classView: 'st_view',
            classViewActive: 'st_active_view',
            classViewContainer: 'st_view_container',
            contentAnim: 'slideH',
            contentAnimTime: 600,
            contentEasing: 'easeInOutExpo',
            offsetBR: 0,
            offsetTL: 0,
            orientation: 'horizontal',
            tabSaveState: false,
            tabsAnimTime: 300,
            tabsEasing: '',
            tabsScroll: true,
            tabsSlideLength: 0,
            totalHeight: 0,
            totalWidth: 0,
            urlLinking: true
        },

		conf = $.extend(true, {}, default_config, client_config);

        return this.each(function() {
            var tabs = new SlideTabs($(this), conf);
            tabs.init();
        });
    };

    function SlideTabs($container, conf) {
        var $tabsCont = $container.find('.' + conf.classTabsContainer),
			$tabsInnerCont = $tabsCont.children('div').first(),
			$tabs = $tabsCont.find('.' + conf.classTabsList),
			$a = $tabs.children('li').find('a'),
			$contentCont = $container.find('.' + conf.classViewContainer),
			$content = $contentCont.find('.' + conf.classView),
			$prev = $container.find('.' + conf.classBtnPrev).click(function() { tabs[conf.buttonsFunction + 'Prev'](val); return false; }), // Bind the prev button click event
			$next = $container.find('.' + conf.classBtnNext).click(function() { tabs[conf.buttonsFunction + 'Next'](val); return false; }), // Bind the next button click event					
			$tab, $activeTab = [], $li, $lastElem, $view, $activeView,
			val = {}, margin = 0;

        this.init = function() {
            // Set the correct function and object names
            if (conf.orientation == 'horizontal') {
                $tabsInnerCont.css('overflow', 'hidden');
                val.func = 'outerWidth';
                val.obj = 'left';
                val.attr = 'marginLeft';
            } else {
                val.func = 'outerHeight';
                val.obj = 'top';
                val.attr = 'marginTop';
                val.prevBtnH = $prev.outerHeight();
                val.nextBtnH = $next.outerHeight();
                (val.prevBtnH >= val.nextBtnH) ? val.buttonsH = val.prevBtnH : val.buttonsH = val.nextBtnH;
            }

            // Set the dimensions if specified in the options
            if (conf.totalWidth > 0) { resize.width(); }
            if (conf.totalHeight > 0) { resize.height(); }

            tabs.init();
            if (conf.autoplay == true) { autoplay.init(); } // Set intervals if autoplay is enabled						
        };

        /* 
        * Resizing methods
        */
        var resize = {
            width: function() {
                var contentContOW = ($contentCont.outerWidth() - $contentCont.width());

                $container.css('width', conf.totalWidth + 'px');

                if (conf.orientation == 'horizontal') {
                    var tabsContOW = ($tabsInnerCont.outerWidth() - $tabsInnerCont.width()),
					    buttonsW = ($prev.outerWidth('true') + $next.outerWidth('true'));

                    $tabsInnerCont.css('width', ((conf.totalWidth - buttonsW) - tabsContOW) + 'px');
                    $contentCont.css('width', (conf.totalWidth - contentContOW) + 'px');
                } else {
                    $contentCont.css('width', ((conf.totalWidth - $tabsCont.outerWidth('true')) - contentContOW) + 'px');
                }
            },

            height: function() {
                var contentContOH = ($contentCont.outerHeight() - $contentCont.height());

                $container.css('height', conf.totalHeight + 'px');

                if (conf.orientation == 'vertical') {
                    var tabsContOH = ($tabsCont.outerHeight() - $tabsCont.height());

                    $tabsCont.css('height', (conf.totalHeight - tabsContOH) + 'px');
                    $contentCont.css('height', (conf.totalHeight - contentContOH) + 'px');
                } else {
                    $contentCont.css('height', (conf.totalHeight - $tabsCont.outerHeight()) + 'px');
                }
            }
        },

        /* 
        * Tab methods
        */
		tabs = {
		    animated: '#' + $container.attr('id') + ' .' + conf.classTabsList + ':animated',

		    init: function() {
		        this.setSlideLength(); // Set the tabs slide length value
		        this.posActive(); // Position the active tab			
		        this.bind(); // Bind tab events				
		    },

		    setSlideLength: function() {
		        if (conf.tabsSlideLength == 0) {
		            if (conf.orientation == 'horizontal') { conf.tabsSlideLength = $tabsInnerCont.outerWidth('true'); }
		            else { conf.tabsSlideLength = ($tabsCont.height() - val.buttonsH); }
		        }
		    },

		    bind: function() {

		        // Delegate the tabs click event
		        $tabs.find('li a.' + conf.classTab).unbind().bind('click', function() {
		            tabs.click(this, true);
		            return false;
		        });

		        // Mouse scroll wheel event				
		        if ($.fn.mousewheel && conf.tabsScroll == true) {
		            $tabs.mousewheel(function(event, delta) {
		                (delta > 0) ? tabs.slidePrev(val) : tabs.slideNext(val);
		                return false; // Prevents default scrolling
		            });
		        }

		        // Find any external links on the page
		        $('a.' + conf.classExtLink).each(function() {
		            // Bind the links with a matching rel attribute					
		            if ($(this).attr('rel') == $container.attr('id')) {
		                $(this).click(function() {
		                    $tab = tabs.findByRel($(this).attr('href').slice(1));
		                    tabs.click($tab);
		                    return false;
		                });
		            };
		        });
		    },

		    posActive: function() {
		        // Get the active tab
		        this.getActive();

		        // Show the active tab's content
		        content.showActive();

		        $lastElem = $tabs.children('li:last');
		        $tab = $activeTab;
		        $activeTab = $activeTab.parent('li');

		        if (($lastElem[val.func](true) + $lastElem.position()[val.obj]) > conf.tabsSlideLength) {
		            // Get the values needed to get the total width/height of the tabs
		            val.elemD = $activeTab[val.func](true);
		            val.elemP = $activeTab.position()[val.obj];

		            // Find the active element's position
		            if (val.elemP > conf.tabsSlideLength) {
		                margin += (val.elemD + (val.elemP - conf.tabsSlideLength));
		                margin = (margin + conf.offsetBR);
		            } else if ((val.elemP + val.elemD) > conf.tabsSlideLength) {
		                margin += (val.elemD - (conf.tabsSlideLength - val.elemP));
		                margin = (margin + conf.offsetBR);
		            } else {
		                margin = (margin - conf.offsetTL);
		            }

		            // Set the active element's position					
		            $tabs.css(val.attr, - +margin);

		            // Show the directional buttons after the position has been set
		            this.initButtons();
		            this.showButtons();
		        }
		    },

		    initButtons: function() {
		        if (conf.buttonsFunction == 'slide') {
		            // Deactivate the arrow button if the tab element is at the beginning or end of the list													
		            if ($tabs.children("li:first").position()[val.obj] == (0 + conf.offsetTL)) { this.disableButton($prev); }
		            else { this.enableButton($prev); }

		            if (($lastElem.position()[val.obj] + $lastElem[val.func](true)) == (conf.tabsSlideLength - conf.offsetBR)) { this.disableButton($next); }
		            else { this.enableButton($next); }
		        } else {
		            this.setButtonState();
		        }
		    },

		    enableButton: function($btn) {
		        $btn.removeClass(conf.classBtnDisabled);
		    },

		    disableButton: function($btn) {
		        $btn.addClass(conf.classBtnDisabled);
		    },

		    showButtons: function() {
		        // Show the directional buttons
		        $prev.show(); $next.show();
		    },

		    click: function(tab, organic) {
		        // Return false if an animation is already running
		        if ($(content.animated).length) { return false; }

		        $tab = $(tab);

		        // Return false if the active tab is clicked
		        if ($tab.hasClass(conf.classTabActive)) { return false; }

		        $li = $tab.parent('li');

		        // Set the new active state
		        this.setActive();

		        if (conf.autoplay == true) {
		            if (conf.autoplayClickStop == true && organic == true) {
		                // Disable autoplay
		                conf.autoplay = false;
		                autoplay.clearInterval();
		            } else {
		                val.index = $tab.parent().index(); // Set the clicked tab's index			
		                autoplay.setInterval(); // Set new interval
		            }
		        }

		        // Get the element's position
		        val.elemP = $li.position();
		        val.activeElemP = $activeTab.position();
		        // Get the clicked tab's hash value
		        val.hash = this.getHash($tab);

		        // Slide partially hidden tab into view
		        this.slideClicked(val);

		        // Set the content vars and remove/add the active class
		        $activeView = $content.children('div.' + conf.classViewActive).removeClass(conf.classViewActive);
		        $view = $content.children('div' + val.hash).addClass(conf.classViewActive);

		        if (conf.autoHeight == true) { content.adjustHeight(); }

		        // Show/animate the clicked tab's content into view	
		        if (conf.contentAnim.length > 0) {
		            content[conf.contentAnim](val);
		        } else {
		            $activeView.hide(); $view.show();
		        }
		    },

		    clickPrev: function() {
		        // Return false if an animation is already running				
		        if ($(content.animated).length) { return false; }

		        // Find the previous tab
		        val.$prevTab = this.find('prev');
		        if (val.$prevTab.length) { this.click(val.$prevTab); }
		    },

		    clickNext: function() {
		        // Return false if an animation is already running				
		        if ($(content.animated).length) { return false; }

		        // Find the next tab
		        val.$nextTab = this.find('next');
		        if (val.$nextTab.length) { this.click(val.$nextTab); }
		    },

		    find: function(func) {
		        return $tab.parent()[func]().children('a.' + conf.classTab);
		    },

		    findByRel: function(rel) {
		        // Find the tab via it's rel attribute
		        return $tabs.find('[rel=' + rel + ']');
		    },

		    getHash: function($tab) {
		        // Get the tab's hash value				
		        val.hash = $tab.attr('hash');

		        if (val.hash) { return val.hash; }
		        else { return $tab.prop('hash'); }
		    },

		    getActive: function() {
		        if (conf.urlLinking == true && location.hash) {
		            // Use the URL's hash value to find the active-tab via it's rel attribute
		            $activeTab = this.findByRel(location.hash.slice(1));
		        }

		        // If no tab element has been found
		        if (!$activeTab.length) {
		            // See if the active-tab id is saved in a cookie
		            if ($.cookie) { var savedTabIndex = $.cookie($container.attr('id')); }

		            if (savedTabIndex) {
		                // Remove the static active class
		                this.removeActive();

		                // Set the active class on the saved tab						
		                $activeTab = $a.eq(savedTabIndex).addClass(conf.classTabActive);
		            } else {
		                $activeTab = $tabs.children('li').find('.' + conf.classTabActive);

		                // Set the first tab element to 'active' if no tab element has the active class
		                if (!$activeTab.length) { $activeTab = $tabs.find('a:first').addClass(conf.classTabActive); }
		            }

		        } else {
		            // Remove the static active class
		            this.removeActive();
		            // Add the active class to the tab
		            $activeTab.addClass(conf.classTabActive);
		        }

		        this.saveActive($activeTab);
		    },

		    removeActive: function() {
		        // Remove the current active class
		        $tabs.children('li').find('.' + conf.classTabActive).removeClass(conf.classTabActive);
		    },

		    setActive: function() {
		        // Set new active states				
		        $activeTab = $tabs.children('li').find('a.' + conf.classTabActive).removeClass(conf.classTabActive);
		        $tab.addClass(conf.classTabActive);
		        this.saveActive($tab);
		    },

		    saveActive: function($tab) {
		        // Save the active tab's parent index value in a cookie so we can retrive it later
		        if (conf.tabSaveState == true) { $.cookie($container.attr('id'), $tab.parent('li').index()); }
		    },

		    slideClicked: function(val) {
		        val.elemP = val.elemP[val.obj];
		        val.elemD = $li[val.func](true);
		        val.nextElemPos = ($li.next().length == 1) ? $li.next().position()[val.obj] : 0;

		        if (val.elemP < (0 + conf.offsetTL)) {
		            val.elemHidden = (val.elemD - val.nextElemPos);
		            margin = (margin - (val.elemHidden + conf.offsetTL));

		            this.enableButton($next);
		        } else if ((val.elemD + val.elemP) > (conf.tabsSlideLength - conf.offsetBR)) {
		            margin += (val.elemD - (conf.tabsSlideLength - (val.elemP + conf.offsetBR)));

		            this.enableButton($prev);
		        }

		        this.animate();
		        this.setButtonState();
		    },

		    slidePrev: function(val) {
		        // Return false if an animation is running
		        if ($(tabs.animated).length) { return false; }

		        // Find the element and set the margin 			
		        $tabs.children('li').each(function() {
		            $li = $(this);
		            val.elemP = $li.position()[val.obj];

		            if (val.elemP >= (0 + conf.offsetTL)) {
		                val.elemHidden = ($li.prev()[val.func](true) - val.elemP);
		                margin = ((margin - val.elemHidden) - conf.offsetTL);

		                $li = $li.prev(); // Set the $li variable to the first visible element

		                tabs.animate();
		                tabs.setButtonState($next);

		                return false;
		            }
		        });
		    },

		    slideNext: function(val) {		      
		        // Return false if an animation is running
		        if ($(tabs.animated).length) { return false; }

		        // Find the element and set the margin					
		        $tabs.children('li').each(function() {
		            $li = $(this);
		            val.elemD = $li[val.func](true);
		            val.elemP = $li.position()[val.obj];

		            if ((val.elemD + val.elemP) > (conf.tabsSlideLength - conf.offsetBR)) {
		                val.elemHidden = (conf.tabsSlideLength - val.elemP);
		                margin += ((val.elemD - val.elemHidden) + conf.offsetBR);

		                tabs.animate();
		                tabs.setButtonState($prev);

		                return false;
		            }
		        });
		    },

		    animate: function() {
		        // Animate tabs with the new value					
		        if (conf.orientation == 'horizontal') { $tabs.animate({ 'marginLeft': - +margin }, conf.tabsAnimTime, conf.tabsEasing); }
		        else { $tabs.animate({ 'marginTop': - +margin }, conf.tabsAnimTime, conf.tabsEasing); }
		    },

		    setButtonState: function($btn) {
		        // If the directional buttons are set to click through the tabs, get the active tab instead of the first visible
		        if (conf.buttonsFunction == 'click') { $li = $tab.parent('li'); }

		        if ($li.is(':first-child')) { this.disableButton($prev); this.enableButton($next); } // Is the active or visible tab the first one?
		        else if ($li.is(':last-child')) { this.disableButton($next); this.enableButton($prev); } // Is the active or visible tab the last one?
		        else {
		            if ($btn) { this.enableButton($btn); } // Enable the specified button only
		            else if (conf.buttonsFunction == 'click') { this.enableButton($prev); this.enableButton($next); } // Enable both buttons										
		        }
		    }
		},

        /* 
        * Content methods
        */
		content = {
		    animated: '#' + $container.attr('id') + ' :animated',

		    showActive: function() {
		        // Show the active tab's content
		        $view = $content.children($activeTab.attr('href')).addClass(conf.classViewActive);

		        // Set the content div's to absolute and hide the 'inactive' content		
		        $content.children('div').css('position', 'absolute').not('div.' + conf.classViewActive).hide();

		        // Set the content container's height if autoHeight is set to: true
		        if (conf.autoHeight == true) { $content.css('height', $view.height()).parent().css('height', 'auto'); }
		    },

		    adjustHeight: function() {
		        // Set the content's height
		        if (conf.autoHeightTime > 0) { $content.animate({ 'height': $view.height() }, conf.autoHeightTime); }
		        else { $content.css('height', $view.height()); }
		    },

		    fade: function() {
		        $activeView.fadeOut(conf.contentAnimTime);
		        $view.fadeIn(conf.contentAnimTime);
		    },

		    fadeOutIn: function() {
		        $activeView.fadeOut(conf.contentAnimTime, function() {
		            $view.fadeIn(conf.contentAnimTime);
		        });
		    },

		    slideH: function(val) {
		        val.wh = $container.outerWidth(true);
		        this.setSlideValues(val);

		        $activeView.animate({ 'left': val.animVal }, conf.contentAnimTime, conf.contentEasing);

		        $view.css({ 'display': 'block', 'left': val.cssVal }).animate({ 'left': '0px' }, conf.contentAnimTime, conf.contentEasing, function() {
		            $activeView.css('display', 'none');
		        });
		    },

		    slideV: function(val) {
		        val.wh = $container.outerHeight(true);
		        this.setSlideValues(val);

		        $activeView.animate({ 'top': val.animVal }, conf.contentAnimTime, conf.contentEasing);

		        $view.css({ 'display': 'block', 'top': val.cssVal }).animate({ 'top': '0px' }, conf.contentAnimTime, conf.contentEasing, function() {
		            $activeView.css('display', 'none');
		        });
		    },

		    setSlideValues: function(val) {
		        if (val.elemP > val.activeElemP[val.obj]) { val.animVal = -val.wh; val.cssVal = val.wh; }
		        else { val.animVal = val.wh; val.cssVal = -val.wh; }
		    }
		},

        /* 
        * Autoplay methods
        */
		autoplay = {
		    init: function() {
		        val.index = 0;
		        this.setInterval(); // Set the autoplay interval
		    },

		    setInterval: function() {
		        this.clearInterval();
		        // Set the new interval
		        val.intervalId = setInterval(function() { autoplay.play(); }, conf.autoplayInterval);
		    },

		    clearInterval: function() {
		        // Clear any previous interval
		        clearInterval(val.intervalId);
		    },

		    play: function() {
		        // Set the next tab's index				
		        val.index++; if (val.index == $a.length) { val.index = 0; }
		        // Trigger the click event for the next tab
		        tabs.click($a[val.index]);
		    }
		};
    };

})(jQuery);