    var CustomerTaxClass="";
    $(function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var editFlag = 0;
        CustomerTaxClass = {
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
                CustomerTaxClass.HideAll();
                $("#divTaxCustomerClassGrid").show();
                CustomerTaxClass.LoadCustomerTaxStaticImage();
                CustomerTaxClass.BindCustomerTaxClasses(null);
                $("#btnAddNewTaxCustomerClass").click(function() {
                    CustomerTaxClass.HideAll();
                    $("#" + lblCustomerTaxClassHeading).html(getLoclae(AspxTaxManagement, "New Customer Tax Class"));
                    $("#divCustomerTaxClass").show();
                    $("#txtTaxCustomerClassName").val('');
                    $("#hdnTaxCustomerClass").val(0);
                });
                $("#btnCancel").click(function() {
                    CustomerTaxClass.HideAll();
                    $("#divTaxCustomerClassGrid").show();
                });
                $("#btnSaveTaxCustomerClass").bind("click", function() {
                    CustomerTaxClass.SaveAndUpdateTaxCustmerClass();
                });
                $('#txtCustomerClassName').keyup(function(event) {
                    if (event.keyCode == 13) {
                        CustomerTaxClass.SearchCustomerClassName();
                    }
                });
                $("#btnDeleteSelected").click(function() {
                    var taxCustomerClass_Ids = '';
                    $('.TaxCustomerClassChkbox').each(function() {
                        if ($(this).attr('checked')) {
                            taxCustomerClass_Ids += $(this).val() + ',';
                        }
                    });
                    if (taxCustomerClass_Ids != "") {
                        var properties = {
                            onComplete: function(e) {
                                CustomerTaxClass.ConfirmDeleteTaxCustomerClass(taxCustomerClass_Ids, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delect the selected customer tax class?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Please select at least one customer tax class before delete.') + '</p>');
                    }
                });
            },
            HideAll: function() {
                $("#divTaxCustomerClassGrid").hide();
                $("#divCustomerTaxClass").hide();
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: CustomerTaxClass.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: CustomerTaxClass.config.contentType,
                    cache: CustomerTaxClass.config.cache,
                    async: CustomerTaxClass.config.async,
                    data: CustomerTaxClass.config.data,
                    dataType: CustomerTaxClass.config.dataType,
                    url: CustomerTaxClass.config.url,
                    success: CustomerTaxClass.ajaxSuccess,
                    error: CustomerTaxClass.ajaxFailure
                });
            },

            LoadCustomerTaxStaticImage: function() {
                $('#ajaxCustomerTaxClassImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },

            BindCustomerTaxClasses: function(classNm) {
                this.config.method = "GetTaxCustomerClassDetails";
                this.config.data = { className: classNm, aspxCommonObj: aspxCommonObj };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxCustomerClassDetails_pagesize").length > 0) ? $("#gdvTaxCustomerClassDetails_pagesize :selected").text() : 10;

                $("#gdvTaxCustomerClassDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: getLocale(AspxTaxManagement, 'TaxCostomerClass_ID'), name: 'tax_customer_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: getLocale(AspxTaxManagement, 'Customer Tax Class Name'), name: 'tax_customer_class_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTaxManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: getLocale(AspxTaxManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CustomerTaxClass.EditTaxCustomerClass', arguments: '1,2,3' },
                        { display: getLocale(AspxTaxManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'CustomerTaxClass.DeleteTaxCustomerClass', arguments: '' }
                    ],
                    txtClass: 'sfInputbox',
                    rp: perpage,
                    nomsg: getLocale(AspxTaxManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false} }
                });
            },
            EditTaxCustomerClass: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxCustomerClassDetails":
                        $("#hdnTaxCustomerClass").val(argus[0]);
                        $("#txtTaxCustomerClassName").val(argus[3]);
                        $("#" + lblCustomerTaxClassHeading).html(getLocale(AspxTaxManagement, "Edit Customer Tax Class: ") + "'" + argus[3] + "'");
                        CustomerTaxClass.HideAll();
                        $("#divCustomerTaxClass").show();
                        break;
                    default:
                        break;
                }
            },
            DeleteTaxCustomerClass: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxCustomerClassDetails":
                        var properties = {
                            onComplete: function(e) {
                                CustomerTaxClass.DeleteTaxCustomerClassByID(argus[0], e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxTaxManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTaxManagement, 'Are you sure you want to delete this customer tax class?') + "</p>", properties);
                        break;
                    default:
                        break;
                }
            },
            SaveAndUpdateTaxCustmerClass: function() {
                var taxCustomerClassId = $("#hdnTaxCustomerClass").val();
                editFlag = taxCustomerClassId;
                var taxCustomerClassName = $("#txtTaxCustomerClassName").val();
                if (taxCustomerClassName != "") {
                    this.config.url = this.config.baseURL + "SaveAndUpdateTaxCustmerClass";
                    this.config.data = JSON2.stringify({ taxCustomerClassID: taxCustomerClassId, taxCustomerClassName: taxCustomerClassName, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config);
                    CustomerTaxClass.BindCustomerTaxClasses(null);
                    CustomerTaxClass.HideAll();
                    $("#divTaxCustomerClassGrid").show();
                } else {
                    csscody.alert("<h2>" + getLocale(AspxTaxManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTaxManagement, 'Customer tax Class can not be empty.') + "</p>");
                    return false;
                }
            },
            ConfirmDeleteTaxCustomerClass: function(Ids, event) {
                CustomerTaxClass.DeleteTaxCustomerClassByID(Ids, event);
            },

            DeleteTaxCustomerClassByID: function(_taxCustomerClass_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxCustomerClass";
                    this.config.data = JSON2.stringify({ taxCustomerClassIDs: _taxCustomerClass_Ids, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                }
                return false;
            },
            ajaxSuccess: function() {
                switch (CustomerTaxClass.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        if (editFlag > 0) {
                            csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Customer tax class has been updated successfully.') + '</p>');
                        } else {
                            csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Customer tax class has been saved successfully.') + '</p>');
                        }
                        CustomerTaxClass.BindCustomerTaxClasses(null);
                        CustomerTaxClass.HideAll();
                        $("#divTaxCustomerClassGrid").show();
                        break;
                    case 2:
                        csscody.info('<h2>' + getLocale(AspxTaxManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Customer tax class has been deleted successfully.') + '</p>');
                        CustomerTaxClass.BindCustomerTaxClasses(null);
                        break;
                }
            },
            ajaxFailure: function() {
                switch (CustomerTaxClass.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to save customer tax class!') + '</p>');
                        break;
                    case 2:
                        csscody.error('<h2>' + getLocale(AspxTaxManagement, 'Error Message') + '</h2><p>' + getLocale(AspxTaxManagement, 'Failed to delete customer tax class!') + '</p>');
                        break;
                }
            },
            SearchCustomerClassName: function() {
                var classNm = $.trim($("#txtCustomerClassName").val());
                if (classNm.length < 1) {
                    classNm = null;
                }
                CustomerTaxClass.BindCustomerTaxClasses(classNm);
            }
        };
        CustomerTaxClass.init();
    });