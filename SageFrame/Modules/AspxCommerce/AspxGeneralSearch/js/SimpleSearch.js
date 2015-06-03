(function ($) {
    $.SimpleSearchView = function (p) {
        p = $.extend({
            ShowCategoryForSearch: "",
            ShowSearchKeyWords: "",
            ResultPage: "",
            AdvanceSearchPageName: ""
        }, p);

        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        };
        var isGiftCard = false;
        var simpleSearch = {
            PassSimpleSearchTerm: function () {
                var categoryId;
                if (p.ShowCategoryForSearch.toLowerCase() == 'true') {
                    categoryId = $("#txtSelectedCategory").val();
                } else {
                    categoryId = 0;
                }
                var searchText = $.trim($("#txtSimpleSearchText").val());
                if (categoryId == "0") {
                    categoryId = 0;
                }
                if (searchText == getLocale(AspxGeneralSearch, "What are you shopping today?")) {
                    searchText = "";
                }
                if (searchText != "") {
                    var currentUrl = window.location.href;
                    currentUrl = currentUrl.toLowerCase();
                    p.ResultPage = p.ResultPage.toLowerCase();
                    if (p.ResultPage == "show-details-page") {
                        if (typeof (ItemList) != "undefined") {
                            ItemList.IsInSamePage = true;
                            ItemList.RowTotal = 0;
                            ItemList.BindSimpleSearchResultItems(1, $("#ddlSimpleSearchPageSize").val(), 0, $("#ddlSimpleSortBy option:selected").val(), 0);
                        } else {
                            window.location.href = aspxRedirectPath + "search/simplesearch" + pageExtension + "?cid=" + categoryId + "&isgiftcard=" + isGiftCard + "&q=" + searchText;

                        }
                    } else {
                        window.location.href = aspxRedirectPath + "search/simplesearch" + pageExtension + "?cid=" + categoryId + "&isgiftcard=" + isGiftCard + "&q=" + searchText;
                    }
                }
                return false;
            },
            init: function (config) {
                $('#txtSimpleSearchText').val('');
                if (p.ShowCategoryForSearch.toLowerCase() == 'true') {
                    $("#sfFrontCategory").show();
                }
                $("#lblGeneralSearch").click(function () {
                    $(".cssSearchContainer").slideToggle();
                });
                $('.cssClassSageSearchWrapper').outside('click', function () {
                    $('.cssSearchContainer').stop(true, true).slideUp('slow');
                });
                $('#txtSimpleSearchText').autocomplete({
                    source: function (request, response) {
                        var searchTerm = $.trim($('#txtSimpleSearchText').val());
                        $.ajax({
                            url: aspxservicePath + "AspxCommonHandler.ashx/GetSearchedTermList",
                            data: JSON2.stringify({ search: searchTerm, aspxCommonObj: aspxCommonObj() }),
                            dataType: "json",
                            async: false,
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item.SearchTerm
                                    };
                                }));
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    minLength: 2
                });
                $("#btnSimpleSearch").bind("click", function () {
                    simpleSearch.PassSimpleSearchTerm();
                });

                $("#sfSimpleSearchCategory").on("change", function () {
                    $("#txtSelectedCategory").val($(this).val());
                });

                $(".cssClassSageSearchBox").each(function () {
                    if ($(this).val() == "") {
                        $(this).addClass("lightText").val(getLocale(AspxGeneralSearch, "What are you shopping today?"));
                    }
                });
                $(".cssClassSageSearchBox").bind("focus", function () {
                    if ($(this).val() == getLocale(AspxGeneralSearch, "What are you shopping today?")) {
                        $(this).removeClass("lightText").val("");
                    }
                });
                $(".cssClassSageSearchBox").bind("blur", function () {
                    if ($(this).val() == "") {
                        $(this).val("What are you shopping today?").addClass("lightText");
                    }
                });
                $("#txtSimpleSearchText").bind("focus", function () {
                    $("#txtSimpleSearchText").val("");
                });
                $("#txtSimpleSearchText").keyup(function (event) {
                    if (event.keyCode == 13) {
                        $("#btnSimpleSearch").click();
                    }
                });
                $(".cssClassSageSearchBox").bind("focusout", function () {
                    if ($(this).val() == "") {
                        $(this).val(getLocale(AspxGeneralSearch, "What are you shopping today?")).addClass("lightText");
                    }
                });

                $("#lnkAdvanceSearch").click(function () {
                    window.location.href = aspxRedirectPath + p.AdvanceSearchPageName + pageExtension;
                });
            }
        };
        simpleSearch.init();
    };
    $.fn.SimpleSearchInit = function (p) {
        $.SimpleSearchView(p);
    };
    $.fn.outside = function (ename, cb) {
        return this.each(function () {
            var $this = $(this),
                self = this;

            $(document).bind(ename, function tempo(e) {
                if (e.target !== self && !$.contains(self, e.target)) {
                    cb.apply(self, [e]);
                    if (!self.parentNode) $(document.body).unbind(ename, tempo);
                }
            });
        });
    };
})(jQuery);