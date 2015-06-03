var AbandonedCart = "";
$(function() {

    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    AbandonedCart = {
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
                type: AbandonedCart.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: AbandonedCart.config.contentType,
                cache: AbandonedCart.config.cache,
                async: AbandonedCart.config.async,
                url: AbandonedCart.config.url,
                data: AbandonedCart.config.data,
                dataType: AbandonedCart.config.dataType,
                success: AbandonedCart.ajaxSuccess,
                error: AbandonedCart.ajaxFailure
            });
        },
        init: function () {          
            AbandonedCart.LoadAbandonAndLiveStaticImage();
            AbandonedCart.BindAbandonedCart(null);
            $("#btnAbandonedSearch").on("click", function() {
                AbandonedCart.SearchAbandonedShoppingCart();
            });
            $('#txtAbdCustomerName').keyup(function(event) {
                if (event.keyCode == 13) {
                    AbandonedCart.SearchAbandonedShoppingCart();
                }
            });
        },

        LoadAbandonAndLiveStaticImage: function() {
            $('#ajaxAbandonAndliveImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },


        BindAbandonedCart: function(CstNm) {
            aspxCommonObj.UserName = CstNm;
            this.config.method = "GetAbandonedCartDetails";
            this.config.data = { aspxCommonObj: aspxCommonObj, timeToAbandonCart: timeToAbandonCart };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAbandonedCart_pagesize").length > 0) ? $("#gdvAbandonedCart_pagesize :selected").text() : 10;

            $("#gdvAbandonedCart").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
				{ display: getLocale(AspxShoppingCartManagement, 'Customer Name'), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: getLocale(AspxShoppingCartManagement, 'Number Of Items'), name: 'number_OfItems', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
				{ display: getLocale(AspxShoppingCartManagement, 'Quantity Of Items'), name: 'quantity_OfItems', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
				{ display: getLocale(AspxShoppingCartManagement, 'SubTotal'), name: 'subTotal', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                { display: getLocale(AspxShoppingCartManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
				],
                rp: perpage,
                nomsg: getLocale(AspxShoppingCartManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ajaxSuccess: function(data) {
            switch (AbandonedCart.config.ajaxCallMode) {
                case 0:
                    break;
            }
        },
        SearchAbandonedShoppingCart: function() {
            var CstNm = $.trim($("#txtAbdCustomerName").val());
            if (CstNm.length < 1) {
                CstNm = null;
            }
            AbandonedCart.BindAbandonedCart(CstNm);
        }
    }
    AbandonedCart.init();
});
    