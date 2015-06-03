    var PendingTags="";
    $(function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var arrTagItems = new Array();
        var arrTagItemsToBind = new Array();
        PendingTags = {
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
                PendingTags.HideAll();
                PendingTags.LoadPendingTagsStaticImage();
                $("#divShowTagDetails").show();
                PendingTags.BindTagDetails(null);
                PendingTags.GetStatusOfTag();
                $("#btnSaveTag").click(function() {
                    var validTag = $("#form1").validate({
                        messages: {
                            Tag: {
                                required: '*',
                                maxlength: "*"
                            }
                        }
                    });
                    if (validTag.form()) {
                        PendingTags.UpdateTags();
                    } else return false;
                });
                $("#btnBack").click(function() {
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                });
                $("#btnCancel").click(function() {
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                });
                $("#btnSearchPendingTags").click(function() {
                    PendingTags.SearchTags();
                });
                $('#txtSearchTag').keyup(function(event) {
                    if (event.keyCode == 13) {
                        PendingTags.SearchTags();
                    }
                });
                $('#btnApproveAllSelected').click(function() {
                    var tags_ids = '';
                    tags_ids = SageData.Get("gdvTags").Arr.join(',');
                    if (tags_ids.length > 0) {
                        var properties = {
                            onComplete: function(e) {
                                PendingTags.ApproveAllSelectedTags(tags_ids, e);
                            }
                        }
                        csscody.messageInfo("<h2>" + getLocale(AspxTagsManagement, 'Approve Confirmation') + '</h2><p>' + getLocale(AspxTagsManagement, 'Are you sure you want to approve the selected tag(s)?') + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxTagsManagement, 'Information Alert') + '</h2><p>' + getLocale(AspxTagsManagement, 'Please select at least one pending tag before approve it.') + '</p>');
                    }
                });
                $("#ddlTagItemDisplay").change(function() {
                    var items_per_page = $(this).val();
                    $("#Pagination").pagination(arrTagItems.length, {
                        callback: pageselectCallback,
                        items_per_page: items_per_page,
                                                                      prev_text: "Prev",
                        next_text: "Next",
                        prev_show_always: false,
                        next_show_always: false
                    });
                });
            },
            LoadPendingTagsStaticImage: function() {
                $('#ajaxPendingTagsImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: PendingTags.config.type, beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: PendingTags.config.contentType,
                    cache: PendingTags.config.cache,
                    async: PendingTags.config.async,
                    data: PendingTags.config.data,
                    dataType: PendingTags.config.dataType,
                    url: PendingTags.config.url,
                    success: PendingTags.ajaxSuccess,
                    error: PendingTags.ajaxFailure
                });
            },

            ApproveAllSelectedTags: function(tagsIDs, event) {
                if (event) {
                    var newTags = "";
                    var newTagStatus = 3;
                    this.config.url = this.config.baseURL + "UpdateTag";
                    this.config.data = JSON2.stringify({ itemTagIDs: tagsIDs, itemId: null, statusID: newTagStatus, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config);
                }
                return false;
            },

            UpdateTags: function(itemTagIDs) {
                var itemTagIDs = $("#hdnItemTagID").val();
                var hdnStatusID = $("#hdnStatusID").val();
                var hdnTag = $("#hdnTag").val();
                var newTag = $("#txtTag").val();
                var newStatusID = $("#selectStatus").val();
                if (hdnStatusID != newStatusID || hdnTag != newTag) {
                    this.config.url = this.config.baseURL + "UpdateTag";
                    this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, itemId: null, statusID: newStatusID, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                } else {
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                }
            },

            HideAll: function() {
                $("#divEditTag").hide();
                $("#divShowTagDetails").hide();
                $("#divTagedItemsDetails").hide();
            },

            clearTagForm: function() {
                $('#txtTag').removeClass('error');
                $('#txtTag').parents('td').find('label').remove();
            },

            GetStatusOfTag: function() {
                this.config.url = this.config.baseURL + "GetStatus";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config)
            },

            BindTagDetails: function(tags) {
                this.config.method = "GetTagDetailsListPending";
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTags_pagesize").length > 0) ? $("#gdvTags_pagesize :selected").text() : 10;

                $("#gdvTags").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: getLocale(AspxTagsManagement, 'ItemTagIDs'), name: 'itemtag_ids', cssclass: 'cssClassHeadCheckBox', controlclass: 'attribHeaderChkbox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false },
                        { display: getLocale(AspxTagsManagement, 'Tag'), name: 'tag', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTagsManagement, 'User Counts'), name: 'user_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
                        { display: getLocale(AspxTagsManagement, 'Item Counts'), name: 'item_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
                        { display: getLocale(AspxTagsManagement, 'Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'StatusID'), name: 'status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'UserIDs'), name: 'user_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'ItemIDs'), name: 'item_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'Tag Count'), name: 'tag_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],
                    buttons: [
                        { display: getLocale(AspxTagsManagement, 'View'), name: 'view_items', enable: true, _event: 'click', trigger: '3', callMethod: 'PendingTags.ShowTaggedItems', arguments: '1,2,3,4,5,6,7,8,9' },
                                           { display: getLocale(AspxTagsManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'PendingTags.DeleteTags', arguments: '0' }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                    param: { tag: tags, aspxCommonObj: aspxCommonObj },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 9: { sorter: false } }
                });
            },

            EditTags: function(tblID, argus) {
                switch (tblID) {
                case "gdvTags":
                    $("#" + lblEditTagDetails).html("Edit Tag: '" + argus[4] + "'");
                    PendingTags.HideAll();
                    PendingTags.clearTagForm();
                    $("#divEditTag").show();
                    $("#hdnItemTagID").val(argus[3]);
                    $("#hdnTag").val(argus[4]);
                    $("#hdnStatusID").val(argus[5]);
                    $("#txtTag").val(argus[4]);
                    $("#selectStatus").val(argus[5]);
                    break;
                default:
                    break;
                }
            },
            ShowTaggedItems: function(tblID, argus) {
                switch (tblID) {
                case "gdvTags":
                    $("#" + lblTagViewHeading).html("View Pending Tag: '" + argus[3] + "'");
                    PendingTags.BindTagedItemsDetails(argus[9], argus[3]);
                    break;
                default:
                    break;
                }
            },
            BindTagedItemsDetails: function(IDs, tag) {
                this.config.url = this.config.baseURL + "GetItemsByMultipleItemID";
                this.config.data = JSON2.stringify({ itemIDs: IDs, tagName: tag, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            DeleteTags: function(tblID, argus) {
                switch (tblID) {
                case "gdvTags":
                    var properties = {
                        onComplete: function(e) {
                            PendingTags.ConfirmDeleteTag(argus[3], e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxTagsManagement, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxTagsManagement, 'Are you sure you want to delete this tag?') + "</p>", properties);
                    break;
                default:
                    break;
                }
            },

            ConfirmDeleteTag: function(itemTagIDs, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTag";
                    this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 4;
                    this.ajaxCall(this.config);
                }
                return false;
            },

            pageselectCallback: function(page_index, jq) {
                               var items_per_page = $('#ddlTagItemDisplay').val();


                var max_elem = Math.min((page_index + 1) * items_per_page, arrTagItems.length);
                var newcontent = '';
                arrTagItemsToBind.length = 0;

                               for (var i = page_index * items_per_page; i < max_elem; i++) {
                                       arrTagItemsToBind.push(arrTagItems[i]);
                }
                PendingTags.BindResults();


                              
                               return false;
            },

            BindResults: function() {
                $("#divShowTagItemsResult").html('');
                $("#divShowTagItemsResult").html('<table><tbody><tr></tr></tbody></table>');
                $.each(arrTagItemsToBind, function(index, value) {
                    if (value.BaseImage == "") {
                        value.BaseImage = '<%=NoImagePendingTagsPath %>';
                    }
                    if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    var tagItems = '';
                    var isAppend = false;
                    var isNewRow = false;
                    var istrue = (index + 1) % 6;
                    if (istrue != 0) {
                        isAppend = true;
                        tagItems += '<td>';
                        tagItems += ' <div class="cssClassGrid3Box">';
                        tagItems += '<div class="cssClassGrid3BoxInfo">';
                        tagItems += '<h2><a href="' + aspxRedirectPath + 'item/' + value.SKU + pageExtension + '" target="blank">' + value.Name + '</a></h2>';
                        tagItems += '<div class="cssClassGrid3Picture"><img height="81" width="123" src="' + aspxRootPath + "/Modules/AspxCommerce/AspxItemsManagement/uploads/Small/" + value.BaseImage + '" alt="' + value.AlternateText + '" title="' + value.Name + '" /></div>';
                        tagItems += '<div class="cssClassGrid3PriceBox">';
                        tagItems += '<div class="cssClassGrid3PriceBox"><div class="cssClassGrid3Price">';
                        tagItems += ' <p class="cssClassGrid3OffPrice">Price :<span class="cssClassGrid3RealPrice"> <span>' + value.Price + '</span></span> </p>';
                        tagItems += '<div class="cssClassclear"></div></div></div></div>';
                        tagItems += '</td>';
                    } else {
                        isNewRow = true;
                        tagItems += '<tr>';
                        tagItems += '<td>';
                        tagItems += ' <div class="cssClassGrid3Box">';
                        tagItems += '<div class="cssClassGrid3BoxInfo">';
                        tagItems += '<h2><a href="' + aspxRedirectPath + 'item/' + value.SKU + pageExtension + '">' + value.Name + '</a></h2>';
                        tagItems += '<div class="cssClassGrid3Picture"><img height="81" width="123" src="' + aspxRootPath + value.BaseImage + "/Modules/AspxCommerce/AspxItemsManagement/uploads/Small/" + '" alt="' + value.AlternateText + '" title="' + value.Name + '" /></div>';
                        tagItems += '<div class="cssClassGrid3PriceBox">';
                        tagItems += '<div class="cssClassGrid3PriceBox"><div class="cssClassGrid3Price">';
                        tagItems += ' <p class="cssClassGrid3OffPrice">Price :<span class="cssClassGrid3RealPrice"> <span>' + value.Price + '</span></span> </p>';
                        tagItems += '<div class="cssClassclear"></div></div></div></div>';
                        tagItems += '</td>';
                        tagItems += '</tr>';
                    }
                    if (isAppend) {
                        $("#divShowTagItemsResult").find('table>tbody tr:last').append(tagItems);
                    }
                    if (isNewRow) {
                        $("#divShowTagItemsResult").find('table>tbody').append(tagItems);
                    }
                });
            },

            SearchTags: function() {
                var tags = $.trim($("#txtSearchTag").val());
                if (tags.length < 1) {
                    tags = null;
                }
                PendingTags.BindTagDetails(tags);
            },
            ajaxSuccess: function(data) {
                switch (PendingTags.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    PendingTags.BindTagDetails(null);
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                    break;
                case 2:
                    csscody.info('<h2>' + getLocale(AspxTagsManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTagsManagement, 'Tags has been updated successfully.') + '</p>');
                    PendingTags.BindTagDetails(null);
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                    break;
                case 3:
                    $.each(data.d, function(index, item) {
                        $("#selectStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                        $("#ddlStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                    });
                    break;
                case 4:
                    PendingTags.BindTagDetails(null);
                    csscody.info('<h2>' + getLocale(AspxTagsManagement, 'Successful Message') + '</h2><p>' + getLocale(AspxTagsManagement, 'Tags has been deleted successfully.') + '</p>');
                    break;
                case 5:
                    var tableElements = "";
                    PendingTags.HideAll();
                    $("#divTagedItemsDetails").show();
                    $.each(data.d, function(index, item) {
                        tableElements += '<tr>';
                        tableElements += '<td><img height="81" width="100" src="' + aspxRootPath + "/Modules/AspxCommerce/AspxItemsManagement/uploads/Small/" + item.BaseImage + '" alt="' + item.AlternateText + '" title="' + item.Name + '" /></td>';
                        tableElements += '<td>' + item.Name + '</td>';
                        tableElements += '<td>' + item.SKU + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + item.Price + '</td>';
                        tableElements += '</tr>';

                    });
                    $("#divTagedItemsDetails").find('table>tbody').html(tableElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $("#divTagedItemsDetails").find("table>tbody tr:even").addClass("sfEven");
                    $("#divTagedItemsDetails").find("table>tbody tr:odd").addClass("sfOdd");
                    break;
                }
            }
        };
        PendingTags.init();
    });