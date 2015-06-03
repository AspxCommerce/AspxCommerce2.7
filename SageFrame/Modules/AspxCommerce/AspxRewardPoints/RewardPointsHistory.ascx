<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RewardPointsHistory.ascx.cs"
    Inherits="Modules_AspxCommerce_RewardPoints_RewardPointsHistory" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxRewardPoints
        });
    });
    var RewardPointsModulePath = '<%=AspxRewardPointsModulePath%>';
    var lblRewardPointsRuleEdit = "<%=lblRewardPointsRuleEdit.ClientID%>";
    //]]>
</script>
<div class="cssClassHeader">
            <h1>
                <span class="sfLocale">Reward Points</span>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
<div id="dvEditView" class="" style="display: none;">
    <div class="cssClassCommonBox Curve">
        
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td class="cssClassheader">
                                <span class = "sfLocale">Reward Points History</span>  
                            </td>
                            <td class="cssClassheader">
                                <span class="sfLocale">Manage Settings</span> 
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="cssClasscontent">
                                <button type="button" id="btnView" class="cssClassView sfBtn">
                                    <span class="sfLocale icon-preview">View</span></button>
                            </td>
                            <td class="cssClasscontent">
                                <button type="button" id="btnEdit" class="cssClassEdit sfBtn">
                                    <span class="sfLocale icon-edit">Edit</span></button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div id="dvRewardPointsHistory" class="cssClassdvRewardPointsHistory" style="display: none;">
    <div id="gdvRewardPointsHistory_grid" class="cssClassgdvRewardPointsHistory_grid">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h3>
                    <asp:Label ID="lblHistory" runat="server" CssClass="sfLocale" Text="Reward Points History :"
                        meta:resourcekey="lblHistoryResource1"></asp:Label>
                </h3>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="sfGridwrapper">
                <div class="sfGridWrapperContent">
                    <div class="sfFormwrapper sfTableOption">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label class="sfFormlabel sfLocale">
                                        Customer Name :</label><input type="text" id="txtCustomerName" class="cssClassTextBox" />
                                </td>
                                <td>
                                    <label class="sfFormlabel sfLocale">
                                        Customer Email :</label><input type="text" id="txtEmail" class="cssClassTextBox" />
                                </td>
                                <td>
                                   
                                            <button type="button" id="btnSearchHistory" class="sfBtn">
                                                <span class="sfLocale icon-search">Search</span></button>
                                        
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table id="gdvRewardPointsHistory" width="100%" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="Div2" class="cssClassBtnBackEdit">
        <button type="button" id="btnBackHistory" class="cssClassEdit sfBtn">
            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
    </div>
</div>
<div id="dvRewardPointsHistoryByCustomer" class="cssClassdvRewardPointsHistoryByCustomer"
    style="display: none;">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h3>
                <asp:Label ID="lblBalance" runat="server" CssClass="sfLocale" Text="Balance :" meta:resourcekey="lblBalanceResource1"></asp:Label>
            </h3>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent sfFormwrapper">
                <div id="dvPoints">
                    <span id="spanTotalRewardPointsHeading" class="sfLocale cssClassSpanHeadings" style="font-weight: bold;">
                        Net Points: &nbsp;</span><span id="spanNetPoints" style="font-weight: bold;"></span>
                </div>
                <div id="dvPointsAmount">
                    <span id="spanTotalRewardAmountHeading" class="sfLocale cssClassSpanHeadings" style="font-weight: bold;">
                        Net Amount:&nbsp; </span><span id="spanNetAmount" class="cssClassFormatCurrency"
                            style="font-weight: bold;"></span>
                </div>
            </div>
        </div>
    </div><br />

    <div id="gdvRewardPointsHistoryByCustomer_grid" class="cssClassgdvRewardPointsHistoryByCustomer_grid">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h3>
                    <asp:Label ID="Label1" runat="server" CssClass="sfLocale" Text="Reward Points History"
                        meta:resourcekey="lblHistoryResource1"></asp:Label>
                </h3>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="sfGridwrapper">
                <div class="sfGridWrapperContent">
                    <div class="sfFormwrapper sfTableOption clearfix">
                      
<div class="sfFloatLeft"><label class="sfFormlabel sfLocale">Date Added:</label>
<input type="text" id="txtDateFrom" class="sfTextBoxSmall" /></div>
<div class="sfFloatLeft"><button type="button" onclick="RewardPointsHistory.SearchRewards()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                       
                                    </div>
                           
                    </div>
                    <div class="log">
                    </div>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="sfGridwrapper">
                <div class="sfGridWrapperContent">
                    <table id="gdvRewardPointsHistoryByCustomer" width="100%" border="0" cellpadding="0"
                        cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="Div4" class="cssClassBtnBackEdit">
        <button type="button" id="btnBackToHistory" class="cssClassEdit sfBtn">
            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
    </div>
</div>
<div id="dvEditRewardPoints" class="cssClassdvEditRewardPoints" style="display:none;">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h3>
                <asp:Label ID="lblGeneralSettings" runat="server" CssClass="sfLocale" Text="Manage Settings :"
                    meta:resourcekey="lblGeneralSettingsResource1"></asp:Label>
            </h3>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table id="gdvRewadPointsGeneral" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  width="30%">
                            <span class="sfFormlabel sfLocale">Enable Reward Points :</span>
                        </td>
                        <td>
                            <input type="checkbox" id="chkEnableRewardPoints" name="enableRewardPoints" class="cssClassCheckBox" />
                        </td>
                    </tr>
                    <tr class="noBg">
                        <td>
                            <div class="cssClassHeader">
                                <h3>
                                    <span class="sfFormlabel sfLocale">Exchange Rate Calculation :</span>
                                </h3>
                            </div>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="noBg">
                        <td style="text-align: right !important;">
                            <span class="sfFormlabel sfLocale">Reward Points =</span>
                        </td>
                        <td>
                            <span class="sfFormlabel sfLocale">Reward Amount</span>(<span id="spanCurrency"></span>)
                        </td>
                    </tr>
                    <tr class="noBg">
                        <td style="text-align: right !important;">
                            <input type="text" id="txtRewardPoints" name="RewardPoints" class="sfInputbox sfShortInputbox required number"
                                datatype="Integer" maxlength="5" />
                            =
                        </td>
                        <td>
                            <input type="text" id="txtRewardAmount" name="RewardAmount" class="sfInputbox sfShortInputbox required number"
                                datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="sfFormlabel sfLocale">Awarded Order Status :</span>
                        </td>
                        <td>
                            <select id="ddlOrderStatus" name="ddlOrderStatus" class="sfListmenu required" multiple="multiple">
                                <option value="0" class="sfLocale">--All--</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="sfFormlabel sfLocale">Cancelled Order Status :</span>
                        </td>
                        <td>
                            <select id="SelectOrderStatus" name="SelectOrderStatus" class="sfListmenu required"
                                multiple="multiple">
                                <option value="0" class="sfLocale">--All--</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="sfFormlabel sfLocale">Reward Points Expires in (Days) :</span>
                        </td>
                        <td>
                            <input type="text" id="txtRewardPointsExpiresInDays" name="RewardPointsExpiresInDays"
                                class="sfInputbox required number" datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="sfFormlabel sfLocale">Minimum balance (Reward Points) in order to redeem:</span>
                        </td>
                        <td>
                            <input type="text" id="txtMinRedeemBalance" name="MinRedeemBalance" class="sfInputbox required number"
                                datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="sfFormlabel sfLocale">Capped Balance (Reward Points) :</span>
                        </td>
                        <td>
                            <input type="text" id="txtCappedBalance" name="CappedBalance" class="sfInputbox required number"
                                datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <button type="button" id="btnSave" class="cssClassEdit sfBtn">
                                <span class="sfLocale icon-save">Save</span></button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="cssClassClear">
    </div>
    <br />
    <br />
    <div id="gdvRewardPointsSettings_grid" class="cssClassgdvRewardPointsSettings_grid">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h3>
                    <asp:Label ID="lblTitle" runat="server" CssClass="sfLocale" Text="Manage Reward Rules :"
                        meta:resourcekey="lblTitleResource1"></asp:Label>
                </h3>
                <div class="cssClassHeaderRight">
                    <div class="sfButtonwrapper">
                        <p>
                            <button type="button" id="btnAddRewardRule" class="cssClassButtonSubmit sfBtn">
                                <span class="sfLocale icon-addnew">Add New Reward Rule</span></button>
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
                                    <label class="sfFormlabel sfLocale">
                                        Reward Rule:</label><input type="text" id="txtRewardRule" class="cssClassTextBox" />
                                </td>
                                <td>
                                    <label class="sfFormlabel sfLocale">
                                        Is Active:</label>
                                    <select id="ddlIsActive" class="sfListmenu">
                                        <option value="" class="sfLocale">--All--</option>
                                        <option value="0" class="sfLocale">True</option>
                                        <option value="1" class="sfLocale">False</option>
                                    </select>
                                </td>
                                <td>
                                    
                                            <button type="button" id="btnSearchRewardRule" class="sfBtn">
                                                <span class="sfLocale icon-search">Search</span></button>
                                      
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="loading">
                        <img id="ajaxRewardPointsSettingImage" src="" alt="loading...." />
                    </div>
                    <div class="log">
                    </div>
                    <table id="gdvRewadPointsSetting" width="100%" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="dvBtnBackEdit" class="cssClassBtnBackEdit">
        <button type="button" id="btnBackEdit" class="cssClassEdit sfBtn">
            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
    </div>
</div>
<div id="divLoadUserControl" class="cssClasMyAccountInformation">
    <div class="cssClassMyDashBoardInformation">
    </div>
</div>
<div id="dvRewardPointsEditForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h3>
                <asp:Label ID="lblRewardPointsRuleEdit" CssClass="sfLocale" runat="server" meta:resourcekey="lblRewardPointsRuleEditResource1"></asp:Label>
            </h3>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table border="0" width="100%" id="tblRewardPointsRuleEdit" class="cssClassPadding">
                    <tr>
                        <td>
                            <asp:Label ID="lblRewardRuleNameEdit" Text="Reward Rule Name:" runat="server" CssClass="sfFormlabel"
                                meta:resourcekey="lblRewardRuleNameEditResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" id="txtRewardRuleNameEdit" name="RewardRuleNameEdit" class="sfInputbox required " />
                            <input type="hidden" id="hdnRewardRuleID" />
                            <input type="hidden" id="hdnRewardRuleIDs" />
                            <input type="hidden" id="hdnRewardRuleSettingID" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRewardRuleTypeEdit" Text="Reward Rule Type:" runat="server" CssClass="sfFormlabel"
                                meta:resourcekey="lblRewardRuleTypeEditResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <select id="ddlRewardRule" class="sfListmenu">
                                <option value="0" class="sfLocale">--All--</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRewardPointsEdit" Text="Reward Points:" runat="server" CssClass="sfFormlabel"
                                meta:resourcekey="lblRewardPointsEditResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" id="txtRewardPointsEdit" name="RewardPointsEdit" class="sfInputbox sfShortInputbox required number"
                                datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr id="trPAmount" style="display: none;">
                        <td>
                            <asp:Label ID="lblPurchaseAmountEdit" Text="On Purchase of $:" runat="server" CssClass="sfFormlabel"
                                meta:resourcekey="lblPurchaseAmountEditResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" id="txtPurchaseAmountEdit" name="PurchaseAmountEdit" class="sfInputbox sfShortInputbox required number"
                                datatype="Integer" maxlength="5" />
                        </td>
                    </tr>
                    <tr id="isActive">
                        <td>
                            <asp:Label ID="lblIsActive" Text="Is Active:" runat="server" CssClass="sfFormlabel"
                                meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="checkbox" id="chkIsActiveEdit" class="sfCheckBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <div class="sfButtonwrapper">
                                <p>
                                    <button type="button" id="btnCancelRewardPointsRuleEdit" class="sfBtn">
                                        <span class="sfLocale icon-close">Cancel</span></button>
                                </p>
                                <p>
                                    <button type="button" id="btnSubmitRewardPointsRuleEdit" class="sfBtn">
                                        <span class="sfLocale icon-save">Save</span></button>
                                </p>
                                <p id="delete">
                                    <button type="button" id="btnDeleteRewardPointsRuleEdit" class="sfBtn">
                                       <span class="sfLocale icon-delete">Delete</span></button>
                                </p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
    <input type="hidden" id="hdnRewardRuleIDView" />
    <input type="hidden" id="hdnRewardRuleName" />
</div>
