var ItemCompareDetailsAPI = "";
(function ($) {
    $.ItemCompareDetailsView = function (p) {
        p = $.extend({
            servicepath: '',
            NoImageItemComparePath:'',
            AllowAddToCart: '',
            AllowOutStockPurchase: '',
        }, p);
        var customerId, ip, countryName, sessionCode, servicePath, userName;
        var IDs = "";
        var costVar = "";
        var groupItems = [];
        customerId = AspxCommerce.utils.GetCustomerID();
        ip = AspxCommerce.utils.GetClientIP();
        countryName = AspxCommerce.utils.GetAspxClientCoutry();
        sessionCode = AspxCommerce.utils.GetSessionCode();
        servicePath = p.servicepath;
        userName = AspxCommerce.utils.GetUserName()
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                UserName: AspxCommerce.utils.GetUserName()
            };
            return aspxCommonInfo;
        };
        var GetCompareList = function (IDs, costVar) {
            $.ajax({
                type: "POST",
                url: p.servicepath + "GetCompareList",
                data: JSON2.stringify({ itemIDs: IDs, CostVariantValueIDs: costVar, aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var myIds = new Array();
                    var myAttributes = new Array();
                    $("#tblItemCompareList >tbody").html('');
                    $("#divItemCompareElements").html("");
                    $("#scriptStaticField").tmpl().appendTo("#divItemCompareElements");
                    Array.prototype.RemoveNA = function (arr, obj) {
                        var i = this.length;
                        while (i--) {
                            if (this[i].AttributeID === obj) {
                                arr.splice(i, 1);
                            }
                        }
                    };
                    Array.prototype.Count = function (obj) {
                        var i = this.length;
                        var cc = 0;
                        while (i--) {
                            if (this[i] === obj) {
                                cc++;
                            }
                        }
                        return cc;
                    };
                    var oldc;
                    var itemCount;
                    var emArr = [];
                    $.each(msg.d, function (index, value) {

                        if (index == 0) {
                            oldc = value.AttributeID; itemCount = 1;
                        }
                        if (index != 0 && value.AttributeID == oldc) {
                            itemCount++;
                        }
                        if (value.AttributeValue == "") {
                            emArr.push(value.AttributeID);
                        }
                    });
                    $.each(emArr, function (index, value) {

                        if (itemCount == emArr.Count(value)) {
                            msg.d.RemoveNA(msg.d, value);
                        }
                    });

                    $.each(msg.d, function (index, value) {
                        var cssClass = '';
                        var noAttValue = [];
                        cssClass = 'cssClassCompareAttributeClass';
                        var pattern = '"', re = new RegExp(pattern, "g");
                        if (value.InputTypeID == 7) {
                            if (value.AttributeValue != "") {
                                cssClass = 'cssClassFormatCurrency cssClassCompareAttributeClass';
                            }
                        }

                        if (jQuery.inArray(value.AttributeID, myAttributes) < 0) {
                            $("#tblItemCompareList >tbody").append('<tr id="trCompare_' + index + '"></tr>');
                            if (value.AttributeName == 'Variants') {
                                value.AttributeName = getLocale(AspxItemsCompare, "Variants");
                            }
                            $("#tblItemCompareList >tbody> tr:last").append('<td class="cssClassCompareAttributeClass"><span class="cssClassLabel">' + value.AttributeName + ': </span></td>');
                            var valz;
                            if (value.AttributeValue == "") {
                                valz = "n/a"; noAttValue.push(value.AttributeID);
                            } else {
                                if (value.InputTypeID == 7) {
                                    valz = parseFloat(value.AttributeValue).toFixed(2);
                                }
                                else {
                                    valz = value.AttributeValue;
                                }
                            }
                            var y = Encoder.htmlDecode(valz);
                            y = y.replace(re, '\\');
                            var attributValue;
                            if (groupItems.length > 0) {
                                $.each(groupItems, function (index, item) {
                                    if (value.ItemID = item) {
                                        if (value.InputTypeID == 7) {
                                            if (value.AttributeValue != "") {
                                                attributValue = [{ CssClass: cssClass, AttributeValue: getLocale(AspxItemsCompare, "Starting At") + y }];
                                                return;
                                            }
                                            else {
                                                attributValue = [{ CssClass: cssClass, AttributeValue: y }];
                                                return;
                                            }
                                        }
                                        else {
                                            attributValue = [{ CssClass: cssClass, AttributeValue: y }];
                                            return;
                                        }
                                    }
                                    else {
                                        attributValue = [{ CssClass: cssClass, AttributeValue: y }];
                                        return;
                                    }
                                });
                            }
                            else {
                                attributValue = [{ CssClass: cssClass, AttributeValue: y }];
                            }
                            $("#scriptAttributeValue").tmpl(attributValue).appendTo("#tblItemCompareList tbody#itemDetailBody>tr:last");
                            myAttributes.push(value.AttributeID);

                        }
                        else {
                            var valz1;
                            if (value.AttributeValue == "") {
                                valz1 = "n/a";
                            } else {
                                if (value.InputTypeID == 7) {
                                    valz1 = parseFloat(value.AttributeValue).toFixed(2);
                                }
                                else {
                                    valz1 = value.AttributeValue;
                                }
                            }
                            var z = Encoder.htmlDecode(valz1);
                            z = z.replace(re, '\\');
                            var i = index % (myAttributes.length);
                            attributValue = [{ CssClass: cssClass, AttributeValue: z }];
                            $("#scriptAttributeValue").tmpl(attributValue).appendTo("#trCompare_" + i + "");
                        }
                    });

                    $("#tblItemCompareList tr:even").addClass("sfEven");
                    $("#tblItemCompareList tr:odd").addClass("sfOdd");
                    var cookieCurrency = $("#ddlCurrency").val();
                    Currency.currentCurrency = BaseCurrency;
                    Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                },
                error: function () {
                    csscody.error('<h2>' + getLocale(AspxItemsCompare, 'Error Message') + '</h2><p>' + getLocale(AspxItemsCompare, 'Sorry, Compare list error occured!') + '</p>');
                }
            });
            if (groupItems.length > 0) {
                $.each(groupItems, function (index, item) {
                    $(".cssClassCompareAttributeClass >span").each(function () {
                        var colposition = $(this).parents("td").prop('cellIndex');
                        if ($(this).html() == "Price: ") {
                            $(this).parents("tr").find("td").eq(index).prepend(getLocale(AspxItemsCompare, "Starting At"));
                        }
                    });
                });
            }
        }
        var RecentAdd = function (Id, costVar) {
            var param = JSON2.stringify({ IDs: Id, CostVarinatIds: costVar, aspxCommonObj: aspxCommonObj() });
            $.ajax({
                type: "Post",
                url: p.servicepath + "AddComparedItems",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                },
                error: function () {
                    csscody.error('<h2>' + getLocale(AspxItemsCompare, 'Error Message') + '</h2><p>' + getLocale(AspxItemsCompare, 'Sorry, error occured!') + '</p>');
                }
            });
        }
        var GetCompareListImage = function (IDs, costVar) {
            $.ajax({
                type: "POST",
                async: false,
                url: p.servicepath + "GetCompareListImage",
                data: JSON2.stringify({ itemIDs: IDs, CostVariantValueIDs: costVar, aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var htMl = '';
                    $("#divCompareElements").html("");
                    var length = msg.d.length;
                    if (length > 0) {
                        var value;
                        for (var index = 0; index < length; index++) {
                            value = msg.d[index];
                            var imagePath = itemImagePath + value.BaseImage;
                            if (value.BaseImage == "") {
                                imagePath = p.NoImageItemComparePath;
                            }
                            else if (value.AlternateText == "") {
                                value.AlternateText = value.Name;
                            }

                            var items = [{
                                aspxRedirectPath: aspxRedirectPath, itemID: value.ItemID, CostVariant: (value.CostVariantItemID == 0 ? false : true), index: index, name: value.Name, sku: value.SKU,
                                imagePath: aspxRootPath + imagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: value.ListPrice,
                                price: value.Price, shortDescription: Encoder.htmlDecode(value.ShortDescription), itemTypeID: value.ItemTypeID
                            }];
                            $("#scriptResultProductGrid2").tmpl(items).appendTo("#tblItemCompareList thead > tr");
                            if (value.ListPrice == "") {
                                $(".cssRegularPrice_" + value.ItemID + "").parent('p').remove();
                            }
                            if (p.AllowAddToCart.toLowerCase() == 'true') {
                                $("#cssClassAddtoCard_" + value.ItemID + "_" + index).show();
                                if (p.AllowOutStockPurchase.toLowerCase() == 'false') {
                                    if (value.IsOutOfStock) {
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index + "").find('label').removeClass();
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index + " span").html(getLocale(AspxItemsCompare, 'Out Of Stock'));
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index).removeClass('cssClassAddtoCard');
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index).addClass('cssClassOutOfStock');
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index + " a").removeAttr('onclick');
                                        $("#cssClassAddtoCard_" + value.ItemID + "_" + index).find(".sfButtonwrapper").addClass('cssClassOutOfStock');
                                    }
                                }
                            }
                            if (value.ItemTypeID == 5) {
                                groupItems.push(value.ItemID);
                                $("#cssClassProductsGridRealPrice_" + value.ItemID).prepend(getLocale(AspxItemsCompare, "Starting At"));
                            }
                            if (value.CostVariantItemID != '0') {
                                var href = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + value.SKU + pageExtension + '?varId=' + value.CostVariantItemID + '';
                                $("#cssClassAddtoCard_" + value.ItemID + "_" + index).find('a').attr('href', href);
                            }

                        };
                    }
                    else {
                        $(".cssClassHeaderRight").hide();
                    }
                    var cookieCurrency = $("#ddlCurrency").val();
                    Currency.currentCurrency = BaseCurrency;
                    Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                },
                error: function () {
                    csscody.error('<h2>' + getLocale(AspxItemsCompare, 'Error Message') + '</h2><p>' + getLocale(AspxItemsCompare, 'Sorry, compare list error occured!') + '</p>');
                }
            });
        }
        IDs = $.cookies.get("ItemCompareDetail");
        costVar = $.cookies.get("costVariant");
        if (IDs != null && IDs != '') {
            GetCompareListImage(IDs, costVar);
            GetCompareList(IDs, costVar);
            RecentAdd(IDs, costVar);
        } else {
            $("#divCompareElementsPopUP").html('<span class="cssClassNotFound">' + getLocale(AspxItemsCompare, 'No Items found in you Compare Item List.') + '</span>');
        }
        if ($("#tblRecentlyComparedItemList").length > 0) {
            RecentlyComparedItems.RecentlyCompareItemsList();
        }
        $('#btnPrintItemCompare').click(function () {
            printPage();
        });
        printPage = function () {
            window.print();
            if (window.stop) {
                location.reload(); window.stop();
            }
            return false;
        };
        var CheckWishListUniqueness = function(itemID, sku, CostVariant){
            if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                if (CostVariant == '0') {
                    CostVariant = "";
                }
                var checkparam = { ID: itemID, costVariantValueIDs: CostVariant, aspxCommonObj: aspxCommonObj() };
                var checkdata = JSON2.stringify(checkparam);
                $.ajax({
                    type: "POST",
                    url: servicePath + "CheckWishItems",
                    data: checkdata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d) {
                            csscody.alert('<h2>' + getLocale(AspxItemsCompare, 'Information Alert') + '</h2><p>' + getLocale(AspxItemsCompare, 'The selected item already in your wishlist.') + '</p>');
                        } else {
                            AspxCommerce.RootFunction.AddToWishListFromJS(itemID, ip, countryName, CostVariant);
                        }
                    }
                });
            } else {
                window.location.href = aspxRootPath + LogInURL + pageExtension;
                return false;
            }
        }
        ItemCompareDetailsAPI = function () {
            return {
                CheckWishListUniqueness : CheckWishListUniqueness
            };
        }();
    };
    $.fn.ItemCompareDetails = function (p) {
        $.ItemCompareDetailsView(p);
    };
})(jQuery);