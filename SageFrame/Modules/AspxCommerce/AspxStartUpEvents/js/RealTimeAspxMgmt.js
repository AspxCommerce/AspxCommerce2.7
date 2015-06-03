var realTimeEvent;
$(document).ready(function () {
    var sessionID = AspxCommerce.utils.GetSessionCode();
    var storeID = AspxCommerce.utils.GetStoreID();
    var portalID = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var stored_cookie = sessionID + "-" + storeID + "-" + portalID + "-" + userName;
    createCookie("session_aspxCommonInfo_cookie", stored_cookie);
});

(function ($) {
       realTimeEvent = $.connection._aspxrthub;
    
    var realTimeMgmt = {
        MakeConnection: function () {
            $.connection.hub.start().done(function () {
            }
            ).fail(function (e) {
                $.connection.hub.start();
            });
            $.connection.hub.error(function () {
                 $.connection.hub.stop();
            });
            $.connection.hub.stateChanged(function (change) {
                if (change.newState === $.signalR.connectionState.reconnecting) {
                   
                }
                else if (change.newState === $.signalR.connectionState.connected) {
                    
                }
           });

        },
        BindEvents: function () {
            realTimeEvent.client.BindOutOfStock = function (isOutOfStock, itemID, itemSKU) {
                if (isOutOfStock) {
                    var id = "addtocart" + itemID;
                    $("button[addtocart=" + id + "]").each(function () {
                        if ($("#dvCompareList").length > 0) {
                            location.reload();
                            return false;
                        }
                        var $parent = $(this).parents('.sfButtonwrapper');
                        $parent.addClass("cssClassOutOfStock");
                        $parent.html('');
                        $parent.append("<button type='button'><span>Out Of Stock</span></button>");

                    });
                  
                    if (($(".tblGroupedItem").html() == "")||($(".tblGroupedItem").html() == undefined)) {
                        if ($("#dynItemDetailsForm").length > 0) {
                            var $btnWrappr = $("#addWishListThis").get(0).attributes.value;
                            var ItemID = parseInt($btnWrappr.nodeValue.match(/\d+/));
                                if (ItemID == itemID) {
                                    ItemDetail.BindItemBasicByitemSKU(itemSKU);
                                    return false;
                            }
                        }
                    }
                    else {
                     var isToChangeAddToCart = false;
                        var totalItemInGroupCounter = 0;
                        var totalDisabledItemCounter = 0;
                        $(".tblGroupedItem >tbody >tr").each(function () {
                            if ($(this).find("input[type='checkbox']").data().itemid == ItemID) {
                                $(this).find("input,textbox").prop("disabled", true);
                                $(this).addClass("disabledGroupItm");
                                isToChangeAddToCart = true;
                            }

                        });
                        if (isToChangeAddToCart == true) {
                            $(".tblGroupedItem >tbody >tr").each(function () {
                                if ($(this).hasClass("disabledGroupItm")) {
                                    totalDisabledItemCounter++;
                                }
                                totalItemInGroupCounter++;
                            });
                            if (totalItemInGroupCounter == totalDisabledItemCounter) {
                                if ($("#btnAddToMyCart").hasClass("addtoCart ")) {
                                    $("#btnAddToMyCart").prop("disabled", true);
                                    $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
                                    $("#btnAddToMyCart").addClass("cssClassOutOfStock");
                                    $("#btnAddToMyCart").removeClass('addtoCart ');
                                    $("#btnAddToMyCart").find("span").html("out of stock");
                                    $("#btnAddToMyCart").prop({ "style": "" });
                                }
                            }
                        }
                    }

                }
                               else {
                    $('.sfButtonwrapper').each(function () {
                        try {
                            var ItemID = parseInt($(this).data().itemid);
                            if (itemID == ItemID) {
                                if ($(this).hasClass('cssClassOutOfStock')) {
                                    $(this).removeClass("cssClassOutOfStock");
                                    $(this).html('');

                                    $(this).append("<label class='i-cart cssClassCartLabel cssClassGreenBtn'><button>Cart +</button></label>");
                                                                       if ($("#dvCompareList").length > 0) {
                                        location.reload();
                                        return false;
                                    }
                                    var Attrbs = [];
                                    $.each($(this).data(), function (attrb, attrbvalue) {
                                        Attrbs.push({
                                            key: attrb,
                                            value: attrbvalue
                                        });

                                    })
                                    var $btn = $(this).find('button');
                                    $.each(Attrbs, function (attrbIndex, attrbs) {

                                        $btn.attr(attrbs.key, attrbs.value);
                                    });
                                }
                            }
                        }
                        catch (exception) {
                            return true;
                        }
                    });
                   
                    if (($(".tblGroupedItem").html() == "")||($(".tblGroupedItem").html() == undefined))  {
                        if ($("#dynItemDetailsForm").length > 0) {
                            var $btnWrappr = $("#addWishListThis").get(0).attributes.value;
                            var itemID = parseInt($btnWrappr.nodeValue.match(/\d+/));
                            if (itemID == ItemID) {
                                ItemDetail.BindItemBasicByitemSKU(itemSKU);
                            }
                                                   }
                    }
                    else {
                        var isToChangeOutofStock = false;
                        $(".tblGroupedItem >tbody >tr").each(function () {
                            if ($(this).hasClass("disabledGroupItm")) {
                                if ($(this).find("input[type='checkbox']").data().itemid == ItemID) {
                                    $(this).find("input,textbox").prop("disabled", false);
                                    $(this).removeClass("disabledGroupItm");
                                    isToChangeOutofStock = true;
                                }
                            }
                        });
                        if (isToChangeOutofStock == true) {
                            if ($("#btnAddToMyCart").hasClass("cssClassOutOfStock")) {
                                $("#btnAddToMyCart").prop("disabled", false);
                                $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
                                $("#btnAddToMyCart").removeClass("cssClassOutOfStock");
                                $("#btnAddToMyCart").addClass('addtoCart ');
                                $("#btnAddToMyCart").find("span").html("cart+");
                                $("#btnAddToMyCart").prop({ "style": "display: block;" });
                            }
                        }
                    }
                }

            };
            realTimeEvent.client.BindAddToCart = function (ItemID, itemSKU) {
                $('.sfButtonwrapper').each(function () {
                    try {
                        var itemID = parseInt($(this).data().itemid);
                        if (itemID == ItemID) {
                            if ($(this).hasClass('cssClassOutOfStock')) {
                                $(this).removeClass("cssClassOutOfStock");
                                $(this).html('');
                                $(this).append(" <label class='i-cart cssClassCartLabel cssClassGreenBtn'><button>Cart +</button></label>");
                                                               if ($("#dvCompareList").length > 0) {
                                    location.reload();
                                    return false;
                                }
                                var Attrbs = [];
                                $.each($(this).data(), function (attrb, attrbvalue) {
                                    Attrbs.push({
                                        key: attrb,
                                        value: attrbvalue
                                    });

                                })
                                var $btn = $(this).find('button');
                                $.each(Attrbs, function (attrbIndex, attrbs) {
                                    $btn.attr(attrbs.key, attrbs.value);
                                });


                            }
                        }
                    }
                    catch (exception) {
                        return true;
                    }
                });

                if (($(".tblGroupedItem").html() == "")||($(".tblGroupedItem").html() == undefined)) {
                    if ($("#dynItemDetailsForm").length > 0) {
                        var $btnWrappr = $("#addWishListThis").get(0).attributes.value;
                        var itemID = parseInt($btnWrappr.nodeValue.match(/\d+/));
                        if (itemID == ItemID) {
                            ItemDetail.BindItemBasicByitemSKU(itemSKU);
                        }
                                           }
                }
                else {
                    var isToChangeOutofStock = false;
                    $(".tblGroupedItem >tbody >tr").each(function () {
                        if ($(this).hasClass("disabledGroupItm")) {
                            if ($(this).find("input[type='checkbox']").data().itemid == ItemID) {
                                $(this).find("input,textbox").prop("disabled", false);
                                $(this).removeClass("disabledGroupItm");
                                isToChangeOutofStock = true;
                            }
                        }
                    });
                    if (isToChangeOutofStock == true) {
                        if ($("#btnAddToMyCart").hasClass("cssClassOutOfStock")) {
                            $("#btnAddToMyCart").prop("disabled", false);
                            $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
                            $("#btnAddToMyCart").removeClass("cssClassOutOfStock");
                            $("#btnAddToMyCart").addClass('addtoCart ');
                            $("#btnAddToMyCart").find("span").html("cart+");
                            $("#btnAddToMyCart").prop({ "style": "display: block;" });
                        }
                    }
                }
            };
            realTimeEvent.client.BroadCastDisconnected = function () {
                           };
            realTimeEvent.client.NotificationGetAllSuccess = function (data) {
                var count = 0;
                if (data.UsersInfoCount !== 0) {
                    $("#spanUsersInfo").html(data.UsersInfoCount);
                    $("#spanUsersInfo").show();
                    count += parseInt(data.d.UsersInfoCount);
                }
                else {
                    $("#spanUsersInfo").hide();
                }
                if (data.ItemsInfoCount !== 0) {
                    $("#spanItemsInfo").html(data.ItemsInfoCount);
                    $("#spanItemsInfo").show();
                    count += parseInt(data.d.ItemsInfoCount);
                }
                else {
                    $("#spanItemsInfo").hide();
                }
                if (data.NewOrdersCount !== 0) {
                    $("#spanOrdersInfo").html(data.NewOrdersCount);
                    $("#spanOrdersInfo").show();
                    count += parseInt(data.d.NewOrdersCount);
                }
                else {
                    $("#spanOrdersInfo").hide();
                }
                notificationsNumber -= parseInt(data.d.length);
                UpdateTitle();
            };
         realTimeEvent.client.MaxConnectionTimeOut = function (isMax) {
                if (isMax==true)
                   {
                        $.connection.stop();
                    }
         };

        },
        Init: function () {
            realTimeMgmt.MakeConnection();
            realTimeMgmt.BindEvents();
        }

    };
    realTimeMgmt.Init();
})(jQuery);

(function ($) {
    var refreshKeyPressed = false;
    var modifierPressed = false;

    var f5key = 116;
    var rkey = 82;
    var modkey = [17, 224, 91, 93];

       $(document).bind(
        'keydown',
        function (evt) {
                       if (evt.which == f5key || window.modifierPressed && evt.which == rkey) {
                refreshKeyPressed = true;
            }

                       if (modkey.indexOf(evt.which) >= 0) {
                modifierPressed = true;
            }

            if (refreshKeyPressed || modifierPressed) {
                $.connection.hub.stop();
            }
        }
    );

    $(window).bind('beforeunload', function(){ 
      $.connection.hub.stop();
    });
       $(document).bind(
        'keyup',
        function (evt) {
                       if (evt.which == f5key || evt.which == rkey) {
                refreshKeyPressed = false;
            }

                       if (modkey.indexOf(evt.which) >= 0) {
                modifierPressed = false;
            }

            if (refreshKeyPressed || modifierPressed) {
                $.connection.hub.stop();
            }
        }
    );
} (jQuery));

function UpdateNotifications(caseValue){
    try {
        if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
            $.connection.hub.start();
        }
        var portalID = AspxCommerce.utils.GetPortalID();
        var storeID = AspxCommerce.utils.GetStoreID();
        var itemOnCart = $.connection._aspxrthub;        

               itemOnCart.server.notificationUsersGetAll(storeID, portalID, caseValue);
    } 
    catch (e) {

    }
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}