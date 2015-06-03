<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Store-Closed.aspx.cs" Inherits="Modules_AspxCommerce_Store_Closed" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="../../js/jquery-1.4.4.js" ></script>
<script type="text/javascript" >
    $(document).ready(function() {      
        $('#cssPathStoreClosed').attr('href', '' + aspxTemplateFolderPath + '/css/template.css');
         $(".sfLocale").localize({
             moduleKey:Store
        });
    });
</script>
    <title class="sfLocale">AspxCommerce: Store is Closed</title>
        <link id="cssPathStoreClosed" rel="stylesheet" type="text/css" />
        <style type="text/css">
		body { overflow:hidden;}
		</style>
</head>
<body>
    <form id="form1" runat="server">

    
    <div class="cssClassOuterMainWrapper" >
    <div class="cssClassLogo" >
        <h1><a href="#" class="sfLocale">AspxCommerce</a></h1>
    </div>
    <div class="closed"><h2><asp:Literal ID="ltrPageContent" runat="server" 
            meta:resourcekey="ltrPageContentResource1"></asp:Literal></h2></div>
    </div>
        
    </form>
</body>
</html>
