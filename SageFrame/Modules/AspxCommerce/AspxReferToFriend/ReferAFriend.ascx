<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReferAFriend.ascx.cs"
    Inherits="Modules_AspxReferToFriend_ReferAFriend" %>

<script type="text/javascript" >
    //<![CDATA[
    var userFullName = '<%=UserFullName%>';
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxItemDetails
        });
    });
    var ReferAFriend = "";
    $(function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        ReferAFriend = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
                url: "",
                method: "",
                ajaxCallMode: ""
            },
            vars: {
                totalPrice: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ReferAFriend.config.type,
                    contentType: ReferAFriend.config.contentType,
                    cache: ReferAFriend.config.cache,
                    async: ReferAFriend.config.async,
                    data: ReferAFriend.config.data,
                    dataType: ReferAFriend.config.dataType,
                    url: ReferAFriend.config.url,
                    success: ReferAFriend.config.ajaxCallMode,
                    error: ReferAFriend.ajaxFailure
                });
            },
            init: function() {
                ShowPopup('a.popupEmailAFriend');
                $(".cssClassClose").bind("click", function() {
                    RemovePopUp();
                });

                if (AspxCommerce.utils.GetUserName().toLowerCase() != "anonymoususer") {
                    $("#txtYourName").val(userFullName);
                    $("#txtYourName").attr('disabled', 'disabled');
                    $("#txtYourName").attr('readonly', 'readonly');
                    $("#txtYourEmail").val(ItemDetail.vars.userEmail);
                    $("#txtYourEmail").attr('disabled', 'disabled');
                    $("#txtYourEmail").attr('readonly', 'readonly');
                } else {
                    $("#txtYourName").removeAttr('disabled');
                    $("#txtYourName").removeAttr('readonly');
                    $("#txtYourEmail").removeAttr('disabled');
                    $("#txtYourEmail").removeAttr('readonly');
                }


                var m = $("#EmailForm").validate({
                    ignore: ':hidden',
                    rules: {
                        yourname: {
                            minlength: 2
                        },
                        youremail: "required",
                        friendname: {
                            minlength: 2
                        },
                        friendemail: "required",
                        subj: {
                            minlength: 2
                        },
                        msg: "required"
                    },
                    messages: {
                        yourname: {
                            required: "*",
                            minlength: "" + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                        },
                        youremail: "Valid email required",
                        friendname: {
                            required: "*",
                            minlength: "" + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                        },
                        friendemail: "Valid email required",
                        subj: {
                            required: "*",
                            minlength: "" + getLocale(AspxItemDetails, "(at least 2 chars)") + ""
                        },
                        msg: "*"
                    }
                });
                $("#btnSendEmail").bind("click", function() {
                    $(".required").each(function(i) {
                        if ($(this).val() == $(this).attr('title')) {
                            $(this).val('');
                        }
                    });
                    if (m.form()) {
                        ReferAFriend.SendEmailToFriend();
                        RemovePopUp();
                        return false;
                    } else {
                    $(":text").labelify();
                    $("#txtMessage").labelify();
                    $("input[type=\"text\"]").css("color", "#000");
                    $("#txtMessage").css("color", "#000");
                        return false;
                    }                    
                });
                $(":text").labelify();
                $("#txtMessage").labelify();
                $("input[type=\"text\"]").css("color", "#000");
                $("#txtMessage").css("color", "#000");
            },
            SendEmailToFriend: function() {
                var senderName = $("#txtYourName").val();
                var senderEmail = $.trim($("#txtYourEmail").val());
                var receiverName = $("#txtFriendName").val();
                var receiverEmail = $("#txtFriendEmail").val();
                var subject = $("#txtSubject").val();
                var message = $("#txtMessage").val();

                var imgpath = $('.cssClassProductBigPicture').attr('imagepath');
                imgpath = imgpath.replace('/uploads/', '/uploads/Small/');
                var fullDate = new Date();
                var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                if (twoDigitMonth.length == 2) {
                } else if (twoDigitMonth.length == 1) {
                    twoDigitMonth = '0' + twoDigitMonth;
                }
                var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                var dateyear = fullDate.getFullYear();

                var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
                var serverHostLoc = 'http://' + serverLocation;
                var itemprice = $('#spanPrice').html();
                var emailAFriendInfo = {
                    itemInfo: {
                        src: serverHostLoc + "/" + imgpath,
                        alt: $('#spanItemName').html(),
                        title: $('#spanItemName').html(),
                        price: itemprice,
                        href: window.location.href
                                                                    }
                };
                var referToFriendObj = {
                    ItemID: ItemDetail.vars.itemId,
                    SenderName: senderName,
                    SenderEmail: senderEmail,
                    ReceiverName: receiverName,
                    ReceiverEmail: receiverEmail,
                    Subject: subject,
                    Message: message
                };
                this.config.url = this.config.baseURL + "SaveAndSendEmailMessage";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, referToFriendObj: referToFriendObj, messageBodyDetail: emailAFriendInfo.itemInfo });
                this.config.ajaxCallMode = ReferAFriend.SaveAndSendEmailSuccess;
                this.ajaxCall(this.config);
            },
            ClearForm: function() {
                if (AspxCommerce.utils.GetUserName().toLowerCase() != "anonymoususer") {
                    $("#txtYourName").val(userFullName);
                    $("#txtYourEmail").val(ItemDetail.vars.userEmail);
                } else {
                    $("#txtYourName").val('');
                    $("#txtYourEmail").val('');
                }
                $("#txtFriendName").val('');
                $("#txtFriendEmail").val('');
                $("#txtSubject").val('');
                $("#txtMessage").val('');
            },
            SaveAndSendEmailSuccess: function() {
                csscody.info("<h2>" + getLocale(AspxItemDetails, "Successful Message") + "</h2><p>" + getLocale(AspxItemDetails, "Email has been sent successfully.") + "</p>");
                ReferAFriend.ClearForm();
                $('#fade , #popuprel').fadeOut();
            }
        };
        ReferAFriend.init();
    });
    //]]>
</script>

<form class="cmxform" id="EmailForm" method="post" action="">
<div class="popupbox" id="popuprel">
<div class="cssPopUpBody">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale"><i class="i-close"></i>Close</span></button>
    </div>
    <h2>
        <label id="lblTitle" class="sfLocale">
            Email A Friend</label>
    </h2>
    <div class="sfFormwrapper">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblEmailAFriend">
            <tr>
                <td width="20%">
                    <label id="lblYourName" class="cssClassLabel sfLocale">
                        Your Name:</label><span class="cssClassRequired">*</span>
                </td>
                <td width="80%">
                    <input type="text" id="txtYourName" name="yourname" class="required"
                        title="Your Name" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblYourEmail" class="cssClassLabel sfLocale">
                        Your Email:</label><span class="cssClassRequired">*</span>
                </td>
                <td>
                    <input type="text" id="txtYourEmail" name="youremail" class="required email" title="Your Email" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblFriendName" class="cssClassLabel sfLocale">
                        Friend Name:</label><span class="cssClassRequired">*</span>
                </td>
                <td>
                    <input type="text" id="txtFriendName" name="friendname" class="required" title="Your Friend Name" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblFriendEmail" class="cssClassLabel sfLocale">
                        Friend Email:</label><span class="cssClassRequired sfLocale">*</span>
                </td>
                <td>
                    <input type="text" id="txtFriendEmail" name="friendemail" class="required email"
                        title="Your Friend Email" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblSubject" class="cssClassLabel sfLocale">
                        Subject:</label><span class="cssClassRequired">*</span>
                </td>
                <td>
                    <input type="text" id="txtSubject" name="subj" class="required" title="Your Subject Goes Here" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblMessage" class="cssClassLabel sfLocale">
                        Message:</label><span class="cssClassRequired">*</span>
                </td>
                <td>
                    <textarea id="txtMessage" cols="30" rows="6" name="msg" class="cssClassTextarea required"
                        onkeydown="limitMaxText(this);" onkeyup="limitMaxText(this);" title="Your Message Goes Here"></textarea>
                </td>
            </tr>
        </table>
        <div class="sfButtonwrapper">
            <label class="cssClassGreenBtn i-send"><button type="button" id="btnSendEmail" class="sfBtn">
                <span class="sfLocale">Send</span></button></label>
        </div>
    </div>
</div>
</div>
</form>
