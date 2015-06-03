<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleEditor.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.ModuleEditor" %>
<%@ Register Src="PackageDetails.ascx" TagName="PackageDetails" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script type="text/javascript">
    $(window).load(function () {
        $('.sfCollapsewrapper div').show();
        $('.ajax__tab_inner').click(function () {
            $('.sfCollapsewrapper div').show();
        });
    });
</script>
<div class="sfButtonwrapper">
    <asp:LinkButton ID="imbUninstall" runat="server" CausesValidation="False" meta:resourcekey="imbUninstallResource1"
        CssClass="icon-uninstall sfBtn" Text="Uninstall Extension" OnClick="imbUninstall_Click" />
    <asp:LinkButton ID="imbCancel" runat="server" CausesValidation="False" CssClass="icon-close sfBtn"
        OnClick="imbCancel_Click" meta:resourcekey="imbCancelResource1" Text="Cancel" />
</div>
<ajax:TabContainer ID="TabContainerManageModules" runat="server" ActiveTabIndex="3"
    meta:resourcekey="TabContainerManageModulesResource1">
    <ajax:TabPanel ID="TabPanelModuleEditor" runat="server" meta:resourcekey="TabPanelModuleEditorResource1">
        <HeaderTemplate>
            Module Settings
        </HeaderTemplate>
        <ContentTemplate>
            <div class="sfCollapsewrapper">
                <h3>Edit Extension</h3>
                <div id="divExtensionSettings" runat="server" class="sfCollapsecontent">
                    <div class="sfFormwrapper sfMargintop">
                        <p class="sfNote">
                            <asp:Label ID="lblExtensionSettingsHelp" runat="server" meta:resourceKey="lblExtensionSettingsHelpResource1"
                                Text="In this section, you can set up more advanced settings for Module Controls on this Module."></asp:Label>
                        </p>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblModuleName" runat="server" CssClass="sfFormlabel" Text="Module Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblModuleNameD" runat="server" CssClass="cssClassFormLabelField" meta:resourceKey="lblModuleNameDResource1"></asp:Label>
                                    <asp:HiddenField ID="hdnModuleName" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFolderName" runat="server" CssClass="sfFormlabel" Text="Folder Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFolderName" runat="server" CssClass="sfInputbox" MaxLength="200"
                                        meta:resourceKey="txtFolderNameResource1" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBusinessControllerClass" runat="server" CssClass="sfFormlabel"
                                        Text="Business Controller Class"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBusinessControllerClass" runat="server" CssClass="sfInputbox"
                                        MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDependencies" runat="server" CssClass="sfFormlabel" Text="Dependencies"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDependencies" runat="server" CssClass="sfInputbox" meta:resourceKey="txtDependenciesResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPermissions" runat="server" CssClass="sfFormlabel" Text="Permissions"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPermissions" runat="server" CssClass="sfInputbox" meta:resourceKey="txtPermissionsResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsPortable" runat="server" CssClass="sfFormlabel" Text="Is Portable?"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsPortable" runat="server" CssClass="sfCheckbox" Enabled="False"
                                        meta:resourceKey="chkIsPortableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsSearchable" runat="server" CssClass="sfFormlabel" meta:resourceKey="lblIsSearchableResource1"
                                        Text="Is Searchable?"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsSearchable" runat="server" Checked="True" CssClass="sfCheckbox"
                                        Enabled="False" meta:resourceKey="chkIsSearchableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsUpgradable" runat="server" CssClass="sfFormlabel" meta:resourceKey="lblIsUpgradableResource1"
                                        Text="Is Upgradable?"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsUpgradable" runat="server" Checked="True" CssClass="sfCheckbox"
                                        Enabled="False" meta:resourceKey="chkIsUpgradableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsPremium" runat="server" CssClass="sfFormlabel" meta:resourceKey="lblIsPremiumResource1"
                                        Text="Is Premium Module?"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsPremium" runat="server" CssClass="sfCheckbox" meta:resourceKey="chkIsPremiumResource1"></asp:CheckBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divPackageSettings" runat="server" style="">
                <uc1:PackageDetails ID="PackageDetails1" runat="server"></uc1:PackageDetails>
            </div>
            <div class="sfButtonwrapper">
                <asp:LinkButton ID="imbUpdate" runat="server" CssClass="icon-update sfBtn" Text="Update Module"
                    meta:resourceKey="imbUpdateResource1" OnClick="imbUpdate_Click"></asp:LinkButton>
                <%-- <asp:Label ID="lblUpdateModule" runat="server" AssociatedControlID="imbUpdate" 
                    meta:resourceKey="lblUpdateModuleResource1" Text="Update Module"></asp:Label>--%>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelDefinitions" runat="server" meta:resourcekey="TabPanelDefinitionsResource1">
        <HeaderTemplate>
            Module Definition Settings
        </HeaderTemplate>
        <ContentTemplate>
            <asp:UpdatePanel ID="udpDefinitions" runat="server" Visible="False">
                <ContentTemplate>
                    <div class="sfCollapsewrapper">
                        <div id="divModuleDefinitions" runat="server" class="sfCollapsecontent">
                            <p class="sfNote">
                                <asp:Label ID="lblModuleDefinitionsHelp" runat="server" Text="In this section, you can set update information for Module Definitions on this Module."
                                    meta:resourcekey="lblModuleDefinitionsHelpResource1"></asp:Label>
                            </p>
                            <div class="sfFormwrapper">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="lblDefinition" runat="server" CssClass="sfFormlabel" Text="Select Definition" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDefinitions" runat="server" AutoPostBack="True" CssClass="sfListmenu"
                                                OnSelectedIndexChanged="ddlDefinitions_SelectedIndexChanged" meta:resourcekey="ddlDefinitionsResource1" />
                                            <asp:HiddenField ID="hdnModuleDefinition" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFriendlyName" runat="server" CssClass="sfFormlabel" Text="Friendly Name" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFriendlyName" ValidationGroup="ModuleDef" runat="server" CssClass="sfInputbox"
                                                MaxLength="200" meta:resourcekey="txtFriendlyNameResource1" />
                                            <asp:Label ID="lblDefinitionError" runat="server" CssClass="NormalRed" Visible="False"
                                                meta:resourcekey="lblDefinitionErrorResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDefaultCacheTime" runat="server" Text="Default Cache Time" CssClass="sfFormlabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDefaultCacheTime" CssClass="sfInputbox" ValidationGroup="ModuleDef"
                                                runat="server" MaxLength="200" meta:resourcekey="txtDefaultCacheTimeResource1" />
                                            <asp:RequiredFieldValidator ID="rfvCacheTime" ValidationGroup="ModuleDef" ControlToValidate="txtDefaultCacheTime"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="sfButtonwrapper">
                        <asp:LinkButton class="CommandButton" Text="Update Definition" ID="imbUpdateDefinition"
                            runat="server" ValidationGroup="ModuleDef" CssClass="icon-update sfBtn" CausesValidation="True"
                            OnClick="imbUpdateDefinition_Click" meta:resourcekey="imbUpdateDefinitionResource1" />
                        <%--<asp:Label ID="lblUpdateDefinition" runat="server" Text="Update Definition" AssociatedControlID="imbUpdateDefinition"
                            meta:resourcekey="lblUpdateDefinitionResource1" />--%>
                        <asp:HiddenField ID="hdfModuleDefID" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelModuleControls" runat="server" meta:resourcekey="TabPanelModuleControlsResource1">
        <HeaderTemplate>
            Module Controls Settings
        </HeaderTemplate>
        <ContentTemplate>
            <div class="sfCollapsewrapper">
                <div id="divModuleControls" runat="server" class="sfCollapsecontent">
                    <p class="sfNote">
                        <asp:Label ID="lblModuleControlsHelp" runat="server" Text="In this section, you can update settings for Module Controls on this Module."
                            meta:resourcekey="lblModuleControlsHelpResource1"></asp:Label>
                    </p>
                    <div class="sfButtonwrapper">
                        <asp:LinkButton class="icon-addnew sfBtn" ID="imbAddControl" Text="Add Module Control"
                            runat="server" CausesValidation="False" OnClick="imbAddControl_Click" meta:resourcekey="imbAddControlResource1" />
                        <%--<asp:Label ID="lblAddControl" runat="server" Text="Add Module Control" AssociatedControlID="imbAddControl"
                            meta:resourcekey="lblAddControlResource1" />--%>
                    </div>
                    <div class="sfGridwrapper">
                        <asp:GridView ID="gdvControls" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="None" BorderWidth="0px" Visible="False" EmptyDataText="No Modulecontrols to Show..."
                            OnRowCommand="gdvControls_RowCommand" OnRowDeleting="gdvControls_RowDeleting"
                            OnRowEditing="gdvControls_RowEditing" meta:resourcekey="gdvControlsResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Control" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlKey" runat="server" Text='<%# Eval("ControlKey") %>' meta:resourcekey="lblControlKeyResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlTitle" runat="server" Text='<%# Eval("ControlTitle") %>'
                                            meta:resourcekey="lblControlTitleResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlSrc" runat="server" Text='<%# Eval("ControlSrc") %>' meta:resourcekey="lblControlSrcResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnModuleDefID" runat="server" Value='<%# Eval("ModuleDefID") %>' />
                                        <asp:LinkButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleControlID") %>'
                                            CommandName="Edit" CssClass="icon-edit" ToolTip="Edit ModuleControl" meta:resourcekey="imbEditResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfEdit" />
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleControlID") %>'
                                            CssClass="icon-delete" CommandName="Delete" ToolTip="Delete ModuleControl" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you delete this Module Control?');" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfDelete" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="sfOdd" />
                            <AlternatingRowStyle CssClass="sfEven" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
</ajax:TabContainer>
<div id="auditBar" runat="server" class="sfAuditbar clearfix sfPadding" visible="false">
    <i class="icon-info" data-title="Information"></i>
    <asp:Label ID="lblCreatedBy" runat="server" Visible="False" meta:resourcekey="lblCreatedByResource1" />
    <asp:Label ID="lblUpdatedBy" runat="server" Visible="False" meta:resourcekey="lblUpdatedByResource1" />
</div>
