
var WishItemAPI = function () {
    var buttonTemplate = '<label class="i-wishlist cssWishListLabel cssClassDarkBtn"><button type="button" id={ID} onclick="WishItemAPI.CheckWishListUniqueness(${Params})" ><span>' + getLocale(AspxApiJs, "WishList +") + '</span></button></label>';

    var itemDeatilButtonTemplate = '<label class="i-wishlist cssWishListLabel cssClassDarkBtn"><button type="button" id={ID} onclick="WishItemAPI.AddWishItemFromDetailPage(${Params})" ><span>' + getLocale(AspxApiJs, "WishList +") + '</span></button></label>';

    var AspxCommonObj = function () {
        var aspxCommonObj = {
            StoreID: storeID,
            PortalID: portalID,
            CultureName: cultureName,
            UserName: userName,
            CustomerID: customerID,
            SessionCode: sessionCode
        };
        return aspxCommonObj;
    };
    var $ajaxCall = function (async, method, param, successFx, error) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: async,
            url: aspxRootPath + 'Modules/AspxCommerce/AspxWishList/Handler.ashx/' + method,
            data: param,
            dataType: "json",
            success: successFx,
            error: error
        });
    };
    var wishListURL = "";
    var costVariantValueIDs = "";
    var count = 0;
    var wishItemCount = function () {
        $ajaxCall(true, "CountWishItems", param, function (msg) {
            count = msg.d;
            return count;
        }, function () { });
    };
    var getCount = function () {
        var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
        $ajaxCall(true, "CountWishItems", param, function (msg) {
            count = msg.d;
            if (!$("input:hidden[name='itemwishmenu']").attr('done')) {
                //getCount();               
                $('<a href="' + aspxRedirectPath + setting.WishListPage + pageExtension + '" id="lnkMyWishlist"><i class="i-mywishlist"></i>' + getLocale(AspxApiJs, "My Wishlist") + ' <span class="cssClassTotalCount">[' + count + ']</span></a>').insertAfter($("input:hidden[name='itemwishmenu']"));
                $("input:hidden[name='itemwishmenu']").attr('done', 1);

            } else if ($("input:hidden[name='dashitemwishmenu']").length > 0) {
                if (!$("input:hidden[name='dashitemwishmenu']").attr('done') == "1") {

                    $('<li id="dashMyWishlist">' + getLocale(AspxApiJs, "Your Wishlist Contains:") + '<a href="' + aspxRedirectPath + setting.WishListPage + pageExtension + '"> <span class="cssClassTotalCount">' + count + ' ' + 'items</span></a>').insertAfter($("input:hidden[name='dashitemwishmenu']"));
                    $("input:hidden[name='dashitemwishmenu']").attr('done', 1);
                }
            }
            else {
                $("#lnkMyWishlist").html(getLocale(AspxApiJs, "My Wishlist") + " <span class=\"cssClassTotalCount\">[" + msg.d + "]</span>");
                var myWishlistLink = '';
                var loginLink = '';
                if (IsUseFriendlyUrls) {
                    myWishlistLink = setting.WishListPage + pageExtension;
                    loginLink = LogInURL + pageExtension + '?ReturnUrl=' + AspxCommerce.utils.GetAspxRedirectPath() + myWishlistLink;
                } else {
                    myWishlistLink = setting.WishListPage;
                    loginLink = LogInURL;
                }
                if (AspxCommonObj().CustomerID > 0 && AspxCommonObj().UserName.toLowerCase() != 'anonymoususer') {
                    $("#lnkMyWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myWishlistLink + '');
                } else {
                    $("#lnkMyWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + loginLink + '');
                }
            }
        }, function () {

        });
    };


    var CheckWishListUniqueness = function (itemID, itemSKU, elem) {
        if (AspxCommonObj().CustomerID == 0 && userName.toLowerCase() == "anonymoususer") {
            AspxCommerce.RootFunction.Login();
            return false;
        }
        if (Boolean.parse($.trim($(elem).offsetParent().find(".cssClassProductsBoxInfo").attr("costvariantItem")))) {
            AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
        } else {
            var checkparam = { ID: itemID, aspxCommonObj: AspxCommonObj(), costVariantValueIDs: costVariantValueIDs };
            var checkdata = JSON2.stringify(checkparam);

            $ajaxCall(true, "CheckWishItems", checkdata, function (data) {
                if (data.d) {
                    csscody.alert('<h2>' + getLocale(AspxApiJs, 'Information Alert') + '</h2><p>' + getLocale(AspxApiJs, 'The selected item already exist in your wishlist.') + '</p>');
                } else {
                    AddToWishListFromJS(itemID, AspxCommerce.utils.GetClientIP(), AspxCommerce.utils.GetAspxClientCoutry(), "");
                }
            }, function () {

            });
        }
    };

    var AddWishItemFromDetailPage = function (itemID, itemSKU, elem) {
        var itemCostVariantIDs = [];
        if ($('#divCostVariant').is(':empty')) {
            //itemCostVariantIDs = '0';
            itemCostVariantIDs.push(0);
            //global item id at itemdetail page
            addtoWishList(itemID, '', itemSKU);
        } else {
            $("#divCostVariant select option:selected").each(function () {
                if ($(this).val() != 0) {
                    itemCostVariantIDs.push($(this).val());
                } else {
                }
            });
            $("#divCostVariant input[type=radio]:checked").each(function () {
                if ($(this).val() != 0) {
                    itemCostVariantIDs.push($(this).val());
                } else {
                }
            });

            $("#divCostVariant input[type=checkbox]:checked").each(function () {
                if ($(this).val() != 0) {
                    itemCostVariantIDs.push($(this).val());
                } else {
                }
            });

            if (ItemDetail.info.IsCombinationMatched) {
                addtoWishList(itemID, itemCostVariantIDs.join('@'), itemSKU);
            } else {
                csscody.alert('<h2>' + getLocale(AspxApiJs, 'Information Alert') + '</h2><p>' + getLocale(AspxApiJs, 'Please choose available variants!') + '</p>');
            }
        }
    };

    var addtoWishList = function (itemID, costVariantValueIDs, itemSKU) {
        var param = JSON.stringify({});
        if (AspxCommerce.utils.GetUserName().toLowerCase() != 'anonymoususer') {
            if (costVariantValueIDs == 'yes') {
                AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
            } else {
                var aspxCommonInfo = AspxCommonObj();
                delete aspxCommonInfo.CultureName;
                delete aspxCommonInfo.SessionCode;
                delete aspxCommonInfo.CutomerID;
                var checkparam = { ID: itemID, costVariantValueIDs: costVariantValueIDs, aspxCommonObj: aspxCommonInfo };
                var checkdata = JSON2.stringify(checkparam);
                $ajaxCall(true, "CheckWishItems", checkdata, function (msg) {
                    if (msg.d) {
                        csscody.alert('<h2>' + getLocale(AspxApiJs, "Information Alert") + '</h2><p>' + getLocale(AspxApiJs, "The selected item already exist in wishlist.") + '</p>');
                    } else {
                        AddToWishListFromJS(itemID, AspxCommerce.utils.GetClientIP(), AspxCommerce.utils.GetAspxClientCoutry(), costVariantValueIDs);
                    }

                }, function (msg) {
                    csscody.error('<h2>' + getLocale(AspxApiJs, "Error Message") + '</h2><p>' + getLocale(AspxApiJs, "Failed to add item in wishlist!") + '</p>');

                });
            }
        }
        else {
            AspxCommerce.RootFunction.Login(window.location.href);
        }

    };

    var AddToWishListFromJS = function (itemID, ip, countryName, costVariantValueIDs) {
        var aspxCommonInfo = AspxCommonObj();
        delete aspxCommonInfo.CultureName;
        delete aspxCommonInfo.SessionCode;
        delete aspxCommonInfo.CutomerID;
        var saveWishList = {
            ItemID: itemID,
            CostVariantValueIDs: costVariantValueIDs,
            IP: ip,
            CountryName: countryName
        };
        var addparam = { saveWishListInfo: saveWishList, aspxCommonObj: aspxCommonInfo };
        var adddata = JSON2.stringify(addparam);

        $ajaxCall(true, "SaveWishItems", adddata, function (data) {
            if ($('#lnkMyWishlist').length > 0) {
                //IncreaseWishListCount(); // for header counter increase                            
                // HeaderControl.GetWishListCount(); // for header wish counter increase for database
                getCount();
            }
            if ($('#divRecentlyAddedWishList').length > 0) {
                getMyWishList(); //for wishlist item in rightside
            }
            csscody.info('<h2>' + getLocale(AspxApiJs, "Successful Message") + '</h2><p>' + getLocale(AspxApiJs, "Item has been successfully added to wishlist.") + '</p>');

        }, function () {
            csscody.error('<h2>' + getLocale(AspxApiJs, "Error Message") + '</h2><p>' + getLocale(AspxApiJs, "Failed to add item in wishlist!") + '</p>');
        });
    };

    var IncreaseWishListCount = function () {
        var wishListCount = $('#lnkMyWishlist span').html().replace(/[^0-9]/gi, '');
        wishListCount = parseInt(wishListCount) + 1;
        $('.cssClassLoginStatusInfo ul li a#lnkMyWishlist span').html(" [" + wishListCount + "]");
    };

    var setting = {
        WishListPage: ""
    };
    var getSetting = function () {
        var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
        $ajaxCall(false, "GetWishItemsSetting", param, function (msg) {
            setting.WishListPage = fixedEncodeURIComponent(msg.d.WishListPageName);
        }, function () { });
    };

    var getMyWishList = function () {
        wishItemsFront.BindWishList();
    }

    var enableWishList = function (runMenu) {
        try {
            //FOR MENU
            if (runMenu) {
                if ($("input:hidden[name='itemwishmenu']").length > 0) {
                    if (!$("input:hidden[name='itemwishmenu']").attr('done')) {
                        getCount();
                    }
                }              
            }
            //FOR LATEST ITEM TEMPLATE itemDetailWish
            var template = buttonTemplate;
            $.template("WishItemButton", template);
            $("input:hidden[name='itemwish']").each(function (index, item) {
                var value = $(this).val();
                if (value != "") {
                    if (!$(this).attr('done')) {

                        $(this).attr('done', 1);
                        var itemid = value.split(',')[0];
                        var param = { Params: value, ID: 'wishitem-' + index };
                        $.tmpl("WishItemButton", param).insertAfter($(this));
                    }
                }
            });

            //for item detail page
            var template = itemDeatilButtonTemplate;
            $.template("WishItemButton", template);
            $("input:hidden[name='itemDetailWish']").each(function (index, item) {
                var value = $(this).val();
                if (value != "") {
                    if (!$(this).attr('done')) {

                        $(this).attr('done', 1);
                        var itemid = value.split(',')[0];
                        var param = { Params: value, ID: 'wishitem-' + index };
                        $.tmpl("WishItemButton", param).insertAfter($(this));
                    }
                }
            });
        } catch (e) {

        }
    };

    var init = function () {
       // getSetting();
      //  enableWishList(true);
        //$(document).bind('DOMNodeInserted', function (event) {
        //    enableWishList(false); //
        //});

        //ITEM DETAIL PAGE
        if ($("#addWishListThis") != null) {
            // $.template("WishItemButton", template);
            $("input:hidden[name='itemDetailWish']").each(function (index, item) {
                var value = $(this).val();
                if (value != "") {
                    if (!$(this).attr('done')) {
                        $(this).attr('done', 1);
                        var itemid = value.split(',')[0];
                        var param = { Params: value, ID: 'wishitem-' + index };
                        $.tmpl("WishItemButton", param).insertAfter($(this));
                    }
                }
            });

            $("#addWishListThis").off().on("click", function () {

                var itemCostVariantIDs = [];
                if ($('#divCostVariant').is(':empty')) {
                    //itemCostVariantIDs = '0';
                    itemCostVariantIDs.push(0);
                    //global item id at itemdetail page
                    addtoWishList(itemID, '', $('#spanSKU').html());
                } else {
                    $("#divCostVariant select option:selected").each(function () {
                        if ($(this).val() != 0) {
                            itemCostVariantIDs.push($(this).val());
                        } else {
                        }
                    });
                    $("#divCostVariant input[type=radio]:checked").each(function () {
                        if ($(this).val() != 0) {
                            itemCostVariantIDs.push($(this).val());
                        } else {
                        }
                    });

                    $("#divCostVariant input[type=checkbox]:checked").each(function () {
                        if ($(this).val() != 0) {
                            itemCostVariantIDs.push($(this).val());
                        } else {
                        }
                    });

                    if (info.IsCombinationMatched) {
                        addtoWishList(itemId, itemCostVariantIDs.join('@'), $('#spanSKU').html());
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxApiJs, 'Information Alert') + '</h2><p>' + getLocale(AspxApiJs, 'Please choose available variants!') + '</p>');
                    }
                }
            });
        }
    };
    init();
    window.onload = function () {
        // init();
        // enableWishList(true);      
    }
    return {
        Init: init,
        Count: getCount,
        Add: addtoWishList,
        CheckWishListUniqueness: CheckWishListUniqueness,
        AddWishItemFromDetailPage: AddWishItemFromDetailPage,
        WihsItemCount: wishItemCount
    };
} ();