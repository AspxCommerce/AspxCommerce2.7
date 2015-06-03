/*   11/21/2010
PikaChoose
Jquery plugin for photo galleries
Copyright (C) 2010 Jeremy Fry

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
//=============UserDefined functions==============================



//===============================================================================================================
//======================provided jQuery==========================================================================
(function($) {
    var defaults = { autoPlay: true, speed: 5000, text: { play: "", stop: "", previous: "", next: "" }, transition: [1], showCaption: true, IESafe: false, showTooltips: false, animationFinished: null };
    $.fn.PikaChoose = function(o) {
        return this.each(
    function() {
        $(this).data('pikachoose', new $pc(this, o))
    ;
    }
    );
    }

    $.PikaChoose = function(e, o) {
        this.options = $.extend(
        {}, defaults, o || {})
        ;
        this.list = null;
        this.image = null;
        this.anchor = null;
        this.caption = null;
        this.imgNav = null;
        this.imgPlay = null;
        this.imgPrev = null;
        this.imgNext = null;
        this.textNext = null;
        this.textPrev = null;
        this.previous = null;
        this.next = null;
        this.aniDiv = null;
        this.thumbs = null;
        this.transition = null;
        this.active = null;
        this.tooltip = null;
        this.animating = false;
        this.stillOut = null;
        if (e.nodeName == 'UL' || e.nodeName == 'OL') {
            this.list = $(e);
            this.build();
            this.bindEvents();
        }
        else {
            return;
        }
        var y = 0;
        var x = 0;
        for (var t = 0; t < 25; t++) {
            var a = '<div col="' + y + '" row="' + x + '"></div>';
            this.aniDiv.append(a);
            y++
            if (y == 5) {
                x++;
                y = 0;
            }
        }
    };
    var $pc = $.PikaChoose;
    $pc.fn = $pc.prototype = { pikachoose: '4.1.4' };
    $pc.fn.extend = $pc.extend = $.extend;
    $pc.fn.extend({ build: function() {
        this.step = 0;
        this.wrap = $("<div class='pika-image'></div>").insertBefore(this.list);
        LatestItems.AddStyle();
        this.image = $("<img id=\"image2\">").appendTo(this.wrap);
        // alert("inside pickachoose height is " + newObject.height);
        //height=" + newObject.height + " width=" + newObject.width + "
        //===================adding Zoom effect to the image in gallery=============================================

        this.anchor = this.image.wrap("<a>").parent();
        $("#image2").css('height', newObject.height);
        $("#image2").css('width', newObject.width);
        this.image.wrap("<div id=\"zoom01\" class=\"zoom\">").parent();

        // ImageZoom();
        this.imgNav = $("<div class='pika-imgnav'></div>").insertAfter(this.anchor);

        this.imgPlay = $("<a></a>").appendTo(this.imgNav);
        if (this.options.autoPlay) {
            this.imgPlay.addClass('pause');
        }
        else {
            this.imgPlay.addClass('play');
        }
        this.imgPrev = $("<a class='previous'></a>").insertAfter(this.imgPlay);
        $('a.previous').css("height", newObject.height);

        this.imgNext = $("<a class='next'></a>").insertAfter(this.imgPrev);
        $('a.next').css("height", newObject.height);
        this.caption = $("<div class='caption'></div>").insertAfter(this.imgNav);
        if (!this.options.showCaption) {
            this.caption.hide();
        }
        this.tooltip = $("<div class='pika-tooltip'></div>").insertAfter(this.list);
        this.tooltip.hide();
        this.aniDiv = $("<div class='animation'></div>").insertAfter(this.caption);
        this.textNav = $("<div class='pika-textnav'></div>").insertAfter(this.aniDiv);
        this.textPrev = $("<a class='previous'>" + this.options.text.previous + "</a>").appendTo(this.textNav); this.textNext = $("<a class='next'>" + this.options.text.next + "</a>").appendTo(this.textNav); this.list.addClass('pika-thumbs'); this.list.children('li').wrapInner("<div class='clip' />");


        // $('div.clip').css('margin', 10);
        this.thumbs = this.list.find('img'); this.active = this.thumbs.eq(0); this.finishAnimating({ 'source': this.active.attr('ref') || this.active.attr('src'), 'caption': this.active.parents('li:first').find('span:first').html(), 'clickThrough': this.active.parent().attr('href') || "" }); var self = this; this.thumbs.each(function() { self.createThumb($(this), self); }); if (typeof (this.options.buildFinished) == 'function') { this.options.buildFinished(this); }
    }, createThumb: function(ele) {

        var self = ele;
        var that = this;
        self.hide();
        $.data(ele[0], 'clickThrough', self.parent('a').attr('href') || "#");
        if (self.parent('a').length > 0) {
            self.unwrap();
        }
        $.data(ele[0], 'caption', self.next('span').html() || "");
        self.next('span').remove(); $.data(ele[0], 'source', self.attr('ref') || self.attr('src'));
        $.data(ele[0], 'order', self.closest('ul').find('li').index(self.parents('li')));
        var data = $.data(ele[0]);
        $('<img />').bind('load', { data: data }, function() {
            var img = $(this);
            var w = img.width();
            var h = img.height();
            if (w === 0) {
                w = img.attr("width");
            }
            if (h === 0) {
                h = img.attr("height");
            }
            $('div.clip').css('height', newObject.thumbHeight);
            $('div.clip').css('width', newObject.thumbWidth);

            var rw = parseInt(self.parents('.clip').css('width').slice(0, -2)) / w;
            var rh = parseInt(self.parents('.clip').css('height').slice(0, -2)) / h;
            var ratio;
            if (rw < rh) {
                ratio = rh;
                var left = ((w * ratio - parseInt(self.parents('.clip').css('width').slice(0, -2))) / 2) * -1;
                left = Math.round(left);
                self.css({ left: left });
            }
            else {
                ratio = rw;
                self.css({ top: 0 });
            }

            var width = Math.round(w * ratio);
            var height = Math.round(h * ratio);
            self.css("position", "relative");
            var imgcss = { width: newObject.thumbWidth + "px", height: newObject.thumbHeight + "px", right: 2 + "px", left: 1 + "px" };
            self.css(imgcss);
            self.hover(function(e) {
                clearTimeout(that.stillOut);
                $(this).stop(true, true).fadeTo(250, 1);
                if (!that.options.showTooltips) {
                    return;
                }
                that.tooltip.show().stop(true, true).html(data.caption).animate({ top: $(this).parent().position().top, left: $(this).parent().position().left, opacity: 1.0 }, 'fast');
            }, function(e) {
                if (!$(this).hasClass("active")) {
                    $(this).stop(true, true).fadeTo(250, 0.4);
                    that.stillOut = setTimeout(that.hideTooltip, 700);
                }
            });
            if (data.order == 0) {
                self.fadeTo(250, 1);
                self.addClass('active');
            }
            else {
                self.fadeTo(250, 0.4);
            }
        }).attr('src', self.attr('src'));
    }, bindEvents: function() {
        this.thumbs.bind('click', { self: this }, this.imgClick);
        this.imgNext.bind('click', { self: this }, this.nextClick);
        this.textNext.bind('click', { self: this }, this.nextClick);
        this.imgPrev.bind('click', { self: this }, this.prevClick);
        this.textPrev.bind('click', { self: this }, this.prevClick);
        this.imgPlay.bind('click', { self: this }, this.playClick);
        this.wrap.bind('mouseenter', { self: this }, function(e) {
            e.data.self.imgPlay.stop(true, true).fadeIn('fast');
        }
       );
        this.wrap.bind('mouseleave', { self: this }, function(e) {
            e.data.self.imgPlay.stop(true, true).fadeOut('fast');
        }
        );
        this.tooltip.bind('mouseenter', {
            self: this
        }, function(e) {
            clearTimeout(e.data.self.stillOut);
        }
           );
        this.tooltip.bind('mouseleave', { self: this }, function(e) {
            e.data.self.stillOut = setTimeout(e.data.self.hideTooltip, 700);
        });
    }, hideTooltip: function(e) {
        $(".pika-tooltip").animate({ opacity: 0.01 });
    }, imgClick: function(e, x) {
        var self = e.data.self;
        var data = $.data(this);
        if (self.animating) {
            return;
        }
        self.caption.fadeOut('slow');
        if (typeof (x) == 'undefined' || x.how != "auto") {
            if (self.options.autoPlay) {
                self.imgPlay.trigger('click');
            }
        }
        self.animating = true;
        self.active.fadeTo(300, 0.4).removeClass('active');
        self.active = $(this);
        self.active.addClass('active').fadeTo(200, 1);
        $('<img />').bind('load', {
            self: self,
            data: data
        }, function() {
            self.aniDiv.css({
                height: self.image.height(),
                width: self.image.width()
            }).fadeIn('fast');
            self.aniDiv.children('div').css(
            { 'width': '20%', 'height': '20%', 'float': 'left' }
            );
            var n = 0;
            if (self.options.transition[0] == -1) {
                n = Math.floor(Math.random() * 6) + 1;
            }
            else {
                n = self.options.transition[self.step];
                self.step++;
                if (self.step >= self.options.transition.length) {
                    self.step = 0;
                }
            }
            if (self.options.IESafe && $.browser.msie) {
                n = 1;
            }
            self.doAnimation(n, data);
        }).attr('src', $.data(this).source);
    }, doAnimation: function(n, data) {
        var self = this;
        var aWidth = self.aniDiv.children('div').eq(0).width();
        var aHeight = self.aniDiv.children('div').eq(0).height();
        self.aniDiv.children().each(function() {
            var div = $(this);
            var xOffset = Math.floor(div.parent().width() / 5) * div.attr('col');
            var yOffset = Math.floor(div.parent().height() / 5) * div.attr('row');
            div.css({ 'background': 'url(' + data.source + ') -' + xOffset + 'px -' + yOffset + 'px', 'width': '0px', 'height': '0px', 'position': 'absolute', 'top': yOffset + 'px', 'left': xOffset + 'px', 'float': 'none'
            });
        });

        switch (n) {

            case 0: self.aniDiv.hide();
                self.image.fadeOut('slow', function() {
                    self.image.attr('src', data.source).fadeIn('slow', function() {
                        self.finishAnimating(data);
                    }
             );
                }
              );
                break;
            case 1: self.aniDiv.height(self.image.height()).hide().css({ 'background': 'url(' + data.source + ') top left no-repeat' });
                self.aniDiv.children('div').hide();
                self.aniDiv.fadeIn(1, function() {
                    self.finishAnimating(data);
                    self.aniDiv.css({ 'background': 'transparent' });
                }
                 );

                break;

            case 2: self.aniDiv.children().hide().each(function(index) {
                var delay = index * 30;
                $(this).css({ opacity: 0.1 }).show().delay(delay).animate({ opacity: 1, "width": aWidth, "height": aHeight }, 200, 'linear', function() {
                    if ($(".animation div").index(this) == 24) {
                        self.finishAnimating(data);
                    }
                }
                      );
            }
                       );
                break;

            case 3: self.aniDiv.children("div:lt(5)").hide().each(function(index) {
                var delay = $(this).attr('col') * 100;
                $(this).css({ opacity: 0.1, "width": aWidth }).show().delay(delay).animate({ opacity: 1, "height": self.image.height() }, 700, 'linear', function() {
                    if ($(".animation div").index(this) == 4) {
                        self.finishAnimating(data);
                    }
                }
                 );
            });
                break;
            case 4: self.aniDiv.children().hide().each(function(index) {
                var delay = $(this).attr('col') * 10;
                aHeight = self.gapper($(this), aHeight);
                $(this).css({ opacity: 0.1, "height": aHeight }).show().delay(delay).animate({ opacity: 1, "width": aWidth }, 800, 'linear', function() {
                    if ($(".animation div").index(this) == 24) {
                        self.finishAnimating(data);
                    }
                }
              );
            }
               );
                break;

            case 5: self.aniDiv.children().show().each(function(index) {
                var delay = index * Math.floor(Math.random() * 5) * 10;
                aHeight = self.gapper($(this), aHeight);
                if ($(".animation div").index(this) == 24) {
                    delay = 800;
                }
                $(this).css({ "height": aHeight, "width": aWidth, "opacity": .01 }).delay(delay).animate({ "opacity": 1 }, 800, function() {
                    if ($(".animation div").index(this) == 24) {
                        self.finishAnimating(data);
                    }
                }
                 );
            });
                break;

            case 6: self.aniDiv.height(self.image.height()).hide().css({ 'background': 'url(' + data.source + ') top left no-repeat' });
                self.aniDiv.children('div').hide();
                self.aniDiv.css({ width: 0 }).show().animate({ width: self.image.width() }, 'slow', function() {
                    self.finishAnimating(data);
                    self.aniDiv.css({ 'background': 'transparent' });
                }
             );
                break;
        }
    }, finishAnimating: function(data) {
        this.animating = false;
        this.image.attr('src', data.source);
        this.aniDiv.hide();
        this.anchor.attr('href', data.clickThrough);
        if (this.options.showCaption) {
            this.caption.html(data.caption).fadeIn('slow');
        }
        if (this.options.autoPlay == true) {
            var self = this;
            this.image.delay(this.options.speed).fadeIn(0, function() {
                if (self.options.autoPlay) {
                    self.nextClick();
                }
            }
        );
        }
        if (typeof (this.options.animationFinished) == 'function') {
            this.options.animationFinished(this);
        }
    }, gapper: function(ele, aHeight) {
        if (ele.attr('row') == 9 && ele.attr('col') == 0) {
            var gap = ani_divs.height() - (aHeight * 9);
            return gap;
        }
        return aHeight;
    }, nextClick: function(e) {
        var how = "natural";
        if (e != undefined) {
            try {
                var self = e.data.self;
                if (typeof (e.data.self.options.next) == 'function') {
                    e.data.self.options.next(this);
                }
            }

            catch (err) {
                var self = this;
                how = "auto";
            }
        }
        else {
            var self = this;
            how = "auto";
        }
        var next = self.active.parents('li:first').next().find('img');
        if (next.length == 0) {
            next = self.list.find('img').eq(0);
        };
        next.trigger('click', { how: how });
    }, prevClick: function(e) {
        if (typeof (e.data.self.options.previous) == 'function') {
            e.data.self.options.previous(this);
        }
        var self = e.data.self;
        var prev = self.active.parents('li:first').prev().find('img');
        if (prev.length == 0) {
            prev = self.list.find('img:last');
        };
        prev.trigger('click');
    }, playClick: function(e) {
        var self = e.data.self;
        self.options.autoPlay = !self.options.autoPlay;
        self.imgPlay.toggleClass('play').toggleClass('pause');
        if (self.options.autoPlay) {
            self.nextClick();
        }
    }
    });
})(jQuery);

