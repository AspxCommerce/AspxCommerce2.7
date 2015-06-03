<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyOrders.ascx.cs" Inherits="Modules_AspxUserDashBoard_MyOrders" %>
<script type="text/javascript">
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    //<![CDATA[
    var MyOrders = "";
    aspxCommonObj.UserName = AspxCommerce.utils.GetUserName();
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });
    });
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID(),
        SessionCode: AspxCommerce.utils.GetSessionCode()
    };

    MyOrders = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: moduleRootPath, url: "",
            method: "",
            oncomplete: 0,
            ajaxCallMode: "",
            error: ""
        },

        ajaxCall: function (config) {
            $.ajax({
                type: MyOrders.config.type,
                    beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", userModuleIDUD);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: MyOrders.config.contentType,
                cache: MyOrders.config.cache,
                async: MyOrders.config.async,
                data: MyOrders.config.data,
                dataType: MyOrders.config.dataType,
                url: MyOrders.config.url,
                success: MyOrders.config.ajaxCallMode,
                error: MyOrders.config.error
            });
        },
        vars: {
            itemQuantity: "",
            itemQuantityInCart: "",
            itemName: "",
            variantName: ""
        },

        init: function () {
            MyOrders.GetMyOrders();
            MyOrders.OrderHideAll();
            $("#divTrackMyOrder").show();
            $("#divMyOrders").show();
            $("#lnkBack").bind("click", function () {
                MyOrders.OrderHideAll();
                $("#divTrackMyOrder").show();
                $("#divMyOrders").show();
                return false;
            });
            $("#txtOrderID").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    if (e.which != 13) {
                        $("#errmsgOrderID").html(getLocale(AspxUserDashBoard, "Digits Only")).css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }
            });

            $("#txtOrderID").keyup(function (event) {
                if (event.keyCode == 13) {
                    $("#btnGetOrderDetails").click();
                }
            });
            $("#btnGetOrderDetails").click(function () {
                var orderID = $.trim($("#txtOrderID").val());
                if (orderID == "") {
                    csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "Please enter order ID.") + "</p>");
                } else {
                    MyOrders.GetAllOrderDetails(orderID);
                }
                return false;
            });
        },

        OrderHideAll: function () {
            $("#divMyOrders").hide();
            $("#divOrderDetails").hide();
            $("#divTrackMyOrder").hide();
        },

        GetMyOrders: function () {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvMyOrder_pagesize").length > 0) ? $("#gdvMyOrder_pagesize :selected").text() : 10;
            $("#gdvMyOrder").sagegrid({
                url: this.config.baseURL + "UserDashBoardHandler.ashx/",
                functionMethod: 'GetMyOrderList',
                colModel: [
                    { display: getLocale(AspxUserDashBoard, 'Order ID'), name: 'order_id', cssclass: 'cssClassHeadNumber sfLocale', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Invoice Number'), name: 'invoice_number', cssclass: 'cssClassHeadNumber sfLocale', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Customer ID', name: 'customerID', cssclass: 'cssClassHeadNumber sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Customer Name', name: 'customer_name', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Email', name: 'email', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Order Status'), name: 'order_status', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Grand Total', name: 'grand_total', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Gateway Type Name', name: 'payment_gateway_typename', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Method Name', name: 'payment_method_name', cssclass: 'sfLocale', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Ordered Date'), name: 'ordered_date', cssclass: 'cssClassHeadDate sfLocale', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Actions'), name: 'action', cssclass: 'cssClassAction sfLocale', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxUserDashBoard, 'View'), enable: true, _event: 'click', trigger: '1', callMethod: 'MyOrders.GetOrderDetails', arguments: '' },
                     { display: getLocale(AspxUserDashBoard, 'Reorder'), name: 'Reorder', enable: true, _event: 'click', trigger: '1', callMethod: 'MyOrders.ReOrder', arguments: '' },
                      { display: getLocale(AspxUserDashBoard, 'Return'), name: 'Return', enable: true, _event: 'click', trigger: '1', callMethod: 'MyOrders.LoadControl', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxUserDashBoard, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false } }
            });
        },
        LoadControl: function (tblID, argus) {
            var controlName = "Modules/AspxCommerce/AspxReturnAndPolicy/ReturnsSubmit.ascx";
            $.ajax({
                type: "POST",
                url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#divLoadUserControl').html(response.d);
                    var orderID = (argus[0]);
                    ReturnsSubmit.GetOrderDetails(orderID);
                },
                error: function () {
                    csscody.error('<h2>' + getLocale(AspxUserDashBoard, 'Error Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Failed to load control!.') + '</p>');
                }
            });
        },

        ReOrder: function (tblID, argus) {
            switch (tblID) {
                case "gdvMyOrder":
                    MyOrders.GetReOrderDetails(argus[0]);
                    break;
            }
        },
        GetReOrderDetails: function (argus) {
            var orderId = argus;
            this.config.method = "UserDashBoardHandler.ashx/GetMyOrdersforReOrder";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderID: orderId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = MyOrders.ReOrderItems;
            this.config.async = false;
            this.ajaxCall(this.config);

        },
        ReOrderItems: function (data) {
            $.each(data.d, function (index, value) {
                var itemId = value.ItemID;
                itemName = value.ItemName;
                var itemPrice = value.Price;
                var itemSKU = value.SKU;
                var itemQuantity = value.Quantity;
                var totalWeightVariant = value.Weight;
                var itemCostVariantIDs = value.ItemCostVariantValueIDs;
                variantName = value.CostVariants;
                var sessionCode = AspxCommerce.utils.GetSessionCode();

                if (itemCostVariantIDs == '' || itemCostVariantIDs == null) {

                    MyOrders.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, aspxCommonObj);
                }
                else {
                    var itemQuantityTotal = MyOrders.CheckItemQuantity(itemId, itemCostVariantIDs);
                    var itemQuantityInCart = MyOrders.CheckItemQuantityInCart(itemId, itemCostVariantIDs + '@');
                    if (itemQuantityInCart != 0.1) {
                        if (itemQuantityTotal <= 0) {
                            csscody.alert("<h2>" + getLocale(AspxUserDashBoard, 'Information Alert') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Product') + " " + '(' + itemName + " " + ',' + variantName + ')' + " " + getLocale(AspxUserDashBoard, 'is currently Out Of Stock!') + "</p>");
                        } else {
                            if ((eval(itemQuantity) + eval(itemQuantityInCart)) > eval(itemQuantityTotal)) {
                                csscody.alert("<h2>" + getLocale(AspxUserDashBoard, 'Information Alert') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Product') + " " + '(' + itemName + " " + ',' + variantName + ')' + " " + getLocale(AspxUserDashBoard, 'is currently Out Of Stock!') + "</p>");
                            }
                            else {

                                MyOrders.AddItemstoCart(itemId, itemPrice, totalWeightVariant, itemQuantity, itemCostVariantIDs, aspxCommonObj);
                            }
                        }
                    }
                }
            });
        },
        AddToCartFromJS: function (itemId, itemPrice, itemSKU, itemQuantity, aspxCommonObj) {
            var param = { itemID: itemId, itemPrice: itemPrice, itemQuantity: itemQuantity, aspxCommonObj: aspxCommonObj };
            var data = JSON2.stringify(param);
            var myCartUrl;
            var addToCartProperties = {
                onComplete: function (e) {
                    if (e) {
                        if (AspxCommerce.utils.IsUserFriendlyUrl) {
                            myCartUrl = myCartURL + pageExtension;
                        } else {
                            myCartUrl = myCartURL;
                        }
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartUrl;
                    }
                }
            };
            $.ajax({
                type: "POST",
                url: aspxservicePath + "AspxCommonHandler.ashx/AddItemstoCart",
                data: data,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 1) {
                        AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                    }
                    else if (msg.d == 2) {
                        csscody.alert("<h2>" + getLocale(AspxUserDashBoard, 'Information Alert') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Product') + " " + '(' + itemName + ')' + " " + getLocale(AspxUserDashBoard, 'is currently Out Of Stock!') + "</p>");
                        HeaderControl.GetCartItemTotalCount(); ShopingBag.GetCartItemCount(); ShopingBag.GetCartItemListDetails();
                    }
                    else {
                        csscody.info('<h2>' + getLocale(AspxUserDashBoard, "Successful Message") + '</h2><p>' + getLocale(AspxUserDashBoard, 'Item') + " " + '(' + itemName + ')' + " " + getLocale(AspxUserDashBoard, 'has been successfully added to cart.') + '</p>', addToCartProperties);
                        HeaderControl.GetCartItemTotalCount(); ShopingBag.GetCartItemCount(); ShopingBag.GetCartItemListDetails();
                    }
                }
            });
        },

        AddItemstoCart: function (itemId, itemPrice, totalWeightVariant, itemQuantity, itemCostVariantIDs, aspxCommonObj) {

            var costVariantIDs = itemCostVariantIDs + '@';
            var isgiftCard = false;
            var giftCardDetail = null;

            var AddItemToCartObj = {
                ItemID: itemId,
                Price: itemPrice,
                Weight: totalWeightVariant,
                Quantity: itemQuantity,
                CostVariantIDs: costVariantIDs,
                IsGiftCard: isgiftCard
            };
            this.config.method = "AspxCommonHandler.ashx/AddItemstoCartFromDetail";
            this.config.url = aspxservicePath + this.config.method;
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, AddItemToCartObj: AddItemToCartObj, giftCardDetail: giftCardDetail });
            this.config.ajaxCallMode = MyOrders.AddItemstoCartFromDetail;
            this.config.oncomplete = 20;
            this.config.error = MyOrders.GetAddToCartErrorMsg;
            this.config.async = false;
            this.ajaxCall(this.config);

        },
        AddItemstoCartFromDetail: function (msg) {
            if (msg.d == 1) {
                var myCartUrl;
                if (userFriendlyURL) {
                    myCartUrl = myCartURL + pageExtension;
                } else {
                    myCartUrl = myCartURL;
                }
                var addToCartProperties = {
                    onComplete: function (e) {
                        if (e) {
                            window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartURL + pageExtension;
                        }
                    }
                };
                csscody.info('<h2>' + getLocale(AspxUserDashBoard, "Successful Message") + '</h2><p>' + getLocale(AspxUserDashBoard, 'Item') + " " + '(' + itemName + " " + ',' + variantName + ')' + " " + getLocale(AspxUserDashBoard, 'has been successfully added to cart.') + '</p>', addToCartProperties);
                HeaderControl.GetCartItemTotalCount(); ShopingBag.GetCartItemCount(); ShopingBag.GetCartItemListDetails();
            }
            else if (msg.d == 2) {
                csscody.alert("<h2>" + getLocale(AspxUserDashBoard, 'Information Alert') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Product') + " " + '(' + itemName + " " + ',' + variantName + ')' + " " + getLocale(AspxUserDashBoard, 'is currently Out Of Stock!') + "</p>");
            }
        },

        GetAddToCartErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxUserDashBoard, 'Information Alert') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Failed') + " " + '(' + itemName + " " + ',' + variantName + ')' + " " + getLocale(AspxUserDashBoard, 'to add item to cart!') + '</p>');
        },
        oncomplete: function () {
            switch (ItemDetail.config.oncomplete) {
                case 20:
                    ItemDetail.config.oncomplete = 0;
                    if ($("#divCartDetails").length > 0) {
                        AspxCart.GetUserCartDetails();
                    }
                    break;
            }
        },

        CheckItemQuantity: function (itemId, itemCostVariantIDs) {
            this.config.method = "CheckItemQuantity";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonObj, itemCostVariantIDs: itemCostVariantIDs });
            this.config.ajaxCallMode = MyOrders.SetItemQuantity;
            this.config.async = false;
            this.ajaxCall(this.config);
            return MyOrders.vars.itemQuantity;
        },
        SetItemQuantity: function (msg) {
            MyOrders.vars.itemQuantity = msg.d;
        },
        CheckItemQuantityInCart: function (itemId, itemCostVariantIDs) {
            this.config.method = "AspxCoreHandler.ashx/CheckItemQuantityInCart";
            this.config.url = aspxservicePath + this.config.method;
            this.config.data = JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonObj, itemCostVariantIDs: itemCostVariantIDs });
            this.config.ajaxCallMode = MyOrders.SetItemQuantityInCart;
            this.config.async = false;
            this.ajaxCall(this.config);
            return MyOrders.vars.itemQuantityInCart;
        },
        SetItemQuantityInCart: function (msg) {
            MyOrders.vars.itemQuantityInCart = msg.d;
        },

        GetOrderDetails: function (tblID, argus) {
            switch (tblID) {
                case "gdvMyOrder":
                    MyOrders.GetAllOrderDetails(argus[0]);
                    break;
            }
        },

        GetAllOrderDetails: function (argus) {
            var orderId = argus;
            this.config.method = "UserDashBoardHandler.ashx/GetMyOrders";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderID: orderId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = MyOrders.BindMyOrders;
            this.ajaxCall(this.config);
        },

        GetCheckOutPage: function (tdlID, argus) {
            switch (tdlID) {
                case "gdvMyOrder":
                    break;
            }
        },

        BindMyOrders: function (msg) {
            var length = msg.d.length;
            if (length > 0) {
                var elements = '';
                var tableElements = '';
                var grandTotal = '';
                var couponAmount = '';
                var rewardDiscountAmount = '';
                var taxTotal = '';
                var giftCard = '';
                var shippingCost = '';
                var discountAmount = '';
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
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
                        $.each(arrBill,function(index,value){
                         billAdd += '<li>' + value + '</li>';
                        });
                        $(".cssBillingAddressUl").html(billAdd);
                        $("#orderedDate").html(' ' + value.OrderedDate);
                        $("#invoicedNo").html(' ' + value.InVoiceNumber);
                        $('#storeName').html(' ' + value.StoreName);
                        $("#paymentGatewayType").html(' ' + value.PaymentGatewayTypeName);
                        $("#paymentMethod").html(' ' + value.PaymentMethodName);
                    }

                    var shippingAddress = new Array();
                    var shipAdd = '';
                    shippingAddress = value.ShippingAddress.replace(",", " ").split(",");
                    shippingAddress.clean(" ");

                    tableElements += '<tr>';
                    tableElements += '<td class="cssClassMyAccItemName">' + value.ItemName + '<br/>' + value.CostVariants + '</td>';
                    tableElements += '<td class="cssClassMyAccItemSKU">' + value.SKU + '</td>';
                    tableElements += '<td class="cssClassMyAccShippingAdd">' + shippingAddress + '</td>';
                    tableElements += '<td class="cssClassMyAccShppingRate"><span class="cssClassFormatCurrency">' + parseFloat(value.ShippingRate).toFixed(2) + '</span></td>';
                    tableElements += '<td class="cssClassMyAccPrice"><span class="cssClassFormatCurrency">' + parseFloat(value.Price).toFixed(2) + '</span></td>';
                    tableElements += '<td class="cssClassMyAccQuantity">' + value.Quantity + '</td>';
                    tableElements += '<td class="cssClassMyAccSubTotal"><span class="cssClassFormatCurrency">' + parseFloat(value.Price * value.Quantity).toFixed(2) + '</span></td>';
                    tableElements += '</tr>';
                    if (index == 0) {
                        var orderID = value.OrderID;
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetTaxDetailsByOrderID",
                            data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                                $.each(msg.d, function (index, val) {
                                    if (val.TaxSubTotal != 0) {
                                        taxTotal += '<tr><td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + val.TaxManageRuleName + '</td>';
                                        taxTotal += '<td><span class="cssClassFormatCurrency">' + parseFloat(val.TaxSubTotal).toFixed(2) + '</span></td></tr>';

                                    }
                                });
                            }
                        });
                        shippingCost = '<tr>';
                        shippingCost += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + getLocale(AspxUserDashBoard, "Shipping Cost:") + '</td>';
                        shippingCost += '<td><span class="cssClassFormatCurrency">' + parseFloat(value.ShippingRate).toFixed(2) + '</span></td>';
                        shippingCost += '</tr>';
                        discountAmount = '<tr>';
                        discountAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + getLocale(AspxUserDashBoard, "Discount Amount:") + '</td>';
                        discountAmount += '<td><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency">' + parseFloat(value.DiscountAmount).toFixed(2) + '</span></td>';
                        discountAmount += '</tr>';
                        couponAmount = '<tr>';
                        couponAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + getLocale(AspxUserDashBoard, "Coupon Amount:") + '</td>';
                        couponAmount += '<td><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency">' + parseFloat(value.CouponAmount).toFixed(2) + '</span></td>';
                        couponAmount += '</tr>';
                        rewardDiscountAmount = '<tr>';
                        rewardDiscountAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + getLocale(AspxUserDashBoard, "Discount(Reward Points):") + '</td>';
                        rewardDiscountAmount += '<td><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency cssClassSubTotal">' + parseFloat(value.RewardDiscountAmount).toFixed(2) + '</span></td>';
                        rewardDiscountAmount += '</tr>';
                        if (value.GiftCard != "" && value.GiftCard != null) {
                            var giftCardUsed = value.GiftCard.split('#');
                            for (var g = 0; g < giftCardUsed.length; g++) {
                                var keyVal = giftCardUsed[g].split('=');
                                giftCard += '<tr>';
                                giftCard += '<td colspan="4"></td><td class="cssClassLabel cssClassRowbold" colspan="2">' + getLocale(AspxUserDashBoard, 'Gift Card') + '(' + keyVal[0] +
                                    ') :</td>';
                                giftCard += '<td ><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + parseFloat(keyVal[1]).toFixed(2) + '</span></td>';
                                giftCard += '</tr>';
                            }
                        }
                        grandTotal = '<tr>';
                        grandTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">' + getLocale(AspxUserDashBoard, "Grand Total:") + '</td>';
                        grandTotal += '<td class="cssClassGrandTotal"><span class="cssClassFormatCurrency">' + parseFloat(value.GrandTotal).toFixed(2) + '</span></td>';
                        grandTotal += '</tr>';

                        if (value.OrderType == 2) {
                            $.ajax({
                                type: "POST",
                                url: aspxservicePath + "AspxServiceItemsHandler.ashx/GetServiceDetailsByOrderID",
                                data: JSON2.stringify({ orderID: orderID, aspxCommonObj: aspxCommonObj }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function (msg) {
                                    var length = msg.d.length;
                                    if (length > 0) {
                                        $('.cssClassServiceDetails').show();
                                        var val;
                                        for (var index = 0; index < length; index++) {
                                            val = msg.d[index];
                                            $('#serviceName').html(' ' + val.ServiceCategoryName);
                                            $('#serviceProductName').html(' ' + val.ServiceProductName);
                                            $('#serviceDuration').html(' ' + val.ServiceDuration);
                                            $('#providerName').html(' ' + val.EmployeeName);
                                            $('#storeLocationName').html(' ' + val.StoreLocationName);
                                            var date = 'new ' + val.PreferredDate.replace(/[/]/gi, '');;
                                            date = eval(date);
                                            $('#serviceDate').html(' ' + formatDate(date, 'yyyy/MM/dd'));
                                            $('#availableTime').html(' ' + val.PreferredTime);
                                            $('#bookAppointmentTime').html(' ' + val.PreferredTimeInterval);
                                        };
                                    }
                                }
                            });
                        }
                        else {
                            $('.cssClassServiceDetails').hide();
                            $('#serviceName').html(' ');
                            $('#serviceProductName').html(' ');
                            $('#serviceDuration').html(' ');
                            $('#providerName').html(' ');
                            $('#storeLocationName').html(' ');
                            $('#serviceDate').html(' ');
                            $('#availableTime').html(' ');
                            $('#bookAppointmentTime').html(' ');
                        }
                    }
                };

                $("#divOrderDetails").find('table>tbody').html(tableElements);
                $("#divOrderDetails").find('table>tbody').append(taxTotal);
                $("#divOrderDetails").find('table>tbody').append(shippingCost);
                $("#divOrderDetails").find('table>tbody').append(discountAmount);
                $("#divOrderDetails").find('table>tbody').append(couponAmount);
                $("#divOrderDetails").find('table>tbody').append(rewardDiscountAmount);
                $("#divOrderDetails").find('table>tbody').append(giftCard);
                $("#divOrderDetails").find('table>tbody').append(grandTotal);
                MyOrders.OrderHideAll();
                $("#divOrderDetails").show();
                $("#txtOrderID").val('');
                var cookieCurrency = $("#ddlCurrency").val();
                Currency.currentCurrency = BaseCurrency;
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
            } else {

                csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "Order ID does not exist!") + "</p>");
                $("#txtOrderID").val('');
                return false;
            }
        }
    },

    $(function () {
        MyOrders.init();
    });
    //]]>
</script>
<div id="divTrackMyOrder" class="cssClassSearchPanel sfFormwrapper clearfix">
    <div class="sfLocale sfCol_12">
        Order ID:
    </div>
    <div class="sfCol_67">
        <input type="text" id="txtOrderID" class="sfTextBoxSmall" /><span id="errmsgOrderID"></span></div>
    <div class="sfButtonwrapper sfCol_21">
        <label class="cssClassOrangeBtn i-arrow-right">
            <button type="button" id="btnGetOrderDetails">
                <span class="sfLocale">View Details</span></button></label>
    </div>
</div>
<div id="divMyOrders">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span class="sfLocale">My Orders</span>
            </h2>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxUserDashMyOrder" src="" class="sfLocale" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table class="sfGridWrapperTable" id="gdvMyOrder" cellspacing="0" cellpadding="0"
                    border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divOrderDetails" class="sfFormwrapper">
    <div class="cssClassStoreDetail cssClassBMar30 clearfix">
        <ul>
            <li><b><span class="cssClassLabel sfLocale">Ordered Date</span></b><span id="orderedDate">
            </span></li>
            <li><b><span class="cssClassLabel sfLocale">Invoice Number</span></b><span id="invoicedNo">
            </span></li>
            <li><b><span class="cssClassLabel sfLocale">Store Name</span></b><span id="storeName">
            </span></li>
            <li class="cssPaymentDetail"><b><span class="cssClassLabel sfLocale">Payment Method</span></b><span
                id="paymentMethod"> </span></li>
        </ul>
    </div>
    <div class="cssClassBillingAddress cssClassStorePayment cssBox">
        <h2 class="sfLocale">
            Billing Address :</h2>
        <ul class="cssBillingAddressUl">
        </ul>
    </div>
    <div class="cssClassServiceDetails" style="display: none">
        <ul>
            <li>
                <h4>
                    <span class="sfLocale">Service Details:</span></h4>
            </li>
            <li>
                <label class="sfLocale">
                    Service Name:</label><span class="cssClassLabel" id="serviceName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Product Name:</label><span class="cssClassLabel" id="serviceProductName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Duration:</label><span class="cssClassLabel" id="serviceDuration"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Provider Name:</label><span class="cssClassLabel" id="providerName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Store Location:</label><span class="cssClassLabel" id="storeLocationName"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Date:</label><span class="cssClassLabel" id="serviceDate"></span> </li>
            <li>
                <label class="sfLocale">
                    Available Time:</label><span class="cssClassLabel" id="availableTime"></span>
            </li>
            <li>
                <label class="sfLocale">
                    Appointment Time:</label><span class="cssClassLabel" id="bookAppointmentTime"></span>
            </li>
        </ul>
    </div>
    <br />
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2 class="sfLocale">
                <span class="sfLocale">Ordered Items:</span></h2>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table class="sfGridWrapperTable" cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td class="sfLocale">
                                Item Name
                            </td>
                            <td class="sfLocale">
                                SKU
                            </td>
                            <td class="sfLocale">
                                Shipping Address
                            </td>
                            <td class="sfLocale">
                                Shipping Rate
                            </td>
                            <td class="sfLocale">
                                Price
                            </td>
                            <td class="cssClassQtyTbl sfLocale">
                                Quantity
                            </td>
                            <td class="cssClassSubTotalTbl sfLocale">
                                Sub Total
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="sfButtonwrapper">
        <label class="cssClassDarkBtn i-arrow-left">
            <button type="button" id="lnkBack" class="sfBtn">
                <span class="sfLocale">Go back</span></button></label>
    </div>
</div>
