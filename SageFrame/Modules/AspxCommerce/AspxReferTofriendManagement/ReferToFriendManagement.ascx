<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReferToFriendManagement.ascx.cs"
    Inherits="Modules_AspxReferTofriendManagement_ReferToFriendManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var storeId, portalId, userName, cultureName, customerId, ip, countryName, sessionCode, userFriendlyURL;


    $(document).ready(function() {
        storeId = AspxCommerce.utils.GetStoreID();
        portalId = AspxCommerce.utils.GetPortalID();
        userName = AspxCommerce.utils.GetUserName();
        cultureName = AspxCommerce.utils.GetCultureName();
        customerId = AspxCommerce.utils.GetCustomerID();
        ip = AspxCommerce.utils.GetClientIP();
        countryName = AspxCommerce.utils.GetAspxClientCoutry();
        sessionCode = AspxCommerce.utils.GetSessionCode();
        userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl(); ;
        LoadReferToFriendStaticImage();
        BindEmailListInGrid();
        $('#btnDeleteSelected').click(function() {
            var emailAFriend_Ids = '';
                        $(".EmailsChkbox").each(function(i) {
                if ($(this).attr("checked")) {
                    emailAFriend_Ids += $(this).val() + ',';
                }
            });
            if (emailAFriend_Ids != "") {
                var properties = { onComplete: function(e) {
                    ConfirmDeleteMultipleEmails(emailAFriend_Ids, e);
                                    }
                }
                csscody.confirm("<h1>"+getLocale(AspxReferToFriendManagement,"Delete Confirmation")+'</h1><p>'+getLocale(AspxReferToFriendManagement,"Do you want to delete all selected items?")+"</p>", properties);
            }
            else {
                csscody.alert('<h1>'+getLocale(AspxReferToFriendManagement,"Information Alert")+"</h1><p>"+getLocale(AspxReferToFriendManagement,"You need to select at least one item before you can do this.<br/> To select one or more items, just check the box before each item.")+'</p>');
            }
        })
    });
    
    function LoadReferToFriendStaticImage() {
        $('#ajaxReferToFriendMgmtImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
    }

    function BindEmailListInGrid(senderNm, senderEmail, receiverNm, receiverEmail, sub) {
        var offset_ = 1;
        var current_ = 1;
        var perpage = ($("#gdvReferToAFriend_pagesize").length > 0) ? $("#gdvReferToAFriend_pagesize :selected").text() : 10;
        
        $("#gdvReferToAFriend").sagegrid({
            url: aspxservicePath + "AspxCommerceWebService.asmx/",
            functionMethod: 'GetAllReferToAFriendEmailList',
            colModel: [
                    { display: getLocale(AspxReferToFriendManagement,'EmailAFriendID'), name: 'emailafriend_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'EmailsChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxReferToFriendManagement,'Item ID'), name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Sender Name'), name: 'sender_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Sender Email'), name: 'sender_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Receiver Name'), name: 'receiver_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Receiver Email'), name: 'receiver_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Subject'), name: 'subject', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Message'), name: 'massage', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxReferToFriendManagement,'Store ID'), name: 'store_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Portal ID'), name: 'portal_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Active'), name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Is Deleted'), name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Is Modified'), name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Send On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: false },
                    { display: getLocale(AspxReferToFriendManagement,'Updated On'), name: 'UpdatedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'center', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Deleted On'), name: 'DeletedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'center', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Added By'), name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Updated By'), name: 'UpdatedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Deleted By'), name: 'DeletedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxReferToFriendManagement,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				],

            buttons: [
                			          {display: getLocale(AspxReferToFriendManagement,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'DeleteReferToAFriendEmails', arguments: '' }
    			    ],
            rp: perpage,
            nomsg: getLocale(AspxReferToFriendManagement,"No Records Found!"),
            param: {senderName: senderNm, senderEmail: senderEmail, receiverName: receiverNm, receiverEmail: receiverEmail, subject:sub, storeID: storeId, portalID: portalId, userName: userName },
            current: current_,
            pnew: offset_,
            sortcol: { 0: { sorter: false }, 1: { sorter: false }, 19: { sorter: false} }
        });
    }
    
    function DeleteReferToAFriendEmails(tblID, argus) {
        switch (tblID) {
            case "gdvReferToAFriend":
                var properties = { onComplete: function(e) {
                    DeleteMultipleEmailsInfo(argus[0], e);
                }
                }
                csscody.confirm("<h1>"+getLocale(AspxReferToFriendManagement,"Delete Confirmation")+"</h1><p>"+getLocale(AspxReferToFriendManagement,"Do you want to delete?")+"</p>", properties);
                break;
            default:
                break;
        }           
    }

    function ConfirmDeleteMultipleEmails(Ids, event) {
            DeleteMultipleEmailsInfo(Ids, event);      
    }

    function DeleteMultipleEmailsInfo(emailAFriend_Ids, event) {
        if (event) {
            var params = { emailAFriendIDs: emailAFriend_Ids, storeID: storeId, portalID: portalId, userName: userName };
            var mydata = JSON2.stringify(params);
            $.ajax({
                type: "POST",
                url: aspxservicePath + "AspxCommerceWebService.asmx/DeleteReferToFriendEmailUser",
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function() {
                    BindEmailListInGrid();
                }
            });
        }
        return false;
    }
    function SearchReferFrindsList() {
        var senderNm = $.trim($("#txtSearchSenderName").val());
        var senderEmail = $.trim($("#txtSearchSenderEmail").val());
        var receiverNm = $.trim($("#txtReceiverName").val());
        var receiverEmail = $.trim($("#txtReceiverEmail").val());
        var sub = $.trim($("#txtSubject").val());
        if (senderNm.length < 1) {
            senderNm = null;
        }
        if (senderEmail.length < 1) {
            senderEmail = null;
        }
        if (receiverNm.length < 1) {
            receiverNm = null;
        }
        if (receiverEmail.length < 1) {
            receiverEmail = null;
        }
        if (sub.length < 1) {
            sub = null;
        }
        BindEmailListInGrid(senderNm, senderEmail, receiverNm, receiverEmail, sub); 
    }
    //]]>
</script>

<div id="divShowReferToFriendDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Refer a friend list" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
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
                <div class="cssClassSearchPanel sfFormwrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Sender Name:</label>
                                <input type="text" id="txtSearchSenderName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Sender Email:</label>
                                <input type="text" id="txtSearchSenderEmail" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Receiver Name:</label>
                                <input type="text" id="txtReceiverName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale"">
                                    Receiver Email:</label>
                                <input type="text" id="txtReceiverEmail" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale"">
                                    Subject:</label>
                                <input type="text" id="txtSubject" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <div class="sfButtonwrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="SearchReferFrindsList()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxReferToFriendMgmtImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvReferToAFriend" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnEmailaFriendID" />
