
var giftCardReport;
$(function() {
    var reportGiftCard = function() {
        var cardType = 0;
        var aspxCommonObj = function() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        };
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
                async: false,
                url: aspxservicePath + "AspxCoreHandler.ashx/" + method,
                data: param,
                dataType: "json",
                success: successFx,
                error: error
            });
        };
        var getGiftCardType = function() {
                       var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });

            $ajaxCall("GetGiftCardTypes", param, function(msg) {
                var listtype = "";
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        if (index == 0) {
                            cardType = item.TypeId;
                            listtype += "<label><input type=\"radio\" checked=\"checked\"name=\"giftcardtype\" value=\"" + item.TypeId + " \"/>" + item.Type + "</label> ";
                        } else {
                            listtype += "<label><input type=\"radio\" name=\"giftcardtype\" value=\"" + item.TypeId + " \"/>" + item.Type + "</label> ";

                        }
                    };
                    $("#giftCardType").html(listtype);
                    loadReport(null, null, null, null);
                    $("input[id$='_csvGiftCardHiddenCsv']").val($("input[name=giftcardtype]").val());
                    $("input[name=giftcardtype]").bind("change", function() {
                        cardType = parseInt($(this).val());
                        $("input[id$='_csvGiftCardHiddenCsv']").val($(this).val());
                        loadReport(null, null, null, null);
                        clear();
                    });
                }
            }, null);
        };
        var clear = function() {
            $("#txtSku").val('');
            $("#txtItemName").val('');
            $("#txtDateFrom").val('');
            $("#txtDateTo").val('');
        };
        var loadReport = function(sku, name, startDate, toDate) {
            var objGiftCard = {
                SKU: sku,
                ItemName: name,
                FromDate: startDate,
                ToDate: toDate,
                GiftCardType: cardType
            };
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvGiftCardReport_pagesize").length > 0) ? $("#gdvGiftCardReport_pagesize :selected").text() : 10;


            $("#gdvGiftCardReport").sagegrid({
                url: aspxservicePath + "AspxCoreHandler.ashx/",
                functionMethod: "GetGiftCardReport",
                colModel: [
                                   {display: getLocale(AspxItemsManagement, 'SKU'), name: 'SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Item Name'), name: 'ItemName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'from', name: 'FromDate', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'to', name: 'ToDate', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'row', name: 'row', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxItemsManagement,'Gift Card Code'), name: 'GiftCardCode', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxItemsManagement, 'Total Sale Amount'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxItemsManagement, 'Total Purchases'), name: 'total', cssclass: '', controlclass: '', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxItemsManagement, 'Active'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                param: { objGiftcard: objGiftCard, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: {}
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        };
        var init = function() {
            getGiftCardType();

            $("#btnGetReport").bind("click", function() {
                var sku = $.trim($("#txtsku").val());
                var name = $.trim($("#txtItemName").val());
                var from = $.trim($("#txtFromDate").val());
                if (from.length < 1) {
                    from = null;
                }
                var to = $.trim($("#txtToDate").val());
                if (to.length < 1)
                {
                    to = null;
                }
                               loadReport(sku, name, from, to);


            });
            $("#txtFromDate,#txtToDate").datepicker({
                maxDate: 0,
                changeYear: false,
                changeMonth: false,
                dateFormat: 'yy/mm/dd',
                onSelect: function() {

                    if (this.id == "txtFromDate") {
                        var to = $("#txtToDate");
                        if (this.value > to.val()) {
                            to.val('');
                        }
                    } else {
                        var from = $("#txtFromDate");
                        if (this.value < from.val()) {
                            from.val('');
                        }
                    }
                }
            });
        } ();
    } ();
});