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

    //You can also just pass a string of information, either way it will save.
    Req.MerchantPrivateData = "Xml or other string that you would like to pass with the order.";

    //you could have also just done the following (commented out so the value is not updated)
    //you can only set the MerchantPrivateData as a string once, it will override any value given to it.
    //Req.MerchantPrivateData = "This is something I need to know";

    //You can mix and match Xml with the string setting as needed, the values will be maintained.
    // Add a "shopper-id" node.
    System.Xml.XmlDocument tempDoc = new System.Xml.XmlDocument();
    System.Xml.XmlNode tempNode = tempDoc.CreateElement("shopper-id");
    tempNode.InnerText = "1234567890";
    Req.AddMerchantPrivateDataNode(tempNode);

    // Now we are going to add a few products.
    Req.AddItem("Candy Bar", "Big Candy bar that costs $1.00", "arx-112", 1.00m, 1);

    //Set the item without a merchant-item-id but with a Merchant-private-data string
    Req.AddItem("Fat Free Candy Bar", "Candy bar that looks like paper pulp", 0.45m, 2, "Merchant Private Data 1");

    //You can also call the AddItem Method and set the property on the Item itself as a property.
    ShoppingCartItem cartItem = Req.AddItem("Flat Candy", "Candy run over by the truck", "candy-flat", 0.02m, 1);
    cartItem.MerchantPrivateItemData = "Merchant Private Data 2";

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

<html>
<head>
  <title>Cart post with merchant private data</title>
</head>
<body>
  This page demonstrates a cart post with merchant private data.
  <p />
  <form id="Form1" method="post" runat="server">
    <cc1:GCheckoutButton ID="GCheckoutButton1" OnClick="PostCartToGoogle" runat="server" />
  </form>
</body>
</html>
