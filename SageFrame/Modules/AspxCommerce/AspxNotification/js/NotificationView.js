var NotificationView = "";

$(function () {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();

    NotificationView = {
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
            ajaxCallMode: ""

        },

        ajaxCall: function (config) {
            $.ajax({
                type: NotificationView.config.type,
                contentType: NotificationView.config.contentType,
                cache: NotificationView.config.cache,
                async: NotificationView.config.async,
                url: NotificationView.config.url,
                data: NotificationView.config.data,
                dataType: NotificationView.config.dataType,
                success: NotificationView.config.ajaxCallMode,
                error: NotificationView.ajaxFailure
            });
        },

        GetAllNotifications: function () {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            NotificationView.config.method = "GetAllNotification";
            NotificationView.config.url = this.config.baseURL + this.config.method;
            NotificationView.config.data = param;
            NotificationView.config.ajaxCallMode = NotificationView.BindNotificationData;
            NotificationView.ajaxCall(NotificationView.config);
        },

        GetAllNotificationByType: function (notificationTypeID) {            
            var param = JSON2.stringify({ notificationTypeID: notificationTypeID, storeID: storeId, portalID: portalId, cultureName: cultureName });
            NotificationView.config.method = "GetNotificationByType";
            NotificationView.config.url = this.config.baseURL + this.config.method;
            NotificationView.config.data = param;
            NotificationView.config.ajaxCallMode = NotificationView.BindNotificationByTypeData;
            NotificationView.ajaxCall(NotificationView.config);
        },

        UpdateNotification: function (notificationID) {
            var param = JSON2.stringify({ notificationID: notificationID, storeID: storeId, portalID: portalId });
            this.config.method = "UpdateNotification";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
                       this.ajaxCall(this.config);
        },

        BindNotificationData: function (data) {
                                  if (data.d.length > 0) {
                var htmlc = "";
                nbc_notifs = 0;
                var htmlo = "";
                nbo_notifs = 0;
                $.each(data.d, function (index, item) {
                    if (item.NotificationTypeID == 3) {
                                               nbc_notifs = item.NotificationCount;
                        $("#customers_notif_value").attr("notifyType", item.NotificationTypeID);
                    }
                    if (item.NotificationTypeID == 1) {
                        nbo_notifs = item.NotificationCount;
                        $("#orders_notif_value").attr("notifyType", item.NotificationTypeID);
                    }
                });
                if (nbc_notifs > 0) {
                    $("#list_customers_notif").prev("p").hide();
                                                          $("#customers_notif_value").text(nbc_notifs);
                    $("#customers_notif_number_wrapper").show();
                }
                else {
                    $("#customers_notif_number_wrapper").hide();
                }
                if (nbo_notifs > 0) {
                    $("#list_orders_notif").prev("p").hide();
                                                          $("#orders_notif_value").text(nbo_notifs);
                    $("#orders_notif_number_wrapper").show();
                }
                else {
                    $("#orders_notif_number_wrapper").hide();
                }
            }

        },

        BindNotificationByTypeData: function (data) {
            if (data.d.length > 0) {
                var htmlc = "";
                nbc_notifs = 0;
                var htmlo = "";
                nbo_notifs = 0;
                $.each(data.d, function (index, item) {
                    if (item.NotificationTypeID == 3) {
                        htmlc += "<li>" + getLocale(AspxNotification, "A new Customer has been registered on your shop") + "<br />" + getLocale(AspxNotification, "Customer Name:") + "<strong><a id='lnkCustomerDetail' notifyID=" + item.NotificationID + " customerID=" + item.UserName + ">" + item.UserName + "</a></strong></li>";
                                                                  }
                    if (item.NotificationTypeID == 1) {
                        htmlo += "<li>" + getLocale(AspxNotification, "A new Order has been placed on your shop") + "<br />" + getLocale(AspxNotification, "Order ID:") + "<strong>" + item.OrderID + "</strong><br /><a id='lnkOrderDetail' notifyID=" + item.NotificationID + " href=\"#" + "\">" + getLocale(AspxNotification, "See Order Detail") + "</a></li>";
                                                                  }
                });
                if (htmlc != "") {
                    $("#list_customers_notif").prev("p").hide();
                    $("#list_customers_notif").empty().append(htmlc);
                                                                         }
                else {
                    $("#customers_notif_number_wrapper").hide();
                }
                if (htmlo != "") {
                    $("#list_orders_notif").prev("p").hide();
                    $("#list_orders_notif").empty().append(htmlo);
                                                                         }
                else {
                    $("#orders_notif_number_wrapper").hide();
                }
            }

        },

        init: function () {
            NotificationView.GetAllNotifications();
            var youEditFieldFor = "";
            var hints = $('.translatable span.hint');
            if (youEditFieldFor) {
                hints.html(hints.html() + '<br /><span class="red">' + youEditFieldFor + '</span>');
            }
            var html = "";
            var nb_notifs = 0;
            var wrapper_id = "";
            var type = new Array();
            var notificationTypeID = 0;
            var notificationCount = 0;
            $(".notifs").on("click", function () {
                               $('.notifs').removeClass('open_notifs');
                $(this).addClass('open_notifs');
                wrapper_id = $(this).attr("id");
                type = wrapper_id.split("s_notif")
                notificationTypeID = $(this).children("span").children("span").attr("notifyType");
                notificationCount = $(this).children("span").children("span").html();
                if (!$("#" + wrapper_id + "_wrapper").is(":visible")) {
                    $(".notifs_wrapper").hide();
                    $("#" + wrapper_id + "_number_wrapper").hide();
                    $("#" + wrapper_id + "_wrapper").show();
                } else {
                    $("#" + wrapper_id + "_wrapper").hide();
                }
                if (notificationCount > 0) {
                    NotificationView.GetAllNotificationByType(notificationTypeID);
                }

            });
            $("#main").click(function () {
                $(".notifs_wrapper").hide();
                $('.notifs').removeClass('open_notifs');
            });

            $("#lnkCustomerDetail").on("click", function () {
                var notificationID = $(this).attr("notifyID");
                var customerID = $(this).attr("customerID");
                NotificationView.UpdateNotification(notificationID);
                window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Admin/Users-Management.aspx?id=" + customerID + "";
            });

            $("#lnkShowAllCustomers").on("click", function () {
                window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Admin/Users-Management.aspx";
            });
        }
    }
    NotificationView.init();
});