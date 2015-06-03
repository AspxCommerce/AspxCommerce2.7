<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguagePackInstaller.ascx.cs"
    Inherits="Localization_LanguagePackInstaller" %>
<h1>
    <asp:Label ID="lblLanguagePackInstaller" runat="server" Text="Language Pack Installer"
        meta:resourcekey="lblLanguagePackInstallerResource1"></asp:Label>
</h1>
<div class="sfFormwrapper">
    <asp:Wizard ID="wzrdInstallLanguagePack" runat="server" class="hideTD" Style="width: 100%"
        ActiveStepIndex="0" OnNextButtonClick="wzrdInstallLanguagePack_NextButtonClick"
        meta:resourcekey="wzrdInstallLanguagePackResource1" OnFinishButtonClick="wzrdInstallLanguagePack_FinishButtonClick">
        <StartNavigationTemplate>
            <div class="sfButtonwrapper">
                <label class="sfLocale sfBtn">
                    Next <i class="icon-arrow-slimdouble-e"></i>
                    <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext"
                        Text="" CausesValidation="False" meta:resourcekey="StartNextButtonResource1" />
                </label>
            </div>
        </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource1">
                <div class="divWizard">
                    <asp:Label ID="lblLanguagePack" runat="server" Text="Browse Language Pack ZIP:" CssClass="sfFormlabel"
                        meta:resourcekey="lblLanguagePackResource1"></asp:Label>
                    <asp:FileUpload ID="fluLanguagePack" CssClass="cssClassNormalFileUpload" runat="server"
                        meta:resourcekey="fluLanguagePackResource1" />
                </div>
            </asp:WizardStep>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource2">
                <div class="cssClassLanuageInstallerInfo">
                    <h1>
                        Package Information
                    </h1>
                    <ul>
                        <li>
                            <asp:Label ID="lblPackageName" runat="server" CssClass="sfFormlabel cssClassLeftInfo"
                                meta:resourcekey="lblPackageNameResource1">Name:</asp:Label>
                            <asp:Label ID="lblPackageNameD" runat="server" meta:resourcekey="lblPackageNameDResource1"
                                CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblType" runat="server" CssClass="sfFormlabel cssClassLeftInfo" meta:resourcekey="lblTypeResource1">Type:</asp:Label>
                            <asp:Label ID="lblTypeD" runat="server" meta:resourcekey="lblTypeDResource1" CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblFriendlyName" runat="server" CssClass="sfFormlabel cssClassLeftInfo"
                                meta:resourcekey="lblFriendlyNameResource1">Friendly Name:</asp:Label>
                            <asp:Label ID="lblFriendlyNameD" runat="server" meta:resourcekey="lblFriendlyNameDResource1"
                                CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblDescription" runat="server" CssClass="sfFormlabel cssClassLeftInfo"
                                meta:resourcekey="lblDescriptionResource1">Description:</asp:Label>
                            <asp:Label ID="lblDescriptionD" runat="server" meta:resourcekey="lblDescriptionDResource1"
                                CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblVersion" runat="server" CssClass="sfFormlabel cssClassLeftInfo"
                                meta:resourcekey="lblVersionResource1">Version:</asp:Label>
                            <asp:Label ID="lblVersionD" runat="server" meta:resourcekey="lblVersionDResource1"
                                CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblOwner" runat="server" CssClass="sfFormlabel cssClassLeftInfo" meta:resourcekey="lblOwnerResource1">Owner:</asp:Label>
                            <asp:Label ID="lblOwnerD" runat="server" meta:resourcekey="lblOwnerDResource1" CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblOrganisation" runat="server" CssClass="sfFormlabel cssClassLeftInfo"
                                meta:resourcekey="lblOrganisationResource1">Organisation:</asp:Label>
                            <asp:Label ID="lblOrganisationD" runat="server" meta:resourcekey="lblOrganisationDResource1"
                                CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblURL" runat="server" CssClass="sfFormlabel cssClassLeftInfo" meta:resourcekey="lblURLResource1">URL:</asp:Label>
                            <asp:Label ID="lblURLD" runat="server" meta:resourcekey="lblURLDResource1" CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="lblEmail" runat="server" CssClass="sfFormlabel cssClassLeftInfo" meta:resourcekey="lblEmailResource1">Email Address:</asp:Label>
                            <asp:Label ID="lblEmailD" runat="server" meta:resourcekey="lblEmailDResource1" CssClass="cssClassRightInfo"></asp:Label>
                        </li>
                    </ul>
                </div>
            </asp:WizardStep>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource3">
                <div class="cssClassLanuageInstallerInfo">
                    <h2>
                        Release Notes
                    </h2>
                    <asp:Label ID="lblReleaseNotes" runat="server" meta:resourcekey="lblReleaseNotesResource1"></asp:Label>
                </div>
            </asp:WizardStep>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource4">
                <div class="cssClassLanuageInstallerInfo">
                    <h1>
                        License
                    </h1>
                    <p>
                        <strong>
                            <asp:Label ID="lblLicense" runat="server" meta:resourcekey="lblLicenseResource1"></asp:Label></strong></p>
                    <p class="cssClassInputCheckBox">
                        <asp:CheckBox ID="chkAcceptLicense" runat="server" Text="Accept License" meta:resourcekey="chkAcceptLicenseResource1" /></p>
                    <p>
                        <asp:Label ID="lblAcceptMessage" runat="server" EnableViewState="False" CssClass="sfError"
                            meta:resourcekey="lblAcceptMessageResource1" /></p>
                </div>
            </asp:WizardStep>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource5">
                <div class="cssClassLanuageInstallerInfo">
                    <h2>
                        Package Installation Stat
                    </h2>
                    <div class="cssClassLanguagePackInstallerTable">
                        <asp:GridView ID="gvLangFiles" runat="server" AutoGenerateColumns="false" GridLines="None"
                            OnRowDataBound="gvLangFiles_RowDataBound" meta:resourcekey="gvLangFilesResource1">
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked="True" meta:resourcekey="chkSelectResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ModuleName" meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="IsExist" meta:resourcekey="BoundFieldResource2" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <asp:CheckBox ID="chkOverWrite" runat="server" Text=" Overwrite Existing Files" meta:resourcekey="chkOverWriteResource1" />
                </div>
            </asp:WizardStep>
            <asp:WizardStep runat="server" meta:resourcekey="WizardStepResource6">
                <div class="cssClassLanuageInstallerInfo">
                    <h1>
                        Package Installation Report
                    </h1>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="sfButtonwrapper">
                <label class="sfLocale icon-complete sfBtn">
                    Finish
                    <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="" CssClass="sfBtn"
                        meta:resourcekey="FinishButtonResource1" />
                </label>
            </div>
        </FinishNavigationTemplate>
        <StepNavigationTemplate>
            <div class="sfButtonwrapper">
             <label class="sfLocale sfBtn">Previous <i class="icon-arrow-slimdouble-w"></i>
                <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" Text=""
                    CssClass="sfBtn" CommandName="MovePrevious" meta:resourcekey="StepPreviousButtonResource1" />
                   </label>
                    <label class="sfLocale sfBtn">Next <i class="icon-arrow-slimdouble-e"></i>
                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text=""
                    CssClass="sfBtn" meta:resourcekey="StepNextButtonResource1" />
            </div>
        </StepNavigationTemplate>
    </asp:Wizard>
</div>
<div class="sfButtonwrapper">
    <label class="sfLocale icon-close sfBtn">
        Cancel
        <asp:Button ID="imbCancel" runat="server" OnClick="imbCancel_Click" meta:resourcekey="imbCancelResource1" />
    </label>
    
</div>
