<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HandleModuleControls.aspx.cs"
    Inherits="Sagin_HandleModuleControls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <title>Untitled Page</title>
    <asp:PlaceHolder ID="pchHolder" runat="server"></asp:PlaceHolder>
    <asp:Literal runat="server" ID="ltrJQueryLibrary"></asp:Literal>
    <asp:Literal ID="SageFrameModuleCSSlinks" runat="server"></asp:Literal>
    <link href="../Administrator/Templates/Default/css/iframe.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="sfFormwrapper">
            <asp:PlaceHolder ID="message" runat="server"></asp:PlaceHolder>
            <ajax:TabContainer ID="TabContainerManagePages" runat="server" ActiveTabIndex="0"
                meta:resourcekey="TabContainerManagePagesResource1">
                <ajax:TabPanel ID="TabPanelEdit" runat="server" HeaderText="Edit">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="pchEdit" runat="server"></asp:PlaceHolder>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabPanelSettings" runat="server" HeaderText="Settings">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="pchSetting" runat="server"></asp:PlaceHolder>
                    </ContentTemplate>
                </ajax:TabPanel>
            </ajax:TabContainer>
        </div>
        <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>

        <script type="text/javascript">
            //<![CDATA[


            $(function () {
                if ($('.ajax__tab_outer').length === 1)
                    $($('.ajax__tab_outer')).hide();
            });
            String.Format = function () {
                var s = arguments[0];
                for (var i = 0; i < arguments.length - 1; i++) {
                    var reg = new RegExp("\\{" + i + "\\}", "gm");
                    s = s.replace(reg, arguments[i + 1]);
                }
                return s;
            }
            var dialogConfirmed = false;
            function ConfirmDialog(obj, title, dialogText) {
                if (!dialogConfirmed) {
                    $('body').append(String.Format("<div id='dialog-confirm' title='{0}'><p>{1}</p></div>",
                        title, dialogText));
                    if (title == "message") {
                        $('#dialog-confirm').dialog
                        ({
                            height: 150,
                            width: 350,
                            modal: true,
                            resizable: false,
                            draggable: false,
                            close: function (event, ui) { $('body').find('#dialog-confirm').remove(); },
                            buttons:
                            {
                                'OK': function () {
                                    $(this).dialog('close');
                                }
                            }
                        });
                    }
                    else {
                        $('#dialog-confirm').dialog
                        ({
                            height: 110,
                            modal: true,
                            resizable: false,
                            draggable: false,
                            close: function (event, ui) { $('body').find('#dialog-confirm').remove(); },
                            buttons:
                            {
                                'Yes': function () {
                                    $(this).dialog('close');
                                    dialogConfirmed = true;
                                    if (obj) obj.click();
                                },
                                'No': function () {
                                    $(this).dialog('close');
                                }
                            }
                        });
                    }
                }
                return dialogConfirmed;
            }
            function pageLoad(sender, args) {
                if (args.get_isPartialLoad()) {
                    String.Format = function () {
                        var s = arguments[0];
                        for (var i = 0; i < arguments.length - 1; i++) {
                            var reg = new RegExp("\\{" + i + "\\}", "gm");
                            s = s.replace(reg, arguments[i + 1]);
                        }
                        return s;
                    }
                    var dialogConfirmed = false;
                    function ConfirmDialog(obj, title, dialogText) {
                        if (!dialogConfirmed) {
                            $('body').append(String.Format("<div id='dialog-confirm' title='{0}'><p>{1}</p></div>",
                        title, dialogText));
                            if (title == "message") {
                                $('#dialog-confirm').dialog
                        ({
                            height: 110,
                            modal: true,
                            resizable: false,
                            draggable: false,
                            close: function (event, ui) { $('body').find('#dialog-confirm').remove(); },
                            buttons:
                            {
                                'OK': function () {
                                    $(this).dialog('close');
                                }
                            }
                        });
                            }
                            else {
                                $('#dialog-confirm').dialog
                        ({
                            height: 110,
                            modal: true,
                            resizable: false,
                            draggable: false,
                            close: function (event, ui) { $('body').find('#dialog-confirm').remove(); },
                            buttons:
                            {
                                'Yes': function () {
                                    $(this).dialog('close');
                                    dialogConfirmed = true;
                                    if (obj) obj.click();
                                },
                                'No': function () {
                                    $(this).dialog('close');
                                }
                            }
                        });
                            }
                        }
                        return dialogConfirmed;
                    }
                }
            }
            //]]>	
        </script>

        <div id="dialog" title="Confirmation Required">
            <label id="sf_lblConfirmation">
            </label>
        </div>

    </form>
</body>
</html>
