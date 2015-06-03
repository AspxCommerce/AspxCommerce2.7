String.Format = function() {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}
var dialogConfirmed = false;
function ConfirmDialog(obj, title, dialogText) {
    if (!dialogConfirmed) {
        $('body').append(String.Format("<div id='dialog-confirm' title='{0}'><p>{1}</p></div>",
                    title, dialogText));
        if (title == "message") {
            $('#dialog-confirm').dialog
                    ({
                        height: 110,
                        modal: true,
                        resizable: false,
                        draggable: false,
                        close: function(event, ui) { $('body').find('#dialog-confirm').remove(); },
                        buttons:
                        {
                            'OK': function() {
                                $(this).dialog('close');
                            }
                        }
                    });
        }
        else {
            $('#dialog-confirm').dialog
                    ({
                        height: 110,
                        modal: true,
                        resizable: false,
                        draggable: false,
                        close: function(event, ui) { $('body').find('#dialog-confirm').remove(); },
                        buttons:
                        {
                            'Yes': function() {
                                $(this).dialog('close');
                                dialogConfirmed = true;
                                if (obj) obj.click();
                            },
                            'No': function() {
                                $(this).dialog('close');
                            }
                        }
                    });
        }
    }
    return dialogConfirmed;
}
function pageLoad(sender, args) {
    if (args.get_isPartialLoad()) {
        String.Format = function() {
            var s = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, arguments[i + 1]);
            }
            return s;
        }
        var dialogConfirmed = false;
        function ConfirmDialog(obj, title, dialogText) {
            if (!dialogConfirmed) {
                $('body').append(String.Format("<div id='dialog-confirm' title='{0}'><p>{1}</p></div>",
                    title, dialogText));
                if (title == "message") {
                    $('#dialog-confirm').dialog
                    ({
                        height: 110,
                        modal: true,
                        resizable: false,
                        draggable: false,
                        close: function(event, ui) { $('body').find('#dialog-confirm').remove(); },
                        buttons:
                        {
                            'OK': function() {
                                $(this).dialog('close');
                            }
                        }
                    });
                }
                else {
                    $('#dialog-confirm').dialog
                    ({
                        height: 110,
                        modal: true,
                        resizable: false,
                        draggable: false,
                        close: function(event, ui) { $('body').find('#dialog-confirm').remove(); },
                        buttons:
                        {
                            'Yes': function() {
                                $(this).dialog('close');
                                dialogConfirmed = true;
                                if (obj) obj.click();
                            },
                            'No': function() {
                                $(this).dialog('close');
                            }
                        }
                    });
                }
            }
            return dialogConfirmed;
        }
    }
}
$(function() {
    $('.ajax__tab_tab').off().on("click", function() { 
        var index = $('span.ajax__tab_active').index();
        $('.ajax__tab_body').find('.ajax__tab_panel').eq(index).find('div.sfCollapsewrapper div.sfCollapsecontent').eq(0).show();
        //$('.ajax__tab_body').find('.ajax__tab_panel').eq(index).find('div.sfCollapsewrapper div.sfAccordianholder').removeClass("Active");
        //$('.ajax__tab_body').find('.ajax__tab_panel').eq(index).find('div.sfCollapsewrapper div.sfAccordianholder').eq(0).addClass("Active");
    });
});
$(function() {
    $('div.sfCollapsecontent').hide();
    //$('div.sfCollapsewrapper div.sfCollapsecontent').show();
    $('div.sfCollapsewrapper div.sfCollapsecontent').eq(0).show();
    $('div.sfCollapsewrapper div.sfAccordianholder').eq(0).addClass("Active");
    $('div.sfAccordianholder').off().on("click", function() {
        if (!$(this).hasClass("Active")) {
           
            $(this).addClass("Active");
            $(this).parent().next("div").slideDown("fast");
        }
        else {
            $(this).removeClass("Active");
            $(this).parent().next("div").slideUp("fast");
        }
    });

    $('div.sfInformationcontent:first').show();
    $('div.sfInformationholder:first').addClass("Active");
    $('div.sfInformationheader').on("click", function() {
        $(this).next("div").slideToggle("fast", function() {
            if (!$(this).closest("div.sfInformationholder").hasClass("Active")) {
                $(this).closest("div.sfInformationholder").addClass("Active");
            }
            else if ($(this).closest("div.sfInformationholder").hasClass("Active")) {
                $(this).closest("div.sfInformationholder").removeClass("Active");
            }
            $("div.sfInformationholder").not($(this).closest("div.sfInformationholder")).removeClass("Active");
        });
        $('div.sfInformationcontent').not($(this).next("div")).slideUp("fast");
    });
    var link = document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = '<%=templateFavicon %>';
    document.getElementsByTagName('head')[0].appendChild(link);
});
function ScrollToTop() {
    $(document).scrollTop(0);
}