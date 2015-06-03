var ShopingBag = "";
$(function () {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID(),
        SessionCode: AspxCommerce.utils.GetSessionCode()
    };
    var itemCostVariantData = '';
    var cartItems = '';

    findPos = function (obj) {
        var curleft = curtop = 0;
        if (obj.offsetParent) {
            curleft = obj.offsetLeft;
            curtop = obj.offsetTop;
            while (obj = obj.offsetParent) {
                curleft += obj.offsetLeft;
                curtop += obj.offsetTop;
            }
        }
        return [curleft, curtop];
    };

    ShopingBag = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: "",
            ajaxCallMode: ""
        },

        init: function () {
            $('.cssClassShoppingCart').outside('click', function () {
                $('.Shopingcartpopup').stop(true, true).slideUp('slow');
            });
            ShopingBag.LoadAllImages();
            ShopingBag.hideShoppingCart();
            $("#divMiniShoppingCart").hide();
            if (allowAddToCart.toLowerCase() == 'true') {
                if (showMiniShopCart.toLowerCase() == 'true') {
                    ShopingBag.GetCartItemCount();
                    $("#divMiniShoppingCart").show();
                    if (BagType == "Popup") {
                        var closeIcon = '<div class="cssClassCloseIcon"> <button type="button" class="cssClassClose"><span class="sfLocale">Close</span></button></div>';
                        $(".Shopingcartpopup").removeAttr('id').attr('id', 'popuprel6');
                        $(".Shopingcartpopup").addClass("popupbox");
                        $(".Shopingcartpopup").prepend(closeIcon);
                        $("#divMiniShoppingCart a").attr('rel', 'popuprel6');
                        $("#lnkShoppingBag").removeAttr('onclick');
                        $("#divMiniShoppingCart a").removeAttr('onclick');
                        $("#imgarrow").hide();
                        if (HeaderControl.vars.itemCount > 0) {
                            $("#divMiniShoppingCart a").on('click', function () {

                                if (BagType == "Popup") {
                                    ShopingBag.showShoppingCartForPopup();
                                    $('#tblListCartItems').next('div').next().show();
                                    $('#tblListCartItems').next('div').show();
                                    ShowPopup(this);
                                }
                            });
                        }
                    }
                }
            }

            $("#lnkViewCart").bind('click', function () {
                    window.location = AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL + pageExtension;      
            });

            $("#lnkMiniCheckOut").click(function () {

                if (aspxCommonObj.CustomerID <= 0 && aspxCommonObj.UserName.toLowerCase() == 'anonymoususer') {
                    if (allowAnonymousCheckOut.toLowerCase() == 'false') {
                        csscody.alert('<h2>' + getLocale(AspxShoppingBagHeader, "Information Alert") + "</h2><p>" + getLocale(AspxShoppingBagHeader, "Checkout is not allowed for Anonymous User!") + '</p>');
                        return false;
                    }
                }
                var singleAddressLink = '';
                    singleAddressLink = singleAddressChkOutURL + pageExtension;    
                var totalCartItemPrice = ShopingBag.GetTotalCartItemPrice();
                if (totalCartItemPrice < minCartSubTotalAmount) {
                    csscody.alert('<h2>' + getLocale(AspxShoppingBagHeader, "Information Alert") + '</h2><p>' + getLocale(AspxShoppingBagHeader, "You are not eligible to proceed further:Your Order Amount is too low!!!") + '</p>');
                    return false;
                }
                else {
                    if (aspxCommonObj.CustomerID > 0 && aspxCommonObj.UserName.toLowerCase() != 'anonymoususer') {
                        window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                    }
                    else {
                        if (allowAnonymousCheckOut.toLowerCase() == 'true') {
                            window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                        }
                    }
                }
            });
            $(".cssClassClose").bind("click", function () {
                $('body').removeClass('cssNoBodyScroll');
                $('#fade, #popuprel6').fadeOut();
            });
        },

        ajaxCall: function (config) {
            $.ajax({
                type: ShopingBag.config.type,
                contentType: ShopingBag.config.contentType,
                cache: ShopingBag.config.cache,
                async: ShopingBag.config.async,
                url: ShopingBag.config.url,
                data: ShopingBag.config.data,
                dataType: ShopingBag.config.dataType,
                success: ShopingBag.config.ajaxCallMode,
                error: ShopingBag.ajaxFailure
            });
        },


        SetTotalCartItemPrice: function (data) {
            ShopingBag.vars.totalPrice = data.d;
        },

        AddcartItemsDynamically: function (itemIDD, ItemSKU) {
            var isUpdate = false;
            $.each(cartItems, function (index, item) {
                $('.sfButtonwrapper').each(function () {

                    try {

                        var $btnWrappr = $(this);
                        var itemID = parseInt($(this).data().itemid);
                        if (itemID == item.ItemID) {

                            if (!(parseInt(item.ItemQuantity) == 0)) {

                                if ($btnWrappr.hasClass('cssClassOutOfStock')) {
                                    $btnWrappr.removeClass("cssClassOutOfStock");
                                    $btnWrappr.html('');
                                    $btnWrappr.append(" <label class='i-cart cssClassCartLabel cssClassGreenBtn'><button>Cart +</button></label>");
                                    var Attrbs = [];
                                    $.each($btnWrappr.data(), function (attrb, attrbvalue) {
                                        Attrbs.push({
                                            key: attrb,
                                            value: attrbvalue
                                        });

                                    })
                                    var $btn = $(this).find('button');
                                    $.each(Attrbs, function (attrbIndex, attrbs) {
                                        $btn.attr(attrbs.key, attrbs.value);
                                    });

                                    isUpdate = true;

                                }
                            }
                        }
                    }
                    catch (exception) {
                        return true;
                    }
                });
            });
            if ($("#dynItemDetailsForm").length > 0) {
                isUpdate = true;
            }
            if (allowRealTimeNotifications.toLowerCase() == 'true') {
             if (isUpdate) {
                 try {
                 
                     var itemInstock = $.connection._aspxrthub;
                     //$.connection.hub.start().done(function () {
                     //    itemInstock.server.updateItemToStock(36, 'Testsample');
                     //});
                   itemInstock.server.updateItemToStock(itemIDD, ItemSKU);
                }
                catch (e) {
                    console.log(getLocale(AspxShoppingBagHeader, 'Error Connecting Hub.'));
                }
            }
            else {
                    if (($(".tblGroupedItem").html() == "")||($(".tblGroupedItem").html() == undefined))  {
                        if ($("#dynItemDetailsForm").length > 0) {
                            ItemDetail.BindItemBasicByitemSKU(ItemSKU);
                          }
                    }
                    else {
                        var isToChangeOutofStock = false;
                        $(".tblGroupedItem >tbody >tr").each(function () {
                            if ($(this).hasClass("disabledGroupItm")) {
                                if ($(this).find("input[type='checkbox']").data().itemid == itemIDD) {
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
                                                           }
                        }
                    }

                }
            }

        },

        BindCartDetailsInShoppingBag: function (data) {     
            $("#tblListCartItems").html('');
            if (data.d.length > 0) {
                cartItems = data.d;
                $.each(data.d, function (index, item) {
                    ShopingBag.BindCartItemslist(item, index);
                });
                $("a").bind("click", function (e) {
                                       if ($(this).attr("costvariants") != null) {
                        itemCostVariantData = $(this).attr("costvariants");
                                               $.session("ItemCostVariantData", 'empty');
                        $.session("ItemCostVariantData", itemCostVariantData);
                    }
                });
               
                $(".imgCartItemDelete").bind("click", function () {
                    var cartId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
                    var cartItemId = parseInt($(this).attr("name").replace(/[^0-9]/gi, ''));
                    var ItemID = parseInt($(this).attr("ItemID").replace(/[^0-9]/gi, ''));
                    var ItemSKU = $(this).attr("sku");
                    var properties = {
                        onComplete: function (e) { 
                            if ($("#divMiniShoppingCart1").offset() != null) {
                                ShoppingCartFlyOver.DeleteCartItemByID(cartId, cartItemId, e);
                                ShopingBag.DeleteCartItemByID(cartId, cartItemId, e);
                                ShopingBag.AddcartItemsDynamically(ItemID, ItemSKU);
                                ShopingBag.GetCartItemCount();
                            }
                            else {                               
                                ShopingBag.DeleteCartItemByID(cartId, cartItemId, e);
                                ShopingBag.AddcartItemsDynamically(ItemID, ItemSKU);
                                ShopingBag.GetCartItemCount();
                            }



                        }
                    }
                    csscody.confirm("<h1>" + getLocale(AspxShoppingBagHeader, "Delete Confirmation") + "</h1><p>" + getLocale(AspxShoppingBagHeader, "Do you want to delete this item from your cart list?") + "</p>", properties);
                });
            }
            else {
                $("#ShoppingCartPopUp").hide();
            }
            var cookieCurrency = $("#ddlCurrency").val();
            Currency.currentCurrency = BaseCurrency;
            Currency.convertAll(Currency.currentCurrency, cookieCurrency);
        },

        BindShoppingBagOnDelete: function () {            
            ShopingBag.GetCartItemListDetails();
            $("#divMiniShoppingCart").show();
            if ($("#lnkMyCart").length > 0) {
                HeaderControl.GetCartItemTotalCount();
            }           
            ShopingBag.GetCartItemCount();
            if ($('#divCartDetails').length > 0) {
                if (typeof (AspxCart) == 'object') {
                    AspxCart.GetUserCartDetails();
                    if (RewardedPoint != undefined)
                        RewardedPoint();
                }
                if (typeof (CheckOutApi) == 'object') {
                    location.reload(true);
                    ShopingBag.securepost("RSC", {}, function (data) {
                        coupons = [];
                    }, function () { });
                    ShopingBag.securepost("RGC", {}, function () { }, function () { });
                }
            }

            if (BagType == "Popup") {
                if ($.trim($("#tblListCartItems").html()) == "") {
                    $('#tblListCartItems').append('<tr><td><span class="cssEmpty">' + getLocale(AspxShoppingBagHeader, "No item!") + '</span></td></tr>');
                    $('#tblListCartItems').next('div').next().hide();
                    $('#tblListCartItems').next('div').hide();
                }
            }
            return false;
        },
        securepost : function (method, param, successFx, error) {
            $.ajax({
                type: "POST",
                beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", shoppingUMId);
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
        },

        LoadAllImages: function () {
            $('#fullShoppingBag').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_full.png');
            $("#emptyShoppingBag").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_empty.png');
            $("#imgarrow").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/arrow_down.png');
        },

        hideShoppingCart: function () {
            $('#ShoppingCartPopUp').hide();
            $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_down.png");
        },

        GetCartItemCount: function () {
            var count = HeaderControl.vars.itemCount;
            var shoppingCartURLLink = '';
                shoppingCartURLLink =AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL + pageExtension;    
            if (count > 0) {
                $("#fullShoppingBag").show();
                $("#emptyShoppingBag").hide();
                var windowWidth = $(window).width();               
                if (windowWidth > 480) {
                    if (BagType != "Popup") {
                        $("#imgarrow").show();
                        $("#cartItemCount").html('<a onclick="if(!this.disabled){ShopingBag.showShoppingCart();};" href="javascript:void(0);" id="lnkshoppingcart">' + getLocale(AspxShoppingBagHeader, "Cart") + '[ ' + count + ' ]</a>');
                    }
                    else {
                        $("#cartItemCount").html('<a  href="javascript:void(0);" id="lnkshoppingcart" rel="popuprel6">' + getLocale(AspxShoppingBagHeader, "Cart") + '[ ' + count + ' ]</a>');
                        if ($(".Shopingcartpopup").hasClass("popupbox")) {
                            $("#divMiniShoppingCart a").on('click', function () {
                                if (BagType == "Popup") {
                                    ShopingBag.showShoppingCartForPopup();
                                    $('#tblListCartItems').next('div').next().show();
                                    $('#tblListCartItems').next('div').show();
                                    ShowPopup(this);
                                }
                            });
                        }

                    }
                }
                else {
                    $("#fullShoppingBag").find('a').removeAttr('onclick');
                    $("#emptyShoppingBag").find('a').removeAttr('onclick');
                    $("#fullShoppingBag").find('a').prop('href', shoppingCartURLLink);
                    $("#cartItemCount").find('img').remove();
                    $("#cartItemCount").html('<a href="' + shoppingCartURLLink + '">'+ '[ ' + count + ' ]</a>');
                }
            }
            else {
                $("#cartItemCount").html('');
                $("#imgarrow").hide();
                $("#fullShoppingBag").hide();
                $("#emptyShoppingBag").show();
                if (windowWidth > 480) {
                    $("#cartItemCount").html("<span>" + getLocale(AspxShoppingBagHeader, "No item") + "</span>");
                }
            }
        },

        showShoppingCart: function () {
            if (!$("#ShoppingCartPopUp").is(":visible")) {
                ShopingBag.GetCartItemListDetails();
            }
            var obj = $('#lnkshoppingcart');
            var pos = findPos(obj);
            $('#ShoppingCartPopUp').css('left', pos[0] - 180);
            $('#ShoppingCartPopUp').css('top', pos[1] + 20);
            $('#ShoppingCartPopUp').slideToggle("slow");

            if ($('#imgarrow').attr("src").indexOf("arrow_up.png") > -1) {
                $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_down.png");

            } else {
                $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_up.png");
            }
            return false;

        },
        showShoppingCartForPopup: function () {
            ShopingBag.GetCartItemListDetails();
            return false;
        },

        GetCartItemListDetails: function () {        
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.method = "AspxCoreHandler.ashx/GetCartDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ShopingBag.BindCartDetailsInShoppingBag;
                       this.ajaxCall(this.config);

        },


        BindCartItemslist: function (item, index) {          
            var total = parseFloat(item.TotalItemCost).toFixed(2);
            var price = parseFloat(item.Price).toFixed(2);        
            if (item.CostVariants != '') {
                $('#tblListCartItems').append('<tr><td><a  costvariants="' + item.CostVariants + '" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + pageExtension + '?varId=' + item.CostVariantsValueIDs + '">' + item.ItemName + ' (' + item.CostVariants + ')' + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + total + '</span></td><td><a class="imgCartItemDelete" name="' + item.CartItemID + '" ItemID="' + item.ItemID + '" id="btnDelete_' + item.CartID + '"><i class="i-delete"></i></a></td></tr>');
            }
            else {
                $('#tblListCartItems').append('<tr><td><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + pageExtension + '">' + item.ItemName + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + total + '</span></td><td><a class="imgCartItemDelete" sku="' + item.SKU + '"name="' + item.CartItemID + '" ItemID="' + item.ItemID + '" id="btnDelete_' + item.CartID + '"><i class="i-delete"></i></a></td></tr>');
            }
        },

        DeleteCartItemByID: function (id, cartItemId, event) {
            if (event) {
                var param = JSON2.stringify({ cartID: id, cartItemID: cartItemId, aspxCommonObj: aspxCommonObj });
                this.config.method = "AspxCoreHandler.ashx/DeleteCartItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = ShopingBag.BindShoppingBagOnDelete;
                               this.ajaxCall(this.config);
            }
            return false;
        },

        vars: {
            totalPrice: ""
        },

        GetTotalCartItemPrice: function () {
            this.config.method = "AspxCoreHandler.ashx/GetTotalCartItemPrice";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = ShopingBag.SetTotalCartItemPrice;
            this.config.async = false;
            this.ajaxCall(this.config);
            return ShopingBag.vars.totalPrice;
        }
    }

    ShopingBag.init();

});