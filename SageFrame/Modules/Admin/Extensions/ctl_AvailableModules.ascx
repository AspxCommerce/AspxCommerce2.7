<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_AvailableModules.ascx.cs"
    Inherits="SageFrame.DesktopModules.Admin.Extensions.Modules_Admin_Extensions_ctl_AvailableModules" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<div>
    <asp:Wizard ID="wizInstall" runat="server" Width="100%" DisplaySideBar="False" ActiveStepIndex="0"
        DisplayCancelButton="True" OnNextButtonClick="wizInstall_NextButtonClick" OnCancelButtonClick="wizInstall_CancelButtonClick"
        OnFinishButtonClick="wizInstall_FinishButtonClick" OnActiveStepChanged="wizInstall_ActiveStepChanged"
        meta:resourcekey="wizInstallResource1">
        <FinishNavigationTemplate>
            <div class="sfButtonwrapper">
                <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" CausesValidation="False"
                    CommandName="MoveComplete" CssClass="sfBtn" Text="Finish" meta:resourcekey="FinishButtonResource1" />
            </div>
        </FinishNavigationTemplate>
        <HeaderTemplate>
            <h3>
                <asp:Label ID="lblTitle" CssClass="cssClassNormalTitle" runat="server" Text="Available Modules"
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h3>
        </HeaderTemplate>
        <StartNavigationTemplate>
            <div class="sfButtonwrapper">
                <asp:Button ID="StartNextButton" runat="server" AlternateText="Next" CausesValidation="False"
                    CommandName="MoveNext" CssClass="sfBtn" Text="Next" meta:resourcekey="StartNextButtonResource1" />
                <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" CausesValidation="False"
                    CommandName="Cancel" CssClass="sfBtn" Text="Cancel" meta:resourcekey="CancelButtonResource1" />
            </div>
        </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="Step0" runat="Server" Title="Introduction" StepType="Start" AllowReturn="false"
                meta:resourcekey="Step0Resource1">
                <div class="sfFormwrapper sfPadding">
                    <asp:Label ID="lblWarningMessage" runat="server" CssClass="sfError" EnableViewState="False"
                        meta:resourcekey="lblWarningMessageResource1" />
                    <asp:Panel ID="pnlPackage" runat="server" meta:resourcekey="pnlPackageResource1"
                        CssClass="sfGridwrapper">
                        <asp:GridView ID="gdvModule" runat="server" AutoGenerateColumns="false" 
                            meta:resourcekey="gdvModuleResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>' 
                                            meta:resourcekey="lblnameResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Friendly Name" 
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFriendlyname" runat="server" 
                                            Text='<%# Eval("FriendlyName") %>' meta:resourcekey="lblFriendlynameResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" 
                                    meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' 
                                            meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Version" 
                                    meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>' 
                                            meta:resourcekey="lblVersionResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Install" 
                                    meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbInstall" runat="server" 
                                            meta:resourcekey="cbInstallResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step1" runat="Server" Title="ReleaseNotes" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step3Resource1">
                <div class="sfFormwrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlReleaseNotes" runat="server" CssClass="" Width="650px" meta:resourcekey="pnlReleaseNotesResource1">
                                    <asp:Label ID="lblReleaseNotes" runat="server" Text="ReleaseNotes:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblReleaseNotesResource1"></asp:Label>
                                    <asp:Label ID="lblReleaseNotesD" runat="server" Text="" meta:resourcekey="lblReleaseNotesDResource1"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step2" runat="server" Title="License" StepType="Step" AllowReturn="false"
                meta:resourcekey="Step4Resource1">
                <div class="sfFormwrapper">
                    <div class="sfLicense">
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
            <asp:WizardStep ID="Step3" runat="Server" Title="InstallResults" StepType="Finish"
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
                <asp:Button ID="StepNextButton" runat="server" AlternateText="Next" CausesValidation="False"
                    CommandName="MoveNext" CssClass="sfBtn" Text="Next" meta:resourcekey="StepNextButtonResource1" />
                <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" CausesValidation="False"
                    CommandName="Cancel" CssClass="sfBtn" Text="Cancel" meta:resourcekey="CancelButtonResource2" />
            </div>
        </StepNavigationTemplate>
    </asp:Wizard>
</div>
