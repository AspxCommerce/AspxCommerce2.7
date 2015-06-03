 var CustomerTotalOrders;
 $(function() {
     var aspxCommonObj = function() {
         var aspxCommonInfo = {
             StoreID: AspxCommerce.utils.GetStoreID(),
             PortalID: AspxCommerce.utils.GetPortalID(),
             CultureName: AspxCommerce.utils.GetCultureName()
         };
         return aspxCommonInfo;
     };
     CustomerTotalOrders = {
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

         init: function() {
             CustomerTotalOrders.BindCustomerOrderTotal(null);
             $("#btnSearchCustomerTotalOrders").on("click", function() {
                 CustomerTotalOrders.SearchCustomeOrderTotal();
             });
             $('#txtSearchUserNm').keyup(function(event) {
                 if (event.keyCode == 13) {
                     CustomerTotalOrders.SearchCustomeOrderTotal();
                 }
             });
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: CustomerTotalOrders.config.type, beforeSend: function (request) {
                     request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                     request.setRequestHeader("UMID", umi);
                     request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                     request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                     request.setRequestHeader("PType", "v");
                     request.setRequestHeader('Escape', '0');
                 },
                 contentType: CustomerTotalOrders.config.contentType,
                 cache: CustomerTotalOrders.config.cache,
                 async: CustomerTotalOrders.config.async,
                 url: CustomerTotalOrders.config.url,
                 data: CustomerTotalOrders.config.data,
                 dataType: CustomerTotalOrders.config.dataType,
                 success: CustomerTotalOrders.ajaxSuccess,
                 error: CustomerTotalOrders.ajaxFailure
             });
         },
         BindCustomerOrderTotal: function(user) {
             this.config.method = "GetCustomerOrderTotal";
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvCustomerOrderTotal_pagesize").length > 0) ? $("#gdvCustomerOrderTotal_pagesize :selected").text() : 10;

             $("#gdvCustomerOrderTotal").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                                     { display: getLocale(AspxCustomerManagement, "Customer Name"), name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Number Of Orders"), name: 'number_of_Orders', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Average Order Amount"), name: 'average_order', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                     { display: getLocale(AspxCustomerManagement, "Total Order Amount"), name: 'total_order', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                     { display: getLocale(AspxCustomerManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                 ],
                 buttons: [
                                 ],
                 rp: perpage,
                 nomsg: getLocale(AspxCustomerManagement, "No Records Found!"),
                 param: { aspxCommonObj: aspxCommonObj(), user: user },
                 current: current_,
                 pnew: offset_,
                 sortcol: { 4: { sorter: false } }
             });
             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
         },
         ajaxSuccess: function(msg) {
             switch (CustomerTotalOrders.config.ajaxCallMode) {
                 case 0:
                     break;
             }
         },
         SearchCustomeOrderTotal: function() {
             var UserName = $.trim($("#txtSearchUserNm").val());
             if (UserName.length < 1) {
                 UserName = null;
             }
             CustomerTotalOrders.BindCustomerOrderTotal(UserName);
         }
     };
     CustomerTotalOrders.init();
 });