var SearchTerm = "";

$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var aspxCommonObj = {
        StoreID: storeId,
        PortalID: portalId,
        UserName: userName,
        CultureName: cultureName
    };
    SearchTerm = {
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
            url: ""
        },
        init: function() {
            SearchTerm.LoadSearchTermStaticImage();
            SearchTerm.GetSearchTermDetails(null);
            $("#btnDeleteAllSearchTerm").click(function() {
                var searchTermIds = '';
                searchTermIds = SageData.Get("gdvSearchTerm").Arr.join(',');
                if (searchTermIds.length == 0) {
                    csscody.alert('<h2>'+getLocale(AspxSearchTermManagement,"Information Alert")+'</h2><p>'+getLocale(AspxSearchTermManagement,"Please select at least search term(s) before delete.")+'</p>');
                   return false;
                }
                var properties = {
                    onComplete: function(e) {
                        SearchTerm.DeleteSearchTerm(searchTermIds, e);
                    }
                };
                csscody.confirm("<h2>"+getLocale(AspxSearchTermManagement,"Delete Confirmation")+"</h2><p>"+getLocale(AspxSearchTermManagement,"Are you sure you want to delete selected search term(s)?")+"</p>", properties);

            });
            $("#btnSearchTerm").click(function() {
                SearchTerm.SearchTerm();
            });
            $('#txtSearchTerm').keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnSearchTerm").click();
                }
            });
        },
        LoadSearchTermStaticImage: function() {
            $('#ajaxSearchTermImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        ajaxCall: function(config) {
            $.ajax({
                type: SearchTerm.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: SearchTerm.config.contentType,
                cache: SearchTerm.config.cache,
                async: SearchTerm.config.async,
                url: SearchTerm.config.url,
                data: SearchTerm.config.data,
                dataType: SearchTerm.config.dataType,
                success: SearchTerm.ajaxSuccess,
                error: SearchTerm.ajaxFailure
            });
        },

        GetSearchTermDetails: function(searchTerm) {
            this.config.method = "ManageSearchTerms";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvSearchTerm_pagesize").length > 0) ? $("#gdvSearchTerm_pagesize :selected").text() : 10;

            $("#gdvSearchTerm").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                     { display:getLocale(AspxSearchTermManagement,'SearchTermID'), name:'search_term_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'searchTermCheckbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                     { display: getLocale(AspxSearchTermManagement,'Search Term'), name: 'search_term', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxSearchTermManagement,'No Of Use'), name: 'no_of_use', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxSearchTermManagement,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                 ],
                buttons: [
                                    {display:getLocale(AspxSearchTermManagement,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'SearchTerm.DeleteSearchTerms', arguments: '' }
                 ],
                rp: perpage,
                nomsg: getLocale(AspxSearchTermManagement,"No Records Found!"),
                param: { aspxCommonObj: aspxCommonObj, searchTerm: searchTerm },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 3: { sorter: false} }
            });
        },

        DeleteSearchTerms: function(tblID, argus) {
            switch (tblID) {
                case "gdvSearchTerm":
                    var properties = {
                        onComplete: function(e) {
                            SearchTerm.DeleteSearchTerm(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>"+getLocale(AspxSearchTermManagement,"Delete Confirmation")+"</h2><p>"+getLocale(AspxSearchTermManagement,"Are you sure you want to delete this search term?")+"</p>", properties);
                    break;
                default:
                    break;
            }
        },

        DeleteSearchTerm: function(Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteSearchTerm";
                this.config.data = JSON2.stringify({ searchTermID: Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },

        SearchTerm: function() {
            var search = $.trim($("#txtSearchTerm").val());
            if (search.length < 1) {
                search = null;
            }
            SearchTerm.GetSearchTermDetails(search);
        },
        ajaxSuccess: function(msg) {
            switch (SearchTerm.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    SearchTerm.GetSearchTermDetails(null);
                    csscody.info("<h2>"+getLocale(AspxSearchTermManagement,"Successful Message")+"</h2><p>"+getLocale(AspxSearchTermManagement,"Search term has been deleted successfully.")+"</p>");
                    break;
            }
        }
    };
  SearchTerm.init();
});