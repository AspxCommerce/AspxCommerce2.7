<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Refunds.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminDashBoard_Refunds" %>

<script type="text/javascript">

    $(document).ready(function () {
        $(".sfLocale").localize({
            moduleKey: AspxAdminDashBoard
        });
    });

    $(function () {
        var refund = function () {

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
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/' + method,
                    data: param,
                    dataType: "json",
                    success: successFx,
                    error: error
                });
            };
            var getTotalRefund = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var day = $.trim($("#ddlRefund").val());
                var param = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetTotalRefund", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblRefund").show();
                        $("#spnNoData").html('');
                        $("#lblTotalRefundCount").html(data.d.length);
                        var total = 0;
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.RefundAmount + "</td>";
                            tr += "<td>" + item.ShippingCost + "</td>";
                            tr += "<td>" + item.OtherPostalCharges + "</td>";
                            tr += "<td>" + item.TotalRefundAmount + "</td>";
                            tr += "</tr>";
                            total += item.TotalRefundAmount;
                        });
                        $("#tblRefund tr:gt(0)").remove();
                        $("#lblTotalRefundecAmount").html(total);
                        $("#tblRefund>tbody").append(tr);
                        $("#spnNoData").html('');
                        $("#tblRefund > tbody tr:even").addClass("sfEven");
                        $("tblRefund > tbody tr:odd").addClass("sfOdd");
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    } else {
                        $("#tblRefund").hide();
                        $("#lblTotalRefundCount").html(0);
                        $("#lblTotalRefundecAmount").html(0);
                        $("#spnNoData").html(getLocale(AspxAdminDashBoard, "No Records Found!"));
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    }

                }, null);
            };
            var getTopRefundReason = function () {
                var aspxCommonInfo = aspxCommonObj();
                aspxCommonInfo.UserName = null;
                var day = $.trim($("#ddlRefund").val());
                var param = JSON2.stringify({ day: day, aspxCommonObj: aspxCommonInfo });
                $ajaxCall("GetTopRefundReason", param, function (data) {
                    if (data.d.length > 0) {
                        var tr = "";
                        $("#tblRefundReason").show();
                        $.each(data.d, function (index, item) {
                            tr += "<tr>";
                            tr += "<td>" + parseInt(index + 1) + "</td>";
                            tr += "<td>" + item.ReturnReasonAliasName + "</td>";
                            tr += "<td>" + item.TotalReason + "</td>";
                            tr += "</tr>";

                        });
                        $("#tblRefundReason tr:gt(0)").remove();
                        $("#tblRefundReason").append(tr);
                        $("#spnRefundReason").html('');
                        $("#tblRefundReason > tbody tr:even").addClass("sfEven");
                        $("tblRefundReason > tbody tr:odd").addClass("sfOdd");

                    } else {
                        $("#tblRefundReason").hide();
                        $("#spnRefundReason").html(getLocale(AspxAdminDashBoard, "No Records Found!"));
                    }

                }, null);
            };


            var init = function () {
                getTotalRefund();
                getTopRefundReason();
                $("#ddlRefund").bind("change", function () {
                    getTotalRefund();
                    getTopRefundReason();
                });
            };
            return { Init: init };
        }();
        refund.Init();

    });
</script>

<div>
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblRefund" runat="server" CssClass="cssClassLabel"
                    Text="Refund" meta:resourcekey="lblRefundResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <div class="classTableWrapper">
                <div class="">
                    <label class="sfLabel sfLocale">
                        Select Time :</label>
                    <label>
                        <select id="ddlRefund" class="sfSelect reportTrigger">
                            <option value="1" selected="selected" class="sfLocale">Last 24 Hours</option>
                            <option value="7" class="sfLocale">Last 7 Days</option>
                            <option value="30" class="sfLocale">Last 30 Days</option>
                            <option value="365" class="sfLocale">Last 365 Days</option>
                        </select></label>
                </div>
                <div class="sfHighlight">
                    <label class="sfLabel sfLocale">
                        Total Refunded Item No:
                    </label>
                    <strong>
                        <label class="sfLabel" id="lblTotalRefundCount">
                            0
                        </label>
                    </strong>
                </div>
                <div>
                    <table style="display: none;" cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRefund">
                        <tr class="cssClassHeading ">
                            <td class="sfLocale">S.N
                            </td>
                            <td class="sfLocale">RefundAmount
                            </td>
                            <td class="sfLocale">ShippingCost
                            </td>
                            <td class="sfLocale">OtderPostalCharges
                            </td>
                            <td class="sfLocale">TotalRefundAmount
                            </td>
                        </tr>
                    </table>
                    <span id="spnNoData" class="sfError"></span>
                </div>
                <div class="sfHighlight">
                    <label class="sfLabel sfLocale">
                        Total Refunded Amount:</label>
                    <strong>
                        <label id="lblTotalRefundecAmount" class="sfLabel cssClassFormatCurrency">
                            0</label></strong>
                </div>
                <div>
                    <label class="sfLabel sfLocale">
                        Top 5 refund reason:</label>
                    <table style="display: none;" cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRefundReason">
                        <tr class="cssClassHeading ">
                            <td class="sfLocale">S.N
                            </td>
                            <td class="sfLocale">Refund Reason
                            </td>
                            <td class="sfLocale">Total Reason
                            </td>
                        </tr>
                    </table>
                    <span class="sfError" id="spnRefundReason"></span>
                </div>
            </div>
        </div>
    </div>
</div>
