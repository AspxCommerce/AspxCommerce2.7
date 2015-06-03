<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingRateEstimate.ascx.cs"
    Inherits="ShippingRateEstimate" %>

<div id="dvEstimateRate" class="sfFormwrapper" style="display: none;">

    <div class="toogleWrapper">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHead" runat="server" Text="Estimate Shipping Rate" meta:resourcekey="lblHeadResource1"></asp:Label>
            </h2>
        </div>
        <div class="cssClassRatingEst clearfix">
            <p><strong><span class="sfLocale">Enter your destination to get a shipping estimate.</span></strong></p>
            <div class="sfGridTableWrapper">
                <div>
                    <label>
                        <asp:Label ID="lblCountry" runat="server" CssClass="sfLabel" Text="Choose Country:"
                            meta:resourcekey="lblCountryResource1"></asp:Label>
                    </label>
                    <div>
                        <select id="ddlcountry" class="sfListmenu">
                            <asp:Literal ID="ddlCountry" runat="server"></asp:Literal>
                        </select>
                    </div>
                </div>
                <div id="state" style="display: none;">
                    <label>
                        <asp:Label ID="lblState" runat="server" CssClass="sfLabel" Text="State:" meta:resourcekey="lblStateResource1"></asp:Label>
                    </label>
                    <div>
                        <select id="ddlState" class="sfListmenu">
                        </select>
                        <input type="text" id="txtState" name="state" visible="false" class="sfInputbox" />
                    </div>
                </div>
                <div id="city">
                    <label>
                        <asp:Label ID="lblCity" runat="server" CssClass="sfLabel" class="sfInputbox" Text="City:"
                            meta:resourcekey="lblCityResource1"></asp:Label>
                    </label>
                    <div>
                        <input type="text" id="txtCity" name="city" class="sfInputbox" />
                    </div>
                </div>
                <div id="postalCode" style="display: none;">
                    <label>
                        <asp:Label ID="lblPostalCode" runat="server" CssClass="sfLabel" Text="Postal Code:"
                            meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                    </label>
                    <div>
                        <input type="text" id="txtPostalCode" name="postalCode" class="sfInputbox required" />
                    </div>
                </div>
            </div>
            <div class="sfButtonwrapper">
                <label class="i-estimate cssClassOrangeBtn">
                    <button type="button" id="btnCalculateRate">
                        <span class="sfLocale">Get Estimate Rate</span></button>
                </label>
            </div>
        </div>
        <div id="result" style="display: none;">
            <div class="cssClassCommonBox Curve">
                <div class="sfFormwrapper">
                    <table id="gdvRateDetail" cellspacing="0" cellpadding="0" border="0" width="100%" class="sfGridTableWrapper">
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var showShippingRateCalculator = '<%=ShowRateEstimate %>';
    var count = parseInt('<%=Count %>');
    var postalCountry = ["AU", "AT", "BE", "BR", "CA", "CN", "DK", "FI", "GQ", "FR", "DE", "GR", "IN", "ID", "IT", "JP", "LY", "LU", "MY", "MX", "NL", "NO", "PH", "PT", "RU", "SG", "ZA", "KR", "SE", "ES", "CH", "SY", "TH", "TR", "TM", "UK"];
    var dimentionalUnit = "<%=DimentionalUnit%>";
    var weightUnit = "<%=WeightUnit%>";
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxShippingRateEstimate
        });
        
    });


</script>
