 var MostViewedItems;
 $(function() {
     var aspxCommonObj = function() {
         var aspxCommonInfo = {
             StoreID: AspxCommerce.utils.GetStoreID(),
             PortalID: AspxCommerce.utils.GetPortalID(),
             UserName: AspxCommerce.utils.GetUserName(),
             CultureName: AspxCommerce.utils.GetCultureName()
         };
         return aspxCommonInfo;
     };
     MostViewedItems = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             data: '{}',
             dataType: 'json',
             baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
             method: "",
             url: ""
         },
         init: function() {
             MostViewedItems.LoadMostViewedItemStaticImage();
             MostViewedItems.BindIMostViewedtemsGrid(null);
             $("#btnSearchMostViewedItens").click(function() {
                 MostViewedItems.SearchItems();
             });
             $('#txtSearchName').keyup(function(event) {
                 if (event.keyCode == 13) {
                     MostViewedItems.SearchItems();
                 }
             });
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: MostViewedItems.config.type, beforeSend: function (request) {
                     request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                     request.setRequestHeader("UMID", umi);
                     request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                     request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                     request.setRequestHeader("PType", "v");
                     request.setRequestHeader('Escape', '0');
                 },
                 contentType: MostViewedItems.config.contentType,
                 cache: MostViewedItems.config.cache,
                 async: MostViewedItems.config.async,
                 url: MostViewedItems.config.url,
                 data: MostViewedItems.config.data,
                 dataType: MostViewedItems.config.dataType,
                 success: MostViewedItems.ajaxSuccess,
                 error: MostViewedItems.ajaxFailure
             });
         },
         LoadMostViewedItemStaticImage: function() {
             $('#ajaxMostViewedItemImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },

         SearchItems: function() {
             var Nm = $.trim($("#txtSearchName").val());
             if (Nm.length < 1) {
                 Nm = null;
             }
             MostViewedItems.BindIMostViewedtemsGrid(Nm);
         },
         BindIMostViewedtemsGrid: function(Nm) {
             this.config.method = "GetMostViewedItemsList";
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvMostViewedItems_pagesize").length > 0) ? $("#gdvMostViewedItems_pagesize :selected").text() : 10;
             $("#gdvMostViewedItems").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: getLocale(AspxItemsManagement,'ItemID'), name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemsChkbox', elemDefault: false, controlclass: 'classClassCheckBox', hide: true },
                     { display: getLocale(AspxItemsManagement, 'Item Type ID'), name: '_itemTypeID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true},
                     { display: getLocale(AspxItemsManagement, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: '', coltype: 'Price', align: 'left' },
                     { display: getLocale(AspxItemsManagement,'Number Of Views'), name: 'noofViews', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                 ],
                 rp: perpage,
                 nomsg: getLocale(AspxItemsManagement,"No Records Found!"),
                 param: { name: Nm, currencySymbol: currencySymbol, aspxCommonObj: aspxCommonObj() },
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false} }
             });
             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
         },
         ajaxSuccess: function(msg) {
             switch (MostViewedItems.config.ajaxCallMode) {
                 case 0:
                     break;
             }
         }
     };
     MostViewedItems.init();
 });