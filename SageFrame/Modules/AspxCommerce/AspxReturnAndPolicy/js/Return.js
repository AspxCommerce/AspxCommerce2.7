var ReturnManage;
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
    var msgbody = '';
                            function aspxCommonObj() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            SessionCode: AspxCommerce.utils.GetSessionCode(),
            CustomerID: AspxCommerce.utils.GetCustomerID()
        };
        return aspxCommonInfo;
    }

    ReturnManage = {
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
        init: function () {
            ReturnManage.LoadReturnMgmtStaticImage();
            ReturnManage.HideAll();
            $("#selectShippingMethod").hide();
            $("#divReturnDetails").show();
            $("#txtDateAdded").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtDateModified").datepicker({ dateFormat: 'yy/mm/dd' });
            ReturnManage.BindReturnDetails(null, null, null, null, null, null);
            ReturnManage.GetReturnStatus();
            ReturnManage.ForceNumericInput();
            $("#btnBack").click(function () {
                ReturnManage.ClearAll();
                ReturnManage.HideAll();
                $("#divReturnDetails").show();
            });
            $("#btnSPBack").click(function () {
                ReturnManage.HideAll();
                $("#divReturnDetails").show();
            });
            $("#btnUpdateReturnStatus").click(function () {
                var rtStatus = $("#selectReturnStatus option:selected").val();
                var rtAction = $("#selectReturnAction option:selected").val();
                var otherPostalCharges = $("#txtOtherPostalCharges").val();
                if (rtStatus == 2) {
                    if (rtAction != 0) {
                        ReturnManage.ReturnUpdate();
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxReturnAndPolicy, "Information Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Please select return action first to update return status") + '</p>');
                    }
                }
                else {
                    if (rtAction != 0) {
                        csscody.alert('<h2>' + getLocale(AspxReturnAndPolicy, "Information Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "No Return Action could be choosen if return status is Pending or Awaiting Products") + '</p>');

                    }
                    else {
                        if (otherPostalCharges == 0) {
                            ReturnManage.ReturnUpdate();
                        }
                        else {
                            csscody.alert('<h2>' + getLocale(AspxReturnAndPolicy, "Information Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Other postal charges can not be applied if Status is not completed") + '</p>');
                        }
                    }

                }
            });
            $("#btnSaveComments").click(function () {
                ReturnManage.SaveComments();
            });

            $('#selectReturnAction').change(function () {
                var rtnActionID = $("#selectReturnAction option:selected").val();
                var rtnStatusID = $("#selectReturnStatus option:selected").val();
                if (rtnActionID == 3) {
                    $("#selectShippingMethod").show();
                    $("#spanShippingMethod").html(getLocale(AspxReturnAndPolicy, "Shipping Method : "));
                }
                else {
                    $("#selectShippingMethod").hide();
                    $("#spanShippingMethod").html("");
                }
            });

        },
        ajaxCall: function (config) {
            $.ajax({
                type: ReturnManage.config.type,
                contentType: ReturnManage.config.contentType,
                cache: ReturnManage.config.cache,
                async: ReturnManage.config.async,
                data: ReturnManage.config.data,
                dataType: ReturnManage.config.dataType,
                url: ReturnManage.config.url,
                success: ReturnManage.ajaxSuccess,
                error: ReturnManage.ajaxFailure
            });
        },

        LoadReturnMgmtStaticImage: function () {
            $('#ajaxReturnMgmtStaticImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideAll: function () {
            $("#divReturnDetails").hide();
            $("#divReturnDetailForm").parent('div').hide();
            $("#divEditReturnStatus").hide();
        },
        ForceNumericInput: function () {
            $("#txtReturnID,#txtOrderID").keydown(function (e) {
                               if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                                       return;
                }
                               if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
                if (e.keyCode == 96) {
                    if ($(this).val() == 0) {
                        e.preventDefault();
                    }
                }
                if (e.keyCode == 8) {
                    if (($(this).val() > 0) && ($(this).val() < 9)) {
                        e.preventDefault();
                    }
                }

            });
        },
        BindReturnDetails: function (returnID, orderID, customerNm, returnStatusType, dateAdded, dateModified) {
            var returnDetailObj = {
                ReturnID: returnID,
                OrderID: orderID,
                CustomerName: customerNm,
                ReturnStatus: returnStatusType,
                DateAdded: dateAdded,
                DateModified: dateModified
            };
            this.config.method = "GetReturnDetails";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvReturnDetails_pagesize").length > 0) ? $("#gdvReturnDetails_pagesize :selected").text() : 10;

            $("#gdvReturnDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: getLocale(AspxReturnAndPolicy, 'Return ID'), name: 'return_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Order ID'), name: 'order_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'SKU'), name: 'sku', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Ordered Date'), name: 'ordered_date', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'CustomerID'), name: 'customerID', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'Customer'), name: 'customer', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Return Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Date Added'), name: 'date_added', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Date Modified'), name: 'date_modified', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReturnAndPolicy, 'Return Action'), name: 'return_action', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'Shipping Method ID'), name: 'shipping_methodid', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'UserName'), name: 'user_name', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'Other Postal Charges'), name: 'otherPostalCharges', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReturnAndPolicy, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: getLocale(AspxReturnAndPolicy, 'View'), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'ReturnManage.ViewReturns', arguments: '1,2,3,4,5,6,7,8,9,10,11,12' },
                    { display: getLocale(AspxReturnAndPolicy, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ReturnManage.EditReturns', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxReturnAndPolicy, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj(), returnDetailObj: returnDetailObj },
                current: current_,
                pnew: offset_,
                sortcol: { 12: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ViewReturns: function (tblID, argus) {
            switch (tblID) {
                case "gdvReturnDetails":
                    ReturnManage.HideAll();
                    $('#' + lblReturnForm1).html(getLocale(AspxReturnAndPolicy, "Order ID: #") + argus[3]);
                    $("#divReturnDetailForm").parent('div').show();
                    ReturnManage.BindAllReturnDetailsForm(argus[0], argus[3], argus[7], argus[14]);

                    ReturnManage.ViewComments(argus[0], argus[3]);
                    $("#hdnReturnID").val(argus[0]);
                    $("#hdnOrderID").val(argus[3]);
                    $("#hdnCustomerID").val(argus[7]);
                    $("#hdnUserName").val(argus[14]);

                    break;
                default:
                    break;
            }
        },
        ClearAll: function () {
            $("#hdnReturnID").val('');
            $("#hdnOrderID").val('');
            $("#SPOrderID").html('');

        },
        EditReturns: function (tblID, argus) {

            switch (tblID) {
                case "gdvReturnDetails":

                    ReturnManage.HideAll();
                    $("#divEditReturnStatus").show();
                    $("#spanReturnFiledDate").html(argus[10]);
                    $("#spanOrderDate").html(argus[6]);
                    $("#spanReturnID").html(argus[0]);
                    $("#spanOrderID").html(argus[3]);
                    $("#spanCustomerName").html(argus[8]);
                    $('#selectReturnStatus').val($('#ddlReturnStatus option:contains(' + argus[9] + ')').attr('value'));
                    ReturnManage.GetReturnAction();
                    ReturnManage.GetShippingMethods(argus[0], argus[3]);
                    var returnActionID = argus[12];
                    var shippingMethodID = argus[13];
                    if (returnActionID != '' && returnActionID != null) {
                        $("#selectReturnAction").attr('selectedIndex', returnActionID);
                    }
                    if (shippingMethodID != '' && shippingMethodID != null) {
                        $("#selectShippingMethod").attr('selectedIndex', shippingMethodID);
                    }
                    $("#txtOtherPostalCharges").val(argus[15]);

                    $("#hdnReturnID").val(argus[0]);
                    $("#hdnOrderID").val(argus[3]);
                    break;
            }
        },

        ReturnUpdate: function () {
            var orderID = $("#spanOrderID").text();
            var returnID = $("#spanReturnID").text();
            var shippingCost = $("#hdnShippingCost").text();
            var otherPostalCharges = $("#txtOtherPostalCharges").val();
            if (shippingCost == '') {
                shippingCost = 0;
            }
            if (otherPostalCharges == '') {
                otherPostalCharges = 0;
            }
            var returnActionID = $("#selectReturnAction option:selected").val();
            var returnStatusID = $("#selectReturnStatus option:selected").val();
            var shippingMethodID = $("#selectShippingMethod option:selected").val();
            var returnDetailObj = {
                ReturnID: returnID,
                OrderID: orderID,
                ReturnActionID: returnActionID,
                ReturnStatusID: returnStatusID,
                ShippingMethodID: shippingMethodID,
                ShippingCost: shippingCost,
                OtherPostalCharges: otherPostalCharges
            };
            this.config.url = this.config.baseURL + "ReturnUpdate";
            this.config.data = JSON2.stringify({ returnDetailObj: returnDetailObj, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },


        SaveComments: function () {
            var orderID = $("#hdnOrderID").val();
            var returnID = $("#hdnReturnID").val();
            var commentText = Encoder.htmlEncode($("#txtAreaComments").val());
            var isCustomerNotifiedByEmail = $('#chkIsCustomerNotifyByEmail').prop('checked') ? 1 : 0;
            if (commentText == '' || commentText == null) {
                csscody.alert('<h2>' + getLocale(AspxReturnAndPolicy, "Information Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "You can not send blank comments") + '</p>');
            }
            else {
                var returnDetailObj = {
                    ReturnID: parseInt(returnID),
                    OrderID: orderID,
                    CommentText: commentText,
                    IsCustomerNotifiedByEmail: isCustomerNotifiedByEmail
                };

                this.config.url = this.config.baseURL + "ReturnSaveComments";
                this.config.data = JSON2.stringify({ returnDetailObj: returnDetailObj, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            }
        },
        ViewComments: function (returnId, orderId) {
            var returnDetailObj = {
                ReturnID: returnId,
                OrderID: orderId
            };
            this.config.url = this.config.baseURL + "GetMyReturnsComment";
            this.config.data = JSON2.stringify({ returnDetailObj: returnDetailObj, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        },
        BindAllReturnDetailsForm: function (returnId, orderId, customerId, userName) {
                                                                                                    aspxCommonObj1 = aspxCommonObj();
            aspxCommonObj1.CustomerID = customerId;
            aspxCommonObj1.UserName = userName;
            var returnDetailObj = {
                ReturnID: parseInt(returnId),
                OrderID: orderId
            };
            this.config.url = this.config.baseURL + "GetMyReturnsDetails";
            this.config.data = JSON2.stringify({ returnDetailObj: returnDetailObj, aspxCommonObj: aspxCommonObj1 });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        GetReturnStatus: function () {
            $("#selectReturnStatus").empty();
            this.config.url = this.config.baseURL + "GetReturnStatusList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        GetReturnAction: function () {
            $("#selectReturnAction").empty();
            this.config.url = this.config.baseURL + "GetReturnActionList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 6;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        GetShippingMethods: function (returnId, orderId) {
            $("#selectShippingMethod").empty();
                       var returnDetailObj = {
                ReturnID: returnId,
                OrderID: orderId
            };
            this.config.url = this.config.baseURL + "GetMyReturnsShippingMethod";
            this.config.data = JSON2.stringify({ returnDetailObj: returnDetailObj, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },
        SearchReturns: function () {
            var returnID = $.trim($("#txtReturnID").val());
            var orderID = $.trim($("#txtOrderID").val());
            var customerNm = $.trim($("#txtCustomerName").val());
            var returnStatusType = '';
            var dateAdded = $.trim($("#txtDateAdded").val());
            var dateModified = $.trim($("#txtDateModified").val());

            if (returnID.length < 1) {
                returnID = null;
            }
            if (orderID.length < 1) {
                orderID = null;
            }
            if (customerNm.length < 1) {
                customerNm = null;
            }

            if ($("#ddlReturnStatus option:selected").val() == "0") {
                returnStatusType = null;
            }
            else {
                returnStatusType = $("#ddlReturnStatus option:selected").text();
            }
            if (dateAdded.length < 1) {
                dateAdded = null;
            }
            if (dateModified.length < 1) {
                dateModified = null;
            }

            ReturnManage.BindReturnDetails(returnID, orderID, customerNm, returnStatusType, dateAdded, dateModified);
        },

        SendEmail: function () {

            var subject = "Return Notification";
            var message = $("#txtAreaComments").val();
            var recieverEmail = $("#hdnRcvrEmail").val();
            var rtnID = $("#hdnrtnID").val();
            var ordID = $("#hdnOrderID").val();
            var itemName = $("#hdnItemName").val();
            var variant = $("#hdnVariants").val();
            var qty = $("#hdnQty").val();
            var status = $("#hdnReturnStatus").val();
            var action = $("#hdnIsReturnAction").val();
            var bodyDetails = ordID + "#" + rtnID + "#" + itemName + "#" + variant + "#" + qty + "#" + status + "#" + action;

            var sendEmailObj = {
                SenderName: userName,
                SenderEmail: senderEmail,
                ReceiverEmail: recieverEmail,
                Subject: subject,
                Message: message,
                MessageBody: bodyDetails
            }

            this.config.url = this.config.baseURL + "ReturnSendEmail";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), sendEmailObj: sendEmailObj });
            this.config.ajaxCallMode = 9;
            this.config.async = false;
            this.ajaxCall(this.config);

        },
        ajaxSuccess: function (data) {
            switch (ReturnManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info("<h2>" + getLocale(AspxReturnAndPolicy, "Successful Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Saved Successfully") + "</p>");
                    ReturnManage.BindReturnDetails(null, null, null, null, null, null);
                    var ReturnId = $("#hdnReturnID").val();
                    $("#txtOtherPostalCharges").val('');
                    ReturnManage.HideAll();
                    $("#divReturnDetails").show();
                    break;

                case 2:
                    var commentsList = '';
                    $.each(data.d, function (index, value) {
                        if (value.CustomerID != 1 && value.CustomerID != 2) {
                            commentsList += '<hr /><li><span>' + getLocale(AspxReturnAndPolicy, "Commented By:") + '</span>' + ' ' + value.AddedBy + ' ' + '<span> | </span>' + ' ' + getLocale(AspxReturnAndPolicy, 'Admin Notified by Email :') + ' ' + value.IsNotified + ' ' + '<span> | </span>' + value.AddedOn + '</li>';

                            commentsList += '<li><span>' + getLocale(AspxReturnAndPolicy, 'Comments:') + '</span>' + ' ' + value.CommentText + '</li></br>';
                        }
                        else {
                            commentsList += '<hr /><li><span>' + getLocale(AspxReturnAndPolicy, "Commented By:") + '</span>' + ' ' + value.AddedBy + ' ' + '<span> | </span>' + ' ' + getLocale(AspxReturnAndPolicy, 'Customer Notified by Email : ') + ' ' + value.IsNotified + ' ' + '<span> | </span>' + value.AddedOn + '</li>';
                            commentsList += '<li><span>' + getLocale(AspxReturnAndPolicy, "Comments:") + '</span>' + ' ' + value.CommentText + '</li></br>';
                        }
                    });
                    $('#ulCommentsList').html(commentsList);
                    $('#chkIsCustomerNotifyByEmail').prop('checked', false);
                    break;

                case 3:
                    var elements = '';
                    var tableElements = '';
                    var CustomerNm = '';
                    var Email = '';
                    $("#txtAreaComments").val("");
                    $.each(data.d, function (index, value) {
                        Array.prototype.clean = function (deleteValue) {
                            for (var i = 0; i < this.length; i++) {
                                if (this[i] == deleteValue) {
                                    this.splice(i, 1);
                                    i--;
                                }
                            }
                            return this;
                        };
                        if (index < 1) {
                            var billAdd = '';
                            var arrBill;
                            arrBill = value.ShippingAddress.split(',');
                            $.each(arrBill, function (index, value) {
                                billAdd += '<li>' + value + '</li>';
                            });
                            $('#ulOrderAddress').html(billAdd);
                            $("#returnFiledDate").html(value.ReturnFileDate);
                        }
                        if (index < 1) {
                            var returnAdd = '';
                            var arrReturn;
                            arrReturn = value.ReturnAddress.split(',');
                            $.each(arrReturn, function (index, value) {
                                returnAdd += '<li>' + value + '</li>';
                            });
                            $("#ulReturnAddress").html(returnAdd);
                            CustomerNm = arrReturn[0] + arrReturn[1];
                            Email = arrReturn[9];
                            $("#hdnRcvrEmail").val(Email);
                        }
                        if (index < 1) {
                            var shippingMethod = '';
                            if (value.ShippingMethodName != '' && value.ShippingMethodName != null) {
                                shippingMethod += '<li>' + '<b>' + getLocale(AspxReturnAndPolicy, "Shipping Method Name :") + '</b>' + ' ' + value.ShippingMethodName + '</li>';
                                shippingMethod += '<li>' + '<b>' + getLocale(AspxReturnAndPolicy, "Shipping Provider Name :") + '</b>' + ' ' + value.ShippingProviderName + '</li>';
                                $("#ulShippingMethods").html(shippingMethod);
                                $('#ShippingDetails').show();
                            }
                            else {
                                $('#ShippingDetails').hide();
                            }

                        }
                        tableElements += '<tr>';
                        tableElements += '<td class="cssClassReturnHistoryReturnID"><input type="hidden" size="3" id="hdnrtnID" class="cssClasshdnrtnID" value="' + value.ReturnID + '">' + value.ReturnID + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryItemName"><input type="hidden" size="3" id="hdnItemName" class="cssClasshdnItemName" value="' + value.ItemName + '">' + value.ItemName + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryCostVariants"><input type="hidden" size="3" id="hdnVariants" class="cssClasshdnVariants" value="' + value.CostVariants + '">' + value.CostVariants + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryQuantity"><input type="hidden" size="3" id="hdnQty" class="cssClasshdnQty" value="' + value.Quantity + '">' + value.Quantity + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryReturnReason">' + value.ReturnReason + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryOtherDetails">' + value.OtherFaults + '</td>';
                        tableElements += '<td class="cssClassReturnHistoryRetrunProductStatus">' + value.RetrunProductStatus + '</td>';
                        tableElements += '<td class="cssClassReturnHistorySKU"><input type="hidden" size="3" id="hdnReturnStatus" class="cssClasshdnReturnStatus" value="' + value.ReturnStatus + '">' + value.ReturnStatus + '</td>';

                        if (value.ReturnAction == "")
                        {
                            value.ReturnAction = getLocale(AspxReturnAndPolicy, "No Action Taken");
                        }
                        tableElements += '<td class="cssClassReturnHistoryReturnAction"><input type="hidden" size="3" id="hdnIsReturnAction" class="cssClasshdnIsReturnAction" value="' + value.ReturnAction + '">' + value.ReturnAction + '</td>';
                        tableElements += '</tr>';


                    });
                    $("#divItemReturnDetailsForm").find('table>tbody').html(tableElements);
                    ReturnManage.HideAll();
                    $("#divReturnDetailForm").parent('div').show();
                    break;
                case 4:
                    $.each(data.d, function (index, item) {
                        var returnStatusElements = "<option value=" + item.Value + ">" + item.Text + "</option>";
                        $("#ddlReturnStatus").append(returnStatusElements);
                        $("#selectReturnStatus").append(returnStatusElements);
                    });
                    break;
                case 6:
                    var returnActionElements1 = "<option value='0'>----Select----</option>";
                    var returnActionElements = '';
                    var total = '';
                    $.each(data.d, function (index, item) {
                        returnActionElements += "<option value=" + item.Value + ">" + item.Text + "</option>";
                    });
                    total = returnActionElements1 + returnActionElements;
                    $("#selectReturnAction").append(total);
                    break;
                case 7:

                    var shippingMehodElements1 = "<option value='0'>----Select----</option>";
                    var shippingMehodElements = '';
                    var totalShp = '';
                    $.each(data.d, function (index, item) {
                        shippingMehodElements += "<option value=" + item.ShippingMethodID + ">" + item.ShippingMethodName + "</option>";
                        $("#hdnShippingCost").html(item.ShippingCost);
                    });
                    totalShp = shippingMehodElements1 + shippingMehodElements;
                    $("#selectShippingMethod").append(totalShp);
                    break;
                case 8:

                    var rtrnId = $("#hdnReturnID").val();
                    var ordrId = $("#hdnOrderID").val();
                    var custID = $("#hdnCustomerID").val();
                    var usrNm = $("#hdnUserName").val();
                    csscody.info("<h2>" + getLocale(AspxReturnAndPolicy, "Successful Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Comment Posted Successfully") + "</p>");
                    var isCustomerNotifiedByEmail = $('#chkIsCustomerNotifyByEmail').prop('checked') ? 1 : 0;
                    if (isCustomerNotifiedByEmail == 1) {
                        ReturnManage.SendEmail();
                        $('#chkIsCustomerNotifyByEmail').attr('checked', false);
                    }
                    else {
                        $("#txtAreaComments").val('');
                    }
                    ReturnManage.BindAllReturnDetailsForm(rtrnId, ordrId, custID, usrNm);
                    ReturnManage.ViewComments(rtrnId, ordrId);
                    break;
                case 9:
                    $("#txtAreaComments").val('');
                    $('#chkIsCustomerNotifyByEmail').prop('checked', false)
                    csscody.info("<h2>" + getLocale(AspxReturnAndPolicy, "Sucess Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Notification email has been send successfully.") + "</p>");
            }
            if (allowRealTimeNotifications.toLowerCase() == 'true') {
                UpdateNotifications(1);
            }
        }
    };
    ReturnManage.init();
});
