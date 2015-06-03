<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fallback.aspx.cs" Inherits="Sagin_Fallback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fallback Page</title>
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/json2.min.js" type="text/javascript"></script>
    <link href="../Administrator/Templates/Default/css/login.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">
        //<![CDATA[
        var fallBackPath = '<%=fallBackPath %>';
        $(function () {
            $('div.sfExceptionHead').bind("click", function () {
                $(this).next("div").slideToggle("fast");
            });
            $('#btnActivate').bind("click", function () {
                var param = JSON2.stringify({ PortalID: parseInt(1) });
                $.ajax({
                    isPostBack: false,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: "<%=appPath%>" + "/Services/SageFrameGlobalWebService.asmx/" + "ActivateDefaultTemplate",
                    data: param,
                    success: function (msg) {
                        window.location = fallBackPath;
                    }
                });
            });
            var link = document.createElement('link');
            link.type = 'image/x-icon';
            link.rel = 'shortcut icon';
            link.href = '<%=templateFavicon %>';
            document.getElementsByTagName('head')[0].appendChild(link);
        });
    //]]>	
    </script>--%>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="sfFallback">
        <div class="sfLogoholder">
            <a class="sflogo" target="_blank" href="http://www.sageframe.com/">
                <asp:Image ID="imgLogo" runat="server" alt="SageFrame" />
            </a>
        </div>
        <div class="sfFallbackHolder">
            <div class="sfFallbackStatement">
                <h3>
                    SNAP! Some thing is not right with the template. We are so sorry</h3>
                <div class="sfTemplateerror">
                    <p>
                        There is something wrong with the preset.</p>
                    <div class="sfExceptionHead">
                        View Exception Details</div>
                    <div class="sfException">
                        <asp:Literal ID="ltrErrorMessage" runat="server"></asp:Literal>
                    </div>
                </div>
               <%-- <div class="sfButtonWrapper">
                    <h4>
                        Do you want to switch to the default template?</h4>
                   <span id="btnActivate" class="sfBtn">Yes, Switch to default</span>
                    <asp:Button runat="server" ID="btnFallback" Text="Yes, Switch to Default" 
                        CssClass="sfBtn" onclick="btnFallback_Click" />
                </div>--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
