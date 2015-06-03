
(function ($) {
    $.AdvanceSearchSet = function (p) {
        p = $.extend({
            AdvanceSearchModulePath: ""
        }, p);

        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
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
                baseURL: p.AdvanceSearchModulePath + "AdvanceSearchHandler.ashx/",
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
                        $("#chkEnableAdvanceSearch").prop("checked", item.IsEnableAdvanceSearch);
                        $("#chkEnableBrandSearch").prop("checked", item.IsEnableBrandSearch);
                        $("#txtNoOfItemsInARow").val(item.NoOfItemsInARow);
                        $("#txtAdvanceSearchPage").val(item.AdvanceSearchPageName);
                    });
                }
            },
            CompositeViewListSettingUpdate: function () {
                var settingKeys = "IsEnableAdvanceSearch*IsEnableBrandSearch*NoOfItemsInARow*AdvanceSearchPageName";

                var isEnableAdvanceSearch = $("#chkEnableAdvanceSearch").prop("checked");
                var isEnableBrandSearch = $("#chkEnableBrandSearch").prop("checked");
                var noOfItemsInARow = $("#txtNoOfItemsInARow").val();
                var advanceSearchPageName = $("#txtAdvanceSearchPage").val();


                var settingValues = isEnableAdvanceSearch + "*" + isEnableBrandSearch + "*" + noOfItemsInARow + "*" + advanceSearchPageName;
                var AdvanceSearchSettingKeyValuePairObj = {
                    SettingKey: settingKeys,
                    SettingValue: settingValues
                };
                var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), advanceObj: AdvanceSearchSettingKeyValuePairObj });
                this.config.method = "SaveAndUpdateAdvanceSearchSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = AdvanceSearchSetting.AdvanceSearchSettingSuccess;
                this.ajaxCall(this.config);
            },
            AdvanceSearchSettingSuccess: function (data) {
                SageFrame.messaging.show(getLocale(AdvanceSearchLang, "Setting Saved Successfully"), "Success");
            },

            Init: function () {
                AdvanceSearchSetting.GetAdvanceSearchSetting();
                $("#btnAdvanceSearchSettingSave").click(function () {
                    AdvanceSearchSetting.CompositeViewListSettingUpdate();
                });
            }
        };
        AdvanceSearchSetting.Init();
    };
    $.fn.AdvanceSearchSetting = function (p) {
        $.AdvanceSearchSet(p);
    };
})(jQuery);