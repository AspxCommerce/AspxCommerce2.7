$(function() {
    var provider = function() {
        var $ajaxCall = function(method, param, successFx, error) {
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: "application/json; charset=utf-8",
                async: true,
                url: aspxservicePath + 'AspxCommerceWebService.asmx/' + method,
                data: param,
                dataType: "json",
                success: successFx,
                error: error
            });
        };
        var $init = function() {
            $bindFuntionToControl();
        };

        var $bindFuntionToControl = function() {

        };
        var $getAllShippingMethodsbyProvider = function(providerId) {
            
        };

        var $deleteShippingMethod = function() {
            
        };

        var $getProviderDynamicInfo = function(providerId) {
            
        };

        var $getRemainingShippingMethodsofProvider = function() {

        };

        $init();

    }();
});