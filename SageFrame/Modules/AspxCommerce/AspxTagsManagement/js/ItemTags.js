var ItemTags = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var tagedItemID = '';
    ItemTags = {
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
            ItemTags.LoadItemsTagsStaticImage();
            ItemTags.BindItemTagsDetails();
            ItemTags.HideDiv();
            $("#divItemTagDetails").show();
            $("#btnBack").click(function() {
                ItemTags.HideDiv();
                $("#divItemTagDetails").show();
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: ItemTags.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: ItemTags.config.contentType,
                cache: ItemTags.config.cache,
                async: ItemTags.config.async,
                data: ItemTags.config.data,
                dataType: ItemTags.config.dataType,
                url: ItemTags.config.url,
                success: ItemTags.ajaxSuccess,
                error: ItemTags.ajaxFailure
            });
        },
        LoadItemsTagsStaticImage: function() {
            $('#ajaxItemTagsImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxItemTagsImage2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideDiv: function() {
            $("#divItemTagDetails").hide();
            $("#divShowItemsTagsList").hide();
        },
        BindItemTagsDetails: function() {
            this.config.method = "GetItemTagDetailsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvItemTag_pagesize").length > 0) ? $("#gdvItemTag_pagesize :selected").text() : 10;

            $("#gdvItemTag").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxTagsManagement,'Item ID'), name: 'itemId', cssclass: 'cssClassHide', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxTagsManagement, 'Item Name'), name: 'item_name', cssclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement, 'Number Of Unique Tags'), name: 'number_of_unique_tags', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement, 'Number Of Tags'), name: 'number_of_tags', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
    				],

                buttons: [
                    { display: getLocale(AspxTagsManagement, 'View'), name: 'showtags', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemTags.ShowItemTagsList', arguments: '1,2,3' }
    			    ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false} }
            });
        },

        ShowItemTagsList: function(tblID, argus) {
            switch (tblID) {
                case "gdvItemTag":
                                                                    $("#" + lblShowItemTagHeading).html(getLocale(AspxTagsManagement,'Tags submitted for:') + argus[3]);
                    $("input[id$='HdnGridData']").val(argus[0]);
                    ItemTags.BindItemsTagsList(argus[0]);
                    tagedItemID = argus[0];
                    ItemTags.HideDiv();
                    $("#divShowItemsTagsList").show();
                    break;
            }
        },
        BindItemsTagsList: function(itemId) {
                       this.config.method = "ShowItemTagList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShowItemTagsList_pagesize").length > 0) ? $("#gdvShowItemTagsList_pagesize :selected").text() : 10;

            $("#gdvShowItemTagsList").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxTagsManagement, 'Tag Name'), name: 'tag_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement, 'Tag Use'), name: 'tag_use', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
    				],

                buttons: [
    			    ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, itemID: itemId },
                current: current_,
                pnew: offset_,
                sortcol: { 2: { sorter: false} }
            });
        },
        ajaxSuccess: function(data) {
            switch (ItemTags.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    }
    ItemTags.init();
});