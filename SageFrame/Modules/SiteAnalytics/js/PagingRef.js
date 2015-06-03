
(function ($) {
    var tempPageNo = 0;
    var chooseCase = 0;
    $.Page4 = function (Setting) {
        var Options = $.extend({
            Type: 'Table',
            divID: 'GenaralizedPaging',
            PagingID: 'divPaging',
            Columns: ['CodeID', 'Tittle'],
            CountUrl: 'GetCount',
            CountData: {},
            DataUrl: 'GetSnippetList',
            DataParams: { pageNo: 1, range: 5 },
            PageNo: 7,
            Myheader: '',
            Wrapper: 'Li',
            Extra: '',
            AutoWrap: true,
            Format: ['<div><div>', 'CodeID', '</div><div>', 'Tittle', '</div></div>'],
            OnComplete3: function (data) {
            }
        }, Setting);
        var pagingID = Options.PagingID;
        var divID = Options.divID;
        pa4 = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: SageFrameAppPath + "/Modules/SiteAnalytics/SiteAnalyticWebService.asmx/",
                method: "",
                url: "",
                ajaxCallMode: 0
            },
            ajaxSuccess: function (msg) {
                switch (pa4.config.ajaxCallMode) {
                    case 2:
                        var TotalRow = 0;
                        $.each(msg.d, function (index, value) {
                            if (index == 0) {
                                TotalRow = value.Count;
                                pa4.Paging(TotalRow, Options.DataParams.range);
                            }
                        });
                        if (Options.AutoWrap == true) {
                            if (Options.Type.toLowerCase() == 'table') {
                                if (Options.Columns.length > 0) {
                                    var Table = '<table>';
                                    var trh = '<tr>';
                                    var th = '';
                                    $.each(Options.Columns, function (index, value) {
                                        th += '<td>' + value + '</td>';
                                    });
                                    trh += th;
                                    trh += '</tr>';
                                    $.each(msg.d, function (index, data) {
                                        var tr = '';
                                        tr += '<tr>';
                                        var td = '';
                                        $.each(Options.Columns, function (count, value) {
                                            $.each(data, function (dataindex, datavalue) {
                                                if (dataindex == value) {
                                                    td += '<td>' + datavalue + '</td>';
                                                }
                                            });
                                        });
                                        tr += td;
                                        tr += '</tr>';
                                        trh += tr;
                                    });
                                    Table += trh;
                                    $('#' + Options.divID).html(Table);
                                }
                            }
                            else {
                                var div = '';
                                div += Options.Myheader;
                                $.each(msg.d, function (index, data) {
                                    div += '<' + Options.Wrapper + '>';
                                    $.each(Options.Format, function (count, value) {
                                        var test = 0;
                                        $.each(data, function (dataindex, datavalue) {
                                            if (dataindex == value) {
                                                div += datavalue;
                                                test = 1;
                                                return false;
                                            }
                                        });
                                        if (test == 0) {
                                            div += value;
                                        }
                                    });
                                    div += '</' + Options.Wrapper + '>';
                                });
                                $('#tblPost').html(div);
                                $('#tblPost').append(Options.Extra);
                                $('#tblPost tr').each(function (index, value) {
                                    var styleClass = index % 2 == 0 ? "sfOdd" : "sfEven";
                                    $(this).addClass(styleClass);
                                });
                            }
                        }
                        else {

                            Options.OnComplete3(msg);
                        }
                        break;
                }
            },
            ajaxFailure: function (data) {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: pa4.config.type,
                    contentType: pa4.config.contentType,
                    cache: pa4.config.cache,
                    async: pa4.config.async,
                    url: pa4.config.url,
                    data: pa4.config.data,
                    dataType: pa4.config.dataType,
                    beforeSend: function () {
                        // $('#divAjaxLoading').show();
                    },
                    complete: function () {
                        //$('#divAjaxLoading').hide();
                    },
                    success: function (msg) {
                        pa4.ajaxSuccess(msg);
                    },
                    error: function (msg) {
                        pa4.ajaxFailure(msg);
                    }
                });
            },
            GetList: function (click) {
                Options.DataParams.pageNo = tempPageNo;
                pa4.config.ajaxCallMode = 2;
                pa4.config.method = Options.DataUrl;
                pa4.config.url = pa4.config.baseURL + pa4.config.method;
                pa4.config.data = JSON2.stringify(Options.DataParams);
                pa4.ajaxCall(pa4.config);
                switch (click) {
                    case 'First':
                        $('.Paging' + pagingID).each(function (index, value) {
                            if ($(this).hasClass('click')) {
                                PageNo = $(this).text();
                            }
                        });
                        break;
                    case 'last':
                        $('.Paging' + pagingID).removeClass('click');
                        $('.Paging' + pagingID).last().addClass('click');
                        PageNo = $('.Paging' + pagingID).last().text();
                        break;
                }
            },
            Paging: function (TotalRow, Range) {
                var number_Of_Page = 0;
                var remainder = TotalRow % Range;
                if (remainder > 0)
                    number_Of_Page = (TotalRow - remainder) / Range + 1;
                else
                    number_Of_Page = TotalRow / Range;
                var noOfPage = parseInt(number_Of_Page);
                var atOnce = 8;
                var limitPerShow = 15;
                pa4.PageList(chooseCase, noOfPage, atOnce, limitPerShow, tempPageNo);
            },
            PageList: function (CurrentCase, noOfPage, atOnce, limitPerShow, CurrentValue) {
                var p = '';
                if (noOfPage < limitPerShow) {
                    if (noOfPage < atOnce) {
                        for (var i = 0; i < noOfPage; i++) {
                            p += '<span class="Paging' + pagingID + '"> ' + (i + 1) + ' </span>';
                        }
                    }
                    else {
                        p += '<span id = "previous3"> < </span>';
                        for (var i = 0; i < atOnce; i++) {
                            p += '<span class="Paging' + pagingID + '"> ' + (i + 1) + ' </span>';
                        }
                        p += '<span id = "next3"> > </span>';
                    }
                }
                else {
                    p += '<span id = "previous3"> < </span>';
                    switch (CurrentCase) {
                        case 0:
                            for (var i = 0; i < atOnce; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + (i + 1) + ' </span>';
                            }
                            p += '<span class="gap">...</span>';
                            for (var i = 0; i < 2; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + (noOfPage - 1 + i) + ' </span>';
                            }
                            break;
                        case 1:
                            for (var i = 0; i < 2; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + (i + 1) + ' </span>';
                            }
                            p += '<span class="gap">...</span>';
                            for (var i = 0; i < atOnce; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + ((noOfPage - atOnce) + 1 + i) + ' </span>';
                            }
                            break;
                        default:
                            for (var i = 0; i < 2; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + (i + 1) + ' </span>';
                            }
                            p += '<span class="gap">...</span>';
                            var start = atOnce - 4;
                            var limit = noOfPage - start;
                            for (var i = start; i < limit; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + i + ' </span>';
                            }
                            p += '<span class="gap">...</span>';
                            for (var i = 0; i < 2; i++) {
                                p += '<span class="Paging' + pagingID + '"> ' + (noOfPage - 1 + i) + ' </span>';
                            }
                            break;
                    }
                    p += '<span id = "next3"> > </span>';
                }
                $('#' + Options.PagingID).html(p);
                $('#next3').off().on('click', function () {
                    var tempval;
                    var me;
                    $('.Paging' + pagingID).each(function (index, value) {
                        if ($(this).hasClass('click')) {
                            me = $(this);
                            tempval = parseInt(me.text());
                        }
                    });
                    tempPageNo = tempval + 1;
                    if (me.next().hasClass('gap')) {
                        if (CurrentCase == 0) {
                            if (tempval == atOnce) {
                                chooseCase = tempval;
                                pa4.GetList('First');
                            }
                        }
                        else if (CurrentCase == 1) { }
                        else {
                            if ((noOfPage - atOnce) < tempval) {
                                chooseCase = 1;
                                pa4.GetList('First');
                            }
                            else {
                                $('.Paging' + pagingID).each(function (index, value) {
                                    CurrentCase = atOnce + 1;
                                    if (index == 0 || index == 1 || index == (noOfPage - 2) || index == (noOfPage - 2)) {
                                    }
                                    else {
                                        $(this).text(parseInt($(this).text()) + 1);
                                    }
                                });
                            }
                        }
                    }
                    else {
                        if (isNaN(me.next().text()) == false) {
                            me.next().addClass('click');
                            me.removeClass('click');
                            pa4.GetList('First');
                        }
                    }
                });
                $('#previous3').off().on('click', function () {
                    var tempval;
                    var me;
                    $('.Paging' + pagingID).each(function (index, value) {
                        if ($(this).hasClass('click')) {
                            me = $(this);
                            tempval = parseInt(me.text());
                        }
                    });
                    tempPageNo = tempval - 1;
                    if (me.prev().hasClass('gap')) {
                        if (CurrentCase == 1) {
                            if (tempval == (noOfPage - atOnce + 1)) {                               
                                chooseCase = tempval;
                                pa4.GetList('First');
                            }
                        }
                        else if (CurrentCase == 0) { }
                        else {
                            if ((atOnce + 1) > tempval) {
                                chooseCase = 0;
                                pa4.GetList('First');
                            }
                            else {
                                $('.Paging' + pagingID).each(function (index, value) {
                                    if (index == 0 || index == 1 || index == (noOfPage - 2) || index == (noOfPage - 2)) {
                                    }
                                    else {
                                        $(this).text(parseInt($(this).text()) - 1);
                                    }
                                });
                            }
                        }
                    }
                    else {
                        if (isNaN(me.prev().text()) == false) {
                            me.prev().addClass('click');
                            me.removeClass('click');
                            pa4.GetList('First');
                        }
                    }
                });
                $('.Paging' + pagingID).off().on('click', function () {
                    if ($(this).attr('class') != "Paging click") {
                        var clickValue = parseInt($(this).text());
                        tempPageNo = clickValue;
                        if (clickValue <= atOnce) {
                            chooseCase = 0;
                            pa4.GetList('First');
                        }
                        else if (clickValue > (noOfPage - atOnce) & clickValue <= noOfPage) {
                            chooseCase = 1;
                            pa4.GetList('First');
                        }
                        else {
                            $('.Paging' + pagingID).removeClass('click');
                            $(this).addClass('click');
                            pa4.GetList('First');
                        }
                        pa4.ClassClick(chooseCase, clickValue);
                    }
                });
                pa4.ClassClick(chooseCase, CurrentValue);
            },
            ClassClick: function (Current, CurrentValue) {
                $('.Paging' + pagingID).removeClass('click');
                if (Current == 0) {
                    if (CurrentValue == 0) {
                        $('.Paging' + pagingID).each(function (index, value) {
                            if (parseInt($(this).text()) == Options.PageNo) {
                                $(this).addClass('click');
                            }
                        });
                    }
                    else {
                        $('.Paging' + pagingID).each(function (index, value) {
                            if (parseInt($(this).text()) == (CurrentValue)) {
                                $(this).addClass('click');
                            }
                        });
                    }
                }
                else if (Current == 1) {
                    $('.Paging' + pagingID).each(function (index, value) {
                        if (parseInt($(this).text()) == CurrentValue) {
                            $(this).addClass('click');
                        }
                    });
                }
                else {
                    $('.Paging' + pagingID).each(function (index, value) {
                        if (parseInt($(this).text()) == CurrentValue) {
                            $(this).addClass('click');
                        }
                    });
                }
            },
            Pages: function (language) {
                return pa4.GetList('First');
            },
            init: function () {
                var PageNo = Options.PageNo;
                tempPageNo = PageNo;
                return pa4.Pages();
            }
        };
        return pa4.init();
    };
    $.fn.PagingRef = function (Setting) {
        return $.Page4(Setting);
    };
} (jQuery));




    
