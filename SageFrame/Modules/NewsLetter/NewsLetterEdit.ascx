<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsLetterEdit.ascx.cs"
    Inherits="Modules_NewsLetter_NewsLetterEdit" %>
<script type="text/javascript">
    var UserModuleID = '<%=UserModuleID %>';
    var PortalID = '<%=PortalID %>';
    var NewsLetterPath = '<%=ModulePath %>';
    var UserName = '<%=UserName %>';
    var CultureName = '<%=CultureName %>';
    var resolvedURL = '<%=resolvedURL %>';
    var PassURL = '<%=PassURL %>';
    var PageExtension = '<%=PageExtension %>';
        
</script>
<div class="sfAdminPanel">
    <%--<div class="main">
        <a href="#" class="cssClassActive" name="EmailSubsciption" id="aEmail">Email Subsciption</a>
        <%--<a href="#" id="aMobile" name="MobileSubscription">Mobile Subscription</a>
    </div>--%>
    <div id="Email">
        <div id="divNlEmailForm" class="sfFormwrapper">
            <table>
                <tr>
                    <td>
                        <label class="sfFormlabel">
                            Message Template Type</label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <select id="ddlMessageTemlate">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="sfFormlabel">
                            To</label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <textarea id="txtEmailList" name="emaillist" class="sfInputbox" rows="4" cols="40"></textarea>
                    </td>
                    <td>
                        <div class="sfButtonwrapper sftype1">
                            <label id="btnSubsciberAdd" class="sfAdd">
                                Select Subscriber</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="sfFormlabel">
                            Subject</label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <textarea id="txtSubject" name="subject" rows="2" cols="20" class="sfInputbox"></textarea>
                    </td>
                    <td style="display:none;">
                        <div class="sfButtonwrapper sftype1">
                            <label id="btnAddSubjectToken" class="sfAdd">
                                Add Subject Token</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="sfFormlabel">
                            Message</label>
                    </td>
                    <td>
                        :
                    </td>
                    <td colspan="2">                     
                         <textarea id="txtBodyMsg" rows="5" cols="20" name="bodymsg" style="height: 450px; width:800px;"></textarea>
                    </td>
                    
                </tr>            
            </table>
            <div class="sfEmailsendDiv">
                <a href="#" id="btnSendEmail" class="sfBtn">Send</a>
            </div>
        </div>
        <div id="divMessageListing">
            <div id="MessageListing" class="sfGridwrapper">
            </div>
        </div>
    </div>
    <div id="PhoneMessage">
        <table>
            <tr>
                <td>
                    <label class="sfFormlabel">
                        Message</label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <textarea id="txtMessage" cols="50" rows="5"></textarea>
                </td>
            </tr>
        </table>
          <div class="sfSmssendDiv">
                <a href="#" id="btnSendSms" class="sfsmsSend">Send</a>
            </div>
    </div>
</div>
<div id="TokenList" style="display: none;">
    <select id="lstMessageToken" multiple="multiple">
    </select>
    <div class="sfButtonwrapper">
        <label id="btnAddToken" class="sfBtn">
            Add
        </label>
    </div>
</div>
<div id="divMessageBodyTokenList" style="display: none;">
    <select id="lstMesageBodyToken" multiple="multiple">
    </select>
    <div class="sfButtonwrapper">
        <label id="btnAddBodyToken" class="sfBtn">
            Add
        </label>
    </div>
</div>
<div id="divSubscriberList" style="display:none;">


</div>
