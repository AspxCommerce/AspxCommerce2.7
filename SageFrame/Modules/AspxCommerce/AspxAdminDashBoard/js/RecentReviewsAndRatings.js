var recentReviewsAndRatings = '';
$(function () {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var userIP = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();

    var ratingValues = '';
    var ItemsReview = [];
    var arrItemDetailsReviewList = new Array();
    var arrItemReviewList = new Array();
    var currentpage = 0;
    var rowTotal = 0;
    var itemReviewID = '';
    Array.prototype.IsNotExist = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) {
                return false;

            }
        }
        return true; btnDeleteReview
    };
    recentReviewsAndRatings = {
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
        ajaxCall: function (config) {
            $.ajax({
                type: recentReviewsAndRatings.config.type,
                contentType: recentReviewsAndRatings.config.contentType,
                cache: recentReviewsAndRatings.config.cache,
                async: recentReviewsAndRatings.config.async,
                url: recentReviewsAndRatings.config.url,
                data: recentReviewsAndRatings.config.data,
                dataType: recentReviewsAndRatings.config.dataType,
                success: recentReviewsAndRatings.ajaxSuccess,
                error: recentReviewsAndRatings.ajaxFailure
            });
        },
        pageselectCallback: function (page_index, jq, execute) {
                       if (execute) {
                $("#tblRatingPerUser").html('');

                var items_per_page = $("#ddlPageSize").val();
                var max_elem = Math.min((page_index + 1) * items_per_page, arrItemReviewList.length);

                               ItemsReview = [];

                for (var i = 0; i < max_elem; i++) {
                    recentReviewsAndRatings.BindAverageUserRating(arrItemReviewList[i]);
                    ItemsReview.push(arrItemReviewList[i].ItemReviewID);
                }
                $.each(arrItemDetailsReviewList, function (index, item) {

                    if (!ItemsReview.IsNotExist(item.ItemReviewID)) {
                        recentReviewsAndRatings.BindPerUserIndividualRatings(item.ItemReviewID, item.ItemRatingCriteria, item.RatingValue);
                    }
                });
                $("input.star-rate").rating();
                $("#tblRatingPerUser tr:even").addClass("sfOdd");
                $("#tblRatingPerUser tr:odd").addClass("sfEven");
            }
            return false;

        },
        getOptionsFromForm: function () {
            var opt = { callback: recentReviewsAndRatings.pageselectCallback };
            opt.items_per_page = $('#ddlPageSize').val();
            opt.current_page = currentpage;
            opt.callfunction = true,
            opt.function_name = { name: recentReviewsAndRatings.GetItemRatingPerUser, limit: $('#ddlPageSize').val() },
            opt.prev_text = "«";
            opt.next_text = "»";
            opt.prev_show_always = false;
            opt.next_show_always = false;
            return opt;
        },
        ClearReviewForm: function () {
                       $('.auto-submit-star').rating('drain');
            $('.auto-submit-star').removeAttr('checked');
            $('.auto-submit-star').rating('select', -1);

            $("#txtNickName").val('');
            $("#txtSummaryReview").val('');
            $("#txtReview").val('');
            $("label.error").hide();
        },
        HideAll: function () {
            $('#fade, #popuprel5').fadeOut();
        },
        RatingCriteria: function (item) {
            var ratingCriteria = '';
            ratingCriteria += '<tr><td class="cssClassRatingAdminTitleName"><label class="cssClassLabel">' + item.ItemRatingCriteria + ':<span class="cssClassRequired">*</span></label></td><td>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" title=' + getLocale(AspxAdminDashBoard, "Worst") + ' value="1" validate="required:true" />';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" title=' + getLocale(AspxAdminDashBoard, "Bad") + ' value="2"/>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" title=' + getLocale(AspxAdminDashBoard, "OK") + ' value="3"/>';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" title=' + getLocale(AspxAdminDashBoard, "Good") + ' value="4" />';
            ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star item-rating-crieteria' + item.ItemRatingCriteriaID + '" title=' + getLocale(AspxAdminDashBoard, "Best") + ' value="5" />';
            ratingCriteria += '<span id="hover-test' + item.ItemRatingCriteriaID + '"></span>';
            ratingCriteria += '<label for="star' + item.ItemRatingCriteriaID + '" class="error">' + getLocale(AspxAdminDashBoard, "Please rate for") + item.ItemRatingCriteria + '</label></tr></td>';
            $("#tblRatingCriteria").append(ratingCriteria);
        },
        BindRatingReviewStatusDropDown: function (item) {
            $("#selectStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
        },
        EditReview: function (reviewID) {
            recentReviewsAndRatings.BindReviewPopUp(reviewID);
            ShowPopupControl("popuprel5");
        },
        BindItemReviewDetails: function (item) {
            $("#lnkItemName").html(item.ItemName);
            $("#lnkItemName").attr("href", aspxRedirectPath + 'item/' + item.ItemSKU + pageExtension);
            $("#lnkItemName").attr("target", "_blank");
            $("#lnkItemName").attr("name", item.ItemID);
            $("#lblPostedBy").html(item.AddedBy);
            $("#lblViewFromIP").html(item.ViewFromIP);
            $("#txtNickName").val(item.Username);
            $("#lblAddedOn").html(item.AddedOn);
            $("#txtSummaryReview").val(Encoder.htmlDecode(item.ReviewSummary));
            $("#txtReview").val(Encoder.htmlDecode(item.Review));
            $("#lblViewFromIP").html(item.ViewFromIP);
            $("#selectStatus").val(item.StatusID);
        },
        BindStarRatingAverage: function (itemAvgRating) {
            $("#divAverageRating").html('');
            var ratingStars = '';
            var ratingTitle = [getLocale(AspxAdminDashBoard, "Worst"), getLocale(AspxAdminDashBoard, "Ugly"), getLocale(AspxAdminDashBoard, "Bad"), getLocale(AspxAdminDashBoard, "Not Bad"), getLocale(AspxAdminDashBoard, "Average"), getLocale(AspxAdminDashBoard, "OK"), getLocale(AspxAdminDashBoard, "Nice"), getLocale(AspxAdminDashBoard, "Good"), getLocale(AspxAdminDashBoard, "Best"), getLocale(AspxAdminDashBoard, "Excellent")];            var ratingText = [getLocale(AspxAdminDashBoard, "0.5"), getLocale(AspxAdminDashBoard, "1"), getLocale(AspxAdminDashBoard, "1.5"), getLocale(AspxAdminDashBoard, "2"), getLocale(AspxAdminDashBoard, "2.5"), getLocale(AspxAdminDashBoard, "3"), getLocale(AspxAdminDashBoard, "3.5"), getLocale(AspxAdminDashBoard, "4"), getLocale(AspxAdminDashBoard, "4.5"), getLocale(AspxAdminDashBoard, "5")];
            var i = 0;
            ratingStars += '<div class="cssClassToolTip">';
            for (i = 0; i < 10; i++) {
                if (itemAvgRating == ratingText[i]) {
                    ratingStars += '<input name="avgItemRating" type="radio" class="auto-star-avg {split:2}" disabled="disabled" value="' + ratingTitle[i] + '"  checked="checked" />';
                    $(".cssClassRatingTitle1").html(ratingTitle[i]);
                } else {
                    ratingStars += '<input name="avgItemRating" type="radio" class="auto-star-avg {split:2}" value="' + ratingTitle[i] + '" disabled="disabled"  />';
                }
            }
            ratingStars += '</div>';
            $("#divAverageRating").append(ratingStars);
        },
        BindStarRatingsDetails: function () {
            $.metadata.setType("attr", "validate");
            $('.auto-submit-star').rating({
                required: true,
                focus: function (value, link) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var tip = $('#hover-test' + ratingCriteria_id);
                    tip[0].data = tip[0].data || tip.html();
                    tip.html(link.title || 'value: ' + value);
                    $("#tblRatingCriteria label.error").hide();
                },
                blur: function (value, link) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var tip = $('#hover-test' + ratingCriteria_id);
                    tip.html(tip[0].data || '');
                    $("#tblRatingCriteria label.error").hide();
                },

                callback: function (value, event) {
                    var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                    var starRatingValues = $(this).attr("value");
                    var len = ratingCriteria_id.length;
                    var isAppend = true;
                    if (ratingValues != '') {
                        var stringSplit = ratingValues.split('#');
                        $.each(stringSplit, function (index, item) {
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
                    } else {
                        ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                    }
                }
            });
        },
        DeleteReview: function (obj) {
            var review_id = $(obj).val();           
            recentReviewsAndRatings.ConfirmSingleDeleteItemReview(review_id, e);               
           },
        BindItemsRatingByUser: function (item, index) {
            arrItemDetailsReviewList.push(item);
            if (ItemsReview.IsNotExist(item.ItemReviewID)) {
                ItemsReview.push(item.ItemReviewID);
                arrItemReviewList.push(item);
            }
        },
        BindStarRatingAveragePerUser: function (itemReviewID, itemAvgRating) {
            var ratingStars = '';
            var ratingTitle = [getLocale(AspxAdminDashBoard, "Worst"), getLocale(AspxAdminDashBoard, "Ugly"), getLocale(AspxAdminDashBoard, "Bad"), getLocale(AspxAdminDashBoard, "Not Bad"), getLocale(AspxAdminDashBoard, "Average"), getLocale(AspxAdminDashBoard, "OK"), getLocale(AspxAdminDashBoard, "Nice"), getLocale(AspxAdminDashBoard, "Good"), getLocale(AspxAdminDashBoard, "Best"), getLocale(AspxAdminDashBoard, "Excellent")];            var ratingText = [getLocale(AspxAdminDashBoard, "0.5"), getLocale(AspxAdminDashBoard, "1"), getLocale(AspxAdminDashBoard, "1.5"), getLocale(AspxAdminDashBoard, "2"), getLocale(AspxAdminDashBoard, "2.5"), getLocale(AspxAdminDashBoard, "3"), getLocale(AspxAdminDashBoard, "3.5"), getLocale(AspxAdminDashBoard, "4"), getLocale(AspxAdminDashBoard, "4.5"), getLocale(AspxAdminDashBoard, "5")];
            var i = 0;
            var ratingTitleText = '';
            ratingStars += '<div class="cssClassRatingStar"><div class="cssClassToolTip">';
            ratingStars += '<span class="cssClassRatingTitle cssClassUserRatingTitle_' + itemReviewID + '"></span>';
            for (i = 0; i < 10; i++) {
                if (itemAvgRating == ratingText[i]) {
                    ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" value="' + ratingTitle[i] + '"  checked="checked" />';
                    ratingTitleText = ratingTitle[i];
                } else {
                    ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" value="' + ratingTitle[i] + '" disabled="disabled" />';
                }
            }
            ratingStars += '<input type="hidden" value="' + ratingTitleText + '" id="hdnRatingTitle' + itemReviewID + '"></input><span class="cssClassToolTipInfo cssClassReviewId_' + itemReviewID + '"></span></div></div><div class="cssClassClear"></div>';
            return ratingStars;
        },

        BindAverageUserRating: function (item) {
            itemReviewID = item.ItemReviewID;
            var userRatings = '';
            userRatings += '<tr><td>';
            userRatings += '<div class="cssClassRateReviewWrapper clearfix"><div class="cssClassItemRating">';
            userRatings += '<div class="cssClassItemRatingBox">' + recentReviewsAndRatings.BindStarRatingAveragePerUser(item.ItemReviewID, item.RatingAverage) + '</div>';
            userRatings += '<div class="cssClassRatingReviewDate"><p> (Posted on <strong>' + formatDate(new Date(item.AddedOn), "yyyy/M/d hh:mm:ssa") + '</strong>)</p></div></div>';
            userRatings += '<div class="cssRatingDesWrap" style="word-wrap:break-word"><div class="cssClassRatingInfo"><p>' + (item.ReviewSummary) + '<span>' + getLocale(AspxAdminDashBoard, "Review by: ") + '<strong>' + item.Username + '</strong></span></p></div>';
            userRatings += '<div class="cssClassRatingReviewDesc"><p>' + (item.Review) + '</p></div></div>';
            userRatings += '</div><div class="sfButtonwrapper"><p><button type="button" id="btnEditReview" class="sfBtn" onclick="recentReviewsAndRatings.EditReview(' + item.ItemReviewID + ' )"><span class="icon-edit"></span></button></p></div></td></tr>';
            $("#tblRatingPerUser").append(userRatings);
            var ratingToolTip = $("#hdnRatingTitle" + item.ItemReviewID + "").val();
            $(".cssClassUserRatingTitle_" + item.ItemReviewID + "").html(ratingToolTip);
        },

        BindPerUserIndividualRatings: function (itemReviewID, itemRatingCriteria, ratingValue) {
            var userRatingStarsDetailsInfo = '';
            var ratingTitle = [getLocale(AspxAdminDashBoard, "Worst"), getLocale(AspxAdminDashBoard, "Ugly"), getLocale(AspxAdminDashBoard, "Bad"), getLocale(AspxAdminDashBoard, "Not Bad"), getLocale(AspxAdminDashBoard, "Average"), getLocale(AspxAdminDashBoard, "OK"), getLocale(AspxAdminDashBoard, "Nice"), getLocale(AspxAdminDashBoard, "Good"), getLocale(AspxAdminDashBoard, "Best"), getLocale(AspxAdminDashBoard, "Excellent")];            var ratingText = [getLocale(AspxAdminDashBoard, "0.5"), getLocale(AspxAdminDashBoard, "1"), getLocale(AspxAdminDashBoard, "1.5"), getLocale(AspxAdminDashBoard, "2"), getLocale(AspxAdminDashBoard, "2.5"), getLocale(AspxAdminDashBoard, "3"), getLocale(AspxAdminDashBoard, "3.5"), getLocale(AspxAdminDashBoard, "4"), getLocale(AspxAdminDashBoard, "4.5"), getLocale(AspxAdminDashBoard, "5")];
            var i = 0;
            userRatingStarsDetailsInfo += '<div class="cssClassToolTipDetailInfo">';
            userRatingStarsDetailsInfo += '<span class="cssClassCriteriaTitle">' + itemRatingCriteria + ': </span>';
            for (i = 0; i < 10; i++) {
                if (ratingValue == ratingText[i]) {
                    userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" value="' + ratingTitle[i] + '"  disabled="disabled" checked="checked"/>';
                } else {
                    userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" value="' + ratingTitle[i] + '"  disabled="disabled"/>';
                }
            }
            userRatingStarsDetailsInfo += '</div>';
            $('#tblRatingPerUser span.cssClassReviewId_' + itemReviewID + '').append(userRatingStarsDetailsInfo);
        },
        SaveItemRatings: function () {
            var ratingManageObj = {
                ItemReviewID: $("#btnDeleteReview").attr("name"),
                ViewFromIP: userIP,
                viewFromCountry: countryName,
                UserName: $("#txtNickName").val(),
                ReviewSummary: $("#txtSummaryReview").val(),
                Review: $("#txtReview").val(),
                ItemRatingCriteria: ratingValues,
                ItemID: $("#lnkItemName").attr("name"),
                StatusID: $("#selectStatus").val()
            };
            this.config.url = this.config.baseURL + "UpdateItemRating";
            this.config.data = JSON2.stringify({ ratingManageObj: ratingManageObj, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindRatingCriteria: function () {
            this.config.url = this.config.baseURL + "GetItemRatingCriteria";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, isFlag: false });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        GetStatusOfRatingReview: function () {
            this.config.url = this.config.baseURL + "GetStatus";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindReviewPopUp: function (reviewID) {
            review_id = reviewID;
            $("#btnDeleteReview").attr("name", review_id);
            this.config.url = this.config.baseURL + "GetItemRatingByReviewID";
            this.config.data = JSON2.stringify({ itemReviewID: review_id, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        ConfirmSingleDeleteItemReview: function (review_id, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteSingleItemRating";
                this.config.data = JSON2.stringify({ itemReviewID: review_id, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            } else {
                return false;
            }
        },
        GetItemRatingPerUser: function (offset, limit, currenpage) {
            currentpage = currenpage;
            ItemsReview = [];
            recentReviewsAndRatings.config.url = recentReviewsAndRatings.config.baseURL + "GetRecentItemReviewsAndRatings";
            recentReviewsAndRatings.config.data = JSON2.stringify({ offset: offset, limit: limit, aspxCommonObj: aspxCommonObj });
            recentReviewsAndRatings.config.ajaxCallMode = 6;
            recentReviewsAndRatings.ajaxCall(recentReviewsAndRatings.config);
        },
        ajaxSuccess: function (msg) {
            switch (recentReviewsAndRatings.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info('<h2>' + getLocale(AspxAdminDashBoard, "Information Alert") + '</h2><p>' + getLocale(AspxAdminDashBoard, "This review has been updated successfully.") + '</p>');
                    recentReviewsAndRatings.GetItemRatingPerUser(1, $("#ddlPageSize").val(), 0);
                    recentReviewsAndRatings.HideAll();
                    recentReviewsAndRatings.ClearReviewForm();
                    break;
                case 2:
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            recentReviewsAndRatings.RatingCriteria(item);
                        };
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxAdminDashBoard, "Information Alert") + '</h2><p> ' + getLocale(AspxAdminDashBoard, " No criteria for rating found!") + '</p>');
                    }
                    break;
                case 3:
                    $.each(msg.d, function (index, item) {
                        recentReviewsAndRatings.BindRatingReviewStatusDropDown(item);
                    });
                    break;
                case 4:
                    $("#tblRatingCriteria label.error").hide();
                    var itemAvgRating = '';
                    $.each(msg.d, function (index, item) {
                        if (index == 0) {
                            recentReviewsAndRatings.BindStarRatingsDetails();
                            recentReviewsAndRatings.BindItemReviewDetails(item);
                            recentReviewsAndRatings.BindStarRatingAverage(item.RatingAverage);
                            itemRatingAverage = item.RatingAverage;
                        }
                        itemAvgRating = JSON2.stringify(item.RatingValue);
                        $('input.item-rating-crieteria' + item.ItemRatingCriteriaID).rating('select', itemAvgRating);
                    });
                    $.metadata.setType("class");
                    $('input.auto-star-avg').rating();
                    break;
                case 5:
                    recentReviewsAndRatings.GetItemRatingPerUser(1, $("#ddlPageSize").val(), 0);
                    recentReviewsAndRatings.HideAll();
                    return false;
                    break;
                case 6:
                    arrItemDetailsReviewList.length = 0;
                    arrItemReviewList.length = 0;
                    var length = msg.d.length;
                    if (length > 0) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            rowTotal = item.RowTotal;
                            recentReviewsAndRatings.BindItemsRatingByUser(item, index);
                        };
                                               var optInit = recentReviewsAndRatings.getOptionsFromForm();
                        $("#Pagination").pagination(rowTotal, optInit);
                    } else {
                        $("#divSearchPageNumber").hide();
                        var avgRating = "<tr><td class=\"cssClassNotFound\">" + getLocale(AspxAdminDashBoard, 'Currently there are no reviews') + "</td></tr>";
                        $("#tblRatingPerUser").append(avgRating);
                    }
                    break;
            }
        },
        ajaxFailure: function (data) {
            switch (recentReviewsAndRatings.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Failed to save Rating") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Failed to bind Rating Criteria") + ' </p>');
                    break;
                case 3:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Failed to bind Status Of Rating") + ' </p>');
                    break;
                case 4:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Failed to bind Review PopUp.") + ' </p>');
                    break;
                case 5:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Unable to delete.") + '</p>');
                    break;
                case 6:
                    csscody.error('<h2>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h2><p>' + getLocale(AspxAdminDashBoard, "Error! Failed to bind User Rating.") + '</p>');
                    break;
            }
        },
        init: function (config) {
            recentReviewsAndRatings.BindRatingCriteria();
            recentReviewsAndRatings.GetStatusOfRatingReview();
            recentReviewsAndRatings.GetItemRatingPerUser(1, $("#ddlPageSize").val(), 0);

            $("#ddlPageSize").change(function () {
                var items_per_page = $(this).val();
                var offset = 1;
                recentReviewsAndRatings.GetItemRatingPerUser(offset, items_per_page, 0);

            });

            $(".cssClassClose").click(function () {
                $('#fade, #popuprel5').fadeOut();
            });

            $("#btnReviewBack").click(function () {
                $('#fade, #popuprel5').fadeOut();
            });
            $("#btnDeleteReview").click(function () {
                var review_id = $(this).attr("name");
                var properties = {
                    onComplete: function (e) {
                        if (e) {
                            recentReviewsAndRatings.ConfirmSingleDeleteItemReview(review_id, e);
                        } else {
                            return false;
                        }
                    }
                };
                csscody.confirm("<h1>" + getLocale(AspxAdminDashBoard, 'Delete Confirmation') + "</h1><p>" + getLocale(AspxAdminDashBoard, 'Do you want to delete this item rating and review?') + "</p>", properties);
            });

            var v = $("#form1").validate({
                ignore: ':hidden',
                rules: {
                    name: "required",
                    summary: "required",
                    review: "required"
                },
                messages: {
                    name: "at least 2 chars",
                    summary: "at least 2 chars",
                    review: "*"
                }
            });

            $("#btnSubmitReview").click(function () {
                if (v.form()) {
                    recentReviewsAndRatings.SaveItemRatings();
                    return false;
                } else {
                    return false;
                }
            });
        }
    };
    recentReviewsAndRatings.init();
});
