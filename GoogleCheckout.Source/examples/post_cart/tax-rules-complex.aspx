<%@ Page Language="C#" %>
<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>

<script runat="server" language="c#">

  public void Page_Load(Object sender, EventArgs e) {
    // If the items in the cart aren't eligible for Google Checkout, uncomment line below.
    //GCheckoutButton1.Enabled = false;
  }
  
  private void PostCartToGoogle(object sender, System.Web.UI.ImageClickEventArgs e) {
    CheckoutShoppingCartRequest Req = GCheckoutButton1.CreateRequest();

    //we are going to create an alternate tax table for candy in ohio
    //we are then going to assign it to ohio only.
    //so if the customer is in ohio, they will be charged 10% tax
    //if they are in the rest of the country, they will pay 2% tax
    //This is because we are going to set up a default tax.
    AlternateTaxTable att = new AlternateTaxTable("candy");
    //if they are in the zip code 44256, tax them 15%
    att.AddZipTaxRule("44256", .15);
    //now add the state. you must go from most specific to least specific
    //http://code.google.com/apis/checkout/developer/Google_Checkout_XML_API_Taxes.html#Ordering_Tax_Rules
    att.AddStateTaxRule("OH", .10);
    Req.AlternateTaxTables.Add(att);
    
    Req.AddItem("Mars bar", "Packed with peanuts", 0.75m, 2, att);

    //default tax, all people not in ohio will be charged 2% for the candy.
    //if they are in Ohio, they will be charged 10%
    Req.AddPostalAreaTaxRule("US", .02, true);
    //if they are in Canada, we want to charge 8%
    Req.AddPostalAreaTaxRule("CA", .08, true);
    //if they are in the UK, charge 4%
    Req.AddPostalAreaTaxRule("GB", .04, true);
    
    //lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5);

    Req.AddZipTaxRule("100*", 0.08375, false);
    Req.AddStateTaxRule("NY", 0.0400, true);

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>Example of Complex Tax Rules</title>
</head>
<body>
  This page demonstrates a simple cart post.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
