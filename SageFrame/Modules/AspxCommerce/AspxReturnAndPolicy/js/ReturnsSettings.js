var ReturnsSettings;
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var msgbody = '';
    var aspxCommonObj = {
        StoreID: storeId,
        PortalID: portalId,
        UserName: userName,
        CultureName: cultureName,
        CustomerID: customerId,
        SessionCode: sessionCode
    };
    ReturnsSettings = {

        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            url: "",
            method: ""
        },

        ajaxCall: function(config) {
            $.ajax({
                type: ReturnsSettings.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ReturnsSettings.config.contentType,
                cache: ReturnsSettings.config.cache,
                async: ReturnsSettings.config.async,
                data: ReturnsSettings.config.data,
                dataType: ReturnsSettings.config.dataType,
                url: ReturnsSettings.config.url,
                success: ReturnsSettings.ajaxSuccess,
                error: ReturnsSettings.ajaxFailure
            });
        },
        init: function() {

            var frm = $("#form1").validate({
                messages: {
                    ExpiresInDays: {
                        required: '*'
                    }
                },
                rules:
                    {
                        ExpiresInDays: {
                            required: true,
                            number: true,
                            maxlength: 5
                        }
                    }
            });

            ReturnsSettings.GetSettings();

            $("#btnSave").on("click", function() {
                if (frm.form()) {
                    ReturnsSettings.SaveUpdateSettings();
                    return false;
                }
            });

            $("#btnBack").on("click", function() {
                document.location = path + "Admin/AspxCommerce/Sales/Returns" + pageExtension;
            });

        },
        SaveUpdateSettings: function() {
            var expiresInDays = $("#txtExpiresInDays").val();
            this.config.url = this.config.baseURL + "ReturnSaveUpdateSettings";
            this.config.data = JSON2.stringify({ expiresInDays: expiresInDays, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        GetSettings: function() {           
            this.config.url = this.config.baseURL + "ReturnGetSettings";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        ajaxSuccess: function(data) {
            switch (ReturnsSettings.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info("<h2>" + getLocale(AspxReturnAndPolicy, "Successful Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Saved Successfully") + "</p>");
                    ReturnsSettings.GetSettings();
                    break;
                case 2:

                    $.each(data.d, function(index, item) {
                        $("#txtExpiresInDays").val(item.ExpiresInDays);
                    });
                    break;
            }
        }
    };
    ReturnsSettings.init();
});
