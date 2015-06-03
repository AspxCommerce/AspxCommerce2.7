var ItemDetail = "";
$(function () {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var userIP = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var itemId = itemID;
    var itemName = itemNamePageBehind;
    var ratingValues = '';
    var ItemsReview = new Array();
    var isNotificationExist = false;
    var variantId = new Array();
    var arrItemDetailsReviewList = new Array();
    var arrItemReviewList = new Array();
    var arrCostVariants;
    var FormCount = new Array();
    var arrCombination = [];
    var variantValuesID = [];
    var itemQuantityInCart;
    var groupItemQty = [];
    var Imagelist = '';
    var ImageTypes = '';
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: storeId,
            PortalID: portalId,
            UserName: userName,
            CultureName: cultureName,
            CustomerID: customerId,
            SessionCode: sessionCode
        };
        return aspxCommonInfo;
    };
    GetCount = function (id) {
        var count = 0;
        for (var i = 0; i < variantValuesID.length; i++) {
            if (variantValuesID[i] == id) {
                count++;
            }
        }
        return count;
    };
    CheckContains = function (checkon, toCheck) {
        var x = checkon.split('@');
        for (var i = 0; i < x.length; i++) {
            if (x[i] == toCheck) {
                return true;
            }
        }
        return false;
    };
    IsExists = function (arr, val) {
        var isExist = false;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == val) {
                isExist = true; break;
            }
        }
        return isExist;
    };
    function getObjects(obj, key) {
        var objects = [];
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                objects.push(obj[i][key]);
            }

        }
        return objects;
    }
    function getValByObjects(obj, key, key2, x, y, keyOfValue) {
        var value = '';
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                if (obj[i][key] == x && obj[i][key2] == y) {
                    value = obj[i][keyOfValue];
                }
            }

        }
        return value;
    }

    function getModifiersByObjects(obj, combinationId) {
        var modifiers = { Price: '', IsPricePercentage: false, Weight: '', IsWeightPercentage: false, Quantity: 0 };

        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                if (obj[i]["CombinationID"] == combinationId) {
                    modifiers.Price = obj[i]["CombinationPriceModifier"];
                    modifiers.IsPricePercentage = obj[i]["CombinationPriceModifierType"];
                    modifiers.Weight = obj[i]["CombinationWeightModifier"];
                    modifiers.IsWeightPercentage = obj[i]["CombinationWeightModifierType"];
                    modifiers.Quantity = obj[i]["CombinationQuantity"];
                }
            }

        }
        return modifiers;
    }
    function checkAvailibility(elem) {
        var cvids = [];
        var values = [];
        var currentValue = elem == null ? 1 : $(elem).val();
        var currentCostVariant = elem == null ? 1 : $(elem).parents('span:eq(0)').prop('id').replace('subDiv', '');
        $("#Notify").hide();
        $("#divCostVariant select option:selected").each(function () {
            if (this.value != 0) {
                values.push(this.value);
                cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
            }
        });

        $("#divCostVariant input[type=radio]:checked").each(function () {
            if ($(this).is(":checked"))
            { $(this).addClass("cssRadioChecked") }
            else { $(this).removeClass("cssRadioChecked"); }
            values.push(this.value);
            cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
        });

        $("#divCostVariant input[type=radio]").each(function () {
            if ($(this).is(":checked"))
            { $(this).addClass("cssRadioChecked") }
            else { $(this).removeClass("cssRadioChecked"); }
        });

        $("#divCostVariant input[type=checkbox]:checked").each(function () {
            values.push(this.value);
            cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
        });

        var infos = CheckVariantCombination(cvids.join('@'), values.join('@'), currentCostVariant, currentValue);

        $("#spanAvailability").html(ItemDetail.info.IsCombinationMatched == true ? '<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>' : '<b>' + getLocale(AspxItemDetails, 'Not available') + '</b>');
        if (ItemDetail.info.IsCombinationMatched == true) {
            $("#btnAddToMyCart").removeClass("cssClassOutOfStock").addClass('addtoCart ').removeAttr("disabled").prop('enabled', "enabled").find("span").html(getLocale(AspxItemDetails, "Cart +"));
            $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
        }
        else {
            $("#btnAddToMyCart").removeClass("addtoCart").addClass('cssClassOutOfStock').prop("disabled", "disabled").find("span").html(getLocale(AspxItemDetails, "Out Of Stock"));
            $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
        }

        if (ItemDetail.info.IsCombinationMatched) {
            $("#hdnQuantity").val('').val(ItemDetail.info.Quantity);
            $("#txtQty").removeAttr('disabled').prop("enabled", "enabled");
            if (ItemDetail.info.Quantity == 0 || ItemDetail.info.Quantity < 0) {
                if (allowAddToCart.toLowerCase() == 'true') {
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        $("#spanAvailability").html("<b>" + getLocale(AspxItemDetails, "Out Of Stock") + "</b>");
                        $("#spanAvailability").addClass('cssOutOfStock');
                        $("#spanAvailability").removeClass('cssInStock');
                        if (userName != "anonymoususer") {
                            $("#Notify").show();
                            $("#Notify #txtNotifiy").hide();
                        }
                        else {
                            $("#Notify").show();
                            $("#txtNotifiy").show();
                        }
                    }
                }
                else {
                    $("#btnAddToMyCart").parent('label').remove();
                }

            } else {
                $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>' + '</b>');
                $("#spanAvailability").removeClass('cssOutOfStock');
                $("#spanAvailability").addClass('cssInStock');
                $("#Notify").hide();
            }
            var itemsCartInfo = ItemDetail.CheckItemQuantityInCart(itemID, values.join('@') + '@');
            var quantityinCart = itemsCartInfo.ItemQuantityInCart;
            if (ItemDetail.info.Quantity <= quantityinCart) {
                if (allowAddToCart.toLowerCase() == 'true') {
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        $("#txtQty").removeAttr('enabled').prop("disabled", "disabled");
                        $("#btnAddToMyCart").removeClass("addtoCart ").addClass('cssClassOutOfStock').prop("disabled", "disabled").find("span").html(getLocale(AspxItemDetails, "Out Of Stock"));
                        $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
                        $("#spanAvailability").html("<b>" + getLocale(AspxItemDetails, "Out Of Stock") + "</b>");
                        $("#spanAvailability").addClass('cssOutOfStock');
                        $("#spanAvailability").removeClass('cssInStock');
                        if (userName != "anonymoususer") {
                            $("#Notify").show();
                            $("#Notify #txtNotifiy").hide();
                        }
                        else {
                            $("#Notify").show();
                            $("#txtNotifiy").show();
                        }
                    }
                }
                else {
                    $("#btnAddToMyCart").parent('label').remove();
                }
            }
            var price = 0;
            if (ItemDetail.info.IsPricePercentage) {
                price = eval($("#hdnPrice").val()) * eval(ItemDetail.info.PriceModifier) / 100;
            } else {
                price = eval(ItemDetail.info.PriceModifier);
            }

            $("#spanPrice").html(parseFloat((eval($("#hdnPrice").val())) + (eval(price))).toFixed(2));
            $("#spanPrice").attr('bc', parseFloat((eval($("#hdnPrice").val())) + (eval(price))).toFixed(2));
            var taxPriceVariant = eval($("#hdnPrice").val()) + eval(price);
            var taxrate = (eval($("#hdnTaxRateValue").val()) * 100) / (eval($("#hdnPrice").val()));
            $("#spanTax").html(parseFloat(((taxPriceVariant * taxrate) / 100)).toFixed(2));
            if ($("#hdnListPrice").val() != '') {
                $(".cssClassYouSave").show();
                var variantAddedPrice = eval($("#hdnPrice").val()) + eval(price);
                var variantAddedSavingPercent = (($("#hdnListPrice").val() - variantAddedPrice) / $("#hdnListPrice").val()) * 100;
                savingPercent2 = variantAddedSavingPercent.toFixed(2);
                $("#spanSaving").html('<b>' + variantAddedSavingPercent.toFixed(2) + '%</b>');
            }
            ItemDetail.ResetGallery(ItemDetail.info.CombinationID);

        } else {
            $("#btnAddToMyCart").removeClass("cssClassOutOfStock").addClass('addtoCart ').removeAttr("disabled").prop('enabled', "enabled").find("span").html(getLocale(AspxItemDetails, "Cart +"));
            $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
            $("#spanAvailability").removeClass('cssOutOfStock');
            $("#spanAvailability").removeClass('cssInStock');

        }
        var cookieCurrency = $("#ddlCurrency").val();
        $('#spanPrice').removeAttr('data-currency-' + cookieCurrency + '');
        $('#spanPrice').removeAttr('data-currency');
        Currency.currentCurrency = BaseCurrency;
        Currency.convertAll(Currency.currentCurrency, cookieCurrency);
    }


    function getSelectecdVariantValues() {


        var cvids = [];
        var values = [];

        $("#divCostVariant select option:selected").each(function () {
            if (this.value != 0) {
                values.push(this.value); cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
            }
        });

        $("#divCostVariant input[type=radio]:checked").each(function () {
            if ($(this).is(":checked"))
            { $(this).addClass("cssRadioChecked") }
            else { $(this).removeClass("cssRadioChecked"); }
            values.push(this.value);
            cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
        });

        $("#divCostVariant input[type=radio]").each(function () {
            if ($(this).is(":checked"))
            { $(this).addClass("cssRadioChecked") }
            else { $(this).removeClass("cssRadioChecked"); }
        });

        $("#divCostVariant input[type=checkbox]:checked").each(function () {
            values.push(this.value);
            cvids.push($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
        });
        return { Values: values, CVIds: cvids };
    }
    function selectFirstcombination() {
        if (arrCombination.length > 0) {
            var cvcombinationList = getObjects(arrCombination, 'CombinationType');
            var cvValuecombinationList = getObjects(arrCombination, 'CombinationValues');
            var x = cvcombinationList[0].split('@');
            var y = cvValuecombinationList[0].split('@');
            $("#Notify").hide();
            $("#divCostVariant select").each(function (i) {
                if (parseFloat($(this).parent("span:eq(0)").prop('id').replace('subDiv', '')) == x[i]) {
                    if ($(this).find("option[value=" + y[i] + "]").length > 0) {
                        $(this).find("option[value=" + y[i] + "]").prop('selected', 'selected');

                    } else {
                        var options = $(this).html();
                        var noOption = "<option value='0'>" + getLocale(AspxItemDetails, "Not required") + "</option>";
                        $(this).html(noOption + options);
                        $(this).find('option[value=0]').prop('selected', 'selected');
                    }
                } else {
                    var val = parseFloat($(this).parent("span:eq(0)").prop('id').replace('subDiv', ''));
                    var xIndex = 0;
                    for (var indx = 0; indx < x.length; indx++) {
                        if (x[indx] == val) {
                            xIndex = indx;
                            break;
                        }
                    }

                    if ($(this).find("option[value=" + y[xIndex] + "]").length > 0) {
                        $(this).find("option[value=" + y[xIndex] + "]").prop('selected', 'selected');

                    } else {
                        var options = $(this).html();
                        var noOption = "<option value='0'>" + getLocale(AspxItemDetails, "Not required") + "</option>";
                        $(this).html(noOption + options);
                        $(this).find('option[value=0]').prop('selected', 'selected');
                    }
                }

            });

            $("#divCostVariant input[type=radio]").each(function (i) {
                if (parseFloat($(this).parents("span:eq(0)").prop('id').replace('subDiv', '')) == x[i]) {
                    if ($(this).val() == y[i]) {
                        $(this).prop('checked', 'checked').addClass("cssRadioChecked");

                    } else {
                        $(this).removeAttr('checked');
                    }
                } else {
                    var val = parseFloat($(this).parents("span:eq(0)").prop('id').replace('subDiv', ''));
                    var xIndex = 0;
                    for (var indx = 0; indx < x.length; indx++) {
                        if (x[indx] == val) {
                            xIndex = indx;
                            break;
                        }
                    }
                    if ($(this).val() == y[xIndex]) {
                        $(this).prop('checked', 'checked').addClass("cssRadioChecked");

                    } else {
                        $(this).removeAttr('checked');
                    }
                }
            });

            $("#divCostVariant input[type=checkbox]:checked").each(function (i) {
                if (parseFloat($(this).parent("span:eq(0)").prop('id').replace('subDiv', '')) == x[i]) {
                    if ($(this).val() == y[i]) {
                        $(this).prop('checked', 'checked');

                    } else {
                        $(this).removeAttr('checked');
                    }
                } else {
                    var val = parseFloat($(this).parent("span:eq(0)").prop('id').replace('subDiv', ''));
                    var xIndex = 0;
                    for (var indx = 0; indx < x.length; indx++) {
                        if (x[indx] == val) {
                            xIndex = indx;
                            break;
                        }
                    }

                    if ($(this).val() == y[xIndex]) {
                        $(this).prop('checked', 'checked');

                    } else {
                        $(this).removeAttr('checked');
                    }
                }
            });


            CheckVariantCombination(cvcombinationList[0], cvValuecombinationList[0], x[0], y[0]);
            $("#spanAvailability").html(ItemDetail.info.IsCombinationMatched == true ? '<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>' : '<b>' + getLocale(AspxItemDetails, 'Not available') + '</b>');
            if (ItemDetail.info.IsCombinationMatched == true) {
                $("#btnAddToMyCart").removeClass("cssClassOutOfStock").addClass('addtoCart ').removeAttr("disabled").prop('enabled', "enabled").find("span").html(getLocale(AspxItemDetails, "Cart +"));
                $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
            }
            else {
                $("#btnAddToMyCart").removeClass("addtoCart").addClass('cssClassOutOfStock').prop("disabled", "disabled").find("span").html(getLocale(AspxItemDetails, "Out Of Stock"));
                $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
            }
            if (ItemDetail.info.IsCombinationMatched) {
                $("#hdnQuantity").val('').val(ItemDetail.info.Quantity);
                $("#txtQty").removeAttr('disabled').prop("enabled", "enabled");
                if (ItemDetail.info.Quantity == 0 || ItemDetail.info.Quantity < 0) {
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            $("#btnAddToMyCart").removeClass("addtoCart ").addClass('cssClassOutOfStock').prop("disabled", "disabled").find("span").html(getLocale(AspxItemDetails, "Out Of Stock"));
                            $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
                            $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'Out Of Stock') + '</b>');
                            $("#spanAvailability").addClass('cssOutOfStock');
                            $("#spanAvailability").removeClass('cssInStock');
                            if (userName != "anonymoususer") {
                                $("#Notify").show();
                                $("#Notify #txtNotifiy").hide();
                            } else {
                                $("#Notify").show();
                                $("#txtNotifiy").show();
                            }
                        }
                    }
                    else {
                        $("#btnAddToMyCart").parent('label').remove();
                    }

                } else {
                    $("#btnAddToMyCart").removeClass("cssClassOutOfStock").addClass('addtoCart ').removeAttr("disabled").prop('enabled', "enabled").find("span").html(getLocale(AspxItemDetails, "Cart +"));
                    $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
                    $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>');
                    $("#spanAvailability").removeClass('cssOutOfStock');
                    $("#spanAvailability").addClass('cssInStock');
                    $("#Notify").hide();
                }
                var values = getSelectecdVariantValues().Values;
                var itemsCartInfo = ItemDetail.CheckItemQuantityInCart(itemID, values.join('@') + '@');
                var quantityinCart = itemsCartInfo.ItemQuantityInCart;
                if (ItemDetail.info.Quantity <= quantityinCart) {
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            $("#txtQty").removeAttr('enabled').prop("disabled", "disabled");
                            $("#btnAddToMyCart").removeClass("addtoCart ").addClass('cssClassOutOfStock').prop("disabled", "disabled").find("span").html(getLocale(AspxItemDetails, "Out Of Stock"));
                            $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
                            $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'Out Of Stock') + '</b>');
                            $("#spanAvailability").addClass('cssOutOfStock');
                            $("#spanAvailability").removeClass('cssInStock');
                            if (userName != "anonymoususer") {
                                $("#Notify").show();
                                $("#Notify #txtNotifiy").hide();
                            } else {
                                $("#Notify").show()
                                $("#txtNotifiy").show();
                            }
                        }
                    }
                    else {
                        $("#btnAddToMyCart").parent('label').remove();
                    }
                }
                var price = 0;
                if (ItemDetail.info.IsPricePercentage) {
                    price = eval($("#hdnPrice").val()) * eval(ItemDetail.info.PriceModifier) / 100;
                } else {
                    price = eval(ItemDetail.info.PriceModifier);
                }

                $("#spanPrice").html(parseFloat((eval($("#hdnPrice").val())) + (eval(price))).toFixed(2));
                $("#spanPrice").attr('bc', parseFloat((eval($("#hdnPrice").val())) + (eval(price))).toFixed(2));
                var taxPriceVariant = eval($("#hdnPrice").val()) + eval(price);
                var taxrate = (parseFloat(($("#hdnTaxRateValue").val()) * 100) / (eval($("#hdnPrice").val())).toFixed(2));
                if ($("#hdnListPrice").val() != '') {
                    $(".cssClassYouSave").show();
                    var variantAddedPrice = eval($("#hdnPrice").val()) + eval(price);
                    var variantAddedSavingPercent = (($("#hdnListPrice").val() - variantAddedPrice) / $("#hdnListPrice").val()) * 100;
                    savingPercent2 = variantAddedSavingPercent.toFixed(2);
                    $("#spanSaving").html('<b>' + variantAddedSavingPercent.toFixed(2) + '%</b>');
                }
            }
        }
        var cookieCurrency = $("#ddlCurrency").val();
        $('#spanPrice').removeAttr('data-currency-' + cookieCurrency + '');
        $('#spanPrice').removeAttr('data-currency');
        Currency.currentCurrency = BaseCurrency;
        Currency.convertAll(Currency.currentCurrency, cookieCurrency);
    }

    CheckVariantCombination = function (costVIds, costValIds, currentCostVar, currentCostVal) {
        ItemDetail.info.IsCombinationMatched = false;
        var cvcombinationList = getObjects(arrCombination, 'CombinationType');
        var cvValuecombinationList = getObjects(arrCombination, 'CombinationValues');
        for (var j = 0; j < cvcombinationList.length; j++) {
            if (ItemDetail.info.IsCombinationMatched == true)
                break;
            var matchedIndex = 0;
            var matchedValues = 0;
            if (cvcombinationList[j].length == costVIds.length) {
                var cb = costVIds.split('@');
                for (var id = 0; id < cb.length; id++) {
                    if (ItemDetail.info.IsCombinationMatched == true)
                        break;
                    var element = cb[id];
                    if (CheckContains(cvcombinationList[j], element)) {
                        matchedIndex++;
                        if (matchedIndex == cb.length) {
                            var cvb = costValIds.split('@');
                            for (var d = 0; d < cvb.length; d++) {
                                var element1 = cvb[d];
                                if (CheckContains(cvValuecombinationList[j], element1)) {
                                    matchedValues++;
                                }
                                if (matchedValues == cvb.length) {
                                    var combinationId = getValByObjects(arrCombination, 'CombinationType', 'CombinationValues', cvcombinationList[j], cvValuecombinationList[j], 'CombinationID');
                                    var modifiers = getModifiersByObjects(arrCombination, combinationId);
                                    ItemDetail.info.IsCombinationMatched = true;
                                    ItemDetail.info.CombinationID = combinationId;
                                    ItemDetail.info.IsPricePercentage = modifiers.IsPricePercentage;
                                    ItemDetail.info.PriceModifier = modifiers.Price;
                                    ItemDetail.info.Quantity = modifiers.Quantity;
                                    ItemDetail.info.IsWeightPercentage = modifiers.IsWeightPercentage;
                                    ItemDetail.info.WeightModifier = modifiers.Weight;
                                    break;
                                } else {
                                    ItemDetail.info.IsCombinationMatched = false;
                                    ItemDetail.info.CombinationID = 0;
                                    ItemDetail.info.IsPricePercentage = false;
                                    ItemDetail.info.PriceModifier = 0;
                                    ItemDetail.info.Quantity = 0;
                                    ItemDetail.info.IsWeightPercentage = false;
                                    ItemDetail.info.WeightModifier = 0;
                                }
                            }
                        } else {
                            ItemDetail.info.IsCombinationMatched = false;
                            ItemDetail.info.CombinationID = 0;
                            ItemDetail.info.IsPricePercentage = false;
                            ItemDetail.info.PriceModifier = 0;
                            ItemDetail.info.Quantity = 0;
                            ItemDetail.info.IsWeightPercentage = false;
                            ItemDetail.info.WeightModifier = 0;

                            var combinationIndex0 = cvcombinationList[j].split('@');
                            var combinationIndex01 = cvValuecombinationList[j].split('@');
                            for (var w = 0; w < combinationIndex0.length; w++) {
                                if (combinationIndex0[w] == currentCostVar) {
                                    if (combinationIndex01[w] == currentCostVal) {

                                        if (!IsExists(ItemDetail.info.AvailableCombination, cvValuecombinationList[j]))
                                            ItemDetail.info.AvailableCombination.push(cvValuecombinationList[j]);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else {
                var combinationIndex = cvcombinationList[j].split('@');
                var combinationIndex1 = cvValuecombinationList[j].split('@');
                for (var z = 0; z < combinationIndex.length; z++) {
                    if (combinationIndex[z] == currentCostVar) {
                        if (combinationIndex1[z] == currentCostVal) {
                            if (!IsExists(ItemDetail.info.AvailableCombination, cvValuecombinationList[j]))
                                ItemDetail.info.AvailableCombination.push(cvValuecombinationList[j]);
                        }
                    }
                }
            }
        }
        return ItemDetail.info;
    };

    var snippet = {};
    ItemDetail = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            oncomplete: 0,
            ajaxCallMode: "",
            error: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: ItemDetail.config.type,
                contentType: ItemDetail.config.contentType,
                cache: ItemDetail.config.cache,
                async: ItemDetail.config.async,
                url: ItemDetail.config.url,
                data: ItemDetail.config.data,
                dataType: ItemDetail.config.dataType,
                success: ItemDetail.config.ajaxCallMode,
                error: ItemDetail.config.error,
                complete: ItemDetail.oncomplete
            });
        },
        CreateSnippet: function () {
            try {
                var snippetHtml = "";
                snippetHtml += "<div style=\"display:none;\"> <div itemscope itemtype=\"http://data-vocabulary.org/Product\"> ";
                snippetHtml += " <span itemprop=\"brand\" >" + snippet.Brand + "</span> ";
                snippetHtml += "<span itemprop=\"name\">" + snippet.Name + "</span>";
                snippetHtml += "<span itemprop=\"description\">" + snippet.Description + "</span>";
                snippetHtml += "<span itemprop=\"category\"  content=\"" + snippet.Description + "\"></span>";


                snippetHtml += "<span itemprop=\"review\" itemscopeitemtype=\"http://data-vocabulary.org/Review-aggregate\">";
                snippetHtml += "<span itemprop=\"rating\">" + art + "</span>";
                snippetHtml += "<span itemprop=\"count\">" + trc + "</span>";
                snippetHtml += "</span>";

                snippetHtml += "<span itemprop=\"offerDetails\" itemscope itemtype=\"http://data-vocabulary.org/Offer\">";
                snippetHtml += "<span itemprop=\"price\">" + snippet.Price + "</span>";
                snippetHtml += "<span itemprop=\"currency\">" + "USD" + "</span>";
                var availability = snippet.IsOutOfStock == true ? "out_of_stock" : "in_stock";
                snippetHtml += "<span itemprop=\"availability\" content=\"" + availability + "\"></span>";

                snippetHtml += "</span>";
                snippetHtml += "</div>";

                $("body").append(snippetHtml);
            } catch (e) {

            }

        },

        SetCompareItemsCount: function (msg) {
            ItemDetail.vars.countCompareItems = msg.d;
        },

        BindItemQuantityDiscount: function (msg) {
            $("#itemQtyDiscount>tbody").html('');
            if (msg.d.length > 0) {
                $("#bulkDiscount").html(getLocale(AspxItemDetails, '(Bulk Discount available)'));
                $("#bulkDiscount").show();
                var qytDiscount = '';
                $.each(msg.d, function (index, item) {
                    qytDiscount += "<tr><td>" + parseFloat(item.Quantity) + "</td><td><span class='cssClassFormatCurrency'>" + (item.Price).toFixed(2) + "</span></td></tr>";
                });
                $("#itemQtyDiscount>tbody").append(qytDiscount);
                $("#itemQtyDiscount > tbody tr:even").addClass("sfEven");
                $("#itemQtyDiscount > tbody tr:odd").addClass("sfOdd");
            } else {
                $("#bulkDiscount").hide();
                $("#divQtyDiscount").hide();
            }
        },

        BindCostVariant: function (msg) {
            if (msg.d.length > 0) {
                var CostVariant = '';
                var variantValue = [];
                $.each(msg.d, function (index, item) {
                    if (CostVariant.indexOf(item.CostVariantID) == -1) {
                        CostVariant += item.CostVariantID;
                        variantId.push(item.CostVariantID);
                        var addSpan = '';
                        addSpan += '<div id="div_' + item.CostVariantID + '" class="cssClassHalfColumn">';
                        addSpan += '<span id="spn_' + item.CostVariantID + '" ><b>' + item.CostVariantName + ':</b> ' + '</span>';
                        addSpan += '<span class="spn_Close"><a href="#"><img class="imgDelete" src="' + aspxTemplateFolderPath + '/images/admin/uncheck.png" title="' + getLocale(AspxItemDetails, "Don\'t use this option") + '"" alt="' + getLocale(AspxItemDetails, "Don\'t use this option") + '"/></a></span>'; $('#divCostVariant').append(addSpan);
                        addSpan += '</div>';

                    }
                    var valueID = '';
                    var itemCostValueName = '';
                    if (item.CostVariantsValueID != -1) {
                        if (item.InputTypeID == 5 || item.InputTypeID == 6) {
                            if ($('#controlCostVariant_' + item.CostVariantID + '').length == 0) {
                                itemCostValueName += '<span class="sfListmenu" id="subDiv' + item.CostVariantID + '">';
                                valueID = 'controlCostVariant_' + item.CostVariantID;
                                itemCostValueName += ItemDetail.CreateControl(item, valueID, false);

                                itemCostValueName += "</span>";
                                $('#div_' + item.CostVariantID + '').append(itemCostValueName);
                            }
                            if (!IsExists(variantValue, item.CostVariantsValueID)) {
                                variantValue.push(item.CostVariantsValueID);
                                optionValues = ItemDetail.BindInsideControl(item, valueID);
                                $('#controlCostVariant_' + item.CostVariantID + '').append(optionValues);
                            }
                            $('#controlCostVariant_' + item.CostVariantID + ' option:first-child').prop("selected", "selected");
                        } else {
                            if ($('#subDiv' + item.CostVariantID + '').length == 0) {
                                itemCostValueName += '<span class="cssClassRadio" id="subDiv' + item.CostVariantID + '">';
                                valueID = 'controlCostVariant_' + item.CostVariantID;
                                itemCostValueName += ItemDetail.CreateControl(item, valueID, true);
                                itemCostValueName += "</span>";
                                $('#div_' + item.CostVariantID + '').append(itemCostValueName);
                            } else {
                                valueID = 'controlCostVariant_' + item.CostVariantID;
                                itemCostValueName += ItemDetail.CreateControl(item, valueID, false);
                                $('#subDiv' + item.CostVariantID + '').append(itemCostValueName);
                            }
                        }
                    }
                });
                $('#divCostVariant').append('<div class="cssClassClear"></div>');
                if ($('#divCostVariant').is(':empty')) {
                    $("#divCostVariant").removeClass("cssClassCostVariant");
                } else {
                    $("#divCostVariant").addClass("cssClassCostVariant");
                }
                if ($.session("ItemCostVariantData") != undefined) {
                    $.each(arrCostVariants, function (i, variant) {
                        var itemColl = $("#divCostVariant").find("[Variantname=" + variant + "]");
                        if ($(itemColl).is("input[type='checkbox'] ,input[type='radio']")) {
                            $("#divCostVariant").find("input:checkbox").removeAttr("checked");
                            $(itemColl).prop("checked", "checked");
                        } else if ($(itemColl).is('select>option')) {
                            $("#divCostVariant").find("select>option").removeAttr("selected");
                            $(itemColl).prop("selected", "selected");
                        }

                    });
                    $.session("ItemCostVariantData", 'empty');
                }
                $('#divCostVariant select,#divCostVariant input[type=radio],#divCostVariant input[type=checkbox]').unbind().bind("change", function () {
                    checkAvailibility(this);
                });

                $("#divCostVariant").on("click", ".spn_Close", function () {
                    $(this).next('span:first').find(" input[type=radio]").removeAttr('checked');
                    if ($(this).next('span:first').find("select").find("option[value=0]").length == 0) {
                        var options = $(this).next('span:first').find("select").html();
                        var noOption = "<option value=0 >" + getLocale(AspxItemDetails, "Not required") + "</option>";
                        $(this).next('span:first').find("select").html(noOption + options);
                    } else {
                        $(this).next('span:first').find("select").find("option[value=0]").prop('selected', 'selected');
                    }
                    checkAvailibility(null);
                });
                $('.cssClassDropDownItem').MakeFancyItemDropDown();
                setTimeout(function () {
                    ItemDetail.LoadCostVariantCombination(itemSKU);
                    ItemDetail.variantCheckQuery();
                }, 200);
            }
            else {
                ItemDetail.LoadCostVariantCombination(itemSKU);
            }
        },

        BindItemRatingCriteria: function (msg) {
            if (msg.d.length > 0) {
                $.each(msg.d, function (index, item) {
                    ItemDetail.RatingCriteria(item);
                });
            } else {
                csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "No rating criteria are found!") + "</p>");
            }
        },

        SaveItemRatingMsg: function (msg) {
            csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Message") + "</h2><p>" + getLocale(AspxItemDetails, "Your review has been accepted for moderation.") + "</p>");
            $('#fade, #popuprel2').fadeOut();
            ItemDetail.CheckReviewByUser(userName);
            ItemDetail.CheckReviewByIP(userIP);
            if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                if (allowMultipleReviewPerUser.toLowerCase() != "true" && ItemDetail.vars.existReviewByUser.toLowerCase() == "true") {
                    $(".cssClassItemRating a").removeClass("popupAddReview");
                    $(".cssClassAddYourReview").hide();
                }
            }

            if (allowMultipleReviewPerIP.toLowerCase() != "true" && ItemDetail.vars.existReviewByIP.toLowerCase() == "true") {
                $(".cssClassItemRating a").removeClass("popupAddReview");
                $(".cssClassAddYourReview").hide();
            }

        },


        BindReferFriendPopup: function (msg) {
            $('#controlload').html(msg.d);
        },

        BindItemBasicInfoByitemSKU: function (msg) {
            if (msg.d != null) {
                ItemDetail.BindItemsBasicInfo(msg.d);
                if (itemTypeId != 5) {
                    ItemDetail.GetCostVariantsByitemSKU(itemSKU);
                }
            }
        },

        SetItemQuantityInCart: function (msg) {
            ItemDetail.vars.itemQuantityInCart = msg.d;
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
                csscody.addToCart('<h2>' + getLocale(AspxItemDetails, "Successful Message") + '</h2><p>' + getLocale(AspxItemDetails, 'Item has been successfully added to cart.') + '</p>', addToCartProperties);
                if (allowRealTimeNotifications.toLowerCase() == 'true') {
                    try {
                        var itemOnCart = $.connection._aspxrthub;
                        itemOnCart.server.checkIfItemOutOfStock(itemId, itemSKU, "", AspxCommerce.AspxCommonObj());

                    }
                    catch (Exception) {
                        console.log(getLocale(AspxItemDetails, 'Error Connecting Hub.'));
                    }
                }
                HeaderControl.GetCartItemTotalCount(); ShopingBag.GetCartItemCount(); ShopingBag.GetCartItemListDetails();
            }
            else if (msg.d == 2) {
                if (allowOutStockPurchase.toLowerCase() == 'false') {
                    csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'This product is currently Out Of Stock!') + "</p>");
                }
                else {
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
                    csscody.addToCart('<h2>' + getLocale(AspxItemDetails, "Successful Message") + '</h2><p>' + getLocale(AspxItemDetails, 'Item has been successfully added to cart.') + '</p>', addToCartProperties);
                    HeaderControl.GetCartItemTotalCount(); ShopingBag.GetCartItemCount(); ShopingBag.GetCartItemListDetails();
                }
            }


        },

        SetReviewByUserStatus: function (msg) {
            ItemDetail.vars.existReviewByUser = msg.d.toString();
        },

        SetReviewByIPStatus: function (msg) {
            ItemDetail.vars.existReviewByIP = msg.d.toString();
        },

        BindCostVariantCombination: function (msg) {
            $.each(msg.d, function (index, item) {
                var CostVariantCombination = {
                    CombinationID: 0,
                    CombinationType: "",
                    CombinationValues: "",
                    CombinationPriceModifier: "",
                    CombinationPriceModifierType: "",
                    CombinationWeightModifier: "",
                    CombinationWeightModifierType: "",
                    CombinationQuantity: 0,
                    ImageFile: ""
                };
                CostVariantCombination = item;
                arrCombination.push(CostVariantCombination);
            });
            selectFirstcombination();
        },

        GetNotificationMessage: function () {
            csscody.info("<h2>" + getLocale(AspxItemDetails, "Information Message") + "</h2><p>" + getLocale(AspxItemDetails, "Thank you ! You will be notified as soon as the items get available in the store for purchase.") + "</p>");
        },
        GetNotificationResult: function (msg) {
            if (msg.d.length > 0) {
                $.each(msg.d, function (index, value) {
                    if (value.ItemSKU == itemSKU && value.Email == userEmail && value.MailStatus == 0) {
                        isNotificationExist = true;
                        return false;
                    }
                });
            }
        },
        oncomplete: function () {
            switch (ItemDetail.config.oncomplete) {
                case 20:
                    ItemDetail.config.oncomplete = 0;
                    if ($("#divCartDetails").length > 0) {
                        AspxCart.GetUserCartDetails();
                    }
                    if ($("#dynItemDetailsForm").length > 0) {
                        ItemDetail.BindItemBasicByitemSKU(itemSKU);
                    }
                    break;
            }
        },

        GetLoadErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxItemDetails, "Error Message") + '</h2><p>' + getLocale(AspxItemDetails, 'Failed to load cost variants!') + '</p>');
        },

        GetItemRatingErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxItemDetails, 'Error Message') + '</h2><p>' + getLocale(AspxItemDetails, 'Failed to save!') + '</p>');
        },

        GetAddToCartErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'Failed to add item to cart!') + '</p>');
        },
        GetNotificationErrorMsg: function () {
            csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Error') + '</h2><p>' + getLocale(AspxItemDetails, 'Error occured!') + "</p>");
        },
        vars: {
            countCompareItems: "",
            itemSKU: itemSKU,
            itemQuantityInCart: "",
            userItemQuantityInCart: "",
            userEmail: userEmail,
            itemId: itemID,
            existReviewByUser: "",
            existReviewByIP: ""
        },
        info: {
            IsCombinationMatched: false,
            AvailableCombination: [],
            CombinationID: 0,
            PriceModifier: '',
            IsPricePercentage: false,
            WeightModifier: '',
            IsWeightPercentage: false,
            Quantity: 0,
            MinCartQuantity: 0,
            MaxCartQuantity: 0

        },

        BindDownloadEvent: function () {
            $(".cssClassLink").jDownload({
                root: aspxFilePath,
                dialogTitle: getLocale(AspxItemDetails, 'AspxCommerce download sample item:')
            });
        },

        LoadCostVariantCombination: function () {
            var param = JSON2.stringify({ itemSku: itemSKU, aspxCommonObj: aspxCommonObj() });
            this.config.method = "AspxCoreHandler.ashx/GetCostVariantCombinationbyItemSku";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.BindCostVariantCombination;
            this.config.async = false;
            this.ajaxCall(this.config);
        },

        BindItemQuantityDiscountByUserName: function (itemSKU) {
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), itemSKU: itemSKU });
            this.config.method = "AspxCoreHandler.ashx/GetItemQuantityDiscountByUserName";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.BindItemQuantityDiscount;
            this.ajaxCall(this.config);

        },

        GetCostVariantsByitemSKU: function (itemSKU) {
            $('#divCostVariant').html('');
            var param = JSON2.stringify({ itemSku: itemSKU, aspxCommonObj: aspxCommonObj() });
            this.config.method = "AspxCoreHandler.ashx/GetCostVariantsByItemSKU";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.BindCostVariant;
            this.config.error = ItemDetail.GetLoadErrorMsg;
            this.ajaxCall(this.config);
        },

        CreateControl: function (item, controlID, isChecked) {
            var controlElement = '';
            var costPriceValue = item.CostVariantsPriceValue;
            var weightValue = item.CostVariantsWeightValue;
            if (item.InputTypeID == 5) {
                controlElement = "<select id='" + controlID + "' multiple></select>";
            } else if (item.InputTypeID == 6) {
                controlElement = "<select id='" + controlID + "'></select>";
            } else if (item.InputTypeID == 9 || item.InputTypeID == 10) {
                controlElement = "<label><input  name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "'><span>" + item.CostVariantsValueName + "</span></label>";

            } else if (item.InputTypeID == 11 || item.InputTypeID == 12) {
                controlElement = "<input  name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "'><label>" + item.CostVariantsValueName + "</label></br>";

            }
            return controlElement;
        },

        BindInsideControl: function (item, controlID) {
            var optionValues = '';
            var costPriceValue = item.CostVariantsPriceValue;
            var weightValue = item.CostVariantsWeightValue;
            if (item.InputTypeID == 5) {
                optionValues = "<option value=" + item.CostVariantsValueID + ">" + item.CostVariantsValueName + "</option>";

            } else if (item.InputTypeID == 6) {
                optionValues = "<option value=" + item.CostVariantsValueID + ">" + item.CostVariantsValueName + "</option>";

            }
            return optionValues;
        },

        CheckReviewByUser: function (userName) {
            var param = JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonObj() });
            this.config.method = "AspxCoreHandler.ashx/CheckReviewByUser";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.SetReviewByUserStatus;
            this.config.async = false;
            this.ajaxCall(this.config);
        },

        CheckReviewByIP: function (userIP) {
            var param = JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonObj(), userIP: userIP });
            this.config.method = "AspxCoreHandler.ashx/CheckReviewByIP";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.SetReviewByIPStatus;
            this.config.async = false;
            this.ajaxCall(this.config);
        },

        BindStarRating: function (itemAvgRating) {
            var ratingStars = '';
            var ratingTitle = [getLocale(AspxItemDetails, "Worst"), getLocale(AspxItemDetails, "Ugly"), getLocale(AspxItemDetails, "Bad"), getLocale(AspxItemDetails, "Not Bad"), getLocale(AspxItemDetails, "Average"), getLocale(AspxItemDetails, "OK"), getLocale(AspxItemDetails, "Nice"), getLocale(AspxItemDetails, "Good"), getLocale(AspxItemDetails, "Best"), getLocale(AspxItemDetails, "Excellent")]; var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
            var i = 0;
            ratingStars += '<tr><td>';
            for (i = 0; i < 10; i++) {
                if (itemAvgRating == ratingText[i]) {
                    ratingStars += '<input name="avgItemRating" type="radio" class="star {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                    $(".cssClassRatingTitle").html(ratingTitle[i]);
                } else {
                    ratingStars += '<input name="avgItemRating" type="radio" class="star {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                }
            }
            ratingStars += '</td></tr>';
            $("#tblAverageRating").append(ratingStars);
        },

        BindViewDetailsRatingInfo: function (itemRatingCriteriaId, itemRatingCriteria, ratingCriteriaAverage) {
            var ratingStarsDetailsInfo = '';
            var ratingTitle = [getLocale(AspxItemDetails, "Worst"), getLocale(AspxItemDetails, "Ugly"), getLocale(AspxItemDetails, "Bad"), getLocale(AspxItemDetails, "Not Bad"), getLocale(AspxItemDetails, "Average"), getLocale(AspxItemDetails, "OK"), getLocale(AspxItemDetails, "Nice"), getLocale(AspxItemDetails, "Good"), getLocale(AspxItemDetails, "Best"), getLocale(AspxItemDetails, "Excellent")]; var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
            var i = 0;
            ratingStarsDetailsInfo += '<div class="cssClassToolTipDetailInfo">';
            ratingStarsDetailsInfo += '<span class="cssClassCriteriaTitle">' + itemRatingCriteria + ': </span>';
            for (i = 0; i < 10; i++) {
                if (ratingCriteriaAverage == ratingText[i]) {
                    ratingStarsDetailsInfo += '<input name="avgItemDetailRating' + itemRatingCriteriaId + '" type="radio" class="star {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                } else {
                    ratingStarsDetailsInfo += '<input name="avgItemDetailRating' + itemRatingCriteriaId + '" type="radio" class="star {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                }
            }
            ratingStarsDetailsInfo += '</div>';
            $(".cssClassToolTipInfo").append(ratingStarsDetailsInfo);
        },

        BindPopUp: function () {

            ItemDetail.ClearReviewForm();
            $("#lblYourReviewing").html(getLocale(AspxItemDetails, "You're Reviewing:") + itemName + '');
            if (userName.toLowerCase() != "anonymouseuser") {
                $("#txtUserName").val(userName);
            }
            $.metadata.setType("attr", "validate");
            $('.auto-submit-star').rating({
                required: false,
                focus: function (value, link) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var tip = $('#hover-test' + ratingCriteria_id);
                    tip[0].data = tip[0].data || tip.html();
                    tip.html(link.title || 'value: ' + value);
                    $("#tblRatingCriteria label.error").hide();
                },
                blur: function (value, link) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var tip = $('#hover-test' + ratingCriteria_id);
                    tip.html('<span class="cssClassToolTip">' + tip[0].data || '' + '</span>');
                    $("#tblRatingCriteria label.error").hide();
                },

                callback: function (value, event) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var starRatingValues = $(this).attr("value");
                    var len = ratingCriteria_id.length;
                    var isAppend = true;
                    if (ratingValues != '') {
                        var stringSplit = ratingValues.split('#');
                        $.each(stringSplit, function (index, item) {
                            if (item.substring(0, item.indexOf('-')) == ratingCriteria_id) {
                                var index = ratingValues.indexOf(ratingCriteria_id + "-");
                                var toReplace = ratingValues.substr(index, 2 + len);
                                ratingValues = ratingValues.replace(toReplace, ratingCriteria_id + "-" + value);
                                isAppend = false;
                            }
                        });
                        if (isAppend) {
                            ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                        }
                    } else {
                        ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                    }
                }
            });
        },

        BindRatingCriteria: function () {
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), isFlag: false });
            this.config.method = "AspxCoreHandler.ashx/GetItemRatingCriteria";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.BindItemRatingCriteria;
            this.ajaxCall(this.config);
        },

        RatingCriteria: function (item) {
            var ratingCriteria = '';
            ratingCriteria += '<tr><td class="cssClassReviewCriteria"><label class="cssClassLabel">' + item.ItemRatingCriteria + ':<span class="cssClassRequired">*</span></label></td><td>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="1" title=' + getLocale(AspxItemDetails, "Worst") + ' validate="required:true" />';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="2" title="' + getLocale(AspxItemDetails, "Bad") + '"/>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="3" title="' + getLocale(AspxItemDetails, "OK") + '"/>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="4" title="' + getLocale(AspxItemDetails, "Good") + '"/>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="5" title="' + getLocale(AspxItemDetails, "Best") + '"/>';
            ratingCriteria += '<span id="hover-test' + item.ItemRatingCriteriaID + '" class="cssClassRatingText"></span>';
            ratingCriteria += '<label for="star' + item.ItemRatingCriteriaID + '" class="error">' + getLocale(AspxItemDetails, 'Please rate for') + '&nbsp;' + item.ItemRatingCriteria + '</label></td></tr>';
            $("#tblRatingCriteria").append(ratingCriteria);
        },

        ClearReviewForm: function () {
            $('.auto-submit-star').rating('drain');
            $('.auto-submit-star').removeAttr('checked');
            $('.auto-submit-star').rating('select', -1);

            $("#txtUserName").val('');
            $("#txtSummaryReview").val('');
            $("#txtReview").val('');
            $("label.error").hide();
        },

        SaveItemRatings: function () {
            var hasStars = 0;

            $("td.cssClassReviewCriteria").each(function (index) {
                $tParent = $(this).parent();
                $tStarRatingControl = $tParent.find('span.star-rating-control');
                $starRating = $tStarRatingControl.find('.star-rating');
                if ($starRating.hasClass('star-rating-on')) {
                    hasStars++;
                }
            });

            if (hasStars < 3) {
                csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Please Add stars in the missing rating field!") + "</p>");
                return false;
            }

            var statusId = 2;
            var ratingValue = ratingValues;
            var nickName = Encoder.htmlEncode($("#txtUserName").val());
            var summaryReview = Encoder.htmlEncode($("#txtSummaryReview").val());
            var review = Encoder.htmlEncode($("#txtReview").val());
            var ratingSaveObj = {
                ViewFromIP: userIP,
                viewFromCountry: countryName,
                UserName: nickName,
                ReviewSummary: summaryReview,
                Review: review,
                ItemRatingCriteria: ratingValue,
                ItemID: itemId,
                StatusID: statusId
            };
            var param = JSON2.stringify({ ratingSaveObj: ratingSaveObj, aspxCommonObj: aspxCommonObj() });
            this.config.method = "AspxCoreHandler.ashx/SaveItemRating";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.SaveItemRatingMsg;
            this.config.error = ItemDetail.GetItemRatingErrorMsg;
            this.ajaxCall(this.config);
        },

        ShowUsingPage: function () {
            $.metadata.setType("attr", "validate");
            var ControlName = "Modules/AspxCommerce/AspxReferToFriend/ReferAFriend.ascx";
            this.config.method = "LoadControlHandler.aspx/Result";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + ControlName + "'}";
            this.config.ajaxCallMode = ItemDetail.BindReferFriendPopup;
            this.ajaxCall(this.config);

        },

        BindItemBasicByitemSKU: function (itemSKU) {
            var checkparam = { itemSKU: itemSKU, aspxCommonObj: aspxCommonObj() };
            var checkdata = JSON2.stringify(checkparam);
            this.config.method = "AspxCoreHandler.ashx/GetItemBasicInfoByitemSKU";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = checkdata;
            this.config.async = false;
            this.config.ajaxCallMode = ItemDetail.BindItemBasicInfoByitemSKU;
            this.config.async = false;
            this.ajaxCall(this.config);

        },

        AddToCartToJS: function (itemId, itemPrice, itemSKU, itemQuantity) {
            AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity);
        },

        CheckItemQuantityInCart: function (itemId, itemCostVariantIDs) {
            ItemDetail.vars.itemQuantityInCart = 0;
            ItemDetail.vars.userItemQuantityInCart = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: aspxservicePath + 'AspxCoreHandler.ashx/CheckItemQuantityInCart',
                data: JSON2.stringify({ itemID: itemId, aspxCommonObj: aspxCommonObj(), itemCostVariantIDs: itemCostVariantIDs }),
                dataType: "json",
                success: function (data) { ItemDetail.vars.itemQuantityInCart = data.d.ItemQuantityInCart; ItemDetail.vars.userItemQuantityInCart = data.d.UserItemQuantityInCart; },
                error: function () { }
            });
            var itemsCartInfo = {
                ItemQuantityInCart: ItemDetail.vars.itemQuantityInCart,
                UserItemQuantityInCart: ItemDetail.vars.userItemQuantityInCart
            };
            return itemsCartInfo;
        },



        SetItemQuantityInCart: function (msg) {
            itemQuantityInCart = msg.d;
        },
        Kit: function () {


            var COMPONENTTYPE = { DROPDOWN: 1, RADIO: 2, CHECKBOX: 3 };
            var itemComponents = [];
            var configPrice = 0;
            var basePrice = 0;
            var baseWeight = 0;
            var weight = 0;
            var selectedKitNComponents = [];

            var kitItemInfo = {
                Description: '',
                Data: [],
                Price: 0,
                Weight: 0
            };
            getkitInfoForCart = function () {
                return kitItemInfo;
            };
            var $ajaxCall = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            get = function () {
                $ajaxCall("GetItemKits", JSON2.stringify({ itemID: parseInt(itemID), commonInfo: aspxCommonObj() }), function (data) {

                    if (data.d.length > 0) {
                        itemComponents = data.d;
                        buidItemComponetsUI();
                    }
                }, null);
            };

            getunique = function (x) {

                var unique = [];
                var distinct = [];
                for (var i in x) {

                    if (x[i].KitID == 0) {

                        if (x[i].KitComponentID != 0) {
                            unique.push(x[i].KitComponentID);
                        }

                        distinct.push(x[i]);
                    }
                }
                return distinct;
            }

            getItemKitsByComponent = function (id) {

                var len = itemComponents.length;
                var items = [];
                for (var z = 0; z < len; z++) {

                    if (itemComponents[z].KitID != 0 && itemComponents[z].KitComponentID == id)
                        items.push(itemComponents[z]);
                }
                return items;

            };

            buidItemComponetsUI = function () {
                $(".cssClassDwnWrapper").html('');
                $(".cssClassDwnWrapper").html("<div class='cssClassHeader'><h3>Kit Configuration</h3></div>");

                var tempComponents = getunique(itemComponents);
                tempComponents = tempComponents.sort(function (a, b) {
                    return a.KitComponentOrder - b.KitComponentOrder
                })
                var len = tempComponents.length;
                for (var z = 0; z < len; z++) {
                    var html = "";
                    switch (tempComponents[z].KitComponentType) {
                        case COMPONENTTYPE.CHECKBOX:
                            html = ui.CheckBox();
                            break;
                        case COMPONENTTYPE.RADIO:
                            html = ui.Radio();
                            break;
                        case COMPONENTTYPE.DROPDOWN:
                            html = ui.DropDown();
                            break;
                    }

                    var $elem = $(html);
                    $elem.find(".componentkit").data('item', tempComponents[z]);
                    $elem.find(".component label").html(tempComponents[z].KitComponentName);

                    var kts = getItemKitsByComponent(tempComponents[z].KitComponentID);
                    buildItemComponentKits($elem.find(".componentkit"), kts, tempComponents[z].KitComponentType, z)


                    $(".cssClassDwnWrapper").append($elem);

                }
                $(".currencyDropdown").currencyDropdown();
                $(".cssClassDwnWrapper").show();
                $(".componentkit select,.componentkit input[type=radio],.componentkit [type=checkbox]").trigger("change")
            }


            buildItemComponentKits = function ($appendTo, componentKits, componentType, index) {

                var len = componentKits.length;
                componentKits = componentKits.sort(function (a, b) {
                    return a.KitOrder - b.KitOrder
                });
                var $kitElem = "";
                switch (componentType) {
                    case COMPONENTTYPE.CHECKBOX:
                        $kitElem = $appendTo;
                        break;
                    case COMPONENTTYPE.RADIO:
                        $kitElem = $appendTo;
                        break;
                    case COMPONENTTYPE.DROPDOWN:
                        if (len > 0) {
                            $kitElem = $("<select class='currencyDropdown' id=" + componentKits[0].KitComponentID + "_" + componentType + " ></select>");
                            $appendTo.append($kitElem);

                        }
                        break;
                }
                for (var z = 0; z < len; z++) {

                    var kIt = componentKits[z];
                    var kitHtml = "";
                    switch (componentType) {
                        case COMPONENTTYPE.CHECKBOX:
                            if (kIt.IsDefault) {
                                kitHtml += "<label><input type='checkbox'  value=" + kIt.KitID + " checked='checked'  />" + kIt.KitName + "(<span class='cssClassFormatCurrency'>" + parseFloat(kIt.Price).toFixed(2) + "</span>)" + " </label>";
                            }
                            else {
                                kitHtml += "<label><input type='checkbox'  value=" + kIt.KitID + "  />" + kIt.KitName + "(<span class='cssClassFormatCurrency'>" + parseFloat(kIt.Price).toFixed(2) + "</span>)" + "</label>";
                            }
                            var $subkitElem = $(kitHtml);
                            $subkitElem.data('item', kIt);
                            $kitElem.append($subkitElem);
                            break;
                        case COMPONENTTYPE.RADIO:
                            if (kIt.IsDefault) {
                                kitHtml += "<label><input type='radio' name=" + index + kIt.KitComponentName.replace(' ', '_') + "_" + kIt.KitComponentID + " checked='checked' value=" + kIt.KitID + "  />" + kIt.KitName + "(<span class='cssClassFormatCurrency'>" + parseFloat(kIt.Price).toFixed(2) + "</span>)" + " </label>";
                            } else {
                                kitHtml += "<label><input type='radio' name=" + index + kIt.KitComponentName.replace(' ', '_') + "_" + kIt.KitComponentID + "  value=" + kIt.KitID + "  />" + kIt.KitName + "(<span class='cssClassFormatCurrency'>" + parseFloat(kIt.Price).toFixed(2) + "</span>)" + " </label>";

                            }
                            var $subkitElem = $(kitHtml);
                            $subkitElem.data('item', kIt);
                            $kitElem.append($subkitElem);
                            break;
                        case COMPONENTTYPE.DROPDOWN:

                            if (kIt.IsDefault) {
                                kitHtml += "<option selected='selected' value=" + kIt.KitID + " price=" + kIt.Price + " >" + kIt.KitName + "</option>";
                            } else {
                                kitHtml += "<option value=" + kIt.KitID + " price=" + kIt.Price + " >" + kIt.KitName + "</option>";
                            }
                            var $subkitElem = $(kitHtml);
                            $subkitElem.data('item', kIt);
                            $kitElem.append($subkitElem);
                            break;
                    }

                    $appendTo.append($kitElem);
                }


            }

            getSelectedTexts = function (selectedKitConfig) {

                var html = "";

                $.each(selectedKitConfig, function (index, item) {
                    if (index > 0) {
                        html += "</br>";
                    }
                    html += item.KitComponentName + " :-";

                    $.each(item.SelectedKits, function (idx, kit) {
                        if (idx > 0) {
                            html += "</br>";
                        }
                        html += kit.KitName;
                    });

                });
                return html;
            }
            var configWeight = 0;
            getUserSelection = function () {
                selectedKitNComponents = [];

                configPrice = 0;
                configWeight = 0;
                $(".cssClassComponentWrapper").each(function (index, item) {

                    var selectedKitNComponent = {};
                    var $container = $(this).find(".componentkit");
                    var component = $container.data('item');
                    selectedKitNComponent.KitComponentType = component.KitComponentType;
                    selectedKitNComponent.KitComponentID = component.KitComponentID;
                    selectedKitNComponent.KitComponentName = component.KitComponentName;
                    selectedKitNComponent.SelectedKits = [];
                    if (component.KitComponentType == 1) {

                        var $option = $container.find("select").find("option:selected");
                        if ($option.get(0) != undefined) {
                            var kitID = parseInt($option.val());
                            var kitInfo = $option.data('item');
                            configPrice += kitInfo.Price;
                            configWeight += kitInfo.Weight;
                            selectedKitNComponent.SelectedKits.push(kitInfo);
                        }
                    }
                    else if (component.KitComponentType == 2) {
                        var $option = $container.find("input[type=radio]:checked").parents("label:eq(0)");
                        if ($option.get(0) != undefined) {
                            var kitID = parseInt($container.find("input[type=radio]:checked").val());
                            var kitInfo = $option.data('item');
                            configPrice += kitInfo.Price;
                            configWeight += kitInfo.Weight;
                            selectedKitNComponent.SelectedKits.push(kitInfo);
                        }
                    } else if (component.KitComponentType == 3) {
                        $container.find("input[type=checkbox]:checked").each(function (i, checkbox) {
                            var kitInfo = $(this).parents("label:eq(0)").data('item');
                            configPrice += kitInfo.Price;
                            configWeight += kitInfo.Weight;
                            selectedKitNComponent.SelectedKits.push(kitInfo);
                        });
                    }
                    selectedKitNComponents.push(selectedKitNComponent);
                });

                return selectedKitNComponents;
            }

            ui = function () {

                dropdown = function () {
                    var html = "";
                    html += "<div class='cssClassComponentWrapper'><div class='component'><label>COMPONENTNAME</label></div><div class='componentkit typedropdown clearfix'> </div> </div>";
                    return html;
                };
                checkbox = function () {
                    var html = "";
                    html += "<div class='cssClassComponentWrapper'><div class='component'><label>COMPONENTNAME</label></div><div class='componentkit typecheckbox clearfix'> </div> </div>";
                    return html;
                }
                radio = function () {
                    var html = "";
                    html += "<div class='cssClassComponentWrapper'><div class='component'><label>COMPONENTNAME</label></div><div class='componentkit typeradio clearfix'> </div> </div>";
                    return html;
                }

                return { DropDown: dropdown, CheckBox: checkbox, Radio: radio };

            } ();

            UIEvent = function () {

                $("body").on("change", ".componentkit select,.componentkit input[type=radio],.componentkit [type=checkbox]", function () {

                    var selected = getUserSelection();
                    var total = parseFloat(configPrice) + parseFloat(basePrice);
                    $("#spanPrice").html('').html(parseFloat(total).toFixed(2));
                    var cookieCurrency = $("#ddlCurrency").val();
                    $("#spanPrice").attr('bc', parseFloat(total).toFixed(2));
                    $('#spanPrice').removeAttr('data-currency-' + cookieCurrency + '');
                    $('#spanPrice').removeAttr('data-currency');
                    Currency.currentCurrency = BaseCurrency;
                    Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                    kitItemInfo.Description = getSelectedTexts(selected);
                    kitItemInfo.Data = selectedKitNComponents;
                    kitItemInfo.Price = parseFloat(configPrice);
                    kitItemInfo.Weight = parseFloat(configWeight);
                });

            };
            init = function (_basePrice, _baseWeight) {

                $(".cssPriceDetailwrap .popbox").remove()
                $(".cssClassDwnWrapper").hide();
                $("#spanListPrice").remove();
                $(".cssClassYouSave").remove();
                $("#bulkDiscount").remove();
                $("#spanPrice").html('').html(parseFloat(_basePrice).toFixed(2));
                $("#spanPrice").attr('bc', parseFloat(_basePrice).toFixed(2));
                basePrice = _basePrice;
                baseWeight = _baseWeight;
                get();
                UIEvent();
            }
            return { Init: init, GetKitInfo: getkitInfoForCart };
        } (),

        GetGroupImageLists: function (sku, combinationId) {
            this.config.method = "AspxCoreHandler.ashx/GetItemsImageGalleryInfoBySKU";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ itemSKU: sku, aspxCommonObj: aspxCommonObj(), combinationId: combinationId });
            this.config.ajaxCallMode = ItemDetail.BindItemsImageGallery;
            this.ajaxCall(this.config);
        },
        GetGroupedItems: function (SKU) {
            this.config.method = "AspxCoreHandler.ashx/GetGroupedItemsByGroupSKU";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ groupSKU: SKU, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = ItemDetail.BindGroupItems;
            this.ajaxCall(this.config);
        },

        GetStartPriceOfGroupAfterDeletion: function () {
            var itemIDs = '';
            $(".delItemFrmGrp").each(function (index, item) {
                itemIDs += $(this).data().itemid + ',';
            });

            this.config.method = "AspxCoreHandler.ashx/GetStartPriceOfGroupAfterDeletion";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ groupItems: itemIDs, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = ItemDetail.BindStartingPriceAfterDel;
            this.ajaxCall(this.config);
        },
        BindStartingPriceAfterDel: function (msg) {
            if (msg.d.length > 0) {
                $("#spanPrice").html(parseFloat(msg.d).toFixed(2));
                $("#spanPrice").attr('bc', parseFloat(msg.d).toFixed(2));
            }
        },

        ForceNumericInput: function (defaultQuantityInGroup) {
            $(".cssClassItemQty").keydown(function (e) {
                if ($.inArray(e.keyCode, [8, 9, 27, 13, 110]) !== -1 ||
         (e.keyCode == 65 && e.ctrlKey === true) ||
         (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
                if (e.shiftKey == 190) {
                    e.preventDefault();
                    return;
                }
                if (e.keyCode == 96) {
                    if ($(this).val() == 0) {
                        $(this).val($(this).data().minitemincart);
                        e.preventDefault();
                    }
                }
                if (e.keyCode == 8) {
                    if (($(this).val() > 0) && ($(this).val() < 9)) {
                        $(this).val($(this).data().minitemincart);
                        e.preventDefault();
                    }
                }

            });
        },
        BindGroupItemsEvents: function () {
            $(".tblGroupedItem >tbody > tr").each(function () {
                if ($(this).hasClass('disabledGroupItm')) {
                    $(this).find("input,textbox").prop("disabled", "disabled");
                }
            });

        },
        BindGroupItems: function (msg) {
            if (msg.d.length > 0) {
                var htmlTable = '';
                htmlTable += '<table class="tblGroupedItem"><thead>';
                htmlTable += '<tr><th></th><th></th><th>' + getLocale(AspxItemDetails, 'Item Name') + '</th>';
                htmlTable += '<th>' + getLocale(AspxItemDetails, 'Price') + '</th>';
                htmlTable += '<th>' + getLocale(AspxItemDetails, 'Qty') + '</th>';
                htmlTable += '</tr>';
                htmlTable += '</thead>';
                var defaultQuantityInGroup = 1;
                htmlTable += '<tbody>';
                $.each(msg.d, function (index, item) {
                    Imagelist += item.BaseImage + ';';
                });
                ItemDetail.ResizeImageDynamically(Imagelist, "Small", "Item");
                $.each(msg.d, function (index, item) {
                    if (item.IsOutOfStock == 0) {
                        htmlTable += '<tr>';
                    }
                    else {
                        htmlTable += '<tr class="disabledGroupItm">';
                    }
                    var imagePath = itemImagePath + item.BaseImage;
                    if (item.BaseImage == "") {
                        imagePath = noItemDetailImagePath;
                    } else if (item.AlternateText == "") {
                        item.AlternateText = item.Name;
                    }
                    htmlTable += '<td><input data-MinItemInCart="' + item.MinCartQuantity + '" data-MaxItemInCart="' + item.MaxCartQuantity + '" data-price="' + item.Price + '"data-SKU="' + item.SKU + '" data-Name="' + item.Name + '" data-ItemID="' + item.ItemID + '" id="chk' + item.ItemID + '"type="checkBox"></td>'
                    htmlTable += '<td><p class="cssClassitemDetailPicture">';
                    htmlTable += '<img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + item.AlternateText + '" title="' + item.AlternateText + '"></p></td>';
                    htmlTable += '<td>'
                    htmlTable += '<div class="cssClassCartPictureInformation">';
                    htmlTable += '<h3>';
                    htmlTable += '<a href="' + itemPagePath + item.SKU + pageExtension + '">' + item.Name + '</a></h3></div>';
                    htmlTable += '</td>';
                    htmlTable += '<td><span id="spanPrice" class="cssClassFormatCurrency">' + parseFloat(item.Price).toFixed(2) + '</span></td>';
                    htmlTable += '<td><input type="text" class="cssClassItemQty" data-ItemSKU="' + item.SKU + '" data-ItemID="' + item.ItemID + '" data-ActualQty="' + item.Quantity + '" id="txt' + item.ItemID + '" value="' + item.MinCartQuantity + '"data-MinItemInCart="' + item.MinCartQuantity + '"\><label class="lblNotification" style="color: #FF0000;"></label></td>';
                    htmlTable += '</tr>';

                });
                htmlTable += '</tbody>';
                htmlTable += '</table>';
                $(".cssClassGroupedItems").remove();
                $(".cssPriceDetailwrap").append("<div class='cssClassGroupedItems'>" + htmlTable + "</div>");
                ItemDetail.ForceNumericInput(defaultQuantityInGroup);
                ItemDetail.BindGroupItemsEvents();
            }
            else {
                $("#spanPrice").remove();
                $(".detailButtonWrapper").remove();
                $(".cssClassProductRealPrice").remove();
                $(".cssClassItemCategoriesHeading").remove();
                $(".cssPriceDetailwrap").append("<div class='cssClassGroupedItems'><p>" + getLocale(AspxItemDetails, "No grouped Item found.") + "</p></div>");
            }

        },
        GroupProductVars: {
            ItemAddedOnCart: 0

        },
        InsertGrpItemsToCart: function (itemIDs, itemQtys, itemPrices, itemSkus) {
            var itemsCartInfo = {
                CartItemIDs: itemIDs,
                CartItemQtys: itemQtys,
                CartItemPrices: itemPrices,
                CartItemSKUs: itemSkus
            };
            this.config.method = "AspxCommonHandler.ashx/AddGroupItemsToCart";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                itemCartObj: itemsCartInfo,
                aspxCommonObj: aspxCommonObj()
            });
            this.config.ajaxCallMode = ItemDetail.RefreshGroupItems;
            this.ajaxCall(this.config);

        },
        RefreshGroupItems: function (msg) {
            if (msg.d.length > 0) {
                if (allowRealTimeNotifications.toLowerCase() == 'true') {
                    try {
                        var groupObj = [];
                        $.each(msg.d, function (index, item) {
                            groupObj.push({ CartItemIDs: item.ItemID, CartItemSkus: item.ItemSKU, CartItemReturnVals: item.RetValue });
                        });
                        var itemOnCart = $.connection._aspxrthub;
                        itemOnCart.server.updateItemStockFromItemDetails(groupObj, AspxCommerce.AspxCommonObj());

                    }
                    catch (Exception) {
                        console.log(getLocale(AspxItemDetails, 'Error Connecting Hub.'));
                    }
                }

                if ($("#lnkMyCart").length > 0) {
                    HeaderControl.GetCartItemTotalCount();
                }

                if ($("#lnkShoppingBag").length > 0) {
                    ShopingBag.GetCartItemCount();

                }
                var addToCartProperties = {
                    onComplete: function (e) {
                        if (e) {
                            window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartURL + pageExtension;
                        }
                    }
                };
                csscody.addToCart("<h2>" + getLocale(AspxItemDetails, "Successful Message") + "</h2><p>" + getLocale(AspxItemDetails, 'The product has been successfully added to cart.') + "</p>", addToCartProperties);
                ItemDetail.BindItemBasicByitemSKU(itemSKU);
            }
        },

        GetItemQuantityOfGroupInCart: function () {
            var groupedItemId = '';
            var itemCostVariant = '';
            var itemCostVariantIDs = '';
            var counter = 0;
            $(".tblGroupedItem >tbody >tr").each(function () {
                var $objCheckBox = $(this).find('input[type="checkbox"]');
                if ($objCheckBox.prop("checked")) {
                    counter++;
                    var ItemID = parseInt($objCheckBox.data().itemid);
                    groupedItemId += ItemID + ',';
                    itemCostVariantIDs += itemCostVariant + '0@' + ',';
                }
            });
            if (counter <= 0) {
                return 0;
            }
            this.config.method = "AspxCoreHandler.ashx/GetGroupItemQuantityInCart";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ itemIDs: groupedItemId, aspxCommonObj: aspxCommonObj(), itemCostVariantIDs: itemCostVariantIDs });
            this.config.ajaxCallMode = ItemDetail.SetGroupItemQuantityInCart;
            this.ajaxCall(this.config);
            return groupItemQty;
        },

        SetGroupItemQuantityInCart: function (msg) {
            groupItemQty = [];
            $.each(msg.d, function (index, item) {
                groupItemQty.push({
                    itemID: item.ItemID,
                    qty: item.Qty,
                    userQty: item.UserQty
                });
            });

        },
        AddGroupItemsToCart: function () {
            var addGrpItmsToCart = true;
            var checkCounter = 0;
            var myQtyArray = ItemDetail.GetItemQuantityOfGroupInCart();
            $(".tblGroupedItem >tbody >tr").each(function () {
                if ($(this).html() == "" || $(this).html().length <= 0) {
                    addGrpItmsToCart = false;
                    return false;
                }
                var $objCheckBox = $(this).find('input[type="checkbox"]');
                if ($objCheckBox.prop("checked")) {
                    checkCounter++;
                    if ($(this).find('input[type="text"]').val() == "") {
                        $(this).find('.lblNotification:eq(0)').html(getLocale(AspxItemDetails, 'Quantity cannot be empty.'));
                        addGrpItmsToCart = false;
                        return false;
                    }
                    var totQtyInTxtBox = parseInt($.trim($(this).find('input[type="text"]').val()));
                    var actualQty = parseInt($(this).find('input[type="text"]').data().actualqty);
                    var ItemID = $objCheckBox.data().itemid;
                    var ItemSKU = $objCheckBox.data().sku;
                    var ItemName = $objCheckBox.data().name;
                    var minCartQuantity = $objCheckBox.data().minitemincart;
                    var maxCartQuantity = $objCheckBox.data().maxitemincart;
                    var itemCostVariantIDs = '';
                    var itemObj = $.grep(myQtyArray, function (e) { return e.itemID == ItemID; });
                    var itemQuantityInCart = itemObj[0].qty;
                    var userItemQuantityInCart = itemObj[0].userQty;
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if (parseInt(totQtyInTxtBox + userItemQuantityInCart) < minCartQuantity) {
                            $(this).find('.lblNotification:eq(0)').html(getLocale(AspxItemDetails, 'You cannot add less than' + minCartQuantity + 'quantity of this item in cart'));
                            addGrpItmsToCart = false;
                            return false;
                        }
                        else if (parseInt(totQtyInTxtBox + userItemQuantityInCart) > maxCartQuantity) {
                            $(this).find('.lblNotification:eq(0)').html(getLocale(AspxItemDetails, 'You have reached limit of this item in cart'));
                            addGrpItmsToCart = false;
                            return false;
                        }
                        else if (parseInt(totQtyInTxtBox + itemQuantityInCart) > actualQty) {
                            $(this).find('.lblNotification:eq(0)').html(getLocale(AspxItemDetails, 'The Quantity Is Greater Than The Available Quantity.'));
                            addGrpItmsToCart = false;
                            return false;
                        }
                        else {
                            $(this).find('.lblNotification:eq(0)').html('');
                        }
                    } else {
                        $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                        addGrpItmsToCart = true;
                    }
                }

            });
            if (checkCounter < 1) {
                csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Message") + "</h2><p>" + getLocale(AspxItemDetails, 'Please check at least 1 item before adding into cart!') + "</p>");
                addGrpItmsToCart = false;
                return false;
            };

            if (addGrpItmsToCart == true) {
                var groupedItemId = '';
                var groupedItemPrice = '';
                var groupedItemquantity = '';
                var groupedItemSkus = '';
                $(".tblGroupedItem >tbody >tr").each(function () {
                    var $objCheckBox = $(this).find('input[type="checkbox"]');
                    if ($objCheckBox.prop("checked")) {
                        var ItemID = parseInt($objCheckBox.data().itemid);
                        var ItemPrice = $objCheckBox.data().price;
                        var ItemSKU = $objCheckBox.data().sku;
                        var ItemQuantity = $(this).find('input[type="text"]').val();
                        groupedItemId += ItemID + ',';
                        groupedItemPrice += ItemPrice + ',';
                        groupedItemquantity += ItemQuantity + ',';
                        groupedItemSkus += ItemSKU + ',';
                    }
                });
                ItemDetail.InsertGrpItemsToCart(groupedItemId, groupedItemquantity, groupedItemPrice, groupedItemSkus);
            }
            else {
                csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Message") + "</h2><p>" + getLocale(AspxItemDetails, 'The product items contain invalid quantity!') + "</p>");
            };

        },
        BindItemsBasicInfo: function (item) {
            ItemDetail.info.IsCombinationMatched = true;
            ItemDetail.info.IsPricePercentage = false;
            ItemDetail.info.IsWeightPercentage = false;
            ItemDetail.info.PriceModifier = 0;
            ItemDetail.info.WeightModifier = 0;
            ItemDetail.info.Quantity = item.Quantity;
            ItemDetail.info.MinCartQuantity = item.MinCartQuantity;
            ItemDetail.info.MaxCartQuantity = item.MaxCartQuantity;
            if (item.ItemViewCount == null) {
                item.ItemViewCount = 1;
            }
            var wishData = item.ItemID + "," + "'" + item.SKU + "'" + "," + "this";
            $("#addWishListThis").val(wishData);
            $("#addCompareListThis").val(wishData);
            $("#viewCount").text(item.ItemViewCount);
            var itemCategories = item.ItemCategories.split("#");
            if (itemCategories.length > 0) {
                $(".cssItemCategories").show();
                var catHtml = "";
                catHtml += "<ul>";
                $.each(itemCategories, function (index, value) {
                    var hrefCategory = aspxRedirectPath + "category/" + fixedEncodeURIComponent(value) + pageExtension;
                    if (index == itemCategories.length - 1) {
                        catHtml += "<li><a href='" + hrefCategory + "'>" + value + "</a></li>";
                    } else {
                        catHtml += "<li><a href='" + hrefCategory + "'>" + value + "</a>,&nbsp;</li>";
                    }
                });
                catHtml += "</ul>";
                $('.cssClassCategoriesName').html('').append(catHtml);

            } else {
                $(".cssItemCategories").hide();
            }
            ItemDetail.config.async = true;
            if (itemTypeId == 3) {
                ItemDetail.BindGiftCardInfo(item);

            } else {
                if (item.ListPrice != "") {
                    $("#" + lblListPrice).show();
                    $("#spanListPrice").html(parseFloat(item.ListPrice).toFixed(2));
                    $("#hdnListPrice").val(item.ListPrice);
                }
                else {
                    $("#spanListPrice").remove();
                }

                snippet.Name = itemName;
                snippet.Price = parseFloat(item.Price).toFixed(2);
                snippet.IsOutOfStock = item.IsOutOfStock;
                snippet.Brand = item.BrandName;
                snippet.Description = item.Description;
                ItemDetail.CreateSnippet();
                $("#spanItemName").html(itemName);
                $("#spanSKU").html(item.SKU);
                $("#spanPrice").html(parseFloat(item.Price).toFixed(2));
                $("#spanPrice").attr('bc', parseFloat(item.Price).toFixed(2));
                $("#hdnPrice").val(item.Price);
                $("#hdnWeight").val(item.Weight);
                $("#hdnQuantity").val(item.Quantity);
                if (allowAddToCart.toLowerCase() == 'true') {
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if (item.IsOutOfStock) {
                            $("#txtQty").removeAttr('enabled').prop("disabled", "disabled");
                            $("#btnAddToMyCart span").html(getLocale(AspxItemDetails, 'Out Of Stock'));
                            $("#btnAddToMyCart").prop("disabled", "disabled");
                            $("#btnAddToMyCart").removeClass('addtoCart ');
                            $("#btnAddToMyCart").addClass('cssClassOutOfStock');
                            $("#btnAddToMyCart").parent('label').removeClass('i-cart cssClassCartLabel cssClassGreenBtn');
                            $("#btnAddToMyCart").show();
                            $("#btnAddToMyCart").removeAttr('style');
                            $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'Out Of Stock') + '</b>');
                            $("#spanAvailability").addClass('cssOutOfStock');
                            $("#spanAvailability").removeClass('cssInStock');
                            if (userName != "anonymoususer") {
                                $("#Notify").show();
                                $("#Notify #txtNotifiy").hide();
                            }
                            else {
                                $("#Notify").show();
                                $("#txtNotifiy").show();
                            }
                        } else {
                            $("#txtQty").removeAttr('disabled').prop("enabled", "enabled");
                            $("#btnAddToMyCart span").html(getLocale(AspxItemDetails, 'Cart +'));
                            $("#btnAddToMyCart").removeAttr("disabled");
                            $("#btnAddToMyCart").removeClass('cssClassOutOfStock');
                            $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
                            $("#btnAddToMyCart").addClass('addtoCart ');
                            $("#btnAddToMyCart").show();
                            $("#btnAddToMyCart").removeAttr('style');
                            $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>');
                            $("#spanAvailability").removeClass('cssOutOfStock');
                            $("#spanAvailability").addClass('cssInStock');
                            $("#Notify").hide();
                        }
                    }
                    else {
                        $("#btnAddToMyCart").show();
                        $("#btnAddToMyCart").removeAttr('style');
                    }
                }
                else {
                    $("#btnAddToMyCart").parent('label').remove();
                }
                $("#txtQty").val(ItemDetail.info.MinCartQuantity < 0 ? 1 : ItemDetail.info.MinCartQuantity);
                $("#txtQty").attr('addedValue', ItemDetail.info.MinCartQuantity < 0 ? 1 : ItemDetail.info.MinCartQuantity);
                $("#hdnTaxRateValue").val(item.TaxRateValue);
                if (item.SampleLink != '' && item.SampleFile != '') {
                    $("#dwnlDiv").show();
                    $(".cssClassDwnWrapper").show();
                    $("#spanDownloadLink").html(item.SampleLink);
                    $("#spanDownloadLink").attr("href", item.SampleFile);

                } else {
                    $("#dwnlDiv").hide();
                    $(".cssClassDwnWrapper").hide();
                }
                if (item.ListPrice != "") {
                    $(".cssClassYouSave").show();
                    var savingPercent = ((item.ListPrice - item.Price) / item.ListPrice) * 100;
                    savingPercent = savingPercent.toFixed(2);
                    $("#spanSaving").html('<b>' + savingPercent + '%</b>');
                }


                if (itemTypeId == 5) {
                    ItemDetail.GetGroupImageLists(item.SKU, '');
                    ItemDetail.GetGroupedItems(item.SKU);
                    $(".cssQtywrapper").remove();
                    $(".cssClassYouSave").remove();
                    $("#spanListPrice").remove();
                    $(".cssClassAvailiability").remove();
                    $("#bulkDiscount").remove();
                    $("#btnAddToMyCart").attr({ onClick: "ItemDetail.AddGroupItemsToCart();" });
                    if ($(".StartingAt").length <= 0) {
                        $("#spanPrice").wrap('<div class="StartingAt"><p>' + getLocale(AspxItemDetails, 'Starting At') + '</p></div>');
                    }
                    $("#spanPrice").html('').html(parseFloat(item.Price).toFixed(2));
                    $("#spanPrice").attr('bc', parseFloat(item.Price).toFixed(2));

                }

                if (itemTypeId == 6) {
                    ItemDetail.Kit.Init(item.Price, item.Weight);
                }

                var shortDesc = '';
                if (item.ShortDescription.length > 870) {
                    shortDesc = item.ShortDescription.substring(0, 870);
                    shortDesc += " >>>";
                } else {
                    shortDesc = item.ShortDescription;
                }
                $('.cssClassProductInformation').show();
                $('.cssClassItemQuickOverview').show();
                $("#divItemShortDesc").html(Encoder.htmlDecode(shortDesc));

                if (item.BrandID != null && item.BrandID != "" && item.BrandID != 0) {
                    ItemDetail.BindBrandDetail(item.BrandName, item.BrandDescription, item.BrandImageUrl);
                }
                else {
                    $(".itemBrand").remove();
                }
                $("#dynItemDetailsForm").show();
            }
            var cookieCurrency = $("#ddlCurrency").val();
            $('#spanPrice').removeAttr('data-currency-' + cookieCurrency + '');
            $('#spanPrice').removeAttr('data-currency-' + cookieCurrency + '');
            $('#spanPrice').removeAttr('data-currency');
            Currency.currentCurrency = BaseCurrency;
            Currency.convertAll(Currency.currentCurrency, cookieCurrency);
        },

        BindGiftCardInfo: function (item) {
            snippet.Name = itemName;
            snippet.Price = parseFloat(item.Price).toFixed(2);
            snippet.IsOutOfStock = item.IsOutOfStock;
            snippet.Brand = item.BrandName;
            snippet.Description = item.Description;
            ItemDetail.CreateSnippet();
            $("#spanItemName").html(item.Name);
            $("#spanSKU").html(item.SKU);
            $("#spanPrice").html(parseFloat(item.Price).toFixed(2));
            $("#spanPrice").attr('bc', parseFloat(item.Price).toFixed(2));
            var price = parseFloat(item.Price).toFixed(2);
            $("#hdnPrice").val(item.Price);
            $("#hdnWeight").val(item.Weight);

            $(".cssClassDwnWrapper").html('');
            $(".cssClassYouSave").remove();
            $(".cssClassProductOffPrice").remove();
            $(".cssItemCategories").hide();
            $("#btnAddToMyCart span").html('Cart +');
            $("#btnAddToMyCart").removeAttr("disabled");
            $("#btnAddToMyCart").removeClass('cssClassOutOfStock');
            $("#btnAddToMyCart").parent('label').addClass('i-cart cssClassCartLabel cssClassGreenBtn');
            $("#btnAddToMyCart").addClass('addtoCart ');
            $(".cssClssQTY").hide();
            if (allowAddToCart.toLowerCase() == 'true') {
                $("#btnAddToMyCart").show();
                $("#btnAddToMyCart").removeAttr('style');
            }
            else {
                $("#btnAddToMyCart").parent('label').remove();
            }
            $("#spanAvailability").html('<b>' + getLocale(AspxItemDetails, 'In stock') + '</b>');
            $("#spanAvailability").removeClass('cssOutOfStock');
            $("#spanAvailability").addClass('cssInStock');

            var $li = '<div id="divCostVariant" class="sfFormwrapper"></div><table >'; $li += '<tr ><td>  <span>' + getLocale(AspxItemDetails, "Sender Name:") + '<em>*</em></span></td>';
            $li += '<td> <input id ="txtgc_senerName" type="text" minlength="2" messages="*" class="sfTextBoxSmall" name="gcsender_name"> </td></tr>';
            $li += ' <tr ><td> <span>' + getLocale(AspxItemDetails, "Sender Email:") + '<em>*</em></span></td>';
            $li += ' <td><input type="text" value="" class="sfTextBoxSmall" name="gcsender_email" id="txtgc_senerEmail"> </td></tr> ';
            $li += '<tr><td><span >' + getLocale(AspxItemDetails, "Recipient Name:") + '<em>*</em></span></td>';
            $li += ' <td><input type="text" minlength="2" name="gcrecipient_name" id="txtgc_recieverName"> </td></tr>';
            $li += '<tr> <td><span >' + getLocale(AspxItemDetails, "Recipient Email:") + '<em>*</em></span></td> ';
            $li += '<td><input type="text" class="sfTextBoxSmall" name="gcrecipient_email" id="txtgc_recieverEmail"> </td></tr>';
            $li += '<tr> <td><span >' + getLocale(AspxItemDetails, "Message:") + '</span> </td>';
            $li += ' <td><textarea rows="3" class="sfTextBoxSmall"  cols="5" style="width: 135px; height: 50px;" id="txtgc_messege" name="gcmessage"></textarea> </td> </tr>';
            $li += '<tr><td><span>' + getLocale(AspxItemDetails, "Send gift by:") + '</span></td><td id="GiftCardTypes"></td></tr></table> ';
            $(".cssClassDwnWrapper").html($li);

            $('.cssClassProductInformation').show();
            ItemDetail.GetGiftCardTypes();


        },
        GetGiftCardTypes: function () {
            this.config.method = "AspxCoreHandler.ashx/GetGiftCardTypes";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = function (data) {

                $("#GiftCardTypes").html('');
                $.each(data.d.reverse(), function (i, item) {
                    var x = $('<label>').append($("<input type=radio />").attr('name', 'giftcard-type').attr('value', item.TypeId)).append(item.Type.toLowerCase());

                    $("#GiftCardTypes").append(x);

                });
                $("input[name=giftcard-type]:first").prop('checked', 'checked');
            };
            this.ajaxCall(this.config);
        },

        BindBrandDetail: function (BrandName, BrandDescription, BrandImageUrl) {
            ItemDetail.ResizeImageDynamically(BrandImageUrl, "Small", "Brand");
            var html = '<h3><label>' + getLocale(AspxItemDetails, "Brand Information") + '</label></h3>';
            var shortDesc = '';
            var brandUrl = aspxRedirectPath + "brand/" + fixedEncodeURIComponent(BrandName) + pageExtension;
            if (BrandDescription.length > 250) {
                shortDesc = BrandDescription.substring(0, 250);
                shortDesc += ".....!";
            } else {
                shortDesc = Encoder.htmlDecode(BrandDescription);
            }
            html += "<p><a href='" + brandUrl + "'>";
            html += "<img id=\"imgBrand\" src='" + aspxRootPath + BrandImageUrl.replace("uploads", "uploads/Small") + "'/></a>";
            html += "</p><a href='" + brandUrl + "'>" + getLocale(AspxItemDetails, 'View all products under this brand') + "</a>";
            $(".itemBrand").html('').append(html);

        },
        AddItemsToMyCompare: function (itemId) {
            var itemCostVariantIDs = [];
            if ($('#divCostVariant').is(':empty')) {
                itemCostVariantIDs.push(0);
            } else {
                $("#divCostVariant select option:selected").each(function () {

                    if ($(this).val() != 0) {
                        itemCostVariantIDs.push($(this).val());
                    } else {
                    }
                });
                $("#divCostVariant input[type=radio]:checked").each(function () {
                    if ($(this).prop("checked", "checked")) {
                        if ($(this).val() != 0) {
                            itemCostVariantIDs.push($(this).val());
                        }
                    }
                });

                $("#divCostVariant input[type=checkbox]:checked").each(function () {
                    if ($(this).prop("checked", "checked")) {
                        if ($(this).val() != 0) {
                            itemCostVariantIDs.push($(this).val());
                        }
                    }
                });
            }
            if (ItemDetail.info.IsCombinationMatched) {
                ItemsCompare.AddToCompareFromDetails(itemId, itemCostVariantIDs.join('@'));
            }
            else {
                csscody.alert('<h2>' + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'Please choose available variants!') + '</p>');
            }
        },

        AddToMyCart: function () {
            if (itemTypeId == 3) {
                var giftCardDetail = {
                    Price: $("#hdnPrice").val(),
                    GiftCardTypeId: parseFloat($("input[name=giftcard-type]:checked").val()),
                    GiftCardCode: '',
                    GraphicThemeId: parseFloat($(".jcarousel-skin ul li a.selected").attr('data-id')),
                    SenderName: $.trim($("#txtgc_senerName").val()),
                    SenderEmail: $.trim($("#txtgc_senerEmail").val()),
                    RecipientName: $.trim($("#txtgc_recieverName").val()),
                    RecipientEmail: $.trim($("#txtgc_recieverEmail").val()),
                    Messege: $.trim($("#txtgc_messege").val())
                };


                if (parseFloat($("input[name=giftcard-type]:checked").val()) == 2) {

                } else {
                    if ($.trim($("#txtgc_recieverName").val()) == "" ||
                        $.trim($("#txtgc_recieverEmail").val()) == "") {

                        csscody.alert('<h2>' + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'Please fill valid required data!') + '</p>');
                        return false;

                    } else {
                        if (!/^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/.test(giftCardDetail.RecipientEmail)) {
                            csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Please fill valid email address!") + "</p>");
                            return false;
                        }

                    }
                }

                if (giftCardDetail.SenderName != "" || giftCardDetail.SenderEmail != "") {
                    if (!/^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/.test(giftCardDetail.SenderEmail)) {
                        csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Please fill valid email address!") + "</p>");
                        return false;
                    }
                    var AddItemToCartObj = {
                        ItemID: itemId,
                        Price: $("#hdnPrice").val(),
                        Weight: 0,
                        Quantity: 1,
                        CostVariantIDs: '0@',
                        IsGiftCard: true,
                        IsKitItem: false
                    };
                    var paramz = {
                        aspxCommonObj: aspxCommonObj(),
                        AddItemToCartObj: AddItemToCartObj,
                        giftCardDetail: giftCardDetail,
                        kitInfo: {}
                    };
                    var dataz = JSON2.stringify(paramz);
                    this.config.method = "AspxCommonHandler.ashx/AddItemstoCartFromDetail";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = dataz;
                    this.config.ajaxCallMode = ItemDetail.AddItemstoCartFromDetail;
                    this.config.oncomplete = 20;
                    this.config.error = ItemDetail.GetAddToCartErrorMsg;
                    this.ajaxCall(this.config);
                }
                else {
                    csscody.alert('<h2>' + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Please fill valid required data!") + "</p>");
                    return false;
                }
            } else if (itemTypeId == 6) {
                var kitinfo = ItemDetail.Kit.GetKitInfo();
                if (kitinfo.Data.length == 0) {
                    csscody.alert('<h2>' + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Please choose valid configuration!") + "</p>");
                    return false;
                }
                var AddItemToCartObj = {
                    ItemID: itemId,
                    Price: kitinfo.Price,
                    Weight: kitinfo.Weight,
                    Quantity: $.trim($("#txtQty").val()),
                    CostVariantIDs: '0@',
                    IsGiftCard: false,
                    IsKitItem: true
                };
                var paramz = {
                    aspxCommonObj: aspxCommonObj(),
                    AddItemToCartObj: AddItemToCartObj,
                    giftCardDetail: {},
                    kitInfo: kitinfo
                };
                var itemsCartInfo = ItemDetail.CheckItemQuantityInCart(itemId, '0@');
                var itemQuantityInCart = itemsCartInfo.ItemQuantityInCart;
                var userItemQuantityInCart = itemsCartInfo.UserItemQuantityInCart;

                if (itemQuantityInCart != -1) {
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            if ((eval($("#txtQty").val()) + eval(userItemQuantityInCart)) < eval(ItemDetail.info.MinCartQuantity)) {
                                csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'The requested quantity for this item is not valid') + "</p>");
                                return false;
                            }
                            else if ((eval($("#txtQty").val()) + eval(userItemQuantityInCart)) > eval(ItemDetail.info.MaxCartQuantity)) {
                                csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'The requested quantity for this item is not valid') + "</p>");
                                return false;
                            }
                            else if (ItemDetail.info.Quantity <= 0) {
                                csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'This product is currently Out Of Stock!') + "</p>");
                                return false;
                            } else {
                                if ((eval($.trim($("#txtQty").val())) + eval(itemQuantityInCart)) > eval(ItemDetail.info.Quantity)) {
                                    csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'This product is currently Out Of Stock!') + "</p>");
                                    return false;
                                }
                            }
                        }
                    }
                    else {
                        $("#btnAddToMyCart").parent('label').remove();
                    }
                }
                var dataz = JSON2.stringify(paramz);
                this.config.method = "AspxCommonHandler.ashx/AddItemstoCartFromDetail";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = dataz;
                this.config.ajaxCallMode = ItemDetail.AddItemstoCartFromDetail;
                this.config.oncomplete = 20;
                this.config.error = ItemDetail.GetAddToCartErrorMsg;
                this.ajaxCall(this.config);
            }
            else {

                if (ItemDetail.info.IsCombinationMatched) {
                    if ($.trim($("#txtQty").val()) == "" || $.trim($("#txtQty").val()) <= 0) {
                        csscody.alert('<h2>' + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'Invalid quantity.') + '</p>');
                        return false;
                    }
                    var itemPrice = $("#hdnPrice").val();
                    var itemQuantity = $.trim($("#txtQty").val());
                    var itemCostVariantIDs = [];
                    var weightWithVariant = 0;
                    var totalWeightVariant = 0;
                    var costVariantPrice = 0;
                    if ($('#divCostVariant').is(':empty')) {
                        itemCostVariantIDs.push(0);
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
                            } else { }
                        });

                    }
                    var itemsCartInfo = ItemDetail.CheckItemQuantityInCart(itemId, itemCostVariantIDs.join('@') + '@');
                    var itemQuantityInCart = itemsCartInfo.ItemQuantityInCart;
                    var userItemQuantityInCart = itemsCartInfo.UserItemQuantityInCart;

                    if (itemQuantityInCart != -1) {
                        if (allowAddToCart.toLowerCase() == 'true') {
                            if (allowOutStockPurchase.toLowerCase() == 'false') {
                                if ((eval($("#txtQty").val()) + eval(userItemQuantityInCart)) < eval(ItemDetail.info.MinCartQuantity)) {
                                    csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'The requested quantity for this item is not valid') + "</p>");
                                    return false;
                                }
                                else if ((eval($("#txtQty").val()) + eval(userItemQuantityInCart)) > eval(ItemDetail.info.MaxCartQuantity)) {
                                    csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'The requested quantity for this item is not valid') + "</p>");
                                    return false;
                                }
                                else if (ItemDetail.info.Quantity <= 0) {
                                    csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'This product is currently Out Of Stock!') + "</p>");
                                    return false;
                                } else {
                                    if ((eval($.trim($("#txtQty").val())) + eval(itemQuantityInCart)) > eval(ItemDetail.info.Quantity)) {
                                        csscody.alert("<h2>" + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'This product is currently Out Of Stock!') + "</p>");
                                        return false;
                                    }
                                }
                            }
                        }
                        else {
                            $("#btnAddToMyCart").parent('label').remove();
                        }
                    }
                    if (ItemDetail.info.IsPricePercentage) {
                        costVariantPrice = eval($("#hdnPrice").val()) * eval(ItemDetail.info.PriceModifier) / 100;
                    } else {
                        costVariantPrice = eval(ItemDetail.info.PriceModifier);
                    }
                    if (ItemDetail.info.IsWeightPercentage) {
                        weightWithVariant = eval($("#hdnWeight").val()) * eval(ItemDetail.info.WeightModifier) / 100;
                    } else {
                        weightWithVariant = eval(ItemDetail.info.WeightModifier);
                    }

                    totalWeightVariant = eval($("#hdnWeight").val()) + eval(weightWithVariant);
                    itemPrice = eval(itemPrice) + eval(costVariantPrice);
                    var AddItemToCartObj = {
                        ItemID: itemId,
                        Price: itemPrice,
                        Weight: totalWeightVariant,
                        Quantity: itemQuantity,
                        CostVariantIDs: itemCostVariantIDs.join('@') + '@',
                        IsGiftCard: false,
                        IsKitItem: false
                    };
                    var paramz = {
                        aspxCommonObj: aspxCommonObj(),
                        AddItemToCartObj: AddItemToCartObj,
                        giftCardDetail: {},
                        kitInfo: {}
                    };
                    var data = JSON2.stringify(paramz);
                    this.config.method = "AspxCommonHandler.ashx/AddItemstoCartFromDetail";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = data;
                    this.config.ajaxCallMode = ItemDetail.AddItemstoCartFromDetail;
                    this.config.oncomplete = 20;
                    this.config.error = ItemDetail.GetAddToCartErrorMsg;
                    this.ajaxCall(this.config);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxItemDetails, 'Information Alert') + '</h2><p>' + getLocale(AspxItemDetails, 'Please choose available variants!') + '</p>');
                }
            }

        },

        ResetGallery: function (costcombinationId) {
            //var ids = '';
            //$("#divCostVariant input[type=radio]:checked").each(function () {
            //    ids += $(this).val() + "@";
            //});

            //$("#divCostVariant input[type=checkbox]:checked").each(function () {
            //    ids += $(this).val() + "@";
            //});
            //$("#divCostVariant select option:selected").each(function () {
            //    ids += $(this).val() + "@";
            //});
            //ids = ids.substr(0, ids.length - 1);
            ItemImageGalleryApi.ReloadItemImageGallery(itemId, itemTypeId, itemSKU, costcombinationId, aspxCommonObj());
        },
        ResizeImageDynamically: function (Imagelist, ImageTypes, ImageCatType) {
            ItemDetail.config.method = "MultipleImageResizer";
            ItemDetail.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + ItemDetail.config.method;
            ItemDetail.config.data = JSON2.stringify({ imgCollection: Imagelist, types: ImageTypes, imageCatType: ImageCatType, aspxCommonObj: aspxCommonObj() });
            ItemDetail.config.ajaxCallMode = ItemDetail.ResizeImageSuccess;
            ItemDetail.ajaxCall(ItemDetail.config);
        },
        ResizeImageSuccess: function () {
        },
        GetAllNotification: function (itemId, costVariantValueIDs) {
            var getNotificationObj = {
                ItemID: itemId,
                VariantID: costVariantValueIDs,
                Email: userEmail,
                ItemSKU: itemSKU
            };
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), getNotificationObj: getNotificationObj });
            this.config.method = "AspxCoreHandler.ashx/GetAllNotification";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.GetNotificationResult;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        InsertNotification: function (itemId, costVariantValueIDs) {
            var insertNotificationObj = {
                ItemID: itemId,
                VariantID: costVariantValueIDs,
                Email: userEmail,
                ItemSKU: itemSKU
            };
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), insertNotificationObj: insertNotificationObj });
            this.config.method = "AspxCoreHandler.ashx/InsertNotification";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = ItemDetail.GetNotificationMessage;
            this.config.error = ItemDetail.GetNotificationErrorMsg;
            this.config.async = true;
            this.ajaxCall(this.config);
        },

        variantCheckQuery: function () {
            if (variantQuery != null && variantQuery != '') {
                var variantIds = variantQuery.split('@');
                var elem = null;
                $.each(variantIds, function (index, value) {
                    if ($("#divCostVariant").find(".sfListmenu").find('select option[value=' + value + ']').parents().attr("id") != undefined) {
                        $("#divCostVariant").find(".sfListmenu").find('select option[value=' + value + ']').prop("selected", "selected");
                        var id = $("#divCostVariant").find(".sfListmenu").find('select option[value=' + value + ']').parents().attr("id");
                        id = parseFloat(id.substring(id.lastIndexOf('_') + 1, id.length));
                        if (variantId.indexOf(id) != -1) {
                            variantId.splice(variantId.indexOf(id), 1);
                        }
                        elem = $("#divCostVariant").find(".sfListmenu").find('select option[value=' + value + ']');
                    }
                    if ($("#divCostVariant").find('input:radio[value=' + value + ']').attr("name") != undefined) {
                        $("#divCostVariant").find('input:radio[value=' + value + ']').prop("checked", true);
                        var name = $("#divCostVariant").find('input:radio[value=' + value + ']').attr("name");
                        name = parseFloat(name.substring(name.lastIndexOf('_') + 1, name.length));
                        if (variantId.indexOf(name) != -1) {
                            variantId.splice(variantId.indexOf(name), 1);
                        }
                        checkAvailibility($("#divCostVariant").find('input:radio[value=' + value + ']'));
                        elem = $("#divCostVariant").find('input:radio[value=' + value + ']');
                    }
                });
                $.each(variantId, function (index, value) {
                    if ($("#controlCostVariant_" + value).parents().is('.sfListmenu')) {
                        $("#controlCostVariant_" + value).prepend("<option value=0 >" + getLocale(AspxItemDetails, "Not required") + "</option>");
                        $("#controlCostVariant_" + value).find("option[value=0]").prop('selected', 'selected');
                        elem = null;
                    }
                    else {
                        $('.cssClassRadio input[name="controlCostVariant_' + value + '"]').removeAttr('checked');
                        elem = null;
                    }
                });
                checkAvailibility(elem);
                if (ItemDetail.info.IsCombinationMatched == false)
                    selectFirstcombination();
            }

        },
        Init: function () {
            $("#" + lblListPrice).hide();
            $(".cssClassMasterLeft").html('');
            $("#divCenterContent").removeClass("cssClassMasterWrapperLeftCenter");
            $("#divCenterContent").addClass("cssClassMasterWrapperCenter");
            $(".cssClassYouSave").hide();
            if (itemName != "") {
                if (allowAddToCart.toLowerCase() != 'true') {
                    $(".cssClssQTY").hide();
                }
                var costVariantsData = '';

                if ($.session("ItemCostVariantData")) {
                    costVariantsData = $.session("ItemCostVariantData");
                    arrCostVariants = costVariantsData.split(',');
                }
                if (userFriendlyURL) {
                    $("#lnkContinueShopping").prop("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Home' + pageExtension);
                } else {
                    $("#lnkContinueShopping").prop("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Home');
                }

                $("#divEmailAFriend").hide();
                if (enableEmailFriend.toLowerCase() == 'true') {
                    $("#divEmailAFriend").show();
                }
                var Input = $('input[name=notify]');
                var default_value = Input.val();

                $(Input).focus(function () {
                    if ($(this).val() == default_value) {
                        $(this).val("");
                    }
                }).blur(function () {
                    if ($(this).val().length == 0) {
                        $(this).val(default_value);
                    }
                });
                if (userName != "anonymoususer") {
                    $("li #Notify").css('display', 'none');
                }
                else {
                    $("li #Notify").css('display', 'none');
                }

                if (customerId <= 0 && userName.toLowerCase() == "anonymoususer") {
                    if (allowAnonymousReviewRate.toLowerCase() == 'true') {
                    } else {
                        $(".cssClassItemRating a").removeClass("popupAddReview");
                        $(".cssClassAddYourReview").hide();
                    }
                }

                $("#bulkDiscount").html(getLocale(AspxItemDetails, '(Bulk Discount available)'));
                $("#itemQtyDiscount > tbody tr:even").addClass("sfEven");
                $("#itemQtyDiscount > tbody tr:odd").addClass("sfOdd");

                $("#dynItemDetailsForm").find(".cssClassIncrease").click(function () {
                    var cloneRow = $(this).closest('tr').clone(true);
                    $(cloneRow).appendTo("#AddTagTable");
                    $(cloneRow).find("input[type='text']").val('');
                    $(this).remove();
                });

                $("#spanDownloadLink").click(function () {
                    ItemDetail.BindDownloadEvent();
                });
                ItemDetail.BindItemBasicByitemSKU(itemSKU);
                ItemDetail.vars.existReviewByUser = isReviewExistByUser;
                ItemDetail.vars.existReviewByIP = isReviewExistByIP;
                var itemVariantIds = '';
                $("#divCostVariant input[type=radio]:checked").each(function () {
                    itemVariantIds += $(this).val() + ",";
                });
                $("#divCostVariant input[type=checkbox]:checked").each(function () {
                    itemVariantIds += $(this).val() + ",";
                });
                $("#divCostVariant select option:selected").each(function () {
                    itemVariantIds += $(this).val() + ",";
                });
                itemVariantIds = itemVariantIds.substr(0, itemVariantIds.length - 1);

                if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                    if (allowMultipleReviewPerUser.toLowerCase() != "true" && ItemDetail.vars.existReviewByUser.toLowerCase() == "true") {
                        $(".cssClassItemRating a").removeClass("popupAddReview");
                        $(".cssClassAddYourReview").hide();
                    }
                }

                if (allowMultipleReviewPerIP.toLowerCase() != "true" && ItemDetail.vars.existReviewByIP.toLowerCase() == "true") {
                    $(".cssClassItemRating a").removeClass("popupAddReview");
                    $(".cssClassAddYourReview").hide();
                }
                $("#txtQty").bind("contextmenu", function (e) {
                    return false;
                });
                $('#txtQty').bind('paste', function (e) {
                    e.preventDefault();
                });
                $("#txtQty").bind('focus', function (e) {
                    $(this).val('');
                    $('#lblNotification').html('');
                });
                $("#txtQty").bind('select', function (e) {
                    $(this).val('');
                    $('#lblNotification').html('');
                });
                $("#txtQty").bind('blur', function (e) {
                    $('#lblNotification').html('');
                    $("#txtQty").val($(this).attr('addedValue'));
                });
                // $('.popbox').hide();
                if (itemTypeId != 5) {
                    // ItemDetail.GetPriceHistory(itemId);
                }
                $('.popbox').popbox();
                $("#txtQty").bind("keypress", function (e) {
                    var itemCostVariantIDs = '';
                    if ($('#divCostVariant').is(':empty')) {
                        itemCostVariantIDs = '0@';
                    } else {
                        $("#divCostVariant select option:selected").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs += $(this).val() + "@";
                            } else {
                            }
                        });
                        $("#divCostVariant input[type=radio]:checked").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs += $(this).val() + "@";
                            } else {
                            }
                        });
                        $("#divCostVariant input[type=checkbox]:checked").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs += $(this).val() + "@";
                            } else {
                            }
                        });
                    }
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            if (ItemDetail.info.Quantity <= 0) {
                                return false;
                            } else {
                                if ((e.which >= 48 && e.which <= 57)) {
                                    var num;
                                    if (e.which == 48)
                                        num = 0;
                                    if (e.which == 49)
                                        num = 1;
                                    if (e.which == 50)
                                        num = 2;
                                    if (e.which == 51)
                                        num = 3;
                                    if (e.which == 52)
                                        num = 4;
                                    if (e.which == 53)
                                        num = 5;
                                    if (e.which == 54)
                                        num = 6;
                                    if (e.which == 55)
                                        num = 7;
                                    if (e.which == 56)
                                        num = 8;
                                    if (e.which == 57)
                                        num = 9;

                                    var itemsCartInfo = ItemDetail.CheckItemQuantityInCart(itemId, itemCostVariantIDs);
                                    var itemQuantityInCart = itemsCartInfo.ItemQuantityInCart;

                                    if (itemQuantityInCart != 0.1) {
                                        if ((eval($("#txtQty").val() + '' + num) + eval(itemQuantityInCart)) > eval(ItemDetail.info.Quantity)) {
                                            $('#lblNotification').html(getLocale(AspxItemDetails, "The quantity is greater than the available quantity.")); return false;

                                        } else {
                                            $('#lblNotification').html('');
                                        }
                                    } else {
                                        $("#txtQty").val(1).prop("disabled", "disabled");
                                    }

                                }
                            }
                        }
                    }

                    if ($(this).val() == "") {
                        if (e.which != 8 && e.which != 0 && (e.which < 49 || e.which > 57)) {
                            return false;
                        }
                    } else {
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    }
                    if (num != undefined) {
                        $("#txtQty").attr('addedValue', eval($("#txtQty").val() + '' + num));
                    }
                });

                $(".cssClassTotalReviews").bind("click", function () {
                    $.metadata.setType("class");
                    var $tabs = $('#ItemDetails_TabContainer').tabs();

                    $("#ItemDetails_TabContainer").find('ul').removeClass();
                    $("#ItemDetails_TabContainer ").find("ul").addClass("responsive-tabs__list");

                    $("#ItemDetails_TabContainer").removeClass();
                    $("#ItemDetails_TabContainer").addClass("responsive-tabs responsive-tabs--enabled");
                    $("#tablist1-tab4").trigger("click");
                });
                //ItemDetail.BindRatingCriteria();
                $("#dynItemDetailsForm").show();
                $('.cssClassItemRating a.popupAddReview').bind("click", function () {
                    ItemDetail.BindPopUp();
                    ShowPopup(this);
                });

                $(".cssClassClose").click(function () {
                    $('#fade, #popuprel2').fadeOut();
                });

                $("#btnNotify").bind("click", function () {
                    var itemCostVariantIDs = [];
                    if ($('#divCostVariant').is(':empty')) {
                        itemCostVariantIDs.push(null);
                    } else {
                        $("#divCostVariant input[type=radio]:checked").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs.push($(this).val());
                            } else { }
                        });

                        $("#divCostVariant input[type=checkbox]:checked").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs.push($(this).val());
                            } else { }
                        });

                        $("#divCostVariant select option:selected").each(function () {
                            if ($(this).val() != 0) {
                                itemCostVariantIDs.push($(this).val());
                            } else { }
                        });
                    }

                    isNotificationExist = false;
                    if (userName == "anonymoususer") {
                        userEmail = $("#txtNotifiy").val();
                    }
                    var filter = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    if (filter.test(userEmail) == true) {
                        ItemDetail.GetAllNotification(itemId, itemCostVariantIDs.join('@'));
                        if (isNotificationExist == false) {
                            ItemDetail.InsertNotification(itemId, itemCostVariantIDs.join('@'));
                        }
                        else {
                            csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "You are already interested in this product") + "</p>");
                        }
                    }
                    else {
                        csscody.alert("<h2>" + getLocale(AspxItemDetails, "Information Alert") + "</h2><p>" + getLocale(AspxItemDetails, "Invalid Email!") + "</p>");
                        return false;

                    }
                });

                $("#btnSubmitReview").click(function () {
                    $("#form1").validate({
                        messages: {
                            urname: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                            },
                            uremail: {
                                required: '*'
                            },
                            fname: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                            },
                            femail: {
                                required: '*'
                            },
                            subject: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                            },
                            message: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 100 chars)") + ""
                            },
                            name: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                            },
                            summary: {
                                required: '*',
                                minlength: "* " + getLocale(AspxItemDetails, "(at least 2 chars)") + "",
                                maxlength: "" + getLocale(AspxItemDetails, "(at most 300 chars)") + ""
                            },
                            review: {
                                required: '*',
                                minlength: "*"
                            }
                        },
                        submitHandler: function () { ItemDetail.SaveItemRatings(); }
                    });
                    RemovePopUp();
                });

                $('a.popupEmailAFriend').click(function () {
                    ItemDetail.ShowUsingPage();
                    ShowPopup(this);
                });

            } else {
                $('#itemDetails').hide();
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Home" + pageExtension;
                } else {
                    window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Home";
                }
            }
        }
    };
    ItemDetail.Init();
    return {
        Info: ItemDetail.info
    }
});



