(function($) {
    $.createLangSwitch = function(p) {
        p = $.extend({
            PortalID: 1,
            UserModuleID: 1,
            CultureCode:"en-US",
            LangSwitchContainerID: "divNav1",
            SwitchType: '',
            DropDownType: ''
        }, p);
        var LanguageSwitch = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: SageFrameAppPath + "/Modules/LanguageSwitcher/js/WebMethods.aspx/",
                method: "",
                url: "",
                ajaxCallMode: 0,
                arr: [],
                arrPages: [],
                CultureCode: "",
                LangSwitchContainerID: p.LangSwitchContainerID,
                PortalID: p.PortalID,
                UserModuleID: p.UserModuleID,
                LanguageSwitchSettings: ""
            },
            init: function() {
                //this.GetCultureInfo();
               // LanguageSwitch.GetLanguageSettings();
                LanguageSwitch.BindFlagClickEvent();
                //this.InitializeCarousel();               
              
            },
            BindEvents: function() {
                $(LanguageSwitch.config.ContainerClientID + " ul li").on('click', function () {                
                    $(LanguageSwitch.config.ContainerClientID + " ul li").removeClass("cssClassActive");
                    $(this).addClass("cssClassActive");
                });
            },
            ajaxFailure: function() {
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: LanguageSwitch.config.type,
                    contentType: LanguageSwitch.config.contentType,
                    cache: LanguageSwitch.config.cache,
                    url: LanguageSwitch.config.url,
                    data: LanguageSwitch.config.data,
                    dataType: LanguageSwitch.config.dataType,
                    success: LanguageSwitch.ajaxSuccess,
                    complete: LanguageSwitch.ajaxComplete,
                    error: LanguageSwitch.ajaxFailure,
                    async: false
                });
            },
            GetPages: function() {
                this.config.method = "GetMenuFront";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: parseInt(LanguageSwitch.config.PortalID), UserName: LanguageSwitch.config.UserName });
                this.ajaxSuccess = LanguageSwitch.BindFlags;
                this.ajaxCall(this.config);
            },
            GetCultureInfo: function() {
                this.config.method = "GetCurrentCultureInfo";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = '{}';
                this.ajaxSuccess = function(data) {
                LanguageSwitch.config.CultureCode = data.d;
                    LanguageSwitch.GetLanguageSettings();
                };
                this.ajaxCall(this.config);
            },
            GetLanguageSettings: function() {
                LanguageSwitch.config.method = "GetLanguageSettings";
                LanguageSwitch.config.url = this.config.baseURL + this.config.method;
                LanguageSwitch.config.data = JSON2.stringify({ PortalID: parseInt(LanguageSwitch.config.PortalID), UserModuleID: parseInt(LanguageSwitch.config.UserModuleID) });
                LanguageSwitch.ajaxSuccess = LanguageSwitch.LoadFlags;
                LanguageSwitch.ajaxCall(this.config);
            },
            LoadFlags: function(data) {
                LanguageSwitch.config.method = "GetAvailableLanguages";
                LanguageSwitch.config.url = LanguageSwitch.config.baseURL + LanguageSwitch.config.method;
                LanguageSwitch.config.data = JSON2.stringify({ baseURL: SageFrameAppPath, PortalID: parseInt(LanguageSwitch.config.PortalID) });
                LanguageSwitch.config.ajaxCallMode = null;
                $.ajax({
                    type: LanguageSwitch.config.type,
                    async: false,
                    contentType: LanguageSwitch.config.contentType,
                    cache: LanguageSwitch.config.cache,
                    url: LanguageSwitch.config.url,
                    data: LanguageSwitch.config.data,
                    dataType: LanguageSwitch.config.dataType,
                    success: function(msg) {
                        if (msg.d.length > 0) {
                            LanguageSwitch.BindFlags(msg, data.d);
                        }
                    },
                    error: LanguageSwitch.ajaxFailure
                });
            },
            BindFlags: function(msg, settings) {
                languages = msg.d;
                var switchSettings = settings;
                if (switchSettings.length > 0) {
                    var _SwitchType = "";
                    var _ListTypeFlags = "false";
                    var _ListTypeName = "false";
                    var _ListTypeBoth = "false";
                    var _ListAlign = "H";
                    var _EnableCarousel = "false";
                    var _DropDownType = "Flag";
                    $.each(switchSettings, function(index, item) {
                        if (item.Key == "ListTypeBoth") {
                            _ListTypeBoth = item.Value;
                        }
                        if (item.Key == "ListTypeFlag") {
                            _ListTypeFlags = item.Value;
                        }
                        if (item.Key == "ListTypeName") {
                            _ListTypeName = item.Value;
                        }
                        if (item.Key == "ListAlign") {
                            _ListAlign = item.Value;
                        }
                        if (item.Key == "EnableCarousel") {
                            _EnableCarousel = item.Value;
                        }
                        if (item.Key == "SwitchType") {
                            _SwitchType = item.Value;
                        }
                        if (item.Key == "DropDownType") {
                            _DropDownType = item.Value;
                        }
                    });
                    var html = "";
                    var data = msg.d;
                    if (_SwitchType.toLowerCase() == "list") {
                        html = _ListAlign.toLowerCase() == "h" ? '<ul id="imgFlagButton_' + LanguageSwitch.config.UserModuleID + '" class="defaultButtonClass">' : '<ul id="imgFlagButton_' + LanguageSwitch.config.UserModuleID + '">';
                        var carousel = "";
                        $.each(data, function(index, item) {
                            if (_EnableCarousel.toLowerCase() == "false") {
                                if (_ListTypeBoth.toLowerCase() == "true") {
                                    if (_ListAlign.toLowerCase() == "h") {

                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                            html += '<li class="cssClassFlagButtonHor cssClassSelectedFlag"  code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /><span>' + item.LanguageName + '</span></li>';
                                        }
                                        else {
                                            html += '<li class="cssClassFlagButtonHor"  code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /><span>' + item.LanguageName + '</span></li>';
                                        }
                                    }
                                    else {
                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {

                                            html += '<li class="cssClassFlagButtonVer cssClassSelectedFlag"  code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /><span>' + item.LanguageName + '</span></li>';
                                        }
                                        else {
                                            html += '<li class="cssClassFlagButtonVer"  code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /><span>' + item.LanguageName + '</span></li>';
                                        }
                                    }
                                }
                                else if (_ListTypeName.toLowerCase() == "true") {
                                    if (_ListAlign.toLowerCase() == "h") {

                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                            html += '<li class="cssClassLanguageName"  code="' + item.LanguageCode + '"><span>' + item.LanguageN + "<b>(" + item.NativeName + ")</b>" + '</span></li>';
                                        }
                                        else {

                                            html += '<li class="cssClassLanguageName" code="' + item.LanguageCode + '" ><span>' + item.LanguageN + "<b>(" + item.NativeName + ")</b>" + '</span></li>';
                                        }
                                    }
                                    else {
                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                            html += '<li class="cssClassSelectedFlag cssClassLanguageNameVer"  code="' + item.LanguageCode + '"><span>' + item.LanguageN + "<b>(" + item.NativeName + ")</b>" + '</span></li>';
                                        }
                                        else {
                                            html += '<li class="cssClassLanguageNameVer" code="' + item.LanguageCode + '" ><span>' + item.LanguageN + "<b>(" + item.NativeName + ")</b>" + '</span></li>';
                                        }
                                    }
                                }
                                else if (_ListTypeFlags.toLowerCase() == "true") {
                                    if (_ListAlign.toLowerCase() == "h") {

                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                            html += '<li class="cssClassSelectedFlag cssClassLanguageFlag flag" title="' + item.LanguageName + '" code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /></li>';
                                        }
                                        else {
                                            html += '<li class="cssClassLanguageFlag flag"  code="' + item.LanguageCode + '" title="' + item.LanguageName + '"><img src=' + item.FlagPath + ' /></li>';
                                        }
                                    }
                                    else {
                                        if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                            html += '<li class="cssClassSelectedFlag cssClassLanguageFlagVer flag" title="' + item.LanguageName + '" code="' + item.LanguageCode + '"><img src=' + item.FlagPath + ' /></li>';
                                        }
                                        else {
                                            html += '<li class="cssClassLanguageFlagVer flag"  code="' + item.LanguageCode + '" title="' + item.LanguageName + '"><img src=' + item.FlagPath + ' /></li>';
                                        }
                                    }
                                }
                                $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_container_' + LanguageSwitch.config.UserModuleID).hide();
                            }
                            else if (_EnableCarousel.toLowerCase() == "true") {
                                $(LanguageSwitch.config.LangSwitchContainerID + ' #divFlagButton_' + LanguageSwitch.config.UserModuleID + '').hide();
                                if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                    carousel += '<li class="cssClassSelectedFlag" title="' + item.LanguageName + '"><a href="#" code=' + item.LanguageCode + '><img src=' + item.FlagPath + ' /></a></li>';
                                    // carousel += '<li class="cssClassSelectedFlag" style="diaplay:none" title="' + item.LanguageName + '"><a href="#" code=' + item.LanguageCode + '><img src=' + item.FlagPath + ' /></a></li>';

                                }
                                else {
                                    carousel += '<li title="' + item.LanguageName + '" ><a href="#" code=' + item.LanguageCode + '><img src=' + item.FlagPath + ' /></a></li>';
                                }
                            }
                        });
                        html += '</ul>';
                        $(LanguageSwitch.config.LangSwitchContainerID + ' #divFlagButton_' + LanguageSwitch.config.UserModuleID + '').append(html);
                        if (_EnableCarousel.toLowerCase() == "true") {

                            $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul_' + LanguageSwitch.config.UserModuleID + '').append(carousel);
                            $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_container_' + LanguageSwitch.config.UserModuleID + '').show();
                        }
                    }
                    else if (_SwitchType.toLowerCase() == "dropdown") {
                        var ddl = "";
                        if (_DropDownType.toLowerCase() == "normal") {
                            ddl = '<select id="ddlLocales_' + LanguageSwitch.config.UserModuleID + '">';
                            $.each(data, function(index, item) {
                                if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                    ddl += '<option selected="selected">' + item.LanguageCode + '</option>';
                                }
                                else {
                                    ddl += '<option>' + item.LanguageCode + '</option>';
                                }
                            });
                            ddl += '</select>';
                            $(LanguageSwitch.config.LangSwitchContainerID + ' #divPlainDDL_' + LanguageSwitch.config.UserModuleID + '').append(ddl);
                        }
                        else if (_DropDownType.toLowerCase() == "flag") {
                            ddl = '<select id="ddlFlaggedLocales_' + LanguageSwitch.config.UserModuleID + '">';
                            $.each(data, function(index, item) {
                                if (item.LanguageCode == LanguageSwitch.config.CultureCode) {
                                    ddl += '<option  title="' + item.FlagPath + '" selected="selected"  value="' + item.LanguageCode + '" >' + item.LanguageCode + '</option>';
                                }
                                else {
                                    ddl += '<option title="' + item.FlagPath + '"  value=' + item.LanguageCode + ' >' + item.LanguageCode + '</option>';
                                }
                            });
                            ddl += '</select>';
                            $(LanguageSwitch.config.LangSwitchContainerID + ' #divFlagDDL_' + LanguageSwitch.config.UserModuleID + '').append(ddl);
                            $('#ddlFlaggedLocales_' + LanguageSwitch.config.UserModuleID + '').msDropDown().data("dd");
                            $('div.dd').removeAttr("style");
                        }
                        $(LanguageSwitch.config.LangSwitchContainerID + ' #imgFlagButton_' + LanguageSwitch.config.UserModuleID + '').hide();
                        $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_container_' + LanguageSwitch.config.UserModuleID + '').hide();
                    }
                }
                LanguageSwitch.BindFlagClickEvent();
            },
            BindFlagClickEvent: function() {
                $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').bind("click", function() {
                    var code = $(this).find("a").attr("code");
                    LanguageSwitch.SetCulture(code);
                });
                $(LanguageSwitch.config.LangSwitchContainerID + ' #divFlagButton_' + LanguageSwitch.config.UserModuleID + ' li').bind("click", function() {
                    var code = $(this).attr("code");
                    LanguageSwitch.SetCulture(code);
                });
                $(LanguageSwitch.config.LangSwitchContainerID + ' #ddlFlaggedLocales_' + LanguageSwitch.config.UserModuleID + '').change(function () {                   
                    var code = $('#ddlFlaggedLocales_' + LanguageSwitch.config.UserModuleID + ' option:selected').text();
                    LanguageSwitch.SetCulture(code);
                });
                $(LanguageSwitch.config.LangSwitchContainerID + ' #ddlLocales_' + LanguageSwitch.config.UserModuleID + '').bind("change", function() {
                    var code = $(LanguageSwitch.config.LangSwitchContainerID + ' #ddlLocales_' + LanguageSwitch.config.UserModuleID + ' option:selected').text();
                    LanguageSwitch.SetCulture(code);
                });
                LanguageSwitch.InitializeToolTip();               
                if (p.SwitchType.toLowerCase() == 'dropdown') {
                    if (p.DropDownType.toLowerCase() == 'flag') {
                        $('#ddlFlaggedLocales_' + LanguageSwitch.config.UserModuleID + '').msDropDown().data("dd");
                        $('div.dd').removeAttr("style");
                    }
                    $(LanguageSwitch.config.LangSwitchContainerID + ' #imgFlagButton_' + LanguageSwitch.config.UserModuleID + '').hide();
                    $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_container_' + LanguageSwitch.config.UserModuleID + '').hide();
                }
            },
            InitializeCarousel: function() {
                if ($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').length >= 3) {
                    $(".cssClassCarousel").css({ 'height': '10px', 'width': '72px' });
                }
                //move the last list item before the first item. The purpose of this is if the user clicks to slide left he will be able to see the last item.
                $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:first').before($(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul li:last'));
                if ($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').length > 1) {
                    this.LoadImages();
                    this.BindEvents();
                    //when user clicks the image for sliding right        
                    $('#right_scroll_' + LanguageSwitch.config.UserModuleID + ' img').click(function() {
                        //get the width of the items ( i like making the jquery part dynamic, so if you change the width in the css you won't have o change it here too ) '
                        var item_width = $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').outerWidth() + 10;
                        //calculae the new left indent of the unordered list
                        var left_indent = parseInt($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + '').css('left')) - item_width;
                        //make the sliding effect using jquery's anumate function '
                        $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ':not(:animated)').animate({ 'left': left_indent }, 500, function() {
                            //get the first list item and put it after the last list item (that's how the infinite effects is made)                        
                            $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:last').after($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:first'));
                            //and get the left indent to the default -210px
                            $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + '').css({ 'left': '0px' });
                        });
                    });
                    //when user clicks the image for sliding left
                    $('#left_scroll_' + LanguageSwitch.config.UserModuleID + ' img').click(function() {
                        var item_width = $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').outerWidth() + 10;
                        /* same as for sliding right except that it's current left indent + the item width (for the sliding right it's - item_width) */
                        var left_indent = parseInt($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + '').css('left')) + item_width;
                        $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ':not(:animated)').animate({ 'left': left_indent }, 500, function() {
                            /* when sliding to left we are moving the last item before the first list item */
                            $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:first').before($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:last'));
                            /* and again, when we make that change we are setting the left indent of our unordered list to the default -210px */
                            $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul_' + LanguageSwitch.config.UserModuleID + '').css({ 'left': '0px' });
                        });
                    });
                    //SHOWING FIRST SELECTED CULTURE
                    for (var i = 0; i < $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li.cssClassSelectedFlag').index(); i++) {
                        $('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:first').before($('#carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li:last'));
                           
                    }
                }
                
            },
            InitializeToolTip: function() {
                $(LanguageSwitch.config.LangSwitchContainerID + ' #imgFlagButton_' + LanguageSwitch.config.UserModuleID + ' li').tooltip({ position: "bottom center" });
                $(LanguageSwitch.config.LangSwitchContainerID + ' #carousel_ul_' + LanguageSwitch.config.UserModuleID + ' li').tooltip({ position: "bottom center" });
            },
            LoadImages: function() {
                $(LanguageSwitch.config.LangSwitchContainerID + ' img.imgLeftScroll').attr("src", LanguageSwitchFilePath + "images/left.png");
                $(LanguageSwitch.config.LangSwitchContainerID + ' img.imgRightScroll').attr("src", LanguageSwitchFilePath + "images/right.png");
            },
            SetCulture: function(code) {
                this.config.method = "SetCultureInfo";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ CultureCode: code });
                this.ajaxCall(this.config);
                this.ajaxComplete = location.reload(true);
            }
        };
        LanguageSwitch.init();
    };
    $.fn.langswitcher = function(p) {
        $.createLangSwitch(p);
    };
})(jQuery);