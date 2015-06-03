<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributesSetManage.ascx.cs"
    Inherits="Modules_AspxAttributesManagement_AttributesSetManage" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxAttributesManagement
        });
    });
    var lblAttributeSetInfo = "<%= lblAttributeSetInfo.ClientID %>";
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<!-- Grid -->
<div id="divAttribSetGrid">
    <div class="cssClassBodyContentBox">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h1>
                    <asp:Label ID="lblAttrSetsGridHeading" runat="server" Text="Manage Attribute Sets"
                        meta:resourcekey="lblAttrSetsGridHeadingResource1"></asp:Label>
                </h1>
                <div class="cssClassHeaderRight">
                    <div class="sfButtonwrapper">
                        <p>
                            <button type="button" id="btnAddNewSet" class="sfBtn">
                                <span class="sfLocale icon-addnew">Add New Set</span></button>
                        </p>
                        <div class="cssClassClear">
                        </div>
                    </div>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="sfGridwrapper">
                <div class="sfGridWrapperContent">
                    <div class="sfFormwrapper sfTableOption">
                        <table border="0" cellspacing="0" cellpadding="0" >
                            <tr>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        Attribute Set Name:</label>
                                    <input type="text" id="txtSearchAttributeSetName" class="sfTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        Active:</label>
                                    <select id="ddlIsActive" class="sfListmenu">
                                        <option value="" class="sfLocale">-- All -- </option>
                                        <option value="True" class="sfLocale">Yes </option>
                                        <option value="False" class="sfLocale">No </option>
                                    </select>
                                </td>
                                <td>
                                    <label class="cssClassLabel sfLocale">
                                        In System:</label>
                                    <select id="ddlUserInSystem" class="sfListmenu">
                                        <option value="" class="sfLocale">--All--</option>
                                        <option value="True" class="sfLocale">Yes</option>
                                        <option value="False" class="sfLocale">No</option>
                                    </select>
                                </td>
                                <td>
                                            <button type="button" onclick="AttributeSetManage.SearchAttributeSetName()" class="sfBtn">
                                                <span class="sfLocale icon-search">Search</span></button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="loading">
                        <img id="ajaxAttributeSetMgmtImage" src="" class="sfLocale" alt="loading...." title="loadin...." />
                    </div>
                    <div class="log">
                    </div>
                    <table id="gdvAttributeSet" width="100%" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
        </div>
</div>
<!-- End of Grid -->
<div id="divAttribSetAddForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttrSetsFormHeading" runat="server" Text="Add New Attribute Set"
                    meta:resourcekey="lblAttrSetsFormHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cssClassPadding tdpadding">
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblAttributeSetName" runat="server" Text="Name:" CssClass="cssClassLabel"
                            meta:resourcekey="lblAttributeSetNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" class="sfInputbox" name="" id="txtAttributeSetName" />
                        <span class="cssClassRequired sfLocale">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblType" runat="server" Text="Based On:" CssClass="cssClassLabel"
                            meta:resourcekey="lblTypeResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select class="sfListmenu" name="" id="ddlAttributeSet">
                            <span class="cssClassRequired sfLocale">*</span>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button id="btnBackAdd" type="button" class="sfBtn" >
                    <span class="sfLocale icon-arrow-slim-w">Back</span>
                </button>
            </p>
            <p>
                <button id="btnSaveAttributeSet" type="button" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<div id="divAttribSetEditForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttributeSetInfo" runat="server" meta:resourcekey="lblAttributeSetInfoResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td width="35%" style="vertical-align:top;">
                        <h3>
                            <asp:Label ID="lblAttributeNameTitle" runat="server" Text="Edit Set Name" CssClass="cssClassLabel"
                                meta:resourcekey="lblAttributeNameTitleResource1"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAttributeSetNameTitle" runat="server" Text="Name:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblAttributeSetNameTitleResource1"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="text" class="sfInputbox" name="" id="txtOldAttributeSetName" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="65%" style="vertical-align:top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="vertical-align:top;">
                                    <h3>
                                        <asp:Label ID="lblGroups" runat="server" Text="Groups" CssClass="cssClassLabel" meta:resourcekey="lblGroupsResource1"></asp:Label>
                                    </h3>
                                </td>
                                <td style="vertical-align:top;">
                                    <div class="cssClassPaddingNone">
                                        <p>
                                            <button type="button" id="btnAddNewGroup" class="sfBtn">
                                                <span class="sfLocale icon-addnew">Add New Group</span></button>
                                            <button type="button" id="btnCollapse" class="sfBtn">
                                                <span class="sfLocale icon-collapse">Collapse All</span></button>
                                            <button type="button" id="btnExpand" class="sfBtn">
                                                <span class="sfLocale icon-expand">Expand All</span></button>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="contextMenu" id="myMenu1">
                                        <ul>
                                            <li id="rename" class="cssClassSeparator">
                                                <img runat="server" id="imgRename" class="sfLocale" src="" alt="Rename" title="Rename" />
                                                <b>Rename</b></li>
                                            <li id="delete" class="cssClassSeparator">
                                                <img runat="server" id="imgDelete" class="sfLocale" src="" alt="Delete" title="Delete" />
                                                <b>Delete</b></li>
                                            <li id="remove" class="cssClassSeparator">
                                                <img runat="server" id="imgRemove" class="sfLocale" src="" alt="Remove" title="Remove" />
                                                <b>Remove</b></li>
                                        </ul>
                                    </div>
                                    <div id="dvTree" style="float: left;">
                                    </div>
                                    <%--<div>
                                                                                                            <a href="#" id="aCollapse" onclick="$('#tree').tree('closeNode', $('#tree').find('li'));">
                                                                                                                Collapse all</a> | <a href="#" id="aExpand" onclick="$('#tree').tree('openNode', $('#tree').find('li'));">
                                                                                                                    Expand all</a>
                                                                                                        </div>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button id="btnBackEdit" type="button" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span>
                </button>
            </p>
            <p>
                <button class="btnResetEdit sfBtn" type="button">
                    <span class="sfLocale icon-refresh">Reset</span>
                </button>
            </p>
            <p>
                <button class="btnDeleteAttributeSet sfBtn" type="button" style="display:none;">
                    <span class="sfLocale icon-delete">Delete</span>
                </button>
            </p>
            <p>
                <button class="btnUpdateAttributeSet sfBtn" type="button">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel5">
    <div class="cssPopUpBody">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale">
                    <i class="i-close"></i>Close</span></button>
        </div>
        <h2>
            <label id="lblGroupName" class="cssGroupName sfLocale">
               Please Enter Group Name</label>
        </h2>       
         <div class="cssClassShareList clearfix">
             <label id="lblGroup" class="sfLocale">Group Name</label>
             <input type="text" id="txtGroupName" name="GroupName" class="required" />
             <div class="sfButtonwrapper">            
            <p>
                <button id="btnSaveGroupName" class="sfBtn" type="button">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
         </div>                       
</div>
</div>
