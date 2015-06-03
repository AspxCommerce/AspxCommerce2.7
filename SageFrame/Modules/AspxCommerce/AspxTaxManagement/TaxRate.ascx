<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxRate.ascx.cs" Inherits="Modules_AspxTaxManagement_TaxRate" %>

<script type="text/javascript">
    //<![CDATA[   
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxTaxManagement
        });
    });
    var lblTaxRateHeading='<%=lblTaxRateHeading.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divTaxRatesGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Tax Rates" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewTaxRate" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Tax Rate</span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>
                    
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonTaxRate_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
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
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Tax Rate Title:</label>
                                <input type="text" id="txtRateTitle" class="sfTextBoxSmall" />
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale ">
                                    Country:</label>
                                <select id="ddlSearchCountry" class="sfListmenu sfLocale">
                                    <option value="0">--Select Country--</option>
                                </select>
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    State/Province:</label>
                                <input type="text" id="txtSearchState" class="sfTextBoxSmall" />
                            </td>
                            <td width="150">
                                <label class="cssClassLabel sfLocale">
                                    Zip/Post Code:</label>
                                <input type="text" id="txtSearchZip" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <div class="sfButtonwrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="TaxRate.SearchTaxRate()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxTaxRateImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvTaxRateDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="TaxRateDetailsExportTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divTaxRateInformation" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTaxRateHeading" runat="server" Text="Tax Rate Information" meta:resourcekey="lblTaxRateHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
             <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
            <table cellspacing="0" cellpadding="0" border="0" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTaxRateTitle" runat="server" Text="Tax Rate Title:" CssClass="cssClassLabel"
                            meta:resourcekey="lblTaxRateTitleResource1"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTaxRateTitle" name="rateTitle" minlength="2" class="sfInputbox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="cssClassLabel"
                            meta:resourcekey="lblCountryResource1"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlCountry" class="sfListmenu required">
                            <option value="0" class="sfLocale">--Select Country--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblState" runat="server" Text="State/Province:" CssClass="cssClassLabel"
                            meta:resourcekey="lblStateResource1"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlState" class="sfListmenu">
                            <option value="0" class="sfLocale">--Select State--</option>
                        </select>
                        <input type="text" id="txtState" name="state" minlength="2" class="sfInputbox " />
                    </td>
                </tr>
                <tr id="trZipPostCode">
                    <td>
                        <asp:Label ID="lblZipPostCode" runat="server" Text="Zip/Post Code:" CssClass="cssClassLabel"
                            meta:resourcekey="lblZipPostCodeResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtZipPostCode" name="zipCode" minlength="5" class="sfInputbox  zipPostcode" />
                        <span id="errmsgZipPostCode"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsZipPostRange" runat="server" Text="Is Zip/Post Range:" CssClass="cssClassLabel"
                            meta:resourcekey="lblIsZipPostRangeResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkIsTaxZipRange" class="cssClassCheckBox" />
                    </td>
                </tr>
                <tr id="trRangeFrom">
                    <td>
                        <asp:Label ID="lblRangeFrom" runat="server" Text="Range From:" CssClass="cssClassLabel"
                            meta:resourcekey="lblRangeFromResource1"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtRangeFrom" name="rangefrom" minlength="5" class="sfInputbox required rangeFrom alphaNumberic" />
                        <span id="errmsgRangeFrom"></span>
                    </td>
                </tr>
                <tr id="trRangeTo">
                    <td>
                        <asp:Label ID="lblRangeTo" runat="server" Text="Range To:" CssClass="cssClassLabel"
                            meta:resourcekey="lblRangeToResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtRangeTo" name="rangeTo" minlength="5" class="sfInputbox required rangeTo alphaNumberic" />
                        <span id="errmsgRangeTo"></span><span class="cssClassRequired">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRateType" runat="server" Text="Rate Type:" CssClass="cssClassLabel"
                            meta:resourcekey="lblRateTypeResource1"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlTaxRateType" class="sfListmenu">
                            <%-- <option value="False">Absolute ($)</option>
                            <option value="True">Percent (%)</option>--%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTaxRateValue" runat="server" Text="Tax Rate:" CssClass="cssClassLabel"
                            meta:resourcekey="lblTaxRateValueResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtTaxRateValue" name="taxRate" class="sfInputbox required alphaNumberic" />
                        <span id="errmsgTaxRateValue"></span><span class="cssClassRequired">*</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancel" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span>
                </button>
            </p>
            <p>
                <button type="button" id="btnSaveTaxRate" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnTaxRateID" />
<asp:HiddenField ID="_csvTaxRateHdnValue" runat="server" />
