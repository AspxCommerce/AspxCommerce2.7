<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RewardPoint.ascx.cs" Inherits="Modules_AspxCommerce_AspxRewardPoints_RewardPoint" %>



  <div id="dvRewardPointsMain" class="cssClassdvRewardPointsMain" style="display:none">
     
        <div id="dvRPMainHeading" class="cssClassHeader">
            <h2 class="sfLocale">Reward Points : </h2></div>
<div class="cssClassRewardWrap">
        <div id="dvRPBalance" class="cssClassdvRPBalance">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="noborder">
                <tr>
                    <td colspan="2">
                        <b class="sfLocale">Current Balance : </b>
                    </td>
                </tr>
                <tr class="sfEven">
                    <td class="sfLocale">
                        Total Reward Points :
                    </td>
                    <td>
                        <span id="spanTotalRP" class="cssClassspanTotalRP">0</span>
                    </td>
                </tr>
                <tr class="sfOdd">
                    <td class="sfLocale">
                        Total Reward Amount :
                    </td>
                    <td>
                        <span id="spanTotalRA" class="cssClassspanTotalRA">0.00</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       <b><span class="sfLocale">(Rate: </span></b><span id="spanRPExchangePoints" class="cssClassspanRPExchangePoints">0</span>
                        <span class="sfLocale">Points =  </span><span id="spanRPExchangeAmount" class="cssClassspanRPExchangeAmount ">
                            0.00</span><b>)</b>
                    </td>
                </tr>
                <tr class="sfOdd">
                    <td colspan="2">
                      
                        <b class="sfLocale">Choose Number of Points to Redeem</b>

                       
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="divRange" style="display: none">
                            <div id="slider-range">
                            </div>
                          <div class="cssClassTMar10"><span style="font-size:14px;" class="sfLocale">Reward Points Range:</span> <span id="amount"></span> </div>
                        </div>
                    </td>
                    <tr>
                    <td><div id="dvUsePoints" class="sfBtn" style="display: none">
                            <label class="cssClassOrangeBtn i-arrow-right">
                            
                          <button type="button" id="btnUsePoints" class="sfBtn">
                               <span class="sfLocale">Go</span></button></label>
                        </div></td>
                    </tr>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        <input type="checkbox" id="chkUseRewardPoints" class="cssClasschkUseRewardPoints" />
                        <span class="sfLocale">Use Maximum</span>
                    </td>  
                </tr>
            </table>
        </div>
 
        <div id="dvRPCurrent" class="cssClassdvRPCurrent" style="display: none;">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="noborder">
                <tbody>
                    <tr>
                        <td colspan="2">
                            <span id="spanHeading" class="cssClassspanHeading"><b class="sfLocale">You will be awarded...</b>
                            </span>
               
                        </td>
                    </tr>
                    <tr>
                        <td class="cssClassTDDetails" colspan="2">
                        <%--    <ul id="ulRewardDetails">
                            </ul>
                        </td>
                        <td class="cssClassTDDetails" style="font-weight: bold; text-align: right;">
                            <ul id="ulRewardSub">
                            </ul>--%>

                            <table id="tblRewardDetail" width="100%" cellspacing="0" cellpadding="0" border="0" class="noborder">

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                          <b class="sfLocale">Total :</b>
                        </td>
                        <td class="cssClassTDDetails" style="font-weight: bold; text-align: right;">
                            <div id="dvPointsTotal" class="cssClassdvPointsTotal">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">  
                            <span class="cssClassspanHeading"><b class="sfLocale">Note: </b>&nbsp;<span class="sfLocale">You can use these points for your next purchase.</span>
                                </span>
                            <br />
                            <span class="cssClassspanHeading sfLocale">Your rewarded points should be minimum</span>&nbsp<b><span
                                id="spanCapped">0</span></b>&nbsp;<span class="sfLocale">to add in your account</span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        </div>
    </div>

<script type="text/javascript" >
    var RewardedPoint = "";
    var GeneralSettings = '<%=GeneralSettings %>';
    $(function () {

        var RewardPoints = function () {
            var rewardpoint = parseFloat('<%=rewardpoint%>');
            var isRewardPointEnbled = false;
            var rewardAmounTemp = "";
            var maxRewardPoints = 0;
            var minRewardPoints = 0;
            var isPurchaseActive = false;

            var $ajaxCall = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: '<%=servicePath%>' + '/RewardPointsHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            var $securepost = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST",
                    //contentType: "application/json; charset=utf-8",
                    async: false,
                    url: aspxservicePath + 'securepost.ashx?call=' + method,
                    data: param,
                    // dataType: "json",
                    success: successFx,
                    error: error
                });
            };

            function aspxCommonObj() {
                var aspxCommonInfo = {
                    StoreID: AspxCommerce.utils.GetStoreID(),
                    PortalID: AspxCommerce.utils.GetPortalID(),
                    UserName: AspxCommerce.utils.GetUserName(),
                    CultureName: AspxCommerce.utils.GetCultureName(),
                    SessionCode: AspxCommerce.utils.GetSessionCode(),
                    CustomerID: AspxCommerce.utils.GetCustomerID()
                };
                return aspxCommonInfo;
            }


            var _setting = {
                Rate: 0,
                RewardRate: 0,
                TotalRewardAmount: 0,
                TotalRewardPoints: 0,
                MinRedeemBalance: 0,
                BalanceCapped: 0
            };
            var getGeneralSetting = function () {                
                
                    if (AspxCommerce.utils.GetCustomerID() > 0) {
                        if (GeneralSettings != null || GeneralSettings != undefined) {
                            var item = JSON.parse(GeneralSettings);
                            
                            if (item.IsActive) {
                                maxRewardPoints = item.TotalRewardPoints;
                                _setting.Rate = (item.RewardExchangeRate / item.RewardPoints);
                                _setting.RewardRate = item.RewardPointsEarned / item.AmountSpent;
                                _setting.TotalRewardAmount = item.TotalRewardAmount;
                                _setting.TotalRewardPoints = item.TotalRewardPoints;
                                _setting.MinRedeemBalance = item.MinRedeemBalance;
                                _setting.BalanceCapped = item.BalanceCapped;

                                $("#spanTotalRewardPoints").html(item.TotalRewardPoints);
                                $("#spanTotalRewardAmount").html(parseFloat(item.TotalRewardAmount).toFixed(2));
                                $("#spanRPExchangePoints").html(item.RewardPoints);
                                $("#spanRPExchangeAmount").html(parseFloat(item.RewardExchangeRate).toFixed(2));
                                $("#spanRPExchangeAmount").addClass('cssClassFormatCurrency')
                                $("#spanTotalRP").html(item.TotalRewardPoints);
                                $("#spanTotalRA").html(parseFloat(item.TotalRewardAmount).toFixed(2));
                                $("#spanTotalRA").addClass('cssClassFormatCurrency');
                                $("#spanCapped").html(item.BalanceCapped);
                                $(".divRange").show();
                                $("#dvUsePoints").show();

                                $('#dvRewardPointsMain').show();
                                                                
                                isPurchaseActive = item.IsPurchaseActive.toString().toLowerCase() == 'true';                                
                                if (isPurchaseActive) {
                                    $('#dvRPCurrent').show();
                                } else {
                                    $('#dvRPCurrent').hide();
                                }


                                if (rewardpoint > 0) {
                                    $("#slider-range").slider({
                                        range: true,
                                        min: minRewardPoints,
                                        max: maxRewardPoints,
                                        values: [rewardpoint, maxRewardPoints],
                                        slide: function (event, ui) {
                                            $("#amount").html("<span>" + ui.values[0] + "</span>" + " - " + "<span>" + ui.values[1] + "</span>");
                                        },
                                        change: function (event, ui) {
                                            $("#amount").html("<span>" + ui.values[0] + "</span>" + " - " + "<span>" + ui.values[1] + "</span>");

                                        }
                                    });
                                } else {
                                    $("#slider-range").slider({
                                        range: true,
                                        min: minRewardPoints,
                                        max: maxRewardPoints,
                                        values: [minRewardPoints, maxRewardPoints],
                                        slide: function (event, ui) {
                                            $("#amount").html("<span>" + ui.values[0] + "</span>" + " - " + "<span>" + ui.values[1] + "</span>");
                                        },
                                        change: function (event, ui) {
                                            $("#amount").html("<span>" + ui.values[0] + "</span>" + " - " + "<span>" + ui.values[1] + "</span>");

                                        }
                                    });
                                }
                                if (rewardpoint > 0) {
                                    var total = rewardpoint * _setting.Rate;
                                    cartCalculator.AddOtherAmount("RewardPoints:", total, cartCalculator.OperationType.minus);
                                    cartCalculator.ShowData();
                                }

                                $("#amount").html("<span>" + $("#slider-range").slider("values", 0) + "</span>" +
                                    " - " + "<span>" + $("#slider-range").slider("values", 1) + "</span>");
                            }

                        }
                    }

            }();

            var calcItemReward = function () {               
                var arrRewardtotalPrice = 0, arrRewardDetails = "", arrRewardSub = "";
                $.each(cartCalculator.Items, function (index, item) {
                    arrRewardDetails += '<tr><td>' + '<b>' + parseFloat((item.TotalItemCost) * _setting.RewardRate).toFixed(2) + '</b>' + getLocale(AspxCartLocale, "Points for Product:") + item.ItemName + '&nbsp &nbsp' + '</td>';
                    arrRewardDetails += '<td>' + '+' + item.Quantity + "x" + parseFloat((item.Price) * _setting.RewardRate).toFixed(2) + '</td></tr>';
                    arrRewardtotalPrice = arrRewardtotalPrice + parseFloat((item.Price * item.Quantity).toFixed(2));

                });
                if (isPurchaseActive) {
                    $('#dvPointsTotal').empty();
                    $('#tblRewardDetail').html(arrRewardDetails);
                    $('#dvPointsTotal').append(arrRewardtotalPrice.toFixed(2) * _setting.RewardRate.toFixed(2));

                }
                var cookieCurrency = $("#ddlCurrency").val();
                Currency.currentCurrency = BaseCurrency;
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
            };

            var init = function () {
                if (AspxCommerce.utils.GetCustomerID() > 0) {
                    calcItemReward();
                }
                $("#chkUseRewardPoints").on("change", function () {
                    rewardAmounTemp = "";
                    var itemSumTotal = 0.00;
                    itemSumTotal = cartCalculator.Calculate();

                    rewardAmounTemp = parseFloat(_setting.TotalRewardAmount).toFixed(2);
                    var totalRA = parseFloat(_setting.TotalRewardAmount).toFixed(2);
                    var totalRP = parseFloat(_setting.TotalRewardPoints).toFixed(2);
                    var minRedeem = parseFloat(_setting.MinRedeemBalance).toFixed(2);

                    if (this.checked) {
                        if (eval(totalRP) >= eval(minRedeem)) {
                            if (eval(itemSumTotal) > eval(totalRA)) {
                                $securepost("SSA", { k: 'RewardPoints', v: totalRP }, function () { }, null);
                                $('#slider-range').slider("option", "values", [totalRP, totalRP]);
                                cartCalculator.AddOtherAmount("RewardPoints:", totalRA, cartCalculator.OperationType.minus);
                                cartCalculator.ShowData();       
                                $("#txtTotalCost").val(itemSumTotal - totalRA);
                            } else {
                                csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Item subtotal should be more than reward amount.!") + "</p>");
                                $securepost("SSA", { k: 'RewardPoints', v: 0 }, function () { }, null);
                                $("#chkUseRewardPoints").attr("checked", false);
                                cartCalculator.AddOtherAmount("RewardPoints:", 0, cartCalculator.OperationType.minus);
                                cartCalculator.ShowData();
                            }
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Your minimum redeem balance should be ") + minRedeem + getLocale(AspxCartLocale, " reward points") + "</p>");
                            $("#chkUseRewardPoints").attr("checked", false);


                        }
                    } else {
                        $securepost("SSA", { k: 'RewardPoints', v: 0 }, function () { }, null);
                        cartCalculator.AddOtherAmount("RewardPoints:", 0, cartCalculator.OperationType.minus);
                        cartCalculator.ShowData();
                        $('#slider-range').slider("option", "values", [0, totalRP]);
                        $("#txtRewardAmount").val(0.00);
                    }

                });
                $("#btnUsePoints").bind("click", function () {
                    rewardAmounTemp = "";
                    $("#chkUseRewardPoints").attr("checked", false);
                    var points = $('#slider-range').slider("option", "values");
                    var itemSumTotal = 0.00;
                    itemSumTotal = cartCalculator.Calculate();
                    rewardAmounTemp = points[0] * parseFloat(_setting.Rate).toFixed(2);
                    var totalRP = points[0];
                    var totalRA = points[0] * parseFloat(_setting.Rate).toFixed(2);
                    var minRedeem = parseFloat(_setting.MinRedeemBalance).toFixed(2);
                    if (eval(totalRP) >= eval(minRedeem)) {
                        if (eval(itemSumTotal) > eval(totalRA)) {
                            $securepost("SSA", { k: 'RewardPoints', v: totalRP }, function () { }, null);

                            cartCalculator.AddOtherAmount("RewardPoints:", totalRA, cartCalculator.OperationType.minus);
                            cartCalculator.ShowData();
                            $("#txtTotalCost").val(itemSumTotal - totalRA);
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Item subtotal should be more than reward amount.!") + "</p>");

                            $securepost("SSA", { k: 'RewardPoints', v: 0 }, function () { }, null);
                            cartCalculator.AddOtherAmount("RewardPoints:", 0, cartCalculator.OperationType.minus);
                            cartCalculator.ShowData();
                        }
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxCartLocale, "Information Alert") + "</h2><p>" + getLocale(AspxCartLocale, "Your minimum redeem balance should be ") + minRedeem + getLocale(AspxCartLocale, " reward points") + "</p>");
                        $securepost("SSA", { k: 'RewardPoints', v: 0 }, function () { }, null);
                        cartCalculator.AddOtherAmount("RewardPoints:", 0, cartCalculator.OperationType.minus);
                        cartCalculator.ShowData();
                    }

                });
            };
            var get = function () { };
            RewardedPoint = calcItemReward
            return {
                Init: init,
                Get: get
            }
            
        }();
        window.onload = function () {
            setTimeout(function () {
                RewardPoints.Init();
            }, 2000);
        }

    });

</script>