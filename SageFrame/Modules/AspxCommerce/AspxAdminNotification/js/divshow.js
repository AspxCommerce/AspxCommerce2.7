var OrderManage;
$(function () {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    OrderManage = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
            url: "",
            method: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: OrderManage.config.type,
                contentType: OrderManage.config.contentType,
                cache: OrderManage.config.cache,
                async: OrderManage.config.async,
                data: OrderManage.config.data,
                dataType: OrderManage.config.dataType,
                url: OrderManage.config.url,
                success: OrderManage.ajaxSuccess,
                error: OrderManage.ajaxFailure
            });
        },

        init: function () {

        },



        ViewOrders: function (tblID, argus) {
            switch (tblID) {
                case "gdvOrderDetails":
                    OrderManage.HideAll();
                    oid = argus[0];
                    $('#' + lblOrderForm1).html("Order ID: " + argus[0]);
                    $("#divOrderDetailForm").show();
                    OrderManage.BindAllOrderDetailsForm(argus[0]);
                    break;
                default:
                    break;
            }
        },








        BindAllOrderDetailsForm: function (argus) {
            var orderId = argus;
            this.config.url = this.config.baseURL + "GetAllOrderDetailsForView";
            this.config.data = JSON2.stringify({ orderId: orderId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },



        ajaxSuccess: function (data) {
            switch (OrderManage.config.ajaxCallMode) {
                case 0:
                    break;

                case 3:
                    OrderManage.IsModuleInstalled();
                    var elements = '';
                    var tableElements = '';
                    var grandTotal = '';
                    var couponAmount = '';
                    var rewardAmount = '';
                    var subTotal = '';
                    var taxTotal = '';
                    var giftCard = '';
                    var shippingCost = '';
                    var discountAmount = '';
                    var additionalNote = "";
                    $.each(data.d, function (index, value) {
                        Array.prototype.clean = function (deleteValue) {
                            for (var i = 0; i < this.length; i++) {
                                if (this[i] == deleteValue) {
                                    this.splice(i, 1);
                                    i--;
                                }
                            }
                            return this;
                        };
                        if (index < 1) {
                            var billAdd = '';
                            var arrBill;
                            arrBill = value.BillingAddress.split(',');
                            billAdd += '<li><h4>' + getLocale(AspxOrderManagement, 'Billing Address :') + '</h4></li>';
                            billAdd += '<li>' + arrBill[0] + ' ' + arrBill[1] + '</li>';
                            billAdd += '<li>' + arrBill[2] + '</li><li>' + arrBill[3] + '</li><li>' + arrBill[4] + '</li>';
                            billAdd += '<li>' + arrBill[5] + ' ' + arrBill[6] + ' ' + arrBill[7] + '</li><li>' + arrBill[8] + '</li><li>' + arrBill[9] + ', ' + arrBill[10] + '</li><li>' + arrBill[11] + '</li><li>' + arrBill[12] + '</li>';
                            $("#divOrderDetailForm").find('ul').html(billAdd);
                            $("#OrderDate").html(value.OrderedDate);
                            $("#invoiceNo").html(value.InVoiceNumber);
                                                       $("#PaymentMethod").html(value.PaymentMethodName);
                            additionalNote = value.Remarks;
                            $("#storeName").html(storeName);
                            $("#storeDescription").html(value.StoreDescription);
                            if (value.OrderType == 2) {
                                $("#btnCreateShippingLabel").hide();
                            } else {
                                $("#btnCreateShippingLabel").show();
                            }
                        }
                        tableElements += '<tr>';
                        tableElements += '<td>' + value.SKU + '</td>';
                        if (value.CostVariants != "") {
                            tableElements += '<td>' + value.ItemName + '<br/>' + '(' + value.CostVariants + ')' + '</td>';
                        } else {
                            tableElements += '<td>' + value.ItemName + '<br/></td>';
                        }
                        var shippingAddress = new Array();
                        var shipAdd = '';
                        if (value.ShippingAddress != "N/A") {
                            shippingAddress = value.ShippingAddress.split(",");
                                                       if ($.trim(shippingAddress[0]) != '')
                                shipAdd = $.trim(shippingAddress[0]) + '</br>';
                            if ($.trim(shippingAddress[1]) != '')
                                shipAdd += $.trim(shippingAddress[1]) + '</br>';
                            if ($.trim(shippingAddress[2]) != '')
                                shipAdd += $.trim(shippingAddress[2]) + '</br>';
                            if ($.trim(shippingAddress[3]) != '')
                                shipAdd += $.trim(shippingAddress[3]) + '</br>';
                            if ($.trim(shippingAddress[4]) != '')
                                shipAdd += $.trim(shippingAddress[4]) + ' ';
                            if ($.trim(shippingAddress[5]) != '')
                                shipAdd += $.trim(shippingAddress[5]) + ' ';
                            if ($.trim(shippingAddress[6]) != '')
                                shipAdd += $.trim(shippingAddress[6]) + '</br>';
                            if ($.trim(shippingAddress[7]) != '')
                                shipAdd += $.trim(shippingAddress[7]) + '</br>';
                            if ($.trim(shippingAddress[8]) != '')
                                shipAdd += $.trim(shippingAddress[8]) + '</br>';
                            if ($.trim(shippingAddress[9]) != '')
                                shipAdd += $.trim(shippingAddress[9]) + ' ';
                            if ($.trim(shippingAddress[10]) != '')
                                shipAdd += $.trim(shippingAddress[10]) + '</br>';
                            if ($.trim(shippingAddress[11]) != '')
                                shipAdd += $.trim(shippingAddress[11]) + '</br>';
                            if ($.trim(shippingAddress[12]) != '')
                                shipAdd += $.trim(shippingAddress[12]);
                        } else {
                            shipAdd = value.ShippingAddress.split(",");
                        }
                        tableElements += '<td>' + value.ShippingMethod + '</td>';
                        tableElements += '<td>' + shipAdd + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.ShippingRate.toFixed(2) + '</span></td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td class="cssOrderItemQuantity">' + value.Quantity + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            subTotal = '<tr>';
                            subTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Sub Total :') + '</b></td>';
                            subTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.GrandSubTotal).toFixed(2) + '</span></td>';
                            subTotal += '</tr>';

                            var orderID = value.OrderID;
                            $.ajax({
                                type: "POST",
                                url: aspxservicePath + "AspxCommerceWebService.asmx/GetTaxDetailsByOrderID",
                                data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function (msg) {
                                    $.each(msg.d, function (index, val) {
                                        if (val.TaxSubTotal != 0) {
                                            taxTotal += '<tr>';
                                            taxTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + val.TaxManageRuleName + ':' + '</b></td>';
                                            taxTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (val.TaxSubTotal).toFixed(2) + '</span></td>';
                                            taxTotal += '</tr>';
                                        }
                                    });

                                }

                            });
                                                                                                                                        shippingCost = '<tr>';
                            shippingCost += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Shipping Cost :') + '</b></td>';
                            shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.TotalShippingCost.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            discountAmount = '<tr>';
                            discountAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Discount Amount :') + '</b></td>';
                            discountAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            discountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Coupon Amount :') + '</b></td>';
                                                       couponAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + value.CouponAmount.toFixed(2) + '</span></span></td>';
                                                                                                                                        couponAmount += '</tr>';
                                                       rewardAmount = '<tr>';
                            rewardAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Discount ( Reward Points ) :') + '</b></td>';
                            rewardAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + value.RewardDiscountAmount.toFixed(2) + '</span></span></td>';
                            rewardAmount += '</tr>';
                                                       if (value.GiftCard != "" && value.GiftCard != null) {
                                var giftCardUsed = value.GiftCard.split('#');
                                for (var g = 0; g < giftCardUsed.length; g++) {
                                    var keyVal = giftCardUsed[g].split('=');
                                    giftCard += '<tr>';
                                    giftCard += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Gift Card') + '(' + keyVal[0] +
                                        ') :</b></td>';
                                    giftCard += '<td class="cssClassAlignRight" ><span class="cssClassFormatCurrency" >' + keyVal[1] + '</span></td>';
                                    giftCard += '</tr>';
                                }
                            }
                            grandTotal = '<tr>';
                            grandTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>' + getLocale(AspxOrderManagement, 'Grand Total :') + '</b></td>';
                            grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                        }
                    });
                    $("#divOrderDetailForm").find('table>tbody').html(tableElements);
                    $("#divOrderDetailForm").find('table>tbody').append(subTotal);
                    $("#divOrderDetailForm").find('table>tbody').append(discountAmount);
                    $("#divOrderDetailForm").find('table>tbody').append(taxTotal);
                    $("#divOrderDetailForm").find('table>tbody').append(shippingCost);
                    $("#divOrderDetailForm").find('table>tbody').append(couponAmount);
                    if (isInstalled == true) {
                        $("#divOrderDetailForm").find('table>tbody').append(rewardAmount);
                    }
                    giftCard != "" ? $("#divOrderDetailForm").find('table>tbody').append(giftCard) : giftCard = "";
                    $("#divOrderDetailForm").find('table>tbody').append(grandTotal);
                    $("#divOrderDetailForm").find("table>tbody tr:even").addClass("sfEven");
                    $("#divOrderDetailForm").find("table>tbody tr:odd").addClass("sfOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    OrderManage.HideAll();
                    $("#divOrderDetailForm").show();
                    break;

            }
        }
    };
    OrderManage.init();
});
