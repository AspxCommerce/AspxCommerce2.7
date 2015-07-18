<%@ Page Language="VB" %>

<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    ' If the items in the cart aren't eligible for Google Checkout, uncomment line below.
    'GCheckoutButton1.Enabled = false;
  End Sub
    
  Private Sub PostCartToGoogle(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Dim Req As CheckoutShoppingCartRequest = GCheckoutButton1.CreateRequest
    'all of the following examples are explained in the following document
    'http://code.google.com/apis/checkout/developer/Google_Checkout_Digital_Delivery.html
    
    'Sample on how to create an email digital item.
    Dim emailDigitalItem As DigitalItem = New DigitalItem
    Req.AddItem("Email Digital Item", "Cool DigitalItem", 2, 1, emailDigitalItem)
    
    'Sample on how to create a Url Digital Item.
    Dim urlDigitalItem As DigitalItem = New DigitalItem(New Uri("http://www.google.com/download.aspx?myitem=1"), "Url Description for item")
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2, 1, urlDigitalItem)
    
    'Sample on how to create a Key Digital Item
    Dim keyDigitalItem As DigitalItem = New DigitalItem("24-235-sdf-123541-53", "Key Description for item")
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2, 1, keyDigitalItem)
    
    'Sample on how to create a Url/Key Digital Item
    Dim keyUrlItem As DigitalItem = New DigitalItem("24-235-sdf-123541-53", "http://www.google.com/download.aspx?myitem=1", "Url/Key Description for item")
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2, 1, keyUrlItem)
    
    'lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5)
    
    'Add a rule to tax all items at 7.5% for Ohio
    Req.AddStateTaxRule("OH", 7.5, True)
    
    Dim Resp As GCheckoutResponse = Req.Send
    If Resp.IsGood Then
      Response.Redirect(Resp.RedirectUrl, True)
    Else
      Response.Write("Resp.ResponseXml = " + Resp.ResponseXml + "<br>")
      Response.Write("Resp.RedirectUrl = " + Resp.RedirectUrl + "<br>")
      Response.Write("Resp.IsGood = " + Resp.IsGood + "<br>")
      Response.Write("Resp.ErrorMessage = " + Resp.ErrorMessage + "<br>")
    End If
  End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Digital Delivery Example</title>
</head>
<body>
  This page demonstrates a simple cart post with digital delivery.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
