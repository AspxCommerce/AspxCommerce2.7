(function($){
    $.updateSetting = function (p) {
        p = $.extend({
            ModuleServicePath:'',
            ShowCategoryForSearch: '',
            EnableAdvanceSearch: '',
            ShowSearchKeyWord: ''
        }, p);
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var simpleSearchSetting = {
            init: function (config) {
                if (p.ShowCategoryForSearch.toLowerCase() == 'true') {
                    $("#chkShowCatForSearch").prop('checked', 'checked');
                }
                if (p.EnableAdvanceSearch.toLowerCase() == 'true') {
                    $("#chkEnableAdvanceSearch").prop('checked', 'checked');
                }
                if (p.ShowSearchKeyWord.toLowerCase() == 'true') {
                    $("#chkShowSearchKeyWord").prop('checked', 'checked');
                }
                $('#btnSaveSearchSetting').click(function () {
                    p.ShowCategoryForSearch = $("#chkShowCatForSearch").prop("checked");
                    p.EnableAdvanceSearch = $("#chkEnableAdvanceSearch").prop("checked");
                    p.ShowSearchKeyWord = $("#chkShowSearchKeyWord").prop("checked");
                    $.ajax({
                        type: 'post',
                        async: false,
                        url: p.ModuleServicePath + "AspxCommonHandler.ashx/SetSearchSetting",
                        contentType: "application/json;charset=utf-8",
                        data: JSON2.stringify({ searchSettingObj: p, aspxCommonObj: aspxCommonObj }),
                        dataType: 'JSON',
                        success: function () {
                            SageFrame.messaging.show(getLocale(AspxGeneralSearch, 'Setting Saved Successfully'), "Success");
                        },
                        error: ''
                    });
                });
            }
        }
        simpleSearchSetting.init();
    }
    $.fn.SimpleSearchSettingInit = function (p) {
        $.updateSetting(p);
    };
})(jQuery);