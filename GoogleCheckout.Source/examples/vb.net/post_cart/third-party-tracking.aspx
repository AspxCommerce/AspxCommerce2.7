<%@ Page Language="VB" %>
<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	' This sample shows how to implement third-party tracking with Checkout and 
	' the .NET sample code. See
	' http://code.google.com/apis/checkout/developer/checkout_pixel_tracking.html
	' for details on the underlying XML API for tracking.

	' For the purposes of this example, let's say your tracking provider requires
	' you to hit this URL when an order has been placed:

	'    https://example.com/track?mid=A&oid=B&ot=C&zp=D

	'    mid - Your merchant ID with the tracking provider. Let's say it's 123.
	'    oid - Order ID.
	'    ot  - Order total.
	'    zp  - Customer's ZIP/postal code.

	' The code below shows how to set things up so this URL is hit from the 
	' Checkout "thank you" page, that is after the order has been placed. As a 
	' result, Checkout will render this JavaScript code on the "thank you" page:

	'    script type="text/javascript"
	'         function loadAnalytics() {
	'            analytic1 = new Image();
	'            analytic1.src = "https://example.com/track?mid\u003d123&oid\u003d502117496814353&ot\u003d1.50&zp\u003d94043";
	'         }
 
	' The order ID was 502117496814353, the total was $1.50 and the customer's ZIP
	' code was 94043. Note that the \uXXXX escape sequences will be interpreted by
	' JavaScript, so the customer's web browser will hit this URL:

	'    https://example.com/track?mid=123&oid=502117496814353&ot=1.50&zp=94043

    Private Sub PostCartToGoogle(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Req As CheckoutShoppingCartRequest = GCheckoutButton1.CreateRequest
        ' Add tracking URL for entire order.
        Dim trackingUrl As ParameterizedUrl = New ParameterizedUrl("https://example.com/track?mid=123")
        trackingUrl.AddParameter("oid", UrlParameterType.OrderID)
        trackingUrl.AddParameter("ot", UrlParameterType.OrderTotal)
        trackingUrl.AddParameter("zp", UrlParameterType.ShippingPostalCode)
        Req.ParameterizedUrls.AddUrl(trackingUrl)
        ' Add items.
		Req.AddItem("Mars bar", "Packed with peanuts", 0.75, 2)
        ' Submit the cart to Google.
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
    <title>Cart post with third-party tracking</title>
  </head>

  <body>
    This page demonstrates a cart post with third-party tracking.
    <br />
    <form id="Form1" method="post" runat="server">
      <cc1:GCheckoutButton id="GCheckoutButton1" onclick="PostCartToGoogle" runat="server" />
    </form>
  </body>
</html>
