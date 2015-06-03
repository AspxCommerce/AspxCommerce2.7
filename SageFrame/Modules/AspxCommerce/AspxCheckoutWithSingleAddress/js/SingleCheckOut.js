
var CheckOutApi = "";
; (function ($) {

    var $securepost = function (method, param, successFx, error) {
        $.ajax({
            type: "POST",
            async: true,
            url: aspxservicePath + 'securepost.ashx?call=' + method,
            data: param,
            success: successFx,
            error: error
        });
    };

    $.checkoutView = function (option) {
        option = $.extend({
            Coupon: '',
            Items: '[]',
            ServerVars: '',
            InitScript: '',
            cartCount: '',
            RewardSettings: ''

        }, option);

        var p = $.parseJSON(option.ServerVars);
        p.Coupon = $.parseJSON(option.Coupon);
        p.Items = $.parseJSON(option.Items);
        p.GiftCard = $.parseJSON(option.GiftCard);
        p.InitScript = option.InitScript;
        var RootPath = AspxCommerce.utils.GetAspxRootPath();

        var selectedBillingAddressBeforeAddingNewAddress = "";

        var $accor = '';
        var isGiftCardUsed = false;
        var temptotal = 0;
        var tempUserAddresses = [];
        var basketItems = [];
        var rateAjaxRequest = null;
        $.expr[':'].exactcontains = function (a, i, m) {

            return $.trim($(a).text()).match("^" + m[3] + "$");
        };
        $.validator.addMethod("alpha_dash", function (value, element) {
            return this.optional(element) || /^[a-z0-9_ \-]+$/i.test(value);
        });
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                CustomerID: AspxCommerce.utils.GetCustomerID(),
                SessionCode: AspxCommerce.utils.GetSessionCode()
            };
            return aspxCommonInfo;
        };
        var taxDetail = "";

        var cartCalculator = {
            OtherAmount: [],
            OperationType: {
                add: "add",
                minus: "minus"
            },
            Items: p.Items,

            CartTax: [],
            SubTotal: 0,
            getSubTotalWithTax: function () {
                var subtotal = 0;

                $.each(cartCalculator.Items, function (index, item) {

                    subtotal += item.Quantity * (parseFloat(item.Price).toFixed(2) + parseFloat(item.UnitTax).toFixed(2));
                });
                return subtotal;
            },
            getSubTotal: function () {
                var subtotal = 0;

                $.each(cartCalculator.Items, function (index, item) {
                    subtotal += item.Quantity * (parseFloat(item.Price).toFixed(2));
                });
                return subtotal;

            },
            ItemTax: function () {
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };
                gettotalTax = function () {
                    var totalTax = 0;
                    taxDetail = "";
                    $.each(cartCalculator.CartTax, function (index, value) {
                        if (value.TaxRateValue > 0) {
                            taxDetail += '<label><span class="sfLocale">' + value.TaxManageRuleName + ':' + '</span>';
                            totalTax += value.TaxRateValue;
                            taxDetail += '<span class="cssClassFormatCurrency sfLocale">' + parseFloat(value.TaxRateValue).toFixed(2) + '</span></label><br/>';
                        }
                    });
                    return totalTax;

                };

                var getItem = function (itemId) {
                    var item = {};
                    for (var i = 0; i < cartCalculator.Items.length; i++) {

                        if (cartCalculator.Items[i].ItemID == itemId) {
                            item = cartCalculator.Items[i]; break;
                        }

                    }
                    return item;

                };

                assignItemTax = function (itemId, tax) {
                    var item = getItem(itemId);
                    if (item.ItemID)
                        item.UnitTax = tax;

                }
                calcTax = function () {
                    var totalTax = gettotalTax();
                    $securepost("STx", {
                        v: totalTax
                    }, function (data) { }, function () { });

                    cartCalculator.AddOtherAmount("Total Tax:", totalTax, cartCalculator.OperationType.add, 3);

                };

                return { Add: assignItemTax, Get: gettotalTax, Calculate: calcTax, GetTaxDetail: taxDetail };

            }(),
            Clear: function () {
                this.OtherAmount = [];
            },
            GiftCard: function () {

                var $ajaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };
                var recentlyUsedGiftCard = p.GiftCard;
                var cartTotal = 0;
                var tempPayment;
                var parsestrToBool = function (input) {
                    try {
                        return !!$.parseJSON(input.toLowerCase());
                    } catch (e) {
                    }
                };

                var checkGiftCardUsed = function () {
                    var allowtocheckout = true;
                    for (var i = 0; i < recentlyUsedGiftCard.length; i++) {
                        if (allowtocheckout == false)
                            break;
                        var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), giftCardCode: recentlyUsedGiftCard[i].GiftCardCode, amount: recentlyUsedGiftCard[i].ReducedAmount });
                        $ajaxCall("CheckGiftCardUsed", param, function (data) {
                            allowtocheckout = data.d;
                            if (allowtocheckout == false) {
                                resetGiftCard();
                            }
                        });

                    }
                    return allowtocheckout;
                };

                var addGiftCardInPayment = function (isTotalZero, restamount) {
                    if (isTotalZero) {
                        tempPayment = $("#dvPGList").clone();
                        $("#dvinfo").remove();
                        var gC = $("input[name=PGLIST][realname='Gift Card']").parents('label:eq(0)');
                        $("#dvPGList").find('label').not($("input[name=PGLIST][realname='Gift Card']").parents('label:eq(0)')).remove();
                        $("#dvPGList br").remove();
                        $("input[name=PGLIST][realname='Gift Card']").trigger('click');
                        var dvInfo = $("<div id=dvinfo>").html("<p>" + getLocale(AspxCheckoutWithSingleAddress, 'Gift Card has been applied to your cart') + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, 'your total due amount') + '<span class="cssClassFormatCurrency"> ' + restamount + "</span>  .</p>");
                        if ($("#dvinfo").length == 0) {
                            $("#dvPaymentInfo").append(dvInfo);
                        }
                        $("#dvPaymentInfo").find('div.sfFormwrapper').hide();
                        $("#btnPlaceOrderGiftCard").parents('div:eq(0)').find('label.cssClassGreenBtn').not($("#btnPlaceOrderGiftCard").parents('label:eq(0)')).remove();

                    } else {
                        $("#btnPlaceOrderGiftCard").parents('label:eq(0)').remove();
                        $("#dvPaymentInfo").find('div.sfFormwrapper').show();
                        $("#dvinfo").remove();
                        var dvInfo = $("<div id=dvinfo>").html("<p>" + getLocale(AspxCheckoutWithSingleAddress, 'Gift Card has been applied to your cart') + " </p><p>" + getLocale(AspxCheckoutWithSingleAddress, 'your total due amount') + "<span class='cssClassFormatCurrency'> " + restamount + "</span> .</p><p>" + getLocale(AspxCheckoutWithSingleAddress, 'To checkout you can still apply giftcard or choose another payment option') + "</p>");
                        if ($("#dvinfo").length == 0) {
                            $("#dvPaymentInfo").append(dvInfo);
                        }
                    }
                };


                var getGiftCardSesion = function () {
                    CheckOut.UserCart.IsGiftCardUsed = false;
                    $securepost("GGC", {}, isGiftCardUsedf, function () { });
                };

                var isGiftCardUsedf = function (data) {
                    $(".cssClassGiftCard").remove();
                    recentlyUsedGiftCard = [];
                    cartTotal = 0;
                    if (data.d != "" && data.d != null) {
                        var infos = $.parseJSON(data.d);
                        $(".cssClassGiftCard").remove();
                        recentlyUsedGiftCard = [];
                        CheckOut.UserCart.IsGiftCardUsed = true;
                        $.each(infos, function (index, value) {
                            CheckOut.UserCart.IsGiftCardUsed = true;
                            recentlyUsedGiftCard.push(value);
                            var id = setTimeout(function () {
                                applyGiftCardFromSession(recentlyUsedGiftCard[index]);
                            }, 200);
                        });
                    } else {
                        CheckOut.UserCart.IsGiftCardUsed = false;
                        $("#btnPlaceOrderGiftCard").parents("label:eq(0)").remove();
                    }
                };

                var checkUsed = function (arr, code) {
                    var isExist = false;
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].GiftCardCode == code) {
                            isExist = true;
                            break;
                        }
                    }
                    return isExist;
                };

                var deleteUsed = function (arr, code) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].GiftCardCode == code) {
                            arr.splice(i, 1);
                            break;
                        }
                    }
                };

                var getGiftCardType = function (itemid) {
                    var type;
                    var aspxCommonInfo = aspxCommonObj();
                    aspxCommonInfo.UserName = null;
                    aspxCommonInfo.CultureName = null;
                    aspxCommonInfo.SessionCode = null;
                    aspxCommonInfo.CustomerID = null;
                    var param = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, itemId: itemid });
                    $ajaxCall("GetGiftCardType", param, function (data) {
                        type = data.d;
                    },
                        null);
                    return type;
                };

                var getTotalGiftCardAmount = function () {
                    var total = 0;
                    for (var i = 0; i < recentlyUsedGiftCard.length; i++) {
                        total += parseFloat(recentlyUsedGiftCard[i].ReducedAmount);
                    }
                    return total;
                };

                var getReducedAmount = function (arr, code) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].GiftCardCode == code) {
                            return arr[i].ReducedAmount;
                            break;
                        }
                    }
                    return 0;
                };

                var resetGiftCard = function () {
                    $securepost("RGC", {}, function () { }, function () { });
                    recentlyUsedGiftCard = [];
                    $("#dvinfo").remove();
                    CheckOut.BindFunction();
                    $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                    $accor.tabs('option', 'active', 4);
                    $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                    csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Amount has been reduced from your gift card by other user!.") + "</p>");

                };

                var applyGiftCardFromSession = function (veriftication, cartTotal) {
                    var showOtherPaymnetOption = false;
                    var $td = $("<td>");
                    var $content = $("<strong>").append("GiftCard (" + veriftication.GiftCardCode + ")");
                    var $span = $("<span>").addClass("cssClassDeleteGiftCard").attr('data-id', veriftication.GiftCardCode).append("<sup><a href='#'>x</a></sup>");

                    $span.bind("click", function () {
                        var code = $.trim($(this).attr('data-id'));
                        deleteUsed(recentlyUsedGiftCard, code);
                        $(this).parents("tr:eq(0)").remove();
                        CheckOut.UserCart.GiftCardDetail = recentlyUsedGiftCard;

                        $securepost("SGC", {
                            v: JSON.stringify(recentlyUsedGiftCard)
                        }, function (data) { }, function () { });


                        csscody.info("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Gift Card removed successfully!") + "</p>");
                        $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                        $accor.tabs('option', 'active', 4);
                        $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                        CheckOut.LoadPGatewayList();
                        $('#dvPGList input[name="PGLIST"]:first').trigger('click');
                        getGiftCardSesion();
                        $("#btnPlaceOrderGiftCard").remove();
                        $("#dvinfo").remove();

                    });

                    $td.append($content.append($span));

                    var $tdamount = $("<td>").append("<span class=\"cssClassNegative\">-</span><label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(veriftication.ReducedAmount).toFixed(2) + "</label>");
                    if (cartTotal == 0) {
                        showOtherPaymnetOption = false;
                        addGiftCardInPayment(true, 0);
                    } else {
                        if (cartTotal != undefined) {
                            showOtherPaymnetOption = true;
                            addGiftCardInPayment(false, parseFloat(cartTotal).toFixed(2));
                        }
                    }
                    CheckOut.UserCart.GiftCardDetail = recentlyUsedGiftCard;
                    if (recentlyUsedGiftCard.length > 0) {
                        $securepost("SGC", {
                            v: JSON.stringify(recentlyUsedGiftCard)
                        }, function (data) { }, function () { });
                    }

                    return $("<tr>").addClass("cssClassGiftCard cssClassSubTotalAmount").append($td).append($tdamount);

                };

                var init = function () {
                    getGiftCardSesion();
                };
                var clearGiftCard = function () {
                    var param = JSON2.stringify({ key: "UsedGiftCard", value: [] });
                    $ajaxCall("SetSessionVariable", param, null, null);
                };
                var getglist = function () {
                    return recentlyUsedGiftCard;
                };
                var add = function (giftcards) {

                    recentlyUsedGiftCard = giftcards;
                };
                var apply = function () {
                    var html = "";
                    var nodes = [];
                    var cartTotal = cartCalculator.Calculate();
                    $.each(recentlyUsedGiftCard, function (index, value) {
                        CheckOut.UserCart.IsGiftCardUsed = true;
                        var $tr = applyGiftCardFromSession(recentlyUsedGiftCard[index], parseFloat(cartTotal).toFixed(2));
                        nodes.push($tr);
                    });
                    return nodes;

                };
                return {
                    Init: init,
                    GiftCardList: getglist,
                    GetGiftCardAmount: getTotalGiftCardAmount,
                    GetGiftCardType: getGiftCardType,
                    CheckGiftCardIsUsed: checkGiftCardUsed,
                    ClearGiftCard: clearGiftCard,
                    ResetGiftCard: resetGiftCard,
                    Hold: add, Apply: apply
                };
            }(),

            Calculate: function () {

                var giftcardAmt = function () {
                    var giftcardAmount = 0;
                    var appliedgiftcards = cartCalculator.GiftCard.GiftCardList();
                    if (appliedgiftcards.length > 0) {
                        for (var i = 0; i < appliedgiftcards.length; i++) {
                            giftcardAmount += parseFloat(appliedgiftcards[i].ReducedAmount);
                        }
                    }
                    return giftcardAmount;

                };

                var subtotal = cartCalculator.getSubTotal();
                var other = cartCalculator.CalculateOther();
                if (subtotal + other <= 0) {

                    this.Clear();
                    try {
                        CheckOut.Coupon.Reset();
                        CheckOut.Discount.Init();
                    } catch (e) {

                    }

                    var subtotal = cartCalculator.getSubTotal();
                    var other = cartCalculator.CalculateOther();

                    var grandtotal = subtotal + other - giftcardAmt();

                    return grandtotal;
                }
                else {
                    var grandtotal = subtotal + other - giftcardAmt();
                    return grandtotal;
                }

            },
            GetIndex: function (key) {

                var data = {
                    index: -1,
                    value: ""
                };
                var len = this.OtherAmount.length;
                for (var i = 0; i < len; i++) {

                    if (this.OtherAmount[i].key == key) {
                        data = {
                            index: i,
                            value: cartCalculator.OtherAmount[i].value
                        };
                        break;
                    }

                }

                return data;

            },
            AddOtherAmount: function (key, value, type, order) {

                var otherAmount = {
                    key: key,
                    value: value,
                    operationType: type, order: order
                };
                var info = cartCalculator.GetIndex(key);
                if (info.index == -1) {
                    cartCalculator.OtherAmount.push(otherAmount);
                } else {
                    cartCalculator.OtherAmount[info.index].value = value;
                    cartCalculator.OtherAmount[info.index].operationType = type;
                }
            },
            AddOtherAmountAuto: function (key, value, type) {

                var otherAmount = {
                    key: key,
                    value: value,
                    operationType: type, order: cartCalculator.OtherAmount.length + 1
                };
                var info = cartCalculator.GetIndex(key);
                if (info.index == -1) {
                    cartCalculator.OtherAmount.push(otherAmount);
                } else {
                    cartCalculator.OtherAmount[info.index].value = value;
                    cartCalculator.OtherAmount[info.index].operationType = type;
                }
            },
            CalculateOther: function () {
                var total = 0;
                $.each(cartCalculator.OtherAmount, function (index, item) {

                    if (cartCalculator.OperationType.add == item.operationType) {

                        total += item.value;
                    } else {
                        total -= item.value;
                    }

                });
                return total;
            },
            ShowDummyData: function () {


            },
            ShowData: function () {
                var rows = "";
                rows += "<tr><td><strong class=\"sfLocale\">" + getLocale(AspxCheckoutWithSingleAddress, "Sub Total:") + "</strong></td><td><label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(this.getSubTotal()).toFixed(2) + "</label></td></tr>";
                this.OtherAmount.sort(function (a, b) {
                    return a.order - b.order;
                });
                $.each(this.OtherAmount, function (index, item) {
                    var localekey = getLocale(AspxCheckoutWithSingleAddress, item.key);

                    rows += "<tr><td><strong class=\"sfLocale\">" + localekey + "</strong></td>";
                    if (cartCalculator.OperationType.add == item.operationType) {
                        rows += "<td>";
                    } else {
                        rows += "<td><span class=\"cssClassNegative\">-</span>";
                    }
                    if (item.order == 3) {
                        if (taxDetail != "") {
                            rows += taxDetail;
                        }
                        else {
                            rows += "<span class=\"cssClassFormatCurrency\">0.00</span>";
                        }
                        rows += "</td></tr>";
                    }
                    else {
                        rows += "<label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(item.value).toFixed(2) + "</label></td></tr>";
                    }

                });

                $(".cssClassSubTotalAmount").html(rows);

                var trs = cartCalculator.GiftCard.Apply();

                if (trs.length > 0) {

                    for (var z = 0; z < trs.length; z++) {
                        $(".cssClassSubTotalAmount tbody").append(trs[z]);
                    }

                }
                var _total = this.Calculate();
                $securepost("SGT", {
                    v: _total
                }, function (data) { }, function () { });
                var cookieCurrency = Currency.cookie.read();
                var currentCurrency = '';
                if (cookieCurrency == null || cookieCurrency == BaseCurrency) {
                    currentCurrency = BaseCurrency;
                }
                else {
                    currentCurrency = cookieCurrency;
                }
                $("#lblTotalCost").removeAttr("data-currency");
                $("#lblTotalCost").removeAttr("data-currency-" + currentCurrency + "");
                $("#lblTotalCost").attr('bc', parseFloat(_total).toFixed(2));
                $("#lblTotalCost").text(parseFloat(_total).toFixed(2));
                var cookieCurrency = $("#ddlCurrency").val();
                Currency.currentCurrency = BaseCurrency;
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
            },
            InstanceAPI: function () {

                CartAPI = function (cart) {

                    return {
                        GetTotal: cart.Calculate,
                        GetSubTotal: cart.getSubTotal
                    };
                }(this);
            }

        };

        cartCalculator.InstanceAPI();


        var CheckOut = {
            BillingAddress: {
                AddressID: 0,
                FirstName: "",
                LastName: "",
                CompanyName: "",
                EmailAddress: "",
                Address: "",
                Address2: "",
                City: "",
                State: "",
                Zip: "",
                Country: "",
                Phone: "",
                Mobile: "",
                Fax: "",
                Website: "",
                IsDefaultBilling: false,
                IsBillingAsShipping: false
            },
            SetTempAddr: function (add) {
                tempUserAddresses = add;
            },
            SetBasketItems: function (arr) {
                basketItems = arr;
            },
            ShippingAddress: {
                AddressID: 0,
                FirstName: "",
                LastName: "",
                CompanyName: "",
                EmailAddress: "",
                Address: "",
                Address2: "",
                City: "",
                State: "",
                Zip: "",
                Country: "",
                Phone: "",
                Mobile: "",
                Fax: "",
                Website: "",
                isDefaultShipping: false
            },
            Vars: {
                temptotal: 0,
                GatewayName: "",
                ItemIDs: "",
                Tax: 0,
                AddressID: 0,
                Country: "",
                State: "",
                Zip: "",
                len: 0,
                CostVariantsValueIDs: "",
                checkIfExist: ""
            },
            UserCart: {
                ShowShippingAdd: false,
                isUserGuest: true,
                isActive: true,
                IsFShipping: false, myAccountURL: p.myAccountURL,
                CartDiscount: 0,
                TotalDiscount: p.Discount,
                IsDownloadItemInCart: false,
                IsDownloadItemInCartFull: false,
                CountDownloadableItem: 0,
                CountAllItem: 0,
                paymentMethodName: "",
                paymentMethodCode: "",
                shippingRate: 0,
                amount: 0,
                baseSubTotalAmt: 0.00,
                lstItems: [],
                spAddressID: 0,
                spMethodID: 0,
                spCost: 0,
                ID: 0,
                qty: 0,
                Tax: 0,
                price: 0,
                weight: 0,
                CartID: 0,
                ItemType: '',
                GiftCardDetail: [],
                CartItemId: 0,
                objTaxList: [],
                spName: "",
                RewardPointsDiscount: 0,
                UsedRewardPoints: 0,
                RewardedPoints: 0,
                IsPurchaseActive: false
            },
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: "", error: "",
                sessionValue: ""
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: CheckOut.config.type,
                    contentType: CheckOut.config.contentType,
                    cache: CheckOut.config.cache,
                    async: CheckOut.config.async,
                    url: CheckOut.config.url,
                    data: CheckOut.config.data,
                    dataType: CheckOut.config.dataType,
                    success: CheckOut.config.ajaxCallMode,
                    error: CheckOut.config.error
                });
            },

            CheckDownloadableOnlyInCart: function () {
                if (CheckOut.UserCart.IsDownloadItemInCart) {
                    if (AspxCommerce.utils.GetUserName() == 'anonymoususer') {
                        $('.cssClassCheckOutMethodLeft p:first').html('').html(getLocale(AspxCheckoutWithSingleAddress, 'Your cart contains Digital item(s)!') + '<br/>' + getLocale(AspxCheckoutWithSingleAddress, 'Checkout as') + '<b> ' + getLocale(AspxCheckoutWithSingleAddress, 'Existing User') + '</b> ' + getLocale(AspxCheckoutWithSingleAddress, 'OR') + '<b> ' + getLocale(AspxCheckoutWithSingleAddress, 'Register') + '</b>');
                        $('.cssClassCheckOutMethodLeft .cssClassPadding #rdbGuest ,.cssClassCheckOutMethodLeft .cssClassPadding  #lblguest').remove();
                        $('#btnCheckOutMethodContinue').hide();
                        $('#rdbRegister').prop('checked', true);
                        $('#dvLogin').show();
                    }
                } else {
                    $('#rdbGuest').prop('checked', true);
                }
                if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                    CheckOut.UserCart.IsDownloadItemInCartFull = true;
                } else {
                    CheckOut.UserCart.IsDownloadItemInCartFull = false;
                }
                if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                    $('#dvBilling .cssClassCheckBox').hide();
                } else {
                    $('#dvBilling .cssClassCheckBox').show();
                }
            },

            AddUpdateUserAddress: function () {
                var addressIdX = $("#hdnAddressID").val();
                var firstNameX = Encoder.htmlEncode($("#popuprel .sfFormwrapper  #txtFirstName").val());
                var lastNameX = Encoder.htmlEncode($("#popuprel .sfFormwrapper  #txtLastName").val());
                var emailX = $("#popuprel .sfFormwrapper #txtEmailAddress").val();
                var companyX = Encoder.htmlEncode($("#popuprel .sfFormwrapper #txtCompanyName").val());
                var address1X = Encoder.htmlEncode($("#popuprel .sfFormwrapper #txtAddress1").val());
                var address2X = Encoder.htmlEncode($("#popuprel .sfFormwrapper #txtAddress2").val());
                var cityX = Encoder.htmlEncode($("#popuprel .sfFormwrapper #txtCity").val());
                var stateX = '';
                if ($.trim($("#popuprel .sfFormwrapper #ddlBLCountry").find('option:selected').text()) == 'United States') {
                    stateX = $.trim($("#popuprel .sfFormwrapper #ddlBLState").find('option:selected').text());
                } else {
                    stateX = $("#popuprel .sfFormwrapper #txtState").val();
                }
                var zipX = $("#popuprel .sfFormwrapper #txtZip").val();
                var phoneX = $("#popuprel .sfFormwrapper #txtPhone").val();
                var mobileX = $("#popuprel .sfFormwrapper #txtMobile").val();
                var faxX = '';
                if ($("#popuprel .sfFormwrapper #txtFax").length > 0)
                    faxX = $("#popuprel .sfFormwrapper #txtFax").val();
                var webSiteX = '';
                if ($("#popuprel .sfFormwrapper #txtFax").length > 0)
                    webSiteX = $("#popuprel .sfFormwrapper #txtWebsite").val();

                var countryNameX = $.trim($("#popuprel .sfFormwrapper #ddlBLCountry").find('option:selected').text());
                var isDefaultShippingX = false;
                var isDefaultBillingX = false;

                this.config.method = "AspxCoreHandler.ashx/AddUpdateUserAddress";
                this.config.url = this.config.baseURL + this.config.method;
                var addressObj = {
                    AddressID: addressIdX,
                    CustomerID: AspxCommerce.utils.GetCustomerID(),
                    FirstName: firstNameX,
                    LastName: lastNameX,
                    Email: emailX,
                    Company: companyX,
                    Address1: address1X,
                    Address2: Encoder.htmlEncode(address2X),
                    City: cityX,
                    State: stateX,
                    Zip: zipX,
                    Phone: phoneX,
                    Mobile: mobileX,
                    Fax: faxX,
                    WebSite: webSiteX,
                    Country: countryNameX,
                    DefaultShipping: isDefaultShippingX,
                    DefaultBilling: isDefaultBillingX
                };
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.SessionCode = null;
                this.config.data = JSON2.stringify({ addressObj: addressObj, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = CheckOut.BindUserAddressOnUpdate;
                this.ajaxCall(this.config);
            },

            ClearAll: function () {
                $("#hdnAddressID").val(0);
                $("#txtFirstName").val('');
                $("#txtLastName").val('');
                $("#txtEmailAddress").val('');
                $("#txtCompanyName").val('');
                $("#txtAddress1").val('');
                $("#txtAddress2").val('');
                $("#txtCity").val('');
                $("#txtState").val('');
                $('#ddlBLState').val(1);
                $("#ddlBLCountry").val(1);
                $("#txtZip").val('');
                $("#txtPhone").val('');
                $("#txtMobile").val('');
                $("#txtFax").val('');
                $("#txtWebsite").val('');
            },
            ConfirmADDNewAddress: function (event) {
                if (event) {
                    var route = '';
                    route = AspxCommerce.utils.GetAspxRedirectPath() + CheckOut.UserCart.myAccountURL + pageExtension;
                    window.location.href = route;
                    return false;
                } else {
                    return false;
                }
            },

            BindBillingData: function () {

                $('#dvCPBilling').html('');
                var itemsarray = [];
                $('#dvBilling input:text,#dvBillingSelect input[type=radio]:checked').each(function () {
                    var items = '';
                    if ($(this).prop('class') == 'cssBillingShipping')
                        items = Encoder.htmlEncode($(this).parent().text());
                    else
                        items = Encoder.htmlEncode($(this).val());
                    if (items != '') {
                        itemsarray.push(items);
                    }
                });

                var html = '<ul>';
                $.each(itemsarray, function (index, item) {
                    if (item != '') {
                        html += '<li>' + item + '</li>';
                    }
                });

                html += '</ul>';
                var htmlBtn = '<button type="button" id="btnBillingChange"><span>' + getLocale(AspxCheckoutWithSingleAddress, "Change") + '</span></button>';
                $('#dvCPBilling').html('').append(html);
                $('#divBillingBtn').html('').append(htmlBtn);
                itemsarray = [];
                $('#btnBillingChange').bind("click", function () {
                    $('#dvCPBilling').html('');
                    $("#divShippingAddressBtn").html('');
                    $("#dvCPShipping").html('');
                    $("#divShippingMethodBtn").html('');
                    $("#dvCPShippingMethod").html('');
                    $("#divPaymentBtn").html('');
                    $("#dvCPPaymentMethod").html('');
                    itemsarray = [];
                    $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                    $accor.tabs('option', 'active', 1);
                    $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                });
            },
            QuantitityDiscountAmount: function () {
                this.config.method = "AspxCoreHandler.ashx/GetDiscountQuantityAmount";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = CheckOut.SetDiscountQuantityAmount;
                this.config.async = false;
                this.ajaxCall(this.config);
            },

            BindShippingData: function () {
                $('#dvCPShipping').html('');
                var itemsarray = [];
                $('#dvShipping input:text, #dvShippingSelect input[type=radio]:checked').each(function () {
                    var items = '';
                    if ($(this).prop('class') == 'cssBillingShipping')
                        items = Encoder.htmlEncode($(this).parent().text());
                    else
                        items = Encoder.htmlEncode($(this).val());
                    itemsarray.push(items);
                });

                var html = '<ul>';
                $.each(itemsarray, function (index, item) {
                    if (item != '') {
                        html += '<li>' + item + '</li>';
                    }
                });
                html += '</ul>';
                var htmlBtn = '<button type="button" id="btnShippingChange"><span>' + getLocale(AspxCheckoutWithSingleAddress, "Change") + '</span></button>';
                $('#divShippingAddressBtn').html('').append(htmlBtn);
                $('#dvCPShipping').html('').append(html);
                itemsarray = [];
                $('#btnShippingChange').bind("click", function () {
                    $('#dvCPShipping').html('');
                    $("#divShippingMethodBtn").html('');
                    $("#dvCPShippingMethod").html('');
                    $("#divPaymentBtn").html('');
                    $("#dvCPPaymentMethod").html('');
                    $accor = $("#tabs").tabs({ active: 2, disabled: [0, 1, 3, 4, 5] });
                    $accor.tabs('option', 'active', 2);
                    $accor.tabs({ deactive: [0, 1, 3, 4, 5] });
                });

            },

            BindShippingMethodData: function () {
                $('#dvCPShippingMethod').html('');
                var itemsarray = [];
                var items = $('#divShippingMethod input:radio:checked').parents('tr').find('td div.cssClassCartPictureInformation h3').html();
                itemsarray.push(items);
                var htmlBtn = '<button type="button" id="btnShippingMethodChange"><span>' + getLocale(AspxCheckoutWithSingleAddress, "Change") + '</span></button>';
                var html = '<ul>';
                $.each(itemsarray, function (index, item) {
                    if (item != '') {
                        html += '<li>' + item + '</li>';
                    }
                });
                html += '</ul>';
                $('#divShippingMethodBtn').html('').append(htmlBtn);
                $('#dvCPShippingMethod').html('').append(html);
                itemsarray = [];
                $('#btnShippingMethodChange').bind("click", function () {
                    $('#dvCPShippingMethod').html('');
                    $("#divPaymentBtn").html('');
                    $("#dvCPPaymentMethod").html('');
                    itemsarray = [];
                    $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                    $accor.tabs('option', 'active', 3);
                    $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                });
            },
            BindPaymentData: function () {
                var itemsarray = [];
                var items = '';
                items = $('#dvPGList input[type=radio]:checked').attr('realname');
                itemsarray.push(items);
                if ($('#cardType').length > 0) {
                    items = $.trim($('#AIMChild input:radio:checked').nextAll().find('label').html());
                    itemsarray.push(items);
                }
                var html = '<ul>';
                $('#dvCPPaymentMethod').html('');
                $.each(itemsarray, function (index, item) {
                    if (item != '') {
                        html += '<li>' + item + '</li>';
                    }
                });
                html += '</ul>';
                var htmlBtn = '<button type="button" id="btnPaymentChange"><span>' + getLocale(AspxCheckoutWithSingleAddress, "Change") + '</span></button>';
                $('#divPaymentBtn').html('').append(htmlBtn);
                $('#dvCPPaymentMethod').html('').append(html);
                itemsarray = [];
                $('#btnPaymentChange').bind("click", function () {
                    $('#dvCPPaymentMethod').html('');
                    itemsarray = [];
                    if ($('#cardType').length > 0) {
                        $('#cardType').remove();
                    }
                    $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                    $accor.tabs('option', 'active', 4);
                    $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                });

            },

            AddBillingAsShipping: function () {
                if ($('#chkBillingAsShipping').is(':checked')) {
                    if ($('#dvBillingInfo').is(':hidden')) {

                        $("#ddlShipping input[name='shipping']").val($('input[name="billing"]:checked').val()).prop('checked', 'checked');
                        var id = $("#ddlShipping input[name='shipping']:checked").val();
                        var sAddress = CheckOut.GetUserSelectedShippingAddress(id);
                        CheckOut.GetShippinMethodsFromWeight(basketItems, sAddress);
                    } else {
                        var sipAddress = {
                            AddressID: 0,
                            FirstName: "",
                            LastName: "",
                            CompanyName: "",
                            EmailAddress: "",
                            Address: "",
                            Address2: "",
                            City: "",
                            State: "",
                            Zip: "",
                            Country: "",
                            Phone: "",
                            Mobile: "",
                            Fax: "",
                            Website: "",
                            IsDefaultBilling: false,
                            IsBillingAsShipping: false
                        };
                        $('#txtSPFirstName').val($('#txtFirstName').val());

                        sipAddress.FirstName = Encoder.htmlEncode($('#txtSPFirstName').val());
                        $('#txtSPLastName').val($('#txtLastName').val());
                        sipAddress.LastName = Encoder.htmlEncode($('#txtSPLastName').val());
                        $('#txtSPEmailAddress').val($('#txtEmailAddress').val());
                        sipAddress.EmailAddress = $('#txtSPEmailAddress').val();

                        $('#txtSPCompany').val($('#txtCompanyName').val());
                        sipAddress.CompanyName = Encoder.htmlEncode($('#txtSPCompany').val());
                        $('#txtSPAddress').val($('#txtAddress1').val());
                        sipAddress.Address = Encoder.htmlEncode($('#txtSPAddress').val());
                        $("#txtSPZip").val($("#txtZip").val());
                        sipAddress.Zip = $("#txtSPZip").val();
                        $('#txtSPAddress2').val($('#txtAddress2').val());
                        sipAddress.Address2 = Encoder.htmlEncode($('#txtSPAddress2').val());
                        $('#txtSPCity').val($('#txtCity').val());
                        sipAddress.City = Encoder.htmlEncode($('#txtSPCity').val());
                        if ($('#ddlBLState').is(":hidden")) {
                            $('#txtSPState').val($('#txtState').val());
                            sipAddress.State = Encoder.htmlEncode($('#txtSPState').val());
                        } else {
                            $('#ddlSPState').html("<option class='cssBillingShipping'>" + $.trim($('#ddlBLState option').html()) + "</option>");
                            $('#ddlSPState').val($('#ddlBLState').val());
                            sipAddress.State = $('#ddlSPState').val();
                        }

                        $('#ddlSPCountry').val($('#ddlBLCountry').val());
                        sipAddress.Country = $.trim($('#ddlSPCountry option:selected').text());
                        $('#txtSPPhone').val($('#txtPhone').val());
                        sipAddress.Phone = $('#txtSPPhone').val();
                        $('#txtSPMobile').val($('#txtMobile').val());
                        sipAddress.Mobile = $('#txtSPMobile').val();
                        tempUserAddresses = [];
                        tempUserAddresses.push(sipAddress);
                        CheckOut.GetShippinMethodsFromWeight(basketItems, tempUserAddresses[0]);

                    }
                } else {
                    if (rateAjaxRequest != null)
                        rateAjaxRequest.abort();

                    $('#txtSPFirstName').val("");
                    $('#txtSPLastName').val("");
                    $('#txtSPCompany').val("");
                    $('#txtSPAddress').val("");
                    $('#txtSPAddress2').val("");
                    $('#txtSPEmailAddress').val("");
                    $('#txtSPMobile').val("");
                    $('#txtSPCity').val("");
                    $('#ddlSPState').val(1);
                    $('#txtSPState').val("");
                    $('#txtSPZip').val("");
                    $('#ddlSPCountry').val(1);
                    $('#txtSPPhone').val("");
                    $('#txtSPFax').val("");
                    $('#ddlSPState').hide();
                    $('#txtSPState').show();
                }
            },

            CheckShippingAndBillingCountry: function (checktype) {
                var billing;
                var shipping;
                var blist;
                var slist;
                if ("anonymoususer" != AspxCommerce.utils.GetUserName()) {
                    billing = $.trim($('input[name="billing"]:checked').parent('label').text().split(',')[4]);
                    shipping = $.trim($('input[name="shipping"]:checked').parent('label').text().split(',')[4]);
                } else {
                    billing = $.trim($('#ddlBLCountry').find("option:selected").text());
                    shipping = $.trim($('#ddlSPCountry').find("option:selected").text());
                }

                var allowtoCheckout = { AllowBilling: false, AllowShipping: false };
                switch (checktype) {
                    case "billing":
                        if (billing != "") {
                            if (p.AllowedBillingCountry != "ALL") {
                                blist = p.AllowedBillingCountry.split(',');
                                for (var c = 0; c < blist.length; c++) {
                                    if ($.trim($("#ddlBLCountry").find('option[value=' + blist[c] + ']').text()) == $.trim(billing))
                                        allowtoCheckout.AllowBilling = true;
                                }
                            }
                            else {
                                allowtoCheckout.AllowBilling = true;
                            }
                        }
                        break;
                    case "shipping":
                        if (shipping != "") {
                            if (p.AllowedShippingCountry != "ALL") {
                                slist = p.AllowedShippingCountry.split(',');
                                for (var d = 0; d < slist.length; d++) {
                                    if ($.trim($("#ddlSPCountry").find('option[value=' + slist[d] + ']').text()) == $.trim(shipping))
                                        allowtoCheckout.AllowShipping = true;
                                }
                            }
                            else {
                                allowtoCheckout.AllowShipping = true;
                            }
                        }
                        break;
                    case "both":
                        if (billing != "") {
                            if (p.AllowedBillingCountry != "ALL") {
                                blist = p.AllowedBillingCountry.split(',');
                                for (var c = 0; c < blist.length; c++) {
                                    if ($.trim($("#ddlBLCountry").find('option[value=' + blist[c] + ']').text()) == $.trim(billing))
                                        allowtoCheckout.AllowBilling = true;
                                }
                            }
                            else {
                                allowtoCheckout.AllowBilling = true;
                            }
                        }
                        if (shipping != "") {
                            if (p.AllowedShippingCountry != "ALL") {
                                slist = p.AllowedShippingCountry.split(',');
                                for (var d = 0; d < slist.length; d++) {
                                    if ($.trim($("#ddlSPCountry").find('option[value=' + slist[d] + ']').text()) == $.trim(shipping))
                                        allowtoCheckout.AllowShipping = true;
                                }
                            }
                            else {
                                allowtoCheckout.AllowShipping = true;
                            }

                        }
                        break;
                }
                return allowtoCheckout;
            },

            GetCountry: function () {
                this.config.method = "AspxCoreHandler.ashx/BindCountryList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.ajaxCallMode = CheckOut.BindCountryList;
                this.ajaxCall(this.config);
            },
            GetState: function (countryCode) {
                this.config.method = "AspxCoreHandler.ashx/BindStateList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ countryCode: countryCode });
                this.config.ajaxCallMode = CheckOut.BindStateList;
                this.ajaxCall(this.config);

            },
            GetUserSelectedShippingAddress: function (id) {
                var address;
                if (id != 0) {
                    for (var z = 0; z < tempUserAddresses.length; z++) {
                        if (parseInt(tempUserAddresses[z].AddressID) == parseInt(id)) {
                            address = tempUserAddresses[z];
                            var ccode = '';
                            ccode = $('#ddlSPCountry option:exactcontains(' + $.trim(address.Country) + ')').prop('value');//exactcontains doesnot works when hdnCountryList changed to ddlSPCountry
                            address.State = CheckOut.GetStateCodeByStateName(ccode, address.State);

                            break;
                        }
                    }
                }
                return address;
            },
            GetStateCodeByStateName: function (cCode, state) {
                var stateCode = '';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/GetStateCode',
                    data: JSON2.stringify({ cCode: cCode, stateName: state }),
                    dataType: "json",
                    success: function (data) {
                        if (data.d != null || data.d != '') {
                            stateCode = data.d;
                        } else {
                            stateCode = state;
                        }
                    }
                });
                return stateCode;
            },
            GetShippinMethodsFromWeight: function (basketItemsDetail, shipAddress) {

                var shipToAddress = {
                    ToCity: shipAddress.City,
                    ToCountry: $('#ddlSPCountry option:exactcontains(' + $.trim(shipAddress.Country) + ')').attr('value'),
                    ToCountryName: shipAddress.Country,
                    ToAddress: shipAddress.Address,
                    ToState: shipAddress.State,
                    ToPostalCode: shipAddress.Zip,
                    ToStreetAddress1: shipAddress.Address2,
                    ToStreetAddress2: ""
                };

                var basketItemList = basketItemsDetail;

                var itemsDetail = {
                    DimensionUnit: p.dimentionalUnit,
                    IsSingleCheckOut: true,
                    ShipToAddress: shipToAddress,
                    WeightUnit: p.weightunit,
                    BasketItems: basketItemList,
                    CommonInfo: aspxCommonObj()
                };
                var dv = $("<div id='dvShippingLoading'>").html(getLocale(AspxCheckoutWithSingleAddress, 'Calculating Rate...Please Wait.....'));
                if (rateAjaxRequest != null)
                    rateAjaxRequest.abort();
                $("#divShippingMethod").html('').append(dv);
                rateAjaxRequest = $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/GetRate',
                    data: JSON2.stringify({ itemsDetail: itemsDetail }),
                    dataType: "json",
                    success: CheckOut.BindShippingMethodByWeight,
                    error: CheckOut.GetShippingMethodLoadErrorMsg
                });
                return rateAjaxRequest;
            },
            RewardPoints: function () {

                var rewardpoint = 0;
                var isRewardPointEnbled = false;
                var rewardAmounTemp = "";
                var maxRewardPoints = 0;
                var minRewardPoints = 0;
                var isPurchaseActive = false;

                var $ajaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: aspxRootPath + 'Modules/AspxCommerce/AspxRewardPoints/RewardPointsHandler.ashx/' + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };


                function aspxCommonObj() {
                    var aspxCommonInfo = {
                        StoreID: AspxCommerce.utils.GetStoreID(),
                        PortalID: AspxCommerce.utils.GetPortalID(),
                        UserName: AspxCommerce.utils.GetUserName(),
                        CultureName: AspxCommerce.utils.GetCultureName(),
                        CustomerID: AspxCommerce.utils.GetCustomerID(),
                        SessionCode: AspxCommerce.utils.GetSessionCode()
                    };
                    return aspxCommonInfo;
                }

                var _setting = {};
                var getGeneralSetting = function () {

                    if (option.RewardSettings != null) {
                        var item = JSON.parse(option.RewardSettings);
                        maxRewardPoints = item.TotalRewardPoints;
                        _setting.Rate = item.RewardExchangeRate / item.RewardPoints;
                        _setting.RewardRate = item.RewardPointsEarned / item.AmountSpent;
                        _setting.TotalRewardAmount = item.TotalRewardAmount;
                        _setting.TotalRewardPoints = item.TotalRewardPoints;
                        _setting.MinRedeemBalance = item.MinRedeemBalance;
                        _setting.BalanceCapped = item.BalanceCapped;

                        isPurchaseActive = item.IsPurchaseActive
                        var rewardInfo = { IsPurchaseActive: isPurchaseActive, Setting: _setting };
                        setToCheckout(rewardInfo);

                        if (rewardpoint > 0) {
                            var total = rewardpoint * _setting.Rate;
                            CheckOutApi.CalcAPI.AddOtherAmount("RewardPoints:", total, CheckOutApi.CalcAPI.OperationType.minus);
                            CheckOutApi.CalcAPI.ShowData();

                            CheckOutApi.Set("UsedRewardPoints", rewardpoint);
                            CheckOutApi.Set("RewardPointsDiscount", total);
                        }

                    }

                };

                var setToCheckout = function (rewardInfo) {
                    CheckOutApi.Set('RewardPoint', rewardInfo);
                }

                var addItemRewardPoins = function () {

                    if (isPurchaseActive) {

                        $.each(CheckOutApi.CalcAPI.Items, function (index, item) {

                            var rp = item.Price * item.Quantity * _setting.Rate;
                            CheckOutApi.SetItem(item.ItemID, "RewardedPoints", rp);

                        });


                    }


                };
                var init = function () {
                    $securepost("GSA", { k: "RewardPoints" }, function (data) {
                        rewardpoint = parseFloat(data.d);

                    }, null);
                    getGeneralSetting();
                };
                return { Init: init };
            }(),
            Coupon: function () {
                var coupons = p.Coupon;

                var $ajaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };

                getCoupon = function (key) {
                    var cp = { Key: '', AppliedCount: 0, IsForFreeShipping: false, IsPercentage: false, Value: 0 };

                    for (var i = 0; i < coupons.length; i++) {

                        if (coupons[i].Key == key) {
                            cp = coupons[i];
                            break;
                        }

                    }
                    return cp;

                };


                couponsforCheckOut = function () {

                    var arr = [];
                    $.each(coupons, function (index, item) {

                        arr.push(item.Key + "_" + item.AppliedCount);
                    });
                    return arr.join('&');

                };
                getCouponAmount = function (coupon) {

                    var afterdiscount = cartCalculator.getSubTotal();
                    var totalcouponDiscount = 0;
                    if (coupon.IsPercentage == true) {
                        for (var z = 0; z < coupon.AppliedCount; z++) {

                            var discPer = (afterdiscount * (coupon.Value) / 100).toFixed(2);

                            afterdiscount = afterdiscount - discPer;
                            totalcouponDiscount = (parseFloat(totalcouponDiscount) + parseFloat(discPer))
                        }
                    } else {
                        totalcouponDiscount = coupon.AppliedCount * coupon.Value;

                    }

                    return totalcouponDiscount;
                }

                recalculateDiscount = function () {
                    var order = 3;
                    var total = 0;
                    $.each(coupons, function (index, coupon) {
                        var amount = getCouponAmount(coupon);
                        total += amount;
                        order++;
                        cartCalculator.AddOtherAmount("Coupon Discount(" + coupon.Key + "):", amount, cartCalculator.OperationType.minus, order);


                    });
                    CheckOut.UserCart.CouponDiscountAmount = total;
                    cartCalculator.ShowData();
                }

                var init = function () {
                    window.onload = function () {
                        if (coupons.length > 0) {
                            var order = 3; var total = 0;
                            $.each(coupons, function (index, coupon) {
                                var amount = getCouponAmount(coupon);
                                total += amount;

                                order++;
                                if (coupon.IsForFreeShipping) {
                                    CheckOut.UserCart.IsFShipping = true;
                                }
                                cartCalculator.AddOtherAmount("Coupon Discount(" + coupon.Key + "):", amount, cartCalculator.OperationType.minus, order);


                            });
                            CheckOut.UserCart.CouponDiscountAmount = total;


                        } else {
                            CheckOut.UserCart.CouponDiscountAmount = 0;
                            cartCalculator.AddOtherAmount("Coupon Discount:", 0, cartCalculator.OperationType.minus, 4);

                        }
                        var cookieCurrency = $("#ddlCurrency").val();
                        Currency.currentCurrency = BaseCurrency;
                        Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                    }
                }();
                var getCouponStatus = function () {
                    $securepost("GSC", {}, function (data) {
                        coupons = $.parseJSON(data.d);
                        if (coupons.length > 0)
                            recalculateDiscount();
                    }, function () { });
                };
                return {
                    Init: getCouponStatus,
                    Get: couponsforCheckOut
                };



            }(),
            Discount: function () {

                var quantityDiscount = 0;
                var cartPricerulDiscount = 0;
                var totalDiscount = 0;

                var $coreAjaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: aspxservicePath + "AspxCoreHandler.ashx/" + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };

                var $ajaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };

                getdiscount = function (sessionVariable) {

                    $securepost("GS", JSON2.stringify({
                        key: sessionVariable
                    }),
                        function (data) {
                            SetSessionData(data);
                        }, function () {
                        });

                }

                QuantitityDiscountAmount = function () {

                    $coreAjaxCall("GetDiscountQuantityAmount", JSON2.stringify({
                        aspxCommonObj: aspxCommonObj()
                    }),
                        function (data) {
                            quantityDiscount = parseFloat(data.d).toFixed(2);
                            var totalDiscount = parseFloat(quantityDiscount) + parseFloat(cartPricerulDiscount);
                            CheckOut.UserCart.TotalDiscount = totalDiscount;
                            $securepost("SS", { k: 'DiscountAmount', v: totalDiscount }, function (data) { }, function () { });

                            cartCalculator.AddOtherAmount("" + getLocale(AspxCheckoutWithSingleAddress, "Cart Discount:") + "", totalDiscount, cartCalculator.OperationType.minus, 1);
                            cartCalculator.ShowData();

                        }, function () {


                        });

                };

                GetDiscountCartPriceRule = function (CartID, SpCost) {
                    var aspxCommonInfo = aspxCommonObj();
                    aspxCommonInfo.SessionCode = null;
                    $coreAjaxCall("GetDiscountPriceRule", JSON2.stringify({
                        cartID: CartID,
                        aspxCommonObj: aspxCommonInfo,
                        shippingCost: SpCost
                    }),
                      function (data) {
                          cartPricerulDiscount = parseFloat(data.d).toFixed(2);
                          QuantitityDiscountAmount();
                      }, function () {


                      });
                };


                SetDiscountQuantityAmount = function (msg) {
                    quantityDiscount = parseFloat(msg.d).toFixed(2);
                };
                var init = function (spcost) {

                    GetDiscountCartPriceRule(cartCalculator.Items[0].CartID, spcost);
                };

                return { Init: init };
            }(),

            GetUserCartDetails: function () {
                CheckOut.config.method = "AspxCoreHandler.ashx/GetCartCheckOutDetails";
                CheckOut.config.url = CheckOut.config.baseURL + CheckOut.config.method;
                CheckOut.config.async = false;
                CheckOut.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                CheckOut.config.ajaxCallMode = CheckOut.GetCheckoutCartDetails;
                CheckOut.ajaxCall(CheckOut.config);
            },

            AssignItemsDetails: function () {
                var countGiftCard = 0;
                var countDownloadble = 0;
                var itemTotal = 0;
                CheckOut.UserCart.lstItems = [];
                var applyrewardpoint = false;
                if (CheckOut.UserCart.RewardPoint != undefined) {
                    applyrewardpoint = CheckOut.UserCart.RewardPoint.IsPurchaseActive;
                }
                $.each(cartCalculator.Items, function (index, item) {

                    itemTotal++;
                    if (item.ItemTypeID == 2) {
                        CheckOut.UserCart.lstItems[index] = { "OrderID": 0, "ShippingAddressID": 0, "ShippingMethodID": 0, "ItemID": item.ItemID, "Variants": item.CostVariantsValueIDs, "Quantity": item.Quantity, "Price": item.Price, "Weight": 0, "Remarks": '', "orderItemRemarks": '', "ShippingRate": 0, 'IsDownloadable': true, 'IsGiftCard': false, 'CartItemId': item.CartItemID };
                        countDownloadble++;
                    } else if (item.ItemTypeID == 3) {
                        var counter = index + 1;
                        var vr = $("#item_" + item.ItemID + "_" + counter).attr("isvirtual");
                        var sid = 0;
                        var shippingMethod = 0;
                        var shippingcost = 0;
                        if (vr == "true" || vr == true) {
                        } else {
                            sid = CheckOut.UserCart.spAddressID;
                            shippingMethod = CheckOut.UserCart.spMethodID;
                            shippingcost = CheckOut.UserCart.spCost;
                            countGiftCard++;
                        }
                        CheckOut.UserCart.lstItems[index] = { "OrderID": 0, "ShippingAddressID": sid, "ShippingMethodID": shippingMethod, "ItemID": item.ItemID, "Variants": item.CostVariantsValueIDs, "Quantity": item.Quantity, "Price": item.Price, "Weight": 0, "Remarks": '', "orderItemRemarks": '', "ShippingRate": shippingcost, 'IsDownloadable': false, 'IsGiftCard': true, 'CartItemId': item.CartItemID };

                    }
                    else {
                        if (item.ItemTypeID == 6)
                            CheckOut.UserCart.lstItems[index] = { "OrderID": 0, "ShippingAddressID": CheckOut.UserCart.spAddressID, "ShippingMethodID": CheckOut.UserCart.spMethodID, "ItemID": item.ItemID, "Variants": item.CostVariantsValueIDs, "Quantity": item.Quantity, "Price": item.Price, "Weight": item.Weight, "Remarks": '', "orderItemRemarks": '', "ShippingRate": CheckOut.UserCart.spCost, 'IsDownloadable': false, 'IsGiftCard': false, 'CartItemId': item.CartItemID, KitDescription: item.ShortDescription, KitData: item.KitData };
                        else
                            CheckOut.UserCart.lstItems[index] = { "OrderID": 0, "ShippingAddressID": CheckOut.UserCart.spAddressID, "ShippingMethodID": CheckOut.UserCart.spMethodID, "ItemID": item.ItemID, "Variants": item.CostVariantsValueIDs, "Quantity": item.Quantity, "Price": item.Price, "Weight": item.Weight, "Remarks": '', "orderItemRemarks": '', "ShippingRate": CheckOut.UserCart.spCost, 'IsDownloadable': false, 'IsGiftCard': false, 'CartItemId': item.CartItemID };

                    }
                    var rp = 0;
                    if (applyrewardpoint) {
                        rp = item.Price * item.Quantity * CheckOut.UserCart.RewardPoint.Setting.RewardRate;
                        CheckOut.UserCart.lstItems[index]["RewardedPoints"] = rp;
                    }
                });
                if (eval(countDownloadble + countGiftCard) == eval(itemTotal)) {
                    CheckOut.UserCart.NoShippingAddress = true;
                    CheckOut.UserCart.IsDownloadItemInCart = true;
                } else {
                    CheckOut.UserCart.NoShippingAddress = false;
                    CheckOut.UserCart.IsDownloadItemInCart = false;
                }
            },

            BindUserAddress: function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.SessionCode = null;
                this.config.method = "AspxCoreHandler.ashx/GetAddressBookDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = CheckOut.BindAddressBookDetails;
                this.config.error = CheckOut.GetAddressLoadErrorMsg;
                this.ajaxCall(this.config);
            },

            GetTaxRate: function (itemID) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.SessionCode = null;
                if (aspxCommonInfo.UserName != 'anonymoususer') {
                    var cartTaxObj = {
                        ItemID: itemID,
                        Country: '',
                        State: '',
                        Zip: '',
                        AddressID: CheckOut.Vars.AddressID
                    };
                    this.config.method = "AspxCoreHandler.ashx/GetCartTax";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ cartTaxObj: cartTaxObj, aspxCommonObj: aspxCommonInfo });
                    this.config.ajaxCallMode = CheckOut.BindTaxRate;
                    this.ajaxCall(this.config);

                } else {
                    var cartTaxObj = {
                        ItemID: itemID,
                        Country: CheckOut.Vars.Country,
                        State: CheckOut.Vars.State,
                        Zip: CheckOut.Vars.Zip,
                        AddressID: CheckOut.Vars.AddressID
                    };
                    this.config.method = "AspxCoreHandler.ashx/GetCartTax";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ cartTaxObj: cartTaxObj, aspxCommonObj: aspxCommonInfo });
                    this.config.ajaxCallMode = CheckOut.BindTaxRate;
                    this.ajaxCall(this.config);
                }
            },

            SetSessionValue: function (sessionKey, sessionValue) {
                this.config.method = "AspxCoreHandler.ashx/SetSessionVariable";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ key: sessionKey, value: sessionValue });
                this.config.ajaxCallMode = CheckOut.GetSessionValue;
                this.ajaxCall(this.config);
            },

            LoadPGatewayList: function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                aspxCommonInfo.CustomerID = null;
                aspxCommonInfo.SessionCode = null;
                this.config.method = "AspxCoreHandler.ashx/GetPGList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = CheckOut.BindPaymentGateWayList;
                this.ajaxCall(this.config);
            },

            LoadControl: function (ControlName, Name) {
                this.Vars.GatewayName = Name;
                this.config.method = "LoadControlHandler.aspx/Result";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + ControlName + "'}";
                this.config.ajaxCallMode = CheckOut.LoadPaymentGateway;
                this.config.error = CheckOut.GetPaymentGateWayLoadErrorMsg;
                this.ajaxCall(this.config);

            },

            LoadPaymentGateway: function (data) {
                if (data.d.startsWith('System.Web.HttpException')) {
                    csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "This Payment Gateway failed to load. Please try again Later.") + "</p>");
                    $("#btnPaymentInfoContinue").prop("disabled", "disabled");
                    return false;
                } else {
                    $("#btnPaymentInfoContinue").removeAttr("disabled").prop("enabled", "enabled");

                    var dvList = $("<div>" + data.d + "</div>");
                    if (dvList.find('div').length > 0) {
                        var button = dvList.find('input:last').parents("label:eq(0)");
                        dvList.find('input:last').parents("label:eq(0)").remove();
                        $('#dvPaymentInfo input[type="button"]').remove();
                        $('#dvPlaceOrder .sfButtonwrapper ').find('input').parents("label:eq(0)").not("#btnPlaceBack").remove();
                        $('#dvPlaceOrder .sfButtonwrapper ').append(button);
                        $('#dvPlaceOrder .sfButtonwrapper div ').remove();

                        $('#dvPaymentInfo').find('div').not('.sfButtonwrapper').not('.cssClassClear').not("#dvinfo").not('#dvPGList').remove();
                        $('#dvPaymentInfo .sfButtonwrapper').before(dvList);

                        if (cartCalculator.GiftCard.GiftCardList().length == 0) {
                            $("#btnPlaceOrderGiftCard").parents('label:eq(0)').remove();
                        }
                    } else {
                        $('#dvPaymentInfo').find('div').not('.sfButtonwrapper').not('.cssClassClear').not("#dvinfo").not('#dvPGList').remove();
                        $('#dvPlaceOrder .sfButtonwrapper ').find('input').parents("label:eq(0)").not("#btnPlaceBack").remove();
                        $('#dvPlaceOrder .sfButtonwrapper ').append(data.d);
                        $('#dvPlaceOrder .sfButtonwrapper ').find('div').remove();
                        if (cartCalculator.GiftCard.GiftCardList().length == 0) {
                            $("#btnPlaceOrderGiftCard").parents('label:eq(0)').remove();
                        }
                    }
                }
            },

            GetSessionValue: function (data) {
                CheckOut.config.sessionValue = parseFloat(data.d);
            },

            BindPaymentGateWayList: function (data) {
                if (data.d.length > 0) {
                    $('#dvPGList').html('');
                    $.each(data.d, function (index, item) {
                        if (item.LogoUrl != '') {
                            $('#dvPGList').append('<label><input id="rdb' + item.PaymentGatewayTypeName + '" name="PGLIST" type="radio" realname="' + item.PaymentGatewayTypeName + '" friendlyname="' + item.FriendlyName + '"  source="' + item.ControlSource + '" value="' + item.PaymentGatewayTypeID + '" class="cssClassRadioBtn" /><img class="cssClassImgPGList" src="' + aspxRootPath + item.LogoUrl + '" alt="' + item.PaymentGatewayTypeName + '" title="' + item.PaymentGatewayTypeName + '" visible="true" /></label>');
                        } else {
                            $('#dvPGList').append('<label><input id="rdb' + item.PaymentGatewayTypeName + '" name="PGLIST" type="radio" realname="' + item.PaymentGatewayTypeName + '" friendlyname="' + item.FriendlyName + '"  source="' + item.ControlSource + '" value="' + item.PaymentGatewayTypeID + '" class="cssClassRadioBtn" /><b>' + item.PaymentGatewayTypeName + '</b></label>');
                        }
                    });
                    CheckOut.BindPGFunction();
                }
            },
            BindPGFunction: function () {
                $('#dvPGList input[name="PGLIST"]:first').prop("checked", "checked");

                $('#dvPGList input[name="PGLIST"]').bind("click", function () {
                    if ('paypal' == $(this).attr('friendlyname').toLowerCase()) {
                        CheckOut.UserCart.paymentMethodCode = "Paypal";
                        CheckOut.UserCart.paymentMethodName = "Paypal";
                    } else {
                    }

                });
                $('#dvPGList input[name="PGLIST"]:first').trigger('click');
            },
            BindAddressBookDetails: function (data) {

                var option = '';
                var optionBilling = '';
                var pattern = ",", re = new RegExp(pattern, "g");

                if (data.d.length > 0) {
                    tempUserAddresses = [];
                    $.each(data.d, function (index, item) {
                        tempUserAddresses.push(item);
                        if (item.DefaultShipping == 1) {
                            option += "<div><label><input type='radio' name='shipping' value=" + item.AddressID + " selected='selected' class='cssBillingShipping'> ";
                            option += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                            if (item.Address1 != "")
                                option += ", " + item.Address1.replace(re, "-");

                            if (item.City != "")
                                option += ", " + item.City.replace(re, "-");

                            if (item.State != "")
                                option += ", " + item.State.replace(re, "-");

                            if (item.Country != "")
                                option += ", " + item.Country.replace(re, "-");

                            if (item.Zip != "")
                                option += ", " + item.Zip.replace(re, "-");

                            if (item.Email != "")
                                option += ", " + item.Email.replace(re, "-");

                            if (item.Phone != "")
                                option += ", " + item.Phone.replace(re, "-");

                            if (item.Mobile != "")
                                option += ", " + item.Mobile.replace(re, "-");

                            if (item.Fax != "")
                                option += ", " + item.Fax.replace(re, "-");

                            if (item.Website != "")
                                option += ", " + item.Website.replace(re, "-");

                            if (item.Address2 != "")
                                option += ", " + item.Address2.replace(re, "-");

                            if (item.Company != "")
                                option += ", " + item.Company.replace(re, "-");
                        } else {
                            option += "<div><label><input type='radio' name='shipping' value=" + item.AddressID + " class='cssBillingShipping'> ";
                            option += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                            if (item.Address1 != "")
                                option += ", " + item.Address1.replace(re, "-");

                            if (item.City != "")
                                option += ", " + item.City.replace(re, "-");

                            if (item.State != "")
                                option += ", " + item.State.replace(re, "-");

                            if (item.Country != "")
                                option += ", " + item.Country.replace(re, "-");

                            if (item.Zip != "")
                                option += ", " + item.Zip.replace(re, "-");

                            if (item.Email != "")
                                option += ", " + item.Email.replace(re, "-");

                            if (item.Phone != "")
                                option += ", " + item.Phone.replace(re, "-");

                            if (item.Mobile != "")
                                option += ", " + item.Mobile.replace(re, "-");

                            if (item.Fax != "")
                                option += ", " + item.Fax.replace(re, "-");

                            if (item.Website != "")
                                option += ", " + item.Website.replace(re, "-");

                            if (item.Address2 != "")
                                option += ", " + item.Address2.replace(re, "-");

                            if (item.Company != "")
                                option += ", " + item.Company.replace(re, "-");
                        }
                        option += "</label></div>";
                        if (item.DefaultBilling == 1) {
                            optionBilling += "<div><label><input type='radio' name='billing' value=" + item.AddressID + " selected='selected' class='cssBillingShipping'> ";
                            optionBilling += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                            if (item.Address1 != "")
                                optionBilling += ", " + item.Address1.replace(re, "-");

                            if (item.City != "")
                                optionBilling += ", " + item.City.replace(re, "-");

                            if (item.State != "")
                                optionBilling += ", " + item.State.replace(re, "-");

                            if (item.Country != "")
                                optionBilling += ", " + item.Country.replace(re, "-");

                            if (item.Zip != "")
                                optionBilling += ", " + item.Zip.replace(re, "-");

                            if (item.Email != "")
                                optionBilling += ", " + item.Email.replace(re, "-");

                            if (item.Phone != "")
                                optionBilling += ", " + item.Phone.replace(re, "-");

                            if (item.Mobile != "")
                                optionBilling += ", " + item.Mobile.replace(re, "-");

                            if (item.Fax != "")
                                optionBilling += ", " + item.Fax.replace(re, "-");

                            if (item.Website != "")
                                optionBilling += ", " + item.Website.replace(re, "-");

                            if (item.Address2 != "")
                                optionBilling += ", " + item.Address2.replace(re, "-");

                            if (item.Company != "")
                                optionBilling += ", " + item.Company.replace(re, "-");
                        } else {
                            optionBilling += "<div><label><input type='radio' name='billing' value=" + item.AddressID + " class='cssBillingShipping'> ";
                            optionBilling += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                            if (item.Address1 != "")
                                optionBilling += ", " + item.Address1.replace(re, "-");

                            if (item.City != "")
                                optionBilling += ", " + item.City.replace(re, "-");

                            if (item.State != "")
                                optionBilling += ", " + item.State.replace(re, "-");

                            if (item.Country != "")
                                optionBilling += ", " + item.Country.replace(re, "-");

                            if (item.Zip != "")
                                optionBilling += ", " + item.Zip.replace(re, "-");

                            if (item.Email != "")
                                optionBilling += ", " + item.Email.replace(re, "-");

                            if (item.Phone != "")
                                optionBilling += ", " + item.Phone.replace(re, "-");

                            if (item.Mobile != "")
                                optionBilling += ", " + item.Mobile.replace(re, "-");

                            if (item.Fax != "")
                                optionBilling += ", " + item.Fax.replace(re, "-");

                            if (item.Website != "")
                                optionBilling += ", " + item.Website.replace(re, "-");

                            if (item.Address2 != "")
                                optionBilling += ", " + item.Address2.replace(re, "-");

                            if (item.Company != "")
                                optionBilling += ", " + item.Company.replace(re, "-");
                        }
                        optionBilling += "</label></div>";
                    });

                 
                    $("#ddlShipping").html('');
                    $("#ddlBilling").html('');
                    $("#ddlShipping").html(option);
                    $("#ddlBilling").html(optionBilling);
                    if ($.trim($('#ddlBilling').text()) == "" || $.trim($('#ddlBilling').text()) == null) {

                        $('#addBillingAddress').show();
                    } else {
                        $('#addBillingAddress').show();
                    }
                    if ($.trim($('#ddlShipping').text()) == "" || $.trim($('#ddlShipping').text()) == null) {
                        $('#addShippingAddress').show();
                    } else {
                        $('#addShippingAddress').show();
                    }
                    if (selectedBillingAddressBeforeAddingNewAddress.length > 0) {

                        $("input[name=billing][value=" + selectedBillingAddressBeforeAddingNewAddress + "]").click();
                    }
                }
            },
            GetCheckoutCartDetails: function (data) {
                cartCalculator.Items = data.d;
                var arrRewardtotalPrice = 0;
                var cartHeading = '';
                var cartElements = '';
                cartHeading += '<table class="sfGridTableWrapper" width="100%" border="0" cellpadding="0" cellspacing="0" id="tblCartList">';
                cartHeading += '<thead><tr class="cssClassHeadeTitle">';
                cartHeading += '<th class="cssClassSN"> Sn.';
                cartHeading += ' </th><th class="cssClassProductImageWidth">';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Item Image');
                cartHeading += '</th>';
                cartHeading += '<th>';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Variants');
                cartHeading += '</th>';
                cartHeading += '<th class="cssClassQTY">';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Qty');
                cartHeading += '</th>';
                cartHeading += '<th class="cssClassProductPrice">';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Unit Price');
                cartHeading += '</th>';
                cartHeading += '<th class="cssClassSubTotal">';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Line Total');
                cartHeading += '</th>';
                cartHeading += '<th class="cssClassTaxRate">';
                cartHeading += getLocale(AspxCheckoutWithSingleAddress, 'Unit Tax');
                cartHeading += '</th>';
                cartHeading += '</tr>';
                cartHeading += '</thead>';
                cartHeading += '<tbody>';
                cartHeading += '</table>';
                CheckOut.Vars.ItemIDs = "";
                $("#divCartDetails").html(cartHeading);
                var giftcardCount = 0;
                basketItems = [];
                $.each(data.d, function (index, value) {

                    if (value.ItemTypeID == 1) {
                        var basketItem = {
                            Height: value.Height,
                            ItemName: value.ItemName,
                            Length: value.Length,
                            Width: value.Width,
                            WeightValue: value.Weight,
                            Quantity: value.Quantity
                        };
                        basketItems.push(basketItem);
                    }

                    itemID = value.ItemID;

                    CheckOut.Vars.ItemIDs = CheckOut.Vars.ItemIDs + itemID + '#' + value.CostVariantsValueIDs + ',';
                    var CostVariantsValueIDs = '';
                    CostVariantsValueIDs = value.CostVariantsValueIDs;
                    index = index + 1;
                    var imagePath = itemImagePath + value.ImagePath;
                    if (value.ImagePath == "") {
                        imagePath = p.noImageCheckOutInfoPath;
                    } else if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    if (parseInt(value.ItemTypeID) == 2) {
                        CheckOut.UserCart.IsDownloadItemInCart = true;
                        CheckOut.UserCart.CountDownloadableItem++;
                    }
                    var isVirtual = false;
                    if (parseInt(value.ItemTypeID) == 3) {
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.UserName = null;
                        aspxCommonInfo.CustomerID = null;
                        aspxCommonInfo.SessionCode = null;
                        aspxCommonInfo.CultureName = null;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: aspxservicePath + 'AspxCoreHandler.ashx/GetGiftCardType',
                            data: JSON2.stringify({ aspxCommonObj: aspxCommonInfo, cartitemId: value.CartItemID }),
                            dataType: "json",
                            success: function (dd) {
                                if (dd.d != null) {
                                    isVirtual = CheckOut.UserCart.ShowShippingAdd = dd.d == 2 ? false : true;
                                    giftcardCount++;
                                    if (data.d.length != giftcardCount) {
                                        CheckOut.UserCart.ShowShippingAdd = false;
                                    }
                                }
                            }
                        });
                    }

                    if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.SessionCode = null;
                        var cartTaxOrderObj = {
                            ItemID: itemID,
                            Country: '',
                            State: '',
                            Zip: '',
                            AddressID: CheckOut.Vars.AddressID,
                            CostVariantsValueIDs: CostVariantsValueIDs
                        };
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetCartTaxforOrder",
                            data: JSON2.stringify({ cartTaxOrderObj: cartTaxOrderObj, aspxCommonObj: aspxCommonInfo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {

                                $.each(msg.d, function (index, val) {
                                    if (val.TaxRateValue != 0) {
                                        var objTaxInfo = {
                                            TaxManageRuleId: val.TaxManageRuleID,
                                            ItemID: val.ItemID,
                                            CostVariantsValueIDs: val.CostVariantsValueIDs,
                                            TaxSubTotal: val.TaxRateValue,
                                            StoreID: AspxCommerce.utils.GetStoreID(),
                                            PortalID: AspxCommerce.utils.GetPortalID(),
                                            AddedBy: AspxCommerce.utils.GetUserName()
                                        };
                                        CheckOut.UserCart.objTaxList.push(objTaxInfo);
                                    }
                                });
                            }
                        });
                    } else {
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.SessionCode = null;
                        var cartTaxOrderObj = {
                            ItemID: itemID,
                            Country: CheckOut.Vars.Country,
                            State: $.trim(CheckOut.Vars.State),
                            Zip: CheckOut.Vars.Zip,
                            AddressID: CheckOut.Vars.AddressID,
                            CostVariantsValueIDs: CostVariantsValueIDs
                        };
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetCartTaxforOrder",
                            data: JSON2.stringify({ cartTaxOrderObj: cartTaxOrderObj, aspxCommonObj: aspxCommonInfo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                                $.each(msg.d, function (index, val) {
                                    if (val.TaxRateValue != 0) {
                                        var objTaxInfo = {
                                            TaxManageRuleId: val.TaxManageRuleID,
                                            ItemID: val.ItemID,
                                            CostVariantsValueIDs: val.CostVariantsValueIDs,
                                            TaxSubTotal: val.TaxRateValue,
                                            StoreID: AspxCommerce.utils.GetStoreID(),
                                            PortalID: AspxCommerce.utils.GetPortalID(),
                                            AddedBy: AspxCommerce.utils.GetUserName()
                                        };
                                        CheckOut.UserCart.objTaxList.push(objTaxInfo);
                                    }
                                });
                            }
                        });
                    }

                    CheckOut.Vars.Tax = 0.00;
                    if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.SessionCode = null;
                        cartCalculator.ItemTax.Add(itemID, 0);
                        var cartUnitTaxObj = {
                            ItemID: itemID,
                            Country: '',
                            State: '',
                            Zip: '',
                            AddressID: CheckOut.Vars.AddressID,
                            CostVariantsValueIDs: CostVariantsValueIDs
                        };
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetCartUnitTax",
                            data: JSON2.stringify({ cartUnitTaxObj: cartUnitTaxObj, aspxCommonObj: aspxCommonInfo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                                $.each(msg.d, function (index, val) {
                                    CheckOut.Vars.Tax = val.TaxRateValue;
                                    cartCalculator.ItemTax.Add(val.ItemID, val.TaxRateValue);
                                });
                            }
                        });
                    } else {
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.SessionCode = null;
                        cartCalculator.ItemTax.Add(itemID, 0);
                        var cartUnitTaxObj = {
                            ItemID: itemID,
                            Country: $.trim(CheckOut.Vars.Country),
                            State: $.trim(CheckOut.Vars.State),
                            Zip: $.trim(CheckOut.Vars.Zip),
                            AddressID: CheckOut.Vars.AddressID,
                            CostVariantsValueIDs: CostVariantsValueIDs
                        };
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetCartUnitTax",
                            data: JSON2.stringify({ cartUnitTaxObj: cartUnitTaxObj, aspxCommonObj: aspxCommonInfo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                                $.each(msg.d, function (index, val) {
                                    CheckOut.Vars.Tax = val.TaxRateValue;
                                    cartCalculator.ItemTax.Add(val.ItemID, val.TaxRateValue);
                                });
                            }
                        });
                    }

                    CheckOut.UserCart.CountAllItem++;
                    cartElements += '<tr >';
                    cartElements += '<td><input type="hidden" name="cartItemId" value=' + value.CartItemID + ' />';
                    cartElements += '<b>' + index + "." + '</b>';
                    cartElements += '</td>';
                    cartElements += '<td>';
                    cartElements += '<p class="cssClassCartPicture">';
                    cartElements += '<img title="' + value.AlternateText + '" src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" ></p>';
                    cartElements += '<div class="cssClassCartPictureInformation">';
                    cartElements += '<h3>';
                    if (value.CostVariantsValueIDs != '') {
                        cartElements += '<a class="cssClassLink" id="item_' + value.ItemID + '" itemType="' + value.ItemTypeID + '"  href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + value.SKU + pageExtension + '?varId=' + value.CostVariantsValueIDs + '">' + value.ItemName + ' </a></h3>';
                    } else {

                        if (parseInt(value.ItemTypeID) == 3) {
                            cartElements += '<a class="cssClassLink" id="item_' + value.ItemID + "_" + index + '" isvirtual=' + isVirtual + ' itemType="' + value.ItemTypeID + '"  href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + value.SKU + pageExtension + '">' + value.ItemName + ' </a></h3>';
                        } else {
                            cartElements += '<a class="cssClassLink" id="item_' + value.ItemID + "_" + index + '"  itemType="' + value.ItemTypeID + '"  href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + value.SKU + pageExtension + '">' + value.ItemName + ' </a></h3>';

                        }
                    }
                    cartElements += '</div>';
                    cartElements += '</td>';
                    cartElements += '<td class="row-variants" varIDs="' + value.CostVariantsValueIDs + '">';
                    cartElements += '' + value.CostVariants + '';
                    cartElements += '</td>';
                    cartElements += '<td class="cssClassPreviewQTY">';
                    cartElements += '<input class="num-pallets-input" taxrate="' + (CheckOut.Vars.Tax) + '" price="' + value.Price + '" id="txtQuantity_' + value.CartID + '" type="text" readonly="readonly" disabled="disabled" value="' + value.Quantity + '">';
                    cartElements += '</td>';
                    cartElements += '<td class="price-per-pallet">';
                    cartElements += '<span id="' + value.Weight + '" class="cssClassFormatCurrency">' + parseFloat(value.Price).toFixed(2) + '</span>';
                    cartElements += '</td>';
                    cartElements += '<td class="row-total">';
                    cartElements += '<input class="row-total-input cssClassFormatCurrency" id="txtRowTotal_' + value.CartID + '"  value="' + parseFloat(value.TotalItemCost).toFixed(2) + '" baseValue="' + (value.TotalItemCost).toFixed(2) + '"  readonly="readonly" type="text" />';
                    cartElements += '</td>';
                    cartElements += '<td class="row-taxRate">';
                    cartElements += '<span class="cssClassFormatCurrency">' + parseFloat(CheckOut.Vars.Tax).toFixed(2) + '</span>';
                    cartElements += '</td>';
                    cartElements += '</tr>';
                    CheckOut.UserCart.CartID = value.CartID;
                    arrRewardtotalPrice = arrRewardtotalPrice + parseFloat((value.TotalItemCost * value.Quantity).toFixed(2));
                });
                $("#tblCartList").append(cartElements);
                $("#tblCartList tr:even ").addClass("sfEven");
                $("#tblCartList tr:odd ").addClass("sfOdd");
                CheckOut.BindFunction();
                CheckOut.AssignItemsDetails();

            },
            BindFunction: function () {

                CheckOut.GetTaxRate(CheckOut.Vars.ItemIDs.substring(0, CheckOut.Vars.ItemIDs.length - 1));

                var baseSubTotalAmount = 0.00;
                var subTotalAmount = cartCalculator.getSubTotal();

                if ($('#lnkMyCart').find('.cssClassTotalCount').text().replace('[', '').replace(']', '') == '0') {
                    $('.cssClassAccordionWrapper').hide();
                    $('.cssClassRightAccordainTab').hide();
                    $('.cssClassBodyContentWrapper').append("<div id ='msgnoitem' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h2> <span id='spnheader'>" + getLocale(AspxCheckoutWithSingleAddress, "Message") + " </span></h2> <div class='cssClassClear'> </div></div><div class='sfGridwrapper'><div class='sfGridWrapperContent'><h3>" + getLocale(AspxCheckoutWithSingleAddress, "No Items found in your Cart") + "</h3><div class='sfButtonwrapper'><button type='button' id='btnContinueInStore' class='sfBtn'><span>" + getLocale(AspxCheckoutWithSingleAddress, "Continue to Shopping") + "</span></button></div><div class='cssClassClear'></div></div></div></div>");

                    $("#btnContinueInStore").bind("click", function () {
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + homeURL + pageExtension;
                        return false;
                    });
                }
                $('.cssClassCartPicture img[title]').tipsy({ gravity: 'n' });
                CheckOut.AssignItemsDetails();
                CheckOut.CheckDownloadableOnlyInCart();
            },

            SetDiscountPriceRule: function (data) {
                if (data.d.length > 0) {
                    CheckOut.UserCart.CartDiscount = parseFloat(data.d).toFixed(2);
                }
            },

            BindShippingMethodByWeight: function (data) {

                var shippingmethodId = 0;
                var shippingHeading = '';
                var shippingMethodElements = '';
                shippingHeading += '<table width="100%" cellspacing="0" cellpadding="0" border="0">';
                shippingHeading += '<tbody>';
                shippingHeading += '</tbody></table>';
                $("#divShippingMethod").html(shippingHeading);

                if (data.d.length == 0) {
                    $('#divShippingMethod table>tbody').append("<tr><td>" + getLocale(AspxCheckoutWithSingleAddress, "Items' weight in your cart doesn't meet the shipping provider weight criteria Or") + " " + getLocale(AspxCheckoutWithSingleAddress, "Shipping providers are unable to ship items!") + "</td></tr>");
                    $('#btnShippingMethodContinue').hide();
                }

                $.each(data.d, function (index, value) {
                    if (CheckOut.UserCart.IsFShipping) {
                        shippingMethodElements += '<tr ><td><label><input name="shippingRadio" type="radio" value="' + value.ShippingMethodId + '" shippingName="' + value.ShippingMethodName + '" shippingCost=" ' + 0 + '"/>';
                    }
                    else {
                        shippingMethodElements += '<tr ><td><label><input name="shippingRadio" type="radio" value="' + value.ShippingMethodId + '" shippingName="' + value.ShippingMethodName + '" shippingCost=" ' + value.TotalCharges + '"/>';
                    }
                    if (value.ImagePath != "") {
                        shippingMethodElements += '<p class="cssClassCartPicture"><img  alt="" src="' + aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small') + '" height="83px" width="124px" /></p>';
                    }
                    shippingMethodElements += '<div class="cssClassCartPictureInformation">';
                    shippingMethodElements += '<h3>' + value.ShippingMethodName + '</h3>';
                    shippingMethodElements += '<p><b>' + getLocale(AspxCheckoutWithSingleAddress, "Delivery Time:") + value.DeliveryTime + '</b></p>';
                    shippingMethodElements += '</div></label></td>';
                    if (CheckOut.UserCart.IsFShipping) {
                        shippingMethodElements += '<tr><td id="Fshipping" shippingCost=' + 0 + '><p>' + getLocale(AspxCheckoutWithSingleAddress, 'Shipping & Handling Cost:') + '<span class="cssClassFormatCurrency">' + 0.00 + '</span> (Free Shipping) </p></td></tr>';
                    }
                    else {
                        shippingMethodElements += '<tr><td id="Fshipping" shippingCost=' + value.ShippingCost + '><p>' + getLocale(AspxCheckoutWithSingleAddress, 'Shipping & Handling Cost:') + '<span class="cssClassFormatCurrency">' + parseFloat(value.TotalCharges).toFixed(2) + '</span></p></td></tr>';
                    }
                    shippingMethodElements += '</tr>';
                    if (index == 0) {
                        shippingmethodId = value.ShippingMethodId;
                    }
                });

                $("#divShippingMethod").find("table>tbody").append(shippingMethodElements);
                $("#divShippingMethod").find("table>tbody tr:even").addClass("sfEven");
                $("#divShippingMethod").find("table>tbody tr:odd").addClass("sfOdd");
                CheckOut.UserCart.spMethodID = shippingmethodId;

                $("input[type='radio'][name='shippingRadio']").off().on("change", function () {
                    $(this).prop("checked", "checked");
                    CheckOut.UserCart.spMethodID = $(this).attr("value");
                    var spcost = $.trim($(this).attr("shippingCost"));
                    CheckOut.UserCart.spName = $(this).attr("shippingName");
                    CheckOut.UserCart.spCost = spcost;
                    $securepost("SSpC", {
                        v: spcost
                    }, function (data) { }, function () { });
                    $securepost("SSpN", {
                        v: $(this).attr("shippingName")
                    }, function (data) { }, function () { });
                    CheckOut.Discount.Init(spcost)
                    cartCalculator.AddOtherAmount("Shipping Cost:", parseFloat(spcost), cartCalculator.OperationType.add, 2);

                    cartCalculator.ShowData();
                });
                cartCalculator.ShowData();
                var cookieCurrency = $("#ddlCurrency").val();
                Currency.currentCurrency = BaseCurrency;
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
            },

            BindStateList: function (data) {
                if (data.d.length > 0) {
                    $("#ddlSPState").html('');
                    $('#ddlBLState').html('');
                    $('#popuprel .sfFormwrapper #ddlBLState').html('');
                    $.each(data.d, function (index, item) {
                        if (item.Text != 'NotExists') {
                            var option = '';
                            option += "<option class='cssBillingShipping'> " + item.Text + "</option>";
                            $('#ddlBLState').append(option);
                            $('#popuprel .sfFormwrapper #ddlBLState').append(option);
                            $('#ddlSPState').append(option);
                            $('#popuprel .sfFormwrapper #ddlSPState').append(option);
                            $('#ddlBLState').show();
                            $('#txtState').hide();
                            $('#ddlSPState').show();
                            $('#txtSPState').hide();
                            $('#popuprel .sfFormwrapper #ddlBLState').show();
                            $('#popuprel .sfFormwrapper #txtState').hide();
                            $('#popuprel .sfFormwrapper #ddlSPState').show();
                            $('#popuprel .sfFormwrapper #txtSPState').hide();
                        } else {
                            $('#ddlSPState').hide();
                            $('#txtSPState').show();
                            $('#ddlBLState').hide();
                            $('#txtState').show();
                            $('#popuprel .sfFormwrapper #ddlBLState').hide();
                            $('#popuprel .sfFormwrapper #txtState').show();
                            $('#popuprel .sfFormwrapper #ddlSPState').hide();
                            $('#popuprel .sfFormwrapper  #txtSPState').show();
                        }
                    });
                }
            },

            BindCountryList: function (data) {
                if (data.d.length > 0) {
                    $('#ddlBLCountry').html('');
                    $('#ddlSPCountry').html('');
                    $.each(data.d, function (index, item) {
                        var option = '';
                        option += "<option class='cssBillingShipping' value=" + item.Value + "> " + item.Text + "</option>";
                        $('#ddlBLCountry').append(option);
                        $('#ddlSPCountry').append(option);
                    });
                }
            },

            SetDiscountQuantityAmount: function (data) {
                CheckOut.UserCart.TotalDiscount = parseFloat(data.d).toFixed(2);
            },

            BindUserAddressOnUpdate: function () {
                CheckOut.BindUserAddress();
                $('#addBillingAddress ,#addShippingAddress').show();
               
                RemovePopUp();
            },

            SaveUserEmail: function (email, userModuleID, portalID, userName, userIP) {
                if (CheckOut.CheckPreviousEmailSubscription(email)) {
                    csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "You are already a subscribed member") + "</p>");
                    return false;
                }
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCoreHandler.ashx/SaveEmailSubscriber",
                    data: JSON2.stringify({ email: email, userModuleID: userModuleID, portalID: portalID, userName: userName, clientIP: userIP }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        csscody.info("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Email Subscribed Successfully") + "</p>");
                    }
                });
            },

            CheckPreviousEmailSubscription: function (email) {
                var bitval = true;
                $.ajax({
                    type: "POST",
                    async: false,
                    url: aspxservicePath + "AspxCoreHandler.ashx/CheckPreviousEmailSubscription",
                    data: JSON2.stringify({ email: email }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            bitval = true;
                        } else {
                            bitval = false;
                        }

                    },
                    error: function () {
                    }
                });
                return bitval;
            },

            BindTaxRate: function (data) {
                cartCalculator.CartTax = data.d;
                cartCalculator.ItemTax.Calculate();
            },
            GetPaymentGateWayLoadErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Error Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Failed to load payment gateway!") + "</p>");
            },
            GetAddressLoadErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Error Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Failed loading address...please hit refresh") + "</p>");
            },
            GetDiscountPriceLoadErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Error Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Error occured while using cart discount") + "</p>");
            },
            GetShippingMethodLoadErrorMsg: function () { },
            AddItemProp: function (itemId, key, value) {

                var items = CheckOut.UserCart.lstItems;
                if (items.length > 0) {

                    for (var z = 0; z < items.length; z++) {

                        if (CheckOut.UserCart.lstItems[z].ItemID == itemId) {

                            CheckOut.UserCart.lstItems[z][key] = value;
                            break;
                        }
                    }

                }
            },
            CheckAddressAlreadyExist: function () {
                this.config.async = false;
                this.config.method = "AspxCoreHandler.ashx/CheckAddressAlreadyExist";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = CheckOut.SetIfAddressAlreadyExist;
                this.ajaxCall(this.config);
                return CheckOut.Vars.checkIfExist;
            },
            SetIfAddressAlreadyExist: function (msg) {
                CheckOut.Vars.checkIfExist = msg.d;
            },
            Set: function (key, val) {
                CheckOut.UserCart[key] = val;

            },
            Init: function () {

                //Added to check at list one billing and shipping
                var radio = document.getElementsByName('billing');
                if (radio.length>0) {
                    var isChecked = 0; // default is 0 
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) isChecked = 1;
                    }

                    if (isChecked == 0)
                        radio[0].checked = "checked";
                }

                $('#dvBilling .cssClassCheckBox').show();
                $('#addBillingAddress').show();
                $('#addShippingAddress').show();
                var register = "";
                var checkouturl = "";
                register = 'sf/sfUser-Registration' + pageExtension;
                checkouturl = p.singleAddressCheckOutURL + pageExtension;
                $('.cssClassRegisterlnk').html('<a href ="' + AspxCommerce.utils.GetAspxRedirectPath() + register + '?ReturnUrl=' + AspxCommerce.utils.GetAspxRedirectPath() + checkouturl + '"><span class="cssClassRegisterLink">' + getLocale(AspxCheckoutWithSingleAddress, "Register") + '</span></a>');
                $accor = $("#tabs").tabs({ active: 0, disabled: [1, 2, 3, 4, 5] });
                var $billingSelect = $('#dvBillingSelect');
                var $billingInfo = $('#dvBillingInfo');
                var $shippingSelect = $('#dvShippingSelect');
                var $shippingInfo = $('#dvShippingInfo');
                $('#ddlBLState').hide();
                $('#ddlSPState').hide();
                $('#dvLogin').hide();
                $('#lblAuthCode').hide();
                $('#txtAuthCode').hide();
                $billingSelect.hide();
                $shippingSelect.hide();
                if (p.showSubscription.toLowerCase() == 'true') {
                    $('#chkNewLetter').parent("label:eq(0)").show();
                    $('#chkNewLetter').bind('click', function () {
                        var userModuleID = 0;
                        if ($(this).is(':checked') == true) {
                            $.ajax({
                                type: "POST",
                                url: aspxservicePath + "AspxCoreHandler.ashx/GetUserBillingEmail",
                                data: JSON2.stringify({ addressID: $('input[name="billing"]:checked').val(), aspxCommonObj: aspxCommonObj() }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function (msg) {
                                    var email = '';
                                    if (AspxCommerce.utils.GetUserName().toLowerCase() != 'anonymoususer') {
                                        email = msg.d;
                                    } else {
                                        email = $("#txtEmailAddress").val();
                                    }
                                    CheckOut.SaveUserEmail(email, userModuleID, AspxCommerce.utils.GetPortalID(), AspxCommerce.utils.GetUserName(), AspxCommerce.utils.GetClientIP());
                                }
                            });
                        } else {

                        }
                    });
                } else {
                    $('#chkNewLetter').parent("label:eq(0)").remove();
                }
                if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                    $billingInfo.hide();
                    $billingSelect.show();
                    CheckOut.UserCart.isUserGuest = false;
                    $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                    $accor.tabs('option', 'active', 1);
                    $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                    $('#btnBillingBack').hide();
                    $("#btnBillingBack").parent('label').removeClass('cssClassDarkBtn');
                    if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                        $('#dvBilling .cssClassCheckBox').hide();
                    } else {
                        $('#dvBilling .cssClassCheckBox').show();
                    }

                } else {
                    $('#btnBillingBack').show();
                    $('#btnBillingBack').removeAttr('style');
                    $("#btnBillingBack").parent('label').addClass('cssClassDarkBtn');
                }


                $('#rdbRegister').bind("click", function () {
                    $('#btnCheckOutMethodContinue').hide();
                    $('#dvLogin').show();
                    CheckOut.UserCart.isUserGuest = false;
                });
                $('#rdbGuest').bind("click", function () {
                    $('#btnCheckOutMethodContinue').show();
                    $('#dvLogin').hide();
                    $('#txtLoginEmail').val('');
                    $('#loginPassword').val('');
                    CheckOut.UserCart.isUserGuest = true;
                });
                var v = $("#form1").validate({
                    messages: {
                        FirstName: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        LastName: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        Email: {
                            required: '*',
                            email: '' + getLocale(AspxCheckoutWithSingleAddress, 'Please enter valid email id') + ''
                        },
                        Address1: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + ""
                        },
                        Address2: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + ""
                        },
                        Phone: {
                            required: '*',
                            maxlength: "*",
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 7 chars)") + ""
                        },
                        Company: {
                            maxlength: "*"
                        },
                        mobile: {
                            maxlength: "*",
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 10 digits)") + ""
                        },
                        Fax: {



                        },
                        City: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        stateprovince: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        biZip: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 4 chars)") + "",
                            maxlength: "*", alpha_dash: "* " + getLocale(AspxCheckoutWithSingleAddress, "(no special character allowed)") + ""
                        },
                        spFName: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        spLName: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        spAddress1: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        spAddress2: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        spCountry: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 4 chars)") + ""
                        },
                        spCity: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        SPCompany: {
                            maxlength: "*"
                        },
                        spZip: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 4 chars)") + "",
                            maxlength: "*",
                            alpha_dash: "* " + getLocale(AspxCheckoutWithSingleAddress, "(no special character allowed)") + ""

                        },
                        spstateprovince: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        spPhone: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 7 chars)") + ""
                        },
                        spmobile: {
                            maxlength: "*",
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 10 digits)") + ""
                        },
                        cardCode: {
                            required: '*',
                            minlength: "* " + getLocale(AspxCheckoutWithSingleAddress, "(at least 3 chars)") + ""
                        }
                    },
                    rules:
                                {
                                    creditCard: {
                                        required: true,
                                        creditcard: true
                                    },
                                    FirstName: { minlength: 2 },
                                    LastName: { minlength: 2 },
                                    Address1: { minlength: 2 },
                                    Address2: { minlength: 2 },
                                    Phone: { minlength: 7, digits: true },
                                    mobile: { minlength: 10, digits: true },
                                    City: { minlength: 2 },
                                    stateprovince: { minlength: 2 },
                                    biZip: { minlength: 4, alpha_dash: true },
                                    spFName: { minlength: 2 },
                                    spLName: { minlength: 2 },
                                    spAddress1: { minlength: 2 },
                                    spAddress2: { minlength: 2 },
                                    spCountry: { minlength: 4 },
                                    spCity: { minlength: 2 },
                                    spZip: { minlength: 4, alpha_dash: true },
                                    spstateprovince: { minlength: 2 },
                                    spPhone: { minlength: 7, digits: true },
                                    spmobile: { minlength: 10, digits: true },
                                    cardCode: { minlength: 3 },
                                    Fax: { digits: true }
                                },
                    ignore: ":hidden"
                });

                $('#ddlTransactionType').bind("change", function () {
                    if ($('#ddlTransactionType option:selected').text() == " CAPTURE_ONLY") {
                        $('#lblAuthCode').show();
                        $('#txtAuthCode').show();
                    } else {
                        $('#lblAuthCode').hide();
                        $('#txtAuthCode').hide();
                    }
                });

                $('#btnCheckOutMethodContinue').bind("click", function () {
                    if (option.cartCount > 0) {
                        if (v.form()) {
                            if ($('#rdbGuest').prop('checked') == true) {
                                $('#chkNewLetter').parent("label:eq(0)").show();
                                $accor = $("#tabs").tabs({ disabled: [0, 2, 3, 4, 5] });
                                $accor.tabs('option', 'active', 1);
                                $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                                $billingInfo.show();
                                if (CheckOut.UserCart.isUserGuest) {
                                    $("#trShippingAddress").hide();
                                    $("#trBillingAddress").hide();
                                }
                                $billingSelect.hide();
                                if ($('#rdbGuest').is(':checked')) {
                                    $('#password').hide();
                                    $('#confirmpassword').hide();
                                } else {
                                    $('#password').show();
                                    $('#confirmpassword').show();
                                }
                            }
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please fill the form correctly.") + "</p>");
                        }
                    }
                    else {
                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "You Cart is Empty.") + "</p>");
                    }
                });


                $('#btnBillingContinue').click(function () {
                    if (option.cartCount > 0) {
                        if (v.form()) {
                            var sAddress = "";
                            if (IsABTestInstalled.toLowerCase() == "true") {
                                ABTest.ABTestSaveConversion();
                            }
                            if ($("#chkBillingAsShipping").is(":visible") == false) {
                                CheckOut.Vars.AddressID = $('#dvBillingSelect input:radio:checked').val();
                                CheckOut.GetUserCartDetails();
                            }
                            $("input[type='radio'][name='shippingRadio']:first").prop('checked', 'checked').trigger('change');
                            if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                                CheckOut.UserCart.IsDownloadItemInCartFull = true;
                            } else {
                                CheckOut.UserCart.IsDownloadItemInCartFull = false;
                            }

                            var bill = $('#chkBillingAsShipping').is(':checked') == true ? "both" : "billing";
                            var x = CheckOut.CheckShippingAndBillingCountry(bill);

                            if (x.AllowBilling == true) {

                            } else if (x.AllowBilling == false) {
                                csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                                return false;
                            }

                            CheckOut.AssignItemsDetails();
                            if (CheckOut.UserCart.isUserGuest == false) {
                                if (!$("input[name=billing]").is(":checked")) {
                                    $('#addBillingAddress').show();
                                    csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please select billing address to checkout.") + "</p>");
                                    return false;
                                } else {
                                    CheckOut.BindBillingData();
                                    if ($('#txtFirstName').val() == '') {
                                        $shippingInfo.hide();
                                        $shippingSelect.show();
                                    } else {
                                        $shippingInfo.show();
                                        $shippingSelect.hide();
                                    }
                                    if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                        CheckOut.AssignItemsDetails();
                                        $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                        $accor.tabs('option', 'active', 4);
                                        $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                        CheckOut.UserCart.spCost = 0.00;
                                        $securepost("SSpC", {
                                            v: CheckOut.UserCart.spCost
                                        }, function (data) { }, function () { });

                                        return false;
                                    }
                                    if ($('#chkBillingAsShipping').is(':checked')) {
                                        CheckOut.UserCart.objTaxList = [];
                                        if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                                            CheckOut.Vars.AddressID = $('#dvBillingSelect input:radio:checked').val();
                                            CheckOut.UserCart.spAddressID = $('#dvBillingSelect input:radio:checked').val();
                                            CheckOut.GetUserCartDetails();
                                        }
                                        CheckOut.BindShippingData();
                                        var x = CheckOut.CheckShippingAndBillingCountry("both");
                                        if (x.AllowBilling == true && x.AllowShipping == true) {

                                        } else if (x.AllowBilling == true && x.AllowShipping == false) {
                                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, " Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                                            return false;
                                        } else if (x.AllowBilling == false && x.AllowShipping == true) {
                                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                                            return false;
                                        } else {
                                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing and Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");

                                            return false;
                                        }
                                        if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                            $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                            $accor.tabs('option', 'active', 4);
                                            $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                            $("#txtShippingTotal").val(0);
                                            CheckOut.UserCart.spCost = 0.00;
                                            $securepost("SSpC", {
                                                v: CheckOut.UserCart.spCost
                                            }, function (data) { }, function () { });
                                            return false;
                                        } else {
                                            $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                                            $accor.tabs('option', 'active', 3);
                                            $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                                        }
                                    } else {
                                        $('#dvCPShipping').html('');
                                        $accor = $("#tabs").tabs({ active: 2, disabled: [0, 1, 3, 4, 5] });
                                        $accor.tabs('option', 'active', 2);
                                        $accor.tabs({ deactive: [0, 1, 3, 4, 5] });
                                    }
                                }
                            } else {
                                CheckOut.BindBillingData();
                                if ($('#txtFirstName').val() == '') {
                                    $shippingInfo.hide();
                                    $shippingSelect.show();
                                } else {
                                    $shippingInfo.show();
                                    $shippingSelect.hide();
                                }
                                if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                    $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                    $accor.tabs('option', 'active', 4);
                                    $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                    $("#txtShippingTotal").val(0);
                                    CheckOut.UserCart.spCost = 0.00;
                                    $securepost("SSpC", {
                                        v: CheckOut.UserCart.spCost
                                    }, function (data) { }, function () { });
                                    return false;
                                }
                                if ($('#chkBillingAsShipping').is(':checked')) {
                                    if (AspxCommerce.utils.GetUserName() == 'anonymoususer') {
                                        CheckOut.UserCart.objTaxList = [];
                                        CheckOut.Vars.Country = $('#ddlBLCountry option:selected').text();
                                        CheckOut.Vars.State = $('#txtState').val();
                                        CheckOut.Vars.Zip = $('#txtZip').val();
                                        CheckOut.Vars.AddressID = 0;
                                        CheckOut.GetUserCartDetails();
                                        var state = "";
                                        if ($("#txtState").is(":visible")) {
                                            state = $('#txtState').val();
                                        }
                                        else {
                                            state = $('#ddlBLState').val();
                                        }
                                        sAddress = {
                                            City: $('#txtCity').val(),
                                            Country: $('#ddlBLCountry option:selected').text(),
                                            Address: 0,
                                            State: state,
                                            Zip: $('#txtZip').val(),
                                            Address2: $('#txtAddress2').val()
                                        };
                                    }
                                    CheckOut.BindShippingData();
                                    var x = CheckOut.CheckShippingAndBillingCountry("both");
                                    if (x.AllowBilling == true && x.AllowShipping == true) {

                                    } else if (x.AllowBilling == true && x.AllowShipping == false) {
                                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, " Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");

                                        return false;
                                    } else if (x.AllowBilling == false && x.AllowShipping == true) {
                                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, " Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                                        return false;
                                    } else {
                                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing and Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, " Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");

                                        return false;
                                    }
                                    if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                        CheckOut.AssignItemsDetails();
                                        $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                        $accor.tabs('option', 'active', 4);
                                        $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                        CheckOut.UserCart.spCost = 0.00;
                                        $securepost("SSpC", {
                                            v: CheckOut.UserCart.spCost
                                        }, function (data) { }, function () { });
                                        return false
                                    } else {
                                        $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                                        $accor.tabs('option', 'active', 3);
                                        $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                                    }
                                } else {
                                    $('#dvCPShipping').html('');
                                    $accor = $("#tabs").tabs({ active: 2, disabled: [0, 1, 3, 4, 5] });
                                    $accor.tabs('option', 'active', 2);
                                    $accor.tabs({ deactive: [0, 1, 3, 4, 5] });
                                }
                            }
                        }
                        if ($('#chkBillingAsShipping').is(':checked')) {
                            var id = CheckOut.Vars.AddressID;
                            if (id != 0) {
                                sAddress = CheckOut.GetUserSelectedShippingAddress(id);
                            }
                            CheckOut.GetShippinMethodsFromWeight(basketItems, sAddress);
                        }
                    }
                    else {
                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "You Cart is Empty.") + "</p>");
                    }

                });

                $('#btnBillingBack').bind("click", function () {

                    if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                        $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                        $accor.tabs('option', 'active', 1);
                        $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                    } else {
                        $('#dvCPBilling').html('');
                        $accor = $("#tabs").tabs({ active: 0, disabled: [1, 2, 3, 4, 5] });
                        $accor.tabs('option', 'active', 0);
                        $accor.tabs({ deactive: [1, 2, 3, 4, 5] });
                    }
                });

                $('#btnShippingContinue').bind("click", function () {
                    CheckOut.UserCart.objTaxList = [];
                    if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                        if (!$("input[name=shipping]").is(":checked")) {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please select shipping address to checkout.") + "</p>");

                            return false;
                        }
                        var id = CheckOut.Vars.AddressID = $('#dvShippingSelect input:radio:checked').val();
                        CheckOut.UserCart.spAddressID = id;
                        var sAddress = CheckOut.GetUserSelectedShippingAddress(id);
                        CheckOut.GetShippinMethodsFromWeight(basketItems, sAddress);
                        CheckOut.GetUserCartDetails();

                    } else {
                        CheckOut.Vars.Country = $('#ddlSPCountry option:selected').text();
                        CheckOut.Vars.State = $('#txtSPState').val();
                        CheckOut.Vars.Zip = $('#txtSPZip').val();
                        CheckOut.Vars.AddressID = 0;

                        var sipAddress = {};
                        sipAddress.FirstName = Encoder.htmlEncode($('#txtSPFirstName').val());
                        sipAddress.LastName = Encoder.htmlEncode($('#txtSPLastName').val());
                        sipAddress.EmailAddress = $('#txtSPEmailAddress').val();

                        sipAddress.CompanyName = Encoder.htmlEncode($('#txtSPCompany').val());
                        sipAddress.Address = Encoder.htmlEncode($('#txtSPAddress').val());
                        sipAddress.Address2 = Encoder.htmlEncode($('#txtSPAddress2').val());
                        sipAddress.City = Encoder.htmlEncode($('#txtSPCity').val());
                        sipAddress.Zip = $('#txtSPZip').val();
                        sipAddress.Country = $('#ddlSPCountry option[value=' + $.trim($('#ddlSPCountry').val()) + ']').text();//$('#hdnCountryList option[value=' + $.trim($('#ddlSPCountry').val()) + ']').text();
                        sipAddress.Phone = $('#txtSPPhone').val();
                        sipAddress.Mobile = $('#txtSPMobile').val();
                        tempUserAddresses = [];
                        tempUserAddresses.push(sipAddress);

                        CheckOut.GetShippinMethodsFromWeight(basketItems, tempUserAddresses[0]);
                        CheckOut.GetUserCartDetails();
                    }
                    if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                        CheckOut.UserCart.IsDownloadItemInCartFull = true;
                    } else {
                        CheckOut.UserCart.IsDownloadItemInCartFull = false;
                    }
                    if (v.form()) {
                        var x = CheckOut.CheckShippingAndBillingCountry("both");
                        if (x.AllowBilling == true && x.AllowShipping == true) {

                        } else if (x.AllowBilling == true && x.AllowShipping == false) {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                            return false;

                        } else if (x.AllowBilling == false && x.AllowShipping == true) {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                            return false;
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Selected Billing and Shipping Address is not applicable.") + "</p><p>" + getLocale(AspxCheckoutWithSingleAddress, "Click here for") + " <a href=" + AspxCommerce.utils.GetAspxRedirectPath() + p.shippingDetailPage + pageExtension + ">" + getLocale(AspxCheckoutWithSingleAddress, "more information") + "</a> </p>");
                            return false;
                        }

                        if (CheckOut.UserCart.isUserGuest == false) {
                            if ($.trim($('#ddlShipping').text()) == '' || $.trim($('#ddlShipping').text()) == null) {
                                $('#addShippingAddress').show();
                                return false;
                            } else {
                                CheckOut.BindShippingData();
                                if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                    $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                    $accor.tabs('option', 'active', 4);
                                    $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                    $("#txtShippingTotal").val(0);
                                    CheckOut.UserCart.spCost = 0.00;
                                } else {
                                    $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                                    $accor.tabs('option', 'active', 3);
                                    $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                                }
                            }
                        } else {
                            CheckOut.BindShippingData();
                            if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                                $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                                $accor.tabs('option', 'active', 4);
                                $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                                $("#txtShippingTotal").val(0);
                                CheckOut.UserCart.spCost = 0.00;
                                $securepost("SSpC", {
                                    v: CheckOut.UserCart.spCost
                                }, function (data) { }, function () { });
                            } else {
                                $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                                $accor.tabs('option', 'active', 3);
                                $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                            }
                        }
                    }
                });

                $('#btnShippingBack').bind("click", function () {
                    $('#dvCPShipping').html('');
                    $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                    $accor.tabs('option', 'active', 1);
                    $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                    if ($('#chkBillingAsShipping').prop('checked') == 'true' || $('#chkBillingAsShipping').prop('checked') == true) {
                        $('#chkBillingAsShipping').prop('checked', false);
                    }
                });

                $('#btnShippingMethodBack').bind("click", function () {

                    if ($('#chkBillingAsShipping').is(':checked')) {
                        $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                        $accor.tabs('option', 'active', 1);
                        $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                    } else {
                        $accor = $("#tabs").tabs({ active: 2, disabled: [0, 1, 3, 4, 5] });
                        $accor.tabs('option', 'active', 2);
                        $accor.tabs({ deactive: [0, 1, 3, 4, 5] });
                    }
                    $("#chkBillingAsShipping").prop("checked", false);
                });

                $('#btnShippingMethodContinue').bind("click", function () {
                    $('#btnShippingMethodContinue').show();
                    var count = 0;
                    CheckOut.AssignItemsDetails();

                    $("input[type='radio'][name='shippingRadio']").each(function () {
                        if ($(this).is(':checked') == true)
                            count = 1;
                    });
                    cartCalculator.GiftCard.Init();
                    if (count == 1) {
                        CheckOut.AssignItemsDetails();
                        cartCalculator.ShowData();

                        CheckOut.BindShippingMethodData();
                        $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                        $accor.tabs('option', 'active', 4);
                        $accor.tabs({ deactive: [0, 1, 2, 3, 5] });

                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please check at least one shipping method.") + "</p>");

                    }

                });


                $('#btnPaymentInfoContinue').bind("click", function () {
                    var Total = 0;
                    var disAmount = 0;
                    if (v.form()) {

                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/BindStateList",
                            data: JSON2.stringify({ countryCode: $('#ddlBLCountry :selected').val() }),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d.length > 2) {
                                    CheckOut.BillingAddress.state = $('#ddlBLState :selected').text();
                                    CheckOut.ShippingAddress.spState = $('#ddlSPState :selected').text();// taken form below ajax call
                                }
                            }
                        });


                        if ($('#dvPGList input:radio:checked').prop('checked') == 'checked' || $('#dvPGList input:radio:checked').prop('checked') == true) {
                            if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {



                            }

                            CheckOut.LoadControl($('#dvPGList input:radio:checked').attr('source'), $.trim($('#dvPGList input:radio:checked').attr('friendlyname')));

                            $securepost("SGtw", {
                                v: $('#dvPGList input:radio:checked').prop('value')
                            }, function (data) { }, function () { });

                            if ($.trim($('#dvPGList input:radio:checked').attr('friendlyname').toLowerCase()) == 'aimauthorize') {
                                if ($('#AIMChild').length > 0) {
                                    CheckOut.BindPaymentData();
                                    cartCalculator.ShowData();
                                    $accor = $("#tabs").tabs({ active: 5, disabled: [0, 1, 2, 3, 4] });
                                    $accor.tabs('option', 'active', 5);
                                    $accor.tabs({ deactive: [0, 1, 2, 3, 4] });

                                    if (cartCalculator.Calculate() < 0) {
                                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Your cart is not eligible to checkout due to a negatve total amount!") + "</p>");

                                        $('#dvPlaceOrder .sfButtonwrapper ').find('input').parents('label:eq(0)').not("#btnPlaceBack").remove();
                                        $("#dvPGList input:radio").prop("disabled", "disabled");
                                    } else {
                                        $("#dvPGList input:radio").prop("disabled", false);
                                    }

                                } else {
                                }

                            } else {
                                CheckOut.BindPaymentData();
                                cartCalculator.ShowData();
                                $accor = $("#tabs").tabs({ active: 5, disabled: [0, 1, 2, 3, 4] });
                                $accor.tabs('option', 'active', 5);
                                $accor.tabs({ deactive: [0, 1, 2, 3, 4] });
                                if (cartCalculator.Calculate() < 0) {
                                    csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Your cart is not eligible to checkout due to a negatve total amount!") + "</p>");
                                    $('#dvPlaceOrder .sfButtonwrapper ').find('input').not("#btnPlaceBack").remove();
                                    $("#dvPGList input:radio").prop("disabled", "disabled");
                                } else {
                                    $("#dvPGList input:radio").prop("disabled", false);
                                }

                            }

                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Message") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please select your payment system.") + "</p>");
                        }
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithSingleAddress, "Information Alert") + "</h2><p>" + getLocale(AspxCheckoutWithSingleAddress, "Please fill the form correctly.") + "</p>");
                    }


                });

                $('#btnPaymentInfoBack').bind("click", function () {
                    $('#dvCPPaymentMethod').html('');
                    if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                        $accor = $("#tabs").tabs({ active: 1, disabled: [0, 2, 3, 4, 5] });
                        $accor.tabs('option', 'active', 1);
                        $accor.tabs({ deactive: [0, 2, 3, 4, 5] });
                        $("#txtShippingTotal").val(0);
                        CheckOut.UserCart.spCost = 0.00;
                        $securepost("SSpC", {
                            v: CheckOut.UserCart.spCost
                        }, function (data) { }, function () { });
                    } else {
                        $accor = $("#tabs").tabs({ active: 3, disabled: [0, 1, 2, 4, 5] });
                        $accor.tabs('option', 'active', 3);
                        $accor.tabs({ deactive: [0, 1, 2, 4, 5] });
                    }
                });

                $('#btnPlaceBack').bind("click", function () {
                    $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                    $accor.tabs('option', 'active', 4);
                    $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                });

                $('#chkBillingAsShipping').bind("click", function () {
                    CheckOut.AddBillingAsShipping();
                });

                $(document).on("change", "#ddlSPCountry ,#ddlBLCountry ,input:radio[name=billing]", function () {
                    if ($("#chkBillingAsShipping").is(":checked")) {
                        $("#chkBillingAsShipping").removeAttr("checked");
                        CheckOut.AddBillingAsShipping();
                    }
                    CheckOut.GetState($(this).val());
                    if (this.id == "ddlBLCountry") {
                        if ($("#chkBillingAsShipping").is(":checked")) {
                            $("#chkBillingAsShipping").click();
                        }
                    }
                });

                $(".cssClassClose").bind("click", function () {
                    RemovePopUp();
                });


                $('#addBillingAddress,#addShippingAddress').bind("click", function () {
                    if (p.allowMultipleAddress.toLowerCase() == 'false') {
                        var checkExist = CheckOut.CheckAddressAlreadyExist();
                        if (checkExist) {
                            csscody.alert('<h2>' + getLocale(AspxCheckoutWithSingleAddress, 'Alert Message') + '</h2><p>' + getLocale(AspxCheckoutWithSingleAddress, 'Multiple address book is disabled.') + '</p>');
                            return false;
                        }
                    }
                    $("#ddlBLState").hide();
                    $("#txtState").show();
                    $("#txtState").val('');
                    $('#popuprel .sfFormwrapper div.billingTable').empty();
                    $('<div class="billingTable">' + $('#dvBillingInfo').html() + '</div>').insertBefore('#popuprel .sfFormwrapper .sfButtonwrapper');
                    CheckOut.ClearAll();
                    switch ($(this).prop('id')) {
                        case "addBillingAddress":
                            ShowPopupControl("popuprel");
                            $('#trBillingAddress ,#trShippingAddress').show();
                            break;
                        case "addShippingAddress":
                            $('#popuprel .sfFormwrapper div ul:nth-child(7)').remove();
                            ShowPopupControl("popuprel");
                            break;
                    }
                    //$("#popuprel .sfFormwrapper div #ddlSPCountry ,#popuprel .sfFormwrapper div #ddlBLCountry").bind("change", function () {
                    //    CheckOut.GetState($(this).val());
                    //});
                });
                $('#btnSubmitAddress').bind("click", function () {
                    if (document.getElementsByName('billing').length > 0) {
                        selectedBillingAddressBeforeAddingNewAddress = $("input:radio[name=billing]:checked").val();
                    }
                    if (v.form()) {
                        CheckOut.AddUpdateUserAddress();
                    }
                });
                $('#trBillingAddress , #trShippingAddress').show();
                $('#addBillingAddress , #addShippingAddress').show();
                if ($.trim($('#ddlBilling').text()) == "" || $.trim($('#ddlBilling').text()) == null) {

                    $('#addBillingAddress').show();
                } else {
                    $('#addBillingAddress').show();
                }
                if ($.trim($('#ddlShipping').text()) == "" || $.trim($('#ddlShipping').text()) == null) {
                    $('#addShippingAddress').show();
                } else {
                    $('#addShippingAddress').show();
                }

                CheckOut.CheckDownloadableOnlyInCart();
                if (CheckOut.UserCart.IsDownloadItemInCartFull || CheckOut.UserCart.ShowShippingAdd) {
                    $('#dvBilling .cssClassCheckBox').hide();
                } else {
                    $('#dvBilling .cssClassCheckBox').show();
                }

                $("#ddlShipping input[name=shipping]").bind("change", function () {
                    CheckOut.UserCart.objTaxList = [];
                });
                $("#txtFirstName,#txtLastName,#txtEmailAddress,#txtCompanyName,#txtAddress1,#txtAddress2,#txtCity,#txtState,#txtZip,#txtPhone,#txtMobile,#txtFax,#txtWebsite").bind("keypress", function () {

                    if ($("#chkBillingAsShipping").is(":checked")) {
                        $("#chkBillingAsShipping").removeAttr("checked");
                        CheckOut.AddBillingAsShipping();
                    }
                });
                $("#SingleCheckOut").show();
                $(".cssClassRightAccordainMenu").show();
            }
        };
        CheckOut.Init();
        eval(p.InitScript);
        CheckOutApi = function () {


            return {
                Get: function () { return CheckOut; }
                   , GiftCard: cartCalculator.GiftCard,
                CalcAPI: cartCalculator,
                Set: CheckOut.Set,
                SetItem: CheckOut.AddItemProp,
                GetUserCartDetails: CheckOut.GetUserCartDetails
            }
        }();
        setTimeout(function () {
            CheckOut.RewardPoints.Init();
        }, 1000);

    };

    $.fn.SingleCheckout = function (p) {
        $.checkoutView(p);
    };

})(jQuery);
