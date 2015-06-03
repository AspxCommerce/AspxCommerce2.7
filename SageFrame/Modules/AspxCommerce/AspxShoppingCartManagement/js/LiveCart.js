var LiveCart="";
$(function() {

    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    LiveCart = {
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
                type: LiveCart.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: LiveCart.config.contentType,
                cache: LiveCart.config.cache,
                async: LiveCart.config.async,
                url: LiveCart.config.url,
                data: LiveCart.config.data,
                dataType: LiveCart.config.dataType,
                success: LiveCart.ajaxSuccess,
                error: LiveCart.ajaxFailure
            });
        },
        init: function() {
            LiveCart.LoadLiveCartImageStaticImage();
            LiveCart.BindShoppingCartItems(null, null, null);
            $("#btnLiveSearch").on("click", function() {
                LiveCart.SearchLiveShoppingCart();
            });
            $('#txtSearchItemName,#txtCustomerName,#txtQuantity').keyup(function(event) {
                if (event.keyCode == 13) {
                    LiveCart.SearchLiveShoppingCart();
                }
            });
        },
        LoadLiveCartImageStaticImage: function() {
            $('#ajaxLiveCartImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        BindShoppingCartItems: function(itemNm, userNM, qnty) {
            aspxCommonObj.UserName = userNM;
            this.config.method = "GetShoppingCartItemsDetails";
            this.config.data = { itemName: itemNm, quantity: qnty, aspxCommonObj: aspxCommonObj, timeToAbandonCart: timeToAbandonCart };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShoppingCart_pagesize").length > 0) ? $("#gdvShoppingCart_pagesize :selected").text() : 10;

            $("#gdvShoppingCart").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxShoppingCartManagement,'Cart ID'), name: 'cart_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Item ID'), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Customer Name'), name: 'item_Id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Item Name'), name: 'item_name', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Quantity'), name: 'quantity', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Price'), name: 'price', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'Weight'), name: 'weight', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxShoppingCartManagement,'SKU'), name: 'SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxShoppingCartManagement,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: {}
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

        },
        ajaxSuccess: function(msg) {
            switch (LiveCart.config.ajaxCallMode) {
                case 0:
                    break;
            }
        },
        SearchLiveShoppingCart: function() {
            var itemNm = $.trim($("#txtSearchItemName").val());
            var userNM = $.trim($("#txtCustomerName").val());
            var qnty = $.trim($("#txtQuantity").val());
            if (itemNm.length < 1) {
                itemNm = null;
            }
            if (userNM.length < 1) {
                userNM = null;
            }
            if (qnty.length < 1) {
                qnty = null;
            }
            LiveCart.BindShoppingCartItems(itemNm, userNM, qnty);
        }
    };
    LiveCart.init();
});