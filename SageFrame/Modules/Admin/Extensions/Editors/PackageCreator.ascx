<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackageCreator.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.PackageCreator" %>
<%@ Register Src="PackageDetails.ascx" TagName="PackageDetails" TagPrefix="uc1" %>
<script type="text/javascript">

    var upload;
    var counter = 0;
    var packageCreatorusermoduleID = '<%=SageUserModuleID %>';
    $(document).ready(function () {
        NewPackage.Init();
    });
    var NewPackage = {
        Settings: {
            next: '<%=btnNext.ClientID %>',
            back: '<%=btnBack.ClientID %>',
            hdnPrev: '#<%=hdnPrev.ClientID %>',
            previous: '<%=btnPrevious.ClientID %>',
            ModuleFilePath: 'Modules/Admin/Extensions/Editors/',
            validationObj: ''
        },
        Init: function () {
            var uploadInstallScript = '<%=fuInstallScript.ClientID %>';
            var uploadUninstallScript = '<%=fuUnistallScript.ClientID %>';
            var fuIncludeSource = '<%=fuIncludeSource.ClientID %>';
            var hdnInstallScriptFileName = '<%=hdnInstallScriptFileName.ClientID %>';
            var hdnSrcZipFile = '<%=hdnSrcZipFile.ClientID %>';
            var lboxcontrolList = '<%=lstFolderFiles.ClientID %>';
            var hdnUnInstallSQLFileName = '<%=hdnUnInstallSQLFileName.ClientID %>';
            var availblelistid = '<%=lbAvailableModules.ClientID %>';
            this.FileUploader(uploadInstallScript, "lblInstallScriptFileName", "sql", hdnInstallScriptFileName);
            this.FileUploader(uploadUninstallScript, "lblUninstallScriptName", "sql", hdnUnInstallSQLFileName);
            this.FileUploader(fuIncludeSource, "spIncludeSourceInfo", 'zip,rar', hdnSrcZipFile);
            this.RegisterValidationRules();
            this.AddNavigationRules();
            $('#' + NewPackage.Settings.back).hide();
            $('#<%=chkIncludeSource.ClientID %>').click(function () {
                if ($("#<%=chkIncludeSource.ClientID %>").attr("checked")) {
                    $("#divIncludeSource").show();
                }
                else {
                    $("#divIncludeSource").hide();
                    $("#" + hdnSrcZipFile).val("");
                    $("#spIncludeSourceInfo").html("");
                }
            });
            $("#chkControls").click(function () {
                if ($("#chkControls").is(":checked")) {
                    $("#" + lboxcontrolList + " option").attr("selected", "selected");
                }
                else {
                    $("#" + lboxcontrolList + " option").removeAttr("selected");
                }
            });
        },

        AddNavigationRules: function () {
            var divs = $('#div1, #div2, #div3, #div4, #div5');
            if (counter == 0) {
                $('#div1').show();
                $('#' + NewPackage.Settings.previous).hide();
            }

            var btnEvent = function () {
                $('#' + NewPackage.Settings.next).click(function () {
                    $('#<%=hdnPrev.ClientID %>').val(counter);
                    if (counter == 0) {
                        return true;
                    }
                    if (NewPackage.Settings.validationObj.form()) {
                        counter++;
                        if (counter == 5) {
                            $('#' + NewPackage.Settings.next).unbind();
                            return true;
                        }
                        else {
                            $('#' + NewPackage.Settings.previous).show();
                            if (counter == 4) {
                                $('#' + NewPackage.Settings.next).val('Submit').unbind().bind('click', function () {
                                    SageFrame.messaging.show("Package created", "Success");
                                    $('#' + NewPackage.Settings.back).show();
                                    divs.hide();
                                    $("div.sfButtonwrapper").hide();
                                });
                            }
                            divs.hide()
                             .filter(function (index) { return index == counter }) // figure out correct div to show
                             .show('fast');
                            return false;
                        }
                    }
                });

            };
            btnEvent();

            $('#' + this.Settings.previous).click(function () {
                counter--;
                $('#<%=hdnPrev.ClientID %>').val(counter);
                if (counter == 0) {
                    $('#' + NewPackage.Settings.previous).hide();
                }
                if (counter < 4) {
                    $('#' + NewPackage.Settings.next).val('Next');
                    $('#' + NewPackage.Settings.next).unbind();
                    btnEvent();
                }
                divs.hide() // hide all divs
                .filter(function (index) { return index == counter }) // figure out correct div to show
                .show('fast');
                return false;
            });
        },

        RegisterValidationRules: function () {
            $.validator.addMethod("CheckView", function (value, element) {
                if (counter >= 3) {
                    return $.trim(value).length > 0;
                } else return true;
            }, "Please select View");
            $.validator.addMethod("CheckControls", function (value, element) {
                if (counter >= 2) {
                    return $.trim(value).length > 0;
                } else return true;
            }, "Please select Controls");
            $.validator.addMethod("CheckDLL", function (value, element) {
                if (counter == 4) {
                    return $.trim(value).length > 0;
                } return true;
            }, "Please select DLL files");
            this.Settings.validationObj = $("#form1").validate();
        },

        FileUploader: function (element, divID, validExtension, hdnSQLScriptFileName) {
            var uploadFlag = false;
            upload = new AjaxUpload($('#' + element), {
                action: sageRootPah + this.Settings.ModuleFilePath + 'UploadHandler.ashx?userModuleId=' + packageCreatorusermoduleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&sageFrameSecureToken=' + SageFrameSecureToken,
                name: 'myfile[]',
                multiple: true,
                data: { folderPath: '<%=tmpFoldName.Value%>' },
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    if (validExtension.toLowerCase().indexOf(ext.toLowerCase()) < 0) {
                        alert("Not a valid " + validExtension + " file!");
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message == "LargeImagePixel") {
                        return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                    }
                    if (res && res.Status > 0) {
                        alert("Error while uploading file.");
                    }
                    else {
                        $("#" + hdnSQLScriptFileName).val(file);
                        $("#" + divID).html(file);
                    }
                }
            });
        }
    }
</script>
<asp:HiddenField runat="server" ID="hdnPrev" />
<div id="SubmitConfirmation" style="display: none;">
    Your Package has been created successfully.
</div>
<div id="div1" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <uc1:PackageDetails ID="PackageDetails1" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="20%">
                    <asp:Label ID="lblfriendlyname" runat="server" Text="Friendly name" CssClass="sfFormlabel"
                        meta:resourcekey="lblfriendlynameResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtfriendlyname" runat="server" CssClass="sfInputbox" meta:resourcekey="txtfriendlynameResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmodulename" runat="server" Text="Module name" CssClass="sfFormlabel required"
                        meta:resourcekey="lblmodulenameResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtmodulename" runat="server" CssClass="sfInputbox required" meta:resourcekey="txtmodulenameResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblbusinesscontrollerclass" runat="server" Text="Business controller class"
                        CssClass="sfFormlabel" meta:resourcekey="lblbusinesscontrollerclassResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtbusinesscontrollerclass" runat="server" CssClass="sfInputbox"
                        meta:resourcekey="txtbusinesscontrollerclassResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcompatibleversions" runat="server" Text="Compatible versions" CssClass="sfFormlabel"
                        meta:resourcekey="lblcompatibleversionsResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtcompatibleversions" runat="server" CssClass="sfInputbox" meta:resourcekey="txtcompatibleversionsResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Cache Time" CssClass="sfFormlabel" meta:resourcekey="lblcompatibleversionsResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtCacheTime" runat="server" CssClass="sfInputbox" meta:resourcekey="txtCacheTime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModuleSelect" runat="server" Text="Select a Folder" CssClass="sfFormlabel  required"
                        meta:resourcekey="lblModuleSelectResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <div class="sfAvailableModules">
                        <asp:ListBox ID="lbAvailableModules" runat="server" CssClass="sfListmenubig required"
                            SelectionMode="Single" Height="200"></asp:ListBox>
                    </div>
                    <div class='sfSelectedModules' style="display: none">
                        <asp:ListBox ID="lbModulesList" CssClass="sfListmenubig" runat="server" SelectionMode="Multiple"
                            Height="200"></asp:ListBox>
                        <asp:RequiredFieldValidator ID="rfvModulesList" runat="server" ControlToValidate="lbModulesList"
                            ValidationGroup="vdgExtension" ErrorMessage="* Please choose items" SetFocusOnError="True"
                            CssClass="sfError" meta:resourcekey="rfvModulesListResource1"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<div id="div2" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <div>
            <h2>
                <asp:Label ID="lblInstallScript" runat="server" Text="Sql Script for Install:" meta:resourcekey="lblInstallScriptResource1"></asp:Label>
            </h2>
            <asp:FileUpload ID="fuInstallScript" runat="server" />
            <asp:HiddenField ID="hdnInstallScriptFileName" runat="server" />
            <span id="lblInstallScriptFileName"></span>
            <h3>OR Paste SQL Script below:</h3>
            <asp:TextBox Rows="18" Columns="280" runat="server" ID="InstallScriptTxt" TextMode="MultiLine"
                CssClass="sfTextarea sfFullwidth CheckSqlInstallContent" />
        </div>
        <div>
            <h3>
                <asp:Label ID="lblUnistallScript" runat="server" Text="Sql Script for Uninstall:"
                    meta:resourcekey="lblUnistallScriptResource1"></asp:Label>
            </h3>
            <asp:FileUpload ID="fuUnistallScript" runat="server" />
            <asp:HiddenField ID="hdnUnInstallSQLFileName" runat="server" />
            <span id="lblUninstallScriptName"></span>
            <h3>OR Paste SQL Script below:</h3>
            <asp:TextBox Rows="18" Columns="280" runat="server" ID="UnistallScriptTxt" TextMode="MultiLine"
                CssClass="sfTextarea sfFullwidth" />
        </div>
        <br />
        <div class="sfCheckbox">
            <asp:CheckBox ID="chkIncludeSource" runat="server" Text="Include Source File?" />
        </div>
        <div id="divIncludeSource" style="display: none" class="sfUploadfile clearfix">
            <p>
                <asp:Label ID="lblIncludeSource" runat="server" Text="Upload Source Files Zip:" CssClass="sfFormlabel"
                    meta:resourcekey="lblIncludeSourceResource1"></asp:Label>
            </p>
            <asp:FileUpload ID="fuIncludeSource" runat="server" />
            <asp:HiddenField ID="hdnSrcZipFile" runat="server" />
            <span id="spIncludeSourceInfo" />
        </div>
    </div>
</div>
<div id="div3" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <p class="sfNote">
            <asp:Label ID="lblFilesList" runat="server" Text="The List of files for the package is shown here.In this section you can add,edit or delete the files for this package."
                meta:resourcekey="lblFilesListResource1"></asp:Label>
        </p>
        <asp:ListBox runat="server" ID="lstFolderFiles" SelectionMode="Multiple" CssClass="sfListmenubig sfFullwidth" />
        <br />
        <br />
        <div class="sfCheckbox">
            <input type="checkbox" id="chkControls" />
            <label>
                Select All Files.</label>
        </div>
    </div>
</div>
<div id="div4" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <h3>Select View</h3>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr id="rowSource" runat="server">
                <td width="20%" id="Td5" runat="server">
                    <asp:Label ID="lblSource" runat="server" Text="Source" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td id="Td6" runat="server">
                    <asp:DropDownList runat="server" ID="ddlViewControlSrc" CssClass="sfListmenu" AutoPostBack="false" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblKey" runat="server" Text="Key" CssClass="sfFormlabel " meta:resourcekey="lblKeyResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtViewKey" runat="server" CssClass="sfInputbox CheckView" meta:resourcekey="txtKeyResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvModulekey" runat="server" ControlToValidate="txtViewKey"
                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                        meta:resourcekey="rfvModulekeyResource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblViewTitle" runat="server" Text="Title" CssClass="sfFormlabel" meta:resourcekey="lblTitleResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtViewTitle" runat="server" CssClass="sfInputbox" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvModuleTitle" runat="server" ControlToValidate="txtViewTitle"
                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                        meta:resourcekey="rfvModuleTitleResource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr visible="false" runat="server">
                <td>
                    <asp:Label ID="lblType" runat="server" Text="Type" CssClass="sfFormlabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlTypeResource1" />
                    <asp:Label ID="lblErrorControlType" runat="server" CssClass="sfError" Text="*" Visible="False"
                        meta:resourcekey="lblErrorControlTypeResource1"></asp:Label>
                </td>
            </tr>
            <tr id="rowDisplayOrder" runat="server" visible="False">
                <td id="Td7" runat="server">
                    <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td id="Td8" runat="server">
                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="sfInputbox" MaxLength="2"
                        Text="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIcon" runat="server" Text="Icon" CssClass="sfFormlabel" meta:resourcekey="lblIconResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlViewIcon" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlIconResource1" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHelpURL" runat="server" Text="Help URL" CssClass="sfFormlabel"
                        meta:resourcekey="lblHelpURLResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtViewHelpURL" runat="server" CssClass="sfInputbox" meta:resourcekey="txtHelpURLResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revHelpUrl" runat="server" ControlToValidate="txtViewHelpURL"
                        CssClass="sfError" ErrorMessage="The Help Url is not valid." SetFocusOnError="True"
                        ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$"
                        meta:resourcekey="revHelpUrlResource1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblSupportsPartialRendering" runat="server" Text="Supports Partial Rendering?"
                        CssClass="sfFormlabel" meta:resourcekey="lblSupportsPartialRenderingResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:CheckBox ID="chkViewSupportsPartialRendering" runat="server" CssClass="sfCheckbox"
                        meta:resourcekey="chkSupportsPartialRenderingResource1" />
                </td>
            </tr>--%>
        </table>
        <h3>Select Edit</h3>
        <div class="sfFormwrapper">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr id="Tr3" runat="server">
                    <td id="Td13" runat="server">
                        <asp:Label ID="Label6" runat="server" Text="Source" CssClass="sfFormlabel"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td id="Td14" runat="server">
                        <asp:DropDownList ID="ddlEditControlSrc" runat="server" CssClass="sfListmenu" AutoPostBack="false" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <asp:Label ID="Label7" runat="server" Text="Key:" CssClass="sfFormlabel" meta:resourcekey="lblKeyResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditKey" runat="server" CssClass="sfInputbox" meta:resourcekey="txtKeyResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEditKey"
                            ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                            meta:resourcekey="rfvModulekeyResource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Title" CssClass="sfFormlabel" meta:resourcekey="lblTitleResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditTitle" runat="server" CssClass="sfInputbox" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEditTitle"
                            ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                            meta:resourcekey="rfvModuleTitleResource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr visible="false" runat="server">
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Type" CssClass="sfFormlabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTypeIcon" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlTypeResource1" />
                        <asp:Label ID="Label10" runat="server" CssClass="sfError" Text="*" Visible="False"
                            meta:resourcekey="lblErrorControlTypeResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="Tr4" runat="server" visible="False">
                    <td id="Td15" runat="server">
                        <asp:Label ID="Label11" runat="server" Text="Display Order" CssClass="sfFormlabel"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td id="Td16" runat="server">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="sfInputbox" MaxLength="2" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Icon" CssClass="sfFormlabel" meta:resourcekey="lblIconResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEditIcon" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlIconResource1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Help URL" CssClass="sfFormlabel" meta:resourcekey="lblHelpURLResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditHelpURL" runat="server" CssClass="sfInputbox" meta:resourcekey="txtHelpURLResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEditHelpURL"
                            CssClass="sfError" ErrorMessage="The Help Url is not valid." SetFocusOnError="True"
                            ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$"
                            meta:resourcekey="revHelpUrlResource1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Supports Partial Rendering?" CssClass="sfFormlabel"
                            meta:resourcekey="lblSupportsPartialRenderingResource1"></asp:Label>
                    </td>
                    <td width="30">:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEditSupportsPartialRendering" runat="server" CssClass="sfCheckbox"
                            meta:resourcekey="chkSupportsPartialRenderingResource1" />
                    </td>
                </tr>--%>
            </table>
        </div>
        <h3>Select Setting</h3>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr id="Tr7" runat="server">
                <td id="Td21" runat="server">
                    <asp:Label ID="lblSettingControlSearc" runat="server" Text="Source" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td id="Td22" runat="server">
                    <asp:DropDownList ID="ddlSettingControlSrc" runat="server" AutoPostBack="false" CssClass="sfListmenu" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label20" runat="server" Text="Key" CssClass="sfFormlabel" meta:resourcekey="lblKeyResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtSettingKey" runat="server" CssClass="sfInputbox" meta:resourcekey="txtKeyResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSettingKey"
                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                        meta:resourcekey="rfvModulekeyResource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="Title" CssClass="sfFormlabel" meta:resourcekey="lblTitleResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtSettingTitle" runat="server" CssClass="sfInputbox" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSettingTitle"
                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" CssClass="sfError"
                        meta:resourcekey="rfvModuleTitleResource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr visible="false" runat="server">
                <td>
                    <asp:Label ID="Label22" runat="server" Text="Type" CssClass="sfFormlabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList5" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlTypeResource1" />
                    <asp:Label ID="Label23" runat="server" CssClass="sfError" Text="*" Visible="False"
                        meta:resourcekey="lblErrorControlTypeResource1"></asp:Label>
                </td>
            </tr>
            <tr id="Tr8" runat="server" visible="False">
                <td id="Td23" runat="server">
                    <asp:Label ID="Label24" runat="server" Text="Display Order" CssClass="sfFormlabel"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td id="Td24" runat="server">
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="sfInputbox" MaxLength="2" Text="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="Icon" CssClass="sfFormlabel" meta:resourcekey="lblIconResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSettingIcon" runat="server" CssClass="sfListmenu" meta:resourcekey="ddlIconResource1" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="Help URL" CssClass="sfFormlabel" meta:resourcekey="lblHelpURLResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:TextBox ID="txtSettingHelpURL" runat="server" CssClass="sfInputbox" meta:resourcekey="txtHelpURLResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSettingHelpURL"
                        CssClass="sfError" ErrorMessage="The Help Url is not valid." SetFocusOnError="True"
                        ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$"
                        meta:resourcekey="revHelpUrlResource1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="Supports Partial Rendering?" CssClass="sfFormlabel"
                        meta:resourcekey="lblSupportsPartialRenderingResource1"></asp:Label>
                </td>
                <td width="30">:
                </td>
                <td>
                    <asp:CheckBox ID="chkSettingSupportsPartialRendering" runat="server" CssClass="sfCheckbox"
                        meta:resourcekey="chkSupportsPartialRenderingResource1" />
                </td>
            </tr>--%>
        </table>
    </div>
</div>
<div id="div5" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <p class="sfNote">
            <asp:Label ID="Label1" runat="server" Text="In this section you can select Assemblies to include in this package."
                meta:resourcekey="lblManifestPreviewResource1"></asp:Label>
        </p>
        <asp:ListBox runat="server" ID="lstAssembly" SelectionMode="Multiple" CssClass="sfListmenubig CheckDLL"
            Height="300" Width="500"></asp:ListBox>
    </div>
</div>
<div id="div6" style="display: none">
    <div class="sfFormwrapper sfPadding">
        <div>
            <asp:Label ID="lblCreateManifest" runat="server" Text="Create Manifest:" CssClass="sfFormlabel"
                meta:resourcekey="lblCreateManifestResource1"></asp:Label>
            <asp:CheckBox ID="chkManifest" runat="server" Checked="true" />
        </div>
        <div class="dnnFormItem" id="trManifest2" runat="server">
            <asp:Label ID="lblManifestName" runat="server" Text="Manifest File Name:" CssClass="sfFormlabel"
                meta:resourcekey="lblManifestNameResource1"></asp:Label>
            <asp:TextBox ID="txtManifestName" runat="server" Style="width: 250px" />
        </div>
        <div class="dnnFormItem">
            <asp:Label ID="lblCreatePackage" runat="server" Text="Create Package:" CssClass="sfFormlabel"
                meta:resourcekey="lblCreatePackageResource1"></asp:Label>
            <asp:CheckBox ID="chkPackage" runat="server" Checked="true" />
        </div>
        <div class="dnnFormItem">
            <asp:Label ID="lblPackageName" runat="server" Text="Package Zip Name:" CssClass="sfFormlabel"
                meta:resourcekey="lblPackageNameResource1"></asp:Label>
            <asp:TextBox ID="txtPackageName" runat="server" />
            <asp:HiddenField ID="tmpFoldName" runat="server" />
        </div>
    </div>
</div>
<div class="sfButtonwrapper">
    <asp:Button ID="btnPrevious" runat="server" AlternateText="Previous" CausesValidation="False"
        CommandName="Previous" CssClass="sfBtn" Text="Previous" meta:resourcekey="btnPreviousResource2" />
    <asp:Button ID="btnNext" runat="server" AlternateText="Next" CausesValidation="False"
        CommandName="Next" CssClass="sfBtn" Text="Next" meta:resourcekey="btnNextResource1"
        OnClick="btnSubmit_Click" />
    <asp:Button ID="btnCancelled" runat="server" AlternateText="Cancel" CssClass="sfBtn"
        Text="Cancel" meta:resourcekey="btnCancelResource2" UseSubmitBehavior="false"
        OnClientClick="javascript:return window.location=sageRootPah+'Admin/Modules.aspx';" />
</div>
<asp:Button ID="btnBack" runat="server" AlternateText="Back" CssClass="sfBtn" Text="Back"
    CausesValidation="False" UseSubmitBehavior="false" OnClick="btnBack_Click" />