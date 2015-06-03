var SageFrame = {};
$(function () {
    SageFrame = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            method: "",
            url: "",
            ajaxCallMode: 0,
            baseURL: SageFrameAppPath + "/Services/SageFrameGlobalWebService.asmx/"
        },
        vars: {
            numberRegex: /^[+-]?\d+(\.\d+)?([eE][+-]?\d+)?$/,
            adminimagepath: SageFrameAppPath + "/Administrator/Templates/Default/images/"
        },
        init: function () {
            $('span.sfPopupclose').bind("click", function () {
                $('div.sfPopup,#fade').fadeOut();
            });
            $('body').append('<div id="ajaxBusy" style="display:none"><img align="absmiddle"  src="' + SageFrameAppPath + '/Administrator/Templates/Default/images/ajax-loader.gif">&nbsp;Working...</div>');
            $('div.sfMessage ').on('click', 'img.delete', function () {
                $('div.sfMessage').slideUp('slow');
            });
            $('.sfMessagewrapper').on("click", ".sfMessage", function () {
                $(this).slideUp();
            });
            $(window).scroll(function () {
                if ($(this).scrollTop() > 0) {
                    $('.sfTopbar').css('position', 'fixed');
                } else {
                    $('.sfTopbar').css('position', 'relative');
                }
            });
        },
        messages: {
            NoModulesAssigned: "No Modules Assigned. To Add Modules Please Go To the the <a href='#'>Module Manager</a> Section OR Click the Manage Modules Link on the top right",
            Presets: "These are the list of presets available. The highlighted presets are the ones already applied to pages. You can delete the preset by simply clicking the delete button."
        },
        utils: {
            getapplicationname: function () {
                return SageFrameAppPath;
            },
            GetImagePath: function (imagepath) {
                var fullpath = SageFrame.utils.getapplicationname() + imagepath;
                return fullpath;
            },
            GetUserName: function () {
                return SageFrameUserName;
            },
            IsNumber: function (str) {
                var isnum = false;
                if (SageFrame.vars.numberRegex.test(str)) {
                    isnum = true;
                }
                return isnum;

            },
            GetFileNameWithoutExtension: function (filename) {
                if (filename.indexOf(".") > -1) {
                    filename = filename.substring(0, filename.indexOf("."));
                }
                return filename;
            },
            GetAdminImage: function (imagename) {
                return (SageFrame.vars.adminimagepath + imagename);
            },
            GetFileNameOnly: function (filepath) {
                var index = filepath.lastIndexOf("/");
                var filename = filepath.substr(index + 1);
                return filename;
            },
            ContainsInvalidChar: function (Name) {
                var characterReg = /^\s*[a-z-_\d\&,\s]+\s*$/i;
                if (!Name.match(characterReg)) {
                    return false;
                }
                else {
                    return true;

                }
            },
            GetPageSEOName: function (pagename) {
                return $.trim(pagename.replace(/-/g, " ")).replace(/ /g, '-');
            }
        },
        messaging: {
            show: function (message, messagetype) {
                if (MsgTemplate == "default") {
                    var styledclass = "";
                    switch (messagetype.toLowerCase()) {
                        case "success":
                            styledclass = "sfMessage sfSuccessmsg sfCurve";
                            break;
                        case "error":
                            styledclass = "sfMessage sfErrormsg sfCurve";
                            break;
                        case "alert":
                            styledclass = "sfMessage sfAlertmsg sfCurve";
                            break;
                        default:
                            styledclass = "sfMessage sfCurve";
                            break;
                    }
                    var messagewrapper = '';
                    messagewrapper += "<div class='" + styledclass + "'>";
                    messagewrapper += "<span  id='spnMessage' runat='server'>" + message + "</span>";
                    $('div.sfMessagewrapper:first').html(messagewrapper).animate(0, function () {
                        $('div.sfMessagewrapper:first').slideDown();
                        $(document).scrollTop(0);
                    });
                }
                else if (MsgTemplate == "custom") {
                    if (messagetype == 'alert')
                        messagetype = 'warning';
                    messages.showMessage(message, messagetype);
                }
            },
            GetLocalizedMessage: function (culturecode, modulename, messagetype) {
                var message = "";
                var param = JSON2.stringify({ CultureCode: culturecode, ModuleName: modulename, MessageType: messagetype });
                $.ajax({
                    type: SageFrame.config.type,
                    contentType: SageFrame.config.contentType,
                    cache: SageFrame.config.cache,
                    url: SageFrame.config.baseURL + "GetLocalizedMessage",
                    data: param,
                    dataType: SageFrame.config.dataType,
                    async: false,
                    success: function (msg) {
                        message = msg.d;
                    }

                });
                return message;

            },
            showdivmessage: function (message) {
                var messagewrapper = '';
                messagewrapper += "<div class='sfNotemessage'>";
                messagewrapper += '<p>' + message + '</p>';
                messagewrapper += '</div>';
                return messagewrapper;
            },
            alert: function (message, element) {
                var messagewrapper = '';
                messagewrapper += "<div class='sfMessage sfAlertmsg'>";
                messagewrapper += '<p>' + message + '</p>';
                messagewrapper += '</div>';
                $(element).hide().html(messagewrapper).animate(0, function () {
                    $(element).slideDown().delay(5000).slideUp();
                });

            }
        },
        tooltip: {
            consts: {
                helpImage: (SageFrameAppPath == "/" ? "" : SageFrameAppPath) + "/Administrator/Templates/Default/images/help.png"
            },
            InitializeToolTips: function () {
                this.GetHelpToolTip("imgToolTip", "#testTooltip", "This is my tooltip");
            },
            GetHelpToolTip: function (id, container, message) {
                $(container).html(SageFrame.tooltip.BuildTooltipImage(id));
                $.toolTipRequest(id, message, "dark");

            },
            GetToolTip: function (id, container, message) {
                $(container).append(SageFrame.tooltip.BuildTooltipImage(id));
                $.toolTipRequest(id, message, "dark");
            },
            BuildTooltipImage: function (id) {
                var image = '<i class="sfTooltip icon-info" id=' + id + '></i>';
                return image;
            },
            GetTextBoxToolTip: function (id, message) {
                $.toolTipRequest(id, message, "dark");
            },
            GetGeneralToolTip: function (id) {
                $.toolTipRequestTitle(id, "dark");
            },
            GetTextBoxToolTipImage: function (id, message) {
                $('#' + id).parent().append(SageFrame.tooltip.BuildTooltipImage('img_' + id));
                $.toolTipRequest('img_' + id, message, "dark");
            },
            GetTextBoxToolTipImage_Style: function (id, message, style) {
                $('#' + id).parent().append(SageFrame.tooltip.BuildTooltipImage('img_' + id));
                $.toolTipRequest('img_' + id, message, style);
            }

        },
        popup: {
            show: function (popupid, headertext) {
                var options = {
                    modal: true,
                    title: headertext,
                    minHeight: 125,
                    minWidth: 520,
                    maxWidth: 1000,
                    maxHeight: 1000,
                    dialogClass: "sfFormwrapper",
                    resizable: false
                };
                dlg = $('#' + popupid).dialog(options);
                dlg.parent().appendTo($('form:first'));
            },
            show_wide: function (popupid, headertext) {
                var options = {
                    modal: true,
                    title: headertext,
                    minHeight: 125,
                    minWidth: 720,
                    maxWidth: 1000,
                    maxHeight: 1000,
                    dialogClass: "sfFormwrapper",
                    resizable: false
                };
                var dlg = $('#' + popupid).dialog(options);
                dlg.parent().appendTo($('form:first'));
            },
            close: function (popupid) {
                $('#' + popupid).dialog("close");
            },
            confirm: function (message) {
                var status = false;
                $("#dialog").dialog({
                    resizable: false,
                    height: 140,
                    modal: true,
                    buttons: {
                        "Delete all items": function () {
                            return true;
                            $(this).dialog("close");
                        },
                        Cancel: function () {
                            return false;
                            $(this).dialog("close");
                        }
                    }
                });
            }
        }
    };
    SageFrame.init();

    // $(".confirm").easyconfirm();
    ResetSidebarHeight();
});
//Ajax activity indicator bound to ajax start/stop document events
$('#ajaxBusy').show();
setTimeout("startAjaxBusy()", 1000);
$(document).ajaxStart(function () {
    $('#ajaxBusy').show();
}).ajaxStop(function () {
    $('#ajaxBusy').hide();
});
$(document).ajaxComplete(function () {
    $('#ajaxBusy').hide();
});
function startAjaxBusy(){
    $('#ajaxBusy').hide();
}


(function ($) {
    $.toolTipRequest = function (id, pdata, theme) {
        $('#' + id).qtip({
            content: {
                text: pdata
            },
            position: {
                corner: {
                    target: 'rightBottom',
                    tooltip: 'topLeft'
                }
            },
            style: {
                name: theme
            }
        });
    }
    $.toolTipRequestTitle = function (id, theme) {
        $('#' + id).qtip({
            content: {
                text: $('#' + id).attr("title")
            },
            position: {
                corner: {
                    target: 'leftBottom',
                    tooltip: 'topLeft'
                }
            },
            style: {
                name: theme
            }
        });
    }
})(jQuery);





function ResetSidebarHeight() {
    var scrollpos = $(document).scrollTop();

    var masterheight = $("div.sfMaincontent").height();

    var docheight = $(window).height();
    docheight = docheight - $('div.sfTopwrapper').height() + CalculateAdjustmentHeight();
    var incr = 0;
    if (masterheight > docheight) {
        incr = masterheight - docheight;
    }
    docheight = docheight + incr + 30 + "px";
    $('div.sfSidebar').css("height", docheight);

}


$(document).scroll(function () {
    ResetSidebarHeight();
});
$(document).click(function () {
    ResetSidebarHeight();
});

function CalculateAdjustmentHeight() {
    var calc_height = 10;
    var screen_res = screen.height;
    calc_height = screen_res * 0.28;
    return calc_height;

}

function InitModuleFloat(leftOffset) {

    if (location.href.toLowerCase().indexOf("page-modules") > -1) {

        var topOffset = $('#divDroppable').offset().top;
        var adj = $('#sfOuterwrapper').height() + ($('#sfOuterwrapper').offset().top) - ($('#divFloat').height());
        $("#divFloat").makeFloat({ x: leftOffset, y: 0, speed: "fast", adjustment: adj });
    }

}


$(function () {

    $('div.sfSlideup').parent("div.sfModuleinfo").delay(3500).slideUp("slow");
    $('div.sfFadeout').parent("div.sfModuleinfo").delay(3500).fadeOut("slow");

    $('div.sfModuleinfo div.sfLinks a.sfClose').on("click", function () {
        $(this).parents("div.sfModuleinfo").slideUp("slow");
    });
    $('div.sfModuleinfo div.sfLinks a.sfNclose').bind("click", function () {
        var _ModuleID = $(this).prop("id").replace("close_", "");
        var self = $(this);
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            url: SageFrameAppPath + "/Modules/Admin/ModuleMessage/services/ModuleMessageWebService.asmx/UpdateMessage",
            data: JSON2.stringify({ ModuleID: _ModuleID, IsActive: false }),
            dataType: 'json',
            success: function (msg) {
                $(self).parents("div.sfModuleinfo").slideUp("slow");
                location.reload();
            }
        });
    });

    $('.tooltip').tooltip({
        showURL: false,
        fade: 250,
        track: true
    });
});

$.maxZIndex = $.fn.maxZIndex = function (opt) {
    /// <summary>
    /// Returns the max zOrder in the document (no parameter)
    /// Sets max zOrder by passing a non-zero number
    /// which gets added to the highest zOrder.
    /// </summary>    
    /// <param name="opt" type="object">
    /// inc: increment value, 
    /// group: selector for zIndex elements to find max for
    /// </param>
    /// <returns type="jQuery" />
    var def = { inc: 10, group: "*" };
    $.extend(def, opt);
    var zmax = 0;
    $(def.group).each(function () {
        var cur = parseInt($(this).css('z-index'));
        zmax = cur > zmax ? cur : zmax;
    });
    if (!this.jquery)
        return zmax;

    return this.each(function () {
        zmax += def.inc;
        $(this).css("z-index", zmax);
    });
}

