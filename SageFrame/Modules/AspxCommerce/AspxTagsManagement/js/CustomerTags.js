var CustomerTags = "";
$(function() {

    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var reviewedCustomerName = '';
    CustomerTags = {
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
            CustomerTags.LoadCustoerTagsStaticImage();
            CustomerTags.BindCustomerTags();
            CustomerTags.HideDiv();
            $("#divCustomerTagDetails").show();
            $("#btnBack").click(function() {
                CustomerTags.HideDiv();
                $("#divCustomerTagDetails").show();
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: CustomerTags.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: CustomerTags.config.contentType,
                cache: CustomerTags.config.cache,
                async: CustomerTags.config.async,
                data: CustomerTags.config.data,
                dataType: CustomerTags.config.dataType,
                url: CustomerTags.config.url,
                success: CustomerTags.ajaxSuccess,
                error: CustomerTags.ajaxFailure
            });
        },
        LoadCustoerTagsStaticImage: function() {
            $('#ajaxCustomerImageLoad').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxCustomerTagsImage2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideDiv: function() {
            $("#divCustomerTagDetails").hide();
            $("#divShowCustomerTagList").hide();
        },

        BindCustomerTags: function() {
            this.config.method = "GetCustomerTagDetailsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCusomerTag_pagesize").length > 0) ? $("#gdvCusomerTag_pagesize :selected").text() : 10;

            $("#gdvCusomerTag").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                       { display: getLocale(AspxTagsManagement, 'Customer Name'), name: 'user_name', cssclass: '', coltype: 'label', align: 'left' },
                       { display: getLocale(AspxTagsManagement, 'Total Tags'), name: 'total_tags', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                       { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                   ],

                buttons: [
                       { display: getLocale(AspxTagsManagement, 'View'), name: 'showtags', enable: true, _event: 'click', trigger: '1', callMethod: 'CustomerTags.ShowTagsList', arguments: '0' }
                   ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 2: { sorter: false} }
            });
        },

        ShowTagsList: function(tblID, argus) {
            switch (tblID) {
                case "gdvCusomerTag":
                    $("#" + lblShowHeading).html(getLocale(AspxTagsManagement,'Tags Submitted By:') + argus[3]);
                    $("input[id$='HdnGridData']").val(argus[0]);
                    CustomerTags.BindShowCustomerTagList(argus[0]);
                    reviewedCustomerName = argus[0];
                    CustomerTags.HideDiv();
                    $("#divShowCustomerTagList").show();
                    break;
            }
        },

        BindShowCustomerTagList: function(UserName) {
            aspxCommonObj.UserName = UserName;
            this.config.method = "ShowCustomerTagList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#grdShowTagsList_pagesize").length > 0) ? $("#grdShowTagsList_pagesize :selected").text() : 10;

            $("#grdShowTagsList").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                       { display: getLocale(AspxTagsManagement, 'Item ID'), name: 'itemId', cssclass: '', coltype: 'label', align: 'left', hide: true },
                       { display: getLocale(AspxTagsManagement, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                       { display: getLocale(AspxTagsManagement, 'Tag Name'), name: 'tag_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                       { display: getLocale(AspxTagsManagement, 'Taged On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', format: 'yyyy/MM/dd' },
                       { display: getLocale(AspxTagsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
                   ],

                buttons: [
                   ],
                rp: perpage,
                nomsg: getLocale(AspxTagsManagement, "No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false} }
            });
        },

        ajaxSuccess: function(data) {
            switch (CustomerTags.config.ajaxCallMode) {
                case 0:
                    break;
            }
        }
    }
    CustomerTags.init();
});