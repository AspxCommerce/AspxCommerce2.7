<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryQuantity.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_CategoryQuantity" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxAdminDashBoard
        });
        var categoryStatic = function () {

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
                    url: aspxservicePath + "AspxCoreHandler.ashx/" + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            var drawPieChartRevenue = function (list) {
                $('#dvCatRev').show();
                $("#dvCatRev").html('');
                var datas = [];
                $.each(list, function (index, item) {
                    var arr = [];
                    arr.push(item.CategoryName, item.TotalAmount);
                    datas.push(arr);
                });
                var data = datas;
                if (data.length > 0) {
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot('dvCatRev', [data], {
                        title: '', seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f' } }, legend: { show: true }, cursor: { show: false },
                        seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                        grid: { drawGridLines: true, gridLineColor: '#cccccc', background: 'transparent', borderColor: '#ff0000', borderWidth: 0, shadow: false, shadowAngle: 0, shadowOffset: 1.5, shadowWidth: 3, shadowDepth: 3 }


                    });
                }
            };
            var getHighestRevenueCategory = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var day = $.trim($("#ddlCatRev").val());
                var param = JSON2.stringify({ top: 3, day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetTopCategoryByHighestRevenue", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        var categories = 0;
                        var totalAmount = 0;
                        $("#tblCatRevenue").show();
                        $("#spnCategoryRevenue").html('');
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.CategoryName + "</td>";
                            tr += "<td><label class='cssClassLabel cssClassFormatCurrency'>" + item.TotalAmount + "</label></td>";
                            tr += "</tr>";
                        });

                        $("#tblCatRevenue tr:gt(0)").remove();
                        $("#tblCatRevenue").append(tr);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $("#tblCatRevenue > tbody tr:even").addClass("sfEven");
                        $("tblCatRevenue > tbody tr:odd").addClass("sfOdd");

                        drawPieChartRevenue(data.d);

                    } else {
                        $("#dvCatRev").html('');
                        $("#tblCatRevenue").hide();
                        $("#spnCategoryRevenue").html(getLocale(AspxAdminDashBoard, "No Records Found!"));

                    }

                }, null);
            };
            var drawPieChartTopCatBySell = function (list) {
                $('#dvCatSellReport').show();
                $("#dvCatSellReport").html('');
                var datas = [];
                $.each(list, function (index, item) {
                    var arr = [];
                    arr.push(item.CategoryName, item.TotalItemQuantity);
                    datas.push(arr);
                });
                var data = datas;
                if (data.length > 0) {
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot.config.enablePlugins = true;
                    jQuery.jqplot('dvCatSellReport', [data], {
                        title: '', seriesDefaults: { shadow: true, renderer: jQuery.jqplot.PieRenderer, rendererOptions: { showDataLabels: true, dataLabelFormatString: '%.2f' } }, legend: { show: true }, cursor: { show: false },
                        seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                        grid: { drawGridLines: true, gridLineColor: '#cccccc', background: 'transparent', borderColor: '#ff0000', borderWidth: 0, shadow: false, shadowAngle: 0, shadowOffset: 1.5, shadowWidth: 3, shadowDepth: 3 }

                    });
                }
            };
            var getTopCategoryByItemSell = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var day = $.trim($("#ddlCatSellReportType").val());
                var param = JSON2.stringify({ top: 3, day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetTopCategoryByItemSell", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblTopCategoryBySell").show();
                        $("#spnCategorySell").html('');
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.CategoryName + "</td>";
                            tr += "<td>" + item.TotalItemQuantity + "</td>";
                            tr += "</tr>";
                        });
                        $("#tblTopCategoryBySell tr:gt(0)").remove();
                        $("#tblTopCategoryBySell").append(tr);
                        $("#tblTopCategoryBySell > tbody tr:even").addClass("sfEven");
                        $("tblTopCategoryBySell > tbody tr:odd").addClass("sfOdd");
                        drawPieChartTopCatBySell(data.d);
                    } else {
                        $("#dvCatSellReport").html('');
                        $("#tblTopCategoryBySell").hide();
                        $("#spnCategorySell").html(getLocale(AspxAdminDashBoard, "No Records Found!"));

                    }

                }, null);
            };
            var init = function () {
                getHighestRevenueCategory();
                getTopCategoryByItemSell();
                $("#ddlCatSellReportType").bind("change", function () {
                    getTopCategoryByItemSell();
                });
                $("#ddlCatRev").bind("change", function () {
                    getHighestRevenueCategory();
                });
                var $tabs = $('#container-10').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
                $tabs.tabs('option', 'active', 0);

            };
            return { Init: init };
        }();
        categoryStatic.Init();

    });

</script>

<div class="cssClassTabPanelTable">
    <div id="container-10">
        <ul>
            <li><a href="#fragment-10">
                <asp:Label ID="lblTopCategorySell" runat="server"
                    Text="s" meta:resourcekey="lblTopCategorySellResource1"></asp:Label>
            </a></li>
            <li><a href="#fragment-11">
                <asp:Label ID="lblTopCategoryByRevenue" runat="server"
                    Text="Top Category By Revenue" meta:resourcekey="lblTopCategoryByRevenueResource1"></asp:Label>
            </a></li>
        </ul>
        <div id="fragment-10" style="padding: 10px 0 0;">
            <div class="cssClassCommonBox Curve">
                <div class="sfFormwrapper sfPadding">
                    <div class="classTableWrapperOuter">
                        <div class="sfTimeSelection sfTableOption">
                            <label class="sfLabel sfLocale">
                                Select Time:</label>
                            <label>
                                <select id="ddlCatSellReportType" class="sfSelect reportTrigger">
                                    <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                                    <option value="7" class="sfLocale">Last 7 Days</option>
                                    <option value="30" class="sfLocale">Last 30 Days</option>
                                    <option value="365" class="sfLocale">Last 365 Days</option>
                                </select></label>
                        </div>
                        <div class="sfStatusTable sfGridwrapper">
                            <table style="display: none;" class="classTableWrapper" cellspacing="0" cellpadding="0"
                                width="100%" border="0" id="tblTopCategoryBySell">
                                <tr class="cssClassHeading ">
                                    <td class="sfLocale">S.N
                                    </td>
                                    <td class="sfLocale">Category Name
                                    </td>
                                    <td class="sfLocale">Total Ordered Item Quantity
                                    </td>
                                </tr>
                            </table>
                            <span class="errorMsg sfLocale" id="spnCategorySell"></span>
                        </div>
                        <div class="chart-wrapper">
                            <div id="dvCatSellReport" style="display: none; width: 350px; height: 250px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="fragment-11" style="padding: 10px 0 0;">
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                </div>
                <div class="sfFormwrapper sfPadding">
                    <div class="classTableWrapperOuter">
                        <div class="sfTimeSelection sfTableOption">
                            <label class="sfLabel sfLocale">
                                Select Time :</label>
                            <label>
                                <select id="ddlCatRev" class="sfSelect reportTrigger">
                                    <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                                    <option value="7" class="sfLocale">Last 7 Days</option>
                                    <option value="30" class="sfLocale">Last 30 Days</option>
                                    <option value="365" class="sfLocale">Last 365 Days</option>
                                </select></label>
                        </div>
                        <div class="sfStatusTable sfGridwrapper">
                            <table style="display: none;" class="classTableWrapper" cellspacing="0" cellpadding="0"
                                width="100%" border="0" id="tblCatRevenue">
                                <tr class="cssClassHeading ">
                                    <td class="sfLocale">S.N
                                    </td>
                                    <td class="sfLocale">Category Name
                                    </td>
                                    <td class="sfLocale">Total Revenue
                                    </td>
                                </tr>
                            </table>
                            <span class="sfErrormsg sfLocale" id="spnCategoryRevenue"></span>
                        </div>
                        <div class="chart-wrapper">
                            <div id="dvCatRev" style="display: none; width: 400px; height: 250px;">
                            </div>
                        </div>
                        <p class="sfClassNote">
                            <span class="sfLocale sfClassBold">Note:</span> <span class="sfLocale">Revenue is calculated from Net product purchased price(without discounts) and quantity.</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
