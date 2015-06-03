(function ($) {
    var arrResultToBind = new Array();
    var SortID = 0;
    var arrItemListType = new Array();
    rowTotal = 0;
    currentPage = 0;
    $.createSearchResult = function (p) {
        p = $.extend
        ({
            PortalID: '',
            IsUseFriendlyUrls: '',
            UserName: '',
            baseURL: '',
            CulturalName: '',
            ViewPerPage: 10
        }, p);
        var SearchResult = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: '',
                method: "",
                url: "",
                ajaxCallMode: 0,
                PortalID: p.PortalID,
                IsUseFriendlyUrls: p.IsUseFriendlyUrls,
                UserName: p.UserName,
                baseURL: p.baseURL + "WebService.asmx/"
            },
            init: function () {
                SearchResult.GetSearchResult(1, p.ViewPerPage, 0);
            },
            ajaxSuccess: function (data) {
                switch (SearchResult.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        SearchResult.BindSearchResult(data);
                        break;
                }
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({

                    type: SearchResult.config.type,
                    contentType: SearchResult.config.contentType,
                    async: SearchResult.config.async,
                    cache: SearchResult.config.cache,
                    url: SearchResult.config.url,
                    data: SearchResult.config.data,
                    dataType: SearchResult.config.dataType,
                    success: SearchResult.ajaxSuccess,
                    error: SearchResult.ajaxFailure
                });
            },
            BindPagination: function () {
                $('#divSageSearchResult').pajinate({
                    items_per_page: p.ViewPerPage
                });
            },
            GetSearchResult: function (offset, limit, current) {
                currentPage = current;
                var url = window.location.href;
                searchWord = url.substring(url.indexOf('searchword=') + 11);
                SearchResult.config.method = "GetSearchResult";
                SearchResult.config.url = SearchResult.config.baseURL + SearchResult.config.method;
                SearchResult.config.data = JSON2.stringify({ offset: offset, limit: limit, Searchword: searchWord, SearchBy: p.UserName, CultureName: p.CulturalName, IsUseFriendlyUrls: true, PortalID: p.PortalID });
                SearchResult.config.ajaxCallMode = 1;
                SearchResult.ajaxCall(SearchResult.config);
            },
            BindSearchResult: function (data) {
                arrItemListType.length = 0;
                if (data.d.length > 0) {
                    $('#ulSearchResult').html('');
                    var html = "";
                    var searchWord = '';
                    $.each(data.d, function (index, value) {
                        rowTotal = value.RowTotal;
                        arrItemListType.push(value.ItemID);
                        var htmlContent = "";
                        var Content = "";
                        searchWord = value.SearchWord;
                        Content = value.HTMLContent;
                        var arr = searchWord.split(' ');
                        $.each(arr, function (index, item) {
                            var text = new RegExp('(' + item + ')', 'gi');
                            Content = Content.replace(text, '<strong>$1</strong>');
                        });
                        var pageName = SageFrameHostURL + "/" + value.PageName + SagePageExtension;
                        html += '<li id="liSearchResult" class="sfSearchList"><a href="' + value.URL + '">' + value.PageName + '</a>';
                        html += '<p id="htmContent" class="sfResultDetail">' + Content + '</p>';
                        html += '<p class="sfResultDate">' + value.UpdatedContentOn + '</p>';
                        html += '</li>';
                    });
                    $("#ulSearchResult").append(html);
                    var result = '';
                    $('#h2SearchResult').html('');
                    result = '<span class="sfSearchWord">' + rowTotal + ' results found for  "' + searchWord + '"</span>';
                    $('#h2SearchResult').append(result);
                    if (rowTotal <= p.ViewPerPage) {
                        $('#Pagination').hide();
                    }
                    if (arrItemListType.length > 0) {
                        $("#Pagination").pagination(rowTotal, {
                            items_per_page: p.ViewPerPage,
                            current_page: currentPage,
                            callfunction: true,
                            function_name: { name: SearchResult.GetSearchResult, limit: p.ViewPerPage },
                            prev_text: ".",
                            next_text: ".",
                            prev_show_always: false,
                            next_show_always: false
                        });
                    }
                } else {
                    $("#divSageSearchResult").html('');
                    var msg = '<span class="cssClassNotFound"><b>No Result Found</b></span>';
                    $("#divSageSearchResult").append(msg);
                }
            }
        };
        SearchResult.init();
    };
    $.fn.SageSearchResult = function (p) {
        $.createSearchResult(p);
    };
})(jQuery);