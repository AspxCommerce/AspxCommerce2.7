
//function ItemViewList(appendDiv, pageSize, hdnPrice, hdnWeight, paging, pageNum, data, arrItemListType, rowTotal, allowOutStockPurchase, allowWishListLatestItem, varFunction, currentpage, mainVar, storeId, portalId, userName, cultureName, costVarIDArr, calledID, sortByID, imageSize, customerId, sessionCode, userFriendlyURL) {
function ItemViewList(appendDiv, pageSize, paging, pageNum, data, arrItemListType, rowTotal, allowOutStockPurchase, varFunction, currentpage, mainVar, costVarIDArr, calledID, sortByID, imageSize) {
    arrItemListType.length = 0;
    if (data.d.length > 0) {
        var dropDownID = '';
        $('.' + appendDiv).html('');
        var html = '<table><tr><td>';
        $.each(data.d, function (index, value) {
            rowTotal = value.RowTotal;
            arrItemListType.push(value.ItemID);
            var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(value.SKU) + pageExtension;
            var imagePath = itemImagePath + value.ImagePath;
            if (value.ImagePath == "" || value.ImagePath == null) {
                imagePath = noImageCategoryDetailPath;
            }
            if (value.AlternateText == "" || value.AlternateText == null) {
                value.AlternateText = value.Name;
            }
           
            if (value.IsFeatured == "Yes" || value.IsFeatured == "True") {
                html += '<div id="product_' + value.ItemID + '" class="classInfo"><div  id="productImageWrapID_' + value.ItemID + '" class="itemImageClass"><a href="' + hrefItem + '"><img class="lazy"  alt="' + value.AlternateText + '"  title="' + value.AlternateText + '" src=' + aspxRootPath + imagePath.replace('uploads', 'uploads/' + imageSize + '') + ' /></a>';
                if (imageSize == "Small") {
                    html += '<div class="classIsFeatureSmall"></div>';
                }
                else {
                    html += '<div class="classIsFeatureMedium"></div>';
                }
                if (value.IsSpecial == "Yes" || value.IsSpecial == "True") {
                    if (imageSize == "Small") {
                        html += '<div class="classIsSpecialSmall"></div>';
                    } else {
                        html += '<div class="classIsSpecialMedium"></div>';
                    }
                }
                html += '</div>';
            } else {
                html += '<div id="product_' + value.ItemID + '" class="classInfo"><div  id="productImageWrapID_' + value.ItemID + '" class="itemImageClass"><a href="' + hrefItem + '"><img class="lazy"  alt="' + value.AlternateText + '"  title="' + value.AlternateText + '" src=' + aspxRootPath + imagePath.replace('uploads', 'uploads/' + imageSize + '') + ' /></a>';
                if (value.IsSpecial == "Yes" || value.IsSpecial == "True") {
                    if (imageSize == "Small") {
                        html += '<div class="classIsSpecialSmall"></div>';
                    } else {
                        html += '<div class="classIsSpecialMedium"></div>';
                    }
                }
                html += '</div>';
            }
            //-----------------------------Added For Group Items(ItemTypeID=5)----------------------------//
            if (value.ItemTypeID == 5) {
                html += '<div class="itemInfoClass"><ul><li>' + value.Name + '</li><div class="cssClassProductPrice"></div><li class="cssClassProductRealPrice ">' + getLocale(CoreJsLanguage, "Starting At ") + '<span id="spanPrice_' + value.ItemID + '" class="cssClassFormatCurrency">' + parseFloat(value.Price).toFixed(2) + '</span><input type="hidden"  id="hdnPrice_' + value.ItemID + '" class="cssClassFormatCurrency"></li></ul>';
            }
                //-----------------------------*******----------------------------//
            else {
                html += '<div class="itemInfoClass"><ul><li>' + value.Name + '</li><div class="cssClassProductPrice"></div><li class="cssClassProductRealPrice "><span id="spanPrice_' + value.ItemID + '" class="cssClassFormatCurrency">' + parseFloat(value.Price).toFixed(2) + '</span><input type="hidden"  id="hdnPrice_' + value.ItemID + '" class="cssClassFormatCurrency"></li></ul>';
            }
          
            //-----------------------------Added For data content in real time addtocart/outofstock Scenario----------------------------//
            var dataContent = '';
            dataContent += 'data-class="addtoCart" data-type="button" data-ItemTypeID="' + value.ItemTypeID + '" data-ItemID="' + value.ItemID + '" data-addtocart="';
            dataContent += 'addtocart' + value.ItemID + '"data-title="' + value.Name + '"';
            dataContent += "data-onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + value.ItemID + "," + parseFloat(value.Price).toFixed(2) + "," + JSON2.stringify(value.SKU) + "," + 1 + "," + "\"" + value.CostVariants + "\"" + "," + "this);'";

            //-----------------------------*******----------------------------//
            if (allowOutStockPurchase.toLowerCase() == 'false') {
                if (value.IsOutOfStock) {
                    html += "<div " + dataContent + " class=\"sfButtonwrapper cssClassOutOfStock\"><button type=\"button\"><span>" + getLocale(CoreJsLanguage, "Out Of Stock") + "</span></button></div>";
                } else {
                    html += "<div  " + dataContent + "class=\"sfButtonwrapper\"><label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + value.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + value.ItemID + "," + parseFloat(value.Price).toFixed(2) + "," + JSON2.stringify(value.SKU) + "," + 1 + "," + "\"" + value.CostVariants + "\"" + "," + "this);'>" + getLocale(CoreJsLanguage, "Cart +") + "</button></label></div>";
                }
            } else {
                html += "<div " + dataContent + " class=\"sfButtonwrapper\"><label class='i-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + value.ItemID + "\"  onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + value.ItemID + "," + parseFloat(value.Price).toFixed(2) + "," + JSON2.stringify(value.SKU) + "," + 1 + "," + "\"" + value.CostVariants + "\"" + "," + "this );'>" + getLocale(CoreJsLanguage, "Cart +") + "</button></label></div>";
            }
            html += '<div class="classButtons">';

            //if (allowWishListLatestItem.toLowerCase() == 'true') {
                var xx = '';
                if (value.CostVariants != '') {
                    xx = 'yes';
                }
            //'<label class="i-wishlist cssWishListLabel cssClassDarkBtn"><button type="button" id={ID} onclick="WishItemAPI.CheckWishListUniqueness(${Params})" ><span>' + getLocale(AspxApiJs, "WishList +") + '</span></button></label>';
                html += "<div class='classWishlist'><div class=\"cssClassWishListButton\"><label class='i-wishlist cssWishListLabel cssClassDarkBtn'><button type=\"button\" id=\"addWishList\" onclick=AspxCommerce.RootFunction.CheckWishListUniqueness(\"+ value.ItemID + ',' + JSON2.stringify(value.SKU) + \",this')<span>" + getLocale(AspxApiJs, 'WishList +') +"</span></button></label>'</div></div>";
            //}
            html += '</div>';
            html += '<div class="classViewDetails"> <a href="' + hrefItem + '"><span>' + getLocale(CoreJsLanguage, "View Details") + '</span></a></div>'; html += '</div>';
            html += '<div class="clear"></div>';
            html += '</div>';
        });
        html += '</td></tr>';
        html += '</table>';
        $('.' + appendDiv).append(html);

        if (arrItemListType.length > 0) {
            var items_per_page = $('#' + pageSize).val();
            $('#' + paging).pagination(rowTotal, {
                //  callback:'',
                items_per_page: items_per_page,
                //num_display_entries: 10,
                current_page: currentpage,
                callfunction: true,
                function_name: { name: varFunction, limit: $('#' + pageSize).val(), sortBy: $('#' + sortByID).val() },
                prev_text: "Prev",
                next_text: "Next",
                prev_show_always: false,
                next_show_always: false
            });
            $('#' + pageNum).show();
        }
        //   $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        $('.itemImageClass a img[title]').tipsy({ gravity: 'n' });
        $('#' + sortByID).parent('div').show();
    } else {
        var msg = '<span class="cssClassNotFound"><b>' + getLocale(CoreJsLanguage, "This store has no items listed yet!") + '</b></span>';
        $('.' + appendDiv).append(msg);
    }
    var cookieCurrency = $("#ddlCurrency").val();
    Currency.currentCurrency = BaseCurrency;
    Currency.convertAll(Currency.currentCurrency, cookieCurrency);
}

function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A');
}

function ViewDetails(sku) {
    aspxRedirectPath + "item/" + fixedEncodeURIComponent(sku) + pageExtension;
}
  
