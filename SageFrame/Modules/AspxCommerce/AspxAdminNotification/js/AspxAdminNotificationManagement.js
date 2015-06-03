
(function ($) {
    $.AdminNotificationManagementList = function (p) {
        p = $.extend
        ({
            aspxAdminNotificationModulePath: ""
        }, p);
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            SessionCode: AspxCommerce.utils.GetSessionCode(),
            CustomerID: AspxCommerce.utils.GetCustomerID()
        };

        AdminNotificationManagement = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                               baseURL: p.aspxAdminNotificationModulePath + "AdminNotificationHandler.ashx/",
                method: "",
                url: "",
                userName: userName,
                ajaxCallMode: 0,
                itemid: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: AdminNotificationManagement.config.type,
                    contentType: AdminNotificationManagement.config.contentType,
                    cache: AdminNotificationManagement.config.cache,
                    async: AdminNotificationManagement.config.async,
                    url: AdminNotificationManagement.config.url,
                    data: AdminNotificationManagement.config.data,
                    dataType: AdminNotificationManagement.config.dataType,
                    success: AdminNotificationManagement.ajaxSuccess,
                    error: AdminNotificationManagement.ajaxFailure
                });
            },
            init: function () {
                AdminNotificationManagement.NotificationSettingsGetAll();
                var formVal = $("#form1").validate({
                    ignore: ":hidden",
                    messages: {
                        NotificationCountForUser: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        },
                        NotificationCountForSubscription: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        },
                        NotificationCountForOutOfStock: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        },
                        NotificationCountForLowStock: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        },
                        NotificationQuantityLowStock: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        },
                        NotificationCountForOrders: {
                            required: '*', number: true, maxlength: getLocale(AspxAdminNotificationLanguage, "* (no more than 5 digits)")
                        }
                    }
                });
                $("#btnSaveNotificationSettings").click(function () {
                    if (formVal.form()) {
                        AdminNotificationManagement.NotificationSaveUpdateSettings();
                        csscody.info("<h2>" + getLocale(AspxAdminNotificationLanguage, 'Successful Message') + "</h2><p>" + getLocale(AspxAdminNotificationLanguage, 'Settings saved with success!') + "</p>");
                    }
                });
                $("#btnRefrershNotificationSettings").click(function () {
                    AdminNotificationManagement.NotificationSettingsGetAll();
                });

                $("#ddlNotificationAll").change(function () {
                    var selectedValue = $("#ddlNotificationAll option:selected").val();
                    $("#ddlNotificationUserRegistration").val(selectedValue);
                    $("#ddlNotificationSubscription").val(selectedValue);
                    $("#ddlNotificationLowStock").val(selectedValue);
                    $("#ddlNNotificationOrders").val(selectedValue);
                    $("#ddlNotificationOutOfStock").val(selectedValue);
                    
                });
            },
            NotificationSaveUpdateSettings: function () {
                var NotificationSaveUpdateSettingsInfo = {
                    AllActive: false,
                    UserNotificationActive: false,
                    UserNotificationCount: 0,
                    SubscriptionNotificationActive: false,
                    SubscriptionNotificationCount: 0,
                    OutofStockNotificationActive: false,
                    OutofStockNotificationCount: 0,
                    ItemsLowStockNotificationActive: false,
                    ItemsLowStockCount: 0,
                    OrdersNotificationAtive: false,
                    OrdersNotificationCount: 0
                };
                NotificationSaveUpdateSettingsInfo.AllActive = $("#ddlNotificationAll option:selected").val();
                NotificationSaveUpdateSettingsInfo.UserNotificationActive = $("#ddlNotificationUserRegistration option:selected").val();
                NotificationSaveUpdateSettingsInfo.UserNotificationCount = $("#txtNotificationCountForUser").val();
                NotificationSaveUpdateSettingsInfo.SubscriptionNotificationActive = $("#ddlNotificationSubscription option:selected").val();
                NotificationSaveUpdateSettingsInfo.SubscriptionNotificationCount = $("#txtNotificationCountForSubscription").val();
                NotificationSaveUpdateSettingsInfo.OutofStockNotificationActive = $("#ddlNotificationOutOfStock option:selected").val();
                NotificationSaveUpdateSettingsInfo.OutofStockNotificationCount = $("#txtNotificationCountForOutOfStock").val();
                NotificationSaveUpdateSettingsInfo.ItemsLowStockNotificationActive = $("#ddlNotificationLowStock option:selected").val();
                NotificationSaveUpdateSettingsInfo.ItemsLowStockCount = $("#txtNotificationCountForLowStock").val();
                NotificationSaveUpdateSettingsInfo.OrdersNotificationAtive = $("#ddlNNotificationOrders option:selected").val();
                NotificationSaveUpdateSettingsInfo.OrdersNotificationCount = $("#txtNotificationCountForOrders").val();
                AdminNotificationManagement.config.method = "NotificationSaveUpdateSettings";
                AdminNotificationManagement.config.url = AdminNotificationManagement.config.baseURL + AdminNotificationManagement.config.method;
                AdminNotificationManagement.config.data = JSON2.stringify({ saveUpdateInfo: NotificationSaveUpdateSettingsInfo, aspxCommonObj: aspxCommonObj });
                AdminNotificationManagement.config.ajaxCallMode = 1;
                AdminNotificationManagement.ajaxCall(AdminNotificationManagement.config);
            },
            NotificationSettingsGetAll: function () {
                AdminNotificationManagement.config.method = "NotificationSettingsGetAll";
                AdminNotificationManagement.config.url = AdminNotificationManagement.config.baseURL + AdminNotificationManagement.config.method;
                AdminNotificationManagement.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                AdminNotificationManagement.config.ajaxCallMode = 2;
                AdminNotificationManagement.ajaxCall(AdminNotificationManagement.config);
            },
            NotificationSettingsGetAllSuccess: function (data) {

                $("#ddlNotificationAll").val('' + data.d.AllActive + '');
                $("#ddlNotificationUserRegistration").val('' + data.d.UserNotificationActive + '');
                $("#txtNotificationCountForUser").val(data.d.UserNotificationCount);
                $("#ddlNotificationSubscription").val('' + data.d.SubscriptionNotificationActive + '');
                $("#txtNotificationCountForSubscription").val(data.d.SubscriptionNotificationCount);
                $("#ddlNotificationOutOfStock").val('' + data.d.OutofStockNotificationActive + '');
                $("#txtNotificationCountForOutOfStock").val(data.d.OutofStockNotificationCount);
                $("#ddlNotificationLowStock").val('' + data.d.ItemsLowStockNotificationActive + '');
                $("#txtNotificationCountForLowStock").val(data.d.ItemsLowStockCount);
                $("#ddlNNotificationOrders").val('' + data.d.OrdersNotificationAtive + '');
                $("#txtNotificationCountForOrders").val(data.d.OrdersNotificationCount);

            },
            ClearAll: function (data) {
                $("#ddlNotificationAll").empty();
                $("#ddlNotificationUserRegistration").empty();
                $("#txtNotificationCountForUser").val('');
                $("#ddlNotificationSubscription").empty();
                $("#txtNotificationCountForSubscription").val('');
                $("#ddlNotificationOutOfStock").empty();
                $("#txtNotificationCountForOutOfStock").val('');
                $("#ddlNotificationLowStock").empty();
                $("#txtNotificationCountForLowStock").val('');
                $("#txtNotificationQuantityLowStock").val('');
                $("#ddlNNotificationOrders").empty();
                $("#txtNotificationCountForOrders").val('');
            },
            ajaxSuccess: function (data) {
                switch (AdminNotificationManagement.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        AdminNotificationManagement.NotificationSettingsGetAll();
                        break;
                    case 2:
                        AdminNotificationManagement.NotificationSettingsGetAllSuccess(data);
                        break;
                }
            }
        };
        AdminNotificationManagement.init();
    };
    $.fn.AdminNotificationManagementDetails = function (p) {
        $.AdminNotificationManagementList(p);
    };
})(jQuery);

