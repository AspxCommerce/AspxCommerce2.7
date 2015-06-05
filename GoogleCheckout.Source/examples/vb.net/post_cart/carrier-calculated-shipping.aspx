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
    Req.AddItem("Mars bar", "Packed with peanuts", 0.75, 2)
    
    'lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5)
    
    'Add a rule to tax all items at 7.5% for Ohio
    Req.AddStateTaxRule("OH", 7.5, True)
    
    'The next thing we will do is add a Fedex Home Package.
    'We will set the default to 3.99, the Pickup to Regular Pickup, the additional fixed charge to 1.29 and the discount to 2.5%
    Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Home_Delivery, 3.99, CarrierPickup.REGULAR_PICKUP, 1.29, -2.5)
    Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Second_Day, 9.99, CarrierPickup.REGULAR_PICKUP, 2.34, -24.5)
    
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
  <title>Carrier Calculated Shipping Example</title>
</head>
<body>
  This page demonstrates a simple cart post with carrier calculated shipping.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
