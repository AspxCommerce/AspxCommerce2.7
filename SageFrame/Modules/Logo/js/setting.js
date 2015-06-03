var LogoSetting = {
    config: {
        moduleId: 0,
        portalId: 0,
        type: "POST",
        async: false,
        contentType: "application/json;charset=uf=8",
        dataType: "json",
        serviceUrl: '',
        handlerUrl: '',
        culture: ''
    },
    GetData: function() {
        var myData = JSON2.stringify({ userModuleID: LogoSetting.config.moduleId,
            portalID: LogoSetting.config.portalId,
            CultureCode: LogoSetting.config.culture,
            UserName: SageFrameUserName,
            secureToken: SageFrameSecureToken
        });
        $.ajax({
            type: LogoSetting.config.type,
            url: LogoSetting.config.serviceUrl + "GetLogoData",
            data: myData,
            contentType: LogoSetting.config.contentType,
            dataType: LogoSetting.config.dataType,
            success: function(data) {
                if (data.d != null) {
                    var value = eval(data.d);
                    $("#txtLogoText").text(data.d.LogoText);
                    if (value.LogoPath != "")
                        $("#divLogoIcon").html('<img src="' + resolvedUrl + value.LogoPath + '"/><i class="icon-close" />');
                    $('#txtSlogan').text(data.d.Slogan);
                    $('#txtUrl').val(data.d.url);
                }
            },
            error: function() {
                alert("get logo error");
            }
        });
    },
    Save: function() {
        var logoText = $("#txtLogoText").val();
        var logoPath;
        var logoIcon = $("#divLogoIcon").html();
        var slogan = $('#txtSlogan').val();
        var url = $('#txtUrl').val();
        if (logoIcon.length > 0) {
            logoPath = $("#divLogoIcon").find('img').attr("src").replace(resolvedUrl, '');
        }
        else {
            logoPath = '';
        }
        var myData = JSON2.stringify({ logoText: logoText,
            logoPath: logoPath,
            userModuleID: LogoSetting.config.moduleId,
            portalID: LogoSetting.config.portalId,
            Slogan: slogan,
            URL: url,
            CultureCode: LogoSetting.config.culture,
            UserName: SageFrameUserName,
            secureToken: SageFrameSecureToken
        });
        $.ajax({
            type: LogoSetting.config.type,
            url: LogoSetting.config.serviceUrl + "SaveLogoSettings",
            data: myData,
            contentType: LogoSetting.config.contentType,
            dataType: LogoSetting.config.dataType,
            success: function(data) {
                SageFrame.messaging.show("Logo saved successfully", "Success");
            },
            error: function() {
                alert("error");
            }
        });
    },
    DeleteIcon: function(IconPath) {
        $.ajax({
            type: LogoSetting.config.type,
            url: LogoSetting.config.serviceUrl + "DeleteIcon",
            data: JSON2.stringify({
                IconPath: IconPath,
                userModuleID: LogoSetting.config.moduleId,
                CultureCode: LogoSetting.config.culture,
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                secureToken: SageFrameSecureToken
            }),
            contentType: LogoSetting.config.contentType,
            dataType: LogoSetting.config.dataType,
            success: function(msg) {
                alert("Delete Success");
            }
        });
    }
};
$(function() {
    $("#btnSaveLogo").bind("click", function() {
        LogoSetting.Save();
    });
    LogoSetting.config.moduleId = moduleID;
    LogoSetting.config.portalId = portalID;
    LogoSetting.config.culture = culture;
    LogoSetting.config.serviceUrl = currentDirectory + "LogoWebService.asmx/";
    LogoSetting.config.handlerUrl = currentDirectory + "LogoHandler.ashx";
    LogoUploader();
    LogoSetting.GetData();
    $('#divLogoIcon').on('click', 'i.icon-close', function() {
        var filepath = $(this).prev("img").attr("src");
        var filename = SageFrame.utils.GetFileNameOnly(filepath);
        LogoSetting.DeleteIcon(filename);
        $('#divLogoIcon').html('');
    });
});
function LogoUploader() {
    var upload = new AjaxUpload($('#fluLogo'), {
        action: LogoSetting.config.handlerUrl +'?userModuleId=' + LogoSetting.config.moduleId + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
        name: 'myfile[]',
        multiple: false,
        data: {           
        },
        autoSubmit: true,
        responseType: 'json',
        onChange: function(file, ext) {
        },
        onSubmit: function(file, ext) {
        },
        onComplete: function(file, response) {
            var res = eval(response);
            if (res.Message != null && res.Status > 0) {
                AddLogoImage(res);
                return false;
            }
        }
    });
}
function AddLogoImage(res) {
    $("#divLogoIcon").html('<img src="' + resolvedUrl + res.Message + '"/><i class="icon-close" />');
}