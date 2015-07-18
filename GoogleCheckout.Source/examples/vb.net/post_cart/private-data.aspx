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
    
    'You can also just pass a string of information, either way it will save.
    Req.MerchantPrivateData = "Xml or other string that you would like to pass with the order."
    
    'you could have also just done the following (commented out so the value is not updated)
    'you can only set the MerchantPrivateData as a string once, it will override any value given to it.
    'Req.MerchantPrivateData = "This is something I need to know";
    'You can mix and match Xml with the string setting as needed, the values will be maintained.
    ' Add a "shopper-id" node.
    Dim tempDoc As System.Xml.XmlDocument = New System.Xml.XmlDocument
    Dim tempNode As System.Xml.XmlNode = tempDoc.CreateElement("shopper-id")
    tempNode.InnerText = "1234567890"
    Req.AddMerchantPrivateDataNode(tempNode)
    
    ' Now we are going to add a few products.
    Req.AddItem("Candy Bar", "Big Candy bar that costs $1.00", "arx-112", 1, 1)
    
    'Set the item without a merchant-item-id but with a Merchant-private-data string
    Req.AddItem("Fat Free Candy Bar", "Candy bar that looks like paper pulp", 0.45D, 2, "Merchant Private Data 1")
    
    'You can also call the AddItem Method and set the property on the Item itself as a property.
    Dim cartItem As ShoppingCartItem = Req.AddItem("Flat Candy", "Candy run over by the truck", "candy-flat", 0.02D, 1)
    cartItem.MerchantPrivateItemData = "Merchant Private Data 2"
    
    'lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5)
    
    'Add a rule to tax all items at 7.5% for Ohio
    Req.AddStateTaxRule("OH", 7.5, True)
    
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

<html>
  <head>
    <title>Cart post with merchant private data</title>
  </head>

  <body>
    This page demonstrates a cart post with merchant private data.
    <br />
    <form id="Form1" method="post" runat="server">
      <cc1:GCheckoutButton id="GCheckoutButton1" onclick="PostCartToGoogle" runat="server" />
    </form>
  </body>
</html>
