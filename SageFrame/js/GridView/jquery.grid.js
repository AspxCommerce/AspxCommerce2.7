var SageData;
var gridData = [];
var fromServer = 0;
(function($) {
    $.createGrid = function(t, p) {
        p = $.extend({
            striped: true, //apply odd even stripes
            url: false, //ajax url
            functionMethod: '', // url method
            method: 'POST', // data sending method
            dataType: 'xml', // type of data loaded
            errormsg: 'Connection Error',
            usepager: false, //
            nowrap: true, //
            page: 1, //current page
            total: 1, //total pages
            useRp: true, //use the results per page select box
            current: 1,
            rp: 5, // results per page
            rpOptions: [5, 10, 15, 20, 25, 40], //[8, 16, 32, 64, 128],
            pageOf: '' + getLocale(CoreJsLanguage, "Page") + ' {box} ' + getLocale(CoreJsLanguage, "Of") + ' {count}',
            pnew: 1,
            pageshow: 5,
            title: false,
            nomsg: getLocale(CoreJsLanguage, 'No items'),
            dateformat: 'yyyy-MM-dd',
            onError: true,
            defaultImage: '',
            imageOfType: '',
            runAfterCaseValue: "",
            runAfterCaseArguments: "",
            data: ""
        }, p);

        $(t).show()
        .prop({ cellPadding: 0, cellSpacing: 0 });
        //.removeAttr('width');       
        // Global Varaiables Settings
        var tdalign = new Array();
        var tdtype = new Array();
        var tdformat = new Array();
        var primaryID = new Array();
        var desablesort = new Array();

        //updated onblur July 20 2011, July 26 2011
        var tdvalue = new Array();
        var tdRandomValue = new Array();
        var DownloadArguments = new Array();
        var DownloadMethod = new Array();

        // Update On 12 Dec 2010
        var tdHide = new Array();
        var tdcheckfor = new Array();
        var tdcheckedItems = new Array();

        var ElemClass = new Array();
        var ElemDefault = new Array();
        var ControlClass = new Array();

        //Updated On 29 May 2011
        var tdURL = new Array();
        var QueryPairs = new Array();

        var ShowPopUp = new Array();
        var PopUpID = new Array();

        var PopUpArguments = new Array();
        var PopUpMethod = new Array();

        var BtnTitle = new Array();

        var AltText = new String();


        var setFlag = false;

        SageData = function() {
            var getGridArray = function(id) {
                for (var z = 0; z < gridData.length; z++) {
                    if (gridData[z].id == id) {
                        return gridData[z];
                    }
                }
            };
            var checkExist = function(id) {
                var isExist = false;
                for (var z = 0; z < gridData.length; z++) {

                    if (gridData[z].id == id) {
                        isExist = true;
                        break;
                    }
                }
                return isExist;
            };
            var checkArrItemExist = function(id, tableGridID) {
                var isExist = false;
                var gridArrData = SageData.Get(tableGridID);
                if (gridArrData != undefined) {
                    for (var z = 0; z < gridArrData.Arr.length; z++) {
                        if (gridArrData.Arr[z] == id) {
                            isExist = true;
                            break;
                        }
                    }
                }
                return isExist;
            }
            var checkDelArrItemExist = function(id, tableGridID) {
                var isExist = false;
                var gridArrData = SageData.Get(tableGridID);
                if (gridArrData != undefined) {
                    for (var z = 0; z < gridArrData.DelArr.length; z++) {
                        if (gridArrData.DelArr[z] == id) {
                            isExist = true;
                            break;
                        }
                    }
                }
                return isExist;
            }
            var getIndex = function(id) {
                var index = 0;
                for (var z = 0; z < gridData.length; z++) {

                    if (gridData[z].id == id) {
                        index = z;
                        break;
                    }
                }
                return index;
            };

            var getArrIndex = function(id, tableGridID) {
                var index = 0;
                var gridArrData = SageData.Get(tableGridID);
                var index = SageData.getIndex(tableGridID);
                if (gridArrData != undefined) {
                    for (var z = 0; z < gridArrData.Arr.length; z++) {
                        if (gridArrData.Arr[z] == id) {
                            index = z;
                            break;
                        }
                    }
                }
                return index;
            }
            var getDelArrIndex = function(id, tableGridID) {
                var index = 0;
                var gridArrData = SageData.Get(tableGridID);
                var index = SageData.getIndex(tableGridID);
                if (gridArrData != undefined) {
                    for (var z = 0; z < gridArrData.DelArr.length; z++) {
                        if (gridArrData.DelArr[z] == id) {
                            index = z;
                            break;
                        }
                    }
                }
                return index;
            }
            var pushArr = function(index, data) {
                gridData[index].Arr.push(data);
            };
            var delArrPush = function(index, data) {
                gridData[index].DelArr.push(data);
            };

            var deleteArr = function(tableID, data) {
                var arrIndex = SageData.getArrIndex(data, tableID);
                var index = SageData.getIndex(tableID);
                gridData[index].Arr.splice(arrIndex, 1);
            };
            var delArrDel = function(tableID, data) {
                var arrIndex = SageData.getDelArrIndex(data, tableID);
                var index = SageData.getIndex(tableID);
                gridData[index].DelArr.splice(arrIndex, 1);
            };
            return { Get: getGridArray,
                checkExist: checkExist,
                getIndex: getIndex,
                pushArr: pushArr,
                deleteArr: deleteArr,
                checkArrItemExist: checkArrItemExist,
                delArrPush: delArrPush,
                delArrDel: delArrDel,
                checkDelArrItemExist: checkDelArrItemExist,
                getArrIndex: getArrIndex,
                getDelArrIndex: getDelArrIndex
            }
        } ();

        var gridInfos = {
            id: t.id,
            Arr: [],
            DelArr: []
        };
        if (!SageData.checkExist(t.id)) {
            gridData.push(gridInfos);
        }
        var g = {
            wcf: function() {
                //var param = { offset: p.pnew, limit: p.rp, portalId: 1, userName: 'RAJA' };
                
                if (fromServer !== 0 || p.data == "") {
                    var params = $.extend({ offset: p.pnew, limit: p.rp }, p.param);

                    var mydata = JSON2.stringify(params);

                    $(document).ready(function() {
                        $("#" + t.id).prev().prev(".loading").find('img').prop('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/ajax-loader.gif');
                        $("#" + t.id).prev().prev(".loading").show();
                        $.ajax({
                            type: "POST",
                            url: p.url + p.functionMethod,
                            data: mydata,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            //cache: false,
                            success: function (data) {
                                g.clearAll();
                                g.addHeader();
                                if (data.d.length == 0) {
                                    g.noDataMsg();
                                } else {
                                    g.addData(data);
                                    g.addRowProp();
                                    g.addActions();
                                    g.addControls();
                                    g.newPagination();

                                    $(t).tablesorter({
                                        headers: p.sortcol,
                                        widgets: ['zebra']
                                    });

                                    //$(t).update();
                                    for (j = 0; j < p.colModel.length; j++) {
                                        var cm = p.colModel[j];
                                        if (cm.coltype == 'checkbox') {
                                            if (cm.controlclass != undefined) {
                                                var controlClass = cm.controlclass;
                                            }

                                            if (cm.elemClass != undefined) {
                                                var elementclass = cm.elemClass;
                                            }
                                            $('.' + elementclass).bind('click', function() {
                                                g.MakeHeaderCheck(elementclass, controlClass);
                                            });
                                        }
                                    }
                                }
                                $(t).show();
                            },
                            complete: function() {
                                $("#" + t.id).prev().prev(".loading").hide();
                            
                                if (p.runAfterCaseValue != "") {
                                    g.runAfterCase(p.runAfterCaseValue);

                                    window[p.runAfterCaseValue](p.runAfterCaseArguments);
                                }
                            
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                $("#" + t.id).prev().prev(".loading").hide();
                                if (p.onError) {
                                    g.clearAll();
                                    //alert(xhr.status);
                                    //alert(thrownError);
                                    $(this).prev("div.log").text(xhr.responseText);
                                }
                            }
                        });
                    });
                }
                else {
                    var data = JSON.parse('{"d":' + p.data + '}');
                    g.clearAll();
                    g.addHeader();
                    if (data.length == 0) {
                        g.noDataMsg();
                    } else {
                        g.addData(data);
                        g.addRowProp();
                        g.addActions();
                        g.addControls();
                        g.newPagination();

                        $(t).tablesorter({
                            headers: p.sortcol,
                            widgets: ['zebra']
                        });

                        //$(t).update();
                        for (j = 0; j < p.colModel.length; j++) {
                            var cm = p.colModel[j];
                            if (cm.coltype == 'checkbox') {
                                if (cm.controlclass != undefined) {
                                    var controlClass = cm.controlclass;
                                }

                                if (cm.elemClass != undefined) {
                                    var elementclass = cm.elemClass;
                                }
                                $('.' + elementclass).bind('click', function () {
                                    g.MakeHeaderCheck(elementclass, controlClass);
                                });
                            }
                        }
                    }
                    $(t).show();

                    $("#" + t.id).prev().prev(".loading").hide();

                    if (p.runAfterCaseValue != "") {
                        g.runAfterCase(p.runAfterCaseValue);

                        window[p.runAfterCaseValue](p.runAfterCaseArguments);
                    }
                    fromServer = 1;

                }

                //Updated On May 30 2011
                //                $(document.body).click(function(e) {
                //                    var target = $(e.target).parents('p');
                //                    if (target.attr("class") !== 'Sageshowhide cssClassActionImg')
                //                        $('div.cssClassActionOnClickShow').hide();
                //                });
            },

            MakeHeaderCheck: function (elementclass, controlClass) {               
                var uncheckedflag = true;
                indexArr = SageData.getIndex(t.id);
                var tableGridId = t.id;
                $('.' + elementclass).each(function () {                   
                    var id = $(this).val();
                    if (!$(this).is(':checked')) {
                        uncheckedflag = false;
                        if (!SageData.checkDelArrItemExist(id, tableGridId)) {
                            SageData.delArrPush(indexArr, id);
                        }
                        if (SageData.checkArrItemExist(id, tableGridId)) {
                            SageData.deleteArr(tableGridId, id);
                        }
                    }
                    else {
                        if (!SageData.checkArrItemExist(id, tableGridId)) {
                            SageData.pushArr(indexArr, id)
                        }
                        if (SageData.checkDelArrItemExist(id, tableGridId)) {
                            SageData.delArrDel(tableGridId, id);
                        }
                    }
                    $('.' + controlClass).prop('checked', uncheckedflag);
                });
            },

            noDataMsg: function() {
                // alert(p.colModel.length);
                var tbody = document.createElement('tbody');
                var tr = document.createElement('tr');
                var td = document.createElement('td');
                $(td).prop('colspan', p.colModel.length);
                $(td).html(p.nomsg);
                $(tr).append(td);
                $(tbody).append(tr);
                $(t).append(tbody);

            },

            newPagination: function() {
                var opt = '';
                var PageWrapper = document.createElement('div');
                //var PageNumberMidBg = document.createElement('div');
                //var PageNumberRightBg = document.createElement('div');
                //var PageNumberLeftBg = document.createElement('div');

                //View 1 - 10 of 1 000 000

                var PageOf = document.createElement('div');
                var Pages = document.createElement('div');
                var ViewOf = document.createElement('div');
                $(Pages).addClass("pagination");


                // Call Pagination Class
                sagePaging.Paging(p.total, p.rp); // total,rowperpage
                sagePaging.NavPaging(p.current, p.pageshow);  // currentPage


                $(PageOf).addClass("cssClassPages");
                var txt = p.pageOf.replace(/{box}/, '<input type="text" value="' + p.current + '" readonly="readonly" />');
                txt = txt.replace(/{count}/, sagePaging.maxPage);
                $(PageOf).prepend(txt);

                for (var nx = 0; nx < p.rpOptions.length; nx++) {

                    if (p.rp == p.rpOptions[nx]) sel = 'selected="selected"'; else sel = '';
                    opt += "<option value='" + p.rpOptions[nx] + "' " + sel + " >" + p.rpOptions[nx] + "</option>";
                };
                var offset = p.pnew + parseInt(p.rp) - 1;
                if (offset > p.total) {
                    offset = p.total;
                }
                $(PageWrapper).prepend('<div class="cssClassViewOf">' + getLocale(CoreJsLanguage, "View") + ' ' + p.pnew + '-' + offset + ' ' + getLocale(CoreJsLanguage, 'Of') + ' ' + p.total + ' ' + getLocale(CoreJsLanguage, 'records') + '</div>');
                $(PageWrapper).prepend('<div class="cssClassViewPerPage">' + getLocale(CoreJsLanguage, "View Per Page:") + ' <select name="rp" id="' + t.id + '_pagesize">+"' + opt + '"</select></div>');
                $(PageWrapper).find('select').change(
					function() {
					    p.page = 1;
					    p.rp = this.value;
					    g.clearAll();
					    p.pnew = 1;
					    p.current = 1;
					    g.wcf();
					}
				);

                $(Pages).prepend(sagePaging.nav);
                $(Pages).prependTo(PageWrapper);
                $(PageOf).prependTo(PageWrapper);

                //$(PageNumberMidBg).prependTo(PageNumberRightBg);
                //$(PageNumberRightBg).prependTo(PageNumberLeftBg);
                //$(PageNumberLeftBg).prependTo(PageWrapper);
                //$(PageNumberMidBg).addClass("cssClassPageNumberMidBg");
                //$(PageNumberRightBg).addClass("cssClassPageNumberRightBg");
                //$(PageNumberLeftBg).addClass("cssClassPageNumberLeftBg");

                $(PageWrapper).insertAfter($(t));
                $(PageWrapper).addClass("cssClassPageNumber clearfix");
                $(PageWrapper).prop('id', (t.id + '_Pagination'));

                $(PageOf).find('input').keypress(function(e) {
                    //alert(sagePaging.maxPage);
                    var txtval = $.trim($(this).prop('value'));
                    var code = (e.keyCode ? e.keyCode : e.which);

                    if (code == 13) { //Enter keycode                   
                        if (txtval != '' && (parseInt(txtval) <= parseInt(sagePaging.maxPage)))
                            g.invokePagination(txtval);

                    } else {
                        if (!g.validate(e)) {
                            return false;
                        }
                    }
                });

                $(Pages).find('a').each(function() {
                    $(this).click(function() {
                        g.invokePagination($(this).attr('alt'));
                    });
                });

            },

            invokePagination: function(pageno) {
                p.current = pageno;
                p.pnew = (pageno - 1) * p.rp + 1; //offset                 
                //g.clearAll();
                g.wcf();
            },

            clearAll: function() {
                $(t).find('thead').remove();
                $(t).find('tbody').remove();
                $(t).css('display', 'none');
                var id = $(t).prop('id') + '_Pagination';
                $('#' + id).remove();
                primaryID.length = 0;
                //$('.cssClassPagination').remove();
            },

            addActions: function() {
                if (p.buttons) {
                    $('tbody tr', t).each(function(k) {
                        var actionwrapper = document.createElement('div');
                        var actiontoolswrapper = document.createElement('div');
                        var actiontools = document.createElement('div');
                        var actionsP = document.createElement('p');
                        var showhide = document.createElement('p');
                        var currentIndex = $(this).closest("tr")[0].rowIndex - 1;
                        $(actionwrapper).addClass('cssClassActionOnClick');
                        $(actiontoolswrapper).addClass('cssClassActionOnClickShow');
                        $(actiontools).addClass('cssClassActionOnClickShowInfo');
                        $(showhide).addClass('Sageshowhide');
                        $(showhide).addClass('cssClassActionImg');
                        $(showhide).append('<a href="javascript:;" class="icon-settings">&nbsp;</a>');
                        //$(showhide).append('<a href=#edit' + currentIndex + '>&nbsp;</a>');

                        //$(actiontoolswrapper).css({ width: '200px', height: '200px', border: '1px solid red' })

                        for (i = 0; i < p.buttons.length; i++) {
                            var btn = p.buttons[i];
                            var button = document.createElement('a');
                            $(button).prop('href', 'javascript:;');
                            $(button).prop('alt', btn.name);
                            var id = $.trim(btn.name) + k;
                            $(button).prop('id', id);
                            $(button).addClass(btn.trigger);
                            $(button).prop('alt', btn._event);
                            if (btn.arguments != undefined) {
                                $(button).prop('itemid', btn.arguments);
                            }
                            //Updated On May 30 2011
                            if (btn.callMethod != undefined) {
                                $(button).prop('method', btn.callMethod);
                            }

                            $(button).html(btn.display);
                            //$(actiontools).append(button);
                            $(actionsP).append(button);
                        }
                        $(actiontools).append(actionsP);
                        $(actiontoolswrapper).append(actiontools);
                        $(actionwrapper).append(actiontoolswrapper);
                        $(actionwrapper).append(showhide);

                        $(this).find('td:last').html(actionwrapper);

                        // $(this).find('td:last').html(actiontools);



                        $(actiontools).find('a').each(function() {
                            var trigger = $(this).prop('class');
                            var callMethod = $(this).prop('method');
                            var _event = $(this).prop('alt');
                            var arguments = '';
                            if ($(this).prop('itemid') != undefined) arguments = $(this).prop('itemid');
                            var arg = new Array();
                            //var argus = null;
                            arg[0] = primaryID[k];
                            arg[1] = p.pnew;
                            arg[2] = p.current;

                            if (arguments != '') {
                                var argus = arguments.split(',');
                                for (loop = 0; loop < argus.length; loop++) {
                                    //arg[arg.length] = argus[loop];
                                    arg[arg.length] = g.getCellValue($(this), argus[loop]);
                                }
                            }


                            switch (_event) {
                                case "click":
                                    $(this).click(function() {
                                        getFunction(t.id, trigger, callMethod, arg);
                                    });
                                    break;
                                case "mouseover":
                                    $(this).mouseover(function() {
                                        getFunction(t.id, trigger, callMethod, arg);
                                    });
                                    break;
                            }
                        });
                        $(actiontoolswrapper).hide();
                        $(showhide).find('a').hover(function() {
                            //$(showhide).hide();
                            $('div.cssClassActionOnClickShow').hide();
                            $(actiontoolswrapper).show();
                        });
						  $(showhide).mouseout(function() {
                            //$(showhide).hide();
                            $('div.cssClassActionOnClickShow').hide();
                            $(actiontoolswrapper).hide();
                        });

                        $(actiontoolswrapper).mouseover(function() {
                            $(this).show();
                        }).mouseout(function() {
                            $(this).hide();
                            //$(showhide).show();
                        });

                    });
                }
            },

            addHeader: function() {
                if (p.colModel) {

                    thead = document.createElement('thead');
                    tr = document.createElement('tr');


                    for (j = 0; j < p.colModel.length; j++) {
                        var cm = p.colModel[j];
                        var th = document.createElement('th');

                        $(th).html(cm.display);


                        if (cm.coltype == 'checkbox') {
                            var chkbox = document.createElement('input');
                            $(chkbox).prop('type', 'checkbox');
                            $(th).html(chkbox);
                            if (cm.elemDefault != undefined) {
                                $(chkbox).prop('checked', cm.elemDefault);
                            }

                            if (cm.controlclass != undefined) {
                                var controlClass = cm.controlclass;
                                $(chkbox).prop('class', cm.controlclass);
                            }


                            if (cm.elemClass != undefined) {
                                var elementclass = cm.elemClass;
                            }

                            $(chkbox).click(function() {                                       
                                $('.' + elementclass)
			                            .not(':disabled')
			                            .prop('checked', $(this).is(':checked'));
                                        g.MakeHeaderCheck(elementclass,controlClass);
                            });
                        }

                        //MOdified On July 20 2011
                        if (cm.value) {

                            tdvalue[j] = cm.value;
                        }

                        if (cm.randomValue) {
                            tdRandomValue[j] = cm.randomValue;
                        }

                        if (cm.downloadarguments) {
                            DownloadArguments[j] = cm.downloadarguments;
                        }

                        if (cm.downloadmethod) {
                            DownloadMethod[j] = cm.downloadmethod;
                        }

                        //Modified On 29 Dec 2010
                        if (cm.checkFor) {
                            tdcheckfor[j] = cm.checkFor;
                        }

                        if (cm.checkedItems) {

                            tdcheckedItems[j] = cm.checkedItems;
                        }

                        if (cm.name)
                            $(th).prop('abbr', cm.name);

                        //th.idx = i;
                        $(th).prop('axis', 'col' + j);

                        if (cm.align) {
                            // th.align = cm.align;
                            tdalign[j] = cm.align;
                        }

                        if (cm.type) {
                            tdformat[j] = cm.format;
                            tdtype[j] = cm.type;
                        }
                        else {
                            tdformat[j] = '';
                            tdtype[j] = '';
                        }

                        if (cm.cssclass)
                            $(th).addClass(cm.cssclass);

                        // Update On 12 Dec 2010
                        if (cm.hide) {
                            $(th).hide();
                            tdHide[j] = cm.hide;
                        }

                        if (cm.process) {
                            th.process = cm.process;
                        }

                        if (cm.elemClass) {
                            ElemClass[j] = cm.elemClass;
                        }

                        if (cm.elemDefault) {
                            ElemDefault[j] = cm.elemDefault;
                        }

                        if (cm.controlclass) {
                            ControlClass[j] = cm.controlclass;
                        }

                        //Updated On May 29 2011
                        if (cm.url) {
                            tdURL[j] = cm.url;
                        }
                        if (cm.queryPairs) {
                            QueryPairs[j] = cm.queryPairs;
                        }
                        if (cm.showpopup) {
                            ShowPopUp[j] = cm.showpopup;
                        }
                        if (cm.popupid) {
                            PopUpID[j] = cm.popupid;
                        }

                        if (cm.btntitle) {
                            BtnTitle[j] = cm.btntitle;
                        }

                        if (cm.poparguments) {
                            PopUpArguments[j] = cm.poparguments;
                        }
                        if (cm.popupmethod) {
                            PopUpMethod[j] = cm.popupmethod;
                        }

                        if (cm.alttext) {
                            AltText = cm.alttext;
                        }

                        $(tr).addClass('cssClassHeading');

                        $(tr).append(th);

                    } // For Ends

                    $(thead).append(tr);
                    $(t).prepend(thead);
                }
            },

            addRows: function(data) {
                // Insert New Row...
            },

            addRowProp: function() {
                //                $('tbody tr', t).each(function() {
                //                    $(this).click(function() {

                //                        alert($(this).find('td').eq(0).html() + 'row is clicked!');
                //                    });
                //                });
            },

            addData: function(data) {
                var tbody = document.createElement('tbody');
                $.each(data.d, function(i, row) {
                    delete (row.__type);
                    p.total = row.RowTotal;
                    delete (row.RowTotal);
                    var setprimaryID = false;
                    var tr = document.createElement('tr');
                    tr.className = (i % 2 && p.striped) ? 'sfOdd' : 'sfEven';
                    if (row.id) tr.id = 'row' + i;

                    //var td = document.createElement('td');

                    //$(td).html(i + 1)
                    //td.align = tdalign[0]; 
                    //$(tr).append(td)
                    //if (tdHide[0]) $(td).hide();
                    var cell = 0;

                    $.each(row, function(ncols) {
                        //alert(row[ncols]);
                        if (setprimaryID == false) {
                            //alert(row[ncols]);
                            primaryID[primaryID.length] = row[ncols]
                            setprimaryID = true;
                        }
                        var td = document.createElement('td');
                        //var idx = $(this).prop('axis').substr(3);
                        td.align = tdalign[cell];
                        td.style.textAlign = td.align;
                        if (tdtype[cell] != '') {
                            row[ncols] = g.formatContent(row[ncols], tdtype[cell], tdformat[cell]);

                        }

                        $(td).html(row[Encoder.htmlDecode(ncols)]);
                        $(tr).append(td);

                        //Update On 12 Dec 2010
                        if (tdHide[cell]) $(td).hide();
                        td = null;
                        cell++;

                    }); // ncols ends

                    if (p.buttons) {
                        var td = document.createElement('td');
                        td.align = tdalign[cell];

                        $(tr).append(td);
                        if (tdHide[cell]) $(td).hide();
                    }

                    $(tbody).append(tr);

                }); // row ends

                $(t).append(tbody);
            },
            formatContent: function(content, type, formats) {
                var returnvalue;
                switch (type) {
                    case 'date':
                        content = String(content);
                        var isDate = /Date\(([-+]?\d+[-+]?\d+)\)/.exec(content);
                        if (isDate) {
                            isDate2 = isDate[1].split('+');
                            var n = parseInt(isDate2[0]);
                            returnvalue = new Date(n);
                            //returnvalue = returnvalue.toString();
                        }

                        if (formats == undefined || formats == '') formats = p.dateformat;
                        returnvalue = $.format.date(returnvalue, formats);
                        break;

                    case "boolean":
                        var txt = formats.split('/');
                        returnvalue = content == true ? txt[0] : txt[1];
                        break;
                }
                return returnvalue;

            },

            addControls: function() {
                if (p.colModel) {
                    var tableGridId = t.id;
                    $('tbody tr', t).each(function(tcount) {
                        $(this).find('td').each(function(rcount) {
                            var cm = p.colModel[rcount];
                            switch (cm.coltype) {
                                case "checkbox":
                                    //Updated On 29 Dec 2010                                   
                                    var chkbox = document.createElement('input');
                                    $(chkbox).prop('type', 'checkbox');
                                    $(chkbox).prop('value', primaryID[tcount]);

                                    if (ElemClass[rcount] != undefined) {
                                        $(chkbox).prop('class', ElemClass[rcount]);
                                    }

                                    if (ElemDefault[rcount] != undefined) {
                                        $(chkbox).prop('checked', ElemDefault[rcount]);
                                    }

                                    var enableCheckStatus = '';
                                    if (tdcheckedItems[rcount] != undefined) {
                                        var parentEl = $(this).parent('TR');
                                        var checkID = $(parentEl).find('TD:eq("' + tdcheckedItems[rcount] + '")').text();
                                        //alert(checkID);
                                        enableCheckStatus = (checkID.toLowerCase() == 'yes') ? 'checked' : '';
                                        //alert(enableCheckStatus);
                                        $(chkbox).prop('checked', enableCheckStatus);                                       
                                    }
                                    var id = $(chkbox).val();
                                    if (SageData.checkArrItemExist(id, tableGridId)) {
                                        $(chkbox).prop('checked', true);
                                    }
                                    if (SageData.checkDelArrItemExist(id, tableGridId)) {
                                        $(chkbox).prop('checked', false);
                                    }

                                    var checkstatus = '';
                                    if (tdcheckfor[rcount] != undefined) {
                                        //var elementclass = ElemClass[rcount];
                                        //var controlClass = ControlClass[rcount];
                                        var parentEls = $(this).parent('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdcheckfor[rcount] + '")').text();
                                        checkstatus = (indexValue.toLowerCase() == 'yes') ? 'disabled' : '';
                                        //alert(checkstatus);                                        
                                        $(chkbox).prop('disabled', checkstatus);
                                    }
                                    $(this).html(chkbox);
                                    break;
                                case "textbox":
                                    var txtbox = document.createElement('input');
                                    $(txtbox).prop('type', 'text');
                                    $(txtbox).prop('id', 'text_' + tcount + '_' + primaryID[tcount]);

                                    //$(txtbox).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(txtbox).addClass(ControlClass[rcount]);
                                    }
                                    var previousvalue = $(this).html();
                                    $(txtbox).prop('value', previousvalue);
                                    $(this).html(txtbox);
                                    break;

                                //Updated On July 20 2011                                                                                                                                                                                                                                                                                                                                       
                                case "linklabel":
                                    var lblVal = document.createElement('label');
                                    var trElement = $(this);
                                    $(lblVal).prop('id', 'lbl_' + tcount + '_' + primaryID[tcount]);
                                    //$(lblVal).addClass(p.txtClass);                                                             
                                    if (ControlClass[rcount] != undefined) {
                                        $(lblVal).addClass(ControlClass[rcount]);
                                    }

                                    if (tdvalue[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdvalue[rcount] + '")').text();
                                        $(lblVal).prop('href', indexValue);
                                    }


                                    var previousvalue = $(this).html();
                                    $(lblVal).html(previousvalue);
                                    $(this).html(lblVal);

                                    $(lblVal).click(function() {
                                        if (DownloadMethod[rcount] != undefined && DownloadArguments[rcount] != undefined) {
                                            g.AddLabelLinkEvent(trElement, $(this), rcount);
                                        }
                                    });
                                    break;

                                case "download":                                 
                                    var lblValwrap = $("<a href='#'></a>");
                                    var lblVal = $("<span/>");
                                    var trElement = $(this);
                                    lblValwrap.append(lblVal);
                                    lblVal.prop('id', 'lbl_' + tcount + '_' + primaryID[tcount]);                                 
                                    //$(lblVal).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        if (tdRandomValue[rcount] != undefined) {
                                            var parentEls = $(this).parents('TR');
                                            var randomValue = $(parentEls).find('TD:eq("' + tdRandomValue[rcount] + '")').text();
                                            lblVal.addClass(ControlClass[rcount] + '_' + randomValue);
                                        }

                                    }
                                    //   alert(randomValue);
                                    //alert(primaryID[tcount]);                                    
                                    if (tdvalue[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdvalue[rcount] + '")').text();
                                        lblVal.attr('href', indexValue);
                                    }

                                    var previousvalue = $(this).html();
                                    lblVal.html(previousvalue);
                                    $(this).html(lblValwrap);

                                    lblVal.click(function() {
                                        if (DownloadMethod[rcount] != undefined && DownloadArguments[rcount] != undefined) {
                                            g.AddLabelLinkEvent(trElement, $(this), rcount);
                                        }
                                    });
                                    break;

                                case "currency":
                                    var lblVal = document.createElement('label');
                                    var trElement = $(this);
                                    $(lblVal).prop('id', 'lbl_' + tcount + '_' + primaryID[tcount]);
                                    //$(lblVal).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(lblVal).addClass(ControlClass[rcount]);
                                    }
                                    if (tdvalue[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdvalue[rcount] + '")').text();
                                        $(lblVal).prop('text', indexValue);
                                    }
                                    var previousvalue = $(this).html();

                                    //For Price $/%
                                    if (tdcheckfor[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdcheckfor[rcount] + '")').text();
                                        if (indexValue.toLowerCase() == 'yes') {
                                            $(lblVal).html(previousvalue);
                                            lblVal = $(lblVal).text() + '%';
                                            $(this).html(lblVal);
                                        }
                                        else {

                                            $(lblVal).html(previousvalue);
                                            $(this).html(lblVal);
                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                        }
                                    }
                                    else {
                                        $(lblVal).html(previousvalue);
                                        $(this).html(lblVal);
                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                    }
                                    break;

                                case "currencyFront":
                                    var lblVal = document.createElement('label');
                                    var trElement = $(this);
                                    $(lblVal).prop('id', 'lbl_' + tcount + '_' + primaryID[tcount]);
                                    //$(lblVal).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(lblVal).addClass(ControlClass[rcount]);
                                    }
                                    if (tdvalue[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdvalue[rcount] + '")').text();
                                        $(lblVal).prop('text', indexValue);
                                    }
                                    var previousvalue = $(this).html();

                                    //For Price $/%
                                    if (tdcheckfor[rcount] != undefined) {
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD:eq("' + tdcheckfor[rcount] + '")').text();
                                        if (indexValue.toLowerCase() == 'yes') {
                                            $(lblVal).html(previousvalue);
                                            lblVal = $(lblVal).text() + '%';
                                            $(this).html(lblVal);
                                        }
                                        else {

                                            $(lblVal).html(previousvalue * rate);
                                            $(this).html(lblVal);
                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                        }
                                    }
                                    else {
                                        $(lblVal).html(previousvalue * rate);
                                        $(this).html(lblVal);
                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                    }
                                    break;

                                case "link":
                                    var href = document.createElement('a');
                                    var trElement = $(this);

                                    $(href).prop('id', 'link_' + tcount + '_' + primaryID[tcount]);

                                    //$(href).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(href).addClass(ControlClass[rcount]);
                                    }

                                    var previousvalue = $(this).html();
                                    $(href).html(previousvalue);
                                    $(this).html(href);

                                    $(href).click(function() {
                                        g.AddLinkEvent(trElement, $(this), rcount);
                                    });
                                    break;
                                    //Added On 5/29/2014 For Localization of Group Items "Starting At" Price
                                case "Price":
                                    var trElement = $(this);
                                    //Get the column number of ItemTypeid=5 i.e Group Item from the hidden ItemTypeID column
                                    col = $('#'+tableGridId+'').find('thead').find('th[abbr="_itemTypeID"]').index();
                                    if (parseInt(trElement.parents("tr").find("td:eq(" + col + ")").html()) == 5) {
                                        trElement.prepend(getLocale(CoreJsLanguage, "Starting At"));
                                    }
                                   
                                    break;
                                case "image":
                                    var imgContainDiv = document.createElement('div');
                                    var imgBox = document.createElement('img');

                                    var altText = AltText;
                                    //alert(altText);
                                    var AltValue = '';
                                    if (altText != '') {
                                        var ele = $(this);
                                        var opt = altText.split(',');
                                        $.each(opt, function(i, item) {
                                            AltValue += g.getCellValue(ele, item) + ' ';
                                        });
                                        AltValue = AltValue.substring(0, AltValue.length - 1);
                                        $(imgBox).prop('alt', AltValue);
                                        $(imgBox).prop('title', AltValue);
                                    }

                                    $(imgBox).prop('id', 'img_' + tcount + '_' + primaryID[tcount]);

                                    //$(imgBox).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(imgContainDiv).addClass(ControlClass[rcount]);
                                    }
                                    var previousvalue = $(this).html();
                                    if (previousvalue == "") {
                                        previousvalue = p.defaultImage;
                                    }
                                    if (p.imageOfType != '') {
                                        switch (p.imageOfType) {
                                            case 'item':
                                                {
                                                    previousvalue = "Modules/AspxCommerce/AspxItemsManagement/uploads/Small/" + previousvalue;
                                                }
                                                break;
                                            case 'category':
                                                {
                                                    previousvalue = "Modules/AspxCommerce/AspxCategoryManagement/uploads/Small/" + previousvalue;
                                                }
                                                break;
                                            case 'payment':
                                                {
                                                    previousvalue = "Modules/AspxCommerce/AspxPaymentGateWayManagement/uploads/Small/" + previousvalue;
                                                }
                                                break;
                                            case 'currency':
                                                {
                                                    previousvalue = "images/flags/" + previousvalue;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    $(imgBox).prop('src', aspxRootPath + previousvalue);
                                    $(imgContainDiv).html(imgBox);
                                    $(this).html(imgContainDiv);
                                    break;

                                //Updated On May 30 2011                                                                                                                                                                                               
                                case "button":
                                    var btnBox = document.createElement('input');
                                    var trElement = $(this);
                                    $(btnBox).prop('type', 'button');
                                    $(btnBox).prop('id', 'btn_' + tcount + '_' + primaryID[tcount]);

                                    //$(btnBox).addClass(p.txtClass);
                                    if (ControlClass[rcount] != undefined) {
                                        $(btnBox).addClass(ControlClass[rcount]);
                                    }
                                    var btnTitle = $(this).html();
                                    if (BtnTitle[rcount] != undefined) {
                                        btnTitle = BtnTitle[rcount]
                                    }
                                    var previousvalue = btnTitle;
                                    //$(btnBox).html('<span><span>' + previousvalue + '</span></span>');
                                    $(btnBox).prop('value', previousvalue);
                                    $(this).html(btnBox);

                                    $(btnBox).click(function() {
                                        g.AddButtonEvent(trElement, $(this), rcount);
                                    });
                                    break;
                            } // Case Ends
                        });

                    });
                }
            },

            getCellValue: function(ele, colIndex) {
                //alert(ele.parents().html())();
                //                               var parentEls = ele.parents()
                //                               .map(function() {
                //                                   return this.tagName;
                //                               })
                //                               .get().join(", ");
                //                               alert(parentEls);

                var parentEls = ele.parents('TR');
                var indexValue = $(parentEls).find('TD:eq("' + colIndex + '")').text();
                if (!indexValue) indexValue = '';
                return indexValue;

            },
            runAfterCase: function(caseValue) {                
                switch (caseValue) {
                    case "0":
                    if ($("#gdvIndexedTables").length) {
                        $("#gdvIndexedTables tr").each(function (index) {
                            var trIndex = index - 1;
                            var children = $(this).children();
                            $(this).children().each(function(index){
                                if (index == 4) {
                                    if($(this).text() == '0'){
                                        $('#rebuild' + trIndex).hide();
                                    }
                                    else {
                                        $('#reindex' + trIndex).hide();
                                    }
                                }
                            });
                        });
                    }
                    default:
                    break;        
                }
            },
            validate: function(e) {
                var key;
                var keychar;

                if (window.event)
                    key = window.event.keyCode;
                else if (e)
                    key = e.which;
                else
                    return true;
                keychar = String.fromCharCode(key);

                // control keys
                if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27))
                    return true;

                // numbers
                else if ((("0123456789").indexOf(keychar) > -1))
                    return true;

                // decimal point jump
                else if ((keychar == ".")) {
                    //myfield.form.elements[dec].focus();
                    return false;
                }
                else
                    return false;

            },
            //UPdated on July 26 2011 (Download)
            AddLabelLinkEvent: function(trElement, lblVal, index) { 
                var arguments = '';
                if (DownloadArguments[index] != undefined) {
                    arguments = DownloadArguments[index];
                }

                var arg = new Array();

                if (arguments != '') {
                    var argus = arguments.split(',');
                    for (loop = 0; loop < argus.length; loop++) {
                        arg[arg.length] = g.getCellValue($(lblVal), argus[loop]);
                    }
                }
                getDownloadFunction(DownloadMethod[index], arg);
            },
            //UPdated on May 30 2011, June 6 2011 (PopUP)
            AddLinkEvent: function(trElement, href, index) {
                if (tdURL[index] != 'item' && tdURL[index] != 'category') {
                    if (ShowPopUp[index]) {
                        $(href).prop('href', '#');
                        $(href).prop('method', PopUpMethod[index]);
                        var arguments = '';
                        if (PopUpArguments[index] != undefined) {
                            arguments = PopUpArguments[index];
                        }

                        var arg = new Array();

                        if (arguments != '') {
                            var argus = arguments.split(',');
                            for (loop = 0; loop < argus.length; loop++) {
                                arg[arg.length] = g.getCellValue($(href), argus[loop]);
                            }
                        }
                        getPopUpFunction(PopUpMethod[index], arg, PopUpID[index]);
                    }
                    else {
                        var queryPairs = QueryPairs[index];
                        var queryString = '';
                        if (queryPairs != '') {
                            var opt = queryPairs.split(';');
                            var querySymbol = '?';

                            $.each(opt, function(i, item) {
                                if (i > 0) {
                                    querySymbol = '&';
                                }
                                var optValue = item.split(":");
                                var queryValue = g.getCellValue(trElement, optValue[1]);
                                queryString += querySymbol + optValue[0] + '=' + queryValue;
                            });
                            $(href).prop('href', tdURL[index] + queryString);
                        }
                    }
                }
                else if (tdURL[index] == 'item') {
                    var queryPairs = QueryPairs[index];
                    var queryString = '';
                    //Updated on August-2-2012 to implement costVariant redirection to item detail page.
                    if (queryPairs.indexOf(',') != -1) {
                        var opt = queryPairs.split(',');
                        var optValue = '';
                        var queryVal = '';
                        $.each(opt, function(i, value) {
                            if (i == 0)
                                optValue = g.getCellValue(trElement, value);
                            if (i == 1)
                                queryVal = g.getCellValue(trElement, value);
                        });
                        if (queryVal != "0") {
                            $(href).prop('href', tdURL[index] + '/' + optValue + '.aspx?varId=' + queryVal);
                        }
                        else if (queryVal == "" || queryVal == "0") {
                            $(href).prop('href', tdURL[index] + '/' + optValue + '.aspx');
                        }
                    }
                    else if (queryPairs != '') {
                        var queryValue = g.getCellValue(trElement, queryPairs);
                        $(href).prop('href', tdURL[index] + '/' + queryValue + '.aspx');
                    }
                }
                else if (tdURL[index] == 'category') {
                    var queryPairs = QueryPairs[index];
                    var queryString = '';
                    if (queryPairs != '') {
                        var queryValue = g.getCellValue(trElement, queryPairs);
                        $(href).prop('href', tdURL[index] + '/' + queryValue + '.aspx');
                    }
                }
            },
            AddButtonEvent: function(trElement, btn, index) {
                if (ShowPopUp[index]) {
                    $(btn).prop('method', PopUpMethod[index]);
                    var arguments = '';
                    if (PopUpArguments[index] != undefined) {
                        arguments = PopUpArguments[index];
                    }

                    var arg = new Array();

                    if (arguments != '') {
                        var argus = arguments.split(',');
                        for (loop = 0; loop < argus.length; loop++) {
                            arg[arg.length] = g.getCellValue($(btn), argus[loop]);
                        }
                    }
                    getPopUpFunction(PopUpMethod[index], arg, PopUpID[index]);
                }
                else {
                    var queryPairs = QueryPairs[index];
                    var queryString = '';
                    if (queryPairs != '') {
                        var opt = queryPairs.split(';');
                        var querySymbol = '?';

                        $.each(opt, function(i, item) {
                            if (i > 0) {
                                querySymbol = '&';
                            }
                            var optValue = item.split(":");
                            var queryValue = g.getCellValue(trElement, optValue[1]);
                            queryString += querySymbol + optValue[0] + '=' + queryValue;
                        });
                        //Redirection HERE
                        window.location.href = tdURL[index] + queryString;
                    }
                }
            }
        };
        g.wcf();
    };

    $.fn.sagegrid = function(p) {

        return this.each(function() {
            $.createGrid(this, p);
        });
    };
})(jQuery);