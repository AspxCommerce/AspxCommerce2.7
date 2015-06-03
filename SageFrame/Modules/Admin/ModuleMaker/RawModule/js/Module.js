(function ($) {
    $.ModuleName = function (p) {
        var order = 0;
        var level = 0;
        p = $.extend
                ({
                    CultureCode: '',
                    UserModuleID: '1'
                }, p);
        var ModuleName = {
            config: {
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                method: "",
                url: "",
                categoryList: "",
                ajaxCallMode: 0,
                arr: [],
                arrModules: [],
                baseURL: SageFrameAppPath + 'FolderPath/WebService/ModuleName.asmx/',
                Path: SageFrameAppPath + 'FolderPath/',
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                UserModuleID: p.UserModuleID                
            },
            init: function () {             
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ModuleName.config.type,
                    contentType: ModuleName.config.contentType,
                    cache: ModuleName.config.cache,
                    async: ModuleName.config.async,
                    url: ModuleName.config.url,
                    data: ModuleName.config.data,
                    dataType: ModuleName.config.dataType,
                    success: ModuleName.ajaxSuccess,
                    error: ModuleName.ajaxFailure
                });
            },
        }
        ModuleName.init();
    }
    $.fn.ModuleName = function (p) {
        $.ModuleName(p);
    };
})(jQuery);