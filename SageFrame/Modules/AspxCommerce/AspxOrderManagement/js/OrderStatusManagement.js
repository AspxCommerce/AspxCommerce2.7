var OrderStatusMgmt = "";

$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var editFlag = 0;
    var isUnique = false;
    var isNewStatus = false;
    var OrderStatusID;
    OrderStatusMgmt = {
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
            ajaxCallMode: 0        },

        ajaxCall: function(config) {
            $.ajax({
                type: OrderStatusMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: OrderStatusMgmt.config.contentType,
                cache: OrderStatusMgmt.config.cache,
                async: OrderStatusMgmt.config.async,
                url: OrderStatusMgmt.config.url,
                data: OrderStatusMgmt.config.data,
                dataType: OrderStatusMgmt.config.dataType,
                success: OrderStatusMgmt.ajaxSuccess,
                error: OrderStatusMgmt.ajaxFailure
            });
        },

        HideAlldiv: function() {
            $('#divOrderStatusDetail').hide();
            $('#divEditOrderStatus').hide();
        },

        Reset: function() {
            $('#txtOrderStatusAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $("#chkIsActiveOrder").removeAttr('disabled');
            $("#chkIsActiveOrder").removeAttr('checked');

            $("#chkIsReduceQuantity").removeAttr('disabled');
            $("#chkIsReduceQuantity").removeAttr('checked');
        },

        ClearForm: function() {
            $("#btnSaveOrderStatus").removeAttr("name");
            $('#' + lblHeading).html(getLocale(AspxOrderManagement,"Add New Order Status"));

            $('#txtOrderStatusAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $("#chkIsActiveOrder").removeAttr('checked');
            $("#isActiveTR").show();
            $("#hdnIsSystem").val(false);

            $('#txtOrderStatusAliasName').removeClass('error');
            $('#txtOrderStatusAliasName').parents('td').find('label').remove();
            $('#txtAliasToolTip').removeClass('error');
            $('#txtAliasToolTip').parents('td').find('label').remove();
            $('#osErrorLabel').html('');
        },
        LoadOrderStatusStaticImage: function() {
                       $('#ajaxOrderStatusMgmtImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        BindOrdersStatusInGrid: function(OrderSatatusName, isAct) {
            this.config.method = "GetAllStatusList";
            this.config.url = this.config.baseURL;
            var tempObj = aspxCommonObj;
            tempObj.CultureName = AspxCommerce.utils.GetCultureName();
            this.config.data = { aspxCommonObj: tempObj, orderStatusName: OrderSatatusName, isActive: isAct };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblOrderStatusDetails_pagesize").length > 0) ? $("#tblOrderStatusDetails_pagesize :selected").text() : 10;

            $("#tblOrderStatusDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxOrderManagement,'Order Status ID'), name: 'OrderStatusID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: getLocale(AspxOrderManagement,'Order Status Name'), name: 'OrderStatusAliasName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOrderManagement,'Alias Tool Tip'), name: 'AliasToolTip', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOrderManagement,'Alias Help'), name: 'AliasHelp', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxOrderManagement,'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: getLocale(AspxOrderManagement,'System'), name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxOrderManagement,'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxOrderManagement,'Reduce Item Quantity'), name: 'ReduceQuantity', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxOrderManagement,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: getLocale(AspxOrderManagement,'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'OrderStatusMgmt.EditOrderStatus', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxOrderManagement,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'OrderStatusMgmt.DeleteOrderStatus', arguments: '1,5' }
                                              ],
                rp: perpage,
                nomsg: 'No Records Found!',//getLocale(AspxOrderManagement,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false} }
            });
        },

        Boolean: function(str) {
            switch (str.toLowerCase()) {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        },

        EditOrderStatus: function(tblID, argus) {
            switch (tblID) {
                case "tblOrderStatusDetails":
                    isNewStatus = false;
                    $('#languageSelect').find('li').each(function () {
                        if ($(this).attr("value") == aspxCommonObj.CultureName) {
                            $('#languageSelect').find('li').removeClass("languageSelected");
                            $(this).addClass("languageSelected");
                            return;

                        }
                    });
                    editFlag = argus[0];
                    OrderStatusMgmt.ClearForm();
                    OrderStatusID=argus[0];
                    $("#btnReset").hide();
                    $('#divOrderStatusDetail').hide();
                    $('#divEditOrderStatus').show();
                    $('#' + lblHeading).html(getLocale(AspxOrderManagement,"Edit Order Status:")+" '" + argus[3] + "'");
                    $('#txtOrderStatusAliasName').val(argus[3]);
                    $('#txtAliasToolTip').val(argus[4]);
                    $('#txtAliasHelp').val(argus[5]);
                    $("#chkIsActiveOrder").prop('checked', OrderStatusMgmt.Boolean(argus[8]));
                    $("#btnSaveOrderStatus").prop("name", argus[0]);
                    $("#chkIsReduceQuantity").prop("checked", OrderStatusMgmt.Boolean(argus[9]));
                    if (argus[7].toLowerCase() != "yes") {
                        $("#isActiveTR").show();
                        $("#hdnIsSystem").val(false);
                        $("#chkIsActiveOrder").removeAttr('disabled');
                        $("#chkIsReduceQuantity").removeAttr('disabled');
                    } else {
                        $("#isActiveTR").show();
                        $("#hdnIsSystem").val(true);
                        $("#chkIsActiveOrder").prop('disabled', 'disabled');
                        $("#chkIsReduceQuantity").prop('disabled', 'disabled');
                                                                                         }
                    break;
                default:
                    break;
            }
        },
        DeleteOrderStatus: function(tblID, argus) {
            switch (tblID) {
                case "tblOrderStatusDetails":
                    if (argus[4].toLowerCase() != "yes") {
                        OrderStatusMgmt.DeleteAttribute(argus[0]);
                    } else {
                        csscody.alert('<h2>'+getLocale(AspxOrderManagement,'Information Alert')+'</h2><p>'+getLocale(AspxOrderManagement,'Sorry! System status can not be deleted.')+'</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteAttribute: function(_orderStatusId) {
            var properties = {
                onComplete: function(e) {
                    OrderStatusMgmt.ConfirmSingleDelete(_orderStatusId, e);

                }
            };
                       csscody.confirm("<h2>"+getLocale(AspxOrderManagement,'Delete Confirmation')+'</h2><p>'+getLocale(AspxOrderManagement,'Are you sure you want to delete this order status?')+"</p>", properties);
        },

        ConfirmSingleDelete: function(_orderStatusId, event) {
            if (event) {
                OrderStatusMgmt.DeleteSingleAttribute(_orderStatusId);
            }
            return false;
        },

        ConfirmDeleteMultiple: function(orderStatus_ids, event) {
            if (event) {
                OrderStatusMgmt.DeleteMultipleAttribute(orderStatus_ids);
            }
        },

        DeleteSingleAttribute: function(_orderStatusId) {
            this.config.method = "DeleteOrderStatusByID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderStatusID: parseInt(_orderStatusId), aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        DeleteMultipleAttribute: function(orderStatus_ids) {
            this.config.method = "DeleteOrderStatusMultipleSelected";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderStatusIDs: orderStatus_ids, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        RebindStatusOnLangugeChange: function () {
            //Added for rebinding data in language select options
            if (isNewStatus) {
                return false;
            }
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            if ($("#languageSelect").length > 0) {
                aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            };

            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: OrderStatusMgmt.config.baseURL + "GetOrderStatusDetailByOrderStatusID",
                data: JSON2.stringify({ aspxCommonObj: aspxCommonInfo, OrderStatusID: OrderStatusID}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async:false,
                success: function (msg) { 
                    if (msg.d) {
                        $('#txtOrderStatusAliasName').val(msg.d.OrderStatusAliasName);
                        $('#txtAliasToolTip').val(msg.d.AliasToolTip);
                        $('#txtAliasHelp').val(msg.d.AliasHelp);
                    }
                },
                error: function (msg) {
                    console.log(msg.d);
                    alert(msg.d);
                }
            });
        },
        SaveOrderStatus: function(OrderStatusID) {
            editFlag = OrderStatusID;
            var OrderStatusAliasName = $('#txtOrderStatusAliasName').val();
            var AliasToolTip = $('#txtAliasToolTip').val();
            var AliasHelp = $('#txtAliasHelp').val();
            var IsActive = $("#chkIsActiveOrder").prop('checked');
            var IsReduceQuantity = $("#chkIsReduceQuantity").prop('checked');
            var IsDeleted = $("#chkIsDeleted").prop('checked');
            var IsSystemUsed = $("#hdnIsSystem").val();
            var SaveOrderStatusObj = {
                OrderStatusID: OrderStatusID,
                OrderStatusAliasName: OrderStatusAliasName,
                AliasToolTip: AliasToolTip,
                AliasHelp: AliasHelp,
                IsSystemUsed: IsSystemUsed,
                IsActive: IsActive,
                IsReduceInQuantity: IsReduceQuantity
            };
            var aspxTempCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName:  $(".languageSelected").attr("value")
            };
          
            this.config.method = "AddUpdateOrderStatus";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                aspxCommonObj: aspxTempCommonObj,
                SaveOrderStatusObj: SaveOrderStatusObj

            });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        SearchOrderStatus: function() {

            var OrderStatusAliasName = $.trim($("#txtOrderStateName").val());
            if (OrderStatusAliasName.length < 1) {
                OrderStatusAliasName = null;
            }
            var isAct = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);

            OrderStatusMgmt.BindOrdersStatusInGrid(OrderStatusAliasName, isAct);
        },
        CheckOrderStatusUniquness: function(orderStatusId) {
            var aspxCommonInfo = aspxCommonObj;
            aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            var orderStatusName = $.trim($('#txtOrderStatusAliasName').val());
            this.config.method = "CheckOrderStatusUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, orderStatusId: orderStatusId, orderStatusAliasName: orderStatusName });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
            return isUnique;
        },

        ajaxSuccess: function(data) {
            switch (OrderStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    if (editFlag > 0) {
                        csscody.info('<h2>'+getLocale(AspxOrderManagement,'Information Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Order status has been updated successfully.')+'</p>');
                    } else {
                        csscody.info('<h2>'+getLocale(AspxOrderManagement,'Information Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Order status has been saved successfully.')+'</p>');
                    }
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 2:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    csscody.info('<h2>'+getLocale(AspxOrderManagement,'Successful Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Order status has been deleted successfully.')+'</p>');
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 3:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    csscody.info('<h2>'+getLocale(AspxOrderManagement,'Successful Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Order status has been deleted successfully.')+'</p>');
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 4:
                    isUnique = data.d;
                    if (data.d == true) {
                        $('#txtOrderStatusAliasName').removeClass('error');
                        $('#osErrorLabel').html('');
                    } else {
                        $('#txtOrderStatusAliasName').addClass('error');
                        $('#osErrorLabel').html('This order status already exist!').css("color", "red");
                        return false;
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (OrderStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>'+getLocale(AspxOrderManagement,'Error Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Failed to save order status')+'</p>');
                    break;
                case 2:
                    csscody.error('<h2>'+getLocale(AspxOrderManagement,'Error Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Failed to delete order status')+'</p>');
                    break;
                case 3:
                    csscody.error('<h2>'+getLocale(AspxOrderManagement,'Error Message')+'</h2><p>'+getLocale(AspxOrderManagement,'Failed to delete selected order status')+'</p>');
                    break;
            }
        },
        init: function() {
            OrderStatusMgmt.LoadOrderStatusStaticImage();
            OrderStatusMgmt.HideAlldiv();
            $('#divOrderStatusDetail').show();
            OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");
                OrderStatusMgmt.RebindStatusOnLangugeChange();

            });
            $('#btnAddNew').bind('click', function () {
                isNewStatus = true;
                OrderStatusMgmt.Reset();
                $("#btnReset").show();
                $('#divOrderStatusDetail').hide();
                $('#divEditOrderStatus').show();
                OrderStatusMgmt.ClearForm();
                editFlag = 0;
            });

            $("#btnBack").bind('click', function() {
                $("#divOrderStatusDetail").show();
                $("#divEditOrderStatus").hide();
            });

            $("#btnReset").bind('click', function() {
                OrderStatusMgmt.Reset();
                OrderStatusMgmt.ClearForm();
            });

            $('#btnDeleteSelected').bind('click', function() {
                var orderStatus_ids = '';
                orderStatus_ids = SageData.Get("tblOrderStatusDetails").Arr.join(',');
                if (orderStatus_ids.length > 0) {
                    var properties = {
                        onComplete: function(e) {
                            OrderStatusMgmt.ConfirmDeleteMultiple(orderStatus_ids, e);
                        }
                    };
                    csscody.confirm("<h2>"+getLocale(AspxOrderManagement,'Delete Confirmation')+'</h2><p>'+getLocale(AspxOrderManagement,'Are you sure you want to delete this selected order status?')+"</p>", properties);
                } else {
                    csscody.alert('<h2>'+getLocale(AspxOrderManagement,'Information Alert')+'</h2><p>'+getLocale(AspxOrderManagement,'Please select at least one order status.')+'</p>');
                }
            });


            $('#btnSaveOrderStatus').bind('click', function() {
                AspxCommerce.CheckSessionActive(aspxCommonObj);
                if (AspxCommerce.vars.IsAlive) {
                    var v = $("#form1").validate({
                        messages: {
                            StatusName: {
                                required: '*',
                                minlength: "* (at least 2 chars)"
                            },
                            ToolTipName: {
                                required: '*',
                                minlength: "* (at least 2 chars)"
                            }
                        }
                    });

                    if (v.form() && OrderStatusMgmt.CheckOrderStatusUniquness(editFlag)) {
                        var orderStatus_id = $(this).prop("name");
                        if (orderStatus_id != '') {
                            OrderStatusMgmt.SaveOrderStatus(orderStatus_id);
                        } else {
                            OrderStatusMgmt.SaveOrderStatus(0);
                        }
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                }
            });
            $("#txtOrderStatusAliasName").bind('focusout', function() {

                OrderStatusMgmt.CheckOrderStatusUniquness(editFlag);
            });
            $('#txtOrderStateName,#ddlVisibitity').keyup(function(event) {
                if (event.keyCode == 13) {
                    OrderStatusMgmt.SearchOrderStatus();
                }
            });
        }
    };
    OrderStatusMgmt.init();
});