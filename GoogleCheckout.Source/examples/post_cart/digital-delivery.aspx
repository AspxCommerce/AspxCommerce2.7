<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  public void Page_Load(Object sender, EventArgs e) {
    // If the items in the cart aren't eligible for Google Checkout, uncomment line below.
    //GCheckoutButton1.Enabled = false;
  }

  private void PostCartToGoogle(object sender, System.Web.UI.ImageClickEventArgs e) {
    CheckoutShoppingCartRequest Req = GCheckoutButton1.CreateRequest();

    //all of the following examples are explained in the following document
    //http://code.google.com/apis/checkout/developer/Google_Checkout_Digital_Delivery.html
    
    //Sample on how to create an email digital item.
    DigitalItem emailDigitalItem = new DigitalItem();
    Req.AddItem("Email Digital Item", "Cool DigitalItem", 2.00M, 1, emailDigitalItem);

    //Sample on how to create a Url Digital Item.
    DigitalItem urlDigitalItem = new DigitalItem(new Uri("http://www.google.com/download.aspx?myitem=1"), "Url Description for item");
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00M, 1, urlDigitalItem);

    //Sample on how to create a Key Digital Item
    DigitalItem keyDigitalItem = new DigitalItem("24-235-sdf-123541-53", "Key Description for item");
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00M, 1, keyDigitalItem);

    //Sample on how to create a Url/Key Digital Item
    DigitalItem keyUrlItem = new DigitalItem("24-235-sdf-123541-53", "http://www.google.com/download.aspx?myitem=1", "Url/Key Description for item");
    Req.AddItem("Url Digital Item", "Cool Url DigitalItem", 2.00M, 1, keyUrlItem);
    
    //lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5);

    //Add a rule to tax all items at 7.5% for Ohio
    Req.AddStateTaxRule("OH", 7.5, true);

    GCheckoutResponse Resp = Req.Send();
    if (Resp.IsGood) {
      Response.Redirect(Resp.RedirectUrl, true);
    }
    else {
      Response.Write("Resp.ResponseXml = " + Resp.ResponseXml + "<br>");
      Response.Write("Resp.RedirectUrl = " + Resp.RedirectUrl + "<br>");
      Response.Write("Resp.IsGood = " + Resp.IsGood + "<br>");
      Response.Write("Resp.ErrorMessage = " + Resp.ErrorMessage + "<br>");
    }
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
