    var ItemTaxClass="";
    $(function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var itemTaxClassFlag = 0;
        var isUniqueTaxClass = false;
        ItemTaxClass = {
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
                ItemTaxClass.HideAll();
                $("#divTaxItemClassGrid").show();
                ItemTaxClass.LoadItemTaxClassStaticImage();
                ItemTaxClass.BindTaxItemClasses(null);
                $("#btnAddNewTaxItemClass").click(function() {
                    ItemTaxClass.HideAll();
                    $("#" + lblItemTaxClassHeading).html(getLocale(AspxTaxManagement, "New Item Tax Class"));
                    $("#divProductTaxClass").show();
                    $("#txtTaxItemClassName").val('');
                    $("#hdnTaxItemClassID").val(0);
                });
                $("#languageSelect li").click(function () {
                    $('#languageSelect').find('li').removeClass("languageSelected");
                    $(this).addClass("languageSelected");

                });
                $("#btnSaveTaxItemClass").click(function () {
                    ItemTaxClass.SaveAndUpdateTaxItemClass(aspxCommonObj, $("#txtTaxItemClassName").val());
                });

                $("#btnCancel").click(function() {
                    ItemTaxClass.HideAll();
                    $("#divTaxItemClassGrid").show();
                });
                $('#txtItemClassName').keyup(function(event) {
                    if (event.keyCode == 13) {
                        ItemTaxClass.SearchItemClassName();
                    }
                });

                $("#spanError").html("");
                $("#txtTaxItemClassName").blur(function() {
                    ItemTaxClass.CheckTaxClassUniqueness(aspxCommonObj, $(this).val());
                });

                $('#txtTaxItemClassName').bind('paste', function(e) {
                    e.preventDefault();
                });

                $('#btnDeleteSelected').click(function() {
                    var TaxItemClass_Ids = '';
                    TaxItemClass_Ids = SageData.Get("gdvTaxItemClassDetails").Arr.join(',');
                    if (TaxItemClass_Ids.length>0) {
                        var properties = {
                            onComplete: function(e) {
                                ItemTaxClass.ConfirmDeleteTaxItemClass(TaxItemClass_Ids, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete the selected item tax class?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please select at least one item tax class.') + '</p>');
                    }
                });
            },
            HideAll: function() {
                $("#divTaxItemClassGrid").hide();
                $("#divProductTaxClass").hide();
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ItemTaxClass.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: ItemTaxClass.config.contentType,
                    cache: ItemTaxClass.config.cache,
                    async: ItemTaxClass.config.async,
                    data: ItemTaxClass.config.data,
                    dataType: ItemTaxClass.config.dataType,
                    url: ItemTaxClass.config.url,
                    success: ItemTaxClass.ajaxSuccess,
                    error: ItemTaxClass.ajaxFailure
                });
            },
            LoadItemTaxClassStaticImage: function() {
                $('#ajaxItemTaxClassImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            BindTaxItemClasses: function(itemClassNm) {
                this.config.method = "GetTaxItemClassDetails";
                this.config.data = { itemClassName: itemClassNm, aspxCommonObj: aspxCommonObj };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxItemClassDetails_pagesize").length > 0) ? $("#gdvTaxItemClassDetails_pagesize :selected").text() : 10;

                $("#gdvTaxItemClassDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: getLocale(AspxTaxManagement, 'TaxItemClass_ID'), name: 'tax_item_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxItemClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: getLocale(AspxTaxManagement, 'Item Tax Class Name'), name: 'tax_item_class_name', cssclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: getLocale(AspxTaxManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemTaxClass.EditTaxItemClass', arguments: '1,2,3' },
                        { display: getLocale(AspxTaxManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemTaxClass.DeleteTaxItemClass', arguments: '' }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxTaxManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false } }
                });
            },
            EditTaxItemClass: function(tblID, argus) {
                switch (tblID) {
                case "gdvTaxItemClassDetails":
                    $("#" + lblItemTaxClassHeading).html(getLocale(AspxTaxManagement, "Edit Item Tax Class: ") + "'" + argus[3] + "'");
                    $("#hdnTaxItemClassID").val(argus[0]);
                    $("#txtTaxItemClassName").val(argus[3]);
                    ItemTaxClass.HideAll();
                    $("#divProductTaxClass").show();
                    break;
                default:
                    break;
                }
            },
            DeleteTaxItemClass: function(tblID, argus) {
                switch (tblID) {
                case "gdvTaxItemClassDetails":
                    var properties = {
                        onComplete: function(e) {
                            ItemTaxClass.DeleteTaxItemClassByID(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete this item tax class?') + "</p>", properties);
                    break;
                default:
                    break;
                }
            },

            ConfirmDeleteTaxItemClass: function(Ids, event) {
                ItemTaxClass.DeleteTaxItemClassByID(Ids, event);
            },

            DeleteTaxItemClassByID: function(taxItemClass_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxItemClass";
                    this.config.data = JSON2.stringify({ taxItemClassIDs: taxItemClass_Ids, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                }
                return false;
            },

            CheckTaxClassUniqueness: function(aspxCommonObj, taxClassName) {
                this.config.url = this.config.baseURL + "CheckTaxClassUniqueness";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, taxItemClassName: taxClassName });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },

            SaveAndUpdateTaxItemClass: function (aspxCommonObj, taxClassName) {
                this.config.url = this.config.baseURL + "CheckTaxClassUniqueness";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, taxItemClassName: taxClassName });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            },
            ajaxSuccess: function(data) {
                switch (ItemTaxClass.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (itemTaxClassFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Item tax class has been updated successfully.') + '</p>');
                    } else {
                        csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Item tax class has been saved successfully.') + '</p>');
                    }
                    ItemTaxClass.BindTaxItemClasses(null);
                    ItemTaxClass.HideAll();
                    $("#divTaxItemClassGrid").show();
                    break;
                case 2:
                    csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Item tax class has been deleted successfully.') + '</p>');
                    ItemTaxClass.BindTaxItemClasses(null);
                    break;
                case 3:
                    if (!data.d) {
                        $("#spanError").html(getLocale(AspxTaxManagement, "Please enter unique tax class name"));
                        $("#txtTaxItemClassName").val("");
                    } else {
                        $("#spanError").html("");
                    }
                    break;
                    case 4:
                        var taxItemClassId = $("#hdnTaxItemClassID").val();
                        if (!data.d) {
                            $("#spanError").html(getLocale(AspxTaxManagement, "Please enter unique tax class name"));
                            $("#txtTaxItemClassName").val("");
                            return;
                        } else {
                            var taxItemClassName = $("#txtTaxItemClassName").val();
                            if (taxItemClassName == '' && $("#spanError").text() == "") {
                                csscody.alert("<h2>" + getLocale(AspxTaxManagement, "Information Alert") + "</h2><p>" + getLocale(AspxTaxManagement, "Item tax class can't be empty!") + "</p>");
                                return false;
                            }
                            if (taxItemClassName != "") {
                                ItemTaxClass.config.url = ItemTaxClass.config.baseURL + "SaveAndUpdateTaxItemClass";
                                ItemTaxClass.config.data = JSON2.stringify({ taxItemClassID: taxItemClassId, taxItemClassName: taxItemClassName, aspxCommonObj: aspxCommonObj });
                                ItemTaxClass.config.ajaxCallMode = 1;
                                ItemTaxClass.ajaxCall(this.config);
                            } else {
                                csscody.alert("<h2>" + getLocale(AspxTaxManagement, "Information Alert") + "</h2><p>" + getLocale(AspxTaxManagement, "Item tax class already exists") + "</p>");
                                return false;
                            }
                            $("#spanError").html("");
                        }
                        break;
                }
            },
            ajaxFailure: function(data) {
                switch (ItemTaxClass.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxTaxManagement, "Information Message") + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to save item tax class!') + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxTaxManagement, "Information Message") + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to delete item tax class!') + '</p>');
                    break;
                }
            },
            SearchItemClassName: function() {
                var itemClassNm = $.trim($("#txtItemClassName").val());
                if (itemClassNm.length < 1) {
                    itemClassNm = null;
                }
                ItemTaxClass.BindTaxItemClasses(itemClassNm);
            }
        };
        ItemTaxClass.init();
    });