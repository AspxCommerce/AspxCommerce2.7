var Shipments = "";
$(function () {
    var isInstalled = false;
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var aspxCommonObj = {
        StoreID: storeId,
        PortalID: portalId,
        UserName: userName,
        CultureName: cultureName
    };
    Shipments = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            url: "",
            method: ""
        },
        IsModuleInstalled: function () {
            var rewardPoints = 'AspxRewardPoints';
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: AspxCommerce.utils.GetAspxServicePath() + "AspxCommonHandler.ashx/" + "GetModuleInstallationInfo",
                data: JSON2.stringify({ moduleFriendlyName: rewardPoints, aspxCommonObj: aspxCommonObj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    isInstalled = response.d;
                },
                error: function () {
                                   }
            });
        },
        init: function () {
            Shipments.HideDivs();
            $("#divShipmentsDetails").show();
            Shipments.BindShipmentsDetails(null, null, null);
            Shipments.ForceNumericInput();
            $("#btnShipmentBack").click(function () {
                Shipments.HideDivs();
                $("#divShipmentsDetails").show();
                $("#divShipmentsDetailForm table >tbody").html('');
            });
            $("#btnShipmentsSearch").on("click", function () {
                Shipments.SearchShipments();
            });

            $('#txtShippingMethodName,#txtOrderID,#txtSearchShipToName').keyup(function (event) {
                if (event.keyCode == 13) {
                    $("#btnShipmentsSearch").click();
                }
            });
        },
        ajaxCall: function (config) {
            $.ajax({
                type: Shipments.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: Shipments.config.contentType,
                cache: Shipments.config.cache,
                async: Shipments.config.async,
                data: Shipments.config.data,
                dataType: Shipments.config.dataType,
                url: Shipments.config.url,
                success: Shipments.ajaxSuccess,
                error: Shipments.ajaxFailure
            });
        },
        ForceNumericInput: function (defaultQuantityInGroup) {
            $("#txtOrderID").keydown(function (e) {
                               if ($.inArray(e.keyCode, [8, 9, 27, 13, 110]) !== -1 ||
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                                       return;
                }
                                              if (e.shiftKey == 190) {
                    e.preventDefault();
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
                if (e.keyCode == 96) {
                    if ($(this).val() == 0) {
                        e.preventDefault();
                    }
                }
                if (e.keyCode == 8) {
                    if (($(this).val() > 0) && ($(this).val() < 9)) {
                        e.preventDefault();
                    }
                }

            });
        },
        HideDivs: function () {
            $("#divShipmentsDetails").hide();
            $("#divShipmentsDetailForm").hide();
        },
        BindShipmentsDetails: function (shippingMethodNm, orderID, shipNm) {
            var shipmentObj = {
                OrderID: orderID,
                ShipToName: shipNm,
                ShippingMethodName: shippingMethodNm
            };
            this.config.method = "GetShipmentsDetails";
            this.config.data = { shipmentObj: shipmentObj, aspxCommonObj: aspxCommonObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShipmentsDetails_pagesize").length > 0) ? $("#gdvShipmentsDetails_pagesize :selected").text() : 10;

            $("#gdvShipmentsDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShipmentsManagement, 'Shipping Method ID'), name: 'shipping_methodId', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxShipmentsManagement, 'Shipping Method Name'), name: 'shipping_method_name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShipmentsManagement, 'Order ID'), name: 'order_id', cssclass: '', controlclass: '', coltype: 'label', align: 'left'},
                    { display: getLocale(AspxShipmentsManagement, 'Ship to Name'), name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShipmentsManagement, 'Shipping Cost'), name: 'shipping_rate', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxShipmentsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxShipmentsManagement, 'View'), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'Shipments.ViewShipments', arguments: '1,2,3,4,5,6' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShipmentsManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { o: { sorter: true }, 5: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ViewShipments: function (tblID, argus) {
            switch (tblID) {
                case "gdvShipmentsDetails":
                    Shipments.HideDivs();
                                       $("#divShipmentsDetailForm").show();
                    Shipments.BindShippindMethodDetails(argus[4]);
                    break;
            }
        },

        BindShippindMethodDetails: function (orderId) {
            this.config.url = this.config.baseURL + "BindAllShipmentsDetails";
            this.config.data = JSON2.stringify({ orderID: orderId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        SearchShipments: function (data) {
            var shippingMethodNm = $.trim($("#txtShippingMethodName").val());
            var orderID = $.trim($("#txtOrderID").val());
            var shipNm = $.trim($("#txtSearchShipToName").val());
            if (shippingMethodNm.length < 1) {
                shippingMethodNm = null;
            }
            if (orderID.length < 1) {
                orderID = null;
            }
            if (shipNm.length < 1) {
                shipNm = null;
            }
            Shipments.BindShipmentsDetails(shippingMethodNm, orderID, shipNm);
        },


        ajaxSuccess: function (data) {
            switch (Shipments.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    Shipments.IsModuleInstalled();
                    var tableElements = '';
                    var grandTotal = '';
                    var dicountAmount = '';
                    var couponAmount = '';
                    var rewardAmount = '';
                    var taxTotal = '';
                    var subTotal = '';
                    var shippingCost = '';
                    var additionalNote = "";
                    var giftCard = "";
                    $.each(data.d, function (index, value) {
                        var cv = "";
                        if (value.CostVariants != "") {
                            cv = "(" + value.CostVariants + ")";
                        }
                        tableElements += '<tr>';
                        tableElements += '<td>' + value.ItemName + cv + '</td>';
                        $("#shipmentDate").html(value.ShipmentDate);
                        tableElements += '<td>' + value.SKU + '</td>';
                        tableElements += '<td>' + value.ShippingAddress + '</td>';
                        tableElements += '<td>' + value.ShippingMethod + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.ShippingRate + '</span></td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td>' + value.Quantity + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            subTotal = '<tr>';
                            subTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Sub Total:")+'</b></td>';
                            subTotal += '<td><span class="cssClassFormatCurrency" >' + (value.GrandSubTotal).toFixed(2) + '</span></td>';
                            subTotal += '</tr>';

                            var orderID = value.OrderID;
                            $.ajax({
                                type: "POST",
                                url: Shipments.config.baseURL + "GetTaxDetailsByOrderID",
                                data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function (msg) {
                                    $.each(msg.d, function (index, val) {

                                        if (val.TaxSubTotal != 0) {
                                            taxTotal += '<tr>';
                                            taxTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + val.TaxManageRuleName + ':' + '</b></td>';
                                            taxTotal += '<td><span class="cssClassFormatCurrency" >' + (val.TaxSubTotal).toFixed(2) + '</span></td>';
                                            taxTotal += '</tr>';
                                        }
                                    });

                                }

                            });


                                                                                                                                        shippingCost = '<tr>';
                            shippingCost += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Shipping Cost:")+'</b></td>';
                            shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.TotalShippingCost.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            dicountAmount = '<tr>';
                            dicountAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td  class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Discount Amount:")+'</b></td>';
                            dicountAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" > - ' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            dicountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td  class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Coupon Amount:")+'</b></td>';
                            couponAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" > - ' + value.CouponAmount.toFixed(2) + '</span></td>';
                            couponAmount += '</tr>';
                            rewardAmount = '<tr>';
                            rewardAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td  class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Discount(RewardPoints):")+'</b></td>';
                            rewardAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" > - ' + value.RewardDiscountAmount.toFixed(2) + '</span></td>';
                            rewardAmount += '</tr>';
                            if (value.GiftCard != "" && value.GiftCard != null) {
                                var giftCardUsed = value.GiftCard.split('#');
                                for (var g = 0; g < giftCardUsed.length; g++) {
                                    var keyVal = giftCardUsed[g].split("=");

                                    giftCard += '<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"GiftCard")+'(' + keyVal[0] + '):</b></td>';
                                    giftCard += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + keyVal[1] + '</span></td>';
                                    giftCard += '</tr>';
                                }
                            }
                            grandTotal = '<tr>';
                            grandTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel cssClassOrderPrice"><b>'+getLocale(AspxShipmentsManagement,"Grand Total:")+'</b></td>';
                            grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                            additionalNote = value.Remarks;
                        }

                    });
                    $("#divShipmentsDetailForm").find('table>tbody').html(tableElements);
                    $("#divShipmentsDetailForm").find('table>tbody').append(subTotal);
                    $("#divShipmentsDetailForm").find('table>tbody').append(taxTotal);
                    $("#divShipmentsDetailForm").find('table>tbody').append(shippingCost);
                    $("#divShipmentsDetailForm").find('table>tbody').append(dicountAmount);
                    $("#divShipmentsDetailForm").find('table>tbody').append(couponAmount);
                    if (isInstalled == true) {
                        $("#divShipmentsDetailForm").find('table>tbody').append(rewardAmount);
                    }
                    $("#divShipmentsDetailForm").find('table>tbody').append(giftCard);
                    $("#divShipmentsDetailForm").find('table>tbody').append(grandTotal);
                    $("#divShipmentsDetailForm").find("table>tbody tr:even").addClass("sfEven");
                    $("#divShipmentsDetailForm").find("table>tbody tr:odd").addClass("sfOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    Shipments.HideDivs();
                    $("#divShipmentsDetailForm").show();
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    break;
            }
        }
    };
    Shipments.init();
});