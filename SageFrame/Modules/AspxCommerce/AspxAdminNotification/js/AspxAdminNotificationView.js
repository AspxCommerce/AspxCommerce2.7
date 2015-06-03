var notificationsNumber = 0;
var originaltitle = $('title').text();
(function ($) {
    $.AdminNotificationViewList = function (p) {
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
        var popupStatus = 0; AdminNotificationView = {
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
                userName: AspxCommerce.utils.GetUserName(),
                ajaxCallMode: 0,
                itemid: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: AdminNotificationView.config.type,
                    contentType: AdminNotificationView.config.contentType,
                    cache: AdminNotificationView.config.cache,
                    async: AdminNotificationView.config.async,
                    url: AdminNotificationView.config.url,
                    data: AdminNotificationView.config.data,
                    dataType: AdminNotificationView.config.dataType,
                    success: AdminNotificationView.ajaxSuccess,
                    error: AdminNotificationView.ajaxFailure
                });
            },

            init: function () {
                AdminNotificationView.NotificationGetAll();
                $("#linkUsersInfo").click(function () {
                    if (!$(".cssClassNotify").is(":visible")) {
                        $(this).addClass("sfNotificationSelect");
                        AdminNotificationView.NotificationUsersGetAll();
                    }
                    else {
                        $(this).removeClass("sfNotificationSelect");
                    }
                });

                $("#linkItemsInfo").click(function () {
                    if (!$(".cssClassNotify").is(":visible")) {
                        $(this).addClass("sfNotificationSelect");
                        AdminNotificationView.NotificationItemsGetAll();
                    }
                    else {
                        $(this).removeClass("sfNotificationSelect");
                    }
                });
                $("#linkOrdersInfo").click(function () {
                    if (!$(".cssClassNotify").is(":visible")) {
                        $(this).addClass("sfNotificationSelect");
                        AdminNotificationView.NotificationOrdersGetAll();
                    }
                    else {
                        $(this).removeClass("sfNotificationSelect");
                    }
                });

                $(".notificationsSticker").outside('click', function () {
                    $('.cssClassNotify').stop(true, true).slideUp('slow');
                    $('.sfNotificationSelect').removeClass("sfNotificationSelect");

                });

                $(document).on('click', 'a.cssClassLowItemInfo', function () {
                    var itemsku = $(this).attr('id');
                    location.href = aspxRedirectPath + 'item/' + itemsku + pageExtension;
                    return false;
                });

            },
            loading: function () {
                $("div.loader").show();
            },
            closeloading: function () {
                $("div.loader").fadeOut('normal');
            },

            NotificationGetAll: function () {
                AdminNotificationView.config.method = "NotificationGetAll";
                AdminNotificationView.config.url = AdminNotificationView.config.baseURL + AdminNotificationView.config.method;
                AdminNotificationView.config.data = JSON2.stringify({ StoreID: aspxCommonObj.StoreID, PortalID: aspxCommonObj.PortalID });
                AdminNotificationView.config.ajaxCallMode = 1;
                AdminNotificationView.ajaxCall(AdminNotificationView.config);
            },
            NotificationGetAllSuccess: function (data) {
                
                if (data.d.UsersInfoCount !== 0) {
                    $("#spanUsersInfo").html(data.d.UsersInfoCount);
                    $("#spanUsersInfo").show();
                    notificationsNumber += parseInt(data.d.UsersInfoCount);
                }
                else {
                    $("#spanUsersInfo").hide();
                }
                if (data.d.ItemsInfoCount !== 0) {
                    $("#spanItemsInfo").html(data.d.ItemsInfoCount);
                    $("#spanItemsInfo").show();
                    notificationsNumber += parseInt(data.d.ItemsInfoCount);
                }
                else {
                    $("#spanItemsInfo").hide();
                }
                if (data.d.NewOrdersCount !== 0) {
                    $("#spanOrdersInfo").html(data.d.NewOrdersCount);
                    $("#spanOrdersInfo").show();
                    notificationsNumber += parseInt(data.d.NewOrdersCount);
                }
                else {
                    $("#spanOrdersInfo").hide();
                }
                UpdateTitle();
            },
            NotificationUsersGetAll: function () {
                AdminNotificationView.config.method = "NotificationUsersGetAll";
                AdminNotificationView.config.url = AdminNotificationView.config.baseURL + AdminNotificationView.config.method;
                AdminNotificationView.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                AdminNotificationView.config.ajaxCallMode = 2;
                AdminNotificationView.ajaxCall(AdminNotificationView.config);
                $("#spanUsersInfo").hide();
            },
            NotificationUsersGetAllSuccess: function (data) {
                var contentUser = "";
                var contentSubscription = "";
                var userData = "";

                contentUser = '<div>';
                if (data.d.length > 0) {
                    contentUser += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "Recently Registered and Subscribed Users:") + '</h5><ul>';
                    var i = 1;                    

                    var userName = "";
                    var customerID = "";

                    var intNewUsers = parseInt($('#spanUsersInfo').text());

                    $.each(data.d, function (index, value) {
                        userName = strEncrypt(value.UserName);
                        customerID = strEncrypt(value.CustomerID);
                        if (value.SubscriptionEmail != '') {
                            contentUser += '<li ' + (intNewUsers > 0 ? 'class="sfLastestNotification"' : '') + '>' + (value.UserName != '' ? "<a class='subsribedName'>" + value.UserName + "</a>" : '') + (value.SubscriptionEmail != "INSUFFICIENT_PARAMS" ? "<span class='subsribedEmail'>" + value.SubscriptionEmail + "</span>" : "") + "<span class='status subscribed'><strong>" + getLocale(AspxAdminNotificationLanguage, 'subscribed on:') + "</strong>" + value.AddedOn + "</span></li>";
                        }
                        else {
                            contentUser += '<li ' + (intNewUsers > 0 ? 'class="sfLastestNotification"' : '') + '>' + '<a id="' + value.UserName + '" class="registeredName" title="Click to View User Profile" href = "' + aspxRedirectPath + 'Admin/AspxCommerce/Customers/Manage-Customers' + pageExtension + "?customerID=" + customerID + "&userName=" + userName + '"> ' + value.UserName + ' </a><span class="status registered"><strong>' + getLocale(AspxAdminNotificationLanguage, "registered on:") + '</strong>' + value.AddedOn + '</span>' + ' </li>';
                        }

                        if (intNewUsers > 0) {
                            intNewUsers--;
                        }
                    });
                    contentUser += '</ul></div>';

                    notificationsNumber -= parseInt(data.d.length);
                    UpdateTitle();

                }
                else {
                    contentUser += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "There are no Recently Registered or Subscribed Users:") + '</h5>';
                    contentUser += '</div>';
                }

                userData += '<div class="cssClassNotify" style="display:none">' + contentUser + '</div>';

                $('.sfqckUserInfo').append(userData);
            },
            NotificationItemsGetAll: function () {
                AdminNotificationView.config.method = "NotificationItemsGetAll";
                AdminNotificationView.config.url = AdminNotificationView.config.baseURL + AdminNotificationView.config.method;
                AdminNotificationView.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                AdminNotificationView.config.ajaxCallMode = 3;
                AdminNotificationView.ajaxCall(AdminNotificationView.config);
                $("#spanItemsInfo").hide();
            },
            NotificationItemsGetAllSuccess: function (data) {
                var contentOutOfStock = "";
                var userData = "";
                var intNewItems = parseInt($('#spanItemsInfo').text());
                contentOutOfStock = '<div>';
                if (data.d.length > 0) {
                    contentOutOfStock += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "The following items are Out of Stock or Low of Stock:") + '</h5><ul>';
                    var i = 1;
                    $.each(data.d, function (index, value) {
                        if (value.UsedStoreSetting != '') {
                            contentOutOfStock += '<li ' + (intNewItems > 0 ? 'class="sfLastestNotification"' : '') + '>' + '<span class="ItemName">' + getLocale(AspxAdminNotificationLanguage, "Item Name:") + ' ' + value.ItemName + "</span>" + '<a id="' + value.SKU + '" class="ItemID" Title = "click here to edit items" href="' + aspxRedirectPath + 'Admin/AspxCommerce/Sales/Catalog/Manage-items' + pageExtension + "?itemid=" + strEncrypt(value.ItemID) + "&itemtypeid=" + strEncrypt(value.ItemTypeID) + "&attributesetid=" + strEncrypt(value.AttributeSetID) + "&currencycode=" + strEncrypt(value.CurrencyCode) + '">' + getLocale(AspxAdminNotificationLanguage, "SKU:") + ' ' + value.SKU + ' </a><span class="status outOfStock"><strong>' + getLocale(AspxAdminNotificationLanguage, "Out of stock") + '</strong></span>';
                        }
                        else {
                            contentOutOfStock += '<li ' + (intNewItems > 0 ? 'class="sfLastestNotification"' : '') + '>' + '<span class="ItemName">' + getLocale(AspxAdminNotificationLanguage, "Item Name:") + ' ' + i++ + ". " + value.ItemName + "</span>" + '<a id="' + value.SKU + '" class="ItemID" Title ="Click here to edit items" href="' + aspxRedirectPath + 'Admin/AspxCommerce/Sales/Catalog/Manage-items' + pageExtension + "?itemid=" + strEncrypt(value.ItemID) + "&itemtypeid=" + strEncrypt(value.ItemTypeID) + "&attributesetid=" + strEncrypt(value.AttributeSetID) + "&currencycode=" + strEncrypt(value.CurrencyCode) + '">' + getLocale(AspxAdminNotificationLanguage, "SKU:") + ' ' + value.SKU + ' </a><span class="status lowStock"><strong>' + getLocale(AspxAdminNotificationLanguage, "Low stock") + '</strong></span></li>';
                        }

                        if (intNewItems > 0) {
                            intNewItems--;
                        }
                    });
                    contentOutOfStock += '</ul></div>';

                    notificationsNumber -= parseInt(data.d.length);
                    UpdateTitle();
                }
                else {
                    contentOutOfStock += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "There are no items Out of Stock or Low of Stock:") + '</h5>';
                    contentOutOfStock += '</div>';
                }

                userData += '<div class="cssClassNotify" style="display:none">' + contentOutOfStock + '</div>';
                $('.sfqckItemsInfo').append(userData);
            },
            NotificationOrdersGetAll: function () {
                AdminNotificationView.config.method = "NotificationOrdersGetAll";
                AdminNotificationView.config.url = AdminNotificationView.config.baseURL + AdminNotificationView.config.method;
                AdminNotificationView.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                AdminNotificationView.config.ajaxCallMode = 4;
                AdminNotificationView.ajaxCall(AdminNotificationView.config);
                $("#spanOrdersInfo").hide();
            },
            NotificationOrdersGetAllSuccess: function (data) {
                var contentOrders = "";
                contentOrders += '<div class="cssClassNotify" style="display:none">';
                contentOrders += '<div>';
                var intNewOrders = parseInt($('#spanOrdersInfo').text());

                if (data.d.length > 0) {
                    contentOrders += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "Following are the Recent Orders:") + '</h5><ul>';
                    var i = 1;
                    $.each(data.d, function (index, value) {
                        contentOrders += '<li ' + (intNewOrders > 0 ? 'class="sfLastestNotification"' : '') + '>' + '<span class="orderId">' + getLocale(AspxAdminNotificationLanguage, "Order OF ID:") + '<a id="' + value.OrderID + '" title="Click to View ORDER"  href="' + aspxRedirectPath + 'Admin/AspxCommerce/Sales/Orders-Overview/Orders' + pageExtension + "?orderid=" + strEncrypt(value.OrderID) + '"> ' + value.OrderID + ' </a></span>' + '<span class="status ' + value.OrderStatusAliasName.toString().toLowerCase() + '"><strong>' + value.OrderStatusAliasName + '</strong></span>' + ' </li>';

                        if (intNewOrders > 0) {
                            intNewOrders--;
                        }

                    });
                    contentOrders += '</ul></div>';

                    notificationsNumber -= parseInt(data.d.length);
                    UpdateTitle();
                }
                else {
                    contentOrders += '<h5 class="cssClassNotifyHead">' + getLocale(AspxAdminNotificationLanguage, "There are no Recent Orders:") + '</h5>';
                    contentOrders += '</div>';
                }

                contentOrders += '</div>';
                $('.sfqckOrdersInfo').append(contentOrders);
            },
            ajaxSuccess: function (data) {
                switch (AdminNotificationView.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        AdminNotificationView.NotificationGetAllSuccess(data);
                        break;
                    case 2:
                        AdminNotificationView.NotificationUsersGetAllSuccess(data);
                        break;
                    case 3:
                        AdminNotificationView.NotificationItemsGetAllSuccess(data);
                        break;
                    case 4:
                        AdminNotificationView.NotificationOrdersGetAllSuccess(data);
                        break;
                }
            }
        };
        AdminNotificationView.init();
    };
    $.fn.AdminNotificationViewDetails = function (p) {
        $.AdminNotificationViewList(p);
    };
    $.fn.outside = function (ename, cb) {
        return this.each(function () {
            var $this = $(this),
                self = this;

            $(document).bind(ename, function tempo(e) {
                if (e.target !== self && !$.contains(self, e.target)) {
                    cb.apply(self, [e]);
                    if (!self.parentNode) $(document.body).unbind(ename, tempo);
                }
            });
        });
    };
})(jQuery);

$(document).ready(function () {
    $(".topopup").click(function () {

        $(".cssClassNotify").each(function () {
            if ($(this).is(':visible')) {
                $parentList = $(this).parent("li").siblings().find('a :first');
                $parentList.removeClass("sfNotificationSelect");
                $(this).slideUp();
            }
        });

        var $t = $(this).parent().find(".cssClassNotify");

        if ($t.is(':visible')) {
            $parentList = $(this).parent("li").siblings().find('a :first');
            $parentList.removeClass("sfNotificationSelect");
            $t.slideUp();
        } else {
            $parentList = $(this).parent("li").siblings().find('a :first');
            $parentList.removeClass("sfNotificationSelect");
            $(this).addClass("sfNotificationSelect");
            $t.slideDown();
        }
    });
});


// Source: http://stackoverflow.com/questions/497790
var dates = {
    convert: function (d) {
                                                               
        if (d != null) {
            return (
                d.constructor === Date ? d :
                d.constructor === Array ? new Date(d[0], d[1], d[2]) :
                d.constructor === Number ? new Date(d) :
                d.constructor === String ? new Date(d) :
                typeof d === "object" ? new Date(d.year, d.month, d.date) :
                NaN
            );
        }
        else {
            return NaN;
        }
    },
    compare: function (a, b) {
                                                         return (
            isFinite(a = this.convert(a).valueOf()) &&
            isFinite(b = this.convert(b).valueOf()) ?
            (a > b) - (a < b) :
            NaN
        );
    },
    inRange: function (d, start, end) {
                                                  return (
            isFinite(d = this.convert(d).valueOf()) &&
            isFinite(start = this.convert(start).valueOf()) &&
            isFinite(end = this.convert(end).valueOf()) ?
            start <= d && d <= end :
            NaN
        );
    }
};

function UpdateTitle(){
    if (notificationsNumber > 0) {
        $('title').text($('title').text() + " (" + notificationsNumber + ")");
    }
    else {
        $('title').text(originaltitle);
    }
}