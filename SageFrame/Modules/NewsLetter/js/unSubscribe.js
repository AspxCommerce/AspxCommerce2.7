$(function () {

    var NewsLetter = {
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
            subscriberID: ''

        },
        init: function () {
            $('input:radio[value=ByEmail]').attr('checked', true);
            $('input:radio[value=ByPhone]').attr('checked', false);
            $('#phoneUnSubscribe').hide();
            $('#divEmailUnSubsCribe').show();
            NewsLetter.GetSetting();
            $("#imageplace").html('<img src="' + NewsLetterPath + 'images/UnSubscribe.jpg" alt="subscribe"/>');

            if (document.location.search.length) {
                $("#divUnSubscribe").hide();
                $("#unsubscribeEmailLink").show();

            }
            else {
                $("#divUnSubscribe").show();
                $("#unsubscribeEmailLink").hide();
            }
            var subscriberID = "";
            NewsLetter.config.subscriberID = NewsLetter.GetsubScriberID(subscriberID);
            if (!isNaN(NewsLetter.config.subscriberID)) {
                if (NewsLetter.config.subscriberID != "null") {
                    alert(NewsLetter.config.subscriberID);
                    NewsLetter.UnSubscribeByEmailLink(NewsLetter.config.subscriberID);

                }
            }
        },
        GetsubScriberID: function (name) {
            return decodeURI((RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]);
        },
        UnSubscribeByEmailLink: function (subscriberID) {
            var param = JSON2.stringify({ subscriberID: subscriberID });
            $.ajax({
                type: "POST",
                url: NewsLetter.config.baseURL + "UnSubscribeByEmailLink",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('.message').html("We are very sorry to know that you unsubscribe our news letter!!!");
                }
            });
        },
        UnSubscribeUserByEmail: function () {
            var email = $("#txtEmailUnSubscribe").val();
            var mydata = JSON2.stringify({ Email: email });
            $.ajax({
                type: "POST",
                async: false,
                url: NewsLetter.config.baseURL + "UnSubscribeUserByEmail",
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#txtEmailUnSubscribe").val('');
                    $("#lblmessage").html("UnSubscibed Successfully");
                },
                error: function () {
                }
            });



        },
        UnSubscribeUserByMobile: function () {
            var phone = $("#txtPhone").val();
            var mydata = JSON2.stringify({ Phone: phone });
            $.ajax({
                type: "POST",
                async: false,
                url: NewsLetter.config.baseURL + "UnSubscribeUserByMobile",
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#txtPhone").val('');
                    $("#lblmessage").html("UnSubscibed Successfully");
                },
                error: function () {
                }
            });
        },
        GetSetting: function () {
            var param = JSON2.stringify({});
            $.ajax({
                type: "POST",
                url: NewsLetter.config.baseURL + "GetNLSettingForUnSubscribe",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $.each(data.d, function (index, item) {
                        if (item.IsMobileSubscription == "false") {
                            $("#divRadio").hide();
                            $("#phoneUnSubscribe").hide();
                        }
                    });
                },
                error: function () {
                    alert('error');
                }
            });
        }
    };
    NewsLetter.init();
    jQuery('input[name=rdbUnSubcribe]:radio').click(function () {
        var clickval = jQuery(this).val();
        if (clickval == 'ByEmail') {
            $('#divEmailUnSubsCribe').show();
            $('#phoneUnSubscribe').hide();
        }
        else if (clickval == 'ByPhone') {
            $('#divEmailUnSubsCribe').hide();
            $('#phoneUnSubscribe').show();
        }
    });
    $("#btnUnSubscribe").on("click", function (event) {
        event.preventDefault();
        if ($('input:radio[value=ByEmail]').prop('checked') == true) {
            var v = $('#form1').validate({
                rules: {
                    Email: { required: true, email: true }

                },
                messages: {
                    Email: { required: '*' }

                }
            });

            if (v.form()) {
                NewsLetter.UnSubscribeUserByEmail();

            }

            return true;
        }
        else {
            if ($("#txtPhone").val() != "") {
                NewsLetter.UnSubscribeUserByMobile();

            }
            return true;

        }
    });

});

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}