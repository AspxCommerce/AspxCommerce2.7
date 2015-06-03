var NotificationSetting = "";

$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    NotificationSetting = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: ModulePath + "AspxNotificationWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: 0

        },
        ajaxCall: function(config) {
            $.ajax({
                type: NotificationSetting.config.type,
                contentType: NotificationSetting.config.contentType,
                cache: NotificationSetting.config.cache,
                async: NotificationSetting.config.async,
                url: NotificationSetting.config.url,
                data: NotificationSetting.config.data,
                dataType: NotificationSetting.config.dataType,
                success: NotificationSetting.ajaxSuccess,
                error: NotificationSetting.ajaxFailure
            });
        },

        ajaxSuccess: function(data) {
            switch (NotificationSetting.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var value = data.d;
                    if (value != null) {
                        NotificationSetting.BindNotificationData(value);
                    }
                    break;
                case 2:
                    csscody.info("<h2>"+getLocale(AspxNotification,'Information Message')+'</h2><p>'+getLocale(AspxNotification,'Notification Setting Has Been Updated Sucessfully')+"</p>");
                    break;
            }
        },

        GetNotificationSetting: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            NotificationSetting.config.method = "GetAllNotificationSettings";
            NotificationSetting.config.url = this.config.baseURL + this.config.method;
            NotificationSetting.config.data = param;
            NotificationSetting.config.ajaxCallMode = 1;
            NotificationSetting.ajaxCall(NotificationSetting.config);
        },

        NotificationSettingUpdate: function() {
            var enableNewOrder = $("#chkEnableNewOrder").attr("checked");
            var newOrderNoticeReceiver = $("#txtNewOrderNoticeReceiver").val();
            var enableOrderStatChange = $("#chkEnableOrderStatChange").attr("checked");
            var orderStatChangeNotifyEmail = $("#txtOrderStatChangeNotifyEmail").val();
            var enableCustomerRegNotify = $("#chkEnableCustomerRegNotify").attr("checked");
            var customerRegEmail = $("#txtCustomerRegEmail").val();
            var enableReviewsNotify = $("#chkEnableReviewsNotify").attr("checked");
            var reviewNotifyEmail = $("#txtReviewNotifyEmail").val();
            var enableNewTagsNotify = $("#chkEnableNewTagsNotify").attr("checked");
            var newTagsNotifyEmail = $("#txtNewTagsNotifyEmail").val();
            var enableCouponNotify = $("#chkEnableCouponNotify").attr("checked");
            var couponUseNotifyEmail = $("#txtCouponUseNotifyEmail").val();
            var enableSuscribeNotify = $("#chkEnableSuscribeNotify").attr("checked");
            var custSuscribeNotifyEmail = $("#txtCustSuscribeNotifyEmail").val();

            var settingValues = '';
            settingValues += enableNewOrder + '*' + newOrderNoticeReceiver + '*' + enableOrderStatChange + '*' + orderStatChangeNotifyEmail + '*' + enableCustomerRegNotify + '*';
            settingValues += customerRegEmail + '*'+ enableReviewsNotify + '*' + reviewNotifyEmail + '*' + enableNewTagsNotify + '*' + newTagsNotifyEmail + '*';
            settingValues += enableCouponNotify + '*' + couponUseNotifyEmail + '*' + enableSuscribeNotify + '*' + custSuscribeNotifyEmail;

            var settingKeys = '';
            settingKeys += 'EnableNewOrder' + '*' + 'NewOrderNoticeReceiver' + '*' + 'EnableOrderStatChangeNotice' + '*' + 'OrderStatChangeNotifyEmail' + '*' + 'EnableCustomerRegNotify' + '*';
            settingKeys += 'CustomerRegNotifyEmail' + '*' + 'EnableReviewsNotify' + '*' + 'ReviewNotifyEmail' + '*' + 'EnableNewTagsNotify' + '*' + 'NewTagsNotifyEmail' + '*';
            settingKeys += 'EnableCouponNotify' + '*' + 'CouponUseNotifyEmail' + '*' + 'EnableSuscribeNotify' + '*' + 'CustSuscribeNotifyEmail';

            var param = JSON2.stringify({ settingKeys: settingKeys, settingValues: settingValues, storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.method = "UpdateNotificationSettings";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindNotificationData: function(obj) {
            $("#chkEnableNewOrder").attr("checked", $.parseJSON(obj.EnableNewOrder.toLowerCase()));
            $("#txtNewOrderNoticeReceiver").val(obj.NewOrderNoticeReceiver);
            $("#chkEnableOrderStatChange").attr("checked", $.parseJSON(obj.EnableOrderStatChangeNotice.toLowerCase()));
            $("#txtOrderStatChangeNotifyEmail").val(obj.OrderStatChangeNotifyEmail);
            $("#chkEnableCustomerRegNotify").attr("checked", $.parseJSON(obj.EnableCustomerRegNotify.toLowerCase()));
            $("#txtCustomerRegEmail").val(obj.CustomerRegNotifyEmail);
            $("#chkEnableReviewsNotify").attr("checked", $.parseJSON(obj.EnableReviewsNotify.toLowerCase()));
            $("#txtReviewNotifyEmail").val(obj.ReviewNotifyEmail);
            $("#chkEnableNewTagsNotify").attr("checked", $.parseJSON(obj.EnableNewTagsNotify.toLowerCase()));
            $("#txtNewTagsNotifyEmail").val(obj.NewTagsNotifyEmail);
            $("#chkEnableCouponNotify").attr("checked", $.parseJSON(obj.EnableCouponNotify.toLowerCase()));
            $("#txtCouponUseNotifyEmail").val(obj.CouponUseNotifyEmail);
            $("#chkEnableSuscribeNotify").attr("checked", $.parseJSON(obj.EnableSuscribeNotify.toLowerCase()));
            $("#txtCustSuscribeNotifyEmail").val(obj.CustSuscribeNotifyEmail);
        },

        init: function() {
            $(".cssClassCommonBox Curve").hide();
            NotificationSetting.GetNotificationSetting();
            $(".cssClassCommonBox Curve").show();

            $("#form1").validate({
                messages: {
                    NewOrderNoticeReceiver: { required: '*'
                    },
                    OrderStatChangeNotifyEmail: { required: '*'
                    },
                    CustomerRegEmail: { required: '*'
                    },
                    ReviewNotifyEmail: { required: '*'
                    },
                    NewTagsNotifyEmail: { required: '*'
                    },
                    CouponUseNotifyEmail: { required: '*'
                    },
                    CustSuscribeNotifyEmail: { required: '*'
                    }
                },
                submitHandler: function() {
                    NotificationSetting.NotificationSettingUpdate();
                }
            });
        }

    }
    NotificationSetting.init();
});