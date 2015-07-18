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
    
  'This rule is discussed at the following url
  'http://code.google.com/apis/checkout/developer/Google_Checkout_XML_API_Taxes.html
  'Example Showing Canada being taxed at 5%
  Private Sub PostCartToGoogle(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Dim Req As CheckoutShoppingCartRequest = GCheckoutButton1.CreateRequest
    Req.AddItem("Mars bar", "Packed with peanuts", 0.75, 2)
    
    'lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5)
    'Tax Canada at 5%
    Req.AddPostalAreaTaxRule("CA", 0.5, True)
    
    'Tax all cities that start with L4L at 7%
    Req.AddPostalAreaTaxRule("CA", "L4L*", 0.7, True)
    
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
<head>
  <title>Example 3: Charging a single tax rate in one state</title>
</head>
<body>
  This page demonstrates a simple cart post.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
