<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_UserRegistration.ascx.cs"
    Inherits="SageFrame.Modules.UserRegistration.ctl_UserRegistration" %>

<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function () {
        $("#form1").validate({
            ignore: ':hidden',
            rules: {
                '<%=Email.UniqueID %>': { email: true }
                },
                messages: {
                    '<%=Email.UniqueID %>': "<br/>Email must be in a correct format."
                }
            });


        $(".sfLocalee").SystemLocalize();
        var FinishButton = '#' + '<%=FinishButton.ClientID %>';
        var pwdID = '#' + '<%=Password.ClientID%>';
        $('#minchar').remove();
        $(pwdID).val('');
        $(pwdID).on("change", function () {
            var len = $(this).val().length;
            if (len < 4 && len != 0) {
                $(this).after('<label class="sfError" id="lblPassswordLength"><br/>Password must be at least 4 chars long</label>');
                return false;
            }
            else {
                $('#lblPassswordLength').remove();
            }
        });
        $(pwdID).click(function () {
            $('#lblPassswordLength').remove();
        });
        $(FinishButton).click(function () {
            var len = $(pwdID).val().length;
            if (len < 4) {
                return false;
            }
        });

        $('.password').pstrength({ minchar: 4 });

    });
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            $('.password').pstrength({ minchar: 4 });
        }



    }
    //]]>	
</script>

<div class="sfUserRegistrationPage">
    <div class="sfUserRegistration">
        <h2>Registration</h2>
        <div class="sfFormwrapper">
            <div class="sfUserRegistrationInfoLeft" id="divRegister" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <span class="sfAllrequired sfLocalee">* All Fields are compulsory. </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" CssClass="sfFormlabel"
                                meta:resourcekey="FirstNameLabelResource1">First Name: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="FirstName" CssClass="sfInputbox" autofocus="autofocus" runat="server"
                                meta:resourcekey="FirstNameResource1" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="FirstName"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName" CssClass="sfFormlabel"
                                meta:resourcekey="LastNameLabelResource1">Last Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" CssClass="sfInputbox" runat="server" meta:resourcekey="LastNameResource1" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="LastName"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvLastNameResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="sfFormlabel"
                                meta:resourcekey="UserNameLabelResource1">User Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserName" MaxLength="50" runat="server" CssClass="sfInputbox" meta:resourcekey="UserNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvUserNameRequiredResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="sfFormlabel"
                                meta:resourcekey="EmailLabelResource1">E-mail:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Email" MaxLength="50" runat="server" CssClass="sfInputbox" meta:resourcekey="EmailResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="Email"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvEmailRequiredResource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email"
                                Display="Dynamic" SetFocusOnError="True" ErrorMessage="*" ValidationGroup="CreateUserWizard1"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="sfError"
                                meta:resourcekey="revEmailResource1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="sfFormlabel"
                                meta:resourcekey="PasswordLabelResource1">Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Password" MaxLength="20" runat="server" TextMode="Password" CssClass="sfInputbox password"
                                meta:resourcekey="PasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPasswordRequired" runat="server" ControlToValidate="Password"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvPasswordRequiredResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                                CssClass="sfFormlabel" meta:resourcekey="ConfirmPasswordLabelResource1">Confirm Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" MaxLength="20"
                                CssClass="sfInputbox" meta:resourcekey="ConfirmPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                meta:resourcekey="rfvConfirmPasswordRequiredResource1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPasswordCompare" runat="server" ControlToCompare="Password"
                                ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1"
                                CssClass="sfError" meta:resourcekey="cvPasswordCompareResource1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="captchaTR">
                        <td>
                            <asp:Label ID="CaptchaLabel" runat="server" Text="Captcha:" AssociatedControlID="CaptchaImage"
                                CssClass="sfFormlabel" meta:resourcekey="CaptchaLabelResource1"></asp:Label>
                        </td>
                        <td class="sfCatpchatd">
                            <asp:Image ID="CaptchaImage" runat="server" CssClass="sfCaptcha" meta:resourcekey="CaptchaImageResource1" />
                            <span id="captchaValidator" runat="server" class="sfrequired">*</span>
                            <asp:ImageButton ID="Refresh" CssClass="sfCaptchadata" runat="server" OnClick="Refresh_Click"
                                ValidationGroup="Sep" meta:resourcekey="RefreshResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="DataLabel" runat="server" Text="Enter Captcha Text" AssociatedControlID="CaptchaValue"
                                CssClass="sfFormlabel" meta:resourcekey="DataLabelResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="CaptchaValue" runat="server" CssClass="sfInputbox" meta:resourcekey="CaptchaValueResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCaptchaValueValidator" runat="server" ControlToValidate="CaptchaValue"
                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="sfError"
                                        meta:resourcekey="rfvCaptchaValueValidatorResource1"></asp:RequiredFieldValidator>
                                    <%-- <asp:CompareValidator ID="cvCaptchaValue" runat="server" Display="Dynamic" ErrorMessage="*"
                                                ValidationGroup="CreateUserWizard1" ControlToValidate="CaptchaValue"
                                                CssClass="sfError" meta:resourcekey="cvCaptchaValueResource1"></asp:CompareValidator>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <div class="sfButtonwrapper">
                                <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" ValidationGroup="CreateUserWizard1"
                                    CommandName="MoveComplete" CssClass="sfBtn" Text="Register" OnClick="FinishButton_Click"
                                    meta:resourcekey="FinishButtonResource1" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divRegistration" runat="server">
                <div class="sfRegistrationInformation">
                    <%= headerTemplate %>
                </div>
                <div id="divRegConfirm" class="sfRegConfirm" runat="server">
                    <h3>Registration Successful</h3>
                    <asp:Label ID="lblRegSuccess" runat="server" CssClass="sfFormlabel" meta:resourcekey="lblRegSuccessResource1">  </asp:Label>
                    <asp:Literal ID="USER_RESISTER_SUCESSFUL_INFORMATION" runat="server" meta:resourcekey="USER_RESISTER_SUCESSFUL_INFORMATIONResource1"></asp:Literal>
                    <div class="sfButtonwrapper">
                        <span><a href='<%=LoginPath%>' class="sfBtn">Go To Login Page</a></span>
                    </div>
                </div>
            </div>
            <!--
        <asp:CheckBox ID="chkIsSubscribeNewsLetter" runat="server" CssClass="sfCheckbox" />
        <asp:Label ID="lblIsSubscribeNewsLetter" runat="server" Text="Subscribe Newsletter:"
                AssociatedControlID="CaptchaValue" CssClass="sfFormlabel"></asp:Label>
        <br />
        -->
        </div>
    </div>
</div>
