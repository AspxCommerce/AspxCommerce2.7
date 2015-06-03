<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuManager.ascx.cs" Inherits="Modules_Admin_MenuManager_MenuManager" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(this).MenuManager({
            CultureCode: '<%=CultureCode %>',
            UserModuleID: '<%=UserModuleID %>'
        });
    });
    //]]>	
</script>

<h1>
    Menu Manager</h1>
<div class="sfAdvanceRadioBtn">
    <div id="tabMenu">
        <div id="dvView" class="clearfix">
            <div class="sfCenterdiv">
                <div class="sfCenterWrapper">
                    <div class="sfCenter">
                        <div class="sfQuicklink">
                            <h3 class="clearfix">
                                <span id="lblMenuItm">Choose Menu Item Type &nbsp;</span><i class="icon-info" data-title="This is from where you can include items into your menu"></i>
                            </h3>
                            <div class="sfRadiobutton">
                                <asp:Literal ID="ltrMenuRadioButtons" runat="server"></asp:Literal>
                            </div>
                            <div id="trSubtext">
                                <div class="divSubText" style="display: none">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="75">
                                                <label class="sfFormlabel">
                                                    SubText:</label>
                                            </td>
                                            <td>
                                                <input type="text" id="txtSubText" class="sfInputbox" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <div class="sfCheckbox">
                                                    <label>
                                                        Active:</label>
                                                    <input type="checkbox" id="chkIsActivePage" />
                                                    <label>
                                                        Visible:</label>
                                                    <input type="checkbox" id="chkIsVisiblePage" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="pageMenuitem">
                                <div class="divExternalLink" style="display: none">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Link Title:</label>
                                                <input type="text" class="sfInputbox sfAuto" id="txtLinkTitle" name="txtLinkTitle" />
                                            </td>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Link URL:</label>
                                                <input type="text" id="txtExternalLink" class="sfInputbox sfAuto" name="txtExternalLink" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Caption :</label>
                                                <input type="text" id="txtCaptionExtLink" class="sfInputbox sfAuto" />
                                            </td>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Icon</label>
                                                <input type="file" id="fupIconExtLink" />
                                                <div class="sfUploadedFilesExtLink">
                                                </div>
                                            </td>
                                        </tr>
                                      <tr>
                                      <td colspan="2">
                                      <br />
                                      </td>
                                      </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="sfCheckbox">
                                                    <label class="sfFormlabel">  <input type="checkbox" id="chkLinkActive" />
                                                  
                                                        Active:</label>
                                                    <label class="sfFormlabel">  <input type="checkbox" id="chkLinkVisible" />
                                                  
                                                        Visible:</label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divHtmlContent" style="display: none">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr >
                                            <td width="10%">
                                                <label class="sfFormlabel">
                                                    Link Title</label>
                                                <input type="text" class="sfInputbox sfAuto" id="txtTitleHtmlContent" name="txtTitleHtmlContent" />
                                            </td>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Caption</label>
                                                <input type="text" id="txtCaptionHtmlContent" class="sfInputbox sfAuto" name="txtCaptionHtmlContent" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="sfFormlabel">
                                                    Icon</label>
                                                <input type="file" id="fupIconHtmlContent" />
                                                <div class="sfUploadedFilesHtmlContent">
                                                </div>
                                            </td>
                                            <td colspan="2">
                                                <label class="sfFormlabel">
                                                    Visible</label>
                                                <input type="checkbox" id="chkVisibleHtmlContent" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <textarea cols="50" rows="5" id="txtHtmlContent" class="sfTextarea"> </textarea>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="trPages">
                                <h4 class="clearfix">
                                    <span>Choose Pages &nbsp;</span><i class="icon-info" data-title="List of pages that you can include in the menu "></i>
                                </h4>
                                <div class="sfCheckbox sfMargintop clearfix">
                                    <label><input type="checkbox" class="sfCheckbox" id="chkPageOrder" />
                                    
                                        Preserve Page Order</label>
                                </div>
                                <div id="divPagelist" class="sfPageList">
                                </div>
                            </div>
                            <div class="sfParentItems sfFormwrapper">
                                <label class="sfFormlabel">
                                    Parent Item:</label>
                                <select id="selMenuItem" class="sfListmenu">
                                </select>
                                <div class="sfButtonwrapper sftype1">
                                    <label id="imgAddmenuItem" class="icon-addnew sfBtn">
                                        Add Menu Item</label>
                                    <label id="imgAddMenuCancel" class="icon-close sfBtn">
                                        Cancel</label>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
            <div class="sfLeftdiv">
                <label class="sfAdd icon-addnew" id="btnAddpage">
                    Add Menu</label>
                <div id="menuList">
                </div>
                <div class="sfDivFlying" style="display: none;">
                    <input type="hidden" id="hdnMenuID" />
                    <i class="sfDivFlyingClose icon-close"></i>
                    <input id="txtMenuName" class="sfInputbox sfFullwidth" type="text" />
                    <div class="sfButtonwrapper sftype1" id="divAdd">
                        <label id="imgAdd" class="icon-create sfBtn" style="display: none">
                            Create</label>
                        <label id="imgUpdate" class="icon-update sfBtn">
                            Update</label>
                        <%--  <label id="imgCancel" class="sfCancel">
                            Cancel</label>--%>
                    </div>
                </div>
            </div>
            <div class="sfRightdiv">
                <div class="sfRightDivContainer">
                    <h3>
                        Manage</h3>
                    <div class="sfRadiobutton sfRadioTab">
                        <label class="sfActive">
                            <input id="rdbView" type="radio" name="rdbsetting" checked="checked" value="1" style="display: none" />
                            View</label>
                        <label>
                            <input id="rdbSetting" type="radio" name="rdbsetting" value="2" style="display: none" />
                            Setting</label>
                    </div>
                    <fieldset id="MenuMgrView">
                        <legend>Menu Items: </legend>
                        <div id="divLstMenu">
                        </div>
                    </fieldset>
                    <fieldset id="MenuMgrSetting" style="display: none">
                        <legend><span>Choose Menu Type:</span> </legend>
                        <div class="sfRadiobutton">
                            <label class='sfActive'>
                                <input id="rdbHorizontalMenu" type="radio" name="rdbChooseMenuType" checked="checked"
                                    value="1" style="display: none" />
                                Horizontal</label>
                            <label>
                                <input id="rdbSideMenu" type="radio" name="rdbChooseMenuType" value="2" style="display: none" />
                                Side Menu</label>
                            <label>
                                <input id="rdbFooter" type="radio" name="rdbChooseMenuType" value="3" style="display: none" />
                                Footer Menu</label>
                        </div>
                        <div id="tblTopClientMenu" class="sfRadiobutton sfMargintop">
                            <label style="display: none">
                                <input id="rdbFlyOutMenu" type="radio" name="rdbMenuTypeStyle" value="1" style="display: none" />
                                Flyout Menu</label>
                            <label>
                                <input id="rdbDropdown" type="radio" name="rdbMenuTypeStyle" value="3" style="display: none" />
                                Dropdown</label>
                            <label>
                                <input id="rdbCssMenu" type="radio" name="rdbMenuTypeStyle" value="4" style="display: none" />
                                CSS Menu</label>
                        </div>
                        <div id="tblSideMenu" style="display: none" class="sfRadiobutton sfMargintop">
                            <input id="rdbDynamic" type="radio" name="rdbSideMenuTypeStyle" value="1" />
                            <label>
                                Page Wise Dynamic</label>
                            <input id="rdbCustom" type="radio" checked="checked" name="rdbSideMenuTypeStyle"
                                value="2" />
                            <label>
                                Custom</label>
                        </div>
                        <div id="tblShowImage" class="sfCheckbox sfMargintop">
                            <input id="chkShowImage" value="Show Image" type="checkbox" />
                            <label id="lblShowImage">
                                Show Image</label>
                            <input id="chkShowText" value="Show Text" type="checkbox" />
                            <label id="lblShowText">
                                Show Text</label>
                        </div>
                        <div class="sfCheckboxholder sfMargintop">
                            <input id="chkCaption" value="Caption" type="checkbox" />
                            <label id="lblCaption">
                                Caption</label>
                            <br />
                            <br />
                            <div id="divCaption" visible="false">
                                <label id="lblLevel">
                                    Level</label>
                                <select id="selLevel" class="sfListmenu sfAuto">
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </div>
                        </div>
                        <div class="sfButtonwrapper sftype1">
                            <label id="btnSave" class="sfBtn icon-save">
                                Save Settings</label>
                        </div>
                    </fieldset>
                <%--    <fieldset id="MenuMgrPermissiom" class="sfMenupermission" style="display: none">
                        <legend>
                            <asp:Label ID="lblpermissions" runat="server" Text="Menu Permission Settings"></asp:Label>
                        </legend>
                        <div class="sfButtonwrapper sftype1">
                            <label class="sfAdd" id="imbAddUsers">
                                Add User</label>
                        </div>
                        <div class="divPermissions sfGridwrapper">
                            <table cellspacing="0" cellpadding="0" width="100%">
                            </table>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="dvUser" class="sfGridwrapper">
                            <table id="tblUser" cellspacing="0" cellpadding="0" width="100%">
                            </table>
                        </div>
                        <div class="sfButtonwrapper sftype1">
                            <label class="sfSave" id="imgSavePermission">
                                Save</label>
                        </div>
                    </fieldset>--%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
       <%-- <div id="divAddUsers" title="Search Users" class="sfFormwrapper" style="display: none">
            <p class="sfNote">
                All form fields are required.</p>
            <table cellpadding="0" cellspacing="0" width="0">
                <tr>
                    <td>
                        <input type="text" name="name" id="txtSearchUsers" class="sfInputbox" />
                    </td>
                    <td class="sftype1">
                        <label id="btnSearchUsers" class="sfSearch">
                            Search</label>
                    </td>
                </tr>
            </table>
            <div id="divSearchedUsers">
            </div>
            <div class="sfButtonwrapper sftype1">
                <label id="btnAddUser" class="sfAdd">
                    Add</label>
                <label id="btnCancelUser" class="sfCancel">
                    Cancel</label>
            </div>
        </div>--%>
    </div>
    <div id="myMenu1" class="sfContextmenu sfCurve Shadow">
        <ul>
            <li id="remove">
                <img runat="server" id="imgRemove" alt="Remove" title="Remove" />
                <b>Delete Menu Item</b></li>
        </ul>
    </div>
</div>
