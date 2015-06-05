<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>
<script runat="server" language="c#">

  public void Page_Load(Object sender, EventArgs e)
  {
    // To disable the donation button, uncomment the line below.
    //DonationButton.Enabled = false;
  }

  private void PostCartToGoogle(object sender, System.Web.UI.ImageClickEventArgs e)
  {
    CheckoutShoppingCartRequest Req = DonationButton.CreateRequest();
    decimal DonationAmount = decimal.Parse(DonationAmountTextBox.Text);
    Req.AddItem("Donation", "Helping us help those in need", DonationAmount, 1);
    GCheckoutResponse Resp = Req.Send();
    if (Resp.IsGood)
    {
      Response.Redirect(Resp.RedirectUrl, true);
    }
    else
    {
      Response.Write("Resp.ResponseXml = " + Resp.ResponseXml + "<br>");
      Response.Write("Resp.RedirectUrl = " + Resp.RedirectUrl + "<br>");
      Response.Write("Resp.IsGood = " + Resp.IsGood + "<br>");
      Response.Write("Resp.ErrorMessage = " + Resp.ErrorMessage + "<br>");
    }
  }

</script>
<html>
  <head>
    <title>Simple cart post for donations</title>
  </head>

  <body>
    Donation amount
    <br>
    <form id="Form1" method="post" runat="server">
      <asp:TextBox id="DonationAmountTextBox" runat="server" />
      <br>
      <cc1:GCheckoutDonateButton id="DonationButton" onclick="PostCartToGoogle" runat="server" />
    </form>
  </body>
</html>
