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
		Req.AddFlatRateShippingMethod("UPS 2 Day Air", 25)

		'lets try adding a Carrier Calculated Shipping Type

		'The first thing that needs to be done for carrier calculated shipping is we must set the FOB address.
		Req.AddShippingPackage("main", "Cleveland", "OH", "44114", DeliveryAddressCategory.COMMERCIAL, 2, 3, 4)

		'The next thing we will do is add a Fedex Home Package.
		'We will set the default to 3.99, the Pickup to Regular Pickup, the additional fixed charge to 1.29 and the discount to 2.5%
		Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Home_Delivery, 3.99m, CarrierPickup.REGULAR_PICKUP, 1.29m, -2.5)
		Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Second_Day, 9.99m, CarrierPickup.REGULAR_PICKUP, 2.34m, -24.5)

		Dim Resp As GCheckoutResponse = Req.Send
        If Resp.IsGood Then
            Response.Redirect(Resp.RedirectUrl, true)
        Else
			Response.Write("Resp.ResponseXml = " & Resp.ResponseXml & "<br>")
			Response.Write("Resp.RedirectUrl = " & Resp.RedirectUrl & "<br>")
			Response.Write("Resp.IsGood = " & Resp.IsGood & "<br>")
			Response.Write("Resp.ErrorMessage = " & Resp.ErrorMessage & "<br>")
        End If
    End Sub


</script>

<html>
  <head>
    <title>Simple cart post</title>
  </head>

  <body>
    This page demonstrates a simple cart post.
    <br />
    <form id="Form1" method="post" runat="server">
      <cc1:GCheckoutButton id="GCheckoutButton1" onclick="PostCartToGoogle" runat="server" />
    </form>
  </body>
</html>
