<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_Extensions.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.ctl_Extensions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $('#divModulesLists input:checkbox:first').on("change", function () {
            if ($(this).is(":checked")) {
                $('#divModulesLists input:checkbox:not(:disabled)').prop("checked", true);
            }
            else {
                $('#divModulesLists input:checkbox:not(:disabled)').prop("checked", false);
            }
        });
    });
    //]]>
</script>
<h1>
    Modules Management</h1>
<asp:PlaceHolder ID="ExtensionPlaceHolder" runat="server">
    <div class="sfButtonwrapper sfPadding sfHideImg">
        <asp:ImageButton ID="imbInstallModule" runat="server" CausesValidation="False" OnClick="imbInstallModule_Click"
            meta:resourcekey="imbInstallModuleResource1" />
        <asp:Label Style="cursor: pointer;" ID="lblInstallModule" runat="server" Text="Install Module"
            CssClass="icon-install-module sfBtn" AssociatedControlID="imbInstallModule" meta:resourcekey="lblInstallModuleResource1" />
        <asp:ImageButton ID="imbCreateNewModule" runat="server" OnClick="imbCreateNewModule_Click"
            meta:resourcekey="imbCreateNewModuleResource1" />
        <asp:Label ID="lblCreateNewModule" runat="server" Text="Create New Module" AssociatedControlID="imbCreateNewModule"
            CssClass="icon-addnew sfBtn" meta:resourcekey="lblCreateNewModuleResource1" />
        <asp:ImageButton ID="imbAvailableModules" Style="display: none" runat="server" OnClick="imbAvailableModules_Click"
            meta:resourcekey="imbAvailableModulesResource1" />
        <asp:Label Style="display: none" ID="lblAvailableModule" runat="server" Text="Available Module"
            CssClass="icon-addnew sfBtn" AssociatedControlID="imbAvailableModules" meta:resourcekey="lblAvailableModuleResource1" />
        <asp:ImageButton ID="imbCreatePackage" runat="server" OnClick="imbCreatePackage_Click"
            meta:resourcekey="imbCreatePackageResource1" />
        <asp:Label Style="cursor: pointer;" ID="lblCreatePackage" runat="server" Text="Create Package"
            CssClass="icon-addnew sfBtn" AssociatedControlID="imbCreatePackage" meta:resourcekey="lblCreatePackageResource1" />
        <asp:ImageButton ID="imbCreateCompositeModule" runat="server" OnClick="imbCreateCompositeModule_Click"
            meta:resourcekey="imbCreateCompositeModuleResource1" />
        <asp:Label Style="cursor: pointer;" ID="lblCreateCompositeModule" runat="server"
            CssClass="icon-addnew sfBtn" Text="Create Composite Package" AssociatedControlID="imbCreateCompositeModule"
            meta:resourcekey="lblCreateCompositeModuleResource1" />
        <asp:ImageButton ID="imbDownloadModules" runat="server" OnClick="imbDownloadModules_Click"/>
        <asp:Label Style="cursor: pointer;" ID="lblDownloadModules" runat="server" CssClass="icon-download sfBtn"
            Text="Download Modules" AssociatedControlID="imbDownloadModules" />
    </div>
    <div class="sfFormwrapper sfPadding sfTableOption">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td width="100">
                    <asp:Label ID="lblSearchModule" runat="server" CssClass="sfFormlabel" Text="Search Module"
                        meta:resourcekey="lblSearchModuleResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="sfInputbox text sfFloatLeft"
                        meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                    <div class="sfHideImg sfFloatLeft">
                        <asp:ImageButton ID="imgSearch" runat="server" OnClick="imgSearch_Click" ToolTip="Search"
                            meta:resourcekey="imgSearchResource1" />
                        <asp:Label ID="lblSearch" runat="server" Text="" AssociatedControlID="imgSearch"
                            CssClass="icon-search sfBtn" meta:resourcekey="lblSearchResource1"></asp:Label>
                    </div>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td width="80" class="sfTxtAlignRgt">
                    <asp:Label ID="lblSRow" runat="server" Text="Show rows" CssClass="sfFormlabel" meta:resourcekey="lblSRowResource1"></asp:Label>
                </td>
                <td width="50">
                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                        CssClass="sfListmenu sfAuto" meta:resourcekey="ddlRecordsPerPageResource1">
                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource1">10</asp:ListItem>
                        <asp:ListItem Value="25" meta:resourcekey="ListItemResource2">25</asp:ListItem>
                        <asp:ListItem Value="50" meta:resourcekey="ListItemResource3">50</asp:ListItem>
                        <asp:ListItem Value="100" meta:resourcekey="ListItemResource4">100</asp:ListItem>
                        <asp:ListItem Value="150" meta:resourcekey="ListItemResource5">150</asp:ListItem>
                        <asp:ListItem Value="200" meta:resourcekey="ListItemResource6">200</asp:ListItem>
                        <asp:ListItem Value="250" meta:resourcekey="ListItemResource7">250</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="sfHideImg sfTxtAlignRgt" width="150">
                    <asp:ImageButton ID="imbBtnSaveChanges" runat="server" ToolTip="Save changes" OnClick="imbBtnSaveChanges_Click"
                        meta:resourcekey="imbBtnSaveChangesResource1" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to save the changes?');" />
                    <asp:Label ID="lblSaveChanges" runat="server" Text="Save changes" AssociatedControlID="imbBtnSaveChanges"
                        CssClass="icon-save sfBtn" Style="cursor: pointer;" meta:resourcekey="lblSaveChangesResource1"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="sfGridwrapper" id="divModulesLists">
        <asp:GridView ID="gdvExtensions" runat="server" DataKeyNames="ModuleID" AutoGenerateColumns="False"
            GridLines="None" class="sfNotfound" Width="100%" EmptyDataText="No Record to Show..."
            OnRowCommand="gdvExtensions_RowCommand" OnPageIndexChanging="gdvExtensions_PageIndexChanging"
            OnRowDataBound="gdvExtensions_RowDataBound" OnRowDeleting="gdvExtensions_RowDeleting"
            OnRowEditing="gdvExtensions_RowEditing" OnRowUpdating="gdvExtensions_RowUpdating"
            AllowPaging="true" meta:resourcekey="gdvExtensionsResource1">
            <Columns>
                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnModuleID" runat="server" Value='<%# Eval("ModuleID") %>' />
                        <asp:LinkButton ID="lnkName" runat="server" CommandArgument='<%# Eval("ModuleID") %>'
                            CommandName="Edit" Text='<%# Eval("FriendlyName") %>' CssClass="sfFormlabel"
                            meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfColName"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("PackageType") %>' meta:resourcekey="lblTypeResource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfColType"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfColDescription"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version" HeaderStyle-CssClass="sfColversion" meta:resourcekey="TemplateFieldResource4">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>' meta:resourcekey="lblVersionResource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfColversion"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Use" HeaderStyle-CssClass="sfInuse" meta:resourcekey="TemplateFieldResource5">
                    <ItemTemplate>
                        <asp:Label ID="lblInUse" runat="server" Text='<%# ConvertToYesNo(Eval("InUse").ToString()) %>'
                            meta:resourcekey="lblInUseResource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfInuse"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                    <HeaderTemplate>
                        <input id="chkBoxIsActiveHeader" runat="server" class="cssCheckBoxIsActiveHeader"
                            type="checkbox" />
                        <asp:Label ID="lblIsActive" runat="server" Text="Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
                        <asp:HiddenField ID="hdnIsAdmin" runat="server" Value='<%# Eval("IsAdmin") %>' />
                        <input id="chkBoxIsActiveItem" runat="server" type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfIsactive" />
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                    <HeaderTemplate>
                        <asp:Label runat="server" Text="Edit" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%-- <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID") %>'
                         CssClass="sfEdit"   CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                            ToolTip="Edit Module" meta:resourcekey="imbEditResource1" />--%>
                        <asp:LinkButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID") %>'
                            CssClass="icon-edit" CommandName="Edit" ToolTip="Edit Module" meta:resourcekey="imbEditResource1" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfDelete"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                    <HeaderTemplate>
                        <asp:Label runat="server" Text="Delete" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID") %>'
                            CssClass="icon-delete" CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                            ToolTip="Delete Module" meta:resourcekey="imbDeleteResource1" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete this extension?');" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfDelete"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="sfPagination" />
            <RowStyle CssClass="sfOdd" />
            <AlternatingRowStyle CssClass="sfEven" />
        </asp:GridView>
    </div>
</asp:PlaceHolder>
