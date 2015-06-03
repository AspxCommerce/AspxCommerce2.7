var arrItemListType = new Array();
var arrResultToBind = new Array();
var itemTemplateViewScriptArr = [];
IsExistedCategory = function(arr, cat) {
    var isExist = false;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == cat) {
            isExist = true;
            break;
        }
    }
    return isExist;
};
Array.prototype.clean = function(deleteValue) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == deleteValue) {
            this.splice(i, 1);
            i--;
        }
    }
    return this;
};

function GetItemTemplateScipt(key) {
    var val = "";
    if (itemTemplateViewScriptArr.length > 0) {

        for (var i = 0; i < itemTemplateViewScriptArr.length; i++) {
            if (itemTemplateViewScriptArr[i].TemplateKey == key) {
                val = itemTemplateViewScriptArr[i].TemplateValue;
                break;
            }
        }
    }
    return val;
}

function GridView(appendDiv, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow) {
    $("#" + appendDiv).html('');
    var itemIds = [];
    var tempScriptGridView = GetItemTemplateScipt('scriptResultGrid');
    $.each(arrResultToBind, function (index, value) {
        if (!IsExistedCategory(itemIds, value.ItemID)) {
            itemIds.push(value.ItemID);
            var imagePath = itemImagePath + value.BaseImage;
            if (value.BaseImage == "") {
                imagePath = noImagePathDetail;
            }
            else {
                imagePath = imagePath.replace('uploads', 'uploads/Medium')

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
                // loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                alternateText: value.Name,
                listPrice: value.ListPrice,
                price: value.Price,
                isCostVariant: '"' + value.IsCostVariantItem + '"',
                shortDescription: Encoder.htmlDecode(value.ShortDescription)               
            }];
            // $.tmpl("scriptResultGridTemp", items).appendTo("#" + appendDiv);
            $.tmpl(tempScriptGridView, items).appendTo("#" + appendDiv);
            // to bind dynamic attribute'
          
            if (value.AttributeValues != null) {
                if (value.AttributeValues != "") {
                    var attrHtml = '';
                    attrValues = [];
                    attrValues = value.AttributeValues.split(',');
                    attrHtml ="<div class='cssGridDyanamicAttr'>";
                    for (var i = 0; i < attrValues.length; i++) {                       
                        var attributes = [];
                        attributes = attrValues[i].split('#');
                        var attributeName = attributes[0];
                        var attributeValue = attributes[1];
                        var inputType =parseInt(attributes[2]);
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
            //   $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            if (value.ListPrice == "") {
                $(".cssRegularPrice_" + value.ItemID + "").remove();
            }
            
            if (allowAddToCart) {
                if (allowOutStockPurchase.toLowerCase() == 'false') {
                    if (value.IsOutOfStock) {
                        // $(".cssClassAddtoCard_" + value.ItemID + " span").html(getLocale(AspxTemplateLocale, 'Out Of Stock'));
                        // $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                        // $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                        // $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');

                        $(".cssClassAddtoCard_" + value.ItemID + " span").html(getLocale(AspxTemplateLocale, 'Out Of Stock'));
                        $(".cssClassAddtoCard_" + value.ItemID).find("div").addClass("cssClassOutOfStock");
                        $(".cssClassAddtoCard_" + value.ItemID).find('label').removeClass('i-cart cssClassGreenBtn');
                        $(".cssClassAddtoCard_" + value.ItemID + "button").removeAttr("onclick");
                    }
                }
            }
            else { $(".cssClassAddtoCard_" + value.ItemID).hide(); }
            if (value.ItemTypeID == 5) {
                $(".cssClassCompareAttributeClass >span").each(function () {
                    if ($(this).html() == "Is In Stock: ") {
                        $(this).parents("tr").find("td").eq(index+1).html("No");
                    }
                });
                $(".cssClassAddtoCard_" + value.ItemID + "").parents(".cssLatestItemInfo").find(".cssClassProductRealPrice").prepend(getLocale(AspxTemplateLocale, "Starting At"));
            }
        }
        
    });
    $(".cssClassDisplayResult").wrapInner("<div class='cssProductGridWrapper'></div>");
        var x = 0;
        $('#' + appendDiv + ' .cssClassProductsBox ').each(function() {
            x++;
           
            $(this).attr('xid',x);
            if ((x % noOfItemsInRow) == 0) {
                $(this).addClass('cssClassNoMargin');
            }
        }); 
        var $container = $(".cssProductGridWrapper");
        $container.imagesLoaded(function () {
            $container.masonry({
                itemSelector: '.cssClassProductsBox',
                EnableSorting: false
            });
        });
        $('.cssClassProductPicture a img[title]').tipsy({ gravity: 'n' });
    }

    function ListView(appendDiv, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow) {
        $("#" + appendDiv).html('');
        var itemIds = [];
        var tempScriptListView = GetItemTemplateScipt('scriptResultList');
        $.each(arrResultToBind, function (index, value) {
            if (!IsExistedCategory(itemIds, value.ItemID)) {
                itemIds.push(value.ItemID);
                var imagePath = itemImagePath + value.BaseImage;
                if (value.BaseImage == "") {
                    imagePath = noImagePathDetail;
                }
                else {
                    imagePath = imagePath.replace('uploads', 'uploads/Medium')
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
                    // loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                    alternateText: value.Name,
                    listPrice: value.ListPrice,
                    price: value.Price,
                    isCostVariant: '"' + value.IsCostVariantItem + '"',
                    shortDescription: Encoder.htmlDecode(value.ShortDescription)
                }];

                //$.tmpl("scriptResultListTemp", items).appendTo("#" + appendDiv);
                $.tmpl(tempScriptListView, items).appendTo("#" + appendDiv);

                // to bind dynamic attribute
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
            
                if (value.ListPrice == "") {
                    $(".cssRegularPrice_" + value.ItemID + "").remove();
                }
                if (allowAddToCart.toLowerCase() == 'true') {
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if (value.IsOutOfStock) {
                            $(".cssClassAddtoCard_" + value.ItemID + " span").html(getLocale(AspxTemplateLocale, 'Out Of Stock'));
                            $(".cssClassAddtoCard_" + value.ItemID).find("div").addClass("cssClassOutOfStock");
                            $(".cssClassAddtoCard_" + value.ItemID).find('label').removeClass('i-cart cssClassGreenBtn');
                            $(".cssClassAddtoCard_" + value.ItemID + "button").removeAttr("onclick");
                        }
                    }
                }
                else { $(".cssClassAddtoCard_" + value.ItemID).hide(); }
                if (value.ItemTypeID == 5) {
                    $(".cssClassAddtoCard_" + value.ItemID + "").parents(".cssLatestItemInfo").find(".cssClassProductRealPrice").prepend(getLocale(AspxTemplateLocale, "Starting At"));
                }
            }
        });           
            $('.cssClassProductPicture a img[title]').tipsy({ gravity: 'n' });
        }

        function BindResults(appendDiv, divViewAs, ddlViewAs, mainVar, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow, displayMode) {
            var viewAsOption = '';
            if (displayMode == "icon") {
                viewAsOption = $("#" + divViewAs).find('a.sfactive').attr("displayId");
                if (typeof viewAsOption == 'undefined') {
                    $("#" + divViewAs).find('a:eq(0)').addClass("sfactive");
                    viewAsOption = $("#" + divViewAs).find('a.sfactive').attr("displayId");
                }
            }
            else {
                viewAsOption = $("#" + ddlViewAs).val();
            }
            if (arrResultToBind.length > 0) {

                switch (viewAsOption) {
                    case '1':
                        GridView(appendDiv, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow);
                        break;
                    case '2':
                        ListView(appendDiv, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow);
                        break;
                }
                if (displayMode == "dropdown") {
                    $("#" + ddlViewAs).val(viewAsOption);
                }
            }
            var cookieCurrency = $("#ddlCurrency").val();
            Currency.currentCurrency = BaseCurrency;
            Currency.convertAll(Currency.currentCurrency, cookieCurrency);
        }

        function BindTemplateDetails(appendDiv, viewOptionDiv, divViewAs, ddlViewAs, ddlSortBy, divSearchPageNumber, divPagination, ddlPageSize, currentPage, msg, varFunctionName, mainVar, allowAddToCart, allowOutStockPurchase, noImagePathDetail, noOfItemsInRow, displayMode, templateArray) {
            var rowTotal = 0;
            itemTemplateViewScriptArr = templateArray;
            $("#" + appendDiv).show();
            arrItemListType.length = 0;
            var length = msg.d.length;
            if (length > 0) {
                $("#" + appendDiv).html('');
                $("#" + viewOptionDiv).show();
                $("#" + divSearchPageNumber).show();
                $("#" + divViewAs).val(1);
                var itemIds = [];
                var headerElements = '';
                var imgCount = 0;
                var value;
                
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    rowTotal = value.RowTotal;
                    if (value.ItemID != null) {
                        if (!IsExistedCategory(itemIds, value.ItemID)) {
                            itemIds.push(value.ItemID);
                            arrItemListType.push(value);
                        }
                    }

                };
                if (arrItemListType.length > 0) {
                    var items_per_page = $('#' + ddlPageSize).val();
                    $("#" + divPagination).pagination(rowTotal, {
                        // callback: categoryDetails.pageselectCallback,
                        items_per_page: items_per_page,
                        //num_display_entries: 10,
                        current_page: currentPage,
                        callfunction: true,
                        function_name: { name: varFunctionName, limit: $('#' + ddlPageSize).val(), sortBy: $('#' + ddlSortBy).val() },
                        prev_text: "Prev",
                        next_text: "Next",
                        prev_show_always: false,
                        next_show_always: false
                    });

                    var max_elem = arrItemListType.length;
                    arrResultToBind.length = 0;
                    // Iterate through a selection of the content and build an HTML string
                    for (var i = 0; i < max_elem; i++) {
                        arrResultToBind.push(arrItemListType[i]);
                    }

                    //BIND FUN
                    BindResults(appendDiv, divViewAs, ddlViewAs, mainVar, allowAddToCart, allowOutStockPurchase,  noImagePathDetail, noOfItemsInRow, displayMode);
                    $("#" + viewOptionDiv).show();
                    $("#" + divPagination).show();
                } else {
                    $("#" + viewOptionDiv).hide();
                    $("#" + divSearchPageNumber).hide();
                    $("#" + appendDiv).html("<span class='cssClassNotFound'>" + getLocale(CoreJsLanguage, "No items found!") + "</span>");
                }
            } else {
                $("#" + viewOptionDiv).hide();
                $("#" + divSearchPageNumber).hide();
                $("#" + appendDiv).html("<span class='cssClassNotFound'>" + getLocale(CoreJsLanguage, "No items found!") + "</span>");
            }
        }

        function CreateDdlPageSizeOption(dropDownId) {
            $("#" + dropDownId).html('');
            var optionalSearchPageSize = '';
            optionalSearchPageSize += "<option data-html-text='8' value='8'>" + 8 + "</option>";
            optionalSearchPageSize += "<option data-html-text='16' value='16'>" + 16 + "</option>";
            optionalSearchPageSize += "<option data-html-text='24' value='24'>" + 24 + "</option>";
            optionalSearchPageSize += "<option data-html-text='32' value='32'>" + 32 + "</option>";
            optionalSearchPageSize += "<option data-html-text='40' value='40'>" + 40 + "</option>";
            optionalSearchPageSize += "<option data-html-text='64' value='64'>" + 64 + "</option>";
            $("#" + dropDownId).html(optionalSearchPageSize);
        }

        function createDropDown(ddlDropdown, divViewAs, option, displayMode) {
            var templateView = '';
            if (option.toLowerCase() == 'sortby') {
                $("#" + ddlDropdown).html('');
                if (sortByOptions != "") {
                    var sortByOption = sortByOptions.split(',').clean('');
                    $.each(sortByOption, function (i) {
                        var sortByOption1 = sortByOption[i].split('#');
                        var displayOptions = "<option data-html-text='" + sortByOption1[1] + "' value=" + sortByOption1[0] + ">" + sortByOption1[1] + "</option>";
                        $("#" + ddlDropdown).append(displayOptions);
                    });
                    $("#" + ddlDropdown).val(sortByOptionsDefault);
                    $("#" + divViewAs).show(); //TO BE REMOVED}
                }
            }
            else if (option.toLowerCase() == 'viewas') {
                if (displayMode.toLowerCase() == "dropdown") {
                    $("#" + ddlDropdown).html('');
                    if (viewAsOptions != "") {
                        var viewAsOption = viewAsOptions.split(',').clean('');
                        $.each(viewAsOption, function (i) {
                            var viewAsOption1 = viewAsOption[i].split('#');
                            var displayOptions = "<option value=" + viewAsOption1[0] + ">" + viewAsOption1[1] + "</option>";
                            $("#" + ddlDropdown).append(displayOptions);
                        });
                        $("#" + ddlDropdown).val(viewAsOptionsDefault);
                        $("#" + ddlDropdown).show();
                        $("#" + divViewAs).show(); //to be removed
                        //$("#ddlSortBy").MakeFancyItemDropDown();
                        //    categoryDetails.LoadAllCategoryContents(1, parseInt($("#ddlPageSize").val()), 0, $("#ddlSortBy option:selected").val());
                    }
                }
                else if (displayMode.toLowerCase() == "icon") {
                    $("#" + divViewAs).find('h4:first').remove();
                    if (viewAsOptions != "") {
                        var viewAsOption = viewAsOptions.split(',').clean('');
                        $.each(viewAsOption, function (i) {
                            var viewAsOption1 = viewAsOption[i].split('#');
                            var displayOptions = "<a class='cssClass" + viewAsOption1[1] + " i-" + viewAsOption1[1] + "' id='view_" + viewAsOption1[0] + "' displayId='" + viewAsOption1[0] + "'   title=" + viewAsOption1[1] + "></a>";
                            $("#" + divViewAs).append(displayOptions);
                        });
                        //$('#' + templateView.vars.divSort + 'a[title]').tipsy({ gravity: 'n' });
                        $("#" + divViewAs).find('a').each(function (i) {
                            if ($(this).attr('displayId') == viewAsOptionsDefault) {
                                $(this).addClass('sfactive');
                            }
                        });
                        $("#" + divViewAs).show();
                        $("#" + divViewAs + " a[title]").tipsy({ gravity: 'n' });
                    }

                }
            }
        }

                           