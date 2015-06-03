<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManagePages.ascx.cs" Inherits="Modules_Pages_ManagePages" %>

<script type="text/javascript">
    //<![CDATA[
    var PageMode = false;
    var Extension = '<%=PageExtension %>'
    $(document).ready(function () {
        var LSTSagePages = [];
        $(this).SageTreeBuilder({
            PortalID: '<%=PortalID%>',
            UserModuleID: '<%=UserModuleID%>',
            UserName: '<%=UserName%>',
            PageName: '<%=PageName%>',
            ContainerClientID: '#navContainer',
            CultureCode: '<%=CultureCode%>',
            baseURL: ServicePath + '/Modules/Pages/Services/PagesWebService.asmx/',
            Mode: $('#rdbAdmin').attr('checked'),
            AppName: '<%=appPath%>',
            HostURL: '<%=GetURL%>',
            StartupPage: '<%=StartupPage%>',
            ActiveTemplateName: '<%=ActiveTemplateName %>'
        });
        $(this).SageFramePageBuilder({
            PortalID: '<%=PortalID%>',
            UserModuleID: '<%=UserModuleID%>',
            UserName: '<%=UserName%>',
            PageName: '<%=PageName%>',
            ContainerClientID: '#navContainer',
            CultureCode: '<%=CultureCode%>',
            baseURL: ServicePath + '/Modules/Pages/Services/PagesWebService.asmx/',
            AppName: '<%=appPath%>',
            HostURL: '<%=GetURL%>',
            StartupPage: '<%=StartupPage%>',
            ActiveTemplateName: '<%=ActiveTemplateName %>',
            PageExtension: '<%=PageExtension %>'
        });
        $(this).ModuleManager({
            ShowSideBar: '<%=IsSideBarVisible%>',
            UserModuleID: '<%=UserModuleID%>'
        });
        if ($('#gdvModules table tr').length == 0) {
            $('#gdvModules').html(SageFrame.messaging.showdivmessage("No Page Selected Yet"));
        }
    });
    var parentDiv = "";
    var droppableOrder = "";
    var singlemodule = "";
    var dropStatus = 0;
    var droppableAttr = "";
    var ModuleName = "";
    var ModuleId = "";
    var Scope_Identity = "";
    var uniqueelem = "";
    var UserModuleID = 0;
    var lstModulePermission = [];
    var ModuleDefID = 0;
    var globalpageid = 1;
    var uniquekey = 1;
    //]]>	
</script>

<div class="section-pageWrapper">
</div>
<h1>
    <span>Page Manager</span>
</h1>
<div class="clearfix sfAdvanceRadioBtn">
    <div id="dvPageType">
        <asp:Literal ID="ltrPageRadioButtons" runat="server"></asp:Literal>
    </div>
    <div class="clear">
    </div>
    <%-- <div class="cssClassTreeNav" id="navContainer">
    </div>--%>
    <div class="cssClassDetails">
        <div id="myMenu1" class="sfContextmenu sfCurve Shadow" style="display: none">
            <ul>
                <li id="add">
                    <img runat="server" id="imgAddNew" alt="AddPage" title="AddPage" />
                    <b>Add New Page</b></li>
                <li id="edit">
                    <img runat="server" id="imgEditNew" alt="editPage" title="EditPage" />
                    <b>Edit Page</b> </li>
                <li id="remove">
                    <img runat="server" id="imgRemove" alt="Remove" title="Remove" />
                    <b>Delete Page</b></li>
                <li id="startpage">
                    <img runat="server" id="imgStarterpage" alt="startpage" title="Startuppage" />
                    <b>Set as start page</b></li>
            </ul>
        </div>
        <div class="sfPageModules">
            <div class="section-modulesWrapper">
                <div>
                    <div id="showPopup" style="display: none">
                        <div class="sfPopupinner">
                            <div id="dvTabPanel">
                                <ul>
                                    <li><a href="#dvEdit">
                                        <asp:Label ID="lblEdit" runat="server" Text="Module Basics"></asp:Label>
                                    </a></li>
                                    <li><a href="#dvPermissions">
                                        <asp:Label ID="lblSetting" runat="server" Text="Permissions"></asp:Label>
                                    </a></li>
                                </ul>
                                <div id="dvEdit" class="sfFormwrapper sfModulepopbox">
                                    <input type="hidden" id="hdnModuleInPage" />
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Module Name
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <label id="lblmoduleName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Module Title
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <input type="text" id="txtModuleTitle" class="sfInputbox" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Pane Name
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <label id="spnPaneName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr id="trheader">
                                            <td>
                                                <label class="sfFormlabel">
                                                    Header Text
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <input type="text" id="txtHeaderTxt" class="sfInputbox" maxlength="50" />
                                                <br />
                                                <input type="checkbox" id="chkShowHeader" class="sfCheckbox" />
                                                <label class="sfFormlabelsmall">
                                                    Show Header
                                                </label>
                                            </td>
                                        </tr>
                                        <tr id="trSuffix">
                                            <td>
                                                <label class="sfFormlabel">
                                                    Module Suffix Class
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <input type="text" id="txtModuleSuffix" class="sfInputbox" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    IsActive</label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <input type="checkbox" id="chkIsActive" checked="checked" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Show in Other Pages
                                                </label>
                                            </td>
                                            <td width="30">:
                                            </td>
                                            <td>
                                                <input type="checkbox" value="All" id="rbAllPages" name="showPagesGroup" checked="checked" />
                                                <label id="lblAll">
                                                    All</label>
                                                <input type="checkbox" value="Custom" id="rbCustomPages" name="showPagesGroup" />
                                                <label>
                                                    Customize</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="32">
                                                <div id="trPages">
                                                    <div id="pageList_Apply" class="sfGridwrapper">
                                                        <div class="sfLcontent">
                                                            <ul class="alt_content" id="pageTree_popup">
                                                            </ul>
                                                            <ul id="hdnPageList" style="display: none">
                                                            </ul>
                                                        </div>
                                                        <div class="sfPagination" id="pagePagination">
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <input type="checkbox" class="sfCheckbox" id="chkInheritPermissions" />
                                                <label id="lblInherit" class="sfFormlabel">
                                                    Inherit Permissions From Page</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <input type="checkbox" id="chkDonotshow" class="sfCheckbox" />
                                                <label id="lblDonotshow" class="sfFormlabel">
                                                    Do not show this popup again</label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dvPermissions">
                                    <%-- <div>
                                        <div class="sfPopupsearch clearfix">
                                            <label class="sfFormlabel">
                                                Search User:
                                            </label>
                                            <input type="text" class="sfInputbox searchinput" id="txtSearchUser" />
                                            <div class="sfButtonwrapper sftype1 sfInline">
                                                <label id="btnSearchUser" class="icon-search">
                                                    Search</label>
                                            </div>
                                        </div>--%>

                                    <div class="divPermissions sfGridwrapper">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <th>
                                                    <label>
                                                        Role</label>
                                                </th>
                                                <th>
                                                    <label>
                                                        View</label>
                                                </th>
                                                <th>
                                                    <label>
                                                        Edit</label>
                                                </th>
                                                <th></th>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div id="divModuleUser" class="sfGridwrapper">
                                        <table id="tblModuleUser" width="100%" cellpadding="0" cellspacing="0">
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="sfButtonwrapper sftype1 sfInline">
                                <label id="spnBtnSave" class="icon-save sfBtn">
                                    Save</label>
                                <label id="spnBtnCancel" class="icon-close sfBtn">
                                    Cancel</label>
                            </div>
                        </div>
                    </div>
                </div>
                <iframe id="divFrame" style="display: none" src='' width='100%'></iframe>
            </div>
        </div>
    </div>
    <div class="sfPageModuleManager">
        <div id="divLayout">
            <div class="divLayoutContainer">
                <h3>Page Module Manager</h3>
                <div class="sfFormwrapper sfPadding sfMarginbtn sfModuletopwrapper" id="PageHeader">
                    <div class="publishbar">
                        <span id="btnPreview" value="Preview" class="icon-page-preview">Preview</span> <span
                            id="btnpublish" name="true" value="Publish" class="icon-unchecked">Publish</span>
                    </div>
                    <div class="sfLayoutSettingHolder clearfix">
                        <div class="ChangeLayout">
                            <label>
                                Assign layout option:</label>
                            <asp:Literal runat="server" ID="ltrLayouts"></asp:Literal>
                            <input type="button" value="Preserve Layout" id="btnSavelayout" class="sfBtn"
                                style="display: none;" />
                            <input type="button" value="Cancel" id="btnLayoutCancel" class="sfBtn" style="display: none;" />
                        </div>
                        <div id="layoutSwitch" class="sfLayoutSwitch">
                            <span id="imgMobileSwitch" class="active icon-pc"></span>
                            <span id="imgPCSwitch" class="icon-handheld"></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div id="divLayoutWireframe">
                </div>
            </div>
            <div class="sfNewPage" style="display: none;">
                <div id="divPageDetails">
                    <h3>Page Details <span class='sfPageDetailCancel icon-close'></span>
                    </h3>
                    <div id="dvPageSetting" class="cssClassTabPabelTabel">
                        <div id="tblPageDetails">
                            <div class="sfFormwrapper">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="50%" class="sfVtop">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="25%">
                                                        <span class="sfFormlabel">Page Name</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" id="txtPageName" name="txtPageName" class="sfInputbox" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="25%">
                                                        <span class="sfFormlabel">Caption</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" id="txtCaption" name="txtCaption" maxlength="100" class="sfInputbox" />
                                                    </td>
                                                </tr>
                                                <tr id="trParent">
                                                    <td>
                                                        <span class="sfFormlabel">Parent Page</span>
                                                    </td>
                                                    <td>
                                                        <select id="cboParentPage" class="sfListmenu">
                                                            <option value="0">-- Select One--</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="sfFormlabel">Refresh Interval</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" class="sfInputbox" id="txtRefreshInterval" />
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>
                                                        <span class="sfFormlabel">Start Date</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" class="sfInputbox" id="txtStartDate" />
                                                        <label id="lblError" class="sfError">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>
                                                        <span class="sfFormlabel">End Date</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" class="sfInputbox" id="txtEndDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="sfFormlabel">Icon</span>
                                                    </td>
                                                    <td>
                                                        <input type="file" id="flIcon" />
                                                        <div class="cssClassUploadFiles">
                                                            <ul>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="trShowInDashboard">
                                                    <td>
                                                        <span class="sfFormlabel">Show In Dashboard</span>
                                                    </td>
                                                    <td>
                                                        <input type="checkbox" id="chkShowInDashboard" />
                                                    </td>
                                                </tr>
                                                <tr id="trIncludeInMenuLbl">
                                                    <td>
                                                        <span class="sfFormlabel">Include In Menu?</span>
                                                    </td>
                                                    <td>
                                                        <input type="checkbox" id="chkMenu" />
                                                    </td>
                                                </tr>
                                                <tr id="trIncludeInMenu">
                                                    <td>
                                                        <label class="sfFormlabel" id="lblSelectMenu">
                                                            Select Menu
                                                        </label>
                                                    </td>
                                                    <td colspan="3">
                                                        <select multiple="multiple" id="selMenulist" class="sfListmenubig">
                                                            <option value="1">Top Menu</option>
                                                            <option value="2">Footer Menu</option>
                                                            <option value="3">Side Menu</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="50%" class="sfVtop">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <span class="sfFormlabel">Page Title</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" id="txtTitle" name="txtTitle" class="sfInputbox" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="sfFormlabel">Description</span>
                                                    </td>
                                                    <td>
                                                        <textarea rows="30" cols="15" id="txtDescription" class="sfTextarea"></textarea>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="sfFormlabel">Keywords</span>
                                                    </td>
                                                    <td>
                                                        <textarea rows="30" cols="15" id="txtKeyWords" class="sfTextarea" maxlength="500"></textarea>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix sfTableOption">
                                <h3 class="sfFloatLeft">Page Permission Settings
                                </h3>
                            </div>
                            <div class="sfFormwrapper clearfix">
                                <div class="divPermission sfGridwrapper">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                                <div id="dvUser" class="sfGridwrapper">
                                    <table id="tblUser" cellspacing="0" cellpadding="0" width="100%">
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="sfButtonwrapper sftype1">
                            <label id="btnSubmit" class="icon-save sfBtn">
                                Save</label>
                            <label class="icon-close sfBtn" id="imbPageCancel">
                                Cancel</label>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div id="dvModule" class="cssClassTabPabelTabel" style="display: none">
                        <div class="sfButtonwrapper sfMarginnone">
                            <a id="btnManageModules" href="#" class="sfBtn">Module Manager</a>
                        </div>
                        <div id="gdvModules" class="sfGridwrapper">
                        </div>
                        <div id="hdnModules" style="display: none">
                        </div>
                        <div id="divPager" class="sfPagination clearfix">
                        </div>
                    </div>
                </div>
                <div id="divAddUsers" class="sfFormwrapper" title="Add Users">
                    <p class="sfNote">
                        <i class="icon-info"></i>First search for users, select the user from search results
                            and finally add the user.
                    </p>
                    <table cellpadding="0" cellspacing="0" width="0">
                        <tr>
                            <td>
                                <input type="text" name="name" id="txtSearchUsers" class="sfInputbox" />
                            </td>
                            <td class="sftype1">
                                <label id="btnSearchUsers" class="icon-search">
                                    Search</label>
                            </td>
                        </tr>
                    </table>
                    <div id="divSearchedUsers">
                    </div>
                    <div class="sfButtonwrapper sftype1">
                        <label id="btnAddUser" class="icon-addnew sfBtn">
                            Add</label>
                        <label id="btnCancelUser" class="icon-close sfBtn">
                            Cancel</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="cssClassTreeNav" id="navContainer">
    </div>
    <%-- <div id="wrapper" class="ModuleList">--%>
    <div id="divDroppable" class="sfFormwrapper sfNone sfModulesHolder">
        <h3>Modules
                <div id="divSearch" class="sfSearch">
                    <input id="txtSearchModules" type="text" class="sfInputbox" style="width: 20px" />
                    <span class="icon-search sfSearchModules"></span>
                </div>
        </h3>
        <asp:Literal runat="server" ID="ltrAdminModules"></asp:Literal>
        <div id="divFloat" class="clearfix">
            <%--<span id="spnFix" class="sfFloatPin">
                    <img alt="" src="<%=appPath%>/Administrator/Templates/Default/images/pin.png" /></span>--%>
            <%--  <img alt="sort" class="sfSorting sfSortdown" src="<%=appPath%>/Administrator/Templates/Default/images/sort.png"
                    id="imgSort" />--%>
            <i id="imgSort" class="icon-ascending-order"></i>
            <div class="clear">
            </div>
            <div class="sfLcontent">
                <ul class="alt_content">
                </ul>
                <ul class="hdnModulelist" style="display: none">
                </ul>
            </div>
            <div id="Pagination" class="sfPagination">
            </div>
        </div>
    </div>
    <%--</div>--%>
</div>
</div>
