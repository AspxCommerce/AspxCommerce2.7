//<![CDATA[
var categoryDetails = '';
var cat = '';
var rowTotal = 0;
var count = 0;
var templateScriptArr = [];
var NewItemArray = [];
var arrPrice = [];
var arry = new Array();
var brandIds = '';
var priceFrom;
var priceTo;
var Imagelist = '';

IsExistedCategory = function (arr, cat) {
    var isExist = false;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == cat) {
            isExist = true;
            break;
        }
    }
    return isExist;
};
indexOfArray = function (arr, val) {
    var arrIndex = -1;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == val) {
            arrIndex = i; break;
        }
    }
    return arrIndex;
};
BindCurrencySymbol = function () {
    var cookieCurrency = Currency.cookie.read();
    Currency.currentCurrency = BaseCurrency;
    Currency.format = 'money_format';
    Currency.convertAll(Currency.currentCurrency, cookieCurrency);
};

$(function () {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var templatePath = AspxCommerce.utils.GetAspxTemplateFolderPath();
   
    var arrItemsOption = new Array();
   //var arrItemsOptionToBind = new Array();
    var currentpage = 0;
    var isByCategory = false;
    var isFirstCall = true;
    categoryDetails = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: "",
            itemid: 0
        },
        GetTemplate: function (key) {
            var val = "";
            if (templateScriptArr.length > 0) {
                for (var i = 0; i < templateScriptArr.length; i++) {
                    if (templateScriptArr[i].TemplateKey == key) {
                        val = templateScriptArr[i].TemplateValue;
                        break;
                    }
                }
            }
            return val;
        },
        init: function () {
            $.each(jsTemplateArray, function (index, value) {
                var tempVal = jsTemplateArray[index].split('@');
                var templateScript = {
                    TemplateKey: tempVal[0],
                    TemplateValue: tempVal[1]
                };
                templateScriptArr.push(templateScript);
            });
            $(":checkbox").prop("checked", false);
            //categoryDetails.LoadAllCategoryContents();
            if (isCategoryHasItems != 0) {
                $("#divShopFilter").show();
                $("#tblFilter").show();
            }
            else {
                $("#divShopFilter").hide();
                $("#tblFilter").hide();
                $("#divShowCategoryItemsList").html("<span class=\"cssClassNotFound\">" + getLocale(DetailsBrowse, "No items found!") + "</span>");
            }
            cat = categorykey;
            $("#slider-range").slider({
                range: true,
                min: minPrice,
                max: maxPrice,
                step: 0.001,
                values: [minPrice, maxPrice],
                slide: function (event, ui) {
                    $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[0]).toFixed(2) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[1]).toFixed(2) + "</span>");
                    BindCurrencySymbol();
                },
                change: function (event, ui) {
                    if (isFirstCall == true) {
                        $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[0]).toFixed(2) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[1]).toFixed(2) + "</span>");
                        BindCurrencySymbol();
                        if (event.originalEvent == undefined && count == 0) {
                            categoryDetails.GetDetail(1, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
                            count = count + 1;
                        }
                        else if (event.originalEvent != undefined) {
                            categoryDetails.GetDetail(1, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
                        }
                    }
                }
            });

            $("#tblFilter").show();
            $("#tblFilter").find('h2').html('<span>' + getLocale(DetailsBrowse, "Filters") + '</span>');
            $("#divShoppingFilter").hide();
            $("#divShoppingFilter").show();
            var brandArr = [];
            $(":checkbox").uniform();
            $("#ddlSortBy").change(function () {
                var items_per_page = $('#ddlPageSize').val();
                var offset = 1;
                categoryDetails.GetDetail(offset, items_per_page, 0, $("#ddlSortBy").val());
            });
            $("#ddlPageSize").change(function () {
                var items_per_page = $(this).val();
                var offset = 1;
                categoryDetails.GetDetail(offset, items_per_page, 0, $("#ddlSortBy").val());
            });
            $("#ddlViewAs").change(function () {
                categoryDetails.BindShoppingFilterResult();

            });
            $("#divViewAs").on('click', "a", function () {
                $("#divViewAs").find('a').removeClass('sfactive');
                $(this).addClass("sfactive");
                categoryDetails.BindShoppingFilterResult();
            });
            $(".filter").on('click', ".chkCategory", function () {
                minPrice = 0;
                maxPrice = 0;
                isCategoryHasItems = 0;
                arrPrice = [];
                NewItemArray = [];
                brandIds = '';
                arry.length = 0;
                var values = $(this).attr('ids');
                var isChecked = false;
                $('.chkCategory').each(function () {
                    if ($(this).prop('checked') == true) {
                        isChecked = true;
                        return false;
                    }
                });
                if ($(this).prop('checked') == true) {
                    if (indexOfArray(brandArr, values) == -1) {
                        brandArr.push(values);
                    }
                }
                else {
                    if (indexOfArray(brandArr, values) != -1) {
                        brandArr.splice(indexOfArray(brandArr, values), 1);
                    }
                }
                if (isChecked) {
                    cat = brandArr.join(',');
                    isByCategory = true;
                }
                else {
                    cat = categorykey;
                    isByCategory = false;
                }
                $('.filter > div:gt(0)').remove();
                categoryDetails.GetAllBrandForCategory();
                categoryDetails.GetShoppingFilter();
                $(":checkbox").not(".chkCategory").uniform();
                if (isCategoryHasItems != 0) {
                    $("#divItemViewOptions").show();
                    $("#divSearchPageNumber").show();
                    var offset = 1;
                    categoryDetails.GetDetail(offset, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
                } else {
                    $("#divItemViewOptions").hide();
                    $("#divSearchPageNumber").hide();
                    $("#divShowCategoryItemsList").html("No items found!");
                }
            });

            $(".filter").on('click', ".chkFilter", function () {
                var isChecked = false;
                brandIds = '';
                $('.chkFilter').each(function () {
                    if ($(this).prop('checked') == true) {
                        isChecked = true;
                        return false;
                    }
                });
                if (!$(this).hasClass('chkBrand')) {
                    var attrValue = $(this).prop('name');
                    var attrIds = $(this).val();
                    var inputTypeId = $(this).attr('inputTypeID');
                    var values = attrIds + '@' + inputTypeId + '@' + attrValue;
                    if ($(this).prop('checked') == true) {
                        if (indexOfArray(arry, values) == -1) {
                            arry.push(values);
                        }
                    }

                    if ($(this).prop('checked') == false) {
                        if (indexOfArray(arry, values) != -1) {
                            arry.splice(indexOfArray(arry, values), 1);
                        }
                        if (indexOfArray(arry, values) == -1) {
                            $(this).parents('ul').find(".chkFilter").each(function () {
                                if ($(this).prop('checked') == true) {
                                    attrIds + '@' + inputTypeId + '@' + attrValue;
                                    var chkval = attrIds + '@' + $(this).attr('inputTypeID') + '@' + $(this).val();
                                    if (chkval == values) {
                                        if (indexOfArray(arry, values) == -1) {
                                            arry.push(values);
                                        }
                                    }
                                }
                            });
                        }
                    }
                    var aux = {};
                    $.each(arry, function (idx, val) {
                        var key = [];
                        key[0] = val.substring(0, val.lastIndexOf('@'));
                        key[1] = val.substring(val.lastIndexOf('@') + 1, val.length);
                        if (!aux[key[0]]) {
                            aux[key[0]] = [];
                        }
                        aux[key[0]].push(key[1]);
                    });

                    NewItemArray = [];
                    $.each(aux, function (idx, val) {
                        NewItemArray.push(idx + '@' + val.join("#"));
                    });
                }
                if ($(".divContent0").length > 0) {
                    $('.chkBrand').each(function () {
                        if ($(this).prop('checked') == true) {
                            brandIds += $(this).attr('ids') + ',';
                        }
                    });
                    brandIds = brandIds.substring(0, brandIds.lastIndexOf(','));
                }
                categoryDetails.GetDetail(1, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
            });

            $(".filter").on('click', 'div[value="8"] a', function () {
                count = 0;
                $("#slider-range").slider("option", "values", [$(this).attr('minprice'), $(this).attr('maxprice')]);
                $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat($("#slider-range").slider("values", 0)).toFixed(2) + "</span>" +
                " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat($("#slider-range").slider("values", 1)).toFixed(2) + "</span>");
                BindCurrencySymbol();
            });

            $(".filter").on('click', '.divTitle', function () {
                var imgPath = $(this).find('img').prop('src');

                if (imgPath.toString().indexOf("arrow_down.png") != -1) {
                    $(this).find('img').prop('src', '' + templatePath + '/images/arrow_up.png');

                } else {
                    $(this).find('img').prop('src', '' + templatePath + '/images/arrow_down.png');
                }
                if ($(this).parent().attr('value') == '8') {
                    $(".divContent" + $(this).parent().attr('value')).slideToggle('fast');
                    $(".divRange").slideToggle('fast');
                } else {
                    $(this).next(".cssClassScroll").slideToggle('fast');
                }
            });
            $(".filter").find('a').on('hover', 'div[value="8"] a', function () {
                $(this).css("text-decoration", "underline");
            });
            $(".filter").find('a').on('mouseout', 'div[value="8"] a', function () {
                $(this).css("text-decoration", "none");
            });
        },
        ajaxCall: function (config) {
            $.ajax({
                type: categoryDetails.config.type,
                contentType: categoryDetails.config.contentType,
                cache: categoryDetails.config.cache,
                async: categoryDetails.config.async,
                url: categoryDetails.config.url,
                data: categoryDetails.config.data,
                dataType: categoryDetails.config.dataType,
                success: categoryDetails.config.ajaxCallMode,
                error: categoryDetails.ajaxFailure
            });
        },
        GetAllSubCategoryForFilter: function () {
            var param = JSON2.stringify({ categorykey: cat, aspxCommonObj: aspxCommonObj });
            this.config.method = "GetAllSubCategoryForFilter";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = categoryDetails.BindSubCategoryForFilter;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        GetAllBrandForCategory: function () {
            var param = JSON2.stringify({ categorykey: cat, isByCategory: isByCategory, aspxCommonObj: aspxCommonObj });
            this.config.method = "GetAllBrandForCategory";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = categoryDetails.BindBrandForCategory;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        GetShoppingFilter: function () {
            var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj, categoryName: cat, isByCategory: isByCategory });
            this.config.method = "GetShoppingFilter";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = categoryDetails.BindShoppingFilter;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        GetItemDetail: function (brandIds, attributes, limit, offset, sortBy) {
            currentPage = currentpage;
            var param = JSON2.stringify({ brandIds: brandIds, attributes: attributes, priceFrom: priceFrom, priceTo: priceTo, categoryName: cat, isByCategory: isByCategory, limit: limit, offset: offset, sortBy: sortBy, aspxCommonObj: aspxCommonObj });
            this.config.method = "GetShoppingFilterItemsResult";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.async = false;
            this.config.ajaxCallMode = categoryDetails.BindItemDetail;
            this.ajaxCall(this.config);
        },
        GetDetail: function (offset1, limit1, currentpage1, sortBy) {
            currentpage = currentpage1;
            sortBy1 = sortBy;
            priceFrom = $("#slider-range").slider("values", 0);
            priceTo = $("#slider-range").slider("values", 1);
            categoryDetails.GetItemDetail(brandIds, NewItemArray.join(','), limit1, offset1, sortBy1);
            categoryDetails.ResizeImageDynamically(Imagelist);
            categoryDetails.BindShoppingFilterResult();
        },

        GridView: function () {
            $("#divShowCategoryItemsList").html('');
            $("#divItemViewOptions").show();
            $("#divSearchPageNumber").show();
            var itemIds = [];
            var tempScriptGridView = categoryDetails.GetTemplate('scriptResultGrid');
            $.each(arrItemsOptionToBind[0], function (index, value) {
                if (!IsExistedCategory(itemIds, value.ItemID)) {
                    itemIds.push(value.ItemID);
                    var imagePath = itemImagePath + value.BaseImage;
                    if (value.BaseImage == "") {
                        imagePath = noImageCategoryDetailPath;
                    }
                    else {
                        imagePath = imagePath.replace('uploads', 'uploads/Medium');
                    }
                    var name = '';
                    if (value.Name.length > 50) {
                        name = value.Name.substring(0, 50);
                        var i = 0;
                        i = name.lastIndexOf(' ');
                        name = name.substring(0, i);
                        name = name + "...";
                    } else {
                        name = value.Name;
                    }
                    var items = [{
                        itemID: value.ItemID,
                        name: name,
                        titleName: value.Name,
                        AspxCommerceRoot: aspxRedirectPath,
                        sku: value.SKU,
                        imagePath: aspxRootPath + imagePath,
                        loaderpath: templatePath + "/images/loader_100x12.gif",
                        alternateText: value.Name,
                        listPrice: (value.ListPrice),
                        price: (value.Price),
                        isCostVariant: '"' + value.IsCostVariantItem + '"',
                        shortDescription: Encoder.htmlDecode(value.ShortDescription)
                    }];
                    $.tmpl(tempScriptGridView, items).appendTo("#divShowCategoryItemsList");
                    if (value.AttributeValues != null) {
                        if (value.AttributeValues != "") {
                            var attrHtml = '';
                            attrValues = [];
                            attrValues = value.AttributeValues.split(',');
                            attrHtml = "<div class='cssGridDyanamicAttr'>";
                            for (var i = 0; i < attrValues.length; i++) {
                                var attributes = [];
                                attributes = attrValues[i].split('#');
                                var attributeName = attributes[0];
                                var attributeValue = attributes[1];
                                var inputType = parseInt(attributes[2]);
                                var validationType = attributes[3];
                                attrHtml += "<div class=\"cssDynamicAttributes\">";
                                attrHtml += "<span>";
                                attrHtml += attributeName;
                                attrHtml += "</span> :";
                                if (inputType == 7) {
                                    attrHtml += "<span class=\"cssClassFormatCurrency\">";
                                }
                                else {
                                    attrHtml += "<span>";
                                }
                                attrHtml += attributeValue;
                                attrHtml += "</span></div>";
                            }
                            attrHtml += "</div>";
                            $('.cssGridDyanamicAttr').html(attrHtml);
                        }
                        else {
                            $('.cssGridDyanamicAttr').remove();
                        }
                    }
                    else {
                        $('.cssGridDyanamicAttr').remove();
                    }
                    BindCurrencySymbol();
                    if (value.ListPrice == "") {
                        $(".cssRegularPrice_" + value.ItemID + "").remove();
                    }
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            if (value.IsOutOfStock) {
                                $(".cssClassAddtoCard_" + value.ItemID + " span").html(getLocale(DetailsBrowse, 'Out Of Stock'));
                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                $(".cssClassAddtoCard_" + value.ItemID).find('label').removeClass('i-cart cssClassGreenBtn');
                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                            }
                        }
                    }
                    else { $(".cssClassAddtoCard_" + value.ItemID).hide(); }
                    if (value.ItemTypeID == 5) {
                        $(".cssClassAddtoCard_" + value.ItemID + "").parents(".cssLatestItemInfo").find(".cssClassProductRealPrice").prepend(getLocale(AspxTemplateLocale, "Starting At"));
                    }
                }
            });
            $("#divShowCategoryItemsList").wrapInner("<div class='cssCatProductGridWrapper'></div>");
            var x = 0;
            $('#divShowCategoryItemsList .cssClassProductsBox ').each(function () {
                x++;
                if ((x % 3) == 0) {
                    $(this).addClass('cssClassNoMargin');
                }
            });
            if (itemIds.length == 0) {
                $("#divItemViewOptions").hide();
                $("#divSearchPageNumber").hide();
                $("#divShowCategoryItemsList").html("<span class=\"cssClassNotFound\">" + getLocale(DetailsBrowse, "No items found or matched!") + "</span>");
            }
            var $container = $(".cssCatProductGridWrapper");

            $container.imagesLoaded(function () {
                $container.masonry({
                    itemSelector: '.cssClassProductsBox',
                    EnableSorting: false
                });
            });
            currencyRate = 0;
        },

        ListView: function () {
            $("#divShowCategoryItemsList").html('');
            $("#divItemViewOptions").show();
            $("#divSearchPageNumber").show();
            var itemIds = [];
            var tempScriptListView = categoryDetails.GetTemplate('scriptResultList');
            $.each(arrItemsOptionToBind[0], function (index, value) {
                if (!IsExistedCategory(itemIds, value.ItemID)) {
                    itemIds.push(value.ItemID);
                    var imagePath = itemImagePath + value.BaseImage;
                    if (value.BaseImage == "") {
                        imagePath = noImageCategoryDetailPath;
                    }
                    else {

                        imagePath = imagePath.replace('uploads', 'uploads/Medium');
                    }
                    var name = '';
                    if (value.Name.length > 50) {
                        name = value.Name.substring(0, 50);
                        var i = 0;
                        i = name.lastIndexOf(' ');
                        name = name.substring(0, i);
                        name = name + "...";
                    } else {
                        name = value.Name;
                    }
                    var items = [{
                        itemID: value.ItemID,
                        name: name,
                        titleName: value.Name,
                        AspxCommerceRoot: aspxRedirectPath,
                        sku: value.SKU,
                        imagePath: aspxRootPath + imagePath,
                        alternateText: value.Name,
                        listPrice: (value.ListPrice),
                        price: (value.Price),
                        isCostVariant: '"' + value.IsCostVariantItem + '"',
                        shortDescription: Encoder.htmlDecode(value.ShortDescription)
                    }];
                    $.tmpl(tempScriptListView, items).appendTo("#divShowCategoryItemsList");

                    if (value.AttributeValues != null) {
                        if (value.AttributeValues != "") {
                            var attrHtml = '';
                            attrValues = [];
                            attrValues = value.AttributeValues.split(',');
                            for (var i = 0; i < attrValues.length; i++) {
                                var attributes = [];
                                attributes = attrValues[i].split('#');
                                var attributeName = attributes[0];
                                var attributeValue = attributes[1];
                                var inputType = parseInt(attributes[2]);
                                var validationType = attributes[3];
                                attrHtml += "<div class=\"cssDynamicAttributes\">";
                                if (inputType == 7) {
                                    attrHtml += "<span class=\"cssClassFormatCurrency\">";
                                }
                                else {
                                    attrHtml += "<span>";
                                }
                                attrHtml += attributeValue;
                                attrHtml += "</span></div>";
                            }
                            $('.cssListDyanamicAttr').html(attrHtml);
                        }
                        else {
                            $('.cssListDyanamicAttr').remove();
                        }
                    }
                    else {
                        $('.cssListDyanamicAttr').remove();
                    }
                    BindCurrencySymbol();
                    if (value.ListPrice == "") {
                        $(".cssRegularPrice_" + value.ItemID + "").remove();
                    }
                    if (allowAddToCart.toLowerCase() == 'true') {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            if (value.IsOutOfStock) {
                                $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                $(".cssClassAddtoCard_" + value.ItemID).find('label').removeClass('i-cart cssClassGreenBtn');
                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                            }
                        }
                    }
                    else { $(".cssClassAddtoCard_" + value.ItemID).hide(); }
                    if (value.ItemTypeID == 5) {
                        $(".cssClassAddtoCard_" + value.ItemID + "").parents(".cssLatestItemInfo").find(".cssClassProductRealPrice").prepend(getLocale(AspxTemplateLocale, "Starting At"));
                    }
                }
            });
            if (itemIds.length == 0) {
                $("#divItemViewOptions").hide();
                $("#divSearchPageNumber").hide();
                $("#divShowCategoryItemsList").html("<span class=\"cssClassNotFound\">" + getLocale(DetailsBrowse, "No items found or matched!") + "</span>");
            }
        },
        pageselectCallback: function (page_index, jq, execute) {
            if (execute) {

                var max_elem = arrItemsOption.length;
                arrItemsOptionToBind[0].length = 0;

                for (var i = 0; i < max_elem; i++) {
                    arrItemsOptionToBind[0].push(arrItemsOption[i]);
                }
                $.each(arrItemsOptionToBind[0], function (index, item) {
                    Imagelist += item.BaseImage + ';';
                });

            }
            return false;
        },
        //Send the list of images to the ImageResizer
        ResizeImageDynamically: function (Imagelist, type) {
            ImageType = {
                "Large": "Large",
                "Medium": "Medium",
                "Small": "Small"
            };
            categoryDetails.config.method = "DynamicImageResizer";
            categoryDetails.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + this.config.method;
            categoryDetails.config.data = JSON2.stringify({ imgCollection: Imagelist, type: ImageType.Medium, imageCatType: "Item", aspxCommonObj: aspxCommonObj });
            categoryDetails.config.ajaxCallMode = categoryDetails.ResizeImageSuccess;
            categoryDetails.ajaxCall(categoryDetails.config);

        },

        //Send the list of images to the ImageResizer
        ResizeCategoryImageDynamically: function (Imagelist, type) {
            categoryDetails.config.method = "DynamicImageResizer";
            categoryDetails.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + this.config.method;
            categoryDetails.config.data = JSON2.stringify({ imgCollection: Imagelist, type: type, imageCatType: "Category", aspxCommonObj: aspxCommonObj });
            categoryDetails.config.ajaxCallMode = categoryDetails.ResizeImageSuccess;
            categoryDetails.ajaxCall(categoryDetails.config);

        },
        ResizeImageSuccess: function () {
        },
        BindShoppingFilterResult: function () {
            var viewAsOption = '';
            if (displaymode == "icon") {
                viewAsOption = $("#divViewAs").find('a.sfactive').attr("displayId");
                if (typeof viewAsOption == 'undefined') {
                    $("#divViewAs").find('a:eq(0)').addClass("sfactive");
                    viewAsOption = $("#divViewAs").find('a.sfactive').attr("displayId");
                }
            }
            else {
                viewAsOption = $("#ddlViewAs").val();
            }
            switch (viewAsOption) {
                case '1':
                    categoryDetails.GridView();
                    break;
                case '2':
                    categoryDetails.ListView();
                    break;
            }
        },
        BindSubCategoryForFilter: function (msg) {
            var elem = '';
            $(".filter").html('');
            var length = msg.d.length;
            if (length > 0) {
                elem = '<div id="divCat" value="b01" class="cssClasscategorgy">';
                elem += '<div class="divTitle"><b><label style="color:#006699">' + getLocale(DetailsBrowse, 'Categories') + '</label></b><img align="right" src="' + templatePath + '/images/arrow_up.png"/></div> <div id="scrollbar1" class="cssClassScroll"><div class="viewport"><div class="overview" id="catOverview"><div class="divContentb01"><ul id="cat"></ul></div></div></div></div></div>';
                $(".filter").append(elem);
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    elem = '';
                    elem = '<li><label><input class="chkCategory" type="checkbox" name="' + value.CategoryName + '" ids="' + value.CategoryID + '" value="' + value.CategoryName + '"/> ' + value.CategoryName + '<span> (' + value.ItemCount + ')</span></label></li>';
                    $(".filter").find('div[value="b01"]').find('ul').append(elem);
                };
            }
        },

        BindBrandForCategory: function (msg) {
            var elem = '';
            var arrBrand = [];
            var length = msg.d.length;
            if (length > 0) {
                elem = '<div value="0" class="cssClasscategorgy">';
                elem += '<div class="divTitle"><b><label style="color:#006699">' + getLocale(DetailsBrowse, 'Brands') + '</label></b><img align="right" src="' + templatePath + '/images/arrow_up.png"/></div><div id="scrollbar2" class="cssClassScroll"><div class="viewport"><div class="overview"><div class="divContent0"><ul></ul></div></div></div></div></div>';
                $(".filter").append(elem);
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    if (indexOfArray(arrBrand, value.BrandID) == -1) {
                        elem = '';
                        elem = '<li><label><input class="chkFilter chkBrand" type="checkbox" name="' + value.BrandName + '" ids="' + value.BrandID + '" value="0"/> ' + value.BrandName + '<span id="count"> (' + value.ItemCount + ')</span></label></li>';
                        $(".filter").find('div[value="0"]').find('ul').append(elem);
                        arrBrand.push(value.BrandID);
                    }
                };
            }
        },

        BindShoppingFilter: function (msg) {
            var attrID = [];
            var attrValue = [];
            var attrName = '';
            var elem = '';
            var length = msg.d.length;
            if (length > 0) {
                $(".divRange").show();
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    isCategoryHasItems = 1;
                    if (value.AttributeID != 8 && value.AttributeID > 48) {
                        if (indexOfArray(attrID, value.AttributeID) == -1) {
                            elem = '<div value=' + value.AttributeID + ' class="cssClasscategorgy"><div class="divTitle"><b><label style="color:#006699">' + value.AttributeName + '</label></b><img align="right" src="' + templatePath + '/images/arrow_up.png"/></div> <div id="scrollbar3" class="cssClassScroll"><div class="viewport"><div class="overview"><div class=' + "divContent" + value.AttributeID + '><ul></ul></div></div></div></div></div>';
                            attrValue = [];
                            attrID.push(value.AttributeID);
                            $(".filter").append(elem);
                            elem = '';
                            elem = '<li><label><input class= "chkFilter" type="checkbox" name="' + value.AttributeValue + '" inputTypeId="' + value.InputTypeID + '"  value="' + value.AttributeID + '"/> ' + value.AttributeValue + '<span id="count"> (' + value.ItemCount + ')</span></label></li>';
                            $(".filter").find('div[value=' + value.AttributeID + ']').find('ul').append(elem);
                            attrValue.push(value.AttributeValue);
                        } else {
                            if (indexOfArray(attrValue, value.AttributeValue) == -1) {
                                itemCount = 1;
                                elem = '';
                                elem = '<li><label><input class="chkFilter" type="checkbox" name="' + value.AttributeValue + '" inputTypeId="' + value.InputTypeID + '"  value="' + value.AttributeID + '"/> ' + value.AttributeValue + '<span id="count"> (' + value.ItemCount + ')</span></label></li>';
                                $(".filter").find('div[value=' + value.AttributeID + ']').find('ul').append(elem);
                                attrValue.push(value.AttributeValue);
                            }
                        }
                    } else if (value.AttributeID == 8) {
                        arrPrice.push(value);
                        if (parseFloat(value.AttributeValue) > maxPrice) {
                            maxPrice = parseFloat(value.AttributeValue);
                        }
                    }
                };
                var interval = parseFloat((maxPrice - minPrice) / 4);
                elem = '<div value="8" class="cssClassbrowseprice">';
                elem += '<div class="divTitle"><b><label style="color:#006699">' + getLocale(DetailsBrowse, 'Price') + '</label></b><img align="right" src="' + templatePath + '/images/arrow_up.png"/></div><div class="divContent8"><ul>';

                if (arrPrice.length > 1) {
                    elem += '<li><a id="f1" href="#" ids=""  minprice=' + GetPriceData(minPrice, 0, interval) + ' maxprice=' + GetPriceData(minPrice, 1, interval) + '>' + '<span class=\"cssClassFormatCurrency\">' + parseFloat(minPrice).toFixed(2) + '</span>' + ' - ' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice, 1, interval) + '</span>' + '</a></li>';
                    elem += '<li><a id="f2" href="#" ids="" minprice=' + GetPriceData(minPrice + 0.01, 1, interval) + ' maxprice=' + GetPriceData(minPrice, 2, interval) + '>' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice + 0.01, 1, interval) + '</span>' + ' - ' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice, 2, interval) + '</span>' + '</a></li>';
                    elem += '<li><a id="f3" href="#" ids="" minprice=' + GetPriceData(minPrice + 0.01, 2, interval) + ' maxprice=' + GetPriceData(minPrice, 3, interval) + '>' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice + 0.01, 2, interval) + '</span>' + ' - ' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice, 3, interval) + '</span>' + '</a></li>';
                    elem += '<li><a id="f4" href="#" ids="" minprice=' + GetPriceData(minPrice + 0.01, 3, interval) + ' maxprice=' + maxPrice + '>' + '<span class=\"cssClassFormatCurrency\">' + GetPriceDataFloat(minPrice + 0.01, 3, interval) + '</span>' + ' - ' + '<span class=\"cssClassFormatCurrency\">' + parseFloat(maxPrice).toFixed(2) + '</span>' + '</a></li>';
                }
                if (arrPrice.length == 1) {
                    elem += '<li><a id="f1" href="#" ids=","  minprice=' + GetPriceData(minPrice, 0, interval) + ' maxprice=' + GetPriceData(minPrice, 1, interval) + '>' + '<span class=\"cssClassFormatCurrency\">' + parseFloat(minPrice).toFixed(2) + '</span>' + '</a></li>';
                    minPrice = 0;
                }
                elem += '</ul></div>';
                elem += '<div class="divRange"><div id="slider-range"></div><p><b style="color: #006699">' + getLocale(DetailsBrowse, "Range: ") + '<span id="amount"></span></b></p></div></div>';
                $(".filter").append(elem);
                BindCurrencySymbol();
                minPrice = parseFloat(minPrice);
                maxPrice = parseFloat(maxPrice);
                $("#slider-range").slider({
                    range: true,
                    min: minPrice,
                    max: maxPrice,
                    step: 0.001,
                    values: [minPrice, maxPrice],
                    slide: function (event, ui) {
                        $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[0]).toFixed(2) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[1]).toFixed(2) + "</span>");
                        BindCurrencySymbol();
                    },
                    change: function (event, ui) {
                        $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[0]).toFixed(2) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat(ui.values[1]).toFixed(2) + "</span>");
                        BindCurrencySymbol();
                        if (event.originalEvent == undefined && count == 0) {
                            categoryDetails.GetDetail(1, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
                            count = count + 1;
                        }
                        else if (event.originalEvent != undefined) {
                            categoryDetails.GetDetail(1, $('#ddlPageSize').val(), 0, $("#ddlSortBy").val());
                        }

                    }
                });
                $("#amount").html("<span class=\"cssClassFormatCurrency\">" + parseFloat($("#slider-range").slider("values", 0)).toFixed(2) + "</span>" +
                           " - " + "<span class=\"cssClassFormatCurrency\">" + parseFloat($("#slider-range").slider("values", 1)).toFixed(2) + "</span>");

                BindCurrencySymbol();
            }
            else {
                if ($(".filter").length == 0) {
                    $("#divShopFilter").hide();
                }
                else {
                    $(".divRange").hide();
                }
            }
        },

        BindItemDetail: function (msg) {
            arrItemsOptionToBind[0].length = 0;
            rowTotal = 0;
            arrItemsOption.length = 0;
            $.each(msg.d, function (index, value) {
                arrItemsOption.push(value);
                rowTotal = value.RowTotal;
            });
            limit = $('#ddlPageSize').val();
            var offset = 1;
            var items_per_page = $('#ddlPageSize').val();

            $("#Pagination").pagination(rowTotal, {
                callback: categoryDetails.pageselectCallback,
                items_per_page: items_per_page,
                current_page: currentpage,
                callfunction: true,
                function_name: { name: categoryDetails.GetDetail, limit: $('#ddlPageSize').val(), sortBy: $("#ddlSortBy").val() },
                prev_text: "Prev",
                next_text: "Next",
                prev_show_always: false,
                next_show_always: false
            });

        },
    };

    categoryDetails.init();
});
function GetPriceData(price, count, interval) {
    return (parseFloat(price + (count * interval)));

}
function GetPriceDataFloat(price, count, interval) {
    return (parseFloat(price + (count * interval)).toFixed(2));

}

