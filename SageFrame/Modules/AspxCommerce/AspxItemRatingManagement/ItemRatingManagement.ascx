<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemRatingManagement.ascx.cs"
    Inherits="Modules_AspxItemRatingManagement_ItemRatingManagement" %>

<script type="text/javascript">

    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxItemRatingManagement
        });
    });
    var lblReviewsFromHeading='<%=lblReviewsFromHeading.ClientID %>';
    var newItemReviewRss = '<%=NewItemReviewRss %>'; 
    var rssFeedUrl = '<%=RssFeedUrl %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divShowItemRatingDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReviewsGridHeading" runat="server" 
                    Text="Comments and Reviews" meta:resourcekey="lblReviewsGridHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassRssDiv">
                <a href="#" class="cssRssImage" style="display: none">
                    <img id="itemReviewRssImage" alt="" src="" title="" />
                </a>
            </div>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" class="sfBtn" id="btnAddNewReview">
                           <span class="sfLocale icon-addnew">Add New Review/Rating</span>
                        </button>
                    </p>
                    <p>
                        <button type="button" class="sfBtn" id="btnDeleteSelected">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
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
                                <input type="text" id="txtSearchItemNme" class="sfTextBoxSmall" />
                            </td>
                            <td>
                               
                                        <button type="button" onclick="ItemRatingManage.SearchItemRatings()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                   
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemRatingMgmtImage" src="" alt="loading...." class="sfLocale"/>
                </div>
                <div class="log">
                </div>
                <table id="gdvReviewsNRatings" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divItemRatingForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewsFromHeading" runat="server" 
                    meta:resourcekey="lblReviewsFromHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table border="0" id="tblEditReviewForm" class="cssClassPadding" width="100%">
                <tr>
                    <td class="cssClassTableLeftCol" width="10%">
                        <label class="cssClassLabel sfLocale">
                            Item:</label>
                    </td>
                    <td>
                        <a href="#" id="lnkItemName" class="cssClassLabel"></a>
                        <select id="selectItemList" class="sfList sfListmenu required">
                          <option value="0">--All--</option>
                          
                        </select>
                          <a href="javascript:;" class="reloadMovie sfLocale">reload</a>
                    </td>
                </tr>
                <tr id="trUserList">
                    <td>
                        <label class="cssClassLabel sfLocale">
                            User Name:</label>
                    </td>
                    <td>
                        <select id="selectUserName" class="sfListmenu required">
                         <option value="0" selected="selected" class="sfLocale">--Select One--</option>
                        </select>
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
                        <input type="text" id="txtNickName" name="name" class="sfInputbox required"
                            minlength="2" /><span class="cssClassRequired">*</span>
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
                        <input type="text" id="txtSummaryReview" name="summary" class="sfInputbox required" /><span class="cssClassRequired">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel sfLocale">
                            Review:</label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextArea required"
                            onkeydown="limitMaxText(this);" onkeyup="limitMaxText(this);"></textarea><span class="cssClassRequired">*</span>
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
            <p>
                <button type="button" id="btnReset" class="sfBtn">
                    <span class="sfLocale icon-refresh">Reset</span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitReview" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
            <p>
                <button type="button" id="btnDeleteReview" class="sfBtn">
                    <span class="sfLocale icon-delete">Delete</span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnItemReview" />
