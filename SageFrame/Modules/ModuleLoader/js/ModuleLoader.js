(function ($) {
    $.createModuleLoader = function (p) {
        p = $.extend
               ({
                   UserModuleID: '1'
               }, p);

        var ModuleLoader = {
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
                lstPagePermission: [],
                arr: [],
                arrModules: [],
                baseURL: SageFrameAppPath + '/Modules/ModuleLoader/Services/ModuleLoaderWebService.asmx/',
                Path: SageFrameAppPath + '/Modules/ModuleLoader/',
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                UserModuleID: p.UserModuleID
            },
            ajaxSuccess: function (data) {
                switch (ModuleLoader.config.ajaxCallMode) {
                    case 1:
                        $("#divUControl").append(data.d);
                        break;
                }
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ModuleLoader.config.type,
                    contentType: ModuleLoader.config.contentType,
                    cache: ModuleLoader.config.cache,
                    url: ModuleLoader.config.url,
                    data: ModuleLoader.config.data,
                    dataType: ModuleLoader.config.dataType,
                    success: ModuleLoader.ajaxSuccess,
                    error: ModuleLoader.ajaxFailure
                });
            },
            ModuleLoaderWithDynamicControl: function () {
                this.config.method = "ModuleLoaderWithDynamicControl";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    ModuleName: "ModuleLoader",
                    ControlTypeName: "edit",
                    UserModuleID: ModuleLoader.config.UserModuleID
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            ModuleLoaderWithStaticControl: function () {
                this.config.method = "ModuleLoaderWithStaticControl";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    ControlPath: "/Modules/ModuleLoader/ModuleLoaderEdit.ascx",
                    UserModuleID: ModuleLoader.config.UserModuleID
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },

            Init: function () {
                ModuleLoader.BindEvent();
            },

            BindEvent: function () {
                //ModuleLoader.ModuleLoaderWithDynamicControl();
                ModuleLoader.ModuleLoaderWithStaticControl();
            }
        };
        ModuleLoader.Init();
    };
    $.fn.ModuleLoader = function (p) {
        $.createModuleLoader(p);
    };

})(jQuery);