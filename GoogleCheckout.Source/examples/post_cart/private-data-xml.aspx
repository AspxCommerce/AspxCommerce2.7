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

    // First we are going to create a multi node merchant-private data.
    // We are going to treat it like a name value pair so we can store multiple values.
    // You can do this in many different ways, including creating many XmlDocuments.
    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
    doc.LoadXml("<root />");
    System.Xml.XmlElement element = doc.CreateElement("more");
    element.InnerText = "We need to know this";
    doc.DocumentElement.AppendChild(element);
    Req.AddMerchantPrivateDataNode(doc.DocumentElement);

    // Add a "shopper-id" node.
    System.Xml.XmlDocument tempDoc = new System.Xml.XmlDocument();
    System.Xml.XmlNode tempNode = tempDoc.CreateElement("shopper-id");
    tempNode.InnerText = "1234567890";
    Req.AddMerchantPrivateDataNode(tempNode);

    // Add a "cart-id" node.
    System.Xml.XmlNode tempNode2 = tempDoc.CreateElement("cart-id");
    tempNode2.InnerText = "0987654321";
    Req.AddMerchantPrivateDataNode(tempNode2);

    // We just created this structure on the order level:
    // <merchant-private-data>
    //   <root xmlns="">
    //     <more>We need to know this</more>
    //   </root>
    //   <shopper-id xmlns="">1234567890</shopper-id>
    //   <cart-id xmlns="">0987654321</cart-id>
    // </merchant-private-data>

    // Now we are going to add a few products.
    Req.AddItem("Candy Bar", "Big Candy bar that costs $1.00", "arx-112", 1.00m, 1);

    // Now we are going to create multiple xml nodes that can be passed into the next item.
    tempNode = tempDoc.CreateElement("supplier-id");
    tempNode.InnerText = "ABC Candy Company";
    Req.AddItem("Fat Free Candy Bar", "Candy bar that looks like paper pulp", "candy-paper", 0.45m, 2, tempNode);

    tempNode = tempDoc.CreateElement("supplier-id");
    tempNode.InnerText = "ABC Candy Company";
    tempNode2 = tempDoc.CreateElement("quantity-on-hand");
    tempNode2.InnerText = "45";
    Req.AddItem("Flat Candy", "Candy run over by the truck", "candy-flat", 0.02m, 200, tempNode, tempNode2);

    //lets make sure we can add 2 different flat rate shipping amounts

    Req.AddFlatRateShippingMethod("UPS Ground", 5);
    Req.AddFlatRateShippingMethod("UPS 2 Day Air", 25);

    //lets try adding a Carrier Calculated Shipping Type

    //The first thing that needs to be done for carrier calculated shipping is we must set the FOB address.
    Req.AddShippingPackage("main", "Cleveland", "OH", "44114", DeliveryAddressCategory.COMMERCIAL, 2, 3, 4);

    //The next thing we will do is add a Fedex Home Package.
    //We will set the default to 3.99, the Pickup to Regular Pickup, the additional fixed charge to 1.29 and the discount to 2.5%
    Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Home_Delivery, 3.99m, CarrierPickup.REGULAR_PICKUP, 1.29m, -2.5);
    Req.AddCarrierCalculatedShippingOption(ShippingType.Fedex_Second_Day, 9.99m, CarrierPickup.REGULAR_PICKUP, 2.34m, -24.5);

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
