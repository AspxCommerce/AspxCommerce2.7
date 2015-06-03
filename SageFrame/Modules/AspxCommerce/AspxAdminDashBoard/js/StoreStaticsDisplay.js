$(function() {
    var date = [];
    var orders = [];
    var customerVists = [];
    var dateLw = [];
    var ordersLw = [];
    var customerVistsLw = [];
    var dateCm = [];
    var ordersCm = [];
    var customerVistsCm = [];
    var dateYr = [];
    var ordersYr = [];
    var customerVistsYr = [];
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var storeStaticsDisplay = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: storeStaticsDisplay.config.type,
                contentType: storeStaticsDisplay.config.contentType,
                cache: storeStaticsDisplay.config.cache,
                async: storeStaticsDisplay.config.async,
                url: storeStaticsDisplay.config.url,
                data: storeStaticsDisplay.config.data,
                dataType: storeStaticsDisplay.config.dataType,
                success: storeStaticsDisplay.ajaxSuccess,
                error: storeStaticsDisplay.ajaxFailure
            });
        },
        HideDiv: function() {
            $("#divLW").hide();
            $("#div24hours").hide();
            $("#divCM").hide();
            $("#divYear").hide();
        },
        ShowCharts: function(id) {
            id = $("#ddlRange option:selected").val();
            var optionChart = $("#ddlChartType").val();
            switch (optionChart) {
                case '1':
                    if (id == "1" || id == null) {
                        storeStaticsDisplay.HideDiv();
                        $("#div24hours").show();
                        storeStaticsDisplay.Visit24HoursBarChart(date, orders, customerVists);
                    }
                    if (id == "7") {
                        storeStaticsDisplay.HideDiv();
                        $("#divLW").show();
                        storeStaticsDisplay.VisitLastWeekBarChart(dateLw, ordersLw, customerVistsLw);
                    }
                    if (id == "30") {
                        storeStaticsDisplay.HideDiv();
                        $("#divCM").show();
                        storeStaticsDisplay.VisitCurrentMonthBarChart(dateCm, ordersCm, customerVistsCm);
                    }
                    if (id == "365") {
                        storeStaticsDisplay.HideDiv();
                        $("#divYear").show();
                        storeStaticsDisplay.VisitYearBarChart(dateYr, ordersYr, customerVistsYr);
                    }
                    break;
                case '2':
                    if (id == "1" || id == null) {
                        storeStaticsDisplay.HideDiv();
                        $("#div24hours").show();
                        storeStaticsDisplay.Visit24HoursPieChart(date, orders, customerVists);
                    }
                    if (id == "7") {
                        storeStaticsDisplay.HideDiv();
                        $("#divLW").show();
                        storeStaticsDisplay.VisitLastWeekPieChart(dateLw, ordersLw, customerVistsLw);
                    }
                    if (id == "30") {
                        storeStaticsDisplay.HideDiv();
                        $("#divCM").show();
                        storeStaticsDisplay.VisitCurrentMonthPieChart(dateCm, ordersCm, customerVistsCm);
                    }
                    if (id == "365") {
                        storeStaticsDisplay.HideDiv();
                        $("#divYear").show();
                        storeStaticsDisplay.VisitYearPieChart(dateYr, ordersYr, customerVistsYr);
                    }
                    break;
                case '3':
                    if (id == "1" || id == null) {
                        $("#div24hours").html('');
                        storeStaticsDisplay.HideDiv();
                        $("#div24hours").show();
                        storeStaticsDisplay.Visit24HoursLineChart(date, orders, customerVists);
                    }
                    if (id == "7") {
                        $("#divLW").html('');
                        storeStaticsDisplay.HideDiv();
                        $("#divLW").show();
                        storeStaticsDisplay.VisitLastWeekLineChart(dateLw, ordersLw, customerVistsLw);
                    }
                    if (id == "30") {
                        $("#divCM").html('');
                        storeStaticsDisplay.HideDiv();
                        $("#divCM").show();
                        storeStaticsDisplay.VisitCurrentMonthLineChart(dateCm, ordersCm, customerVistsCm);
                    }
                    if (id == "365") {
                        $("#divYear").html('');
                        storeStaticsDisplay.HideDiv();
                        $("#divYear").show();
                        storeStaticsDisplay.VisitYearLineChart(dateYr, ordersYr, customerVistsYr);
                    }
                    break;
            }
        },

        ShowChartRange: function() {
            var optionRange = $("#ddlRange").val();
            switch (optionRange) {
                case '1':
                    $("#div24hours").html('');
                    storeStaticsDisplay.HideDiv();
                    $("#div24hours").show();
                    storeStaticsDisplay.BindChartBy24hoursAmount();
                    break;
                case '7':
                    $("#divLW").html('');
                    storeStaticsDisplay.HideDiv();
                    $("#divLW").show();
                    storeStaticsDisplay.BindChartByLastWeekAmount();
                    break;
                case '30':
                    $("#divCM").html('');
                    storeStaticsDisplay.HideDiv();
                    $("#divCM").show();
                    storeStaticsDisplay.BindChartByCurrentMonthAmount();
                    break;
                case '365':
                    $("#divYear").html('');
                    storeStaticsDisplay.HideDiv();
                    $("#divYear").show();
                    storeStaticsDisplay.BindChartByOneYearAmount();
                    break;
            }
        },
        BindChartByLastWeekAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsByLastWeek";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        BindChartByCurrentMonthAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsBycurentMonth";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindChartByOneYearAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsByOneYear";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindChartBy24hoursAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsBy24Hours";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        Visit24HoursBarChart: function(orderdate, orderTotal, customerVisits) {
            try {
                $('#div24hours').html('');
                $.jqplot.config.enablePlugins = true;
                var date = orderdate;
                var arrTime = orderTotal;
                var arrCv = customerVisits;
                if (orderdate != null && orderTotal != null && customerVisits != null) {
                    plot1 = $.jqplot('div24hours', [arrTime, arrCv], {
                                               animate: !$.jqplot.use_excanvas,
                        title: getLocale(AspxAdminDashBoard, "Orders and Customers"),
                        seriesDefaults: {
                            renderer: $.jqplot.BarRenderer,
                            pointLabels: { show: true }
                        },
                        axes: {
                            xaxis: {
                                renderer: $.jqplot.CategoryAxisRenderer,
                                ticks: date
                            }
                        },
                        highlighter: { show: false },
						 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                    });
                    $('#div24hours').bind('jqplotDataClick',
                function(ev, seriesIndex, pointIndex, data) {
                    $('#info1').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
                }
            );
                }
            } catch (err) {

            }
        },
        Visit24HoursPieChart: function(orderdate, orderTotal, customerVisits) {
            $('#div24hours').html('');
            var s1 = 0;
            var s2 = 0;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                $.each(orderTotal, function(index) {
                    s1 += orderTotal[index];
                });
                $.each(customerVisits, function(index) {
                    s2 += customerVisits[index];
                });
                var data = [
                    ['Orders', s1], ['Customer Registration', s2]
                ];
                jQuery.jqplot.config.enablePlugins = true;
                plot1 = jQuery.jqplot('div24hours', [data], {
                    title: '',
                    seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f'} },
                    legend: { show: true },
                    cursor: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        Visit24HoursLineChart: function(orderdate, orderTotal, customerVisits) {
            $('#div24hours').html('');
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                var plot2 = $.jqplot('div24hours', [orderTotal, customerVisits], {
                    title: '',
                    axesDefaults: {
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                    },
                    seriesDefaults: {
                        rendererOptions: {
                            smooth: true
                        }
                    },
                    axes: {
                                               xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: orderdate
                        }
                    },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitLastWeekBarChart: function(orderdate, orderTotal, customerVisits) {
            $('#divLW').html('');
            $.jqplot.config.enablePlugins = true;
            var date = orderdate;
            var arrTime = orderTotal;
            var arrCv = customerVisits;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                plot1 = $.jqplot('divLW', [arrTime, arrCv], {
                                       animate: !$.jqplot.use_excanvas,
                    title: getLocale(AspxAdminDashBoard, "Orders and Customers"),
                    seriesDefaults: {
                        renderer: $.jqplot.BarRenderer,
                        pointLabels: { show: true }
                    },
                    axes: {
                        xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: date
                        }
                    },
                    highlighter: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
                $('#divLW').bind('jqplotDataClick',
                function(ev, seriesIndex, pointIndex, data) {
                    $('#info1').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
                }
            );
            }
        },
        VisitLastWeekPieChart: function(orderdate, orderTotal, customerVisits) {
            $('#divLW').html('');
            var s1 = 0;
            var s2 = 0;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                $.each(orderTotal, function(index) {
                    s1 += orderTotal[index];
                });
                $.each(customerVisits, function(index) {
                    s2 += customerVisits[index];
                });
                var data = [
                    ['Orders', s1], ['Customer Registration', s2]
                ];
                jQuery.jqplot.config.enablePlugins = true;
                plot1 = jQuery.jqplot('divLW', [data], {
                    title: '',
                    seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f'} },
                    legend: { show: true },
                    cursor: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitLastWeekLineChart: function(orderdate, orderTotal, customerVisits) {
            $('#divLW').html('');
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                var plot2 = $.jqplot('divLW', [orderTotal, customerVisits], {
                    title: '',
                    axesDefaults: {
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                    },
                    seriesDefaults: {
                        rendererOptions: {
                            smooth: true
                        }
                    },
                    axes: {
                                               xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: orderdate
                        }
                    },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitCurrentMonthBarChart: function(orderdate, orderTotal, customerVisits) {
            $('#divCM').html('');
            $.jqplot.config.enablePlugins = true;
            var date = orderdate;
            var arrTime = orderTotal;
            var arrCv = customerVisits;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                plot1 = $.jqplot('divCM', [arrTime, arrCv], {
                                       animate: !$.jqplot.use_excanvas,
                    title: getLocale(AspxAdminDashBoard, "Orders and Customers"),
                    seriesDefaults: {
                        renderer: $.jqplot.BarRenderer,
                        pointLabels: { show: true }
                    },
                    axes: {
                        xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: date
                        }
                    },
                    highlighter: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
                $('#divCM').bind('jqplotDataClick',
                function(ev, seriesIndex, pointIndex, data) {
                    $('#info1').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
                }
            );
            }
        },
        VisitCurrentMonthPieChart: function(orderdate, orderTotal, customerVisits) {
            $('#divCM').html('');
            var s3 = 0;
            var s4 = 0;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                $.each(orderTotal, function(index) {
                    s3 += orderTotal[index];
                });
                $.each(customerVisits, function(index) {
                    s4 += customerVisits[index];
                });
                var data = [
                    ['Orders', s3], ['Customer Registration', s4]
                ];
                jQuery.jqplot.config.enablePlugins = true;
                plot1 = jQuery.jqplot('divCM', [data], {
                    title: '',
                    seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f'} },
                    legend: { show: true },
                    cursor: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitCurrentMonthLineChart: function(orderdate, orderTotal, customerVisits) {
            $('#divCM').html('');
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                var plot2 = $.jqplot('divCM', [orderTotal, customerVisits], {
                    title: '',
                    axesDefaults: {
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                    },
                    seriesDefaults: {
                        rendererOptions: {
                            smooth: true
                        }
                    },
                    axes: {
                                               xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: orderdate
                        }
                    },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitYearBarChart: function(orderdate, orderTotal, customerVisits) {
            $('#divYear').html('');
            $.jqplot.config.enablePlugins = true;
            var date = orderdate;
            var arrTime = orderTotal;
            var arrCv = customerVisits;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                plot1 = $.jqplot('divYear', [arrTime, arrCv], {
                                       animate: !$.jqplot.use_excanvas,
                    title: getLocale(AspxAdminDashBoard, "Orders and Customers"),
                    seriesDefaults: {
                        renderer: $.jqplot.BarRenderer,
                        pointLabels: { show: true }
                    },
                    axes: {
                        xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: date
                        }
                    },
                    highlighter: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
                $('#divYear').bind('jqplotDataClick',
                function(ev, seriesIndex, pointIndex, data) {
                    $('#info1').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
                }
            );
            }
        },
        VisitYearPieChart: function(orderdate, orderTotal, customerVisits) {
            $('#divYear').html('');
            var s5 = 0;
            var s6 = 0;
            if (orderdate != null && orderTotal != null && customerVisits != null) {
                $.each(orderTotal, function(index) {
                    s5 += orderTotal[index];
                });
                $.each(customerVisits, function(index) {
                    s6 += customerVisits[index];
                });
                var data = [
                    ['Orders', s5], ['Customer Registration', s6]
                ];
                jQuery.jqplot.config.enablePlugins = true;
                plot1 = jQuery.jqplot('divYear', [data], {
                    title: '',
                    seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f'} },
                    legend: { show: true },
                    cursor: { show: false },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        VisitYearLineChart: function(orderdate, orderTotal, customerVisits) {
            $('#divYear').html('');

            if (orderdate != null && orderTotal != null && customerVisits != null) {
                var plot2 = $.jqplot('divYear', [orderTotal, customerVisits], {
                    title: '',
                    axesDefaults: {
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                    },
                    seriesDefaults: {
                        rendererOptions: {
                            smooth: true
                        }
                    },
                    axes: {
                                               xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: orderdate
                        }
                    },
					 seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
            grid: {
                drawGridLines: true,                       gridLineColor: '#cccccc',                  background: 'transparent',                     borderColor: '#ff0000',                    borderWidth: 0,                          shadow: false,                              shadowAngle: 0,                           shadowOffset: 1.5,                         shadowWidth: 3,                            shadowDepth: 3
            }
                });
            }
        },
        ajaxSuccess: function(msg) {
            switch (storeStaticsDisplay.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    dateLw = [];
                    ordersLw = [];
                    customerVistsLw = [];
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item !== null) {
                                dateLw.push(item.Date);
                                ordersLw.push(parseInt(item.Orders));
                                customerVistsLw.push(parseInt(item.CustomerVisit));
                            }
                        };
                        storeStaticsDisplay.VisitLastWeekBarChart(dateLw, ordersLw, customerVistsLw);
                    }

                    break;
                case 2:
                    dateCm = [];
                    ordersCm = [];
                    customerVistsCm = [];
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item !== null) {
                                dateCm.push(item.Date);
                                ordersCm.push(parseInt(item.Orders));
                                customerVistsCm.push(parseInt(item.CustomerVisit));
                            }
                        };
                        storeStaticsDisplay.VisitCurrentMonthBarChart(dateCm, ordersCm, customerVistsCm);
                    }

                    break;
                case 3:
                    dateYr = [];
                    ordersYr = [];
                    customerVistsYr = [];
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item !== null) {
                                dateYr.push(item.Date);
                                ordersYr.push(parseInt(item.Orders));
                                customerVistsYr.push(parseInt(item.CustomerVisit));
                            }
                        };
                        storeStaticsDisplay.VisitYearBarChart(dateYr, ordersYr, customerVistsYr);
                    }

                    break;
                case 4:
                    date = [];
                    orders = [];
                    customerVists = [];
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            if (item !== null) {
                                date.push(item.Date);
                                orders.push(parseInt(item.Orders));
                                customerVists.push(parseInt(item.CustomerVisit));
                            }
                        };
                        storeStaticsDisplay.Visit24HoursBarChart(date, orders, customerVists);
                    }

                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (storeStaticsDisplay.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Charts By Week.") + '</p>');
                    break;
                case 2:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Charts By Month.") + '</p>');
                    break;
                case 3:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Charts By Year.") + '</p>');
                    break;
                case 4:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Charts By Day.") + '</p>');
                    break;
            }
        },
        init: function(config) {
            storeStaticsDisplay.HideDiv();
            $("#ddlChartType").show();
            $("#lbla").show();
            storeStaticsDisplay.BindChartBy24hoursAmount();
            $(window).load(function() {
                storeStaticsDisplay.ShowCharts();
            });

            $("#ddlRange").change(function() {
                $("#ddlChartType option[value='1']").attr("selected", "selected");
                storeStaticsDisplay.ShowChartRange();
            });
            $("#ddlChartType").change(function() {
                storeStaticsDisplay.ShowCharts(null);
            });
        }
    };
    storeStaticsDisplay.init();
});
