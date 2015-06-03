<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlaceOrderGiftCard.ascx.cs"
    Inherits="PlaceOrderGiftCard" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxGiftCard
        });
    });
    //<![CDATA[
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    $(function () {
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

        var _checkoutVars = "";

        var tempCheckout = CheckOutApi.Get();
        var api = tempCheckout;
        var AspxOrder = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0, checkCartExist: false,
                sessionValue: "",
                error: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: AspxOrder.config.type,
                    contentType: AspxOrder.config.contentType,
                    cache: AspxOrder.config.cache,
                    async: AspxOrder.config.async,
                    url: AspxOrder.config.url,
                    data: AspxOrder.config.data,
                    dataType: AspxOrder.config.dataType,
                    success: AspxOrder.ajaxSuccess,
                    error: AspxOrder.ajaxFailure
                });
            },
            CheckCustomerCartExist: function () {
                var isExist = false;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: AspxCommerce.utils.GetAspxServicePath() + 'AspxCoreHandler.ashx/CheckCustomerCartExist',
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj() }),
                    dataType: "json",
                    success: function (data) {

                        AspxOrder.config.checkCartExist = data.d;
                        isExist = data.d.CartStatus;
                    },
                    error: null
                });
                return isExist;
            },
            GiftCard: function () {
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
                var tempPayment;
                var addGiftCardInPayment = function (isTotalZero, restamount) {

                    if (isTotalZero) {
                        tempPayment = $("#dvPGList").clone();
                        $("#dvinfo").remove();
                        var gC = $("input[name=PGLIST][realname='Gift Card']").parents('label:eq(0)');
                        $("#dvPGList").find('label').not($("input[name=PGLIST][realname='Gift Card']").parents('label:eq(0)')).remove();
                        $("#dvPGList br").remove();
                        $("input[name=PGLIST][realname='Gift Card']").trigger('click');
                        var dvInfo = $("<div id=dvinfo>").html("<p>" + getLocale(AspxGiftCard, 'Gift Card has been applied to your cart') + "</p><p>" + getLocale(AspxGiftCard, 'your total due amount') + ' ' + restamount + " .</p>");
                        $("#dvPaymentInfo").append(dvInfo);
                        $("#dvPaymentInfo").find('div.sfFormwrapper').hide();
                        $("#btnPlaceOrderGiftCard").parents('div:eq(0)').find('label.cssClassGreenBtn').not($("#btnPlaceOrderGiftCard").parents('label:eq(0)')).remove();
                    } else {
                        $("#dvinfo").remove();
                        $("#dvPaymentInfo").find('div.sfFormwrapper').show();
                        $("#btnPlaceOrderGiftCard").parents('label:eq(0)').remove();
                        var dvInfo = $("<div id=dvinfo>").html("<p>" + getLocale(AspxGiftCard, 'Gift Card has been applied to your cart') + "</p><p>" + getLocale(AspxGiftCard, 'your total due amount') + restamount + " .</p><p>" + getLocale(AspxGiftCard, 'To checkout you can still apply giftcard or choose another payment option') + "</p>");
                        $("#dvPaymentInfo").append(dvInfo);
                    }
                };
                var recentlyUsedGiftCard = [];
                var cartTotal = 0;
                var applyGiftCard = function (veriftication) {
                    var showOtherPaymnetOption = false;
                    var $td = $("<td>");
                    var $content = $("<strong>").append("GiftCard (" + veriftication.GiftCardCode + ")");
                    var $span = $("<span>").addClass("cssClassDeleteGiftCard").attr('data-id', veriftication.GiftCardCode).append("<sup><a href='#'>x</a></sup>");

                    $span.bind("click", function () {
                        var code = $.trim($(this).attr('data-id'));

                        deleteUsed(recentlyUsedGiftCard, code);
                        $(this).parents("tr:eq(0)").remove();
                        api.UserCart.GiftCardDetail = recentlyUsedGiftCard;
                        CheckOutApi.CalcAPI.GiftCard.Hold(recentlyUsedGiftCard);
                        CheckOutApi.CalcAPI.ShowData();

                        $securepost("SGC", {
                            v: JSON.stringify(recentlyUsedGiftCard)
                        }, function (data) { }, function () { });


                        csscody.info("<h2>" + getLocale(AspxGiftCard, 'Information Success') + "</h2><p>" + getLocale(AspxGiftCard, 'Gift Card removed successfully!') + "</p>");




                        $accor = $("#tabs").tabs({ active: 4, disabled: [0, 1, 2, 3, 5] });
                        $accor.tabs('option', 'active', 4);
                        $accor.tabs({ deactive: [0, 1, 2, 3, 5] });
                        api.LoadPGatewayList();
                        $('#dvPGList input[name="PGLIST"]:first').trigger('click');



                        CheckOutApi.CalcAPI.GiftCard.Init();



                    });
                    $td.append($content.append($span));
                    var $tdamount = $("<td>").append("<span class=\"cssClassNegative\">-</span>");
                    var total = CheckOutApi.CalcAPI.Calculate();


                    var cardBalance = 0;
                    if (total >= veriftication.Balance) {
                        $tdamount.append("<label>" + veriftication.Balance + "</label> ");
                        var restAmount = parseFloat(total - veriftication.Balance);
                        cardBalance = 0;
                        veriftication.ReducedAmount = veriftication.Balance;

                        showOtherPaymnetOption = true;
                        addGiftCardInPayment(false, parseFloat(restAmount).toFixed(2));
                    } else {
                        cardBalance = veriftication.Balance - total;
                        veriftication.ReducedAmount = total;
                        $tdamount.append("<label>" + total + "</label> ");
                        showOtherPaymnetOption = false;
                        addGiftCardInPayment(true, 0);
                    }


                    api.UserCart.GiftCardDetail = recentlyUsedGiftCard;
                    CheckOutApi.CalcAPI.GiftCard.Hold(recentlyUsedGiftCard);
                    CheckOutApi.CalcAPI.ShowData();

                    if (recentlyUsedGiftCard.length > 0) {
                        $securepost("SGC", {
                            v: JSON.stringify(recentlyUsedGiftCard)
                        }, function (data) { }, function () { });
                    }
                };



                var verify = function () {
                    var aspxCommonInfo = aspxCommonObj();
                    aspxCommonInfo.CultureName = null;
                    aspxCommonInfo.UserName = null;
                    aspxCommonInfo.CustomerID = null;
                    var veriftication = { IsVerified: false, ReducedAmount: 0 };
                    var param = JSON2.stringify({ giftcardCode: $.trim($("#txtGiftCardCode").val()), pinCode: $.trim($("#txtGiftCardPinCode").val()), aspxCommonObj: aspxCommonInfo });
                    $ajaxCall("VerifyGiftCard", param, function (data) {
                        if (data.d != null && data.d !="") {
                            if (data.d.IsVerified) {
                                veriftication.IsVerified = data.d.IsVerified;
                                veriftication.Price = data.d.Price;
                                veriftication.Balance = data.d.Balance;
                                veriftication.GiftCardCode = data.d.GiftCardCode;
                                veriftication.GiftCardId = data.d.GiftCardId;

                                recentlyUsedGiftCard.push(veriftication);

                                applyGiftCard(veriftication);
                                csscody.info("<h2>" + getLocale(AspxGiftCard, 'Information Success') + "</h2><p>" + getLocale(AspxGiftCard, 'Gift Card has been applied successfully!') + "</p>");
                                $("#txtGiftCardCode").val('');
                                $("#txtGiftCardPinCode").val('');
                            }
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxGiftCard, 'Information Message') + "</h2><p>" + getLocale(AspxGiftCard, 'Invalid or Expired giftcard code!') + "</p>");
                        }
                    }, null);
                    return veriftication;
                };
                var checkUsed = function (arr, code) {
                    var isExist = false;
                    if (arr.length > 0) {
                        for (var i = 0; i < arr.length; i++) {
                            if (arr[i].GiftCardCode == code) {
                                isExist = true;
                                break;
                            }
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

                var getReducedAmount = function (arr, code) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].GiftCardCode == code) {
                            return arr[i].ReducedAmount;
                            break;
                        }
                    }
                    return 0;
                };
                var init = function () {

                    var isValidForm = $("#form1").validate({
                        messages: {
                            giftcardpincode: {
                                required: '*'
                            },
                            giftcardcode: {
                                required: '*'
                            }
                        },
                        rules:
                            {
                                giftcardpincode: { required: true, number: true },
                                giftcardcode: { required: true }
                            },
                        ignore: ":hidden"
                    });
                    recentlyUsedGiftCard = CheckOutApi.CalcAPI.GiftCard.GiftCardList();
                    $("#btnAppplyGiftCard").unbind('click').bind("click", function () {
                        var code = $.trim($("#txtGiftCardCode").val());
                        var pin = $.trim($("#txtGiftCardPinCode").val());
                        if (code != "" && pin != "") {
                            if (isValidForm.form()) {
                                if (!checkUsed(recentlyUsedGiftCard, code)) {
                                    verify();
                                } else {
                                    csscody.alert("<h2>" + getLocale(AspxGiftCard, 'Information Message') + "</h2><p>" + getLocale(AspxGiftCard, 'Invalid or Expired giftcard code!') + "</p>");
                                }
                            }
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxGiftCard, 'Information Message') + "</h2><p>" + getLocale(AspxGiftCard, 'Please enter required field!') + "</p>");
                        }
                    });


                    $("#btnCheckBalance").unbind('click').bind("click", function () {
                        window.open(AspxCommerce.utils.GetAspxRedirectPath() + "Balance-Inquiry" + pageExtension);
                    });


                };
                var clearGiftCard = function () {
                    var param = JSON2.stringify({ key: "UsedGiftCard", value: [] });
                    $ajaxCall("SetSessionVariable", param, null, null);
                };

                return {
                    Init: init
                };

            }(),
            init: function () {

                $('#btnPlaceOrderGiftCard').click(function () {
                    $securepost("GCVs", "", function (data) {
                        _checkoutVars = $.parseJSON(data.d);
                    }, null);
                    if (api.UserCart.IsGiftCardUsed) {
                        if (!CheckOutApi.CalcAPI.GiftCard.CheckGiftCardIsUsed()) {
                            CheckOutApi.CalcAPI.GiftCard.ResetGiftCard();
                            SageFrame.messaging.show("Applied Gift Card has insufficient balance.Please veriry once again!", "Alert");
                            return false;
                        }
                    }
                    var aspxCommonInfo = aspxCommonObj();
                    var checkIfCartExist = AspxOrder.CheckCustomerCartExist();
                    if (!checkIfCartExist) {
                        csscody.alert("<h2>" + getLocale(AspxGiftCard, 'Information Alert') + "</h2><p>" + getLocale(AspxGiftCard, 'Your cart has been emptied already!!') + "</p>");
                        return false;
                    }
                    if ($('#SingleCheckOut').length > 0) {

                        AspxOrder.AddOrderDetailsTest();
                    } else {
                        AspxOrder.SendDataForPaymentTestMulti();
                    }
                });
                AspxOrder.GiftCard.Init();
            },
            ajaxSuccess: function (data) {
                switch (AspxOrder.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        AspxOrder.config.checkCartExist = data.d;

                        break;
                    case 2:
                        AspxOrder.config.sessionValue = parseFloat(data.d);
                        break;
                    case 3:
                        document.location = AspxCommerce.utils.GetAspxRedirectPath() + "Gift-Card-Success" + pageExtension;
                        if (allowRealTimeNotifications.toLowerCase() == 'true') {
                            UpdateNotifications(2);
                        }
                        break;
                    case 4:
                        document.location = AspxCommerce.utils.GetAspxRedirectPath() + "Gift-Card-Success" + pageExtension;
                        if (allowRealTimeNotifications.toLowerCase() == 'true') {
                            UpdateNotifications(2);
                        }
                        break;
                }
            },
            ajaxFailure: function () {
                switch (AspxOrder.config.error) {
                    case 3:
                        break;
                    case 4:
                        break;
                }
            },

            getSession: function (Key) {
                this.config.method = "AspxCoreHandler.ashx/GetSessionVariableCart";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ key: Key });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
                return AspxOrder.config.sessionValue;
            },
            SendDataForPaymentTestMulti: function () {
                var creditCardTransactionType = $('#ddlTransactionType option:selected').text();
                var cardNo = $('#txtCardNo').val();
                var cardCode = $('#txtCardCode').val();
                var CardType = $('#cardType option:selected').text();
                var expireDate;
                expireDate = $('#lstMonth option:selected').text();
                expireDate += $('#lstYear option:selected').text();

                var accountNumber = $('#txtAccountNumber').val();
                var routingNumber = $('#txtRoutingNumber').val();
                var accountType = $('#ddlAccountType option:selected').text();
                var bankName = $('#txtBankName').val();
                var accountHoldername = $('#txtAccountHolderName').val();
                var checkType = $('#ddlChequeType option:selected').text();
                var checkNumber = $('#txtChequeNumber').val();
                var recurringBillingStatus = false;

                if ($('#chkRecurringBillingStatus').is(':checked'))
                    recurringBillingStatus = true;
                else
                    recurringBillingStatus = false;

                var paymentMethodName = "Gift Card";
                var paymentMethodCode = "Gift Card";
                var isBillingAsShipping = false;


                if ($('#chkBillingAsShipping').is(":checked"))
                    api.BillingAddress.IsBillingAsShipping = true;
                else
                    api.BillingAddress.IsBillingAsShipping = false;

                var orderRemarks = Encoder.htmlEncode($("#txtAdditionalNote").val());
                var currencyCode = '<%=MainCurrency %>';
                var isTestRequest = "TRUE";
                var isEmailCustomer = "TRUE";
                var discountAmount = "";
                var taxTotal = _checkoutVars.TaxAll; var paymentGatewayID = _checkoutVars.Gateway; var paymentGatewaySubTypeID = 1;
                var amount = _checkoutVars.GrandTotal; var OrderDetails = {
                    BillingAddressInfo: api.BillingAddress,
                    PaymentInfo: {
                        PaymentMethodName: paymentMethodName,
                        PaymentMethodCode: paymentMethodCode,
                        CardNumber: "",
                        TransactionType: creditCardTransactionType,
                        CardType: CardType,
                        CardCode: "",
                        ExpireDate: "",
                        AccountNumber: accountNumber,
                        RoutingNumber: routingNumber,
                        AccountType: accountType,
                        BankName: bankName,
                        AccountHolderName: accountHoldername,
                        ChequeType: checkType,
                        ChequeNumber: checkNumber,
                        RecurringBillingStatus: recurringBillingStatus
                    },
                    OrderDetailsInfo: {
                        SessionCode: '',
                        IsGuestUser: false,
                        InvoiceNumber: "",
                        OrderStatus: "",
                        TransactionID: 0,
                        GrandTotal: amount,
                        DiscountAmount: api.UserCart.TotalDiscount,
                        CouponDiscountAmount: api.UserCart.CouponDiscountAmount,
                        Coupons: [],
                        UsedRewardPoints: api.UserCart.UsedRewardPoints,
                        RewardDiscountAmount: api.UserCart.RewardPointsDiscount,
                        PurchaseOrderNumber: 0,
                        PaymentGatewayTypeID: paymentGatewayID,
                        PaymentGateSubTypeID: paymentGatewaySubTypeID,
                        ClientIPAddress: clientIPAddress,
                        UserBillingAddressID: $('.cssClassBillingAddressInfo span').attr('id'),
                        ShippingMethodID: api.UserCart.spMethodID,
                        PaymentMethodID: 0,
                        TaxTotal: taxTotal,
                        CurrencyCode: currencyCode,
                        CustomerID: AspxCommerce.utils.GetCustomerID(),
                        ResponseCode: 0,
                        ResponseReasonCode: 0,
                        ResponseReasonText: "",
                        Remarks: orderRemarks,
                        IsMultipleCheckOut: true,
                        IsTest: isTestRequest,
                        IsEmailCustomer: isEmailCustomer,
                        IsDownloadable: api.UserCart.IsDownloadItemInCart,
                        IsShippingAddressRequired: api.UserCart.NoShippingAddress
                    },
                    CommonInfo: {
                        PortalID: AspxCommerce.utils.GetPortalID(),
                        StoreID: AspxCommerce.utils.GetStoreID(),
                        CultureName: AspxCommerce.utils.GetCultureName(),
                        AddedBy: AspxCommerce.utils.GetUserName(),
                        IsActive: api.UserCart.isActive
                    }
                };
                var paramData = {
                    OrderDetailsCollection: {
                        ObjOrderDetails: OrderDetails.OrderDetailsInfo,
                        LstOrderItemsInfo: api.UserCart.lstItems,
                        ObjPaymentInfo: OrderDetails.PaymentInfo,
                        GiftCardDetail: api.UserCart.GiftCardDetail,
                        ObjBillingAddressInfo: OrderDetails.BillingAddressInfo,
                        ObjShippingAddressInfo: OrderDetails.objSPAddressInfo,
                        ObjCommonInfo: OrderDetails.CommonInfo,
                        ObjOrderTaxInfo: api.UserCart.objTaxList
                    }
                };
                this.config.method = "AspxCoreHandler.ashx/SaveOrderDetails",
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ "orderDetail": paramData.OrderDetailsCollection }),
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);
            },
            AddOrderDetailsTest: function () {
                if ($('#txtFirstName').val() == '') {
                    var billingAddress = $('#dvBillingSelect input:radio:checked').parent('label').text();
                    var addr = billingAddress.split(',');
                    var Name = addr[0].split(' ');
                    Array.prototype.clean = function (deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    Name.clean("");

                    if ($('#dvBillingSelect input:radio:checked').val() > 0)
                        api.BillingAddress.AddressID = $('#dvBillingSelect input:radio:checked').val();

                    api.BillingAddress.FirstName = Name[0];
                    api.BillingAddress.LastName = Name[1];
                    api.BillingAddress.CompanyName = addr[8];
                    api.BillingAddress.EmailAddress = addr[6];
                    api.BillingAddress.Address = addr[1];
                    api.BillingAddress.Address2 = addr[11];
                    api.BillingAddress.City = addr[2];
                    api.BillingAddress.State = addr[3];
                    api.BillingAddress.Zip = addr[5];
                    api.BillingAddress.Country = addr[4];
                    api.BillingAddress.Phone = addr[7];
                    api.BillingAddress.Mobile = addr[8];
                    api.BillingAddress.Fax = addr[9];
                    api.BillingAddress.WebSite = addr[10];
                } else {
                    api.BillingAddress.FirstName = Encoder.htmlEncode($('#txtFirstName').val());
                    api.BillingAddress.LastName = Encoder.htmlEncode($('#txtLastName').val());
                    api.BillingAddress.CompanyName = Encoder.htmlEncode($('#txtCompanyName').val());
                    api.BillingAddress.EmailAddress = $('#txtEmailAddress').val();
                    api.BillingAddress.Address = Encoder.htmlEncode($('#txtAddress1').val());
                    api.BillingAddress.Address2 = Encoder.htmlEncode($('#txtAddress2').val());
                    api.BillingAddress.City = Encoder.htmlEncode($('#txtCity').val());
                    api.BillingAddress.Country = $('#ddlBLCountry option:selected').text();
                    if (api.BillingAddress.Country == 'United States')
                        api.BillingAddress.State = $('#ddlBLState option:selected').text();
                    else
                        api.BillingAddress.State = Encoder.htmlEncode($('#txtState').val());
                    api.BillingAddress.Zip = $('#txtZip').val();
                    api.BillingAddress.Phone = $('#txtPhone').val();
                    api.BillingAddress.Mobile = $('#txtMobile').val();
                    api.BillingAddress.Fax = $('#txtFax').val();
                    api.BillingAddress.Website = $('#txtWebsite').val();
                    api.BillingAddress.IsDefaultBilling = false;

                }

                if ($('#txtSPFirstName').val() == '') {
                    var address = $('#dvShippingSelect input:radio:checked').parent('label').text();
                    var addr = address.split(',');
                    var Name = addr[0].split(' ');
                    Array.prototype.clean = function (deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    Name.clean("");
                    api.ShippingAddress.AddressID = api.UserCart.spAddressID;
                    api.ShippingAddress.FirstName = Name[0];
                    api.ShippingAddress.LastName = Name[1];
                    api.ShippingAddress.CompanyName = addr[12];
                    api.ShippingAddress.EmailAddress = addr[6];
                    api.ShippingAddress.Address = addr[1];
                    api.ShippingAddress.Address2 = addr[11];
                    api.ShippingAddress.City = addr[2];
                    api.ShippingAddress.State = addr[3];
                    api.ShippingAddress.Zip = addr[5];
                    api.ShippingAddress.Country = addr[4];
                    api.ShippingAddress.Phone = addr[7];
                    api.ShippingAddress.Mobile = addr[8];
                    api.ShippingAddress.Fax = addr[9];
                    api.ShippingAddress.Website = addr[10];
                } else {
                    api.ShippingAddress.FirstName = Encoder.htmlEncode($('#txtSPFirstName').val());
                    api.ShippingAddress.LastName = Encoder.htmlEncode($('#txtSPLastName').val());
                    api.ShippingAddress.CompanyName = Encoder.htmlEncode($('#txtSPCompany').val());
                    api.ShippingAddress.Address = Encoder.htmlEncode($('#txtSPAddress').val());
                    api.ShippingAddress.Address2 = Encoder.htmlEncode($('#txtSPAddress2').val());
                    api.ShippingAddress.City = Encoder.htmlEncode($('#txtSPCity').val());
                    api.ShippingAddress.Zip = $('#txtSPZip').val();
                    api.ShippingAddress.Country = $('#ddlSPCountry option:selected').text();
                    if ($.trim(api.ShippingAddress.Country) == 'United States') {
                        api.ShippingAddress.State = $('#ddlSPState').val();
                    } else {
                        api.ShippingAddress.State = Encoder.htmlEncode($('#txtSPState').val());
                    }
                    api.ShippingAddress.Phone = $('#txtSPPhone').val();
                    api.ShippingAddress.Mobile = $('#txtSPMobile').val();
                    api.ShippingAddress.Fax = '';
                    api.ShippingAddress.Email = $('#txtSPEmailAddress').val();
                    api.ShippingAddress.Website = '';
                    api.ShippingAddress.IsDefaultShipping = false;
                }

                var creditCardTransactionType = $('#ddlTransactionType option:selected').text();
                var cardNo = $('#txtCardNo').val();
                var cardCode = $('#txtCardCode').val();
                var CardType = $('#cardType option:selected').text();

                var expireDate;
                expireDate = $('#lstMonth option:selected').text();
                expireDate += $('#lstYear option:selected').text();

                var accountNumber = $('#txtAccountNumber').val();
                var routingNumber = $('#txtRoutingNumber').val();
                var accountType = $('#ddlAccountType option:selected').text();
                var bankName = $('#txtBankName').val();
                var accountHoldername = $('#txtAccountHolderName').val();
                var checkType = $('#ddlChequeType option:selected').text();
                var checkNumber = $('#txtChequeNumber').val();
                var recurringBillingStatus = false;

                if ($('#chkRecurringBillingStatus').is(':checked'))
                    recurringBillingStatus = true;
                else
                    recurringBillingStatus = false;

                if ($('#chkBillingAsShipping').is(":checked"))
                    api.BillingAddress.IsBillingAsShipping = true;
                else
                    api.BillingAddress.IsBillingAsShipping = false;


                var orderRemarks = Encoder.htmlEncode($("#txtAdditionalNote").val());
                var orderItemRemarks = "Order Item Remarks";
                var currencyCode = '<%=MainCurrency %>';
                var isTestRequest = "TRUE";
                var isEmailCustomer = "TRUE";
                var taxTotal = _checkoutVars.TaxAll; var paymentGatewayID = _checkoutVars.Gateway; var paymentGatewaySubTypeID = 1;
                var shippingMethodID = api.UserCart.spMethodID;
                var paymentMethodCode = "Gift Card";
                var paymentMethodName = "Gift Card";

                shippingRate = _checkoutVars.ShippingCost; var amount = _checkoutVars.GrandTotal;
                var OrderDetails = {
                    BillingAddressInfo: api.BillingAddress,
                    objSPAddressInfo: api.ShippingAddress,
                    PaymentInfo: {
                        PaymentMethodName: paymentMethodName,
                        PaymentMethodCode: paymentMethodCode,
                        CardNumber: "",
                        TransactionType: creditCardTransactionType,
                        CardType: "",
                        CardCode: "",
                        ExpireDate: "",
                        AccountNumber: accountNumber,
                        RoutingNumber: routingNumber,
                        AccountType: accountType,
                        BankName: bankName,
                        AccountHolderName: accountHoldername,
                        ChequeType: checkType,
                        ChequeNumber: checkNumber,
                        RecurringBillingStatus: recurringBillingStatus
                    },
                    OrderDetailsInfo: {
                        SessionCode: AspxCommerce.utils.GetSessionCode(),
                        InvoiceNumber: "",
                        OrderStatus: "",
                        TransactionID: 0,
                        GrandTotal: amount,
                        DiscountAmount: api.UserCart.TotalDiscount,
                        CouponDiscountAmount: api.UserCart.CouponDiscountAmount,
                        Coupons: [],
                        UsedRewardPoints: api.UserCart.UsedRewardPoints,
                        RewardDiscountAmount: api.UserCart.RewardPointsDiscount,
                        PurchaseOrderNumber: 0,
                        PaymentGatewayTypeID: paymentGatewayID,
                        PaymentGateSubTypeID: paymentGatewaySubTypeID,
                        ClientIPAddress: AspxCommerce.utils.GetClientIP(),
                        UserBillingAddressID: api.BillingAddress.AddressID,
                        ShippingMethodID: shippingMethodID,
                        IsGuestUser: api.UserCart.isUserGuest,
                        PaymentMethodID: 0,
                        TaxTotal: taxTotal,
                        CurrencyCode: currencyCode,
                        CustomerID: AspxCommerce.utils.GetCustomerID(),
                        ResponseCode: 0,
                        ResponseReasonCode: 0,
                        ResponseReasonText: "",
                        Remarks: orderRemarks,
                        IsMultipleCheckOut: false,
                        IsTest: isTestRequest,
                        IsEmailCustomer: isEmailCustomer,
                        IsDownloadable: api.UserCart.IsDownloadItemInCart,
                        IsShippingAddressRequired: api.UserCart.NoShippingAddress

                    },
                    CommonInfo: {
                        PortalID: AspxCommerce.utils.GetPortalID(),
                        StoreID: AspxCommerce.utils.GetStoreID(),
                        CultureName: AspxCommerce.utils.GetCultureName(),
                        AddedBy: AspxCommerce.utils.GetUserName(),
                        IsActive: api.UserCart.isActive
                    }
                };

                var paramData = {
                    OrderDetailsCollection: {
                        ObjOrderDetails: OrderDetails.OrderDetailsInfo,
                        LstOrderItemsInfo: api.UserCart.lstItems,
                        ObjPaymentInfo: OrderDetails.PaymentInfo,
                        GiftCardDetail: api.UserCart.GiftCardDetail,
                        ObjBillingAddressInfo: OrderDetails.BillingAddressInfo,
                        ObjShippingAddressInfo: OrderDetails.objSPAddressInfo,
                        ObjCommonInfo: OrderDetails.CommonInfo,
                        ObjOrderTaxInfo: api.UserCart.objTaxList
                    }
                };
                this.config.method = "AspxCoreHandler.ashx/SaveOrderDetails",
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ "orderDetail": paramData.OrderDetailsCollection });
                this.config.ajaxCallMode = 4;
                this.config.error = 4;
                this.ajaxCall(this.config);
            }
        };
        AspxOrder.init();
    });
    //]]>
</script>

<div class="sfFormwrapper clearfix">
    <table class="sfGiftWrapperTable cssClassTMar20 cssClassBMar20" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblGiftCardCode" Text="Gift Card Code:"
                    meta:resourcekey="lblGiftCardCodeResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtGiftCardCode" name="giftcardcode" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="Label1" Text="Pin Code:"
                    meta:resourcekey="Label1Resource1"></asp:Label>
            </td>
            <td>
                <input type="password" id="txtGiftCardPinCode" name="giftcardpincode" />
            </td>
        </tr>
    </table>

    <div class="sfButtonwrapper sfFloatRight">
        <label class="cssClassDarkBtn">
            <button type="button" id="btnCheckBalance">
                <span class="sfLocale">Check GiftCard Balance</span></button></label>
        <label class="cssClassGreyBtn i-apply">
            <button type="button" id="btnAppplyGiftCard">
                <span class="sfLocale">Apply GiftCard</span></button></label>

    </div>
</div>

<label class="cssClassGreenBtn">
    <input id="btnPlaceOrderGiftCard" type="button" class="sfLocale" value="Place Order" />
</label>
