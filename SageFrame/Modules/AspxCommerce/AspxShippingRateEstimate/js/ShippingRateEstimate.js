$(function () {
    
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID(),
        SessionCode: AspxCommerce.utils.GetSessionCode()
    };
    var rate = {
        config: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            url: "",
            method: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: rate.config.type,
                contentType: rate.config.contentType,
                cache: rate.config.cache,
                async: rate.config.async,
                data: rate.config.data,
                dataType: rate.config.dataType,
                url: rate.config.url,
                success: rate.ajaxSuccess,
                error: rate.ajaxFailure
            });
        },
        ajaxFailure: function (data) {
        },
        ajaxSuccess: function (data) {
        },
        Get: function () {
            var shipToAddress = {
                ToCity: $.trim($("#txtCity").val()),
                ToCountry: $.trim($("#ddlcountry").val()),
                ToCountryName: $.trim($("#ddlcountry :selected").text()),
                ToAddress: "",
                ToState: $.trim($("#ddlState").val()),
                ToPostalCode: $.trim($("#txtPostalCode").val()),
                ToStreetAddress1: "",
                ToStreetAddress2: ""
            };

            var basketItems = AspxCart.GetCartInfoForRate();

            var itemsDetail = {
                DimensionUnit: dimentionalUnit,
                IsSingleCheckOut: true,
                ShipToAddress: shipToAddress,
                WeightUnit: weightUnit,
                BasketItems: basketItems,
                CommonInfo: aspxCommonObj
            };

            this.config.url = this.config.baseURL + "GetRate";
            this.config.data = JSON2.stringify({ itemsDetail: itemsDetail });
            this.ajaxSuccess = function (data) {
                if (data.d.length > 0) {
                    var rateList;
                    rateList += "<thead class=\"cssClassHeadeTitle\"><tr><th>" + getLocale(AspxShippingRateEstimate, "Shipping Method") + "</th><th>" + getLocale(AspxShippingRateEstimate, "Estimated Delivery") + "</th><th>" + getLocale(AspxShippingRateEstimate, "Rate") + "</th><th>" + getLocale(AspxShippingRateEstimate, "Currency") + "</th></tr></thead><tbody>";
                    $.each(data.d, function (index, value) {
                        var currency = value.CurrencyCode == null ? "USD" : value.CurrencyCode;
                        var delivery = value.DeliveryTime == null ? "N/A" : value.DeliveryTime;

                        delivery = delivery == "" ? "N/A" : delivery;
                        currency = currency == "" ? "USD" : currency;
                        rateList += "<tr><td>" + value.ShippingMethodName + " </td><td>" + delivery + " </td><td>" + value.TotalCharges + " </td><td>" + currency + " </td></tr>";
                    });
                    rateList += "</tbody>";
                    $("#gdvRateDetail").html(rateList);
                    $("#gdvRateDetail").find("tbody tr:even").addClass("sfEven");
                    $("#gdvRateDetail").find("tbody tr:even").addClass("sfOdd");
                    $("#result").show();
                } else {
                    $("#gdvRateDetail").html("<p>" + getLocale(AspxShippingRateEstimate, "No Results to display!") + "</p>");
                    $("#result").show();
                }
            };
            this.ajaxCall(this.config);
        },
        BindState: function (countryCode) {

            this.config.url = this.config.baseURL + "GetStatesByCountry";
            this.config.data = JSON2.stringify({ countryCode: countryCode });
            this.ajaxSuccess = function (data) {
                if (data.d.length > 0) {
                    var options = "";
                    $.each(data.d, function (index, value) {
                        options += "<option value=" + value.StateCode + ">" + value.State + " </option>";

                    });
                    $("#state").show();
                    $("#ddlState").show();
                    $("#postalCode").show().val('');
                    $("#txtState").hide();
                    $("#ddlState").html(options);

                } else if (postalCountry.Match(countryCode)) {
                    $("#postalCode").show().val('');
                } else {
                    $("#state").hide();
                    $("#ddlState").hide();
                    $("#postalCode").hide().val('');
                    $("#txtState").show().val('');
                }
            };
            this.ajaxCall(this.config);

        },
        Init: function () {

            if (count > 0) {
                $("#dvEstimateRate").show();

            } else {
                $("#dvEstimateRate").hide();
                return false;
            }

            Array.prototype.Match = function (countryCode) {
                for (var i = 0; i < this.length; i++) {
                    if (this[i] == countryCode) {
                        return true;
                    }
                }
                return false;
            };

            $("#ddlcountry").bind("change", function () {
                rate.BindState($(this).val());
            });

            $("input[name=Addtolist]").bind("click", function () {
                if ($(this).val() == 1) {
                    $("#txtCity").val('');
                    $("#city").toggle();
                }

            });

            $("#imbPlus").bind("click", function () {
                $("#addmore").toggle();
            });

            var v = $("#form1").validate({
                messages: {
                    postalCode: {
                        required: '*',
                        minlength: "*",
                        maxlength: "*"
                    },
                    state: {
                        required: '*',
                        minlength: "*",
                        maxlength: "*"
                    }
                },
                rules:
                    {
                        state: { minlength: 2, required: true },
                        postalCode: { minlength: 4, required: true }
                    },
                ignore: ":hidden"
            });


            $("#btnCalculateRate").bind("click", function () {
                if (v.form()) {
                    rate.Get();
                }
            });



        }
    };

    if (showShippingRateCalculator.toLowerCase() == 'true' || showShippingRateCalculator == true) {
        rate.Init();
    }

});