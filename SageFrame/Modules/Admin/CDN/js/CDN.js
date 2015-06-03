(function($) {
    var totalField = 0;
    $.SageCDN = function(p) {
        p = $.extend
        ({
            UserModuleID: 1
        }, p);
        var CDN = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: SageFrameAppPath + '/Modules/Admin/CDN/Services/CDNWebService.asmx/',
                method: "",
                ModulePath: '',
                PortalID: parseInt(SageFramePortalID),
                UserModuleID: p.UserModuleID,
                UserName: SageFrameUserName,
                ajaxCallMode: 0,
                url: "",
                method: "",
                Mode: ""
            },
            ajaxSuccess: function(data) {
                switch (CDN.config.ajaxCallMode) {
                    case 1:
                        break;
                    case 2:
                        CDN.BindCDNLinks(data);
                        break;
                    case 3:
                        CDN.BindCDNLinksForEdit(data);
                        break;
                    case 4:

                        break;
                    case 5:
                        break;
                }
            },
            ajaxFailure: function() {
                alert("you got error");
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: this.config.type,
                    async: this.config.async,
                    contentType: this.config.contentType,
                    cache: this.config.cache,
                    url: this.config.url,
                    data: this.config.data,
                    dataType: this.config.dataType,
                    success: this.ajaxSuccess,
                    error: this.ajaxFailure
                });
            },
            init: function() {
                CDN.BindEvents();
            },
            BindEvents: function() {
                CDN.GetCDNLinks();
                var v = $("#form1").validate({
                    ignore: ':hidden',
                    rules: {
                        'input[type=text]': { required: true }
                    },
                    messages: {
                        'input[type=text]': "Field should not be blank."
                    }
                });
                //End Validate
                $('#ulUrlList').on('click', '.up', function() {
                    var url = $(this).parents('li');
                    var upperbox = url.prev();
                    url.insertBefore(upperbox);
                    CDN.UpdateOrder();
                    CDN.GetCDNLinks();
                });
                $('#ulUrlList').on('click', '.down', function() {
                    var url = $(this).parents('li');
                    var lowerurl = url.next();
                    url.insertAfter(lowerurl);
                    CDN.UpdateOrder();
                    CDN.GetCDNLinks();
                });

                $('#divEdit').on('click', '#spnEdit', function() {
                    CDN.GetCDNLinksForEdit();
                    $('#divSave').show();
                    $('#spnEdit').hide();
                    $('#spnSave').show();
                    $('#divViewList').hide()

                });
                $('#ulUrlList').on('click', '.sfDelete', function() {
                    var urlID = $(this).parents('li').find('input[type=hidden]').val();
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this Link ?");
                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Confirm": function() {
                                CDN.DeleteURL(urlID);
                                CDN.GetCDNLinks();
                                $(this).dialog("close");
                            },
                            "Cancel": function() {
                                $(this).dialog("close");
                            }
                        }
                    });

                });
                $('#divAddJS').on('click', '#spnAddJS', function() {
                    var totalField = $('#divAddjsURL input:text').length + 1;
                    var html = '';
                    html += '<input type="text" class="sfInput"  id="jsURL_' + totalField + '"  /> ';
                    $('#divAddjsURL').append(html);
                    $('#divSave').show();
                    $('#spnSave').show();
                });
                $('#divAddCSS').on('click', '#spnAddCSS', function() {
                    var totalField = $('#divAddcssURL input:text').length + 1;
                    var html = '';
                    html += '<input type="text" class="sfInput"  id="cssURL_' + totalField + '"  /> ';
                    $('#divAddcssURL').append(html);
                    $('#divSave').show();
                    $('#spnSave').show();
                });
                $('#divSave').on('click', '#spnSave', function() {
                    $('#spnSave').hide();
                    $('#spnEdit').show();
                    $('#divViewList').show();
                    if (v.form()) {
                        var jsParam = [];
                        var cssParam = [];
                        $('#divAddjsURL').find('input[type=text]').each(function(index, item) {
                            var jskey = $(this).val();

                            if ($(this).is("[val]")) {
                                var attr = $(this).attr('val');
                                CDN.config.Mode = attr;
                            }
                            else {
                                CDN.config.Mode = "A";
                            }
                            if (jskey != '') {
                                var cssInfo = {
                                    "URL": jskey,
                                    "IsJS": true,
                                    "URLOrder": index,
                                    "PortalID": CDN.config.PortalID,
                                    "Mode": CDN.config.Mode
                                }
                                jsParam.push(cssInfo);
                            }
                        });

                        $('#divAddcssURL').find('input[type=text]').each(function(index, item) {
                            var csskey = $(this).val();
                            if ($(this).is("[val]")) {
                                var attr = $(this).attr('val');
                                CDN.config.Mode = attr;
                            }
                            else {
                                CDN.config.Mode = "A";
                            }
                            if (csskey != '') {
                                var jsInfo = {
                                    "URL": csskey,
                                    "IsJS": false,
                                    "URLOrder": index,
                                    "PortalID": CDN.config.PortalID,
                                    "Mode": CDN.config.Mode
                                }
                                cssParam.push(jsInfo);

                            }
                        });

                        if ($('#divAddjsURL').find('input[type=text]').length > 0) {
                            CDN.SaveLinks(jsParam);
                        }
                        if ($('#divAddcssURL').find('input[type=text]').length) {
                            CDN.SaveLinks(cssParam);
                        }
                        CDN.GetCDNLinks();
                        $('#divAddjsURL').html('');
                        $('#divAddcssURL').html('');
                    }
                });
            },
            DeleteURL: function(urlID) {
                CDN.config.method = "DeleteURL";
                CDN.config.url = CDN.config.baseURL + CDN.config.method;
                CDN.config.data = JSON2.stringify({
                    UrlID: urlID,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                CDN.config.ajaxCallMode = 5;
                CDN.ajaxCall(CDN.config);
            },
            SetCDN: function(EnableCDN) {
                CDN.config.method = "SetCDN";
                CDN.config.url = CDN.config.baseURL + CDN.config.method;
                CDN.config.data = JSON2.stringify({
                    EnableCDN: EnableCDN,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                CDN.config.ajaxCallMode = 5;
                CDN.ajaxCall(CDN.config);
            },
            GetLinkOrder: function() {
                var param = [];
                $('.sfCDNLinks div').find('input[type=hidden]').each(function(index, item) {
                    var key = $(this).val();
                    if (key != '') {
                        var chdINfo = {
                            "URLOrder": index,
                            "URLID": key,
                            "PortalID": CDN.config.PortalID

                        }
                        param.push(chdINfo);
                    }
                });
                return JSON2.stringify({ objOrder: param,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
            },
            UpdateOrder: function() {
                this.config.method = "SaveOrder";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = CDN.GetLinkOrder();
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            },
            SaveLinks: function(param) {
                CDN.config.method = "SaveLinks";
                CDN.config.url = CDN.config.baseURL + CDN.config.method;
                CDN.config.data = JSON2.stringify({
                    CDNInfo: param,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                CDN.config.ajaxCallMode = 1;
                CDN.ajaxCall(CDN.config);
            },
            GetCDNLinks: function() {
                CDN.config.method = "GetCDNLinks";
                CDN.config.url = CDN.config.baseURL + CDN.config.method;
                CDN.config.data = JSON2.stringify({
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                CDN.config.ajaxCallMode = 2;
                CDN.ajaxCall(CDN.config);
            },
            GetCDNLinksForEdit: function() {
                CDN.config.method = "GetCDNLinks";
                CDN.config.url = CDN.config.baseURL + CDN.config.method;
                CDN.config.data = JSON2.stringify({
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                CDN.config.ajaxCallMode = 3;
                CDN.ajaxCall(CDN.config);
            },
            BindCDNLinksForEdit: function(data) {
                $('#divAddjsURL').html('');
                $('#divAddcssURL').html('');
                $('#divAddjsURL').show();
                $('#divAddJS').show();
                var links = data.d;
                var totalField = data.d.length
                var html = '';
                $.each(links, function(index, item) {
                    if (item.IsJS) {
                        $('#divAddjsURL').append('<input type="text" class="sfInput"  id="jsURL_' + index + '" val="' + item.URLID + '"  /> ');
                        $('#jsURL_' + index).val(item.URL);
                    }
                    else {
                        $('#divAddcssURL').append('<input type="text" class="sfInput"  id="cssURL_' + index + '" val="' + item.URLID + '"  /> ');
                        $('#cssURL_' + index).val(item.URL);
                    }
                });

            },
            BindCDNLinks: function(data) {
                var links = data.d;
                var html = '';
                var length = data.d.length;
                var arrow = '';
                var icon = '';
                $.each(links, function(index, item) {

                    if (item != "") {
                        if (item.IsJS)
                        { icon = '<i class="icon-js"></i>'; }
                        else
                        { icon = '<i class="icon-css"></i>'; }

                        if (index == 0)
                        { arrow = '<span class="sfOrder"><i class="down icon-arrow-s"></i></span>'; }
                        else if (index === length - 1)
                        { arrow = '<span class="sfOrder"><i class="up icon-arrow-n"></i></span>'; }
                        else
                        { arrow = '<span class="sfOrder"><i class="up icon-arrow-n"></i><i class="down icon-arrow-s"> </i></span>'; }

                        html += '<li class="sfCDNLinks clearfix"><div class="sfLinks"><div class="sfBlock">';
                        html += '<input type="hidden" class="sfhdn" id="hdnURLID_' + item.URLID + '" value="' + item.URLID + '" />';
                        html += '<span class="sfUrl">' + icon + item.URL + '</span>';
                        //                        html += '<span class="sfOrder"><i class="up icon-arrow-n"></i><i class="down icon-arrow-s"> </i></span>'
                        html += arrow;
                        html += '<span class="sfDelete icon-delete"></span>';
                        html += '</div></li>';
                    }
                });
                $('#ulUrlList').html(html);
            }
        };
        CDN.init();
    };
    $.fn.SageFrameCDN = function(p) {
        $.SageCDN(p);
    }
})(jQuery);