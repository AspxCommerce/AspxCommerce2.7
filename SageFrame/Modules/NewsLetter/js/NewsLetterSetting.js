$(function() {

    var nlSetting = {
        config: {
            baseURL: NewsLetterPath + 'Services/NewsLetterWebService.asmx/',
            method: "",
            ModulePath: '',
            PortalID: PortalID,
            UserModuleID: UserModuleID,
            UserName: UserName

        },
        init: function() {
            nlSetting.GetSetting();
        },
        SaveNLSetting: function() {
            var settKey = new Array();
            var settValue = new Array();
            var settingKey = '';
            var settingValue = '';
            settKey.push("ModuleHeader");
            settKey.push("ModuleDescription");
            settKey.push("UnSubscribePageName");
            settKey.push("IsMobileSubscription");
            settValue.push($("#txtNewsLetterHeader").val());
            settValue.push($("#txtNLDescription").val());
            settValue.push($("#txtPageName").val());
            settValue.push($('input:checkbox[name=ByMobile]').attr('checked'));
            settingKey = settKey.join(",");
            settingValue = settValue.join(",");
            var param = JSON2.stringify({
                SettingKey: settingKey,
                SettingValue: settingValue,
                UserModuleID: nlSetting.config.UserModuleID,
                PortalID: nlSetting.config.PortalID,
                UserName: SageFrameUserName,
                secureToken: SageFrameSecureToken
            });
            $.ajax({
                type: "POST",
                url: nlSetting.config.baseURL + "SaveNLSetting",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    SageFrame.messaging.show("Setting Saved Successfully", "Success");
                },
                error: function() {
                    //alert('error');
                }
            });
        },
        GetSetting: function() {
            var param = JSON2.stringify({
                UserModuleID: nlSetting.config.UserModuleID,
                PortalID: nlSetting.config.PortalID,
                UserName: SageFrameUserName,
                secureToken: SageFrameSecureToken
            });
            $.ajax({
                type: "POST",
                url: nlSetting.config.baseURL + "GetNLSetting",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {

                    $.each(data.d, function(index, item) {
                        $("#txtNewsLetterHeader").val(item.ModuleHeader);
                        $("#txtNLDescription").val(item.ModuleDescription);
                        $("#txtPageName").val(item.UnSubscribePageName);
                    });
                },
                error: function() {
                    //alert('error');
                }
            });
        }

    };
    nlSetting.init();
    $("#btnSaveNLSetting").on("click", function() {
        var v = $('#form1').validate({
            rules: {
                Header: { required: true },
                Description: { required: true }

            },
            messages: {
                Header: { required: '*' },
                Description: { required: '*' }

            }
        });

        if (v.form()) {
            nlSetting.SaveNLSetting();

        }

        return true;


    });

});