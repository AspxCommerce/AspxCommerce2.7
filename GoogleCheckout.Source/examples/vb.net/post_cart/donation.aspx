<%@ Page Language="VB" %>

<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    ' To disable the donation button, uncomment the line below.
    'DonationButton.Enabled = false;
  End Sub
    
  Private Sub PostCartToGoogle(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Dim Req As CheckoutShoppingCartRequest = DonationButton.CreateRequest
    
    Dim DonationAmount As Decimal = Decimal.Parse(DonationAmountTextBox.Text)
    Req.AddItem("Donation", "Helping us help those in need", DonationAmount, 1)
    
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
  <title>Simple cart post for donations</title>
</head>
<body>
  Donation amount
  <br>
  <form id="Form1" method="post" runat="server">
    <asp:TextBox ID="DonationAmountTextBox" runat="server" />
    <br>
    <cc1:GCheckoutDonateButton ID="DonationButton" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
