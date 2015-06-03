
var cardTypeMgmt = '';
$(function () {
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
    var cardTypeFlag = 0;
    cardTypeMgmt = {
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
                type: cardTypeMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: cardTypeMgmt.config.contentType,
                cache: cardTypeMgmt.config.cache,
                async: cardTypeMgmt.config.async,
                url: cardTypeMgmt.config.url,
                data: cardTypeMgmt.config.data,
                dataType: cardTypeMgmt.config.dataType,
                success: cardTypeMgmt.ajaxSuccess,
                error: cardTypeMgmt.ajaxFailure
            });
        },

        HideAlldiv: function () {
            $('#divCardTypeDetail').hide();
            $('#divEditCardType').hide();
        },

        ClearForm: function () {
            $("#btnSaveCardType").removeAttr("name");
            $('#' + lblCardTypeHeading).html(getLocale(AspxCardTypeManagement, "Add New Card"));
            $('#txtCardTypeName').val('');
            $("#chkIsActiveCardType").removeAttr('checked');
            $("#hdnPrevFilePath").val("");
            $('#divCardImage').html('');
            $("#txtCardAlternateText").val('');
            $('#cardTypeImageBrowser').val('');
            cardTypeMgmt.RemoveLabel();
            $("#tblCardTypeDetails .attrChkbox").each(function (i) {
                $(this).removeAttr("checked");
            });
        },

        ResetForm: function () {
            $('#txtCardTypeName').val('');
            $("#chkIsActiveCardType").removeAttr('checked');
            $('#divCardImage').html('');
            $('#txtCardAlternateText').val('');
            cardTypeMgmt.RemoveLabel();
        },

        RemoveLabel: function () {
            $('#txtCardTypeName').removeClass('error');
            $('#txtCardTypeName').parents('td').find('label').remove();
        },

        Boolean: function (str) {
            switch (str.toLowerCase()) {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        },

        BindCardTypeInGrid: function (cardTypeName, isAct) {
            this.config.method = "GetAllCardTypeList";
            this.config.data = { aspxCommonObj: aspxCommonObj, cardTypeName: cardTypeName, isActive: isAct };
            var data = cardTypeMgmt.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblCardTypeDetails_pagesize").length > 0) ? $("#tblCardTypeDetails_pagesize :selected").text() : 10;

            $("#tblCardTypeDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                        { display: getLocale(AspxCardTypeManagement, "Card Type ID"), name: 'CardTypeID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '2', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
				        { display: getLocale(AspxCardTypeManagement, "Card Type Name"), name: 'CardTypeName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxCardTypeManagement, "System Used"), name: 'Is_System_Used', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
				        { display: getLocale(AspxCardTypeManagement, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
              	        { display: getLocale(AspxCardTypeManagement, "IsDeleted"), name: 'IsDeleted', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                        { display: getLocale(AspxCardTypeManagement, "ImagePath"), name: 'ImagePath', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxCardTypeManagement, "AlternateText"), name: 'AlternateText', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
               	        { display: getLocale(AspxCardTypeManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [{ display: getLocale(AspxCardTypeManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'cardTypeMgmt.EditCardType', arguments: '1,2,3,5,6' },
			             { display: getLocale(AspxCardTypeManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'cardTypeMgmt.DeleteCardType', arguments: '2' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxCardTypeManagement, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        EditCardType: function (tblID, argus) {
            switch (tblID) {

                case "tblCardTypeDetails":
                    if (argus[4].toLowerCase() != "yes") {

                        cardTypeMgmt.ClearForm();
                        $('#divCardTypeDetail').hide();
                        $('#divEditCardType').show();
                        $('#' + lblCardTypeHeading).html(getLocale(AspxCardTypeManagement, "Edit Card Type: ") + argus[3]);
                        $('#txtCardTypeName').val(argus[3]);
                        $("#chkIsActiveCardType").prop('checked', cardTypeMgmt.Boolean(argus[5]));
                        $("#btnSaveCardType").prop("name", argus[0]);
                        $("#hdnPrevFilePath").val(argus[6]);
                        $("#txtCardAlternateText").val(argus[7]);
                        if (argus[6] != '') {
                            $("#divCardImage").html('<img src="' + aspxRootPath + argus[6] + '" class="uploadImage" height="90px" width="100px"/></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="cardTypeMgmt.ClickToDeleteImage()" alt="Delete"/>');
                        }
                        $("#btnReset").hide();
                        $("#cardTypeImageBrowser").removeAttr('name');
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxCardTypeManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Sorry! System used card type can not be edited.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        SaveCardType: function (CardTypeId) {
            var newImagePath = '';
            if ($("#divCardImage>img").length > 0) {

                newImagePath = $("#cardTypeImageBrowser").prop('name');
            }
            var cardTypeSaveObj = {
                CardTypeID: CardTypeId,
                CardTypeName: $('#txtCardTypeName').val(),
                IsActive: $("#chkIsActiveCardType").prop('checked'),
                AlternateText: $("#txtCardAlternateText").val(),
                ImagePath: '',
                NewFilePath: newImagePath,
                PrevFilePath: $("#hdnPrevFilePath").val()
            };
            this.config.url = this.config.baseURL + "AddUpdateCardType";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, cardTypeSaveObj: cardTypeSaveObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        ImageUploader: function () {
            maxFileSize = maxFileSize;
            var upload = new AjaxUpload($('#cardTypeImageBrowser'), {
                action: aspxCardTypeModulePath + "CardFileUploader.aspx",
                name: 'myfile[]',
                multiple: false,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxCardTypeManagement, "Alert Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Not a valid image!") + '</p>');
                            return false;
                        }
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxCardTypeManagement, "Alert Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Not a valid image!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null && res.Status > 0) {
                        cardTypeMgmt.AddNewImages(res);
                        return false;
                    }
                    else {
                        csscody.error('<h2>Error Message</h2><p>' + getLocale(AspxCardTypeManagement, "Can\'t upload the image!") + '</p>');
                        return false;
                    }
                }
            });
        },

        AddNewImages: function (response) {
            $("#cardTypeImageBrowser").prop('name', response.Message);
            $("#divCardImage").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="cardTypeMgmt.ClickToDeleteImage()" alt="Delete"/>');
        },

        ClickToDeleteImage: function () {
            $('#divCardImage').html('');
            $("#hdnPrevFilePath").val('');
            return false;
        },

        DeleteCardType: function (tblID, argus) {
            switch (tblID) {
                case "tblCardTypeDetails":
                    if (argus[3].toLowerCase() != "yes") {
                        cardTypeMgmt.DeleteCardTypeByID(argus[0]);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>' + getLocale(AspxCardTypeManagement, "Sorry! System used card type can not be deleted.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteCardTypeByID: function (_cardTypeId) {
            var properties = {
                onComplete: function (e) {
                    cardTypeMgmt.ConfirmSingleDelete(_cardTypeId, e);
                }
            };
            csscody.confirm("<h2>Delete Confirmation</h2><p>" + getLocale(AspxCardTypeManagement, "Are you sure you want to delete this card type?") + "</p>", properties);
        },

        ConfirmSingleDelete: function (_cardTypeId, event) {
            if (event) {
                cardTypeMgmt.DeleteSingleCard(_cardTypeId);
            }
            return false;
        },

        DeleteSingleCard: function (_cardTypeId) {
            this.config.method = "DeleteCardTypeByID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ cardTypeID: parseInt(_cardTypeId), aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        ConfirmDeleteMultiple: function (_cardTypeIds, event) {
            if (event) {
                cardTypeMgmt.DeleteMultipleCard(_cardTypeIds);
            }
        },

        DeleteMultipleCard: function (_cardTypeIds) {
            this.config.method = "DeleteCardTypeMultipleSelected";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ cardTypeIDs: _cardTypeIds, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        ajaxSuccess: function (data) {
            switch (cardTypeMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    cardTypeMgmt.BindCardTypeInGrid(null, null);
                    cardTypeMgmt.ClearForm();
                    if (cardTypeFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxCardTypeManagement, "Alert Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Card Type has been updated successfully.") + '</p>');
                    }
                    else {
                        csscody.info('<h2>' + getLocale(AspxCardTypeManagement, "Alert Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Card Type has been saved successfully.") + '</p>');
                    }
                    $('#divCardTypeDetail').show();
                    $('#divEditCardType').hide();
                    break;
                case 2:
                    cardTypeMgmt.BindCardTypeInGrid(null, null);
                    cardTypeMgmt.ClearForm();
                    csscody.info('<h2>' + getLocale(AspxCardTypeManagement, "Alert Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Card type has been deleted successfully.") + '</p>');
                    $('#divCardTypeDetail').show();
                    $('#divEditCardType').hide();
                    break;
                case 3:
                    cardTypeMgmt.BindCardTypeInGrid(null, null);
                    cardTypeMgmt.ClearForm();
                    csscody.info('<h2>Alert Message</h2><p>' + getLocale(AspxCardTypeManagement, "Selected card type(s) has been deleted successfully.") + '</p>');
                    $('#divCardTypeDetail').show();
                    $('#divEditCardType').hide();
                    break;
            }
        },
        ajaxFailure: function (data) {
            switch (cardTypeMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxCardTypeManagement, "Error Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Failed to update card type!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxCardTypeManagement, "Error Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Failed to delete card type!") + '</p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxCardTypeManagement, "Error Message") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Failed to delete selectd card types!") + '</p>');
                    break;
            }
        },

        SearchCardType: function () {

            var cardTypeName = $.trim($("#txtSearchCardTypeName").val());
            if (cardTypeName.length < 1) {
                cardTypeName = null;
            }
            var isAct = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);
            cardTypeMgmt.BindCardTypeInGrid(cardTypeName, isAct);
        },

        init: function () {
            var validCard = $("#form1").validate({
                rules: {
                    CardTypeName: {
                        minlength: 2
                    }
                },
                messages: {
                    CardTypeName: {
                        required: '*',
                        minlength: getLocale(AspxCardTypeManagement, "* (at least 2 chars)")
                    }
                }
            });
            cardTypeMgmt.BindCardTypeInGrid(null, null);
            cardTypeMgmt.HideAlldiv();
            $('#divCardTypeDetail').show();
            $('#btnAddNew').bind('click', function () {
                $('#divCardTypeDetail').hide();
                $('#divEditCardType').show();
                $("#btnReset").show();
                cardTypeMgmt.ClearForm();
            });

            $("#btnBack").bind('click', function () {
                $("#divCardTypeDetail").show();
                $("#divEditCardType").hide();
                validCard.resetForm();
            });

            $('#btnReset').bind('click', function () {
                cardTypeMgmt.ResetForm();
            });

            $('#btnDeleteSelected').click(function () {
                var _cardTypeIds = '';
                _cardTypeIds = SageData.Get("tblCardTypeDetails").Arr.join(',');
                if (_cardTypeIds.length > 0) {
                    var properties = {
                        onComplete: function (e) {
                            cardTypeMgmt.ConfirmDeleteMultiple(_cardTypeIds, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxCardTypeManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCardTypeManagement, "Are you sure you want to delete the selected card type(s)?") + "</p>", properties);
                }
                else {
                    csscody.alert('<h2>' + getLocale(AspxCardTypeManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCardTypeManagement, "Please select at least one card type before delete.") + '</p>');
                }
            });

            $('#btnSaveCardType').click(function () {               
                if (validCard.form()) {
                    var cardType_id = $(this).prop("name");
                    cardTypeFlag = cardType_id;
                    if (cardType_id != '') {
                        cardTypeMgmt.SaveCardType(cardType_id, storeId, portalId, userName, cultureName);
                    }
                    else {
                        cardTypeMgmt.SaveCardType(0, storeId, portalId, userName, cultureName);
                    }
                }
            })
            cardTypeMgmt.ImageUploader();
        }
    }
    cardTypeMgmt.init();
});
