/*
 * jDownload - A jQuery plugin to assist file downloads
 * Examples and documentation at: http://jdownloadplugin.com
 * Version: 1.4 (08/04/2011)
 * Copyright (c) 2010 Adam Chambers, Tim Myers
 * Licensed under the GNU General Public License v3: http://www.gnu.org/licenses/gpl.html
 * Requires: jQuery v1.4+ & jQueryUI 1.8+
*/

(function($) {

    $.fn.jDownload = function(settings) {
        var config = {
            root: "/",
            filePath: null,
            event: "click", // default click event??
            dialogTitle: null,
            dialogDesc: getLocale(CoreJsLanguage, "Download the file now?"),
            dialogWidth: 400,
            dialogHeight: 'auto',
            dialogModal: true,
            showfileInfo: true,
            start: null,
            stop: null,
            download: null,
            cancel: null
        }
        settings = $.extend(config, settings);

        var dialogID = "jDownloadDialog_" + $('.jDownloadDialog').length;
        var iframeID = "jDownloadFrame_" + $('.jDownloadFrame').length;

        // create html iframe and dialog
        var iframeHTML = '<iframe class="jDownloadFrame" src="javascript:;" id="' + iframeID + '"></iframe>';
        var dialogHTML = '<div class="jDownloadDialog" title="' + getLocale(CoreJsLanguage, settings.dialogTitle) + '" id="' + dialogID + '"></div>';

        // append both to document
        $('body .jDownloadFrame').remove();
        $('body .jDownloadDialog').remove();
        $('body').append(iframeHTML + dialogHTML);


        var iframe = $('#' + iframeID);
        var dialog = $('#' + dialogID);

        // set iframe styles
        iframe.css({
            "height": "0px",
            "width": "0px",
            "visibility": "hidden"
        });

        // set dialog options
        dialog.dialog({
            autoOpen: false,
            buttons: [{
                text: getLocale(CoreJsLanguage, "Cancel"),
                click: function(e) {
                    if ($.isFunction(settings.cancel)) {
                        settings.cancel();
                    }
                    $(this).dialog('close');
                    e.returnValue = false;
                }
            },
                {
                    text: getLocale(CoreJsLanguage, "Download"),
                    click: function() {
                        if ($.isFunction(settings.download)) {
                            settings.download();
                        }
                        start_download();
                    }

}],
            width: settings.dialogWidth,
            height: settings.dialogHeight,
            modal: settings.dialogModal,
            //reset: $(this).unbind(settings.event),
            close: $(this).unbind(settings.event)
            //($.isFunction(settings.stop)) ? settings.stop() : null
        });
       
        $(this).off().on(settings.event, function() {
            if ($.isFunction(settings.start)) {
                settings.start();
            }
            var $this = $(this);

            dialog.html("");

            // if filePath is not specified then use the href attribute
            var filePath = (settings.filePath == null) ? $(this).attr('href') : settings.filePath;

            dialog.html('<p>' + getLocale(CoreJsLanguage, "Fetching File...") + '</p><img src="' + settings.root + 'loader.gif" alt="Loading..." />');

            if (settings.showfileInfo == true) {

                var url = settings.root + 'DownloadHandler.ashx';
                $.ajax({
                    url: url,
                    type: 'GET',
                    dataType: 'json',
                    data: 'action=info&path=' + filePath,
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        dialog.html("<p class=\"jDownloadError\">" + getLocale(CoreJsLanguage, "Fatal Error.") + "</p>");
                    },
                    success: function(data) {
                        setTimeout(function() {
                            if (data == 'error') {

                                dialog.html("<p class=\"jDownloadError\">" + getLocale(CoreJsLanguage, "File cannot be found.") + "</p>");

                            }
                            else {
                                //check to see if file available or not
                                if (data != null) {
                                    // Check to see if file is not allowed
                                    if (data.error == 'denied') {

                                        // append new file info
                                        dialog.html('<p class=\"jDownloadError\">' + getLocale(CoreJsLanguage, "This file type is not allowed.") + '</p>');

                                    } else {

                                        // parse JSON "FileName":"image1","FileType":"image/jpeg","FileSize":61

                                        var html = "<div class=\"jDownloadInfo\">";
                                        html += "<p><span>" + getLocale(CoreJsLanguage, "File Name:") + "</span> " + data.FileName + "</p>";
                                        html += "<p><span>" + getLocale(CoreJsLanguage, "File Type:") + "</span> " + data.FileType + "</p>";
                                        html += "<p><span>" + getLocale(CoreJsLanguage, "File Size:") + "</span> " + data.FileSize + " KB</p>";
                                        html += "</div>";

                                        // remove any old file info & error messages
                                        $('.jDownloadInfo, .jDownloadError').remove();

                                        var desc = ($this.prop('title').length > 0) ? $this.prop('title') : getLocale(CoreJsLanguage, "Download the file now?");

                                        // append new file info
                                        dialog.html('<p>' + desc + '</p>' + html);

                                    }
                                }
                                else {
                                    dialog.html('<p class=\"jDownloadError\">' + getLocale(CoreJsLanguage, "This file is not found!") + '</p>');
                                }
                            }
                        });
                    }
                }
                    , 1000);
            }

            // open dialog 
            dialog.data('jDownloadData', { filePath: filePath }).dialog('open');
            return false;


        });
        $(this).trigger(settings.event);
        /* Iniate download when value Ok is iniated via the dialog */
        function start_download(i) {

            // alert(settings.root);
            // change iframe src to fieDownload.php with filePath as query string?? 
            iframe.prop('src', settings.root + 'DownloadHandler.ashx?action=download&path=' + dialog.data('jDownloadData').filePath);

            // Close dialog
            //dialog.dialog('close');
            ($.isFunction(settings.stop)) ? settings.stop() : null;
            dialog.dialog('close');
            return false;
        }

    }


})(jQuery);