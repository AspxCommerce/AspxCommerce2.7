(function ($) {
    var QId = 0;
    var arrItemListType = new Array();
    rowTotal = 0;
    currentPage = 0;
    var resultCount = 0;
    $.createFAQ = function (p) {
        p = $.extend
                ({
                    PortalID: '',
                    UserModuleID: '',
                    baseURL: '',
                    CultureName: ''
                }, p);
        var FAQList = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                method: "",
                url: "",
                ajaxCallMode: 0,
                baseURL: p.baseURL + "WebService.asmx/",
                PortalID: p.PortalID,
                UserModuleID: p.UserModuleID,
                CultureName: p.CultureName
            },

            init: function () {
                FAQList.LoadFAQList(1, 10, 0);
                $('#txtSearchFaq').keypress(function (e) {
                    if (e.which == 13) {
                        var searchText = $('#txtSearchFaq').val();
                        if (searchText == '' || searchText == '-- Search Your Question --') {
                            FAQList.LoadFAQList(1, 10, 0);
                        }
                        else {
                            $('.sfEmptySearch').hide();
                            FAQList.GetSearchList(searchText);
                            $('#txtSearchFaq').val('');
                        }
                    }
                });

                $('#btnSearchFAQ').click(function () {
                    var searchText = $('#txtSearchFaq').val();
                    if (searchText == '' || searchText == '-- Search Your Question --') {
                        FAQList.LoadFAQList(1, 10, 0);
                    }
                    else {
                        FAQList.GetSearchList(searchText);
                        $('#txtSearchFaq').val('');
                    }
                });
                $('.sfFieldItems').hide();

                var wmSearch = '-- Search Your Question --';
                $('#txtSearchFaq').val(wmSearch).addClass('watermark');
                $('#txtSearchFaq').blur(function () {
                    if ($(this).val().length == 0) {
                        $(this).val(wmSearch).addClass('watermark');
                    }
                });

                $('#txtSearchFaq').focus(function () {
                    if ($(this).val() == wmSearch) {
                        $(this).val('').removeClass('watermark');
                    }
                });

                $('h6.ncFAQHeader').live('click', function () {
                    var displaytype = $(this).next('.sfFieldItems').css('display');
                    if (displaytype == "none") {
                        $('.sfFieldItems').not($(this)).slideUp();
                        $(this).next('.sfFieldItems').slideDown();
                    }
                    else {
                        $(this).next('.sfFieldItems').slideUp();
                    }
                });

                $('h3.categoryHeader').live('click', function () {
                    $('.dvCategory').removeClass('sfActive');
                    $(this).parent().addClass('sfActive');
                    var dis = $(this).next('.dvCategoryList').css('display');
                    if (dis == "none") {
                        $('.dvCategoryList').not($(this)).slideUp();
                        $(this).next('.dvCategoryList').slideDown();
                    }
                    else {
                        $(this).parent().removeClass('sfActive');
                        $(this).next('.dvCategoryList').slideUp();
                    }
                });

                $('#btnNotHelpful').live('click', function () {
                    $(this).parent().hide();
                    $(this).parent().next().fadeIn("slow");
                });

                $('#btnHelpful').live('click', function () {
                    var optionID = 6;
                    var FAqId = $(this).closest('li').attr('id');
                    FAQList.SubmitFAQViewOption(optionID, FAqId);
                    $(this).parent().hide();
                    $(this).parent().next().next().fadeIn("slow");
                });

                $('#btnSendReason').live('click', function () {
                    var reason = $(this).parent().prev().prev().find('.sfReview').val();
                    var userEmail = $(this).parent().prev().find('.sfInputbox').val();
                    QId = $(this).parent().parent().parent().parent().parent().parent().attr('id');
                    if (reason == '') {
                        $(this).parent().prev().prev().find('.spnAlert').show();
                        return false;
                    }
                    else {
                        $(this).parent().prev().prev().find('.spnAlert').hide();
                    }
                    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (userEmail != '') {
                        if (!regex.test(userEmail)) {
                            $(this).parent().prev().find('.spnAlert').show();
                            return false;
                        }
                        else {
                            $(this).parent().prev().find('.spnAlert').hide();
                        }
                    }

                    FAQList.SendReason(reason, userEmail, QId);
                    $(this).parent().parent().parent().hide();
                    $(this).parent().parent().parent().next().fadeIn();
                });

                $('.helpOption li').live('click', function () {
                    var me = $(this);
                    var optionID = me.attr('id');
                    var FAqId = me.parent().parent().parent().parent().parent().attr('id');
                    if (optionID == 6) {
                        me.parent().hide();
                        me.parent().next().slideToggle();
                    }
                    else {
                        FAQList.SubmitFAQViewOption(optionID, FAqId);
                        $('.sfOptionList').hide();
                        me.parent().parent().next().fadeIn("slow");
                    }
                });
            },

            ajaxSuccess: function (data) {
                switch (FAQList.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        FAQList.BindFAQList(data);
                        break;
                    case 2:
                        break;
                }
            },

            GetSearchList: function (searchword) {
                resultCount = 1;
                FAQList.config.method = "GetSearchList";
                FAQList.config.url = FAQList.config.baseURL + FAQList.config.method;
                FAQList.config.data = JSON2.stringify({ PortalID: FAQList.config.PortalID, UserModuleID: FAQList.config.UserModuleID, CultureName: FAQList.config.CultureName, SearchWord: searchword });
                FAQList.config.ajaxCallMode = 1;
                FAQList.ajaxCall(FAQList.config);
            },

            LoadFAQList: function (offset, limit, current) {
                currentPage = current;
                FAQList.config.method = "GetFAQList";
                FAQList.config.url = FAQList.config.baseURL + FAQList.config.method;
                FAQList.config.data = JSON2.stringify({ PortalID: parseInt(FAQList.config.PortalID), UserModuleID: parseInt(FAQList.config.UserModuleID), CultureName: FAQList.config.CultureName, Offset: offset, limit: limit });
                FAQList.config.ajaxCallMode = 1;
                FAQList.ajaxCall(FAQList.config);

            },

            SubmitFAQViewOption: function (optionID, FAqId) {
                this.config.method = "SubmitFAQViewOption";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: this.config.PortalID, UserModuleID: this.config.UserModuleID, FAQId: FAqId, OptionId: optionID, CultureName: this.config.CultureName });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },

            SendReason: function (Review, userEmail, QId) {
                this.config.method = "SubmitReason";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FaqID: QId, Review: Review, userEmail: userEmail, CultureName: this.config.CultureName });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },

            BindFAQList: function (data) {
                $("#ulFAQList").html('');
                $('.sfEmptyresult').hide();
                var Html = "";
                var order = 1;
                var categoryCount = 1;
                if (data.d.length == 0) { 
                    if (resultCount == 1) {
                        $('.sfEmptyresult').slideDown();
                        $("#Pagination").hide();
                    }
                    else {
                        $('.dvFAQwrapper').hide();
                        $('.ncEmptyFAQ').show();
                        $('.sfEmptyresult').hide();
                    }
                }

                var temp = 0;
                var end = 0;
                var openlist = 0;
                $.each(data.d, function (index, value) {
                    rowTotal = value.RowTotal;
                    arrItemListType.push(value.FAQId);
                    if (temp == value.CategoryID) {
                        Html += '<li id="' + value.FAQId + '" class="sfFAQList">';
                        Html += '<div class="ncFAQ">';
                        Html += '<h6 class="ncFAQHeader" style="padding-left:25px">' + value.Question + '</h6>';
                        Html += '<div class="sfFieldItems">' + value.Answer;
                        Html += '<div class="sfFAQHelpful"><strong class="sfLocalee">Was this answer helpful?</strong>';
                        Html += '<input type="button" id="btnHelpful" class="sfBtn sfLocalee" value="Yes"/><input id="btnNotHelpful" class="sfBtn sfLocalee" type="button" Value="No"/> </div>';
                        Html += '<div class="sfOptionList"  style="display:none"><h2 class="sfLocalee">Why not ?</h2>';
                        Html += '<ul class="helpOption" ><li id="1" class="sfLocalee">It does not answer my question</li>';
                        Html += '<li id="2" class="sfLocalee">It contains info that is incorrect</li>';
                        Html += '<li id="3" class="sfLocalee">It is too much to read</li>';
                        Html += '<li id="4" class="sfLocalee">It is confusing</li>';
                        Html += '<li id="5" class="sfLocalee">I do not like the answer</li>';
                        Html += '<li id="6" class="sfLocalee">Other</li></ul>';
                        Html += '<div class="dvReason" style="display:none" >';
                        Html += '<div class="dvReview"><span class="spnrewLabel sfFormlabel" >Reason :</span>';
                        Html += '<textarea id="txtReason" type="textarea" class="sfInputbox sfReview"/>';
                        Html += '<span class="spnAlert">Enter Reason.</span> </div>';
                        Html += '<div class="dvEmail"><span class="spnLabel">Email :</span><input id="txtuserEmail" type="text" class="sfInputbox"/><span>(Optional)</span>';
                        Html += '<span class="spnAlert">Enter valid email.</span> </div>';
                        Html += '<div class="dvSendbtn"><input id="btnSendReason" type="button" class="sfBtn" value="Send" /></div>';
                        Html += '</div></div>';
                        Html += '<div class="sfFeedBack" style="display:none" ><p class="sfLocalee">Thanks for your feedback! Over time we use user feedback to improve the quality of our content and how we deliver it to you.</p></div>';
                        Html += '</div>';
                        Html += '<div class="clear"></div>';
                        Html += '</div></li>';
                    }
                    else {
                        if (openlist != 0) { Html += '</div>'; }
                        if (end != 0) { Html += '</div>'; }
                        end = 0;
                        openlist = 0;
                        Html += '<div class="dvCategory">';
                        Html += '<span style="float:left">' + categoryCount + '</span>';
                        Html += '<h3 class="categoryHeader" style="padding-left:25px">' + value.CategoryName + '<label id="lblTotallist"></label></h3>';
                        Html += '<div class="dvCategoryList" style="display:none">';
                        Html += '<li id="' + value.FAQId + '" class="sfFAQList">';
                        Html += '<div class="ncFAQ">';
                        Html += '<h6 class="ncFAQHeader" style="padding-left:25px">' + value.Question + '</h6>';
                        Html += '<div class="sfFieldItems">' + value.Answer;
                        Html += '<div class="sfFAQHelpful"><strong class="sfLocalee">Was this answer helpful?</strong>';
                        Html += '<input type="button" id="btnHelpful" class="sfBtn sfLocalee" value="Yes"/><input id="btnNotHelpful" class="sfBtn sfLocalee" type="button" Value="No"/> </div>';
                        Html += '<div class="sfOptionList"  style="display:none"><h2 class="sfLocalee">Why not ?</h2>';
                        Html += '<ul class="helpOption" ><li id="1" class="sfLocalee">It does not answer my question</li>';
                        Html += '<li id="2" class="sfLocalee">It contains info that is incorrect</li>';
                        Html += '<li id="3" class="sfLocalee">It is too much to read</li>';
                        Html += '<li id="4" class="sfLocalee">It is confusing</li>';
                        Html += '<li id="5" class="sfLocalee">I do not like the answer</li>';
                        Html += '<li id="6" class="sfLocalee">Other</li></ul>';
                        Html += '<div class="dvReason" style="display:none" >';
                        Html += '<div class="dvReview"><span class="spnrewLabel sfFormlabel" >Reason :</span>';
                        Html += '<textarea id="txtReason" type="textarea" class="sfInputbox sfReview"/>';
                        Html += '<span class="spnAlert">Enter Reason.</span> </div>';
                        Html += '<div class="dvEmail"><span class="spnLabel">Email :</span><input id="txtuserEmail" type="text" class="sfInputbox"/><span>(Optional)</span>';
                        Html += '<span class="spnAlert">Enter valid email.</span> </div>';
                        Html += '<div class="dvSendbtn"><input id="btnSendReason" type="button" class="sfBtn" value="Send" /></div>';
                        Html += '</div></div>';
                        Html += '<div class="sfFeedBack" style="display:none" ><p class="sfLocalee">Thanks for your feedback! Over time we use user feedback to improve the quality of our content and how we deliver it to you.</p></div>';
                        Html += '</div>';
                        Html += '<div class="clear"></div>';
                        Html += '</div></li>';
                        end++;
                        openlist++;
                        categoryCount++;
                    }
                    temp = value.CategoryID;
                });
                $("#ulFAQList").append(Html);
                $('.sfFieldItems').hide();
                $.each($('.dvCategory'), function () {
                    var me = $(this);
                    var liCount = me.find('.dvCategoryList').find('.sfFAQList').length;
                    liCount = '[' + liCount + ']';
                    me.find('#lblTotallist').append(liCount);
                });
                if (rowTotal > 10) {
                    if (arrItemListType.length > 0) {
                        $("#Pagination").pagination(rowTotal, {
                            items_per_page: 10,
                            current_page: currentPage,
                            callfunction: true,
                            function_name: { name: FAQList.LoadFAQList, limit: 10 },
                            prev_text: ".",
                            next_text: ".",
                            prev_show_always: false,
                            next_show_always: false
                        });
                    }
                }
                else {
                    $("#Pagination").hide();
                }
            },

            ajaxFailure: function () {
            },

            ajaxCall: function (config) {
                $.ajax({
                    type: FAQList.config.type,
                    contentType: FAQList.config.contentType,
                    async: FAQList.config.async,
                    cache: FAQList.config.cache,
                    url: FAQList.config.url,
                    data: FAQList.config.data,
                    dataType: FAQList.config.dataType,
                    success: FAQList.ajaxSuccess,
                    error: FAQList.ajaxFailure
                });
            }
        };
        FAQList.init();
    };

    $.fn.SageFAQList = function (p) {
        $.createFAQ(p);
    };
})(jQuery);