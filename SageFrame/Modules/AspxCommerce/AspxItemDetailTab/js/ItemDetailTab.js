var ItemDetailTab =''
$(function () {
    var ItemTags = '';
    var TagNames = '';
    var MyTags = '';
    var UserTags = '';
    var arrItemDetailsReviewList = new Array();
    var arrItemReviewList = new Array();
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            CustomerID: AspxCommerce.utils.GetCustomerID(),
            SessionCode: AspxCommerce.utils.GetSessionCode()
        };
        return aspxCommonInfo;
    };

     ItemDetailTab = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            oncomplete: 0,
            ajaxCallMode: "",
            error: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: ItemDetailTab.config.type,
                contentType: ItemDetailTab.config.contentType,
                cache: ItemDetailTab.config.cache,
                async: ItemDetailTab.config.async,
                url: ItemDetailTab.config.url,
                data: ItemDetailTab.config.data,
                dataType: ItemDetailTab.config.dataType,
                success: ItemDetailTab.config.ajaxCallMode,
                error: ItemDetailTab.config.error,
                complete: ItemDetailTab.oncomplete
            });
        },
        vars: {            
            itemSKU: itemSKU           
        },
        BindItemRatingPerUser: function (msg) {
            arrItemDetailsReviewList.length = 0;
            arrItemReviewList.length = 0;
            var rowTotal = 0;
            var length = msg.d.length;
            if (length > 0) {
                var item;
                for (var index = 0; index < length; index++) {
                    item = msg.d[index];
                    ItemDetailTab.BindItemsRatingByUser(item, index);
                    rowTotal = item.RowTotal;
                };
                var optInit = ItemDetailTab.getOptionsFromForm();
                $("#Pagination").pagination(rowTotal, optInit);
                $("#divSearchPageNumber").show();
            } else {
                $("#divSearchPageNumber").hide();
                var avgRating = "<tr><td>" + getLocale(AspxItemDetailTab, "Currently no rating and reviews are available.") + "</td></tr>";
                $("#tblRatingPerUser").append(avgRating);
            }
        },  

        BindTags: function (msg) {
            $.each(msg.d, function (index, item) {
                ItemDetailTab.BindItemTags(item, index);
            });
            var popularTag='';
            if(ItemTags !="" && ItemTags !=null)
            {
                popularTag="<h2>" + getLocale(AspxItemDetailTab,"Popular Tags:")+"";
                popularTag += "</h2><div id=\"divItemTags\" class=\"cssClassPopular-Itemstags\">";
                popularTag += ItemTags.substring(0, ItemTags.length - 2);
                popularTag += "</div>";
                $("#popularTag").html(popularTag);
            }
            $("#divMyTags").html(MyTags.substring(0, MyTags.length - 2));
        },

        BindTagAfterDelete: function (msg) {
            ItemDetailTab.GetItemTags();
        },

        BindTagAfterAdd: function (msg) {
            ItemDetailTab.GetItemTags();
            ItemDetailTab.ClearTableContentTags(this);
            csscody.info("<h2>" + getLocale(AspxItemDetailTab, "Information Message") + "</h2><p>" + getLocale(AspxItemDetailTab, "your tag(s) has been accepted for moderation.") + "</p>");

        },
        GetTagsLoadErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxItemDetailTab, 'Error Message') + '</h2><p>' + getLocale(AspxItemDetailTab, 'Failed to load item tags!') + '</p>');
        },

        GetTagsSaveErrorMsg: function () {
            csscody.error('<h2>' + getLocale(AspxItemDetailTab, 'Error Message') + '</h2><p>' + getLocale(AspxItemDetailTab, 'Failed to save tags!') + '</p>');
        },

        BindRatingReviewTab: function () {
            $("#tblRatingPerUser").html('');
            ItemDetailTab.GetItemRatingPerUser(1, $("#ddlPageSize").val(), 0);
        },      
        GetItemRatingPerUser: function (offset, limit, currenpage) {
            ItemsReview = [];
            currentpage = currenpage;
            var param = JSON2.stringify({ offset: offset, limit: limit, itemSKU: itemSKU, aspxCommonObj: aspxCommonObj() });
            ItemDetailTab.config.method = "AspxCoreHandler.ashx/GetItemRatingPerUser";
            ItemDetailTab.config.url = ItemDetailTab.config.baseURL + ItemDetailTab.config.method;
            ItemDetailTab.config.data = param;
            ItemDetailTab.config.ajaxCallMode = ItemDetailTab.BindItemRatingPerUser;
            ItemDetailTab.ajaxCall(ItemDetailTab.config);
        },

        BindItemsRatingByUser: function (item, index) {
            arrItemDetailsReviewList.push(item);
            if (!IsExists(ItemsReview, item.ItemReviewID)) {
                ItemsReview.push(item.ItemReviewID);
                arrItemReviewList.push(item);
            }
        },

        BindAverageUserRating: function (item) {
            var userRatings = '';
            userRatings += '<tr><td><div class="cssClassRateReview"><div class="cssClassItemRating">';
            userRatings += '<div class="cssClassItemRatingBox">' + ItemDetailTab.BindStarRatingAveragePerUser(item.ItemReviewID, item.RatingAverage) + '</div>';


            userRatings += '<div class="cssClassRatingInfo"><p><span>' + getLocale(AspxItemDetailTab, 'Reviewed by') + ' <strong>' + item.Username + '</strong></span></p><p class="cssClassRatingReviewDate">(' + getLocale(AspxItemDetailTab, 'Posted on') + '&nbsp;<strong>' + formatDate(new Date(item.AddedOn), "yyyy/M/d hh:mm:ssa") + '</strong>)</p></div></div>';

            userRatings += '<div class="cssClassRatingdesc"><p>' + Encoder.htmlDecode(item.ReviewSummary) + '</p><p class="cssClassRatingReviewDesc">' + Encoder.htmlDecode(item.Review) + '</p></div>';

            userRatings += '</div></td></tr>';
            $("#tblRatingPerUser").append(userRatings);
            var ratingToolTip = $("#hdnRatingTitle" + item.ItemReviewID + "").val();
            $(".cssClassUserRatingTitle_" + item.ItemReviewID + "").html(ratingToolTip);
        },

        BindStarRatingAveragePerUser: function (itemReviewID, itemAvgRating) {
            var ratingStars = '';
            var ratingTitle = [getLocale(AspxItemDetailTab, "Worst"), getLocale(AspxItemDetailTab, "Ugly"), getLocale(AspxItemDetailTab, "Bad"), getLocale(AspxItemDetailTab, "Not Bad"), getLocale(AspxItemDetailTab, "Average"), getLocale(AspxItemDetailTab, "OK"), getLocale(AspxItemDetailTab, "Nice"), getLocale(AspxItemDetailTab, "Good"), getLocale(AspxItemDetailTab, "Best"), getLocale(AspxItemDetailTab, "Excellent")]; var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
            var i = 0;
            var ratingTitleText = '';
            ratingStars += '<div class="cssClassRatingStar"><div class="cssClassToolTip">';
            ratingStars += '<span class="cssClassRatingTitle2 cssClassUserRatingTitle_' + itemReviewID + '"></span>';
            for (i = 0; i < 10; i++) {
                if (itemAvgRating == ratingText[i]) {
                    ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                    ratingTitleText = ratingTitle[i];
                } else {
                    ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                }
            }
            ratingStars += '<input type="hidden" value="' + ratingTitleText + '" id="hdnRatingTitle' + itemReviewID + '"></input><span class="cssClassToolTipInfo cssClassReviewId_' + itemReviewID + '"></span></div></div><div class="cssClassClear"></div>';
            return ratingStars;
        },

        BindPerUserIndividualRatings: function (itemReviewID, itemRatingCriteria, ratingValue) {
            var userRatingStarsDetailsInfo = '';
            var ratingTitle = [getLocale(AspxItemDetailTab, "Worst"), getLocale(AspxItemDetailTab, "Ugly"), getLocale(AspxItemDetailTab, "Bad"), getLocale(AspxItemDetailTab, "Not Bad"), getLocale(AspxItemDetailTab, "Average"), getLocale(AspxItemDetailTab, "OK"), getLocale(AspxItemDetailTab, "Nice"), getLocale(AspxItemDetailTab, "Good"), getLocale(AspxItemDetailTab, "Best"), getLocale(AspxItemDetailTab, "Excellent")]; var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
            var i = 0;
            userRatingStarsDetailsInfo += '<div class="cssClassToolTipDetailInfo">';
            userRatingStarsDetailsInfo += '<span class="cssClassCriteriaTitle">' + itemRatingCriteria + ': </span>';
            for (i = 0; i < 10; i++) {
                if (ratingValue == ratingText[i]) {
                    userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                } else {
                    userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                }
            }
            userRatingStarsDetailsInfo += '</div>';
            $('#tblRatingPerUser span.cssClassReviewId_' + itemReviewID + '').append(userRatingStarsDetailsInfo);
        },

        GetItemTags: function () {
            ItemTags = '';
            TagNames = '';
            MyTags = '';
            UserTags = '';
            this.config.method = "AspxCoreHandler.ashx/GetItemTags";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ itemSKU: itemSKU, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = ItemDetailTab.BindTags;
            this.config.error = ItemDetailTab.GetTagsErrorMsg;
            this.ajaxCall(this.config);
        },

        BindItemTags: function (item, index) {
            if (TagNames.indexOf(item.Tag) == -1) {
                ItemTags += item.Tag + "(" + item.TagCount + "), ";
                TagNames += item.Tag;
            }

            if (item.AddedBy == userName) {
                if (UserTags.indexOf(item.Tag) == -1) {
                    MyTags += item.Tag + "<button type=\"button\" class=\"cssClassCross\" value=" + item.ItemTagID + " onclick ='ItemDetailTab.DeleteMyTag(this)'><span>" + getLocale(AspxItemDetailTab, 'x') + "</span></button>, ";
                    UserTags += item.Tag;
                }
            }
        },

        DeleteMyTag: function (obj) {
            var itemTagId = $(obj).attr("value");
            this.config.method = "AspxCoreHandler.ashx/DeleteUserOwnTag";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ itemTagID: itemTagId, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = ItemDetailTab.BindTagAfterDelete;
            this.ajaxCall(this.config);
        },

        SubmitTag: function () {
            var isValid = false;
            var TagValue = '';
            $(".classTag").each(function () {
                if ($(this).val() == '') {
                    $(this).parents('td').find('span[class="err"]').html('');
                    $('<span class="err" style="color:red;">*<span>').insertAfter(this);
                    isValid = false;
                    return false;
                } else {
                    isValid = true;
                    TagValue += Encoder.htmlEncode($(this).val()) + "#";
                    $(this).siblings('span').remove();
                }
            });
            if (isValid) {
                TagValue = TagValue.substring(0, TagValue.length - 1);
                this.config.method = "AspxCoreHandler.ashx/AddTagsOfItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, tags: TagValue, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = ItemDetailTab.BindTagAfterAdd;
                this.config.error = ItemDetailTab.GetTagsSaveErrorMsg;
                this.ajaxCall(this.config);
            }
        },

        ClearTableContentTags: function (obj) {
            $('#AddTagTable tr:not(:last-child)').remove();
            $(".classTag").val('');
        },

        pageselectCallback: function (page_index, jq, execute) {
            if (execute) {
                var items_per_page = $('#ddlPageSize').val();
                var max_elem = Math.min((page_index + 1) * items_per_page, arrItemReviewList.length);
                $("#tblRatingPerUser").html('');
                ItemsReview = [];
                for (var i = 0; i < max_elem; i++) {
                    ItemDetailTab.BindAverageUserRating(arrItemReviewList[i]);
                    ItemsReview.push(arrItemReviewList[i].ItemReviewID);
                }
                $.each(arrItemDetailsReviewList, function (index, item) {
                    if (IsExists(ItemsReview, item.ItemReviewID)) {
                        ItemDetailTab.BindPerUserIndividualRatings(item.ItemReviewID, item.ItemRatingCriteria, item.RatingValue);
                    }
                });
                $('input.star-rate').rating();
                $("#tblRatingPerUser tr:even").addClass("sfOdd");
                $("#tblRatingPerUser tr:odd").addClass("sfEven");
            }
            return false;
        },

        getOptionsFromForm: function () {
            var opt = { callback: ItemDetailTab.pageselectCallback };
            opt.items_per_page = $('#ddlPageSize').val();
            opt.current_page = currentpage;
            opt.callfunction = true,
                 opt.function_name = { name: ItemDetailTab.GetItemRatingPerUser, limit: $('#ddlPageSize').val() },
                opt.prev_text = "Prev";
            opt.next_text = "Next";
            opt.prev_show_always = false;
            opt.next_show_always = false;
            return opt;
        },
        
        noSpaceonTag: function () {
            $(".classTag").on("keyup", function (e) {
                if (e.keyCode == "32") {
                    e.preventDefault();
                }
            });
            $(".classTag").on("keydown", function (e) {
                if (e.keyCode == "32") {
                    e.preventDefault();
                }
            });
            $('.classTag').on("input", function () {
                $(this).val($(this).val().replace(/ /g, ""));
            });
        },

        Init: function () {
            ItemDetailTab.noSpaceonTag();
            RESPONSIVEUI.responsiveTabs();
            $("img.youtube").YouTubePopup({ idAttribute: 'id' });
            $("#dynItemDetailsForm").show();
            $("#dynItemDetailsForm").find(".cssClassDecrease").click(function () {
                var cloneRow = $(this).closest('tr');
                if (cloneRow.is(":last-child")) {
                    var prevTR = $(cloneRow).prev('tr');
                    var prevTagTitle = prevTR.find("input[type='text']").val();
                    prevTR.remove();
                    $(cloneRow).find("input[type='text']").val(prevTagTitle)
                    return false;
                } else {
                    $(cloneRow).remove();
                }
            });

            $("#dynItemDetailsForm").find("#btnTagSubmit").bind("click", function () {
                ItemDetailTab.SubmitTag();
            });

            $("#dynItemDetailsForm").find("#ddlPageSize").bind("change", function () {
                var items_per_page = $(this).val();
                var offset = 1;
                ItemDetailTab.GetItemRatingPerUser(offset, items_per_page, 0);
            });      
            $(".cssClassTotalReviews").bind("click", function () {
                $.metadata.setType("class");
                var $tabs = $('#ItemDetails_TabContainer').tabs();
                $("#ItemDetails_TabContainer").find('ul').removeClass();
                $("#ItemDetails_TabContainer ").find("ul").addClass("responsive-tabs__list");
                $("#ItemDetails_TabContainer").removeClass();
                $("#ItemDetails_TabContainer").addClass("responsive-tabs responsive-tabs--enabled");
                $("#tablist1-tab4").trigger("click");
            });           
        }

    };
    ItemDetailTab.Init();
});


