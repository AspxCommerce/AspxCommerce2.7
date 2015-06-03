var PaymentGatewayManage = "";

$(function () {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var errorcode = errorCode;
    var chkIsActive = true;
    var progressTime = null;
    var progress = 0;
    var pcount = 0;
    var percentageInterval = [10, 20, 30, 40, 60, 80, 100];
    var timeInterval = [1, 2, 4, 2, 1, 5, 1];
    var fileName = "";
    var ext = "";
    var tempLogo = '';
    var tempOldLogo = '';
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    PaymentGatewayManage = {
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
        vars: {
            IsMultipleShipping: ''
        },
        init: function () {
            PaymentGatewayManage.LoadPaymentGateWayStaticImage();
            if (errorcode == 1) {
                PaymentGatewayManage.HideDivsWhenError();
            } else {
                PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            }
            PaymentGatewayManage.GetOrderStatus();
            $("#btnAddNewPayGateWay").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                $("#" + lblLoadMessage).html("");
                $("#" + lblLoadMessage).hide();
                $("#" + lblPaymentGateWay).html(getLocale(AspxPaymentGateWayManagement, "Add New Payment Method"));
                $("#divPaymentGateWayForm").show();
            });
            $("#btnSearchPaymentgateway").unbind().click(function () {
                PaymentGatewayManage.SearchPaymentgateway();
            });

            $("#btnSearchOrders").unbind().click(function () {
                PaymentGatewayManage.SearchOrders();
            });
            $('#txtSearchPaymentGateWayName,#ddlIsActive').unbind().keyup(function (event) {
                if (event.keyCode == 13) {
                    PaymentGatewayManage.SearchPaymentgateway();
                }
            });
            $('#txtSearchBillToName,#txtSearchShipToName,#ddlOrderStatus').unbind().keyup(function (event) {
                if (event.keyCode == 13) {
                    PaymentGatewayManage.SearchOrders();
                }
            });
            $("#btnDeletePayMethod").unbind().click(function () {
                var paymentGateway_Ids = '';
                paymentGateway_Ids = SageData.Get("gdvPaymentGateway").Arr.join(',');
                if (paymentGateway_Ids.length > 0) {
                    var properties = {
                        onComplete: function(e) {
                            PaymentGatewayManage.ConfirmDeleteMultiplePayments(paymentGateway_Ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxPaymentGateWayManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxPaymentGateWayManagement, "Are you sure you want to delete the selected payment methods?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxPaymentGateWayManagement, "Information Alert") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "Please select atleast one payment method before delete!") + '</p>');
                }
            });

            $("#btnCancelPaymentGateway").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });
            $("#btnSearchTransaction").unbind().click(function () {
                PaymentGatewayManage.BindTransactionLog($.trim($("#txtOrderID").val()), $("#hdnPaymentGatewayIDView").val());
            });

            $("#btnSubmitPayEdit").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                PaymentGatewayManage.UpdatePaymentGatewayMethod();
                $("#divPaymentGateWayManagement").show();
            });


            $("#btnCancelPayEdit").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });

            $("#btnBacktoOrderView").unbind().click(function () {
                $("#dvTransactionDetail").hide();
                $("#divPaymentGateWayManagementEdit").show();
            });
            $("#btnBackOrder").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagementEdit").show();
            });

            $("#btnBackPaymentEdit").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });

            $("#btnBackFromAddNetPaymentForm").unbind().click(function () {
                PaymentGatewayManage.HideAllDivs();
                PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                $("#divPaymentGateWayManagement").show();
            });

            $('#btnPrint').unbind().click(function () {
                PaymentGatewayManage.printPage();
            });
            if (AspxCommerce.utils.GetUserName() == "superuser") {
                $("#divPaymentGateWayManagement .cssClassHeaderRight .sfButtonwrapper").show();
                $("#isActive").show();
                $("p#delete").show();
            } else {
                $("#divPaymentGateWayManagement .cssClassHeaderRight .sfButtonwrapper").remove();
                $("#isActive").remove();
                $("p#delete").remove();
            }
        },

        ajaxCall: function (config) {
            $.ajax({
                type: PaymentGatewayManage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: PaymentGatewayManage.config.contentType,
                cache: PaymentGatewayManage.config.cache,
                async: PaymentGatewayManage.config.async,
                url: PaymentGatewayManage.config.url,
                data: PaymentGatewayManage.config.data,
                dataType: PaymentGatewayManage.config.dataType,
                success: PaymentGatewayManage.ajaxSuccess,
                error: PaymentGatewayManage.ajaxFailure
            });
        },
        LoadPaymentGateWayStaticImage: function () {
            $('#ajaxPayementGatewayImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxPaymentGateWayImage2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        ImageUploader: function (obj, divImage) {
            maxFileSize = maxFilesize;
            var upload = new AjaxUpload($('#' + obj), {
                action: aspxPayementGatewayPath + "MultipleFileUploadHandler.aspx",
                name: 'myfile[]',
                multiple: false,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    pcount = 0;
                    var percentage = $('.progress').find('.percentage');
                    var progressBar = $('.progress').find('.progressBar');
                    $('.progress').show();
                    PaymentGatewayManage.dummyProgress(progressBar, percentage);
                    fileName = file;
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxPaymentGateWayManagement, "Alert Message") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "Not a valid image!") + '</p>');
                            return false;
                        }
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxPaymentGateWayManagement, 'Alert Message') + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "Not a valid image!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.ImagePath != null && res.Status > 0) {
                        tempLogo = res.ImagePath;
                        PaymentGatewayManage.AddNewImages(res, obj, divImage);
                        return false;
                    } else {
                        csscody.error('<h2>' + getLocale(AspxPaymentGateWayManagement, "Error Message") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "Sorry! image can not be uploaded.") + '</p>');
                        return false;
                    }
                }
            });
        },
        dummyProgress: function (progressBar, percentage) {
            if (percentageInterval[pcount]) {
                progress = percentageInterval[pcount] + Math.floor(Math.random() * 10 + 1);
                percentage.text(progress.toString() + '%');
                progressBar.progressbar({
                    value: progress
                });
                var percent = percentage.text();
                percent = percent.replace('%', '');
                percent = percent.replace('%', '');
                if (percent == 100100 || percent > 100100) {
                    percentage.text('100%');
                }
            }
            if (timeInterval[pcount]) {
                progressTime = setTimeout(function () {
                    PaymentGatewayManage.dummyProgress(progressBar, percentage);
                }, timeInterval[pcount] * 10);
            }
            pcount++;
        },

        AddNewImages: function (response, obj, divImage) {
            $("#" + divImage).html('');
            $("#" + divImage).html('<img src="' + aspxRootPath + response.ImagePath + '" class="uploadImage" height="60px" width="100px"/>');
            $("#" + divImage).append('<img src="' + aspxRootPath + 'Administrator/Templates/Default/images/delete.png' + '" id="imgDelete" alt="Delete" title="Delete" />');
            $('.progress').hide();
            $("#imgDelete").off().click(function () {
                $("#logoPreview").html('');
                tempLogo = "";
            });
        },

        printPage: function () {
            var content = $('#divPrintOrderDetail').html();
                var pwin = window.open('', 'print_content', 'width=100,height=100');
                pwin.document.open();
                pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
                pwin.document.close();
                setTimeout(function () { pwin.close(); }, 50000);

        },

        GenerateOrderDetailsPDF: function () {
            var orderdate = $("#OrderDate").html();

            var storeName = $("#storeName").html();
            var storeDescription = $("#storeDescription").html();
            var paymentGatewayType = $("#PaymentGatewayType").html();
            var paymentMethod = $("#PaymentMethod").html();
            var billingAddress = $("#divBillingAddressDetail").html();

            var headingDescription = {
                headingInfo: {
                    orderDate: orderdate,
                    storeName: storeName,
                    storeDescription: storeDescription,
                    paymentGatewayType: paymentGatewayType,
                    paymentMethod: paymentMethod,
                    billingAddress: billingAddress
                }
            };
            var headerString = JSON2.stringify(headingDescription.headingInfo);

            var divContent = $('#orderItemDetail').html();

            var tableDataDescription = {
                totalDataInfo: {
                    itemName: '',
                    shippingMethodName: '',
                    shippingAddress: '',
                    shippingRate: '',
                    price: '',
                    quantity: '',
                    subTotal: ''
                }
            };
            var tdArrayColl = new Array();
            $('#orderItemDetail tbody tr ').each(function () {
                tableDataDescription.totalDataInfo.itemName = $(this).find(' .cssClassItemName').html().replace("&nbsp;", "");
                tableDataDescription.totalDataInfo.shippingMethodName = $(this).find('.cssClassShippingMethod').html().replace("&nbsp;", "");
                tableDataDescription.totalDataInfo.shippingAddress = $(this).find('.cssClassShippingAdress').html().replace("&nbsp;", "");
                tableDataDescription.totalDataInfo.shippingRate = $(this).find('.cssClassShippingRate').html().replace("&nbsp;", "");
                tableDataDescription.totalDataInfo.price = $(this).find('.cssClassPrice').html().replace("&nbsp;", "");
                tableDataDescription.totalDataInfo.quantity = $(this).find('.cssClassQuantity b').length > 0 ? $(this).find('.cssClassQuantity b').html() : $(this).find('.cssClassQuantity ').html();
                tableDataDescription.totalDataInfo.subTotal = $(this).find('.cssClassSubTotal').html();
                tdArrayColl.push(JSON2.stringify(tableDataDescription.totalDataInfo));
            });
            console.log(tdArrayColl.toString());
            $("input[id$='HdnValue']").val(Encoder.htmlEncode(JSON.stringify(tdArrayColl)));
            $("input[id$='hdnDescriptionValue']").val(Encoder.htmlEncode(JSON.stringify(headerString)));
        },
        BindTransactionLog: function (table, data) {
            var orderid = data[3];
            var paymentGatewayID = $("#hdnPaymentGatewayIDView").val();
            PaymentGatewayManage.config.url = PaymentGatewayManage.config.baseURL + "GetAllTransactionDetail";
            PaymentGatewayManage.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, paymentGatewayID: parseInt(paymentGatewayID), orderID: parseInt(orderid) });
            PaymentGatewayManage.config.ajaxCallMode = 5;
            PaymentGatewayManage.ajaxCall(PaymentGatewayManage.config);

        },
        GetDetailTransaction: function (dataall) {
            PaymentGatewayManage.HideAllDivs();
            $("#dvTransactionDetail").show();
            if (dataall == null) {
                $("#spanNodata").html("No Records Found!");
                $("#divTransactionDetail").hide();
            } else {
                $("#divTransactionDetail").show();
                $("#spanNodata").html("");
                $("#dvTransactionDetail").find("table").show();
                $("#dvTransactionDetail .cssClassCommonBox  span.cssNodata").hide();
                var data = dataall[0];
                (data.PaymentGatewayTypeName != "") ? $("#lblBindPName").html(data.PaymentGatewayTypeName) : $("#lblBindPName").html("N/A");
                (data.TransactionID != "") ? $("#lblBindtransactionId").html(data.TransactionID) : $("#lblBindtransactionId").html("N/A");
                (data.OrderID != "") ? $("#lblBindOrderId").html(data.OrderID) : $("#lblBindOrderId").html("N/A");
                (data.TotalAmount != "") ? $("#lblBindtotal").html(data.CurrencySymbol + ' ' + data.TotalAmount) : $("#lblBindtotal").html("N/A");
                (data.PaymentStatus != "") ? $("#lblBindstatus").html(data.PaymentStatus) : $("#lblBindstatus").html("N/A");
                (data.PayerEmail != "") ? $("#lblBindpayerEmail").html(data.PayerEmail) : $("#lblBindpayerEmail").html("N/A");
                (data.CreditCard != "") ? $("#lblBindcreditCard").html(data.CreditCard) : $("#lblBindcreditCard").html("N/A");
                (data.CustomerID != "") ? $("#lblBindcustomerId").html(data.CustomerID) : $("#lblBindcustomerId").html("N/A");
                (data.SessionCode != "") ? $("#lblBindsessionCode").html(data.SessionCode) : $("#lblBindsessionCode").html("N/A");
                (data.ResponseReasonText != "") ? $("#lblBindresponseText").html(data.ResponseReasonText) : $("#lblBindresponseText").html("N/A");
                (data.AuthCode != "") ? $("#lblBindAuthCode").html(data.AuthCode) : $("#lblBindAuthCode").html("N/A");
                var date = new Date(parseInt(data.AddedOn.substr(6)));
                $("#lblBindAddedon").html(date.format('yyyy-mm-dd h:MM TT')); $("#lblBindCustomerName").html(data.AddedBy);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

            }
        },
        BindPaymentMethodGrid: function (paymentgatewayName, isAct) {
            var paymentMethodObj = {
                PaymentGatewayName: paymentgatewayName,
                IsActive: isAct
            };
            this.config.method = "GetAllPaymentMethod";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPaymentGateway_pagesize").length > 0) ? $("#gdvPaymentGateway_pagesize :selected").text() : 10;
            $("#gdvPaymentGateway").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxPaymentGateWayManagement, 'PaymentGatewayId'), name: 'paymentgateway_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'PaymentChkbox', elemDefault: false, controlclass: 'paymentGatewayHeaderChkbox' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Payment Gateway Name'), name: 'paymentgateway_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Used'), name: 'IsUSE', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'View'), name: 'Paymentedit', btntitle: 'View', cssclass: 'cssClassButtonHeader', controlclass: 'cssClassButtonSubmit', coltype: 'button', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: '', poparguments: '8', popupmethod: 'PaymentGatewayManage.BindOrderList' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Setting'), name: 'setting', btntitle: 'Setting', cssclass: 'cssClassButtonHeader', controlclass: 'cssClassButtonSubmit', coltype: 'button', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: 'popuprel2', poparguments: '7,8', popupmethod: 'PaymentGatewayManage.LoadControl' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'HdnEdit'), name: 'HdnPaymentedit', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxPaymentGateWayManagement, 'HdnSetting'), name: 'Hdnsetting', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxPaymentGateWayManagement, 'HdnPaymentGatewayID'), name: 'HdnPaymentgatewayID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: getLocale(AspxPaymentGateWayManagement, 'logoUrl'), name: 'logoUrl', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],
                buttons: [{ display: getLocale(AspxPaymentGateWayManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'PaymentGatewayManage.EditPaymentMethod', arguments: '1,2,3,4,5,6,9' }
                               ],
                rp: perpage,
                nomsg: getLocale(AspxPaymentGateWayManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, paymentMethodObj: paymentMethodObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false }, 5: { sorter: false }, 6: { sorter: false }, 7: { sorter: false }, 8: { sorter: false }, 9: { sorter: false }, 10: { sorter: false} }
            });
        },

        Boolean: function (str) {
            switch (str.toLowerCase()) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        },

        EditPaymentMethod: function (tblID, argus) {
            PaymentGatewayManage.ImageUploader('fuLogo', 'logoPreview');
            switch (tblID) {
                case "gdvPaymentGateway":
                    $("#txtPaymentGatewayName").val(argus[3]);
                    $("#hdnPaymentGatewayID").val(argus[0]);
                    $("#hdnOldLogoUrl").val(argus[9]);
                    $("#chkIsActive").prop('checked', PaymentGatewayManage.Boolean(argus[4]));
                    chkIsActive = PaymentGatewayManage.Boolean(argus[4]);
                    $("#" + lblPaymentGatewayEdit).html(getLocale(AspxPaymentGateWayManagement, 'Editing Payment Gateway Method:') + argus[3]);
                    PaymentGatewayManage.HideAllDivs();
                    $("#chkIsUse").prop('checked', PaymentGatewayManage.Boolean(argus[5]));
                    $("#divPaymentGatewayEditForm").show();
                    tempOldLogo = argus[9];
                    if (argus[9] != "") {
                        $("#logoPreview").html('<img src=' + aspxRootPath + argus[9] + ' alt="' + argus[3] + '" width="100px"  height="60px" />');
                        $("#logoPreview").append('<img src="' + aspxRootPath + 'Administrator/Templates/Default/images/delete.png' + '" id="imgDelete" alt="Delete" title="Delete" />');
                        $("#imgDelete").off().click(function () {
                            $("#logoPreview").html('');
                            tempLogo = "";
                        });
                    }
                    if ($("#btnDeletePay").length > 0) {
                        $("#btnDeletePay").unbind().bind("click", function () {
                            var argx = [];
                            argx[0] = $("#hdnPaymentGatewayID").val();
                            PaymentGatewayManage.DeletePaymentMethod("gdvPaymentGateway", argx);
                        });
                    }

                    break;
                default:
                    break;
            }
        },

        HideAllDivs: function () {
            $("#divPaymentGateWayManagement").hide();
            $("#divPaymentGateWayForm").hide();
            $("#divPaymentGatewayEditForm").hide();
            $("#divPaymentEdit").hide();
            $("#divOrderDetailForm").hide();
            $("#divPaymentGateWayManagementEdit").hide();
        },

        HideDivsWhenError: function () {
            PaymentGatewayManage.HideAllDivs();
            $("#divPaymentGateWayForm").show();
        },

        LoadControl: function (argus, PopUpID) {
            var ControlName = argus[0];
            $("#hdnPaymentGatewayID").val(argus[1]);
            if (ControlName != '' && parseInt(ControlName) != 0) {
                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: aspxservicePath + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + aspxRootPath + ControlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        try {
                            $('#' + PopUpID).html(response.d);
                            if ((response.d).toLowerCase().indexOf("system.web.httpexception") >= 0) {
                                csscody.error('<h2>' + getLocale(AspxPaymentGateWayManagement, "Error Message") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "This Payment gateway is not available in your store!") + '</p>');
                            } else {
                                Setting.LoadPaymentGatewaySetting(argus[1], PopUpID);
                            }
                        } catch (e) {
                            $('#' + PopUpID).hide();
                            //alert("No Modules loaded for this payment method!!");
                            csscody.error('<h2>' + getLocale(AspxPaymentGateWayManagement, "Error Message") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "This Payment gateway is not available in your store!") + '</p>');
                        }
                    },
                    error: function () {
                        csscody.error('<h2>' + getLocale(AspxPaymentGateWayManagement, "Error Message") + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, "Failed to load!") + '</p>');
                    }
                });
            } else if ($.trim(parseInt(ControlName)) == 0) {
                csscody.alert('<h2>' + getLocale(AspxPaymentGateWayManagement, "Information Alert") + "</h2><p>" + getLocale(AspxPaymentGateWayManagement, "This Payment gateway is not available in your store!") + '</p>');
            } else {
                csscody.alert('<h2>' + getLocale(AspxPaymentGateWayManagement, "Information Alert") + "</h2><p>" + getLocale(AspxPaymentGateWayManagement, "This Payment gateway doesn\'t seem to need any settings!") + '</p>');
            }
        },

        BindOrderList: function (argus, billNm, shipNm, orderStatusType) {
            $("#hdnPaymentGatewayIDView").val(argus);
            if (billNm == undefined) {
                billNm = null;
            }
            if (shipNm == undefined) {
                shipNm = null;
            }
            if (orderStatusType == undefined) {
                orderStatusType = null;
            }
            var paymentGatewayId = $("#hdnPaymentGatewayIDView").val();
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPaymentGatewayEdit_pagesize").length > 0) ? $("#gdvPaymentGatewayEdit_pagesize :selected").text() : 10;
            PaymentGatewayManage.HideAllDivs();
            $("#divPaymentGateWayManagementEdit").show();
            var bindOrderObj = {
                PaymentGateWayID: paymentGatewayId,
                OrderStatusName: orderStatusType,
                BillToName: billNm,
                ShipToName: shipNm
            };
            $("#gdvPaymentGatewayEdit").sagegrid({
                url: PaymentGatewayManage.config.baseURL + "GetOrderDetailsbyPayID",
                colModel: [
                    { display: getLocale(AspxPaymentGateWayManagement, 'PaymentGatewayID'), name: 'paymentgateway_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'OrderDetailChkBox', elemDefault: false, controlclass: 'paymentOrderDetailChkbox' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Order ID'), name: 'order_Id', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: '', poparguments: '1,8', popupmethod: 'PaymentGatewayManage.LoadOrderDetails' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Store ID'), name: 'store_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Bill to Name'), name: 'bill_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Ship to Name'), name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Grand Total'), name: 'grand_total', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Order Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'IsMultipleShipping'), name: 'IsMultiShipping', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', hide: true, format: 'Yes/No' },
                    { display: getLocale(AspxPaymentGateWayManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],
                buttons: [{ display: getLocale(AspxPaymentGateWayManagement, 'View Order'), name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'PaymentGatewayManage.LoadOrderDetails', arguments: '1,8' }, { display: 'View Transaction Log', name: 'view log', enable: true, _event: 'click', trigger: '2', callMethod: 'PaymentGatewayManage.BindTransactionLog', arguments: '1,2' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxPaymentGateWayManagement, "No Records Found!"),
                param: { bindOrderObj: bindOrderObj, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false }, 9: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

        },

        GetOrderStatus: function () {
            this.config.url = this.config.baseURL + "GetStatusList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        LoadOrderDetails: function (table, argus) {
            $('#' + lblOrderDetailForm).html("Order ID: #" + argus[3]);
            var orderID = argus[3];
            PaymentGatewayManage.vars.IsMultipleShipping = argus[4];
            PaymentGatewayManage.config.url = PaymentGatewayManage.config.baseURL + "GetAllOrderDetailsForView";
            PaymentGatewayManage.config.data = JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj });
            PaymentGatewayManage.config.ajaxCallMode = 2;
            PaymentGatewayManage.ajaxCall(PaymentGatewayManage.config);
        },

        UpdatePaymentGatewayMethod: function () {
            var paymentGatewayID = $("#hdnPaymentGatewayID").val();
            var paymentMethodName = $("#txtPaymentGatewayName").val();
            var logoUrl = tempLogo;
            var isAct;
            if ($("#chkIsActive").length > 0)
                isAct = $("#chkIsActive").prop('checked');
            else
                isAct = chkIsActive;

            var isUse = $("#chkIsUse").prop('checked');
            var updatePaymentObj = {
                PaymentGateWayID: paymentGatewayID,
                PaymentGatewayName: paymentMethodName,
                IsActive: isAct,
                IsUse: isUse,
                LogoUrl: logoUrl,
                DestinationUrl: destination,
                OldLogoUrl: $("#hdnOldLogoUrl").val()
            };
            this.config.url = this.config.baseURL + "UpdatePaymentMethod";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, updatePaymentObj: updatePaymentObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        DeletePaymentMethod: function (tblID, argus) {
            switch (tblID) {
                case "gdvPaymentGateway":
                    var properties = {
                        onComplete: function (e) {
                            PaymentGatewayManage.DeletePaymentGateMethod(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxPaymentGateWayManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxPaymentGateWayManagement, 'Are you sure you want to delete this payment gateway?') + "</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultiplePayments: function (Ids, event) {
            if (event) {
                PaymentGatewayManage.DeletePaymentGateMethod(Ids, event);
            }
        },

        DeletePaymentGateMethod: function (_paymentGatewayID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeletePaymentMethod";
                this.config.data = JSON2.stringify({ paymentGatewayID: _paymentGatewayID, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
                return false;
            } else return false;
        },

        SearchPaymentgateway: function () {
            var paymentgatewayName = $.trim($("#txtSearchPaymentGateWayName").val());
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : $.trim($("#ddlIsActive").val()) == 0 ? true : false;
            if (paymentgatewayName.length < 1) {
                paymentgatewayName = null;
            }
            PaymentGatewayManage.BindPaymentMethodGrid(paymentgatewayName, isAct);
        },

        SearchOrders: function () {
            var paymentGatewayID = $("#hdnPaymentGatewayIDView").val();
            var billNm = $.trim($("#txtSearchBillToName").val());
            var shipNm = $.trim($("#txtSearchShipToName").val());
            var orderStatusType = '';
            if (billNm.length < 1) {
                billNm = null;
            }
            if (shipNm.length < 1) {
                shipNm = null;
            }
            if ($("#ddlOrderStatus").val() != "0") {
                orderStatusType = $.trim($("#ddlOrderStatus :selected").text());
            } else {
                orderStatusType = null;
            }
            PaymentGatewayManage.BindOrderList(paymentGatewayID, billNm, shipNm, orderStatusType);
        },
        ajaxSuccess: function (data) {
            switch (PaymentGatewayManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(data.d, function (index, item) {
                        var StatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                        $("#ddlOrderStatus").append(StatusElements);
                    });
                    break;
                case 2:
                    var elements = '';
                    var tableElements = '';
                    var grandTotal = '';
                    var couponAmount = '';
                    var taxTotal = '';
                    var shippingCost = '';
                    var discountAmount = '';
                    var additionalNote = "";
                    Array.prototype.clean = function (deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    tableElements += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><thead><tr class="cssClassHeading"><td>' + getLocale(AspxPaymentGateWayManagement, 'Item Name') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'Shipping Method') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'Shipping Address') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'Shipping Rate') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'Price') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'Quantity') + '</td><td>' + getLocale(AspxPaymentGateWayManagement, 'SubTotal') + '</td></tr></thead>';
                    tableElements += '<tbody><tr>';
                    $.each(data.d, function (index, value) {
                        if (index < 1) {
                            var billAdd = '';
                            var arrBill;
                            arrBill = value.BillingAddress.split(",");
                            billAdd += '<b>Billing Address:</b></br>';
                            billAdd += arrBill[0] + ' ' + arrBill[1] + '</br>';
                            if (arrBill[2] != undefined && arrBill[2] != " ") {
                                billAdd += arrBill[2] + '</br>';
                            }
                            if (arrBill[3] != undefined && arrBill[3] != " ") {
                                billAdd += arrBill[3] + '</br>';
                            }
                            if (arrBill[4] != undefined && arrBill[4] != " ") {
                                billAdd += arrBill[4] + '</br>';
                            }
                            if (arrBill[5] != undefined && arrBill[5] != " ") {
                                billAdd += arrBill[5] + ' ';
                            }
                            if (arrBill[6] != undefined && arrBill[6] != " ") {
                                billAdd += arrBill[6] + ' ';
                            }
                            if (arrBill[7] != undefined && arrBill[7] != " ") {
                                billAdd += arrBill[7] + '</br>';
                            }
                            if (arrBill[8] != undefined && arrBill[8] != " ") {
                                billAdd += arrBill[8] + '</br>';
                            }
                            if (arrBill[9] != undefined && arrBill[9] != " ") {
                                billAdd += arrBill[9] + '</br>';
                            }
                            if (arrBill[10] != undefined && arrBill[10] != " ") {
                                billAdd += arrBill[10] + ' ';
                            }
                            if (arrBill[11] != undefined && arrBill[11] != " ") {
                                billAdd += arrBill[11] + '</br>';
                            }
                            if (arrBill[12] != undefined && arrBill[12] != " ") {
                                billAdd += arrBill[12] + '</br>';
                            }
                            if (arrBill[13] != undefined && arrBill[13] != " ") {
                                billAdd += arrBill[13] + '</br>';
                            }

                            $("#divBillingAddressDetail").html(billAdd);
                            $("#OrderDate").html(value.OrderedDate);
                            $("#PaymentGatewayType").html(value.PaymentGatewayTypeName);
                            $("#PaymentMethod").html(value.PaymentMethodName);
                            additionalNote = value.Remarks;
                            $("#storeName").html(value.StoreName);
                            $("#storeDescription").html(value.StoreDescription);
                        }
                        var shippingAddress = new Array();
                        var shipAdd = '';
                        shippingAddress = value.ShippingAddress.replace(",", " ").split(",");

                        shippingAddress.clean(" ");
                        if (value.CostVariants != "") {
                            tableElements += '<td valign="top" class="cssClassItemName">' + value.ItemName + ' ' + '(' + value.CostVariants + ')' + '</td>';
                        } else {
                            tableElements += '<td valign="top" class="cssClassItemName">' + value.ItemName + '</td>';
                        }
                        tableElements += '<td valign="top" class="cssClassShippingMethod">' + value.ShippingMethod + '</td>';
                        tableElements += '<td valign="top" class="cssClassShippingAdress">' + shippingAddress + '</td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassShippingRate" >' + value.ShippingRate.toFixed(2) + '</span></td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassPrice" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td valign="top" class="cssClassQuantity">' + value.Quantity + '</td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            taxTotal = '<tr>';
                            taxTotal += '<td class="cssClassItemName cssNoData">&nbsp;</td><td class="cssClassShippingMethod cssNoData">&nbsp;</td><td class="cssClassShippingAdress cssNoData">&nbsp;</td><td class="cssClassShippingRate cssNoData">&nbsp;</td><td class="cssClassPrice cssNoData">&nbsp;</td><td class="cssClassLabel cssClassQuantity"><b>' + getLocale(AspxPaymentGateWayManagement, 'Tax Total:') + '</b></td>';
                            taxTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.TaxTotal.toFixed(2) + '</span></td>';
                            taxTotal += '</tr>';
                            shippingCost = '<tr>';
                            shippingCost += '<td class="cssClassItemName cssNoData">&nbsp;</td><td class="cssClassShippingMethod cssNoData">&nbsp;</td><td class="cssClassShippingAdress cssNoData">&nbsp;</td><td class="cssClassShippingRate cssNoData">&nbsp;</td><td class="cssClassPrice cssNoData">&nbsp;</td><td class="cssClassLabel cssClassQuantity"><b>' + getLocale(AspxPaymentGateWayManagement, 'Shipping Cost:') + '</b></td>';
                            shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.TotalShippingCost.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            discountAmount = '<tr>';
                            discountAmount += '<td class="cssClassItemName cssNoData">&nbsp;</td><td class="cssClassShippingMethod cssNoData">&nbsp;</td><td class="cssClassShippingAdress cssNoData">&nbsp;</td><td class="cssClassShippingRate cssNoData">&nbsp;</td><td class="cssClassPrice cssNoData">&nbsp;</td><td class="cssClassLabel cssClassQuantity"><b>' + getLocale(AspxPaymentGateWayManagement, 'Discount Amount:') + '</b></td>';
                            discountAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal"> - ' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            discountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td class="cssClassItemName cssNoData">&nbsp;</td><td class="cssClassShippingMethod cssNoData">&nbsp;</td><td class="cssClassShippingAdress cssNoData">&nbsp;</td><td class="cssClassShippingRate cssNoData">&nbsp;</td><td class="cssClassPrice cssNoData">&nbsp;</td><td class="cssClassLabel cssClassQuantity"><b>' + getLocale(AspxPaymentGateWayManagement, 'Coupon Amount:') + '</b></td>';
                            couponAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal"> - ' + value.CouponAmount.toFixed(2) + '</span></span></td>';
                            couponAmount += '</tr>';
                            grandTotal = '<tr>';
                            grandTotal += '<td class="cssClassItemName cssNoData">&nbsp;</td><td class="cssClassShippingMethod cssNoData">&nbsp;</td><td class="cssClassShippingAdress cssNoData">&nbsp;</td><td class="cssClassShippingRate cssNoData">&nbsp;</td><td class="cssClassPrice cssNoData">&nbsp;</td><td class="cssClassLabel cssClassQuantity"><b>' + getLocale(AspxPaymentGateWayManagement, 'Grand Total:') + '</b></td>';
                            grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                        }
                    });
                    tableElements += '</tbody></table>';
                    $("#orderItemDetail").html(tableElements);
                    $("#orderItemDetail").find('table>tbody').append(taxTotal);
                    $("#orderItemDetail").find('table>tbody').append(shippingCost);
                    $("#orderItemDetail").find('table>tbody').append(discountAmount);
                    $("#orderItemDetail").find('table>tbody').append(couponAmount);
                    $("#orderItemDetail").find('table>tbody').append(grandTotal);
                    $("#orderItemDetail").find("table>tbody tr:even").addClass("sfEven");
                    $("#orderItemDetail").find("table>tbody tr:odd").addClass("sfOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    PaymentGatewayManage.HideAllDivs();
                    $("#divOrderDetailForm").show();
                    break;
                case 3:
                    PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                    SageFrame.messaging.show("Payment Gateway Method saved successfully!", "Success");
                    break;
                case 4:
                    //window.location.href = urlPath + "?deleted=true";
                    PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                    PaymentGatewayManage.HideAllDivs();
                    SageFrame.messaging.show("Payment Gateway Method has been deleted successfully!", "Success");
                    $("#divPaymentGateWayManagement").show();
                    break;
                case 5:
                    if (data.d.length > 0) {
                        PaymentGatewayManage.GetDetailTransaction(data.d);
                    } else {
                        PaymentGatewayManage.GetDetailTransaction(null);
                    }
                    break;
            }
        }
    };
    PaymentGatewayManage.init();
});