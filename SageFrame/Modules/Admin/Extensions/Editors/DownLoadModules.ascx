<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownLoadModules.ascx.cs"
    Inherits="Modules_Admin_Extensions_Editors_DownLoadModules" %>
<script type="text/javascript">
    $(function () {
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $(".icon-download,.sfLoader").click(function () {
            ShowProgress();
        });
        var txtSearch = "<%=txtSearch.ClientID%>";
        var btnExtensionSearch = "<%=btnExtensionSearch.ClientID%>";
        $('input[id$=txtSearch]').keypress(function (e) {
            if (e.keyCode == 13) {
                $(this).next('input[id$=btnExtensionSearch]').click();
            }
        });

    });
</script>
<div class="loading" align="center">
    Please wait.<br />
    <br />
    <img src="~/Modules/LayoutManager/images/loading.gif" runat="server" alt="Loading" />
</div>
<asp:HiddenField ID="hdnFileName" runat="server" />
<div id="divModuleList" class="sfTemplate sfFormwrapper" runat="server">
    <%--<div runat="server" id="progressBar" visible="false">
        Please wait.
    </div>--%>
    <h3>
        Choose Module to install.
    </h3>
    <div class="cssClassFormWrapper cssClassControlDiv curve">
        <div class="sfSearchWrapper">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="sfInputbox sfLargeInputbox watermark"></asp:TextBox>
            <asp:Button ID="btnExtensionSearch" CssClass="sfSearchImg" runat="server" OnClick="btnExtensionSearch_Click" />
        </div>
    </div>
    <div class="onlineModuleList clearfix">
        <asp:Repeater ID="rptrModule" runat="server" OnItemCommand="rptrModule_ItemCommand"
            OnItemDataBound="rptrModule_ItemDataBound">
            <ItemTemplate>
                <div class="sfOnlineModule">
                    <div>
                        <div class="sfModuleDetails">
                            <div>
                                <asp:Label ID="lblTemplateName" runat="server" Text='<%#Eval("FileName") %>' />
                                 <%--<asp:Label ID="lblDescription" CssClass="sfDescription" runat="server" Text='<%#Eval("Description") %>' />--%>
                                <asp:LinkButton CssClass="icon-download sfBtn" runat="server" ID="lnkDownLoad" CommandName="DownLoad"
                                    CommandArgument='<%# Eval("URL") %>'></asp:LinkButton>
                            </div>
                        </div>
                        <div class="sfModuleThumbs">
                            <asp:Image ID="imgThubNail" runat="server" ImageUrl='<%#Eval("Thumb") %>' />
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnUrl" runat="server" Value='<%#Eval("URL") %>' />
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblEmptyData" CssClass="sfDefaultInfo" Text="No Modules To Display"
                    runat="server" Visible="false">
                </asp:Label>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="clear">
    </div>
    <div class="sfButtonwrapper">
        <label class="icon-close sfBtn">
            Cancel
            <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" OnClick="CancelButton_Click" />
        </label>
    </div>
</div>
<div class="sfFormwrapper" id="divInstallation" runat="server">
    <h2>
        <asp:Label ID="lblTitle" runat="server" Text="Install Module" meta:resourcekey="lblTitleResource1"></asp:Label>
    </h2>
    <asp:Wizard ID="wizInstall" Width="100%" runat="server" DisplaySideBar="False" ActiveStepIndex="0"
        DisplayCancelButton="True" OnNextButtonClick="wizInstall_NextButtonClick" OnCancelButtonClick="wizInstall_CancelButtonClick"
        OnFinishButtonClick="wizInstall_FinishButtonClick" OnActiveStepChanged="wizInstall_ActiveStepChanged">
        <FinishNavigationTemplate>
            <div class="sfButtonwrapper">
                <label class="icon-complete sfBtn">
                    Finish
                    <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" CausesValidation="False"
                        CommandName="MoveComplete" meta:resourcekey="FinishButtonResource1" />
                </label>
            </div>
        </FinishNavigationTemplate>
        <StartNavigationTemplate>
            <div class="sfButtonwrapper">
                <label class="sfBtn">
                    Next <i class="icon-arrow-slim-e sfTxtOrange"></i>
                    <asp:Button ID="StartNextButton" runat="server" AlternateText="Next" CausesValidation="False"
                        CssClass="sfLoader" CommandName="MoveNext" meta:resourcekey="StartNextButtonResource1" />
                </label>
                <label class="icon-close sfBtn">
                    Cancel
                    <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" CausesValidation="False"
                        CommandName="Cancel" meta:resourcekey="CancelButtonResource1" />
                </label>
            </div>
        </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="Step0" runat="Server" Title="Introduction" StepType="Start" AllowReturn="false"
                meta:resourcekey="Step0Resource1">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblBrowseFileHelp" runat="server" Text="Down load successfully,  click Next to continue."
                                meta:resourcekey="lblBrowseFileHelpResource1"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLoadMessage" runat="server" CssClass="sfError" Visible="true" meta:resourcekey="lblLoadMessageResource1" />
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep ID="Step1" runat="server" Title="Warnings" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step1Resource1">
                <asp:Label ID="lblWarningMessage" runat="server" CssClass="sfError" EnableViewState="False"
                    meta:resourcekey="lblWarningMessageResource1" />
                <asp:Panel ID="pnlRepair" runat="server" Visible="true" meta:resourcekey="pnlRepairResource1">
                    <asp:Label ID="lblRepairInstallHelp" runat="server" Text="Repair Install the previous installed Module overwrite all database and files contents."
                        meta:resourcekey="lblRepairInstallHelpResource1" />
                    <asp:CheckBox ID="chkRepairInstall" runat="server" CssClass="sfCheckbox" meta:resourcekey="chkRepairInstallResource1" />
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep ID="Step2" runat="Server" Title="PackageInfo" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step2Resource1">
                <asp:Panel ID="pnlPackage" runat="server" meta:resourcekey="pnlPackageResource1"
                    CssClass="sfGridwrapper">
                    <asp:GridView ID="gdvModule" runat="server" AutoGenerateColumns="false" Width="100%"
                        meta:resourcekey="gdvModuleResource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>' meta:resourcekey="lblnameResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Friendly Name" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblFriendlyname" runat="server" Text='<%# Eval("FriendlyName") %>'
                                        meta:resourcekey="lblFriendlynameResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Version" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>' meta:resourcekey="lblVersionResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Install" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbInstall" runat="server" meta:resourcekey="cbInstallResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep ID="Step3" runat="Server" Title="ReleaseNotes" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step3Resource1">
                <div class="sfFormwrapper">
                    <asp:Panel ID="pnlReleaseNotes" runat="server" meta:resourcekey="pnlReleaseNotesResource1">
                        <asp:Label CssClass="sfFormlabel" ID="lblReleaseNotes" runat="server" Text="Release Notes:"
                            meta:resourcekey="lblReleaseNotesResource1"></asp:Label>
                        <asp:Label ID="lblReleaseNotesD" CssClass="sfFormlabel" runat="server" Text="" meta:resourcekey="lblReleaseNotesDResource1"></asp:Label>
                    </asp:Panel>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step4" runat="server" Title="License" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step4Resource1">
                <div class="sfFormwrapper">
                    <div class="cssClassLicense">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="" meta:resourcekey="Panel1Resource1">
                                        <asp:Label ID="lblLicense" runat="server" Text="License:" meta:resourcekey="lblLicenseResource1"></asp:Label>
                                        <asp:Label ID="lblLicenseD" runat="server" Text="" meta:resourcekey="lblLicenseDResource1"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkAcceptLicense" runat="server" CssClass="sfCheckbox" TextAlign="Left"
                                        Text="Accept License?" meta:resourcekey="chkAcceptLicenseResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAcceptMessage" runat="server" EnableViewState="False" CssClass="sfError"
                                        meta:resourcekey="lblAcceptMessageResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step5" runat="Server" Title="InstallResults" StepType="Finish"
                meta:resourcekey="Step5Resource1">
                <div class="sfFormwrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblInstallMessage" runat="server" EnableViewState="False" CssClass="sfSuccess"
                                    meta:resourcekey="lblInstallMessageResource1" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <StepNavigationTemplate>
            <div class="sfButtonwrapper">
                <label class="sfBtn">
                    Next <i class="icon-arrow-slim-e sfTxtOrange"></i>
                    <asp:Button ID="StepNextButton" runat="server" AlternateText="Next" CausesValidation="False"
                        CommandName="MoveNext" CssClass="sfBtn sfLoader" meta:resourcekey="StepNextButtonResource1" />
                </label>
                <label class="icon-close sfBtn">
                    Cancel
                    <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" CausesValidation="False"
                        CommandName="Cancel" CssClass="sfBtn" />
                </label>
            </div>
        </StepNavigationTemplate>
    </asp:Wizard>
</div>
