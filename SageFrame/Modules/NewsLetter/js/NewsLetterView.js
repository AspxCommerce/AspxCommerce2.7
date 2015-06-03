$(function () {
    var RootPath = AspxCommerce.utils.GetAspxRootPath();
    rewardPointsObj = {
        Email: "",
        RewardRuleID: 2
    };
    aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID(),
        SessionCode: AspxCommerce.utils.GetSessionCode()

    };
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
            url: "",
            ajaxCallMode: "",
            ModulePath: '',
            PortalID: PortalID,
            UserModuleID: UserModuleID,
            UserName: UserName,
            PageExt: PageExt
        },
        ajaxCall: function(config) {
            $.ajax({
                type: NewsLetter.config.type,
                contentType: NewsLetter.config.contentType,
                cache: NewsLetter.config.cache,
                async: NewsLetter.config.async,
                url: NewsLetter.config.url,
                data: NewsLetter.config.data,
                dataType: NewsLetter.config.dataType,
                success: NewsLetter.config.ajaxCallMode,
                error: NewsLetter.config.error
            });
        },
        init: function() {
            $('input:radio[value=ByEmail]').attr('checked', true);
            $('input:radio[value=ByPhone]').attr('checked', false);
            $('#phoneSubscribe').hide();
            $('#divEmailSubsCribe').show();
            $("#imageplace").html('<img src="' + NewsLetterPath + 'images/subscribe.png" alt="subscribe"/>');
        },
        IsModuleInstalled: function() {
            var rewardPoints = 'AspxRewardPoints';
            $.ajax({
                type: "POST",
                url: AspxCommerce.utils.GetAspxServicePath() + "AspxCommonHandler.ashx/" + "GetModuleInstallationInfo",
                data: JSON2.stringify({ moduleFriendlyName: rewardPoints, aspxCommonObj: aspxCommonObj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function(response) {
                    var isInstalled = response.d;
                    if (isInstalled == true) {
                        NewsLetter.SaveRewardPointsCore();
                    }
                },
                error: function() {
                    csscody.error("<h2>" + GetSystemLocale("Error Message") + "</h2><p>" + GetSystemLocale("Failed to load module installation information!.") + "</p>");
                }
            });
        },
        SaveRewardPointsCore: function() {
            rewardPointsObj.Email = $("#txtSubscribeEmail").val();
            var ModuleServicePath = RootPath + "Modules/AspxCommerce/AspxRewardPoints/RewardPointsHandler.ashx/";

            this.config.method = "RewardPointsSaveNewsLetter";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ rewardPointsInfo: rewardPointsObj, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = NewsLetter.SaveRewardPointsCoreSuccess;
            this.config.error = NewsLetter.SaveRewardPointsCoreError;
            this.ajaxCall(this.config);
        },
        SaveRewardPointsCoreSuccess: function() {
        },
        SaveRewardPointsCoreError: function() {
            csscody.error("<h2>" + GetSystemLocale("Error Message") + "</h2><p>" + GetSystemLocale("Failed to add reward points!") + "</p>");
        },
        SaveEmailSubscriber: function() {
            var email = $("#txtSubscribeEmail").val();
            if (NewsLetter.CheckPreviousEmailSubscription(email)) {
                var html = GetSystemLocale('You are already a subscribed member');
                $("#lblmessage").html(html).css({ "display": "block", "color": "red" });

            }

            else {
                var mydata = JSON2.stringify({
                    Email: email,
                    UserModuleID: NewsLetter.config.UserModuleID,
                    PortalID: NewsLetter.config.PortalID,
                    UserName: NewsLetter.config.UserName,
                    secureToken: SageFrameSecureToken
                });

                $.ajax({
                    type: "POST",
                    async: false,
                    url: NewsLetter.config.baseURL + "SaveEmailSubscriber",
                    data: mydata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        if (aspxCommonObj.CustomerID != 0) {
                            NewsLetter.IsModuleInstalled();
                        }
                        $("#txtSubscribeEmail").val('');
                        $("#lblmessage").html(GetSystemLocale("Subscribed Successfully")).css({ "display": "block", "color": "green" });
                    },
                    error: function() {
                    }
                });
            }


        },
        CheckPreviousEmailSubscription: function(email) {
            var bitval = true;
            $.ajax({
                type: "POST",
                async: false,
                url: NewsLetter.config.baseURL + "CheckPreviousEmailSubscription",
                data: JSON2.stringify({
                    Email: email,
                    PortalID: SageFramePortalID,
                    UserModuleID: NewsLetter.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    if (data.d.length > 0) {
                        bitval = true;
                    }
                    else {
                        bitval = false;
                    }
                },
                error: function() {
                }
            });
            return bitval;
        },
        SaveMobileSubscriber: function() {
            var phone = $("#txtPhone").val();
            var mydata = JSON2.stringify({
                Phone: phone,
                UserModuleID: NewsLetter.config.UserModuleID,
                PortalID: NewsLetter.config.PortalID,
                UserName: NewsLetter.config.UserName,
                secureToken: SageFrameSecureToken
            });
            $.ajax({
                type: "POST",
                async: false,
                url: NewsLetter.config.baseURL + "SaveMobileSubscriber",
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    $("#txtPhone").val('');
                    $("#lblmessage").html(GetSystemLocale("Subscribed Successfully")).css({ "display": "block", "color": "green" });
                },
                error: function() {
                }
            });
        },

    };
    NewsLetter.init();

    jQuery('input[name=rdbSubcribe]:radio').click(function() {
        var clickval = jQuery(this).val();
        if (clickval == 'ByEmail') {
            $('#divEmailSubsCribe').show();
            $('#phoneSubscribe').hide();
        }
        else if (clickval == 'ByPhone') {
            $('#divEmailSubsCribe').hide();
            $('#phoneSubscribe').show();
        }
    });
    $("#btnSubscribe").off().on("click", function (event) {
        //alert('');
        event.preventDefault();
        if ($('input:radio[value=ByEmail]').prop('checked') == true) {
            var email = $('#divEmailSubsCribe>#txtSubscribeEmail').val();
            var email_check = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$/i;
            if (!email_check.test(email)) {
                $('#lblmessage').text(GetSystemLocale("Invalid Email")).css({ "display": "block", "color": "red" });
                return false;
            }
            else {
                NewsLetter.SaveEmailSubscriber();
                return true;
            }

        }
        else {
            if ($("#txtPhone").val() != "") {
                NewsLetter.SaveMobileSubscriber();
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