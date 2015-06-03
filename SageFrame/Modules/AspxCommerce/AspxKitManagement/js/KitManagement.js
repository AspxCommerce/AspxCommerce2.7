var KitMgmnt = function () {

    var form;
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    var isExist = false;
    var $ajaxCall = function (method, param, successFx, error) {
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
            url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
            data: param,
            dataType: "json",
            success: successFx,
            error: error
        });
    };

    clearkitform = function () {
        $("#txtKitName").val('');
        $("#txtKitPrice").val('');
        $("#txtKitQuantity").val('');
        $("#txtKitWeight").val('');
        $("#ddlKitComponent").val(0);

    };
    clearComponentForm = function () {
        $("#txtKitComponentName").val('');
        $("#ddlComponentType").val(0);
    };
    saveKit = function (kitInfo) {
        if (form.form()) {
            var kitInfo = {};
            kitInfo.KitID = _kitId;
            kitInfo.KitName = $.trim($("#txtKitName").val());
            kitInfo.Price = $.trim($("#txtKitPrice").val());
            kitInfo.Quantity = $.trim($("#txtKitQuantity").val());
            kitInfo.Weight = $.trim($("#txtKitWeight").val());
            kitInfo.KitComponentID = $.trim($("#ddlKitComponent").val());
            if (kitInfo.KitComponentID != 0) {
                $ajaxCall("SaveKit", JSON2.stringify({ kit: kitInfo, commonInfo: aspxCommonObj() }), function (data) {

                    csscody.info("<h2>" + getLocale(AspxKitManagement, "Successful Information") + "</h2><p>" + getLocale(AspxKitManagement, 'Kit Component has been saved successfully.') + "</p>");
                    loadKits();
                    clearkitform();
                    $("#dvKits").show();
                    $("#dvNewComponent,#dvEditKit").hide();
                }, null);
            }
            else {
                csscody.alert("<h2>" + getLocale(AspxKitManagement, "Information Alert") + "</h2><p>" + getLocale(AspxKitManagement, 'Please Select Component.') + "</p>");
                return false;
            }
        }
    };
    var _componentId = 0;
    saveComponent = function () {
        if (form.form()) {
            CheckKitComponentExist($.trim($("#txtNewComponent").val()))
            if (!isExist) {
                var componentInfo = {};
                componentInfo.KitComponentID = _componentId;
                componentInfo.KitComponentName = $.trim($("#txtNewComponent").val());
                componentInfo.KitComponentType = parseInt($("#ddlComponentType").val());
                $ajaxCall("SaveComponent", JSON2.stringify({ kitcomponent: componentInfo, commonInfo: aspxCommonObj() }), function (data) {
                    csscody.info("<h2>" + getLocale(AspxKitManagement, "Successful Information") + "</h2><p>" + getLocale(AspxKitManagement, 'Kit Component has been saved successfully.') + "</p>");
                    getComponents();
                    clearComponentForm();
                    $("#dvEditKit").show();
                    $("#dvNewComponent,#dvKits").hide();
                }, null);
            }
            else {
                csscody.alert("<h2>" + getLocale(AspxKitManagement, "Information Alert") + "</h2><p>" + getLocale(AspxKitManagement, 'Component already exist.') + "</p>");
                return false;
            }
        }
    };
    CheckKitComponentExist = function (ComponentName) {
        $ajaxCall("CheckKitComponentExist", JSON2.stringify({ ComponentName: ComponentName, aspxCommonObj: aspxCommonObj() }), function (data) {
            isExist = data.d;
        }, null);
    };
    var _kitId = 0;
    editKit = function (tbl, args) {
        _kitId = parseInt(args[3]);//kit id
        $("#dvKits").hide();
        $("#dvEditKit").show();

        $("#txtKitName").val(args[4])
        $("#txtKitPrice").val(args[5])
        $("#txtKitQuantity").val(args[6])
        $("#txtKitWeight").val(args[7])
        $("#ddlKitComponent").val(args[8])

    };

    deleteConfirm = function (tbl, id) {

        var properties = {
            onComplete: function (e) {
                deletekit(id[0], e);
            }
        };
        csscody.confirm("<h2>" + getLocale(AspxKitManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxKitManagement, "Are you sure you want to delete this item?") + "</p>", properties);


    };

    deletekit = function (id, event) {

        if (event) {
            $ajaxCall("DeleteKit", JSON2.stringify({ kitIds: id, commonInfo: aspxCommonObj() }), function (data) {
                csscody.info("<h2>" + getLocale(AspxKitManagement, "Successful Information") + "</h2><p>" + getLocale(AspxKitManagement, 'Kit has been deleted successfully.') + "</p>");

                loadKits();

            }, null);
        }
        return false;
    };

    deleteKitComponent = function (id, event) {
        if (event) {
            $ajaxCall("DeleteKitComponent", JSON2.stringify({ kitComponentIds: id, commonInfo: aspxCommonObj() }), function (data) {

                csscody.info("<h2>" + getLocale(AspxKitManagement, "Successful Information") + "</h2><p>" + getLocale(AspxKitManagement, 'Kit Component has been deleted successfully.') + "</p>");
                getComponents();

            }, null);
        }
    };

    loadKits = function () {

        var aspxCommonInfo = aspxCommonObj();
        aspxCommonInfo.UserName = null;

        var data = { kitname: $.trim($("#txtSrchKitName").val()), commonInfo: aspxCommonInfo };

        var offset_ = 1;
        var current_ = 1;
        var perpage = ($("#gdvKitList_pagesize").length > 0) ? $("#gdvKitList_pagesize :selected").text() : 10;

        $("#gdvKitList").sagegrid({
            url: aspxservicePath + 'AspxCoreHandler.ashx/GetKitsGrid',
            colModel: [
                 { display: getLocale(AspxKitManagement, 'ID'), name: 'Id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },

            { display: getLocale(AspxKitManagement, 'Kit ID'), name: 'kit_id', cssclass: 'cssClassHeadNumber', elemClass: 'kitCheckoxID', coltype: 'checkbox', align: 'left' },
            { display: getLocale(AspxKitManagement, 'Kit Name'), name: 'kitname', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
            { display: getLocale(AspxKitManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
             { display: getLocale(AspxKitManagement, 'Quantity'), name: 'quantity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
              { display: getLocale(AspxKitManagement, 'Weight'), name: 'weight', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
       { display: getLocale(AspxKitManagement, 'Kit ID'), name: 'kit_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },

           { display: getLocale(AspxKitManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
            ],
            buttons: [
            { display: getLocale(AspxKitManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'KitMgmnt.Edit', arguments: '1,2,3,4,5,6,7' },
            { display: getLocale(AspxKitManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'KitMgmnt.Delete', arguments: '1' }
            ],
            rp: perpage,
            nomsg: getLocale(AspxKitManagement, "No Records Found!"),
            param: data,
            current: current_,
            pnew: offset_,
            sortcol: { 1: { sorter: false }, 7: { sorter: false } }
        });
        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
    };

    getComponents = function () {
        $ajaxCall("GetComponents", JSON2.stringify({ commonInfo: aspxCommonObj() }), function (data) {

            $("#ddlKitComponent").html("<option value='0'>Choose One</option>");
            $.each(data.d, function (index, item) {
                $("#ddlKitComponent").append("<option value=" + item.KitComponentID + ">" + item.KitComponentName + "</option>")
            });

        }, null);

    };

    UIEvent = function () {

        $("body").on("click", "#btnAddNewKit", function () {
            _kitId = 0;
            $("#dvEditKit").show();
            $("#dvNewComponent,#dvKits").hide();
        });
        $("body").on("click", "#btnSearchKit", function () {
            loadKits();
        });
        $("body").on("click", "#btnDeleteAllKit", function () {
            var ids = [];
            ids = SageData.Get("gdvKitList").Arr.join(',');
            if (ids.length > 0) {
                var properties = {
                    onComplete: function (e) {
                        deletekit(ids.join(','), e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxKitManagement, 'Delete Confirmation') + "</h2><p>" + getLocale(AspxKitManagement, 'Are you sure you want to delete the selected item(s)?') + "</p>", properties);
            } else {
                csscody.alert('<h2>' + getLocale(AspxKitManagement, "Information Alert") + '</h2><p>' + getLocale(AspxKitManagement, "Please select at least one item before delete") + '</p>');
            }

        });
        $("body").on("click", "#btnAddNewComponent", function () {
            $("#txtNewComponent").val("");
            $("#dvNewComponent").show();
            $("#dvKits,#dvEditKit").hide();

        });

        $("body").on("click", "#btnSave", function () {

            saveKit();
        });
        $("body").on("click", "#btnCancel", function () {
            _kitId = 0;
            form.resetForm();
            $("#dvKits").show();
            $("#dvNewComponent,#dvEditKit").hide();
            clearkitform();            
        });
        $("body").on("click", "#btnComponentSave", function () {

            saveComponent();
        });
        $("body").on("click", "#btnComponentCancel", function () {
            form.resetForm();
            $("#dvEditKit").show();
            $("#dvNewComponent,#dvKits").hide();
            clearComponentForm();            
        });


    }();

    init = function () {
        $(document).ready(function () {
            form = $("#form1").validate({
                rules: {
                    kitname: "required",
                    price: { required: true, number: true },
                    quantity: { required: true, digits: true },
                    weight: { required: true, number: true },
                    component: "required"
                },
                messages: {
                    kitname: "*",
                    price: "*",
                    quantity: "*",
                    weight: "*",
                    component: "*"
                },
                ignore: ':hidden'
            });
            loadKits();
            getComponents();
        })

    }();
    return {
        Edit: editKit, Delete: deleteConfirm
    };
}();