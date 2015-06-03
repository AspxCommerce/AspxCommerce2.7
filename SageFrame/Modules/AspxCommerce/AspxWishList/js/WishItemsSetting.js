(function ($) {
    $.WishItemsListSettingSetting = function (param) {
        param = $.extend({
            wishItemSetting: {}
        }, param);
        var p = $.parseJSON(param.wishItemSetting);
        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonObj;
        };
        wishItemsSettings = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.WishItemsModulePath + "Service/Service.asmx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: wishItemsSettings.config.type,
                    contentType: wishItemsSettings.config.contentType,
                    cache: wishItemsSettings.config.cache,
                    async: wishItemsSettings.config.async,
                    url: wishItemsSettings.config.url,
                    data: wishItemsSettings.config.data,
                    dataType: wishItemsSettings.config.dataType,
                    success: wishItemsSettings.config.ajaxCallMode,
                    error: wishItemsSettings.config.ajaxFailure
                });
            },
            GetWishItemsSetting: function () {
                var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
                this.config.method = "GetWishItemsSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = wishItemsSettings.BindWishItemsSetting;
                this.ajaxCall(this.config);            },
            BindWishItemsSetting: function () {
                $("#chkEnableImageInWishList").prop("checked", p.IsEnableImageInWishlist)
                $("#txtNoOfRecentWishItems").val(p.NoOfRecentAddedWishItems);
                $("#txtWishItemsPageName").val(p.WishListPageName);
            },
            WishItemsSettingUpdate: function () {
                var isEnableImageInWishlist = $("#chkEnableImageInWishList").prop("checked");
                var noOfRecentAddedWishItems = $("#txtNoOfRecentWishItems").val();
                var wishListPageName = $("#txtWishItemsPageName").val();
                var settingKeys = "IsEnableImageInWishlist*NoOfRecentAddedWishItems*WishListPageName";
                var settingValues = isEnableImageInWishlist + "*" + noOfRecentAddedWishItems + "*" + wishListPageName;
                var wishItemsSettingListObj = {
                    SettingKey: settingKeys,
                    SettingValue: settingValues
                };
                var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), wishlistSettingObj: wishItemsSettingListObj });
                this.config.method = "SaveAndUpdateWishItemsSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = wishItemsSettings.WishItemsSettingSuccess;
                this.ajaxCall(this.config);
            },
            WishItemsSettingSuccess: function (data) {
                SageFrame.messaging.show(getLocale(AspxWishItems, "Setting Saved Successfully"), "Success");
            },
            init: function () {
                wishItemsSettings.BindWishItemsSetting();
                $("#btnWishItemsSettingSave").click(function () {
                    wishItemsSettings.WishItemsSettingUpdate();
                });
            }
        };
        wishItemsSettings.init();
    };
    $.fn.WishItemsSetting = function (p) {
        $.WishItemsListSettingSetting(p);
    };
})(jQuery);