<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NLUnSubscribe.ascx.cs"
    Inherits="Modules_NewsLetter_NLUnSubscribe" %>
    <script type="text/javascript">
        var UserModuleID = '<%=UserModuleID %>';
        var PortalID = '<%=PortalID %>';
        var NewsLetterPath = '<%=ModulePath %>';
        var UserName = '<%=UserName %>';
    </script>
<div id="divUnSubscribe">
    <label id="lblmessage" class="sfMessage">
    </label>
     <div id="imageplace" style="margin-top: 5px;">
       
    </div>
    <div id="header">
        <h2>
            UnSubscribe</h2>
    </div>
    <div id="divRadio">
        <input type="radio" name="rdbUnSubcribe" value="ByEmail">By Email</input>
     <%--   <input type="radio" name="rdbUnSubcribe" id="rdbPhone" value="ByPhone">By Phone</input>--%>
    </div>
    <div id="divEmailUnSubsCribe">
        <div id="divEmailtext">
            Your Email</div>
        <input name="Email" class="sfInputbox" type="text" id="txtEmailUnSubscribe" />
       
    </div>
    <div id="phoneUnSubscribe">
        <div id="divPhoneText">
            Your Mobile Number</div>
        <input name="Mobile" onkeypress="return isNumberKey(event)" type="text" id="txtPhone" />
    </div> <br />
    <a id="btnUnSubscribe" href="#" class="sfBtn">UnSubscribe</a>
    <%--<div id="divbtnUnsubscribe">
        <a id="btnUnsubscibe" href="#" class="sfUnSubscribeButton">Click here to Subscribe</a>
    </div>--%>
</div>
<div id="unsubscribeEmailLink">
<label class="message"></label>

</div>
