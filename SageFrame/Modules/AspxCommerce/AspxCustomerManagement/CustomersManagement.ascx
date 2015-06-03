<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomersManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_CustomersManagement" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCustomerManagement
        });
    });
 
    var checkIfSucccess = '<%=CheckIfSucccess %>';
    var newCustomerRss = '<%=NewCustomerRss %>';
    var rssFeedUrl = '<%=RssFeedUrl %>';
    var umi = '<%=UserModuleId%>';
    //]]>
</script>
<div id="divCustomerList">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttrGridHeading" runat="server" Text="Manage Customers" meta:resourcekey="lblAttrGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassRssDiv">
                <a class="cssRssImage" href="#" style="display: none">
                    <img id="customerRssImage" alt="" src="" title="" />
                </a>
            </div>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewCustomer" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Customer</span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelectedCustomer" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Customer Name:</label>
                                <input type="text" id="txtSearchUserName1" class="sfTextBoxSmall" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <button type="button" id="btnSearchRegisteredUser" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCutomerMgmtImageLoad" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divAddNewCustomer" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">            
            <h1>
                <asp:Label ID="lblCustomerHeading" runat="server" Text="Add New Customer" meta:resourcekey="lblCustomerHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="cssClassUserRegistrationPage">
            <div class="cssClassUserRegistration">
                <div class="sfFormwrapper">
                    <div id="divRegistration" runat="server">
                        <div class="cssClassRegistrationInformation">
                            <%= headerTemplate %>
                        </div>
                        <span class="cssClassRequired sfLocale">Fields marked with * are compulsory.
                        </span>
                        <div class="cssClassUserRegistrationInfoLeft">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2">
                                        <h3 class="sfLocale">
                                            User Info</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="270">
                                        <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" CssClass="sfFormlabel"
                                            meta:resourcekey="FirstNameLabelResource1" Text="First Name: "></asp:Label><span
                                                class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="FirstName" runat="server" CssClass="sfInputbox fristName" meta:resourcekey="FirstNameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="FirstName"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td>
                                        <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName" CssClass="sfFormlabel"
                                            meta:resourcekey="LastNameLabelResource1" Text="Last Name:"></asp:Label><span class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="LastName" runat="server" CssClass="sfInputbox lastName" meta:resourcekey="LastNameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="LastName"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvLastNameResource1"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="sfFormlabel"
                                            meta:resourcekey="EmailLabelResource1" Text="E-mail:"></asp:Label>
                                        <span class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBgBig">
                                            <asp:TextBox ID="Email" runat="server" CssClass="sfInputbox email" meta:resourcekey="EmailResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="Email"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClassrequired"
                                                meta:resourcekey="rfvEmailRequiredResource1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email"
                                                SetFocusOnError="True" ErrorMessage="Please enter a valid Email" ValidationGroup="CreateUserWizard1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                CssClass="cssClassrequired" meta:resourcekey="revEmailResource1"></asp:RegularExpressionValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h3 class="sfLocale">
                                            Create Login</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="sfFormlabel"
                                            meta:resourcekey="UserNameLabelResource1" Text="User Name:"></asp:Label><span class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="sfInputbox userName" meta:resourcekey="UserNameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvUserNameRequiredResource1"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="sfFormlabel"
                                            meta:resourcekey="PasswordLabelResource1" Text="Password:"></asp:Label><span class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="cssClassNormalTestBox sfInputbox  password"
                                                meta:resourcekey="PasswordResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPasswordRequired" runat="server" ControlToValidate="Password"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvPasswordRequiredResource1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password must be at-least of 4 character.."
                                                ControlToValidate="Password" ValidationGroup="CreateUserWizard1" ValidationExpression=".{4,}"
                                                meta:resourcekey="RegularExpressionValidator1Resource1" Text="*"></asp:RegularExpressionValidator>
                                        </p>
                                    </td>
                                    <td>
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                                            CssClass="sfFormlabel" meta:resourcekey="ConfirmPasswordLabelResource1" Text="Confirm Password:"></asp:Label><span
                                                class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="sfInputbox confirmPassword"
                                                meta:resourcekey="ConfirmPasswordResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvConfirmPasswordRequiredResource1"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvPasswordCompare" runat="server" ControlToCompare="Password"
                                                ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Confirm Password must match with Password" ValidationGroup="CreateUserWizard1"
                                                CssClass="cssClasssNormalRed" meta:resourcekey="cvPasswordCompareResource1"></asp:CompareValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" CssClass="sfFormlabel"
                                            meta:resourcekey="QuestionLabelResource1" Text="Security Question:"></asp:Label><span
                                                class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="Question" runat="server" CssClass="sfInputbox question" meta:resourcekey="QuestionResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvQuestionRequired" runat="server" ControlToValidate="Question"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvQuestionRequiredResource1"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td>
                                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" CssClass="sfFormlabel"
                                            meta:resourcekey="AnswerLabelResource1" Text="Security Answer:"></asp:Label><span
                                                class="cssClassrequired">*</span>
                                        <p class="cssClassRegisterInputBg">
                                            <asp:TextBox ID="Answer" runat="server" CssClass="sfInputbox answer" meta:resourcekey="AnswerResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAnswerRequired" runat="server" ControlToValidate="Answer"
                                                ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"
                                                meta:resourcekey="rfvAnswerRequiredResource1"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <asp:CheckBox ID="chkIsSubscribeNewsLetter" runat="server" CssClass="cssClassCheckBox"
                            meta:resourcekey="chkIsSubscribeNewsLetterResource1" />
                        <asp:Label ID="lblIsSubscribeNewsLetter" runat="server" Text="Subscribe Newsletter"
                            CssClass="sfFormlabel" meta:resourcekey="lblIsSubscribeNewsLetterResource1"></asp:Label>
                        <%-- <asp:Label ID="lblIsSubscribeNewsLetter" runat="server" Text="Subscribe Newsletter:"
                AssociatedControlID="CaptchaValue" CssClass="sfFormlabel"></asp:Label>--%>
                        <br />
                        <div class="sfButtonwrapper">
                            <p>
                                <button type="button" id="btnBack" class="sfBtn">
                                    <span class="sfLocale icon-arrow-slim-w">Back</span>
                                </button>
                            </p>
                            <p>
                                <label class="sfBtn icon-save">
                                    <span class="sfLocale">Register</span>
                                    <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" BorderStyle="None"
                                        ValidationGroup="CreateUserWizard1" CssClass="cssClassButtonSubmit" OnClick="FinishButton_Click" /></label>
                            </p>
                        </div>
                    </div>
                    <%--<div id="divRegConfirm" class="cssClassRegConfirm" runat="server">
                <h3>
                    Registration Successful</h3>
                <asp:Label ID="lblRegSuccess" runat="server" CssClass="sfFormlabel">
                 <asp:Literal ID="USER_RESISTER_SUCESSFUL_INFORMATION" runat="server" meta:resourcekey="USER_RESISTER_SUCESSFUL_INFORMATIONResource1"></asp:Literal>            
                 </asp:Label>
              <div class="sfButtonwrapper">                   
                    <span><a href='<%=LoginPath%>'>Go To Login Page</a></span>
                </div>
            </div>--%>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divCustomerInfo" style="display: none">
    <div class="cssClassHeader">
        <h1>
            <span>Customer Information</span>
        </h1>
    </div>
    <div class="sfButtonwrapper">
        <button type="button" id="btnPersonalInfoBack" class="sfBtn">
            <span class="sfLocale icon-arrow-slim-w">Back</span>
        </button>
    </div>
    <input type="hidden" id="selectedCustomerID" />
    <input type="hidden" id="selectedCustomerUserName" />
    <div class="cssClassHeader">
        <h3>
            <label class="sfLocale">
                Customer Personal Details</label>
        </h3>
    </div>
    <div class="sfFormwrapper sfSecMrg-b clearfix">
        <div id="divCustDetail">
        </div>
    </div>
    <div class="cssClassHeader">
        <h3>
            <label class="sfLocale">
                Customer Recent Orders</label>
        </h3>
    </div>
    <div id="divCustRecentOrd" class="sfGridwrapper sfSecMrg-b">
        <div class="sfGridWrapperContent">
            <div class="loading">
                <img id="Img1" src="" alt="loading...." title="loading...." />
            </div>
            <div class="log">
            </div>
            <table id="gdvCustRecentOrders" cellspacing="0" cellpadding="0" border="0" width="100%">
            </table>
        </div>
    </div>
    <div class="cssClassHeader">
        <h3>
            <label class="sfLocale">
                Customer Shopping cart Items</label>
        </h3>
    </div>
    <div class="sfTableOption">
        <p>
            <button type="button" id="btnDeleteSelectedShop" class="sfBtn">
                <span class="sfLocale icon-delete">Delete Selected Items</span>
            </button>
        </p>
    </div>
    <div id="divCustShoppingCart" class="sfGridwrapper sfSecMrg-b">
        <div class="sfGridWrapperContent">
            <div class="loading">
                <img id="Img2" src="" alt="loading...." title="loading...." />
            </div>
            <div class="log">
            </div>
            <table id="gdvCustShoppCartDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
            </table>
        </div>
    </div>
    <div class="cssClassHeader">
        <h3>
            <label class="sfLocale">
                Customer Wishlist Items</label>
        </h3>
    </div>
    <div class="sfTableOption">
        <p>
            <button type="button" id="btnDeleteSectedWish" class="sfBtn">
                <span class="sfLocale icon-delete">Delete Selected Items</span>
            </button>
        </p>
    </div>
    <div id="divCustWish" class="sfGridwrapper">
        <div class="sfGridWrapperContent">
            <div class="loading">
                <img id="Img3" src="" alt="loading...." title="loading...." />
            </div>
            <div class="log">
            </div>
            <table id="gdvCustWishlist" cellspacing="0" cellpadding="0" border="0" width="100%">
            </table>
        </div>
    </div>
</div>
