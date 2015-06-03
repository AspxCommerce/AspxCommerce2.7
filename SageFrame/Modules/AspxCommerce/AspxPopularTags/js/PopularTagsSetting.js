
(function ($) {
    $.PopularTags = function (p) {
        p = $.extend({
            PopularTagsModulePath: ""
        }, p);

        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonObj;
        };

        var PopularTagSetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.PopularTagsModulePath + "PopularTagsHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: PopularTagSetting.config.type,
                    contentType: PopularTagSetting.config.contentType,
                    cache: PopularTagSetting.config.cache,
                    async: PopularTagSetting.config.async,
                    url: PopularTagSetting.config.url,
                    data: PopularTagSetting.config.data,
                    dataType: PopularTagSetting.config.dataType,
                    success: PopularTagSetting.config.ajaxCallMode,
                    error: PopularTagSetting.config.ajaxFailure
                });
            },
            GetPopularTagSettingValues: function () {
                var isEnablePopularTag = $("chkEnablePopularTag").prop("checked");
                var PopularTagCount = $("#txtPopularTagCount").val();
                var PopularTagInARow = $("#txtPopularTagInARow").val();
                var isEnablePopularTagRss = $("#chkEnablePopularTagRss").prop("checked");
                var PopularTagRssCount = $("#txtPopularTagRssCount").val();
                var PopularTagsRssPageName = $("#txtPopularTagRssPageName").val();
                var PopularTagDetailsPage = $("#txtPopularTagDetailPage").val();
                var settingKeys = "IsEnablePopularTag*PopularTagCount*PopularTagInARow*IsEnablePopularTagRss*PopularTagRssCount*PopularTagDetailsPage";
                var settingValues = isEnablePopularTag * PopularTagCount * PopularTagInARow * isEnablePopularTagRss * PopularTagRssCount * PopularTagDetailsPage;
            },
            GetPopularTagSetting: function () {
                var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
                this.config.method = "GetPopularTagsSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = PopularTagSetting.BindPopularTagSetting;
                this.ajaxCall(this.config);
            },
            BindPopularTagSetting: function (data) {
                if (data.d.length > 0) {
                    $.each(data.d, function (index, item) {
                        $("#chkEnablePopularTag").prop("checked", item.IsEnablePopularTag);
                        $("#txtPopularTagCount").val(item.PopularTagCount);
                        $("#txtPopularTagInARow").val(item.TaggedItemInARow);
                        $("#chkEnablePopularTagRss").prop("checked", item.IsEnablePopularTagRss);
                        $("#txtPopularTagRssCount").val(item.PopularTagRssCount);
                        $("#txtPopularTagRssPageName").val(item.PopularTagsRssPageName);
                        $("#txtViewAllTagsPageName").val(item.ViewAllTagsPageName);
                        $("#txtViewTaggedItemPageName").val(item.ViewTaggedItemPageName);
                    });
                }
            },
            PopularTagSettingUpdate: function () {
                var settingKeys = "IsEnablePopularTag*PopularTagCount*TaggedItemInARow*IsEnablePopularTagRss*PopularTagRssCount*PopularTagsRssPageName*ViewAllTagsPageName*ViewTaggedItemPageName";
                var isEnablePopularTag = $("#chkEnablePopularTag").prop("checked");
                var popularTagCount = $("#txtPopularTagCount").val();
                var taggedItemInARow = $("#txtPopularTagInARow").val();
                var isEnablePopularTagRss = $("#chkEnablePopularTagRss").prop("checked");
                var popularTagRssCount = $("#txtPopularTagRssCount").val();
                var popularTagsRssPageName = $("#txtPopularTagRssPageName").val();
                var viewAllTagsPageName = $("#txtViewAllTagsPageName").val();
                var viewTaggedItemPageName = $("#txtViewTaggedItemPageName").val();
                var settingValues = isEnablePopularTag + "*" + popularTagCount + "*" + taggedItemInARow + "*" + isEnablePopularTagRss + "*" + popularTagRssCount + "*" + popularTagsRssPageName + "*" + viewAllTagsPageName + "*" + viewTaggedItemPageName;
                var popularTagSettingKeyValuePairObj = {
                    SettingKey: settingKeys,
                    SettingValue: settingValues
                };
                var param = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), pTSettingList: popularTagSettingKeyValuePairObj });
                this.config.method = "SaveUpdatePopularTagsSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = PopularTagSetting.PopularTagSettingSuccess;
                this.ajaxCall(this.config);
            },
            PopularTagSettingSuccess: function (data) {
                SageFrame.messaging.show(getLocale(AspxPopularTags, "Setting Saved Successfully"), "Success");
            },

            Init: function () {
                PopularTagSetting.GetPopularTagSetting();
                $("#btnPopularTagSettingSave").click(function () {
                    PopularTagSetting.PopularTagSettingUpdate();
                });
            }
        };
        PopularTagSetting.Init();
    };
    $.fn.PopularTag = function (p) {
        $.PopularTags(p);
    };
})(jQuery);