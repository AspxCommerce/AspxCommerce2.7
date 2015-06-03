//<![CDATA[

var AspxCommerce = {};
$(function () {
    AspxCommerce = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: "",
            ajaxCallMode: 0

        },

        vars: {
            IsAlive: true
        },

        ajaxCall: function (config) {
            $.ajax({
                type: AspxCommerce.config.type,
                contentType: AspxCommerce.config.contentType,
                cache: AspxCommerce.config.cache,
                async: AspxCommerce.config.async,
                url: AspxCommerce.config.url,
                data: AspxCommerce.config.data,
                dataType: AspxCommerce.config.dataType,
                success: AspxCommerce.ajaxSuccess,
                error: AspxCommerce.ajaxFailure
            });
        },

        utils: {
            GetStoreID: function () {
                return SageFramePortalID;
            },
            GetPortalID: function () {
                return SageFramePortalID;
            },
            GetUserName: function () {
                return SageFrameUserName;
            },
            GetCultureName: function () {
                return SageFrameCurrentCulture;
            },
            GetCustomerID: function () {
                return customerID;
            },
            GetTemplateName: function () {
                return templateName;
            },
            IsUserFriendlyUrl: function () {
                return Boolean.parse(IsUseFriendlyUrls);
            },
            GetSessionCode: function () {
                return sessionCode;
            },
            GetClientIP: function () {
                return clientIPAddress;
            },
            GetAspxServicePath: function () {
                return aspxservicePath;
            },
            GetAspxRedirectPath: function () {
                return aspxRedirectPath;
            },
            GetAspxRootPath: function () {
                return aspxRootPath;
            },
            GetAspxTemplateFolderPath: function () {
                return aspxTemplateFolderPath;
            }
            ,
            GetAspxClientCoutry: function () {
                return aspxCountryName;
            }
        },
        AspxCommonObj: function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                SessionCode: AspxCommerce.utils.GetSessionCode(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonInfo;
        },
        CheckSessionActive: function (aspxCommonObj) {
            AspxCommerce.config.url = AspxCommerce.config.baseURL + "AspxCommonHandler.ashx/CheckSessionActive";
            AspxCommerce.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            AspxCommerce.config.ajaxCallMode = 1;
            AspxCommerce.ajaxCall(AspxCommerce.config);
        },
        GetUrlVars: function () {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        },
        RootFunction: {
            BindRecentlyComparedItem: function (response, index) {
                var RecentlyComparedItems = '';
                if (index % 2 === 0) {
                    RecentlyComparedItems = '<tr class="sfEven"><td><a href="' + aspxRedirectPath + 'item/' + response.SKU + pageExtension + '">' + response.ItemName + '</a></td></tr>';
                }
                else {
                    RecentlyComparedItems = '<tr class="sfOdd"><td><a href="' + aspxRedirectPath + 'item/' + response.SKU + pageExtension + '">' + response.ItemName + '</a></td></tr>';
                }
                $("#tblRecentlyComparedItemList>tbody").append(RecentlyComparedItems);
            },

            Login: function (returnUrl) {
                returnUrl = window.location.href;
                window.location.href = AspxCommerce.utils.GetAspxRootPath() + LogInURL + pageExtension + '?' + "ReturnUrl=" + returnUrl;
                return false;
            },
            //region wish Items

            CheckWishListUniqueness: function (itemID, itemSKU, elem) {
                debugger;
                if (AspxCommerce.AspxCommonObj().CustomerID == 0 && AspxCommerce.utils.GetUserName().toLowerCase() == "anonymoususer") {
                    AspxCommerce.RootFunction.Login();
                    return false;
                }
                if (Boolean.parse($.trim($(elem).offsetParent().find(".cssClassProductsBoxInfo").attr("costvariantItem")))) {
                    AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                } else {
                    var costVariantValueIDs = "";
                    var checkparam = { ID: itemID, aspxCommonObj: AspxCommerce.AspxCommonObj(), costVariantValueIDs: costVariantValueIDs };
                    var checkdata = JSON2.stringify(checkparam);
                    $.ajax({
                        type: "POST",
                        url: aspxRootPath + 'Modules/AspxCommerce/AspxWishList/Handler.ashx/CheckWishItems',
                        data: checkdata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d) {
                                csscody.alert('<h2>' + getLocale(CoreJsLanguage, "Information Alert") + '</h2><p>' + getLocale(CoreJsLanguage, "The selected item already exist in wishlist.") + '</p>');
                            } else {
                                AspxCommerce.RootFunction.AddToWishListFromJS(itemID, AspxCommerce.utils.GetClientIP(), AspxCommerce.utils.GetAspxClientCoutry(), costVariantValueIDs);
                            }
                        },
                        error: function (msg) {
                            csscody.error('<h2>' + getLocale(CoreJsLanguage, "Error Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Failed to add item in wishlist!") + '</p>');
                        }
                    });
                }
            },

             AddWishItemFromDetailPage : function (itemID, itemSKU, elem) {
                var itemCostVariantIDs = [];
                if ($('#divCostVariant').is(':empty')) {
                    //itemCostVariantIDs = '0';
                    itemCostVariantIDs.push(0);
                    //global item id at itemdetail page
                    AspxCommerce.RootFunction.AddToWishList(itemID, '', itemSKU);
                } else {
                    $("#divCostVariant select option:selected").each(function () {
                        if ($(this).val() !== 0) {
                            itemCostVariantIDs.push($(this).val());
                        } 
                    });
                    $("#divCostVariant input[type=radio]:checked").each(function () {
                        if ($(this).val() !== 0) {
                            itemCostVariantIDs.push($(this).val());
                        } 
                    });

                    $("#divCostVariant input[type=checkbox]:checked").each(function () {
                        if ($(this).val() !== 0) {
                            itemCostVariantIDs.push($(this).val());
                        }
                    });

                    if (ItemDetail.info.IsCombinationMatched) {
                        AspxCommerce.RootFunction.AddToWishList(itemID, itemCostVariantIDs.join('@'), itemSKU);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxApiJs, 'Information Alert') + '</h2><p>' + getLocale(AspxApiJs, 'Please choose available variants!') + '</p>');
                    }
                }
            },

            AddToWishList: function(itemID, costVariantValueIDs, itemSKU) {
                if (AspxCommerce.utils.GetUserName().toLowerCase() != 'anonymoususer') {
                    if (costVariantValueIDs == 'yes') {
                        AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                    } else {
                        var aspxCommonInfo = AspxCommerce.AspxCommonObj();
                        delete aspxCommonInfo.CultureName;
                        delete aspxCommonInfo.SessionCode;
                        delete aspxCommonInfo.CutomerID;
                        var checkparam = { ID: itemID, costVariantValueIDs: costVariantValueIDs, aspxCommonObj: aspxCommonInfo };
                        var checkdata = JSON2.stringify(checkparam);
                        $.ajax({
                            type: "POST",
                            url: aspxRootPath + 'Modules/AspxCommerce/AspxWishList/Handler.ashx/CheckWishItems',
                            data: checkdata,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function(msg) {
                                if (msg.d) {
                                    csscody.alert('<h2>' + getLocale(CoreJsLanguage, "Information Alert") + '</h2><p>' + getLocale(CoreJsLanguage, "The selected item already exist in wishlist.") + '</p>');
                                } else {
                                    AspxCommerce.RootFunction.AddToWishListFromJS(itemID, AspxCommerce.utils.GetClientIP(), AspxCommerce.utils.GetAspxClientCoutry(), costVariantValueIDs);
                                }
                            },
                            error: function(msg) {
                                csscody.error('<h2>' + getLocale(CoreJsLanguage, "Error Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Failed to add item in wishlist!") + '</p>');
                            }
                        });
                    }
                }
                else {
                    AspxCommerce.RootFunction.Login(window.location.href);
                }
            },

            AddToWishListFromJS: function(itemID, ip, countryName, costVariantValueIDs) {
                var aspxCommonInfo = AspxCommerce.AspxCommonObj();
                delete aspxCommonInfo.CultureName;
                delete aspxCommonInfo.SessionCode;
                delete aspxCommonInfo.CutomerID;
                var saveWishList = {
                    ItemID: itemID,
                    CostVariantValueIDs: costVariantValueIDs,
                    IP: ip,
                    CountryName: countryName
                };
                //   var addparam = { ID: itemID, IP: ip, countryName: countryName, costVariantValueIDs: costVariantValueIDs, aspxCommonObj: aspxCommonInfo };
                var addparam = { saveWishListInfo: saveWishList, aspxCommonObj: aspxCommonInfo };
                var adddata = JSON2.stringify(addparam);
                $.ajax({
                    type: "POST",
                    url: aspxRootPath + 'Modules/AspxCommerce/AspxWishList/Handler.ashx/SaveWishItems',
                    data: adddata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        //MyWishList();
                        if ($('#lnkMyWishlist').length > 0) {
                            AspxCommerce.RootFunction.IncreaseWishListCount(); // for header counter increase                            
                            //HeaderControl.GetWishListCount(); // for header wish counter increase for database
                        }
                        if ($('#divRecentlyAddedWishList').length > 0) {
                            WishItems.BindMyWishList(); //for wishlist item in rightside
                        }
                        csscody.info('<h2>' + getLocale(CoreJsLanguage, "Successful Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Item has been successfully added to wishlist.") + '</p>');
                    },
                    error: function(msg) {
                        csscody.error('<h2>' + getLocale(CoreJsLanguage, "Error Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Failed to add item in wishlist!") + '</p>');
                    }
                });
            },

            IncreaseWishListCount: function() {
                var wishListCount = $('#lnkMyWishlist span').html().replace(/[^0-9]/gi, '');
                wishListCount = parseInt(wishListCount) + 1;
                $('.cssClassLoginStatusInfo ul li a#lnkMyWishlist span').html(" [" + wishListCount + "]");
            },
//region wish items end

            AddToCartToJSFromTemplate: function (itemId, itemPrice, itemSKU, itemQuantity) {
                AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity);
            },
            AddToCartFromJS: function (itemId, itemPrice, itemSKU, itemQuantity, isCostVariant, elem) {
                var $btnWraper = $(elem).parents("div[class='sfButtonwrapper']");
                var itemTypeID = parseInt($btnWraper.data().itemtypeid);
                if (isCostVariant.toLowerCase() == "true") {
                    AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                }
                else if (itemTypeID == 6) {
                    AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                }
                else if (itemTypeID == 5) {
                    AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                }
                else {
                    var param = { itemID: itemId, itemPrice: itemPrice, itemQuantity: itemQuantity, aspxCommonObj: AspxCommerce.AspxCommonObj() };
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
                                if (AspxCommerce.IsModuleInstalled('AspxABTesting')) {
                                    ABTest.ABTestVisitOnPreviousPage(myCartURL); //Path---~/Modules/AspxCommerce/AspxABTesting/js/ABTest.js---//
                                } else {
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartUrl;
                                }

                            }
                        }
                    };
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCommonHandler.ashx/AddItemstoCart",
                        data: data,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 1) {
                                AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                            } else if (msg.d == 2) {
                                //out of stock
                                try {
                                    var realTimeEvent = $.connection._aspxrthub;
                                    realTimeEvent.server.checkIfItemOutOfStock(itemId, itemSKU, "", AspxCommerce.AspxCommonObj());
                                }
                                catch (Exception) {
                                    var param = { itemID: itemId, costVariantsValueIDs: '', aspxCommonObj: AspxCommerce.AspxCommonObj() };
                                    var data = JSON2.stringify(param);
                                    $.ajax({
                                        type: "POST",
                                        url: aspxservicePath + "AspxCommonHandler.ashx/CheckItemOutOfStock",
                                        data: data,
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data) {
                                            if (data.d) {
                                                var id = $(elem).attr("addtocart");
                                                $("button[addtocart=" + id + "]").each(function () {
                                                    var $parent = $(this).parents('.sfButtonwrapper');
                                                    $parent.addClass("cssClassOutOfStock");
                                                    $parent.html('');
                                                    $parent.append("<button type='button'><span>Out Of Stock</span></button>");
                                                });
                                            }
                                        }
                                    });
                                }
                                //csscody.alert('<h2>' + getLocale(CoreJsLanguage, "Information Alert") + '</h2><p>' + getLocale(CoreJsLanguage, "This product is currently out of stock!.") + '</p>');
                            } else if (msg.d == 3) {
                                //out of stock
                                AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                            } else {

                                try {
                                    var realTimeEvent = $.connection._aspxrthub;
                                    realTimeEvent.server.checkIfItemOutOfStock(itemId, itemSKU, "", AspxCommerce.AspxCommonObj());
                                }
                                catch (Exception) {
                                    var param = { itemID: itemId, costVariantsValueIDs: '', aspxCommonObj: AspxCommerce.AspxCommonObj() };
                                    var data = JSON2.stringify(param);
                                    $.ajax({
                                        type: "POST",
                                        url: aspxservicePath + "AspxCommonHandler.ashx/CheckItemOutOfStock",
                                        data: data,
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (msg) {
                                            if (msg.d) {
                                                var id = $(elem).attr("addtocart");
                                                $("button[addtocart=" + id + "]").each(function () {
                                                    var $parent = $(this).$(this).parents('.sfButtonwrapper');
                                                    $parent.addClass("cssClassOutOfStock");
                                                    $parent.html('');
                                                    $parent.append("<button type='button'><span>Out Of Stock</span></button>");
                                                });
                                            }
                                        }
                                    });
                                }

                                if ($("#divMiniShoppingCart1").offset() != null) {
                                    $("#CartItemLoader").html('<img src="./Modules/ShoppingCart/image/loader.gif">');
                                    var basketX = '';
                                    var basketY = '';
                                    var Itemid = "productImageWrapID_" + itemId;
                                    var productIDValSplitter = $((Itemid).split("_"));
                                    var productIDVal = productIDValSplitter[1];
                                    var productX = $("#productImageWrapID_" + productIDVal).offset().left;
                                    var productY = $("#productImageWrapID_" + productIDVal).offset().top;
                                    if ($("#productID_" + productIDVal).length > 0) {
                                        basketX = $("#productID_" + productIDVal).offset().left;
                                        basketY = $("#productID_" + productIDVal).offset().top;
                                    } else {
                                        basketX = $("#divMiniShoppingCart1").offset().left;
                                        basketY = $("#divMiniShoppingCart1").offset().top;
                                    }
                                    var gotoX = basketX - productX;
                                    var gotoY = basketY - productY;

                                    var newImageWidth = $("#productImageWrapID_" + productIDVal).width() / 5;
                                    var newImageHeight = $("#productImageWrapID_" + productIDVal).height() / 5;

                                    $("#productImageWrapID_" + productIDVal + " img")
                                    .clone()
                                    .prependTo("#productImageWrapID_" + productIDVal)
                                    .css({ 'position': 'absolute' })
                                    .animate({ opacity: 0.4 }, 50)
                                    .animate({
                                        opacity: 0.4,
                                        marginLeft: gotoX,
                                        marginTop: gotoY,
                                        width: newImageWidth,
                                        height: newImageHeight
                                    }, 1500, function () {
                                        $(this).remove();

                                        //TODO:: Add jQuery Counter increament HERE :: done
                                        //  IncreaseMyCartItemCount(); //for header cart count
                                        HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                                        if ($("#cartItemCount").offset() !== null) {
                                            ShopingBag.GetCartItemCount(); //for shopping bag counter from database
                                            //  IncreaseShoppingBagCount(); // for shopping bag counter from static
                                            // ShopingBag.GetCartItemListDetails(); //for details in shopping bag
                                        }

                                        if ($("#divMiniShoppingCart1").offset() !== null) {
                                            ShoppingCartFlyOver.GetCartItemCount();
                                            ShoppingCartFlyOver.GetCartItemListDetails();
                                        }
                                        if ($("#divCartDetails").length > 0) {
                                            AspxCart.GetUserCartDetails(); //for binding mycart's tblCartList
                                        }
                                        if ($("#productID_" + productIDVal).length > 0) {
                                            $("#productID_" + productIDVal).animate({ opacity: 0 }, 100);
                                            $("#productID_" + productIDVal).animate({ opacity: 0 }, 100);
                                            $("#productID_" + productIDVal).animate({ opacity: 1 }, 100);
                                            $("#CartItemLoader").empty();

                                        } else {
                                            $("#tblCartListItems tr:last").hide();
                                            $("#tblCartListItems tr:last").show("slow");
                                            $("#CartItemLoader").empty();
                                        }
                                    });
                                    csscody.addToCart('<h2>' + getLocale(CoreJsLanguage, "Successful Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Item has been successfully added to cart.") + '</p>', addToCartProperties);
                                } else {
                                    csscody.addToCart('<h2>' + getLocale(CoreJsLanguage, "Successful Message") + '</h2><p>' + getLocale(CoreJsLanguage, "Item has been successfully added to cart.") + '</p>', addToCartProperties);
                                    //TODO:: Add jQuery Counter increament HERE :: done
                                    //  IncreaseMyCartItemCount(); //for header cart count
                                    HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                                    ShopingBag.GetCartItemCount(); //for shopping bag counter from database
                                    //  IncreaseShoppingBagCount(); // for shopping bag counter from static
                                    //ShopingBag.GetCartItemListDetails(); //for details in shopping bag
                                    if ($("#divCartDetails").length > 0) {
                                        AspxCart.GetUserCartDetails(); //for binding mycart's tblCartList
                                    }
                                }
                            }
                        }
                    });
                }
            },

            RedirectToItemDetails: function (itemSKU) {
                window.location.href = aspxRedirectPath + 'item/' + itemSKU + pageExtension;
                return false;
            }

        },

        ajaxSuccess: function (data) {
            switch (AspxCommerce.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    AspxCommerce.vars.IsAlive = data.d;
                    break;
                default:
                    break;
            }
        },

        IsModuleInstalled: function (moduleName) {
            var isInstalled = false;
            $.ajax({
                type: "POST",
                url: AspxCommerce.utils.GetAspxServicePath() + "AspxCommonHandler.ashx/" + "GetModuleInstallationInfo",
                data: JSON2.stringify({ moduleFriendlyName: moduleName, aspxCommonObj: AspxCommerce.AspxCommonObj() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (response.d) {
                        isInstalled = true;
                    }
                },
                error: function () {
                    //csscody.error('<h2>' + getLocale(CoreJsLanguage, 'Error Message') + '</h2><p>' + getLocale(CoreJsLanguage, 'Failed to load module installation information!.') + '</p>');
                }
            });
            return isInstalled;
        },
        init: function () {

            //  $('body').append('<div id="ajaxBusy"><div id="dialog" style="background-color:#AAAAAA; position:absolute;left:50%;top:50%;display:none;z-index:9999;" >Please Wait...<br /><img src="' + AspxCommerce.utils.GetAspxRootPath() + 'Templates/Default/images/progress_bar.gif" alt="" title="Loading"/></div><div id="mask" style=" position:absolute;left:0;top:0;z-index:9000;background-color:#000;display:none;"></div></div>');


        }
    };
    AspxCommerce.init();

});

//]]>

function fixedEncodeURIComponent(str) {
    // return encodeURIComponent(str).replace( /!/g , '%21').replace( /'/g , '%27').replace( /\(/g , '%28').replace( /\)/g , '%29').replace( /-/g , '_').replace( /\*/g , '%2A').replace( /%26/g , 'ampersand').replace( /%20/g , '-');

    var Results = encodeURIComponent(str);
    // Results = Results.Replace("%", "%25");
    Results = Results.replace("!", "%21");
    Results = Results.replace("'", "%27");
    Results = Results.replace("(", "%28");
    Results = Results.replace(")", "%29");
    Results = Results.replace("*", "%2A");
    Results = Results.replace("<", "%3C");
    Results = Results.replace(">", "%3E");
    Results = Results.replace("#", "%23");
    Results = Results.replace("{", "%7B");
    Results = Results.replace("}", "%7D");
    Results = Results.replace("|", "%7C");
    Results = Results.replace("\"", "%5C");
    Results = Results.replace("^", "%5E");
    Results = Results.replace("~", "%7E");
    Results = Results.replace("[", "%5B");
    Results = Results.replace("]", "%5D");
    Results = Results.replace("`", "%60");
    Results = Results.replace(";", "%3B");
    Results = Results.replace("/", "%2F");
    Results = Results.replace("?", "%3F");
    Results = Results.replace(":", "%3A");
    Results = Results.replace("@", "%40");
    Results = Results.replace("=", "%3D");
    Results = Results.replace("&", "%26");
    Results = Results.replace("%26", "ampersand");
    Results = Results.replace("$", "%24");
    Results = Results.replace(" ", "%20");
    return Results;
}

function fixedDecodeURIComponent(str) {
    var Results = str;
    // Results = Results.Replace("%25","%");
    Results = Results.replace("%21", "!");
    Results = Results.replace("%27", "'");
    Results = Results.replace("%28", "(");
    Results = Results.replace("%29", ")");
    Results = Results.replace("%2A", "*");
    Results = Results.replace("%3C", "<");
    Results = Results.replace("%3E", ">");
    Results = Results.replace("%23", "#");
    Results = Results.replace("%7B", "{");
    Results = Results.replace("%7D", "}");
    Results = Results.replace("%7C", "|");
    Results = Results.replace("%5C", "\"");
    Results = Results.replace("%5E", "^");
    Results = Results.replace("%7E", "~");
    Results = Results.replace("%5B", "[");
    Results = Results.replace("%5D", "]");
    Results = Results.replace("%60", "`");
    Results = Results.replace("%3B", ";");
    Results = Results.replace("%2F", "/");
    Results = Results.replace("%3F", "?");
    Results = Results.replace("%3A", ":");
    Results = Results.replace("%40", "@");
    Results = Results.replace("%3D", "=");
    Results = Results.replace("ampersand", "%26");
    Results = Results.replace("%26", "&");
    Results = Results.replace("%24", "$");
    Results = Results.replace("%20", " ");
    return Results;
}

////////////////////Added BY Niranjan ? Modified By Santosh//////////

function GetSystemLocale(text) {
    return SystemLocale[$.trim(text)] == undefined ? text : SystemLocale[$.trim(text)];
}

function getLocale(moduleKey, text) {
    return moduleKey[$.trim(text)] == undefined ? text : moduleKey[$.trim(text)];
}
$.fn.SystemLocalize = function () {

    return this.each(function () {

        var t = $(this);
        if (t.is("input:button")) {
            var text = t.attr("value");
            var localeValue = SystemLocale[$.trim(text)];
            t.attr("value", localeValue);
        }
        else {
            t.html(SystemLocale[$.trim(t.text())] == undefined ? $.trim(t.text()) : SystemLocale[$.trim(t.text())]);
        }
    });
}

$.fn.localize = function (p) {
    return this.each(function () {
        var t = $(this);
           t.html(p.moduleKey[t.html().replace(/^\s+|\s+$/g, "")] == undefined ? t.html().replace(/^\s+|\s+$/g, "") : p.moduleKey[t.html().replace(/^\s+|\s+$/g, "")]);
        if (t.is("input[type='button']")) {
            t.val(p.moduleKey[t.attr("value")] == undefined ? t.attr("value") : p.moduleKey[t.attr("value")]);
        }
    });
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */
/*  Base64 class: Base 64 encoding / decoding (c) Chris Veness 2002-2011                          */
/*    note: depends on Utf8 class                                                                 */
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */

var Base64 = {};  // Base64 namespace

Base64.code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

/**
 * Encode string into Base64, as defined by RFC 4648 [http://tools.ietf.org/html/rfc4648]
 * (instance method extending String object). As per RFC 4648, no newlines are added.
 *
 * @param {String} str The string to be encoded as base-64
 * @param {Boolean} [utf8encode=false] Flag to indicate whether str is Unicode string to be encoded 
 *   to UTF8 before conversion to base64; otherwise string is assumed to be 8-bit characters
 * @returns {String} Base64-encoded string
 */
Base64.encode = function (str, utf8encode) {  // http://tools.ietf.org/html/rfc4648
    utf8encode = (typeof utf8encode == 'undefined') ? false : utf8encode;
    var o1, o2, o3, bits, h1, h2, h3, h4, e = [], pad = '', c, plain, coded;
    var b64 = Base64.code;

    plain = utf8encode ? Utf8.encode(str) : str;

    c = plain.length % 3;  // pad string to length of multiple of 3
    if (c > 0) { while (c++ < 3) { pad += '='; plain += '\0'; } }
    // note: doing padding here saves us doing special-case packing for trailing 1 or 2 chars

    for (c = 0; c < plain.length; c += 3) {  // pack three octets into four hexets
        o1 = plain.charCodeAt(c);
        o2 = plain.charCodeAt(c + 1);
        o3 = plain.charCodeAt(c + 2);

        bits = o1 << 16 | o2 << 8 | o3;

        h1 = bits >> 18 & 0x3f;
        h2 = bits >> 12 & 0x3f;
        h3 = bits >> 6 & 0x3f;
        h4 = bits & 0x3f;

        // use hextets to index into code string
        e[c / 3] = b64.charAt(h1) + b64.charAt(h2) + b64.charAt(h3) + b64.charAt(h4);
    }
    coded = e.join('');  // join() is far faster than repeated string concatenation in IE

    // replace 'A's from padded nulls with '='s
    coded = coded.slice(0, coded.length - pad.length) + pad;

    return coded;
}

/**
 * Decode string from Base64, as defined by RFC 4648 [http://tools.ietf.org/html/rfc4648]
 * (instance method extending String object). As per RFC 4648, newlines are not catered for.
 *
 * @param {String} str The string to be decoded from base-64
 * @param {Boolean} [utf8decode=false] Flag to indicate whether str is Unicode string to be decoded 
 *   from UTF8 after conversion from base64
 * @returns {String} decoded string
 */
Base64.decode = function (str, utf8decode) {
    utf8decode = (typeof utf8decode == 'undefined') ? false : utf8decode;
    var o1, o2, o3, h1, h2, h3, h4, bits, d = [], plain, coded;
    var b64 = Base64.code;

    coded = utf8decode ? Utf8.decode(str) : str;

    for (var c = 0; c < coded.length; c += 4) {  // unpack four hexets into three octets
        h1 = b64.indexOf(coded.charAt(c));
        h2 = b64.indexOf(coded.charAt(c + 1));
        h3 = b64.indexOf(coded.charAt(c + 2));
        h4 = b64.indexOf(coded.charAt(c + 3));

        bits = h1 << 18 | h2 << 12 | h3 << 6 | h4;

        o1 = bits >>> 16 & 0xff;
        o2 = bits >>> 8 & 0xff;
        o3 = bits & 0xff;

        d[c / 4] = String.fromCharCode(o1, o2, o3);
        // check for padding
        if (h4 == 0x40) d[c / 4] = String.fromCharCode(o1, o2);
        if (h3 == 0x40) d[c / 4] = String.fromCharCode(o1);
    }
    plain = d.join('');  // join() is far faster than repeated string concatenation in IE

    return utf8decode ? Utf8.decode(plain) : plain;
}

//$(document).ready(function () {
//    if (document.URL.toLocaleLowerCase().indexOf("admin/") != -1) {
//        startLoader();

//        setTimeout("stopLoader()", 1000);

//        $(document).ajaxStart(function () {

//            if ($("#fade").length < 1) {
//                var $divappend;

//                if ($("#sfOuterwrapper").length > 0) {
//                    $divappend = $("#sfOuterwrapper");
//                }
//                else {
//                    $divappend = $("#lytA_sfOuterWrapper");

//                }

//                $divappend.prepend("<div id='fade'></div><div id='aspxcommereloading'><img id='loading' src='" + AspxCommerce.utils.GetAspxRootPath() + "images/aspx-loader2.gif' title='aspxcommerce loaging' alt='aspxcommerce loaging'></div>");
//                $("#fade").show();
//            }
//        });

//        $(document).ajaxComplete(function () {
//            $("#fade").remove();
//            $("#aspxcommereloading").remove();
//        });
//    }    
//});

function startLoader() {
        if ($("#fade").length < 1) {
            var $divappend;

            if ($("#sfOuterwrapper").length > 0) {
                $divappend = $("#sfOuterwrapper");
            }
            else {
                $divappend = $("#lytA_sfOuterWrapper");

            }
            $divappend.prepend("<div id='aspxcommereloading'>" + "<img id='loading' src='" + AspxCommerce.utils.GetAspxRootPath() + "images/aspx-loader2.gif' title='aspxcommerce loading' alt='aspxcommerce loading'>" + "</div><div id='fade'></div>");
            $("#fade").show();
        }
}

function stopLoader() {
    $("#fade").remove();
    $("#aspxcommereloading").remove();
}

/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */


function strEncrypt(value) {
    if (value !== null) {
        return Base64.encode(value.toString().trim());
    }
    else {
        return '';
    }
    
}

function strDecrypt(value) {
    var result = Base64.decode(value);
    if (result.toString().split(',').length > 0) {
        var tempResult = result.toString().split(',');
        var strI = '';
        for (var i = 0; i < tempResult.length; i++) {
            strI += tempResult[i];
        }
        result = parseInt(strI);
    }
    return result;
}
Boolean.parse = function (b) { var a = b.trim().toLowerCase(); if (a === "false") return false; if (a === "true") return true }