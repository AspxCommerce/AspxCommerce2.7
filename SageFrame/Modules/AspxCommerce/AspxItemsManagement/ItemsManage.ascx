<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsManage.ascx.cs" Inherits="Modules_AspxItemsManagement_ItemsManage" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxItemsManagement
        });
    });

    //<![CDATA[
    var userEmail = '<%=userEmail %>';
    var maxFileSize = '<%=MaximumFileSize %>';
    var maxDownloadFileSize = '<%=MaxDownloadFileSize%>';
    var PriceUnit = '<%=PriceUnit %>';
    var WeightUnit = '<%=WeightUnit %>';
    var DimensionUnit = '<%=DimensionUnit %>';
    var lowStockItemRss = '<%=LowStockItemRss %>';
    var rssFeedUrl = '<%=RssFeedUrl %>';
    var currencyCode = '<%=CurrencyCodeSlected %>';
    var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
    var NotificationItemID = strDecrypt(getParameterByName("itemid"));
    var NotificationItemTypeID = strDecrypt(getParameterByName("itemtypeid"));
    var NotificationAttributeSetID = strDecrypt(getParameterByName("attributesetid"));
    var NotificationCurrencyCode = strDecrypt(getParameterByName("currencycode"));
    var ItemTabSetting = '<%=Settings %>';
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    var itemTypeArray = [];
    var attributeSetArray = [];
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<!-- Grid -->
<div id="gdvItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Items" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassRssDiv">
                <a class="cssRssImage" href="#" style="display: none">
                    <img id="lowStockRssImage" alt="" src="" title="" />
                </a>
            </div>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper" id="divItemButtonWrapper">
                    <p>
                        <button type="button" id="btnAddNew" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Item</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddItemSetting" class="sfBtn">
                            <span class="sfLocale icon-item-setting">Item Settings</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
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
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    SKU:</label>
                                <input type="text" id="txtSearchSKU" class="sfTextBoxSmall" style="width:85px;" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Name:</label>
                                <input type="text" id="txtSearchName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Item Type:</label>
                                <select id="ddlSearchItemType" class="sfListmenu" style="width:100px;">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Attribute Set Name:</label>
                                <select id="ddlAttributeSetName" class="sfListmenu" style="width:140px;">
                                    <option value="0" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Visibility:</label>
                                <select id="ddlVisibitity" class="sfListmenu" style="width:85px;">
                                    <option value="" class="sfLocale">--All--</option>
                                    <option value="True" class="sfLocale">True</option>
                                    <option value="False" class="sfLocale">False</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Active:</label>
                                <select id="ddlIsActive" class="sfListmenu" style="width:85px;">
                                    <option value="" class="sfLocale">--All--</option>
                                    <option value="True" class="sfLocale">True</option>
                                    <option value="False" class="sfLocale">False</option>
                                </select>
                            </td>
                            <td>
                                <br />
                                <button type="button" id="btnSearchItems" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxImageLoader" src="" alt="loading...." class="sfLocale" />
                </div>
                <div class="log">
                </div>
                
                <table id="gdvItems" class="sfTblComplex" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
        <div class="cssClassItemSetting sfFormwrapper" style="display: none">
            <h3 class="cssClassHeading"><span class="sfLocale">Item Settings</span></h3>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableCostVariantOption" runat="server" 
                            Text="Show Cost Variant Option:" 
                            meta:resourcekey="lblEnableCostVariantOptionResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableCostVariantOption" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableGroupPrice" runat="server" Text="Show Group Price:" 
                            meta:resourcekey="lblEnableGroupPriceResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableGroupPrice" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableTierPrice" runat="server" Text="Show Tier Price:" 
                            meta:resourcekey="lblEnableTierPriceResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableTierPrice" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableRelatedItem" runat="server" Text="Show Related Item:" 
                            meta:resourcekey="lblEnableRelatedItemResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableRelatedItem" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableCrossSellItem" runat="server" 
                            Text="Show Cross Sell Item:" meta:resourcekey="lblEnableCrossSellItemResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableCrossSellItem" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEnableUpSellItem" runat="server" Text="Show Up Sell Item:" 
                            meta:resourcekey="lblEnableUpSellItemResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkEnableUpSellItem" />
                    </td>
                </tr>
                <tr>
                    <td>
                       
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
            <div class="sfButtonwrapper">
         <label class="sfBtn icon-arrow-slim-w "><input type="button" id="btnItemSettingBack" class="sfbtn" /><span class="sfLocale">Back</span></label>
         <label class="sfBtn icon-save "><input type="button" id="btnItemSettingSave" class= "sfbtn" /><span class="sfLocale">Save</span></label>
        </div>
        </div>
        
    </div>
</div>
<!-- End of Grid -->
<!-- Add New Item -->
<div id="divItemMgrTitle" class="cssClassHeader">
    <h1>
        <span id="spnAddTitle"></span>
    </h1>
</div><div id="gdvItems_form" style="display: none" class="sfFormwrapper">



    <div class="cssClassCommonBox cssClassAttribute Curve  sfSecMrg-b" style="display: none;">
        <h3 class="sfLocale">Select Item Type</h3>
        <div>
            <%--<h2>
                <asp:Label ID="lblTabInfo" runat="server" Text="Create Item Settings" meta:resourcekey="lblTabInfoResource1"></asp:Label>
            </h2>--%>
            <table cellspacing="0" cellpadding="0" border="0" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblAttributeSet" runat="server" Text="Attribute Set:" CssClass="cssClassLabel"
                            meta:resourcekey="lblAttributeSetResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlAttributeSet" class="sfListmenu" name="D1">
                        </select>
                    </td>
                    <td>
                        <asp:Label ID="lblItemType" runat="server" Text="Item Type:" CssClass="cssClassLabel"
                            meta:resourcekey="lblItemTypeResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlItemType" class="sfListmenu" name="D2">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divItemTabWrapper">
        <h3 class="sfLocale">Set Item Details</h3>
        <div id="divItemTypeTab" class="clearfix sfAdvanceRadioBtn sfStdMrg-b ">
            <div class="sfRadiobutton sfRadioBig">
                <label class="sfActive" id="generalTab">
                <input type="radio" style="display: none;" name="ItemAttributeTab" checked="checked" id="rdbGeneralType"/>
                <span class ="sfLocale">General</span></label>
                <label id="advancedTab"><input type="radio" style="display: none;" name="ItemAttributeTab" id="rdbAdvancedType"/>
               <span class="sfLocale">Advanced</span></label>
            </div>
            
        </div>
        <div class="cssClassLanguageSettingWrapper">
            <span class="sfLocale">Select Langauge:</span>
            <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
            
        </div>
        <div class="cssItemContentWrapper">
        <div id="divGeneralContent" style="display:none;">
        </div>
        <div id="divAdvancedContent" style="display:none;">
    <div id="gdvItems_accordin" style="display: none" class="sfSecMrg-t">       

        <div class="cssClassCommonBox Curve">
            <input type="hidden" id="ItemMgt_itemID" value="0" />
            <div class="quick-navigation">
                <ul class="sf-menu" id="ulQuickNavigation">
                    <li></li>
                </ul>
            </div>
            <div id="dynItemForm" class="cssClassAccordionWrapper">
            </div>
        </div>
    </div>
            </div>
       </div>
        </div>
</div>

<input type="hidden" id="hdnSKUTxtBox" />
<div class="popupbox cssClassVariantImagews" id="popuprel2">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <div class="sfGridWrapperContent">
        <div id="divUploader">
            <input type="file" class="cssClassBrowse" id="imageUploader"/>
        </div>
        <table cellspacing="0" cellpadding="o" border="0" width="100%" id="VariantsImagesTable">
            <thead>
                <tr class="cssClassHeading">
                    <td class="sfLocale">Image
                    </td>
                    <td class="sfLocale">Remove
                    </td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnImageBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
            <p>
                <button type="button" id="btnSaveImages" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
            
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>

<div class="popupbox" id="popuprel12">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose sfBtn">
            <span class="sfLocale icon-close">Close</span></button>
    </div>
    <div id="dvAttMgmt">
        <div id="divAttribForm">
            <div class="cssClassCommonBox Curve">
                
                <div class="cssClassHeader">
                    <h2>
                        <asp:Label ID="lblAttrFormHeading" runat="server" Text="General Information" meta:resourcekey="lblAttrFormHeadingResource1"></asp:Label>
                    </h2>
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
                                <div class="LanguageFlag">
                   
                                    </div>
                                <h2>
                                    <asp:Label ID="lblTab1Info" runat="server" Text="General Information" meta:resourcekey="lblTab1InfoResource1"></asp:Label>
                                </h2>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td width="20%">
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
                                            <table id="dataTable" cellspacing="0" cellpadding="0" border="0" width="100%" class="att-multiselect">
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
                                                    <td class="tddefault"></td>
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
                                            <input class="sfInputbox verifyInteger" id="txtLength" type="text" maxlength="10" />
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
                                            <input class="sfInputbox verifyInteger" id="txtDisplayOrder" type="text" />
                                            <span class="cssClassRequired sfLocale">*</span>
                                            <span class="iferror sfLocale" style="color: #FF0000;">Integer Number</span>
                                        </td>
                                    </tr>
                                    <tr id="activeTR">
                                        <td>
                                            <asp:Label ID="lblActive" runat="server" Text="Active:" CssClass="cssClassLabel"
                                                meta:resourcekey="lblActiveResource1"></asp:Label>
                                        </td>
                                        <td class="cssClassTableRightCol">
                                            <input type="checkbox" name="chkActive" class="cssClassCheckBox" checked="checked" disabled="disabled" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="fragment-2">
                            <div class="sfFormwrapper">
                                <h2>
                                    <asp:Label ID="lblTab2Info" runat="server" Text="Frontend Display Settings" meta:resourcekey="lblTab2InfoResource1"></asp:Label>
                                </h2>
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
                                            <asp:Label ID="lblIsUseInFilter" runat="server" Text="Used in Filter:" 
                                                CssClass="cssClassLabel" meta:resourcekey="lblIsUseInFilterResource1"></asp:Label>
                                        </td>
                                        <td class="cssClassTableRightCol">
                                            <input type="checkbox" name="chkIsUseInFilter" class="cssClassCheckBox" />
                                        </td>
                                    </tr>

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
                        <button type="button" id="btnResetA" class="sfBtn">
                            <span class="sfLocale icon-refresh">Reset</span></button>
                    </p>

                    <p>
                        <button type="button" id="btnSaveAttribute" class="sfBtn">
                            <span class="sfLocale icon-save">Save</span></button>
                    </p>
                </div>
            </div>
        </div>

    </div>
</div>

<script type="text/javascript">

    var attributesManage = '';
    $(function () {

        var lblAttrFormHeading = "<%= lblAttrFormHeading.ClientID %>";
        var lblLength = "<%= lblLength.ClientID %>";
        var lblDefaultValue = "<%= lblDefaultValue.ClientID %>";
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        };
        var isUnique = false;
        var editFlag = 0;
        var arrAttrValueId = "";
        attributesManage = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: attributesManage.config.type
                    ,beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },

                    contentType: attributesManage.config.contentType,
                    cache: attributesManage.config.cache,
                    async: attributesManage.config.async,
                    url: attributesManage.config.url,
                    data: attributesManage.config.data,
                    dataType: attributesManage.config.dataType,
                    success: attributesManage.ajaxSuccess,
                    error: attributesManage.ajaxFailure
                });
            },
            LoadAttributeStaticImage: function () {
                $('.cssClassSuccessImg').prop('src', '' + aspxTemplateFolderPath + '/images/right.jpg');
            },
            ClearOptionTable: function (btnAddOption) {
                btnAddOption.closest("tr:eq(0)").find("input:not(:last)").each(function (i) {
                    $(this).val('');
                    $(this).removeAttr('checked');
                });
            },
            onInit: function () {
                attributesManage.SetFirstTabActive();
                $("#ddlApplyTo").val('0');
                $('.itemTypes').hide();
                $('#btnReset').hide();
                $('.cssClassRight').hide();
                $('.cssClassError').hide();
                $("#lstItemType").each(function () {
                    $("#lstItemType option").removeAttr("selected");
                });
                //$("#txtAttributeName").focus();
            },
            SetFirstTabActive: function () {
                var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
                $tabs.tabs('option', 'active', 0);
            },
            Boolean: function (str) {
                switch (str) {
                    case "1":
                        return true;
                    case "0":
                        return false;
                    default:
                        //throw new Error("Boolean.parse: Cannot convert string to boolean.");
                }
            },
            CreateValidationClass: function (attValType) {
                var validationClass = '';

                switch (attValType) {
                    case "1":
                        //AlphabetsOnly
                        validationClass += 'verifyAlphabetsOnly';
                        break;
                    case "2":
                        //AlphaNumeric
                        validationClass += 'verifyAlphaNumeric';
                        break;
                    case "3":
                        //DecimalNumber
                        validationClass += 'verifyDecimal';
                        break;
                    case "4":
                        //Email
                        validationClass += 'verifyEmail';
                        break;
                    case "5":
                        //IntegerNumber
                        validationClass += 'verifyInteger';
                        break;
                    case "6":
                        //Price
                        validationClass += 'verifyPrice';
                        break;
                    case "7":
                        // URL
                        validationClass += 'verifyUrl';
                        break;
                    default:
                        //NONE
                        validationClass += '';
                        break;
                }
                return validationClass;
            },
            GetValidationTypeErrorMessage: function (attValType) {
                var retString = ''
                switch (attValType) {
                    case "1":
                        //AlphabetsOnly
                        retString = 'Alphabets Only';
                        break;
                    case "2":
                        //AlphaNumeric
                        retString = 'AlphaNumeric';
                        break;
                    case "3":
                        //DecimalNumber
                        retString = 'Decimal Number';
                        break;
                    case "4":
                        //Email
                        retString = 'Email Address';
                        break;
                    case "5":
                        //IntegerNumber
                        retString = 'Integer Number';
                        break;
                    case "6":
                        //Price
                        retString = 'Price error';
                        break;
                    case "7":
                        //WebURL
                        retString = 'Web URL';
                        break;
                }
                return retString;
            },
            ClearForm: function () {
                $('.class-text').removeClass('error').next('span').removeClass('error');
                var inputs = $("#container-7").find('INPUT, SELECT, TEXTAREA');
                $.each(inputs, function (i, item) {
                    rmErrorClass(item);
                    $(this).val('');
                    $(this).prop('checked', false);
                });
                attributesManage.onInit();
                $('#' + lblAttrFormHeading).html(getLocale(AspxItemsManagement, "New Item Attribute"));
                $(".delbutton").removeAttr("id");
                $("#btnSaveAttribute").removeAttr("name");
                $('#' + lblLength).html(getLocale("Length:"));
                $(".delbutton").hide();
                $("#btnReset").show();
                $(".required:enabled").each(function () {

                    if ($(this).parent("td").find("span.error").length == 1) {
                        $(this).removeClass("error").addClass("required");
                        $(this).parent("td").find("span.error").remove();
                    }

                });
                $('#txtAttributeName').val('');
                $('#txtAttributeName').removeAttr('disabled');
                $('#ddlAttributeType').val('1');
                $('#ddlAttributeType').removeAttr('disabled');

                $("#default_value_text").prop("class", "sfInputbox");
                $("#default_value_text").val('');
                $("#default_value_textarea").val('');
                $("#default_value_date").val('');
                $("#trdefaultValue").show();
                $("#default_value_text").show();
                $("#fileDefaultTooltip").html('');
                $("#fileDefaultTooltip").hide();
                $("#default_value_textarea").hide();
                $("#div_default_value_date").hide();
                $("#default_value_yesno").hide();

                $('#default_value_text').val('');
                $("#dataTable tr:gt(1)").remove();
                attributesManage.ClearOptionTable($("input[type='button'].AddOption"));
                $('#trOptionsAdd').hide();

                $('#ddlTypeValidation').val('8');
                $('#ddlTypeValidation').removeAttr('disabled');

                $('#txtLength').val('');
                $('#txtLength').removeAttr('disabled');
                $('#txtLength').next('span').next('span').show();
                $('#txtAliasName').val('');
                $('#txtAliasToolTip').val('');
                $('#txtAliasHelp').val('');
                $('#txtDisplayOrder').val('');
                $('#ddlApplyTo').val('0');
                $('.itemTypes').hide();

                $('input[name=chkUniqueValue]').removeAttr('checked');
                $('input[name=chkValuesRequired]').removeAttr('checked');
                $('input[name=chkActive]').prop('checked', 'checked');
                $('#activeTR').show();

                //Next Tab
                $('input[name=chkIsEnableEditor]').removeAttr('checked');
                $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                $('input[name=chkUseInAdvancedSearch]').removeAttr('disabled');
                $('input[name=chkUseInAdvancedSearch]').removeAttr('checked');
                $('input[name=chkComparable]').removeAttr('disabled');
                $('input[name=chkComparable]').removeAttr('checked');
                $('input[name=chkUseForPriceRule]').removeAttr('disabled');
                $('input[name=chkUseForPriceRule]').removeAttr('checked');
                $('input[name=chkIsUseInFilter]').removeAttr('disabled');
                $('input[name=chkIsUseInFilter]').removeAttr('checked');
                $('input[name=chkShowInItemListing]').removeAttr('checked');
                $('input[name=chkShowInItemDetail]').removeAttr('checked');
                $('input[name=optionValueId]').val('0');
                return false;
            },
            BindAttributeOptionsValues: function (_fillOptionValues) {
                var _fillOptions = _fillOptionValues;
                if (_fillOptions != undefined && _fillOptions != "") {
                    var arr = _fillOptions.split("!#!");
                    var htmlContent = '';
                    $.each(arr, function (i) {
                        var btnOption = "Add More";
                        var btnName = "AddMore";
                        if (i > 0) {
                            btnOption = "Delete Option";
                            var btnName = "DeleteOption";
                        }
                        var arr2 = arr[i].split("#!#");
                        var cloneRow = $('#dataTable tbody>tr:last').clone(true);
                        $(cloneRow).find("input").each(function (j) {

                            if (this.name == "optionValueId") {
                                $(this).val(arr2[0]);
                            } else if (this.name == "value") {
                                $(this).val(arr2[1]);
                            } else if (this.name == "position") {
                                $(this).val(arr2[2]);
                            } else if (this.name == "Alias") {
                                $(this).val(arr2[3]);
                            } else if ($(this).hasClass("class-isdefault")) {
                                this.checked = attributesManage.Boolean(arr2[4]);
                            } else if ($(this).hasClass("AddOption")) {
                                $(this).prop("name", btnName);
                                $(this).prop("value", btnOption);
                            }
                        });
                        $(cloneRow).appendTo("#dataTable");
                    });
                    $('#dataTable>tbody tr:first').remove();
                }
            },
            ValidationTypeEnableDisable: function (fillOptionValues, isChanged) {
                var selectedVal = $("#ddlAttributeType :selected").val();
                switch (selectedVal) {
                    case "1":
                        //TextField
                        $("#ddlTypeValidation").removeAttr('disabled');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Default Value:"));
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        $("#trdefaultValue").show();
                        $("#default_value_text").show();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "2":
                        //TextArea
                        $('#ddlTypeValidation').val('8');
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $('#' + lblDefaultValue).html("Default Value:");
                        $('#' + lblLength).html("Rows:");
                        if (isChanged) {
                            $('#txtLength').val(3);
                        }
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        $("#trdefaultValue").show();
                        $("#default_value_text").hide();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").show();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').removeAttr('disabled');
                        break;
                    case "3":
                        //Date
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Default Value:"));
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").show();
                        $("#default_value_text").hide();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").show();
                        $("#default_value_date").datepicker({ dateFormat: 'yy/mm/dd' });
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "4":
                        //Boolean
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Default Value:"));
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").show();
                        $("#default_value_text").hide();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").show();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "5":
                        //MultipleSelect
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html("Length:");
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Size:"));
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        if (isChanged) {
                            $('#txtLength').val(3);
                        }
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();
                        //$("input[name=defaultChk]").show();
                        //$("input[name=defaultRdo]").hide();
                        $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                        $(".AddOption").val("Add More");
                        $(".AddOption").show();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "6":
                        //DropDown
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").val('');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();
                        //$("input[name=defaultChk]").hide();
                        //$("input[name=defaultRdo]").show();
                        $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                        $(".AddOption").val("Add More");
                        $(".AddOption").show();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "7":
                        //Price
                        $('#ddlTypeValidation').val('6');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Default Value:"));
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        $("#trdefaultValue").show();
                        $("#default_value_text").show();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "8":
                        //File
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Allowed File Extension(s):"));
                        $("#fileDefaultTooltip").html(getLocale(AspxItemsManagement, "- Separate each file extensions with space"));
                        $("#fileDefaultTooltip").show();
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Size:(KB)"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        $("#trdefaultValue").show();
                        $("#default_value_text").show();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "9":
                        //Radio
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();

                        //$("input[name=defaultChk]").hide();
                        //$("input[name=defaultRdo]").show();
                        $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                        $(".AddOption").hide();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "10":
                        //RadioButtonList
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();
                        //$("input[name=defaultChk]").hide();
                        //$("input[name=defaultRdo]").show();
                        $(".tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                        $(".AddOption").val("Add More");
                        $(".AddOption").show();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "11":
                        //CheckBox
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();
                        //$("input[name=defaultChk]").show();
                        //$("input[name=defaultRdo]").hide();
                        $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                        $(".AddOption").hide();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "12":
                        //CheckBoxList
                        $('#ddlTypeValidation').val('8');
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#ddlTypeValidation").prop('disabled', 'disabled');
                        $("#txtLength").prop('disabled', 'disabled');
                        $('#txtLength').next('span').next('span').hide();
                        $("#trdefaultValue").hide();
                        $('#trOptionsAdd').show();
                        //$("input[name=defaultChk]").show();
                        //$("input[name=defaultRdo]").hide();
                        $(".tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                        $(".AddOption").val(getLocale(AspxItemsManagement, "Add More"));
                        $(".AddOption").show();
                        attributesManage.BindAttributeOptionsValues(fillOptionValues);
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    case "13":
                        $("#ddlTypeValidation").removeAttr('disabled');
                        $('#' + lblDefaultValue).html(getLocale(AspxItemsManagement, "Default Value:"));
                        $('#' + lblLength).html(getLocale(AspxItemsManagement, "Length:"));
                        if (isChanged) {
                            $('#txtLength').val('');
                        }
                        $("#txtLength").removeAttr('disabled');
                        $('#txtLength').next('span').next('span').show();
                        $("#trdefaultValue").show();
                        $("#default_value_text").show();
                        $("#fileDefaultTooltip").html('');
                        $("#fileDefaultTooltip").hide();
                        $("#default_value_textarea").hide();
                        $("#div_default_value_date").hide();
                        $("#default_value_yesno").hide();
                        $('#trOptionsAdd').hide();
                        $('input[name=chkIsEnableEditor]').prop('disabled', 'disabled');
                        break;
                    default:
                        break;
                }
            },

            FillDefaultValue: function (defaultVal) {
                var selectedAttributeType = $("#ddlAttributeType :selected").val();
                switch (selectedAttributeType) {
                    case "1":
                        $('#default_value_text').val(defaultVal);
                        break;
                    case "2":
                        $('textarea#default_value_textarea').val(defaultVal);
                        break;
                    case "3":
                        //var formattedDate = formatDate(new Date(DateDeserialize(defaultVal)), "yyyy/M/d");
                        $('#default_value_date').val(defaultVal);
                        break;
                    case "4":
                        $('#default_value_yesno').val(defaultVal);
                        break;
                    case "8":
                        $('#default_value_text').val(defaultVal);
                        break;
                    default:
                        break;
                }
            },

            DateDeserialize: function (dateStr) {
                return eval('new' + dateStr.replace(/\//g, ' '));
            },
            IsUnique: function (attributeName, attributeId) {
                //  var isUnique = false;
                var attrbuteUniqueObj = {
                    AttributeID: attributeId,
                    AttributeName: attributeName
                };
                var aspxTempCommonObj = aspxCommonObj();
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                this.config.url = this.config.baseURL + "CheckUniqueAttributeName";
                this.config.data = JSON2.stringify({ attrbuteUniqueObj: attrbuteUniqueObj, aspxCommonObj: aspxTempCommonObj });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
                return isUnique;
            },
            SaveAttribute: function (_attributeId, _flag) {
                $('#iferror').hide();
                //    if (checkForm($("#form1")))
                // {
                var selectedItemTypeID = '';
                var validateErrorMessage = '';
                var itemSelected = false;
                var isUsedInConfigItem = false;

                var error = {
                    current: { Id: '', Msg: '' },
                    list: [],
                    exist: false
                }
                // Validate name
                var attributeName = $('#txtAttributeName').val();
                if (!attributeName) {
                    // validateErrorMessage += 'Please enter attribute name.<br/>';
                    error.exist = true;
                    var current = { Id: $('#txtAttributeName'), Msg: "" + getLocale(AspxItemsManagement, "Please enter attribute name") + "" };
                    error.list.push(current);
                } else if (!attributesManage.IsUnique(attributeName, _attributeId)) {
                    //validateErrorMessage += "'" + getLocale(AspxItemsManagement, "Please enter unique attribute name") + "'" + attributeName.trim() + "'" + getLocale(AspxItemsManagement, "already exists.") + '<br/>';
                    error.exist = true;
                    var current = { Id: $('#txtAttributeName'), Msg: getLocale(AspxItemsManagement, "Please enter unique attribute name") + "'" + attributeName.trim() + "'" + getLocale(AspxItemsManagement, "already exists.") };
                    error.list.push(current);
                }
                //Validate ddlApplyTo and lstItemType selected at least one item
                var selectedValue = $("#ddlApplyTo").val();
                if (selectedValue !== "0") {
                    $("#lstItemType").each(function () {
                        if ($("#lstItemType :selected").length != 0) {
                            itemSelected = true;
                            $("#lstItemType option:selected").each(function (i) {
                                //alert($(this).text() + " : " + $(this).val());
                                selectedItemTypeID += $(this).val() + ',';
                                if ($(this).val() == '3') {
                                    isUsedInConfigItem = true;
                                }
                            });
                        }
                    });
                    if (!itemSelected) {
                        // validateErrorMessage += getLocale(AspxItemsManagement, "Please select at least one item type.") + "<br/>";
                        error.exist = true;

                        var current = { Id: $('#ddlApplyTo'), Msg: getLocale(AspxItemsManagement, "Please select at least one item type.") };
                        error.list.push(current);
                    }
                } else {
                    isUsedInConfigItem = true;
                    $("#lstItemType option").each(function (i) {
                        selectedItemTypeID += $(this).val() + ',';
                    });
                }

                selectedItemTypeID = selectedItemTypeID.substring(0, selectedItemTypeID.length - 1);

                // Validate attribute max length
                if ($('#toggleElement').is(':checked'))
                    var _Length = '';
                if (!($('#txtLength').is(':disabled'))) {
                    _Length = $('#txtLength').val();
                }
                // Validate options value inputs filled
                var selectedVal = $("#ddlAttributeType :selected").val();
                var _saveOptions = '';
                if (selectedVal == 5 || selectedVal == 6 || selectedVal == 9 || selectedVal == 10 || selectedVal == 11 || selectedVal == 12) {
                    $("#dataTable").find("tr input").each(function (i) {
                        //if ($(this).is(":visible")) {
                        //  if (!$(this).prop('name', 'Alias')) {
                        $(this).parent('td').find('span').removeClass('error');
                        $(this).removeClass('error');
                        var optionsText = $(this).val();
                        if ($(this).hasClass("class-text")) {
                            if (!optionsText && $(this).prop("name") != "Alias") {
                                validateErrorMessage = getLocale(AspxItemsManagement, "Please enter all option values and display order for your attribute.") + "<br/>";
                                $(this).parent('td').find('span').addClass('error').show();
                                attributesManage.SetFirstTabActive();
                                $(this).addClass('error');
                                $(this).focus();
                            } else {
                                if ($(this).prop("name") == "position") {
                                    var value = optionsText.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                                    var intRegex = /^\d+$/;
                                    if (!intRegex.test(value)) {
                                        validateErrorMessage = getLocale(AspxItemsManagement, "Display order is numeric value.") + '<br/>';
                                        $(this).parent('td').find('span').addClass('error').show();
                                        attributesManage.SetFirstTabActive();
                                        $(this).addClass('error');
                                        $(this).focus();
                                    }
                                }
                                _saveOptions += optionsText + "#!#";
                            }
                        } else if ($(this).hasClass("class-isdefault")) { //&& $(this).is(":visible") && $("#container-7 ul li:first").hasClass("ui-tabs-selected")) {
                            var _IsChecked = $(this).prop('checked');
                            _saveOptions += _IsChecked + "!#!";
                        }
                        // }
                        //}           
                    });
                }
                var display = $("#txtDisplayOrder").val();
                if (display == '' || display <= 0) {
                    error.exist = true;
                    var current = { Id: $('#txtDisplayOrder'), Msg: getLocale(AspxItemsManagement, "Please enter display order.") };
                    error.list.push(current);
                }
                var txtAliasName = $("#txtAliasName").val();
                if (txtAliasName == '' || txtAliasName <= 0) {
                    error.exist = true;
                    var current = { Id: $('#txtAliasName'), Msg: getLocale(AspxItemsManagement, "Please enter alias name.") };
                    error.list.push(current);
                }
                if ($('#trOptionsAdd').is(":visible")) {
                    $('#trOptionsAdd').find("table td input[type=text]").each(function (index, item) {
                        var input = $(item);
                        if ($.trim(input.val()) == '') {
                            error.exist = true;
                            var current = { Id: input, Msg: getLocale(AspxItemsManagement, "Please enter values and alias.") };
                            error.list.push(current);
                        }
                    })
                }


                _saveOptions = _saveOptions.substring(0, _saveOptions.length - 3);
                if (!error.exist) {
                    var aspxCommonInfo = aspxCommonObj();
                    var _StoreID = aspxCommonInfo.StoreID;
                    var _PortalID = aspxCommonInfo.PortalID;
                    var _CultureName = $(".languageSelected").attr("value");
                    var _UserName = aspxCommonInfo.UserName;

                    var _attributeName = $('#txtAttributeName').val();
                    var _inputTypeID = $('#ddlAttributeType').val();

                    var selectedAttributeType = $("#ddlAttributeType :selected").val();
                    var _DefaultValue = "";
                    switch (selectedAttributeType) {
                        case "1":
                            _DefaultValue = $("#default_value_text").val();
                            break;
                        case "2":
                            _DefaultValue = $("textarea#default_value_textarea").val();
                            break;
                        case "3":
                            _DefaultValue = $("#default_value_date").val();
                            break;
                        case "4":
                            _DefaultValue = $("#default_value_yesno").val();
                            break;
                        case "8":
                            _DefaultValue = $("#default_value_text").val();
                            break;
                        default:
                            _DefaultValue = '';
                    }

                    var _ValidationTypeID = $('#ddlTypeValidation').val();
                    var _AliasName = $('#txtAliasName').val();
                    var _AliasToolTip = $('#txtAliasToolTip').val();
                    var _AliasHelp = $('#txtAliasHelp').val();
                    var _DisplayOrder = $('#txtDisplayOrder').val();

                    var _IsUnique = $('input[name=chkUniqueValue]').prop('checked');
                    var _IsRequired = $('input[name=chkValuesRequired]').prop('checked');
                    var _IsEnableEditor = $('input[name=chkIsEnableEditor]').prop('checked');
                    var _ShowInAdvanceSearch = $('input[name=chkUseInAdvancedSearch]').prop('checked');
                    var _ShowInComparison = $('input[name=chkComparable]').prop('checked');
                    var _IsUseInFilter = $('input[name=chkIsUseInFilter]').prop('checked'); //false
                    var _IsIncludeInPriceRule = $('input[name=chkUseForPriceRule]').prop('checked');
                    var _IsShowInItemListing = $('input[name=chkShowInItemListing]').prop('checked');
                    var _IsShowInItemDetail = $('input[name=chkShowInItemDetail]').prop('checked');
                    var _IsActive = $('input[name=chkActive]').prop('checked');
                    var _IsModified = true;
                    var _attributeValueId = arrAttrValueId;
                    var _ItemTypes = selectedItemTypeID;
                    var _Flag = _flag;
                    var _IsUsedInConfigItem = isUsedInConfigItem;

                    attributesManage.AddAttributeInfo(_attributeId, _attributeName, _inputTypeID, _DefaultValue,
                        _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder, _IsUnique, _IsRequired,
                        _IsEnableEditor, _ShowInAdvanceSearch, _ShowInComparison, _IsUseInFilter,
                        _IsIncludeInPriceRule, _IsShowInItemListing, _IsShowInItemDetail, _StoreID, _PortalID, _IsActive, _IsModified, _UserName,
                        _CultureName, _ItemTypes, _Flag, _IsUsedInConfigItem, _saveOptions, _attributeValueId);

                    return false;
                }
                else {

                    for (var x = 0; x < error.list.length; x++) {
                        csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Validation Alert') + "</h2><p>" + error.list[x].Msg + "</p>");
                        break;


                    }

                }
                // }
            },

            AddAttributeInfo: function (_attributeId, _attributeName, _inputTypeID, _DefaultValue,
                _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder,
                _IsUnique, _IsRequired, _IsEnableEditor,
                _ShowInAdvanceSearch, _ShowInComparison, _IsUseInFilter, _IsIncludeInPriceRule, _IsShowInItemListing, _IsShowInItemDetail,
                _storeId, _portalId, _IsActive, _IsModified, _userName, _CultureName, _ItemTypes, _flag, _isUsedInConfigItem, _saveOptions, _attributeValueId) {

                var info = {
                    AttributeID: parseInt(_attributeId),
                    AttributeName: _attributeName,
                    InputTypeID: _inputTypeID,
                    DefaultValue: _DefaultValue,
                    ValidationTypeID: _ValidationTypeID,
                    Length: _Length >= 0 ? _Length : null,
                    AliasName: _AliasName,
                    AliasToolTip: _AliasToolTip,
                    AliasHelp: _AliasHelp,
                    DisplayOrder: _DisplayOrder,
                    IsUnique: _IsUnique,
                    IsRequired: _IsRequired,
                    IsEnableEditor: _IsEnableEditor,
                    ShowInAdvanceSearch: _ShowInAdvanceSearch,
                    ShowInComparison: _ShowInComparison,
                    IsIncludeInPriceRule: _IsIncludeInPriceRule,
                    IsShowInItemListing: _IsShowInItemListing,
                    IsShowInItemDetail: _IsShowInItemDetail,
                    IsUseInFilter: _IsUseInFilter,
                    StoreID: _storeId,
                    PortalID: _portalId,
                    IsActive: _IsActive,
                    IsModified: _IsModified,
                    UpdatedBy: _userName,
                    AddedBy: _userName,
                    CultureName: _CultureName,
                    ItemTypes: _ItemTypes,
                    Flag: _flag,
                    IsUsedInConfigItem: _isUsedInConfigItem,
                    SaveOptions: _saveOptions,
                    AttributeValueID: _attributeValueId
                };
                var config = DynamicAttribute.Get();
                this.config.url = this.config.baseURL + "SaveUpdateAttributeInfo";
                this.config.data = JSON2.stringify({ attributeInfo: info, config: config });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(this.config);
                return false;
            },
            BindAttributesInputType: function () {
                this.config.url = this.config.baseURL + "GetAttributesInputTypeList";
                this.config.data = "{}";
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            BindAttributesValidationType: function () {
                this.config.url = this.config.baseURL + "GetAttributesValidationTypeList";
                this.config.data = "{}";
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            BindAttributesItemType: function () {
                this.config.url = this.config.baseURL + "GetAttributesItemTypeList";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            SearchAttributeName: function () {
                var attributeNm = $.trim($("#txtSearchAttributeName").val());
                var required = $.trim($('#ddlIsRequired').val()) == "" ? null : $.trim($('#ddlIsRequired').val()) == "True" ? true : false;
                var SearchComparable = $.trim($("#ddlComparable").val()) == "" ? null : $.trim($("#ddlComparable").val()) == "True" ? true : false;
                var isSystem = $.trim($("#ddlIsSystem").val()) == "" ? null : $.trim($("#ddlIsSystem").val()) == "True" ? true : false;
                if (attributeNm.length < 1) {
                    attributeNm = null;
                }
                attributesManage.BindAttributeGrid(attributeNm, required, SearchComparable, isSystem);
            },
            ajaxSuccess: function (msg) {
                switch (attributesManage.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        $("#ddlAttributeType").get(0).options.length = 0;
                        //$("#ddlAttributeType").get(0).options[0] = new Option("Select Type", "-1");
                        $.each(msg.d, function (index, item) {
                            $("#ddlAttributeType").get(0).options[$("#ddlAttributeType").get(0).options.length] = new Option(item.InputType, item.InputTypeID);
                        });
                        break
                    case 2:
                        $.each(msg.d, function (index, item) {
                            $("#ddlTypeValidation").get(0).options[$("#ddlTypeValidation").get(0).options.length] = new Option(item.ValidationType, item.ValidationTypeID);
                        });
                        break;
                    case 3:
                        itemTypeArray = msg.d;
                        $('#lstItemType').get(0).options.length = 0;
                        $('#lstItemType').prop('multiple', 'multiple');
                        $('#lstItemType').prop('size', '5');
                        //$('#lstItemType').removeAttr('multiple');
                        $.each(msg.d, function (index, item) {
                            $("#lstItemType").get(0).options[$("#lstItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                        });
                        break;
                    case 4:
                        attributesManage.FillForm(msg);
                        $('#divAttribGrid').hide();
                        $('#divAttribForm').show(); zz
                        break;
                    case 5:
                        attributesManage.BindAttributeGrid(null, null, null, null);
                        csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxItemsManagement, 'Attribute has been deleted successfully.') + "</p>");
                        $('#divAttribForm').hide();
                        $('#divAttribGrid').show();
                        break;
                    case 6:
                        attributesManage.BindAttributeGrid(null, null, null, null);
                        csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxItemsManagement, 'Selected attribute(s) has been deleted successfully.') + "</p>");
                        break;
                    case 7:
                        attributesManage.BindAttributeGrid(null, null, null, null);
                        break;
                    case 8:
                        isUnique = msg.d;
                        break;
                    case 9:
                        // attributesManage.BindAttributeGrid(null, null, null, null);
                        // $('#divAttribGrid').show();
                        if (editFlag > 0) {
                            csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxItemsManagement, 'Attribute has been updated successfully.') + "</p>");
                        } else {
                            csscody.info("<h2>" + getLocale(AspxItemsManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxItemsManagement, 'Attribute has been saved successfully.') + "</p>");
                        }
                        attributesManage.ClearForm();
                        // $('#divAttribForm').hide();
                        var attInfo = msg.d;
                        DynamicAttribute.Process(attInfo);
                        //popup close
                        break;
                }
            },
            ajaxFailure: function (msg) {
                switch (attributesManage.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to load attributes input type.") + '</p>');
                        break;
                    case 2:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to load validation type.") + '</p>');
                        break;
                    case 3:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to load attributes item type.") + '</p>');
                        break;
                    case 4:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to update attributes.") + '</p>');
                        break;
                    case 5:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to delete attribute.") + '</p>');
                        break;
                    case 6:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to delete attributes.") + '</p>');
                        break;
                    case 7:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to operate.") + '</p>');
                        break;
                    case 8:
                        break;
                    case 9:
                        csscody.error('<h2>' + getLocale(AspxItemsManagement, "Error Message") + '</h2><p>' + getLocale(AspxItemsManagement, "Failed to save attribute.") + '</p>');
                        break;
                }
            },
            init: function (config) {
                attributesManage.LoadAttributeStaticImage();
                attributesManage.onInit();
                //$('#divAttribForm').hide();

                attributesManage.BindAttributesInputType();
                attributesManage.BindAttributesValidationType();
                attributesManage.BindAttributesItemType();
                $('.itemTypes').hide();
                $('#ddlApplyTo').change(function () {
                    var selectedValue = $(this).val();
                    if (selectedValue !== "0") {
                        $('.itemTypes').show();
                    } else {
                        $('.itemTypes').hide();
                    }
                });


                $('#btnBack').bind("click", function () {
                    $('#divAttribForm').hide();
                    $('#divAttribGrid').show();
                    attributesManage.ClearForm();
                });

                $('#btnResetA').bind("click", function () {
                    attributesManage.ClearForm();
                });

                $('#btnSaveAttribute').click(function () {
                    //check if its update or save new
                    //Get the Id of the attribute to update               
                    var attribute_id = $(this).prop("name");
                    if (attribute_id != '') {
                        editFlag = attribute_id;
                        attributesManage.SaveAttribute(attribute_id, false);
                    } else {
                        editFlag = 0;
                        attributesManage.SaveAttribute(0, true);
                    }
                });

                //validate name on focus lost
                $('#txtAttributeName').blur(function () {
                    // Validate name
                    var errors = '';
                    var attributeName = $(this).val();
                    var attribute_id = $('#btnSaveAttribute').prop("name");
                    if (attribute_id == '') {
                        attribute_id = 0;
                    }
                    if (!attributeName) {

                        errors += getLocale(AspxItemsManagement, "Please enter attribute name");
                    }
                    //check uniqueness
                    else if (!attributesManage.IsUnique(attributeName, attribute_id)) {
                        errors += "'" + getLocale(AspxItemsManagement, "Please enter Please enter unique attribute name") + "'" + attributeName.trim() + "'" + getLocale(AspxItemsManagement, "already exists.") + '<br/>';
                    }

                    if (errors) {
                        $('.cssClassRight').hide();
                        $('.cssClassError').show();
                        $(".cssClassError").parent('div').addClass("diverror");
                        $('.cssClassError').prevAll("input:first").addClass("error");
                        $('.cssClassError').html(errors);
                        return false;
                    } else {
                        $(this).parent("td").find("span.error").hide();
                        $('.cssClassRight').show();
                        $('.cssClassError').hide();
                        $(".cssClassError").parent('div').removeClass("diverror");
                        $('.cssClassError').prevAll("input:first").removeClass("error");
                    }
                });

                $("#ddlAttributeType").bind("change", function () {
                    $('.class-text').removeClass('error').next('span').removeClass('error');
                    $("#default_value_text").prop("class", "sfInputbox");
                    $('#ddlTypeValidation').val('8');
                    $('#iferror').html('');
                    $('#iferror').hide();
                    //$("#dataTable>tbody tr:not(:first)").remove();
                    if ($(this).val() == 1 || $(this).val() == 2 || $(this).val() == 3 || $(this).val() == 7) {
                        $("input[name=chkValuesRequired]").prop('checked', false).prop("disabled", false);
                    } else {
                        $("input[name=chkValuesRequired]").prop('checked', false).prop("disabled", true);
                    }
                    $("#dataTable tr:gt(1)").remove();
                    $("#dataTable>tbody tr").find("input:not(:last)").each(function (i) {
                        if (this.name == "optionValueId") {
                            if ($(this).val() == '') {
                                $(this).val('0');
                            }
                        } else if (this.name == "value") {
                            $(this).val('');
                        } else if (this.name == "position") {
                            $(this).val('');
                        } else if ($(this).hasClass("class-isdefault")) {
                            this.checked = false;
                        }

                    });

                    attributesManage.ValidationTypeEnableDisable("", true);
                    if ($(this).val() == 10) {
                        $("#dataTable .tddefault").find('input[name=defaultRdo]').prop('checked', true);
                    }
                    var attValType = $("#ddlTypeValidation").val();
                    $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "");
                    $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
                });
                $("#ddlAttributeType").trigger("change");

                $("#ddlTypeValidation").bind("change", function () {
                    var attValType = $("#ddlTypeValidation").val();
                    $("#default_value_text").prop("class", "sfInputbox " + attributesManage.CreateValidationClass(attValType) + "").val('');
                    $('#iferror').hide();
                    $('#iferror').html(attributesManage.GetValidationTypeErrorMessage(attValType));
                });

                $("input[type=button].AddOption").click(function () {
                    var checkedState = false;
                    if ($(this).prop("name") == "DeleteOption") {
                        var t = $(this).closest('tr');

                        var attrId = t.find("td").find('input[type="hidden"]').val();
                        if (attrId != '0') {
                            arrAttrValueId += attrId + ',';

                        }
                        t.find("td")
                            .wrapInner("<div style='DISPLAY: block'/>")
                            .parent().find("td div")
                            .slideUp(300, function () {
                                t.remove();
                            });


                    } else if ($(this).prop("name") == "AddMore") {
                        checkedState = $('#dataTable>tbody tr:first').find('input[type="radio"]').prop("checked");
                        var cloneRow = $(this).closest('tr').clone(true);
                        $(cloneRow).find("input").each(function (i) {
                            if (this.name == "optionValueId") {
                                $(this).val('0');
                            } else if (this.name == "value") {
                                $(this).val('');
                            } else if (this.name == "position") {
                                $(this).val('');
                            } else if (this.name == "Alias") {
                                $(this).val('');
                            } else if ($(this).hasClass("class-isdefault")) {
                                this.checked = false;
                            } else if ($(this).hasClass("AddOption")) {
                                $(this).prop("name", "DeleteOption");
                                $(this).prop("value", "Delete Option");
                            }
                            $(this).parent('td').find('span').removeClass('error');
                            $(this).removeClass('error');
                        });
                        $(cloneRow).appendTo("#dataTable");
                        $('#dataTable>tbody tr:first').find('input[type="radio"]').prop("checked", checkedState);
                        $('#dataTable tr:last').hide();
                        $('#dataTable tr:last td').fadeIn('slow');
                        $('#dataTable tr:last').show();
                        $('#dataTable tr:last td').show();
                    }
                });

            }
        };
        attributesManage.init();
    });

</script>
