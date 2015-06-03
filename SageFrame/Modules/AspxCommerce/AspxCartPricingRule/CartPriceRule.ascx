<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CartPriceRule.ascx.cs"
    Inherits="Modules_AspxCartPricingRule_CartPriceRule" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCartPricingRule
        });
    }); var umi = '<%=UserModuleId%>';
    //]]>
</script>

<div class="cssClassTabMenu" id="cartPricingRuleTabMenu" style="display: none">
    <div id="divCartPricingRuleForm" class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCartPricingRuleFormHeading" runat="server"
                    Text="Shopping Cart Price Rules"
                    meta:resourcekey="lblCartPricingRuleFormHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div id="placeholder-templates">
        </div>
        <div class="cssClassTabPanelTable">
            <div id="CartPriceRule-TabContainer" class="cssClassTabpanelContent">
                <ul>
                    <li><a href="#CartPriceRule-1"><span id="lblRuleInformation" class="sfLocale">Cart Rule Information</span>
                    </a></li>
                    <li class="ui-tabs"><a href="#CartPriceRule-2"><span id="lblCondition" class="sfLocale">Condition</span>
                    </a></li>
                    <li><a href="#CartPriceRule-3"><span id="lblAction" class="sfLocale">Action</span></a></li>
                </ul>
                <div id="CartPriceRule-1" class="sfFormwrapper">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="10%">
                                <span id="CartPriceRule-lblName" class="cssClassLabel sfLocale">Rule Name:</span>
                            </td>
                            <td class="cssClassTableRightCol">
                                <input type="text" id="CartPriceRule-txtName" name="RuleName" class="sfInputbox  required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblDescription" class="cssClassLabel sfLocale">Description:</span>
                            </td>
                            <td>
                                <textarea id="CartPriceRule-txtDescription" rows="4" cols="80" name="Description"
                                    class="cssClassTextArea"></textarea>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                <span id="CartPriceRule-lblStores" class="cssClassLabel sfLocale">Stores:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-mulStores" multiple="multiple" name="Stores" class="cssClassMultiSelect required">
                                </select>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblRoles" class="cssClassLabel sfLocale">Roles:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-mulRoles" multiple="multiple" name="Roles" class="cssClassMultiSelect required">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblFromDate" class="cssClassLabel sfLocale">From:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtFromDate" name="FromDate" class="from sfInputbox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblToDate" class="cssClassLabel sfLocale">To:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtToDate" name="ToDate" class="to sfInputbox required" />
                                <span id="created" style="color: #ED1C24;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblPriority" class="cssClassLabel sfLocale">Priority:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtPriority" name="Priority" class="sfInputbox required" maxlength="2" />
                                <span id="spanPriority" style="color: Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblIsActive" class="cssClassLabel sfLocale">Active:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkIsActive" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="CartPriceRule-2">
                    <div class="cssClassFieldSet">
                        <h2 class="sfLocale">Conditions (leave blank for all products)</h2>
                        <div class="cssClassFieldSetContent">
                            <span class="cssClassOnClick sfLocale">IF
                                <input type="hidden" name="type_0" id="type_0" title="type" value="combination" />
                                <input type="hidden" name="pricingRuleID" id="pricingRuleID" value="0" />
                                <a class="cssClassFieldSetLabel sfLocale" href="#" onclick="cartPriceRuleFormat.Edit(this)">ALL</a> <span class="cssClassElement">
                                    <select name="aggregator_0" id="aggregator_0" class="element-value-changer select"
                                        onblur="cartPriceRuleFormat.GetDropdownValue(this)" onchange="cartPriceRuleFormat.GetDropdownValue(this)">
                                        <option value="ALL" selected="selected" class="sfLocale">ALL</option>
                                        <option value="ANY" class="sfLocale">ANY</option>
                                    </select>
                                </span></span>&nbsp; <span class="sfLocale">of these conditions are</span> <span class="cssClassOnClick"><a class="cssClassFieldSetLabel sfLocale"
                                    onclick="cartPriceRuleFormat.Edit(this)">TRUE</a><span class="cssClassElement">
                                        <select name="value_0" id="value_0" title="value" class="element-value-changer select"
                                            onblur="cartPriceRuleFormat.GetDropdownValue(this)" onchange="cartPriceRuleFormat.GetDropdownValue(this)">
                                            <option value="TRUE" selected="selected" class="sfLocale">TRUE</option>
                                            <option value="FALSE" class="sfLocale">FALSE</option>
                                        </select>
                                    </span></span>&nbsp;
                            <ul class="cssClassOnClickChildren" id="">
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="CartPriceRule-3" class="sfFormwrapper">
                    <table border="0" cellpadding="0" cellspacing="0" class="tdbordernone">
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblApply" class="cssClassLabel sfLocale">Apply:</span>
                            </td>
                            <td class="cssClassTableRightCol">
                                <select id="CartPriceRule-cboApply" class="sfListmenu">
                                    <option value="1" class="sfLocale">Percent of product price discount</option>
                                    <option value="2" class="sfLocale">Fixed amount discount</option>
                                    <option value="3" class="sfLocale">Fixed amount discount for whole cart</option>
                                    <option value="4" class="sfLocale">Buy X get Y free (discount Qty[Y] is Value)</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblValue" class="cssClassLabel sfLocale">Value:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtValue" name="Value" class="sfInputbox required" />
                                <span id="percError" style="color: #ED1C24;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblDiscountQuantity" class="cssClassLabel sfLocale">Maximum Qty Discount is Applied To:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtDiscountQuantity" name="MaximumQtyDiscountAppliedTo"
                                    class="sfInputbox" />
                            </td>
                        </tr>
                        <tr style="display: none;" id="trBuyX">
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblDiscountStep" class="cssClassLabel sfLocale">Discount Qty Step (Buy X):</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtDiscountStep" name="DiscountQtyStep" class="sfInputbox" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblApplytoShippingAmount" class="cssClassLabel sfLocale">Apply To Shipping Amount:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkApplytoShippingAmount" class="cssClassCheckBox" />
                            </td>
                        </tr>
                        <tr style="display: none;" id="trApplytoShipping">
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblFreeShipping" class="cssClassLabel sfLocale">Shipping Discount:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-cboFreeShipping" class="sfListmenu">
                                    <option value="0" selected="selected" class="sfLocale">By Percentage Off</option>
                                    <option value="1" class="sfLocale">By Fixed Amount</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblFurtherRuleProcessing" class="cssClassLabel sfLocale">Further Rule Processing:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkFurtherRuleProcessing" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="sfButtonwrapper">
                <p>
                    <button type="submit" id="btnSaveCartPricingRule" class="sfBtn">
                        <span class="sfLocale icon-save">Save</span></button>
                </p>
                <p>
                    <button type="button" id="btnCancelCartPricingRule" class="sfBtn">
                        <span class="sfLocale icon-close">Cancel</span></button>
                </p>
                <p>
                    <button type="button" id="btnResetCartPricingRule" class="sfBtn">
                        <span class="sfLocale icon-refresh">Reset</span></button>
                </p>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
<div id="cartPricingRuleGrid" class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h1>
            <asp:Label ID="lblCartPriceRulesGridHeading" runat="server"
                Text="Shopping Cart Price Rules"
                meta:resourcekey="lblCartPriceRulesGridHeadingResource1"></asp:Label>
        </h1>
        <div class="cssClassHeaderRight">
            <div class="sfButtonwrapper">
                <p>
                    <button type="button" id="btnAddCartPricingRule" class="sfBtn">
                        <span class="sfLocale icon-addnew">Add Pricing Rule</span></button>
                </p>
                <p>
                    <button type="button" id="btnDeleteCartRules" class="sfBtn">
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
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <label class="cssClassLabel sfLocale">
                                Rule Name:</label><br />

                            <input type="text" id="txtCartPriceRuleSrc" class="sfTextBoxSmall" />
                        </td>
                        <td width="90">
                            <label class="cssClassLabel sfLocale">
                                From:</label>
                            <input type="text" id="txtCartPricingRuleStartDate" class="sfTextBoxSmall" />
                        </td>
                        <td width="90">
                            <label class="cssClassLabel sfLocale">
                                To:</label>
                            <input type="text" id="txtCartPricingRuleEndDate" class="sfTextBoxSmall" />
                        </td>
                        <td width="90">
                            <label class="cssClassLabel sfLocale">
                                Status</label>
                            <select id="ddlCartPricingRuleIsActive" class="sfSelect">
                                <option value="" class="sfLocale">-- All --</option>
                                <option value="True" class="sfLocale">Active</option>
                                <option value="False" class="sfLocale">Inactive</option>
                            </select>
                        </td>
                        <td class="cssClassNone">
                            <br />
                            <button type="button" onclick="cartPriceRuleFormat.SearchCartPricingRule()" class="sfBtn">
                                <span class="sfLocale icon-search">Search</span></button>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="loading">
                <img id="ajaxCartPriceImageLoad" src="" class="sfLocale" alt="loading...." />
            </div>
            <div class="log">
            </div>
            <table id="gdvCartPricingRules" width="100%">
            </table>
        </div>
    </div>
</div>
