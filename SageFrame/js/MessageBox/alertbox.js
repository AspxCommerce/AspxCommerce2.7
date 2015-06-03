var x;
$(document).ready(function() {  
    if ($('body').find('#BoxOverlay').length == 0) {      
        csscody.initialize();
    }
});

jQuery.bind = function(object, method) {
    var args = Array.prototype.slice.call(arguments, 2);
    return function() {
        var args2 = [this].concat(args, $.makeArray(arguments));
        return method.apply(object, args2);
    };
};

jQuery.fn.setDelay = function(time, func) {
    return this.each(function() {
        //if (func != undefined) {
            setTimeout(func, time);
        //}
    });
};

function sleep(ms,func) {
    var dt = new Date();
    dt.setTime(dt.getTime() + ms);
    while (new Date().getTime() < dt.getTime());
    func;
}


jQuery.fn.extend({
    $chain: [],
    chain: function(fn) {
        this.$chain.push(fn);
        return this;
    },
    callChain: function(context) {
        return (this.$chain.length) ? this.$chain.pop().apply(context, arguments) : false;
    },
    clearChain: function() {
        this.$chain.empty();
        return this;
    }
});

(function($) {

    csscody = {
        getOptions: function() {
            return {
                name: 'alert',
                zIndex: 999,
                onReturn: false,
                onReturnFunction: function(e) { },
                BoxStyles: { 'width': 500 },
                OverlayStyles: { 'backgroundColor': '#000', 'opacity': 0.7 },
                showDuration: 100,
                closeDuration: 100,
                moveDuration: 500,
                onCloseComplete: $.bind(this, function() {
                    this.options.onReturnFunction(this.options.onReturn);
                })
            };
        },


        initialize: function(options) {           
            this.i = 0;
            this.options = $.extend(this.getOptions(), options);
            $('body').append('<div id="BoxOverlay"></div><div id="' + this.options.name + '-Box"><div id="' + this.options.name + '-InBox"><div id="' + this.options.name + '-BoxContent"><div id="' + this.options.name + '-BoxContenedor"></div></div></div></div>');

            this.Content = $('#' + this.options.name + '-BoxContenedor');
            this.Contenedor = $('#' + this.options.name + '-BoxContent');
            this.InBox = $('#' + this.options.name + '-InBox');
            this.Box = $('#' + this.options.name + '-Box');

            $('#BoxOverlay').css({
                position: 'fixed',
                top: 0,
                left: 0,
                opacity: this.options.OverlayStyles.opacity,
                backgroundColor: this.options.OverlayStyles.backgroundColor,
                'z-index': this.options.zIndex,
                height: $(document).height(),
                width: $(document).width()
            }).hide();

            this.Box.css({
                display: 'none',
                position: 'fixed'
               // top: x, //$(window).height()/ 2,
               // left: 0,
                //'z-index': this.options.zIndex + 2,
               // width: this.options.BoxStyles.width + 'px'
            });
            //Milson's Chanages 
            //this.preloadImages();

            $(window).bind('resize', $.bind(this, function() {
                if (this.options.display == 1) {
                    $('#BoxOverlay').css({
                        height: 0,
                        width: 0
                    });
                    $('#BoxOverlay').css({
                        height: $(document).height(),
                        width: $(document).width()
                    });
                    // this.replaceBox();
                }
            }));

            this.Box.bind('keydown', $.bind(this, function(obj, event) {
                if (event.keyCode == 27) {
                    this.options.onReturn = false;
                    this.display(0);
                }
            }));

            $(window).bind('scroll', $.bind(this, function() {
                //  this.replaceBox();                 
            }));

        },

        replaceBox: function() {
            if (this.options.display == 1) {

                this.Box.stop();

                this.Box.animate({
                    left: (($(document).width() - this.options.BoxStyles.width) / 2),
                    top: ($(document).scrollTop() + ($(window).height() - this.Box.outerHeight()) / 2)
                }, {
                    duration: this.options.moveDuration,
                    easing: 'easeOutBack'
                });

                $(this).setDelay(this.options.moveDuration, $.bind(this, function() {
                    $('#BoxAlertBtnOk').focus();
                    $('#BoxPromptInput').focus();
                    $('#BoxConfirmBtnOk').focus();
                }));
            }
        },

        display: function(option) {
            if (this.options.display == 0 && option != 0 || option == 1) {

                if ($.browser.msie && $.browser.version == "6.0") {//IE6
                    $('embed, object, select').css({ '-ms-filter': 'alpha(opacity=0)' });
                }

                //                if (!$.support.maxHeight) { //IE6
                //                    $('embed, object, select').css({ 'visibility': 'hidden' });
                //                }

                this.togFlashObjects('hidden');

                this.options.display = 1;
                $('#BoxOverlay').css({
                    height: $(document).height(),
                    width: $(document).width()
                });

                $('#BoxOverlay').stop();
                $('#BoxOverlay').fadeIn(this.options.showDuration, $.bind(this, function() {

                    this.Box.css({
                        //                        display: 'block',
                        //                        left: (($(document).width() - this.options.BoxStyles.width) / 2)


                        display: 'block',
                        position: 'fixed'
                        //top: $(window).height() / 2, //x + $(window).height() / (2.5)
                        //left: (($(document).width() - this.options.BoxStyles.width) / 2),
                        //'z-index': this.options.zIndex + 2,
                        //width: this.options.BoxStyles.width + 'px'
                    });
                    //  this.replaceBox();
                }));

            } else {
                this.Box.css({
                    display: 'none'
                   // top: x//$(window).height()/2
                });

                this.options.display = 0;

                $(this).setDelay(500, $.bind(this, this.queue));

                $(this.Content).empty();
                this.Content.removeClass();

                if (this.i == 1) {
                    $('#BoxOverlay').stop();
                    $('#BoxOverlay').fadeOut(this.options.closeDuration, $.bind(this, function() {
                        $('#BoxOverlay').hide();
                        if ($.browser.msie && $.browser.version == "6.0") {//IE6
                            $('embed, object, select').css({ '-ms-filter': 'alpha(opacity=0)' });
                        }
                        //            if (!$.support.maxHeight) { //IE6
                        //              $('embed, object, select').css({ 'visibility' : 'hidden' });
                        //            }

                        this.togFlashObjects('visible');

                        this.options.onCloseComplete.call();
                    }));
                }
                $('#stwrapper, #stwrapper #footer, #stwrapper #main').hide().css('z-index', '-9999');    
            }
        },

        messageBox: function(type, message, properties, input) {
            x = $(window).scrollTop();
            $(this).chain(function() {

                properties = $.extend({
                    'textBoxBtnOk': getLocale(CoreJsLanguage, 'OK'),
                    'textBoxBtnCancel': getLocale(CoreJsLanguage, 'Cancel'),
                    'textBoxContinue': getLocale(CoreJsLanguage, 'Continue Shopping'),
                    'textBoxCheckOut': getLocale(CoreJsLanguage, 'CheckOut'),
                    'textBoxBtnYes': getLocale(CoreJsLanguage, 'Yes'),
                    'textBoxBtnNo': getLocale(CoreJsLanguage, 'No'),
                    'textBoxInputPrompt': null,
                    'password': false,
                    'onComplete': function(e) { }
                }, properties || {});

                this.options.onReturnFunction = properties.onComplete;

                this.Content.append('<div id="' + this.options.name + '-Buttons"></div>');
                if (type == 'alert' || type == 'info' || type == 'error') {
                    $('#' + this.options.name + '-Buttons').append('<input id="BoxAlertBtnOk" class="sfBtn" type="submit" />');

                    $('#BoxAlertBtnOk').val(properties.textBoxBtnOk).css({ 'width': 70 });

                    $('#BoxAlertBtnOk').bind('click', $.bind(this, function() {
                        this.options.onReturn = true;                       
                        this.display(0);
                    }));

                    if (type == 'alert') {
                        clase = 'BoxAlert';
                    } else if (type == 'error') {
                        clase = 'BoxError';
                    } else if (type == 'info') {
                        clase = 'BoxInfo';
                    }

                    this.Content.addClass(clase).prepend(message);
                    this.display(1);

                }
                else if (type == 'confirm') {
                    $('#' + this.options.name + '-Buttons').append('<input id="BoxConfirmBtnOk" type="submit" /> <input id="BoxConfirmBtnCancel" type="submit" />');
                    $('#BoxConfirmBtnOk').val(properties.textBoxBtnOk).css({ 'width': 70 });
                    $('#BoxConfirmBtnCancel').val(properties.textBoxBtnCancel).css({ 'width': 70 });

                    $('#BoxConfirmBtnOk').bind('click', $.bind(this, function() {
                        this.options.onReturn = true;
                        this.display(0);
                    }));

                    $('#BoxConfirmBtnCancel').bind('click', $.bind(this, function() {
                        this.options.onReturn = false;
                        this.display(0);
                    }));

                    this.Content.addClass('BoxConfirm').prepend(message);
                    this.display(1);
                }
                else if (type == 'prompt') {

                    $('#' + this.options.name + '-Buttons').append('<input id="BoxPromptBtnOk" type="submit" /> <input id="BoxPromptBtnCancel" type="submit" />');
                    $('#BoxPromptBtnOk').val(properties.textBoxBtnOk).css({ 'width': 70 });
                    $('#BoxPromptBtnCancel').val(properties.textBoxBtnCancel).css({ 'width': 70 });

                    type = properties.password ? 'password' : 'text';

                    this.Content.prepend('<input id="BoxPromptInput" type="' + type + '" />');
                    $('#BoxPromptInput').val(properties.input);
                    $('#BoxPromptInput').css({ 'width': 250 });

                    $('#BoxPromptBtnOk').bind('click', $.bind(this, function() {
                        this.options.onReturn = $('#BoxPromptInput').val();
                        this.display(0);
                    }));

                    $('#BoxPromptBtnCancel').bind('click', $.bind(this, function() {
                        this.options.onReturn = false;
                        this.display(0);
                    }));

                    this.Content.addClass('BoxPrompt').prepend(message + '<br />');
                    this.display(1);
                }
                else if (type == 'messageInfo') {
                    $('#' + this.options.name + '-Buttons').append('<input id="BoxMessageInfoBtnOk" type="submit" /> <input id="BoxMessageInfoBtnCancel" type="submit" />');
                    $('#BoxMessageInfoBtnOk').val(properties.textBoxBtnYes).css({ 'width': 70 });
                    $('#BoxMessageInfoBtnCancel').val(properties.textBoxBtnNo).css({ 'width': 70 });

                    $('#BoxMessageInfoBtnOk').bind('click', $.bind(this, function() {
                        this.options.onReturn = true;
                        this.display(0);
                    }));

                    $('#BoxMessageInfoBtnCancel').bind('click', $.bind(this, function() {
                        this.options.onReturn = false;
                        this.display(0);
                    }));

                    this.Content.addClass('MessageInfo').prepend(message);
                    this.display(1);

                }
                else if (type == 'addToCart') {
                    $('#' + this.options.name + '-Buttons').append('<input id="BoxContinueBtn" type="submit" class="cssClassSubmitBtn" /> <input id="BoxCheckOutBtn" type="submit" class="cssClassSubmitBtn" />');
                    $('#BoxContinueBtn').val(properties.textBoxContinue).css({ 'width': 150 });
                    $('#BoxCheckOutBtn').val(properties.textBoxCheckOut).css({ 'width': 120 });

                    type = properties.password ? 'password' : 'text';

                    $('#BoxContinueBtn').bind('click', $.bind(this, function() {
                        this.options.onReturn = false;
                        this.display(0);                        
                    }));

                    $('#BoxCheckOutBtn').bind('click', $.bind(this, function() {
                        this.options.onReturn = true;
                        this.display(0);
                    }));

                    this.Content.addClass('AddToCartInfo').prepend(message + '<br />');
                    this.display(1);
                }
                else {
                    this.options.onReturn = false;
                    this.display(0);
                }

            });

            this.i++;

            if (this.i == 1) {
                $(this).callChain(this);
            }
        },

        queue: function() {
            this.i--;
            $(this).callChain(this);
        },

        chk: function(obj) {
            return !!(obj || obj === 0);
        },

        togFlashObjects: function(state) {
            var hideobj = new Array("embed", "iframe", "object");
            for (y = 0; y < hideobj.length; y++) {
                var objs = document.getElementsByTagName(hideobj[y]);
                for (i = 0; i < objs.length; i++) {
                    objs[i].style.visibility = state;
                }
            }
        },

        preloadImages: function() {
            var img = new Array(2);
            img[0] = new Image(); img[1] = new Image(); img[2] = new Image();
            img[0].src = this.Box.css('background-image').replace(new RegExp("url\\('?([^']*)'?\\)", 'gi'), "$1");
            img[1].src = this.InBox.css('background-image').replace(new RegExp("url\\('?([^']*)'?\\)", 'gi'), "$1");
            img[2].src = this.Contenedor.css('background-image').replace(new RegExp("url\\('?([^']*)'?\\)", 'gi'), "$1");
        },

        /*
        Property: alert
        Shortcut for alert // http://www.csscody.com/demo   --  more examples here ----//
      
    Argument:
        properties - see Options in messageBox
        */
        alert: function (message, properties) {           
            this.messageBox('alert', message, properties);
            $(document).keypress(function (e) {
                if (e.which == 13) {                  
                    $('#BoxAlertBtnOk').trigger('click');
                    return false;
                }
            });
        },

        /*
        Property: info
        Shortcut for alert info
      
    Argument:
        properties - see Options in messageBox
        */
        info: function(message, properties) {
            this.messageBox('info', message, properties);
            $(document).keypress(function (e) {
                if (e.which == 13) {
                    $('#BoxAlertBtnOk').trigger('click');
                    return false;
                }
            });
        },

        /*
        Property: error
        Shortcut for alert error
      
    Argument:
        properties - see Options in messageBox
        */
        error: function(message, properties) {
            this.messageBox('error', message, properties);
        },

        /*
        Property: confirm
        Shortcut for confirm
      
    Argument:
        properties - see Options in messageBox
        */
        confirm: function(message, properties) {
            this.messageBox('confirm', message, properties);
        },

        /*
        Property: prompt
        Shortcut for prompt
      
       Argument:
        properties - see Options in messageBox
        */
        messageInfo: function(message, properties) {
            this.messageBox('messageInfo', message, properties);
        },

        addToCart: function(message, properties) {
            this.messageBox('addToCart', message, properties);
        },

        /*
        Property: messageInfo
        Shortcut for messageInfo
      
    Argument:
        properties - see Options in messageBox
        */
        prompt: function(message, input, properties) {
            this.messageBox('prompt', message, properties, input);
        }
    };
})(jQuery);