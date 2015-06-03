var PopularTags = "";
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var tagName = '';
    PopularTags = {
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
            PopularTags.LoadPopularTagsStaticImage();
            PopularTags.BindPopulatTags();
            PopularTags.HideDiv();
            $("#divPopularTagDetails").show();

            $("#btnBack").click(function() {
                PopularTags.HideDiv();
                $("#divPopularTagDetails").show();
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: PopularTags.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: PopularTags.config.contentType,
                cache: PopularTags.config.cache,
                async: PopularTags.config.async,
                data: PopularTags.config.data,
                dataType: PopularTags.config.dataType,
                url: PopularTags.config.url,
                success: PopularTags.ajaxSuccess,
                error: PopularTags.ajaxFailure
            });
        },
        LoadPopularTagsStaticImage: function() {
            $('#ajaxPopulartagsImage').prop("src", aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxPopularTagsImage2').prop('src', aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideDiv: function() {
            $("#divPopularTagDetails").hide();
            $("#divShowPopulartagsDetails").hide();
        },
        BindPopulatTags: function() {
            this.config.method = "GetPopularTagDetailsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPopularTag_pagesize").length > 0) ? $("#gdvPopularTag_pagesize :selected").text() : 10;

            $("#gdvPopularTag").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                        { display: getLocale(AspxTagsManagement, 'Tag Name'), name: 'tag_name', cssclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTagsManagement, 'Popularity'), name: 'popularity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                    ],

                buttons: [
                        { display: getLocale(AspxTagsManagement, 'View'), name: 'showtags', enable: true, _event: 'click', trigger: '1', callMethod: 'PopularTags.ShowTagsDetails', arguments: '0' }
                    ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 2: { sorter: false} }
            });
        },
        ShowTagsDetails: function(tblID, argus) {
            switch (tblID) {
                case "gdvPopularTag":
                    $("#" + lblShowPopularHeading).html(getLocale(AspxTagsManagement,'Tag Details:') + argus[0]);
                    $("input[id$='HdnGridData']").val(argus[0]);
                    PopularTags.ShowPopularTagsList(argus[0]);
                    tagName = argus[0];
                    PopularTags.HideDiv();
                    $("#divShowPopulartagsDetails").show();
                    break;
            }
        },
        ShowPopularTagsList: function(tagName) {
            this.config.method = "ShowPopularTagList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShowPopulatTagsDetails_pagesize").length > 0) ? $("#gdvShowPopulatTagsDetails_pagesize :selected").text() : 10;

            $("#gdvShowPopulatTagsDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                        { display: getLocale(AspxTagsManagement, 'User Name'), name: 'user_name', cssclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTagsManagement, 'Item ID'), name: 'itemId', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxTagsManagement, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
                    ],

                buttons: [
                    ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, tagName: tagName },
                current: current_,
                pnew: offset_,
                sortcol: { 3: { sorter: false} }
            });
        },
        ajaxSuccess: function(data) {
            switch (PopularTags.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    };
    PopularTags.init();
});