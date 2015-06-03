<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstallWizard.aspx.cs" Inherits="Install_InstallWizard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/install.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            var flag;
            var check = "";
            var ExistingDatabasename = "<%=txtExistingDatabaseName.ClientID%>";
            var NewDatabasename = "<%=txtNewDataBaseName.ClientID%>";
            var btnInstall = "<%=btnInstall.ClientID%>";
            var txtServer = "<%=txtServer.ClientID%>";
            var txtUserId = "<%=txtUserId.ClientID%>";
            var txtPassword = "<%=txtPassword.ClientID%>";
            var chkIntegrated = "<%=chkIntegrated.ClientID %>";
            var txtDataBase = "<%=txtDataBase.ClientID %>";
            var btnTestPermission = "<%=btnTestPermission.ClientID %>";

            $('#' + ExistingDatabasename).attr('disabled', 'disabled');

            check = document.cookie.split(';');
            flag = check[0];
            if (flag == "rdbExistingDatabase") {
                $('#rdbExistingDatabase').attr("checked", true);
                $('#rdbCreateDatabase').attr("checked", false);
                $('#' + NewDatabasename).attr('disabled', 'true').addClass("sfInactive");
                $('#' + NewDatabasename).val('');
                $('#' + ExistingDatabasename).attr('disabled', false).removeClass("sfInactive");

            } if (flag == "rdbCreateDatabase") {
                $('#rdbExistingDatabase').attr("checked", false);
                $('#rdbCreateDatabase').attr("checked", true);
                $('#' + ExistingDatabasename).attr("disabled", "disabled").addClass("sfInactive");
                $('#' + ExistingDatabasename).val('');
                $('#' + NewDatabasename).attr('disabled', false).removeClass("sfInactive");

            }


            $('#' + btnTestPermission).bind("click", function(e) {
                if ($('#rdbCreateDatabase').attr("checked")) {
                    document.cookie = $('#rdbCreateDatabase').attr("id");
                    check = document.cookie.split(';');
                    flag = check[0];
                } else {
                    document.cookie = $('#rdbExistingDatabase').attr("id");
                    check = document.cookie.split(';');
                    flag = check[0];
                }

            });


            $('#' + btnInstall).bind("click", function() {

                if ($('#' + chkIntegrated).attr('checked')) {

                    if ($('#' + txtServer).val() == "") {
                        $("#lblServerError").text("Please Enter a Server name");
                        return false;
                    }
                    if ($('#' + txtDataBase).val() == "") {
                        $("#lblDatabaseError").text("Please Enter a Database Name");
                        return false;
                    }
                }
                else {
                    var result = true;
                    result = CheckValidation();
                    return result;
                }

            });


            $('#rdbCreateDatabase').bind("click", function() {
                $('#' + ExistingDatabasename).attr("disabled", "disabled").addClass("sfInactive");
                $('#' + ExistingDatabasename).val('');
                $('#' + NewDatabasename).attr('disabled', false).removeClass("sfInactive");
                $('#lblExistingDatabaseError').html('');
                $('#lblNewDatabaseError').show();

            });
            $('#rdbExistingDatabase').bind("click", function() {
                $('#' + NewDatabasename).attr('disabled', 'true').addClass("sfInactive");
                $('#' + NewDatabasename).val('');
                $('#' + ExistingDatabasename).attr('disabled', false).removeClass("sfInactive");
                $('#lblNewDatabaseError').html('');
                $('#lblExistingDatabaseError').show();
            });

            $('#' + txtServer).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblServerError').html('').hide();
                }
            });
            $('#' + txtDataBase).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblDatabaseError').html('').hide();
                }
            });
            $('#' + txtUserId).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblUserIdError').html('').hide();
                }
            });
            $('#' + txtPassword).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblPasswordError').html('').hide();
                }
            });
            $('#' + ExistingDatabasename).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblExistingDatabaseError').html('').hide();
                }
            });
            $('#' + NewDatabasename).keyup(function(e) {
                if ($(this).val().length > 0) {
                    $('#lblNewDatabaseError').html('').hide();
                }
            });
            function CheckValidation() {
                if ($('#' + txtServer).val() == "") {
                    $("#lblServerError").text("Please Enter a Server name");
                    return false;
                }
                if ($('#' + txtUserId).val() == "") {

                    $("#lblUserIdError").text("Please Enter a UserId");
                    return false;
                }
                if ($('#' + txtPassword).val() == "") {

                    $("#lblPasswordError").text("Please Enter a Password");
                    return false;
                }
                if ($('#rdbExistingDatabase').prop("checked") && $('#' + ExistingDatabasename).val().trim().length == 0) {
                    $("#lblExistingDatabaseError").text("Please Enter a Exesting Database Name");
                    return false;
                }
                if ($('#rdbCreateDatabase').prop("checked") && $('#' + NewDatabasename).val().trim().length == 0) {
                    $("#lblNewDatabaseError").text("Please Enter a New Database Name");
                    return false;
                }

            }
            $('#divTemplateList input:radio').prop("checked", false);
            $('#divTemplateList input:radio:first').prop("checked", true);
            $('#divTemplateList input:radio').on("change", function() {
                ////                if ($(this).attr("checked")) {
                ////                    $('#divTemplateList input:radio').not($(this)).attr("checked", false);
                ////                }
                ////                else {
                // $(this).prop("checked", true);
                $('#divTemplateList input:radio').not($(this)).attr("checked", false);
                //}
            });

        });
        function eraseCookie(name) {
            createCookie(name, "", -1);
        }

    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/SageFrameWebService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div id="sfInstallWrapper" class="clearfix">
        <div class="sfLogo">
            <h1>
                <asp:Label ID="lblTitle" runat="server" />
            </h1>
            <div class="sfVersion">
                <asp:Label ID="lblVersion" runat="server" />
            </div>
        </div>
        <div id="divHelp" class="sfHelp">
            <a href="~/Help.htm" class="icon-help" runat="server" data-title="Help" target="_blank"></a>
        </div>
        <div class="sfOuter sfCurve">
            <div class="sfInner sfCurve">
                <asp:Label ID="lblInstallError" runat="server" Visible="false" />
                <asp:HiddenField ID="hdnConnectionStringForAll" runat="server" Value="" />
                <asp:HiddenField ID="hdnNextButtonClientID" runat="server" Value="0" />
                <asp:Label ID="lblPermissionsError" runat="server" CssClass="cssClasssNormalRed"
                    EnableViewState="false" Visible="false" />
                <asp:Label ID="lblDataBaseError" runat="server" CssClass="cssClasssNormalRed" EnableViewState="false" />
                <asp:Label ID="lblRequiredDatabaseName" runat="server" CssClass="cssClasssNormalRed"
                    EnableViewState="false" />
                <asp:Panel ID="pnlStartInstall" runat="server">
                    <div class="sfInstallpart">
                        <div class="sfFormwrapper">
                            <p class="sfWelcomeText">
                                Welcome to the AspxCommerce Installation Wizard.
                            </p>
                            <h2>
                                Let's get started !</h2>
                            <table id="tblDatabase" runat="Server" cellpadding="0" cellspacing="0" border="0"
                                width="100%" class="sfInstalllationTable">
                                <tr>
                                    <td colspan="3">
                                        <h3>
                                            Database Credentials</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="140px">
                                        <%--<asp:Label ID="lblFileName" runat="Server" CssClass="sfFormlabel" />--%>
                                        <asp:Label ID="lblServer" runat="Server" CssClass="sfFormlabel" />
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtServer" runat="Server" CssClass="sfInputbox" />
                                        <i class="icon-info" data-title="Enter the Name or IP Address of the computer where the Database is located.">
                                        </i>
                                        <%--<asp:Label ID="lblServerHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                        <label id="lblServerError" class="sfError">
                                        </label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr class="sfTdseperator">
                                    <td>
                                        <asp:Label ID="lblIntegrated" runat="Server" CssClass="sfFormlabel" />
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkIntegrated" runat="Server" AutoPostBack="True" OnCheckedChanged="chkIntegrated_CheckedChanged"
                                            CssClass="sfCheckBox" />
                                        <asp:Label ID="lblIntegratedHelp" runat="Server" CssClass="sfHelptext sfInline" />
                                    </td>
                                </tr>
                                <tr id="trDatabaseName" runat="server" visible="false" class="sfTdseperator">
                                    <td>
                                        <br />
                                        <span class="sfFormlabel">Database Name</span>
                                    </td>
                                    <td colspan="2">
                                        <br />
                                        <%--<asp:Label ID="lblDatabase" runat="Server" CssClass="sfFormlabel" Text="Data Base:" />--%>
                                        <asp:TextBox ID="txtDataBase" runat="Server" CssClass="sfInputbox" />
                                        <i class="icon-info" data-title="Enter a database name"></i>
                                        <%-- <asp:Label ID="lblDatabaseNameHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                        <label id="Label1" class="sfError">
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trUser" runat="Server" class="sfTdseperator">
                                    <td>
                                    </td>
                                    <td colspan="2" class="sfCustomLabel">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="sfSubTable">
                                            <tr>
                                                <td width="75px;">
                                                    <asp:Label ID="lblUserID" runat="Server" CssClass="sfFormlabel" />
                                                </td>
                                                <td width="215px;">
                                                    <asp:TextBox ID="txtUserId" runat="Server" CssClass="sfInputbox" />
                                                    <label id="lblUserIdError" class="sfError">
                                                    </label>
                                                </td>
                                                <td>
                                                    <i class="icon-info" data-title="User ID to access the server"></i>
                                                    <%-- <asp:Label ID="lblUserHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                                </td>
                                            </tr>
                                            <tr id="trPassword" runat="Server" class="sfTdseperator">
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="Server" CssClass="sfFormlabel" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPassword" runat="Server" CssClass="sfInputbox" TextMode="Password"
                                                        EnableViewState="true" />
                                                    <label id="lblPasswordError" class="sfError">
                                                    </label>
                                                </td>
                                                <td>
                                                    <i class="icon-info" data-title="Password to access the server"></i>
                                                    <%--<asp:Label ID="lblPasswordHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trDatabaseHeading" runat="server">
                                    <td>
                                        <span class="sfFormlabel">Database Name</span>
                                    </td>
                                    <td id="trrdbCreateDatabase" runat="server" class="sfTdseperator">
                                        <label>
                                            <input id="rdbCreateDatabase" type="radio" name="rdbDataBase" checked="checked" />
                                            Create New Database</label>
                                    </td>
                                    <td id="trrdbExistingDatabase" runat="server" class="sfgap">
                                        <label>
                                            <input id="rdbExistingDatabase" type="radio" name="rdbDataBase" />
                                            Existing Database</label>
                                    </td>
                                </tr>
                                <tr class="sfDBNameField">
                                    <td>
                                    </td>
                                    <td id="trnewDatabase" runat="server" class="sfgap">
                                        <asp:TextBox ID="txtNewDataBaseName" runat="Server" CssClass="sfInputbox" AutoPostBack="false" />
                                        <%--<asp:Label ID="lblNewDatabaseHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                        <label id="lblNewDatabaseError" class="sfError">
                                        </label>
                                    </td>
                                    <td id="trExistingDatabase" runat="server" class="sfgap2">
                                        <asp:TextBox ID="txtExistingDatabaseName" runat="Server" CssClass="sfInputbox" AutoPostBack="false" />
                                        <%--<asp:Label ID="lblExistingDatabaseHelp" runat="Server" CssClass="sfHelptext icon-info" />--%>
                                        <label id="lblExistingDatabaseError" class="sfError">
                                        </label>
                                    </td>
                                </tr>
                            </table>
                            <div class="sfButtonwrapper">
                                <p>
                                    (Optional) You can test your database connectivity before installing AspxCommerce to
                                    check whether you have configured database properly or not. However, you can skip
                                    this step and install AspxCommerce directly as well.</p>
                                <asp:Button ID="btnTestPermission" runat="server" CssClass="sfBtn" Text=" Test Configuration"
                                    OnClick="btnTestPermission_Click" /></div>
                        </div>
                        <div id="divTemplateList" class="sfTemplate">
                            <h3>
                                Choose Template
                            </h3>
                            <ul>
                                <asp:Repeater ID="rptrTemplateList" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <p>
                                                <asp:RadioButton ID="chkIsActive" runat="server" GroupName="SelectTemplate" Text='<%#Eval("TemplateName") %>' />
                                                <asp:Label ID="lblTemplateName" runat="server" Text='<%#Eval("TemplateName") %>'
                                                    CssClass="sfHide" />
                                            </p>
                                            <asp:Image ID="imgThubNail" runat="server" ImageUrl='<%#Eval("ThumbImage") %>' />
                                            <br />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
            </div>
            <div class="sfinstalbtn">
                <asp:Button ID="btnInstall" runat="server" CssClass="sfBtn" Text="Install AspxCommerce"
                    OnClick="btnInstall_Click" />
            </div>
            </asp:Panel>
            <asp:Timer runat="server" ID="UpdateTimer" Interval="1000" OnTick="UpdateTimer_Tick"
                Enabled="false" />
            <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <div class="sfProcessWrapper">
                        <div class="sfmaincontent">
                            <div class="sfloadingDiv" id="loadingDiv" runat="server">
                               <h2> <asp:Label ID="lblDBProgress" runat="server" Text="Installing Database Scripts ...Please wait...This may take a moment"
                                    EnableViewState="false"></asp:Label></h2>
                                <asp:Image ID="imgDBProgress" runat="server" AlternateText="Installing Database Scripts..."
                                    ToolTip="Installing Database Scripts..." />
                            </div>
                            <asp:TextBox ID="txtFeedback" runat="server" class="cssClassFeedBack" Columns="60"
                                Rows="6" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="lblInstallErrorOccur" runat="server" Visible="false" EnableViewState="false" />
                        </div>
                    </div>
                    <div class="sfButtonwrapper">
                        <asp:Button ID="btnCancel" runat="server" CssClass="sfBtn" Text="Cancel" Visible="false"
                            OnClick="btnCancel_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
