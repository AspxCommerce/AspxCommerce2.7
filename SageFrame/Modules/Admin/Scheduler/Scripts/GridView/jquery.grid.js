﻿

(function ($) {

    $.createGrid = function (t, p) {
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
            rpOptions: [5, 10, 15, 20, 25, 40],
            pageOf: 'Page {box} of {count}',
            pnew: 1,
            pageshow: 5,
            title: false,
            nomsg: 'No items',
            dateformat: 'yyyy-MM-dd',
            onError: true,
            SuccessFn: function () { },
            SuccessPara: null


        }, p);

        $(t).show()
        .attr({ cellPadding: 0, cellSpacing: 0 });
        //.removeAttr('width');

        // Global Varaiables Settings
        var tdalign = new Array();
        var tdtype = new Array();
        var tdformat = new Array();
        var primaryID = new Array();
        var desablesort = new Array();

        // Update On 12 Dec 2010
        var tdHide = new Array();
        var tdcheckfor = new Array();
        var tdcheckedItems = new Array();

        var ElemClass = new Array();
        var ElemDefault = new Array();
        var ControlClass = new Array();

        var setFlag = false;
        var g = {


            wcf: function () {
                //var param = { offset: p.pnew, limit: p.rp, portalId: 1, userName: 'RAJA' };



                var params = $.extend({ offset: p.pnew, limit: p.rp }, p.param);

                var mydata = JSON2.stringify(params);

                $(document).ready(function () {
                    $("#loading").ajaxStart(function () {
                        $(this).show();
                    });

                    $("#loading").ajaxStop(function () {
                        $(this).hide();
                    });


                    $('#log').ajaxError(function (e, xhr, ajaxOptions, exception) {
                        if (p.onError) {
                            g.clearAll();
                            $(this).text(xhr.responseText);
                        }
                    });

                    $.ajax({
                        type: "POST",
                        url: p.url + p.functionMethod,
                        data: mydata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
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
                            }
                            $(t).show();
                            $(t).wrap("<div class='cssTableGridWrapper'></div>");
                            if (p.SuccessPara != null) {
                                jQuery.facebox(p.SuccessPara);

                                $("div.content").find('select').change(
                                function () {
                                    p.page = 1;
                                    p.rp = this.value;
                                    g.clearAll();
                                    p.pnew = 1;
                                    p.current = 1;
                                    g.wcf();

                                    // console.log("hi");
                                });

                            }
                        }
                    });
                });
            },

            noDataMsg: function () {
                // alert(p.colModel.length);
                var tbody = document.createElement('tbody');
                var tr = document.createElement('tr');
                var td = document.createElement('td');
                $(td).attr('colspan', p.colModel.length);
                $(td).html(p.nomsg);
                $(tr).append(td);
                $(tbody).append(tr);
                $(t).append(tbody);

            },

            newPagination: function () {
                var opt = '';
                var PageWrapper = document.createElement('div');
                var PageOf = document.createElement('div');
                var Pages = document.createElement('div');
                $(Pages).addClass("cssClassPageNumber clearfix");


                // Call Pagination Class
                sagePaging.Paging(p.total, p.rp); // total,rowperpage
                sagePaging.NavPaging(p.current, p.pageshow);  // currentPage


                $(PageOf).addClass("cssClassPages");
                var txt = p.pageOf.replace(/{box}/, '<input type="text" value="' + p.current + '" />');
                txt = txt.replace(/{count}/, sagePaging.maxPage);
                $(PageOf).prepend(txt);

                for (var nx = 0; nx < p.rpOptions.length; nx++) {

                    if (p.rp == p.rpOptions[nx]) sel = 'selected="selected"'; else sel = '';
                    opt += "<option value='" + p.rpOptions[nx] + "' " + sel + " >" + p.rpOptions[nx] + "</option>";
                };
                $(PageWrapper).prepend("<div class='cssClassViewPerPage'>View Per Page <select name='rp' id='rp'>" + opt + "</select></div>");
                $(PageWrapper).find('select').change(
					function () {
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
                $(PageWrapper).insertAfter($(t));
                $(PageWrapper).addClass('cssClassPagination');
                $(PageWrapper).attr('id', ($(t).attr('id') + '_Pagination'));

                $(PageOf).find('input').keypress(function (e) {
                    //alert(sagePaging.maxPage);
                    var txtval = $.trim($(this).attr('value'));
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

                $(Pages).find('a').each(function () {
                    $(this).click(function () {
                        g.invokePagination($(this).attr('alt'));
                    });
                });

            },


            invokePagination: function (pageno) {
                p.current = pageno;
                p.pnew = (pageno - 1) * p.rp + 1; //offset                 
                //g.clearAll();
                g.wcf();

            },

            clearAll: function () {
                $(t).find('thead').remove();
                $(t).find('tbody').remove();
                $(t).css('display', 'none');
                var id = $(t).attr('id') + '_Pagination';
                $('#' + id).remove();
                primaryID.length = 0;
                //$('.cssClassPagination').remove();
            },

            addActions: function () {
                if (p.buttons) {
                    $('tbody tr', t).each(function (k) {

                        var actionwrapper = document.createElement('div');
                        var actiontoolswrapper = document.createElement('div');
                        var actiontools = document.createElement('div');
                        var actionsP = document.createElement('p');
                        var showhide = document.createElement('p');


                        $(actionwrapper).addClass('cssClassActionOnClick');
                        $(actiontoolswrapper).addClass('cssClassActionOnClickShow');
                        $(actiontools).addClass('cssClassActionOnClickShowInfo');
                        $(showhide).addClass('Sageshowhide');
                        $(showhide).addClass('cssClassActionImg');
                        $(showhide).append('<a href="#">&nbsp;</a>');


                        //$(actiontoolswrapper).css({ width: '200px', height: '200px', border: '1px solid red' })

                        for (i = 0; i < p.buttons.length; i++) {
                            var btn = p.buttons[i];
                            var button = document.createElement('a');
                            $(button).attr('href', '#');
                            $(button).attr('alt', btn.name);
                            var id = $.trim(btn.name) + k;
                            $(button).attr('id', id);
                            $(button).addClass(btn.trigger);
                            $(button).attr('alt', btn._event);
                            if (btn.arguments != undefined) {
                                $(button).attr('itemid', btn.arguments);
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

                        $(actiontools).find('a').each(function () {
                            var trigger = $(this).attr('class');
                            var _event = $(this).attr('alt');
                            var arguments = '';
                            if ($(this).attr('itemid') != undefined) arguments = $(this).attr('itemid');
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

                                    $(this).click(function () {

                                        getFunction(trigger, arg);
                                    });
                                    break;
                                case "mouseover":
                                    $(this).mouseover(function () {
                                        getFunction(trigger, arg);
                                    });
                                    break;
                            }


                        });
                        $(actiontoolswrapper).hide();
                        $(showhide).find('a').click(function () {
                            //$(showhide).hide();
                            $(actiontoolswrapper).show();
                        });

                        $(actiontoolswrapper).mouseover(function () {
                            $(this).show();
                        }).mouseout(function () {
                            $(this).hide();
                            //$(showhide).show();
                        });

                    });
                }

            },

            addHeader: function () {
                if (p.colModel) {

                    thead = document.createElement('thead');
                    tr = document.createElement('tr');


                    for (j = 0; j < p.colModel.length; j++) {
                        var cm = p.colModel[j];
                        var th = document.createElement('th');

                        $(th).html(cm.display);
                        if (cm.coltype == 'l') {

                        }

                        if (cm.coltype == 'checkbox') {
                            var chkbox = document.createElement('input');
                            $(chkbox).attr('type', 'checkbox');
                            $(th).html(chkbox);

                            if (cm.elemDefault != undefined) {
                                $(chkbox).attr('checked', cm.elemDefault);
                            }

                            if (cm.controlclass != undefined) {
                                $(chkbox).attr('class', cm.controlclass);
                            }



                            var elementclass = cm.elemClass;

                            $(chkbox).click(function () {

                                $("INPUT[class='" + elementclass + "'][type='checkbox']")
			                            .not(':disabled')
			                            .attr('checked', $(this).is(':checked'));
                            });

                        }

                        //Modified On 29 Dec 2010 
                        if (cm.checkFor) {

                            tdcheckfor[j] = cm.checkFor;
                        }

                        if (cm.checkedItems) {

                            tdcheckedItems[j] = cm.checkedItems;
                        }

                        if (cm.name)
                            $(th).attr('abbr', cm.name);

                        //th.idx = i;
                        $(th).attr('axis', 'col' + j);

                        if (cm.align) {
                            th.align = cm.align;
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



                        $(tr).addClass('cssClassHeadingOne');


                        $(tr).append(th);


                    } // For Ends

                    $(thead).append(tr);
                    $(t).prepend(thead);



                }
            },

            addRows: function (data) {
                // Insert New Row...
            },

            addRowProp: function () {
                //                $('tbody tr', t).each(function() {
                //                    $(this).click(function() {

                //                        alert($(this).find('td').eq(0).html() + 'row is clicked!');
                //                    });
                //                });
            },

            addData: function (data) {
                var tbody = document.createElement('tbody');
                $.each(data.d, function (i, row) {
                    delete (row.__type);
                    p.total = row.RowTotal;
                    delete (row.RowTotal);
                    var setprimaryID = false;
                    var tr = document.createElement('tr');
                    tr.className = (i % 2 && p.striped) ? 'cssClassAlternativeOdd' : 'cssClassAlternativeEven';
                    if (row.id) tr.id = 'row' + i;

                    //var td = document.createElement('td');

                    //$(td).html(i + 1)
                    //td.align = tdalign[0];
                    //$(tr).append(td)
                    //if (tdHide[0]) $(td).hide();
                    var cell = 0;

                    function getUnit(num) {

                        var r = "";
                        switch (num) {

                            case 1: r = "sec";
                                break;
                            case 2: r = "min";
                                break;
                            case 3: r = "hrs";
                                break;
                            case 4:
                                r = "days";
                                break;

                        }
                        return r;

                    }
                    $.each(row, function (ncols) {
                        //alert(row[ncols]);
                        if (setprimaryID == false) {
                            //alert(row[ncols]);
                            primaryID[primaryID.length] = row[ncols]
                            setprimaryID = true;
                        }
                        var td = document.createElement('td');
                        //var idx = $(this).attr('axis').substr(3);
                        td.align = tdalign[cell];

                        if (tdtype[cell] != '') {
                            row[ncols] = g.formatContent(row[ncols], tdtype[cell], tdformat[cell]);

                        }

                        if (ncols == 'RetryTimeLapse') row[ncols] = row[ncols] + ' ' + getUnit(row['RetryFrequencyUnit']);
                        else if (ncols == 'StartDate' && row['StartHour'] != undefined) row[ncols] = row[ncols].substring(0, row[ncols].indexOf(':') -2) + ' ' + row['StartHour'] + ':' + row['StartMin'] + ':00';
                        else if (ncols == 'EndDate') row[ncols] = row[ncols].substring(0, row[ncols].indexOf(':') - 2);
                        else if (ncols == 'return_text') row[ncols] = row[ncols].substring(0, 40);
                        else if (ncols == 'HistoryEndDate') row[ncols] = row[ncols];
                        else if (ncols == 'RunningMode') {
                            switch (row[ncols]) {
                                case 0:
                                    row[ncols] = "Hourly";
                                    break;
                                case 1:
                                    row[ncols] = "Daily";
                                    break;
                                case 2:
                                    row[ncols] = "Weekly";
                                    break;
                                case 3:
                                    row[ncols] = "WeekNumber";
                                    break;
                                case 4:
                                    row[ncols] = "Calendar";
                                    break;

                                case 5:
                                    row[ncols] = "Once";
                                    break;
                            }



                        }



                        $(td).html(row[ncols])
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
            formatContent: function (content, type, formats) {
                switch (type) {
                    case 'date':
                        content = String(content);
                        var isDate = /Date\(([-+]?\d+[-+]?\d+)\)/.exec(content);
                        if (isDate) {
                            isDate2 = isDate[1].split('+');
                            var n = parseInt(isDate2[0]);
                            returnvalue = new Date(n);
                            if (returnvalue.getFullYear() == "1900") returnvalue = "N/A";
                            //returnvalue = returnvalue.toString();
                        } else { returnvalue = "N/A"; }

                        if (formats == undefined || formats == '') formats = p.dateformat;
                        if(returnvalue instanceof Date)
                        returnvalue = returnvalue.getDate() + "/" + (returnvalue.getMonth() + 1) + "/" + returnvalue.getFullYear() + " " + returnvalue.getHours() + ":" + returnvalue.getMinutes();
                        break;

                    case "boolean":
                        var txt = formats.split('/');
                        returnvalue = content == true ? txt[0] : txt[1];
                        break;
                }
                return returnvalue;

            },

            addControls: function () {
                if (p.colModel) {
                    $('tbody tr', t).each(function (tcount) {
                        $(this).find('td').each(function (rcount) {
                            var cm = p.colModel[rcount];
                            // if(cm!=undefined)
                            switch (cm.coltype) {
                                case "checkbox":
                                    //Updated On 29 Dec 2010
                                    var chkbox = document.createElement('input');
                                    $(chkbox).attr('type', 'checkbox');
                                    $(chkbox).attr('value', primaryID[tcount]);

                                    if (ElemClass[rcount] != undefined) {
                                        $(chkbox).attr('class', ElemClass[rcount]);
                                    }

                                    if (ElemDefault[rcount] != undefined) {
                                        $(chkbox).attr('checked', ElemDefault[rcount]);
                                    }

                                    var enableCheckStatus = '';
                                    if (tdcheckedItems[rcount] != undefined) {
                                        var parentEl = $(this).parents('TR');
                                        var checkID = $(parentEl).find('TD').eq(tdcheckedItems[rcount]).text();
                                        //alert(checkID);
                                        enableCheckStatus = (checkID == 'yes') ? 'checked' : '';
                                        //alert(enableCheckStatus);
                                        $(chkbox).attr('checked', enableCheckStatus);
                                    }

                                    var checkstatus = '';
                                    if (tdcheckfor[rcount] != undefined) {
                                        //var elementclass = ElemClass[rcount];
                                        //var controlClass = ControlClass[rcount];
                                        var parentEls = $(this).parents('TR');
                                        var indexValue = $(parentEls).find('TD').eq(tdcheckfor[rcount]).text();
                                        checkstatus = (indexValue == 'yes') ? 'disabled' : '';
                                        //alert(checkstatus);
                                        $(chkbox).attr('disabled', checkstatus);
                                    }

                                    $(this).html(chkbox);

                                    if (ElemClass[rcount] != undefined) {

                                        var elementclass = ElemClass[rcount];
                                        var controlClass = ControlClass[rcount];

                                        $('.' + elementclass).click(function () {
                                            var uncheckedflag = true;
                                            $('.' + elementclass).each(function () {

                                                if (!$(this).is(':checked')) {

                                                    uncheckedflag = false;
                                                }
                                                $('.' + controlClass).attr('checked', uncheckedflag);
                                            });
                                        });
                                    }
                                    break;
                                case "textbox":
                                    var txtbox = document.createElement('input');
                                    $(txtbox).attr('type', 'text');
                                    $(txtbox).attr('id', 'text_' + tcount + '_' + primaryID[tcount]);
                                    //$(txtbox).addClass(p.txtClass);
                                    if (ElemClass[rcount] != undefined) {
                                        $(txtbox).addClass(ElemClass[rcount]);
                                    }
                                    var previousvalue = $(this).html();
                                    $(txtbox).attr('value', previousvalue);
                                    $(this).html(txtbox);
                                    break;
                            } // Case Ends
                        });

                    });
                }
            },

            getCellValue: function (ele, colIndex) {
                //alert(ele.parents().html())();
                //                               var parentEls = ele.parents()
                //                               .map(function() {
                //                                   return this.tagName;
                //                               })
                //                               .get().join(", ");
                //                               alert(parentEls);

                var parentEls = ele.parents('TR');
                var indexValue = $(parentEls).find('TD').eq(colIndex).html();
                if (!indexValue) indexValue = '';
                return indexValue;

            },
            validate: function (e) {
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

            }
        };

        g.wcf();



    };

    $.fn.sagegrid = function (p) {

        return this.each(function () {
            $.createGrid(this, p);
        });
    };
})(jQuery);