var wishItemsFront = "";
$(function () {
    var AspxCommonObj = function () {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonObj;
    };
    var countryName = countryNameWishList;
    var comment = $("#txtComment").val();
    var ItemIDs = 0;
    var ItemComments = "";
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var wishlistcount = noOfRecentAddedWishItems;
    var WishItems = {
        config: {
            isPostBack: false,
            async: true,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxRootPath + 'Modules/AspxCommerce/AspxWishList/Service/Service.asmx/',
            method: "",
            url: "",
            ajaxCallMode: "",
            error: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: WishItems.config.type,
                contentType: WishItems.config.contentType,
                cache: WishItems.config.cache,
                async: WishItems.config.async,
                url: WishItems.config.url,
                data: WishItems.config.data,
                dataType: WishItems.config.dataType,
                success: WishItems.config.ajaxCallMode,
                error: WishItems.ajaxFailure
            });
        },

        BindWishItemsAfterDelete: function () {
            WishItems.BindMyWishList();
        },

        BindRecentWishItemList: function (msg) {
            $('.wishItem').remove();
            $("#tblWishItem>tbody").html('');
            $("#tblWishItem>thead").html('');
            $(".cssClassWishLink").html('');
            var wishItem = '<div class="cssClassCommonSideBoxTable wishItem">';
            var length = msg.d.length;
            if (length > 0) {
                wishItem += "<table class=\"cssClassMyWishItemTable\" id=\"tblWishItem\" width=\"100%\">";
                wishItem += "<tbody>";
                var x = '';
                var item;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    x += WishItems.BindWishItems(item, index);
                };
                wishItem += x;
                wishItem += "</tbody>";
                var myWishListLink = "";
                if (userFriendlyURL) {
                    myWishListLink = wishListURLSetting + pageExtension;
                } else {
                    myWishListLink = wishListURLSetting;
                }
                wishItem += "</table>";
                wishItem += '<div class=\"cssClassWishLink\"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + myWishListLink + '" id="lnkGoToWishlist"> <span class="gowishlist">' + getLocale(AspxWishItems, "Go to Wishlist") + '</span></a></div>';
                wishItem += "</div>";
                $("#lnkMyWishlist").html("<i class=\"i-mywishlist\"></i>" + getLocale(AspxWishItems, "My Wishlist") + " <span class=\"cssClassTotalCount\">[" + msg.d.length + "]</span>");
            } else {
                wishItem += '<tr><td><span class=\"cssClassNotFound\">' + getLocale(AspxWishItems, "Your Wishlist is empty!") + '</span></tr></td>'
                $(".cssClassWishLink").html('');
            }
            $("#lnkMyWishlist").html("<i class=\"i-mywishlist\"></i>" + getLocale(AspxWishItems, "My Wishlist") + " <span class=\"cssClassTotalCount\">[" + msg.d.length + "]</span>");
            $('#wishItem').html(wishItem);
            var cookieCurrency = $("#ddlCurrency").val();
            Currency.currentCurrency = BaseCurrency;
            Currency.convertAll(Currency.currentCurrency, cookieCurrency);
        },

        DeleteWishListItem: function (itemId) {
            var properties = {
                onComplete: function (e) {
                    WishItems.ConfirmDeleteWishItem(itemId, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxWishItems, "Delete Confirmation") + "</h2><p>" + getLocale(AspxWishItems, "Are you sure you want to delete this wished item?") + "</p>", properties);
        },

        ConfirmDeleteWishItem: function (id, event) {
            if (event) {
                WishItems.config.method = "DeleteWishItem";
                WishItems.config.url = WishItems.config.baseURL + WishItems.config.method;
                WishItems.config.data = JSON2.stringify({ wishItemID: id, aspxCommonObj: AspxCommonObj() });
                WishItems.config.ajaxCallMode = WishItems.BindWishItemsAfterDelete;
                WishItems.ajaxCall(WishItems.config);
            }
        },

        BindMyWishList: function () {
            var isShowAll = 0;
            var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), flagShowAll: isShowAll, count: wishlistcount });
            WishItems.config.method = "GetRecentWishItemList";
            WishItems.config.url = WishItems.config.baseURL + WishItems.config.method;
            WishItems.config.data = param;
            WishItems.config.ajaxCallMode = WishItems.BindRecentWishItemList;
            WishItems.ajaxCall(WishItems.config);
        },

        BindWishItems: function (response, index) {
            var imagePath = itemImagePath + response.ImagePath;
            if (response.ImagePath == "") {
                imagePath = wishListNoImagePath;
            }
            if (response.AlternateText == "") {
                response.AlternateText = response.ItemName;
            }
            var href = '';
            if (response.CostVariantValueIDs == "") {
                href = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension;
            }
            else {
                href = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '?varId=' + response.CostVariantValueIDs + '';
            }
            if (index % 2 == 0) {
                Items = '<tr class="sfEven" id="trWishItem_' + response.ItemID + '"><td class="cssClassWishItemDetails">';
                if (showWishItemImage.toLowerCase() == 'true') {
                    Items += '<a href ="' + href + '"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/></div></a>';
                }
                if (response.ItemCostVariantValue != '') {
                    Items += '<a href="' + href + '">' + response.ItemName + '( ' + response.ItemCostVariantValue + ' )' + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + parseInt(response.Price).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="wishItemsFront.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
                else {
                    Items += '<a href="' + href + '">' + response.ItemName + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + parseInt(response.Price).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="wishItemsFront.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
            } else {
                Items = '<tr class="sfOdd" id="trWishItem_' + response.ItemID + '"><td class="cssClassWishItemDetails">';
                if (showWishItemImage.toLowerCase() == 'true') {
                    Items += '<a href ="' + href + '"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/></div></a>';
                }
                if (response.ItemCostVariantValue != '') {
                    Items += '<a href="' + href + '">' + response.ItemName + '( ' + response.ItemCostVariantValue + ' )' + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + parseInt(response.Price).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="wishItemsFront.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
                else {
                    Items += '<a href="' + href + '">' + response.ItemName + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + parseInt(response.Price).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="wishItemsFront.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }

            }
            return Items;
        },
        Init: function () {
            if (userFriendlyURL) {
                $("#lnkGoToWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + wishListURLSetting + pageExtension);
            } else {
                $("#lnkGoToWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + wishListURLSetting);
            }
            if (eval(wishlistcount) > 0) {
                $('#divRecentlyAddedWishList').show();
                $('#divRecentlyAddedWishList .cssClassImage img[title]').tipsy({ gravity: 'n' });
            }
        }
    };
    WishItems.Init();
    wishItemsFront = function () {
        return {
            BindWishList: WishItems.BindMyWishList,
            DeleteWishListItem: WishItems.DeleteWishListItem
        }
    }();
});