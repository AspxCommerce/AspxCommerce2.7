<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguageManager.ascx.cs"
    Inherits="Localization_Language" %>
<%--    <%@ Reference VirtualPath="~/Modules/Language/LanguageSetUp.ascx" %>    
--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modules/Language/CreateLanguagePack.ascx" TagName="CreateLanguagePack"
    TagPrefix="uc1" %>
<%@ Register Src="~/Modules/Language/LanguagePackInstaller.ascx" TagName="LanguagePackInstaller"
    TagPrefix="uc2" %>
<%@ Register Src="~/Modules/Language/LanguageSetUp.ascx" TagName="LanguageSetUp"
    TagPrefix="uc3" %>
<%@ Register Src="~/Modules/Language/TimeZoneEditor.ascx" TagName="TimeZoneEditor"
    TagPrefix="uc4" %>
<%@ Register Src="~/Modules/Language/LocalPage.ascx" TagName="MenuEditor" TagPrefix="uc5" %>
<%@ Register Src="~/Modules/Language/LocalModuleTitle.ascx" TagName="ModuleTitleEditor"
    TagPrefix="uc6" %>

<script type="text/javascript">
    //<![CDATA[
    $.Localization = {
        TextAreaID: 0,
        FilePath: "",
        ID: 0,
        GridID: '<%=gdvResxKeyValue.ClientID%>'
    };
    $(function() {
        $(document).on('click', "#" + $.Localization.GridID + ' .sfEdit', function(e) {
		 $('#txtResxValueEditor').ckeditor("config");
            var index = $(this).attr("value");
            $.Localization.ID = index;
            var data = $('#' + $.Localization.GridID + ' textarea[title=' + index + ']').val();
            $('#txtResxValueEditor').val(data);
            ShowPopUp("editorDiv");
            e.preventDefault();
        });
       
        BindEvents();
    });
    function BindEvents() {
        $('#fade').click(function() {
            $('#fade,#editorDiv,#translatorDiv,#divMessagePopUp,#divConfirmPopUp').fadeOut();
        });
        $('#btnCloseFB').bind("click", function() {
            var id = $.Localization.ID;
            $('#' + $.Localization.GridID + ' textarea[title="' + id + '"]').val($('#txtResxValueEditor').val());
            $('#editorDiv').dialog("close");
        });
        $('.closePopUp').bind("click", function() {
            $('#fade,#editorDiv,#translatorDiv,#divMessagePopUp,#divConfirmPopUp').fadeOut();
        });
        $('#btnSave').bind("click", function() {
            var id = $.Localization.ID;
            $('#' + $.Localization.GridID + ' textarea[title="' + id + '"]').val($('#translatedTxt').val());
            $('#translatorDiv,#fade').fadeOut();
        });
    }

    function ShowPopUp(popupid) {

        var options = {
            modal: true,
            title: "Edit Language",
            height: 500,
            width: 600,
            dialogClass: "sfFormwrapper"
        };
        dlg = $('#' + popupid).dialog(options);
        dlg.parent().appendTo($('form:first'));
    }
    //]]>	
       
</script>

<div id="divActivityIndicator">
</div>
<div id="fade">
</div>
<asp:HiddenField ID="hdntemp" Value="" runat="server" />
<asp:HyperLink ID="hlRefresh" runat="server" NavigateUrl="~/SageFrame/Admin/Localization.aspx"
    Visible="false">There has been few action on the website's culture so ,Please click here to refresh the page.</asp:HyperLink>
<div id="langEditFirstDiv" runat="server">
    <h1>
        <asp:Label ID="lblTimeZoneEditor" runat="server" Text="Language Manager" meta:resourcekey="lblTimeZoneEditorResource1"></asp:Label>
    </h1>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="10%">
                <asp:Label ID="lblSysDefault" runat="server" Text="System Default" CssClass="sfFormlabel"
                    meta:resourcekey="lblSysDefaultResource1"></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgFlagSystemDefault" runat="server" meta:resourcekey="imgFlagSystemDefaultResource2" />
                <asp:Label ID="lblSystemDefault" runat="server" meta:resourcekey="lblSystemDefaultResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentCultureLbl" runat="server" Text="Current Culture" CssClass="sfFormlabel"
                    meta:resourcekey="lblCurrentCultureLblResource1"></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgFlagCurrentCulture" runat="server" meta:resourcekey="imgFlagCurrentCultureResource1" />
                <asp:Label ID="lblCurrentCulture" runat="server" meta:resourcekey="lblSiteDefaultResource1"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="sfButtonwrapper">
        <label class="sfLocale icon-addnew sfBtn">
            Add Language
            <asp:Button ID="imbAddLanguage" runat="server" OnClick="imbAddLanguage_Click" meta:resourcekey="imbAddLanguageResource1" />
        </label>
        <label class="icon-install-module sfBtn sfLocale">
            Install Language Pack
            <asp:Button ID="imbInstallLang" runat="server" OnClick="imbInstallLang_Click" meta:resourcekey="imbInstallLangResource1" />
        </label>
        <label class="icon-package sfBtn sfLocale">
            Create Language Pack
            <asp:Button ID="imbCreateLangPack" runat="server" OnClick="imbCreateLangPack_Click"
                meta:resourcekey="imbCreateLangPackResource1" />
        </label>
        <label class="icon-time sfBtn sfLocale" time zone editor>
            Time Zone Editor
            <asp:Button ID="imbEditTimeZone" runat="server" OnClick="imbEditTimeZone_Click" meta:resourcekey="imbEditTimeZoneResource1" />
        </label>
        <label class="icon-menu sfBtn sfLocale">
            Localize Menu
            <asp:Button ID="imbLocalizeMenu" OnClick="imbLocalizeMenu_Click" runat="server" meta:resourcekey="imbEditTimeZoneResource1"
                Style="width: 14px;" />
        </label>
        <label class="icon-menu sfBtn sfLocale">
            Localize Module Title
            <asp:Button ID="imbLocalizeModuleTitle" OnClick="imbLocalizeModuleTitle_Click" runat="server"
                Style="width: 14px;" /></label>
    </div>
    <div class="sfFormwrapper sfPadding">
        <asp:HiddenField ID="hdnCultureCode" runat="server" Value="en-US" />
        <div class="sfTableOption clearfix">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <p class="sfNote">
                            <i class="icon-info"></i>The default site language cannot be disabled</p>
                    </td>
                    <td class="sfTxtAlignRgt">
                        <asp:Label ID="lblPageSize" runat="server" Text="Show Rows:" CssClass="sfFormlabel"
                            meta:resourcekey="lblPageSizeResource1"></asp:Label>
                    </td>
                    <td width="50px;" class="sfTxtAlignRgt">
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="sfListmenu sfAuto" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged1" meta:resourcekey="ddlPageSizeResource1">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="sfGridwrapper sfPadding">
        <asp:GridView ID="gdvLangList" runat="server" GridLines="None" AllowPaging="True"
            Width="100%" AutoGenerateColumns="False" OnRowCommand="gdvLangList_RowCommand"
            OnRowDataBound="gdvLangList_RowDataBound" meta:resourcekey="gdvLangListResource1"
            OnPageIndexChanging="gdvLangList_PageIndexChanging" DataKeyNames="LanguageID">
            <Columns>
                <asp:TemplateField HeaderText="Language" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:Image ID="imgFlag" runat="server" meta:resourcekey="imgFlagResource2" />
                        <span class="cssClassLangName">
                            <asp:Label ID="lblLanguageName" runat="server" Text='<%# Eval("LanguageN") %>' meta:resourcekey="lblLanguageNameResource1"></asp:Label>
                        </span><span class="cssClassCountry">(
                            <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("Country") %>' meta:resourcekey="lblCountryNameResource1"></asp:Label>
                            ) </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LanguageCode" HeaderText="Code" meta:resourcekey="BoundFieldResource1">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Enabled" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsEnabled" runat="server" meta:resourcekey="chkIsEnabledResource2"
                            OnCheckedChanged="chkIsEnabled_CheckedChanged" AutoPostBack="True" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="sfEnable" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnLanguageDel" runat="server" CssClass="icon-edit" CommandName="EditResources"
                            CommandArgument='<%# Container.DataItemIndex %>' meta:resourcekey="btnLanguageDelResource1" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="sfEdit" />
                </asp:TemplateField>
                <asp:BoundField DataField="LanguageID" meta:resourcekey="BoundFieldResource2" />
                <asp:TemplateField HeaderText="Delete" meta:resourcekey="TemplateFieldResource4">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnLanguageDelete" runat="server" CssClass="icon-delete" CommandName="DeleteResources"
                            OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete the language?');"
                            CommandArgument='<%# Container.DataItemIndex %>' meta:resourcekey="btnLanguageDelResource1" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="sfDelete" />
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="sfEven" />
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <PagerStyle CssClass="sfPagination" />
            <RowStyle CssClass="sfOdd" />
        </asp:GridView>
    </div>
</div>
<div id="langEditSecondDiv" runat="server" class="sfLanguage">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>
                <asp:Label ID="lblLanguageResourceEditor" runat="server" Text="Language Resource Editor"
                    meta:resourcekey="lblLanguageResourceEditorResource1"></asp:Label>
            </h1>
            <div class="sfFormwrapper sfPadding">
                <div class="sfTreeview">
                    <asp:TreeView ID="tvList" ShowLines="True" runat="server" ImageSet="Msdn" OnSelectedNodeChanged="tvList_SelectedNodeChanged"
                        meta:resourcekey="tvListResource1">
                        <SelectedNodeStyle CssClass="sfSelectednode" />
                    </asp:TreeView>
                </div>
                <div class="sflanguagecontent" id="languageContent" runat="server">
                    <div class="sfLanguageinfo">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="25%">
                                    <label class="sfFormlabel">
                                        Selected Language</label>
                                </td>
                                <td>
                                    <asp:Image ID="imgSelectedLang" runat="server" meta:resourcekey="imgSelectedLangResource1" />
                                    <asp:Label ID="lblSelectedLanguage" runat="server" meta:resourcekey="lblSelectedLanguageResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Selected Folder</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSelectedFolder" runat="server" meta:resourcekey="lblSelectedFolderResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Resource File</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSelectedFile" runat="server" meta:resourcekey="lblSelectedFileResource1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    <div class="sfButtonWrapper sfTableOption clearfix">
                     <label class="sfLocale icon-delete sfBtn sfFloatRight">
                        Delete File
                        <asp:ImageButton ID="imbDeleteResxFile" runat="server" OnClick="imbDeleteResxFile_Click"
                            meta:resourcekey="imbDeleteResxFileResource1" />
                    </label>
                    <label class="sfLocale icon-save sfBtn sfFloatRight">
                        Save
                        <asp:Button ID="imbUpdate" runat="server" OnClick="imbUpdate_Click" meta:resourcekey="imbUpdateResource1" />
                    </label>                   
                   
                    </div>
                    <div class="sfGridwrapper">
                        <asp:GridView ID="gdvResxKeyValue" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="gdvResxKeyValue_RowDataBound" meta:resourcekey="gdvResxKeyValueResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Default Values" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <ul id="keyvalue" runat="server">
                                            <li>
                                                <asp:Label runat="server" ID="lblKey" Text='<%# Eval("Key") %>' meta:resourcekey="lblKeyResource1"></asp:Label>
                                            </li>
                                            <li id="defaultvalue" class="sfResxvalue" runat="server">
                                                <%#Eval("DefaultValue")%></li>
                                        </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Local Values" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtResxValue" ToolTip="<%# Container.DataItemIndex+1 %>" runat="server"
                                            TextMode="MultiLine" CssClass="sfTextarea" Text='<%# Eval("Value") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                    <ItemTemplate>
                                        <label class="icon-edit">
                                            <asp:Button ID="imgEditResxValue" CssClass="sfEdit icon-edit" Text="<%# Container.DataItemIndex+1 %>"
                                                runat="server" meta:resourcekey="imgEditResxValueResource1" />
                                        </label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="sfEven" />
                            <RowStyle CssClass="sfOdd" />
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblScriptRunner" runat="server"></asp:Label>
                </div>
                <div class="sfButtonwrapper" id="controlButtons" runat="server">
                <label class="sfLocale icon-arrow-slimdouble-w sfBtn" style="margin-left:20px;">
                        Back
                        <asp:Button ID="imbCancel" runat="server" OnClick="imbCancel_Click" meta:resourcekey="imbCancelResource1" />
                    </label>
                    
                </div>
                <div class="clear">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imbCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="editorDiv" style="display: none">
        <textarea id="txtResxValueEditor"></textarea>
        <div class="sfButtonwrapper sftype1">
            <label id="btnCloseFB" class="icon-save sfBtn">
                Save</label>
        </div>
    </div>
</div>
<uc3:LanguageSetUp runat="server" ID="ctrl_LanguagePackSetup" />
<uc1:CreateLanguagePack runat="server" ID="CreateLanguagePack1" />
<uc4:TimeZoneEditor ID="ctrl_TimeZoneEditor" runat="server" />
<uc5:MenuEditor ID="ctrl_MenuEditor" runat="server" />
<uc6:ModuleTitleEditor ID="ctrl_ModuleTitleEditor" runat="server" />
<br />
<uc2:LanguagePackInstaller runat="server" ID="LanguagePackInstaller1" />
