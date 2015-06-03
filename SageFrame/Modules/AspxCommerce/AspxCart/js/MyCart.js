var cartCalculator = "";
var CartAPI = "";
//var AspxCart = "";;
(function ($) {
    $.CartView = function (p) {

        p = $.extend({
            ShowItemImagesOnCartSetting: '',
            AllowOutStockPurchaseSetting: '',
            MinCartSubTotalAmountSetting: '',
            AllowMultipleAddShippingSetting: '',
            NoImageMyCartPathSetting: '',
            MultipleAddressChkOutURL: '',
            Coupon: '[]',
            Items: '',
            CartPRDiscount: 0,
            QuantityDiscount: 0,
            CartModulePath: '',
            AllowShippingRateEstimate: '',
            AllowCouponDiscount: '',
            AllowRealTimeNotifications: '',
            CartItemCount:0
        }, p);

        function DisableRightClick(event) {
                       if (event.button == 2) {
               
                       }
                       return false;
        }
        var couponCode = '';
        var couponSessionAmount = 0.00;
        var couponSessionPercentAmount = 0.00;
        var couponPercentValue = 0;
        var isCouponPercent = 0;
        var CartPriceDiscount = 0;
        var CartItems = [];
        var basketItems = [];
        var itemQuantityInCart=0;
        var userItemQuantityInCart=0;
        var RootPath = AspxCommerce.utils.GetAspxRootPath();

        var lowerPrice = 0;
        var ItemSubtotal = 0;


        function aspxCommonObj() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                SessionCode: AspxCommerce.utils.GetSessionCode(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonInfo;
        }

        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();

        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        var updateCart = true;
        var qtydx = 0;
        var sessionCouponCodeFlag = false;

        cartCalculator = {
            OtherAmount: [],
            OperationType: {
                add: "add",
                minus: "minus"
            },
            Items: $.parseJSON(p.Items),
            SubTotal: 0,
            getSubTotal: function () {
                var subtotal = 0;
                basketItems = [];
                $.each(cartCalculator.Items, function (index, item) {

                    subtotal += item.Quantity * parseFloat(item.Price).toFixed(2);

                    if (item.ItemTypeID != 3 && item.ItemTypeID != 2 && item.ItemTypeID != 4) {
                        var basketItem = {
                            Height: item.Height,
                            ItemName: item.ItemName,
                            Length: item.Length,
                            Width: item.Width,
                            WeightValue: item.Weight,
                            Quantity: item.Quantity
                        };
                        basketItems.push(basketItem);
                    }
                });
                return subtotal;
            },
            Clear: function () {
                this.OtherAmount = [];
            },
            Calculate: function () {
                var subtotal = cartCalculator.getSubTotal();
                var other = cartCalculator.CalculateOther();
                if (subtotal + other <= 0) {

                    this.Clear();
                    try {
                                               AspxCart.Coupon.Reset();
                        AspxCart.Discount.Init();
                    } catch (e) {

                    }

                    var subtotal = cartCalculator.getSubTotal();
                    var other = cartCalculator.CalculateOther();
                    var grandtotal = subtotal + other;
                    return grandtotal;
                }
                else {
                    var grandtotal = subtotal + other;
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

                rows += "<tr><td><strong class=\"sfLocale\">" + getLocale(AspxCartLocale, "Sub Total:") + "</strong></td><td><label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(this.getSubTotal()).toFixed(2) + "</label></td></tr>";
                this.OtherAmount.sort(function (a, b) {

                    return a.order - b.order;

                });
                $.each(this.OtherAmount, function (index, item) {
                    var localekey = getLocale(AspxCartLocale, item.key);

                    rows += "<tr class=\"sfOdd\"><td><strong class=\"sfLocale\">" + localekey + "</strong></td>";
                    if (cartCalculator.OperationType.add == item.operationType) {

                        rows += "<td>";
                    } else {
                        rows += "<td><span class=\"cssClassNegative\">-</span>";
                    }

                    rows += "<label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(item.value).toFixed(2) + "</label></td></tr>";

                });
                rows += "<tr><td><strong class=\"sfLocale\">" + getLocale(AspxCartLocale, "Grand Total:") + "</strong></td><td><label class=\"cssClassFormatCurrency sfLocale\" >" + parseFloat(this.Calculate()).toFixed(2) + "</label></td></tr>";

                $(".cssClassSubTotalAmount").html(rows);
                
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
        
        AspxCart = {
            config: {
                isPostBack: false,
                async: true,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: '',
                ajaxCallMode: "",
                error: ""
            },
            SetBasketItems: function (arr) {
                basketItems = arr;
            },
            Vars: {
                CartID: 0,
                CartPriceDiscount: 0
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: AspxCart.config.type,
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", userModuleIDCart);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: AspxCart.config.contentType,
                    cache: AspxCart.config.cache,
                    async: AspxCart.config.async,
                    url: AspxCart.config.url,
                    data: AspxCart.config.data,
                    dataType: AspxCart.config.dataType,
                    success: AspxCart.config.ajaxCallMode,
                    error: AspxCart.config.error
                });
                if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                    UpdateNotifications(2);
                }
            },

            RefreshCartOnUpdate: function () {
                if ($("#lnkMyCart").length > 0) {
                    HeaderControl.GetCartItemTotalCount();
                }
                if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                    CartItems.length = 0;
                    $(".num-pallets-input").each(function () {
                        dynamicCartItemID = parseInt($(this).attr("itemID"));
                        dynamicCartItemSKU = $(this).attr("sku");
                        CartItems.push({ CartItemIDs: dynamicCartItemID, CartItemSkus: dynamicCartItemSKU });
                    });

                    try {
                        var itemOnCart = $.connection._aspxrthub;
                        itemOnCart.server.checkIfItemOutOfStockFromCart(CartItems, AspxCommerce.AspxCommonObj());
                    }
                    catch (Exception) {
                        console.log(getLocale(AspxCartLocale, 'Error Connecting Hub.'));
                    }
                }

                if ($("#lnkShoppingBag").length > 0) {
                    ShopingBag.GetCartItemCount();
                }
                AspxCart.GetUserCartDetails();
                csscody.info("<h2>" + getLocale(AspxCartLocale, "Successful Message") + "</h2><p>" + getLocale(AspxCartLocale, "Your cart has been updated successfully.") + "</p>");
            },

            GetCartInfoForRate: function () {

                var basketItem = {
                    Height: "5",
                    ItemName: "testI",
                    Length: "5",
                    Width: "4",
                    Weight: 0
                };
                return basketItems;
            },
            BindCartDetails: function (msg) {
                cartCalculator.Items = msg.d;
                var length = msg.d.length;
                if (length > 0) {
                    $('.cssClassSubTotalAmount,.cssClassLeftRightBtn,.cssClassapplycoupon,.cssClassCartbtn').show();
                    var cartHeading = '';
                    var cartElements = '';
                    cartHeading += '<table cellspacing="0" cellpadding="0" border="0" width="100%" id="tblCartList" class="sfGridTableWrapper">';
                    cartHeading += '<thead><tr class="cssClassHeadeTitle">';
                    cartHeading += '<th class="cssClassSN">Sn.';
                    if (p.ShowItemImagesOnCartSetting.toLowerCase() == 'true') {
                        cartHeading += '</th><th class="cssClassItemImageWidth">';
                        cartHeading += getLocale(AspxCartLocale, 'Item Description');
                    }
                    cartHeading += '</th><th>';
                    cartHeading += getLocale(AspxCartLocale, 'Variants');
                    cartHeading += '</th>';
                    cartHeading += '<th class="cssClassQTY">';
                    cartHeading += getLocale(AspxCartLocale, 'Qty');
                    cartHeading += '</th>';
                    cartHeading += '<th class="cssClassItemPrice">';
                    cartHeading += getLocale(AspxCartLocale, 'Unit Price');
                    cartHeading += '</th>';           
                    cartHeading += '<th class="cssClassSubTotal">';
                    cartHeading += getLocale(AspxCartLocale, 'Line Total');
                    cartHeading += '</th>';
                    cartHeading += '<th class="cssClassAction">';
                    cartHeading += getLocale(AspxCartLocale, 'Action');
                    cartHeading += '</th>';                   
                    cartHeading += '</tr>';
                    cartHeading += '</thead>';
                    cartHeading += '<tbody>';
                    cartHeading += '</table>';
                    $("#divCartDetails").html(cartHeading);
                    basketItems = [];
                    var value;
                    for (var index = 0; index < length; index++) {
                        
                        value = cartCalculator.Items[index];
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
                        var imagePath = itemImagePath + value.ImagePath;
                        if (value.ImagePath == "") {
                            imagePath = p.NoImageMyCartPathSetting;
                        } else if (value.AlternateText == "") {
                            value.AlternateText = value.Name;
                        }
                        if ((index+1) % 2 == 0) {
                            cartElements += '<tr class="sfEven" >';
                        }
                        else {
                            cartElements += '<tr class="sfOdd" >';
                        }                        
                        cartElements += '<td>';
                        cartElements += '<b>' + (index+1) + "." + '</b>';
                        cartElements += '</td>';
                        if (value.ItemTypeID == 6) {
                            if (p.ShowItemImagesOnCartSetting.toLowerCase() == 'true') {
                                cartElements += '<td>';
                                cartElements += '<p class="cssClassCartPicture">';
                                cartElements += '<img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + value.AlternateText + '" title="' + value.AlternateText + '"></p>';
                            }
                            var configs = value.ShortDescription.split('</br>');
                            var ul = "<ul>";
                            $.each(configs, function (ind, li) {

                                ul += "<li>" + li + "</li>";
                            });
                            ul += "<ul class='kitInfo' >";


                            cartElements += '<div class="cssClassCartPictureInformation">';


                            cartElements += '<a href="item/' + value.SKU + pageExtension + '"  costvariants="' + value.CostVariants + '" onclick=AspxCart.SetCostVartSession(this);>' + value.ItemName + ' </a>';
                            cartElements += '' + ul + '';
                            cartElements += '</div>';
                          
                            cartElements += '</td>';
                        }
                        else {
                            cartElements += '<td>';
                            if (p.ShowItemImagesOnCartSetting.toLowerCase() == 'true') {

                                cartElements += '<p class="cssClassCartPicture">';
                                cartElements += '<img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + value.AlternateText + '" title="' + value.AlternateText + '"></p>';
                            }
                            cartElements += '<div class="cssClassCartPictureInformation">';

                            if (value.CostVariantsValueIDs != '') {
                                cartElements += '<a href="item/' + value.SKU + pageExtension + '?varId=' + value.CostVariantsValueIDs + '"  costvariants="' + value.CostVariants + '" onclick=AspxCart.SetCostVartSession(this);>' + value.ItemName + ' </a>';
                            } else {
                                cartElements += '<a href="item/' + value.SKU + pageExtension + '"  costvariants="' + value.CostVariants + '" onclick=AspxCart.SetCostVartSession(this);>' + value.ItemName + ' </a>';
                            }
                            if (value.ItemTypeID == 3) {
                                cartElements += "<ul class='giftcardInfo'>";
                                cartElements += "<li>";
                                cartElements += value.ShortDescription;
                                cartElements += "</li>";
                                cartElements += "</ul>";
                            }
                            cartElements += '</div>';

                           
                            cartElements += '</td>';

                        }
                        cartElements += '<td class="row-variants">';
                        cartElements += '' + value.CostVariants + '';
                        cartElements += '</td>';
                        cartElements += '<td class="cssClassQTYInput">';
                        cartElements += '<input class="num-pallets-input" price="' + parseFloat(value.Price).toFixed(2) + '" id="txtQuantity_' + value.CartItemID + '" type="text" cartID="' + value.CartID + '" value="' + value.Quantity + '" quantityInCart="' + value.Quantity + '" actualQty="' + value.ItemQuantity + '" costVariantID="' + value.CostVariantsValueIDs + '" itemID="' + value.ItemID + '" addedValue="' + value.Quantity +'" sku="' + value.SKU + '" minCartQuantity="' + value.MinCartQuantity + '" maxCartQuantity="' + value.MaxCartQuantity + '" autocomplete="off">';
                        cartElements += '<label class="lblNotification" style="color: #FF0000;"></label></td>';
                       
                        cartElements += '<td class="price-per-pallet">';
                        cartElements += '<span class="cssClassFormatCurrency">' + parseFloat(value.Price).toFixed(2) + '</span>';
                        cartElements += '</td>';
                     
                        cartElements += '<td class="row-total">';
                        cartElements += '<input class="row-total-input cssClassFormatCurrency" autocomplete="off" id="txtRowTotal_' + value.CartID + '" value="' + parseFloat(value.TotalItemCost).toFixed(2) + '"  readonly="readonly" type="text" />';
                        cartElements += '</td>';
                       
                        cartElements += '<td>';
                        cartElements += '<a class="ClassDeleteCartItems" title="Delete" value="' + value.CartItemID + '" cartID="' + value.CartID + '"itemID="' + value.ItemID + '"sku="' + value.SKU + '" ><i class="i-delete" original-title=\"' + getLocale(AspxCartLocale, "Delete") + '\"></i></a>';
                        cartElements += '</td>';
                        cartElements += '</tr>';
                        AspxCart.Vars.CartID = value.CartID;
                    };
                    AspxCart.Discount.Init();
                    AspxCart.Coupon.Init();
                    $("#tblCartList").append(cartElements);
                    $("#tblCartList tr:even ").addClass("sfEven");
                    $("#tblCartList tr:odd ").addClass("sfOdd");
                    //$(".tipsy").remove();
                    //$('#tblCartList .cssClassCartPicture img[title]').tipsy();
                    //$("#tblCartList .i-delete").tipsy();

                } else {
                    if (!$("#dvEstimateRate").is(":hidden")) {
                        $("#dvEstimateRate").remove();
                    }
                    AspxCart.Coupon.Reset();                   
                    $(".cssClassCartInformation").html("<span class=\"cssClassNotFound\">" + getLocale(AspxCartLocale, "Your cart is empty!") + "</span>");
                }
                AspxCart.BindCartFunctions();

              
            },
            BindCartFunctions: function () {
                $(".ClassDeleteCartItems").off().on("click", function () {
                    var cartId = $(this).attr("cartID");
                    var cartItemId = $(this).attr("value");
                    var properties = {
                        onComplete: function (e) {
                            AspxCart.DeleteCartItem(cartId, cartItemId, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxCartLocale, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCartLocale, "Are you sure you want to delete this item from your cart?") + "</p>", properties);
                    return false;
                });


                $(".num-pallets-input").off().on("contextmenu", function (e) {
                    return false;
                });
                $('.num-pallets-input').on('paste', function (e) {
                    e.preventDefault();
                });
          
            },
            RefreshCartOnDelete: function () {
                                if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                CartItems.length = 0;
                $(".num-pallets-input").each(function () {
                    dynamicCartItemID = parseInt($(this).attr("itemID"));
                    dynamicCartItemSKU = $(this).attr("sku");
                    CartItems.push({ CartItemIDs: dynamicCartItemID, CartItemSkus: dynamicCartItemSKU });
                });
                try{
                    var itemOnCart = $.connection._aspxrthub;
                    itemOnCart.server.checkIfItemOutOfStockFromCart(CartItems, AspxCommerce.AspxCommonObj());
                }
                catch(Exception)
                {
                  console.log(getLocale(AspxCartLocale, 'Error Connecting Hub.'));
                }
                }               
                if ($("#lnkMyCart").length > 0) {
                    HeaderControl.GetCartItemTotalCount();                }
                if ($("#lnkShoppingBag").length > 0) {
                    ShopingBag.GetCartItemCount();                 
                } 
                AspxCart.GetUserCartDetails();
                if (RewardedPoint != undefined)
                    RewardedPoint();
            },
            RefreshCartOnClear: function () {
                                if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                CartItems.length = 0;
                $(".num-pallets-input").each(function () {
                    dynamicCartItemID = parseInt($(this).attr("itemID"));
                    dynamicCartItemSKU = $(this).attr("sku");
                    CartItems.push({ CartItemIDs: dynamicCartItemID, CartItemSkus: dynamicCartItemSKU });
                });
                try{
                    var itemOnCart = $.connection._aspxrthub;
                    itemOnCart.server.checkIfItemOutOfStockFromCart(CartItems, AspxCommerce.AspxCommonObj());
                }
                catch(Exception)
                {
                    console.log(getLocale(AspxCartLocale, 'Error Connecting Hub.'));
                }
                }
                AspxCart.Coupon.Reset();
                if ($("#lnkMyCart").length > 0) {
                    HeaderControl.GetCartItemTotalCount();                }
                if ($("#lnkShoppingBag").length > 0) {
                    ShopingBag.GetCartItemCount();
                    ShopingBag.GetCartItemListDetails();                }
                if ($("#divRelatedItems").length > 0) {
                    YouMayAlsoLike.GetItemRetatedUpSellAndCrossSellList();
                }
                AspxCart.GetUserCartDetails();
            },
            SetItemQuantityInCart: function (msg) {          
                itemQuantityInCart = msg.d.ItemQuantityInCart;
                userItemQuantityInCart=msg.d.UserItemQuantityInCart
            },
            Coupon: function () {            
                var coupons = $.parseJSON(p.Coupon);
                var $ajaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", userModuleIDCart);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: p.CartModulePath + 'CartHandler.ashx/' + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", userModuleIDCart);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        async: true,
                        url: aspxservicePath + 'securepost.ashx?call=' + method,
                        data: param,
                        success: successFx,
                        error: error
                    });
                };

                VerifyCouponCode = function (itemIds, cartItemIds) {

                    couponCode = $.trim($("#txtCouponCode").val());
                    var totalCost = cartCalculator.Calculate();
                    if (couponCode == "") {
                        csscody.alert("<h2>" + getLocale(AspxCartLocale, 'Information Alert') + "</h2><p>" + getLocale(AspxCartLocale, "Please enter coupon code!") + "</p>");
                        return false;
                    } else {

                        var coupon = getCoupon(couponCode);
                        var aCount = coupon.AppliedCount;
                        if (aCount != 0) {
                            aCount = parseInt(aCount) + 1;
                        }

                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.CultureName = null;
                        aspxCommonInfo.CustomerID = null;
                        aspxCommonInfo.SessionCode = null;

                        $ajaxCall("VerifyCouponCode", JSON2.stringify({
                            totalCost: totalCost,
                            couponCode: couponCode,
                            itemIds: itemIds,
                            cartItemIds: cartItemIds,
                            aspxCommonObj: aspxCommonInfo,
                            appliedCount: aCount
                        }),
                            function (data) {
                                VerifyCoupon(data);
                            }, function () {
                                csscody.error("<h2>" + getLocale(AspxCartLocale, "Error Message") + "</h2><p>" + getLocale(AspxCartLocale, "Sorry! error occured!") + "</p>");
                            });
                    }

                }
                ResetCouponSession = function (sessionKey) {

                    $securepost("RSC", {}, function (data) {
                        coupons = [];
                    }, function () { });

                }


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

                VerifyCoupon = function (msg) {                   
                    var item = msg.d;
                    if (item.Verification) {
                        var isUsedCoupon = false;

                        var coupon = getCoupon(couponCode);
                        if (coupon.Key != '')
                            isUsedCoupon = true;
                        coupon.AppliedCount++;

                        if (!sessionCouponCodeFlag) {
                            coupon.Key = couponCode;
                            coupon.IsForFreeShipping = false;
                            if (item.IsForFreeShipping.toLowerCase() == "yes") {

                                coupon.IsForFreeShipping = true;
                                if (!isUsedCoupon)
                                    coupons.push(coupon);
                                $securepost("SSC", {
                                    k: 'CC',
                                    v: JSON.stringify(coupons)
                                }, function (data) { }, function () { });
                                $("#txtCouponCode").val('');
                                csscody.info("<h2>" + getLocale(AspxCartLocale, 'Information Message') + '</h2><p>' + getLocale(AspxCartLocale, "Congratulation! you need not to worry about shipping cost. It's free!!") + "</p>");

                            } else {
                                var couponAmount;
                                var actualAmount = cartCalculator.Calculate();
                                var newCouponSessionValue;
                                if (item.IsPercentage == true) {
                                    coupon.IsPercentage = true;
                                    coupon.Value = item.CouponAmount;
                                } else {
                                    coupon.Value = item.CouponAmount;
                                    coupon.IsPercentage = false;
                                }
                                if (!isUsedCoupon)
                                    coupons.push(coupon);
                                                               $securepost("SSC", {
                                    k: 'CC',
                                    v: JSON.stringify(coupons)
                                }, function (data) { }, function () { });

                                this.recalculateDiscount();                  
                                $("#txtCouponCode").val('');

                                if (item.IsPercentage == true) {
                                    csscody.info("<h2>" + getLocale(AspxCartLocale, "Information Message") + "</h2><p>" + getLocale(AspxCartLocale, "Congratulation! you have got discount amount of") + ' ' + item.CouponAmount + "%.</p>");
                                } else {
                                    csscody.info("<h2>" + getLocale(AspxCartLocale, "Information Message") + "</h2><p>" + getLocale(AspxCartLocale, "Congratulation! you have got discount amount of") + ' ' + "<span class='cssClassFormatCurrency'>" + parseFloat(item.CouponAmount).toFixed(2)+ "</span>.</p>");
                                }
                                var  cookieCurrency = $("#ddlCurrency").val();                                
                                Currency.currentCurrency = BaseCurrency;                                
                                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                            }
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Coupon code is already used, Multiple coupon is not allowed.") + "</p>");
                            $("#txtCouponCode").val('');
                        }
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Coupon is either invalid, expired, reached it's usage limit or exceeded your cart total purchase amount!") + "</p>");
                        $("#txtCouponCode").val('');
                    }

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
                    var order = 1;
                    $.each(coupons, function (index, coupon) {
                        var amount = getCouponAmount(coupon);
                        order++;
                          var localekey = getLocale(AspxCartLocale, "Coupon Discount");
                        cartCalculator.AddOtherAmount(localekey+"(" + coupon.Key + ")", amount, cartCalculator.OperationType.minus, order);


                    });
                    cartCalculator.ShowData();
                }

                var init = function () {

                    if (coupons.length > 0) {
                        var order = 1;
                        $.each(coupons, function (index, coupon) {
                            var amount = getCouponAmount(coupon); order++;
                              var localekey = getLocale(AspxCartLocale, "Coupon Discount");
                            cartCalculator.AddOtherAmount(localekey+"(" +  coupon.Key + ")", amount, cartCalculator.OperationType.minus, order);


                        });

                    }
                    if (p.AllowShippingRateEstimate.toLowerCase() == 'true')
                    {
                        $("#divShippingRate").show();
                    }
                    if (p.AllowCouponDiscount.toLowerCase() == 'true') {
                        $("#divCouponDiscountBox").show();
                    }
                    $("#txtCouponCode").val('');
                    $("#btnSubmitCouponCode").bind("click", function () {
                        var itemIds = "";
                        var cartItemIds = "";
                        $(".num-pallets-input").each(function () {
                            itemIds += $(this).attr("itemID") + ',';
                            cartItemIds += $(this).attr("id") + ',';
                            cartItemIds = cartItemIds.replace("txtQuantity_", "");
                        });
                        itemIds = itemIds.substr(0, itemIds.length - 1);
                        cartItemIds = cartItemIds.substr(0, cartItemIds.length - 1);
                        VerifyCouponCode(itemIds, cartItemIds);
                        return false;
                    });
                    $('.cssClassCouponHelp').hide();
                    $('#txtCouponCode').bind("focus", function () {
                        $('.cssClassCouponHelp').show();
                    });
                    $('#txtCouponCode').bind("focusout", function () {
                        $('.cssClassCouponHelp').hide();
                    });

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
                    Reset: ResetCouponSession,
                };
            }(),

            Discount: function () {

                var quantityDiscount = p.QuantityDiscount;
                var cartPricerulDiscount = p.CartPRDiscount;
              
                var totalDiscount = parseFloat(quantityDiscount) + parseFloat(cartPricerulDiscount);
              
                cartCalculator.AddOtherAmount("Cart Discount:", totalDiscount, cartCalculator.OperationType.minus, 1);
                cartCalculator.ShowData();

                var $coreAjaxCall = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", userModuleIDCart);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
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
                        beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", userModuleIDCart);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: p.CartModulePath + "CartHandler.ashx/" + method,
                        data: param,
                        dataType: "json",
                        success: successFx,
                        error: error
                    });
                };
                var $securepost = function (method, param, successFx, error) {
                    $.ajax({
                        type: "POST",
                        beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", userModuleIDCart);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
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
                            $securepost("SS", { k: 'DiscountAmount', v: totalDiscount }, function (data) { }, function () { });

                            cartCalculator.AddOtherAmount("Cart Discount:", totalDiscount, cartCalculator.OperationType.minus, 1);
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
                var init = function () {

                    GetDiscountCartPriceRule(cartCalculator.Items[0].CartID, 0);
                };

                return { Init: init };
            }(),

            GetCartDetailsErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCartLocale, "Error Message") + "</h2><p>" + getLocale(AspxCartLocale, "Failed to load cart's details!") + "</p>");
            },
            DeleteCartItemErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCartLocale, "Error Message") + "</h2><p>" + getLocale(AspxCartLocale, "Failed to delete cart's items!") + "</p>");
            },
            ClearCartItemsErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxCartLocale, "Error Message") + "</h2><p>" + getLocale(AspxCartLocale, "Failed to clear cart's items!") + "</p>");
            },
            CouponUserRecordErrorMsg: function () {
                csscody.error('<h2>' + getLocale(AspxCartLocale, 'Error Message') + "</h2><p>" + getLocale(AspxCartLocale, "Failed to update coupon user!") + '</p>');
            },

            UpdateCart: function (cartItemId, cartID, quantity) {

                var updateCartInfo = {
                    CartID: cartID,
                    CartItemIDs: cartItemId,
                    Quantities: quantity,
                    AllowOutOfStock: p.AllowOutStockPurchaseSetting.toLowerCase()
                };
               
                AspxCart.config.method = "CartHandler.ashx/UpdateShoppingCart";
                AspxCart.config.url = p.CartModulePath + AspxCart.config.method;
                AspxCart.config.data = JSON2.stringify({
                    updateCartObj: updateCartInfo,
                    aspxCommonObj: aspxCommonObj()
                });
                AspxCart.config.ajaxCallMode = AspxCart.RefreshCartOnUpdate;
                AspxCart.ajaxCall(AspxCart.config);

            },


            SetCostVartSession: function (obj) {
                if ($(obj).attr("costvariants") != null) {
                    itemCostVariantData = $(obj).attr("costvariants");
                    $.session("ItemCostVariantData", 'empty');
                    $.session("ItemCostVariantData", itemCostVariantData);
                }
            },
            GetUserCartDetails: function () {
                AspxCart.config.method = "AspxCoreHandler.ashx/GetCartDetails";
                AspxCart.config.url = AspxCart.config.baseURL + AspxCart.config.method;
                AspxCart.config.async = false;
                AspxCart.config.data = JSON2.stringify({
                    aspxCommonObj: aspxCommonObj()
                });
                AspxCart.config.ajaxCallMode = AspxCart.BindCartDetails;
                AspxCart.config.error = AspxCart.GetCartDetailsErrorMsg;
                AspxCart.ajaxCall(AspxCart.config);

            },

            DeleteCartItem: function (cartId, cartItemId, event) {
                if (event) {
                    var aspxCommonInfo = aspxCommonObj();
                    aspxCommonInfo.CultureName = null;
                    aspxCommonInfo.UserName = null;
                    AspxCart.config.method = "AspxCoreHandler.ashx/DeleteCartItem";
                    AspxCart.config.url = AspxCart.config.baseURL + AspxCart.config.method;
                    AspxCart.config.data = JSON2.stringify({
                        cartID: cartId,
                        cartItemID: cartItemId,
                        aspxCommonObj: aspxCommonInfo
                    });
                    AspxCart.config.ajaxCallMode = AspxCart.RefreshCartOnDelete;
                    AspxCart.config.error = AspxCart.DeleteCartItemErrorMsg;
                    AspxCart.ajaxCall(AspxCart.config);
                }
            },
            ClearCartItems: function (event) {
                if (event) {
                    var aspxCommonInfo = aspxCommonObj();
                    aspxCommonInfo.CultureName = null;
                    aspxCommonInfo.UserName = null;
                    var cartID = $("#tblCartList .ClassDeleteCartItems").attr("cartid");
                    AspxCart.config.method = "CartHandler.ashx/ClearAllCartItems";
                    AspxCart.config.url = p.CartModulePath + AspxCart.config.method;
                    AspxCart.config.data = JSON2.stringify({
                        cartID: cartID,
                        aspxCommonObj: aspxCommonInfo
                    });
                    AspxCart.config.ajaxCallMode = AspxCart.RefreshCartOnClear;
                    AspxCart.config.error = AspxCart.ClearCartItemsErrorMsg;
                    AspxCart.ajaxCall(AspxCart.config);
                }
            },
            CheckItemQuantityInCart: function (itemId, itemCostVariantIDs) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.CultureName = null;
                aspxCommonInfo.UserName = null;
                AspxCart.config.async = false;
                AspxCart.config.method = "AspxCoreHandler.ashx/CheckItemQuantityInCart";
                AspxCart.config.url = aspxservicePath + AspxCart.config.method;
                AspxCart.config.data = JSON2.stringify({
                    itemID: itemId,
                    aspxCommonObj: aspxCommonInfo,
                    itemCostVariantIDs: itemCostVariantIDs
                });
                AspxCart.config.ajaxCallMode = AspxCart.SetItemQuantityInCart;
                AspxCart.config.error = 7;
                AspxCart.ajaxCall(AspxCart.config);
                var itemsCartInfo = {
                ItemQuantityInCart: itemQuantityInCart,
                UserItemQuantityInCart: userItemQuantityInCart
            };
            return itemsCartInfo;
            },

            Init: function () {
                AspxCart.BindCartFunctions();
                if(p.CartItemCount > 0)
                {
                    $(".cssMyCartContainer").show();
                }
                               $("#divCheckOutMultiple").click(function () {
                    if ($(".cssClassBlueBtn ").length > 0) {
                        if ($("#txtTotalCost").attr("value").replace(/[^-0-9\.]+/g, "") < 0) {
                            csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You can't proceed to checkout your amount is not applicable!") + "</p>");
                            return false;
                        }
                    }
                    var totalCartItemPrice = HeaderControl.GetTotalCartItemPrice();
                    if (totalCartItemPrice == 0) {
                        csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You have not added any items in cart yet!") + "</p>");
                        AspxCart.GetUserCartDetails();
                        return false;
                    }
                    if (totalCartItemPrice < p.MinCartSubTotalAmountSetting) {
                        csscody.alert("<h2>" + getLocale(AspxHeaderControl, "Information Alert") + "</h2><p>" + getLocale(AspxHeaderControl, "You are not eligible to proceed further. Your order amount must be at least") + "<span class='cssClassFormatCurrency'>" + p.MinCartSubTotalAmountSetting + "</span></p>");

                    } else {
                        var multipleAddressLink = '';
                        if (userFriendlyURL) {
                            multipleAddressLink = p.MultipleAddressChkOutURL + pageExtension;
                        } else {
                            multipleAddressLink = p.MultipleAddressChkOutURL;
                        }
                        if (aspxCommonObj().CustomerID > 0 && aspxCommonObj().UserName.toLowerCase() != 'anonymoususer') {

                            window.location = AspxCommerce.utils.GetAspxRedirectPath() + multipleAddressLink;

                        } else {
                            if (allowAnonymousCheckOutSetting.toLowerCase() == 'true') {
                                window.location = AspxCommerce.utils.GetAspxRedirectPath() + multipleAddressLink;

                            } else {
                                csscody.alert('<h2>' + getLocale(AspxHeaderControl, 'Information Alert') + '</h2><p>' + getLocale(AspxHeaderControl, 'Checkout is not allowed for anonymous user!') + '</p>');
                            }
                        }
                    }
                    return false;
                });
            
                if (userFriendlyURL) {

                    $("#lnkContinueShopping").bind("click", function () {
                        CartItems.length = 0;
                        $(".num-pallets-input").each(function () {
                            dynamicCartItemID = parseInt($(this).attr("itemID"));
                            dynamicCartItemSKU = $(this).attr("sku");
                            CartItems.push({ CartItemIDs: dynamicCartItemID, CartItemSkus: dynamicCartItemSKU });
                        });
                         if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                       try{
                        var itemOnCart = $.connection._aspxrthub;
                        $.each(CartItems, function (index, item) {
                          itemOnCart.server.checkIfItemOutOfStockFromCart(CartItems, AspxCommerce.AspxCommonObj());
                        });
                        }
                        catch(Exception)
                        {
                         console.log(getLocale(AspxCartLocale, 'Error Connecting Hub.'));
                        }
                        }
                         window.location.href = aspxRedirectPath + homeURL + pageExtension;
                         return false;
                    });
                } else {
                    CartItems.length = 0;
                    $(".num-pallets-input").each(function () {
                        dynamicCartItemID = parseInt($(this).attr("itemID"));
                        dynamicCartItemSKU = $(this).attr("sku");
                        CartItems.push({ CartItemIDs: dynamicCartItemID, CartItemSkus: dynamicCartItemSKU });
                    });
                     if (p.AllowRealTimeNotifications.toLowerCase() == 'true') {
                    try{
                    var itemOnCart = $.connection._aspxrthub;
                    $.each(CartItems, function (index, item) {
                        itemOnCart.server.checkIfItemOutOfStockFromCart(CartItems, AspxCommerce.AspxCommonObj());
                    });
                    }
                    catch(Exception)
                        {
                        console.log(getLocale(AspxCartLocale, 'Error Connecting Hub.'));
                        }
                        }

                    $("#lnkContinueShopping").bind("click", function () {
                        window.location.href = aspxRedirectPath + homeURL;
                        return false;
                    });
                }
                if (aspxCommonObj().CustomerID > 0 && aspxCommonObj().UserName.toLowerCase() != 'anonymoususer' && p.AllowMultipleAddShippingSetting.toLowerCase() == 'true') {
                    $("#divCheckOutMultiple").show();
                }


                $("#btnClear").bind("click", function () {
                    var properties = {
                        onComplete: function (e) {
                            AspxCart.ClearCartItems(e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxCartLocale, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxCartLocale, "Are you sure you want to clear all cart's items?") + "</p>", properties);
                    return false;
                });

                $("#btnUpdateShoppingCart").bind("click", function () {
                    var cartItemId = '';
                    var quantity = '';
                    var cartID = 0;
                    var updateCart = true;
                    $(".num-pallets-input").each(function () {
                        if ($(this).val() == "" || $(this).val() <= 0) {
                            updateCart = false;                           
                            return false;
                        }
                        var totQtyInTxtBox = parseInt($.trim($(this).val()));
                        var initQtyInCart = parseInt($(this).attr("quantityincart"));
                        var itemId = $(this).attr("itemID");
                        var itemCostVariantIDs = $.trim($(this).attr("costvariantid"));
                        if(itemCostVariantIDs=="")
                         {
                           itemCostVariantIDs='0';
                         }
                        var itemsCartInfo = AspxCart.CheckItemQuantityInCart(itemId, itemCostVariantIDs + '@');
                        var itemQuantityInCart = itemsCartInfo.ItemQuantityInCart;
                        var userItemQuantityInCart = itemsCartInfo.UserItemQuantityInCart;
                        var minCartQuantity=parseInt($(this).attr("minCartQuantity"));
                        var maxCartQuantity=parseInt($(this).attr("maxCartQuantity"));
                        $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html('');
                        if (itemQuantityInCart != -1) {                            if (p.AllowOutStockPurchaseSetting.toLowerCase() == 'false') {
                                                          if (parseInt(totQtyInTxtBox + userItemQuantityInCart - initQtyInCart)  < minCartQuantity) {
                                   $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html(getLocale(AspxCartLocale, 'You cannot add less than' + minCartQuantity + 'quantity of this item in cart'));
                                    updateCart = false;
                                    return false;
                                }
                               else if (parseInt(totQtyInTxtBox + itemQuantityInCart - initQtyInCart) > parseInt($.trim($(this).attr("actualQty")))) {                                   
                                    $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html(getLocale(AspxCartLocale, 'The Quantity Is Greater Than The Available Quantity.'));
                                    updateCart = false;
                                    return false;
                                }
                                                            else if (parseInt(totQtyInTxtBox + userItemQuantityInCart - initQtyInCart)   > maxCartQuantity) {
                                    $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html(getLocale(AspxCartLocale, 'You have reached limit of this item in cart'));
                                    updateCart = false;
                                    return false;
                                }
                            } else {
                                $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                                updateCart = true;
                            }
                        }
                    });
                    
                    if (updateCart == true) {
                        $(".num-pallets-input").each(function () {
                            cartItemId += parseInt($(this).attr("id").replace(/[^0-9]/gi, '')) + ',';
                            quantity += $(this).val() + ',';
                            cartID = $(this).attr("cartID");                          
                        });
                        AspxCart.UpdateCart(cartItemId, cartID, quantity);
                        if( RewardedPoint !=undefined)
                            RewardedPoint();
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Message") + "</h2><p>" + getLocale(AspxCartLocale, 'Your cart contains invalid quantity!') + "</p>");
                    }
                    return false;
                });
            }
        };
        AspxCart.Init();
        CartAPI["GetCartInfoForRate"] = AspxCart.GetCartInfoForRate;
    };

    $.fn.MyCart = function (p) {

        $.CartView(p);
    };
})(jQuery);