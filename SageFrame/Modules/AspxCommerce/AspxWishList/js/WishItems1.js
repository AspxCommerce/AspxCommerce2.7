var wishItemsFront = "";
(function ($) {
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
    var ItemIDs = 0; var ItemComments = "";
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
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
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
            WishItemAPI.Count();
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
                wishItem += " <thead><tr><td></td></tr></thead>";
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
                wishItem += '<div class=\"cssClassWishLink\"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + myWishListLink + '" id="lnkGoToWishlist"> <span class="gowishlist">' + getLocale(AspxWishItems, "Go to Wishlist") + '</span></a></div>';
                wishItem += "</table></div>";

            } else {
                $("#tblWishItem>tbody").html('<tr><td><span class=\"cssClassNotFound\">' + getLocale(AspxWishItems, "Your Wishlist is empty!") + '</span></tr></td>');
                $(".cssClassWishLink").html('');
            }
            $('#wishItem').html(wishItem);
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
                this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ wishItemID: id, aspxCommonObj: AspxCommonObj() });
                this.config.ajaxCallMode = WishItems.BindWishItemsAfterDelete;
                this.ajaxCall(this.config);
            }
        },

        BindMyWishList: function () {
            var isShowAll = 0;
            var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), flagShowAll: isShowAll, count: wishlistcount });
            this.config.method = "AspxCommerceWebService.asmx/GetRecentWishItemList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = WishItems.BindRecentWishItemList;
            this.ajaxCall(this.config);
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
                    Items += '<a href="' + href + '">' + response.ItemName + '( ' + response.ItemCostVariantValue + ' )' + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="WishItems.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
                else {
                    Items += '<a href="' + href + '">' + response.ItemName + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="WishItems.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
            } else {
                Items = '<tr class="sfOdd" id="trWishItem_' + response.ItemID + '"><td class="cssClassWishItemDetails">';
                if (showWishItemImage.toLowerCase() == 'true') {
                    Items += '<a href ="' + href + '"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/></div></a>';
                }
                if (response.ItemCostVariantValue != '') {
                    Items += '<a href="' + href + '">' + response.ItemName + '( ' + response.ItemCostVariantValue + ' )' + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="WishItems.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                }
                else {
                    Items += '<a href="' + href + '">' + response.ItemName + '</a></br><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img onclick="WishItems.DeleteWishListItem(' + response.WishItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
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
            if (allowWishItemList.toLowerCase() == 'true' && eval(wishlistcount) > 0) {
                $('#divRecentlyAddedWishList').show();
                $('#divRecentlyAddedWishList .cssClassImage img[title]').tipsy({ gravity: 'n' });
            }
        }
    };
    WishItems.Init();

    wishItemsFront = function () {
        return {
            BindWishList: WishItems.BindMyWishList
        }
    }();
})(jQuery);