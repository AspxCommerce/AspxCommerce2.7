// jQuery File Tree Plugin
//
// Version 1.01
//
// Cory S.N. LaViska
// A Beautiful Site (http://abeautifulsite.net/)
// 24 March 2008
//
// Visit http://abeautifulsite.net/notebook.php?article=58 for more information
//
// Usage: $('.fileTreeDemo').fileTree( options, callback )
//
// Options:  root           - root folder to display; default = /
//           script         - location of the serverside AJAX file to use; default = jqueryFileTree.php
//           folderEvent    - event to trigger expand/collapse; default = click
//           expandSpeed    - default = 500 (ms); use -1 for no animation
//           collapseSpeed  - default = 500 (ms); use -1 for no animation
//           expandEasing   - easing function to use on expand (optional)
//           collapseEasing - easing function to use on collapse (optional)
//           multiFolder    - whether or not to limit the browser to one subfolder at a time
//           loadMessage    - Message to display while initial tree loads (can be HTML)
//
// History:
//
// 1.01 - updated to work with foreign characters in directory/file names (12 April 2008)
// 1.00 - released (24 March 2008)
//
// TERMS OF USE
// 
// jQuery File Tree is licensed under a Creative Commons License and is copyrighted (C)2008 by Cory S.N. LaViska.
// For details, visit http://creativecommons.org/licenses/by/3.0/us/
//
if (jQuery) (function($) {

    $.extend($.fn, {
        fileTree: function(o, h, dire) {///Modified to handle directory click:Paras
            // Defaults
            if (!o) var o = {};
            if (o.root == undefined) o.root = '/';
            if (o.script == undefined) o.script = 'jqueryFileTree.php';
            if (o.folderEvent == undefined) o.folderEvent = 'click';
            if (o.expandSpeed == undefined) o.expandSpeed = 500;
            if (o.collapseSpeed == undefined) o.collapseSpeed = 500;
            if (o.expandEasing == undefined) o.expandEasing = null;
            if (o.collapseEasing == undefined) o.collapseEasing = null;
            if (o.multiFolder == undefined) o.multiFolder = true;
            if (o.loadMessage == undefined) o.loadMessage = 'Loading...';

            $(this).each(function() {
                
                function showTree(c, t) {
                    $(c).addClass('wait');
                    $(".jqueryFileTree.start").remove();
                    $.post(o.script, { dir: t }, function(data) {
                        $(c).find('.start').html('');
                        $(c).removeClass('wait').append(data);
                        if (o.root == t) $(c).find('UL:hidden').show(); else $(c).find('UL:hidden').slideDown({ duration: o.expandSpeed, easing: o.expandEasing });
                        bindTree(c);
                    });
                }

                ///Modified to handle the directory expand and collapse style for other directory types
                ///also the directory click retruns a string containing rel and id
                function bindTree(t) {
                    $(t).find('LI A').bind(o.folderEvent, function() {
                    
                        if ($(this).parent().hasClass('directory')) {
                            if ($(this).parent().hasClass('collapsed')) {
                                dire($(this).attr('rel') + "##" + $(this).prop('id'));
                                $(this).addClass("cssClassTreeSelected");
                                // Expand
                                if (!o.multiFolder) {
                                    $(this).parent().parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                    $(this).parent().parent().find('LI.directory').removeClass('expanded sfActive').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded_db').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded_lock').addClass('collapsed');
                                    
                                }
                                $(this).parent().find('UL').remove(); // cleanup
                                showTree($(this).parent(), escape($(this).attr('rel').match(/.*\//)));
                                $(this).parent().removeClass('collapsed').addClass('expanded sfActive');
                            } else {
                                // Collapse
                                $(this).parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                $(this).parent().removeClass('expanded sfActive').addClass('collapsed');
                                

                            }
                        }
                        else if ($(this).parent().hasClass('database')) {
                            if ($(this).parent().hasClass('collapsed')) {
                                dire($(this).attr('rel') + "##" + $(this).prop('id'));
                                $(this).addClass("cssClassTreeSelectedDB");
                                // Expand
                                if (!o.multiFolder) {
                                    $(this).parent().parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                    $(this).parent().parent().find('LI.directory').removeClass('expanded sfActive').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded sfActive').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded_lock').addClass('collapsed');
                                }
                                $(this).parent().find('UL').remove(); // cleanup
                                showTree($(this).parent(), escape($(this).attr('rel').match(/.*\//)));
                                $(this).parent().removeClass('collapsed').addClass('expanded_db');
                            } else {
                                // Collapse
                                $(this).parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                

                            }
                        }
                        else if ($(this).parent().hasClass('locked')) {
                            if ($(this).parent().hasClass('collapsed')) {
                                dire($(this).attr('rel') + "##" + $(this).prop('id'));
                                $(this).addClass("cssClassTreeSelectedLocked");
                                // Expand
                                if (!o.multiFolder) {
                                    $(this).parent().parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                    $(this).parent().parent().find('LI.directory').removeClass('expanded sfActive').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded sfActive').addClass('collapsed');
                                    $('.jqueryFileTree li').removeClass('expanded_db').addClass('collapsed');
                                }
                                $(this).parent().find('UL').remove(); // cleanup
                                showTree($(this).parent(), escape($(this).attr('rel').match(/.*\//)));
                                $(this).parent().removeClass('collapsed').addClass('expanded_lock');
                            } else {
                                // Collapse
                                $(this).parent().find('UL').slideUp({ duration: o.collapseSpeed, easing: o.collapseEasing });
                                $(this).parent().removeClass('expanded_lock').addClass('collapsed');
                                $(this).parent().removeClass('expanded_db').addClass('collapsed');

                            }
                        }
                        else {
                            h($(this).attr('rel'));
                        }
                        return false;
                    });
                    // Prevent A from triggering the # on non-click events
                    if (o.folderEvent.toLowerCase != 'click') $(t).find('LI A').bind('click', function() { return false; });
                }
                // Loading message
                $(this).html('<ul class="jqueryFileTree start"><li class="wait">' + o.loadMessage + '<li></ul>');
                // Get the initial file list
                showTree($(this), escape(o.root));
            });
        }
    });

})(jQuery);