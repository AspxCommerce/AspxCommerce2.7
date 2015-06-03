var StockOutofNotification = "";
var notificationID = '';
var productUrl = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    StockOutofNotification = {
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
            method: "",
            ajaxCallMode: 0,
            error: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: StockOutofNotification.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: StockOutofNotification.config.contentType,
                cache: StockOutofNotification.config.cache,
                async: StockOutofNotification.config.async,
                data: StockOutofNotification.config.data,
                dataType: StockOutofNotification.config.dataType,
                url: StockOutofNotification.config.url,
                success: StockOutofNotification.ajaxSuccess,
                error: StockOutofNotification.ajaxFailure
            });
        },

        init: function() {

            StockOutofNotification.BindNotificationListInGrid(null, null, null, null, storeId, portalId);

            $("#btnShow").click(function() {
                StockOutofNotification.BindNotificationListInGrid(null, null, null, null, storeId, portalId);
                $("#txtProductName").val('');
                $("#txtMailStatus").val('');
                $("#ddlItemStatus").val('');
            });
            $("#btnSearch").click(function() {
                StockOutofNotification.SearchNotificationList();

            });
            $("#btnDeleteSelected").click(function() {
                var notificationIds = '';
                notificationIds = SageData.Get("tblOutofStockNotification").Arr.join(',');
                if (notificationIds.length == 0) {
                    csscody.alert('<h2>'+getLocale(AspxOutOfStockNotification,'Information Alert')+'</h2><p>'+getLocale(AspxOutOfStockNotification,'None of the data are selected')+'</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        StockOutofNotification.DeleteMultipleNotification(notificationIds, e);
                    }
                };
                csscody.confirm("<h2>"+getLocale(AspxOutOfStockNotification,'Delete Confirmation')+'</h2><p>'+getLocale(AspxOutOfStockNotification,'Are you sure you want to delete records?')+"</p>", properties);
            });
                                                                                                                                                                                                                                                                        
        },

        ajaxSuccess: function(data) {
            switch (StockOutofNotification.config.ajaxCallMode) {
                case 1:
                    StockOutofNotification.BindNotificationListInGrid(null, null, null, null, storeId, portalId);
                    csscody.info("<h2>"+getLocale(AspxOutOfStockNotification,'Successful Message')+'</h2><p>'+getLocale(AspxOutOfStockNotification,'Record has been deleted successfully.')+"</p>");
                    break;
                                                                        }
        },

        ajaxFailure: function() {
            switch (StockOutofNotification.config.error) {
                case 1:
                    csscody.error("<h2>"+getLocale(AspxOutOfStockNotification,'Error Message')+'</h2><p>'+getLocale(AspxOutOfStockNotification,'Failed to delete record')+"</p>");
                    break;
                                                         }
        },
        BindNotificationListInGrid: function(itemSKU, mailStatus, itemStatus, customer, storeId, portalId) {
            var getAllNotificationObj = {
                ItemSKU: itemSKU,
                MailStatus: mailStatus,
                ItemStatus: itemStatus,
                Customer: customer
            };
            this.config.method = "GetNotificationList";
            this.config.data = { getAllNotificationObj: getAllNotificationObj, aspxCommonObj: aspxCommonObj };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblOutofStockNotification_pagesize").length > 0) ? $("#tblOutofStockNotification_pagesize :selected").text() : 10;
            $("#tblOutofStockNotification").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxOutOfStockNotification, 'Notification ID'), name: '_notificationID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'newsChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxOutOfStockNotification, 'ItemID'), name: '_itemID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxOutOfStockNotification, 'Variant Value'), name: '_variantValueID', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOutOfStockNotification, 'Item SKU'), name: '_itemSKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOutOfStockNotification, 'Customer'), name: '_customer', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOutOfStockNotification, 'Email'), name: '_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOutOfStockNotification, 'Mail Status'), name: '_mailStatus', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Send/Not Send' },
                   { display: getLocale(AspxOutOfStockNotification,'Active'), name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxOutOfStockNotification,'Recieved On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', hide: false },
                    { display: getLocale(AspxOutOfStockNotification,'Item Status'), name: 'ItemStatus', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Available/OutOfStock', hide: true },
                    { display: getLocale(AspxOutOfStockNotification,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                                   {display: getLocale(AspxOutOfStockNotification,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StockOutofNotification.DeleteNotification', arguments: '1,2,3,4,5,6,7' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxOutOfStockNotification,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 10: { sorter: false} }
            });
        },
        DeleteMultipleNotification: function(ids, event) {
            StockOutofNotification.DeleteNotificationByID(ids, event);
        },
        DeleteNotification: function(tblID, argus) {
            switch (tblID) {
                case "tblOutofStockNotification":
                    var properties = {
                        onComplete: function(e) {
                            StockOutofNotification.DeleteNotificationByID(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>"+getLocale(AspxOutOfStockNotification,'Delete Confirmation')+'</h2><p>'+getLocale(AspxOutOfStockNotification,'Are you sure you want to delete this record?')+"</p>", properties);
                    break;
                default:
                    break;
            }

        },
        DeleteNotificationByID: function(ids, event) {
            notificationID = ids;
            if (event) {

                var param = JSON2.stringify({ notificationID: notificationID, aspxCommonObj:aspxCommonObj });
                this.config.method = "DeleteNotification";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 1;
                this.config.error = 1;
                this.ajaxCall(this.config);

            }
        },
                                                                                                         
       
                                                                                                                                                                                                    
                     
              
        SearchNotificationList: function() {
            var itemSKU = $.trim($("#txtProductName").val());
            var mailStatus = $.trim($("#txtMailStatus").val());
                       var itemStatus = null;
            var customer = $.trim($("#txtCustomerName").val());
            if (customer == '') {
                customer = null;
            }
            if (itemSKU.length < 1) {
                itemSKU = null;
            }
            if (mailStatus == "0") {
                mailStatus = null;
            }
            else if (mailStatus == "Yes") {
                mailStatus = '1';
            }
            else if (mailStatus == "No") {
                mailStatus = '0';
            }
            StockOutofNotification.BindNotificationListInGrid(itemSKU, mailStatus, itemStatus, customer, storeId, portalId);
        }
    };
    StockOutofNotification.init();
});
