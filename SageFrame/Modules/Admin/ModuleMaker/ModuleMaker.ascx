<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleMaker.ascx.cs" Inherits="Modules_Admin_ModuleMaker" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(this).ModuleMaker({
            UserModuleID: '<%=userModuleID%>',
            hdnTableList: "#" + "<%=hdnTableList.ClientID%>",
            databaseModuleList: "#" + "<%=hdnModuleListFromDatabase.ClientID%>",
            folderModuleList: "#" + "<%=hdnModuleList.ClientID%>",
            hdnXML: "#" + "<%=hdnXML.ClientID%>",
            txttableName: "#" + "<%=txtTablename.ClientID%>",
            txtModuleDescription: "#" + "<%=txtModuleDescription.ClientID%>",
            txtModuleName: "#" + "<%=txtModuleName.ClientID%>"
        });
    });
    //]]>
</script>
<h1>Module Maker</h1>
<div class="moduleCreator" id="divModuleCreator">
    <div class="sfFormwrapper clearfix">
        <h4></h4>
        <h3>Basic settings</h3>
        <table>
            <tr>
                <td>Module Name</td>
                <td>
                    <asp:TextBox runat="server" ID="txtModuleName" CssClass="sfInputbox"></asp:TextBox></td>
                <td><span class="sfloader icon-search" style="display: none;"></span><span class="error sfError"></span></td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <asp:TextBox TextMode="MultiLine" Columns="20" runat="server" CssClass="sfInputbox" ID="txtModuleDescription"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td>Include</td>
                <td>
                    <label>
                        <asp:CheckBox runat="server" ID="chkCss" />
                        CSS 
                    </label>
                    <label>
                        <asp:CheckBox runat="server" ID="chkJS" />
                        JS</label>
                    <label class="lblWebservice hide" style="display: none;">
                        <asp:CheckBox runat="server" ID="chkWebService" />
                        Webservice</label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Control Type 
                </td>
                <td>
                    <label>
                        <asp:CheckBox runat="server" ID="chkView" Checked="true" Enabled="false" />View</label>
                    <label>
                        <asp:CheckBox runat="server" ID="chkEdit" />Edit</label>
                    <label>
                        <asp:CheckBox runat="server" ID="chkSetting" />Settings</label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Module Type</td>
                <td>
                    <div class="sfRadiobutton sfRadioTab">
                        <label class="sfActive">
                            <input id="rdbPortal" type="radio" name="types" checked="checked" value="1" style="display: none" />
                            portal</label>
                        <label>
                            <input id="rdbSetting" type="radio" name="types" value="2" style="display: none" />
                            Admin</label>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnAdmin" Value="1" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <h3>Table Settings</h3>
        <table>
            <tr>
                <td>Table Name</td>
                <td>
                    <asp:TextBox ID="txtTablename" runat="server"></asp:TextBox></td>
                <td><span class="sfloader icon-search" style="display: none;"></span><span class="error1 sfError"></span></td>
            </tr>
        </table>

        <table id="divProperties">
            <thead>
                <tr>
                    <td></td>
                    <td>Column Name</td>
                    <td>Data Type</td>
                    <td align="center">Allow Nulls</td>

                </tr>
            </thead>
            <tr>
                <td>
                    <%--<span class="sfDelete deleterow icon-delete"></span>--%>
                    <span class="info icon-password" title="this is the primary key of the table"></span>
                </td>
                <td>
                    <input type="text" class=" Properties sfInputbox" id="properties__1" /></td>
                <td>
                    <div id="autocompleteValue_1"></div>
                    <input type="text" class="sfInputbox dataTypeInput" id="autocomplete_1" /><span class="sfBtn icon-arrow-slim-s" id="btnDrop_1"></span>
                </td>
                <td align="center">
                    <input type="checkbox" class="chkNull" disabled="disabled" /></td>
                <td>
                    <label>
                        <input type="checkbox" class="" id="autoincrement" />
                        Auto Increment
                    </label>
                </td>
            </tr>
        </table>
        <span class="sfBtn icon-addnew sfHighlightBtn" title="Add new Row" id="AddRow">Add Column</span>
        <div class="clearfix"></div>
        <label class="icon-navigate sfBtn next sfFloatRight">
            <input type="button" title="takes to create the sql queries" id="btnCreateSQL" />Ok! Lets Move to SQL procedure</label>
    </div>
</div>

<div class="modulecreator" id="divSqlformation" style="display: none;">
    <div class="sfFormwrapper">
        <ul id="slqProcedures">
        </ul>
        <h1></h1>
        <input type="button" value="I want to change my data" title="change previous data" id="btnBack" class="sfBtn" />
        <input type="button" value="Download Class Zip " title="Download Class Zip" id="btnCreateZipHelp" class="sfBtn" />
        <input type="button" value="Create New Module" title="change previous data" id="btnCreateNewModuleHelp" class="sfBtn" />
        <asp:Button runat="server" ID="btnCreateNewModule" Style="display: none;" OnClick="btnCreateNewModule_Click" CssClass="sfBtn " />
        <asp:Button runat="server" ID="btnCreateZip" Style="display: none;" OnClick="btnCreateZip_Click" CssClass="sfBtn" />
    </div>
</div>

<asp:HiddenField runat="server" ID="hdnTableList" />
<asp:HiddenField runat="server" ID="hdnModuleListFromDatabase" />
<asp:HiddenField runat="server" ID="hdnModuleList" />
<asp:HiddenField runat="server" ID="hdnXML" />
