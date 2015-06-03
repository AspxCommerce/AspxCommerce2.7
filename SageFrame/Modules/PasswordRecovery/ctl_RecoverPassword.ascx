<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_RecoverPassword.ascx.cs"
    Inherits="SageFrame.Modules.PasswordRecovery.ctl_RecoverPassword" %>

<div class="sfRecoverPasswordPage" runat="server" id="divRecoverpwd">
  <div class="sfFormwrapper sfLogininside" >
    <asp:Wizard ID="wzdPasswordRecover" runat="server" DisplaySideBar="False" ActiveStepIndex="0"
        DisplayCancelButton="True" OnCancelButtonClick="CancelButton_Click" OnNextButtonClick="wzdPasswordRecover_NextButtonClick"
        OnFinishButtonClick="wzdPasswordRecover_FinishButtonClick" Width="100%" 
          meta:resourcekey="wzdPasswordRecoverResource1">
      <FinishNavigationTemplate>
        <div class="sfButtonwrapper">
          <asp:Button ID="FinishButton" runat="server" AlternateText="Finish"  
                CommandName="MoveComplete" CssClass="sfBtn"
                    Text="Finish" meta:resourcekey="FinishButtonResource1" />
        </div>
      </FinishNavigationTemplate>
      <StartNavigationTemplate>
        <div class="sfButtonwrapper">
          <asp:Button ID="StartNextButton" runat="server" AlternateText="Next"  
                CommandName="MoveNext" CssClass="sfBtn" ValidationGroup="vdgRecoveredPassword"
                    Text="Next" meta:resourcekey="StartNextButtonResource1" />
          <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel"  
                CommandName="Cancel" CssClass="sfBtn"
                    Text="Cancel" meta:resourcekey="CancelButtonResource1" />
        </div>
      </StartNavigationTemplate>
      <StepNavigationTemplate>
        <div class="sfButtonwrapper">
          <asp:Button ID="StepNextButton" runat="server" AlternateText="Next"  
                CommandName="MoveNext" CssClass="sfBtn" ValidationGroup="vdgRecoveredPassword"
                    Text="Next" meta:resourcekey="StepNextButtonResource1" />
        </div>
      </StepNavigationTemplate>
      <WizardSteps>
        <asp:WizardStep ID="WizardStep1" runat="server" Title="Setting New Password" 
              meta:resourcekey="WizardStep1Resource1"> <%= helpTemplate %>
          <div class="sfFormwrapper">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td width="35%"><asp:Label ID="lblPassword" runat="server" Text="Password" 
                        CssClass="sfFormlabel" meta:resourcekey="lblPasswordResource1"></asp:Label></td>
                <td width="30">:</td>
                <td><asp:HiddenField ID="hdnRecoveryCode" runat="server" />
                  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                        CssClass="sfInputbox" MaxLength="20" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvRecoveredPassword" runat="server" ControlToValidate="txtPassword"
                                    ValidationGroup="vdgRecoveredPassword" ErrorMessage="*" 
                        CssClass="sfError" meta:resourcekey="rfvRecoveredPasswordResource1"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td><asp:Label ID="lblRetypePassword" runat="server" Text="Retype Password" 
                        CssClass="sfFormlabel" MaxLength="20" 
                        meta:resourcekey="lblRetypePasswordResource1"></asp:Label></td>
                <td width="30">:</td>
                <td><asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" 
                        CssClass="sfInputbox" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                                    ValidationGroup="vdgRecoveredPassword" ErrorMessage="*" 
                        CssClass="sfError" meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="*" CssClass="sfError"
                                    ControlToCompare="txtPassword" 
                        ControlToValidate="txtRetypePassword"  ValidationGroup="vdgRecoveredPassword" 
                        meta:resourcekey="cvPasswordResource1" ></asp:CompareValidator></td>
              </tr>
              <tr>
                <td colspan="3"><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="vdgRecoveredPassword" 
                        meta:resourcekey="ValidationSummary1Resource1" /></td>
              </tr>
              <tr>
                <td colspan="3"></td>
              </tr>
            </table>
          </div>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStep2" runat="server" Title="Finished Template" 
              meta:resourcekey="WizardStep2Resource1">
          <asp:Literal ID="litPasswordChangedSuccessful" runat="server" 
                meta:resourcekey="litPasswordChangedSuccessfulResource1"></asp:Literal>
        </asp:WizardStep>
      </WizardSteps>
    </asp:Wizard>
  </div>
</div>
