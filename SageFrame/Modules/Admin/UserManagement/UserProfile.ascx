<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="Modules_UserProfile" %>
<link href="../js/jquery-ui-1.8.14.custom/css/redmond/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function () {

        $("#TabProfile").tabs();
        var btnSave = '<%=btnSave.ClientID%>';
        $('#' + btnSave).on("click", function () {
            ValidationRules();
            ClearField();
        });
        $('#' + '<%=txtBirthDate.ClientID%>').attr('readOnly', 'true');
        $('#' + '<%=txtBirthDate.ClientID %>').datepicker({
            dateFormat: 'yy-mm-dd',
            changeMonth: true,
            changeYear: true,
            yearRange: '-100:+0'
        });
    });

    function ClearField() {

        $('#' + '<%=txtEmail1.UniqueID %>').text('');
        $('#' + '<%=txtEmail2.UniqueID %>').text('');
        $('#' + '<%=txtEmail3.UniqueID %>').text('');
        $('#' + '<%=txtResPhone.UniqueID %>').text('');
        $('#' + '<%=txtMobile.UniqueID %>').text('');
    }

    function ValidationRules() {
        $("#form1").validate({
            ignore: ':hidden',
            rules: {
                '<%=txtFName.UniqueID %>': { required: true },
                '<%=txtLName.UniqueID %>': { required: true },
                '<%=txtEmail1.UniqueID %>': { required: true, email: true },
                '<%=txtEmail2.UniqueID %>': { email: true },
                '<%=txtEmail3.UniqueID %>': { email: true },
                '<%=txtResPhone.UniqueID %>': { phone: true },
                '<%=txtMobile.UniqueID %>': { phone: true }

            },
            messages: {
                '<%=txtFName.UniqueID %>': "<br/>First Name should not be blank.",
                '<%=txtLName.UniqueID %>': "<br/>Last Name should not be blank.",
                '<%=txtEmail1.UniqueID %>': "<br/>Email should not be blank and must be in a correct format.",
                '<%=txtEmail2.UniqueID %>': "<br/>Email must be in a correct format.",
                '<%=txtEmail3.UniqueID %>': "<br/>Email must be in a correct format.",
                '<%=txtResPhone.UniqueID %>': "<br/>Please give Valid Phone No.",
                '<%=txtMobile.UniqueID %>': "<br/>Please give Valid Mobile No."
            }
        });
    }
    //]]>	

</script>

<div class="sfEditprofile">
    <div id="sfUserProfile" runat="server" class="sfFormwrapper sfUserprofile clearfix">
        <h1>User Profile Setting</h1>
        <div id="TabProfile">
            <ul>
                <li><a href="#ProfileSetting">UserInfo</a></li>
                <li><a href="#Changepswd">Change Password</a></li>
            </ul>
            <div id="ProfileSetting" class="sfFormwrapper clearfix">
                <div class="sfImagewrapper">
                    <p class="sfDisplayName">
                        <asp:Label ID="Label1" runat="server" CssClass="sfFormlabel" Text="User Name" meta:resourcekey="Label1Resource1"></asp:Label>
                        <asp:Label ID="lblDisplayUserName" runat="server" CssClass="sfFormlabel sfDefaultName"></asp:Label>
                    </p>
                    <div class="sfDefaultImage">
                        <asp:Image ID="imgUser" runat="server" Width="120px" meta:resourcekey="imgUserResource1" />
                    </div>
                    <span>Please upload square image</span>
                    <div class="sfProfileimage" runat="server" id="imgProfileEdit">
                        <label class="sfLocale icon-close">
                            <asp:Button ID="btnDeleteProfilePic" runat="server" OnClick="btnDeleteProfilePic_Click"
                                meta:resourcekey="btnDeleteProfilePicResource1" /></label><br />
                    </div>
                    <asp:FileUpload ID="fuImage" runat="server" meta:resourcekey="fuImageResource1" />
                </div>
                <div class="sfProfilewrapper">
                    <table id="tblEditProfile" runat="server" width="100%" style="margin-bottom: 20px; padding-right: 40px;">
                        <tr>
                            <td width="125px;">
                                <asp:Label ID="Label3" runat="server" CssClass="sfFormlabel" Text="First Name" meta:resourcekey="Label3Resource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFName" runat="server" CssClass="sfInputbox" meta:resourcekey="txtFNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" CssClass="sfFormlabel" Text="Last Name" meta:resourcekey="Label17Resource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLName" runat="server" CssClass="sfInputbox" meta:resourcekey="txtLNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFullName" runat="server" CssClass="sfFormlabel" Text="Full Name"
                                    meta:resourcekey="lblFullNameResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="sfInputbox" meta:resourcekey="txtFullNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server">
                                <asp:Label ID="lblBirthDate" runat="server" CssClass="sfFormlabel" Text="Birth Date"
                                    meta:resourcekey="lblBirthDateResource1"></asp:Label>
                            </td>
                            <td runat="server">
                                <asp:TextBox runat="server" ID="txtBirthDate" CssClass="sfInputbox" meta:resourcekey="txtBirthDateResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server">
                                <asp:Label ID="lblGender" runat="server" CssClass="sfFormlabel" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                            </td>
                            <td runat="server">
                                <asp:RadioButtonList runat="server" ID="rdbGender" meta:resourcekey="rdbGenderResource1"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">Male</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource2">Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLocation" runat="server" CssClass="sfFormlabel" Text="Location"
                                    meta:resourcekey="lblLocationResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLocation" runat="server" CssClass="sfInputbox" meta:resourcekey="txtLocationResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAboutYou" runat="server" CssClass="sfFormlabel" Text="About You"
                                    meta:resourcekey="lblAboutYouResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAboutYou" runat="server" CssClass="sfInputbox" TextMode="MultiLine"
                                    meta:resourcekey="txtAboutYouResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <h3>Contacts</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" CssClass="sfFormlabel" Text="Email" meta:resourcekey="Label18Resource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" CssClass="sfInputbox"  MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail1" runat="server" ControlToValidate="txtEmail1"
                                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="rfvUserProfile"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="sfError"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtEmail2" runat="server" CssClass="sfInputbox"  MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail2" runat="server" ControlToValidate="txtEmail2"
                                    Display="Dynamic" SetFocusOnError="True"  ValidationGroup="rfvUserProfile"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="sfError"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtEmail3" runat="server" CssClass="sfInputbox" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail3" runat="server" ControlToValidate="txtEmail3"
                                    Display="Dynamic" SetFocusOnError="True"  ValidationGroup="rfvUserProfile"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="sfError"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblResPhone" runat="server" CssClass="sfFormlabel" Text="Res. Phone"
                                    meta:resourcekey="lblResPhoneResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtResPhone" runat="server" CssClass="sfInputbox" meta:resourcekey="txtResPhoneResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMobilePhone" runat="server" CssClass="sfFormlabel" Text="Mobile"
                                    meta:resourcekey="lblMobilePhoneResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="sfInputbox" meta:resourcekey="txtMobileResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOthers" runat="server" CssClass="sfFormlabel" Text="Others" meta:resourcekey="lblOthersResource1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthers" runat="server" CssClass="sfInputbox" OnTextChanged="txtOthers_TextChanged"
                                    TextMode="MultiLine" meta:resourcekey="txtOthersResource1"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="sfButtonwrapper clearfix sfTopmargin20" id="divSaveProfile" runat="server">
                        <label class="sfLocale sfBtn icon-save">
                            Save
                        <asp:Button runat="Server" ID="btnSave" OnClick="btnSave_Click"
                            ValidationGroup="rfvUserProfile" meta:resourcekey="btnSaveResource1" /></label>
                        <asp:LinkButton ID="lnkCancel" CssClass="sfBtn icon-close" runat="server" Text="Cancel" OnClick="lnkCancel_Click"
                            meta:resourcekey="lnkCancelResource1"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div id="Changepswd">
                <table id="tblChangePasswordSettings" runat="server" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" width="36%" runat="server">
                            <asp:Label ID="lblNewPassword" runat="server" CssClass="sfFormlabel" Text="New Password"
                                meta:resourcekey="lblNewPasswordResource1"></asp:Label>
                        </td>
                        <td id="Td3" runat="server">
                            <div class="sfPassword">
                                <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" CssClass="sfInputbox password"
                                    TextMode="Password" ValidationGroup="vgManagePassword" meta:resourcekey="txtNewPasswordResource1" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPassword"
                                    ErrorMessage="Password is required." CssClass="sfRequired" ValidationGroup="vgManagePassword"
                                    meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td4" runat="server">
                            <asp:Label ID="lblRetypeNewPassword" runat="server" CssClass="sfFormlabel" Text="Retype New Password"
                                meta:resourcekey="lblRetypeNewPasswordResource1"></asp:Label>
                        </td>
                        <td id="Td6" runat="server">
                            <asp:TextBox ID="txtRetypeNewPassword" runat="server" MaxLength="20" CssClass="sfInputbox"
                                TextMode="Password" ValidationGroup="vgManagePassword" meta:resourcekey="txtRetypeNewPasswordResource1" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRetypeNewPassword"
                                ErrorMessage="Type password again." CssClass="sfRequired" ValidationGroup="vgManagePassword"
                                Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="Password Doesn't Match"
                                ControlToValidate="txtRetypeNewPassword" ControlToCompare="txtNewPassword" ValidationGroup="vgManagePassword"
                                Display="Dynamic"></asp:CompareValidator>
                            <label id="lblValidationmsg" class="sfRequired">
                            </label>
                        </td>
                    </tr>
                    <tr id="Tr3" runat="server">
                        <td id="Td8" runat="server">&nbsp;
                            
                        </td>
                        <td id="Td9" runat="server">
                            <div class="sfButtonwrapper">
                                <label class="sfLocale sfBtn icon-save">
                                    Save
                                <asp:Button ID="btnManagePasswordSave" runat="server" ValidationGroup="vgManagePassword"
                                    OnClick="btnManagePasswordSave_Click" meta:resourcekey="btnManagePasswordSaveResource1" />
                                </label>
                            </div>
                            <div class="sfValidationsummary">
                                <label id="lblChangepwdval">
                                </label>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="divUserInfo" runat="server" class="sfUserprofile sfFormwrapper sfUserBasic clearfix">
        <h1>User Profile</h1>
        <div class="sfViewprofile sfPadding clearfix">
            <table id="tblViewProfile" cellpadding="0" cellspacing="0" width="100%" runat="server">
                <tr>
                    <td width="15%">
                        <asp:Label ID="Label2" runat="server" CssClass="sfFormlabel" Text="User Name" meta:resourcekey="Label2Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblViewUserName" runat="server" meta:resourcekey="lblViewUserNameResource1"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="sfFormlabel" Text="First Name" meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewFirstName" runat="server" meta:resourcekey="lblViewFirstNameResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="sfFormlabel" Text="LastName" meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewLastName" runat="server" meta:resourcekey="lblViewLastNameResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trviewFullName" runat="server" visible="true">
                    <td class="style1">
                        <asp:Label ID="Label6" runat="server" CssClass="sfFormlabel" Text="FullName" meta:resourcekey="Label6Resource1"></asp:Label>
                    </td>
                    <td colspan="2" class="style1">
                        <asp:Label ID="lblviewFullName" runat="server" meta:resourcekey="lblviewFullNameResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trviewBirthDate" runat="server">
                    <td runat="server">
                        <asp:Label ID="lblBirthDateTest" runat="server" Text="BirthDate" CssClass="sfFormlabel"
                            meta:resourcekey="lblBirthDateTestResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblviewBirthDate" meta:resourcekey="lblviewBirthDateResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trviewGender" runat="server">
                    <td runat="server">
                        <asp:Label ID="lblGenderText" runat="server" Text="Gender" CssClass="sfFormlabel"
                            meta:resourcekey="lblGenderTextResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblviewGender" meta:resourcekey="lblviewGenderResource1"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trViewLocation" visible="true">
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="sfFormlabel" Text="Location" meta:resourcekey="Label7Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewLocation" runat="server" meta:resourcekey="lblViewLocationResource1"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trViewAboutYou" visible="true">
                    <td>
                        <asp:Label ID="Label8" runat="server" CssClass="sfFormlabel" Text="About You" meta:resourcekey="Label8Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewAboutYou" runat="server" meta:resourcekey="lblViewAboutYouResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trViewEmail" runat="server">
                    <td>
                        <asp:Label ID="Label9" runat="server" CssClass="sfFormlabel" Text="Email" meta:resourcekey="Label9Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewEmail1" runat="server" meta:resourcekey="lblViewEmail1Resource1"></asp:Label><br />
                        <asp:Label ID="lblViewEmail2" runat="server" meta:resourcekey="lblViewEmail2Resource1"></asp:Label><br />
                        <asp:Label ID="lblViewEmail3" runat="server" meta:resourcekey="lblViewEmail3Resource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trViewResPhone" runat="server">
                    <td>
                        <asp:Label ID="Label10" runat="server" CssClass="sfFormlabel" Text="Res. Phone" meta:resourcekey="Label10Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewResPhone" runat="server" meta:resourcekey="lblViewResPhoneResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trViewMobile" runat="server">
                    <td>
                        <asp:Label ID="Label11" runat="server" CssClass="sfFormlabel" Text="Mobile" meta:resourcekey="Label11Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewMobile" runat="server" meta:resourcekey="lblViewMobileResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trViewOthers" runat="server">
                    <td>
                        <asp:Label ID="Label12" runat="server" CssClass="sfFormlabel" Text="Others" meta:resourcekey="Label12Resource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblViewOthers" runat="server" meta:resourcekey="lblViewOthersResource1"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="sfButtonwrapper" id="divEditprofile" runat="server">
                <label class="sfLocale sfBtn icon-edit">
                    Edit
                <asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_Click"
                    meta:resourcekey="btnEditResource1" />
                </label>
            </div>
        </div>
        <div class="sfProfileimage" runat="server" id="imgProfileView">
            <asp:Image ID="imgViewImage" runat="server" Width="120px" meta:resourcekey="imgViewImageResource1" />
        </div>
    </div>
</div>
