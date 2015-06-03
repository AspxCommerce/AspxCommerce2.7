<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CostVariantOptions.ascx.cs"
    Inherits="Modules_AspxCostVariantOptionsManagement_CostVariantOptions" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxCostVariantOptionsManagement
        });
    });
    //<![CDATA[
    var lblCostVarFormHeading = '<%=lblCostVarFormHeading.ClientID%>';
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<!-- Grid -->
<div id="divShowOptionDetails">
    <div class="cssClassCommonBox Curve">
        <input type="hidden" value="" id="existingorders" />
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCostVarGridHeading" runat="server" Text="Variant Options" meta:resourcekey="lblCostVarGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewVariantOption" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Cost Variant Option</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>                    
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td >
                                <label class="cssClassLabel sfLocale">
                                    Cost Variant Name:</label>
                                <input type="text" id="txtVariantName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                        <button type="button" id="btnSearchCostVariants" class="sfBtn" >
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxLoad" src="" class="sfLocale" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCostVariantGrid" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<!-- form -->
<div id="divAddNewOptions" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1><asp:Label ID="lblCostVarFormHeading" runat="server" meta:resourcekey="lblCostVarFormHeadingResource1"></asp:Label></h1>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7" class="cssClassMargin">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Cost Variant Option Properties"
                            meta:resourcekey="lblTabTitle1Resource1"></asp:Label>
                    </a></li>
                    <%-- <li><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Frontend Properties"></asp:Label>
                    </a></li>--%>
                    <li><a href="#fragment-3">
                        <asp:Label ID="lblTabTitle3" runat="server" Text="Variants Properties" meta:resourcekey="lblTabTitle3Resource1"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                      <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblTab1Info" runat="server" Text="General Information" meta:resourcekey="lblTab1InfoResource1"></asp:Label>
                        </h2>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tdpadding">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCostVariantName" runat="server" Text="Cost Variant Name:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblCostVariantNameResource1"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="text" id="txtCostVariantName" class="sfInputbox required" />
                                    <span class="cssClassRight">
                                        <img class="cssClassSuccessImg" alt="Right"></span> <b class="cssClassError sfLocale">
                                            Ops! found something error, must be unique with no spaces</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCostVariantDescription" runat="server" Text="Description:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblCostVariantDescriptionResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <textarea id="txtDescription" name="txtDescription" title="Cost Variant Description"
                                        rows="2" cols="15" class="cssClassTextArea"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text="Type:" CssClass="cssClassLabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="ddlAttributeType" class="sfListmenu" name="" title="Cost Variant Input Type">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblDisplayOrderResource1"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input class="sfInputbox required" id="txtDisplayOrder" type="text" /><span id="dispalyOrder"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblActive" runat="server" Text="Active:" CssClass="cssClassLabel"
                                        meta:resourcekey="lblActiveResource1"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkActive" class="cssClassCheckBox" disabled="disabled" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="fragment-3">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblTab3Info" runat="server" Text="Cost Variants Settings" meta:resourcekey="lblTab3InfoResource1"></asp:Label>
                        </h2>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent cssClassPadding">
                                <table width="100%" cellspacing="0" cellpadding="0" id="tblVariantTable" class="tdpadding">
                                    <thead>
                                        <tr class="cssClassHeading">
                                            <th align="left" class="sfLocale">
                                                Pos.
                                            </th>
                                            <th align="left" class="sfLocale">
                                                Name
                                            </th>
                                            <th align="left" class="sfLocale">
                                                Status
                                            </th>
                                            <th align="left">&nbsp;
                                                
                                            </th>
                                             <th align="left">&nbsp;
                                                
                                            </th>
                                            <th align="left">&nbsp;
                                                
                                            </th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <input type="hidden" class="cssClassVariantValue" value="0" />
                                            <input type="text" size="3" id="txtPos" class="cssClassDisplayOrder" disabled="disabled" />
                                        </td>
                                        <td>
                                            <input type="text" class="cssClassVariantValueName" />
                                        </td>
                                        <td>
                                            <select class="cssClassIsActive">
                                                <option value="true" class="sfLocale">Active</option>
                                                <option value="false" class="sfLocale">Disabled</option>
                                            </select>
                                        </td>
                                        <td>                                        
                                            <a class="icon-addnew cssClassAddRow"></a>                                            
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span>
                </button>
            </p>
            <p>
                <button type="button" id="btnReset" class="sfBtn">
                    <span class="sfLocale icon-refresh">Reset</span>
                </button>
            </p>
            <p>
                <button type="button" id="btnSaveVariantOption" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
            <%--<p>
                <button type="button" class="delbutton" class="sfBtn">
                    <span class="sfLocale icon-delete">Delete</span>
                </button>
            </p>--%>
        </div>
    </div>
</div>
<!-- End form -->
