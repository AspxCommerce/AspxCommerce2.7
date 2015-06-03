var HeaderControl = "";
$(function () {   
    $("#lnkCheckOut").html("<i class='i-checkout'></i>"+getLocale(AspxHeaderControl, "Checkout"));
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID(),
        SessionCode: AspxCommerce.utils.GetSessionCode()
    };
    var myAccountURL = myAccountURLSetting;
    var shoppingCartURL = shoppingCartURLSetting;
    var wishListURL = "My-WishList";
    var frmLoginCheck = frmLogin;

    HeaderControl = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",//"AspxCommerceWebService.asmx/",
            url: "",
            method: "",
            ajaxCallMode: ""
        },

        vars: {
            totalPrice: "",
            itemCount: ""
        },

        ajaxCall: function (config) {
            $.ajax({
                type: HeaderControl.config.type,
                contentType: HeaderControl.config.contentType,
                cache: HeaderControl.config.cache,
                async: HeaderControl.config.async,
                data: HeaderControl.config.data,
                dataType: HeaderControl.config.dataType,
                url: HeaderControl.config.url,
                success: HeaderControl.config.ajaxCallMode,
                error: HeaderControl.ajaxFailure
            });
        },

        init: function () {
            if (headerType.toLowerCase() == 'horizontal') {
                $(".sfHeaderDropdown").remove();
                $(".cssClassLoginStatusWrapper").show();
            }
            else {
                var htmToAppend = $(".cssClassLoginStatusInfo ul").html();
                $(".cssClassLoginStatusInfo ul").remove();
                $(".sfHeaderDropdown dt").append("<a id=\"#\" class=\"sfLocale\"><i class='i-account'></i>Account</a><b></b>");
                $(".sfHeaderDropdown").show();
                $(".sfHeaderDropdown").find('ul').html(htmToAppend);
            }
            $("#lnkMyAccount").html("<i class='i-user'></i>" + getLocale(AspxHeaderControl, "My Account"));
            if (allowAddToCart.toLowerCase() == 'true') {
                $(".cssClassCheckOut").show();
            }
            else {
                $(".cssClassCheckOut").hide();
            }

            HeaderControl.vars.itemCount = cartItemCount;
            if (allowAddToCart.toLowerCase() == 'true') {
                if (frmLoginCheck.toLowerCase() == "true" && loginMessageInfo.toLowerCase() == 'true' && loginMessageInfoCount == 1) {
                    if (HeaderControl.vars.itemCount > 0) {
                        var properties = {
                            onComplete: function (e) {
                                if (e) {
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL + pageExtension;
                                }
                            }
                        };
                        csscody.messageInfo("<h2>" + getLocale(AspxHeaderControl, "Notice Information") + "</h2><p>" + getLocale(AspxHeaderControl, "Your cart contains items. Do you want to look at them?") + "</p>", properties);
                    }
                }
            }


            if (aspxCommonObj.CustomerID > 0 && aspxCommonObj.UserName.toLowerCase() != 'anonymoususer') {
                $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myAccountURL + pageExtension);
            } else {
                $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + LogInURL + pageExtension + '?ReturnUrl=' + AspxCommerce.utils.GetAspxRedirectPath() + myAccountURL + pageExtension);
            }
            $("#lnkProceedToSingleCheckout , #lnkProceedToSingleChkout,#lnkCheckOut").click(function () {
                if (IsABTestInstalled.toLowerCase() == "true") {
                    ABTest.ABTestSaveConversion();
                }
                if (IsKPIInstalled.toLowerCase() == "true") {
                    KPICommon.KPISaveConversion('My Cart');
                }

                if ($(".cssClassBlueBtn ").length > 0) {
                    if (CartAPI.GetTotal() < 0) {
                        csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You can't proceed to checkout your amount is not applicable!") + "</p>");
                        return false;
                    }
                }
                var totalCartItemPrice = HeaderControl.GetTotalCartItemPrice();
                if (totalCartItemPrice == 0) {
                    csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You have not added any items in cart yet!") + "</p>");
                    if (typeof (AspxCart) == 'object') {
                        AspxCart.GetUserCartDetails();
                    }
                    return false;
                }
                if (totalCartItemPrice < minCartSubTotalAmountSetting) {
                    csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You are not eligible to proceed further. Your order amount must be at least") + "<span class='cssClassFormatCurrency'>" + minCartSubTotalAmountSetting + "</span></p>");

                } else {
                    var singleAddressLink = '';
                    singleAddressLink = singleAddressChkOutURL + pageExtension;
                    if (aspxCommonObj.CustomerID > 0 && aspxCommonObj.UserName.toLowerCase() != 'anonymoususer') {
                        if (IsABTestInstalled.toLowerCase() == "true") {
                            ABTest.ABTestVisitOnPreviousPage(singleAddressLink);
                        } else {
                            window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                        }

                    } else {
                        if (allowAnonymousCheckOutSetting.toLowerCase() == 'true') {
                            window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxHeaderControl, 'Information Alert') + '</h2><p>' + getLocale(AspxHeaderControl, 'Checkout is not allowed for anonymous user!') + '</p>');
                        }
                    }

                }

            });
        },

        GetCartItemTotalCount: function () {
            HeaderControl.config.url = HeaderControl.config.baseURL + "GetCartItemsCount";
            HeaderControl.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            HeaderControl.config.ajaxCallMode = HeaderControl.BindCartItemsCount;
            HeaderControl.config.async = false;
            HeaderControl.ajaxCall(this.config);
        },

        GetTotalCartItemPrice: function () {
            this.config.url = this.config.baseURL + "GetTotalCartItemPrice";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = HeaderControl.SetTotalCartItemPrice;
            this.config.async = false;
            this.ajaxCall(this.config);
            return HeaderControl.vars.totalPrice;
        },

        BindCartItemsCount: function (msg) {
            var shoppingCartURLLink = '';
            shoppingCartURLLink = shoppingCartURL + pageExtension;
            $("#lnkMyCart").html(getLocale(AspxHeaderControl, "My Cart") + " <span class=\"cssClassTotalCount\"> [" + msg.d + "]</span>");
            HeaderControl.vars.itemCount = msg.d;
            if (msg.d == 0) {
                frmLoginCheck = "false";
            }
            if (frmLoginCheck.toLowerCase() == "true" && loginMessageInfo.toLowerCase() == 'true' && loginMessageInfoCount == 1) {
                if (msg.d > 0) {
                    var properties = {
                        onComplete: function (e) {
                            if (e) {
                                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL;
                            }
                        }
                    };
                    csscody.messageInfo("<h2>" + getLocale(AspxHeaderControl, "Notice Information") + "</h2><p>" + getLocale(AspxHeaderControl, "Your cart contains items. Do you want to look at them?") + "</p>", properties);
                }
            }
            $("#lnkMyCart").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURLLink + '');

        },
        SetTotalCartItemPrice: function (msg) {
            HeaderControl.vars.totalPrice = msg.d;
        }
    };
    HeaderControl.init();
});