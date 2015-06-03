var shippingProviderMgmt;
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var editFlag = 0;
    var isUnique = false;
    var ids = "";
    var isZipInstalled = false;
    var zipProvider;
    var settingProviderId = 0;
    shippingProviderMgmt = {
        IsfreshZipInstalled: function() {
            return isZipInstalled;
        },
        GetInstalledProviderinfo: function() {
            return zipProvider;
        },
        GetSettingId: function() {
            return settingProviderId;
        },
        SetProviderId: function(id) {
            editFlag = id;
        },
        GetProviderId: function() { return editFlag; },
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
        ajaxCall: function(config) {
            $.ajax({
                type: shippingProviderMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: shippingProviderMgmt.config.contentType,
                cache: shippingProviderMgmt.config.cache,
                async: shippingProviderMgmt.config.async,
                url: shippingProviderMgmt.config.url,
                data: shippingProviderMgmt.config.data,
                dataType: shippingProviderMgmt.config.dataType,
                success: shippingProviderMgmt.ajaxSuccess,
                error: shippingProviderMgmt.ajaxFailure
            });
        },
        FileHandler: function() {
            var $ajaxUpload = function(uploaderId) {
                               new AjaxUpload(String(uploaderId), {
                    action: aspxItemModulePath + '/ProviderHandler.aspx',
                    name: 'myfile',
                    onSubmit: function(file, ext) {
                        var baseLocation = "Modules/AspxCommerce/AspxShippingManagement/temp";
                        var validExt = "zip";
                        var maxFileSize = "1024";
                        var regExp = /\s+/g;
                        var myregexp = new RegExp("(" + validExt.replace(regExp, "|") + ")", "i");
                        if (ext != "exe") {
                            if (ext && myregexp.test(ext)) {
                                this.setData({
                                    'BaseLocation': baseLocation,
                                    'ValidExtension': validExt,
                                    'MaxFileSize': maxFileSize,
                                    'StoreId': aspxCommonObj.StoreID,
                                    'PortalId': aspxCommonObj.PortalID,
                                    'IsRepair': $("#cbRepair").is(":checked"),
                                    'UserName': userName

                                });
                            } else {
                                csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please upload valid zip file!") + '</p>');
                                return false;
                            }
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please upload valid zip file!") + '</p>');
                            return false;
                        }
                    },
                    onComplete: function(file, ajaxFileResponse) {
                        var res = eval(ajaxFileResponse);
                        if (res.Status > 0) {
                            $("#dvUploaderProvider").hide();
                            isZipInstalled = true;
                            var fileExt = (-1 !== file.indexOf('.')) ? file.replace(/.*[.]/, '') : '';
                            var myregexp = new RegExp("(zip)", "i");
                            if (myregexp.test(fileExt)) {
                                                                                          } else {
                                                           }
                            csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping provider has been saved successfully.") + '</p>');
                            shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                            $('#divShippingProviderDetails').show();
                            $('#divEditShippingProvider').hide();
                            shippingProviderMgmt.ClearForm();
                        } else {
                            isZipInstalled = false;
                            csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + '</h2><p>' + res.Message + '</p>');
                        }
                    }
                }, "btnReadZipFile");
            };

            var rollBack = function() {
                var temp = zipProvider.ExtractedPath.substr(zipProvider.ExtractedPath.lastIndexOf("Module"));

                $.ajax({
                    type: "POST", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    url: providerServicePath,
                    data: JSON2.stringify({ temppath: temp, dllFiles: zipProvider.DllFiles, unistallfile: zipProvider.UninstallScript }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {

                    },
                    error: function() {
                                           }
                });

            };

            var initUploader = function() {
                $ajaxUpload('fuShippingProvider');
            };
            initUploader();

        } (),
        LoadSippingProviderStaticImage: function() {
            $('#ajaxShippingProviderImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideAllDiv: function() {
            $('#divShippingProviderDetails').hide();
            $('#divEditShippingProvider').hide();
        },

        ClearForm: function() {
            $("#btnSaveShippingProvider").removeAttr("name");
            $("#" + lblSPHeading).html(getLocale(AspxShippingManagement, "Add New Shipping Provider"));

            $('#txtSPServiceCode').val('');
            $('#txtSPName').val('');
            $('#txtSPAliasHelp').val('');
            $("#chkIsActiveSP").removeAttr('checked');
            $("#isActiveSp").show();

            $('#txtSPServiceCode').removeClass('error');
            $('#txtSPServiceCode').parents('td').find('label').remove();
            $('#txtSPName').removeClass('error');
            $('#txtSPName').parents('td').find('label').remove();
            $("#tblShippingProviderList .attrChkbox").each(function(i) {
                $(this).removeAttr("checked");
            });
            $('#sperrorLabel').html('');
            $("#dvUploaderProvider").show();
            editFlag = 0;
        },
        LoadControl: function(argus, PopUpID) {
            var controlName = argus[0];
            var providerId = argus[1];
            settingProviderId = parseInt(providerId);
            if (controlName != "") {
                $.ajax({
                    type: "POST", 
                    url: aspxservicePath + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + controlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        if (response.d.startsWith('System.Web.HttpException')) {
                            csscody.alert("<h2>" + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to load control.Please submit sfe with valid data!") + "</p>");
                            $("#fade ,.popupbox").fadeOut();
                        } else {
                            $("#" + PopUpID).html('').html(response.d);
                                                       ShowPopupControl(PopUpID);
                            $("#fade ,.cssClassClose").unbind().bind("click", function() {
                                settingProviderId = 0;
                                $("#fade ,.popupbox").fadeOut();
                            });
                        }
                    },
                    error: function() {
                        csscody.alert("<h2>" + getLocale(AspxShippingManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxShippingManagement, "Failed to load control!") + "</p>");
                    }
                });
            } else {
                csscody.alert("<h2>" + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "This shipping provider does not have Setting!") + "</p>");
            }
        },
        BindShippingProviderNameInGrid: function(shippingProviderName, isAct) {
            this.config.url = this.config.baseURL;
            this.config.method = "GetShippingProviderNameList";
            this.config.data = { aspxCommonObj: aspxCommonObj, shippingProviderName: shippingProviderName, isActive: isAct };
            var shippingProviderData = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblShippingProviderList_pagesize").length > 0) ? $("#tblShippingProviderList_pagesize :selected").text() : 10;

            $("#tblShippingProviderList").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetShippingProviderNameList',
                colModel: [
                     { display: getLocale(AspxShippingManagement, 'ShippingProvider ID'), name: 'ShippingProvderID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                     { display: getLocale(AspxShippingManagement, 'Shipping Provider Code'), name: 'ShippingProviderServiceCode', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Shipping Provider Name'), name: 'ShippingProviderName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Shipping Provider Alias Help'), name: 'ShippingProviderAliasHelp', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxShippingManagement, 'Setting'), name: 'setting', btntitle: 'Setting', cssclass: 'cssClassButtonHeader', controlclass: 'cssClassButtonSubmit', coltype: 'button', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: 'popuprel3', poparguments: '5,6', popupmethod: 'shippingProviderMgmt.LoadControl' },
                     { display: getLocale(AspxShippingManagement, 'HiddenSetting'), name: 'HiddenSetting', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: getLocale(AspxShippingManagement, 'HdnProviderID'), name: 'HdnProviderID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: getLocale(AspxShippingManagement, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                     { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                 ],

                buttons: [{ display: getLocale(AspxShippingManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'shippingProviderMgmt.EditShippingProvider', arguments: '1,2,3,4,7' },
                     { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'shippingProviderMgmt.DeleteShippingProvider', arguments: '1' }
                 ],
                rp: perpage,
                nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                param: shippingProviderData,
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
        EditShippingProvider: function(tblID, argus) {
            switch (tblID) {
                case 'tblShippingProviderList':
                    isZipInstalled = false;
                    shippingProviderMgmt.ClearForm();
                    $('#divShippingProviderDetails').hide();
                    $('#divEditShippingProvider').show();
                    $('#btnSPReset').hide();
                    $("#" + lblSPHeading).html(getLocale(AspxShippingManagement, "Edit Shipping Provider ID: ") + "'" + argus[0] + "'");
                    $('#txtSPServiceCode').val(argus[3]);
                    $('#txtSPName').val(argus[4]);
                    $('#txtSPAliasHelp').val(argus[5]);
                    $("#chkIsActiveSP").prop('checked', shippingProviderMgmt.Boolean(argus[7]));
                    $("#btnSaveShippingProvider").prop("name", argus[0]);
                    editFlag = argus[0];

                                                          if (argus[6] != "") {
                        $("#dvResponse").show().find("#divShowShippingMethodGrid").show();
                        shippingProviderMgmt.ProviderEdit.LoadGrid(editFlag);
                    } else {
                        $("#gdvShippingMethod,#tblShippingMethodAdd").html('');
                        $("#dvResponse").hide();
                    }
                    break;
                default:
                    break;
            }
        },
        ProviderEdit: function() {
            var tempId = 0;
            var tempSetting;
            var deactivateM = function(tbl, args) {
                var isActive = args[4] == "Yes" ? false : true;
                deactivate(args[0], isActive);
            };
            var deactivate = function(id, isActive) {
                shippingProviderMgmt.config.url = shippingProviderMgmt.config.baseURL + "DeactivateShippingMethod";
                shippingProviderMgmt.config.data = JSON2.stringify({ shippingMethodId: id, aspxCommonObj: aspxCommonObj, isActive: isActive });
                shippingProviderMgmt.ajaxSuccess = function(data) {
                    var info = isActive ? ' activated ' : ' deactivated ';
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Information Success") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping Methods has been " + info + " successfully.") + '</p>');
                };
                shippingProviderMgmt.ajaxCall(shippingProviderMgmt.config);
                loadAddedShippingMethod(tempId);
            };
            var deleteM = function(tbl, args) {
                var properties = {
                    onComplete: function(e) {
                        deleteMethod(args[0], e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this shipping provider(s)?") + "</p>", properties);


            };
            var deleteMethod = function(id, e) {
                if (e) {

                    shippingProviderMgmt.config.url = shippingProviderMgmt.config.baseURL + "DeleteShippingByShippingMethodID";
                    shippingProviderMgmt.config.data = JSON2.stringify({ shippingMethodIds: id, aspxCommonObj: aspxCommonObj });
                    shippingProviderMgmt.ajaxSuccess = function(data) {
                        csscody.info('<h2>' + getLocale(AspxShippingManagement, "Information Success") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping Methods has been deleted.") + '</p>');
                        loadremainginShippingMethod(tempId);
                    };
                    shippingProviderMgmt.ajaxCall(shippingProviderMgmt.config);
                    loadAddedShippingMethod(tempId);
                }
            };
            var deleteAll = function(sids, e) {

                if (e) {
                    deleteMethod(sids.join(','), e);
                }
            };

            $("#btnDeleteSelected").unbind().bind("click", function() {
                var sids = [];
                $("#gdvShippingMethod .attrChkbox").each(function(i) {
                    if ($(this).prop("checked")) {
                        sids.push($(this).val());
                    }
                });
                if (sids.length > 0) {
                    var properties = {
                        onComplete: function(e) {
                            deleteAll(sids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete the selected shipping provider(s)?") + "</p>", properties);

                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please select at least one shipping provider before delete.") + '</p>');
                }

            });
            var loadAddedShippingMethod = function(shippingProviderId) {

                tempId = shippingProviderId;
                var shippingProviderData = { aspxCommonObj: aspxCommonObj, shippingProviderId: shippingProviderId };
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvShippingMethod_pagesize").length > 0) ? $("#tblShippingProviderList_pagesize :selected").text() : 10;

                $("#gdvShippingMethod").sagegrid({
                    url: aspxservicePath + "AspxCoreHandler.ashx/",
                    functionMethod: 'GetShippingMethodListByProvider',
                    colModel: [
                         { display: getLocale(AspxShippingManagement, 'ShippingMethodID'), name: 'ShippingMethodID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                         { display: getLocale(AspxShippingManagement, 'Shipping Method Name'), name: 'ShippingMethodName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                         { display: getLocale(AspxShippingManagement, 'WeightLimitFrom'), name: 'WeightLimitFrom', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                         { display: getLocale(AspxShippingManagement, 'WeightLimitFrom'), name: 'SWeightLimitFrom', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                         { display: getLocale(AspxShippingManagement, 'delivery'), name: 'delivery', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                         { display: getLocale(AspxShippingManagement, 'Active'), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                         { display: getLocale(AspxShippingManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                     ],

                    buttons: [{ display: getLocale(AspxShippingManagement, 'ChangeActive'), name: 'ChangeActive', enable: true, _event: 'click', trigger: '1', callMethod: 'shippingProviderMgmt.ProviderEdit.Deactivate', arguments: '1,5' },
                         { display: getLocale(AspxShippingManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'shippingProviderMgmt.ProviderEdit.Delete', arguments: '1' }
                     ],
                    rp: perpage,
                    nomsg: getLocale(AspxShippingManagement, "No Records Found!"),
                    param: shippingProviderData,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 3: { sorter: false} }
                });
                loadsettings(shippingProviderId);
                loadremainginShippingMethod(shippingProviderId);

            };
            var loadremainginShippingMethod = function(id) {

                shippingProviderMgmt.config.url = shippingProviderMgmt.config.baseURL + "GetProviderRemainingMethod";
                shippingProviderMgmt.config.data = JSON2.stringify({ shippingProviderId: id, aspxCommonObj: aspxCommonObj });
                shippingProviderMgmt.ajaxSuccess = function(data) {

                    var $table = $("<table id='tblShippingMethodAdd'/>");
                    $.each(data.d, function(index, item) {
                                                                                                                    var $td1 = $("<td/>");
                        var $checkbox = $("<input type='checkbox'/>").prop('value', item.ShippingMethodCode);
                        $td1.append($checkbox);
                                               var $td2 = $("<td/>");
                        var $label = $("<label/>").html(item.ShippingMethodName);
                        $td2.append($label);
                        $table.append($("<tr/>").append($td1).append($td2));
                    });
                    $("#dvResponse").show();                    $("#dvAddMethods").html('').append($table);
                                   };
                shippingProviderMgmt.ajaxCall(shippingProviderMgmt.config);
            };

            var loadsettings = function(id) {
                shippingProviderMgmt.config.url = shippingProviderMgmt.config.baseURL + "LoadProviderSetting";
                shippingProviderMgmt.config.data = JSON2.stringify({ providerId: id, aspxCommonObj: aspxCommonObj });
                shippingProviderMgmt.ajaxSuccess = function(data) {
                    if (data.d != "" || data.d != null)
                        tempSetting = $.parseJSON(data.d);
                };
                shippingProviderMgmt.ajaxCall(shippingProviderMgmt.config);
            };
            var getSettingField = function(field) {
                var val = "";
                if (tempSetting != null) {
                    $.each(tempSetting, function(index, row) {

                        if (row.Key.indexOf(field) != -1) {
                            val = row.Value;
                            return;
                        }
                    });
                }
                return val;
            };
            return { Deactivate: deactivateM,
                Delete: deleteM,
                LoadGrid: loadAddedShippingMethod,
                LoadRemainingMethod: loadremainginShippingMethod,
                GetSettingField: getSettingField
            };

        } (),
        SaveShippingProvider: function (shippingProviderID) {           
            var spServiceCode = $.trim($('#txtSPServiceCode').val());
            var spName = $.trim($('#txtSPName').val());
            var spAliasName = $.trim($('#txtSPAliasHelp').val());
            var isActive = $("#chkIsActiveSP").is(":checked");


            var methodList = [];
            var maxWeight = shippingProviderMgmt.ProviderEdit.GetSettingField('MaxWeight');
            $("#dvResponse #tblShippingMethodAdd").find("tr").each(function(index, item) {

                var $checkbox = $(this).find("input[type=checkbox]");
                if ($checkbox.is(":checked")) {
                    var shipingMethods = {
                        ShippingMethodId: 0,
                        ShippingMethodCode: '',
                        ShippingMethodName: '',
                        ImagePath: '',
                        AlternateText: '',
                        DisplayOrder: 0,
                        DeliveryTime: 'as provider calculation',
                        WeightLimitFrom: 0.1,
                        WeightLimitTo: maxWeight,
                        IsActive: true,
                        AddedBy: userName
                    };
                    shipingMethods.ShippingMethodCode = $checkbox.prop('value');
                    shipingMethods.ShippingMethodName = $(this).find("label").html();
                    shipingMethods.AlternateText = $(this).find("label").html();

                    methodList.push(shipingMethods);
                }
            });

            this.config.url = this.config.baseURL + "ShippingProviderAddUpdate";
                                                                                                                         
            var provider = {
                ShippingProviderID: shippingProviderID,
                ShippingProviderName: spName,
                ShippingProviderServiceCode: spServiceCode,
                ShippingProviderAliasHelp: spAliasName,
                AssemblyName: '',
                ShippingProviderNamespace: '',
                ShippingProviderClass: '',
                TempFolderPath: '',
                ExtractedPath: '',
                ManifestFile: '',
                DllFiles: [],
                IsUnique: true,
                IsActive: isActive
            };
            if (isZipInstalled) {
                zipProvider.ShippingProviderID = editFlag;
                provider = zipProvider;
            }
            var aspxTempCommonObj = aspxCommonObj;
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            this.config.data = JSON2.stringify({
                methods: methodList,
                provider: provider,
                isAddedZip: isZipInstalled,
                aspxCommonObj: aspxTempCommonObj
            });
            this.ajaxSuccess = function() {
                if (editFlag > 0) {
                    csscody.info("<h2>" + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping provider has been updated successfully.") + "</p>");
                } else {
                    csscody.info("<h2>" + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping provider has been saved successfully.") + "</p>");
                }
                shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                $('#divShippingProviderDetails').show();
                $('#divEditShippingProvider').hide();
                shippingProviderMgmt.ClearForm();
            };
            this.ajaxCall(this.config);
        },
        DeleteShippingProvider: function(tblID, argus) {
            switch (tblID) {
                case 'tblShippingProviderList':
                    shippingProviderMgmt.DeleteSingleSP(argus[0]);
                    break;
                default:
                    break;
            }
        },
        DeleteSingleSP: function(_shippingProviderID) {
            var properties = {
                onComplete: function(e) {
                    shippingProviderMgmt.ConfirmSingleSPDelete(_shippingProviderID, e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete this shipping provider?") + "</p>", properties);
        },

        ConfirmSingleSPDelete: function(_shippingProviderID, event) {
            if (event) {
                shippingProviderMgmt.DeleteSingleShippingProvider(_shippingProviderID);
            }
            return false;
        },

        DeleteSingleShippingProvider: function(_shippingProviderID) {
            this.config.url = this.config.baseURL + "DeleteShippingProviderByID";
            this.config.data = JSON2.stringify({ shippingProviderID: parseInt(_shippingProviderID), aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ConfirmDeleteMultipleSP: function(shippingProvider_ids, event) {

            if (event) {
                shippingProviderMgmt.DeleteMultipleShippingProviders(shippingProvider_ids);
            }
        },

        DeleteMultipleShippingProviders: function(shippingProvider_ids) {
            this.config.url = this.config.baseURL + "DeleteShippingProviderMultipleSelected";
            this.config.data = JSON2.stringify({ shippingProviderIDs: shippingProvider_ids, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return false;
        },

        SearchShippingProvider: function() {
            var shippingProviderName = $.trim($('#txtSearchShippingProviderName').val());
            if (shippingProviderName.length < 1) {
                shippingProviderName = null;
            }
            var isAct = $.trim($("#ddlSPVisibitity").val()) == "" ? null : ($.trim($("#ddlSPVisibitity").val()) == "True" ? true : false);

            shippingProviderMgmt.BindShippingProviderNameInGrid(shippingProviderName, isAct);
        },
        ajaxSuccess: function(data) {
            switch (shippingProviderMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 2:
                    shippingProviderMgmt.ClearForm();
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Shipping provider has been deleted successfully.") + '</p>');
                    shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                    $('#divShippingProviderDetails').show();
                    $('#divEditShippingProvider').hide();
                    break;
                case 3:
                    shippingProviderMgmt.ClearForm();
                    csscody.info('<h2>' + getLocale(AspxShippingManagement, "Successful Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Selected shipping provider(s) has been deleted successfully.") + '</p>');
                    shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                    $('#divShippingProviderDetails').show();
                    $('#divEditShippingProvider').hide();
                    break;
                case 4:
                    isUnique = data.d;
                    if (data.d == true) {
                        $('#txtSPName').removeClass('error');
                        $('#sperrorLabel').html('');
                    } else {
                    $('#txtSPName').addClass('error');
                    $("#sperrorLabel").show();
                        $('#sperrorLabel').html(getLocale(AspxShippingManagement, 'This provider name already exist!')).css("color", "red");                       
                        return false;
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (shippingProviderMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to save shipping provider!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete shipping provider") + '</p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxShippingManagement, "Error Message") + "</h2><p>" + getLocale(AspxShippingManagement, "Failed to delete selected shipping provider(s)") + '</p>');
                    break;
            }
        },
        CheckShippingProviderUniqueness: function(shippingProviderId) {
            var isUnq = false;
            var sPServiceCode = $.trim($('#txtSPServiceCode').val());
            var sPServiceName = $.trim($('#txtSPName').val());
            var aspxTempCommonObj = aspxCommonObj;
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            this.config.url = this.config.baseURL + "CheckShippingProviderUniqueness";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxTempCommonObj, shippingProviderId: shippingProviderId, shippingProviderName: sPServiceName });
            this.ajaxSuccess = function(data) {
            isUnq = data.d;
            if (isUnq == true) {
                $('#txtSPName').removeClass('error');
                $('#sperrorLabel').html('');
            } else {
                $('#txtSPName').addClass('error');
                $("#sperrorLabel").show();
                $('#sperrorLabel').html(getLocale(AspxShippingManagement, 'This provider name already exist!')).css("color", "red");               
            }
            };
            this.ajaxCall(this.config);
            return isUnq;
        },
        init: function() {
            shippingProviderMgmt.HideAllDiv();
            shippingProviderMgmt.LoadSippingProviderStaticImage();
            $('#divShippingProviderDetails').show();
            shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

            });
            $("#btnSPBack").click(function() {
                $("#divShippingProviderDetails").show();
                $("#divEditShippingProvider").hide();
            });
            $("#btnSPReset").click(function() {
                shippingProviderMgmt.ClearForm();
            });
            $('#btnSPAddNew').click(function() {
                $("#btnSPReset").show();
                $('#divShippingProviderDetails').hide();
                $('#divEditShippingProvider').show();
                shippingProviderMgmt.ClearForm();
                $("#btnSaveShippingProvider").prop("name", 0);
                $("#dvResponse").hide();
            });
            $('#txtSearchShippingProviderName,#ddlSPVisibitity').keyup(function(event) {
                if (event.keyCode == 13) {
                    shippingProviderMgmt.SearchShippingProvider();
                }
            });
            $('#btnSaveShippingProvider').click(function() {
                var v = $("#form1").validate({
                    messages: {
                        name: {
                            required: '*',
                            minlength: getLocale(AspxShippingManagement, "* (at least 2 chars)")
                        },
                        name2: {
                            required: '*',
                            minlength: getLocale(AspxShippingManagement, "* (at least 2 chars)")
                        }
                    }
                });

                if (v.form() && shippingProviderMgmt.CheckShippingProviderUniqueness(editFlag)) {
                    var shippingProvider_id = $(this).prop("name");
                    if (isZipInstalled)
                        shippingProvider_id = editFlag;
                    else
                        editFlag = parseInt(shippingProvider_id);

                    if (shippingProvider_id != '') {
                        shippingProviderMgmt.SaveShippingProvider(shippingProvider_id);
                    } else {
                        shippingProviderMgmt.SaveShippingProvider(0);
                    }
                } else {
                    return false;
                }
            });
            $('#txtSPName').bind('focusout', function() {
                shippingProviderMgmt.CheckShippingProviderUniqueness(editFlag);
            });
            $('#btnSPDeleteSelected').click(function() {
                var shippingProvider_ids = '';
                shippingProvider_ids = SageData.Get("tblShippingProviderList").Arr.join(',');
                if (shippingProvider_ids.length>0) {
                    var properties = {
                        onComplete: function(e) {
                            shippingProviderMgmt.ConfirmDeleteMultipleSP(shippingProvider_ids, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxShippingManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxShippingManagement, "Are you sure you want to delete the selected shipping provider(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxShippingManagement, "Information Alert") + "</h2><p>" + getLocale(AspxShippingManagement, "Please select at least one shipping provider before delete.") + '</p>');
                }
            });
        }
    };

    shippingProviderMgmt.init();
});
