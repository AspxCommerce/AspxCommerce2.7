<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsLetterView.ascx.cs"
    Inherits="Modules_NewsLetter_NewsLetterView" %>
<script type="text/javascript">
    $(function() {
        $(".sfLocalee").SystemLocalize();
    });
    var UserModuleID = '<%=UserModuleID %>';
    var PortalID = '<%=PortalID %>';
    var NewsLetterPath = '<%=ModulePath %>';
    var UserName = '<%=UserName %>';
    var PageExt = '<%=PageExt %>';        
</script>
<div id="divSubscribe" class="sfSubscribe">
    <div id="divRadios" style="display: none;">
        <input type="radio" name="rdbSubcribe" class="sfLocalee" value="By Email"/>
    </div>
    <div id="divEmailSubsCribe">
        <div id="divEmailtext" class="sfLocalee cssClassBMar10">Subscribe</div>
          <label id="lblmessage" style="display: none" class="sfMessage sfLocale">
    </label>
        <input name="Email" type="text" id="txtSubscribeEmail" class="sfInputbox cssClassBMar10" />
        <input id="btnSubscribe" type="button" class="sfBtn sfLocalee cssClassGreenBtn" value="Subscribe" />
    </div>
</div>
