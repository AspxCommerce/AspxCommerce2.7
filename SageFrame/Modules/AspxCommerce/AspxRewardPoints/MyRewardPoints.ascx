<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyRewardPoints.ascx.cs"
    Inherits="Modules_AspxCommerce_RewardPoints_MyRewardPoints" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    var RewardPointsModulePath = '<%=AspxRewardPointsModulePath%>';
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var modulePath = RewardPointsModulePath;
    var ModuleServicePath = modulePath + 'RewardPointsHandler.ashx/';
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName(),
        CustomerID: AspxCommerce.utils.GetCustomerID()
    };
    var generalSettingsObj = {
        MinRedeemBalance: 0
    };
    var rewardPointsHistoryObj = {
        CustomerName: "",
        Email: "",
        CustomerID: "",
        DateFrom: "",
        DateTo: ""
    };
    var MyRewardPoints = '';
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxRewardPoints
        });
        MyRewardPoints = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: "",
                error: "",
                sessionValue: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: MyRewardPoints.config.type,
                    contentType: MyRewardPoints.config.contentType,
                    cache: MyRewardPoints.config.cache,
                    async: MyRewardPoints.config.async,
                    url: MyRewardPoints.config.url,
                    data: MyRewardPoints.config.data,
                    dataType: MyRewardPoints.config.dataType,
                    success: MyRewardPoints.config.ajaxCallMode,
                    error: MyRewardPoints.config.error
                });
            },
            SearchRewards: function () {
                var dateFrom = $.trim($("#txtDateFrom").val());

                if (dateFrom.length < 1) {
                    dateFrom = null;
                }

                MyRewardPoints.GetRewardPointsHistoryByCustomer(dateFrom);
            },
            Init: function () {
                $("#txtDateFrom").datepicker({ dateFormat: 'yy/mm/dd' });
                MyRewardPoints.HideAll();
                $("#dvMyRewardPoints").show();
                MyRewardPoints.GetRewardPointsHistoryByCustomer(null);
                MyRewardPoints.GetGeneralSettings();

            },
            GetRewardPointsHistoryByCustomer: function (dateFrom) {
                rewardPointsHistoryObj.CustomerID = aspxCommonObj.CustomerID;
                rewardPointsHistoryObj.DateFrom = dateFrom;

                this.config.method = "GetMyRewardPointsHistory";
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvMyRewardPointsHistory_pagesize").length > 0) ? $("#gdvMyRewardPointsHistory_pagesize :selected").text() : 10;

                $("#gdvMyRewardPointsHistory").sagegrid({
                    url: ModuleServicePath,
                    functionMethod: this.config.method,
                    colModel: [
                { display: getLocale(AspxRewardPoints, 'Row Number'), name: 'row_number', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Points Rewarded'), name: 'rewardPoints', cssclass: 'cssClassrewardPoints', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Reward Amounts'), name: 'rewardAmounts', cssclass: 'cssClassrewardAmounts', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxRewardPoints, 'Points Used'), name: 'usedRewardPoints', cssclass: 'cssClassusedRewardPoints', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Balance'), name: 'balance', cssclass: 'cssClassbalance', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxRewardPoints, 'Reward Reason'), name: 'reason', cssclass: 'cssClassreason', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Created On'), name: 'createdOn', cssclass: 'cssClasscreatedOn', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Expires On'), name: 'expiresOn', cssclass: 'cssClassexpiresOn', controlclass: '', coltype: 'label', align: 'left' },
                { display: getLocale(AspxRewardPoints, 'Total Days Added On'), name: 'totalDaysAddedOn', cssclass: 'cssClasstotalDaysAddedOn', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: getLocale(AspxRewardPoints, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False', hide: true }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxRewardPoints, "No Records Found!"),
                    param: { aspxCommonObj: aspxCommonObj, RewardPointsHistoryCommonObj: rewardPointsHistoryObj },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 4: { sorter: false }, 5: { sorter: false }, 6: { sorter: false }, 7: { sorter: false }, 8: { sorter: false }, 9: { sorter: false } }

                });
            },
            Boolean: function (str) {
                switch (str.toLowerCase()) {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    default:
                        return false;
                }
            },
            GetGeneralSettings: function () {
                this.config.method = "GetGeneralSetting";
                this.config.url = ModuleServicePath + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = MyRewardPoints.BindGeneralSettings;
                this.config.error = MyRewardPoints.GeneralSettingsError;
                this.ajaxCall(this.config);
            },
            BindGeneralSettings: function (msg) {
                var totalRewardPoints = 0;
                var totalRewardAmount = 0;
                var minRedeemBalance = 0;
                $.each(msg.d, function (index, item) {
                    totalRewardPoints = item.TotalRewardPoints;
                    totalRewardAmount = item.TotalRewardAmount;
                    minRedeemBalance = item.MinRedeemBalance;
                    $("#spanRewardPoints").html(item.RewardPoints);
                    $("#spanRewardAmounts").html(parseFloat(item.RewardExchangeRate).toFixed(2));
                    $("#spanAmountSpent").html(parseFloat(item.AmountSpent).toFixed(2));
                    $("#spanRewardPointsEarned").html(item.RewardPointsEarned);
                    $("#spanBalanceCapped").html(item.BalanceCapped);
                    $("#spanMinRedeemBalance").html(item.MinRedeemBalance);
                    $("#spanExpiredinDays").html(item.RewardPointsExpiresInDays);
                    $("#spanOrderStatus").html(item.OrderStatus);
                });
                $("#spanTotalRewardPoints").html(totalRewardPoints);
                $("#spanTotalRewardAmounts").html(parseFloat(totalRewardAmount).toFixed(2));
                if (totalRewardPoints < minRedeemBalance) {
                    $("#spanNotify").show();
                }
                var cookieCurrency = $("#ddlCurrency").val();
                Currency.currentCurrency = BaseCurrency;
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
            },
            GeneralSettingsError: function () {
                csscody.error("<h2>" + getLocale(AspxRewardPoints, "Error Message") + "</h2><p>" + getLocale(AspxRewardPoints, "Failed to load previous setting values!") + "</p>");
            },
            HideAll: function () {
                $("#dvMyRewardPoints").hide();
            }
        };

        MyRewardPoints.Init();

    });

    //]]>
</script>

<div id="dvMyRewardPoints" class="cssClassdvEditRewardPoints" style="display: none;">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblMyRewardPoints" runat="server" Text="My Reward Points" meta:resourcekey="lblMyRewardPointsResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfRewardwrapper">
            <div class="sfGridWrapperContent clearfix">
                <div id="pBalace" class="cssClasspBalance">
                    <div id="spanBalanceHeading" class="cssClassHeading cssClassBMar10"><b class="sfLocale">Your Balance :</b></div>

                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sfLocale cssClassSapanHeadings" id="spanTotalRewardPointsHeading">Total Reward Points :</td>
                            <td><span id="spanTotalRewardPoints"></span></td>
                        </tr>
                        <tr>
                            <td id="spanspanTotalRewardAmountsHeading" class="sfLocale cssClassSapanHeadings">Total Reward Amount:</td>
                            <td><span id="spanTotalRewardAmounts" class="cssClassFormatCurrency"></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cssClassF14 lightgrey"><span class="sfLocale">(Points will be added only if order status is:
                            </span><span id="spanOrderStatus" class="sfLocale" style="color: Red;"></span>)</td>
                        </tr>
                    </table>
                </div>

                <div id="pExchangeRate" class="cssClasspExchangeRate">
                    <div id="spanCurrentExchangeRate" class="cssClassHeading cssClassBMar10"><b class="sfLocale">Current Exchange Rates:</b></div>
                    <ul>
                        <li><span class="sfLocale">Each</span>
                            <span id="spanRewardPoints" class="sfLocale"><strong>0</strong></span>
                            <span class="sfLocale">reward points can be redeemed for</span>

                            <span id="spanRewardAmounts" class="cssClassFormatCurrency"><strong>0</strong></span></li>

                        <li><span class="sfLocale">Each</span>
                            <span id="spanAmountSpent" class="cssClassFormatCurrency"><strong>0</strong></span>
                            <span class="sfLocale">spent will earn</span>
                            <span id="spanRewardPointsEarned"><strong>0</strong></span>
                            <span class="sfLocale">Reward points.</span></li>
                    </ul>
                </div>
                <div id="pBalanceLimitations" class="cssClasspBalanceLimitations">
                    <div id="spanBalanceLimitations" class="cssClassHeading cssClassBMar10"><b class="sfLocale">Balance limitations :</b></div>
                    <ul>
                        <li><span class="sfLocale">Balance is capped at</span>
                            <span id="spanBalanceCapped"><strong>0</strong></span>
                            <span class="sfLocale">Reward points.</span></li>

                        <li><span class="sfLocale">Minimum Balance in order to redeem:</span>
                            <span id="spanMinRedeemBalance"><strong>0</strong></span>
                            <span class="sfLocale">Reward points.</span></li>

                        <li id="spanNotify" class="sfLocale" style="display: none;">Minimum balance for redeeming points is not reached.</li>

                        <li><span class="sfLocale">Each earned reward points record expires in</span>
                            <span id="spanExpiredinDays"><strong>0</strong></span>
                            <span class="sfLocale">days.</span></li>
                    </ul>
                </div>

            </div>
        </div>
    </div>
    <div id="gdvRewardPointsSettings_grid" class="cssClassgdvRewardPointsSettings_grid">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h2>
                    <asp:Label ID="lblTitle" class="sfLocale" runat="server" Text="My Reward Points History" meta:resourcekey="lblTitleResource1"></asp:Label>
                </h2>
            </div>

            <div class="sfGridWrapperContent">
                <div class="cssClassSearchPanel sfFormwrapper clearfix">

                    <div class="sfLocale sfCol_14">
                        Date Added:
                    </div>
                    <div class="sfCol_69">
                        <input type="text" id="txtDateFrom" class="sfTextBoxSmall" />
                    </div>
                    <div class="sfButtonwrapper sfCol_17">
                        <label class="cssClassOrangeBtn i-search">
                            <button type="button" onclick="MyRewardPoints.SearchRewards()">
                                <span class="sfLocale">Search</span></button></label>
                    </div>

                </div>
                <div class="log">
                </div>

            </div>

            <div class="sfGridwrapper">
                <div class="sfGridWrapperContent">
                    <table id="gdvMyRewardPointsHistory" width="100%" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
