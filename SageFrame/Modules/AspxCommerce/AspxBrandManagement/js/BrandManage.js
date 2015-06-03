var BrandManage = "";
var BrandName = '';
var BrandID = 0;
var editor, html = '';

$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo =  {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };

    var brandIds = '';
    var isUnique = true;
    var prevBrandName = '';
    var isNewBrand = false;
    var BrandName = '';
    BrandManage = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: "",
            error: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: BrandManage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: BrandManage.config.contentType,
                cache: BrandManage.config.cache,
                async: BrandManage.config.async,
                url: BrandManage.config.url,
                data: BrandManage.config.data,
                dataType: BrandManage.config.dataType,
                success: BrandManage.config.ajaxCallMode,
                error: BrandManage.error
            });
        },

        BindBrandlistInGrid: function(brandName) {
            this.config.method = "GetAllBrandList";
            this.config.data = { aspxCommonObj: aspxCommonObj(), brandName: brandName };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblBrandManage_pagesize").length > 0) ? $("#tblBrandManage_pagesize :selected").text() : 10;
            $("#tblBrandManage").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(Brand, "BrandID"), name: '_brandID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'brandChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(Brand, "Brand Name"), name: '_brandName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(Brand, "Brand Description"), name: '_brandDescription', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: 'true' },
                    { display: getLocale(Brand, "Brand Image"), name: '_brandImageUrl', cssclass: '', controlclass: 'cssImage', coltype: 'image', align: 'left' },
                    { display: getLocale(Brand, "Slider View"), name: '_isShowInSlider', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(Brand, "Featured"), name: '_isFeatured', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(Brand, "Featured From"), name: '_featuredFrom', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'MM/dd/yyyy', hide: 'true' },
                    { display: getLocale(Brand, "Featured To"), name: '_featuredTo', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'MM/dd/yyyy', hide: 'true' },
                    { display: getLocale(Brand, "Active"), name: '_isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(Brand, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(Brand, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'BrandManage.EditBrand', arguments: '1,2,3,4,5,6,7,8,9' },
                    { display: getLocale(Brand, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'BrandManage.DeleteBrand', arguments: '1' },
                    { display: getLocale(Brand, "Activate"), name: 'active', enable: true, _event: 'click', trigger: '3', callMethod: 'BrandManage.ActiveBrand', arguments: '' },
                    { display: getLocale(Brand, "Deactivate"), name: 'deactive', enable: true, _event: 'click', trigger: '4', callMethod: 'BrandManage.DeactiveBrand', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(Brand, "No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 3: { sorter: false }, 9: { sorter: false } }
            });
        },
        DeleteBrand: function(tblID, argus) {
            switch (tblID) {
            case "tblBrandManage":
                var properties = {
                    onComplete: function(e) {
                        BrandManage.DeleteBrandByID(argus[0], e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(Brand, "Delete Confirmation") + "</h2><p>" + getLocale(Brand, "Are you sure you want to delete this brand?") + "</p>", properties);
                break;
            default:
                break;
            }

        },
        DeleteBrandByID: function(ids, event) {
            brandIds = ids;
            if (event) {
                var param = JSON2.stringify({ BrandID: brandIds, aspxCommonObj: aspxCommonObj() });
                this.config.method = "DeleteBrand";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = BrandManage.DeleteBrandByIDSuccess;
                this.config.error = BrandManage.DeleteBrandByIDFailure;
                this.ajaxCall(this.config);

            }
        },

        DeleteBrandByIDSuccess: function() {
            BrandManage.BindBrandlistInGrid(null);
            csscody.info("<h2>" + getLocale(Brand, "Successful Message") + "</h2><p>" + getLocale(Brand, "Brand has been deleted successfully.") + "</p>");
        },
        DeleteBrandByIDFailure: function() {
            csscody.error("<h2>" + getLocale(Brand, "Error Message") + "</h2><p>" + getLocale(Brand, "Failed to delete brand") + "</p>");

        },
        DeleteMultipleBrand: function(ids, event) {
            BrandManage.DeleteBrandByID(ids, event);

        },
        EditBrand: function(tblID, argus) {
            switch (tblID) {
                case "tblBrandManage":

                    $('#languageSelect').find('li').each(function () {
                        if ($(this).attr("value") == aspxCommonObj.CultureName) {
                            $('#languageSelect').find('li').removeClass("languageSelected");
                            $(this).addClass("languageSelected");
                            return;

                        }
                    });
                isNewBrand = false;
                $("#txtFeatureFrom").val('');
                $("#txtFeatureTo").val('');
                CKEDITOR.instances.editor1.setData('');
                BrandName = argus[3];
                $("#_lblEditBrandName").html(getLocale(Brand, "Editing Brand: ") + argus[3]);
                BrandManage.HideAllAdvertiseDivs();
                $("#divBrandProviderForm").show();
                $("#txtBrandName").val(argus[3]);
                CKEDITOR.instances.editor1.setData(Encoder.htmlDecode(argus[4]));
                if (argus[6].toUpperCase() == 'YES') {
                    $("#ddlIsShowInSlider").val(1);
                } else {
                    $("#ddlIsShowInSlider").val(0);
                    $("#txtFeatureFrom").prop("disabled", "disabled");
                    $("#txtFeatureTo").prop("disabled", "disabled");
                }

                if (argus[7].toUpperCase() == 'YES') {
                    $("#ddlIsFeatured").val(1);
                    $("#txtFeatureFrom").removeAttr("disabled");
                    $("#txtFeatureTo").removeAttr("disabled");
                    $("#txtFeatureFrom").val(argus[8]);
                    $("#txtFeatureTo").val(argus[9]);
                } else {
                    $("#ddlIsFeatured").val(0);

                }
                var image = argus[5];
                BrandID = argus[0];
                prevBrandName = argus[3];
                $("#divBrandImage").html('<img src="' + aspxRootPath + image + '" height="90px" width="100px"/>');
                break;
            default:
                break;
            }
        },

        RebindBrandOnLangugeChange: function () {
            //Added for rebinding data in language select options
            if (isNewBrand) {
                return false;
            }
            var aspxCommonInfo = aspxCommonObj();
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
                url: BrandManage.config.baseURL + "GetBrandDetailByBrandID",
                data: JSON2.stringify({ brandName: BrandName, aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length>0) {
                        CKEDITOR.instances.editor1.setData('');
                        CKEDITOR.instances.editor1.setData(Encoder.htmlDecode(msg.d[0].BrandDescription));
                    }
                },
                error: function () {
                    alert("error");
                }
            });
        },
       
        ActiveBrand: function(tblID, argus) {
            switch (tblID) {
            case "tblBrandManage":
                BrandManage.ActivateBrandID(argus[0]);
                break;
            default:
                break;
            }
        },
        ActivateBrandID: function(brandID) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: BrandManage.config.baseURL + "ActivateBrand",
                data: JSON2.stringify({ brandID: brandID, aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    csscody.info("<h2>" + getLocale(Brand, "Information Message") + "</h2><p>" + getLocale(Brand, "Brand Activated successfully") + "</p>");
                    BrandManage.BindBrandlistInGrid(null);
                },
                error: function() {
                    alert("error");
                }
            });
        },
        DeactiveBrand: function(tblID, argus) {
            switch (tblID) {
            case "tblBrandManage":
                BrandManage.DeActivateBrandID(argus[0]);
                break;
            default:
                break;
            }
        },

        DeActivateBrandID: function(brandID) {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: BrandManage.config.baseURL + "DeActivateBrand",
                data: JSON2.stringify({ brandID: brandID, aspxCommonObj: aspxCommonInfo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    csscody.info("<h2>" + getLocale(Brand, "Information Message") + "</h2><p>" + getLocale(Brand, "Brand DeActivated successfully") + "</p>");
                    BrandManage.BindBrandlistInGrid(null);
                },
                error: function() {
                    alert("error");
                }
            });
        },
        CheckBrandUniqueNess: function (brandName) {
            var aspxTempCommonObj = aspxCommonObj();
            var param = JSON2.stringify({ brandName: brandName, aspxCommonObj: aspxTempCommonObj });
            this.config.method = "CheckBrandUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.async = false;
            this.config.ajaxCallMode = BrandManage.CheckBrandUniqueNessSuccess;
            this.ajaxCall(this.config);
            return isUnique;
        },
        CheckBrandUniqueNessSuccess: function(data) {
            isUnique = data.d;
            if (data.d == false) {
                csscody.info("<h2>" + getLocale(Brand, "Brand Exists") + "</h2><p>" + getLocale(Brand, "Please choose another brand name") + "</p>");
                return false;
            }
        },
        InsertBrand: function(prevFilePath, defaultImageProductURL, brandID, brandName, description, isShowInSlider, isFeatured, featuredFrom, featuredTo) {
            var brandInsertObj = {
                BrandID: brandID,
                BrandName: brandName,
                BrandDescription: description,
                BrandImageUrl: defaultImageProductURL,
                IsShowInSlider: isShowInSlider,
                IsFeatured: isFeatured,
                FeaturedFrom: featuredFrom,
                FeaturedTo: featuredTo
            };
            var aspxTempCommonObj = aspxCommonObj();
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            var param = JSON2.stringify({ prevFilePath: prevFilePath, aspxCommonObj: aspxTempCommonObj, brandInsertObj: brandInsertObj });
            this.config.method = "InsertNewBrand";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = BrandManage.InsertBrandSuccess;
            this.ajaxCall(this.config);
        },
        InsertBrandSuccess: function() {
            if (BrandID > 0) {
                csscody.info("<h2>" + getLocale(Brand, "Information Message") + "</h2><p>" + getLocale(Brand, "Brand Updated successfully") + " </p>");
                BrandManage.BindBrandlistInGrid(null);
                BrandManage.HideAllAdvertiseDivs();
                $("#divBrandManage").show();
            } else {
                csscody.info("<h2>" + getLocale(Brand, "Information Message") + "</h2><p>" + getLocale(Brand, "Brand Inserted successfully") + "</p>");
                BrandManage.BindBrandlistInGrid(null);
                BrandManage.HideAllAdvertiseDivs();
                $("#divBrandManage").show();
            }
        },
        ImageUploader: function() {
            var upload = new AjaxUpload($('#txtBrandImageUrl'), {
                action: aspxRootPath + 'Modules/AspxCommerce/AspxBrandManagement/fileuploadhandler.aspx',
                name: 'myfile[]',
                multiple: false,
                data: { },
                autoSubmit: true,
                responseType: 'json',
                onChange: function(file, ext) {
                },
                onSubmit: function(file, ext) {
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i .test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(Brand, 'Alert Message') + "</h2><p>" + getLocale(Brand, 'Not a valid image!') + '</p>');
                            return false;
                        }
                    } else {
                        csscody.alert('<h2>' + getLocale(Brand, 'Alert Message') + "</h2><p>" + getLocale(Brand, 'Not a valid image!') + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null && res.Status > 0) {
                        BrandName = res.Message.split('/')[2];
                        BrandManage.AddNewImages(res);
                        return false;
                    } else {
                        csscody.error('<h2>' + getLocale(Brand, "Error Message") + "</h2><p>" + getLocale(Brand, 'Can not upload the image!') + '</p>');
                        return false;
                    }
                }
            });
        },
        AddNewImages: function(response) {
            $("#divBrandImage").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
        },

        ClearData: function() {
            $("#txtBrandName").val('');
            CKEDITOR.instances.editor1.setData('');
            $("#ddlIsShowInSlider").val('0');
            $("#ddlIsFeatured").val('0');
            $("#txtFeatureFrom").prop("disabled", "disabled");
            $("#txtFeatureTo").prop("disabled", "disabled");
            $("#txtFeatureFrom").val('');
            $("#txtFeatureTo").val('');
            $("#txtBrandImageUrl").val('');
            $("#divBrandImage").html('');

        },
        HideAllAdvertiseDivs: function() {
            $("#divBrandManage").hide();
            $("#divBrandProviderForm").hide();
        },
        SearchBrand: function() {
            var brandName = $.trim($("#txtSearchBrandName").val());
            if (brandName.length < 1) {
                brandName = null;
            }
            BrandManage.BindBrandlistInGrid(brandName);
        },
        Init: function() {
            BrandManage.BindBrandlistInGrid(null);
            BrandManage.HideAllAdvertiseDivs();
            $("#divBrandManage").show();
            $("#btnAddNewBrand").click(function() {
                BrandID = 0;
                isNewBrand = true;
                $('#_lblEditBrandName').html(getLocale(Brand,"Add New Brand"));
                BrandManage.HideAllAdvertiseDivs();
                $("#divBrandProviderForm").show();
            });
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");
                BrandManage.RebindBrandOnLangugeChange();

            });
            $("#btnDeleteSelected").click(function() {
                var brand_ids = '';
                brand_ids = SageData.Get("tblBrandManage").Arr.join(',');
                if (brand_ids.length == 0) {
                    csscody.alert('<h2>' + getLocale(Brand, 'Information Alert') + '</h2><p>' + getLocale(Brand, 'None of the brand are selected') + '</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        BrandManage.DeleteMultipleBrand(brand_ids, e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(Brand, "Delete Confirmation") + "</h2><p>" + getLocale(Brand, "Do you want to delete?") + "</p>", properties);
            });
            $("#btnSave").bind('click', function() {
                var prevFilePath = '';
                var featuredFrom = null;
                var featuredTo = null;
                var defaultImageProductURL = "";
                if ($("#divBrandImage>img").prop("src") == null) {
                    defaultImageProductURL = "";
                } else {
                    defaultImageProductURL = $("#divBrandImage>img").attr("src").replace(aspxRootPath, "");
                }
                var brandName = $("#txtBrandName").val();
                var description = Encoder.htmlEncode(CKEDITOR.instances.editor1.getData());
                var isShowInSlider = $("#ddlIsShowInSlider").val() == '0' ? false : true;
                var isFeatured = $("#ddlIsFeatured").val() == '0' ? false : true;
                if (brandName == "") {
                    csscody.alert('<h2>' + getLocale(Brand, 'Alert Message') + "</h2><p>" + getLocale(Brand, 'Please enter brand name!') + '</p>');
                    return false;
                }
                if (isFeatured == true) {
                    featuredFrom = $("#txtFeatureFrom").val();
                    featuredTo = $("#txtFeatureTo").val();
                    if (featuredFrom == "" || featuredTo == "") {
                        csscody.alert('<h2>' + getLocale(Brand, 'Alert Message') + "</h2><p>" + getLocale(Brand, 'Please enter valid date!') + '</p>');
                        return false;
                    }
                }
                if (defaultImageProductURL != "") {
                    if (BrandID == 0) {
                        if (!BrandManage.CheckBrandUniqueNess(brandName)) {
                            return false;
                        }
                    } else if (BrandID != 0 && brandName != prevBrandName) {
                        if (!BrandManage.CheckBrandUniqueNess(brandName)) {
                            return false;
                        }
                    }
                    BrandManage.InsertBrand(prevFilePath, defaultImageProductURL, BrandID, brandName, description, isShowInSlider, isFeatured, featuredFrom, featuredTo);
                    BrandManage.ClearData();
                } else {
                    csscody.alert('<h2>' + getLocale(Brand, 'Alert Message') + '</h2><p>' + getLocale(Brand, 'Choose Image!') + '</p>');
                    return false;
                }
            });
            $("#btnShow").click(function() {
                BrandManage.BindBrandlistInGrid(null);
            });
            $("#btnCancel").click(function() {
                BrandManage.ClearData();
                BrandManage.HideAllAdvertiseDivs();
                $("#divBrandManage").show();
            });
            $("#btnSearch").click(function() {
                BrandManage.SearchBrand();
            });
            $("#txtSearchBrandName").keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnSearch").click();
                }
            });
            $("#txtFeatureFrom").datepicker({
                           changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                    $("#txtFeatureTo").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#txtFeatureTo").datepicker({
                           changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                                   }
            });
            $("#ddlIsFeatured").on("change", function() {
                if ($(this).val() == "0") {
                    $("#txtFeatureFrom").val('');
                    $("#txtFeatureTo").val('');
                    $("#txtFeatureFrom").prop("disabled", "disabled");
                    $("#txtFeatureTo").prop("disabled", "disabled");
                } else {
                    $("#txtFeatureFrom").removeAttr("disabled");
                    $("#txtFeatureTo").removeAttr("disabled");
                }
            });
            BrandManage.ImageUploader();
        }
    };
    BrandManage.Init();
});