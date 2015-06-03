<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCardCategory.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxGiftCardManagement_GiftCardCategory" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxGiftCardManagement
        });
    });
    var aspxItemModulePath = '<%=aspxItemModulePath %>';
    var Grid;
    $(function () {
        var umi = '<%=UserModuleID%>';
        var giftCard = function () {
            var aspxCommonObj = function () {
                var aspxCommonInfo = {
                    StoreID: AspxCommerce.utils.GetStoreID(),
                    PortalID: AspxCommerce.utils.GetPortalID(),
                    UserName: AspxCommerce.utils.GetUserName(),
                    CultureName: AspxCommerce.utils.GetCultureName()
                };
                return aspxCommonInfo;
            };
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
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };

            var graphicImageUrl = '';
            var deleteUnSavedImage = function (path) {
                $.post(aspxservicePath + 'AspxCoreHandler.ashx/DeleteTempFile', { path: path });
            };
            var $ajaxUpload = function (uploaderID) {

                new AjaxUpload(String(uploaderID), {
                    action: aspxItemModulePath + '/FileUploader.aspx',
                    name: 'myfile',
                    onSubmit: function (file, ext) {
                        var baseLocation = "Modules/AspxCommerce/AspxItemsManagement/uploads/GiftCard";
                        var validExt = "jpg jpeg png gif";
                        var maxFileSize = "1024";
                        var regExp = /\s+/g;
                        var myregexp = new RegExp("(" + validExt.replace(regExp, "|") + ")", "i");
                        if (ext != "exe") {
                            if (ext && myregexp.test(ext)) {
                                this.setData({
                                    'BaseLocation': baseLocation,
                                    'ValidExtension': validExt,
                                    'MaxFileSize': maxFileSize,
                                    'IsGiftCard': true
                                });
                            } else {
                                csscody.alert('<h2>' + getLocale(AspxGiftCardManagement, "Information Alert") + '</h2><p>' + getLocale(AspxGiftCardManagement, "You are trying to upload invalid file type!") + '</p>');
                                return false;
                            }
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxGiftCardManagement, "Information Alert") + '</h2><p>' + getLocale(AspxGiftCardManagement, "You are trying to upload invalid file type!") + '</p>');
                            return false;
                        }
                    },
                    onComplete: function (file, ajaxFileResponse) {

                        var res = eval(ajaxFileResponse);
                        if (res.Status > 0) {
                            var fileExt = (-1 !== file.indexOf('.')) ? file.replace(/.*[.]/, '') : '';
                            var myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png)", "i");
                            if (myregexp.test(fileExt)) {
                                graphicImageUrl = res.UploadedPath;
                                $("#ThemePreview").html('').html('<div class="cssClassGcPreview"><img src="' + aspxRootPath + res.UploadedPath + '" class="uploadImage" height="90px" width="100px" /></div>');
                            } else {
                                $("#ThemePreview").html('').html('<div class="cssClassLeft"><a href="' + aspxRootPath + res.UploadedPath + '" class="uploadFile" target="_blank">' + file + '</a></div>');
                            }
                        } else {
                            csscody.error('<h2>' + getLocale(AspxGiftCardManagement, "Error Message") + '</h2><p>' + res.Message + '</p>');
                        }
                    }
                });

            };
            var checkExistCategory = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var aspxTempCommonObj = aspxCommonInfo;
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                var catName = $.trim($("#txtGCCategoryName").val());
                var param = JSON2.stringify({ aspxCommonObj: aspxTempCommonObj, giftcardCategoryName: catName });
                $ajaxCall("CheckGiftCardCategory", param, checkResult, null);
            };
            var checkResult = function (data) {
                if (!data.d) {
                    if ($("#themesSlider").find('img').length == 0) {
                        csscody.alert("<h2>" + getLocale(AspxGiftCardManagement, "Information Alert") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Please upload a theme image!") + "</p>");
                    }
                    else {
                        saveNewGiftCardCategory(graphicid);
                        graphicid = "";
                    }
                } else {
                    csscody.alert('<h2>' + getLocale(AspxGiftCardManagement, "Information Alert") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Category name is already exist!") + '</p>');
                }
            };
            var getGiftCardCategory = function (categoryName, addedon, status) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var param = {
                    aspxCommonObj: aspxCommonInfo,
                    categoryName: categoryName, addedon: addedon, status: status
                };
                var data = param;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvGiftCardCategory_pagesize").length > 0) ? $("#gdvGiftCardCategory_pagesize :selected").text() : 10;

                $("#gdvGiftCardCategory").sagegrid({
                    url: aspxservicePath + 'AspxCoreHandler.ashx/',
                    functionMethod: "GetAllGiftCardCategoryGrid",
                    colModel: [
                        { display: getLocale(AspxGiftCardManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: getLocale(AspxGiftCardManagement, 'Gift Card Category'), name: 'GetAllGiftCardCategory', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxGiftCardManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                        { display: getLocale(AspxGiftCardManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                        { display: getLocale(AspxGiftCardManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],
                    buttons: [
                        { display: getLocale(AspxGiftCardManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'Grid.Edit', arguments: '1,2,3' },
                        { display: getLocale(AspxGiftCardManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'Grid.Delete', arguments: '1' }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxGiftCardManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 4: { sorter: false } }
                });



            };

            var bindGiftCartCategory = function (data) {

                var options1 = '<option value="0" selected="selected" >' + getLocale(AspxGiftCardManagement, "All") + '</option>';
                var options;
                $.each(data.d, function (index, item) {
                    options += "<option value=" + item.GiftCardCategoryId + ">" + item.GiftCardCategory + "</option>";
                });
                $("#ddlGCCategory").html('').append(options1 + options);
                $("#editGCCategory,#ddlGCCategoryImg").html('').append('<option value="0">' + getLocale(AspxGiftCardManagement, "Choose One") + '</option>' + options);
            };
            var saveGiftCardCategory = function (cid) {
                var catName = $.trim($("#txtGCCategoryName").val());
                var aspxTempCommonObj = aspxCommonObj();
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                var param = JSON2.stringify({ giftCardCategoryId: cid, aspxCommonObj: aspxTempCommonObj, giftcardCategoryName: catName, isActive: $("input[name=categoryisActive]:checked").val() == 1 ? true : false });
                $ajaxCall("SaveGiftCardCategory", param, null, null);
                catId = 0;
                isNewCat = false;
                $("#giftCardEdit").hide();
                $("#dvGrdCategory").show();
                //$("#divGCThemes").hide();
                csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card category has been saved successfully.") + '</p>');
                getGiftCardCategory(null, null, null);
            };

            var saveNewGiftCardCategory = function (graphicid) {
                var catName = $.trim($("#txtGCCategoryName").val());
                var aspxTempCommonObj = aspxCommonObj();
                aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
                var param = JSON.stringify({ giftCardGraphicId: graphicid, aspxCommonObj: aspxTempCommonObj, giftcardCategoryName: catName, isActive: $("input[name=categoryisActive]:checked").val() == 1 ? true : false });
                $ajaxCall("SaveNewGiftCardCategory", param, null, null);
                isNewCat = false;
                $("#giftCardEdit").hide();
                $("#dvGrdCategory").show();
                //$("#divGCThemes").hide();
                csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card category has been saved successfully.") + '</p>');
                getGiftCardCategory(null, null, null);
            };

            var deleteGiftCardCategory = function (id) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var param = JSON2.stringify({ giftCardCategoryId: id, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("DeleteGiftCardCategory", param, null, null);
                getGiftCardCategory(null, null, null);
            };

            var deleteGiftCardThemeImage = function (id, index) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var giftCardGraphicId = 0;
                var param = JSON2.stringify({ giftCardGraphicId: id, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("DeleteGiftCardThemeImage", param, function () {
                    getAllGiftCardThemeImage(catId);
                    if (index == 0) {
                        csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card theme image has been deleted successfully.") + '</p>');
                    }
                },
                    null);

            };

            var saveGiftCardThemeImage = function () {
                var graphicThemeName = $.trim($("#txtGCThemeName").val());
                var cid = parseInt(catId);
                var graphicImage = graphicImageUrl;
                if (!isNewCat) {
                    var param = JSON2.stringify({ graphicThemeName: graphicThemeName, graphicImage: graphicImage, giftCardCategoryId: cid, aspxCommonObj: aspxCommonObj() });
                    $ajaxCall("SaveGiftCardThemeImage", param, function (d) {
                        graphicImageUrl = '';
                        $("#ThemePreview").html('');
                        $('#fade, #popuprel5').fadeOut();
                        getAllGiftCardThemeImage(catId);
                        csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card theme image has been saved successfully.") + '</p>');
                    }, null);
                }
                else {
                    var param = JSON2.stringify({ graphicThemeName: graphicThemeName, graphicImage: graphicImage, aspxCommonObj: aspxCommonObj() });
                    $ajaxCall("SaveGiftCardThemeImageReturnGiftCardGraphicId", param, function (d) {
                        graphicid += (d.d + ",");
                        graphicImageUrl = '';
                        $("#ThemePreview").html('');
                        $('#fade, #popuprel5').fadeOut();
                        getAllGiftCardThemeImage(catId);
                        csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card theme image has been saved successfully.") + '</p>');
                    }, null);
                }
            };

            var saveGiftCardItemCategory = function (id) {
                var ids = $("#ddlGCCategory").val() == null ? 0 : $("#ddlGCCategory").val().join(',');
                var param = JSON2.stringify({ itemId: id, ids: ids, aspxCommonObj: aspxCommonObj() });
                $ajaxCall("SaveGiftCardItemCategory", param, null, null);
            };


            var getAllGiftCardThemeImage = function (id) {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonInfo, categoryId: id });
                $ajaxCall("GetAllGiftCardThemeImage", param, bindAllGiftCardThemeImage, null);
            };

            var bindAllGiftCardThemeImage = function (data) {
                if (data.d.length > 0) {
                    var $ul = $("<ul>").attr('id', 'themesSlider').addClass("jcarousel-skin-tango");
                    $.each(data.d, function (index, item) {
                        if (item.GraphicImage != null) {
                            var $li = $("<li>");
                            var $a = $("<a>");
                            var $img = $("<img>");
                            $a.addClass("cssClassDelImg").attr("href", "#").attr('data-id', item.GiftCardGraphicId).html('X').attr('style', 'float:right;position:relative;top:10px;');
                            $img.attr('width', '75').attr('height', '75').attr("src", aspxRootPath + item.GraphicImage).attr('alt', item.GraphicName);
                            $li.append($a).append($img);
                            $ul.append($li);
                        }
                    });
                    $(".cssSlider").html('').append($ul);

                    $('#themesSlider').css('width', 1000);
                    $(".cssClassDelImg").unbind().bind("click", function () {

                        var id = $(this).attr('data-id');
                        var properties = {
                            onComplete: function (e) {
                                if (e)
                                    deleteGiftCardThemeImage(id);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxGiftCardManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Are you sure you want to delete the Gift Card theme image?") + "</p>", properties);

                    });
                    $('#themesSlider').jcarousel();
                } else {
                    $(".cssSlider").html('').append("<span>" + getLocale(AspxGiftCardManagement, "No Images") + "</span>");
                }
            };
            var clear = function () {
                $("#txtGCCategoryName").val('');
                $("input[name=categoryisActive][value=1]").prop('checked', 'checked');
                $(".cssSlider").html('')
            };

            Grid = function () {
                var edit = function (tbl, args) {
                    $('#languageSelect').find('li').each(function () {
                        if ($(this).attr("value") == AspxCommerce.utils.GetCultureName()) {
                            $('#languageSelect').find('li').removeClass("languageSelected");
                            $(this).addClass("languageSelected");
                            return;

                        }
                    });
                    clear();
                    $("#giftCardEdit").show();
                    $("#dvGrdCategory").hide();
                    $("#divGCThemes").show();
                    catId = args[0];
                    getAllGiftCardThemeImage(catId);
                    isNewCat = false;
                    $("#txtGCCategoryName").val(args[3]);
                    $("#txtGCCategoryName").data("CatID", catId);
                    if (args[5] == "true" || args[5] == "yes" || args[5].toLowerCase() == "active") {
                        $("input[name=categoryisActive][value=1]").prop('checked', 'checked');
                    } else {
                        $("input[name=categoryisActive][value=0]").prop('checked', 'checked');
                    }
                    $("#giftCardEdit .cssClassHeader>h2>span").html(getLocale(AspxGiftCardManagement, 'Edit Category'));
                    $("#txtGCCategoryName").next('span.iferror').hide();

                };
                var deleteCat = function (tbl, args) {
                    var properties = {
                        onComplete: function (e) {
                            if (e) {
                                deleteGiftCardCategory(args[0], 0);
                                csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card category has been deleted successfully.") + '</p>');
                            }
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxGiftCardManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Are you sure you want to delete the Gift Card category?") + "</p>", properties);

                };
                var deleteAll = function () {
                    var ids = [];
                    $("#gdvGiftCardCategory .attrChkbox").each(function (i) {
                        if ($(this).is(":checked")) {
                            ids.push($(this).val());
                        }
                    });
                    if (ids.length > 0) {
                        var properties = {
                            onComplete: function (e) {
                                if (e) {
                                    for (var z = 0; z < ids.length; z++) {
                                        deleteGiftCardCategory(ids[z], z);
                                    }
                                    csscody.info('<h2>' + getLocale(AspxGiftCardManagement, "Successful Message") + '</h2><p>' + getLocale(AspxGiftCardManagement, "Gift Card category has been deleted successfully.") + '</p>');
                                }

                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxGiftCardManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Are you sure you want to delete all selected Gift Card category?") + "</p>", properties);


                    } else
                        csscody.alert("<h2>" + getLocale(AspxGiftCardManagement, "Information Alert") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Please select Gift Card category to delete!") + "<p>");

                };
                var reload = function () {
                    //Added for rebinding data in language select options
                    if (isNewCat) {
                        return false;
                    }
                    var aspxCommonInfo = aspxCommonObj();
                    if ($("#languageSelect").length > 0) {
                        aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
                    }
                    var param = JSON2.stringify({ categoryID: $("#txtGCCategoryName").data('CatID'), aspxCommonObj: aspxCommonInfo });
                    $ajaxCall("GetGiftCardCategoryDetailByID", param, function (msg) {
                        $("#txtGCCategoryName").val(msg.d[0].GiftCardCategory);
                    }, null);
                };
                return {
                    Edit: edit,
                    Delete: deleteCat,
                    DeleteAll: deleteAll,
                    Reload:reload
                };
            }();
            var load = function () {
                getGiftCardCategory(null, null, null);
                $ajaxUpload('fuGCThemeImage');
                bindFunctions();
                resetAll();
                $("#languageSelect li").click(function () {
                    $('#languageSelect').find('li').removeClass("languageSelected");
                    $(this).addClass("languageSelected");
                    Grid.Reload();
                });
            };
            var catId = 0;
            var isNewCat = false;
            var graphicid = "";
            var isValidForm = function (form) {
                switch (form) {
                    case 'graphicImage':
                        $("#txtGCThemeName").next('span.iferror').hide();
                        if (graphicImageUrl == "") {
                            $("#ThemePreview").html(getLocale(AspxGiftCardManagement, "Please Upload File First!!"));
                        }
                        if (!isNewCat) {
                            if (parseInt(catId) != 0 && $.trim($("#txtGCThemeName").val()) != "" && graphicImageUrl != "" && $("#ThemePreview").find('img').length > 0)
                                return true;
                            else {
                                $("#txtGCThemeName,#fuGCThemeImage").next('span.iferror').show();
                            }
                        }
                        else {
                            if ($.trim($("#txtGCThemeName").val()) != "" && graphicImageUrl != "" && $("#ThemePreview").find('img').length > 0) {
                                return true;
                            } else {
                                $("#txtGCThemeName,#fuGCThemeImage").next('span.iferror').show();
                            }
                        }
                        break;
                    case 'addcategory':
                        $("#txtGCCategoryName").next('span.iferror').hide();
                        if ($.trim($("#txtGCCategoryName").val()) != "")
                            return true;
                        else {
                            $("#txtGCCategoryName").next('span.iferror').show();
                        }
                        break;
                    case 'editCategory':
                        $("#txtGCCategoryName").next('span.iferror').hide();
                        if (parseInt($("editGCCategory").val()) != 0 && $.trim($("#txtGCCategoryName").val()) != "")
                            return true;
                        else {
                            $("#txtGCCategoryName").next('span.iferror').show();

                        }
                        break;
                }
            };
            var searchCategory = function () {
                var categoryName = $.trim($("#txtGiftCardCategory").val());
                var addedon = $.trim($("#txtCategoryAddedOn").val()) == "" ? null : $.trim($("#txtCategoryAddedOn").val());
                var status = $.trim($("#ddlGiftCardCategoryStatus").val()) == "0" ? null : $.trim($("#ddlGiftCardCategoryStatus").val());
                getGiftCardCategory(categoryName, addedon, status);
            };
            var bindFunctions = function () {
                $("#txtGiftCardCategory,#txtCategoryAddedOn").bind("keypress", function (e) {
                    if (e.which == 13) {
                        searchCategory();
                    }
                });
                $("#btnDeleteAllCategory").unbind().on("click", function () {
                    Grid.DeleteAll();
                });
                $("#btnAddCategory").unbind().on("click", function () {
                    clear();
                    $("#giftCardEdit .cssClassHeader>h2>span").html(getLocale(AspxGiftCardManagement, 'Add Category'));
                    catId = 0;
                    isNewCat = true;
                    $('#giftCardEdit').show();
                    $("#dvGrdCategory").hide();
                    $("#txtGCCategoryName").next('span.iferror').hide();

                });
                $("#btnAddGiftCardThemeImage").unbind().on("click", function () {
                    ShowPopupControl('popuprel5');
                    $("#txtGCThemeName").val('');
                    $("#ThemePreview").html('');
                    $("#fade").unbind().on("click", function () {
                        $('#fade, #popuprel5').fadeOut();
                        if (graphicImageUrl != '')
                            deleteUnSavedImage(graphicImageUrl);
                    });
                });

                $("#btnCancelGCCategory").unbind().on("click", function () {
                    $('#giftCardEdit').hide();
                    $("#dvGrdCategory").show();
                    //$("#divGCThemes").hide();
                    catId = 0;
                    isNewCat = false;
                });
                $("#btnCancelGCThemeImage").unbind().on("click", function () {
                    $('#fade, #popuprel5').fadeOut();
                    if (graphicImageUrl != '')
                        deleteUnSavedImage(graphicImageUrl);
                });

                $('.cssClassClose').unbind().on("click", function () {
                    if (!$(".popupbox#popuprel5").is(":hidden")) {
                        if (graphicImageUrl != '')
                            deleteUnSavedImage(graphicImageUrl);
                    }
                    $('#fade, .popupbox').fadeOut();
                });

                $("#btnSetGCCategory").unbind().on("click", function () {
                    saveGiftCardItemCategory();

                });

                $("#btnSaveGCCategory").off().on("click", function () {
                    var form = isNewCat ? 'addcategory' : 'editCategory';
                    if (isValidForm(form)) {
                        $("#ddlGCCategory").val();
                        $.trim($("#txtGCThemeName").val());
                        if (isNewCat) {
                            checkExistCategory();

                        } else {
                            if ($("#themesSlider").find('img').length == 0) {
                                csscody.alert("<h2>" + getLocale(AspxGiftCardManagement, "Information Alert") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Please upload a theme image!") + "</p>");

                            } else {
                                saveGiftCardCategory(catId);
                            }
                        }
                    }
                });
                $("#btnDelete").off().on("click", function () {
                    var id = parseInt($("#editGCCategory").find("option:selected").val());
                    if (id == 0) {
                        csscody.alert("<h2>" + getLocale(AspxGiftCardManagement, "Information Alert") + "</h2><p>" + getLocale(AspxGiftCardManagement, "Please select Gift Card category!") + "</p>");

                    } else {
                        var properties = {
                            onComplete: function (e) {
                                if (e)
                                    deleteGiftCardCategory(id);
                            }
                        };
                    }
                });

                $("#btnSaveGCThemeImage").unbind().on("click", function () {
                    if (isValidForm('graphicImage')) {
                        saveGiftCardThemeImage();
                    }

                });
                $("#btnGiftCardCategoryManage").on("click", function () {
                    $("#dvGiftCard").show();
                    $("#dvManageGiftCardCategory").hide();
                });


                $("#btnSearchGiftCardCategory").unbind().on("click", function () {
                    searchCategory();
                });
                $("#txtCategoryAddedOn").datepicker({ changeYear: true, changeMonth: true, dateFormat: 'yy/mm/dd' });

            };
            var resetAll = function () {

                $("#txtGCCategoryName").val('');
                $("#txtGCThemeName").val('');
                $("#ThemePreview").html('');
                $("#ddlGCCategory").val(0);

                $("#aAddCategory").parents("tr").show();
                $("#trEditGC").show();
                $("#giftCardEdit .cssClassHeader>h2>span").html(getLocale(AspxGiftCardManagement, 'Edit Category'));

            };
            return {
                Init: load,
                SaveCategory: saveGiftCardCategory,
                SaveThemeImage: saveGiftCardThemeImage,
                DeleteCategory: deleteGiftCardCategory,
                DeleteThemeImage: deleteGiftCardThemeImage,
                SaveItemCategory: saveGiftCardItemCategory
            };
        }();
        giftCard.Init();
    });

</script>

<div id="dvManageGiftCardCategory">
    <div id="dvGrdCategory" class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label runat="server" ID="lblGiftCardCategory"
                    meta:resourcekey="lblGiftCardCategoryResource1">Gift Card Category</asp:Label></h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button id="btnAddCategory" type="button" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New </span>
                        </button>
                    </p>
                    <p>
                        <button id="btnDeleteAllCategory" type="button" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>

                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Category:</label>
                                <input type="text" id="txtGiftCardCategory" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Added On:</label>
                                <input type="text" id="txtCategoryAddedOn" class="sfTextBoxSmall" />

                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Status:
                                </label>
                                <select id="ddlGiftCardCategoryStatus" class="sfSelect">
                                    <option value="0" class="sfLocale">-- All -- </option>
                                    <option value="True" class="sfLocale">Active </option>
                                    <option value="False" class="sfLocale">Inactive </option>
                                </select>
                            </td>
                            <td>
                                <button type="button" id="btnSearchGiftCardCategory" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxStoreAccessImage1" src="" class="sfLocale" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvGiftCardCategory" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>

    <div id="giftCardEdit" style="display: none; width: 100%;">
        <div class="sfGridWrapperContent">
            <div class="cssClassCommonBox ">
                <div class="cssClassHeader">
                    <h2>
                        <asp:Label ID="Label1" runat="server" Text="Edit Category"
                            meta:resourcekey="Label1Resource1"></asp:Label>
                    </h2>
                </div>
                <div class="sfFormwrapper">
                     <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                    <table cellspacing="0" cellpadding="o" border="0">

                        <tr>
                            <td class="cssClassGiftCatTd">
                                <label class="cssClassLabel sfLocale">
                                    Category Name:
                                </label>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <input type="text" class="sfTextBoxSmall" id="txtGCCategoryName" /><span class="iferror"
                                    style="color: red;">*</span>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="cssClassGiftCatTd">
                                <label class="cssClassLabel sfLocale">
                                    Active:
                                </label>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <label>
                                    <input type="radio" name="categoryisActive" value="1" checked="checked" /><span class="sfLocale">Active</span></label>
                                &nbsp;&nbsp;
                                 <label>
                                     <input type="radio" name="categoryisActive" value="0"/><span class="sfLocale">InActive</span></label>
                            </td>
                            <td></td>
                        </tr>
                        <tr id="divGCThemes">
                            <td class="cssClassGiftCatTd">
                                <label class="cssClassLabel sfLocale">Category themes:</label>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <div class="cssSlider">
                                </div>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <input type="button" id="btnAddGiftCardThemeImage" value="Add Theme Image" class="sfBtn sfLocales" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnCancelGCCategory" class="sfBtn">
                            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnSaveGCCategory" class="sfBtn">
                            <span class="sfLocale icon-save">Save</span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="popupbox" id="popuprel5">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale">Close</span></button>
        </div>
        <div class="sfGridWrapperContent">
            <div class="cssClassCommonBox " style="width: 400px;">
                <div class="cssClassHeader">
                    <h2>
                        <asp:Label ID="Label2" runat="server" Text="Add New Theme Image"
                            meta:resourcekey="Label2Resource1"></asp:Label>
                    </h2>
                </div>
                <div>
                    <table cellspacing="0" cellpadding="o" border="0" width="100%">
                        <tr>
                            <td class="cssClassGiftCatTd">
                                <label class="sfLocale">
                                    Theme Name:
                                </label>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <input type="text" class="sfInputbox" id="txtGCThemeName" /><span class="iferror"
                                    style="color: red;">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassGiftCatTd">
                                <label class="sfLocale">
                                    Image File:
                                </label>
                            </td>
                            <td class="cssClassGiftCatTd">
                                <input type="file" class="" id="fuGCThemeImage" />
                                <span class="iferror" style="color: red;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td id="ThemePreview"></td>
                        </tr>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnSaveGCThemeImage" class="sfBtn">
                            <span class="sfLocale icon-save">Save</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnCancelGCThemeImage" class="sfBtn">
                            <span class="sfLocale icon-close">Cancel</span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
