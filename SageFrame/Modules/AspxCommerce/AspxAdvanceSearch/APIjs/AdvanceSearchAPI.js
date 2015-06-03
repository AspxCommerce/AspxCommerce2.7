
(function ($) {
    var advancePageUrlPage = "";
    var isAdvanceSearchEnable = false;
    var AspxCommonObj = function () {
        var aspxCommonObj = {
            StoreID: storeID,
            PortalID: portalID,
            CultureName: cultureName
        };
        return aspxCommonObj;
    };

    var AdvanceSearchSetting = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxRootPath + "Modules/AspxCommerce/AspxAdvanceSearch/AdvanceSearchHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: AdvanceSearchSetting.config.type,
                contentType: AdvanceSearchSetting.config.contentType,
                cache: AdvanceSearchSetting.config.cache,
                async: AdvanceSearchSetting.config.async,
                url: AdvanceSearchSetting.config.url,
                data: AdvanceSearchSetting.config.data,
                dataType: AdvanceSearchSetting.config.dataType,
                success: AdvanceSearchSetting.config.ajaxCallMode,
                error: AdvanceSearchSetting.config.ajaxFailure
            });
        },

        //GetAdvanceSearchSetting: function () {
        //    var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
        //    this.config.method = "GetAdvanceSearchSetting";
        //    this.config.url = this.config.baseURL + this.config.method;
        //    this.config.data = param;
        //    this.config.ajaxCallMode = AdvanceSearchSetting.BindAdvanceSearchSetting;
        //    this.ajaxCall(this.config);
        //},
        BindAdvanceSearchSetting: function (data) {
            if (data.d.length > 0) {
                $.each(data.d, function (index, item) {
                    advancePageUrlPage = item.AdvanceSearchPageName;
                    isAdvanceSearchEnable = item.IsEnableAdvanceSearch;
                });
            }
        },

        Init: function () {
            $("#lnkAdvanceSearch").html('Advanced Search');
            var userFriendlyURL = true;
            $("#lnkAdvanceSearch").click(function () {
                if (userFriendlyURL) {
                    window.location.href = aspxRedirectPath + advancePageUrlPage + pageExtension;
                } else {
                    window.location.href = aspxRedirectPath + advancePageUrlPage;
                }
            });
        }
    }
    AdvanceSearchSetting.Init();
})(jQuery);