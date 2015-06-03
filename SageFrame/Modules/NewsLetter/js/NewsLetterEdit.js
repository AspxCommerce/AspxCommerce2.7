$(function() {
    var theOptions = {
        question: "Are You Sure ?",
        yesAnswer: "Yes",
        cancelAnswer: "Cancel"
    };
    var unsubscribePage = '';
    var NewsLetterEdit = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: { data: '' },
            dataType: 'json',
            baseURL: NewsLetterPath + 'Services/NewsLetterWebService.asmx/',
            method: "",
            ModulePath: '',
            PortalID: PortalID,
            UserModuleID: UserModuleID,
            UserName: UserName,
            ajaxCallMode: 0,
            url: "",
            method: "",
            CultureName: CultureName,
            Current: 1,
            PageSize: 15,
            resolvedURL: resolvedURL,
            PassURL: PassURL,
            PageExtension: PageExtension

        },
        ajaxSuccess: function(data) {
            switch (NewsLetterEdit.config.ajaxCallMode) {
                case 0:
                    NewsLetterEdit.BindMessageTemplateType(data);
                    break;
                case 1:
                    NewsLetterEdit.BindMessageToken(data);
                    break;
                case 2:
                    NewsLetterEdit.BindMessageBodyToken(data);
                    break;
                case 3:
                    NewsLetterEdit.SendEmailToSubscriber(data);
                    break;
                case 4:
                    NewsLetterEdit.BindSubscriberEmailList(data);
                    break;
                case 5:
                    NewsLetterEdit.BindMessageByTemplateTypeID(data);
                    break;

            }
        },
        ajaxFailure: function() {
            //handle error
            //alert("you got error");
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
            NewsLetterEdit.GetSetting();
            NewsLetterEdit.LoadEditor();
            NewsLetterEdit.LoadMessageTemplateType();
            //  NewsLetterEdit.GetMessageTemplateList(NewsLetterEdit.config.Current, NewsLetterEdit.config.PageSize);
            $("#Email").show();
            $("#PhoneMessage").hide();
            var TemplateTypeID = $("#ddlMessageTemlate").val();
            NewsLetterEdit.GetMessageTemplateByTypeID(TemplateTypeID);
        },
        LoadEditor: function() {
            delete CKEDITOR.instances['txtBodyMsg'];
            $('#txtBodyMsg').ckeditor();
        },
        LoadMessageTemplateType: function() {
            NewsLetterEdit.config.method = "LoadMessageTemplateType";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({
                PortalID: NewsLetterEdit.config.PortalID,
                UserName: NewsLetterEdit.config.UserName,
                CultureName: NewsLetterEdit.config.CultureName,
                UserModuleID: NewsLetterEdit.config.UserModuleID,
                secureToken: SageFrameSecureToken
            });
            NewsLetterEdit.config.ajaxCallMode = 0;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        BindMessageTemplateType: function(data) {
            var htmldata = "";
            $.each(data.d, function(index, value) {
                htmldata += '<option value="' + value.MessageTemplateTypeID + '">' + value.CultureName + '</option>';

            });
            $("#ddlMessageTemlate").html(htmldata);
        },
        GetMessageTokenByTemplate: function() {
            NewsLetterEdit.config.method = "GetMessageTokenByTemplate";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({
                MessageTemplateTypeID: $("#ddlMessageTemlate").val(),
                PortalID: NewsLetterEdit.config.PortalID,
                UserModuleID: NewsLetterEdit.config.UserModuleID,
                UserName: NewsLetterEdit.config.UserName,
                secureToken: SageFrameSecureToken
            });
            NewsLetterEdit.config.ajaxCallMode = 1;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        GetMessageBodyTokenByTemplate: function() {
            NewsLetterEdit.config.method = "GetMessageTokenByTemplate";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({ MessageTemplateTypeID: $("#ddlMessageTemlate").val(), PortalID: NewsLetterEdit.config.PortalID });
            NewsLetterEdit.config.ajaxCallMode = 2;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        BindMessageToken: function(data) {
            var htmldata = "";
            $.each(data.d, function(index, value) {
                htmldata += '<option value="' + value.MessageTokenID + '">' + value.MessageTokenKey + '</option>';
            });
            $("#lstMessageToken").html(htmldata);
        },
        BindMessageBodyToken: function(data) {
            var htmldata = "";
            $.each(data.d, function(index, value) {
                htmldata += '<option value="' + value.MessageTokenID + '">' + value.MessageTokenKey + '</option>';
            });
            $("#lstMesageBodyToken").html(htmldata);
        },
        SendEmailForSubscriber: function() {
            var bodymsg = '';
            bodymsg += $("#txtBodyMsg").val();
            bodymsg += '</br>';
            bodymsg += ("<a href='" + NewsLetterEdit.config.PassURL + "/" + unsubscribePage + "' target='_blank' style='color:#ff0000;'>Click here to unsubscribe</a>");
            NewsLetterEdit.config.method = "SendEmailForSubscriber";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({ subscriberList: $("#txtEmailList").val(), Subject: $("#txtSubject").val(), BodyMsg: bodymsg, UserModuleID: NewsLetterEdit.config.UserModuleID, PortalID: NewsLetterEdit.config.PortalID });
            NewsLetterEdit.config.ajaxCallMode = 3;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        SendEmailToSubscriber: function(data) {
            NewsLetterEdit.ClearEmailForm();
            SageFrame.messaging.show("Send SuccessFully", "Success");

        },
        GetSetting: function () {
            var param = JSON2.stringify({
                UserModuleID: NewsLetterEdit.config.UserModuleID,
                PortalID: NewsLetterEdit.config.PortalID,
                UserName: SageFrameUserName,
                secureToken: SageFrameSecureToken
            });
            $.ajax({
                type: "POST",
                url: NewsLetterEdit.config.baseURL + "GetNLSetting",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    $.each(data.d, function(index, item) {
                        if (item.IsMobileSubscription == "false") {
                            $("#aMobile").hide();
                        }
                        unsubscribePage = item.UnSubscribePageName + NewsLetterEdit.config.PageExtension;
                    });
                },
                error: function() {
                    alert('error');
                }
            });
        },
        ClearEmailForm: function() {
            $("#txtEmailList").val('');
            $("#txtSubject").val('');
            $("#txtBodyMsg").val('');
        },
        GetSubscriberEmailList: function() {
            NewsLetterEdit.config.method = "GetSubscriberEmailList";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({ PortalID: NewsLetterEdit.config.PortalID });
            NewsLetterEdit.config.ajaxCallMode = 4;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        BindSubscriberEmailList: function(data) {
            var emailhtml = "";
            if (data.d.length > 0) {
                emailhtml += '<table>';
                $.each(data.d, function(index, value) {
                    emailhtml += '<tr><td><input type="checkbox" id="delit_' + index + '" value="' + value.SubscriberEmail + '"/></td><td><label>' + value.SubscriberEmail + '</label></td></tr>';
                });
                emailhtml += '</table>';
                emailhtml += '<a href="#" id="btnEmailOK" class="sfbtnOk">Ok</a>';
            }
            else {
                emailhtml += 'No Subscriber found!!!';
            }
            $("#divSubscriberList").html(emailhtml);
        },
        GetMessageTemplateByTypeID: function(TemplateTypeID) {
            NewsLetterEdit.config.method = "GetMessageTemplateByTypeID";
            NewsLetterEdit.config.url = NewsLetterEdit.config.baseURL + NewsLetterEdit.config.method;
            NewsLetterEdit.config.data = JSON2.stringify({ MessageTemplateTypeID: TemplateTypeID, CultureName: NewsLetterEdit.config.CultureName, PortalID: NewsLetterEdit.config.PortalID });
            NewsLetterEdit.config.ajaxCallMode = 5;
            NewsLetterEdit.ajaxCall(NewsLetterEdit.config);
        },
        BindMessageByTemplateTypeID: function(data) {
            $.each(data.d, function(index, value) {
                $("#txtSubject").val(value.Subject);
                $("#txtBodyMsg").val(value.Body);

            });

        },
        GetMessageTemplateList: function(current, pageSize) {
            var mydata = JSON2.stringify({
                current: current,
                pageSize: pageSize,
                PortalID: NewsLetterEdit.config.PortalID,
                UserName: NewsLetterEdit.config.UserName,
                CultureName: NewsLetterEdit.config.CultureName,
                UserModuleID: NewsLetterEdit.config.UserModuleID,
                secureToken: SageFrameSecureToken
            });
            $.ajax({
                type: "POST",
                async: false,
                url: NewsLetterEdit.config.baseURL + "GetMessageTemplateList",
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    $("#MessageListing").html('');
                    var pageElements = '';
                    var rowTotal = 0;
                    var totalPage = 0;
                    var headElements = '';
                    var msgElement = '';
                    if (data.d.length > 0) {
                        headElements += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><thead class="sfHeading">';
                        headElements += '<th>Message Template Subject</th><th>From Email</th><th class="cssClassColumnEdit"></th><th class="cssClassColumnDelete"></th></thead>';
                        headElements += '</table>';
                        $("#MessageListing").html(headElements);
                        $.each(data.d, function(index, value) {
                            rowTotal = value.MessageTokenID;
                            msgElement += '<tr><td><label>' + value.Subject + '<label></td><td><label>' + value.MailFrom + '</label></td><td><a href="#" class="cssClassEditMessage" id="EditMessage_' + value.MessageTemplateID + '"><img src="' + NewsLetterEdit.config.resolvedURL + 'Administrator/Templates/default/images/btnedit.png" alt="Edit" /></a></td></tr>';
                        });
                        $("#MessageListing>table").append(msgElement);
                        totalPage = Math.ceil(rowTotal / pageSize);
                        var isCurrenExist = false;
                        pageElements += '<div id="divMessagePageNo" class="pageNo">';
                        for (var i = 1; i <= totalPage; i++) {
                            if (i == current) {
                                isCurrenExist = true;
                                pageElements += '<a href="#"><span class="current">' + i + '</span></a>';
                            }
                            else {
                                pageElements += '<a href="#"><span>' + i + '</span></a>';
                            }
                        }

                        pageElements += '</div>';
                        pageElements += '<div class="PageSize"><select id="ddlMessagePageSize" class="cssClassDropDown">';
                        pageElements += '<option value="15">15</option><option value="20">20</option><option value="30">30</option><option value="40">40</option>';
                        pageElements += '</select></div>';
                        $("#MessageListing").append(pageElements);
                        ////////////////////////////////////////////
                        $("#ddlMessagePageSize>option[value='" + pageSize + "']").attr("selected", "selected");
                        if (!isCurrenExist) {
                            $("#divMessagePageNo").find('a:last').find('span').addClass('current');
                        }

                        $("#ddlMessagePageSize>option[value='" + pageSize + "']").attr("selected", "selected");
                        if (!isCurrenExist) {
                            $("#divMessagePageNo").find('a:last').find('span').addClass('current');
                        }
                        $("#divMessagePageNo>a").bind("click", function(e) {
                            var obj = $(this);
                            e.preventDefault();
                            $("#divMessagePageNo>a span").removeAttr('class');
                            var curr = parseInt($(this).find('span').html());
                            var pageSize = $("#ddlMessagePageSize").val();
                            NewsLetterEdit.GetMessageTemplateList(curr, pageSize);
                        });
                        $("#divMessagePageNo>a span[class='current']").parent('a').unbind("click").attr("disabled", "disabled");
                        $("#ddlMessagePageSize").bind("change", function() {
                            var curr = NewsLetterEdit.config.Current;
                            var pageSize = $(this).val();
                            NewsLetterEdit.GetMessageTemplateList(curr, pageSize);

                        });


                        $(".cssClassEditMessage").bind("click", function(e) {
                            e.preventDefault();
                            var messageTemplateID = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
                            var myData = JSON2.stringify({ messageTemplateID: messageTemplateID });
                            $.ajax({
                                type: "POST",
                                async: false,
                                url: NewsLetterEdit.config.baseURL + "GetMessageInfoByMessageTemplateID",
                                data: myData,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function(data) {
                                    $.each(data.d, function(index, value) {
                                        $("#ddlMessageTemlate").val(value.MessageTemplateTypeID);
                                        $("#txtSubject").val(value.Subject);
                                        $("#txtBodyMsg").val(value.Body);
                                    });

                                },
                                error: function() {
                                    // alert('errorediting');
                                }

                            });
                        });

                    }
                    else {
                        msgElement = 'No Mesage Found';
                        $("#MessageListing").html(msgElement);
                    }

                },

                error: function() {
                    // alert('last error');
                }

            });
        }


    };
    NewsLetterEdit.init();
    $("#ddlMessageTemlate").on("change", function() {
        var TemplateTypeID = $(this).val();
        NewsLetterEdit.GetMessageTemplateByTypeID(TemplateTypeID);
    });
    $("#btnAddSubjectToken").on("click", function() {
        NewsLetterEdit.GetMessageTokenByTemplate();
        $("#TokenList").attr("style", "display:block;");
        SageFrame.popup.show("TokenList", "Select message token");

    });
    $("#btnAddToken").on("click", function() {

        var txtSelectedValuesObj = document.getElementById('txtSubject');
        var selectedArray = "";
        var selObj = document.getElementById('lstMessageToken');
        var i;
        var count = 0;
        for (i = 0; i < selObj.options.length; i++) {
            if (selObj.options[i].selected) {
                selectedArray += selObj.options[i].text;
                count++;
            }
        }
        txtSelectedValuesObj.value = selectedArray;
        SageFrame.popup.close("TokenList");

    });
    $("#btnAddBodyMsgToken").on("click", function() {
        NewsLetterEdit.GetMessageBodyTokenByTemplate();
        $("#divMessageBodyTokenList").attr("style", "display:block;");
        SageFrame.popup.show("divMessageBodyTokenList", "Select message token");
    });
    $("#btnAddBodyToken").bind("click", function() {
        var txtSelectedValuesObj = document.getElementById('txtBodyMsg');
        var selectedArray = "";
        var selObj = document.getElementById('lstMesageBodyToken');
        var i;
        var count = 0;
        for (i = 0; i < selObj.options.length; i++) {
            if (selObj.options[i].selected) {
                selectedArray += selObj.options[i].text;
                count++;
            }
        }
        $("#txtBodyMsg").val(selectedArray);
        SageFrame.popup.close("divMessageBodyTokenList");
    });
    $("#btnSubsciberAdd").on("click", function() {
        NewsLetterEdit.GetSubscriberEmailList();
        // $("#divSubscriberList").attr("style", "display:block;");
        SageFrame.popup.show("divSubscriberList", "Select Subscriber");
    });
    $("#btnSendEmail").on("click", function() {
        // e.preventDefault();
        //CKEDITOR.instances['txtBodyMsg'].updateElement();
        var v = $('#form1').validate({
            rules: {
                subject: { required: true },
                bodymsg: { required: true }
            },
            messages: {
                subject: { required: '*' },
                bodymsg: { required: '*' }

            }
        });

        if (v.form()) {
            NewsLetterEdit.SendEmailForSubscriber();
        }

    });
    $(document).on("click", '#btnEmailOK', function() {
        SageFrame.popup.close("divSubscriberList");
        var emailArr = new Array();
        $("input[type=checkbox][id^='delit_']:checked").each(function() {

            emailArr.push($(this).attr("value"));
        })
        emailArr.join(",");
        var emailList = emailArr;
        $("#txtEmailList").val(emailList);
    });
    $(".main a").on("click", function() {
        var tabType = $(this).attr("name");
        if (tabType == "EmailSubsciption") {
            $('.sfAdminPanel').find('a').removeClass('cssClassActive');
            $(this).addClass('cssClassActive');
            $("#Email").show();
            $("#PhoneMessage").hide();

        }
        if (tabType == "MobileSubscription") {
            $('.sfAdminPanel').find('a').removeClass('cssClassActive');
            $(this).addClass('cssClassActive');
            $("#Email").hide();
            $("#PhoneMessage").show();
        }
    });
});