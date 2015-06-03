/*
Author: Shyam Yadav
*/

var messages = {};

(function ($) {
    var settings = {
        inEffect: { opacity: 'show' }, // in effect
        inEffectDuration: 600, 			// in effect duration in miliseconds
        stayTime: 3000, 			// time in miliseconds before the item has to disappear
        text: '', 				// content of the item. Might be a string or a jQuery object. Be aware that any jQuery object which is acting as a message will be deleted when the toast is fading away.
        sticky: false, 			// should the toast item sticky or not?
        type: 'notice', 			// notice, warning, error, success
        position: 'middle-center',        // top-left, top-center, top-right, middle-left, middle-center, middle-right ... Position of the toast container holding different toast. Position can be set only once at the very first call, changing the position after the first call does nothing
        closeText: '',                 // text which will be shown as close button, set to '' when you want to introduce an image via css
        close: null                // callback function when the toastmessage is closed
    };

    messages = {
        init: function (options) {      
            if (options) {
                $.extend(settings, options);
            }
        },       
          showToast: function (options) {
            var localSettings = {};
            $.extend(localSettings, settings, options);
            var toastWrapAll, toastItemOuter, toastItemInner, toastItemClose, toastItemImage;
            toastWrapAll = (!$('.toast-container').length) ? $('<div></div>').addClass('toast-container').addClass('toast-position-' + localSettings.position).appendTo('body') : $('.toast-container');
            toastItemOuter = $('<div></div>').addClass('toast-item-wrapper');
            toastItemInner = $('<div></div>').hide().addClass('toast-item toast-type-' + localSettings.type).appendTo(toastWrapAll).html($('<p>').append(localSettings.text)).animate(localSettings.inEffect, localSettings.inEffectDuration).wrap(toastItemOuter);
            toastItemClose = $('<div></div>').addClass('toast-item-close').prependTo(toastItemInner).html(localSettings.closeText).click(function () { $().toastmessage('removeToast', toastItemInner, localSettings) });
            toastItemImage = $('<div></div>').addClass('toast-item-image').addClass('toast-item-image-' + localSettings.type).prependTo(toastItemInner);

            if (navigator.userAgent.match(/MSIE 6/i)) {
                toastWrapAll.css({ top: document.documentElement.scrollTop });
            }
            if (!localSettings.sticky) {
                setTimeout(function () {
                    $().toastmessage('removeToast', toastItemInner, localSettings);
                },
				localSettings.stayTime);
            }
            return toastItemInner;

        },
        showMessage: function (message, msgType) {                           
            var options = { text: message, type: msgType };
            return $().toastmessage('showToast', options);
        },
        removeToast: function (obj, options) {
            obj.animate({ opacity: '0' }, 600, function () {
                obj.parent().animate({ height: '0px' }, 300, function () {
                    obj.parent().remove();
                });
            });
            // callback
            if (options && options.close !== null) {
                options.close();
            }
        }
    };

    $.fn.toastmessage = function (method) {  
        // Method calling logic
        if (messages[method]) {
            return messages[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return messages.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.toastmessage');
        }
    };

})(jQuery);