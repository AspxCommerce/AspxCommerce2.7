<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomersReviews.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemRatingManagement_CustomersReviews" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
        moduleKey: AspxItemRatingManagement
        });
    });

        var lblCRHeading='<%=lblCRHeading.ClientID %>';
    var lblReviewsFromHeading = '<%=lblReviewsFromHeading.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divCustomerReviews">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReviewHeading" runat="server" Text="Customers Reviews" meta:resourcekey="lblReviewHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCustomerReview_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxCustomerItemReviewImage2" src="" alt="loading...." class="sfLocale" />
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerReviews" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CustomerReviewExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divShowCustomerReviewList" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCRHeading" runat="server" meta:resourcekey="lblCRHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnBackCustomerReviews" class="sfBtn">
                            <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportReviewsToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button2_Click" Text="Export to Excel" meta:resourcekey="btnExportReviewsToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportReviews" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCustomerReviewDetail_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportReviewsResource1" />
                    </p>
                    
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Nick Name:</label>
                                <input type="text" id="txtSearchUserName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Status:</label>
                                <select id="ddlStatus" class="sfListmenu">
                                    <option value="" class="sfLocale">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Item Name:</label>
                                <input type="text" id="txtsearchItemNm" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                
                                        <button type="button" onclick="CustomerReviews.SearchItemRatings()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                    
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerItemReviewImage1" src="" alt="loading...." class="sfLocale" />
                </div>
                <div class="log">
                </div>
                <table id="gdvShowCustomerReviewList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ShowCustomerReviewListExportTbl" width="100%" border="0" cellpadding="0"
                    cellspacing="0" style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnReviews" runat="server" />
<div id="divCustomerItemRatingForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReviewsFromHeading" runat="server" meta:resourcekey="lblReviewsFromHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <table border="0" id="tblEditReviewForm" class="cssClassPadding">
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Item:</label>
                    </td>
                    <td>
                        <label id="lnkItemNames" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr id="trPostedBy">
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Posted By:</label>
                    </td>
                    <td>
                        <label id="lblPostedBy" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr id="trViewedIP">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            View IP:</label>
                    </td>
                    <td>
                        <label id="lblViewFromIP" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr id="trSummaryRating">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Summary Rating:</label>
                    </td>
                    <td>
                        <div id="divAverageRating">
                        </div>
                        <span class="cssClassRatingTitle"></span>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Detailed Rating:</label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingCriteria">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Nick Name:</label>
                    </td>
                    <td>
                        <input type="text" id="txtNickName" class="sfInputbox " />
                    </td>
                </tr>
                <tr id="trAddedOn">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Added On:</label>
                    </td>
                    <td>
                        <label id="lblAddedOn">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Summary Of Review:</label>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="sfInputbox " />
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Review:</label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" class="cssClassTextArea"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Status:</label>
                    </td>
                    <td>
                        <select id="selectStatus" class="sfListmenu">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnReviewBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnUser" />
<input type="hidden" id="hdnItemReviewID" />
<asp:HiddenField ID="_csvCustomerReviewHiddenValue" runat="server" />
<asp:HiddenField ID="_csvCustomerReviewDetailValue" runat="server" />
