<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributesManage.ascx.cs"
    Inherits="Modules_AspxAttributesManagement_AttributesManage" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxAttributesManagement
        });
    });
    var lblAttrFormHeading = "<%= lblAttrFormHeading.ClientID %>";
    var lblLength = "<%= lblLength.ClientID %>";
    var lblDefaultValue = "<%= lblDefaultValue.ClientID %>";
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<!-- Grid -->
<div id="divAttribGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttrGridHeading" runat="server" Text="Manage Attributes" meta:resourcekey="lblAttrGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                     <p>
                        <button type="button" id="btnAddNew" class="sfBtn">
                           <span class="sfLocale icon-addnew">Add New Attribute</span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                           <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>
                   
                    <div class="cssClassClear">
                    </div>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Attribute Name:</label>
                                <input type="text" id="txtSearchAttributeName" class="sfTextBoxSmall" />
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Required:</label>
                                <select id="ddlIsRequired" class="sfListmenu" style="width:150px;">
                                    <option value="" class="sfLocale">-- All --</option>
                                    <option value="True" class="sfLocale">Yes</option>
                                    <option value="False" class="sfLocale">No</option>
                                </select>
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Comparable:</label><br />
<select id="ddlComparable" class="sfListmenu">
                                        <option value="" class="sfLocale">-- All --</option>
                                        <option value="True" class="sfLocale">Yes</option>
                                        <option value="False" class="sfLocale">No</option>
                                    </select>
                            </td>
                            <td width="160">
                                <label class="cssClassLabel sfLocale">
                                    System:</label>
                                <select id="ddlIsSystem" class="sfListmenu">
                                    <option value="" class="sfLocale">-- All --</option>
                                    <option value="True" class="sfLocale">Yes</option>
                                    <option value="False" class="sfLocale">No</option>
                                </select>
                            </td>
                            <td><br />
                                        <button type="button" id="btnSearchAttribute" class="sfBtn">
                                           <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxAttributeImageLoader" src="" class="sfLocale" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvAttributes" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<!-- form -->
<div id="divAttribForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttrFormHeading" runat="server" Text="General Information" meta:resourcekey="lblAttrFormHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Attribute Properties" meta:resourcekey="lblTabTitle1Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Frontend Properties" meta:resourcekey="lblTabTitle2Resource1"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                    <div class="sfFormwrapper">
                         <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                        <h3>
                            <asp:Label ID="lblTab1Info" runat="server" Text="General Information" meta:resourcekey="lblTab1InfoResource1"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAttributeName" runat="server" Text="Attribute Name:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblAttributeNameResource1"></asp:Label>
                                
                                </td>
                                <td class="cssClassTableRightCol required">
                                    <input type="text" id="txtAttributeName" class="sfInputbox" />
                                    <span class="cssClassRight">
                                        <img class="cssClassSuccessImg sfLocale" height="13" width="18" title="Right" src="" alt="Right" /></span>
                                    <b class="cssClassError sfLocale">Ops! found something error, must be unique with no spaces</b>    <span class="cssClassRequired">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text="Type:" CssClass="cssClassLabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="ddlAttributeType" class="sfListmenu" title="Attribute Input Type">
                                    </select>
                                </td>
                            </tr>
                            <tr id="trdefaultValue">
                                <td>
                                    <asp:Label ID="lblDefaultValue" runat="server" Text="Default Value:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblDefaultValueResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="text" class="sfInputbox" title="Default Value" value="" name="default_value_text"
                                        id="default_value_text" />
                                        <span id="iferror" class="iferror" style='color: #FF0000;'></span>
                                    <div id="fileDefaultTooltip" style="display: none;" class="cssClassRed">
                                    </div>
                                    <textarea class="cssClassTextArea" cols="15" rows="2" title="Default Value" name="default_value_textarea"
                                        id="default_value_textarea"></textarea>
                                    <div id="div_default_value_date">
                                        <input type="text" class="sfInputbox" title="Default Value" value="" id="default_value_date"
                                            name="default_value_date" />
                                    </div>
                                    <select class="sfListmenu" title="Default Value" name="default_value_yesno"
                                        id="default_value_yesno">
                                        <option value="1" class="sfLocale">Yes</option>
                                        <option selected="selected" value="0" class="sfLocale">No</option>
                                    </select>
                                </td>
                            </tr>
                            <tr id="trOptionsAdd">
                                <td>
                                    <asp:Label ID="lblAddOptions" runat="server" Text="Manage Options (values of your attribute):"
                                        CssClass="cssClassLabel" meta:resourcekey="lblAddOptionsResource1"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <table id="dataTable" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblValue" runat="server" Text="Value:" CssClass="cssClassLabel" meta:resourcekey="lblValueResource1"></asp:Label>
                                                    <span class="cssClassRequired">*</span>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblPosition" runat="server" Text="Display Order:" CssClass="cssClassLabel"
                                                        meta:resourcekey="lblPositionResource1"></asp:Label>
                                                    <span class="cssClassRequired">*</span>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblAlias" runat="server" Text="Alias:" CssClass="cssClassLabel" meta:resourcekey="lblAliasResource1"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblIsDefault" runat="server" Text="Is Default:" CssClass="cssClassLabel"
                                                        meta:resourcekey="lblIsDefaultResource1"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td>
                                                <input type="hidden" class="class-text" value="0" name="optionValueId" />
                                                <input type="text" class="class-text" value="" name="value" />
                                                <span class='' style='color: #FF0000;'>*</span>
                                            </td>
                                            <td>
                                                <input type="text" class="class-text" value="" name="position" />
                                                <span class='' style='color: #FF0000;'>*</span>
                                            </td>
                                            <td>
                                                <input type="text" class="class-text" value="" name="Alias" />
                                            </td>
                                            <td class="tddefault">
                                            </td>
                                            <td>
                                                <input type="Button" value="Add More" name="AddMore" class="AddOption cssClassButtonSubmit sfLocale" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUniqueValue" runat="server" Text="Unique Value:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblUniqueValueResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUniqueValue" value="" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTypeValidation" runat="server" Text="Type Validation:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblTypeValidationResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="ddlTypeValidation" class="sfListmenu" name="" title="Attribute Input Validation Type">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblValuesRequired" runat="server" Text="Values Required:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblValuesRequiredResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkValuesRequired" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblApplyTo" runat="server" Text="Apply To:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblApplyToResource1"></asp:Label>
                                    
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="ddlApplyTo" class="sfListmenu" name="">
                                        <option selected="selected" value="0" class="sfLocale">All Item Types</option>
                                        <option value="1" class="sfLocale">Selected Item Types</option>
                                    </select><span class="cssClassRequired">*</span>
                                </td>
                            </tr>
                            <tr class="itemTypes">
                                <td>&nbsp;
                                    
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="lstItemType" class="cssClassMultiSelect">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLength" runat="server" Text="Length:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblLengthResource1"></asp:Label>
                                   
                                </td>
                                <td class="cssClassTableRightCol required">
                                    <input class="sfInputbox verifyInteger" id="txtLength" type="text" MaxLength="10"/>
                                    <span class="iferror sfLocale" style="color: #FF0000;">Integer Number</span> <span class="cssClassRequired">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAliasName" runat="server" Text="Alias Name:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblAliasNameResource1"></asp:Label>
                                  
                                </td>
                                <td class="cssClassTableRightCol required">
                                    <input class="sfInputbox" id="txtAliasName" type="text" />
                                <span class="cssClassRequired">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAliasToolTip" runat="server" Text="Alias ToolTip:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblAliasToolTipResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input class="sfInputbox" id="txtAliasToolTip" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAliasHelp" runat="server" Text="Alias Help:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblAliasHelpResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input class="sfInputbox" id="txtAliasHelp" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblDisplayOrderResource1"></asp:Label>
                                
                                </td>
                                <td class="cssClassTableRightCol required">
                                    <input class="sfInputbox verifyInteger" id="txtDisplayOrder" type="text" /> <span class="cssClassRequired sfLocale">*</span>
                                    <span class="iferror sfLocale" style="color: #FF0000;">Integer Number</span>   
                                </td>
                            </tr>
                            <tr id="activeTR">
                                <td>
                                    <asp:Label ID="lblActive" runat="server" Text="Active:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblActiveResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkActive" class="cssClassCheckBox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="fragment-2">
                    <div class="sfFormwrapper">
                        <h3>
                            <asp:Label ID="lblTab2Info" runat="server" Text="Frontend Display Settings" meta:resourcekey="lblTab2InfoResource1"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsEnableEditor" runat="server" Text="Is Enable Editor:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblIsEnableEditorResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsEnableEditor" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsUseInFilter" runat="server" Text="Used in Filter:" CssClass="cssClassLabel" meta:resourcekey="lblIsUseInFilterResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsUseInFilter" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="lblShowInGrid" runat="server" Text="Show in Grid:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInGrid" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsEnableSorting" runat="server" Text="Is Enable Sorting:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsEnableSorting" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsUseInFilter" runat="server" Text="Used in Filter:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsUseInFilter" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowInSearch" runat="server" Text="Use in Search:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInSearch" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUseInAdvancedSearch" runat="server" Text="Use in Advanced Search:"
                                        CssClass="cssClassLabel" meta:resourcekey="lblUseInAdvancedSearchResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseInAdvancedSearch" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblComparable" runat="server" Text="Comparable on Front-end:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblComparableResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkComparable" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUseForPriceRule" runat="server" Text="Use for Price Rule Conditions:"
                                        CssClass="cssClassLabel" meta:resourcekey="lblUseForPriceRuleResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseForPriceRule" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowInItemListing" runat="server" Text="Show In Item Listing:"
                                        CssClass="cssClassLabel" meta:resourcekey="lblShowInItemListingResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInItemListing" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowInItemDetail" runat="server" Text="Show In Item Detail Page:"
                                        CssClass="cssClassLabel" meta:resourcekey="lblShowInItemDetailResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInItemDetail" class="cssClassCheckBox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
            <p>
                <button type="button" id="btnReset" class="sfBtn">
                   <span class="sfLocale icon-refresh">Reset</span></button>
            </p>
            <p>
                <button type="button" class="delbutton sfBtn">
                    <span class="sfLocale icon-delete">Delete</span></button>
            </p>
            <p>
                <button type="button" id="btnSaveAttribute" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
        </div>
    </div>
</div>
<!-- End form -->
