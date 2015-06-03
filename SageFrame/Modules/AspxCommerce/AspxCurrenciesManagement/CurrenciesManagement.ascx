<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeFile="CurrenciesManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCurrenciesManagement_CurrenciesManagement" %>
<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
        moduleKey: AspxCurrenciesManagement
        });
    });
    var maxFileSize = '<%=maxFileSize%>';  
    var umi = '<%=UserModuleId%>';
    var hdnInputID = '<%=hdnCurrencyCode.ClientID%>';
</script>
<div id="divCurrencyManage">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCurrencyManageHeading" runat="server" 
                    Text="Manage Currencies" meta:resourcekey="lblCurrencyManageHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewCurrency" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Currency</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnRealTimeUpdate" class="sfBtn">
                            <span class="sfLocale icon-update">Real Time Update</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxImageLoad" />
                </div>
                <div class="log">
                </div>
                <table id="tblCurrencyManage" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<br />
<div id="divCurrencyForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span id="_lblEditCurrencyName" cssclass="cssClassLabel"></span>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table id="tblEditCurrencyManage" border="0" class="cssClassPadding tdpadding" width="100%">
                 <tr>
                    <td>
                        <asp:Label ID="lblCountryName" runat="server" CssClass="cssClassLabel" 
                            Text="Country:" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                       <asp:Literal ID="ltrCountry" runat="server" EnableViewState="False" 
                            meta:resourcekey="ltrCountryResource1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblRegion" runat="server" CssClass="cssClassLabel" Text="Region" 
                            meta:resourcekey="lblRegionResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtRegion" class="sfInputbox required" name="region" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCurrencyName" runat="server" CssClass="cssClassLabel" 
                            Text="Currency Name:" meta:resourcekey="lblCurrencyNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtCurrencyName" class="sfInputbox required" name="currencyName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCurrencyCode" runat="server" CssClass="cssClassLabel" 
                            Text="Currency Code:" meta:resourcekey="lblCurrencyCodeResource1"></asp:Label>
                        <input type="hidden" class="cssClassFormatCurrency" value="0"/>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="currencyCode" class="sfListmenu">
                            <option value="0">- Select One -</option>
                            <option value="AED">AED</option>
                            <option value="AFN">AFN</option>
                            <option value="ALL">ALL</option>
                            <option value="AMD">AMD</option>
                            <option value="ANG">ANG</option>
                            <option value="AOA">AOA</option>
                            <option value="ARS">ARS</option>
                            <option value="ATS">ATS</option>
                            <option value="AUD">AUD</option>
                            <option value="AWG">AWG</option>
                            <option value="AZN">AZN</option>
                            <option value="BAM">BAM</option>
                            <option value="BBD">BBD</option>
                            <option value="BDT">BDT</option>
                            <option value="BEF">BEF</option>
                            <option value="BGN">BGN</option>
                            <option value="BHD">BHD</option>
                            <option value="BIF">BIF</option>
                            <option value="BMD">BMD</option>
                            <option value="BND">BND</option>
                            <option value="BOB">BOB</option>
                            <option value="BRL">BRL</option>
                            <option value="BSD">BSD</option>
                            <option value="BTC">BTC</option>
                            <option value="BTN">BTN</option>
                            <option value="BWP">BWP</option>
                            <option value="BYR">BYR</option>
                            <option value="BZD">BZD</option>
                            <option value="CAD">CAD</option>
                            <option value="CDF">CDF</option>
                            <option value="CHF">CHF</option>
                            <option value="CLF">CLF</option>
                            <option value="CLP">CLP</option>
                            <option value="CNH">CNH</option>
                            <option value="CNY">CNY</option>
                            <option value="COP">COP</option>
                            <option value="CRC">CRC</option>
                            <option value="CUP">CUP</option>
                            <option value="CVE">CVE</option>
                            <option value="CYP">CYP</option>
                            <option value="CZK">CZK</option>
                            <option value="DEM">DEM</option>
                            <option value="DJF">DJF</option>
                            <option value="DKK">DKK</option>
                            <option value="DOP">DOP</option>
                            <option value="DZD">DZD</option>
                            <option value="DZD">DZD</option>
                            <option value="EGP">EGP</option>
                            <option value="ESP">ESP</option>
                            <option value="ETB">ETB</option>
                            <option value="EUR">EUR</option>
                            <option value="FIM">FIM</option>
                            <option value="FJD">FJD</option>
                            <option value="FKP">FKP</option>
                            <option value="FRF">FRF</option>
                            <option value="GBP">GBP</option>
                            <option value="GEL">GEL</option>
                            <option value="GHS">GHS</option>
                            <option value="GIP">GIP</option>
                            <option value="GMD">GMD</option>
                            <option value="GNF">GNF</option>
                            <option value="GRD">GRD</option>
                            <option value="GTQ">GTQ</option>
                            <option value="GYD">GYD</option>
                            <option value="HKD">HKD</option>
                            <option value="HNL">HNL</option>
                            <option value="HRK">HRK</option>
                            <option value="HTG">HTG</option>
                            <option value="HUF">HUF</option>
                            <option value="IDR">IDR</option>
                            <option value="IEP">IEP</option>
                            <option value="ILS">ILS</option>
                            <option value="INR">INR</option>
                            <option value="IQD">IQD</option>
                            <option value="IRR">IRR</option>
                            <option value="ISK">ISK</option>
                            <option value="ITL">ITL</option>
                            <option value="JEP">JEP</option>
                            <option value="JMD">JMD</option>
                            <option value="JOD">JOD</option>
                            <option value="JPY">JPY</option>
                            <option value="KES">KES</option>
                            <option value="KGS">KGS</option>
                            <option value="KHR">KHR</option>
                            <option value="KMF">KMF</option>
                            <option value="KPW">KPW</option>
                            <option value="KRW">KRW</option>
                            <option value="KWD">KWD</option>
                            <option value="KYD">KYD</option>
                            <option value="KZT">KZT</option>
                            <option value="LAK">LAK</option>
                            <option value="LBP">LBP</option>
                            <option value="LKR">LKR</option>
                            <option value="LRD">LRD</option>
                            <option value="LSL">LSL</option>
                            <option value="LTL">LTL</option>
                            <option value="LUF">LUF</option>
                            <option value="LVL">LVL</option>
                            <option value="LYD">LYD</option>
                            <option value="MAD">MAD</option>
                            <option value="MCF">MCF</option>
                            <option value="MDL">MDL</option>
                            <option value="MGA">MGA</option>
                            <option value="MKD">MKD</option>
                            <option value="MMK">MMK</option>
                            <option value="MNT">MNT</option>
                            <option value="MOP">MOP</option>
                            <option value="MRO">MRO</option>
                            <option value="MTL">MTL</option>
                            <option value="MUR">MUR</option>
                            <option value="MVR">MVR</option>
                            <option value="MWK">MWK</option>
                            <option value="MXN">MXN</option>
                            <option value="MYR">MYR</option>
                            <option value="MZN">MZN</option>
                            <option value="NAD">NAD</option>
                            <option value="NGN">NGN</option>
                            <option value="NIO">NIO</option>
                            <option value="NLG">NLG</option>
                            <option value="NOK">NOK</option>
                            <option value="NPR">NPR</option>
                            <option value="NZD">NZD</option>
                            <option value="OMR">OMR</option>
                            <option value="PAB">PAB</option>
                            <option value="PEN">PEN</option>
                            <option value="PGK">PGK</option>
                            <option value="PHP">PHP</option>
                            <option value="PKR">PKR</option>
                            <option value="PLN">PLN</option>
                            <option value="PTE">PTE</option>
                            <option value="PYG">PYG</option>
                            <option value="QAR">QAR</option>
                            <option value="RON">RON</option>
                            <option value="RSD">RSD</option>
                            <option value="RUB">RUB</option>
                            <option value="RWF">RWF</option>
                            <option value="SAR">SAR</option>
                            <option value="SBD">SBD</option>
                            <option value="SCR">SCR</option>
                            <option value="SDG">SDG</option>
                            <option value="SEK">SEK</option>
                            <option value="SGD">SGD</option>
                            <option value="SHP">SHP</option>
                            <option value="SIT">SIT</option>
                            <option value="SKK">SKK</option>
                            <option value="SLL">SLL</option>
                            <option value="SML">SML</option>
                            <option value="SOS">SOS</option>
                            <option value="SRD">SRD</option>
                            <option value="STD">STD</option>
                            <option value="SVC">SVC</option>
                            <option value="SYP">SYP</option>
                            <option value="SZL">SZL</option>
                            <option value="THB">THB</option>
                            <option value="TJS">TJS</option>
                            <option value="TMT">TMT</option>
                            <option value="TND">TND</option>
                            <option value="TOP">TOP</option>
                            <option value="TRY">TRY</option>
                            <option value="TTD">TTD</option>
                            <option value="TWD">TWD</option>
                            <option value="TZS">TZS</option>
                            <option value="UAH">UAH</option>
                            <option value="UGX">UGX</option>
                            <option value="USD" selected="selected">USD</option>
                            <option value="UYU">UYU</option>
                            <option value="UZS">UZS</option>
                            <option value="VAL">VAL</option>
                            <option value="VEB">VEB</option>
                            <option value="VEF">VEF</option>
                            <option value="VND">VND</option>
                            <option value="VUV">VUV</option>
                            <option value="WST">WST</option>
                            <option value="XAF">XAF</option>
                            <option value="XAG">XAG</option>
                            <option value="XAU">XAU</option>
                            <option value="XCD">XCD</option>
                            <option value="XCP">XCP</option>
                            <option value="XDR">XDR</option>
                            <option value="XOF">XOF</option>
                            <option value="XPD">XPD</option>
                            <option value="XPF">XPF</option>
                            <option value="XPT">XPT</option>
                            <option value="YER">YER</option>
                            <option value="ZAR">ZAR</option>
                            <option value="ZMK">ZMK</option>
                            <option value="ZMW">ZMW</option>
                            <option value="ZWL">ZWL</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCurrencySymbol" runat="server" CssClass="cssClassLabel" 
                            Text="Currency Symbol:" meta:resourcekey="lblCurrencySymbolResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtCurrencySymbol" class="sfInputbox required" name="currencySymbol" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblConversionRate" runat="server" CssClass="cssClassLabel" 
                            Text="Rate" meta:resourcekey="lblConversionRateResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtConversionRate" class="sfInputbox required" name="conversionRate" datatype="float" maxlength="6"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDisplayOrder" runat="server" CssClass="cssClassLabel" 
                            Text="Display Order" meta:resourcekey="lblDisplayOrderResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtDisplayOrder" name="displayOrder" class="sfInputbox required digits displayOrder"
                            minlength="1" />
                        <span id="errdisplayOrder"></span><span id="erruniqueOrder"></span>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="lblIsPrimaryForStore" runat="server" Text="Default:"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsPrimaryForStore" class="sfListmenu">
                            <option value="0">NO</option>
                            <option value="1">YES</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsBaseCurrency" runat="server" Text="Is Base Currency:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsFeatured" class="sfListmenu">
                            <option value="0">NO</option>
                            <option value="1">YES</option>
                        </select>
                    </td>
                </tr>--%>
                 <tr>
                    <td>
                        <asp:Label ID="lblIsActive" runat="server" Text="Active?" 
                            CssClass="cssClassLabel" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="cbIsActive" name="isActive" checked="checked" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="lblCurrencyImgUrl" runat="server" CssClass="cssClassLabel" 
                            Text="Currency Flag" meta:resourcekey="lblCurrencyImgUrlResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol" style="height: 100px">
                        <asp:Literal ID="ltrCountryFlag" runat="server" EnableViewState="False" 
                            meta:resourcekey="ltrCountryFlagResource1"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancel" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p>
          <%--  <p>
                <button type="button" id="btnSave">
                    <span><span class="sfLocale">Save</span></span></button>
            </p>--%>
             <asp:HiddenField ID="hdnCurrencyCode" runat="server" Value=""/>
             <button type="button" id="btnSave" class="cssClassButtonSubmit sfBtn" data-currencycode=""
                             value="Save">Save</button>

        </div>
    </div>
</div>
