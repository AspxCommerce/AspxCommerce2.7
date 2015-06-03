<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upgrade.aspx.cs" Inherits="upgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script src="../../js/jquery-1.9.1.js"></script>
	<link rel="stylesheet" type="text/css" href="css/module.css"/>
    <script type="text/javascript">
        //<![CDATA[   
        var ModuleFilePath = "../Modules/Upgrade/";
        $(document).ready(function () {
            var options = {
                type: "POST",
                url: '<%=appPath%>' + "/Upgrade",
                data: "{installerZipFile:'<%=ViewState["fileName"]%>'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "done") {
                        window.location = "../";
                    }
                }
            };
            $.ajax(options);

            var refreshId = setInterval(function () {
                $("#reportDiv").load(ModuleFilePath + 'Reporter.ashx', function (response, status, xhr) {
                    if (response.d != null && response.d.lastIndexOf("Successfully") > -1) {
                        clearInterval(refreshId);
                    }
                });
            }, 4000);
            $.ajaxSetup({ cache: false });
        });
        //]]>	
    </script>

</head>
<body>
	<div class="sfUpgradeWrap clearfix">
    <h1>Sageframe v3.5 upgrading...</h1>
    <p>This might take a while</p>
    <div class="sfFormwrapper sfPadding clearfix">
        <div id="reportDiv">
        	<div class="sfUpgPreloader"><img src="<%=upgradingGif %>" alt="" /></div>            
          <div class="sfUpgProcessStatus">            
            </div>
            <span class="sfUpgLogo"><img src="<%=imagePath %>" alt="sageframe" /></span>
        </div>
    </div>
    </div>
</body>
</html>
