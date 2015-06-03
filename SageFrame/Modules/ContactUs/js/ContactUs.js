(function($) {
    $.feedbackTab = function(p) {
        p = $.extend
        ({
            PortalID: 1,
            ContactUsPath: '',
            UserName: '',
            subject: '',
            emailSucessMsg: '',
            UserModuleID:''
        }, p);
        var feedbackTab = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: p.ContactUsPath + 'Services/ContactUsWebService.asmx/',
                method: "",
                ModulePath: '',
                PortalID: p.PortalID,
                UserName: p.UserName,
                subject: p.subject,
                emailSucessMsg: p.emailSucessMsg
            },

            ContactUsSaveAndSendEmail: function(name, email, subject, message) {
                var param = JSON2.stringify({
                    name: name,
                    email: email,
                    subject: subject,
                    message: message,
                    isActive: true,
                    portalID: feedbackTab.config.PortalID,
                    addedBy: feedbackTab.config.UserName,
                    UserModuleID: p.UserModuleID,
                    SecureToken: SageFrameSecureToken
                });
                $.ajax({
                    type: "POST",
                    url: feedbackTab.config.baseURL + "ContactUsAddAndSendEmail",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        feedbackTab.ClearForm();
                        $('a.feedback-tab').click();
                        jAlert(feedbackTab.config.emailSucessMsg);
                    },
                    error: function() {
                        alert('error');
                    }
                });
            },
            ClearForm: function() {
                $("#txtName").val('');
                $("#txtContactEmail").val('');
                $("#txtMessage").val('');
                $('.Required').remove();
                $('.invalid').remove();
            },
            init: function() {
                $("#btnSubmit").off().on("click", function(event) {
                    var v = $('#form1').validate({
                        rules: {
                            name: { required: true },
                            email: { required: true },
                            message: { required: true }
                        },
                        messages: {
                            name: { required: '*' },
                            email: { required: '*' },
                            message: { required: '*' }
                        }
                    });
                    if (v.form()) {
                        var name = $("#txtName").val();
                        var email = $("#txtContactEmail").val();
                        var message = $("#txtMessage").val();
                        feedbackTab.ContactUsSaveAndSendEmail(name, email, feedbackTab.config.subject, message);
                    }
                    return true;

                });
                $("#btnReset").on('click', function() {
                    feedbackTab.ClearForm();
                });
            }
        };
        feedbackTab.init();
    };
    $.fn.feedback = function(p) {
        $.feedbackTab(p);
    };
})(jQuery);