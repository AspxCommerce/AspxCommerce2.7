<%@ Page Language="C#" %>
<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>

<script runat="server" language="c#">

  public void Page_Load(Object sender, EventArgs e) {
    // If the items in the cart aren't eligible for Google Checkout, uncomment line below.
    //GCheckoutButton1.Enabled = false;
  }

  //This rule is discussed at the following url
  //http://code.google.com/apis/checkout/developer/Google_Checkout_XML_API_Taxes.html
  //Example 2
  
  private void PostCartToGoogle(object sender, System.Web.UI.ImageClickEventArgs e) {
    CheckoutShoppingCartRequest Req = GCheckoutButton1.CreateRequest();
    Req.AddItem("Mars bar", "Packed with peanuts", 0.75m, 2);

    //lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5);

    //Add a rule to tax all items at 6% for CT
    Req.AddStateTaxRule("CT", .06, true);
    //Add a rule to tax all items at 5% for MD
    Req.AddStateTaxRule("MD", .05, false);

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
<head>
  <title>Example 2: Charging a single tax rate in one state</title>
</head>
<body>
  This page demonstrates a simple cart post.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
