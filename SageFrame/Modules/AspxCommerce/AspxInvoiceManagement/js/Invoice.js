var invoiceMgmt;
$(function () {
    var isInstalled = false;
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var itemModulePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/";
    var customerEmail = '';
    var aspxCommonObj = {
        StoreID: storeId,
        PortalID: portalId,
        UserName: userName,
        CultureName: cultureName
    };
    var invoiceObj = {
        InvoiceNumber: null,
        BillToName: null,
        OrderStatusName: null,
        billingEmail: '',
        itemDetails: '',
        billingshipping: '',
        additional: ''
    };

    var shippingAddress = '';
    var billingAddress = '';
    invoiceMgmt = {
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
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: invoiceMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: invoiceMgmt.config.contentType,
                cache: invoiceMgmt.config.cache,
                async: invoiceMgmt.config.async,
                url: invoiceMgmt.config.url,
                data: invoiceMgmt.config.data,
                dataType: invoiceMgmt.config.dataType,
                success: invoiceMgmt.ajaxSuccess,
                error: invoiceMgmt.ajaxFailure
            });
        },
        HideAll: function () {
            $('#divOrderDetails').hide();
            $('#divInvoiceForm').hide();
        },
        LoadInvoiceAjaxStaticImage: function () {
            $('#imgStoreLogo').prop('src', AspxCommerce.utils.GetAspxRootPath() + storeLogoUrl.replace('uploads', 'uploads/Small'));
        },
        ClearInvoiceForm: function () {
            $('#spanInvoiceNo').html('');
            $("#spanCustomerName").html('');
            $("#spanCustomerEmail").html('');
            $("#spanOrderID").html('');
            $("#spanOrderDate").html('');
            $("#spanOrderStatus").html('');
            $("#spanPaymentMethod").html('');
            $("#spanShippingMethod").html('');
            $("#divBillingAddressInfo").html('');
            $("#divShippingAddressInfo").html('');
            $('#divOrderItemDetails>table').empty();
            invoiceObj = {
                InvoiceNumber: null,
                BillToName: null,
                OrderStatusName: null,
                billingEmail: '',
                itemDetails: '',
                billingshipping: '',
                additional: ''
            };
        },
        BindInvoiceInformation: function (invoiceNum, billToName, statusType) {
            invoiceObj.InvoiceNumber = invoiceNum;
            invoiceObj.BillToName = billToName;
            invoiceObj.OrderStatusName = statusType;
            this.config.url = this.config.baseURL;
            this.config.method = "GetInvoiceDetailsList";
            this.config.data = { invoiceObj: invoiceObj, aspxCommonObj: aspxCommonObj };
            var invoiceData = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvInvoiceDetails_pagesize").length > 0) ? $("#gdvInvoiceDetails_pagesize :selected").text() : 10;

            $("#gdvInvoiceDetails").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxInvoiceManagement, 'Invoice Number'), name: 'invoice_number', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', url: '', queryPairs: '' },
                    { display: getLocale(AspxInvoiceManagement, 'Invoice Date'), name: 'invoice_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Order ID'), name: 'order_id', cssclass: '', controlclass: '', coltype: 'label', align: 'left'},
                    { display: getLocale(AspxInvoiceManagement, 'Customer Name'), name: 'CustomerName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Order Date'), name: 'order_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Bill to Name'), name: 'bill_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Ship to Name'), name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxInvoiceManagement, 'Amount'), name: 'amount', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxInvoiceManagement, 'Customer Email'), name: 'CustomerEmail', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxInvoiceManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                                                  { display: getLocale(AspxInvoiceManagement, 'View'), name: 'view Invoice', enable: true, _event: 'click', trigger: '3', callMethod: 'invoiceMgmt.ViewAttributes', arguments: '1,2,3,4,5,6,7,8,9' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxInvoiceManagement, "No Records Found!"),
                param: invoiceData, current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        ViewAttributes: function (tblID, argus) {
            switch (tblID) {
                case "gdvInvoiceDetails":
                    customerEmail = argus[11];
                    $("#" + lblInvoiceForm).html("Invoice Number: " + argus[0]);
                    $('#divOrderDetails').hide();
                    $('#divInvoiceForm').show();
                    $('#spanInvoiceNo').html(argus[0]);
                    $("#spanCustomerName").html(argus[5]);
                    $("#spanCustomerEmail").html(argus[11]);
                    invoiceMgmt.getItemInvoiceDetail(argus[4]);
                    break;
                default:
                    break;
            }
        },
        GetInvoiceStatus: function () {
            this.config.url = this.config.baseURL + "GetStatusList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        getItemInvoiceDetail: function (orderId) {
            this.config.url = this.config.baseURL + "GetInvoiceDetailsByOrderID";
            this.config.data = JSON2.stringify({ orderID: orderId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindInvoiceDetail: function (msg) {
            var span = '';
            var span1 = '';
            var orderID = 0;
            var itemOrderDetails = '';
            var additionalNote = "";
            var length = msg.d.length;
            if (length > 0) {
                var item;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    if (index == 0) {
                        $("input[id$='hdnIsMultipleShipping']").val(item.IsMultipleShipping);
                        $("#spanOrderID").html(item.OrderID);
                        $("#spanOrderDate").html(item.OrderDate);
                        $("#spanOrderStatus").html(item.OrderStatusName);
                        $("#spanPaymentMethod").html(item.PaymentMethodName);
                        additionalNote = item.Remarks;
                        if (item.IsMultipleShipping != true) {
                            if (item.ShippingMethodName != '') {
                                $("#spanShippingMethod").html(item.ShippingMethodName);
                            } else {
                                if (item.ItemTypeId == 2) {
                                    $("#spanShippingMethod").html(getLocale(AspxInvoiceManagement, "Downloadable Items don't need Shipping Method"));
                                } else if (item.ItemTypeId == 4) {
                                    $("#spanShippingMethod").html(getLocale(AspxInvoiceManagement, "Service type Items don't need Shipping Method"));
                                } else {
                                    $("#spanShippingMethod").html(getLocale(AspxInvoiceManagement, "Shipping Method is not available."));
                                }
                            }
                        } else {
                            $('#spanShippingMethod').html(getLocale(AspxInvoiceManagement, 'Multiple Shipping Exist'));
                        }

                        $("#spanStoreName").html(storeName);
                        $("#spanStoreDescription").html(item.StoreDescription);
                        invoiceObj.billingEmail = item.Email;
                        invoiceObj.BillToName = item.BillingName;
                        billingAddress = {
                            BillingName: $.trim(item.BillingName),
                            Company: $.trim(item.Company),
                            Address1: $.trim(item.Address1),
                            Address2: $.trim(item.Address2),
                            City: $.trim(item.City),
                            State: $.trim(item.State),
                            Zip: $.trim(item.Zip),
                            Country: $.trim(item.Country),
                            Email: $.trim(item.Email),
                            Phone: $.trim(item.Phone),
                            Mobile: $.trim(item.Mobile),
                            Fax: $.trim(item.Fax),
                            Website: $.trim(item.Website)
                        };
                        span = "<ul class='cssBillingAddressUl'>";
                        span += "<h4>Billing Address: </h4>";
                        if (item.BillingName != "")
                            span += "<li>" + $.trim(item.BillingName) + "</li>";

                        if (item.Company != "")
                            span += "<li>" + $.trim(item.Company) + "</li>";

                        if (item.Address1 != "")
                            span += "<li>" + $.trim(item.Address1) + "</li>";

                        if (item.Address2 != "")
                            span += "<li>" + $.trim(item.Address2) + "</li>";

                        if (item.City != "")
                            span += "<li>" + $.trim(item.City);

                        if (item.State != "")
                            span += " " + $.trim(item.State);

                        if (item.Zip != "")
                            span += " " + $.trim(item.Zip) + "</li>";

                        if (item.Country != "")
                            span += "<li>" + $.trim(item.Country) + "</li>";

                        if (item.Email != "")
                            span += "<li>" + $.trim(item.Email) + "</li>";

                        if (item.Phone != "")
                            span += "<li>" + $.trim(item.Phone);

                        if (item.Mobile != "")
                            span += ", " + $.trim(item.Mobile) + "</li>";

                        if (item.Fax != "")
                            span += "<li>" + $.trim(item.Fax) + "</li>";

                        if (item.Website != "")
                            span += "<li>" + $.trim(item.Website) + "</li>";

                        span += "</ul>";
                        span1 = "<ul class='cssShippingAddressUl'>";

                        if (item.IsMultipleShipping != true) {
                            shippingAddress = {
                                ShippingName: $.trim(item.ShippingName),
                                ShipEmail: $.trim(item.ShipEmail),
                                ShipCompany: $.trim(item.ShipCompany),
                                ShipAddress1: $.trim(item.ShipAddress1),
                                ShipAddress2: $.trim(item.ShipAddress2),
                                ShipCity: $.trim(item.ShipCity),
                                ShipState: $.trim(item.ShipState),
                                ShipZip: $.trim(item.ShipZip),
                                ShipCountry: $.trim(item.ShipCountry),
                                ShipPhone: $.trim(item.ShipPhone),
                                ShipMobile: $.trim(item.ShipMobile),
                                ShipFax: $.trim(item.ShipFax),
                                ShipWebsite: $.trim(item.ShipWebsite)
                            };
                            if (item.ShippingName != '') {
                                span1 += "<h4>" + getLocale(AspxInvoiceManagement, 'Shipping Address:') + "</h4>";
                            } else {
                                if (item.ItemTypeId == 2) {
                                    span1 += "<li><b>" + getLocale(AspxInvoiceManagement, 'Shipping Address do not needed for Downloadable Items') + "</b></li>";
                                } else if (item.ItemTypeId == 4) {
                                    span1 += "<li><b>" + getLocale(AspxInvoiceManagement, 'Shipping Address do not needed for Service type Items') + "</b></li>";
                                } else {
                                    span1 += "<li><b>" + getLocale(AspxInvoiceManagement, 'Shipping Address is not available') + "</b></li>";
                                }
                            }
                            if (item.ShippingName != "")
                                span1 += "<li>" + $.trim(item.ShippingName) + "</li>";

                            if (item.ShipCompany != "")
                                span1 += "<li>" + $.trim(item.ShipCompany) + "</li>";

                            if (item.ShipAddress1 != "")
                                span1 += "<li>" + $.trim(item.ShipAddress1) + "</li>";

                            if (item.ShipAddress2 != "")
                                span1 += "<li>" + $.trim(item.ShipAddress2) + "</li>";

                            if (item.ShipCity != "")
                                span1 += "<li>" + $.trim(item.ShipCity);

                            if (item.ShipState != "")
                                span1 += " " + $.trim(item.ShipState);

                            if (item.ShipZip != "")
                                span1 += " " + $.trim(item.ShipZip) + "</li>";

                            if (item.ShipCountry != "")
                                span1 += "<li>" + $.trim(item.ShipCountry) + "</li>";

                            if (item.ShipEmail != "")
                                span1 += "<li>" + $.trim(item.ShipEmail) + "</li>";

                            if (item.ShipPhone != "")
                                span1 += "<li>" + $.trim(item.ShipPhone);

                            if (item.ShipMobile != "")
                                span1 += ", " + $.trim(item.ShipMobile) + "</li>";

                            if (item.ShipFax != "")
                                span1 += "<li>" + $.trim(item.ShipFax) + "</li>";

                            if (item.ShipWebsite != "")
                                span1 += "<li>" + $.trim(item.ShipWebsite) + "</li>";
                            span1 += "</ul>";
                            itemOrderDetails = '<table width="100%" border="0" cellspacing="0" cellpadding="0" class="OrderDetailsTable"><thead><tr align="left" class="cssClassLabel"><th class="cssClassItemID"><b>' + getLocale(AspxInvoiceManagement, "Item ID") + '</b></th><th class="cssClassItemSku"><b>' + getLocale(AspxInvoiceManagement, "Item SKU") + '</b></th><th class="cssClassItemName"><b>' + getLocale(AspxInvoiceManagement, "Item Name") + '</b></th><th class="cssClassQuantity"><b>' + getLocale(AspxInvoiceManagement, "Quantity") + '</b></th><th class="cssClassUnitPrice"><b>' + getLocale(AspxInvoiceManagement, "Unit Price") + '</b></th><th class="cssClassSubTotal"><b>' + getLocale(AspxInvoiceManagement, "Line Total") + '</b></th></td></tr></thead><tbody>';
                            $.each(msg.d, function (index, item) {
                                var imagePath = itemModulePath + item.ImagePath;
                                if (item.ImagePath == "") {
                                    imagePath = noItemImagePath;
                                }
                                if (item.CostVariants == "") {
                                    itemOrderDetails += "<tr><td class='cssClassItemID'>" + item.ItemId + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemSku'>" + item.SKU + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemName'><span><img src='" + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + "' alt='" + item.ItemName + "' title='" + item.ItemName + "'/></span></br><p>" + item.ItemName + "</p></td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                                } else {
                                    itemOrderDetails += "<tr><td class='cssClassItemID'>" + item.ItemId + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemSku'>" + item.SKU + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemName'><span><img src='" + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + "' alt='" + item.ItemName + "' title='" + item.ItemName + "'/></span></br><p>" + item.ItemName + " (" + item.CostVariants + ")" + "</p></td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                                }
                            });
                            if (index == 0) {
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>Sub Total :</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandSubTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>Discount Amount :</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' > " + item.DiscountAmount.toFixed(2) + "</span></td></tr>";
                                var orderID = item.OrderID;
                                $.ajax({
                                    type: "POST", beforeSend: function (request) {
                                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                        request.setRequestHeader("UMID", umi);
                                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                        request.setRequestHeader("PType", "v");
                                        request.setRequestHeader('Escape', '0');
                                    },
                                    url: invoiceMgmt.config.baseURL + "GetTaxDetailsByOrderID",
                                    data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: false,
                                    success: function (msg) {
                                        $.each(msg.d, function (index, val) {
                                            if (val.TaxSubTotal != 0) {
                                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice '><b>" + val.TaxManageRuleName + ':' + "</b></td>";
                                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + (val.TaxSubTotal).toFixed(2) + "</span></td></tr>";
                                            }
                                        });

                                    }

                                });

                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>" + getLocale(AspxInvoiceManagement, 'Shipping Cost :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.TotalShippingCost.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>" + getLocale(AspxInvoiceManagement, 'Coupon Amount :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' >  " + item.CouponAmount.toFixed(2) + "</span></td></tr>";
                                if (isInstalled == true) {
                                    itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>" + getLocale(AspxInvoiceManagement, 'Discount ( Reward Points ) :') + "</b></td>";
                                    itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' > " + item.RewardDiscountAmount.toFixed(2) + "</span></td></tr>";
                                }
                                if (item.GiftCard != "" && item.GiftCard != null) {
                                    var giftCardUsed = item.GiftCard.split('#');
                                    for (var g = 0; g < giftCardUsed.length; g++) {
                                        var keyVal = giftCardUsed[g].split("=");
                                        itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>" + getLocale(AspxInvoiceManagement, 'GiftCard') + "(" + keyVal[0] +
                                            ") :</b></td>";
                                        itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + keyVal[1] + "</span></td></tr>";
                                    }
                                }

                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassOrderPrice'><b>" + getLocale(AspxInvoiceManagement, 'Grand Total :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandTotal.toFixed(2) + "</span></td></tr>";
                            }
                            itemOrderDetails += '</tbody></table>';
                        } else {
                            itemOrderDetails = '<table class="OrderDetailsTable" width="100%" border="0" cellspacing="0" cellpadding="0"><thead><tr align="left" class="cssClassLabel"><th class="cssClassItemID cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, "Item ID") + '</b></th><th class="cssClassItemSku cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Item SKU') + '</b></th><th class="cssClassItemName cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Item Name') + '</b></th><th class="cssClassShippingMethod cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Shipping Method') + '</b></th><th class="cssClassShippingAddress cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Shipping To') + '</b></th><th class="cssClassQuantity cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Quantity') + '</b></th><th class="cssClassPrice cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Unit Price') + '</b></th><th class="cssClassSubTotal cssClassTDHeading"><b>' + getLocale(AspxInvoiceManagement, 'Line Total') + '</b></th></td></tr></thead><tbody>';
                            $.each(msg.d, function (index, item) {

                                var shippingDetails = "";
                                if (item.ShippingName != "")
                                    shippingDetails += $.trim(item.ShippingName);

                                if (item.ShipAddress1 != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipAddress1);

                                if (item.ShipAddress2 != "")
                                    shippingDetails += "," + $.trim(item.ShipAddress2);

                                if (item.ShipCompany != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCompany);

                                if (item.ShipCity != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCity);

                                if (item.ShipState != "")
                                    shippingDetails += " " + $.trim(item.ShipState);

                                if (item.ShipZip != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipZip);

                                if (item.ShipCountry != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCountry);

                                if (item.ShipEmail != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipEmail);

                                if (item.ShipPhone != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipPhone);

                                if (item.ShipMobile != "")
                                    shippingDetails += ", " + $.trim(item.ShipMobile);

                                if (item.ShipFax != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipFax);

                                if (item.ShipWebsite != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipWebsite);

                                if (item.ShippingMethodName == "") {
                                    item.ShippingMethodName = 'N/A';
                                }
                                if (shippingDetails == "") {
                                    shippingDetails = "N/A";
                                }
                                var imagePath = itemModulePath + item.ImagePath;
                                if (item.ImagePath == "") {
                                    imagePath = noItemImagePath;
                                }
                                if (item.CostVariants == "") {
                                    itemOrderDetails += "<tr><td class='cssClassItemID'>" + item.ItemId + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemSku'>" + item.SKU + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemName'><span><img src='" + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + "' alt='" + item.ItemName + "' title='" + item.ItemName + "'/></span></br><p>" + item.ItemName + "</p></td>";
                                    itemOrderDetails += "<td class='cssClassShippingMethod'>" + item.ShippingMethodName + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingAddress'>" + shippingDetails + "</td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";
                                } else {
                                    itemOrderDetails += "<tr><td class='cssClassItemID'>" + item.ItemId + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemSku'>" + item.SKU + "</td>";
                                    itemOrderDetails += "<td class='cssClassItemName'><span><img src='" + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + "' alt='" + item.ItemName + "' title='" + item.ItemName + "'/></span></br><p>" + item.ItemName + " (" + item.CostVariants + ")" + "</p></td>";
                                    itemOrderDetails += "<td class='cssClassShippingMethod'>" + item.ShippingMethodName + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingAddress'>" + shippingDetails + "</td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";

                                }
                                itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                            });

                            if (index == 0) {

                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Sub Total :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandSubTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Discount Amount :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' > " + item.DiscountAmount.toFixed(2) + "</span></td></tr>";
                                var orderID = item.OrderID;
                                $.ajax({
                                    type: "POST", beforeSend: function (request) {
                                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                        request.setRequestHeader("UMID", umi);
                                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                        request.setRequestHeader("PType", "v");
                                        request.setRequestHeader('Escape', '0');
                                    },
                                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetTaxDetailsByOrderID",
                                    data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: false,
                                    success: function (msg) {
                                        $.each(msg.d, function (index, val) {
                                            if (val.TaxSubTotal != 0) {
                                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + val.TaxManageRuleName + ':' + "</b></td>";
                                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + (val.TaxSubTotal).toFixed(2) + "</span></td></tr>";

                                            }
                                        });

                                    }

                                });

                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Shipping Cost :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.TotalShippingCost.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Coupon Amount :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' > " + item.CouponAmount.toFixed(2) + "</span></td></tr>";

                                if (isInstalled == true) {
                                    itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Discount ( Reward Points ):') + "</b></td>";
                                    itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassMinus'> - </span><span class='cssClassFormatCurrency cssClassSubTotal' > " + item.RewardDiscountAmount.toFixed(2) + "</span></td></tr>";
                                }
                                itemOrderDetails += "<tr><td class='cssClassItemID cssNoData'>&nbsp;</td><td class='cssClassItemSku cssNoData'>&nbsp;</td><td class='cssClassItemName cssNoData'><p>&nbsp;</p></td><td class='cssClassShippingMethod cssNoData'>&nbsp;</td><td class='cssClassShippingAddress cssNoData'>&nbsp;</td><td class='cssClassQuantity cssNoData'>&nbsp;</td><td class='cssClassLabel cssClassPrice'><b>" + getLocale(AspxInvoiceManagement, 'Grand Total :') + "</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandTotal.toFixed(2) + "</span></td></tr>";
                            }
                            itemOrderDetails += '</tbody></table>';
                        }
                        $("#divOrderItemDetails").html(itemOrderDetails);
                        $("#divOrderItemDetails").append("<span class='remarks'></span>");
                        $("#divOrderItemDetails").find(".OrderDetailsTable>tbody tr:even").addClass("sfEven");
                        $("#divOrderItemDetails").find(".OrderDetailsTable>tbody tr:odd").addClass("sfOdd");
                        if (additionalNote != '' && additionalNote != undefined) {
                            $(".remarks").html("").html(getLocale(AspxInvoiceManagement, "*Additional Note :- ") + "'" + additionalNote + "'");
                        } else {
                            $(".remarks").html("");
                        }
                    }

                    $("#divOrderDetailForm").show();
                };
                $("#divBillingAddressInfo").html(span);
                $("#divShippingAddressInfo").html(span1);
            } else {
                csscody.alert("<h1>" + getLocale(AspxInvoiceManagement, 'Information') + '</h1><p>' + getLocale(AspxInvoiceManagement, 'No Invoice is Available for this Order') + "</p>");
                $('#divOrderDetails').show();
                $('#divInvoiceForm').hide();
            }
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        BindInvoiceForEmail: function (msg) {
            var additional = "";
            var billingshipping = "";
            var itemDetails = "";
            var itemOrderDetails = "";
            var span = '';
            var span1 = '';
            var orderID = 0;
            var length = msg.d.length;
            if (length > 0) {
                var item;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    if (index == 0) {
                        additional += item.OrderStatusName + "#";
                        additional += item.StoreName + "#";
                        additional += item.StoreDescription + "#";
                        additional += $('#customerNameEdit').html() + "#";
                        additional += item.OrderID + "#";
                        additional += item.PaymentMethodName + "#";
                        if (item.IsMultipleShipping != true) {
                            additional += item.ShippingMethodName + "#";
                        } else {
                            additional += 'Multiple Shipping Exist' + "#";
                        }
                        additional += $('#spanInvoiceNo').html();
                        invoiceObj.additional = additional;

                        span = ' <table cellspacing="1" cellpadding="1" border="0" align="left" width="300"> <tbody> <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: bold 14px Arial, Helvetica, sans-serif;"> <strong>' + getLocale(AspxInvoiceManagement, 'Billing To:') + '</strong> </td> </tr>';
                        if (item.BillingName != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.BillingName + '</td></tr>';

                        if (item.Address1 != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Address1 + '</td></tr>';

                        if (item.Address2 != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Address2 + '</td></tr>';

                        if (item.Company != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Company + '</td></tr>';

                        if (item.City != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.City + '</td></tr>';

                        if (item.State != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.State + '</td></tr>';

                        if (item.Zip != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Zip + '</td></tr>';
                        if (item.Country != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Country + '</td></tr>';

                        if (item.Email != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Email + '</td></tr>';

                        if (item.Phone != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Phone + '</td></tr>';

                        if (item.Mobile != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Mobile + '</td></tr>';

                        if (item.Fax != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Fax + '</td></tr>';

                        if (item.Website != "")
                            span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Website + '</td></tr>';

                        span += "</table>";
                        if (item.IsMultipleShipping != true) {
                            span1 = '<table cellspacing="1" cellpadding="1" border="0" align="left" width="280"> <tbody> <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: bold 14px Arial, Helvetica, sans-serif;">' + getLocale(AspxInvoiceManagement, 'Shipping To: ') + '</td> </tr>';
                            if (item.ShippingName != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShippingName + '</td></tr>';

                            if (item.ShipAddress1 != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipAddress1 + '</td></tr>';

                            if (item.ShipAddress2 != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipAddress2 + '</td></tr>';

                            if (item.ShipCompany != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCompany + '</td></tr>';

                            if (item.ShipCity != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCity + '</td></tr>';

                            if (item.ShipState != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipState + '</td></tr>';

                            if (item.ShipZip != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipZip + '</td></tr>';

                            if (item.ShipCountry != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCountry + '</td></tr>';

                            if (item.ShipEmail != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipEmail + '</td></tr>';

                            if (item.ShipPhone != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipPhone + '</td></tr>';

                            if (item.ShipMobile != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipMobile + '</td></tr>';

                            if (item.ShipFax != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipFax + '</td></tr>';

                            if (item.ShipWebsite != "")
                                span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipWebsite + '</td></tr>';

                            span1 += "</table>";
                            itemOrderDetails = ' <table cellspacing="0" cellpadding="0" border="0" width="620" style="border: 1px solid #dcdccc;"> <tbody> <tr style="background: #e5e5de;"><td width="50" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Item Image") + '</strong> </td> <td width="333" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Item Name") + '</strong> </td><td width="46" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Price") + '</strong> </td> <td width="89" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Qty") + '</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Line Total') + '</strong> </td> </tr> ';
                            $.each(msg.d, function (index, item) {
                                var cv = "";
                                if (item.CostVariants != "") {
                                    cv = "(" + item.CostVariants + ")";
                                }
                                var imagePath = itemModulePath + item.ImagePath;
                                if (item.ImagePath == "") {
                                    imagePath = noItemImagePath;
                                }
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f"><img height="81" width="123" src="' + serverHostLoc + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + item.ItemName + '" title="' + item.ItemName + '" /></td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ItemName + cv + '</td>';
                                itemOrderDetails += '<td  style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.Price.toFixed(2) + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.Quantity + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.SubTotal.toFixed(2) + '</td></tr>';
                            });
                            if (index == 0) {
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="5" colspan="3"> &nbsp; </td> <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, "Line Total") + '</td> ';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.GrandSubTotal.toFixed(2) + ' </td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> Taxes </td> ';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.TaxTotal.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Shipping Cost </td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.ShippingCost.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Discount</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.DiscountAmount.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Coupon </td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.CouponAmount.toFixed(2) + '</td></tr>';
                                if (isInstalled == true) {
                                    itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Discount ( Reward Points ) </td>';
                                    itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.RewardDiscountAmount.toFixed(2) + '</td></tr>';
                                }
                                itemOrderDetails += '<tr><td style="padding: 5px; border-right: 1px solid #dcdccc;" colspan="3"> &nbsp; </td> <td style="padding: 5px; border-right: 1px solid #dcdccc; font: bold 14px Arial, Helvetica, sans-serif; color: #000;">' + getLocale(AspxInvoiceManagement, "Total Cost") + '</td>';
                                itemOrderDetails += ' <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.GrandTotal.toFixed(2) + '</td></tr>';
                            }
                            itemOrderDetails += '</table>';
                        } else {
                            itemOrderDetails = "";
                            itemOrderDetails = '<table cellspacing="0" cellpadding="0" border="0" width="620" style="border: 1px solid #dcdccc;"><thead><tr style="background: #e5e5de;"><td width="50" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Item Image') + '</strong> </td><td width="333" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Item Name") + '</strong> </td><td width="46" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Price') + '</strong> </td> <td width="89" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, "Qty") + '</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Shipping Method') + '</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Shipping To') + '</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>' + getLocale(AspxInvoiceManagement, 'Line Total') + '</strong> </td> </tr> </thead>';
                            $.each(msg.d, function (index, item) {
                                var cv = "";
                                if (item.CostVariants != "") {
                                    cv = "(" + item.CostVariants + ")";
                                }
                                var imagePath = itemModulePath + item.ImagePath;
                                if (item.ImagePath == "") {
                                    imagePath = noItemImagePath;
                                }
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f"><img height="81" width="123" src="' + serverHostLoc + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + item.ItemName + '" title="' + item.ItemName + '" /></td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ItemName + cv + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.Price.toFixed(2) + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.Quantity + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ShippingMethodName + '</td>';
                                var shippingDetails = "";
                                if (item.ShippingName != "")
                                    shippingDetails += "<br/>" + item.ShippingName;

                                if (item.ShipAddress1 != "")
                                    shippingDetails += ",<br/>" + item.ShipAddress1;

                                if (item.ShipAddress2 != "")
                                    shippingDetails += "," + item.ShipAddress2;

                                if (item.ShipCompany != "")
                                    shippingDetails += ",<br/>" + item.ShipCompany;

                                if (item.ShipCity != "")
                                    shippingDetails += ",<br/>" + item.ShipCity;

                                if (item.ShipState != "")
                                    shippingDetails += "," + item.ShipState;

                                if (item.ShipZip != "")
                                    shippingDetails += ",<br/>" + item.ShipZip;

                                if (item.ShipCountry != "")
                                    shippingDetails += ",<br/>" + item.ShipCountry;

                                if (item.ShipEmail != "")
                                    shippingDetails += ",<br/>" + item.ShipEmail;

                                if (item.ShipPhone != "")
                                    shippingDetails += ",<br/>" + item.ShipPhone;

                                if (item.ShipMobile != "")
                                    shippingDetails += ", " + item.ShipMobile;

                                if (item.ShipFax != "")
                                    shippingDetails += ",<br/>" + item.ShipFax;

                                if (item.ShipWebsite != "")
                                    shippingDetails += ",<br/>" + item.ShipWebsite;

                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + shippingDetails + "</td>";
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f"  >' + item.SubTotal.toFixed(2) + "</td></tr>";
                            });

                            if (index == 0) {
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="5" colspan="5"> &nbsp; </td> <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, "Line Total") + '</td> ';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.GrandSubTotal.toFixed(2) + ' </td></tr>';

                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> ' + getLocale(AspxInvoiceManagement, 'Taxes') + ' </td> ';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.TaxTotal.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, 'Shipping Cost') + ' </td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.TotalShippingCost.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, 'Discount') + '</td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.DiscountAmount.toFixed(2) + '</td></tr>';
                                itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, 'Coupon') + ' </td>';
                                itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.CouponAmount.toFixed(2) + '</td></tr>';
                                if (isInstalled == true) {
                                    itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">' + getLocale(AspxInvoiceManagement, 'Discount (Reward Points)') + ' </td>';
                                    itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.RewardDiscountAmount.toFixed(2) + '</td></tr>';
                                }
                                itemOrderDetails += '<tr><td style="padding: 5px; border-right: 1px solid #dcdccc;" colspan="5"> &nbsp; </td> <td style="padding: 5px; border-right: 1px solid #dcdccc; font: bold 14px Arial, Helvetica, sans-serif; color: #000;">' + getLocale(AspxInvoiceManagement, 'Total Cost') + ' </td>';
                                itemOrderDetails += ' <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.GrandTotal.toFixed(2) + '</td></tr>';
                            }
                            itemOrderDetails += '</table>';
                        }

                        billingshipping = span + '</td><td width="280" valign="top" style="border: 1px solid #dcdccc; border-right: none;border-bottom: none; float: right">' + span1;
                        itemDetails = itemOrderDetails;
                    }
                };
                invoiceObj.billingshipping = billingshipping;
                invoiceObj.itemDetails = itemDetails;
            }
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        GenerateInvoicePDF: function () {
            var invoceDetailInfo = {
                headerInfo: {
                    InvoiceNo: $('#spanInvoiceNo').html(),
                    InvoiceDate: '',
                    StoreName: $('#spanStoreName').html(),
                    StoreDescription: $('#spanStoreDescription').html(),
                    CustomerName: $('#spanCustomerName').html(),
                    CustomerEmail: $('#spanCustomerEmail').html(),
                    OrderId: $('#spanOrderID').html(),
                    OrderDate: $('#spanOrderDate').html(),
                    Status: $('#spanOrderStatus').html(),
                    PaymentMethod: $('#spanPaymentMethod').html(),
                    ShippingMethod: $('#spanShippingMethod').html(),
                    BillingAddress: JSON2.stringify(billingAddress),
                    ShippingAddress: JSON2.stringify(shippingAddress)
                },
                tableDataInfo: {
                    ItemId: '',
                    SKU: '',
                    ItemName: '',
                    ImagePath: '',
                    ShippingMethodName: '',
                    ShippingAddressDetail: '',
                    Price: '',
                    Quantity: '',
                    SubTotal: ''
                }
            };
            var headerString = JSON2.stringify(invoceDetailInfo.headerInfo);
            var tdArrayColl = new Array();
            $('#divOrderItemDetails tr ').each(function () {
                invoceDetailInfo.tableDataInfo.ItemId = $(this).find('.cssClassItemID b').length > 0 ? $(this).find('.cssClassItemID b').html() : $(this).find('.cssClassItemID ').html().replace("&nbsp;", "");
                invoceDetailInfo.tableDataInfo.SKU = $(this).find('.cssClassItemSku b').length > 0 ? $(this).find('.cssClassItemSku b').html() : $(this).find('.cssClassItemSku ').html().replace("&nbsp;", "");
                invoceDetailInfo.tableDataInfo.ItemName = $(this).find('.cssClassItemName b').length > 0 ? $(this).find('.cssClassItemName b').html() : $(this).find('.cssClassItemName p').html().replace("&nbsp;", "");
                invoceDetailInfo.tableDataInfo.ImagePath = $(this).find('.cssClassItemName b').length > 0 ? null : $(this).find('.cssClassItemName span img ').prop('src');
                if ($("input[id$='hdnIsMultipleShipping']").val().toLowerCase() == 'true') {
                    invoceDetailInfo.tableDataInfo.ShippingMethodName = $(this).find('.cssClassShippingMethod b').length > 0 ? $(this).find('.cssClassShippingMethod b').html() : $(this).find('.cssClassShippingMethod').html().replace("&nbsp;", "");
                    invoceDetailInfo.tableDataInfo.ShippingAddressDetail = $(this).find('.cssClassShippingAddress b').length > 0 ? $(this).find('.cssClassShippingAddress b').html() : $(this).find('.cssClassShippingAddress').html().replace("&nbsp;", "");
                } else {
                    invoceDetailInfo.tableDataInfo.ShippingMethodName = $(this).find('.cssClassShippingMethod b').length > 0 ? $(this).find('.cssClassShippingMethod b').html() : $(this).find('.cssClassShippingMethod').html();
                    invoceDetailInfo.tableDataInfo.ShippingAddressDetail = $(this).find('.cssClassShippingAddress b').length > 0 ? $(this).find('.cssClassShippingAddress b').html() : $(this).find('.cssClassShippingAddress').html();

                }
                invoceDetailInfo.tableDataInfo.Quantity = $(this).find('.cssClassQuantity b').length > 0 ? $(this).find('.cssClassQuantity b').html() : $(this).find('.cssClassQuantity').html().replace("&nbsp;", "");
                invoceDetailInfo.tableDataInfo.Price = $(this).find('.cssClassPrice').length > 0 ? $(this).find('.cssClassPrice').html() : ($(this).find('.cssClassUnitPrice').length > 0 ? $(this).find('.cssClassUnitPrice b').html() : $(this).find('.cssClassOrderPrice b').html()); invoceDetailInfo.tableDataInfo.SubTotal = $(this).find('.cssClassSubTotal b').length > 0 ? $(this).find('.cssClassSubTotal b').html() : $(this).find('.cssClassSubTotal').html();
                tdArrayColl.push(JSON2.stringify(invoceDetailInfo.tableDataInfo));
            });

            $("input[id$='HdnValue']").val(tdArrayColl.toString().replace(/<\/?[^>]+>/gi, ''));
            $("input[id$='invoiceHeaderDetails']").val(headerString);
            $("input[id$='hdnRemarks']").val($('.remarks').html());
        },
        PrintPage: function () {
            var content = $('#divPrintInvoiceForm').html();
            var pwin = window.open('', 'print_content', 'width=100,height=100');
            pwin.document.open();
            pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
            pwin.document.close();
            setTimeout(function () { pwin.close(); }, 5000);
        },
        SearchInvoices: function () {
            var invoiceNum = $.trim($("#txtInvoiceNumber").val());
            var billToName = $.trim($("#txtbillToName").val());
            var statusType = '';
            if (invoiceNum.length < 1) {
                invoiceNum = null;
            }
            if ($("#ddlStatus").val() != "0") {
                statusType = $.trim($("#ddlStatus").val());
            } else {
                statusType = null;
            }
            if (billToName.length < 1) {
                billToName = null;
            }
            invoiceMgmt.BindInvoiceInformation(invoiceNum, billToName, statusType);
        },
        ajaxSuccess: function (msg) {
            switch (invoiceMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(msg.d, function (index, item) {
                        var couponStatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                        $("#ddlStatus").append(couponStatusElements);
                    });
                    break;
                case 2:
                    invoiceMgmt.IsModuleInstalled();
                    invoiceMgmt.BindInvoiceDetail(msg);
                    invoiceMgmt.BindInvoiceForEmail(msg);
                    break;
                case 4:
                    csscody.info('<h2>' + getLocale(AspxInvoiceManagement, 'Information Message') + '</h2><p>' + getLocale(AspxInvoiceManagement, ' Email has been send to the customer successfully!') + '</p>');
                    break;
            }
        },
        ajaxFailure: function (data) {
            switch (invoiceMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxInvoiceManagement, 'Error Message') + '</h1><p>' + getLocale(AspxInvoiceManagement, 'Failed to load Order Status list  !!') + '</p>');
                    break;
                case 2:
                    csscody.error('<h1>' + getLocale(AspxInvoiceManagement, 'Error Message') + '</h1><p>' + getLocale(AspxInvoiceManagement, 'Failed to load Invoice Details !!') + '</p>');
                    break;
                case 4:
                    csscody.error('<h1>' + getLocale(AspxInvoiceManagement, 'Error Message') + '</h1><p>' + getLocale(AspxInvoiceManagement, 'Failed to send email !!') + '</p>');
                    break;
            }
        },
        EmailInvoice: function (e) {
            if (e) {
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj, receiverEmail: invoiceObj.billingEmail, billingShipping: invoiceObj.billingshipping, itemTable: invoiceObj.itemDetails, additionalFields: invoiceObj.additional, templateName: templatename });
                this.config.url = this.config.baseURL + "EmailInvoice";
                this.config.data = param;
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
        },
        IsModuleInstalled: function () {
            var rewardPoints = 'AspxRewardPoints';
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: AspxCommerce.utils.GetAspxServicePath() + "AspxCommonHandler.ashx/" + "GetModuleInstallationInfo",
                data: JSON2.stringify({ moduleFriendlyName: rewardPoints, aspxCommonObj: aspxCommonObj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    isInstalled = response.d;
                },
                error: function () {
                }
            });
        },
        init: function () {

            invoiceMgmt.LoadInvoiceAjaxStaticImage();
            invoiceMgmt.BindInvoiceInformation(null, null, null);
            invoiceMgmt.GetInvoiceStatus();
            invoiceMgmt.HideAll();
            $('#divOrderDetails').show();

            $("#btnBack").click(function () {
                invoiceMgmt.HideAll();
                $('#divOrderDetails').show();
                invoiceMgmt.BindInvoiceInformation(null, null, null);
                invoiceMgmt.ClearInvoiceForm();
            });
            $('#btnPrint').click(function () {
                invoiceMgmt.PrintPage();
            });
            $('#btnEmail').click(function () {
                var properties = {
                    onComplete: function (e) {
                        invoiceMgmt.EmailInvoice(e);
                    }
                };
                csscody.messageInfo("<h2>" + getLocale(AspxInvoiceManagement, 'Email Confirmation') + '</h2><p>' + getLocale(AspxInvoiceManagement, 'Do you want to email this invoice to billing address email ?') + "</br>" + invoiceObj.billingEmail + " </p>", properties);
            });
            $('#txtInvoiceNumber,#txtbillToName,#ddlStatus').keyup(function (event) {
                if (event.keyCode == 13) {
                    invoiceMgmt.SearchInvoices();
                }
            });
            var path = window.location.href;
            var pathArr = path.split('/');
            var pageName = pathArr[pathArr.length - 1];
            pageName = pageName.split('.');
            if (new RegExp('\\b' + "Reports" + '\\b').test(pageName)) {
                $('#' + lblInvoiceHeader).html(getLocale(AspxInvoiceManagement, "Invoiced Reports"));
            } else {
                $('#' + lblInvoiceHeader).html(getLocale(AspxInvoiceManagement, "Invoices"));
            }
        }
    };
    invoiceMgmt.init();
});