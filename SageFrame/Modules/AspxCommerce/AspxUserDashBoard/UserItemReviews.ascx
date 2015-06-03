<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserItemReviews.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserProductReviews" %>

<script type="text/javascript">
    //<![CDATA[
    var UserItemReviews = "";
    aspxCommonObj.UserName = AspxCommerce.utils.GetUserName();
    $(function() {

        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });

        UserItemReviews = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: moduleRootPath,                url: "",
                method: "",
                ajaxCallMode: "",
                error: ""
            },

            ajaxCall: function(config) {
                $.ajax({
                    type: UserItemReviews.config.type,
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", userModuleIDUD);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: UserItemReviews.config.contentType,
                    cache: UserItemReviews.config.cache,
                    async: UserItemReviews.config.async,
                    data: UserItemReviews.config.data,
                    dataType: UserItemReviews.config.dataType,
                    url: UserItemReviews.config.url,
                    success: UserItemReviews.config.ajaxCallMode,
                    error: UserItemReviews.config.error
                });
            },

            init: function() {
                UserItemReviews.BindReviewsAndRatingsGridByUser();
                UserItemReviews.ShowGridTable();

                if (aspxCommonObj.UserName.toLowerCase() != "superuser") {
                    $("#lblUpdateRebiew").hide();
                }

                var v = $("#form1").validate({
                    messages: {
                        name: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + ""
                        },
                        summary: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + ""
                        },
                        review: {
                            required: '*',
                            minlength: "*"
                        }
                    }
                });

                $('#btnUpdateReview').click(function () {                    
                    if (v.form()) {
                        UserItemReviews.UpdateItemRatingsByUser();
                        UserItemReviews.BindRatingCriteria();
                        UserItemReviews.ShowGridTable();
                        return false;
                    }
                    else {
                        return false;
                    }
                });

                $('#btnDeleteSelected').click(function() {
                    var itemReview_ids = '';
                                        $(".itemRatingChkbox").each(function(i) {
                        if ($(this).prop("checked")) {
                            itemReview_ids += $(this).val() + ',';
                        }
                    });
                    if (itemReview_ids != "") {
                        var properties = {
                            onComplete: function(e) {
                                UserItemReviews.ConfirmDeleteMultipleItemRating(itemReview_ids, e);
                            }
                        }
                        csscody.confirm("<h1>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h1><p>" + getLocale(AspxUserDashBoard, "Do you want to delete all selected items?") + "</p>", properties);
                    } else {
                        csscody.alert("<h1>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h1><p>" + getLocale(AspxUserDashBoard, "You need to select at least one item before you can do this.") + "<br/>" + getLocale(AspxUserDashBoard, "To select one or more items, just check the box before each item.") + "</p>");
                    }
                    return false;
                });

                $("#btnReviewBack").click(function() {
                    UserItemReviews.ShowGridTable();
                    return false;
                });

                $("#btnDeleteReview").click(function() {
                    var review_id = $(this).attr("name");
                    var properties = { onComplete: function(e) {
                        if (e) {
                            UserItemReviews.ConfirmSingleDeleteItemReview(review_id, e);
                        } else {
                            return false;
                        }
                    }
                    }
                    csscody.confirm("<h1>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h1><p>" + getLocale(AspxUserDashBoard, "Do you want to delete this item rating and review?") + "</p>", properties);
                    return false;
                });
            },

            ShowGridTable: function() {
                UserItemReviews.HideAll();
                $("#divShowItemRatingDetails").show();
            },

            BindReviewsAndRatingsGridByUser: function() {
                             var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvReviewsNRatings_pagesize").length > 0) ? $("#gdvReviewsNRatings_pagesize :selected").text() : 10;

                $("#gdvReviewsNRatings").sagegrid({
                    url: this.config.baseURL + "UserDashBoardHandler.ashx/",
                    functionMethod: "GetUserReviewsAndRatings",
                    colModel: [
                    { display: 'ItemReviewID', name: 'itemreview_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemRatingChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Nick Name'), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxUserDashBoard, 'Total Rating Average'), name: 'total_rating_average', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxUserDashBoard, 'View IP'), name: 'view_from_IP', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: getLocale(AspxUserDashBoard, 'Review Summary'), name: 'review_summary', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Review', name: 'review', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Status'), name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'link', align: 'left', url: 'item', queryPairs: '12', showpopup: false, popupid: '', poparguments: '', popupmethod: '' },
                    { display: getLocale(AspxUserDashBoard, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Added By', name: 'AddedBy', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Status ID', name: 'status_id', cssclass: '', controlclass: 'cssClassHeadNumber', coltype: 'label', align: 'left', hide: true },
                    { display: 'Item SKU', name: 'item_SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				],

                    buttons: [{ display: getLocale(AspxUserDashBoard, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'UserItemReviews.EditReviewsNRatings', arguments: '1,2,3,4,5,6,7,8,9,11,12' },
    			          { display: getLocale(AspxUserDashBoard, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'UserItemReviews.DeleteReviewsNRatings', arguments: '' }
    			    ],
                    rp: perpage,
                    nomsg: getLocale(AspxUserDashBoard, "No Records Found!"),
                    param: { aspxCommonObj: aspxCommonObj },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 1: { sorter: false }, 13: { sorter: false} }
                });
            },

            EditReviewsNRatings: function(tblID, argus) {
                switch (tblID) {
                    case "gdvReviewsNRatings":
                        ReviewID = argus[0];
                        status = argus[9];
                        UserItemReviews.ClearReviewForm();
                        UserItemReviews.BindItemReviewDetails(argus);
                        UserItemReviews.BindRatingSummary(argus[0]);
                        UserItemReviews.HideAll();
                        $("#divItemRatingForm").show();
                        break;
                    default:
                        break;
                }
            },

            BindItemReviewDetails: function(argus) {
                UserItemReviews.BindRatingCriteria();
                $("#btnDeleteReview").attr("name", argus[0]);
                $("#lnkItemName").html(argus[10]);
                $("#lnkItemName").attr("href", aspxRedirectPath + "item/" + argus[13] + pageExtension);
                $("#lnkItemName").attr("name", argus[3]);
                $("#lblViewFromIP").html(argus[6]);
                $("#txtNickName").val(argus[4]);
                $("#lblAddedOn").html(argus[11]);
                $("#txtSummaryReview").val(argus[7]);
                $("#txtReview").val(argus[8]);
                $("#lblStatus").html(argus[9]);
                if (argus[9].toLowerCase() != "pending") {
                    $("#txtSummaryReview").attr('disabled', 'disabled');
                    $("#txtReview").attr('disabled', 'disabled');
                    $('#lblUpdateRebiew').hide();
                }
                else {
                    $("#txtSummaryReview").removeAttr('disabled');
                    $("#txtReview").removeAttr('disabled');
                    $("#txtNickName").removeAttr('disabled');
                    $('#lblUpdateRebiew').show();
                }
            },

            BindRatingSummary: function (review_id) {
                this.config.method = "AspxCoreHandler.ashx/GetItemRatingByReviewID";
                this.config.url = aspxservicePath + this.config.method;                
                this.config.data = JSON2.stringify({ itemReviewID: review_id, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = UserItemReviews.BindItemRatingByReviewID;
                this.ajaxCall(this.config);
            },

            BindStarRatingsDetails: function() {
                                $('.auto-submit-star').rating({
                    required: true,
                    focus: function(value, link) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var tip = $('#hover-test' + ratingCriteria_id);
                        tip[0].data = tip[0].data || tip.html();
                        tip.html(link.title || 'value: ' + value);
                        $("#tblRatingCriteria label.error").hide();
                    },
                    blur: function(value, link) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var tip = $('#hover-test' + ratingCriteria_id);
                        tip.html(tip[0].data || '');
                        $("#tblRatingCriteria label.error").hide();
                    },
                    callback: function(value, event) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var starRatingValues = $(this).attr("value");
                        var len = ratingCriteria_id.length;
                        var isAppend = true;
                        if (ratingValues != '') {
                            var stringSplit = ratingValues.split('#');
                            $.each(stringSplit, function(index, item) {
                                if (item.substring(0, item.indexOf('-')) == ratingCriteria_id) {
                                    var index = ratingValues.indexOf(ratingCriteria_id + "-");
                                    var toReplace = ratingValues.substr(index, 2 + len);
                                    ratingValues = ratingValues.replace(toReplace, ratingCriteria_id + "-" + value);
                                    isAppend = false;
                                }
                            });
                            if (isAppend) {
                                ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                            }
                        }
                        else {
                            ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                        }
                    }
                });
            },

            BindStarRatingAverage: function(itemAvgRating) {
                $("#divAverageRating").html('');
                var ratingStars = '';
                var ratingTitle = [getLocale(AspxUserDashBoard, "Worst"), getLocale(AspxUserDashBoard, "Ugly"), getLocale(AspxUserDashBoard, "Bad"), getLocale(AspxUserDashBoard, "Not Bad"), getLocale(AspxUserDashBoard, "Average"), getLocale(AspxUserDashBoard, "OK"), getLocale(AspxUserDashBoard, "Nice"), getLocale(AspxUserDashBoard, "Good"), getLocale(AspxUserDashBoard, "Best"), getLocale(AspxUserDashBoard, "Excellent")];                 var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
                var i = 0;
                ratingStars += '<div class="cssClassToolTip">';
                for (i = 0; i < 10; i++) {
                    if (itemAvgRating == ratingText[i]) {
                        ratingStars += '<input name="avgItemRating" type="radio" class="auto-star-avg {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                        $(".cssClassRatingTitle").html(ratingTitle[i]);
                    }
                    else {
                        ratingStars += '<input name="avgItemRating" type="radio" class="auto-star-avg {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                    }
                }
                ratingStars += '</div>';
                $("#divAverageRating").append(ratingStars);
            },

            DeleteReviewsNRatings: function(tblID, argus) {
                switch (tblID) {
                    case "gdvReviewsNRatings":
                        var properties = {
                            onComplete: function(e) {
                                UserItemReviews.ConfirmSingleDeleteItemReview(argus[0], e);
                            }
                        };
                        csscody.confirm("<h1" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h1><p>" + getLocale(AspxUserDashBoard, "Do you want to delete this item rating and review?") + "</p>", properties);
                        break;
                    default:
                        break;
                }
            },

            ConfirmSingleDeleteItemReview: function(itemReviewID, event) {
                if (event) {
                    this.config.method = "AspxCoreHandler.ashx/DeleteSingleItemRating";
                    this.config.url = aspxservicePath + this.config.method;                   
                    this.config.data = JSON2.stringify({ itemReviewID: itemReviewID, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = UserItemReviews.BindReviewAndRatingOnDelete;
                    this.config.error = UserItemReviews.GetReviewDeleteErrorMsg;
                    this.ajaxCall(this.config);
                }
                return false;
            },

            ConfirmDeleteMultipleItemRating: function(itemReview_ids, event) {
                if (event) {
                    UserItemReviews.DeleteMultipleItemRating(itemReview_ids);
                }
            },

            DeleteMultipleItemRating: function (_itemReviewIds) {
                this.config.method = "AspxCoreHandler.ashx/DeleteMultipleItemRatings";
                this.config.url = aspxservicePath + this.config.method;               
                this.config.data = JSON2.stringify({ itemReviewIDs: _itemReviewIds, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = UserItemReviews.BindReviewOnMultipleDelete;
                this.config.error = UserItemReviews.GetDeleteMultipleMsg;
                this.ajaxCall(this.config);
                return false;
            },

            HideAll: function() {
                $("#divShowItemRatingDetails").hide();
                $("#divItemRatingForm").hide();
            },

            BindRatingCriteria: function() {
                var functionName = '';
                var param = '';
                if (status.toLowerCase() == 'pending') {
                    functionName = "GetItemRatingCriteria";
                    param = JSON2.stringify({ aspxCommonObj: aspxCommonObj, isFlag: false });
                }
                else {
                    functionName = "GetItemRatingCriteriaByReviewID";
                    param = JSON2.stringify({ aspxCommonObj: aspxCommonObj, itemReviewID: ReviewID, isFlag: false });
                }
                this.config.method = "AspxCoreHandler.ashx/";
                this.config.url = aspxservicePath + this.config.method+functionName;
                               this.config.data = param;
                this.config.ajaxCallMode = UserItemReviews.BindItemRatingCriteria;
                this.ajaxCall(this.config);
            },

            RatingCriteria: function(item) {
                var ratingCriteria = '';
                ratingCriteria += '<tr><td class="cssClassRatingTitleName"><label class="cssClassLabel">' + item.ItemRatingCriteria + ':<span class="cssClassRequired">*</span></label></td><td>';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" value="1" title=' + getLocale(AspxUserDashBoard, "Worst") + ' />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" value="2" title=' + getLocale(AspxUserDashBoard, "Bad") + ' />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" value="3" title=' + getLocale(AspxUserDashBoard, "OK") + ' />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" value="4" title=' + getLocale(AspxUserDashBoard, "Good") + ' />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" value="5" title=' + getLocale(AspxUserDashBoard, "Best") + ' disabled="disabled"/>';
                ratingCriteria += '<span id="hover-test' + item.ItemRatingCriteriaID + '" class="cssClassHoverText"></span>';
                ratingCriteria += '<label for="star' + item.ItemRatingCriteriaID + '" class="error">Please rate for ' + item.ItemRatingCriteria + '</label></tr></td>';
                $("#tblRatingCriteria").append(ratingCriteria);
            },

            UpdateItemRatingsByUser: function() {
                var functionName = ''; var param = ''; var nickName = '';
                var summaryReview = ''; var review = ''; var itemId = ''; var itemReviewID = ''; var statusIs = '';
                if (status.toLocaleLowerCase() == "approved") {
                    functionName = "UpdateItemRatingByUser";
                    nickName = $("#txtNickName").val();
                    summaryReview = $("#txtSummaryReview").val();
                    review = $("#txtReview").val();
                    itemId = $("#lnkItemName").attr("name");
                    itemReviewID = $("#btnDeleteReview").attr("name");
                    var updateItemRatingObj = {
                        ReviewSummary: summaryReview,
                        Review: review,
                        ItemReviewID: itemReviewID,
                        ItemID: itemId,
                        UserName: nickName
                    };
                    param = JSON2.stringify({ updateItemRatingObj: updateItemRatingObj, aspxCommonObj: aspxCommonObj });
                }
                else {
                    functionName = "UpdateItemRating";
                    statusId = '2';
                    ratingValue = ratingValues;
                    nickName = $("#txtNickName").val();
                    summaryReview = $("#txtSummaryReview").val();
                    review = $("#txtReview").val();
                    itemId = $("#lnkItemName").attr("name");
                    itemReviewID = $("#btnDeleteReview").attr("name");
                    var ratingManageObj = {
                        ItemRatingCriteria: ratingValue,
                        StatusID: statusId,
                        ReviewSummary: summaryReview,
                        Review: review,
                        ItemReviewID: itemReviewID,
                        ItemID: itemId,
                        ViewFromIP: userIP,
                        viewFromCountry: countryName,
                        UserName: nickName
                    };
                    param = JSON2.stringify({ ratingManageObj: ratingManageObj, aspxCommonObj: aspxCommonObj });
                }
                this.config.url = aspxservicePath + "AspxCoreHandler.ashx/"+ functionName;
                
                this.config.data = param;
                this.config.ajaxCallMode = UserItemReviews.BindReviewOnUpdate;
                this.config.error = UserItemReviews.GetReviewUpdateErrorMsg;
                this.ajaxCall(this.config);
            },

            ClearReviewForm: function() {
                                $('.auto-submit-star').rating('drain');
                $('.auto-submit-star').removeAttr('checked');
                $('.auto-submit-star').rating('select', -1);
                $("#txtNickName").val('');
                $("#txtSummaryReview").val('');
                $("#txtReview").val('');
                $("label.error").hide();
            },

            BindItemRatingByReviewID: function(msg) {
                $("#tblRatingCriteria label.error").hide();
                var itemAvgRating = '';
                var item;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    if (index == 0) {
                        UserItemReviews.BindStarRatingsDetails();
                        UserItemReviews.BindStarRatingAverage(item.RatingAverage);
                        itemRatingAverage = item.RatingAverage;
                    }
                    itemAvgRating = JSON2.stringify(item.RatingValue);
                    $('input.item-rating-crieteria' + item.ItemRatingCriteriaID).rating('select', itemAvgRating);
                    if (status.toLowerCase() == "approved") {
                        $('input.item-rating-crieteria' + item.ItemRatingCriteriaID).rating('disable');
                        $("#txtNickName").attr('disabled', 'disabled');
                    }
                };
                $('input.auto-star-avg').rating();
            },

            BindReviewAndRatingOnDelete: function() {
                csscody.info('<h2>' + getLocale(AspxUserDashBoard, "Information Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "This review has been deleted successfully.") + '</p>');
                UserItemReviews.BindReviewsAndRatingsGridByUser();
                UserItemReviews.ShowGridTable();
            },

            BindReviewOnMultipleDelete: function() {
                csscody.info('<h2>' + getLocale(AspxUserDashBoard, "Information Message") + '</h2><p>' + getLocale(AspxUserDashBoard, "This review has been deleted successfully.") + '</p>');
                UserItemReviews.BindReviewsAndRatingsGridByUser();
            },

            BindItemRatingCriteria: function(msg) {
                $("#tblRatingCriteria").html('');
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        UserItemReviews.RatingCriteria(item);
                    };
                }
                else {
                    csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "No criteria for rating found!") + "</p>");
                }
            },

            BindReviewOnUpdate: function() {
                csscody.info("<h2>" + getLocale(AspxUserDashBoard, "Information Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "This review has been updated successfully.") + "</p>");
                UserItemReviews.BindReviewsAndRatingsGridByUser();
                UserItemReviews.ClearReviewForm();
            },

            GetReviewDeleteErrorMsg: function() {
                csscody.error("<h2>" + getLocale(AspxUserDashBoard, "Error Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Failed to deleted!") + "</p>");
            },

            GetDeleteMultipleMsg: function() {
                csscody.error("<h2>" + getLocale(AspxUserDashBoard, "Error Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Failed to deleted!") + "</p>");
            },

            GetReviewUpdateErrorMsg: function() {
                csscody.error("<h2>" + getLocale(AspxUserDashBoard, "Error Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Failed to deleted!") + "</p>");
            }


        },
        UserItemReviews.init();
    });
    //]]>
</script>

<div id="divShowItemRatingDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span id="lblReviewNRatingGridHeading" class="sfLocale">All Reviews and Ratings</span>
            </h2>           

        </div>        
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxUserItemReviewImage" src="" class="sfLocale" alt="loading...." />
                </div>
             <div class="log">
                </div>
                
                <table class="sfGridWrapperTable" id="gdvReviewsNRatings" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                </div>
            </div>
                <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <label class="cssClassDarkBtn icon-delete">
                        <button type="button" id="btnDeleteSelected">
                            <span class="sfLocale">Delete All Selected</span></button>
                    </label>
                    
                </div>
            </div>
            
       
    </div>
</div>
<div class="cssClassBodyContentWrapper" id="divItemRatingForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <label id="lblFormTitle" class="sfLocale">Item's Rating and Review</label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblEditReviewForm"
                class="cssClassPadding">
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Item:</label>
                    </td>
                    <td>
                        <a href="#" id="lnkItemName" class="cssClassLabel"></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            View IP:</label>
                    </td>
                    <td>
                        <label id="lblViewFromIP" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
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
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Detailed Rating:</label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" id="tblRatingCriteria">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Nick Name:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtNickName" name="name" class="sfInputbox required"
                            minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Added On:</label>
                    </td>
                    <td>
                        <label id="lblAddedOn" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Summary Of Review:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="sfInputbox required"
                            minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Review:</label><span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextArea required"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel sfLocale">
                            Status:</label>
                    </td>
                    <td>
                        <label id="lblStatus" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper cssClassTMar30">
            <label class="cssClassDarkBtn i-arrow-left"><button type="button" id="btnReviewBack" class="sfBtn">
               <span class="sfLocale">Back</span></button></label>
             <label class="cssClassGreenBtn " id="lblUpdateRebiew"> <button type="submit" id="btnUpdateReview" class="sfBtn">
                <span class="sfLocale icon-send">Submit</span></button></label>
            <label class="cssClassGreenBtn i-delete"><button type="button" id="btnDeleteReview" class="sfBtn">
                <span class="sfLocale">Delete</span></button></label>
        </div>
    </div>
</div>
