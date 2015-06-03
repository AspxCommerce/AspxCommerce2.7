


var RewardPoints = function () {

       var rewardpoint = 0;
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
                  url: aspxRootPath + 'Modules/AspxCommerce/AspxRewardPoints/RewardPointsHandler.ashx/' + method,
            data: param,
            dataType: "json",
            success: successFx,
            error: error
        });
    };
    var $securepost = function (method, param, successFx, error) {
        $.ajax({
            type: "POST",
                       async: true,
            url: aspxservicePath + 'securepost.ashx?call=' + method,
            data: param,
                       success: successFx,
            error: error
        });
    };

 
    function aspxCommonObj() {
        var aspxCommonInfo = {
            StoreID: storeID,
            PortalID: portalID,
            UserName: userName,
            CultureName: cultureName,
            SessionCode:sessionCode,
            CustomerID: customerID
        };
        return aspxCommonInfo;
    }

    var _setting = {};
    var getGeneralSetting = function () {
               $ajaxCall("GetGeneralSetting", JSON2.stringify({
            aspxCommonObj: aspxCommonObj()
        }), function (data) {
            if (data.d != null) {
                var item = data.d[0];
                maxRewardPoints = item.TotalRewardPoints;
                _setting.Rate = item.RewardExchangeRate / item.RewardPoints;
                _setting.RewardRate = item.RewardPointsEarned / item.AmountSpent;
                _setting.TotalRewardAmount = item.TotalRewardAmount;
                _setting.TotalRewardPoints = item.TotalRewardPoints;
                _setting.MinRedeemBalance = item.MinRedeemBalance;
                _setting.BalanceCapped = item.BalanceCapped;


                $ajaxCall("IsPurchaseActive", JSON2.stringify({
                    aspxCommonObj: aspxCommonObj()
                }), function (data) {

                    isPurchaseActive = data.d;
                                       var rewardInfo = { IsPurchaseActive: isPurchaseActive, Setting: _setting };
                    setToCheckout(rewardInfo);

                }, function () { });

                if (rewardpoint > 0) {
                    var total = rewardpoint * _setting.Rate;
                    CheckOutApi.CalcAPI.AddOtherAmount("RewardPoints:", total, CheckOutApi.CalcAPI.OperationType.minus);
                    CheckOutApi.CalcAPI.ShowData();

                    CheckOutApi.Set("UsedRewardPoints", rewardpoint);
                    CheckOutApi.Set("RewardPointsDiscount", total);

                                   }


            }


        }, function () { });

    };

    var setToCheckout = function (rewardInfo) {

      
        CheckOutApi.Set('RewardPoint', rewardInfo);
    }

    var addItemRewardPoins = function () {

        if (isPurchaseActive) {

            $.each(CheckOutApi.CalcAPI.Items, function (index, item) {

                var rp = item.Price * item.Quantity * _setting.Rate;
                               CheckOutApi.SetItem(item.ItemID, "RewardedPoints", rp);

            });


        }


    };
    var init = function () {
        $securepost("GSA", { k: "RewardPoints" }, function (data) {
            rewardpoint = parseFloat(data.d);

        }, null);
        getGeneralSetting();
    };

    setTimeout(function () {
        init();
    }, 5000);
   
}();



