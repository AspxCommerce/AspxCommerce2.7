<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayThroughPaypal.aspx.cs"
    Inherits="Modules_AspxCommerce_PayPalGateWay_PayThroughPaypal" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Processing your Order....</title>

   <script type="text/javascript" src="http://code.jquery.com/jquery-latest.pack.js"></script>
    
<script type="text/javascript">
    $(function() {
        $(this).bind("contextmenu", function(e) {
            e.preventDefault();
        });
        $("#clickhere").click(function() {          
            document.PaypalForm.submit();

        });
//        $(".sfLocale").localize({
//             moduleKey:PayPal
//        });      
    });

</script>

<style type="text/css">

body {
font-family:verdana;
font-size:15px;
}
</style>

</head>
<body >
<form id="Form1" runat="server">
<div>
<asp:Label runat="server" >Connecting to Paypal...</asp:Label>
<p>
    <asp:Label ID="lblnotity"  runat="server" meta:resourcekey="lblnotityResource1">if you are not redirecting to Paypal click </asp:Label>  
    <asp:LinkButton ID="clickhere" runat="server"  onclick="clickhere_Click" 
        meta:resourcekey="clickhereResource1">here..</asp:LinkButton>
</p>
 
</div>
    </form>
</body>
</html>
