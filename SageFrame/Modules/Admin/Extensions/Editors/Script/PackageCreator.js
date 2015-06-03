var upload;
var ModuleFilePath = "Modules/Admin/Extensions/Editors/";

$(document).ready(function () {
    var counter = 0;
    var divs = $('#div1, #div2, #div3, #div4, #div5'); //
    var next = '<%=btnNext.ClientID %>';
    var previous = '<%=btnPrevious.ClientID %>';
    var availblelistid = '<%=lbAvailableModules.ClientID %>';
    var newlistid = '<%=lbModulesList.ClientID %>';

    var uploadInstallScript = '<%=fuInstallScript.ClientID %>';
    var uploadUninstallScript = '<%=fuUnistallScript.ClientID %>';
    var fuIncludeSource = '<%=fuIncludeSource.ClientID %>';

    ImageUploader(uploadInstallScript, "lblInstallScriptFileName");
    ImageUploader(uploadUninstallScript, "lblUninstallScriptName");
    ImageUploader(fuIncludeSource, "spIncludeSourceInfo");


    if (counter == 0) {
        //alert(0);
        $('#div1').show();
        $('#' + previous).hide();
    }
    $('#' + next).click(function () {
        counter++;
        if (counter == 5) {
            return true;
        }
        else {
            $('#' + previous).show();
            if (counter == 4) {
                $('#' + next).val('Submit');
            }
            divs.hide() // hide all divs
          .filter(function (index) { return index == counter }) // figure out correct div to show
          .show('fast');
            return false;
        }
    });

    $('#' + previous).click(function () {
        counter--;

        if (counter == 0) {
            $('#' + previous).hide();
        }

        if (counter < 4) {
            $('#' + next).val('Next');
        }

        divs.hide() // hide all divs
          .filter(function (index) { return index == counter }) // figure out correct div to show
          .show('fast');
        return false;
    });

    $('#<%=chkIncludeSource.ClientID %>').click(function () {
        if ($("#<%=chkIncludeSource.ClientID %>").attr("checked")) {
            $("#divIncludeSource").show();
        } else {
            $("#divIncludeSource").hide();
        }
    });


    $("#add").click(function () {
        $('#' + availblelistid + '  option:selected').appendTo('#' + newlistid);
    });

    $("#remove").click(function () {
        $('#' + newlistid + ' option:selected').appendTo('#' + availblelistid);
    });

});
function ImageUploader(element, divID) {
    var uploadFlag = false;
    upload = new AjaxUpload($('#' + element), {
        action: sageRootPah + ModuleFilePath + 'UploadHandler.ashx?userModuleId=' + PackageCreatorUserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&sageFrameSecureToken=' + SageFrameSecureToken,
        name: 'myfile[]',
        multiple: true,
        data: { folderPath: '<%=tmpFoldName.Value%>' },
        autoSubmit: true,
        responseType: 'json',
        onChange: function (file, ext) {

        },
        onSubmit: function (file, ext) {

            if (ext != "SqlDataProvider") {
                alert('<h1>Alert Message</h1><p>Not a valid SqlDataProvider file!</p>');
                return false;
            }
        },
        onComplete: function (file, response) {
            var res = eval(response);
            if (res.Message == "LargeImagePixel") {
                return ConfirmDialog(this, 'message', "The image size is too large in pixel");
            }
            if (res && res.Status > 0) {
                alert("Error while uploading file");

            } else {
                $("#" + divID).html(file);
            }
        }
    });
}