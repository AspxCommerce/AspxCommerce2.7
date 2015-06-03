<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRecords.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminDashBoard_UserRecords" %>

<script type="text/javascript">

    $(function () {

        $(".sfLocale").localize({
            moduleKey: AspxAdminDashBoard
        });

        var visitor = function () {

            var aspxCommonObj = function () {
                var aspxCommonInfo = {
                    StoreID: AspxCommerce.utils.GetStoreID(),
                    PortalID: AspxCommerce.utils.GetPortalID(),
                    UserName: AspxCommerce.utils.GetUserName(),
                    CultureName: AspxCommerce.utils.GetCultureName()
                };
                return aspxCommonInfo;
            };
            var $ajaxCall = function (method, param, successFx, error) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            var drawPieChartNewAccountStatics = function (s1, s2) {
                $("#dvVisitorAccount").html('');
                if (s1 > 0 && s2 > s1 != null && s2 != null) {
                    $('#dvVisitorAccount').show();
                    $("#dvVisitorAccount").html('');
                    var data = [['OnlyVisited', s1], ['TotalCustomer', s2]];

                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot('dvVisitorAccount', [data], {
                        title: '', seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f' } }, legend: { show: true }, cursor: { show: false },
                        seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                        grid: { drawGridLines: true, gridLineColor: '#cccccc', background: 'transparent', borderColor: '#ff0000', borderWidth: 0, shadow: false, shadowAngle: 0, shadowOffset: 1.5, shadowWidth: 3, shadowDepth: 3 }

                    });
                }
            };
            var getVisitorNewAccountStatics = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                aspxCommonObj.CultureName = null;
                var day = $.trim($("#ddlVisitorAccount").val());
                var param = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetVisitorsNewAccount", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblVisitorNewAccount").show();
                        $("#spnVisitorNewAccount").html('');
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.TotalVisit + "</td>";
                            tr += "<td>" + item.TotalCustomer + "</td>";
                            tr += "</tr>";
                            var rate = parseFloat((item.TotalCustomer / item.TotalVisit) * 100).toFixed(2);
                            if (rate == 'NaN') {
                                rate = '0.00';
                            }
                            $("#lblVisitorAccount").html(rate + '%');
                            drawPieChartNewAccountStatics(item.TotalVisit, item.TotalCustomer);
                        });
                        $("#tblVisitorNewAccount tr:gt(0)").remove();
                        $("#tblVisitorNewAccount").append(tr);
                    } else {
                        $("#tblVisitorNewAccount").hide();
                        $("#spnVisitorNewAccount").html(getLocale(AspxAdminDashBoard, "No Records Found!"));

                    }

                }, null);

            };
            var drawPieChartVisitorsOrder = function (s1, s2) {
                $("#dvVisitorOrder").html('');
                if (s1 > 0 && s2 > s1 != null && s2 != null) {
                    $('#dvVisitorOrder').show();
                    $("#dvVisitorOrder").html('');
                    var data = [['OnlyVisited', s1], ['TotalOrder', s2]];
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot('dvVisitorOrder', [data], {
                        title: '', seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f' } }, legend: { show: true }, cursor: { show: false },
                        seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                        grid: { drawGridLines: true, gridLineColor: '#cccccc', background: 'transparent', borderColor: '#ff0000', borderWidth: 0, shadow: false, shadowAngle: 0, shadowOffset: 1.5, shadowWidth: 3, shadowDepth: 3 }

                    });

                }
            };
            var getVisitorsOrder = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                aspxCommonObj.CultureName = null;
                var day = $.trim($("#ddlVisitorOrder").val());
                var param = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetVisitorsOrder", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblVisitorOrder").show();
                        $("#spnVisitorOrder").html('');
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.TotalVisit + "</td>";
                            tr += "<td>" + item.TotalOrder + "</td>";
                            tr += "</tr>";
                            var rate = parseFloat((item.TotalOrder / item.TotalVisit) * 100).toFixed(2);
                            if (rate == 'NaN') {
                                rate = '0.00';
                            }
                            $("#lblVisitorOrderRate").html(rate + '%');
                            drawPieChartVisitorsOrder(item.TotalVisit, item.TotalOrder);
                        });
                        $("#tblVisitorOrder tr:gt(0)").remove();
                        $("#tblVisitorOrder").append(tr);
                    } else {
                        $("#tblVisitorOrder").hide();
                        $("#spnVisitorOrder").html(getLocale(AspxAdminDashBoard, "No Records Found!"));

                    }

                }, null);

            };


            var drawPieChartVisitorsNewOrder = function (s1, s2) {
                $("#dvNewAccountNewOrder").html('');
                if (s1 > 0 && s2 > s1 != null && s2 != null) {
                    $('#dvNewAccountNewOrder').show();
                    $("#dvNewAccountNewOrder").html('');
                    var datas = [];
                    var data = [['TotalCustomer', s1], ['TotalOrder', s2]];
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot('dvNewAccountNewOrder', [data], {
                        title: '', seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f' } }, legend: { show: true }, cursor: { show: false },
                        seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                        grid: { drawGridLines: true, gridLineColor: '#cccccc', background: 'transparent', borderColor: '#ff0000', borderWidth: 0, shadow: false, shadowAngle: 0, shadowOffset: 1.5, shadowWidth: 3, shadowDepth: 3 }

                    });
                }
            };
            var getVisitorsNewOrder = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                aspxCommonObj.CultureName = null;
                var day = $.trim($("#ddlNewAccountNewOrder").val());
                var param = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetVisitorsNewOrder", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblNewAccountNewOrder").show();
                        $("#spnNewAccountNewOrder").html('');
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.TotalCustomer + "</td>";
                            tr += "<td>" + item.TotalOrder + "</td>";
                            tr += "</tr>";
                            var rate = parseFloat((item.TotalOrder / item.TotalCustomer) * 100).toFixed(2);
                            if (rate == 'NaN') {
                                rate = '0.00';
                            }
                            $("#lblNewAccountNewOrder").html(rate + '%');


                            drawPieChartVisitorsNewOrder(item.TotalCustomer, item.TotalOrder);
                        });
                        $("#tblNewAccountNewOrder tr:gt(0)").remove();
                        $("#tblNewAccountNewOrder").append(tr);
                    } else {
                        $("#tblNewAccountNewOrder").hide();
                        $("#spnNewAccountNewOrder").html(getLocale(AspxAdminDashBoard, "No Records Found!"));

                    }

                }, null);

            };

            var init = function () {
                getVisitorNewAccountStatics();
                getVisitorsOrder();
                getVisitorsNewOrder();
                $("#ddlVisitorAccount").bind("change", function () {
                    getVisitorNewAccountStatics();
                });
                $("#ddlVisitorOrder").bind("change", function () {
                    getVisitorsOrder();
                });
                $("#ddlNewAccountNewOrder").bind("change", function () {
                    getVisitorsNewOrder();
                });
                var $tabs = $('#container-x').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
                $tabs.tabs('option', 'active', 0);
            };
            return { Init: init };
        }();
        visitor.Init();

    });
</script>
<div class="cssClassTabPanelTable sfPriMrg-t">
    <div id="container-x">
        <ul>
            <li><a href="#fragment-1">
                <asp:Label ID="lblVisitor" runat="server" Text="Order By All Visitors " meta:resourcekey="lblVisitorResource1"></asp:Label>
            </a></li>
            <li><a href="#fragment-2">
                <asp:Label ID="lblNewAccount" runat="server"
                    Text="New Account By Visitor" meta:resourcekey="lblNewAccountResource1"></asp:Label>
            </a></li>
            <li><a href="#fragment-3">
                <asp:Label ID="lblNewAccountOrder" runat="server"
                    Text="Order By New Account" meta:resourcekey="lblNewAccountOrderResource1"></asp:Label>
            </a></li>
        </ul>


        <div id="fragment-1" style="padding: 0;">
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                </div>
                <div class="sfFormwrapper sfPadding">
                    <div class="classTableWrapperOuter">
                        <div class="sfTimeSelection sfTableOption">
                            <label class="sfLabel sfLocale">
                                Select Time:</label>
                            <label>
                                <select id="ddlVisitorOrder" class="sfSelect reportTrigger">
                                    <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                                    <option value="7" class="sfLocale">Last 7 Days</option>
                                    <option value="30" class="sfLocale">Last 30 Days</option>
                                    <option value="365" class="sfLocale">Last 365 Days</option>
                                </select></label>
                        </div>
                        <div class="sfStatusTable sfGridwrapper">
                            <table style="display: none;" class="classTableWrapper" cellspacing="0" cellpadding="0" width="100%" border="0" id="tblVisitorOrder">

                                <tr class="cssClassHeading ">
                                    <td class="sfLocale">S.N
                                    </td>
                                    <td class="sfLocale">Total Visitor
                                    </td>
                                    <td class="sfLocale">Total Order
                                    </td>
                                </tr>
                            </table>
                            <span class="sfError" id="spnVisitorOrder"></span>
                        </div>
                        <div class="chart-wrapper">
                            <div id="dvVisitorOrder" style="display: none; width: 350px; height: 250px;"></div>
                        </div>
                        <div class="sfHighlight">
                            <label class="sflabel sfLocale">
                                All Visitor To Order Rate:</label><br />
                            <strong>
                                <label class="sflabel" id="lblVisitorOrderRate">
                                    0%</label></strong>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="fragment-2" style="padding: 0;">
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                </div>
                <div class="sfFormwrapper sfPadding">
                    <div class="classTableWrapperOuter">
                        <div class="sfTimeSelection sfTableOption">
                            <label class="sfLabel sfLocale">
                                Select Time :</label>
                            <label>
                                <select id="ddlVisitorAccount" class="sfSelect reportTrigger">
                                    <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                                    <option value="7" class="sfLocale">Last 7 Days</option>
                                    <option value="30" class="sfLocale">Last 30 Days</option>
                                    <option value="365" class="sfLocale">Last 365 Days</option>
                                </select></label>
                        </div>
                        <div class="sfStatusTable sfGridwrapper">
                            <table style="display: none;" class="classTableWrapper" cellspacing="0" cellpadding="0" width="100%" border="0" id="tblVisitorNewAccount">

                                <tr class="cssClassHeading ">
                                    <td class="sfLocale">S.N
                                    </td>
                                    <td class="sfLocale">Total Visitor
                                    </td>
                                    <td class="sfLocale">Account Created
                                    </td>
                                </tr>

                            </table>
                            <span class="sfError" id="spnVisitorNewAccount"></span>
                            <div>
                            </div>

                        </div>
                        <div class="chart-wrapper">
                            <div id="dvVisitorAccount" style="display: none; width: 400px; height: 250px;"></div>
                        </div>
                        <div class="sfHighlight">
                            <label class="sfLabel sfLocale">
                                Account Created By Visitor Rate:</label><br />
                            <strong>
                                <label class="sfLabel" id="lblVisitorAccount">
                                    0%</label></strong>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="fragment-3" style="padding: 0;">
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                </div>
                <div class="sfFormwrapper sfPadding">
                    <div class="classTableWrapperOuter">
                        <div class="sfTimeSelection sfTableOption">
                            <label class="sfLabel sfLocale">
                                Select Time:</label>
                            <label>
                                <select id="ddlNewAccountNewOrder" class="sfSelect reportTrigger">
                                    <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                                    <option value="7" class="sfLocale">Last 7 Days</option>
                                    <option value="30" class="sfLocale">Last 30 Days</option>
                                    <option value="365" class="sfLocale">Last 365 Days</option>
                                </select></label>
                        </div>
                        <div class="sfStatusTable sfGridwrapper">
                            <table style="display: none;" class="classTableWrapper" cellspacing="0" cellpadding="0" width="100%" border="0" id="tblNewAccountNewOrder">

                                <tr class="cssClassHeading ">
                                    <td class="sfLocale">S.N
                                    </td>
                                    <td class="sfLocale">Total New Account
                                    </td>
                                    <td class="sfLocale">Total Order
                                    </td>
                                </tr>

                            </table>
                            <span class="sfError" id="spnNewAccountNewOrder"></span>
                            <div>
                            </div>

                        </div>
                        <div class="chart-wrapper">
                            <div id="dvNewAccountNewOrder" style="display: none; width: 400px; height: 250px;"></div>
                        </div>
                        <div class="sfHighlight">
                            <label class="sfLabel sfLocale">
                                New Account's Order Rate:</label><br />
                            <strong>
                                <label class="sfLabel" id="lblNewAccountNewOrder">
                                    0%</label></strong>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</div>
