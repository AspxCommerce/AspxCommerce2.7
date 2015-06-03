<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Currencyconversion.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCurrencyConverter_Currencyconversion" %>
<div class="cssClassCurrencySelect">
    <asp:Literal ID="litCurrency"
        runat="server" EnableViewState="false"></asp:Literal>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        var cookieCurrency = Currency.cookie.read();
        if (cookieCurrency == null || cookieCurrency == "") {
            cookieCurrency = BaseCurrency;
        }
        $("#ddlCurrency").val(cookieCurrency).prop('selected', true);
        MakeFancyDropDown();
        $('#ddlCurrency').change(function () {       
            Currency.format = 'money_format';
            var newCurrency = $("#ddlCurrency").val();
            Currency.convertAll(BaseCurrency, newCurrency);
        });
    });
    //]]>
</script>
