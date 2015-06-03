<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AIM_AuthorizeDotNET.ascx.cs"
    Inherits="Modules_AspxPaymentGateway_AuthorizeDotNET" %>



<div class="cssClassAIMChildContent clearfix">
    <div id="AIMChild" class="AIMChild clearfix">

        <p>
            <label>
                <input id="rdbCheck" type="radio" name="paymentType" checked="checked" class="cssClassRadioBtn" /><strong class="sfLocale">Check/ MoneyOrder</strong></label>
        </p>

        <p>
            <label>
                <input id="rdbCreditCard" type="radio" name="paymentType" class="cssClassRadioBtn" /><strong class="sfLocale">Credit Card</strong></label>
        </p>
    </div>

    <div id="dvCheque" class="cssClassTMar20">
        <ul>
            <li class="cssTextLi">
                <div>
                    <span class="sfLocale">Account Number :</span> <span class="cssClassRequired">*</span>
                </div>
                <input id="txtAccountNumber" type="text" name="accountNumber" class=" required" minlength="5" />
            </li>
            <li class="cssTextLi NoRmargin">
                <div><span class="sfLocale">Routing Number : </span><span class="cssClassRequired">*</span></label></div>
                <input id="txtRoutingNumber" type="text" name="routingNumber" class=" required" minlength="9" />
            </li>
            <li class="cssTextLi">
                <div><span class="sfLocale">Account Type : </span><span class="cssClassRequired">*</span></div>
                <select id="ddlAccountType">
                    <option class="sfLocale">CHECKING</option>
                    <option class="sfLocale">BUSINESSCHECKING</option>
                    <option class="sfLocale">SAVINGS</option>
                </select>

            </li>
            <li class="cssTextLi NoRmargin">
                <div>
                    <span class="sfLocale">Bank Name :</span> <span class="cssClassRequired">*</span>
                </div>
                <input id="txtBankName" type="text" name="bankName" class=" required" minlength="2" />

            </li>
            <li class="cssTextLi">
                <div>
                    <span class="sfLocale">Account Holder :</span> <span class="cssClassRequired">*</span>
                </div>
                <input id="txtAccountHolderName" type="text" name="accountHolderName" class=" required"
                    minlength="2" /></li>

            <li class="cssTextLi NoRmargin">
                <div>
                    <span class="sfLocale">Cheque Type :</span> <span class="cssClassRequired">*</span>
                </div>
                <select id="ddlChequeType">
                    <option class="sfLocale">ARC</option>
                    <option class="sfLocale">BOC</option>
                    <option class="sfLocale">CCD</option>
                    <option class="sfLocale">PPD</option>
                    <option class="sfLocale">TEL</option>
                    <option class="sfLocale">WEB</option>
                </select>
            </li>
            <li class="cssTextLi">
                <div>
                    <span class="sfLocale">Cheque Number :</span><span class="cssClassRequired">*</span>
                </div>
                <input id="txtChequeNumber" type="text" name="chequeNumber" class=" required" minlength="4" /></li>
        </ul>
        <div class="cssClassCheckBox">
            <input id="chkRecurringBillingStatus" type="checkbox" /><span class="sfLocale">Recurring Billing Status</span>
        </div>
    </div>

    <div id="creditCard" class="cssClassAIMChildContent clearfix" style="display: none;">
        <%--  <b>
        <label>
            Transaction Type : <span class="cssClassRequired">*</span></label></b>
    <select id="ddlTransactionType">
    </select>
    <b>
        <label id="lblAuthCode">
            AuthorizeCode:<span class="cssClassRequired">*</span>
        </label>
    </b>
    <input type="text" id="txtAuthCode" class="required" minlength="5" />
    <br />--%>
        <ul>
            <li>
                <div>
                    <span class="sfLocale">Card Type :</span><span class="cssClassRequired">*</span>
                    <select id="cardType">
                </div>
                <option selected="selected" class="sfLocale">--Select one--</option>
                </select>
            </li>
            <li>
                <div>
                    <div>
                        <span class="sfLocale">Card No : </span><span class="cssClassRequired">*</span>
                    </div>
                    <input id="txtCardNo" type="text" maxlength="16" size="22" class="creditcard  required"
                        name="creditCard" />
            </li>
            <li>
                <div>

                    <span class="sfLocale">Card Code :</span><span class="cssClassRequired">*</span>
                </div>
                <input id="txtCardCode" type="text" size="10" maxlength="4" name="cardCode" class=" required"
                    minlength="3" />
            </li>
            <a class="screenshot" href="javascript:;" class="sfLocale">What is this?</a>
            <div id="ccv">
                <img src="" alt="" />

            </div>

            <li>
                <div>

                    <span class="sfLocale">Expire Date :</span> <span class="cssClassRequired">*</span>
                </div>
                <select id="lstMonth" class="required">
                    <option value="Month" selected="selected" class="sfLocale">--Month--</option>
                    <option value="01" class="sfLocale">01</option>
                    <option value="02" class="sfLocale">02</option>
                    <option value="03" class="sfLocale">03</option>
                    <option value="04" class="sfLocale">04</option>
                    <option value="05" class="sfLocale">05</option>
                    <option value="06" class="sfLocale">06</option>
                    <option value="07" class="sfLocale">07</option>
                    <option value="08" class="sfLocale">08</option>
                    <option value="09" class="sfLocale">09</option>
                    <option value="10" class="sfLocale">10</option>
                    <option value="11" class="sfLocale">11</option>
                    <option value="12" class="sfLocale">12</option>
                </select>

                <select id="lstYear">
                    <option value="Year" selected="selected" class="sfLocale">--Year--</option>
                    <option value="2011" class="sfLocale">2011</option>
                    <option value="2012" class="sfLocale">2012</option>
                    <option value="2013" class="sfLocale">2013</option>
                    <option value="2014" class="sfLocale">2014</option>
                    <option value="2015" class="sfLocale">2015</option>
                    <option value="2016" class="sfLocale">2016</option>
                    <option value="2017" class="sfLocale">2017</option>
                    <option value="2018" class="sfLocale">2018</option>
                    <option value="2019" class="sfLocale">2019</option>
                    <option value="2020" class="sfLocale">2020</option>
                    <option value="2021" class="sfLocale">2021</option>
                    <option value="2022" class="sfLocale">2022</option>
                </select>
            </li>
    </div>
</div>


<label class="cssClassGreenBtn">
    <input id="ibtnAIM" type="button" class="sfLocale" value="Place Order" />

</label>


<script type="text/javascript">
    //<![CDATA[

    $(function () {

        $(".sfLocale").localize({
            moduleKey: AIMAuthorize
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
        }


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

        var _checkoutVars;

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
                addressPath: '<%=AddressPath %>',
                templateName: AspxCommerce.utils.GetTemplateName(),
                loadonce: 1,
                error: 0
            },
            AIMSetting: {
                version: "",
                deleimData: "",
                apiLogin: "",
                transactionKey: "",
                relayResponse: "",
                delemChar: "",
                encapChar: "",
                isTestRequest: ""
            },
            ajaxFailure: function () {
                switch (AspxOrder.config.error) {
                    case 3:
                        csscody.error("<h2>" + getLocale(AIMAuthorize, "Error Message") + "</h2><p>" + getLocale(AIMAuthorize, "Failed to connect with server!") + "</p>");
                        break;
                    case 4:
                        csscody.error("<h2>" + getLocale(AIMAuthorize, "Error Message") + "</h2><p>" + getLocale(AIMAuthorize, "Failed to connect with server!") + "</p>");
                        break;
                }
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
            CheckCreditCard: function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CustomerID = null;
                aspxCommonInfo.UserName = null;
                this.config.method = "AspxCoreHandler.ashx/CheckCreditCard";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, creditCardNo: $('#txtCardNo').val() });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(AspxOrder.config);
            },
            GetCardType: function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CustomerID = null;
                aspxCommonInfo.UserName = null;
                this.config.method = "AIMHandler.ashx/GetCardType",
                this.config.url = '<%=AIMPath %>' + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);

            },
            GetTransactionType: function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CustomerID = null;
                aspxCommonInfo.UserName = null;
                this.config.method = "AIMHandler.ashx/GetTransactionType",
                this.config.url = '<%=AIMPath %>' + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 7;
                this.ajaxCall(this.config);
            },
            SendDataForPayment: function () {
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
                    if ($.trim(api.BillingAddress.Country) == 'United States')
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

                var creditCardTransactionType = "AUTH_CAPTURE"; var cardNo = $('#txtCardNo').val();
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
                    api.BillingAddress.isBillingAsShipping = true;
                else
                    api.BillingAddress.isBillingAsShipping = false;


                var orderRemarks = $("#txtAdditionalNote").val();
                var orderItemRemarks = "Order Item Remarks";
                var currencyCode = '<%=MainCurrency %>';
                var isEmailCustomer = "TRUE";
                var paymentGatewaySubTypeID = 1;
                var taxTotal = _checkoutVars.TaxAll;
                var paymentGatewayID = _checkoutVars.Gateway;
                shippingRate = _checkoutVars.ShippingCost;
                var amount = _checkoutVars.GrandTotal;

                var OrderDetails = {
                    BillingAddressInfo: api.BillingAddress,
                    objSPAddressInfo: api.ShippingAddress,
                    PaymentInfo: {
                        PaymentMethodName: api.UserCart.paymentMethodName,
                        PaymentMethodCode: api.UserCart.paymentMethodCode,
                        CardNumber: cardNo,
                        TransactionType: creditCardTransactionType,
                        CardType: CardType,
                        CardCode: cardCode,
                        ExpireDate: expireDate,
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
                        CustomerID: AspxCommerce.utils.GetCustomerID(),
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
                        ShippingMethodID: api.UserCart.spMethodID,
                        PaymentMethodID: 0,
                        TaxTotal: taxTotal,
                        CurrencyCode: currencyCode,
                        ResponseCode: 0,
                        ResponseReasonCode: 0,
                        IsGuestUser: api.UserCart.isUserGuest,
                        ResponseReasonText: "",
                        Remarks: orderRemarks,
                        IsMultipleCheckOut: false,
                        Version: AspxOrder.AIMSetting.version,
                        DelimData: AspxOrder.AIMSetting.deleimData,
                        APILogin: AspxOrder.AIMSetting.apiLogin,
                        TransactionKey: AspxOrder.AIMSetting.transactionKey,
                        RelayResponse: AspxOrder.AIMSetting.relayResponse,
                        DelimChar: AspxOrder.AIMSetting.delemChar,
                        EncapeChar: AspxOrder.AIMSetting.encapChar,
                        IsTest: AspxOrder.AIMSetting.isTestRequest,
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
                        ObjBillingAddressInfo: OrderDetails.BillingAddressInfo,
                        ObjShippingAddressInfo: OrderDetails.objSPAddressInfo,
                        ObjCommonInfo: OrderDetails.CommonInfo,
                        ObjOrderTaxInfo: api.UserCart.objTaxList
                    }
                };
                this.config.method = "AIMHandler.ashx/SendPaymentInfoAIM";
                this.config.url = '<%=AIMPath %>' + this.config.method;
                this.config.data = JSON2.stringify({ "OrderDetail": paramData.OrderDetailsCollection, TemplateName: this.config.templateName, addressPath: this.config.addressPath });
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);
            },
            loadSettingAIM: function () {
                var aspxCommonInfo = aspxCommonObj();
                this.config.method = "AIMHandler.ashx/GetAllAuthorizedNetAIMSetting";
                this.config.url = '<%=AIMPath %>' + this.config.method;
                this.config.data = JSON2.stringify({ paymentGatewayID: _checkoutVars.Gateway, aspxCommonObj: aspxCommonInfo });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            },
            init: function () {
                AspxOrder.GetCardType();
                api.UserCart.paymentMethodCode = "ECHECK";
                api.UserCart.paymentMethodName = "Cheque";
                $('#lblAuthCode').hide();
                $('#txtAuthCode').hide();
                $('#rdbCheck').attr("checked", true);
                $('#creditCard').hide();

                $('#rdbCreditCard').on("click", function () {
                    api.UserCart.paymentMethodCode = "CC";
                    api.UserCart.paymentMethodName = "Credit Card";
                    $('#dvCheque').hide();
                    $('#creditCard').show();
                });

                $('#rdbCheck').on("click", function () {
                    api.UserCart.paymentMethodCode = "ECHECK";
                    api.UserCart.paymentMethodName = "Cheque";
                    $('#creditCard').hide();
                    $('#dvCheque').show();
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

                $('#ibtnAIM').on("click", function () {
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
                    var checkIfCartExist = AspxOrder.CheckCustomerCartExist();
                    if (!checkIfCartExist) {
                        csscody.alert("<h2>" + getLocale(AIMAuthorize, "Information Alert") + "</h2><p>" + getLocale(AIMAuthorize, "Your cart has been emptied already!!") + "</p>");


                        return false;
                    }

                    AspxOrder.loadSettingAIM();
                });

                $("#ccv").hide();
                $(".screenshot").bind("click", function () {
                    $("#ccv").show();
                    $("#ccv img").attr('src', aspxRootPath + 'Templates/AspxCommerce/images/cc_ccv.jpg');
                    $("#ccv img").attr('alt', getLocale(AIMAuthorize, "credit card verification code"));
                    $("#ccv  p").html('').html(getLocale(AIMAuthorize, 'The card code is a three- or four- digit security code that is printed on the back of cards. The number typically appears at the end of the signature panel.'));

                });
                $("#txtCardCode").bind("focusin", function () { $("#ccv").hide(); });

                $('#txtCardNo').bind("change", function () {

                    AspxOrder.CheckCreditCard();
                });
            },
            SendDataForPaymentForMulti: function () {
                var creditCardTransactionType = "AUTH_CAPTURE"; var cardNo = $('#txtCardNo').val();
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

                var isBillingAsShipping = false;


                if ($('#rdbCreditCard').is('selected')) {
                    api.UserCart.paymentMethodCode = "CC";
                    api.UserCart.paymentMethodName = "Credit Card";
                }

                if ($('#rdbCheque').is('selected')) {
                    api.UserCart.paymentMethodCode = "ECHECK";
                    api.UserCart.paymentMethodName = "Cheque";
                }

                if ($('#chkBillingAsShipping').is(":checked"))
                    api.BillingAddress.isBillingAsShipping = true;
                else
                    api.BillingAddress.isBillingAsShipping = false;

                var orderRemarks = $("#txtAdditionalNote").val();
                var currencyCode = '<%=MainCurrency %>';

                var isEmailCustomer = "TRUE";
                var discountAmount = "";
                var paymentGatewaySubTypeID = 1;
                var taxTotal = _checkoutVars.TaxAll;
                var paymentGatewayID = _checkoutVars.Gateway;
                shippingRate = _checkoutVars.ShippingCost;
                var amount = _checkoutVars.GrandTotal;
                var OrderDetails = {
                    BillingAddressInfo: api.BillingAddress,

                    PaymentInfo: {
                        PaymentMethodName: api.UserCart.paymentMethodName,
                        PaymentMethodCode: api.UserCart.paymentMethodCode,
                        CardNumber: cardNo,
                        TransactionType: creditCardTransactionType,
                        CardType: CardType,
                        CardCode: cardCode,
                        ExpireDate: expireDate,
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
                        Version: AspxOrder.AIMSetting.version,
                        DelimData: AspxOrder.AIMSetting.deleimData,
                        APILogin: AspxOrder.AIMSetting.apiLogin,
                        TransactionKey: AspxOrder.AIMSetting.transactionKey,
                        RelayResponse: AspxOrder.AIMSetting.relayResponse,
                        DelimChar: AspxOrder.AIMSetting.delemChar,
                        EncapeChar: AspxOrder.AIMSetting.encapChar,
                        IsTest: AspxOrder.AIMSetting.isTestRequest,
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
                        ObjBillingAddressInfo: OrderDetails.BillingAddressInfo,
                        ObjCommonInfo: OrderDetails.CommonInfo,
                        ObjOrderTaxInfo: api.UserCart.objTaxList
                    }
                };

                this.config.method = "AIMHandler.ashx/SendPaymentInfoAIM";
                this.config.url = '<%=AIMPath %>' + this.config.method;
                this.config.data = JSON2.stringify({ "OrderDetail": paramData.OrderDetailsCollection, TemplateName: this.config.templateName, addressPath: this.config.addressPath });
                this.config.ajaxCallMode = 4;
                this.config.error = 4;
                this.ajaxCall(this.config);
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
                        if (data.d == "Transaction completed successfully.") {
                            csscody.alert("<h2>" + getLocale(AIMAuthorize, "Information Alert") + "</h2><p>" + data.d + "!!</p>");

                            var homepath = "";
                            if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                                homepath = "AIM-Success" + pageExtension;
                            } else {
                                homepath = "AIM-Success";
                            }

                            var route = AspxCommerce.utils.GetAspxRedirectPath() + homepath;
                            window.location.href = route;
                        } else {
                            csscody.alert("<h2>" + getLocale(AIMAuthorize, "Information Alert") + "</h2><p>" + getLocale(AIMAuthorize, "Error occured:") + data.d + "!!</p>");
                        }
                        return true;
                        break;
                    case 4:
                        csscody.alert("<h2>" + getLocale(AIMAuthorize, "Information Alert") + "</h2><p>" + data.d + "!!</p>");
                        if (data.d == "Transaction completed successfully.") {
                            var $tab = $('#dvMultipleAddress').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
                            $tab.tabs('select', 4);
                            var home = '';
                            if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                                home = "home" + pageExtension;
                            } else {
                                home = "home";
                            }
                            $("#orderSuccess").find('a').attr('href', '').attr('href', aspxRedirectPath + home);
                            var successpath = '';
                            if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                                successpath = "AIM-Success" + pageExtension;
                            } else {
                                successpath = "AIM-Success";
                            }

                            var route = AspxCommerce.utils.GetAspxRedirectPath() + successpath;
                            window.location.href = route;
                        } else {
                        }
                        return true;
                        break;
                    case 6:
                        if (data.d.length > 0) {
                            $('#cardType').html('');
                            $.each(data.d, function (index, item) {
                                var option = '';
                                option += "<option title=" + aspxRootPath + item.ImagePath + " > " + item.cardTypeName + "</option>";
                                $('#cardType').append(option);
                            });
                        }
                        break;
                    case 7:
                        if (data.d.length > 0) {
                            $('#ddlTransactionType').html('');
                            $.each(data.d, function (index, item) {
                                var option = '';
                                option += "<option> " + item.transactionTypeName + "</option>";
                                $('#ddlTransactionType').append(option);
                            });
                        }
                        break;
                    case 8:
                        if (data.d.length > 0) {
                            $.each(data.d, function (index, item) {
                                AspxOrder.AIMSetting.version = item.Version;
                                AspxOrder.AIMSetting.deleimData = item.DelimData;
                                AspxOrder.AIMSetting.apiLogin = item.APILoginID;
                                AspxOrder.AIMSetting.transactionKey = item.TransactionKey;
                                AspxOrder.AIMSetting.relayResponse = item.RelayResponse;
                                AspxOrder.AIMSetting.delemChar = item.DelimChar;
                                AspxOrder.AIMSetting.encapChar = item.EncapChar;
                                AspxOrder.AIMSetting.isTestRequest = item.IsTestAIM;
                            });
                            if ($('#SingleCheckOut').length > 0) {
                                AspxOrder.SendDataForPayment();
                            } else {
                                AspxOrder.SendDataForPaymentForMulti();
                            }
                        }
                        break;
                    case 9:
                        if (data.d) {
                            csscody.alert("<h2>" + getLocale(AIMAuthorize, "Information Alert") + "</h2><p>" + getLocale(AIMAuthorize, "Your credit card is blacklisted!!") + "</p>");
                            $('#txtCardNo').val('');
                            if ($('#SingleCheckOut').length > 0) {
                                $('#btnPaymentInfoContinue').attr('disabled', 'disabled');
                            }
                            $('#btnBillingContinue').attr('disabled', 'disabled');
                        } else {
                            if ($('#SingleCheckOut').length > 0) {
                                $('#btnPaymentInfoContinue').removeAttr('disabled');
                            }
                            $('#btnBillingContinue').removeAttr('disabled');
                        }
                        break;
                }
            }
        };
        AspxOrder.init();

    });
    //]]> 
</script>
