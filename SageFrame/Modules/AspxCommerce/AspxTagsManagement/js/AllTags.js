var AllTags="";
$(function() {

    $(".sfLocale").localize({
        moduleKey: AspxTagsManagement
    });

    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var arrTagItems = new Array();
    var arrTagItemsToBind = new Array();
    AllTags = {
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
        LoadItemTagRssImage: function() {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#allTagsRssImage').parent('a').show();
            $('#allTagsRssImage').parent('a').removeAttr('href').prop('href', pageurl + '?type=rss&action=newtags');
            $('#allTagsRssImage').removeAttr('src').prop('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#allTagsRssImage').removeAttr('title').prop('title', getLocale(AspxTagsManagement,'New Tag Rss Feed'));
            $('#allTagsRssImage').removeAttr('alt').prop('alt', getLocale(AspxTagsManagement,'New Tag Rss Feed'));
        },
        init: function() {
            AllTags.HideAll();
            $("#divShowTagDetails").show();
            AllTags.LoadAllTagsStaticImage();
            AllTags.BindTagDetails(null, null);
                       if (newItemTagRss.toLowerCase() == 'true') {
                AllTags.LoadItemTagRssImage();
            }
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
                    AllTags.UpdateTags();
                } else return false;
            });
            $("#tblTagsItems").on("change", ".classStatusList", function() {
                var itemId = $(this).attr("itemid");
                var itemTagId = $(this).attr("tags");
                AllTags.UpdateTags(itemId, itemTagId, $(this).val());
            });
            $("#btnSearchTags").click(function() {
                AllTags.SearchTags();
            });
            $("#btnBack").click(function() {
                AllTags.HideAll();
                $("#divTagedItemsDetails").find('tblTagsItems>tbody').html('');
                $("#divShowTagDetails").show();
            });
            $("#btnCancel").click(function() {
                AllTags.HideAll();
                $("#divShowTagDetails").show();
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
            $('#btnDeleteSelected').click(function() {
                var itemTags_Ids = '';
                               itemTags_Ids = SageData.Get("gdvTags").Arr.join(',');
                if (itemTags_Ids.length>0) {
                    var properties = {
                        onComplete: function(e) {
                            AllTags.ConfirmDeleteMultipleItemTags(itemTags_Ids, e);
                        }
                    };
                    csscody.confirm("<h1>"+getLocale(AspxTagsManagement,'Delete Confirmation')+'</h1><p>'+getLocale(AspxTagsManagement,'Are you sure you want to the selected tag(s)?')+"</p>", properties);
                } else {
                    csscody.alert('<h1>'+getLocale(AspxTagsManagement,'Information Alert')+'</h1><p>'+getLocale(AspxTagsManagement,'Please select at least one tag before delete.')+'</p>');
                }
            });
            $('#txtSearchTag,#ddlStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    AllTags.SearchTags();
                }
            });
        },

        ajaxCall: function(config) {
            $.ajax({
                type: AllTags.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: AllTags.config.contentType,
                cache: AllTags.config.cache,
                async: AllTags.config.async,
                data: AllTags.config.data,
                dataType: AllTags.config.dataType,
                url: AllTags.config.url,
                success: AllTags.ajaxSuccess,
                error: AllTags.ajaxFailure
            });
        },
        LoadAllTagsStaticImage: function() {
            $('#ajaxAllTagsImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        UpdateTags: function(itemId, itemTagId, newStatusID) {
            this.config.url = this.config.baseURL + "UpdateTag";
            this.config.data = JSON2.stringify({ itemTagIDs: itemTagId, itemId: itemId, statusID: newStatusID, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
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
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config)
        },

        BindTagDetails: function(tags, tagStatus) {
            this.config.method = "GetTagDetailsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvTags_pagesize").length > 0) ? $("#gdvTags_pagesize :selected").text() : 10;

            $("#gdvTags").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxTagsManagement,'ItemTagIDs'), name: 'itemtag_ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TagChkbox', elemDefault: false, controlclass: 'tagHeaderChkbox' },
                    { display: getLocale(AspxTagsManagement,'Tag'), name: 'tag', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement,'User Counts'), name: 'user_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
                    { display: getLocale(AspxTagsManagement,'Item Counts'), name: 'item_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
                    { display: getLocale(AspxTagsManagement,'Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement,'StatusID'), name: 'status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement,'UserIDs'), name: 'user_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement,'ItemIDs'), name: 'item_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement,'Tag Count'), name: 'tag_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxTagsManagement,'View'), name: 'view_items', enable: true, _event: 'click', trigger: '3', callMethod: 'AllTags.ShowTaggedItems', arguments: '1,2,3,4,5,6,7,8,9' },
                                   { display: getLocale(AspxTagsManagement,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AllTags.DeleteTags', arguments: '0' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement,"No Records Found!"),
                param: { tag: tags, tagStatus: tagStatus, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 9: { sorter: false } }
            });
        },

        EditTags: function(tblID, argus) {
            switch (tblID) {
            case "gdvTags":
                $("#" + lblEditTagDetails).html("Edit Tag: '" + argus[4] + "'");
                AllTags.HideAll();
                AllTags.clearTagForm();
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

        DeleteTags: function(tblID, argus) {
            switch (tblID) {
            case "gdvTags":
                var properties = {
                    onComplete: function(e) {
                        AllTags.ConfirmDeleteTag(argus[3], e);
                    }
                }
                csscody.confirm("<h1>"+getLocale(AspxTagsManagement,'Delete Confirmation')+'</h1><p>'+getLocale(AspxTagsManagement,'Are you sure you want to delete this tag?')+"</p>", properties);
                break;
            default:
                break;
            }
        },

        ConfirmDeleteTag: function(itemTagIDs, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteTag";
                this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config)
            }
            return false;
        },

        ConfirmDeleteMultipleItemTags: function(Ids, event) {
            AllTags.DeleteItemTagInfo(Ids, event);
        },

        DeleteItemTagInfo: function(_TagID_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteMultipleTag";
                this.config.data = JSON2.stringify({ itemTagIDs: _TagID_Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config)
            }
            return false;
        },

        ShowTaggedItems: function(tblID, argus) {
            switch (tblID) {
            case "gdvTags":
                $("#" + lblTagViewHeading).html(getLocale(AspxTagsManagement,"View Tag:") + argus[3]);
                AllTags.BindTagedItemsDetails(argus[9], argus[3]);
                break;
            default:
                break;
            }
        },
        BindTagedItemsDetails: function(IDs, tagNm) {
            this.config.url = this.config.baseURL + "GetItemsByMultipleItemID";
            this.config.data = JSON2.stringify({ itemIDs: IDs, tagName: tagNm, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        SearchTags: function() {
            var tags = $.trim($("#txtSearchTag").val());
            var tagStatus = '';
            if (tags.length < 1) {
                tags = null;
            }
            if ($("#ddlStatus").val() != 0) {
                tagStatus = $.trim($("#ddlStatus").val());
            } else {
                tagStatus = null;
            }
            AllTags.BindTagDetails(tags, tagStatus);
        },
        ajaxSuccess: function(data) {
            switch (AllTags.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.info('<h2>'+getLocale(AspxTagsManagement,'Successful Message')+'</h2><p>'+getLocale(AspxTagsManagement,'Status has been updated successfully.')+'</p>');
                break;
            case 2:
                $.each(data.d, function(index, item) {
                                                          $(".classStatusList").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                });
                break;
            case 3:
                AllTags.BindTagDetails(null, null);
                csscody.info('<h2>'+getLocale(AspxTagsManagement,'Successful Message')+'</h2><p>'+getLocale(AspxTagsManagement,'Tag has been deleted successfully.')+'</p>');
                break;
            case 4:
                AllTags.BindTagDetails(null, null);
                csscody.info('<h2>'+getLocale(AspxTagsManagement,'Successful Message')+'</h2><p>'+getLocale(AspxTagsManagement,'Selected tag(s) has been deleted successfully.')+'</p>');
                break;
            case 5:
                var tableElements = "";
                AllTags.HideAll();
                $("#divTagedItemsDetails").show();
                $.each(data.d, function(index, item) {
                    $(".classStatusList").html('');
                    tableElements += '<tr>';
                    tableElements += '<td><img height="81" width="100" src="' + aspxRootPath + itemImagePath + item.BaseImage + '" alt="' + item.AlternateText + '" title="' + item.Name + '" /></td>';
                    tableElements += '<td>' + item.Name + '</td>'; 
                    tableElements += '<td>' + item.SKU + '</td>';
                    tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + item.Price + '</td>';
                    if (item.StatusID == 1) {
                        tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel"><select id="status' + item.ItemTagIDs + '" class="classStatusList" itemid=' + item.ItemID + ' tags=' + item.ItemTagIDs + '><option value=1>' + getLocale(AspxTagsManagement, 'Disabled') + '</option><option value=2>' + getLocale(AspxTagsManagement,' Pending')+'</option><option value=3>' + getLocale(AspxTagsManagement,'Approved')+'</option></select></td>';
                    } else if (item.StatusID == 2) {
                        tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel"><select id="status' + item.ItemTagIDs + '" class="classStatusList" itemid=' + item.ItemID + ' tags=' + item.ItemTagIDs + '><option value=2>' + getLocale(AspxTagsManagement, 'Pending')+'</option><option value=1>' + getLocale(AspxTagsManagement,'Disabled')+'</option><option value=3>' + getLocale(AspxTagsManagement,'Approved')+'</option></select></td>';
                    } else if (item.StatusID == 3) {
                        tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel"><select id="status' + item.ItemTagIDs + '" class="classStatusList" itemid=' + item.ItemID + ' tags=' + item.ItemTagIDs + '><option value=3>' + getLocale(AspxTagsManagement, 'Approved')+'</option><option value=1>' + getLocale(AspxTagsManagement, 'Disabled')+'</option><option value=2>' + getLocale(AspxTagsManagement,'Pending')+'</option></select></td>';
                    }
                    tableElements += '</tr>';
                    $("#table>tbody>tr .classStatusList").find("option[text=" + item.Status + "]").prop("selected", "selected");

                });
                $("#divTagedItemsDetails").find('table>tbody').html(tableElements);
                $("#divTagedItemsDetails").find("table>tbody tr:even").addClass("sfEven");
                $("#divTagedItemsDetails").find("table>tbody tr:odd").addClass("sfOdd");
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                break;
            }
        }
    };
    AllTags.init();
});

