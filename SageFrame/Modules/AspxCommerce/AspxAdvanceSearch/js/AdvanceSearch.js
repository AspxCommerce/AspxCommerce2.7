(function ($) {
    $.AdvanceSearchView = function (p) {

        p = $.extend({
            NoImageAdSearchPathSetting: "",
            AllowAddToCart: "",
            AllowOutStockPurchaseSetting: "",
            NoOfItemsInRow: "",
            Displaymode: "",
            AdvanceSearchModulePath: ""
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

        IndexOfArray = function (arr, val) {
            var arrIndex = -1;
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] == val) {
                    arrIndex = i; break;
                }
            }
            return arrIndex;
        };
        function AddUpdateAdvanceSearchTerm() {
            var searchTerm = $.trim($("#txtSearchFor").val());
            if (searchTerm == "") {
                return false;
            }
            if (searchTerm == "What are you shopping today?") {
                searchTerm = "";
                return false;
            }
            $.ajax({
                type: "POST",
                url: p.AdvanceSearchModulePath + "AdvanceSearchHandler.ashx/AddUpdateSearchTerm",
                data: JSON2.stringify({ hasData: hasData, searchTerm: searchTerm, aspxCommonObj: aspxCommonObj() }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function () {
                }
            });
        }

        var currentPage = 0;
        var templateScriptArr = [];
        var hasData = false;
        var NewItemArray = [];
        var arry = [];
        var rowTotal = 0;

        var AdvanceSearch = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: p.AdvanceSearchModulePath + "AdvanceSearchHandler.ashx/",
                url: "",
                method: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: AdvanceSearch.config.type,
                    contentType: AdvanceSearch.config.contentType,
                    cache: AdvanceSearch.config.cache,
                    async: AdvanceSearch.config.async,
                    data: AdvanceSearch.config.data,
                    dataType: AdvanceSearch.config.dataType,
                    url: AdvanceSearch.config.url,
                    success: AdvanceSearch.config.ajaxCallMode,
                    error: AdvanceSearch.ajaxFailure
                });
            },
            AdvanceSearchHideAll: function () {
                $("#divItemViewOptions").hide();
                $("#divSearchPageNumber").hide();
                $("#divShowAdvanceSearchResult").hide();
            },
            GetAllBrandForItem: function (categoryID, isGiftCard) {
                this.config.url = this.config.baseURL + "GetAllBrandForSearchByCategoryID";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), CategoryID: categoryID, IsGiftCard: isGiftCard });
                this.config.ajaxCallMode = AdvanceSearch.BindAllBrandForItem;
                this.ajaxCall(this.config);
            },
            GetShoppingFilter: function (categoryID, isGiftCard) {
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), CategoryID: categoryID, IsGiftCard: isGiftCard });
                this.config.method = "GetDynamicAttributesForAdvanceSearch";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = AdvanceSearch.BindShoppingFilter;
                this.config.async = false;
                this.ajaxCall(this.config);
            },
            ShowSearchResult: function (offset, limit, currentpage1, sortBy) {
                currentPage = currentpage1;
                var isGiftCard = false;
                var categoryId = $("#ddlCategory").val();
                if ($("#ddlCategory option:selected").attr("isgiftcard") != undefined)
                    isGiftCard = $("#ddlCategory option:selected").attr("isgiftcard");
                var brandId = $("#lstBrands").val();
                var priceFrom = $.trim($("#txtPriceFrom").val());
                var priceTo = $.trim($("#txtPriceTo").val());
                var searchText = $.trim($("#txtSearchFor").val());
                if (priceFrom != "0.00" && priceTo != "0.00") {
                    searchText = $.trim($("#txtSearchFor").val());
                } else {
                    if (searchText == getLocale(AdvanceSearchLang, "What are you shopping today?")) {
                        searchText = "";
                    }
                }
                var attributeIds = NewItemArray.join(',');
                if (categoryId == "0") {
                    categoryId = null;
                }
                if (searchText == getLocale(AdvanceSearchLang, "What are you shopping today?")) {
                    searchText = "";
                }             
                if (priceTo != "") {
                    if (!/^[0-9]\d*(\.\d+)?$/.test(priceTo)) {
                        $("#errmsgPriceTo").html(getLocale(AdvanceSearchLang, "Valid Digits And Decimal Only")).css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }
                if (priceFrom != "") {
                    if (!/^[0-9]\d*(\.\d+)?$/.test(priceFrom)) {
                        $("#errmsgPriceFrom").html(getLocale(AdvanceSearchLang, "Valid Digits And Decimal Only")).css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }            
                if (priceFrom == "" && priceTo == "") {
                    priceFrom = null;
                    priceTo = null;
                }
                else if (priceFrom == "" || priceTo == "") {
                    return false;
                }
                else if (parseInt(priceTo, 10) < parseInt(priceFrom, 10)) {
                    csscody.alert('<h2>' + getLocale(AdvanceSearchLang, 'Information Alert') + '</h2><p>' + getLocale(AdvanceSearchLang, 'To Price must be greater than From Price') + '</p>');
                    return false;
                }
                if (priceFrom != null && priceTo != null)
                {
                    var cookieCurrency = Currency.cookie.read();
                    var currentCurrencyRate = 0.00;
                    var rate = $.parseJSON(currencyRate);
                    $.each(rate, function (index, item) {
                        if (cookieCurrency == item.CurrencyCode) {
                            currentCurrencyRate = item.CurrencyRate;
                        }
                                           });

                    if (cookieCurrency == BaseCurrency) {
                        priceFrom = parseFloat(priceFrom).toFixed(2);
                        priceTo = parseFloat(priceTo).toFixed(2);
                    } else {
                        priceFrom = parseFloat(priceFrom / currentCurrencyRate).toFixed(2);
                        priceTo = parseFloat(priceTo / currentCurrencyRate).toFixed(2);
                    }
                }

                var itemByDynamicSearchObj = {
                    CategoryID: categoryId,
                    IsGiftCard: isGiftCard,
                    SearchText: searchText,
                    BrandID: brandId,
                    PriceFrom: priceFrom,
                    PriceTo: priceTo,
                    AttributeIDs: attributeIds,
                    RowTotal: rowTotal,
                    SortBy: sortBy
                };

                var params = JSON2.stringify({
                    offset: offset,
                    limit: limit,
                    aspxCommonObj: aspxCommonObj(),
                    searchObj: itemByDynamicSearchObj
                });
                AdvanceSearch.config.url = AdvanceSearch.config.baseURL + "GetItemsByDyanamicAdvanceSearch";
                AdvanceSearch.config.data = params;
                AdvanceSearch.config.ajaxCallMode = AdvanceSearch.BindItemsByDyanamicAdvanceSearch;
                AdvanceSearch.ajaxCall(AdvanceSearch.config);
            },
            BindAllBrandForItem: function (msg) {               
                $('#lstBrands').html('');
                $('#lstBrands').append("<option value='0'>" + getLocale(AdvanceSearchLang, "All Brands") + "</option>");
                $.each(msg.d, function (index, item) {
                    $('#lstBrands').append("<option value='" + item.BrandID + "'>" + item.BrandName + "</option>");

                });
                $("#lstBrands").val('0');
            },
            BindShoppingFilter: function (msg) {
                var attrID = [];
                var attrValue = [];
                var attrName = '';
                var elem = '';
                var value;
                var length = msg.d.length;
                if (length > 0) {
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (IndexOfArray(attrID, value.AttributeID) == -1) {
                            elem = '<div value=' + value.AttributeID + ' class="cssDyanamicAttributes"><label Class="cssClassLabel">' + value.AttributeName + ': </label><ul></ul></div>';
                            attrValue = [];
                            attrID.push(value.AttributeID);
                            $(".sfAdvanceSearch .cssDynAttrWrapper").append(elem);
                            elem = '';
                            elem = '<li><input class= "chkFilter" type="checkbox" name="' + value.AttributeValue + '" inputTypeId="' + value.InputTypeID + '" value="' + value.AttributeID + '"/> ' + value.AttributeValue + '</li>';
                            $(".sfAdvanceSearch .cssDynAttrWrapper").find('div[value=' + value.AttributeID + ']').find('ul').append(elem);
                            attrValue.push(value.AttributeValue);
                        } else {
                            if (IndexOfArray(attrValue, value.AttributeValue) == -1) {
                                itemCount = 1;
                                elem = '';
                                elem = '<li><input class="chkFilter" type="checkbox" name="' + value.AttributeValue + '" inputTypeId="' + value.InputTypeID + '" value="' + value.AttributeID + '"/> ' + value.AttributeValue + '</li>';
                                $(".sfAdvanceSearch .cssDynAttrWrapper").find('div[value=' + value.AttributeID + ']').find('ul').append(elem);
                                attrValue.push(value.AttributeValue);
                            }
                        }
                    };
                    $(".cssDynAttrWrapper").show();
                }
            },
            BindItemsByDyanamicAdvanceSearch: function (msg) {
                var length = msg.d.length;
                if (length > 0) {
                    hasData = true;
                    rowTotal = msg.d[0].RowTotal;
                } else {
                    hasData = false;
                }
                BindTemplateDetails('divShowAdvanceSearchResult', 'divItemViewOptions', 'divViewAs', 'ddlAdvanceSearchViewAs', 'ddlAdvanceSearchSortBy', 'divSearchPageNumber', 'Pagination', 'ddlPageSize2', currentPage, msg, AdvanceSearch.ShowSearchResult, 'AdvanceSearch', p.AllowAddToCart, p.AllowOutStockPurchaseSetting,
                      p.NoImageAdSearchPathSetting, p.NoOfItemsInRow, p.Displaymode, templateScriptArr);
                AddUpdateAdvanceSearchTerm();
            },
            Init: function () {
                $.each(jsTemplateArray, function (index, value) {                  
                    var tempVal = jsTemplateArray[index].split('@');
                    var templateScript = {
                        TemplateKey: tempVal[0],
                        TemplateValue: tempVal[1]
                    };
                    templateScriptArr.push(templateScript);
                });
                
                $("#txtPriceFrom").bind("keypress", function (e) {
                    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                        if (e.which != 13) {
                            $("#errmsgPriceFrom").html(getLocale(AdvanceSearchLang, "Valid Digits And Decimal Only")).css("color", "red").show().fadeOut(1600);
                            return false;
                        }
                    }
                });
                $("#txtPriceFrom,#txtPriceTo").bind('paste', function (e) {
                    e.preventDefault();
                });
                $("#txtPriceFrom,#txtPriceTo").bind('contextmenu', function (e) {
                    e.preventDefault();
                });
                $("#txtPriceTo").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                        if (e.which != 13) {
                            $("#errmsgPriceTo").html(getLocale(AdvanceSearchLang, "Valid Digits And Decimal Only")).css("color", "red").show().fadeOut(1600);
                            return false;
                        }
                    }
                });
                AdvanceSearch.AdvanceSearchHideAll();
                $("#btnAdvanceSearch").click(function () {
                    rowTotal = 0;
                    var offset = 1;
                    var limit = $("#ddlPageSize2").val();
                    AdvanceSearch.ShowSearchResult(offset, limit, 0, $("#ddlAdvanceSearchSortBy option:selected").val());
                    return false;
                });
                $('#txtSearchFor').autocomplete({
                    source: function (request, response) {
                        var searchTerm = $.trim($('#txtSearchFor').val());
                        var aspxCommonInfo = aspxCommonObj();
                        aspxCommonInfo.CultureName = null;
                        aspxCommonInfo.UserName = null;
                        if (searchTerm != '') {                            
                            $.ajax({
                                url: AdvanceSearch.config.baseURL + "GetSearchedTermList",
                                data: JSON2.stringify({ search: searchTerm, aspxCommonObj: aspxCommonInfo }),
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataFilter: function (data) { return data; },
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            value: item.SearchTerm
                                        }
                                    }))
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert(textStatus);
                                }
                            });
                        }
                    },
                    minLength: 2
                });
                $(".searchForTextBox").each(function () {
                    if ($(this).val() == "") {
                        $(this).addClass("lightText").val(getLocale(AdvanceSearchLang, "What are you shopping today?"));
                    }
                });
                $(".searchForTextBox").bind("focus", function () {
                    if ($(this).val() == getLocale(AdvanceSearchLang, "What are you shopping today?")) {
                        $(this).removeClass("lightText").val("");
                    }
                                   });
                $(".searchForTextBox").bind("blur", function () {
                    if ($(this).val() == "") {
                        $(this).val(getLocale(AdvanceSearchLang, "What are you shopping today?")).addClass("lightText");
                    }

                });
                $(".searchForTextBox").keyup(function (event) {
                    if ($(this).val() != getLocale(AdvanceSearchLang, "What are you shopping today?")) {
                        $("#txtSearchFor").next('span').remove();
                    }
                });

                $("#txtSearchFor,#txtPriceTo,#txtPriceFrom").keyup(function (event) {
                    if (event.keyCode == 13) {
                        $("#btnAdvanceSearch").click();
                    }
                });
                $(".sfAdvanceSearch").on('click', ".chkFilter", function () {
                    var isChecked = false;
                    $('.chkFilter').each(function () {
                        if ($(this).prop('checked') == true) {
                            isChecked = true;
                            return false;
                        }
                    });
                    var attrValue = $(this).prop('name');
                    var attrIds = $(this).val();
                    var inputTypeId = $(this).attr('inputTypeID');
                    var values = attrIds + '@' + inputTypeId + '@' + attrValue;
                    if ($(this).prop('checked') == true) {
                        if (IndexOfArray(arry, values) == -1) {
                            arry.push(values);
                        }
                    }
                    if ($(this).prop('checked') == false) {
                        if (IndexOfArray(arry, values) != -1) {
                            arry.splice(IndexOfArray(arry, values), 1);
                        }
                        if (IndexOfArray(arry, values) == -1) {
                            $(this).parents('ul').find(".chkFilter").each(function () {
                                if ($(this).prop('checked') == true) {
                                    attrIds + '@' + inputTypeId + '@' + attrValue;
                                    var chkval = attrIds + '@' + $(this).attr('inputTypeID') + '@' + $(this).val();
                                    if (chkval == values) {
                                        if (IndexOfArray(arry, values) == -1) {
                                            arry.push(values);
                                        }
                                    }
                                }
                            });
                        }
                    }
                    var aux = {};
                    $.each(arry, function (idx, val) {
                        var key = [];
                        key[0] = val.substring(0, val.lastIndexOf('@'));
                        key[1] = val.substring(val.lastIndexOf('@') + 1, val.length);
                        if (!aux[key[0]]) {
                            aux[key[0]] = [];
                        }
                        aux[key[0]].push(key[1]);
                    });
                    NewItemArray = [];
                    $.each(aux, function (idx, val) {
                        NewItemArray.push(idx + '@' + val.join("#"));
                    });
                    return false;
                });
            }
        };
        AdvanceSearch.Init();
    };
    $.fn.AdvanceSearchInit = function (p) {  
        $.AdvanceSearchView(p);
    };
})(jQuery); 