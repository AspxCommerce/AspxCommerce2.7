(function($) {
    $.MegaCategorySettingView = function(param) {
        param = $.extend
        ({
            Settings: ''
            ,umi:0
        }, param);

        var p = $.parseJSON(param.Settings);
        var umi = $.parseJSON(param.umi);
        function aspxCommonObj() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        }
        var MegaCategorySetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.MegaModulePath + "Services/MegaCategoryWebService.asmx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: MegaCategorySetting.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: MegaCategorySetting.config.contentType,
                    cache: MegaCategorySetting.config.cache,
                    async: MegaCategorySetting.config.async,
                    data: MegaCategorySetting.config.data,
                    dataType: MegaCategorySetting.config.dataType,
                    url: MegaCategorySetting.config.url,
                    success: MegaCategorySetting.config.ajaxCallMode,
                    error: MegaCategorySetting.ajaxFailure
                });
            },
            BindMegaCategorySetting: function() {
                $("#slcMode").val(p.ModeOfView);
                if (p.ModeOfView == 'horizontal') {
                    $("#trEvent").show();
                    $("#slcEffect option[value='show']").hide();
                } else {
                    $("#trDirection").show();
                    $("#slcEffect option[value='show']").hide();
                }
                $("#txtNoOfColumn").val(p.NoOfColumn);                
                $("#chkShowImage").prop("checked", p.ShowCatImage);
                $("#chkShowSubCatImage").prop("checked", p.ShowSubCatImage);
                $("#slcSpeed").val(p.Speed);
                $("#slcEffect").val(p.Effect);
                $("#slcDirection").val(p.Direction);
                $("#slcEvent").val(p.EventMega);
            },
            MegaCategorySettingUpdate: function() {
                var subImage;
                var catImage;
                var mode = $("#slcMode").val();
                var column = $("#txtNoOfColumn").val();
                var speed = $("#slcSpeed").val();
                var effect = $("#slcEffect").val();
                var direction = $("#slcDirection").val();
                var event = $("#slcEvent").val();
                if ($('#chkShowImage').is(':checked')) {
                    catImage = 'true';
                } else {
                    catImage = 'false';
                }
                if ($('#chkShowSubCatImage').is(':checked')) {
                    subImage = 'true';
                } else {
                    subImage = 'false';
                }
                var settingKeys = "ModeOfView*NoOfColumn*ShowCategoryImage*ShowSubCategoryImage*Speed*Effect*Direction*EventMega";
                var settingValues = mode + "*" + column + "*" + catImage + "*" + subImage + "*" + speed + "*" + effect + "*" + direction + "*" + event;
                var param = JSON2.stringify({ SettingValues: settingValues, SettingKeys: settingKeys, aspxCommonObj: aspxCommonObj() });
                this.config.method = "MegaCategorySettingUpdate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = MegaCategorySetting.MegaCategorySettingSuccess;
                this.ajaxCall(this.config);
            },
            MegaCategorySettingSuccess: function() {
                SageFrame.messaging.show(getLocale(AspxMegaCategory, "Setting Saved Successfully"), "Success");
            },

            init: function () {               
                MegaCategorySetting.BindMegaCategorySetting();
                $("#btnMenuCatSave").click(function() {                   
                    if (v.form()) {
                        MegaCategorySetting.MegaCategorySettingUpdate();
                    }
                    else {
                        SageFrame.messaging.show(getLocale(AspxMegaCategory, "Please fill all the required form."), "Alert");
                    }
                });
                $("#slcMode").on("change", function() {
                    var mode = $(this).val();
                    if (mode == 'horizontal') {
                        $("#trEvent").show();
                        $("#trDirection").hide();
                        $("#slcEffect option[value='show']").hide();
                    } else {
                        $("#trEvent").hide();
                        $("#trDirection").show();
                        $("#slcEffect option[value='show']").show();
                    }
                });

                var v = $("#form1").validate({
                    messages: {
                        NoOfColumn: {
                            required: '*'
                        }
                    },
                    rules:
                        {
                            NoOfColumn: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            }
                        }
                });
            }
        };
        MegaCategorySetting.init();
    };
    $.fn.MegaCategorySetting = function(p) {
        $.MegaCategorySettingView(p);
    };
})(jQuery);